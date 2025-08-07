using System;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x0200088F RID: 2191
public class CryoTank : StateMachineComponent<CryoTank.StatesInstance>, ISidescreenButtonControl
{
	// Token: 0x1700042E RID: 1070
	// (get) Token: 0x06003C4F RID: 15439 RVA: 0x0014E463 File Offset: 0x0014C663
	public string SidescreenButtonText
	{
		get
		{
			return BUILDINGS.PREFABS.CRYOTANK.DEFROSTBUTTON;
		}
	}

	// Token: 0x1700042F RID: 1071
	// (get) Token: 0x06003C50 RID: 15440 RVA: 0x0014E46F File Offset: 0x0014C66F
	public string SidescreenButtonTooltip
	{
		get
		{
			return BUILDINGS.PREFABS.CRYOTANK.DEFROSTBUTTONTOOLTIP;
		}
	}

	// Token: 0x06003C51 RID: 15441 RVA: 0x0014E47B File Offset: 0x0014C67B
	public bool SidescreenEnabled()
	{
		return true;
	}

	// Token: 0x06003C52 RID: 15442 RVA: 0x0014E47E File Offset: 0x0014C67E
	public void OnSidescreenButtonPressed()
	{
		this.OnClickOpen();
	}

	// Token: 0x06003C53 RID: 15443 RVA: 0x0014E486 File Offset: 0x0014C686
	public bool SidescreenButtonInteractable()
	{
		return this.HasDefrostedFriend();
	}

	// Token: 0x06003C54 RID: 15444 RVA: 0x0014E48E File Offset: 0x0014C68E
	public int ButtonSideScreenSortOrder()
	{
		return 20;
	}

	// Token: 0x06003C55 RID: 15445 RVA: 0x0014E492 File Offset: 0x0014C692
	public void SetButtonTextOverride(ButtonMenuTextOverride text)
	{
		throw new NotImplementedException();
	}

	// Token: 0x06003C56 RID: 15446 RVA: 0x0014E499 File Offset: 0x0014C699
	public int HorizontalGroupID()
	{
		return -1;
	}

