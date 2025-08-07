using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

// Token: 0x0200056D RID: 1389
public class AnimEventHandlerManager : KMonoBehaviour
{
	// Token: 0x1700011D RID: 285
	// (get) Token: 0x06001EFD RID: 7933 RVA: 0x000B1C4D File Offset: 0x000AFE4D
	// (set) Token: 0x06001EFE RID: 7934 RVA: 0x000B1C54 File Offset: 0x000AFE54
	public static AnimEventHandlerManager Instance { get; private set; }

	// Token: 0x06001EFF RID: 7935 RVA: 0x000B1C5C File Offset: 0x000AFE5C
	public static void DestroyInstance()
	{
		AnimEventHandlerManager.Instance = null;
	}

	// Token: 0x06001F00 RID: 7936 RVA: 0x000B1C64 File Offset: 0x000AFE64
	protected override void OnPrefabInit()
	{
		AnimEventHandlerManager.Instance = this;
		this.handlers = new List<AnimEventHandler>();
	}

	// Token: 0x06001F01 RID: 7937 RVA: 0x000B1C77 File Offset: 0x000AFE77
	public void Add(AnimEventHandler handler)
	{
		this.handlers.Add(handler);
	}

	// Token: 0x06001F02 RID: 7938 RVA: 0x000B1C85 File Offset: 0x000AFE85
	public void Remove(AnimEventHandler handler)
	{
		this.handlers.Remove(handler);
	}

	// Token: 0x06001F03 RID: 7939 RVA: 0x000B1C94 File Offset: 0x000AFE94
	private bool IsVisibleToZoom()
	{
		return !(Game.MainCamera == null) && Game.MainCamera.orthographicSize < 40f;
	}

	// Token: 0x06001F04 RID: 7940 RVA: 0x000B1CB8 File Offset: 0x000AFEB8
	public void LateUpdate()
	{
		if (!this.IsVisibleToZoom())
		{
			return;
		}
		AnimEventHandlerManager.<>c__DisplayClass11_0 CS$<>8__locals1;
		Grid.GetVisibleCellRangeInActiveWorld(out CS$<>8__locals1.min, out CS$<>8__locals1.max, 4, 1.5f);
		foreach (AnimEventHandler animEventHandler in this.handlers)
		{
			if (AnimEventHandlerManager.<LateUpdate>g__IsVisible|11_0(animEventHandler, ref CS$<>8__locals1))
			{
				animEventHandler.UpdateOffset();
			}
		}
	}

	// Token: 0x06001F06 RID: 7942 RVA: 0x000B1D40 File Offset: 0x000AFF40
	[CompilerGenerated]
	internal static bool <LateUpdate>g__IsVisible|11_0(AnimEventHandler handler, ref AnimEventHandlerManager.<>c__DisplayClass11_0 A_1)
	{
		int num;
		int num2;
		Grid.CellToXY(handler.GetCachedCell(), out num, out num2);
		return num >= A_1.min.x && num2 >= A_1.min.y && num < A_1.max.x && num2 < A_1.max.y;
	}

	// Token: 0x04001201 RID: 4609
	private const float HIDE_DISTANCE = 40f;

	// Token: 0x04001203 RID: 4611
	private List<AnimEventHandler> handlers;
}
