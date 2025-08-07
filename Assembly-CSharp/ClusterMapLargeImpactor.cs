using System;
using KSerialization;

// Token: 0x02000B30 RID: 2864
public class ClusterMapLargeImpactor : GameStateMachine<ClusterMapLargeImpactor, ClusterMapLargeImpactor.Instance, IStateMachineTarget, ClusterMapLargeImpactor.Def>
{
	// Token: 0x06005493 RID: 21651 RVA: 0x001EBBA8 File Offset: 0x001E9DA8
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.traveling;
		this.traveling.DefaultState(this.traveling.unidentified).EventTransition(GameHashes.ClusterDestinationReached, this.arrived, null);
		this.traveling.unidentified.ParamTransition<bool>(this.IsIdentified, this.traveling.identified, GameStateMachine<ClusterMapLargeImpactor, ClusterMapLargeImpactor.Instance, IStateMachineTarget, ClusterMapLargeImpactor.Def>.IsTrue);
		this.traveling.identified.ParamTransition<bool>(this.IsIdentified, this.traveling.unidentified, GameStateMachine<ClusterMapLargeImpactor, ClusterMapLargeImpactor.Instance, IStateMachineTarget, ClusterMapLargeImpactor.Def>.IsFalse).ToggleStatusItem(Db.Get().MiscStatusItems.ClusterMeteorRemainingTravelTime, null);
		this.arrived.DoNothing();
	}

	// Token: 0x040038DE RID: 14558
	public StateMachine<ClusterMapLargeImpactor, ClusterMapLargeImpactor.Instance, IStateMachineTarget, ClusterMapLargeImpactor.Def>.BoolParameter IsIdentified;

	// Token: 0x040038DF RID: 14559
	public ClusterMapLargeImpactor.TravelingState traveling;

	// Token: 0x040038E0 RID: 14560
	public GameStateMachine<ClusterMapLargeImpactor, ClusterMapLargeImpactor.Instance, IStateMachineTarget, ClusterMapLargeImpactor.Def>.State arrived;

	// Token: 0x02001C45 RID: 7237
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x040085B2 RID: 34226
		public string name;

		// Token: 0x040085B3 RID: 34227
		public string description;

		// Token: 0x040085B4 RID: 34228
		public string eventID;

		// Token: 0x040085B5 RID: 34229
		public int destinationWorldID;

		// Token: 0x040085B6 RID: 34230
		public float arrivalTime;
	}

	// Token: 0x02001C46 RID: 7238
	public class TravelingState : GameStateMachine<ClusterMapLargeImpactor, ClusterMapLargeImpactor.Instance, IStateMachineTarget, ClusterMapLargeImpactor.Def>.State
	{
		// Token: 0x040085B7 RID: 34231
		public GameStateMachine<ClusterMapLargeImpactor, ClusterMapLargeImpactor.Instance, IStateMachineTarget, ClusterMapLargeImpactor.Def>.State unidentified;

		// Token: 0x040085B8 RID: 34232
		public GameStateMachine<ClusterMapLargeImpactor, ClusterMapLargeImpactor.Instance, IStateMachineTarget, ClusterMapLargeImpactor.Def>.State identified;
	}

	// Token: 0x02001C47 RID: 7239
	public new class Instance : GameStateMachine<ClusterMapLargeImpactor, ClusterMapLargeImpactor.Instance, IStateMachineTarget, ClusterMapLargeImpactor.Def>.GameInstance
	{
		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x0600AA4A RID: 43594 RVA: 0x003BA47E File Offset: 0x003B867E
		public WorldContainer World_Destination
		{
			get
			{
				return ClusterManager.Instance.GetWorld(this.DestinationWorldID);
			}
		}

		// Token: 0x0600AA4B RID: 43595 RVA: 0x003BA490 File Offset: 0x003B8690
		public AxialI ClusterGridPosition()
		{
			return this.visualizer.Location;
		}

		// Token: 0x0600AA4C RID: 43596 RVA: 0x003BA49D File Offset: 0x003B869D
		public Instance(IStateMachineTarget master, ClusterMapLargeImpactor.Def def)
			: base(master, def)
		{
			this.traveler.getSpeedCB = new Func<float>(this.GetSpeed);
			this.traveler.onTravelCB = new global::System.Action(this.OnTravellerMoved);
		}

		// Token: 0x0600AA4D RID: 43597 RVA: 0x003BA4DC File Offset: 0x003B86DC
		private void OnTravellerMoved()
		{
			Game.Instance.Trigger(-1975776133, this);
		}

		// Token: 0x0600AA4E RID: 43598 RVA: 0x003BA4EE File Offset: 0x003B86EE
		protected override void OnCleanUp()
		{
			Components.LongRangeMissileTargetables.Remove(base.gameObject.GetComponent<ClusterGridEntity>());
			this.visualizer.Deselect();
			base.OnCleanUp();
		}

		// Token: 0x0600AA4F RID: 43599 RVA: 0x003BA518 File Offset: 0x003B8718
		public override void StartSM()
		{
			base.StartSM();
			if (this.DestinationWorldID < 0)
			{
				this.Setup(base.def.destinationWorldID, base.def.arrivalTime);
			}
			Components.LongRangeMissileTargetables.Add(base.gameObject.GetComponent<ClusterGridEntity>());
			this.RefreshVisuals(false);
		}

		// Token: 0x0600AA50 RID: 43600 RVA: 0x003BA56C File Offset: 0x003B876C
		public void RefreshVisuals(bool playIdentifyAnimationIfVisible = false)
		{
			this.selectable.SetName(base.def.name);
			this.descriptor.description = base.def.description;
			this.visualizer.PlayRevealAnimation(playIdentifyAnimationIfVisible);
			base.Trigger(1980521255, null);
		}

		// Token: 0x0600AA51 RID: 43601 RVA: 0x003BA5C0 File Offset: 0x003B87C0
		public void Setup(int destinationWorldID, float arrivalTime)
		{
			this.DestinationWorldID = destinationWorldID;
			this.ArrivalTime = arrivalTime;
			AxialI location = this.World_Destination.GetComponent<ClusterGridEntity>().Location;
			this.destinationSelector.SetDestination(location);
			this.traveler.RevalidatePath(false);
			ClusterFogOfWarManager.Instance smi = SaveGame.Instance.GetSMI<ClusterFogOfWarManager.Instance>();
			foreach (AxialI axialI in this.traveler.CurrentPath)
			{
				smi.RevealLocation(axialI, 0, 0);
			}
			int count = this.traveler.CurrentPath.Count;
			float num = arrivalTime - GameUtil.GetCurrentTimeInCycles() * 600f;
			this.Speed = (float)count / num * 600f;
		}

		// Token: 0x0600AA52 RID: 43602 RVA: 0x003BA690 File Offset: 0x003B8890
		public float GetSpeed()
		{
			return this.Speed;
		}

		// Token: 0x040085B9 RID: 34233
		[Serialize]
		public int DestinationWorldID = -1;

		// Token: 0x040085BA RID: 34234
		[Serialize]
		public float ArrivalTime;

		// Token: 0x040085BB RID: 34235
		[Serialize]
		private float Speed;

		// Token: 0x040085BC RID: 34236
		[MyCmpGet]
		private InfoDescription descriptor;

		// Token: 0x040085BD RID: 34237
		[MyCmpGet]
		private KSelectable selectable;

		// Token: 0x040085BE RID: 34238
		[MyCmpGet]
		private ClusterMapMeteorShowerVisualizer visualizer;

		// Token: 0x040085BF RID: 34239
		[MyCmpGet]
		private ClusterTraveler traveler;

		// Token: 0x040085C0 RID: 34240
		[MyCmpGet]
		private ClusterDestinationSelector destinationSelector;
	}
}
