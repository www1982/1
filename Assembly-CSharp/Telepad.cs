using System;
using System.Collections;
using System.Collections.Generic;
using Klei.AI;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020007DB RID: 2011
public class Telepad : StateMachineComponent<Telepad.StatesInstance>
{
	// Token: 0x0600364E RID: 13902 RVA: 0x0012E5B4 File Offset: 0x0012C7B4
	public void AddNewBaseMinion(GameObject minion, bool extra_power_banks)
	{
		Ref<MinionIdentity> @ref = new Ref<MinionIdentity>(minion.GetComponent<MinionIdentity>());
		this.aNewHopeEvents.Add(@ref);
		if (extra_power_banks)
		{
			this.extraPowerBanksEvents.Add(@ref);
		}
	}

	// Token: 0x0600364F RID: 13903 RVA: 0x0012E5E8 File Offset: 0x0012C7E8
	public void ScheduleNewBaseEvents()
	{
		this.aNewHopeEvents.RemoveAll((Ref<MinionIdentity> entry) => entry == null || entry.Get() == null);
		this.extraPowerBanksEvents.RemoveAll((Ref<MinionIdentity> entry) => entry == null || entry.Get() == null);
		Effect a_new_hope = Db.Get().effects.Get("AnewHope");
		Action<object> <>9__2;
		for (int i = 0; i < this.aNewHopeEvents.Count; i++)
		{
			GameObject gameObject = this.aNewHopeEvents[i].Get().gameObject;
			GameScheduler instance = GameScheduler.Instance;
			string text = "ANewHope";
			float num = 3f + 0.5f * (float)i;
			Action<object> action;
			if ((action = <>9__2) == null)
			{
				action = (<>9__2 = delegate(object m)
				{
					GameObject gameObject3 = m as GameObject;
					if (gameObject3 == null)
					{
						return;
					}
					this.RemoveFromEvents(this.aNewHopeEvents, gameObject3);
					gameObject3.GetComponent<Effects>().Add(a_new_hope, true);
				});
			}
			instance.Schedule(text, num, action, gameObject, null);
		}
		Action<object> <>9__3;
		for (int j = 0; j < this.extraPowerBanksEvents.Count; j++)
		{
			GameObject gameObject2 = this.extraPowerBanksEvents[j].Get().gameObject;
			GameScheduler instance2 = GameScheduler.Instance;
			string text2 = "ExtraPowerBanks";
			float num2 = 3f + 4.5f * (float)j;
			Action<object> action2;
			if ((action2 = <>9__3) == null)
			{
				action2 = (<>9__3 = delegate(object m)
				{
					GameObject gameObject4 = m as GameObject;
					if (gameObject4 == null)
					{
						return;
					}
					this.RemoveFromEvents(this.extraPowerBanksEvents, gameObject4);
					GameUtil.GetTelepad(ClusterManager.Instance.GetStartWorld().id).Trigger(1982288670, null);
				});
			}
			instance2.Schedule(text2, num2, action2, gameObject2, null);
		}
	}

	// Token: 0x06003650 RID: 13904 RVA: 0x0012E754 File Offset: 0x0012C954
	private void RemoveFromEvents(List<Ref<MinionIdentity>> listToRemove, GameObject go)
	{
		for (int i = listToRemove.Count - 1; i >= 0; i--)
		{
			if (listToRemove[i].Get() != null && listToRemove[i].Get() == go.GetComponent<MinionIdentity>())
			{
				listToRemove.RemoveAt(i);
				return;
			}
		}
	}

