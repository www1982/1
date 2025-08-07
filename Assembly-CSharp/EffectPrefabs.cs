using System;
using UnityEngine;

// Token: 0x020008D1 RID: 2257
public class EffectPrefabs : MonoBehaviour
{
	// Token: 0x17000454 RID: 1108
	// (get) Token: 0x06003EA4 RID: 16036 RVA: 0x00161401 File Offset: 0x0015F601
	// (set) Token: 0x06003EA5 RID: 16037 RVA: 0x00161408 File Offset: 0x0015F608
	public static EffectPrefabs Instance { get; private set; }

	// Token: 0x06003EA6 RID: 16038 RVA: 0x00161410 File Offset: 0x0015F610
	private void Awake()
	{
		EffectPrefabs.Instance = this;
	}

	// Token: 0x0400269C RID: 9884
	public GameObject DreamBubble;

	// Token: 0x0400269D RID: 9885
	public GameObject ThoughtBubble;

	// Token: 0x0400269E RID: 9886
	public GameObject ThoughtBubbleConvo;

	// Token: 0x0400269F RID: 9887
	public GameObject MeteorBackground;

	// Token: 0x040026A0 RID: 9888
	public GameObject SparkleStreakFX;

	// Token: 0x040026A1 RID: 9889
	public GameObject HappySingerFX;

	// Token: 0x040026A2 RID: 9890
	public GameObject HugFrenzyFX;

	// Token: 0x040026A3 RID: 9891
	public GameObject GameplayEventDisplay;

	// Token: 0x040026A4 RID: 9892
	public GameObject OpenTemporalTearBeam;

	// Token: 0x040026A5 RID: 9893
	public GameObject MissileSmokeTrailFX;

	// Token: 0x040026A6 RID: 9894
	public GameObject LongRangeMissileSmokeTrailFX;

	// Token: 0x040026A7 RID: 9895
	public GameObject PlantPollinated;
}
