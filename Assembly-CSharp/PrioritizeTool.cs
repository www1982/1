using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200097D RID: 2429
public class PrioritizeTool : FilteredDragTool
{
	// Token: 0x0600464C RID: 17996 RVA: 0x001944F0 File Offset: 0x001926F0
	public static void DestroyInstance()
	{
		PrioritizeTool.Instance = null;
	}

	// Token: 0x0600464D RID: 17997 RVA: 0x001944F8 File Offset: 0x001926F8
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.interceptNumberKeysForPriority = true;
		PrioritizeTool.Instance = this;
		this.visualizer = Util.KInstantiate(this.visualizer, null, null);
		this.viewMode = OverlayModes.Priorities.ID;
		Game.Instance.prioritizableRenderer.currentTool = this;
	}

	// Token: 0x0600464E RID: 17998 RVA: 0x00194548 File Offset: 0x00192748
	public override string GetFilterLayerFromGameObject(GameObject input)
	{
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		if (input.GetComponent<Diggable>())
		{
			flag = true;
		}
		if (input.GetComponent<Constructable>() || (input.GetComponent<Deconstructable>() && input.GetComponent<Deconstructable>().IsMarkedForDeconstruction()))
		{
			flag2 = true;
		}
		if (input.GetComponent<Clearable>() || input.GetComponent<Moppable>() || input.GetComponent<StorageLocker>())
		{
			flag3 = true;
		}
		if (flag2)
		{
			return ToolParameterMenu.FILTERLAYERS.CONSTRUCTION;
		}
		if (flag)
		{
			return ToolParameterMenu.FILTERLAYERS.DIG;
		}
		if (flag3)
		{
			return ToolParameterMenu.FILTERLAYERS.CLEAN;
		}
		return ToolParameterMenu.FILTERLAYERS.OPERATE;
	}

	// Token: 0x0600464F RID: 17999 RVA: 0x001945DC File Offset: 0x001927DC
	protected override void GetDefaultFilters(Dictionary<string, ToolParameterMenu.ToggleState> filters)
	{
		filters.Add(ToolParameterMenu.FILTERLAYERS.ALL, ToolParameterMenu.ToggleState.On);
		filters.Add(ToolParameterMenu.FILTERLAYERS.CONSTRUCTION, ToolParameterMenu.ToggleState.Off);
		filters.Add(ToolParameterMenu.FILTERLAYERS.DIG, ToolParameterMenu.ToggleState.Off);
		filters.Add(ToolParameterMenu.FILTERLAYERS.CLEAN, ToolParameterMenu.ToggleState.Off);
		filters.Add(ToolParameterMenu.FILTERLAYERS.OPERATE, ToolParameterMenu.ToggleState.Off);
	}

	// Token: 0x06004650 RID: 18000 RVA: 0x0019461C File Offset: 0x0019281C
	private bool TryPrioritizeGameObject(GameObject target, PrioritySetting priority)
	{
		string filterLayerFromGameObject = this.GetFilterLayerFromGameObject(target);
		if (base.IsActiveLayer(filterLayerFromGameObject))
		{
			Prioritizable component = target.GetComponent<Prioritizable>();
			if (component != null && component.showIcon && component.IsPrioritizable())
			{
				component.SetMasterPriority(priority);
				return true;
			}
		}
		return false;
	}

	// Token: 0x06004651 RID: 18001 RVA: 0x00194664 File Offset: 0x00192864
	protected override void OnDragTool(int cell, int distFromOrigin)
	{
		PrioritySetting lastSelectedPriority = ToolMenu.Instance.PriorityScreen.GetLastSelectedPriority();
		int num = 0;
		for (int i = 0; i < 45; i++)
		{
			GameObject gameObject = Grid.Objects[cell, i];
			if (gameObject != null)
			{
				if (gameObject.GetComponent<Pickupable>())
				{
					ObjectLayerListItem objectLayerListItem = gameObject.GetComponent<Pickupable>().objectLayerListItem;
					while (objectLayerListItem != null)
					{
						GameObject gameObject2 = objectLayerListItem.gameObject;
						objectLayerListItem = objectLayerListItem.nextItem;
						if (!(gameObject2 == null) && !(gameObject2.GetComponent<MinionIdentity>() != null) && this.TryPrioritizeGameObject(gameObject2, lastSelectedPriority))
						{
							num++;
						}
					}
				}
				else if (this.TryPrioritizeGameObject(gameObject, lastSelectedPriority))
				{
					num++;
				}
			}
		}
		if (num > 0)
		{
			PriorityScreen.PlayPriorityConfirmSound(lastSelectedPriority);
		}
	}

	// Token: 0x06004652 RID: 18002 RVA: 0x00194720 File Offset: 0x00192920
	protected override void OnActivateTool()
	{
		base.OnActivateTool();
		ToolMenu.Instance.PriorityScreen.ShowDiagram(true);
		ToolMenu.Instance.PriorityScreen.Show(true);
		ToolMenu.Instance.PriorityScreen.transform.localScale = new Vector3(1.35f, 1.35f, 1.35f);
	}

	// Token: 0x06004653 RID: 18003 RVA: 0x0019477C File Offset: 0x0019297C
	protected override void OnDeactivateTool(InterfaceTool new_tool)
	{
		base.OnDeactivateTool(new_tool);
		ToolMenu.Instance.PriorityScreen.Show(false);
		ToolMenu.Instance.PriorityScreen.ShowDiagram(false);
		ToolMenu.Instance.PriorityScreen.transform.localScale = new Vector3(1f, 1f, 1f);
	}

	// Token: 0x06004654 RID: 18004 RVA: 0x001947D8 File Offset: 0x001929D8
	public void Update()
	{
		PrioritySetting lastSelectedPriority = ToolMenu.Instance.PriorityScreen.GetLastSelectedPriority();
		int num = 0;
		if (lastSelectedPriority.priority_class >= PriorityScreen.PriorityClass.high)
		{
			num += 9;
		}
		if (lastSelectedPriority.priority_class >= PriorityScreen.PriorityClass.topPriority)
		{
			num = num;
		}
		num += lastSelectedPriority.priority_value;
		Texture2D texture2D = this.cursors[num - 1];
		MeshRenderer componentInChildren = this.visualizer.GetComponentInChildren<MeshRenderer>();
		if (componentInChildren != null)
		{
			componentInChildren.material.mainTexture = texture2D;
		}
	}

	// Token: 0x04002EB6 RID: 11958
	public GameObject Placer;

	// Token: 0x04002EB7 RID: 11959
	public static PrioritizeTool Instance;

	// Token: 0x04002EB8 RID: 11960
	public Texture2D[] cursors;
}
