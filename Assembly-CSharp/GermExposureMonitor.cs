using System;
using System.Collections.Generic;
using Klei.AI;
using KSerialization;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020009F1 RID: 2545
public class GermExposureMonitor : GameStateMachine<GermExposureMonitor, GermExposureMonitor.Instance>
{
	// Token: 0x06004A4B RID: 19019 RVA: 0x001AE908 File Offset: 0x001ACB08
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		base.serializable = StateMachine.SerializeType.Never;
		this.root.Update(delegate(GermExposureMonitor.Instance smi, float dt)
		{
			smi.OnInhaleExposureTick(dt);
		}, UpdateRate.SIM_1000ms, true).EventHandler(GameHashes.EatCompleteEater, delegate(GermExposureMonitor.Instance smi, object obj)
		{
			smi.OnEatComplete(obj);
		}).EventHandler(GameHashes.SicknessAdded, delegate(GermExposureMonitor.Instance smi, object data)
		{
			smi.OnSicknessAdded(data);
		})
			.EventHandler(GameHashes.SicknessCured, delegate(GermExposureMonitor.Instance smi, object data)
			{
				smi.OnSicknessCured(data);
			})
			.EventHandler(GameHashes.SleepFinished, delegate(GermExposureMonitor.Instance smi)
			{
				smi.OnSleepFinished();
			});
	}

	// Token: 0x06004A4C RID: 19020 RVA: 0x001AE9F5 File Offset: 0x001ACBF5
	public static float GetContractionChance(float rating)
	{
		return 0.5f - 0.5f * (float)Math.Tanh(0.25 * (double)rating);
	}

	// Token: 0x02001A54 RID: 6740
	public enum ExposureState
	{
		// Token: 0x04007F66 RID: 32614
		None,
		// Token: 0x04007F67 RID: 32615
		Contact,
		// Token: 0x04007F68 RID: 32616
		Exposed,
		// Token: 0x04007F69 RID: 32617
		Contracted,
		// Token: 0x04007F6A RID: 32618
		Sick
	}

	// Token: 0x02001A55 RID: 6741
	public class ExposureStatusData
	{
		// Token: 0x04007F6B RID: 32619
		public ExposureType exposure_type;

		// Token: 0x04007F6C RID: 32620
		public GermExposureMonitor.Instance owner;
	}

	// Token: 0x02001A56 RID: 6742
	[SerializationConfig(MemberSerialization.OptIn)]
	public new class Instance : GameStateMachine<GermExposureMonitor, GermExposureMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x0600A30D RID: 41741 RVA: 0x003A2814 File Offset: 0x003A0A14
		public Instance(IStateMachineTarget master)
			: base(master)
		{
			this.sicknesses = master.GetComponent<MinionModifiers>().sicknesses;
			this.primaryElement = master.GetComponent<PrimaryElement>();
			this.traits = master.GetComponent<Traits>();
			this.lastDiseaseSources = new Dictionary<HashedString, GermExposureMonitor.Instance.DiseaseSourceInfo>();
			this.lastExposureTime = new Dictionary<HashedString, float>();
			this.inhaleExposureTick = new Dictionary<HashedString, GermExposureMonitor.Instance.InhaleTickInfo>();
			GameClock.Instance.Subscribe(-722330267, new Action<object>(this.OnNightTime));
			base.gameObject.Subscribe(-1582839653, new Action<object>(this.OnBionicTagsChanged));
			this.inateImmunities = DUPLICANTSTATS.GetStatsFor(base.gameObject).DiseaseImmunities.IMMUNITIES;
			OxygenBreather component = base.GetComponent<OxygenBreather>();
			if (component != null)
			{
				OxygenBreather oxygenBreather = component;
				oxygenBreather.onBreathableGasConsumed = (Action<SimHashes, float, float, byte, int>)Delegate.Combine(oxygenBreather.onBreathableGasConsumed, new Action<SimHashes, float, float, byte, int>(this.OnAirConsumed));
			}
		}

		// Token: 0x0600A30E RID: 41742 RVA: 0x003A2924 File Offset: 0x003A0B24
		protected override void OnCleanUp()
		{
			base.gameObject.Unsubscribe(-1582839653, new Action<object>(this.OnBionicTagsChanged));
			base.OnCleanUp();
		}

		// Token: 0x0600A30F RID: 41743 RVA: 0x003A2948 File Offset: 0x003A0B48
		public override void StartSM()
		{
			base.StartSM();
			this.RefreshStatusItems();
		}

		// Token: 0x0600A310 RID: 41744 RVA: 0x003A2958 File Offset: 0x003A0B58
		public override void StopSM(string reason)
		{
			GameClock.Instance.Unsubscribe(-722330267, new Action<object>(this.OnNightTime));
			foreach (ExposureType exposureType in GERM_EXPOSURE.TYPES)
			{
				Guid guid;
				this.statusItemHandles.TryGetValue(exposureType.germ_id, out guid);
				guid = base.GetComponent<KSelectable>().RemoveStatusItem(guid, false);
			}
			base.StopSM(reason);
		}

		// Token: 0x0600A311 RID: 41745 RVA: 0x003A29C4 File Offset: 0x003A0BC4
		public void OnEatComplete(object obj)
		{
			Edible edible = (Edible)obj;
			HandleVector<int>.Handle handle = GameComps.DiseaseContainers.GetHandle(edible.gameObject);
			if (handle != HandleVector<int>.InvalidHandle)
			{
				DiseaseHeader header = GameComps.DiseaseContainers.GetHeader(handle);
				if (header.diseaseIdx != 255)
				{
					Disease disease = Db.Get().Diseases[(int)header.diseaseIdx];
					float num = edible.unitsConsumed / (edible.unitsConsumed + edible.Units);
					int num2 = Mathf.CeilToInt((float)header.diseaseCount * num);
					GameComps.DiseaseContainers.ModifyDiseaseCount(handle, -num2);
					KPrefabID component = edible.GetComponent<KPrefabID>();
					this.InjectDisease(disease, num2, component.PrefabID(), Sickness.InfectionVector.Digestion);
				}
			}
		}

		// Token: 0x0600A312 RID: 41746 RVA: 0x003A2A74 File Offset: 0x003A0C74
		public void OnAirConsumed(SimHashes elementConsumed, float massConsumed, float temperature, byte disseaseIDX, int disseaseCount)
		{
			if (disseaseIDX != 255)
			{
				Disease disease = Db.Get().Diseases[(int)disseaseIDX];
				this.InjectDisease(disease, disseaseCount, ElementLoader.FindElementByHash(elementConsumed).tag, Sickness.InfectionVector.Inhalation);
			}
		}

		// Token: 0x0600A313 RID: 41747 RVA: 0x003A2AB4 File Offset: 0x003A0CB4
		public void OnInhaleExposureTick(float dt)
		{
			foreach (KeyValuePair<HashedString, GermExposureMonitor.Instance.InhaleTickInfo> keyValuePair in this.inhaleExposureTick)
			{
				if (keyValuePair.Value.inhaled)
				{
					keyValuePair.Value.inhaled = false;
					keyValuePair.Value.ticks++;
				}
				else
				{
					keyValuePair.Value.ticks = Mathf.Max(0, keyValuePair.Value.ticks - 1);
				}
			}
		}

		// Token: 0x0600A314 RID: 41748 RVA: 0x003A2B54 File Offset: 0x003A0D54
		public void TryInjectDisease(byte disease_idx, int count, Tag source, Sickness.InfectionVector vector)
		{
			if (disease_idx != 255)
			{
				Disease disease = Db.Get().Diseases[(int)disease_idx];
				this.InjectDisease(disease, count, source, vector);
			}
		}

		// Token: 0x0600A315 RID: 41749 RVA: 0x003A2B85 File Offset: 0x003A0D85
		public float GetGermResistance()
		{
			return Db.Get().Attributes.GermResistance.Lookup(base.gameObject).GetTotalValue();
		}

		// Token: 0x0600A316 RID: 41750 RVA: 0x003A2BA8 File Offset: 0x003A0DA8
		public float GetResistanceToExposureType(ExposureType exposureType, float overrideExposureTier = -1f)
		{
			float num = overrideExposureTier;
			if (num == -1f)
			{
				num = this.GetExposureTier(exposureType.germ_id);
			}
			num = Mathf.Clamp(num, 1f, 3f);
			float num2 = GERM_EXPOSURE.EXPOSURE_TIER_RESISTANCE_BONUSES[(int)num - 1];
			float totalValue = Db.Get().Attributes.GermResistance.Lookup(base.gameObject).GetTotalValue();
			return (float)exposureType.base_resistance + totalValue + num2;
		}

		// Token: 0x0600A317 RID: 41751 RVA: 0x003A2C14 File Offset: 0x003A0E14
		public int AssessDigestedGerms(ExposureType exposure_type, int count)
		{
			int exposure_threshold = exposure_type.exposure_threshold;
			int num = count / exposure_threshold;
			return MathUtil.Clamp(1, 3, num);
		}

		// Token: 0x0600A318 RID: 41752 RVA: 0x003A2C34 File Offset: 0x003A0E34
		public bool AssessInhaledGerms(ExposureType exposure_type)
		{
			GermExposureMonitor.Instance.InhaleTickInfo inhaleTickInfo;
			this.inhaleExposureTick.TryGetValue(exposure_type.germ_id, out inhaleTickInfo);
			if (inhaleTickInfo == null)
			{
				inhaleTickInfo = new GermExposureMonitor.Instance.InhaleTickInfo();
				this.inhaleExposureTick[exposure_type.germ_id] = inhaleTickInfo;
			}
			if (!inhaleTickInfo.inhaled)
			{
				float exposureTier = this.GetExposureTier(exposure_type.germ_id);
				inhaleTickInfo.inhaled = true;
				return inhaleTickInfo.ticks >= GERM_EXPOSURE.INHALE_TICK_THRESHOLD[(int)exposureTier];
			}
			return false;
		}

		// Token: 0x0600A319 RID: 41753 RVA: 0x003A2CAC File Offset: 0x003A0EAC
		public bool IsImmuneToDisease(string sicknessID)
		{
			if (this.inateImmunities == null)
			{
				return false;
			}
			for (int i = 0; i < this.inateImmunities.Length; i++)
			{
				if (sicknessID == this.inateImmunities[i])
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600A31A RID: 41754 RVA: 0x003A2CEC File Offset: 0x003A0EEC
		public void InjectDisease(Disease disease, int count, Tag source, Sickness.InfectionVector vector)
		{
			foreach (ExposureType exposureType in GERM_EXPOSURE.TYPES)
			{
				if (disease.id == exposureType.germ_id && !this.IsImmuneToDisease(exposureType.sickness_id) && count > exposureType.exposure_threshold && this.HasMinExposurePeriodElapsed(exposureType.germ_id) && this.IsExposureValidForTraits(exposureType))
				{
					Sickness sickness = ((exposureType.sickness_id != null) ? Db.Get().Sicknesses.Get(exposureType.sickness_id) : null);
					if (sickness == null || sickness.infectionVectors.Contains(vector))
					{
						GermExposureMonitor.ExposureState exposureState = this.GetExposureState(exposureType.germ_id);
						float exposureTier = this.GetExposureTier(exposureType.germ_id);
						if (exposureState == GermExposureMonitor.ExposureState.None || exposureState == GermExposureMonitor.ExposureState.Contact)
						{
							float contractionChance = GermExposureMonitor.GetContractionChance(this.GetResistanceToExposureType(exposureType, -1f));
							this.SetExposureState(exposureType.germ_id, GermExposureMonitor.ExposureState.Contact);
							if (contractionChance > 0f)
							{
								this.lastDiseaseSources[disease.id] = new GermExposureMonitor.Instance.DiseaseSourceInfo(source, vector, contractionChance, base.transform.GetPosition());
								if (exposureType.infect_immediately)
								{
									this.InfectImmediately(exposureType);
								}
								else
								{
									bool flag = true;
									bool flag2 = vector == Sickness.InfectionVector.Inhalation;
									bool flag3 = vector == Sickness.InfectionVector.Digestion;
									int num = 1;
									if (flag2)
									{
										flag = this.AssessInhaledGerms(exposureType);
									}
									if (flag3)
									{
										num = this.AssessDigestedGerms(exposureType, count);
									}
									if (flag)
									{
										if (flag2)
										{
											this.inhaleExposureTick[exposureType.germ_id].ticks = 0;
										}
										this.SetExposureState(exposureType.germ_id, GermExposureMonitor.ExposureState.Exposed);
										this.SetExposureTier(exposureType.germ_id, (float)num);
										float num2 = Mathf.Clamp01(contractionChance);
										GermExposureTracker.Instance.AddExposure(exposureType, num2);
									}
								}
							}
						}
						else if (exposureState == GermExposureMonitor.ExposureState.Exposed && exposureTier < 3f)
						{
							float contractionChance2 = GermExposureMonitor.GetContractionChance(this.GetResistanceToExposureType(exposureType, -1f));
							if (contractionChance2 > 0f)
							{
								this.lastDiseaseSources[disease.id] = new GermExposureMonitor.Instance.DiseaseSourceInfo(source, vector, contractionChance2, base.transform.GetPosition());
								if (!exposureType.infect_immediately)
								{
									bool flag4 = true;
									bool flag5 = vector == Sickness.InfectionVector.Inhalation;
									bool flag6 = vector == Sickness.InfectionVector.Digestion;
									int num3 = 1;
									if (flag5)
									{
										flag4 = this.AssessInhaledGerms(exposureType);
									}
									if (flag6)
									{
										num3 = this.AssessDigestedGerms(exposureType, count);
									}
									if (flag4)
									{
										if (flag5)
										{
											this.inhaleExposureTick[exposureType.germ_id].ticks = 0;
										}
										this.SetExposureTier(exposureType.germ_id, this.GetExposureTier(exposureType.germ_id) + (float)num3);
										float num4 = Mathf.Clamp01(GermExposureMonitor.GetContractionChance(this.GetResistanceToExposureType(exposureType, -1f)) - contractionChance2);
										GermExposureTracker.Instance.AddExposure(exposureType, num4);
									}
								}
							}
						}
					}
				}
			}
			this.RefreshStatusItems();
		}

		// Token: 0x0600A31B RID: 41755 RVA: 0x003A2FC0 File Offset: 0x003A11C0
		public GermExposureMonitor.ExposureState GetExposureState(string germ_id)
		{
			GermExposureMonitor.ExposureState exposureState;
			this.exposureStates.TryGetValue(germ_id, out exposureState);
			return exposureState;
		}

		// Token: 0x0600A31C RID: 41756 RVA: 0x003A2FE0 File Offset: 0x003A11E0
		public float GetExposureTier(string germ_id)
		{
			float num = 1f;
			this.exposureTiers.TryGetValue(germ_id, out num);
			return Mathf.Clamp(num, 1f, 3f);
		}

		// Token: 0x0600A31D RID: 41757 RVA: 0x003A3012 File Offset: 0x003A1212
		public void SetExposureState(string germ_id, GermExposureMonitor.ExposureState exposure_state)
		{
			this.exposureStates[germ_id] = exposure_state;
			this.RefreshStatusItems();
		}

		// Token: 0x0600A31E RID: 41758 RVA: 0x003A3027 File Offset: 0x003A1227
		public void SetExposureTier(string germ_id, float tier)
		{
			tier = Mathf.Clamp(tier, 0f, 3f);
			this.exposureTiers[germ_id] = tier;
			this.RefreshStatusItems();
		}

		// Token: 0x0600A31F RID: 41759 RVA: 0x003A304E File Offset: 0x003A124E
		public void ContractGerms(string germ_id)
		{
			DebugUtil.DevAssert(this.GetExposureState(germ_id) == GermExposureMonitor.ExposureState.Exposed, "Duplicant is contracting a sickness but was never exposed to it!", null);
			this.SetExposureState(germ_id, GermExposureMonitor.ExposureState.Contracted);
		}

		// Token: 0x0600A320 RID: 41760 RVA: 0x003A3070 File Offset: 0x003A1270
		public void OnSicknessAdded(object sickness_instance_data)
		{
			SicknessInstance sicknessInstance = (SicknessInstance)sickness_instance_data;
			foreach (ExposureType exposureType in GERM_EXPOSURE.TYPES)
			{
				if (exposureType.sickness_id == sicknessInstance.Sickness.Id)
				{
					this.SetExposureState(exposureType.germ_id, GermExposureMonitor.ExposureState.Sick);
				}
			}
		}

		// Token: 0x0600A321 RID: 41761 RVA: 0x003A30C4 File Offset: 0x003A12C4
		public void OnSicknessCured(object sickness_instance_data)
		{
			SicknessInstance sicknessInstance = (SicknessInstance)sickness_instance_data;
			foreach (ExposureType exposureType in GERM_EXPOSURE.TYPES)
			{
				if (exposureType.sickness_id == sicknessInstance.Sickness.Id)
				{
					this.SetExposureState(exposureType.germ_id, GermExposureMonitor.ExposureState.None);
				}
			}
		}

		// Token: 0x0600A322 RID: 41762 RVA: 0x003A3118 File Offset: 0x003A1318
		private bool IsExposureValidForTraits(ExposureType exposure_type)
		{
			if (exposure_type.required_traits != null && exposure_type.required_traits.Count > 0)
			{
				foreach (string text in exposure_type.required_traits)
				{
					if (!this.traits.HasTrait(text))
					{
						return false;
					}
				}
			}
			if (exposure_type.excluded_traits != null && exposure_type.excluded_traits.Count > 0)
			{
				foreach (string text2 in exposure_type.excluded_traits)
				{
					if (this.traits.HasTrait(text2))
					{
						return false;
					}
				}
			}
			if (exposure_type.excluded_effects != null && exposure_type.excluded_effects.Count > 0)
			{
				Effects component = base.master.GetComponent<Effects>();
				foreach (string text3 in exposure_type.excluded_effects)
				{
					if (component.HasEffect(text3))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600A323 RID: 41763 RVA: 0x003A3264 File Offset: 0x003A1464
		private bool HasMinExposurePeriodElapsed(string germ_id)
		{
			float num;
			this.lastExposureTime.TryGetValue(germ_id, out num);
			return num == 0f || GameClock.Instance.GetTime() - num > 540f;
		}

		// Token: 0x0600A324 RID: 41764 RVA: 0x003A32A4 File Offset: 0x003A14A4
		private void RefreshStatusItems()
		{
			foreach (ExposureType exposureType in GERM_EXPOSURE.TYPES)
			{
				Guid guid;
				this.contactStatusItemHandles.TryGetValue(exposureType.germ_id, out guid);
				Guid guid2;
				this.statusItemHandles.TryGetValue(exposureType.germ_id, out guid2);
				GermExposureMonitor.ExposureState exposureState = this.GetExposureState(exposureType.germ_id);
				if (guid2 == Guid.Empty && (exposureState == GermExposureMonitor.ExposureState.Exposed || exposureState == GermExposureMonitor.ExposureState.Contracted) && !string.IsNullOrEmpty(exposureType.sickness_id))
				{
					guid2 = base.GetComponent<KSelectable>().AddStatusItem(Db.Get().DuplicantStatusItems.ExposedToGerms, new GermExposureMonitor.ExposureStatusData
					{
						exposure_type = exposureType,
						owner = this
					});
				}
				else if (guid2 != Guid.Empty && exposureState != GermExposureMonitor.ExposureState.Exposed && exposureState != GermExposureMonitor.ExposureState.Contracted)
				{
					guid2 = base.GetComponent<KSelectable>().RemoveStatusItem(guid2, false);
				}
				this.statusItemHandles[exposureType.germ_id] = guid2;
				if (guid == Guid.Empty && exposureState == GermExposureMonitor.ExposureState.Contact)
				{
					if (!string.IsNullOrEmpty(exposureType.sickness_id))
					{
						guid = base.GetComponent<KSelectable>().AddStatusItem(Db.Get().DuplicantStatusItems.ContactWithGerms, new GermExposureMonitor.ExposureStatusData
						{
							exposure_type = exposureType,
							owner = this
						});
					}
				}
				else if (guid != Guid.Empty && exposureState != GermExposureMonitor.ExposureState.Contact)
				{
					guid = base.GetComponent<KSelectable>().RemoveStatusItem(guid, false);
				}
				this.contactStatusItemHandles[exposureType.germ_id] = guid;
			}
		}

		// Token: 0x0600A325 RID: 41765 RVA: 0x003A3417 File Offset: 0x003A1617
		private void OnNightTime(object data)
		{
			this.UpdateReports();
		}

		// Token: 0x0600A326 RID: 41766 RVA: 0x003A3420 File Offset: 0x003A1620
		private void UpdateReports()
		{
			ReportManager.Instance.ReportValue(ReportManager.ReportType.DiseaseStatus, (float)this.primaryElement.DiseaseCount, StringFormatter.Replace(UI.ENDOFDAYREPORT.NOTES.GERMS, "{0}", base.master.name), base.master.gameObject.GetProperName());
		}

		// Token: 0x0600A327 RID: 41767 RVA: 0x003A3474 File Offset: 0x003A1674
		public void InfectImmediately(ExposureType exposure_type)
		{
			if (exposure_type.infection_effect != null)
			{
				base.master.GetComponent<Effects>().Add(exposure_type.infection_effect, true);
			}
			if (exposure_type.sickness_id != null)
			{
				string lastDiseaseSource = this.GetLastDiseaseSource(exposure_type.germ_id);
				SicknessExposureInfo sicknessExposureInfo = new SicknessExposureInfo(exposure_type.sickness_id, lastDiseaseSource);
				this.sicknesses.Infect(sicknessExposureInfo);
			}
		}

		// Token: 0x0600A328 RID: 41768 RVA: 0x003A34D0 File Offset: 0x003A16D0
		private void OnBionicTagsChanged(object o)
		{
			if (o == null)
			{
				return;
			}
			TagChangedEventData tagChangedEventData = (TagChangedEventData)o;
			if (tagChangedEventData.tag == GameTags.BionicBedTime && !tagChangedEventData.added)
			{
				this.OnSleepFinished();
			}
		}

		// Token: 0x0600A329 RID: 41769 RVA: 0x003A3508 File Offset: 0x003A1708
		public void OnSleepFinished()
		{
			foreach (ExposureType exposureType in GERM_EXPOSURE.TYPES)
			{
				if (!exposureType.infect_immediately && exposureType.sickness_id != null)
				{
					GermExposureMonitor.ExposureState exposureState = this.GetExposureState(exposureType.germ_id);
					if (exposureState == GermExposureMonitor.ExposureState.Exposed)
					{
						this.SetExposureState(exposureType.germ_id, GermExposureMonitor.ExposureState.None);
					}
					if (exposureState == GermExposureMonitor.ExposureState.Contracted)
					{
						this.SetExposureState(exposureType.germ_id, GermExposureMonitor.ExposureState.Sick);
						string lastDiseaseSource = this.GetLastDiseaseSource(exposureType.germ_id);
						SicknessExposureInfo sicknessExposureInfo = new SicknessExposureInfo(exposureType.sickness_id, lastDiseaseSource);
						this.sicknesses.Infect(sicknessExposureInfo);
					}
					this.SetExposureTier(exposureType.germ_id, 0f);
				}
			}
		}

		// Token: 0x0600A32A RID: 41770 RVA: 0x003A35A8 File Offset: 0x003A17A8
		public string GetLastDiseaseSource(string id)
		{
			GermExposureMonitor.Instance.DiseaseSourceInfo diseaseSourceInfo;
			string text;
			if (this.lastDiseaseSources.TryGetValue(id, out diseaseSourceInfo))
			{
				switch (diseaseSourceInfo.vector)
				{
				case Sickness.InfectionVector.Contact:
					text = DUPLICANTS.DISEASES.INFECTIONSOURCES.SKIN;
					break;
				case Sickness.InfectionVector.Digestion:
					text = string.Format(DUPLICANTS.DISEASES.INFECTIONSOURCES.FOOD, diseaseSourceInfo.sourceObject.ProperName());
					break;
				case Sickness.InfectionVector.Inhalation:
					text = string.Format(DUPLICANTS.DISEASES.INFECTIONSOURCES.AIR, diseaseSourceInfo.sourceObject.ProperName());
					break;
				default:
					text = DUPLICANTS.DISEASES.INFECTIONSOURCES.UNKNOWN;
					break;
				}
			}
			else
			{
				text = DUPLICANTS.DISEASES.INFECTIONSOURCES.UNKNOWN;
			}
			return text;
		}

		// Token: 0x0600A32B RID: 41771 RVA: 0x003A3644 File Offset: 0x003A1844
		public Vector3 GetLastExposurePosition(string germ_id)
		{
			GermExposureMonitor.Instance.DiseaseSourceInfo diseaseSourceInfo;
			if (this.lastDiseaseSources.TryGetValue(germ_id, out diseaseSourceInfo))
			{
				return diseaseSourceInfo.position;
			}
			return base.transform.GetPosition();
		}

		// Token: 0x0600A32C RID: 41772 RVA: 0x003A3678 File Offset: 0x003A1878
		public float GetExposureWeight(string id)
		{
			float exposureTier = this.GetExposureTier(id);
			GermExposureMonitor.Instance.DiseaseSourceInfo diseaseSourceInfo;
			if (this.lastDiseaseSources.TryGetValue(id, out diseaseSourceInfo))
			{
				return diseaseSourceInfo.factor * exposureTier;
			}
			return 0f;
		}

		// Token: 0x04007F6D RID: 32621
		[Serialize]
		public Dictionary<HashedString, GermExposureMonitor.Instance.DiseaseSourceInfo> lastDiseaseSources;

		// Token: 0x04007F6E RID: 32622
		[Serialize]
		public Dictionary<HashedString, float> lastExposureTime;

		// Token: 0x04007F6F RID: 32623
		private Dictionary<HashedString, GermExposureMonitor.Instance.InhaleTickInfo> inhaleExposureTick;

		// Token: 0x04007F70 RID: 32624
		private string[] inateImmunities;

		// Token: 0x04007F71 RID: 32625
		private Sicknesses sicknesses;

		// Token: 0x04007F72 RID: 32626
		private PrimaryElement primaryElement;

		// Token: 0x04007F73 RID: 32627
		private Traits traits;

		// Token: 0x04007F74 RID: 32628
		[Serialize]
		private Dictionary<string, GermExposureMonitor.ExposureState> exposureStates = new Dictionary<string, GermExposureMonitor.ExposureState>();

		// Token: 0x04007F75 RID: 32629
		[Serialize]
		private Dictionary<string, float> exposureTiers = new Dictionary<string, float>();

		// Token: 0x04007F76 RID: 32630
		private Dictionary<string, Guid> statusItemHandles = new Dictionary<string, Guid>();

		// Token: 0x04007F77 RID: 32631
		private Dictionary<string, Guid> contactStatusItemHandles = new Dictionary<string, Guid>();

		// Token: 0x02002867 RID: 10343
		[Serializable]
		public class DiseaseSourceInfo
		{
			// Token: 0x0600CBE9 RID: 52201 RVA: 0x0041A7BE File Offset: 0x004189BE
			public DiseaseSourceInfo(Tag sourceObject, Sickness.InfectionVector vector, float factor, Vector3 position)
			{
				this.sourceObject = sourceObject;
				this.vector = vector;
				this.factor = factor;
				this.position = position;
			}

			// Token: 0x0400B32A RID: 45866
			public Tag sourceObject;

			// Token: 0x0400B32B RID: 45867
			public Sickness.InfectionVector vector;

			// Token: 0x0400B32C RID: 45868
			public float factor;

			// Token: 0x0400B32D RID: 45869
			public Vector3 position;
		}

		// Token: 0x02002868 RID: 10344
		public class InhaleTickInfo
		{
			// Token: 0x0400B32E RID: 45870
			public bool inhaled;

			// Token: 0x0400B32F RID: 45871
			public int ticks;
		}
	}
}
