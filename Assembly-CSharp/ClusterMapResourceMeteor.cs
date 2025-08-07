using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x02000B34 RID: 2868
public class ClusterMapResourceMeteor : GameStateMachine<ClusterMapResourceMeteor, ClusterMapResourceMeteor.Instance, IStateMachineTarget, ClusterMapResourceMeteor.Def>
{
	// Token: 0x060054B5 RID: 21685 RVA: 0x001EC730 File Offset: 0x001EA930
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.traveling;
		this.traveling.DefaultState(this.traveling.unidentified).EventTransition(GameHashes.ClusterDestinationReached, this.leaving, null);
		this.traveling.unidentified.ParamTransition<bool>(this.IsIdentified, this.traveling.identified, GameStateMachine<ClusterMapResourceMeteor, ClusterMapResourceMeteor.Instance, IStateMachineTarget, ClusterMapResourceMeteor.Def>.IsTrue);
		this.traveling.identified.ParamTransition<bool>(this.IsIdentified, this.traveling.unidentified, GameStateMachine<ClusterMapResourceMeteor, ClusterMapResourceMeteor.Instance, IStateMachineTarget, ClusterMapResourceMeteor.Def>.IsFalse).ToggleStatusItem(Db.Get().MiscStatusItems.ClusterMeteorRemainingTravelTime, null);
		this.leaving.Enter(new StateMachine<ClusterMapResourceMeteor, ClusterMapResourceMeteor.Instance, IStateMachineTarget, ClusterMapResourceMeteor.Def>.State.Callback(ClusterMapResourceMeteor.DestinationReached));
	}

	// Token: 0x060054B6 RID: 21686 RVA: 0x001EC7EF File Offset: 0x001EA9EF
	public static void DestinationReached(ClusterMapResourceMeteor.Instance smi)
	{
		smi.DestinationReached();
		Util.KDestroyGameObject(smi.gameObject);
	}

	// Token: 0x040038EC RID: 14572
	public StateMachine<ClusterMapResourceMeteor, ClusterMapResourceMeteor.Instance, IStateMachineTarget, ClusterMapResourceMeteor.Def>.BoolParameter IsIdentified;

	// Token: 0x040038ED RID: 14573
	public ClusterMapResourceMeteor.TravelingState traveling;

	// Token: 0x040038EE RID: 14574
	public GameStateMachine<ClusterMapResourceMeteor, ClusterMapResourceMeteor.Instance, IStateMachineTarget, ClusterMapResourceMeteor.Def>.State leaving;

	// Token: 0x040038EF RID: 14575
	public GameStateMachine<ClusterMapResourceMeteor, ClusterMapResourceMeteor.Instance, IStateMachineTarget, ClusterMapResourceMeteor.Def>.State left;

	// Token: 0x02001C4C RID: 7244
	public class Def : StateMachine.BaseDef, IGameObjectEffectDescriptor
	{
		// Token: 0x0600AA6F RID: 43631 RVA: 0x003BAB69 File Offset: 0x003B8D69
		public List<Descriptor> GetDescriptors(GameObject go)
		{
			return new List<Descriptor>();
		}

		// Token: 0x040085D6 RID: 34262
		public string name;

		// Token: 0x040085D7 RID: 34263
		public string description;

		// Token: 0x040085D8 RID: 34264
		public string description_Hidden;

		// Token: 0x040085D9 RID: 34265
		public string name_Hidden;

		// Token: 0x040085DA RID: 34266
		public string eventID;

		// Token: 0x040085DB RID: 34267
		private AxialI destination;

		// Token: 0x040085DC RID: 34268
		public float arrivalTime;
	}

	// Token: 0x02001C4D RID: 7245
	public class TravelingState : GameStateMachine<ClusterMapResourceMeteor, ClusterMapResourceMeteor.Instance, IStateMachineTarget, ClusterMapResourceMeteor.Def>.State
	{
		// Token: 0x040085DD RID: 34269
		public GameStateMachine<ClusterMapResourceMeteor, ClusterMapResourceMeteor.Instance, IStateMachineTarget, ClusterMapResourceMeteor.Def>.State unidentified;

		// Token: 0x040085DE RID: 34270
		public GameStateMachine<ClusterMapResourceMeteor, ClusterMapResourceMeteor.Instance, IStateMachineTarget, ClusterMapResourceMeteor.Def>.State identified;
	}

	// Token: 0x02001C4E RID: 7246
	public new class Instance : GameStateMachine<ClusterMapResourceMeteor, ClusterMapResourceMeteor.Instance, IStateMachineTarget, ClusterMapResourceMeteor.Def>.GameInstance
	{
		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x0600AA72 RID: 43634 RVA: 0x003BAB80 File Offset: 0x003B8D80
		public bool HasBeenIdentified
		{
			get
			{
				return base.sm.IsIdentified.Get(this);
			}
		}

		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x0600AA73 RID: 43635 RVA: 0x003BAB93 File Offset: 0x003B8D93
		public float IdentifyingProgress
		{
			get
			{
				return this.identifyingProgress;
			}
		}

		// Token: 0x0600AA74 RID: 43636 RVA: 0x003BAB9B File Offset: 0x003B8D9B
		public AxialI ClusterGridPosition()
		{
			return this.visualizer.Location;
		}

		// Token: 0x0600AA75 RID: 43637 RVA: 0x003BABA8 File Offset: 0x003B8DA8
		public Instance(IStateMachineTarget master, ClusterMapResourceMeteor.Def def)
			: base(master, def)
		{
			this.traveler.getSpeedCB = new Func<float>(this.GetSpeed);
			this.traveler.onTravelCB = new global::System.Action(this.OnTravellerMoved);
		}

		// Token: 0x0600AA76 RID: 43638 RVA: 0x003BABE0 File Offset: 0x003B8DE0
		private void OnTravellerMoved()
		{
			Game.Instance.Trigger(-1975776133, this);
		}

		// Token: 0x0600AA77 RID: 43639 RVA: 0x003BABF2 File Offset: 0x003B8DF2
		protected override void OnCleanUp()
		{
			this.visualizer.Deselect();
			base.OnCleanUp();
		}

		// Token: 0x0600AA78 RID: 43640 RVA: 0x003BAC08 File Offset: 0x003B8E08
		public void Identify()
		{
			if (!this.HasBeenIdentified)
			{
				this.identifyingProgress = 1f;
				base.sm.IsIdentified.Set(true, this, false);
				Game.Instance.Trigger(1427028915, this);
				this.RefreshVisuals(true);
				if (ClusterMapScreen.Instance.IsActive())
				{
					KFMOD.PlayUISound(GlobalAssets.GetSound("ClusterMapMeteor_Reveal", false));
				}
			}
		}

		// Token: 0x0600AA79 RID: 43641 RVA: 0x003BAC70 File Offset: 0x003B8E70
		public void ProgressIdentifiction(float points)
		{
			if (!this.HasBeenIdentified)
			{
				this.identifyingProgress += points;
				this.identifyingProgress = Mathf.Clamp(this.identifyingProgress, 0f, 1f);
				if (this.identifyingProgress == 1f)
				{
					this.Identify();
				}
			}
		}

		// Token: 0x0600AA7A RID: 43642 RVA: 0x003BACC1 File Offset: 0x003B8EC1
		public override void StartSM()
		{
			base.StartSM();
			this.RefreshVisuals(false);
		}

		// Token: 0x0600AA7B RID: 43643 RVA: 0x003BACD0 File Offset: 0x003B8ED0
		public void RefreshVisuals(bool playIdentifyAnimationIfVisible = false)
		{
			if (this.HasBeenIdentified)
			{
				this.selectable.SetName(base.def.name);
				this.descriptor.description = base.def.description;
				this.visualizer.PlayRevealAnimation(playIdentifyAnimationIfVisible);
			}
			else
			{
				this.selectable.SetName(base.def.name_Hidden);
				this.descriptor.description = base.def.description_Hidden;
				this.visualizer.PlayHideAnimation();
			}
			base.Trigger(1980521255, null);
		}

		// Token: 0x0600AA7C RID: 43644 RVA: 0x003BAD64 File Offset: 0x003B8F64
		public void Setup(AxialI destination, float arrivalTime)
		{
			this.Destination = destination;
			this.ArrivalTime = arrivalTime;
			this.destinationSelector.SetDestination(destination);
			this.traveler.RevalidatePath(false);
			int count = this.traveler.CurrentPath.Count;
			float num = arrivalTime - GameUtil.GetCurrentTimeInCycles() * 600f;
			this.Speed = (float)count / num * 600f;
		}

		// Token: 0x0600AA7D RID: 43645 RVA: 0x003BADC6 File Offset: 0x003B8FC6
		public float GetSpeed()
		{
			return this.Speed;
		}

		// Token: 0x0600AA7E RID: 43646 RVA: 0x003BADCE File Offset: 0x003B8FCE
		public void DestinationReached()
		{
			global::System.Action onDestinationReached = this.OnDestinationReached;
			if (onDestinationReached == null)
			{
				return;
			}
			onDestinationReached();
		}

		// Token: 0x040085DF RID: 34271
		[Serialize]
		public AxialI Destination;

		// Token: 0x040085E0 RID: 34272
		[Serialize]
		public float ArrivalTime;

		// Token: 0x040085E1 RID: 34273
		[Serialize]
		private float Speed;

		// Token: 0x040085E2 RID: 34274
		[Serialize]
		private float identifyingProgress;

		// Token: 0x040085E3 RID: 34275
		public global::System.Action OnDestinationReached;

		// Token: 0x040085E4 RID: 34276
		[MyCmpGet]
		private InfoDescription descriptor;

		// Token: 0x040085E5 RID: 34277
		[MyCmpGet]
		private KSelectable selectable;

		// Token: 0x040085E6 RID: 34278
		[MyCmpGet]
		private ClusterMapMeteorShowerVisualizer visualizer;

		// Token: 0x040085E7 RID: 34279
		[MyCmpGet]
		private ClusterTraveler traveler;

		// Token: 0x040085E8 RID: 34280
		[MyCmpGet]
		private ClusterDestinationSelector destinationSelector;
	}
}
