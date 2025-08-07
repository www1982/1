using System;
using System.Collections.Generic;
using System.Diagnostics;
using STRINGS;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000FE2 RID: 4066
	[DebuggerDisplay("{base.Id}")]
	public abstract class Sickness : Resource
	{
		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x06007DA4 RID: 32164 RVA: 0x003246B9 File Offset: 0x003228B9
		public new string Name
		{
			get
			{
				return Strings.Get(this.name);
			}
		}

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x06007DA5 RID: 32165 RVA: 0x003246CB File Offset: 0x003228CB
		public float SicknessDuration
		{
			get
			{
				return this.sicknessDuration;
			}
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x06007DA6 RID: 32166 RVA: 0x003246D3 File Offset: 0x003228D3
		public StringKey DescriptiveSymptoms
		{
			get
			{
				return this.descriptiveSymptoms;
			}
		}

		// Token: 0x06007DA7 RID: 32167 RVA: 0x003246DC File Offset: 0x003228DC
		public Sickness(string id, Sickness.SicknessType type, Sickness.Severity severity, float immune_attack_strength, List<Sickness.InfectionVector> infection_vectors, float sickness_duration, string recovery_effect = null)
			: base(id, null, null)
		{
			this.name = new StringKey("STRINGS.DUPLICANTS.DISEASES." + id.ToUpper() + ".NAME");
			this.id = id;
			this.sicknessType = type;
			this.severity = severity;
			this.infectionVectors = infection_vectors;
			this.sicknessDuration = sickness_duration;
			this.recoveryEffect = recovery_effect;
			this.descriptiveSymptoms = new StringKey("STRINGS.DUPLICANTS.DISEASES." + id.ToUpper() + ".DESCRIPTIVE_SYMPTOMS");
			this.cureSpeedBase = new Attribute(id + "CureSpeed", false, Attribute.Display.Normal, false, 0f, null, null, null, null);
			this.cureSpeedBase.BaseValue = 1f;
			this.cureSpeedBase.SetFormatter(new ToPercentAttributeFormatter(1f, GameUtil.TimeSlice.None));
			Db.Get().Attributes.Add(this.cureSpeedBase);
		}

		// Token: 0x06007DA8 RID: 32168 RVA: 0x003247D8 File Offset: 0x003229D8
		public object[] Infect(GameObject go, SicknessInstance diseaseInstance, SicknessExposureInfo exposure_info)
		{
			object[] array = new object[this.components.Count];
			for (int i = 0; i < this.components.Count; i++)
			{
				array[i] = this.components[i].OnInfect(go, diseaseInstance);
			}
			return array;
		}

		// Token: 0x06007DA9 RID: 32169 RVA: 0x00324824 File Offset: 0x00322A24
		public void Cure(GameObject go, object[] componentData)
		{
			for (int i = 0; i < this.components.Count; i++)
			{
				this.components[i].OnCure(go, componentData[i]);
			}
		}

		// Token: 0x06007DAA RID: 32170 RVA: 0x0032485C File Offset: 0x00322A5C
		public List<Descriptor> GetSymptoms()
		{
			return this.GetSymptoms(null);
		}

		// Token: 0x06007DAB RID: 32171 RVA: 0x00324868 File Offset: 0x00322A68
		public List<Descriptor> GetSymptoms(GameObject victim)
		{
			List<Descriptor> list = new List<Descriptor>();
			for (int i = 0; i < this.components.Count; i++)
			{
				List<Descriptor> symptoms = this.components[i].GetSymptoms(victim);
				if (symptoms != null && symptoms.Count > 0)
				{
					list.AddRange(symptoms);
				}
			}
			if (this.fatalityDuration > 0f)
			{
				list.Add(new Descriptor(string.Format(DUPLICANTS.DISEASES.DEATH_SYMPTOM, GameUtil.GetFormattedCycles(this.fatalityDuration, "F1", false)), string.Format(DUPLICANTS.DISEASES.DEATH_SYMPTOM_TOOLTIP, GameUtil.GetFormattedCycles(this.fatalityDuration, "F1", false)), Descriptor.DescriptorType.SymptomAidable, false));
			}
			return list;
		}

		// Token: 0x06007DAC RID: 32172 RVA: 0x00324912 File Offset: 0x00322B12
		protected void AddSicknessComponent(Sickness.SicknessComponent cmp)
		{
			this.components.Add(cmp);
		}

		// Token: 0x06007DAD RID: 32173 RVA: 0x00324920 File Offset: 0x00322B20
		public T GetSicknessComponent<T>() where T : Sickness.SicknessComponent
		{
			for (int i = 0; i < this.components.Count; i++)
			{
				if (this.components[i] is T)
				{
					return this.components[i] as T;
				}
			}
			return default(T);
		}

		// Token: 0x06007DAE RID: 32174 RVA: 0x00324976 File Offset: 0x00322B76
		public virtual List<Descriptor> GetSicknessSourceDescriptors()
		{
			return new List<Descriptor>();
		}

		// Token: 0x06007DAF RID: 32175 RVA: 0x00324980 File Offset: 0x00322B80
		public List<Descriptor> GetQualitativeDescriptors()
		{
			List<Descriptor> list = new List<Descriptor>();
			using (List<Sickness.InfectionVector>.Enumerator enumerator = this.infectionVectors.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					switch (enumerator.Current)
					{
					case Sickness.InfectionVector.Contact:
						list.Add(new Descriptor(DUPLICANTS.DISEASES.DESCRIPTORS.INFO.SKINBORNE, DUPLICANTS.DISEASES.DESCRIPTORS.INFO.SKINBORNE_TOOLTIP, Descriptor.DescriptorType.Information, false));
						break;
					case Sickness.InfectionVector.Digestion:
						list.Add(new Descriptor(DUPLICANTS.DISEASES.DESCRIPTORS.INFO.FOODBORNE, DUPLICANTS.DISEASES.DESCRIPTORS.INFO.FOODBORNE_TOOLTIP, Descriptor.DescriptorType.Information, false));
						break;
					case Sickness.InfectionVector.Inhalation:
						list.Add(new Descriptor(DUPLICANTS.DISEASES.DESCRIPTORS.INFO.AIRBORNE, DUPLICANTS.DISEASES.DESCRIPTORS.INFO.AIRBORNE_TOOLTIP, Descriptor.DescriptorType.Information, false));
						break;
					case Sickness.InfectionVector.Exposure:
						list.Add(new Descriptor(DUPLICANTS.DISEASES.DESCRIPTORS.INFO.SUNBORNE, DUPLICANTS.DISEASES.DESCRIPTORS.INFO.SUNBORNE_TOOLTIP, Descriptor.DescriptorType.Information, false));
						break;
					}
				}
			}
			list.Add(new Descriptor(Strings.Get(this.descriptiveSymptoms), "", Descriptor.DescriptorType.Information, false));
			return list;
		}

		// Token: 0x04005ECF RID: 24271
		private StringKey name;

		// Token: 0x04005ED0 RID: 24272
		private StringKey descriptiveSymptoms;

		// Token: 0x04005ED1 RID: 24273
		private float sicknessDuration = 600f;

		// Token: 0x04005ED2 RID: 24274
		public float fatalityDuration;

		// Token: 0x04005ED3 RID: 24275
		public HashedString id;

		// Token: 0x04005ED4 RID: 24276
		public Sickness.SicknessType sicknessType;

		// Token: 0x04005ED5 RID: 24277
		public Sickness.Severity severity;

		// Token: 0x04005ED6 RID: 24278
		public string recoveryEffect;

		// Token: 0x04005ED7 RID: 24279
		public List<Sickness.InfectionVector> infectionVectors;

		// Token: 0x04005ED8 RID: 24280
		private List<Sickness.SicknessComponent> components = new List<Sickness.SicknessComponent>();

		// Token: 0x04005ED9 RID: 24281
		public Amount amount;

		// Token: 0x04005EDA RID: 24282
		public Attribute amountDeltaAttribute;

		// Token: 0x04005EDB RID: 24283
		public Attribute cureSpeedBase;

		// Token: 0x020025C1 RID: 9665
		public abstract class SicknessComponent
		{
			// Token: 0x0600C180 RID: 49536
			public abstract object OnInfect(GameObject go, SicknessInstance diseaseInstance);

			// Token: 0x0600C181 RID: 49537
			public abstract void OnCure(GameObject go, object instance_data);

			// Token: 0x0600C182 RID: 49538 RVA: 0x004068EF File Offset: 0x00404AEF
			public virtual List<Descriptor> GetSymptoms()
			{
				return null;
			}

			// Token: 0x0600C183 RID: 49539 RVA: 0x004068F2 File Offset: 0x00404AF2
			public virtual List<Descriptor> GetSymptoms(GameObject victim)
			{
				return this.GetSymptoms();
			}
		}

		// Token: 0x020025C2 RID: 9666
		public enum InfectionVector
		{
			// Token: 0x0400A8B2 RID: 43186
			Contact,
			// Token: 0x0400A8B3 RID: 43187
			Digestion,
			// Token: 0x0400A8B4 RID: 43188
			Inhalation,
			// Token: 0x0400A8B5 RID: 43189
			Exposure
		}

		// Token: 0x020025C3 RID: 9667
		public enum SicknessType
		{
			// Token: 0x0400A8B7 RID: 43191
			Pathogen,
			// Token: 0x0400A8B8 RID: 43192
			Ailment,
			// Token: 0x0400A8B9 RID: 43193
			Injury
		}

		// Token: 0x020025C4 RID: 9668
		public enum Severity
		{
			// Token: 0x0400A8BB RID: 43195
			Benign,
			// Token: 0x0400A8BC RID: 43196
			Minor,
			// Token: 0x0400A8BD RID: 43197
			Major,
			// Token: 0x0400A8BE RID: 43198
			Critical
		}
	}
}
