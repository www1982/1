using System;
using STRINGS;
using TUNING;

namespace Database
{
	// Token: 0x02000EF9 RID: 3833
	public class AssignableSlots : ResourceSet<AssignableSlot>
	{
		// Token: 0x060079AB RID: 31147 RVA: 0x002FF2F8 File Offset: 0x002FD4F8
		public AssignableSlots()
		{
			this.Bed = base.Add(new OwnableSlot("Bed", MISC.TAGS.BED));
			this.MessStation = base.Add(new OwnableSlot("MessStation", MISC.TAGS.MESSSTATION));
			this.Clinic = base.Add(new OwnableSlot("Clinic", MISC.TAGS.CLINIC));
			this.MedicalBed = base.Add(new OwnableSlot("MedicalBed", MISC.TAGS.CLINIC));
			this.MedicalBed.showInUI = false;
			this.GeneShuffler = base.Add(new OwnableSlot("GeneShuffler", MISC.TAGS.GENE_SHUFFLER));
			this.GeneShuffler.showInUI = false;
			this.Toilet = base.Add(new OwnableSlot("Toilet", MISC.TAGS.TOILET));
			this.MassageTable = base.Add(new OwnableSlot("MassageTable", MISC.TAGS.MASSAGE_TABLE));
			this.RocketCommandModule = base.Add(new OwnableSlot("RocketCommandModule", MISC.TAGS.COMMAND_MODULE));
			this.HabitatModule = base.Add(new OwnableSlot("HabitatModule", MISC.TAGS.HABITAT_MODULE));
			this.ResetSkillsStation = base.Add(new OwnableSlot("ResetSkillsStation", "ResetSkillsStation"));
			this.WarpPortal = base.Add(new OwnableSlot("WarpPortal", MISC.TAGS.WARP_PORTAL));
			this.WarpPortal.showInUI = false;
			this.BionicUpgrade = base.Add(new OwnableSlot("BionicUpgrade", MISC.TAGS.BIONICUPGRADE));
			this.Toy = base.Add(new EquipmentSlot(global::TUNING.EQUIPMENT.TOYS.SLOT, MISC.TAGS.TOY, false));
			this.Suit = base.Add(new EquipmentSlot(global::TUNING.EQUIPMENT.SUITS.SLOT, MISC.TAGS.SUIT, true));
			this.Tool = base.Add(new EquipmentSlot(global::TUNING.EQUIPMENT.TOOLS.TOOLSLOT, MISC.TAGS.MULTITOOL, false));
			this.Outfit = base.Add(new EquipmentSlot(global::TUNING.EQUIPMENT.CLOTHING.SLOT, UI.StripLinkFormatting(MISC.TAGS.CLOTHES), true));
		}

		// Token: 0x0400585A RID: 22618
		public AssignableSlot Bed;

		// Token: 0x0400585B RID: 22619
		public AssignableSlot MessStation;

		// Token: 0x0400585C RID: 22620
		public AssignableSlot Clinic;

		// Token: 0x0400585D RID: 22621
		public AssignableSlot GeneShuffler;

		// Token: 0x0400585E RID: 22622
		public AssignableSlot MedicalBed;

		// Token: 0x0400585F RID: 22623
		public AssignableSlot Toilet;

		// Token: 0x04005860 RID: 22624
		public AssignableSlot MassageTable;

		// Token: 0x04005861 RID: 22625
		public AssignableSlot RocketCommandModule;

		// Token: 0x04005862 RID: 22626
		public AssignableSlot HabitatModule;

		// Token: 0x04005863 RID: 22627
		public AssignableSlot ResetSkillsStation;

		// Token: 0x04005864 RID: 22628
		public AssignableSlot WarpPortal;

		// Token: 0x04005865 RID: 22629
		public AssignableSlot Toy;

		// Token: 0x04005866 RID: 22630
		public AssignableSlot Suit;

		// Token: 0x04005867 RID: 22631
		public AssignableSlot Tool;

		// Token: 0x04005868 RID: 22632
		public AssignableSlot Outfit;

		// Token: 0x04005869 RID: 22633
		public AssignableSlot BionicUpgrade;
	}
}
