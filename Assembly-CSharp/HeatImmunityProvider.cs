using System;
using Klei.AI;

// Token: 0x020005C0 RID: 1472
public class HeatImmunityProvider : EffectImmunityProviderStation<HeatImmunityProvider.Instance>
{
	// Token: 0x040013D2 RID: 5074
	public const string PROVIDED_IMMUNITY_EFFECT_NAME = "RefreshingTouch";

	// Token: 0x0200145E RID: 5214
	public new class Def : EffectImmunityProviderStation<HeatImmunityProvider.Instance>.Def
	{
	}

	// Token: 0x0200145F RID: 5215
	public new class Instance : EffectImmunityProviderStation<HeatImmunityProvider.Instance>.BaseInstance
	{
		// Token: 0x06008D86 RID: 36230 RVA: 0x00358C9C File Offset: 0x00356E9C
		public Instance(IStateMachineTarget master, HeatImmunityProvider.Def def)
			: base(master, def)
		{
		}

		// Token: 0x06008D87 RID: 36231 RVA: 0x00358CA6 File Offset: 0x00356EA6
		protected override void ApplyImmunityEffect(Effects target)
		{
			target.Add("RefreshingTouch", true);
		}
	}
}
