using System;
using TemplateClasses;
using UnityEngine;

// Token: 0x02000991 RID: 2449
public class StampToolPreview_Area : IStampToolPreviewPlugin
{
	// Token: 0x06004711 RID: 18193 RVA: 0x00198978 File Offset: 0x00196B78
	public void Setup(StampToolPreviewContext context)
	{
		if (StampToolPreview_Area.material == null)
		{
			StampToolPreview_Area.material = StampToolPreviewUtil.MakeMaterial(Assets.GetTexture("stamptool_vis_background"));
			StampToolPreview_Area.material.name = "Area (" + StampToolPreview_Area.material.name + ")";
		}
		context.onErrorChangeFn = (Action<string>)Delegate.Combine(context.onErrorChangeFn, new Action<string>(delegate(string error)
		{
			Color color = ((error != null) ? StampToolPreviewUtil.COLOR_ERROR : StampToolPreviewUtil.COLOR_OK);
			color.a = 1f;
			StampToolPreview_Area.material.color = color;
		}));
		for (int i = 0; i < context.stampTemplate.cells.Count; i++)
		{
			Cell cell = context.stampTemplate.cells[i];
			MeshRenderer meshRenderer;
			GameObject gameObject;
			StampToolPreviewUtil.MakeQuad(out gameObject, out meshRenderer, 1f, null);
			gameObject.name = "AreaPlacer";
			gameObject.transform.SetParent(context.previewParent, false);
			gameObject.transform.localPosition = new Vector3((float)cell.location_x, (float)cell.location_y + Grid.HalfCellSizeInMeters);
			context.cleanupFn = (global::System.Action)Delegate.Combine(context.cleanupFn, new global::System.Action(delegate
			{
				if (!gameObject.IsNullOrDestroyed())
				{
					global::UnityEngine.Object.Destroy(gameObject);
				}
			}));
			meshRenderer.sharedMaterial = StampToolPreview_Area.material;
		}
	}

	// Token: 0x04002F08 RID: 12040
	public static Material material;
}
