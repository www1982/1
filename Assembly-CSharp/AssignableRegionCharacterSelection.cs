using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000C66 RID: 3174
[AddComponentMenu("KMonoBehaviour/scripts/AssignableRegionCharacterSelection")]
public class AssignableRegionCharacterSelection : KMonoBehaviour
{
	// Token: 0x14000021 RID: 33
	// (add) Token: 0x060060D8 RID: 24792 RVA: 0x0023CDD4 File Offset: 0x0023AFD4
	// (remove) Token: 0x060060D9 RID: 24793 RVA: 0x0023CE0C File Offset: 0x0023B00C
	public event Action<MinionIdentity> OnDuplicantSelected;

	// Token: 0x060060DA RID: 24794 RVA: 0x0023CE41 File Offset: 0x0023B041
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.buttonPool = new UIPool<KButton>(this.buttonPrefab);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060060DB RID: 24795 RVA: 0x0023CE68 File Offset: 0x0023B068
	public void Open()
	{
		base.gameObject.SetActive(true);
		this.buttonPool.ClearAll();
		foreach (MinionIdentity minionIdentity in Components.MinionIdentities.Items)
		{
			KButton btn = this.buttonPool.GetFreeElement(this.buttonParent, true);
			CrewPortrait componentInChildren = btn.GetComponentInChildren<CrewPortrait>();
			componentInChildren.SetIdentityObject(minionIdentity, true);
			this.portraitList.Add(componentInChildren);
			btn.ClearOnClick();
			btn.onClick += delegate
			{
				this.SelectDuplicant(btn);
			};
			this.buttonIdentityMap.Add(btn, minionIdentity);
		}
	}

	// Token: 0x060060DC RID: 24796 RVA: 0x0023CF50 File Offset: 0x0023B150
	public void Close()
	{
		this.buttonPool.DestroyAllActive();
		this.buttonIdentityMap.Clear();
		this.portraitList.Clear();
		base.gameObject.SetActive(false);
	}

	// Token: 0x060060DD RID: 24797 RVA: 0x0023CF7F File Offset: 0x0023B17F
	private void SelectDuplicant(KButton btn)
	{
		if (this.OnDuplicantSelected != null)
		{
			this.OnDuplicantSelected(this.buttonIdentityMap[btn]);
		}
		this.Close();
	}

	// Token: 0x0400416E RID: 16750
	[SerializeField]
	private KButton buttonPrefab;

	// Token: 0x0400416F RID: 16751
	[SerializeField]
	private GameObject buttonParent;

	// Token: 0x04004170 RID: 16752
	private UIPool<KButton> buttonPool;

	// Token: 0x04004171 RID: 16753
	private Dictionary<KButton, MinionIdentity> buttonIdentityMap = new Dictionary<KButton, MinionIdentity>();

	// Token: 0x04004172 RID: 16754
	private List<CrewPortrait> portraitList = new List<CrewPortrait>();
}
