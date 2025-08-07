using System;

namespace EventSystem2Syntax
{
	// Token: 0x02000EB0 RID: 3760
	internal class OldExample : KMonoBehaviour2
	{
		// Token: 0x0600785E RID: 30814 RVA: 0x002EAE1C File Offset: 0x002E901C
		protected override void OnPrefabInit()
		{
			base.OnPrefabInit();
			base.Subscribe(0, new Action<object>(this.OnObjectDestroyed));
			bool flag = false;
			base.Trigger(0, flag);
		}

		// Token: 0x0600785F RID: 30815 RVA: 0x002EAE51 File Offset: 0x002E9051
		private void OnObjectDestroyed(object data)
		{
			Debug.Log((bool)data);
		}
	}
}
