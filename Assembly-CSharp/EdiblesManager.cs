using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020008D0 RID: 2256
[AddComponentMenu("KMonoBehaviour/scripts/EdiblesManager")]
public class EdiblesManager : KMonoBehaviour
{
	// Token: 0x06003E9E RID: 16030 RVA: 0x00161348 File Offset: 0x0015F548
	public static List<EdiblesManager.FoodInfo> GetAllLoadedFoodTypes()
	{
		return EdiblesManager.s_allFoodTypes.Where(new Func<EdiblesManager.FoodInfo, bool>(DlcManager.IsCorrectDlcSubscribed)).ToList<EdiblesManager.FoodInfo>();
	}

	// Token: 0x06003E9F RID: 16031 RVA: 0x00161365 File Offset: 0x0015F565
	public static List<EdiblesManager.FoodInfo> GetAllFoodTypes()
	{
		global::Debug.Assert(SaveLoader.Instance != null, "Call GetAllLoadedFoodTypes from the frontend");
		return EdiblesManager.s_allFoodTypes.Where(new Func<EdiblesManager.FoodInfo, bool>(Game.IsCorrectDlcActiveForCurrentSave)).ToList<EdiblesManager.FoodInfo>();
	}

	// Token: 0x06003EA0 RID: 16032 RVA: 0x00161398 File Offset: 0x0015F598
	public static EdiblesManager.FoodInfo GetFoodInfo(string foodID)
	{
		string text = foodID.Replace("Compost", "");
		EdiblesManager.FoodInfo foodInfo = null;
		EdiblesManager.s_allFoodMap.TryGetValue(text, out foodInfo);
		return foodInfo;
	}

	// Token: 0x06003EA1 RID: 16033 RVA: 0x001613C7 File Offset: 0x0015F5C7
	public static bool TryGetFoodInfo(string foodID, out EdiblesManager.FoodInfo info)
	{
		info = null;
		if (string.IsNullOrEmpty(foodID))
		{
			return false;
		}
		info = EdiblesManager.GetFoodInfo(foodID);
		return info != null;
	}

	// Token: 0x04002699 RID: 9881
	private static List<EdiblesManager.FoodInfo> s_allFoodTypes = new List<EdiblesManager.FoodInfo>();

	// Token: 0x0400269A RID: 9882
	private static Dictionary<string, EdiblesManager.FoodInfo> s_allFoodMap = new Dictionary<string, EdiblesManager.FoodInfo>();

	// Token: 0x02001885 RID: 6277
	public class FoodInfo : IConsumableUIItem, IHasDlcRestrictions
	{
		// Token: 0x06009CEE RID: 40174 RVA: 0x00391AD7 File Offset: 0x0038FCD7
		public string[] GetRequiredDlcIds()
		{
			return this.requiredDlcIds;
		}

		// Token: 0x06009CEF RID: 40175 RVA: 0x00391ADF File Offset: 0x0038FCDF
		public string[] GetForbiddenDlcIds()
		{
			return this.forbiddenDlcIds;
		}

		// Token: 0x06009CF0 RID: 40176 RVA: 0x00391AE8 File Offset: 0x0038FCE8
		[Obsolete("Use constructor with required/forbidden instead")]
		public FoodInfo(string id, string dlcId, float caloriesPerUnit, int quality, float preserveTemperatue, float rotTemperature, float spoilTime, bool can_rot)
			: this(id, caloriesPerUnit, quality, preserveTemperatue, rotTemperature, spoilTime, can_rot, null, null)
		{
			if (dlcId != "")
			{
				this.requiredDlcIds = new string[] { dlcId };
			}
		}

		// Token: 0x06009CF1 RID: 40177 RVA: 0x00391B28 File Offset: 0x0038FD28
		public FoodInfo(string id, float caloriesPerUnit, int quality, float preserveTemperatue, float rotTemperature, float spoilTime, bool can_rot, string[] requiredDlcIds = null, string[] forbiddenDlcIds = null)
		{
			this.Id = id;
			this.requiredDlcIds = requiredDlcIds;
			this.forbiddenDlcIds = forbiddenDlcIds;
			this.CaloriesPerUnit = caloriesPerUnit;
			this.Quality = quality;
			this.PreserveTemperature = preserveTemperatue;
			this.RotTemperature = rotTemperature;
			this.StaleTime = spoilTime / 2f;
			this.SpoilTime = spoilTime;
			this.CanRot = can_rot;
			this.Name = Strings.Get("STRINGS.ITEMS.FOOD." + id.ToUpper() + ".NAME");
			this.Description = Strings.Get("STRINGS.ITEMS.FOOD." + id.ToUpper() + ".DESC");
			this.Effects = new List<string>();
			EdiblesManager.s_allFoodTypes.Add(this);
			EdiblesManager.s_allFoodMap[this.Id] = this;
		}

		// Token: 0x06009CF2 RID: 40178 RVA: 0x00391BFF File Offset: 0x0038FDFF
		public EdiblesManager.FoodInfo AddEffects(List<string> effects, string[] requiredDlcIds = null, string[] forbiddenDlcIds = null)
		{
			if (DlcManager.IsCorrectDlcSubscribed(requiredDlcIds, forbiddenDlcIds))
			{
				this.Effects.AddRange(effects);
			}
			return this;
		}

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x06009CF3 RID: 40179 RVA: 0x00391C17 File Offset: 0x0038FE17
		public string ConsumableId
		{
			get
			{
				return this.Id;
			}
		}

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x06009CF4 RID: 40180 RVA: 0x00391C1F File Offset: 0x0038FE1F
		public string ConsumableName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x06009CF5 RID: 40181 RVA: 0x00391C27 File Offset: 0x0038FE27
		public int MajorOrder
		{
			get
			{
				return this.Quality;
			}
		}

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x06009CF6 RID: 40182 RVA: 0x00391C2F File Offset: 0x0038FE2F
		public int MinorOrder
		{
			get
			{
				return (int)this.CaloriesPerUnit;
			}
		}

		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x06009CF7 RID: 40183 RVA: 0x00391C38 File Offset: 0x0038FE38
		public bool Display
		{
			get
			{
				return this.CaloriesPerUnit != 0f;
			}
		}

		// Token: 0x0400791C RID: 31004
		public string Id;

		// Token: 0x0400791D RID: 31005
		public string Name;

		// Token: 0x0400791E RID: 31006
		public string Description;

		// Token: 0x0400791F RID: 31007
		public float CaloriesPerUnit;

		// Token: 0x04007920 RID: 31008
		public float PreserveTemperature;

		// Token: 0x04007921 RID: 31009
		public float RotTemperature;

		// Token: 0x04007922 RID: 31010
		public float StaleTime;

		// Token: 0x04007923 RID: 31011
		public float SpoilTime;

		// Token: 0x04007924 RID: 31012
		public bool CanRot;

		// Token: 0x04007925 RID: 31013
		public int Quality;

		// Token: 0x04007926 RID: 31014
		public List<string> Effects;

		// Token: 0x04007927 RID: 31015
		private string[] requiredDlcIds;

		// Token: 0x04007928 RID: 31016
		private string[] forbiddenDlcIds;
	}
}
