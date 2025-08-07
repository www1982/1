using System;
using System.Collections.Generic;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using STRINGS;
using UnityEngine;

// Token: 0x02000985 RID: 2437
public class SandboxSampleTool : InterfaceTool
{
	// Token: 0x060046B0 RID: 18096 RVA: 0x0019623D File Offset: 0x0019443D
	public override void GetOverlayColorData(out HashSet<ToolMenu.CellColorData> colors)
	{
		colors = new HashSet<ToolMenu.CellColorData>();
		colors.Add(new ToolMenu.CellColorData(this.currentCell, this.radiusIndicatorColor));
	}

	// Token: 0x060046B1 RID: 18097 RVA: 0x0019625F File Offset: 0x0019445F
	public override void OnMouseMove(Vector3 cursorPos)
	{
		base.OnMouseMove(cursorPos);
		this.currentCell = Grid.PosToCell(cursorPos);
	}

	// Token: 0x060046B2 RID: 18098 RVA: 0x00196274 File Offset: 0x00194474
	public override void OnLeftClickDown(Vector3 cursor_pos)
	{
		int num = Grid.PosToCell(cursor_pos);
		if (!Grid.IsValidCell(num))
		{
			PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Negative, UI.DEBUG_TOOLS.INVALID_LOCATION, null, cursor_pos, 1.5f, false, true);
			return;
		}
		SandboxSampleTool.Sample(num);
		KFMOD.PlayUISound(GlobalAssets.GetSound("SandboxTool_Click", false));
		this.PlaySound();
	}

	// Token: 0x060046B3 RID: 18099 RVA: 0x001962D8 File Offset: 0x001944D8
	public static void Sample(int cell)
	{
		SandboxToolParameterMenu.instance.settings.SetIntSetting("SandboxTools.SelectedElement", (int)Grid.Element[cell].idx);
		SandboxToolParameterMenu.instance.settings.SetFloatSetting("SandboxTools.Mass", Mathf.Round(Grid.Mass[cell] * 100f) / 100f);
		SandboxToolParameterMenu.instance.settings.SetFloatSetting("SandbosTools.Temperature", Mathf.Round(Grid.Temperature[cell] * 10f) / 10f);
		SandboxToolParameterMenu.instance.settings.SetIntSetting("SandboxTools.DiseaseCount", Grid.DiseaseCount[cell]);
		SandboxToolParameterMenu.instance.RefreshDisplay();
	}

	// Token: 0x060046B4 RID: 18100 RVA: 0x00196390 File Offset: 0x00194590
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

	// Token: 0x060046B5 RID: 18101 RVA: 0x00196426 File Offset: 0x00194626
	protected override void OnDeactivateTool(InterfaceTool new_tool)
	{
		base.OnDeactivateTool(new_tool);
		SandboxToolParameterMenu.instance.gameObject.SetActive(false);
		this.StopSound();
	}

	// Token: 0x060046B6 RID: 18102 RVA: 0x00196448 File Offset: 0x00194648
	private void PlaySound()
	{
		Element element = ElementLoader.elements[SandboxToolParameterMenu.instance.settings.GetIntSetting("SandboxTools.SelectedElement")];
		float num = 1f;
		float num2 = 1f;
		string text = GlobalAssets.GetSound("Ore_bump_Rock", false);
		switch (element.state & Element.State.Solid)
		{
		case Element.State.Vacuum:
			text = GlobalAssets.GetSound("ConduitBlob_Gas", false);
			break;
		case Element.State.Gas:
			text = GlobalAssets.GetSound("ConduitBlob_Gas", false);
			break;
		case Element.State.Liquid:
			text = GlobalAssets.GetSound("ConduitBlob_Liquid", false);
			break;
		case Element.State.Solid:
			text = GlobalAssets.GetSound("Ore_bump_" + element.substance.GetMiningSound(), false);
			if (text == null)
			{
				text = GlobalAssets.GetSound("Ore_bump_Rock", false);
			}
			num = 0.7f;
			num2 = 2f;
			break;
		}
		this.ev = KFMOD.CreateInstance(text);
		ATTRIBUTES_3D attributes_3D = SoundListenerController.Instance.transform.GetPosition().To3DAttributes();
		this.ev.set3DAttributes(attributes_3D);
		this.ev.setVolume(num);
		this.ev.setPitch(num2);
		this.ev.setParameterByName("blobCount", (float)global::UnityEngine.Random.Range(0, 6), false);
		this.ev.setParameterByName("SandboxToggle", 1f, false);
		this.ev.start();
	}

	// Token: 0x060046B7 RID: 18103 RVA: 0x00196597 File Offset: 0x00194797
	private void StopSound()
	{
		this.ev.stop(global::FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
		this.ev.release();
	}

	// Token: 0x04002ED0 RID: 11984
	protected Color radiusIndicatorColor = new Color(0.5f, 0.7f, 0.5f, 0.2f);

	// Token: 0x04002ED1 RID: 11985
	private int currentCell;

	// Token: 0x04002ED2 RID: 11986
	private EventInstance ev;
}
