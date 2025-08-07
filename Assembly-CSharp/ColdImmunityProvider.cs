using System;
using System.Collections.Generic;
using Klei.AI;
using UnityEngine;

// Token: 0x02000582 RID: 1410
public class ColdImmunityProvider : EffectImmunityProviderStation<ColdImmunityProvider.Instance>
{
	// Token: 0x0400129C RID: 4764
	public const string PROVIDED_IMMUNITY_EFFECT_NAME = "WarmTouch";

	// Token: 0x020013C3 RID: 5059
	public new class Def : EffectImmunityProviderStation<ColdImmunityProvider.Instance>.Def, IGameObjectEffectDescriptor
	{
		// Token: 0x06008B4D RID: 35661 RVA: 0x00352884 File Offset: 0x00350A84
		public override string[] DefaultAnims()
		{
			return new string[] { "warmup_pre", "warmup_loop", "warmup_pst" };
		}

		// Token: 0x06008B4E RID: 35662 RVA: 0x003528A4 File Offset: 0x00350AA4
		public override string DefaultAnimFileName()
		{
			return "anim_warmup_kanim";
		}

		// Token: 0x06008B4F RID: 35663 RVA: 0x003528AC File Offset: 0x00350AAC
		public List<Descriptor> GetDescriptors(GameObject go)
		{
			return new List<Descriptor>
			{
				new Descriptor(Strings.Get("STRINGS.DUPLICANTS.MODIFIERS." + "WarmTouch".ToUpper() + ".PROVIDERS_NAME"), Strings.Get("STRINGS.DUPLICANTS.MODIFIERS." + "WarmTouch".ToUpper() + ".PROVIDERS_TOOLTIP"), Descriptor.DescriptorType.Effect, false)
			};
		}
	}

	// Token: 0x020013C4 RID: 5060
	public new class Instance : EffectImmunityProviderStation<ColdImmunityProvider.Instance>.BaseInstance
	{
		// Token: 0x06008B51 RID: 35665 RVA: 0x00352919 File Offset: 0x00350B19
		public Instance(IStateMachineTarget master, ColdImmunityProvider.Def def)
			: base(master, def)
		{
		}

		// Token: 0x06008B52 RID: 35666 RVA: 0x00352923 File Offset: 0x00350B23
		protected override void ApplyImmunityEffect(Effects target)
		{
			target.Add("WarmTouch", true);
		}
	}
}
