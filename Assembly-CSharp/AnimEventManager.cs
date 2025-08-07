using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

// Token: 0x0200051C RID: 1308
public class AnimEventManager : Singleton<AnimEventManager>
{
	// Token: 0x06001C11 RID: 7185 RVA: 0x000984DA File Offset: 0x000966DA
	public void FreeResources()
	{
	}

	// Token: 0x06001C12 RID: 7186 RVA: 0x000984DC File Offset: 0x000966DC
	public HandleVector<int>.Handle PlayAnim(KAnimControllerBase controller, KAnim.Anim anim, KAnim.PlayMode mode, float time, bool use_unscaled_time)
	{
		AnimEventManager.AnimData animData = default(AnimEventManager.AnimData);
		animData.frameRate = anim.frameRate;
		animData.totalTime = anim.totalTime;
		animData.numFrames = anim.numFrames;
		animData.useUnscaledTime = use_unscaled_time;
		AnimEventManager.EventPlayerData eventPlayerData = default(AnimEventManager.EventPlayerData);
		eventPlayerData.elapsedTime = time;
		eventPlayerData.mode = mode;
		eventPlayerData.controller = controller as KBatchedAnimController;
		eventPlayerData.currentFrame = eventPlayerData.controller.GetFrameIdx(eventPlayerData.elapsedTime, false);
		eventPlayerData.previousFrame = -1;
		eventPlayerData.events = null;
		eventPlayerData.updatingEvents = null;
		eventPlayerData.events = GameAudioSheets.Get().GetEvents(anim.id);
		if (eventPlayerData.events == null)
		{
			eventPlayerData.events = AnimEventManager.emptyEventList;
		}
		HandleVector<int>.Handle handle3;
		if (animData.useUnscaledTime)
		{
			HandleVector<int>.Handle handle = this.uiAnimData.Allocate(animData);
			HandleVector<int>.Handle handle2 = this.uiEventData.Allocate(eventPlayerData);
			handle3 = this.indirectionData.Allocate(new AnimEventManager.IndirectionData(handle, handle2, true));
		}
		else
		{
			HandleVector<int>.Handle handle4 = this.animData.Allocate(animData);
			HandleVector<int>.Handle handle5 = this.eventData.Allocate(eventPlayerData);
			handle3 = this.indirectionData.Allocate(new AnimEventManager.IndirectionData(handle4, handle5, false));
		}
		return handle3;
	}

	// Token: 0x06001C13 RID: 7187 RVA: 0x00098610 File Offset: 0x00096810
	public void SetMode(HandleVector<int>.Handle handle, KAnim.PlayMode mode)
	{
		if (!handle.IsValid())
		{
			return;
		}
		AnimEventManager.IndirectionData data = this.indirectionData.GetData(handle);
		KCompactedVector<AnimEventManager.EventPlayerData> kcompactedVector = (data.isUIData ? this.uiEventData : this.eventData);
		AnimEventManager.EventPlayerData data2 = kcompactedVector.GetData(data.eventDataHandle);
		data2.mode = mode;
		kcompactedVector.SetData(data.eventDataHandle, data2);
	}

	// Token: 0x06001C14 RID: 7188 RVA: 0x0009866C File Offset: 0x0009686C
	public void StopAnim(HandleVector<int>.Handle handle)
	{
		if (!handle.IsValid())
		{
			return;
		}
		AnimEventManager.IndirectionData data = this.indirectionData.GetData(handle);
		KCompactedVector<AnimEventManager.AnimData> kcompactedVector = (data.isUIData ? this.uiAnimData : this.animData);
		KCompactedVector<AnimEventManager.EventPlayerData> kcompactedVector2 = (data.isUIData ? this.uiEventData : this.eventData);
		AnimEventManager.EventPlayerData data2 = kcompactedVector2.GetData(data.eventDataHandle);
		this.StopEvents(data2);
		kcompactedVector.Free(data.animDataHandle);
		kcompactedVector2.Free(data.eventDataHandle);
		this.indirectionData.Free(handle);
	}

	// Token: 0x06001C15 RID: 7189 RVA: 0x000986F8 File Offset: 0x000968F8
	public float GetElapsedTime(HandleVector<int>.Handle handle)
	{
		AnimEventManager.IndirectionData data = this.indirectionData.GetData(handle);
		return (data.isUIData ? this.uiEventData : this.eventData).GetData(data.eventDataHandle).elapsedTime;
	}

	// Token: 0x06001C16 RID: 7190 RVA: 0x00098738 File Offset: 0x00096938
	public void SetElapsedTime(HandleVector<int>.Handle handle, float elapsed_time)
	{
		AnimEventManager.IndirectionData data = this.indirectionData.GetData(handle);
		KCompactedVector<AnimEventManager.EventPlayerData> kcompactedVector = (data.isUIData ? this.uiEventData : this.eventData);
		AnimEventManager.EventPlayerData data2 = kcompactedVector.GetData(data.eventDataHandle);
		data2.elapsedTime = elapsed_time;
		kcompactedVector.SetData(data.eventDataHandle, data2);
	}

