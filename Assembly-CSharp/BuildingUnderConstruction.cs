using System;
using UnityEngine;

// Token: 0x020006D2 RID: 1746
public class BuildingUnderConstruction : Building
{
	// Token: 0x06002B12 RID: 11026 RVA: 0x000F8EA0 File Offset: 0x000F70A0
	protected override void OnPrefabInit()
	{
		Vector3 position = base.transform.GetPosition();
		position.z = Grid.GetLayerZ(this.Def.SceneLayer);
		base.transform.SetPosition(position);
		base.gameObject.SetLayerRecursively(LayerMask.NameToLayer("Construction"));
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		Rotatable component2 = base.GetComponent<Rotatable>();
		if (component != null && component2 == null)
		{
			component.Offset = this.Def.GetVisualizerOffset();
		}
		KBoxCollider2D component3 = base.GetComponent<KBoxCollider2D>();
		if (component3 != null)
		{
			Vector3 visualizerOffset = this.Def.GetVisualizerOffset();
			component3.offset += new Vector2(visualizerOffset.x, visualizerOffset.y);
		}
		base.OnPrefabInit();
	}

	// Token: 0x06002B13 RID: 11027 RVA: 0x000F8F6C File Offset: 0x000F716C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.Def.IsTilePiece)
		{
			int num = Grid.PosToCell(base.transform.GetPosition());
			this.Def.RunOnArea(num, base.Orientation, delegate(int c)
			{
				TileVisualizer.RefreshCell(c, this.Def.TileLayer, this.Def.ReplacementLayer);
			});
		}
		base.RegisterBlockTileRenderer();
	}

	// Token: 0x06002B14 RID: 11028 RVA: 0x000F8FC1 File Offset: 0x000F71C1
	protected override void OnCleanUp()
	{
		base.UnregisterBlockTileRenderer();
		base.OnCleanUp();
	}

	// Token: 0x04001960 RID: 6496
	[MyCmpAdd]
	private KSelectable selectable;

	// Token: 0x04001961 RID: 6497
	[MyCmpAdd]
	private SaveLoadRoot saveLoadRoot;

	// Token: 0x04001962 RID: 6498
	[MyCmpAdd]
	private KPrefabID kPrefabID;
}
