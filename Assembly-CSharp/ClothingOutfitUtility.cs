using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Database;
using Newtonsoft.Json.Linq;
using STRINGS;

// Token: 0x02000810 RID: 2064
public static class ClothingOutfitUtility
{
	// Token: 0x06003856 RID: 14422 RVA: 0x00138DF0 File Offset: 0x00136FF0
	public static string GetName(this ClothingOutfitUtility.OutfitType self)
	{
		switch (self)
		{
		case ClothingOutfitUtility.OutfitType.Clothing:
			return UI.MINION_BROWSER_SCREEN.OUTFIT_TYPE_CLOTHING;
		case ClothingOutfitUtility.OutfitType.JoyResponse:
			return UI.MINION_BROWSER_SCREEN.OUTFIT_TYPE_JOY_RESPONSE;
		case ClothingOutfitUtility.OutfitType.AtmoSuit:
			return UI.MINION_BROWSER_SCREEN.OUTFIT_TYPE_ATMOSUIT;
		default:
			DebugUtil.DevAssert(false, string.Format("Couldn't find name for outfit type: {0}", self), null);
			return self.ToString();
		}
	}

	// Token: 0x06003857 RID: 14423 RVA: 0x00138E58 File Offset: 0x00137058
	public static bool SaveClothingOutfitData()
	{
		if (!Directory.Exists(Util.RootFolder()))
		{
			Directory.CreateDirectory(Util.RootFolder());
		}
		string text = Path.Combine(Util.RootFolder(), Util.GetKleiItemUserDataFolderName());
		if (!Directory.Exists(text))
		{
			Directory.CreateDirectory(text);
		}
		string text2 = Path.Combine(text, ClothingOutfitUtility.OutfitFile_U47_to_Present);
		string text3 = SerializableOutfitData.ToJsonString(SerializableOutfitData.ToJson(CustomClothingOutfits.Instance.Internal_GetOutfitData()));
		return ClothingOutfitUtility.TryWriteTo(text2, text3);
	}

	// Token: 0x06003858 RID: 14424 RVA: 0x00138EC4 File Offset: 0x001370C4
	public static void LoadClothingOutfitData(ClothingOutfits dbClothingOutfits)
	{
		string text = ClothingOutfitUtility.GetPathToJsonFile(ClothingOutfitUtility.OutfitFile_U47_to_Present);
		if (!File.Exists(text))
		{
			text = ClothingOutfitUtility.GetPathToJsonFile(ClothingOutfitUtility.OutfitFile_U44_to_U46);
			if (!File.Exists(text))
			{
				return;
			}
		}
		string text2;
		if (!ClothingOutfitUtility.TryReadFrom(text, out text2))
		{
			return;
		}
		SerializableOutfitData.Version2 version = null;
		try
		{
			version = SerializableOutfitData.FromJson(JObject.Parse(text2));
		}
		catch (Exception ex)
		{
			DebugUtil.DevAssert(false, "ClothingOutfitData Parse failed: " + ex.ToString(), null);
		}
		if (version == null)
		{
			return;
		}
		foreach (KeyValuePair<string, SerializableOutfitData.Version2.CustomTemplateOutfitEntry> keyValuePair in version.OutfitIdToUserAuthoredTemplateOutfit)
		{
			string text3;
			SerializableOutfitData.Version2.CustomTemplateOutfitEntry customTemplateOutfitEntry;
			keyValuePair.Deconstruct(out text3, out customTemplateOutfitEntry);
			string text4 = text3;
			SerializableOutfitData.Version2.CustomTemplateOutfitEntry customTemplateOutfitEntry2 = customTemplateOutfitEntry;
			ClothingOutfitResource clothingOutfitResource = dbClothingOutfits.TryGet(text4);
			if (clothingOutfitResource != null)
			{
				DebugUtil.LogWarningArgs(new object[] { string.Format("UserAuthored outfit with id \"{0}\" of type {1} conflicts with DatabaseAuthored outfit with id \"{2}\" of type {3}. This may result in weird behaviour with outfits.", new object[] { text4, customTemplateOutfitEntry2.outfitType, clothingOutfitResource.Id, clothingOutfitResource.outfitType }) });
			}
		}
		List<string> list = new List<string>();
		foreach (KeyValuePair<string, Dictionary<string, string>> keyValuePair2 in version.PersonalityIdToAssignedOutfits)
		{
			string text3;
			Dictionary<string, string> dictionary;
			keyValuePair2.Deconstruct(out text3, out dictionary);
			string text5 = text3;
			Personality personalityFromNameStringKey = Db.Get().Personalities.GetPersonalityFromNameStringKey(text5);
			if (personalityFromNameStringKey.IsNullOrDestroyed())
			{
				DebugUtil.LogWarningArgs(new object[]
				{
					false,
					"<Loadings Outfit Error> Couldn't find personality \"" + text5 + "\" to apply outfit preferences"
				});
			}
			else if (text5 != personalityFromNameStringKey.Id)
			{
				list.Add(text5);
			}
		}
		foreach (string text6 in list)
		{
			Personality personalityFromNameStringKey2 = Db.Get().Personalities.GetPersonalityFromNameStringKey(text6);
			if (!personalityFromNameStringKey2.IsNullOrDestroyed() && version.PersonalityIdToAssignedOutfits.ContainsKey(text6))
			{
				string id = personalityFromNameStringKey2.Id;
				Dictionary<string, string> dictionary2 = version.PersonalityIdToAssignedOutfits[text6];
				version.PersonalityIdToAssignedOutfits.Remove(text6);
				Dictionary<string, string> dictionary3;
				if (version.PersonalityIdToAssignedOutfits.TryGetValue(id, out dictionary3))
				{
					using (Dictionary<string, string>.Enumerator enumerator4 = dictionary2.GetEnumerator())
					{
						while (enumerator4.MoveNext())
						{
							KeyValuePair<string, string> keyValuePair3 = enumerator4.Current;
							string text3;
							string text7;
							keyValuePair3.Deconstruct(out text3, out text7);
							string text8 = text3;
							string text9 = text7;
							if (!dictionary3.ContainsKey(text8))
							{
								dictionary3[text8] = text9;
							}
						}
						continue;
					}
				}
				version.PersonalityIdToAssignedOutfits.Add(id, dictionary2);
			}
		}
		CustomClothingOutfits.Instance.Internal_SetOutfitData(version);
	}

