using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x02000A71 RID: 2673
public class VineBranch : PlantBranchGrowerBase<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>
{
	// Token: 0x06004D4C RID: 19788 RVA: 0x001BF967 File Offset: 0x001BDB67
	public static bool IsCellFoundation(int cell)
	{
		return Grid.IsSolidCell(cell) || Grid.HasDoor[cell];
	}

	// Token: 0x06004D4D RID: 19789 RVA: 0x001BF980 File Offset: 0x001BDB80
	public static bool IsCellAvailable(GameObject questionerObj, int cell, Func<int, bool> foundationCheckFunction = null)
	{
		int num = Grid.PosToCell(questionerObj);
		int num2 = (int)Grid.WorldIdx[num];
		return cell != Grid.InvalidCell && (int)Grid.WorldIdx[cell] == num2 && ((foundationCheckFunction == null) ? (!VineBranch.IsCellFoundation(cell)) : (!foundationCheckFunction(cell))) && !Grid.IsLiquid(cell) && Grid.Objects[cell, 1] == null && Grid.Objects[cell, 5] == null;
	}

	// Token: 0x06004D4E RID: 19790 RVA: 0x001BF9F8 File Offset: 0x001BDBF8
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.undevelopedBranch;
		this.undevelopedBranch.InitializeStates(this.masterTarget, this.Mother, this.dead, this.DieSignal).ParamTransition<bool>(this.MarkedForDeath, this.dead, GameStateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.IsTrue).ParamTransition<GameObject>(this.Mother, this.dead, GameStateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.IsNull)
			.EventTransition(GameHashes.Grow, this.mature, (VineBranch.Instance smi) => smi.IsGrown)
			.UpdateTransition(this.mature, (VineBranch.Instance smi, float dt) => smi.IsGrown, UpdateRate.SIM_4000ms, false)
			.Enter(new StateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.State.Callback(VineBranch.RecalculateMyShape))
			.Enter(new StateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.State.Callback(VineBranch.SubscribreSurroundingCellChangeListeners))
			.Exit(new StateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.State.Callback(VineBranch.UnSubscribreSurroundingSolidChangesListeners))
			.DefaultState(this.undevelopedBranch.growing);
		this.undevelopedBranch.wilted.PlayAnim(new Func<VineBranch.Instance, string>(VineBranch.GetWiltAnim), KAnim.PlayMode.Loop).EventHandler(GameHashes.BranchShapeChanged, delegate(VineBranch.Instance smi)
		{
			VineBranch.RefreshAnim(smi, VineBranch.GetWiltAnim(smi), KAnim.PlayMode.Loop);
		}).EventTransition(GameHashes.WiltRecover, this.undevelopedBranch.growing, (VineBranch.Instance smi) => !smi.IsWilting);
		this.undevelopedBranch.growing.EventTransition(GameHashes.Wilt, this.undevelopedBranch.wilted, (VineBranch.Instance smi) => smi.IsWilting).PlayAnim((VineBranch.Instance smi) => smi.Anims.grow, KAnim.PlayMode.Paused).EventHandler(GameHashes.BranchShapeChanged, delegate(VineBranch.Instance smi)
		{
			VineBranch.RefreshAnim(smi, smi.Anims.grow, KAnim.PlayMode.Paused);
		})
			.ToggleStatusItem(Db.Get().CreatureStatusItems.Growing, (VineBranch.Instance smi) => smi)
			.Enter(new StateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.State.Callback(VineBranch.RefreshPositionPercent))
			.Update(new Action<VineBranch.Instance, float>(VineBranch.RefreshPositionPercent), UpdateRate.SIM_4000ms, false)
			.EventHandler(GameHashes.ConsumePlant, new StateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.State.Callback(VineBranch.RefreshPositionPercent))
			.DefaultState(this.undevelopedBranch.growing.wild);
		this.undevelopedBranch.growing.wild.ParamTransition<bool>(this.WildPlanted, this.undevelopedBranch.growing.domestic, GameStateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.IsFalse).ToggleAttributeModifier("Growing", (VineBranch.Instance smi) => smi.wildGrowingRate, null);
		this.undevelopedBranch.growing.domestic.ParamTransition<bool>(this.WildPlanted, this.undevelopedBranch.growing.wild, GameStateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.IsTrue).ToggleAttributeModifier("Growing", (VineBranch.Instance smi) => smi.baseGrowingRate, null);
		this.mature.InitializeStates(this.masterTarget, this.Mother, this.dead, this.DieSignal).ParamTransition<bool>(this.MarkedForDeath, this.dead, GameStateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.IsTrue).ParamTransition<GameObject>(this.Mother, this.dead, GameStateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.IsNull)
			.Enter(new StateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.State.Callback(VineBranch.RecalculateShapeAndSpawnBranchesIfSpawnedByDiscovery))
			.Enter(new StateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.State.Callback(VineBranch.SetupFruitMeter))
			.Enter(new StateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.State.Callback(VineBranch.SubscribreSurroundingCellChangeListeners))
			.Exit(new StateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.State.Callback(VineBranch.UnSubscribreSurroundingSolidChangesListeners))
			.Update(new Action<VineBranch.Instance, float>(VineBranch.SpawnBranchIfPossible), UpdateRate.SIM_4000ms, false)
			.DefaultState(this.mature.healthy);
		this.mature.healthy.PlayAnim((VineBranch.Instance smi) => smi.Anims.idle, KAnim.PlayMode.Loop).EventHandler(GameHashes.BranchShapeChanged, delegate(VineBranch.Instance smi)
		{
			VineBranch.RefreshAnim(smi, smi.Anims.idle, KAnim.PlayMode.Loop);
		}).DefaultState(this.mature.healthy.growing);
		this.mature.healthy.growing.EventTransition(GameHashes.Grow, this.mature.healthy.harvestReady, (VineBranch.Instance smi) => smi.IsReadyForHarvest).UpdateTransition(this.mature.healthy.harvestReady, (VineBranch.Instance smi, float dt) => smi.IsReadyForHarvest, UpdateRate.SIM_4000ms, false).EventTransition(GameHashes.Wilt, this.mature.wilted, (VineBranch.Instance smi) => smi.IsWilting)
			.EventHandler(GameHashes.BranchShapeChanged, new StateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.State.Callback(VineBranch.RecreateFruitMeter))
			.EventHandler(GameHashes.BranchShapeChanged, new StateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.State.Callback(VineBranch.UpdateFruitMeterGrowAnimations))
			.ToggleStatusItem(Db.Get().CreatureStatusItems.GrowingFruit, (VineBranch.Instance smi) => smi)
			.Enter(new StateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.State.Callback(VineBranch.UpdateFruitMeterGrowAnimations))
			.Update(new Action<VineBranch.Instance, float>(VineBranch.UpdateFruitMeterGrowAnimations), UpdateRate.SIM_200ms, false)
			.DefaultState(this.mature.healthy.growing.wild);
		this.mature.healthy.growing.wild.ParamTransition<bool>(this.WildPlanted, this.mature.healthy.growing.domestic, GameStateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.IsFalse).ToggleAttributeModifier("Fruit Growing", (VineBranch.Instance smi) => smi.wildFruitGrowingRate, null);
		this.mature.healthy.growing.domestic.ParamTransition<bool>(this.WildPlanted, this.mature.healthy.growing.wild, GameStateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.IsTrue).ToggleAttributeModifier("Fruit Growing", (VineBranch.Instance smi) => smi.baseFruitGrowingRate, null);
		this.mature.healthy.harvestReady.ToggleTag(GameTags.FullyGrown).EventTransition(GameHashes.Harvest, this.mature.healthy.harvest, null).EventHandler(GameHashes.BranchShapeChanged, new StateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.State.Callback(VineBranch.RecreateFruitMeter))
			.EventHandler(GameHashes.BranchShapeChanged, delegate(VineBranch.Instance smi)
			{
				VineBranch.PlayAnimsOnFruit(smi, smi.Anims.meter_harvest_ready, KAnim.PlayMode.Loop);
			})
			.Enter(new StateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.State.Callback(VineBranch.MakeItHarvestable))
			.Exit(new StateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.State.Callback(VineBranch.ResetOldAge))
			.ToggleAttributeModifier("GetOld", (VineBranch.Instance smi) => smi.getOldRate, null)
			.Enter(delegate(VineBranch.Instance smi)
			{
				VineBranch.PlayAnimsOnFruit(smi, smi.Anims.meter_harvest_ready, KAnim.PlayMode.Loop);
			})
			.UpdateTransition(this.mature.healthy.selfHarvestFromOld, new Func<VineBranch.Instance, float, bool>(VineBranch.ShouldSelfHarvestFromOldAge), UpdateRate.SIM_4000ms, false);
		this.mature.healthy.harvest.Target(this.Fruit).OnAnimQueueComplete(this.mature.healthy.growing).Target(this.masterTarget)
			.Enter(delegate(VineBranch.Instance smi)
			{
				VineBranch.PlayAnimsOnFruit(smi, smi.Anims.meter_harvest, KAnim.PlayMode.Once);
			})
			.Enter(new StateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.State.Callback(VineBranch.MakeItNotHarvestable))
			.Enter(new StateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.State.Callback(VineBranch.ResetFruitGrowProgress))
			.Enter(new StateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.State.Callback(VineBranch.SpawnHarvestedFruit))
			.TriggerOnExit(GameHashes.HarvestComplete, null)
			.ScheduleGoTo(3f, this.mature.healthy.growing);
		this.mature.healthy.selfHarvestFromOld.Target(this.Fruit).OnAnimQueueComplete(this.mature.healthy.growing).Target(this.masterTarget)
			.Enter(delegate(VineBranch.Instance smi)
			{
				VineBranch.PlayAnimsOnFruit(smi, smi.Anims.meter_harvest, KAnim.PlayMode.Once);
			})
			.Enter(new StateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.State.Callback(VineBranch.ForceCancelHarvest))
			.Enter(new StateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.State.Callback(VineBranch.MakeItNotHarvestable))
			.Enter(new StateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.State.Callback(VineBranch.ResetOldAge))
			.Enter(new StateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.State.Callback(VineBranch.ResetFruitGrowProgress))
			.Enter(new StateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.State.Callback(VineBranch.SpawnHarvestedFruit))
			.TriggerOnExit(GameHashes.HarvestComplete, null)
			.ScheduleGoTo(3f, this.mature.healthy.growing);
		this.mature.wilted.PlayAnim(new Func<VineBranch.Instance, string>(VineBranch.GetWiltAnim), KAnim.PlayMode.Loop).Enter(delegate(VineBranch.Instance smi)
		{
			VineBranch.PlayAnimsOnFruit(smi, VineBranch.GetFruitWiltAnim(smi), KAnim.PlayMode.Loop);
		}).EventHandler(GameHashes.BranchShapeChanged, delegate(VineBranch.Instance smi)
		{
			VineBranch.RefreshAnim(smi, VineBranch.GetWiltAnim(smi), KAnim.PlayMode.Loop);
		})
			.EventHandler(GameHashes.BranchShapeChanged, new StateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.State.Callback(VineBranch.RecreateFruitMeter))
			.EventHandler(GameHashes.BranchShapeChanged, delegate(VineBranch.Instance smi)
			{
				VineBranch.PlayAnimsOnFruit(smi, smi.Anims.meter_wilted, KAnim.PlayMode.Loop);
			})
			.EventTransition(GameHashes.WiltRecover, this.mature.healthy, (VineBranch.Instance smi) => !smi.IsWilting)
			.EventTransition(GameHashes.Harvest, this.mature.healthy.harvest, null);
		this.dead.Target(this.masterTarget).ToggleMainStatusItem(Db.Get().CreatureStatusItems.Dead, null).Enter(new StateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.State.Callback(VineBranch.HarvestOnDeath))
			.Enter(delegate(VineBranch.Instance smi)
			{
				if (!smi.gameObject.GetComponent<KPrefabID>().HasTag(GameTags.Uprooted) && !smi.IsWild)
				{
					Notifier notifier = smi.gameObject.AddOrGet<Notifier>();
					Notification notification = VineBranch.CreateDeathNotification(smi);
					notifier.Add(notification, "");
				}
				GameUtil.KInstantiate(Assets.GetPrefab(EffectConfigs.PlantDeathId), smi.transform.GetPosition(), Grid.SceneLayer.FXFront, null, 0).SetActive(true);
				smi.Trigger(1623392196, null);
				smi.DestroySelf(null);
			});
	}

	// Token: 0x06004D4F RID: 19791 RVA: 0x001C0473 File Offset: 0x001BE673
	private static bool ShouldSelfHarvestFromOldAge(VineBranch.Instance smi, float dt)
	{
		return smi.IsOld;
	}

	// Token: 0x06004D50 RID: 19792 RVA: 0x001C047B File Offset: 0x001BE67B
	private static string GetWiltAnim(VineBranch.Instance smi)
	{
		return smi.Anims.GetBaseWiltAnim(smi.Anims.GetWiltLevel(smi.GrowthPercentage));
	}

	// Token: 0x06004D51 RID: 19793 RVA: 0x001C0499 File Offset: 0x001BE699
	private static string GetFruitWiltAnim(VineBranch.Instance smi)
	{
		return smi.Anims.GetMeterWiltAnim(smi.Anims.GetWiltLevel(smi.FruitGrowthPercentage));
	}

	// Token: 0x06004D52 RID: 19794 RVA: 0x001C04B7 File Offset: 0x001BE6B7
	private static void PlayAnimsOnFruit(VineBranch.Instance smi, string animName, KAnim.PlayMode playmode)
	{
		smi.PlayAnimOnFruitMeter(animName, playmode);
	}

	// Token: 0x06004D53 RID: 19795 RVA: 0x001C04C1 File Offset: 0x001BE6C1
	private static void UpdateFruitMeterGrowAnimations(VineBranch.Instance smi, float dt)
	{
		VineBranch.UpdateFruitMeterGrowAnimations(smi);
	}

	// Token: 0x06004D54 RID: 19796 RVA: 0x001C04C9 File Offset: 0x001BE6C9
	private static void UpdateFruitMeterGrowAnimations(VineBranch.Instance smi)
	{
		smi.UpdateFruitGrowMeterPosition();
	}

	// Token: 0x06004D55 RID: 19797 RVA: 0x001C04D1 File Offset: 0x001BE6D1
	private static void RecreateFruitMeter(VineBranch.Instance smi)
	{
		smi.CreateFruitMeter();
	}

	// Token: 0x06004D56 RID: 19798 RVA: 0x001C04D9 File Offset: 0x001BE6D9
	private static void SetupFruitMeter(VineBranch.Instance smi)
	{
		smi.CreateFruitMeter();
	}

	// Token: 0x06004D57 RID: 19799 RVA: 0x001C04E1 File Offset: 0x001BE6E1
	private static void SpawnBranchIfPossible(VineBranch.Instance smi, float dt)
	{
		smi.AttemptToSpawnBranch();
	}

	// Token: 0x06004D58 RID: 19800 RVA: 0x001C04E9 File Offset: 0x001BE6E9
	private static void MakeItHarvestable(VineBranch.Instance smi)
	{
		smi.SetHarvestableState(true);
	}

	// Token: 0x06004D59 RID: 19801 RVA: 0x001C04F2 File Offset: 0x001BE6F2
	private static void ForceCancelHarvest(VineBranch.Instance smi)
	{
		smi.ForceCancelHarvest();
	}

	// Token: 0x06004D5A RID: 19802 RVA: 0x001C04FA File Offset: 0x001BE6FA
	private static void MakeItNotHarvestable(VineBranch.Instance smi)
	{
		smi.SetHarvestableState(false);
	}

	// Token: 0x06004D5B RID: 19803 RVA: 0x001C0503 File Offset: 0x001BE703
	private static void RefreshPositionPercent(VineBranch.Instance smi, float dt)
	{
		VineBranch.RefreshPositionPercent(smi);
	}

	// Token: 0x06004D5C RID: 19804 RVA: 0x001C050B File Offset: 0x001BE70B
	private static void RefreshPositionPercent(VineBranch.Instance smi)
	{
		smi.AnimController.SetPositionPercent(smi.GrowthPercentage);
	}

	// Token: 0x06004D5D RID: 19805 RVA: 0x001C051E File Offset: 0x001BE71E
	private static void SubscribreSurroundingCellChangeListeners(VineBranch.Instance smi)
	{
		smi.SubscribeSurroundingSolidChangesListeners();
	}

	// Token: 0x06004D5E RID: 19806 RVA: 0x001C0526 File Offset: 0x001BE726
	private static void UnSubscribreSurroundingSolidChangesListeners(VineBranch.Instance smi)
	{
		smi.UnSubscribreSurroundingSolidChangesListeners();
	}

	// Token: 0x06004D5F RID: 19807 RVA: 0x001C052E File Offset: 0x001BE72E
	private static void ResetFruitGrowProgress(VineBranch.Instance smi)
	{
		smi.ResetFruitGrowProgress();
	}

	// Token: 0x06004D60 RID: 19808 RVA: 0x001C0536 File Offset: 0x001BE736
	private static void ResetOldAge(VineBranch.Instance smi)
	{
		smi.ResetOldAge();
	}

	// Token: 0x06004D61 RID: 19809 RVA: 0x001C053E File Offset: 0x001BE73E
	private static void SpawnHarvestedFruit(VineBranch.Instance smi)
	{
		smi.SpawnHarvestedFruit();
	}

	// Token: 0x06004D62 RID: 19810 RVA: 0x001C0546 File Offset: 0x001BE746
	private static void RecalculateMyShape(VineBranch.Instance smi)
	{
		smi.RecalculateMyShape();
	}

	// Token: 0x06004D63 RID: 19811 RVA: 0x001C054E File Offset: 0x001BE74E
	private static void OnMotherRecovered(VineBranch.Instance smi)
	{
		smi.Trigger(912965142, true);
	}

	// Token: 0x06004D64 RID: 19812 RVA: 0x001C0561 File Offset: 0x001BE761
	private static void OnMotherWilted(VineBranch.Instance smi)
	{
		smi.Trigger(912965142, false);
	}

	// Token: 0x06004D65 RID: 19813 RVA: 0x001C0574 File Offset: 0x001BE774
	private static void RecalculateShapeAndSpawnBranchesIfSpawnedByDiscovery(VineBranch.Instance smi)
	{
		if (smi.IsNewGameSpawned)
		{
			smi.RecalculateMyShape();
			VineBranch.SpawnBranchIfPossible(smi, 0f);
		}
	}

	// Token: 0x06004D66 RID: 19814 RVA: 0x001C058F File Offset: 0x001BE78F
	public static void HarvestOnDeath(VineBranch.Instance smi)
	{
		if (smi.IsReadyForHarvest)
		{
			VineBranch.SpawnHarvestedFruit(smi);
		}
	}

	// Token: 0x06004D67 RID: 19815 RVA: 0x001C05A0 File Offset: 0x001BE7A0
	private static void RefreshAnim(VineBranch.Instance smi, string animName, KAnim.PlayMode playmode)
	{
		float elapsedTime = smi.AnimController.GetElapsedTime();
		smi.AnimController.Play(animName, playmode, 1f, 0f);
		smi.AnimController.SetElapsedTime(elapsedTime);
	}

	// Token: 0x06004D68 RID: 19816 RVA: 0x001C05E4 File Offset: 0x001BE7E4
	private static Notification CreateDeathNotification(VineBranch.Instance smi)
	{
		return new Notification(CREATURES.STATUSITEMS.PLANTDEATH.NOTIFICATION, NotificationType.Bad, (List<Notification> notificationList, object data) => CREATURES.STATUSITEMS.PLANTDEATH.NOTIFICATION_TOOLTIP + notificationList.ReduceMessages(false), "/t• " + smi.gameObject.GetProperName(), true, 0f, null, null, null, true, false, false);
	}

	// Token: 0x06004D6A RID: 19818 RVA: 0x001C064C File Offset: 0x001BE84C
	// Note: this type is marked as 'beforefieldinit'.
	static VineBranch()
	{
		Dictionary<VineBranch.Shape, VineBranch.ShapeCategory> dictionary = new Dictionary<VineBranch.Shape, VineBranch.ShapeCategory>();
		dictionary[VineBranch.Shape.Top] = VineBranch.ShapeCategory.Line;
		dictionary[VineBranch.Shape.Bottom] = VineBranch.ShapeCategory.Line;
		dictionary[VineBranch.Shape.Left] = VineBranch.ShapeCategory.Line;
		dictionary[VineBranch.Shape.Right] = VineBranch.ShapeCategory.Line;
		dictionary[VineBranch.Shape.InCornerTopLeft] = VineBranch.ShapeCategory.InCorner;
		dictionary[VineBranch.Shape.InCornerTopRight] = VineBranch.ShapeCategory.InCorner;
		dictionary[VineBranch.Shape.InCornerBottomLeft] = VineBranch.ShapeCategory.InCorner;
		dictionary[VineBranch.Shape.InCornerBottomRight] = VineBranch.ShapeCategory.InCorner;
		dictionary[VineBranch.Shape.OutCornerTopLeft] = VineBranch.ShapeCategory.OutCorner;
		dictionary[VineBranch.Shape.OutCornerTopRight] = VineBranch.ShapeCategory.OutCorner;
		dictionary[VineBranch.Shape.OutCornerBottomLeft] = VineBranch.ShapeCategory.OutCorner;
		dictionary[VineBranch.Shape.OutCornerBottomRight] = VineBranch.ShapeCategory.OutCorner;
		dictionary[VineBranch.Shape.TopEnd] = VineBranch.ShapeCategory.DeadEnd;
		dictionary[VineBranch.Shape.BottomEnd] = VineBranch.ShapeCategory.DeadEnd;
		dictionary[VineBranch.Shape.LeftEnd] = VineBranch.ShapeCategory.DeadEnd;
		dictionary[VineBranch.Shape.RightEnd] = VineBranch.ShapeCategory.DeadEnd;
		VineBranch.GetShapeCategory = dictionary;
		Dictionary<VineBranch.ShapeCategory, VineBranch.AnimSet> dictionary2 = new Dictionary<VineBranch.ShapeCategory, VineBranch.AnimSet>();
		dictionary2[VineBranch.ShapeCategory.Line] = new VineBranch.AnimSet("line_");
		dictionary2[VineBranch.ShapeCategory.InCorner] = new VineBranch.AnimSet("incorner_");
		dictionary2[VineBranch.ShapeCategory.OutCorner] = new VineBranch.AnimSet("outcorner_");
		dictionary2[VineBranch.ShapeCategory.DeadEnd] = new VineBranch.AnimSet("end_");
		VineBranch.GetAnimSetByShapeCategory = dictionary2;
	}

	// Token: 0x04003367 RID: 13159
	private static Dictionary<VineBranch.Shape, VineBranch.ShapeCategory> GetShapeCategory;

	// Token: 0x04003368 RID: 13160
	private static Dictionary<VineBranch.ShapeCategory, VineBranch.AnimSet> GetAnimSetByShapeCategory;

	// Token: 0x04003369 RID: 13161
	public StateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.TargetParameter Fruit;

	// Token: 0x0400336A RID: 13162
	public StateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.TargetParameter Mother;

	// Token: 0x0400336B RID: 13163
	public StateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.TargetParameter Branch;

	// Token: 0x0400336C RID: 13164
	public StateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.IntParameter BranchNumber;

	// Token: 0x0400336D RID: 13165
	public StateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.IntParameter BranchShape;

	// Token: 0x0400336E RID: 13166
	public StateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.BoolParameter GrowingClockwise;

	// Token: 0x0400336F RID: 13167
	public StateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.IntParameter RootShape;

	// Token: 0x04003370 RID: 13168
	public StateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.IntParameter RootDirection;

	// Token: 0x04003371 RID: 13169
	public StateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.BoolParameter WildPlanted;

	// Token: 0x04003372 RID: 13170
	public StateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.BoolParameter MarkedForDeath;

	// Token: 0x04003373 RID: 13171
	public StateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.Signal DieSignal;

	// Token: 0x04003374 RID: 13172
	public StateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.Signal OnShapeChangedSignal;

	// Token: 0x04003375 RID: 13173
	public VineBranch.GrowingStates undevelopedBranch;

	// Token: 0x04003376 RID: 13174
	public VineBranch.GrownStates mature;

	// Token: 0x04003377 RID: 13175
	public GameStateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.State dead;

	// Token: 0x02001B4B RID: 6987
	public class AnimSet
	{
		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x0600A6DB RID: 42715 RVA: 0x003AECF3 File Offset: 0x003ACEF3
		public string pre_grow
		{
			get
			{
				return this.suffix + "pre_grow";
			}
		}

		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x0600A6DC RID: 42716 RVA: 0x003AED05 File Offset: 0x003ACF05
		public string grow
		{
			get
			{
				return this.suffix + "grow";
			}
		}

		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x0600A6DD RID: 42717 RVA: 0x003AED17 File Offset: 0x003ACF17
		public string grow_pst
		{
			get
			{
				return this.suffix + "grow_pst";
			}
		}

		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x0600A6DE RID: 42718 RVA: 0x003AED29 File Offset: 0x003ACF29
		public string idle
		{
			get
			{
				return this.suffix + "idle";
			}
		}

		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x0600A6DF RID: 42719 RVA: 0x003AED3B File Offset: 0x003ACF3B
		public string meter_target
		{
			get
			{
				return this.suffix + "meter_target";
			}
		}

		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x0600A6E0 RID: 42720 RVA: 0x003AED4D File Offset: 0x003ACF4D
		public string meter
		{
			get
			{
				return this.suffix + "meter";
			}
		}

		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x0600A6E1 RID: 42721 RVA: 0x003AED5F File Offset: 0x003ACF5F
		public string meter_wilted
		{
			get
			{
				return this.suffix + "meter_wilted";
			}
		}

		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x0600A6E2 RID: 42722 RVA: 0x003AED71 File Offset: 0x003ACF71
		public string meter_harvest
		{
			get
			{
				return this.suffix + "meter_harvest";
			}
		}

		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x0600A6E3 RID: 42723 RVA: 0x003AED83 File Offset: 0x003ACF83
		public string meter_harvest_ready
		{
			get
			{
				return this.suffix + "meter_harvest_ready";
			}
		}

		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x0600A6E4 RID: 42724 RVA: 0x003AED95 File Offset: 0x003ACF95
		private string wilted
		{
			get
			{
				return this.suffix + "wilted";
			}
		}

		// Token: 0x0600A6E5 RID: 42725 RVA: 0x003AEDA8 File Offset: 0x003ACFA8
		public int GetWiltLevel(float growthPercentage)
		{
			int num;
			if (growthPercentage < 0.75f)
			{
				num = 1;
			}
			else if (growthPercentage < 1f)
			{
				num = 2;
			}
			else
			{
				num = 3;
			}
			return num;
		}

		// Token: 0x0600A6E6 RID: 42726 RVA: 0x003AEDD0 File Offset: 0x003ACFD0
		public string GetBaseWiltAnim(int level)
		{
			return this.GetWiltAnim(this.wilted, level);
		}

		// Token: 0x0600A6E7 RID: 42727 RVA: 0x003AEDDF File Offset: 0x003ACFDF
		public string GetMeterWiltAnim(int level)
		{
			return this.GetWiltAnim(this.meter_wilted, level);
		}

		// Token: 0x0600A6E8 RID: 42728 RVA: 0x003AEDEE File Offset: 0x003ACFEE
		private string GetWiltAnim(string wiltName, int level)
		{
			return wiltName + level.ToString();
		}

		// Token: 0x0600A6E9 RID: 42729 RVA: 0x003AEDFD File Offset: 0x003ACFFD
		public AnimSet(string suffix)
		{
			this.suffix = suffix;
		}

		// Token: 0x0400822D RID: 33325
		public string suffix;

		// Token: 0x0400822E RID: 33326
		private const int WILT_LEVELS = 3;
	}

	// Token: 0x02001B4C RID: 6988
	public enum ShapeCategory
	{
		// Token: 0x04008230 RID: 33328
		Line,
		// Token: 0x04008231 RID: 33329
		InCorner,
		// Token: 0x04008232 RID: 33330
		OutCorner,
		// Token: 0x04008233 RID: 33331
		DeadEnd
	}

	// Token: 0x02001B4D RID: 6989
	public enum Shape
	{
		// Token: 0x04008235 RID: 33333
		Top,
		// Token: 0x04008236 RID: 33334
		Bottom,
		// Token: 0x04008237 RID: 33335
		Left,
		// Token: 0x04008238 RID: 33336
		Right,
		// Token: 0x04008239 RID: 33337
		InCornerTopLeft,
		// Token: 0x0400823A RID: 33338
		InCornerTopRight,
		// Token: 0x0400823B RID: 33339
		InCornerBottomLeft,
		// Token: 0x0400823C RID: 33340
		InCornerBottomRight,
		// Token: 0x0400823D RID: 33341
		OutCornerTopLeft,
		// Token: 0x0400823E RID: 33342
		OutCornerTopRight,
		// Token: 0x0400823F RID: 33343
		OutCornerBottomLeft,
		// Token: 0x04008240 RID: 33344
		OutCornerBottomRight,
		// Token: 0x04008241 RID: 33345
		TopEnd,
		// Token: 0x04008242 RID: 33346
		BottomEnd,
		// Token: 0x04008243 RID: 33347
		LeftEnd,
		// Token: 0x04008244 RID: 33348
		RightEnd
	}

	// Token: 0x02001B4E RID: 6990
	public class Def : PlantBranchGrowerBase<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.PlantBranchGrowerBaseDef
	{
		// Token: 0x04008245 RID: 33349
		public float GROWTH_RATE = 0.0016666667f;

		// Token: 0x04008246 RID: 33350
		public float WILD_GROWTH_RATE = 0.00041666668f;
	}

	// Token: 0x02001B4F RID: 6991
	public class GrowingSpeedState : GameStateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.State
	{
		// Token: 0x04008247 RID: 33351
		public GameStateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.State wild;

		// Token: 0x04008248 RID: 33352
		public GameStateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.State domestic;
	}

	// Token: 0x02001B50 RID: 6992
	public class BranchAliveSubstate : GameStateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.PlantAliveSubState
	{
		// Token: 0x0600A6EC RID: 42732 RVA: 0x003AEE34 File Offset: 0x003AD034
		public GameStateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.State InitializeStates(StateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.TargetParameter plant, StateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.TargetParameter mother, GameStateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.State death_state, StateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.Signal dieSignal)
		{
			base.InitializeStates(plant, death_state);
			base.root.Target(plant).OnSignal(dieSignal, death_state).OnTargetLost(mother, death_state)
				.Target(mother)
				.EventHandler(GameHashes.Wilt, new StateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.State.Callback(VineBranch.OnMotherWilted))
				.EventHandler(GameHashes.WiltRecover, new StateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.State.Callback(VineBranch.OnMotherRecovered))
				.Target(plant);
			return this;
		}
	}

	// Token: 0x02001B51 RID: 6993
	public class GrowingStates : VineBranch.BranchAliveSubstate
	{
		// Token: 0x04008249 RID: 33353
		public VineBranch.GrowingSpeedState growing;

		// Token: 0x0400824A RID: 33354
		public GameStateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.State wilted;
	}

	// Token: 0x02001B52 RID: 6994
	public class FruitGrowingStates : GameStateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.State
	{
		// Token: 0x0400824B RID: 33355
		public VineBranch.GrowingSpeedState growing;

		// Token: 0x0400824C RID: 33356
		public GameStateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.State wilted;

		// Token: 0x0400824D RID: 33357
		public GameStateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.State harvestReady;

		// Token: 0x0400824E RID: 33358
		public GameStateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.State selfHarvestFromOld;

		// Token: 0x0400824F RID: 33359
		public GameStateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.State harvest;
	}

	// Token: 0x02001B53 RID: 6995
	public class GrownStates : VineBranch.BranchAliveSubstate
	{
		// Token: 0x04008250 RID: 33360
		public VineBranch.FruitGrowingStates healthy;

		// Token: 0x04008251 RID: 33361
		public GameStateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.State wilted;
	}

	// Token: 0x02001B54 RID: 6996
	public new class Instance : GameStateMachine<VineBranch, VineBranch.Instance, IStateMachineTarget, VineBranch.Def>.GameInstance, IManageGrowingStates, IWiltCause
	{
		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x0600A6F1 RID: 42737 RVA: 0x003AEEBF File Offset: 0x003AD0BF
		public GameObject Mother
		{
			get
			{
				return base.sm.Mother.Get(this);
			}
		}

		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x0600A6F2 RID: 42738 RVA: 0x003AEED2 File Offset: 0x003AD0D2
		public GameObject Branch
		{
			get
			{
				return base.sm.Branch.Get(this);
			}
		}

		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x0600A6F3 RID: 42739 RVA: 0x003AEEE5 File Offset: 0x003AD0E5
		public VineBranch.Instance BranchSMI
		{
			get
			{
				if (!(this.Branch == null))
				{
					return this.Branch.GetSMI<VineBranch.Instance>();
				}
				return null;
			}
		}

		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x0600A6F4 RID: 42740 RVA: 0x003AEF02 File Offset: 0x003AD102
		public int MyBranchNumber
		{
			get
			{
				return base.sm.BranchNumber.Get(this);
			}
		}

		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x0600A6F5 RID: 42741 RVA: 0x003AEF15 File Offset: 0x003AD115
		public bool IsGrowingClockwise
		{
			get
			{
				return base.sm.GrowingClockwise.Get(this);
			}
		}

		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x0600A6F6 RID: 42742 RVA: 0x003AEF28 File Offset: 0x003AD128
		public bool IsWild
		{
			get
			{
				return base.sm.WildPlanted.Get(this);
			}
		}

		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x0600A6F7 RID: 42743 RVA: 0x003AEF3B File Offset: 0x003AD13B
		public bool MaxBranchNumberReached
		{
			get
			{
				return this.MyBranchNumber >= 12;
			}
		}

		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x0600A6F8 RID: 42744 RVA: 0x003AEF4A File Offset: 0x003AD14A
		public bool CanChangeShape
		{
			get
			{
				return !this.isSpawningNextBranch && this.Branch == null && !this.MaxBranchNumberReached;
			}
		}

		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x0600A6F9 RID: 42745 RVA: 0x003AEF6D File Offset: 0x003AD16D
		public VineBranch.Shape MyShape
		{
			get
			{
				return (VineBranch.Shape)base.sm.BranchShape.Get(this);
			}
		}

		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x0600A6FA RID: 42746 RVA: 0x003AEF80 File Offset: 0x003AD180
		public VineBranch.ShapeCategory MyShapeCategory
		{
			get
			{
				return VineBranch.GetShapeCategory[this.MyShape];
			}
		}

		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x0600A6FB RID: 42747 RVA: 0x003AEF92 File Offset: 0x003AD192
		public VineBranch.Shape RootShape
		{
			get
			{
				return (VineBranch.Shape)base.sm.RootShape.Get(this);
			}
		}

		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x0600A6FC RID: 42748 RVA: 0x003AEFA5 File Offset: 0x003AD1A5
		public Direction RootDirection
		{
			get
			{
				return (Direction)base.sm.RootDirection.Get(this);
			}
		}

		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x0600A6FD RID: 42749 RVA: 0x003AEFB8 File Offset: 0x003AD1B8
		public VineBranch.AnimSet Anims
		{
			get
			{
				return VineBranch.GetAnimSetByShapeCategory[this.MyShapeCategory];
			}
		}

		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x0600A6FE RID: 42750 RVA: 0x003AEFCA File Offset: 0x003AD1CA
		public bool IsOld
		{
			get
			{
				return this.oldAge.value >= this.oldAge.GetMax();
			}
		}

		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x0600A6FF RID: 42751 RVA: 0x003AEFE7 File Offset: 0x003AD1E7
		public bool IsWilting
		{
			get
			{
				return this.wiltCondition.IsWilting() || this.MotherSMI.IsWilting;
			}
		}

		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x0600A700 RID: 42752 RVA: 0x003AF003 File Offset: 0x003AD203
		public bool IsGrown
		{
			get
			{
				return this.GrowthPercentage >= 1f;
			}
		}

		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x0600A701 RID: 42753 RVA: 0x003AF015 File Offset: 0x003AD215
		public float GrowthPercentage
		{
			get
			{
				return this.maturity.value / this.maturity.GetMax();
			}
		}

		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x0600A702 RID: 42754 RVA: 0x003AF02E File Offset: 0x003AD22E
		public bool IsReadyForHarvest
		{
			get
			{
				return this.FruitGrowthPercentage >= 1f;
			}
		}

		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x0600A703 RID: 42755 RVA: 0x003AF040 File Offset: 0x003AD240
		public float FruitGrowthPercentage
		{
			get
			{
				return this.fruitMaturity.value / this.fruitMaturity.GetMax();
			}
		}

		// Token: 0x0600A704 RID: 42756 RVA: 0x003AF05C File Offset: 0x003AD25C
		public Instance(IStateMachineTarget master, VineBranch.Def def)
			: base(master, def)
		{
			Amounts amounts = base.gameObject.GetAmounts();
			this.maturity = amounts.Get(Db.Get().Amounts.Maturity);
			this.fruitMaturity = amounts.Get(Db.Get().Amounts.Maturity2);
			this.baseGrowingRate = new AttributeModifier(this.maturity.deltaAttribute.Id, def.GROWTH_RATE, CREATURES.STATS.MATURITY.GROWING, false, false, true);
			this.wildGrowingRate = new AttributeModifier(this.maturity.deltaAttribute.Id, def.WILD_GROWTH_RATE, CREATURES.STATS.MATURITY.GROWINGWILD, false, false, true);
			this.baseFruitGrowingRate = new AttributeModifier(this.fruitMaturity.deltaAttribute.Id, def.GROWTH_RATE, CREATURES.STATS.MATURITY.GROWING, false, false, true);
			this.wildFruitGrowingRate = new AttributeModifier(this.fruitMaturity.deltaAttribute.Id, def.WILD_GROWTH_RATE, CREATURES.STATS.MATURITY.GROWINGWILD, false, false, true);
			this.oldAge = amounts.Add(new AmountInstance(Db.Get().Amounts.OldAge, base.gameObject));
			this.oldAge.maxAttribute.ClearModifiers();
			this.oldAge.maxAttribute.Add(new AttributeModifier(Db.Get().Amounts.OldAge.maxAttribute.Id, 2400f, null, false, false, true));
			this.getOldRate = new AttributeModifier(this.oldAge.deltaAttribute.Id, 1f, null, false, false, true);
			this.wiltCondition = base.GetComponent<WiltCondition>();
			this.AnimController = base.GetComponent<KBatchedAnimController>();
			this.uprootMonitor = base.GetComponent<UprootedMonitor>();
			this.harvestable = base.GetComponent<Harvestable>();
			this.uprootMonitor.customFoundationCheckFn = new Func<int, bool>(this.IsCellFoundation);
			this.SetCellRegistrationAsPlant(true);
			base.Subscribe(1119167081, new Action<object>(this.OnSpawnedByDiscovery));
		}

		// Token: 0x0600A705 RID: 42757 RVA: 0x003AF290 File Offset: 0x003AD490
		public override void StartSM()
		{
			this.wasMarkedForDeadBeforeStartSM = base.sm.MarkedForDeath.Get(this);
			base.master.gameObject.AddTag(GameTags.GrowingPlant);
			base.StartSM();
			this.SetAnimOrientation(this.MyShape, this.IsGrowingClockwise);
			this.Schedule(1f, new Action<object>(this.DelayedResetUprootMonitor), null);
		}

		// Token: 0x0600A706 RID: 42758 RVA: 0x003AF2FC File Offset: 0x003AD4FC
		public override void PostParamsInitialized()
		{
			base.PostParamsInitialized();
			this.MotherSMI = ((this.Mother == null) ? null : this.Mother.GetSMI<VineMother.Instance>());
			if (this.wasMarkedForDeadBeforeStartSM)
			{
				base.sm.MarkedForDeath.Set(true, this, false);
			}
			this.HideAllFruitSymbols();
		}

		// Token: 0x0600A707 RID: 42759 RVA: 0x003AF353 File Offset: 0x003AD553
		protected override void OnCleanUp()
		{
			this.DestroyFruitMeter();
			this.KillForwardBranch();
			this.SetCellRegistrationAsPlant(false);
			base.OnCleanUp();
		}

		// Token: 0x0600A708 RID: 42760 RVA: 0x003AF36E File Offset: 0x003AD56E
		public void DestroySelf(object o)
		{
			CreatureHelpers.DeselectCreature(base.gameObject);
			Util.KDestroyGameObject(base.gameObject);
		}

		// Token: 0x0600A709 RID: 42761 RVA: 0x003AF388 File Offset: 0x003AD588
		public void SetCellRegistrationAsPlant(bool doRegister)
		{
			int num = Grid.PosToCell(this);
			if (doRegister && Grid.Objects[num, 5] == null)
			{
				Grid.Objects[num, 5] = base.gameObject;
				return;
			}
			if (!doRegister && Grid.Objects[num, 5] == base.gameObject)
			{
				Grid.Objects[num, 5] = null;
			}
		}

		// Token: 0x0600A70A RID: 42762 RVA: 0x003AF3EF File Offset: 0x003AD5EF
		public void SetHarvestableState(bool canBeHarvested)
		{
			this.harvestable.SetCanBeHarvested(canBeHarvested);
		}

		// Token: 0x0600A70B RID: 42763 RVA: 0x003AF400 File Offset: 0x003AD600
		public void SetAutoHarvestInChainReaction(bool autoharvest)
		{
			HarvestDesignatable component = base.GetComponent<HarvestDesignatable>();
			if (component != null)
			{
				component.SetHarvestWhenReady(autoharvest);
				if (this.BranchSMI != null)
				{
					this.BranchSMI.SetAutoHarvestInChainReaction(autoharvest);
				}
			}
		}

		// Token: 0x0600A70C RID: 42764 RVA: 0x003AF438 File Offset: 0x003AD638
		public void ForceCancelHarvest()
		{
			this.harvestable.ForceCancelHarvest(true);
		}

		// Token: 0x0600A70D RID: 42765 RVA: 0x003AF44B File Offset: 0x003AD64B
		public void ResetOldAge()
		{
			this.oldAge.SetValue(0f);
		}

		// Token: 0x0600A70E RID: 42766 RVA: 0x003AF460 File Offset: 0x003AD660
		private void OnSpawnedByDiscovery(object o)
		{
			float num = 1f - (float)this.MyBranchNumber / 12f;
			float num2 = ((global::UnityEngine.Random.Range(0f, 1f) <= num) ? 1f : global::UnityEngine.Random.Range(0f, 1f));
			this.maturity.SetValue(this.maturity.maxAttribute.GetTotalValue() * num2);
			if (this.IsGrown)
			{
				this.IsNewGameSpawned = true;
				this.fruitMaturity.SetValue(this.fruitMaturity.maxAttribute.GetTotalValue() * global::UnityEngine.Random.Range(0f, 1f));
			}
		}

		// Token: 0x0600A70F RID: 42767 RVA: 0x003AF503 File Offset: 0x003AD703
		public void ResetFruitGrowProgress()
		{
			this.fruitMaturity.SetValue(0f);
		}

		// Token: 0x0600A710 RID: 42768 RVA: 0x003AF516 File Offset: 0x003AD716
		public void SpawnHarvestedFruit()
		{
			base.GetComponent<Crop>().SpawnConfiguredFruit(null);
		}

		// Token: 0x0600A711 RID: 42769 RVA: 0x003AF524 File Offset: 0x003AD724
		public void HideAllFruitSymbols()
		{
			foreach (VineBranch.ShapeCategory shapeCategory in VineBranch.GetAnimSetByShapeCategory.Keys)
			{
				VineBranch.AnimSet animSet = VineBranch.GetAnimSetByShapeCategory[shapeCategory];
				this.AnimController.SetSymbolVisiblity(animSet.meter_target, false);
			}
		}

		// Token: 0x0600A712 RID: 42770 RVA: 0x003AF598 File Offset: 0x003AD798
		public void CreateFruitMeter()
		{
			this.DestroyFruitMeter();
			this.fruitMeter = new MeterController(this.AnimController, this.Anims.meter_target, this.Anims.meter, Meter.Offset.NoChange, Grid.SceneLayer.Building, Array.Empty<string>());
			base.sm.Fruit.Set(this.fruitMeter.gameObject, this, false);
		}

		// Token: 0x0600A713 RID: 42771 RVA: 0x003AF5F8 File Offset: 0x003AD7F8
		private void DestroyFruitMeter()
		{
			if (this.fruitMeter != null)
			{
				this.fruitMeter.Unlink();
				Util.KDestroyGameObject(this.fruitMeter.gameObject);
				this.fruitMeter = null;
				base.sm.Fruit.Set(null, this);
			}
		}

		// Token: 0x0600A714 RID: 42772 RVA: 0x003AF636 File Offset: 0x003AD836
		public void PlayAnimOnFruitMeter(string animName, KAnim.PlayMode playMode)
		{
			if (this.fruitMeter != null)
			{
				this.fruitMeter.meterController.Play(animName, playMode, 1f, 0f);
			}
		}

		// Token: 0x0600A715 RID: 42773 RVA: 0x003AF664 File Offset: 0x003AD864
		public void UpdateFruitGrowMeterPosition()
		{
			if (this.fruitMeter != null)
			{
				if (this.fruitMeter.meterController.currentAnim != this.Anims.meter)
				{
					this.PlayAnimOnFruitMeter(this.Anims.meter, KAnim.PlayMode.Paused);
				}
				this.fruitMeter.SetPositionPercent(this.FruitGrowthPercentage);
			}
		}

		// Token: 0x0600A716 RID: 42774 RVA: 0x003AF6C4 File Offset: 0x003AD8C4
		private void KillForwardBranch()
		{
			if (this.Branch != null)
			{
				VineBranch.Instance smi = this.Branch.GetSMI<VineBranch.Instance>();
				if (smi != null)
				{
					smi.sm.DieSignal.Trigger(smi);
					smi.sm.MarkedForDeath.Set(true, smi, false);
				}
				base.sm.Branch.Set(null, this);
			}
		}

		// Token: 0x0600A717 RID: 42775 RVA: 0x003AF728 File Offset: 0x003AD928
		public void SetupRootInformation(VineMother.Instance mother)
		{
			CellOffset cellOffsetDirection = Grid.GetCellOffsetDirection(Grid.PosToCell(this), Grid.PosToCell(mother));
			Direction direction = ((cellOffsetDirection == CellOffset.left) ? Direction.Left : ((cellOffsetDirection == CellOffset.right) ? Direction.Right : ((cellOffsetDirection == CellOffset.up) ? Direction.Up : Direction.Down)));
			base.sm.RootDirection.Set((int)direction, this, false);
			base.sm.RootShape.Set(1, this, false);
			base.sm.BranchNumber.Set(1, this, false);
			base.sm.WildPlanted.Set(mother.IsWild, this, false);
			base.sm.Mother.Set(mother.gameObject, this, false);
			this.MotherSMI = ((this.Mother == null) ? null : this.Mother.GetSMI<VineMother.Instance>());
			HarvestDesignatable component = mother.GetComponent<HarvestDesignatable>();
			base.GetComponent<HarvestDesignatable>().SetHarvestWhenReady(component.HarvestWhenReady);
		}

		// Token: 0x0600A718 RID: 42776 RVA: 0x003AF824 File Offset: 0x003ADA24
		public void SetupRootInformation(VineBranch.Instance root)
		{
			CellOffset cellOffsetDirection = Grid.GetCellOffsetDirection(Grid.PosToCell(this), Grid.PosToCell(root));
			Direction direction = ((cellOffsetDirection == CellOffset.left) ? Direction.Left : ((cellOffsetDirection == CellOffset.right) ? Direction.Right : ((cellOffsetDirection == CellOffset.up) ? Direction.Up : Direction.Down)));
			base.sm.RootDirection.Set((int)direction, this, false);
			base.sm.RootShape.Set((int)root.MyShape, this, false);
			base.sm.BranchNumber.Set(root.MyBranchNumber + 1, this, false);
			base.sm.WildPlanted.Set(root.IsWild, this, false);
			base.sm.Mother.Set(root.Mother, this, false);
			this.MotherSMI = ((this.Mother == null) ? null : this.Mother.GetSMI<VineMother.Instance>());
			HarvestDesignatable component = root.GetComponent<HarvestDesignatable>();
			base.GetComponent<HarvestDesignatable>().SetHarvestWhenReady(component.HarvestWhenReady);
		}

		// Token: 0x0600A719 RID: 42777 RVA: 0x003AF92C File Offset: 0x003ADB2C
		public void AttemptToSpawnBranch()
		{
			if (this.CanSpawnBranch())
			{
				this.isSpawningNextBranch = true;
				int cellToSpawnBranch = this.GetCellToSpawnBranch();
				GameObject gameObject = this.SpawnBranchOnCell(cellToSpawnBranch);
				base.sm.Branch.Set(gameObject, this, false);
				this.isSpawningNextBranch = false;
				if (this.IsNewGameSpawned)
				{
					gameObject.Trigger(1119167081, null);
				}
				this.ResetUprootMonitor();
			}
			if (this.IsNewGameSpawned)
			{
				this.IsNewGameSpawned = false;
			}
		}

		// Token: 0x0600A71A RID: 42778 RVA: 0x003AF99C File Offset: 0x003ADB9C
		private GameObject SpawnBranchOnCell(int cell)
		{
			Vector3 vector = Grid.CellToPosCBC(cell, Grid.SceneLayer.BuildingFront);
			GameObject gameObject = Util.KInstantiate(Assets.GetPrefab(base.def.BRANCH_PREFAB_NAME), vector);
			gameObject.SetActive(true);
			gameObject.GetSMI<VineBranch.Instance>().SetupRootInformation(this);
			return gameObject;
		}

		// Token: 0x0600A71B RID: 42779 RVA: 0x003AF9E0 File Offset: 0x003ADBE0
		private bool IsCellFoundation(int cell)
		{
			return VineBranch.IsCellFoundation(cell) || (this.MotherSMI.IsOnPlanterBox && this.MotherSMI.PlanterboxCell == cell);
		}

		// Token: 0x0600A71C RID: 42780 RVA: 0x003AFA0C File Offset: 0x003ADC0C
		private bool IsCellAvailable(int cell)
		{
			bool flag = VineBranch.IsCellAvailable(base.gameObject, cell, new Func<int, bool>(this.IsCellFoundation));
			if (flag && this.IsNewGameSpawned)
			{
				flag = SaveGame.Instance.worldGenSpawner.GetSpawnableInCell(cell) == null;
			}
			return flag;
		}

		// Token: 0x0600A71D RID: 42781 RVA: 0x003AFA54 File Offset: 0x003ADC54
		public bool CanSpawnBranch()
		{
			bool flag = this.Branch == null;
			flag = flag && !this.MaxBranchNumberReached;
			flag = flag && this.IsGrown;
			if (flag)
			{
				int cellToSpawnBranch = this.GetCellToSpawnBranch();
				flag = flag && cellToSpawnBranch != Grid.InvalidCell;
				flag = flag && this.IsCellAvailable(cellToSpawnBranch);
			}
			return flag;
		}

		// Token: 0x0600A71E RID: 42782 RVA: 0x003AFAB8 File Offset: 0x003ADCB8
		public int GetCellToSpawnBranch()
		{
			int num = Grid.PosToCell(base.gameObject);
			switch (this.MyShape)
			{
			case VineBranch.Shape.Top:
			case VineBranch.Shape.Bottom:
				if (this.RootDirection != Direction.Left)
				{
					return Grid.OffsetCell(num, -1, 0);
				}
				return Grid.OffsetCell(num, 1, 0);
			case VineBranch.Shape.Left:
			case VineBranch.Shape.Right:
				if (this.RootDirection != Direction.Up)
				{
					return Grid.OffsetCell(num, 0, 1);
				}
				return Grid.OffsetCell(num, 0, -1);
			case VineBranch.Shape.InCornerTopLeft:
				if (this.RootDirection != Direction.Down)
				{
					return Grid.OffsetCell(num, 0, -1);
				}
				return Grid.OffsetCell(num, 1, 0);
			case VineBranch.Shape.InCornerTopRight:
				if (this.RootDirection != Direction.Down)
				{
					return Grid.OffsetCell(num, 0, -1);
				}
				return Grid.OffsetCell(num, -1, 0);
			case VineBranch.Shape.InCornerBottomLeft:
				if (this.RootDirection != Direction.Up)
				{
					return Grid.OffsetCell(num, 0, 1);
				}
				return Grid.OffsetCell(num, 1, 0);
			case VineBranch.Shape.InCornerBottomRight:
				if (this.RootDirection != Direction.Up)
				{
					return Grid.OffsetCell(num, 0, 1);
				}
				return Grid.OffsetCell(num, -1, 0);
			case VineBranch.Shape.OutCornerTopLeft:
				if (this.RootDirection != Direction.Up)
				{
					return Grid.OffsetCell(num, 0, 1);
				}
				return Grid.OffsetCell(num, -1, 0);
			case VineBranch.Shape.OutCornerTopRight:
				if (this.RootDirection != Direction.Up)
				{
					return Grid.OffsetCell(num, 0, 1);
				}
				return Grid.OffsetCell(num, 1, 0);
			case VineBranch.Shape.OutCornerBottomLeft:
				if (this.RootDirection != Direction.Down)
				{
					return Grid.OffsetCell(num, 0, -1);
				}
				return Grid.OffsetCell(num, -1, 0);
			case VineBranch.Shape.OutCornerBottomRight:
				if (this.RootDirection != Direction.Down)
				{
					return Grid.OffsetCell(num, 0, -1);
				}
				return Grid.OffsetCell(num, 1, 0);
			default:
				return Grid.InvalidCell;
			}
		}

		// Token: 0x0600A71F RID: 42783 RVA: 0x003AFC24 File Offset: 0x003ADE24
		public void SubscribeSurroundingSolidChangesListeners()
		{
			KPrefabID component = base.gameObject.GetComponent<KPrefabID>();
			this.UnSubscribreSurroundingSolidChangesListeners();
			CellOffset[] array = new CellOffset[]
			{
				new CellOffset(-1, -1),
				new CellOffset(0, -1),
				new CellOffset(1, -1),
				new CellOffset(-1, 0),
				new CellOffset(1, 0),
				new CellOffset(-1, 1),
				new CellOffset(0, 1),
				new CellOffset(1, 1)
			};
			Extents extents = new Extents(Grid.PosToCell(base.gameObject), array);
			this.solidPartitionerEntry = GameScenePartitioner.Instance.Add("VineBranchSurroundingListenerSolids", base.gameObject, extents, GameScenePartitioner.Instance.solidChangedLayer, new Action<object>(this.OnSurroundingCellsBlockageChangedDetected));
			this.buildingsPartitionerEntry = GameScenePartitioner.Instance.Add("VineBranchSurroundingListenerBuildings", base.gameObject, extents, GameScenePartitioner.Instance.objectLayers[1], new Action<object>(this.OnSurroundingCellsBlockageChangedDetected));
			this.plantsPartitionerEntry = GameScenePartitioner.Instance.Add("VineBranchSurroundingListenerPlants", component, extents, GameScenePartitioner.Instance.plantsChangedLayer, new Action<object>(this.OnSurroundingCellsBlockageChangedDetected));
			this.liquidsPartitionerEntry = GameScenePartitioner.Instance.Add("VineBranchSurroundingListenerLiquids", base.gameObject, extents, GameScenePartitioner.Instance.liquidChangedLayer, new Action<object>(this.OnSurroundingCellsBlockageChangedDetected));
		}

		// Token: 0x0600A720 RID: 42784 RVA: 0x003AFD94 File Offset: 0x003ADF94
		public void UnSubscribreSurroundingSolidChangesListeners()
		{
			GameScenePartitioner.Instance.Free(ref this.solidPartitionerEntry);
			GameScenePartitioner.Instance.Free(ref this.buildingsPartitionerEntry);
			GameScenePartitioner.Instance.Free(ref this.plantsPartitionerEntry);
			GameScenePartitioner.Instance.Free(ref this.liquidsPartitionerEntry);
			this.solidPartitionerEntry = HandleVector<int>.InvalidHandle;
			this.buildingsPartitionerEntry = HandleVector<int>.InvalidHandle;
			this.plantsPartitionerEntry = HandleVector<int>.InvalidHandle;
			this.liquidsPartitionerEntry = HandleVector<int>.InvalidHandle;
		}

		// Token: 0x0600A721 RID: 42785 RVA: 0x003AFE0D File Offset: 0x003AE00D
		private void OnSurroundingCellsBlockageChangedDetected(object o)
		{
			if (this.CanChangeShape)
			{
				this.RecalculateMyShape();
			}
		}

		// Token: 0x0600A722 RID: 42786 RVA: 0x003AFE1D File Offset: 0x003AE01D
		private void SetShape(VineBranch.Shape shape, bool clockwise)
		{
			base.sm.BranchShape.Set((int)shape, this, false);
			base.sm.GrowingClockwise.Set(clockwise, this, false);
			this.SetAnimOrientation(shape, clockwise);
			base.Trigger(838747413, null);
		}

		// Token: 0x0600A723 RID: 42787 RVA: 0x003AFE5C File Offset: 0x003AE05C
		public void RecalculateMyShape()
		{
			VineBranch.Shape shape = VineBranch.Shape.TopEnd;
			bool flag = false;
			switch (this.RootDirection)
			{
			case Direction.Up:
				switch (base.smi.RootShape)
				{
				case VineBranch.Shape.Left:
				case VineBranch.Shape.InCornerTopLeft:
				case VineBranch.Shape.OutCornerBottomLeft:
					shape = this.ChooseCompatibleShape(new VineBranch.Shape[]
					{
						VineBranch.Shape.BottomEnd,
						VineBranch.Shape.InCornerBottomLeft,
						VineBranch.Shape.OutCornerTopLeft,
						VineBranch.Shape.Left
					});
					flag = shape == VineBranch.Shape.OutCornerTopLeft;
					break;
				case VineBranch.Shape.Right:
				case VineBranch.Shape.InCornerTopRight:
				case VineBranch.Shape.OutCornerBottomRight:
					shape = this.ChooseCompatibleShape(new VineBranch.Shape[]
					{
						VineBranch.Shape.BottomEnd,
						VineBranch.Shape.InCornerBottomRight,
						VineBranch.Shape.OutCornerTopRight,
						VineBranch.Shape.Right
					});
					flag = shape != VineBranch.Shape.OutCornerTopRight;
					break;
				}
				break;
			case Direction.Right:
				switch (base.smi.RootShape)
				{
				case VineBranch.Shape.Top:
				case VineBranch.Shape.InCornerTopRight:
				case VineBranch.Shape.OutCornerTopLeft:
					shape = this.ChooseCompatibleShape(new VineBranch.Shape[]
					{
						VineBranch.Shape.LeftEnd,
						VineBranch.Shape.InCornerTopLeft,
						VineBranch.Shape.OutCornerTopRight,
						VineBranch.Shape.Top
					});
					flag = shape == VineBranch.Shape.OutCornerTopRight;
					break;
				case VineBranch.Shape.Bottom:
				case VineBranch.Shape.InCornerBottomRight:
				case VineBranch.Shape.OutCornerBottomLeft:
					shape = this.ChooseCompatibleShape(new VineBranch.Shape[]
					{
						VineBranch.Shape.LeftEnd,
						VineBranch.Shape.InCornerBottomLeft,
						VineBranch.Shape.OutCornerBottomRight,
						VineBranch.Shape.Bottom
					});
					flag = shape != VineBranch.Shape.OutCornerBottomRight;
					break;
				}
				break;
			case Direction.Down:
				switch (base.smi.RootShape)
				{
				case VineBranch.Shape.Left:
				case VineBranch.Shape.InCornerBottomLeft:
				case VineBranch.Shape.OutCornerTopLeft:
					shape = this.ChooseCompatibleShape(new VineBranch.Shape[]
					{
						VineBranch.Shape.TopEnd,
						VineBranch.Shape.InCornerTopLeft,
						VineBranch.Shape.OutCornerBottomLeft,
						VineBranch.Shape.Left
					});
					flag = shape != VineBranch.Shape.OutCornerBottomLeft;
					break;
				case VineBranch.Shape.Right:
				case VineBranch.Shape.InCornerBottomRight:
				case VineBranch.Shape.OutCornerTopRight:
					shape = this.ChooseCompatibleShape(new VineBranch.Shape[]
					{
						VineBranch.Shape.TopEnd,
						VineBranch.Shape.InCornerTopRight,
						VineBranch.Shape.OutCornerBottomRight,
						VineBranch.Shape.Right
					});
					flag = shape == VineBranch.Shape.OutCornerBottomRight;
					break;
				}
				break;
			case Direction.Left:
			{
				VineBranch.Shape rootShape = this.RootShape;
				switch (rootShape)
				{
				case VineBranch.Shape.Top:
				case VineBranch.Shape.InCornerTopLeft:
					break;
				case VineBranch.Shape.Bottom:
				case VineBranch.Shape.InCornerBottomLeft:
					goto IL_0084;
				case VineBranch.Shape.Left:
				case VineBranch.Shape.Right:
				case VineBranch.Shape.InCornerTopRight:
					goto IL_0230;
				default:
					if (rootShape != VineBranch.Shape.OutCornerTopRight)
					{
						if (rootShape != VineBranch.Shape.OutCornerBottomRight)
						{
							goto IL_0230;
						}
						goto IL_0084;
					}
					break;
				}
				shape = this.ChooseCompatibleShape(new VineBranch.Shape[]
				{
					VineBranch.Shape.RightEnd,
					VineBranch.Shape.InCornerTopRight,
					VineBranch.Shape.OutCornerTopLeft,
					VineBranch.Shape.Top
				});
				flag = shape != VineBranch.Shape.OutCornerTopLeft;
				break;
				IL_0084:
				shape = this.ChooseCompatibleShape(new VineBranch.Shape[]
				{
					VineBranch.Shape.RightEnd,
					VineBranch.Shape.InCornerBottomRight,
					VineBranch.Shape.OutCornerBottomLeft,
					VineBranch.Shape.Bottom
				});
				flag = shape == VineBranch.Shape.OutCornerBottomLeft;
				break;
			}
			}
			IL_0230:
			base.smi.SetShape(shape, flag);
		}

		// Token: 0x0600A724 RID: 42788 RVA: 0x003B00A8 File Offset: 0x003AE2A8
		private void SetAnimOrientation(VineBranch.Shape shape, bool clockwise)
		{
			this.AnimController.FlipX = false;
			this.AnimController.FlipY = false;
			this.AnimController.Rotation = 0f;
			switch (shape)
			{
			case VineBranch.Shape.Top:
				this.AnimController.FlipY = clockwise;
				this.AnimController.Rotation = (float)(clockwise ? 0 : 180);
				break;
			case VineBranch.Shape.Bottom:
				this.AnimController.FlipX = clockwise;
				break;
			case VineBranch.Shape.Left:
				this.AnimController.FlipX = clockwise;
				this.AnimController.Rotation = 90f;
				break;
			case VineBranch.Shape.Right:
				this.AnimController.FlipX = clockwise;
				this.AnimController.Rotation = 270f;
				break;
			case VineBranch.Shape.InCornerTopLeft:
				this.AnimController.FlipX = clockwise;
				this.AnimController.Rotation = (float)(clockwise ? 90 : 180);
				break;
			case VineBranch.Shape.InCornerTopRight:
				this.AnimController.FlipY = clockwise;
				this.AnimController.Rotation = (float)(clockwise ? 0 : 270);
				break;
			case VineBranch.Shape.InCornerBottomLeft:
				this.AnimController.FlipX = clockwise;
				this.AnimController.Rotation = (float)(clockwise ? 0 : 90);
				break;
			case VineBranch.Shape.InCornerBottomRight:
				this.AnimController.FlipY = clockwise;
				this.AnimController.Rotation = (float)(clockwise ? 90 : 0);
				break;
			case VineBranch.Shape.OutCornerTopLeft:
				this.AnimController.FlipY = clockwise;
				this.AnimController.Rotation = (float)(clockwise ? 90 : 0);
				break;
			case VineBranch.Shape.OutCornerTopRight:
				this.AnimController.FlipX = clockwise;
				this.AnimController.Rotation = (float)(clockwise ? 0 : 90);
				break;
			case VineBranch.Shape.OutCornerBottomLeft:
				this.AnimController.FlipY = clockwise;
				this.AnimController.Rotation = (float)(clockwise ? 0 : 270);
				break;
			case VineBranch.Shape.OutCornerBottomRight:
				this.AnimController.FlipX = clockwise;
				this.AnimController.Rotation = (float)(clockwise ? 90 : 180);
				break;
			case VineBranch.Shape.TopEnd:
				this.AnimController.FlipX = clockwise;
				this.AnimController.Rotation = (float)(clockwise ? 90 : 270);
				break;
			case VineBranch.Shape.BottomEnd:
				this.AnimController.FlipX = clockwise;
				this.AnimController.Rotation = (float)(clockwise ? 270 : 90);
				break;
			case VineBranch.Shape.LeftEnd:
				this.AnimController.FlipX = clockwise;
				this.AnimController.Rotation = (float)(clockwise ? 0 : 180);
				break;
			case VineBranch.Shape.RightEnd:
				this.AnimController.FlipX = clockwise;
				this.AnimController.Rotation = (float)(clockwise ? 180 : 0);
				break;
			}
			this.AnimController.Rotation *= -1f;
			this.AnimController.Offset = new Vector3(0f, (float)(this.AnimController.FlipY ? 1 : 0), 0f);
			bool flag = this.AnimController.FlipY && Mathf.Abs(this.AnimController.Rotation) == 90f;
			this.AnimController.Pivot = new Vector3(0f, flag ? (-0.5f) : 0.5f, 0f);
		}

		// Token: 0x0600A725 RID: 42789 RVA: 0x003B0404 File Offset: 0x003AE604
		private VineBranch.Shape ChooseCompatibleShape(VineBranch.Shape[] possibleShapesOrderedByPriority)
		{
			bool flag = false;
			VineBranch.Shape shape = VineBranch.Shape.TopEnd;
			int num = Grid.PosToCell(base.gameObject);
			int num2 = Grid.OffsetCell(num, -1, 0);
			int num3 = Grid.OffsetCell(num, 1, 0);
			int num4 = Grid.OffsetCell(num, 0, 1);
			int num5 = Grid.OffsetCell(num, 0, -1);
			int num6 = Grid.OffsetCell(num, -1, 1);
			int num7 = Grid.OffsetCell(num, 1, 1);
			int num8 = Grid.OffsetCell(num, -1, -1);
			int num9 = Grid.OffsetCell(num, 1, -1);
			foreach (VineBranch.Shape shape2 in possibleShapesOrderedByPriority)
			{
				VineBranch.ShapeCategory shapeCategory = VineBranch.GetShapeCategory[shape2];
				if (shapeCategory == VineBranch.ShapeCategory.DeadEnd)
				{
					shape = shape2;
				}
				if (!this.MaxBranchNumberReached || shapeCategory == VineBranch.ShapeCategory.DeadEnd)
				{
					switch (shape2)
					{
					case VineBranch.Shape.Top:
						flag = this.IsCellFoundation(num4) && (this.IsCellAvailable(num2) || this.IsCellAvailable(num3));
						break;
					case VineBranch.Shape.Bottom:
						flag = this.IsCellFoundation(num5) && (this.IsCellAvailable(num2) || this.IsCellAvailable(num3));
						break;
					case VineBranch.Shape.Left:
						flag = this.IsCellFoundation(num2) && (this.IsCellAvailable(num4) || this.IsCellAvailable(num5));
						break;
					case VineBranch.Shape.Right:
						flag = this.IsCellFoundation(num3) && (this.IsCellAvailable(num4) || this.IsCellAvailable(num5));
						break;
					case VineBranch.Shape.InCornerTopLeft:
						flag = this.IsCellFoundation(num4) && this.IsCellFoundation(num2);
						break;
					case VineBranch.Shape.InCornerTopRight:
						flag = this.IsCellFoundation(num4) && this.IsCellFoundation(num3);
						break;
					case VineBranch.Shape.InCornerBottomLeft:
						flag = this.IsCellFoundation(num5) && this.IsCellFoundation(num2);
						break;
					case VineBranch.Shape.InCornerBottomRight:
						flag = this.IsCellFoundation(num5) && this.IsCellFoundation(num3);
						break;
					case VineBranch.Shape.OutCornerTopLeft:
						flag = (this.IsCellAvailable(num4) || this.IsCellAvailable(num2)) && this.IsCellFoundation(num6);
						break;
					case VineBranch.Shape.OutCornerTopRight:
						flag = (this.IsCellAvailable(num4) || this.IsCellAvailable(num3)) && this.IsCellFoundation(num7);
						break;
					case VineBranch.Shape.OutCornerBottomLeft:
						flag = (this.IsCellAvailable(num5) || this.IsCellAvailable(num2)) && this.IsCellFoundation(num8);
						break;
					case VineBranch.Shape.OutCornerBottomRight:
						flag = (this.IsCellAvailable(num5) || this.IsCellAvailable(num3)) && this.IsCellFoundation(num9);
						break;
					case VineBranch.Shape.TopEnd:
						flag = !this.IsCellAvailable(num2) && !this.IsCellAvailable(num4) && !this.IsCellAvailable(num3);
						break;
					case VineBranch.Shape.BottomEnd:
						flag = !this.IsCellAvailable(num2) && !this.IsCellAvailable(num5) && !this.IsCellAvailable(num3);
						break;
					case VineBranch.Shape.LeftEnd:
						flag = !this.IsCellAvailable(num4) && !this.IsCellAvailable(num5) && !this.IsCellAvailable(num2);
						break;
					case VineBranch.Shape.RightEnd:
						flag = !this.IsCellAvailable(num4) && !this.IsCellAvailable(num5) && !this.IsCellAvailable(num3);
						break;
					}
					if (flag)
					{
						return shape2;
					}
				}
			}
			return shape;
		}

		// Token: 0x0600A726 RID: 42790 RVA: 0x003B0723 File Offset: 0x003AE923
		private void DelayedResetUprootMonitor(object o)
		{
			this.ResetUprootMonitor();
		}

		// Token: 0x0600A727 RID: 42791 RVA: 0x003B072C File Offset: 0x003AE92C
		public void ResetUprootMonitor()
		{
			CellOffset[] array = new CellOffset[0];
			if (!this.CanChangeShape && !this.MaxBranchNumberReached)
			{
				switch (this.MyShape)
				{
				case VineBranch.Shape.Top:
					array = new CellOffset[] { CellOffset.up };
					break;
				case VineBranch.Shape.Bottom:
					array = new CellOffset[] { CellOffset.down };
					break;
				case VineBranch.Shape.Left:
					array = new CellOffset[] { CellOffset.left };
					break;
				case VineBranch.Shape.Right:
					array = new CellOffset[] { CellOffset.right };
					break;
				case VineBranch.Shape.InCornerTopLeft:
					array = new CellOffset[]
					{
						CellOffset.up,
						CellOffset.left
					};
					break;
				case VineBranch.Shape.InCornerTopRight:
					array = new CellOffset[]
					{
						CellOffset.up,
						CellOffset.right
					};
					break;
				case VineBranch.Shape.InCornerBottomLeft:
					array = new CellOffset[]
					{
						CellOffset.down,
						CellOffset.left
					};
					break;
				case VineBranch.Shape.InCornerBottomRight:
					array = new CellOffset[]
					{
						CellOffset.down,
						CellOffset.right
					};
					break;
				case VineBranch.Shape.OutCornerTopLeft:
					array = new CellOffset[]
					{
						new CellOffset(-1, 1)
					};
					break;
				case VineBranch.Shape.OutCornerTopRight:
					array = new CellOffset[]
					{
						new CellOffset(1, 1)
					};
					break;
				case VineBranch.Shape.OutCornerBottomLeft:
					array = new CellOffset[]
					{
						new CellOffset(-1, -1)
					};
					break;
				case VineBranch.Shape.OutCornerBottomRight:
					array = new CellOffset[]
					{
						new CellOffset(1, -1)
					};
					break;
				case VineBranch.Shape.TopEnd:
					array = new CellOffset[] { this.IsGrowingClockwise ? CellOffset.left : CellOffset.right };
					break;
				case VineBranch.Shape.BottomEnd:
					array = new CellOffset[] { this.IsGrowingClockwise ? CellOffset.right : CellOffset.left };
					break;
				case VineBranch.Shape.LeftEnd:
					array = new CellOffset[] { this.IsGrowingClockwise ? CellOffset.down : CellOffset.up };
					break;
				case VineBranch.Shape.RightEnd:
					array = new CellOffset[] { this.IsGrowingClockwise ? CellOffset.up : CellOffset.down };
					break;
				}
			}
			this.uprootMonitor.SetNewMonitorCells(array);
		}

		// Token: 0x0600A728 RID: 42792 RVA: 0x003B099C File Offset: 0x003AEB9C
		public float TimeUntilNextHarvest()
		{
			float num = ((this.maturity.GetDelta() <= 0f) ? 0f : ((this.maturity.GetMax() - this.maturity.value) / this.maturity.GetDelta()));
			float num2 = ((this.fruitMaturity.GetDelta() <= 0f) ? 0f : ((this.fruitMaturity.GetMax() - this.fruitMaturity.value) / this.fruitMaturity.GetDelta()));
			return num + num2;
		}

		// Token: 0x0600A729 RID: 42793 RVA: 0x003B0A24 File Offset: 0x003AEC24
		public float GetCurrentGrowthPercentage()
		{
			if (!this.IsGrown)
			{
				return this.GrowthPercentage;
			}
			return this.FruitGrowthPercentage;
		}

		// Token: 0x0600A72A RID: 42794 RVA: 0x003B0A3B File Offset: 0x003AEC3B
		public float PercentGrown()
		{
			return this.GetCurrentGrowthPercentage();
		}

		// Token: 0x0600A72B RID: 42795 RVA: 0x003B0A43 File Offset: 0x003AEC43
		public Crop GetCropComponent()
		{
			return base.GetComponent<Crop>();
		}

		// Token: 0x0600A72C RID: 42796 RVA: 0x003B0A4B File Offset: 0x003AEC4B
		public float DomesticGrowthTime()
		{
			return this.maturity.GetMax() / this.baseGrowingRate.Value;
		}

		// Token: 0x0600A72D RID: 42797 RVA: 0x003B0A64 File Offset: 0x003AEC64
		public float WildGrowthTime()
		{
			return this.maturity.GetMax() / this.wildGrowingRate.Value;
		}

		// Token: 0x0600A72E RID: 42798 RVA: 0x003B0A80 File Offset: 0x003AEC80
		public void OverrideMaturityLevel(float percent)
		{
			float num = this.maturity.GetMax() * percent;
			this.maturity.SetValue(num);
		}

		// Token: 0x0600A72F RID: 42799 RVA: 0x003B0AA8 File Offset: 0x003AECA8
		public bool IsWildPlanted()
		{
			return this.IsWild;
		}

		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x0600A730 RID: 42800 RVA: 0x003B0AB0 File Offset: 0x003AECB0
		public string WiltStateString
		{
			get
			{
				return "    • " + DUPLICANTS.STATS.VINEMOTHERHEALTH.NAME;
			}
		}

		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x0600A731 RID: 42801 RVA: 0x003B0AC6 File Offset: 0x003AECC6
		public WiltCondition.Condition[] Conditions
		{
			get
			{
				return new WiltCondition.Condition[] { WiltCondition.Condition.UnhealthyRoot };
			}
		}

		// Token: 0x04008252 RID: 33362
		private bool isSpawningNextBranch;

		// Token: 0x04008253 RID: 33363
		public bool IsNewGameSpawned;

		// Token: 0x04008254 RID: 33364
		public AttributeModifier baseGrowingRate;

		// Token: 0x04008255 RID: 33365
		public AttributeModifier wildGrowingRate;

		// Token: 0x04008256 RID: 33366
		public AttributeModifier baseFruitGrowingRate;

		// Token: 0x04008257 RID: 33367
		public AttributeModifier wildFruitGrowingRate;

		// Token: 0x04008258 RID: 33368
		public AttributeModifier getOldRate;

		// Token: 0x04008259 RID: 33369
		public KBatchedAnimController AnimController;

		// Token: 0x0400825A RID: 33370
		private AmountInstance maturity;

		// Token: 0x0400825B RID: 33371
		private AmountInstance fruitMaturity;

		// Token: 0x0400825C RID: 33372
		private AmountInstance oldAge;

		// Token: 0x0400825D RID: 33373
		private WiltCondition wiltCondition;

		// Token: 0x0400825E RID: 33374
		private VineMother.Instance MotherSMI;

		// Token: 0x0400825F RID: 33375
		private UprootedMonitor uprootMonitor;

		// Token: 0x04008260 RID: 33376
		private Harvestable harvestable;

		// Token: 0x04008261 RID: 33377
		private MeterController fruitMeter;

		// Token: 0x04008262 RID: 33378
		private HandleVector<int>.Handle solidPartitionerEntry = HandleVector<int>.InvalidHandle;

		// Token: 0x04008263 RID: 33379
		private HandleVector<int>.Handle buildingsPartitionerEntry = HandleVector<int>.InvalidHandle;

		// Token: 0x04008264 RID: 33380
		private HandleVector<int>.Handle plantsPartitionerEntry = HandleVector<int>.InvalidHandle;

		// Token: 0x04008265 RID: 33381
		private HandleVector<int>.Handle liquidsPartitionerEntry = HandleVector<int>.InvalidHandle;

		// Token: 0x04008266 RID: 33382
		private bool wasMarkedForDeadBeforeStartSM;
	}
}