	// Token: 0x06003C57 RID: 15447 RVA: 0x0014E49C File Offset: 0x0014C69C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
		Demolishable component = base.GetComponent<Demolishable>();
		if (component != null)
		{
			component.allowDemolition = !this.HasDefrostedFriend();
		}
	}

	// Token: 0x06003C58 RID: 15448 RVA: 0x0014E4D9 File Offset: 0x0014C6D9
	public bool HasDefrostedFriend()
	{
		return base.smi.IsInsideState(base.smi.sm.closed) && this.chore == null;
	}

	// Token: 0x06003C59 RID: 15449 RVA: 0x0014E504 File Offset: 0x0014C704
	public void DropContents()
	{
		MinionStartingStats minionStartingStats = new MinionStartingStats(GameTags.Minions.Models.Standard, false, null, "AncientKnowledge", false);
		GameObject prefab = Assets.GetPrefab(BaseMinionConfig.GetMinionIDForModel(minionStartingStats.personality.model));
		GameObject gameObject = Util.KInstantiate(prefab, null, null);
		gameObject.name = prefab.name;
		Immigration.Instance.ApplyDefaultPersonalPriorities(gameObject);
		Vector3 vector = Grid.CellToPosCBC(Grid.OffsetCell(Grid.PosToCell(base.transform.position), this.dropOffset), Grid.SceneLayer.Move);
		gameObject.transform.SetLocalPosition(vector);
		gameObject.SetActive(true);
		minionStartingStats.Apply(gameObject);
		gameObject.GetComponent<MinionIdentity>().arrivalTime = (float)global::UnityEngine.Random.Range(-2000, -1000);
		MinionResume component = gameObject.GetComponent<MinionResume>();
		int num = 3;
		for (int i = 0; i < num; i++)
		{
			component.ForceAddSkillPoint();
		}
		base.smi.sm.defrostedDuplicant.Set(gameObject, base.smi, false);
		gameObject.GetComponent<Navigator>().SetCurrentNavType(NavType.Floor);
		ChoreProvider component2 = gameObject.GetComponent<ChoreProvider>();
		if (component2 != null)
		{
			base.smi.defrostAnimChore = new EmoteChore(component2, Db.Get().ChoreTypes.EmoteHighPriority, "anim_interacts_cryo_chamber_kanim", new HashedString[] { "defrost", "defrost_exit" }, KAnim.PlayMode.Once, false);
			Vector3 position = gameObject.transform.GetPosition();
			position.z = Grid.GetLayerZ(Grid.SceneLayer.Gas);
			gameObject.transform.SetPosition(position);
			gameObject.GetMyWorld().SetDupeVisited();
		}
		SaveGame.Instance.ColonyAchievementTracker.defrostedDuplicant = true;
	}

	// Token: 0x06003C5A RID: 15450 RVA: 0x0014E6B0 File Offset: 0x0014C8B0
	public void ShowEventPopup()
	{
		GameObject gameObject = base.smi.sm.defrostedDuplicant.Get(base.smi);
		if (this.opener != null && gameObject != null)
		{
			SimpleEvent.StatesInstance statesInstance = GameplayEventManager.Instance.StartNewEvent(Db.Get().GameplayEvents.CryoFriend, -1, null).smi as SimpleEvent.StatesInstance;
			statesInstance.minions = new GameObject[] { gameObject, this.opener };
			statesInstance.SetTextParameter("dupe", this.opener.GetProperName());
			statesInstance.SetTextParameter("friend", gameObject.GetProperName());
			statesInstance.ShowEventPopup();
		}
	}

	// Token: 0x06003C5B RID: 15451 RVA: 0x0014E75C File Offset: 0x0014C95C
	public void Cheer()
	{
		GameObject gameObject = base.smi.sm.defrostedDuplicant.Get(base.smi);
		if (this.opener != null && gameObject != null)
		{
			Db db = Db.Get();
			this.opener.GetComponent<Effects>().Add(Db.Get().effects.Get("CryoFriend"), true);
			new EmoteChore(this.opener.GetComponent<Effects>(), db.ChoreTypes.EmoteHighPriority, db.Emotes.Minion.Cheer, 1, null);
			gameObject.GetComponent<Effects>().Add(Db.Get().effects.Get("CryoFriend"), true);
			new EmoteChore(gameObject.GetComponent<Effects>(), db.ChoreTypes.EmoteHighPriority, db.Emotes.Minion.Cheer, 1, null);
		}
	}

	// Token: 0x06003C5C RID: 15452 RVA: 0x0014E846 File Offset: 0x0014CA46
	private void OnClickOpen()
	{
		this.ActivateChore(null);
	}

	// Token: 0x06003C5D RID: 15453 RVA: 0x0014E84F File Offset: 0x0014CA4F
	private void OnClickCancel()
	{
		this.CancelActivateChore(null);
	}

	// Token: 0x06003C5E RID: 15454 RVA: 0x0014E858 File Offset: 0x0014CA58
	public void ActivateChore(object param = null)
	{
		if (this.chore != null)
		{
			return;
		}
		base.GetComponent<Workable>().SetWorkTime(1.5f);
		this.chore = new WorkChore<Workable>(Db.Get().ChoreTypes.EmptyStorage, this, null, true, delegate(Chore o)
		{
			this.CompleteActivateChore();
		}, null, null, true, null, false, true, Assets.GetAnim(this.overrideAnim), false, true, true, PriorityScreen.PriorityClass.high, 5, false, true);
	}

	// Token: 0x06003C5F RID: 15455 RVA: 0x0014E8C4 File Offset: 0x0014CAC4
	public void CancelActivateChore(object param = null)
	{
		if (this.chore == null)
		{
			return;
		}
		this.chore.Cancel("User cancelled");
		this.chore = null;
	}

	// Token: 0x06003C60 RID: 15456 RVA: 0x0014E8E8 File Offset: 0x0014CAE8
	private void CompleteActivateChore()
	{
		this.opener = this.chore.driver.gameObject;
		base.smi.GoTo(base.smi.sm.open);
		this.chore = null;
		Demolishable component = base.smi.GetComponent<Demolishable>();
		if (component != null)
		{
			component.allowDemolition = true;
		}
		Game.Instance.userMenu.Refresh(base.gameObject);
	}

	// Token: 0x0400250C RID: 9484
	public string[][] possible_contents_ids;

	// Token: 0x0400250D RID: 9485
	public string machineSound;

	// Token: 0x0400250E RID: 9486
	public string overrideAnim;

	// Token: 0x0400250F RID: 9487
	public CellOffset dropOffset = CellOffset.none;

	// Token: 0x04002510 RID: 9488
	private GameObject opener;

	// Token: 0x04002511 RID: 9489
	private Chore chore;

	// Token: 0x02001859 RID: 6233
	public class StatesInstance : GameStateMachine<CryoTank.States, CryoTank.StatesInstance, CryoTank, object>.GameInstance
	{
		// Token: 0x06009C58 RID: 40024 RVA: 0x003907F3 File Offset: 0x0038E9F3
		public StatesInstance(CryoTank master)
			: base(master)
		{
		}

		// Token: 0x040078B7 RID: 30903
		public Chore defrostAnimChore;
	}

	// Token: 0x0200185A RID: 6234
	public class States : GameStateMachine<CryoTank.States, CryoTank.StatesInstance, CryoTank>
	{
		// Token: 0x06009C59 RID: 40025 RVA: 0x003907FC File Offset: 0x0038E9FC
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.closed;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.closed.PlayAnim("on").Enter(delegate(CryoTank.StatesInstance smi)
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
			this.open.GoTo(this.defrost).Exit(delegate(CryoTank.StatesInstance smi)
			{
				smi.master.DropContents();
			});
			this.defrost.PlayAnim("defrost").OnAnimQueueComplete(this.defrostExit).Update(delegate(CryoTank.StatesInstance smi, float dt)
			{
				smi.sm.defrostedDuplicant.Get(smi).GetComponent<KBatchedAnimController>().SetSceneLayer(Grid.SceneLayer.BuildingUse);
			}, UpdateRate.SIM_200ms, false)
				.Exit(delegate(CryoTank.StatesInstance smi)
				{
					smi.master.ShowEventPopup();
				});
			this.defrostExit.PlayAnim("defrost_exit").Update(delegate(CryoTank.StatesInstance smi, float dt)
			{
				if (smi.defrostAnimChore == null || smi.defrostAnimChore.isComplete)
				{
					smi.GoTo(this.off);
				}
			}, UpdateRate.SIM_200ms, false).Exit(delegate(CryoTank.StatesInstance smi)
			{
				GameObject gameObject = smi.sm.defrostedDuplicant.Get(smi);
				if (gameObject != null)
				{
					gameObject.GetComponent<KBatchedAnimController>().SetSceneLayer(Grid.SceneLayer.Move);
					smi.master.Cheer();
				}
			});
			this.off.PlayAnim("off").Enter(delegate(CryoTank.StatesInstance smi)
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

		// Token: 0x040078B8 RID: 30904
		public StateMachine<CryoTank.States, CryoTank.StatesInstance, CryoTank, object>.TargetParameter defrostedDuplicant;

		// Token: 0x040078B9 RID: 30905
		public GameStateMachine<CryoTank.States, CryoTank.StatesInstance, CryoTank, object>.State closed;

		// Token: 0x040078BA RID: 30906
		public GameStateMachine<CryoTank.States, CryoTank.StatesInstance, CryoTank, object>.State open;

		// Token: 0x040078BB RID: 30907
		public GameStateMachine<CryoTank.States, CryoTank.StatesInstance, CryoTank, object>.State defrost;

		// Token: 0x040078BC RID: 30908
		public GameStateMachine<CryoTank.States, CryoTank.StatesInstance, CryoTank, object>.State defrostExit;

		// Token: 0x040078BD RID: 30909
		public GameStateMachine<CryoTank.States, CryoTank.StatesInstance, CryoTank, object>.State off;
	}
}
