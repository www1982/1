using System;

namespace Klei.AI
{
	// Token: 0x02000FFF RID: 4095
	public class FertilityModifier : Resource
	{
		// Token: 0x06007E46 RID: 32326 RVA: 0x0032886C File Offset: 0x00326A6C
		public FertilityModifier(string id, Tag targetTag, string name, string description, Func<string, string> tooltipCB, FertilityModifier.FertilityModFn applyFunction)
			: base(id, name)
		{
			this.Description = description;
			this.TargetTag = targetTag;
			this.TooltipCB = tooltipCB;
			this.ApplyFunction = applyFunction;
		}

		// Token: 0x06007E47 RID: 32327 RVA: 0x00328895 File Offset: 0x00326A95
		public string GetTooltip()
		{
			if (this.TooltipCB != null)
			{
				return this.TooltipCB(this.Description);
			}
			return this.Description;
		}

		// Token: 0x04005F4F RID: 24399
		public string Description;

		// Token: 0x04005F50 RID: 24400
		public Tag TargetTag;

		// Token: 0x04005F51 RID: 24401
		public Func<string, string> TooltipCB;

		// Token: 0x04005F52 RID: 24402
		public FertilityModifier.FertilityModFn ApplyFunction;

		// Token: 0x020025E9 RID: 9705
		// (Invoke) Token: 0x0600C212 RID: 49682
		public delegate void FertilityModFn(FertilityMonitor.Instance inst, Tag eggTag);
	}
}
