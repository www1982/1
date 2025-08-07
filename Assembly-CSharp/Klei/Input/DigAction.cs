using System;
using Klei.Actions;
using UnityEngine;

namespace Klei.Input
{
	// Token: 0x0200101A RID: 4122
	[ActionType("InterfaceTool", "Dig", true)]
	public abstract class DigAction
	{
		// Token: 0x06007F02 RID: 32514 RVA: 0x0032B5FC File Offset: 0x003297FC
		public void Uproot(int cell)
		{
			if (!Grid.ObjectLayers[1].ContainsKey(cell))
			{
				if (Grid.ObjectLayers[5].ContainsKey(cell))
				{
					GameObject gameObject = Grid.ObjectLayers[5][cell];
					if (gameObject == null)
					{
						return;
					}
					IDigActionEntity component = gameObject.GetComponent<IDigActionEntity>();
					this.EntityDig(component);
				}
				return;
			}
			GameObject gameObject2 = Grid.ObjectLayers[1][cell];
			if (gameObject2 == null)
			{
				return;
			}
			IDigActionEntity component2 = gameObject2.GetComponent<IDigActionEntity>();
			this.EntityDig(component2);
		}

		// Token: 0x06007F03 RID: 32515
		public abstract void Dig(int cell, int distFromOrigin);

		// Token: 0x06007F04 RID: 32516
		protected abstract void EntityDig(IDigActionEntity digAction);
	}
}
