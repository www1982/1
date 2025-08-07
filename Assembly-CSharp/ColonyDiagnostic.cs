using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x020008A9 RID: 2217
public abstract class ColonyDiagnostic : ISim4000ms, IHasDlcRestrictions
{
	// Token: 0x06003D95 RID: 15765 RVA: 0x00157EB2 File Offset: 0x001560B2
	public GameObject GetNextClickThroughObject()
	{
		if (this.aggregatedUniqueClickThroughObjects.Count == 0)
		{
			return null;
		}
		this.clickThroughIndex = (this.clickThroughIndex + 1) % this.aggregatedUniqueClickThroughObjects.Count;
		return this.aggregatedUniqueClickThroughObjects[this.clickThroughIndex];
	}

	// Token: 0x06003D96 RID: 15766 RVA: 0x00157EF0 File Offset: 0x001560F0
	public ColonyDiagnostic(int worldID, string name)
	{
		this.worldID = worldID;
		this.name = name;
		this.id = base.GetType().Name;
		this.IsWorldModuleInterior = ClusterManager.Instance.GetWorld(worldID).IsModuleInterior;
		this.colors = new Dictionary<ColonyDiagnostic.DiagnosticResult.Opinion, Color>();
		this.colors.Add(ColonyDiagnostic.DiagnosticResult.Opinion.DuplicantThreatening, Constants.NEGATIVE_COLOR);
		this.colors.Add(ColonyDiagnostic.DiagnosticResult.Opinion.Bad, Constants.NEGATIVE_COLOR);
		this.colors.Add(ColonyDiagnostic.DiagnosticResult.Opinion.Warning, Constants.NEGATIVE_COLOR);
		this.colors.Add(ColonyDiagnostic.DiagnosticResult.Opinion.Concern, Constants.WARNING_COLOR);
		this.colors.Add(ColonyDiagnostic.DiagnosticResult.Opinion.Normal, Constants.NEUTRAL_COLOR);
		this.colors.Add(ColonyDiagnostic.DiagnosticResult.Opinion.Suggestion, Constants.NEUTRAL_COLOR);
		this.colors.Add(ColonyDiagnostic.DiagnosticResult.Opinion.Tutorial, Constants.NEUTRAL_COLOR);
		this.colors.Add(ColonyDiagnostic.DiagnosticResult.Opinion.Good, Constants.POSITIVE_COLOR);
		SimAndRenderScheduler.instance.Add(this, true);
	}

	// Token: 0x1700044A RID: 1098
	// (get) Token: 0x06003D97 RID: 15767 RVA: 0x00158025 File Offset: 0x00156225
	// (set) Token: 0x06003D98 RID: 15768 RVA: 0x0015802D File Offset: 0x0015622D
	public int worldID { get; protected set; }

	// Token: 0x1700044B RID: 1099
	// (get) Token: 0x06003D99 RID: 15769 RVA: 0x00158036 File Offset: 0x00156236
	// (set) Token: 0x06003D9A RID: 15770 RVA: 0x0015803E File Offset: 0x0015623E
	public bool IsWorldModuleInterior { get; private set; }

	// Token: 0x06003D9B RID: 15771 RVA: 0x00158047 File Offset: 0x00156247
	public void OnCleanUp()
	{
		SimAndRenderScheduler.instance.Remove(this);
	}

	// Token: 0x06003D9C RID: 15772 RVA: 0x00158054 File Offset: 0x00156254
	public void Sim4000ms(float dt)
	{
		this.SetResult(ColonyDiagnosticUtility.IgnoreFirstUpdate ? ColonyDiagnosticUtility.NoDataResult : this.Evaluate());
	}

	// Token: 0x06003D9D RID: 15773 RVA: 0x00158070 File Offset: 0x00156270
	public DiagnosticCriterion[] GetCriteria()
	{
		DiagnosticCriterion[] array = new DiagnosticCriterion[this.criteria.Values.Count];
		this.criteria.Values.CopyTo(array, 0);
		return array;
	}

	// Token: 0x1700044C RID: 1100
	// (get) Token: 0x06003D9E RID: 15774 RVA: 0x001580A6 File Offset: 0x001562A6
	// (set) Token: 0x06003D9F RID: 15775 RVA: 0x001580AE File Offset: 0x001562AE
	public ColonyDiagnostic.DiagnosticResult LatestResult
	{
		get
		{
			return this.latestResult;
		}
		private set
		{
			this.latestResult = value;
		}
	}

