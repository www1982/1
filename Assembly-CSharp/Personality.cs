using System;
using System.Collections.Generic;
using Klei.AI;
using UnityEngine;

// Token: 0x02000A21 RID: 2593
public class Personality : Resource
{
	// Token: 0x17000528 RID: 1320
	// (get) Token: 0x06004B3F RID: 19263 RVA: 0x001B4C33 File Offset: 0x001B2E33
	public string description
	{
		get
		{
			return this.GetDescription();
		}
	}

	// Token: 0x06004B40 RID: 19264 RVA: 0x001B4C3C File Offset: 0x001B2E3C
	[Obsolete("Modders: Use constructor with isStartingMinion parameter")]
	public Personality(string name_string_key, string name, string Gender, string PersonalityType, string StressTrait, string JoyTrait, string StickerType, string CongenitalTrait, int headShape, int mouth, int neck, int eyes, int hair, int body, string description)
		: this(name_string_key, name, Gender, PersonalityType, StressTrait, JoyTrait, StickerType, CongenitalTrait, headShape, mouth, neck, eyes, hair, body, 0, 0, 0, 0, 0, 0, headShape, headShape, description, true, "", GameTags.Minions.Models.Standard, 0)
	{
	}

	// Token: 0x06004B41 RID: 19265 RVA: 0x001B4C80 File Offset: 0x001B2E80
	[Obsolete("Modders: Added additional body part customization to duplicant personalities")]
	public Personality(string name_string_key, string name, string Gender, string PersonalityType, string StressTrait, string JoyTrait, string StickerType, string CongenitalTrait, int headShape, int mouth, int neck, int eyes, int hair, int body, string description, bool isStartingMinion)
		: this(name_string_key, name, Gender, PersonalityType, StressTrait, JoyTrait, StickerType, CongenitalTrait, headShape, mouth, neck, eyes, hair, body, 0, 0, 0, 0, 0, 0, headShape, headShape, description, true, "", GameTags.Minions.Models.Standard, 0)
	{
	}

	// Token: 0x06004B42 RID: 19266 RVA: 0x001B4CC4 File Offset: 0x001B2EC4
	[Obsolete("Modders: Added a custom gravestone image to duplicant personalities")]
	public Personality(string name_string_key, string name, string Gender, string PersonalityType, string StressTrait, string JoyTrait, string StickerType, string CongenitalTrait, int headShape, int mouth, int neck, int eyes, int hair, int body, int belt, int cuff, int foot, int hand, int pelvis, int leg, string description, bool isStartingMinion)
		: this(name_string_key, name, Gender, PersonalityType, StressTrait, JoyTrait, StickerType, CongenitalTrait, headShape, mouth, neck, eyes, hair, body, 0, 0, 0, 0, 0, 0, headShape, headShape, description, isStartingMinion, "", GameTags.Minions.Models.Standard, 0)
	{
	}

	// Token: 0x06004B43 RID: 19267 RVA: 0x001B4D0C File Offset: 0x001B2F0C
	[Obsolete("Modders: Added 'model', 'arm_skin' and 'leg skin' to duplicant personalities")]
	public Personality(string name_string_key, string name, string Gender, string PersonalityType, string StressTrait, string JoyTrait, string StickerType, string CongenitalTrait, int headShape, int mouth, int neck, int eyes, int hair, int body, int belt, int cuff, int foot, int hand, int pelvis, int leg, string description, bool isStartingMinion, string graveStone)
		: this(name_string_key, name, Gender, PersonalityType, StressTrait, JoyTrait, StickerType, CongenitalTrait, headShape, mouth, neck, eyes, hair, body, 0, 0, 0, 0, 0, 0, headShape, headShape, description, isStartingMinion, "", GameTags.Minions.Models.Standard, 0)
	{
	}

	// Token: 0x06004B44 RID: 19268 RVA: 0x001B4D54 File Offset: 0x001B2F54
	[Obsolete("Modders: Added override_speech_mouth to duplicant personalities")]
	public Personality(string name_string_key, string name, string Gender, string PersonalityType, string StressTrait, string JoyTrait, string StickerType, string CongenitalTrait, int headShape, int mouth, int neck, int eyes, int hair, int body, int belt, int cuff, int foot, int hand, int pelvis, int leg, int arm_skin, int leg_skin, string description, bool isStartingMinion, string graveStone, Tag model)
		: this(name_string_key, name, Gender, PersonalityType, StressTrait, JoyTrait, StickerType, CongenitalTrait, headShape, mouth, neck, eyes, hair, body, belt, cuff, foot, hand, pelvis, leg, arm_skin, leg_skin, description, isStartingMinion, graveStone, model, 0)
	{
	}

	// Token: 0x06004B45 RID: 19269 RVA: 0x001B4D9C File Offset: 0x001B2F9C
	public Personality(string name_string_key, string name, string Gender, string PersonalityType, string StressTrait, string JoyTrait, string StickerType, string CongenitalTrait, int headShape, int mouth, int neck, int eyes, int hair, int body, int belt, int cuff, int foot, int hand, int pelvis, int leg, int arm_skin, int leg_skin, string description, bool isStartingMinion, string graveStone, Tag model, int SpeechMouth)
		: base(name_string_key, name)
	{
		this.nameStringKey = name_string_key;
		this.genderStringKey = Gender;
		this.personalityType = PersonalityType;
		this.stresstrait = StressTrait;
		this.joyTrait = JoyTrait;
		this.stickerType = StickerType;
		this.congenitaltrait = CongenitalTrait;
		this.unformattedDescription = description;
		this.headShape = headShape;
		this.mouth = mouth;
		this.neck = neck;
		this.eyes = eyes;
		this.hair = hair;
		this.body = body;
		this.belt = belt;
		this.cuff = cuff;
		this.foot = foot;
		this.hand = hand;
		this.pelvis = pelvis;
		this.leg = leg;
		this.arm_skin = arm_skin;
		this.leg_skin = leg_skin;
		this.startingMinion = isStartingMinion;
		this.graveStone = graveStone;
		this.model = model;
		this.speech_mouth = SpeechMouth;
	}

