using System;
using STRINGS;
using UnityEngine;

// Token: 0x020006DB RID: 1755
[AddComponentMenu("KMonoBehaviour/Workable/AstronautTrainingCenter")]
public class AstronautTrainingCenter : Workable
{
	// Token: 0x06002B48 RID: 11080 RVA: 0x000FA1D5 File Offset: 0x000F83D5
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.chore = this.CreateChore();
	}

	// Token: 0x06002B49 RID: 11081 RVA: 0x000FA1EC File Offset: 0x000F83EC
	private Chore CreateChore()
	{
		return new WorkChore<AstronautTrainingCenter>(Db.Get().ChoreTypes.Train, this, null, true, null, null, null, false, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
	}

	// Token: 0x06002B4A RID: 11082 RVA: 0x000FA21F File Offset: 0x000F841F
	protected override void OnStartWork(WorkerBase worker)
	{
		base.OnStartWork(worker);
		base.GetComponent<Operational>().SetActive(true, false);
	}

	// Token: 0x06002B4B RID: 11083 RVA: 0x000FA235 File Offset: 0x000F8435
	protected override bool OnWorkTick(WorkerBase worker, float dt)
	{
		worker == null;
		return true;
	}

	// Token: 0x06002B4C RID: 11084 RVA: 0x000FA240 File Offset: 0x000F8440
	protected override void OnCompleteWork(WorkerBase worker)
	{
		base.OnCompleteWork(worker);
		if (this.chore != null && !this.chore.isComplete)
		{
			this.chore.Cancel("completed but not complete??");
		}
		this.chore = this.CreateChore();
	}

	// Token: 0x06002B4D RID: 11085 RVA: 0x000FA27A File Offset: 0x000F847A
	protected override void OnStopWork(WorkerBase worker)
	{
		base.OnStopWork(worker);
		base.GetComponent<Operational>().SetActive(false, false);
	}

	// Token: 0x06002B4E RID: 11086 RVA: 0x000FA290 File Offset: 0x000F8490
	public override float GetPercentComplete()
	{
		base.worker == null;
		return 0f;
	}

	// Token: 0x06002B4F RID: 11087 RVA: 0x000FA2A4 File Offset: 0x000F84A4
	public AstronautTrainingCenter()
	{
		Chore.Precondition precondition = default(Chore.Precondition);
		precondition.id = "IsNotMarkedForDeconstruction";
		precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.IS_MARKED_FOR_DECONSTRUCTION;
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			Deconstructable deconstructable = data as Deconstructable;
			return deconstructable == null || !deconstructable.IsMarkedForDeconstruction();
		};
		this.IsNotMarkedForDeconstruction = precondition;
		base..ctor();
	}

	// Token: 0x04001992 RID: 6546
	public float daysToMasterRole;

	// Token: 0x04001993 RID: 6547
	private Chore chore;

	// Token: 0x04001994 RID: 6548
	public Chore.Precondition IsNotMarkedForDeconstruction;
}
