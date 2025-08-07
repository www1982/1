using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using TUNING;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020005B3 RID: 1459
public class EntityCellVisualizer : KMonoBehaviour
{
	// Token: 0x17000151 RID: 337
	// (get) Token: 0x0600219A RID: 8602 RVA: 0x000C2255 File Offset: 0x000C0455
	public BuildingCellVisualizerResources Resources
	{
		get
		{
			return BuildingCellVisualizerResources.Instance();
		}
	}

	// Token: 0x17000152 RID: 338
	// (get) Token: 0x0600219B RID: 8603 RVA: 0x000C225C File Offset: 0x000C045C
	protected int CenterCell
	{
		get
		{
			return Grid.PosToCell(this);
		}
	}

	// Token: 0x0600219C RID: 8604 RVA: 0x000C2264 File Offset: 0x000C0464
	protected virtual void DefinePorts()
	{
	}

	// Token: 0x0600219D RID: 8605 RVA: 0x000C2266 File Offset: 0x000C0466
	protected override void OnPrefabInit()
	{
		this.LoadDiseaseIcon();
		this.DefinePorts();
	}

	// Token: 0x0600219E RID: 8606 RVA: 0x000C2274 File Offset: 0x000C0474
	public void ConnectedEventWithDelay(float delay, int connectionCount, int cell, string soundName)
	{
		base.StartCoroutine(this.ConnectedDelay(delay, connectionCount, cell, soundName));
	}

	// Token: 0x0600219F RID: 8607 RVA: 0x000C2288 File Offset: 0x000C0488
	private IEnumerator ConnectedDelay(float delay, int connectionCount, int cell, string soundName)
	{
		float startTime = Time.realtimeSinceStartup;
		float currentTime = startTime;
		while (currentTime < startTime + delay)
		{
			currentTime += Time.unscaledDeltaTime;
			yield return SequenceUtil.WaitForEndOfFrame;
		}
		this.ConnectedEvent(cell);
		string sound = GlobalAssets.GetSound(soundName, false);
		if (sound != null)
		{
			Vector3 position = base.transform.GetPosition();
			position.z = 0f;
			EventInstance eventInstance = SoundEvent.BeginOneShot(sound, position, 1f, false);
			eventInstance.setParameterByName("connectedCount", (float)connectionCount, false);
			SoundEvent.EndOneShot(eventInstance);
		}
		yield break;
	}

	// Token: 0x060021A0 RID: 8608 RVA: 0x000C22B4 File Offset: 0x000C04B4
	private int ComputeCell(CellOffset cellOffset)
	{
		CellOffset cellOffset2 = cellOffset;
		if (this.rotatable != null)
		{
			cellOffset2 = this.rotatable.GetRotatedCellOffset(cellOffset);
		}
		return Grid.OffsetCell(Grid.PosToCell(base.gameObject), cellOffset2);
	}

	// Token: 0x060021A1 RID: 8609 RVA: 0x000C22F0 File Offset: 0x000C04F0
	public void ConnectedEvent(int cell)
	{
		foreach (EntityCellVisualizer.PortEntry portEntry in this.ports)
		{
			if (this.ComputeCell(portEntry.cellOffset) == cell && portEntry.visualizer != null)
			{
				SizePulse pulse = portEntry.visualizer.AddComponent<SizePulse>();
				pulse.speed = 20f;
				pulse.multiplier = 0.75f;
				pulse.updateWhenPaused = true;
				SizePulse pulse2 = pulse;
				pulse2.onComplete = (global::System.Action)Delegate.Combine(pulse2.onComplete, new global::System.Action(delegate
				{
					global::UnityEngine.Object.Destroy(pulse);
				}));
			}
		}
	}

	// Token: 0x060021A2 RID: 8610 RVA: 0x000C23CC File Offset: 0x000C05CC
	public virtual void AddPort(EntityCellVisualizer.Ports type, CellOffset cell)
	{
		this.AddPort(type, cell, Color.white);
	}

