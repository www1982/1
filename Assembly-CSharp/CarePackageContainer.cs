using System;
using System.Collections;
using System.Collections.Generic;
using Database;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000C0E RID: 3086
public class CarePackageContainer : KScreen, ITelepadDeliverableContainer
{
	// Token: 0x06005D22 RID: 23842 RVA: 0x002201A8 File Offset: 0x0021E3A8
	public GameObject GetGameObject()
	{
		return base.gameObject;
	}

	// Token: 0x170006DE RID: 1758
	// (get) Token: 0x06005D23 RID: 23843 RVA: 0x002201B0 File Offset: 0x0021E3B0
	public CarePackageInfo Info
	{
		get
		{
			return this.info;
		}
	}

	// Token: 0x06005D24 RID: 23844 RVA: 0x002201B8 File Offset: 0x0021E3B8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.Initialize();
		base.StartCoroutine(this.DelayedGeneration());
	}

	// Token: 0x06005D25 RID: 23845 RVA: 0x002201D3 File Offset: 0x0021E3D3
	public override float GetSortKey()
	{
		return 50f;
	}

	// Token: 0x06005D26 RID: 23846 RVA: 0x002201DA File Offset: 0x0021E3DA
	private IEnumerator DelayedGeneration()
	{
		yield return SequenceUtil.WaitForEndOfFrame;
		if (this.controller != null)
		{
			this.GenerateCharacter(this.controller.IsStarterMinion);
		}
		yield break;
	}

	// Token: 0x06005D27 RID: 23847 RVA: 0x002201E9 File Offset: 0x0021E3E9
	protected override void OnCmpDisable()
	{
		base.OnCmpDisable();
		if (this.animController != null)
		{
			this.animController.gameObject.DeleteObject();
			this.animController = null;
		}
	}

	// Token: 0x06005D28 RID: 23848 RVA: 0x00220218 File Offset: 0x0021E418
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		if (this.controller != null)
		{
			CharacterSelectionController characterSelectionController = this.controller;
			characterSelectionController.OnLimitReachedEvent = (global::System.Action)Delegate.Remove(characterSelectionController.OnLimitReachedEvent, new global::System.Action(this.OnCharacterSelectionLimitReached));
			CharacterSelectionController characterSelectionController2 = this.controller;
			characterSelectionController2.OnLimitUnreachedEvent = (global::System.Action)Delegate.Remove(characterSelectionController2.OnLimitUnreachedEvent, new global::System.Action(this.OnCharacterSelectionLimitUnReached));
			CharacterSelectionController characterSelectionController3 = this.controller;
			characterSelectionController3.OnReshuffleEvent = (Action<bool>)Delegate.Remove(characterSelectionController3.OnReshuffleEvent, new Action<bool>(this.Reshuffle));
		}
	}

	// Token: 0x06005D29 RID: 23849 RVA: 0x002202AE File Offset: 0x0021E4AE
	private void Initialize()
	{
		this.professionIconMap = new Dictionary<string, Sprite>();
		this.professionIcons.ForEach(delegate(CarePackageContainer.ProfessionIcon ic)
		{
			this.professionIconMap.Add(ic.professionName, ic.iconImg);
		});
		if (CarePackageContainer.containers == null)
		{
			CarePackageContainer.containers = new List<ITelepadDeliverableContainer>();
		}
		CarePackageContainer.containers.Add(this);
	}

	// Token: 0x06005D2A RID: 23850 RVA: 0x002202F0 File Offset: 0x0021E4F0
	private void GenerateCharacter(bool is_starter)
	{
		int num = 0;
		do
		{
			this.info = Immigration.Instance.RandomCarePackage();
			num++;
		}
		while (this.IsCharacterRedundant() && num < 20);
		if (this.animController != null)
		{
			global::UnityEngine.Object.Destroy(this.animController.gameObject);
			this.animController = null;
		}
		this.carePackageInstanceData = new CarePackageContainer.CarePackageInstanceData();
		this.carePackageInstanceData.info = this.info;
		if (this.info.facadeID == "SELECTRANDOM")
		{
			this.carePackageInstanceData.facadeID = Db.GetEquippableFacades().resources.FindAll((EquippableFacadeResource match) => match.DefID == this.info.id).GetRandom<EquippableFacadeResource>().Id;
		}
		else
		{
			this.carePackageInstanceData.facadeID = this.info.facadeID;
		}
		this.SetAnimator();
		this.SetInfoText();
		this.selectButton.ClearOnClick();
		if (!this.controller.IsStarterMinion)
		{
			this.selectButton.onClick += delegate
			{
				this.SelectDeliverable();
			};
		}
	}

	// Token: 0x06005D2B RID: 23851 RVA: 0x002203FC File Offset: 0x0021E5FC
	private void SetAnimator()
	{
		GameObject prefab = Assets.GetPrefab(this.info.id.ToTag());
		EdiblesManager.FoodInfo foodInfo = EdiblesManager.GetFoodInfo(this.info.id);
		int num;
		if (ElementLoader.FindElementByName(this.info.id) != null)
		{
			num = 1;
		}
		else if (foodInfo != null && foodInfo.CaloriesPerUnit > 0f)
		{
			num = (int)Mathf.Max(1f, this.info.quantity % foodInfo.CaloriesPerUnit);
		}
		else
		{
			num = (int)this.info.quantity;
		}
		if (prefab != null)
		{
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Util.KInstantiateUI(this.contentBody, this.contentBody.transform.parent.gameObject, false);
				gameObject.SetActive(true);
				Image component = gameObject.GetComponent<Image>();
				global::Tuple<Sprite, Color> tuple;
				if (!this.carePackageInstanceData.facadeID.IsNullOrWhiteSpace())
				{
					tuple = Def.GetUISprite(prefab.PrefabID(), this.carePackageInstanceData.facadeID);
				}
				else
				{
					tuple = Def.GetUISprite(prefab, "ui", false);
				}
				component.sprite = tuple.first;
				component.color = tuple.second;
				this.entryIcons.Add(gameObject);
				if (num > 1)
				{
					int num2;
					int num3;
					int num4;
					if (num % 2 == 1)
					{
						num2 = Mathf.CeilToInt((float)(num / 2));
						num3 = num2 - i;
						num4 = ((num3 > 0) ? 1 : (-1));
						num3 = Mathf.Abs(num3);
					}
					else
					{
						num2 = num / 2 - 1;
						if (i <= num2)
						{
							num3 = Mathf.Abs(num2 - i);
							num4 = -1;
						}
						else
						{
							num3 = Mathf.Abs(num2 + 1 - i);
							num4 = 1;
						}
					}
					int num5 = 0;
					if (num % 2 == 0)
					{
						num5 = ((i <= num2) ? (-6) : 6);
						gameObject.transform.SetPosition(gameObject.transform.position += new Vector3((float)num5, 0f, 0f));
					}
					gameObject.transform.localScale = new Vector3(1f - (float)num3 * 0.1f, 1f - (float)num3 * 0.1f, 1f);
					gameObject.transform.Rotate(0f, 0f, 3f * (float)num3 * (float)num4);
					gameObject.transform.SetPosition(gameObject.transform.position + new Vector3(25f * (float)num3 * (float)num4, 5f * (float)num3) + new Vector3((float)num5, 0f, 0f));
					gameObject.GetComponent<Canvas>().sortingOrder = num - num3;
				}
			}
			return;
		}
		GameObject gameObject2 = Util.KInstantiateUI(this.contentBody, this.contentBody.transform.parent.gameObject, false);
		gameObject2.SetActive(true);
		Image component2 = gameObject2.GetComponent<Image>();
		component2.sprite = Def.GetUISpriteFromMultiObjectAnim(ElementLoader.GetElement(this.info.id.ToTag()).substance.anim, "ui", false, "");
		component2.color = ElementLoader.GetElement(this.info.id.ToTag()).substance.uiColour;
		this.entryIcons.Add(gameObject2);
	}

	// Token: 0x06005D2C RID: 23852 RVA: 0x00220740 File Offset: 0x0021E940
	private string GetSpawnableName()
	{
		GameObject prefab = Assets.GetPrefab(this.info.id);
		if (prefab == null)
		{
			Element element = ElementLoader.FindElementByName(this.info.id);
			if (element != null)
			{
				return element.substance.name;
			}
			return "";
		}
		else
		{
			if (string.IsNullOrEmpty(this.carePackageInstanceData.facadeID))
			{
				return prefab.GetProperName();
			}
			return EquippableFacade.GetNameOverride(this.carePackageInstanceData.info.id, this.carePackageInstanceData.facadeID);
		}
	}

	// Token: 0x06005D2D RID: 23853 RVA: 0x002207CC File Offset: 0x0021E9CC
	private string GetSpawnableQuantityOnly()
	{
		if (ElementLoader.GetElement(this.info.id.ToTag()) != null)
		{
			return string.Format(UI.IMMIGRANTSCREEN.CARE_PACKAGE_ELEMENT_COUNT_ONLY, GameUtil.GetFormattedMass(this.info.quantity, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
		}
		if (EdiblesManager.GetFoodInfo(this.info.id) != null)
		{
			return string.Format(UI.IMMIGRANTSCREEN.CARE_PACKAGE_ELEMENT_COUNT_ONLY, GameUtil.GetFormattedCaloriesForItem(this.info.id, this.info.quantity, GameUtil.TimeSlice.None, true));
		}
		return string.Format(UI.IMMIGRANTSCREEN.CARE_PACKAGE_ELEMENT_COUNT_ONLY, this.info.quantity.ToString());
	}

	// Token: 0x06005D2E RID: 23854 RVA: 0x00220880 File Offset: 0x0021EA80
	private string GetCurrentQuantity(WorldInventory inventory)
	{
		if (ElementLoader.GetElement(this.info.id.ToTag()) != null)
		{
			float amount = inventory.GetAmount(this.info.id.ToTag(), false);
			return string.Format(UI.IMMIGRANTSCREEN.CARE_PACKAGE_CURRENT_AMOUNT, GameUtil.GetFormattedMass(amount, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
		}
		if (EdiblesManager.GetFoodInfo(this.info.id) != null)
		{
			float num = WorldResourceAmountTracker<RationTracker>.Get().CountAmountForItemWithID(this.info.id, inventory, true);
			return string.Format(UI.IMMIGRANTSCREEN.CARE_PACKAGE_CURRENT_AMOUNT, GameUtil.GetFormattedCalories(num, GameUtil.TimeSlice.None, true));
		}
		float amount2 = inventory.GetAmount(this.info.id.ToTag(), false);
		return string.Format(UI.IMMIGRANTSCREEN.CARE_PACKAGE_CURRENT_AMOUNT, amount2.ToString());
	}

	// Token: 0x06005D2F RID: 23855 RVA: 0x0022094C File Offset: 0x0021EB4C
	private string GetSpawnableQuantity()
	{
		if (ElementLoader.GetElement(this.info.id.ToTag()) != null)
		{
			return string.Format(UI.IMMIGRANTSCREEN.CARE_PACKAGE_ELEMENT_QUANTITY, GameUtil.GetFormattedMass(this.info.quantity, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"), Assets.GetPrefab(this.info.id).GetProperName());
		}
		if (EdiblesManager.GetFoodInfo(this.info.id) != null)
		{
			return string.Format(UI.IMMIGRANTSCREEN.CARE_PACKAGE_ELEMENT_QUANTITY, GameUtil.GetFormattedCaloriesForItem(this.info.id, this.info.quantity, GameUtil.TimeSlice.None, true), Assets.GetPrefab(this.info.id).GetProperName());
		}
		return string.Format(UI.IMMIGRANTSCREEN.CARE_PACKAGE_ELEMENT_COUNT, Assets.GetPrefab(this.info.id).GetProperName(), this.info.quantity.ToString());
	}

	// Token: 0x06005D30 RID: 23856 RVA: 0x00220A4C File Offset: 0x0021EC4C
	private string GetSpawnableDescription()
	{
		Element element = ElementLoader.GetElement(this.info.id.ToTag());
		if (element != null)
		{
			return element.Description();
		}
		GameObject prefab = Assets.GetPrefab(this.info.id);
		if (prefab == null)
		{
			return "";
		}
		InfoDescription component = prefab.GetComponent<InfoDescription>();
		if (component != null)
		{
			return component.description;
		}
		return prefab.GetProperName();
	}

	// Token: 0x06005D31 RID: 23857 RVA: 0x00220ABC File Offset: 0x0021ECBC
	private string GetSpawnableEffects()
	{
		GameObject prefab = Assets.GetPrefab(this.info.id);
		if (prefab == null)
		{
			return "";
		}
		string text = "";
		IGameObjectEffectDescriptor[] components = prefab.GetComponents<IGameObjectEffectDescriptor>();
		if (components != null)
		{
			IGameObjectEffectDescriptor[] array = components;
			for (int i = 0; i < array.Length; i++)
			{
				List<Descriptor> descriptors = array[i].GetDescriptors(prefab);
				if (descriptors != null)
				{
					foreach (Descriptor descriptor in descriptors)
					{
						text = text + descriptor.text + "\n";
					}
				}
			}
		}
		return text;
	}

	// Token: 0x06005D32 RID: 23858 RVA: 0x00220B74 File Offset: 0x0021ED74
	private void SetInfoText()
	{
		this.characterName.SetText(this.GetSpawnableName());
		this.effects.SetText(this.GetSpawnableEffects());
		this.description.SetText(this.GetSpawnableDescription());
		this.itemName.SetText(this.GetSpawnableName());
		this.quantity.SetText(this.GetSpawnableQuantityOnly());
		this.currentQuantity.SetText(this.GetCurrentQuantity(ClusterManager.Instance.activeWorld.worldInventory));
	}

	// Token: 0x06005D33 RID: 23859 RVA: 0x00220BF8 File Offset: 0x0021EDF8
	public void SelectDeliverable()
	{
		if (this.controller != null)
		{
			this.controller.AddDeliverable(this.carePackageInstanceData);
		}
		if (MusicManager.instance.SongIsPlaying("Music_SelectDuplicant"))
		{
			MusicManager.instance.SetSongParameter("Music_SelectDuplicant", "songSection", 1f, true);
		}
		this.selectButton.GetComponent<ImageToggleState>().SetActive();
		this.selectButton.ClearOnClick();
		this.selectButton.onClick += delegate
		{
			this.DeselectDeliverable();
			if (MusicManager.instance.SongIsPlaying("Music_SelectDuplicant"))
			{
				MusicManager.instance.SetSongParameter("Music_SelectDuplicant", "songSection", 0f, true);
			}
		};
		this.selectedBorder.SetActive(true);
		this.titleBar.color = this.selectedTitleColor;
	}

	// Token: 0x06005D34 RID: 23860 RVA: 0x00220CA0 File Offset: 0x0021EEA0
	public void DeselectDeliverable()
	{
		if (this.controller != null)
		{
			this.controller.RemoveDeliverable(this.carePackageInstanceData);
		}
		this.selectButton.GetComponent<ImageToggleState>().SetInactive();
		this.selectButton.Deselect();
		this.selectButton.ClearOnClick();
		this.selectButton.onClick += delegate
		{
			this.SelectDeliverable();
		};
		this.selectedBorder.SetActive(false);
		this.titleBar.color = this.deselectedTitleColor;
	}

	// Token: 0x06005D35 RID: 23861 RVA: 0x00220D26 File Offset: 0x0021EF26
	private void OnReplacedEvent(ITelepadDeliverable stats)
	{
		if (stats == this.carePackageInstanceData)
		{
			this.DeselectDeliverable();
		}
	}

	// Token: 0x06005D36 RID: 23862 RVA: 0x00220D38 File Offset: 0x0021EF38
	private void OnCharacterSelectionLimitReached()
	{
		if (this.controller != null && this.controller.IsSelected(this.info))
		{
			return;
		}
		this.selectButton.ClearOnClick();
		if (this.controller.AllowsReplacing)
		{
			this.selectButton.onClick += this.ReplaceCharacterSelection;
			return;
		}
		this.selectButton.onClick += this.CantSelectCharacter;
	}

	// Token: 0x06005D37 RID: 23863 RVA: 0x00220DAE File Offset: 0x0021EFAE
	private void CantSelectCharacter()
	{
		KMonoBehaviour.PlaySound(GlobalAssets.GetSound("Negative", false));
	}

	// Token: 0x06005D38 RID: 23864 RVA: 0x00220DC0 File Offset: 0x0021EFC0
	private void ReplaceCharacterSelection()
	{
		if (this.controller == null)
		{
			return;
		}
		this.controller.RemoveLast();
		this.SelectDeliverable();
	}

	// Token: 0x06005D39 RID: 23865 RVA: 0x00220DE4 File Offset: 0x0021EFE4
	private void OnCharacterSelectionLimitUnReached()
	{
		if (this.controller != null && this.controller.IsSelected(this.info))
		{
			return;
		}
		this.selectButton.ClearOnClick();
		this.selectButton.onClick += delegate
		{
			this.SelectDeliverable();
		};
	}

	// Token: 0x06005D3A RID: 23866 RVA: 0x00220E35 File Offset: 0x0021F035
	public void SetReshufflingState(bool enable)
	{
		this.reshuffleButton.gameObject.SetActive(enable);
	}

	// Token: 0x06005D3B RID: 23867 RVA: 0x00220E48 File Offset: 0x0021F048
	private void Reshuffle(bool is_starter)
	{
		if (this.controller != null && this.controller.IsSelected(this.info))
		{
			this.DeselectDeliverable();
		}
		this.ClearEntryIcons();
		this.GenerateCharacter(is_starter);
	}

	// Token: 0x06005D3C RID: 23868 RVA: 0x00220E80 File Offset: 0x0021F080
	public void SetController(CharacterSelectionController csc)
	{
		if (csc == this.controller)
		{
			return;
		}
		this.controller = csc;
		CharacterSelectionController characterSelectionController = this.controller;
		characterSelectionController.OnLimitReachedEvent = (global::System.Action)Delegate.Combine(characterSelectionController.OnLimitReachedEvent, new global::System.Action(this.OnCharacterSelectionLimitReached));
		CharacterSelectionController characterSelectionController2 = this.controller;
		characterSelectionController2.OnLimitUnreachedEvent = (global::System.Action)Delegate.Combine(characterSelectionController2.OnLimitUnreachedEvent, new global::System.Action(this.OnCharacterSelectionLimitUnReached));
		CharacterSelectionController characterSelectionController3 = this.controller;
		characterSelectionController3.OnReshuffleEvent = (Action<bool>)Delegate.Combine(characterSelectionController3.OnReshuffleEvent, new Action<bool>(this.Reshuffle));
		CharacterSelectionController characterSelectionController4 = this.controller;
		characterSelectionController4.OnReplacedEvent = (Action<ITelepadDeliverable>)Delegate.Combine(characterSelectionController4.OnReplacedEvent, new Action<ITelepadDeliverable>(this.OnReplacedEvent));
	}

	// Token: 0x06005D3D RID: 23869 RVA: 0x00220F40 File Offset: 0x0021F140
	public void DisableSelectButton()
	{
		this.selectButton.soundPlayer.AcceptClickCondition = () => false;
		this.selectButton.GetComponent<ImageToggleState>().SetDisabled();
		this.selectButton.soundPlayer.Enabled = false;
	}

	// Token: 0x06005D3E RID: 23870 RVA: 0x00220FA0 File Offset: 0x0021F1A0
	private bool IsCharacterRedundant()
	{
		foreach (ITelepadDeliverableContainer telepadDeliverableContainer in CarePackageContainer.containers)
		{
			if (telepadDeliverableContainer != this)
			{
				CarePackageContainer carePackageContainer = telepadDeliverableContainer as CarePackageContainer;
				if (carePackageContainer != null && carePackageContainer.info == this.info)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06005D3F RID: 23871 RVA: 0x00221014 File Offset: 0x0021F214
	public string GetValueColor(bool isPositive)
	{
		if (!isPositive)
		{
			return "<color=#ff2222ff>";
		}
		return "<color=green>";
	}

	// Token: 0x06005D40 RID: 23872 RVA: 0x00221024 File Offset: 0x0021F224
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.IsAction(global::Action.Escape))
		{
			this.controller.OnPressBack();
		}
		if (!KInputManager.currentControllerIsGamepad)
		{
			e.Consumed = true;
		}
	}

	// Token: 0x06005D41 RID: 23873 RVA: 0x00221048 File Offset: 0x0021F248
	public override void OnKeyUp(KButtonEvent e)
	{
		if (!KInputManager.currentControllerIsGamepad)
		{
			e.Consumed = true;
		}
	}

	// Token: 0x06005D42 RID: 23874 RVA: 0x00221058 File Offset: 0x0021F258
	protected override void OnCmpEnable()
	{
		base.OnActivate();
		if (this.info == null)
		{
			return;
		}
		this.ClearEntryIcons();
		this.SetAnimator();
		this.SetInfoText();
	}

	// Token: 0x06005D43 RID: 23875 RVA: 0x0022107C File Offset: 0x0021F27C
	private void ClearEntryIcons()
	{
		for (int i = 0; i < this.entryIcons.Count; i++)
		{
			global::UnityEngine.Object.Destroy(this.entryIcons[i]);
		}
	}

	// Token: 0x04003DEB RID: 15851
	[Header("UI References")]
	[SerializeField]
	private GameObject contentBody;

	// Token: 0x04003DEC RID: 15852
	[SerializeField]
	private LocText characterName;

	// Token: 0x04003DED RID: 15853
	public GameObject selectedBorder;

	// Token: 0x04003DEE RID: 15854
	[SerializeField]
	private Image titleBar;

	// Token: 0x04003DEF RID: 15855
	[SerializeField]
	private Color selectedTitleColor;

	// Token: 0x04003DF0 RID: 15856
	[SerializeField]
	private Color deselectedTitleColor;

	// Token: 0x04003DF1 RID: 15857
	[SerializeField]
	private KButton reshuffleButton;

	// Token: 0x04003DF2 RID: 15858
	private KBatchedAnimController animController;

	// Token: 0x04003DF3 RID: 15859
	[SerializeField]
	private LocText itemName;

	// Token: 0x04003DF4 RID: 15860
	[SerializeField]
	private LocText quantity;

	// Token: 0x04003DF5 RID: 15861
	[SerializeField]
	private LocText currentQuantity;

	// Token: 0x04003DF6 RID: 15862
	[SerializeField]
	private LocText description;

	// Token: 0x04003DF7 RID: 15863
	[SerializeField]
	private LocText effects;

	// Token: 0x04003DF8 RID: 15864
	[SerializeField]
	private KToggle selectButton;

	// Token: 0x04003DF9 RID: 15865
	private CarePackageInfo info;

	// Token: 0x04003DFA RID: 15866
	public CarePackageContainer.CarePackageInstanceData carePackageInstanceData;

	// Token: 0x04003DFB RID: 15867
	private CharacterSelectionController controller;

	// Token: 0x04003DFC RID: 15868
	private static List<ITelepadDeliverableContainer> containers;

	// Token: 0x04003DFD RID: 15869
	[SerializeField]
	private Sprite enabledSpr;

	// Token: 0x04003DFE RID: 15870
	[SerializeField]
	private List<CarePackageContainer.ProfessionIcon> professionIcons;

	// Token: 0x04003DFF RID: 15871
	private Dictionary<string, Sprite> professionIconMap;

	// Token: 0x04003E00 RID: 15872
	public float baseCharacterScale = 0.38f;

	// Token: 0x04003E01 RID: 15873
	private List<GameObject> entryIcons = new List<GameObject>();

	// Token: 0x02001D4C RID: 7500
	[Serializable]
	public struct ProfessionIcon
	{
		// Token: 0x040088C5 RID: 35013
		public string professionName;

		// Token: 0x040088C6 RID: 35014
		public Sprite iconImg;
	}

	// Token: 0x02001D4D RID: 7501
	public class CarePackageInstanceData : ITelepadDeliverable
	{
		// Token: 0x0600ADAE RID: 44462 RVA: 0x003C56FA File Offset: 0x003C38FA
		public GameObject Deliver(Vector3 position)
		{
			GameObject gameObject = this.info.Deliver(position);
			gameObject.GetComponent<CarePackage>().SetFacade(this.facadeID);
			return gameObject;
		}

		// Token: 0x040088C7 RID: 35015
		public CarePackageInfo info;

		// Token: 0x040088C8 RID: 35016
		public string facadeID;
	}
}
