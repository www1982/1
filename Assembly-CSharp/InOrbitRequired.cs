using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x020005C4 RID: 1476
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/InOrbitRequired")]
public class InOrbitRequired : KMonoBehaviour, IGameObjectEffectDescriptor
{
	// Token: 0x06002201 RID: 8705 RVA: 0x000C4060 File Offset: 0x000C2260
	protected override void OnSpawn()
	{
		WorldContainer myWorld = this.GetMyWorld();
		this.craftModuleInterface = myWorld.GetComponent<CraftModuleInterface>();
		base.OnSpawn();
		bool flag = this.craftModuleInterface.HasTag(GameTags.RocketNotOnGround);
		this.UpdateFlag(flag);
		this.craftModuleInterface.Subscribe(-1582839653, new Action<object>(this.OnTagsChanged));
	}

	// Token: 0x06002202 RID: 8706 RVA: 0x000C40BB File Offset: 0x000C22BB
	protected override void OnCleanUp()
	{
		if (this.craftModuleInterface != null)
		{
			this.craftModuleInterface.Unsubscribe(-1582839653, new Action<object>(this.OnTagsChanged));
		}
	}

	// Token: 0x06002203 RID: 8707 RVA: 0x000C40E8 File Offset: 0x000C22E8
	private void OnTagsChanged(object data)
	{
		TagChangedEventData tagChangedEventData = (TagChangedEventData)data;
		if (tagChangedEventData.tag == GameTags.RocketNotOnGround)
		{
			this.UpdateFlag(tagChangedEventData.added);
		}
	}

	// Token: 0x06002204 RID: 8708 RVA: 0x000C411A File Offset: 0x000C231A
	private void UpdateFlag(bool newInOrbit)
	{
		this.operational.SetFlag(InOrbitRequired.inOrbitFlag, newInOrbit);
		base.GetComponent<KSelectable>().ToggleStatusItem(Db.Get().BuildingStatusItems.InOrbitRequired, !newInOrbit, this);
	}

	// Token: 0x06002205 RID: 8709 RVA: 0x000C414D File Offset: 0x000C234D
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		return new List<Descriptor>
		{
			new Descriptor(UI.BUILDINGEFFECTS.IN_ORBIT_REQUIRED, UI.BUILDINGEFFECTS.TOOLTIPS.IN_ORBIT_REQUIRED, Descriptor.DescriptorType.Requirement, false)
		};
	}

	// Token: 0x040013D7 RID: 5079
	[MyCmpReq]
	private Building building;

	// Token: 0x040013D8 RID: 5080
	[MyCmpReq]
	private Operational operational;

	// Token: 0x040013D9 RID: 5081
	public static readonly Operational.Flag inOrbitFlag = new Operational.Flag("in_orbit", Operational.Flag.Type.Requirement);

	// Token: 0x040013DA RID: 5082
	private CraftModuleInterface craftModuleInterface;
}
