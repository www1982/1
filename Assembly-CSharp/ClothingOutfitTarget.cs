using System;
using System.Collections.Generic;
using System.Linq;
using Database;
using STRINGS;
using UnityEngine;

// Token: 0x02000812 RID: 2066
public readonly struct ClothingOutfitTarget : IEquatable<ClothingOutfitTarget>
{
	// Token: 0x170003CD RID: 973
	// (get) Token: 0x06003862 RID: 14434 RVA: 0x0013941B File Offset: 0x0013761B
	public string OutfitId
	{
		get
		{
			return this.impl.OutfitId;
		}
	}

	// Token: 0x170003CE RID: 974
	// (get) Token: 0x06003863 RID: 14435 RVA: 0x00139428 File Offset: 0x00137628
	public ClothingOutfitUtility.OutfitType OutfitType
	{
		get
		{
			return this.impl.OutfitType;
		}
	}

	// Token: 0x06003864 RID: 14436 RVA: 0x00139435 File Offset: 0x00137635
	public string[] ReadItems()
	{
		return this.impl.ReadItems(this.OutfitType).Where(new Func<string, bool>(ClothingOutfitTarget.DoesClothingItemExist)).ToArray<string>();
	}

	// Token: 0x06003865 RID: 14437 RVA: 0x0013945E File Offset: 0x0013765E
	public void WriteItems(ClothingOutfitUtility.OutfitType outfitType, string[] items)
	{
		this.impl.WriteItems(outfitType, items);
	}

	// Token: 0x170003CF RID: 975
	// (get) Token: 0x06003866 RID: 14438 RVA: 0x0013946D File Offset: 0x0013766D
	public bool CanWriteItems
	{
		get
		{
			return this.impl.CanWriteItems;
		}
	}

	// Token: 0x06003867 RID: 14439 RVA: 0x0013947A File Offset: 0x0013767A
	public string ReadName()
	{
		return this.impl.ReadName();
	}

	// Token: 0x06003868 RID: 14440 RVA: 0x00139487 File Offset: 0x00137687
	public void WriteName(string name)
	{
		this.impl.WriteName(name);
	}

	// Token: 0x170003D0 RID: 976
	// (get) Token: 0x06003869 RID: 14441 RVA: 0x00139495 File Offset: 0x00137695
	public bool CanWriteName
	{
		get
		{
			return this.impl.CanWriteName;
		}
	}

	// Token: 0x0600386A RID: 14442 RVA: 0x001394A2 File Offset: 0x001376A2
	public void Delete()
	{
		this.impl.Delete();
	}

	// Token: 0x170003D1 RID: 977
	// (get) Token: 0x0600386B RID: 14443 RVA: 0x001394AF File Offset: 0x001376AF
	public bool CanDelete
	{
		get
		{
			return this.impl.CanDelete;
		}
	}

	// Token: 0x0600386C RID: 14444 RVA: 0x001394BC File Offset: 0x001376BC
	public bool DoesExist()
	{
		return this.impl.DoesExist();
	}

	// Token: 0x0600386D RID: 14445 RVA: 0x001394C9 File Offset: 0x001376C9
	public ClothingOutfitTarget(ClothingOutfitTarget.Implementation impl)
	{
		this.impl = impl;
	}

	// Token: 0x0600386E RID: 14446 RVA: 0x001394D2 File Offset: 0x001376D2
	public bool DoesContainLockedItems()
	{
		return ClothingOutfitTarget.DoesContainLockedItems(this.ReadItems());
	}

	// Token: 0x0600386F RID: 14447 RVA: 0x001394E0 File Offset: 0x001376E0
	public static bool DoesContainLockedItems(IList<string> itemIds)
	{
		foreach (string text in itemIds)
		{
			PermitResource permitResource = Db.Get().Permits.TryGet(text);
			if (permitResource != null && !permitResource.IsUnlocked())
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06003870 RID: 14448 RVA: 0x00139544 File Offset: 0x00137744
	public IEnumerable<ClothingItemResource> ReadItemValues()
	{
		return from i in this.ReadItems()
			select Db.Get().Permits.ClothingItems.Get(i);
	}

	// Token: 0x06003871 RID: 14449 RVA: 0x00139570 File Offset: 0x00137770
	public static bool DoesClothingItemExist(string clothingItemId)
	{
		return !Db.Get().Permits.ClothingItems.TryGet(clothingItemId).IsNullOrDestroyed();
	}

	// Token: 0x06003872 RID: 14450 RVA: 0x0013958F File Offset: 0x0013778F
	public bool Is<T>() where T : ClothingOutfitTarget.Implementation
	{
		return this.impl is T;
	}

	// Token: 0x06003873 RID: 14451 RVA: 0x001395A0 File Offset: 0x001377A0
	public bool Is<T>(out T value) where T : ClothingOutfitTarget.Implementation
	{
		ClothingOutfitTarget.Implementation implementation = this.impl;
		if (implementation is T)
		{
			T t = (T)((object)implementation);
			value = t;
			return true;
		}
		value = default(T);
		return false;
	}

	// Token: 0x06003874 RID: 14452 RVA: 0x001395D4 File Offset: 0x001377D4
	public bool IsTemplateOutfit()
	{
		return this.Is<ClothingOutfitTarget.DatabaseAuthoredTemplate>() || this.Is<ClothingOutfitTarget.UserAuthoredTemplate>();
	}

	// Token: 0x06003875 RID: 14453 RVA: 0x001395E6 File Offset: 0x001377E6
	public static ClothingOutfitTarget ForNewTemplateOutfit(ClothingOutfitUtility.OutfitType outfitType)
	{
		return new ClothingOutfitTarget(new ClothingOutfitTarget.UserAuthoredTemplate(outfitType, ClothingOutfitTarget.GetUniqueNameIdFrom(UI.OUTFIT_NAME.NEW)));
	}

	// Token: 0x06003876 RID: 14454 RVA: 0x00139607 File Offset: 0x00137807
	public static ClothingOutfitTarget ForNewTemplateOutfit(ClothingOutfitUtility.OutfitType outfitType, string id)
	{
		if (ClothingOutfitTarget.DoesTemplateExist(id))
		{
			throw new ArgumentException("Can not create a new target with id " + id + ", an outfit with that id already exists");
		}
		return new ClothingOutfitTarget(new ClothingOutfitTarget.UserAuthoredTemplate(outfitType, id));
	}

	// Token: 0x06003877 RID: 14455 RVA: 0x00139638 File Offset: 0x00137838
	public static ClothingOutfitTarget ForTemplateCopyOf(ClothingOutfitTarget sourceTarget)
	{
		return new ClothingOutfitTarget(new ClothingOutfitTarget.UserAuthoredTemplate(sourceTarget.OutfitType, ClothingOutfitTarget.GetUniqueNameIdFrom(UI.OUTFIT_NAME.COPY_OF.Replace("{OutfitName}", sourceTarget.ReadName()))));
	}

	// Token: 0x06003878 RID: 14456 RVA: 0x0013966B File Offset: 0x0013786B
	public static ClothingOutfitTarget FromMinion(ClothingOutfitUtility.OutfitType outfitType, GameObject minionInstance)
	{
		return new ClothingOutfitTarget(new ClothingOutfitTarget.MinionInstance(outfitType, minionInstance));
	}

	// Token: 0x06003879 RID: 14457 RVA: 0x00139680 File Offset: 0x00137880
	public static ClothingOutfitTarget FromTemplateId(string outfitId)
	{
		return ClothingOutfitTarget.TryFromTemplateId(outfitId).Value;
	}

	// Token: 0x0600387A RID: 14458 RVA: 0x0013969C File Offset: 0x0013789C
	public static Option<ClothingOutfitTarget> TryFromTemplateId(string outfitId)
	{
		if (outfitId == null)
		{
			return Option.None;
		}
		SerializableOutfitData.Version2.CustomTemplateOutfitEntry customTemplateOutfitEntry;
		ClothingOutfitUtility.OutfitType outfitType;
		if (CustomClothingOutfits.Instance.Internal_GetOutfitData().OutfitIdToUserAuthoredTemplateOutfit.TryGetValue(outfitId, out customTemplateOutfitEntry) && Enum.TryParse<ClothingOutfitUtility.OutfitType>(customTemplateOutfitEntry.outfitType, true, out outfitType))
		{
			return new ClothingOutfitTarget(new ClothingOutfitTarget.UserAuthoredTemplate(outfitType, outfitId));
		}
		ClothingOutfitResource clothingOutfitResource = Db.Get().Permits.ClothingOutfits.TryGet(outfitId);
		if (!clothingOutfitResource.IsNullOrDestroyed())
		{
			return new ClothingOutfitTarget(new ClothingOutfitTarget.DatabaseAuthoredTemplate(clothingOutfitResource));
		}
		return Option.None;
	}

	// Token: 0x0600387B RID: 14459 RVA: 0x00139735 File Offset: 0x00137935
	public static bool DoesTemplateExist(string outfitId)
	{
		return Db.Get().Permits.ClothingOutfits.TryGet(outfitId) != null || CustomClothingOutfits.Instance.Internal_GetOutfitData().OutfitIdToUserAuthoredTemplateOutfit.ContainsKey(outfitId);
	}

	// Token: 0x0600387C RID: 14460 RVA: 0x0013976A File Offset: 0x0013796A
	public static IEnumerable<ClothingOutfitTarget> GetAllTemplates()
	{
		foreach (ClothingOutfitResource clothingOutfitResource in Db.Get().Permits.ClothingOutfits.resources)
		{
			yield return new ClothingOutfitTarget(new ClothingOutfitTarget.DatabaseAuthoredTemplate(clothingOutfitResource));
		}
		List<ClothingOutfitResource>.Enumerator enumerator = default(List<ClothingOutfitResource>.Enumerator);
		foreach (KeyValuePair<string, SerializableOutfitData.Version2.CustomTemplateOutfitEntry> keyValuePair in CustomClothingOutfits.Instance.Internal_GetOutfitData().OutfitIdToUserAuthoredTemplateOutfit)
		{
			string text;
			SerializableOutfitData.Version2.CustomTemplateOutfitEntry customTemplateOutfitEntry;
			keyValuePair.Deconstruct(out text, out customTemplateOutfitEntry);
			string text2 = text;
			ClothingOutfitUtility.OutfitType outfitType;
			if (Enum.TryParse<ClothingOutfitUtility.OutfitType>(customTemplateOutfitEntry.outfitType, true, out outfitType))
			{
				yield return new ClothingOutfitTarget(new ClothingOutfitTarget.UserAuthoredTemplate(outfitType, text2));
			}
		}
		Dictionary<string, SerializableOutfitData.Version2.CustomTemplateOutfitEntry>.Enumerator enumerator2 = default(Dictionary<string, SerializableOutfitData.Version2.CustomTemplateOutfitEntry>.Enumerator);
		yield break;
		yield break;
	}

	// Token: 0x0600387D RID: 14461 RVA: 0x00139773 File Offset: 0x00137973
	public static ClothingOutfitTarget GetRandom()
	{
		return ClothingOutfitTarget.GetAllTemplates().GetRandom<ClothingOutfitTarget>();
	}

	// Token: 0x0600387E RID: 14462 RVA: 0x00139780 File Offset: 0x00137980
	public static Option<ClothingOutfitTarget> GetRandom(ClothingOutfitUtility.OutfitType onlyOfType)
	{
		IEnumerable<ClothingOutfitTarget> enumerable = from t in ClothingOutfitTarget.GetAllTemplates()
			where t.OutfitType == onlyOfType
			select t;
		if (enumerable == null || enumerable.Count<ClothingOutfitTarget>() == 0)
		{
			return Option.None;
		}
		return enumerable.GetRandom<ClothingOutfitTarget>();
	}

	// Token: 0x0600387F RID: 14463 RVA: 0x001397D4 File Offset: 0x001379D4
	public static string GetUniqueNameIdFrom(string preferredName)
	{
		if (!ClothingOutfitTarget.DoesTemplateExist(preferredName))
		{
			return preferredName;
		}
		string text = "testOutfit";
		string text2 = UI.OUTFIT_NAME.RESOLVE_CONFLICT.Replace("{OutfitName}", text).Replace("{ConflictNumber}", 1.ToString());
		string text3 = UI.OUTFIT_NAME.RESOLVE_CONFLICT.Replace("{OutfitName}", text).Replace("{ConflictNumber}", 2.ToString());
		string text4;
		if (text2 != text3)
		{
			text4 = UI.OUTFIT_NAME.RESOLVE_CONFLICT;
		}
		else
		{
			text4 = "{OutfitName} ({ConflictNumber})";
		}
		for (int i = 1; i < 10000; i++)
		{
			string text5 = text4.Replace("{OutfitName}", preferredName).Replace("{ConflictNumber}", i.ToString());
			if (!ClothingOutfitTarget.DoesTemplateExist(text5))
			{
				return text5;
			}
		}
		throw new Exception("Couldn't get a unique name for preferred name: " + preferredName);
	}

	// Token: 0x06003880 RID: 14464 RVA: 0x001398A2 File Offset: 0x00137AA2
	public static bool operator ==(ClothingOutfitTarget a, ClothingOutfitTarget b)
	{
		return a.Equals(b);
	}

	// Token: 0x06003881 RID: 14465 RVA: 0x001398AC File Offset: 0x00137AAC
	public static bool operator !=(ClothingOutfitTarget a, ClothingOutfitTarget b)
	{
		return !a.Equals(b);
	}

	// Token: 0x06003882 RID: 14466 RVA: 0x001398BC File Offset: 0x00137ABC
	public override bool Equals(object obj)
	{
		if (obj is ClothingOutfitTarget)
		{
			ClothingOutfitTarget clothingOutfitTarget = (ClothingOutfitTarget)obj;
			return this.Equals(clothingOutfitTarget);
		}
		return false;
	}

	// Token: 0x06003883 RID: 14467 RVA: 0x001398E1 File Offset: 0x00137AE1
	public bool Equals(ClothingOutfitTarget other)
	{
		if (this.impl == null || other.impl == null)
		{
			return this.impl == null == (other.impl == null);
		}
		return this.OutfitId == other.OutfitId;
	}

	// Token: 0x06003884 RID: 14468 RVA: 0x0013991A File Offset: 0x00137B1A
	public override int GetHashCode()
	{
		return Hash.SDBMLower(this.impl.OutfitId);
	}

	// Token: 0x04002238 RID: 8760
	public readonly ClothingOutfitTarget.Implementation impl;

	// Token: 0x04002239 RID: 8761
	public static readonly string[] NO_ITEMS = new string[0];

	// Token: 0x0400223A RID: 8762
	public static readonly ClothingItemResource[] NO_ITEM_VALUES = new ClothingItemResource[0];

	// Token: 0x02001772 RID: 6002
	public interface Implementation
	{
		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x060098FC RID: 39164
		string OutfitId { get; }

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x060098FD RID: 39165
		ClothingOutfitUtility.OutfitType OutfitType { get; }

		// Token: 0x060098FE RID: 39166
		string[] ReadItems(ClothingOutfitUtility.OutfitType outfitType);

		// Token: 0x060098FF RID: 39167
		void WriteItems(ClothingOutfitUtility.OutfitType outfitType, string[] items);

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x06009900 RID: 39168
		bool CanWriteItems { get; }

		// Token: 0x06009901 RID: 39169
		string ReadName();

		// Token: 0x06009902 RID: 39170
		void WriteName(string name);

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x06009903 RID: 39171
		bool CanWriteName { get; }

		// Token: 0x06009904 RID: 39172
		void Delete();

		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x06009905 RID: 39173
		bool CanDelete { get; }

		// Token: 0x06009906 RID: 39174
		bool DoesExist();
	}

	// Token: 0x02001773 RID: 6003
	public readonly struct MinionInstance : ClothingOutfitTarget.Implementation
	{
		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x06009907 RID: 39175 RVA: 0x003833AA File Offset: 0x003815AA
		public bool CanWriteItems
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x06009908 RID: 39176 RVA: 0x003833AD File Offset: 0x003815AD
		public bool CanWriteName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x06009909 RID: 39177 RVA: 0x003833B0 File Offset: 0x003815B0
		public bool CanDelete
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600990A RID: 39178 RVA: 0x003833B3 File Offset: 0x003815B3
		public bool DoesExist()
		{
			return !this.minionInstance.IsNullOrDestroyed();
		}

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x0600990B RID: 39179 RVA: 0x003833C4 File Offset: 0x003815C4
		public string OutfitId
		{
			get
			{
				return this.minionInstance.GetInstanceID().ToString() + "_outfit";
			}
		}

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x0600990C RID: 39180 RVA: 0x003833EE File Offset: 0x003815EE
		public ClothingOutfitUtility.OutfitType OutfitType
		{
			get
			{
				return this.m_outfitType;
			}
		}

		// Token: 0x0600990D RID: 39181 RVA: 0x003833F6 File Offset: 0x003815F6
		public MinionInstance(ClothingOutfitUtility.OutfitType outfitType, GameObject minionInstance)
		{
			this.minionInstance = minionInstance;
			this.m_outfitType = outfitType;
			this.accessorizer = minionInstance.GetComponent<WearableAccessorizer>();
		}

		// Token: 0x0600990E RID: 39182 RVA: 0x00383412 File Offset: 0x00381612
		public string[] ReadItems(ClothingOutfitUtility.OutfitType outfitType)
		{
			return this.accessorizer.GetClothingItemsIds(outfitType);
		}

		// Token: 0x0600990F RID: 39183 RVA: 0x00383420 File Offset: 0x00381620
		public void WriteItems(ClothingOutfitUtility.OutfitType outfitType, string[] items)
		{
			this.accessorizer.ClearClothingItems(new ClothingOutfitUtility.OutfitType?(outfitType));
			this.accessorizer.ApplyClothingItems(outfitType, items.Select((string i) => Db.Get().Permits.ClothingItems.Get(i)));
		}

		// Token: 0x06009910 RID: 39184 RVA: 0x0038346F File Offset: 0x0038166F
		public string ReadName()
		{
			return UI.OUTFIT_NAME.MINIONS_OUTFIT.Replace("{MinionName}", this.minionInstance.GetProperName());
		}

		// Token: 0x06009911 RID: 39185 RVA: 0x0038348B File Offset: 0x0038168B
		public void WriteName(string name)
		{
			throw new InvalidOperationException("Can not change change the outfit id for a minion instance");
		}

		// Token: 0x06009912 RID: 39186 RVA: 0x00383497 File Offset: 0x00381697
		public void Delete()
		{
			throw new InvalidOperationException("Can not delete a minion instance outfit");
		}

		// Token: 0x040075B7 RID: 30135
		private readonly ClothingOutfitUtility.OutfitType m_outfitType;

		// Token: 0x040075B8 RID: 30136
		public readonly GameObject minionInstance;

		// Token: 0x040075B9 RID: 30137
		public readonly WearableAccessorizer accessorizer;
	}

	// Token: 0x02001774 RID: 6004
	public readonly struct UserAuthoredTemplate : ClothingOutfitTarget.Implementation
	{
		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x06009913 RID: 39187 RVA: 0x003834A3 File Offset: 0x003816A3
		public bool CanWriteItems
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x06009914 RID: 39188 RVA: 0x003834A6 File Offset: 0x003816A6
		public bool CanWriteName
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x06009915 RID: 39189 RVA: 0x003834A9 File Offset: 0x003816A9
		public bool CanDelete
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06009916 RID: 39190 RVA: 0x003834AC File Offset: 0x003816AC
		public bool DoesExist()
		{
			return CustomClothingOutfits.Instance.Internal_GetOutfitData().OutfitIdToUserAuthoredTemplateOutfit.ContainsKey(this.OutfitId);
		}

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x06009917 RID: 39191 RVA: 0x003834C8 File Offset: 0x003816C8
		public string OutfitId
		{
			get
			{
				return this.m_outfitId[0];
			}
		}

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x06009918 RID: 39192 RVA: 0x003834D2 File Offset: 0x003816D2
		public ClothingOutfitUtility.OutfitType OutfitType
		{
			get
			{
				return this.m_outfitType;
			}
		}

		// Token: 0x06009919 RID: 39193 RVA: 0x003834DA File Offset: 0x003816DA
		public UserAuthoredTemplate(ClothingOutfitUtility.OutfitType outfitType, string outfitId)
		{
			this.m_outfitId = new string[] { outfitId };
			this.m_outfitType = outfitType;
		}

		// Token: 0x0600991A RID: 39194 RVA: 0x003834F4 File Offset: 0x003816F4
		public string[] ReadItems(ClothingOutfitUtility.OutfitType outfitType)
		{
			SerializableOutfitData.Version2.CustomTemplateOutfitEntry customTemplateOutfitEntry;
			if (CustomClothingOutfits.Instance.Internal_GetOutfitData().OutfitIdToUserAuthoredTemplateOutfit.TryGetValue(this.OutfitId, out customTemplateOutfitEntry))
			{
				ClothingOutfitUtility.OutfitType outfitType2;
				global::Debug.Assert(Enum.TryParse<ClothingOutfitUtility.OutfitType>(customTemplateOutfitEntry.outfitType, true, out outfitType2) && outfitType2 == this.m_outfitType);
				return customTemplateOutfitEntry.itemIds;
			}
			return ClothingOutfitTarget.NO_ITEMS;
		}

		// Token: 0x0600991B RID: 39195 RVA: 0x0038354C File Offset: 0x0038174C
		public void WriteItems(ClothingOutfitUtility.OutfitType outfitType, string[] items)
		{
			CustomClothingOutfits.Instance.Internal_EditOutfit(outfitType, this.OutfitId, items);
		}

		// Token: 0x0600991C RID: 39196 RVA: 0x00383560 File Offset: 0x00381760
		public string ReadName()
		{
			return this.OutfitId;
		}

		// Token: 0x0600991D RID: 39197 RVA: 0x00383568 File Offset: 0x00381768
		public void WriteName(string name)
		{
			if (this.OutfitId == name)
			{
				return;
			}
			if (ClothingOutfitTarget.DoesTemplateExist(name))
			{
				throw new Exception(string.Concat(new string[] { "Can not change outfit name from \"", this.OutfitId, "\" to \"", name, "\", \"", name, "\" already exists" }));
			}
			if (CustomClothingOutfits.Instance.Internal_GetOutfitData().OutfitIdToUserAuthoredTemplateOutfit.ContainsKey(this.OutfitId))
			{
				CustomClothingOutfits.Instance.Internal_RenameOutfit(this.m_outfitType, this.OutfitId, name);
			}
			else
			{
				CustomClothingOutfits.Instance.Internal_EditOutfit(this.m_outfitType, name, ClothingOutfitTarget.NO_ITEMS);
			}
			this.m_outfitId[0] = name;
		}

		// Token: 0x0600991E RID: 39198 RVA: 0x00383622 File Offset: 0x00381822
		public void Delete()
		{
			CustomClothingOutfits.Instance.Internal_RemoveOutfit(this.m_outfitType, this.OutfitId);
		}

		// Token: 0x040075BA RID: 30138
		private readonly string[] m_outfitId;

		// Token: 0x040075BB RID: 30139
		private readonly ClothingOutfitUtility.OutfitType m_outfitType;
	}

	// Token: 0x02001775 RID: 6005
	public readonly struct DatabaseAuthoredTemplate : ClothingOutfitTarget.Implementation
	{
		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x0600991F RID: 39199 RVA: 0x0038363A File Offset: 0x0038183A
		public bool CanWriteItems
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x06009920 RID: 39200 RVA: 0x0038363D File Offset: 0x0038183D
		public bool CanWriteName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x06009921 RID: 39201 RVA: 0x00383640 File Offset: 0x00381840
		public bool CanDelete
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06009922 RID: 39202 RVA: 0x00383643 File Offset: 0x00381843
		public bool DoesExist()
		{
			return true;
		}

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x06009923 RID: 39203 RVA: 0x00383646 File Offset: 0x00381846
		public string OutfitId
		{
			get
			{
				return this.m_outfitId;
			}
		}

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x06009924 RID: 39204 RVA: 0x0038364E File Offset: 0x0038184E
		public ClothingOutfitUtility.OutfitType OutfitType
		{
			get
			{
				return this.m_outfitType;
			}
		}

		// Token: 0x06009925 RID: 39205 RVA: 0x00383656 File Offset: 0x00381856
		public DatabaseAuthoredTemplate(ClothingOutfitResource outfit)
		{
			this.m_outfitId = outfit.Id;
			this.m_outfitType = outfit.outfitType;
			this.resource = outfit;
		}

		// Token: 0x06009926 RID: 39206 RVA: 0x00383677 File Offset: 0x00381877
		public string[] ReadItems(ClothingOutfitUtility.OutfitType outfitType)
		{
			return this.resource.itemsInOutfit;
		}

		// Token: 0x06009927 RID: 39207 RVA: 0x00383684 File Offset: 0x00381884
		public void WriteItems(ClothingOutfitUtility.OutfitType outfitType, string[] items)
		{
			throw new InvalidOperationException("Can not set items on a Db authored outfit");
		}

		// Token: 0x06009928 RID: 39208 RVA: 0x00383690 File Offset: 0x00381890
		public string ReadName()
		{
			return this.resource.Name;
		}

		// Token: 0x06009929 RID: 39209 RVA: 0x0038369D File Offset: 0x0038189D
		public void WriteName(string name)
		{
			throw new InvalidOperationException("Can not set name on a Db authored outfit");
		}

		// Token: 0x0600992A RID: 39210 RVA: 0x003836A9 File Offset: 0x003818A9
		public void Delete()
		{
			throw new InvalidOperationException("Can not delete a Db authored outfit");
		}

		// Token: 0x040075BC RID: 30140
		public readonly ClothingOutfitResource resource;

		// Token: 0x040075BD RID: 30141
		private readonly string m_outfitId;

		// Token: 0x040075BE RID: 30142
		private readonly ClothingOutfitUtility.OutfitType m_outfitType;
	}
}
