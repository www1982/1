using System;
using System.Diagnostics;
using Klei.AI;
using UnityEngine;

// Token: 0x020005A4 RID: 1444
public class SolidConsumerMonitor : GameStateMachine<SolidConsumerMonitor, SolidConsumerMonitor.Instance, IStateMachineTarget, SolidConsumerMonitor.Def>
{
	// Token: 0x060020F6 RID: 8438 RVA: 0x000BDFD8 File Offset: 0x000BC1D8
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.satisfied;
		this.root.EventHandler(GameHashes.EatSolidComplete, delegate(SolidConsumerMonitor.Instance smi, object data)
		{
			smi.OnEatSolidComplete(data);
		}).ToggleBehaviour(GameTags.Creatures.WantsToEat, (SolidConsumerMonitor.Instance smi) => smi.targetEdible != null && !smi.targetEdible.HasTag(GameTags.Creatures.ReservedByCreature), null);
		this.satisfied.TagTransition(GameTags.Creatures.Hungry, this.lookingforfood, false);
		this.lookingforfood.TagTransition(GameTags.Creatures.Hungry, this.satisfied, true).PreBrainUpdate(new Action<SolidConsumerMonitor.Instance>(SolidConsumerMonitor.FindFood));
	}

	// Token: 0x060020F7 RID: 8439 RVA: 0x000BE088 File Offset: 0x000BC288
	[Conditional("DETAILED_SOLID_CONSUMER_MONITOR_PROFILE")]
	private static void BeginDetailedSample(string region_name)
	{
	}

	// Token: 0x060020F8 RID: 8440 RVA: 0x000BE08A File Offset: 0x000BC28A
	[Conditional("DETAILED_SOLID_CONSUMER_MONITOR_PROFILE")]
	private static void EndDetailedSample(string region_name)
	{
	}

	// Token: 0x060020F9 RID: 8441 RVA: 0x000BE08C File Offset: 0x000BC28C
	private static void FindFood(SolidConsumerMonitor.Instance smi)
	{
		if (smi.IsTargetEdibleValid())
		{
			return;
		}
		smi.ClearTargetEdible();
		Diet diet = smi.diet;
		int num = 0;
		int num2 = 0;
		Grid.PosToXY(smi.gameObject.transform.GetPosition(), out num, out num2);
		num -= 8;
		num2 -= 8;
		bool flag = false;
		if (diet.CanEatPreyCritter)
		{
			CavityInfo cavityForCell = Game.Instance.roomProber.GetCavityForCell(Grid.PosToCell(smi));
			KPrefabID kprefabID = null;
			int num3 = int.MaxValue;
			if (cavityForCell != null)
			{
				foreach (KPrefabID kprefabID2 in cavityForCell.creatures)
				{
					if (!kprefabID2.HasTag(GameTags.Creatures.ReservedByCreature) && diet.GetDietInfo(kprefabID2.PrefabTag) != null)
					{
						int cost = smi.GetCost(kprefabID2.gameObject);
						if (cost != -1 && (cost < num3 || num3 == -1))
						{
							kprefabID = kprefabID2;
							num3 = cost;
						}
					}
					if (kprefabID != null && num3 < 3)
					{
						break;
					}
				}
			}
			if (kprefabID != null)
			{
				smi.SetTargetEdible(kprefabID.gameObject, num3);
				smi.targetEdibleOffset = smi.GetBestEdibleOffset(kprefabID.gameObject);
				flag = true;
			}
		}
		bool flag2 = false;
		if (!flag && diet.CanEatAnySolid)
		{
			ListPool<Storage, SolidConsumerMonitor>.PooledList pooledList = ListPool<Storage, SolidConsumerMonitor>.Allocate();
			int num4 = 32;
			foreach (CreatureFeeder creatureFeeder in Components.CreatureFeeders.GetItems(smi.GetMyWorldId()))
			{
				Vector2I targetFeederCell = creatureFeeder.GetTargetFeederCell();
				if (targetFeederCell.x >= num && targetFeederCell.x <= num + num4 && targetFeederCell.y >= num2 && targetFeederCell.y <= num2 + num4 && !creatureFeeder.StoragesAreEmpty())
				{
					int cost2 = smi.GetCost(Grid.XYToCell(targetFeederCell.x, targetFeederCell.y));
					if (smi.IsCloserThanTargetEdible(cost2))
					{
						foreach (Storage storage in creatureFeeder.storages)
						{
							if (!(storage == null) && !storage.IsEmpty() && smi.GetCost(Grid.PosToCell(storage.items[0])) != -1)
							{
								foreach (GameObject gameObject in storage.items)
								{
									if (!(gameObject == null))
									{
										KPrefabID component = gameObject.GetComponent<KPrefabID>();
										if (!component.HasAnyTags(SolidConsumerMonitor.creatureTags) && diet.GetDietInfo(component.PrefabTag) != null)
										{
											smi.SetTargetEdible(gameObject, cost2);
											smi.targetEdibleOffset = Vector3.zero;
											flag2 = true;
											break;
										}
									}
								}
								if (flag2)
								{
									break;
								}
							}
						}
					}
				}
			}
			pooledList.Recycle();
		}
		bool flag3 = false;
		if (!flag && !flag2 && diet.CanEatAnyPlantDirectly)
		{
			ListPool<ScenePartitionerEntry, GameScenePartitioner>.PooledList pooledList2 = ListPool<ScenePartitionerEntry, GameScenePartitioner>.Allocate();
			GameScenePartitioner.Instance.GatherEntries(num, num2, 16, 16, GameScenePartitioner.Instance.plants, pooledList2);
			foreach (ScenePartitionerEntry scenePartitionerEntry in pooledList2)
			{
				KPrefabID kprefabID3 = (KPrefabID)scenePartitionerEntry.obj;
				Diet.Info dietInfo = diet.GetDietInfo(kprefabID3.PrefabTag);
				Vector3 vector = kprefabID3.transform.GetPosition();
				bool flag4 = kprefabID3.HasTag(GameTags.PlantedOnFloorVessel);
				if (flag4)
				{
					vector += SolidConsumerMonitor.PLANT_ON_FLOOR_VESSEL_OFFSET;
				}
				int num5 = smi.GetCost(Grid.PosToCell(vector));
				Vector3 vector2 = Vector3.zero;
				if (smi.IsCloserThanTargetEdible(num5) && !kprefabID3.HasAnyTags(SolidConsumerMonitor.creatureTags) && dietInfo != null)
				{
					if (kprefabID3.HasTag(GameTags.Plant))
					{
						IPlantConsumptionInstructions[] plantConsumptionInstructions = GameUtil.GetPlantConsumptionInstructions(kprefabID3.gameObject);
						if (plantConsumptionInstructions == null || plantConsumptionInstructions.Length == 0)
						{
							continue;
						}
						bool flag5 = false;
						foreach (IPlantConsumptionInstructions plantConsumptionInstructions2 in plantConsumptionInstructions)
						{
							if (plantConsumptionInstructions2.CanPlantBeEaten() && dietInfo.foodType == plantConsumptionInstructions2.GetDietFoodType())
							{
								CellOffset[] allowedOffsets = plantConsumptionInstructions2.GetAllowedOffsets();
								if (allowedOffsets != null)
								{
									num5 = -1;
									foreach (CellOffset cellOffset in allowedOffsets)
									{
										int cost3 = smi.GetCost(Grid.OffsetCell(Grid.PosToCell(vector), cellOffset));
										if (cost3 != -1 && (num5 == -1 || cost3 < num5))
										{
											num5 = cost3;
											vector2 = cellOffset.ToVector3();
										}
									}
									if (num5 != -1)
									{
										flag5 = true;
										break;
									}
								}
								else
								{
									flag5 = true;
								}
							}
						}
						if (!flag5)
						{
							continue;
						}
					}
					smi.SetTargetEdible(kprefabID3.gameObject, num5);
					smi.targetEdibleOffset = vector2 + (flag4 ? SolidConsumerMonitor.PLANT_ON_FLOOR_VESSEL_OFFSET : Vector3.zero);
					flag3 = true;
				}
			}
			pooledList2.Recycle();
		}
		if (!flag && !flag2 && !flag3 && diet.CanEatAnySolid)
		{
			bool flag6 = false;
			ListPool<ScenePartitionerEntry, GameScenePartitioner>.PooledList pooledList3 = ListPool<ScenePartitionerEntry, GameScenePartitioner>.Allocate();
			GameScenePartitioner.Instance.GatherEntries(num, num2, 16, 16, GameScenePartitioner.Instance.pickupablesLayer, pooledList3);
			foreach (ScenePartitionerEntry scenePartitionerEntry2 in pooledList3)
			{
				Pickupable pickupable = (Pickupable)scenePartitionerEntry2.obj;
				KPrefabID kprefabID4 = pickupable.KPrefabID;
				if (!kprefabID4.HasAnyTags(SolidConsumerMonitor.creatureTags) && diet.GetDietInfo(kprefabID4.PrefabTag) != null)
				{
					bool flag7;
					smi.ProcessEdible(pickupable.gameObject, out flag7);
					smi.targetEdibleOffset = Vector3.zero;
					flag6 = flag6 || flag7;
				}
			}
			pooledList3.Recycle();
		}
	}

	// Token: 0x0400132F RID: 4911
	public static Vector3 PLANT_ON_FLOOR_VESSEL_OFFSET = Vector3.down;

	// Token: 0x04001330 RID: 4912
	private GameStateMachine<SolidConsumerMonitor, SolidConsumerMonitor.Instance, IStateMachineTarget, SolidConsumerMonitor.Def>.State satisfied;

	// Token: 0x04001331 RID: 4913
	private GameStateMachine<SolidConsumerMonitor, SolidConsumerMonitor.Instance, IStateMachineTarget, SolidConsumerMonitor.Def>.State lookingforfood;

	// Token: 0x04001332 RID: 4914
	private static Tag[] creatureTags = new Tag[]
	{
		GameTags.Creatures.ReservedByCreature,
		GameTags.CreatureBrain
	};

	// Token: 0x0200142F RID: 5167
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04006BD4 RID: 27604
		public Diet diet;

		// Token: 0x04006BD5 RID: 27605
		public Vector3[] possibleEatPositionOffsets = new Vector3[] { Vector3.zero };

		// Token: 0x04006BD6 RID: 27606
		public Vector2 navigatorSize = Vector2.one;
	}

	// Token: 0x02001430 RID: 5168
	public new class Instance : GameStateMachine<SolidConsumerMonitor, SolidConsumerMonitor.Instance, IStateMachineTarget, SolidConsumerMonitor.Def>.GameInstance
	{
		// Token: 0x06008CBF RID: 36031 RVA: 0x00356B7F File Offset: 0x00354D7F
		public Instance(IStateMachineTarget master, SolidConsumerMonitor.Def def)
			: base(master, def)
		{
			this.diet = DietManager.Instance.GetPrefabDiet(base.gameObject);
		}

		// Token: 0x06008CC0 RID: 36032 RVA: 0x00356B9F File Offset: 0x00354D9F
		public bool CanSearchForPickupables(bool foodAtFeeder)
		{
			return !foodAtFeeder;
		}

		// Token: 0x06008CC1 RID: 36033 RVA: 0x00356BA5 File Offset: 0x00354DA5
		public bool IsCloserThanTargetEdible(int cost)
		{
			return cost != -1 && (cost < this.targetEdibleCost || this.targetEdibleCost == -1);
		}

		// Token: 0x06008CC2 RID: 36034 RVA: 0x00356BC4 File Offset: 0x00354DC4
		public bool IsTargetEdibleValid()
		{
			if (this.targetEdible == null)
			{
				return false;
			}
			int cost = this.GetCost(Grid.PosToCell(this.targetEdible.transform.GetPosition() + this.targetEdibleOffset));
			return cost != -1 && cost <= this.targetEdibleCost + 4;
		}

		// Token: 0x06008CC3 RID: 36035 RVA: 0x00356C1A File Offset: 0x00354E1A
		public void ClearTargetEdible()
		{
			this.targetEdibleCost = -1;
			this.targetEdible = null;
			this.targetEdibleOffset = Vector3.zero;
		}

		// Token: 0x06008CC4 RID: 36036 RVA: 0x00356C38 File Offset: 0x00354E38
		public bool ProcessEdible(GameObject edible, out bool isReachable)
		{
			int cost = this.GetCost(edible);
			isReachable = cost != -1;
			if (cost != -1 && (cost < this.targetEdibleCost || this.targetEdibleCost == -1))
			{
				this.SetTargetEdible(edible, cost);
				return true;
			}
			return false;
		}

		// Token: 0x06008CC5 RID: 36037 RVA: 0x00356C77 File Offset: 0x00354E77
		public void SetTargetEdible(GameObject gameObject, int cost)
		{
			if (this.targetEdible == gameObject)
			{
				return;
			}
			this.targetEdibleCost = cost;
			this.targetEdible = gameObject;
		}

		// Token: 0x06008CC6 RID: 36038 RVA: 0x00356C96 File Offset: 0x00354E96
		public int GetCost(GameObject edible)
		{
			return this.GetCost(Grid.PosToCell(edible.transform.GetPosition() + base.smi.GetBestEdibleOffset(edible)));
		}

		// Token: 0x06008CC7 RID: 36039 RVA: 0x00356CC0 File Offset: 0x00354EC0
		public int GetCost(int cell)
		{
			if (this.drowningMonitor != null && this.drowningMonitor.canDrownToDeath && !this.drowningMonitor.livesUnderWater && !this.drowningMonitor.IsCellSafe(cell))
			{
				return -1;
			}
			return this.navigator.GetNavigationCost(cell);
		}

		// Token: 0x06008CC8 RID: 36040 RVA: 0x00356D18 File Offset: 0x00354F18
		public void OnEatSolidComplete(object data)
		{
			KPrefabID kprefabID = data as KPrefabID;
			if (kprefabID == null)
			{
				return;
			}
			PrimaryElement component = kprefabID.GetComponent<PrimaryElement>();
			if (component == null)
			{
				return;
			}
			Diet.Info dietInfo = this.diet.GetDietInfo(kprefabID.PrefabTag);
			if (dietInfo == null)
			{
				return;
			}
			AmountInstance amountInstance = Db.Get().Amounts.Calories.Lookup(base.smi.gameObject);
			string properName = kprefabID.GetProperName();
			PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Negative, properName, kprefabID.transform, 1.5f, false);
			float num = amountInstance.GetMax() - amountInstance.value;
			float num2 = dietInfo.ConvertCaloriesToConsumptionMass(num);
			IPlantConsumptionInstructions plantConsumptionInstructions = null;
			foreach (IPlantConsumptionInstructions plantConsumptionInstructions3 in GameUtil.GetPlantConsumptionInstructions(kprefabID.gameObject))
			{
				if (dietInfo.foodType == plantConsumptionInstructions3.GetDietFoodType())
				{
					plantConsumptionInstructions = plantConsumptionInstructions3;
				}
			}
			float num3;
			if (plantConsumptionInstructions != null)
			{
				num2 = plantConsumptionInstructions.ConsumePlant(num2);
				num3 = dietInfo.ConvertConsumptionMassToCalories(num2);
			}
			else if (dietInfo.foodType == Diet.Info.FoodType.EatPrey || dietInfo.foodType == Diet.Info.FoodType.EatButcheredPrey)
			{
				float num4 = this.diet.AvailableCaloriesInPrey(kprefabID.PrefabTag);
				float num5 = Mathf.Clamp(1f - num / num4, 0f, 1f);
				if (num5 > 0f)
				{
					Butcherable component2 = kprefabID.GetComponent<Butcherable>();
					if (component2 != null)
					{
						component2.CreateDrops(num5);
					}
				}
				component.Mass = 0f;
				num3 = Mathf.Min(num, num4);
			}
			else
			{
				num2 = Mathf.Min(num2, component.Mass);
				component.Mass -= num2;
				Pickupable component3 = component.GetComponent<Pickupable>();
				if (component3.storage != null)
				{
					component3.storage.Trigger(-1452790913, base.gameObject);
					component3.storage.Trigger(-1697596308, base.gameObject);
				}
				num3 = dietInfo.ConvertConsumptionMassToCalories(num2);
			}
			CreatureCalorieMonitor.CaloriesConsumedEvent caloriesConsumedEvent = new CreatureCalorieMonitor.CaloriesConsumedEvent
			{
				tag = kprefabID.PrefabTag,
				calories = num3
			};
			base.Trigger(-2038961714, caloriesConsumedEvent);
			this.targetEdible = null;
		}

		// Token: 0x06008CC9 RID: 36041 RVA: 0x00356F49 File Offset: 0x00355149
		public string[] GetTargetEdibleEatAnims()
		{
			return this.diet.GetDietInfo(this.targetEdible.PrefabID()).eatAnims;
		}

		// Token: 0x06008CCA RID: 36042 RVA: 0x00356F68 File Offset: 0x00355168
		public Vector3 GetBestEdibleOffset(GameObject edible)
		{
			int num = int.MaxValue;
			Vector3 vector = Vector3.zero;
			foreach (Vector3 vector2 in base.def.possibleEatPositionOffsets)
			{
				Vector3 vector3 = edible.transform.position + vector2;
				if (vector2.x > 0f)
				{
					vector3 += new Vector3(base.def.navigatorSize.x / 2f, 0f, 0f);
				}
				else if (vector2.x < 0f)
				{
					vector3 -= new Vector3(base.def.navigatorSize.x / 2f, 0f, 0f);
				}
				if (vector2.y > 0f)
				{
					vector3 += new Vector3(0f, base.def.navigatorSize.y / 2f, 0f);
				}
				else if (vector2.y < 0f)
				{
					vector3 -= new Vector3(0f, base.def.navigatorSize.y / 2f, 0f);
				}
				int navigationCost = this.navigator.GetNavigationCost(Grid.PosToCell(vector3));
				if (navigationCost != -1 && navigationCost < num)
				{
					num = navigationCost;
					vector = vector2;
				}
			}
			return vector;
		}

		// Token: 0x04006BD7 RID: 27607
		private const int RECALC_THRESHOLD = 4;

		// Token: 0x04006BD8 RID: 27608
		public GameObject targetEdible;

		// Token: 0x04006BD9 RID: 27609
		public Vector3 targetEdibleOffset;

		// Token: 0x04006BDA RID: 27610
		private int targetEdibleCost;

		// Token: 0x04006BDB RID: 27611
		[MyCmpGet]
		private Navigator navigator;

		// Token: 0x04006BDC RID: 27612
		[MyCmpGet]
		private DrowningMonitor drowningMonitor;

		// Token: 0x04006BDD RID: 27613
		public Diet diet;
	}
}
