using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000B09 RID: 2825
public class SetLocker : StateMachineComponent<SetLocker.StatesInstance>, ISidescreenButtonControl
{
	// Token: 0x060052EA RID: 21226 RVA: 0x001E2E53 File Offset: 0x001E1053
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x060052EB RID: 21227 RVA: 0x001E2E5B File Offset: 0x001E105B
	public void ChooseContents()
	{
		this.contents = this.possible_contents_ids[global::UnityEngine.Random.Range(0, this.possible_contents_ids.GetLength(0))];
	}

	// Token: 0x060052EC RID: 21228 RVA: 0x001E2E7C File Offset: 0x001E107C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
		if (this.contents == null)
		{
			this.ChooseContents();
		}
		else
		{
			string[] array = this.contents;
			for (int i = 0; i < array.Length; i++)
			{
				if (Assets.GetPrefab(array[i]) == null)
				{
					this.ChooseContents();
					break;
				}
			}
		}
		if (this.pendingRummage)
		{
			this.ActivateChore(null);
		}
	}

	// Token: 0x060052ED RID: 21229 RVA: 0x001E2EEC File Offset: 0x001E10EC
	public void DropContents()
	{
		if (this.contents == null)
		{
			return;
		}
		if (DlcManager.IsExpansion1Active() && this.numDataBanks.Length >= 2)
		{
			int num = global::UnityEngine.Random.Range(this.numDataBanks[0], this.numDataBanks[1]);
			for (int i = 0; i <= num; i++)
			{
				Scenario.SpawnPrefab(Grid.PosToCell(base.gameObject), this.dropOffset.x, this.dropOffset.y, "OrbitalResearchDatabank", Grid.SceneLayer.Front).SetActive(true);
				PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Plus, Assets.GetPrefab("OrbitalResearchDatabank".ToTag()).GetProperName(), base.smi.master.transform, 1.5f, false);
			}
		}
		for (int j = 0; j < this.contents.Length; j++)
		{
			GameObject gameObject = Scenario.SpawnPrefab(Grid.PosToCell(base.gameObject), this.dropOffset.x, this.dropOffset.y, this.contents[j], Grid.SceneLayer.Front);
			if (gameObject != null)
			{
				gameObject.SetActive(true);
				PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Plus, Assets.GetPrefab(this.contents[j].ToTag()).GetProperName(), base.smi.master.transform, 1.5f, false);
			}
		}
		base.gameObject.Trigger(-372600542, this);
	}

	// Token: 0x060052EE RID: 21230 RVA: 0x001E305B File Offset: 0x001E125B
	private void OnClickOpen()
	{
		this.ActivateChore(null);
	}

	// Token: 0x060052EF RID: 21231 RVA: 0x001E3064 File Offset: 0x001E1264
	private void OnClickCancel()
	{
		this.CancelChore(null);
	}

	// Token: 0x060052F0 RID: 21232 RVA: 0x001E3070 File Offset: 0x001E1270
	public void ActivateChore(object param = null)
	{
		if (this.chore != null)
		{
			return;
		}
		Prioritizable.AddRef(base.gameObject);
		base.Trigger(1980521255, null);
		this.pendingRummage = true;
		base.GetComponent<Workable>().SetWorkTime(1.5f);
		this.chore = new WorkChore<Workable>(Db.Get().ChoreTypes.EmptyStorage, this, null, true, delegate(Chore o)
		{
			this.CompleteChore();
		}, delegate(Chore o)
		{
			base.smi.GoTo(base.smi.sm.being_worked);
		}, delegate(Chore o)
		{
			this.OnChoreEnd();
		}, true, null, false, true, Assets.GetAnim(this.overrideAnim), false, true, true, PriorityScreen.PriorityClass.high, 5, false, true);
	}

	// Token: 0x060052F1 RID: 21233 RVA: 0x001E3110 File Offset: 0x001E1310
	public void CancelChore(object param = null)
	{
		if (this.chore == null)
		{
			return;
		}
		this.pendingRummage = false;
		Prioritizable.RemoveRef(base.gameObject);
		base.Trigger(1980521255, null);
		this.chore.Cancel("User cancelled");
		this.chore = null;
	}

	// Token: 0x060052F2 RID: 21234 RVA: 0x001E3150 File Offset: 0x001E1350
	private void OnChoreEnd()
	{
		if (this.skipAnim && this.chore != null)
		{
			base.smi.GoTo(base.smi.sm.closed);
		}
	}

	// Token: 0x060052F3 RID: 21235 RVA: 0x001E3180 File Offset: 0x001E1380
	private void CompleteChore()
	{
		this.used = true;
		if (this.skipAnim)
		{
			this.DropContents();
			base.smi.GoTo(base.smi.sm.off);
		}
		else
		{
			base.smi.GoTo(base.smi.sm.open);
		}
		this.chore = null;
		this.pendingRummage = false;
		Game.Instance.userMenu.Refresh(base.gameObject);
		Prioritizable.RemoveRef(base.gameObject);
	}

	// Token: 0x170005CF RID: 1487
	// (get) Token: 0x060052F4 RID: 21236 RVA: 0x001E3208 File Offset: 0x001E1408
	public string SidescreenButtonText
	{
		get
		{
			return (this.chore == null) ? UI.USERMENUACTIONS.OPENPOI.NAME : UI.USERMENUACTIONS.OPENPOI.NAME_OFF;
		}
	}

	// Token: 0x170005D0 RID: 1488
	// (get) Token: 0x060052F5 RID: 21237 RVA: 0x001E3223 File Offset: 0x001E1423
	public string SidescreenButtonTooltip
	{
		get
		{
			return (this.chore == null) ? UI.USERMENUACTIONS.OPENPOI.TOOLTIP : UI.USERMENUACTIONS.OPENPOI.TOOLTIP_OFF;
		}
	}

	// Token: 0x060052F6 RID: 21238 RVA: 0x001E323E File Offset: 0x001E143E
	public bool SidescreenEnabled()
	{
		return true;
	}

	// Token: 0x060052F7 RID: 21239 RVA: 0x001E3241 File Offset: 0x001E1441
	public int HorizontalGroupID()
	{
		return -1;
	}

	// Token: 0x060052F8 RID: 21240 RVA: 0x001E3244 File Offset: 0x001E1444
	public void OnSidescreenButtonPressed()
	{
		if (this.chore == null)
		{
			this.OnClickOpen();
			return;
		}
		this.OnClickCancel();
	}

	// Token: 0x060052F9 RID: 21241 RVA: 0x001E325B File Offset: 0x001E145B
	public bool SidescreenButtonInteractable()
	{
		return !this.used;
	}

	// Token: 0x060052FA RID: 21242 RVA: 0x001E3266 File Offset: 0x001E1466
	public int ButtonSideScreenSortOrder()
	{
		return 20;
	}

	// Token: 0x060052FB RID: 21243 RVA: 0x001E326A File Offset: 0x001E146A
	public void SetButtonTextOverride(ButtonMenuTextOverride text)
	{
		throw new NotImplementedException();
	}

	// Token: 0x040037C4 RID: 14276
	[MyCmpAdd]
	private Prioritizable prioritizable;

	// Token: 0x040037C5 RID: 14277
	public string[][] possible_contents_ids;

	// Token: 0x040037C6 RID: 14278
	public string machineSound;

	// Token: 0x040037C7 RID: 14279
	public string overrideAnim;

	// Token: 0x040037C8 RID: 14280
	public Vector2I dropOffset = Vector2I.zero;

	// Token: 0x040037C9 RID: 14281
	public int[] numDataBanks;

	// Token: 0x040037CA RID: 14282
	[Serialize]
	private string[] contents;

	// Token: 0x040037CB RID: 14283
	public bool dropOnDeconstruct;

	// Token: 0x040037CC RID: 14284
	public bool skipAnim;

	// Token: 0x040037CD RID: 14285
	[Serialize]
	private bool pendingRummage;

	// Token: 0x040037CE RID: 14286
	[Serialize]
	private bool used;

	// Token: 0x040037CF RID: 14287
	private Chore chore;

	// Token: 0x02001C09 RID: 7177
	public class StatesInstance : GameStateMachine<SetLocker.States, SetLocker.StatesInstance, SetLocker, object>.GameInstance
	{
		// Token: 0x0600A978 RID: 43384 RVA: 0x003B7ECE File Offset: 0x003B60CE
		public StatesInstance(SetLocker master)
			: base(master)
		{
		}

		// Token: 0x0600A979 RID: 43385 RVA: 0x003B7ED7 File Offset: 0x003B60D7
		public override void StartSM()
		{
			base.StartSM();
			base.smi.Subscribe(-702296337, delegate(object o)
			{
				if (base.smi.master.dropOnDeconstruct && base.smi.IsInsideState(base.smi.sm.closed))
				{
					base.smi.master.DropContents();
				}
			});
		}
	}

	// Token: 0x02001C0A RID: 7178
	public class States : GameStateMachine<SetLocker.States, SetLocker.StatesInstance, SetLocker>
	{
		// Token: 0x0600A97B RID: 43387 RVA: 0x003B7F48 File Offset: 0x003B6148
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.closed;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.closed.PlayAnim("on").Enter(delegate(SetLocker.StatesInstance smi)
			{
				if (smi.master.machineSound != null)
				{
					LoopingSounds component = smi.master.GetComponent<LoopingSounds>();
					if (component != null)
					{
						component.StartSound(GlobalAssets.GetSound(smi.master.machineSound, false));
					}
				}
			});
			this.being_worked.DoNothing();
			this.open.PlayAnim("working_pre").QueueAnim("working_loop", false, null).QueueAnim("working_pst", false, null)
				.OnAnimQueueComplete(this.off)
				.Exit(delegate(SetLocker.StatesInstance smi)
				{
					smi.master.DropContents();
				});
			this.off.PlayAnim("off").Enter(delegate(SetLocker.StatesInstance smi)
			{
				if (smi.master.machineSound != null)
				{
					LoopingSounds component2 = smi.master.GetComponent<LoopingSounds>();
					if (component2 != null)
					{
						component2.StopSound(GlobalAssets.GetSound(smi.master.machineSound, false));
					}
				}
			});
		}

		// Token: 0x040084DE RID: 34014
		public GameStateMachine<SetLocker.States, SetLocker.StatesInstance, SetLocker, object>.State closed;

		// Token: 0x040084DF RID: 34015
		public GameStateMachine<SetLocker.States, SetLocker.StatesInstance, SetLocker, object>.State being_worked;

		// Token: 0x040084E0 RID: 34016
		public GameStateMachine<SetLocker.States, SetLocker.StatesInstance, SetLocker, object>.State open;

		// Token: 0x040084E1 RID: 34017
		public GameStateMachine<SetLocker.States, SetLocker.StatesInstance, SetLocker, object>.State off;
	}
}
