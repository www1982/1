using System;
using UnityEngine;

// Token: 0x02000DA3 RID: 3491
public class QuickLayout : KMonoBehaviour
{
	// Token: 0x06006D5F RID: 27999 RVA: 0x0029662C File Offset: 0x0029482C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.ForceUpdate();
	}

	// Token: 0x06006D60 RID: 28000 RVA: 0x0029663A File Offset: 0x0029483A
	private void OnEnable()
	{
		this.ForceUpdate();
	}

	// Token: 0x06006D61 RID: 28001 RVA: 0x00296642 File Offset: 0x00294842
	private void LateUpdate()
	{
		this.Run(false);
	}

	// Token: 0x06006D62 RID: 28002 RVA: 0x0029664B File Offset: 0x0029484B
	public void ForceUpdate()
	{
		this.Run(true);
	}

	// Token: 0x06006D63 RID: 28003 RVA: 0x00296654 File Offset: 0x00294854
	private void Run(bool forceUpdate = false)
	{
		forceUpdate = forceUpdate || this._elementSize != this.elementSize;
		forceUpdate = forceUpdate || this._spacing != this.spacing;
		forceUpdate = forceUpdate || this._layoutDirection != this.layoutDirection;
		forceUpdate = forceUpdate || this._offset != this.offset;
		if (forceUpdate)
		{
			this._elementSize = this.elementSize;
			this._spacing = this.spacing;
			this._layoutDirection = this.layoutDirection;
			this._offset = this.offset;
		}
		int num = 0;
		for (int i = 0; i < base.transform.childCount; i++)
		{
			if (base.transform.GetChild(i).gameObject.activeInHierarchy)
			{
				num++;
			}
		}
		if (num != this.oldActiveChildCount || forceUpdate)
		{
			this.Layout();
			this.oldActiveChildCount = num;
		}
	}

	// Token: 0x06006D64 RID: 28004 RVA: 0x0029674C File Offset: 0x0029494C
	public void Layout()
	{
		Vector3 vector = this._offset;
		bool flag = false;
		for (int i = 0; i < base.transform.childCount; i++)
		{
			if (base.transform.GetChild(i).gameObject.activeInHierarchy)
			{
				flag = true;
				base.transform.GetChild(i).rectTransform().anchoredPosition = vector;
				vector += (float)(this._elementSize + this._spacing) * this.GetDirectionVector();
			}
		}
		if (this.driveParentRectSize != null)
		{
			if (!flag)
			{
				if (this._layoutDirection == QuickLayout.LayoutDirection.BottomToTop || this._layoutDirection == QuickLayout.LayoutDirection.TopToBottom)
				{
					this.driveParentRectSize.sizeDelta = new Vector2(Mathf.Abs(this.driveParentRectSize.sizeDelta.x), 0f);
					return;
				}
				if (this._layoutDirection == QuickLayout.LayoutDirection.LeftToRight || this._layoutDirection == QuickLayout.LayoutDirection.LeftToRight)
				{
					this.driveParentRectSize.sizeDelta = new Vector2(0f, Mathf.Abs(this.driveParentRectSize.sizeDelta.y));
					return;
				}
			}
			else
			{
				if (this._layoutDirection == QuickLayout.LayoutDirection.BottomToTop || this._layoutDirection == QuickLayout.LayoutDirection.TopToBottom)
				{
					this.driveParentRectSize.sizeDelta = new Vector2(this.driveParentRectSize.sizeDelta.x, Mathf.Abs(vector.y));
					return;
				}
				if (this._layoutDirection == QuickLayout.LayoutDirection.LeftToRight || this._layoutDirection == QuickLayout.LayoutDirection.LeftToRight)
				{
					this.driveParentRectSize.sizeDelta = new Vector2(Mathf.Abs(vector.x), this.driveParentRectSize.sizeDelta.y);
				}
			}
		}
	}

	// Token: 0x06006D65 RID: 28005 RVA: 0x002968E4 File Offset: 0x00294AE4
	private Vector2 GetDirectionVector()
	{
		Vector2 vector = Vector3.zero;
		switch (this._layoutDirection)
		{
		case QuickLayout.LayoutDirection.TopToBottom:
			vector = Vector2.down;
			break;
		case QuickLayout.LayoutDirection.BottomToTop:
			vector = Vector2.up;
			break;
		case QuickLayout.LayoutDirection.LeftToRight:
			vector = Vector2.right;
			break;
		case QuickLayout.LayoutDirection.RightToLeft:
			vector = Vector2.left;
			break;
		}
		return vector;
	}

	// Token: 0x04004AAF RID: 19119
	[Header("Configuration")]
	[SerializeField]
	private int elementSize;

	// Token: 0x04004AB0 RID: 19120
	[SerializeField]
	private int spacing;

	// Token: 0x04004AB1 RID: 19121
	[SerializeField]
	private QuickLayout.LayoutDirection layoutDirection;

	// Token: 0x04004AB2 RID: 19122
	[SerializeField]
	private Vector2 offset;

	// Token: 0x04004AB3 RID: 19123
	[SerializeField]
	private RectTransform driveParentRectSize;

	// Token: 0x04004AB4 RID: 19124
	private int _elementSize;

	// Token: 0x04004AB5 RID: 19125
	private int _spacing;

	// Token: 0x04004AB6 RID: 19126
	private QuickLayout.LayoutDirection _layoutDirection;

	// Token: 0x04004AB7 RID: 19127
	private Vector2 _offset;

	// Token: 0x04004AB8 RID: 19128
	private int oldActiveChildCount;

	// Token: 0x02001FA4 RID: 8100
	private enum LayoutDirection
	{
		// Token: 0x040091A2 RID: 37282
		TopToBottom,
		// Token: 0x040091A3 RID: 37283
		BottomToTop,
		// Token: 0x040091A4 RID: 37284
		LeftToRight,
		// Token: 0x040091A5 RID: 37285
		RightToLeft
	}
}
