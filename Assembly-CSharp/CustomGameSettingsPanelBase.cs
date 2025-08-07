using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000CAB RID: 3243
public abstract class CustomGameSettingsPanelBase : MonoBehaviour
{
	// Token: 0x060063BC RID: 25532 RVA: 0x0025728F File Offset: 0x0025548F
	public virtual void Init()
	{
	}

	// Token: 0x060063BD RID: 25533 RVA: 0x00257291 File Offset: 0x00255491
	public virtual void Uninit()
	{
	}

	// Token: 0x060063BE RID: 25534 RVA: 0x00257293 File Offset: 0x00255493
	private void OnEnable()
	{
		this.isDirty = true;
	}

	// Token: 0x060063BF RID: 25535 RVA: 0x0025729C File Offset: 0x0025549C
	private void Update()
	{
		if (this.isDirty)
		{
			this.isDirty = false;
			this.Refresh();
		}
	}

	// Token: 0x060063C0 RID: 25536 RVA: 0x002572B3 File Offset: 0x002554B3
	protected void AddWidget(CustomGameSettingWidget widget)
	{
		widget.onSettingChanged += this.OnWidgetChanged;
		this.widgets.Add(widget);
	}

	// Token: 0x060063C1 RID: 25537 RVA: 0x002572D3 File Offset: 0x002554D3
	private void OnWidgetChanged(CustomGameSettingWidget widget)
	{
		this.isDirty = true;
	}

	// Token: 0x060063C2 RID: 25538 RVA: 0x002572DC File Offset: 0x002554DC
	public virtual void Refresh()
	{
		foreach (CustomGameSettingWidget customGameSettingWidget in this.widgets)
		{
			customGameSettingWidget.Refresh();
		}
	}

	// Token: 0x040043DF RID: 17375
	protected List<CustomGameSettingWidget> widgets = new List<CustomGameSettingWidget>();

	// Token: 0x040043E0 RID: 17376
	private bool isDirty;
}
