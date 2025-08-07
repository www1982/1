using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000DED RID: 3565
public class CounterSideScreen : SideScreenContent, IRender200ms
{
	// Token: 0x0600709A RID: 28826 RVA: 0x002AD98F File Offset: 0x002ABB8F
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x0600709B RID: 28827 RVA: 0x002AD998 File Offset: 0x002ABB98
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.resetButton.onClick += this.ResetCounter;
		this.incrementMaxButton.onClick += this.IncrementMaxCount;
		this.decrementMaxButton.onClick += this.DecrementMaxCount;
		this.incrementModeButton.onClick += this.ToggleMode;
		this.advancedModeToggle.onClick += this.ToggleAdvanced;
		this.maxCountInput.onEndEdit += delegate
		{
			this.UpdateMaxCountFromTextInput(this.maxCountInput.currentValue);
		};
		this.UpdateCurrentCountLabel(this.targetLogicCounter.currentCount);
	}

	// Token: 0x0600709C RID: 28828 RVA: 0x002ADA46 File Offset: 0x002ABC46
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<LogicCounter>() != null;
	}

	// Token: 0x0600709D RID: 28829 RVA: 0x002ADA54 File Offset: 0x002ABC54
	public override void SetTarget(GameObject target)
	{
		base.SetTarget(target);
		this.maxCountInput.minValue = 1f;
		this.maxCountInput.maxValue = 10f;
		this.targetLogicCounter = target.GetComponent<LogicCounter>();
		this.UpdateCurrentCountLabel(this.targetLogicCounter.currentCount);
		this.UpdateMaxCountLabel(this.targetLogicCounter.maxCount);
		this.advancedModeCheckmark.enabled = this.targetLogicCounter.advancedMode;
	}

	// Token: 0x0600709E RID: 28830 RVA: 0x002ADACC File Offset: 0x002ABCCC
	public void Render200ms(float dt)
	{
		if (this.targetLogicCounter == null)
		{
			return;
		}
		this.UpdateCurrentCountLabel(this.targetLogicCounter.currentCount);
	}

	// Token: 0x0600709F RID: 28831 RVA: 0x002ADAF0 File Offset: 0x002ABCF0
	private void UpdateCurrentCountLabel(int value)
	{
		string text = value.ToString();
		if (value == this.targetLogicCounter.maxCount)
		{
			text = UI.FormatAsAutomationState(text, UI.AutomationState.Active);
		}
		else
		{
			text = UI.FormatAsAutomationState(text, UI.AutomationState.Standby);
		}
		this.currentCount.text = (this.targetLogicCounter.advancedMode ? string.Format(UI.UISIDESCREENS.COUNTER_SIDE_SCREEN.CURRENT_COUNT_ADVANCED, text) : string.Format(UI.UISIDESCREENS.COUNTER_SIDE_SCREEN.CURRENT_COUNT_SIMPLE, text));
	}

	// Token: 0x060070A0 RID: 28832 RVA: 0x002ADB5F File Offset: 0x002ABD5F
	private void UpdateMaxCountLabel(int value)
	{
		this.maxCountInput.SetAmount((float)value);
	}

	// Token: 0x060070A1 RID: 28833 RVA: 0x002ADB6E File Offset: 0x002ABD6E
	private void UpdateMaxCountFromTextInput(float newValue)
	{
		this.SetMaxCount((int)newValue);
	}

	// Token: 0x060070A2 RID: 28834 RVA: 0x002ADB78 File Offset: 0x002ABD78
	private void IncrementMaxCount()
	{
		this.SetMaxCount(this.targetLogicCounter.maxCount + 1);
	}

	// Token: 0x060070A3 RID: 28835 RVA: 0x002ADB8D File Offset: 0x002ABD8D
	private void DecrementMaxCount()
	{
		this.SetMaxCount(this.targetLogicCounter.maxCount - 1);
	}

	// Token: 0x060070A4 RID: 28836 RVA: 0x002ADBA4 File Offset: 0x002ABDA4
	private void SetMaxCount(int newValue)
	{
		if (newValue > 10)
		{
			newValue = 1;
		}
		if (newValue < 1)
		{
			newValue = 10;
		}
		if (newValue < this.targetLogicCounter.currentCount)
		{
			this.targetLogicCounter.currentCount = newValue;
		}
		this.targetLogicCounter.maxCount = newValue;
		this.UpdateCounterStates();
		this.UpdateMaxCountLabel(newValue);
	}

	// Token: 0x060070A5 RID: 28837 RVA: 0x002ADBF4 File Offset: 0x002ABDF4
	private void ResetCounter()
	{
		this.targetLogicCounter.ResetCounter();
	}

	// Token: 0x060070A6 RID: 28838 RVA: 0x002ADC01 File Offset: 0x002ABE01
	private void UpdateCounterStates()
	{
		this.targetLogicCounter.SetCounterState();
		this.targetLogicCounter.UpdateLogicCircuit();
		this.targetLogicCounter.UpdateVisualState(true);
		this.targetLogicCounter.UpdateMeter();
	}

	// Token: 0x060070A7 RID: 28839 RVA: 0x002ADC30 File Offset: 0x002ABE30
	private void ToggleMode()
	{
	}

	// Token: 0x060070A8 RID: 28840 RVA: 0x002ADC34 File Offset: 0x002ABE34
	private void ToggleAdvanced()
	{
		this.targetLogicCounter.advancedMode = !this.targetLogicCounter.advancedMode;
		this.advancedModeCheckmark.enabled = this.targetLogicCounter.advancedMode;
		this.UpdateCurrentCountLabel(this.targetLogicCounter.currentCount);
		this.UpdateCounterStates();
	}

	// Token: 0x04004D80 RID: 19840
	public LogicCounter targetLogicCounter;

	// Token: 0x04004D81 RID: 19841
	public KButton resetButton;

	// Token: 0x04004D82 RID: 19842
	public KButton incrementMaxButton;

	// Token: 0x04004D83 RID: 19843
	public KButton decrementMaxButton;

	// Token: 0x04004D84 RID: 19844
	public KButton incrementModeButton;

	// Token: 0x04004D85 RID: 19845
	public KToggle advancedModeToggle;

	// Token: 0x04004D86 RID: 19846
	public KImage advancedModeCheckmark;

	// Token: 0x04004D87 RID: 19847
	public LocText currentCount;

	// Token: 0x04004D88 RID: 19848
	[SerializeField]
	private KNumberInputField maxCountInput;
}
