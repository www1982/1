using System;
using System.Collections.Generic;
using Klei.Actions;
using UnityEngine;

namespace Klei.Input
{
	// Token: 0x02001019 RID: 4121
	[CreateAssetMenu(fileName = "InterfaceToolConfig", menuName = "Klei/Interface Tools/Config")]
	public class InterfaceToolConfig : ScriptableObject
	{
		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06007EFD RID: 32509 RVA: 0x0032B597 File Offset: 0x00329797
		public DigAction DigAction
		{
			get
			{
				return ActionFactory<DigToolActionFactory, DigAction, DigToolActionFactory.Actions>.GetOrCreateAction(this.digAction);
			}
		}

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x06007EFE RID: 32510 RVA: 0x0032B5A4 File Offset: 0x003297A4
		public int Priority
		{
			get
			{
				return this.priority;
			}
		}

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x06007EFF RID: 32511 RVA: 0x0032B5AC File Offset: 0x003297AC
		public global::Action InputAction
		{
			get
			{
				return (global::Action)Enum.Parse(typeof(global::Action), this.inputAction);
			}
		}

		// Token: 0x04005FB4 RID: 24500
		[SerializeField]
		private DigToolActionFactory.Actions digAction;

		// Token: 0x04005FB5 RID: 24501
		public static InterfaceToolConfig.Comparer ConfigComparer = new InterfaceToolConfig.Comparer();

		// Token: 0x04005FB6 RID: 24502
		[SerializeField]
		[Tooltip("Defines which config will take priority should multiple configs be activated\n0 is the lower bound for this value.")]
		private int priority;

		// Token: 0x04005FB7 RID: 24503
		[SerializeField]
		[Tooltip("This will serve as a key for activating different configs. Currently, these Actionsare how we indicate that different input modes are desired.\nAssigning Action.Invalid to this field will indicate that this is the \"default\" config")]
		private string inputAction = global::Action.Invalid.ToString();

		// Token: 0x020025FC RID: 9724
		public class Comparer : IComparer<InterfaceToolConfig>
		{
			// Token: 0x0600C249 RID: 49737 RVA: 0x0040A93B File Offset: 0x00408B3B
			public int Compare(InterfaceToolConfig lhs, InterfaceToolConfig rhs)
			{
				if (lhs.Priority == rhs.Priority)
				{
					return 0;
				}
				if (lhs.Priority <= rhs.Priority)
				{
					return -1;
				}
				return 1;
			}
		}
	}
}
