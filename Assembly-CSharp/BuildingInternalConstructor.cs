using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020006CE RID: 1742
public class BuildingInternalConstructor : GameStateMachine<BuildingInternalConstructor, BuildingInternalConstructor.Instance, IStateMachineTarget, BuildingInternalConstructor.Def>
{
	// Token: 0x06002B02 RID: 11010 RVA: 0x000F88FC File Offset: 0x000F6AFC
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.inoperational;
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		this.inoperational.EventTransition(GameHashes.OperationalChanged, this.operational, (BuildingInternalConstructor.Instance smi) => smi.GetComponent<Operational>().IsOperational).Enter(delegate(BuildingInternalConstructor.Instance smi)
		{
			smi.ShowConstructionSymbol(false);
		});
		this.operational.DefaultState(this.operational.constructionRequired).EventTransition(GameHashes.OperationalChanged, this.inoperational, (BuildingInternalConstructor.Instance smi) => !smi.GetComponent<Operational>().IsOperational);
		this.operational.constructionRequired.EventTransition(GameHashes.OnStorageChange, this.operational.constructionHappening, (BuildingInternalConstructor.Instance smi) => smi.GetMassForConstruction() != null).EventTransition(GameHashes.OnStorageChange, this.operational.constructionSatisfied, (BuildingInternalConstructor.Instance smi) => smi.HasOutputInStorage()).ToggleFetch((BuildingInternalConstructor.Instance smi) => smi.CreateFetchList(), this.operational.constructionHappening)
			.ParamTransition<bool>(this.constructionRequested, this.operational.constructionSatisfied, GameStateMachine<BuildingInternalConstructor, BuildingInternalConstructor.Instance, IStateMachineTarget, BuildingInternalConstructor.Def>.IsFalse)
			.Enter(delegate(BuildingInternalConstructor.Instance smi)
			{
				smi.ShowConstructionSymbol(true);
			})
			.Exit(delegate(BuildingInternalConstructor.Instance smi)
			{
				smi.ShowConstructionSymbol(false);
			});
		this.operational.constructionHappening.EventTransition(GameHashes.OnStorageChange, this.operational.constructionSatisfied, (BuildingInternalConstructor.Instance smi) => smi.HasOutputInStorage()).EventTransition(GameHashes.OnStorageChange, this.operational.constructionRequired, (BuildingInternalConstructor.Instance smi) => smi.GetMassForConstruction() == null).ToggleChore((BuildingInternalConstructor.Instance smi) => smi.CreateWorkChore(), this.operational.constructionHappening, this.operational.constructionHappening)
			.ParamTransition<bool>(this.constructionRequested, this.operational.constructionSatisfied, GameStateMachine<BuildingInternalConstructor, BuildingInternalConstructor.Instance, IStateMachineTarget, BuildingInternalConstructor.Def>.IsFalse)
			.Enter(delegate(BuildingInternalConstructor.Instance smi)
			{
				smi.ShowConstructionSymbol(true);
			})
			.Exit(delegate(BuildingInternalConstructor.Instance smi)
			{
				smi.ShowConstructionSymbol(false);
			});
		this.operational.constructionSatisfied.EventTransition(GameHashes.OnStorageChange, this.operational.constructionRequired, (BuildingInternalConstructor.Instance smi) => !smi.HasOutputInStorage() && this.constructionRequested.Get(smi)).ParamTransition<bool>(this.constructionRequested, this.operational.constructionRequired, (BuildingInternalConstructor.Instance smi, bool p) => p && !smi.HasOutputInStorage());
	}

	// Token: 0x0400195A RID: 6490
	public GameStateMachine<BuildingInternalConstructor, BuildingInternalConstructor.Instance, IStateMachineTarget, BuildingInternalConstructor.Def>.State inoperational;

	// Token: 0x0400195B RID: 6491
	public BuildingInternalConstructor.OperationalStates operational;

	// Token: 0x0400195C RID: 6492
	public StateMachine<BuildingInternalConstructor, BuildingInternalConstructor.Instance, IStateMachineTarget, BuildingInternalConstructor.Def>.BoolParameter constructionRequested = new StateMachine<BuildingInternalConstructor, BuildingInternalConstructor.Instance, IStateMachineTarget, BuildingInternalConstructor.Def>.BoolParameter(true);

	// Token: 0x02001553 RID: 5459
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04006F4D RID: 28493
		public DefComponent<Storage> storage;

		// Token: 0x04006F4E RID: 28494
		public float constructionMass;

		// Token: 0x04006F4F RID: 28495
		public List<string> outputIDs;

		// Token: 0x04006F50 RID: 28496
		public bool spawnIntoStorage;

		// Token: 0x04006F51 RID: 28497
		public string constructionSymbol;
	}

	// Token: 0x02001554 RID: 5460
	public class OperationalStates : GameStateMachine<BuildingInternalConstructor, BuildingInternalConstructor.Instance, IStateMachineTarget, BuildingInternalConstructor.Def>.State
	{
		// Token: 0x04006F52 RID: 28498
		public GameStateMachine<BuildingInternalConstructor, BuildingInternalConstructor.Instance, IStateMachineTarget, BuildingInternalConstructor.Def>.State constructionRequired;

		// Token: 0x04006F53 RID: 28499
		public GameStateMachine<BuildingInternalConstructor, BuildingInternalConstructor.Instance, IStateMachineTarget, BuildingInternalConstructor.Def>.State constructionHappening;

		// Token: 0x04006F54 RID: 28500
		public GameStateMachine<BuildingInternalConstructor, BuildingInternalConstructor.Instance, IStateMachineTarget, BuildingInternalConstructor.Def>.State constructionSatisfied;
	}

	// Token: 0x02001555 RID: 5461
	public new class Instance : GameStateMachine<BuildingInternalConstructor, BuildingInternalConstructor.Instance, IStateMachineTarget, BuildingInternalConstructor.Def>.GameInstance, ISidescreenButtonControl
	{
		// Token: 0x060090C4 RID: 37060 RVA: 0x00361770 File Offset: 0x0035F970
		public Instance(IStateMachineTarget master, BuildingInternalConstructor.Def def)
			: base(master, def)
		{
			this.storage = def.storage.Get(this);
			base.GetComponent<RocketModule>().AddModuleCondition(ProcessCondition.ProcessConditionType.RocketPrep, new InternalConstructionCompleteCondition(this));
		}

		// Token: 0x060090C5 RID: 37061 RVA: 0x003617A0 File Offset: 0x0035F9A0
		protected override void OnCleanUp()
		{
			Element element = null;
			float num = 0f;
			float num2 = 0f;
			byte maxValue = byte.MaxValue;
			int num3 = 0;
			foreach (string text in base.def.outputIDs)
			{
				GameObject gameObject = this.storage.FindFirst(text);
				if (gameObject != null)
				{
					PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
					global::Debug.Assert(element == null || element == component.Element);
					element = component.Element;
					num2 = GameUtil.GetFinalTemperature(num, num2, component.Mass, component.Temperature);
					num += component.Mass;
					gameObject.DeleteObject();
				}
			}
			if (element != null)
			{
				element.substance.SpawnResource(base.transform.GetPosition(), num, num2, maxValue, num3, false, false, false);
			}
			base.OnCleanUp();
		}

		// Token: 0x060090C6 RID: 37062 RVA: 0x003618A0 File Offset: 0x0035FAA0
		public FetchList2 CreateFetchList()
		{
			FetchList2 fetchList = new FetchList2(this.storage, Db.Get().ChoreTypes.Fetch);
			PrimaryElement component = base.GetComponent<PrimaryElement>();
			fetchList.Add(component.Element.tag, null, base.def.constructionMass, Operational.State.None);
			return fetchList;
		}

		// Token: 0x060090C7 RID: 37063 RVA: 0x003618EC File Offset: 0x0035FAEC
		public PrimaryElement GetMassForConstruction()
		{
			PrimaryElement component = base.GetComponent<PrimaryElement>();
			return this.storage.FindFirstWithMass(component.Element.tag, base.def.constructionMass);
		}

		// Token: 0x060090C8 RID: 37064 RVA: 0x00361921 File Offset: 0x0035FB21
		public bool HasOutputInStorage()
		{
			return this.storage.FindFirst(base.def.outputIDs[0].ToTag());
		}

		// Token: 0x060090C9 RID: 37065 RVA: 0x00361949 File Offset: 0x0035FB49
		public bool IsRequestingConstruction()
		{
			base.sm.constructionRequested.Get(this);
			return base.smi.sm.constructionRequested.Get(base.smi);
		}

		// Token: 0x060090CA RID: 37066 RVA: 0x00361978 File Offset: 0x0035FB78
		public void ConstructionComplete(bool force = false)
		{
			SimHashes simHashes;
			if (!force)
			{
				PrimaryElement massForConstruction = this.GetMassForConstruction();
				simHashes = massForConstruction.ElementID;
				float mass = massForConstruction.Mass;
				float num = massForConstruction.Temperature * massForConstruction.Mass;
				massForConstruction.Mass -= base.def.constructionMass;
				Mathf.Clamp(num / mass, 0f, 318.15f);
			}
			else
			{
				simHashes = SimHashes.Cuprite;
				float temperature = base.GetComponent<PrimaryElement>().Temperature;
			}
			foreach (string text in base.def.outputIDs)
			{
				GameObject gameObject = GameUtil.KInstantiate(Assets.GetPrefab(text), base.transform.GetPosition(), Grid.SceneLayer.Ore, null, 0);
				gameObject.GetComponent<PrimaryElement>().SetElement(simHashes, false);
				gameObject.SetActive(true);
				if (base.def.spawnIntoStorage)
				{
					this.storage.Store(gameObject, false, false, true, false);
				}
			}
		}

		// Token: 0x060090CB RID: 37067 RVA: 0x00361A80 File Offset: 0x0035FC80
		public WorkChore<BuildingInternalConstructorWorkable> CreateWorkChore()
		{
			return new WorkChore<BuildingInternalConstructorWorkable>(Db.Get().ChoreTypes.Build, base.master, null, true, null, null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
		}

		// Token: 0x060090CC RID: 37068 RVA: 0x00361AB8 File Offset: 0x0035FCB8
		public void ShowConstructionSymbol(bool show)
		{
			KBatchedAnimController component = base.master.GetComponent<KBatchedAnimController>();
			if (component != null)
			{
				component.SetSymbolVisiblity(base.def.constructionSymbol, show);
			}
		}

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x060090CD RID: 37069 RVA: 0x00361AF4 File Offset: 0x0035FCF4
		public string SidescreenButtonText
		{
			get
			{
				if (!base.smi.sm.constructionRequested.Get(base.smi))
				{
					return string.Format(UI.UISIDESCREENS.BUTTONMENUSIDESCREEN.ALLOW_INTERNAL_CONSTRUCTOR.text, Assets.GetPrefab(base.def.outputIDs[0]).GetProperName());
				}
				return string.Format(UI.UISIDESCREENS.BUTTONMENUSIDESCREEN.DISALLOW_INTERNAL_CONSTRUCTOR.text, Assets.GetPrefab(base.def.outputIDs[0]).GetProperName());
			}
		}

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x060090CE RID: 37070 RVA: 0x00361B80 File Offset: 0x0035FD80
		public string SidescreenButtonTooltip
		{
			get
			{
				if (!base.smi.sm.constructionRequested.Get(base.smi))
				{
					return string.Format(UI.UISIDESCREENS.BUTTONMENUSIDESCREEN.ALLOW_INTERNAL_CONSTRUCTOR_TOOLTIP.text, Assets.GetPrefab(base.def.outputIDs[0]).GetProperName());
				}
				return string.Format(UI.UISIDESCREENS.BUTTONMENUSIDESCREEN.DISALLOW_INTERNAL_CONSTRUCTOR_TOOLTIP.text, Assets.GetPrefab(base.def.outputIDs[0]).GetProperName());
			}
		}

		// Token: 0x060090CF RID: 37071 RVA: 0x00361C0C File Offset: 0x0035FE0C
		public void OnSidescreenButtonPressed()
		{
			base.smi.sm.constructionRequested.Set(!base.smi.sm.constructionRequested.Get(base.smi), base.smi, false);
			if (DebugHandler.InstantBuildMode && base.smi.sm.constructionRequested.Get(base.smi) && !this.HasOutputInStorage())
			{
				this.ConstructionComplete(true);
			}
		}

		// Token: 0x060090D0 RID: 37072 RVA: 0x00361C87 File Offset: 0x0035FE87
		public void SetButtonTextOverride(ButtonMenuTextOverride text)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060090D1 RID: 37073 RVA: 0x00361C8E File Offset: 0x0035FE8E
		public bool SidescreenEnabled()
		{
			return true;
		}

		// Token: 0x060090D2 RID: 37074 RVA: 0x00361C91 File Offset: 0x0035FE91
		public bool SidescreenButtonInteractable()
		{
			return true;
		}

		// Token: 0x060090D3 RID: 37075 RVA: 0x00361C94 File Offset: 0x0035FE94
		public int ButtonSideScreenSortOrder()
		{
			return 20;
		}

		// Token: 0x060090D4 RID: 37076 RVA: 0x00361C98 File Offset: 0x0035FE98
		public int HorizontalGroupID()
		{
			return -1;
		}

		// Token: 0x04006F55 RID: 28501
		private Storage storage;

		// Token: 0x04006F56 RID: 28502
		[Serialize]
		private float constructionElapsed;

		// Token: 0x04006F57 RID: 28503
		private ProgressBar progressBar;
	}
}
