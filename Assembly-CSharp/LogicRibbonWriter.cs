using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000773 RID: 1907
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/LogicRibbonWriter")]
public class LogicRibbonWriter : KMonoBehaviour, ILogicRibbonBitSelector, IRender200ms
{
	// Token: 0x060031CF RID: 12751 RVA: 0x0011A957 File Offset: 0x00118B57
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<LogicRibbonWriter>(-905833192, LogicRibbonWriter.OnCopySettingsDelegate);
	}

	// Token: 0x060031D0 RID: 12752 RVA: 0x0011A970 File Offset: 0x00118B70
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.Subscribe<LogicRibbonWriter>(-801688580, LogicRibbonWriter.OnLogicValueChangedDelegate);
		this.ports = base.GetComponent<LogicPorts>();
		this.kbac = base.GetComponent<KBatchedAnimController>();
		this.kbac.Play("idle", KAnim.PlayMode.Once, 1f, 0f);
	}

	// Token: 0x060031D1 RID: 12753 RVA: 0x0011A9CC File Offset: 0x00118BCC
	public void OnLogicValueChanged(object data)
	{
		LogicValueChanged logicValueChanged = (LogicValueChanged)data;
		if (logicValueChanged.portID != LogicRibbonWriter.INPUT_PORT_ID)
		{
			return;
		}
		this.currentValue = logicValueChanged.newValue;
		this.UpdateLogicCircuit();
		this.UpdateVisuals();
	}

	// Token: 0x060031D2 RID: 12754 RVA: 0x0011AA0C File Offset: 0x00118C0C
	private void OnCopySettings(object data)
	{
		LogicRibbonWriter component = ((GameObject)data).GetComponent<LogicRibbonWriter>();
		if (component != null)
		{
			this.SetBitSelection(component.selectedBit);
		}
	}

	// Token: 0x060031D3 RID: 12755 RVA: 0x0011AA3C File Offset: 0x00118C3C
	private void UpdateLogicCircuit()
	{
		int num = this.currentValue << this.selectedBit;
		base.GetComponent<LogicPorts>().SendSignal(LogicRibbonWriter.OUTPUT_PORT_ID, num);
	}

	// Token: 0x060031D4 RID: 12756 RVA: 0x0011AA6B File Offset: 0x00118C6B
	public void Render200ms(float dt)
	{
		this.UpdateVisuals();
	}

	// Token: 0x060031D5 RID: 12757 RVA: 0x0011AA74 File Offset: 0x00118C74
	private LogicCircuitNetwork GetInputNetwork()
	{
		LogicCircuitNetwork logicCircuitNetwork = null;
		if (this.ports != null)
		{
			int portCell = this.ports.GetPortCell(LogicRibbonWriter.INPUT_PORT_ID);
			logicCircuitNetwork = Game.Instance.logicCircuitManager.GetNetworkForCell(portCell);
		}
		return logicCircuitNetwork;
	}

	// Token: 0x060031D6 RID: 12758 RVA: 0x0011AAB4 File Offset: 0x00118CB4
	private LogicCircuitNetwork GetOutputNetwork()
	{
		LogicCircuitNetwork logicCircuitNetwork = null;
		if (this.ports != null)
		{
			int portCell = this.ports.GetPortCell(LogicRibbonWriter.OUTPUT_PORT_ID);
			logicCircuitNetwork = Game.Instance.logicCircuitManager.GetNetworkForCell(portCell);
		}
		return logicCircuitNetwork;
	}

	// Token: 0x060031D7 RID: 12759 RVA: 0x0011AAF4 File Offset: 0x00118CF4
	public void SetBitSelection(int bit)
	{
		this.selectedBit = bit;
		this.UpdateLogicCircuit();
	}

	// Token: 0x060031D8 RID: 12760 RVA: 0x0011AB03 File Offset: 0x00118D03
	public int GetBitSelection()
	{
		return this.selectedBit;
	}

	// Token: 0x060031D9 RID: 12761 RVA: 0x0011AB0B File Offset: 0x00118D0B
	public int GetBitDepth()
	{
		return this.bitDepth;
	}

	// Token: 0x170002FD RID: 765
	// (get) Token: 0x060031DA RID: 12762 RVA: 0x0011AB13 File Offset: 0x00118D13
	public string SideScreenTitle
	{
		get
		{
			return "STRINGS.UI.UISIDESCREENS.LOGICBITSELECTORSIDESCREEN.RIBBON_WRITER_TITLE";
		}
	}

	// Token: 0x170002FE RID: 766
	// (get) Token: 0x060031DB RID: 12763 RVA: 0x0011AB1A File Offset: 0x00118D1A
	public string SideScreenDescription
	{
		get
		{
			return UI.UISIDESCREENS.LOGICBITSELECTORSIDESCREEN.RIBBON_WRITER_DESCRIPTION;
		}
	}

	// Token: 0x060031DC RID: 12764 RVA: 0x0011AB26 File Offset: 0x00118D26
	public bool SideScreenDisplayWriterDescription()
	{
		return true;
	}

	// Token: 0x060031DD RID: 12765 RVA: 0x0011AB29 File Offset: 0x00118D29
	public bool SideScreenDisplayReaderDescription()
	{
		return false;
	}

	// Token: 0x060031DE RID: 12766 RVA: 0x0011AB2C File Offset: 0x00118D2C
	public bool IsBitActive(int bit)
	{
		LogicCircuitNetwork logicCircuitNetwork = null;
		if (this.ports != null)
		{
			int portCell = this.ports.GetPortCell(LogicRibbonWriter.OUTPUT_PORT_ID);
			logicCircuitNetwork = Game.Instance.logicCircuitManager.GetNetworkForCell(portCell);
		}
		return logicCircuitNetwork != null && logicCircuitNetwork.IsBitActive(bit);
	}

	// Token: 0x060031DF RID: 12767 RVA: 0x0011AB78 File Offset: 0x00118D78
	public int GetInputValue()
	{
		LogicPorts component = base.GetComponent<LogicPorts>();
		if (!(component != null))
		{
			return 0;
		}
		return component.GetInputValue(LogicRibbonWriter.INPUT_PORT_ID);
	}

	// Token: 0x060031E0 RID: 12768 RVA: 0x0011ABA4 File Offset: 0x00118DA4
	public int GetOutputValue()
	{
		LogicPorts component = base.GetComponent<LogicPorts>();
		if (!(component != null))
		{
			return 0;
		}
		return component.GetOutputValue(LogicRibbonWriter.OUTPUT_PORT_ID);
	}

	// Token: 0x060031E1 RID: 12769 RVA: 0x0011ABD0 File Offset: 0x00118DD0
	public void UpdateVisuals()
	{
		bool inputNetwork = this.GetInputNetwork() != null;
		LogicCircuitNetwork outputNetwork = this.GetOutputNetwork();
		int num = 0;
		if (inputNetwork)
		{
			num++;
			this.kbac.SetSymbolTint(LogicRibbonWriter.INPUT_SYMBOL, LogicCircuitNetwork.IsBitActive(0, this.GetInputValue()) ? this.colorOn : this.colorOff);
		}
		if (outputNetwork != null)
		{
			num += 4;
			this.kbac.SetSymbolTint(LogicRibbonWriter.BIT_ONE_SYMBOL, this.IsBitActive(0) ? this.colorOn : this.colorOff);
			this.kbac.SetSymbolTint(LogicRibbonWriter.BIT_TWO_SYMBOL, this.IsBitActive(1) ? this.colorOn : this.colorOff);
			this.kbac.SetSymbolTint(LogicRibbonWriter.BIT_THREE_SYMBOL, this.IsBitActive(2) ? this.colorOn : this.colorOff);
			this.kbac.SetSymbolTint(LogicRibbonWriter.BIT_FOUR_SYMBOL, this.IsBitActive(3) ? this.colorOn : this.colorOff);
		}
		this.kbac.Play(num.ToString() + "_" + (this.GetBitSelection() + 1).ToString(), KAnim.PlayMode.Once, 1f, 0f);
	}

	// Token: 0x04001DF0 RID: 7664
	public static readonly HashedString INPUT_PORT_ID = new HashedString("LogicRibbonWriterInput");

	// Token: 0x04001DF1 RID: 7665
	public static readonly HashedString OUTPUT_PORT_ID = new HashedString("LogicRibbonWriterOutput");

	// Token: 0x04001DF2 RID: 7666
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x04001DF3 RID: 7667
	private static readonly EventSystem.IntraObjectHandler<LogicRibbonWriter> OnLogicValueChangedDelegate = new EventSystem.IntraObjectHandler<LogicRibbonWriter>(delegate(LogicRibbonWriter component, object data)
	{
		component.OnLogicValueChanged(data);
	});

	// Token: 0x04001DF4 RID: 7668
	private static readonly EventSystem.IntraObjectHandler<LogicRibbonWriter> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<LogicRibbonWriter>(delegate(LogicRibbonWriter component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x04001DF5 RID: 7669
	private LogicPorts ports;

	// Token: 0x04001DF6 RID: 7670
	public int bitDepth = 4;

	// Token: 0x04001DF7 RID: 7671
	[Serialize]
	public int selectedBit;

	// Token: 0x04001DF8 RID: 7672
	[Serialize]
	private int currentValue;

	// Token: 0x04001DF9 RID: 7673
	private KBatchedAnimController kbac;

	// Token: 0x04001DFA RID: 7674
	private Color colorOn = new Color(0.34117648f, 0.7254902f, 0.36862746f);

	// Token: 0x04001DFB RID: 7675
	private Color colorOff = new Color(0.9529412f, 0.2901961f, 0.2784314f);

	// Token: 0x04001DFC RID: 7676
	private static KAnimHashedString BIT_ONE_SYMBOL = "bit1_bloom";

	// Token: 0x04001DFD RID: 7677
	private static KAnimHashedString BIT_TWO_SYMBOL = "bit2_bloom";

	// Token: 0x04001DFE RID: 7678
	private static KAnimHashedString BIT_THREE_SYMBOL = "bit3_bloom";

	// Token: 0x04001DFF RID: 7679
	private static KAnimHashedString BIT_FOUR_SYMBOL = "bit4_bloom";

	// Token: 0x04001E00 RID: 7680
	private static KAnimHashedString INPUT_SYMBOL = "input_light_bloom";
}
