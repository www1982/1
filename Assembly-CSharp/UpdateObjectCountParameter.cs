using System;
using System.Collections.Generic;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

// Token: 0x0200053B RID: 1339
internal class UpdateObjectCountParameter : LoopingSoundParameterUpdater
{
	// Token: 0x06001D99 RID: 7577 RVA: 0x000A0628 File Offset: 0x0009E828
	public static UpdateObjectCountParameter.Settings GetSettings(HashedString path_hash, SoundDescription description)
	{
		UpdateObjectCountParameter.Settings settings = default(UpdateObjectCountParameter.Settings);
		if (!UpdateObjectCountParameter.settings.TryGetValue(path_hash, out settings))
		{
			settings = default(UpdateObjectCountParameter.Settings);
			EventDescription eventDescription = RuntimeManager.GetEventDescription(description.path);
			USER_PROPERTY user_PROPERTY;
			if (eventDescription.getUserProperty("minObj", out user_PROPERTY) == RESULT.OK)
			{
				settings.minObjects = (float)((short)user_PROPERTY.floatValue());
			}
			else
			{
				settings.minObjects = 1f;
			}
			USER_PROPERTY user_PROPERTY2;
			if (eventDescription.getUserProperty("maxObj", out user_PROPERTY2) == RESULT.OK)
			{
				settings.maxObjects = user_PROPERTY2.floatValue();
			}
			else
			{
				settings.maxObjects = 0f;
			}
			USER_PROPERTY user_PROPERTY3;
			if (eventDescription.getUserProperty("curveType", out user_PROPERTY3) == RESULT.OK && user_PROPERTY3.stringValue() == "exp")
			{
				settings.useExponentialCurve = true;
			}
			settings.parameterId = description.GetParameterId(UpdateObjectCountParameter.parameterHash);
			settings.path = path_hash;
			UpdateObjectCountParameter.settings[path_hash] = settings;
		}
		return settings;
	}

	// Token: 0x06001D9A RID: 7578 RVA: 0x000A0710 File Offset: 0x0009E910
	public static void ApplySettings(EventInstance ev, int count, UpdateObjectCountParameter.Settings settings)
	{
		float num = 0f;
		if (settings.maxObjects != settings.minObjects)
		{
			num = ((float)count - settings.minObjects) / (settings.maxObjects - settings.minObjects);
			num = Mathf.Clamp01(num);
		}
		if (settings.useExponentialCurve)
		{
			num *= num;
		}
		ev.setParameterByID(settings.parameterId, num, false);
	}

	// Token: 0x06001D9B RID: 7579 RVA: 0x000A076C File Offset: 0x0009E96C
	public UpdateObjectCountParameter()
		: base("objectCount")
	{
	}

	// Token: 0x06001D9C RID: 7580 RVA: 0x000A078C File Offset: 0x0009E98C
	public override void Add(LoopingSoundParameterUpdater.Sound sound)
	{
		UpdateObjectCountParameter.Settings settings = UpdateObjectCountParameter.GetSettings(sound.path, sound.description);
		UpdateObjectCountParameter.Entry entry = new UpdateObjectCountParameter.Entry
		{
			ev = sound.ev,
			settings = settings
		};
		this.entries.Add(entry);
	}

	// Token: 0x06001D9D RID: 7581 RVA: 0x000A07D8 File Offset: 0x0009E9D8
	public override void Update(float dt)
	{
		DictionaryPool<HashedString, int, LoopingSoundManager>.PooledDictionary pooledDictionary = DictionaryPool<HashedString, int, LoopingSoundManager>.Allocate();
		foreach (UpdateObjectCountParameter.Entry entry in this.entries)
		{
			int num = 0;
			pooledDictionary.TryGetValue(entry.settings.path, out num);
			num = (pooledDictionary[entry.settings.path] = num + 1);
		}
		foreach (UpdateObjectCountParameter.Entry entry2 in this.entries)
		{
			int num2 = pooledDictionary[entry2.settings.path];
			UpdateObjectCountParameter.ApplySettings(entry2.ev, num2, entry2.settings);
		}
		pooledDictionary.Recycle();
	}

	// Token: 0x06001D9E RID: 7582 RVA: 0x000A08C4 File Offset: 0x0009EAC4
	public override void Remove(LoopingSoundParameterUpdater.Sound sound)
	{
		for (int i = 0; i < this.entries.Count; i++)
		{
			if (this.entries[i].ev.handle == sound.ev.handle)
			{
				this.entries.RemoveAt(i);
				return;
			}
		}
	}

	// Token: 0x06001D9F RID: 7583 RVA: 0x000A091C File Offset: 0x0009EB1C
	public static void Clear()
	{
		UpdateObjectCountParameter.settings.Clear();
	}

	// Token: 0x04001141 RID: 4417
	private List<UpdateObjectCountParameter.Entry> entries = new List<UpdateObjectCountParameter.Entry>();

	// Token: 0x04001142 RID: 4418
	private static Dictionary<HashedString, UpdateObjectCountParameter.Settings> settings = new Dictionary<HashedString, UpdateObjectCountParameter.Settings>();

	// Token: 0x04001143 RID: 4419
	private static readonly HashedString parameterHash = "objectCount";

	// Token: 0x0200138C RID: 5004
	private struct Entry
	{
		// Token: 0x040069A5 RID: 27045
		public EventInstance ev;

		// Token: 0x040069A6 RID: 27046
		public UpdateObjectCountParameter.Settings settings;
	}

	// Token: 0x0200138D RID: 5005
	public struct Settings
	{
		// Token: 0x040069A7 RID: 27047
		public HashedString path;

		// Token: 0x040069A8 RID: 27048
		public PARAMETER_ID parameterId;

		// Token: 0x040069A9 RID: 27049
		public float minObjects;

		// Token: 0x040069AA RID: 27050
		public float maxObjects;

		// Token: 0x040069AB RID: 27051
		public bool useExponentialCurve;
	}
}