	// Token: 0x060021A3 RID: 8611 RVA: 0x000C23DB File Offset: 0x000C05DB
	public virtual void AddPort(EntityCellVisualizer.Ports type, CellOffset cell, Color tint)
	{
		this.AddPort(type, cell, tint, tint, 1.5f, false);
	}

	// Token: 0x060021A4 RID: 8612 RVA: 0x000C23ED File Offset: 0x000C05ED
	public virtual void AddPort(EntityCellVisualizer.Ports type, CellOffset cell, Color connectedTint, Color disconnectedTint, float scale = 1.5f, bool hideBG = false)
	{
		this.ports.Add(new EntityCellVisualizer.PortEntry(type, cell, connectedTint, disconnectedTint, scale, hideBG));
		this.addedPorts |= type;
	}

	// Token: 0x060021A5 RID: 8613 RVA: 0x000C2418 File Offset: 0x000C0618
	protected override void OnCleanUp()
	{
		foreach (EntityCellVisualizer.PortEntry portEntry in this.ports)
		{
			if (portEntry.visualizer != null)
			{
				global::UnityEngine.Object.Destroy(portEntry.visualizer);
			}
		}
		GameObject[] array = new GameObject[] { this.switchVisualizer, this.wireVisualizerAlpha, this.wireVisualizerBeta };
		for (int i = 0; i < array.Length; i++)
		{
			global::UnityEngine.Object.Destroy(array[i]);
		}
		base.OnCleanUp();
	}

	// Token: 0x060021A6 RID: 8614 RVA: 0x000C24BC File Offset: 0x000C06BC
	protected override void OnCmpEnable()
	{
		base.OnCmpEnable();
		if (this.icons == null)
		{
			this.icons = new Dictionary<GameObject, Image>();
		}
		Components.EntityCellVisualizers.Add(this);
	}

	// Token: 0x060021A7 RID: 8615 RVA: 0x000C24E2 File Offset: 0x000C06E2
	protected override void OnCmpDisable()
	{
		base.OnCmpDisable();
		Components.EntityCellVisualizers.Remove(this);
	}

