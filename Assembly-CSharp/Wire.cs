using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007FA RID: 2042
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/Wire")]
public class Wire : KMonoBehaviour, IDisconnectable, IFirstFrameCallback, IWattageRating, IHaveUtilityNetworkMgr, IBridgedNetworkItem
{
	// Token: 0x06003784 RID: 14212 RVA: 0x0013431C File Offset: 0x0013251C
	public static float GetMaxWattageAsFloat(Wire.WattageRating rating)
	{
		switch (rating)
		{
		case Wire.WattageRating.Max500:
			return 500f;
		case Wire.WattageRating.Max1000:
			return 1000f;
		case Wire.WattageRating.Max2000:
			return 2000f;
		case Wire.WattageRating.Max20000:
			return 20000f;
		case Wire.WattageRating.Max50000:
			return 50000f;
		default:
			return 0f;
		}
	}

	// Token: 0x170003BD RID: 957
	// (get) Token: 0x06003785 RID: 14213 RVA: 0x00134368 File Offset: 0x00132568
	public bool IsConnected
	{
		get
		{
			int num = Grid.PosToCell(base.transform.GetPosition());
			return Game.Instance.electricalConduitSystem.GetNetworkForCell(num) is ElectricalUtilityNetwork;
		}
	}

	// Token: 0x170003BE RID: 958
	// (get) Token: 0x06003786 RID: 14214 RVA: 0x001343A0 File Offset: 0x001325A0
	public ushort NetworkID
	{
		get
		{
			int num = Grid.PosToCell(base.transform.GetPosition());
			ElectricalUtilityNetwork electricalUtilityNetwork = Game.Instance.electricalConduitSystem.GetNetworkForCell(num) as ElectricalUtilityNetwork;
			if (electricalUtilityNetwork == null)
			{
				return ushort.MaxValue;
			}
			return (ushort)electricalUtilityNetwork.id;
		}
	}

