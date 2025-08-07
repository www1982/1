using System;
using System.Linq;
using Database;
using UnityEngine;

// Token: 0x02000CD0 RID: 3280
public class FullBodyUIMinionWidget : KMonoBehaviour
{
	// Token: 0x1700075B RID: 1883
	// (get) Token: 0x06006511 RID: 25873 RVA: 0x002601E5 File Offset: 0x0025E3E5
	// (set) Token: 0x06006512 RID: 25874 RVA: 0x002601ED File Offset: 0x0025E3ED
	public KBatchedAnimController animController { get; private set; }

	// Token: 0x06006513 RID: 25875 RVA: 0x002601F6 File Offset: 0x0025E3F6
	protected override void OnSpawn()
	{
		this.TrySpawnDisplayMinion();
	}

	// Token: 0x06006514 RID: 25876 RVA: 0x00260200 File Offset: 0x0025E400
	private void TrySpawnDisplayMinion()
	{
		if (this.animController == null)
		{
			this.animController = Util.KInstantiateUI(Assets.GetPrefab(new Tag("FullMinionUIPortrait")), this.duplicantAnimAnchor.gameObject, false).GetComponent<KBatchedAnimController>();
			this.animController.gameObject.SetActive(true);
			this.animController.animScale = 0.38f;
		}
	}

	// Token: 0x06006515 RID: 25877 RVA: 0x00260268 File Offset: 0x0025E468
	private void InitializeAnimator()
	{
		this.TrySpawnDisplayMinion();
		this.animController.Queue("idle_default", KAnim.PlayMode.Loop, 1f, 0f);
		Accessorizer component = this.animController.GetComponent<Accessorizer>();
		for (int i = component.GetAccessories().Count - 1; i >= 0; i--)
		{
			component.RemoveAccessory(component.GetAccessories()[i].Get());
		}
	}

	// Token: 0x06006516 RID: 25878 RVA: 0x002602D8 File Offset: 0x0025E4D8
	public void SetDefaultPortraitAnimator()
	{
		MinionIdentity minionIdentity = ((Components.MinionIdentities.Count > 0) ? Components.MinionIdentities[0] : null);
		HashedString hashedString = ((minionIdentity != null) ? minionIdentity.personalityResourceId : Db.Get().Personalities.resources.GetRandom<Personality>().Id);
		this.InitializeAnimator();
		this.animController.GetComponent<Accessorizer>().ApplyMinionPersonality(Db.Get().Personalities.Get(hashedString));
		Accessorizer accessorizer = ((minionIdentity != null) ? minionIdentity.GetComponent<Accessorizer>() : null);
		KAnim.Build.Symbol symbol = null;
		KAnim.Build.Symbol symbol2 = null;
		if (accessorizer)
		{
			symbol = accessorizer.GetAccessory(Db.Get().AccessorySlots.Hair).symbol;
			symbol2 = Db.Get().AccessorySlots.HatHair.Lookup("hat_" + HashCache.Get().Get(accessorizer.GetAccessory(Db.Get().AccessorySlots.Hair).symbol.hash)).symbol;
		}
		this.UpdateHatOverride(null, symbol, symbol2);
		this.UpdateClothingOverride(this.animController.GetComponent<SymbolOverrideController>(), minionIdentity, null);
	}

	// Token: 0x06006517 RID: 25879 RVA: 0x00260400 File Offset: 0x0025E600
	public void SetPortraitAnimator(IAssignableIdentity assignableIdentity)
	{
		if (assignableIdentity == null || assignableIdentity.IsNull())
		{
			this.SetDefaultPortraitAnimator();
			return;
		}
		this.InitializeAnimator();
		string text = "";
		MinionIdentity minionIdentity;
		StoredMinionIdentity storedMinionIdentity;
		this.GetMinionIdentity(assignableIdentity, out minionIdentity, out storedMinionIdentity);
		Accessorizer accessorizer = null;
		Accessorizer component = this.animController.GetComponent<Accessorizer>();
		KAnim.Build.Symbol symbol = null;
		KAnim.Build.Symbol symbol2 = null;
		if (minionIdentity != null)
		{
			accessorizer = minionIdentity.GetComponent<Accessorizer>();
			foreach (ResourceRef<Accessory> resourceRef in accessorizer.GetAccessories())
			{
				component.AddAccessory(resourceRef.Get());
			}
			text = minionIdentity.GetComponent<MinionResume>().CurrentHat;
			symbol = accessorizer.GetAccessory(Db.Get().AccessorySlots.Hair).symbol;
			symbol2 = Db.Get().AccessorySlots.HatHair.Lookup("hat_" + HashCache.Get().Get(accessorizer.GetAccessory(Db.Get().AccessorySlots.Hair).symbol.hash)).symbol;
		}
		else if (storedMinionIdentity != null)
		{
			foreach (ResourceRef<Accessory> resourceRef2 in storedMinionIdentity.accessories)
			{
				component.AddAccessory(resourceRef2.Get());
			}
			text = storedMinionIdentity.currentHat;
			symbol = storedMinionIdentity.GetAccessory(Db.Get().AccessorySlots.Hair).symbol;
			symbol2 = Db.Get().AccessorySlots.HatHair.Lookup("hat_" + HashCache.Get().Get(storedMinionIdentity.GetAccessory(Db.Get().AccessorySlots.Hair).symbol.hash)).symbol;
		}
		this.UpdateHatOverride(text, symbol, symbol2);
		this.UpdateClothingOverride(this.animController.GetComponent<SymbolOverrideController>(), minionIdentity, storedMinionIdentity);
	}

