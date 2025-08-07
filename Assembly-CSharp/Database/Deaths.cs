using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000EEA RID: 3818
	public class Deaths : ResourceSet<Death>
	{
		// Token: 0x06007974 RID: 31092 RVA: 0x002F96EC File Offset: 0x002F78EC
		public Deaths(ResourceSet parent)
			: base("Deaths", parent)
		{
			this.Generic = new Death("Generic", this, DUPLICANTS.DEATHS.GENERIC.NAME, DUPLICANTS.DEATHS.GENERIC.DESCRIPTION, "dead_on_back", "dead_on_back");
			this.Frozen = new Death("Frozen", this, DUPLICANTS.DEATHS.FROZEN.NAME, DUPLICANTS.DEATHS.FROZEN.DESCRIPTION, "death_freeze_trans", "death_freeze_solid");
			this.Suffocation = new Death("Suffocation", this, DUPLICANTS.DEATHS.SUFFOCATION.NAME, DUPLICANTS.DEATHS.SUFFOCATION.DESCRIPTION, "death_suffocation", "dead_on_back");
			this.Starvation = new Death("Starvation", this, DUPLICANTS.DEATHS.STARVATION.NAME, DUPLICANTS.DEATHS.STARVATION.DESCRIPTION, "dead_on_back", "dead_on_back");
			this.Overheating = new Death("Overheating", this, DUPLICANTS.DEATHS.OVERHEATING.NAME, DUPLICANTS.DEATHS.OVERHEATING.DESCRIPTION, "dead_on_back", "dead_on_back");
			this.Drowned = new Death("Drowned", this, DUPLICANTS.DEATHS.DROWNED.NAME, DUPLICANTS.DEATHS.DROWNED.DESCRIPTION, "death_suffocation", "dead_on_back");
			this.Explosion = new Death("Explosion", this, DUPLICANTS.DEATHS.EXPLOSION.NAME, DUPLICANTS.DEATHS.EXPLOSION.DESCRIPTION, "dead_on_back", "dead_on_back");
			this.Slain = new Death("Combat", this, DUPLICANTS.DEATHS.COMBAT.NAME, DUPLICANTS.DEATHS.COMBAT.DESCRIPTION, "dead_on_back", "dead_on_back");
			this.FatalDisease = new Death("FatalDisease", this, DUPLICANTS.DEATHS.FATALDISEASE.NAME, DUPLICANTS.DEATHS.FATALDISEASE.DESCRIPTION, "dead_on_back", "dead_on_back");
			this.Radiation = new Death("Radiation", this, DUPLICANTS.DEATHS.RADIATION.NAME, DUPLICANTS.DEATHS.RADIATION.DESCRIPTION, "dead_on_back", "dead_on_back");
			this.HitByHighEnergyParticle = new Death("HitByHighEnergyParticle", this, DUPLICANTS.DEATHS.HITBYHIGHENERGYPARTICLE.NAME, DUPLICANTS.DEATHS.HITBYHIGHENERGYPARTICLE.DESCRIPTION, "dead_on_back", "dead_on_back");
			this.DeadBattery = new Death("DeadBattery", this, DUPLICANTS.DEATHS.HITBYHIGHENERGYPARTICLE.NAME, DUPLICANTS.DEATHS.HITBYHIGHENERGYPARTICLE.DESCRIPTION, "dead_on_back", "dead_on_back");
		}

		// Token: 0x040056D6 RID: 22230
		public Death Generic;

		// Token: 0x040056D7 RID: 22231
		public Death Frozen;

		// Token: 0x040056D8 RID: 22232
		public Death Suffocation;

		// Token: 0x040056D9 RID: 22233
		public Death Starvation;

		// Token: 0x040056DA RID: 22234
		public Death Slain;

		// Token: 0x040056DB RID: 22235
		public Death Overheating;

		// Token: 0x040056DC RID: 22236
		public Death Drowned;

		// Token: 0x040056DD RID: 22237
		public Death Explosion;

		// Token: 0x040056DE RID: 22238
		public Death FatalDisease;

		// Token: 0x040056DF RID: 22239
		public Death Radiation;

		// Token: 0x040056E0 RID: 22240
		public Death HitByHighEnergyParticle;

		// Token: 0x040056E1 RID: 22241
		public Death DeadBattery;

		// Token: 0x040056E2 RID: 22242
		public Death DeadCyborgChargeExpired;
	}
}
