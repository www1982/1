using System;
using UnityEngine;

// Token: 0x02000DFF RID: 3583
public class IncubatorSideScreen : ReceptacleSideScreen
{
	// Token: 0x06007114 RID: 28948 RVA: 0x002B0047 File Offset: 0x002AE247
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<EggIncubator>() != null;
	}

	// Token: 0x06007115 RID: 28949 RVA: 0x002B0058 File Offset: 0x002AE258
	protected override void SetResultDescriptions(GameObject go)
	{
		string text = "";
		InfoDescription component = go.GetComponent<InfoDescription>();
		if (component)
		{
			text += component.description;
		}
		this.descriptionLabel.SetText(text);
	}

	// Token: 0x06007116 RID: 28950 RVA: 0x002B0093 File Offset: 0x002AE293
	protected override bool RequiresAvailableAmountToDeposit()
	{
		return false;
	}

	// Token: 0x06007117 RID: 28951 RVA: 0x002B0096 File Offset: 0x002AE296
	protected override Sprite GetEntityIcon(Tag prefabTag)
	{
		return Def.GetUISprite(Assets.GetPrefab(prefabTag), "ui", false).first;
	}

	// Token: 0x06007118 RID: 28952 RVA: 0x002B00B0 File Offset: 0x002AE2B0
	public override void SetTarget(GameObject target)
	{
		base.SetTarget(target);
		EggIncubator incubator = target.GetComponent<EggIncubator>();
		this.continuousToggle.ChangeState(incubator.autoReplaceEntity ? 0 : 1);
		this.continuousToggle.onClick = delegate
		{
			incubator.autoReplaceEntity = !incubator.autoReplaceEntity;
			this.continuousToggle.ChangeState(incubator.autoReplaceEntity ? 0 : 1);
		};
	}

	// Token: 0x04004DCE RID: 19918
	public DescriptorPanel RequirementsDescriptorPanel;

	// Token: 0x04004DCF RID: 19919
	public DescriptorPanel HarvestDescriptorPanel;

	// Token: 0x04004DD0 RID: 19920
	public DescriptorPanel EffectsDescriptorPanel;

	// Token: 0x04004DD1 RID: 19921
	public MultiToggle continuousToggle;
}
