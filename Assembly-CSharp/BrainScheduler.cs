using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000465 RID: 1125
[AddComponentMenu("KMonoBehaviour/scripts/BrainScheduler")]
public class BrainScheduler : KMonoBehaviour, IRenderEveryTick, ICPULoad
{
	// Token: 0x17000071 RID: 113
	// (get) Token: 0x060017AB RID: 6059 RVA: 0x0008339C File Offset: 0x0008159C
	private bool isAsyncPathProbeEnabled
	{
		get
		{
			return !TuningData<BrainScheduler.Tuning>.Get().disableAsyncPathProbes;
		}
	}

	// Token: 0x060017AC RID: 6060 RVA: 0x000833AB File Offset: 0x000815AB
	public List<BrainScheduler.BrainGroup> debugGetBrainGroups()
	{
		return this.brainGroups;
	}

	// Token: 0x060017AD RID: 6061 RVA: 0x000833B4 File Offset: 0x000815B4
	protected override void OnPrefabInit()
	{
		this.brainGroups.Add(new BrainScheduler.DupeBrainGroup());
		this.brainGroups.Add(new BrainScheduler.CreatureBrainGroup());
		Components.Brains.Register(new Action<Brain>(this.OnAddBrain), new Action<Brain>(this.OnRemoveBrain));
		CPUBudget.AddRoot(this);
		foreach (BrainScheduler.BrainGroup brainGroup in this.brainGroups)
		{
			CPUBudget.AddChild(this, brainGroup, brainGroup.LoadBalanceThreshold());
		}
		CPUBudget.FinalizeChildren(this);
	}

	// Token: 0x060017AE RID: 6062 RVA: 0x0008345C File Offset: 0x0008165C
	private void OnAddBrain(Brain brain)
	{
		bool flag = false;
		foreach (BrainScheduler.BrainGroup brainGroup in this.brainGroups)
		{
			if (brain.HasTag(brainGroup.tag))
			{
				brainGroup.AddBrain(brain);
				flag = true;
			}
			Navigator component = brain.GetComponent<Navigator>();
			if (component != null)
			{
				component.executePathProbeTaskAsync = this.isAsyncPathProbeEnabled;
			}
		}
		DebugUtil.Assert(flag);
	}

	// Token: 0x060017AF RID: 6063 RVA: 0x000834E4 File Offset: 0x000816E4
	private void OnRemoveBrain(Brain brain)
	{
		bool flag = false;
		foreach (BrainScheduler.BrainGroup brainGroup in this.brainGroups)
		{
			if (brain.HasTag(brainGroup.tag))
			{
				flag = true;
				brainGroup.RemoveBrain(brain);
			}
			Navigator component = brain.GetComponent<Navigator>();
			if (component != null)
			{
				component.executePathProbeTaskAsync = false;
			}
		}
		DebugUtil.Assert(flag);
	}

	// Token: 0x060017B0 RID: 6064 RVA: 0x00083568 File Offset: 0x00081768
	public void PrioritizeBrain(Brain brain)
	{
		foreach (BrainScheduler.BrainGroup brainGroup in this.brainGroups)
		{
			if (brain.HasTag(brainGroup.tag))
			{
				brainGroup.PrioritizeBrain(brain);
			}
		}
	}

	// Token: 0x060017B1 RID: 6065 RVA: 0x000835CC File Offset: 0x000817CC
	public float GetEstimatedFrameTime()
	{
		return TuningData<BrainScheduler.Tuning>.Get().frameTime;
	}

	// Token: 0x060017B2 RID: 6066 RVA: 0x000835D8 File Offset: 0x000817D8
	public bool AdjustLoad(float currentFrameTime, float frameTimeDelta)
	{
		return false;
	}

	// Token: 0x060017B3 RID: 6067 RVA: 0x000835DC File Offset: 0x000817DC
	public void RenderEveryTick(float dt)
	{
		if (Game.IsQuitting() || KMonoBehaviour.isLoadingScene)
		{
			return;
		}
		foreach (BrainScheduler.BrainGroup brainGroup in this.brainGroups)
		{
			brainGroup.RenderEveryTick(dt);
		}
	}

	// Token: 0x060017B4 RID: 6068 RVA: 0x0008363C File Offset: 0x0008183C
	protected override void OnForcedCleanUp()
	{
		CPUBudget.Remove(this);
		base.OnForcedCleanUp();
	}

	// Token: 0x04000DB0 RID: 3504
	public const float millisecondsPerFrame = 33.33333f;

	// Token: 0x04000DB1 RID: 3505
	public const float secondsPerFrame = 0.033333328f;

