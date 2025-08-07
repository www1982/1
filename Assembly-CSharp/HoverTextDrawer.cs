using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000C1D RID: 3101
public class HoverTextDrawer
{
	// Token: 0x06005DDE RID: 24030 RVA: 0x00225214 File Offset: 0x00223414
	public HoverTextDrawer(HoverTextDrawer.Skin skin, RectTransform parent)
	{
		this.shadowBars = new HoverTextDrawer.Pool<Image>(skin.shadowBarWidget.gameObject, parent);
		this.selectBorders = new HoverTextDrawer.Pool<Image>(skin.selectBorderWidget.gameObject, parent);
		this.textWidgets = new HoverTextDrawer.Pool<LocText>(skin.textWidget.gameObject, parent);
		this.iconWidgets = new HoverTextDrawer.Pool<Image>(skin.iconWidget.gameObject, parent);
		this.skin = skin;
	}

	// Token: 0x06005DDF RID: 24031 RVA: 0x0022528A File Offset: 0x0022348A
	public void SetEnabled(bool enabled)
	{
		this.shadowBars.SetEnabled(enabled);
		this.textWidgets.SetEnabled(enabled);
		this.iconWidgets.SetEnabled(enabled);
		this.selectBorders.SetEnabled(enabled);
	}

	// Token: 0x06005DE0 RID: 24032 RVA: 0x002252BC File Offset: 0x002234BC
	public void BeginDrawing(Vector2 root_pos)
	{
		this.rootPos = root_pos + this.skin.baseOffset;
		if (this.skin.enableDebugOffset)
		{
			this.rootPos += this.skin.debugOffset;
		}
		this.currentPos = this.rootPos;
		this.textWidgets.BeginDrawing();
		this.iconWidgets.BeginDrawing();
		this.shadowBars.BeginDrawing();
		this.selectBorders.BeginDrawing();
		this.firstShadowBar = true;
		this.minLineHeight = 0;
	}

	// Token: 0x06005DE1 RID: 24033 RVA: 0x0022534F File Offset: 0x0022354F
	public void EndDrawing()
	{
		this.shadowBars.EndDrawing();
		this.iconWidgets.EndDrawing();
		this.textWidgets.EndDrawing();
		this.selectBorders.EndDrawing();
	}

	// Token: 0x06005DE2 RID: 24034 RVA: 0x00225380 File Offset: 0x00223580
	public void DrawText(string text, TextStyleSetting style, Color color, bool override_color = true)
	{
		if (!this.skin.drawWidgets)
		{
			return;
		}
		LocText widget = this.textWidgets.Draw(this.currentPos).widget;
		Color color2 = Color.white;
		if (widget.textStyleSetting != style)
		{
			widget.textStyleSetting = style;
			widget.ApplySettings();
		}
		if (style != null)
		{
			color2 = style.textColor;
		}
		if (override_color)
		{
			color2 = color;
		}
		widget.color = color2;
		if (widget.text != text)
		{
			widget.text = text;
			widget.KForceUpdateDirty();
		}
		this.currentPos.x = this.currentPos.x + widget.renderedWidth;
		this.maxShadowX = Mathf.Max(this.currentPos.x, this.maxShadowX);
		this.minLineHeight = (int)Mathf.Max((float)this.minLineHeight, widget.renderedHeight);
	}

	// Token: 0x06005DE3 RID: 24035 RVA: 0x00225455 File Offset: 0x00223655
	public void DrawText(string text, TextStyleSetting style)
	{
		this.DrawText(text, style, Color.white, false);
	}

	// Token: 0x06005DE4 RID: 24036 RVA: 0x00225465 File Offset: 0x00223665
	public void AddIndent(int width = 36)
	{
		if (!this.skin.drawWidgets)
		{
			return;
		}
		this.currentPos.x = this.currentPos.x + (float)width;
	}

	// Token: 0x06005DE5 RID: 24037 RVA: 0x00225488 File Offset: 0x00223688
	public void NewLine(int min_height = 26)
	{
		if (!this.skin.drawWidgets)
		{
			return;
		}
		this.currentPos.y = this.currentPos.y - (float)Math.Max(min_height, this.minLineHeight);
		this.currentPos.x = this.rootPos.x;
		this.minLineHeight = 0;
	}

	// Token: 0x06005DE6 RID: 24038 RVA: 0x002254DC File Offset: 0x002236DC
	public void DrawIcon(Sprite icon, int min_width = 18)
	{
		this.DrawIcon(icon, Color.white, min_width, 2);
	}

