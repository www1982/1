using System;

// Token: 0x02000A7B RID: 2683
public class Quest : Resource
{
	// Token: 0x06004DEC RID: 19948 RVA: 0x001C3E78 File Offset: 0x001C2078
	public Quest(string id, QuestCriteria[] criteria)
		: base(id, id)
	{
		Debug.Assert(criteria.Length != 0);
		this.Criteria = criteria;
		string text = "STRINGS.CODEX.QUESTS." + id.ToUpperInvariant();
		StringEntry stringEntry;
		if (Strings.TryGet(text + ".NAME", out stringEntry))
		{
			this.Title = stringEntry.String;
		}
		if (Strings.TryGet(text + ".COMPLETE", out stringEntry))
		{
			this.CompletionText = stringEntry.String;
		}
		for (int i = 0; i < this.Criteria.Length; i++)
		{
			this.Criteria[i].PopulateStrings("STRINGS.CODEX.QUESTS.");
		}
	}

	// Token: 0x040033D0 RID: 13264
	public const string STRINGS_PREFIX = "STRINGS.CODEX.QUESTS.";

	// Token: 0x040033D1 RID: 13265
	public readonly QuestCriteria[] Criteria;

	// Token: 0x040033D2 RID: 13266
	public readonly string Title;

	// Token: 0x040033D3 RID: 13267
	public readonly string CompletionText;

	// Token: 0x02001B64 RID: 7012
	public struct ItemData
	{
		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x0600A77D RID: 42877 RVA: 0x003B1174 File Offset: 0x003AF374
		// (set) Token: 0x0600A77E RID: 42878 RVA: 0x003B117E File Offset: 0x003AF37E
		public int ValueHandle
		{
			get
			{
				return this.valueHandle - 1;
			}
			set
			{
				this.valueHandle = value + 1;
			}
		}

		// Token: 0x040082C6 RID: 33478
		public int LocalCellId;

		// Token: 0x040082C7 RID: 33479
		public float CurrentValue;

		// Token: 0x040082C8 RID: 33480
		public Tag SatisfyingItem;

		// Token: 0x040082C9 RID: 33481
		public Tag QualifyingTag;

		// Token: 0x040082CA RID: 33482
		public HashedString CriteriaId;

		// Token: 0x040082CB RID: 33483
		private int valueHandle;
	}

	// Token: 0x02001B65 RID: 7013
	public enum State
	{
		// Token: 0x040082CD RID: 33485
		NotStarted,
		// Token: 0x040082CE RID: 33486
		InProgress,
		// Token: 0x040082CF RID: 33487
		Completed
	}
}
