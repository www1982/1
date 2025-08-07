using System;
using UnityEngine;

// Token: 0x0200082E RID: 2094
[AddComponentMenu("KMonoBehaviour/scripts/FactionManager")]
public class FactionManager : KMonoBehaviour
{
	// Token: 0x0600395B RID: 14683 RVA: 0x0013DCC0 File Offset: 0x0013BEC0
	public static void DestroyInstance()
	{
		FactionManager.Instance = null;
	}

	// Token: 0x0600395C RID: 14684 RVA: 0x0013DCC8 File Offset: 0x0013BEC8
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		FactionManager.Instance = this;
	}

	// Token: 0x0600395D RID: 14685 RVA: 0x0013DCD6 File Offset: 0x0013BED6
	protected override void OnSpawn()
	{
		base.OnSpawn();
	}

	// Token: 0x0600395E RID: 14686 RVA: 0x0013DCE0 File Offset: 0x0013BEE0
	public Faction GetFaction(FactionManager.FactionID faction)
	{
		switch (faction)
		{
		case FactionManager.FactionID.Duplicant:
			return this.Duplicant;
		case FactionManager.FactionID.Friendly:
			return this.Friendly;
		case FactionManager.FactionID.Hostile:
			return this.Hostile;
		case FactionManager.FactionID.Prey:
			return this.Prey;
		case FactionManager.FactionID.Predator:
			return this.Predator;
		case FactionManager.FactionID.Pest:
			return this.Pest;
		default:
			return null;
		}
	}

	// Token: 0x0600395F RID: 14687 RVA: 0x0013DD38 File Offset: 0x0013BF38
	public FactionManager.Disposition GetDisposition(FactionManager.FactionID of_faction, FactionManager.FactionID to_faction)
	{
		if (FactionManager.Instance.GetFaction(of_faction).Dispositions.ContainsKey(to_faction))
		{
			return FactionManager.Instance.GetFaction(of_faction).Dispositions[to_faction];
		}
		return FactionManager.Disposition.Neutral;
	}

	// Token: 0x040022AB RID: 8875
	public static FactionManager Instance;

	// Token: 0x040022AC RID: 8876
	public Faction Duplicant = new Faction(FactionManager.FactionID.Duplicant);

	// Token: 0x040022AD RID: 8877
	public Faction Friendly = new Faction(FactionManager.FactionID.Friendly);

	// Token: 0x040022AE RID: 8878
	public Faction Hostile = new Faction(FactionManager.FactionID.Hostile);

	// Token: 0x040022AF RID: 8879
	public Faction Predator = new Faction(FactionManager.FactionID.Predator);

	// Token: 0x040022B0 RID: 8880
	public Faction Prey = new Faction(FactionManager.FactionID.Prey);

	// Token: 0x040022B1 RID: 8881
	public Faction Pest = new Faction(FactionManager.FactionID.Pest);

	// Token: 0x020017A2 RID: 6050
	public enum FactionID
	{
		// Token: 0x0400765F RID: 30303
		Duplicant,
		// Token: 0x04007660 RID: 30304
		Friendly,
		// Token: 0x04007661 RID: 30305
		Hostile,
		// Token: 0x04007662 RID: 30306
		Prey,
		// Token: 0x04007663 RID: 30307
		Predator,
		// Token: 0x04007664 RID: 30308
		Pest,
		// Token: 0x04007665 RID: 30309
		NumberOfFactions
	}

	// Token: 0x020017A3 RID: 6051
	public enum Disposition
	{
		// Token: 0x04007667 RID: 30311
		Assist,
		// Token: 0x04007668 RID: 30312
		Neutral,
		// Token: 0x04007669 RID: 30313
		Attack
	}
}
