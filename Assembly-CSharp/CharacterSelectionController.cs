using System;
using System.Collections.Generic;
using Klei.CustomSettings;
using UnityEngine;

// Token: 0x02000C10 RID: 3088
public class CharacterSelectionController : KModalScreen
{
	// Token: 0x170006E0 RID: 1760
	// (get) Token: 0x06005D80 RID: 23936 RVA: 0x0022306D File Offset: 0x0022126D
	// (set) Token: 0x06005D81 RID: 23937 RVA: 0x00223075 File Offset: 0x00221275
	public bool IsStarterMinion { get; set; }

	// Token: 0x170006E1 RID: 1761
	// (get) Token: 0x06005D82 RID: 23938 RVA: 0x0022307E File Offset: 0x0022127E
	public bool AllowsReplacing
	{
		get
		{
			return this.allowsReplacing;
		}
	}

	// Token: 0x06005D83 RID: 23939 RVA: 0x00223086 File Offset: 0x00221286
	protected virtual void OnProceed()
	{
	}

	// Token: 0x06005D84 RID: 23940 RVA: 0x00223088 File Offset: 0x00221288
	protected virtual void OnDeliverableAdded()
	{
	}

	// Token: 0x06005D85 RID: 23941 RVA: 0x0022308A File Offset: 0x0022128A
	protected virtual void OnDeliverableRemoved()
	{
	}

	// Token: 0x06005D86 RID: 23942 RVA: 0x0022308C File Offset: 0x0022128C
	protected virtual void OnLimitReached()
	{
	}

	// Token: 0x06005D87 RID: 23943 RVA: 0x0022308E File Offset: 0x0022128E
	protected virtual void OnLimitUnreached()
	{
	}

	// Token: 0x06005D88 RID: 23944 RVA: 0x00223090 File Offset: 0x00221290
	protected virtual void InitializeContainers()
	{
		this.DisableProceedButton();
		if (this.containers != null && this.containers.Count > 0)
		{
			return;
		}
		this.OnReplacedEvent = null;
		this.containers = new List<ITelepadDeliverableContainer>();
		if (this.IsStarterMinion || CustomGameSettings.Instance.GetCurrentQualitySetting(CustomGameSettingConfigs.CarePackages).id != "Enabled")
		{
			this.numberOfDuplicantOptions = 3;
			this.numberOfCarePackageOptions = 0;
		}
		else
		{
			this.numberOfCarePackageOptions = ((global::UnityEngine.Random.Range(0, 101) > 70) ? 2 : 1);
			this.numberOfDuplicantOptions = 4 - this.numberOfCarePackageOptions;
		}
		for (int i = 0; i < this.numberOfDuplicantOptions; i++)
		{
			CharacterContainer characterContainer = Util.KInstantiateUI<CharacterContainer>(this.containerPrefab.gameObject, this.containerParent, false);
			characterContainer.SetController(this);
			characterContainer.SetReshufflingState(true);
			this.containers.Add(characterContainer);
		}
		for (int j = 0; j < this.numberOfCarePackageOptions; j++)
		{
			CarePackageContainer carePackageContainer = Util.KInstantiateUI<CarePackageContainer>(this.carePackageContainerPrefab.gameObject, this.containerParent, false);
			carePackageContainer.SetController(this);
			this.containers.Add(carePackageContainer);
			carePackageContainer.gameObject.transform.SetSiblingIndex(global::UnityEngine.Random.Range(0, carePackageContainer.transform.parent.childCount));
		}
		this.selectedDeliverables = new List<ITelepadDeliverable>();
	}

	// Token: 0x06005D89 RID: 23945 RVA: 0x002231D8 File Offset: 0x002213D8
	public virtual void OnPressBack()
	{
		foreach (ITelepadDeliverableContainer telepadDeliverableContainer in this.containers)
		{
			CharacterContainer characterContainer = telepadDeliverableContainer as CharacterContainer;
			if (characterContainer != null)
			{
				characterContainer.ForceStopEditingTitle();
			}
		}
		this.Show(false);
	}

	// Token: 0x06005D8A RID: 23946 RVA: 0x00223240 File Offset: 0x00221440
	public void RemoveLast()
	{
		if (this.selectedDeliverables == null || this.selectedDeliverables.Count == 0)
		{
			return;
		}
		ITelepadDeliverable telepadDeliverable = this.selectedDeliverables[this.selectedDeliverables.Count - 1];
		if (this.OnReplacedEvent != null)
		{
			this.OnReplacedEvent(telepadDeliverable);
		}
	}

