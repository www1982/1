using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020008D7 RID: 2263
public class Electrobank : KMonoBehaviour, ISim1000ms, ISim200ms, IConsumableUIItem, IGameObjectEffectDescriptor
{
	// Token: 0x17000455 RID: 1109
	// (get) Token: 0x06003EB8 RID: 16056 RVA: 0x0016167D File Offset: 0x0015F87D
	// (set) Token: 0x06003EB7 RID: 16055 RVA: 0x00161674 File Offset: 0x0015F874
	public string ID { get; private set; }

	// Token: 0x17000456 RID: 1110
	// (get) Token: 0x06003EB9 RID: 16057 RVA: 0x00161685 File Offset: 0x0015F885
	public bool IsFullyCharged
	{
		get
		{
			return this.charge == Electrobank.capacity;
		}
	}

	// Token: 0x17000457 RID: 1111
	// (get) Token: 0x06003EBA RID: 16058 RVA: 0x00161694 File Offset: 0x0015F894
	public float Charge
	{
		get
		{
			return this.charge;
		}
	}

	// Token: 0x06003EBB RID: 16059 RVA: 0x0016169C File Offset: 0x0015F89C
	protected override void OnPrefabInit()
	{
		this.ID = base.gameObject.PrefabID().ToString();
		base.Subscribe(748399584, new Action<object>(this.OnCraft));
		base.OnPrefabInit();
	}

