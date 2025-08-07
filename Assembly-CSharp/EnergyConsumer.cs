using System;
using System.Collections.Generic;
using System.Diagnostics;
using FMOD.Studio;
using FMODUnity;
using KSerialization;
using UnityEngine;

// Token: 0x020008E5 RID: 2277
[SerializationConfig(MemberSerialization.OptIn)]
[DebuggerDisplay("{name} {WattsUsed}W")]
[AddComponentMenu("KMonoBehaviour/scripts/EnergyConsumer")]
public class EnergyConsumer : KMonoBehaviour, ISaveLoadable, IEnergyConsumer, ICircuitConnected, IGameObjectEffectDescriptor
{
	// Token: 0x17000473 RID: 1139
	// (get) Token: 0x06003F6B RID: 16235 RVA: 0x00164C4B File Offset: 0x00162E4B
	public int PowerSortOrder
	{
		get
		{
			return this.powerSortOrder;
		}
	}

	// Token: 0x17000474 RID: 1140
	// (get) Token: 0x06003F6C RID: 16236 RVA: 0x00164C53 File Offset: 0x00162E53
	// (set) Token: 0x06003F6D RID: 16237 RVA: 0x00164C5B File Offset: 0x00162E5B
	public int PowerCell { get; private set; }

	// Token: 0x17000475 RID: 1141
	// (get) Token: 0x06003F6E RID: 16238 RVA: 0x00164C64 File Offset: 0x00162E64
	public bool HasWire
	{
		get
		{
			return Grid.Objects[this.PowerCell, 26] != null;
		}
	}

	// Token: 0x17000476 RID: 1142
	// (get) Token: 0x06003F6F RID: 16239 RVA: 0x00164C7E File Offset: 0x00162E7E
	// (set) Token: 0x06003F70 RID: 16240 RVA: 0x00164C90 File Offset: 0x00162E90
	public virtual bool IsPowered
	{
		get
		{
			return this.operational.GetFlag(EnergyConsumer.PoweredFlag);
		}
		protected set
		{
			this.operational.SetFlag(EnergyConsumer.PoweredFlag, value);
		}
	}

	// Token: 0x17000477 RID: 1143
	// (get) Token: 0x06003F71 RID: 16241 RVA: 0x00164CA3 File Offset: 0x00162EA3
	public bool IsConnected
	{
		get
		{
			return this.CircuitID != ushort.MaxValue;
		}
	}

	// Token: 0x17000478 RID: 1144
	// (get) Token: 0x06003F72 RID: 16242 RVA: 0x00164CB5 File Offset: 0x00162EB5
	public string Name
	{
		get
		{
			return this.selectable.GetName();
		}
	}

	// Token: 0x17000479 RID: 1145
	// (get) Token: 0x06003F73 RID: 16243 RVA: 0x00164CC2 File Offset: 0x00162EC2
	// (set) Token: 0x06003F74 RID: 16244 RVA: 0x00164CCA File Offset: 0x00162ECA
	public bool IsVirtual { get; private set; }

	// Token: 0x1700047A RID: 1146
	// (get) Token: 0x06003F75 RID: 16245 RVA: 0x00164CD3 File Offset: 0x00162ED3
	// (set) Token: 0x06003F76 RID: 16246 RVA: 0x00164CDB File Offset: 0x00162EDB
	public object VirtualCircuitKey { get; private set; }

	// Token: 0x1700047B RID: 1147
	// (get) Token: 0x06003F77 RID: 16247 RVA: 0x00164CE4 File Offset: 0x00162EE4
	// (set) Token: 0x06003F78 RID: 16248 RVA: 0x00164CEC File Offset: 0x00162EEC
	public ushort CircuitID { get; private set; }

	// Token: 0x1700047C RID: 1148
	// (get) Token: 0x06003F79 RID: 16249 RVA: 0x00164CF5 File Offset: 0x00162EF5
	// (set) Token: 0x06003F7A RID: 16250 RVA: 0x00164CFD File Offset: 0x00162EFD
	public float BaseWattageRating
	{
		get
		{
			return this._BaseWattageRating;
		}
		set
		{
			this._BaseWattageRating = value;
		}
	}

	// Token: 0x1700047D RID: 1149
	// (get) Token: 0x06003F7B RID: 16251 RVA: 0x00164D06 File Offset: 0x00162F06
	public float WattsUsed
	{
		get
		{
			if (this.operational.IsActive)
			{
				return this.BaseWattageRating;
			}
			return 0f;
		}
	}

	// Token: 0x1700047E RID: 1150
	// (get) Token: 0x06003F7C RID: 16252 RVA: 0x00164D21 File Offset: 0x00162F21
	public float WattsNeededWhenActive
	{
		get
		{
			return this.building.Def.EnergyConsumptionWhenActive;
		}
	}

