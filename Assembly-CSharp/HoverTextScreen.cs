using System;
using UnityEngine;

// Token: 0x02000C1E RID: 3102
public class HoverTextScreen : KScreen
{
	// Token: 0x06005DEB RID: 24043 RVA: 0x00225837 File Offset: 0x00223A37
	public static void DestroyInstance()
	{
		HoverTextScreen.Instance = null;
	}

	// Token: 0x06005DEC RID: 24044 RVA: 0x0022583F File Offset: 0x00223A3F
	protected override void OnActivate()
	{
		base.OnActivate();
		HoverTextScreen.Instance = this;
		this.drawer = new HoverTextDrawer(this.skin.skin, base.GetComponent<RectTransform>());
	}

	// Token: 0x06005DED RID: 24045 RVA: 0x0022586C File Offset: 0x00223A6C
	public HoverTextDrawer BeginDrawing()
	{
		Vector2 zero = Vector2.zero;
		Vector2 vector = KInputManager.GetMousePos();
		RectTransform rectTransform = base.transform.parent as RectTransform;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, vector, base.transform.parent.GetComponent<Canvas>().worldCamera, out zero);
		zero.x += rectTransform.sizeDelta.x / 2f;
		zero.y -= rectTransform.sizeDelta.y / 2f;
		this.drawer.BeginDrawing(zero);
		return this.drawer;
	}

	// Token: 0x06005DEE RID: 24046 RVA: 0x00225904 File Offset: 0x00223B04
	private void Update()
	{
		bool flag = PlayerController.Instance.ActiveTool.ShowHoverUI();
		this.drawer.SetEnabled(flag);
	}

	// Token: 0x06005DEF RID: 24047 RVA: 0x00225930 File Offset: 0x00223B30
	public Sprite GetSprite(string byName)
	{
		foreach (Sprite sprite in this.HoverIcons)
		{
			if (sprite != null && sprite.name == byName)
			{
				return sprite;
			}
		}
		global::Debug.LogWarning("No icon named " + byName + " was found on HoverTextScreen.prefab");
		return null;
	}

	// Token: 0x06005DF0 RID: 24048 RVA: 0x00225985 File Offset: 0x00223B85
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		this.drawer.Cleanup();
	}

	// Token: 0x04003E8D RID: 16013
	[SerializeField]
	private HoverTextSkin skin;

	// Token: 0x04003E8E RID: 16014
	public Sprite[] HoverIcons;

	// Token: 0x04003E8F RID: 16015
	public HoverTextDrawer drawer;

	// Token: 0x04003E90 RID: 16016
	public static HoverTextScreen Instance;
}
