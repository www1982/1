using System;
using System.Collections.Generic;
using Klei.AI;

namespace Database
{
	// Token: 0x02000F01 RID: 3841
	public class Personalities : ResourceSet<Personality>
	{
		// Token: 0x060079C2 RID: 31170 RVA: 0x002FFC6C File Offset: 0x002FDE6C
		public Personalities()
		{
			foreach (Personalities.PersonalityInfo personalityInfo in AsyncLoadManager<IGlobalAsyncLoader>.AsyncLoader<Personalities.PersonalityLoader>.Get().entries)
			{
				if (string.IsNullOrEmpty(personalityInfo.RequiredDlcId) || DlcManager.IsContentSubscribed(personalityInfo.RequiredDlcId))
				{
					base.Add(new Personality(personalityInfo.Name.ToUpper(), Strings.Get(string.Format("STRINGS.DUPLICANTS.PERSONALITIES.{0}.NAME", personalityInfo.Name.ToUpper())), personalityInfo.Gender.ToUpper(), personalityInfo.PersonalityType, personalityInfo.StressTrait, personalityInfo.JoyTrait, personalityInfo.StickerType, personalityInfo.CongenitalTrait, personalityInfo.HeadShape, personalityInfo.Mouth, personalityInfo.Neck, personalityInfo.Eyes, personalityInfo.Hair, personalityInfo.Body, personalityInfo.Belt, personalityInfo.Cuff, personalityInfo.Foot, personalityInfo.Hand, personalityInfo.Pelvis, personalityInfo.Leg, personalityInfo.Leg_Skin, personalityInfo.Arm_Skin, Strings.Get(string.Format("STRINGS.DUPLICANTS.PERSONALITIES.{0}.DESC", personalityInfo.Name.ToUpper())), personalityInfo.ValidStarter, personalityInfo.Grave, personalityInfo.Model, personalityInfo.SpeechMouth)
					{
						requiredDlcId = personalityInfo.RequiredDlcId
					});
				}
			}
		}

		// Token: 0x060079C3 RID: 31171 RVA: 0x002FFDBC File Offset: 0x002FDFBC
		private void AddTrait(Personality personality, string trait_name)
		{
			Trait trait = Db.Get().traits.TryGet(trait_name);
			if (trait != null)
			{
				personality.AddTrait(trait);
			}
		}

		// Token: 0x060079C4 RID: 31172 RVA: 0x002FFDE4 File Offset: 0x002FDFE4
		private void SetAttribute(Personality personality, string attribute_name, int value)
		{
			Klei.AI.Attribute attribute = Db.Get().Attributes.TryGet(attribute_name);
			if (attribute == null)
			{
				Debug.LogWarning("Attribute does not exist: " + attribute_name);
				return;
			}
			personality.SetAttribute(attribute, value);
		}

		// Token: 0x060079C5 RID: 31173 RVA: 0x002FFE1E File Offset: 0x002FE01E
		public List<Personality> GetStartingPersonalities()
		{
			return this.resources.FindAll((Personality x) => x.startingMinion);
		}

		// Token: 0x060079C6 RID: 31174 RVA: 0x002FFE4C File Offset: 0x002FE04C
		public List<Personality> GetAll(bool onlyEnabledMinions, bool onlyStartingMinions)
		{
			return this.resources.FindAll((Personality personality) => (!onlyStartingMinions || personality.startingMinion) && (!onlyEnabledMinions || !personality.Disabled) && (!(Game.Instance != null) || Game.IsDlcActiveForCurrentSave(personality.requiredDlcId)));
		}

		// Token: 0x060079C7 RID: 31175 RVA: 0x002FFE84 File Offset: 0x002FE084
		public Personality GetRandom(bool onlyEnabledMinions, bool onlyStartingMinions)
		{
			return this.GetAll(onlyEnabledMinions, onlyStartingMinions).GetRandom<Personality>();
		}

		// Token: 0x060079C8 RID: 31176 RVA: 0x002FFE94 File Offset: 0x002FE094
		public Personality GetRandom(Tag model, bool onlyEnabledMinions, bool onlyStartingMinions)
		{
			return this.GetAll(onlyEnabledMinions, onlyStartingMinions).FindAll((Personality personality) => personality.model == model || model == null).GetRandom<Personality>();
		}

		// Token: 0x060079C9 RID: 31177 RVA: 0x002FFECC File Offset: 0x002FE0CC
		public Personality GetRandom(List<Tag> models, bool onlyEnabledMinions, bool onlyStartingMinions)
		{
			return this.GetAll(onlyEnabledMinions, onlyStartingMinions).FindAll((Personality personality) => models.Contains(personality.model)).GetRandom<Personality>();
		}

		// Token: 0x060079CA RID: 31178 RVA: 0x002FFF04 File Offset: 0x002FE104
		public Personality GetPersonalityFromNameStringKey(string name_string_key)
		{
			foreach (Personality personality in Db.Get().Personalities.resources)
			{
				if (personality.nameStringKey.Equals(name_string_key, StringComparison.CurrentCultureIgnoreCase))
				{
					return personality;
				}
			}
			return null;
		}

		// Token: 0x020020EB RID: 8427
		public class PersonalityLoader : AsyncCsvLoader<Personalities.PersonalityLoader, Personalities.PersonalityInfo>
		{
			// Token: 0x0600B891 RID: 47249 RVA: 0x003EA426 File Offset: 0x003E8626
			public PersonalityLoader()
				: base(Assets.instance.personalitiesFile)
			{
			}

			// Token: 0x0600B892 RID: 47250 RVA: 0x003EA438 File Offset: 0x003E8638
			public override void Run()
			{
				base.Run();
			}
		}

		// Token: 0x020020EC RID: 8428
		public class PersonalityInfo : Resource
		{
			// Token: 0x040096B8 RID: 38584
			public int HeadShape;

			// Token: 0x040096B9 RID: 38585
			public int Mouth;

			// Token: 0x040096BA RID: 38586
			public int Neck;

			// Token: 0x040096BB RID: 38587
			public int Eyes;

			// Token: 0x040096BC RID: 38588
			public int Hair;

			// Token: 0x040096BD RID: 38589
			public int Body;

			// Token: 0x040096BE RID: 38590
			public int Belt;

			// Token: 0x040096BF RID: 38591
			public int Cuff;

			// Token: 0x040096C0 RID: 38592
			public int Foot;

			// Token: 0x040096C1 RID: 38593
			public int Hand;

			// Token: 0x040096C2 RID: 38594
			public int Pelvis;

			// Token: 0x040096C3 RID: 38595
			public int Leg;

			// Token: 0x040096C4 RID: 38596
			public int Arm_Skin;

			// Token: 0x040096C5 RID: 38597
			public int Leg_Skin;

			// Token: 0x040096C6 RID: 38598
			public int SpeechMouth;

			// Token: 0x040096C7 RID: 38599
			public string Gender;

			// Token: 0x040096C8 RID: 38600
			public string PersonalityType;

			// Token: 0x040096C9 RID: 38601
			public string StressTrait;

			// Token: 0x040096CA RID: 38602
			public string JoyTrait;

			// Token: 0x040096CB RID: 38603
			public string StickerType;

			// Token: 0x040096CC RID: 38604
			public string CongenitalTrait;

			// Token: 0x040096CD RID: 38605
			public string Design;

			// Token: 0x040096CE RID: 38606
			public bool ValidStarter;

			// Token: 0x040096CF RID: 38607
			public string Grave;

			// Token: 0x040096D0 RID: 38608
			public string Model;

			// Token: 0x040096D1 RID: 38609
			public string RequiredDlcId;
		}
	}
}
