using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000CC3 RID: 3267
[AddComponentMenu("KMonoBehaviour/scripts/DropDown")]
public class DropDown : KMonoBehaviour
{
	// Token: 0x17000755 RID: 1877
	// (get) Token: 0x060064AC RID: 25772 RVA: 0x0025D763 File Offset: 0x0025B963
	// (set) Token: 0x060064AD RID: 25773 RVA: 0x0025D76B File Offset: 0x0025B96B
	public bool open { get; private set; }

	// Token: 0x17000756 RID: 1878
	// (get) Token: 0x060064AE RID: 25774 RVA: 0x0025D774 File Offset: 0x0025B974
	public List<IListableOption> Entries
	{
		get
		{
			return this.entries;
		}
	}

	// Token: 0x060064AF RID: 25775 RVA: 0x0025D77C File Offset: 0x0025B97C
	public void Initialize(IEnumerable<IListableOption> contentKeys, Action<IListableOption, object> onEntrySelectedAction, Func<IListableOption, IListableOption, object, int> sortFunction = null, Action<DropDownEntry, object> refreshAction = null, bool displaySelectedValueWhenClosed = true, object targetData = null)
	{
		this.targetData = targetData;
		this.sortFunction = sortFunction;
		this.onEntrySelectedAction = onEntrySelectedAction;
		this.displaySelectedValueWhenClosed = displaySelectedValueWhenClosed;
		this.rowRefreshAction = refreshAction;
		this.ChangeContent(contentKeys);
		this.openButton.ClearOnClick();
		this.openButton.onClick += delegate
		{
			this.OnClick();
		};
		this.canvasScaler = GameScreenManager.Instance.ssOverlayCanvas.GetComponent<KCanvasScaler>();
	}

	// Token: 0x060064B0 RID: 25776 RVA: 0x0025D7ED File Offset: 0x0025B9ED
	public void CustomizeEmptyRow(string txt, Sprite icon)
	{
		this.emptyRowLabel = txt;
		this.emptyRowSprite = icon;
	}

	// Token: 0x060064B1 RID: 25777 RVA: 0x0025D7FD File Offset: 0x0025B9FD
	public void OnClick()
	{
		if (!this.open)
		{
			this.Open();
			return;
		}
		this.Close();
	}

	// Token: 0x060064B2 RID: 25778 RVA: 0x0025D814 File Offset: 0x0025BA14
	public void ChangeContent(IEnumerable<IListableOption> contentKeys)
	{
		this.entries.Clear();
		foreach (IListableOption listableOption in contentKeys)
		{
			this.entries.Add(listableOption);
		}
		this.built = false;
	}

	// Token: 0x060064B3 RID: 25779 RVA: 0x0025D874 File Offset: 0x0025BA74
	private void Update()
	{
		if (!this.open)
		{
			return;
		}
		if (!Input.GetMouseButtonDown(0) && Input.GetAxis("Mouse ScrollWheel") == 0f && !KInputManager.steamInputInterpreter.GetSteamInputActionIsDown(global::Action.MouseLeft))
		{
			return;
		}
		float canvasScale = this.canvasScaler.GetCanvasScale();
		if (this.scrollRect.rectTransform().GetPosition().x + this.scrollRect.rectTransform().sizeDelta.x * canvasScale < KInputManager.GetMousePos().x || this.scrollRect.rectTransform().GetPosition().x > KInputManager.GetMousePos().x || this.scrollRect.rectTransform().GetPosition().y - this.scrollRect.rectTransform().sizeDelta.y * canvasScale > KInputManager.GetMousePos().y || this.scrollRect.rectTransform().GetPosition().y < KInputManager.GetMousePos().y)
		{
			this.Close();
		}
	}

