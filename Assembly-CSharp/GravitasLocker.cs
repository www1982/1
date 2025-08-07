using System;
using UnityEngine;

// Token: 0x0200093F RID: 2367
public class GravitasLocker : GameStateMachine<GravitasLocker, GravitasLocker.Instance, IStateMachineTarget, GravitasLocker.Def>
{
	// Token: 0x0600436A RID: 17258 RVA: 0x00184CBC File Offset: 0x00182EBC
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.close;
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		this.close.ParamTransition<bool>(this.IsOpen, this.open, GameStateMachine<GravitasLocker, GravitasLocker.Instance, IStateMachineTarget, GravitasLocker.Def>.IsTrue).DefaultState(this.close.idle);
		this.close.idle.PlayAnim("on").ParamTransition<bool>(this.WorkOrderGiven, this.close.work, GameStateMachine<GravitasLocker, GravitasLocker.Instance, IStateMachineTarget, GravitasLocker.Def>.IsTrue);
		this.close.work.DefaultState(this.close.work.waitingForDupe);
		this.close.work.waitingForDupe.Enter(new StateMachine<GravitasLocker, GravitasLocker.Instance, IStateMachineTarget, GravitasLocker.Def>.State.Callback(GravitasLocker.StartlWorkChore_OpenLocker)).Exit(new StateMachine<GravitasLocker, GravitasLocker.Instance, IStateMachineTarget, GravitasLocker.Def>.State.Callback(GravitasLocker.StopWorkChore)).WorkableCompleteTransition((GravitasLocker.Instance smi) => smi.GetWorkable(), this.close.work.complete)
			.ParamTransition<bool>(this.WorkOrderGiven, this.close, GameStateMachine<GravitasLocker, GravitasLocker.Instance, IStateMachineTarget, GravitasLocker.Def>.IsFalse);
		this.close.work.complete.Enter(delegate(GravitasLocker.Instance smi)
		{
			this.WorkOrderGiven.Set(false, smi, false);
		}).Enter(new StateMachine<GravitasLocker, GravitasLocker.Instance, IStateMachineTarget, GravitasLocker.Def>.State.Callback(GravitasLocker.Open)).TriggerOnEnter(GameHashes.UIRefresh, null);
		this.open.ParamTransition<bool>(this.IsOpen, this.close, GameStateMachine<GravitasLocker, GravitasLocker.Instance, IStateMachineTarget, GravitasLocker.Def>.IsFalse).DefaultState(this.open.opening);
		this.open.opening.PlayAnim("working").OnAnimQueueComplete(this.open.idle);
		this.open.idle.PlayAnim("empty").Enter(new StateMachine<GravitasLocker, GravitasLocker.Instance, IStateMachineTarget, GravitasLocker.Def>.State.Callback(GravitasLocker.SpawnLoot)).ParamTransition<bool>(this.WorkOrderGiven, this.open.work, GameStateMachine<GravitasLocker, GravitasLocker.Instance, IStateMachineTarget, GravitasLocker.Def>.IsTrue);
		this.open.work.DefaultState(this.open.work.waitingForDupe);
		this.open.work.waitingForDupe.Enter(new StateMachine<GravitasLocker, GravitasLocker.Instance, IStateMachineTarget, GravitasLocker.Def>.State.Callback(GravitasLocker.StartWorkChore_CloseLocker)).Exit(new StateMachine<GravitasLocker, GravitasLocker.Instance, IStateMachineTarget, GravitasLocker.Def>.State.Callback(GravitasLocker.StopWorkChore)).WorkableCompleteTransition((GravitasLocker.Instance smi) => smi.GetWorkable(), this.open.work.complete)
			.ParamTransition<bool>(this.WorkOrderGiven, this.open.idle, GameStateMachine<GravitasLocker, GravitasLocker.Instance, IStateMachineTarget, GravitasLocker.Def>.IsFalse);
		this.open.work.complete.Enter(delegate(GravitasLocker.Instance smi)
		{
			this.WorkOrderGiven.Set(false, smi, false);
		}).Enter(new StateMachine<GravitasLocker, GravitasLocker.Instance, IStateMachineTarget, GravitasLocker.Def>.State.Callback(GravitasLocker.Close)).TriggerOnEnter(GameHashes.UIRefresh, null);
	}

	// Token: 0x0600436B RID: 17259 RVA: 0x00184F88 File Offset: 0x00183188
	public static void Open(GravitasLocker.Instance smi)
	{
		smi.Open();
	}

	// Token: 0x0600436C RID: 17260 RVA: 0x00184F90 File Offset: 0x00183190
	public static void Close(GravitasLocker.Instance smi)
	{
		smi.Close();
	}

	// Token: 0x0600436D RID: 17261 RVA: 0x00184F98 File Offset: 0x00183198
	public static void SpawnLoot(GravitasLocker.Instance smi)
	{
		smi.SpawnLoot();
	}

	// Token: 0x0600436E RID: 17262 RVA: 0x00184FA0 File Offset: 0x001831A0
	public static void StartWorkChore_CloseLocker(GravitasLocker.Instance smi)
	{
		smi.CreateWorkChore_CloseLocker();
	}

	// Token: 0x0600436F RID: 17263 RVA: 0x00184FA8 File Offset: 0x001831A8
	public static void StartlWorkChore_OpenLocker(GravitasLocker.Instance smi)
	{
		smi.CreateWorkChore_OpenLocker();
	}

	// Token: 0x06004370 RID: 17264 RVA: 0x00184FB0 File Offset: 0x001831B0
	public static void StopWorkChore(GravitasLocker.Instance smi)
	{
		smi.StopWorkChore();
	}

	// Token: 0x04002CEC RID: 11500
	public const float CLOSE_WORKTIME = 1f;

	// Token: 0x04002CED RID: 11501
	public const float OPEN_WORKTIME = 1.5f;

	// Token: 0x04002CEE RID: 11502
	public const string CLOSED_ANIM_NAME = "on";

	// Token: 0x04002CEF RID: 11503
	public const string OPENING_ANIM_NAME = "working";

	// Token: 0x04002CF0 RID: 11504
	public const string OPENED = "empty";

	// Token: 0x04002CF1 RID: 11505
	private StateMachine<GravitasLocker, GravitasLocker.Instance, IStateMachineTarget, GravitasLocker.Def>.BoolParameter IsOpen;

	// Token: 0x04002CF2 RID: 11506
	private StateMachine<GravitasLocker, GravitasLocker.Instance, IStateMachineTarget, GravitasLocker.Def>.BoolParameter WasEmptied;

	// Token: 0x04002CF3 RID: 11507
	private StateMachine<GravitasLocker, GravitasLocker.Instance, IStateMachineTarget, GravitasLocker.Def>.BoolParameter WorkOrderGiven;

	// Token: 0x04002CF4 RID: 11508
	public GravitasLocker.CloseStates close;

	// Token: 0x04002CF5 RID: 11509
	public GravitasLocker.OpenStates open;

	// Token: 0x02001927 RID: 6439
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04007B71 RID: 31601
		public bool CanBeClosed;

		// Token: 0x04007B72 RID: 31602
		public string SideScreen_OpenButtonText;

		// Token: 0x04007B73 RID: 31603
		public string SideScreen_OpenButtonTooltip;

		// Token: 0x04007B74 RID: 31604
		public string SideScreen_CancelOpenButtonText;

		// Token: 0x04007B75 RID: 31605
		public string SideScreen_CancelOpenButtonTooltip;

		// Token: 0x04007B76 RID: 31606
		public string SideScreen_CloseButtonText;

		// Token: 0x04007B77 RID: 31607
		public string SideScreen_CloseButtonTooltip;

		// Token: 0x04007B78 RID: 31608
		public string SideScreen_CancelCloseButtonText;

		// Token: 0x04007B79 RID: 31609
		public string SideScreen_CancelCloseButtonTooltip;

		// Token: 0x04007B7A RID: 31610
		public string OPEN_INTERACT_ANIM_NAME = "anim_interacts_clothingfactory_kanim";

		// Token: 0x04007B7B RID: 31611
		public string CLOSE_INTERACT_ANIM_NAME = "anim_interacts_clothingfactory_kanim";

		// Token: 0x04007B7C RID: 31612
		public string[] ObjectsToSpawn = new string[0];

		// Token: 0x04007B7D RID: 31613
		public string[] LootSymbols = new string[0];
	}

	// Token: 0x02001928 RID: 6440
	public class WorkStates : GameStateMachine<GravitasLocker, GravitasLocker.Instance, IStateMachineTarget, GravitasLocker.Def>.State
	{
		// Token: 0x04007B7E RID: 31614
		public GameStateMachine<GravitasLocker, GravitasLocker.Instance, IStateMachineTarget, GravitasLocker.Def>.State waitingForDupe;

		// Token: 0x04007B7F RID: 31615
		public GameStateMachine<GravitasLocker, GravitasLocker.Instance, IStateMachineTarget, GravitasLocker.Def>.State complete;
	}

	// Token: 0x02001929 RID: 6441
	public class CloseStates : GameStateMachine<GravitasLocker, GravitasLocker.Instance, IStateMachineTarget, GravitasLocker.Def>.State
	{
		// Token: 0x04007B80 RID: 31616
		public GameStateMachine<GravitasLocker, GravitasLocker.Instance, IStateMachineTarget, GravitasLocker.Def>.State idle;

		// Token: 0x04007B81 RID: 31617
		public GravitasLocker.WorkStates work;
	}

	// Token: 0x0200192A RID: 6442
	public class OpenStates : GameStateMachine<GravitasLocker, GravitasLocker.Instance, IStateMachineTarget, GravitasLocker.Def>.State
	{
		// Token: 0x04007B82 RID: 31618
		public GameStateMachine<GravitasLocker, GravitasLocker.Instance, IStateMachineTarget, GravitasLocker.Def>.State opening;

		// Token: 0x04007B83 RID: 31619
		public GameStateMachine<GravitasLocker, GravitasLocker.Instance, IStateMachineTarget, GravitasLocker.Def>.State idle;

		// Token: 0x04007B84 RID: 31620
		public GravitasLocker.WorkStates work;
	}

	// Token: 0x0200192B RID: 6443
	public new class Instance : GameStateMachine<GravitasLocker, GravitasLocker.Instance, IStateMachineTarget, GravitasLocker.Def>.GameInstance, ISidescreenButtonControl
	{
		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x06009E86 RID: 40582 RVA: 0x00397078 File Offset: 0x00395278
		public bool WorkOrderGiven
		{
			get
			{
				return base.smi.sm.WorkOrderGiven.Get(base.smi);
			}
		}

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x06009E87 RID: 40583 RVA: 0x00397095 File Offset: 0x00395295
		public bool IsOpen
		{
			get
			{
				return base.smi.sm.IsOpen.Get(base.smi);
			}
		}

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x06009E88 RID: 40584 RVA: 0x003970B2 File Offset: 0x003952B2
		public bool HasContents
		{
			get
			{
				return !base.smi.sm.WasEmptied.Get(base.smi) && base.def.ObjectsToSpawn.Length != 0;
			}
		}

		// Token: 0x06009E89 RID: 40585 RVA: 0x003970E2 File Offset: 0x003952E2
		public Workable GetWorkable()
		{
			return this.workable;
		}

		// Token: 0x06009E8A RID: 40586 RVA: 0x003970EA File Offset: 0x003952EA
		public void Open()
		{
			base.smi.sm.IsOpen.Set(true, base.smi, false);
		}

		// Token: 0x06009E8B RID: 40587 RVA: 0x0039710A File Offset: 0x0039530A
		public void Close()
		{
			base.smi.sm.IsOpen.Set(false, base.smi, false);
		}

		// Token: 0x06009E8C RID: 40588 RVA: 0x0039712A File Offset: 0x0039532A
		public Instance(IStateMachineTarget master, GravitasLocker.Def def)
			: base(master, def)
		{
		}

		// Token: 0x06009E8D RID: 40589 RVA: 0x00397134 File Offset: 0x00395334
		public override void StartSM()
		{
			this.DefineDropSpawnPositions();
			base.StartSM();
			this.UpdateContentPreviewSymbols();
		}

		// Token: 0x06009E8E RID: 40590 RVA: 0x00397148 File Offset: 0x00395348
		public void DefineDropSpawnPositions()
		{
			if (this.dropSpawnPositions == null && base.def.LootSymbols.Length != 0)
			{
				this.dropSpawnPositions = new Vector3[base.def.LootSymbols.Length];
				for (int i = 0; i < this.dropSpawnPositions.Length; i++)
				{
					bool flag;
					Vector3 vector = this.animController.GetSymbolTransform(base.def.LootSymbols[i], out flag).GetColumn(3);
					vector.z = Grid.GetLayerZ(Grid.SceneLayer.Ore);
					this.dropSpawnPositions[i] = (flag ? vector : base.gameObject.transform.GetPosition());
				}
			}
		}

		// Token: 0x06009E8F RID: 40591 RVA: 0x003971FC File Offset: 0x003953FC
		public void CreateWorkChore_CloseLocker()
		{
			if (this.chore == null)
			{
				this.workable.SetWorkTime(1f);
				this.chore = new WorkChore<Workable>(Db.Get().ChoreTypes.Repair, this.workable, null, true, null, null, null, true, null, false, true, Assets.GetAnim(base.def.CLOSE_INTERACT_ANIM_NAME), false, true, true, PriorityScreen.PriorityClass.high, 5, false, true);
			}
		}

		// Token: 0x06009E90 RID: 40592 RVA: 0x00397268 File Offset: 0x00395468
		public void CreateWorkChore_OpenLocker()
		{
			if (this.chore == null)
			{
				this.workable.SetWorkTime(1.5f);
				this.chore = new WorkChore<Workable>(Db.Get().ChoreTypes.EmptyStorage, this.workable, null, true, null, null, null, true, null, false, true, Assets.GetAnim(base.def.OPEN_INTERACT_ANIM_NAME), false, true, true, PriorityScreen.PriorityClass.high, 5, false, true);
			}
		}

		// Token: 0x06009E91 RID: 40593 RVA: 0x003972D2 File Offset: 0x003954D2
		public void StopWorkChore()
		{
			if (this.chore != null)
			{
				this.chore.Cancel("Canceled by user");
				this.chore = null;
			}
		}

		// Token: 0x06009E92 RID: 40594 RVA: 0x003972F4 File Offset: 0x003954F4
		public void SpawnLoot()
		{
			if (this.HasContents)
			{
				for (int i = 0; i < base.def.ObjectsToSpawn.Length; i++)
				{
					string text = base.def.ObjectsToSpawn[i];
					GameObject gameObject = Scenario.SpawnPrefab(Grid.PosToCell(base.gameObject), 0, 0, text, Grid.SceneLayer.Ore);
					gameObject.SetActive(true);
					if (this.dropSpawnPositions != null && i < this.dropSpawnPositions.Length)
					{
						gameObject.transform.position = this.dropSpawnPositions[i];
					}
				}
				base.smi.sm.WasEmptied.Set(true, base.smi, false);
				this.UpdateContentPreviewSymbols();
			}
		}

		// Token: 0x06009E93 RID: 40595 RVA: 0x003973A0 File Offset: 0x003955A0
		public void UpdateContentPreviewSymbols()
		{
			for (int i = 0; i < base.def.LootSymbols.Length; i++)
			{
				this.animController.SetSymbolVisiblity(base.def.LootSymbols[i], false);
			}
			if (this.HasContents)
			{
				for (int j = 0; j < Mathf.Min(base.def.LootSymbols.Length, base.def.ObjectsToSpawn.Length); j++)
				{
					KAnim.Build.Symbol symbolByIndex = Assets.GetPrefab(base.def.ObjectsToSpawn[j]).GetComponent<KBatchedAnimController>().AnimFiles[0].GetData().build.GetSymbolByIndex(0U);
					SymbolOverrideController component = base.gameObject.GetComponent<SymbolOverrideController>();
					string text = base.def.LootSymbols[j];
					component.AddSymbolOverride(text, symbolByIndex, 0);
					this.animController.SetSymbolVisiblity(text, true);
				}
			}
		}

		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x06009E94 RID: 40596 RVA: 0x00397488 File Offset: 0x00395688
		public string SidescreenButtonText
		{
			get
			{
				if (!this.IsOpen)
				{
					if (!this.WorkOrderGiven)
					{
						return base.def.SideScreen_OpenButtonText;
					}
					return base.def.SideScreen_CancelOpenButtonText;
				}
				else
				{
					if (!this.WorkOrderGiven)
					{
						return base.def.SideScreen_CloseButtonText;
					}
					return base.def.SideScreen_CancelCloseButtonText;
				}
			}
		}

		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x06009E95 RID: 40597 RVA: 0x003974DC File Offset: 0x003956DC
		public string SidescreenButtonTooltip
		{
			get
			{
				if (!this.IsOpen)
				{
					if (!this.WorkOrderGiven)
					{
						return base.def.SideScreen_OpenButtonTooltip;
					}
					return base.def.SideScreen_CancelOpenButtonTooltip;
				}
				else
				{
					if (!this.WorkOrderGiven)
					{
						return base.def.SideScreen_CloseButtonTooltip;
					}
					return base.def.SideScreen_CancelCloseButtonTooltip;
				}
			}
		}

		// Token: 0x06009E96 RID: 40598 RVA: 0x00397530 File Offset: 0x00395730
		public bool SidescreenEnabled()
		{
			return !this.IsOpen || base.def.CanBeClosed;
		}

		// Token: 0x06009E97 RID: 40599 RVA: 0x00397547 File Offset: 0x00395747
		public bool SidescreenButtonInteractable()
		{
			return !this.IsOpen || base.def.CanBeClosed;
		}

		// Token: 0x06009E98 RID: 40600 RVA: 0x0039755E File Offset: 0x0039575E
		public int HorizontalGroupID()
		{
			return 0;
		}

		// Token: 0x06009E99 RID: 40601 RVA: 0x00397561 File Offset: 0x00395761
		public int ButtonSideScreenSortOrder()
		{
			return 20;
		}

		// Token: 0x06009E9A RID: 40602 RVA: 0x00397565 File Offset: 0x00395765
		public void SetButtonTextOverride(ButtonMenuTextOverride textOverride)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06009E9B RID: 40603 RVA: 0x0039756C File Offset: 0x0039576C
		public void OnSidescreenButtonPressed()
		{
			base.smi.sm.WorkOrderGiven.Set(!base.smi.sm.WorkOrderGiven.Get(base.smi), base.smi, false);
		}

		// Token: 0x04007B85 RID: 31621
		[MyCmpGet]
		private Workable workable;

		// Token: 0x04007B86 RID: 31622
		[MyCmpGet]
		private KBatchedAnimController animController;

		// Token: 0x04007B87 RID: 31623
		private Chore chore;

		// Token: 0x04007B88 RID: 31624
		private Vector3[] dropSpawnPositions;
	}
}
