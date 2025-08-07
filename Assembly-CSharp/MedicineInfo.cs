using System;
using System.Collections.Generic;

// Token: 0x020009C2 RID: 2498
[Serializable]
public class MedicineInfo
{
	// Token: 0x060048EE RID: 18670 RVA: 0x001A4847 File Offset: 0x001A2A47
	public MedicineInfo(string id, string effect, MedicineInfo.MedicineType medicineType, string doctorStationId, string[] curedDiseases = null)
		: this(id, effect, medicineType, doctorStationId, curedDiseases, null)
	{
	}

	// Token: 0x060048EF RID: 18671 RVA: 0x001A4858 File Offset: 0x001A2A58
	public MedicineInfo(string id, string effect, MedicineInfo.MedicineType medicineType, string doctorStationId, string[] curedDiseases, string[] curedEffects)
	{
		Debug.Assert(!string.IsNullOrEmpty(effect) || (curedDiseases != null && curedDiseases.Length != 0), "Medicine should have an effect or cure diseases");
		this.id = id;
		this.effect = effect;
		this.medicineType = medicineType;
		this.doctorStationId = doctorStationId;
		if (curedDiseases != null)
		{
			this.curedSicknesses = new List<string>(curedDiseases);
		}
		else
		{
			this.curedSicknesses = new List<string>();
		}
		if (curedEffects != null)
		{
			this.curedEffects = new List<string>(curedEffects);
			return;
		}
		this.curedEffects = new List<string>();
	}

	// Token: 0x060048F0 RID: 18672 RVA: 0x001A48E5 File Offset: 0x001A2AE5
	public Tag GetSupplyTag()
	{
		return MedicineInfo.GetSupplyTagForStation(this.doctorStationId);
	}

	// Token: 0x060048F1 RID: 18673 RVA: 0x001A48F4 File Offset: 0x001A2AF4
	public static Tag GetSupplyTagForStation(string stationID)
	{
		Tag tag = TagManager.Create(stationID + GameTags.MedicalSupplies.Name);
		Assets.AddCountableTag(tag);
		return tag;
	}

	// Token: 0x0400300A RID: 12298
	public string id;

	// Token: 0x0400300B RID: 12299
	public string effect;

	// Token: 0x0400300C RID: 12300
	public MedicineInfo.MedicineType medicineType;

	// Token: 0x0400300D RID: 12301
	public List<string> curedSicknesses;

	// Token: 0x0400300E RID: 12302
	public List<string> curedEffects;

	// Token: 0x0400300F RID: 12303
	public string doctorStationId;

	// Token: 0x020019D0 RID: 6608
	public enum MedicineType
	{
		// Token: 0x04007DC3 RID: 32195
		Booster,
		// Token: 0x04007DC4 RID: 32196
		CureAny,
		// Token: 0x04007DC5 RID: 32197
		CureSpecific
	}
}
