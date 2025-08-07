using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Klei;
using Newtonsoft.Json;
using STRINGS;
using UnityEngine;

namespace KMod
{
	// Token: 0x02000F78 RID: 3960
	public class Manager
	{
		// Token: 0x06007BE6 RID: 31718 RVA: 0x0031380D File Offset: 0x00311A0D
		public static string GetDirectory()
		{
			return Path.Combine(Util.RootFolder(), "mods/");
		}

		// Token: 0x06007BE8 RID: 31720 RVA: 0x0031385C File Offset: 0x00311A5C
		public void LoadModDBAndInitialize()
		{
			string filename = this.GetFilename();
			try
			{
				if (FileUtil.FileExists(filename, 0))
				{
					Manager.PersistentData persistentData = JsonConvert.DeserializeObject<Manager.PersistentData>(File.ReadAllText(filename));
					this.mods = persistentData.mods;
				}
			}
			catch (Exception)
			{
				global::Debug.LogWarningFormat(UI.FRONTEND.MODS.DB_CORRUPT, new object[] { filename });
				this.mods = new List<Mod>();
			}
			foreach (Mod mod in this.mods)
			{
				if (mod.enabledForDlc == null)
				{
					mod.SetEnabledForDlc("", mod.enabled);
				}
			}
			List<Mod> list = new List<Mod>();
			bool flag = false;
			foreach (Mod mod2 in this.mods)
			{
				Mod.Status status = mod2.status;
				if (status != Mod.Status.UninstallPending)
				{
					if (status == Mod.Status.ReinstallPending)
					{
						global::Debug.LogFormat("Latent reinstall of mod {0}", new object[] { mod2.title });
						if (!string.IsNullOrEmpty(mod2.reinstall_path) && File.Exists(mod2.reinstall_path))
						{
							bool flag2 = mod2.IsEnabledForActiveDlc();
							mod2.file_source = new ZipFile(mod2.reinstall_path);
							mod2.SetEnabledForActiveDlc(false);
							if (mod2.Uninstall())
							{
								mod2.Install();
								if (mod2.status == Mod.Status.Installed)
								{
									mod2.SetEnabledForActiveDlc(flag2);
								}
							}
							flag = true;
						}
						else if (mod2.IsEnabledForActiveDlc())
						{
							mod2.SetEnabledForActiveDlc(false);
							flag = true;
						}
					}
				}
				else
				{
					global::Debug.LogFormat("Latent uninstall of mod {0} from {1}", new object[]
					{
						mod2.title,
						mod2.label.install_path
					});
					if (mod2.Uninstall())
					{
						list.Add(mod2);
					}
					else
					{
						DebugUtil.Assert(mod2.status == Mod.Status.UninstallPending);
						global::Debug.LogFormat("\t...failed to uninstall mod {0}", new object[] { mod2.title });
					}
					if (mod2.status != Mod.Status.UninstallPending)
					{
						flag = true;
					}
				}
				if (!string.IsNullOrEmpty(mod2.reinstall_path))
				{
					mod2.reinstall_path = null;
					flag = true;
				}
			}
			foreach (Mod mod3 in list)
			{
				this.mods.Remove(mod3);
			}
			foreach (Mod mod4 in this.mods)
			{
				mod4.ScanContent();
			}
			if (flag)
			{
				this.Save();
			}
		}

		// Token: 0x06007BE9 RID: 31721 RVA: 0x00313B7C File Offset: 0x00311D7C
		public void Shutdown()
		{
			foreach (Mod mod in this.mods)
			{
				mod.Unload((Content)0);
			}
		}

		// Token: 0x06007BEA RID: 31722 RVA: 0x00313BD0 File Offset: 0x00311DD0
		public void Sanitize(GameObject parent)
		{
			ListPool<Label, Manager>.PooledList pooledList = ListPool<Label, Manager>.Allocate();
			foreach (Mod mod in this.mods)
			{
				if (!mod.is_subscribed)
				{
					pooledList.Add(mod.label);
				}
			}
			foreach (Label label in pooledList)
			{
				this.Unsubscribe(label, this);
			}
			pooledList.Recycle();
			this.Report(parent);
		}

