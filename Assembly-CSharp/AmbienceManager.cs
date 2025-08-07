using System;
using System.Collections.Generic;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

// Token: 0x02000542 RID: 1346
[AddComponentMenu("KMonoBehaviour/scripts/AmbienceManager")]
public class AmbienceManager : KMonoBehaviour
{
	// Token: 0x170000EC RID: 236
	// (get) Token: 0x06001DC7 RID: 7623 RVA: 0x000A17B4 File Offset: 0x0009F9B4
	// (set) Token: 0x06001DC6 RID: 7622 RVA: 0x000A17AC File Offset: 0x0009F9AC
	public static float BoilingTreshold { get; private set; } = 1f;

	// Token: 0x06001DC8 RID: 7624 RVA: 0x000A17BC File Offset: 0x0009F9BC
	protected override void OnSpawn()
	{
		if (!RuntimeManager.IsInitialized)
		{
			base.enabled = false;
			return;
		}
		AmbienceManager.BoilingTreshold = this.LiquidMaterial.GetFloat("_BoilingTreshold");
		for (int i = 0; i < this.quadrants.Length; i++)
		{
			this.quadrants[i] = new AmbienceManager.Quadrant(this.quadrantDefs[i]);
		}
	}

	// Token: 0x06001DC9 RID: 7625 RVA: 0x000A1818 File Offset: 0x0009FA18
	protected override void OnForcedCleanUp()
	{
		AmbienceManager.Quadrant[] array = this.quadrants;
		for (int i = 0; i < array.Length; i++)
		{
			foreach (AmbienceManager.Layer layer in array[i].GetAllLayers())
			{
				layer.Stop();
			}
		}
	}

