using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Database;
using STRINGS;
using UnityEngine;

// Token: 0x020006C3 RID: 1731
[AddComponentMenu("KMonoBehaviour/scripts/Building")]
public class Building : KMonoBehaviour, IGameObjectEffectDescriptor, IUniformGridObject, IApproachable
{
	// Token: 0x17000205 RID: 517
	// (get) Token: 0x06002A8E RID: 10894 RVA: 0x000F6457 File Offset: 0x000F4657
	public Orientation Orientation
	{
		get
		{
			if (!(this.rotatable != null))
			{
				return Orientation.Neutral;
			}
			return this.rotatable.GetOrientation();
		}
	}

	// Token: 0x17000206 RID: 518
	// (get) Token: 0x06002A8F RID: 10895 RVA: 0x000F6474 File Offset: 0x000F4674
	public int[] PlacementCells
	{
		get
		{
			if (this.placementCells == null)
			{
				this.RefreshCells();
			}
			return this.placementCells;
		}
	}

	// Token: 0x06002A90 RID: 10896 RVA: 0x000F648A File Offset: 0x000F468A
	public Extents GetExtents()
	{
		if (this.extents.width == 0 || this.extents.height == 0)
		{
			this.RefreshCells();
		}
		return this.extents;
	}

	// Token: 0x06002A91 RID: 10897 RVA: 0x000F64B4 File Offset: 0x000F46B4
	public Extents GetValidPlacementExtents()
	{
		Extents extents = this.GetExtents();
		extents.x--;
		extents.y--;
		extents.width += 2;
		extents.height += 2;
		return extents;
	}

