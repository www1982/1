using System;
using System.Collections.Generic;
using FMOD.Studio;
using STRINGS;
using UnityEngine;

// Token: 0x020009B6 RID: 2486
public class LogicCircuitNetwork : UtilityNetwork
{
	// Token: 0x06004866 RID: 18534 RVA: 0x001A1ED8 File Offset: 0x001A00D8
	public override void AddItem(object item)
	{
		if (item is LogicWire)
		{
			LogicWire logicWire = (LogicWire)item;
			LogicWire.BitDepth maxBitDepth = logicWire.MaxBitDepth;
			List<LogicWire> list = this.wireGroups[(int)maxBitDepth];
			if (list == null)
			{
				list = new List<LogicWire>();
				this.wireGroups[(int)maxBitDepth] = list;
			}
			list.Add(logicWire);
			return;
		}
		if (item is ILogicEventReceiver)
		{
			ILogicEventReceiver logicEventReceiver = (ILogicEventReceiver)item;
			this.receivers.Add(logicEventReceiver);
			return;
		}
		if (item is ILogicEventSender)
		{
			ILogicEventSender logicEventSender = (ILogicEventSender)item;
			this.senders.Add(logicEventSender);
		}
	}

	// Token: 0x06004867 RID: 18535 RVA: 0x001A1F58 File Offset: 0x001A0158
	public override void RemoveItem(object item)
	{
		if (item is LogicWire)
		{
			LogicWire logicWire = (LogicWire)item;
			this.wireGroups[(int)logicWire.MaxBitDepth].Remove(logicWire);
			return;
		}
		if (item is ILogicEventReceiver)
		{
			ILogicEventReceiver logicEventReceiver = item as ILogicEventReceiver;
			this.receivers.Remove(logicEventReceiver);
			return;
		}
		if (item is ILogicEventSender)
		{
			ILogicEventSender logicEventSender = (ILogicEventSender)item;
			this.senders.Remove(logicEventSender);
		}
	}

	// Token: 0x06004868 RID: 18536 RVA: 0x001A1FC2 File Offset: 0x001A01C2
	public override void ConnectItem(object item)
	{
		if (item is ILogicEventReceiver)
		{
			((ILogicEventReceiver)item).OnLogicNetworkConnectionChanged(true);
			return;
		}
		if (item is ILogicEventSender)
		{
			((ILogicEventSender)item).OnLogicNetworkConnectionChanged(true);
		}
	}

	// Token: 0x06004869 RID: 18537 RVA: 0x001A1FED File Offset: 0x001A01ED
	public override void DisconnectItem(object item)
	{
		if (item is ILogicEventReceiver)
		{
			ILogicEventReceiver logicEventReceiver = item as ILogicEventReceiver;
			logicEventReceiver.ReceiveLogicEvent(0);
			logicEventReceiver.OnLogicNetworkConnectionChanged(false);
			return;
		}
		if (item is ILogicEventSender)
		{
			(item as ILogicEventSender).OnLogicNetworkConnectionChanged(false);
		}
	}

	// Token: 0x0600486A RID: 18538 RVA: 0x001A2020 File Offset: 0x001A0220
	public override void Reset(UtilityNetworkGridNode[] grid)
	{
		this.resetting = true;
		this.previousValue = -1;
		this.outputValue = 0;
		for (int i = 0; i < 2; i++)
		{
			List<LogicWire> list = this.wireGroups[i];
			if (list != null)
			{
				for (int j = 0; j < list.Count; j++)
				{
					LogicWire logicWire = list[j];
					if (logicWire != null)
					{
						int num = Grid.PosToCell(logicWire.transform.GetPosition());
						UtilityNetworkGridNode utilityNetworkGridNode = grid[num];
						utilityNetworkGridNode.networkIdx = -1;
						grid[num] = utilityNetworkGridNode;
					}
				}
				list.Clear();
			}
		}
		this.senders.Clear();
		this.receivers.Clear();
		this.resetting = false;
		this.RemoveOverloadedNotification();
	}

