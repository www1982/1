using System;

namespace EventSystem2Syntax
{
	// Token: 0x02000EB1 RID: 3761
	internal class NewExample : KMonoBehaviour2
	{
		// Token: 0x06007861 RID: 30817 RVA: 0x002EAE6C File Offset: 0x002E906C
		protected override void OnPrefabInit()
		{
			base.Subscribe<NewExample, NewExample.ObjectDestroyedEvent>(new Action<NewExample, NewExample.ObjectDestroyedEvent>(NewExample.OnObjectDestroyed));
			base.Trigger<NewExample.ObjectDestroyedEvent>(new NewExample.ObjectDestroyedEvent
			{
				parameter = false
			});
		}

		// Token: 0x06007862 RID: 30818 RVA: 0x002EAEA2 File Offset: 0x002E90A2
		private static void OnObjectDestroyed(NewExample example, NewExample.ObjectDestroyedEvent evt)
		{
		}

		// Token: 0x020020C8 RID: 8392
		private struct ObjectDestroyedEvent : IEventData
		{
			// Token: 0x0400954C RID: 38220
			public bool parameter;
		}
	}
}
