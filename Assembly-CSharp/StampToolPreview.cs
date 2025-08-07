using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200098F RID: 2447
public class StampToolPreview
{
	// Token: 0x06004708 RID: 18184 RVA: 0x00198587 File Offset: 0x00196787
	public StampToolPreview(InterfaceTool tool, params IStampToolPreviewPlugin[] plugins)
	{
		this.context = new StampToolPreviewContext();
		this.context.previewParent = new GameObject("StampToolPreview::Preview").transform;
		this.context.tool = tool;
		this.plugins = plugins;
	}

	// Token: 0x06004709 RID: 18185 RVA: 0x001985C7 File Offset: 0x001967C7
	public IEnumerator Setup(TemplateContainer stampTemplate)
	{
		this.Cleanup();
		this.context.stampTemplate = stampTemplate;
		if (this.plugins != null)
		{
			IStampToolPreviewPlugin[] array = this.plugins;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Setup(this.context);
			}
		}
		yield return null;
		if (this.context.frameAfterSetupFn != null)
		{
			this.context.frameAfterSetupFn();
		}
		yield break;
	}

	// Token: 0x0600470A RID: 18186 RVA: 0x001985E0 File Offset: 0x001967E0
	public void Refresh(int originCell)
	{
		if (this.context.stampTemplate == null)
		{
			return;
		}
		if (originCell == this.prevOriginCell)
		{
			return;
		}
		this.prevOriginCell = originCell;
		if (!Grid.IsValidCell(originCell))
		{
			return;
		}
		if (this.context.refreshFn != null)
		{
			this.context.refreshFn(originCell);
		}
		this.context.previewParent.transform.SetPosition(Grid.CellToPosCBC(originCell, this.context.tool.visualizerLayer));
		this.context.previewParent.gameObject.SetActive(true);
	}

	// Token: 0x0600470B RID: 18187 RVA: 0x00198675 File Offset: 0x00196875
	public void OnErrorChange(string error)
	{
		if (this.context.onErrorChangeFn != null)
		{
			this.context.onErrorChangeFn(error);
		}
	}

	// Token: 0x0600470C RID: 18188 RVA: 0x00198695 File Offset: 0x00196895
	public void OnPlace()
	{
		if (this.context.onPlaceFn != null)
		{
			this.context.onPlaceFn();
		}
	}

	// Token: 0x0600470D RID: 18189 RVA: 0x001986B4 File Offset: 0x001968B4
	public void Cleanup()
	{
		if (this.context.cleanupFn != null)
		{
			this.context.cleanupFn();
		}
		this.prevOriginCell = Grid.InvalidCell;
		this.context.stampTemplate = null;
		this.context.frameAfterSetupFn = null;
		this.context.refreshFn = null;
		this.context.onPlaceFn = null;
		this.context.cleanupFn = null;
	}

	// Token: 0x04002EFF RID: 12031
	private IStampToolPreviewPlugin[] plugins;

	// Token: 0x04002F00 RID: 12032
	private StampToolPreviewContext context;

	// Token: 0x04002F01 RID: 12033
	private int prevOriginCell;
}
