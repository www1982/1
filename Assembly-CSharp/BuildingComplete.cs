using System;
using System.Collections.Generic;
using Klei.AI;
using KSerialization;
using UnityEngine;

// Token: 0x020006C8 RID: 1736
public class BuildingComplete : Building
{
	// Token: 0x06002ABE RID: 10942 RVA: 0x000F731D File Offset: 0x000F551D
	private bool WasReplaced()
	{
		return this.replacingTileLayer != ObjectLayer.NumLayers;
	}

	// Token: 0x06002ABF RID: 10943 RVA: 0x000F732C File Offset: 0x000F552C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		Vector3 position = base.transform.GetPosition();
		position.z = Grid.GetLayerZ(this.Def.SceneLayer);
		base.transform.SetPosition(position);
		base.gameObject.SetLayerRecursively(LayerMask.NameToLayer("Default"));
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		Rotatable component2 = base.GetComponent<Rotatable>();
		if (component != null && component2 == null)
		{
			component.Offset = this.Def.GetVisualizerOffset();
		}
		KBoxCollider2D component3 = base.GetComponent<KBoxCollider2D>();
		if (component3 != null)
		{
			Vector3 visualizerOffset = this.Def.GetVisualizerOffset();
			component3.offset += new Vector2(visualizerOffset.x, visualizerOffset.y);
		}
		Attributes attributes = this.GetAttributes();
		foreach (Klei.AI.Attribute attribute in this.Def.attributes)
		{
			attributes.Add(attribute);
		}
		foreach (AttributeModifier attributeModifier in this.Def.attributeModifiers)
		{
			Klei.AI.Attribute attribute2 = Db.Get().BuildingAttributes.Get(attributeModifier.AttributeId);
			if (attributes.Get(attribute2) == null)
			{
				attributes.Add(attribute2);
			}
			attributes.Add(attributeModifier);
		}
		foreach (AttributeInstance attributeInstance in attributes)
		{
			AttributeModifier attributeModifier2 = new AttributeModifier(attributeInstance.Id, attributeInstance.GetTotalValue(), null, false, false, true);
			this.regionModifiers.Add(attributeModifier2);
		}
		if (this.Def.SelfHeatKilowattsWhenActive != 0f || this.Def.ExhaustKilowattsWhenActive != 0f)
		{
			base.gameObject.AddOrGet<KBatchedAnimHeatPostProcessingEffect>();
		}
		if (this.Def.UseStructureTemperature)
		{
			GameComps.StructureTemperatures.Add(base.gameObject);
		}
		base.Subscribe<BuildingComplete>(1606648047, BuildingComplete.OnObjectReplacedDelegate);
		if (this.Def.Entombable)
		{
			base.Subscribe<BuildingComplete>(-1089732772, BuildingComplete.OnEntombedChange);
		}
	}

	// Token: 0x06002AC0 RID: 10944 RVA: 0x000F75A0 File Offset: 0x000F57A0
	private void OnEntombedChanged()
	{
		if (base.gameObject.HasTag(GameTags.Entombed))
		{
			Components.EntombedBuildings.Add(this);
			return;
		}
		Components.EntombedBuildings.Remove(this);
	}

	// Token: 0x06002AC1 RID: 10945 RVA: 0x000F75CB File Offset: 0x000F57CB
	public override void UpdatePosition()
	{
		base.UpdatePosition();
		GameScenePartitioner.Instance.UpdatePosition(this.scenePartitionerEntry, base.GetExtents());
	}

	// Token: 0x06002AC2 RID: 10946 RVA: 0x000F75EC File Offset: 0x000F57EC
	private void OnObjectReplaced(object data)
	{
		Constructable.ReplaceCallbackParameters replaceCallbackParameters = (Constructable.ReplaceCallbackParameters)data;
		this.replacingTileLayer = replaceCallbackParameters.TileLayer;
	}

	// Token: 0x06002AC3 RID: 10947 RVA: 0x000F760C File Offset: 0x000F580C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.primaryElement = base.GetComponent<PrimaryElement>();
		int num = Grid.PosToCell(base.transform.GetPosition());
		if (this.Def.IsFoundation)
		{
			foreach (int num2 in base.PlacementCells)
			{
				Grid.Foundation[num2] = true;
				Game.Instance.roomProber.SolidChangedEvent(num2, false);
			}
		}
		if (Grid.IsValidCell(num))
		{
			Vector3 vector = Grid.CellToPosCBC(num, this.Def.SceneLayer);
			base.transform.SetPosition(vector);
		}
		if (this.primaryElement != null)
		{
			if (this.primaryElement.Mass == 0f)
			{
				this.primaryElement.Mass = this.Def.Mass[0];
			}
			float temperature = this.primaryElement.Temperature;
			if (temperature > 0f && !float.IsNaN(temperature) && !float.IsInfinity(temperature))
			{
				BuildingComplete.MinKelvinSeen = Mathf.Min(BuildingComplete.MinKelvinSeen, temperature);
			}
			PrimaryElement primaryElement = this.primaryElement;
			primaryElement.setTemperatureCallback = (PrimaryElement.SetTemperatureCallback)Delegate.Combine(primaryElement.setTemperatureCallback, new PrimaryElement.SetTemperatureCallback(this.OnSetTemperature));
		}
		if (!base.gameObject.HasTag(GameTags.RocketInSpace))
		{
			this.Def.MarkArea(num, base.Orientation, this.Def.ObjectLayer, base.gameObject);
			if (this.Def.IsTilePiece)
			{
				this.Def.MarkArea(num, base.Orientation, this.Def.TileLayer, base.gameObject);
				this.Def.RunOnArea(num, base.Orientation, delegate(int c)
				{
					TileVisualizer.RefreshCell(c, this.Def.TileLayer, this.Def.ReplacementLayer);
				});
			}
		}
		base.RegisterBlockTileRenderer();
		if (this.Def.PreventIdleTraversalPastBuilding)
		{
			for (int j = 0; j < base.PlacementCells.Length; j++)
			{
				Grid.PreventIdleTraversal[base.PlacementCells[j]] = true;
			}
		}
		Components.BuildingCompletes.Add(this);
		BuildingConfigManager.Instance.AddBuildingCompleteKComponents(base.gameObject, this.Def.Tag);
		this.hasSpawnedKComponents = true;
		this.scenePartitionerEntry = GameScenePartitioner.Instance.Add(base.name, this, base.GetExtents(), GameScenePartitioner.Instance.completeBuildings, null);
		if (this.prefabid.HasTag(GameTags.TemplateBuilding))
		{
			Components.TemplateBuildings.Add(this);
		}
		Attributes attributes = this.GetAttributes();
		if (attributes != null)
		{
			Deconstructable component = base.GetComponent<Deconstructable>();
			if (component != null)
			{
				int k = 1;
				while (k < component.constructionElements.Length)
				{
					Tag tag = component.constructionElements[k];
					Element element = ElementLoader.GetElement(tag);
					if (element != null)
					{
						using (List<AttributeModifier>.Enumerator enumerator = element.attributeModifiers.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								AttributeModifier attributeModifier = enumerator.Current;
								attributes.Add(attributeModifier);
							}
							goto IL_0341;
						}
						goto IL_02E1;
					}
					goto IL_02E1;
					IL_0341:
					k++;
					continue;
					IL_02E1:
					GameObject gameObject = Assets.TryGetPrefab(tag);
					if (!(gameObject != null))
					{
						goto IL_0341;
					}
					PrefabAttributeModifiers component2 = gameObject.GetComponent<PrefabAttributeModifiers>();
					if (component2 != null)
					{
						foreach (AttributeModifier attributeModifier2 in component2.descriptors)
						{
							attributes.Add(attributeModifier2);
						}
						goto IL_0341;
					}
					goto IL_0341;
				}
			}
		}
		BuildingInventory.Instance.RegisterBuilding(this);
	}

	// Token: 0x06002AC4 RID: 10948 RVA: 0x000F7998 File Offset: 0x000F5B98
	private void OnSetTemperature(PrimaryElement primary_element, float temperature)
	{
		BuildingComplete.MinKelvinSeen = Mathf.Min(BuildingComplete.MinKelvinSeen, temperature);
	}

	// Token: 0x06002AC5 RID: 10949 RVA: 0x000F79AA File Offset: 0x000F5BAA
	public void SetCreationTime(float time)
	{
		this.creationTime = time;
	}

	// Token: 0x06002AC6 RID: 10950 RVA: 0x000F79B3 File Offset: 0x000F5BB3
	private string GetInspectSound()
	{
		return GlobalAssets.GetSound("AI_Inspect_" + base.GetComponent<KPrefabID>().PrefabTag.Name, false);
	}

	// Token: 0x06002AC7 RID: 10951 RVA: 0x000F79D8 File Offset: 0x000F5BD8
	protected override void OnCleanUp()
	{
		if (Game.quitting)
		{
			return;
		}
		GameScenePartitioner.Instance.Free(ref this.scenePartitionerEntry);
		if (this.hasSpawnedKComponents)
		{
			BuildingConfigManager.Instance.DestroyBuildingCompleteKComponents(base.gameObject, this.Def.Tag);
		}
		if (this.Def.UseStructureTemperature)
		{
			GameComps.StructureTemperatures.Remove(base.gameObject);
		}
		base.OnCleanUp();
		if (!this.WasReplaced() && base.gameObject.GetMyWorldId() != 255)
		{
			int num = Grid.PosToCell(this);
			this.Def.UnmarkArea(num, base.Orientation, this.Def.ObjectLayer, base.gameObject);
			if (this.Def.IsTilePiece)
			{
				this.Def.UnmarkArea(num, base.Orientation, this.Def.TileLayer, base.gameObject);
				this.Def.RunOnArea(num, base.Orientation, delegate(int c)
				{
					TileVisualizer.RefreshCell(c, this.Def.TileLayer, this.Def.ReplacementLayer);
				});
			}
			if (this.Def.IsFoundation)
			{
				foreach (int num2 in base.PlacementCells)
				{
					Grid.Foundation[num2] = false;
					Game.Instance.roomProber.SolidChangedEvent(num2, false);
				}
			}
			if (this.Def.PreventIdleTraversalPastBuilding)
			{
				for (int j = 0; j < base.PlacementCells.Length; j++)
				{
					Grid.PreventIdleTraversal[base.PlacementCells[j]] = false;
				}
			}
		}
		if (this.WasReplaced() && this.Def.IsTilePiece && this.replacingTileLayer != this.Def.TileLayer)
		{
			int num3 = Grid.PosToCell(this);
			this.Def.UnmarkArea(num3, base.Orientation, this.Def.TileLayer, base.gameObject);
			this.Def.RunOnArea(num3, base.Orientation, delegate(int c)
			{
				TileVisualizer.RefreshCell(c, this.Def.TileLayer, this.Def.ReplacementLayer);
			});
		}
		Components.BuildingCompletes.Remove(this);
		Components.EntombedBuildings.Remove(this);
		Components.TemplateBuildings.Remove(this);
		base.UnregisterBlockTileRenderer();
		BuildingInventory.Instance.UnregisterBuilding(this);
		base.Trigger(-21016276, this);
	}

	// Token: 0x0400192C RID: 6444
	[MyCmpReq]
	private Modifiers modifiers;

	// Token: 0x0400192D RID: 6445
	[MyCmpGet]
	public KPrefabID prefabid;

	// Token: 0x0400192E RID: 6446
	public bool isManuallyOperated;

	// Token: 0x0400192F RID: 6447
	public bool isArtable;

	// Token: 0x04001930 RID: 6448
	public PrimaryElement primaryElement;

	// Token: 0x04001931 RID: 6449
	[Serialize]
	public float creationTime = -1f;

	// Token: 0x04001932 RID: 6450
	private bool hasSpawnedKComponents;

	// Token: 0x04001933 RID: 6451
	private ObjectLayer replacingTileLayer = ObjectLayer.NumLayers;

	// Token: 0x04001934 RID: 6452
	public List<AttributeModifier> regionModifiers = new List<AttributeModifier>();

	// Token: 0x04001935 RID: 6453
	private static readonly EventSystem.IntraObjectHandler<BuildingComplete> OnEntombedChange = new EventSystem.IntraObjectHandler<BuildingComplete>(delegate(BuildingComplete component, object data)
	{
		component.OnEntombedChanged();
	});

	// Token: 0x04001936 RID: 6454
	private static readonly EventSystem.IntraObjectHandler<BuildingComplete> OnObjectReplacedDelegate = new EventSystem.IntraObjectHandler<BuildingComplete>(delegate(BuildingComplete component, object data)
	{
		component.OnObjectReplaced(data);
	});

	// Token: 0x04001937 RID: 6455
	private HandleVector<int>.Handle scenePartitionerEntry;

	// Token: 0x04001938 RID: 6456
	public static float MinKelvinSeen = float.MaxValue;
}
