using System;
using System.Collections.Generic;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using Klei.AI;
using UnityEngine;

// Token: 0x02000983 RID: 2435
public class SandboxFloodTool : FloodTool
{
	// Token: 0x06004697 RID: 18071 RVA: 0x001959EB File Offset: 0x00193BEB
	public static void DestroyInstance()
	{
		SandboxFloodTool.instance = null;
	}

	// Token: 0x06004698 RID: 18072 RVA: 0x001959F3 File Offset: 0x00193BF3
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		SandboxFloodTool.instance = this;
		this.floodCriteria = (int cell) => Grid.IsValidCell(cell) && Grid.Element[cell] == Grid.Element[this.mouseCell] && Grid.WorldIdx[cell] == Grid.WorldIdx[this.mouseCell];
		this.paintArea = delegate(HashSet<int> cells)
		{
			foreach (int num in cells)
			{
				this.PaintCell(num);
			}
		};
	}

	// Token: 0x06004699 RID: 18073 RVA: 0x00195A28 File Offset: 0x00193C28
	private void PaintCell(int cell)
	{
		this.recentlyAffectedCells.Add(cell);
		Game.CallbackInfo callbackInfo = new Game.CallbackInfo(delegate
		{
			this.recentlyAffectedCells.Remove(cell);
		}, false);
		Element element = ElementLoader.elements[this.settings.GetIntSetting("SandboxTools.SelectedElement")];
		byte b = Db.Get().Diseases.GetIndex(Db.Get().Diseases.Get("FoodPoisoning").id);
		Disease disease = Db.Get().Diseases.TryGet(this.settings.GetStringSetting("SandboxTools.SelectedDisease"));
		if (disease != null)
		{
			b = Db.Get().Diseases.GetIndex(disease.id);
		}
		int index = Game.Instance.callbackManager.Add(callbackInfo).index;
		int cell2 = cell;
		SimHashes id = element.id;
		CellElementEvent sandBoxTool = CellEventLogger.Instance.SandBoxTool;
		float floatSetting = this.settings.GetFloatSetting("SandboxTools.Mass");
		float floatSetting2 = this.settings.GetFloatSetting("SandbosTools.Temperature");
		int num = index;
		SimMessages.ReplaceElement(cell2, id, sandBoxTool, floatSetting, floatSetting2, b, this.settings.GetIntSetting("SandboxTools.DiseaseCount"), num);
	}

	// Token: 0x170004FA RID: 1274
	// (get) Token: 0x0600469A RID: 18074 RVA: 0x00195B5C File Offset: 0x00193D5C
	private SandboxSettings settings
	{
		get
		{
			return SandboxToolParameterMenu.instance.settings;
		}
	}

	// Token: 0x0600469B RID: 18075 RVA: 0x00195B68 File Offset: 0x00193D68
	public void Activate()
	{
		PlayerController.Instance.ActivateTool(this);
	}

	// Token: 0x0600469C RID: 18076 RVA: 0x00195B78 File Offset: 0x00193D78
	protected override void OnActivateTool()
	{
		base.OnActivateTool();
		SandboxToolParameterMenu.instance.gameObject.SetActive(true);
		SandboxToolParameterMenu.instance.DisableParameters();
		SandboxToolParameterMenu.instance.massSlider.row.SetActive(true);
		SandboxToolParameterMenu.instance.temperatureSlider.row.SetActive(true);
		SandboxToolParameterMenu.instance.elementSelector.row.SetActive(true);
		SandboxToolParameterMenu.instance.diseaseSelector.row.SetActive(true);
		SandboxToolParameterMenu.instance.diseaseCountSlider.row.SetActive(true);
	}

	// Token: 0x0600469D RID: 18077 RVA: 0x00195C0E File Offset: 0x00193E0E
	protected override void OnDeactivateTool(InterfaceTool new_tool)
	{
		base.OnDeactivateTool(new_tool);
		SandboxToolParameterMenu.instance.gameObject.SetActive(false);
		this.ev.release();
	}

	// Token: 0x0600469E RID: 18078 RVA: 0x00195C34 File Offset: 0x00193E34
	public override void GetOverlayColorData(out HashSet<ToolMenu.CellColorData> colors)
	{
		colors = new HashSet<ToolMenu.CellColorData>();
		foreach (int num in this.recentlyAffectedCells)
		{
			colors.Add(new ToolMenu.CellColorData(num, this.recentlyAffectedCellColor));
		}
		foreach (int num2 in this.cellsToAffect)
		{
			colors.Add(new ToolMenu.CellColorData(num2, this.areaColour));
		}
	}

	// Token: 0x0600469F RID: 18079 RVA: 0x00195CF0 File Offset: 0x00193EF0
	public override void OnMouseMove(Vector3 cursorPos)
	{
		base.OnMouseMove(cursorPos);
		this.cellsToAffect = base.Flood(Grid.PosToCell(cursorPos));
	}

	// Token: 0x060046A0 RID: 18080 RVA: 0x00195D0C File Offset: 0x00193F0C
	public override void OnLeftClickDown(Vector3 cursor_pos)
	{
		base.OnLeftClickDown(cursor_pos);
		Element element = ElementLoader.elements[this.settings.GetIntSetting("SandboxTools.SelectedElement")];
		string text;
		if (element.IsSolid)
		{
			text = GlobalAssets.GetSound("Break_" + element.substance.GetMiningBreakSound(), false);
			if (text == null)
			{
				text = GlobalAssets.GetSound("Break_Rock", false);
			}
		}
		else if (element.IsGas)
		{
			text = GlobalAssets.GetSound("SandboxTool_Bucket_Gas", false);
		}
		else if (element.IsLiquid)
		{
			text = GlobalAssets.GetSound("SandboxTool_Bucket_Liquid", false);
		}
		else
		{
			text = GlobalAssets.GetSound("Break_Rock", false);
		}
		this.ev = KFMOD.CreateInstance(text);
		ATTRIBUTES_3D attributes_3D = SoundListenerController.Instance.transform.GetPosition().To3DAttributes();
		this.ev.set3DAttributes(attributes_3D);
		this.ev.setParameterByName("SandboxToggle", 1f, false);
		this.ev.start();
		KFMOD.PlayUISound(GlobalAssets.GetSound("SandboxTool_Bucket", false));
	}

	// Token: 0x060046A1 RID: 18081 RVA: 0x00195E0C File Offset: 0x0019400C
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.SandboxCopyElement))
		{
			int num = Grid.PosToCell(PlayerController.GetCursorPos(KInputManager.GetMousePos()));
			if (Grid.IsValidCell(num))
			{
				SandboxSampleTool.Sample(num);
			}
		}
		if (!e.Consumed)
		{
			base.OnKeyDown(e);
		}
	}

	// Token: 0x04002EC8 RID: 11976
	public static SandboxFloodTool instance;

	// Token: 0x04002EC9 RID: 11977
	protected HashSet<int> recentlyAffectedCells = new HashSet<int>();

	// Token: 0x04002ECA RID: 11978
	protected HashSet<int> cellsToAffect = new HashSet<int>();

	// Token: 0x04002ECB RID: 11979
	protected Color recentlyAffectedCellColor = new Color(1f, 1f, 1f, 0.1f);

	// Token: 0x04002ECC RID: 11980
	private EventInstance ev;
}