	// Token: 0x06003651 RID: 13905 RVA: 0x0012E7AC File Offset: 0x0012C9AC
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.GetComponent<Deconstructable>().allowDeconstruction = false;
		int num = 0;
		int num2 = 0;
		Grid.CellToXY(Grid.PosToCell(this), out num, out num2);
		if (num == 0)
		{
			global::Debug.LogError(string.Concat(new string[]
			{
				"Headquarters spawned at: (",
				num.ToString(),
				",",
				num2.ToString(),
				")"
			}));
		}
	}

	// Token: 0x06003652 RID: 13906 RVA: 0x0012E820 File Offset: 0x0012CA20
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Components.Telepads.Add(this);
		this.meter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.Behind, Grid.SceneLayer.NoLayer, new string[] { "meter_target", "meter_fill", "meter_frame", "meter_OL" });
		this.meter.gameObject.GetComponent<KBatchedAnimController>().SetDirty();
		base.smi.StartSM();
		this.ScheduleNewBaseEvents();
	}

	// Token: 0x06003653 RID: 13907 RVA: 0x0012E8A8 File Offset: 0x0012CAA8
	protected override void OnCleanUp()
	{
		Components.Telepads.Remove(this);
		base.OnCleanUp();
	}

	// Token: 0x06003654 RID: 13908 RVA: 0x0012E8BC File Offset: 0x0012CABC
	public void Update()
	{
		if (base.smi.IsColonyLost())
		{
			return;
		}
		if (Immigration.Instance.ImmigrantsAvailable && base.GetComponent<Operational>().IsOperational)
		{
			base.smi.sm.openPortal.Trigger(base.smi);
			this.selectable.SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.NewDuplicantsAvailable, this);
		}
		else
		{
			base.smi.sm.closePortal.Trigger(base.smi);
			this.selectable.SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.Wattson, this);
		}
		if (this.GetTimeRemaining() < -120f)
		{
			Messenger.Instance.QueueMessage(new DuplicantsLeftMessage());
			Immigration.Instance.EndImmigration();
		}
	}

	// Token: 0x06003655 RID: 13909 RVA: 0x0012E9A5 File Offset: 0x0012CBA5
	public void RejectAll()
	{
		Immigration.Instance.EndImmigration();
		base.smi.sm.closePortal.Trigger(base.smi);
	}

	// Token: 0x06003656 RID: 13910 RVA: 0x0012E9D0 File Offset: 0x0012CBD0
	public void OnAcceptDelivery(ITelepadDeliverable delivery)
	{
		int num = Grid.PosToCell(this);
		Immigration.Instance.EndImmigration();
		GameObject gameObject = delivery.Deliver(Grid.CellToPosCBC(num, Grid.SceneLayer.Move));
		MinionIdentity component = gameObject.GetComponent<MinionIdentity>();
		if (component != null)
		{
			ReportManager.Instance.ReportValue(ReportManager.ReportType.PersonalTime, GameClock.Instance.GetTimeSinceStartOfReport(), string.Format(UI.ENDOFDAYREPORT.NOTES.PERSONAL_TIME, DUPLICANTS.CHORES.NOT_EXISTING_TASK), gameObject.GetProperName());
			foreach (MinionIdentity minionIdentity in Components.LiveMinionIdentities.GetWorldItems(base.gameObject.GetComponent<KSelectable>().GetMyWorldId(), false))
			{
				minionIdentity.GetComponent<Effects>().Add("NewCrewArrival", true);
			}
			MinionResume component2 = component.GetComponent<MinionResume>();
			int num2 = 0;
			while ((float)num2 < this.startingSkillPoints)
			{
				component2.ForceAddSkillPoint();
				num2++;
			}
			if (component.HasTag(GameTags.Minions.Models.Bionic))
			{
				GameScheduler.Instance.Schedule("BonusBatteryDelivery", 5f, delegate(object data)
				{
					base.Trigger(1982288670, null);
				}, null, null);
			}
		}
		base.smi.sm.closePortal.Trigger(base.smi);
	}

	// Token: 0x06003657 RID: 13911 RVA: 0x0012EB14 File Offset: 0x0012CD14
	public float GetTimeRemaining()
	{
		return Immigration.Instance.GetTimeRemaining();
	}

	// Token: 0x040020CB RID: 8395
	[MyCmpReq]
	private KSelectable selectable;

	// Token: 0x040020CC RID: 8396
	private MeterController meter;

	// Token: 0x040020CD RID: 8397
	private const float MAX_IMMIGRATION_TIME = 120f;

	// Token: 0x040020CE RID: 8398
	private const int NUM_METER_NOTCHES = 8;

	// Token: 0x040020CF RID: 8399
	private List<MinionStartingStats> minionStats;

	// Token: 0x040020D0 RID: 8400
	public float startingSkillPoints;

	// Token: 0x040020D1 RID: 8401
	[Serialize]
	private List<Ref<MinionIdentity>> aNewHopeEvents = new List<Ref<MinionIdentity>>();

	// Token: 0x040020D2 RID: 8402
	[Serialize]
	private List<Ref<MinionIdentity>> extraPowerBanksEvents = new List<Ref<MinionIdentity>>();

	// Token: 0x040020D3 RID: 8403
	public static readonly HashedString[] PortalBirthAnim = new HashedString[] { "portalbirth" };

	// Token: 0x02001725 RID: 5925
	public class StatesInstance : GameStateMachine<Telepad.States, Telepad.StatesInstance, Telepad, object>.GameInstance
	{
		// Token: 0x060097DF RID: 38879 RVA: 0x0037EBB5 File Offset: 0x0037CDB5
		public StatesInstance(Telepad master)
			: base(master)
		{
		}

		// Token: 0x060097E0 RID: 38880 RVA: 0x0037EBBE File Offset: 0x0037CDBE
		public bool IsColonyLost()
		{
			return GameFlowManager.Instance != null && GameFlowManager.Instance.IsGameOver();
		}

		// Token: 0x060097E1 RID: 38881 RVA: 0x0037EBDC File Offset: 0x0037CDDC
		public void UpdateMeter()
		{
			float timeRemaining = Immigration.Instance.GetTimeRemaining();
			float totalWaitTime = Immigration.Instance.GetTotalWaitTime();
			float num = Mathf.Clamp01(1f - timeRemaining / totalWaitTime);
			base.master.meter.SetPositionPercent(num);
		}

		// Token: 0x060097E2 RID: 38882 RVA: 0x0037EC1F File Offset: 0x0037CE1F
		public IEnumerator SpawnExtraPowerBanks()
		{
			int cellTarget = Grid.OffsetCell(Grid.PosToCell(base.gameObject), 1, 2);
			int count = 5;
			int num;
			for (int i = 0; i < count; i = num + 1)
			{
				PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Plus, MISC.POPFX.EXTRA_POWERBANKS_BIONIC, base.gameObject.transform, new Vector3(0f, 0.5f, 0f), 1.5f, false, false);
				KMonoBehaviour.PlaySound(GlobalAssets.GetSound("SandboxTool_Spawner", false));
				GameObject gameObject = Util.KInstantiate(Assets.GetPrefab("DisposableElectrobank_RawMetal"), Grid.CellToPosCBC(cellTarget, Grid.SceneLayer.Front) - Vector3.right / 2f);
				gameObject.SetActive(true);
				Vector2 vector = new Vector2((-2.5f + 5f * ((float)i / 5f)) / 2f, 2f);
				if (GameComps.Fallers.Has(gameObject))
				{
					GameComps.Fallers.Remove(gameObject);
				}
				GameComps.Fallers.Add(gameObject, vector);
				yield return new WaitForSeconds(0.25f);
				num = i;
			}
			yield return new WaitForSeconds(0.35f);
			PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Plus, ITEMS.LUBRICATIONSTICK.NAME, base.gameObject.transform, new Vector3(0f, 0.5f, 0f), 1.5f, false, false);
			KMonoBehaviour.PlaySound(GlobalAssets.GetSound("SandboxTool_Spawner", false));
			GameObject gameObject2 = Util.KInstantiate(Assets.GetPrefab("LubricationStick"), Grid.CellToPosCBC(cellTarget, Grid.SceneLayer.Front) - Vector3.right / 2f);
			gameObject2.SetActive(true);
			Vector2 vector2 = new Vector2(3.75f, 2.5f);
			if (GameComps.Fallers.Has(gameObject2))
			{
				GameComps.Fallers.Remove(gameObject2);
			}
			GameComps.Fallers.Add(gameObject2, vector2);
			yield return 0;
			yield break;
		}
	}

	// Token: 0x02001726 RID: 5926
	public class States : GameStateMachine<Telepad.States, Telepad.StatesInstance, Telepad>
	{
		// Token: 0x060097E3 RID: 38883 RVA: 0x0037EC30 File Offset: 0x0037CE30
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.idle;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.root.OnSignal(this.idlePortal, this.resetToIdle).EventTransition(GameHashes.BonusTelepadDelivery, this.bonusDelivery.pre, null);
			this.resetToIdle.GoTo(this.idle);
			this.idle.Enter(delegate(Telepad.StatesInstance smi)
			{
				smi.UpdateMeter();
			}).Update("TelepadMeter", delegate(Telepad.StatesInstance smi, float dt)
			{
				smi.UpdateMeter();
			}, UpdateRate.SIM_4000ms, false).EventTransition(GameHashes.OperationalChanged, this.unoperational, (Telepad.StatesInstance smi) => !smi.GetComponent<Operational>().IsOperational)
				.PlayAnim("idle")
				.OnSignal(this.openPortal, this.opening);
			this.unoperational.PlayAnim("idle").Enter("StopImmigration", delegate(Telepad.StatesInstance smi)
			{
				smi.master.meter.SetPositionPercent(0f);
			}).EventTransition(GameHashes.OperationalChanged, this.idle, (Telepad.StatesInstance smi) => smi.GetComponent<Operational>().IsOperational);
			this.opening.Enter(delegate(Telepad.StatesInstance smi)
			{
				smi.master.meter.SetPositionPercent(1f);
			}).PlayAnim("working_pre").OnAnimQueueComplete(this.open);
			this.open.OnSignal(this.closePortal, this.close).Enter(delegate(Telepad.StatesInstance smi)
			{
				smi.master.meter.SetPositionPercent(1f);
			}).PlayAnim("working_loop", KAnim.PlayMode.Loop)
				.Transition(this.close, (Telepad.StatesInstance smi) => smi.IsColonyLost(), UpdateRate.SIM_200ms)
				.EventTransition(GameHashes.OperationalChanged, this.close, (Telepad.StatesInstance smi) => !smi.GetComponent<Operational>().IsOperational);
			this.close.Enter(delegate(Telepad.StatesInstance smi)
			{
				smi.master.meter.SetPositionPercent(0f);
			}).PlayAnims((Telepad.StatesInstance smi) => Telepad.States.workingAnims, KAnim.PlayMode.Once).OnAnimQueueComplete(this.idle);
			this.bonusDelivery.pre.PlayAnim("bionic_working_pre").OnAnimQueueComplete(this.bonusDelivery.loop);
			this.bonusDelivery.loop.PlayAnim("bionic_working_loop", KAnim.PlayMode.Loop).ScheduleAction("SpawnBonusDelivery", 1f, delegate(Telepad.StatesInstance smi)
			{
				smi.master.StartCoroutine(smi.SpawnExtraPowerBanks());
			}).ScheduleGoTo(3f, this.bonusDelivery.pst);
			this.bonusDelivery.pst.PlayAnim("bionic_working_pst").OnAnimQueueComplete(this.idle);
		}

		// Token: 0x040074C2 RID: 29890
		public StateMachine<Telepad.States, Telepad.StatesInstance, Telepad, object>.Signal openPortal;

		// Token: 0x040074C3 RID: 29891
		public StateMachine<Telepad.States, Telepad.StatesInstance, Telepad, object>.Signal closePortal;

		// Token: 0x040074C4 RID: 29892
		public StateMachine<Telepad.States, Telepad.StatesInstance, Telepad, object>.Signal idlePortal;

		// Token: 0x040074C5 RID: 29893
		public GameStateMachine<Telepad.States, Telepad.StatesInstance, Telepad, object>.State idle;

		// Token: 0x040074C6 RID: 29894
		public GameStateMachine<Telepad.States, Telepad.StatesInstance, Telepad, object>.State resetToIdle;

		// Token: 0x040074C7 RID: 29895
		public GameStateMachine<Telepad.States, Telepad.StatesInstance, Telepad, object>.State opening;

		// Token: 0x040074C8 RID: 29896
		public GameStateMachine<Telepad.States, Telepad.StatesInstance, Telepad, object>.State open;

		// Token: 0x040074C9 RID: 29897
		public GameStateMachine<Telepad.States, Telepad.StatesInstance, Telepad, object>.State close;

		// Token: 0x040074CA RID: 29898
		public GameStateMachine<Telepad.States, Telepad.StatesInstance, Telepad, object>.State unoperational;

		// Token: 0x040074CB RID: 29899
		public Telepad.States.BonusDeliveryStates bonusDelivery;

		// Token: 0x040074CC RID: 29900
		private static readonly HashedString[] workingAnims = new HashedString[] { "working_loop", "working_pst" };

		// Token: 0x020027EB RID: 10219
		public class BonusDeliveryStates : GameStateMachine<Telepad.States, Telepad.StatesInstance, Telepad, object>.State
		{
			// Token: 0x0400B10D RID: 45325
			public GameStateMachine<Telepad.States, Telepad.StatesInstance, Telepad, object>.State pre;

			// Token: 0x0400B10E RID: 45326
			public GameStateMachine<Telepad.States, Telepad.StatesInstance, Telepad, object>.State loop;

			// Token: 0x0400B10F RID: 45327
			public GameStateMachine<Telepad.States, Telepad.StatesInstance, Telepad, object>.State pst;
		}
	}
}
