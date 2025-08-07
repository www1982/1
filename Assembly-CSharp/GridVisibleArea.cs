using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000949 RID: 2377
public class GridVisibleArea
{
	// Token: 0x170004CF RID: 1231
	// (get) Token: 0x06004405 RID: 17413 RVA: 0x00187B54 File Offset: 0x00185D54
	public GridArea CurrentArea
	{
		get
		{
			return this.VisibleAreas[0];
		}
	}

	// Token: 0x170004D0 RID: 1232
	// (get) Token: 0x06004406 RID: 17414 RVA: 0x00187B62 File Offset: 0x00185D62
	public GridArea PreviousArea
	{
		get
		{
			return this.VisibleAreas[1];
		}
	}

	// Token: 0x170004D1 RID: 1233
	// (get) Token: 0x06004407 RID: 17415 RVA: 0x00187B70 File Offset: 0x00185D70
	public GridArea PreviousPreviousArea
	{
		get
		{
			return this.VisibleAreas[2];
		}
	}

	// Token: 0x170004D2 RID: 1234
	// (get) Token: 0x06004408 RID: 17416 RVA: 0x00187B7E File Offset: 0x00185D7E
	public GridArea CurrentAreaExtended
	{
		get
		{
			return this.VisibleAreasExtended[0];
		}
	}

	// Token: 0x170004D3 RID: 1235
	// (get) Token: 0x06004409 RID: 17417 RVA: 0x00187B8C File Offset: 0x00185D8C
	public GridArea PreviousAreaExtended
	{
		get
		{
			return this.VisibleAreasExtended[1];
		}
	}

	// Token: 0x170004D4 RID: 1236
	// (get) Token: 0x0600440A RID: 17418 RVA: 0x00187B9A File Offset: 0x00185D9A
	public GridArea PreviousPreviousAreaExtended
	{
		get
		{
			return this.VisibleAreasExtended[2];
		}
	}

	// Token: 0x0600440B RID: 17419 RVA: 0x00187BA8 File Offset: 0x00185DA8
	public GridVisibleArea()
	{
	}

	// Token: 0x0600440C RID: 17420 RVA: 0x00187BD3 File Offset: 0x00185DD3
	public GridVisibleArea(int padding)
	{
		this._padding = padding;
	}

	// Token: 0x0600440D RID: 17421 RVA: 0x00187C08 File Offset: 0x00185E08
	public void Update()
	{
		if (!this.debugFreezeVisibleArea)
		{
			this.VisibleAreas[2] = this.VisibleAreas[1];
			this.VisibleAreas[1] = this.VisibleAreas[0];
			this.VisibleAreas[0] = GridVisibleArea.GetVisibleArea();
		}
		if (!this.debugFreezeVisibleAreasExtended)
		{
			this.VisibleAreasExtended[2] = this.VisibleAreasExtended[1];
			this.VisibleAreasExtended[1] = this.VisibleAreasExtended[0];
			this.VisibleAreasExtended[0] = GridVisibleArea.GetVisibleAreaExtended(this._padding);
		}
		foreach (GridVisibleArea.Callback callback in this.Callbacks)
		{
			callback.OnUpdate();
		}
	}

	// Token: 0x0600440E RID: 17422 RVA: 0x00187CF8 File Offset: 0x00185EF8
	public void AddCallback(string name, global::System.Action on_update)
	{
		GridVisibleArea.Callback callback = new GridVisibleArea.Callback
		{
			Name = name,
			OnUpdate = on_update
		};
		this.Callbacks.Add(callback);
	}

	// Token: 0x0600440F RID: 17423 RVA: 0x00187D2C File Offset: 0x00185F2C
	public void Run(Action<int> in_view)
	{
		if (in_view != null)
		{
			this.CurrentArea.Run(in_view);
		}
	}

	// Token: 0x06004410 RID: 17424 RVA: 0x00187D4C File Offset: 0x00185F4C
	public void RunExtended(Action<int> in_view)
	{
		if (in_view != null)
		{
			this.CurrentAreaExtended.Run(in_view);
		}
	}

