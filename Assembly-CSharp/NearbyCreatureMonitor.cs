using System;
using System.Collections.Generic;

// Token: 0x02000878 RID: 2168
public class NearbyCreatureMonitor : GameStateMachine<NearbyCreatureMonitor, NearbyCreatureMonitor.Instance, IStateMachineTarget>
{
	// Token: 0x06003BA6 RID: 15270 RVA: 0x0014AD58 File Offset: 0x00148F58
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.Update("UpdateNearbyCreatures", delegate(NearbyCreatureMonitor.Instance smi, float dt)
		{
			smi.UpdateNearbyCreatures(dt);
		}, UpdateRate.SIM_1000ms, false);
	}

	// Token: 0x0200182D RID: 6189
	public new class Instance : GameStateMachine<NearbyCreatureMonitor, NearbyCreatureMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x14000035 RID: 53
		// (add) Token: 0x06009BCE RID: 39886 RVA: 0x0038EFB4 File Offset: 0x0038D1B4
		// (remove) Token: 0x06009BCF RID: 39887 RVA: 0x0038EFEC File Offset: 0x0038D1EC
		public event Action<float, List<KPrefabID>, List<KPrefabID>> OnUpdateNearbyCreatures;

		// Token: 0x06009BD0 RID: 39888 RVA: 0x0038F021 File Offset: 0x0038D221
		public Instance(IStateMachineTarget master)
			: base(master)
		{
		}

		// Token: 0x06009BD1 RID: 39889 RVA: 0x0038F02C File Offset: 0x0038D22C
		public void UpdateNearbyCreatures(float dt)
		{
			CavityInfo cavityForCell = Game.Instance.roomProber.GetCavityForCell(Grid.PosToCell(base.gameObject));
			if (cavityForCell != null)
			{
				this.OnUpdateNearbyCreatures(dt, cavityForCell.creatures, cavityForCell.eggs);
			}
		}
	}
}