	// Token: 0x04000DB2 RID: 3506
	public const float framesPerSecond = 30.000006f;

	// Token: 0x04000DB3 RID: 3507
	private List<BrainScheduler.BrainGroup> brainGroups = new List<BrainScheduler.BrainGroup>();

	// Token: 0x0200124A RID: 4682
	private class Tuning : TuningData<BrainScheduler.Tuning>
	{
		// Token: 0x0400658C RID: 25996
		public bool disableAsyncPathProbes;

		// Token: 0x0400658D RID: 25997
		public float frameTime = 5f;
	}

	// Token: 0x0200124B RID: 4683
	public abstract class BrainGroup : ICPULoad
	{
		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x0600859B RID: 34203 RVA: 0x003392AA File Offset: 0x003374AA
		// (set) Token: 0x0600859C RID: 34204 RVA: 0x003392B2 File Offset: 0x003374B2
		public Tag tag { get; private set; }

		// Token: 0x0600859D RID: 34205 RVA: 0x003392BC File Offset: 0x003374BC
		protected BrainGroup(Tag tag)
		{
			this.tag = tag;
			this.probeSize = this.InitialProbeSize();
			this.probeCount = this.InitialProbeCount();
			string text = tag.ToString();
			this.increaseLoadLabel = "IncLoad" + text;
			this.decreaseLoadLabel = "DecLoad" + text;
		}

		// Token: 0x0600859E RID: 34206 RVA: 0x0033933F File Offset: 0x0033753F
		public void AddBrain(Brain brain)
		{
			this.brains.Add(brain);
		}

		// Token: 0x0600859F RID: 34207 RVA: 0x00339350 File Offset: 0x00337550
		public void RemoveBrain(Brain brain)
		{
			int num = this.brains.IndexOf(brain);
			if (num != -1)
			{
				this.brains.RemoveAt(num);
				this.OnRemoveBrain(num, ref this.nextUpdateBrain);
				this.OnRemoveBrain(num, ref this.nextPathProbeBrain);
			}
			if (this.priorityBrains.Contains(brain))
			{
				List<Brain> list = new List<Brain>(this.priorityBrains);
				list.Remove(brain);
				this.priorityBrains = new Queue<Brain>(list);
			}
		}

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x060085A0 RID: 34208 RVA: 0x003393C2 File Offset: 0x003375C2
		public int BrainCount
		{
			get
			{
				return this.brains.Count;
			}
		}

		// Token: 0x060085A1 RID: 34209 RVA: 0x003393CF File Offset: 0x003375CF
		public void PrioritizeBrain(Brain brain)
		{
			if (!this.priorityBrains.Contains(brain))
			{
				this.priorityBrains.Enqueue(brain);
			}
		}

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x060085A2 RID: 34210 RVA: 0x003393EB File Offset: 0x003375EB
		// (set) Token: 0x060085A3 RID: 34211 RVA: 0x003393F3 File Offset: 0x003375F3
		public int probeSize { get; private set; }

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x060085A4 RID: 34212 RVA: 0x003393FC File Offset: 0x003375FC
		// (set) Token: 0x060085A5 RID: 34213 RVA: 0x00339404 File Offset: 0x00337604
		public int probeCount { get; private set; }

		// Token: 0x060085A6 RID: 34214 RVA: 0x00339410 File Offset: 0x00337610
		public bool AdjustLoad(float currentFrameTime, float frameTimeDelta)
		{
			if (this.debugFreezeLoadAdustment)
			{
				return false;
			}
			bool flag = frameTimeDelta > 0f;
			int num = 0;
			int num2 = Math.Max(this.probeCount, Math.Min(this.brains.Count, CPUBudget.coreCount));
			num += num2 - this.probeCount;
			this.probeCount = num2;
			float num3 = Math.Min(1f, (float)this.probeCount / (float)CPUBudget.coreCount);
			float num4 = num3 * (float)this.probeSize;
			float num5 = num3 * (float)this.probeSize;
			float num6 = currentFrameTime / num5;
			float num7 = frameTimeDelta / num6;
			if (num == 0)
			{
				float num8 = num4 + num7 / (float)CPUBudget.coreCount;
				int num9 = MathUtil.Clamp(this.MinProbeSize(), this.IdealProbeSize(), (int)(num8 / num3));
				num += num9 - this.probeSize;
				this.probeSize = num9;
			}
			if (num == 0)
			{
				int num10 = Math.Max(1, (int)num3 + (flag ? 1 : (-1)));
				int num11 = MathUtil.Clamp(this.MinProbeSize(), this.IdealProbeSize(), (int)((num5 + num7) / (float)num10));
				int num12 = Math.Min(this.brains.Count, num10 * CPUBudget.coreCount);
				num += num12 - this.probeCount;
				this.probeCount = num12;
				this.probeSize = num11;
			}
			if (num == 0 && flag)
			{
				int num13 = this.probeSize + this.ProbeSizeStep();
				num += num13 - this.probeSize;
				this.probeSize = num13;
			}
			if (num >= 0 && num <= 0 && this.brains.Count > 0)
			{
				global::Debug.LogWarning("AdjustLoad() failed");
			}
			return num != 0;
		}

