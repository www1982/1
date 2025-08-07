using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200052C RID: 1324
[AddComponentMenu("KMonoBehaviour/scripts/KBatchedAnimEventToggler")]
public class KBatchedAnimEventToggler : KMonoBehaviour
{
	// Token: 0x06001D29 RID: 7465 RVA: 0x0009DBB0 File Offset: 0x0009BDB0
	protected override void OnPrefabInit()
	{
		Vector3 position = this.eventSource.transform.GetPosition();
		position.z = Grid.GetLayerZ(Grid.SceneLayer.Front);
		int num = LayerMask.NameToLayer("Default");
		foreach (KBatchedAnimEventToggler.Entry entry in this.entries)
		{
			entry.controller.transform.SetPosition(position);
			entry.controller.SetLayer(num);
			entry.controller.gameObject.SetActive(false);
		}
		int num2 = Hash.SDBMLower(this.enableEvent);
		int num3 = Hash.SDBMLower(this.disableEvent);
		base.Subscribe(this.eventSource, num2, new Action<object>(this.Enable));
		base.Subscribe(this.eventSource, num3, new Action<object>(this.Disable));
	}

	// Token: 0x06001D2A RID: 7466 RVA: 0x0009DCA0 File Offset: 0x0009BEA0
	protected override void OnSpawn()
	{
		this.animEventHandler = base.GetComponentInParent<AnimEventHandler>();
	}

	// Token: 0x06001D2B RID: 7467 RVA: 0x0009DCB0 File Offset: 0x0009BEB0
	private void Enable(object data)
	{
		this.StopAll();
		HashedString context = this.animEventHandler.GetContext();
		if (!context.IsValid)
		{
			return;
		}
		foreach (KBatchedAnimEventToggler.Entry entry in this.entries)
		{
			if (entry.context == context)
			{
				entry.controller.gameObject.SetActive(true);
				entry.controller.Play(entry.anim, KAnim.PlayMode.Loop, 1f, 0f);
			}
		}
	}

	// Token: 0x06001D2C RID: 7468 RVA: 0x0009DD58 File Offset: 0x0009BF58
	private void Disable(object data)
	{
		this.StopAll();
	}

	// Token: 0x06001D2D RID: 7469 RVA: 0x0009DD60 File Offset: 0x0009BF60
	private void StopAll()
	{
		foreach (KBatchedAnimEventToggler.Entry entry in this.entries)
		{
			entry.controller.StopAndClear();
			entry.controller.gameObject.SetActive(false);
		}
	}

	// Token: 0x040010F9 RID: 4345
	[SerializeField]
	public GameObject eventSource;

	// Token: 0x040010FA RID: 4346
	[SerializeField]
	public string enableEvent;

	// Token: 0x040010FB RID: 4347
	[SerializeField]
	public string disableEvent;

	// Token: 0x040010FC RID: 4348
	[SerializeField]
	public List<KBatchedAnimEventToggler.Entry> entries;

	// Token: 0x040010FD RID: 4349
	private AnimEventHandler animEventHandler;

	// Token: 0x02001387 RID: 4999
	[Serializable]
	public struct Entry
	{
		// Token: 0x04006995 RID: 27029
		public string anim;

		// Token: 0x04006996 RID: 27030
		public HashedString context;

		// Token: 0x04006997 RID: 27031
		public KBatchedAnimController controller;
	}
}
