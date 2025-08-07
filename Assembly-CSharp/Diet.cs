using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x0200058F RID: 1423
public class Diet
{
	// Token: 0x1700013F RID: 319
	// (get) Token: 0x0600207B RID: 8315 RVA: 0x000BB53C File Offset: 0x000B973C
	// (set) Token: 0x0600207C RID: 8316 RVA: 0x000BB544 File Offset: 0x000B9744
	public Diet.Info[] infos { get; private set; }

	// Token: 0x17000140 RID: 320
	// (get) Token: 0x0600207D RID: 8317 RVA: 0x000BB54D File Offset: 0x000B974D
	// (set) Token: 0x0600207E RID: 8318 RVA: 0x000BB555 File Offset: 0x000B9755
	public Diet.Info[] solidEdiblesInfo { get; private set; }

	// Token: 0x17000141 RID: 321
	// (get) Token: 0x0600207F RID: 8319 RVA: 0x000BB55E File Offset: 0x000B975E
	// (set) Token: 0x06002080 RID: 8320 RVA: 0x000BB566 File Offset: 0x000B9766
	public Diet.Info[] directlyEatenPlantInfos { get; private set; }

	// Token: 0x17000142 RID: 322
	// (get) Token: 0x06002081 RID: 8321 RVA: 0x000BB56F File Offset: 0x000B976F
	// (set) Token: 0x06002082 RID: 8322 RVA: 0x000BB577 File Offset: 0x000B9777
	public Diet.Info[] preyInfos { get; private set; }

	// Token: 0x17000143 RID: 323
	// (get) Token: 0x06002083 RID: 8323 RVA: 0x000BB580 File Offset: 0x000B9780
	public bool CanEatAnySolid
	{
		get
		{
			return this.solidEdiblesInfo != null && this.solidEdiblesInfo.Length != 0;
		}
	}

	// Token: 0x17000144 RID: 324
	// (get) Token: 0x06002084 RID: 8324 RVA: 0x000BB596 File Offset: 0x000B9796
	public bool CanEatAnyPlantDirectly
	{
		get
		{
			return this.directlyEatenPlantInfos != null && this.directlyEatenPlantInfos.Length != 0;
		}
	}

	// Token: 0x17000145 RID: 325
	// (get) Token: 0x06002085 RID: 8325 RVA: 0x000BB5AC File Offset: 0x000B97AC
	public bool CanEatPreyCritter
	{
		get
		{
			return this.preyInfos != null && this.preyInfos.Length != 0;
		}
	}