		// Token: 0x060085A7 RID: 34215 RVA: 0x00339594 File Offset: 0x00337794
		public void ResetLoad()
		{
			this.probeSize = this.InitialProbeSize();
			this.probeCount = this.InitialProbeCount();
		}

		// Token: 0x060085A8 RID: 34216 RVA: 0x003395AE File Offset: 0x003377AE
		private void IncrementBrainIndex(ref int brainIndex)
		{
			brainIndex++;
			if (brainIndex == this.brains.Count)
			{
				brainIndex = 0;
			}
		}

		// Token: 0x060085A9 RID: 34217 RVA: 0x003395C8 File Offset: 0x003377C8
		private void ClampBrainIndex(ref int brainIndex)
		{
			brainIndex = MathUtil.Clamp(0, this.brains.Count - 1, brainIndex);
		}

		// Token: 0x060085AA RID: 34218 RVA: 0x003395E1 File Offset: 0x003377E1
		private void OnRemoveBrain(int removedIndex, ref int brainIndex)
		{
			if (removedIndex < brainIndex)
			{
				brainIndex--;
				return;
			}
			if (brainIndex == this.brains.Count)
			{
				brainIndex = 0;
			}
		}

		// Token: 0x060085AB RID: 34219 RVA: 0x00339604 File Offset: 0x00337804
		private void AsyncPathProbe()
		{
			this.pathProbeJob.Reset(null);
			for (int num = 0; num != this.brains.Count; num++)
			{
				this.ClampBrainIndex(ref this.nextPathProbeBrain);
				Brain brain = this.brains[this.nextPathProbeBrain];
				if (brain.IsRunning())
				{
					Navigator component = brain.GetComponent<Navigator>();
					if (component != null)
					{
						component.executePathProbeTaskAsync = true;
						component.PathProber.potentialCellsPerUpdate = this.probeSize;
						component.pathProbeTask.Update();
						this.pathProbeJob.Add(component.pathProbeTask);
						if (this.pathProbeJob.Count == this.probeCount)
						{
							break;
						}
					}
				}
				this.IncrementBrainIndex(ref this.nextPathProbeBrain);
			}
			CPUBudget.Start(this);
			GlobalJobManager.Run(this.pathProbeJob);
			CPUBudget.End(this);
		}

		// Token: 0x060085AC RID: 34220 RVA: 0x003396DC File Offset: 0x003378DC
		public void RenderEveryTick(float dt)
		{
			this.BeginBrainGroupUpdate();
			int num = this.InitialProbeCount();
			int num2 = 0;
			while (num2 != this.brains.Count && num != 0)
			{
				this.ClampBrainIndex(ref this.nextUpdateBrain);
				this.debugMaxPriorityBrainCountSeen = Mathf.Max(this.debugMaxPriorityBrainCountSeen, this.priorityBrains.Count);
				Brain brain;
				if (this.AllowPriorityBrains() && this.priorityBrains.Count > 0)
				{
					brain = this.priorityBrains.Dequeue();
				}
				else
				{
					brain = this.brains[this.nextUpdateBrain];
					this.IncrementBrainIndex(ref this.nextUpdateBrain);
				}
				if (brain.IsRunning())
				{
					brain.UpdateBrain();
					num--;
				}
				num2++;
			}
			this.EndBrainGroupUpdate();
		}

		// Token: 0x060085AD RID: 34221 RVA: 0x0033979C File Offset: 0x0033799C
		public void AccumulatePathProbeIterations(Dictionary<string, int> pathProbeIterations)
		{
			foreach (Brain brain in this.brains)
			{
				Navigator component = brain.GetComponent<Navigator>();
				if (!(component == null) && !pathProbeIterations.ContainsKey(brain.name))
				{
					pathProbeIterations.Add(brain.name, component.PathProber.updateCount);
				}
			}
		}

		// Token: 0x060085AE RID: 34222
		protected abstract int InitialProbeCount();

