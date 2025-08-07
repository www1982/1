using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000952 RID: 2386
[SerializationConfig(MemberSerialization.OptIn)]
public class HighEnergyParticle : StateMachineComponent<HighEnergyParticle.StatesInstance>
{
	// Token: 0x06004475 RID: 17525 RVA: 0x00189762 File Offset: 0x00187962
	protected override void OnPrefabInit()
	{
		this.loopingSounds = base.gameObject.GetComponent<LoopingSounds>();
		this.flyingSound = GlobalAssets.GetSound("Radbolt_travel_LP", false);
		base.OnPrefabInit();
	}

	// Token: 0x06004476 RID: 17526 RVA: 0x0018978C File Offset: 0x0018798C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Components.HighEnergyParticles.Add(this);
		base.GetComponent<KSelectable>().AddStatusItem(Db.Get().MiscStatusItems.HighEnergyParticleCount, base.gameObject);
		this.emitter.SetEmitting(false);
		this.emitter.Refresh();
		this.SetDirection(this.direction);
		base.gameObject.layer = LayerMask.NameToLayer("PlaceWithDepth");
		this.StartLoopingSound();
		base.smi.StartSM();
	}

	// Token: 0x06004477 RID: 17527 RVA: 0x00189814 File Offset: 0x00187A14
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		this.StopLoopingSound();
		Components.HighEnergyParticles.Remove(this);
		if (this.capturedBy != null && this.capturedBy.currentParticle == this)
		{
			this.capturedBy.currentParticle = null;
		}
	}

	// Token: 0x06004478 RID: 17528 RVA: 0x00189868 File Offset: 0x00187A68
	public void SetDirection(EightDirection direction)
	{
		this.direction = direction;
		float angle = EightDirectionUtil.GetAngle(direction);
		base.smi.master.transform.rotation = Quaternion.Euler(0f, 0f, angle);
	}

	// Token: 0x06004479 RID: 17529 RVA: 0x001898A8 File Offset: 0x00187AA8
	public void Collide(HighEnergyParticle.CollisionType collisionType)
	{
		this.collision = collisionType;
		GameObject gameObject = new GameObject("HEPcollideFX");
		gameObject.SetActive(false);
		gameObject.transform.SetPosition(Grid.CellToPosCCC(Grid.PosToCell(base.smi.master.transform.position), Grid.SceneLayer.FXFront));
		KBatchedAnimController fxAnim = gameObject.AddComponent<KBatchedAnimController>();
		fxAnim.AnimFiles = new KAnimFile[] { Assets.GetAnim("hep_impact_kanim") };
		fxAnim.initialAnim = "graze";
		gameObject.SetActive(true);
		switch (collisionType)
		{
		case HighEnergyParticle.CollisionType.Captured:
			fxAnim.Play("full", KAnim.PlayMode.Once, 1f, 0f);
			break;
		case HighEnergyParticle.CollisionType.CaptureAndRelease:
			fxAnim.Play("partial", KAnim.PlayMode.Once, 1f, 0f);
			break;
		case HighEnergyParticle.CollisionType.PassThrough:
			fxAnim.Play("graze", KAnim.PlayMode.Once, 1f, 0f);
			break;
		}
		fxAnim.onAnimComplete += delegate(HashedString arg)
		{
			Util.KDestroyGameObject(fxAnim);
		};
		if (collisionType == HighEnergyParticle.CollisionType.PassThrough)
		{
			this.collision = HighEnergyParticle.CollisionType.None;
			return;
		}
		base.smi.sm.destroySignal.Trigger(base.smi);
	}

	// Token: 0x0600447A RID: 17530 RVA: 0x00189A03 File Offset: 0x00187C03
	public void DestroyNow()
	{
		base.smi.sm.destroySimpleSignal.Trigger(base.smi);
	}

	// Token: 0x0600447B RID: 17531 RVA: 0x00189A20 File Offset: 0x00187C20
	private void Capture(HighEnergyParticlePort input)
	{
		if (input.currentParticle != null)
		{
			DebugUtil.LogArgs(new object[] { "Particle was backed up and caused an explosion!" });
			base.smi.sm.destroySignal.Trigger(base.smi);
			return;
		}
		this.capturedBy = input;
		input.currentParticle = this;
		input.Capture(this);
		if (input.currentParticle == this)
		{
			input.currentParticle = null;
			this.capturedBy = null;
			this.Collide(HighEnergyParticle.CollisionType.Captured);
			return;
		}
		this.capturedBy = null;
		this.Collide(HighEnergyParticle.CollisionType.CaptureAndRelease);
	}

	// Token: 0x0600447C RID: 17532 RVA: 0x00189AB1 File Offset: 0x00187CB1
	public void Uncapture()
	{
		if (this.capturedBy != null)
		{
			this.capturedBy.currentParticle = null;
		}
		this.capturedBy = null;
	}

	// Token: 0x0600447D RID: 17533 RVA: 0x00189AD4 File Offset: 0x00187CD4
	public void CheckCollision()
	{
		if (this.collision != HighEnergyParticle.CollisionType.None)
		{
			return;
		}
		int num = Grid.PosToCell(base.smi.master.transform.GetPosition());
		GameObject gameObject = Grid.Objects[num, 1];
		if (gameObject != null)
		{
			gameObject.GetComponent<Operational>();
			HighEnergyParticlePort component = gameObject.GetComponent<HighEnergyParticlePort>();
			if (component != null)
			{
				Vector2 vector = Grid.CellToPosCCC(component.GetHighEnergyParticleInputPortPosition(), Grid.SceneLayer.NoLayer);
				if (base.GetComponent<KCircleCollider2D>().Intersects(vector))
				{
					if (component.InputActive() && component.AllowCapture(this))
					{
						this.Capture(component);
						return;
					}
					this.Collide(HighEnergyParticle.CollisionType.PassThrough);
				}
			}
		}
		KCircleCollider2D component2 = base.GetComponent<KCircleCollider2D>();
		int num2 = 0;
		int num3 = 0;
		Grid.CellToXY(num, out num2, out num3);
		ListPool<ScenePartitionerEntry, HighEnergyParticle>.PooledList pooledList = ListPool<ScenePartitionerEntry, HighEnergyParticle>.Allocate();
		GameScenePartitioner.Instance.GatherEntries(num2 - 1, num3 - 1, 3, 3, GameScenePartitioner.Instance.collisionLayer, pooledList);
		foreach (ScenePartitionerEntry scenePartitionerEntry in pooledList)
		{
			KCollider2D kcollider2D = scenePartitionerEntry.obj as KCollider2D;
			HighEnergyParticle component3 = kcollider2D.gameObject.GetComponent<HighEnergyParticle>();
			if (!(component3 == null) && !(component3 == this) && component3.isCollideable)
			{
				bool flag = component2.Intersects(component3.transform.position);
				bool flag2 = kcollider2D.Intersects(base.transform.position);
				if (flag && flag2)
				{
					this.payload += component3.payload;
					component3.DestroyNow();
					this.Collide(HighEnergyParticle.CollisionType.HighEnergyParticle);
					return;
				}
			}
		}
		pooledList.Recycle();
		GameObject gameObject2 = Grid.Objects[num, 3];
		if (gameObject2 != null)
		{
			ObjectLayerListItem objectLayerListItem = gameObject2.GetComponent<Pickupable>().objectLayerListItem;
			while (objectLayerListItem != null)
			{
				GameObject gameObject3 = objectLayerListItem.gameObject;
				objectLayerListItem = objectLayerListItem.nextItem;
				if (!(gameObject3 == null))
				{
					KPrefabID component4 = gameObject3.GetComponent<KPrefabID>();
					Health component5 = gameObject2.GetComponent<Health>();
					if (component5 != null && component4 != null && component4.HasTag(GameTags.Creature) && !component5.IsDefeated())
					{
						component5.Damage(20f);
						this.Collide(HighEnergyParticle.CollisionType.Creature);
						return;
					}
				}
			}
		}
		GameObject gameObject4 = Grid.Objects[num, 0];
		if (gameObject4 != null)
		{
			Health component6 = gameObject4.GetComponent<Health>();
			if (component6 != null && !component6.IsDefeated() && !gameObject4.HasTag(GameTags.Dead) && !gameObject4.HasTag(GameTags.Dying))
			{
				component6.Damage(20f);
				WoundMonitor.Instance smi = gameObject4.GetSMI<WoundMonitor.Instance>();
				if (smi != null && !component6.IsDefeated())
				{
					smi.PlayKnockedOverImpactAnimation();
				}
				gameObject4.GetComponent<PrimaryElement>().AddDisease(Db.Get().Diseases.GetIndex(Db.Get().Diseases.RadiationPoisoning.Id), Mathf.FloorToInt(this.payload * 0.5f / 0.01f), "HEPImpact");
				this.Collide(HighEnergyParticle.CollisionType.Minion);
				return;
			}
		}
		if (Grid.IsSolidCell(num))
		{
			GameObject gameObject5 = Grid.Objects[num, 9];
			if (gameObject5 == null || !gameObject5.HasTag(GameTags.HEPPassThrough) || this.capturedBy == null || this.capturedBy.gameObject != gameObject5)
			{
				this.Collide(HighEnergyParticle.CollisionType.Solid);
			}
			return;
		}
	}

	// Token: 0x0600447E RID: 17534 RVA: 0x00189E68 File Offset: 0x00188068
	public void MovingUpdate(float dt)
	{
		if (this.collision != HighEnergyParticle.CollisionType.None)
		{
			return;
		}
		Vector3 position = base.transform.GetPosition();
		int num = Grid.PosToCell(position);
		Vector3 vector = position + EightDirectionUtil.GetNormal(this.direction) * this.speed * dt;
		int num2 = Grid.PosToCell(vector);
		SaveGame.Instance.ColonyAchievementTracker.radBoltTravelDistance += this.speed * dt;
		this.loopingSounds.UpdateVelocity(this.flyingSound, vector - position);
		if (!Grid.IsValidCell(num2))
		{
			base.smi.sm.destroySimpleSignal.Trigger(base.smi);
			return;
		}
		if (num != num2)
		{
			this.payload -= 0.1f;
			byte index = Db.Get().Diseases.GetIndex(Db.Get().Diseases.RadiationPoisoning.Id);
			int num3 = Mathf.FloorToInt(5f);
			if (!Grid.Element[num2].IsVacuum)
			{
				SimMessages.ModifyDiseaseOnCell(num2, index, num3);
			}
		}
		if (this.payload <= 0f)
		{
			base.smi.sm.destroySimpleSignal.Trigger(base.smi);
		}
		base.transform.SetPosition(vector);
	}

	// Token: 0x0600447F RID: 17535 RVA: 0x00189FB3 File Offset: 0x001881B3
	private void StartLoopingSound()
	{
		this.loopingSounds.StartSound(this.flyingSound);
	}

	// Token: 0x06004480 RID: 17536 RVA: 0x00189FC7 File Offset: 0x001881C7
	private void StopLoopingSound()
	{
		this.loopingSounds.StopSound(this.flyingSound);
	}

	// Token: 0x04002DC7 RID: 11719
	[Serialize]
	private EightDirection direction;

	// Token: 0x04002DC8 RID: 11720
	[Serialize]
	public float speed;

	// Token: 0x04002DC9 RID: 11721
	[Serialize]
	public float payload;

	// Token: 0x04002DCA RID: 11722
	[MyCmpReq]
	private RadiationEmitter emitter;

	// Token: 0x04002DCB RID: 11723
	[Serialize]
	public float perCellFalloff;

	// Token: 0x04002DCC RID: 11724
	[Serialize]
	public HighEnergyParticle.CollisionType collision;

	// Token: 0x04002DCD RID: 11725
	[Serialize]
	public HighEnergyParticlePort capturedBy;

	// Token: 0x04002DCE RID: 11726
	public short emitRadius;

	// Token: 0x04002DCF RID: 11727
	public float emitRate;

	// Token: 0x04002DD0 RID: 11728
	public float emitSpeed;

	// Token: 0x04002DD1 RID: 11729
	private LoopingSounds loopingSounds;

	// Token: 0x04002DD2 RID: 11730
	public string flyingSound;

	// Token: 0x04002DD3 RID: 11731
	public bool isCollideable;

	// Token: 0x02001964 RID: 6500
	public enum CollisionType
	{
		// Token: 0x04007BFE RID: 31742
		None,
		// Token: 0x04007BFF RID: 31743
		Solid,
		// Token: 0x04007C00 RID: 31744
		Creature,
		// Token: 0x04007C01 RID: 31745
		Minion,
		// Token: 0x04007C02 RID: 31746
		Captured,
		// Token: 0x04007C03 RID: 31747
		HighEnergyParticle,
		// Token: 0x04007C04 RID: 31748
		CaptureAndRelease,
		// Token: 0x04007C05 RID: 31749
		PassThrough
	}

	// Token: 0x02001965 RID: 6501
	public class StatesInstance : GameStateMachine<HighEnergyParticle.States, HighEnergyParticle.StatesInstance, HighEnergyParticle, object>.GameInstance
	{
		// Token: 0x06009F07 RID: 40711 RVA: 0x00397D7B File Offset: 0x00395F7B
		public StatesInstance(HighEnergyParticle smi)
			: base(smi)
		{
		}
	}

	// Token: 0x02001966 RID: 6502
	public class States : GameStateMachine<HighEnergyParticle.States, HighEnergyParticle.StatesInstance, HighEnergyParticle>
	{
		// Token: 0x06009F08 RID: 40712 RVA: 0x00397D84 File Offset: 0x00395F84
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.ready.pre;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.ready.OnSignal(this.destroySimpleSignal, this.destroying.instant).OnSignal(this.destroySignal, this.destroying.explode, (HighEnergyParticle.StatesInstance smi) => smi.master.collision == HighEnergyParticle.CollisionType.Creature).OnSignal(this.destroySignal, this.destroying.explode, (HighEnergyParticle.StatesInstance smi) => smi.master.collision == HighEnergyParticle.CollisionType.Minion)
				.OnSignal(this.destroySignal, this.destroying.explode, (HighEnergyParticle.StatesInstance smi) => smi.master.collision == HighEnergyParticle.CollisionType.Solid)
				.OnSignal(this.destroySignal, this.destroying.blackhole, (HighEnergyParticle.StatesInstance smi) => smi.master.collision == HighEnergyParticle.CollisionType.HighEnergyParticle)
				.OnSignal(this.destroySignal, this.destroying.captured, (HighEnergyParticle.StatesInstance smi) => smi.master.collision == HighEnergyParticle.CollisionType.Captured)
				.OnSignal(this.destroySignal, this.catchAndRelease, (HighEnergyParticle.StatesInstance smi) => smi.master.collision == HighEnergyParticle.CollisionType.CaptureAndRelease)
				.Enter(delegate(HighEnergyParticle.StatesInstance smi)
				{
					smi.master.emitter.SetEmitting(true);
					smi.master.isCollideable = true;
				})
				.Update(delegate(HighEnergyParticle.StatesInstance smi, float dt)
				{
					smi.master.MovingUpdate(dt);
					smi.master.CheckCollision();
				}, UpdateRate.SIM_EVERY_TICK, false);
			this.ready.pre.PlayAnim("travel_pre").OnAnimQueueComplete(this.ready.moving);
			this.ready.moving.PlayAnim("travel_loop", KAnim.PlayMode.Loop);
			this.catchAndRelease.Enter(delegate(HighEnergyParticle.StatesInstance smi)
			{
				smi.master.collision = HighEnergyParticle.CollisionType.None;
			}).PlayAnim("explode", KAnim.PlayMode.Once).OnAnimQueueComplete(this.ready.pre);
			this.destroying.Enter(delegate(HighEnergyParticle.StatesInstance smi)
			{
				smi.master.isCollideable = false;
				smi.master.StopLoopingSound();
			});
			this.destroying.instant.Enter(delegate(HighEnergyParticle.StatesInstance smi)
			{
				global::UnityEngine.Object.Destroy(smi.master.gameObject);
			});
			this.destroying.explode.PlayAnim("explode").Enter(delegate(HighEnergyParticle.StatesInstance smi)
			{
				this.EmitRemainingPayload(smi);
			});
			this.destroying.blackhole.PlayAnim("collision").Enter(delegate(HighEnergyParticle.StatesInstance smi)
			{
				this.EmitRemainingPayload(smi);
			});
			this.destroying.captured.PlayAnim("travel_pst").OnAnimQueueComplete(this.destroying.instant).Enter(delegate(HighEnergyParticle.StatesInstance smi)
			{
				smi.master.emitter.SetEmitting(false);
			});
		}

		// Token: 0x06009F09 RID: 40713 RVA: 0x003980BC File Offset: 0x003962BC
		private void EmitRemainingPayload(HighEnergyParticle.StatesInstance smi)
		{
			smi.master.GetComponent<KBatchedAnimController>().GetCurrentAnim();
			smi.master.emitter.emitRadiusX = 6;
			smi.master.emitter.emitRadiusY = 6;
			smi.master.emitter.emitRads = smi.master.payload * 0.5f * 600f / 9f;
			smi.master.emitter.Refresh();
			SimMessages.AddRemoveSubstance(Grid.PosToCell(smi.master.gameObject), SimHashes.Fallout, CellEventLogger.Instance.ElementEmitted, smi.master.payload * 0.001f, 5000f, Db.Get().Diseases.GetIndex(Db.Get().Diseases.RadiationPoisoning.Id), Mathf.FloorToInt(smi.master.payload * 0.5f / 0.01f), true, -1);
			smi.Schedule(1f, delegate(object obj)
			{
				global::UnityEngine.Object.Destroy(smi.master.gameObject);
			}, null);
		}

		// Token: 0x04007C06 RID: 31750
		public HighEnergyParticle.States.ReadyStates ready;

		// Token: 0x04007C07 RID: 31751
		public HighEnergyParticle.States.DestructionStates destroying;

		// Token: 0x04007C08 RID: 31752
		public GameStateMachine<HighEnergyParticle.States, HighEnergyParticle.StatesInstance, HighEnergyParticle, object>.State catchAndRelease;

		// Token: 0x04007C09 RID: 31753
		public StateMachine<HighEnergyParticle.States, HighEnergyParticle.StatesInstance, HighEnergyParticle, object>.Signal destroySignal;

		// Token: 0x04007C0A RID: 31754
		public StateMachine<HighEnergyParticle.States, HighEnergyParticle.StatesInstance, HighEnergyParticle, object>.Signal destroySimpleSignal;

		// Token: 0x0200284A RID: 10314
		public class ReadyStates : GameStateMachine<HighEnergyParticle.States, HighEnergyParticle.StatesInstance, HighEnergyParticle, object>.State
		{
			// Token: 0x0400B2AD RID: 45741
			public GameStateMachine<HighEnergyParticle.States, HighEnergyParticle.StatesInstance, HighEnergyParticle, object>.State pre;

			// Token: 0x0400B2AE RID: 45742
			public GameStateMachine<HighEnergyParticle.States, HighEnergyParticle.StatesInstance, HighEnergyParticle, object>.State moving;
		}

		// Token: 0x0200284B RID: 10315
		public class DestructionStates : GameStateMachine<HighEnergyParticle.States, HighEnergyParticle.StatesInstance, HighEnergyParticle, object>.State
		{
			// Token: 0x0400B2AF RID: 45743
			public GameStateMachine<HighEnergyParticle.States, HighEnergyParticle.StatesInstance, HighEnergyParticle, object>.State instant;

			// Token: 0x0400B2B0 RID: 45744
			public GameStateMachine<HighEnergyParticle.States, HighEnergyParticle.StatesInstance, HighEnergyParticle, object>.State explode;

			// Token: 0x0400B2B1 RID: 45745
			public GameStateMachine<HighEnergyParticle.States, HighEnergyParticle.StatesInstance, HighEnergyParticle, object>.State captured;

			// Token: 0x0400B2B2 RID: 45746
			public GameStateMachine<HighEnergyParticle.States, HighEnergyParticle.StatesInstance, HighEnergyParticle, object>.State blackhole;
		}
	}
}