	// Token: 0x06003787 RID: 14215 RVA: 0x001343E4 File Offset: 0x001325E4
	protected override void OnSpawn()
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		Game.Instance.electricalConduitSystem.AddToNetworks(num, this, false);
		this.InitializeSwitchState();
		base.Subscribe<Wire>(774203113, Wire.OnBuildingBrokenDelegate);
		base.Subscribe<Wire>(-1735440190, Wire.OnBuildingFullyRepairedDelegate);
		base.GetComponent<KSelectable>().AddStatusItem(Wire.WireCircuitStatus, this);
		base.GetComponent<KSelectable>().AddStatusItem(Wire.WireMaxWattageStatus, this);
		base.GetComponent<KBatchedAnimController>().SetSymbolVisiblity(Wire.OutlineSymbol, false);
	}

	// Token: 0x06003788 RID: 14216 RVA: 0x00134474 File Offset: 0x00132674
	protected override void OnCleanUp()
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		BuildingComplete component = base.GetComponent<BuildingComplete>();
		if (component.Def.ReplacementLayer == ObjectLayer.NumLayers || Grid.Objects[num, (int)component.Def.ReplacementLayer] == null)
		{
			Game.Instance.electricalConduitSystem.RemoveFromNetworks(num, this, false);
		}
		base.Unsubscribe<Wire>(774203113, Wire.OnBuildingBrokenDelegate, false);
		base.Unsubscribe<Wire>(-1735440190, Wire.OnBuildingFullyRepairedDelegate, false);
		base.OnCleanUp();
	}

	// Token: 0x06003789 RID: 14217 RVA: 0x00134500 File Offset: 0x00132700
	private void InitializeSwitchState()
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		bool flag = false;
		GameObject gameObject = Grid.Objects[num, 1];
		if (gameObject != null)
		{
			CircuitSwitch component = gameObject.GetComponent<CircuitSwitch>();
			if (component != null)
			{
				flag = true;
				component.AttachWire(this);
			}
		}
		if (!flag)
		{
			this.Connect();
		}
	}

	// Token: 0x0600378A RID: 14218 RVA: 0x0013455C File Offset: 0x0013275C
	public UtilityConnections GetWireConnections()
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		return Game.Instance.electricalConduitSystem.GetConnections(num, true);
	}

	// Token: 0x0600378B RID: 14219 RVA: 0x0013458C File Offset: 0x0013278C
	public string GetWireConnectionsString()
	{
		UtilityConnections wireConnections = this.GetWireConnections();
		return Game.Instance.electricalConduitSystem.GetVisualizerString(wireConnections);
	}

	// Token: 0x0600378C RID: 14220 RVA: 0x001345B0 File Offset: 0x001327B0
	private void OnBuildingBroken(object data)
	{
		this.Disconnect();
	}

	// Token: 0x0600378D RID: 14221 RVA: 0x001345B8 File Offset: 0x001327B8
	private void OnBuildingFullyRepaired(object data)
	{
		this.InitializeSwitchState();
	}

	// Token: 0x0600378E RID: 14222 RVA: 0x001345C0 File Offset: 0x001327C0
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.GetComponent<KPrefabID>().AddTag(GameTags.Wires, false);
		if (Wire.WireCircuitStatus == null)
		{
			Wire.WireCircuitStatus = new StatusItem("WireCircuitStatus", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null).SetResolveStringCallback(delegate(string str, object data)
			{
				Wire wire = (Wire)data;
				int num = Grid.PosToCell(wire.transform.GetPosition());
				CircuitManager circuitManager = Game.Instance.circuitManager;
				ushort circuitID = circuitManager.GetCircuitID(num);
				float wattsUsedByCircuit = circuitManager.GetWattsUsedByCircuit(circuitID);
				GameUtil.WattageFormatterUnit wattageFormatterUnit = GameUtil.WattageFormatterUnit.Watts;
				if (wire.MaxWattageRating >= Wire.WattageRating.Max20000)
				{
					wattageFormatterUnit = GameUtil.WattageFormatterUnit.Kilowatts;
				}
				float maxWattageAsFloat = Wire.GetMaxWattageAsFloat(wire.MaxWattageRating);
				float wattsNeededWhenActive = circuitManager.GetWattsNeededWhenActive(circuitID);
				string wireLoadColor = GameUtil.GetWireLoadColor(wattsUsedByCircuit, maxWattageAsFloat, wattsNeededWhenActive);
				string text = ((wattsUsedByCircuit < 0f) ? "?" : GameUtil.GetFormattedWattage(wattsUsedByCircuit, wattageFormatterUnit, true));
				str = str.Replace("{CurrentLoadAndColor}", (wireLoadColor == Color.white.ToHexString()) ? text : string.Concat(new string[] { "<color=#", wireLoadColor, ">", text, "</color>" }));
				str = str.Replace("{MaxLoad}", GameUtil.GetFormattedWattage(maxWattageAsFloat, wattageFormatterUnit, true));
				str = str.Replace("{WireType}", this.GetProperName());
				return str;
			});
		}
		if (Wire.WireMaxWattageStatus == null)
		{
			Wire.WireMaxWattageStatus = new StatusItem("WireMaxWattageStatus", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null).SetResolveStringCallback(delegate(string str, object data)
			{
				Wire wire2 = (Wire)data;
				GameUtil.WattageFormatterUnit wattageFormatterUnit2 = GameUtil.WattageFormatterUnit.Watts;
				if (wire2.MaxWattageRating >= Wire.WattageRating.Max20000)
				{
					wattageFormatterUnit2 = GameUtil.WattageFormatterUnit.Kilowatts;
				}
				int num2 = Grid.PosToCell(wire2.transform.GetPosition());
				CircuitManager circuitManager2 = Game.Instance.circuitManager;
				ushort circuitID2 = circuitManager2.GetCircuitID(num2);
				float wattsNeededWhenActive2 = circuitManager2.GetWattsNeededWhenActive(circuitID2);
				float maxWattageAsFloat2 = Wire.GetMaxWattageAsFloat(wire2.MaxWattageRating);
				str = str.Replace("{TotalPotentialLoadAndColor}", (wattsNeededWhenActive2 > maxWattageAsFloat2) ? string.Concat(new string[]
				{
					"<color=#",
					new Color(0.9843137f, 0.6901961f, 0.23137255f).ToHexString(),
					">",
					GameUtil.GetFormattedWattage(wattsNeededWhenActive2, wattageFormatterUnit2, true),
					"</color>"
				}) : GameUtil.GetFormattedWattage(wattsNeededWhenActive2, wattageFormatterUnit2, true));
				str = str.Replace("{MaxLoad}", GameUtil.GetFormattedWattage(maxWattageAsFloat2, wattageFormatterUnit2, true));
				return str;
			});
		}
	}

	// Token: 0x0600378F RID: 14223 RVA: 0x00134677 File Offset: 0x00132877
	public Wire.WattageRating GetMaxWattageRating()
	{
		return this.MaxWattageRating;
	}

	// Token: 0x06003790 RID: 14224 RVA: 0x0013467F File Offset: 0x0013287F
	public bool IsDisconnected()
	{
		return this.disconnected;
	}

	// Token: 0x06003791 RID: 14225 RVA: 0x00134688 File Offset: 0x00132888
	public bool Connect()
	{
		BuildingHP component = base.GetComponent<BuildingHP>();
		if (component == null || component.HitPoints > 0)
		{
			this.disconnected = false;
			Game.Instance.electricalConduitSystem.ForceRebuildNetworks();
		}
		return !this.disconnected;
	}

	// Token: 0x06003792 RID: 14226 RVA: 0x001346D0 File Offset: 0x001328D0
	public void Disconnect()
	{
		this.disconnected = true;
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Power, Db.Get().BuildingStatusItems.WireDisconnected, null);
		Game.Instance.electricalConduitSystem.ForceRebuildNetworks();
	}

	// Token: 0x06003793 RID: 14227 RVA: 0x0013471E File Offset: 0x0013291E
	public void SetFirstFrameCallback(global::System.Action ffCb)
	{
		this.firstFrameCallback = ffCb;
		base.StartCoroutine(this.RunCallback());
	}

	// Token: 0x06003794 RID: 14228 RVA: 0x00134734 File Offset: 0x00132934
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

	// Token: 0x06003795 RID: 14229 RVA: 0x00134743 File Offset: 0x00132943
	public IUtilityNetworkMgr GetNetworkManager()
	{
		return Game.Instance.electricalConduitSystem;
	}

	// Token: 0x06003796 RID: 14230 RVA: 0x00134750 File Offset: 0x00132950
	public void AddNetworks(ICollection<UtilityNetwork> networks)
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		UtilityNetwork networkForCell = Game.Instance.electricalConduitSystem.GetNetworkForCell(num);
		if (networkForCell != null)
		{
			networks.Add(networkForCell);
		}
	}

	// Token: 0x06003797 RID: 14231 RVA: 0x0013478C File Offset: 0x0013298C
	public bool IsConnectedToNetworks(ICollection<UtilityNetwork> networks)
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		UtilityNetwork networkForCell = Game.Instance.electricalConduitSystem.GetNetworkForCell(num);
		return networks.Contains(networkForCell);
	}

	// Token: 0x06003798 RID: 14232 RVA: 0x001347C2 File Offset: 0x001329C2
	public int GetNetworkCell()
	{
		return Grid.PosToCell(this);
	}

	// Token: 0x040021B0 RID: 8624
	[SerializeField]
	public Wire.WattageRating MaxWattageRating;

	// Token: 0x040021B1 RID: 8625
	[SerializeField]
	private bool disconnected = true;

	// Token: 0x040021B2 RID: 8626
	public static readonly KAnimHashedString OutlineSymbol = new KAnimHashedString("outline");

	// Token: 0x040021B3 RID: 8627
	public float circuitOverloadTime;

	// Token: 0x040021B4 RID: 8628
	private static readonly EventSystem.IntraObjectHandler<Wire> OnBuildingBrokenDelegate = new EventSystem.IntraObjectHandler<Wire>(delegate(Wire component, object data)
	{
		component.OnBuildingBroken(data);
	});

	// Token: 0x040021B5 RID: 8629
	private static readonly EventSystem.IntraObjectHandler<Wire> OnBuildingFullyRepairedDelegate = new EventSystem.IntraObjectHandler<Wire>(delegate(Wire component, object data)
	{
		component.OnBuildingFullyRepaired(data);
	});

	// Token: 0x040021B6 RID: 8630
	private static StatusItem WireCircuitStatus = null;

	// Token: 0x040021B7 RID: 8631
	private static StatusItem WireMaxWattageStatus = null;

	// Token: 0x040021B8 RID: 8632
	private global::System.Action firstFrameCallback;

	// Token: 0x02001759 RID: 5977
	public enum WattageRating
	{
		// Token: 0x0400756C RID: 30060
		Max500,
		// Token: 0x0400756D RID: 30061
		Max1000,
		// Token: 0x0400756E RID: 30062
		Max2000,
		// Token: 0x0400756F RID: 30063
		Max20000,
		// Token: 0x04007570 RID: 30064
		Max50000,
		// Token: 0x04007571 RID: 30065
		NumRatings
	}
}
