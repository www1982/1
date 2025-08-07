using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000F19 RID: 3865
	public class Thoughts : ResourceSet<Thought>
	{
		// Token: 0x06007A20 RID: 31264 RVA: 0x00307474 File Offset: 0x00305674
		public Thoughts(ResourceSet parent)
			: base("Thoughts", parent)
		{
			this.GotInfected = new Thought("GotInfected", this, "crew_state_sick", null, "crew_state_sick", "bubble_alert", SpeechMonitor.PREFIX_SAD, DUPLICANTS.THOUGHTS.GOTINFECTED.TOOLTIP, false, 4f);
			this.Starving = new Thought("Starving", this, "crew_state_hungry", null, "crew_state_hungry", "bubble_alert", SpeechMonitor.PREFIX_SAD, DUPLICANTS.THOUGHTS.STARVING.TOOLTIP, false, 4f);
			this.Hot = new Thought("Hot", this, "crew_state_temp_up", null, "crew_state_temp_up", "bubble_alert", SpeechMonitor.PREFIX_SAD, DUPLICANTS.THOUGHTS.HOT.TOOLTIP, false, 4f);
			this.Cold = new Thought("Cold", this, "crew_state_temp_down", null, "crew_state_temp_down", "bubble_alert", SpeechMonitor.PREFIX_SAD, DUPLICANTS.THOUGHTS.COLD.TOOLTIP, false, 4f);
			this.BreakBladder = new Thought("BreakBladder", this, "crew_state_full_bladder", null, "crew_state_full_bladder", "bubble_conversation", SpeechMonitor.PREFIX_SAD, DUPLICANTS.THOUGHTS.BREAKBLADDER.TOOLTIP, false, 4f);
			this.FullBladder = new Thought("FullBladder", this, "crew_state_full_bladder", null, "crew_state_full_bladder", "bubble_alert", SpeechMonitor.PREFIX_SAD, DUPLICANTS.THOUGHTS.FULLBLADDER.TOOLTIP, false, 4f);
			this.ExpellingGunk = new Thought("ExpellingGunk", this, "crew_state_gunk_dump_desire", null, "crew_state_gunk_dump_desire", "bubble_alert", SpeechMonitor.PREFIX_SAD, DUPLICANTS.THOUGHTS.EXPELLINGSPOILEDOIL.TOOLTIP, false, 4f);
			this.ExpellGunkDesire = new Thought("ExpellGunkDesire", this, "crew_state_gunk_dump_desire", null, "crew_state_gunk_dump_desire", "bubble_alert", SpeechMonitor.PREFIX_SAD, DUPLICANTS.THOUGHTS.EXPELLGUNKDESIRE.TOOLTIP, false, 4f);
			this.RefillOilDesire = new Thought("RefillOilDesire", this, "crew_state_oil_change_desire", null, "crew_state_oil_change_desire", "bubble_alert", SpeechMonitor.PREFIX_SAD, DUPLICANTS.THOUGHTS.REFILLOILDESIRE.TOOLTIP, false, 4f);
			this.PoorDecor = new Thought("PoorDecor", this, "crew_state_decor", null, "crew_state_decor", "bubble_alert", SpeechMonitor.PREFIX_SAD, DUPLICANTS.THOUGHTS.POORDECOR.TOOLTIP, false, 4f);
			this.PoorFoodQuality = new Thought("PoorFoodQuality", this, "crew_state_yuck", null, "crew_state_yuck", "bubble_alert", SpeechMonitor.PREFIX_SAD, DUPLICANTS.THOUGHTS.POOR_FOOD_QUALITY.TOOLTIP, false, 4f);
			this.GoodFoodQuality = new Thought("GoodFoodQuality", this, "crew_state_happy", null, "crew_state_happy", "bubble_alert", SpeechMonitor.PREFIX_SAD, DUPLICANTS.THOUGHTS.GOOD_FOOD_QUALITY.TOOLTIP, false, 4f);
			this.Happy = new Thought("Happy", this, "crew_state_happy", null, "crew_state_happy", "bubble_alert", SpeechMonitor.PREFIX_SAD, DUPLICANTS.THOUGHTS.HAPPY.TOOLTIP, false, 4f);
			this.Unhappy = new Thought("Unhappy", this, "crew_state_unhappy", null, "crew_state_unhappy", "bubble_alert", SpeechMonitor.PREFIX_SAD, DUPLICANTS.THOUGHTS.UNHAPPY.TOOLTIP, false, 4f);
			this.Sleepy = new Thought("Sleepy", this, "crew_state_sleepy", null, "crew_state_sleepy", "bubble_conversation", SpeechMonitor.PREFIX_SAD, DUPLICANTS.THOUGHTS.SLEEPY.TOOLTIP, false, 4f);
			this.Suffocating = new Thought("Suffocating", this, "crew_state_cantbreathe", null, "crew_state_cantbreathe", "bubble_alert", SpeechMonitor.PREFIX_SAD, DUPLICANTS.THOUGHTS.SUFFOCATING.TOOLTIP, false, 4f);
			this.Angry = new Thought("Angry", this, "crew_state_angry", null, "crew_state_angry", "bubble_alert", SpeechMonitor.PREFIX_SAD, DUPLICANTS.THOUGHTS.ANGRY.TOOLTIP, false, 4f);
			this.Raging = new Thought("Enraged", this, "crew_state_enraged", null, "crew_state_enraged", "bubble_alert", SpeechMonitor.PREFIX_SAD, DUPLICANTS.THOUGHTS.RAGING.TOOLTIP, false, 4f);
			this.PutridOdour = new Thought("PutridOdour", this, "crew_state_smelled_putrid_odour", null, "crew_state_smelled_putrid_odour", "bubble_alert", SpeechMonitor.PREFIX_SAD, DUPLICANTS.THOUGHTS.PUTRIDODOUR.TOOLTIP, true, 4f);
			this.Noisy = new Thought("Noisy", this, "crew_state_noisey", null, "crew_state_noisey", "bubble_alert", SpeechMonitor.PREFIX_SAD, DUPLICANTS.THOUGHTS.NOISY.TOOLTIP, true, 4f);
			this.NewRole = new Thought("NewRole", this, "crew_state_role", null, "crew_state_role", "bubble_alert", SpeechMonitor.PREFIX_SAD, DUPLICANTS.THOUGHTS.NEWROLE.TOOLTIP, false, 4f);
			this.Encourage = new Thought("Encourage", this, "crew_state_encourage", null, "crew_state_happy", "bubble_conversation", SpeechMonitor.PREFIX_HAPPY, DUPLICANTS.THOUGHTS.ENCOURAGE.TOOLTIP, false, 4f);
			this.Chatty = new Thought("Chatty", this, "crew_state_chatty", null, "conversation_short", "bubble_conversation", SpeechMonitor.PREFIX_HAPPY, DUPLICANTS.THOUGHTS.CHATTY.TOOLTIP, false, 4f);
			this.CatchyTune = new Thought("CatchyTune", this, "crew_state_music", null, "crew_state_music", "bubble_conversation", SpeechMonitor.PREFIX_SINGER, DUPLICANTS.THOUGHTS.CATCHYTUNE.TOOLTIP, true, 4f);
			this.Dreaming = new Thought("Dreaming", this, "pajamas", null, null, "bubble_dream", null, DUPLICANTS.THOUGHTS.DREAMY.TOOLTIP, false, 4f);
			for (int i = this.Count - 1; i >= 0; i--)
			{
				this.resources[i].priority = 100 * (this.Count - i);
			}
		}

		// Token: 0x04005960 RID: 22880
		public Thought Starving;

		// Token: 0x04005961 RID: 22881
		public Thought Hot;

		// Token: 0x04005962 RID: 22882
		public Thought Cold;

		// Token: 0x04005963 RID: 22883
		public Thought BreakBladder;

		// Token: 0x04005964 RID: 22884
		public Thought FullBladder;

		// Token: 0x04005965 RID: 22885
		public Thought ExpellGunkDesire;

		// Token: 0x04005966 RID: 22886
		public Thought ExpellingGunk;

		// Token: 0x04005967 RID: 22887
		public Thought RefillOilDesire;

		// Token: 0x04005968 RID: 22888
		public Thought Happy;

		// Token: 0x04005969 RID: 22889
		public Thought Unhappy;

		// Token: 0x0400596A RID: 22890
		public Thought PoorDecor;

		// Token: 0x0400596B RID: 22891
		public Thought PoorFoodQuality;

		// Token: 0x0400596C RID: 22892
		public Thought GoodFoodQuality;

		// Token: 0x0400596D RID: 22893
		public Thought Sleepy;

		// Token: 0x0400596E RID: 22894
		public Thought Suffocating;

		// Token: 0x0400596F RID: 22895
		public Thought Angry;

		// Token: 0x04005970 RID: 22896
		public Thought Raging;

		// Token: 0x04005971 RID: 22897
		public Thought GotInfected;

		// Token: 0x04005972 RID: 22898
		public Thought PutridOdour;

		// Token: 0x04005973 RID: 22899
		public Thought Noisy;

		// Token: 0x04005974 RID: 22900
		public Thought NewRole;

		// Token: 0x04005975 RID: 22901
		public Thought Chatty;

		// Token: 0x04005976 RID: 22902
		public Thought Encourage;

		// Token: 0x04005977 RID: 22903
		public Thought CatchyTune;

		// Token: 0x04005978 RID: 22904
		public Thought Dreaming;
	}
}
