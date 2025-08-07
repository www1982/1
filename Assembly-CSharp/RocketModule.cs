using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000B65 RID: 2917
[AddComponentMenu("KMonoBehaviour/scripts/RocketModule")]
public class RocketModule : KMonoBehaviour
{
	// Token: 0x060056E1 RID: 22241 RVA: 0x001F7974 File Offset: 0x001F5B74
	public ProcessCondition AddModuleCondition(ProcessCondition.ProcessConditionType conditionType, ProcessCondition condition)
	{
		if (!this.moduleConditions.ContainsKey(conditionType))
		{
			this.moduleConditions.Add(conditionType, new List<ProcessCondition>());
		}
		if (!this.moduleConditions[conditionType].Contains(condition))
		{
			this.moduleConditions[conditionType].Add(condition);
		}
		return condition;
	}

	// Token: 0x060056E2 RID: 22242 RVA: 0x001F79C8 File Offset: 0x001F5BC8
	public List<ProcessCondition> GetConditionSet(ProcessCondition.ProcessConditionType conditionType)
	{
		List<ProcessCondition> list = new List<ProcessCondition>();
		if (conditionType == ProcessCondition.ProcessConditionType.All)
		{
			using (Dictionary<ProcessCondition.ProcessConditionType, List<ProcessCondition>>.Enumerator enumerator = this.moduleConditions.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<ProcessCondition.ProcessConditionType, List<ProcessCondition>> keyValuePair = enumerator.Current;
					list.AddRange(keyValuePair.Value);
				}
				return list;
			}
		}
		if (this.moduleConditions.ContainsKey(conditionType))
		{
			list = this.moduleConditions[conditionType];
		}
		return list;
	}

	// Token: 0x060056E3 RID: 22243 RVA: 0x001F7A48 File Offset: 0x001F5C48
	public void SetBGKAnim(KAnimFile anim_file)
	{
		this.bgAnimFile = anim_file;
	}

	// Token: 0x060056E4 RID: 22244 RVA: 0x001F7A51 File Offset: 0x001F5C51
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		GameUtil.SubscribeToTags<RocketModule>(this, RocketModule.OnRocketOnGroundTagDelegate, false);
		GameUtil.SubscribeToTags<RocketModule>(this, RocketModule.OnRocketNotOnGroundTagDelegate, false);
	}

	// Token: 0x060056E5 RID: 22245 RVA: 0x001F7A74 File Offset: 0x001F5C74
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (!DlcManager.FeatureClusterSpaceEnabled())
		{
			this.conditionManager = this.FindLaunchConditionManager();
			Spacecraft spacecraftFromLaunchConditionManager = SpacecraftManager.instance.GetSpacecraftFromLaunchConditionManager(this.conditionManager);
			if (spacecraftFromLaunchConditionManager != null)
			{
				this.SetParentRocketName(spacecraftFromLaunchConditionManager.GetRocketName());
			}
			this.RegisterWithConditionManager();
		}
		KSelectable component = base.GetComponent<KSelectable>();
		if (component != null)
		{
			component.AddStatusItem(Db.Get().BuildingStatusItems.RocketName, this);
		}
		this.FixSorting();
		AttachableBuilding component2 = base.GetComponent<AttachableBuilding>();
		component2.onAttachmentNetworkChanged = (Action<object>)Delegate.Combine(component2.onAttachmentNetworkChanged, new Action<object>(this.OnAttachmentNetworkChanged));
		if (this.bgAnimFile != null)
		{
			this.AddBGGantry();
		}
	}

	// Token: 0x060056E6 RID: 22246 RVA: 0x001F7B28 File Offset: 0x001F5D28
	public void FixSorting()
	{
		int num = 0;
		AttachableBuilding attachableBuilding = base.GetComponent<AttachableBuilding>();
		while (attachableBuilding != null)
		{
			BuildingAttachPoint attachedTo = attachableBuilding.GetAttachedTo();
			if (!(attachedTo != null))
			{
				break;
			}
			attachableBuilding = attachedTo.GetComponent<AttachableBuilding>();
			num++;
		}
		Vector3 localPosition = base.transform.GetLocalPosition();
		localPosition.z = Grid.GetLayerZ(Grid.SceneLayer.Building) - (float)num * 0.01f;
		base.transform.SetLocalPosition(localPosition);
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		if (component.enabled)
		{
			component.enabled = false;
			component.enabled = true;
		}
	}

	// Token: 0x060056E7 RID: 22247 RVA: 0x001F7BB4 File Offset: 0x001F5DB4
	private void OnAttachmentNetworkChanged(object ab)
	{
		this.FixSorting();
	}

	// Token: 0x060056E8 RID: 22248 RVA: 0x001F7BBC File Offset: 0x001F5DBC
	private void AddBGGantry()
	{
		KAnimControllerBase component = base.GetComponent<KAnimControllerBase>();
		GameObject gameObject = new GameObject();
		gameObject.name = string.Format(this.rocket_module_bg_base_string, base.name, this.rocket_module_bg_affix);
		gameObject.SetActive(false);
		Vector3 position = component.transform.GetPosition();
		position.z = Grid.GetLayerZ(Grid.SceneLayer.InteriorWall);
		gameObject.transform.SetPosition(position);
		gameObject.transform.parent = base.transform;
		KBatchedAnimController kbatchedAnimController = gameObject.AddOrGet<KBatchedAnimController>();
		kbatchedAnimController.AnimFiles = new KAnimFile[] { this.bgAnimFile };
		kbatchedAnimController.initialAnim = this.rocket_module_bg_anim;
		kbatchedAnimController.fgLayer = Grid.SceneLayer.NoLayer;
		kbatchedAnimController.initialMode = KAnim.PlayMode.Paused;
		kbatchedAnimController.FlipX = component.FlipX;
		kbatchedAnimController.FlipY = component.FlipY;
		gameObject.SetActive(true);
	}

	// Token: 0x060056E9 RID: 22249 RVA: 0x001F7C88 File Offset: 0x001F5E88
	private void OnRocketOnGroundTag(object data)
	{
		this.RegisterComponents();
		Operational component = base.GetComponent<Operational>();
		if (this.operationalLandedRequired && component != null)
		{
			component.SetFlag(RocketModule.landedFlag, true);
		}
	}

	// Token: 0x060056EA RID: 22250 RVA: 0x001F7CC0 File Offset: 0x001F5EC0
	private void OnRocketNotOnGroundTag(object data)
	{
		this.DeregisterComponents();
		Operational component = base.GetComponent<Operational>();
		if (this.operationalLandedRequired && component != null)
		{
			component.SetFlag(RocketModule.landedFlag, false);
		}
	}

	// Token: 0x060056EB RID: 22251 RVA: 0x001F7CF8 File Offset: 0x001F5EF8
	public void DeregisterComponents()
	{
		KSelectable component = base.GetComponent<KSelectable>();
		component.IsSelectable = false;
		BuildingComplete component2 = base.GetComponent<BuildingComplete>();
		if (component2 != null)
		{
			component2.UpdatePosition();
		}
		if (SelectTool.Instance.selected == component)
		{
			SelectTool.Instance.Select(null, false);
		}
		Deconstructable component3 = base.GetComponent<Deconstructable>();
		if (component3 != null)
		{
			component3.SetAllowDeconstruction(false);
		}
		HandleVector<int>.Handle handle = GameComps.StructureTemperatures.GetHandle(base.gameObject);
		if (handle.IsValid())
		{
			GameComps.StructureTemperatures.Disable(handle);
		}
		FakeFloorAdder component4 = base.GetComponent<FakeFloorAdder>();
		if (component4 != null)
		{
			component4.SetFloor(false);
		}
		AccessControl component5 = base.GetComponent<AccessControl>();
		if (component5 != null)
		{
			component5.SetRegistered(false);
		}
		foreach (ManualDeliveryKG manualDeliveryKG in base.GetComponents<ManualDeliveryKG>())
		{
			DebugUtil.DevAssert(!manualDeliveryKG.IsPaused, "RocketModule ManualDeliver chore was already paused, when this rocket lands it will re-enable it.", null);
			manualDeliveryKG.Pause(true, "Rocket heading to space");
		}
		BuildingConduitEndpoints[] components2 = base.GetComponents<BuildingConduitEndpoints>();
		for (int i = 0; i < components2.Length; i++)
		{
			components2[i].RemoveEndPoint();
		}
		ReorderableBuilding component6 = base.GetComponent<ReorderableBuilding>();
		if (component6 != null)
		{
			component6.ShowReorderArm(false);
		}
		Workable component7 = base.GetComponent<Workable>();
		if (component7 != null)
		{
			component7.RefreshReachability();
		}
		Structure component8 = base.GetComponent<Structure>();
		if (component8 != null)
		{
			component8.UpdatePosition();
		}
		WireUtilitySemiVirtualNetworkLink component9 = base.GetComponent<WireUtilitySemiVirtualNetworkLink>();
		if (component9 != null)
		{
			component9.SetLinkConnected(false);
		}
		PartialLightBlocking component10 = base.GetComponent<PartialLightBlocking>();
		if (component10 != null)
		{
			component10.ClearLightBlocking();
		}
	}

	// Token: 0x060056EC RID: 22252 RVA: 0x001F7E9C File Offset: 0x001F609C
	public void RegisterComponents()
	{
		base.GetComponent<KSelectable>().IsSelectable = true;
		BuildingComplete component = base.GetComponent<BuildingComplete>();
		if (component != null)
		{
			component.UpdatePosition();
		}
		Deconstructable component2 = base.GetComponent<Deconstructable>();
		if (component2 != null)
		{
			component2.SetAllowDeconstruction(true);
		}
		HandleVector<int>.Handle handle = GameComps.StructureTemperatures.GetHandle(base.gameObject);
		if (handle.IsValid())
		{
			GameComps.StructureTemperatures.Enable(handle);
		}
		Storage[] components = base.GetComponents<Storage>();
		for (int i = 0; i < components.Length; i++)
		{
			components[i].UpdateStoredItemCachedCells();
		}
		FakeFloorAdder component3 = base.GetComponent<FakeFloorAdder>();
		if (component3 != null)
		{
			component3.SetFloor(true);
		}
		AccessControl component4 = base.GetComponent<AccessControl>();
		if (component4 != null)
		{
			component4.SetRegistered(true);
		}
		ManualDeliveryKG[] components2 = base.GetComponents<ManualDeliveryKG>();
		for (int i = 0; i < components2.Length; i++)
		{
			components2[i].Pause(false, "Landing on world");
		}
		BuildingConduitEndpoints[] components3 = base.GetComponents<BuildingConduitEndpoints>();
		for (int i = 0; i < components3.Length; i++)
		{
			components3[i].AddEndpoint();
		}
		ReorderableBuilding component5 = base.GetComponent<ReorderableBuilding>();
		if (component5 != null)
		{
			component5.ShowReorderArm(true);
		}
		Workable component6 = base.GetComponent<Workable>();
		if (component6 != null)
		{
			component6.RefreshReachability();
		}
		Structure component7 = base.GetComponent<Structure>();
		if (component7 != null)
		{
			component7.UpdatePosition();
		}
		WireUtilitySemiVirtualNetworkLink component8 = base.GetComponent<WireUtilitySemiVirtualNetworkLink>();
		if (component8 != null)
		{
			component8.SetLinkConnected(true);
		}
		PartialLightBlocking component9 = base.GetComponent<PartialLightBlocking>();
		if (component9 != null)
		{
			component9.SetLightBlocking();
		}
	}

	// Token: 0x060056ED RID: 22253 RVA: 0x001F802C File Offset: 0x001F622C
	private void ToggleComponent(Type cmpType, bool enabled)
	{
		MonoBehaviour monoBehaviour = (MonoBehaviour)base.GetComponent(cmpType);
		if (monoBehaviour != null)
		{
			monoBehaviour.enabled = enabled;
		}
	}

	// Token: 0x060056EE RID: 22254 RVA: 0x001F8056 File Offset: 0x001F6256
	public void RegisterWithConditionManager()
	{
		global::Debug.Assert(!DlcManager.FeatureClusterSpaceEnabled());
		if (this.conditionManager != null)
		{
			this.conditionManager.RegisterRocketModule(this);
		}
	}

	// Token: 0x060056EF RID: 22255 RVA: 0x001F807F File Offset: 0x001F627F
	protected override void OnCleanUp()
	{
		if (this.conditionManager != null)
		{
			this.conditionManager.UnregisterRocketModule(this);
		}
		base.OnCleanUp();
	}

	// Token: 0x060056F0 RID: 22256 RVA: 0x001F80A4 File Offset: 0x001F62A4
	public virtual LaunchConditionManager FindLaunchConditionManager()
	{
		if (!DlcManager.FeatureClusterSpaceEnabled())
		{
			foreach (GameObject gameObject in AttachableBuilding.GetAttachedNetwork(base.GetComponent<AttachableBuilding>()))
			{
				LaunchConditionManager component = gameObject.GetComponent<LaunchConditionManager>();
				if (component != null)
				{
					return component;
				}
			}
		}
		return null;
	}

	// Token: 0x060056F1 RID: 22257 RVA: 0x001F8114 File Offset: 0x001F6314
	public void SetParentRocketName(string newName)
	{
		this.parentRocketName = newName;
		NameDisplayScreen.Instance.UpdateName(base.gameObject);
	}

	// Token: 0x060056F2 RID: 22258 RVA: 0x001F812D File Offset: 0x001F632D
	public virtual string GetParentRocketName()
	{
		return this.parentRocketName;
	}

	// Token: 0x060056F3 RID: 22259 RVA: 0x001F8138 File Offset: 0x001F6338
	public void MoveToSpace()
	{
		Prioritizable component = base.GetComponent<Prioritizable>();
		if (component != null && component.GetMyWorld() != null)
		{
			component.GetMyWorld().RemoveTopPriorityPrioritizable(component);
		}
		int num = Grid.PosToCell(base.transform.GetPosition());
		Building component2 = base.GetComponent<Building>();
		component2.Def.UnmarkArea(num, component2.Orientation, component2.Def.ObjectLayer, base.gameObject);
		Vector3 vector = new Vector3(-1f, -1f, 0f);
		base.gameObject.transform.SetPosition(vector);
		LogicPorts component3 = base.GetComponent<LogicPorts>();
		if (component3 != null)
		{
			component3.OnMove();
		}
		base.GetComponent<KSelectable>().ToggleStatusItem(Db.Get().BuildingStatusItems.Entombed, false, this);
	}

	// Token: 0x060056F4 RID: 22260 RVA: 0x001F8208 File Offset: 0x001F6408
	public void MoveToPad(int newCell)
	{
		base.gameObject.transform.SetPosition(Grid.CellToPos(newCell, CellAlignment.Bottom, Grid.SceneLayer.Building));
		int num = Grid.PosToCell(base.transform.GetPosition());
		Building component = base.GetComponent<Building>();
		component.RefreshCells();
		component.Def.MarkArea(num, component.Orientation, component.Def.ObjectLayer, base.gameObject);
		LogicPorts component2 = base.GetComponent<LogicPorts>();
		if (component2 != null)
		{
			component2.OnMove();
		}
		Prioritizable component3 = base.GetComponent<Prioritizable>();
		if (component3 != null && component3.IsTopPriority())
		{
			component3.GetMyWorld().AddTopPriorityPrioritizable(component3);
		}
	}

	// Token: 0x04003A27 RID: 14887
	public LaunchConditionManager conditionManager;

	// Token: 0x04003A28 RID: 14888
	public Dictionary<ProcessCondition.ProcessConditionType, List<ProcessCondition>> moduleConditions = new Dictionary<ProcessCondition.ProcessConditionType, List<ProcessCondition>>();

	// Token: 0x04003A29 RID: 14889
	public static readonly Operational.Flag landedFlag = new Operational.Flag("landed", Operational.Flag.Type.Requirement);

	// Token: 0x04003A2A RID: 14890
	public bool operationalLandedRequired = true;

	// Token: 0x04003A2B RID: 14891
	private string rocket_module_bg_base_string = "{0}{1}";

	// Token: 0x04003A2C RID: 14892
	private string rocket_module_bg_affix = "BG";

	// Token: 0x04003A2D RID: 14893
	private string rocket_module_bg_anim = "on";

	// Token: 0x04003A2E RID: 14894
	[SerializeField]
	private KAnimFile bgAnimFile;

	// Token: 0x04003A2F RID: 14895
	protected string parentRocketName = UI.STARMAP.DEFAULT_NAME;

	// Token: 0x04003A30 RID: 14896
	private static readonly EventSystem.IntraObjectHandler<RocketModule> OnRocketOnGroundTagDelegate = GameUtil.CreateHasTagHandler<RocketModule>(GameTags.RocketOnGround, delegate(RocketModule component, object data)
	{
		component.OnRocketOnGroundTag(data);
	});

	// Token: 0x04003A31 RID: 14897
	private static readonly EventSystem.IntraObjectHandler<RocketModule> OnRocketNotOnGroundTagDelegate = GameUtil.CreateHasTagHandler<RocketModule>(GameTags.RocketNotOnGround, delegate(RocketModule component, object data)
	{
		component.OnRocketNotOnGroundTag(data);
	});
}
