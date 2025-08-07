using System;

namespace TUNING
{
	// Token: 0x02000F8B RID: 3979
	public class MEDICINE
	{
		// Token: 0x04005C10 RID: 23568
		public const float DEFAULT_MASS = 1f;

		// Token: 0x04005C11 RID: 23569
		public const float RECUPERATION_DISEASE_MULTIPLIER = 1.1f;

		// Token: 0x04005C12 RID: 23570
		public const float RECUPERATION_DOCTORED_DISEASE_MULTIPLIER = 1.2f;

		// Token: 0x04005C13 RID: 23571
		public const float WORK_TIME = 10f;

		// Token: 0x04005C14 RID: 23572
		public static readonly MedicineInfo BASICBOOSTER = new MedicineInfo("BasicBooster", "Medicine_BasicBooster", MedicineInfo.MedicineType.Booster, null, null);

		// Token: 0x04005C15 RID: 23573
		public static readonly MedicineInfo INTERMEDIATEBOOSTER = new MedicineInfo("IntermediateBooster", "Medicine_IntermediateBooster", MedicineInfo.MedicineType.Booster, null, null);

		// Token: 0x04005C16 RID: 23574
		public static readonly MedicineInfo BASICCURE = new MedicineInfo("BasicCure", null, MedicineInfo.MedicineType.CureSpecific, null, new string[] { "FoodSickness" });

		// Token: 0x04005C17 RID: 23575
		public static readonly MedicineInfo ANTIHISTAMINE = new MedicineInfo("Antihistamine", "HistamineSuppression", MedicineInfo.MedicineType.CureSpecific, null, new string[] { "Allergies" }, new string[] { "DupeMosquitoBite" });

		// Token: 0x04005C18 RID: 23576
		public static readonly MedicineInfo INTERMEDIATECURE = new MedicineInfo("IntermediateCure", null, MedicineInfo.MedicineType.CureSpecific, "DoctorStation", new string[] { "SlimeSickness" });

		// Token: 0x04005C19 RID: 23577
		public static readonly MedicineInfo ADVANCEDCURE = new MedicineInfo("AdvancedCure", null, MedicineInfo.MedicineType.CureSpecific, "AdvancedDoctorStation", new string[] { "ZombieSickness" });

		// Token: 0x04005C1A RID: 23578
		public static readonly MedicineInfo BASICRADPILL = new MedicineInfo("BasicRadPill", "Medicine_BasicRadPill", MedicineInfo.MedicineType.Booster, null, null);

		// Token: 0x04005C1B RID: 23579
		public static readonly MedicineInfo INTERMEDIATERADPILL = new MedicineInfo("IntermediateRadPill", "Medicine_IntermediateRadPill", MedicineInfo.MedicineType.Booster, "AdvancedDoctorStation", null);
	}
}
