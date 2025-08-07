using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using KSerialization;
using UnityEngine;

// Token: 0x0200060C RID: 1548
[AddComponentMenu("KMonoBehaviour/scripts/SaveManager")]
public class SaveManager : KMonoBehaviour
{
	// Token: 0x1400000B RID: 11
	// (add) Token: 0x060024C8 RID: 9416 RVA: 0x000D2058 File Offset: 0x000D0258
	// (remove) Token: 0x060024C9 RID: 9417 RVA: 0x000D2090 File Offset: 0x000D0290
	public event Action<SaveLoadRoot> onRegister;

	// Token: 0x1400000C RID: 12
	// (add) Token: 0x060024CA RID: 9418 RVA: 0x000D20C8 File Offset: 0x000D02C8
	// (remove) Token: 0x060024CB RID: 9419 RVA: 0x000D2100 File Offset: 0x000D0300
	public event Action<SaveLoadRoot> onUnregister;

	// Token: 0x060024CC RID: 9420 RVA: 0x000D2135 File Offset: 0x000D0335
	protected override void OnPrefabInit()
	{
		Assets.RegisterOnAddPrefab(new Action<KPrefabID>(this.OnAddPrefab));
	}

	// Token: 0x060024CD RID: 9421 RVA: 0x000D2148 File Offset: 0x000D0348
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Assets.UnregisterOnAddPrefab(new Action<KPrefabID>(this.OnAddPrefab));
	}

	// Token: 0x060024CE RID: 9422 RVA: 0x000D2164 File Offset: 0x000D0364
	private void OnAddPrefab(KPrefabID prefab)
	{
		if (prefab == null)
		{
			return;
		}
		Tag saveLoadTag = prefab.GetSaveLoadTag();
		this.prefabMap[saveLoadTag] = prefab.gameObject;
	}

	// Token: 0x060024CF RID: 9423 RVA: 0x000D2194 File Offset: 0x000D0394
	public Dictionary<Tag, List<SaveLoadRoot>> GetLists()
	{
		return this.sceneObjects;
	}

	// Token: 0x060024D0 RID: 9424 RVA: 0x000D219C File Offset: 0x000D039C
	private List<SaveLoadRoot> GetSaveLoadRootList(SaveLoadRoot saver)
	{
		KPrefabID component = saver.GetComponent<KPrefabID>();
		if (component == null)
		{
			DebugUtil.LogErrorArgs(saver.gameObject, new object[]
			{
				"All savers must also have a KPrefabID on them but",
				saver.gameObject.name,
				"does not have one."
			});
			return null;
		}
		List<SaveLoadRoot> list;
		if (!this.sceneObjects.TryGetValue(component.GetSaveLoadTag(), out list))
		{
			list = new List<SaveLoadRoot>();
			this.sceneObjects[component.GetSaveLoadTag()] = list;
		}
		return list;
	}

	// Token: 0x060024D1 RID: 9425 RVA: 0x000D2218 File Offset: 0x000D0418
	public void Register(SaveLoadRoot root)
	{
		List<SaveLoadRoot> saveLoadRootList = this.GetSaveLoadRootList(root);
		if (saveLoadRootList == null)
		{
			return;
		}
		saveLoadRootList.Add(root);
		if (this.onRegister != null)
		{
			this.onRegister(root);
		}
	}

	// Token: 0x060024D2 RID: 9426 RVA: 0x000D224C File Offset: 0x000D044C
	public void Unregister(SaveLoadRoot root)
	{
		if (this.onRegister != null)
		{
			this.onUnregister(root);
		}
		List<SaveLoadRoot> saveLoadRootList = this.GetSaveLoadRootList(root);
		if (saveLoadRootList == null)
		{
			return;
		}
		saveLoadRootList.Remove(root);
	}

	// Token: 0x060024D3 RID: 9427 RVA: 0x000D2284 File Offset: 0x000D0484
	public GameObject GetPrefab(Tag tag)
	{
		GameObject gameObject = null;
		if (this.prefabMap.TryGetValue(tag, out gameObject))
		{
			return gameObject;
		}
		DebugUtil.LogArgs(new object[]
		{
			"Item not found in prefabMap",
			"[" + tag.Name + "]"
		});
		return null;
	}

	// Token: 0x060024D4 RID: 9428 RVA: 0x000D22D4 File Offset: 0x000D04D4
	private void SortAssociatedObjects(ref List<Tag> objectTags, List<Tag> associatedTags)
	{
		int num = objectTags.FindIndex((Tag t) => associatedTags.Contains(t));
		if (num >= 0)
		{
			Tag tag = objectTags[num];
			foreach (Tag tag2 in associatedTags)
			{
				if (tag2 != tag && objectTags.Contains(tag2))
				{
					objectTags.Remove(tag2);
					objectTags.Insert(num + 1, tag2);
				}
			}
		}
	}

	// Token: 0x060024D5 RID: 9429 RVA: 0x000D237C File Offset: 0x000D057C
	public void Save(BinaryWriter writer)
	{
		writer.Write(SaveManager.SAVE_HEADER);
		writer.Write(7);
		writer.Write(36);
		int num = 0;
		Dictionary<Tag, List<Tag>> dictionary = new Dictionary<Tag, List<Tag>>();
		foreach (KeyValuePair<Tag, List<SaveLoadRoot>> keyValuePair in this.sceneObjects)
		{
			if (keyValuePair.Value.Count > 0)
			{
				num++;
				if (keyValuePair.Value[0].associatedTag != Tag.Invalid)
				{
					if (!dictionary.ContainsKey(keyValuePair.Value[0].associatedTag))
					{
						dictionary.Add(keyValuePair.Value[0].associatedTag, new List<Tag>());
					}
					dictionary[keyValuePair.Value[0].associatedTag].Add(keyValuePair.Key);
				}
			}
		}
		writer.Write(num);
		this.orderedKeys.Clear();
		this.orderedKeys.AddRange(this.sceneObjects.Keys);
		this.orderedKeys.Remove(SaveGame.Instance.PrefabID());
		this.orderedKeys = this.orderedKeys.OrderBy((Tag a) => a.Name == "StickerBomb").ToList<Tag>();
		this.orderedKeys = this.orderedKeys.OrderBy((Tag a) => a.Name.Contains("UnderConstruction")).ToList<Tag>();
		foreach (KeyValuePair<Tag, List<Tag>> keyValuePair2 in dictionary)
		{
			this.SortAssociatedObjects(ref this.orderedKeys, keyValuePair2.Value);
		}
		this.Write(SaveGame.Instance.PrefabID(), new List<SaveLoadRoot>(new SaveLoadRoot[] { SaveGame.Instance.GetComponent<SaveLoadRoot>() }), writer);
		foreach (Tag tag in this.orderedKeys)
		{
			List<SaveLoadRoot> list = this.sceneObjects[tag];
			if (list.Count > 0)
			{
				foreach (SaveLoadRoot saveLoadRoot in list)
				{
					if (!(saveLoadRoot == null) && saveLoadRoot.GetComponent<SimCellOccupier>() != null)
					{
						this.Write(tag, list, writer);
						break;
					}
				}
			}
		}
		foreach (Tag tag2 in this.orderedKeys)
		{
			List<SaveLoadRoot> list2 = this.sceneObjects[tag2];
			if (list2.Count > 0)
			{
				foreach (SaveLoadRoot saveLoadRoot2 in list2)
				{
					if (!(saveLoadRoot2 == null) && saveLoadRoot2.GetComponent<SimCellOccupier>() == null)
					{
						this.Write(tag2, list2, writer);
						break;
					}
				}
			}
		}
	}

	// Token: 0x060024D6 RID: 9430 RVA: 0x000D2704 File Offset: 0x000D0904
	private void Write(Tag key, List<SaveLoadRoot> value, BinaryWriter writer)
	{
		int count = value.Count;
		Tag tag = key;
		writer.WriteKleiString(tag.Name);
		writer.Write(count);
		long position = writer.BaseStream.Position;
		int num = -1;
		writer.Write(num);
		long position2 = writer.BaseStream.Position;
		foreach (SaveLoadRoot saveLoadRoot in value)
		{
			if (saveLoadRoot != null)
			{
				saveLoadRoot.Save(writer);
			}
			else
			{
				DebugUtil.LogWarningArgs(new object[] { "Null game object when saving" });
			}
		}
		long position3 = writer.BaseStream.Position;
		long num2 = position3 - position2;
		writer.BaseStream.Position = position;
		writer.Write((int)num2);
		writer.BaseStream.Position = position3;
	}

	// Token: 0x060024D7 RID: 9431 RVA: 0x000D27EC File Offset: 0x000D09EC
	public bool Load(IReader reader)
	{
		char[] array = reader.ReadChars(SaveManager.SAVE_HEADER.Length);
		if (array == null || array.Length != SaveManager.SAVE_HEADER.Length)
		{
			return false;
		}
		for (int i = 0; i < SaveManager.SAVE_HEADER.Length; i++)
		{
			if (array[i] != SaveManager.SAVE_HEADER[i])
			{
				return false;
			}
		}
		int num = reader.ReadInt32();
		int num2 = reader.ReadInt32();
		if (num != 7 || num2 > 36)
		{
			DebugUtil.LogWarningArgs(new object[] { string.Format("SAVE FILE VERSION MISMATCH! Expected {0}.{1} but got {2}.{3}", new object[] { 7, 36, num, num2 }) });
			return false;
		}
		this.ClearScene();
		try
		{
			int num3 = reader.ReadInt32();
			for (int j = 0; j < num3; j++)
			{
				string text = reader.ReadKleiString();
				int num4 = reader.ReadInt32();
				int num5 = reader.ReadInt32();
				Tag tag = TagManager.Create(text);
				GameObject gameObject;
				if (!this.prefabMap.TryGetValue(tag, out gameObject))
				{
					DebugUtil.LogWarningArgs(new object[] { "Could not find prefab '" + text + "'" });
					reader.SkipBytes(num5);
				}
				else
				{
					List<SaveLoadRoot> list = new List<SaveLoadRoot>(num4);
					this.sceneObjects[tag] = list;
					for (int k = 0; k < num4; k++)
					{
						SaveLoadRoot saveLoadRoot = SaveLoadRoot.Load(gameObject, reader);
						if (SaveManager.DEBUG_OnlyLoadThisCellsObjects == -1 && saveLoadRoot == null)
						{
							global::Debug.LogError("Error loading data [" + text + "]");
							return false;
						}
					}
				}
			}
		}
		catch (Exception ex)
		{
			DebugUtil.LogErrorArgs(new object[]
			{
				"Error deserializing prefabs\n\n",
				ex.ToString()
			});
			throw ex;
		}
		return true;
	}

	// Token: 0x060024D8 RID: 9432 RVA: 0x000D29B0 File Offset: 0x000D0BB0
	private void ClearScene()
	{
		foreach (KeyValuePair<Tag, List<SaveLoadRoot>> keyValuePair in this.sceneObjects)
		{
			foreach (SaveLoadRoot saveLoadRoot in keyValuePair.Value)
			{
				global::UnityEngine.Object.Destroy(saveLoadRoot.gameObject);
			}
		}
		this.sceneObjects.Clear();
	}

	// Token: 0x04001576 RID: 5494
	public const int SAVE_MAJOR_VERSION_LAST_UNDOCUMENTED = 7;

	// Token: 0x04001577 RID: 5495
	public const int SAVE_MAJOR_VERSION = 7;

	// Token: 0x04001578 RID: 5496
	public const int SAVE_MINOR_VERSION_EXPLICIT_VALUE_TYPES = 4;

	// Token: 0x04001579 RID: 5497
	public const int SAVE_MINOR_VERSION_LAST_UNDOCUMENTED = 7;

	// Token: 0x0400157A RID: 5498
	public const int SAVE_MINOR_VERSION_MOD_IDENTIFIER = 8;

	// Token: 0x0400157B RID: 5499
	public const int SAVE_MINOR_VERSION_FINITE_SPACE_RESOURCES = 9;

	// Token: 0x0400157C RID: 5500
	public const int SAVE_MINOR_VERSION_COLONY_REQ_ACHIEVEMENTS = 10;

	// Token: 0x0400157D RID: 5501
	public const int SAVE_MINOR_VERSION_TRACK_NAV_DISTANCE = 11;

	// Token: 0x0400157E RID: 5502
	public const int SAVE_MINOR_VERSION_EXPANDED_WORLD_INFO = 12;

	// Token: 0x0400157F RID: 5503
	public const int SAVE_MINOR_VERSION_BASIC_COMFORTS_FIX = 13;

	// Token: 0x04001580 RID: 5504
	public const int SAVE_MINOR_VERSION_PLATFORM_TRAIT_NAMES = 14;

	// Token: 0x04001581 RID: 5505
	public const int SAVE_MINOR_VERSION_ADD_JOY_REACTIONS = 15;

	// Token: 0x04001582 RID: 5506
	public const int SAVE_MINOR_VERSION_NEW_AUTOMATION_WARNING = 16;

	// Token: 0x04001583 RID: 5507
	public const int SAVE_MINOR_VERSION_ADD_GUID_TO_HEADER = 17;

	// Token: 0x04001584 RID: 5508
	public const int SAVE_MINOR_VERSION_EXPANSION_1_INTRODUCED = 20;

	// Token: 0x04001585 RID: 5509
	public const int SAVE_MINOR_VERSION_CONTENT_SETTINGS = 21;

	// Token: 0x04001586 RID: 5510
	public const int SAVE_MINOR_VERSION_COLONY_REQ_REMOVE_SERIALIZATION = 22;

	// Token: 0x04001587 RID: 5511
	public const int SAVE_MINOR_VERSION_ROTTABLE_TUNING = 23;

	// Token: 0x04001588 RID: 5512
	public const int SAVE_MINOR_VERSION_LAUNCH_PAD_SOLIDITY = 24;

	// Token: 0x04001589 RID: 5513
	public const int SAVE_MINOR_VERSION_BASE_GAME_MERGEDOWN = 25;

	// Token: 0x0400158A RID: 5514
	public const int SAVE_MINOR_VERSION_FALLING_WATER_WORLDIDX_SERIALIZATION = 26;

	// Token: 0x0400158B RID: 5515
	public const int SAVE_MINOR_VERSION_ROCKET_RANGE_REBALANCE = 27;

	// Token: 0x0400158C RID: 5516
	public const int SAVE_MINOR_VERSION_ENTITIES_WRONG_LAYER = 28;

	// Token: 0x0400158D RID: 5517
	public const int SAVE_MINOR_VERSION_TAGBITS_REWORK = 29;

	// Token: 0x0400158E RID: 5518
	public const int SAVE_MINOR_VERSION_ACCESSORY_SLOT_UPGRADE = 30;

	// Token: 0x0400158F RID: 5519
	public const int SAVE_MINOR_VERSION_GEYSER_CAN_BE_RENAMED = 31;

	// Token: 0x04001590 RID: 5520
	public const int SAVE_MINOR_VERSION_SPACE_SCANNERS_TELESCOPES = 32;

	// Token: 0x04001591 RID: 5521
	public const int SAVE_MINOR_VERSION_U50_CRITTERS = 33;

	// Token: 0x04001592 RID: 5522
	public const int SAVE_MINOR_VERSION_DLC_ADD_ONS = 34;

	// Token: 0x04001593 RID: 5523
	public const int SAVE_MINOR_VERSION_U53_SCHEDULES = 35;

	// Token: 0x04001594 RID: 5524
	public const int SAVE_MINOR_VERSION_POKESHELL_MOLTS = 36;

	// Token: 0x04001595 RID: 5525
	public const int SAVE_MINOR_VERSION = 36;

	// Token: 0x04001596 RID: 5526
	private Dictionary<Tag, GameObject> prefabMap = new Dictionary<Tag, GameObject>();

	// Token: 0x04001597 RID: 5527
	private Dictionary<Tag, List<SaveLoadRoot>> sceneObjects = new Dictionary<Tag, List<SaveLoadRoot>>();

	// Token: 0x0400159A RID: 5530
	public static int DEBUG_OnlyLoadThisCellsObjects = -1;

	// Token: 0x0400159B RID: 5531
	private static readonly char[] SAVE_HEADER = new char[] { 'K', 'S', 'A', 'V' };

	// Token: 0x0400159C RID: 5532
	private List<Tag> orderedKeys = new List<Tag>();

	// Token: 0x020014A5 RID: 5285
	private enum BoundaryTag : uint
	{
		// Token: 0x04006D62 RID: 28002
		Component = 3735928559U,
		// Token: 0x04006D63 RID: 28003
		Prefab = 3131961357U,
		// Token: 0x04006D64 RID: 28004
		Complete = 3735929054U
	}
}
