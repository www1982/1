using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Database;
using Klei;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x02000651 RID: 1617
public class Db : EntityModifierSet
{
	// Token: 0x06002771 RID: 10097 RVA: 0x000E11B8 File Offset: 0x000DF3B8
	public static string GetPath(string dlcId, string folder)
	{
		string text;
		if (dlcId == "")
		{
			text = FileSystem.Normalize(Path.Combine(Application.streamingAssetsPath, folder));
		}
		else
		{
			string contentDirectoryName = DlcManager.GetContentDirectoryName(dlcId);
			text = FileSystem.Normalize(Path.Combine(Application.streamingAssetsPath, "dlc", contentDirectoryName, folder));
		}
		return text;
	}

	// Token: 0x06002772 RID: 10098 RVA: 0x000E1204 File Offset: 0x000DF404
	public static Db Get()
	{
		if (Db._Instance == null)
		{
			Db._Instance = Resources.Load<Db>("Db");
			Db._Instance.Initialize();
		}
		return Db._Instance;
	}

	// Token: 0x06002773 RID: 10099 RVA: 0x000E1231 File Offset: 0x000DF431
	public static BuildingFacades GetBuildingFacades()
	{
		return Db.Get().Permits.BuildingFacades;
	}

	// Token: 0x06002774 RID: 10100 RVA: 0x000E1242 File Offset: 0x000DF442
	public static ArtableStages GetArtableStages()
	{
		return Db.Get().Permits.ArtableStages;
	}

	// Token: 0x06002775 RID: 10101 RVA: 0x000E1253 File Offset: 0x000DF453
	public static EquippableFacades GetEquippableFacades()
	{
		return Db.Get().Permits.EquippableFacades;
	}

	// Token: 0x06002776 RID: 10102 RVA: 0x000E1264 File Offset: 0x000DF464
	public static StickerBombs GetStickerBombs()
	{
		return Db.Get().Permits.StickerBombs;
	}

	// Token: 0x06002777 RID: 10103 RVA: 0x000E1275 File Offset: 0x000DF475
	public static MonumentParts GetMonumentParts()
	{
		return Db.Get().Permits.MonumentParts;
	}

