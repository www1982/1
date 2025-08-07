using System;
using System.Collections.Generic;
using STRINGS;

namespace Klei.AI
{
	// Token: 0x02000FDE RID: 4062
	public class FoodSickness : Sickness
	{
		// Token: 0x06007D9E RID: 32158 RVA: 0x00323F58 File Offset: 0x00322158
		public FoodSickness()
			: base("FoodSickness", Sickness.SicknessType.Pathogen, Sickness.Severity.Minor, 0.005f, new List<Sickness.InfectionVector> { Sickness.InfectionVector.Digestion }, 1020f, "FoodSicknessRecovery")
		{
			base.AddSicknessComponent(new CommonSickEffectSickness());
			base.AddSicknessComponent(new AttributeModifierSickness(new AttributeModifier[]
			{
				new AttributeModifier("BladderDelta", 0.33333334f, DUPLICANTS.DISEASES.FOODSICKNESS.NAME, false, false, true),
				new AttributeModifier("ToiletEfficiency", -0.2f, DUPLICANTS.DISEASES.FOODSICKNESS.NAME, false, false, true),
				new AttributeModifier("StaminaDelta", -0.05f, DUPLICANTS.DISEASES.FOODSICKNESS.NAME, false, false, true)
			}));
			base.AddSicknessComponent(new AnimatedSickness(new HashedString[] { "anim_idle_sick_kanim" }, Db.Get().Expressions.Sick));
			base.AddSicknessComponent(new PeriodicEmoteSickness(Db.Get().Emotes.Minion.Sick, 10f));
		}

		// Token: 0x04005EC7 RID: 24263
		public const string ID = "FoodSickness";

		// Token: 0x04005EC8 RID: 24264
		public const string RECOVERY_ID = "FoodSicknessRecovery";

		// Token: 0x04005EC9 RID: 24265
		private const float VOMIT_FREQUENCY = 200f;
	}
}
