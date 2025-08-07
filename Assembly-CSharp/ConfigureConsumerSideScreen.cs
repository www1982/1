using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000DE9 RID: 3561
public class ConfigureConsumerSideScreen : SideScreenContent
{
	// Token: 0x06007086 RID: 28806 RVA: 0x002AD6C3 File Offset: 0x002AB8C3
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<IConfigurableConsumer>() != null;
	}

	// Token: 0x06007087 RID: 28807 RVA: 0x002AD6CE File Offset: 0x002AB8CE
	public override void SetTarget(GameObject target)
	{
		base.SetTarget(target);
		this.targetProducer = target.GetComponent<IConfigurableConsumer>();
		if (this.settings == null)
		{
			this.settings = this.targetProducer.GetSettingOptions();
		}
		this.PopulateOptions();
	}

	// Token: 0x06007088 RID: 28808 RVA: 0x002AD704 File Offset: 0x002AB904
	private void ClearOldOptions()
	{
		if (this.descriptor != null)
		{
			this.descriptor.gameObject.SetActive(false);
		}
		for (int i = 0; i < this.settingToggles.Count; i++)
		{
			this.settingToggles[i].gameObject.SetActive(false);
		}
	}

	// Token: 0x06007089 RID: 28809 RVA: 0x002AD760 File Offset: 0x002AB960
	private void PopulateOptions()
	{
		this.ClearOldOptions();
		for (int i = this.settingToggles.Count; i < this.settings.Length; i++)
		{
			IConfigurableConsumerOption setting = this.settings[i];
			HierarchyReferences component = Util.KInstantiateUI(this.consumptionSettingTogglePrefab, this.consumptionSettingToggleContainer.gameObject, true).GetComponent<HierarchyReferences>();
			this.settingToggles.Add(component);
			component.GetReference<LocText>("Label").text = setting.GetName();
			component.GetReference<Image>("Image").sprite = setting.GetIcon();
			MultiToggle reference = component.GetReference<MultiToggle>("Toggle");
			reference.onClick = (global::System.Action)Delegate.Combine(reference.onClick, new global::System.Action(delegate
			{
				this.SelectOption(setting);
			}));
		}
		this.RefreshToggles();
		this.RefreshDetails();
	}

	// Token: 0x0600708A RID: 28810 RVA: 0x002AD84A File Offset: 0x002ABA4A
	private void SelectOption(IConfigurableConsumerOption option)
	{
		this.targetProducer.SetSelectedOption(option);
		this.RefreshToggles();
		this.RefreshDetails();
	}

	// Token: 0x0600708B RID: 28811 RVA: 0x002AD864 File Offset: 0x002ABA64
	private void RefreshToggles()
	{
		for (int i = 0; i < this.settingToggles.Count; i++)
		{
			MultiToggle reference = this.settingToggles[i].GetReference<MultiToggle>("Toggle");
			reference.ChangeState((this.settings[i] == this.targetProducer.GetSelectedOption()) ? 1 : 0);
			reference.gameObject.SetActive(true);
		}
	}

	// Token: 0x0600708C RID: 28812 RVA: 0x002AD8C8 File Offset: 0x002ABAC8
	private void RefreshDetails()
	{
		if (this.descriptor == null)
		{
			GameObject gameObject = Util.KInstantiateUI(this.settingDescriptorPrefab, this.settingEffectRowsContainer.gameObject, true);
			this.descriptor = gameObject.GetComponent<LocText>();
		}
		IConfigurableConsumerOption selectedOption = this.targetProducer.GetSelectedOption();
		if (selectedOption != null)
		{
			this.descriptor.text = selectedOption.GetDetailedDescription();
			this.selectedOptionNameLabel.text = "<b>" + selectedOption.GetName() + "</b>";
			this.descriptor.gameObject.SetActive(true);
			return;
		}
		this.selectedOptionNameLabel.text = UI.UISIDESCREENS.FABRICATORSIDESCREEN.NORECIPESELECTED;
	}

	// Token: 0x0600708D RID: 28813 RVA: 0x002AD96E File Offset: 0x002ABB6E
	public override int GetSideScreenSortOrder()
	{
		return 1;
	}

	// Token: 0x04004D75 RID: 19829
	[SerializeField]
	private RectTransform consumptionSettingToggleContainer;

	// Token: 0x04004D76 RID: 19830
	[SerializeField]
	private GameObject consumptionSettingTogglePrefab;

	// Token: 0x04004D77 RID: 19831
	[SerializeField]
	private RectTransform settingRequirementRowsContainer;

	// Token: 0x04004D78 RID: 19832
	[SerializeField]
	private RectTransform settingEffectRowsContainer;

	// Token: 0x04004D79 RID: 19833
	[SerializeField]
	private LocText selectedOptionNameLabel;

	// Token: 0x04004D7A RID: 19834
	[SerializeField]
	private GameObject settingDescriptorPrefab;

	// Token: 0x04004D7B RID: 19835
	private IConfigurableConsumer targetProducer;

	// Token: 0x04004D7C RID: 19836
	private IConfigurableConsumerOption[] settings;

	// Token: 0x04004D7D RID: 19837
	private LocText descriptor;

	// Token: 0x04004D7E RID: 19838
	private List<HierarchyReferences> settingToggles = new List<HierarchyReferences>();

	// Token: 0x04004D7F RID: 19839
	private List<GameObject> requirementRows = new List<GameObject>();
}
