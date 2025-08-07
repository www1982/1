using System;
using System.Collections.Generic;
using FMOD.Studio;
using Klei.CustomSettings;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000833 RID: 2099
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/Comet")]
public class Comet : KMonoBehaviour, ISim33ms
{
	// Token: 0x170003E4 RID: 996
	// (get) Token: 0x06003973 RID: 14707 RVA: 0x0013E49F File Offset: 0x0013C69F
	public float ExplosionMass
	{
		get
		{
			return this.explosionMass;
		}
	}

	// Token: 0x170003E5 RID: 997
	// (get) Token: 0x06003974 RID: 14708 RVA: 0x0013E4A7 File Offset: 0x0013C6A7
	public float AddTileMass
	{
		get
		{
			return this.addTileMass;
		}
	}

	// Token: 0x170003E6 RID: 998
	// (get) Token: 0x06003975 RID: 14709 RVA: 0x0013E4AF File Offset: 0x0013C6AF
	public Vector3 TargetPosition
	{
		get
		{
			return this.anim.PositionIncludingOffset;
		}
	}

	// Token: 0x170003E7 RID: 999
	// (get) Token: 0x06003976 RID: 14710 RVA: 0x0013E4BC File Offset: 0x0013C6BC
	// (set) Token: 0x06003977 RID: 14711 RVA: 0x0013E4C4 File Offset: 0x0013C6C4
	public Vector2 Velocity
	{
		get
		{
			return this.velocity;
		}
		set
		{
			this.velocity = value;
		}
	}

	// Token: 0x06003978 RID: 14712 RVA: 0x0013E4D0 File Offset: 0x0013C6D0
	private float GetVolume(GameObject gameObject)
	{
		float num = 1f;
		if (gameObject != null && this.selectable != null && this.selectable.IsSelected)
		{
			num = 1f;
		}
		return num;
	}

