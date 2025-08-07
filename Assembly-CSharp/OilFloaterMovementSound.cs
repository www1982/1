using System;

// Token: 0x020000B5 RID: 181
internal class OilFloaterMovementSound : KMonoBehaviour
{
	// Token: 0x0600033B RID: 827 RVA: 0x0001A678 File Offset: 0x00018878
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.sound = GlobalAssets.GetSound(this.sound, false);
		base.Subscribe<OilFloaterMovementSound>(1027377649, OilFloaterMovementSound.OnObjectMovementStateChangedDelegate);
		Singleton<CellChangeMonitor>.Instance.RegisterCellChangedHandler(base.transform, new global::System.Action(this.OnCellChanged), "OilFloaterMovementSound");
	}

	// Token: 0x0600033C RID: 828 RVA: 0x0001A6D0 File Offset: 0x000188D0
	private void OnObjectMovementStateChanged(object data)
	{
		GameHashes gameHashes = (GameHashes)data;
		this.isMoving = gameHashes == GameHashes.ObjectMovementWakeUp;
		this.UpdateSound();
	}

	// Token: 0x0600033D RID: 829 RVA: 0x0001A6F8 File Offset: 0x000188F8
	private void OnCellChanged()
	{
		this.UpdateSound();
	}

	// Token: 0x0600033E RID: 830 RVA: 0x0001A700 File Offset: 0x00018900
	private void UpdateSound()
	{
		bool flag = this.isMoving && base.GetComponent<Navigator>().CurrentNavType != NavType.Swim;
		if (flag == this.isPlayingSound)
		{
			return;
		}
		LoopingSounds component = base.GetComponent<LoopingSounds>();
		if (flag)
		{
			component.StartSound(this.sound);
		}
		else
		{
			component.StopSound(this.sound);
		}
		this.isPlayingSound = flag;
	}

	// Token: 0x0600033F RID: 831 RVA: 0x0001A760 File Offset: 0x00018960
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Singleton<CellChangeMonitor>.Instance.UnregisterCellChangedHandler(base.transform, new global::System.Action(this.OnCellChanged));
	}

	// Token: 0x0400024E RID: 590
	public string sound;

	// Token: 0x0400024F RID: 591
	public bool isPlayingSound;

	// Token: 0x04000250 RID: 592
	public bool isMoving;

	// Token: 0x04000251 RID: 593
	private static readonly EventSystem.IntraObjectHandler<OilFloaterMovementSound> OnObjectMovementStateChangedDelegate = new EventSystem.IntraObjectHandler<OilFloaterMovementSound>(delegate(OilFloaterMovementSound component, object data)
	{
		component.OnObjectMovementStateChanged(data);
	});
}