	// Token: 0x06003F7D RID: 16253 RVA: 0x00164D33 File Offset: 0x00162F33
	protected override void OnPrefabInit()
	{
		this.CircuitID = ushort.MaxValue;
		this.IsPowered = false;
		this.BaseWattageRating = this.building.Def.EnergyConsumptionWhenActive;
	}

	// Token: 0x06003F7E RID: 16254 RVA: 0x00164D60 File Offset: 0x00162F60
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Components.EnergyConsumers.Add(this);
		Building component = base.GetComponent<Building>();
		this.PowerCell = component.GetPowerInputCell();
		Game.Instance.circuitManager.Connect(this);
		Game.Instance.energySim.AddEnergyConsumer(this);
	}

	// Token: 0x06003F7F RID: 16255 RVA: 0x00164DB1 File Offset: 0x00162FB1
	protected override void OnCleanUp()
	{
		Game.Instance.energySim.RemoveEnergyConsumer(this);
		Game.Instance.circuitManager.Disconnect(this, true);
		Components.EnergyConsumers.Remove(this);
		base.OnCleanUp();
	}

	// Token: 0x06003F80 RID: 16256 RVA: 0x00164DE5 File Offset: 0x00162FE5
	public virtual void EnergySim200ms(float dt)
	{
		this.CircuitID = Game.Instance.circuitManager.GetCircuitID(this);
		if (!this.IsConnected)
		{
			this.IsPowered = false;
		}
		this.circuitOverloadTime = Mathf.Max(0f, this.circuitOverloadTime - dt);
	}

	// Token: 0x06003F81 RID: 16257 RVA: 0x00164E24 File Offset: 0x00163024
	public virtual void SetConnectionStatus(CircuitManager.ConnectionStatus connection_status)
	{
		switch (connection_status)
		{
		case CircuitManager.ConnectionStatus.NotConnected:
			this.IsPowered = false;
			return;
		case CircuitManager.ConnectionStatus.Unpowered:
			if (this.IsPowered && base.GetComponent<Battery>() == null)
			{
				this.IsPowered = false;
				this.circuitOverloadTime = 6f;
				this.PlayCircuitSound("overdraw");
				return;
			}
			break;
		case CircuitManager.ConnectionStatus.Powered:
			if (!this.IsPowered && this.circuitOverloadTime <= 0f)
			{
				this.IsPowered = true;
				this.PlayCircuitSound("powered");
			}
			break;
		default:
			return;
		}
	}

	// Token: 0x06003F82 RID: 16258 RVA: 0x00164EA8 File Offset: 0x001630A8
	protected void PlayCircuitSound(string state)
	{
		EventReference eventReference;
		if (state == "powered")
		{
			eventReference = Sounds.Instance.BuildingPowerOnMigrated;
		}
		else if (state == "overdraw")
		{
			eventReference = Sounds.Instance.ElectricGridOverloadMigrated;
		}
		else
		{
			eventReference = default(EventReference);
			global::Debug.Log("Invalid state for sound in EnergyConsumer.");
		}
		if (!CameraController.Instance.IsAudibleSound(base.transform.GetPosition()))
		{
			return;
		}
		float num;
		if (!this.lastTimeSoundPlayed.TryGetValue(state, out num))
		{
			num = 0f;
		}
		float num2 = (Time.time - num) / this.soundDecayTime;
		Vector3 position = base.transform.GetPosition();
		position.z = 0f;
		FMOD.Studio.EventInstance eventInstance = KFMOD.BeginOneShot(eventReference, CameraController.Instance.GetVerticallyScaledPosition(position, false), 1f);
		eventInstance.setParameterByName("timeSinceLast", num2, false);
		KFMOD.EndOneShot(eventInstance);
		this.lastTimeSoundPlayed[state] = Time.time;
	}

	// Token: 0x06003F83 RID: 16259 RVA: 0x00164F96 File Offset: 0x00163196
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		return null;
	}

	// Token: 0x04002768 RID: 10088
	[MyCmpReq]
	private Building building;

	// Token: 0x04002769 RID: 10089
	[MyCmpGet]
	protected Operational operational;

	// Token: 0x0400276A RID: 10090
	[MyCmpGet]
	private KSelectable selectable;

	// Token: 0x0400276B RID: 10091
	[SerializeField]
	public int powerSortOrder;

	// Token: 0x0400276D RID: 10093
	[Serialize]
	protected float circuitOverloadTime;

	// Token: 0x0400276E RID: 10094
	public static readonly Operational.Flag PoweredFlag = new Operational.Flag("powered", Operational.Flag.Type.Requirement);

	// Token: 0x0400276F RID: 10095
	private Dictionary<string, float> lastTimeSoundPlayed = new Dictionary<string, float>();

	// Token: 0x04002770 RID: 10096
	private float soundDecayTime = 10f;

	// Token: 0x04002774 RID: 10100
	private float _BaseWattageRating;
}
