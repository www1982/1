using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000AB1 RID: 2737
public class MultipleRenderTarget : MonoBehaviour
{
	// Token: 0x1400001E RID: 30
	// (add) Token: 0x06004F65 RID: 20325 RVA: 0x001CB738 File Offset: 0x001C9938
	// (remove) Token: 0x06004F66 RID: 20326 RVA: 0x001CB770 File Offset: 0x001C9970
	public event Action<Camera> onSetupComplete;

	// Token: 0x06004F67 RID: 20327 RVA: 0x001CB7A5 File Offset: 0x001C99A5
	private void Start()
	{
		base.StartCoroutine(this.SetupProxy());
	}

	// Token: 0x06004F68 RID: 20328 RVA: 0x001CB7B4 File Offset: 0x001C99B4
	private IEnumerator SetupProxy()
	{
		yield return null;
		Camera component = base.GetComponent<Camera>();
		Camera camera = new GameObject().AddComponent<Camera>();
		camera.CopyFrom(component);
		this.renderProxy = camera.gameObject.AddComponent<MultipleRenderTargetProxy>();
		camera.name = component.name + " MRT";
		camera.transform.parent = component.transform;
		camera.transform.SetLocalPosition(Vector3.zero);
		camera.depth = component.depth - 1f;
		component.cullingMask = 0;
		component.clearFlags = CameraClearFlags.Color;
		this.quad = new FullScreenQuad("MultipleRenderTarget", component, true);
		if (this.onSetupComplete != null)
		{
			this.onSetupComplete(camera);
		}
		yield break;
	}

	// Token: 0x06004F69 RID: 20329 RVA: 0x001CB7C3 File Offset: 0x001C99C3
	private void OnPreCull()
	{
		if (this.renderProxy != null)
		{
			this.quad.Draw(this.renderProxy.Textures[0]);
		}
	}

	// Token: 0x06004F6A RID: 20330 RVA: 0x001CB7EB File Offset: 0x001C99EB
	public void ToggleColouredOverlayView(bool enabled)
	{
		if (this.renderProxy != null)
		{
			this.renderProxy.ToggleColouredOverlayView(enabled);
		}
	}

	// Token: 0x04003568 RID: 13672
	private MultipleRenderTargetProxy renderProxy;

	// Token: 0x04003569 RID: 13673
	private FullScreenQuad quad;

	// Token: 0x0400356B RID: 13675
	public bool isFrontEnd;
}
