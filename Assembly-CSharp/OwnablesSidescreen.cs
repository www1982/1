using System;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000E15 RID: 3605
public class OwnablesSidescreen : SideScreenContent
{
	// Token: 0x060071CD RID: 29133 RVA: 0x002B4284 File Offset: 0x002B2484
	private void DefineCategories()
	{
		if (this.categories == null)
		{
			OwnablesSidescreen.Category[] array = new OwnablesSidescreen.Category[2];
			array[0] = new OwnablesSidescreen.Category((IAssignableIdentity assignableIdentity) => (assignableIdentity as MinionIdentity).GetEquipment(), new OwnablesSidescreenCategoryRow.Data(UI.UISIDESCREENS.OWNABLESSIDESCREEN.CATEGORIES.SUITS, new OwnablesSidescreenCategoryRow.AssignableSlotData[]
			{
				new OwnablesSidescreenCategoryRow.AssignableSlotData(Db.Get().AssignableSlots.Suit, new Func<IAssignableIdentity, bool>(this.Always)),
				new OwnablesSidescreenCategoryRow.AssignableSlotData(Db.Get().AssignableSlots.Outfit, new Func<IAssignableIdentity, bool>(this.Always))
			}));
			array[1] = new OwnablesSidescreen.Category((IAssignableIdentity assignableIdentity) => assignableIdentity.GetSoleOwner(), new OwnablesSidescreenCategoryRow.Data(UI.UISIDESCREENS.OWNABLESSIDESCREEN.CATEGORIES.AMENITIES, new OwnablesSidescreenCategoryRow.AssignableSlotData[]
			{
				new OwnablesSidescreenCategoryRow.AssignableSlotData(Db.Get().AssignableSlots.Bed, new Func<IAssignableIdentity, bool>(this.Always)),
				new OwnablesSidescreenCategoryRow.AssignableSlotData(Db.Get().AssignableSlots.Toilet, new Func<IAssignableIdentity, bool>(this.Always)),
				new OwnablesSidescreenCategoryRow.AssignableSlotData(Db.Get().AssignableSlots.MessStation, new Func<IAssignableIdentity, bool>(MessStation.CanBeAssignedTo))
			}));
			this.categories = array;
		}
	}

	// Token: 0x060071CE RID: 29134 RVA: 0x002B43EB File Offset: 0x002B25EB
	private bool Always(IAssignableIdentity identity)
	{
		return true;
	}

	// Token: 0x060071CF RID: 29135 RVA: 0x002B43EE File Offset: 0x002B25EE
	private Func<IAssignableIdentity, bool> HasAmount(string amountID)
	{
		return delegate(IAssignableIdentity identity)
		{
			if (identity == null)
			{
				return false;
			}
			GameObject targetGameObject = identity.GetOwners()[0].GetComponent<MinionAssignablesProxy>().GetTargetGameObject();
			return Db.Get().Amounts.Get(amountID).Lookup(targetGameObject) != null;
		};
	}

	// Token: 0x060071D0 RID: 29136 RVA: 0x002B4407 File Offset: 0x002B2607
	protected override void OnSpawn()
	{
		base.OnSpawn();
	}

	// Token: 0x060071D1 RID: 29137 RVA: 0x002B440F File Offset: 0x002B260F
	private void ActivateSecondSidescreen(AssignableSlotInstance slot)
	{
		((OwnablesSecondSideScreen)DetailsScreen.Instance.SetSecondarySideScreen(this.selectedSlotScreenPrefab, slot.slot.Name)).SetSlot(slot);
		if (slot != null && this.OnSlotInstanceSelected != null)
		{
			this.OnSlotInstanceSelected(slot);
		}
	}

	// Token: 0x060071D2 RID: 29138 RVA: 0x002B444E File Offset: 0x002B264E
	private void DeactivateSecondScreen()
	{
		DetailsScreen.Instance.ClearSecondarySideScreen();
	}

