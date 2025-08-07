using System;
using UnityEngine;

// Token: 0x0200096D RID: 2413
public class CopySettingsTool : DragTool
{
	// Token: 0x0600457D RID: 17789 RVA: 0x00190690 File Offset: 0x0018E890
	public static void DestroyInstance()
	{
		CopySettingsTool.Instance = null;
	}

	// Token: 0x0600457E RID: 17790 RVA: 0x00190698 File Offset: 0x0018E898
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		CopySettingsTool.Instance = this;
	}

	// Token: 0x0600457F RID: 17791 RVA: 0x001906A6 File Offset: 0x0018E8A6
	public void Activate()
	{
		PlayerController.Instance.ActivateTool(this);
	}

	// Token: 0x06004580 RID: 17792 RVA: 0x001906B3 File Offset: 0x0018E8B3
	public void SetSourceObject(GameObject sourceGameObject)
	{
		this.sourceGameObject = sourceGameObject;
	}

	// Token: 0x06004581 RID: 17793 RVA: 0x001906BC File Offset: 0x0018E8BC
	protected override void OnDragTool(int cell, int distFromOrigin)
	{
		if (this.sourceGameObject == null)
		{
			return;
		}
		if (Grid.IsValidCell(cell))
		{
			CopyBuildingSettings.ApplyCopy(cell, this.sourceGameObject);
		}
	}

	// Token: 0x06004582 RID: 17794 RVA: 0x001906E2 File Offset: 0x0018E8E2
	protected override void OnActivateTool()
	{
		base.OnActivateTool();
	}

	// Token: 0x06004583 RID: 17795 RVA: 0x001906EA File Offset: 0x0018E8EA
	protected override void OnDeactivateTool(InterfaceTool new_tool)
	{
		base.OnDeactivateTool(new_tool);
		this.sourceGameObject = null;
	}

	// Token: 0x04002E5E RID: 11870
	public static CopySettingsTool Instance;

	// Token: 0x04002E5F RID: 11871
	public GameObject Placer;

	// Token: 0x04002E60 RID: 11872
	private GameObject sourceGameObject;
}