	// Token: 0x06002778 RID: 10104 RVA: 0x000E1288 File Offset: 0x000DF488
	public override void Initialize()
	{
		base.Initialize();
		this.Urges = new Urges();
		this.AssignableSlots = new AssignableSlots();
		this.StateMachineCategories = new StateMachineCategories();
		this.Personalities = new Personalities();
		this.Faces = new Faces();
		this.Shirts = new Shirts();
		this.Expressions = new Expressions(this.Root);
		this.Emotes = new Emotes(this.Root);
		this.Thoughts = new Thoughts(this.Root);
		this.Dreams = new Dreams(this.Root);
		this.Deaths = new Deaths(this.Root);
		this.StatusItemCategories = new StatusItemCategories(this.Root);
		this.TechTreeTitles = new TechTreeTitles(this.Root);
		this.TechTreeTitles.Load(DlcManager.IsExpansion1Active() ? this.researchTreeFileExpansion1 : this.researchTreeFileVanilla);
		this.Techs = new Techs(this.Root);
		this.TechItems = new TechItems(this.Root);
		this.Techs.Init();
		this.Techs.Load(DlcManager.IsExpansion1Active() ? this.researchTreeFileExpansion1 : this.researchTreeFileVanilla);
		this.TechItems.Init();
		this.Accessories = new Accessories(this.Root);
		this.AccessorySlots = new AccessorySlots(this.Root);
		this.ScheduleBlockTypes = new ScheduleBlockTypes(this.Root);
		this.ScheduleGroups = new ScheduleGroups(this.Root);
		this.RoomTypeCategories = new RoomTypeCategories(this.Root);
		this.RoomTypes = new RoomTypes(this.Root);
		this.ArtifactDropRates = new ArtifactDropRates(this.Root);
		this.SpaceDestinationTypes = new SpaceDestinationTypes(this.Root);
		this.Diseases = new Diseases(this.Root, false);
		this.Sicknesses = new global::Database.Sicknesses(this.Root);
		this.SkillPerks = new SkillPerks(this.Root);
		this.SkillGroups = new SkillGroups(this.Root);
		this.Skills = new Skills(this.Root);
		this.ColonyAchievements = new ColonyAchievements(this.Root);
		this.MiscStatusItems = new MiscStatusItems(this.Root);
		this.CreatureStatusItems = new CreatureStatusItems(this.Root);
		this.BuildingStatusItems = new BuildingStatusItems(this.Root);
		this.RobotStatusItems = new RobotStatusItems(this.Root);
		this.ChoreTypes = new ChoreTypes(this.Root);
		this.Quests = new Quests(this.Root);
		this.GameplayEvents = new GameplayEvents(this.Root);
		this.GameplaySeasons = new GameplaySeasons(this.Root);
		this.Stories = new Stories(this.Root);
		if (DlcManager.FeaturePlantMutationsEnabled())
		{
			this.PlantMutations = new PlantMutations(this.Root);
		}
		this.OrbitalTypeCategories = new OrbitalTypeCategories(this.Root);
		this.ArtableStatuses = new ArtableStatuses(this.Root);
		this.Permits = new PermitResources(this.Root);
		Effect effect = new Effect("CenterOfAttention", DUPLICANTS.MODIFIERS.CENTEROFATTENTION.NAME, DUPLICANTS.MODIFIERS.CENTEROFATTENTION.TOOLTIP, 0f, true, true, false, null, -1f, 0f, null, "");
		effect.Add(new AttributeModifier("StressDelta", -0.008333334f, DUPLICANTS.MODIFIERS.CENTEROFATTENTION.NAME, false, false, true));
		this.effects.Add(effect);
		this.Spices = new Spices(this.Root);
		this.CollectResources(this.Root, this.ResourceTable);
	}

	// Token: 0x06002779 RID: 10105 RVA: 0x000E1629 File Offset: 0x000DF829
	public void PostProcess()
	{
		this.Techs.PostProcess();
		this.Permits.PostProcess();
	}

	// Token: 0x0600277A RID: 10106 RVA: 0x000E1644 File Offset: 0x000DF844
	private void CollectResources(Resource resource, List<Resource> resource_table)
	{
		if (resource.Guid != null)
		{
			resource_table.Add(resource);
		}
		ResourceSet resourceSet = resource as ResourceSet;
		if (resourceSet != null)
		{
			for (int i = 0; i < resourceSet.Count; i++)
			{
				this.CollectResources(resourceSet.GetResource(i), resource_table);
			}
		}
	}

	// Token: 0x0600277B RID: 10107 RVA: 0x000E1690 File Offset: 0x000DF890
	public ResourceType GetResource<ResourceType>(ResourceGuid guid) where ResourceType : Resource
	{
		Resource resource = this.ResourceTable.FirstOrDefault((Resource s) => s.Guid == guid);
		if (resource == null)
		{
			string text = "Could not find resource: ";
			ResourceGuid guid2 = guid;
			global::Debug.LogWarning(text + ((guid2 != null) ? guid2.ToString() : null));
			return default(ResourceType);
		}
		ResourceType resourceType = (ResourceType)((object)resource);
		if (resourceType == null)
		{
			global::Debug.LogError(string.Concat(new string[]
			{
				"Resource type mismatch for resource: ",
				resource.Id,
				"\nExpecting Type: ",
				typeof(ResourceType).Name,
				"\nGot Type: ",
				resource.GetType().Name
			}));
			return default(ResourceType);
		}
		return resourceType;
	}

	// Token: 0x0600277C RID: 10108 RVA: 0x000E175B File Offset: 0x000DF95B
	public void ResetProblematicDbs()
	{
		this.Emotes.ResetProblematicReferences();
	}

	// Token: 0x04001703 RID: 5891
	private static Db _Instance;

	// Token: 0x04001704 RID: 5892
	public TextAsset researchTreeFileVanilla;

