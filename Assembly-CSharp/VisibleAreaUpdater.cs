using System;

// Token: 0x02000BDA RID: 3034
public class VisibleAreaUpdater
{
	// Token: 0x06005AF1 RID: 23281 RVA: 0x0020DC2E File Offset: 0x0020BE2E
	public VisibleAreaUpdater(Action<int> outside_view_first_time_cb, Action<int> inside_view_first_time_cb, string name)
	{
		this.OutsideViewFirstTimeCallback = outside_view_first_time_cb;
		this.InsideViewFirstTimeCallback = inside_view_first_time_cb;
		this.UpdateCallback = new Action<int>(this.InternalUpdateCell);
		this.Name = name;
	}

	// Token: 0x06005AF2 RID: 23282 RVA: 0x0020DC5D File Offset: 0x0020BE5D
	public void Update()
	{
		if (CameraController.Instance != null && this.VisibleArea == null)
		{
			this.VisibleArea = CameraController.Instance.VisibleArea;
			this.VisibleArea.Run(this.InsideViewFirstTimeCallback);
		}
	}

	// Token: 0x06005AF3 RID: 23283 RVA: 0x0020DC95 File Offset: 0x0020BE95
	private void InternalUpdateCell(int cell)
	{
		this.OutsideViewFirstTimeCallback(cell);
		this.InsideViewFirstTimeCallback(cell);
	}

	// Token: 0x06005AF4 RID: 23284 RVA: 0x0020DCAF File Offset: 0x0020BEAF
	public void UpdateCell(int cell)
	{
		if (this.VisibleArea != null)
		{
			this.VisibleArea.RunIfVisible(cell, this.UpdateCallback);
		}
	}

	// Token: 0x04003C55 RID: 15445
	private GridVisibleArea VisibleArea;

	// Token: 0x04003C56 RID: 15446
	private Action<int> OutsideViewFirstTimeCallback;

	// Token: 0x04003C57 RID: 15447
	private Action<int> InsideViewFirstTimeCallback;

	// Token: 0x04003C58 RID: 15448
	private Action<int> UpdateCallback;

	// Token: 0x04003C59 RID: 15449
	private string Name;
}