	// Token: 0x06003859 RID: 14425 RVA: 0x001391BC File Offset: 0x001373BC
	public static string GetPathToJsonFile(string jsonFileName)
	{
		return Path.Combine(Util.RootFolder(), Util.GetKleiItemUserDataFolderName(), jsonFileName);
	}

	// Token: 0x0600385A RID: 14426 RVA: 0x001391D0 File Offset: 0x001373D0
	public static bool TryWriteTo(string path, string data)
	{
		bool flag = false;
		try
		{
			using (FileStream fileStream = File.Open(path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
			{
				byte[] bytes = Encoding.UTF8.GetBytes(data);
				fileStream.Write(bytes, 0, bytes.Length);
				flag = true;
			}
		}
		catch (Exception ex)
		{
			DebugUtil.DevAssert(false, "ClothingOutfitData Write failed: " + ex.ToString(), null);
		}
		return flag;
	}

	// Token: 0x0600385B RID: 14427 RVA: 0x00139248 File Offset: 0x00137448
	public static bool TryReadFrom(string path, out string data)
	{
		data = null;
		bool flag = false;
		try
		{
			using (FileStream fileStream = File.Open(path, FileMode.Open))
			{
				using (StreamReader streamReader = new StreamReader(fileStream, new UTF8Encoding(false, true)))
				{
					data = streamReader.ReadToEnd();
					flag = true;
				}
			}
		}
		catch (Exception ex)
		{
			DebugUtil.DevAssert(false, "ClothingOutfitData Load failed: " + ex.ToString(), null);
		}
		return flag;
	}

	// Token: 0x04002232 RID: 8754
	public static readonly PermitCategory[] PERMIT_CATEGORIES_FOR_CLOTHING = new PermitCategory[]
	{
		PermitCategory.DupeTops,
		PermitCategory.DupeGloves,
		PermitCategory.DupeBottoms,
		PermitCategory.DupeShoes
	};

	// Token: 0x04002233 RID: 8755
	public static readonly PermitCategory[] PERMIT_CATEGORIES_FOR_ATMO_SUITS = new PermitCategory[]
	{
		PermitCategory.AtmoSuitHelmet,
		PermitCategory.AtmoSuitBody,
		PermitCategory.AtmoSuitGloves,
		PermitCategory.AtmoSuitBelt,
		PermitCategory.AtmoSuitShoes
	};

	// Token: 0x04002234 RID: 8756
	private static string OutfitFile_U44_to_U46 = "OutfitUserData.json";

	// Token: 0x04002235 RID: 8757
	private static string OutfitFile_U47_to_Present = "OutfitUserData2.json";

	// Token: 0x0200176E RID: 5998
	public enum OutfitType
	{
		// Token: 0x040075AA RID: 30122
		Clothing,
		// Token: 0x040075AB RID: 30123
		JoyResponse,
		// Token: 0x040075AC RID: 30124
		AtmoSuit,
		// Token: 0x040075AD RID: 30125
		LENGTH
	}
}