	// Token: 0x06002A92 RID: 10898 RVA: 0x000F64FC File Offset: 0x000F46FC
	public bool PlacementCellsContainCell(int cell)
	{
		for (int i = 0; i < this.PlacementCells.Length; i++)
		{
			if (this.PlacementCells[i] == cell)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06002A93 RID: 10899 RVA: 0x000F652C File Offset: 0x000F472C
	public void RefreshCells()
	{
		this.placementCells = new int[this.Def.PlacementOffsets.Length];
		int num = Grid.PosToCell(this);
		if (num < 0)
		{
			this.extents.x = -1;
			this.extents.y = -1;
			this.extents.width = this.Def.WidthInCells;
			this.extents.height = this.Def.HeightInCells;
			return;
		}
		Orientation orientation = this.Orientation;
		for (int i = 0; i < this.Def.PlacementOffsets.Length; i++)
		{
			CellOffset rotatedCellOffset = Rotatable.GetRotatedCellOffset(this.Def.PlacementOffsets[i], orientation);
			int num2 = Grid.OffsetCell(num, rotatedCellOffset);
			this.placementCells[i] = num2;
		}
		int num3 = 0;
		int num4 = 0;
		Grid.CellToXY(this.placementCells[0], out num3, out num4);
		int num5 = num3;
		int num6 = num4;
		foreach (int num7 in this.placementCells)
		{
			int num8 = 0;
			int num9 = 0;
			Grid.CellToXY(num7, out num8, out num9);
			num3 = Math.Min(num3, num8);
			num4 = Math.Min(num4, num9);
			num5 = Math.Max(num5, num8);
			num6 = Math.Max(num6, num9);
		}
		this.extents.x = num3;
		this.extents.y = num4;
		this.extents.width = num5 - num3 + 1;
		this.extents.height = num6 - num4 + 1;
	}

	// Token: 0x06002A94 RID: 10900 RVA: 0x000F669C File Offset: 0x000F489C
	[OnDeserialized]
	internal void OnDeserialized()
	{
		PrimaryElement component = base.GetComponent<PrimaryElement>();
		if (component != null && component.Temperature == 0f)
		{
			if (component.Element == null)
			{
				DeserializeWarnings.Instance.PrimaryElementHasNoElement.Warn(base.name + " primary element has no element.", base.gameObject);
				return;
			}
			if (!(this is BuildingUnderConstruction))
			{
				DeserializeWarnings.Instance.BuildingTemeperatureIsZeroKelvin.Warn(base.name + " is at zero degrees kelvin. Resetting temperature.", null);
				component.Temperature = component.Element.defaultValues.temperature;
			}
		}
	}

	// Token: 0x06002A95 RID: 10901 RVA: 0x000F6734 File Offset: 0x000F4934
	public static void CreateBuildingMeltedNotification(GameObject building)
	{
		Vector3 pos = building.transform.GetPosition();
		Notifier notifier = building.AddOrGet<Notifier>();
		Notification notification = new Notification(MISC.NOTIFICATIONS.BUILDING_MELTED.NAME, NotificationType.Bad, (List<Notification> notificationList, object data) => MISC.NOTIFICATIONS.BUILDING_MELTED.TOOLTIP + notificationList.ReduceMessages(false), "/t• " + notifier.GetProperName(), true, 0f, delegate(object o)
		{
			GameUtil.FocusCamera(pos, 2f, true, true);
		}, null, null, true, true, false);
		notifier.Add(notification, "");
	}

	// Token: 0x06002A96 RID: 10902 RVA: 0x000F67C2 File Offset: 0x000F49C2
	public void SetDescription(string desc)
	{
		this.description = desc;
	}

	// Token: 0x17000207 RID: 519
	// (get) Token: 0x06002A97 RID: 10903 RVA: 0x000F67CB File Offset: 0x000F49CB
	public string Desc
	{
		get
		{
			return this.Def.Desc;
		}
	}

	// Token: 0x17000208 RID: 520
	// (get) Token: 0x06002A98 RID: 10904 RVA: 0x000F67D8 File Offset: 0x000F49D8
	public string DescFlavour
	{
		get
		{
			return this.descriptionFlavour;
		}
	}

	// Token: 0x17000209 RID: 521
	// (get) Token: 0x06002A99 RID: 10905 RVA: 0x000F67E0 File Offset: 0x000F49E0
	public string DescEffect
	{
		get
		{
			return this.Def.Effect;
		}
	}

	// Token: 0x06002A9A RID: 10906 RVA: 0x000F67ED File Offset: 0x000F49ED
	public void SetDescriptionFlavour(string descriptionFlavour)
	{
		this.descriptionFlavour = descriptionFlavour;
	}

	// Token: 0x06002A9B RID: 10907 RVA: 0x000F67F8 File Offset: 0x000F49F8
	protected override void OnSpawn()
	{
		if (this.Def == null)
		{
			global::Debug.LogError("Missing building definition on object " + base.name);
		}
		KSelectable component = base.GetComponent<KSelectable>();
		if (component != null)
		{
			component.SetName(this.Def.Name);
			component.SetStatusIndicatorOffset(new Vector3(0f, -0.35f, 0f));
		}
		Prioritizable component2 = base.GetComponent<Prioritizable>();
		if (component2 != null)
		{
			component2.iconOffset.y = 0.3f;
		}
		if (base.GetComponent<KPrefabID>().HasTag(RoomConstraints.ConstraintTags.IndustrialMachinery))
		{
			this.scenePartitionerEntry = GameScenePartitioner.Instance.Add(base.name, base.gameObject, this.GetExtents(), GameScenePartitioner.Instance.industrialBuildings, null);
		}
		if (this.Def.Deprecated && base.GetComponent<KSelectable>() != null)
		{
			KSelectable component3 = base.GetComponent<KSelectable>();
			Building.deprecatedBuildingStatusItem = new StatusItem("BUILDING_DEPRECATED", "BUILDING", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022, null);
			component3.AddStatusItem(Building.deprecatedBuildingStatusItem, null);
		}
	}

	// Token: 0x06002A9C RID: 10908 RVA: 0x000F6917 File Offset: 0x000F4B17
	protected override void OnCleanUp()
	{
		GameScenePartitioner.Instance.Free(ref this.scenePartitionerEntry);
		base.OnCleanUp();
	}

	// Token: 0x06002A9D RID: 10909 RVA: 0x000F692F File Offset: 0x000F4B2F
	public virtual void UpdatePosition()
	{
		this.RefreshCells();
		GameScenePartitioner.Instance.UpdatePosition(this.scenePartitionerEntry, this.GetExtents());
	}

	// Token: 0x06002A9E RID: 10910 RVA: 0x000F6950 File Offset: 0x000F4B50
	protected void RegisterBlockTileRenderer()
	{
		if (this.Def.BlockTileAtlas != null)
		{
			PrimaryElement component = base.GetComponent<PrimaryElement>();
			if (component != null)
			{
				SimHashes visualizationElementID = this.GetVisualizationElementID(component);
				int num = Grid.PosToCell(base.transform.GetPosition());
				Constructable component2 = base.GetComponent<Constructable>();
				bool flag = component2 != null && component2.IsReplacementTile;
				World.Instance.blockTileRenderer.AddBlock(base.gameObject.layer, this.Def, flag, visualizationElementID, num);
			}
		}
	}

	// Token: 0x06002A9F RID: 10911 RVA: 0x000F69D8 File Offset: 0x000F4BD8
	public CellOffset GetRotatedOffset(CellOffset offset)
	{
		if (!(this.rotatable != null))
		{
			return offset;
		}
		return this.rotatable.GetRotatedCellOffset(offset);
	}

	// Token: 0x06002AA0 RID: 10912 RVA: 0x000F69F6 File Offset: 0x000F4BF6
	public int GetBottomLeftCell()
	{
		return Grid.PosToCell(base.transform.GetPosition());
	}

	// Token: 0x06002AA1 RID: 10913 RVA: 0x000F6A08 File Offset: 0x000F4C08
	public int GetPowerInputCell()
	{
		CellOffset rotatedOffset = this.GetRotatedOffset(this.Def.PowerInputOffset);
		return Grid.OffsetCell(this.GetBottomLeftCell(), rotatedOffset);
	}

	// Token: 0x06002AA2 RID: 10914 RVA: 0x000F6A34 File Offset: 0x000F4C34
	public int GetPowerOutputCell()
	{
		CellOffset rotatedOffset = this.GetRotatedOffset(this.Def.PowerOutputOffset);
		return Grid.OffsetCell(this.GetBottomLeftCell(), rotatedOffset);
	}

	// Token: 0x06002AA3 RID: 10915 RVA: 0x000F6A60 File Offset: 0x000F4C60
	public int GetUtilityInputCell()
	{
		CellOffset rotatedOffset = this.GetRotatedOffset(this.Def.UtilityInputOffset);
		return Grid.OffsetCell(this.GetBottomLeftCell(), rotatedOffset);
	}

	// Token: 0x06002AA4 RID: 10916 RVA: 0x000F6A8C File Offset: 0x000F4C8C
	public int GetHighEnergyParticleInputCell()
	{
		CellOffset rotatedOffset = this.GetRotatedOffset(this.Def.HighEnergyParticleInputOffset);
		return Grid.OffsetCell(this.GetBottomLeftCell(), rotatedOffset);
	}

	// Token: 0x06002AA5 RID: 10917 RVA: 0x000F6AB8 File Offset: 0x000F4CB8
	public int GetHighEnergyParticleOutputCell()
	{
		CellOffset rotatedOffset = this.GetRotatedOffset(this.Def.HighEnergyParticleOutputOffset);
		return Grid.OffsetCell(this.GetBottomLeftCell(), rotatedOffset);
	}

	// Token: 0x06002AA6 RID: 10918 RVA: 0x000F6AE4 File Offset: 0x000F4CE4
	public int GetUtilityOutputCell()
	{
		CellOffset rotatedOffset = this.GetRotatedOffset(this.Def.UtilityOutputOffset);
		return Grid.OffsetCell(this.GetBottomLeftCell(), rotatedOffset);
	}

	// Token: 0x06002AA7 RID: 10919 RVA: 0x000F6B0F File Offset: 0x000F4D0F
	public CellOffset GetUtilityInputOffset()
	{
		return this.GetRotatedOffset(this.Def.UtilityInputOffset);
	}

	// Token: 0x06002AA8 RID: 10920 RVA: 0x000F6B22 File Offset: 0x000F4D22
	public CellOffset GetUtilityOutputOffset()
	{
		return this.GetRotatedOffset(this.Def.UtilityOutputOffset);
	}

	// Token: 0x06002AA9 RID: 10921 RVA: 0x000F6B35 File Offset: 0x000F4D35
	public CellOffset GetHighEnergyParticleInputOffset()
	{
		return this.GetRotatedOffset(this.Def.HighEnergyParticleInputOffset);
	}

	// Token: 0x06002AAA RID: 10922 RVA: 0x000F6B48 File Offset: 0x000F4D48
	public CellOffset GetHighEnergyParticleOutputOffset()
	{
		return this.GetRotatedOffset(this.Def.HighEnergyParticleOutputOffset);
	}

	// Token: 0x06002AAB RID: 10923 RVA: 0x000F6B5C File Offset: 0x000F4D5C
	protected void UnregisterBlockTileRenderer()
	{
		if (this.Def.BlockTileAtlas != null)
		{
			PrimaryElement component = base.GetComponent<PrimaryElement>();
			if (component != null)
			{
				SimHashes visualizationElementID = this.GetVisualizationElementID(component);
				int num = Grid.PosToCell(base.transform.GetPosition());
				Constructable component2 = base.GetComponent<Constructable>();
				bool flag = component2 != null && component2.IsReplacementTile;
				World.Instance.blockTileRenderer.RemoveBlock(this.Def, flag, visualizationElementID, num);
			}
		}
	}

	// Token: 0x06002AAC RID: 10924 RVA: 0x000F6BD9 File Offset: 0x000F4DD9
	private SimHashes GetVisualizationElementID(PrimaryElement pe)
	{
		if (!(this is BuildingComplete))
		{
			return SimHashes.Void;
		}
		return pe.ElementID;
	}

	// Token: 0x06002AAD RID: 10925 RVA: 0x000F6BEF File Offset: 0x000F4DEF
	public void RunOnArea(Action<int> callback)
	{
		this.Def.RunOnArea(Grid.PosToCell(this), this.Orientation, callback);
	}

	// Token: 0x06002AAE RID: 10926 RVA: 0x000F6C0C File Offset: 0x000F4E0C
	public List<Descriptor> RequirementDescriptors(BuildingDef def)
	{
		List<Descriptor> list = new List<Descriptor>();
		BuildingComplete component = def.BuildingComplete.GetComponent<BuildingComplete>();
		if (def.RequiresPowerInput)
		{
			float wattsNeededWhenActive = component.GetComponent<IEnergyConsumer>().WattsNeededWhenActive;
			if (wattsNeededWhenActive > 0f)
			{
				string formattedWattage = GameUtil.GetFormattedWattage(wattsNeededWhenActive, GameUtil.WattageFormatterUnit.Automatic, true);
				Descriptor descriptor = new Descriptor(string.Format(UI.BUILDINGEFFECTS.REQUIRESPOWER, formattedWattage), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.REQUIRESPOWER, formattedWattage), Descriptor.DescriptorType.Requirement, false);
				list.Add(descriptor);
			}
		}
		if (def.InputConduitType == ConduitType.Liquid)
		{
			Descriptor descriptor2 = default(Descriptor);
			descriptor2.SetupDescriptor(UI.BUILDINGEFFECTS.REQUIRESLIQUIDINPUT, UI.BUILDINGEFFECTS.TOOLTIPS.REQUIRESLIQUIDINPUT, Descriptor.DescriptorType.Requirement);
			list.Add(descriptor2);
		}
		else if (def.InputConduitType == ConduitType.Gas)
		{
			Descriptor descriptor3 = default(Descriptor);
			descriptor3.SetupDescriptor(UI.BUILDINGEFFECTS.REQUIRESGASINPUT, UI.BUILDINGEFFECTS.TOOLTIPS.REQUIRESGASINPUT, Descriptor.DescriptorType.Requirement);
			list.Add(descriptor3);
		}
		if (def.OutputConduitType == ConduitType.Liquid)
		{
			Descriptor descriptor4 = default(Descriptor);
			descriptor4.SetupDescriptor(UI.BUILDINGEFFECTS.REQUIRESLIQUIDOUTPUT, UI.BUILDINGEFFECTS.TOOLTIPS.REQUIRESLIQUIDOUTPUT, Descriptor.DescriptorType.Requirement);
			list.Add(descriptor4);
		}
		else if (def.OutputConduitType == ConduitType.Gas)
		{
			Descriptor descriptor5 = default(Descriptor);
			descriptor5.SetupDescriptor(UI.BUILDINGEFFECTS.REQUIRESGASOUTPUT, UI.BUILDINGEFFECTS.TOOLTIPS.REQUIRESGASOUTPUT, Descriptor.DescriptorType.Requirement);
			list.Add(descriptor5);
		}
		if (component.isManuallyOperated)
		{
			Descriptor descriptor6 = default(Descriptor);
			descriptor6.SetupDescriptor(UI.BUILDINGEFFECTS.REQUIRESMANUALOPERATION, UI.BUILDINGEFFECTS.TOOLTIPS.REQUIRESMANUALOPERATION, Descriptor.DescriptorType.Requirement);
			list.Add(descriptor6);
		}
		if (component.Def.RequiredSkillPerkID != null)
		{
			Descriptor descriptor7 = default(Descriptor);
			string text = GameUtil.NamesOfSkillsWithSkillPerk(component.Def.RequiredSkillPerkID);
			if (Game.IsDlcActiveForCurrentSave("DLC3_ID"))
			{
				string text2 = UI.BUILDINGEFFECTS.TOOLTIPS.REQUIRESSKILLEDOPERATION_DLC3.Replace("{Skill}", text).Replace("{Booster}", GameUtil.NamesOfBoostersWithSkillPerk(component.Def.RequiredSkillPerkID));
				descriptor7.SetupDescriptor(UI.BUILDINGEFFECTS.REQUIRESSKILLEDOPERATION_DLC3, text2, Descriptor.DescriptorType.Requirement);
			}
			else
			{
				string text3 = UI.BUILDINGEFFECTS.TOOLTIPS.REQUIRESSKILLEDOPERATION.Replace("{Skill}", text);
				descriptor7.SetupDescriptor(UI.BUILDINGEFFECTS.REQUIRESSKILLEDOPERATION, text3, Descriptor.DescriptorType.Requirement);
			}
			list.Add(descriptor7);
		}
		if (component.isArtable)
		{
			Descriptor descriptor8 = default(Descriptor);
			descriptor8.SetupDescriptor(UI.BUILDINGEFFECTS.REQUIRESCREATIVITY, UI.BUILDINGEFFECTS.TOOLTIPS.REQUIRESCREATIVITY, Descriptor.DescriptorType.Requirement);
			list.Add(descriptor8);
		}
		if (def.BuildingUnderConstruction != null)
		{
			Constructable component2 = def.BuildingUnderConstruction.GetComponent<Constructable>();
			if (component2 != null && component2.requiredSkillPerk != HashedString.Invalid)
			{
				StringBuilder stringBuilder = new StringBuilder();
				List<Skill> skillsWithPerk = Db.Get().Skills.GetSkillsWithPerk(component2.requiredSkillPerk);
				for (int i = 0; i < skillsWithPerk.Count; i++)
				{
					Skill skill = skillsWithPerk[i];
					stringBuilder.Append(skill.Name);
					if (i != skillsWithPerk.Count - 1)
					{
						stringBuilder.Append(", ");
					}
				}
				string text4 = stringBuilder.ToString();
				list.Add(new Descriptor(UI.BUILD_REQUIRES_SKILL.Replace("{Skill}", text4), UI.BUILD_REQUIRES_SKILL_TOOLTIP.Replace("{Skill}", text4), Descriptor.DescriptorType.Requirement, false));
			}
		}
		return list;
	}

	// Token: 0x06002AAF RID: 10927 RVA: 0x000F6F54 File Offset: 0x000F5154
	public List<Descriptor> EffectDescriptors(BuildingDef def)
	{
		List<Descriptor> list = new List<Descriptor>();
		if (def.EffectDescription != null)
		{
			list.AddRange(def.EffectDescription);
		}
		if (def.GeneratorWattageRating > 0f && base.GetComponent<Battery>() == null)
		{
			Descriptor descriptor = default(Descriptor);
			descriptor.SetupDescriptor(string.Format(UI.BUILDINGEFFECTS.ENERGYGENERATED, GameUtil.GetFormattedWattage(def.GeneratorWattageRating, GameUtil.WattageFormatterUnit.Automatic, true)), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.ENERGYGENERATED, GameUtil.GetFormattedWattage(def.GeneratorWattageRating, GameUtil.WattageFormatterUnit.Automatic, true)), Descriptor.DescriptorType.Effect);
			list.Add(descriptor);
		}
		if (def.ExhaustKilowattsWhenActive > 0f || def.SelfHeatKilowattsWhenActive > 0f)
		{
			Descriptor descriptor2 = default(Descriptor);
			string formattedHeatEnergy = GameUtil.GetFormattedHeatEnergy((def.ExhaustKilowattsWhenActive + def.SelfHeatKilowattsWhenActive) * 1000f, GameUtil.HeatEnergyFormatterUnit.Automatic);
			descriptor2.SetupDescriptor(string.Format(UI.BUILDINGEFFECTS.HEATGENERATED, formattedHeatEnergy), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.HEATGENERATED, formattedHeatEnergy), Descriptor.DescriptorType.Effect);
			list.Add(descriptor2);
		}
		return list;
	}

	// Token: 0x06002AB0 RID: 10928 RVA: 0x000F7054 File Offset: 0x000F5254
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		foreach (Descriptor descriptor in this.RequirementDescriptors(this.Def))
		{
			list.Add(descriptor);
		}
		foreach (Descriptor descriptor2 in this.EffectDescriptors(this.Def))
		{
			list.Add(descriptor2);
		}
		return list;
	}

	// Token: 0x06002AB1 RID: 10929 RVA: 0x000F70FC File Offset: 0x000F52FC
	public override Vector2 PosMin()
	{
		Extents extents = this.GetExtents();
		return new Vector2((float)extents.x, (float)extents.y);
	}

	// Token: 0x06002AB2 RID: 10930 RVA: 0x000F7124 File Offset: 0x000F5324
	public override Vector2 PosMax()
	{
		Extents extents = this.GetExtents();
		return new Vector2((float)(extents.x + extents.width), (float)(extents.y + extents.height));
	}

	// Token: 0x06002AB3 RID: 10931 RVA: 0x000F7159 File Offset: 0x000F5359
	public CellOffset[] GetOffsets()
	{
		return OffsetGroups.Use;
	}

	// Token: 0x06002AB4 RID: 10932 RVA: 0x000F7160 File Offset: 0x000F5360
	public int GetCell()
	{
		return Grid.PosToCell(this);
	}

	// Token: 0x0400191C RID: 6428
	public BuildingDef Def;

	// Token: 0x0400191D RID: 6429
	[MyCmpGet]
	private Rotatable rotatable;

	// Token: 0x0400191E RID: 6430
	[MyCmpAdd]
	private StateMachineController stateMachineController;

	// Token: 0x0400191F RID: 6431
	private int[] placementCells;

	// Token: 0x04001920 RID: 6432
	private Extents extents;

	// Token: 0x04001921 RID: 6433
	private static StatusItem deprecatedBuildingStatusItem;

	// Token: 0x04001922 RID: 6434
	private string description;

	// Token: 0x04001923 RID: 6435
	private string descriptionFlavour;

	// Token: 0x04001924 RID: 6436
	private HandleVector<int>.Handle scenePartitionerEntry;
}
