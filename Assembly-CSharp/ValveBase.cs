using System;
using KSerialization;
using UnityEngine;

// Token: 0x020007EF RID: 2031
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/ValveBase")]
public class ValveBase : KMonoBehaviour, ISaveLoadable
{
	// Token: 0x170003B8 RID: 952
	// (get) Token: 0x06003738 RID: 14136 RVA: 0x00132DFB File Offset: 0x00130FFB
	// (set) Token: 0x06003737 RID: 14135 RVA: 0x00132DF2 File Offset: 0x00130FF2
	public float CurrentFlow
	{
		get
		{
			return this.currentFlow;
		}
		set
		{
			this.currentFlow = value;
		}
	}

	// Token: 0x170003B9 RID: 953
	// (get) Token: 0x06003739 RID: 14137 RVA: 0x00132E03 File Offset: 0x00131003
	public HandleVector<int>.Handle AccumulatorHandle
	{
		get
		{
			return this.flowAccumulator;
		}
	}

	// Token: 0x170003BA RID: 954
	// (get) Token: 0x0600373A RID: 14138 RVA: 0x00132E0B File Offset: 0x0013100B
	public float MaxFlow
	{
		get
		{
			return this.maxFlow;
		}
	}

	// Token: 0x0600373B RID: 14139 RVA: 0x00132E13 File Offset: 0x00131013
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.flowAccumulator = Game.Instance.accumulators.Add("Flow", this);
	}

	// Token: 0x0600373C RID: 14140 RVA: 0x00132E38 File Offset: 0x00131038
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Building component = base.GetComponent<Building>();
		this.inputCell = component.GetUtilityInputCell();
		this.outputCell = component.GetUtilityOutputCell();
		Conduit.GetFlowManager(this.conduitType).AddConduitUpdater(new Action<float>(this.ConduitUpdate), ConduitFlowPriority.Default);
		this.UpdateAnim();
		this.OnCmpEnable();
	}

	// Token: 0x0600373D RID: 14141 RVA: 0x00132E93 File Offset: 0x00131093
	protected override void OnCleanUp()
	{
		Game.Instance.accumulators.Remove(this.flowAccumulator);
		Conduit.GetFlowManager(this.conduitType).RemoveConduitUpdater(new Action<float>(this.ConduitUpdate));
		base.OnCleanUp();
	}

	// Token: 0x0600373E RID: 14142 RVA: 0x00132ED0 File Offset: 0x001310D0
	private void ConduitUpdate(float dt)
	{
		ConduitFlow flowManager = Conduit.GetFlowManager(this.conduitType);
		ConduitFlow.Conduit conduit = flowManager.GetConduit(this.inputCell);
		if (!flowManager.HasConduit(this.inputCell) || !flowManager.HasConduit(this.outputCell))
		{
			this.OnMassTransfer(0f);
			this.UpdateAnim();
			return;
		}
		ConduitFlow.ConduitContents contents = conduit.GetContents(flowManager);
		float num = Mathf.Min(contents.mass, this.currentFlow * dt);
		float num2 = 0f;
		if (num > 0f)
		{
			int num3 = (int)(num / contents.mass * (float)contents.diseaseCount);
			num2 = flowManager.AddElement(this.outputCell, contents.element, num, contents.temperature, contents.diseaseIdx, num3);
			Game.Instance.accumulators.Accumulate(this.flowAccumulator, num2);
			if (num2 > 0f)
			{
				flowManager.RemoveElement(this.inputCell, num2);
			}
		}
		this.OnMassTransfer(num2);
		this.UpdateAnim();
	}

	// Token: 0x0600373F RID: 14143 RVA: 0x00132FC5 File Offset: 0x001311C5
	protected virtual void OnMassTransfer(float amount)
	{
	}

	// Token: 0x06003740 RID: 14144 RVA: 0x00132FC8 File Offset: 0x001311C8
	public virtual void UpdateAnim()
	{
		float averageRate = Game.Instance.accumulators.GetAverageRate(this.flowAccumulator);
		if (averageRate > 0f)
		{
			int i = 0;
			while (i < this.animFlowRanges.Length)
			{
				if (averageRate <= this.animFlowRanges[i].minFlow)
				{
					if (this.curFlowIdx != i)
					{
						this.curFlowIdx = i;
						this.controller.Play(this.animFlowRanges[i].animName, (averageRate <= 0f) ? KAnim.PlayMode.Once : KAnim.PlayMode.Loop, 1f, 0f);
						return;
					}
					return;
				}
				else
				{
					i++;
				}
			}
			return;
		}
		this.controller.Play("off", KAnim.PlayMode.Once, 1f, 0f);
	}

	// Token: 0x04002178 RID: 8568
	[SerializeField]
	public ConduitType conduitType;

	// Token: 0x04002179 RID: 8569
	[SerializeField]
	public float maxFlow = 0.5f;

	// Token: 0x0400217A RID: 8570
	[Serialize]
	private float currentFlow;

	// Token: 0x0400217B RID: 8571
	[MyCmpGet]
	protected KBatchedAnimController controller;

	// Token: 0x0400217C RID: 8572
	protected HandleVector<int>.Handle flowAccumulator = HandleVector<int>.InvalidHandle;

	// Token: 0x0400217D RID: 8573
	private int curFlowIdx = -1;

	// Token: 0x0400217E RID: 8574
	private int inputCell;

	// Token: 0x0400217F RID: 8575
	private int outputCell;

	// Token: 0x04002180 RID: 8576
	[SerializeField]
	public ValveBase.AnimRangeInfo[] animFlowRanges;

	// Token: 0x0200174C RID: 5964
	[Serializable]
	public struct AnimRangeInfo
	{
		// Token: 0x06009891 RID: 39057 RVA: 0x00381391 File Offset: 0x0037F591
		public AnimRangeInfo(float min_flow, string anim_name)
		{
			this.minFlow = min_flow;
			this.animName = anim_name;
		}

		// Token: 0x04007542 RID: 30018
		public float minFlow;

		// Token: 0x04007543 RID: 30019
		public string animName;
	}
}
