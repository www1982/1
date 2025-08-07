using System;

namespace Database
{
	// Token: 0x02000F1A RID: 3866
	public class Urges : ResourceSet<Urge>
	{
		// Token: 0x06007A21 RID: 31265 RVA: 0x00307980 File Offset: 0x00305B80
		public Urges()
		{
			this.HealCritical = base.Add(new Urge("HealCritical"));
			this.BeOffline = base.Add(new Urge("BeOffline"));
			this.BeIncapacitated = base.Add(new Urge("BeIncapacitated"));
			this.PacifyEat = base.Add(new Urge("PacifyEat"));
			this.PacifySleep = base.Add(new Urge("PacifySleep"));
			this.PacifyIdle = base.Add(new Urge("PacifyIdle"));
			this.EmoteHighPriority = base.Add(new Urge("EmoteHighPriority"));
			this.RecoverBreath = base.Add(new Urge("RecoverBreath"));
			this.RecoverWarmth = base.Add(new Urge("RecoverWarmth"));
			this.Aggression = base.Add(new Urge("Aggression"));
			this.MoveToQuarantine = base.Add(new Urge("MoveToQuarantine"));
			this.WashHands = base.Add(new Urge("WashHands"));
			this.Shower = base.Add(new Urge("Shower"));
			this.Eat = base.Add(new Urge("Eat"));
			this.ReloadElectrobank = base.Add(new Urge("ReloadElectrobank"));
			this.Pee = base.Add(new Urge("Pee"));
			this.RestDueToDisease = base.Add(new Urge("RestDueToDisease"));
			this.Sleep = base.Add(new Urge("Sleep"));
			this.Narcolepsy = base.Add(new Urge("Narcolepsy"));
			this.Doctor = base.Add(new Urge("Doctor"));
			this.Heal = base.Add(new Urge("Heal"));
			this.Feed = base.Add(new Urge("Feed"));
			this.PacifyRelocate = base.Add(new Urge("PacifyRelocate"));
			this.Emote = base.Add(new Urge("Emote"));
			this.MoveToSafety = base.Add(new Urge("MoveToSafety"));
			this.WarmUp = base.Add(new Urge("WarmUp"));
			this.CoolDown = base.Add(new Urge("CoolDown"));
			this.LearnSkill = base.Add(new Urge("LearnSkill"));
			this.EmoteIdle = base.Add(new Urge("EmoteIdle"));
			this.OilRefill = base.Add(new Urge("OilRefill"));
			this.GunkPee = base.Add(new Urge("GunkPee"));
			this.FindOxygenRefill = base.Add(new Urge("FindOxygenRefill"));
			this.Fart = base.Add(new Urge("Fart"));
		}

		// Token: 0x04005979 RID: 22905
		public Urge BeIncapacitated;

		// Token: 0x0400597A RID: 22906
		public Urge BeOffline;

		// Token: 0x0400597B RID: 22907
		public Urge Sleep;

		// Token: 0x0400597C RID: 22908
		public Urge Narcolepsy;

		// Token: 0x0400597D RID: 22909
		public Urge Eat;

		// Token: 0x0400597E RID: 22910
		public Urge ReloadElectrobank;

		// Token: 0x0400597F RID: 22911
		public Urge WashHands;

		// Token: 0x04005980 RID: 22912
		public Urge Shower;

		// Token: 0x04005981 RID: 22913
		public Urge Pee;

		// Token: 0x04005982 RID: 22914
		public Urge MoveToQuarantine;

		// Token: 0x04005983 RID: 22915
		public Urge HealCritical;

		// Token: 0x04005984 RID: 22916
		public Urge RecoverBreath;

		// Token: 0x04005985 RID: 22917
		public Urge FindOxygenRefill;

		// Token: 0x04005986 RID: 22918
		public Urge RecoverWarmth;

		// Token: 0x04005987 RID: 22919
		public Urge Emote;

		// Token: 0x04005988 RID: 22920
		public Urge Feed;

		// Token: 0x04005989 RID: 22921
		public Urge Doctor;

		// Token: 0x0400598A RID: 22922
		public Urge Flee;

		// Token: 0x0400598B RID: 22923
		public Urge Heal;

		// Token: 0x0400598C RID: 22924
		public Urge PacifyIdle;

		// Token: 0x0400598D RID: 22925
		public Urge PacifyEat;

		// Token: 0x0400598E RID: 22926
		public Urge PacifySleep;

		// Token: 0x0400598F RID: 22927
		public Urge PacifyRelocate;

		// Token: 0x04005990 RID: 22928
		public Urge RestDueToDisease;

		// Token: 0x04005991 RID: 22929
		public Urge EmoteHighPriority;

		// Token: 0x04005992 RID: 22930
		public Urge Aggression;

		// Token: 0x04005993 RID: 22931
		public Urge MoveToSafety;

		// Token: 0x04005994 RID: 22932
		public Urge WarmUp;

		// Token: 0x04005995 RID: 22933
		public Urge CoolDown;

		// Token: 0x04005996 RID: 22934
		public Urge LearnSkill;

		// Token: 0x04005997 RID: 22935
		public Urge EmoteIdle;

		// Token: 0x04005998 RID: 22936
		public Urge OilRefill;

		// Token: 0x04005999 RID: 22937
		public Urge GunkPee;

		// Token: 0x0400599A RID: 22938
		public Urge Fart;
	}
}