	// Token: 0x0600486B RID: 18539 RVA: 0x001A20D4 File Offset: 0x001A02D4
	public void UpdateLogicValue()
	{
		if (this.resetting)
		{
			return;
		}
		this.previousValue = this.outputValue;
		this.outputValue = 0;
		foreach (ILogicEventSender logicEventSender in this.senders)
		{
			logicEventSender.LogicTick();
		}
		foreach (ILogicEventSender logicEventSender2 in this.senders)
		{
			int logicValue = logicEventSender2.GetLogicValue();
			this.outputValue |= logicValue;
		}
	}

	// Token: 0x0600486C RID: 18540 RVA: 0x001A2190 File Offset: 0x001A0390
	public int GetBitsUsed()
	{
		int num;
		if (this.outputValue > 1)
		{
			num = 4;
		}
		else
		{
			num = 1;
		}
		return num;
	}

	// Token: 0x0600486D RID: 18541 RVA: 0x001A21AF File Offset: 0x001A03AF
	public bool IsBitActive(int bit)
	{
		return (this.OutputValue & (1 << bit)) > 0;
	}

	// Token: 0x0600486E RID: 18542 RVA: 0x001A21C1 File Offset: 0x001A03C1
	public static bool IsBitActive(int bit, int value)
	{
		return (value & (1 << bit)) > 0;
	}

	// Token: 0x0600486F RID: 18543 RVA: 0x001A21CE File Offset: 0x001A03CE
	public static int GetBitValue(int bit, int value)
	{
		return value & (1 << bit);
	}

	// Token: 0x06004870 RID: 18544 RVA: 0x001A21D8 File Offset: 0x001A03D8
	public void SendLogicEvents(bool force_send, int id)
	{
		if (this.resetting)
		{
			return;
		}
		if (this.outputValue != this.previousValue || force_send)
		{
			foreach (ILogicEventReceiver logicEventReceiver in this.receivers)
			{
				logicEventReceiver.ReceiveLogicEvent(this.outputValue);
			}
			if (!force_send)
			{
				this.TriggerAudio((this.previousValue >= 0) ? this.previousValue : 0, id);
			}
		}
	}

	// Token: 0x06004871 RID: 18545 RVA: 0x001A2268 File Offset: 0x001A0468
	private void TriggerAudio(int old_value, int id)
	{
		SpeedControlScreen instance = SpeedControlScreen.Instance;
		if (old_value != this.outputValue && instance != null && !instance.IsPaused)
		{
			int num = 0;
			GridArea visibleArea = GridVisibleArea.GetVisibleArea();
			List<LogicWire> list = new List<LogicWire>();
			for (int i = 0; i < 2; i++)
			{
				List<LogicWire> list2 = this.wireGroups[i];
				if (list2 != null)
				{
					for (int j = 0; j < list2.Count; j++)
					{
						num++;
						if (visibleArea.Min <= list2[j].transform.GetPosition() && list2[j].transform.GetPosition() <= visibleArea.Max)
						{
							list.Add(list2[j]);
						}
					}
				}
			}
			if (list.Count > 0)
			{
				int num2 = Mathf.CeilToInt((float)(list.Count / 2));
				if (list[num2] != null)
				{
					Vector3 position = list[num2].transform.GetPosition();
					position.z = 0f;
					string text = "Logic_Circuit_Toggle";
					LogicCircuitNetwork.LogicSoundPair logicSoundPair = new LogicCircuitNetwork.LogicSoundPair();
					if (!LogicCircuitNetwork.logicSoundRegister.ContainsKey(id))
					{
						LogicCircuitNetwork.logicSoundRegister.Add(id, logicSoundPair);
					}
					else
					{
						logicSoundPair.playedIndex = LogicCircuitNetwork.logicSoundRegister[id].playedIndex;
						logicSoundPair.lastPlayed = LogicCircuitNetwork.logicSoundRegister[id].lastPlayed;
					}
					if (logicSoundPair.playedIndex < 2)
					{
						LogicCircuitNetwork.logicSoundRegister[id].playedIndex = logicSoundPair.playedIndex + 1;
					}
					else
					{
						LogicCircuitNetwork.logicSoundRegister[id].playedIndex = 0;
						LogicCircuitNetwork.logicSoundRegister[id].lastPlayed = Time.time;
					}
					float num3 = (Time.time - logicSoundPair.lastPlayed) / 3f;
					EventInstance eventInstance = KFMOD.BeginOneShot(GlobalAssets.GetSound(text, false), position, 1f);
					eventInstance.setParameterByName("logic_volumeModifer", num3, false);
					eventInstance.setParameterByName("wireCount", (float)(num % 24), false);
					eventInstance.setParameterByName("enabled", (float)this.outputValue, false);
					KFMOD.EndOneShot(eventInstance);
				}
			}
		}
	}