	// Token: 0x06004411 RID: 17425 RVA: 0x00187D6C File Offset: 0x00185F6C
	public void Run(Action<int> outside_view, Action<int> inside_view, Action<int> inside_view_second_time)
	{
		if (outside_view != null)
		{
			this.PreviousArea.RunOnDifference(this.CurrentArea, outside_view);
		}
		if (inside_view != null)
		{
			this.CurrentArea.RunOnDifference(this.PreviousArea, inside_view);
		}
		if (inside_view_second_time != null)
		{
			this.PreviousArea.RunOnDifference(this.PreviousPreviousArea, inside_view_second_time);
		}
	}

	// Token: 0x06004412 RID: 17426 RVA: 0x00187DC4 File Offset: 0x00185FC4
	public void RunExtended(Action<int> outside_view, Action<int> inside_view, Action<int> inside_view_second_time)
	{
		if (outside_view != null)
		{
			this.PreviousAreaExtended.RunOnDifference(this.CurrentAreaExtended, outside_view);
		}
		if (inside_view != null)
		{
			this.CurrentAreaExtended.RunOnDifference(this.PreviousAreaExtended, inside_view);
		}
		if (inside_view_second_time != null)
		{
			this.PreviousAreaExtended.RunOnDifference(this.PreviousPreviousAreaExtended, inside_view_second_time);
		}
	}

	// Token: 0x06004413 RID: 17427 RVA: 0x00187E1C File Offset: 0x0018601C
	public void RunIfVisible(int cell, Action<int> action)
	{
		this.CurrentArea.RunIfInside(cell, action);
	}

	// Token: 0x06004414 RID: 17428 RVA: 0x00187E3C File Offset: 0x0018603C
	public void RunIfVisibleExtended(int cell, Action<int> action)
	{
		this.CurrentAreaExtended.RunIfInside(cell, action);
	}

	// Token: 0x06004415 RID: 17429 RVA: 0x00187E59 File Offset: 0x00186059
	public static GridArea GetVisibleArea()
	{
		return GridVisibleArea.GetVisibleAreaExtended(0);
	}

	// Token: 0x06004416 RID: 17430 RVA: 0x00187E64 File Offset: 0x00186064
	public static GridArea GetVisibleAreaExtended(int padding)
	{
		GridArea gridArea = default(GridArea);
		Camera mainCamera = Game.MainCamera;
		if (mainCamera != null)
		{
			Vector3 vector = mainCamera.ViewportToWorldPoint(new Vector3(1f, 1f, mainCamera.transform.GetPosition().z));
			Vector3 vector2 = mainCamera.ViewportToWorldPoint(new Vector3(0f, 0f, mainCamera.transform.GetPosition().z));
			vector.x += (float)padding;
			vector.y += (float)padding;
			vector2.x -= (float)padding;
			vector2.y -= (float)padding;
			if (CameraController.Instance != null)
			{
				Vector2I vector2I;
				Vector2I vector2I2;
				CameraController.Instance.GetWorldCamera(out vector2I, out vector2I2);
				gridArea.SetExtents(Math.Max((int)(vector2.x - 0.5f), vector2I.x), Math.Max((int)(vector2.y - 0.5f), vector2I.y), Math.Min((int)(vector.x + 1.5f), vector2I2.x + vector2I.x), Math.Min((int)(vector.y + 1.5f), vector2I2.y + vector2I.y));
			}
			else
			{
				gridArea.SetExtents(Math.Max((int)(vector2.x - 0.5f), 0), Math.Max((int)(vector2.y - 0.5f), 0), Math.Min((int)(vector.x + 1.5f), Grid.WidthInCells), Math.Min((int)(vector.y + 1.5f), Grid.HeightInCells));
			}
		}
		return gridArea;
	}

	// Token: 0x04002D94 RID: 11668
	private GridArea[] VisibleAreas = new GridArea[3];

	// Token: 0x04002D95 RID: 11669
	private GridArea[] VisibleAreasExtended = new GridArea[3];

	// Token: 0x04002D96 RID: 11670
	private List<GridVisibleArea.Callback> Callbacks = new List<GridVisibleArea.Callback>();

	// Token: 0x04002D97 RID: 11671
	public bool debugFreezeVisibleArea;

	// Token: 0x04002D98 RID: 11672
	public bool debugFreezeVisibleAreasExtended;

	// Token: 0x04002D99 RID: 11673
	private readonly int _padding;

	// Token: 0x0200195C RID: 6492
	public struct Callback
	{
		// Token: 0x04007BDB RID: 31707
		public global::System.Action OnUpdate;

		// Token: 0x04007BDC RID: 31708
		public string Name;
	}
}
