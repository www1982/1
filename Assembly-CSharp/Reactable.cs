using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004EB RID: 1259
public abstract class Reactable
{
	// Token: 0x170000AC RID: 172
	// (get) Token: 0x06001AEF RID: 6895 RVA: 0x00094C7A File Offset: 0x00092E7A
	public bool IsValid
	{
		get
		{
			return this.partitionerEntry.IsValid();
		}
	}

	// Token: 0x170000AD RID: 173
	// (get) Token: 0x06001AF0 RID: 6896 RVA: 0x00094C87 File Offset: 0x00092E87
	// (set) Token: 0x06001AF1 RID: 6897 RVA: 0x00094C8F File Offset: 0x00092E8F
	public float creationTime { get; private set; }

	// Token: 0x170000AE RID: 174
	// (get) Token: 0x06001AF2 RID: 6898 RVA: 0x00094C98 File Offset: 0x00092E98
	public bool IsReacting
	{
		get
		{
			return this.reactor != null;
		}
	}

	// Token: 0x06001AF3 RID: 6899 RVA: 0x00094CA8 File Offset: 0x00092EA8
	public Reactable(GameObject gameObject, HashedString id, ChoreType chore_type, int range_width = 15, int range_height = 8, bool follow_transform = false, float globalCooldown = 0f, float localCooldown = 0f, float lifeSpan = float.PositiveInfinity, float max_initial_delay = 0f, ObjectLayer overrideLayer = ObjectLayer.NumLayers)
	{
		this.rangeHeight = range_height;
		this.rangeWidth = range_width;
		this.id = id;
		this.gameObject = gameObject;
		this.choreType = chore_type;
		this.globalCooldown = globalCooldown;
		this.localCooldown = localCooldown;
		this.lifeSpan = lifeSpan;
		this.initialDelay = ((max_initial_delay > 0f) ? global::UnityEngine.Random.Range(0f, max_initial_delay) : 0f);
		this.creationTime = GameClock.Instance.GetTime();
		ObjectLayer objectLayer = ((overrideLayer == ObjectLayer.NumLayers) ? this.reactionLayer : overrideLayer);
		ReactionMonitor.Def def = gameObject.GetDef<ReactionMonitor.Def>();
		if (overrideLayer != objectLayer && def != null)
		{
			objectLayer = def.ReactionLayer;
		}
		this.reactionLayer = objectLayer;
		this.Initialize(follow_transform);
	}

	// Token: 0x06001AF4 RID: 6900 RVA: 0x00094D84 File Offset: 0x00092F84
	public void Initialize(bool followTransform)
	{
		this.UpdateLocation();
		if (followTransform)
		{
			this.transformId = Singleton<CellChangeMonitor>.Instance.RegisterCellChangedHandler(this.gameObject.transform, new global::System.Action(this.UpdateLocation), "Reactable follow transform");
		}
	}

	// Token: 0x06001AF5 RID: 6901 RVA: 0x00094DBB File Offset: 0x00092FBB
	public void Begin(GameObject reactor)
	{
		this.reactor = reactor;
		this.lastTriggerTime = GameClock.Instance.GetTime();
		this.InternalBegin();
	}

	// Token: 0x06001AF6 RID: 6902 RVA: 0x00094DDC File Offset: 0x00092FDC
	public void End()
	{
		this.InternalEnd();
		if (this.reactor != null)
		{
			GameObject gameObject = this.reactor;
			this.InternalEnd();
			this.reactor = null;
			if (gameObject != null)
			{
				ReactionMonitor.Instance smi = gameObject.GetSMI<ReactionMonitor.Instance>();
				if (smi != null)
				{
					smi.StopReaction();
				}
			}
		}
	}

	// Token: 0x06001AF7 RID: 6903 RVA: 0x00094E2C File Offset: 0x0009302C
	public bool CanBegin(GameObject reactor, Navigator.ActiveTransition transition)
	{
		float time = GameClock.Instance.GetTime();
		float num = time - this.creationTime;
		float num2 = time - this.lastTriggerTime;
		if (num < this.initialDelay || num2 < this.globalCooldown)
		{
			return false;
		}
		ChoreConsumer component = reactor.GetComponent<ChoreConsumer>();
		Chore chore = ((component != null) ? component.choreDriver.GetCurrentChore() : null);
		if (chore == null || this.choreType.priority <= chore.choreType.priority)
		{
			return false;
		}
		int num3 = 0;
		while (this.additionalPreconditions != null && num3 < this.additionalPreconditions.Count)
		{
			if (!this.additionalPreconditions[num3](reactor, transition))
			{
				return false;
			}
			num3++;
		}
		return this.InternalCanBegin(reactor, transition);
	}

