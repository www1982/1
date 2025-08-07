using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000772 RID: 1906
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/LogicRibbonReader")]
public class LogicRibbonReader : KMonoBehaviour, ILogicRibbonBitSelector, IRender200ms
{
	// Token: 0x060031BA RID: 12730 RVA: 0x0011A424 File Offset: 0x00118624
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.Subscribe<LogicRibbonReader>(-801688580, LogicRibbonReader.OnLogicValueChangedDelegate);
		this.ports = base.GetComponent<LogicPorts>();
		this.kbac = base.GetComponent<KBatchedAnimController>();
		this.kbac.Play("idle", KAnim.PlayMode.Once, 1f, 0f);
	}

	// Token: 0x060031BB RID: 12731 RVA: 0x0011A480 File Offset: 0x00118680
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<LogicRibbonReader>(-905833192, LogicRibbonReader.OnCopySettingsDelegate);
	}

	// Token: 0x060031BC RID: 12732 RVA: 0x0011A49C File Offset: 0x0011869C
	public void OnLogicValueChanged(object data)
	{
		LogicValueChanged logicValueChanged = (LogicValueChanged)data;
		if (logicValueChanged.portID != LogicRibbonReader.INPUT_PORT_ID)
		{
			return;
		}
		this.currentValue = logicValueChanged.newValue;
		this.UpdateLogicCircuit();
		this.UpdateVisuals();
	}

	// Token: 0x060031BD RID: 12733 RVA: 0x0011A4DC File Offset: 0x001186DC
	private void OnCopySettings(object data)
	{
		LogicRibbonReader component = ((GameObject)data).GetComponent<LogicRibbonReader>();
		if (component != null)
		{
			this.SetBitSelection(component.selectedBit);
		}
	}

	// Token: 0x060031BE RID: 12734 RVA: 0x0011A50C File Offset: 0x0011870C
	private void UpdateLogicCircuit()
	{
		LogicPorts component = base.GetComponent<LogicPorts>();
		LogicWire.BitDepth bitDepth = LogicWire.BitDepth.NumRatings;
		int portCell = component.GetPortCell(LogicRibbonReader.OUTPUT_PORT_ID);
		GameObject gameObject = Grid.Objects[portCell, 31];
		if (gameObject != null)
		{
			LogicWire component2 = gameObject.GetComponent<LogicWire>();
			if (component2 != null)
			{
				bitDepth = component2.MaxBitDepth;
			}
		}
		if (bitDepth != LogicWire.BitDepth.OneBit && bitDepth == LogicWire.BitDepth.FourBit)
		{
			int num = this.currentValue >> this.selectedBit;
			component.SendSignal(LogicRibbonReader.OUTPUT_PORT_ID, num);
		}
		else
		{
			int num = this.currentValue & (1 << this.selectedBit);
			component.SendSignal(LogicRibbonReader.OUTPUT_PORT_ID, (num > 0) ? 1 : 0);
		}
		this.UpdateVisuals();
	}

	// Token: 0x060031BF RID: 12735 RVA: 0x0011A5B5 File Offset: 0x001187B5
	public void Render200ms(float dt)
	{
		this.UpdateVisuals();
	}

	// Token: 0x060031C0 RID: 12736 RVA: 0x0011A5BD File Offset: 0x001187BD
	public void SetBitSelection(int bit)
	{
		this.selectedBit = bit;
		this.UpdateLogicCircuit();
	}

	// Token: 0x060031C1 RID: 12737 RVA: 0x0011A5CC File Offset: 0x001187CC
	public int GetBitSelection()
	{
		return this.selectedBit;
	}

	// Token: 0x060031C2 RID: 12738 RVA: 0x0011A5D4 File Offset: 0x001187D4
	public int GetBitDepth()
	{
		return this.bitDepth;
	}

	// Token: 0x170002FB RID: 763
	// (get) Token: 0x060031C3 RID: 12739 RVA: 0x0011A5DC File Offset: 0x001187DC
	public string SideScreenTitle
	{
		get
		{
			return "STRINGS.UI.UISIDESCREENS.LOGICBITSELECTORSIDESCREEN.RIBBON_READER_TITLE";
		}
	}

	// Token: 0x170002FC RID: 764
	// (get) Token: 0x060031C4 RID: 12740 RVA: 0x0011A5E3 File Offset: 0x001187E3
	public string SideScreenDescription
	{
		get
		{
			return UI.UISIDESCREENS.LOGICBITSELECTORSIDESCREEN.RIBBON_READER_DESCRIPTION;
		}
	}

	// Token: 0x060031C5 RID: 12741 RVA: 0x0011A5EF File Offset: 0x001187EF
	public bool SideScreenDisplayWriterDescription()
	{
		return false;
	}

	// Token: 0x060031C6 RID: 12742 RVA: 0x0011A5F2 File Offset: 0x001187F2
	public bool SideScreenDisplayReaderDescription()
	{
		return true;
	}

	// Token: 0x060031C7 RID: 12743 RVA: 0x0011A5F8 File Offset: 0x001187F8
	public bool IsBitActive(int bit)
	{
		LogicCircuitNetwork logicCircuitNetwork = null;
		if (this.ports != null)
		{
			int portCell = this.ports.GetPortCell(LogicRibbonReader.INPUT_PORT_ID);
			logicCircuitNetwork = Game.Instance.logicCircuitManager.GetNetworkForCell(portCell);
		}
		return logicCircuitNetwork != null && logicCircuitNetwork.IsBitActive(bit);
	}

	// Token: 0x060031C8 RID: 12744 RVA: 0x0011A644 File Offset: 0x00118844
	public int GetInputValue()
	{
		LogicPorts component = base.GetComponent<LogicPorts>();
		if (!(component != null))
		{
			return 0;
		}
		return component.GetInputValue(LogicRibbonReader.INPUT_PORT_ID);
	}

	// Token: 0x060031C9 RID: 12745 RVA: 0x0011A670 File Offset: 0x00118870
	public int GetOutputValue()
	{
		LogicPorts component = base.GetComponent<LogicPorts>();
		if (!(component != null))
		{
			return 0;
		}
		return component.GetOutputValue(LogicRibbonReader.OUTPUT_PORT_ID);
	}

	// Token: 0x060031CA RID: 12746 RVA: 0x0011A69C File Offset: 0x0011889C
	private LogicCircuitNetwork GetInputNetwork()
	{
		LogicCircuitNetwork logicCircuitNetwork = null;
		if (this.ports != null)
		{
			int portCell = this.ports.GetPortCell(LogicRibbonReader.INPUT_PORT_ID);
			logicCircuitNetwork = Game.Instance.logicCircuitManager.GetNetworkForCell(portCell);
		}
		return logicCircuitNetwork;
	}

	// Token: 0x060031CB RID: 12747 RVA: 0x0011A6DC File Offset: 0x001188DC
	private LogicCircuitNetwork GetOutputNetwork()
	{
		LogicCircuitNetwork logicCircuitNetwork = null;
		if (this.ports != null)
		{
			int portCell = this.ports.GetPortCell(LogicRibbonReader.OUTPUT_PORT_ID);
			logicCircuitNetwork = Game.Instance.logicCircuitManager.GetNetworkForCell(portCell);
		}
		return logicCircuitNetwork;
	}

	// Token: 0x060031CC RID: 12748 RVA: 0x0011A71C File Offset: 0x0011891C
	public void UpdateVisuals()
	{
		bool inputNetwork = this.GetInputNetwork() != null;
		LogicCircuitNetwork outputNetwork = this.GetOutputNetwork();
		this.GetInputValue();
		int num = 0;
		if (inputNetwork)
		{
			num += 4;
			this.kbac.SetSymbolTint(this.BIT_ONE_SYMBOL, this.IsBitActive(0) ? this.colorOn : this.colorOff);
			this.kbac.SetSymbolTint(this.BIT_TWO_SYMBOL, this.IsBitActive(1) ? this.colorOn : this.colorOff);
			this.kbac.SetSymbolTint(this.BIT_THREE_SYMBOL, this.IsBitActive(2) ? this.colorOn : this.colorOff);
			this.kbac.SetSymbolTint(this.BIT_FOUR_SYMBOL, this.IsBitActive(3) ? this.colorOn : this.colorOff);
		}
		if (outputNetwork != null)
		{
			num++;
			this.kbac.SetSymbolTint(this.OUTPUT_SYMBOL, LogicCircuitNetwork.IsBitActive(0, this.GetOutputValue()) ? this.colorOn : this.colorOff);
		}
		this.kbac.Play(num.ToString() + "_" + (this.GetBitSelection() + 1).ToString(), KAnim.PlayMode.Once, 1f, 0f);
	}

	// Token: 0x04001DDF RID: 7647
	public static readonly HashedString INPUT_PORT_ID = new HashedString("LogicRibbonReaderInput");

	// Token: 0x04001DE0 RID: 7648
	public static readonly HashedString OUTPUT_PORT_ID = new HashedString("LogicRibbonReaderOutput");

	// Token: 0x04001DE1 RID: 7649
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x04001DE2 RID: 7650
	private static readonly EventSystem.IntraObjectHandler<LogicRibbonReader> OnLogicValueChangedDelegate = new EventSystem.IntraObjectHandler<LogicRibbonReader>(delegate(LogicRibbonReader component, object data)
	{
		component.OnLogicValueChanged(data);
	});

	// Token: 0x04001DE3 RID: 7651
	private static readonly EventSystem.IntraObjectHandler<LogicRibbonReader> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<LogicRibbonReader>(delegate(LogicRibbonReader component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x04001DE4 RID: 7652
	private KAnimHashedString BIT_ONE_SYMBOL = "bit1_bloom";

	// Token: 0x04001DE5 RID: 7653
	private KAnimHashedString BIT_TWO_SYMBOL = "bit2_bloom";

	// Token: 0x04001DE6 RID: 7654
	private KAnimHashedString BIT_THREE_SYMBOL = "bit3_bloom";

	// Token: 0x04001DE7 RID: 7655
	private KAnimHashedString BIT_FOUR_SYMBOL = "bit4_bloom";

	// Token: 0x04001DE8 RID: 7656
	private KAnimHashedString OUTPUT_SYMBOL = "output_light_bloom";

	// Token: 0x04001DE9 RID: 7657
	private KBatchedAnimController kbac;

	// Token: 0x04001DEA RID: 7658
	private Color colorOn = new Color(0.34117648f, 0.7254902f, 0.36862746f);

	// Token: 0x04001DEB RID: 7659
	private Color colorOff = new Color(0.9529412f, 0.2901961f, 0.2784314f);

	// Token: 0x04001DEC RID: 7660
	private LogicPorts ports;

	// Token: 0x04001DED RID: 7661
	public int bitDepth = 4;

	// Token: 0x04001DEE RID: 7662
	[Serialize]
	public int selectedBit;

	// Token: 0x04001DEF RID: 7663
	[Serialize]
	private int currentValue;
}
