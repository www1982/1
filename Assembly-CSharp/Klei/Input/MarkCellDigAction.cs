using System;
using Klei.Actions;
using UnityEngine;

namespace Klei.Input
{
	// Token: 0x0200101B RID: 4123
	[Action("Mark Cell")]
	public class MarkCellDigAction : DigAction
	{
		// Token: 0x06007F06 RID: 32518 RVA: 0x0032B67C File Offset: 0x0032987C
		public override void Dig(int cell, int distFromOrigin)
		{
			GameObject gameObject = DigTool.PlaceDig(cell, distFromOrigin);
			if (gameObject != null)
			{
				Prioritizable component = gameObject.GetComponent<Prioritizable>();
				if (component != null)
				{
					component.SetMasterPriority(ToolMenu.Instance.PriorityScreen.GetLastSelectedPriority());
				}
			}
		}

		// Token: 0x06007F07 RID: 32519 RVA: 0x0032B6BF File Offset: 0x003298BF
		protected override void EntityDig(IDigActionEntity digActionEntity)
		{
			if (digActionEntity == null)
			{
				return;
			}
			digActionEntity.MarkForDig(true);
		}
	}
}
