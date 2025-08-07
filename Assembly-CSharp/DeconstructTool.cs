using System;
using UnityEngine;

// Token: 0x0200096F RID: 2415
public class DeconstructTool : FilteredDragTool
{
	// Token: 0x06004591 RID: 17809 RVA: 0x00190F65 File Offset: 0x0018F165
	public static void DestroyInstance()
	{
		DeconstructTool.Instance = null;
	}

	// Token: 0x06004592 RID: 17810 RVA: 0x00190F6D File Offset: 0x0018F16D
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		DeconstructTool.Instance = this;
	}

	// Token: 0x06004593 RID: 17811 RVA: 0x00190F7B File Offset: 0x0018F17B
	public void Activate()
	{
		PlayerController.Instance.ActivateTool(this);
	}

	// Token: 0x06004594 RID: 17812 RVA: 0x00190F88 File Offset: 0x0018F188
	protected override string GetConfirmSound()
	{
		return "Tile_Confirm_NegativeTool";
	}

	// Token: 0x06004595 RID: 17813 RVA: 0x00190F8F File Offset: 0x0018F18F
	protected override string GetDragSound()
	{
		return "Tile_Drag_NegativeTool";
	}

	// Token: 0x06004596 RID: 17814 RVA: 0x00190F96 File Offset: 0x0018F196
	protected override void OnDragTool(int cell, int distFromOrigin)
	{
		this.DeconstructCell(cell);
	}

	// Token: 0x06004597 RID: 17815 RVA: 0x00190FA0 File Offset: 0x0018F1A0
	public void DeconstructCell(int cell)
	{
		for (int i = 0; i < 45; i++)
		{
			GameObject gameObject = Grid.Objects[cell, i];
			if (gameObject != null)
			{
				string filterLayerFromGameObject = this.GetFilterLayerFromGameObject(gameObject);
				if (base.IsActiveLayer(filterLayerFromGameObject))
				{
					gameObject.Trigger(-790448070, null);
					Prioritizable component = gameObject.GetComponent<Prioritizable>();
					if (component != null)
					{
						component.SetMasterPriority(ToolMenu.Instance.PriorityScreen.GetLastSelectedPriority());
					}
				}
			}
		}
	}

	// Token: 0x06004598 RID: 17816 RVA: 0x00191012 File Offset: 0x0018F212
	protected override void OnActivateTool()
	{
		base.OnActivateTool();
		ToolMenu.Instance.PriorityScreen.Show(true);
	}

	// Token: 0x06004599 RID: 17817 RVA: 0x0019102A File Offset: 0x0018F22A
	protected override void OnDeactivateTool(InterfaceTool new_tool)
	{
		base.OnDeactivateTool(new_tool);
		ToolMenu.Instance.PriorityScreen.Show(false);
	}

	// Token: 0x04002E63 RID: 11875
	public static DeconstructTool Instance;
}
