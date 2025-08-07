using System;
using UnityEngine;

// Token: 0x02000BFE RID: 3070
public class Infrared : MonoBehaviour
{
	// Token: 0x06005C76 RID: 23670 RVA: 0x0021C3D6 File Offset: 0x0021A5D6
	public static void DestroyInstance()
	{
		Infrared.Instance = null;
	}

	// Token: 0x06005C77 RID: 23671 RVA: 0x0021C3DE File Offset: 0x0021A5DE
	private void Awake()
	{
		Infrared.temperatureParametersId = Shader.PropertyToID("_TemperatureParameters");
		Infrared.Instance = this;
		this.OnResize();
		this.UpdateState();
	}

	// Token: 0x06005C78 RID: 23672 RVA: 0x0021C401 File Offset: 0x0021A601
	private void OnRenderImage(RenderTexture source, RenderTexture dest)
	{
		Graphics.Blit(source, this.minionTexture);
		Graphics.Blit(source, dest);
	}

	// Token: 0x06005C79 RID: 23673 RVA: 0x0021C418 File Offset: 0x0021A618
	private void OnResize()
	{
		if (this.minionTexture != null)
		{
			this.minionTexture.DestroyRenderTexture();
		}
		if (this.cameraTexture != null)
		{
			this.cameraTexture.DestroyRenderTexture();
		}
		int num = 2;
		this.minionTexture = new RenderTexture(Screen.width / num, Screen.height / num, 0, RenderTextureFormat.ARGB32);
		this.cameraTexture = new RenderTexture(Screen.width / num, Screen.height / num, 0, RenderTextureFormat.ARGB32);
		base.GetComponent<Camera>().targetTexture = this.cameraTexture;
	}

	// Token: 0x06005C7A RID: 23674 RVA: 0x0021C4A0 File Offset: 0x0021A6A0
	public void SetMode(Infrared.Mode mode)
	{
		Vector4 zero;
		if (mode != Infrared.Mode.Disabled)
		{
			if (mode != Infrared.Mode.Disease)
			{
				zero = new Vector4(1f, 0f, 0f, 0f);
			}
			else
			{
				zero = new Vector4(1f, 0f, 0f, 0f);
				GameComps.InfraredVisualizers.ClearOverlayColour();
			}
		}
		else
		{
			zero = Vector4.zero;
		}
		Shader.SetGlobalVector("_ColouredOverlayParameters", zero);
		this.mode = mode;
		this.UpdateState();
	}

	// Token: 0x06005C7B RID: 23675 RVA: 0x0021C518 File Offset: 0x0021A718
	private void UpdateState()
	{
		base.gameObject.SetActive(this.mode > Infrared.Mode.Disabled);
		if (base.gameObject.activeSelf)
		{
			this.Update();
		}
	}

	// Token: 0x06005C7C RID: 23676 RVA: 0x0021C544 File Offset: 0x0021A744
	private void Update()
	{
		switch (this.mode)
		{
		case Infrared.Mode.Disabled:
			break;
		case Infrared.Mode.Infrared:
			GameComps.InfraredVisualizers.UpdateTemperature();
			return;
		case Infrared.Mode.Disease:
			GameComps.DiseaseContainers.UpdateOverlayColours();
			break;
		default:
			return;
		}
	}

	// Token: 0x04003D3F RID: 15679
	private RenderTexture minionTexture;

	// Token: 0x04003D40 RID: 15680
	private RenderTexture cameraTexture;

	// Token: 0x04003D41 RID: 15681
	private Infrared.Mode mode;

	// Token: 0x04003D42 RID: 15682
	public static int temperatureParametersId;

	// Token: 0x04003D43 RID: 15683
	public static Infrared Instance;

	// Token: 0x02001D3D RID: 7485
	public enum Mode
	{
		// Token: 0x04008894 RID: 34964
		Disabled,
		// Token: 0x04008895 RID: 34965
		Infrared,
		// Token: 0x04008896 RID: 34966
		Disease
	}
}