	// Token: 0x060021A8 RID: 8616 RVA: 0x000C24F8 File Offset: 0x000C06F8
	public void DrawIcons(HashedString mode)
	{
		EntityCellVisualizer.Ports ports = (EntityCellVisualizer.Ports)0;
		if (base.gameObject.GetMyWorldId() != ClusterManager.Instance.activeWorldId)
		{
			ports = (EntityCellVisualizer.Ports)0;
		}
		else if (mode == OverlayModes.Power.ID)
		{
			ports = EntityCellVisualizer.Ports.PowerIn | EntityCellVisualizer.Ports.PowerOut;
		}
		else if (mode == OverlayModes.GasConduits.ID)
		{
			ports = EntityCellVisualizer.Ports.GasIn | EntityCellVisualizer.Ports.GasOut;
		}
		else if (mode == OverlayModes.LiquidConduits.ID)
		{
			ports = EntityCellVisualizer.Ports.LiquidIn | EntityCellVisualizer.Ports.LiquidOut;
		}
		else if (mode == OverlayModes.SolidConveyor.ID)
		{
			ports = EntityCellVisualizer.Ports.SolidIn | EntityCellVisualizer.Ports.SolidOut;
		}
		else if (mode == OverlayModes.Radiation.ID)
		{
			ports = EntityCellVisualizer.Ports.HighEnergyParticleIn | EntityCellVisualizer.Ports.HighEnergyParticleOut;
		}
		else if (mode == OverlayModes.Disease.ID)
		{
			ports = EntityCellVisualizer.Ports.DiseaseIn | EntityCellVisualizer.Ports.DiseaseOut;
		}
		else if (mode == OverlayModes.Temperature.ID || mode == OverlayModes.HeatFlow.ID)
		{
			ports = EntityCellVisualizer.Ports.HeatSource | EntityCellVisualizer.Ports.HeatSink;
		}
		bool flag = false;
		foreach (EntityCellVisualizer.PortEntry portEntry in this.ports)
		{
			if ((portEntry.type & ports) == portEntry.type)
			{
				this.DrawUtilityIcon(portEntry);
				flag = true;
			}
			else if (portEntry.visualizer != null && portEntry.visualizer.activeInHierarchy)
			{
				portEntry.visualizer.SetActive(false);
			}
		}
		if (mode == OverlayModes.Power.ID)
		{
			if (!flag)
			{
				Switch component = base.GetComponent<Switch>();
				if (component != null)
				{
					int num = Grid.PosToCell(base.transform.GetPosition());
					Color32 color = (component.IsHandlerOn() ? this.Resources.switchColor : this.Resources.switchOffColor);
					this.DrawUtilityIcon(num, this.Resources.switchIcon, ref this.switchVisualizer, color, 1f, false);
					return;
				}
				WireUtilityNetworkLink component2 = base.GetComponent<WireUtilityNetworkLink>();
				if (component2 != null)
				{
					int num2;
					int num3;
					component2.GetCells(out num2, out num3);
					this.DrawUtilityIcon(num2, (Game.Instance.circuitManager.GetCircuitID(num2) == ushort.MaxValue) ? this.Resources.electricityBridgeIcon : this.Resources.electricityConnectedIcon, ref this.wireVisualizerAlpha, this.Resources.electricityInputColor, 1f, false);
					this.DrawUtilityIcon(num3, (Game.Instance.circuitManager.GetCircuitID(num3) == ushort.MaxValue) ? this.Resources.electricityBridgeIcon : this.Resources.electricityConnectedIcon, ref this.wireVisualizerBeta, this.Resources.electricityInputColor, 1f, false);
					return;
				}
			}
		}
		else
		{
			foreach (GameObject gameObject in new GameObject[] { this.switchVisualizer, this.wireVisualizerAlpha, this.wireVisualizerBeta })
			{
				if (gameObject != null && gameObject.activeInHierarchy)
				{
					gameObject.SetActive(false);
				}
			}
		}
	}

	// Token: 0x060021A9 RID: 8617 RVA: 0x000C27DC File Offset: 0x000C09DC
	private Sprite GetSpriteForPortType(EntityCellVisualizer.Ports type, bool connected)
	{
		if (type <= EntityCellVisualizer.Ports.SolidOut)
		{
			if (type <= EntityCellVisualizer.Ports.LiquidIn)
			{
				switch (type)
				{
				case EntityCellVisualizer.Ports.PowerIn:
					if (!connected)
					{
						return this.Resources.electricityInputIcon;
					}
					return this.Resources.electricityBridgeConnectedIcon;
				case EntityCellVisualizer.Ports.PowerOut:
					if (!connected)
					{
						return this.Resources.electricityOutputIcon;
					}
					return this.Resources.electricityBridgeConnectedIcon;
				case EntityCellVisualizer.Ports.PowerIn | EntityCellVisualizer.Ports.PowerOut:
					break;
				case EntityCellVisualizer.Ports.GasIn:
					return this.Resources.gasInputIcon;
				default:
					if (type == EntityCellVisualizer.Ports.GasOut)
					{
						return this.Resources.gasOutputIcon;
					}
					if (type == EntityCellVisualizer.Ports.LiquidIn)
					{
						return this.Resources.liquidInputIcon;
					}
					break;
				}
			}
			else
			{
				if (type == EntityCellVisualizer.Ports.LiquidOut)
				{
					return this.Resources.liquidOutputIcon;
				}
				if (type == EntityCellVisualizer.Ports.SolidIn)
				{
					return this.Resources.liquidInputIcon;
				}
				if (type == EntityCellVisualizer.Ports.SolidOut)
				{
					return this.Resources.liquidOutputIcon;
				}
			}
		}
		else if (type <= EntityCellVisualizer.Ports.DiseaseIn)
		{
			if (type == EntityCellVisualizer.Ports.HighEnergyParticleIn)
			{
				return this.Resources.highEnergyParticleInputIcon;
			}
			if (type == EntityCellVisualizer.Ports.HighEnergyParticleOut)
			{
				return this.GetIconForHighEnergyOutput();
			}
			if (type == EntityCellVisualizer.Ports.DiseaseIn)
			{
				return this.diseaseSourceSprite;
			}
		}
		else
		{
			if (type == EntityCellVisualizer.Ports.DiseaseOut)
			{
				return this.diseaseSourceSprite;
			}
			if (type == EntityCellVisualizer.Ports.HeatSource)
			{
				return this.Resources.heatSourceIcon;
			}
			if (type == EntityCellVisualizer.Ports.HeatSink)
			{
				return this.Resources.heatSinkIcon;
			}
		}
		return null;
	}

