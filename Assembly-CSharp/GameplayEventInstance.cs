using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x020004BC RID: 1212
[SerializationConfig(MemberSerialization.OptIn)]
public class GameplayEventInstance : ISaveLoadable
{
	// Token: 0x1700009F RID: 159
	// (get) Token: 0x060019E8 RID: 6632 RVA: 0x0008E5B2 File Offset: 0x0008C7B2
	// (set) Token: 0x060019E9 RID: 6633 RVA: 0x0008E5BA File Offset: 0x0008C7BA
	public StateMachine.Instance smi { get; private set; }

	// Token: 0x170000A0 RID: 160
	// (get) Token: 0x060019EA RID: 6634 RVA: 0x0008E5C3 File Offset: 0x0008C7C3
	// (set) Token: 0x060019EB RID: 6635 RVA: 0x0008E5CB File Offset: 0x0008C7CB
	public bool seenNotification
	{
		get
		{
			return this._seenNotification;
		}
		set
		{
			this._seenNotification = value;
			this.monitorCallbackObjects.ForEach(delegate(GameObject x)
			{
				x.Trigger(-1122598290, this);
			});
		}
	}

	// Token: 0x170000A1 RID: 161
	// (get) Token: 0x060019EC RID: 6636 RVA: 0x0008E5EB File Offset: 0x0008C7EB
	public GameplayEvent gameplayEvent
	{
		get
		{
			if (this._gameplayEvent == null)
			{
				this._gameplayEvent = Db.Get().GameplayEvents.TryGet(this.eventID);
			}
			return this._gameplayEvent;
		}
	}

	// Token: 0x060019ED RID: 6637 RVA: 0x0008E616 File Offset: 0x0008C816
	public GameplayEventInstance(GameplayEvent gameplayEvent, int worldId)
	{
		this.eventID = gameplayEvent.Id;
		this.tags = new List<Tag>();
		this.eventStartTime = GameUtil.GetCurrentTimeInCycles();
		this.worldId = worldId;
	}

	// Token: 0x060019EE RID: 6638 RVA: 0x0008E64C File Offset: 0x0008C84C
	public StateMachine.Instance PrepareEvent(GameplayEventManager manager)
	{
		this.smi = this.gameplayEvent.GetSMI(manager, this);
		return this.smi;
	}

	// Token: 0x060019EF RID: 6639 RVA: 0x0008E668 File Offset: 0x0008C868
	public void StartEvent()
	{
		StateMachine.Instance smi = this.smi;
		smi.OnStop = (Action<string, StateMachine.Status>)Delegate.Combine(smi.OnStop, new Action<string, StateMachine.Status>(this.OnStop));
		this.smi.StartSM();
		GameplayEventManager.Instance.Trigger(1491341646, this);
	}

	// Token: 0x060019F0 RID: 6640 RVA: 0x0008E6B7 File Offset: 0x0008C8B7
	public void RegisterMonitorCallback(GameObject go)
	{
		if (this.monitorCallbackObjects == null)
		{
			this.monitorCallbackObjects = new List<GameObject>();
		}
		if (!this.monitorCallbackObjects.Contains(go))
		{
			this.monitorCallbackObjects.Add(go);
		}
	}

	// Token: 0x060019F1 RID: 6641 RVA: 0x0008E6E6 File Offset: 0x0008C8E6
	public void UnregisterMonitorCallback(GameObject go)
	{
		if (this.monitorCallbackObjects == null)
		{
			this.monitorCallbackObjects = new List<GameObject>();
		}
		this.monitorCallbackObjects.Remove(go);
	}

	// Token: 0x060019F2 RID: 6642 RVA: 0x0008E708 File Offset: 0x0008C908
	public void OnStop(string reason, StateMachine.Status status)
	{
		GameplayEventManager.Instance.Trigger(1287635015, this);
		if (this.monitorCallbackObjects != null)
		{
			this.monitorCallbackObjects.ForEach(delegate(GameObject x)
			{
				x.Trigger(1287635015, this);
			});
		}
		if (status == StateMachine.Status.Success)
		{
			using (List<HashedString>.Enumerator enumerator = this.gameplayEvent.successEvents.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					HashedString hashedString = enumerator.Current;
					GameplayEvent gameplayEvent = Db.Get().GameplayEvents.TryGet(hashedString);
					DebugUtil.DevAssert(gameplayEvent != null, string.Format("GameplayEvent {0} is null", hashedString), null);
					if (gameplayEvent != null && gameplayEvent.IsAllowed())
					{
						GameplayEventManager.Instance.StartNewEvent(gameplayEvent, -1, null);
					}
				}
				return;
			}
		}
		if (status == StateMachine.Status.Failed)
		{
			foreach (HashedString hashedString2 in this.gameplayEvent.failureEvents)
			{
				GameplayEvent gameplayEvent2 = Db.Get().GameplayEvents.TryGet(hashedString2);
				DebugUtil.DevAssert(gameplayEvent2 != null, string.Format("GameplayEvent {0} is null", hashedString2), null);
				if (gameplayEvent2 != null && gameplayEvent2.IsAllowed())
				{
					GameplayEventManager.Instance.StartNewEvent(gameplayEvent2, -1, null);
				}
			}
		}
	}

	// Token: 0x060019F3 RID: 6643 RVA: 0x0008E860 File Offset: 0x0008CA60
	public float AgeInCycles()
	{
		return GameUtil.GetCurrentTimeInCycles() - this.eventStartTime;
	}

	// Token: 0x04000EE6 RID: 3814
	[Serialize]
	public readonly HashedString eventID;

	// Token: 0x04000EE7 RID: 3815
	[Serialize]
	public List<Tag> tags;

	// Token: 0x04000EE8 RID: 3816
	[Serialize]
	public float eventStartTime;

	// Token: 0x04000EE9 RID: 3817
	[Serialize]
	public readonly int worldId;

	// Token: 0x04000EEA RID: 3818
	[Serialize]
	private bool _seenNotification;

	// Token: 0x04000EEB RID: 3819
	public List<GameObject> monitorCallbackObjects;

	// Token: 0x04000EEC RID: 3820
	public GameplayEventInstance.GameplayEventPopupDataCallback GetEventPopupData;

	// Token: 0x04000EED RID: 3821
	private GameplayEvent _gameplayEvent;

	// Token: 0x020012F5 RID: 4853
	// (Invoke) Token: 0x06008843 RID: 34883
	public delegate EventInfoData GameplayEventPopupDataCallback();
}
