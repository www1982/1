using System;
using Klei;
using STRINGS;
using UnityEngine;

// Token: 0x020000FE RID: 254
public class LayEggStates : GameStateMachine<LayEggStates, LayEggStates.Instance, IStateMachineTarget, LayEggStates.Def>
{
	// Token: 0x06000493 RID: 1171 RVA: 0x00025848 File Offset: 0x00023A48
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.layeggpre;
		GameStateMachine<LayEggStates, LayEggStates.Instance, IStateMachineTarget, LayEggStates.Def>.State root = this.root;
		string text = CREATURES.STATUSITEMS.LAYINGANEGG.NAME;
		string text2 = CREATURES.STATUSITEMS.LAYINGANEGG.TOOLTIP;
		string text3 = "";
		StatusItem.IconType iconType = StatusItem.IconType.Info;
		NotificationType notificationType = NotificationType.Neutral;
		bool flag = false;
		StatusItemCategory main = Db.Get().StatusItemCategories.Main;
		root.ToggleStatusItem(text, text2, text3, iconType, notificationType, flag, default(HashedString), 129022, null, null, main);
		this.layeggpre.Enter(new StateMachine<LayEggStates, LayEggStates.Instance, IStateMachineTarget, LayEggStates.Def>.State.Callback(LayEggStates.LayEgg)).Exit(new StateMachine<LayEggStates, LayEggStates.Instance, IStateMachineTarget, LayEggStates.Def>.State.Callback(LayEggStates.ShowEgg)).PlayAnim("lay_egg_pre")
			.OnAnimQueueComplete(this.layeggpst);
		this.layeggpst.PlayAnim("lay_egg_pst").OnAnimQueueComplete(this.moveaside);
		this.moveaside.MoveTo(new Func<LayEggStates.Instance, int>(LayEggStates.GetMoveAsideCell), this.lookategg, this.behaviourcomplete, false);
		this.lookategg.Enter(new StateMachine<LayEggStates, LayEggStates.Instance, IStateMachineTarget, LayEggStates.Def>.State.Callback(LayEggStates.FaceEgg)).GoTo(this.behaviourcomplete);
		this.behaviourcomplete.QueueAnim("idle_loop", true, null).BehaviourComplete(GameTags.Creatures.Fertile, false);
	}

	// Token: 0x06000494 RID: 1172 RVA: 0x00025966 File Offset: 0x00023B66
	private static void LayEgg(LayEggStates.Instance smi)
	{
		smi.eggPos = smi.transform.GetPosition();
		smi.GetSMI<FertilityMonitor.Instance>().LayEgg();
	}

	// Token: 0x06000495 RID: 1173 RVA: 0x00025984 File Offset: 0x00023B84
	private static void ShowEgg(LayEggStates.Instance smi)
	{
		FertilityMonitor.Instance smi2 = smi.GetSMI<FertilityMonitor.Instance>();
		if (smi2 != null)
		{
			smi2.ShowEgg();
		}
	}

	// Token: 0x06000496 RID: 1174 RVA: 0x000259A1 File Offset: 0x00023BA1
	private static void FaceEgg(LayEggStates.Instance smi)
	{
		smi.Get<Facing>().Face(smi.eggPos);
	}

	// Token: 0x06000497 RID: 1175 RVA: 0x000259B4 File Offset: 0x00023BB4
	private static int GetMoveAsideCell(LayEggStates.Instance smi)
	{
		int num = 1;
		if (GenericGameSettings.instance.acceleratedLifecycle)
		{
			num = 8;
		}
		int num2 = Grid.PosToCell(smi);
		if (Grid.IsValidCell(num2))
		{
			int num3 = Grid.OffsetCell(num2, num, 0);
			if (Grid.IsValidCell(num3) && !Grid.Solid[num3])
			{
				return num3;
			}
			int num4 = Grid.OffsetCell(num2, -num, 0);
			if (Grid.IsValidCell(num4))
			{
				return num4;
			}
		}
		return Grid.InvalidCell;
	}

	// Token: 0x0400034D RID: 845
	public GameStateMachine<LayEggStates, LayEggStates.Instance, IStateMachineTarget, LayEggStates.Def>.State layeggpre;

	// Token: 0x0400034E RID: 846
	public GameStateMachine<LayEggStates, LayEggStates.Instance, IStateMachineTarget, LayEggStates.Def>.State layeggpst;

	// Token: 0x0400034F RID: 847
	public GameStateMachine<LayEggStates, LayEggStates.Instance, IStateMachineTarget, LayEggStates.Def>.State moveaside;

	// Token: 0x04000350 RID: 848
	public GameStateMachine<LayEggStates, LayEggStates.Instance, IStateMachineTarget, LayEggStates.Def>.State lookategg;

	// Token: 0x04000351 RID: 849
	public GameStateMachine<LayEggStates, LayEggStates.Instance, IStateMachineTarget, LayEggStates.Def>.State behaviourcomplete;

	// Token: 0x0200111F RID: 4383
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001120 RID: 4384
	public new class Instance : GameStateMachine<LayEggStates, LayEggStates.Instance, IStateMachineTarget, LayEggStates.Def>.GameInstance
	{
		// Token: 0x0600818C RID: 33164 RVA: 0x0032F39F File Offset: 0x0032D59F
		public Instance(Chore<LayEggStates.Instance> chore, LayEggStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Creatures.Fertile);
		}

		// Token: 0x0400620E RID: 25102
		public Vector3 eggPos;
	}
}