	// Token: 0x060021AA RID: 8618 RVA: 0x000C2950 File Offset: 0x000C0B50
	protected virtual void DrawUtilityIcon(EntityCellVisualizer.PortEntry port)
	{
		int num = this.ComputeCell(port.cellOffset);
		bool flag = true;
		bool flag2 = true;
		EntityCellVisualizer.Ports type = port.type;
		if (type <= EntityCellVisualizer.Ports.GasOut)
		{
			if (type - EntityCellVisualizer.Ports.PowerIn > 1)
			{
				if (type == EntityCellVisualizer.Ports.GasIn || type == EntityCellVisualizer.Ports.GasOut)
				{
					flag = null != Grid.Objects[num, 12];
				}
			}
			else
			{
				bool flag3 = base.GetComponent<Building>() as BuildingPreview != null;
				BuildingEnabledButton component = base.GetComponent<BuildingEnabledButton>();
				flag2 = !flag3 && Game.Instance.circuitManager.GetCircuitID(num) != ushort.MaxValue;
				flag = flag3 || component == null || component.IsEnabled;
			}
		}
		else if (type <= EntityCellVisualizer.Ports.LiquidOut)
		{
			if (type == EntityCellVisualizer.Ports.LiquidIn || type == EntityCellVisualizer.Ports.LiquidOut)
			{
				flag = null != Grid.Objects[num, 16];
			}
		}
		else if (type == EntityCellVisualizer.Ports.SolidIn || type == EntityCellVisualizer.Ports.SolidOut)
		{
			flag = null != Grid.Objects[num, 20];
		}
		this.DrawUtilityIcon(num, this.GetSpriteForPortType(port.type, flag2), ref port.visualizer, flag ? port.connectedTint : port.disconnectedTint, port.scale, port.hideBG);
	}

	// Token: 0x060021AB RID: 8619 RVA: 0x000C2A8C File Offset: 0x000C0C8C
	protected virtual void LoadDiseaseIcon()
	{
		DiseaseVisualization.Info info = Assets.instance.DiseaseVisualization.GetInfo(this.DiseaseCellVisName);
		if (info.name != null)
		{
			this.diseaseSourceSprite = Assets.instance.DiseaseVisualization.overlaySprite;
			this.diseaseSourceColour = GlobalAssets.Instance.colorSet.GetColorByName(info.overlayColourName);
		}
	}

	// Token: 0x060021AC RID: 8620 RVA: 0x000C2AEC File Offset: 0x000C0CEC
	protected virtual Sprite GetIconForHighEnergyOutput()
	{
		IHighEnergyParticleDirection component = base.GetComponent<IHighEnergyParticleDirection>();
		Sprite sprite = this.Resources.highEnergyParticleOutputIcons[0];
		if (component != null)
		{
			int directionIndex = EightDirectionUtil.GetDirectionIndex(component.Direction);
			sprite = this.Resources.highEnergyParticleOutputIcons[directionIndex];
		}
		return sprite;
	}