	// Token: 0x060064B4 RID: 25780 RVA: 0x0025D978 File Offset: 0x0025BB78
	private void Build(List<IListableOption> contentKeys)
	{
		this.built = true;
		for (int i = this.contentContainer.childCount - 1; i >= 0; i--)
		{
			Util.KDestroyGameObject(this.contentContainer.GetChild(i));
		}
		this.rowLookup.Clear();
		if (this.addEmptyRow)
		{
			this.emptyRow = Util.KInstantiateUI(this.rowEntryPrefab, this.contentContainer.gameObject, true);
			this.emptyRow.GetComponent<KButton>().onClick += delegate
			{
				this.onEntrySelectedAction(null, this.targetData);
				if (this.displaySelectedValueWhenClosed)
				{
					this.selectedLabel.text = this.emptyRowLabel ?? UI.DROPDOWN.NONE;
				}
				this.Close();
			};
			string text = this.emptyRowLabel ?? UI.DROPDOWN.NONE;
			this.emptyRow.GetComponent<DropDownEntry>().label.text = text;
			if (this.emptyRowSprite != null)
			{
				this.emptyRow.GetComponent<DropDownEntry>().image.sprite = this.emptyRowSprite;
			}
		}
		for (int j = 0; j < contentKeys.Count; j++)
		{
			GameObject gameObject = Util.KInstantiateUI(this.rowEntryPrefab, this.contentContainer.gameObject, true);
			IListableOption id = contentKeys[j];
			gameObject.GetComponent<DropDownEntry>().entryData = id;
			gameObject.GetComponent<KButton>().onClick += delegate
			{
				this.onEntrySelectedAction(id, this.targetData);
				if (this.displaySelectedValueWhenClosed)
				{
					this.selectedLabel.text = id.GetProperName();
				}
				this.Close();
			};
			this.rowLookup.Add(id, gameObject);
		}
		this.RefreshEntries();
		this.Close();
		this.scrollRect.gameObject.transform.SetParent(this.targetDropDownContainer.transform);
		this.scrollRect.gameObject.SetActive(false);
	}

	// Token: 0x060064B5 RID: 25781 RVA: 0x0025DB18 File Offset: 0x0025BD18
	private void RefreshEntries()
	{
		foreach (KeyValuePair<IListableOption, GameObject> keyValuePair in this.rowLookup)
		{
			DropDownEntry component = keyValuePair.Value.GetComponent<DropDownEntry>();
			component.label.text = keyValuePair.Key.GetProperName();
			if (component.portrait != null && keyValuePair.Key is IAssignableIdentity)
			{
				component.portrait.SetIdentityObject(keyValuePair.Key as IAssignableIdentity, true);
			}
		}
		if (this.sortFunction != null)
		{
			this.entries.Sort((IListableOption a, IListableOption b) => this.sortFunction(a, b, this.targetData));
			for (int i = 0; i < this.entries.Count; i++)
			{
				this.rowLookup[this.entries[i]].transform.SetAsFirstSibling();
			}
			if (this.emptyRow != null)
			{
				this.emptyRow.transform.SetAsFirstSibling();
			}
		}
		foreach (KeyValuePair<IListableOption, GameObject> keyValuePair2 in this.rowLookup)
		{
			DropDownEntry component2 = keyValuePair2.Value.GetComponent<DropDownEntry>();
			this.rowRefreshAction(component2, this.targetData);
		}
		if (this.emptyRow != null)
		{
			this.rowRefreshAction(this.emptyRow.GetComponent<DropDownEntry>(), this.targetData);
		}
	}

	// Token: 0x060064B6 RID: 25782 RVA: 0x0025DCBC File Offset: 0x0025BEBC
	protected override void OnCleanUp()
	{
		Util.KDestroyGameObject(this.scrollRect);
		base.OnCleanUp();
	}

