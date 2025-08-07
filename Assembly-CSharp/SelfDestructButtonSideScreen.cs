using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000E2E RID: 3630
public class SelfDestructButtonSideScreen : SideScreenContent
{
	// Token: 0x060072FF RID: 29439 RVA: 0x002BC696 File Offset: 0x002BA896
	protected override void OnSpawn()
	{
		this.Refresh();
		this.button.onClick += this.TriggerDestruct;
	}

	// Token: 0x06007300 RID: 29440 RVA: 0x002BC6B5 File Offset: 0x002BA8B5
	public override int GetSideScreenSortOrder()
	{
		return -150;
	}

	// Token: 0x06007301 RID: 29441 RVA: 0x002BC6BC File Offset: 0x002BA8BC
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<CraftModuleInterface>() != null && target.HasTag(GameTags.RocketInSpace);
	}

	// Token: 0x06007302 RID: 29442 RVA: 0x002BC6D9 File Offset: 0x002BA8D9
	public override void SetTarget(GameObject target)
	{
		this.craftInterface = target.GetComponent<CraftModuleInterface>();
		this.acknowledgeWarnings = false;
		this.craftInterface.Subscribe<SelfDestructButtonSideScreen>(-1582839653, SelfDestructButtonSideScreen.TagsChangedDelegate);
		this.Refresh();
	}

	// Token: 0x06007303 RID: 29443 RVA: 0x002BC70A File Offset: 0x002BA90A
	public override void ClearTarget()
	{
		if (this.craftInterface != null)
		{
			this.craftInterface.Unsubscribe<SelfDestructButtonSideScreen>(-1582839653, SelfDestructButtonSideScreen.TagsChangedDelegate, false);
			this.craftInterface = null;
		}
	}

	// Token: 0x06007304 RID: 29444 RVA: 0x002BC737 File Offset: 0x002BA937
	private void OnTagsChanged(object data)
	{
		if (((TagChangedEventData)data).tag == GameTags.RocketStranded)
		{
			this.Refresh();
		}
	}

	// Token: 0x06007305 RID: 29445 RVA: 0x002BC756 File Offset: 0x002BA956
	private void TriggerDestruct()
	{
		if (this.acknowledgeWarnings)
		{
			this.craftInterface.gameObject.Trigger(-1061799784, null);
			this.acknowledgeWarnings = false;
		}
		else
		{
			this.acknowledgeWarnings = true;
		}
		this.Refresh();
	}

	// Token: 0x06007306 RID: 29446 RVA: 0x002BC78C File Offset: 0x002BA98C
	private void Refresh()
	{
		if (this.craftInterface == null)
		{
			return;
		}
		this.statusText.text = UI.UISIDESCREENS.SELFDESTRUCTSIDESCREEN.MESSAGE_TEXT;
		if (this.acknowledgeWarnings)
		{
			this.button.GetComponentInChildren<LocText>().text = UI.UISIDESCREENS.SELFDESTRUCTSIDESCREEN.BUTTON_TEXT_CONFIRM;
			this.button.GetComponentInChildren<ToolTip>().toolTip = UI.UISIDESCREENS.SELFDESTRUCTSIDESCREEN.BUTTON_TOOLTIP_CONFIRM;
			return;
		}
		this.button.GetComponentInChildren<LocText>().text = UI.UISIDESCREENS.SELFDESTRUCTSIDESCREEN.BUTTON_TEXT;
		this.button.GetComponentInChildren<ToolTip>().toolTip = UI.UISIDESCREENS.SELFDESTRUCTSIDESCREEN.BUTTON_TOOLTIP;
	}

	// Token: 0x04004F3E RID: 20286
	public KButton button;

	// Token: 0x04004F3F RID: 20287
	public LocText statusText;

	// Token: 0x04004F40 RID: 20288
	private CraftModuleInterface craftInterface;

	// Token: 0x04004F41 RID: 20289
	private bool acknowledgeWarnings;

	// Token: 0x04004F42 RID: 20290
	private static readonly EventSystem.IntraObjectHandler<SelfDestructButtonSideScreen> TagsChangedDelegate = new EventSystem.IntraObjectHandler<SelfDestructButtonSideScreen>(delegate(SelfDestructButtonSideScreen cmp, object data)
	{
		cmp.OnTagsChanged(data);
	});
}
