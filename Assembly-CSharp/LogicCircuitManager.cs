using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020009B3 RID: 2483
public class LogicCircuitManager
{
	// Token: 0x06004843 RID: 18499 RVA: 0x001A17DC File Offset: 0x0019F9DC
	public LogicCircuitManager(UtilityNetworkManager<LogicCircuitNetwork, LogicWire> conduit_system)
	{
		this.conduitSystem = conduit_system;
		this.timeSinceBridgeRefresh = 0f;
		this.elapsedTime = 0f;
		for (int i = 0; i < 2; i++)
		{
			this.bridgeGroups[i] = new List<LogicUtilityNetworkLink>();
		}
	}

	// Token: 0x06004844 RID: 18500 RVA: 0x001A183C File Offset: 0x0019FA3C
	public void RenderEveryTick(float dt)
	{
		this.Refresh(dt);
	}

	// Token: 0x06004845 RID: 18501 RVA: 0x001A1848 File Offset: 0x0019FA48
	private void Refresh(float dt)
	{
		if (this.conduitSystem.IsDirty)
		{
			this.conduitSystem.Update();
			LogicCircuitNetwork.logicSoundRegister.Clear();
			this.PropagateSignals(true);
			this.elapsedTime = 0f;
		}
		else if (SpeedControlScreen.Instance != null && !SpeedControlScreen.Instance.IsPaused)
		{
			this.elapsedTime += dt;
			this.timeSinceBridgeRefresh += dt;
			while (this.elapsedTime > LogicCircuitManager.ClockTickInterval)
			{
				this.elapsedTime -= LogicCircuitManager.ClockTickInterval;
				this.PropagateSignals(false);
				if (this.onLogicTick != null)
				{
					this.onLogicTick();
				}
			}
			if (this.timeSinceBridgeRefresh > LogicCircuitManager.BridgeRefreshInterval)
			{
				this.UpdateCircuitBridgeLists();
				this.timeSinceBridgeRefresh = 0f;
			}
		}
		foreach (UtilityNetwork utilityNetwork in Game.Instance.logicCircuitSystem.GetNetworks())
		{
			LogicCircuitNetwork logicCircuitNetwork = (LogicCircuitNetwork)utilityNetwork;
			this.CheckCircuitOverloaded(dt, logicCircuitNetwork.id, logicCircuitNetwork.GetBitsUsed());
		}
	}

	// Token: 0x06004846 RID: 18502 RVA: 0x001A197C File Offset: 0x0019FB7C
	private void PropagateSignals(bool force_send_events)
	{
		IList<UtilityNetwork> networks = Game.Instance.logicCircuitSystem.GetNetworks();
		foreach (UtilityNetwork utilityNetwork in networks)
		{
			((LogicCircuitNetwork)utilityNetwork).UpdateLogicValue();
		}
		foreach (UtilityNetwork utilityNetwork2 in networks)
		{
			LogicCircuitNetwork logicCircuitNetwork = (LogicCircuitNetwork)utilityNetwork2;
			logicCircuitNetwork.SendLogicEvents(force_send_events, logicCircuitNetwork.id);
		}
	}

	// Token: 0x06004847 RID: 18503 RVA: 0x001A1A18 File Offset: 0x0019FC18
	public LogicCircuitNetwork GetNetworkForCell(int cell)
	{
		return this.conduitSystem.GetNetworkForCell(cell) as LogicCircuitNetwork;
	}

	// Token: 0x06004848 RID: 18504 RVA: 0x001A1A2B File Offset: 0x0019FC2B
	public void AddVisElem(ILogicUIElement elem)
	{
		this.uiVisElements.Add(elem);
		if (this.onElemAdded != null)
		{
			this.onElemAdded(elem);
		}
	}

	// Token: 0x06004849 RID: 18505 RVA: 0x001A1A4D File Offset: 0x0019FC4D
	public void RemoveVisElem(ILogicUIElement elem)
	{
		if (this.onElemRemoved != null)
		{
			this.onElemRemoved(elem);
		}
		this.uiVisElements.Remove(elem);
	}

	// Token: 0x0600484A RID: 18506 RVA: 0x001A1A70 File Offset: 0x0019FC70
	public List<ILogicUIElement> GetVisElements()
	{
		return this.uiVisElements;
	}