	// Token: 0x060071D3 RID: 29139 RVA: 0x002B445C File Offset: 0x002B265C
	public override void SetTarget(GameObject target)
	{
		base.SetTarget(target);
		this.UnsubscribeFromLastTarget();
		this.lastSelectedSlot = null;
		this.DefineCategories();
		this.CreateCategoryRows();
		this.DeactivateSecondScreen();
		this.RefreshSelectedStatusOnRows();
		IAssignableIdentity component = target.GetComponent<IAssignableIdentity>();
		for (int i = 0; i < this.categoryRows.Length; i++)
		{
			Assignables assignables = this.categories[i].getAssignablesFn(component);
			this.categoryRows[i].SetOwner(assignables);
		}
		this.titleSection.SetActive(target.GetComponent<MinionIdentity>().model == BionicMinionConfig.MODEL);
		MinionIdentity minionIdentity = component as MinionIdentity;
		if (minionIdentity != null)
		{
			this.lastTarget = minionIdentity;
			this.minionDestroyedCallbackIDX = minionIdentity.gameObject.Subscribe(1502190696, new Action<object>(this.OnTargetDestroyed));
		}
	}

	// Token: 0x060071D4 RID: 29140 RVA: 0x002B452E File Offset: 0x002B272E
	private void OnTargetDestroyed(object o)
	{
		this.ClearTarget();
	}

	// Token: 0x060071D5 RID: 29141 RVA: 0x002B4538 File Offset: 0x002B2738
	public override void ClearTarget()
	{
		base.ClearTarget();
		this.lastSelectedSlot = null;
		this.RefreshSelectedStatusOnRows();
		for (int i = 0; i < this.categoryRows.Length; i++)
		{
			this.categoryRows[i].SetOwner(null);
		}
		this.DeactivateSecondScreen();
		this.UnsubscribeFromLastTarget();
	}

	// Token: 0x060071D6 RID: 29142 RVA: 0x002B4588 File Offset: 0x002B2788
	private void CreateCategoryRows()
	{
		if (this.categoryRows == null)
		{
			this.originalCategoryRow.gameObject.SetActive(false);
			this.categoryRows = new OwnablesSidescreenCategoryRow[this.categories.Length];
			for (int i = 0; i < this.categories.Length; i++)
			{
				OwnablesSidescreenCategoryRow.Data data = this.categories[i].data;
				OwnablesSidescreenCategoryRow component = Util.KInstantiateUI(this.originalCategoryRow.gameObject, this.originalCategoryRow.transform.parent.gameObject, false).GetComponent<OwnablesSidescreenCategoryRow>();
				OwnablesSidescreenCategoryRow ownablesSidescreenCategoryRow = component;
				ownablesSidescreenCategoryRow.OnSlotRowClicked = (Action<OwnablesSidescreenItemRow>)Delegate.Combine(ownablesSidescreenCategoryRow.OnSlotRowClicked, new Action<OwnablesSidescreenItemRow>(this.OnSlotRowClicked));
				component.gameObject.SetActive(true);
				component.SetCategoryData(data);
				this.categoryRows[i] = component;
			}
			this.RefreshSelectedStatusOnRows();
		}
	}

	// Token: 0x060071D7 RID: 29143 RVA: 0x002B465F File Offset: 0x002B285F
	private void OnSlotRowClicked(OwnablesSidescreenItemRow slotRow)
	{
		if (slotRow.IsLocked || slotRow.SlotInstance == this.lastSelectedSlot)
		{
			this.SetSelectedSlot(null);
			return;
		}
		this.SetSelectedSlot(slotRow.SlotInstance);
	}

	// Token: 0x060071D8 RID: 29144 RVA: 0x002B468C File Offset: 0x002B288C
	public void RefreshSelectedStatusOnRows()
	{
		if (this.categoryRows == null)
		{
			return;
		}
		for (int i = 0; i < this.categoryRows.Length; i++)
		{
			this.categoryRows[i].SetSelectedRow_VisualsOnly(this.lastSelectedSlot);
		}
	}

