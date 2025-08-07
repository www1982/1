using System;
using Klei.AI;
using UnityEngine;

// Token: 0x020009C4 RID: 2500
[AddComponentMenu("KMonoBehaviour/Workable/MedicinalPillWorkable")]
public class MedicinalPillWorkable : Workable, IConsumableUIItem
{
	// Token: 0x060048F6 RID: 18678 RVA: 0x001A4C14 File Offset: 0x001A2E14
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.SetWorkTime(10f);
		this.showProgressBar = false;
		this.synchronizeAnims = false;
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.Normal, null);
		this.CreateChore();
	}

	// Token: 0x060048F7 RID: 18679 RVA: 0x001A4C74 File Offset: 0x001A2E74
	protected override void OnCompleteWork(WorkerBase worker)
	{
		Effects component = worker.GetComponent<Effects>();
		if (!string.IsNullOrEmpty(this.pill.info.effect))
		{
			EffectInstance effectInstance = component.Get(this.pill.info.effect);
			if (effectInstance != null)
			{
				effectInstance.timeRemaining = effectInstance.effect.duration;
			}
			else
			{
				component.Add(this.pill.info.effect, true);
			}
		}
		Sicknesses sicknesses = worker.GetSicknesses();
		foreach (string text in this.pill.info.curedSicknesses)
		{
			SicknessInstance sicknessInstance = sicknesses.Get(text);
			if (sicknessInstance != null)
			{
				Game.Instance.savedInfo.curedDisease = true;
				sicknessInstance.Cure();
			}
		}
		foreach (string text2 in this.pill.info.curedEffects)
		{
			if (component.HasEffect(text2))
			{
				Game.Instance.savedInfo.curedDisease = true;
				component.Remove(text2);
			}
		}
		base.gameObject.DeleteObject();
	}

	// Token: 0x060048F8 RID: 18680 RVA: 0x001A4DD0 File Offset: 0x001A2FD0
	private void CreateChore()
	{
		new TakeMedicineChore(this);
	}

	// Token: 0x060048F9 RID: 18681 RVA: 0x001A4DDC File Offset: 0x001A2FDC
	public bool CanBeTakenBy(GameObject consumer)
	{
		if (!string.IsNullOrEmpty(this.pill.info.effect))
		{
			Effects component = consumer.GetComponent<Effects>();
			if (component == null || component.HasEffect(this.pill.info.effect))
			{
				return false;
			}
		}
		if (this.pill.info.medicineType == MedicineInfo.MedicineType.Booster)
		{
			return true;
		}
		Sicknesses sicknesses = consumer.GetSicknesses();
		if (this.pill.info.medicineType == MedicineInfo.MedicineType.CureAny && sicknesses.Count > 0)
		{
			return true;
		}
		foreach (SicknessInstance sicknessInstance in sicknesses)
		{
			if (this.pill.info.curedSicknesses.Contains(sicknessInstance.modifier.Id))
			{
				return true;
			}
		}
		consumer.GetComponent<Effects>();
		for (int i = 0; i < this.pill.info.curedEffects.Count; i++)
		{
			string text = this.pill.info.curedEffects[i];
			if (this.pill.info.curedEffects.Contains(text))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x1700051A RID: 1306
	// (get) Token: 0x060048FA RID: 18682 RVA: 0x001A4F24 File Offset: 0x001A3124
	public string ConsumableId
	{
		get
		{
			return this.PrefabID().Name;
		}
	}

	// Token: 0x1700051B RID: 1307
	// (get) Token: 0x060048FB RID: 18683 RVA: 0x001A4F3F File Offset: 0x001A313F
	public string ConsumableName
	{
		get
		{
			return this.GetProperName();
		}
	}

	// Token: 0x1700051C RID: 1308
	// (get) Token: 0x060048FC RID: 18684 RVA: 0x001A4F47 File Offset: 0x001A3147
	public int MajorOrder
	{
		get
		{
			return (int)(this.pill.info.medicineType + 1000);
		}
	}

	// Token: 0x1700051D RID: 1309
	// (get) Token: 0x060048FD RID: 18685 RVA: 0x001A4F5F File Offset: 0x001A315F
	public int MinorOrder
	{
		get
		{
			return 0;
		}
	}

	// Token: 0x1700051E RID: 1310
	// (get) Token: 0x060048FE RID: 18686 RVA: 0x001A4F62 File Offset: 0x001A3162
	public bool Display
	{
		get
		{
			return true;
		}
	}

	// Token: 0x04003011 RID: 12305
	public MedicinalPill pill;
}
