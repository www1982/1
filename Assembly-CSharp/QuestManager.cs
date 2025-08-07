using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x02000A83 RID: 2691
[SerializationConfig(MemberSerialization.OptIn)]
public class QuestManager : KMonoBehaviour
{
	// Token: 0x06004E20 RID: 20000 RVA: 0x001C4D34 File Offset: 0x001C2F34
	protected override void OnPrefabInit()
	{
		if (QuestManager.instance != null)
		{
			global::UnityEngine.Object.Destroy(QuestManager.instance);
			return;
		}
		QuestManager.instance = this;
		base.OnPrefabInit();
	}

	// Token: 0x06004E21 RID: 20001 RVA: 0x001C4D5C File Offset: 0x001C2F5C
	public static QuestInstance InitializeQuest(Tag ownerId, Quest quest)
	{
		QuestInstance questInstance;
		if (!QuestManager.TryGetQuest(ownerId.GetHash(), quest, out questInstance))
		{
			questInstance = (QuestManager.instance.ownerToQuests[ownerId.GetHash()][quest.IdHash] = new QuestInstance(quest));
		}
		questInstance.Initialize(quest);
		return questInstance;
	}

	// Token: 0x06004E22 RID: 20002 RVA: 0x001C4DB0 File Offset: 0x001C2FB0
	public static QuestInstance InitializeQuest(HashedString ownerId, Quest quest)
	{
		QuestInstance questInstance;
		if (!QuestManager.TryGetQuest(ownerId.HashValue, quest, out questInstance))
		{
			questInstance = (QuestManager.instance.ownerToQuests[ownerId.HashValue][quest.IdHash] = new QuestInstance(quest));
		}
		questInstance.Initialize(quest);
		return questInstance;
	}

	// Token: 0x06004E23 RID: 20003 RVA: 0x001C4E04 File Offset: 0x001C3004
	public static QuestInstance GetInstance(Tag ownerId, Quest quest)
	{
		QuestInstance questInstance;
		QuestManager.TryGetQuest(ownerId.GetHash(), quest, out questInstance);
		return questInstance;
	}

	// Token: 0x06004E24 RID: 20004 RVA: 0x001C4E24 File Offset: 0x001C3024
	public static QuestInstance GetInstance(HashedString ownerId, Quest quest)
	{
		QuestInstance questInstance;
		QuestManager.TryGetQuest(ownerId.HashValue, quest, out questInstance);
		return questInstance;
	}

	// Token: 0x06004E25 RID: 20005 RVA: 0x001C4E44 File Offset: 0x001C3044
	public static bool CheckState(HashedString ownerId, Quest quest, Quest.State state)
	{
		QuestInstance questInstance;
		QuestManager.TryGetQuest(ownerId.HashValue, quest, out questInstance);
		return questInstance != null && questInstance.CurrentState == state;
	}

	// Token: 0x06004E26 RID: 20006 RVA: 0x001C4E70 File Offset: 0x001C3070
	public static bool CheckState(Tag ownerId, Quest quest, Quest.State state)
	{
		QuestInstance questInstance;
		QuestManager.TryGetQuest(ownerId.GetHash(), quest, out questInstance);
		return questInstance != null && questInstance.CurrentState == state;
	}

	// Token: 0x06004E27 RID: 20007 RVA: 0x001C4E9C File Offset: 0x001C309C
	private static bool TryGetQuest(int ownerId, Quest quest, out QuestInstance qInst)
	{
		qInst = null;
		Dictionary<HashedString, QuestInstance> dictionary;
		if (!QuestManager.instance.ownerToQuests.TryGetValue(ownerId, out dictionary))
		{
			dictionary = (QuestManager.instance.ownerToQuests[ownerId] = new Dictionary<HashedString, QuestInstance>());
		}
		return dictionary.TryGetValue(quest.IdHash, out qInst);
	}

	// Token: 0x040033E2 RID: 13282
	private static QuestManager instance;

	// Token: 0x040033E3 RID: 13283
	[Serialize]
	private Dictionary<int, Dictionary<HashedString, QuestInstance>> ownerToQuests = new Dictionary<int, Dictionary<HashedString, QuestInstance>>();
}
