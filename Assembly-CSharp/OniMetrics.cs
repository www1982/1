using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000BEF RID: 3055
public class OniMetrics : MonoBehaviour
{
	// Token: 0x06005C0D RID: 23565 RVA: 0x00217C48 File Offset: 0x00215E48
	private static void EnsureMetrics()
	{
		if (OniMetrics.Metrics != null)
		{
			return;
		}
		OniMetrics.Metrics = new List<Dictionary<string, object>>(2);
		for (int i = 0; i < 2; i++)
		{
			OniMetrics.Metrics.Add(null);
		}
	}

	// Token: 0x06005C0E RID: 23566 RVA: 0x00217C7F File Offset: 0x00215E7F
	public static void LogEvent(OniMetrics.Event eventType, string key, object data)
	{
		OniMetrics.EnsureMetrics();
		if (OniMetrics.Metrics[(int)eventType] == null)
		{
			OniMetrics.Metrics[(int)eventType] = new Dictionary<string, object>();
		}
		OniMetrics.Metrics[(int)eventType][key] = data;
	}

	// Token: 0x06005C0F RID: 23567 RVA: 0x00217CB8 File Offset: 0x00215EB8
	public static void SendEvent(OniMetrics.Event eventType, string debugName)
	{
		if (OniMetrics.Metrics[(int)eventType] == null || OniMetrics.Metrics[(int)eventType].Count == 0)
		{
			return;
		}
		ThreadedHttps<KleiMetrics>.Instance.SendEvent(OniMetrics.Metrics[(int)eventType], debugName);
		OniMetrics.Metrics[(int)eventType].Clear();
	}

	// Token: 0x04003CF6 RID: 15606
	private static List<Dictionary<string, object>> Metrics;

	// Token: 0x02001D29 RID: 7465
	public enum Event : short
	{
		// Token: 0x0400885D RID: 34909
		NewSave,
		// Token: 0x0400885E RID: 34910
		EndOfCycle,
		// Token: 0x0400885F RID: 34911
		NumEvents
	}
}