	// Token: 0x04001705 RID: 5893
	public TextAsset researchTreeFileExpansion1;

	// Token: 0x04001706 RID: 5894
	public Diseases Diseases;

	// Token: 0x04001707 RID: 5895
	public global::Database.Sicknesses Sicknesses;

	// Token: 0x04001708 RID: 5896
	public Urges Urges;

	// Token: 0x04001709 RID: 5897
	public AssignableSlots AssignableSlots;

	// Token: 0x0400170A RID: 5898
	public StateMachineCategories StateMachineCategories;

	// Token: 0x0400170B RID: 5899
	public Personalities Personalities;

	// Token: 0x0400170C RID: 5900
	public Faces Faces;

	// Token: 0x0400170D RID: 5901
	public Shirts Shirts;

	// Token: 0x0400170E RID: 5902
	public Expressions Expressions;

	// Token: 0x0400170F RID: 5903
	public Emotes Emotes;

	// Token: 0x04001710 RID: 5904
	public Thoughts Thoughts;

	// Token: 0x04001711 RID: 5905
	public Dreams Dreams;

	// Token: 0x04001712 RID: 5906
	public BuildingStatusItems BuildingStatusItems;

	// Token: 0x04001713 RID: 5907
	public MiscStatusItems MiscStatusItems;

	// Token: 0x04001714 RID: 5908
	public CreatureStatusItems CreatureStatusItems;

	// Token: 0x04001715 RID: 5909
	public RobotStatusItems RobotStatusItems;

	// Token: 0x04001716 RID: 5910
	public StatusItemCategories StatusItemCategories;

	// Token: 0x04001717 RID: 5911
	public Deaths Deaths;

	// Token: 0x04001718 RID: 5912
	public ChoreTypes ChoreTypes;

	// Token: 0x04001719 RID: 5913
	public TechItems TechItems;

	// Token: 0x0400171A RID: 5914
	public AccessorySlots AccessorySlots;

	// Token: 0x0400171B RID: 5915
	public Accessories Accessories;

	// Token: 0x0400171C RID: 5916
	public ScheduleBlockTypes ScheduleBlockTypes;

	// Token: 0x0400171D RID: 5917
	public ScheduleGroups ScheduleGroups;

	// Token: 0x0400171E RID: 5918
	public RoomTypeCategories RoomTypeCategories;

	// Token: 0x0400171F RID: 5919
	public RoomTypes RoomTypes;

	// Token: 0x04001720 RID: 5920
	public ArtifactDropRates ArtifactDropRates;

	// Token: 0x04001721 RID: 5921
	public SpaceDestinationTypes SpaceDestinationTypes;

	// Token: 0x04001722 RID: 5922
	public SkillPerks SkillPerks;

	// Token: 0x04001723 RID: 5923
	public SkillGroups SkillGroups;

	// Token: 0x04001724 RID: 5924
	public Skills Skills;

	// Token: 0x04001725 RID: 5925
	public ColonyAchievements ColonyAchievements;

	// Token: 0x04001726 RID: 5926
	public Quests Quests;

	// Token: 0x04001727 RID: 5927
	public GameplayEvents GameplayEvents;

	// Token: 0x04001728 RID: 5928
	public GameplaySeasons GameplaySeasons;

	// Token: 0x04001729 RID: 5929
	public PlantMutations PlantMutations;

	// Token: 0x0400172A RID: 5930
	public Spices Spices;

	// Token: 0x0400172B RID: 5931
	public Techs Techs;

	// Token: 0x0400172C RID: 5932
	public TechTreeTitles TechTreeTitles;

	// Token: 0x0400172D RID: 5933
	public OrbitalTypeCategories OrbitalTypeCategories;

	// Token: 0x0400172E RID: 5934
	public PermitResources Permits;

	// Token: 0x0400172F RID: 5935
	public ArtableStatuses ArtableStatuses;

	// Token: 0x04001730 RID: 5936
	public Stories Stories;

	// Token: 0x020014E4 RID: 5348
	[Serializable]
	public class SlotInfo : Resource
	{
	}
}