	// Token: 0x060071D9 RID: 29145 RVA: 0x002B46C8 File Offset: 0x002B28C8
	public void SetSelectedSlot(AssignableSlotInstance slot)
	{
		this.lastSelectedSlot = slot;
		if (slot != null)
		{
			this.ActivateSecondSidescreen(slot);
		}
		else
		{
			this.DeactivateSecondScreen();
		}
		this.RefreshSelectedStatusOnRows();
	}

	// Token: 0x060071DA RID: 29146 RVA: 0x002B46EC File Offset: 0x002B28EC
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		if (this.categoryRows != null)
		{
			for (int i = 0; i < this.categoryRows.Length; i++)
			{
				if (this.categoryRows[i] != null)
				{
					this.categoryRows[i].SetOwner(null);
				}
			}
		}
		this.UnsubscribeFromLastTarget();
	}

	// Token: 0x060071DB RID: 29147 RVA: 0x002B473E File Offset: 0x002B293E
	private void UnsubscribeFromLastTarget()
	{
		if (this.lastTarget != null && this.minionDestroyedCallbackIDX != -1)
		{
			this.lastTarget.Unsubscribe(this.minionDestroyedCallbackIDX);
		}
		this.minionDestroyedCallbackIDX = -1;
		this.lastTarget = null;
	}

	// Token: 0x060071DC RID: 29148 RVA: 0x002B4776 File Offset: 0x002B2976
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<IAssignableIdentity>() != null;
	}

	// Token: 0x060071DD RID: 29149 RVA: 0x002B4781 File Offset: 0x002B2981
	public void OnValidate()
	{
	}

	// Token: 0x060071DE RID: 29150 RVA: 0x002B4783 File Offset: 0x002B2983
	private void SetScrollBarVisibility(bool isVisible)
	{
		this.scrollbarSection.gameObject.SetActive(isVisible);
		this.mainLayoutGroup.padding.right = (isVisible ? 20 : 0);
		this.scrollRect.enabled = isVisible;
	}

	// Token: 0x04004E4F RID: 20047
	public OwnablesSecondSideScreen selectedSlotScreenPrefab;

	// Token: 0x04004E50 RID: 20048
	public OwnablesSidescreenCategoryRow originalCategoryRow;

	// Token: 0x04004E51 RID: 20049
	[Header("Editor Settings")]
	public bool usingSlider = true;

	// Token: 0x04004E52 RID: 20050
	public GameObject titleSection;

	// Token: 0x04004E53 RID: 20051
	public GameObject scrollbarSection;

	// Token: 0x04004E54 RID: 20052
	public VerticalLayoutGroup mainLayoutGroup;

	// Token: 0x04004E55 RID: 20053
	public KScrollRect scrollRect;

	// Token: 0x04004E56 RID: 20054
	private OwnablesSidescreenCategoryRow[] categoryRows;

	// Token: 0x04004E57 RID: 20055
	private AssignableSlotInstance lastSelectedSlot;

	// Token: 0x04004E58 RID: 20056
	private OwnablesSidescreen.Category[] categories;

	// Token: 0x04004E59 RID: 20057
	public Action<AssignableSlotInstance> OnSlotInstanceSelected;

	// Token: 0x04004E5A RID: 20058
	private MinionIdentity lastTarget;

	// Token: 0x04004E5B RID: 20059
	private int minionDestroyedCallbackIDX = -1;

	// Token: 0x0200201B RID: 8219
	public struct Category
	{
		// Token: 0x0600B53F RID: 46399 RVA: 0x003DF38F File Offset: 0x003DD58F
		public Category(Func<IAssignableIdentity, Assignables> getAssignablesFn, OwnablesSidescreenCategoryRow.Data categoryData)
		{
			this.getAssignablesFn = getAssignablesFn;
			this.data = categoryData;
		}

		// Token: 0x04009316 RID: 37654
		public Func<IAssignableIdentity, Assignables> getAssignablesFn;

		// Token: 0x04009317 RID: 37655
		public OwnablesSidescreenCategoryRow.Data data;
	}
}
