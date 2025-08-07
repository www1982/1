using System;
using System.Collections.Generic;
using Klei;
using Klei.AI;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020007E3 RID: 2019
public class Toilet : StateMachineComponent<Toilet.StatesInstance>, ISaveLoadable, IUsable, IGameObjectEffectDescriptor, IBasicBuilding
{
	// Token: 0x170003A8 RID: 936
	// (get) Token: 0x060036A6 RID: 13990 RVA: 0x0012FDAB File Offset: 0x0012DFAB
	// (set) Token: 0x060036A7 RID: 13991 RVA: 0x0012FDB3 File Offset: 0x0012DFB3
	public int FlushesUsed
	{
		get
		{
			return this._flushesUsed;
		}
		set
		{
			this._flushesUsed = value;
			base.smi.sm.flushes.Set(value, base.smi, false);
		}
	}

	// Token: 0x060036A8 RID: 13992 RVA: 0x0012FDDC File Offset: 0x0012DFDC
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Components.Toilets.Add(this);
		Components.BasicBuildings.Add(this);
		base.smi.StartSM();
		base.GetComponent<ToiletWorkableUse>().trackUses = true;
		this.meter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.Behind, Grid.SceneLayer.NoLayer, new string[] { "meter_target", "meter_arrow", "meter_scale" });
		this.meter.SetPositionPercent((float)this.FlushesUsed / (float)this.maxFlushes);
		this.FlushesUsed = this._flushesUsed;
		base.Subscribe<Toilet>(493375141, Toilet.OnRefreshUserMenuDelegate);
	}

	// Token: 0x060036A9 RID: 13993 RVA: 0x0012FE8F File Offset: 0x0012E08F
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Components.BasicBuildings.Remove(this);
		Components.Toilets.Remove(this);
	}

	// Token: 0x060036AA RID: 13994 RVA: 0x0012FEAD File Offset: 0x0012E0AD
	public bool IsUsable()
	{
		return base.smi.HasTag(GameTags.Usable);
	}

	// Token: 0x060036AB RID: 13995 RVA: 0x0012FEBF File Offset: 0x0012E0BF
	public void Flush(WorkerBase worker)
	{
		this.FlushMultiple(worker, 1);
	}

	// Token: 0x060036AC RID: 13996 RVA: 0x0012FECC File Offset: 0x0012E0CC
	public void FlushMultiple(WorkerBase worker, int flushCount)
	{
		int num = this.maxFlushes - this.FlushesUsed;
		int num2 = Mathf.Min(flushCount, num);
		this.FlushesUsed += num2;
		this.meter.SetPositionPercent((float)this.FlushesUsed / (float)this.maxFlushes);
		float num3 = 0f;
		Tag tag = ElementLoader.FindElementByHash(SimHashes.Dirt).tag;
		float num4;
		SimUtil.DiseaseInfo diseaseInfo;
		this.storage.ConsumeAndGetDisease(tag, base.smi.DirtUsedPerFlush() * (float)num2, out num4, out diseaseInfo, out num3);
		byte index = Db.Get().Diseases.GetIndex(this.diseaseId);
		int num5 = this.diseasePerFlush * num2;
		float num6 = base.smi.MassPerFlush() + num4;
		GameObject gameObject = ElementLoader.FindElementByHash(this.solidWastePerUse.elementID).substance.SpawnResource(base.transform.GetPosition(), num6, this.solidWasteTemperature, index, num5, true, false, false);
		gameObject.GetComponent<PrimaryElement>().AddDisease(diseaseInfo.idx, diseaseInfo.count, "Toilet.Flush");
		num5 += diseaseInfo.count;
		this.storage.Store(gameObject, false, false, true, false);
		int num7 = this.diseaseOnDupePerFlush * num2;
		worker.GetComponent<PrimaryElement>().AddDisease(index, num7, "Toilet.Flush");
		PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Resource, string.Format(DUPLICANTS.DISEASES.ADDED_POPFX, Db.Get().Diseases[(int)index].Name, num5 + num7), base.transform, Vector3.up, 1.5f, false, false);
		Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_LotsOfGerms, true);
	}

	// Token: 0x060036AD RID: 13997 RVA: 0x00130078 File Offset: 0x0012E278
	private void OnRefreshUserMenu(object data)
	{
		if (base.smi.GetCurrentState() == base.smi.sm.full || !base.smi.IsSoiled || base.smi.cleanChore != null)
		{
			return;
		}
		Game.Instance.userMenu.AddButton(base.gameObject, new KIconButtonMenu.ButtonInfo("status_item_toilet_needs_emptying", UI.USERMENUACTIONS.CLEANTOILET.NAME, delegate
		{
			base.smi.GoTo(base.smi.sm.earlyclean);
		}, global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.CLEANTOILET.TOOLTIP, true), 1f);
	}

	// Token: 0x060036AE RID: 13998 RVA: 0x0013010A File Offset: 0x0012E30A
	private void SpawnMonster()
	{
		GameUtil.KInstantiate(Assets.GetPrefab(new Tag("Glom")), base.smi.transform.GetPosition(), Grid.SceneLayer.Creatures, null, 0).SetActive(true);
	}

	// Token: 0x060036AF RID: 13999 RVA: 0x0013013C File Offset: 0x0012E33C
	public List<Descriptor> RequirementDescriptors()
	{
		List<Descriptor> list = new List<Descriptor>();
		string text = base.GetComponent<ManualDeliveryKG>().RequestedItemTag.ProperName();
		float num = base.smi.DirtUsedPerFlush();
		Descriptor descriptor = default(Descriptor);
		descriptor.SetupDescriptor(string.Format(UI.BUILDINGEFFECTS.ELEMENTCONSUMEDPERUSE, text, GameUtil.GetFormattedMass(num, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.##}")), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.ELEMENTCONSUMEDPERUSE, text, GameUtil.GetFormattedMass(num, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.##}")), Descriptor.DescriptorType.Requirement);
		list.Add(descriptor);
		return list;
	}

	// Token: 0x060036B0 RID: 14000 RVA: 0x001301C0 File Offset: 0x0012E3C0
	public List<Descriptor> EffectDescriptors()
	{
		List<Descriptor> list = new List<Descriptor>();
		string text = ElementLoader.FindElementByHash(this.solidWastePerUse.elementID).tag.ProperName();
		float num = base.smi.MassPerFlush() + base.smi.DirtUsedPerFlush();
		list.Add(new Descriptor(string.Format(UI.BUILDINGEFFECTS.ELEMENTEMITTED_TOILET, text, GameUtil.GetFormattedMass(num, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.##}"), GameUtil.GetFormattedTemperature(this.solidWasteTemperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false)), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.ELEMENTEMITTED_TOILET, text, GameUtil.GetFormattedMass(num, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.##}"), GameUtil.GetFormattedTemperature(this.solidWasteTemperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false)), Descriptor.DescriptorType.Effect, false));
		Disease disease = Db.Get().Diseases.Get(this.diseaseId);
		int num2 = this.diseasePerFlush + this.diseaseOnDupePerFlush;
		list.Add(new Descriptor(string.Format(UI.BUILDINGEFFECTS.DISEASEEMITTEDPERUSE, disease.Name, GameUtil.GetFormattedDiseaseAmount(num2, GameUtil.TimeSlice.None)), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.DISEASEEMITTEDPERUSE, disease.Name, GameUtil.GetFormattedDiseaseAmount(num2, GameUtil.TimeSlice.None)), Descriptor.DescriptorType.DiseaseSource, false));
		return list;
	}

	// Token: 0x060036B1 RID: 14001 RVA: 0x001302D5 File Offset: 0x0012E4D5
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		list.AddRange(this.RequirementDescriptors());
		list.AddRange(this.EffectDescriptors());
		return list;
	}

	// Token: 0x040020F5 RID: 8437
	private static readonly HashedString[] FULL_ANIMS = new HashedString[] { "full_pre", "full" };

	// Token: 0x040020F6 RID: 8438
	private const string EXIT_FULL_ANIM_NAME = "full_pst";

	// Token: 0x040020F7 RID: 8439
	private const string EXIT_FULL_GUNK_ANIM_NAME = "full_gunk_pst";

	// Token: 0x040020F8 RID: 8440
	private static readonly HashedString[] GUNK_CLOGGED_ANIMS = new HashedString[] { "full_gunk_pre", "full_gunk" };

	// Token: 0x040020F9 RID: 8441
	[SerializeField]
	public Toilet.SpawnInfo solidWastePerUse;

	// Token: 0x040020FA RID: 8442
	[SerializeField]
	public float solidWasteTemperature;

	// Token: 0x040020FB RID: 8443
	[SerializeField]
	public Toilet.SpawnInfo gasWasteWhenFull;

	// Token: 0x040020FC RID: 8444
	[SerializeField]
	public int maxFlushes = 15;

	// Token: 0x040020FD RID: 8445
	[SerializeField]
	public string diseaseId;

	// Token: 0x040020FE RID: 8446
	[SerializeField]
	public int diseasePerFlush;

	// Token: 0x040020FF RID: 8447
	[SerializeField]
	public int diseaseOnDupePerFlush;

	// Token: 0x04002100 RID: 8448
	[SerializeField]
	public float dirtUsedPerFlush = 13f;

	// Token: 0x04002101 RID: 8449
	[Serialize]
	public int _flushesUsed;

	// Token: 0x04002102 RID: 8450
	private MeterController meter;

	// Token: 0x04002103 RID: 8451
	[MyCmpReq]
	private Storage storage;

	// Token: 0x04002104 RID: 8452
	[MyCmpReq]
	private ManualDeliveryKG manualdeliverykg;

	// Token: 0x04002105 RID: 8453
	private static readonly EventSystem.IntraObjectHandler<Toilet> OnRefreshUserMenuDelegate = new EventSystem.IntraObjectHandler<Toilet>(delegate(Toilet component, object data)
	{
		component.OnRefreshUserMenu(data);
	});

	// Token: 0x02001732 RID: 5938
	[Serializable]
	public struct SpawnInfo
	{
		// Token: 0x06009823 RID: 38947 RVA: 0x0037F859 File Offset: 0x0037DA59
		public SpawnInfo(SimHashes element_id, float mass, float interval)
		{
			this.elementID = element_id;
			this.mass = mass;
			this.interval = interval;
		}

		// Token: 0x040074F7 RID: 29943
		[HashedEnum]
		public SimHashes elementID;

		// Token: 0x040074F8 RID: 29944
		public float mass;

		// Token: 0x040074F9 RID: 29945
		public float interval;
	}

	// Token: 0x02001733 RID: 5939
	public class StatesInstance : GameStateMachine<Toilet.States, Toilet.StatesInstance, Toilet, object>.GameInstance
	{
		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x06009824 RID: 38948 RVA: 0x0037F870 File Offset: 0x0037DA70
		public bool IsCloggedWithGunk
		{
			get
			{
				return base.sm.cloggedWithGunk.Get(this);
			}
		}

		// Token: 0x06009825 RID: 38949 RVA: 0x0037F883 File Offset: 0x0037DA83
		public StatesInstance(Toilet master)
			: base(master)
		{
			this.activeUseChores = new List<Chore>();
		}

		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x06009826 RID: 38950 RVA: 0x0037F8A2 File Offset: 0x0037DAA2
		public bool IsSoiled
		{
			get
			{
				return base.master.FlushesUsed > 0;
			}
		}

		// Token: 0x06009827 RID: 38951 RVA: 0x0037F8B2 File Offset: 0x0037DAB2
		public int GetFlushesRemaining()
		{
			return base.master.maxFlushes - base.master.FlushesUsed;
		}

		// Token: 0x06009828 RID: 38952 RVA: 0x0037F8CC File Offset: 0x0037DACC
		public bool RequiresDirtDelivery()
		{
			return base.master.storage.IsEmpty() || !base.master.storage.Has(GameTags.Dirt) || (base.master.storage.GetAmountAvailable(GameTags.Dirt) < base.master.manualdeliverykg.capacity && !this.IsSoiled);
		}

		// Token: 0x06009829 RID: 38953 RVA: 0x0037F93D File Offset: 0x0037DB3D
		public float MassPerFlush()
		{
			return base.master.solidWastePerUse.mass;
		}

		// Token: 0x0600982A RID: 38954 RVA: 0x0037F94F File Offset: 0x0037DB4F
		public float DirtUsedPerFlush()
		{
			return base.master.dirtUsedPerFlush;
		}

		// Token: 0x0600982B RID: 38955 RVA: 0x0037F95C File Offset: 0x0037DB5C
		public bool IsToxicSandRemoved()
		{
			Tag tag = GameTagExtensions.Create(base.master.solidWastePerUse.elementID);
			return base.master.storage.FindFirst(tag) == null;
		}

		// Token: 0x0600982C RID: 38956 RVA: 0x0037F998 File Offset: 0x0037DB98
		public void CreateCleanChore()
		{
			if (this.cleanChore != null)
			{
				this.cleanChore.Cancel("dupe");
			}
			ToiletWorkableClean component = base.master.GetComponent<ToiletWorkableClean>();
			component.SetIsCloggedByGunk(this.IsCloggedWithGunk);
			this.cleanChore = new WorkChore<ToiletWorkableClean>(Db.Get().ChoreTypes.CleanToilet, component, null, true, new Action<Chore>(this.OnCleanComplete), null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, true, true);
		}

		// Token: 0x0600982D RID: 38957 RVA: 0x0037FA0C File Offset: 0x0037DC0C
		public void CancelCleanChore()
		{
			if (this.cleanChore != null)
			{
				this.cleanChore.Cancel("Cancelled");
				this.cleanChore = null;
			}
		}

		// Token: 0x0600982E RID: 38958 RVA: 0x0037FA30 File Offset: 0x0037DC30
		private void DropFromStorage(Tag tag)
		{
			ListPool<GameObject, Toilet>.PooledList pooledList = ListPool<GameObject, Toilet>.Allocate();
			base.master.storage.Find(tag, pooledList);
			foreach (GameObject gameObject in pooledList)
			{
				base.master.storage.Drop(gameObject, true);
			}
			pooledList.Recycle();
		}

		// Token: 0x0600982F RID: 38959 RVA: 0x0037FAAC File Offset: 0x0037DCAC
		private void OnCleanComplete(Chore chore)
		{
			this.cleanChore = null;
			Tag tag = GameTagExtensions.Create(base.master.solidWastePerUse.elementID);
			Tag tag2 = ElementLoader.FindElementByHash(SimHashes.Dirt).tag;
			this.DropFromStorage(tag);
			this.DropFromStorage(tag2);
			base.sm.cloggedWithGunk.Set(false, this, false);
			base.master.meter.SetPositionPercent((float)base.master.FlushesUsed / (float)base.master.maxFlushes);
		}

		// Token: 0x06009830 RID: 38960 RVA: 0x0037FB34 File Offset: 0x0037DD34
		public void Flush()
		{
			WorkerBase worker = base.master.GetComponent<ToiletWorkableUse>().worker;
			base.master.Flush(worker);
		}

		// Token: 0x06009831 RID: 38961 RVA: 0x0037FB60 File Offset: 0x0037DD60
		public void FlushAll()
		{
			WorkerBase worker = base.master.GetComponent<ToiletWorkableUse>().worker;
			base.master.FlushMultiple(worker, base.master.maxFlushes - base.master.FlushesUsed);
		}

		// Token: 0x06009832 RID: 38962 RVA: 0x0037FBA1 File Offset: 0x0037DDA1
		public void FlushGunk()
		{
			base.sm.cloggedWithGunk.Set(true, this, false);
			this.Flush();
		}

		// Token: 0x06009833 RID: 38963 RVA: 0x0037FBBD File Offset: 0x0037DDBD
		public HashedString[] GetCloggedAnimations()
		{
			if (this.IsCloggedWithGunk)
			{
				return Toilet.GUNK_CLOGGED_ANIMS;
			}
			return Toilet.FULL_ANIMS;
		}

		// Token: 0x040074FA RID: 29946
		public Chore cleanChore;

		// Token: 0x040074FB RID: 29947
		public List<Chore> activeUseChores;

		// Token: 0x040074FC RID: 29948
		public float monsterSpawnTime = 1200f;
	}

	// Token: 0x02001734 RID: 5940
	public class States : GameStateMachine<Toilet.States, Toilet.StatesInstance, Toilet>
	{
		// Token: 0x06009834 RID: 38964 RVA: 0x0037FBD4 File Offset: 0x0037DDD4
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.needsdirt;
			base.serializable = StateMachine.SerializeType.ParamsOnly;
			this.root.PlayAnim("off").EventTransition(GameHashes.OnStorageChange, this.needsdirt, (Toilet.StatesInstance smi) => smi.RequiresDirtDelivery()).EventTransition(GameHashes.OperationalChanged, this.notoperational, (Toilet.StatesInstance smi) => !smi.Get<Operational>().IsOperational);
			this.needsdirt.Enter(delegate(Toilet.StatesInstance smi)
			{
				if (smi.RequiresDirtDelivery())
				{
					smi.master.manualdeliverykg.RequestDelivery();
				}
			}).ToggleMainStatusItem(Db.Get().BuildingStatusItems.Unusable, null).EventTransition(GameHashes.OnStorageChange, this.ready, (Toilet.StatesInstance smi) => !smi.RequiresDirtDelivery());
			this.ready.ParamTransition<int>(this.flushes, this.full, (Toilet.StatesInstance smi, int p) => smi.GetFlushesRemaining() <= 0).ParamTransition<int>(this.flushes, this.earlyclean, (Toilet.StatesInstance smi, int p) => smi.IsCloggedWithGunk).ToggleMainStatusItem(Db.Get().BuildingStatusItems.Toilet, null)
				.ToggleRecurringChore(new Func<Toilet.StatesInstance, Chore>(this.CreateUrgentUseChore), null)
				.ToggleRecurringChore(new Func<Toilet.StatesInstance, Chore>(this.CreateBreakUseChore), null)
				.ToggleTag(GameTags.Usable)
				.EventHandler(GameHashes.Flush, delegate(Toilet.StatesInstance smi, object data)
				{
					smi.Flush();
				})
				.EventHandler(GameHashes.FlushGunk, delegate(Toilet.StatesInstance smi, object data)
				{
					smi.FlushGunk();
				});
			this.earlyclean.PlayAnims(new Func<Toilet.StatesInstance, HashedString[]>(Toilet.States.GetCloggedAnimations), KAnim.PlayMode.Once).OnAnimQueueComplete(this.earlyWaitingForClean);
			this.earlyWaitingForClean.Enter(delegate(Toilet.StatesInstance smi)
			{
				smi.CreateCleanChore();
			}).Exit(delegate(Toilet.StatesInstance smi)
			{
				smi.CancelCleanChore();
			}).ToggleStatusItem(Db.Get().BuildingStatusItems.ToiletNeedsEmptying, null)
				.ToggleMainStatusItem(delegate(Toilet.StatesInstance smi)
				{
					if (!smi.sm.cloggedWithGunk.Get(smi))
					{
						return Db.Get().BuildingStatusItems.Unusable;
					}
					return Db.Get().BuildingStatusItems.UnusableGunked;
				}, null)
				.EventTransition(GameHashes.OnStorageChange, this.exit_full, (Toilet.StatesInstance smi) => smi.IsToxicSandRemoved());
			this.full.PlayAnims(new Func<Toilet.StatesInstance, HashedString[]>(Toilet.States.GetCloggedAnimations), KAnim.PlayMode.Once).OnAnimQueueComplete(this.fullWaitingForClean);
			this.fullWaitingForClean.Enter(delegate(Toilet.StatesInstance smi)
			{
				smi.CreateCleanChore();
			}).Exit(delegate(Toilet.StatesInstance smi)
			{
				smi.CancelCleanChore();
			}).ToggleStatusItem(Db.Get().BuildingStatusItems.ToiletNeedsEmptying, null)
				.ToggleMainStatusItem(delegate(Toilet.StatesInstance smi)
				{
					if (!smi.sm.cloggedWithGunk.Get(smi))
					{
						return Db.Get().BuildingStatusItems.Unusable;
					}
					return Db.Get().BuildingStatusItems.UnusableGunked;
				}, null)
				.EventTransition(GameHashes.OnStorageChange, this.exit_full, (Toilet.StatesInstance smi) => smi.IsToxicSandRemoved())
				.Enter(delegate(Toilet.StatesInstance smi)
				{
					smi.Schedule(smi.monsterSpawnTime, delegate
					{
						smi.master.SpawnMonster();
					}, null);
				});
			this.exit_full.PlayAnim(new Func<Toilet.StatesInstance, string>(Toilet.States.GetUnclogedAnimation), KAnim.PlayMode.Once).OnAnimQueueComplete(this.empty).Exit(new StateMachine<Toilet.States, Toilet.StatesInstance, Toilet, object>.State.Callback(Toilet.States.ClearCloggedByGunkFlag))
				.ScheduleGoTo(0.74f, this.empty);
			this.empty.PlayAnim("off").Enter("ClearFlushes", delegate(Toilet.StatesInstance smi)
			{
				smi.master.FlushesUsed = 0;
			}).GoTo(this.needsdirt);
			this.notoperational.EventTransition(GameHashes.OperationalChanged, this.needsdirt, (Toilet.StatesInstance smi) => smi.Get<Operational>().IsOperational).ToggleMainStatusItem(Db.Get().BuildingStatusItems.Unusable, null);
		}

		// Token: 0x06009835 RID: 38965 RVA: 0x0038007B File Offset: 0x0037E27B
		private static void ClearCloggedByGunkFlag(Toilet.StatesInstance smi)
		{
			smi.sm.cloggedWithGunk.Set(false, smi, false);
		}

		// Token: 0x06009836 RID: 38966 RVA: 0x00380091 File Offset: 0x0037E291
		public static string GetUnclogedAnimation(Toilet.StatesInstance smi)
		{
			if (!smi.sm.cloggedWithGunk.Get(smi))
			{
				return "full_pst";
			}
			return "full_gunk_pst";
		}

		// Token: 0x06009837 RID: 38967 RVA: 0x003800B1 File Offset: 0x0037E2B1
		public static HashedString[] GetCloggedAnimations(Toilet.StatesInstance smi)
		{
			return smi.GetCloggedAnimations();
		}

		// Token: 0x06009838 RID: 38968 RVA: 0x003800B9 File Offset: 0x0037E2B9
		private Chore CreateUrgentUseChore(Toilet.StatesInstance smi)
		{
			Chore chore = this.CreateUseChore(smi, Db.Get().ChoreTypes.Pee);
			chore.AddPrecondition(ChorePreconditions.instance.IsBladderFull, null);
			chore.AddPrecondition(ChorePreconditions.instance.NotCurrentlyPeeing, null);
			return chore;
		}

		// Token: 0x06009839 RID: 38969 RVA: 0x003800F4 File Offset: 0x0037E2F4
		private Chore CreateBreakUseChore(Toilet.StatesInstance smi)
		{
			Chore chore = this.CreateUseChore(smi, Db.Get().ChoreTypes.BreakPee);
			chore.AddPrecondition(ChorePreconditions.instance.IsBladderNotFull, null);
			chore.AddPrecondition(ChorePreconditions.instance.IsScheduledTime, Db.Get().ScheduleBlockTypes.Hygiene);
			return chore;
		}

		// Token: 0x0600983A RID: 38970 RVA: 0x00380148 File Offset: 0x0037E348
		private Chore CreateUseChore(Toilet.StatesInstance smi, ChoreType choreType)
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

		// Token: 0x040074FD RID: 29949
		public GameStateMachine<Toilet.States, Toilet.StatesInstance, Toilet, object>.State needsdirt;

		// Token: 0x040074FE RID: 29950
		public GameStateMachine<Toilet.States, Toilet.StatesInstance, Toilet, object>.State empty;

		// Token: 0x040074FF RID: 29951
		public GameStateMachine<Toilet.States, Toilet.StatesInstance, Toilet, object>.State notoperational;

		// Token: 0x04007500 RID: 29952
		public GameStateMachine<Toilet.States, Toilet.StatesInstance, Toilet, object>.State ready;

		// Token: 0x04007501 RID: 29953
		public GameStateMachine<Toilet.States, Toilet.StatesInstance, Toilet, object>.State earlyclean;

		// Token: 0x04007502 RID: 29954
		public GameStateMachine<Toilet.States, Toilet.StatesInstance, Toilet, object>.State earlyWaitingForClean;

		// Token: 0x04007503 RID: 29955
		public GameStateMachine<Toilet.States, Toilet.StatesInstance, Toilet, object>.State full;

		// Token: 0x04007504 RID: 29956
		public GameStateMachine<Toilet.States, Toilet.StatesInstance, Toilet, object>.State fullWaitingForClean;

		// Token: 0x04007505 RID: 29957
		public GameStateMachine<Toilet.States, Toilet.StatesInstance, Toilet, object>.State exit_full;

		// Token: 0x04007506 RID: 29958
		public StateMachine<Toilet.States, Toilet.StatesInstance, Toilet, object>.BoolParameter cloggedWithGunk;

		// Token: 0x04007507 RID: 29959
		public StateMachine<Toilet.States, Toilet.StatesInstance, Toilet, object>.IntParameter flushes = new StateMachine<Toilet.States, Toilet.StatesInstance, Toilet, object>.IntParameter(0);

		// Token: 0x020027F0 RID: 10224
		public class ReadyStates : GameStateMachine<Toilet.States, Toilet.StatesInstance, Toilet, object>.State
		{
			// Token: 0x0400B128 RID: 45352
			public GameStateMachine<Toilet.States, Toilet.StatesInstance, Toilet, object>.State idle;

			// Token: 0x0400B129 RID: 45353
			public GameStateMachine<Toilet.States, Toilet.StatesInstance, Toilet, object>.State inuse;

			// Token: 0x0400B12A RID: 45354
			public GameStateMachine<Toilet.States, Toilet.StatesInstance, Toilet, object>.State flush;
		}
	}
}
