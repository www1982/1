using System;
using System.Collections.Generic;
using Database;
using Klei.AI;
using UnityEngine;

// Token: 0x02000B0B RID: 2827
[AddComponentMenu("KMonoBehaviour/scripts/SicknessTrigger")]
public class SicknessTrigger : KMonoBehaviour, IGameObjectEffectDescriptor
{
	// Token: 0x06005301 RID: 21249 RVA: 0x001E32C8 File Offset: 0x001E14C8
	public void AddTrigger(GameHashes src_event, string[] sickness_ids, SicknessTrigger.SourceCallback source_callback)
	{
		this.triggers.Add(new SicknessTrigger.TriggerInfo
		{
			srcEvent = src_event,
			sickness_ids = sickness_ids,
			sourceCallback = source_callback
		});
	}

	// Token: 0x06005302 RID: 21250 RVA: 0x001E3304 File Offset: 0x001E1504
	protected override void OnSpawn()
	{
		for (int i = 0; i < this.triggers.Count; i++)
		{
			SicknessTrigger.TriggerInfo trigger = this.triggers[i];
			base.Subscribe((int)trigger.srcEvent, delegate(object data)
			{
				this.OnSicknessTrigger((GameObject)data, trigger);
			});
		}
	}

	// Token: 0x06005303 RID: 21251 RVA: 0x001E3364 File Offset: 0x001E1564
	private void OnSicknessTrigger(GameObject target, SicknessTrigger.TriggerInfo trigger)
	{
		int num = global::UnityEngine.Random.Range(0, trigger.sickness_ids.Length);
		string text = trigger.sickness_ids[num];
		Sickness sickness = null;
		global::Database.Sicknesses sicknesses = Db.Get().Sicknesses;
		for (int i = 0; i < sicknesses.Count; i++)
		{
			if (sicknesses[i].Id == text)
			{
				sickness = sicknesses[i];
				break;
			}
		}
		if (sickness != null)
		{
			string text2 = trigger.sourceCallback(base.gameObject, target);
			SicknessExposureInfo sicknessExposureInfo = new SicknessExposureInfo(sickness.Id, text2);
			target.GetComponent<MinionModifiers>().sicknesses.Infect(sicknessExposureInfo);
			return;
		}
		DebugUtil.DevLogErrorFormat(base.gameObject, "Couldn't find sickness with id [{0}]", new object[] { text });
	}

	// Token: 0x06005304 RID: 21252 RVA: 0x001E3420 File Offset: 0x001E1620
	public List<Descriptor> EffectDescriptors(GameObject go)
	{
		Dictionary<GameHashes, HashSet<string>> dictionary = new Dictionary<GameHashes, HashSet<string>>();
		foreach (SicknessTrigger.TriggerInfo triggerInfo in this.triggers)
		{
			HashSet<string> hashSet = null;
			if (!dictionary.TryGetValue(triggerInfo.srcEvent, out hashSet))
			{
				hashSet = new HashSet<string>();
				dictionary[triggerInfo.srcEvent] = hashSet;
			}
			foreach (string text in triggerInfo.sickness_ids)
			{
				hashSet.Add(text);
			}
		}
		List<Descriptor> list = new List<Descriptor>();
		List<string> list2 = new List<string>();
		string properName = base.GetComponent<KSelectable>().GetProperName();
		foreach (KeyValuePair<GameHashes, HashSet<string>> keyValuePair in dictionary)
		{
			HashSet<string> value = keyValuePair.Value;
			list2.Clear();
			foreach (string text2 in value)
			{
				Sickness sickness = Db.Get().Sicknesses.TryGet(text2);
				list2.Add(sickness.Name);
			}
			string text3 = string.Join(", ", list2.ToArray());
			string text4 = Strings.Get("STRINGS.DUPLICANTS.DISEASES.TRIGGERS." + Enum.GetName(typeof(GameHashes), keyValuePair.Key).ToUpper()).String;
			string text5 = Strings.Get("STRINGS.DUPLICANTS.DISEASES.TRIGGERS.TOOLTIPS." + Enum.GetName(typeof(GameHashes), keyValuePair.Key).ToUpper()).String;
			text4 = text4.Replace("{ItemName}", properName).Replace("{Diseases}", text3);
			text5 = text5.Replace("{ItemName}", properName).Replace("{Diseases}", text3);
			list.Add(new Descriptor(text4, text5, Descriptor.DescriptorType.Effect, false));
		}
		return list;
	}

	// Token: 0x06005305 RID: 21253 RVA: 0x001E3670 File Offset: 0x001E1870
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		return this.EffectDescriptors(go);
	}

	// Token: 0x040037D1 RID: 14289
	public List<SicknessTrigger.TriggerInfo> triggers = new List<SicknessTrigger.TriggerInfo>();

	// Token: 0x02001C0B RID: 7179
	// (Invoke) Token: 0x0600A97E RID: 43390
	public delegate string SourceCallback(GameObject source, GameObject target);

	// Token: 0x02001C0C RID: 7180
	[Serializable]
	public struct TriggerInfo
	{
		// Token: 0x040084E2 RID: 34018
		[HashedEnum]
		public GameHashes srcEvent;

		// Token: 0x040084E3 RID: 34019
		public string[] sickness_ids;

		// Token: 0x040084E4 RID: 34020
		public SicknessTrigger.SourceCallback sourceCallback;
	}
}