	// Token: 0x06004B46 RID: 19270 RVA: 0x001B4E95 File Offset: 0x001B3095
	public string GetDescription()
	{
		this.unformattedDescription = this.unformattedDescription.Replace("{0}", this.Name);
		return this.unformattedDescription;
	}

	// Token: 0x06004B47 RID: 19271 RVA: 0x001B4EBC File Offset: 0x001B30BC
	public void SetAttribute(Klei.AI.Attribute attribute, int value)
	{
		Personality.StartingAttribute startingAttribute = new Personality.StartingAttribute(attribute, value);
		this.attributes.Add(startingAttribute);
	}

	// Token: 0x06004B48 RID: 19272 RVA: 0x001B4EDD File Offset: 0x001B30DD
	public void AddTrait(Trait trait)
	{
		this.traits.Add(trait);
	}

	// Token: 0x06004B49 RID: 19273 RVA: 0x001B4EEB File Offset: 0x001B30EB
	public void SetSelectedTemplateOutfitId(ClothingOutfitUtility.OutfitType outfitType, Option<string> outfit)
	{
		CustomClothingOutfits.Instance.Internal_SetDuplicantPersonalityOutfit(outfitType, this.Id, outfit);
	}

	// Token: 0x06004B4A RID: 19274 RVA: 0x001B4F00 File Offset: 0x001B3100
	public string GetSelectedTemplateOutfitId(ClothingOutfitUtility.OutfitType outfitType)
	{
		string text;
		if (CustomClothingOutfits.Instance.Internal_TryGetDuplicantPersonalityOutfit(outfitType, this.Id, out text))
		{
			return text;
		}
		return null;
	}

	// Token: 0x06004B4B RID: 19275 RVA: 0x001B4F28 File Offset: 0x001B3128
	public Sprite GetMiniIcon()
	{
		if (string.IsNullOrWhiteSpace(this.nameStringKey))
		{
			return Assets.GetSprite("unknown");
		}
		string text;
		if (this.nameStringKey == "MIMA")
		{
			text = "Mi-Ma";
		}
		else
		{
			text = this.nameStringKey[0].ToString() + this.nameStringKey.Substring(1).ToLower();
		}
		return Assets.GetSprite("dreamIcon_" + text);
	}

	// Token: 0x040031E6 RID: 12774
	public List<Personality.StartingAttribute> attributes = new List<Personality.StartingAttribute>();

	// Token: 0x040031E7 RID: 12775
	public List<Trait> traits = new List<Trait>();

	// Token: 0x040031E8 RID: 12776
	public int headShape;

	// Token: 0x040031E9 RID: 12777
	public int mouth;

	// Token: 0x040031EA RID: 12778
	public int neck;

	// Token: 0x040031EB RID: 12779
	public int eyes;

	// Token: 0x040031EC RID: 12780
	public int hair;

	// Token: 0x040031ED RID: 12781
	public int body;

	// Token: 0x040031EE RID: 12782
	public int belt;

	// Token: 0x040031EF RID: 12783
	public int cuff;

	// Token: 0x040031F0 RID: 12784
	public int foot;

	// Token: 0x040031F1 RID: 12785
	public int hand;

	// Token: 0x040031F2 RID: 12786
	public int pelvis;

	// Token: 0x040031F3 RID: 12787
	public int leg;

	// Token: 0x040031F4 RID: 12788
	public int leg_skin;

	// Token: 0x040031F5 RID: 12789
	public int arm_skin;

	// Token: 0x040031F6 RID: 12790
	public int speech_mouth;

	// Token: 0x040031F7 RID: 12791
	public string nameStringKey;

	// Token: 0x040031F8 RID: 12792
	public string genderStringKey;

	// Token: 0x040031F9 RID: 12793
	public string personalityType;

	// Token: 0x040031FA RID: 12794
	public Tag model;

	// Token: 0x040031FB RID: 12795
	public string stresstrait;

	// Token: 0x040031FC RID: 12796
	public string joyTrait;

	// Token: 0x040031FD RID: 12797
	public string stickerType;

	// Token: 0x040031FE RID: 12798
	public string congenitaltrait;

	// Token: 0x040031FF RID: 12799
	public string unformattedDescription;

	// Token: 0x04003200 RID: 12800
	public string graveStone;

	// Token: 0x04003201 RID: 12801
	public bool startingMinion;

	// Token: 0x04003202 RID: 12802
	public string requiredDlcId;

	// Token: 0x02001AD6 RID: 6870
	public class StartingAttribute
	{
		// Token: 0x0600A561 RID: 42337 RVA: 0x003A8D18 File Offset: 0x003A6F18
		public StartingAttribute(Klei.AI.Attribute attribute, int value)
		{
			this.attribute = attribute;
			this.value = value;
		}

		// Token: 0x04008121 RID: 33057
		public Klei.AI.Attribute attribute;

		// Token: 0x04008122 RID: 33058
		public int value;
	}
}
