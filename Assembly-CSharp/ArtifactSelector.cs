using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x020006A7 RID: 1703
public class ArtifactSelector : KMonoBehaviour
{
	// Token: 0x170001F4 RID: 500
	// (get) Token: 0x0600297E RID: 10622 RVA: 0x000F1323 File Offset: 0x000EF523
	public int AnalyzedArtifactCount
	{
		get
		{
			return this.analyzedArtifactCount;
		}
	}

	// Token: 0x170001F5 RID: 501
	// (get) Token: 0x0600297F RID: 10623 RVA: 0x000F132B File Offset: 0x000EF52B
	public int AnalyzedSpaceArtifactCount
	{
		get
		{
			return this.analyzedSpaceArtifactCount;
		}
	}

	// Token: 0x06002980 RID: 10624 RVA: 0x000F1333 File Offset: 0x000EF533
	public List<string> GetAnalyzedArtifactIDs()
	{
		return this.analyzedArtifatIDs;
	}

	// Token: 0x06002981 RID: 10625 RVA: 0x000F133C File Offset: 0x000EF53C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		ArtifactSelector.Instance = this;
		this.placedArtifacts.Add(ArtifactType.Terrestrial, new List<string>());
		this.placedArtifacts.Add(ArtifactType.Space, new List<string>());
		this.placedArtifacts.Add(ArtifactType.Any, new List<string>());
	}

	// Token: 0x06002982 RID: 10626 RVA: 0x000F1388 File Offset: 0x000EF588
	protected override void OnSpawn()
	{
		base.OnSpawn();
		int num = 0;
		int num2 = 0;
		foreach (string text in this.analyzedArtifatIDs)
		{
			ArtifactType artifactType = this.GetArtifactType(text);
			if (artifactType != ArtifactType.Space)
			{
				if (artifactType == ArtifactType.Terrestrial)
				{
					num++;
				}
			}
			else
			{
				num2++;
			}
		}
		if (num > this.analyzedArtifactCount)
		{
			this.analyzedArtifactCount = num;
		}
		if (num2 > this.analyzedSpaceArtifactCount)
		{
			this.analyzedSpaceArtifactCount = num2;
		}
	}

	// Token: 0x06002983 RID: 10627 RVA: 0x000F141C File Offset: 0x000EF61C
	public bool RecordArtifactAnalyzed(string id)
	{
		if (this.analyzedArtifatIDs.Contains(id))
		{
			return false;
		}
		this.analyzedArtifatIDs.Add(id);
		return true;
	}

	// Token: 0x06002984 RID: 10628 RVA: 0x000F143B File Offset: 0x000EF63B
	public void IncrementAnalyzedTerrestrialArtifacts()
	{
		this.analyzedArtifactCount++;
	}

	// Token: 0x06002985 RID: 10629 RVA: 0x000F144B File Offset: 0x000EF64B
	public void IncrementAnalyzedSpaceArtifacts()
	{
		this.analyzedSpaceArtifactCount++;
	}

	// Token: 0x06002986 RID: 10630 RVA: 0x000F145C File Offset: 0x000EF65C
	public string GetUniqueArtifactID(ArtifactType artifactType = ArtifactType.Any)
	{
		List<string> list = new List<string>();
		foreach (string text in ArtifactConfig.artifactItems[artifactType])
		{
			if (!this.placedArtifacts[artifactType].Contains(text) && Game.IsCorrectDlcActiveForCurrentSave(Assets.GetPrefab(text.ToTag()).GetComponent<KPrefabID>()))
			{
				list.Add(text);
			}
		}
		string text2 = "artifact_officemug";
		if (list.Count == 0 && artifactType != ArtifactType.Any)
		{
			foreach (string text3 in ArtifactConfig.artifactItems[ArtifactType.Any])
			{
				if (!this.placedArtifacts[ArtifactType.Any].Contains(text3) && Game.IsCorrectDlcActiveForCurrentSave(Assets.GetPrefab(text3.ToTag()).GetComponent<KPrefabID>()))
				{
					list.Add(text3);
					artifactType = ArtifactType.Any;
				}
			}
		}
		if (list.Count != 0)
		{
			text2 = list[global::UnityEngine.Random.Range(0, list.Count)];
		}
		this.placedArtifacts[artifactType].Add(text2);
		return text2;
	}

	// Token: 0x06002987 RID: 10631 RVA: 0x000F15A0 File Offset: 0x000EF7A0
	public void ReserveArtifactID(string artifactID, ArtifactType artifactType = ArtifactType.Any)
	{
		if (this.placedArtifacts[artifactType].Contains(artifactID))
		{
			DebugUtil.Assert(true, string.Format("Tried to add {0} to placedArtifacts but it already exists in the list!", artifactID));
		}
		this.placedArtifacts[artifactType].Add(artifactID);
	}

	// Token: 0x06002988 RID: 10632 RVA: 0x000F15D9 File Offset: 0x000EF7D9
	public ArtifactType GetArtifactType(string artifactID)
	{
		if (this.placedArtifacts[ArtifactType.Terrestrial].Contains(artifactID))
		{
			return ArtifactType.Terrestrial;
		}
		if (this.placedArtifacts[ArtifactType.Space].Contains(artifactID))
		{
			return ArtifactType.Space;
		}
		return ArtifactType.Any;
	}

	// Token: 0x04001897 RID: 6295
	public static ArtifactSelector Instance;

	// Token: 0x04001898 RID: 6296
	[Serialize]
	private Dictionary<ArtifactType, List<string>> placedArtifacts = new Dictionary<ArtifactType, List<string>>();

	// Token: 0x04001899 RID: 6297
	[Serialize]
	private int analyzedArtifactCount;

	// Token: 0x0400189A RID: 6298
	[Serialize]
	private int analyzedSpaceArtifactCount;

	// Token: 0x0400189B RID: 6299
	[Serialize]
	private List<string> analyzedArtifatIDs = new List<string>();

	// Token: 0x0400189C RID: 6300
	private const string DEFAULT_ARTIFACT_ID = "artifact_officemug";
}
