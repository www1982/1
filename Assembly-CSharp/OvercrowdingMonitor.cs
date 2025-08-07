using System;
using System.Diagnostics;
using Klei.AI;
using STRINGS;

// Token: 0x0200087A RID: 2170
public class OvercrowdingMonitor : GameStateMachine<OvercrowdingMonitor, OvercrowdingMonitor.Instance, IStateMachineTarget, OvercrowdingMonitor.Def>
{
	// Token: 0x06003BAB RID: 15275 RVA: 0x0014ADEC File Offset: 0x00148FEC
	[Conditional("DETAILED_OVERCROWDING_MONITOR_PROFILE")]
	private static void BeginDetailedSample(string regionName)
	{
	}

	// Token: 0x06003BAC RID: 15276 RVA: 0x0014ADEE File Offset: 0x00148FEE
	[Conditional("DETAILED_OVERCROWDING_MONITOR_PROFILE")]
	private static void EndDetailedSample(string regionName)
	{
	}

	// Token: 0x06003BAD RID: 15277 RVA: 0x0014ADF0 File Offset: 0x00148FF0
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.Update(new Action<OvercrowdingMonitor.Instance, float>(OvercrowdingMonitor.UpdateState), UpdateRate.SIM_1000ms, true);
	}

	// Token: 0x06003BAE RID: 15278 RVA: 0x0014AE14 File Offset: 0x00149014
	private static bool IsConfined(OvercrowdingMonitor.Instance smi)
	{
		if (smi.kpid.HasAnyTags(OvercrowdingMonitor.confinementImmunity))
		{
			return false;
		}
		if (smi.isFish)
		{
			int num = Grid.PosToCell(smi);
			if (Grid.IsValidCell(num) && !Grid.IsLiquid(num))
			{
				return true;
			}
			if (smi.fishOvercrowdingMonitor.cellCount < smi.def.spaceRequiredPerCreature)
			{
				return true;
			}
		}
		else
		{
			if (smi.cavity == null)
			{
				return true;
			}
			if (smi.cavity.numCells < smi.def.spaceRequiredPerCreature)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06003BAF RID: 15279 RVA: 0x0014AE98 File Offset: 0x00149098
	private static bool IsFutureOvercrowded(OvercrowdingMonitor.Instance smi)
	{
		if (smi.cavity != null)
		{
			int num = smi.cavity.creatures.Count + smi.cavity.eggs.Count;
			return num != 0 && smi.cavity.eggs.Count != 0 && smi.cavity.numCells / num < smi.def.spaceRequiredPerCreature;
		}
		return false;
	}

	// Token: 0x06003BB0 RID: 15280 RVA: 0x0014AF04 File Offset: 0x00149104
	private static int CalculateOvercrowdedModifer(OvercrowdingMonitor.Instance smi)
	{
		if (smi.fishOvercrowdingMonitor != null)
		{
			int fishCount = smi.fishOvercrowdingMonitor.fishCount;
			if (fishCount <= 0)
			{
				return 0;
			}
			int num = smi.fishOvercrowdingMonitor.cellCount / smi.def.spaceRequiredPerCreature;
			if (num < smi.fishOvercrowdingMonitor.fishCount)
			{
				return -(fishCount - num);
			}
			return 0;
		}
		else
		{
			if (smi.cavity == null)
			{
				return 0;
			}
			if (smi.cavity.creatures.Count <= 1)
			{
				return 0;
			}
			int num2 = smi.cavity.numCells / smi.def.spaceRequiredPerCreature;
			if (num2 < smi.cavity.creatures.Count)
			{
				return -(smi.cavity.creatures.Count - num2);
			}
			return 0;
		}
	}

	// Token: 0x06003BB1 RID: 15281 RVA: 0x0014AFB8 File Offset: 0x001491B8
	private static bool IsOvercrowded(OvercrowdingMonitor.Instance smi)
	{
		if (smi.def.spaceRequiredPerCreature == 0)
		{
			return false;
		}
		if (smi.fishOvercrowdingMonitor == null)
		{
			return smi.cavity != null && smi.cavity.creatures.Count > 1 && smi.cavity.numCells / smi.cavity.creatures.Count < smi.def.spaceRequiredPerCreature;
		}
		int fishCount = smi.fishOvercrowdingMonitor.fishCount;
		if (fishCount > 0)
		{
			return smi.fishOvercrowdingMonitor.cellCount / fishCount < smi.def.spaceRequiredPerCreature;
		}
		int num = Grid.PosToCell(smi);
		return Grid.IsValidCell(num) && !Grid.IsLiquid(num);
	}

	// Token: 0x06003BB2 RID: 15282 RVA: 0x0014B068 File Offset: 0x00149268
	private static void UpdateState(OvercrowdingMonitor.Instance smi, float dt)
	{
		bool flag = smi.kpid.HasTag(GameTags.Creatures.Confined);
		bool flag2 = smi.kpid.HasTag(GameTags.Creatures.Expecting);
		bool flag3 = smi.kpid.HasTag(GameTags.Creatures.Overcrowded);
		OvercrowdingMonitor.UpdateCavity(smi, dt);
		if (smi.def.spaceRequiredPerCreature == 0)
		{
			return;
		}
		bool flag4 = OvercrowdingMonitor.IsConfined(smi);
		bool flag5 = OvercrowdingMonitor.IsOvercrowded(smi);
		if (flag5)
		{
			if (!smi.isFish)
			{
				smi.overcrowdedModifier.SetValue((float)OvercrowdingMonitor.CalculateOvercrowdedModifer(smi));
			}
			else
			{
				smi.fishOvercrowdedModifier.SetValue((float)OvercrowdingMonitor.CalculateOvercrowdedModifer(smi));
			}
		}
		bool flag6 = !smi.isBaby && OvercrowdingMonitor.IsFutureOvercrowded(smi);
		if (flag != flag4 || flag2 != flag6 || flag3 != flag5)
		{
			KPrefabID kpid = smi.kpid;
			Effect effect = (smi.isFish ? smi.fishOvercrowdedEffect : smi.overcrowdedEffect);
			kpid.SetTag(GameTags.Creatures.Confined, flag4);
			kpid.SetTag(GameTags.Creatures.Overcrowded, flag5);
			kpid.SetTag(GameTags.Creatures.Expecting, flag6);
			OvercrowdingMonitor.SetEffect(smi, smi.stuckEffect, flag4);
			OvercrowdingMonitor.SetEffect(smi, effect, !flag4 && flag5);
			OvercrowdingMonitor.SetEffect(smi, smi.futureOvercrowdedEffect, !flag4 && flag6);
		}
	}

	// Token: 0x06003BB3 RID: 15283 RVA: 0x0014B191 File Offset: 0x00149391
	private static void SetEffect(OvercrowdingMonitor.Instance smi, Effect effect, bool set)
	{
		if (set)
		{
			smi.effects.Add(effect, false);
			return;
		}
		smi.effects.Remove(effect);
	}

	// Token: 0x06003BB4 RID: 15284 RVA: 0x0014B1B4 File Offset: 0x001493B4
	private static void UpdateCavity(OvercrowdingMonitor.Instance smi, float dt)
	{
		CavityInfo cavityForCell = Game.Instance.roomProber.GetCavityForCell(Grid.PosToCell(smi));
		if (cavityForCell != smi.cavity)
		{
			if (smi.cavity != null)
			{
				if (smi.kpid.HasTag(GameTags.Egg))
				{
					smi.cavity.RemoveFromCavity(smi.kpid, smi.cavity.eggs);
				}
				else
				{
					smi.cavity.RemoveFromCavity(smi.kpid, smi.cavity.creatures);
				}
				Game.Instance.roomProber.UpdateRoom(cavityForCell);
			}
			smi.cavity = cavityForCell;
			if (smi.cavity != null)
			{
				if (smi.kpid.HasTag(GameTags.Egg))
				{
					smi.cavity.eggs.Add(smi.kpid);
				}
				else
				{
					smi.cavity.creatures.Add(smi.kpid);
				}
				Game.Instance.roomProber.UpdateRoom(smi.cavity);
			}
		}
	}

	// Token: 0x04002497 RID: 9367
	public const float OVERCROWDED_FERTILITY_DEBUFF = -1f;

	// Token: 0x04002498 RID: 9368
	public static Tag[] confinementImmunity = new Tag[]
	{
		GameTags.Creatures.Burrowed,
		GameTags.Creatures.Digger
	};

	// Token: 0x0200182F RID: 6191
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04007829 RID: 30761
		public int spaceRequiredPerCreature;
	}

	// Token: 0x02001830 RID: 6192
	public new class Instance : GameStateMachine<OvercrowdingMonitor, OvercrowdingMonitor.Instance, IStateMachineTarget, OvercrowdingMonitor.Def>.GameInstance
	{
		// Token: 0x06009BD6 RID: 39894 RVA: 0x0038F094 File Offset: 0x0038D294
		public Instance(IStateMachineTarget master, OvercrowdingMonitor.Def def)
			: base(master, def)
		{
			BabyMonitor.Def def2 = master.gameObject.GetDef<BabyMonitor.Def>();
			this.isBaby = def2 != null;
			FishOvercrowdingMonitor.Def def3 = master.gameObject.GetDef<FishOvercrowdingMonitor.Def>();
			this.isFish = def3 != null;
			this.futureOvercrowdedEffect = new Effect("FutureOvercrowded", CREATURES.MODIFIERS.FUTURE_OVERCROWDED.NAME, CREATURES.MODIFIERS.FUTURE_OVERCROWDED.TOOLTIP, 0f, true, false, true, null, -1f, 0f, null, "");
			this.futureOvercrowdedEffect.Add(new AttributeModifier(Db.Get().Amounts.Fertility.deltaAttribute.Id, -1f, CREATURES.MODIFIERS.FUTURE_OVERCROWDED.NAME, true, false, true));
			this.overcrowdedEffect = new Effect("Overcrowded", CREATURES.MODIFIERS.OVERCROWDED.NAME, CREATURES.MODIFIERS.OVERCROWDED.TOOLTIP, 0f, true, false, true, null, -1f, 0f, null, "");
			this.overcrowdedModifier = new AttributeModifier(Db.Get().CritterAttributes.Happiness.Id, 0f, CREATURES.MODIFIERS.OVERCROWDED.NAME, false, false, false);
			this.overcrowdedEffect.Add(this.overcrowdedModifier);
			this.fishOvercrowdedEffect = new Effect("Overcrowded", CREATURES.MODIFIERS.OVERCROWDED.NAME, CREATURES.MODIFIERS.OVERCROWDED.FISHTOOLTIP, 0f, true, false, true, null, -1f, 0f, null, "");
			this.fishOvercrowdedModifier = new AttributeModifier(Db.Get().CritterAttributes.Happiness.Id, -5f, CREATURES.MODIFIERS.OVERCROWDED.NAME, false, false, false);
			this.fishOvercrowdedEffect.Add(this.fishOvercrowdedModifier);
			this.stuckEffect = new Effect("Confined", CREATURES.MODIFIERS.CONFINED.NAME, CREATURES.MODIFIERS.CONFINED.TOOLTIP, 0f, true, false, true, null, -1f, 0f, null, "");
			this.stuckEffect.Add(new AttributeModifier(Db.Get().CritterAttributes.Happiness.Id, -10f, CREATURES.MODIFIERS.CONFINED.NAME, false, false, true));
			this.stuckEffect.Add(new AttributeModifier(Db.Get().Amounts.Fertility.deltaAttribute.Id, -1f, CREATURES.MODIFIERS.CONFINED.NAME, true, false, true));
			OvercrowdingMonitor.UpdateState(this, 0f);
		}

		// Token: 0x06009BD7 RID: 39895 RVA: 0x0038F304 File Offset: 0x0038D504
		protected override void OnCleanUp()
		{
			if (this.cavity == null)
			{
				return;
			}
			if (this.kpid.HasTag(GameTags.Egg))
			{
				this.cavity.RemoveFromCavity(this.kpid, this.cavity.eggs);
				return;
			}
			this.cavity.RemoveFromCavity(this.kpid, this.cavity.creatures);
		}

		// Token: 0x06009BD8 RID: 39896 RVA: 0x0038F365 File Offset: 0x0038D565
		public void RoomRefreshUpdateCavity()
		{
			OvercrowdingMonitor.UpdateState(this, 0f);
		}

		// Token: 0x0400782A RID: 30762
		public CavityInfo cavity;

		// Token: 0x0400782B RID: 30763
		public bool isBaby;

		// Token: 0x0400782C RID: 30764
		public bool isFish;

		// Token: 0x0400782D RID: 30765
		public Effect futureOvercrowdedEffect;

		// Token: 0x0400782E RID: 30766
		public Effect overcrowdedEffect;

		// Token: 0x0400782F RID: 30767
		public AttributeModifier overcrowdedModifier;

		// Token: 0x04007830 RID: 30768
		public Effect fishOvercrowdedEffect;

		// Token: 0x04007831 RID: 30769
		public AttributeModifier fishOvercrowdedModifier;

		// Token: 0x04007832 RID: 30770
		public Effect stuckEffect;

		// Token: 0x04007833 RID: 30771
		[MyCmpReq]
		public KPrefabID kpid;

		// Token: 0x04007834 RID: 30772
		[MyCmpReq]
		public Effects effects;

		// Token: 0x04007835 RID: 30773
		[MySmiGet]
		public FishOvercrowdingMonitor.Instance fishOvercrowdingMonitor;
	}
}
