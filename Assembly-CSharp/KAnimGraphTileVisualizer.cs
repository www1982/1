using System;
using KSerialization;
using UnityEngine;

// Token: 0x020005C7 RID: 1479
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/KAnimGraphTileVisualizer")]
public class KAnimGraphTileVisualizer : KMonoBehaviour, ISaveLoadable, IUtilityItem
{
	// Token: 0x17000159 RID: 345
	// (get) Token: 0x0600220F RID: 8719 RVA: 0x000C440B File Offset: 0x000C260B
	// (set) Token: 0x06002210 RID: 8720 RVA: 0x000C4413 File Offset: 0x000C2613
	public UtilityConnections Connections
	{
		get
		{
			return this._connections;
		}
		set
		{
			this._connections = value;
			base.Trigger(-1041684577, this._connections);
		}
	}

	// Token: 0x1700015A RID: 346
	// (get) Token: 0x06002211 RID: 8721 RVA: 0x000C4434 File Offset: 0x000C2634
	public IUtilityNetworkMgr ConnectionManager
	{
		get
		{
			switch (this.connectionSource)
			{
			case KAnimGraphTileVisualizer.ConnectionSource.Gas:
				return Game.Instance.gasConduitSystem;
			case KAnimGraphTileVisualizer.ConnectionSource.Liquid:
				return Game.Instance.liquidConduitSystem;
			case KAnimGraphTileVisualizer.ConnectionSource.Electrical:
				return Game.Instance.electricalConduitSystem;
			case KAnimGraphTileVisualizer.ConnectionSource.Logic:
				return Game.Instance.logicCircuitSystem;
			case KAnimGraphTileVisualizer.ConnectionSource.Tube:
				return Game.Instance.travelTubeSystem;
			case KAnimGraphTileVisualizer.ConnectionSource.Solid:
				return Game.Instance.solidConduitSystem;
			default:
				return null;
			}
		}
	}

