using System;
using UnityEngine;

// Token: 0x02000BF9 RID: 3065
public abstract class VisualizerEffect : MonoBehaviour
{
	// Token: 0x06005C62 RID: 23650
	protected abstract void SetupMaterial();

	// Token: 0x06005C63 RID: 23651
	protected abstract void SetupOcclusionTex();

	// Token: 0x06005C64 RID: 23652
	protected abstract void OnPostRender();

	// Token: 0x06005C65 RID: 23653 RVA: 0x0021BF39 File Offset: 0x0021A139
	protected virtual void Start()
	{
		this.SetupMaterial();
		this.SetupOcclusionTex();
		this.myCamera = base.GetComponent<Camera>();
	}

	// Token: 0x04003D31 RID: 15665
	protected Material material;

	// Token: 0x04003D32 RID: 15666
	protected Camera myCamera;

	// Token: 0x04003D33 RID: 15667
	protected Texture2D OcclusionTex;
}
