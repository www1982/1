using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000CCE RID: 3278
public class FrontEndBackground : UIDupeRandomizer
{
	// Token: 0x06006504 RID: 25860 RVA: 0x0025FDF8 File Offset: 0x0025DFF8
	protected override void Start()
	{
		this.tuning = TuningData<FrontEndBackground.Tuning>.Get();
		base.Start();
		for (int i = 0; i < this.anims.Length; i++)
		{
			int minionIndex = i;
			KBatchedAnimController kbatchedAnimController = this.anims[i].minions[0];
			if (kbatchedAnimController.gameObject.activeInHierarchy)
			{
				kbatchedAnimController.onAnimComplete += delegate(HashedString name)
				{
					this.WaitForABit(minionIndex, name);
				};
				this.WaitForABit(i, HashedString.Invalid);
			}
		}
		this.dreckoController = base.transform.GetChild(0).Find("startmenu_drecko").GetComponent<KBatchedAnimController>();
		if (this.dreckoController.gameObject.activeInHierarchy)
		{
			this.dreckoController.enabled = false;
			this.nextDreckoTime = global::UnityEngine.Random.Range(this.tuning.minFirstDreckoInterval, this.tuning.maxFirstDreckoInterval) + Time.unscaledTime;
		}
	}

	// Token: 0x06006505 RID: 25861 RVA: 0x0025FEE6 File Offset: 0x0025E0E6
	protected override void Update()
	{
		base.Update();
		this.UpdateDrecko();
	}

	// Token: 0x06006506 RID: 25862 RVA: 0x0025FEF4 File Offset: 0x0025E0F4
	private void UpdateDrecko()
	{
		if (this.dreckoController.gameObject.activeInHierarchy && Time.unscaledTime > this.nextDreckoTime)
		{
			this.dreckoController.enabled = true;
			this.dreckoController.Play("idle", KAnim.PlayMode.Once, 1f, 0f);
			this.nextDreckoTime = global::UnityEngine.Random.Range(this.tuning.minDreckoInterval, this.tuning.maxDreckoInterval) + Time.unscaledTime;
		}
	}

	// Token: 0x06006507 RID: 25863 RVA: 0x0025FF73 File Offset: 0x0025E173
	private void WaitForABit(int minion_idx, HashedString name)
	{
		base.StartCoroutine(this.WaitForTime(minion_idx));
	}

	// Token: 0x06006508 RID: 25864 RVA: 0x0025FF83 File Offset: 0x0025E183
	private IEnumerator WaitForTime(int minion_idx)
	{
		this.anims[minion_idx].lastWaitTime = global::UnityEngine.Random.Range(this.anims[minion_idx].minSecondsBetweenAction, this.anims[minion_idx].maxSecondsBetweenAction);
		yield return new WaitForSecondsRealtime(this.anims[minion_idx].lastWaitTime);
		base.GetNewBody(minion_idx);
		using (List<KBatchedAnimController>.Enumerator enumerator = this.anims[minion_idx].minions.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KBatchedAnimController kbatchedAnimController = enumerator.Current;
				kbatchedAnimController.ClearQueue();
				kbatchedAnimController.Play(this.anims[minion_idx].anim_name, KAnim.PlayMode.Once, 1f, 0f);
			}
			yield break;
		}
		yield break;
	}

	// Token: 0x04004509 RID: 17673
	private KBatchedAnimController dreckoController;

	// Token: 0x0400450A RID: 17674
	private float nextDreckoTime;

	// Token: 0x0400450B RID: 17675
	private FrontEndBackground.Tuning tuning;

	// Token: 0x02001E9B RID: 7835
	public class Tuning : TuningData<FrontEndBackground.Tuning>
	{
		// Token: 0x04008E0A RID: 36362
		public float minDreckoInterval;

		// Token: 0x04008E0B RID: 36363
		public float maxDreckoInterval;

		// Token: 0x04008E0C RID: 36364
		public float minFirstDreckoInterval;

		// Token: 0x04008E0D RID: 36365
		public float maxFirstDreckoInterval;
	}
}
