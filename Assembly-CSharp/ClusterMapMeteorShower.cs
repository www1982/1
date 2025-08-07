using System;
using System.Collections.Generic;
using Klei.AI;
using KSerialization;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000B31 RID: 2865
public class ClusterMapMeteorShower : GameStateMachine<ClusterMapMeteorShower, ClusterMapMeteorShower.Instance, IStateMachineTarget, ClusterMapMeteorShower.Def>
{
	// Token: 0x06005495 RID: 21653 RVA: 0x001EBC64 File Offset: 0x001E9E64
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.traveling;
		this.traveling.DefaultState(this.traveling.unidentified).EventTransition(GameHashes.ClusterDestinationReached, this.arrived, null).EventTransition(GameHashes.MissileDamageEncountered, this.destroyed, null);
		this.traveling.unidentified.ParamTransition<bool>(this.IsIdentified, this.traveling.identified, GameStateMachine<ClusterMapMeteorShower, ClusterMapMeteorShower.Instance, IStateMachineTarget, ClusterMapMeteorShower.Def>.IsTrue);
		this.traveling.identified.ParamTransition<bool>(this.IsIdentified, this.traveling.unidentified, GameStateMachine<ClusterMapMeteorShower, ClusterMapMeteorShower.Instance, IStateMachineTarget, ClusterMapMeteorShower.Def>.IsFalse).ToggleStatusItem(Db.Get().MiscStatusItems.ClusterMeteorRemainingTravelTime, null);
		this.arrived.Enter(new StateMachine<ClusterMapMeteorShower, ClusterMapMeteorShower.Instance, IStateMachineTarget, ClusterMapMeteorShower.Def>.State.Callback(ClusterMapMeteorShower.DestinationReached));
		this.destroyed.Enter(new StateMachine<ClusterMapMeteorShower, ClusterMapMeteorShower.Instance, IStateMachineTarget, ClusterMapMeteorShower.Def>.State.Callback(ClusterMapMeteorShower.HandleDestruction));
	}

	// Token: 0x06005496 RID: 21654 RVA: 0x001EBD4C File Offset: 0x001E9F4C
	public static void DestinationReached(ClusterMapMeteorShower.Instance smi)
	{
		smi.DestinationReached();
		Util.KDestroyGameObject(smi.gameObject);
	}

	// Token: 0x06005497 RID: 21655 RVA: 0x001EBD60 File Offset: 0x001E9F60
	public static void HandleDestruction(ClusterMapMeteorShower.Instance smi)
	{
		GameplayEventInstance gameplayEventInstance = GameplayEventManager.Instance.GetGameplayEventInstance(smi.def.eventID, smi.DestinationWorldID);
		if (gameplayEventInstance != null)
		{
			gameplayEventInstance.smi.StopSM("ShotDown");
		}
		Util.KDestroyGameObject(smi.gameObject);
	}

	// Token: 0x040038E1 RID: 14561
	public StateMachine<ClusterMapMeteorShower, ClusterMapMeteorShower.Instance, IStateMachineTarget, ClusterMapMeteorShower.Def>.BoolParameter IsIdentified;

	// Token: 0x040038E2 RID: 14562
	public ClusterMapMeteorShower.TravelingState traveling;

	// Token: 0x040038E3 RID: 14563
	public GameStateMachine<ClusterMapMeteorShower, ClusterMapMeteorShower.Instance, IStateMachineTarget, ClusterMapMeteorShower.Def>.State arrived;

	// Token: 0x040038E4 RID: 14564
	public GameStateMachine<ClusterMapMeteorShower, ClusterMapMeteorShower.Instance, IStateMachineTarget, ClusterMapMeteorShower.Def>.State destroyed;

	// Token: 0x02001C48 RID: 7240
	public class Def : StateMachine.BaseDef, IGameObjectEffectDescriptor
	{
		// Token: 0x0600AA53 RID: 43603 RVA: 0x003BA698 File Offset: 0x003B8898
		public List<Descriptor> GetDescriptors(GameObject go)
		{
			GameplayEvent gameplayEvent = Db.Get().GameplayEvents.Get(this.eventID);
			List<Descriptor> list = new List<Descriptor>();
			ClusterMapMeteorShower.Instance smi = go.GetSMI<ClusterMapMeteorShower.Instance>();
			if (smi != null && smi.sm.IsIdentified.Get(smi) && gameplayEvent is MeteorShowerEvent)
			{
				List<MeteorShowerEvent.BombardmentInfo> meteorsInfo = (gameplayEvent as MeteorShowerEvent).GetMeteorsInfo();
				float num = 0f;
				foreach (MeteorShowerEvent.BombardmentInfo bombardmentInfo in meteorsInfo)
				{
					num += bombardmentInfo.weight;
				}
				foreach (MeteorShowerEvent.BombardmentInfo bombardmentInfo2 in meteorsInfo)
				{
					GameObject prefab = Assets.GetPrefab(bombardmentInfo2.prefab);
					string formattedPercent = GameUtil.GetFormattedPercent((float)Mathf.RoundToInt(bombardmentInfo2.weight / num * 100f), GameUtil.TimeSlice.None);
					string text = prefab.GetProperName() + " " + formattedPercent;
					Descriptor descriptor = new Descriptor(text, UI.GAMEOBJECTEFFECTS.TOOLTIPS.METEOR_SHOWER_SINGLE_METEOR_PERCENTAGE_TOOLTIP, Descriptor.DescriptorType.Effect, false);
					list.Add(descriptor);
				}
			}
			return list;
		}

		// Token: 0x040085C1 RID: 34241
		public string name;

		// Token: 0x040085C2 RID: 34242
		public string description;

		// Token: 0x040085C3 RID: 34243
		public string description_Hidden;

		// Token: 0x040085C4 RID: 34244
		public string name_Hidden;

		// Token: 0x040085C5 RID: 34245
		public string eventID;

		// Token: 0x040085C6 RID: 34246
		public int destinationWorldID;

		// Token: 0x040085C7 RID: 34247
		public float arrivalTime;
	}

	// Token: 0x02001C49 RID: 7241
	public class TravelingState : GameStateMachine<ClusterMapMeteorShower, ClusterMapMeteorShower.Instance, IStateMachineTarget, ClusterMapMeteorShower.Def>.State
	{
		// Token: 0x040085C8 RID: 34248
		public GameStateMachine<ClusterMapMeteorShower, ClusterMapMeteorShower.Instance, IStateMachineTarget, ClusterMapMeteorShower.Def>.State unidentified;

		// Token: 0x040085C9 RID: 34249
		public GameStateMachine<ClusterMapMeteorShower, ClusterMapMeteorShower.Instance, IStateMachineTarget, ClusterMapMeteorShower.Def>.State identified;
	}

	// Token: 0x02001C4A RID: 7242
	public new class Instance : GameStateMachine<ClusterMapMeteorShower, ClusterMapMeteorShower.Instance, IStateMachineTarget, ClusterMapMeteorShower.Def>.GameInstance, ISidescreenButtonControl
	{
		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x0600AA56 RID: 43606 RVA: 0x003BA7F4 File Offset: 0x003B89F4
		public WorldContainer World_Destination
		{
			get
			{
				return ClusterManager.Instance.GetWorld(this.DestinationWorldID);
			}
		}

		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x0600AA57 RID: 43607 RVA: 0x003BA806 File Offset: 0x003B8A06
		public string SidescreenButtonText
		{
			get
			{
				if (!base.smi.sm.IsIdentified.Get(base.smi))
				{
					return "Identify";
				}
				return "Dev Hide";
			}
		}

		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x0600AA58 RID: 43608 RVA: 0x003BA830 File Offset: 0x003B8A30
		public string SidescreenButtonTooltip
		{
			get
			{
				if (!base.smi.sm.IsIdentified.Get(base.smi))
				{
					return "Identifies the meteor shower";
				}
				return "Dev unidentify back";
			}
		}

		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x0600AA59 RID: 43609 RVA: 0x003BA85A File Offset: 0x003B8A5A
		public bool HasBeenIdentified
		{
			get
			{
				return base.sm.IsIdentified.Get(this);
			}
		}

		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x0600AA5A RID: 43610 RVA: 0x003BA86D File Offset: 0x003B8A6D
		public float IdentifyingProgress
		{
			get
			{
				return this.identifyingProgress;
			}
		}

		// Token: 0x0600AA5B RID: 43611 RVA: 0x003BA875 File Offset: 0x003B8A75
		public AxialI ClusterGridPosition()
		{
			return this.visualizer.Location;
		}

		// Token: 0x0600AA5C RID: 43612 RVA: 0x003BA882 File Offset: 0x003B8A82
		public Instance(IStateMachineTarget master, ClusterMapMeteorShower.Def def)
			: base(master, def)
		{
			this.traveler.getSpeedCB = new Func<float>(this.GetSpeed);
			this.traveler.onTravelCB = new global::System.Action(this.OnTravellerMoved);
		}

		// Token: 0x0600AA5D RID: 43613 RVA: 0x003BA8C1 File Offset: 0x003B8AC1
		private void OnTravellerMoved()
		{
			Game.Instance.Trigger(-1975776133, this);
		}

		// Token: 0x0600AA5E RID: 43614 RVA: 0x003BA8D3 File Offset: 0x003B8AD3
		protected override void OnCleanUp()
		{
			this.visualizer.Deselect();
			Components.LongRangeMissileTargetables.Remove(base.gameObject.GetComponent<ClusterGridEntity>());
			base.OnCleanUp();
		}

		// Token: 0x0600AA5F RID: 43615 RVA: 0x003BA8FC File Offset: 0x003B8AFC
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

		// Token: 0x0600AA60 RID: 43616 RVA: 0x003BA964 File Offset: 0x003B8B64
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

		// Token: 0x0600AA61 RID: 43617 RVA: 0x003BA9B8 File Offset: 0x003B8BB8
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

		// Token: 0x0600AA62 RID: 43618 RVA: 0x003BAA0C File Offset: 0x003B8C0C
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

		// Token: 0x0600AA63 RID: 43619 RVA: 0x003BAAA0 File Offset: 0x003B8CA0
		public void Setup(int destinationWorldID, float arrivalTime)
		{
			this.DestinationWorldID = destinationWorldID;
			this.ArrivalTime = arrivalTime;
			AxialI location = this.World_Destination.GetComponent<ClusterGridEntity>().Location;
			this.destinationSelector.SetDestination(location);
			this.traveler.RevalidatePath(false);
			int count = this.traveler.CurrentPath.Count;
			float num = arrivalTime - GameUtil.GetCurrentTimeInCycles() * 600f;
			this.Speed = (float)count / num * 600f;
		}

		// Token: 0x0600AA64 RID: 43620 RVA: 0x003BAB13 File Offset: 0x003B8D13
		public float GetSpeed()
		{
			return this.Speed;
		}

		// Token: 0x0600AA65 RID: 43621 RVA: 0x003BAB1B File Offset: 0x003B8D1B
		public void DestinationReached()
		{
			global::System.Action onDestinationReached = this.OnDestinationReached;
			if (onDestinationReached == null)
			{
				return;
			}
			onDestinationReached();
		}

		// Token: 0x0600AA66 RID: 43622 RVA: 0x003BAB2D File Offset: 0x003B8D2D
		public void SetButtonTextOverride(ButtonMenuTextOverride textOverride)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600AA67 RID: 43623 RVA: 0x003BAB34 File Offset: 0x003B8D34
		public bool SidescreenEnabled()
		{
			return false;
		}

		// Token: 0x0600AA68 RID: 43624 RVA: 0x003BAB37 File Offset: 0x003B8D37
		public bool SidescreenButtonInteractable()
		{
			return true;
		}

		// Token: 0x0600AA69 RID: 43625 RVA: 0x003BAB3A File Offset: 0x003B8D3A
		public void OnSidescreenButtonPressed()
		{
			this.Identify();
		}

		// Token: 0x0600AA6A RID: 43626 RVA: 0x003BAB42 File Offset: 0x003B8D42
		public int HorizontalGroupID()
		{
			return -1;
		}

		// Token: 0x0600AA6B RID: 43627 RVA: 0x003BAB45 File Offset: 0x003B8D45
		public int ButtonSideScreenSortOrder()
		{
			return SORTORDER.KEEPSAKES;
		}

		// Token: 0x040085CA RID: 34250
		[Serialize]
		public int DestinationWorldID = -1;

		// Token: 0x040085CB RID: 34251
		[Serialize]
		public float ArrivalTime;

		// Token: 0x040085CC RID: 34252
		[Serialize]
		private float Speed;

		// Token: 0x040085CD RID: 34253
		[Serialize]
		private float identifyingProgress;

		// Token: 0x040085CE RID: 34254
		public global::System.Action OnDestinationReached;

		// Token: 0x040085CF RID: 34255
		[MyCmpGet]
		private InfoDescription descriptor;

		// Token: 0x040085D0 RID: 34256
		[MyCmpGet]
		private KSelectable selectable;

		// Token: 0x040085D1 RID: 34257
		[MyCmpGet]
		private ClusterMapMeteorShowerVisualizer visualizer;

		// Token: 0x040085D2 RID: 34258
		[MyCmpGet]
		private ClusterTraveler traveler;

		// Token: 0x040085D3 RID: 34259
		[MyCmpGet]
		private ClusterDestinationSelector destinationSelector;
	}
}