	// Token: 0x06004872 RID: 18546 RVA: 0x001A24A0 File Offset: 0x001A06A0
	public void UpdateOverloadTime(float dt, int bits_used)
	{
		bool flag = false;
		List<LogicWire> list = null;
		List<LogicUtilityNetworkLink> list2 = null;
		for (int i = 0; i < 2; i++)
		{
			List<LogicWire> list3 = this.wireGroups[i];
			List<LogicUtilityNetworkLink> list4 = this.relevantBridges[i];
			float num = (float)LogicWire.GetBitDepthAsInt((LogicWire.BitDepth)i);
			if ((float)bits_used > num && ((list4 != null && list4.Count > 0) || (list3 != null && list3.Count > 0)))
			{
				flag = true;
				list = list3;
				list2 = list4;
				break;
			}
		}
		if (list != null)
		{
			list.RemoveAll((LogicWire x) => x == null);
		}
		if (list2 != null)
		{
			list2.RemoveAll((LogicUtilityNetworkLink x) => x == null);
		}
		if (flag)
		{
			this.timeOverloaded += dt;
			if (this.timeOverloaded > 6f)
			{
				this.timeOverloaded = 0f;
				if (this.targetOverloadedWire == null)
				{
					if (list2 != null && list2.Count > 0)
					{
						int num2 = global::UnityEngine.Random.Range(0, list2.Count);
						this.targetOverloadedWire = list2[num2].gameObject;
					}
					else if (list != null && list.Count > 0)
					{
						int num3 = global::UnityEngine.Random.Range(0, list.Count);
						this.targetOverloadedWire = list[num3].gameObject;
					}
				}
				if (this.targetOverloadedWire != null)
				{
					this.targetOverloadedWire.Trigger(-794517298, new BuildingHP.DamageSourceInfo
					{
						damage = 1,
						source = BUILDINGS.DAMAGESOURCES.LOGIC_CIRCUIT_OVERLOADED,
						popString = UI.GAMEOBJECTEFFECTS.DAMAGE_POPS.LOGIC_CIRCUIT_OVERLOADED,
						takeDamageEffect = SpawnFXHashes.BuildingLogicOverload,
						fullDamageEffectName = "logic_ribbon_damage_kanim",
						statusItemID = Db.Get().BuildingStatusItems.LogicOverloaded.Id
					});
				}
				if (this.overloadedNotification == null)
				{
					this.timeOverloadNotificationDisplayed = 0f;
					this.overloadedNotification = new Notification(MISC.NOTIFICATIONS.LOGIC_CIRCUIT_OVERLOADED.NAME, NotificationType.BadMinor, null, null, true, 0f, null, null, this.targetOverloadedWire.transform, true, false, false);
					Game.Instance.FindOrAdd<Notifier>().Add(this.overloadedNotification, "");
					return;
				}
			}
		}
		else
		{
			this.timeOverloaded = Mathf.Max(0f, this.timeOverloaded - dt * 0.95f);
			this.timeOverloadNotificationDisplayed += dt;
			if (this.timeOverloadNotificationDisplayed > 5f)
			{
				this.RemoveOverloadedNotification();
			}
		}
	}

	// Token: 0x06004873 RID: 18547 RVA: 0x001A271B File Offset: 0x001A091B
	private void RemoveOverloadedNotification()
	{
		if (this.overloadedNotification != null)
		{
			Game.Instance.FindOrAdd<Notifier>().Remove(this.overloadedNotification);
			this.overloadedNotification = null;
		}
	}