	// Token: 0x06002212 RID: 8722 RVA: 0x000C44AC File Offset: 0x000C26AC
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.connectionManager = this.ConnectionManager;
		int num = Grid.PosToCell(base.transform.GetPosition());
		this.connectionManager.SetConnections(this.Connections, num, this.isPhysicalBuilding);
		Building component = base.GetComponent<Building>();
		TileVisualizer.RefreshCell(num, component.Def.TileLayer, component.Def.ReplacementLayer);
	}

	// Token: 0x06002213 RID: 8723 RVA: 0x000C4518 File Offset: 0x000C2718
	protected override void OnCleanUp()
	{
		if (this.connectionManager != null && !this.skipCleanup)
		{
			this.skipRefresh = true;
			int num = Grid.PosToCell(base.transform.GetPosition());
			this.connectionManager.ClearCell(num, this.isPhysicalBuilding);
			Building component = base.GetComponent<Building>();
			TileVisualizer.RefreshCell(num, component.Def.TileLayer, component.Def.ReplacementLayer);
		}
	}

	// Token: 0x06002214 RID: 8724 RVA: 0x000C4584 File Offset: 0x000C2784
	[ContextMenu("Refresh")]
	public void Refresh()
	{
		if (this.connectionManager == null || this.skipRefresh)
		{
			return;
		}
		int num = Grid.PosToCell(base.transform.GetPosition());
		this.Connections = this.connectionManager.GetConnections(num, this.isPhysicalBuilding);
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		if (component != null)
		{
			string text = this.connectionManager.GetVisualizerString(num);
			if (base.GetComponent<BuildingUnderConstruction>() != null && component.HasAnimation(text + "_place"))
			{
				text += "_place";
			}
			if (text != null && text != "")
			{
				component.Play(text, KAnim.PlayMode.Once, 1f, 0f);
			}
		}
	}

	// Token: 0x06002215 RID: 8725 RVA: 0x000C4644 File Offset: 0x000C2844
	public int GetNetworkID()
	{
		UtilityNetwork network = this.GetNetwork();
		if (network == null)
		{
			return -1;
		}
		return network.id;
	}

	// Token: 0x06002216 RID: 8726 RVA: 0x000C4664 File Offset: 0x000C2864
	private UtilityNetwork GetNetwork()
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		return this.connectionManager.GetNetworkForDirection(num, Direction.None);
	}

	// Token: 0x06002217 RID: 8727 RVA: 0x000C4690 File Offset: 0x000C2890
	public UtilityNetwork GetNetworkForDirection(Direction d)
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		return this.connectionManager.GetNetworkForDirection(num, d);
	}

	// Token: 0x06002218 RID: 8728 RVA: 0x000C46BC File Offset: 0x000C28BC
	public void UpdateConnections(UtilityConnections new_connections)
	{
		this._connections = new_connections;
		if (this.connectionManager != null)
		{
			int num = Grid.PosToCell(base.transform.GetPosition());
			this.connectionManager.SetConnections(new_connections, num, this.isPhysicalBuilding);
		}
	}

	// Token: 0x06002219 RID: 8729 RVA: 0x000C46FC File Offset: 0x000C28FC
	public KAnimGraphTileVisualizer GetNeighbour(Direction d)
	{
		KAnimGraphTileVisualizer kanimGraphTileVisualizer = null;
		Vector2I vector2I;
		Grid.PosToXY(base.transform.GetPosition(), out vector2I);
		int num = -1;
		switch (d)
		{
		case Direction.Up:
			if (vector2I.y < Grid.HeightInCells - 1)
			{
				num = Grid.XYToCell(vector2I.x, vector2I.y + 1);
			}
			break;
		case Direction.Right:
			if (vector2I.x < Grid.WidthInCells - 1)
			{
				num = Grid.XYToCell(vector2I.x + 1, vector2I.y);
			}
			break;
		case Direction.Down:
			if (vector2I.y > 0)
			{
				num = Grid.XYToCell(vector2I.x, vector2I.y - 1);
			}
			break;
		case Direction.Left:
			if (vector2I.x > 0)
			{
				num = Grid.XYToCell(vector2I.x - 1, vector2I.y);
			}
			break;
		}
		if (num != -1)
		{
			ObjectLayer objectLayer;
			switch (this.connectionSource)
			{
			case KAnimGraphTileVisualizer.ConnectionSource.Gas:
				objectLayer = ObjectLayer.GasConduitTile;
				break;
			case KAnimGraphTileVisualizer.ConnectionSource.Liquid:
				objectLayer = ObjectLayer.LiquidConduitTile;
				break;
			case KAnimGraphTileVisualizer.ConnectionSource.Electrical:
				objectLayer = ObjectLayer.WireTile;
				break;
			case KAnimGraphTileVisualizer.ConnectionSource.Logic:
				objectLayer = ObjectLayer.LogicWireTile;
				break;
			case KAnimGraphTileVisualizer.ConnectionSource.Tube:
				objectLayer = ObjectLayer.TravelTubeTile;
				break;
			case KAnimGraphTileVisualizer.ConnectionSource.Solid:
				objectLayer = ObjectLayer.SolidConduitTile;
				break;
			default:
				throw new ArgumentNullException("wtf");
			}
			GameObject gameObject = Grid.Objects[num, (int)objectLayer];
			if (gameObject != null)
			{
				kanimGraphTileVisualizer = gameObject.GetComponent<KAnimGraphTileVisualizer>();
			}
		}
		return kanimGraphTileVisualizer;
	}

	// Token: 0x040013E1 RID: 5089
	[Serialize]
	private UtilityConnections _connections;

	// Token: 0x040013E2 RID: 5090
	public bool isPhysicalBuilding;

	// Token: 0x040013E3 RID: 5091
	public bool skipCleanup;

	// Token: 0x040013E4 RID: 5092
	public bool skipRefresh;

	// Token: 0x040013E5 RID: 5093
	public KAnimGraphTileVisualizer.ConnectionSource connectionSource;

	// Token: 0x040013E6 RID: 5094
	[NonSerialized]
	public IUtilityNetworkMgr connectionManager;

	// Token: 0x02001460 RID: 5216
	public enum ConnectionSource
	{
		// Token: 0x04006C61 RID: 27745
		Gas,
		// Token: 0x04006C62 RID: 27746
		Liquid,
		// Token: 0x04006C63 RID: 27747
		Electrical,
		// Token: 0x04006C64 RID: 27748
		Logic,
		// Token: 0x04006C65 RID: 27749
		Tube,
		// Token: 0x04006C66 RID: 27750
		Solid
	}
}
