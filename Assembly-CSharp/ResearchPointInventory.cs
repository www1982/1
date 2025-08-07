using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

// Token: 0x02000AC1 RID: 2753
public class ResearchPointInventory
{
	// Token: 0x06005000 RID: 20480 RVA: 0x001CF6E0 File Offset: 0x001CD8E0
	public ResearchPointInventory()
	{
		foreach (ResearchType researchType in Research.Instance.researchTypes.Types)
		{
			this.PointsByTypeID.Add(researchType.id, 0f);
		}
	}

	// Token: 0x06005001 RID: 20481 RVA: 0x001CF75C File Offset: 0x001CD95C
	public void AddResearchPoints(string researchTypeID, float points)
	{
		if (!this.PointsByTypeID.ContainsKey(researchTypeID))
		{
			Debug.LogWarning("Research inventory is missing research point key " + researchTypeID);
			return;
		}
		Dictionary<string, float> pointsByTypeID = this.PointsByTypeID;
		pointsByTypeID[researchTypeID] += points;
	}

	// Token: 0x06005002 RID: 20482 RVA: 0x001CF7A1 File Offset: 0x001CD9A1
	public void RemoveResearchPoints(string researchTypeID, float points)
	{
		this.AddResearchPoints(researchTypeID, -points);
	}

	// Token: 0x06005003 RID: 20483 RVA: 0x001CF7AC File Offset: 0x001CD9AC
	[OnDeserialized]
	private void OnDeserialized()
	{
		foreach (ResearchType researchType in Research.Instance.researchTypes.Types)
		{
			if (!this.PointsByTypeID.ContainsKey(researchType.id))
			{
				this.PointsByTypeID.Add(researchType.id, 0f);
			}
		}
	}

	// Token: 0x040035E2 RID: 13794
	public Dictionary<string, float> PointsByTypeID = new Dictionary<string, float>();
}
