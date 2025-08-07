using System;
using UnityEngine;

// Token: 0x02000E75 RID: 3701
public class URLOpenFunction : MonoBehaviour
{
	// Token: 0x0600760C RID: 30220 RVA: 0x002D2DDE File Offset: 0x002D0FDE
	private void Start()
	{
		if (this.triggerButton != null)
		{
			this.triggerButton.ClearOnClick();
			this.triggerButton.onClick += delegate
			{
				this.OpenUrl(this.fixedURL);
			};
		}
	}

	// Token: 0x0600760D RID: 30221 RVA: 0x002D2E10 File Offset: 0x002D1010
	public void OpenUrl(string url)
	{
		if (url == "blueprints")
		{
			if (LockerMenuScreen.Instance != null)
			{
				LockerMenuScreen.Instance.ShowInventoryScreen();
				return;
			}
		}
		else
		{
			App.OpenWebURL(url);
		}
	}

	// Token: 0x0600760E RID: 30222 RVA: 0x002D2E3D File Offset: 0x002D103D
	public void SetURL(string url)
	{
		this.fixedURL = url;
	}

	// Token: 0x040051DB RID: 20955
	[SerializeField]
	private KButton triggerButton;

	// Token: 0x040051DC RID: 20956
	[SerializeField]
	private string fixedURL;
}
