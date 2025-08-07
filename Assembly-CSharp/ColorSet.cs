using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x02000C9E RID: 3230
public class ColorSet : ScriptableObject
{
	// Token: 0x0600636E RID: 25454 RVA: 0x00255CC8 File Offset: 0x00253EC8
	private void Init()
	{
		if (this.namedLookup == null)
		{
			this.namedLookup = new Dictionary<string, Color32>();
			foreach (FieldInfo fieldInfo in typeof(ColorSet).GetFields())
			{
				if (fieldInfo.FieldType == typeof(Color32))
				{
					this.namedLookup[fieldInfo.Name] = (Color32)fieldInfo.GetValue(this);
				}
			}
		}
	}

	// Token: 0x0600636F RID: 25455 RVA: 0x00255D3E File Offset: 0x00253F3E
	public Color32 GetColorByName(string name)
	{
		this.Init();
		return this.namedLookup[name];
	}

	// Token: 0x06006370 RID: 25456 RVA: 0x00255D52 File Offset: 0x00253F52
	public void RefreshLookup()
	{
		this.namedLookup = null;
		this.Init();
	}

	// Token: 0x06006371 RID: 25457 RVA: 0x00255D61 File Offset: 0x00253F61
	public bool IsDefaultColorSet()
	{
		return Array.IndexOf<ColorSet>(GlobalAssets.Instance.colorSetOptions, this) == 0;
	}

	// Token: 0x04004345 RID: 17221
	public string settingName;

	// Token: 0x04004346 RID: 17222
	[Header("Logic")]
	public Color32 logicOn;

	// Token: 0x04004347 RID: 17223
	public Color32 logicOff;

	// Token: 0x04004348 RID: 17224
	public Color32 logicDisconnected;

	// Token: 0x04004349 RID: 17225
	public Color32 logicOnText;

	// Token: 0x0400434A RID: 17226
	public Color32 logicOffText;

	// Token: 0x0400434B RID: 17227
	public Color32 logicOnSidescreen;

	// Token: 0x0400434C RID: 17228
	public Color32 logicOffSidescreen;

	// Token: 0x0400434D RID: 17229
	[Header("Decor")]
	public Color32 decorPositive;

	// Token: 0x0400434E RID: 17230
	public Color32 decorNegative;

	// Token: 0x0400434F RID: 17231
	public Color32 decorBaseline;

	// Token: 0x04004350 RID: 17232
	public Color32 decorHighlightPositive;

	// Token: 0x04004351 RID: 17233
	public Color32 decorHighlightNegative;

	// Token: 0x04004352 RID: 17234
	[Header("Crop Overlay")]
	public Color32 cropHalted;

	// Token: 0x04004353 RID: 17235
	public Color32 cropGrowing;

	// Token: 0x04004354 RID: 17236
	public Color32 cropGrown;

	// Token: 0x04004355 RID: 17237
	[Header("Harvest Overlay")]
	public Color32 harvestEnabled;

	// Token: 0x04004356 RID: 17238
	public Color32 harvestDisabled;

	// Token: 0x04004357 RID: 17239
	[Header("Gameplay Events")]
	public Color32 eventPositive;

	// Token: 0x04004358 RID: 17240
	public Color32 eventNegative;

	// Token: 0x04004359 RID: 17241
	public Color32 eventNeutral;

	// Token: 0x0400435A RID: 17242
	[Header("Notifications")]
	public Color32 NotificationNormal;

	// Token: 0x0400435B RID: 17243
	public Color32 NotificationNormalBG;

	// Token: 0x0400435C RID: 17244
	public Color32 NotificationBad;

	// Token: 0x0400435D RID: 17245
	public Color32 NotificationBadBG;

	// Token: 0x0400435E RID: 17246
	public Color32 NotificationEvent;

	// Token: 0x0400435F RID: 17247
	public Color32 NotificationEventBG;

	// Token: 0x04004360 RID: 17248
	public Color32 NotificationMessage;

	// Token: 0x04004361 RID: 17249
	public Color32 NotificationMessageBG;

	// Token: 0x04004362 RID: 17250
	public Color32 NotificationMessageImportant;

	// Token: 0x04004363 RID: 17251
	public Color32 NotificationMessageImportantBG;

	// Token: 0x04004364 RID: 17252
	public Color32 NotificationTutorial;