	// Token: 0x06004874 RID: 18548 RVA: 0x001A2744 File Offset: 0x001A0944
	public void UpdateRelevantBridges(List<LogicUtilityNetworkLink>[] bridgeGroups)
	{
		LogicCircuitManager logicCircuitManager = Game.Instance.logicCircuitManager;
		for (int i = 0; i < bridgeGroups.Length; i++)
		{
			if (this.relevantBridges[i] != null)
			{
				this.relevantBridges[i].Clear();
			}
			for (int j = 0; j < bridgeGroups[i].Count; j++)
			{
				if (logicCircuitManager.GetNetworkForCell(bridgeGroups[i][j].cell_one) == this || logicCircuitManager.GetNetworkForCell(bridgeGroups[i][j].cell_two) == this)
				{
					if (this.relevantBridges[i] == null)
					{
						this.relevantBridges[i] = new List<LogicUtilityNetworkLink>();
					}
					this.relevantBridges[i].Add(bridgeGroups[i][j]);
				}
			}
		}
	}

	// Token: 0x1700050C RID: 1292
	// (get) Token: 0x06004875 RID: 18549 RVA: 0x001A27F5 File Offset: 0x001A09F5
	public int OutputValue
	{
		get
		{
			return this.outputValue;
		}
	}

	// Token: 0x1700050D RID: 1293
	// (get) Token: 0x06004876 RID: 18550 RVA: 0x001A2800 File Offset: 0x001A0A00
	public int WireCount
	{
		get
		{
			int num = 0;
			for (int i = 0; i < 2; i++)
			{
				if (this.wireGroups[i] != null)
				{
					num += this.wireGroups[i].Count;
				}
			}
			return num;
		}
	}

	// Token: 0x1700050E RID: 1294
	// (get) Token: 0x06004877 RID: 18551 RVA: 0x001A2836 File Offset: 0x001A0A36
	public List<ILogicEventSender> Senders
	{
		get
		{
			return this.senders;
		}
	}

	// Token: 0x1700050F RID: 1295
	// (get) Token: 0x06004878 RID: 18552 RVA: 0x001A283E File Offset: 0x001A0A3E
	public List<ILogicEventReceiver> Receivers
	{
		get
		{
			return this.receivers;
		}
	}

	// Token: 0x04002FCA RID: 12234
	private List<LogicWire>[] wireGroups = new List<LogicWire>[2];

	// Token: 0x04002FCB RID: 12235
	private List<LogicUtilityNetworkLink>[] relevantBridges = new List<LogicUtilityNetworkLink>[2];

	// Token: 0x04002FCC RID: 12236
	private List<ILogicEventReceiver> receivers = new List<ILogicEventReceiver>();

	// Token: 0x04002FCD RID: 12237
	private List<ILogicEventSender> senders = new List<ILogicEventSender>();

	// Token: 0x04002FCE RID: 12238
	private int previousValue = -1;

	// Token: 0x04002FCF RID: 12239
	private int outputValue;

	// Token: 0x04002FD0 RID: 12240
	private bool resetting;

	// Token: 0x04002FD1 RID: 12241
	public static float logicSoundLastPlayedTime = 0f;

	// Token: 0x04002FD2 RID: 12242
	private const float MIN_OVERLOAD_TIME_FOR_DAMAGE = 6f;

	// Token: 0x04002FD3 RID: 12243
	private const float MIN_OVERLOAD_NOTIFICATION_DISPLAY_TIME = 5f;

	// Token: 0x04002FD4 RID: 12244
	public const int VALID_LOGIC_SIGNAL_MASK = 15;

	// Token: 0x04002FD5 RID: 12245
	public const int UNINITIALIZED_LOGIC_STATE = -16;

	// Token: 0x04002FD6 RID: 12246
	private GameObject targetOverloadedWire;

	// Token: 0x04002FD7 RID: 12247
	private float timeOverloaded;

	// Token: 0x04002FD8 RID: 12248
	private float timeOverloadNotificationDisplayed;

	// Token: 0x04002FD9 RID: 12249
	private Notification overloadedNotification;

	// Token: 0x04002FDA RID: 12250
	public static Dictionary<int, LogicCircuitNetwork.LogicSoundPair> logicSoundRegister = new Dictionary<int, LogicCircuitNetwork.LogicSoundPair>();

	// Token: 0x020019C0 RID: 6592
	public class LogicSoundPair
	{
		// Token: 0x04007DA3 RID: 32163
		public int playedIndex;

		// Token: 0x04007DA4 RID: 32164
		public float lastPlayed;
	}
}