	// Token: 0x06006518 RID: 25880 RVA: 0x00260610 File Offset: 0x0025E810
	private void UpdateHatOverride(string current_hat, KAnim.Build.Symbol hair_symbol, KAnim.Build.Symbol hat_hair_symbol)
	{
		AccessorySlot hat = Db.Get().AccessorySlots.Hat;
		this.animController.SetSymbolVisiblity(hat.targetSymbolId, !string.IsNullOrEmpty(current_hat));
		this.animController.SetSymbolVisiblity(Db.Get().AccessorySlots.Hair.targetSymbolId, string.IsNullOrEmpty(current_hat));
		this.animController.SetSymbolVisiblity(Db.Get().AccessorySlots.HatHair.targetSymbolId, !string.IsNullOrEmpty(current_hat));
		SymbolOverrideController component = this.animController.GetComponent<SymbolOverrideController>();
		if (hair_symbol != null)
		{
			component.AddSymbolOverride("snapto_hair_always", hair_symbol, 1);
		}
		if (hat_hair_symbol != null)
		{
			component.AddSymbolOverride(Db.Get().AccessorySlots.HatHair.targetSymbolId, hat_hair_symbol, 1);
		}
	}

	// Token: 0x06006519 RID: 25881 RVA: 0x002606E8 File Offset: 0x0025E8E8
	private void UpdateClothingOverride(SymbolOverrideController symbolOverrideController, MinionIdentity identity, StoredMinionIdentity storedMinionIdentity)
	{
		string[] array = null;
		if (identity != null)
		{
			array = identity.GetComponent<WearableAccessorizer>().GetClothingItemsIds(ClothingOutfitUtility.OutfitType.Clothing);
		}
		else if (storedMinionIdentity != null)
		{
			array = storedMinionIdentity.GetClothingItemIds(ClothingOutfitUtility.OutfitType.Clothing);
		}
		if (array != null)
		{
			this.animController.GetComponent<WearableAccessorizer>().ApplyClothingItems(ClothingOutfitUtility.OutfitType.Clothing, array.Select((string i) => Db.Get().Permits.ClothingItems.Get(i)));
		}
	}

	// Token: 0x0600651A RID: 25882 RVA: 0x00260759 File Offset: 0x0025E959
	public void UpdateEquipment(Equippable equippable, KAnimFile animFile)
	{
		this.animController.GetComponent<WearableAccessorizer>().ApplyEquipment(equippable, animFile);
	}

	// Token: 0x0600651B RID: 25883 RVA: 0x0026076D File Offset: 0x0025E96D
	public void RemoveEquipment(Equippable equippable)
	{
		this.animController.GetComponent<WearableAccessorizer>().RemoveEquipment(equippable);
	}

	// Token: 0x0600651C RID: 25884 RVA: 0x00260780 File Offset: 0x0025E980
	private void GetMinionIdentity(IAssignableIdentity assignableIdentity, out MinionIdentity minionIdentity, out StoredMinionIdentity storedMinionIdentity)
	{
		if (assignableIdentity is MinionAssignablesProxy)
		{
			minionIdentity = ((MinionAssignablesProxy)assignableIdentity).GetTargetGameObject().GetComponent<MinionIdentity>();
			storedMinionIdentity = ((MinionAssignablesProxy)assignableIdentity).GetTargetGameObject().GetComponent<StoredMinionIdentity>();
			return;
		}
		minionIdentity = assignableIdentity as MinionIdentity;
		storedMinionIdentity = assignableIdentity as StoredMinionIdentity;
	}

	// Token: 0x0400450E RID: 17678
	[SerializeField]
	private GameObject duplicantAnimAnchor;

	// Token: 0x04004510 RID: 17680
	public const float UI_MINION_PORTRAIT_ANIM_SCALE = 0.38f;
}
