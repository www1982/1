using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000986 RID: 2438
public class SandboxSpawnerTool : InterfaceTool
{
	// Token: 0x060046B9 RID: 18105 RVA: 0x001965D9 File Offset: 0x001947D9
	public override void GetOverlayColorData(out HashSet<ToolMenu.CellColorData> colors)
	{
		colors = new HashSet<ToolMenu.CellColorData>();
		colors.Add(new ToolMenu.CellColorData(this.currentCell, this.radiusIndicatorColor));
	}

	// Token: 0x060046BA RID: 18106 RVA: 0x001965FB File Offset: 0x001947FB
	public override void OnMouseMove(Vector3 cursorPos)
	{
		base.OnMouseMove(cursorPos);
		this.currentCell = Grid.PosToCell(cursorPos);
	}

	// Token: 0x060046BB RID: 18107 RVA: 0x00196610 File Offset: 0x00194810
	public override void OnLeftClickDown(Vector3 cursor_pos)
	{
		this.Place(Grid.PosToCell(cursor_pos));
	}

	// Token: 0x060046BC RID: 18108 RVA: 0x00196620 File Offset: 0x00194820
	private void Place(int cell)
	{
		if (!Grid.IsValidBuildingCell(cell))
		{
			return;
		}
		string stringSetting = SandboxToolParameterMenu.instance.settings.GetStringSetting("SandboxTools.SelectedEntity");
		GameObject prefab = Assets.GetPrefab(stringSetting);
		if (prefab.HasTag(GameTags.BaseMinion))
		{
			this.SpawnMinion(stringSetting);
		}
		else if (prefab.GetComponent<Building>() != null)
		{
			BuildingDef def = prefab.GetComponent<Building>().Def;
			def.Build(cell, Orientation.Neutral, null, def.DefaultElements(), 298.15f, true, -1f);
		}
		else
		{
			KBatchedAnimController component = prefab.GetComponent<KBatchedAnimController>();
			Grid.SceneLayer sceneLayer = ((component == null) ? Grid.SceneLayer.Creatures : component.sceneLayer);
			GameObject gameObject = GameUtil.KInstantiate(prefab, Grid.CellToPosCBC(this.currentCell, sceneLayer), sceneLayer, null, 0);
			if (gameObject.GetComponent<Pickupable>() != null && !gameObject.HasTag(GameTags.Creature))
			{
				gameObject.transform.position += Vector3.up * (Grid.CellSizeInMeters / 3f);
			}
			gameObject.SetActive(true);
		}
		GameUtil.KInstantiate(this.fxPrefab, Grid.CellToPosCCC(this.currentCell, Grid.SceneLayer.FXFront), Grid.SceneLayer.FXFront, null, 0).GetComponent<KAnimControllerBase>().Play("placer", KAnim.PlayMode.Once, 1f, 0f);
		KFMOD.PlayUISound(this.soundPath);
	}

	// Token: 0x060046BD RID: 18109 RVA: 0x00196776 File Offset: 0x00194976
	protected override void OnActivateTool()
	{
		base.OnActivateTool();
		SandboxToolParameterMenu.instance.gameObject.SetActive(true);
		SandboxToolParameterMenu.instance.DisableParameters();
		SandboxToolParameterMenu.instance.entitySelector.row.SetActive(true);
	}

	// Token: 0x060046BE RID: 18110 RVA: 0x001967AD File Offset: 0x001949AD
	protected override void OnDeactivateTool(InterfaceTool new_tool)
	{
		base.OnDeactivateTool(new_tool);
		SandboxToolParameterMenu.instance.gameObject.SetActive(false);
	}

	// Token: 0x060046BF RID: 18111 RVA: 0x001967C8 File Offset: 0x001949C8
	private void SpawnMinion(string prefabID)
	{
		GameObject prefab = Assets.GetPrefab(prefabID);
		Tag tag = prefabID;
		GameObject gameObject = Util.KInstantiate(prefab, null, null);
		gameObject.name = prefab.name;
		Immigration.Instance.ApplyDefaultPersonalPriorities(gameObject);
		Vector3 vector = Grid.CellToPosCBC(this.currentCell, Grid.SceneLayer.Move);
		gameObject.transform.SetLocalPosition(vector);
		gameObject.SetActive(true);
		new MinionStartingStats(tag, false, null, null, false).Apply(gameObject);
		gameObject.GetMyWorld().SetDupeVisited();
	}

	// Token: 0x060046C0 RID: 18112 RVA: 0x00196844 File Offset: 0x00194A44
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.SandboxCopyElement))
		{
			int num = Grid.PosToCell(PlayerController.GetCursorPos(KInputManager.GetMousePos()));
			List<ObjectLayer> list = new List<ObjectLayer>();
			list.Add(ObjectLayer.Pickupables);
			list.Add(ObjectLayer.Plants);
			list.Add(ObjectLayer.Minion);
			list.Add(ObjectLayer.Building);
			if (Grid.IsValidCell(num))
			{
				foreach (ObjectLayer objectLayer in list)
				{
					GameObject gameObject = Grid.Objects[num, (int)objectLayer];
					if (gameObject)
					{
						SandboxToolParameterMenu.instance.settings.SetStringSetting("SandboxTools.SelectedEntity", gameObject.PrefabID().ToString());
						break;
					}
				}
			}
		}
		if (!e.Consumed)
		{
			base.OnKeyDown(e);
		}
	}

	// Token: 0x04002ED3 RID: 11987
	protected Color radiusIndicatorColor = new Color(0.5f, 0.7f, 0.5f, 0.2f);

	// Token: 0x04002ED4 RID: 11988
	private int currentCell;

	// Token: 0x04002ED5 RID: 11989
	private string soundPath = GlobalAssets.GetSound("SandboxTool_Spawner", false);

	// Token: 0x04002ED6 RID: 11990
	[SerializeField]
	private GameObject fxPrefab;
}
