using System;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

// Token: 0x02000D87 RID: 3463
[AddComponentMenu("KMonoBehaviour/scripts/OpenURLButtons")]
public class OpenURLButtons : KMonoBehaviour
{
	// Token: 0x06006BC1 RID: 27585 RVA: 0x0028AA00 File Offset: 0x00288C00
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		for (int i = 0; i < this.buttonData.Count; i++)
		{
			OpenURLButtons.URLButtonData data = this.buttonData[i];
			GameObject gameObject = Util.KInstantiateUI(this.buttonPrefab, base.gameObject, true);
			string text = Strings.Get(data.stringKey);
			gameObject.GetComponentInChildren<LocText>().SetText(text);
			switch (data.urlType)
			{
			case OpenURLButtons.URLButtonType.url:
				gameObject.GetComponent<KButton>().onClick += delegate
				{
					this.OpenURL(data.url);
				};
				break;
			case OpenURLButtons.URLButtonType.platformUrl:
				gameObject.GetComponent<KButton>().onClick += delegate
				{
					this.OpenPlatformURL(data.url);
				};
				break;
			case OpenURLButtons.URLButtonType.patchNotes:
				gameObject.GetComponent<KButton>().onClick += delegate
				{
					this.OpenPatchNotes();
				};
				break;
			case OpenURLButtons.URLButtonType.feedbackScreen:
				gameObject.GetComponent<KButton>().onClick += delegate
				{
					this.OpenFeedbackScreen();
				};
				break;
			}
		}
	}

	// Token: 0x06006BC2 RID: 27586 RVA: 0x0028AB0B File Offset: 0x00288D0B
	public void OpenPatchNotes()
	{
		Util.KInstantiateUI(this.patchNotesScreenPrefab, FrontEndManager.Instance.gameObject, true);
	}

	// Token: 0x06006BC3 RID: 27587 RVA: 0x0028AB24 File Offset: 0x00288D24
	public void OpenFeedbackScreen()
	{
		Util.KInstantiateUI(this.feedbackScreenPrefab.gameObject, FrontEndManager.Instance.gameObject, true);
	}

	// Token: 0x06006BC4 RID: 27588 RVA: 0x0028AB42 File Offset: 0x00288D42
	public void OpenURL(string URL)
	{
		App.OpenWebURL(URL);
	}

	// Token: 0x06006BC5 RID: 27589 RVA: 0x0028AB4C File Offset: 0x00288D4C
	public void OpenPlatformURL(string URL)
	{
		if (DistributionPlatform.Inst.Platform == "Steam" && DistributionPlatform.Inst.Initialized)
		{
			DistributionPlatform.Inst.GetAuthTicket(delegate(byte[] ticket)
			{
				string text2 = string.Concat(Array.ConvertAll<byte, string>(ticket, (byte x) => x.ToString("X2")));
				App.OpenWebURL(URL.Replace("{SteamID}", DistributionPlatform.Inst.LocalUser.Id.ToInt64().ToString()).Replace("{SteamTicket}", text2));
			});
			return;
		}
		string text = URL.Replace("{SteamID}", "").Replace("{SteamTicket}", "");
		App.OpenWebURL("https://accounts.klei.com/login?goto={gotoUrl}".Replace("{gotoUrl}", WebUtility.HtmlEncode(text)));
	}

	// Token: 0x04004969 RID: 18793
	public GameObject buttonPrefab;

	// Token: 0x0400496A RID: 18794
	public List<OpenURLButtons.URLButtonData> buttonData;

	// Token: 0x0400496B RID: 18795
	[SerializeField]
	private GameObject patchNotesScreenPrefab;

	// Token: 0x0400496C RID: 18796
	[SerializeField]
	private FeedbackScreen feedbackScreenPrefab;

	// Token: 0x02001F6C RID: 8044
	public enum URLButtonType
	{
		// Token: 0x040090C4 RID: 37060
		url,
		// Token: 0x040090C5 RID: 37061
		platformUrl,
		// Token: 0x040090C6 RID: 37062
		patchNotes,
		// Token: 0x040090C7 RID: 37063
		feedbackScreen
	}

	// Token: 0x02001F6D RID: 8045
	[Serializable]
	public class URLButtonData
	{
		// Token: 0x040090C8 RID: 37064
		public string stringKey;

		// Token: 0x040090C9 RID: 37065
		public OpenURLButtons.URLButtonType urlType;

		// Token: 0x040090CA RID: 37066
		public string url;
	}
}