		// Token: 0x060085AF RID: 34223
		protected abstract int InitialProbeSize();

		// Token: 0x060085B0 RID: 34224
		protected abstract int MinProbeSize();

		// Token: 0x060085B1 RID: 34225
		protected abstract int IdealProbeSize();

		// Token: 0x060085B2 RID: 34226
		protected abstract int ProbeSizeStep();

		// Token: 0x060085B3 RID: 34227
		public abstract float GetEstimatedFrameTime();

		// Token: 0x060085B4 RID: 34228
		public abstract float LoadBalanceThreshold();

		// Token: 0x060085B5 RID: 34229
		public abstract bool AllowPriorityBrains();

		// Token: 0x060085B6 RID: 34230 RVA: 0x00339820 File Offset: 0x00337A20
		public virtual void BeginBrainGroupUpdate()
		{
			if (Game.BrainScheduler.isAsyncPathProbeEnabled)
			{
				this.AsyncPathProbe();
			}
		}

		// Token: 0x060085B7 RID: 34231 RVA: 0x00339834 File Offset: 0x00337A34
		public virtual void EndBrainGroupUpdate()
		{
		}

		// Token: 0x0400658F RID: 25999
		protected List<Brain> brains = new List<Brain>();

		// Token: 0x04006590 RID: 26000
		protected Queue<Brain> priorityBrains = new Queue<Brain>();

		// Token: 0x04006591 RID: 26001
		private string increaseLoadLabel;

		// Token: 0x04006592 RID: 26002
		private string decreaseLoadLabel;

		// Token: 0x04006593 RID: 26003
		public bool debugFreezeLoadAdustment;

		// Token: 0x04006594 RID: 26004
		public int debugMaxPriorityBrainCountSeen;

		// Token: 0x04006595 RID: 26005
		private WorkItemCollection<Navigator.PathProbeTask, object> pathProbeJob = new WorkItemCollection<Navigator.PathProbeTask, object>();

		// Token: 0x04006596 RID: 26006
		private int nextUpdateBrain;

		// Token: 0x04006597 RID: 26007
		private int nextPathProbeBrain;
	}

	// Token: 0x0200124C RID: 4684
	private class DupeBrainGroup : BrainScheduler.BrainGroup
	{
		// Token: 0x060085B8 RID: 34232 RVA: 0x00339836 File Offset: 0x00337A36
		public DupeBrainGroup()
			: base(GameTags.DupeBrain)
		{
		}

		// Token: 0x060085B9 RID: 34233 RVA: 0x0033984A File Offset: 0x00337A4A
		protected override int InitialProbeCount()
		{
			return TuningData<BrainScheduler.DupeBrainGroup.Tuning>.Get().initialProbeCount;
		}

		// Token: 0x060085BA RID: 34234 RVA: 0x00339856 File Offset: 0x00337A56
		protected override int InitialProbeSize()
		{
			return TuningData<BrainScheduler.DupeBrainGroup.Tuning>.Get().initialProbeSize;
		}

		// Token: 0x060085BB RID: 34235 RVA: 0x00339862 File Offset: 0x00337A62
		protected override int MinProbeSize()
		{
			return TuningData<BrainScheduler.DupeBrainGroup.Tuning>.Get().minProbeSize;
		}

		// Token: 0x060085BC RID: 34236 RVA: 0x0033986E File Offset: 0x00337A6E
		protected override int IdealProbeSize()
		{
			return TuningData<BrainScheduler.DupeBrainGroup.Tuning>.Get().idealProbeSize;
		}

		// Token: 0x060085BD RID: 34237 RVA: 0x0033987A File Offset: 0x00337A7A
		protected override int ProbeSizeStep()
		{
			return TuningData<BrainScheduler.DupeBrainGroup.Tuning>.Get().probeSizeStep;
		}

		// Token: 0x060085BE RID: 34238 RVA: 0x00339886 File Offset: 0x00337A86
		public override float GetEstimatedFrameTime()
		{
			return TuningData<BrainScheduler.DupeBrainGroup.Tuning>.Get().estimatedFrameTime;
		}

		// Token: 0x060085BF RID: 34239 RVA: 0x00339892 File Offset: 0x00337A92
		public override float LoadBalanceThreshold()
		{
			return TuningData<BrainScheduler.DupeBrainGroup.Tuning>.Get().loadBalanceThreshold;
		}

		// Token: 0x060085C0 RID: 34240 RVA: 0x0033989E File Offset: 0x00337A9E
		public override bool AllowPriorityBrains()
		{
			return this.usePriorityBrain;
		}

