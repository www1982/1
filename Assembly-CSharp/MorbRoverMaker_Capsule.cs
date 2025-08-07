using System;
using UnityEngine;

// Token: 0x02000344 RID: 836
public class MorbRoverMaker_Capsule : KMonoBehaviour
{
	// Token: 0x06001145 RID: 4421 RVA: 0x00065114 File Offset: 0x00063314
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.MorbDevelopment_Meter = new MeterController(this.buildingAnimCtr, "meter_morb_target", "meter_morb_1", Meter.Offset.UserSpecified, Grid.SceneLayer.BuildingBack, Array.Empty<string>());
		this.GermMeter = new MeterController(this.buildingAnimCtr, "meter_germs_target", "meter_germs", Meter.Offset.UserSpecified, Grid.SceneLayer.BuildingBack, Array.Empty<string>());
		this.MorbDevelopment_Capsule_Meter = new MeterController(this.buildingAnimCtr, "meter_capsule_target", "meter_capsule", Meter.Offset.UserSpecified, Grid.SceneLayer.BuildingBack, Array.Empty<string>());
		this.MorbDevelopment_Capsule_Meter.meterController.onAnimComplete += this.OnGermAddedAnimationComplete;
	}

	// Token: 0x06001146 RID: 4422 RVA: 0x000651AC File Offset: 0x000633AC
	private void OnGermAddedAnimationComplete(HashedString animName)
	{
		if (animName == MorbRoverMaker_Capsule.MORB_CAPSULE_METER_PUMP_ANIM_NAME)
		{
			this.MorbDevelopment_Capsule_Meter.meterController.Play("meter_capsule", KAnim.PlayMode.Once, 1f, 0f);
		}
	}

	// Token: 0x06001147 RID: 4423 RVA: 0x000651E0 File Offset: 0x000633E0
	public void PlayPumpGermsAnimation()
	{
		if (this.MorbDevelopment_Capsule_Meter.meterController.currentAnim != MorbRoverMaker_Capsule.MORB_CAPSULE_METER_PUMP_ANIM_NAME)
		{
			this.MorbDevelopment_Capsule_Meter.meterController.Play(MorbRoverMaker_Capsule.MORB_CAPSULE_METER_PUMP_ANIM_NAME, KAnim.PlayMode.Once, 1f, 0f);
		}
	}

	// Token: 0x06001148 RID: 4424 RVA: 0x00065220 File Offset: 0x00063420
	public void SetMorbDevelopmentProgress(float morbDevelopmentProgress)
	{
		global::Debug.Assert(true, "MORB PHASES COUNT needs to be larger than 0");
		string text = "meter_morb_" + (1 + Mathf.FloorToInt(morbDevelopmentProgress * 4f)).ToString();
		if (this.MorbDevelopment_Meter.meterController.currentAnim != text)
		{
			this.MorbDevelopment_Meter.meterController.Play(text, KAnim.PlayMode.Loop, 1f, 0f);
		}
	}

	// Token: 0x06001149 RID: 4425 RVA: 0x00065297 File Offset: 0x00063497
	public void SetGermMeterProgress(float progress)
	{
		this.GermMeter.SetPositionPercent(progress);
	}

	// Token: 0x04000AE9 RID: 2793
	public const byte MORB_PHASES_COUNT = 5;

	// Token: 0x04000AEA RID: 2794
	public const byte MORB_FIRST_PHASE_INDEX = 1;

	// Token: 0x04000AEB RID: 2795
	private const string GERM_METER_TARGET_NAME = "meter_germs_target";

	// Token: 0x04000AEC RID: 2796
	private const string GERM_METER_ANIMATION_NAME = "meter_germs";

	// Token: 0x04000AED RID: 2797
	private const string MORB_METER_TARGET_NAME = "meter_morb_target";

	// Token: 0x04000AEE RID: 2798
	private const string MORB_METER_ANIMATION_NAME = "meter_morb";

	// Token: 0x04000AEF RID: 2799
	private const string MORB_CAPSULE_METER_TARGET_NAME = "meter_capsule_target";

	// Token: 0x04000AF0 RID: 2800
	private const string MORB_CAPSULE_METER_ANIMATION_NAME = "meter_capsule";

	// Token: 0x04000AF1 RID: 2801
	private static HashedString MORB_CAPSULE_METER_PUMP_ANIM_NAME = new HashedString("germ_pump");

	// Token: 0x04000AF2 RID: 2802
	[MyCmpGet]
	private KBatchedAnimController buildingAnimCtr;

	// Token: 0x04000AF3 RID: 2803
	private MeterController MorbDevelopment_Meter;

	// Token: 0x04000AF4 RID: 2804
	private MeterController MorbDevelopment_Capsule_Meter;

	// Token: 0x04000AF5 RID: 2805
	private MeterController GermMeter;
}
