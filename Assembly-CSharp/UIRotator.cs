using System;
using UnityEngine;

// Token: 0x02000009 RID: 9
[AddComponentMenu("KMonoBehaviour/prefabs/UIRotator")]
public class UIRotator : KMonoBehaviour
{
	// Token: 0x06000021 RID: 33 RVA: 0x00002873 File Offset: 0x00000A73
	protected override void OnPrefabInit()
	{
		this.rotationSpeed = global::UnityEngine.Random.Range(this.minRotationSpeed, this.maxRotationSpeed);
	}

	// Token: 0x06000022 RID: 34 RVA: 0x0000288C File Offset: 0x00000A8C
	private void Update()
	{
		base.GetComponent<RectTransform>().Rotate(0f, 0f, this.rotationSpeed * Time.unscaledDeltaTime);
	}

	// Token: 0x04000028 RID: 40
	public float minRotationSpeed = 1f;

	// Token: 0x04000029 RID: 41
	public float maxRotationSpeed = 1f;

	// Token: 0x0400002A RID: 42
	public float rotationSpeed = 1f;
}