	// Token: 0x060064B7 RID: 25783 RVA: 0x0025DCD0 File Offset: 0x0025BED0
	public void Open()
	{
		if (this.open)
		{
			return;
		}
		if (!this.built)
		{
			this.Build(this.entries);
		}
		else
		{
			this.RefreshEntries();
		}
		this.open = true;
		this.scrollRect.gameObject.SetActive(true);
		this.scrollRect.rectTransform().localScale = Vector3.one;
		foreach (KeyValuePair<IListableOption, GameObject> keyValuePair in this.rowLookup)
		{
			keyValuePair.Value.SetActive(true);
		}
		float num = Mathf.Max(32f, this.rowEntryPrefab.GetComponent<LayoutElement>().preferredHeight);
		this.scrollRect.rectTransform().sizeDelta = new Vector2(this.scrollRect.rectTransform().sizeDelta.x, num * (float)Mathf.Min(this.contentContainer.childCount, 8));
		Vector3 vector = this.dropdownAlignmentTarget.TransformPoint(this.dropdownAlignmentTarget.rect.x, this.dropdownAlignmentTarget.rect.y, 0f);
		Vector2 vector2 = new Vector2(Mathf.Min(0f, (float)Screen.width - (vector.x + (this.rowEntryPrefab.GetComponent<LayoutElement>().minWidth * this.canvasScaler.GetCanvasScale() + DropDown.edgePadding.x))), -Mathf.Min(0f, vector.y - (this.scrollRect.rectTransform().sizeDelta.y * this.canvasScaler.GetCanvasScale() + DropDown.edgePadding.y)));
		vector += vector2;
		this.scrollRect.rectTransform().SetPosition(vector);
	}

	// Token: 0x060064B8 RID: 25784 RVA: 0x0025DEB4 File Offset: 0x0025C0B4
	public void Close()
	{
		if (!this.open)
		{
			return;
		}
		this.open = false;
		foreach (KeyValuePair<IListableOption, GameObject> keyValuePair in this.rowLookup)
		{
			keyValuePair.Value.SetActive(false);
		}
		this.scrollRect.SetActive(false);
	}

	// Token: 0x040044B2 RID: 17586
	public GameObject targetDropDownContainer;

	// Token: 0x040044B3 RID: 17587
	public LocText selectedLabel;

	// Token: 0x040044B5 RID: 17589
	public KButton openButton;

	// Token: 0x040044B6 RID: 17590
	public Transform contentContainer;

	// Token: 0x040044B7 RID: 17591
	public GameObject scrollRect;

	// Token: 0x040044B8 RID: 17592
	public RectTransform dropdownAlignmentTarget;

	// Token: 0x040044B9 RID: 17593
	public GameObject rowEntryPrefab;

	// Token: 0x040044BA RID: 17594
	public bool addEmptyRow = true;

	// Token: 0x040044BB RID: 17595
	private static Vector2 edgePadding = new Vector2(8f, 8f);

	// Token: 0x040044BC RID: 17596
	public object targetData;

	// Token: 0x040044BD RID: 17597
	private List<IListableOption> entries = new List<IListableOption>();

	// Token: 0x040044BE RID: 17598
	private Action<IListableOption, object> onEntrySelectedAction;

	// Token: 0x040044BF RID: 17599
	private Action<DropDownEntry, object> rowRefreshAction;

	// Token: 0x040044C0 RID: 17600
	public Dictionary<IListableOption, GameObject> rowLookup = new Dictionary<IListableOption, GameObject>();

	// Token: 0x040044C1 RID: 17601
	private Func<IListableOption, IListableOption, object, int> sortFunction;

	// Token: 0x040044C2 RID: 17602
	private GameObject emptyRow;

	// Token: 0x040044C3 RID: 17603
	private string emptyRowLabel;

	// Token: 0x040044C4 RID: 17604
	private Sprite emptyRowSprite;

	// Token: 0x040044C5 RID: 17605
	private bool built;

	// Token: 0x040044C6 RID: 17606
	private bool displaySelectedValueWhenClosed = true;

	// Token: 0x040044C7 RID: 17607
	private const int ROWS_BEFORE_SCROLL = 8;

	// Token: 0x040044C8 RID: 17608
	private KCanvasScaler canvasScaler;
}
