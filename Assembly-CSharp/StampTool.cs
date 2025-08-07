using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200098C RID: 2444
public class StampTool : InterfaceTool
{
	// Token: 0x060046FB RID: 18171 RVA: 0x0019808C File Offset: 0x0019628C
	public static void DestroyInstance()
	{
		StampTool.Instance = null;
	}

	// Token: 0x060046FC RID: 18172 RVA: 0x00198094 File Offset: 0x00196294
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		StampTool.Instance = this;
		this.preview = new StampToolPreview(this, new IStampToolPreviewPlugin[]
		{
			new StampToolPreview_Placers(this.PlacerPrefab),
			new StampToolPreview_Area(),
			new StampToolPreview_SolidLiquidGas(),
			new StampToolPreview_Prefabs()
		});
	}

	// Token: 0x060046FD RID: 18173 RVA: 0x001980E5 File Offset: 0x001962E5
	private void Update()
	{
		this.preview.Refresh(Grid.PosToCell(this.GetCursorPos()));
	}

	// Token: 0x060046FE RID: 18174 RVA: 0x00198100 File Offset: 0x00196300
	public void Activate(TemplateContainer template, bool SelectAffected = false, bool DeactivateOnStamp = false)
	{
		this.selectAffected = SelectAffected;
		this.deactivateOnStamp = DeactivateOnStamp;
		if (this.stampTemplate == template || template == null || template.cells == null)
		{
			return;
		}
		this.stampTemplate = template;
		PlayerController.Instance.ActivateTool(this);
		base.StartCoroutine(this.preview.Setup(template));
	}

	// Token: 0x060046FF RID: 18175 RVA: 0x00198155 File Offset: 0x00196355
	private Vector3 GetCursorPos()
	{
		return PlayerController.GetCursorPos(KInputManager.GetMousePos());
	}

	// Token: 0x06004700 RID: 18176 RVA: 0x00198161 File Offset: 0x00196361
	public override void OnLeftClickDown(Vector3 cursor_pos)
	{
		base.OnLeftClickDown(cursor_pos);
		this.Stamp(cursor_pos);
	}

	// Token: 0x06004701 RID: 18177 RVA: 0x00198178 File Offset: 0x00196378
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.BuildMenuKeyQ))
		{
			Vector3 cursorPos = this.GetCursorPos();
			DebugBaseTemplateButton.Instance.ClearSelection();
			if (this.stampTemplate.cells != null)
			{
				for (int i = 0; i < this.stampTemplate.cells.Count; i++)
				{
					DebugBaseTemplateButton.Instance.AddToSelection(Grid.XYToCell((int)(cursorPos.x + (float)this.stampTemplate.cells[i].location_x), (int)(cursorPos.y + (float)this.stampTemplate.cells[i].location_y)));
				}
			}
		}
		base.OnKeyDown(e);
	}

	// Token: 0x06004702 RID: 18178 RVA: 0x00198220 File Offset: 0x00196420
	private void Stamp(Vector2 pos)
	{
		if (!this.ready)
		{
			return;
		}
		int num = Grid.OffsetCell(Grid.PosToCell(pos), Mathf.FloorToInt(-this.stampTemplate.info.size.X / 2f), 0);
		int num2 = Grid.OffsetCell(Grid.PosToCell(pos), Mathf.FloorToInt(this.stampTemplate.info.size.X / 2f), 0);
		int num3 = Grid.OffsetCell(Grid.PosToCell(pos), 0, 1 + Mathf.FloorToInt(-this.stampTemplate.info.size.Y / 2f));
		int num4 = Grid.OffsetCell(Grid.PosToCell(pos), 0, 1 + Mathf.FloorToInt(this.stampTemplate.info.size.Y / 2f));
		if (!Grid.IsValidBuildingCell(num) || !Grid.IsValidBuildingCell(num2) || !Grid.IsValidBuildingCell(num4) || !Grid.IsValidBuildingCell(num3))
		{
			return;
		}
		this.ready = false;
		bool pauseOnComplete = SpeedControlScreen.Instance.IsPaused;
		if (SpeedControlScreen.Instance.IsPaused)
		{
			SpeedControlScreen.Instance.Unpause(true);
		}
		if (this.stampTemplate.cells != null)
		{
			this.preview.OnPlace();
			List<GameObject> list = new List<GameObject>();
			for (int i = 0; i < this.stampTemplate.cells.Count; i++)
			{
				for (int j = 0; j < 34; j++)
				{
					GameObject gameObject = Grid.Objects[Grid.XYToCell((int)(pos.x + (float)this.stampTemplate.cells[i].location_x), (int)(pos.y + (float)this.stampTemplate.cells[i].location_y)), j];
					if (gameObject != null && !list.Contains(gameObject))
					{
						list.Add(gameObject);
					}
				}
			}
			foreach (GameObject gameObject2 in list)
			{
				if (gameObject2 != null)
				{
					Util.KDestroyGameObject(gameObject2);
				}
			}
		}
		TemplateLoader.Stamp(this.stampTemplate, pos, delegate
		{
			this.CompleteStamp(pauseOnComplete);
		});
		if (this.selectAffected)
		{
			DebugBaseTemplateButton.Instance.ClearSelection();
			if (this.stampTemplate.cells != null)
			{
				for (int k = 0; k < this.stampTemplate.cells.Count; k++)
				{
					DebugBaseTemplateButton.Instance.AddToSelection(Grid.XYToCell((int)(pos.x + (float)this.stampTemplate.cells[k].location_x), (int)(pos.y + (float)this.stampTemplate.cells[k].location_y)));
				}
			}
		}
		if (this.deactivateOnStamp)
		{
			base.DeactivateTool(null);
		}
	}

	// Token: 0x06004703 RID: 18179 RVA: 0x00198528 File Offset: 0x00196728
	private void CompleteStamp(bool pause)
	{
		if (pause)
		{
			SpeedControlScreen.Instance.Pause(true, false);
		}
		this.ready = true;
		this.OnDeactivateTool(null);
	}

	// Token: 0x06004704 RID: 18180 RVA: 0x00198547 File Offset: 0x00196747
	protected override void OnDeactivateTool(InterfaceTool new_tool)
	{
		base.OnDeactivateTool(new_tool);
		if (base.gameObject.activeSelf)
		{
			return;
		}
		this.preview.Cleanup();
		this.stampTemplate = null;
	}

	// Token: 0x04002EF0 RID: 12016
	public static StampTool Instance;

	// Token: 0x04002EF1 RID: 12017
	private StampToolPreview preview;

	// Token: 0x04002EF2 RID: 12018
	public TemplateContainer stampTemplate;

	// Token: 0x04002EF3 RID: 12019
	public GameObject PlacerPrefab;

	// Token: 0x04002EF4 RID: 12020
	private bool ready = true;

	// Token: 0x04002EF5 RID: 12021
	private bool selectAffected;

	// Token: 0x04002EF6 RID: 12022
	private bool deactivateOnStamp;
}
