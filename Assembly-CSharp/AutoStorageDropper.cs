using System;
using UnityEngine;

// Token: 0x02000574 RID: 1396
public class AutoStorageDropper : GameStateMachine<AutoStorageDropper, AutoStorageDropper.Instance, IStateMachineTarget, AutoStorageDropper.Def>
{
	// Token: 0x06001F30 RID: 7984 RVA: 0x000B2A50 File Offset: 0x000B0C50
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.idle;
		this.root.Update(delegate(AutoStorageDropper.Instance smi, float dt)
		{
			smi.UpdateBlockedStatus();
		}, UpdateRate.SIM_200ms, true);
		this.idle.EventTransition(GameHashes.OnStorageChange, this.pre_drop, null).OnSignal(this.checkCanDrop, this.pre_drop, (AutoStorageDropper.Instance smi) => !smi.GetComponent<Storage>().IsEmpty()).ParamTransition<bool>(this.isBlocked, this.blocked, GameStateMachine<AutoStorageDropper, AutoStorageDropper.Instance, IStateMachineTarget, AutoStorageDropper.Def>.IsTrue);
		this.pre_drop.ScheduleGoTo((AutoStorageDropper.Instance smi) => smi.def.delay, this.dropping);
		this.dropping.Enter(delegate(AutoStorageDropper.Instance smi)
		{
			smi.Drop();
		}).GoTo(this.idle);
		this.blocked.ParamTransition<bool>(this.isBlocked, this.pre_drop, GameStateMachine<AutoStorageDropper, AutoStorageDropper.Instance, IStateMachineTarget, AutoStorageDropper.Def>.IsFalse).ToggleStatusItem(Db.Get().BuildingStatusItems.OutputTileBlocked, null);
	}

	// Token: 0x04001218 RID: 4632
	private GameStateMachine<AutoStorageDropper, AutoStorageDropper.Instance, IStateMachineTarget, AutoStorageDropper.Def>.State idle;

	// Token: 0x04001219 RID: 4633
	private GameStateMachine<AutoStorageDropper, AutoStorageDropper.Instance, IStateMachineTarget, AutoStorageDropper.Def>.State pre_drop;

	// Token: 0x0400121A RID: 4634
	private GameStateMachine<AutoStorageDropper, AutoStorageDropper.Instance, IStateMachineTarget, AutoStorageDropper.Def>.State dropping;

	// Token: 0x0400121B RID: 4635
	private GameStateMachine<AutoStorageDropper, AutoStorageDropper.Instance, IStateMachineTarget, AutoStorageDropper.Def>.State blocked;

	// Token: 0x0400121C RID: 4636
	private StateMachine<AutoStorageDropper, AutoStorageDropper.Instance, IStateMachineTarget, AutoStorageDropper.Def>.BoolParameter isBlocked;

	// Token: 0x0400121D RID: 4637
	public StateMachine<AutoStorageDropper, AutoStorageDropper.Instance, IStateMachineTarget, AutoStorageDropper.Def>.Signal checkCanDrop;

	// Token: 0x020013B3 RID: 5043
	public class DropperFxConfig
	{
		// Token: 0x04006A5C RID: 27228
		public string animFile;

		// Token: 0x04006A5D RID: 27229
		public string animName;

		// Token: 0x04006A5E RID: 27230
		public Grid.SceneLayer layer = Grid.SceneLayer.FXFront;

		// Token: 0x04006A5F RID: 27231
		public bool useElementTint = true;

		// Token: 0x04006A60 RID: 27232
		public bool flipX;

		// Token: 0x04006A61 RID: 27233
		public bool flipY;
	}

	// Token: 0x020013B4 RID: 5044
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04006A62 RID: 27234
		public CellOffset dropOffset;

		// Token: 0x04006A63 RID: 27235
		public bool asOre;

		// Token: 0x04006A64 RID: 27236
		public SimHashes[] elementFilter;

		// Token: 0x04006A65 RID: 27237
		public bool invertElementFilterInitialValue;

		// Token: 0x04006A66 RID: 27238
		public bool blockedBySubstantialLiquid;

		// Token: 0x04006A67 RID: 27239
		public AutoStorageDropper.DropperFxConfig neutralFx;

		// Token: 0x04006A68 RID: 27240
		public AutoStorageDropper.DropperFxConfig leftFx;

		// Token: 0x04006A69 RID: 27241
		public AutoStorageDropper.DropperFxConfig rightFx;

		// Token: 0x04006A6A RID: 27242
		public AutoStorageDropper.DropperFxConfig upFx;

		// Token: 0x04006A6B RID: 27243
		public AutoStorageDropper.DropperFxConfig downFx;

		// Token: 0x04006A6C RID: 27244
		public Vector3 fxOffset = Vector3.zero;

		// Token: 0x04006A6D RID: 27245
		public float cooldown = 2f;

		// Token: 0x04006A6E RID: 27246
		public float delay;
	}

	// Token: 0x020013B5 RID: 5045
	public new class Instance : GameStateMachine<AutoStorageDropper, AutoStorageDropper.Instance, IStateMachineTarget, AutoStorageDropper.Def>.GameInstance
	{
		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x06008B0B RID: 35595 RVA: 0x00351E3E File Offset: 0x0035003E
		// (set) Token: 0x06008B0C RID: 35596 RVA: 0x00351E46 File Offset: 0x00350046
		public bool isInvertElementFilter { get; private set; }

		// Token: 0x06008B0D RID: 35597 RVA: 0x00351E4F File Offset: 0x0035004F
		public Instance(IStateMachineTarget master, AutoStorageDropper.Def def)
			: base(master, def)
		{
			this.isInvertElementFilter = def.invertElementFilterInitialValue;
		}

		// Token: 0x06008B0E RID: 35598 RVA: 0x00351E65 File Offset: 0x00350065
		public void SetInvertElementFilter(bool value)
		{
			base.smi.isInvertElementFilter = value;
			base.smi.sm.checkCanDrop.Trigger(base.smi);
		}

		// Token: 0x06008B0F RID: 35599 RVA: 0x00351E90 File Offset: 0x00350090
		public void UpdateBlockedStatus()
		{
			int num = Grid.PosToCell(base.smi.GetDropPosition());
			bool flag = Grid.IsSolidCell(num) || (base.def.blockedBySubstantialLiquid && Grid.IsSubstantialLiquid(num, 0.35f));
			base.sm.isBlocked.Set(flag, base.smi, false);
		}

		// Token: 0x06008B10 RID: 35600 RVA: 0x00351EF0 File Offset: 0x003500F0
		private bool IsFilteredElement(SimHashes element)
		{
			for (int num = 0; num != base.def.elementFilter.Length; num++)
			{
				if (base.def.elementFilter[num] == element)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06008B11 RID: 35601 RVA: 0x00351F28 File Offset: 0x00350128
		private bool AllowedToDrop(SimHashes element)
		{
			return base.def.elementFilter == null || base.def.elementFilter.Length == 0 || (!this.isInvertElementFilter && this.IsFilteredElement(element)) || (this.isInvertElementFilter && !this.IsFilteredElement(element));
		}

		// Token: 0x06008B12 RID: 35602 RVA: 0x00351F78 File Offset: 0x00350178
		public void Drop()
		{
			bool flag = false;
			Element element = null;
			for (int i = this.m_storage.Count - 1; i >= 0; i--)
			{
				GameObject gameObject = this.m_storage.items[i];
				PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
				if (this.AllowedToDrop(component.ElementID))
				{
					if (base.def.asOre)
					{
						this.m_storage.Drop(gameObject, true);
						gameObject.transform.SetPosition(this.GetDropPosition());
						element = component.Element;
						flag = true;
					}
					else
					{
						Dumpable component2 = gameObject.GetComponent<Dumpable>();
						if (!component2.IsNullOrDestroyed())
						{
							component2.Dump(this.GetDropPosition());
							element = component.Element;
							flag = true;
						}
					}
				}
			}
			AutoStorageDropper.DropperFxConfig dropperAnim = this.GetDropperAnim();
			if (flag && dropperAnim != null && GameClock.Instance.GetTime() > this.m_timeSinceLastDrop + base.def.cooldown)
			{
				this.m_timeSinceLastDrop = GameClock.Instance.GetTime();
				Vector3 vector = Grid.CellToPosCCC(Grid.PosToCell(this.GetDropPosition()), dropperAnim.layer);
				vector += ((this.m_rotatable != null) ? this.m_rotatable.GetRotatedOffset(base.def.fxOffset) : base.def.fxOffset);
				KBatchedAnimController kbatchedAnimController = FXHelpers.CreateEffect(dropperAnim.animFile, vector, null, false, dropperAnim.layer, false);
				kbatchedAnimController.destroyOnAnimComplete = false;
				kbatchedAnimController.FlipX = dropperAnim.flipX;
				kbatchedAnimController.FlipY = dropperAnim.flipY;
				if (dropperAnim.useElementTint)
				{
					kbatchedAnimController.TintColour = element.substance.colour;
				}
				kbatchedAnimController.Play(dropperAnim.animName, KAnim.PlayMode.Once, 1f, 0f);
			}
		}

		// Token: 0x06008B13 RID: 35603 RVA: 0x00352140 File Offset: 0x00350340
		public AutoStorageDropper.DropperFxConfig GetDropperAnim()
		{
			CellOffset cellOffset = ((this.m_rotatable != null) ? this.m_rotatable.GetRotatedCellOffset(base.def.dropOffset) : base.def.dropOffset);
			if (cellOffset.x < 0)
			{
				return base.def.leftFx;
			}
			if (cellOffset.x > 0)
			{
				return base.def.rightFx;
			}
			if (cellOffset.y < 0)
			{
				return base.def.downFx;
			}
			if (cellOffset.y > 0)
			{
				return base.def.upFx;
			}
			return base.def.neutralFx;
		}

		// Token: 0x06008B14 RID: 35604 RVA: 0x003521E0 File Offset: 0x003503E0
		public Vector3 GetDropPosition()
		{
			if (!(this.m_rotatable != null))
			{
				return base.transform.GetPosition() + base.def.dropOffset.ToVector3();
			}
			return base.transform.GetPosition() + this.m_rotatable.GetRotatedCellOffset(base.def.dropOffset).ToVector3();
		}

		// Token: 0x04006A6F RID: 27247
		[MyCmpGet]
		private Storage m_storage;

		// Token: 0x04006A70 RID: 27248
		[MyCmpGet]
		private Rotatable m_rotatable;

		// Token: 0x04006A72 RID: 27250
		private float m_timeSinceLastDrop;
	}
}