	// Token: 0x060021AD RID: 8621 RVA: 0x000C2B2C File Offset: 0x000C0D2C
	private void DrawUtilityIcon(int cell, Sprite icon_img, ref GameObject visualizerObj, Color tint, float scaleMultiplier = 1.5f, bool hideBG = false)
	{
		Vector3 vector = Grid.CellToPosCCC(cell, Grid.SceneLayer.Building);
		if (visualizerObj == null)
		{
			visualizerObj = global::Util.KInstantiate(Assets.UIPrefabs.ResourceVisualizer, GameScreenManager.Instance.worldSpaceCanvas, null);
			visualizerObj.transform.SetAsFirstSibling();
			this.icons.Add(visualizerObj, visualizerObj.transform.GetChild(0).GetComponent<Image>());
		}
		if (!visualizerObj.gameObject.activeInHierarchy)
		{
			visualizerObj.gameObject.SetActive(true);
		}
		visualizerObj.GetComponent<Image>().enabled = !hideBG;
		this.icons[visualizerObj].raycastTarget = this.enableRaycast;
		this.icons[visualizerObj].sprite = icon_img;
		visualizerObj.transform.GetChild(0).gameObject.GetComponent<Image>().color = tint;
		visualizerObj.transform.SetPosition(vector);
		if (visualizerObj.GetComponent<SizePulse>() == null)
		{
			visualizerObj.transform.localScale = Vector3.one * scaleMultiplier;
		}
	}

	// Token: 0x060021AE RID: 8622 RVA: 0x000C2C40 File Offset: 0x000C0E40
	public Image GetPowerOutputIcon()
	{
		foreach (EntityCellVisualizer.PortEntry portEntry in this.ports)
		{
			if (portEntry.type == EntityCellVisualizer.Ports.PowerOut)
			{
				return (portEntry.visualizer != null) ? portEntry.visualizer.transform.GetChild(0).GetComponent<Image>() : null;
			}
		}
		return null;
	}

	// Token: 0x060021AF RID: 8623 RVA: 0x000C2CC4 File Offset: 0x000C0EC4
	public Image GetPowerInputIcon()
	{
		foreach (EntityCellVisualizer.PortEntry portEntry in this.ports)
		{
			if (portEntry.type == EntityCellVisualizer.Ports.PowerIn)
			{
				return (portEntry.visualizer != null) ? portEntry.visualizer.transform.GetChild(0).GetComponent<Image>() : null;
			}
		}
		return null;
	}

	// Token: 0x04001395 RID: 5013
	protected List<EntityCellVisualizer.PortEntry> ports = new List<EntityCellVisualizer.PortEntry>();

	// Token: 0x04001396 RID: 5014
	public EntityCellVisualizer.Ports addedPorts;

	// Token: 0x04001397 RID: 5015
	private GameObject switchVisualizer;

	// Token: 0x04001398 RID: 5016
	private GameObject wireVisualizerAlpha;

	// Token: 0x04001399 RID: 5017
	private GameObject wireVisualizerBeta;

	// Token: 0x0400139A RID: 5018
	public const EntityCellVisualizer.Ports HEAT_PORTS = EntityCellVisualizer.Ports.HeatSource | EntityCellVisualizer.Ports.HeatSink;

	// Token: 0x0400139B RID: 5019
	public const EntityCellVisualizer.Ports POWER_PORTS = EntityCellVisualizer.Ports.PowerIn | EntityCellVisualizer.Ports.PowerOut;

	// Token: 0x0400139C RID: 5020
	public const EntityCellVisualizer.Ports GAS_PORTS = EntityCellVisualizer.Ports.GasIn | EntityCellVisualizer.Ports.GasOut;

	// Token: 0x0400139D RID: 5021
	public const EntityCellVisualizer.Ports LIQUID_PORTS = EntityCellVisualizer.Ports.LiquidIn | EntityCellVisualizer.Ports.LiquidOut;

	// Token: 0x0400139E RID: 5022
	public const EntityCellVisualizer.Ports SOLID_PORTS = EntityCellVisualizer.Ports.SolidIn | EntityCellVisualizer.Ports.SolidOut;

