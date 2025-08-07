using System;
using System.Collections.Generic;

// Token: 0x0200052A RID: 1322
public class KAnimSynchronizer
{
	// Token: 0x170000DA RID: 218
	// (get) Token: 0x06001CD1 RID: 7377 RVA: 0x0009BC06 File Offset: 0x00099E06
	// (set) Token: 0x06001CD2 RID: 7378 RVA: 0x0009BC0E File Offset: 0x00099E0E
	public string IdleAnim
	{
		get
		{
			return this.idle_anim;
		}
		set
		{
			this.idle_anim = value;
		}
	}

	// Token: 0x06001CD3 RID: 7379 RVA: 0x0009BC17 File Offset: 0x00099E17
	public KAnimSynchronizer(KAnimControllerBase master_controller)
	{
		this.masterController = master_controller;
	}

	// Token: 0x06001CD4 RID: 7380 RVA: 0x0009BC47 File Offset: 0x00099E47
	private void Clear(KAnimControllerBase controller)
	{
		controller.Play(this.IdleAnim, KAnim.PlayMode.Loop, 1f, 0f);
	}

	// Token: 0x06001CD5 RID: 7381 RVA: 0x0009BC65 File Offset: 0x00099E65
	public void Add(KAnimControllerBase controller)
	{
		this.Targets.Add(controller);
	}

	// Token: 0x06001CD6 RID: 7382 RVA: 0x0009BC73 File Offset: 0x00099E73
	public void Remove(KAnimControllerBase controller)
	{
		this.Clear(controller);
		this.Targets.Remove(controller);
	}

	// Token: 0x06001CD7 RID: 7383 RVA: 0x0009BC89 File Offset: 0x00099E89
	public void RemoveWithoutIdleAnim(KAnimControllerBase controller)
	{
		this.Targets.Remove(controller);
	}

	// Token: 0x06001CD8 RID: 7384 RVA: 0x0009BC98 File Offset: 0x00099E98
	private void Clear(KAnimSynchronizedController controller)
	{
		controller.Play(this.IdleAnim, KAnim.PlayMode.Loop, 1f, 0f);
	}

	// Token: 0x06001CD9 RID: 7385 RVA: 0x0009BCB6 File Offset: 0x00099EB6
	public void Add(KAnimSynchronizedController controller)
	{
		this.SyncedControllers.Add(controller);
	}

	// Token: 0x06001CDA RID: 7386 RVA: 0x0009BCC4 File Offset: 0x00099EC4
	public void Remove(KAnimSynchronizedController controller)
	{
		this.Clear(controller);
		this.SyncedControllers.Remove(controller);
	}

	// Token: 0x06001CDB RID: 7387 RVA: 0x0009BCDC File Offset: 0x00099EDC
	public void Clear()
	{
		foreach (KAnimControllerBase kanimControllerBase in this.Targets)
		{
			if (!(kanimControllerBase == null) && kanimControllerBase.AnimFiles != null)
			{
				this.Clear(kanimControllerBase);
			}
		}
		this.Targets.Clear();
		foreach (KAnimSynchronizedController kanimSynchronizedController in this.SyncedControllers)
		{
			if (!(kanimSynchronizedController.synchronizedController == null) && kanimSynchronizedController.synchronizedController.AnimFiles != null)
			{
				this.Clear(kanimSynchronizedController);
			}
		}
		this.SyncedControllers.Clear();
	}

