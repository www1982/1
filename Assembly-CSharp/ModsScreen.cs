using System;
using System.Collections.Generic;
using System.Linq;
using KMod;
using STRINGS;
using UnityEngine;

// Token: 0x02000C27 RID: 3111
public class ModsScreen : KModalScreen
{
	// Token: 0x06005E8F RID: 24207 RVA: 0x0022A5E8 File Offset: 0x002287E8
	protected override void OnActivate()
	{
		base.OnActivate();
		this.closeButtonTitle.onClick += this.Exit;
		this.closeButton.onClick += this.Exit;
		global::System.Action action = delegate
		{
			App.OpenWebURL("http://steamcommunity.com/workshop/browse/?appid=457140");
		};
		this.workshopButton.onClick += action;
		this.UpdateToggleAllButton();
		this.toggleAllButton.onClick += this.OnToggleAllClicked;
		Global.Instance.modManager.Sanitize(base.gameObject);
		this.mod_footprint.Clear();
		foreach (Mod mod in Global.Instance.modManager.mods)
		{
			if (mod.IsEnabledForActiveDlc())
			{
				this.mod_footprint.Add(mod.label);
				if ((mod.loaded_content & (Content.LayerableFiles | Content.Strings | Content.DLL | Content.Translation | Content.Animation)) == (mod.available_content & (Content.LayerableFiles | Content.Strings | Content.DLL | Content.Translation | Content.Animation)))
				{
					mod.Uncrash();
				}
			}
		}
		this.BuildDisplay();
		Manager modManager = Global.Instance.modManager;
		modManager.on_update = (Manager.OnUpdate)Delegate.Combine(modManager.on_update, new Manager.OnUpdate(this.RebuildDisplay));
	}

	// Token: 0x06005E90 RID: 24208 RVA: 0x0022A740 File Offset: 0x00228940
	protected override void OnDeactivate()
	{
		Manager modManager = Global.Instance.modManager;
		modManager.on_update = (Manager.OnUpdate)Delegate.Remove(modManager.on_update, new Manager.OnUpdate(this.RebuildDisplay));
		base.OnDeactivate();
	}

	// Token: 0x06005E91 RID: 24209 RVA: 0x0022A774 File Offset: 0x00228974
	private void Exit()
	{
		Global.Instance.modManager.Save();
		if (!Global.Instance.modManager.MatchFootprint(this.mod_footprint, Content.LayerableFiles | Content.Strings | Content.DLL | Content.Translation | Content.Animation))
		{
			Global.Instance.modManager.RestartDialog(UI.FRONTEND.MOD_DIALOGS.MODS_SCREEN_CHANGES.TITLE, UI.FRONTEND.MOD_DIALOGS.MODS_SCREEN_CHANGES.MESSAGE, new global::System.Action(this.Deactivate), true, base.gameObject, null);
		}
		else
		{
			this.Deactivate();
		}
		Global.Instance.modManager.events.Clear();
	}

	// Token: 0x06005E92 RID: 24210 RVA: 0x0022A7FE File Offset: 0x002289FE
	private void RebuildDisplay(object change_source)
	{
		if (change_source != this)
		{
			this.BuildDisplay();
		}
	}

	// Token: 0x06005E93 RID: 24211 RVA: 0x0022A80A File Offset: 0x00228A0A
	private bool ShouldDisplayMod(Mod mod)
	{
		return mod.status != Mod.Status.NotInstalled && mod.status != Mod.Status.UninstallPending && !mod.HasOnlyTranslationContent();
	}

