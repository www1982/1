using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A7D RID: 2685
public class QuestCriteria
{
	// Token: 0x17000557 RID: 1367
	// (get) Token: 0x06004E0A RID: 19978 RVA: 0x001C4B30 File Offset: 0x001C2D30
	// (set) Token: 0x06004E0B RID: 19979 RVA: 0x001C4B38 File Offset: 0x001C2D38
	public string Text { get; private set; }

	// Token: 0x17000558 RID: 1368
	// (get) Token: 0x06004E0C RID: 19980 RVA: 0x001C4B41 File Offset: 0x001C2D41
	// (set) Token: 0x06004E0D RID: 19981 RVA: 0x001C4B49 File Offset: 0x001C2D49
	public string Tooltip { get; private set; }

	// Token: 0x06004E0E RID: 19982 RVA: 0x001C4B54 File Offset: 0x001C2D54
	public QuestCriteria(Tag id, float[] targetValues = null, int requiredCount = 1, HashSet<Tag> acceptedTags = null, QuestCriteria.BehaviorFlags flags = QuestCriteria.BehaviorFlags.None)
	{
		global::Debug.Assert(targetValues == null || (targetValues.Length != 0 && targetValues.Length <= 32));
		this.CriteriaId = id;
		this.EvaluationBehaviors = flags;
		this.TargetValues = targetValues;
		this.AcceptedTags = acceptedTags;
		this.RequiredCount = requiredCount;
	}

	// Token: 0x06004E0F RID: 19983 RVA: 0x001C4BB0 File Offset: 0x001C2DB0
	public bool ValueSatisfies(float value, int valueHandle)
	{
		if (float.IsNaN(value))
		{
			return false;
		}
		float num = ((this.TargetValues == null) ? 0f : this.TargetValues[valueHandle]);
		return this.ValueSatisfies_Internal(value, num);
	}

	// Token: 0x06004E10 RID: 19984 RVA: 0x001C4BE7 File Offset: 0x001C2DE7
	protected virtual bool ValueSatisfies_Internal(float current, float target)
	{
		return true;
	}

	// Token: 0x06004E11 RID: 19985 RVA: 0x001C4BEA File Offset: 0x001C2DEA
	public bool IsSatisfied(uint satisfactionState, uint satisfactionMask)
	{
		return (satisfactionState & satisfactionMask) == satisfactionMask;
	}

	// Token: 0x06004E12 RID: 19986 RVA: 0x001C4BF4 File Offset: 0x001C2DF4
	public void PopulateStrings(string prefix)
	{
		string text = this.CriteriaId.Name.ToUpperInvariant();
		StringEntry stringEntry;
		if (Strings.TryGet(prefix + "CRITERIA." + text + ".NAME", out stringEntry))
		{
			this.Text = stringEntry.String;
		}
		if (Strings.TryGet(prefix + "CRITERIA." + text + ".TOOLTIP", out stringEntry))
		{
			this.Tooltip = stringEntry.String;
		}
	}

	// Token: 0x06004E13 RID: 19987 RVA: 0x001C4C61 File Offset: 0x001C2E61
	public uint GetSatisfactionMask()
	{
		if (this.TargetValues == null)
		{
			return 1U;
		}
		return (uint)Mathf.Pow(2f, (float)(this.TargetValues.Length - 1));
	}

	// Token: 0x06004E14 RID: 19988 RVA: 0x001C4C83 File Offset: 0x001C2E83
	public uint GetValueMask(int valueHandle)
	{
		if (this.TargetValues == null)
		{
			return 1U;
		}
		if (!QuestCriteria.HasBehavior(this.EvaluationBehaviors, QuestCriteria.BehaviorFlags.TrackArea))
		{
			valueHandle %= this.TargetValues.Length;
		}
		return 1U << valueHandle;
	}

	// Token: 0x06004E15 RID: 19989 RVA: 0x001C4CAF File Offset: 0x001C2EAF
	public static bool HasBehavior(QuestCriteria.BehaviorFlags flags, QuestCriteria.BehaviorFlags behavior)
	{
		return (flags & behavior) == behavior;
	}

	// Token: 0x040033D9 RID: 13273
	public const int MAX_VALUES = 32;

	// Token: 0x040033DA RID: 13274
	public const int INVALID_VALUE = -1;

	// Token: 0x040033DB RID: 13275
	public readonly Tag CriteriaId;

	// Token: 0x040033DC RID: 13276
	public readonly QuestCriteria.BehaviorFlags EvaluationBehaviors;

	// Token: 0x040033DD RID: 13277
	public readonly float[] TargetValues;

	// Token: 0x040033DE RID: 13278
	public readonly int RequiredCount = 1;

	// Token: 0x040033DF RID: 13279
	public readonly HashSet<Tag> AcceptedTags;

	// Token: 0x02001B69 RID: 7017
	public enum BehaviorFlags
	{
		// Token: 0x040082DA RID: 33498
		None,
		// Token: 0x040082DB RID: 33499
		TrackArea,
		// Token: 0x040082DC RID: 33500
		AllowsRegression,
		// Token: 0x040082DD RID: 33501
		TrackValues = 4,
		// Token: 0x040082DE RID: 33502
		TrackItems = 8,
		// Token: 0x040082DF RID: 33503
		UniqueItems = 24
	}
}
