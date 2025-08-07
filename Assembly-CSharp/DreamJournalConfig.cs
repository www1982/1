using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000255 RID: 597
public class DreamJournalConfig : IEntityConfig
{
	// Token: 0x06000C13 RID: 3091 RVA: 0x000493D8 File Offset: 0x000475D8
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000C14 RID: 3092 RVA: 0x000493DA File Offset: 0x000475DA
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x06000C15 RID: 3093 RVA: 0x000493DC File Offset: 0x000475DC
	public GameObject CreatePrefab()
	{
		KAnimFile anim = Assets.GetAnim("dream_journal_kanim");
		GameObject gameObject = EntityTemplates.CreateLooseEntity(DreamJournalConfig.ID.Name, ITEMS.DREAMJOURNAL.NAME, ITEMS.DREAMJOURNAL.DESC, 1f, true, anim, "object", Grid.SceneLayer.BuildingFront, EntityTemplates.CollisionShape.RECTANGLE, 0.6f, 0.7f, true, 0, SimHashes.Creature, new List<Tag> { GameTags.StoryTraitResource });
		gameObject.AddOrGet<EntitySplitter>().maxStackSize = 25f;
		return gameObject;
	}

	// Token: 0x0400085A RID: 2138
	public static Tag ID = new Tag("DreamJournal");

	// Token: 0x0400085B RID: 2139
	public const float MASS = 1f;

	// Token: 0x0400085C RID: 2140
	public const int FABRICATION_TIME_SECONDS = 300;

	// Token: 0x0400085D RID: 2141
	private const string ANIM_FILE = "dream_journal_kanim";

	// Token: 0x0400085E RID: 2142
	private const string INITIAL_ANIM = "object";

	// Token: 0x0400085F RID: 2143
	public const int MAX_STACK_SIZE = 25;
}