	// Token: 0x0600484B RID: 18507 RVA: 0x001A1A78 File Offset: 0x0019FC78
	public static void ToggleNoWireConnected(bool show_missing_wire, GameObject go)
	{
		go.GetComponent<KSelectable>().ToggleStatusItem(Db.Get().BuildingStatusItems.NoLogicWireConnected, show_missing_wire, null);
	}

	// Token: 0x0600484C RID: 18508 RVA: 0x001A1A98 File Offset: 0x0019FC98
	private void CheckCircuitOverloaded(float dt, int id, int bits_used)
	{
		UtilityNetwork networkByID = Game.Instance.logicCircuitSystem.GetNetworkByID(id);
		if (networkByID != null)
		{
			LogicCircuitNetwork logicCircuitNetwork = (LogicCircuitNetwork)networkByID;
			if (logicCircuitNetwork != null)
			{
				logicCircuitNetwork.UpdateOverloadTime(dt, bits_used);
			}
		}
	}

	// Token: 0x0600484D RID: 18509 RVA: 0x001A1ACB File Offset: 0x0019FCCB
	public void Connect(LogicUtilityNetworkLink bridge)
	{
		this.bridgeGroups[(int)bridge.bitDepth].Add(bridge);
	}

	// Token: 0x0600484E RID: 18510 RVA: 0x001A1AE0 File Offset: 0x0019FCE0
	public void Disconnect(LogicUtilityNetworkLink bridge)
	{
		this.bridgeGroups[(int)bridge.bitDepth].Remove(bridge);
	}

	// Token: 0x0600484F RID: 18511 RVA: 0x001A1AF8 File Offset: 0x0019FCF8
	private void UpdateCircuitBridgeLists()
	{
		foreach (UtilityNetwork utilityNetwork in Game.Instance.logicCircuitSystem.GetNetworks())
		{
			LogicCircuitNetwork logicCircuitNetwork = (LogicCircuitNetwork)utilityNetwork;
			if (this.updateEvenBridgeGroups)
			{
				if (logicCircuitNetwork.id % 2 == 0)
				{
					logicCircuitNetwork.UpdateRelevantBridges(this.bridgeGroups);
				}
			}
			else if (logicCircuitNetwork.id % 2 == 1)
			{
				logicCircuitNetwork.UpdateRelevantBridges(this.bridgeGroups);
			}
		}
		this.updateEvenBridgeGroups = !this.updateEvenBridgeGroups;
	}

	// Token: 0x04002FB4 RID: 12212
	public static float ClockTickInterval = 0.1f;

	// Token: 0x04002FB5 RID: 12213
	private float elapsedTime;

	// Token: 0x04002FB6 RID: 12214
	private UtilityNetworkManager<LogicCircuitNetwork, LogicWire> conduitSystem;

	// Token: 0x04002FB7 RID: 12215
	private List<ILogicUIElement> uiVisElements = new List<ILogicUIElement>();

	// Token: 0x04002FB8 RID: 12216
	public static float BridgeRefreshInterval = 1f;

	// Token: 0x04002FB9 RID: 12217
	private List<LogicUtilityNetworkLink>[] bridgeGroups = new List<LogicUtilityNetworkLink>[2];

	// Token: 0x04002FBA RID: 12218
	private bool updateEvenBridgeGroups;

	// Token: 0x04002FBB RID: 12219
	private float timeSinceBridgeRefresh;

	// Token: 0x04002FBC RID: 12220
	public global::System.Action onLogicTick;

	// Token: 0x04002FBD RID: 12221
	public Action<ILogicUIElement> onElemAdded;

	// Token: 0x04002FBE RID: 12222
	public Action<ILogicUIElement> onElemRemoved;

	// Token: 0x020019BF RID: 6591
	private struct Signal
	{
		// Token: 0x0600A0BD RID: 41149 RVA: 0x0039C354 File Offset: 0x0039A554
		public Signal(int cell, int value)
		{
			this.cell = cell;
			this.value = value;
		}

		// Token: 0x04007DA1 RID: 32161
		public int cell;

		// Token: 0x04007DA2 RID: 32162
		public int value;
	}
}
