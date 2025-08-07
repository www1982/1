using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200044C RID: 1100
public static class SequenceUtil
{
	// Token: 0x1700005D RID: 93
	// (get) Token: 0x060016D6 RID: 5846 RVA: 0x0008141C File Offset: 0x0007F61C
	public static YieldInstruction WaitForNextFrame
	{
		get
		{
			return null;
		}
	}

	// Token: 0x1700005E RID: 94
	// (get) Token: 0x060016D7 RID: 5847 RVA: 0x0008141F File Offset: 0x0007F61F
	public static YieldInstruction WaitForEndOfFrame
	{
		get
		{
			if (SequenceUtil.waitForEndOfFrame == null)
			{
				SequenceUtil.waitForEndOfFrame = new WaitForEndOfFrame();
			}
			return SequenceUtil.waitForEndOfFrame;
		}
	}

	// Token: 0x1700005F RID: 95
	// (get) Token: 0x060016D8 RID: 5848 RVA: 0x00081437 File Offset: 0x0007F637
	public static YieldInstruction WaitForFixedUpdate
	{
		get
		{
			if (SequenceUtil.waitForFixedUpdate == null)
			{
				SequenceUtil.waitForFixedUpdate = new WaitForFixedUpdate();
			}
			return SequenceUtil.waitForFixedUpdate;
		}
	}

	// Token: 0x060016D9 RID: 5849 RVA: 0x00081450 File Offset: 0x0007F650
	public static YieldInstruction WaitForSeconds(float duration)
	{
		WaitForSeconds waitForSeconds;
		if (!SequenceUtil.scaledTimeCache.TryGetValue(duration, out waitForSeconds))
		{
			waitForSeconds = (SequenceUtil.scaledTimeCache[duration] = new WaitForSeconds(duration));
		}
		return waitForSeconds;
	}

	// Token: 0x060016DA RID: 5850 RVA: 0x00081484 File Offset: 0x0007F684
	public static WaitForSecondsRealtime WaitForSecondsRealtime(float duration)
	{
		WaitForSecondsRealtime waitForSecondsRealtime;
		if (!SequenceUtil.reailTimeWaitCache.TryGetValue(duration, out waitForSecondsRealtime))
		{
			waitForSecondsRealtime = (SequenceUtil.reailTimeWaitCache[duration] = new WaitForSecondsRealtime(duration));
		}
		return waitForSecondsRealtime;
	}

	// Token: 0x04000D5F RID: 3423
	private static WaitForEndOfFrame waitForEndOfFrame = null;

	// Token: 0x04000D60 RID: 3424
	private static WaitForFixedUpdate waitForFixedUpdate = null;

	// Token: 0x04000D61 RID: 3425
	private static Dictionary<float, WaitForSeconds> scaledTimeCache = new Dictionary<float, WaitForSeconds>();

	// Token: 0x04000D62 RID: 3426
	private static Dictionary<float, WaitForSecondsRealtime> reailTimeWaitCache = new Dictionary<float, WaitForSecondsRealtime>();
}