	// Token: 0x06005E94 RID: 24212 RVA: 0x0022A828 File Offset: 0x00228A28
	private void BuildDisplay()
	{
		foreach (ModsScreen.DisplayedMod displayedMod in this.displayedMods)
		{
			if (displayedMod.rect_transform != null)
			{
				global::UnityEngine.Object.Destroy(displayedMod.rect_transform.gameObject);
			}
		}
		this.displayedMods.Clear();
		ModsScreen.ModOrderingDragListener modOrderingDragListener = new ModsScreen.ModOrderingDragListener(this, this.displayedMods);
		for (int num = 0; num != Global.Instance.modManager.mods.Count; num++)
		{
			Mod mod = Global.Instance.modManager.mods[num];
			if (this.ShouldDisplayMod(mod))
			{
				HierarchyReferences hierarchyReferences = Util.KInstantiateUI<HierarchyReferences>(this.entryPrefab, this.entryParent.gameObject, false);
				this.displayedMods.Add(new ModsScreen.DisplayedMod
				{
					rect_transform = hierarchyReferences.gameObject.GetComponent<RectTransform>(),
					mod_index = num
				});
				hierarchyReferences.GetComponent<DragMe>().listener = modOrderingDragListener;
				LocText reference = hierarchyReferences.GetReference<LocText>("Title");
				string text = mod.title;
				StringEntry stringEntry;
				if (Strings.TryGet(mod.title, out stringEntry))
				{
					text = stringEntry;
				}
				hierarchyReferences.name = mod.title;
				ToolTip reference2 = hierarchyReferences.GetReference<ToolTip>("Description");
				if (mod.available_content == (Content)0)
				{
					switch (mod.contentCompatability)
					{
					case ModContentCompatability.NoContent:
						text += UI.FRONTEND.MODS.CONTENT_FAILURE.NO_CONTENT;
						reference2.toolTip = UI.FRONTEND.MODS.CONTENT_FAILURE.NO_CONTENT_TOOLTIP;
						goto IL_039F;
					case ModContentCompatability.OldAPI:
						text += UI.FRONTEND.MODS.CONTENT_FAILURE.OLD_API;
						reference2.toolTip = UI.FRONTEND.MODS.CONTENT_FAILURE.OLD_API_TOOLTIP;
						goto IL_039F;
					}
					string text2 = GlobalAssets.Instance.colorSet.GetColorByName("statusItemBad").ToHexString();
					string text3 = UI.FRONTEND.MODS.CONTENT_FAILURE.DISABLED_CONTENT_TOOLTIP + "\n\n";
					if (mod.GetRequiredDlcIds() != null)
					{
						text3 += UI.FRONTEND.MODS.CONTENT_FAILURE.DISABLED_CONTENT_TOOLTIP_REQUIRED;
						foreach (string text4 in mod.GetRequiredDlcIds())
						{
							if (DlcManager.IsContentSubscribed(text4))
							{
								text3 = text3 + "\n     •  <i>" + DlcManager.GetDlcTitleNoFormatting(text4) + "</i>";
							}
							else
							{
								text3 = string.Concat(new string[]
								{
									text3,
									"\n     •  <i><color=#",
									text2,
									">",
									DlcManager.GetDlcTitleNoFormatting(text4),
									"</color></i>"
								});
							}
						}
						if (mod.GetForbiddenDlcIds() != null)
						{
							text3 += "\n\n";
						}
					}
					if (mod.GetForbiddenDlcIds() != null)
					{
						text3 += UI.FRONTEND.MODS.CONTENT_FAILURE.DISABLED_CONTENT_TOOLTIP_FORBIDDEN_DLC;
						foreach (string text5 in mod.GetForbiddenDlcIds())
						{
							if (!DlcManager.IsContentSubscribed(text5))
							{
								text3 = text3 + "\n     •  <i>" + DlcManager.GetDlcTitleNoFormatting(text5) + "</i>";
							}
							else
							{
								text3 = string.Concat(new string[]
								{
									text3,
									"\n     •  <i><color=#",
									text2,
									">",
									DlcManager.GetDlcTitleNoFormatting(text5),
									"</color></i>"
								});
							}
						}
					}
					reference2.toolTip = text3;
					text += UI.FRONTEND.MODS.CONTENT_FAILURE.DISABLED_CONTENT;
				}
				IL_039F:
				reference.text = text;
				LocText reference3 = hierarchyReferences.GetReference<LocText>("Version");
				if (mod.packagedModInfo != null && mod.packagedModInfo.version != null && mod.packagedModInfo.version.Length > 0)
				{
					string text6 = mod.packagedModInfo.version;
					if (text6.StartsWith("V"))
					{
						text6 = "v" + text6.Substring(1, text6.Length - 1);
					}
					else if (!text6.StartsWith("v"))
					{
						text6 = "v" + text6;
					}
					reference3.text = text6;
					reference3.gameObject.SetActive(true);
				}
				else
				{
					reference3.gameObject.SetActive(false);
				}
				if (mod.available_content > (Content)0)
				{
					StringEntry stringEntry2;
					if (Strings.TryGet(mod.description, out stringEntry2))
					{
						reference2.toolTip = stringEntry2;
					}
					else
					{
						reference2.toolTip = mod.description;
					}
				}
				if (mod.crash_count != 0)
				{
					reference.color = Color.Lerp(Color.white, Color.red, (float)mod.crash_count / 3f);
				}
				KButton reference4 = hierarchyReferences.GetReference<KButton>("ManageButton");
				reference4.GetComponentInChildren<LocText>().text = (mod.IsLocal ? UI.FRONTEND.MODS.MANAGE_LOCAL : UI.FRONTEND.MODS.MANAGE);
				reference4.isInteractable = mod.is_managed;
				if (reference4.isInteractable)
				{
					reference4.GetComponent<ToolTip>().toolTip = mod.manage_tooltip;
					reference4.onClick += mod.on_managed;
				}
				KImage reference5 = hierarchyReferences.GetReference<KImage>("BG");
				MultiToggle toggle = hierarchyReferences.GetReference<MultiToggle>("EnabledToggle");
				toggle.ChangeState(mod.IsEnabledForActiveDlc() ? 1 : 0);
				if (mod.available_content != (Content)0)
				{
					reference5.defaultState = KImage.ColorSelector.Inactive;
					reference5.ColorState = KImage.ColorSelector.Inactive;
					MultiToggle toggle2 = toggle;
					toggle2.onClick = (global::System.Action)Delegate.Combine(toggle2.onClick, new global::System.Action(delegate
					{
						this.OnToggleClicked(toggle, mod.label);
					}));
					toggle.GetComponent<ToolTip>().OnToolTip = () => mod.IsEnabledForActiveDlc() ? UI.FRONTEND.MODS.TOOLTIPS.ENABLED : UI.FRONTEND.MODS.TOOLTIPS.DISABLED;
				}
				else
				{
					reference5.defaultState = KImage.ColorSelector.Disabled;
					reference5.ColorState = KImage.ColorSelector.Disabled;
				}
				hierarchyReferences.gameObject.SetActive(true);
			}
		}
		foreach (ModsScreen.DisplayedMod displayedMod2 in this.displayedMods)
		{
			displayedMod2.rect_transform.gameObject.SetActive(true);
		}
		int count = this.displayedMods.Count;
	}

