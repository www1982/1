using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x0200097F RID: 2431
public class SandboxClearFloorTool : BrushTool
{
	// Token: 0x06004667 RID: 18023 RVA: 0x00194EB1 File Offset: 0x001930B1
	public static void DestroyInstance()
	{
		SandboxClearFloorTool.instance = null;
	}

	// Token: 0x170004F7 RID: 1271
	// (get) Token: 0x06004668 RID: 18024 RVA: 0x00194EB9 File Offset: 0x001930B9
	private SandboxSettings settings
	{
		get
		{
			return SandboxToolParameterMenu.instance.settings;
		}
	}

	// Token: 0x06004669 RID: 18025 RVA: 0x00194EC5 File Offset: 0x001930C5
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		SandboxClearFloorTool.instance = this;
	}

	// Token: 0x0600466A RID: 18026 RVA: 0x00194ED3 File Offset: 0x001930D3
	protected override string GetDragSound()
	{
		return "";
	}

	// Token: 0x0600466B RID: 18027 RVA: 0x00194EDA File Offset: 0x001930DA
	public void Activate()
	{
		PlayerController.Instance.ActivateTool(this);
	}

	// Token: 0x0600466C RID: 18028 RVA: 0x00194EE8 File Offset: 0x001930E8
	protected override void OnActivateTool()
	{
		base.OnActivateTool();
		SandboxToolParameterMenu.instance.gameObject.SetActive(true);
		SandboxToolParameterMenu.instance.DisableParameters();
		SandboxToolParameterMenu.instance.brushRadiusSlider.row.SetActive(true);
		SandboxToolParameterMenu.instance.brushRadiusSlider.SetValue((float)this.settings.GetIntSetting("SandboxTools.BrushSize"), true);
	}

	// Token: 0x0600466D RID: 18029 RVA: 0x00194F4B File Offset: 0x0019314B
	protected override void OnDeactivateTool(InterfaceTool new_tool)
	{
		base.OnDeactivateTool(new_tool);
		SandboxToolParameterMenu.instance.gameObject.SetActive(false);
	}

	// Token: 0x0600466E RID: 18030 RVA: 0x00194F64 File Offset: 0x00193164
	public override void GetOverlayColorData(out HashSet<ToolMenu.CellColorData> colors)
	{
		colors = new HashSet<ToolMenu.CellColorData>();
		foreach (int num in this.cellsInRadius)
		{
			colors.Add(new ToolMenu.CellColorData(num, this.radiusIndicatorColor));
		}
	}

	// Token: 0x0600466F RID: 18031 RVA: 0x00194FCC File Offset: 0x001931CC
	public override void OnMouseMove(Vector3 cursorPos)
	{
		base.OnMouseMove(cursorPos);
	}

	// Token: 0x06004670 RID: 18032 RVA: 0x00194FD5 File Offset: 0x001931D5
	public override void OnLeftClickDown(Vector3 cursor_pos)
	{
		base.OnLeftClickDown(cursor_pos);
		KFMOD.PlayUISound(GlobalAssets.GetSound("SandboxTool_Click", false));
	}

	// Token: 0x06004671 RID: 18033 RVA: 0x00194FF0 File Offset: 0x001931F0
	protected override void OnPaintCell(int cell, int distFromOrigin)
	{
		base.OnPaintCell(cell, distFromOrigin);
		bool flag = false;
		using (List<Pickupable>.Enumerator enumerator = Components.Pickupables.Items.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				Pickupable pickup = enumerator.Current;
				if (!(pickup.storage != null) && Grid.PosToCell(pickup) == cell && Components.LiveMinionIdentities.Items.Find((MinionIdentity match) => match.gameObject == pickup.gameObject) == null)
				{
					if (!flag)
					{
						KFMOD.PlayOneShot(this.soundPath, pickup.gameObject.transform.GetPosition(), 1f);
						PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Negative, UI.SANDBOXTOOLS.CLEARFLOOR.DELETED, pickup.transform, 1.5f, false);
						flag = true;
					}
					Util.KDestroyGameObject(pickup.gameObject);
				}
			}
		}
	}

	// Token: 0x04002EBD RID: 11965
	public static SandboxClearFloorTool instance;

	// Token: 0x04002EBE RID: 11966
	private string soundPath = GlobalAssets.GetSound("SandboxTool_ClearFloor", false);
}