	// Token: 0x06003DA0 RID: 15776 RVA: 0x001580B7 File Offset: 0x001562B7
	public virtual string GetAverageValueString()
	{
		if (this.tracker != null)
		{
			return this.tracker.FormatValueString(Mathf.Round(this.tracker.GetAverageValue(this.trackerSampleCountSeconds)));
		}
		return "";
	}

	// Token: 0x06003DA1 RID: 15777 RVA: 0x001580E8 File Offset: 0x001562E8
	public virtual string GetCurrentValueString()
	{
		return "";
	}

	// Token: 0x06003DA2 RID: 15778 RVA: 0x001580EF File Offset: 0x001562EF
	protected void AddCriterion(string id, DiagnosticCriterion criterion)
	{
		if (!this.criteria.ContainsKey(id))
		{
			criterion.SetID(id);
			this.criteria.Add(id, criterion);
		}
	}

	// Token: 0x06003DA3 RID: 15779 RVA: 0x00158114 File Offset: 0x00156314
	public virtual ColonyDiagnostic.DiagnosticResult Evaluate()
	{
		ColonyDiagnostic.DiagnosticResult diagnosticResult = new ColonyDiagnostic.DiagnosticResult(ColonyDiagnostic.DiagnosticResult.Opinion.Normal, "", null);
		bool flag = false;
		if (!ClusterManager.Instance.GetWorld(this.worldID).IsDiscovered)
		{
			return diagnosticResult;
		}
		this.aggregatedUniqueClickThroughObjects.Clear();
		foreach (KeyValuePair<string, DiagnosticCriterion> keyValuePair in this.criteria)
		{
			if (ColonyDiagnosticUtility.Instance.IsCriteriaEnabled(this.worldID, this.id, keyValuePair.Key))
			{
				ColonyDiagnostic.DiagnosticResult diagnosticResult2 = keyValuePair.Value.Evaluate();
				if (diagnosticResult2.opinion < diagnosticResult.opinion || (!flag && diagnosticResult2.opinion == ColonyDiagnostic.DiagnosticResult.Opinion.Normal))
				{
					flag = true;
					diagnosticResult.opinion = diagnosticResult2.opinion;
					diagnosticResult.Message = diagnosticResult2.Message;
					diagnosticResult.clickThroughTarget = diagnosticResult2.clickThroughTarget;
					if (diagnosticResult2.clickThroughObjects != null)
					{
						foreach (GameObject gameObject in diagnosticResult2.clickThroughObjects)
						{
							if (!this.aggregatedUniqueClickThroughObjects.Contains(gameObject))
							{
								this.aggregatedUniqueClickThroughObjects.Add(gameObject);
							}
						}
					}
				}
			}
		}
		return diagnosticResult;
	}

	// Token: 0x06003DA4 RID: 15780 RVA: 0x0015827C File Offset: 0x0015647C
	public void SetResult(ColonyDiagnostic.DiagnosticResult result)
	{
		this.LatestResult = result;
	}

	// Token: 0x1700044D RID: 1101
	// (get) Token: 0x06003DA5 RID: 15781 RVA: 0x00158285 File Offset: 0x00156485
	protected string NO_MINIONS
	{
		get
		{
			return this.IsWorldModuleInterior ? UI.COLONY_DIAGNOSTICS.NO_MINIONS_ROCKET : UI.COLONY_DIAGNOSTICS.NO_MINIONS_PLANETOID;
		}
	}

	// Token: 0x06003DA6 RID: 15782 RVA: 0x001582A0 File Offset: 0x001564A0
	public virtual string[] GetRequiredDlcIds()
	{
		return null;
	}

	// Token: 0x06003DA7 RID: 15783 RVA: 0x001582A3 File Offset: 0x001564A3
	public virtual string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x04002607 RID: 9735
	private int clickThroughIndex;

	// Token: 0x04002608 RID: 9736
	private List<GameObject> aggregatedUniqueClickThroughObjects = new List<GameObject>();

	// Token: 0x0400260A RID: 9738
	public string name;

	// Token: 0x0400260B RID: 9739
	public string id;

	// Token: 0x0400260D RID: 9741
	public string icon = "icon_errand_operate";

	// Token: 0x0400260E RID: 9742
	private Dictionary<string, DiagnosticCriterion> criteria = new Dictionary<string, DiagnosticCriterion>();

	// Token: 0x0400260F RID: 9743
	public ColonyDiagnostic.PresentationSetting presentationSetting;