	// Token: 0x06002086 RID: 8326 RVA: 0x000BB5C4 File Offset: 0x000B97C4
	public bool IsConsumedTagAbleToBeEatenDirectly(Tag tag)
	{
		if (this.directlyEatenPlantInfos == null && this.preyInfos == null)
		{
			return false;
		}
		for (int i = 0; i < this.directlyEatenPlantInfos.Length; i++)
		{
			if (this.directlyEatenPlantInfos[i].consumedTags.Contains(tag))
			{
				return true;
			}
		}
		for (int j = 0; j < this.preyInfos.Length; j++)
		{
			if (this.preyInfos[j].consumedTags.Contains(tag))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06002087 RID: 8327 RVA: 0x000BB638 File Offset: 0x000B9838
	private void UpdateSecondaryInfoArrays()
	{
		Diet.Info[] array;
		if (this.infos != null)
		{
			array = this.infos.Where((Diet.Info i) => i.foodType == Diet.Info.FoodType.EatPlantDirectly || i.foodType == Diet.Info.FoodType.EatPlantStorage).ToArray<Diet.Info>();
		}
		else
		{
			array = null;
		}
		this.directlyEatenPlantInfos = array;
		Diet.Info[] array2;
		if (this.infos != null)
		{
			array2 = this.infos.Where((Diet.Info i) => i.foodType == Diet.Info.FoodType.EatSolid).ToArray<Diet.Info>();
		}
		else
		{
			array2 = null;
		}
		this.solidEdiblesInfo = array2;
		Diet.Info[] array3;
		if (this.infos != null)
		{
			array3 = this.infos.Where((Diet.Info i) => i.foodType == Diet.Info.FoodType.EatPrey || i.foodType == Diet.Info.FoodType.EatButcheredPrey).ToArray<Diet.Info>();
		}
		else
		{
			array3 = null;
		}
		this.preyInfos = array3;
	}

	// Token: 0x06002088 RID: 8328 RVA: 0x000BB708 File Offset: 0x000B9908
	public Diet(params Diet.Info[] infos)
	{
		this.infos = infos;
		this.consumedTags = new List<KeyValuePair<Tag, float>>();
		this.producedTags = new List<KeyValuePair<Tag, float>>();
		for (int i = 0; i < infos.Length; i++)
		{
			Diet.Info info = infos[i];
			using (HashSet<Tag>.Enumerator enumerator = info.consumedTags.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Tag tag = enumerator.Current;
					if (-1 == this.consumedTags.FindIndex((KeyValuePair<Tag, float> e) => e.Key == tag))
					{
						this.consumedTags.Add(new KeyValuePair<Tag, float>(tag, info.caloriesPerKg));
					}
					if (this.consumedTagToInfo.ContainsKey(tag))
					{
						string text = "Duplicate diet entry: ";
						Tag tag2 = tag;
						global::Debug.LogError(text + tag2.ToString());
					}
					this.consumedTagToInfo[tag] = info;
				}
			}
			if (info.producedElement != Tag.Invalid && -1 == this.producedTags.FindIndex((KeyValuePair<Tag, float> e) => e.Key == info.producedElement))
			{
				this.producedTags.Add(new KeyValuePair<Tag, float>(info.producedElement, info.producedConversionRate));
			}
		}
		this.UpdateSecondaryInfoArrays();
	}

	// Token: 0x06002089 RID: 8329 RVA: 0x000BB8AC File Offset: 0x000B9AAC
	public Diet(Diet diet)
	{
		this.infos = new Diet.Info[diet.infos.Length];
		for (int i = 0; i < diet.infos.Length; i++)
		{
			this.infos[i] = new Diet.Info(diet.infos[i]);
		}
		this.consumedTags = new List<KeyValuePair<Tag, float>>();
		this.producedTags = new List<KeyValuePair<Tag, float>>();
		Diet.Info[] infos = this.infos;
		for (int j = 0; j < infos.Length; j++)
		{
			Diet.Info info = infos[j];
			using (HashSet<Tag>.Enumerator enumerator = info.consumedTags.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Tag tag = enumerator.Current;
					if (-1 == this.consumedTags.FindIndex((KeyValuePair<Tag, float> e) => e.Key == tag))
					{
						this.consumedTags.Add(new KeyValuePair<Tag, float>(tag, info.caloriesPerKg));
					}
					if (this.consumedTagToInfo.ContainsKey(tag))
					{
						string text = "Duplicate diet entry: ";
						Tag tag2 = tag;
						global::Debug.LogError(text + tag2.ToString());
					}
					this.consumedTagToInfo[tag] = info;
				}
			}
			if (info.producedElement != Tag.Invalid && -1 == this.producedTags.FindIndex((KeyValuePair<Tag, float> e) => e.Key == info.producedElement))
			{
				this.producedTags.Add(new KeyValuePair<Tag, float>(info.producedElement, info.producedConversionRate));
			}
		}
		this.UpdateSecondaryInfoArrays();
	}

	// Token: 0x0600208A RID: 8330 RVA: 0x000BBA88 File Offset: 0x000B9C88
	public Diet.Info GetDietInfo(Tag tag)
	{
		Diet.Info info = null;
		this.consumedTagToInfo.TryGetValue(tag, out info);
		return info;
	}

	// Token: 0x0600208B RID: 8331 RVA: 0x000BBAA8 File Offset: 0x000B9CA8
	public float AvailableCaloriesInPrey(Tag tag)
	{
		Diet.Info dietInfo = this.GetDietInfo(tag);
		if (dietInfo == null)
		{
			return 0f;
		}
		GameObject prefab = Assets.GetPrefab(tag);
		if (dietInfo.foodType == Diet.Info.FoodType.EatPrey)
		{
			return prefab.GetComponent<PrimaryElement>().Mass * dietInfo.caloriesPerKg;
		}
		Butcherable component = prefab.GetComponent<Butcherable>();
		float num = 0f;
		if (component == null)
		{
			return 0f;
		}
		foreach (KeyValuePair<string, float> keyValuePair in component.drops)
		{
			Diet.Info dietInfo2 = this.GetDietInfo(new Tag(keyValuePair.Key));
			if (dietInfo2 != null)
			{
				num += keyValuePair.Value * dietInfo2.caloriesPerKg;
			}
		}
		return num;
	}

