using System;
using Database;
using UnityEngine;

// Token: 0x0200073F RID: 1855
[AddComponentMenu("KMonoBehaviour/Workable/GetBalloonWorkable")]
public class GetBalloonWorkable : Workable
{
	// Token: 0x06002F03 RID: 12035 RVA: 0x0010DA8C File Offset: 0x0010BC8C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.faceTargetWhenWorking = true;
		this.workerStatusItem = null;
		this.workingStatusItem = null;
		this.workAnims = GetBalloonWorkable.GET_BALLOON_ANIMS;
		this.workingPstComplete = new HashedString[] { GetBalloonWorkable.PST_ANIM };
		this.workingPstFailed = new HashedString[] { GetBalloonWorkable.PST_ANIM };
	}

	// Token: 0x06002F04 RID: 12036 RVA: 0x0010DAF0 File Offset: 0x0010BCF0
	protected override void OnStartWork(WorkerBase worker)
	{
		base.OnStartWork(worker);
		BalloonOverrideSymbol balloonOverride = this.balloonArtist.GetBalloonOverride();
		if (balloonOverride.animFile.IsNone())
		{
			worker.gameObject.GetComponent<SymbolOverrideController>().AddSymbolOverride("body", Assets.GetAnim("balloon_anim_kanim").GetData().build.GetSymbol("body"), 0);
			return;
		}
		worker.gameObject.GetComponent<SymbolOverrideController>().AddSymbolOverride("body", balloonOverride.symbol.Unwrap(), 0);
	}

	// Token: 0x06002F05 RID: 12037 RVA: 0x0010DB8C File Offset: 0x0010BD8C
	protected override void OnCompleteWork(WorkerBase worker)
	{
		GameObject gameObject = Util.KInstantiate(Assets.GetPrefab("EquippableBalloon"), worker.transform.GetPosition());
		gameObject.GetComponent<Equippable>().Assign(worker.GetComponent<MinionIdentity>());
		gameObject.GetComponent<Equippable>().isEquipped = true;
		gameObject.SetActive(true);
		base.OnCompleteWork(worker);
		BalloonOverrideSymbol balloonOverride = this.balloonArtist.GetBalloonOverride();
		this.balloonArtist.GiveBalloon(balloonOverride);
		gameObject.GetComponent<EquippableBalloon>().SetBalloonOverride(balloonOverride);
	}

	// Token: 0x06002F06 RID: 12038 RVA: 0x0010DC06 File Offset: 0x0010BE06
	public override Vector3 GetFacingTarget()
	{
		return this.balloonArtist.master.transform.GetPosition();
	}

	// Token: 0x06002F07 RID: 12039 RVA: 0x0010DC1D File Offset: 0x0010BE1D
	public void SetBalloonArtist(BalloonArtistChore.StatesInstance chore)
	{
		this.balloonArtist = chore;
	}

	// Token: 0x06002F08 RID: 12040 RVA: 0x0010DC26 File Offset: 0x0010BE26
	public BalloonArtistChore.StatesInstance GetBalloonArtist()
	{
		return this.balloonArtist;
	}

	// Token: 0x04001BC9 RID: 7113
	private static readonly HashedString[] GET_BALLOON_ANIMS = new HashedString[] { "working_pre", "working_loop" };

	// Token: 0x04001BCA RID: 7114
	private static readonly HashedString PST_ANIM = new HashedString("working_pst");

	// Token: 0x04001BCB RID: 7115
	private BalloonArtistChore.StatesInstance balloonArtist;

	// Token: 0x04001BCC RID: 7116
	private const string TARGET_SYMBOL_TO_OVERRIDE = "body";

	// Token: 0x04001BCD RID: 7117
	private const int TARGET_OVERRIDE_PRIORITY = 0;
}
