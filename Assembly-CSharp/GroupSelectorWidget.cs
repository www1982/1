using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000E84 RID: 3716
public class GroupSelectorWidget : MonoBehaviour
{
	// Token: 0x06007667 RID: 30311 RVA: 0x002D4A67 File Offset: 0x002D2C67
	public void Initialize(object widget_id, IList<GroupSelectorWidget.ItemData> options, GroupSelectorWidget.ItemCallbacks item_callbacks)
	{
		this.widgetID = widget_id;
		this.options = options;
		this.itemCallbacks = item_callbacks;
		this.addItemButton.onClick += this.OnAddItemClicked;
	}

	// Token: 0x06007668 RID: 30312 RVA: 0x002D4A98 File Offset: 0x002D2C98
	public void Reconfigure(IList<int> selected_option_indices)
	{
		this.selectedOptionIndices.Clear();
		this.selectedOptionIndices.AddRange(selected_option_indices);
		this.selectedOptionIndices.Sort();
		this.addItemButton.isInteractable = this.selectedOptionIndices.Count < this.options.Count;
		this.RebuildSelectedVisualizers();
	}

	// Token: 0x06007669 RID: 30313 RVA: 0x002D4AF0 File Offset: 0x002D2CF0
	private void OnAddItemClicked()
	{
		if (!this.IsSubPanelOpen())
		{
			if (this.RebuildSubPanelOptions() > 0)
			{
				this.unselectedItemsPanel.GetComponent<GridLayoutGroup>().constraintCount = Mathf.Min(this.numExpectedPanelColumns, this.unselectedItemsPanel.childCount);
				this.unselectedItemsPanel.gameObject.SetActive(true);
				this.unselectedItemsPanel.GetComponent<Selectable>().Select();
				return;
			}
		}
		else
		{
			this.CloseSubPanel();
		}
	}

	// Token: 0x0600766A RID: 30314 RVA: 0x002D4B5C File Offset: 0x002D2D5C
	private void OnItemAdded(int option_idx)
	{
		if (this.itemCallbacks.onItemAdded != null)
		{
			this.itemCallbacks.onItemAdded(this.widgetID, this.options[option_idx].userData);
			this.RebuildSubPanelOptions();
		}
	}

	// Token: 0x0600766B RID: 30315 RVA: 0x002D4B99 File Offset: 0x002D2D99
	private void OnItemRemoved(int option_idx)
	{
		if (this.itemCallbacks.onItemRemoved != null)
		{
			this.itemCallbacks.onItemRemoved(this.widgetID, this.options[option_idx].userData);
		}
	}

	// Token: 0x0600766C RID: 30316 RVA: 0x002D4BD0 File Offset: 0x002D2DD0
	private void RebuildSelectedVisualizers()
	{
		foreach (GameObject gameObject in this.selectedVisualizers)
		{
			Util.KDestroyGameObject(gameObject);
		}
		this.selectedVisualizers.Clear();
		foreach (int num in this.selectedOptionIndices)
		{
			GameObject gameObject2 = this.CreateItem(num, new Action<int>(this.OnItemRemoved), this.selectedItemsPanel.gameObject, true);
			this.selectedVisualizers.Add(gameObject2);
		}
	}

	// Token: 0x0600766D RID: 30317 RVA: 0x002D4C94 File Offset: 0x002D2E94
	private GameObject CreateItem(int idx, Action<int> on_click, GameObject parent, bool is_selected_item)
	{
		GameObject gameObject = Util.KInstantiateUI(this.itemTemplate, parent, true);
		KButton component = gameObject.GetComponent<KButton>();
		component.onClick += delegate
		{
			on_click(idx);
		};
		component.fgImage.sprite = this.options[idx].sprite;
		if (parent == this.selectedItemsPanel.gameObject)
		{
			HierarchyReferences component2 = component.GetComponent<HierarchyReferences>();
			if (component2 != null)
			{
				Component reference = component2.GetReference("CancelImg");
				if (reference != null)
				{
					reference.gameObject.SetActive(true);
				}
			}
		}
		gameObject.GetComponent<ToolTip>().OnToolTip = () => this.itemCallbacks.getItemHoverText(this.widgetID, this.options[idx].userData, is_selected_item);
		return gameObject;
	}

