using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000E7F RID: 3711
[AddComponentMenu("KMonoBehaviour/scripts/BreakdownListRow")]
public class BreakdownListRow : KMonoBehaviour
{
	// Token: 0x0600764A RID: 30282 RVA: 0x002D4080 File Offset: 0x002D2280
	public void ShowData(string name, string value)
	{
		base.gameObject.transform.localScale = Vector3.one;
		this.nameLabel.text = name;
		this.valueLabel.text = value;
		this.dotOutlineImage.gameObject.SetActive(true);
		Vector2 vector = Vector2.one * 0.6f;
		this.dotOutlineImage.rectTransform.localScale.Set(vector.x, vector.y, 1f);
		this.dotInsideImage.gameObject.SetActive(true);
		this.dotInsideImage.color = BreakdownListRow.statusColour[0];
		this.iconImage.gameObject.SetActive(false);
		this.checkmarkImage.gameObject.SetActive(false);
		this.SetHighlighted(false);
		this.SetImportant(false);
	}

	// Token: 0x0600764B RID: 30283 RVA: 0x002D415C File Offset: 0x002D235C
	public void ShowStatusData(string name, string value, BreakdownListRow.Status dotColor)
	{
		this.ShowData(name, value);
		this.dotOutlineImage.gameObject.SetActive(true);
		this.dotInsideImage.gameObject.SetActive(true);
		this.iconImage.gameObject.SetActive(false);
		this.checkmarkImage.gameObject.SetActive(false);
		this.SetStatusColor(dotColor);
	}

	// Token: 0x0600764C RID: 30284 RVA: 0x002D41BC File Offset: 0x002D23BC
	public void SetStatusColor(BreakdownListRow.Status dotColor)
	{
		this.checkmarkImage.gameObject.SetActive(dotColor > BreakdownListRow.Status.Default);
		this.checkmarkImage.color = BreakdownListRow.statusColour[(int)dotColor];
		switch (dotColor)
		{
		case BreakdownListRow.Status.Red:
			this.checkmarkImage.sprite = this.statusFailureIcon;
			return;
		case BreakdownListRow.Status.Green:
			this.checkmarkImage.sprite = this.statusSuccessIcon;
			return;
		case BreakdownListRow.Status.Yellow:
			this.checkmarkImage.sprite = this.statusWarningIcon;
			return;
		default:
			return;
		}
	}

	// Token: 0x0600764D RID: 30285 RVA: 0x002D4240 File Offset: 0x002D2440
	public void ShowCheckmarkData(string name, string value, BreakdownListRow.Status status)
	{
		this.ShowData(name, value);
		this.dotOutlineImage.gameObject.SetActive(true);
		this.dotOutlineImage.rectTransform.localScale = Vector3.one;
		this.dotInsideImage.gameObject.SetActive(true);
		this.iconImage.gameObject.SetActive(false);
		this.SetStatusColor(status);
	}

	// Token: 0x0600764E RID: 30286 RVA: 0x002D42A4 File Offset: 0x002D24A4
	public void ShowIconData(string name, string value, Sprite sprite)
	{
		this.ShowData(name, value);
		this.dotOutlineImage.gameObject.SetActive(false);
		this.dotInsideImage.gameObject.SetActive(false);
		this.iconImage.gameObject.SetActive(true);
		this.checkmarkImage.gameObject.SetActive(false);
		this.iconImage.sprite = sprite;
		this.iconImage.color = Color.white;
	}

	// Token: 0x0600764F RID: 30287 RVA: 0x002D4319 File Offset: 0x002D2519
	public void ShowIconData(string name, string value, Sprite sprite, Color spriteColor)
	{
		this.ShowIconData(name, value, sprite);
		this.iconImage.color = spriteColor;
	}

	// Token: 0x06007650 RID: 30288 RVA: 0x002D4334 File Offset: 0x002D2534
	public void SetHighlighted(bool highlighted)
	{
		this.isHighlighted = highlighted;
		Vector2 vector = Vector2.one * 0.8f;
		this.dotOutlineImage.rectTransform.localScale.Set(vector.x, vector.y, 1f);
		this.nameLabel.alpha = (this.isHighlighted ? 0.9f : 0.5f);
		this.valueLabel.alpha = (this.isHighlighted ? 0.9f : 0.5f);
	}