	// Token: 0x06001C17 RID: 7191 RVA: 0x0009878C File Offset: 0x0009698C
	public void Update()
	{
		float deltaTime = Time.deltaTime;
		float unscaledDeltaTime = Time.unscaledDeltaTime;
		this.Update(deltaTime, this.animData.GetDataList(), this.eventData.GetDataList());
		this.Update(unscaledDeltaTime, this.uiAnimData.GetDataList(), this.uiEventData.GetDataList());
		for (int i = 0; i < this.finishedCalls.Count; i++)
		{
			this.finishedCalls[i].TriggerStop();
		}
		this.finishedCalls.Clear();
	}

	// Token: 0x06001C18 RID: 7192 RVA: 0x00098814 File Offset: 0x00096A14
	private void Update(float dt, List<AnimEventManager.AnimData> anim_data, List<AnimEventManager.EventPlayerData> event_data)
	{
		if (dt <= 0f)
		{
			return;
		}
		for (int i = 0; i < event_data.Count; i++)
		{
			AnimEventManager.EventPlayerData eventPlayerData = event_data[i];
			if (!(eventPlayerData.controller == null) && eventPlayerData.mode != KAnim.PlayMode.Paused)
			{
				eventPlayerData.currentFrame = eventPlayerData.controller.GetFrameIdx(eventPlayerData.elapsedTime, false);
				event_data[i] = eventPlayerData;
				this.PlayEvents(eventPlayerData);
				eventPlayerData.previousFrame = eventPlayerData.currentFrame;
				eventPlayerData.elapsedTime += dt * eventPlayerData.controller.GetPlaySpeed();
				event_data[i] = eventPlayerData;
				if (eventPlayerData.updatingEvents != null)
				{
					for (int j = 0; j < eventPlayerData.updatingEvents.Count; j++)
					{
						eventPlayerData.updatingEvents[j].OnUpdate(eventPlayerData);
					}
				}
				event_data[i] = eventPlayerData;
				if (eventPlayerData.mode != KAnim.PlayMode.Loop && eventPlayerData.currentFrame >= anim_data[i].numFrames - 1)
				{
					this.StopEvents(eventPlayerData);
					this.finishedCalls.Add(eventPlayerData.controller);
				}
			}
		}
	}

	// Token: 0x06001C19 RID: 7193 RVA: 0x0009892C File Offset: 0x00096B2C
	private void PlayEvents(AnimEventManager.EventPlayerData data)
	{
		for (int i = 0; i < data.events.Count; i++)
		{
			data.events[i].Play(data);
		}
	}

	// Token: 0x06001C1A RID: 7194 RVA: 0x00098964 File Offset: 0x00096B64
	private void StopEvents(AnimEventManager.EventPlayerData data)
	{
		for (int i = 0; i < data.events.Count; i++)
		{
			data.events[i].Stop(data);
		}
		if (data.updatingEvents != null)
		{
			data.updatingEvents.Clear();
		}
	}

	// Token: 0x06001C1B RID: 7195 RVA: 0x000989AC File Offset: 0x00096BAC
	public AnimEventManager.DevTools_DebugInfo DevTools_GetDebugInfo()
	{
		return new AnimEventManager.DevTools_DebugInfo(this, this.animData, this.eventData, this.uiAnimData, this.uiEventData);
	}

	// Token: 0x04001074 RID: 4212
	private static readonly List<AnimEvent> emptyEventList = new List<AnimEvent>();

	// Token: 0x04001075 RID: 4213
	private const int INITIAL_VECTOR_SIZE = 256;

	// Token: 0x04001076 RID: 4214
	private KCompactedVector<AnimEventManager.AnimData> animData = new KCompactedVector<AnimEventManager.AnimData>(256);

	// Token: 0x04001077 RID: 4215
	private KCompactedVector<AnimEventManager.EventPlayerData> eventData = new KCompactedVector<AnimEventManager.EventPlayerData>(256);

	// Token: 0x04001078 RID: 4216
	private KCompactedVector<AnimEventManager.AnimData> uiAnimData = new KCompactedVector<AnimEventManager.AnimData>(256);

	// Token: 0x04001079 RID: 4217
	private KCompactedVector<AnimEventManager.EventPlayerData> uiEventData = new KCompactedVector<AnimEventManager.EventPlayerData>(256);

	// Token: 0x0400107A RID: 4218
	private KCompactedVector<AnimEventManager.IndirectionData> indirectionData = new KCompactedVector<AnimEventManager.IndirectionData>(0);

	// Token: 0x0400107B RID: 4219
	private List<KBatchedAnimController> finishedCalls = new List<KBatchedAnimController>();

	// Token: 0x0200137C RID: 4988
	public struct AnimData
	{
		// Token: 0x04006972 RID: 26994
		public float frameRate;

		// Token: 0x04006973 RID: 26995
		public float totalTime;

		// Token: 0x04006974 RID: 26996
		public int numFrames;

		// Token: 0x04006975 RID: 26997
		public bool useUnscaledTime;
	}