	// Token: 0x06005DE7 RID: 24039 RVA: 0x002254EC File Offset: 0x002236EC
	public void DrawIcon(Sprite icon, Color color, int image_size = 18, int horizontal_spacing = 2)
	{
		if (!this.skin.drawWidgets)
		{
			return;
		}
		this.AddIndent(horizontal_spacing);
		HoverTextDrawer.Pool<Image>.Entry entry = this.iconWidgets.Draw(this.currentPos + this.skin.shadowImageOffset);
		entry.widget.sprite = icon;
		entry.widget.color = this.skin.shadowImageColor;
		entry.rect.sizeDelta = new Vector2((float)image_size, (float)image_size);
		HoverTextDrawer.Pool<Image>.Entry entry2 = this.iconWidgets.Draw(this.currentPos);
		entry2.widget.sprite = icon;
		entry2.widget.color = color;
		entry2.rect.sizeDelta = new Vector2((float)image_size, (float)image_size);
		this.AddIndent(horizontal_spacing);
		this.currentPos.x = this.currentPos.x + (float)image_size;
		this.maxShadowX = Mathf.Max(this.currentPos.x, this.maxShadowX);
	}

	// Token: 0x06005DE8 RID: 24040 RVA: 0x002255D8 File Offset: 0x002237D8
	public void BeginShadowBar(bool selected = false)
	{
		if (!this.skin.drawWidgets)
		{
			return;
		}
		if (this.firstShadowBar)
		{
			this.firstShadowBar = false;
		}
		else
		{
			this.NewLine(22);
		}
		this.isShadowBarSelected = selected;
		this.shadowStartPos = this.currentPos;
		this.maxShadowX = this.rootPos.x;
	}

	// Token: 0x06005DE9 RID: 24041 RVA: 0x00225630 File Offset: 0x00223830
	public void EndShadowBar()
	{
		if (!this.skin.drawWidgets)
		{
			return;
		}
		this.NewLine(22);
		HoverTextDrawer.Pool<Image>.Entry entry = this.shadowBars.Draw(this.currentPos);
		entry.rect.anchoredPosition = this.shadowStartPos + new Vector2(-this.skin.shadowBarBorder.x, this.skin.shadowBarBorder.y);
		entry.rect.sizeDelta = new Vector2(this.maxShadowX - this.rootPos.x + this.skin.shadowBarBorder.x * 2f, this.shadowStartPos.y - this.currentPos.y + this.skin.shadowBarBorder.y * 2f);
		if (this.isShadowBarSelected)
		{
			HoverTextDrawer.Pool<Image>.Entry entry2 = this.selectBorders.Draw(this.currentPos);
			entry2.rect.anchoredPosition = this.shadowStartPos + new Vector2(-this.skin.shadowBarBorder.x - this.skin.selectBorder.x, this.skin.shadowBarBorder.y + this.skin.selectBorder.y);
			entry2.rect.sizeDelta = new Vector2(this.maxShadowX - this.rootPos.x + this.skin.shadowBarBorder.x * 2f + this.skin.selectBorder.x * 2f, this.shadowStartPos.y - this.currentPos.y + this.skin.shadowBarBorder.y * 2f + this.skin.selectBorder.y * 2f);
		}
	}

	// Token: 0x06005DEA RID: 24042 RVA: 0x00225814 File Offset: 0x00223A14
	public void Cleanup()
	{
		this.shadowBars.Cleanup();
		this.textWidgets.Cleanup();
		this.iconWidgets.Cleanup();
	}

	// Token: 0x04003E81 RID: 16001
	public HoverTextDrawer.Skin skin;

	// Token: 0x04003E82 RID: 16002
	private Vector2 currentPos;

	// Token: 0x04003E83 RID: 16003
	private Vector2 rootPos;

	// Token: 0x04003E84 RID: 16004
	private Vector2 shadowStartPos;

	// Token: 0x04003E85 RID: 16005
	private float maxShadowX;

	// Token: 0x04003E86 RID: 16006
	private bool firstShadowBar;

	// Token: 0x04003E87 RID: 16007
	private bool isShadowBarSelected;

	// Token: 0x04003E88 RID: 16008
	private int minLineHeight;

	// Token: 0x04003E89 RID: 16009
	private HoverTextDrawer.Pool<LocText> textWidgets;

	// Token: 0x04003E8A RID: 16010
	private HoverTextDrawer.Pool<Image> iconWidgets;

	// Token: 0x04003E8B RID: 16011
	private HoverTextDrawer.Pool<Image> shadowBars;

	// Token: 0x04003E8C RID: 16012
	private HoverTextDrawer.Pool<Image> selectBorders;

	// Token: 0x02001D5F RID: 7519
	[Serializable]
	public class Skin
	{
		// Token: 0x04008909 RID: 35081
		public Vector2 baseOffset;

		// Token: 0x0400890A RID: 35082
		public LocText textWidget;

		// Token: 0x0400890B RID: 35083
		public Image iconWidget;