		// Token: 0x06007BEB RID: 31723 RVA: 0x00313C84 File Offset: 0x00311E84
		public bool HaveMods()
		{
			foreach (Mod mod in this.mods)
			{
				if (mod.status == Mod.Status.Installed && mod.HasContent())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06007BEC RID: 31724 RVA: 0x00313CE8 File Offset: 0x00311EE8
		public List<Mod> GetAllCrashableMods()
		{
			List<Mod> list = new List<Mod>();
			foreach (Mod mod in this.mods)
			{
				if (mod.DevModCrashTriggered || (mod.status != Mod.Status.NotInstalled && mod.IsActive() && !mod.HasOnlyTranslationContent()))
				{
					list.Add(mod);
				}
			}
			return list;
		}

		// Token: 0x06007BED RID: 31725 RVA: 0x00313D64 File Offset: 0x00311F64
		public bool HasCrashableMods()
		{
			return this.GetAllCrashableMods().Count > 0;
		}

		// Token: 0x06007BEE RID: 31726 RVA: 0x00313D74 File Offset: 0x00311F74
		private void Install(Mod mod)
		{
			if (mod.status != Mod.Status.NotInstalled)
			{
				return;
			}
			global::Debug.LogFormat("\tInstalling mod: {0}", new object[] { mod.title });
			mod.Install();
			if (mod.status == Mod.Status.Installed)
			{
				global::Debug.Log("\tSuccessfully installed.");
				this.events.Add(new Event
				{
					event_type = EventType.Installed,
					mod = mod.label
				});
				return;
			}
			global::Debug.Log("\tFailed install. Will install on restart.");
			this.events.Add(new Event
			{
				event_type = EventType.InstallFailed,
				mod = mod.label
			});
			this.events.Add(new Event
			{
				event_type = EventType.RestartRequested,
				mod = mod.label
			});
		}

		// Token: 0x06007BEF RID: 31727 RVA: 0x00313E44 File Offset: 0x00312044
		private void Uninstall(Mod mod)
		{
			if (mod.status == Mod.Status.NotInstalled)
			{
				return;
			}
			global::Debug.LogFormat("\tUninstalling mod {0}", new object[] { mod.title });
			mod.Uninstall();
			if (mod.status == Mod.Status.UninstallPending)
			{
				global::Debug.Log("\tFailed. Will re-install on restart.");
				mod.status = Mod.Status.ReinstallPending;
				this.events.Add(new Event
				{
					event_type = EventType.RestartRequested,
					mod = mod.label
				});
			}
		}

		// Token: 0x06007BF0 RID: 31728 RVA: 0x00313EC0 File Offset: 0x003120C0
		public void Subscribe(Mod mod, object caller)
		{
			global::Debug.LogFormat("Subscribe to mod {0}", new object[] { mod.title });
			Mod mod2 = this.mods.Find((Mod candidate) => mod.label.Match(candidate.label));
			mod.is_subscribed = true;
			if (mod2 == null)
			{
				this.mods.Add(mod);
				this.Install(mod);
			}
			else
			{
				if (mod2.status == Mod.Status.UninstallPending)
				{
					mod2.status = Mod.Status.Installed;
					this.events.Add(new Event
					{
						event_type = EventType.Installed,
						mod = mod2.label
					});
				}
				bool flag = mod2.label.version != mod.label.version;
				bool flag2 = mod2.available_content != mod.available_content;
				bool flag3 = flag || flag2 || mod2.status == Mod.Status.ReinstallPending;
				if (flag)
				{
					this.events.Add(new Event
					{
						event_type = EventType.VersionUpdate,
						mod = mod.label
					});
				}
				if (flag2)
				{
					this.events.Add(new Event
					{
						event_type = EventType.AvailableContentChanged,
						mod = mod.label
					});
				}
				string root = mod.file_source.GetRoot();
				mod2.CopyPersistentDataTo(mod);
				int num = this.mods.IndexOf(mod2);
				this.mods.RemoveAt(num);
				this.mods.Insert(num, mod);
				if (!mod2.description.IsNullOrWhiteSpace())
				{
					mod.description = mod2.description;
				}
				else
				{
					mod.description = UI.FRONTEND.MODS.NO_DESCRIPTION;
				}
				if (!mod2.title.IsNullOrWhiteSpace())
				{
					mod.title = mod2.title;
				}
				if (flag3 || mod.status == Mod.Status.NotInstalled)
				{
					if (mod.IsEnabledForActiveDlc())
					{
						mod.reinstall_path = root;
						mod.status = Mod.Status.ReinstallPending;
						this.events.Add(new Event
						{
							event_type = EventType.RestartRequested,
							mod = mod.label
						});
					}
					else
					{
						if (flag3)
						{
							this.Uninstall(mod);
						}
						this.Install(mod);
					}
				}
				else
				{
					mod.file_source = mod2.file_source;
				}
			}
			mod.file_source.Dispose();
			this.dirty = true;
			this.Update(caller);
		}

		// Token: 0x06007BF1 RID: 31729 RVA: 0x0031417C File Offset: 0x0031237C
		public void Update(Mod mod, object caller)
		{
			global::Debug.LogFormat("Update mod {0}", new object[] { mod.title });
			Mod mod2 = this.mods.Find((Mod candidate) => mod.label.Match(candidate.label));
			DebugUtil.DevAssert(!string.IsNullOrEmpty(mod2.label.id), "Should be subscribed to a mod we are getting an Update notification for", null);
			if (mod2.status == Mod.Status.UninstallPending)
			{
				return;
			}
			this.events.Add(new Event
			{
				event_type = EventType.VersionUpdate,
				mod = mod.label
			});
			string root = mod.file_source.GetRoot();
			mod2.CopyPersistentDataTo(mod);
			mod.is_subscribed = mod2.is_subscribed;
			int num = this.mods.IndexOf(mod2);
			this.mods.RemoveAt(num);
			this.mods.Insert(num, mod);
			if (mod.IsEnabledForActiveDlc())
			{
				mod.reinstall_path = root;
				mod.status = Mod.Status.ReinstallPending;
				this.events.Add(new Event
				{
					event_type = EventType.RestartRequested,
					mod = mod.label
				});
			}
			else
			{
				this.Uninstall(mod);
				this.Install(mod);
			}
			mod.file_source.Dispose();
			this.dirty = true;
			this.Update(caller);
		}

		// Token: 0x06007BF2 RID: 31730 RVA: 0x0031430C File Offset: 0x0031250C
		public void Unsubscribe(Label label, object caller)
		{
			global::Debug.LogFormat("Unsubscribe from mod {0}", new object[] { label.ToString() });
			int num = 0;
			foreach (Mod mod in this.mods)
			{
				if (mod.label.Match(label))
				{
					global::Debug.LogFormat("\t...found it: {0}", new object[] { mod.title });
					break;
				}
				num++;
			}
			if (num == this.mods.Count)
			{
				global::Debug.LogFormat("\t...not found", Array.Empty<object>());
				return;
			}
			Mod mod2 = this.mods[num];
			mod2.SetEnabledForActiveDlc(false);
			mod2.Unload((Content)0);
			this.events.Add(new Event
			{
				event_type = EventType.Uninstalled,
				mod = mod2.label
			});
			if (mod2.IsActive())
			{
				global::Debug.LogFormat("\tCould not unload all content provided by mod {0} : {1}\nUninstall will likely fail", new object[]
				{
					mod2.title,
					mod2.label.ToString()
				});
				this.events.Add(new Event
				{
					event_type = EventType.RestartRequested,
					mod = mod2.label
				});
			}
			if (mod2.status == Mod.Status.Installed)
			{
				global::Debug.LogFormat("\tUninstall mod {0} : {1}", new object[]
				{
					mod2.title,
					mod2.label.ToString()
				});
				mod2.Uninstall();
			}
			if (mod2.status == Mod.Status.NotInstalled)
			{
				global::Debug.LogFormat("\t...success. Removing from management list {0} : {1}", new object[]
				{
					mod2.title,
					mod2.label.ToString()
				});
				this.mods.RemoveAt(num);
			}
			this.dirty = true;
			this.Update(caller);
		}

		// Token: 0x06007BF3 RID: 31731 RVA: 0x003144F8 File Offset: 0x003126F8
		public bool IsInDevMode()
		{
			return this.mods.Exists((Mod mod) => mod.IsEnabledForActiveDlc() && mod.label.distribution_platform == Label.DistributionPlatform.Dev);
		}

		// Token: 0x06007BF4 RID: 31732 RVA: 0x00314524 File Offset: 0x00312724
		public void Load(Content content)
		{
			if ((content & Content.DLL) != (Content)0 && this.load_user_mod_loader_dll)
			{
				if (!DLLLoader.LoadUserModLoaderDLL())
				{
					global::Debug.Log("Using builtin mod system.");
				}
				else
				{
					global::Debug.LogWarning("Using ModLoader.DLL for custom mod loading! This is not the standard mod loading method.");
				}
				this.load_user_mod_loader_dll = false;
			}
			foreach (Mod mod in this.mods)
			{
				if (mod.IsEnabledForActiveDlc())
				{
					mod.Load(content);
				}
			}
			if ((content & Content.DLL) != (Content)0)
			{
				IReadOnlyList<Mod> readOnlyList = this.mods.AsReadOnly();
				foreach (Mod mod2 in this.mods)
				{
					if (mod2.IsEnabledForActiveDlc())
					{
						mod2.PostLoad(readOnlyList);
					}
				}
			}
			bool flag = false;
			foreach (Mod mod3 in this.mods)
			{
				Content content2 = mod3.loaded_content & content;
				Content content3 = mod3.available_content & content;
				if (mod3.IsEnabledForActiveDlc() && content2 != content3)
				{
					mod3.SetCrashed();
					if (!mod3.IsEnabledForActiveDlc())
					{
						flag = true;
						this.events.Add(new Event
						{
							event_type = EventType.Deactivated,
							mod = mod3.label
						});
					}
					global::Debug.LogFormat("Failed to load mod {0}...disabling", new object[] { mod3.title });
					this.events.Add(new Event
					{
						event_type = EventType.LoadError,
						mod = mod3.label
					});
				}
			}
			if (flag)
			{
				this.Save();
			}
		}

		// Token: 0x06007BF5 RID: 31733 RVA: 0x0031470C File Offset: 0x0031290C
		public void Unload(Content content)
		{
			foreach (Mod mod in this.mods)
			{
				mod.Unload(content);
			}
		}

		// Token: 0x06007BF6 RID: 31734 RVA: 0x00314760 File Offset: 0x00312960
		public void Update(object change_source)
		{
			if (!this.dirty)
			{
				return;
			}
			this.dirty = false;
			this.Save();
			if (this.on_update != null)
			{
				this.on_update(change_source);
			}
		}

		// Token: 0x06007BF7 RID: 31735 RVA: 0x00314790 File Offset: 0x00312990
		public bool MatchFootprint(List<Label> footprint, Content relevant_content)
		{
			if (footprint == null)
			{
				return true;
			}
			bool flag = true;
			bool flag2 = true;
			bool flag3 = false;
			int num = -1;
			Func<Label, Mod, bool> is_match = (Label label, Mod mod) => mod.label.Match(label);
			using (List<Label>.Enumerator enumerator = footprint.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Label label = enumerator.Current;
					bool flag4 = false;
					for (int num2 = num + 1; num2 != this.mods.Count; num2++)
					{
						Mod mod3 = this.mods[num2];
						num = num2;
						Content content = mod3.available_content & relevant_content;
						bool flag5 = content > (Content)0;
						if (is_match(label, mod3))
						{
							if (flag5)
							{
								if (!mod3.IsEnabledForActiveDlc())
								{
									List<Event> list = this.events;
									Event @event = new Event
									{
										event_type = EventType.ExpectedActive,
										mod = label
									};
									list.Add(@event);
									flag = false;
								}
								else if (!mod3.AllActive(content))
								{
									List<Event> list2 = this.events;
									Event @event = new Event
									{
										event_type = EventType.LoadError,
										mod = label
									};
									list2.Add(@event);
								}
							}
							flag4 = true;
							break;
						}
						if (flag5 && mod3.IsEnabledForActiveDlc())
						{
							List<Event> list3 = this.events;
							Event @event = new Event
							{
								event_type = EventType.ExpectedInactive,
								mod = mod3.label
							};
							list3.Add(@event);
							flag3 = true;
						}
					}
					if (!flag4)
					{
						List<Event> list4 = this.events;
						Event @event = new Event
						{
							event_type = (this.mods.Exists((Mod candidate) => is_match(label, candidate)) ? EventType.OutOfOrder : EventType.NotFound),
							mod = label
						};
						list4.Add(@event);
						flag2 = false;
					}
				}
			}
			for (int num3 = num + 1; num3 != this.mods.Count; num3++)
			{
				Mod mod2 = this.mods[num3];
				if ((mod2.available_content & relevant_content) > (Content)0 && mod2.IsEnabledForActiveDlc())
				{
					List<Event> list5 = this.events;
					Event @event = new Event
					{
						event_type = EventType.ExpectedInactive,
						mod = mod2.label
					};
					list5.Add(@event);
					flag3 = true;
				}
			}
			return flag2 && flag && !flag3;
		}

		// Token: 0x06007BF8 RID: 31736 RVA: 0x00314A24 File Offset: 0x00312C24
		private string GetFilename()
		{
			return FileSystem.Normalize(Path.Combine(Manager.GetDirectory(), "mods.json"));
		}

		// Token: 0x06007BF9 RID: 31737 RVA: 0x00314A3C File Offset: 0x00312C3C
		public static void Dialog(GameObject parent = null, string title = null, string text = null, string confirm_text = null, global::System.Action on_confirm = null, string cancel_text = null, global::System.Action on_cancel = null, string configurable_text = null, global::System.Action on_configurable_clicked = null, Sprite image_sprite = null)
		{
			((ConfirmDialogScreen)KScreenManager.Instance.StartScreen(ScreenPrefabs.Instance.ConfirmDialogScreen.gameObject, parent ?? Global.Instance.globalCanvas)).PopupConfirmDialog(text, on_confirm, on_cancel, configurable_text, on_configurable_clicked, title, confirm_text, cancel_text, image_sprite);
		}

		// Token: 0x06007BFA RID: 31738 RVA: 0x00314A8C File Offset: 0x00312C8C
		private static string MakeModList(List<Event> events, EventType event_type)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine();
			int num = 30;
			foreach (Event @event in events)
			{
				if (@event.event_type == event_type)
				{
					stringBuilder.AppendLine(@event.mod.title);
					if (--num <= 0)
					{
						stringBuilder.AppendLine(UI.FRONTEND.MOD_DIALOGS.ADDITIONAL_MOD_EVENTS);
						break;
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06007BFB RID: 31739 RVA: 0x00314B20 File Offset: 0x00312D20
		private static string MakeEventList(List<Event> events)
		{
			return Manager.MakeEventList(events, "\n");
		}

		// Token: 0x06007BFC RID: 31740 RVA: 0x00314B30 File Offset: 0x00312D30
		private static string MakeEventList(List<Event> events, string prefix)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(prefix);
			string text = null;
			string text2 = null;
			int num = 30;
			foreach (Event @event in events)
			{
				Event.GetUIStrings(@event.event_type, out text, out text2);
				stringBuilder.AppendFormat("{0}: {1}", text, @event.mod.title);
				if (!string.IsNullOrEmpty(@event.details))
				{
					stringBuilder.AppendFormat(" ({0})", @event.details);
				}
				stringBuilder.Append("\n");
				if (--num <= 0)
				{
					stringBuilder.AppendLine(UI.FRONTEND.MOD_DIALOGS.ADDITIONAL_MOD_EVENTS);
					break;
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06007BFD RID: 31741 RVA: 0x00314C08 File Offset: 0x00312E08
		private static string MakeModList(List<Event> events)
		{
			return Manager.MakeModList(events, "\n");
		}

		// Token: 0x06007BFE RID: 31742 RVA: 0x00314C18 File Offset: 0x00312E18
		private static string MakeModList(List<Event> events, string prefix)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(prefix);
			HashSetPool<string, Manager>.PooledHashSet pooledHashSet = HashSetPool<string, Manager>.Allocate();
			int num = 30;
			foreach (Event @event in events)
			{
				if (pooledHashSet.Add(@event.mod.title))
				{
					stringBuilder.AppendLine(@event.mod.title);
					if (--num <= 0)
					{
						stringBuilder.AppendLine(UI.FRONTEND.MOD_DIALOGS.ADDITIONAL_MOD_EVENTS);
						break;
					}
				}
			}
			pooledHashSet.Recycle();
			return stringBuilder.ToString();
		}

		// Token: 0x06007BFF RID: 31743 RVA: 0x00314CC8 File Offset: 0x00312EC8
		private void LoadFailureDialog(GameObject parent)
		{
			if (this.events.Count == 0)
			{
				return;
			}
			foreach (Event @event in this.events)
			{
				if (@event.event_type == EventType.LoadError)
				{
					foreach (Mod mod in this.mods)
					{
						if (!mod.IsLocal && mod.label.Match(@event.mod))
						{
							mod.status = Mod.Status.ReinstallPending;
						}
					}
				}
			}
			this.dirty = true;
			this.Update(this);
			string text = UI.FRONTEND.MOD_DIALOGS.LOAD_FAILURE.TITLE;
			string text2 = string.Format(UI.FRONTEND.MOD_DIALOGS.LOAD_FAILURE.MESSAGE, Manager.MakeModList(this.events, EventType.LoadError));
			string text3 = UI.FRONTEND.MOD_DIALOGS.RESTART.OK;
			string text4 = UI.FRONTEND.MOD_DIALOGS.RESTART.CANCEL;
			Manager.Dialog(parent, text, text2, text3, new global::System.Action(App.instance.Restart), text4, delegate
			{
			}, null, null, null);
			this.events.Clear();
		}

		// Token: 0x06007C00 RID: 31744 RVA: 0x00314E18 File Offset: 0x00313018
		private void DevRestartDialog(GameObject parent, bool is_crash)
		{
			if (this.events.Count == 0)
			{
				return;
			}
			if (is_crash)
			{
				Manager.Dialog(parent, UI.FRONTEND.MOD_DIALOGS.MOD_ERRORS_ON_BOOT.TITLE, string.Format(UI.FRONTEND.MOD_DIALOGS.MOD_ERRORS_ON_BOOT.DEV_MESSAGE, Manager.MakeEventList(this.events)), UI.FRONTEND.MOD_DIALOGS.RESTART.OK, delegate
				{
					foreach (Mod mod in this.mods)
					{
						mod.SetEnabledForActiveDlc(false);
					}
					this.dirty = true;
					this.Update(this);
					App.instance.Restart();
				}, UI.FRONTEND.MOD_DIALOGS.RESTART.CANCEL, delegate
				{
				}, null, null, null);
			}
			else
			{
				Manager.Dialog(parent, UI.FRONTEND.MOD_DIALOGS.MOD_EVENTS.TITLE, string.Format(UI.FRONTEND.MOD_DIALOGS.RESTART.DEV_MESSAGE, Manager.MakeEventList(this.events)), UI.FRONTEND.MOD_DIALOGS.RESTART.OK, delegate
				{
					App.instance.Restart();
				}, UI.FRONTEND.MOD_DIALOGS.RESTART.CANCEL, delegate
				{
				}, null, null, null);
			}
			this.events.Clear();
		}

		// Token: 0x06007C01 RID: 31745 RVA: 0x00314F30 File Offset: 0x00313130
		public void RestartDialog(string title, string message_format, global::System.Action on_cancel, bool with_details, GameObject parent, string cancel_text = null)
		{
			if (this.events.Count == 0)
			{
				return;
			}
			string text = string.Format(message_format, with_details ? Manager.MakeEventList(this.events, null) : Manager.MakeModList(this.events, null));
			string text2 = UI.FRONTEND.MOD_DIALOGS.RESTART.OK;
			string text3 = cancel_text ?? UI.FRONTEND.MOD_DIALOGS.RESTART.CANCEL;
			Manager.Dialog(parent, title, text, text2, new global::System.Action(App.instance.Restart), text3, on_cancel, null, null, null);
			this.events.Clear();
		}

		// Token: 0x06007C02 RID: 31746 RVA: 0x00314FB4 File Offset: 0x003131B4
		public void NotifyDialog(string title, string message_format, GameObject parent)
		{
			if (this.events.Count == 0)
			{
				return;
			}
			Manager.Dialog(parent, title, string.Format(message_format, Manager.MakeEventList(this.events)), null, null, null, null, null, null, null);
			this.events.Clear();
		}

		// Token: 0x06007C03 RID: 31747 RVA: 0x00314FFC File Offset: 0x003131FC
		public void SearchForModsInStackTrace(StackTrace stackTrace)
		{
			foreach (StackFrame stackFrame in stackTrace.GetFrames())
			{
				if (stackFrame != null)
				{
					Assembly assembly = null;
					MethodBase method = stackFrame.GetMethod();
					if (method != null)
					{
						Type declaringType = method.DeclaringType;
						if (declaringType != null)
						{
							assembly = declaringType.Assembly;
						}
					}
					foreach (Mod mod in this.mods)
					{
						if (mod.loaded_mod_data != null && !mod.foundInStackTrace)
						{
							if (assembly != null && mod.loaded_mod_data.dlls.Contains(assembly))
							{
								global::Debug.Log(string.Format("{0}'s assembly declared the method {1}:{2} in the stack trace, adding to referenced mods list", mod.title, method.DeclaringType, method.Name));
								mod.foundInStackTrace = true;
							}
							else if (method != null && mod.loaded_mod_data.patched_methods.Contains(method))
							{
								global::Debug.Log(string.Format("{0}'s patched_method {1}:{2} appears in the stack trace, adding to referenced mods list", mod.title, method.DeclaringType, method.Name));
								mod.foundInStackTrace = true;
							}
						}
					}
				}
			}
			string text = stackTrace.ToString();
			this.SearchForModsInStackTrace(text);
		}

		// Token: 0x06007C04 RID: 31748 RVA: 0x00315164 File Offset: 0x00313364
		public void SearchForModsInStackTrace(string stackStr)
		{
			foreach (Mod mod in this.mods)
			{
				if (mod.loaded_mod_data != null && !mod.foundInStackTrace)
				{
					foreach (MethodBase methodBase in mod.loaded_mod_data.patched_methods)
					{
						if (new Regex(Regex.Escape(methodBase.DeclaringType.ToString()) + "[.:]" + Regex.Escape(methodBase.Name.ToString())).Match(stackStr).Success)
						{
							global::Debug.Log(string.Format("{0}'s patched_method {1}.{2} matched in the stack trace, adding to referenced mods list", mod.title, methodBase.DeclaringType, methodBase.Name));
							mod.foundInStackTrace = true;
							break;
						}
					}
				}
			}
		}

		// Token: 0x06007C05 RID: 31749 RVA: 0x0031526C File Offset: 0x0031346C
		public void HandleErrors(List<YamlIO.Error> world_gen_errors)
		{
			string text = FileSystem.Normalize(Manager.GetDirectory());
			ListPool<Mod, Manager>.PooledList pooledList = ListPool<Mod, Manager>.Allocate();
			foreach (YamlIO.Error error in world_gen_errors)
			{
				string text2 = ((error.file.source != null) ? FileSystem.Normalize(error.file.source.GetRoot()) : string.Empty);
				YamlIO.LogError(error, text2.Contains(text));
				if (error.severity != YamlIO.Error.Severity.Recoverable && text2.Contains(text))
				{
					foreach (Mod mod in this.mods)
					{
						if (mod.IsEnabledForActiveDlc() && text2.Contains(mod.label.install_path))
						{
							this.events.Add(new Event
							{
								event_type = EventType.BadWorldGen,
								mod = mod.label,
								details = Path.GetFileName(error.file.full_path)
							});
							break;
						}
					}
				}
			}
			foreach (Mod mod2 in pooledList)
			{
				mod2.SetCrashed();
				if (!mod2.IsDev)
				{
					this.events.Add(new Event
					{
						event_type = EventType.Deactivated,
						mod = mod2.label
					});
				}
				this.dirty = true;
			}
			pooledList.Recycle();
			this.Update(this);
		}

		// Token: 0x06007C06 RID: 31750 RVA: 0x00315468 File Offset: 0x00313668
		public void Report(GameObject parent)
		{
			if (this.events.Count == 0)
			{
				return;
			}
			for (int i = 0; i < this.events.Count; i++)
			{
				Event @event = this.events[i];
				for (int num = this.events.Count - 1; num != i; num--)
				{
					if (this.events[num].event_type == @event.event_type && this.events[num].mod.Match(@event.mod) && this.events[num].details == @event.details)
					{
						this.events.RemoveAt(num);
					}
				}
			}
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			foreach (Event event2 in this.events)
			{
				EventType event_type = event2.event_type;
				if (event_type <= EventType.ActiveDuringCrash)
				{
					if (event_type != EventType.LoadError)
					{
						if (event_type == EventType.ActiveDuringCrash)
						{
							flag = true;
						}
					}
					else
					{
						flag2 = true;
					}
				}
				else if (event_type != EventType.RestartRequested)
				{
					if (event_type == EventType.Deactivated)
					{
						if ((this.FindMod(event2.mod).available_content & (Content.LayerableFiles | Content.Strings | Content.DLL | Content.Translation | Content.Animation)) != (Content)0)
						{
							flag3 = true;
						}
					}
				}
				else
				{
					flag3 = true;
				}
			}
			flag3 = flag || flag2 || flag3;
			bool flag4 = this.IsInDevMode();
			if (flag3 && flag4)
			{
				this.DevRestartDialog(parent, flag);
				return;
			}
			if (flag2)
			{
				this.LoadFailureDialog(parent);
				return;
			}
			if (flag)
			{
				this.RestartDialog(UI.FRONTEND.MOD_DIALOGS.MOD_ERRORS_ON_BOOT.TITLE, UI.FRONTEND.MOD_DIALOGS.MOD_ERRORS_ON_BOOT.MESSAGE, null, false, parent, null);
				return;
			}
			if (flag3)
			{
				this.RestartDialog(UI.FRONTEND.MOD_DIALOGS.MOD_EVENTS.TITLE, UI.FRONTEND.MOD_DIALOGS.RESTART.MESSAGE, null, true, parent, null);
				return;
			}
			this.NotifyDialog(UI.FRONTEND.MOD_DIALOGS.MOD_EVENTS.TITLE, flag4 ? UI.FRONTEND.MOD_DIALOGS.MOD_EVENTS.DEV_MESSAGE : UI.FRONTEND.MOD_DIALOGS.MOD_EVENTS.MESSAGE, parent);
		}

		// Token: 0x06007C07 RID: 31751 RVA: 0x0031565C File Offset: 0x0031385C
		public bool Save()
		{
			if (!FileUtil.CreateDirectory(Manager.GetDirectory(), 5))
			{
				return false;
			}
			using (FileStream stream = FileUtil.Create(this.GetFilename(), 5))
			{
				if (stream == null)
				{
					return false;
				}
				using (StreamWriter streamWriter = FileUtil.DoIODialog<StreamWriter>(() => new StreamWriter(stream), this.GetFilename(), null, 5))
				{
					if (streamWriter == null)
					{
						return false;
					}
					string text = JsonConvert.SerializeObject(new Manager.PersistentData(this.current_version, this.mods), Formatting.Indented);
					streamWriter.Write(text);
				}
			}
			return true;
		}

		// Token: 0x06007C08 RID: 31752 RVA: 0x0031571C File Offset: 0x0031391C
		public Mod FindMod(Label label)
		{
			foreach (Mod mod in this.mods)
			{
				if (mod.label.Equals(label))
				{
					return mod;
				}
			}
			return null;
		}

		// Token: 0x06007C09 RID: 31753 RVA: 0x00315788 File Offset: 0x00313988
		public bool IsModEnabled(Label id)
		{
			Mod mod = this.FindMod(id);
			return mod != null && mod.IsEnabledForActiveDlc();
		}

		// Token: 0x06007C0A RID: 31754 RVA: 0x003157A8 File Offset: 0x003139A8
		public bool EnableMod(Label id, bool enabled, object caller)
		{
			Mod mod = this.FindMod(id);
			if (mod == null)
			{
				return false;
			}
			if (mod.IsEmpty())
			{
				return false;
			}
			if (mod.IsEnabledForActiveDlc() == enabled)
			{
				return false;
			}
			mod.SetEnabledForActiveDlc(enabled);
			if (enabled)
			{
				mod.Load((Content)0);
			}
			else
			{
				mod.Unload((Content)0);
			}
			this.dirty = true;
			this.Update(caller);
			return true;
		}

		// Token: 0x06007C0B RID: 31755 RVA: 0x00315800 File Offset: 0x00313A00
		public void Reinsert(int source_index, int target_index, bool move_to_end, object caller)
		{
			if (move_to_end)
			{
				target_index = this.mods.Count;
			}
			DebugUtil.Assert(source_index != target_index);
			if (source_index < -1 || this.mods.Count <= source_index)
			{
				return;
			}
			if (target_index < -1 || this.mods.Count <= target_index)
			{
				return;
			}
			Mod mod = this.mods[source_index];
			this.mods.RemoveAt(source_index);
			if (source_index > target_index)
			{
				target_index++;
			}
			if (target_index == this.mods.Count)
			{
				this.mods.Add(mod);
			}
			else
			{
				this.mods.Insert(target_index, mod);
			}
			this.dirty = true;
			this.Update(caller);
		}

		// Token: 0x06007C0C RID: 31756 RVA: 0x003158AC File Offset: 0x00313AAC
		public void SendMetricsEvent()
		{
			ListPool<string, Manager>.PooledList pooledList = ListPool<string, Manager>.Allocate();
			foreach (Mod mod in this.mods)
			{
				if (mod.IsEnabledForActiveDlc())
				{
					pooledList.Add(mod.title);
				}
			}
			DictionaryPool<string, object, Manager>.PooledDictionary pooledDictionary = DictionaryPool<string, object, Manager>.Allocate();
			pooledDictionary["ModCount"] = pooledList.Count;
			pooledDictionary["Mods"] = pooledList;
			ThreadedHttps<KleiMetrics>.Instance.SendEvent(pooledDictionary, "Mods");
			pooledDictionary.Recycle();
			pooledList.Recycle();
			KCrashReporter.haveActiveMods = pooledList.Count > 0;
		}

		// Token: 0x04005B22 RID: 23330
		public const Content all_content = Content.LayerableFiles | Content.Strings | Content.DLL | Content.Translation | Content.Animation;

		// Token: 0x04005B23 RID: 23331
		public const Content boot_content = Content.LayerableFiles | Content.Strings | Content.DLL | Content.Translation | Content.Animation;

		// Token: 0x04005B24 RID: 23332
		public const Content on_demand_content = (Content)0;

		// Token: 0x04005B25 RID: 23333
		public List<IDistributionPlatform> distribution_platforms = new List<IDistributionPlatform>();

		// Token: 0x04005B26 RID: 23334
		public List<Mod> mods = new List<Mod>();

		// Token: 0x04005B27 RID: 23335
		public List<Event> events = new List<Event>();

		// Token: 0x04005B28 RID: 23336
		private bool dirty = true;

		// Token: 0x04005B29 RID: 23337
		public Manager.OnUpdate on_update;

		// Token: 0x04005B2A RID: 23338
		private const int IO_OP_RETRY_COUNT = 5;

		// Token: 0x04005B2B RID: 23339
		private bool load_user_mod_loader_dll = true;

		// Token: 0x04005B2C RID: 23340
		private const int MAX_DIALOG_ENTRIES = 30;

		// Token: 0x04005B2D RID: 23341
		private int current_version = 1;

		// Token: 0x0200210F RID: 8463
		// (Invoke) Token: 0x0600B8EE RID: 47342
		public delegate void OnUpdate(object change_source);

		// Token: 0x02002110 RID: 8464
		private class PersistentData
		{
			// Token: 0x0600B8F1 RID: 47345 RVA: 0x003EAA4B File Offset: 0x003E8C4B
			public PersistentData()
			{
			}

			// Token: 0x0600B8F2 RID: 47346 RVA: 0x003EAA53 File Offset: 0x003E8C53
			public PersistentData(int version, List<Mod> mods)
			{
				this.version = version;
				this.mods = mods;
			}

			// Token: 0x04009739 RID: 38713
			public int version;

			// Token: 0x0400973A RID: 38714
			public List<Mod> mods;
		}
	}
}
