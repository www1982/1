using System;
using System.Linq;
using TUNING;
using UnityEngine;

// Token: 0x020007CC RID: 1996
public class SpiceGrinderWorkable : Workable, IConfigurableConsumer
{
	// Token: 0x0600358C RID: 13708 RVA: 0x0012B46C File Offset: 0x0012966C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.requiredSkillPerk = Db.Get().SkillPerks.CanSpiceGrinder.Id;
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Spicing;
		this.attributeConverter = Db.Get().AttributeConverters.CookingSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.PART_DAY_EXPERIENCE;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.Cooking.Id;
		this.skillExperienceMultiplier = SKILLS.PART_DAY_EXPERIENCE;
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_spice_grinder_kanim") };
		base.SetWorkTime(5f);
		this.showProgressBar = true;
		this.lightEfficiencyBonus = true;
	}

	// Token: 0x0600358D RID: 13709 RVA: 0x0012B52C File Offset: 0x0012972C
	protected override void OnStartWork(WorkerBase worker)
	{
		if (this.Grinder.CurrentFood != null)
		{
			float num = this.Grinder.CurrentFood.Calories * 0.001f / 1000f;
			base.SetWorkTime(num * 5f);
		}
		else
		{
			global::Debug.LogWarning("SpiceGrider attempted to start spicing with no food");
			base.StopWork(worker, true);
		}
		this.Grinder.UpdateFoodSymbol();
	}

	// Token: 0x0600358E RID: 13710 RVA: 0x0012B595 File Offset: 0x00129795
	protected override void OnAbortWork(WorkerBase worker)
	{
		if (this.Grinder.CurrentFood == null)
		{
			return;
		}
		this.Grinder.UpdateFoodSymbol();
	}

	// Token: 0x0600358F RID: 13711 RVA: 0x0012B5B6 File Offset: 0x001297B6
	protected override void OnCompleteWork(WorkerBase worker)
	{
		if (this.Grinder.CurrentFood == null)
		{
			return;
		}
		this.Grinder.SpiceFood();
	}

	// Token: 0x06003590 RID: 13712 RVA: 0x0012B5D8 File Offset: 0x001297D8
	public IConfigurableConsumerOption[] GetSettingOptions()
	{
		return SpiceGrinder.SettingOptions.Values.ToArray<SpiceGrinder.Option>();
	}

	// Token: 0x06003591 RID: 13713 RVA: 0x0012B5F6 File Offset: 0x001297F6
	public IConfigurableConsumerOption GetSelectedOption()
	{
		return this.Grinder.SelectedOption;
	}

	// Token: 0x06003592 RID: 13714 RVA: 0x0012B603 File Offset: 0x00129803
	public void SetSelectedOption(IConfigurableConsumerOption option)
	{
		this.Grinder.OnOptionSelected(option as SpiceGrinder.Option);
	}

	// Token: 0x04002055 RID: 8277
	[MyCmpAdd]
	public Notifier notifier;

	// Token: 0x04002056 RID: 8278
	[SerializeField]
	public Vector3 finishedSeedDropOffset;

	// Token: 0x04002057 RID: 8279
	public SpiceGrinder.StatesInstance Grinder;
}
