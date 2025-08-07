using System;
using UnityEngine;

// Token: 0x02000A9F RID: 2719
public class CameraReferenceTexture : MonoBehaviour
{
	// Token: 0x06004EE8 RID: 20200 RVA: 0x001C7D24 File Offset: 0x001C5F24
	private void OnPreCull()
	{
		if (this.quad == null)
		{
			this.quad = new FullScreenQuad("CameraReferenceTexture", base.GetComponent<Camera>(), this.referenceCamera.GetComponent<CameraRenderTexture>().ShouldFlip());
		}
		if (this.referenceCamera != null)
		{
			this.quad.Draw(this.referenceCamera.GetComponent<CameraRenderTexture>().GetTexture());
		}
	}

	// Token: 0x04003467 RID: 13415
	public Camera referenceCamera;

	// Token: 0x04003468 RID: 13416
	private FullScreenQuad quad;
}