	// Token: 0x06005E95 RID: 24213 RVA: 0x0022AEF8 File Offset: 0x002290F8
	private void OnToggleClicked(MultiToggle toggle, Label mod)
	{
		Manager modManager = Global.Instance.modManager;
		bool flag = modManager.IsModEnabled(mod);
		flag = !flag;
		toggle.ChangeState(flag ? 1 : 0);
		modManager.EnableMod(mod, flag, this);
		this.UpdateToggleAllButton();
	}

	// Token: 0x06005E96 RID: 24214 RVA: 0x0022AF38 File Offset: 0x00229138
	private bool AreAnyModsDisabled()
	{
		return Global.Instance.modManager.mods.Any((Mod mod) => !mod.IsEmpty() && !mod.IsEnabledForActiveDlc() && this.ShouldDisplayMod(mod));
	}

	// Token: 0x06005E97 RID: 24215 RVA: 0x0022AF5A File Offset: 0x0022915A
	private void UpdateToggleAllButton()
	{
		this.toggleAllButton.GetComponentInChildren<LocText>().text = (this.AreAnyModsDisabled() ? UI.FRONTEND.MODS.ENABLE_ALL : UI.FRONTEND.MODS.DISABLE_ALL);
	}

	// Token: 0x06005E98 RID: 24216 RVA: 0x0022AF88 File Offset: 0x00229188
	private void OnToggleAllClicked()
	{
		bool flag = this.AreAnyModsDisabled();
		Manager modManager = Global.Instance.modManager;
		foreach (Mod mod in modManager.mods)
		{
			if (this.ShouldDisplayMod(mod))
			{
				modManager.EnableMod(mod.label, flag, this);
			}
		}
		this.BuildDisplay();
		this.UpdateToggleAllButton();
	}

