using System;
using UnityEngine;

// Token: 0x02000AB6 RID: 2742
public class ScreenResize : MonoBehaviour
{
	// Token: 0x06004F83 RID: 20355 RVA: 0x001CBE7C File Offset: 0x001CA07C
	private void Awake()
	{
		ScreenResize.Instance = this;
		this.isFullscreen = Screen.fullScreen;
		this.OnResize = (global::System.Action)Delegate.Combine(this.OnResize, new global::System.Action(this.SaveResolutionToPrefs));
	}

	// Token: 0x06004F84 RID: 20356 RVA: 0x001CBEB4 File Offset: 0x001CA0B4
	private void LateUpdate()
	{
		if (Screen.width != this.Width || Screen.height != this.Height || this.isFullscreen != Screen.fullScreen)
		{
			this.Width = Screen.width;
			this.Height = Screen.height;
			this.isFullscreen = Screen.fullScreen;
			this.TriggerResize();
		}
	}

	// Token: 0x06004F85 RID: 20357 RVA: 0x001CBF0F File Offset: 0x001CA10F
	public void TriggerResize()
	{
		if (this.OnResize != null)
		{
			this.OnResize();
		}
	}

	// Token: 0x06004F86 RID: 20358 RVA: 0x001CBF24 File Offset: 0x001CA124
	private void SaveResolutionToPrefs()
	{
		GraphicsOptionsScreen.OnResize();
	}

	// Token: 0x04003580 RID: 13696
	public global::System.Action OnResize;

	// Token: 0x04003581 RID: 13697
	public static ScreenResize Instance;

	// Token: 0x04003582 RID: 13698
	private int Width;

	// Token: 0x04003583 RID: 13699
	private int Height;

	// Token: 0x04003584 RID: 13700
	private bool isFullscreen;
}
