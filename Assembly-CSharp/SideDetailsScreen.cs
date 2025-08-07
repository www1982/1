using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000DC9 RID: 3529
public class SideDetailsScreen : KScreen
{
	// Token: 0x06006F3F RID: 28479 RVA: 0x002A5DC1 File Offset: 0x002A3FC1
	protected override void OnSpawn()
	{
		base.OnSpawn();
		SideDetailsScreen.Instance = this;
		this.Initialize();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06006F40 RID: 28480 RVA: 0x002A5DE1 File Offset: 0x002A3FE1
	protected override void OnForcedCleanUp()
	{
		SideDetailsScreen.Instance = null;
		base.OnForcedCleanUp();
	}

	// Token: 0x06006F41 RID: 28481 RVA: 0x002A5DF0 File Offset: 0x002A3FF0
	private void Initialize()
	{
		if (this.screens == null)
		{
			return;
		}
		this.rectTransform = base.GetComponent<RectTransform>();
		this.screenMap = new Dictionary<string, SideTargetScreen>();
		List<SideTargetScreen> list = new List<SideTargetScreen>();
		foreach (SideTargetScreen sideTargetScreen in this.screens)
		{
			SideTargetScreen sideTargetScreen2 = Util.KInstantiateUI<SideTargetScreen>(sideTargetScreen.gameObject, this.body.gameObject, false);
			sideTargetScreen2.gameObject.SetActive(false);
			list.Add(sideTargetScreen2);
		}
		list.ForEach(delegate(SideTargetScreen s)
		{
			this.screenMap.Add(s.name, s);
		});
		this.backButton.onClick += delegate
		{
			this.Show(false);
		};
	}

	// Token: 0x06006F42 RID: 28482 RVA: 0x002A5EB4 File Offset: 0x002A40B4
	public void SetTitle(string newTitle)
	{
		this.title.text = newTitle;
	}

	// Token: 0x06006F43 RID: 28483 RVA: 0x002A5EC4 File Offset: 0x002A40C4
	public void SetScreen(string screenName, object content, float x)
	{
		if (!this.screenMap.ContainsKey(screenName))
		{
			global::Debug.LogError("Tried to open a screen that does exist on the manager!");
			return;
		}
		if (content == null)
		{
			global::Debug.LogError("Tried to set " + screenName + " with null content!");
			return;
		}
		if (!base.gameObject.activeInHierarchy)
		{
			base.gameObject.SetActive(true);
		}
		Rect rect = this.rectTransform.rect;
		this.rectTransform.offsetMin = new Vector2(x, this.rectTransform.offsetMin.y);
		this.rectTransform.offsetMax = new Vector2(x + rect.width, this.rectTransform.offsetMax.y);
		if (this.activeScreen != null)
		{
			this.activeScreen.gameObject.SetActive(false);
		}
		this.activeScreen = this.screenMap[screenName];
		this.activeScreen.gameObject.SetActive(true);
		this.SetTitle(this.activeScreen.displayName);
		this.activeScreen.SetTarget(content);
	}

	// Token: 0x04004C83 RID: 19587
	[SerializeField]
	private List<SideTargetScreen> screens;

	// Token: 0x04004C84 RID: 19588
	[SerializeField]
	private LocText title;

	// Token: 0x04004C85 RID: 19589
	[SerializeField]
	private KButton backButton;

	// Token: 0x04004C86 RID: 19590
	[SerializeField]
	private RectTransform body;

	// Token: 0x04004C87 RID: 19591
	private RectTransform rectTransform;

	// Token: 0x04004C88 RID: 19592
	private Dictionary<string, SideTargetScreen> screenMap;

	// Token: 0x04004C89 RID: 19593
	private SideTargetScreen activeScreen;

	// Token: 0x04004C8A RID: 19594
	public static SideDetailsScreen Instance;
}
