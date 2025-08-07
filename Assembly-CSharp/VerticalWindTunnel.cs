using System;
using System.Collections.Generic;
using Klei.AI;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000BD6 RID: 3030
[SerializationConfig(MemberSerialization.OptIn)]
public class VerticalWindTunnel : StateMachineComponent<VerticalWindTunnel.StatesInstance>, IGameObjectEffectDescriptor, ISim200ms
{
	// Token: 0x06005AD3 RID: 23251 RVA: 0x0020CF64 File Offset: 0x0020B164
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		ElementConsumer[] components = base.GetComponents<ElementConsumer>();
		this.bottomConsumer = components[0];
		this.bottomConsumer.EnableConsumption(false);
		this.bottomConsumer.OnElementConsumed += delegate(Sim.ConsumedMassInfo info)
		{
			this.OnElementConsumed(false, info);
		};
		this.topConsumer = components[1];
		this.topConsumer.EnableConsumption(false);
		this.topConsumer.OnElementConsumed += delegate(Sim.ConsumedMassInfo info)
		{
			this.OnElementConsumed(true, info);
		};
		this.operational = base.GetComponent<Operational>();
	}

	// Token: 0x06005AD4 RID: 23252 RVA: 0x0020CFE4 File Offset: 0x0020B1E4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.invalidIntake = this.HasInvalidIntake();
		base.GetComponent<KSelectable>().ToggleStatusItem(Db.Get().BuildingStatusItems.WindTunnelIntake, this.invalidIntake, this);
		this.operational.SetFlag(VerticalWindTunnel.validIntakeFlag, !this.invalidIntake);
		GameScheduler.Instance.Schedule("Scheduling Tutorial", 2f, delegate(object obj)
		{
			Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_Schedule, true);
		}, null, null);
		this.workables = new VerticalWindTunnelWorkable[this.choreOffsets.Length];
		this.chores = new Chore[this.choreOffsets.Length];
		for (int i = 0; i < this.workables.Length; i++)
		{
			Vector3 vector = Grid.CellToPosCBC(Grid.OffsetCell(Grid.PosToCell(this), this.choreOffsets[i]), Grid.SceneLayer.Move);
			GameObject gameObject = ChoreHelpers.CreateLocator("VerticalWindTunnelWorkable", vector);
			KSelectable kselectable = gameObject.AddOrGet<KSelectable>();
			kselectable.SetName(this.GetProperName());
			kselectable.IsSelectable = false;
			VerticalWindTunnelWorkable verticalWindTunnelWorkable = gameObject.AddOrGet<VerticalWindTunnelWorkable>();
			int player_index = i;
			VerticalWindTunnelWorkable verticalWindTunnelWorkable2 = verticalWindTunnelWorkable;
			verticalWindTunnelWorkable2.OnWorkableEventCB = (Action<Workable, Workable.WorkableEvent>)Delegate.Combine(verticalWindTunnelWorkable2.OnWorkableEventCB, new Action<Workable, Workable.WorkableEvent>(delegate(Workable workable, Workable.WorkableEvent ev)
			{
				this.OnWorkableEvent(player_index, ev);
			}));
			verticalWindTunnelWorkable.overrideAnim = this.overrideAnims[i];
			verticalWindTunnelWorkable.preAnims = this.workPreAnims[i];
			verticalWindTunnelWorkable.loopAnim = this.workAnims[i];
			verticalWindTunnelWorkable.pstAnims = this.workPstAnims[i];
			this.workables[i] = verticalWindTunnelWorkable;
			this.workables[i].windTunnel = this;
		}
		base.smi.StartSM();
	}

	// Token: 0x06005AD5 RID: 23253 RVA: 0x0020D190 File Offset: 0x0020B390
	protected override void OnCleanUp()
	{
		this.UpdateChores(false);
		for (int i = 0; i < this.workables.Length; i++)
		{
			if (this.workables[i])
			{
				Util.KDestroyGameObject(this.workables[i]);
				this.workables[i] = null;
			}
		}
		base.OnCleanUp();
	}

	// Token: 0x06005AD6 RID: 23254 RVA: 0x0020D1E4 File Offset: 0x0020B3E4
	private Chore CreateChore(int i)
	{
		Workable workable = this.workables[i];
		ChoreType relax = Db.Get().ChoreTypes.Relax;
		IStateMachineTarget stateMachineTarget = workable;
		ChoreProvider choreProvider = null;
		bool flag = true;
		Action<Chore> action = null;
		Action<Chore> action2 = null;
		ScheduleBlockType recreation = Db.Get().ScheduleBlockTypes.Recreation;
		WorkChore<VerticalWindTunnelWorkable> workChore = new WorkChore<VerticalWindTunnelWorkable>(relax, stateMachineTarget, choreProvider, flag, action, action2, new Action<Chore>(this.OnSocialChoreEnd), false, recreation, false, true, null, false, true, false, PriorityScreen.PriorityClass.high, 5, false, true);
		workChore.AddPrecondition(ChorePreconditions.instance.CanDoWorkerPrioritizable, workable);
		return workChore;
	}

	// Token: 0x06005AD7 RID: 23255 RVA: 0x0020D24C File Offset: 0x0020B44C
	private void OnSocialChoreEnd(Chore chore)
	{
		if (base.gameObject.HasTag(GameTags.Operational))
		{
			this.UpdateChores(true);
		}
	}

	// Token: 0x06005AD8 RID: 23256 RVA: 0x0020D268 File Offset: 0x0020B468
	public void UpdateChores(bool update = true)
	{
		for (int i = 0; i < this.choreOffsets.Length; i++)
		{
			Chore chore = this.chores[i];
			if (update)
			{
				if (chore == null || chore.isComplete)
				{
					this.chores[i] = this.CreateChore(i);
				}
			}
			else if (chore != null)
			{
				chore.Cancel("locator invalidated");
				this.chores[i] = null;
			}
		}
	}

	// Token: 0x06005AD9 RID: 23257 RVA: 0x0020D2C8 File Offset: 0x0020B4C8
	public void Sim200ms(float dt)
	{
		bool flag = this.HasInvalidIntake();
		if (flag != this.invalidIntake)
		{
			this.invalidIntake = flag;
			base.GetComponent<KSelectable>().ToggleStatusItem(Db.Get().BuildingStatusItems.WindTunnelIntake, this.invalidIntake, this);
			this.operational.SetFlag(VerticalWindTunnel.validIntakeFlag, !this.invalidIntake);
		}
	}

	// Token: 0x06005ADA RID: 23258 RVA: 0x0020D328 File Offset: 0x0020B528
	private float GetIntakeRatio(int fromCell, int radius)
	{
		float num = 0f;
		float num2 = 0f;
		for (int i = -radius; i < radius; i++)
		{
			for (int j = -radius; j < radius; j++)
			{
				int num3 = Grid.OffsetCell(fromCell, j, i);
				if (!Grid.IsSolidCell(num3))
				{
					if (Grid.IsGas(num3))
					{
						num2 += 1f;
					}
					num += 1f;
				}
			}
		}
		return num2 / num;
	}

	// Token: 0x06005ADB RID: 23259 RVA: 0x0020D38C File Offset: 0x0020B58C
	private bool HasInvalidIntake()
	{
		Vector3 position = base.transform.GetPosition();
		int num = Grid.XYToCell((int)position.x, (int)position.y);
		int num2 = Grid.OffsetCell(num, (int)this.topConsumer.sampleCellOffset.x, (int)this.topConsumer.sampleCellOffset.y);
		int num3 = Grid.OffsetCell(num, (int)this.bottomConsumer.sampleCellOffset.x, (int)this.bottomConsumer.sampleCellOffset.y);
		this.avgGasAccumTop += this.GetIntakeRatio(num2, (int)this.topConsumer.consumptionRadius);
		this.avgGasAccumBottom += this.GetIntakeRatio(num3, (int)this.bottomConsumer.consumptionRadius);
		int num4 = 5;
		this.avgGasCounter = (this.avgGasCounter + 1) % num4;
		if (this.avgGasCounter == 0)
		{
			double num5 = (double)(this.avgGasAccumTop / (float)num4);
			float num6 = this.avgGasAccumBottom / (float)num4;
			this.avgGasAccumBottom = 0f;
			this.avgGasAccumTop = 0f;
			return num5 < 0.5 || (double)num6 < 0.5;
		}
		return this.invalidIntake;
	}

	// Token: 0x06005ADC RID: 23260 RVA: 0x0020D4B0 File Offset: 0x0020B6B0
	public void SetGasWalls(bool set)
	{
		Building component = base.GetComponent<Building>();
		Sim.Cell.Properties properties = (Sim.Cell.Properties)3;
		Vector3 position = base.transform.GetPosition();
		for (int i = 0; i < component.Def.HeightInCells; i++)
		{
			int num = Grid.XYToCell(Mathf.FloorToInt(position.x) - 2, Mathf.FloorToInt(position.y) + i);
			int num2 = Grid.XYToCell(Mathf.FloorToInt(position.x) + 2, Mathf.FloorToInt(position.y) + i);
			if (set)
			{
				SimMessages.SetCellProperties(num, (byte)properties);
				SimMessages.SetCellProperties(num2, (byte)properties);
			}
			else
			{
				SimMessages.ClearCellProperties(num, (byte)properties);
				SimMessages.ClearCellProperties(num2, (byte)properties);
			}
		}
	}

	// Token: 0x06005ADD RID: 23261 RVA: 0x0020D554 File Offset: 0x0020B754
	private void OnElementConsumed(bool isTop, Sim.ConsumedMassInfo info)
	{
		Building component = base.GetComponent<Building>();
		Vector3 position = base.transform.GetPosition();
		CellOffset cellOffset = (isTop ? new CellOffset(0, component.Def.HeightInCells + 1) : new CellOffset(0, 0));
		SimMessages.AddRemoveSubstance(Grid.OffsetCell(Grid.XYToCell((int)position.x, (int)position.y), cellOffset), info.removedElemIdx, CellEventLogger.Instance.ElementEmitted, info.mass, info.temperature, info.diseaseIdx, info.diseaseCount, true, -1);
	}

	// Token: 0x06005ADE RID: 23262 RVA: 0x0020D5DC File Offset: 0x0020B7DC
	public void OnWorkableEvent(int player, Workable.WorkableEvent ev)
	{
		if (ev == Workable.WorkableEvent.WorkStarted)
		{
			this.players.Add(player);
		}
		else
		{
			this.players.Remove(player);
		}
		base.smi.sm.playerCount.Set(this.players.Count, base.smi, false);
	}

	// Token: 0x06005ADF RID: 23263 RVA: 0x0020D634 File Offset: 0x0020B834
	List<Descriptor> IGameObjectEffectDescriptor.GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		list.Add(new Descriptor(BUILDINGS.PREFABS.VERTICALWINDTUNNEL.DISPLACEMENTEFFECT.Replace("{amount}", GameUtil.GetFormattedMass(this.displacementAmount_DescriptorOnly, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")), BUILDINGS.PREFABS.VERTICALWINDTUNNEL.DISPLACEMENTEFFECT_TOOLTIP.Replace("{amount}", GameUtil.GetFormattedMass(this.displacementAmount_DescriptorOnly, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")), Descriptor.DescriptorType.Requirement, false));
		list.Add(new Descriptor(UI.BUILDINGEFFECTS.RECREATION, UI.BUILDINGEFFECTS.TOOLTIPS.RECREATION, Descriptor.DescriptorType.Effect, false));
		Effect.AddModifierDescriptions(base.gameObject, list, this.specificEffect, true);
		return list;
	}

	// Token: 0x04003C33 RID: 15411
	public string specificEffect;

	// Token: 0x04003C34 RID: 15412
	public string trackingEffect;

	// Token: 0x04003C35 RID: 15413
	public int basePriority;

	// Token: 0x04003C36 RID: 15414
	public float displacementAmount_DescriptorOnly;

	// Token: 0x04003C37 RID: 15415
	public static readonly Operational.Flag validIntakeFlag = new Operational.Flag("valid_intake", Operational.Flag.Type.Requirement);

	// Token: 0x04003C38 RID: 15416
	private bool invalidIntake;

	// Token: 0x04003C39 RID: 15417
	private float avgGasAccumTop;

	// Token: 0x04003C3A RID: 15418
	private float avgGasAccumBottom;

	// Token: 0x04003C3B RID: 15419
	private int avgGasCounter;

	// Token: 0x04003C3C RID: 15420
	public CellOffset[] choreOffsets = new CellOffset[]
	{
		new CellOffset(0, 0),
		new CellOffset(-1, 0),
		new CellOffset(1, 0)
	};

	// Token: 0x04003C3D RID: 15421
	private VerticalWindTunnelWorkable[] workables;

	// Token: 0x04003C3E RID: 15422
	private Chore[] chores;

	// Token: 0x04003C3F RID: 15423
	private ElementConsumer bottomConsumer;

	// Token: 0x04003C40 RID: 15424
	private ElementConsumer topConsumer;

	// Token: 0x04003C41 RID: 15425
	private Operational operational;

	// Token: 0x04003C42 RID: 15426
	public HashSet<int> players = new HashSet<int>();

	// Token: 0x04003C43 RID: 15427
	public HashedString[] overrideAnims = new HashedString[] { "anim_interacts_windtunnel_center_kanim", "anim_interacts_windtunnel_left_kanim", "anim_interacts_windtunnel_right_kanim" };

	// Token: 0x04003C44 RID: 15428
	public string[][] workPreAnims = new string[][]
	{
		new string[] { "weak_working_front_pre", "weak_working_back_pre" },
		new string[] { "medium_working_front_pre", "medium_working_back_pre" },
		new string[] { "strong_working_front_pre", "strong_working_back_pre" }
	};

	// Token: 0x04003C45 RID: 15429
	public string[] workAnims = new string[] { "weak_working_loop", "medium_working_loop", "strong_working_loop" };

	// Token: 0x04003C46 RID: 15430
	public string[][] workPstAnims = new string[][]
	{
		new string[] { "weak_working_back_pst", "weak_working_front_pst" },
		new string[] { "medium_working_back_pst", "medium_working_front_pst" },
		new string[] { "strong_working_back_pst", "strong_working_front_pst" }
	};

	// Token: 0x02001D04 RID: 7428
	public class States : GameStateMachine<VerticalWindTunnel.States, VerticalWindTunnel.StatesInstance, VerticalWindTunnel>
	{
		// Token: 0x0600ACD2 RID: 44242 RVA: 0x003C2BDC File Offset: 0x003C0DDC
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.unoperational;
			this.unoperational.Enter(delegate(VerticalWindTunnel.StatesInstance smi)
			{
				smi.SetActive(false);
			}).TagTransition(GameTags.Operational, this.operational, false).PlayAnim("off");
			this.operational.TagTransition(GameTags.Operational, this.unoperational, true).Enter("CreateChore", delegate(VerticalWindTunnel.StatesInstance smi)
			{
				smi.master.UpdateChores(true);
			}).Exit("CancelChore", delegate(VerticalWindTunnel.StatesInstance smi)
			{
				smi.master.UpdateChores(false);
			})
				.DefaultState(this.operational.stopped);
			this.operational.stopped.PlayAnim("off").ParamTransition<int>(this.playerCount, this.operational.pre, (VerticalWindTunnel.StatesInstance smi, int p) => p > 0);
			this.operational.pre.PlayAnim("working_pre").OnAnimQueueComplete(this.operational.playing);
			this.operational.playing.PlayAnim("working_loop", KAnim.PlayMode.Loop).Enter(delegate(VerticalWindTunnel.StatesInstance smi)
			{
				smi.SetActive(true);
			}).Exit(delegate(VerticalWindTunnel.StatesInstance smi)
			{
				smi.SetActive(false);
			})
				.ParamTransition<int>(this.playerCount, this.operational.post, (VerticalWindTunnel.StatesInstance smi, int p) => p == 0)
				.Enter("GasWalls", delegate(VerticalWindTunnel.StatesInstance smi)
				{
					smi.master.SetGasWalls(true);
				})
				.Exit("GasWalls", delegate(VerticalWindTunnel.StatesInstance smi)
				{
					smi.master.SetGasWalls(false);
				});
			this.operational.post.PlayAnim("working_pst").QueueAnim("off_pre", false, null).OnAnimQueueComplete(this.operational.stopped);
		}

		// Token: 0x040087F0 RID: 34800
		public StateMachine<VerticalWindTunnel.States, VerticalWindTunnel.StatesInstance, VerticalWindTunnel, object>.IntParameter playerCount;

		// Token: 0x040087F1 RID: 34801
		public GameStateMachine<VerticalWindTunnel.States, VerticalWindTunnel.StatesInstance, VerticalWindTunnel, object>.State unoperational;

		// Token: 0x040087F2 RID: 34802
		public VerticalWindTunnel.States.OperationalStates operational;

		// Token: 0x020028D3 RID: 10451
		public class OperationalStates : GameStateMachine<VerticalWindTunnel.States, VerticalWindTunnel.StatesInstance, VerticalWindTunnel, object>.State
		{
			// Token: 0x0400B4EE RID: 46318
			public GameStateMachine<VerticalWindTunnel.States, VerticalWindTunnel.StatesInstance, VerticalWindTunnel, object>.State stopped;

			// Token: 0x0400B4EF RID: 46319
			public GameStateMachine<VerticalWindTunnel.States, VerticalWindTunnel.StatesInstance, VerticalWindTunnel, object>.State pre;

			// Token: 0x0400B4F0 RID: 46320
			public GameStateMachine<VerticalWindTunnel.States, VerticalWindTunnel.StatesInstance, VerticalWindTunnel, object>.State playing;

			// Token: 0x0400B4F1 RID: 46321
			public GameStateMachine<VerticalWindTunnel.States, VerticalWindTunnel.StatesInstance, VerticalWindTunnel, object>.State post;
		}
	}

	// Token: 0x02001D05 RID: 7429
	public class StatesInstance : GameStateMachine<VerticalWindTunnel.States, VerticalWindTunnel.StatesInstance, VerticalWindTunnel, object>.GameInstance
	{
		// Token: 0x0600ACD4 RID: 44244 RVA: 0x003C2E42 File Offset: 0x003C1042
		public StatesInstance(VerticalWindTunnel smi)
			: base(smi)
		{
			this.operational = base.master.GetComponent<Operational>();
		}

		// Token: 0x0600ACD5 RID: 44245 RVA: 0x003C2E5C File Offset: 0x003C105C
		public void SetActive(bool active)
		{
			this.operational.SetActive(this.operational.IsOperational && active, false);
		}

		// Token: 0x040087F3 RID: 34803
		private Operational operational;
	}
}