	// Token: 0x06001CDC RID: 7388 RVA: 0x0009BDB4 File Offset: 0x00099FB4
	public void Sync(KAnimControllerBase controller)
	{
		if (this.masterController == null)
		{
			return;
		}
		if (controller == null)
		{
			return;
		}
		KAnim.Anim currentAnim = this.masterController.GetCurrentAnim();
		if (currentAnim != null && !string.IsNullOrEmpty(controller.defaultAnim) && !controller.HasAnimation(currentAnim.name))
		{
			controller.Play(controller.defaultAnim, KAnim.PlayMode.Loop, 1f, 0f);
			return;
		}
		if (currentAnim == null)
		{
			return;
		}
		KAnim.PlayMode mode = this.masterController.GetMode();
		float playSpeed = this.masterController.GetPlaySpeed();
		float elapsedTime = this.masterController.GetElapsedTime();
		controller.Play(currentAnim.name, mode, playSpeed, elapsedTime);
		Facing component = controller.GetComponent<Facing>();
		if (component != null)
		{
			float num = component.transform.GetPosition().x;
			num += (this.masterController.FlipX ? (-0.5f) : 0.5f);
			component.Face(num);
			return;
		}
		controller.FlipX = this.masterController.FlipX;
		controller.FlipY = this.masterController.FlipY;
	}

	// Token: 0x06001CDD RID: 7389 RVA: 0x0009BED4 File Offset: 0x0009A0D4
	public void SyncController(KAnimSynchronizedController controller)
	{
		if (this.masterController == null)
		{
			return;
		}
		if (controller == null)
		{
			return;
		}
		KAnim.Anim currentAnim = this.masterController.GetCurrentAnim();
		string text = ((currentAnim != null) ? (currentAnim.name + controller.Postfix) : string.Empty);
		if (!string.IsNullOrEmpty(controller.synchronizedController.defaultAnim) && !controller.synchronizedController.HasAnimation(text))
		{
			controller.Play(controller.synchronizedController.defaultAnim, KAnim.PlayMode.Loop, 1f, 0f);
			return;
		}
		if (currentAnim == null)
		{
			return;
		}
		KAnim.PlayMode mode = this.masterController.GetMode();
		float playSpeed = this.masterController.GetPlaySpeed();
		float elapsedTime = this.masterController.GetElapsedTime();
		controller.Play(text, mode, playSpeed, elapsedTime);
		Facing component = controller.synchronizedController.GetComponent<Facing>();
		if (component != null)
		{
			float num = component.transform.GetPosition().x;
			num += (this.masterController.FlipX ? (-0.5f) : 0.5f);
			component.Face(num);
			return;
		}
		controller.synchronizedController.FlipX = this.masterController.FlipX;
		controller.synchronizedController.FlipY = this.masterController.FlipY;
	}

	// Token: 0x06001CDE RID: 7390 RVA: 0x0009C01C File Offset: 0x0009A21C
	public void Sync()
	{
		for (int i = 0; i < this.Targets.Count; i++)
		{
			KAnimControllerBase kanimControllerBase = this.Targets[i];
			this.Sync(kanimControllerBase);
		}
		for (int j = 0; j < this.SyncedControllers.Count; j++)
		{
			KAnimSynchronizedController kanimSynchronizedController = this.SyncedControllers[j];
			this.SyncController(kanimSynchronizedController);
		}
	}

	// Token: 0x06001CDF RID: 7391 RVA: 0x0009C080 File Offset: 0x0009A280
	public void SyncTime()
	{
		float elapsedTime = this.masterController.GetElapsedTime();
		for (int i = 0; i < this.Targets.Count; i++)
		{
			this.Targets[i].SetElapsedTime(elapsedTime);
		}
		for (int j = 0; j < this.SyncedControllers.Count; j++)
		{
			this.SyncedControllers[j].synchronizedController.SetElapsedTime(elapsedTime);
		}
	}

	// Token: 0x040010DC RID: 4316
	private string idle_anim = "idle_default";

	// Token: 0x040010DD RID: 4317
	private KAnimControllerBase masterController;

	// Token: 0x040010DE RID: 4318
	private List<KAnimControllerBase> Targets = new List<KAnimControllerBase>();

	// Token: 0x040010DF RID: 4319
	private List<KAnimSynchronizedController> SyncedControllers = new List<KAnimSynchronizedController>();
}
