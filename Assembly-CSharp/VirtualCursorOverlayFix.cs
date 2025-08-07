using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000E7D RID: 3709
public class VirtualCursorOverlayFix : MonoBehaviour
{
	// Token: 0x06007640 RID: 30272 RVA: 0x002D3DF8 File Offset: 0x002D1FF8
	private void Awake()
	{
		int width = Screen.currentResolution.width;
		int height = Screen.currentResolution.height;
		this.cursorRendTex = new RenderTexture(width, height, 0);
		this.screenSpaceCamera.enabled = true;
		this.screenSpaceCamera.targetTexture = this.cursorRendTex;
		this.screenSpaceOverlayImage.material.SetTexture("_MainTex", this.cursorRendTex);
		base.StartCoroutine(this.RenderVirtualCursor());
	}

	// Token: 0x06007641 RID: 30273 RVA: 0x002D3E74 File Offset: 0x002D2074
	private IEnumerator RenderVirtualCursor()
	{
		bool ShowCursor = KInputManager.currentControllerIsGamepad;
		while (Application.isPlaying)
		{
			ShowCursor = KInputManager.currentControllerIsGamepad;
			if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftAlt) && Input.GetKey(KeyCode.C))
			{
				ShowCursor = true;
			}
			this.screenSpaceCamera.enabled = true;
			if (!this.screenSpaceOverlayImage.enabled && ShowCursor)
			{
				yield return SequenceUtil.WaitForSecondsRealtime(0.1f);
			}
			this.actualCursor.enabled = ShowCursor;
			this.screenSpaceOverlayImage.enabled = ShowCursor;
			this.screenSpaceOverlayImage.material.SetTexture("_MainTex", this.cursorRendTex);
			yield return null;
		}
		yield break;
	}

	// Token: 0x04005210 RID: 21008
	private RenderTexture cursorRendTex;

	// Token: 0x04005211 RID: 21009
	public Camera screenSpaceCamera;

	// Token: 0x04005212 RID: 21010
	public Image screenSpaceOverlayImage;

	// Token: 0x04005213 RID: 21011
	public RawImage actualCursor;
}
