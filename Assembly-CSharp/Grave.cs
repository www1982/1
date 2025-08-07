using System;
using System.Collections.Generic;
using KSerialization;
using TUNING;
using UnityEngine;

// Token: 0x02000742 RID: 1858
public class Grave : StateMachineComponent<Grave.StatesInstance>
{
	// Token: 0x06002F17 RID: 12055 RVA: 0x0010DFEA File Offset: 0x0010C1EA
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<Grave>(-1697596308, Grave.OnStorageChangedDelegate);
		this.epitaphIdx = global::UnityEngine.Random.Range(0, int.MaxValue);
	}

	// Token: 0x06002F18 RID: 12056 RVA: 0x0010E014 File Offset: 0x0010C214
	protected override void OnSpawn()
	{
		base.GetComponent<Storage>().SetOffsets(Grave.DELIVERY_OFFSETS);
		Storage component = base.GetComponent<Storage>();
		Storage storage = component;
		storage.OnWorkableEventCB = (Action<Workable, Workable.WorkableEvent>)Delegate.Combine(storage.OnWorkableEventCB, new Action<Workable, Workable.WorkableEvent>(this.OnWorkEvent));
		KAnimFile anim = Assets.GetAnim("anim_bury_dupe_kanim");
		int num = 0;
		KAnim.Anim anim2;
		for (;;)
		{
			anim2 = anim.GetData().GetAnim(num);
			if (anim2 == null)
			{
				goto IL_008F;
			}
			if (anim2.name == "working_pre")
			{
				break;
			}
			num++;
		}
		float num2 = (float)(anim2.numFrames - 3) / anim2.frameRate;
		component.SetWorkTime(num2);
		IL_008F:
		base.OnSpawn();
		base.smi.StartSM();
		Components.Graves.Add(this);
	}

	// Token: 0x06002F19 RID: 12057 RVA: 0x0010E0CC File Offset: 0x0010C2CC
	protected override void OnCleanUp()
	{
		Components.Graves.Remove(this);
		base.OnCleanUp();
	}

	// Token: 0x06002F1A RID: 12058 RVA: 0x0010E0E0 File Offset: 0x0010C2E0
	private void OnStorageChanged(object data)
	{
		GameObject gameObject = (GameObject)data;
		if (gameObject != null)
		{
			this.graveName = gameObject.name;
			MinionIdentity component = gameObject.GetComponent<MinionIdentity>();
			if (component != null)
			{
				Personality personality = Db.Get().Personalities.TryGet(component.personalityResourceId);
				KAnimFile anim = Assets.GetAnim("gravestone_kanim");
				if (personality != null && anim.GetData().GetAnim(personality.graveStone) != null)
				{
					this.graveAnim = personality.graveStone;
				}
			}
			Util.KDestroyGameObject(gameObject);
		}
	}

	// Token: 0x06002F1B RID: 12059 RVA: 0x0010E167 File Offset: 0x0010C367
	private void OnWorkEvent(Workable workable, Workable.WorkableEvent evt)
	{
	}

	// Token: 0x04001BD7 RID: 7127
	[Serialize]
	public string graveName;

	// Token: 0x04001BD8 RID: 7128
	[Serialize]
	public string graveAnim = "closed";

	// Token: 0x04001BD9 RID: 7129
	[Serialize]
	public int epitaphIdx;

	// Token: 0x04001BDA RID: 7130
	[Serialize]
	public float burialTime = -1f;

	// Token: 0x04001BDB RID: 7131
	private static readonly CellOffset[] DELIVERY_OFFSETS = new CellOffset[1];

	// Token: 0x04001BDC RID: 7132
	private static readonly EventSystem.IntraObjectHandler<Grave> OnStorageChangedDelegate = new EventSystem.IntraObjectHandler<Grave>(delegate(Grave component, object data)
	{
		component.OnStorageChanged(data);
	});

	// Token: 0x02001604 RID: 5636
	public class StatesInstance : GameStateMachine<Grave.States, Grave.StatesInstance, Grave, object>.GameInstance
	{
		// Token: 0x0600938C RID: 37772 RVA: 0x0036BF41 File Offset: 0x0036A141
		public StatesInstance(Grave master)
			: base(master)
		{
		}

		// Token: 0x0600938D RID: 37773 RVA: 0x0036BF4C File Offset: 0x0036A14C
		public void CreateFetchTask()
		{
			this.chore = new FetchChore(Db.Get().ChoreTypes.FetchCritical, base.GetComponent<Storage>(), DUPLICANTSTATS.STANDARD.BaseStats.DEFAULT_MASS, new HashSet<Tag> { GameTags.BaseMinion }, FetchChore.MatchCriteria.MatchTags, GameTags.Corpse, null, null, true, null, null, null, Operational.State.Operational, 0);
			this.chore.allowMultifetch = false;
		}

		// Token: 0x0600938E RID: 37774 RVA: 0x0036BFB3 File Offset: 0x0036A1B3
		public void CancelFetchTask()
		{
			this.chore.Cancel("Exit State");
			this.chore = null;
		}

		// Token: 0x040071A4 RID: 29092
		private FetchChore chore;
	}

	// Token: 0x02001605 RID: 5637
	public class States : GameStateMachine<Grave.States, Grave.StatesInstance, Grave>
	{
		// Token: 0x0600938F RID: 37775 RVA: 0x0036BFCC File Offset: 0x0036A1CC
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.empty;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.empty.PlayAnim("open").Enter("CreateFetchTask", delegate(Grave.StatesInstance smi)
			{
				smi.CreateFetchTask();
			}).Exit("CancelFetchTask", delegate(Grave.StatesInstance smi)
			{
				smi.CancelFetchTask();
			})
				.ToggleMainStatusItem(Db.Get().BuildingStatusItems.GraveEmpty, null)
				.EventTransition(GameHashes.OnStorageChange, this.full, null);
			this.full.PlayAnim((Grave.StatesInstance smi) => smi.master.graveAnim, KAnim.PlayMode.Once).ToggleMainStatusItem(Db.Get().BuildingStatusItems.Grave, null).Enter(delegate(Grave.StatesInstance smi)
			{
				if (smi.master.burialTime < 0f)
				{
					smi.master.burialTime = GameClock.Instance.GetTime();
				}
			});
		}

		// Token: 0x040071A5 RID: 29093
		public GameStateMachine<Grave.States, Grave.StatesInstance, Grave, object>.State empty;

		// Token: 0x040071A6 RID: 29094
		public GameStateMachine<Grave.States, Grave.StatesInstance, Grave, object>.State full;
	}
}
