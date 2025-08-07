using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x0200096C RID: 2412
public class ClusterMapSelectTool : InterfaceTool
{
	// Token: 0x06004571 RID: 17777 RVA: 0x0019040E File Offset: 0x0018E60E
	public static void DestroyInstance()
	{
		ClusterMapSelectTool.Instance = null;
	}

	// Token: 0x06004572 RID: 17778 RVA: 0x00190416 File Offset: 0x0018E616
	protected override void OnPrefabInit()
	{
		ClusterMapSelectTool.Instance = this;
	}

	// Token: 0x06004573 RID: 17779 RVA: 0x0019041E File Offset: 0x0018E61E
	public void Activate()
	{
		PlayerController.Instance.ActivateTool(this);
		ToolMenu.Instance.PriorityScreen.ResetPriority();
		this.Select(null, false);
	}

	// Token: 0x06004574 RID: 17780 RVA: 0x00190442 File Offset: 0x0018E642
	public KSelectable GetSelected()
	{
		return this.m_selected;
	}

	// Token: 0x06004575 RID: 17781 RVA: 0x0019044A File Offset: 0x0018E64A
	public override bool ShowHoverUI()
	{
		return ClusterMapScreen.Instance.HasCurrentHover();
	}

	// Token: 0x06004576 RID: 17782 RVA: 0x00190456 File Offset: 0x0018E656
	protected override void OnDeactivateTool(InterfaceTool new_tool)
	{
		base.OnDeactivateTool(new_tool);
		base.ClearHover();
		this.Select(null, false);
	}

	// Token: 0x06004577 RID: 17783 RVA: 0x00190470 File Offset: 0x0018E670
	private void UpdateHoveredSelectables()
	{
		this.m_hoveredSelectables.Clear();
		if (ClusterMapScreen.Instance.HasCurrentHover())
		{
			AxialI currentHoverLocation = ClusterMapScreen.Instance.GetCurrentHoverLocation();
			List<KSelectable> list = (from entity in ClusterGrid.Instance.GetVisibleEntitiesAtCell(currentHoverLocation)
				select entity.GetComponent<KSelectable>() into selectable
				where selectable != null && selectable.IsSelectable
				select selectable).ToList<KSelectable>();
			this.m_hoveredSelectables.AddRange(list);
		}
	}

	// Token: 0x06004578 RID: 17784 RVA: 0x00190504 File Offset: 0x0018E704
	public override void LateUpdate()
	{
		this.UpdateHoveredSelectables();
		KSelectable kselectable = ((this.m_hoveredSelectables.Count > 0) ? this.m_hoveredSelectables[0] : null);
		base.UpdateHoverElements(this.m_hoveredSelectables);
		if (!this.hasFocus)
		{
			base.ClearHover();
		}
		else if (kselectable != this.hover)
		{
			base.ClearHover();
			this.hover = kselectable;
			if (kselectable != null)
			{
				Game.Instance.Trigger(2095258329, kselectable.gameObject);
				kselectable.Hover(!this.playedSoundThisFrame);
				this.playedSoundThisFrame = true;
			}
		}
		this.playedSoundThisFrame = false;
	}

	// Token: 0x06004579 RID: 17785 RVA: 0x001905A7 File Offset: 0x0018E7A7
	public void SelectNextFrame(KSelectable new_selected, bool skipSound = false)
	{
		this.delayedNextSelection = new_selected;
		this.delayedSkipSound = skipSound;
		UIScheduler.Instance.ScheduleNextFrame("DelayedSelect", new Action<object>(this.DoSelectNextFrame), null, null);
	}

	// Token: 0x0600457A RID: 17786 RVA: 0x001905D5 File Offset: 0x0018E7D5
	private void DoSelectNextFrame(object data)
	{
		this.Select(this.delayedNextSelection, this.delayedSkipSound);
		this.delayedNextSelection = null;
	}

	// Token: 0x0600457B RID: 17787 RVA: 0x001905F0 File Offset: 0x0018E7F0
	public void Select(KSelectable new_selected, bool skipSound = false)
	{
		if (new_selected == this.m_selected)
		{
			return;
		}
		if (this.m_selected != null)
		{
			this.m_selected.Unselect();
		}
		GameObject gameObject = null;
		if (new_selected != null && new_selected.GetMyWorldId() == -1)
		{
			if (new_selected == this.hover)
			{
				base.ClearHover();
			}
			new_selected.Select();
			gameObject = new_selected.gameObject;
		}
		this.m_selected = ((gameObject == null) ? null : new_selected);
		Game.Instance.Trigger(-1503271301, gameObject);
	}

	// Token: 0x04002E59 RID: 11865
	private List<KSelectable> m_hoveredSelectables = new List<KSelectable>();

	// Token: 0x04002E5A RID: 11866
	private KSelectable m_selected;

	// Token: 0x04002E5B RID: 11867
	public static ClusterMapSelectTool Instance;

	// Token: 0x04002E5C RID: 11868
	private KSelectable delayedNextSelection;

	// Token: 0x04002E5D RID: 11869
	private bool delayedSkipSound;
}
