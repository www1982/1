using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000BE2 RID: 3042
public class WhiteBoard : KGameObjectComponentManager<WhiteBoard.Data>, IKComponentManager
{
	// Token: 0x06005B2D RID: 23341 RVA: 0x0020F220 File Offset: 0x0020D420
	public HandleVector<int>.Handle Add(GameObject go)
	{
		return base.Add(go, new WhiteBoard.Data
		{
			keyValueStore = new Dictionary<HashedString, object>()
		});
	}

	// Token: 0x06005B2E RID: 23342 RVA: 0x0020F24C File Offset: 0x0020D44C
	protected override void OnCleanUp(HandleVector<int>.Handle h)
	{
		WhiteBoard.Data data = base.GetData(h);
		data.keyValueStore.Clear();
		data.keyValueStore = null;
		base.SetData(h, data);
	}

	// Token: 0x06005B2F RID: 23343 RVA: 0x0020F27C File Offset: 0x0020D47C
	public bool HasValue(HandleVector<int>.Handle h, HashedString key)
	{
		return h.IsValid() && base.GetData(h).keyValueStore.ContainsKey(key);
	}

	// Token: 0x06005B30 RID: 23344 RVA: 0x0020F29B File Offset: 0x0020D49B
	public object GetValue(HandleVector<int>.Handle h, HashedString key)
	{
		return base.GetData(h).keyValueStore[key];
	}

	// Token: 0x06005B31 RID: 23345 RVA: 0x0020F2B0 File Offset: 0x0020D4B0
	public void SetValue(HandleVector<int>.Handle h, HashedString key, object value)
	{
		if (!h.IsValid())
		{
			return;
		}
		WhiteBoard.Data data = base.GetData(h);
		data.keyValueStore[key] = value;
		base.SetData(h, data);
	}

	// Token: 0x06005B32 RID: 23346 RVA: 0x0020F2E4 File Offset: 0x0020D4E4
	public void RemoveValue(HandleVector<int>.Handle h, HashedString key)
	{
		if (!h.IsValid())
		{
			return;
		}
		WhiteBoard.Data data = base.GetData(h);
		data.keyValueStore.Remove(key);
		base.SetData(h, data);
	}

	// Token: 0x02001D14 RID: 7444
	public struct Data
	{
		// Token: 0x04008820 RID: 34848
		public Dictionary<HashedString, object> keyValueStore;
	}
}
