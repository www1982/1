using System;
using FMOD.Studio;
using UnityEngine;

// Token: 0x02000CAD RID: 3245
public class DLCBetaMessageScreen : KModalScreen
{
	// Token: 0x060063CA RID: 25546 RVA: 0x00257490 File Offset: 0x00255690
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.confirmButton.onClick += delegate
		{
			base.gameObject.SetActive(false);
			AudioMixer.instance.Stop(AudioMixerSnapshots.Get().FrontEndWelcomeScreenSnapshot, STOP_MODE.ALLOWFADEOUT);
		};
		this.quitButton.onClick += delegate
		{
			App.Quit();
		};
	}

	// Token: 0x060063CB RID: 25547 RVA: 0x002574E4 File Offset: 0x002556E4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (!this.betaIsLive || (Application.isEditor && this.skipInEditor) || !DlcManager.IsContentSubscribed("DLC4_ID"))
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		AudioMixer.instance.Start(AudioMixerSnapshots.Get().FrontEndWelcomeScreenSnapshot);
	}

	// Token: 0x060063CC RID: 25548 RVA: 0x0025753B File Offset: 0x0025573B
	private void Update()
	{
		this.logo.rectTransform().localPosition = new Vector3(0f, Mathf.Sin(Time.realtimeSinceStartup) * 7.5f);
	}

	// Token: 0x040043E8 RID: 17384
	public RectTransform logo;

	// Token: 0x040043E9 RID: 17385
	public KButton confirmButton;

	// Token: 0x040043EA RID: 17386
	public KButton quitButton;

	// Token: 0x040043EB RID: 17387
	public LocText bodyText;

	// Token: 0x040043EC RID: 17388
	public RectTransform messageContainer;

	// Token: 0x040043ED RID: 17389
	private bool betaIsLive;

	// Token: 0x040043EE RID: 17390
	private bool skipInEditor;
}
