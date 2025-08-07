using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000729 RID: 1833
public class FishFeeder : GameStateMachine<FishFeeder, FishFeeder.Instance, IStateMachineTarget, FishFeeder.Def>
{
	// Token: 0x06002E40 RID: 11840 RVA: 0x0010920C File Offset: 0x0010740C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.notoperational;
		this.root.Enter(new StateMachine<FishFeeder, FishFeeder.Instance, IStateMachineTarget, FishFeeder.Def>.State.Callback(FishFeeder.SetupFishFeederTopAndBot)).Exit(new StateMachine<FishFeeder, FishFeeder.Instance, IStateMachineTarget, FishFeeder.Def>.State.Callback(FishFeeder.CleanupFishFeederTopAndBot)).EventHandler(GameHashes.OnStorageChange, new GameStateMachine<FishFeeder, FishFeeder.Instance, IStateMachineTarget, FishFeeder.Def>.GameEvent.Callback(FishFeeder.OnStorageChange))
			.EventHandler(GameHashes.RefreshUserMenu, new GameStateMachine<FishFeeder, FishFeeder.Instance, IStateMachineTarget, FishFeeder.Def>.GameEvent.Callback(FishFeeder.OnRefreshUserMenu));
		this.notoperational.TagTransition(GameTags.Operational, this.operational, false);
		this.operational.DefaultState(this.operational.on).TagTransition(GameTags.Operational, this.notoperational, true);
		this.operational.on.DoNothing();
		int num = 19;
		FishFeeder.ballSymbols = new HashedString[num];
		for (int i = 0; i < num; i++)
		{
			FishFeeder.ballSymbols[i] = "ball" + i.ToString();
		}
	}

	// Token: 0x06002E41 RID: 11841 RVA: 0x00109304 File Offset: 0x00107504
	private static void SetupFishFeederTopAndBot(FishFeeder.Instance smi)
	{
		Storage storage = smi.Get<Storage>();
		smi.fishFeederTop = new FishFeeder.FishFeederTop(smi, FishFeeder.ballSymbols, storage.Capacity());
		smi.fishFeederTop.RefreshStorage();
		smi.fishFeederBot = new FishFeeder.FishFeederBot(smi, 10f, FishFeeder.ballSymbols);
		smi.fishFeederBot.RefreshStorage();
		smi.fishFeederTop.ToggleMutantSeedFetches(smi.ForbidMutantSeeds);
		smi.UpdateMutantSeedStatusItem();
	}

	// Token: 0x06002E42 RID: 11842 RVA: 0x00109372 File Offset: 0x00107572
	private static void CleanupFishFeederTopAndBot(FishFeeder.Instance smi)
	{
		smi.fishFeederTop.Cleanup();
	}

	// Token: 0x06002E43 RID: 11843 RVA: 0x00109380 File Offset: 0x00107580
	private static void MoveStoredContentsToConsumeOffset(FishFeeder.Instance smi)
	{
		foreach (GameObject gameObject in smi.GetComponent<Storage>().items)
		{
			if (!(gameObject == null))
			{
				FishFeeder.OnStorageChange(smi, gameObject);
			}
		}
	}

	// Token: 0x06002E44 RID: 11844 RVA: 0x001093E4 File Offset: 0x001075E4
	private static void OnStorageChange(FishFeeder.Instance smi, object data)
	{
		if ((GameObject)data == null)
		{
			return;
		}
		smi.fishFeederTop.RefreshStorage();
		smi.fishFeederBot.RefreshStorage();
	}

	// Token: 0x06002E45 RID: 11845 RVA: 0x0010940C File Offset: 0x0010760C
	private static void OnRefreshUserMenu(FishFeeder.Instance smi, object data)
	{
		if (DlcManager.FeatureRadiationEnabled())
		{
			Game.Instance.userMenu.AddButton(smi.gameObject, new KIconButtonMenu.ButtonInfo("action_switch_toggle", smi.ForbidMutantSeeds ? UI.USERMENUACTIONS.ACCEPT_MUTANT_SEEDS.ACCEPT : UI.USERMENUACTIONS.ACCEPT_MUTANT_SEEDS.REJECT, delegate
			{
				smi.ForbidMutantSeeds = !smi.ForbidMutantSeeds;
				FishFeeder.OnRefreshUserMenu(smi, null);
			}, global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.ACCEPT_MUTANT_SEEDS.FISH_FEEDER_TOOLTIP, true), 1f);
		}
	}

	// Token: 0x04001B4F RID: 6991
	public GameStateMachine<FishFeeder, FishFeeder.Instance, IStateMachineTarget, FishFeeder.Def>.State notoperational;

	// Token: 0x04001B50 RID: 6992
	public FishFeeder.OperationalState operational;

	// Token: 0x04001B51 RID: 6993
	public static HashedString[] ballSymbols;

	// Token: 0x020015CE RID: 5582
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x020015CF RID: 5583
	public class OperationalState : GameStateMachine<FishFeeder, FishFeeder.Instance, IStateMachineTarget, FishFeeder.Def>.State
	{
		// Token: 0x040070E3 RID: 28899
		public GameStateMachine<FishFeeder, FishFeeder.Instance, IStateMachineTarget, FishFeeder.Def>.State on;
	}

	// Token: 0x020015D0 RID: 5584
	public new class Instance : GameStateMachine<FishFeeder, FishFeeder.Instance, IStateMachineTarget, FishFeeder.Def>.GameInstance
	{
		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x060092BD RID: 37565 RVA: 0x00367BEA File Offset: 0x00365DEA
		// (set) Token: 0x060092BE RID: 37566 RVA: 0x00367BF2 File Offset: 0x00365DF2
		public bool ForbidMutantSeeds
		{
			get
			{
				return this.forbidMutantSeeds;
			}
			set
			{
				this.forbidMutantSeeds = value;
				this.fishFeederTop.ToggleMutantSeedFetches(this.forbidMutantSeeds);
				this.UpdateMutantSeedStatusItem();
			}
		}

		// Token: 0x060092BF RID: 37567 RVA: 0x00367C14 File Offset: 0x00365E14
		public Instance(IStateMachineTarget master, FishFeeder.Def def)
			: base(master, def)
		{
			this.mutantSeedStatusItem = new StatusItem("FISHFEEDERACCEPTSMUTANTSEEDS", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, false, 129022, null);
			base.Subscribe(-905833192, new Action<object>(this.OnCopySettingsDelegate));
		}

		// Token: 0x060092C0 RID: 37568 RVA: 0x00367C6C File Offset: 0x00365E6C
		private void OnCopySettingsDelegate(object data)
		{
			GameObject gameObject = (GameObject)data;
			if (gameObject == null)
			{
				return;
			}
			FishFeeder.Instance smi = gameObject.GetSMI<FishFeeder.Instance>();
			if (smi == null)
			{
				return;
			}
			this.ForbidMutantSeeds = smi.ForbidMutantSeeds;
		}

		// Token: 0x060092C1 RID: 37569 RVA: 0x00367CA1 File Offset: 0x00365EA1
		public void UpdateMutantSeedStatusItem()
		{
			base.gameObject.GetComponent<KSelectable>().ToggleStatusItem(this.mutantSeedStatusItem, Game.IsDlcActiveForCurrentSave("EXPANSION1_ID") && !this.forbidMutantSeeds, null);
		}

		// Token: 0x040070E4 RID: 28900
		private StatusItem mutantSeedStatusItem;

		// Token: 0x040070E5 RID: 28901
		public FishFeeder.FishFeederTop fishFeederTop;

		// Token: 0x040070E6 RID: 28902
		public FishFeeder.FishFeederBot fishFeederBot;

		// Token: 0x040070E7 RID: 28903
		[Serialize]
		private bool forbidMutantSeeds;
	}

	// Token: 0x020015D1 RID: 5585
	public class FishFeederTop : IRenderEveryTick
	{
		// Token: 0x060092C2 RID: 37570 RVA: 0x00367CD3 File Offset: 0x00365ED3
		public FishFeederTop(FishFeeder.Instance smi, HashedString[] ball_symbols, float capacity)
		{
			this.smi = smi;
			this.ballSymbols = ball_symbols;
			this.massPerBall = capacity / (float)ball_symbols.Length;
			this.FillFeeder(this.mass);
			SimAndRenderScheduler.instance.Add(this, false);
		}

		// Token: 0x060092C3 RID: 37571 RVA: 0x00367D10 File Offset: 0x00365F10
		private void FillFeeder(float mass)
		{
			KBatchedAnimController component = this.smi.GetComponent<KBatchedAnimController>();
			for (int i = 0; i < this.ballSymbols.Length; i++)
			{
				bool flag = mass > (float)(i + 1) * this.massPerBall;
				component.SetSymbolVisiblity(this.ballSymbols[i], flag);
			}
		}

		// Token: 0x060092C4 RID: 37572 RVA: 0x00367D64 File Offset: 0x00365F64
		public void RefreshStorage()
		{
			float num = 0f;
			foreach (GameObject gameObject in this.smi.GetComponent<Storage>().items)
			{
				if (!(gameObject == null))
				{
					num += gameObject.GetComponent<PrimaryElement>().Mass;
				}
			}
			this.targetMass = num;
			this.timeSinceLastBallAppeared = 0f;
		}

		// Token: 0x060092C5 RID: 37573 RVA: 0x00367DEC File Offset: 0x00365FEC
		public void RenderEveryTick(float dt)
		{
			this.timeSinceLastBallAppeared += dt;
			if (Mathf.Abs(this.targetMass - this.mass) > 1f && this.timeSinceLastBallAppeared > 0.025f)
			{
				float num = Mathf.Min(this.massPerBall, this.targetMass - this.mass);
				this.mass += num;
				this.FillFeeder(this.mass);
				this.timeSinceLastBallAppeared = 0f;
			}
		}

		// Token: 0x060092C6 RID: 37574 RVA: 0x00367E6B File Offset: 0x0036606B
		public void Cleanup()
		{
			SimAndRenderScheduler.instance.Remove(this);
		}

		// Token: 0x060092C7 RID: 37575 RVA: 0x00367E78 File Offset: 0x00366078
		public void ToggleMutantSeedFetches(bool allow)
		{
			StorageLocker component = this.smi.GetComponent<StorageLocker>();
			if (component != null)
			{
				component.UpdateForbiddenTag(GameTags.MutatedSeed, !allow);
			}
		}

		// Token: 0x040070E8 RID: 28904
		private FishFeeder.Instance smi;

		// Token: 0x040070E9 RID: 28905
		private float mass;

		// Token: 0x040070EA RID: 28906
		private float targetMass;

		// Token: 0x040070EB RID: 28907
		private HashedString[] ballSymbols;

		// Token: 0x040070EC RID: 28908
		private float massPerBall;

		// Token: 0x040070ED RID: 28909
		private float timeSinceLastBallAppeared;
	}

	// Token: 0x020015D2 RID: 5586
	public class FishFeederBot
	{
		// Token: 0x060092C8 RID: 37576 RVA: 0x00367EAC File Offset: 0x003660AC
		public FishFeederBot(FishFeeder.Instance smi, float mass_per_ball, HashedString[] ball_symbols)
		{
			this.smi = smi;
			this.massPerBall = mass_per_ball;
			this.anim = GameUtil.KInstantiate(Assets.GetPrefab("FishFeederBot"), smi.transform.GetPosition(), Grid.SceneLayer.Front, null, 0).GetComponent<KBatchedAnimController>();
			this.anim.transform.SetParent(smi.transform);
			this.anim.gameObject.SetActive(true);
			this.anim.SetSceneLayer(Grid.SceneLayer.Building);
			this.anim.Play("ball", KAnim.PlayMode.Once, 1f, 0f);
			this.anim.Stop();
			foreach (HashedString hashedString in ball_symbols)
			{
				this.anim.SetSymbolVisiblity(hashedString, false);
			}
			foreach (Storage storage in smi.gameObject.GetComponents<Storage>())
			{
				if (storage.storageID == "FishFeederBot")
				{
					this.botStorage = storage;
				}
				else if (storage.storageID == "FishFeederTop")
				{
					this.topStorage = storage;
				}
			}
			if (!this.botStorage.IsEmpty())
			{
				this.SetBallSymbol(this.botStorage.items[0].gameObject);
				this.anim.Play("ball", KAnim.PlayMode.Once, 1f, 0f);
			}
		}

		// Token: 0x060092C9 RID: 37577 RVA: 0x00368030 File Offset: 0x00366230
		public void RefreshStorage()
		{
			if (this.refreshingStorage)
			{
				return;
			}
			this.refreshingStorage = true;
			foreach (GameObject gameObject in this.botStorage.items)
			{
				if (!(gameObject == null))
				{
					int num = Grid.CellBelow(Grid.CellBelow(Grid.PosToCell(this.smi.transform.GetPosition())));
					gameObject.transform.SetPosition(Grid.CellToPosCBC(num, Grid.SceneLayer.Ore));
				}
			}
			if (this.botStorage.IsEmpty())
			{
				float num2 = 0f;
				foreach (GameObject gameObject2 in this.topStorage.items)
				{
					if (!(gameObject2 == null))
					{
						num2 += gameObject2.GetComponent<PrimaryElement>().Mass;
					}
				}
				if (num2 > 0f)
				{
					Pickupable pickupable = this.topStorage.items[0].GetComponent<Pickupable>().Take(this.massPerBall);
					this.botStorage.Store(pickupable.gameObject, false, false, true, false);
					this.SetBallSymbol(pickupable.gameObject);
					this.anim.Play("ball", KAnim.PlayMode.Once, 1f, 0f);
				}
				else
				{
					this.anim.SetSymbolVisiblity(FishFeeder.FishFeederBot.HASH_FEEDBALL, false);
				}
			}
			this.refreshingStorage = false;
		}

		// Token: 0x060092CA RID: 37578 RVA: 0x003681CC File Offset: 0x003663CC
		private void SetBallSymbol(GameObject stored_go)
		{
			if (stored_go == null)
			{
				return;
			}
			this.anim.SetSymbolVisiblity(FishFeeder.FishFeederBot.HASH_FEEDBALL, true);
			KAnim.Build build = stored_go.GetComponent<KBatchedAnimController>().AnimFiles[0].GetData().build;
			KAnim.Build.Symbol symbol = ((build.GetSymbol("algae") != null) ? build.GetSymbol("algae") : build.GetSymbol("object"));
			if (symbol != null)
			{
				this.anim.GetComponent<SymbolOverrideController>().AddSymbolOverride(FishFeeder.FishFeederBot.HASH_FEEDBALL, symbol, 0);
			}
			HashedString hashedString = new HashedString("FishFeeder" + stored_go.GetComponent<KPrefabID>().PrefabTag.Name);
			this.anim.SetBatchGroupOverride(hashedString);
			int num = Grid.CellBelow(Grid.CellBelow(Grid.PosToCell(this.smi.transform.GetPosition())));
			stored_go.transform.SetPosition(Grid.CellToPosCBC(num, Grid.SceneLayer.BuildingUse));
		}

		// Token: 0x040070EE RID: 28910
		private KBatchedAnimController anim;

		// Token: 0x040070EF RID: 28911
		private Storage topStorage;

		// Token: 0x040070F0 RID: 28912
		private Storage botStorage;

		// Token: 0x040070F1 RID: 28913
		private bool refreshingStorage;

		// Token: 0x040070F2 RID: 28914
		private FishFeeder.Instance smi;

		// Token: 0x040070F3 RID: 28915
		private float massPerBall;

		// Token: 0x040070F4 RID: 28916
		private static readonly HashedString HASH_FEEDBALL = "feedball";
	}
}
