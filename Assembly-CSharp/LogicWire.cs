using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200077A RID: 1914
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/LogicWire")]
public class LogicWire : KMonoBehaviour, IFirstFrameCallback, IHaveUtilityNetworkMgr, IBridgedNetworkItem, IBitRating, IDisconnectable
{
	// Token: 0x0600324C RID: 12876 RVA: 0x0011BD59 File Offset: 0x00119F59
	public static int GetBitDepthAsInt(LogicWire.BitDepth rating)
	{
		if (rating == LogicWire.BitDepth.OneBit)
		{
			return 1;
		}
		if (rating != LogicWire.BitDepth.FourBit)
		{
			return 0;
		}
		return 4;
	}

	// Token: 0x0600324D RID: 12877 RVA: 0x0011BD6C File Offset: 0x00119F6C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		int num = Grid.PosToCell(base.transform.GetPosition());
		Game.Instance.logicCircuitSystem.AddToNetworks(num, this, false);
		base.Subscribe<LogicWire>(774203113, LogicWire.OnBuildingBrokenDelegate);
		base.Subscribe<LogicWire>(-1735440190, LogicWire.OnBuildingFullyRepairedDelegate);
		this.Connect();
		base.GetComponent<KBatchedAnimController>().SetSymbolVisiblity(LogicWire.OutlineSymbol, false);
	}

	// Token: 0x0600324E RID: 12878 RVA: 0x0011BDDC File Offset: 0x00119FDC
	protected override void OnCleanUp()
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		BuildingComplete component = base.GetComponent<BuildingComplete>();
		if (component.Def.ReplacementLayer == ObjectLayer.NumLayers || Grid.Objects[num, (int)component.Def.ReplacementLayer] == null)
		{
			Game.Instance.logicCircuitSystem.RemoveFromNetworks(num, this, false);
		}
		base.Unsubscribe<LogicWire>(774203113, LogicWire.OnBuildingBrokenDelegate, false);
		base.Unsubscribe<LogicWire>(-1735440190, LogicWire.OnBuildingFullyRepairedDelegate, false);
		base.OnCleanUp();
	}

	// Token: 0x1700031A RID: 794
	// (get) Token: 0x0600324F RID: 12879 RVA: 0x0011BE68 File Offset: 0x0011A068
	public bool IsConnected
	{
		get
		{
			int num = Grid.PosToCell(base.transform.GetPosition());
			return Game.Instance.logicCircuitSystem.GetNetworkForCell(num) is LogicCircuitNetwork;
		}
	}

	// Token: 0x06003250 RID: 12880 RVA: 0x0011BE9E File Offset: 0x0011A09E
	public bool IsDisconnected()
	{
		return this.disconnected;
	}

	// Token: 0x06003251 RID: 12881 RVA: 0x0011BEA8 File Offset: 0x0011A0A8
	public bool Connect()
	{
		BuildingHP component = base.GetComponent<BuildingHP>();
		if (component == null || component.HitPoints > 0)
		{
			this.disconnected = false;
			Game.Instance.logicCircuitSystem.ForceRebuildNetworks();
		}
		return !this.disconnected;
	}

	// Token: 0x06003252 RID: 12882 RVA: 0x0011BEF0 File Offset: 0x0011A0F0
	public void Disconnect()
	{
		this.disconnected = true;
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Power, Db.Get().BuildingStatusItems.WireDisconnected, null);
		Game.Instance.logicCircuitSystem.ForceRebuildNetworks();
	}

	// Token: 0x06003253 RID: 12883 RVA: 0x0011BF40 File Offset: 0x0011A140
	public UtilityConnections GetWireConnections()
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		return Game.Instance.logicCircuitSystem.GetConnections(num, true);
	}

	// Token: 0x06003254 RID: 12884 RVA: 0x0011BF70 File Offset: 0x0011A170
	public string GetWireConnectionsString()
	{
		UtilityConnections wireConnections = this.GetWireConnections();
		return Game.Instance.logicCircuitSystem.GetVisualizerString(wireConnections);
	}

	// Token: 0x06003255 RID: 12885 RVA: 0x0011BF94 File Offset: 0x0011A194
	private void OnBuildingBroken(object data)
	{
		this.Disconnect();
	}

	// Token: 0x06003256 RID: 12886 RVA: 0x0011BF9C File Offset: 0x0011A19C
	private void OnBuildingFullyRepaired(object data)
	{
		this.Connect();
	}

	// Token: 0x06003257 RID: 12887 RVA: 0x0011BFA5 File Offset: 0x0011A1A5
	public void SetFirstFrameCallback(global::System.Action ffCb)
	{
		this.firstFrameCallback = ffCb;
		base.StartCoroutine(this.RunCallback());
	}

	// Token: 0x06003258 RID: 12888 RVA: 0x0011BFBB File Offset: 0x0011A1BB
	private IEnumerator RunCallback()
	{
		yield return null;
		if (this.firstFrameCallback != null)
		{
			this.firstFrameCallback();
			this.firstFrameCallback = null;
		}
		yield return null;
		yield break;
	}

	// Token: 0x06003259 RID: 12889 RVA: 0x0011BFCA File Offset: 0x0011A1CA
	public LogicWire.BitDepth GetMaxBitRating()
	{
		return this.MaxBitDepth;
	}

	// Token: 0x0600325A RID: 12890 RVA: 0x0011BFD2 File Offset: 0x0011A1D2
	public IUtilityNetworkMgr GetNetworkManager()
	{
		return Game.Instance.logicCircuitSystem;
	}

	// Token: 0x0600325B RID: 12891 RVA: 0x0011BFE0 File Offset: 0x0011A1E0
	public void AddNetworks(ICollection<UtilityNetwork> networks)
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		UtilityNetwork networkForCell = Game.Instance.logicCircuitSystem.GetNetworkForCell(num);
		if (networkForCell != null)
		{
			networks.Add(networkForCell);
		}
	}

	// Token: 0x0600325C RID: 12892 RVA: 0x0011C01C File Offset: 0x0011A21C
	public bool IsConnectedToNetworks(ICollection<UtilityNetwork> networks)
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		UtilityNetwork networkForCell = Game.Instance.logicCircuitSystem.GetNetworkForCell(num);
		return networks.Contains(networkForCell);
	}

	// Token: 0x0600325D RID: 12893 RVA: 0x0011C052 File Offset: 0x0011A252
	public int GetNetworkCell()
	{
		return Grid.PosToCell(this);
	}

	// Token: 0x04001E29 RID: 7721
	[SerializeField]
	public LogicWire.BitDepth MaxBitDepth;

	// Token: 0x04001E2A RID: 7722
	[SerializeField]
	private bool disconnected = true;

	// Token: 0x04001E2B RID: 7723
	public static readonly KAnimHashedString OutlineSymbol = new KAnimHashedString("outline");

	// Token: 0x04001E2C RID: 7724
	private static readonly EventSystem.IntraObjectHandler<LogicWire> OnBuildingBrokenDelegate = new EventSystem.IntraObjectHandler<LogicWire>(delegate(LogicWire component, object data)
	{
		component.OnBuildingBroken(data);
	});

	// Token: 0x04001E2D RID: 7725
	private static readonly EventSystem.IntraObjectHandler<LogicWire> OnBuildingFullyRepairedDelegate = new EventSystem.IntraObjectHandler<LogicWire>(delegate(LogicWire component, object data)
	{
		component.OnBuildingFullyRepaired(data);
	});

	// Token: 0x04001E2E RID: 7726
	private global::System.Action firstFrameCallback;

	// Token: 0x0200165A RID: 5722
	public enum BitDepth
	{
		// Token: 0x04007279 RID: 29305
		OneBit,
		// Token: 0x0400727A RID: 29306
		FourBit,
		// Token: 0x0400727B RID: 29307
		NumRatings
	}
}
