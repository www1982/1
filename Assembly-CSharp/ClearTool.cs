using System;
using UnityEngine;

// Token: 0x0200096B RID: 2411
public class ClearTool : DragTool
{
	// Token: 0x0600456A RID: 17770 RVA: 0x00190311 File Offset: 0x0018E511
	public static void DestroyInstance()
	{
		ClearTool.Instance = null;
	}

	// Token: 0x0600456B RID: 17771 RVA: 0x00190319 File Offset: 0x0018E519
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		ClearTool.Instance = this;
		this.interceptNumberKeysForPriority = true;
	}

	// Token: 0x0600456C RID: 17772 RVA: 0x0019032E File Offset: 0x0018E52E
	public void Activate()
	{
		PlayerController.Instance.ActivateTool(this);
	}

	// Token: 0x0600456D RID: 17773 RVA: 0x0019033C File Offset: 0x0018E53C
	protected override void OnDragTool(int cell, int distFromOrigin)
	{
		GameObject gameObject = Grid.Objects[cell, 3];
		if (gameObject == null)
		{
			return;
		}
		ObjectLayerListItem objectLayerListItem = gameObject.GetComponent<Pickupable>().objectLayerListItem;
		while (objectLayerListItem != null)
		{
			GameObject gameObject2 = objectLayerListItem.gameObject;
			objectLayerListItem = objectLayerListItem.nextItem;
			if (!(gameObject2 == null) && !(gameObject2.GetComponent<MinionIdentity>() != null) && gameObject2.GetComponent<Clearable>().isClearable)
			{
				gameObject2.GetComponent<Clearable>().MarkForClear(false, false);
				Prioritizable component = gameObject2.GetComponent<Prioritizable>();
				if (component != null)
				{
					component.SetMasterPriority(ToolMenu.Instance.PriorityScreen.GetLastSelectedPriority());
				}
			}
		}
	}

	// Token: 0x0600456E RID: 17774 RVA: 0x001903D5 File Offset: 0x0018E5D5
	protected override void OnActivateTool()
	{
		base.OnActivateTool();
		ToolMenu.Instance.PriorityScreen.Show(true);
	}

	// Token: 0x0600456F RID: 17775 RVA: 0x001903ED File Offset: 0x0018E5ED
	protected override void OnDeactivateTool(InterfaceTool new_tool)
	{
		base.OnDeactivateTool(new_tool);
		ToolMenu.Instance.PriorityScreen.Show(false);
	}

	// Token: 0x04002E58 RID: 11864
	public static ClearTool Instance;
}
