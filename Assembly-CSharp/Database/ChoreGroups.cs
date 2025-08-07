using System;
using Klei.AI;
using STRINGS;

namespace Database
{
	// Token: 0x02000EE2 RID: 3810
	public class ChoreGroups : ResourceSet<ChoreGroup>
	{
		// Token: 0x06007958 RID: 31064 RVA: 0x002F434C File Offset: 0x002F254C
		private ChoreGroup Add(string id, string name, Klei.AI.Attribute attribute, string sprite, int default_personal_priority, bool user_prioritizable = true)
		{
			ChoreGroup choreGroup = new ChoreGroup(id, name, attribute, sprite, default_personal_priority, user_prioritizable);
			base.Add(choreGroup);
			return choreGroup;
		}

		// Token: 0x06007959 RID: 31065 RVA: 0x002F4374 File Offset: 0x002F2574
		public ChoreGroups(ResourceSet parent)
			: base("ChoreGroups", parent)
		{
			this.Combat = this.Add("Combat", DUPLICANTS.CHOREGROUPS.COMBAT.NAME, Db.Get().Attributes.Digging, "icon_errand_combat", 5, true);
			this.LifeSupport = this.Add("LifeSupport", DUPLICANTS.CHOREGROUPS.LIFESUPPORT.NAME, Db.Get().Attributes.LifeSupport, "icon_errand_life_support", 5, true);
			this.Toggle = this.Add("Toggle", DUPLICANTS.CHOREGROUPS.TOGGLE.NAME, Db.Get().Attributes.Toggle, "icon_errand_toggle", 5, true);
			this.MedicalAid = this.Add("MedicalAid", DUPLICANTS.CHOREGROUPS.MEDICALAID.NAME, Db.Get().Attributes.Caring, "icon_errand_care", 4, true);
			if (DlcManager.FeatureClusterSpaceEnabled())
			{
				this.Rocketry = this.Add("Rocketry", DUPLICANTS.CHOREGROUPS.ROCKETRY.NAME, Db.Get().Attributes.SpaceNavigation, "icon_errand_rocketry", 4, true);
			}
			this.Basekeeping = this.Add("Basekeeping", DUPLICANTS.CHOREGROUPS.BASEKEEPING.NAME, Db.Get().Attributes.Strength, "icon_errand_tidy", 4, true);
			this.Cook = this.Add("Cook", DUPLICANTS.CHOREGROUPS.COOK.NAME, Db.Get().Attributes.Cooking, "icon_errand_cook", 3, true);
			this.Art = this.Add("Art", DUPLICANTS.CHOREGROUPS.ART.NAME, Db.Get().Attributes.Art, "icon_errand_art", 3, true);
			this.Research = this.Add("Research", DUPLICANTS.CHOREGROUPS.RESEARCH.NAME, Db.Get().Attributes.Learning, "icon_errand_research", 3, true);
			this.MachineOperating = this.Add("MachineOperating", DUPLICANTS.CHOREGROUPS.MACHINEOPERATING.NAME, Db.Get().Attributes.Machinery, "icon_errand_operate", 3, true);
			this.Farming = this.Add("Farming", DUPLICANTS.CHOREGROUPS.FARMING.NAME, Db.Get().Attributes.Botanist, "icon_errand_farm", 3, true);
			this.Ranching = this.Add("Ranching", DUPLICANTS.CHOREGROUPS.RANCHING.NAME, Db.Get().Attributes.Ranching, "icon_errand_ranch", 3, true);
			this.Build = this.Add("Build", DUPLICANTS.CHOREGROUPS.BUILD.NAME, Db.Get().Attributes.Construction, "icon_errand_toggle", 2, true);
			this.Dig = this.Add("Dig", DUPLICANTS.CHOREGROUPS.DIG.NAME, Db.Get().Attributes.Digging, "icon_errand_dig", 2, true);
			this.Hauling = this.Add("Hauling", DUPLICANTS.CHOREGROUPS.HAULING.NAME, Db.Get().Attributes.Strength, "icon_errand_supply", 1, true);
			this.Storage = this.Add("Storage", DUPLICANTS.CHOREGROUPS.STORAGE.NAME, Db.Get().Attributes.Strength, "icon_errand_storage", 1, true);
			this.Recreation = this.Add("Recreation", DUPLICANTS.CHOREGROUPS.RECREATION.NAME, Db.Get().Attributes.Strength, "icon_errand_storage", 1, false);
			Debug.Assert(true);
		}

		// Token: 0x0600795A RID: 31066 RVA: 0x002F46DC File Offset: 0x002F28DC
		public ChoreGroup FindByHash(HashedString id)
		{
			ChoreGroup choreGroup = null;
			foreach (ChoreGroup choreGroup2 in Db.Get().ChoreGroups.resources)
			{
				if (choreGroup2.IdHash == id)
				{
					choreGroup = choreGroup2;
					break;
				}
			}
			return choreGroup;
		}

		// Token: 0x040055D0 RID: 21968
		public ChoreGroup Build;

		// Token: 0x040055D1 RID: 21969
		public ChoreGroup Basekeeping;

		// Token: 0x040055D2 RID: 21970
		public ChoreGroup Cook;

		// Token: 0x040055D3 RID: 21971
		public ChoreGroup Art;

		// Token: 0x040055D4 RID: 21972
		public ChoreGroup Dig;

		// Token: 0x040055D5 RID: 21973
		public ChoreGroup Research;

		// Token: 0x040055D6 RID: 21974
		public ChoreGroup Farming;

		// Token: 0x040055D7 RID: 21975
		public ChoreGroup Ranching;

		// Token: 0x040055D8 RID: 21976
		public ChoreGroup Hauling;

		// Token: 0x040055D9 RID: 21977
		public ChoreGroup Storage;

		// Token: 0x040055DA RID: 21978
		public ChoreGroup MachineOperating;

		// Token: 0x040055DB RID: 21979
		public ChoreGroup MedicalAid;

		// Token: 0x040055DC RID: 21980
		public ChoreGroup Combat;

		// Token: 0x040055DD RID: 21981
		public ChoreGroup LifeSupport;

		// Token: 0x040055DE RID: 21982
		public ChoreGroup Toggle;

		// Token: 0x040055DF RID: 21983
		public ChoreGroup Recreation;

		// Token: 0x040055E0 RID: 21984
		public ChoreGroup Rocketry;
	}
}
