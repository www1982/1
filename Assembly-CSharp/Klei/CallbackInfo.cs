using System;

namespace Klei
{
	// Token: 0x02000FBB RID: 4027
	public struct CallbackInfo
	{
		// Token: 0x06007C9B RID: 31899 RVA: 0x0031F650 File Offset: 0x0031D850
		public CallbackInfo(HandleVector<Game.CallbackInfo>.Handle h)
		{
			this.handle = h;
		}

		// Token: 0x06007C9C RID: 31900 RVA: 0x0031F65C File Offset: 0x0031D85C
		public void Release()
		{
			if (this.handle.IsValid())
			{
				Game.CallbackInfo item = Game.Instance.callbackManager.GetItem(this.handle);
				global::System.Action cb = item.cb;
				if (!item.manuallyRelease)
				{
					Game.Instance.callbackManager.Release(this.handle);
				}
				cb();
			}
		}

		// Token: 0x04005DFF RID: 24063
		private HandleVector<Game.CallbackInfo>.Handle handle;
	}
}
