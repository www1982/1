using System;
using System.Collections.Generic;
using KSerialization;
using ProcGen;
using UnityEngine;

// Token: 0x0200093B RID: 2363
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/GermExposureTracker")]
public class GermExposureTracker : KMonoBehaviour
{
	// Token: 0x06004341 RID: 17217 RVA: 0x0018206C File Offset: 0x0018026C
	protected override void OnPrefabInit()
	{
		global::Debug.Assert(GermExposureTracker.Instance == null);
		GermExposureTracker.Instance = this;
	}

	// Token: 0x06004342 RID: 17218 RVA: 0x00182084 File Offset: 0x00180284
	protected override void OnSpawn()
	{
		this.rng = new SeededRandom(GameClock.Instance.GetCycle());
	}

	// Token: 0x06004343 RID: 17219 RVA: 0x0018209B File Offset: 0x0018029B
	protected override void OnForcedCleanUp()
	{
		GermExposureTracker.Instance = null;
	}

	// Token: 0x06004344 RID: 17220 RVA: 0x001820A4 File Offset: 0x001802A4
	public void AddExposure(ExposureType exposure_type, float amount)
	{
		float num;
		this.accumulation.TryGetValue(exposure_type.germ_id, out num);
		float num2 = num + amount;
		if (num2 > 1f)
		{
			using (List<MinionIdentity>.Enumerator enumerator = Components.LiveMinionIdentities.Items.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					MinionIdentity minionIdentity = enumerator.Current;
					GermExposureMonitor.Instance smi = minionIdentity.GetSMI<GermExposureMonitor.Instance>();
					if (smi.GetExposureState(exposure_type.germ_id) == GermExposureMonitor.ExposureState.Exposed)
					{
						float exposureWeight = minionIdentity.GetSMI<GermExposureMonitor.Instance>().GetExposureWeight(exposure_type.germ_id);
						if (exposureWeight > 0f)
						{
							this.exposure_candidates.Add(new GermExposureTracker.WeightedExposure
							{
								weight = exposureWeight,
								monitor = smi
							});
						}
					}
				}
				goto IL_00F8;
			}
			IL_00AF:
			num2 -= 1f;
			if (this.exposure_candidates.Count > 0)
			{
				GermExposureTracker.WeightedExposure weightedExposure = WeightedRandom.Choose<GermExposureTracker.WeightedExposure>(this.exposure_candidates, this.rng);
				this.exposure_candidates.Remove(weightedExposure);
				weightedExposure.monitor.ContractGerms(exposure_type.germ_id);
			}
			IL_00F8:
			if (num2 > 1f)
			{
				goto IL_00AF;
			}
		}
		this.accumulation[exposure_type.germ_id] = num2;
		this.exposure_candidates.Clear();
	}

	// Token: 0x04002CD6 RID: 11478
	public static GermExposureTracker Instance;

	// Token: 0x04002CD7 RID: 11479
	[Serialize]
	private Dictionary<HashedString, float> accumulation = new Dictionary<HashedString, float>();

	// Token: 0x04002CD8 RID: 11480
	private SeededRandom rng;

	// Token: 0x04002CD9 RID: 11481
	private List<GermExposureTracker.WeightedExposure> exposure_candidates = new List<GermExposureTracker.WeightedExposure>();

	// Token: 0x02001921 RID: 6433
	private class WeightedExposure : IWeighted
	{
		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x06009E73 RID: 40563 RVA: 0x00396E64 File Offset: 0x00395064
		// (set) Token: 0x06009E74 RID: 40564 RVA: 0x00396E6C File Offset: 0x0039506C
		public float weight { get; set; }

		// Token: 0x04007B67 RID: 31591
		public GermExposureMonitor.Instance monitor;
	}
}
