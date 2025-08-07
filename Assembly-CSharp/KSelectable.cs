using System;
using UnityEngine;

// Token: 0x020005CC RID: 1484
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/KSelectable")]
public class KSelectable : KMonoBehaviour
{
	// Token: 0x17000161 RID: 353
	// (get) Token: 0x0600223D RID: 8765 RVA: 0x000C4FAE File Offset: 0x000C31AE
	public bool IsSelected
	{
		get
		{
			return this.selected;
		}
	}

	// Token: 0x17000162 RID: 354
	// (get) Token: 0x0600223E RID: 8766 RVA: 0x000C4FB6 File Offset: 0x000C31B6
	// (set) Token: 0x0600223F RID: 8767 RVA: 0x000C4FC8 File Offset: 0x000C31C8
	public bool IsSelectable
	{
		get
		{
			return this.selectable && base.isActiveAndEnabled;
		}
		set
		{
			this.selectable = value;
		}
	}

	// Token: 0x17000163 RID: 355
	// (get) Token: 0x06002240 RID: 8768 RVA: 0x000C4FD1 File Offset: 0x000C31D1
	public bool DisableSelectMarker
	{
		get
		{
			return this.disableSelectMarker;
		}
	}

	// Token: 0x06002241 RID: 8769 RVA: 0x000C4FDC File Offset: 0x000C31DC
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.statusItemGroup = new StatusItemGroup(base.gameObject);
		base.GetComponent<KPrefabID>() != null;
		if (this.entityName == null || this.entityName.Length <= 0)
		{
			this.SetName(base.name);
		}
		if (this.entityGender == null)
		{
			this.entityGender = "NB";
		}
	}

	// Token: 0x06002242 RID: 8770 RVA: 0x000C5044 File Offset: 0x000C3244
	public virtual string GetName()
	{
		if (this.entityName == null || this.entityName == "" || this.entityName.Length <= 0)
		{
			global::Debug.Log("Warning Item has blank name!", base.gameObject);
			return base.name;
		}
		return this.entityName;
	}

	// Token: 0x06002243 RID: 8771 RVA: 0x000C5096 File Offset: 0x000C3296
	public void SetStatusIndicatorOffset(Vector3 offset)
	{
		if (this.statusItemGroup == null)
		{
			return;
		}
		this.statusItemGroup.SetOffset(offset);
	}

	// Token: 0x06002244 RID: 8772 RVA: 0x000C50AD File Offset: 0x000C32AD
	public void SetName(string name)
	{
		this.entityName = name;
	}

	// Token: 0x06002245 RID: 8773 RVA: 0x000C50B6 File Offset: 0x000C32B6
	public void SetGender(string Gender)
	{
		this.entityGender = Gender;
	}

	// Token: 0x06002246 RID: 8774 RVA: 0x000C50C0 File Offset: 0x000C32C0
	public float GetZoom()
	{
		Bounds bounds = Util.GetBounds(base.gameObject);
		return 1.05f * Mathf.Max(bounds.extents.x, bounds.extents.y);
	}

	// Token: 0x06002247 RID: 8775 RVA: 0x000C50FC File Offset: 0x000C32FC
	public Vector3 GetPortraitLocation()
	{
		return Util.GetBounds(base.gameObject).center;
	}

	// Token: 0x06002248 RID: 8776 RVA: 0x000C511C File Offset: 0x000C331C
	private void ClearHighlight()
	{
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		if (component != null)
		{
			component.HighlightColour = new Color(0f, 0f, 0f, 0f);
		}
		base.Trigger(-1201923725, false);
	}

	// Token: 0x06002249 RID: 8777 RVA: 0x000C5170 File Offset: 0x000C3370
	private void ApplyHighlight(float highlight)
	{
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		if (component != null)
		{
			component.HighlightColour = new Color(highlight, highlight, highlight, highlight);
		}
		base.Trigger(-1201923725, true);
	}

	// Token: 0x0600224A RID: 8778 RVA: 0x000C51B4 File Offset: 0x000C33B4
	public void Select()
	{
		this.selected = true;
		this.ClearHighlight();
		this.ApplyHighlight(0.2f);
		base.Trigger(-1503271301, true);
		if (base.GetComponent<LoopingSounds>() != null)
		{
			base.GetComponent<LoopingSounds>().UpdateObjectSelection(this.selected);
		}
		if (base.transform.GetComponentInParent<LoopingSounds>() != null)
		{
			base.transform.GetComponentInParent<LoopingSounds>().UpdateObjectSelection(this.selected);
		}
		int childCount = base.transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			int childCount2 = base.transform.GetChild(i).childCount;
			for (int j = 0; j < childCount2; j++)
			{
				if (base.transform.GetChild(i).transform.GetChild(j).GetComponent<LoopingSounds>() != null)
				{
					base.transform.GetChild(i).transform.GetChild(j).GetComponent<LoopingSounds>().UpdateObjectSelection(this.selected);
				}
			}
		}
		this.UpdateWorkerSelection(this.selected);
		this.UpdateWorkableSelection(this.selected);
	}

	// Token: 0x0600224B RID: 8779 RVA: 0x000C52CC File Offset: 0x000C34CC
	public void Unselect()
	{
		if (this.selected)
		{
			this.selected = false;
			this.ClearHighlight();
			base.Trigger(-1503271301, false);
		}
		if (base.GetComponent<LoopingSounds>() != null)
		{
			base.GetComponent<LoopingSounds>().UpdateObjectSelection(this.selected);
		}
		if (base.transform.GetComponentInParent<LoopingSounds>() != null)
		{
			base.transform.GetComponentInParent<LoopingSounds>().UpdateObjectSelection(this.selected);
		}
		foreach (object obj in base.transform)
		{
			Transform transform = (Transform)obj;
			if (transform.GetComponent<LoopingSounds>() != null)
			{
				transform.GetComponent<LoopingSounds>().UpdateObjectSelection(this.selected);
			}
		}
		this.UpdateWorkerSelection(this.selected);
		this.UpdateWorkableSelection(this.selected);
	}

	// Token: 0x0600224C RID: 8780 RVA: 0x000C53C4 File Offset: 0x000C35C4
	public void Hover(bool playAudio)
	{
		this.ClearHighlight();
		if (!DebugHandler.HideUI)
		{
			this.ApplyHighlight(0.25f);
		}
		if (playAudio)
		{
			this.PlayHoverSound();
		}
	}

	// Token: 0x0600224D RID: 8781 RVA: 0x000C53E7 File Offset: 0x000C35E7
	private void PlayHoverSound()
	{
		if (CellSelectionObject.IsSelectionObject(base.gameObject))
		{
			return;
		}
		UISounds.PlaySound(UISounds.Sound.Object_Mouseover);
	}

	// Token: 0x0600224E RID: 8782 RVA: 0x000C53FD File Offset: 0x000C35FD
	public void Unhover()
	{
		if (!this.selected)
		{
			this.ClearHighlight();
		}
	}

	// Token: 0x0600224F RID: 8783 RVA: 0x000C540D File Offset: 0x000C360D
	public Guid ToggleStatusItem(StatusItem status_item, bool on, object data = null)
	{
		if (on)
		{
			return this.AddStatusItem(status_item, data);
		}
		return this.RemoveStatusItem(status_item, false);
	}

	// Token: 0x06002250 RID: 8784 RVA: 0x000C5423 File Offset: 0x000C3623
	public Guid ToggleStatusItem(StatusItem status_item, Guid guid, bool show, object data = null)
	{
		if (show)
		{
			if (guid != Guid.Empty)
			{
				return guid;
			}
			return this.AddStatusItem(status_item, data);
		}
		else
		{
			if (guid != Guid.Empty)
			{
				return this.RemoveStatusItem(guid, false);
			}
			return guid;
		}
	}

	// Token: 0x06002251 RID: 8785 RVA: 0x000C5458 File Offset: 0x000C3658
	public Guid SetStatusItem(StatusItemCategory category, StatusItem status_item, object data = null)
	{
		if (this.statusItemGroup == null)
		{
			return Guid.Empty;
		}
		return this.statusItemGroup.SetStatusItem(category, status_item, data);
	}

	// Token: 0x06002252 RID: 8786 RVA: 0x000C5476 File Offset: 0x000C3676
	public Guid ReplaceStatusItem(Guid guid, StatusItem status_item, object data = null)
	{
		if (this.statusItemGroup == null)
		{
			return Guid.Empty;
		}
		if (guid != Guid.Empty)
		{
			this.statusItemGroup.RemoveStatusItem(guid, false);
		}
		return this.AddStatusItem(status_item, data);
	}

	// Token: 0x06002253 RID: 8787 RVA: 0x000C54A9 File Offset: 0x000C36A9
	public Guid AddStatusItem(StatusItem status_item, object data = null)
	{
		if (this.statusItemGroup == null)
		{
			return Guid.Empty;
		}
		return this.statusItemGroup.AddStatusItem(status_item, data, null);
	}

	// Token: 0x06002254 RID: 8788 RVA: 0x000C54C7 File Offset: 0x000C36C7
	public Guid RemoveStatusItem(StatusItem status_item, bool immediate = false)
	{
		if (this.statusItemGroup == null)
		{
			return Guid.Empty;
		}
		this.statusItemGroup.RemoveStatusItem(status_item, immediate);
		return Guid.Empty;
	}

	// Token: 0x06002255 RID: 8789 RVA: 0x000C54EA File Offset: 0x000C36EA
	public Guid RemoveStatusItem(Guid guid, bool immediate = false)
	{
		if (this.statusItemGroup == null)
		{
			return Guid.Empty;
		}
		this.statusItemGroup.RemoveStatusItem(guid, immediate);
		return Guid.Empty;
	}

	// Token: 0x06002256 RID: 8790 RVA: 0x000C550D File Offset: 0x000C370D
	public bool HasStatusItem(StatusItem status_item)
	{
		return this.statusItemGroup != null && this.statusItemGroup.HasStatusItem(status_item);
	}

	// Token: 0x06002257 RID: 8791 RVA: 0x000C5525 File Offset: 0x000C3725
	public StatusItemGroup.Entry GetStatusItem(StatusItemCategory category)
	{
		return this.statusItemGroup.GetStatusItem(category);
	}

	// Token: 0x06002258 RID: 8792 RVA: 0x000C5533 File Offset: 0x000C3733
	public StatusItemGroup GetStatusItemGroup()
	{
		return this.statusItemGroup;
	}

	// Token: 0x06002259 RID: 8793 RVA: 0x000C553C File Offset: 0x000C373C
	public void UpdateWorkerSelection(bool selected)
	{
		Workable[] components = base.GetComponents<Workable>();
		if (components.Length != 0)
		{
			for (int i = 0; i < components.Length; i++)
			{
				if (components[i].worker != null && components[i].GetComponent<LoopingSounds>() != null)
				{
					components[i].GetComponent<LoopingSounds>().UpdateObjectSelection(selected);
				}
			}
		}
	}

	// Token: 0x0600225A RID: 8794 RVA: 0x000C5590 File Offset: 0x000C3790
	public void UpdateWorkableSelection(bool selected)
	{
		WorkerBase component = base.GetComponent<WorkerBase>();
		if (component != null && component.GetWorkable() != null)
		{
			Workable workable = base.GetComponent<WorkerBase>().GetWorkable();
			if (workable.GetComponent<LoopingSounds>() != null)
			{
				workable.GetComponent<LoopingSounds>().UpdateObjectSelection(selected);
			}
		}
	}

	// Token: 0x0600225B RID: 8795 RVA: 0x000C55E1 File Offset: 0x000C37E1
	protected override void OnLoadLevel()
	{
		this.OnCleanUp();
		base.OnLoadLevel();
	}

	// Token: 0x0600225C RID: 8796 RVA: 0x000C55F0 File Offset: 0x000C37F0
	protected override void OnCleanUp()
	{
		if (this.statusItemGroup != null)
		{
			this.statusItemGroup.Destroy();
			this.statusItemGroup = null;
		}
		if (this.selected && SelectTool.Instance != null)
		{
			if (SelectTool.Instance.selected == this)
			{
				SelectTool.Instance.Select(null, true);
			}
			else
			{
				this.Unselect();
			}
		}
		base.OnCleanUp();
	}

	// Token: 0x040013EF RID: 5103
	private const float hoverHighlight = 0.25f;

	// Token: 0x040013F0 RID: 5104
	private const float selectHighlight = 0.2f;

	// Token: 0x040013F1 RID: 5105
	public string entityName;

	// Token: 0x040013F2 RID: 5106
	public string entityGender;

	// Token: 0x040013F3 RID: 5107
	private bool selected;

	// Token: 0x040013F4 RID: 5108
	[SerializeField]
	private bool selectable = true;

	// Token: 0x040013F5 RID: 5109
	[SerializeField]
	private bool disableSelectMarker;

	// Token: 0x040013F6 RID: 5110
	private StatusItemGroup statusItemGroup;
}