		// Token: 0x0400890C RID: 35084
		public Vector2 shadowImageOffset;

		// Token: 0x0400890D RID: 35085
		public Color shadowImageColor;

		// Token: 0x0400890E RID: 35086
		public Image shadowBarWidget;

		// Token: 0x0400890F RID: 35087
		public Image selectBorderWidget;

		// Token: 0x04008910 RID: 35088
		public Vector2 shadowBarBorder;

		// Token: 0x04008911 RID: 35089
		public Vector2 selectBorder;

		// Token: 0x04008912 RID: 35090
		public bool drawWidgets;

		// Token: 0x04008913 RID: 35091
		public bool enableProfiling;

		// Token: 0x04008914 RID: 35092
		public bool enableDebugOffset;

		// Token: 0x04008915 RID: 35093
		public bool drawInProgressHoverText;

		// Token: 0x04008916 RID: 35094
		public Vector2 debugOffset;
	}

	// Token: 0x02001D60 RID: 7520
	private class Pool<WidgetType> where WidgetType : MonoBehaviour
	{
		// Token: 0x0600ADE6 RID: 44518 RVA: 0x003C65AC File Offset: 0x003C47AC
		public Pool(GameObject prefab, RectTransform master_root)
		{
			this.prefab = prefab;
			GameObject gameObject = new GameObject(typeof(WidgetType).Name);
			this.root = gameObject.AddComponent<RectTransform>();
			this.root.SetParent(master_root);
			this.root.anchoredPosition = Vector2.zero;
			this.root.anchorMin = Vector2.zero;
			this.root.anchorMax = Vector2.one;
			this.root.sizeDelta = Vector2.zero;
			gameObject.AddComponent<CanvasGroup>();
		}

		// Token: 0x0600ADE7 RID: 44519 RVA: 0x003C6648 File Offset: 0x003C4848
		public HoverTextDrawer.Pool<WidgetType>.Entry Draw(Vector2 pos)
		{
			HoverTextDrawer.Pool<WidgetType>.Entry entry;
			if (this.drawnWidgets < this.entries.Count)
			{
				entry = this.entries[this.drawnWidgets];
				if (!entry.widget.gameObject.activeSelf)
				{
					entry.widget.gameObject.SetActive(true);
				}
			}
			else
			{
				GameObject gameObject = Util.KInstantiateUI(this.prefab, this.root.gameObject, false);
				gameObject.SetActive(true);
				entry.widget = gameObject.GetComponent<WidgetType>();
				entry.rect = gameObject.GetComponent<RectTransform>();
				this.entries.Add(entry);
			}
			entry.rect.anchoredPosition = new Vector2(pos.x, pos.y);
			this.drawnWidgets++;
			return entry;
		}

		// Token: 0x0600ADE8 RID: 44520 RVA: 0x003C6719 File Offset: 0x003C4919
		public void BeginDrawing()
		{
			this.drawnWidgets = 0;
		}

		// Token: 0x0600ADE9 RID: 44521 RVA: 0x003C6724 File Offset: 0x003C4924
		public void EndDrawing()
		{
			for (int i = this.drawnWidgets; i < this.entries.Count; i++)
			{
				if (this.entries[i].widget.gameObject.activeSelf)
				{
					this.entries[i].widget.gameObject.SetActive(false);
				}
			}
		}

		// Token: 0x0600ADEA RID: 44522 RVA: 0x003C678F File Offset: 0x003C498F
		public void SetEnabled(bool enabled)
		{
			if (enabled)
			{
				this.root.gameObject.GetComponent<CanvasGroup>().alpha = 1f;
				return;
			}
			this.root.gameObject.GetComponent<CanvasGroup>().alpha = 0f;
		}

		// Token: 0x0600ADEB RID: 44523 RVA: 0x003C67CC File Offset: 0x003C49CC
		public void Cleanup()
		{
			foreach (HoverTextDrawer.Pool<WidgetType>.Entry entry in this.entries)
			{
				global::UnityEngine.Object.Destroy(entry.widget.gameObject);
			}
			this.entries.Clear();
		}

		// Token: 0x04008917 RID: 35095
		private GameObject prefab;

		// Token: 0x04008918 RID: 35096
		private RectTransform root;

		// Token: 0x04008919 RID: 35097
		private List<HoverTextDrawer.Pool<WidgetType>.Entry> entries = new List<HoverTextDrawer.Pool<WidgetType>.Entry>();

		// Token: 0x0400891A RID: 35098
		private int drawnWidgets;

		// Token: 0x020028DE RID: 10462
		public struct Entry
		{
			// Token: 0x0400B523 RID: 46371
			public WidgetType widget;

			// Token: 0x0400B524 RID: 46372
			public RectTransform rect;
		}
	}
}
