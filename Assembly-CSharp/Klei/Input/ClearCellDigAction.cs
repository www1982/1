using System;
using Klei.Actions;

namespace Klei.Input
{
	// Token: 0x0200101D RID: 4125
	[Action("Clear Cell")]
	public class ClearCellDigAction : DigAction
	{
		// Token: 0x06007F0C RID: 32524 RVA: 0x0032B70C File Offset: 0x0032990C
		public override void Dig(int cell, int distFromOrigin)
		{
			if (Grid.Solid[cell] && !Grid.Foundation[cell])
			{
				SimMessages.Dig(cell, -1, true);
			}
		}

		// Token: 0x06007F0D RID: 32525 RVA: 0x0032B730 File Offset: 0x00329930
		protected override void EntityDig(IDigActionEntity digActionEntity)
		{
			if (digActionEntity == null)
			{
				return;
			}
			digActionEntity.Dig();
		}
	}
}
