using System;
using UnityEngine;

// Token: 0x020006E2 RID: 1762
[AddComponentMenu("KMonoBehaviour/scripts/BubbleSpawner")]
public class BubbleSpawner : KMonoBehaviour
{
	// Token: 0x06002BC2 RID: 11202 RVA: 0x000FC2C5 File Offset: 0x000FA4C5
	protected override void OnSpawn()
	{
		this.emitMass += (global::UnityEngine.Random.value - 0.5f) * this.emitVariance * this.emitMass;
		base.OnSpawn();
		base.Subscribe<BubbleSpawner>(-1697596308, BubbleSpawner.OnStorageChangedDelegate);
	}

	// Token: 0x06002BC3 RID: 11203 RVA: 0x000FC304 File Offset: 0x000FA504
	private void OnStorageChanged(object data)
	{
		GameObject gameObject = this.storage.FindFirst(ElementLoader.FindElementByHash(this.element).tag);
		if (gameObject == null)
		{
			return;
		}
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		if (component.Mass >= this.emitMass)
		{
			gameObject.GetComponent<PrimaryElement>().Mass -= this.emitMass;
			BubbleManager.instance.SpawnBubble(base.transform.GetPosition(), this.initialVelocity, component.ElementID, this.emitMass, component.Temperature);
		}
	}

	// Token: 0x040019DC RID: 6620
	public SimHashes element;

	// Token: 0x040019DD RID: 6621
	public float emitMass;

	// Token: 0x040019DE RID: 6622
	public float emitVariance;

	// Token: 0x040019DF RID: 6623
	public Vector3 emitOffset = Vector3.zero;

	// Token: 0x040019E0 RID: 6624
	public Vector2 initialVelocity;

	// Token: 0x040019E1 RID: 6625
	[MyCmpGet]
	private Storage storage;

	// Token: 0x040019E2 RID: 6626
	private static readonly EventSystem.IntraObjectHandler<BubbleSpawner> OnStorageChangedDelegate = new EventSystem.IntraObjectHandler<BubbleSpawner>(delegate(BubbleSpawner component, object data)
	{
		component.OnStorageChanged(data);
	});
}
