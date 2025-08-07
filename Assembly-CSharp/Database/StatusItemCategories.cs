using System;

namespace Database
{
	// Token: 0x02000F11 RID: 3857
	public class StatusItemCategories : ResourceSet<StatusItemCategory>
	{
		// Token: 0x060079FE RID: 31230 RVA: 0x00303CCC File Offset: 0x00301ECC
		public StatusItemCategories(ResourceSet parent)
			: base("StatusItemCategories", parent)
		{
			this.Main = new StatusItemCategory("Main", this, "Main");
			this.Role = new StatusItemCategory("Role", this, "Role");
			this.Power = new StatusItemCategory("Power", this, "Power");
			this.Toilet = new StatusItemCategory("Toilet", this, "Toilet");
			this.Research = new StatusItemCategory("Research", this, "Research");
			this.Hitpoints = new StatusItemCategory("Hitpoints", this, "Hitpoints");
			this.Suffocation = new StatusItemCategory("Suffocation", this, "Suffocation");
			this.WoundEffects = new StatusItemCategory("WoundEffects", this, "WoundEffects");
			this.EntityReceptacle = new StatusItemCategory("EntityReceptacle", this, "EntityReceptacle");
			this.PreservationState = new StatusItemCategory("PreservationState", this, "PreservationState");
			this.PreservationTemperature = new StatusItemCategory("PreservationTemperature", this, "PreservationTemperature");
			this.PreservationAtmosphere = new StatusItemCategory("PreservationAtmosphere", this, "PreservationAtmosphere");
			this.ExhaustTemperature = new StatusItemCategory("ExhaustTemperature", this, "ExhaustTemperature");
			this.OperatingEnergy = new StatusItemCategory("OperatingEnergy", this, "OperatingEnergy");
			this.AccessControl = new StatusItemCategory("AccessControl", this, "AccessControl");
			this.RequiredRoom = new StatusItemCategory("RequiredRoom", this, "RequiredRoom");
			this.Yield = new StatusItemCategory("Yield", this, "Yield");
			this.Heat = new StatusItemCategory("Heat", this, "Heat");
			this.Stored = new StatusItemCategory("Stored", this, "Stored");
			this.Ownable = new StatusItemCategory("Ownable", this, "Ownable");
		}

		// Token: 0x04005924 RID: 22820
		public StatusItemCategory Main;

		// Token: 0x04005925 RID: 22821
		public StatusItemCategory Role;

		// Token: 0x04005926 RID: 22822
		public StatusItemCategory Power;

		// Token: 0x04005927 RID: 22823
		public StatusItemCategory Toilet;

		// Token: 0x04005928 RID: 22824
		public StatusItemCategory Research;

		// Token: 0x04005929 RID: 22825
		public StatusItemCategory Hitpoints;

		// Token: 0x0400592A RID: 22826
		public StatusItemCategory Suffocation;

		// Token: 0x0400592B RID: 22827
		public StatusItemCategory WoundEffects;

		// Token: 0x0400592C RID: 22828
		public StatusItemCategory EntityReceptacle;

		// Token: 0x0400592D RID: 22829
		public StatusItemCategory PreservationState;

		// Token: 0x0400592E RID: 22830
		public StatusItemCategory PreservationAtmosphere;

		// Token: 0x0400592F RID: 22831
		public StatusItemCategory PreservationTemperature;

		// Token: 0x04005930 RID: 22832
		public StatusItemCategory ExhaustTemperature;

		// Token: 0x04005931 RID: 22833
		public StatusItemCategory OperatingEnergy;

		// Token: 0x04005932 RID: 22834
		public StatusItemCategory AccessControl;

		// Token: 0x04005933 RID: 22835
		public StatusItemCategory RequiredRoom;

		// Token: 0x04005934 RID: 22836
		public StatusItemCategory Yield;

		// Token: 0x04005935 RID: 22837
		public StatusItemCategory Heat;

		// Token: 0x04005936 RID: 22838
		public StatusItemCategory Stored;

		// Token: 0x04005937 RID: 22839
		public StatusItemCategory Ownable;
	}
}
