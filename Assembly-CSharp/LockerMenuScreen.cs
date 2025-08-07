using System;
using FMOD.Studio;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000D21 RID: 3361
public class LockerMenuScreen : KModalScreen
{
	// Token: 0x060067A7 RID: 26535 RVA: 0x0027119B File Offset: 0x0026F39B
	protected override void OnActivate()
	{
		LockerMenuScreen.Instance = this;
		this.Show(false);
	}

	// Token: 0x060067A8 RID: 26536 RVA: 0x002711AA File Offset: 0x0026F3AA
	public override float GetSortKey()
	{
		return 40f;
	}

	// Token: 0x060067A9 RID: 26537 RVA: 0x002711B1 File Offset: 0x0026F3B1
	public void ShowInventoryScreen()
	{
		if (!base.isActiveAndEnabled)
		{
			this.Show(true);
		}
		LockerNavigator.Instance.PushScreen(LockerNavigator.Instance.kleiInventoryScreen, null);
		MusicManager.instance.SetSongParameter("Music_SupplyCloset", "SupplyClosetView", "inventory", true);
	}

	// Token: 0x060067AA RID: 26538 RVA: 0x002711F4 File Offset: 0x0026F3F4
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		MultiToggle multiToggle = this.buttonInventory;
		multiToggle.onClick = (global::System.Action)Delegate.Combine(multiToggle.onClick, new global::System.Action(delegate
		{
			this.ShowInventoryScreen();
		}));
		MultiToggle multiToggle2 = this.buttonDuplicants;
		multiToggle2.onClick = (global::System.Action)Delegate.Combine(multiToggle2.onClick, new global::System.Action(delegate
		{
			MinionBrowserScreenConfig.Personalities(default(Option<Personality>)).ApplyAndOpenScreen(null);
			MusicManager.instance.SetSongParameter("Music_SupplyCloset", "SupplyClosetView", "dupe", true);
		}));
		MultiToggle multiToggle3 = this.buttonOutfitBroswer;
		multiToggle3.onClick = (global::System.Action)Delegate.Combine(multiToggle3.onClick, new global::System.Action(delegate
		{
			OutfitBrowserScreenConfig.Mannequin().ApplyAndOpenScreen();
			MusicManager.instance.SetSongParameter("Music_SupplyCloset", "SupplyClosetView", "wardrobe", true);
		}));
		this.closeButton.onClick += delegate
		{
			this.Show(false);
		};
		this.ConfigureHoverForButton(this.buttonInventory, UI.LOCKER_MENU.BUTTON_INVENTORY_DESCRIPTION, true);
		this.ConfigureHoverForButton(this.buttonDuplicants, UI.LOCKER_MENU.BUTTON_DUPLICANTS_DESCRIPTION, true);
		this.ConfigureHoverForButton(this.buttonOutfitBroswer, UI.LOCKER_MENU.BUTTON_OUTFITS_DESCRIPTION, true);
		this.descriptionArea.text = UI.LOCKER_MENU.DEFAULT_DESCRIPTION;
	}

	// Token: 0x060067AB RID: 26539 RVA: 0x00271314 File Offset: 0x0026F514
	private void ConfigureHoverForButton(MultiToggle toggle, string desc, bool useHoverColor = true)
	{
		LockerMenuScreen.<>c__DisplayClass17_0 CS$<>8__locals1 = new LockerMenuScreen.<>c__DisplayClass17_0();
		CS$<>8__locals1.useHoverColor = useHoverColor;
		CS$<>8__locals1.<>4__this = this;
		CS$<>8__locals1.defaultColor = new Color(0.30980393f, 0.34117648f, 0.38431373f, 1f);
		CS$<>8__locals1.hoverColor = new Color(0.7019608f, 0.3647059f, 0.53333336f, 1f);
		toggle.onEnter = null;
		toggle.onExit = null;
		toggle.onEnter = (global::System.Action)Delegate.Combine(toggle.onEnter, CS$<>8__locals1.<ConfigureHoverForButton>g__OnHoverEnterFn|0(toggle, desc));
		toggle.onExit = (global::System.Action)Delegate.Combine(toggle.onExit, CS$<>8__locals1.<ConfigureHoverForButton>g__OnHoverExitFn|1(toggle));
	}

	// Token: 0x060067AC RID: 26540 RVA: 0x002713BC File Offset: 0x0026F5BC
	public override void Show(bool show = true)
	{
		base.Show(show);
		if (show)
		{
			AudioMixer.instance.Start(AudioMixerSnapshots.Get().FrontEndSupplyClosetSnapshot);
			MusicManager.instance.OnSupplyClosetMenu(true, 0.5f);
			MusicManager.instance.PlaySong("Music_SupplyCloset", false);
			ThreadedHttps<KleiAccount>.Instance.AuthenticateUser(new KleiAccount.GetUserIDdelegate(this.TriggerShouldRefreshClaimItems), false);
		}
		else
		{
			AudioMixer.instance.Stop(AudioMixerSnapshots.Get().FrontEndSupplyClosetSnapshot, STOP_MODE.ALLOWFADEOUT);
			MusicManager.instance.OnSupplyClosetMenu(false, 1f);
			if (MusicManager.instance.SongIsPlaying("Music_SupplyCloset"))
			{
				MusicManager.instance.StopSong("Music_SupplyCloset", true, STOP_MODE.ALLOWFADEOUT);
			}
		}
		this.RefreshClaimItemsButton();
	}

	// Token: 0x060067AD RID: 26541 RVA: 0x0027146F File Offset: 0x0026F66F
	private void TriggerShouldRefreshClaimItems()
	{
		this.refreshRequested = true;
	}

	// Token: 0x060067AE RID: 26542 RVA: 0x00271478 File Offset: 0x0026F678
	protected override void OnSpawn()
	{
		base.OnSpawn();
	}

	// Token: 0x060067AF RID: 26543 RVA: 0x00271480 File Offset: 0x0026F680
	protected override void OnForcedCleanUp()
	{
		base.OnForcedCleanUp();
	}

	// Token: 0x060067B0 RID: 26544 RVA: 0x00271488 File Offset: 0x0026F688
	private void RefreshClaimItemsButton()
	{
		this.noConnectionIcon.SetActive(!ThreadedHttps<KleiAccount>.Instance.HasValidTicket());
		this.refreshRequested = false;
		bool hasClaimable = PermitItems.HasUnopenedItem();
		this.dropsAvailableNotification.SetActive(hasClaimable);
		this.buttonClaimItems.ChangeState(hasClaimable ? 0 : 1);
		this.buttonClaimItems.GetComponent<HierarchyReferences>().GetReference<Image>("FGIcon").material = (hasClaimable ? null : this.desatUIMaterial);
		this.buttonClaimItems.onClick = null;
		MultiToggle multiToggle = this.buttonClaimItems;
		multiToggle.onClick = (global::System.Action)Delegate.Combine(multiToggle.onClick, new global::System.Action(delegate
		{
			if (!hasClaimable)
			{
				return;
			}
			global::UnityEngine.Object.FindObjectOfType<KleiItemDropScreen>(true).Show(true);
			this.Show(false);
		}));
		this.ConfigureHoverForButton(this.buttonClaimItems, hasClaimable ? UI.LOCKER_MENU.BUTTON_CLAIM_DESCRIPTION : UI.LOCKER_MENU.BUTTON_CLAIM_NONE_DESCRIPTION, hasClaimable);
	}

	// Token: 0x060067B1 RID: 26545 RVA: 0x00271580 File Offset: 0x0026F780
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.Escape) || e.TryConsume(global::Action.MouseRight))
		{
			this.Show(false);
			AudioMixer.instance.Stop(AudioMixerSnapshots.Get().FrontEndSupplyClosetSnapshot, STOP_MODE.ALLOWFADEOUT);
			MusicManager.instance.OnSupplyClosetMenu(false, 1f);
			if (MusicManager.instance.SongIsPlaying("Music_SupplyCloset"))
			{
				MusicManager.instance.StopSong("Music_SupplyCloset", true, STOP_MODE.ALLOWFADEOUT);
			}
		}
		base.OnKeyDown(e);
	}

	// Token: 0x060067B2 RID: 26546 RVA: 0x002715F5 File Offset: 0x0026F7F5
	private void Update()
	{
		if (this.refreshRequested)
		{
			this.RefreshClaimItemsButton();
		}
	}

	// Token: 0x04004705 RID: 18181
	public static LockerMenuScreen Instance;

	// Token: 0x04004706 RID: 18182
	[SerializeField]
	private MultiToggle buttonInventory;

	// Token: 0x04004707 RID: 18183
	[SerializeField]
	private MultiToggle buttonDuplicants;

	// Token: 0x04004708 RID: 18184
	[SerializeField]
	private MultiToggle buttonOutfitBroswer;

	// Token: 0x04004709 RID: 18185
	[SerializeField]
	private MultiToggle buttonClaimItems;

	// Token: 0x0400470A RID: 18186
	[SerializeField]
	private LocText descriptionArea;

	// Token: 0x0400470B RID: 18187
	[SerializeField]
	private KButton closeButton;

	// Token: 0x0400470C RID: 18188
	[SerializeField]
	private GameObject dropsAvailableNotification;

	// Token: 0x0400470D RID: 18189
	[SerializeField]
	private GameObject noConnectionIcon;

	// Token: 0x0400470E RID: 18190
	private const string LOCKER_MENU_MUSIC = "Music_SupplyCloset";

	// Token: 0x0400470F RID: 18191
	private const string MUSIC_PARAMETER = "SupplyClosetView";

	// Token: 0x04004710 RID: 18192
	[SerializeField]
	private Material desatUIMaterial;

	// Token: 0x04004711 RID: 18193
	private bool refreshRequested;
}
