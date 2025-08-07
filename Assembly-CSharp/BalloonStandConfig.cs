using System;
using Klei.AI;
using UnityEngine;

// Token: 0x020002E3 RID: 739
public class BalloonStandConfig : IEntityConfig
{
	// Token: 0x06000F06 RID: 3846 RVA: 0x000589F4 File Offset: 0x00056BF4
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity(BalloonStandConfig.ID, BalloonStandConfig.ID, false);
		KAnimFile[] array = new KAnimFile[] { Assets.GetAnim("anim_interacts_balloon_receiver_kanim") };
		GetBalloonWorkable getBalloonWorkable = gameObject.AddOrGet<GetBalloonWorkable>();
		getBalloonWorkable.workTime = 2f;
		getBalloonWorkable.workLayer = Grid.SceneLayer.BuildingFront;
		getBalloonWorkable.overrideAnims = array;
		getBalloonWorkable.synchronizeAnims = false;
		return gameObject;
	}

	// Token: 0x06000F07 RID: 3847 RVA: 0x00058A52 File Offset: 0x00056C52
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000F08 RID: 3848 RVA: 0x00058A54 File Offset: 0x00056C54
	public void OnSpawn(GameObject inst)
	{
		GetBalloonWorkable component = inst.GetComponent<GetBalloonWorkable>();
		WorkChore<GetBalloonWorkable> workChore = new WorkChore<GetBalloonWorkable>(Db.Get().ChoreTypes.JoyReaction, component, null, true, new Action<Chore>(this.MakeNewBalloonChore), null, null, true, Db.Get().ScheduleBlockTypes.Recreation, false, true, null, false, true, true, PriorityScreen.PriorityClass.high, 5, true, true);
		workChore.AddPrecondition(this.HasNoBalloon, workChore);
		workChore.AddPrecondition(ChorePreconditions.instance.IsNotARobot, null);
		component.GetBalloonArtist().NextBalloonOverride();
	}

	// Token: 0x06000F09 RID: 3849 RVA: 0x00058AD4 File Offset: 0x00056CD4
	private void MakeNewBalloonChore(Chore chore)
	{
		GetBalloonWorkable component = chore.target.GetComponent<GetBalloonWorkable>();
		WorkChore<GetBalloonWorkable> workChore = new WorkChore<GetBalloonWorkable>(Db.Get().ChoreTypes.JoyReaction, component, null, true, new Action<Chore>(this.MakeNewBalloonChore), null, null, true, Db.Get().ScheduleBlockTypes.Recreation, false, true, null, false, true, true, PriorityScreen.PriorityClass.high, 5, true, true);
		workChore.AddPrecondition(this.HasNoBalloon, workChore);
		workChore.AddPrecondition(ChorePreconditions.instance.IsNotARobot, null);
		component.GetBalloonArtist().NextBalloonOverride();
	}

	// Token: 0x06000F0A RID: 3850 RVA: 0x00058B58 File Offset: 0x00056D58
	public BalloonStandConfig()
	{
		Chore.Precondition precondition = default(Chore.Precondition);
		precondition.id = "HasNoBalloon";
		precondition.description = "__ Duplicant doesn't have a balloon already";
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			return !(context.consumerState.consumer == null) && !context.consumerState.gameObject.GetComponent<Effects>().HasEffect("HasBalloon");
		};
		this.HasNoBalloon = precondition;
		base..ctor();
	}

	// Token: 0x040009CB RID: 2507
	public static readonly string ID = "BalloonStand";

	// Token: 0x040009CC RID: 2508
	private Chore.Precondition HasNoBalloon;
}
