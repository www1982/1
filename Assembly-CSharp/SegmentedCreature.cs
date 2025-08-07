using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000109 RID: 265
public class SegmentedCreature : GameStateMachine<SegmentedCreature, SegmentedCreature.Instance, IStateMachineTarget, SegmentedCreature.Def>
{
	// Token: 0x060004CA RID: 1226 RVA: 0x0002707C File Offset: 0x0002527C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.freeMovement.idle;
		this.root.Enter(new StateMachine<SegmentedCreature, SegmentedCreature.Instance, IStateMachineTarget, SegmentedCreature.Def>.State.Callback(this.SetRetractedPath));
		this.retracted.DefaultState(this.retracted.pre).Enter(delegate(SegmentedCreature.Instance smi)
		{
			this.PlayBodySegmentsAnim(smi, "idle_loop", KAnim.PlayMode.Loop, false, 0);
		}).Exit(new StateMachine<SegmentedCreature, SegmentedCreature.Instance, IStateMachineTarget, SegmentedCreature.Def>.State.Callback(this.SetRetractedPath));
		this.retracted.pre.Update(new Action<SegmentedCreature.Instance, float>(this.UpdateRetractedPre), UpdateRate.SIM_EVERY_TICK, false);
		this.retracted.loop.ParamTransition<bool>(this.isRetracted, this.freeMovement, (SegmentedCreature.Instance smi, bool p) => !this.isRetracted.Get(smi)).Update(new Action<SegmentedCreature.Instance, float>(this.UpdateRetractedLoop), UpdateRate.SIM_EVERY_TICK, false);
		this.freeMovement.DefaultState(this.freeMovement.idle).ParamTransition<bool>(this.isRetracted, this.retracted, (SegmentedCreature.Instance smi, bool p) => this.isRetracted.Get(smi)).Update(new Action<SegmentedCreature.Instance, float>(this.UpdateFreeMovement), UpdateRate.SIM_EVERY_TICK, false);
		this.freeMovement.idle.Transition(this.freeMovement.moving, (SegmentedCreature.Instance smi) => smi.GetComponent<Navigator>().IsMoving(), UpdateRate.SIM_200ms).Enter(delegate(SegmentedCreature.Instance smi)
		{
			this.PlayBodySegmentsAnim(smi, "idle_loop", KAnim.PlayMode.Loop, true, 0);
		});
		this.freeMovement.moving.Transition(this.freeMovement.idle, (SegmentedCreature.Instance smi) => !smi.GetComponent<Navigator>().IsMoving(), UpdateRate.SIM_200ms).Enter(delegate(SegmentedCreature.Instance smi)
		{
			this.PlayBodySegmentsAnim(smi, "walking_pre", KAnim.PlayMode.Once, false, 0);
			this.PlayBodySegmentsAnim(smi, "walking_loop", KAnim.PlayMode.Loop, false, smi.def.animFrameOffset);
		}).Exit(delegate(SegmentedCreature.Instance smi)
		{
			this.PlayBodySegmentsAnim(smi, "walking_pst", KAnim.PlayMode.Once, true, 0);
		});
	}

	// Token: 0x060004CB RID: 1227 RVA: 0x00027234 File Offset: 0x00025434
	private void PlayBodySegmentsAnim(SegmentedCreature.Instance smi, string animName, KAnim.PlayMode playMode, bool queue = false, int frameOffset = 0)
	{
		LinkedListNode<SegmentedCreature.CreatureSegment> linkedListNode = smi.GetFirstBodySegmentNode();
		int num = 0;
		while (linkedListNode != null)
		{
			if (queue)
			{
				linkedListNode.Value.animController.Queue(animName, playMode, 1f, 0f);
			}
			else
			{
				linkedListNode.Value.animController.Play(animName, playMode, 1f, 0f);
			}
			if (frameOffset > 0)
			{
				float num2 = (float)linkedListNode.Value.animController.GetCurrentNumFrames();
				float num3 = (float)num * ((float)frameOffset / num2);
				linkedListNode.Value.animController.SetElapsedTime(num3);
			}
			num++;
			linkedListNode = linkedListNode.Next;
		}
	}

	// Token: 0x060004CC RID: 1228 RVA: 0x000272DC File Offset: 0x000254DC
	private void UpdateRetractedPre(SegmentedCreature.Instance smi, float dt)
	{
		if (this.UpdateHeadPosition(smi) == 0f)
		{
			return;
		}
		bool flag = true;
		for (LinkedListNode<SegmentedCreature.CreatureSegment> linkedListNode = smi.GetFirstBodySegmentNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
		{
			linkedListNode.Value.distanceToPreviousSegment = Mathf.Max(smi.def.minSegmentSpacing, linkedListNode.Value.distanceToPreviousSegment - dt * smi.def.retractionSegmentSpeed);
			if (linkedListNode.Value.distanceToPreviousSegment > smi.def.minSegmentSpacing)
			{
				flag = false;
			}
		}
		SegmentedCreature.CreatureSegment value = smi.GetHeadSegmentNode().Value;
		LinkedListNode<SegmentedCreature.PathNode> linkedListNode2 = smi.path.First;
		Vector3 forward = value.Forward;
		Quaternion rotation = value.Rotation;
		int num = 0;
		while (linkedListNode2 != null)
		{
			Vector3 vector = value.Position - smi.def.pathSpacing * (float)num * forward;
			linkedListNode2.Value.position = Vector3.Lerp(linkedListNode2.Value.position, vector, dt * smi.def.retractionPathSpeed);
			linkedListNode2.Value.rotation = Quaternion.Slerp(linkedListNode2.Value.rotation, rotation, dt * smi.def.retractionPathSpeed);
			num++;
			linkedListNode2 = linkedListNode2.Next;
		}
		this.UpdateBodyPosition(smi);
		if (flag)
		{
			smi.GoTo(this.retracted.loop);
		}
	}

	// Token: 0x060004CD RID: 1229 RVA: 0x00027430 File Offset: 0x00025630
	private void UpdateRetractedLoop(SegmentedCreature.Instance smi, float dt)
	{
		if (this.UpdateHeadPosition(smi) != 0f)
		{
			this.SetRetractedPath(smi);
			this.UpdateBodyPosition(smi);
		}
	}

	// Token: 0x060004CE RID: 1230 RVA: 0x00027450 File Offset: 0x00025650
	private void SetRetractedPath(SegmentedCreature.Instance smi)
	{
		SegmentedCreature.CreatureSegment value = smi.GetHeadSegmentNode().Value;
		LinkedListNode<SegmentedCreature.PathNode> linkedListNode = smi.path.First;
		Vector3 position = value.Position;
		Quaternion rotation = value.Rotation;
		Vector3 forward = value.Forward;
		int num = 0;
		while (linkedListNode != null)
		{
			linkedListNode.Value.position = position - smi.def.pathSpacing * (float)num * forward;
			linkedListNode.Value.rotation = rotation;
			num++;
			linkedListNode = linkedListNode.Next;
		}
	}

	// Token: 0x060004CF RID: 1231 RVA: 0x000274D0 File Offset: 0x000256D0
	private void UpdateFreeMovement(SegmentedCreature.Instance smi, float dt)
	{
		float num = this.UpdateHeadPosition(smi);
		if (num != 0f)
		{
			this.AdjustBodySegmentsSpacing(smi, num);
			this.UpdateBodyPosition(smi);
		}
	}

	// Token: 0x060004D0 RID: 1232 RVA: 0x000274FC File Offset: 0x000256FC
	private float UpdateHeadPosition(SegmentedCreature.Instance smi)
	{
		SegmentedCreature.CreatureSegment value = smi.GetHeadSegmentNode().Value;
		if (value.Position == smi.previousHeadPosition)
		{
			return 0f;
		}
		SegmentedCreature.PathNode value2 = smi.path.First.Value;
		SegmentedCreature.PathNode pathNode = smi.path.First.Next.Value;
		float magnitude = (value2.position - pathNode.position).magnitude;
		float magnitude2 = (value.Position - pathNode.position).magnitude;
		float num = magnitude2 - magnitude;
		value2.position = value.Position;
		value2.rotation = value.Rotation;
		smi.previousHeadPosition = value2.position;
		Vector3 normalized = (value2.position - pathNode.position).normalized;
		int num2 = Mathf.FloorToInt(magnitude2 / smi.def.pathSpacing);
		for (int i = 0; i < num2; i++)
		{
			Vector3 vector = pathNode.position + normalized * smi.def.pathSpacing;
			LinkedListNode<SegmentedCreature.PathNode> last = smi.path.Last;
			last.Value.position = vector;
			last.Value.rotation = value2.rotation;
			float num3 = magnitude2 - (float)i * smi.def.pathSpacing;
			float num4 = num3 - smi.def.pathSpacing / num3;
			last.Value.rotation = Quaternion.Lerp(value2.rotation, pathNode.rotation, num4);
			smi.path.RemoveLast();
			smi.path.AddAfter(smi.path.First, last);
			pathNode = last.Value;
		}
		return num;
	}

	// Token: 0x060004D1 RID: 1233 RVA: 0x000276C0 File Offset: 0x000258C0
	private void AdjustBodySegmentsSpacing(SegmentedCreature.Instance smi, float spacing)
	{
		for (LinkedListNode<SegmentedCreature.CreatureSegment> linkedListNode = smi.GetFirstBodySegmentNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
		{
			linkedListNode.Value.distanceToPreviousSegment += spacing;
			if (linkedListNode.Value.distanceToPreviousSegment < smi.def.minSegmentSpacing)
			{
				spacing = linkedListNode.Value.distanceToPreviousSegment - smi.def.minSegmentSpacing;
				linkedListNode.Value.distanceToPreviousSegment = smi.def.minSegmentSpacing;
			}
			else
			{
				if (linkedListNode.Value.distanceToPreviousSegment <= smi.def.maxSegmentSpacing)
				{
					break;
				}
				spacing = linkedListNode.Value.distanceToPreviousSegment - smi.def.maxSegmentSpacing;
				linkedListNode.Value.distanceToPreviousSegment = smi.def.maxSegmentSpacing;
			}
		}
	}

	// Token: 0x060004D2 RID: 1234 RVA: 0x0002778C File Offset: 0x0002598C
	private void UpdateBodyPosition(SegmentedCreature.Instance smi)
	{
		LinkedListNode<SegmentedCreature.CreatureSegment> linkedListNode = smi.GetFirstBodySegmentNode();
		LinkedListNode<SegmentedCreature.PathNode> linkedListNode2 = smi.path.First;
		float num = 0f;
		float num2 = smi.LengthPercentage();
		int num3 = 0;
		while (linkedListNode != null)
		{
			float num4 = linkedListNode.Value.distanceToPreviousSegment;
			float num5 = 0f;
			while (linkedListNode2.Next != null)
			{
				num5 = (linkedListNode2.Value.position - linkedListNode2.Next.Value.position).magnitude - num;
				if (num4 < num5)
				{
					break;
				}
				num4 -= num5;
				num = 0f;
				linkedListNode2 = linkedListNode2.Next;
			}
			if (linkedListNode2.Next == null)
			{
				linkedListNode.Value.SetPosition(linkedListNode2.Value.position);
				linkedListNode.Value.SetRotation(smi.path.Last.Value.rotation);
			}
			else
			{
				SegmentedCreature.PathNode value = linkedListNode2.Value;
				SegmentedCreature.PathNode value2 = linkedListNode2.Next.Value;
				linkedListNode.Value.SetPosition(linkedListNode2.Value.position + (linkedListNode2.Next.Value.position - linkedListNode2.Value.position).normalized * num4);
				linkedListNode.Value.SetRotation(Quaternion.Slerp(value.rotation, value2.rotation, num4 / num5));
				num = num4;
			}
			linkedListNode.Value.animController.FlipX = linkedListNode.Previous.Value.Position.x < linkedListNode.Value.Position.x;
			linkedListNode.Value.animController.animScale = smi.baseAnimScale + smi.baseAnimScale * smi.def.compressedMaxScale * ((float)(smi.def.numBodySegments - num3) / (float)smi.def.numBodySegments) * (1f - num2);
			linkedListNode = linkedListNode.Next;
			num3++;
		}
	}

	// Token: 0x060004D3 RID: 1235 RVA: 0x00027988 File Offset: 0x00025B88
	private void DrawDebug(SegmentedCreature.Instance smi, float dt)
	{
		SegmentedCreature.CreatureSegment value = smi.GetHeadSegmentNode().Value;
		DrawUtil.Arrow(value.Position, value.Position + value.Up, 0.05f, Color.red, 0f);
		DrawUtil.Arrow(value.Position, value.Position + value.Forward * 0.06f, 0.05f, Color.cyan, 0f);
		int num = 0;
		foreach (SegmentedCreature.PathNode pathNode in smi.path)
		{
			Color color = Color.HSVToRGB((float)num / (float)smi.def.numPathNodes, 1f, 1f);
			DrawUtil.Gnomon(pathNode.position, 0.05f, Color.cyan, 0f);
			DrawUtil.Arrow(pathNode.position, pathNode.position + pathNode.rotation * Vector3.up * 0.5f, 0.025f, color, 0f);
			num++;
		}
		for (LinkedListNode<SegmentedCreature.CreatureSegment> linkedListNode = smi.segments.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
		{
			DrawUtil.Circle(linkedListNode.Value.Position, 0.05f, Color.white, new Vector3?(Vector3.forward), 0f);
			DrawUtil.Gnomon(linkedListNode.Value.Position, 0.05f, Color.white, 0f);
		}
	}

	// Token: 0x04000372 RID: 882
	public SegmentedCreature.RectractStates retracted;

	// Token: 0x04000373 RID: 883
	public SegmentedCreature.FreeMovementStates freeMovement;

	// Token: 0x04000374 RID: 884
	private StateMachine<SegmentedCreature, SegmentedCreature.Instance, IStateMachineTarget, SegmentedCreature.Def>.BoolParameter isRetracted;

	// Token: 0x02001142 RID: 4418
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x0400625B RID: 25179
		public HashedString segmentTrackerSymbol;

		// Token: 0x0400625C RID: 25180
		public Vector3 headOffset = Vector3.zero;

		// Token: 0x0400625D RID: 25181
		public Vector3 bodyPivot = Vector3.zero;

		// Token: 0x0400625E RID: 25182
		public Vector3 tailPivot = Vector3.zero;

		// Token: 0x0400625F RID: 25183
		public int numBodySegments;

		// Token: 0x04006260 RID: 25184
		public float minSegmentSpacing;

		// Token: 0x04006261 RID: 25185
		public float maxSegmentSpacing;

		// Token: 0x04006262 RID: 25186
		public int numPathNodes;

		// Token: 0x04006263 RID: 25187
		public float pathSpacing;

		// Token: 0x04006264 RID: 25188
		public KAnimFile midAnim;

		// Token: 0x04006265 RID: 25189
		public KAnimFile tailAnim;

		// Token: 0x04006266 RID: 25190
		public string movingAnimName;

		// Token: 0x04006267 RID: 25191
		public string idleAnimName;

		// Token: 0x04006268 RID: 25192
		public float retractionSegmentSpeed = 1f;

		// Token: 0x04006269 RID: 25193
		public float retractionPathSpeed = 1f;

		// Token: 0x0400626A RID: 25194
		public float compressedMaxScale = 1.2f;

		// Token: 0x0400626B RID: 25195
		public int animFrameOffset;

		// Token: 0x0400626C RID: 25196
		public HashSet<HashedString> hideBoddyWhenStartingAnimNames = new HashSet<HashedString> { "rocket_biological" };

		// Token: 0x0400626D RID: 25197
		public HashSet<HashedString> retractWhenStartingAnimNames = new HashSet<HashedString> { "trapped", "trussed", "escape", "drown_pre", "drown_loop", "drown_pst", "rocket_biological" };

		// Token: 0x0400626E RID: 25198
		public HashSet<HashedString> retractWhenEndingAnimNames = new HashSet<HashedString> { "floor_floor_2_0", "grooming_pst", "fall" };
	}

	// Token: 0x02001143 RID: 4419
	public class RectractStates : GameStateMachine<SegmentedCreature, SegmentedCreature.Instance, IStateMachineTarget, SegmentedCreature.Def>.State
	{
		// Token: 0x0400626F RID: 25199
		public GameStateMachine<SegmentedCreature, SegmentedCreature.Instance, IStateMachineTarget, SegmentedCreature.Def>.State pre;

		// Token: 0x04006270 RID: 25200
		public GameStateMachine<SegmentedCreature, SegmentedCreature.Instance, IStateMachineTarget, SegmentedCreature.Def>.State loop;
	}

	// Token: 0x02001144 RID: 4420
	public class FreeMovementStates : GameStateMachine<SegmentedCreature, SegmentedCreature.Instance, IStateMachineTarget, SegmentedCreature.Def>.State
	{
		// Token: 0x04006271 RID: 25201
		public GameStateMachine<SegmentedCreature, SegmentedCreature.Instance, IStateMachineTarget, SegmentedCreature.Def>.State idle;

		// Token: 0x04006272 RID: 25202
		public GameStateMachine<SegmentedCreature, SegmentedCreature.Instance, IStateMachineTarget, SegmentedCreature.Def>.State moving;

		// Token: 0x04006273 RID: 25203
		public GameStateMachine<SegmentedCreature, SegmentedCreature.Instance, IStateMachineTarget, SegmentedCreature.Def>.State layEgg;

		// Token: 0x04006274 RID: 25204
		public GameStateMachine<SegmentedCreature, SegmentedCreature.Instance, IStateMachineTarget, SegmentedCreature.Def>.State poop;

		// Token: 0x04006275 RID: 25205
		public GameStateMachine<SegmentedCreature, SegmentedCreature.Instance, IStateMachineTarget, SegmentedCreature.Def>.State dead;
	}

	// Token: 0x02001145 RID: 4421
	public new class Instance : GameStateMachine<SegmentedCreature, SegmentedCreature.Instance, IStateMachineTarget, SegmentedCreature.Def>.GameInstance
	{
		// Token: 0x060081E8 RID: 33256 RVA: 0x00330174 File Offset: 0x0032E374
		public Instance(IStateMachineTarget master, SegmentedCreature.Def def)
			: base(master, def)
		{
			global::Debug.Assert((float)def.numBodySegments * def.maxSegmentSpacing < (float)def.numPathNodes * def.pathSpacing);
			this.CreateSegments();
		}

		// Token: 0x060081E9 RID: 33257 RVA: 0x003301C8 File Offset: 0x0032E3C8
		private void CreateSegments()
		{
			float num = (float)SegmentedCreature.Instance.creatureBatchSlot * 0.01f;
			SegmentedCreature.Instance.creatureBatchSlot = (SegmentedCreature.Instance.creatureBatchSlot + 1) % 10;
			SegmentedCreature.CreatureSegment value = this.segments.AddFirst(new SegmentedCreature.CreatureSegment(base.GetComponent<KBatchedAnimController>(), base.gameObject, num, base.smi.def.headOffset, Vector3.zero)).Value;
			base.gameObject.SetActive(false);
			value.animController = base.GetComponent<KBatchedAnimController>();
			value.animController.SetSymbolVisiblity(base.smi.def.segmentTrackerSymbol, false);
			value.symbol = base.smi.def.segmentTrackerSymbol;
			value.SetPosition(base.transform.position);
			base.gameObject.SetActive(true);
			this.baseAnimScale = value.animController.animScale;
			value.animController.onAnimEnter += this.AnimEntered;
			value.animController.onAnimComplete += this.AnimComplete;
			for (int i = 0; i < base.def.numBodySegments; i++)
			{
				GameObject gameObject = new GameObject(base.gameObject.GetProperName() + string.Format(" Segment {0}", i));
				gameObject.SetActive(false);
				gameObject.transform.parent = base.transform;
				gameObject.transform.position = value.Position;
				KAnimFile kanimFile = base.def.midAnim;
				Vector3 vector = base.def.bodyPivot;
				if (i == base.def.numBodySegments - 1)
				{
					kanimFile = base.def.tailAnim;
					vector = base.def.tailPivot;
				}
				KBatchedAnimController kbatchedAnimController = gameObject.AddOrGet<KBatchedAnimController>();
				kbatchedAnimController.AnimFiles = new KAnimFile[] { kanimFile };
				kbatchedAnimController.isMovable = true;
				kbatchedAnimController.SetSymbolVisiblity(base.smi.def.segmentTrackerSymbol, false);
				kbatchedAnimController.sceneLayer = value.animController.sceneLayer;
				SegmentedCreature.CreatureSegment creatureSegment = new SegmentedCreature.CreatureSegment(value.animController, gameObject, num + (float)(i + 1) * 0.0001f, Vector3.zero, vector);
				creatureSegment.animController = kbatchedAnimController;
				creatureSegment.symbol = base.smi.def.segmentTrackerSymbol;
				creatureSegment.distanceToPreviousSegment = base.smi.def.minSegmentSpacing;
				creatureSegment.animLink = new KAnimLink(value.animController, kbatchedAnimController);
				this.segments.AddLast(creatureSegment);
				gameObject.SetActive(true);
			}
			for (int j = 0; j < base.def.numPathNodes; j++)
			{
				this.path.AddLast(new SegmentedCreature.PathNode(value.Position));
			}
		}

		// Token: 0x060081EA RID: 33258 RVA: 0x00330488 File Offset: 0x0032E688
		public void AnimEntered(HashedString name)
		{
			if (base.smi.def.retractWhenStartingAnimNames.Contains(name))
			{
				base.smi.sm.isRetracted.Set(true, base.smi, false);
			}
			else
			{
				base.smi.sm.isRetracted.Set(false, base.smi, false);
			}
			if (base.smi.def.hideBoddyWhenStartingAnimNames.Contains(name))
			{
				this.SetBodySegmentsVisibility(false);
				return;
			}
			this.SetBodySegmentsVisibility(true);
		}

		// Token: 0x060081EB RID: 33259 RVA: 0x00330514 File Offset: 0x0032E714
		public void SetBodySegmentsVisibility(bool visible)
		{
			for (LinkedListNode<SegmentedCreature.CreatureSegment> linkedListNode = base.smi.GetFirstBodySegmentNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				linkedListNode.Value.animController.SetVisiblity(visible);
			}
		}

		// Token: 0x060081EC RID: 33260 RVA: 0x0033054A File Offset: 0x0032E74A
		public void AnimComplete(HashedString name)
		{
			if (base.smi.def.retractWhenEndingAnimNames.Contains(name))
			{
				base.smi.sm.isRetracted.Set(true, base.smi, false);
			}
		}

		// Token: 0x060081ED RID: 33261 RVA: 0x00330582 File Offset: 0x0032E782
		public LinkedListNode<SegmentedCreature.CreatureSegment> GetHeadSegmentNode()
		{
			return base.smi.segments.First;
		}

		// Token: 0x060081EE RID: 33262 RVA: 0x00330594 File Offset: 0x0032E794
		public LinkedListNode<SegmentedCreature.CreatureSegment> GetFirstBodySegmentNode()
		{
			return base.smi.segments.First.Next;
		}

		// Token: 0x060081EF RID: 33263 RVA: 0x003305AC File Offset: 0x0032E7AC
		public float LengthPercentage()
		{
			float num = 0f;
			for (LinkedListNode<SegmentedCreature.CreatureSegment> linkedListNode = this.GetFirstBodySegmentNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				num += linkedListNode.Value.distanceToPreviousSegment;
			}
			float num2 = this.MinLength();
			float num3 = this.MaxLength();
			return Mathf.Clamp(num - num2, 0f, num3) / (num3 - num2);
		}

		// Token: 0x060081F0 RID: 33264 RVA: 0x00330600 File Offset: 0x0032E800
		public float MinLength()
		{
			return base.smi.def.minSegmentSpacing * (float)base.smi.def.numBodySegments;
		}

		// Token: 0x060081F1 RID: 33265 RVA: 0x00330624 File Offset: 0x0032E824
		public float MaxLength()
		{
			return base.smi.def.maxSegmentSpacing * (float)base.smi.def.numBodySegments;
		}

		// Token: 0x060081F2 RID: 33266 RVA: 0x00330648 File Offset: 0x0032E848
		protected override void OnCleanUp()
		{
			this.GetHeadSegmentNode().Value.animController.onAnimEnter -= this.AnimEntered;
			this.GetHeadSegmentNode().Value.animController.onAnimComplete -= this.AnimComplete;
			for (LinkedListNode<SegmentedCreature.CreatureSegment> linkedListNode = this.GetFirstBodySegmentNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				linkedListNode.Value.CleanUp();
			}
		}

		// Token: 0x04006276 RID: 25206
		private const int NUM_CREATURE_SLOTS = 10;

		// Token: 0x04006277 RID: 25207
		private static int creatureBatchSlot;

		// Token: 0x04006278 RID: 25208
		public float baseAnimScale;

		// Token: 0x04006279 RID: 25209
		public Vector3 previousHeadPosition;

		// Token: 0x0400627A RID: 25210
		public float previousDist;

		// Token: 0x0400627B RID: 25211
		public LinkedList<SegmentedCreature.PathNode> path = new LinkedList<SegmentedCreature.PathNode>();

		// Token: 0x0400627C RID: 25212
		public LinkedList<SegmentedCreature.CreatureSegment> segments = new LinkedList<SegmentedCreature.CreatureSegment>();
	}

	// Token: 0x02001146 RID: 4422
	public class PathNode
	{
		// Token: 0x060081F3 RID: 33267 RVA: 0x003306B5 File Offset: 0x0032E8B5
		public PathNode(Vector3 position)
		{
			this.position = position;
			this.rotation = Quaternion.identity;
		}

		// Token: 0x0400627D RID: 25213
		public Vector3 position;

		// Token: 0x0400627E RID: 25214
		public Quaternion rotation;
	}

	// Token: 0x02001147 RID: 4423
	public class CreatureSegment
	{
		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x060081F4 RID: 33268 RVA: 0x003306CF File Offset: 0x0032E8CF
		public float ZOffset
		{
			get
			{
				return Grid.GetLayerZ(this.head.sceneLayer) + this.zRelativeOffset;
			}
		}

		// Token: 0x060081F5 RID: 33269 RVA: 0x003306E8 File Offset: 0x0032E8E8
		public CreatureSegment(KBatchedAnimController head, GameObject go, float zRelativeOffset, Vector3 offset, Vector3 pivot)
		{
			this.head = head;
			this.m_transform = go.transform;
			this.zRelativeOffset = zRelativeOffset;
			this.offset = offset;
			this.pivot = pivot;
			this.SetPosition(go.transform.position);
		}

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x060081F6 RID: 33270 RVA: 0x00330738 File Offset: 0x0032E938
		public Vector3 Position
		{
			get
			{
				Vector3 vector = this.offset;
				vector.x *= (float)(this.animController.FlipX ? (-1) : 1);
				if (vector != Vector3.zero)
				{
					vector = this.Rotation * vector;
				}
				if (this.symbol.IsValid)
				{
					bool flag;
					Vector3 vector2 = this.animController.GetSymbolTransform(this.symbol, out flag).GetColumn(3);
					vector2.z = this.ZOffset;
					return vector2 + vector;
				}
				return this.m_transform.position + vector;
			}
		}

		// Token: 0x060081F7 RID: 33271 RVA: 0x003307DC File Offset: 0x0032E9DC
		public void SetPosition(Vector3 value)
		{
			bool flag = false;
			if (this.animController != null && this.animController.sceneLayer != this.head.sceneLayer)
			{
				this.animController.SetSceneLayer(this.head.sceneLayer);
				flag = true;
			}
			value.z = this.ZOffset;
			this.m_transform.position = value;
			if (flag)
			{
				this.animController.enabled = false;
				this.animController.enabled = true;
			}
		}

		// Token: 0x060081F8 RID: 33272 RVA: 0x0033085D File Offset: 0x0032EA5D
		public void SetRotation(Quaternion rotation)
		{
			this.m_transform.rotation = rotation;
		}

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x060081F9 RID: 33273 RVA: 0x0033086C File Offset: 0x0032EA6C
		public Quaternion Rotation
		{
			get
			{
				if (this.symbol.IsValid)
				{
					bool flag;
					Vector3 vector = this.animController.GetSymbolLocalTransform(this.symbol, out flag).MultiplyVector(Vector3.right);
					if (!this.animController.FlipX)
					{
						vector.y *= -1f;
					}
					return Quaternion.FromToRotation(Vector3.right, vector);
				}
				return this.m_transform.rotation;
			}
		}

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x060081FA RID: 33274 RVA: 0x003308DB File Offset: 0x0032EADB
		public Vector3 Forward
		{
			get
			{
				return this.Rotation * (this.animController.FlipX ? Vector3.left : Vector3.right);
			}
		}

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x060081FB RID: 33275 RVA: 0x00330901 File Offset: 0x0032EB01
		public Vector3 Up
		{
			get
			{
				return this.Rotation * Vector3.up;
			}
		}

		// Token: 0x060081FC RID: 33276 RVA: 0x00330913 File Offset: 0x0032EB13
		public void CleanUp()
		{
			global::UnityEngine.Object.Destroy(this.m_transform.gameObject);
		}

		// Token: 0x0400627F RID: 25215
		public KBatchedAnimController animController;

		// Token: 0x04006280 RID: 25216
		public KAnimLink animLink;

		// Token: 0x04006281 RID: 25217
		public float distanceToPreviousSegment;

		// Token: 0x04006282 RID: 25218
		public HashedString symbol;

		// Token: 0x04006283 RID: 25219
		public Vector3 offset;

		// Token: 0x04006284 RID: 25220
		public Vector3 pivot;

		// Token: 0x04006285 RID: 25221
		public KBatchedAnimController head;

		// Token: 0x04006286 RID: 25222
		private float zRelativeOffset;

		// Token: 0x04006287 RID: 25223
		private Transform m_transform;
	}
}
