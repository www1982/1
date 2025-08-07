using System;
using Klei;
using UnityEngine;

// Token: 0x020006EB RID: 1771
public class Chlorinator : GameStateMachine<Chlorinator, Chlorinator.StatesInstance, IStateMachineTarget, Chlorinator.Def>
{
	// Token: 0x06002C0E RID: 11278 RVA: 0x000FD808 File Offset: 0x000FBA08
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.inoperational;
		this.inoperational.TagTransition(GameTags.Operational, this.ready, false);
		this.ready.TagTransition(GameTags.Operational, this.inoperational, true).DefaultState(this.ready.idle);
		this.ready.idle.EventTransition(GameHashes.OnStorageChange, this.ready.wait, (Chlorinator.StatesInstance smi) => smi.CanEmit()).EnterTransition(this.ready.wait, (Chlorinator.StatesInstance smi) => smi.CanEmit()).Target(this.hopper)
			.PlayAnim("hopper_idle_loop");
		this.ready.wait.ScheduleGoTo(new Func<Chlorinator.StatesInstance, float>(Chlorinator.GetPoppingDelay), this.ready.popPre).EnterTransition(this.ready.idle, (Chlorinator.StatesInstance smi) => !smi.CanEmit()).Target(this.hopper)
			.PlayAnim("hopper_idle_loop");
		this.ready.popPre.Target(this.hopper).PlayAnim("meter_hopper_pre").OnAnimQueueComplete(this.ready.pop);
		this.ready.pop.Enter(delegate(Chlorinator.StatesInstance smi)
		{
			smi.TryEmit();
		}).Target(this.hopper).PlayAnim("meter_hopper_loop")
			.OnAnimQueueComplete(this.ready.popPst);
		this.ready.popPst.Target(this.hopper).PlayAnim("meter_hopper_pst").OnAnimQueueComplete(this.ready.wait);
	}

	// Token: 0x06002C0F RID: 11279 RVA: 0x000FDA04 File Offset: 0x000FBC04
	public static float GetPoppingDelay(Chlorinator.StatesInstance smi)
	{
		return smi.def.popWaitRange.Get();
	}

	// Token: 0x04001A06 RID: 6662
	private GameStateMachine<Chlorinator, Chlorinator.StatesInstance, IStateMachineTarget, Chlorinator.Def>.State inoperational;

	// Token: 0x04001A07 RID: 6663
	private Chlorinator.ReadyStates ready;

	// Token: 0x04001A08 RID: 6664
	public StateMachine<Chlorinator, Chlorinator.StatesInstance, IStateMachineTarget, Chlorinator.Def>.TargetParameter hopper;

	// Token: 0x02001577 RID: 5495
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04006FBC RID: 28604
		public MathUtil.MinMax popWaitRange = new MathUtil.MinMax(0.2f, 0.8f);

		// Token: 0x04006FBD RID: 28605
		public Tag primaryOreTag;

		// Token: 0x04006FBE RID: 28606
		public float primaryOreMassPerOre;

		// Token: 0x04006FBF RID: 28607
		public MathUtil.MinMaxInt primaryOreCount = new MathUtil.MinMaxInt(1, 1);

		// Token: 0x04006FC0 RID: 28608
		public Tag secondaryOreTag;

		// Token: 0x04006FC1 RID: 28609
		public float secondaryOreMassPerOre;

		// Token: 0x04006FC2 RID: 28610
		public MathUtil.MinMaxInt secondaryOreCount = new MathUtil.MinMaxInt(1, 1);

		// Token: 0x04006FC3 RID: 28611
		public Vector3 offset = Vector3.zero;

		// Token: 0x04006FC4 RID: 28612
		public MathUtil.MinMax initialVelocity = new MathUtil.MinMax(1f, 3f);

		// Token: 0x04006FC5 RID: 28613
		public MathUtil.MinMax initialDirectionHalfAngleDegreesRange = new MathUtil.MinMax(160f, 20f);
	}

	// Token: 0x02001578 RID: 5496
	public class ReadyStates : GameStateMachine<Chlorinator, Chlorinator.StatesInstance, IStateMachineTarget, Chlorinator.Def>.State
	{
		// Token: 0x04006FC6 RID: 28614
		public GameStateMachine<Chlorinator, Chlorinator.StatesInstance, IStateMachineTarget, Chlorinator.Def>.State idle;

		// Token: 0x04006FC7 RID: 28615
		public GameStateMachine<Chlorinator, Chlorinator.StatesInstance, IStateMachineTarget, Chlorinator.Def>.State wait;

		// Token: 0x04006FC8 RID: 28616
		public GameStateMachine<Chlorinator, Chlorinator.StatesInstance, IStateMachineTarget, Chlorinator.Def>.State popPre;

		// Token: 0x04006FC9 RID: 28617
		public GameStateMachine<Chlorinator, Chlorinator.StatesInstance, IStateMachineTarget, Chlorinator.Def>.State pop;

		// Token: 0x04006FCA RID: 28618
		public GameStateMachine<Chlorinator, Chlorinator.StatesInstance, IStateMachineTarget, Chlorinator.Def>.State popPst;
	}

	// Token: 0x02001579 RID: 5497
	public class StatesInstance : GameStateMachine<Chlorinator, Chlorinator.StatesInstance, IStateMachineTarget, Chlorinator.Def>.GameInstance
	{
		// Token: 0x06009152 RID: 37202 RVA: 0x00363374 File Offset: 0x00361574
		public StatesInstance(IStateMachineTarget master, Chlorinator.Def def)
			: base(master, def)
		{
			this.storage = base.GetComponent<ComplexFabricator>().outStorage;
			KAnimControllerBase component = master.GetComponent<KAnimControllerBase>();
			this.hopperMeter = new MeterController(component, "meter_target", "meter_hopper_pre", Meter.Offset.Behind, Grid.SceneLayer.NoLayer, new string[] { "meter_target" });
			base.sm.hopper.Set(this.hopperMeter.gameObject, this, false);
		}

		// Token: 0x06009153 RID: 37203 RVA: 0x003633E6 File Offset: 0x003615E6
		public bool CanEmit()
		{
			return !this.storage.IsEmpty();
		}

		// Token: 0x06009154 RID: 37204 RVA: 0x003633F8 File Offset: 0x003615F8
		public void TryEmit()
		{
			this.TryEmit(base.smi.def.primaryOreCount.Get(), base.def.primaryOreTag, base.def.primaryOreMassPerOre);
			this.TryEmit(base.smi.def.secondaryOreCount.Get(), base.def.secondaryOreTag, base.def.secondaryOreMassPerOre);
		}

		// Token: 0x06009155 RID: 37205 RVA: 0x00363468 File Offset: 0x00361668
		private void TryEmit(int oreSpawnCount, Tag emitTag, float amount)
		{
			GameObject gameObject = this.storage.FindFirst(emitTag);
			if (gameObject == null)
			{
				return;
			}
			PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
			Substance substance = component.Element.substance;
			float num;
			SimUtil.DiseaseInfo diseaseInfo;
			float num2;
			this.storage.ConsumeAndGetDisease(emitTag, amount, out num, out diseaseInfo, out num2);
			if (num <= 0f)
			{
				return;
			}
			float num3 = num * component.MassPerUnit / (float)oreSpawnCount;
			Vector3 vector = base.smi.gameObject.transform.position;
			vector += base.def.offset;
			bool flag = global::UnityEngine.Random.value >= 0.5f;
			for (int i = 0; i < oreSpawnCount; i++)
			{
				float num4 = base.def.initialDirectionHalfAngleDegreesRange.Get() * 3.1415927f / 180f;
				Vector2 normalized = new Vector2(-Mathf.Cos(num4), Mathf.Sin(num4));
				if (flag)
				{
					normalized.x = -normalized.x;
				}
				flag = !flag;
				normalized = normalized.normalized;
				Vector3 vector2 = normalized * base.def.initialVelocity.Get();
				Vector3 vector3 = vector;
				vector3 += normalized * 0.1f;
				GameObject gameObject2 = substance.SpawnResource(vector3, num3, num2, diseaseInfo.idx, diseaseInfo.count / oreSpawnCount, false, false, false);
				KFMOD.PlayOneShot(GlobalAssets.GetSound("Chlorinator_popping", false), CameraController.Instance.GetVerticallyScaledPosition(vector3, false), 1f);
				if (GameComps.Fallers.Has(gameObject2))
				{
					GameComps.Fallers.Remove(gameObject2);
				}
				GameComps.Fallers.Add(gameObject2, vector2);
			}
		}

		// Token: 0x04006FCB RID: 28619
		public Storage storage;

		// Token: 0x04006FCC RID: 28620
		public MeterController hopperMeter;
	}
}