	// Token: 0x0600766E RID: 30318 RVA: 0x002D4D66 File Offset: 0x002D2F66
	public bool IsSubPanelOpen()
	{
		return this.unselectedItemsPanel.gameObject.activeSelf;
	}

	// Token: 0x0600766F RID: 30319 RVA: 0x002D4D78 File Offset: 0x002D2F78
	public void CloseSubPanel()
	{
		this.ClearSubPanelOptions();
		this.unselectedItemsPanel.gameObject.SetActive(false);
	}

	// Token: 0x06007670 RID: 30320 RVA: 0x002D4D94 File Offset: 0x002D2F94
	private void ClearSubPanelOptions()
	{
		foreach (object obj in this.unselectedItemsPanel.transform)
		{
			Util.KDestroyGameObject(((Transform)obj).gameObject);
		}
	}

	// Token: 0x06007671 RID: 30321 RVA: 0x002D4DF4 File Offset: 0x002D2FF4
	private int RebuildSubPanelOptions()
	{
		IList<int> list = this.itemCallbacks.getSubPanelDisplayIndices(this.widgetID);
		if (list.Count > 0)
		{
			this.ClearSubPanelOptions();
			using (IEnumerator<int> enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					int num = enumerator.Current;
					if (!this.selectedOptionIndices.Contains(num))
					{
						this.CreateItem(num, new Action<int>(this.OnItemAdded), this.unselectedItemsPanel.gameObject, false);
					}
				}
				goto IL_007E;
			}
		}
		this.CloseSubPanel();
		IL_007E:
		return list.Count;
	}

	// Token: 0x0400523E RID: 21054
	[SerializeField]
	private GameObject itemTemplate;

	// Token: 0x0400523F RID: 21055
	[SerializeField]
	private RectTransform selectedItemsPanel;

	// Token: 0x04005240 RID: 21056
	[SerializeField]
	private RectTransform unselectedItemsPanel;

	// Token: 0x04005241 RID: 21057
	[SerializeField]
	private KButton addItemButton;

	// Token: 0x04005242 RID: 21058
	[SerializeField]
	private int numExpectedPanelColumns = 3;

	// Token: 0x04005243 RID: 21059
	private object widgetID;

	// Token: 0x04005244 RID: 21060
	private GroupSelectorWidget.ItemCallbacks itemCallbacks;

	// Token: 0x04005245 RID: 21061
	private IList<GroupSelectorWidget.ItemData> options;

	// Token: 0x04005246 RID: 21062
	private List<int> selectedOptionIndices = new List<int>();

	// Token: 0x04005247 RID: 21063
	private List<GameObject> selectedVisualizers = new List<GameObject>();

	// Token: 0x02002076 RID: 8310
	[Serializable]
	public struct ItemData
	{
		// Token: 0x0600B67E RID: 46718 RVA: 0x003E1F6D File Offset: 0x003E016D
		public ItemData(Sprite sprite, object user_data)
		{
			this.sprite = sprite;
			this.userData = user_data;
		}

		// Token: 0x04009451 RID: 37969
		public Sprite sprite;

		// Token: 0x04009452 RID: 37970
		public object userData;
	}

	// Token: 0x02002077 RID: 8311
	public struct ItemCallbacks
	{
		// Token: 0x04009453 RID: 37971
		public Func<object, IList<int>> getSubPanelDisplayIndices;

		// Token: 0x04009454 RID: 37972
		public Action<object, object> onItemAdded;

		// Token: 0x04009455 RID: 37973
		public Action<object, object> onItemRemoved;

		// Token: 0x04009456 RID: 37974
		public Func<object, object, bool, string> getItemHoverText;
	}
}
