using System;
using UnityEngine;

// Token: 0x02000802 RID: 2050
public class CargoDropperStorage : GameStateMachine<CargoDropperStorage, CargoDropperStorage.StatesInstance, IStateMachineTarget, CargoDropperStorage.Def>
{
	// Token: 0x060037C6 RID: 14278 RVA: 0x00135589 File Offset: 0x00133789
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.EventHandler(GameHashes.JettisonCargo, delegate(CargoDropperStorage.StatesInstance smi, object data)
		{
			smi.JettisonCargo(data);
		});
	}

	// Token: 0x02001760 RID: 5984
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04007581 RID: 30081
		public Vector3 dropOffset;
	}

	// Token: 0x02001761 RID: 5985
	public class StatesInstance : GameStateMachine<CargoDropperStorage, CargoDropperStorage.StatesInstance, IStateMachineTarget, CargoDropperStorage.Def>.GameInstance
	{
		// Token: 0x060098CF RID: 39119 RVA: 0x0038277C File Offset: 0x0038097C
		public StatesInstance(IStateMachineTarget master, CargoDropperStorage.Def def)
			: base(master, def)
		{
		}

		// Token: 0x060098D0 RID: 39120 RVA: 0x00382788 File Offset: 0x00380988
		public void JettisonCargo(object data)
		{
			Vector3 vector = base.master.transform.GetPosition() + base.def.dropOffset;
			Storage component = base.GetComponent<Storage>();
			if (component != null)
			{
				GameObject gameObject = component.FindFirst("ScoutRover");
				if (gameObject != null)
				{
					component.Drop(gameObject, true);
					Vector3 position = base.master.transform.GetPosition();
					position.z = Grid.GetLayerZ(Grid.SceneLayer.Creatures);
					gameObject.transform.SetPosition(position);
					ChoreProvider component2 = gameObject.GetComponent<ChoreProvider>();
					if (component2 != null)
					{
						KBatchedAnimController component3 = gameObject.GetComponent<KBatchedAnimController>();
						if (component3 != null)
						{
							component3.Play("enter", KAnim.PlayMode.Once, 1f, 0f);
						}
						new EmoteChore(component2, Db.Get().ChoreTypes.EmoteHighPriority, null, new HashedString[] { "enter" }, KAnim.PlayMode.Once, false);
					}
					gameObject.GetMyWorld().SetRoverLanded();
				}
				component.DropAll(vector, false, false, default(Vector3), true, null);
			}
		}
	}
}
