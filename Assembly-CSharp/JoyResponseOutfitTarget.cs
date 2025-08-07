using System;
using UnityEngine;

// Token: 0x0200099B RID: 2459
public readonly struct JoyResponseOutfitTarget
{
	// Token: 0x06004750 RID: 18256 RVA: 0x0019B1D5 File Offset: 0x001993D5
	public JoyResponseOutfitTarget(JoyResponseOutfitTarget.Implementation impl)
	{
		this.impl = impl;
	}

	// Token: 0x06004751 RID: 18257 RVA: 0x0019B1DE File Offset: 0x001993DE
	public Option<string> ReadFacadeId()
	{
		return this.impl.ReadFacadeId();
	}

	// Token: 0x06004752 RID: 18258 RVA: 0x0019B1EB File Offset: 0x001993EB
	public void WriteFacadeId(Option<string> facadeId)
	{
		this.impl.WriteFacadeId(facadeId);
	}

	// Token: 0x06004753 RID: 18259 RVA: 0x0019B1F9 File Offset: 0x001993F9
	public string GetMinionName()
	{
		return this.impl.GetMinionName();
	}

	// Token: 0x06004754 RID: 18260 RVA: 0x0019B206 File Offset: 0x00199406
	public Personality GetPersonality()
	{
		return this.impl.GetPersonality();
	}

	// Token: 0x06004755 RID: 18261 RVA: 0x0019B213 File Offset: 0x00199413
	public static JoyResponseOutfitTarget FromMinion(GameObject minionInstance)
	{
		return new JoyResponseOutfitTarget(new JoyResponseOutfitTarget.MinionInstanceTarget(minionInstance));
	}

	// Token: 0x06004756 RID: 18262 RVA: 0x0019B225 File Offset: 0x00199425
	public static JoyResponseOutfitTarget FromPersonality(Personality personality)
	{
		return new JoyResponseOutfitTarget(new JoyResponseOutfitTarget.PersonalityTarget(personality));
	}

	// Token: 0x04002F25 RID: 12069
	private readonly JoyResponseOutfitTarget.Implementation impl;

	// Token: 0x0200199E RID: 6558
	public interface Implementation
	{
		// Token: 0x0600A001 RID: 40961
		Option<string> ReadFacadeId();

		// Token: 0x0600A002 RID: 40962
		void WriteFacadeId(Option<string> permitId);

		// Token: 0x0600A003 RID: 40963
		string GetMinionName();

		// Token: 0x0600A004 RID: 40964
		Personality GetPersonality();
	}

	// Token: 0x0200199F RID: 6559
	public readonly struct MinionInstanceTarget : JoyResponseOutfitTarget.Implementation
	{
		// Token: 0x0600A005 RID: 40965 RVA: 0x0039AA28 File Offset: 0x00398C28
		public MinionInstanceTarget(GameObject minionInstance)
		{
			this.minionInstance = minionInstance;
			this.wearableAccessorizer = minionInstance.GetComponent<WearableAccessorizer>();
		}

		// Token: 0x0600A006 RID: 40966 RVA: 0x0039AA3D File Offset: 0x00398C3D
		public string GetMinionName()
		{
			return this.minionInstance.GetProperName();
		}

		// Token: 0x0600A007 RID: 40967 RVA: 0x0039AA4A File Offset: 0x00398C4A
		public Personality GetPersonality()
		{
			return Db.Get().Personalities.Get(this.minionInstance.GetComponent<MinionIdentity>().personalityResourceId);
		}

		// Token: 0x0600A008 RID: 40968 RVA: 0x0039AA6B File Offset: 0x00398C6B
		public Option<string> ReadFacadeId()
		{
			return this.wearableAccessorizer.GetJoyResponseId();
		}

		// Token: 0x0600A009 RID: 40969 RVA: 0x0039AA78 File Offset: 0x00398C78
		public void WriteFacadeId(Option<string> permitId)
		{
			this.wearableAccessorizer.SetJoyResponseId(permitId);
		}

		// Token: 0x04007CFD RID: 31997
		public readonly GameObject minionInstance;

		// Token: 0x04007CFE RID: 31998
		public readonly WearableAccessorizer wearableAccessorizer;
	}

	// Token: 0x020019A0 RID: 6560
	public readonly struct PersonalityTarget : JoyResponseOutfitTarget.Implementation
	{
		// Token: 0x0600A00A RID: 40970 RVA: 0x0039AA86 File Offset: 0x00398C86
		public PersonalityTarget(Personality personality)
		{
			this.personality = personality;
		}

		// Token: 0x0600A00B RID: 40971 RVA: 0x0039AA8F File Offset: 0x00398C8F
		public string GetMinionName()
		{
			return this.personality.Name;
		}

		// Token: 0x0600A00C RID: 40972 RVA: 0x0039AA9C File Offset: 0x00398C9C
		public Personality GetPersonality()
		{
			return this.personality;
		}

		// Token: 0x0600A00D RID: 40973 RVA: 0x0039AAA4 File Offset: 0x00398CA4
		public Option<string> ReadFacadeId()
		{
			return this.personality.GetSelectedTemplateOutfitId(ClothingOutfitUtility.OutfitType.JoyResponse);
		}

		// Token: 0x0600A00E RID: 40974 RVA: 0x0039AAB7 File Offset: 0x00398CB7
		public void WriteFacadeId(Option<string> facadeId)
		{
			this.personality.SetSelectedTemplateOutfitId(ClothingOutfitUtility.OutfitType.JoyResponse, facadeId);
		}

		// Token: 0x04007CFF RID: 31999
		public readonly Personality personality;
	}
}
