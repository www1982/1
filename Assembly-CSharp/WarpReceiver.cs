using System;
using System.Linq;
using KSerialization;

// Token: 0x020007F4 RID: 2036
public class WarpReceiver : Workable
{
	// Token: 0x0600376B RID: 14187 RVA: 0x00133E6C File Offset: 0x0013206C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x0600376C RID: 14188 RVA: 0x00133E74 File Offset: 0x00132074
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.warpReceiverSMI = new WarpReceiver.WarpReceiverSM.Instance(this);
		this.warpReceiverSMI.StartSM();
		Components.WarpReceivers.Add(this);
	}

	// Token: 0x0600376D RID: 14189 RVA: 0x00133EA0 File Offset: 0x001320A0
	public void ReceiveWarpedDuplicant(WorkerBase dupe)
	{
		dupe.transform.SetPosition(Grid.CellToPos(Grid.PosToCell(this), CellAlignment.Bottom, Grid.SceneLayer.Move));
		Debug.Assert(this.chore == null);
		KAnimFile anim = Assets.GetAnim("anim_interacts_warp_portal_receiver_kanim");
		ChoreType migrate = Db.Get().ChoreTypes.Migrate;
		KAnimFile kanimFile = anim;
		this.chore = new WorkChore<Workable>(migrate, this, dupe.GetComponent<ChoreProvider>(), true, delegate(Chore o)
		{
			this.CompleteChore();
		}, null, null, true, null, true, true, kanimFile, false, true, false, PriorityScreen.PriorityClass.compulsory, 5, false, true);
		Workable component = base.GetComponent<Workable>();
		component.workLayer = Grid.SceneLayer.Building;
		component.workAnims = new HashedString[] { "printing_pre", "printing_loop" };
		component.workingPstComplete = new HashedString[] { "printing_pst" };
		component.workingPstFailed = new HashedString[] { "printing_pst" };
		component.synchronizeAnims = true;
		float num = 0f;
		KAnimFileData data = anim.GetData();
		for (int i = 0; i < data.animCount; i++)
		{
			KAnim.Anim anim2 = data.GetAnim(i);
			if (component.workAnims.Contains(anim2.hash))
			{
				num += anim2.totalTime;
			}
		}
		component.SetWorkTime(num);
		this.Used = true;
	}

	// Token: 0x0600376E RID: 14190 RVA: 0x00133FFB File Offset: 0x001321FB
	private void CompleteChore()
	{
		this.chore.Cleanup();
		this.chore = null;
		this.warpReceiverSMI.GoTo(this.warpReceiverSMI.sm.idle);
	}

	// Token: 0x0600376F RID: 14191 RVA: 0x0013402A File Offset: 0x0013222A
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Components.WarpReceivers.Remove(this);
	}

	// Token: 0x040021A2 RID: 8610
	[MyCmpAdd]
	public Notifier notifier;

	// Token: 0x040021A3 RID: 8611
	private WarpReceiver.WarpReceiverSM.Instance warpReceiverSMI;

	// Token: 0x040021A4 RID: 8612
	private Notification notification;

	// Token: 0x040021A5 RID: 8613
	[Serialize]
	public bool IsConsumed;

	// Token: 0x040021A6 RID: 8614
	private Chore chore;

	// Token: 0x040021A7 RID: 8615
	[Serialize]
	public bool Used;

	// Token: 0x02001755 RID: 5973
	public class WarpReceiverSM : GameStateMachine<WarpReceiver.WarpReceiverSM, WarpReceiver.WarpReceiverSM.Instance, WarpReceiver>
	{
		// Token: 0x060098AF RID: 39087 RVA: 0x00382145 File Offset: 0x00380345
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.idle;
			this.idle.PlayAnim("idle");
		}

		// Token: 0x04007567 RID: 30055
		public GameStateMachine<WarpReceiver.WarpReceiverSM, WarpReceiver.WarpReceiverSM.Instance, WarpReceiver, object>.State idle;

		// Token: 0x02002800 RID: 10240
		public new class Instance : GameStateMachine<WarpReceiver.WarpReceiverSM, WarpReceiver.WarpReceiverSM.Instance, WarpReceiver, object>.GameInstance
		{
			// Token: 0x0600CA4B RID: 51787 RVA: 0x004173F2 File Offset: 0x004155F2
			public Instance(WarpReceiver master)
				: base(master)
			{
			}
		}
	}
}