	// Token: 0x04002610 RID: 9744
	private ColonyDiagnostic.DiagnosticResult latestResult = new ColonyDiagnostic.DiagnosticResult(ColonyDiagnostic.DiagnosticResult.Opinion.Normal, UI.COLONY_DIAGNOSTICS.NO_DATA, null);

	// Token: 0x04002611 RID: 9745
	public Dictionary<ColonyDiagnostic.DiagnosticResult.Opinion, Color> colors = new Dictionary<ColonyDiagnostic.DiagnosticResult.Opinion, Color>();

	// Token: 0x04002612 RID: 9746
	public Tracker tracker;

	// Token: 0x04002613 RID: 9747
	protected float trackerSampleCountSeconds = 4f;

	// Token: 0x02001868 RID: 6248
	public enum PresentationSetting
	{
		// Token: 0x040078DF RID: 30943
		AverageValue,
		// Token: 0x040078E0 RID: 30944
		CurrentValue
	}

	// Token: 0x02001869 RID: 6249
	public struct DiagnosticResult
	{
		// Token: 0x06009C7D RID: 40061 RVA: 0x00390D14 File Offset: 0x0038EF14
		public DiagnosticResult(ColonyDiagnostic.DiagnosticResult.Opinion opinion, string message, global::Tuple<Vector3, GameObject> clickThroughTarget = null)
		{
			this.message = message;
			this.opinion = opinion;
			this.clickThroughTarget = null;
			this.clickThroughObjects = null;
		}

		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x06009C7F RID: 40063 RVA: 0x00390D3B File Offset: 0x0038EF3B
		// (set) Token: 0x06009C7E RID: 40062 RVA: 0x00390D32 File Offset: 0x0038EF32
		public string Message
		{
			get
			{
				return this.message;
			}
			set
			{
				this.message = value;
			}
		}

		// Token: 0x06009C80 RID: 40064 RVA: 0x00390D44 File Offset: 0x0038EF44
		public string GetFormattedMessage()
		{
			switch (this.opinion)
			{
			case ColonyDiagnostic.DiagnosticResult.Opinion.Bad:
				return string.Concat(new string[]
				{
					"<color=",
					Constants.NEGATIVE_COLOR_STR,
					">",
					this.message,
					"</color>"
				});
			case ColonyDiagnostic.DiagnosticResult.Opinion.Warning:
				return string.Concat(new string[]
				{
					"<color=",
					Constants.NEGATIVE_COLOR_STR,
					">",
					this.message,
					"</color>"
				});
			case ColonyDiagnostic.DiagnosticResult.Opinion.Concern:
				return string.Concat(new string[]
				{
					"<color=",
					Constants.WARNING_COLOR_STR,
					">",
					this.message,
					"</color>"
				});
			case ColonyDiagnostic.DiagnosticResult.Opinion.Suggestion:
			case ColonyDiagnostic.DiagnosticResult.Opinion.Normal:
				return string.Concat(new string[]
				{
					"<color=",
					Constants.WHITE_COLOR_STR,
					">",
					this.message,
					"</color>"
				});
			case ColonyDiagnostic.DiagnosticResult.Opinion.Good:
				return string.Concat(new string[]
				{
					"<color=",
					Constants.POSITIVE_COLOR_STR,
					">",
					this.message,
					"</color>"
				});
			}
			return this.message;
		}

		// Token: 0x040078E1 RID: 30945
		public ColonyDiagnostic.DiagnosticResult.Opinion opinion;

		// Token: 0x040078E2 RID: 30946
		public global::Tuple<Vector3, GameObject> clickThroughTarget;

		// Token: 0x040078E3 RID: 30947
		public List<GameObject> clickThroughObjects;

		// Token: 0x040078E4 RID: 30948
		private string message;

		// Token: 0x02002828 RID: 10280
		public enum Opinion
		{
			// Token: 0x0400B216 RID: 45590
			Unset,
			// Token: 0x0400B217 RID: 45591
			DuplicantThreatening,
			// Token: 0x0400B218 RID: 45592
			Bad,
			// Token: 0x0400B219 RID: 45593
			Warning,
			// Token: 0x0400B21A RID: 45594
			Concern,
			// Token: 0x0400B21B RID: 45595
			Suggestion,
			// Token: 0x0400B21C RID: 45596
			Tutorial,
			// Token: 0x0400B21D RID: 45597
			Normal,
			// Token: 0x0400B21E RID: 45598
			Good
		}
	}
}
