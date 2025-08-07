using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000F4B RID: 3915
	public class CreaturePoopKGProduction : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x06007B00 RID: 31488 RVA: 0x0030A759 File Offset: 0x00308959
		public CreaturePoopKGProduction(Tag poopElement, float amountToPoop)
		{
			this.poopElement = poopElement;
			this.amountToPoop = amountToPoop;
		}

		// Token: 0x06007B01 RID: 31489 RVA: 0x0030A770 File Offset: 0x00308970
		public override bool Success()
		{
			return Game.Instance.savedInfo.creaturePoopAmount.ContainsKey(this.poopElement) && Game.Instance.savedInfo.creaturePoopAmount[this.poopElement] >= this.amountToPoop;
		}

		// Token: 0x06007B02 RID: 31490 RVA: 0x0030A7C0 File Offset: 0x003089C0
		public void Deserialize(IReader reader)
		{
			this.amountToPoop = reader.ReadSingle();
			string text = reader.ReadKleiString();
			this.poopElement = new Tag(text);
		}

		// Token: 0x06007B03 RID: 31491 RVA: 0x0030A7EC File Offset: 0x003089EC
		public override string GetProgress(bool complete)
		{
			float num = 0f;
			Game.Instance.savedInfo.creaturePoopAmount.TryGetValue(this.poopElement, out num);
			return string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.POOP_PRODUCTION, GameUtil.GetFormattedMass(complete ? this.amountToPoop : num, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.Tonne, true, "{0:0.#}"), GameUtil.GetFormattedMass(this.amountToPoop, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.Tonne, true, "{0:0.#}"));
		}

		// Token: 0x040059D0 RID: 22992
		private Tag poopElement;

		// Token: 0x040059D1 RID: 22993
		private float amountToPoop;
	}
}
