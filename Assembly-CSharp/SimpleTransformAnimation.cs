using System;
using UnityEngine;

// Token: 0x02000E4B RID: 3659
public class SimpleTransformAnimation : MonoBehaviour
{
	// Token: 0x06007459 RID: 29785 RVA: 0x002C45C3 File Offset: 0x002C27C3
	private void Start()
	{
	}

	// Token: 0x0600745A RID: 29786 RVA: 0x002C45C5 File Offset: 0x002C27C5
	private void Update()
	{
		base.transform.Rotate(this.rotationSpeed * Time.unscaledDeltaTime);
		base.transform.Translate(this.translateSpeed * Time.unscaledDeltaTime);
	}

	// Token: 0x04005036 RID: 20534
	[SerializeField]
	private Vector3 rotationSpeed;

	// Token: 0x04005037 RID: 20535
	[SerializeField]
	private Vector3 translateSpeed;
}