	// Token: 0x0200137D RID: 4989
	[DebuggerDisplay("{controller.name}, Anim={currentAnim}, Frame={currentFrame}, Mode={mode}")]
	public struct EventPlayerData
	{
		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x06008AAA RID: 35498 RVA: 0x00350D0B File Offset: 0x0034EF0B
		// (set) Token: 0x06008AAB RID: 35499 RVA: 0x00350D13 File Offset: 0x0034EF13
		public int currentFrame { readonly get; set; }

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06008AAC RID: 35500 RVA: 0x00350D1C File Offset: 0x0034EF1C
		// (set) Token: 0x06008AAD RID: 35501 RVA: 0x00350D24 File Offset: 0x0034EF24
		public int previousFrame { readonly get; set; }

		// Token: 0x06008AAE RID: 35502 RVA: 0x00350D2D File Offset: 0x0034EF2D
		public ComponentType GetComponent<ComponentType>()
		{
			return this.controller.GetComponent<ComponentType>();
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06008AAF RID: 35503 RVA: 0x00350D3A File Offset: 0x0034EF3A
		public string name
		{
			get
			{
				return this.controller.name;
			}
		}

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06008AB0 RID: 35504 RVA: 0x00350D47 File Offset: 0x0034EF47
		public float normalizedTime
		{
			get
			{
				return this.elapsedTime / this.controller.CurrentAnim.totalTime;
			}
		}

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06008AB1 RID: 35505 RVA: 0x00350D60 File Offset: 0x0034EF60
		public Vector3 position
		{
			get
			{
				return this.controller.transform.GetPosition();
			}
		}

		// Token: 0x06008AB2 RID: 35506 RVA: 0x00350D72 File Offset: 0x0034EF72
		public void AddUpdatingEvent(AnimEvent ev)
		{
			if (this.updatingEvents == null)
			{
				this.updatingEvents = new List<AnimEvent>();
			}
			this.updatingEvents.Add(ev);
		}

		// Token: 0x06008AB3 RID: 35507 RVA: 0x00350D93 File Offset: 0x0034EF93
		public void SetElapsedTime(float elapsedTime)
		{
			this.elapsedTime = elapsedTime;
		}

		// Token: 0x06008AB4 RID: 35508 RVA: 0x00350D9C File Offset: 0x0034EF9C
		public void FreeResources()
		{
			this.elapsedTime = 0f;
			this.mode = KAnim.PlayMode.Once;
			this.currentFrame = 0;
			this.previousFrame = 0;
			this.events = null;
			this.updatingEvents = null;
			this.controller = null;
		}

		// Token: 0x04006976 RID: 26998
		public float elapsedTime;

		// Token: 0x04006977 RID: 26999
		public KAnim.PlayMode mode;

		// Token: 0x0400697A RID: 27002
		public List<AnimEvent> events;

		// Token: 0x0400697B RID: 27003
		public List<AnimEvent> updatingEvents;

		// Token: 0x0400697C RID: 27004
		public KBatchedAnimController controller;
	}

	// Token: 0x0200137E RID: 4990
	private struct IndirectionData
	{
		// Token: 0x06008AB5 RID: 35509 RVA: 0x00350DD3 File Offset: 0x0034EFD3
		public IndirectionData(HandleVector<int>.Handle anim_data_handle, HandleVector<int>.Handle event_data_handle, bool is_ui_data)
		{
			this.isUIData = is_ui_data;
			this.animDataHandle = anim_data_handle;
			this.eventDataHandle = event_data_handle;
		}

		// Token: 0x0400697D RID: 27005
		public bool isUIData;

		// Token: 0x0400697E RID: 27006
		public HandleVector<int>.Handle animDataHandle;

		// Token: 0x0400697F RID: 27007
		public HandleVector<int>.Handle eventDataHandle;
	}

	// Token: 0x0200137F RID: 4991
	public readonly struct DevTools_DebugInfo
	{
		// Token: 0x06008AB6 RID: 35510 RVA: 0x00350DEA File Offset: 0x0034EFEA
		public DevTools_DebugInfo(AnimEventManager eventManager, KCompactedVector<AnimEventManager.AnimData> animData, KCompactedVector<AnimEventManager.EventPlayerData> eventData, KCompactedVector<AnimEventManager.AnimData> uiAnimData, KCompactedVector<AnimEventManager.EventPlayerData> uiEventData)
		{
			this.eventManager = eventManager;
			this.animData = animData;
			this.eventData = eventData;
			this.uiAnimData = uiAnimData;
			this.uiEventData = uiEventData;
		}

		// Token: 0x04006980 RID: 27008
		public readonly AnimEventManager eventManager;

		// Token: 0x04006981 RID: 27009
		public readonly KCompactedVector<AnimEventManager.AnimData> animData;

		// Token: 0x04006982 RID: 27010
		public readonly KCompactedVector<AnimEventManager.EventPlayerData> eventData;

		// Token: 0x04006983 RID: 27011
		public readonly KCompactedVector<AnimEventManager.AnimData> uiAnimData;

		// Token: 0x04006984 RID: 27012
		public readonly KCompactedVector<AnimEventManager.EventPlayerData> uiEventData;
	}
}