	// Token: 0x06003EBC RID: 16060 RVA: 0x001616E8 File Offset: 0x0015F8E8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.Subscribe(856640610, new Action<object>(this.ClearHealthBar));
		Components.Electrobanks.Add(base.gameObject.GetMyWorldId(), this);
		this.radiationEmitter = base.GetComponent<RadiationEmitter>();
		this.UpdateRadiationEmitter();
	}

	// Token: 0x06003EBD RID: 16061 RVA: 0x0016173B File Offset: 0x0015F93B
	private void OnCraft(object data)
	{
		WorldResourceAmountTracker<ElectrobankTracker>.Get().RegisterAmountProduced(this.Charge);
	}

	// Token: 0x06003EBE RID: 16062 RVA: 0x00161750 File Offset: 0x0015F950
	private void UpdateRadiationEmitter()
	{
		if (this.radiationEmitter == null)
		{
			return;
		}
		bool flag = this.timeSincePowerDrawn < 0.5f;
		this.radiationEmitter.emitRads = (flag ? this.radioactivityTuning : 0f);
		this.radiationEmitter.Refresh();
	}

	// Token: 0x06003EBF RID: 16063 RVA: 0x001617A0 File Offset: 0x0015F9A0
	private static GameObject Replace(GameObject electrobank, Tag replacement, bool dropFromStorage = false)
	{
		Vector3 position = electrobank.transform.GetPosition();
		GameObject gameObject = Util.KInstantiate(Assets.GetPrefab(replacement), position);
		gameObject.GetComponent<PrimaryElement>().SetElement(electrobank.GetComponent<PrimaryElement>().Element.id, true);
		gameObject.SetActive(true);
		Storage storage = electrobank.GetComponent<Pickupable>().storage;
		if (storage != null)
		{
			storage.Remove(electrobank, true);
		}
		electrobank.DeleteObject();
		if (storage != null && !dropFromStorage)
		{
			storage.Store(gameObject, false, false, true, false);
		}
		return gameObject;
	}

	// Token: 0x06003EC0 RID: 16064 RVA: 0x00161825 File Offset: 0x0015FA25
	public static GameObject ReplaceEmptyWithCharged(GameObject EmptyElectrobank, bool dropFromStorage = false)
	{
		return Electrobank.Replace(EmptyElectrobank, "Electrobank", dropFromStorage);
	}

	// Token: 0x06003EC1 RID: 16065 RVA: 0x00161838 File Offset: 0x0015FA38
	public static GameObject ReplaceChargedWithEmpty(GameObject ChargedElectrobank, bool dropFromStorage = false)
	{
		return Electrobank.Replace(ChargedElectrobank, "EmptyElectrobank", dropFromStorage);
	}

	// Token: 0x06003EC2 RID: 16066 RVA: 0x0016184B File Offset: 0x0015FA4B
	public static GameObject ReplaceEmptyWithGarbage(GameObject ChargedElectrobank, bool dropFromStorage = false)
	{
		return Electrobank.Replace(ChargedElectrobank, "GarbageElectrobank", dropFromStorage);
	}

	// Token: 0x06003EC3 RID: 16067 RVA: 0x00161860 File Offset: 0x0015FA60
	public float AddPower(float joules)
	{
		if (joules < 0f)
		{
			joules = 0f;
		}
		float num = Mathf.Min(joules, Electrobank.capacity - this.charge);
		this.charge += num;
		return num;
	}

	// Token: 0x06003EC4 RID: 16068 RVA: 0x001618A0 File Offset: 0x0015FAA0
	public float RemovePower(float joules, bool dropWhenEmpty)
	{
		float num = Mathf.Min(this.charge, joules);
		this.charge -= num;
		if (this.charge <= 0f)
		{
			this.OnEmpty(dropWhenEmpty);
		}
		if (num > 0f)
		{
			this.timeSincePowerDrawn = 0f;
		}
		return num;
	}

	// Token: 0x06003EC5 RID: 16069 RVA: 0x001618F0 File Offset: 0x0015FAF0
	protected virtual void OnEmpty(bool dropWhenEmpty)
	{
		if (this.rechargeable)
		{
			Electrobank.ReplaceChargedWithEmpty(base.gameObject, dropWhenEmpty);
			return;
		}
		if (!this.keepEmpty)
		{
			if (this.pickupable.storage != null)
			{
				this.pickupable.storage.Remove(base.gameObject, true);
			}
			Util.KDestroyGameObject(base.gameObject);
		}
	}

	// Token: 0x06003EC6 RID: 16070 RVA: 0x00161950 File Offset: 0x0015FB50
	public void FullyCharge()
	{
		this.charge = Electrobank.capacity;
	}

	// Token: 0x06003EC7 RID: 16071 RVA: 0x00161960 File Offset: 0x0015FB60
	public virtual void Explode()
	{
		int num = Grid.PosToCell(base.gameObject.transform.position);
		float num2 = Grid.Temperature[num];
		num2 += this.charge / (Grid.Mass[num] * Grid.Element[num].specificHeatCapacity);
		num2 = Mathf.Clamp(num2, 1f, 9999f);
		SimMessages.ReplaceElement(num, Grid.Element[num].id, CellEventLogger.Instance.SandBoxTool, Grid.Mass[num], num2, Grid.DiseaseIdx[num], Grid.DiseaseCount[num], -1);
		Game.Instance.SpawnFX(SpawnFXHashes.MeteorImpactMetal, base.gameObject.transform.position, 0f);
		KFMOD.PlayOneShot(GlobalAssets.GetSound("Battery_explode", false), base.gameObject.transform.position, 1f);
		if (this.rechargeable)
		{
			Electrobank.ReplaceEmptyWithGarbage(base.gameObject, false);
			return;
		}
		base.gameObject.DeleteObject();
	}

	// Token: 0x06003EC8 RID: 16072 RVA: 0x00161A6C File Offset: 0x0015FC6C
	protected void LaunchNearbyStuff()
	{
		ListPool<ScenePartitionerEntry, Comet>.PooledList pooledList = ListPool<ScenePartitionerEntry, Comet>.Allocate();
		Vector3 position = base.transform.position;
		GameScenePartitioner.Instance.GatherEntries((int)position.x - 3, (int)position.y - 3, 6, 6, GameScenePartitioner.Instance.pickupablesLayer, pooledList);
		foreach (ScenePartitionerEntry scenePartitionerEntry in pooledList)
		{
			GameObject gameObject = (scenePartitionerEntry.obj as Pickupable).gameObject;
			if (!(gameObject.GetComponent<MinionIdentity>() != null) && !(gameObject.GetComponent<CreatureBrain>() != null) && gameObject.GetDef<RobotAi.Def>() == null)
			{
				Vector2 vector = gameObject.transform.GetPosition() - position;
				vector = vector.normalized;
				vector *= (float)global::UnityEngine.Random.Range(4, 6);
				vector.y += (float)global::UnityEngine.Random.Range(2, 4);
				if (GameComps.Fallers.Has(gameObject))
				{
					GameComps.Fallers.Remove(gameObject);
				}
				if (GameComps.Gravities.Has(gameObject))
				{
					GameComps.Gravities.Remove(gameObject);
				}
				GameComps.Fallers.Add(gameObject, vector);
			}
		}
		pooledList.Recycle();
	}

	// Token: 0x06003EC9 RID: 16073 RVA: 0x00161BBC File Offset: 0x0015FDBC
	public void Sim1000ms(float dt)
	{
		if (this.pickupable.KPrefabID.HasTag(GameTags.Stored))
		{
			return;
		}
		this.EvaluateWaterDamage(dt);
		this.UpdateHealthBar();
	}

	// Token: 0x06003ECA RID: 16074 RVA: 0x00161BE3 File Offset: 0x0015FDE3
	public virtual void Sim200ms(float dt)
	{
		this.UpdateRadiationEmitter();
		this.timeSincePowerDrawn = Mathf.Min(this.timeSincePowerDrawn + dt, 10f);
	}

	// Token: 0x06003ECB RID: 16075 RVA: 0x00161C04 File Offset: 0x0015FE04
	private void EvaluateWaterDamage(float dt)
	{
		if (Grid.IsValidCell(this.pickupable.cachedCell) && Grid.Element[this.pickupable.cachedCell].HasTag(GameTags.AnyWater) && global::UnityEngine.Random.Range(1, 101) > 75)
		{
			PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Negative, UI.GAMEOBJECTEFFECTS.DAMAGE_POPS.POWER_BANK_WATER_DAMAGE, base.transform, 1.5f, false);
			this.Damage(global::UnityEngine.Random.Range(0f, dt));
		}
	}

	// Token: 0x06003ECC RID: 16076 RVA: 0x00161C88 File Offset: 0x0015FE88
	public void Damage(float amount)
	{
		Game.Instance.SpawnFX(SpawnFXHashes.ElectrobankDamage, Grid.PosToCell(base.gameObject), 0f);
		KFMOD.PlayOneShot(GlobalAssets.GetSound("Battery_sparks_short", false), base.gameObject.transform.position, 1f);
		this.currentHealth -= amount;
		if (this.healthBar == null)
		{
			this.CreateHealthBar();
		}
		this.healthBar.Update();
		this.lastDamageTime = Time.time;
		if (this.currentHealth <= 0f)
		{
			this.Explode();
		}
	}

	// Token: 0x06003ECD RID: 16077 RVA: 0x00161D24 File Offset: 0x0015FF24
	protected override void OnCleanUp()
	{
		this.ClearHealthBar(null);
		Components.Electrobanks.Remove(base.gameObject.GetMyWorldId(), this);
		base.OnCleanUp();
	}

	// Token: 0x06003ECE RID: 16078 RVA: 0x00161D49 File Offset: 0x0015FF49
	public void CreateHealthBar()
	{
		this.healthBar = ProgressBar.CreateProgressBar(base.gameObject, () => this.currentHealth / 10f);
		this.healthBar.SetVisibility(true);
		this.healthBar.barColor = Util.ColorFromHex("CC3333");
	}

	// Token: 0x06003ECF RID: 16079 RVA: 0x00161D89 File Offset: 0x0015FF89
	public void UpdateHealthBar()
	{
		if (this.healthBar != null && Time.time - this.lastDamageTime > 5f)
		{
			this.ClearHealthBar(null);
		}
	}

	// Token: 0x06003ED0 RID: 16080 RVA: 0x00161DB3 File Offset: 0x0015FFB3
	public void ClearHealthBar(object data = null)
	{
		if (this.healthBar != null)
		{
			Util.KDestroyGameObject(this.healthBar);
			this.healthBar = null;
		}
	}

	// Token: 0x06003ED1 RID: 16081 RVA: 0x00161DD8 File Offset: 0x0015FFD8
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		Descriptor descriptor = default(Descriptor);
		descriptor.SetupDescriptor(string.Format(UI.BUILDINGEFFECTS.ELECTROBANKS, GameUtil.GetFormattedJoules(this.Charge, "F1", GameUtil.TimeSlice.None)), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.ELECTROBANKS, GameUtil.GetFormattedJoules(this.Charge, "F1", GameUtil.TimeSlice.None)), Descriptor.DescriptorType.Effect);
		list.Add(descriptor);
		return list;
	}

	// Token: 0x17000458 RID: 1112
	// (get) Token: 0x06003ED2 RID: 16082 RVA: 0x00161E44 File Offset: 0x00160044
	public string ConsumableId
	{
		get
		{
			return this.PrefabID().Name;
		}
	}

	// Token: 0x17000459 RID: 1113
	// (get) Token: 0x06003ED3 RID: 16083 RVA: 0x00161E5F File Offset: 0x0016005F
	public string ConsumableName
	{
		get
		{
			return this.GetProperName();
		}
	}

	// Token: 0x1700045A RID: 1114
	// (get) Token: 0x06003ED4 RID: 16084 RVA: 0x00161E67 File Offset: 0x00160067
	public int MajorOrder
	{
		get
		{
			return 500;
		}
	}

	// Token: 0x1700045B RID: 1115
	// (get) Token: 0x06003ED5 RID: 16085 RVA: 0x00161E6E File Offset: 0x0016006E
	public int MinorOrder
	{
		get
		{
			return 0;
		}
	}

	// Token: 0x1700045C RID: 1116
	// (get) Token: 0x06003ED6 RID: 16086 RVA: 0x00161E71 File Offset: 0x00160071
	public bool Display
	{
		get
		{
			return true;
		}
	}

	// Token: 0x040026BB RID: 9915
	private static float capacity = 120000f;

	// Token: 0x040026BC RID: 9916
	[Serialize]
	private float charge = Electrobank.capacity;

	// Token: 0x040026BD RID: 9917
	private const float MAX_HEALTH = 10f;

	// Token: 0x040026BE RID: 9918
	[Serialize]
	private float currentHealth = 10f;

	// Token: 0x040026BF RID: 9919
	[Serialize]
	private float timeSincePowerDrawn = 0.5f;

	// Token: 0x040026C0 RID: 9920
	private const float RADIATION_EMITTER_TIMEOUT = 0.5f;

	// Token: 0x040026C1 RID: 9921
	public float radioactivityTuning;

	// Token: 0x040026C2 RID: 9922
	private RadiationEmitter radiationEmitter;

	// Token: 0x040026C3 RID: 9923
	private float lastDamageTime;

	// Token: 0x040026C4 RID: 9924
	public ProgressBar healthBar;

	// Token: 0x040026C5 RID: 9925
	public bool rechargeable;

	// Token: 0x040026C6 RID: 9926
	public bool keepEmpty;

	// Token: 0x040026C7 RID: 9927
	[MyCmpGet]
	private Pickupable pickupable;
}
