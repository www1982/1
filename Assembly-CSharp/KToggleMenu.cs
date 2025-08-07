using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000CFC RID: 3324
public class KToggleMenu : KScreen
{
	// Token: 0x1400002A RID: 42
	// (add) Token: 0x06006671 RID: 26225 RVA: 0x0026AC60 File Offset: 0x00268E60
	// (remove) Token: 0x06006672 RID: 26226 RVA: 0x0026AC98 File Offset: 0x00268E98
	public event KToggleMenu.OnSelect onSelect;

	// Token: 0x06006673 RID: 26227 RVA: 0x0026ACCD File Offset: 0x00268ECD
	public void Setup(IList<KToggleMenu.ToggleInfo> toggleInfo)
	{
		this.toggleInfo = toggleInfo;
		this.RefreshButtons();
	}

	// Token: 0x06006674 RID: 26228 RVA: 0x0026ACDC File Offset: 0x00268EDC
	protected void Setup()
	{
		this.RefreshButtons();
	}

	// Token: 0x06006675 RID: 26229 RVA: 0x0026ACE4 File Offset: 0x00268EE4
	private void RefreshButtons()
	{
		foreach (KToggle ktoggle in this.toggles)
		{
			if (ktoggle != null)
			{
				global::UnityEngine.Object.Destroy(ktoggle.gameObject);
			}
		}
		this.toggles.Clear();
		if (this.toggleInfo == null)
		{
			return;
		}
		Transform transform = ((this.toggleParent != null) ? this.toggleParent : base.transform);
		for (int i = 0; i < this.toggleInfo.Count; i++)
		{
			int idx = i;
			KToggleMenu.ToggleInfo toggleInfo = this.toggleInfo[i];
			if (toggleInfo == null)
			{
				this.toggles.Add(null);
			}
			else
			{
				KToggle ktoggle2 = global::UnityEngine.Object.Instantiate<KToggle>(this.prefab, Vector3.zero, Quaternion.identity);
				ktoggle2.gameObject.name = "Toggle:" + toggleInfo.text;
				ktoggle2.transform.SetParent(transform, false);
				ktoggle2.group = this.group;
				ktoggle2.onClick += delegate
				{
					this.OnClick(idx);
				};
				ktoggle2.GetComponentsInChildren<Text>(true)[0].text = toggleInfo.text;
				toggleInfo.toggle = ktoggle2;
				this.toggles.Add(ktoggle2);
			}
		}
	}

	// Token: 0x06006676 RID: 26230 RVA: 0x0026AE5C File Offset: 0x0026905C
	public int GetSelected()
	{
		return KToggleMenu.selected;
	}

	// Token: 0x06006677 RID: 26231 RVA: 0x0026AE63 File Offset: 0x00269063
	private void OnClick(int i)
	{
		UISounds.PlaySound(UISounds.Sound.ClickObject);
		if (this.onSelect == null)
		{
			return;
		}
		this.onSelect(this.toggleInfo[i]);
	}

	// Token: 0x06006678 RID: 26232 RVA: 0x0026AE8C File Offset: 0x0026908C
	public override void OnKeyDown(KButtonEvent e)
	{
		if (this.toggles == null)
		{
			return;
		}
		for (int i = 0; i < this.toggleInfo.Count; i++)
		{
			global::Action hotKey = this.toggleInfo[i].hotKey;
			if (hotKey != global::Action.NumActions && e.TryConsume(hotKey))
			{
				this.toggles[i].Click();
				return;
			}
		}
	}

	// Token: 0x0400462F RID: 17967
	[SerializeField]
	private Transform toggleParent;

	// Token: 0x04004630 RID: 17968
	[SerializeField]
	private KToggle prefab;

	// Token: 0x04004631 RID: 17969
	[SerializeField]
	private ToggleGroup group;

	// Token: 0x04004633 RID: 17971
	protected IList<KToggleMenu.ToggleInfo> toggleInfo;

	// Token: 0x04004634 RID: 17972
	protected List<KToggle> toggles = new List<KToggle>();

	// Token: 0x04004635 RID: 17973
	private static int selected = -1;

	// Token: 0x02001EC6 RID: 7878
	// (Invoke) Token: 0x0600B155 RID: 45397
	public delegate void OnSelect(KToggleMenu.ToggleInfo toggleInfo);

	// Token: 0x02001EC7 RID: 7879
	public class ToggleInfo
	{
		// Token: 0x0600B158 RID: 45400 RVA: 0x003D4806 File Offset: 0x003D2A06
		public ToggleInfo(string text, object user_data = null, global::Action hotKey = global::Action.NumActions)
		{
			this.text = text;
			this.userData = user_data;
			this.hotKey = hotKey;
		}

		// Token: 0x04008EDB RID: 36571
		public string text;

		// Token: 0x04008EDC RID: 36572
		public object userData;

		// Token: 0x04008EDD RID: 36573
		public KToggle toggle;

		// Token: 0x04008EDE RID: 36574
		public global::Action hotKey;
	}
}