	// Token: 0x04003EF3 RID: 16115
	[SerializeField]
	private KButton closeButtonTitle;

	// Token: 0x04003EF4 RID: 16116
	[SerializeField]
	private KButton closeButton;

	// Token: 0x04003EF5 RID: 16117
	[SerializeField]
	private KButton toggleAllButton;

	// Token: 0x04003EF6 RID: 16118
	[SerializeField]
	private KButton workshopButton;

	// Token: 0x04003EF7 RID: 16119
	[SerializeField]
	private GameObject entryPrefab;

	// Token: 0x04003EF8 RID: 16120
	[SerializeField]
	private Transform entryParent;

	// Token: 0x04003EF9 RID: 16121
	private List<ModsScreen.DisplayedMod> displayedMods = new List<ModsScreen.DisplayedMod>();

	// Token: 0x04003EFA RID: 16122
	private List<Label> mod_footprint = new List<Label>();

	// Token: 0x02001D77 RID: 7543
	private struct DisplayedMod
	{
		// Token: 0x04008973 RID: 35187
		public RectTransform rect_transform;

		// Token: 0x04008974 RID: 35188
		public int mod_index;
	}

	// Token: 0x02001D78 RID: 7544
	private class ModOrderingDragListener : DragMe.IDragListener
	{
		// Token: 0x0600AE30 RID: 44592 RVA: 0x003C7602 File Offset: 0x003C5802
		public ModOrderingDragListener(ModsScreen screen, List<ModsScreen.DisplayedMod> mods)
		{
			this.screen = screen;
			this.mods = mods;
		}

		// Token: 0x0600AE31 RID: 44593 RVA: 0x003C761F File Offset: 0x003C581F
		public void OnBeginDrag(Vector2 pos)
		{
			this.startDragIdx = this.GetDragIdx(pos, false);
		}

		// Token: 0x0600AE32 RID: 44594 RVA: 0x003C7630 File Offset: 0x003C5830
		public void OnEndDrag(Vector2 pos)
		{
			if (this.startDragIdx < 0)
			{
				return;
			}
			int dragIdx = this.GetDragIdx(pos, true);
			if (dragIdx != this.startDragIdx)
			{
				int mod_index = this.mods[this.startDragIdx].mod_index;
				int num = ((0 <= dragIdx && dragIdx < this.mods.Count) ? this.mods[dragIdx].mod_index : (-1));
				Global.Instance.modManager.Reinsert(mod_index, num, dragIdx >= this.mods.Count, this);
				this.screen.BuildDisplay();
			}
		}

		// Token: 0x0600AE33 RID: 44595 RVA: 0x003C76C8 File Offset: 0x003C58C8
		private int GetDragIdx(Vector2 pos, bool halfPosition)
		{
			int num = -1;
			for (int i = 0; i < this.mods.Count; i++)
			{
				Vector2 vector;
				RectTransformUtility.ScreenPointToLocalPointInRectangle(this.mods[i].rect_transform, pos, null, out vector);
				if (!halfPosition)
				{
					vector += this.mods[i].rect_transform.rect.min;
				}
				if (vector.y >= 0f)
				{
					break;
				}
				num = i;
			}
			return num;
		}

		// Token: 0x04008975 RID: 35189
		private List<ModsScreen.DisplayedMod> mods;

		// Token: 0x04008976 RID: 35190
		private ModsScreen screen;

		// Token: 0x04008977 RID: 35191
		private int startDragIdx = -1;
	}
}