	// Token: 0x04004365 RID: 17253
	public Color32 NotificationTutorialBG;

	// Token: 0x04004366 RID: 17254
	[Header("PrioritiesScreen")]
	public Color32 PrioritiesNeutralColor;

	// Token: 0x04004367 RID: 17255
	public Color32 PrioritiesLowColor;

	// Token: 0x04004368 RID: 17256
	public Color32 PrioritiesHighColor;

	// Token: 0x04004369 RID: 17257
	[Header("Info Screen Status Items")]
	public Color32 statusItemBad;

	// Token: 0x0400436A RID: 17258
	public Color32 statusItemEvent;

	// Token: 0x0400436B RID: 17259
	public Color32 statusItemMessageImportant;

	// Token: 0x0400436C RID: 17260
	[Header("Germ Overlay")]
	public Color32 germFoodPoisoning;

	// Token: 0x0400436D RID: 17261
	public Color32 germPollenGerms;

	// Token: 0x0400436E RID: 17262
	public Color32 germSlimeLung;

	// Token: 0x0400436F RID: 17263
	public Color32 germZombieSpores;

	// Token: 0x04004370 RID: 17264
	public Color32 germRadiationSickness;

	// Token: 0x04004371 RID: 17265
	[Header("Room Overlay")]
	public Color32 roomNone;

	// Token: 0x04004372 RID: 17266
	public Color32 roomFood;

	// Token: 0x04004373 RID: 17267
	public Color32 roomSleep;

	// Token: 0x04004374 RID: 17268
	public Color32 roomRecreation;

	// Token: 0x04004375 RID: 17269
	public Color32 roomBathroom;

	// Token: 0x04004376 RID: 17270
	public Color32 roomHospital;

	// Token: 0x04004377 RID: 17271
	public Color32 roomIndustrial;

	// Token: 0x04004378 RID: 17272
	public Color32 roomAgricultural;

	// Token: 0x04004379 RID: 17273
	public Color32 roomScience;

	// Token: 0x0400437A RID: 17274
	public Color32 roomBionic;

	// Token: 0x0400437B RID: 17275
	public Color32 roomPark;

	// Token: 0x0400437C RID: 17276
	[Header("Power Overlay")]
	public Color32 powerConsumer;

	// Token: 0x0400437D RID: 17277
	public Color32 powerGenerator;

	// Token: 0x0400437E RID: 17278
	public Color32 powerBuildingDisabled;

	// Token: 0x0400437F RID: 17279
	public Color32 powerCircuitUnpowered;

	// Token: 0x04004380 RID: 17280
	public Color32 powerCircuitSafe;

	// Token: 0x04004381 RID: 17281
	public Color32 powerCircuitStraining;

	// Token: 0x04004382 RID: 17282
	public Color32 powerCircuitOverloading;

	// Token: 0x04004383 RID: 17283
	[Header("Light Overlay")]
	public Color32 lightOverlay;

	// Token: 0x04004384 RID: 17284
	[Header("Conduit Overlay")]
	public Color32 conduitNormal;

	// Token: 0x04004385 RID: 17285
	public Color32 conduitInsulated;

	// Token: 0x04004386 RID: 17286
	public Color32 conduitRadiant;

	// Token: 0x04004387 RID: 17287
	[Header("Temperature Overlay")]
	public Color32 temperatureThreshold0;

	// Token: 0x04004388 RID: 17288
	public Color32 temperatureThreshold1;

	// Token: 0x04004389 RID: 17289
	public Color32 temperatureThreshold2;

	// Token: 0x0400438A RID: 17290
	public Color32 temperatureThreshold3;

	// Token: 0x0400438B RID: 17291
	public Color32 temperatureThreshold4;

	// Token: 0x0400438C RID: 17292
	public Color32 temperatureThreshold5;

	// Token: 0x0400438D RID: 17293
	public Color32 temperatureThreshold6;

	// Token: 0x0400438E RID: 17294
	public Color32 temperatureThreshold7;

	// Token: 0x0400438F RID: 17295
	public Color32 heatflowThreshold0;

	// Token: 0x04004390 RID: 17296
	public Color32 heatflowThreshold1;

	// Token: 0x04004391 RID: 17297
	public Color32 heatflowThreshold2;

	// Token: 0x04004392 RID: 17298
	private Dictionary<string, Color32> namedLookup;
}
