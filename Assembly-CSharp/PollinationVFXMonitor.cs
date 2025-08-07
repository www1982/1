using System;
using Klei.AI;
using UnityEngine;

// Token: 0x0200059F RID: 1439
public class PollinationVFXMonitor : GameStateMachine<PollinationVFXMonitor, PollinationVFXMonitor.Instance, IStateMachineTarget, PollinationVFXMonitor.Def>
{
	// Token: 0x060020DC RID: 8412 RVA: 0x000BDA3C File Offset: 0x000BBC3C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.idle;
		this.idle.EventTransition(GameHashes.EffectAdded, this.pollinated, new StateMachine<PollinationVFXMonitor, PollinationVFXMonitor.Instance, IStateMachineTarget, PollinationVFXMonitor.Def>.Transition.ConditionCallback(PollinationVFXMonitor.IsPollinated));
		this.pollinated.EventTransition(GameHashes.EffectRemoved, this.idle, GameStateMachine<PollinationVFXMonitor, PollinationVFXMonitor.Instance, IStateMachineTarget, PollinationVFXMonitor.Def>.Not(new StateMachine<PollinationVFXMonitor, PollinationVFXMonitor.Instance, IStateMachineTarget, PollinationVFXMonitor.Def>.Transition.ConditionCallback(PollinationVFXMonitor.IsPollinated))).Toggle("Toggle Pollination VFX", new StateMachine<PollinationVFXMonitor, PollinationVFXMonitor.Instance, IStateMachineTarget, PollinationVFXMonitor.Def>.State.Callback(PollinationVFXMonitor.CreatePollinationEffect), new StateMachine<PollinationVFXMonitor, PollinationVFXMonitor.Instance, IStateMachineTarget, PollinationVFXMonitor.Def>.State.Callback(PollinationVFXMonitor.DestroyPollinationEffect));
	}

	// Token: 0x060020DD RID: 8413 RVA: 0x000BDAC5 File Offset: 0x000BBCC5
	private static bool IsPollinated(PollinationVFXMonitor.Instance smi)
	{
		return smi.IsPollinated();
	}

	// Token: 0x060020DE RID: 8414 RVA: 0x000BDACD File Offset: 0x000BBCCD
	private static void DestroyPollinationEffect(PollinationVFXMonitor.Instance smi)
	{
		smi.DestroyPollinationEffect();
	}

	// Token: 0x060020DF RID: 8415 RVA: 0x000BDAD5 File Offset: 0x000BBCD5
	private static void CreatePollinationEffect(PollinationVFXMonitor.Instance smi)
	{
		smi.CreatePollinationEffect();
	}

	// Token: 0x04001321 RID: 4897
	private GameStateMachine<PollinationVFXMonitor, PollinationVFXMonitor.Instance, IStateMachineTarget, PollinationVFXMonitor.Def>.State idle;

	// Token: 0x04001322 RID: 4898
	private GameStateMachine<PollinationVFXMonitor, PollinationVFXMonitor.Instance, IStateMachineTarget, PollinationVFXMonitor.Def>.State pollinated;

	// Token: 0x02001421 RID: 5153
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001422 RID: 5154
	public new class Instance : GameStateMachine<PollinationVFXMonitor, PollinationVFXMonitor.Instance, IStateMachineTarget, PollinationVFXMonitor.Def>.GameInstance
	{
		// Token: 0x06008C9C RID: 35996 RVA: 0x0035654D File Offset: 0x0035474D
		public Instance(IStateMachineTarget master, PollinationVFXMonitor.Def def)
			: base(master, def)
		{
			this.effects = base.GetComponent<Effects>();
			this.occupyArea = base.GetComponent<OccupyArea>();
		}

		// Token: 0x06008C9D RID: 35997 RVA: 0x0035656F File Offset: 0x0035476F
		public override void StartSM()
		{
			this.isHangingPlant = base.gameObject.HasTag(GameTags.Hanging);
			base.StartSM();
		}

		// Token: 0x06008C9E RID: 35998 RVA: 0x00356590 File Offset: 0x00354790
		public bool IsPollinated()
		{
			if (this.effects == null)
			{
				return false;
			}
			foreach (HashedString hashedString in PollinationMonitor.PollinationEffects)
			{
				if (this.effects.HasEffect(hashedString))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06008C9F RID: 35999 RVA: 0x003565DC File Offset: 0x003547DC
		public void CreatePollinationEffect()
		{
			this.DestroyPollinationEffect();
			Vector4 vector = new Vector4(float.MaxValue, float.MinValue, float.MaxValue, float.MinValue);
			foreach (CellOffset cellOffset in this.occupyArea.OccupiedCellsOffsets)
			{
				if ((float)cellOffset.x < vector.x)
				{
					vector.x = (float)cellOffset.x;
				}
				if ((float)cellOffset.x > vector.y)
				{
					vector.y = (float)cellOffset.x;
				}
				if ((float)cellOffset.y < vector.z)
				{
					vector.z = (float)cellOffset.y;
				}
				if ((float)cellOffset.y > vector.w)
				{
					vector.w = (float)cellOffset.y;
				}
			}
			int num = 1 + (int)Mathf.Clamp(vector.y - vector.x, 0f, 2.1474836E+09f);
			int num2 = 1 + (int)Mathf.Clamp(vector.w - vector.z, 0f, 2.1474836E+09f);
			Vector3 vector2 = Grid.CellToPosCBC(this.occupyArea.GetOffsetCellWithRotation(new CellOffset(0, this.isHangingPlant ? (-num2 + 1) : 0)), Grid.SceneLayer.BuildingFront);
			GameObject gameObject = Util.KInstantiate(EffectPrefabs.Instance.PlantPollinated, vector2, Quaternion.identity, base.gameObject, "PollinationVFX", true, 0);
			this.pollinationEffect = gameObject.GetComponent<ParticleSystem>();
			ParticleSystem.ShapeModule shape = this.pollinationEffect.shape;
			Vector3 scale = shape.scale;
			Vector3 position = shape.position;
			scale.x = (float)num;
			scale.y = (float)num2;
			position.y = (float)num2 * 0.5f;
			shape.scale = scale;
			shape.position = position;
		}

		// Token: 0x06008CA0 RID: 36000 RVA: 0x003567A2 File Offset: 0x003549A2
		public void DestroyPollinationEffect()
		{
			if (this.pollinationEffect != null)
			{
				this.pollinationEffect.DeleteObject();
				this.pollinationEffect = null;
			}
		}

		// Token: 0x04006BBF RID: 27583
		private Effects effects;

		// Token: 0x04006BC0 RID: 27584
		private ParticleSystem pollinationEffect;

		// Token: 0x04006BC1 RID: 27585
		private OccupyArea occupyArea;

		// Token: 0x04006BC2 RID: 27586
		private bool isHangingPlant;
	}
}