	// Token: 0x06005D8B RID: 23947 RVA: 0x00223290 File Offset: 0x00221490
	public void AddDeliverable(ITelepadDeliverable deliverable)
	{
		if (this.selectedDeliverables.Contains(deliverable))
		{
			global::Debug.Log("Tried to add the same minion twice.");
			return;
		}
		if (this.selectedDeliverables.Count >= this.selectableCount)
		{
			global::Debug.LogError("Tried to add minions beyond the allowed limit");
			return;
		}
		this.selectedDeliverables.Add(deliverable);
		this.OnDeliverableAdded();
		if (this.selectedDeliverables.Count == this.selectableCount)
		{
			this.EnableProceedButton();
			if (this.OnLimitReachedEvent != null)
			{
				this.OnLimitReachedEvent();
			}
			this.OnLimitReached();
		}
	}

	// Token: 0x06005D8C RID: 23948 RVA: 0x00223318 File Offset: 0x00221518
	public void RemoveDeliverable(ITelepadDeliverable deliverable)
	{
		bool flag = this.selectedDeliverables.Count >= this.selectableCount;
		this.selectedDeliverables.Remove(deliverable);
		this.OnDeliverableRemoved();
		if (flag && this.selectedDeliverables.Count < this.selectableCount)
		{
			this.DisableProceedButton();
			if (this.OnLimitUnreachedEvent != null)
			{
				this.OnLimitUnreachedEvent();
			}
			this.OnLimitUnreached();
		}
	}

	// Token: 0x06005D8D RID: 23949 RVA: 0x00223382 File Offset: 0x00221582
	public bool IsSelected(ITelepadDeliverable deliverable)
	{
		return this.selectedDeliverables.Contains(deliverable);
	}

	// Token: 0x06005D8E RID: 23950 RVA: 0x00223390 File Offset: 0x00221590
	protected void EnableProceedButton()
	{
		this.proceedButton.isInteractable = true;
		this.proceedButton.ClearOnClick();
		this.proceedButton.onClick += delegate
		{
			this.OnProceed();
		};
	}

	// Token: 0x06005D8F RID: 23951 RVA: 0x002233C0 File Offset: 0x002215C0
	protected void DisableProceedButton()
	{
		this.proceedButton.ClearOnClick();
		this.proceedButton.isInteractable = false;
		this.proceedButton.onClick += delegate
		{
			KMonoBehaviour.PlaySound(GlobalAssets.GetSound("Negative", false));
		};
	}

	// Token: 0x04003E35 RID: 15925
	[SerializeField]
	private CharacterContainer containerPrefab;

	// Token: 0x04003E36 RID: 15926
	[SerializeField]
	private CarePackageContainer carePackageContainerPrefab;

	// Token: 0x04003E37 RID: 15927
	[SerializeField]
	private GameObject containerParent;

	// Token: 0x04003E38 RID: 15928
	[SerializeField]
	protected KButton proceedButton;

	// Token: 0x04003E39 RID: 15929
	protected int numberOfDuplicantOptions = 3;

	// Token: 0x04003E3A RID: 15930
	protected int numberOfCarePackageOptions;

	// Token: 0x04003E3B RID: 15931
	[SerializeField]
	protected int selectableCount;

	// Token: 0x04003E3C RID: 15932
	[SerializeField]
	private bool allowsReplacing;

	// Token: 0x04003E3E RID: 15934
	protected List<ITelepadDeliverable> selectedDeliverables;

	// Token: 0x04003E3F RID: 15935
	protected List<ITelepadDeliverableContainer> containers;

	// Token: 0x04003E40 RID: 15936
	public global::System.Action OnLimitReachedEvent;

	// Token: 0x04003E41 RID: 15937
	public global::System.Action OnLimitUnreachedEvent;

	// Token: 0x04003E42 RID: 15938
	public Action<bool> OnReshuffleEvent;

	// Token: 0x04003E43 RID: 15939
	public Action<ITelepadDeliverable> OnReplacedEvent;

	// Token: 0x04003E44 RID: 15940
	public global::System.Action OnProceedEvent;
}