		// Token: 0x060085C1 RID: 34241 RVA: 0x003398A6 File Offset: 0x00337AA6
		public override void BeginBrainGroupUpdate()
		{
			base.BeginBrainGroupUpdate();
			this.usePriorityBrain = !this.usePriorityBrain;
		}

		// Token: 0x0400659A RID: 26010
		private bool usePriorityBrain = true;

		// Token: 0x02002628 RID: 9768
		public class Tuning : TuningData<BrainScheduler.DupeBrainGroup.Tuning>
		{
			// Token: 0x0400A9B3 RID: 43443
			public int initialProbeCount = 1;

			// Token: 0x0400A9B4 RID: 43444
			public int initialProbeSize = 1000;

			// Token: 0x0400A9B5 RID: 43445
			public int minProbeSize = 100;

			// Token: 0x0400A9B6 RID: 43446
			public int idealProbeSize = 1000;

			// Token: 0x0400A9B7 RID: 43447
			public int probeSizeStep = 100;

			// Token: 0x0400A9B8 RID: 43448
			public float estimatedFrameTime = 2f;

			// Token: 0x0400A9B9 RID: 43449
			public float loadBalanceThreshold = 0.1f;
		}
	}

	// Token: 0x0200124D RID: 4685
	private class CreatureBrainGroup : BrainScheduler.BrainGroup
	{
		// Token: 0x060085C2 RID: 34242 RVA: 0x003398BD File Offset: 0x00337ABD
		public CreatureBrainGroup()
			: base(GameTags.CreatureBrain)
		{
		}

		// Token: 0x060085C3 RID: 34243 RVA: 0x003398CA File Offset: 0x00337ACA
		protected override int InitialProbeCount()
		{
			return TuningData<BrainScheduler.CreatureBrainGroup.Tuning>.Get().initialProbeCount;
		}

		// Token: 0x060085C4 RID: 34244 RVA: 0x003398D6 File Offset: 0x00337AD6
		protected override int InitialProbeSize()
		{
			return TuningData<BrainScheduler.CreatureBrainGroup.Tuning>.Get().initialProbeSize;
		}

		// Token: 0x060085C5 RID: 34245 RVA: 0x003398E2 File Offset: 0x00337AE2
		protected override int MinProbeSize()
		{
			return TuningData<BrainScheduler.CreatureBrainGroup.Tuning>.Get().minProbeSize;
		}

		// Token: 0x060085C6 RID: 34246 RVA: 0x003398EE File Offset: 0x00337AEE
		protected override int IdealProbeSize()
		{
			return TuningData<BrainScheduler.CreatureBrainGroup.Tuning>.Get().idealProbeSize;
		}

		// Token: 0x060085C7 RID: 34247 RVA: 0x003398FA File Offset: 0x00337AFA
		protected override int ProbeSizeStep()
		{
			return TuningData<BrainScheduler.CreatureBrainGroup.Tuning>.Get().probeSizeStep;
		}

		// Token: 0x060085C8 RID: 34248 RVA: 0x00339906 File Offset: 0x00337B06
		public override float GetEstimatedFrameTime()
		{
			return TuningData<BrainScheduler.CreatureBrainGroup.Tuning>.Get().estimatedFrameTime;
		}

		// Token: 0x060085C9 RID: 34249 RVA: 0x00339912 File Offset: 0x00337B12
		public override float LoadBalanceThreshold()
		{
			return TuningData<BrainScheduler.CreatureBrainGroup.Tuning>.Get().loadBalanceThreshold;
		}

		// Token: 0x060085CA RID: 34250 RVA: 0x0033991E File Offset: 0x00337B1E
		public override bool AllowPriorityBrains()
		{
			return true;
		}

		// Token: 0x02002629 RID: 9769
		public class Tuning : TuningData<BrainScheduler.CreatureBrainGroup.Tuning>
		{
			// Token: 0x0400A9BA RID: 43450
			public int initialProbeCount = 5;

			// Token: 0x0400A9BB RID: 43451
			public int initialProbeSize = 1000;

			// Token: 0x0400A9BC RID: 43452
			public int minProbeSize = 100;

			// Token: 0x0400A9BD RID: 43453
			public int idealProbeSize = 300;

			// Token: 0x0400A9BE RID: 43454
			public int probeSizeStep = 100;

			// Token: 0x0400A9BF RID: 43455
			public float estimatedFrameTime = 1f;

			// Token: 0x0400A9C0 RID: 43456
			public float loadBalanceThreshold = 0.1f;
		}
	}
}