	// Token: 0x06001AF8 RID: 6904 RVA: 0x00094EE6 File Offset: 0x000930E6
	public bool IsExpired()
	{
		return GameClock.Instance.GetTime() - this.creationTime > this.lifeSpan;
	}

	// Token: 0x06001AF9 RID: 6905
	public abstract bool InternalCanBegin(GameObject reactor, Navigator.ActiveTransition transition);

	// Token: 0x06001AFA RID: 6906
	public abstract void Update(float dt);

	// Token: 0x06001AFB RID: 6907
	protected abstract void InternalBegin();

	// Token: 0x06001AFC RID: 6908
	protected abstract void InternalEnd();

	// Token: 0x06001AFD RID: 6909
	protected abstract void InternalCleanup();

	// Token: 0x06001AFE RID: 6910 RVA: 0x00094F04 File Offset: 0x00093104
	public void Cleanup()
	{
		this.End();
		this.InternalCleanup();
		if (this.transformId != -1)
		{
			Singleton<CellChangeMonitor>.Instance.UnregisterCellChangedHandler(this.transformId, new global::System.Action(this.UpdateLocation));
			this.transformId = -1;
		}
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
	}

	// Token: 0x06001AFF RID: 6911 RVA: 0x00094F5C File Offset: 0x0009315C
	private void UpdateLocation()
	{
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
		if (this.gameObject != null)
		{
			this.sourceCell = Grid.PosToCell(this.gameObject);
			Extents extents = new Extents(Grid.PosToXY(this.gameObject.transform.GetPosition()).x - this.rangeWidth / 2, Grid.PosToXY(this.gameObject.transform.GetPosition()).y - this.rangeHeight / 2, this.rangeWidth, this.rangeHeight);
			this.partitionerEntry = GameScenePartitioner.Instance.Add("Reactable", this, extents, GameScenePartitioner.Instance.objectLayers[(int)this.reactionLayer], null);
		}
	}

	// Token: 0x06001B00 RID: 6912 RVA: 0x0009501D File Offset: 0x0009321D
	public Reactable AddPrecondition(Reactable.ReactablePrecondition precondition)
	{
		if (this.additionalPreconditions == null)
		{
			this.additionalPreconditions = new List<Reactable.ReactablePrecondition>();
		}
		this.additionalPreconditions.Add(precondition);
		return this;
	}

	// Token: 0x06001B01 RID: 6913 RVA: 0x0009503F File Offset: 0x0009323F
	public void InsertPrecondition(int index, Reactable.ReactablePrecondition precondition)
	{
		if (this.additionalPreconditions == null)
		{
			this.additionalPreconditions = new List<Reactable.ReactablePrecondition>();
		}
		index = Math.Min(index, this.additionalPreconditions.Count);
		this.additionalPreconditions.Insert(index, precondition);
	}

	// Token: 0x04000FE1 RID: 4065
	private HandleVector<int>.Handle partitionerEntry;

	// Token: 0x04000FE2 RID: 4066
	protected GameObject gameObject;

	// Token: 0x04000FE3 RID: 4067
	public HashedString id;

	// Token: 0x04000FE4 RID: 4068
	public bool preventChoreInterruption = true;

	// Token: 0x04000FE5 RID: 4069
	public int sourceCell;

	// Token: 0x04000FE6 RID: 4070
	private int rangeWidth;

	// Token: 0x04000FE7 RID: 4071
	private int rangeHeight;

	// Token: 0x04000FE8 RID: 4072
	private int transformId = -1;

	// Token: 0x04000FE9 RID: 4073
	public float globalCooldown;

	// Token: 0x04000FEA RID: 4074
	public float localCooldown;

	// Token: 0x04000FEB RID: 4075
	public float lifeSpan = float.PositiveInfinity;

	// Token: 0x04000FEC RID: 4076
	private float lastTriggerTime = -2.1474836E+09f;

	// Token: 0x04000FED RID: 4077
	private float initialDelay;

	// Token: 0x04000FEF RID: 4079
	protected GameObject reactor;

	// Token: 0x04000FF0 RID: 4080
	private ChoreType choreType;

	// Token: 0x04000FF1 RID: 4081
	protected LoggerFSS log;

	// Token: 0x04000FF2 RID: 4082
	private List<Reactable.ReactablePrecondition> additionalPreconditions;

	// Token: 0x04000FF3 RID: 4083
	private ObjectLayer reactionLayer;

	// Token: 0x0200133A RID: 4922
	// (Invoke) Token: 0x06008910 RID: 35088
	public delegate bool ReactablePrecondition(GameObject go, Navigator.ActiveTransition transition);
}
