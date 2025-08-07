using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000980 RID: 2432
public class SandboxCritterTool : BrushTool
{
	// Token: 0x06004673 RID: 18035 RVA: 0x00195129 File Offset: 0x00193329
	public static void DestroyInstance()
	{
		SandboxCritterTool.instance = null;
	}

	// Token: 0x06004674 RID: 18036 RVA: 0x00195131 File Offset: 0x00193331
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		SandboxCritterTool.instance = this;
	}

	// Token: 0x06004675 RID: 18037 RVA: 0x0019513F File Offset: 0x0019333F
	protected override string GetDragSound()
	{
		return "";
	}

	// Token: 0x06004676 RID: 18038 RVA: 0x00195146 File Offset: 0x00193346
	public void Activate()
	{
		PlayerController.Instance.ActivateTool(this);
	}

	// Token: 0x06004677 RID: 18039 RVA: 0x00195153 File Offset: 0x00193353
	protected override void OnActivateTool()
	{
		base.OnActivateTool();
		SandboxToolParameterMenu.instance.gameObject.SetActive(true);
		SandboxToolParameterMenu.instance.DisableParameters();
		SandboxToolParameterMenu.instance.brushRadiusSlider.SetValue(6f, true);
	}

	// Token: 0x06004678 RID: 18040 RVA: 0x0019518A File Offset: 0x0019338A
	protected override void OnDeactivateTool(InterfaceTool new_tool)
	{
		base.OnDeactivateTool(new_tool);
		SandboxToolParameterMenu.instance.gameObject.SetActive(false);
	}

	// Token: 0x06004679 RID: 18041 RVA: 0x001951A4 File Offset: 0x001933A4
	public override void GetOverlayColorData(out HashSet<ToolMenu.CellColorData> colors)
	{
		colors = new HashSet<ToolMenu.CellColorData>();
		foreach (int num in this.cellsInRadius)
		{
			colors.Add(new ToolMenu.CellColorData(num, this.radiusIndicatorColor));
		}
	}

	// Token: 0x0600467A RID: 18042 RVA: 0x0019520C File Offset: 0x0019340C
	public override void OnMouseMove(Vector3 cursorPos)
	{
		base.OnMouseMove(cursorPos);
	}

	// Token: 0x0600467B RID: 18043 RVA: 0x00195215 File Offset: 0x00193415
	public override void OnLeftClickDown(Vector3 cursor_pos)
	{
		base.OnLeftClickDown(cursor_pos);
		KFMOD.PlayUISound(GlobalAssets.GetSound("SandboxTool_Click", false));
	}

	// Token: 0x0600467C RID: 18044 RVA: 0x00195230 File Offset: 0x00193430
	protected override void OnPaintCell(int cell, int distFromOrigin)
	{
		base.OnPaintCell(cell, distFromOrigin);
		HashSetPool<GameObject, SandboxCritterTool>.PooledHashSet pooledHashSet = HashSetPool<GameObject, SandboxCritterTool>.Allocate();
		foreach (Health health in Components.Health.Items)
		{
			if (Grid.PosToCell(health) == cell && health.GetComponent<KPrefabID>().HasTag(GameTags.Creature))
			{
				pooledHashSet.Add(health.gameObject);
			}
		}
		foreach (GameObject gameObject in pooledHashSet)
		{
			KFMOD.PlayOneShot(this.soundPath, gameObject.gameObject.transform.GetPosition(), 1f);
			Util.KDestroyGameObject(gameObject);
		}
		pooledHashSet.Recycle();
	}

	// Token: 0x04002EBF RID: 11967
	public static SandboxCritterTool instance;

	// Token: 0x04002EC0 RID: 11968
	private string soundPath = GlobalAssets.GetSound("SandboxTool_ClearFloor", false);
}
