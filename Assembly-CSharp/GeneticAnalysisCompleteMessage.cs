using System;
using KSerialization;
using STRINGS;

// Token: 0x02000D51 RID: 3409
public class GeneticAnalysisCompleteMessage : Message
{
	// Token: 0x060069B6 RID: 27062 RVA: 0x0027FC57 File Offset: 0x0027DE57
	public GeneticAnalysisCompleteMessage()
	{
	}

	// Token: 0x060069B7 RID: 27063 RVA: 0x0027FC5F File Offset: 0x0027DE5F
	public GeneticAnalysisCompleteMessage(Tag subSpeciesID)
	{
		this.subSpeciesID = subSpeciesID;
	}

	// Token: 0x060069B8 RID: 27064 RVA: 0x0027FC6E File Offset: 0x0027DE6E
	public override string GetSound()
	{
		return "";
	}

	// Token: 0x060069B9 RID: 27065 RVA: 0x0027FC78 File Offset: 0x0027DE78
	public override string GetMessageBody()
	{
		PlantSubSpeciesCatalog.SubSpeciesInfo subSpeciesInfo = PlantSubSpeciesCatalog.Instance.FindSubSpecies(this.subSpeciesID);
		return MISC.NOTIFICATIONS.GENETICANALYSISCOMPLETE.MESSAGEBODY.Replace("{Plant}", subSpeciesInfo.speciesID.ProperName()).Replace("{Subspecies}", subSpeciesInfo.GetNameWithMutations(subSpeciesInfo.speciesID.ProperName(), true, false)).Replace("{Info}", subSpeciesInfo.GetMutationsTooltip());
	}

	// Token: 0x060069BA RID: 27066 RVA: 0x0027FCDF File Offset: 0x0027DEDF
	public override string GetTitle()
	{
		return MISC.NOTIFICATIONS.GENETICANALYSISCOMPLETE.NAME;
	}

	// Token: 0x060069BB RID: 27067 RVA: 0x0027FCEC File Offset: 0x0027DEEC
	public override string GetTooltip()
	{
		PlantSubSpeciesCatalog.SubSpeciesInfo subSpeciesInfo = PlantSubSpeciesCatalog.Instance.FindSubSpecies(this.subSpeciesID);
		return MISC.NOTIFICATIONS.GENETICANALYSISCOMPLETE.TOOLTIP.Replace("{Plant}", subSpeciesInfo.speciesID.ProperName());
	}

	// Token: 0x060069BC RID: 27068 RVA: 0x0027FD24 File Offset: 0x0027DF24
	public override bool IsValid()
	{
		return this.subSpeciesID.IsValid;
	}

	// Token: 0x04004842 RID: 18498
	[Serialize]
	public Tag subSpeciesID;
}
