using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x0200072C RID: 1836
public class FlushToilet : StateMachineComponent<FlushToilet.SMInstance>, IUsable, IGameObjectEffectDescriptor, IBasicBuilding
{
	// Token: 0x06002E50 RID: 11856 RVA: 0x0010966C File Offset: 0x0010786C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Building component = base.GetComponent<Building>();
		this.inputCell = component.GetUtilityInputCell();
		this.outputCell = component.GetUtilityOutputCell();
		ConduitFlow liquidConduitFlow = Game.Instance.liquidConduitFlow;
		liquidConduitFlow.onConduitsRebuilt += this.OnConduitsRebuilt;
		liquidConduitFlow.AddConduitUpdater(new Action<float>(this.OnConduitUpdate), ConduitFlowPriority.Default);
		KBatchedAnimController component2 = base.GetComponent<KBatchedAnimController>();
		this.fillMeter = new MeterController(component2, "meter_target", "meter", this.meterOffset, Grid.SceneLayer.NoLayer, new Vector3(0.4f, 3.2f, 0.1f), Array.Empty<string>());
		this.contaminationMeter = new MeterController(component2, "meter_target", "meter_dirty", this.meterOffset, Grid.SceneLayer.NoLayer, new Vector3(0.4f, 3.2f, 0.1f), Array.Empty<string>());
		this.gunkMeter = new MeterController(component2, "meter_target", "meter_gunky", this.meterOffset, Grid.SceneLayer.NoLayer, new Vector3(0.4f, 3.2f, 0.1f), Array.Empty<string>());
		Components.Toilets.Add(this);
		Components.BasicBuildings.Add(this);
		base.smi.StartSM();
		base.smi.ShowFillMeter();
	}

	// Token: 0x06002E51 RID: 11857 RVA: 0x001097A4 File Offset: 0x001079A4
	protected override void OnCleanUp()
	{
		Game.Instance.liquidConduitFlow.onConduitsRebuilt -= this.OnConduitsRebuilt;
		Components.BasicBuildings.Remove(this);
		Components.Toilets.Remove(this);
		base.OnCleanUp();
	}

	// Token: 0x06002E52 RID: 11858 RVA: 0x001097DD File Offset: 0x001079DD
	private void OnConduitsRebuilt()
	{
		base.Trigger(-2094018600, null);
	}

	// Token: 0x06002E53 RID: 11859 RVA: 0x001097EB File Offset: 0x001079EB
	public bool IsUsable()
	{
		return base.smi.HasTag(GameTags.Usable);
	}

	// Token: 0x06002E54 RID: 11860 RVA: 0x00109800 File Offset: 0x00107A00
	private void AddDisseaseToWorker(WorkerBase worker)
	{
		if (worker != null)
		{
			byte index = Db.Get().Diseases.GetIndex(this.diseaseId);
			worker.GetComponent<PrimaryElement>().AddDisease(index, this.diseaseOnDupePerFlush, "FlushToilet.Flush");
			PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Resource, string.Format(DUPLICANTS.DISEASES.ADDED_POPFX, Db.Get().Diseases[(int)index].Name, this.diseasePerFlush + this.diseaseOnDupePerFlush), base.transform, Vector3.up, 1.5f, false, false);
			Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_LotsOfGerms, true);
			return;
		}
		DebugUtil.LogWarningArgs(new object[] { "Tried to add disease on toilet use but worker was null" });
	}

	// Token: 0x06002E55 RID: 11861 RVA: 0x001098CC File Offset: 0x00107ACC
	private void Flush(WorkerBase worker)
	{
		ToiletWorkableUse component = base.GetComponent<ToiletWorkableUse>();
		ListPool<GameObject, Storage>.PooledList pooledList = ListPool<GameObject, Storage>.Allocate();
		this.storage.Find(FlushToilet.WaterTag, pooledList);
		float num = 0f;
		float num2 = this.massConsumedPerUse;
		foreach (GameObject gameObject in pooledList)
		{
			PrimaryElement component2 = gameObject.GetComponent<PrimaryElement>();
			float num3 = Mathf.Min(component2.Mass, num2);
			component2.Mass -= num3;
			num2 -= num3;
			num += num3 * component2.Temperature;
		}
		pooledList.Recycle();
		float lastAmountOfWasteMassRemovedFromDupe = component.lastAmountOfWasteMassRemovedFromDupe;
		num += lastAmountOfWasteMassRemovedFromDupe * this.newPeeTemperature;
		float num4 = this.massConsumedPerUse + lastAmountOfWasteMassRemovedFromDupe;
		float num5 = num / num4;
		byte index = Db.Get().Diseases.GetIndex(this.diseaseId);
		this.storage.AddLiquid(component.lastElementRemovedFromDupe, num4, num5, index, this.diseasePerFlush, false, true);
	}

	// Token: 0x06002E56 RID: 11862 RVA: 0x001099E0 File Offset: 0x00107BE0
	public List<Descriptor> RequirementDescriptors()
	{
		List<Descriptor> list = new List<Descriptor>();
		string text = ElementLoader.FindElementByHash(SimHashes.Water).tag.ProperName();
		list.Add(new Descriptor(string.Format(UI.BUILDINGEFFECTS.ELEMENTCONSUMEDPERUSE, text, GameUtil.GetFormattedMass(this.massConsumedPerUse, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.##}")), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.ELEMENTCONSUMEDPERUSE, text, GameUtil.GetFormattedMass(this.massConsumedPerUse, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.##}")), Descriptor.DescriptorType.Requirement, false));
		return list;
	}

	// Token: 0x06002E57 RID: 11863 RVA: 0x00109A5C File Offset: 0x00107C5C
	public List<Descriptor> EffectDescriptors()
	{
		List<Descriptor> list = new List<Descriptor>();
		string text = ElementLoader.FindElementByHash(SimHashes.DirtyWater).tag.ProperName();
		list.Add(new Descriptor(string.Format(UI.BUILDINGEFFECTS.ELEMENTEMITTED_TOILET, text, GameUtil.GetFormattedMass(this.massEmittedPerUse, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.##}"), GameUtil.GetFormattedTemperature(this.newPeeTemperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false)), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.ELEMENTEMITTED_TOILET, text, GameUtil.GetFormattedMass(this.massEmittedPerUse, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.##}"), GameUtil.GetFormattedTemperature(this.newPeeTemperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false)), Descriptor.DescriptorType.Effect, false));
		Disease disease = Db.Get().Diseases.Get(this.diseaseId);
		int num = this.diseasePerFlush + this.diseaseOnDupePerFlush;
		list.Add(new Descriptor(string.Format(UI.BUILDINGEFFECTS.DISEASEEMITTEDPERUSE, disease.Name, GameUtil.GetFormattedDiseaseAmount(num, GameUtil.TimeSlice.None)), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.DISEASEEMITTEDPERUSE, disease.Name, GameUtil.GetFormattedDiseaseAmount(num, GameUtil.TimeSlice.None)), Descriptor.DescriptorType.DiseaseSource, false));
		return list;
	}

	// Token: 0x06002E58 RID: 11864 RVA: 0x00109B5D File Offset: 0x00107D5D
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		list.AddRange(this.RequirementDescriptors());
		list.AddRange(this.EffectDescriptors());
		return list;
	}

	// Token: 0x06002E59 RID: 11865 RVA: 0x00109B7C File Offset: 0x00107D7C
	private void OnConduitUpdate(float dt)
	{
		if (this.GetSMI() == null)
		{
			return;
		}
		ConduitFlow liquidConduitFlow = Game.Instance.liquidConduitFlow;
		bool flag = base.smi.master.requireOutput && liquidConduitFlow.GetContents(this.outputCell).mass > 0f && base.smi.HasContaminatedMass();
		base.smi.sm.outputBlocked.Set(flag, base.smi, false);
	}

	// Token: 0x04001B59 RID: 7001
	private static readonly HashedString[] CLOGGED_ANIMS = new HashedString[] { "full_gunk_pre", "full_gunk" };

	// Token: 0x04001B5A RID: 7002
	private const string UNCLOG_ANIM = "full_gunk_pst";

	// Token: 0x04001B5B RID: 7003
	private MeterController fillMeter;

	// Token: 0x04001B5C RID: 7004
	private MeterController contaminationMeter;

	// Token: 0x04001B5D RID: 7005
	private MeterController gunkMeter;

	// Token: 0x04001B5E RID: 7006
	public Meter.Offset meterOffset = Meter.Offset.Behind;

	// Token: 0x04001B5F RID: 7007
	[SerializeField]
	public float massConsumedPerUse = 5f;

	// Token: 0x04001B60 RID: 7008
	[SerializeField]
	public float massEmittedPerUse = 5f;

	// Token: 0x04001B61 RID: 7009
	[SerializeField]
	public float newPeeTemperature;

	// Token: 0x04001B62 RID: 7010
	[SerializeField]
	public string diseaseId;

	// Token: 0x04001B63 RID: 7011
	[SerializeField]
	public int diseasePerFlush;

	// Token: 0x04001B64 RID: 7012
	[SerializeField]
	public int diseaseOnDupePerFlush;

	// Token: 0x04001B65 RID: 7013
	[SerializeField]
	public bool requireOutput = true;

	// Token: 0x04001B66 RID: 7014
	[MyCmpGet]
	private ConduitConsumer conduitConsumer;

	// Token: 0x04001B67 RID: 7015
	[MyCmpGet]
	private Storage storage;

	// Token: 0x04001B68 RID: 7016
	public static readonly Tag WaterTag = GameTagExtensions.Create(SimHashes.Water);

	// Token: 0x04001B69 RID: 7017
	private int inputCell;

	// Token: 0x04001B6A RID: 7018
	private int outputCell;

	// Token: 0x020015D6 RID: 5590
	public class SMInstance : GameStateMachine<FlushToilet.States, FlushToilet.SMInstance, FlushToilet, object>.GameInstance
	{
		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x060092D1 RID: 37585 RVA: 0x003683A9 File Offset: 0x003665A9
		public bool IsClogged
		{
			get
			{
				return base.sm.isClogged.Get(this);
			}
		}

		// Token: 0x060092D2 RID: 37586 RVA: 0x003683BC File Offset: 0x003665BC
		public SMInstance(FlushToilet master)
			: base(master)
		{
			this.activeUseChores = new List<Chore>();
			this.UpdateFullnessState();
			this.UpdateDirtyState();
		}

		// Token: 0x060092D3 RID: 37587 RVA: 0x003683E0 File Offset: 0x003665E0
		public void CreateCleanChore()
		{
			if (this.cleanChore != null)
			{
				this.cleanChore.Cancel("dupe");
			}
			ToiletWorkableClean component = base.GetComponent<ToiletWorkableClean>();
			component.SetIsCloggedByGunk(this.IsClogged);
			this.cleanChore = new WorkChore<ToiletWorkableClean>(Db.Get().ChoreTypes.CleanToilet, component, null, true, new Action<Chore>(this.OnCleanComplete), null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, true, true);
		}

		// Token: 0x060092D4 RID: 37588 RVA: 0x0036844F File Offset: 0x0036664F
		public void CancelCleanChore()
		{
			if (this.cleanChore != null)
			{
				this.cleanChore.Cancel("Cancelled");
				this.cleanChore = null;
			}
		}

		// Token: 0x060092D5 RID: 37589 RVA: 0x00368470 File Offset: 0x00366670
		private void OnCleanComplete(object o)
		{
			base.sm.isClogged.Set(false, this, false);
		}

		// Token: 0x060092D6 RID: 37590 RVA: 0x00368488 File Offset: 0x00366688
		public bool HasValidConnections()
		{
			return Game.Instance.liquidConduitFlow.HasConduit(base.master.inputCell) && (!base.master.requireOutput || Game.Instance.liquidConduitFlow.HasConduit(base.master.outputCell));
		}

		// Token: 0x060092D7 RID: 37591 RVA: 0x003684DC File Offset: 0x003666DC
		public bool UpdateFullnessState()
		{
			float num = 0f;
			ListPool<GameObject, FlushToilet>.PooledList pooledList = ListPool<GameObject, FlushToilet>.Allocate();
			base.master.storage.Find(FlushToilet.WaterTag, pooledList);
			foreach (GameObject gameObject in pooledList)
			{
				PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
				num += component.Mass;
			}
			pooledList.Recycle();
			bool flag = num >= base.master.massConsumedPerUse;
			base.master.conduitConsumer.enabled = !flag;
			float num2 = Mathf.Clamp01(num / base.master.massConsumedPerUse);
			base.master.fillMeter.SetPositionPercent(num2);
			return flag;
		}

		// Token: 0x060092D8 RID: 37592 RVA: 0x003685A8 File Offset: 0x003667A8
		public void SetDirtyStatesForClogged()
		{
			bool flag = base.GetComponent<ToiletWorkableUse>().last_user_id == BionicMinionConfig.ID;
			this.SetDirtyStateMeterPercentage((float)(flag ? 0 : 1), (float)(flag ? 1 : 0));
		}

		// Token: 0x060092D9 RID: 37593 RVA: 0x003685E6 File Offset: 0x003667E6
		public void SetDirtyStateMeterPercentage(float contaminationPercentage, float gunkPercentage)
		{
			base.master.contaminationMeter.SetPositionPercent(contaminationPercentage);
			base.master.gunkMeter.SetPositionPercent(gunkPercentage);
		}

		// Token: 0x060092DA RID: 37594 RVA: 0x0036860C File Offset: 0x0036680C
		public void UpdateDirtyState()
		{
			ToiletWorkableUse component = base.GetComponent<ToiletWorkableUse>();
			float percentComplete = component.GetPercentComplete();
			bool flag = component.last_user_id == BionicMinionConfig.ID;
			this.SetDirtyStateMeterPercentage(flag ? 0f : percentComplete, flag ? percentComplete : 0f);
		}

		// Token: 0x060092DB RID: 37595 RVA: 0x00368658 File Offset: 0x00366858
		public void AddDisseaseToWorker()
		{
			WorkerBase worker = base.master.GetComponent<ToiletWorkableUse>().worker;
			base.master.AddDisseaseToWorker(worker);
		}

		// Token: 0x060092DC RID: 37596 RVA: 0x00368684 File Offset: 0x00366884
		public void Flush()
		{
			bool flag = base.GetComponent<ToiletWorkableUse>().last_user_id == BionicMinionConfig.ID;
			base.master.fillMeter.SetPositionPercent(0f);
			base.master.contaminationMeter.SetPositionPercent(flag ? 0f : 1f);
			base.master.gunkMeter.SetPositionPercent(flag ? 1f : 0f);
			base.smi.ShowFillMeter();
			WorkerBase worker = base.master.GetComponent<ToiletWorkableUse>().worker;
			base.master.Flush(worker);
		}

		// Token: 0x060092DD RID: 37597 RVA: 0x00368728 File Offset: 0x00366928
		public void ShowFillMeter()
		{
			base.master.fillMeter.gameObject.SetActive(true);
			base.master.contaminationMeter.gameObject.SetActive(false);
			base.master.gunkMeter.gameObject.SetActive(false);
		}

		// Token: 0x060092DE RID: 37598 RVA: 0x00368778 File Offset: 0x00366978
		public bool HasContaminatedMass()
		{
			foreach (GameObject gameObject in base.GetComponent<Storage>().items)
			{
				PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
				if (!(component == null) && (component.ElementID == SimHashes.DirtyWater || component.ElementID == GunkMonitor.GunkElement) && component.Mass > 0f)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060092DF RID: 37599 RVA: 0x00368808 File Offset: 0x00366A08
		public void ShowContaminatedMeter()
		{
			bool flag = base.GetComponent<ToiletWorkableUse>().last_user_id == BionicMinionConfig.ID;
			base.master.fillMeter.gameObject.SetActive(false);
			base.master.contaminationMeter.gameObject.SetActive(!flag);
			base.master.gunkMeter.gameObject.SetActive(flag);
		}

		// Token: 0x040070F8 RID: 28920
		public List<Chore> activeUseChores;

		// Token: 0x040070F9 RID: 28921
		private Chore cleanChore;
	}

	// Token: 0x020015D7 RID: 5591
	public class States : GameStateMachine<FlushToilet.States, FlushToilet.SMInstance, FlushToilet>
	{
		// Token: 0x060092E0 RID: 37600 RVA: 0x00368878 File Offset: 0x00366A78
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.disconnected;
			base.serializable = StateMachine.SerializeType.ParamsOnly;
			this.disconnected.PlayAnim("off").EventTransition(GameHashes.ConduitConnectionChanged, this.backedup, (FlushToilet.SMInstance smi) => smi.HasValidConnections()).Enter(delegate(FlushToilet.SMInstance smi)
			{
				smi.GetComponent<Operational>().SetActive(false, false);
			});
			this.backedup.PlayAnim("off").ToggleStatusItem(Db.Get().BuildingStatusItems.OutputPipeFull, null).EventTransition(GameHashes.ConduitConnectionChanged, this.disconnected, (FlushToilet.SMInstance smi) => !smi.HasValidConnections())
				.ParamTransition<bool>(this.outputBlocked, this.fillingInactive, GameStateMachine<FlushToilet.States, FlushToilet.SMInstance, FlushToilet, object>.IsFalse)
				.Enter(delegate(FlushToilet.SMInstance smi)
				{
					smi.GetComponent<Operational>().SetActive(false, false);
				});
			this.filling.PlayAnim("on").Enter(delegate(FlushToilet.SMInstance smi)
			{
				smi.GetComponent<Operational>().SetActive(true, false);
			}).EventTransition(GameHashes.ConduitConnectionChanged, this.disconnected, (FlushToilet.SMInstance smi) => !smi.HasValidConnections())
				.ParamTransition<bool>(this.outputBlocked, this.backedup, GameStateMachine<FlushToilet.States, FlushToilet.SMInstance, FlushToilet, object>.IsTrue)
				.EventTransition(GameHashes.OnStorageChange, this.ready, (FlushToilet.SMInstance smi) => smi.UpdateFullnessState())
				.EventTransition(GameHashes.OperationalChanged, this.fillingInactive, (FlushToilet.SMInstance smi) => !smi.GetComponent<Operational>().IsOperational);
			this.fillingInactive.PlayAnim("on").Enter(delegate(FlushToilet.SMInstance smi)
			{
				smi.GetComponent<Operational>().SetActive(false, false);
			}).EventTransition(GameHashes.OperationalChanged, this.filling, (FlushToilet.SMInstance smi) => smi.GetComponent<Operational>().IsOperational)
				.ParamTransition<bool>(this.outputBlocked, this.backedup, GameStateMachine<FlushToilet.States, FlushToilet.SMInstance, FlushToilet, object>.IsTrue);
			this.ready.DefaultState(this.ready.idle).ToggleTag(GameTags.Usable).Enter(delegate(FlushToilet.SMInstance smi)
			{
				smi.master.fillMeter.SetPositionPercent(1f);
				smi.master.contaminationMeter.SetPositionPercent(0f);
				smi.master.gunkMeter.SetPositionPercent(0f);
			})
				.PlayAnim("on")
				.EventHandler(GameHashes.FlushGunk, new GameStateMachine<FlushToilet.States, FlushToilet.SMInstance, FlushToilet, object>.GameEvent.Callback(this.OnFlushedGunk))
				.EventTransition(GameHashes.ConduitConnectionChanged, this.disconnected, (FlushToilet.SMInstance smi) => !smi.HasValidConnections())
				.ParamTransition<bool>(this.outputBlocked, this.backedup, GameStateMachine<FlushToilet.States, FlushToilet.SMInstance, FlushToilet, object>.IsTrue)
				.ToggleRecurringChore(new Func<FlushToilet.SMInstance, Chore>(this.CreateUrgentUseChore), null)
				.ToggleRecurringChore(new Func<FlushToilet.SMInstance, Chore>(this.CreateBreakUseChore), null);
			this.ready.idle.ParamTransition<bool>(this.isClogged, this.clogged, GameStateMachine<FlushToilet.States, FlushToilet.SMInstance, FlushToilet, object>.IsTrue).Enter(delegate(FlushToilet.SMInstance smi)
			{
				smi.GetComponent<Operational>().SetActive(false, false);
			}).ToggleMainStatusItem(Db.Get().BuildingStatusItems.FlushToilet, null)
				.WorkableStartTransition((FlushToilet.SMInstance smi) => smi.master.GetComponent<ToiletWorkableUse>(), this.ready.inuse);
			this.ready.inuse.Enter(delegate(FlushToilet.SMInstance smi)
			{
				smi.ShowContaminatedMeter();
			}).ToggleMainStatusItem(Db.Get().BuildingStatusItems.FlushToiletInUse, null).Update(delegate(FlushToilet.SMInstance smi, float dt)
			{
				smi.UpdateDirtyState();
			}, UpdateRate.SIM_200ms, false)
				.WorkableCompleteTransition((FlushToilet.SMInstance smi) => smi.master.GetComponent<ToiletWorkableUse>(), this.ready.completed)
				.WorkableStopTransition((FlushToilet.SMInstance smi) => smi.master.GetComponent<ToiletWorkableUse>(), this.flushed);
			this.ready.completed.Enter(delegate(FlushToilet.SMInstance smi)
			{
				smi.AddDisseaseToWorker();
			}).EnterTransition(this.clogged, (FlushToilet.SMInstance smi) => smi.IsClogged).EnterGoTo(this.flushing);
			this.clogged.PlayAnims((FlushToilet.SMInstance smi) => FlushToilet.CLOGGED_ANIMS, KAnim.PlayMode.Once).Enter(delegate(FlushToilet.SMInstance smi)
			{
				smi.ShowContaminatedMeter();
			}).Enter(new StateMachine<FlushToilet.States, FlushToilet.SMInstance, FlushToilet, object>.State.Callback(this.SetDirtyStatesForClogged))
				.Enter(new StateMachine<FlushToilet.States, FlushToilet.SMInstance, FlushToilet, object>.State.Callback(this.CreateCleanChore))
				.Exit(new StateMachine<FlushToilet.States, FlushToilet.SMInstance, FlushToilet, object>.State.Callback(this.CancelCleanChore))
				.ParamTransition<bool>(this.isClogged, this.unclogged, GameStateMachine<FlushToilet.States, FlushToilet.SMInstance, FlushToilet, object>.IsFalse);
			this.unclogged.PlayAnim("full_gunk_pst").OnAnimQueueComplete(this.flushing);
			this.flushing.Enter(delegate(FlushToilet.SMInstance smi)
			{
				smi.Flush();
			}).PlayAnim("flush").OnAnimQueueComplete(this.flushed);
			this.flushed.EventTransition(GameHashes.OnStorageChange, this.fillingInactive, (FlushToilet.SMInstance smi) => !smi.HasContaminatedMass()).ParamTransition<bool>(this.outputBlocked, this.backedup, GameStateMachine<FlushToilet.States, FlushToilet.SMInstance, FlushToilet, object>.IsTrue).PlayAnim("on");
		}

		// Token: 0x060092E1 RID: 37601 RVA: 0x00368EA5 File Offset: 0x003670A5
		public void OnFlushedGunk(FlushToilet.SMInstance smi, object o)
		{
			smi.sm.isClogged.Set(true, smi, false);
		}

		// Token: 0x060092E2 RID: 37602 RVA: 0x00368EBB File Offset: 0x003670BB
		public void SetDirtyStatesForClogged(FlushToilet.SMInstance smi)
		{
			smi.SetDirtyStatesForClogged();
		}

		// Token: 0x060092E3 RID: 37603 RVA: 0x00368EC3 File Offset: 0x003670C3
		public void CreateCleanChore(FlushToilet.SMInstance smi)
		{
			smi.CreateCleanChore();
		}

		// Token: 0x060092E4 RID: 37604 RVA: 0x00368ECB File Offset: 0x003670CB
		public void CancelCleanChore(FlushToilet.SMInstance smi)
		{
			smi.CancelCleanChore();
		}

		// Token: 0x060092E5 RID: 37605 RVA: 0x00368ED3 File Offset: 0x003670D3
		private Chore CreateUrgentUseChore(FlushToilet.SMInstance smi)
		{
			Chore chore = this.CreateUseChore(smi, Db.Get().ChoreTypes.Pee);
			chore.AddPrecondition(ChorePreconditions.instance.IsBladderFull, null);
			chore.AddPrecondition(ChorePreconditions.instance.NotCurrentlyPeeing, null);
			return chore;
		}

		// Token: 0x060092E6 RID: 37606 RVA: 0x00368F0D File Offset: 0x0036710D
		private Chore CreateBreakUseChore(FlushToilet.SMInstance smi)
		{
			Chore chore = this.CreateUseChore(smi, Db.Get().ChoreTypes.BreakPee);
			chore.AddPrecondition(ChorePreconditions.instance.IsBladderNotFull, null);
			return chore;
		}

		// Token: 0x060092E7 RID: 37607 RVA: 0x00368F38 File Offset: 0x00367138
		private Chore CreateUseChore(FlushToilet.SMInstance smi, ChoreType choreType)
		{
			WorkChore<ToiletWorkableUse> workChore = new WorkChore<ToiletWorkableUse>(choreType, smi.master, null, true, null, null, null, false, null, true, true, null, false, true, false, PriorityScreen.PriorityClass.personalNeeds, 5, false, false);
			smi.activeUseChores.Add(workChore);
			WorkChore<ToiletWorkableUse> workChore2 = workChore;
			workChore2.onExit = (Action<Chore>)Delegate.Combine(workChore2.onExit, new Action<Chore>(delegate(Chore exiting_chore)
			{
				smi.activeUseChores.Remove(exiting_chore);
			}));
			workChore.AddPrecondition(ChorePreconditions.instance.IsPreferredAssignableOrUrgentBladder, smi.master.GetComponent<Assignable>());
			workChore.AddPrecondition(ChorePreconditions.instance.IsExclusivelyAvailableWithOtherChores, smi.activeUseChores);
			return workChore;
		}

		// Token: 0x040070FA RID: 28922
		public GameStateMachine<FlushToilet.States, FlushToilet.SMInstance, FlushToilet, object>.State disconnected;

		// Token: 0x040070FB RID: 28923
		public GameStateMachine<FlushToilet.States, FlushToilet.SMInstance, FlushToilet, object>.State backedup;

		// Token: 0x040070FC RID: 28924
		public FlushToilet.States.ReadyStates ready;

		// Token: 0x040070FD RID: 28925
		public GameStateMachine<FlushToilet.States, FlushToilet.SMInstance, FlushToilet, object>.State fillingInactive;

		// Token: 0x040070FE RID: 28926
		public GameStateMachine<FlushToilet.States, FlushToilet.SMInstance, FlushToilet, object>.State filling;

		// Token: 0x040070FF RID: 28927
		public GameStateMachine<FlushToilet.States, FlushToilet.SMInstance, FlushToilet, object>.State clogged;

		// Token: 0x04007100 RID: 28928
		public GameStateMachine<FlushToilet.States, FlushToilet.SMInstance, FlushToilet, object>.State unclogged;

		// Token: 0x04007101 RID: 28929
		public GameStateMachine<FlushToilet.States, FlushToilet.SMInstance, FlushToilet, object>.State flushing;

		// Token: 0x04007102 RID: 28930
		public GameStateMachine<FlushToilet.States, FlushToilet.SMInstance, FlushToilet, object>.State flushed;

		// Token: 0x04007103 RID: 28931
		public StateMachine<FlushToilet.States, FlushToilet.SMInstance, FlushToilet, object>.BoolParameter outputBlocked;

		// Token: 0x04007104 RID: 28932
		public StateMachine<FlushToilet.States, FlushToilet.SMInstance, FlushToilet, object>.BoolParameter isClogged;

		// Token: 0x0200277C RID: 10108
		public class ReadyStates : GameStateMachine<FlushToilet.States, FlushToilet.SMInstance, FlushToilet, object>.State
		{
			// Token: 0x0400AE8F RID: 44687
			public GameStateMachine<FlushToilet.States, FlushToilet.SMInstance, FlushToilet, object>.State idle;

			// Token: 0x0400AE90 RID: 44688
			public GameStateMachine<FlushToilet.States, FlushToilet.SMInstance, FlushToilet, object>.State inuse;

			// Token: 0x0400AE91 RID: 44689
			public GameStateMachine<FlushToilet.States, FlushToilet.SMInstance, FlushToilet, object>.State completed;
		}
	}
}
