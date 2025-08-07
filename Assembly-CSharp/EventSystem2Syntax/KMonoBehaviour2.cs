using System;

namespace EventSystem2Syntax
{
	// Token: 0x02000EB2 RID: 3762
	internal class KMonoBehaviour2
	{
		// Token: 0x06007864 RID: 30820 RVA: 0x002EAEAC File Offset: 0x002E90AC
		protected virtual void OnPrefabInit()
		{
		}

		// Token: 0x06007865 RID: 30821 RVA: 0x002EAEAE File Offset: 0x002E90AE
		public void Subscribe(int evt, Action<object> cb)
		{
		}

		// Token: 0x06007866 RID: 30822 RVA: 0x002EAEB0 File Offset: 0x002E90B0
		public void Trigger(int evt, object data)
		{
		}

		// Token: 0x06007867 RID: 30823 RVA: 0x002EAEB2 File Offset: 0x002E90B2
		public void Subscribe<ListenerType, EventType>(Action<ListenerType, EventType> cb) where EventType : IEventData
		{
		}

		// Token: 0x06007868 RID: 30824 RVA: 0x002EAEB4 File Offset: 0x002E90B4
		public void Trigger<EventType>(EventType evt) where EventType : IEventData
		{
		}
	}
}
