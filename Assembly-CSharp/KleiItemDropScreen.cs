using System;
using System.Collections;
using System.Collections.Generic;
using Database;
using FMOD.Studio;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000D00 RID: 3328
public class KleiItemDropScreen : KModalScreen
{
	// Token: 0x060066B5 RID: 26293 RVA: 0x0026D339 File Offset: 0x0026B539
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		KleiItemDropScreen.Instance = this;
		this.closeButton.onClick += delegate
		{
			this.Show(false);
		};
		if (string.IsNullOrEmpty(KleiAccount.KleiToken))
		{
			base.Show(false);
		}
	}

	// Token: 0x060066B6 RID: 26294 RVA: 0x0026D371 File Offset: 0x0026B571
	protected override void OnActivate()
	{
		KleiItemDropScreen.Instance = this;
		this.Show(false);
	}

	// Token: 0x060066B7 RID: 26295 RVA: 0x0026D380 File Offset: 0x0026B580
	public override void Show(bool show = true)
	{
		this.serverRequestState.Reset();
		if (!show)
		{
			this.animatedLoadingIcon.gameObject.SetActive(false);
			if (this.activePresentationRoutine != null)
			{
				base.StopCoroutine(this.activePresentationRoutine);
			}
			if (this.shouldDoCloseRoutine)
			{
				this.closeButton.gameObject.SetActive(false);
				Updater.RunRoutine(this, this.AnimateScreenOutRoutine()).Then(delegate
				{
					base.Show(false);
				});
				this.shouldDoCloseRoutine = false;
			}
			else
			{
				base.Show(false);
			}
			AudioMixer.instance.Stop(AudioMixerSnapshots.Get().FrontEndItemDropScreenSnapshot, STOP_MODE.ALLOWFADEOUT);
			return;
		}
		AudioMixer.instance.Start(AudioMixerSnapshots.Get().FrontEndItemDropScreenSnapshot);
		base.Show(true);
	}

	// Token: 0x060066B8 RID: 26296 RVA: 0x0026D43D File Offset: 0x0026B63D
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.Escape) || e.TryConsume(global::Action.MouseRight))
		{
			this.Show(false);
		}
		base.OnKeyDown(e);
	}

	// Token: 0x060066B9 RID: 26297 RVA: 0x0026D460 File Offset: 0x0026B660
	protected override void OnShow(bool show)
	{
		base.OnShow(show);
		if (!show)
		{
			return;
		}
		if (PermitItems.HasUnopenedItem())
		{
			this.PresentNextUnopenedItem(true);
			this.shouldDoCloseRoutine = true;
			return;
		}
		this.userMessageLabel.SetText(UI.ITEM_DROP_SCREEN.NOTHING_AVAILABLE);
		this.PresentNoItemAvailablePrompt(true);
		this.shouldDoCloseRoutine = true;
	}

	// Token: 0x060066BA RID: 26298 RVA: 0x0026D4B4 File Offset: 0x0026B6B4
	public void PresentNextUnopenedItem(bool firstItemPresentation = true)
	{
		int num = 0;
		using (IEnumerator<KleiItems.ItemData> enumerator = PermitItems.IterateInventory().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.IsOpened)
				{
					num++;
				}
			}
		}
		this.RefreshUnopenedItemsLabel();
		foreach (KleiItems.ItemData itemData in PermitItems.IterateInventory())
		{
			if (!itemData.IsOpened)
			{
				this.PresentItem(itemData, firstItemPresentation, num == 1);
				return;
			}
		}
		this.PresentNoItemAvailablePrompt(false);
	}

	// Token: 0x060066BB RID: 26299 RVA: 0x0026D560 File Offset: 0x0026B760
	private void RefreshUnopenedItemsLabel()
	{
		int num = 0;
		using (IEnumerator<KleiItems.ItemData> enumerator = PermitItems.IterateInventory().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.IsOpened)
				{
					num++;
				}
			}
		}
		if (num > 1)
		{
			this.unopenedItemCountLabel.gameObject.SetActive(true);
			this.unopenedItemCountLabel.SetText(UI.ITEM_DROP_SCREEN.UNOPENED_ITEM_COUNT, (float)num);
			return;
		}
		if (num == 1)
		{
			this.unopenedItemCountLabel.gameObject.SetActive(true);
			this.unopenedItemCountLabel.SetText(UI.ITEM_DROP_SCREEN.UNOPENED_ITEM, (float)num);
			return;
		}
		this.unopenedItemCountLabel.gameObject.SetActive(false);
	}

	// Token: 0x060066BC RID: 26300 RVA: 0x0026D61C File Offset: 0x0026B81C
	public void PresentItem(KleiItems.ItemData item, bool firstItemPresentation, bool lastItemPresentation)
	{
		this.userMessageLabel.SetText(UI.ITEM_DROP_SCREEN.THANKS_FOR_PLAYING);
		this.giftAcknowledged = false;
		this.serverRequestState.revealConfirmedByServer = false;
		this.serverRequestState.revealRejectedByServer = false;
		if (this.activePresentationRoutine != null)
		{
			base.StopCoroutine(this.activePresentationRoutine);
		}
		this.activePresentationRoutine = base.StartCoroutine(this.PresentItemRoutine(item, firstItemPresentation, lastItemPresentation));
		this.acceptButton.ClearOnClick();
		this.acknowledgeButton.ClearOnClick();
		this.acceptButton.GetComponentInChildren<LocText>().SetText(UI.ITEM_DROP_SCREEN.PRINT_ITEM_BUTTON);
		this.acceptButton.onClick += delegate
		{
			this.RequestReveal(item);
		};
		this.acknowledgeButton.onClick += delegate
		{
			if (this.serverRequestState.revealConfirmedByServer)
			{
				this.giftAcknowledged = true;
			}
		};
	}

	// Token: 0x060066BD RID: 26301 RVA: 0x0026D6FD File Offset: 0x0026B8FD
	private void RequestReveal(KleiItems.ItemData item)
	{
		this.serverRequestState.revealRequested = true;
		PermitItems.QueueRequestOpenOrUnboxItem(item, new KleiItems.ResponseCallback(this.OnOpenItemRequestResponse));
	}

	// Token: 0x060066BE RID: 26302 RVA: 0x0026D720 File Offset: 0x0026B920
	public void OnOpenItemRequestResponse(KleiItems.Result result)
	{
		if (!this.serverRequestState.revealRequested)
		{
			return;
		}
		this.serverRequestState.revealRequested = false;
		if (result.Success)
		{
			this.serverRequestState.revealRejectedByServer = false;
			this.serverRequestState.revealConfirmedByServer = true;
			return;
		}
		this.serverRequestState.revealRejectedByServer = true;
		this.serverRequestState.revealConfirmedByServer = false;
	}

	// Token: 0x060066BF RID: 26303 RVA: 0x0026D780 File Offset: 0x0026B980
	public void PresentNoItemAvailablePrompt(bool firstItemPresentation)
	{
		this.userMessageLabel.SetText(UI.ITEM_DROP_SCREEN.NOTHING_AVAILABLE);
		this.noItemAvailableAcknowledged = false;
		this.acknowledgeButton.ClearOnClick();
		this.acceptButton.ClearOnClick();
		this.acceptButton.GetComponentInChildren<LocText>().SetText(UI.ITEM_DROP_SCREEN.DISMISS_BUTTON);
		this.acceptButton.onClick += delegate
		{
			this.noItemAvailableAcknowledged = true;
		};
		if (this.activePresentationRoutine != null)
		{
			base.StopCoroutine(this.activePresentationRoutine);
		}
		this.activePresentationRoutine = base.StartCoroutine(this.PresentNoItemAvailableRoutine(firstItemPresentation));
	}

	// Token: 0x060066C0 RID: 26304 RVA: 0x0026D817 File Offset: 0x0026BA17
	private IEnumerator AnimateScreenInRoutine()
	{
		float scaleFactor = base.transform.parent.GetComponent<CanvasScaler>().scaleFactor;
		float OPEN_WIDTH = (float)Screen.width / scaleFactor;
		float num = Mathf.Clamp((float)Screen.height / scaleFactor, 720f, 900f);
		KFMOD.PlayUISound(GlobalAssets.GetSound("GiftItemDrop_Screen_Open", false));
		this.userMessageLabel.gameObject.SetActive(false);
		yield return Updater.Ease(delegate(Vector2 v2)
		{
			this.shieldMaskRect.sizeDelta = v2;
		}, this.shieldMaskRect.sizeDelta, new Vector2(this.shieldMaskRect.sizeDelta.x, num), 0.5f, Easing.CircInOut, -1f);
		yield return Updater.Ease(delegate(Vector2 v2)
		{
			this.shieldMaskRect.sizeDelta = v2;
		}, this.shieldMaskRect.sizeDelta, new Vector2(OPEN_WIDTH, this.shieldMaskRect.sizeDelta.y), 0.25f, Easing.CircInOut, -1f);
		this.userMessageLabel.gameObject.SetActive(true);
		yield break;
	}

	// Token: 0x060066C1 RID: 26305 RVA: 0x0026D826 File Offset: 0x0026BA26
	private IEnumerator AnimateScreenOutRoutine()
	{
		KFMOD.PlayUISound(GlobalAssets.GetSound("GiftItemDrop_Screen_Close", false));
		this.userMessageLabel.gameObject.SetActive(false);
		yield return Updater.Ease(delegate(Vector2 v2)
		{
			this.shieldMaskRect.sizeDelta = v2;
		}, this.shieldMaskRect.sizeDelta, new Vector2(8f, this.shieldMaskRect.sizeDelta.y), 0.25f, Easing.CircInOut, -1f);
		yield return Updater.Ease(delegate(Vector2 v2)
		{
			this.shieldMaskRect.sizeDelta = v2;
		}, this.shieldMaskRect.sizeDelta, new Vector2(this.shieldMaskRect.sizeDelta.x, 0f), 0.25f, Easing.CircInOut, -1f);
		yield break;
	}

	// Token: 0x060066C2 RID: 26306 RVA: 0x0026D835 File Offset: 0x0026BA35
	private IEnumerator PresentNoItemAvailableRoutine(bool firstItem)
	{
		yield return null;
		this.itemNameLabel.SetText("");
		this.itemDescriptionLabel.SetText("");
		this.itemRarityLabel.SetText("");
		this.itemCategoryLabel.SetText("");
		if (firstItem)
		{
			this.animatedPod.Play("idle", KAnim.PlayMode.Loop, 1f, 0f);
			this.acceptButtonRect.gameObject.SetActive(false);
			this.shieldMaskRect.sizeDelta = new Vector2(8f, 0f);
			this.shieldMaskRect.gameObject.SetActive(true);
		}
		if (firstItem)
		{
			this.closeButton.gameObject.SetActive(false);
			yield return Updater.WaitForSeconds(0.5f);
			yield return this.AnimateScreenInRoutine();
			yield return Updater.WaitForSeconds(0.125f);
			this.closeButton.gameObject.SetActive(true);
		}
		else
		{
			yield return Updater.WaitForSeconds(0.25f);
		}
		Vector2 animate_offset = new Vector2(0f, -30f);
		this.acceptButtonRect.FindOrAddComponent<CanvasGroup>().alpha = 0f;
		this.acceptButtonRect.gameObject.SetActive(true);
		this.acceptButtonPosition.SetOn(this.acceptButtonRect);
		yield return Updater.WaitForSeconds(0.75f);
		yield return PresUtil.OffsetToAndFade(this.acceptButton.rectTransform(), animate_offset, 1f, 0.125f, Easing.ExpoOut);
		yield return Updater.Until(() => this.noItemAvailableAcknowledged);
		yield return PresUtil.OffsetFromAndFade(this.acceptButton.rectTransform(), animate_offset, 0f, 0.125f, Easing.SmoothStep);
		this.Show(false);
		yield break;
	}

	// Token: 0x060066C3 RID: 26307 RVA: 0x0026D84B File Offset: 0x0026BA4B
	private IEnumerator PresentItemRoutine(KleiItems.ItemData item, bool firstItem, bool lastItem)
	{
		yield return null;
		if (item.ItemId == 0UL)
		{
			global::Debug.LogError("Could not find dropped item inventory.");
			yield break;
		}
		this.itemNameLabel.SetText("");
		this.itemDescriptionLabel.SetText("");
		this.itemRarityLabel.SetText("");
		this.itemCategoryLabel.SetText("");
		this.permitVisualizer.ResetState();
		if (firstItem)
		{
			this.animatedPod.Play("idle", KAnim.PlayMode.Loop, 1f, 0f);
			this.acceptButtonRect.gameObject.SetActive(false);
			this.shieldMaskRect.sizeDelta = new Vector2(8f, 0f);
			this.shieldMaskRect.gameObject.SetActive(true);
		}
		if (firstItem)
		{
			this.closeButton.gameObject.SetActive(false);
			yield return Updater.WaitForSeconds(0.5f);
			yield return this.AnimateScreenInRoutine();
			yield return Updater.WaitForSeconds(0.125f);
			this.closeButton.gameObject.SetActive(true);
		}
		Vector2 animate_offset = new Vector2(0f, -30f);
		if (firstItem)
		{
			this.acceptButtonRect.FindOrAddComponent<CanvasGroup>().alpha = 0f;
			this.acceptButtonRect.gameObject.SetActive(true);
			this.acceptButtonPosition.SetOn(this.acceptButtonRect);
			this.animatedPod.Play("powerup", KAnim.PlayMode.Once, 1f, 0f);
			this.animatedPod.Queue("working_loop", KAnim.PlayMode.Loop, 1f, 0f);
			yield return Updater.WaitForSeconds(1.25f);
			yield return PresUtil.OffsetToAndFade(this.acceptButton.rectTransform(), animate_offset, 1f, 0.125f, Easing.ExpoOut);
			yield return Updater.Until(() => this.serverRequestState.revealRequested);
			yield return PresUtil.OffsetFromAndFade(this.acceptButton.rectTransform(), animate_offset, 0f, 0.125f, Easing.SmoothStep);
		}
		else
		{
			this.RequestReveal(item);
		}
		this.animatedLoadingIcon.gameObject.rectTransform().anchoredPosition = new Vector2(0f, -352f);
		if (this.animatedLoadingIcon.GetComponent<CanvasGroup>() != null)
		{
			this.animatedLoadingIcon.GetComponent<CanvasGroup>().alpha = 1f;
		}
		yield return new WaitForSecondsRealtime(0.3f);
		if (!this.serverRequestState.revealConfirmedByServer && !this.serverRequestState.revealRejectedByServer)
		{
			this.animatedLoadingIcon.gameObject.SetActive(true);
			this.animatedLoadingIcon.Play("loading_rocket", KAnim.PlayMode.Loop, 1f, 0f);
			yield return Updater.Until(() => this.serverRequestState.revealConfirmedByServer || this.serverRequestState.revealRejectedByServer);
			yield return new WaitForSecondsRealtime(2f);
			yield return PresUtil.OffsetFromAndFade(this.animatedLoadingIcon.gameObject.rectTransform(), new Vector2(0f, -512f), 0f, 0.25f, Easing.SmoothStep);
			this.animatedLoadingIcon.gameObject.SetActive(false);
		}
		if (this.serverRequestState.revealRejectedByServer)
		{
			this.animatedPod.Play("idle", KAnim.PlayMode.Loop, 1f, 0f);
			this.errorMessage.gameObject.SetActive(true);
			yield return Updater.WaitForSeconds(3f);
			this.errorMessage.gameObject.SetActive(false);
		}
		else if (this.serverRequestState.revealConfirmedByServer)
		{
			float num = 1f;
			this.animatedPod.PlaySpeedMultiplier = (firstItem ? 1f : (1f * num));
			this.animatedPod.Play("additional_pre", KAnim.PlayMode.Once, 1f, 0f);
			this.animatedPod.Queue("working_loop", KAnim.PlayMode.Loop, 1f, 0f);
			yield return Updater.WaitForSeconds(firstItem ? 1f : (1f / num));
			this.animatedPod.PlaySpeedMultiplier = 1f;
			this.RefreshUnopenedItemsLabel();
			DropScreenPresentationInfo dropScreenPresentationInfo;
			dropScreenPresentationInfo.UseEquipmentVis = false;
			dropScreenPresentationInfo.BuildOverride = null;
			dropScreenPresentationInfo.Sprite = null;
			string name = "";
			string desc = "";
			PermitRarity rarity = PermitRarity.Unknown;
			string categoryString = "";
			string text;
			if (PermitItems.TryGetBoxInfo(item, out name, out desc, out text))
			{
				dropScreenPresentationInfo.UseEquipmentVis = false;
				dropScreenPresentationInfo.BuildOverride = null;
				dropScreenPresentationInfo.Sprite = Assets.GetSprite(text);
				rarity = PermitRarity.Loyalty;
			}
			else
			{
				PermitResource permitResource = Db.Get().Permits.Get(item.Id);
				dropScreenPresentationInfo.Sprite = permitResource.GetPermitPresentationInfo().sprite;
				dropScreenPresentationInfo.UseEquipmentVis = permitResource.Category == PermitCategory.Equipment;
				if (permitResource is EquippableFacadeResource)
				{
					dropScreenPresentationInfo.BuildOverride = (permitResource as EquippableFacadeResource).BuildOverride;
				}
				name = permitResource.Name;
				desc = permitResource.Description;
				rarity = permitResource.Rarity;
				PermitCategory category = permitResource.Category;
				if (category != PermitCategory.Building)
				{
					if (category != PermitCategory.Artwork)
					{
						if (category != PermitCategory.JoyResponse)
						{
							categoryString = PermitCategories.GetDisplayName(permitResource.Category);
						}
						else
						{
							categoryString = PermitCategories.GetDisplayName(permitResource.Category);
							if (permitResource is BalloonArtistFacadeResource)
							{
								categoryString = PermitCategories.GetDisplayName(permitResource.Category) + ": " + UI.KLEI_INVENTORY_SCREEN.CATEGORIES.JOY_RESPONSES.BALLOON_ARTIST;
							}
						}
					}
					else
					{
						categoryString = PermitCategories.GetDisplayName(permitResource.Category);
						if (permitResource is ArtableStage)
						{
							categoryString = Assets.GetPrefab((permitResource as ArtableStage).prefabId).GetProperName();
						}
					}
				}
				else
				{
					categoryString = Assets.GetPrefab((permitResource as BuildingFacadeResource).PrefabID).GetProperName();
				}
			}
			this.permitVisualizer.ConfigureWith(dropScreenPresentationInfo);
			yield return this.permitVisualizer.AnimateIn();
			KFMOD.PlayUISoundWithLabeledParameter(GlobalAssets.GetSound("GiftItemDrop_Rarity", false), "GiftItemRarity", string.Format("{0}", rarity));
			this.itemNameLabel.SetText(name);
			this.itemDescriptionLabel.SetText(desc);
			this.itemRarityLabel.SetText(rarity.GetLocStringName());
			this.itemCategoryLabel.SetText(categoryString);
			this.itemTextContainerPosition.SetOn(this.itemTextContainer);
			yield return Updater.Parallel(new Updater[] { PresUtil.OffsetToAndFade(this.itemTextContainer.rectTransform(), animate_offset, 1f, 0.125f, Easing.CircInOut) });
			yield return Updater.Until(() => this.giftAcknowledged);
			if (lastItem)
			{
				this.animatedPod.Play("working_pst", KAnim.PlayMode.Once, 1f, 0f);
				this.animatedPod.Queue("idle", KAnim.PlayMode.Loop, 1f, 0f);
				yield return Updater.Parallel(new Updater[] { PresUtil.OffsetFromAndFade(this.itemTextContainer.rectTransform(), animate_offset, 0f, 0.125f, Easing.CircInOut) });
				this.itemNameLabel.SetText("");
				this.itemDescriptionLabel.SetText("");
				this.itemRarityLabel.SetText("");
				this.itemCategoryLabel.SetText("");
				yield return this.permitVisualizer.AnimateOut();
			}
			else
			{
				this.itemNameLabel.SetText("");
				this.itemDescriptionLabel.SetText("");
				this.itemRarityLabel.SetText("");
				this.itemCategoryLabel.SetText("");
			}
			name = null;
			desc = null;
			categoryString = null;
		}
		this.PresentNextUnopenedItem(false);
		yield break;
	}

	// Token: 0x060066C4 RID: 26308 RVA: 0x0026D86F File Offset: 0x0026BA6F
	public static bool HasItemsToShow()
	{
		return PermitItems.HasUnopenedItem();
	}

	// Token: 0x04004668 RID: 18024
	[SerializeField]
	private RectTransform shieldMaskRect;

	// Token: 0x04004669 RID: 18025
	[SerializeField]
	private KButton closeButton;

	// Token: 0x0400466A RID: 18026
	[Header("Animated Item")]
	[SerializeField]
	private KleiItemDropScreen_PermitVis permitVisualizer;

	// Token: 0x0400466B RID: 18027
	[SerializeField]
	private KBatchedAnimController animatedPod;

	// Token: 0x0400466C RID: 18028
	[SerializeField]
	private LocText userMessageLabel;

	// Token: 0x0400466D RID: 18029
	[SerializeField]
	private LocText unopenedItemCountLabel;

	// Token: 0x0400466E RID: 18030
	[Header("Item Info")]
	[SerializeField]
	private RectTransform itemTextContainer;

	// Token: 0x0400466F RID: 18031
	[SerializeField]
	private LocText itemNameLabel;

	// Token: 0x04004670 RID: 18032
	[SerializeField]
	private LocText itemDescriptionLabel;

	// Token: 0x04004671 RID: 18033
	[SerializeField]
	private LocText itemRarityLabel;

	// Token: 0x04004672 RID: 18034
	[SerializeField]
	private LocText itemCategoryLabel;

	// Token: 0x04004673 RID: 18035
	[Header("Accept Button")]
	[SerializeField]
	private RectTransform acceptButtonRect;

	// Token: 0x04004674 RID: 18036
	[SerializeField]
	private KButton acceptButton;

	// Token: 0x04004675 RID: 18037
	[SerializeField]
	private KBatchedAnimController animatedLoadingIcon;

	// Token: 0x04004676 RID: 18038
	[SerializeField]
	private KButton acknowledgeButton;

	// Token: 0x04004677 RID: 18039
	[SerializeField]
	private LocText errorMessage;

	// Token: 0x04004678 RID: 18040
	private Coroutine activePresentationRoutine;

	// Token: 0x04004679 RID: 18041
	private KleiItemDropScreen.ServerRequestState serverRequestState;

	// Token: 0x0400467A RID: 18042
	private bool giftAcknowledged;

	// Token: 0x0400467B RID: 18043
	private bool noItemAvailableAcknowledged;

	// Token: 0x0400467C RID: 18044
	public static KleiItemDropScreen Instance;

	// Token: 0x0400467D RID: 18045
	private bool shouldDoCloseRoutine;

	// Token: 0x0400467E RID: 18046
	private const float TEXT_AND_BUTTON_ANIMATE_OFFSET_Y = -30f;

	// Token: 0x0400467F RID: 18047
	private PrefabDefinedUIPosition acceptButtonPosition = new PrefabDefinedUIPosition();

	// Token: 0x04004680 RID: 18048
	private PrefabDefinedUIPosition itemTextContainerPosition = new PrefabDefinedUIPosition();

	// Token: 0x02001ED0 RID: 7888
	private struct ServerRequestState
	{
		// Token: 0x0600B166 RID: 45414 RVA: 0x003D48EF File Offset: 0x003D2AEF
		public void Reset()
		{
			this.revealRequested = false;
			this.revealConfirmedByServer = false;
			this.revealRejectedByServer = false;
		}

		// Token: 0x04008EF4 RID: 36596
		public bool revealRequested;

		// Token: 0x04008EF5 RID: 36597
		public bool revealConfirmedByServer;

		// Token: 0x04008EF6 RID: 36598
		public bool revealRejectedByServer;
	}
}
