using System;
using UnityEngine;

// Token: 0x020000D2 RID: 210
internal class BeckonFromSpaceStates : GameStateMachine<BeckonFromSpaceStates, BeckonFromSpaceStates.Instance, IStateMachineTarget, BeckonFromSpaceStates.Def>
{
	// Token: 0x060003AB RID: 939 RVA: 0x0001F208 File Offset: 0x0001D408
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.beckoning;
		this.beckoning.ToggleMainStatusItem(Db.Get().CreatureStatusItems.Beckoning, null).DefaultState(this.beckoning.pre);
		this.beckoning.pre.PlayAnim("beckoning_pre").OnAnimQueueComplete(this.beckoning.loop);
		this.beckoning.loop.PlayAnim("beckoning_loop").Enter(new StateMachine<BeckonFromSpaceStates, BeckonFromSpaceStates.Instance, IStateMachineTarget, BeckonFromSpaceStates.Def>.State.Callback(BeckonFromSpaceStates.MooEchoFX)).OnAnimQueueComplete(this.beckoning.pst);
		this.beckoning.pst.PlayAnim("beckoning_pst").OnAnimQueueComplete(this.behaviourcomplete);
		this.behaviourcomplete.PlayAnim("idle_loop", KAnim.PlayMode.Loop).Enter(new StateMachine<BeckonFromSpaceStates, BeckonFromSpaceStates.Instance, IStateMachineTarget, BeckonFromSpaceStates.Def>.State.Callback(BeckonFromSpaceStates.DoBeckon)).Enter(new StateMachine<BeckonFromSpaceStates, BeckonFromSpaceStates.Instance, IStateMachineTarget, BeckonFromSpaceStates.Def>.State.Callback(BeckonFromSpaceStates.MooCheer))
			.BehaviourComplete(GameTags.Creatures.WantsToBeckon, false);
	}

	// Token: 0x060003AC RID: 940 RVA: 0x0001F308 File Offset: 0x0001D508
	private static void MooEchoFX(BeckonFromSpaceStates.Instance smi)
	{
		KBatchedAnimController kbatchedAnimController = FXHelpers.CreateEffect("moo_call_fx_kanim", smi.master.transform.position, null, false, Grid.SceneLayer.Front, false);
		kbatchedAnimController.destroyOnAnimComplete = true;
		kbatchedAnimController.Play("moo_call", KAnim.PlayMode.Once, 1f, 0f);
	}

	// Token: 0x060003AD RID: 941 RVA: 0x0001F358 File Offset: 0x0001D558
	private static void MooCheer(BeckonFromSpaceStates.Instance smi)
	{
		Vector3 position = smi.transform.GetPosition();
		ListPool<ScenePartitionerEntry, BeckonFromSpaceStates>.PooledList pooledList = ListPool<ScenePartitionerEntry, BeckonFromSpaceStates>.Allocate();
		Extents extents = new Extents((int)position.x, (int)position.y, 15);
		GameScenePartitioner.Instance.GatherEntries(extents, GameScenePartitioner.Instance.pickupablesLayer, pooledList);
		foreach (ScenePartitionerEntry scenePartitionerEntry in pooledList)
		{
			KPrefabID kprefabID = (scenePartitionerEntry.obj as Pickupable).KPrefabID;
			if (!(kprefabID.gameObject == smi.gameObject) && kprefabID.HasTag("Moo") && kprefabID.GetSMI<AnimInterruptMonitor.Instance>() != null)
			{
				kprefabID.GetSMI<AnimInterruptMonitor.Instance>().PlayAnimSequence(smi.def.choirAnims);
			}
		}
		pooledList.Recycle();
	}

	// Token: 0x060003AE RID: 942 RVA: 0x0001F43C File Offset: 0x0001D63C
	private static void DoBeckon(BeckonFromSpaceStates.Instance smi)
	{
		Db.Get().Amounts.Beckoning.Lookup(smi.gameObject).value = 0f;
		WorldContainer myWorld = smi.GetMyWorld();
		Vector3 position = smi.transform.position;
		float num = (float)(myWorld.Height + myWorld.WorldOffset.y - 1);
		float layerZ = Grid.GetLayerZ(smi.def.sceneLayer);
		float num2 = (num - position.y) * Mathf.Tan(0.2617994f);
		float num3 = position.x + (float)global::UnityEngine.Random.Range(-5, 5);
		float num4 = num3 - num2;
		float num5 = num3 + num2;
		float num6 = position.x;
		bool flag = false;
		if (num4 > (float)myWorld.WorldOffset.x && num4 < (float)(myWorld.WorldOffset.x + myWorld.Width))
		{
			num6 = num4;
			flag = false;
		}
		else if (num4 > (float)myWorld.WorldOffset.x && num4 < (float)(myWorld.WorldOffset.x + myWorld.Width))
		{
			num6 = num5;
			flag = true;
		}
		DebugUtil.DevAssert(myWorld.ContainsPoint(new Vector2(num6, num)), "Gassy Moo spawned outside world bounds", null);
		Vector3 vector = new Vector3(num6, num, layerZ);
		GameObject gameObject = Util.KInstantiate(Assets.GetPrefab(smi.def.prefab), vector, Quaternion.identity, null, null, true, 0);
		GassyMooComet component = gameObject.GetComponent<GassyMooComet>();
		if (component != null)
		{
			component.spawnWithOffset = true;
			if (num6 != position.x)
			{
				component.SetCustomInitialFlip(flag);
			}
		}
		gameObject.SetActive(true);
	}

	// Token: 0x040002CB RID: 715
	public BeckonFromSpaceStates.BeckoningState beckoning;

	// Token: 0x040002CC RID: 716
	public GameStateMachine<BeckonFromSpaceStates, BeckonFromSpaceStates.Instance, IStateMachineTarget, BeckonFromSpaceStates.Def>.State behaviourcomplete;

	// Token: 0x0200108A RID: 4234
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x040060C4 RID: 24772
		public string prefab;

		// Token: 0x040060C5 RID: 24773
		public Grid.SceneLayer sceneLayer;

		// Token: 0x040060C6 RID: 24774
		public HashedString[] choirAnims = new HashedString[] { "reply_loop" };
	}

	// Token: 0x0200108B RID: 4235
	public new class Instance : GameStateMachine<BeckonFromSpaceStates, BeckonFromSpaceStates.Instance, IStateMachineTarget, BeckonFromSpaceStates.Def>.GameInstance
	{
		// Token: 0x06008032 RID: 32818 RVA: 0x0032D157 File Offset: 0x0032B357
		public Instance(Chore<BeckonFromSpaceStates.Instance> chore, BeckonFromSpaceStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Creatures.WantsToBeckon);
		}
	}

	// Token: 0x0200108C RID: 4236
	public class BeckoningState : GameStateMachine<BeckonFromSpaceStates, BeckonFromSpaceStates.Instance, IStateMachineTarget, BeckonFromSpaceStates.Def>.State
	{
		// Token: 0x040060C7 RID: 24775
		public GameStateMachine<BeckonFromSpaceStates, BeckonFromSpaceStates.Instance, IStateMachineTarget, BeckonFromSpaceStates.Def>.State pre;

		// Token: 0x040060C8 RID: 24776
		public GameStateMachine<BeckonFromSpaceStates, BeckonFromSpaceStates.Instance, IStateMachineTarget, BeckonFromSpaceStates.Def>.State loop;

		// Token: 0x040060C9 RID: 24777
		public GameStateMachine<BeckonFromSpaceStates, BeckonFromSpaceStates.Instance, IStateMachineTarget, BeckonFromSpaceStates.Def>.State pst;
	}
}
