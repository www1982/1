using System;
using FMOD.Studio;
using STRINGS;
using UnityEngine;

// Token: 0x02000C20 RID: 3104
public class ImmigrantScreen : CharacterSelectionController
{
	// Token: 0x06005DF3 RID: 24051 RVA: 0x002259A8 File Offset: 0x00223BA8
	public static void DestroyInstance()
	{
		ImmigrantScreen.instance = null;
	}

	// Token: 0x170006E3 RID: 1763
	// (get) Token: 0x06005DF4 RID: 24052 RVA: 0x002259B0 File Offset: 0x00223BB0
	public Telepad Telepad
	{
		get
		{
			return this.telepad;
		}
	}

	// Token: 0x06005DF5 RID: 24053 RVA: 0x002259B8 File Offset: 0x00223BB8
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x06005DF6 RID: 24054 RVA: 0x002259C0 File Offset: 0x00223BC0
	protected override void OnSpawn()
	{
		this.activateOnSpawn = false;
		base.ConsumeMouseScroll = false;
		base.OnSpawn();
		base.IsStarterMinion = false;
		this.rejectButton.onClick += this.OnRejectAll;
		this.confirmRejectionBtn.onClick += this.OnRejectionConfirmed;
		this.cancelRejectionBtn.onClick += this.OnRejectionCancelled;
		ImmigrantScreen.instance = this;
		this.title.text = UI.IMMIGRANTSCREEN.IMMIGRANTSCREENTITLE;
		this.proceedButton.GetComponentInChildren<LocText>().text = UI.IMMIGRANTSCREEN.PROCEEDBUTTON;
		this.closeButton.onClick += delegate
		{
			this.Show(false);
		};
		this.Show(false);
	}

	// Token: 0x06005DF7 RID: 24055 RVA: 0x00225A80 File Offset: 0x00223C80
	protected override void OnShow(bool show)
	{
		if (show)
		{
			KFMOD.PlayUISound(GlobalAssets.GetSound("Dialog_Popup", false));
			AudioMixer.instance.Start(AudioMixerSnapshots.Get().MENUNewDuplicantSnapshot);
			MusicManager.instance.PlaySong("Music_SelectDuplicant", false);
			this.hasShown = true;
		}
		else
		{
			AudioMixer.instance.Stop(AudioMixerSnapshots.Get().MENUNewDuplicantSnapshot, STOP_MODE.ALLOWFADEOUT);
			if (MusicManager.instance.SongIsPlaying("Music_SelectDuplicant"))
			{
				MusicManager.instance.StopSong("Music_SelectDuplicant", true, STOP_MODE.ALLOWFADEOUT);
			}
			if (Immigration.Instance.ImmigrantsAvailable && this.hasShown)
			{
				AudioMixer.instance.Start(AudioMixerSnapshots.Get().PortalLPDimmedSnapshot);
			}
		}
		base.OnShow(show);
	}

	// Token: 0x06005DF8 RID: 24056 RVA: 0x00225B36 File Offset: 0x00223D36
	public void DebugShuffleOptions()
	{
		this.OnRejectionConfirmed();
		Immigration.Instance.timeBeforeSpawn = 0f;
	}

	// Token: 0x06005DF9 RID: 24057 RVA: 0x00225B4D File Offset: 0x00223D4D
	public override void OnPressBack()
	{
		if (this.rejectConfirmationScreen.activeSelf)
		{
			this.OnRejectionCancelled();
			return;
		}
		base.OnPressBack();
	}

	// Token: 0x06005DFA RID: 24058 RVA: 0x00225B69 File Offset: 0x00223D69
	public override void Deactivate()
	{
		this.Show(false);
	}

	// Token: 0x06005DFB RID: 24059 RVA: 0x00225B72 File Offset: 0x00223D72
	public static void InitializeImmigrantScreen(Telepad telepad)
	{
		ImmigrantScreen.instance.Initialize(telepad);
		ImmigrantScreen.instance.Show(true);
	}

	// Token: 0x06005DFC RID: 24060 RVA: 0x00225B8C File Offset: 0x00223D8C
	private void Initialize(Telepad telepad)
	{
		this.InitializeContainers();
		foreach (ITelepadDeliverableContainer telepadDeliverableContainer in this.containers)
		{
			CharacterContainer characterContainer = telepadDeliverableContainer as CharacterContainer;
			if (characterContainer != null)
			{
				characterContainer.SetReshufflingState(false);
			}
		}
		this.telepad = telepad;
	}

	// Token: 0x06005DFD RID: 24061 RVA: 0x00225BFC File Offset: 0x00223DFC
	protected override void OnProceed()
	{
		this.telepad.OnAcceptDelivery(this.selectedDeliverables[0]);
		this.Show(false);
		this.containers.ForEach(delegate(ITelepadDeliverableContainer cc)
		{
			global::UnityEngine.Object.Destroy(cc.GetGameObject());
		});
		this.containers.Clear();
		AudioMixer.instance.Stop(AudioMixerSnapshots.Get().MENUNewDuplicantSnapshot, STOP_MODE.ALLOWFADEOUT);
		AudioMixer.instance.Stop(AudioMixerSnapshots.Get().PortalLPDimmedSnapshot, STOP_MODE.ALLOWFADEOUT);
		MusicManager.instance.PlaySong("Stinger_NewDuplicant", false);
	}

	// Token: 0x06005DFE RID: 24062 RVA: 0x00225C98 File Offset: 0x00223E98
	private void OnRejectAll()
	{
		this.rejectConfirmationScreen.transform.SetAsLastSibling();
		this.rejectConfirmationScreen.SetActive(true);
	}

	// Token: 0x06005DFF RID: 24063 RVA: 0x00225CB6 File Offset: 0x00223EB6
	private void OnRejectionCancelled()
	{
		this.rejectConfirmationScreen.SetActive(false);
	}

	// Token: 0x06005E00 RID: 24064 RVA: 0x00225CC4 File Offset: 0x00223EC4
	private void OnRejectionConfirmed()
	{
		this.telepad.RejectAll();
		this.containers.ForEach(delegate(ITelepadDeliverableContainer cc)
		{
			global::UnityEngine.Object.Destroy(cc.GetGameObject());
		});
		this.containers.Clear();
		this.rejectConfirmationScreen.SetActive(false);
		this.Show(false);
		AudioMixer.instance.Stop(AudioMixerSnapshots.Get().MENUNewDuplicantSnapshot, STOP_MODE.ALLOWFADEOUT);
		AudioMixer.instance.Stop(AudioMixerSnapshots.Get().PortalLPDimmedSnapshot, STOP_MODE.ALLOWFADEOUT);
	}

	// Token: 0x04003E92 RID: 16018
	[SerializeField]
	private KButton closeButton;

	// Token: 0x04003E93 RID: 16019
	[SerializeField]
	private KButton rejectButton;

	// Token: 0x04003E94 RID: 16020
	[SerializeField]
	private LocText title;

	// Token: 0x04003E95 RID: 16021
	[SerializeField]
	private GameObject rejectConfirmationScreen;

	// Token: 0x04003E96 RID: 16022
	[SerializeField]
	private KButton confirmRejectionBtn;

	// Token: 0x04003E97 RID: 16023
	[SerializeField]
	private KButton cancelRejectionBtn;

	// Token: 0x04003E98 RID: 16024
	public static ImmigrantScreen instance;

	// Token: 0x04003E99 RID: 16025
	private Telepad telepad;

	// Token: 0x04003E9A RID: 16026
	private bool hasShown;
}
