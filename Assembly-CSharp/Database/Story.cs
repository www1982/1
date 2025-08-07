using System;
using ProcGen;

namespace Database
{
	// Token: 0x02000F1B RID: 3867
	public class Story : Resource, IComparable<Story>
	{
		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x06007A22 RID: 31266 RVA: 0x00307C69 File Offset: 0x00305E69
		// (set) Token: 0x06007A23 RID: 31267 RVA: 0x00307C71 File Offset: 0x00305E71
		public int HashId { get; private set; }

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x06007A24 RID: 31268 RVA: 0x00307C7A File Offset: 0x00305E7A
		public WorldTrait StoryTrait
		{
			get
			{
				if (this._cachedStoryTrait == null)
				{
					this._cachedStoryTrait = SettingsCache.GetCachedStoryTrait(this.worldgenStoryTraitKey, false);
				}
				return this._cachedStoryTrait;
			}
		}

		// Token: 0x06007A25 RID: 31269 RVA: 0x00307C9C File Offset: 0x00305E9C
		public Story(string id, string worldgenStoryTraitKey, int displayOrder)
		{
			this.Id = id;
			this.worldgenStoryTraitKey = worldgenStoryTraitKey;
			this.displayOrder = displayOrder;
			this.kleiUseOnlyCoordinateOrder = -1;
			this.updateNumber = -1;
			this.sandboxStampTemplateId = null;
			this.HashId = Hash.SDBMLower(id);
		}

		// Token: 0x06007A26 RID: 31270 RVA: 0x00307CDC File Offset: 0x00305EDC
		public Story(string id, string worldgenStoryTraitKey, int displayOrder, int kleiUseOnlyCoordinateOrder, int updateNumber, string sandboxStampTemplateId)
		{
			this.Id = id;
			this.worldgenStoryTraitKey = worldgenStoryTraitKey;
			this.displayOrder = displayOrder;
			this.updateNumber = updateNumber;
			this.sandboxStampTemplateId = sandboxStampTemplateId;
			this.kleiUseOnlyCoordinateOrder = kleiUseOnlyCoordinateOrder;
			this.HashId = Hash.SDBMLower(id);
		}

		// Token: 0x06007A27 RID: 31271 RVA: 0x00307D28 File Offset: 0x00305F28
		public int CompareTo(Story other)
		{
			return this.displayOrder.CompareTo(other.displayOrder);
		}

		// Token: 0x06007A28 RID: 31272 RVA: 0x00307D49 File Offset: 0x00305F49
		public bool IsNew()
		{
			return this.updateNumber == LaunchInitializer.UpdateNumber();
		}

		// Token: 0x06007A29 RID: 31273 RVA: 0x00307D58 File Offset: 0x00305F58
		public Story AutoStart()
		{
			this.autoStart = true;
			return this;
		}

		// Token: 0x06007A2A RID: 31274 RVA: 0x00307D62 File Offset: 0x00305F62
		public Story SetKeepsake(string prefabId)
		{
			this.keepsakePrefabId = prefabId;
			return this;
		}

		// Token: 0x0400599B RID: 22939
		public const int MODDED_STORY = -1;

		// Token: 0x0400599C RID: 22940
		public int kleiUseOnlyCoordinateOrder;

		// Token: 0x0400599E RID: 22942
		public bool autoStart;

		// Token: 0x0400599F RID: 22943
		public string keepsakePrefabId;

		// Token: 0x040059A0 RID: 22944
		public readonly string worldgenStoryTraitKey;

		// Token: 0x040059A1 RID: 22945
		private readonly int displayOrder;

		// Token: 0x040059A2 RID: 22946
		private readonly int updateNumber;

		// Token: 0x040059A3 RID: 22947
		public string sandboxStampTemplateId;

		// Token: 0x040059A4 RID: 22948
		private WorldTrait _cachedStoryTrait;
	}
}
