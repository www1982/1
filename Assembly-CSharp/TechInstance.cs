using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000AC6 RID: 2758
public class TechInstance
{
	// Token: 0x06005022 RID: 20514 RVA: 0x001D0112 File Offset: 0x001CE312
	public TechInstance(Tech tech)
	{
		this.tech = tech;
	}

	// Token: 0x06005023 RID: 20515 RVA: 0x001D0137 File Offset: 0x001CE337
	public bool IsComplete()
	{
		return this.complete;
	}

	// Token: 0x06005024 RID: 20516 RVA: 0x001D013F File Offset: 0x001CE33F
	public void Purchased()
	{
		if (!this.complete)
		{
			this.complete = true;
		}
	}

	// Token: 0x06005025 RID: 20517 RVA: 0x001D0150 File Offset: 0x001CE350
	public void UnlockPOITech(string tech_id)
	{
		TechItem techItem = Db.Get().TechItems.Get(tech_id);
		if (techItem == null || !techItem.isPOIUnlock)
		{
			return;
		}
		if (!this.UnlockedPOITechIds.Contains(tech_id))
		{
			this.UnlockedPOITechIds.Add(tech_id);
			BuildingDef buildingDef = Assets.GetBuildingDef(techItem.Id);
			if (buildingDef != null)
			{
				Game.Instance.Trigger(-107300940, buildingDef);
			}
		}
	}

	// Token: 0x06005026 RID: 20518 RVA: 0x001D01BC File Offset: 0x001CE3BC
	public float GetTotalPercentageComplete()
	{
		float num = 0f;
		int num2 = 0;
		foreach (string text in this.progressInventory.PointsByTypeID.Keys)
		{
			if (this.tech.RequiresResearchType(text))
			{
				num += this.PercentageCompleteResearchType(text);
				num2++;
			}
		}
		return num / (float)num2;
	}

	// Token: 0x06005027 RID: 20519 RVA: 0x001D023C File Offset: 0x001CE43C
	public float PercentageCompleteResearchType(string type)
	{
		if (!this.tech.RequiresResearchType(type))
		{
			return 1f;
		}
		return Mathf.Clamp01(this.progressInventory.PointsByTypeID[type] / this.tech.costsByResearchTypeID[type]);
	}

	// Token: 0x06005028 RID: 20520 RVA: 0x001D027C File Offset: 0x001CE47C
	public TechInstance.SaveData Save()
	{
		string[] array = new string[this.progressInventory.PointsByTypeID.Count];
		this.progressInventory.PointsByTypeID.Keys.CopyTo(array, 0);
		float[] array2 = new float[this.progressInventory.PointsByTypeID.Count];
		this.progressInventory.PointsByTypeID.Values.CopyTo(array2, 0);
		string[] array3 = this.UnlockedPOITechIds.ToArray();
		return new TechInstance.SaveData
		{
			techId = this.tech.Id,
			complete = this.complete,
			inventoryIDs = array,
			inventoryValues = array2,
			unlockedPOIIDs = array3
		};
	}

	// Token: 0x06005029 RID: 20521 RVA: 0x001D0330 File Offset: 0x001CE530
	public void Load(TechInstance.SaveData save_data)
	{
		this.complete = save_data.complete;
		for (int i = 0; i < save_data.inventoryIDs.Length; i++)
		{
			this.progressInventory.AddResearchPoints(save_data.inventoryIDs[i], save_data.inventoryValues[i]);
		}
		if (save_data.unlockedPOIIDs != null)
		{
			this.UnlockedPOITechIds = new List<string>(save_data.unlockedPOIIDs);
		}
	}

	// Token: 0x040035F5 RID: 13813
	public Tech tech;

	// Token: 0x040035F6 RID: 13814
	private bool complete;

	// Token: 0x040035F7 RID: 13815
	public ResearchPointInventory progressInventory = new ResearchPointInventory();

	// Token: 0x040035F8 RID: 13816
	public List<string> UnlockedPOITechIds = new List<string>();

	// Token: 0x02001B9E RID: 7070
	public struct SaveData
	{
		// Token: 0x0400836A RID: 33642
		public string techId;

		// Token: 0x0400836B RID: 33643
		public bool complete;

		// Token: 0x0400836C RID: 33644
		public string[] inventoryIDs;

		// Token: 0x0400836D RID: 33645
		public float[] inventoryValues;

		// Token: 0x0400836E RID: 33646
		public string[] unlockedPOIIDs;
	}
}
