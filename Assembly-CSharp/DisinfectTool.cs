using System;
using UnityEngine;

// Token: 0x02000972 RID: 2418
public class DisinfectTool : DragTool
{
	// Token: 0x060045B5 RID: 17845 RVA: 0x001917BB File Offset: 0x0018F9BB
	public static void DestroyInstance()
	{
		DisinfectTool.Instance = null;
	}

	// Token: 0x060045B6 RID: 17846 RVA: 0x001917C3 File Offset: 0x0018F9C3
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		DisinfectTool.Instance = this;
		this.interceptNumberKeysForPriority = true;
		this.viewMode = OverlayModes.Disease.ID;
	}

	// Token: 0x060045B7 RID: 17847 RVA: 0x001917E3 File Offset: 0x0018F9E3
	public void Activate()
	{
		PlayerController.Instance.ActivateTool(this);
	}

	// Token: 0x060045B8 RID: 17848 RVA: 0x001917F0 File Offset: 0x0018F9F0
	protected override void OnDragTool(int cell, int distFromOrigin)
	{
		for (int i = 0; i < 45; i++)
		{
			GameObject gameObject = Grid.Objects[cell, i];
			if (gameObject != null)
			{
				Disinfectable component = gameObject.GetComponent<Disinfectable>();
				if (component != null && component.GetComponent<PrimaryElement>().DiseaseCount > 0)
				{
					component.MarkForDisinfect(false);
				}
			}
		}
	}

	// Token: 0x04002E6C RID: 11884
	public static DisinfectTool Instance;
}
