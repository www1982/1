using System;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;

// Token: 0x020009B5 RID: 2485
internal class LogicEventHandler : ILogicEventReceiver, ILogicNetworkConnection, ILogicUIElement, IUniformGridObject
{
	// Token: 0x0600485C RID: 18524 RVA: 0x001A1C72 File Offset: 0x0019FE72
	public LogicEventHandler(int cell, Action<int, int> on_value_changed, Action<int, bool> on_connection_changed, LogicPortSpriteType sprite_type)
	{
		this.cell = cell;
		this.onValueChanged = on_value_changed;
		this.onConnectionChanged = on_connection_changed;
		this.spriteType = sprite_type;
	}

	// Token: 0x0600485D RID: 18525 RVA: 0x001A1C98 File Offset: 0x0019FE98
	public void ReceiveLogicEvent(int value)
	{
		this.TriggerAudio(value);
		int num = this.value;
		this.value = value;
		this.onValueChanged(value, num);
	}

	// Token: 0x1700050B RID: 1291
	// (get) Token: 0x0600485E RID: 18526 RVA: 0x001A1CC7 File Offset: 0x0019FEC7
	public int Value
	{
		get
		{
			return this.value;
		}
	}

	// Token: 0x0600485F RID: 18527 RVA: 0x001A1CCF File Offset: 0x0019FECF
	public int GetLogicUICell()
	{
		return this.cell;
	}

	// Token: 0x06004860 RID: 18528 RVA: 0x001A1CD7 File Offset: 0x0019FED7
	public LogicPortSpriteType GetLogicPortSpriteType()
	{
		return this.spriteType;
	}

	// Token: 0x06004861 RID: 18529 RVA: 0x001A1CDF File Offset: 0x0019FEDF
	public Vector2 PosMin()
	{
		return Grid.CellToPos2D(this.cell);
	}

	// Token: 0x06004862 RID: 18530 RVA: 0x001A1CF1 File Offset: 0x0019FEF1
	public Vector2 PosMax()
	{
		return Grid.CellToPos2D(this.cell);
	}

	// Token: 0x06004863 RID: 18531 RVA: 0x001A1D03 File Offset: 0x0019FF03
	public int GetLogicCell()
	{
		return this.cell;
	}

	// Token: 0x06004864 RID: 18532 RVA: 0x001A1D0C File Offset: 0x0019FF0C
	private void TriggerAudio(int new_value)
	{
		LogicCircuitNetwork networkForCell = Game.Instance.logicCircuitManager.GetNetworkForCell(this.cell);
		SpeedControlScreen instance = SpeedControlScreen.Instance;
		if (networkForCell != null && new_value != this.value && instance != null && !instance.IsPaused)
		{
			if (KPlayerPrefs.HasKey(AudioOptionsScreen.AlwaysPlayAutomation) && KPlayerPrefs.GetInt(AudioOptionsScreen.AlwaysPlayAutomation) != 1 && OverlayScreen.Instance.GetMode() != OverlayModes.Logic.ID)
			{
				return;
			}
			string text = "Logic_Building_Toggle";
			if (!CameraController.Instance.IsAudibleSound(Grid.CellToPosCCC(this.cell, Grid.SceneLayer.BuildingFront)))
			{
				return;
			}
			LogicCircuitNetwork.LogicSoundPair logicSoundPair = new LogicCircuitNetwork.LogicSoundPair();
			Dictionary<int, LogicCircuitNetwork.LogicSoundPair> logicSoundRegister = LogicCircuitNetwork.logicSoundRegister;
			int id = networkForCell.id;
			if (!logicSoundRegister.ContainsKey(id))
			{
				logicSoundRegister.Add(id, logicSoundPair);
			}
			else
			{
				logicSoundPair.playedIndex = logicSoundRegister[id].playedIndex;
				logicSoundPair.lastPlayed = logicSoundRegister[id].lastPlayed;
			}
			if (logicSoundPair.playedIndex < 2)
			{
				logicSoundRegister[id].playedIndex = logicSoundPair.playedIndex + 1;
			}
			else
			{
				logicSoundRegister[id].playedIndex = 0;
				logicSoundRegister[id].lastPlayed = Time.time;
			}
			float num = (Time.time - logicSoundPair.lastPlayed) / 3f;
			EventInstance eventInstance = KFMOD.BeginOneShot(GlobalAssets.GetSound(text, false), Grid.CellToPos(this.cell), 1f);
			eventInstance.setParameterByName("logic_volumeModifer", num, false);
			eventInstance.setParameterByName("wireCount", (float)(networkForCell.WireCount % 24), false);
			eventInstance.setParameterByName("enabled", (float)new_value, false);
			KFMOD.EndOneShot(eventInstance);
		}
	}

	// Token: 0x06004865 RID: 18533 RVA: 0x001A1EBC File Offset: 0x001A00BC
	public void OnLogicNetworkConnectionChanged(bool connected)
	{
		if (this.onConnectionChanged != null)
		{
			this.onConnectionChanged(this.cell, connected);
		}
	}

	// Token: 0x04002FC5 RID: 12229
	private int cell;

	// Token: 0x04002FC6 RID: 12230
	private int value;

	// Token: 0x04002FC7 RID: 12231
	private Action<int, int> onValueChanged;

	// Token: 0x04002FC8 RID: 12232
	private Action<int, bool> onConnectionChanged;

	// Token: 0x04002FC9 RID: 12233
	private LogicPortSpriteType spriteType;
}
