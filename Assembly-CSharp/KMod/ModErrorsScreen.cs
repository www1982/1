using System;
using System.Collections.Generic;
using UnityEngine;

namespace KMod
{
	// Token: 0x02000F79 RID: 3961
	public class ModErrorsScreen : KScreen
	{
		// Token: 0x06007C0E RID: 31758 RVA: 0x003159D0 File Offset: 0x00313BD0
		public static bool ShowErrors(List<Event> events)
		{
			if (Global.Instance.modManager.events.Count == 0)
			{
				return false;
			}
			GameObject gameObject = GameObject.Find("Canvas");
			ModErrorsScreen modErrorsScreen = Util.KInstantiateUI<ModErrorsScreen>(Global.Instance.modErrorsPrefab, gameObject, false);
			modErrorsScreen.Initialize(events);
			modErrorsScreen.gameObject.SetActive(true);
			return true;
		}

		// Token: 0x06007C0F RID: 31759 RVA: 0x00315A24 File Offset: 0x00313C24
		private void Initialize(List<Event> events)
		{
			foreach (Event @event in events)
			{
				HierarchyReferences hierarchyReferences = Util.KInstantiateUI<HierarchyReferences>(this.entryPrefab, this.entryParent.gameObject, true);
				LocText reference = hierarchyReferences.GetReference<LocText>("Title");
				LocText reference2 = hierarchyReferences.GetReference<LocText>("Description");
				KButton reference3 = hierarchyReferences.GetReference<KButton>("Details");
				string text;
				string text2;
				Event.GetUIStrings(@event.event_type, out text, out text2);
				reference.text = text;
				reference.GetComponent<ToolTip>().toolTip = text2;
				reference2.text = @event.mod.title;
				ToolTip component = reference2.GetComponent<ToolTip>();
				if (component != null)
				{
					ToolTip toolTip = component;
					Label mod = @event.mod;
					toolTip.toolTip = mod.ToString();
				}
				reference3.isInteractable = false;
				Mod mod2 = Global.Instance.modManager.FindMod(@event.mod);
				if (mod2 != null)
				{
					if (component != null && !string.IsNullOrEmpty(mod2.description))
					{
						StringEntry stringEntry;
						if (Strings.TryGet(mod2.description, out stringEntry))
						{
							component.toolTip = stringEntry;
						}
						else
						{
							component.toolTip = mod2.description;
						}
					}
					if (mod2.on_managed != null)
					{
						reference3.onClick += mod2.on_managed;
						reference3.isInteractable = true;
					}
				}
			}
		}

		// Token: 0x06007C10 RID: 31760 RVA: 0x00315BA4 File Offset: 0x00313DA4
		protected override void OnActivate()
		{
			base.OnActivate();
			this.closeButtonTitle.onClick += this.Deactivate;
			this.closeButton.onClick += this.Deactivate;
		}

		// Token: 0x04005B2E RID: 23342
		[SerializeField]
		private KButton closeButtonTitle;

		// Token: 0x04005B2F RID: 23343
		[SerializeField]
		private KButton closeButton;

		// Token: 0x04005B30 RID: 23344
		[SerializeField]
		private GameObject entryPrefab;

		// Token: 0x04005B31 RID: 23345
		[SerializeField]
		private Transform entryParent;
	}
}