	// Token: 0x0400139F RID: 5023
	public const EntityCellVisualizer.Ports ENERGY_PARTICLES_PORTS = EntityCellVisualizer.Ports.HighEnergyParticleIn | EntityCellVisualizer.Ports.HighEnergyParticleOut;

	// Token: 0x040013A0 RID: 5024
	public const EntityCellVisualizer.Ports DISEASE_PORTS = EntityCellVisualizer.Ports.DiseaseIn | EntityCellVisualizer.Ports.DiseaseOut;

	// Token: 0x040013A1 RID: 5025
	public const EntityCellVisualizer.Ports MATTER_PORTS = EntityCellVisualizer.Ports.GasIn | EntityCellVisualizer.Ports.GasOut | EntityCellVisualizer.Ports.LiquidIn | EntityCellVisualizer.Ports.LiquidOut | EntityCellVisualizer.Ports.SolidIn | EntityCellVisualizer.Ports.SolidOut;

	// Token: 0x040013A2 RID: 5026
	protected Sprite diseaseSourceSprite;

	// Token: 0x040013A3 RID: 5027
	protected Color32 diseaseSourceColour;

	// Token: 0x040013A4 RID: 5028
	[MyCmpGet]
	private Rotatable rotatable;

	// Token: 0x040013A5 RID: 5029
	protected bool enableRaycast = true;

	// Token: 0x040013A6 RID: 5030
	protected Dictionary<GameObject, Image> icons;

	// Token: 0x040013A7 RID: 5031
	public string DiseaseCellVisName = DUPLICANTSTATS.STANDARD.Secretions.PEE_DISEASE;

	// Token: 0x0200144B RID: 5195
	[Flags]
	public enum Ports
	{
		// Token: 0x04006C11 RID: 27665
		PowerIn = 1,
		// Token: 0x04006C12 RID: 27666
		PowerOut = 2,
		// Token: 0x04006C13 RID: 27667
		GasIn = 4,
		// Token: 0x04006C14 RID: 27668
		GasOut = 8,
		// Token: 0x04006C15 RID: 27669
		LiquidIn = 16,
		// Token: 0x04006C16 RID: 27670
		LiquidOut = 32,
		// Token: 0x04006C17 RID: 27671
		SolidIn = 64,
		// Token: 0x04006C18 RID: 27672
		SolidOut = 128,
		// Token: 0x04006C19 RID: 27673
		HighEnergyParticleIn = 256,
		// Token: 0x04006C1A RID: 27674
		HighEnergyParticleOut = 512,
		// Token: 0x04006C1B RID: 27675
		DiseaseIn = 1024,
		// Token: 0x04006C1C RID: 27676
		DiseaseOut = 2048,
		// Token: 0x04006C1D RID: 27677
		HeatSource = 4096,
		// Token: 0x04006C1E RID: 27678
		HeatSink = 8192
	}

	// Token: 0x0200144C RID: 5196
	protected class PortEntry
	{
		// Token: 0x06008D2A RID: 36138 RVA: 0x00357A2F File Offset: 0x00355C2F
		public PortEntry(EntityCellVisualizer.Ports type, CellOffset cellOffset, Color connectedTint, Color disconnectedTint, float scale, bool hideBG)
		{
			this.type = type;
			this.cellOffset = cellOffset;
			this.visualizer = null;
			this.connectedTint = connectedTint;
			this.disconnectedTint = disconnectedTint;
			this.scale = scale;
			this.hideBG = hideBG;
		}

		// Token: 0x04006C1F RID: 27679
		public EntityCellVisualizer.Ports type;

		// Token: 0x04006C20 RID: 27680
		public CellOffset cellOffset;

		// Token: 0x04006C21 RID: 27681
		public GameObject visualizer;

		// Token: 0x04006C22 RID: 27682
		public Color connectedTint;

		// Token: 0x04006C23 RID: 27683
		public Color disconnectedTint;

		// Token: 0x04006C24 RID: 27684
		public float scale;

		// Token: 0x04006C25 RID: 27685
		public bool hideBG;
	}
}