	// Token: 0x06007651 RID: 30289 RVA: 0x002D43C0 File Offset: 0x002D25C0
	public void SetDisabled(bool disabled)
	{
		this.isDisabled = disabled;
		this.nameLabel.alpha = (this.isDisabled ? 0.4f : 0.5f);
		this.valueLabel.alpha = (this.isDisabled ? 0.4f : 0.5f);
	}

	// Token: 0x06007652 RID: 30290 RVA: 0x002D4414 File Offset: 0x002D2614
	public void SetImportant(bool important)
	{
		this.isImportant = important;
		this.dotOutlineImage.rectTransform.localScale = Vector3.one;
		this.nameLabel.alpha = (this.isImportant ? 1f : 0.5f);
		this.valueLabel.alpha = (this.isImportant ? 1f : 0.5f);
		this.nameLabel.fontStyle = (this.isImportant ? FontStyles.Bold : FontStyles.Normal);
		this.valueLabel.fontStyle = (this.isImportant ? FontStyles.Bold : FontStyles.Normal);
	}

	// Token: 0x06007653 RID: 30291 RVA: 0x002D44AC File Offset: 0x002D26AC
	public void HideIcon()
	{
		this.dotOutlineImage.gameObject.SetActive(false);
		this.dotInsideImage.gameObject.SetActive(false);
		this.iconImage.gameObject.SetActive(false);
		this.checkmarkImage.gameObject.SetActive(false);
	}

	// Token: 0x06007654 RID: 30292 RVA: 0x002D44FD File Offset: 0x002D26FD
	public void AddTooltip(string tooltipText)
	{
		if (this.tooltip == null)
		{
			this.tooltip = base.gameObject.AddComponent<ToolTip>();
		}
		this.tooltip.SetSimpleTooltip(tooltipText);
	}

	// Token: 0x06007655 RID: 30293 RVA: 0x002D452A File Offset: 0x002D272A
	public void ClearTooltip()
	{
		if (this.tooltip != null)
		{
			this.tooltip.ClearMultiStringTooltip();
		}
	}

	// Token: 0x06007656 RID: 30294 RVA: 0x002D4545 File Offset: 0x002D2745
	public void SetValue(string value)
	{
		this.valueLabel.text = value;
	}

	// Token: 0x0400521E RID: 21022
	private static Color[] statusColour = new Color[]
	{
		new Color(0.34117648f, 0.36862746f, 0.45882353f, 1f),
		new Color(0.72156864f, 0.38431373f, 0f, 1f),
		new Color(0.38431373f, 0.72156864f, 0f, 1f),
		new Color(0.72156864f, 0.72156864f, 0f, 1f)
	};

	// Token: 0x0400521F RID: 21023
	public Image dotOutlineImage;

	// Token: 0x04005220 RID: 21024
	public Image dotInsideImage;

	// Token: 0x04005221 RID: 21025
	public Image iconImage;

	// Token: 0x04005222 RID: 21026
	public Image checkmarkImage;

	// Token: 0x04005223 RID: 21027
	public LocText nameLabel;

	// Token: 0x04005224 RID: 21028
	public LocText valueLabel;

	// Token: 0x04005225 RID: 21029
	private bool isHighlighted;

	// Token: 0x04005226 RID: 21030
	private bool isDisabled;

	// Token: 0x04005227 RID: 21031
	private bool isImportant;

	// Token: 0x04005228 RID: 21032
	private ToolTip tooltip;

	// Token: 0x04005229 RID: 21033
	[SerializeField]
	private Sprite statusSuccessIcon;

	// Token: 0x0400522A RID: 21034
	[SerializeField]
	private Sprite statusWarningIcon;

	// Token: 0x0400522B RID: 21035
	[SerializeField]
	private Sprite statusFailureIcon;

	// Token: 0x0200206F RID: 8303
	public enum Status
	{
		// Token: 0x04009431 RID: 37937
		Default,
		// Token: 0x04009432 RID: 37938
		Red,
		// Token: 0x04009433 RID: 37939
		Green,
		// Token: 0x04009434 RID: 37940
		Yellow
	}
}
