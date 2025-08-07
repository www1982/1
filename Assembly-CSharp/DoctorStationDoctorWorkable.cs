using System;
using TUNING;
using UnityEngine;

// Token: 0x020008C6 RID: 2246
[AddComponentMenu("KMonoBehaviour/Workable/DoctorStationDoctorWorkable")]
public class DoctorStationDoctorWorkable : Workable
{
	// Token: 0x06003E43 RID: 15939 RVA: 0x0015C5FB File Offset: 0x0015A7FB
	private DoctorStationDoctorWorkable()
	{
		this.synchronizeAnims = false;
	}

	// Token: 0x06003E44 RID: 15940 RVA: 0x0015C60C File Offset: 0x0015A80C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.attributeConverter = Db.Get().AttributeConverters.DoctorSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.BARELY_EVER_EXPERIENCE;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.MedicalAid.Id;
		this.skillExperienceMultiplier = SKILLS.BARELY_EVER_EXPERIENCE;
	}

	// Token: 0x06003E45 RID: 15941 RVA: 0x0015C664 File Offset: 0x0015A864
	protected override void OnSpawn()
	{
		base.OnSpawn();
	}

	// Token: 0x06003E46 RID: 15942 RVA: 0x0015C66C File Offset: 0x0015A86C
	protected override void OnStartWork(WorkerBase worker)
	{
		base.OnStartWork(worker);
		this.station.SetHasDoctor(true);
	}

	// Token: 0x06003E47 RID: 15943 RVA: 0x0015C681 File Offset: 0x0015A881
	protected override void OnStopWork(WorkerBase worker)
	{
		base.OnStopWork(worker);
		this.station.SetHasDoctor(false);
	}

	// Token: 0x06003E48 RID: 15944 RVA: 0x0015C696 File Offset: 0x0015A896
	protected override void OnCompleteWork(WorkerBase worker)
	{
		base.OnCompleteWork(worker);
		this.station.CompleteDoctoring();
	}

	// Token: 0x04002649 RID: 9801
	[MyCmpReq]
	private DoctorStation station;
}
