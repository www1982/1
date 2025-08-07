using System;
using Klei.AI;
using UnityEngine;

// Token: 0x020005B0 RID: 1456
public class EffectImmunityProviderStation<StateMachineInstanceType> : GameStateMachine<EffectImmunityProviderStation<StateMachineInstanceType>, StateMachineInstanceType, IStateMachineTarget, EffectImmunityProviderStation<StateMachineInstanceType>.Def> where StateMachineInstanceType : EffectImmunityProviderStation<StateMachineInstanceType>.BaseInstance
{
	// Token: 0x0600218F RID: 8591 RVA: 0x000C1EFC File Offset: 0x000C00FC
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.inactive;
		this.inactive.EventTransition(GameHashes.ActiveChanged, this.active, (StateMachineInstanceType smi) => smi.GetComponent<Operational>().IsActive);
		this.active.EventTransition(GameHashes.ActiveChanged, this.inactive, (StateMachineInstanceType smi) => !smi.GetComponent<Operational>().IsActive);
	}

	// Token: 0x0400138E RID: 5006
	public GameStateMachine<EffectImmunityProviderStation<StateMachineInstanceType>, StateMachineInstanceType, IStateMachineTarget, EffectImmunityProviderStation<StateMachineInstanceType>.Def>.State inactive;

	// Token: 0x0400138F RID: 5007
	public GameStateMachine<EffectImmunityProviderStation<StateMachineInstanceType>, StateMachineInstanceType, IStateMachineTarget, EffectImmunityProviderStation<StateMachineInstanceType>.Def>.State active;

	// Token: 0x02001447 RID: 5191
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x06008D12 RID: 36114 RVA: 0x0035776F File Offset: 0x0035596F
		public virtual string[] DefaultAnims()
		{
			return new string[] { "", "", "" };
		}

		// Token: 0x06008D13 RID: 36115 RVA: 0x0035778F File Offset: 0x0035598F
		public virtual string DefaultAnimFileName()
		{
			return "anim_warmup_kanim";
		}

		// Token: 0x06008D14 RID: 36116 RVA: 0x00357796 File Offset: 0x00355996
		public string[] GetAnimNames()
		{
			if (this.overrideAnims != null)
			{
				return this.overrideAnims;
			}
			return this.DefaultAnims();
		}

		// Token: 0x06008D15 RID: 36117 RVA: 0x003577AD File Offset: 0x003559AD
		public string GetAnimFileName(GameObject entity)
		{
			if (this.overrideFileName != null)
			{
				return this.overrideFileName(entity);
			}
			return this.DefaultAnimFileName();
		}

		// Token: 0x04006C06 RID: 27654
		public Action<GameObject, StateMachineInstanceType> onEffectApplied;

		// Token: 0x04006C07 RID: 27655
		public Func<GameObject, bool> specialRequirements;

		// Token: 0x04006C08 RID: 27656
		public Func<GameObject, string> overrideFileName;

		// Token: 0x04006C09 RID: 27657
		public string[] overrideAnims;

		// Token: 0x04006C0A RID: 27658
		public CellOffset[][] range;
	}

	// Token: 0x02001448 RID: 5192
	public abstract class BaseInstance : GameStateMachine<EffectImmunityProviderStation<StateMachineInstanceType>, StateMachineInstanceType, IStateMachineTarget, EffectImmunityProviderStation<StateMachineInstanceType>.Def>.GameInstance
	{
		// Token: 0x06008D17 RID: 36119 RVA: 0x003577D2 File Offset: 0x003559D2
		public string GetAnimFileName(GameObject entity)
		{
			return base.def.GetAnimFileName(entity);
		}

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x06008D18 RID: 36120 RVA: 0x003577E0 File Offset: 0x003559E0
		public string PreAnimName
		{
			get
			{
				return base.def.GetAnimNames()[0];
			}
		}

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x06008D19 RID: 36121 RVA: 0x003577EF File Offset: 0x003559EF
		public string LoopAnimName
		{
			get
			{
				return base.def.GetAnimNames()[1];
			}
		}

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x06008D1A RID: 36122 RVA: 0x003577FE File Offset: 0x003559FE
		public string PstAnimName
		{
			get
			{
				return base.def.GetAnimNames()[2];
			}
		}

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x06008D1B RID: 36123 RVA: 0x0035780D File Offset: 0x00355A0D
		public bool CanBeUsed
		{
			get
			{
				return this.IsActive && (base.def.specialRequirements == null || base.def.specialRequirements(base.gameObject));
			}
		}

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x06008D1C RID: 36124 RVA: 0x0035783E File Offset: 0x00355A3E
		protected bool IsActive
		{
			get
			{
				return base.IsInsideState(base.sm.active);
			}
		}

		// Token: 0x06008D1D RID: 36125 RVA: 0x00357851 File Offset: 0x00355A51
		public BaseInstance(IStateMachineTarget master, EffectImmunityProviderStation<StateMachineInstanceType>.Def def)
			: base(master, def)
		{
		}

		// Token: 0x06008D1E RID: 36126 RVA: 0x0035785C File Offset: 0x00355A5C
		public int GetBestAvailableCell(Navigator dupeLooking, out int _cost)
		{
			_cost = int.MaxValue;
			if (!this.CanBeUsed)
			{
				return Grid.InvalidCell;
			}
			int num = Grid.PosToCell(this);
			int num2 = Grid.InvalidCell;
			if (base.def.range != null)
			{
				for (int i = 0; i < base.def.range.GetLength(0); i++)
				{
					int num3 = int.MaxValue;
					for (int j = 0; j < base.def.range[i].Length; j++)
					{
						int num4 = Grid.OffsetCell(num, base.def.range[i][j]);
						if (dupeLooking.CanReach(num4))
						{
							int navigationCost = dupeLooking.GetNavigationCost(num4);
							if (navigationCost < num3)
							{
								num3 = navigationCost;
								num2 = num4;
							}
						}
					}
					if (num2 != Grid.InvalidCell)
					{
						_cost = num3;
						break;
					}
				}
				return num2;
			}
			if (dupeLooking.CanReach(num))
			{
				_cost = dupeLooking.GetNavigationCost(num);
				return num;
			}
			return Grid.InvalidCell;
		}

		// Token: 0x06008D1F RID: 36127 RVA: 0x00357940 File Offset: 0x00355B40
		public void ApplyImmunityEffect(GameObject target, bool triggerEvents = true)
		{
			Effects component = target.GetComponent<Effects>();
			if (component == null)
			{
				return;
			}
			this.ApplyImmunityEffect(component);
			if (triggerEvents)
			{
				Action<GameObject, StateMachineInstanceType> onEffectApplied = base.def.onEffectApplied;
				if (onEffectApplied == null)
				{
					return;
				}
				onEffectApplied(component.gameObject, (StateMachineInstanceType)((object)this));
			}
		}

		// Token: 0x06008D20 RID: 36128
		protected abstract void ApplyImmunityEffect(Effects target);

		// Token: 0x06008D21 RID: 36129 RVA: 0x00357989 File Offset: 0x00355B89
		public override void StartSM()
		{
			Components.EffectImmunityProviderStations.Add(this);
			base.StartSM();
		}

		// Token: 0x06008D22 RID: 36130 RVA: 0x0035799C File Offset: 0x00355B9C
		protected override void OnCleanUp()
		{
			Components.EffectImmunityProviderStations.Remove(this);
			base.OnCleanUp();
		}
	}
}