	// Token: 0x06001DCA RID: 7626 RVA: 0x000A1880 File Offset: 0x0009FA80
	private void LateUpdate()
	{
		GridArea visibleArea = GridVisibleArea.GetVisibleArea();
		Vector2I min = visibleArea.Min;
		Vector2I max = visibleArea.Max;
		Vector2I vector2I = min + (max - min) / 2;
		Vector3 vector = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, Camera.main.transform.GetPosition().z));
		Vector3 vector2 = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, Camera.main.transform.GetPosition().z));
		Vector3 vector3 = vector2 + (vector - vector2) / 2f;
		Vector3 vector4 = vector - vector2;
		if (vector4.x > vector4.y)
		{
			vector4.y = vector4.x;
		}
		else
		{
			vector4.x = vector4.y;
		}
		vector = vector3 + vector4 / 2f;
		vector2 = vector3 - vector4 / 2f;
		Vector3 vector5 = vector4 / 2f / 2f;
		this.quadrants[0].Update(new Vector2I(min.x, min.y), new Vector2I(vector2I.x, vector2I.y), new Vector3(vector2.x + vector5.x, vector2.y + vector5.y, this.emitterZPosition));
		this.quadrants[1].Update(new Vector2I(vector2I.x, min.y), new Vector2I(max.x, vector2I.y), new Vector3(vector3.x + vector5.x, vector2.y + vector5.y, this.emitterZPosition));
		this.quadrants[2].Update(new Vector2I(min.x, vector2I.y), new Vector2I(vector2I.x, max.y), new Vector3(vector2.x + vector5.x, vector3.y + vector5.y, this.emitterZPosition));
		this.quadrants[3].Update(new Vector2I(vector2I.x, vector2I.y), new Vector2I(max.x, max.y), new Vector3(vector3.x + vector5.x, vector3.y + vector5.y, this.emitterZPosition));
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		for (int i = 0; i < this.quadrants.Length; i++)
		{
			num += (float)this.quadrants[i].spaceLayer.tileCount;
			num2 += (float)this.quadrants[i].facilityLayer.tileCount;
			num3 += (float)this.quadrants[i].totalTileCount;
		}
		AudioMixer.instance.UpdateSpaceVisibleSnapshot(num / num3);
		AudioMixer.instance.UpdateFacilityVisibleSnapshot(num2 / num3);
	}

	// Token: 0x04001151 RID: 4433
	public Material LiquidMaterial;

	// Token: 0x04001153 RID: 4435
	private float emitterZPosition;

	// Token: 0x04001154 RID: 4436
	public AmbienceManager.QuadrantDef[] quadrantDefs;

	// Token: 0x04001155 RID: 4437
	public AmbienceManager.Quadrant[] quadrants = new AmbienceManager.Quadrant[4];

	// Token: 0x02001393 RID: 5011
	public class Tuning : TuningData<AmbienceManager.Tuning>
	{
		// Token: 0x040069BB RID: 27067
		public int backwallTileValue = 1;

		// Token: 0x040069BC RID: 27068
		public int foundationTileValue = 2;

		// Token: 0x040069BD RID: 27069
		public int buildingTileValue = 3;
	}

	// Token: 0x02001394 RID: 5012
	public class LiquidLayer : AmbienceManager.Layer
	{
		// Token: 0x06008AC6 RID: 35526 RVA: 0x00350EB6 File Offset: 0x0034F0B6
		public LiquidLayer(EventReference sound, EventReference one_shot_sound = default(EventReference))
			: base(sound, one_shot_sound)
		{
		}

		// Token: 0x06008AC7 RID: 35527 RVA: 0x00350EC0 File Offset: 0x0034F0C0
		public override void Reset()
		{
			base.Reset();
			this.boilingTileCount = 0;
			this.averageBoilIntensity = 0f;
		}

		// Token: 0x06008AC8 RID: 35528 RVA: 0x00350EDA File Offset: 0x0034F0DA
		public override void UpdatePercentage(int cell_count)
		{
			base.UpdatePercentage(cell_count);
			this.boilTilePercentage = (float)this.boilingTileCount / (float)cell_count;
		}

		// Token: 0x06008AC9 RID: 35529 RVA: 0x00350EF3 File Offset: 0x0034F0F3
		public override void UpdateParameters(Vector3 emitter_position)
		{
			base.UpdateParameters(emitter_position);
			this.soundEvent.setParameterByName("Boiling_Tile_Percentage", this.boilTilePercentage, false);
		}

		// Token: 0x06008ACA RID: 35530 RVA: 0x00350F14 File Offset: 0x0034F114
		public override void UpdateAverageTemperature()
		{
			base.UpdateAverageTemperature();
			this.UpdateAverageBoilIntensity();
		}

		// Token: 0x06008ACB RID: 35531 RVA: 0x00350F22 File Offset: 0x0034F122
		public void UpdateAverageBoilIntensity()
		{
			this.averageBoilIntensity = ((this.tileCount > 0) ? (this.averageBoilIntensity / (float)this.tileCount) : 0f);
			this.soundEvent.setParameterByName("Boiling_Intensity", this.averageBoilIntensity, false);
		}

		// Token: 0x040069BE RID: 27070
		private const string BOILING_INTENSITY_ID = "Boiling_Intensity";

		// Token: 0x040069BF RID: 27071
		private const string BOILING_TILE_PERCENTAGE_ID = "Boiling_Tile_Percentage";

		// Token: 0x040069C0 RID: 27072
		public int boilingTileCount;

		// Token: 0x040069C1 RID: 27073
		public float boilTilePercentage;

		// Token: 0x040069C2 RID: 27074
		public float averageBoilIntensity;
	}

	// Token: 0x02001395 RID: 5013
	public class Layer : IComparable<AmbienceManager.Layer>
	{
		// Token: 0x06008ACC RID: 35532 RVA: 0x00350F60 File Offset: 0x0034F160
		public Layer(EventReference sound, EventReference one_shot_sound = default(EventReference))
		{
			this.sound = sound;
			this.oneShotSound = one_shot_sound;
		}

		// Token: 0x06008ACD RID: 35533 RVA: 0x00350F76 File Offset: 0x0034F176
		public virtual void Reset()
		{
			this.tileCount = 0;
			this.averageTemperature = 0f;
			this.averageRadiation = 0f;
		}

		// Token: 0x06008ACE RID: 35534 RVA: 0x00350F95 File Offset: 0x0034F195
		public virtual void UpdatePercentage(int cell_count)
		{
			this.tilePercentage = (float)this.tileCount / (float)cell_count;
		}

		// Token: 0x06008ACF RID: 35535 RVA: 0x00350FA7 File Offset: 0x0034F1A7
		public virtual void UpdateAverageTemperature()
		{
			this.averageTemperature /= (float)this.tileCount;
			this.soundEvent.setParameterByName("averageTemperature", this.averageTemperature, false);
		}

		// Token: 0x06008AD0 RID: 35536 RVA: 0x00350FD5 File Offset: 0x0034F1D5
		public void UpdateAverageRadiation()
		{
			this.averageRadiation = ((this.tileCount > 0) ? (this.averageRadiation / (float)this.tileCount) : 0f);
			this.soundEvent.setParameterByName("averageRadiation", this.averageRadiation, false);
		}

		// Token: 0x06008AD1 RID: 35537 RVA: 0x00351014 File Offset: 0x0034F214
		public virtual void UpdateParameters(Vector3 emitter_position)
		{
			if (!this.soundEvent.isValid())
			{
				return;
			}
			Vector3 vector = new Vector3(emitter_position.x, emitter_position.y, 0f);
			this.soundEvent.set3DAttributes(vector.To3DAttributes());
			this.soundEvent.setParameterByName("tilePercentage", this.tilePercentage, false);
		}

		// Token: 0x06008AD2 RID: 35538 RVA: 0x00351071 File Offset: 0x0034F271
		public void SetCustomParameter(string parameterName, float value)
		{
			this.soundEvent.setParameterByName(parameterName, value, false);
		}

		// Token: 0x06008AD3 RID: 35539 RVA: 0x00351082 File Offset: 0x0034F282
		public int CompareTo(AmbienceManager.Layer layer)
		{
			return layer.tileCount - this.tileCount;
		}

		// Token: 0x06008AD4 RID: 35540 RVA: 0x00351091 File Offset: 0x0034F291
		public void SetVolume(float volume)
		{
			if (this.volume != volume)
			{
				this.volume = volume;
				if (this.soundEvent.isValid())
				{
					this.soundEvent.setVolume(volume);
				}
			}
		}

		// Token: 0x06008AD5 RID: 35541 RVA: 0x003510BD File Offset: 0x0034F2BD
		public void Stop()
		{
			if (this.soundEvent.isValid())
			{
				this.soundEvent.stop(global::FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
				this.soundEvent.release();
			}
			this.isRunning = false;
		}

		// Token: 0x06008AD6 RID: 35542 RVA: 0x003510EC File Offset: 0x0034F2EC
		public void Start(Vector3 emitter_position)
		{
			if (!this.isRunning)
			{
				if (!this.oneShotSound.IsNull)
				{
					EventInstance eventInstance = KFMOD.CreateInstance(this.oneShotSound);
					if (!eventInstance.isValid())
					{
						string text = "Could not find event: ";
						EventReference eventReference = this.oneShotSound;
						global::Debug.LogWarning(text + eventReference.ToString());
						return;
					}
					ATTRIBUTES_3D attributes_3D = new Vector3(emitter_position.x, emitter_position.y, 0f).To3DAttributes();
					eventInstance.set3DAttributes(attributes_3D);
					eventInstance.setVolume(this.tilePercentage * 2f);
					eventInstance.start();
					eventInstance.release();
					return;
				}
				else
				{
					this.soundEvent = KFMOD.CreateInstance(this.sound);
					if (this.soundEvent.isValid())
					{
						this.soundEvent.start();
					}
					this.isRunning = true;
				}
			}
		}

		// Token: 0x040069C3 RID: 27075
		private const string TILE_PERCENTAGE_ID = "tilePercentage";

		// Token: 0x040069C4 RID: 27076
		private const string AVERAGE_TEMPERATURE_ID = "averageTemperature";

		// Token: 0x040069C5 RID: 27077
		private const string AVERAGE_RADIATION_ID = "averageRadiation";

		// Token: 0x040069C6 RID: 27078
		public EventReference sound;

		// Token: 0x040069C7 RID: 27079
		public EventReference oneShotSound;

		// Token: 0x040069C8 RID: 27080
		public int tileCount;

		// Token: 0x040069C9 RID: 27081
		public float tilePercentage;

		// Token: 0x040069CA RID: 27082
		public float volume;

		// Token: 0x040069CB RID: 27083
		public bool isRunning;

		// Token: 0x040069CC RID: 27084
		protected EventInstance soundEvent;

		// Token: 0x040069CD RID: 27085
		public float averageTemperature;

		// Token: 0x040069CE RID: 27086
		public float averageRadiation;
	}

	// Token: 0x02001396 RID: 5014
	[Serializable]
	public class QuadrantDef
	{
		// Token: 0x040069CF RID: 27087
		public string name;

		// Token: 0x040069D0 RID: 27088
		public EventReference[] liquidSounds;

		// Token: 0x040069D1 RID: 27089
		public EventReference[] gasSounds;

		// Token: 0x040069D2 RID: 27090
		public EventReference[] solidSounds;

		// Token: 0x040069D3 RID: 27091
		public EventReference fogSound;

		// Token: 0x040069D4 RID: 27092
		public EventReference spaceSound;

		// Token: 0x040069D5 RID: 27093
		public EventReference rocketInteriorSound;

		// Token: 0x040069D6 RID: 27094
		public EventReference facilitySound;

		// Token: 0x040069D7 RID: 27095
		public EventReference radiationSound;
	}

	// Token: 0x02001397 RID: 5015
	public class Quadrant
	{
		// Token: 0x06008AD8 RID: 35544 RVA: 0x003511D0 File Offset: 0x0034F3D0
		public Quadrant(AmbienceManager.QuadrantDef def)
		{
			this.name = def.name;
			this.fogLayer = new AmbienceManager.Layer(def.fogSound, default(EventReference));
			this.allLayers.Add(this.fogLayer);
			this.loopingLayers.Add(this.fogLayer);
			this.spaceLayer = new AmbienceManager.Layer(def.spaceSound, default(EventReference));
			this.allLayers.Add(this.spaceLayer);
			this.loopingLayers.Add(this.spaceLayer);
			this.m_isClusterSpaceEnabled = DlcManager.FeatureClusterSpaceEnabled();
			if (this.m_isClusterSpaceEnabled)
			{
				this.rocketInteriorLayer = new AmbienceManager.Layer(def.rocketInteriorSound, default(EventReference));
				this.allLayers.Add(this.rocketInteriorLayer);
			}
			this.facilityLayer = new AmbienceManager.Layer(def.facilitySound, default(EventReference));
			this.allLayers.Add(this.facilityLayer);
			this.loopingLayers.Add(this.facilityLayer);
			this.m_isRadiationEnabled = Sim.IsRadiationEnabled();
			if (this.m_isRadiationEnabled)
			{
				this.radiationLayer = new AmbienceManager.Layer(def.radiationSound, default(EventReference));
				this.allLayers.Add(this.radiationLayer);
			}
			for (int i = 0; i < 4; i++)
			{
				this.gasLayers[i] = new AmbienceManager.Layer(def.gasSounds[i], default(EventReference));
				this.liquidLayers[i] = new AmbienceManager.LiquidLayer(def.liquidSounds[i], default(EventReference));
				this.allLayers.Add(this.gasLayers[i]);
				this.allLayers.Add(this.liquidLayers[i]);
				this.loopingLayers.Add(this.gasLayers[i]);
				this.loopingLayers.Add(this.liquidLayers[i]);
			}
			for (int j = 0; j < this.solidLayers.Length; j++)
			{
				if (j >= def.solidSounds.Length)
				{
					string text = "Missing solid layer: ";
					SolidAmbienceType solidAmbienceType = (SolidAmbienceType)j;
					global::Debug.LogError(text + solidAmbienceType.ToString());
				}
				this.solidLayers[j] = new AmbienceManager.Layer(default(EventReference), def.solidSounds[j]);
				this.allLayers.Add(this.solidLayers[j]);
				this.oneShotLayers.Add(this.solidLayers[j]);
			}
			this.solidTimers = new AmbienceManager.Quadrant.SolidTimer[AmbienceManager.Quadrant.activeSolidLayerCount];
			for (int k = 0; k < AmbienceManager.Quadrant.activeSolidLayerCount; k++)
			{
				this.solidTimers[k] = new AmbienceManager.Quadrant.SolidTimer();
			}
		}

		// Token: 0x06008AD9 RID: 35545 RVA: 0x003514C8 File Offset: 0x0034F6C8
		public void Update(Vector2I min, Vector2I max, Vector3 emitter_position)
		{
			this.emitterPosition = emitter_position;
			this.totalTileCount = 0;
			for (int i = 0; i < this.allLayers.Count; i++)
			{
				this.allLayers[i].Reset();
			}
			float num = 1f - AmbienceManager.BoilingTreshold;
			for (int j = min.y; j < max.y; j++)
			{
				if (j % 2 != 1)
				{
					for (int k = min.x; k < max.x; k++)
					{
						if (k % 2 != 0)
						{
							int num2 = Grid.XYToCell(k, j);
							if (Grid.IsValidCell(num2))
							{
								this.totalTileCount++;
								if (Grid.IsVisible(num2))
								{
									if (Grid.GravitasFacility[num2])
									{
										this.facilityLayer.tileCount += 8;
									}
									else
									{
										Element element = Grid.Element[num2];
										if (element != null)
										{
											if (element.IsLiquid && Grid.IsSubstantialLiquid(num2, 0.35f))
											{
												AmbienceType ambience = element.substance.GetAmbience();
												if (ambience != AmbienceType.None)
												{
													this.liquidLayers[(int)ambience].tileCount++;
													this.liquidLayers[(int)ambience].averageTemperature += Grid.Temperature[num2];
													float num3 = Mathf.Clamp01(element.GetRelativeHeatLevel(Grid.Temperature[num2]) - AmbienceManager.BoilingTreshold) / num;
													this.liquidLayers[(int)ambience].boilingTileCount += ((num3 > 0f) ? 1 : 0);
													this.liquidLayers[(int)ambience].averageBoilIntensity += num3;
												}
											}
											else if (element.IsGas)
											{
												AmbienceType ambience2 = element.substance.GetAmbience();
												if (ambience2 != AmbienceType.None)
												{
													this.gasLayers[(int)ambience2].tileCount++;
													this.gasLayers[(int)ambience2].averageTemperature += Grid.Temperature[num2];
												}
											}
											else if (element.IsSolid)
											{
												SolidAmbienceType solidAmbienceType = element.substance.GetSolidAmbience();
												if (Grid.Foundation[num2])
												{
													solidAmbienceType = SolidAmbienceType.Tile;
													this.solidLayers[(int)solidAmbienceType].tileCount += TuningData<AmbienceManager.Tuning>.Get().foundationTileValue;
													this.spaceLayer.tileCount -= TuningData<AmbienceManager.Tuning>.Get().foundationTileValue;
												}
												else if (Grid.Objects[num2, 2] != null)
												{
													solidAmbienceType = SolidAmbienceType.Tile;
													this.solidLayers[(int)solidAmbienceType].tileCount += TuningData<AmbienceManager.Tuning>.Get().backwallTileValue;
													this.spaceLayer.tileCount -= TuningData<AmbienceManager.Tuning>.Get().backwallTileValue;
												}
												else if (solidAmbienceType != SolidAmbienceType.None)
												{
													this.solidLayers[(int)solidAmbienceType].tileCount++;
												}
												else if (element.id == SimHashes.Regolith || element.id == SimHashes.MaficRock)
												{
													this.spaceLayer.tileCount++;
												}
											}
											else if (element.id == SimHashes.Vacuum && CellSelectionObject.IsExposedToSpace(num2))
											{
												if (Grid.Objects[num2, 1] != null)
												{
													this.spaceLayer.tileCount -= TuningData<AmbienceManager.Tuning>.Get().buildingTileValue;
												}
												this.spaceLayer.tileCount++;
											}
										}
									}
									if (Grid.Radiation[num2] > 0f)
									{
										this.radiationLayer.averageRadiation += Grid.Radiation[num2];
										this.radiationLayer.tileCount++;
									}
								}
								else
								{
									this.fogLayer.tileCount++;
								}
							}
						}
					}
				}
			}
			Vector2I vector2I = max - min;
			int num4 = vector2I.x * vector2I.y;
			for (int l = 0; l < this.allLayers.Count; l++)
			{
				this.allLayers[l].UpdatePercentage(num4);
			}
			this.loopingLayers.Sort();
			this.topLayers.Clear();
			for (int m = 0; m < this.loopingLayers.Count; m++)
			{
				AmbienceManager.Layer layer = this.loopingLayers[m];
				if (m < 3 && layer.tilePercentage > 0f)
				{
					layer.Start(emitter_position);
					layer.UpdateAverageTemperature();
					layer.UpdateParameters(emitter_position);
					this.topLayers.Add(layer);
				}
				else
				{
					layer.Stop();
				}
			}
			if (this.m_isClusterSpaceEnabled)
			{
				float num5 = 0f;
				if (ClusterManager.Instance != null && ClusterManager.Instance.activeWorld != null && ClusterManager.Instance.activeWorld.IsModuleInterior)
				{
					num5 = 1f;
				}
				this.rocketInteriorLayer.Start(emitter_position);
				this.rocketInteriorLayer.SetCustomParameter("RocketState", (float)ClusterManager.RocketInteriorState);
				this.rocketInteriorLayer.SetVolume(num5);
			}
			if (this.m_isRadiationEnabled)
			{
				this.radiationLayer.Start(emitter_position);
				this.radiationLayer.UpdateAverageRadiation();
				this.radiationLayer.UpdateParameters(emitter_position);
			}
			this.oneShotLayers.Sort();
			for (int n = 0; n < AmbienceManager.Quadrant.activeSolidLayerCount; n++)
			{
				if (this.solidTimers[n].ShouldPlay() && this.oneShotLayers[n].tilePercentage > 0f)
				{
					this.oneShotLayers[n].Start(emitter_position);
				}
			}
		}

		// Token: 0x06008ADA RID: 35546 RVA: 0x00351A79 File Offset: 0x0034FC79
		public List<AmbienceManager.Layer> GetAllLayers()
		{
			return this.allLayers;
		}

		// Token: 0x040069D8 RID: 27096
		public string name;

		// Token: 0x040069D9 RID: 27097
		public Vector3 emitterPosition;

		// Token: 0x040069DA RID: 27098
		public AmbienceManager.Layer[] gasLayers = new AmbienceManager.Layer[4];

		// Token: 0x040069DB RID: 27099
		public AmbienceManager.LiquidLayer[] liquidLayers = new AmbienceManager.LiquidLayer[4];

		// Token: 0x040069DC RID: 27100
		public AmbienceManager.Layer fogLayer;

		// Token: 0x040069DD RID: 27101
		public AmbienceManager.Layer spaceLayer;

		// Token: 0x040069DE RID: 27102
		public AmbienceManager.Layer rocketInteriorLayer;

		// Token: 0x040069DF RID: 27103
		public AmbienceManager.Layer facilityLayer;

		// Token: 0x040069E0 RID: 27104
		public AmbienceManager.Layer radiationLayer;

		// Token: 0x040069E1 RID: 27105
		public AmbienceManager.Layer[] solidLayers = new AmbienceManager.Layer[21];

		// Token: 0x040069E2 RID: 27106
		private List<AmbienceManager.Layer> allLayers = new List<AmbienceManager.Layer>();

		// Token: 0x040069E3 RID: 27107
		private List<AmbienceManager.Layer> loopingLayers = new List<AmbienceManager.Layer>();

		// Token: 0x040069E4 RID: 27108
		private List<AmbienceManager.Layer> oneShotLayers = new List<AmbienceManager.Layer>();

		// Token: 0x040069E5 RID: 27109
		private List<AmbienceManager.Layer> topLayers = new List<AmbienceManager.Layer>();

		// Token: 0x040069E6 RID: 27110
		public static int activeSolidLayerCount = 2;

		// Token: 0x040069E7 RID: 27111
		public int totalTileCount;

		// Token: 0x040069E8 RID: 27112
		private bool m_isRadiationEnabled;

		// Token: 0x040069E9 RID: 27113
		private bool m_isClusterSpaceEnabled;

		// Token: 0x040069EA RID: 27114
		private const string ROCKET_STATE_FOR_AMBIENCE = "RocketState";

		// Token: 0x040069EB RID: 27115
		private AmbienceManager.Quadrant.SolidTimer[] solidTimers;

		// Token: 0x0200272D RID: 10029
		public class SolidTimer
		{
			// Token: 0x0600C605 RID: 50693 RVA: 0x004115ED File Offset: 0x0040F7ED
			public SolidTimer()
			{
				this.solidTargetTime = Time.unscaledTime + global::UnityEngine.Random.value * AmbienceManager.Quadrant.SolidTimer.solidMinTime;
			}

			// Token: 0x0600C606 RID: 50694 RVA: 0x0041160C File Offset: 0x0040F80C
			public bool ShouldPlay()
			{
				if (Time.unscaledTime > this.solidTargetTime)
				{
					this.solidTargetTime = Time.unscaledTime + AmbienceManager.Quadrant.SolidTimer.solidMinTime + global::UnityEngine.Random.value * (AmbienceManager.Quadrant.SolidTimer.solidMaxTime - AmbienceManager.Quadrant.SolidTimer.solidMinTime);
					return true;
				}
				return false;
			}

			// Token: 0x0400AD0C RID: 44300
			public static float solidMinTime = 9f;

			// Token: 0x0400AD0D RID: 44301
			public static float solidMaxTime = 15f;

			// Token: 0x0400AD0E RID: 44302
			public float solidTargetTime;
		}
	}
}
