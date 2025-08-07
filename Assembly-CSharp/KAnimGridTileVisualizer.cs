using System;
using Rendering;
using UnityEngine;

// Token: 0x020005C8 RID: 1480
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/KAnimGridTileVisualizer")]
public class KAnimGridTileVisualizer : KMonoBehaviour, IBlockTileInfo
{
	// Token: 0x0600221B RID: 8731 RVA: 0x000C483F File Offset: 0x000C2A3F
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.Subscribe<KAnimGridTileVisualizer>(-1503271301, KAnimGridTileVisualizer.OnSelectionChangedDelegate);
		base.Subscribe<KAnimGridTileVisualizer>(-1201923725, KAnimGridTileVisualizer.OnHighlightChangedDelegate);
	}

	// Token: 0x0600221C RID: 8732 RVA: 0x000C486C File Offset: 0x000C2A6C
	protected override void OnCleanUp()
	{
		Building component = base.GetComponent<Building>();
		if (component != null)
		{
			int num = Grid.PosToCell(base.transform.GetPosition());
			ObjectLayer tileLayer = component.Def.TileLayer;
			if (Grid.Objects[num, (int)tileLayer] == base.gameObject)
			{
				Grid.Objects[num, (int)tileLayer] = null;
			}
			TileVisualizer.RefreshCell(num, tileLayer, component.Def.ReplacementLayer);
		}
		base.OnCleanUp();
	}

	// Token: 0x0600221D RID: 8733 RVA: 0x000C48E4 File Offset: 0x000C2AE4
	private void OnSelectionChanged(object data)
	{
		bool flag = (bool)data;
		World.Instance.blockTileRenderer.SelectCell(Grid.PosToCell(base.transform.GetPosition()), flag);
	}

	// Token: 0x0600221E RID: 8734 RVA: 0x000C4918 File Offset: 0x000C2B18
	private void OnHighlightChanged(object data)
	{
		bool flag = (bool)data;
		World.Instance.blockTileRenderer.HighlightCell(Grid.PosToCell(base.transform.GetPosition()), flag);
	}

	// Token: 0x0600221F RID: 8735 RVA: 0x000C494C File Offset: 0x000C2B4C
	public int GetBlockTileConnectorID()
	{
		return this.blockTileConnectorID;
	}

	// Token: 0x040013E7 RID: 5095
	[SerializeField]
	public int blockTileConnectorID;

	// Token: 0x040013E8 RID: 5096
	private static readonly EventSystem.IntraObjectHandler<KAnimGridTileVisualizer> OnSelectionChangedDelegate = new EventSystem.IntraObjectHandler<KAnimGridTileVisualizer>(delegate(KAnimGridTileVisualizer component, object data)
	{
		component.OnSelectionChanged(data);
	});

	// Token: 0x040013E9 RID: 5097
	private static readonly EventSystem.IntraObjectHandler<KAnimGridTileVisualizer> OnHighlightChangedDelegate = new EventSystem.IntraObjectHandler<KAnimGridTileVisualizer>(delegate(KAnimGridTileVisualizer component, object data)
	{
		component.OnHighlightChanged(data);
	});
}
