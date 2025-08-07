using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x0200085E RID: 2142
[AddComponentMenu("KMonoBehaviour/scripts/Crop")]
public class Crop : KMonoBehaviour, IGameObjectEffectDescriptor
{
	// Token: 0x17000404 RID: 1028
	// (get) Token: 0x06003ACB RID: 15051 RVA: 0x00146D34 File Offset: 0x00144F34
	public string cropId
	{
		get
		{
			return this.cropVal.cropId;
		}
	}

	// Token: 0x17000405 RID: 1029
	// (get) Token: 0x06003ACC RID: 15052 RVA: 0x00146D41 File Offset: 0x00144F41
	// (set) Token: 0x06003ACD RID: 15053 RVA: 0x00146D49 File Offset: 0x00144F49
	public Storage PlanterStorage
	{
		get
		{
			return this.planterStorage;
		}
		set
		{
			this.planterStorage = value;
		}
	}

	// Token: 0x06003ACE RID: 15054 RVA: 0x00146D52 File Offset: 0x00144F52
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		Components.Crops.Add(this);
		this.yield = this.GetAttributes().Add(Db.Get().PlantAttributes.YieldAmount);
	}

	// Token: 0x06003ACF RID: 15055 RVA: 0x00146D85 File Offset: 0x00144F85
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.Subscribe<Crop>(1272413801, Crop.OnHarvestDelegate);
	}

	// Token: 0x06003AD0 RID: 15056 RVA: 0x00146D9E File Offset: 0x00144F9E
	public void Configure(Crop.CropVal cropval)
	{
		this.cropVal = cropval;
	}

	// Token: 0x06003AD1 RID: 15057 RVA: 0x00146DA7 File Offset: 0x00144FA7
	public bool CanGrow()
	{
		return this.cropVal.renewable;
	}

	// Token: 0x06003AD2 RID: 15058 RVA: 0x00146DB4 File Offset: 0x00144FB4
	public void SpawnConfiguredFruit(object callbackParam)
	{
		if (this == null)
		{
			return;
		}
		Crop.CropVal cropVal = this.cropVal;
		if (!string.IsNullOrEmpty(cropVal.cropId))
		{
			this.SpawnSomeFruit(cropVal.cropId, this.yield.GetTotalValue());
			base.Trigger(-1072826864, this);
		}
	}

	// Token: 0x06003AD3 RID: 15059 RVA: 0x00146E08 File Offset: 0x00145008
	public void SpawnSomeFruit(Tag cropID, float amount)
	{
		GameObject gameObject = GameUtil.KInstantiate(Assets.GetPrefab(cropID), base.transform.GetPosition() + this.cropSpawnOffset, Grid.SceneLayer.Ore, null, 0);
		if (gameObject != null)
		{
			MutantPlant component = base.GetComponent<MutantPlant>();
			MutantPlant component2 = gameObject.GetComponent<MutantPlant>();
			if (component != null && component.IsOriginal && component2 != null && base.GetComponent<SeedProducer>().RollForMutation())
			{
				component2.Mutate();
			}
			gameObject.SetActive(true);
			PrimaryElement component3 = gameObject.GetComponent<PrimaryElement>();
			component3.Units = amount;
			component3.Temperature = base.gameObject.GetComponent<PrimaryElement>().Temperature;
			base.Trigger(35625290, gameObject);
			Edible component4 = gameObject.GetComponent<Edible>();
			if (component4)
			{
				ReportManager.Instance.ReportValue(ReportManager.ReportType.CaloriesCreated, component4.Calories, StringFormatter.Replace(UI.ENDOFDAYREPORT.NOTES.HARVESTED, "{0}", component4.GetProperName()), UI.ENDOFDAYREPORT.NOTES.HARVESTED_CONTEXT);
				return;
			}
		}
		else
		{
			DebugUtil.LogErrorArgs(base.gameObject, new object[] { "tried to spawn an invalid crop prefab:", cropID });
		}
	}

	// Token: 0x06003AD4 RID: 15060 RVA: 0x00146F1E File Offset: 0x0014511E
	protected override void OnCleanUp()
	{
		Components.Crops.Remove(this);
		base.OnCleanUp();
	}

	// Token: 0x06003AD5 RID: 15061 RVA: 0x00146F31 File Offset: 0x00145131
	private void OnHarvest(object obj)
	{
	}

	// Token: 0x06003AD6 RID: 15062 RVA: 0x00146F33 File Offset: 0x00145133
	public List<Descriptor> RequirementDescriptors(GameObject go)
	{
		return new List<Descriptor>();
	}

	// Token: 0x06003AD7 RID: 15063 RVA: 0x00146F3C File Offset: 0x0014513C
	public List<Descriptor> InformationDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		Tag tag = new Tag(this.cropVal.cropId);
		GameObject prefab = Assets.GetPrefab(tag);
		if (prefab == null)
		{
			DebugUtil.LogWarningArgs(new object[]
			{
				"Crop",
				base.gameObject.name,
				"has an invalid crop prefab:",
				tag
			});
			return list;
		}
		Edible component = prefab.GetComponent<Edible>();
		Klei.AI.Attribute yieldAmount = Db.Get().PlantAttributes.YieldAmount;
		float preModifiedAttributeValue = go.GetComponent<Modifiers>().GetPreModifiedAttributeValue(yieldAmount);
		if (component != null)
		{
			DebugUtil.Assert(GameTags.DisplayAsCalories.Contains(tag), "Trying to display crop info for an edible fruit which isn't displayed as calories!", tag.ToString());
			float caloriesPerUnit = component.FoodInfo.CaloriesPerUnit;
			float num = caloriesPerUnit * preModifiedAttributeValue;
			string text = GameUtil.GetFormattedCalories(num, GameUtil.TimeSlice.None, true);
			Descriptor descriptor = new Descriptor(string.Format(UI.UISIDESCREENS.PLANTERSIDESCREEN.YIELD, prefab.GetProperName(), text), string.Format(UI.UISIDESCREENS.PLANTERSIDESCREEN.TOOLTIPS.YIELD, "", GameUtil.GetFormattedCalories(caloriesPerUnit, GameUtil.TimeSlice.None, true), GameUtil.GetFormattedCalories(num, GameUtil.TimeSlice.None, true)), Descriptor.DescriptorType.Effect, false);
			list.Add(descriptor);
		}
		else
		{
			string text;
			if (GameTags.DisplayAsUnits.Contains(tag))
			{
				text = GameUtil.GetFormattedUnits((float)this.cropVal.numProduced, GameUtil.TimeSlice.None, false, "");
			}
			else
			{
				text = GameUtil.GetFormattedMass((float)this.cropVal.numProduced, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}");
			}
			Descriptor descriptor2 = new Descriptor(string.Format(UI.UISIDESCREENS.PLANTERSIDESCREEN.YIELD_NONFOOD, prefab.GetProperName(), text), string.Format(UI.UISIDESCREENS.PLANTERSIDESCREEN.TOOLTIPS.YIELD_NONFOOD, text), Descriptor.DescriptorType.Effect, false);
			list.Add(descriptor2);
		}
		return list;
	}

	// Token: 0x06003AD8 RID: 15064 RVA: 0x001470E8 File Offset: 0x001452E8
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		foreach (Descriptor descriptor in this.RequirementDescriptors(go))
		{
			list.Add(descriptor);
		}
		foreach (Descriptor descriptor2 in this.InformationDescriptors(go))
		{
			list.Add(descriptor2);
		}
		return list;
	}

	// Token: 0x0400240A RID: 9226
	[MyCmpReq]
	private KSelectable selectable;

	// Token: 0x0400240B RID: 9227
	public Crop.CropVal cropVal;

	// Token: 0x0400240C RID: 9228
	private AttributeInstance yield;

	// Token: 0x0400240D RID: 9229
	public Vector3 cropSpawnOffset = new Vector3(0f, 0.75f, 0f);

	// Token: 0x0400240E RID: 9230
	public string domesticatedDesc = "";

	// Token: 0x0400240F RID: 9231
	private Storage planterStorage;

	// Token: 0x04002410 RID: 9232
	private static readonly EventSystem.IntraObjectHandler<Crop> OnHarvestDelegate = new EventSystem.IntraObjectHandler<Crop>(delegate(Crop component, object data)
	{
		component.OnHarvest(data);
	});

	// Token: 0x020017ED RID: 6125
	[Serializable]
	public struct CropVal
	{
		// Token: 0x06009AD8 RID: 39640 RVA: 0x0038B6BA File Offset: 0x003898BA
		public CropVal(string crop_id, float crop_duration, int num_produced = 1, bool renewable = true)
		{
			this.cropId = crop_id;
			this.cropDuration = crop_duration;
			this.numProduced = num_produced;
			this.renewable = renewable;
		}

		// Token: 0x0400775D RID: 30557
		public string cropId;

		// Token: 0x0400775E RID: 30558
		public float cropDuration;

		// Token: 0x0400775F RID: 30559
		public int numProduced;

		// Token: 0x04007760 RID: 30560
		public bool renewable;
	}
}
