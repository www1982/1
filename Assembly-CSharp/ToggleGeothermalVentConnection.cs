using System;

// Token: 0x0200073C RID: 1852
public class ToggleGeothermalVentConnection : Toggleable
{
	// Token: 0x06002ED8 RID: 11992 RVA: 0x0010CAC4 File Offset: 0x0010ACC4
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.SetWorkTime(10f);
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim(GeothermalVentConfig.TOGGLE_ANIM_OVERRIDE) };
		this.workAnims = new HashedString[] { GeothermalVentConfig.TOGGLE_ANIMATION };
		this.workingPstComplete = null;
		this.workingPstFailed = null;
		this.workLayer = Grid.SceneLayer.Front;
		this.synchronizeAnims = false;
		this.workAnimPlayMode = KAnim.PlayMode.Once;
		base.SetOffsets(new CellOffset[] { CellOffset.none });
	}

	// Token: 0x06002ED9 RID: 11993 RVA: 0x0010CB54 File Offset: 0x0010AD54
	protected override void OnStartWork(WorkerBase worker)
	{
		base.OnStartWork(worker);
		this.buildingAnimController.Play(GeothermalVentConfig.TOGGLE_ANIMATION, KAnim.PlayMode.Once, 1f, 0f);
		if (this.workerFacing == null || this.workerFacing.gameObject != worker.gameObject)
		{
			this.workerFacing = worker.GetComponent<Facing>();
		}
	}

	// Token: 0x06002EDA RID: 11994 RVA: 0x0010CBBA File Offset: 0x0010ADBA
	protected override bool OnWorkTick(WorkerBase worker, float dt)
	{
		if (this.workerFacing != null)
		{
			this.workerFacing.Face(this.workerFacing.transform.GetLocalPosition().x + 0.5f);
		}
		return base.OnWorkTick(worker, dt);
	}

	// Token: 0x04001BB2 RID: 7090
	[MyCmpGet]
	private KBatchedAnimController buildingAnimController;

	// Token: 0x04001BB3 RID: 7091
	private Facing workerFacing;
}
