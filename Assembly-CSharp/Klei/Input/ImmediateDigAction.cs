using System;
using Klei.Actions;

namespace Klei.Input
{
	// Token: 0x0200101C RID: 4124
	[Action("Immediate")]
	public class ImmediateDigAction : DigAction
	{
		// Token: 0x06007F09 RID: 32521 RVA: 0x0032B6D4 File Offset: 0x003298D4
		public override void Dig(int cell, int distFromOrigin)
		{
			if (Grid.Solid[cell] && !Grid.Foundation[cell])
			{
				SimMessages.Dig(cell, -1, false);
			}
		}

		// Token: 0x06007F0A RID: 32522 RVA: 0x0032B6F8 File Offset: 0x003298F8
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