	// Token: 0x0600208C RID: 8332 RVA: 0x000BBB74 File Offset: 0x000B9D74
	public void FilterDLC()
	{
		foreach (Diet.Info info in this.infos)
		{
			List<Tag> list = new List<Tag>();
			foreach (Tag tag in info.consumedTags)
			{
				GameObject prefab = Assets.GetPrefab(tag);
				if (prefab == null || !Game.IsCorrectDlcActiveForCurrentSave(prefab.GetComponent<KPrefabID>()))
				{
					list.Add(tag);
				}
			}
			using (List<Tag>.Enumerator enumerator2 = list.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					Tag invalid_tag = enumerator2.Current;
					info.consumedTags.Remove(invalid_tag);
					this.consumedTags.RemoveAll((KeyValuePair<Tag, float> t) => t.Key == invalid_tag);
					this.consumedTagToInfo.Remove(invalid_tag);
				}
			}
			if (info.producedElement != Tag.Invalid)
			{
				GameObject prefab2 = Assets.GetPrefab(info.producedElement);
				if (prefab2 == null || !Game.IsCorrectDlcActiveForCurrentSave(prefab2.GetComponent<KPrefabID>()))
				{
					info.consumedTags.Clear();
				}
			}
		}
		this.infos = this.infos.Where((Diet.Info i) => i.consumedTags.Count > 0).ToArray<Diet.Info>();
		this.UpdateSecondaryInfoArrays();
	}

	// Token: 0x040012ED RID: 4845
	public List<KeyValuePair<Tag, float>> consumedTags;

	// Token: 0x040012EE RID: 4846
	public List<KeyValuePair<Tag, float>> producedTags;

	// Token: 0x040012EF RID: 4847
	private Dictionary<Tag, Diet.Info> consumedTagToInfo = new Dictionary<Tag, Diet.Info>();

	// Token: 0x020013E7 RID: 5095
	public class Info
	{
		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x06008BC8 RID: 35784 RVA: 0x00353BEE File Offset: 0x00351DEE
		// (set) Token: 0x06008BC9 RID: 35785 RVA: 0x00353BF6 File Offset: 0x00351DF6
		public HashSet<Tag> consumedTags { get; private set; }

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x06008BCA RID: 35786 RVA: 0x00353BFF File Offset: 0x00351DFF
		// (set) Token: 0x06008BCB RID: 35787 RVA: 0x00353C07 File Offset: 0x00351E07
		public Tag producedElement { get; private set; }

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x06008BCC RID: 35788 RVA: 0x00353C10 File Offset: 0x00351E10
		// (set) Token: 0x06008BCD RID: 35789 RVA: 0x00353C18 File Offset: 0x00351E18
		public float caloriesPerKg { get; private set; }

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x06008BCE RID: 35790 RVA: 0x00353C21 File Offset: 0x00351E21
		// (set) Token: 0x06008BCF RID: 35791 RVA: 0x00353C29 File Offset: 0x00351E29
		public float producedConversionRate { get; private set; }

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x06008BD0 RID: 35792 RVA: 0x00353C32 File Offset: 0x00351E32
		// (set) Token: 0x06008BD1 RID: 35793 RVA: 0x00353C3A File Offset: 0x00351E3A
		public byte diseaseIdx { get; private set; }

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x06008BD2 RID: 35794 RVA: 0x00353C43 File Offset: 0x00351E43
		// (set) Token: 0x06008BD3 RID: 35795 RVA: 0x00353C4B File Offset: 0x00351E4B
		public float diseasePerKgProduced { get; private set; }

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x06008BD4 RID: 35796 RVA: 0x00353C54 File Offset: 0x00351E54
		// (set) Token: 0x06008BD5 RID: 35797 RVA: 0x00353C5C File Offset: 0x00351E5C
		public bool emmitDiseaseOnCell { get; private set; }

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x06008BD6 RID: 35798 RVA: 0x00353C65 File Offset: 0x00351E65
		// (set) Token: 0x06008BD7 RID: 35799 RVA: 0x00353C6D File Offset: 0x00351E6D
		public bool produceSolidTile { get; private set; }

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x06008BD8 RID: 35800 RVA: 0x00353C76 File Offset: 0x00351E76
		// (set) Token: 0x06008BD9 RID: 35801 RVA: 0x00353C7E File Offset: 0x00351E7E
		public Diet.Info.FoodType foodType { get; private set; }

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x06008BDA RID: 35802 RVA: 0x00353C87 File Offset: 0x00351E87
		// (set) Token: 0x06008BDB RID: 35803 RVA: 0x00353C8F File Offset: 0x00351E8F
		public string[] eatAnims { get; set; }

		// Token: 0x06008BDC RID: 35804 RVA: 0x00353C98 File Offset: 0x00351E98
		public Info(HashSet<Tag> consumed_tags, Tag produced_element, float calories_per_kg, float produced_conversion_rate = 1f, string disease_id = null, float disease_per_kg_produced = 0f, bool produce_solid_tile = false, Diet.Info.FoodType food_type = Diet.Info.FoodType.EatSolid, bool emmit_disease_on_cell = false, string[] eat_anims = null)
		{
			this.consumedTags = consumed_tags;
			this.producedElement = produced_element;
			this.caloriesPerKg = calories_per_kg;
			this.producedConversionRate = produced_conversion_rate;
			if (!string.IsNullOrEmpty(disease_id))
			{
				this.diseaseIdx = Db.Get().Diseases.GetIndex(disease_id);
			}
			else
			{
				this.diseaseIdx = byte.MaxValue;
			}
			this.diseasePerKgProduced = disease_per_kg_produced;
			this.emmitDiseaseOnCell = emmit_disease_on_cell;
			this.produceSolidTile = produce_solid_tile;
			this.foodType = food_type;
			if (eat_anims == null)
			{
				eat_anims = new string[] { "eat_pre", "eat_loop", "eat_pst" };
			}
			this.eatAnims = eat_anims;
		}

		// Token: 0x06008BDD RID: 35805 RVA: 0x00353D48 File Offset: 0x00351F48
		public Info(Diet.Info info)
		{
			this.consumedTags = new HashSet<Tag>(info.consumedTags);
			this.producedElement = info.producedElement;
			this.caloriesPerKg = info.caloriesPerKg;
			this.producedConversionRate = info.producedConversionRate;
			this.diseaseIdx = info.diseaseIdx;
			this.diseasePerKgProduced = info.diseasePerKgProduced;
			this.emmitDiseaseOnCell = info.emmitDiseaseOnCell;
			this.produceSolidTile = info.produceSolidTile;
			this.foodType = info.foodType;
			this.eatAnims = info.eatAnims;
		}

		// Token: 0x06008BDE RID: 35806 RVA: 0x00353DD8 File Offset: 0x00351FD8
		public bool IsMatch(Tag tag)
		{
			return this.consumedTags.Contains(tag);
		}

		// Token: 0x06008BDF RID: 35807 RVA: 0x00353DE8 File Offset: 0x00351FE8
		public bool IsMatch(HashSet<Tag> tags)
		{
			if (tags.Count < this.consumedTags.Count)
			{
				foreach (Tag tag in tags)
				{
					if (this.consumedTags.Contains(tag))
					{
						return true;
					}
				}
				return false;
			}
			foreach (Tag tag2 in this.consumedTags)
			{
				if (tags.Contains(tag2))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06008BE0 RID: 35808 RVA: 0x00353EA4 File Offset: 0x003520A4
		public float ConvertCaloriesToConsumptionMass(float calories)
		{
			return calories / this.caloriesPerKg;
		}

		// Token: 0x06008BE1 RID: 35809 RVA: 0x00353EAE File Offset: 0x003520AE
		public float ConvertConsumptionMassToCalories(float mass)
		{
			return this.caloriesPerKg * mass;
		}

		// Token: 0x06008BE2 RID: 35810 RVA: 0x00353EB8 File Offset: 0x003520B8
		public float ConvertConsumptionMassToProducedMass(float consumed_mass)
		{
			return consumed_mass * this.producedConversionRate;
		}

		// Token: 0x06008BE3 RID: 35811 RVA: 0x00353EC2 File Offset: 0x003520C2
		public float ConvertProducedMassToConsumptionMass(float produced_mass)
		{
			return produced_mass / this.producedConversionRate;
		}

		// Token: 0x02002731 RID: 10033
		public enum FoodType
		{
			// Token: 0x0400AD17 RID: 44311
			EatSolid,
			// Token: 0x0400AD18 RID: 44312
			EatPlantDirectly,
			// Token: 0x0400AD19 RID: 44313
			EatPlantStorage,
			// Token: 0x0400AD1A RID: 44314
			EatPrey,
			// Token: 0x0400AD1B RID: 44315
			EatButcheredPrey
		}
	}
}
