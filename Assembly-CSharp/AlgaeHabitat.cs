using System;
using UnityEngine;

// Token: 0x020006D6 RID: 1750
public class AlgaeHabitat : StateMachineComponent<AlgaeHabitat.SMInstance>
{
	// Token: 0x06002B2C RID: 11052 RVA: 0x000F98B0 File Offset: 0x000F7AB0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
		GameScheduler.Instance.Schedule("WaterFetchingTutorial", 2f, delegate(object obj)
		{
			Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_FetchingWater, true);
		}, null, null);
		this.ConfigurePollutedWaterOutput();
		Tutorial.Instance.oxygenGenerators.Add(base.gameObject);
	}

	// Token: 0x06002B2D RID: 11053 RVA: 0x000F991F File Offset: 0x000F7B1F
	protected override void OnCleanUp()
	{
		Tutorial.Instance.oxygenGenerators.Remove(base.gameObject);
		base.OnCleanUp();
	}

	// Token: 0x06002B2E RID: 11054 RVA: 0x000F9940 File Offset: 0x000F7B40
	private void ConfigurePollutedWaterOutput()
	{
		Storage storage = null;
		Tag tag = ElementLoader.FindElementByHash(SimHashes.DirtyWater).tag;
		foreach (Storage storage2 in base.GetComponents<Storage>())
		{
			if (storage2.storageFilters.Contains(tag))
			{
				storage = storage2;
				break;
			}
		}
		foreach (ElementConverter elementConverter in base.GetComponents<ElementConverter>())
		{
			ElementConverter.OutputElement[] outputElements = elementConverter.outputElements;
			for (int j = 0; j < outputElements.Length; j++)
			{
				if (outputElements[j].elementHash == SimHashes.DirtyWater)
				{
					elementConverter.SetStorage(storage);
					break;
				}
			}
		}
		this.pollutedWaterStorage = storage;
	}

	// Token: 0x04001982 RID: 6530
	[MyCmpGet]
	private Operational operational;

	// Token: 0x04001983 RID: 6531
	private Storage pollutedWaterStorage;

	// Token: 0x04001984 RID: 6532
	[SerializeField]
	public float lightBonusMultiplier = 1.1f;

	// Token: 0x04001985 RID: 6533
	public CellOffset pressureSampleOffset = CellOffset.none;

	// Token: 0x0200155A RID: 5466
	public class SMInstance : GameStateMachine<AlgaeHabitat.States, AlgaeHabitat.SMInstance, AlgaeHabitat, object>.GameInstance
	{
		// Token: 0x060090EF RID: 37103 RVA: 0x00361F80 File Offset: 0x00360180
		public SMInstance(AlgaeHabitat master)
			: base(master)
		{
			this.converter = master.GetComponent<ElementConverter>();
		}

		// Token: 0x060090F0 RID: 37104 RVA: 0x00361F95 File Offset: 0x00360195
		public bool HasEnoughMass(Tag tag)
		{
			return this.converter.HasEnoughMass(tag, false);
		}

		// Token: 0x060090F1 RID: 37105 RVA: 0x00361FA4 File Offset: 0x003601A4
		public bool NeedsEmptying()
		{
			return base.smi.master.pollutedWaterStorage.RemainingCapacity() <= 0f;
		}

		// Token: 0x060090F2 RID: 37106 RVA: 0x00361FC8 File Offset: 0x003601C8
		public void CreateEmptyChore()
		{
			if (this.emptyChore != null)
			{
				this.emptyChore.Cancel("dupe");
			}
			AlgaeHabitatEmpty component = base.master.GetComponent<AlgaeHabitatEmpty>();
			this.emptyChore = new WorkChore<AlgaeHabitatEmpty>(Db.Get().ChoreTypes.EmptyStorage, component, null, true, new Action<Chore>(this.OnEmptyComplete), null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, true, true);
			this.emptyChore.AddPrecondition(ChorePreconditions.instance.IsNotARobot, null);
		}

		// Token: 0x060090F3 RID: 37107 RVA: 0x00362046 File Offset: 0x00360246
		public void CancelEmptyChore()
		{
			if (this.emptyChore != null)
			{
				this.emptyChore.Cancel("Cancelled");
				this.emptyChore = null;
			}
		}

		// Token: 0x060090F4 RID: 37108 RVA: 0x00362068 File Offset: 0x00360268
		private void OnEmptyComplete(Chore chore)
		{
			this.emptyChore = null;
			base.master.pollutedWaterStorage.DropAll(true, false, default(Vector3), true, null);
		}

		// Token: 0x04006F6D RID: 28525
		public ElementConverter converter;

		// Token: 0x04006F6E RID: 28526
		public Chore emptyChore;
	}

	// Token: 0x0200155B RID: 5467
	public class States : GameStateMachine<AlgaeHabitat.States, AlgaeHabitat.SMInstance, AlgaeHabitat>
	{
		// Token: 0x060090F5 RID: 37109 RVA: 0x0036209C File Offset: 0x0036029C
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.noAlgae;
			this.root.EventTransition(GameHashes.OperationalChanged, this.notoperational, (AlgaeHabitat.SMInstance smi) => !smi.master.operational.IsOperational).EventTransition(GameHashes.OperationalChanged, this.noAlgae, (AlgaeHabitat.SMInstance smi) => smi.master.operational.IsOperational);
			this.notoperational.QueueAnim("off", false, null);
			this.gotAlgae.PlayAnim("on_pre").OnAnimQueueComplete(this.noWater);
			this.gotEmptied.PlayAnim("on_pre").OnAnimQueueComplete(this.generatingOxygen);
			this.lostAlgae.PlayAnim("on_pst").OnAnimQueueComplete(this.noAlgae);
			this.noAlgae.QueueAnim("off", false, null).EventTransition(GameHashes.OnStorageChange, this.gotAlgae, (AlgaeHabitat.SMInstance smi) => smi.HasEnoughMass(GameTags.Algae)).Enter(delegate(AlgaeHabitat.SMInstance smi)
			{
				smi.master.operational.SetActive(false, false);
			});
			this.noWater.QueueAnim("on", false, null).Enter(delegate(AlgaeHabitat.SMInstance smi)
			{
				smi.master.GetComponent<PassiveElementConsumer>().EnableConsumption(true);
			}).EventTransition(GameHashes.OnStorageChange, this.lostAlgae, (AlgaeHabitat.SMInstance smi) => !smi.HasEnoughMass(GameTags.Algae))
				.EventTransition(GameHashes.OnStorageChange, this.gotWater, (AlgaeHabitat.SMInstance smi) => smi.HasEnoughMass(GameTags.Algae) && smi.HasEnoughMass(GameTags.Water));
			this.needsEmptying.QueueAnim("off", false, null).Enter(delegate(AlgaeHabitat.SMInstance smi)
			{
				smi.CreateEmptyChore();
			}).Exit(delegate(AlgaeHabitat.SMInstance smi)
			{
				smi.CancelEmptyChore();
			})
				.ToggleStatusItem(Db.Get().BuildingStatusItems.HabitatNeedsEmptying, null)
				.EventTransition(GameHashes.OnStorageChange, this.noAlgae, (AlgaeHabitat.SMInstance smi) => !smi.HasEnoughMass(GameTags.Algae) || !smi.HasEnoughMass(GameTags.Water))
				.EventTransition(GameHashes.OnStorageChange, this.gotEmptied, (AlgaeHabitat.SMInstance smi) => smi.HasEnoughMass(GameTags.Algae) && smi.HasEnoughMass(GameTags.Water) && !smi.NeedsEmptying());
			this.gotWater.PlayAnim("working_pre").OnAnimQueueComplete(this.needsEmptying);
			this.generatingOxygen.Enter(delegate(AlgaeHabitat.SMInstance smi)
			{
				smi.master.operational.SetActive(true, false);
			}).Exit(delegate(AlgaeHabitat.SMInstance smi)
			{
				smi.master.operational.SetActive(false, false);
			}).Update("GeneratingOxygen", delegate(AlgaeHabitat.SMInstance smi, float dt)
			{
				int num = Grid.PosToCell(smi.master.transform.GetPosition());
				smi.converter.OutputMultiplier = ((Grid.LightCount[num] > 0) ? smi.master.lightBonusMultiplier : 1f);
			}, UpdateRate.SIM_200ms, false)
				.QueueAnim("working_loop", true, null)
				.EventTransition(GameHashes.OnStorageChange, this.stoppedGeneratingOxygen, (AlgaeHabitat.SMInstance smi) => !smi.HasEnoughMass(GameTags.Water) || !smi.HasEnoughMass(GameTags.Algae) || smi.NeedsEmptying());
			this.stoppedGeneratingOxygen.PlayAnim("working_pst").OnAnimQueueComplete(this.stoppedGeneratingOxygenTransition);
			this.stoppedGeneratingOxygenTransition.EventTransition(GameHashes.OnStorageChange, this.needsEmptying, (AlgaeHabitat.SMInstance smi) => smi.NeedsEmptying()).EventTransition(GameHashes.OnStorageChange, this.noWater, (AlgaeHabitat.SMInstance smi) => !smi.HasEnoughMass(GameTags.Water)).EventTransition(GameHashes.OnStorageChange, this.lostAlgae, (AlgaeHabitat.SMInstance smi) => !smi.HasEnoughMass(GameTags.Algae))
				.EventTransition(GameHashes.OnStorageChange, this.gotWater, (AlgaeHabitat.SMInstance smi) => smi.HasEnoughMass(GameTags.Water) && smi.HasEnoughMass(GameTags.Algae));
		}

		// Token: 0x04006F6F RID: 28527
		public GameStateMachine<AlgaeHabitat.States, AlgaeHabitat.SMInstance, AlgaeHabitat, object>.State generatingOxygen;

		// Token: 0x04006F70 RID: 28528
		public GameStateMachine<AlgaeHabitat.States, AlgaeHabitat.SMInstance, AlgaeHabitat, object>.State stoppedGeneratingOxygen;

		// Token: 0x04006F71 RID: 28529
		public GameStateMachine<AlgaeHabitat.States, AlgaeHabitat.SMInstance, AlgaeHabitat, object>.State stoppedGeneratingOxygenTransition;

		// Token: 0x04006F72 RID: 28530
		public GameStateMachine<AlgaeHabitat.States, AlgaeHabitat.SMInstance, AlgaeHabitat, object>.State noWater;

		// Token: 0x04006F73 RID: 28531
		public GameStateMachine<AlgaeHabitat.States, AlgaeHabitat.SMInstance, AlgaeHabitat, object>.State noAlgae;

		// Token: 0x04006F74 RID: 28532
		public GameStateMachine<AlgaeHabitat.States, AlgaeHabitat.SMInstance, AlgaeHabitat, object>.State needsEmptying;

		// Token: 0x04006F75 RID: 28533
		public GameStateMachine<AlgaeHabitat.States, AlgaeHabitat.SMInstance, AlgaeHabitat, object>.State gotAlgae;

		// Token: 0x04006F76 RID: 28534
		public GameStateMachine<AlgaeHabitat.States, AlgaeHabitat.SMInstance, AlgaeHabitat, object>.State gotWater;

		// Token: 0x04006F77 RID: 28535
		public GameStateMachine<AlgaeHabitat.States, AlgaeHabitat.SMInstance, AlgaeHabitat, object>.State gotEmptied;

		// Token: 0x04006F78 RID: 28536
		public GameStateMachine<AlgaeHabitat.States, AlgaeHabitat.SMInstance, AlgaeHabitat, object>.State lostAlgae;

		// Token: 0x04006F79 RID: 28537
		public GameStateMachine<AlgaeHabitat.States, AlgaeHabitat.SMInstance, AlgaeHabitat, object>.State notoperational;
	}
}