	// Token: 0x06003979 RID: 14713 RVA: 0x0013E50E File Offset: 0x0013C70E
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.remainingTileDamage = this.totalTileDamage;
		this.loopingSounds = base.gameObject.GetComponent<LoopingSounds>();
		this.flyingSound = GlobalAssets.GetSound("Meteor_LP", false);
		this.RandomizeVelocity();
	}

	// Token: 0x0600397A RID: 14714 RVA: 0x0013E54C File Offset: 0x0013C74C
	protected override void OnSpawn()
	{
		this.anim.Offset = this.offsetPosition;
		if (this.spawnWithOffset)
		{
			this.SetupOffset();
		}
		base.OnSpawn();
		this.RandomizeMassAndTemperature();
		this.StartLoopingSound();
		bool flag = this.offsetPosition.x != 0f || this.offsetPosition.y != 0f;
		this.selectable.enabled = !flag;
		this.typeID = base.GetComponent<KPrefabID>().PrefabTag;
		Components.Meteors.Add(base.gameObject.GetMyWorldId(), this);
	}

	// Token: 0x0600397B RID: 14715 RVA: 0x0013E5EB File Offset: 0x0013C7EB
	protected override void OnCleanUp()
	{
		Components.Meteors.Remove(base.gameObject.GetMyWorldId(), this);
	}

	// Token: 0x0600397C RID: 14716 RVA: 0x0013E604 File Offset: 0x0013C804
	protected void SetupOffset()
	{
		Vector3 position = base.transform.GetPosition();
		Vector3 position2 = base.transform.GetPosition();
		position2.z = 0f;
		Vector3 vector = new Vector3(this.velocity.x, this.velocity.y, 0f);
		WorldContainer myWorld = base.gameObject.GetMyWorld();
		float num = (float)(myWorld.WorldOffset.y + myWorld.Height + MissileLauncher.Def.launchRange.y) * Grid.CellSizeInMeters - position2.y;
		float num2 = Vector3.Angle(Vector3.up, -vector) * 0.017453292f;
		float num3 = Mathf.Abs(num / Mathf.Cos(num2));
		Vector3 vector2 = position2 - vector.normalized * num3;
		float num4 = (float)(myWorld.WorldOffset.x + myWorld.Width) * Grid.CellSizeInMeters;
		if (vector2.x < (float)myWorld.WorldOffset.x * Grid.CellSizeInMeters || vector2.x > num4)
		{
			float num5 = ((vector.x < 0f) ? (num4 - position2.x) : (position2.x - (float)myWorld.WorldOffset.x * Grid.CellSizeInMeters));
			num2 = Vector3.Angle((vector.x < 0f) ? Vector3.right : Vector3.left, -vector) * 0.017453292f;
			num3 = Mathf.Abs(num5 / Mathf.Cos(num2));
		}
		Vector3 vector3 = -vector.normalized * num3;
		(position2 + vector3).z = position.z;
		this.offsetPosition = vector3;
		this.anim.Offset = this.offsetPosition;
	}

	// Token: 0x0600397D RID: 14717 RVA: 0x0013E7C8 File Offset: 0x0013C9C8
	public virtual void RandomizeVelocity()
	{
		float num = global::UnityEngine.Random.Range(this.spawnAngle.x, this.spawnAngle.y);
		float num2 = num * 3.1415927f / 180f;
		float num3 = global::UnityEngine.Random.Range(this.spawnVelocity.x, this.spawnVelocity.y);
		this.velocity = new Vector2(-Mathf.Cos(num2) * num3, Mathf.Sin(num2) * num3);
		base.GetComponent<KBatchedAnimController>().Rotation = -num - 90f;
	}

	// Token: 0x0600397E RID: 14718 RVA: 0x0013E84C File Offset: 0x0013CA4C
	public void RandomizeMassAndTemperature()
	{
		float num = global::UnityEngine.Random.Range(this.massRange.x, this.massRange.y) * this.GetMassMultiplier();
		PrimaryElement component = base.GetComponent<PrimaryElement>();
		component.Mass = num;
		component.Temperature = global::UnityEngine.Random.Range(this.temperatureRange.x, this.temperatureRange.y);
		if (this.addTiles > 0)
		{
			float num2 = global::UnityEngine.Random.Range(0.95f, 0.98f);
			this.explosionMass = num * (1f - num2);
			this.addTileMass = num * num2;
			return;
		}
		this.explosionMass = num;
		this.addTileMass = 0f;
	}

	// Token: 0x0600397F RID: 14719 RVA: 0x0013E8F0 File Offset: 0x0013CAF0
	public float GetMassMultiplier()
	{
		float num = 1f;
		SettingLevel currentQualitySetting = CustomGameSettings.Instance.GetCurrentQualitySetting(CustomGameSettingConfigs.MeteorShowers);
		if (this.affectedByDifficulty && currentQualitySetting != null)
		{
			string id = currentQualitySetting.id;
			if (!(id == "Infrequent"))
			{
				if (!(id == "Intense"))
				{
					if (id == "Doomed")
					{
						num *= 0.5f;
					}
				}
				else
				{
					num *= 0.8f;
				}
			}
			else
			{
				num *= 1f;
			}
		}
		return num;
	}

	// Token: 0x06003980 RID: 14720 RVA: 0x0013E96B File Offset: 0x0013CB6B
	public int GetRandomNumOres()
	{
		return global::UnityEngine.Random.Range(this.explosionOreCount.x, this.explosionOreCount.y + 1);
	}

	// Token: 0x06003981 RID: 14721 RVA: 0x0013E98A File Offset: 0x0013CB8A
	public float GetRandomTemperatureForOres()
	{
		return global::UnityEngine.Random.Range(this.explosionTemperatureRange.x, this.explosionTemperatureRange.y);
	}

	// Token: 0x06003982 RID: 14722 RVA: 0x0013E9A8 File Offset: 0x0013CBA8
	[ContextMenu("Explode")]
	private void Explode(Vector3 pos, int cell, int prev_cell, Element element)
	{
		int num = (int)Grid.WorldIdx[cell];
		this.PlayImpactSound(pos);
		Vector3 vector = pos;
		vector.z = Grid.GetLayerZ(Grid.SceneLayer.FXFront2);
		if (this.explosionEffectHash != SpawnFXHashes.None)
		{
			Game.Instance.SpawnFX(this.explosionEffectHash, vector, 0f);
		}
		Substance substance = element.substance;
		int randomNumOres = this.GetRandomNumOres();
		Vector2 vector2 = -this.velocity.normalized;
		Vector2 vector3 = new Vector2(vector2.y, -vector2.x);
		ListPool<ScenePartitionerEntry, Comet>.PooledList pooledList = ListPool<ScenePartitionerEntry, Comet>.Allocate();
		GameScenePartitioner.Instance.GatherEntries((int)pos.x - 3, (int)pos.y - 3, 6, 6, GameScenePartitioner.Instance.pickupablesLayer, pooledList);
		foreach (ScenePartitionerEntry scenePartitionerEntry in pooledList)
		{
			GameObject gameObject = (scenePartitionerEntry.obj as Pickupable).gameObject;
			if (!(gameObject.GetComponent<MinionIdentity>() != null) && !(gameObject.GetComponent<CreatureBrain>() != null) && gameObject.GetDef<RobotAi.Def>() == null)
			{
				Vector2 vector4 = (gameObject.transform.GetPosition() - pos).normalized;
				vector4 += new Vector2(0f, 0.55f);
				vector4 *= 0.5f * global::UnityEngine.Random.Range(this.explosionSpeedRange.x, this.explosionSpeedRange.y);
				if (GameComps.Fallers.Has(gameObject))
				{
					GameComps.Fallers.Remove(gameObject);
				}
				if (GameComps.Gravities.Has(gameObject))
				{
					GameComps.Gravities.Remove(gameObject);
				}
				GameComps.Fallers.Add(gameObject, vector4);
			}
		}
		pooledList.Recycle();
		int num2 = this.splashRadius + 1;
		for (int i = -num2; i <= num2; i++)
		{
			for (int j = -num2; j <= num2; j++)
			{
				int num3 = Grid.OffsetCell(cell, j, i);
				if (Grid.IsValidCellInWorld(num3, num) && !this.destroyedCells.Contains(num3))
				{
					float num4 = (1f - (float)Mathf.Abs(j) / (float)num2) * (1f - (float)Mathf.Abs(i) / (float)num2);
					if (num4 > 0f)
					{
						this.DamageTiles(num3, prev_cell, num4 * this.totalTileDamage * 0.5f);
					}
				}
			}
		}
		float num5 = ((randomNumOres > 0) ? (this.explosionMass / (float)randomNumOres) : 1f);
		float randomTemperatureForOres = this.GetRandomTemperatureForOres();
		PrimaryElement component = base.GetComponent<PrimaryElement>();
		for (int k = 0; k < randomNumOres; k++)
		{
			Vector2 normalized = (vector2 + vector3 * global::UnityEngine.Random.Range(-1f, 1f)).normalized;
			Vector3 vector5 = normalized * global::UnityEngine.Random.Range(this.explosionSpeedRange.x, this.explosionSpeedRange.y);
			Vector3 vector6 = normalized.normalized * 0.75f;
			vector6 += new Vector3(0f, 0.55f, 0f);
			vector6 += pos;
			GameObject gameObject2 = substance.SpawnResource(vector6, num5, randomTemperatureForOres, component.DiseaseIdx, component.DiseaseCount / (randomNumOres + this.addTiles), false, false, false);
			if (GameComps.Fallers.Has(gameObject2))
			{
				GameComps.Fallers.Remove(gameObject2);
			}
			GameComps.Fallers.Add(gameObject2, vector5);
		}
		if (this.addTiles > 0)
		{
			this.DepositTiles(cell, element, num, prev_cell, randomTemperatureForOres);
		}
		this.SpawnCraterPrefabs();
		if (this.OnImpact != null)
		{
			this.OnImpact();
		}
	}

	// Token: 0x06003983 RID: 14723 RVA: 0x0013ED8C File Offset: 0x0013CF8C
	protected virtual void DepositTiles(int cell, Element element, int world, int prev_cell, float temperature)
	{
		float depthOfElement = (float)this.GetDepthOfElement(cell, element, world);
		float num = 1f;
		float num2 = (depthOfElement - (float)this.addTilesMinHeight) / (float)(this.addTilesMaxHeight - this.addTilesMinHeight);
		if (!float.IsNaN(num2))
		{
			num -= num2;
		}
		int num3 = Mathf.Min(this.addTiles, Mathf.Clamp(Mathf.RoundToInt((float)this.addTiles * num), 1, this.addTiles));
		HashSetPool<int, Comet>.PooledHashSet pooledHashSet = HashSetPool<int, Comet>.Allocate();
		HashSetPool<int, Comet>.PooledHashSet pooledHashSet2 = HashSetPool<int, Comet>.Allocate();
		QueuePool<GameUtil.FloodFillInfo, Comet>.PooledQueue pooledQueue = QueuePool<GameUtil.FloodFillInfo, Comet>.Allocate();
		int num4 = -1;
		int num5 = 1;
		if (this.velocity.x < 0f)
		{
			num4 *= -1;
			num5 *= -1;
		}
		pooledQueue.Enqueue(new GameUtil.FloodFillInfo
		{
			cell = prev_cell,
			depth = 0
		});
		pooledQueue.Enqueue(new GameUtil.FloodFillInfo
		{
			cell = Grid.OffsetCell(prev_cell, new CellOffset(num4, 0)),
			depth = 0
		});
		pooledQueue.Enqueue(new GameUtil.FloodFillInfo
		{
			cell = Grid.OffsetCell(prev_cell, new CellOffset(num5, 0)),
			depth = 0
		});
		Func<int, bool> func = (int cell) => Grid.IsValidCellInWorld(cell, world) && !Grid.Solid[cell];
		GameUtil.FloodFillConditional(pooledQueue, func, pooledHashSet2, pooledHashSet, 10);
		float num6 = ((num3 > 0) ? (this.addTileMass / (float)this.addTiles) : 1f);
		int num7 = this.addDiseaseCount / num3;
		if (element.HasTag(GameTags.Unstable))
		{
			UnstableGroundManager component = World.Instance.GetComponent<UnstableGroundManager>();
			using (HashSet<int>.Enumerator enumerator = pooledHashSet.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					int num8 = enumerator.Current;
					if (num3 <= 0)
					{
						break;
					}
					component.Spawn(num8, element, num6, temperature, byte.MaxValue, 0);
					num3--;
				}
				goto IL_0229;
			}
		}
		foreach (int num9 in pooledHashSet)
		{
			if (num3 <= 0)
			{
				break;
			}
			SimMessages.AddRemoveSubstance(num9, element.id, CellEventLogger.Instance.ElementEmitted, num6, temperature, this.diseaseIdx, num7, true, -1);
			num3--;
		}
		IL_0229:
		pooledHashSet.Recycle();
		pooledHashSet2.Recycle();
		pooledQueue.Recycle();
	}

	// Token: 0x06003984 RID: 14724 RVA: 0x0013EFF4 File Offset: 0x0013D1F4
	protected virtual void SpawnCraterPrefabs()
	{
		if (this.craterPrefabs != null && this.craterPrefabs.Length != 0)
		{
			GameObject gameObject = global::Util.KInstantiate(Assets.GetPrefab(this.craterPrefabs[global::UnityEngine.Random.Range(0, this.craterPrefabs.Length)]), Grid.CellToPos(Grid.PosToCell(this)));
			gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -19.5f);
			gameObject.SetActive(true);
		}
	}

	// Token: 0x06003985 RID: 14725 RVA: 0x0013F080 File Offset: 0x0013D280
	protected int GetDepthOfElement(int cell, Element element, int world)
	{
		int num = 0;
		int num2 = Grid.CellBelow(cell);
		while (Grid.IsValidCellInWorld(num2, world) && Grid.Element[num2] == element)
		{
			num++;
			num2 = Grid.CellBelow(num2);
		}
		return num;
	}

	// Token: 0x06003986 RID: 14726 RVA: 0x0013F0B8 File Offset: 0x0013D2B8
	[ContextMenu("DamageTiles")]
	private float DamageTiles(int cell, int prev_cell, float input_damage)
	{
		GameObject gameObject = Grid.Objects[cell, 9];
		float num = 1f;
		bool flag = false;
		if (gameObject != null)
		{
			if (gameObject.GetComponent<KPrefabID>().HasTag(GameTags.Window))
			{
				num = this.windowDamageMultiplier;
			}
			else if (gameObject.GetComponent<KPrefabID>().HasTag(GameTags.Bunker))
			{
				num = this.bunkerDamageMultiplier;
				if (gameObject.GetComponent<Door>() != null)
				{
					Game.Instance.savedInfo.blockedCometWithBunkerDoor = true;
				}
			}
			SimCellOccupier component = gameObject.GetComponent<SimCellOccupier>();
			if (component != null && !component.doReplaceElement)
			{
				flag = true;
			}
		}
		Element element;
		if (flag)
		{
			element = gameObject.GetComponent<PrimaryElement>().Element;
		}
		else
		{
			element = Grid.Element[cell];
		}
		if (element.strength == 0f)
		{
			return 0f;
		}
		float num2 = input_damage * num / element.strength;
		this.PlayTileDamageSound(element, Grid.CellToPos(cell), gameObject);
		if (num2 == 0f)
		{
			return 0f;
		}
		float num5;
		if (flag)
		{
			BuildingHP component2 = gameObject.GetComponent<BuildingHP>();
			float num3 = (float)component2.HitPoints / (float)component2.MaxHitPoints;
			float num4 = num2 * (float)component2.MaxHitPoints;
			component2.gameObject.Trigger(-794517298, new BuildingHP.DamageSourceInfo
			{
				damage = Mathf.RoundToInt(num4),
				source = BUILDINGS.DAMAGESOURCES.COMET,
				popString = UI.GAMEOBJECTEFFECTS.DAMAGE_POPS.COMET
			});
			num5 = Mathf.Min(num3, num2);
		}
		else
		{
			num5 = WorldDamage.Instance.ApplyDamage(cell, num2, prev_cell, BUILDINGS.DAMAGESOURCES.COMET, UI.GAMEOBJECTEFFECTS.DAMAGE_POPS.COMET);
		}
		this.destroyedCells.Add(cell);
		float num6 = num5 / num2;
		return input_damage * (1f - num6);
	}

	// Token: 0x06003987 RID: 14727 RVA: 0x0013F270 File Offset: 0x0013D470
	private void DamageThings(Vector3 pos, int cell, int damage, GameObject ignoreObject = null)
	{
		if (damage == 0 || !Grid.IsValidCell(cell))
		{
			return;
		}
		GameObject gameObject = Grid.Objects[cell, 1];
		if (gameObject != null && gameObject != ignoreObject)
		{
			BuildingHP component = gameObject.GetComponent<BuildingHP>();
			Building component2 = gameObject.GetComponent<Building>();
			if (component != null && !this.damagedEntities.Contains(gameObject))
			{
				float num = (gameObject.GetComponent<KPrefabID>().HasTag(GameTags.Bunker) ? ((float)damage * this.bunkerDamageMultiplier) : ((float)damage));
				if (component2 != null && component2.Def != null)
				{
					this.PlayBuildingDamageSound(component2.Def, Grid.CellToPos(cell), gameObject);
				}
				component.gameObject.Trigger(-794517298, new BuildingHP.DamageSourceInfo
				{
					damage = Mathf.RoundToInt(num),
					source = BUILDINGS.DAMAGESOURCES.COMET,
					popString = UI.GAMEOBJECTEFFECTS.DAMAGE_POPS.COMET
				});
				this.damagedEntities.Add(gameObject);
			}
		}
		ListPool<ScenePartitionerEntry, Comet>.PooledList pooledList = ListPool<ScenePartitionerEntry, Comet>.Allocate();
		GameScenePartitioner.Instance.GatherEntries((int)pos.x, (int)pos.y, 1, 1, GameScenePartitioner.Instance.pickupablesLayer, pooledList);
		foreach (ScenePartitionerEntry scenePartitionerEntry in pooledList)
		{
			Pickupable pickupable = scenePartitionerEntry.obj as Pickupable;
			Health component3 = pickupable.GetComponent<Health>();
			if (component3 != null && !this.damagedEntities.Contains(pickupable.gameObject))
			{
				float num2 = (pickupable.KPrefabID.HasTag(GameTags.Bunker) ? ((float)damage * this.bunkerDamageMultiplier) : ((float)damage));
				component3.Damage(num2);
				this.damagedEntities.Add(pickupable.gameObject);
			}
		}
		pooledList.Recycle();
	}

	// Token: 0x06003988 RID: 14728 RVA: 0x0013F45C File Offset: 0x0013D65C
	public float GetDistanceFromImpact()
	{
		float num = this.velocity.x / this.velocity.y;
		Vector3 position = base.transform.GetPosition();
		float num2 = 0f;
		while (num2 > -6f)
		{
			num2 -= 1f;
			num2 = Mathf.Ceil(position.y + num2) - 0.2f - position.y;
			float num3 = num2 * num;
			Vector3 vector = new Vector3(num3, num2, 0f);
			int num4 = Grid.PosToCell(position + vector);
			if (Grid.IsValidCell(num4) && Grid.Solid[num4])
			{
				return vector.magnitude;
			}
		}
		return 6f;
	}

	// Token: 0x06003989 RID: 14729 RVA: 0x0013F505 File Offset: 0x0013D705
	public float GetSoundDistance()
	{
		return this.GetDistanceFromImpact();
	}

	// Token: 0x0600398A RID: 14730 RVA: 0x0013F510 File Offset: 0x0013D710
	private void PlayTileDamageSound(Element element, Vector3 pos, GameObject tile_go)
	{
		string text = element.substance.GetMiningBreakSound();
		if (text == null)
		{
			if (element.HasTag(GameTags.RefinedMetal))
			{
				text = "RefinedMetal";
			}
			else if (element.HasTag(GameTags.Metal))
			{
				text = "RawMetal";
			}
			else
			{
				text = "Rock";
			}
		}
		text = "MeteorDamage_" + text;
		text = GlobalAssets.GetSound(text, false);
		if (CameraController.Instance && CameraController.Instance.IsAudibleSound(pos, text))
		{
			float volume = this.GetVolume(tile_go);
			KFMOD.PlayOneShot(text, CameraController.Instance.GetVerticallyScaledPosition(pos, false), volume);
		}
	}

	// Token: 0x0600398B RID: 14731 RVA: 0x0013F5AC File Offset: 0x0013D7AC
	private void PlayBuildingDamageSound(BuildingDef def, Vector3 pos, GameObject building_go)
	{
		if (def != null)
		{
			string text = GlobalAssets.GetSound(StringFormatter.Combine("MeteorDamage_Building_", def.AudioCategory), false);
			if (text == null)
			{
				text = GlobalAssets.GetSound("MeteorDamage_Building_Metal", false);
			}
			if (text != null && CameraController.Instance && CameraController.Instance.IsAudibleSound(pos, text))
			{
				float volume = this.GetVolume(building_go);
				KFMOD.PlayOneShot(text, CameraController.Instance.GetVerticallyScaledPosition(pos, false), volume);
			}
		}
	}

	// Token: 0x0600398C RID: 14732 RVA: 0x0013F628 File Offset: 0x0013D828
	public void Sim33ms(float dt)
	{
		if (this.hasExploded)
		{
			return;
		}
		if (this.offsetPosition.y > 0f)
		{
			Vector3 vector = new Vector3(this.velocity.x * dt, this.velocity.y * dt, 0f);
			Vector3 vector2 = this.offsetPosition + vector;
			this.offsetPosition = vector2;
			this.anim.Offset = this.offsetPosition;
		}
		else
		{
			if (this.anim.Offset != Vector3.zero)
			{
				this.anim.Offset = Vector3.zero;
			}
			if (!this.selectable.enabled)
			{
				this.selectable.enabled = true;
			}
			Vector2 vector3 = new Vector2((float)Grid.WidthInCells, (float)Grid.HeightInCells) * -0.1f;
			Vector2 vector4 = new Vector2((float)Grid.WidthInCells, (float)Grid.HeightInCells) * 1.1f;
			Vector3 position = base.transform.GetPosition();
			Vector3 vector5 = position + new Vector3(this.velocity.x * dt, this.velocity.y * dt, 0f);
			int num = Grid.PosToCell(vector5);
			this.loopingSounds.UpdateVelocity(this.flyingSound, vector5 - position);
			Element element = ElementLoader.FindElementByHash(this.EXHAUST_ELEMENT);
			if (this.EXHAUST_ELEMENT != SimHashes.Void && Grid.IsValidCell(num) && !Grid.Solid[num])
			{
				SimMessages.EmitMass(num, element.idx, dt * this.EXHAUST_RATE, element.defaultValues.temperature, this.diseaseIdx, Mathf.RoundToInt((float)this.addDiseaseCount * dt), -1);
			}
			if (vector5.x < vector3.x || vector4.x < vector5.x || vector5.y < vector3.y)
			{
				global::Util.KDestroyGameObject(base.gameObject);
			}
			int num2 = Grid.PosToCell(this);
			int num3 = Grid.PosToCell(this.previousPosition);
			if (num2 != num3)
			{
				if (Grid.IsValidCell(num2) && Grid.Solid[num2])
				{
					PrimaryElement component = base.GetComponent<PrimaryElement>();
					this.remainingTileDamage = this.DamageTiles(num2, num3, this.remainingTileDamage);
					if (this.remainingTileDamage <= 0f)
					{
						this.Explode(position, num2, num3, component.Element);
						this.hasExploded = true;
						if (this.destroyOnExplode)
						{
							global::Util.KDestroyGameObject(base.gameObject);
						}
						return;
					}
				}
				else
				{
					GameObject gameObject = ((this.ignoreObstacleForDamage.Get() == null) ? null : this.ignoreObstacleForDamage.Get().gameObject);
					this.DamageThings(position, num2, this.entityDamage, gameObject);
				}
			}
			if (this.canHitDuplicants && this.age > 0.25f && Grid.Objects[Grid.PosToCell(position), 0] != null)
			{
				base.transform.position = Grid.CellToPos(Grid.PosToCell(position));
				this.Explode(position, num2, num3, base.GetComponent<PrimaryElement>().Element);
				if (this.destroyOnExplode)
				{
					global::Util.KDestroyGameObject(base.gameObject);
				}
				return;
			}
			this.previousPosition = position;
			base.transform.SetPosition(vector5);
		}
		this.age += dt;
	}

	// Token: 0x0600398D RID: 14733 RVA: 0x0013F978 File Offset: 0x0013DB78
	private void PlayImpactSound(Vector3 pos)
	{
		if (this.impactSound == null)
		{
			this.impactSound = "Meteor_Large_Impact";
		}
		this.loopingSounds.StopSound(this.flyingSound);
		string sound = GlobalAssets.GetSound(this.impactSound, false);
		int num = Grid.PosToCell(pos);
		if (Grid.IsValidCell(num) && (int)Grid.WorldIdx[num] == ClusterManager.Instance.activeWorldId)
		{
			float volume = this.GetVolume(base.gameObject);
			pos.z = 0f;
			EventInstance eventInstance = KFMOD.BeginOneShot(sound, pos, volume);
			eventInstance.setParameterByName("userVolume_SFX", KPlayerPrefs.GetFloat("Volume_SFX"), false);
			KFMOD.EndOneShot(eventInstance);
		}
	}

	// Token: 0x0600398E RID: 14734 RVA: 0x0013FA19 File Offset: 0x0013DC19
	private void StartLoopingSound()
	{
		this.loopingSounds.StartSound(this.flyingSound);
		this.loopingSounds.UpdateFirstParameter(this.flyingSound, this.FLYING_SOUND_ID_PARAMETER, (float)this.flyingSoundID);
	}

	// Token: 0x0600398F RID: 14735 RVA: 0x0013FA4C File Offset: 0x0013DC4C
	public void Explode()
	{
		PrimaryElement component = base.GetComponent<PrimaryElement>();
		Vector3 position = base.transform.GetPosition();
		int num = Grid.PosToCell(position);
		this.Explode(position, num, num, component.Element);
		this.hasExploded = true;
		if (this.destroyOnExplode)
		{
			global::Util.KDestroyGameObject(base.gameObject);
		}
	}

	// Token: 0x040022BB RID: 8891
	public SimHashes EXHAUST_ELEMENT = SimHashes.CarbonDioxide;

	// Token: 0x040022BC RID: 8892
	public float EXHAUST_RATE = 50f;

	// Token: 0x040022BD RID: 8893
	public Vector2 spawnVelocity = new Vector2(12f, 15f);

	// Token: 0x040022BE RID: 8894
	public Vector2 spawnAngle = new Vector2(-100f, -80f);

	// Token: 0x040022BF RID: 8895
	public Vector2 massRange;

	// Token: 0x040022C0 RID: 8896
	public Vector2 temperatureRange;

	// Token: 0x040022C1 RID: 8897
	public SpawnFXHashes explosionEffectHash;

	// Token: 0x040022C2 RID: 8898
	public int splashRadius = 1;

	// Token: 0x040022C3 RID: 8899
	public int addTiles;

	// Token: 0x040022C4 RID: 8900
	public int addTilesMinHeight;

	// Token: 0x040022C5 RID: 8901
	public int addTilesMaxHeight;

	// Token: 0x040022C6 RID: 8902
	public int entityDamage = 1;

	// Token: 0x040022C7 RID: 8903
	public float totalTileDamage = 0.2f;

	// Token: 0x040022C8 RID: 8904
	protected float addTileMass;

	// Token: 0x040022C9 RID: 8905
	public int addDiseaseCount;

	// Token: 0x040022CA RID: 8906
	public byte diseaseIdx = byte.MaxValue;

	// Token: 0x040022CB RID: 8907
	public Vector2 elementReplaceTileTemperatureRange = new Vector2(800f, 1000f);

	// Token: 0x040022CC RID: 8908
	public Vector2I explosionOreCount = new Vector2I(0, 0);

	// Token: 0x040022CD RID: 8909
	private float explosionMass;

	// Token: 0x040022CE RID: 8910
	public Vector2 explosionTemperatureRange = new Vector2(500f, 700f);

	// Token: 0x040022CF RID: 8911
	public Vector2 explosionSpeedRange = new Vector2(8f, 14f);

	// Token: 0x040022D0 RID: 8912
	public float windowDamageMultiplier = 5f;

	// Token: 0x040022D1 RID: 8913
	public float bunkerDamageMultiplier;

	// Token: 0x040022D2 RID: 8914
	public string impactSound;

	// Token: 0x040022D3 RID: 8915
	public string flyingSound;

	// Token: 0x040022D4 RID: 8916
	public int flyingSoundID;

	// Token: 0x040022D5 RID: 8917
	private HashedString FLYING_SOUND_ID_PARAMETER = "meteorType";

	// Token: 0x040022D6 RID: 8918
	public bool affectedByDifficulty = true;

	// Token: 0x040022D7 RID: 8919
	public bool Targeted;

	// Token: 0x040022D8 RID: 8920
	[Serialize]
	protected Vector3 offsetPosition;

	// Token: 0x040022D9 RID: 8921
	[Serialize]
	protected Vector2 velocity;

	// Token: 0x040022DA RID: 8922
	[Serialize]
	private float remainingTileDamage;

	// Token: 0x040022DB RID: 8923
	private Vector3 previousPosition;

	// Token: 0x040022DC RID: 8924
	private bool hasExploded;

	// Token: 0x040022DD RID: 8925
	public bool canHitDuplicants;

	// Token: 0x040022DE RID: 8926
	public string[] craterPrefabs;

	// Token: 0x040022DF RID: 8927
	public string[] lootOnDestroyedByMissile;

	// Token: 0x040022E0 RID: 8928
	public bool destroyOnExplode = true;

	// Token: 0x040022E1 RID: 8929
	public bool spawnWithOffset;

	// Token: 0x040022E2 RID: 8930
	private float age;

	// Token: 0x040022E3 RID: 8931
	public global::System.Action OnImpact;

	// Token: 0x040022E4 RID: 8932
	public Ref<KPrefabID> ignoreObstacleForDamage = new Ref<KPrefabID>();

	// Token: 0x040022E5 RID: 8933
	[MyCmpGet]
	private KBatchedAnimController anim;

	// Token: 0x040022E6 RID: 8934
	[MyCmpGet]
	private KSelectable selectable;

	// Token: 0x040022E7 RID: 8935
	public Tag typeID;

	// Token: 0x040022E8 RID: 8936
	private LoopingSounds loopingSounds;

	// Token: 0x040022E9 RID: 8937
	private List<GameObject> damagedEntities = new List<GameObject>();

	// Token: 0x040022EA RID: 8938
	private List<int> destroyedCells = new List<int>();

	// Token: 0x040022EB RID: 8939
	private const float MAX_DISTANCE_TEST = 6f;
}
