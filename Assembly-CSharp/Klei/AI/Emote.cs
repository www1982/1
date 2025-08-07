using System;
using System.Collections.Generic;

namespace Klei.AI
{
	// Token: 0x02001007 RID: 4103
	public class Emote : Resource
	{
		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x06007E83 RID: 32387 RVA: 0x003293E9 File Offset: 0x003275E9
		public int StepCount
		{
			get
			{
				if (this.emoteSteps != null)
				{
					return this.emoteSteps.Count;
				}
				return 0;
			}
		}

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06007E84 RID: 32388 RVA: 0x00329400 File Offset: 0x00327600
		public KAnimFile AnimSet
		{
			get
			{
				if (this.animSetName != HashedString.Invalid && this.animSet == null)
				{
					this.animSet = Assets.GetAnim(this.animSetName);
				}
				return this.animSet;
			}
		}

		// Token: 0x06007E85 RID: 32389 RVA: 0x00329439 File Offset: 0x00327639
		public Emote(ResourceSet parent, string emoteId, EmoteStep[] defaultSteps, string animSetName = null)
			: base(emoteId, parent, null)
		{
			this.emoteSteps.AddRange(defaultSteps);
			this.animSetName = animSetName;
		}

		// Token: 0x06007E86 RID: 32390 RVA: 0x00329474 File Offset: 0x00327674
		public bool IsValidForController(KBatchedAnimController animController)
		{
			bool flag = true;
			int num = 0;
			while (flag && num < this.StepCount)
			{
				flag = animController.HasAnimation(this.emoteSteps[num].anim);
				num++;
			}
			KAnimFileData kanimFileData = ((this.animSet == null) ? null : this.animSet.GetData());
			int num2 = 0;
			while (kanimFileData != null && flag && num2 < this.StepCount)
			{
				bool flag2 = false;
				int num3 = 0;
				while (!flag2 && num3 < kanimFileData.animCount)
				{
					flag2 = kanimFileData.GetAnim(num2).id == this.emoteSteps[num2].anim;
					num3++;
				}
				flag = flag2;
				num2++;
			}
			return flag;
		}

		// Token: 0x06007E87 RID: 32391 RVA: 0x0032952C File Offset: 0x0032772C
		public void ApplyAnimOverrides(KBatchedAnimController animController, KAnimFile overrideSet)
		{
			KAnimFile kanimFile = ((overrideSet != null) ? overrideSet : this.AnimSet);
			if (kanimFile == null || animController == null)
			{
				return;
			}
			animController.AddAnimOverrides(kanimFile, 0f);
		}

		// Token: 0x06007E88 RID: 32392 RVA: 0x0032956C File Offset: 0x0032776C
		public void RemoveAnimOverrides(KBatchedAnimController animController, KAnimFile overrideSet)
		{
			KAnimFile kanimFile = ((overrideSet != null) ? overrideSet : this.AnimSet);
			if (kanimFile == null || animController == null)
			{
				return;
			}
			animController.RemoveAnimOverrides(kanimFile);
		}

		// Token: 0x06007E89 RID: 32393 RVA: 0x003295A8 File Offset: 0x003277A8
		public void CollectStepAnims(out HashedString[] emoteAnims, int iterations)
		{
			emoteAnims = new HashedString[this.emoteSteps.Count * iterations];
			for (int i = 0; i < emoteAnims.Length; i++)
			{
				emoteAnims[i] = this.emoteSteps[i % this.emoteSteps.Count].anim;
			}
		}

		// Token: 0x06007E8A RID: 32394 RVA: 0x003295FD File Offset: 0x003277FD
		public bool IsValidStep(int stepIdx)
		{
			return stepIdx >= 0 && stepIdx < this.emoteSteps.Count;
		}

		// Token: 0x170008F4 RID: 2292
		public EmoteStep this[int stepIdx]
		{
			get
			{
				if (!this.IsValidStep(stepIdx))
				{
					return null;
				}
				return this.emoteSteps[stepIdx];
			}
		}

		// Token: 0x06007E8C RID: 32396 RVA: 0x0032962C File Offset: 0x0032782C
		public int GetStepIndex(HashedString animName)
		{
			int i = 0;
			bool flag = false;
			while (i < this.emoteSteps.Count)
			{
				if (this.emoteSteps[i].anim == animName)
				{
					flag = true;
					break;
				}
				i++;
			}
			Debug.Assert(flag, string.Format("Could not find emote step {0} for emote {1}!", animName, this.Id));
			return i;
		}

		// Token: 0x04005F62 RID: 24418
		private HashedString animSetName = null;

		// Token: 0x04005F63 RID: 24419
		private KAnimFile animSet;

		// Token: 0x04005F64 RID: 24420
		private List<EmoteStep> emoteSteps = new List<EmoteStep>();
	}
}
