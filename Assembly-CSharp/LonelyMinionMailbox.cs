using System;
using UnityEngine;

// Token: 0x0200077C RID: 1916
public class LonelyMinionMailbox : KMonoBehaviour
{
	// Token: 0x06003266 RID: 12902 RVA: 0x0011C320 File Offset: 0x0011A520
	public void Initialize(LonelyMinionHouse.Instance house)
	{
		this.House = house;
		SingleEntityReceptacle component = base.GetComponent<SingleEntityReceptacle>();
		component.occupyingObjectRelativePosition = base.transform.InverseTransformPoint(house.GetParcelPosition());
		component.occupyingObjectRelativePosition.z = -1f;
		StoryInstance storyInstance = StoryManager.Instance.GetStoryInstance(Db.Get().Stories.LonelyMinion.HashId);
		StoryInstance storyInstance2 = storyInstance;
		storyInstance2.StoryStateChanged = (Action<StoryInstance.State>)Delegate.Combine(storyInstance2.StoryStateChanged, new Action<StoryInstance.State>(this.OnStoryStateChanged));
		this.OnStoryStateChanged(storyInstance.CurrentState);
	}

	// Token: 0x06003267 RID: 12903 RVA: 0x0011C3AD File Offset: 0x0011A5AD
	protected override void OnSpawn()
	{
		if (StoryManager.Instance.CheckState(StoryInstance.State.COMPLETE, Db.Get().Stories.LonelyMinion))
		{
			base.gameObject.AddOrGet<Deconstructable>().allowDeconstruction = true;
		}
	}

	// Token: 0x06003268 RID: 12904 RVA: 0x0011C3DC File Offset: 0x0011A5DC
	protected override void OnCleanUp()
	{
		StoryInstance storyInstance = StoryManager.Instance.GetStoryInstance(Db.Get().Stories.LonelyMinion.HashId);
		storyInstance.StoryStateChanged = (Action<StoryInstance.State>)Delegate.Remove(storyInstance.StoryStateChanged, new Action<StoryInstance.State>(this.OnStoryStateChanged));
	}

	// Token: 0x06003269 RID: 12905 RVA: 0x0011C428 File Offset: 0x0011A628
	private void OnStoryStateChanged(StoryInstance.State state)
	{
		QuestInstance quest = QuestManager.GetInstance(this.House.QuestOwnerId, Db.Get().Quests.LonelyMinionFoodQuest);
		if (state == StoryInstance.State.IN_PROGRESS)
		{
			base.Subscribe(-731304873, new Action<object>(this.OnStorageChanged));
			SingleEntityReceptacle singleEntityReceptacle = base.gameObject.AddOrGet<SingleEntityReceptacle>();
			singleEntityReceptacle.enabled = true;
			singleEntityReceptacle.AddAdditionalCriteria(delegate(GameObject candidate)
			{
				EdiblesManager.FoodInfo foodInfo = EdiblesManager.GetFoodInfo(candidate.GetComponent<KPrefabID>().PrefabTag.Name);
				int num = 0;
				return foodInfo != null && quest.DataSatisfiesCriteria(new Quest.ItemData
				{
					CriteriaId = LonelyMinionConfig.FoodCriteriaId,
					QualifyingTag = GameTags.Edible,
					CurrentValue = (float)foodInfo.Quality
				}, ref num);
			});
			RootMenu.Instance.Refresh();
			this.OnStorageChanged(singleEntityReceptacle.Occupant);
		}
		if (state == StoryInstance.State.COMPLETE)
		{
			base.Unsubscribe(-731304873, new Action<object>(this.OnStorageChanged));
			base.gameObject.AddOrGet<Deconstructable>().allowDeconstruction = true;
		}
	}

	// Token: 0x0600326A RID: 12906 RVA: 0x0011C4E3 File Offset: 0x0011A6E3
	private void OnStorageChanged(object data)
	{
		this.House.MailboxContentChanged(data as GameObject);
	}

	// Token: 0x04001E34 RID: 7732
	public LonelyMinionHouse.Instance House;
}
