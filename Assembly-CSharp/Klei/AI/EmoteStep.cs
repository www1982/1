using System;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02001008 RID: 4104
	public class EmoteStep
	{
		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06007E8D RID: 32397 RVA: 0x0032968B File Offset: 0x0032788B
		public int Id
		{
			get
			{
				return this.anim.HashValue;
			}
		}

		// Token: 0x06007E8E RID: 32398 RVA: 0x00329698 File Offset: 0x00327898
		public HandleVector<EmoteStep.Callbacks>.Handle RegisterCallbacks(Action<GameObject> startedCb, Action<GameObject> finishedCb)
		{
			if (startedCb == null && finishedCb == null)
			{
				return HandleVector<EmoteStep.Callbacks>.InvalidHandle;
			}
			EmoteStep.Callbacks callbacks = new EmoteStep.Callbacks
			{
				StartedCb = startedCb,
				FinishedCb = finishedCb
			};
			return this.callbacks.Add(callbacks);
		}

		// Token: 0x06007E8F RID: 32399 RVA: 0x003296D7 File Offset: 0x003278D7
		public void UnregisterCallbacks(HandleVector<EmoteStep.Callbacks>.Handle callbackHandle)
		{
			this.callbacks.Release(callbackHandle);
		}

		// Token: 0x06007E90 RID: 32400 RVA: 0x003296E6 File Offset: 0x003278E6
		public void UnregisterAllCallbacks()
		{
			this.callbacks = new HandleVector<EmoteStep.Callbacks>(64);
		}

		// Token: 0x06007E91 RID: 32401 RVA: 0x003296F8 File Offset: 0x003278F8
		public void OnStepStarted(HandleVector<EmoteStep.Callbacks>.Handle callbackHandle, GameObject parameter)
		{
			if (callbackHandle == HandleVector<EmoteStep.Callbacks>.Handle.InvalidHandle)
			{
				return;
			}
			EmoteStep.Callbacks item = this.callbacks.GetItem(callbackHandle);
			if (item.StartedCb != null)
			{
				item.StartedCb(parameter);
			}
		}

		// Token: 0x06007E92 RID: 32402 RVA: 0x00329734 File Offset: 0x00327934
		public void OnStepFinished(HandleVector<EmoteStep.Callbacks>.Handle callbackHandle, GameObject parameter)
		{
			if (callbackHandle == HandleVector<EmoteStep.Callbacks>.Handle.InvalidHandle)
			{
				return;
			}
			EmoteStep.Callbacks item = this.callbacks.GetItem(callbackHandle);
			if (item.FinishedCb != null)
			{
				item.FinishedCb(parameter);
			}
		}

		// Token: 0x04005F65 RID: 24421
		public HashedString anim = HashedString.Invalid;

		// Token: 0x04005F66 RID: 24422
		public KAnim.PlayMode mode = KAnim.PlayMode.Once;

		// Token: 0x04005F67 RID: 24423
		public float timeout = -1f;

		// Token: 0x04005F68 RID: 24424
		private HandleVector<EmoteStep.Callbacks> callbacks = new HandleVector<EmoteStep.Callbacks>(64);

		// Token: 0x020025EA RID: 9706
		public struct Callbacks
		{
			// Token: 0x0400A928 RID: 43304
			public Action<GameObject> StartedCb;

			// Token: 0x0400A929 RID: 43305
			public Action<GameObject> FinishedCb;
		}
	}
}
