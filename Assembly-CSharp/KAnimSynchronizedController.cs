using System;
using UnityEngine;

// Token: 0x02000529 RID: 1321
public class KAnimSynchronizedController
{
	// Token: 0x170000D9 RID: 217
	// (get) Token: 0x06001CCB RID: 7371 RVA: 0x0009BA3C File Offset: 0x00099C3C
	// (set) Token: 0x06001CCC RID: 7372 RVA: 0x0009BA44 File Offset: 0x00099C44
	public string Postfix
	{
		get
		{
			return this.postfix;
		}
		set
		{
			this.postfix = value;
		}
	}

	// Token: 0x06001CCD RID: 7373 RVA: 0x0009BA50 File Offset: 0x00099C50
	public KAnimSynchronizedController(KAnimControllerBase controller, Grid.SceneLayer layer, string postfix)
	{
		this.controller = controller;
		this.Postfix = postfix;
		GameObject gameObject = Util.KInstantiate(EntityPrefabs.Instance.ForegroundLayer, controller.gameObject, null);
		gameObject.name = controller.name + postfix;
		this.synchronizedController = gameObject.GetComponent<KAnimControllerBase>();
		this.synchronizedController.AnimFiles = controller.AnimFiles;
		gameObject.SetActive(true);
		this.synchronizedController.initialAnim = controller.initialAnim + postfix;
		this.synchronizedController.defaultAnim = this.synchronizedController.initialAnim;
		Vector3 vector = new Vector3(0f, 0f, Grid.GetLayerZ(layer) - 0.1f);
		gameObject.transform.SetLocalPosition(vector);
		this.link = new KAnimLink(controller, this.synchronizedController);
		this.Dirty();
		KAnimSynchronizer synchronizer = controller.GetSynchronizer();
		synchronizer.Add(this);
		synchronizer.SyncController(this);
	}

	// Token: 0x06001CCE RID: 7374 RVA: 0x0009BB40 File Offset: 0x00099D40
	public void Enable(bool enable)
	{
		this.synchronizedController.enabled = enable;
	}

	// Token: 0x06001CCF RID: 7375 RVA: 0x0009BB4E File Offset: 0x00099D4E
	public void Play(HashedString anim_name, KAnim.PlayMode mode = KAnim.PlayMode.Once, float speed = 1f, float time_offset = 0f)
	{
		if (this.synchronizedController.enabled && this.synchronizedController.HasAnimation(anim_name))
		{
			this.synchronizedController.Play(anim_name, mode, speed, time_offset);
		}
	}

	// Token: 0x06001CD0 RID: 7376 RVA: 0x0009BB7C File Offset: 0x00099D7C
	public void Dirty()
	{
		if (this.synchronizedController == null)
		{
			return;
		}
		this.synchronizedController.Offset = this.controller.Offset;
		this.synchronizedController.Pivot = this.controller.Pivot;
		this.synchronizedController.Rotation = this.controller.Rotation;
		this.synchronizedController.FlipX = this.controller.FlipX;
		this.synchronizedController.FlipY = this.controller.FlipY;
	}

	// Token: 0x040010D8 RID: 4312
	private KAnimControllerBase controller;

	// Token: 0x040010D9 RID: 4313
	public KAnimControllerBase synchronizedController;

	// Token: 0x040010DA RID: 4314
	private KAnimLink link;

	// Token: 0x040010DB RID: 4315
	private string postfix;
}
