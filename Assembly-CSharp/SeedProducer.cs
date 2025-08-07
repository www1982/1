using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x02000884 RID: 2180
[AddComponentMenu("KMonoBehaviour/scripts/SeedProducer")]
public class SeedProducer : KMonoBehaviour, IGameObjectEffectDescriptor
{
	// Token: 0x06003BF0 RID: 15344 RVA: 0x0014C4D5 File Offset: 0x0014A6D5
	public void Configure(string SeedID, SeedProducer.ProductionType productionType, int newSeedsProduced = 1)
	{
		this.seedInfo.seedId = SeedID;
		this.seedInfo.productionType = productionType;
		this.seedInfo.newSeedsProduced = newSeedsProduced;
	}

	// Token: 0x06003BF1 RID: 15345 RVA: 0x0014C4FB File Offset: 0x0014A6FB
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.Subscribe<SeedProducer>(-216549700, SeedProducer.DropSeedDelegate);
		base.Subscribe<SeedProducer>(1623392196, SeedProducer.DropSeedDelegate);
		base.Subscribe<SeedProducer>(-1072826864, SeedProducer.CropPickedDelegate);
	}

	// Token: 0x06003BF2 RID: 15346 RVA: 0x0014C538 File Offset: 0x0014A738
	private GameObject ProduceSeed(string seedId, int units = 1, bool canMutate = true)
	{
		if (seedId != null && units > 0)
		{
			Vector3 vector = base.gameObject.transform.GetPosition() + new Vector3(0f, 0.5f, 0f);
			GameObject gameObject = GameUtil.KInstantiate(Assets.GetPrefab(new Tag(seedId)), vector, Grid.SceneLayer.Ore, null, 0);
			MutantPlant component = base.GetComponent<MutantPlant>();
			if (component != null)
			{
				MutantPlant component2 = gameObject.GetComponent<MutantPlant>();
				bool flag = false;
				if (canMutate && component2 != null && component2.IsOriginal)
				{
					flag = this.RollForMutation();
				}
				if (flag)
				{
					component2.Mutate();
				}
				else
				{
					component.CopyMutationsTo(component2);
				}
			}
			PrimaryElement component3 = base.gameObject.GetComponent<PrimaryElement>();
			PrimaryElement component4 = gameObject.GetComponent<PrimaryElement>();
			component4.Temperature = component3.Temperature;
			component4.Units = (float)units;
			base.Trigger(472291861, gameObject);
			gameObject.SetActive(true);
			string text = gameObject.GetProperName();
			if (component != null)
			{
				text = component.GetSubSpeciesInfo().GetNameWithMutations(text, component.IsIdentified, false);
			}
			PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Plus, text, gameObject.transform, 1.5f, false);
			return gameObject;
		}
		return null;
	}

	// Token: 0x06003BF3 RID: 15347 RVA: 0x0014C66C File Offset: 0x0014A86C
	public void DropSeed(object data = null)
	{
		if (this.droppedSeedAlready)
		{
			return;
		}
		if (this.seedInfo.newSeedsProduced <= 0)
		{
			return;
		}
		GameObject gameObject = this.ProduceSeed(this.seedInfo.seedId, this.seedInfo.newSeedsProduced, false);
		Uprootable component = base.GetComponent<Uprootable>();
		if (component != null && component.worker != null)
		{
			gameObject.Trigger(580035959, component.worker);
		}
		base.Trigger(-1736624145, gameObject);
		this.droppedSeedAlready = true;
	}

	// Token: 0x06003BF4 RID: 15348 RVA: 0x0014C6F1 File Offset: 0x0014A8F1
	public void CropDepleted(object data)
	{
		this.DropSeed(null);
	}

	// Token: 0x06003BF5 RID: 15349 RVA: 0x0014C6FC File Offset: 0x0014A8FC
	public void CropPicked(object data)
	{
		if (this.seedInfo.productionType == SeedProducer.ProductionType.Harvest || this.seedInfo.productionType == SeedProducer.ProductionType.HarvestOnly)
		{
			WorkerBase completed_by = base.GetComponent<Harvestable>().completed_by;
			float num = this.seedDropChances;
			if (completed_by != null)
			{
				num += completed_by.GetComponent<AttributeConverters>().Get(Db.Get().AttributeConverters.SeedHarvestChance).Evaluate();
			}
			num *= this.seedDropChanceMultiplier;
			int num2 = ((global::UnityEngine.Random.Range(0f, 1f) <= num) ? 1 : 0);
			if (num2 > 0)
			{
				this.ProduceSeed(this.seedInfo.seedId, num2, true).Trigger(580035959, completed_by);
			}
		}
	}

	// Token: 0x06003BF6 RID: 15350 RVA: 0x0014C7A8 File Offset: 0x0014A9A8
	public bool RollForMutation()
	{
		AttributeInstance attributeInstance = Db.Get().PlantAttributes.MaxRadiationThreshold.Lookup(this);
		int num = Grid.PosToCell(base.gameObject);
		float num2 = Mathf.Clamp(Grid.IsValidCell(num) ? Grid.Radiation[num] : 0f, 0f, attributeInstance.GetTotalValue()) / attributeInstance.GetTotalValue() * 0.8f;
		return global::UnityEngine.Random.value < num2;
	}

	// Token: 0x06003BF7 RID: 15351 RVA: 0x0014C818 File Offset: 0x0014AA18
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		Assets.GetPrefab(new Tag(this.seedInfo.seedId)) != null;
		switch (this.seedInfo.productionType)
		{
		case SeedProducer.ProductionType.Hidden:
		case SeedProducer.ProductionType.DigOnly:
		case SeedProducer.ProductionType.Crop:
			return null;
		case SeedProducer.ProductionType.Harvest:
		case SeedProducer.ProductionType.HarvestOnly:
			list.Add(new Descriptor(UI.GAMEOBJECTEFFECTS.SEED_PRODUCTION_HARVEST, UI.GAMEOBJECTEFFECTS.TOOLTIPS.SEED_PRODUCTION_HARVEST, Descriptor.DescriptorType.Lifecycle, true));
			list.Add(new Descriptor(string.Format(UI.UISIDESCREENS.PLANTERSIDESCREEN.BONUS_SEEDS, GameUtil.GetFormattedPercent(this.seedDropChances * 100f * this.seedDropChanceMultiplier, GameUtil.TimeSlice.None)), string.Format(UI.UISIDESCREENS.PLANTERSIDESCREEN.TOOLTIPS.BONUS_SEEDS, GameUtil.GetFormattedPercent(this.seedDropChances * 100f * this.seedDropChanceMultiplier, GameUtil.TimeSlice.None)), Descriptor.DescriptorType.Effect, false));
			break;
		case SeedProducer.ProductionType.Fruit:
			list.Add(new Descriptor(UI.GAMEOBJECTEFFECTS.SEED_PRODUCTION_FRUIT, UI.GAMEOBJECTEFFECTS.TOOLTIPS.SEED_PRODUCTION_DIG_ONLY, Descriptor.DescriptorType.Lifecycle, true));
			break;
		case SeedProducer.ProductionType.Sterile:
			list.Add(new Descriptor(UI.GAMEOBJECTEFFECTS.MUTANT_STERILE, UI.GAMEOBJECTEFFECTS.TOOLTIPS.MUTANT_STERILE, Descriptor.DescriptorType.Effect, false));
			break;
		default:
			DebugUtil.Assert(false, "Seed producer type descriptor not specified");
			return null;
		}
		return list;
	}

	// Token: 0x040024C2 RID: 9410
	public SeedProducer.SeedInfo seedInfo;

	// Token: 0x040024C3 RID: 9411
	public float seedDropChanceMultiplier = 1f;

	// Token: 0x040024C4 RID: 9412
	public float seedDropChances = 0.1f;

	// Token: 0x040024C5 RID: 9413
	private bool droppedSeedAlready;

	// Token: 0x040024C6 RID: 9414
	private static readonly EventSystem.IntraObjectHandler<SeedProducer> DropSeedDelegate = new EventSystem.IntraObjectHandler<SeedProducer>(delegate(SeedProducer component, object data)
	{
		if (component.seedInfo.productionType != SeedProducer.ProductionType.HarvestOnly)
		{
			component.DropSeed(data);
		}
	});

	// Token: 0x040024C7 RID: 9415
	private static readonly EventSystem.IntraObjectHandler<SeedProducer> CropPickedDelegate = new EventSystem.IntraObjectHandler<SeedProducer>(delegate(SeedProducer component, object data)
	{
		component.CropPicked(data);
	});

	// Token: 0x02001846 RID: 6214
	[Serializable]
	public struct SeedInfo
	{
		// Token: 0x04007868 RID: 30824
		public string seedId;

		// Token: 0x04007869 RID: 30825
		public SeedProducer.ProductionType productionType;

		// Token: 0x0400786A RID: 30826
		public int newSeedsProduced;
	}

	// Token: 0x02001847 RID: 6215
	public enum ProductionType
	{
		// Token: 0x0400786C RID: 30828
		Hidden,
		// Token: 0x0400786D RID: 30829
		DigOnly,
		// Token: 0x0400786E RID: 30830
		Harvest,
		// Token: 0x0400786F RID: 30831
		Fruit,
		// Token: 0x04007870 RID: 30832
		Sterile,
		// Token: 0x04007871 RID: 30833
		Crop,
		// Token: 0x04007872 RID: 30834
		HarvestOnly
	}
}
