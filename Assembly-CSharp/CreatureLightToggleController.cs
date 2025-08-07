using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000859 RID: 2137
public class CreatureLightToggleController : GameStateMachine<CreatureLightToggleController, CreatureLightToggleController.Instance, IStateMachineTarget, CreatureLightToggleController.Def>
{
	// Token: 0x06003AB2 RID: 15026 RVA: 0x00146400 File Offset: 0x00144600
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.light_off;
		base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
		this.light_off.Enter(delegate(CreatureLightToggleController.Instance smi)
		{
			smi.SwitchLight(false);
		}).EventHandlerTransition(GameHashes.TagsChanged, this.turning_on, new Func<CreatureLightToggleController.Instance, object, bool>(CreatureLightToggleController.ShouldProduceLight));
		this.turning_off.BatchUpdate(delegate(List<UpdateBucketWithUpdater<CreatureLightToggleController.Instance>.Entry> instances, float time_delta)
		{
			CreatureLightToggleController.Instance.ModifyBrightness(instances, CreatureLightToggleController.Instance.dim, time_delta);
		}, UpdateRate.SIM_200ms).Transition(this.light_off, (CreatureLightToggleController.Instance smi) => smi.IsOff(), UpdateRate.SIM_200ms);
		this.light_on.Enter(delegate(CreatureLightToggleController.Instance smi)
		{
			smi.SwitchLight(true);
		}).EventHandlerTransition(GameHashes.TagsChanged, this.turning_off, (CreatureLightToggleController.Instance smi, object obj) => !CreatureLightToggleController.ShouldProduceLight(smi, obj));
		this.turning_on.Enter(delegate(CreatureLightToggleController.Instance smi)
		{
			smi.SwitchLight(true);
		}).BatchUpdate(delegate(List<UpdateBucketWithUpdater<CreatureLightToggleController.Instance>.Entry> instances, float time_delta)
		{
			CreatureLightToggleController.Instance.ModifyBrightness(instances, CreatureLightToggleController.Instance.brighten, time_delta);
		}, UpdateRate.SIM_200ms).Transition(this.light_on, (CreatureLightToggleController.Instance smi) => smi.IsOn(), UpdateRate.SIM_200ms);
	}

	// Token: 0x06003AB3 RID: 15027 RVA: 0x0014658F File Offset: 0x0014478F
	public static bool ShouldProduceLight(CreatureLightToggleController.Instance smi, object obj)
	{
		return !smi.prefabID.HasTag(GameTags.Creatures.Overcrowded) && !smi.prefabID.HasTag(GameTags.Creatures.TrappedInCargoBay);
	}

	// Token: 0x040023F7 RID: 9207
	private GameStateMachine<CreatureLightToggleController, CreatureLightToggleController.Instance, IStateMachineTarget, CreatureLightToggleController.Def>.State light_off;

	// Token: 0x040023F8 RID: 9208
	private GameStateMachine<CreatureLightToggleController, CreatureLightToggleController.Instance, IStateMachineTarget, CreatureLightToggleController.Def>.State turning_off;

	// Token: 0x040023F9 RID: 9209
	private GameStateMachine<CreatureLightToggleController, CreatureLightToggleController.Instance, IStateMachineTarget, CreatureLightToggleController.Def>.State light_on;

	// Token: 0x040023FA RID: 9210
	private GameStateMachine<CreatureLightToggleController, CreatureLightToggleController.Instance, IStateMachineTarget, CreatureLightToggleController.Def>.State turning_on;

	// Token: 0x020017E2 RID: 6114
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x020017E3 RID: 6115
	public new class Instance : GameStateMachine<CreatureLightToggleController, CreatureLightToggleController.Instance, IStateMachineTarget, CreatureLightToggleController.Def>.GameInstance
	{
		// Token: 0x06009AAE RID: 39598 RVA: 0x0038AFDC File Offset: 0x003891DC
		public Instance(IStateMachineTarget master, CreatureLightToggleController.Def def)
			: base(master, def)
		{
			this.prefabID = base.gameObject.GetComponent<KPrefabID>();
			this.light = master.GetComponent<Light2D>();
			this.originalLux = this.light.Lux;
			this.originalRange = this.light.Range;
		}

		// Token: 0x06009AAF RID: 39599 RVA: 0x0038B030 File Offset: 0x00389230
		public void SwitchLight(bool on)
		{
			this.light.enabled = on;
		}

		// Token: 0x06009AB0 RID: 39600 RVA: 0x0038B040 File Offset: 0x00389240
		public static void ModifyBrightness(List<UpdateBucketWithUpdater<CreatureLightToggleController.Instance>.Entry> instances, CreatureLightToggleController.Instance.ModifyLuxDelegate modify_lux, float time_delta)
		{
			CreatureLightToggleController.Instance.modify_brightness_job.Reset(null);
			for (int num = 0; num != instances.Count; num++)
			{
				UpdateBucketWithUpdater<CreatureLightToggleController.Instance>.Entry entry = instances[num];
				entry.lastUpdateTime = 0f;
				instances[num] = entry;
				CreatureLightToggleController.Instance data = entry.data;
				modify_lux(data, time_delta);
				data.light.Range = data.originalRange * (float)data.light.Lux / (float)data.originalLux;
				data.light.RefreshShapeAndPosition();
				if (data.light.RefreshShapeAndPosition() != Light2D.RefreshResult.None)
				{
					CreatureLightToggleController.Instance.modify_brightness_job.Add(new CreatureLightToggleController.Instance.ModifyBrightnessTask(data.light.emitter));
				}
			}
			GlobalJobManager.Run(CreatureLightToggleController.Instance.modify_brightness_job);
			for (int num2 = 0; num2 != CreatureLightToggleController.Instance.modify_brightness_job.Count; num2++)
			{
				CreatureLightToggleController.Instance.modify_brightness_job.GetWorkItem(num2).Finish();
			}
			CreatureLightToggleController.Instance.modify_brightness_job.Reset(null);
		}

		// Token: 0x06009AB1 RID: 39601 RVA: 0x0038B131 File Offset: 0x00389331
		public bool IsOff()
		{
			return this.light.Lux == 0;
		}

		// Token: 0x06009AB2 RID: 39602 RVA: 0x0038B141 File Offset: 0x00389341
		public bool IsOn()
		{
			return this.light.Lux >= this.originalLux;
		}

		// Token: 0x04007733 RID: 30515
		private const float DIM_TIME = 25f;

		// Token: 0x04007734 RID: 30516
		private const float GLOW_TIME = 15f;

		// Token: 0x04007735 RID: 30517
		private int originalLux;

		// Token: 0x04007736 RID: 30518
		private float originalRange;

		// Token: 0x04007737 RID: 30519
		private Light2D light;

		// Token: 0x04007738 RID: 30520
		public KPrefabID prefabID;

		// Token: 0x04007739 RID: 30521
		private static WorkItemCollection<CreatureLightToggleController.Instance.ModifyBrightnessTask, object> modify_brightness_job = new WorkItemCollection<CreatureLightToggleController.Instance.ModifyBrightnessTask, object>();

		// Token: 0x0400773A RID: 30522
		public static CreatureLightToggleController.Instance.ModifyLuxDelegate dim = delegate(CreatureLightToggleController.Instance instance, float time_delta)
		{
			float num = (float)instance.originalLux / 25f;
			instance.light.Lux = Mathf.FloorToInt(Mathf.Max(0f, (float)instance.light.Lux - num * time_delta));
		};

		// Token: 0x0400773B RID: 30523
		public static CreatureLightToggleController.Instance.ModifyLuxDelegate brighten = delegate(CreatureLightToggleController.Instance instance, float time_delta)
		{
			float num2 = (float)instance.originalLux / 15f;
			instance.light.Lux = Mathf.CeilToInt(Mathf.Min((float)instance.originalLux, (float)instance.light.Lux + num2 * time_delta));
		};

		// Token: 0x0200281A RID: 10266
		private struct ModifyBrightnessTask : IWorkItem<object>
		{
			// Token: 0x0600CAB3 RID: 51891 RVA: 0x00419146 File Offset: 0x00417346
			public ModifyBrightnessTask(LightGridManager.LightGridEmitter emitter)
			{
				this.emitter = emitter;
				emitter.RemoveFromGrid();
			}

			// Token: 0x0600CAB4 RID: 51892 RVA: 0x00419155 File Offset: 0x00417355
			public void Run(object context, int threadIndex)
			{
				this.emitter.UpdateLitCells();
			}

			// Token: 0x0600CAB5 RID: 51893 RVA: 0x00419162 File Offset: 0x00417362
			public void Finish()
			{
				this.emitter.AddToGrid(false);
			}

			// Token: 0x0400B1CE RID: 45518
			private LightGridManager.LightGridEmitter emitter;
		}

		// Token: 0x0200281B RID: 10267
		// (Invoke) Token: 0x0600CAB7 RID: 51895
		public delegate void ModifyLuxDelegate(CreatureLightToggleController.Instance instance, float time_delta);
	}
}
