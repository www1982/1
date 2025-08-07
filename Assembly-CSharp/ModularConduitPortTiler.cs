using System;
using UnityEngine;

// Token: 0x02000793 RID: 1939
public class ModularConduitPortTiler : KMonoBehaviour
{
	// Token: 0x0600332E RID: 13102 RVA: 0x00120534 File Offset: 0x0011E734
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.GetComponent<KPrefabID>().AddTag(GameTags.ModularConduitPort, true);
		if (this.tags == null || this.tags.Length == 0)
		{
			this.tags = new Tag[] { GameTags.ModularConduitPort };
		}
	}

	// Token: 0x0600332F RID: 13103 RVA: 0x00120584 File Offset: 0x0011E784
	protected override void OnSpawn()
	{
		OccupyArea component = base.GetComponent<OccupyArea>();
		if (component != null)
		{
			this.extents = component.GetExtents();
		}
		KBatchedAnimController component2 = base.GetComponent<KBatchedAnimController>();
		this.leftCapDefault = new KAnimSynchronizedController(component2, (Grid.SceneLayer)(component2.GetLayer() + this.leftCapDefaultSceneLayerAdjust), ModularConduitPortTiler.leftCapDefaultStr);
		if (this.manageLeftCap)
		{
			this.leftCapLaunchpad = new KAnimSynchronizedController(component2, (Grid.SceneLayer)component2.GetLayer(), ModularConduitPortTiler.leftCapLaunchpadStr);
			this.leftCapConduit = new KAnimSynchronizedController(component2, component2.GetLayer() + Grid.SceneLayer.Backwall, ModularConduitPortTiler.leftCapConduitStr);
		}
		this.rightCapDefault = new KAnimSynchronizedController(component2, (Grid.SceneLayer)(component2.GetLayer() + this.rightCapDefaultSceneLayerAdjust), ModularConduitPortTiler.rightCapDefaultStr);
		if (this.manageRightCap)
		{
			this.rightCapLaunchpad = new KAnimSynchronizedController(component2, (Grid.SceneLayer)component2.GetLayer(), ModularConduitPortTiler.rightCapLaunchpadStr);
			this.rightCapConduit = new KAnimSynchronizedController(component2, (Grid.SceneLayer)component2.GetLayer(), ModularConduitPortTiler.rightCapConduitStr);
		}
		Extents extents = new Extents(this.extents.x - 1, this.extents.y, this.extents.width + 2, this.extents.height);
		this.partitionerEntry = GameScenePartitioner.Instance.Add("ModularConduitPort.OnSpawn", base.gameObject, extents, GameScenePartitioner.Instance.objectLayers[(int)this.objectLayer], new Action<object>(this.OnNeighbourCellsUpdated));
		this.UpdateEndCaps();
		this.CorrectAdjacentLaunchPads();
	}

	// Token: 0x06003330 RID: 13104 RVA: 0x001206DA File Offset: 0x0011E8DA
	protected override void OnCleanUp()
	{
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
		base.OnCleanUp();
	}

	// Token: 0x06003331 RID: 13105 RVA: 0x001206F4 File Offset: 0x0011E8F4
	private void UpdateEndCaps()
	{
		int num;
		int num2;
		Grid.CellToXY(Grid.PosToCell(this), out num, out num2);
		int cellLeft = this.GetCellLeft();
		int cellRight = this.GetCellRight();
		if (Grid.IsValidCell(cellLeft))
		{
			if (this.HasTileableNeighbour(cellLeft))
			{
				this.leftCapSetting = ModularConduitPortTiler.AnimCapType.Conduit;
			}
			else if (this.HasLaunchpadNeighbour(cellLeft))
			{
				this.leftCapSetting = ModularConduitPortTiler.AnimCapType.Launchpad;
			}
			else
			{
				this.leftCapSetting = ModularConduitPortTiler.AnimCapType.Default;
			}
		}
		if (Grid.IsValidCell(cellRight))
		{
			if (this.HasTileableNeighbour(cellRight))
			{
				this.rightCapSetting = ModularConduitPortTiler.AnimCapType.Conduit;
			}
			else if (this.HasLaunchpadNeighbour(cellRight))
			{
				this.rightCapSetting = ModularConduitPortTiler.AnimCapType.Launchpad;
			}
			else
			{
				this.rightCapSetting = ModularConduitPortTiler.AnimCapType.Default;
			}
		}
		if (this.manageLeftCap)
		{
			this.leftCapDefault.Enable(this.leftCapSetting == ModularConduitPortTiler.AnimCapType.Default);
			this.leftCapConduit.Enable(this.leftCapSetting == ModularConduitPortTiler.AnimCapType.Conduit);
			this.leftCapLaunchpad.Enable(this.leftCapSetting == ModularConduitPortTiler.AnimCapType.Launchpad);
		}
		if (this.manageRightCap)
		{
			this.rightCapDefault.Enable(this.rightCapSetting == ModularConduitPortTiler.AnimCapType.Default);
			this.rightCapConduit.Enable(this.rightCapSetting == ModularConduitPortTiler.AnimCapType.Conduit);
			this.rightCapLaunchpad.Enable(this.rightCapSetting == ModularConduitPortTiler.AnimCapType.Launchpad);
		}
	}

	// Token: 0x06003332 RID: 13106 RVA: 0x0012080C File Offset: 0x0011EA0C
	private int GetCellLeft()
	{
		int num = Grid.PosToCell(this);
		int num2;
		int num3;
		Grid.CellToXY(num, out num2, out num3);
		CellOffset cellOffset = new CellOffset(this.extents.x - num2 - 1, 0);
		return Grid.OffsetCell(num, cellOffset);
	}

	// Token: 0x06003333 RID: 13107 RVA: 0x00120848 File Offset: 0x0011EA48
	private int GetCellRight()
	{
		int num = Grid.PosToCell(this);
		int num2;
		int num3;
		Grid.CellToXY(num, out num2, out num3);
		CellOffset cellOffset = new CellOffset(this.extents.x - num2 + this.extents.width, 0);
		return Grid.OffsetCell(num, cellOffset);
	}

	// Token: 0x06003334 RID: 13108 RVA: 0x0012088C File Offset: 0x0011EA8C
	private bool HasTileableNeighbour(int neighbour_cell)
	{
		bool flag = false;
		GameObject gameObject = Grid.Objects[neighbour_cell, (int)this.objectLayer];
		if (gameObject != null)
		{
			KPrefabID component = gameObject.GetComponent<KPrefabID>();
			if (component != null && component.HasAnyTags(this.tags))
			{
				flag = true;
			}
		}
		return flag;
	}

	// Token: 0x06003335 RID: 13109 RVA: 0x001208D8 File Offset: 0x0011EAD8
	private bool HasLaunchpadNeighbour(int neighbour_cell)
	{
		GameObject gameObject = Grid.Objects[neighbour_cell, (int)this.objectLayer];
		return gameObject != null && gameObject.GetComponent<LaunchPad>() != null;
	}

	// Token: 0x06003336 RID: 13110 RVA: 0x00120911 File Offset: 0x0011EB11
	private void OnNeighbourCellsUpdated(object data)
	{
		if (this == null || base.gameObject == null)
		{
			return;
		}
		if (this.partitionerEntry.IsValid())
		{
			this.UpdateEndCaps();
		}
	}

	// Token: 0x06003337 RID: 13111 RVA: 0x00120940 File Offset: 0x0011EB40
	private void CorrectAdjacentLaunchPads()
	{
		int cellRight = this.GetCellRight();
		if (Grid.IsValidCell(cellRight) && this.HasLaunchpadNeighbour(cellRight))
		{
			Grid.Objects[cellRight, 1].GetComponent<ModularConduitPortTiler>().UpdateEndCaps();
		}
		int cellLeft = this.GetCellLeft();
		if (Grid.IsValidCell(cellLeft) && this.HasLaunchpadNeighbour(cellLeft))
		{
			Grid.Objects[cellLeft, 1].GetComponent<ModularConduitPortTiler>().UpdateEndCaps();
		}
	}

	// Token: 0x04001EB8 RID: 7864
	private HandleVector<int>.Handle partitionerEntry;

	// Token: 0x04001EB9 RID: 7865
	public ObjectLayer objectLayer = ObjectLayer.Building;

	// Token: 0x04001EBA RID: 7866
	public Tag[] tags;

	// Token: 0x04001EBB RID: 7867
	public bool manageLeftCap = true;

	// Token: 0x04001EBC RID: 7868
	public bool manageRightCap = true;

	// Token: 0x04001EBD RID: 7869
	public int leftCapDefaultSceneLayerAdjust;

	// Token: 0x04001EBE RID: 7870
	public int rightCapDefaultSceneLayerAdjust;

	// Token: 0x04001EBF RID: 7871
	private Extents extents;

	// Token: 0x04001EC0 RID: 7872
	private ModularConduitPortTiler.AnimCapType leftCapSetting;

	// Token: 0x04001EC1 RID: 7873
	private ModularConduitPortTiler.AnimCapType rightCapSetting;

	// Token: 0x04001EC2 RID: 7874
	private static readonly string leftCapDefaultStr = "#cap_left_default";

	// Token: 0x04001EC3 RID: 7875
	private static readonly string leftCapLaunchpadStr = "#cap_left_launchpad";

	// Token: 0x04001EC4 RID: 7876
	private static readonly string leftCapConduitStr = "#cap_left_conduit";

	// Token: 0x04001EC5 RID: 7877
	private static readonly string rightCapDefaultStr = "#cap_right_default";

	// Token: 0x04001EC6 RID: 7878
	private static readonly string rightCapLaunchpadStr = "#cap_right_launchpad";

	// Token: 0x04001EC7 RID: 7879
	private static readonly string rightCapConduitStr = "#cap_right_conduit";

	// Token: 0x04001EC8 RID: 7880
	private KAnimSynchronizedController leftCapDefault;

	// Token: 0x04001EC9 RID: 7881
	private KAnimSynchronizedController leftCapLaunchpad;

	// Token: 0x04001ECA RID: 7882
	private KAnimSynchronizedController leftCapConduit;

	// Token: 0x04001ECB RID: 7883
	private KAnimSynchronizedController rightCapDefault;

	// Token: 0x04001ECC RID: 7884
	private KAnimSynchronizedController rightCapLaunchpad;

	// Token: 0x04001ECD RID: 7885
	private KAnimSynchronizedController rightCapConduit;

	// Token: 0x0200169B RID: 5787
	private enum AnimCapType
	{
		// Token: 0x04007368 RID: 29544
		Default,
		// Token: 0x04007369 RID: 29545
		Conduit,
		// Token: 0x0400736A RID: 29546
		Launchpad
	}
}
