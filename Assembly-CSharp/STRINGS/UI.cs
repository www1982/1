using System;
using System.Collections.Generic;

namespace STRINGS
{
	// Token: 0x02000FA3 RID: 4003
	public class UI
	{
		// Token: 0x06007C60 RID: 31840 RVA: 0x0031E29E File Offset: 0x0031C49E
		public static string FormatAsBuildMenuTab(string text)
		{
			return "<b>" + text + "</b>";
		}

		// Token: 0x06007C61 RID: 31841 RVA: 0x0031E2B0 File Offset: 0x0031C4B0
		public static string FormatAsBuildMenuTab(string text, string hotkey)
		{
			return "<b>" + text + "</b> " + UI.FormatAsHotkey(hotkey);
		}

		// Token: 0x06007C62 RID: 31842 RVA: 0x0031E2C8 File Offset: 0x0031C4C8
		public static string FormatAsBuildMenuTab(string text, global::Action a)
		{
			return "<b>" + text + "</b> " + UI.FormatAsHotKey(a);
		}

		// Token: 0x06007C63 RID: 31843 RVA: 0x0031E2E0 File Offset: 0x0031C4E0
		public static string FormatAsOverlay(string text)
		{
			return "<b>" + text + "</b>";
		}

		// Token: 0x06007C64 RID: 31844 RVA: 0x0031E2F2 File Offset: 0x0031C4F2
		public static string FormatAsOverlay(string text, string hotkey)
		{
			return "<b>" + text + "</b> " + UI.FormatAsHotkey(hotkey);
		}

		// Token: 0x06007C65 RID: 31845 RVA: 0x0031E30A File Offset: 0x0031C50A
		public static string FormatAsOverlay(string text, global::Action a)
		{
			return "<b>" + text + "</b> " + UI.FormatAsHotKey(a);
		}

		// Token: 0x06007C66 RID: 31846 RVA: 0x0031E322 File Offset: 0x0031C522
		public static string FormatAsManagementMenu(string text)
		{
			return "<b>" + text + "</b>";
		}

		// Token: 0x06007C67 RID: 31847 RVA: 0x0031E334 File Offset: 0x0031C534
		public static string FormatAsManagementMenu(string text, string hotkey)
		{
			return "<b>" + text + "</b> " + UI.FormatAsHotkey(hotkey);
		}

		// Token: 0x06007C68 RID: 31848 RVA: 0x0031E34C File Offset: 0x0031C54C
		public static string FormatAsManagementMenu(string text, global::Action a)
		{
			return "<b>" + text + "</b> " + UI.FormatAsHotKey(a);
		}

		// Token: 0x06007C69 RID: 31849 RVA: 0x0031E364 File Offset: 0x0031C564
		public static string FormatAsKeyWord(string text)
		{
			return UI.PRE_KEYWORD + text + UI.PST_KEYWORD;
		}

		// Token: 0x06007C6A RID: 31850 RVA: 0x0031E376 File Offset: 0x0031C576
		public static string FormatAsHotkey(string text)
		{
			return "<b><color=#F44A4A>" + text + "</b></color>";
		}

		// Token: 0x06007C6B RID: 31851 RVA: 0x0031E388 File Offset: 0x0031C588
		public static string FormatAsHotKey(global::Action a)
		{
			return "{Hotkey/" + a.ToString() + "}";
		}

		// Token: 0x06007C6C RID: 31852 RVA: 0x0031E3A6 File Offset: 0x0031C5A6
		public static string FormatAsTool(string text, string hotkey)
		{
			return "<b>" + text + "</b> " + UI.FormatAsHotkey(hotkey);
		}

		// Token: 0x06007C6D RID: 31853 RVA: 0x0031E3BE File Offset: 0x0031C5BE
		public static string FormatAsTool(string text, global::Action a)
		{
			return "<b>" + text + "</b> " + UI.FormatAsHotKey(a);
		}

		// Token: 0x06007C6E RID: 31854 RVA: 0x0031E3D6 File Offset: 0x0031C5D6
		public static string FormatAsLink(string text, string linkID)
		{
			text = UI.StripLinkFormatting(text);
			linkID = CodexCache.FormatLinkID(linkID);
			return string.Concat(new string[] { "<link=\"", linkID, "\">", text, "</link>" });
		}

		// Token: 0x06007C6F RID: 31855 RVA: 0x0031E413 File Offset: 0x0031C613
		public static string FormatAsPositiveModifier(string text)
		{
			return UI.PRE_POS_MODIFIER + text + UI.PST_POS_MODIFIER;
		}

		// Token: 0x06007C70 RID: 31856 RVA: 0x0031E425 File Offset: 0x0031C625
		public static string FormatAsNegativeModifier(string text)
		{
			return UI.PRE_NEG_MODIFIER + text + UI.PST_NEG_MODIFIER;
		}

		// Token: 0x06007C71 RID: 31857 RVA: 0x0031E437 File Offset: 0x0031C637
		public static string FormatAsPositiveRate(string text)
		{
			return UI.PRE_RATE_POSITIVE + text + UI.PST_RATE;
		}

		// Token: 0x06007C72 RID: 31858 RVA: 0x0031E449 File Offset: 0x0031C649
		public static string FormatAsNegativeRate(string text)
		{
			return UI.PRE_RATE_NEGATIVE + text + UI.PST_RATE;
		}

		// Token: 0x06007C73 RID: 31859 RVA: 0x0031E45B File Offset: 0x0031C65B
		public static string CLICK(UI.ClickType c)
		{
			return "(ClickType/" + c.ToString() + ")";
		}

		// Token: 0x06007C74 RID: 31860 RVA: 0x0031E479 File Offset: 0x0031C679
		public static string FormatAsAutomationState(string text, UI.AutomationState state)
		{
			if (state == UI.AutomationState.Active)
			{
				return UI.PRE_AUTOMATION_ACTIVE + text + UI.PST_AUTOMATION;
			}
			return UI.PRE_AUTOMATION_STANDBY + text + UI.PST_AUTOMATION;
		}

		// Token: 0x06007C75 RID: 31861 RVA: 0x0031E49F File Offset: 0x0031C69F
		public static string FormatAsCaps(string text)
		{
			return text.ToUpper();
		}

		// Token: 0x06007C76 RID: 31862 RVA: 0x0031E4A8 File Offset: 0x0031C6A8
		public static string ExtractLinkID(string text)
		{
			string text2 = text;
			int num = text2.IndexOf("<link=");
			if (num != -1)
			{
				int num2 = num + 7;
				int num3 = text2.IndexOf(">") - 1;
				text2 = text.Substring(num2, num3 - num2);
			}
			return text2;
		}

		// Token: 0x06007C77 RID: 31863 RVA: 0x0031E4E8 File Offset: 0x0031C6E8
		public static string StripTagFormatting(string text, string tag)
		{
			string text2 = text;
			try
			{
				string text3 = string.Format("<{0}=", tag);
				string text4 = string.Format("</{0}>", tag);
				int length = text4.Length;
				while (text2.Contains(text3))
				{
					int num = text2.IndexOf(text4);
					if (num > -1)
					{
						text2 = text2.Remove(num, length);
					}
					else
					{
						Debug.LogWarningFormat("String has no closing {0} tag: {1}", new object[] { tag, text });
					}
					int num2 = text2.IndexOf(text3);
					if (num2 != -1)
					{
						int num3 = text2.IndexOf("\">", num2);
						if (num3 != -1)
						{
							text2 = text2.Remove(num2, num3 - num2 + 2);
						}
						else
						{
							text2 = text2.Remove(num2, text3.Length);
							Debug.LogWarningFormat("String has no open {0} closure: {1}", new object[] { tag, text });
						}
					}
					else
					{
						Debug.LogWarningFormat("String has no open {0} tag: {1}", new object[] { tag, text });
					}
				}
			}
			catch
			{
				Debug.LogFormat("STRIP TAG FORMATTING FOR {0} FAILED ON: {1}", new object[] { tag, text });
				text2 = text;
			}
			return text2;
		}

		// Token: 0x06007C78 RID: 31864 RVA: 0x0031E600 File Offset: 0x0031C800
		public static string StripLinkFormatting(string text)
		{
			return UI.StripTagFormatting(UI.StripTagFormatting(text, "link"), "LINK");
		}

		// Token: 0x06007C79 RID: 31865 RVA: 0x0031E617 File Offset: 0x0031C817
		public static string StripStyleFormatting(string text)
		{
			return UI.StripTagFormatting(UI.StripTagFormatting(text, "style"), "STYLE");
		}

		// Token: 0x04005CFB RID: 23803
		public static string PRE_KEYWORD = "<style=\"KKeyword\">";

		// Token: 0x04005CFC RID: 23804
		public static string PST_KEYWORD = "</style>";

		// Token: 0x04005CFD RID: 23805
		public static string PRE_POS_MODIFIER = "<b>";

		// Token: 0x04005CFE RID: 23806
		public static string PST_POS_MODIFIER = "</b>";

		// Token: 0x04005CFF RID: 23807
		public static string PRE_NEG_MODIFIER = "<b>";

		// Token: 0x04005D00 RID: 23808
		public static string PST_NEG_MODIFIER = "</b>";

		// Token: 0x04005D01 RID: 23809
		public static string PRE_RATE_NEGATIVE = "<style=\"consumed\">";

		// Token: 0x04005D02 RID: 23810
		public static string PRE_RATE_POSITIVE = "<style=\"produced\">";

		// Token: 0x04005D03 RID: 23811
		public static string PST_RATE = "</style>";

		// Token: 0x04005D04 RID: 23812
		public static string CODEXLINK = "REQUIREMENTCLASS";

		// Token: 0x04005D05 RID: 23813
		public static string PRE_AUTOMATION_ACTIVE = "<b><style=\"logic_on\">";

		// Token: 0x04005D06 RID: 23814
		public static string PRE_AUTOMATION_STANDBY = "<b><style=\"logic_off\">";

		// Token: 0x04005D07 RID: 23815
		public static string PST_AUTOMATION = "</style></b>";

		// Token: 0x04005D08 RID: 23816
		public static string YELLOW_PREFIX = "<color=#ffff00ff>";

		// Token: 0x04005D09 RID: 23817
		public static string COLOR_SUFFIX = "</color>";

		// Token: 0x04005D0A RID: 23818
		public static string HORIZONTAL_RULE = "------------------";

		// Token: 0x04005D0B RID: 23819
		public static string HORIZONTAL_BR_RULE = "\n" + UI.HORIZONTAL_RULE + "\n";

		// Token: 0x04005D0C RID: 23820
		public static LocString POS_INFINITY = "Infinity";

		// Token: 0x04005D0D RID: 23821
		public static LocString NEG_INFINITY = "-Infinity";

		// Token: 0x04005D0E RID: 23822
		public static LocString PROCEED_BUTTON = "PROCEED";

		// Token: 0x04005D0F RID: 23823
		public static LocString COPY_BUILDING = "Copy";

		// Token: 0x04005D10 RID: 23824
		public static LocString COPY_BUILDING_TOOLTIP = "Create new build orders using the most recent building selection as a template. {Hotkey}";

		// Token: 0x04005D11 RID: 23825
		public static LocString NAME_WITH_UNITS = "{0} x {1}";

		// Token: 0x04005D12 RID: 23826
		public static LocString NA = "N/A";

		// Token: 0x04005D13 RID: 23827
		public static LocString POSITIVE_FORMAT = "+{0}";

		// Token: 0x04005D14 RID: 23828
		public static LocString NEGATIVE_FORMAT = "-{0}";

		// Token: 0x04005D15 RID: 23829
		public static LocString FILTER = "Filter";

		// Token: 0x04005D16 RID: 23830
		public static LocString SPEED_SLOW = "SLOW";

		// Token: 0x04005D17 RID: 23831
		public static LocString SPEED_MEDIUM = "MEDIUM";

		// Token: 0x04005D18 RID: 23832
		public static LocString SPEED_FAST = "FAST";

		// Token: 0x04005D19 RID: 23833
		public static LocString RED_ALERT = "RED ALERT";

		// Token: 0x04005D1A RID: 23834
		public static LocString JOBS = "PRIORITIES";

		// Token: 0x04005D1B RID: 23835
		public static LocString CONSUMABLES = "CONSUMABLES";

		// Token: 0x04005D1C RID: 23836
		public static LocString VITALS = "VITALS";

		// Token: 0x04005D1D RID: 23837
		public static LocString RESEARCH = "RESEARCH";

		// Token: 0x04005D1E RID: 23838
		public static LocString ROLES = "JOB ASSIGNMENTS";

		// Token: 0x04005D1F RID: 23839
		public static LocString RESEARCHPOINTS = "Research points";

		// Token: 0x04005D20 RID: 23840
		public static LocString SCHEDULE = "SCHEDULE";

		// Token: 0x04005D21 RID: 23841
		public static LocString REPORT = "REPORTS";

		// Token: 0x04005D22 RID: 23842
		public static LocString SKILLS = "SKILLS";

		// Token: 0x04005D23 RID: 23843
		public static LocString OVERLAYSTITLE = "OVERLAYS";

		// Token: 0x04005D24 RID: 23844
		public static LocString ALERTS = "ALERTS";

		// Token: 0x04005D25 RID: 23845
		public static LocString MESSAGES = "MESSAGES";

		// Token: 0x04005D26 RID: 23846
		public static LocString ACTIONS = "ACTIONS";

		// Token: 0x04005D27 RID: 23847
		public static LocString QUEUE = "Queue";

		// Token: 0x04005D28 RID: 23848
		public static LocString BASECOUNT = "Base {0}";

		// Token: 0x04005D29 RID: 23849
		public static LocString CHARACTERCONTAINER_SKILLS_TITLE = "ATTRIBUTES";

		// Token: 0x04005D2A RID: 23850
		public static LocString CHARACTERCONTAINER_TRAITS_TITLE = "TRAITS";

		// Token: 0x04005D2B RID: 23851
		public static LocString CHARACTERCONTAINER_TRAITS_TITLE_BIONIC = "BIONIC SYSTEMS";

		// Token: 0x04005D2C RID: 23852
		public static LocString CHARACTERCONTAINER_APTITUDES_TITLE = "INTERESTS";

		// Token: 0x04005D2D RID: 23853
		public static LocString CHARACTERCONTAINER_APTITUDES_TITLE_TOOLTIP = "A Duplicant's starting Attributes are determined by their Interests\n\nLearning Skills related to their Interests will give Duplicants a Morale boost";

		// Token: 0x04005D2E RID: 23854
		public static LocString CHARACTERCONTAINER_EXPECTATIONS_TITLE = "ADDITIONAL INFORMATION";

		// Token: 0x04005D2F RID: 23855
		public static LocString CHARACTERCONTAINER_SKILL_VALUE = " {0} {1}";

		// Token: 0x04005D30 RID: 23856
		public static LocString CHARACTERCONTAINER_NEED = "{0}: {1}";

		// Token: 0x04005D31 RID: 23857
		public static LocString CHARACTERCONTAINER_STRESSTRAIT = "Stress Reaction: {0}";

		// Token: 0x04005D32 RID: 23858
		public static LocString CHARACTERCONTAINER_JOYTRAIT = "Overjoyed Response: {0}";

		// Token: 0x04005D33 RID: 23859
		public static LocString CHARACTERCONTAINER_CONGENITALTRAIT = "Genetic Trait: {0}";

		// Token: 0x04005D34 RID: 23860
		public static LocString CHARACTERCONTAINER_NOARCHETYPESELECTED = "Random";

		// Token: 0x04005D35 RID: 23861
		public static LocString CHARACTERCONTAINER_ARCHETYPESELECT_TOOLTIP = "Change the type of Duplicant the reroll button will produce";

		// Token: 0x04005D36 RID: 23862
		public static LocString CAREPACKAGECONTAINER_INFORMATION_TITLE = "CARE PACKAGE";

		// Token: 0x04005D37 RID: 23863
		public static LocString CHARACTERCONTAINER_ALL_MODELS = "Any";

		// Token: 0x04005D38 RID: 23864
		public static LocString CHARACTERCONTAINER_ATTRIBUTEMODIFIER_INCREASED = "Increased <b>{0}</b>";

		// Token: 0x04005D39 RID: 23865
		public static LocString CHARACTERCONTAINER_ATTRIBUTEMODIFIER_DECREASED = "Decreased <b>{0}</b>";

		// Token: 0x04005D3A RID: 23866
		public static LocString CHARACTERCONTAINER_FILTER_STANDARD = "Check box to allow standard Duplicants";

		// Token: 0x04005D3B RID: 23867
		public static LocString CHARACTERCONTAINER_FILTER_BIONIC = "Check box to allow Bionic Duplicants";

		// Token: 0x04005D3C RID: 23868
		public static LocString PRODUCTINFO_SELECTMATERIAL = "Select {0}:";

		// Token: 0x04005D3D RID: 23869
		public static LocString PRODUCTINFO_RESEARCHREQUIRED = "Research required...";

		// Token: 0x04005D3E RID: 23870
		public static LocString PRODUCTINFO_REQUIRESRESEARCHDESC = "Requires research: {0}";

		// Token: 0x04005D3F RID: 23871
		public static LocString PRODUCTINFO_APPLICABLERESOURCES = "Required resources:";

		// Token: 0x04005D40 RID: 23872
		public static LocString PRODUCTINFO_MISSINGRESOURCES_TITLE = "Requires {0}: {1}";

		// Token: 0x04005D41 RID: 23873
		public static LocString PRODUCTINFO_MISSINGRESOURCES_HOVER = "Missing resources";

		// Token: 0x04005D42 RID: 23874
		public static LocString PRODUCTINFO_MISSINGRESOURCES_DESC = "{0} has yet to be discovered";

		// Token: 0x04005D43 RID: 23875
		public static LocString PRODUCTINFO_UNIQUE_PER_WORLD = "Limit one per " + UI.CLUSTERMAP.PLANETOID_KEYWORD;

		// Token: 0x04005D44 RID: 23876
		public static LocString PRODUCTINFO_ROCKET_INTERIOR = "Rocket interior only";

		// Token: 0x04005D45 RID: 23877
		public static LocString PRODUCTINFO_ROCKET_NOT_INTERIOR = "Cannot build inside rocket";

		// Token: 0x04005D46 RID: 23878
		public static LocString BUILDTOOL_ROTATE = "Rotate this building";

		// Token: 0x04005D47 RID: 23879
		public static LocString BUILDTOOL_ROTATE_CURRENT_DEGREES = "Currently rotated {Degrees} degrees";

		// Token: 0x04005D48 RID: 23880
		public static LocString BUILDTOOL_ROTATE_CURRENT_LEFT = "Currently facing left";

		// Token: 0x04005D49 RID: 23881
		public static LocString BUILDTOOL_ROTATE_CURRENT_RIGHT = "Currently facing right";

		// Token: 0x04005D4A RID: 23882
		public static LocString BUILDTOOL_ROTATE_CURRENT_UP = "Currently facing up";

		// Token: 0x04005D4B RID: 23883
		public static LocString BUILDTOOL_ROTATE_CURRENT_DOWN = "Currently facing down";

		// Token: 0x04005D4C RID: 23884
		public static LocString BUILDTOOL_ROTATE_CURRENT_UPRIGHT = "Currently upright";

		// Token: 0x04005D4D RID: 23885
		public static LocString BUILDTOOL_ROTATE_CURRENT_ON_SIDE = "Currently on its side";

		// Token: 0x04005D4E RID: 23886
		public static LocString BUILDTOOL_CANT_ROTATE = "This building cannot be rotated";

		// Token: 0x04005D4F RID: 23887
		public static LocString EQUIPMENTTAB_OWNED = "Owned Items";

		// Token: 0x04005D50 RID: 23888
		public static LocString EQUIPMENTTAB_HELD = "Held Items";

		// Token: 0x04005D51 RID: 23889
		public static LocString EQUIPMENTTAB_ROOM = "Assigned Rooms";

		// Token: 0x04005D52 RID: 23890
		public static LocString JOBSCREEN_PRIORITY = "Priority";

		// Token: 0x04005D53 RID: 23891
		public static LocString JOBSCREEN_HIGH = "High";

		// Token: 0x04005D54 RID: 23892
		public static LocString JOBSCREEN_LOW = "Low";

		// Token: 0x04005D55 RID: 23893
		public static LocString JOBSCREEN_EVERYONE = "Everyone";

		// Token: 0x04005D56 RID: 23894
		public static LocString JOBSCREEN_DEFAULT = "New Duplicants";

		// Token: 0x04005D57 RID: 23895
		public static LocString BUILD_REQUIRES_SKILL = "Skill: {Skill}";

		// Token: 0x04005D58 RID: 23896
		public static LocString BUILD_REQUIRES_SKILL_TOOLTIP = "At least one Duplicant must have the {Skill} Skill to construct this building";

		// Token: 0x04005D59 RID: 23897
		public static LocString OPERATION_REQUIRES_SKILL = "Skilled Operator: {Skill}";

		// Token: 0x04005D5A RID: 23898
		public static LocString OPERATION_REQUIRES_SKILL_TOOLTIP = "Only a Duplicant with the {Skill} Skill can operate this building";

		// Token: 0x04005D5B RID: 23899
		public static LocString VITALSSCREEN_NAME = "Name";

		// Token: 0x04005D5C RID: 23900
		public static LocString VITALSSCREEN_STRESS = "Stress";

		// Token: 0x04005D5D RID: 23901
		public static LocString VITALSSCREEN_HEALTH = "Health";

		// Token: 0x04005D5E RID: 23902
		public static LocString VITALSSCREEN_SICKNESS = "Disease";

		// Token: 0x04005D5F RID: 23903
		public static LocString VITALSSCREEN_POWERBANKS = "Power";

		// Token: 0x04005D60 RID: 23904
		public static LocString VITALSSCREEN_CALORIES = "Fullness";

		// Token: 0x04005D61 RID: 23905
		public static LocString VITALSSCREEN_RATIONS = "Calories / Cycle";

		// Token: 0x04005D62 RID: 23906
		public static LocString VITALSSCREEN_EATENTODAY = "Eaten Today";

		// Token: 0x04005D63 RID: 23907
		public static LocString VITALSSCREEN_RATIONS_TOOLTIP = "Set how many calories this Duplicant may consume daily";

		// Token: 0x04005D64 RID: 23908
		public static LocString VITALSSCREEN_EATENTODAY_TOOLTIP = "The amount of food this Duplicant has eaten this cycle";

		// Token: 0x04005D65 RID: 23909
		public static LocString VITALSSCREEN_UNTIL_FULL = "Until Full";

		// Token: 0x04005D66 RID: 23910
		public static LocString RESEARCHSCREEN_UNLOCKSTOOLTIP = "Unlocks: {0}";

		// Token: 0x04005D67 RID: 23911
		public static LocString RESEARCHSCREEN_FILTER = "Search Tech";

		// Token: 0x04005D68 RID: 23912
		public static LocString ATTRIBUTELEVEL = "Expertise: Level {0} {1}";

		// Token: 0x04005D69 RID: 23913
		public static LocString ATTRIBUTELEVEL_SHORT = "Level {0} {1}";

		// Token: 0x04005D6A RID: 23914
		public static LocString NEUTRONIUMMASS = "Immeasurable";

		// Token: 0x04005D6B RID: 23915
		public static LocString CALCULATING = "Calculating...";

		// Token: 0x04005D6C RID: 23916
		public static LocString FORMATDAY = "{0:F1} cycles";

		// Token: 0x04005D6D RID: 23917
		public static LocString FORMATSECONDS = "{0}s";

		// Token: 0x04005D6E RID: 23918
		public static LocString DELIVERED = "Delivered: {0} {1}";

		// Token: 0x04005D6F RID: 23919
		public static LocString PICKEDUP = "Picked Up: {0} {1}";

		// Token: 0x04005D70 RID: 23920
		public static LocString COPIED_SETTINGS = "Settings Applied";

		// Token: 0x04005D71 RID: 23921
		public static LocString WELCOMEMESSAGETITLE = "- ALERT -";

		// Token: 0x04005D72 RID: 23922
		public static LocString WELCOMEMESSAGEBODY = "I've awoken at the target location, but colonization efforts have already hit a hitch. I was supposed to land on the planet's surface, but became trapped many miles underground instead.\n\nAlthough the conditions are not ideal, it's imperative that I establish a colony here and begin mounting efforts to escape.";

		// Token: 0x04005D73 RID: 23923
		public static LocString WELCOMEMESSAGEBODY_SPACEDOUT = "The asteroid we call home has collided with an anomalous planet, decimating our colony. Rebuilding it is of the utmost importance.\n\nI've detected a new cluster of material-rich planetoids in nearby space. If I can guide the Duplicants through the perils of space travel, we could build a colony even bigger and better than before.";

		// Token: 0x04005D74 RID: 23924
		public static LocString WELCOMEMESSAGEBODY_KF23 = "This asteroid is oddly tilted, as though a powerful external force once knocked it off its axis.\n\nI'll need to recalibrate my approach to colony-building in order to make the most of this unusual distribution of resources.";

		// Token: 0x04005D75 RID: 23925
		public static LocString WELCOMEMESSAGEBODY_DLC2_CERES = "The ambient temperatures of this planet are inhospitably low.\n\nI've detected the ruins of a scientifically advanced settlement buried deep beneath our landing site.\n\nIf my Duplicants can survive the journey into this frosty planet's core, we could use this newfound technology to build a colony like no other.";

		// Token: 0x04005D76 RID: 23926
		public static LocString WELCOMEMESSAGEBODY_DLC4_PREHISTORIC = "My collision monitoring system has detected an imminent threat to our survival: a huge impactor asteroid is hurtling directly at this planet.\n\nWe must make our way to the surface and mount a defense system in time to destroy the incoming asteroid before it destroys us.";

		// Token: 0x04005D77 RID: 23927
		public static LocString WELCOMEMESSAGEBODY_DLC4_PREHISTORIC_SHATTERED = "Impactor asteroid collision in 10 cycles!\n\nMy scans indicate that the impact will trigger the eruption of all geysers that surround our landing site. There are...so many.\n\nInitiate survival procedures immediately.";

		// Token: 0x04005D78 RID: 23928
		public static LocString WELCOMEMESSAGEBEGIN = "BEGIN";

		// Token: 0x04005D79 RID: 23929
		public static LocString VIEWDUPLICANTS = "Choose a Blueprint";

		// Token: 0x04005D7A RID: 23930
		public static LocString DUPLICANTPRINTING = "Duplicant Printing";

		// Token: 0x04005D7B RID: 23931
		public static LocString ASSIGNDUPLICANT = "Assign Duplicant";

		// Token: 0x04005D7C RID: 23932
		public static LocString CRAFT = "ADD TO QUEUE";

		// Token: 0x04005D7D RID: 23933
		public static LocString CLEAR_COMPLETED = "CLEAR COMPLETED ORDERS";

		// Token: 0x04005D7E RID: 23934
		public static LocString CRAFT_CONTINUOUS = "CONTINUOUS";

		// Token: 0x04005D7F RID: 23935
		public static LocString INCUBATE_CONTINUOUS_TOOLTIP = "When checked, this building will continuously incubate eggs of the selected type";

		// Token: 0x04005D80 RID: 23936
		public static LocString PLACEINRECEPTACLE = "Plant";

		// Token: 0x04005D81 RID: 23937
		public static LocString REMOVEFROMRECEPTACLE = "Uproot";

		// Token: 0x04005D82 RID: 23938
		public static LocString CANCELPLACEINRECEPTACLE = "Cancel";

		// Token: 0x04005D83 RID: 23939
		public static LocString CANCELREMOVALFROMRECEPTACLE = "Cancel";

		// Token: 0x04005D84 RID: 23940
		public static LocString CHANGEPERSECOND = "Change per second: {0}";

		// Token: 0x04005D85 RID: 23941
		public static LocString CHANGEPERCYCLE = "Total change per cycle: {0}";

		// Token: 0x04005D86 RID: 23942
		public static LocString MODIFIER_ITEM_TEMPLATE = "    • {0}: {1}";

		// Token: 0x04005D87 RID: 23943
		public static LocString LISTENTRYSTRING = "     {0}\n";

		// Token: 0x04005D88 RID: 23944
		public static LocString LISTENTRYSTRINGNOLINEBREAK = "     {0}";

		// Token: 0x020023A1 RID: 9121
		public static class PLATFORMS
		{
			// Token: 0x04009EE1 RID: 40673
			public static LocString UNKNOWN = "Your game client";

			// Token: 0x04009EE2 RID: 40674
			public static LocString STEAM = "Steam";

			// Token: 0x04009EE3 RID: 40675
			public static LocString EPIC = "Epic Games Store";

			// Token: 0x04009EE4 RID: 40676
			public static LocString WEGAME = "Wegame";
		}

		// Token: 0x020023A2 RID: 9122
		private enum KeywordType
		{
			// Token: 0x04009EE6 RID: 40678
			Hotkey,
			// Token: 0x04009EE7 RID: 40679
			BuildMenu,
			// Token: 0x04009EE8 RID: 40680
			Attribute,
			// Token: 0x04009EE9 RID: 40681
			Generic
		}

		// Token: 0x020023A3 RID: 9123
		public enum ClickType
		{
			// Token: 0x04009EEB RID: 40683
			Click,
			// Token: 0x04009EEC RID: 40684
			Clicked,
			// Token: 0x04009EED RID: 40685
			Clicking,
			// Token: 0x04009EEE RID: 40686
			Clickable,
			// Token: 0x04009EEF RID: 40687
			Clicks,
			// Token: 0x04009EF0 RID: 40688
			click,
			// Token: 0x04009EF1 RID: 40689
			clicked,
			// Token: 0x04009EF2 RID: 40690
			clicking,
			// Token: 0x04009EF3 RID: 40691
			clickable,
			// Token: 0x04009EF4 RID: 40692
			clicks,
			// Token: 0x04009EF5 RID: 40693
			CLICK,
			// Token: 0x04009EF6 RID: 40694
			CLICKED,
			// Token: 0x04009EF7 RID: 40695
			CLICKING,
			// Token: 0x04009EF8 RID: 40696
			CLICKABLE,
			// Token: 0x04009EF9 RID: 40697
			CLICKS
		}

		// Token: 0x020023A4 RID: 9124
		public enum AutomationState
		{
			// Token: 0x04009EFB RID: 40699
			Active,
			// Token: 0x04009EFC RID: 40700
			Standby
		}

		// Token: 0x020023A5 RID: 9125
		public class VANILLA
		{
			// Token: 0x04009EFD RID: 40701
			public static LocString NAME = "Base Game";

			// Token: 0x04009EFE RID: 40702
			public static LocString NAME_ITAL = "<i>" + UI.VANILLA.NAME + "</i>";
		}

		// Token: 0x020023A6 RID: 9126
		public class DLC1
		{
			// Token: 0x04009EFF RID: 40703
			public static LocString NAME = "Spaced Out!";

			// Token: 0x04009F00 RID: 40704
			public static LocString NAME_ITAL = "<i>" + UI.DLC1.NAME + "</i>";
		}

		// Token: 0x020023A7 RID: 9127
		public class DLC2
		{
			// Token: 0x04009F01 RID: 40705
			public static LocString NAME = "The Frosty Planet Pack";

			// Token: 0x04009F02 RID: 40706
			public static LocString NAME_ITAL = "<i>" + UI.DLC2.NAME + "</i>";

			// Token: 0x04009F03 RID: 40707
			public static LocString MIXING_TOOLTIP = "<b><i>The Frosty Planet Pack</i></b> features frozen biomes and elements useful in thermal regulation";
		}

		// Token: 0x020023A8 RID: 9128
		public class DLC3
		{
			// Token: 0x04009F04 RID: 40708
			public static LocString NAME = "The Bionic Booster Pack";

			// Token: 0x04009F05 RID: 40709
			public static LocString NAME_ITAL = "<i>" + UI.DLC3.NAME + "</i>";

			// Token: 0x04009F06 RID: 40710
			public static LocString MIXING_TOOLTIP = UI.DLC3.NAME_ITAL + " features portable power storage, bionic Duplicants, and remote building operation";
		}

		// Token: 0x020023A9 RID: 9129
		public class DLC4
		{
			// Token: 0x04009F07 RID: 40711
			public static LocString NAME = "The Prehistoric Planet Pack";

			// Token: 0x04009F08 RID: 40712
			public static LocString NAME_ITAL = "<i>" + UI.DLC4.NAME + "</i>";

			// Token: 0x04009F09 RID: 40713
			public static LocString MIXING_TOOLTIP = UI.DLC4.NAME_ITAL + " features carnivorous flora and fauna, biofuel, and a focus on surface defense";
		}

		// Token: 0x020023AA RID: 9130
		public class DIAGNOSTICS_SCREEN
		{
			// Token: 0x04009F0A RID: 40714
			public static LocString TITLE = "Diagnostics";

			// Token: 0x04009F0B RID: 40715
			public static LocString DIAGNOSTIC = "Diagnostic";

			// Token: 0x04009F0C RID: 40716
			public static LocString TOTAL = "Total";

			// Token: 0x04009F0D RID: 40717
			public static LocString RESERVED = "Reserved";

			// Token: 0x04009F0E RID: 40718
			public static LocString STATUS = "Status";

			// Token: 0x04009F0F RID: 40719
			public static LocString SEARCH = "Search";

			// Token: 0x04009F10 RID: 40720
			public static LocString CRITERIA_HEADER_TOOLTIP = "Expand or collapse diagnostic criteria panel";

			// Token: 0x04009F11 RID: 40721
			public static LocString SEE_ALL = "+ See All ({0})";

			// Token: 0x04009F12 RID: 40722
			public static LocString CRITERIA_TOOLTIP = "Toggle the <b>{0}</b> diagnostics evaluation of the <b>{1}</b> criteria";

			// Token: 0x04009F13 RID: 40723
			public static LocString CRITERIA_ENABLED_COUNT = "{0}/{1} criteria enabled";

			// Token: 0x02002C91 RID: 11409
			public class CLICK_TOGGLE_MESSAGE
			{
				// Token: 0x0400C0D1 RID: 49361
				public static LocString ALWAYS = UI.CLICK(UI.ClickType.Click) + " to pin this diagnostic to the sidebar - Current State: <b>Visible On Alert Only</b>";

				// Token: 0x0400C0D2 RID: 49362
				public static LocString ALERT_ONLY = UI.CLICK(UI.ClickType.Click) + " to subscribe to this diagnostic - Current State: <b>Never Visible</b>";

				// Token: 0x0400C0D3 RID: 49363
				public static LocString NEVER = UI.CLICK(UI.ClickType.Click) + " to mute this diagnostic on the sidebar - Current State: <b>Always Visible</b>";

				// Token: 0x0400C0D4 RID: 49364
				public static LocString TUTORIAL_DISABLED = UI.CLICK(UI.ClickType.Click) + " to enable this diagnostic -  Current State: <b>Temporarily disabled</b>";
			}
		}

		// Token: 0x020023AB RID: 9131
		public class TEMPORARY_ACTIONS
		{
			// Token: 0x02002C92 RID: 11410
			public class CAMERA_RETURN
			{
				// Token: 0x0400C0D5 RID: 49365
				public static LocString NAME = "Camera: Return";

				// Token: 0x0400C0D6 RID: 49366
				public static LocString TOOLTIP = "Return camera to its previous position";
			}
		}

		// Token: 0x020023AC RID: 9132
		public class WORLD_SELECTOR_SCREEN
		{
			// Token: 0x04009F14 RID: 40724
			public static LocString TITLE = UI.CLUSTERMAP.PLANETOID;
		}

		// Token: 0x020023AD RID: 9133
		public class COLONY_DIAGNOSTICS
		{
			// Token: 0x04009F15 RID: 40725
			public static LocString NO_MINIONS_PLANETOID = "    • There are no Duplicants on this planetoid";

			// Token: 0x04009F16 RID: 40726
			public static LocString NO_MINIONS_ROCKET = "    • There are no Duplicants aboard this rocket";

			// Token: 0x04009F17 RID: 40727
			public static LocString ROCKET = "rocket";

			// Token: 0x04009F18 RID: 40728
			public static LocString NO_MINIONS_REQUESTED = "    • Crew must be requested to update this diagnostic";

			// Token: 0x04009F19 RID: 40729
			public static LocString NO_DATA = "    • Not enough data for evaluation";

			// Token: 0x04009F1A RID: 40730
			public static LocString NO_DATA_SHORT = "    • No data";

			// Token: 0x04009F1B RID: 40731
			public static LocString MUTE_TUTORIAL = "Diagnostic can be muted in the <b><color=#E5B000>See All</color></b> panel";

			// Token: 0x04009F1C RID: 40732
			public static LocString GENERIC_STATUS_NORMAL = "All values nominal";

			// Token: 0x04009F1D RID: 40733
			public static LocString PLACEHOLDER_CRITERIA_NAME = "Placeholder Criteria Name";

			// Token: 0x04009F1E RID: 40734
			public static LocString GENERIC_CRITERIA_PASS = "Criteria met";

			// Token: 0x04009F1F RID: 40735
			public static LocString GENERIC_CRITERIA_FAIL = "Criteria not met";

			// Token: 0x02002C93 RID: 11411
			public class GENERIC_CRITERIA
			{
				// Token: 0x0400C0D7 RID: 49367
				public static LocString CHECKWORLDHASMINIONS = "Check world has Duplicants";
			}

			// Token: 0x02002C94 RID: 11412
			public class IDLEDIAGNOSTIC
			{
				// Token: 0x0400C0D8 RID: 49368
				public static LocString ALL_NAME = "Idleness";

				// Token: 0x0400C0D9 RID: 49369
				public static LocString TOOLTIP_NAME = "<b>Idleness</b>";

				// Token: 0x0400C0DA RID: 49370
				public static LocString NORMAL = "    • All Duplicants currently have tasks";

				// Token: 0x0400C0DB RID: 49371
				public static LocString IDLE = "    • One or more Duplicants are idle";

				// Token: 0x02003949 RID: 14665
				public static class CRITERIA
				{
					// Token: 0x0400E60A RID: 58890
					public static LocString CHECKIDLE = "Check idle";

					// Token: 0x0400E60B RID: 58891
					public static LocString CHECKIDLESEVERE = "Use high severity idle warning";
				}
			}

			// Token: 0x02002C95 RID: 11413
			public class CHOREGROUPDIAGNOSTIC
			{
				// Token: 0x0400C0DC RID: 49372
				public static LocString ALL_NAME = UI.COLONY_DIAGNOSTICS.ALLCHORESDIAGNOSTIC.ALL_NAME;

				// Token: 0x0200394A RID: 14666
				public static class CRITERIA
				{
				}
			}

			// Token: 0x02002C96 RID: 11414
			public class ALLCHORESDIAGNOSTIC
			{
				// Token: 0x0400C0DD RID: 49373
				public static LocString ALL_NAME = "Errands";

				// Token: 0x0400C0DE RID: 49374
				public static LocString TOOLTIP_NAME = "<b>Errands</b>";

				// Token: 0x0400C0DF RID: 49375
				public static LocString NORMAL = "    • {0} errands pending or in progress";

				// Token: 0x0200394B RID: 14667
				public static class CRITERIA
				{
				}
			}

			// Token: 0x02002C97 RID: 11415
			public class WORKTIMEDIAGNOSTIC
			{
				// Token: 0x0400C0E0 RID: 49376
				public static LocString ALL_NAME = UI.COLONY_DIAGNOSTICS.ALLCHORESDIAGNOSTIC.ALL_NAME;

				// Token: 0x0200394C RID: 14668
				public static class CRITERIA
				{
				}
			}

			// Token: 0x02002C98 RID: 11416
			public class ALLWORKTIMEDIAGNOSTIC
			{
				// Token: 0x0400C0E1 RID: 49377
				public static LocString ALL_NAME = "Work Time";

				// Token: 0x0400C0E2 RID: 49378
				public static LocString TOOLTIP_NAME = "<b>Work Time</b>";

				// Token: 0x0400C0E3 RID: 49379
				public static LocString NORMAL = "    • {0} of Duplicant time spent working";

				// Token: 0x0200394D RID: 14669
				public static class CRITERIA
				{
				}
			}

			// Token: 0x02002C99 RID: 11417
			public class TRAVEL_TIME
			{
				// Token: 0x0400C0E4 RID: 49380
				public static LocString ALL_NAME = "Travel Time";

				// Token: 0x0400C0E5 RID: 49381
				public static LocString TOOLTIP_NAME = "<b>Travel Time</b>";

				// Token: 0x0400C0E6 RID: 49382
				public static LocString NORMAL = "    • {0} of Duplicant time spent traveling between errands";

				// Token: 0x0200394E RID: 14670
				public static class CRITERIA
				{
				}
			}

			// Token: 0x02002C9A RID: 11418
			public class TRAPPEDDUPLICANTDIAGNOSTIC
			{
				// Token: 0x0400C0E7 RID: 49383
				public static LocString ALL_NAME = "Trapped";

				// Token: 0x0400C0E8 RID: 49384
				public static LocString TOOLTIP_NAME = "<b>Trapped</b>";

				// Token: 0x0400C0E9 RID: 49385
				public static LocString NORMAL = "    • No Duplicants are trapped";

				// Token: 0x0400C0EA RID: 49386
				public static LocString STUCK = "    • One or more Duplicants are trapped";

				// Token: 0x0200394F RID: 14671
				public static class CRITERIA
				{
					// Token: 0x0400E60C RID: 58892
					public static LocString CHECKTRAPPED = "Check Trapped";
				}
			}

			// Token: 0x02002C9B RID: 11419
			public class FLOODEDDIAGNOSTIC
			{
				// Token: 0x0400C0EB RID: 49387
				public static LocString ALL_NAME = "Flooded";

				// Token: 0x0400C0EC RID: 49388
				public static LocString TOOLTIP_NAME = "<b>Flooded</b>";

				// Token: 0x0400C0ED RID: 49389
				public static LocString NORMAL = "    • No buildings are flooded";

				// Token: 0x0400C0EE RID: 49390
				public static LocString BUILDING_FLOODED = "    • One or more buildings are flooded";

				// Token: 0x02003950 RID: 14672
				public static class CRITERIA
				{
					// Token: 0x0400E60D RID: 58893
					public static LocString CHECKFLOODED = "Check Flooded";
				}
			}

			// Token: 0x02002C9C RID: 11420
			public class BREATHABILITYDIAGNOSTIC
			{
				// Token: 0x0400C0EF RID: 49391
				public static LocString ALL_NAME = "Breathability";

				// Token: 0x0400C0F0 RID: 49392
				public static LocString TOOLTIP_NAME = "<b>Breathability</b>";

				// Token: 0x0400C0F1 RID: 49393
				public static LocString NORMAL = "    • Oxygen levels are satisfactory";

				// Token: 0x0400C0F2 RID: 49394
				public static LocString POOR = "    • Oxygen is becoming scarce or low pressure";

				// Token: 0x0400C0F3 RID: 49395
				public static LocString SUFFOCATING = "    • One or more Duplicants are suffocating";

				// Token: 0x0400C0F4 RID: 49396
				public static LocString POOR_BIONIC_TANKS = "    • Bionic oxygen tanks are low";

				// Token: 0x0400C0F5 RID: 49397
				public static LocString NEAR_OR_EMPTY_BIONIC_TANKS = "    • Bionic oxygen tanks are critically low";

				// Token: 0x02003951 RID: 14673
				public static class CRITERIA
				{
					// Token: 0x0400E60E RID: 58894
					public static LocString CHECKSUFFOCATION = "Check suffocation";

					// Token: 0x0400E60F RID: 58895
					public static LocString CHECKLOWBREATHABILITY = "Check low breathability";

					// Token: 0x0400E610 RID: 58896
					public static LocString CHECKLOWBIONICOXYGEN = "Check low Bionic Duplicant oxygen tanks";
				}
			}

			// Token: 0x02002C9D RID: 11421
			public class STRESSDIAGNOSTIC
			{
				// Token: 0x0400C0F6 RID: 49398
				public static LocString ALL_NAME = "Max Stress";

				// Token: 0x0400C0F7 RID: 49399
				public static LocString TOOLTIP_NAME = "<b>Max Stress</b>";

				// Token: 0x0400C0F8 RID: 49400
				public static LocString HIGH_STRESS = "    • One or more Duplicants is suffering high stress";

				// Token: 0x0400C0F9 RID: 49401
				public static LocString NORMAL = "    • Duplicants have acceptable stress levels";

				// Token: 0x02003952 RID: 14674
				public static class CRITERIA
				{
					// Token: 0x0400E611 RID: 58897
					public static LocString CHECKSTRESSED = "Check stressed";
				}
			}

			// Token: 0x02002C9E RID: 11422
			public class DECORDIAGNOSTIC
			{
				// Token: 0x0400C0FA RID: 49402
				public static LocString ALL_NAME = "Decor";

				// Token: 0x0400C0FB RID: 49403
				public static LocString TOOLTIP_NAME = "<b>Decor</b>";

				// Token: 0x0400C0FC RID: 49404
				public static LocString LOW = "    • Decor levels are low";

				// Token: 0x0400C0FD RID: 49405
				public static LocString NORMAL = "    • Decor levels are satisfactory";

				// Token: 0x02003953 RID: 14675
				public static class CRITERIA
				{
					// Token: 0x0400E612 RID: 58898
					public static LocString CHECKDECOR = "Check decor";
				}
			}

			// Token: 0x02002C9F RID: 11423
			public class TOILETDIAGNOSTIC
			{
				// Token: 0x0400C0FE RID: 49406
				public static LocString ALL_NAME = "Toilets";

				// Token: 0x0400C0FF RID: 49407
				public static LocString TOOLTIP_NAME = "<b>Toilets</b>";

				// Token: 0x0400C100 RID: 49408
				public static LocString NO_TOILETS = "    • Colony has no toilets";

				// Token: 0x0400C101 RID: 49409
				public static LocString NO_WORKING_TOILETS = "    • Colony has no working toilets";

				// Token: 0x0400C102 RID: 49410
				public static LocString TOILET_URGENT = "    • Duplicants urgently need to use a toilet";

				// Token: 0x0400C103 RID: 49411
				public static LocString FEW_TOILETS = "    • Toilet-to-Duplicant ratio is low";

				// Token: 0x0400C104 RID: 49412
				public static LocString INOPERATIONAL = "    • One or more toilets are out of order";

				// Token: 0x0400C105 RID: 49413
				public static LocString NORMAL = "    • Colony has adequate working toilets";

				// Token: 0x0400C106 RID: 49414
				public static LocString NO_MINIONS_PLANETOID = "    • There are no Duplicants with a bladder on this planetoid";

				// Token: 0x0400C107 RID: 49415
				public static LocString NO_MINIONS_ROCKET = "    • There are no Duplicants with a bladder aboard this rocket";

				// Token: 0x02003954 RID: 14676
				public static class CRITERIA
				{
					// Token: 0x0400E613 RID: 58899
					public static LocString CHECKHASANYTOILETS = "Check has any toilets";

					// Token: 0x0400E614 RID: 58900
					public static LocString CHECKENOUGHTOILETS = "Check enough toilets";

					// Token: 0x0400E615 RID: 58901
					public static LocString CHECKBLADDERS = "Check Duplicants really need to use the toilet";
				}
			}

			// Token: 0x02002CA0 RID: 11424
			public class BEDDIAGNOSTIC
			{
				// Token: 0x0400C108 RID: 49416
				public static LocString ALL_NAME = "Beds";

				// Token: 0x0400C109 RID: 49417
				public static LocString TOOLTIP_NAME = "<b>Beds</b>";

				// Token: 0x0400C10A RID: 49418
				public static LocString NORMAL = "    • Colony has adequate bedding";

				// Token: 0x0400C10B RID: 49419
				public static LocString NOT_ENOUGH_BEDS = "    • One or more Duplicants are missing a bed";

				// Token: 0x0400C10C RID: 49420
				public static LocString MISSING_ASSIGNMENT = "    • One or more Duplicants don't have an assigned bed";

				// Token: 0x0400C10D RID: 49421
				public static LocString CANT_REACH = "    • One or more Duplicants can't reach their bed";

				// Token: 0x0400C10E RID: 49422
				public static LocString NO_MINIONS_PLANETOID = "    • There are no Duplicants on this planetoid who need sleep";

				// Token: 0x0400C10F RID: 49423
				public static LocString NO_MINIONS_ROCKET = "    • There are no Duplicants aboard this rocket who need sleep";

				// Token: 0x02003955 RID: 14677
				public static class CRITERIA
				{
					// Token: 0x0400E616 RID: 58902
					public static LocString CHECKENOUGHBEDS = "Check enough beds";

					// Token: 0x0400E617 RID: 58903
					public static LocString CHECKREACHABILITY = "Check beds are reachable";
				}
			}

			// Token: 0x02002CA1 RID: 11425
			public class FOODDIAGNOSTIC
			{
				// Token: 0x0400C110 RID: 49424
				public static LocString ALL_NAME = "Food";

				// Token: 0x0400C111 RID: 49425
				public static LocString TOOLTIP_NAME = "<b>Food</b>";

				// Token: 0x0400C112 RID: 49426
				public static LocString NORMAL = "    • Food supply is currently adequate";

				// Token: 0x0400C113 RID: 49427
				public static LocString LOW_CALORIES = "    • Food-to-Duplicant ratio is low";

				// Token: 0x0400C114 RID: 49428
				public static LocString HUNGRY = "    • One or more Duplicants are very hungry";

				// Token: 0x0400C115 RID: 49429
				public static LocString NO_FOOD = "    • Duplicants have no food";

				// Token: 0x02003956 RID: 14678
				public class CRITERIA_HAS_FOOD
				{
					// Token: 0x0400E618 RID: 58904
					public static LocString PASS = "    • Duplicants have food";

					// Token: 0x0400E619 RID: 58905
					public static LocString FAIL = "    • Duplicants have no food";
				}

				// Token: 0x02003957 RID: 14679
				public static class CRITERIA
				{
					// Token: 0x0400E61A RID: 58906
					public static LocString CHECKENOUGHFOOD = "Check enough food";

					// Token: 0x0400E61B RID: 58907
					public static LocString CHECKSTARVATION = "Check starvation";
				}
			}

			// Token: 0x02002CA2 RID: 11426
			public class FARMDIAGNOSTIC
			{
				// Token: 0x0400C116 RID: 49430
				public static LocString ALL_NAME = "Crops";

				// Token: 0x0400C117 RID: 49431
				public static LocString TOOLTIP_NAME = "<b>Crops</b>";

				// Token: 0x0400C118 RID: 49432
				public static LocString NORMAL = "    • Crops are being grown in sufficient quantity";

				// Token: 0x0400C119 RID: 49433
				public static LocString NONE = "    • No farm plots";

				// Token: 0x0400C11A RID: 49434
				public static LocString NONE_PLANTED = "    • No crops planted";

				// Token: 0x0400C11B RID: 49435
				public static LocString WILTING = "    • One or more crops are wilting";

				// Token: 0x0400C11C RID: 49436
				public static LocString INOPERATIONAL = "    • One or more farm plots are inoperable";

				// Token: 0x02003958 RID: 14680
				public static class CRITERIA
				{
					// Token: 0x0400E61C RID: 58908
					public static LocString CHECKHASFARMS = "Check colony has farms";

					// Token: 0x0400E61D RID: 58909
					public static LocString CHECKPLANTED = "Check farms are planted";

					// Token: 0x0400E61E RID: 58910
					public static LocString CHECKWILTING = "Check crops wilting";

					// Token: 0x0400E61F RID: 58911
					public static LocString CHECKOPERATIONAL = "Check farm plots operational";
				}
			}

			// Token: 0x02002CA3 RID: 11427
			public class POWERUSEDIAGNOSTIC
			{
				// Token: 0x0400C11D RID: 49437
				public static LocString ALL_NAME = "Power use";

				// Token: 0x0400C11E RID: 49438
				public static LocString TOOLTIP_NAME = "<b>Power use</b>";

				// Token: 0x0400C11F RID: 49439
				public static LocString NORMAL = "    • Power supply is satisfactory";

				// Token: 0x0400C120 RID: 49440
				public static LocString OVERLOADED = "    • One or more power grids are damaged";

				// Token: 0x0400C121 RID: 49441
				public static LocString SIGNIFICANT_POWER_CHANGE_DETECTED = "Significant power use change detected. (Average:{0}, Current:{1})";

				// Token: 0x0400C122 RID: 49442
				public static LocString CIRCUIT_OVER_CAPACITY = "Circuit overloaded {0}/{1}";

				// Token: 0x02003959 RID: 14681
				public static class CRITERIA
				{
					// Token: 0x0400E620 RID: 58912
					public static LocString CHECKOVERWATTAGE = "Check circuit overloaded";

					// Token: 0x0400E621 RID: 58913
					public static LocString CHECKPOWERUSECHANGE = "Check power use change";
				}
			}

			// Token: 0x02002CA4 RID: 11428
			public class HEATDIAGNOSTIC
			{
				// Token: 0x0400C123 RID: 49443
				public static LocString ALL_NAME = UI.COLONY_DIAGNOSTICS.BATTERYDIAGNOSTIC.ALL_NAME;

				// Token: 0x0200395A RID: 14682
				public static class CRITERIA
				{
					// Token: 0x0400E622 RID: 58914
					public static LocString CHECKHEAT = "Check heat";
				}
			}

			// Token: 0x02002CA5 RID: 11429
			public class BATTERYDIAGNOSTIC
			{
				// Token: 0x0400C124 RID: 49444
				public static LocString ALL_NAME = "Battery";

				// Token: 0x0400C125 RID: 49445
				public static LocString TOOLTIP_NAME = "<b>Battery</b>";

				// Token: 0x0400C126 RID: 49446
				public static LocString NORMAL = "    • All batteries functional";

				// Token: 0x0400C127 RID: 49447
				public static LocString NONE = "    • No batteries are connected to a power grid";

				// Token: 0x0400C128 RID: 49448
				public static LocString DEAD_BATTERY = "    • One or more batteries have died";

				// Token: 0x0400C129 RID: 49449
				public static LocString LIMITED_CAPACITY = "    • Low battery capacity relative to power use";

				// Token: 0x0200395B RID: 14683
				public class CRITERIA_CHECK_CAPACITY
				{
					// Token: 0x0400E623 RID: 58915
					public static LocString PASS = "";

					// Token: 0x0400E624 RID: 58916
					public static LocString FAIL = "";
				}

				// Token: 0x0200395C RID: 14684
				public static class CRITERIA
				{
					// Token: 0x0400E625 RID: 58917
					public static LocString CHECKCAPACITY = "Check capacity";

					// Token: 0x0400E626 RID: 58918
					public static LocString CHECKDEAD = "Check dead";
				}
			}

			// Token: 0x02002CA6 RID: 11430
			public class RADIATIONDIAGNOSTIC
			{
				// Token: 0x0400C12A RID: 49450
				public static LocString ALL_NAME = "Radiation";

				// Token: 0x0400C12B RID: 49451
				public static LocString TOOLTIP_NAME = "<b>Radiation</b>";

				// Token: 0x0400C12C RID: 49452
				public static LocString NORMAL = "    • No Radiation concerns";

				// Token: 0x0400C12D RID: 49453
				public static LocString AVERAGE_RADS = "Avg. {0}";

				// Token: 0x0200395D RID: 14685
				public class CRITERIA_RADIATION_SICKNESS
				{
					// Token: 0x0400E627 RID: 58919
					public static LocString PASS = "Healthy";

					// Token: 0x0400E628 RID: 58920
					public static LocString FAIL = "Sick";
				}

				// Token: 0x0200395E RID: 14686
				public class CRITERIA_RADIATION_EXPOSURE
				{
					// Token: 0x0400E629 RID: 58921
					public static LocString PASS = "Safe exposure levels";

					// Token: 0x0400E62A RID: 58922
					public static LocString FAIL_CONCERN = "Exposure levels are above safe limits for one or more Duplicants";

					// Token: 0x0400E62B RID: 58923
					public static LocString FAIL_WARNING = "One or more Duplicants are being exposed to extreme levels of radiation";
				}

				// Token: 0x0200395F RID: 14687
				public static class CRITERIA
				{
					// Token: 0x0400E62C RID: 58924
					public static LocString CHECKSICK = "Check sick";

					// Token: 0x0400E62D RID: 58925
					public static LocString CHECKEXPOSED = "Check exposed";
				}
			}

			// Token: 0x02002CA7 RID: 11431
			public class METEORDIAGNOSTIC
			{
				// Token: 0x0400C12E RID: 49454
				public static LocString ALL_NAME = "Meteor Showers";

				// Token: 0x0400C12F RID: 49455
				public static LocString TOOLTIP_NAME = "<b>Meteor Showers</b>";

				// Token: 0x0400C130 RID: 49456
				public static LocString NORMAL = "    • No meteor showers in progress";

				// Token: 0x0400C131 RID: 49457
				public static LocString SHOWER_UNDERWAY = "    • Meteor bombardment underway! {0} remaining";

				// Token: 0x02003960 RID: 14688
				public static class CRITERIA
				{
					// Token: 0x0400E62E RID: 58926
					public static LocString CHECKUNDERWAY = "Check meteor bombardment";
				}
			}

			// Token: 0x02002CA8 RID: 11432
			public class ENTOMBEDDIAGNOSTIC
			{
				// Token: 0x0400C132 RID: 49458
				public static LocString ALL_NAME = "Entombed";

				// Token: 0x0400C133 RID: 49459
				public static LocString TOOLTIP_NAME = "<b>Entombed</b>";

				// Token: 0x0400C134 RID: 49460
				public static LocString NORMAL = "    • No buildings are entombed";

				// Token: 0x0400C135 RID: 49461
				public static LocString BUILDING_ENTOMBED = "    • One or more buildings are entombed";

				// Token: 0x02003961 RID: 14689
				public static class CRITERIA
				{
					// Token: 0x0400E62F RID: 58927
					public static LocString CHECKENTOMBED = "Check entombed";
				}
			}

			// Token: 0x02002CA9 RID: 11433
			public class ROCKETFUELDIAGNOSTIC
			{
				// Token: 0x0400C136 RID: 49462
				public static LocString ALL_NAME = "Rocket Fuel";

				// Token: 0x0400C137 RID: 49463
				public static LocString TOOLTIP_NAME = "<b>Rocket Fuel</b>";

				// Token: 0x0400C138 RID: 49464
				public static LocString NORMAL = "    • This rocket has sufficient fuel";

				// Token: 0x0400C139 RID: 49465
				public static LocString WARNING = "    • This rocket has no fuel";

				// Token: 0x02003962 RID: 14690
				public static class CRITERIA
				{
				}
			}

			// Token: 0x02002CAA RID: 11434
			public class ROCKETOXIDIZERDIAGNOSTIC
			{
				// Token: 0x0400C13A RID: 49466
				public static LocString ALL_NAME = "Rocket Oxidizer";

				// Token: 0x0400C13B RID: 49467
				public static LocString TOOLTIP_NAME = "<b>Rocket Oxidizer</b>";

				// Token: 0x0400C13C RID: 49468
				public static LocString NORMAL = "    • This rocket has sufficient oxidizer";

				// Token: 0x0400C13D RID: 49469
				public static LocString WARNING = "    • This rocket has insufficient oxidizer";

				// Token: 0x02003963 RID: 14691
				public static class CRITERIA
				{
				}
			}

			// Token: 0x02002CAB RID: 11435
			public class REACTORDIAGNOSTIC
			{
				// Token: 0x0400C13E RID: 49470
				public static LocString ALL_NAME = BUILDINGS.PREFABS.NUCLEARREACTOR.NAME;

				// Token: 0x0400C13F RID: 49471
				public static LocString TOOLTIP_NAME = BUILDINGS.PREFABS.NUCLEARREACTOR.NAME;

				// Token: 0x0400C140 RID: 49472
				public static LocString NORMAL = "    • Safe";

				// Token: 0x0400C141 RID: 49473
				public static LocString CRITERIA_TEMPERATURE_WARNING = "    • Temperature dangerously high";

				// Token: 0x0400C142 RID: 49474
				public static LocString CRITERIA_COOLANT_WARNING = "    • Coolant tank low";

				// Token: 0x02003964 RID: 14692
				public static class CRITERIA
				{
					// Token: 0x0400E630 RID: 58928
					public static LocString CHECKTEMPERATURE = "Check temperature";

					// Token: 0x0400E631 RID: 58929
					public static LocString CHECKCOOLANT = "Check coolant";
				}
			}

			// Token: 0x02002CAC RID: 11436
			public class FLOATINGROCKETDIAGNOSTIC
			{
				// Token: 0x0400C143 RID: 49475
				public static LocString ALL_NAME = "Flight Status";

				// Token: 0x0400C144 RID: 49476
				public static LocString TOOLTIP_NAME = "<b>Flight Status</b>";

				// Token: 0x0400C145 RID: 49477
				public static LocString NORMAL_FLIGHT = "    • This rocket is in flight towards its destination";

				// Token: 0x0400C146 RID: 49478
				public static LocString NORMAL_UTILITY = "    • This rocket is performing a task at its destination";

				// Token: 0x0400C147 RID: 49479
				public static LocString NORMAL_LANDED = "    • This rocket is currently landed on a " + UI.PRE_KEYWORD + "Rocket Platform" + UI.PST_KEYWORD;

				// Token: 0x0400C148 RID: 49480
				public static LocString WARNING_NO_DESTINATION = "    • This rocket is suspended in space with no set destination";

				// Token: 0x0400C149 RID: 49481
				public static LocString WARNING_NO_SPEED = "    • This rocket's flight has been halted";

				// Token: 0x02003965 RID: 14693
				public static class CRITERIA
				{
				}
			}

			// Token: 0x02002CAD RID: 11437
			public class ROCKETINORBITDIAGNOSTIC
			{
				// Token: 0x0400C14A RID: 49482
				public static LocString ALL_NAME = "Rockets in Orbit";

				// Token: 0x0400C14B RID: 49483
				public static LocString TOOLTIP_NAME = "<b>Rockets in Orbit</b>";

				// Token: 0x0400C14C RID: 49484
				public static LocString NORMAL_ONE_IN_ORBIT = "    • {0} is in orbit waiting to land";

				// Token: 0x0400C14D RID: 49485
				public static LocString NORMAL_IN_ORBIT = "    • There are {0} rockets in orbit waiting to land";

				// Token: 0x0400C14E RID: 49486
				public static LocString WARNING_ONE_ROCKETS_STRANDED = "    • No " + BUILDINGS.PREFABS.LAUNCHPAD.NAME + " present. {0} stranded";

				// Token: 0x0400C14F RID: 49487
				public static LocString WARNING_ROCKETS_STRANDED = "    • No " + BUILDINGS.PREFABS.LAUNCHPAD.NAME + " present. {0} rockets stranded";

				// Token: 0x0400C150 RID: 49488
				public static LocString NORMAL_NO_ROCKETS = "    • No rockets waiting to land";

				// Token: 0x02003966 RID: 14694
				public static class CRITERIA
				{
					// Token: 0x0400E632 RID: 58930
					public static LocString CHECKORBIT = "Check Orbiting Rockets";
				}
			}

			// Token: 0x02002CAE RID: 11438
			public class BIONICBATTERYDIAGNOSTIC
			{
				// Token: 0x0400C151 RID: 49489
				public static LocString ALL_NAME = "Bionic Power";

				// Token: 0x0400C152 RID: 49490
				public static LocString TOOLTIP_NAME = "<b>Bionic Power</b>";

				// Token: 0x02003967 RID: 14695
				public class CRITERIA_BATTERIES
				{
					// Token: 0x0400E633 RID: 58931
					public static LocString PASS = "    • " + UI.FormatAsLink("Power Bank", "ELECTROBANK") + " supply is currently adequate";

					// Token: 0x0400E634 RID: 58932
					public static LocString NO_POWERBANKS = "    • Colony has no " + UI.FormatAsLink("Power Banks", "ELECTROBANK") + "\n\nBionic Duplicants are at risk of becoming powerless";

					// Token: 0x0400E635 RID: 58933
					public static LocString LOW_POWERBANKS = "    • " + UI.FormatAsLink("Power Bank", "ELECTROBANK") + " reserves are low:\n\n<indent=20px>    • {0} are currently available</indent>\n<indent=20px>    • {1} are being consumed per cycle</indent>";
				}

				// Token: 0x02003968 RID: 14696
				public class CRITERIA_POWERLEVEL
				{
					// Token: 0x0400E636 RID: 58934
					public static LocString CRITICAL_MODE = "    • One or more Duplicants are in desperate need of " + UI.FormatAsLink("Power Banks", "ELECTROBANK");

					// Token: 0x0400E637 RID: 58935
					public static LocString POWERLESS = "    • One or more Duplicants are incapacitated and in desperate need of " + UI.FormatAsLink("Power Banks", "ELECTROBANK");
				}

				// Token: 0x02003969 RID: 14697
				public static class CRITERIA
				{
					// Token: 0x0400E638 RID: 58936
					public static LocString CHECKENOUGHBATTERIES = "Check enough power banks";

					// Token: 0x0400E639 RID: 58937
					public static LocString CHECKPOWERLEVEL = "Check critical power level";
				}
			}
		}

		// Token: 0x020023AE RID: 9134
		public class SELFCHARGINGBATTERYDIAGNOSTIC
		{
			// Token: 0x04009F20 RID: 40736
			public static LocString ALL_NAME = ITEMS.INDUSTRIAL_PRODUCTS.ELECTROBANK_SELFCHARGING.NAME;

			// Token: 0x04009F21 RID: 40737
			public static LocString TOOLTIP_NAME = ITEMS.INDUSTRIAL_PRODUCTS.ELECTROBANK_SELFCHARGING.NAME;

			// Token: 0x04009F22 RID: 40738
			public static LocString NORMAL = "    • Safe";

			// Token: 0x04009F23 RID: 40739
			public static LocString CRITERIA_BATTERYLIFE_WARNING = "    • End-of-lifespan explosion imminent";

			// Token: 0x02002CAF RID: 11439
			public static class CRITERIA
			{
				// Token: 0x0400C153 RID: 49491
				public static LocString CHECKSELFCHARGINGBATTERYLIFE = "Check lifespan";
			}
		}

		// Token: 0x020023AF RID: 9135
		public class TRACKERS
		{
			// Token: 0x04009F24 RID: 40740
			public static LocString BREATHABILITY = "Breathability";

			// Token: 0x04009F25 RID: 40741
			public static LocString FOOD = "Food";

			// Token: 0x04009F26 RID: 40742
			public static LocString STRESS = "Max Stress";

			// Token: 0x04009F27 RID: 40743
			public static LocString IDLE = "Idle Duplicants";
		}

		// Token: 0x020023B0 RID: 9136
		public class CONTROLS
		{
			// Token: 0x04009F28 RID: 40744
			public static LocString PRESS = "Press";

			// Token: 0x04009F29 RID: 40745
			public static LocString PRESSLOWER = "press";

			// Token: 0x04009F2A RID: 40746
			public static LocString PRESSUPPER = "PRESS";

			// Token: 0x04009F2B RID: 40747
			public static LocString PRESSING = "Pressing";

			// Token: 0x04009F2C RID: 40748
			public static LocString PRESSINGLOWER = "pressing";

			// Token: 0x04009F2D RID: 40749
			public static LocString PRESSINGUPPER = "PRESSING";

			// Token: 0x04009F2E RID: 40750
			public static LocString PRESSED = "Pressed";

			// Token: 0x04009F2F RID: 40751
			public static LocString PRESSEDLOWER = "pressed";

			// Token: 0x04009F30 RID: 40752
			public static LocString PRESSEDUPPER = "PRESSED";

			// Token: 0x04009F31 RID: 40753
			public static LocString PRESSES = "Presses";

			// Token: 0x04009F32 RID: 40754
			public static LocString PRESSESLOWER = "presses";

			// Token: 0x04009F33 RID: 40755
			public static LocString PRESSESUPPER = "PRESSES";

			// Token: 0x04009F34 RID: 40756
			public static LocString PRESSABLE = "Pressable";

			// Token: 0x04009F35 RID: 40757
			public static LocString PRESSABLELOWER = "pressable";

			// Token: 0x04009F36 RID: 40758
			public static LocString PRESSABLEUPPER = "PRESSABLE";

			// Token: 0x04009F37 RID: 40759
			public static LocString CLICK = "Click";

			// Token: 0x04009F38 RID: 40760
			public static LocString CLICKLOWER = "click";

			// Token: 0x04009F39 RID: 40761
			public static LocString CLICKUPPER = "CLICK";

			// Token: 0x04009F3A RID: 40762
			public static LocString CLICKING = "Clicking";

			// Token: 0x04009F3B RID: 40763
			public static LocString CLICKINGLOWER = "clicking";

			// Token: 0x04009F3C RID: 40764
			public static LocString CLICKINGUPPER = "CLICKING";

			// Token: 0x04009F3D RID: 40765
			public static LocString CLICKED = "Clicked";

			// Token: 0x04009F3E RID: 40766
			public static LocString CLICKEDLOWER = "clicked";

			// Token: 0x04009F3F RID: 40767
			public static LocString CLICKEDUPPER = "CLICKED";

			// Token: 0x04009F40 RID: 40768
			public static LocString CLICKS = "Clicks";

			// Token: 0x04009F41 RID: 40769
			public static LocString CLICKSLOWER = "clicks";

			// Token: 0x04009F42 RID: 40770
			public static LocString CLICKSUPPER = "CLICKS";

			// Token: 0x04009F43 RID: 40771
			public static LocString CLICKABLE = "Clickable";

			// Token: 0x04009F44 RID: 40772
			public static LocString CLICKABLELOWER = "clickable";

			// Token: 0x04009F45 RID: 40773
			public static LocString CLICKABLEUPPER = "CLICKABLE";
		}

		// Token: 0x020023B1 RID: 9137
		public class MATH_PICTURES
		{
			// Token: 0x02002CB0 RID: 11440
			public class AXIS_LABELS
			{
				// Token: 0x0400C154 RID: 49492
				public static LocString CYCLES = "Cycles";
			}
		}

		// Token: 0x020023B2 RID: 9138
		public class SPACEDESTINATIONS
		{
			// Token: 0x02002CB1 RID: 11441
			public class WORMHOLE
			{
				// Token: 0x0400C155 RID: 49493
				public static LocString NAME = "Temporal Tear";

				// Token: 0x0400C156 RID: 49494
				public static LocString DESCRIPTION = "The source of our misfortune, though it may also be our shot at freedom. Traces of Neutronium are detectable in my readings.";
			}

			// Token: 0x02002CB2 RID: 11442
			public class RESEARCHDESTINATION
			{
				// Token: 0x0400C157 RID: 49495
				public static LocString NAME = "Alluring Anomaly";

				// Token: 0x0400C158 RID: 49496
				public static LocString DESCRIPTION = "Our researchers would have a field day with this if they could only get close enough.";
			}

			// Token: 0x02002CB3 RID: 11443
			public class DEBRIS
			{
				// Token: 0x0200396A RID: 14698
				public class SATELLITE
				{
					// Token: 0x0400E63A RID: 58938
					public static LocString NAME = "Satellite";

					// Token: 0x0400E63B RID: 58939
					public static LocString DESCRIPTION = "An artificial construct that has escaped its orbit. It no longer appears to be monitored.";
				}
			}

			// Token: 0x02002CB4 RID: 11444
			public class NONE
			{
				// Token: 0x0400C159 RID: 49497
				public static LocString NAME = "Unselected";
			}

			// Token: 0x02002CB5 RID: 11445
			public class ORBIT
			{
				// Token: 0x0400C15A RID: 49498
				public static LocString NAME_FMT = "Orbiting {Name}";
			}

			// Token: 0x02002CB6 RID: 11446
			public class EMPTY_SPACE
			{
				// Token: 0x0400C15B RID: 49499
				public static LocString NAME = "Empty Space";
			}

			// Token: 0x02002CB7 RID: 11447
			public class FOG_OF_WAR_SPACE
			{
				// Token: 0x0400C15C RID: 49500
				public static LocString NAME = "Unexplored Space";
			}

			// Token: 0x02002CB8 RID: 11448
			public class ARTIFACT_POI
			{
				// Token: 0x0200396B RID: 14699
				public class GRAVITASSPACESTATION1
				{
					// Token: 0x0400E63C RID: 58940
					public static LocString NAME = "Destroyed Satellite";

					// Token: 0x0400E63D RID: 58941
					public static LocString DESC = "The remnants of a bygone era, lost in time.\n\nHarvesting space junk requires a rocket equipped with a " + UI.FormatAsLink("Drillcone", "NOSECONEHARVEST") + ".";
				}

				// Token: 0x0200396C RID: 14700
				public class GRAVITASSPACESTATION2
				{
					// Token: 0x0400E63E RID: 58942
					public static LocString NAME = "Demolished Rocket";

					// Token: 0x0400E63F RID: 58943
					public static LocString DESC = "A defunct rocket from a corporation that vanished long ago.\n\nHarvesting space junk requires a rocket equipped with a " + UI.FormatAsLink("Drillcone", "NOSECONEHARVEST") + ".";
				}

				// Token: 0x0200396D RID: 14701
				public class GRAVITASSPACESTATION3
				{
					// Token: 0x0400E640 RID: 58944
					public static LocString NAME = "Ruined Rocket";

					// Token: 0x0400E641 RID: 58945
					public static LocString DESC = "The ruins of a rocket that stopped functioning ages ago.\n\nHarvesting space junk requires a rocket equipped with a " + UI.FormatAsLink("Drillcone", "NOSECONEHARVEST") + ".";
				}

				// Token: 0x0200396E RID: 14702
				public class GRAVITASSPACESTATION4
				{
					// Token: 0x0400E642 RID: 58946
					public static LocString NAME = "Retired Planetary Excursion Module";

					// Token: 0x0400E643 RID: 58947
					public static LocString DESC = "A rocket part from a society that has been wiped out.\n\nHarvesting space junk requires a rocket equipped with a " + UI.FormatAsLink("Drillcone", "NOSECONEHARVEST") + ".";
				}

				// Token: 0x0200396F RID: 14703
				public class GRAVITASSPACESTATION5
				{
					// Token: 0x0400E644 RID: 58948
					public static LocString NAME = "Destroyed Satellite";

					// Token: 0x0400E645 RID: 58949
					public static LocString DESC = "A destroyed Gravitas satellite.\n\nHarvesting space junk requires a rocket equipped with a " + UI.FormatAsLink("Drillcone", "NOSECONEHARVEST") + ".";
				}

				// Token: 0x02003970 RID: 14704
				public class GRAVITASSPACESTATION6
				{
					// Token: 0x0400E646 RID: 58950
					public static LocString NAME = "Annihilated Satellite";

					// Token: 0x0400E647 RID: 58951
					public static LocString DESC = "The remains of a satellite made some time in the past.\n\nHarvesting space junk requires a rocket equipped with a " + UI.FormatAsLink("Drillcone", "NOSECONEHARVEST") + ".";
				}

				// Token: 0x02003971 RID: 14705
				public class GRAVITASSPACESTATION7
				{
					// Token: 0x0400E648 RID: 58952
					public static LocString NAME = "Wrecked Space Shuttle";

					// Token: 0x0400E649 RID: 58953
					public static LocString DESC = "A defunct space shuttle that floats through space unattended.\n\nHarvesting space junk requires a rocket equipped with a " + UI.FormatAsLink("Drillcone", "NOSECONEHARVEST") + ".";
				}

				// Token: 0x02003972 RID: 14706
				public class GRAVITASSPACESTATION8
				{
					// Token: 0x0400E64A RID: 58954
					public static LocString NAME = "Obsolete Space Station Module";

					// Token: 0x0400E64B RID: 58955
					public static LocString DESC = "The module from a space station that ceased to exist ages ago.\n\nHarvesting space junk requires a rocket equipped with a " + UI.FormatAsLink("Drillcone", "NOSECONEHARVEST") + ".";
				}

				// Token: 0x02003973 RID: 14707
				public class RUSSELLSTEAPOT
				{
					// Token: 0x0400E64C RID: 58956
					public static LocString NAME = "Russell's Teapot";

					// Token: 0x0400E64D RID: 58957
					public static LocString DESC = "Has never been disproven to not exist.";
				}
			}

			// Token: 0x02002CB9 RID: 11449
			public class HARVESTABLE_POI
			{
				// Token: 0x0400C15D RID: 49501
				public static LocString POI_PRODUCTION = "{0}";

				// Token: 0x0400C15E RID: 49502
				public static LocString POI_PRODUCTION_TOOLTIP = "{0}";

				// Token: 0x02003974 RID: 14708
				public class CARBONASTEROIDFIELD
				{
					// Token: 0x0400E64E RID: 58958
					public static LocString NAME = "Carbon Asteroid Field";

					// Token: 0x0400E64F RID: 58959
					public static LocString DESC = string.Concat(new string[]
					{
						"An asteroid containing ",
						UI.FormatAsLink("Refined Carbon", "REFINEDCARBON"),
						" and ",
						UI.FormatAsLink("Coal", "CARBON"),
						".\n\nHarvesting resources requires a rocket equipped with a ",
						UI.FormatAsLink("Drillcone", "NOSECONEHARVEST"),
						"."
					});
				}

				// Token: 0x02003975 RID: 14709
				public class METALLICASTEROIDFIELD
				{
					// Token: 0x0400E650 RID: 58960
					public static LocString NAME = "Metallic Asteroid Field";

					// Token: 0x0400E651 RID: 58961
					public static LocString DESC = string.Concat(new string[]
					{
						"An asteroid field containing ",
						UI.FormatAsLink("Iron", "IRON"),
						", ",
						UI.FormatAsLink("Copper", "COPPER"),
						" and ",
						UI.FormatAsLink("Obsidian", "OBSIDIAN"),
						".\n\nHarvesting resources requires a rocket equipped with a ",
						UI.FormatAsLink("Drillcone", "NOSECONEHARVEST"),
						"."
					});
				}

				// Token: 0x02003976 RID: 14710
				public class SATELLITEFIELD
				{
					// Token: 0x0400E652 RID: 58962
					public static LocString NAME = "Space Debris";

					// Token: 0x0400E653 RID: 58963
					public static LocString DESC = "Space junk from a forgotten age.\n\nHarvesting resources requires a rocket equipped with a " + UI.FormatAsLink("Drillcone", "NOSECONEHARVEST") + ".";
				}

				// Token: 0x02003977 RID: 14711
				public class ROCKYASTEROIDFIELD
				{
					// Token: 0x0400E654 RID: 58964
					public static LocString NAME = "Rocky Asteroid Field";

					// Token: 0x0400E655 RID: 58965
					public static LocString DESC = string.Concat(new string[]
					{
						"An asteroid field containing ",
						UI.FormatAsLink("Copper Ore", "CUPRITE"),
						", ",
						UI.FormatAsLink("Sedimentary Rock", "SEDIMENTARYROCK"),
						" and ",
						UI.FormatAsLink("Igneous Rock", "IGNEOUSROCK"),
						".\n\nHarvesting resources requires a rocket equipped with a ",
						UI.FormatAsLink("Drillcone", "NOSECONEHARVEST"),
						"."
					});
				}

				// Token: 0x02003978 RID: 14712
				public class INTERSTELLARICEFIELD
				{
					// Token: 0x0400E656 RID: 58966
					public static LocString NAME = "Ice Asteroid Field";

					// Token: 0x0400E657 RID: 58967
					public static LocString DESC = string.Concat(new string[]
					{
						"An asteroid field containing ",
						UI.FormatAsLink("Ice", "ICE"),
						", ",
						UI.FormatAsLink("Carbon Dioxide", "CARBONDIOXIDE"),
						" and ",
						UI.FormatAsLink("Oxygen", "OXYGEN"),
						".\n\nHarvesting resources requires a rocket equipped with a ",
						UI.FormatAsLink("Drillcone", "NOSECONEHARVEST"),
						"."
					});
				}

				// Token: 0x02003979 RID: 14713
				public class ORGANICMASSFIELD
				{
					// Token: 0x0400E658 RID: 58968
					public static LocString NAME = "Organic Mass Field";

					// Token: 0x0400E659 RID: 58969
					public static LocString DESC = string.Concat(new string[]
					{
						"A mass of harvestable resources containing ",
						UI.FormatAsLink("Algae", "ALGAE"),
						", ",
						UI.FormatAsLink("Slime", "SLIMEMOLD"),
						", ",
						UI.FormatAsLink("Polluted Oxygen", "CONTAMINATEDOXYGEN"),
						" and ",
						UI.FormatAsLink("Dirt", "DIRT"),
						".\n\nHarvesting resources requires a rocket equipped with a ",
						UI.FormatAsLink("Drillcone", "NOSECONEHARVEST"),
						"."
					});
				}

				// Token: 0x0200397A RID: 14714
				public class ICEASTEROIDFIELD
				{
					// Token: 0x0400E65A RID: 58970
					public static LocString NAME = "Exploded Ice Giant";

					// Token: 0x0400E65B RID: 58971
					public static LocString DESC = string.Concat(new string[]
					{
						"A cloud of planetary remains containing ",
						UI.FormatAsLink("Ice", "ICE"),
						", ",
						UI.FormatAsLink("Carbon Dioxide", "CARBONDIOXIDE"),
						", ",
						UI.FormatAsLink("Oxygen", "OXYGEN"),
						" and ",
						UI.FormatAsLink("Natural Gas", "METHANE"),
						".\n\nHarvesting resources requires a rocket equipped with a ",
						UI.FormatAsLink("Drillcone", "NOSECONEHARVEST"),
						"."
					});
				}

				// Token: 0x0200397B RID: 14715
				public class GASGIANTCLOUD
				{
					// Token: 0x0400E65C RID: 58972
					public static LocString NAME = "Exploded Gas Giant";

					// Token: 0x0400E65D RID: 58973
					public static LocString DESC = string.Concat(new string[]
					{
						"The harvestable remains of a planet containing ",
						UI.FormatAsLink("Hydrogen", "HYDROGEN"),
						" in ",
						UI.FormatAsLink("gas", "ELEMENTS_GAS"),
						" form, and ",
						UI.FormatAsLink("Methane", "SOLIDMETHANE"),
						" in ",
						UI.FormatAsLink("solid", "ELEMENTS_SOLID"),
						" and ",
						UI.FormatAsLink("liquid", "ELEMENTS_LIQUID"),
						" form.\n\nHarvesting resources requires a rocket equipped with a ",
						UI.FormatAsLink("Drillcone", "NOSECONEHARVEST"),
						"."
					});
				}

				// Token: 0x0200397C RID: 14716
				public class CHLORINECLOUD
				{
					// Token: 0x0400E65E RID: 58974
					public static LocString NAME = "Chlorine Cloud";

					// Token: 0x0400E65F RID: 58975
					public static LocString DESC = string.Concat(new string[]
					{
						"A cloud of harvestable debris containing ",
						UI.FormatAsLink("Chlorine Gas", "CHLORINEGAS"),
						" and ",
						UI.FormatAsLink("Bleach Stone", "BLEACHSTONE"),
						".\n\nHarvesting resources requires a rocket equipped with a ",
						UI.FormatAsLink("Drillcone", "NOSECONEHARVEST"),
						"."
					});
				}

				// Token: 0x0200397D RID: 14717
				public class GILDEDASTEROIDFIELD
				{
					// Token: 0x0400E660 RID: 58976
					public static LocString NAME = "Gilded Asteroid Field";

					// Token: 0x0400E661 RID: 58977
					public static LocString DESC = string.Concat(new string[]
					{
						"An asteroid field containing ",
						UI.FormatAsLink("Gold", "GOLD"),
						", ",
						UI.FormatAsLink("Fullerene", "FULLERENE"),
						", ",
						UI.FormatAsLink("Regolith", "REGOLITH"),
						" and more.\n\nHarvesting resources requires a rocket equipped with a ",
						UI.FormatAsLink("Drillcone", "NOSECONEHARVEST"),
						"."
					});
				}

				// Token: 0x0200397E RID: 14718
				public class GLIMMERINGASTEROIDFIELD
				{
					// Token: 0x0400E662 RID: 58978
					public static LocString NAME = "Glimmering Asteroid Field";

					// Token: 0x0400E663 RID: 58979
					public static LocString DESC = string.Concat(new string[]
					{
						"An asteroid field containing ",
						UI.FormatAsLink("Tungsten", "TUNGSTEN"),
						", ",
						UI.FormatAsLink("Wolframite", "WOLFRAMITE"),
						" and more.\n\nHarvesting resources requires a rocket equipped with a ",
						UI.FormatAsLink("Drillcone", "NOSECONEHARVEST"),
						"."
					});
				}

				// Token: 0x0200397F RID: 14719
				public class HELIUMCLOUD
				{
					// Token: 0x0400E664 RID: 58980
					public static LocString NAME = "Helium Cloud";

					// Token: 0x0400E665 RID: 58981
					public static LocString DESC = string.Concat(new string[]
					{
						"A cloud of resources containing ",
						UI.FormatAsLink("Water", "WATER"),
						" and ",
						UI.FormatAsLink("Hydrogen Gas", "HYDROGEN"),
						".\n\nHarvesting resources requires a rocket equipped with a ",
						UI.FormatAsLink("Drillcone", "NOSECONEHARVEST"),
						"."
					});
				}

				// Token: 0x02003980 RID: 14720
				public class OILYASTEROIDFIELD
				{
					// Token: 0x0400E666 RID: 58982
					public static LocString NAME = "Oily Asteroid Field";

					// Token: 0x0400E667 RID: 58983
					public static LocString DESC = string.Concat(new string[]
					{
						"An asteroid field containing ",
						UI.FormatAsLink("Solid Methane", "SOLIDMETHANE"),
						", ",
						UI.FormatAsLink("Carbon Dioxide", "CARBONDIOXIDE"),
						" and ",
						UI.FormatAsLink("Crude Oil", "CRUDEOIL"),
						".\n\nHarvesting resources requires a rocket equipped with a ",
						UI.FormatAsLink("Drillcone", "NOSECONEHARVEST"),
						"."
					});
				}

				// Token: 0x02003981 RID: 14721
				public class OXIDIZEDASTEROIDFIELD
				{
					// Token: 0x0400E668 RID: 58984
					public static LocString NAME = "Oxidized Asteroid Field";

					// Token: 0x0400E669 RID: 58985
					public static LocString DESC = string.Concat(new string[]
					{
						"An asteroid field containing ",
						UI.FormatAsLink("Carbon Dioxide", "CARBONDIOXIDE"),
						" and ",
						UI.FormatAsLink("Rust", "RUST"),
						".\n\nHarvesting resources requires a rocket equipped with a ",
						UI.FormatAsLink("Drillcone", "NOSECONEHARVEST"),
						"."
					});
				}

				// Token: 0x02003982 RID: 14722
				public class SALTYASTEROIDFIELD
				{
					// Token: 0x0400E66A RID: 58986
					public static LocString NAME = "Salty Asteroid Field";

					// Token: 0x0400E66B RID: 58987
					public static LocString DESC = string.Concat(new string[]
					{
						"A field of harvestable resources containing ",
						UI.FormatAsLink("Salt Water", "SALTWATER"),
						",",
						UI.FormatAsLink("Brine", "BRINE"),
						" and ",
						UI.FormatAsLink("Carbon Dioxide", "CARBONDIOXIDE"),
						".\n\nHarvesting resources requires a rocket equipped with a ",
						UI.FormatAsLink("Drillcone", "NOSECONEHARVEST"),
						"."
					});
				}

				// Token: 0x02003983 RID: 14723
				public class FROZENOREFIELD
				{
					// Token: 0x0400E66C RID: 58988
					public static LocString NAME = "Frozen Ore Asteroid Field";

					// Token: 0x0400E66D RID: 58989
					public static LocString DESC = string.Concat(new string[]
					{
						"An asteroid field containing ",
						UI.FormatAsLink("Polluted Ice", "DIRTYICE"),
						", ",
						UI.FormatAsLink("Ice", "ICE"),
						", ",
						UI.FormatAsLink("Snow", "SNOW"),
						" and ",
						UI.FormatAsLink("Aluminum Ore", "ALUMINUMORE"),
						".\n\nHarvesting resources requires a rocket equipped with a ",
						UI.FormatAsLink("Drillcone", "NOSECONEHARVEST"),
						"."
					});
				}

				// Token: 0x02003984 RID: 14724
				public class FORESTYOREFIELD
				{
					// Token: 0x0400E66E RID: 58990
					public static LocString NAME = "Forested Ore Field";

					// Token: 0x0400E66F RID: 58991
					public static LocString DESC = string.Concat(new string[]
					{
						"A field of harvestable resources containing ",
						UI.FormatAsLink("Carbon Dioxide", "CARBONDIOXIDE"),
						", ",
						UI.FormatAsLink("Igneous Rock", "IGNEOUSROCK"),
						" and ",
						UI.FormatAsLink("Aluminum Ore", "ALUMINUMORE"),
						".\n\nHarvesting resources requires a rocket equipped with a ",
						UI.FormatAsLink("Drillcone", "NOSECONEHARVEST"),
						"."
					});
				}

				// Token: 0x02003985 RID: 14725
				public class SWAMPYOREFIELD
				{
					// Token: 0x0400E670 RID: 58992
					public static LocString NAME = "Swampy Ore Field";

					// Token: 0x0400E671 RID: 58993
					public static LocString DESC = string.Concat(new string[]
					{
						"An asteroid field containing ",
						UI.FormatAsLink("Mud", "MUD"),
						", ",
						UI.FormatAsLink("Polluted Dirt", "TOXICSAND"),
						" and ",
						UI.FormatAsLink("Cobalt Ore", "COBALTITE"),
						".\n\nHarvesting resources requires a rocket equipped with a ",
						UI.FormatAsLink("Drillcone", "NOSECONEHARVEST"),
						"."
					});
				}

				// Token: 0x02003986 RID: 14726
				public class SANDYOREFIELD
				{
					// Token: 0x0400E672 RID: 58994
					public static LocString NAME = "Sandy Ore Field";

					// Token: 0x0400E673 RID: 58995
					public static LocString DESC = string.Concat(new string[]
					{
						"An asteroid field containing ",
						UI.FormatAsLink("Sandstone", "SANDSTONE"),
						", ",
						UI.FormatAsLink("Algae", "ALGAE"),
						", ",
						UI.FormatAsLink("Copper Ore", "CUPRITE"),
						" and ",
						UI.FormatAsLink("Sand", "SAND"),
						".\n\nHarvesting resources requires a rocket equipped with a ",
						UI.FormatAsLink("Drillcone", "NOSECONEHARVEST"),
						"."
					});
				}

				// Token: 0x02003987 RID: 14727
				public class RADIOACTIVEGASCLOUD
				{
					// Token: 0x0400E674 RID: 58996
					public static LocString NAME = "Radioactive Gas Cloud";

					// Token: 0x0400E675 RID: 58997
					public static LocString DESC = string.Concat(new string[]
					{
						"A cloud of resources containing ",
						UI.FormatAsLink("Chlorine Gas", "CHLORINEGAS"),
						", ",
						UI.FormatAsLink("Uranium Ore", "URANIUMORE"),
						" and ",
						UI.FormatAsLink("Carbon Dioxide", "CARBONDIOXIDE"),
						".\n\nHarvesting resources requires a rocket equipped with a ",
						UI.FormatAsLink("Drillcone", "NOSECONEHARVEST"),
						"."
					});
				}

				// Token: 0x02003988 RID: 14728
				public class RADIOACTIVEASTEROIDFIELD
				{
					// Token: 0x0400E676 RID: 58998
					public static LocString NAME = "Radioactive Asteroid Field";

					// Token: 0x0400E677 RID: 58999
					public static LocString DESC = string.Concat(new string[]
					{
						"An asteroid field containing ",
						UI.FormatAsLink("Bleach Stone", "BLEACHSTONE"),
						", ",
						UI.FormatAsLink("Rust", "RUST"),
						", ",
						UI.FormatAsLink("Uranium Ore", "URANIUMORE"),
						" and ",
						UI.FormatAsLink("Sulfur", "SULFUR"),
						".\n\nHarvesting resources requires a rocket equipped with a ",
						UI.FormatAsLink("Drillcone", "NOSECONEHARVEST"),
						"."
					});
				}

				// Token: 0x02003989 RID: 14729
				public class OXYGENRICHASTEROIDFIELD
				{
					// Token: 0x0400E678 RID: 59000
					public static LocString NAME = "Oxygen Rich Asteroid Field";

					// Token: 0x0400E679 RID: 59001
					public static LocString DESC = string.Concat(new string[]
					{
						"An asteroid field containing ",
						UI.FormatAsLink("Ice", "ICE"),
						", ",
						UI.FormatAsLink("Polluted Oxygen", "CONTAMINATEDOXYGEN"),
						" and ",
						UI.FormatAsLink("Water", "WATER"),
						".\n\nHarvesting resources requires a rocket equipped with a ",
						UI.FormatAsLink("Drillcone", "NOSECONEHARVEST"),
						"."
					});
				}

				// Token: 0x0200398A RID: 14730
				public class INTERSTELLAROCEAN
				{
					// Token: 0x0400E67A RID: 59002
					public static LocString NAME = "Interstellar Ocean";

					// Token: 0x0400E67B RID: 59003
					public static LocString DESC = string.Concat(new string[]
					{
						"An interplanetary body that consists of ",
						UI.FormatAsLink("Salt Water", "SALTWATER"),
						", ",
						UI.FormatAsLink("Brine", "BRINE"),
						", ",
						UI.FormatAsLink("Salt", "SALT"),
						" and ",
						UI.FormatAsLink("Ice", "ICE"),
						".\n\nHarvesting resources requires a rocket equipped with a ",
						UI.FormatAsLink("Drillcone", "NOSECONEHARVEST"),
						"."
					});
				}

				// Token: 0x0200398B RID: 14731
				public class DLC2CERESFIELD
				{
					// Token: 0x0400E67C RID: 59004
					public static LocString NAME = "Frozen Cinnabar Asteroid Field";

					// Token: 0x0400E67D RID: 59005
					public static LocString DESC = string.Concat(new string[]
					{
						"The harvestable remains of a planet containing ",
						UI.FormatAsLink("Cinnabar", "Cinnabar"),
						", ",
						UI.FormatAsLink("Ice", "ICE"),
						" and ",
						UI.FormatAsLink("Mercury", "MERCURY"),
						".\n\nHarvesting resources requires a rocket equipped with a ",
						UI.FormatAsLink("Drillcone", "NOSECONEHARVEST"),
						"."
					});
				}

				// Token: 0x0200398C RID: 14732
				public class DLC2CERESOREFIELD
				{
					// Token: 0x0400E67E RID: 59006
					public static LocString NAME = "Frozen Mercury Asteroid Field";

					// Token: 0x0400E67F RID: 59007
					public static LocString DESC = string.Concat(new string[]
					{
						"An asteroid field containing ",
						UI.FormatAsLink("Cinnabar", "Cinnabar"),
						", ",
						UI.FormatAsLink("Ice", "ICE"),
						" and ",
						UI.FormatAsLink("Mercury", "MERCURY"),
						".\n\nHarvesting resources requires a rocket equipped with a ",
						UI.FormatAsLink("Drillcone", "NOSECONEHARVEST"),
						"."
					});
				}

				// Token: 0x0200398D RID: 14733
				public class DLC4PREHISTORICOREFIELD
				{
					// Token: 0x0400E680 RID: 59008
					public static LocString NAME = "Amber Field";

					// Token: 0x0400E681 RID: 59009
					public static LocString DESC = string.Concat(new string[]
					{
						"The harvestable remains of a planet containing ",
						UI.FormatAsLink("Nickel Ore", "NICKELORE"),
						", ",
						UI.FormatAsLink("Peat", "PEAT"),
						", ",
						UI.FormatAsLink("Amber", "AMBER"),
						" and ",
						UI.FormatAsLink("Shale", "SHALE"),
						".\n\nHarvesting resources requires a rocket equipped with a ",
						UI.FormatAsLink("Drillcone", "NOSECONEHARVEST"),
						"."
					});
				}

				// Token: 0x0200398E RID: 14734
				public class DLC4PREHISTORICMIXINGFIELD
				{
					// Token: 0x0400E682 RID: 59010
					public static LocString NAME = "Conductive Ore Field";

					// Token: 0x0400E683 RID: 59011
					public static LocString DESC = string.Concat(new string[]
					{
						"An asteroid field containing ",
						UI.FormatAsLink("Nickel Ore", "NICKELORE"),
						", ",
						UI.FormatAsLink("Peat", "PEAT"),
						", ",
						UI.FormatAsLink("Amber", "AMBER"),
						" and ",
						UI.FormatAsLink("Shale", "SHALE"),
						".\n\nHarvesting resources requires a rocket equipped with a ",
						UI.FormatAsLink("Drillcone", "NOSECONEHARVEST"),
						"."
					});
				}

				// Token: 0x0200398F RID: 14735
				public class DLC4IMPACTORDEBRISFIELD1
				{
					// Token: 0x0400E684 RID: 59012
					public static LocString NAME = "Demolior Debris";

					// Token: 0x0400E685 RID: 59013
					public static LocString DESC = string.Concat(new string[]
					{
						"The solid harvestable remains of Demolior, containing ",
						UI.FormatAsLink("Iridium", "IRIDIUM"),
						", ",
						UI.FormatAsLink("Mafic Rock", "MAFICROCK"),
						", ",
						UI.FormatAsLink("Gold", "GOLD"),
						", and ",
						UI.FormatAsLink("Granite", "GRANITE"),
						".\n\nHarvesting resources requires a rocket equipped with a ",
						UI.FormatAsLink("Drillcone", "NOSECONEHARVEST"),
						"."
					});
				}

				// Token: 0x02003990 RID: 14736
				public class DLC4IMPACTORDEBRISFIELD2
				{
					// Token: 0x0400E686 RID: 59014
					public static LocString NAME = "Liquid Demolior Debris";

					// Token: 0x0400E687 RID: 59015
					public static LocString DESC = string.Concat(new string[]
					{
						"The harvestable liquid remains of Demolior, containing ",
						UI.FormatAsLink("Isosap", "ISORESIN"),
						", ",
						UI.FormatAsLink("Petroleum", "PETROLEUM"),
						", and ",
						UI.FormatAsLink("Liquid Sulfur", "LIQUIDSULFUR"),
						".\n\nHarvesting resources requires a rocket equipped with a ",
						UI.FormatAsLink("Drillcone", "NOSECONEHARVEST"),
						"."
					});
				}

				// Token: 0x02003991 RID: 14737
				public class DLC4IMPACTORDEBRISFIELD3
				{
					// Token: 0x0400E688 RID: 59016
					public static LocString NAME = "Molten Demolior Debris";

					// Token: 0x0400E689 RID: 59017
					public static LocString DESC = string.Concat(new string[]
					{
						"The harvestable molten remains of Demolior, containing ",
						UI.FormatAsLink("Molten Iridium", "LIQUIDIRIDIUM"),
						", ",
						UI.FormatAsLink("Magma", "MAGMA"),
						", ",
						UI.FormatAsLink("Liquid Oxygen", "LIQUIDOXYGEN"),
						", and ",
						UI.FormatAsLink("Liquid Hydrogen", "LIQUIDHYDROGEN"),
						".\n\nHarvesting resources requires a rocket equipped with a ",
						UI.FormatAsLink("Drillcone", "NOSECONEHARVEST"),
						"."
					});
				}
			}

			// Token: 0x02002CBA RID: 11450
			public class GRAVITAS_SPACE_POI
			{
				// Token: 0x0400C15F RID: 49503
				public static LocString STATION = "Destroyed Gravitas Space Station";
			}

			// Token: 0x02002CBB RID: 11451
			public class TELESCOPE_TARGET
			{
				// Token: 0x0400C160 RID: 49504
				public static LocString NAME = "Telescope Target";
			}

			// Token: 0x02002CBC RID: 11452
			public class ASTEROIDS
			{
				// Token: 0x02003992 RID: 14738
				public class ROCKYASTEROID
				{
					// Token: 0x0400E68A RID: 59018
					public static LocString NAME = "Rocky Asteroid";

					// Token: 0x0400E68B RID: 59019
					public static LocString DESCRIPTION = "A minor mineral planet. Unlike a comet, it does not possess a tail.";
				}

				// Token: 0x02003993 RID: 14739
				public class METALLICASTEROID
				{
					// Token: 0x0400E68C RID: 59020
					public static LocString NAME = "Metallic Asteroid";

					// Token: 0x0400E68D RID: 59021
					public static LocString DESCRIPTION = "A shimmering conglomerate of various metals.";
				}

				// Token: 0x02003994 RID: 14740
				public class CARBONACEOUSASTEROID
				{
					// Token: 0x0400E68E RID: 59022
					public static LocString NAME = "Carbon Asteroid";

					// Token: 0x0400E68F RID: 59023
					public static LocString DESCRIPTION = "A common asteroid containing several useful resources.";
				}

				// Token: 0x02003995 RID: 14741
				public class OILYASTEROID
				{
					// Token: 0x0400E690 RID: 59024
					public static LocString NAME = "Oily Asteroid";

					// Token: 0x0400E691 RID: 59025
					public static LocString DESCRIPTION = "A viscous asteroid that is only loosely held together. Contains fossil fuel resources.";
				}

				// Token: 0x02003996 RID: 14742
				public class GOLDASTEROID
				{
					// Token: 0x0400E692 RID: 59026
					public static LocString NAME = "Gilded Asteroid";

					// Token: 0x0400E693 RID: 59027
					public static LocString DESCRIPTION = "A rich asteroid with thin gold coating and veins of gold deposits throughout.";
				}
			}

			// Token: 0x02002CBD RID: 11453
			public class CLUSTERMAPMETEORSHOWERS
			{
				// Token: 0x02003997 RID: 14743
				public class UNIDENTIFIED
				{
					// Token: 0x0400E694 RID: 59028
					public static LocString NAME = "Unidentified Object";

					// Token: 0x0400E695 RID: 59029
					public static LocString DESCRIPTION = "A cosmic anomaly is traveling through the galaxy.\n\nIts origins and purpose are currently unknown, though a " + BUILDINGS.PREFABS.CLUSTERTELESCOPE.NAME + " could change that.";
				}

				// Token: 0x02003998 RID: 14744
				public class SLIME
				{
					// Token: 0x0400E696 RID: 59030
					public static LocString NAME = "Slimy Meteor Shower";

					// Token: 0x0400E697 RID: 59031
					public static LocString DESCRIPTION = "A shower of slimy, biodynamic meteors on a collision course with the surface of an asteroid.";
				}

				// Token: 0x02003999 RID: 14745
				public class SNOW
				{
					// Token: 0x0400E698 RID: 59032
					public static LocString NAME = "Blizzard Meteor Shower";

					// Token: 0x0400E699 RID: 59033
					public static LocString DESCRIPTION = "A shower of cold, cold meteors on a collision course with the surface of an asteroid.";
				}

				// Token: 0x0200399A RID: 14746
				public class ICE
				{
					// Token: 0x0400E69A RID: 59034
					public static LocString NAME = "Ice Meteor Shower";

					// Token: 0x0400E69B RID: 59035
					public static LocString DESCRIPTION = "A hailstorm of icy space rocks on a collision course with the surface of an asteroid.";
				}

				// Token: 0x0200399B RID: 14747
				public class ICEANDTREES
				{
					// Token: 0x0400E69C RID: 59036
					public static LocString NAME = "Icy Nectar Meteor Shower";

					// Token: 0x0400E69D RID: 59037
					public static LocString DESCRIPTION = "A hailstorm of sweet, icy space rocks on a collision course with the surface of an asteroid";
				}

				// Token: 0x0200399C RID: 14748
				public class COPPER
				{
					// Token: 0x0400E69E RID: 59038
					public static LocString NAME = "Copper Meteor Shower";

					// Token: 0x0400E69F RID: 59039
					public static LocString DESCRIPTION = "A shower of metallic meteors on a collision course with the surface of an asteroid.";
				}

				// Token: 0x0200399D RID: 14749
				public class IRON
				{
					// Token: 0x0400E6A0 RID: 59040
					public static LocString NAME = "Iron Meteor Shower";

					// Token: 0x0400E6A1 RID: 59041
					public static LocString DESCRIPTION = "A shower of metallic space rocks on a collision course with the surface of an asteroid.";
				}

				// Token: 0x0200399E RID: 14750
				public class GOLD
				{
					// Token: 0x0400E6A2 RID: 59042
					public static LocString NAME = "Gold Meteor Shower";

					// Token: 0x0400E6A3 RID: 59043
					public static LocString DESCRIPTION = "A shower of shiny metallic space rocks on a collision course with the surface of an asteroid.";
				}

				// Token: 0x0200399F RID: 14751
				public class URANIUM
				{
					// Token: 0x0400E6A4 RID: 59044
					public static LocString NAME = "Uranium Meteor Shower";

					// Token: 0x0400E6A5 RID: 59045
					public static LocString DESCRIPTION = "A toxic shower of radioactive meteors on a collision course with the surface of an asteroid.";
				}

				// Token: 0x020039A0 RID: 14752
				public class LIGHTDUST
				{
					// Token: 0x0400E6A6 RID: 59046
					public static LocString NAME = "Dust Fluff Meteor Shower";

					// Token: 0x0400E6A7 RID: 59047
					public static LocString DESCRIPTION = "A cloud-like shower of dust fluff meteors heading towards the surface of an asteroid.";
				}

				// Token: 0x020039A1 RID: 14753
				public class HEAVYDUST
				{
					// Token: 0x0400E6A8 RID: 59048
					public static LocString NAME = "Dense Dust Meteor Shower";

					// Token: 0x0400E6A9 RID: 59049
					public static LocString DESCRIPTION = "A dark cloud of heavy dust meteors heading towards the surface of an asteroid.";
				}

				// Token: 0x020039A2 RID: 14754
				public class REGOLITH
				{
					// Token: 0x0400E6AA RID: 59050
					public static LocString NAME = "Regolith Meteor Shower";

					// Token: 0x0400E6AB RID: 59051
					public static LocString DESCRIPTION = "A shower of rocky meteors on a collision course with the surface of an asteroid.";
				}

				// Token: 0x020039A3 RID: 14755
				public class OXYLITE
				{
					// Token: 0x0400E6AC RID: 59052
					public static LocString NAME = "Oxylite Meteor Shower";

					// Token: 0x0400E6AD RID: 59053
					public static LocString DESCRIPTION = "A shower of rocky, oxygen-rich meteors on a collision course with the surface of an asteroid.";
				}

				// Token: 0x020039A4 RID: 14756
				public class BLEACHSTONE
				{
					// Token: 0x0400E6AE RID: 59054
					public static LocString NAME = "Bleach Stone Meteor Shower";

					// Token: 0x0400E6AF RID: 59055
					public static LocString DESCRIPTION = "A shower of bleach stone meteors on a collision course with the surface of an asteroid.";
				}

				// Token: 0x020039A5 RID: 14757
				public class MOO
				{
					// Token: 0x0400E6B0 RID: 59056
					public static LocString NAME = "Gassy Mooteor Shower";

					// Token: 0x0400E6B1 RID: 59057
					public static LocString DESCRIPTION = "A herd of methane-infused meteors that cause a bit of a stink, but do no actual damage.";
				}
			}

			// Token: 0x02002CBE RID: 11454
			public class CLUSTERMAPMETEORS
			{
				// Token: 0x020039A6 RID: 14758
				public class COPPER
				{
					// Token: 0x0400E6B2 RID: 59058
					public static LocString NAME = "Copper Meteor";

					// Token: 0x0400E6B3 RID: 59059
					public static LocString DESCRIPTION = "A shower of metallic meteors on a collision course with the surface of an asteroid.";
				}

				// Token: 0x020039A7 RID: 14759
				public class IRON
				{
					// Token: 0x0400E6B4 RID: 59060
					public static LocString NAME = "Iron Meteor";

					// Token: 0x0400E6B5 RID: 59061
					public static LocString DESCRIPTION = "A shower of metallic space rocks on a collision course with the surface of an asteroid.";
				}

				// Token: 0x020039A8 RID: 14760
				public class LARGEIMACTOR
				{
					// Token: 0x0400E6B6 RID: 59062
					public static LocString NAME = "Demolior";

					// Token: 0x0400E6B7 RID: 59063
					public static LocString DESCRIPTION = "An ancient impactor asteroid on a collision course with the surface of this world.";
				}
			}

			// Token: 0x02002CBF RID: 11455
			public class COMETS
			{
				// Token: 0x020039A9 RID: 14761
				public class ROCKCOMET
				{
					// Token: 0x0400E6B8 RID: 59064
					public static LocString NAME = "Rock Meteor";
				}

				// Token: 0x020039AA RID: 14762
				public class DUSTCOMET
				{
					// Token: 0x0400E6B9 RID: 59065
					public static LocString NAME = "Dust Meteor";
				}

				// Token: 0x020039AB RID: 14763
				public class IRONCOMET
				{
					// Token: 0x0400E6BA RID: 59066
					public static LocString NAME = "Iron Meteor";
				}

				// Token: 0x020039AC RID: 14764
				public class COPPERCOMET
				{
					// Token: 0x0400E6BB RID: 59067
					public static LocString NAME = "Copper Meteor";
				}

				// Token: 0x020039AD RID: 14765
				public class GOLDCOMET
				{
					// Token: 0x0400E6BC RID: 59068
					public static LocString NAME = "Gold Meteor";
				}

				// Token: 0x020039AE RID: 14766
				public class FULLERENECOMET
				{
					// Token: 0x0400E6BD RID: 59069
					public static LocString NAME = "Fullerene Meteor";
				}

				// Token: 0x020039AF RID: 14767
				public class URANIUMORECOMET
				{
					// Token: 0x0400E6BE RID: 59070
					public static LocString NAME = "Uranium Meteor";
				}

				// Token: 0x020039B0 RID: 14768
				public class NUCLEAR_WASTE
				{
					// Token: 0x0400E6BF RID: 59071
					public static LocString NAME = "Radioactive Meteor";
				}

				// Token: 0x020039B1 RID: 14769
				public class SATELLITE
				{
					// Token: 0x0400E6C0 RID: 59072
					public static LocString NAME = "Defunct Satellite";
				}

				// Token: 0x020039B2 RID: 14770
				public class FOODCOMET
				{
					// Token: 0x0400E6C1 RID: 59073
					public static LocString NAME = "Snack Bomb";
				}

				// Token: 0x020039B3 RID: 14771
				public class GASSYMOOCOMET
				{
					// Token: 0x0400E6C2 RID: 59074
					public static LocString NAME = "Gassy Mooteor";
				}

				// Token: 0x020039B4 RID: 14772
				public class SLIMECOMET
				{
					// Token: 0x0400E6C3 RID: 59075
					public static LocString NAME = "Slime Meteor";
				}

				// Token: 0x020039B5 RID: 14773
				public class SNOWBALLCOMET
				{
					// Token: 0x0400E6C4 RID: 59076
					public static LocString NAME = "Snow Meteor";
				}

				// Token: 0x020039B6 RID: 14774
				public class SPACETREESEEDCOMET
				{
					// Token: 0x0400E6C5 RID: 59077
					public static LocString NAME = "Bonbon Meteor";
				}

				// Token: 0x020039B7 RID: 14775
				public class IRIDIUMCOMET
				{
					// Token: 0x0400E6C6 RID: 59078
					public static LocString NAME = "Iridium Meteor";
				}

				// Token: 0x020039B8 RID: 14776
				public class HARDICECOMET
				{
					// Token: 0x0400E6C7 RID: 59079
					public static LocString NAME = "Ice Meteor";
				}

				// Token: 0x020039B9 RID: 14777
				public class LIGHTDUSTCOMET
				{
					// Token: 0x0400E6C8 RID: 59080
					public static LocString NAME = "Dust Fluff Meteor";
				}

				// Token: 0x020039BA RID: 14778
				public class ALGAECOMET
				{
					// Token: 0x0400E6C9 RID: 59081
					public static LocString NAME = "Algae Meteor";
				}

				// Token: 0x020039BB RID: 14779
				public class PHOSPHORICCOMET
				{
					// Token: 0x0400E6CA RID: 59082
					public static LocString NAME = "Phosphoric Meteor";
				}

				// Token: 0x020039BC RID: 14780
				public class OXYLITECOMET
				{
					// Token: 0x0400E6CB RID: 59083
					public static LocString NAME = "Oxylite Meteor";
				}

				// Token: 0x020039BD RID: 14781
				public class BLEACHSTONECOMET
				{
					// Token: 0x0400E6CC RID: 59084
					public static LocString NAME = "Bleach Stone Meteor";
				}

				// Token: 0x020039BE RID: 14782
				public class MINICOMET
				{
					// Token: 0x0400E6CD RID: 59085
					public static LocString NAME = "Debris Projectile";
				}
			}

			// Token: 0x02002CC0 RID: 11456
			public class DWARFPLANETS
			{
				// Token: 0x020039BF RID: 14783
				public class ICYDWARF
				{
					// Token: 0x0400E6CE RID: 59086
					public static LocString NAME = "Interstellar Ice";

					// Token: 0x0400E6CF RID: 59087
					public static LocString DESCRIPTION = "A terrestrial destination, frozen completely solid.";
				}

				// Token: 0x020039C0 RID: 14784
				public class ORGANICDWARF
				{
					// Token: 0x0400E6D0 RID: 59088
					public static LocString NAME = "Organic Mass";

					// Token: 0x0400E6D1 RID: 59089
					public static LocString DESCRIPTION = "A mass of organic material similar to the ooze used to print Duplicants. This sample is heavily degraded.";
				}

				// Token: 0x020039C1 RID: 14785
				public class DUSTYDWARF
				{
					// Token: 0x0400E6D2 RID: 59090
					public static LocString NAME = "Dusty Dwarf";

					// Token: 0x0400E6D3 RID: 59091
					public static LocString DESCRIPTION = "A loosely held together composite of minerals.";
				}

				// Token: 0x020039C2 RID: 14786
				public class SALTDWARF
				{
					// Token: 0x0400E6D4 RID: 59092
					public static LocString NAME = "Salty Dwarf";

					// Token: 0x0400E6D5 RID: 59093
					public static LocString DESCRIPTION = "A dwarf planet with unusually high sodium concentrations.";
				}

				// Token: 0x020039C3 RID: 14787
				public class REDDWARF
				{
					// Token: 0x0400E6D6 RID: 59094
					public static LocString NAME = "Red Dwarf";

					// Token: 0x0400E6D7 RID: 59095
					public static LocString DESCRIPTION = "An M-class star orbited by clusters of extractable aluminum and methane.";
				}
			}

			// Token: 0x02002CC1 RID: 11457
			public class PLANETS
			{
				// Token: 0x020039C4 RID: 14788
				public class TERRAPLANET
				{
					// Token: 0x0400E6D8 RID: 59096
					public static LocString NAME = "Terrestrial Planet";

					// Token: 0x0400E6D9 RID: 59097
					public static LocString DESCRIPTION = "A planet with a walkable surface, though it does not possess the resources to sustain long-term life.";
				}

				// Token: 0x020039C5 RID: 14789
				public class VOLCANOPLANET
				{
					// Token: 0x0400E6DA RID: 59098
					public static LocString NAME = "Volcanic Planet";

					// Token: 0x0400E6DB RID: 59099
					public static LocString DESCRIPTION = "A large terrestrial object composed mainly of molten rock.";
				}

				// Token: 0x020039C6 RID: 14790
				public class SHATTEREDPLANET
				{
					// Token: 0x0400E6DC RID: 59100
					public static LocString NAME = "Shattered Planet";

					// Token: 0x0400E6DD RID: 59101
					public static LocString DESCRIPTION = "A once-habitable planet that has sustained massive damage.\n\nA powerful containment field prevents our rockets from traveling to its surface.";
				}

				// Token: 0x020039C7 RID: 14791
				public class RUSTPLANET
				{
					// Token: 0x0400E6DE RID: 59102
					public static LocString NAME = "Oxidized Asteroid";

					// Token: 0x0400E6DF RID: 59103
					public static LocString DESCRIPTION = "A small planet covered in large swathes of brown rust.";
				}

				// Token: 0x020039C8 RID: 14792
				public class FORESTPLANET
				{
					// Token: 0x0400E6E0 RID: 59104
					public static LocString NAME = "Living Planet";

					// Token: 0x0400E6E1 RID: 59105
					public static LocString DESCRIPTION = "A small green planet displaying several markers of primitive life.";
				}

				// Token: 0x020039C9 RID: 14793
				public class SHINYPLANET
				{
					// Token: 0x0400E6E2 RID: 59106
					public static LocString NAME = "Glimmering Planet";

					// Token: 0x0400E6E3 RID: 59107
					public static LocString DESCRIPTION = "A planet composed of rare, shimmering minerals. From the distance, it looks like gem in the sky.";
				}

				// Token: 0x020039CA RID: 14794
				public class CHLORINEPLANET
				{
					// Token: 0x0400E6E4 RID: 59108
					public static LocString NAME = "Chlorine Planet";

					// Token: 0x0400E6E5 RID: 59109
					public static LocString DESCRIPTION = "A noxious planet permeated by unbreathable chlorine.";
				}

				// Token: 0x020039CB RID: 14795
				public class SALTDESERTPLANET
				{
					// Token: 0x0400E6E6 RID: 59110
					public static LocString NAME = "Arid Planet";

					// Token: 0x0400E6E7 RID: 59111
					public static LocString DESCRIPTION = "A sweltering, desert-like planet covered in surface salt deposits.";
				}

				// Token: 0x020039CC RID: 14796
				public class DLC2CERESSPACEDESTINATION
				{
					// Token: 0x0400E6E8 RID: 59112
					public static LocString NAME = "Ceres";

					// Token: 0x0400E6E9 RID: 59113
					public static LocString DESCRIPTION = "A frozen planet peppered with cinnabar deposits.";
				}

				// Token: 0x020039CD RID: 14797
				public class DLC4PREHISTORICSPACEDESTINATION
				{
					// Token: 0x0400E6EA RID: 59114
					public static LocString NAME = "Prehistoric Ore Field";

					// Token: 0x0400E6EB RID: 59115
					public static LocString DESCRIPTION = "A destination with extractable resources from another era.";
				}

				// Token: 0x020039CE RID: 14798
				public class DLC4PREHISTORICDEMOLIORSPACEDESTINATION
				{
					// Token: 0x0400E6EC RID: 59116
					public static LocString NAME = "Demolior Debris";

					// Token: 0x0400E6ED RID: 59117
					public static LocString DESCRIPTION = "The remains of an obliterated asteroid containing a renewable source of iridium.";
				}

				// Token: 0x020039CF RID: 14799
				public class DLC4PREHISTORICDEMOLIORSPACEDESTINATION2
				{
					// Token: 0x0400E6EE RID: 59118
					public static LocString NAME = "Liquid Demolior Debris";

					// Token: 0x0400E6EF RID: 59119
					public static LocString DESCRIPTION = "The liquid remains of an obliterated asteroid containing a renewable source of Isosap.";
				}

				// Token: 0x020039D0 RID: 14800
				public class DLC4PREHISTORICDEMOLIORSPACEDESTINATION3
				{
					// Token: 0x0400E6F0 RID: 59120
					public static LocString NAME = "Molten Demolior Debris";

					// Token: 0x0400E6F1 RID: 59121
					public static LocString DESCRIPTION = "The hot metallic remains of an obliterated asteroid containing a renewable source of iridium.";
				}
			}

			// Token: 0x02002CC2 RID: 11458
			public class GIANTS
			{
				// Token: 0x020039D1 RID: 14801
				public class GASGIANT
				{
					// Token: 0x0400E6F2 RID: 59122
					public static LocString NAME = "Gas Giant";

					// Token: 0x0400E6F3 RID: 59123
					public static LocString DESCRIPTION = "A massive volume of " + UI.FormatAsLink("Hydrogen Gas", "HYDROGEN") + " formed around a small solid center.";
				}

				// Token: 0x020039D2 RID: 14802
				public class ICEGIANT
				{
					// Token: 0x0400E6F4 RID: 59124
					public static LocString NAME = "Ice Giant";

					// Token: 0x0400E6F5 RID: 59125
					public static LocString DESCRIPTION = "A massive volume of frozen material, primarily composed of " + UI.FormatAsLink("Ice", "ICE") + ".";
				}

				// Token: 0x020039D3 RID: 14803
				public class HYDROGENGIANT
				{
					// Token: 0x0400E6F6 RID: 59126
					public static LocString NAME = "Helium Giant";

					// Token: 0x0400E6F7 RID: 59127
					public static LocString DESCRIPTION = "A massive volume of " + UI.FormatAsLink("Helium", "HELIUM") + " formed around a small solid center.";
				}
			}
		}

		// Token: 0x020023B3 RID: 9139
		public class SPACEARTIFACTS
		{
			// Token: 0x02002CC3 RID: 11459
			public class ARTIFACTTIERS
			{
				// Token: 0x0400C161 RID: 49505
				public static LocString TIER_NONE = "Nothing";

				// Token: 0x0400C162 RID: 49506
				public static LocString TIER0 = "Rarity 0";

				// Token: 0x0400C163 RID: 49507
				public static LocString TIER1 = "Rarity 1";

				// Token: 0x0400C164 RID: 49508
				public static LocString TIER2 = "Rarity 2";

				// Token: 0x0400C165 RID: 49509
				public static LocString TIER3 = "Rarity 3";

				// Token: 0x0400C166 RID: 49510
				public static LocString TIER4 = "Rarity 4";

				// Token: 0x0400C167 RID: 49511
				public static LocString TIER5 = "Rarity 5";
			}

			// Token: 0x02002CC4 RID: 11460
			public class PACUPERCOLATOR
			{
				// Token: 0x0400C168 RID: 49512
				public static LocString NAME = "Percolator";

				// Token: 0x0400C169 RID: 49513
				public static LocString DESCRIPTION = "Don't drink from it! There was a pacu... IN the percolator!";

				// Token: 0x0400C16A RID: 49514
				public static LocString ARTIFACT = "A coffee percolator with the remnants of a blend of coffee that was a personal favorite of Dr. Hassan Aydem.\n\nHe would specifically reserve the consumption of this particular blend for when he was reviewing research papers on Sunday afternoons.";
			}

			// Token: 0x02002CC5 RID: 11461
			public class ROBOTARM
			{
				// Token: 0x0400C16B RID: 49515
				public static LocString NAME = "Robot Arm";

				// Token: 0x0400C16C RID: 49516
				public static LocString DESCRIPTION = "It's not functional. Just cool.";

				// Token: 0x0400C16D RID: 49517
				public static LocString ARTIFACT = "A commercially available robot arm that has had a significant amount of modifications made to it.\n\nThe initials B.A. appear on one of the fingers.";
			}

			// Token: 0x02002CC6 RID: 11462
			public class HATCHFOSSIL
			{
				// Token: 0x0400C16E RID: 49518
				public static LocString NAME = "Pristine Fossil";

				// Token: 0x0400C16F RID: 49519
				public static LocString DESCRIPTION = "The preserved bones of an early species of Hatch.";

				// Token: 0x0400C170 RID: 49520
				public static LocString ARTIFACT = "The preservation of this skeleton occurred artificially using a technique called the \"The Ali Method\".\n\nIt should be noted that this fossilization technique was pioneered by one Dr. Ashkan Seyed Ali, an employee of Gravitas.";
			}

			// Token: 0x02002CC7 RID: 11463
			public class MODERNART
			{
				// Token: 0x0400C171 RID: 49521
				public static LocString NAME = "Modern Art";

				// Token: 0x0400C172 RID: 49522
				public static LocString DESCRIPTION = "I don't get it.";

				// Token: 0x0400C173 RID: 49523
				public static LocString ARTIFACT = "A sculpture of the Neoplastism movement of Modern Art.\n\nGravitas records show that this piece was once used in a presentation called 'Form and Function in Corporate Aesthetic'.";
			}

			// Token: 0x02002CC8 RID: 11464
			public class EGGROCK
			{
				// Token: 0x0400C174 RID: 49524
				public static LocString NAME = "Egg-Shaped Rock";

				// Token: 0x0400C175 RID: 49525
				public static LocString DESCRIPTION = "It's unclear whether this is its naturally occurring shape, or if its appearance as been sculpted.";

				// Token: 0x0400C176 RID: 49526
				public static LocString ARTIFACT = "The words \"Happy Farters Day Dad. Love Macy\" appear on the bottom of this rock, written in a childlish scrawl.";
			}

			// Token: 0x02002CC9 RID: 11465
			public class RAINBOWEGGROCK
			{
				// Token: 0x0400C177 RID: 49527
				public static LocString NAME = "Egg-Shaped Rock";

				// Token: 0x0400C178 RID: 49528
				public static LocString DESCRIPTION = "It's unclear whether this is its naturally occurring shape, or if its appearance as been sculpted.\n\nThis one is rainbow colored.";

				// Token: 0x0400C179 RID: 49529
				public static LocString ARTIFACT = "The words \"Happy Father's Day, Dad. Love you!\" appear on the bottom of this rock, written in very neat handwriting. The words are surrounded by four hearts drawn in what appears to be a pink gel pen.";
			}

			// Token: 0x02002CCA RID: 11466
			public class OKAYXRAY
			{
				// Token: 0x0400C17A RID: 49530
				public static LocString NAME = "Old X-Ray";

				// Token: 0x0400C17B RID: 49531
				public static LocString DESCRIPTION = "Ew, weird. It has five fingers!";

				// Token: 0x0400C17C RID: 49532
				public static LocString ARTIFACT = "The description on this X-ray indicates that it was taken in the Gravitas Medical Facility.\n\nMost likely this X-ray was performed while investigating an injury that occurred within the facility.";
			}

			// Token: 0x02002CCB RID: 11467
			public class SHIELDGENERATOR
			{
				// Token: 0x0400C17D RID: 49533
				public static LocString NAME = "Shield Generator";

				// Token: 0x0400C17E RID: 49534
				public static LocString DESCRIPTION = "A mechanical prototype capable of producing a small section of shielding.";

				// Token: 0x0400C17F RID: 49535
				public static LocString ARTIFACT = "The energy field produced by this shield generator completely ignores those light behaviors which are wave-like and focuses instead on its particle behaviors.\n\nThis seemingly paradoxical state is possible when light is slowed down to the point at which it stops entirely.";
			}

			// Token: 0x02002CCC RID: 11468
			public class TEAPOT
			{
				// Token: 0x0400C180 RID: 49536
				public static LocString NAME = "Encrusted Teapot";

				// Token: 0x0400C181 RID: 49537
				public static LocString DESCRIPTION = "A teapot from the depths of space, coated in a thick layer of Neutronium.";

				// Token: 0x0400C182 RID: 49538
				public static LocString ARTIFACT = "The amount of Neutronium present in this teapot suggests that it has crossed the threshold of the spacetime continuum on countless occasions, floating through many multiple universes over a plethora of times and spaces.\n\nThough there are, theoretically, an infinite amount of outcomes to any one event over many multi-verses, the homogeneity of the still relatively young multiverse suggests that this is then not the only teapot which has crossed into multiple universes. Despite the infinite possible outcomes of infinite multiverses it appears one high probability constant is that there is, or once was, a teapot floating somewhere in space within every universe.";
			}

			// Token: 0x02002CCD RID: 11469
			public class DNAMODEL
			{
				// Token: 0x0400C183 RID: 49539
				public static LocString NAME = "Double Helix Model";

				// Token: 0x0400C184 RID: 49540
				public static LocString DESCRIPTION = "An educational model of genetic information.";

				// Token: 0x0400C185 RID: 49541
				public static LocString ARTIFACT = "A physical representation of the building blocks of life.\n\nThis one contains trace amounts of a Genetic Ooze prototype that was once used by Gravitas.";
			}

			// Token: 0x02002CCE RID: 11470
			public class SANDSTONE
			{
				// Token: 0x0400C186 RID: 49542
				public static LocString NAME = "Sandstone";

				// Token: 0x0400C187 RID: 49543
				public static LocString DESCRIPTION = "A beautiful rock composed of multiple layers of sediment.";

				// Token: 0x0400C188 RID: 49544
				public static LocString ARTIFACT = "This sample of sandstone appears to have been processed by the Gravitas Mining Gun that was made available to the general public.\n\nNote: The Gravitas public Mining Gun model is different than ones used by Duplicants in its larger size, and extra precautionary features added in order to be compliant with national safety standards.";
			}

			// Token: 0x02002CCF RID: 11471
			public class MAGMALAMP
			{
				// Token: 0x0400C189 RID: 49545
				public static LocString NAME = "Magma Lamp";

				// Token: 0x0400C18A RID: 49546
				public static LocString DESCRIPTION = "The sequel to \"Lava Lamp\".";

				// Token: 0x0400C18B RID: 49547
				public static LocString ARTIFACT = "Molten lava and obsidian combined in a way that allows the lava to maintain just enough heat to remain in liquid form.\n\nPlans of this lamp found in the Gravitas archives have been attributed to one Robin Nisbet, PhD.";
			}

			// Token: 0x02002CD0 RID: 11472
			public class OBELISK
			{
				// Token: 0x0400C18C RID: 49548
				public static LocString NAME = "Small Obelisk";

				// Token: 0x0400C18D RID: 49549
				public static LocString DESCRIPTION = "A rectangular stone piece.\n\nIts function is unclear.";

				// Token: 0x0400C18E RID: 49550
				public static LocString ARTIFACT = "On close inspection this rectangle is actually a stone box built with a covert, almost seamless, lid, housing a tiny key.\n\nIt is still unclear what the key unlocks.";
			}

			// Token: 0x02002CD1 RID: 11473
			public class RUBIKSCUBE
			{
				// Token: 0x0400C18F RID: 49551
				public static LocString NAME = "Rubik's Cube";

				// Token: 0x0400C190 RID: 49552
				public static LocString DESCRIPTION = "This mystery of the universe has already been solved.";

				// Token: 0x0400C191 RID: 49553
				public static LocString ARTIFACT = "A well-used, competition-compliant version of the popular puzzle cube.\n\nIt's worth noting that Dr. Dylan 'Nails' Winslow was once a regional Rubik's Cube champion.";
			}

			// Token: 0x02002CD2 RID: 11474
			public class OFFICEMUG
			{
				// Token: 0x0400C192 RID: 49554
				public static LocString NAME = "Office Mug";

				// Token: 0x0400C193 RID: 49555
				public static LocString DESCRIPTION = "An intermediary place to store espresso before you move it to your mouth.";

				// Token: 0x0400C194 RID: 49556
				public static LocString ARTIFACT = "An office mug with the Gravitas logo on it. Though their office mugs were all emblazoned with the same logo, Gravitas colored their mugs differently to distinguish between their various departments.\n\nThis one is from the AI department.";
			}

			// Token: 0x02002CD3 RID: 11475
			public class AMELIASWATCH
			{
				// Token: 0x0400C195 RID: 49557
				public static LocString NAME = "Wrist Watch";

				// Token: 0x0400C196 RID: 49558
				public static LocString DESCRIPTION = "It was discovered in a package labeled \"To be entrusted to Dr. Walker\".";

				// Token: 0x0400C197 RID: 49559
				public static LocString ARTIFACT = "This watch once belonged to pioneering aviator Amelia Earhart and travelled to space via astronaut Dr. Shannon Walker.\n\nHow it came to be floating in space is a matter of speculation, but perhaps the adventurous spirit of its original stewards became infused within the fabric of this timepiece and compelled the universe to launch it into the great unknown.";
			}

			// Token: 0x02002CD4 RID: 11476
			public class MOONMOONMOON
			{
				// Token: 0x0400C198 RID: 49560
				public static LocString NAME = "Moonmoonmoon";

				// Token: 0x0400C199 RID: 49561
				public static LocString DESCRIPTION = "A moon's moon's moon. It's very small.";

				// Token: 0x0400C19A RID: 49562
				public static LocString ARTIFACT = "In contrast to most moons, this object's glowing properties do not come from reflecting an external source of light, but rather from an internal glow of mysterious origin.\n\nThe glow of this object also grants an extraordinary amount of Decor bonus to nearby Duplicants, almost as if it was designed that way.";
			}

			// Token: 0x02002CD5 RID: 11477
			public class BIOLUMINESCENTROCK
			{
				// Token: 0x0400C19B RID: 49563
				public static LocString NAME = "Bioluminescent Rock";

				// Token: 0x0400C19C RID: 49564
				public static LocString DESCRIPTION = "A thriving colony of tiny, microscopic organisms is responsible for giving it its bluish glow.";

				// Token: 0x0400C19D RID: 49565
				public static LocString ARTIFACT = "The microscopic organisms within this rock are of a unique variety whose genetic code shows many tell-tale signs of being genetically engineered within a lab.\n\nFurther analysis reveals they share 99.999% of their genetic code with Shine Bugs.";
			}

			// Token: 0x02002CD6 RID: 11478
			public class PLASMALAMP
			{
				// Token: 0x0400C19E RID: 49566
				public static LocString NAME = "Plasma Lamp";

				// Token: 0x0400C19F RID: 49567
				public static LocString DESCRIPTION = "No space colony is complete without one.";

				// Token: 0x0400C1A0 RID: 49568
				public static LocString ARTIFACT = "The bottom of this lamp contains the words 'Property of the Atmospheric Sciences Department'.\n\nIt's worth noting that the Gravitas Atmospheric Sciences Department once simulated an experiment testing the feasibility of survival in an environment filled with noble gasses, similar to the ones contained within this device.";
			}

			// Token: 0x02002CD7 RID: 11479
			public class MOLDAVITE
			{
				// Token: 0x0400C1A1 RID: 49569
				public static LocString NAME = "Moldavite";

				// Token: 0x0400C1A2 RID: 49570
				public static LocString DESCRIPTION = "A unique green stone formed from the impact of a meteorite.";

				// Token: 0x0400C1A3 RID: 49571
				public static LocString ARTIFACT = "This extremely rare, museum grade moldavite once sat on the desk of Dr. Ren Sato, but it was stolen by some unknown person.\n\nDr. Sato suspected the perpetrator was none other than Director Stern, but was never able to confirm this theory.";
			}

			// Token: 0x02002CD8 RID: 11480
			public class BRICKPHONE
			{
				// Token: 0x0400C1A4 RID: 49572
				public static LocString NAME = "Strange Brick";

				// Token: 0x0400C1A5 RID: 49573
				public static LocString DESCRIPTION = "It still works.";

				// Token: 0x0400C1A6 RID: 49574
				public static LocString ARTIFACT = "This cordless phone once held a direct line to an unknown location in which strange distant voices can be heard but not understood, nor interacted with.\n\nThough Gravitas spent a lot of money and years of study dedicated to discovering its secret, the mystery was never solved.";
			}

			// Token: 0x02002CD9 RID: 11481
			public class SOLARSYSTEM
			{
				// Token: 0x0400C1A7 RID: 49575
				public static LocString NAME = "Self-Contained System";

				// Token: 0x0400C1A8 RID: 49576
				public static LocString DESCRIPTION = "A marvel of the cosmos, inside this display is an entirely self-contained solar system.";

				// Token: 0x0400C1A9 RID: 49577
				public static LocString ARTIFACT = "This marvel of a device was built using parts from an old Tornado-in-a-Box science fair project.\n\nVery faint, faded letters are still visible on the display bottom that read 'Camille P. Grade 5'.";
			}

			// Token: 0x02002CDA RID: 11482
			public class SINK
			{
				// Token: 0x0400C1AA RID: 49578
				public static LocString NAME = "Sink";

				// Token: 0x0400C1AB RID: 49579
				public static LocString DESCRIPTION = "No collection is complete without it.";

				// Token: 0x0400C1AC RID: 49580
				public static LocString ARTIFACT = "A small trace of encrusted soap on this sink strongly suggests it was installed in a personal bathroom, rather than a public one which would have used a soap dispenser.\n\nThe soap sliver is light blue and contains a manufactured blueberry fragrance.";
			}

			// Token: 0x02002CDB RID: 11483
			public class ROCKTORNADO
			{
				// Token: 0x0400C1AD RID: 49581
				public static LocString NAME = "Tornado Rock";

				// Token: 0x0400C1AE RID: 49582
				public static LocString DESCRIPTION = "It's unclear how it formed, although I'm glad it did.";

				// Token: 0x0400C1AF RID: 49583
				public static LocString ARTIFACT = "Speculations about the origin of this rock include a paper written by one Harold P. Moreson, Ph.D. in which he theorized it could be a rare form of hollow geode which failed to form any crystals inside.\n\nThis paper appears in the Gravitas archives, and in all probability, was one of the factors in the hiring of Moreson into the Geology department of the company.";
			}

			// Token: 0x02002CDC RID: 11484
			public class BLENDER
			{
				// Token: 0x0400C1B0 RID: 49584
				public static LocString NAME = "Blender";

				// Token: 0x0400C1B1 RID: 49585
				public static LocString DESCRIPTION = "Equipment used to conduct experiments answering the age-old question, \"Could that blend\"?";

				// Token: 0x0400C1B2 RID: 49586
				public static LocString ARTIFACT = "Trace amounts of edible foodstuffs present in this blender indicate that it was probably used to emulsify the ingredients of a mush bar.\n\nIt is also very likely that it was employed at least once in the production of a peanut butter and banana smoothie.";
			}

			// Token: 0x02002CDD RID: 11485
			public class SAXOPHONE
			{
				// Token: 0x0400C1B3 RID: 49587
				public static LocString NAME = "Mangled Saxophone";

				// Token: 0x0400C1B4 RID: 49588
				public static LocString DESCRIPTION = "The name \"Pesquet\" is barely legible on the inside.";

				// Token: 0x0400C1B5 RID: 49589
				public static LocString ARTIFACT = "Though it is often remarked that \"in space, no one can hear you scream\", Thomas Pesquet proved the same cannot be said for the smooth jazzy sounds of a saxophone.\n\nAlthough this instrument once belonged to the eminent French Astronaut its current bumped and bent shape suggests it has seen many adventures beyond that of just being used to perform an out-of-this-world saxophone solo.";
			}

			// Token: 0x02002CDE RID: 11486
			public class STETHOSCOPE
			{
				// Token: 0x0400C1B6 RID: 49590
				public static LocString NAME = "Stethoscope";

				// Token: 0x0400C1B7 RID: 49591
				public static LocString DESCRIPTION = "Listens to Duplicant heartbeats, or gurgly tummies.";

				// Token: 0x0400C1B8 RID: 49592
				public static LocString ARTIFACT = "The size and shape of this stethescope suggests it was not intended to be used by neither a human-sized nor a Duplicant-sized person but something half-way in between the two beings.";
			}

			// Token: 0x02002CDF RID: 11487
			public class VHS
			{
				// Token: 0x0400C1B9 RID: 49593
				public static LocString NAME = "Archaic Tech";

				// Token: 0x0400C1BA RID: 49594
				public static LocString DESCRIPTION = "Be kind when you handle it. It's very fragile.";

				// Token: 0x0400C1BB RID: 49595
				public static LocString ARTIFACT = "The label on this VHS tape reads \"Jackie and Olivia's House Warming Party\".\n\nUnfortunately, a device with which to play this recording no longer exists in this universe.";
			}

			// Token: 0x02002CE0 RID: 11488
			public class REACTORMODEL
			{
				// Token: 0x0400C1BC RID: 49596
				public static LocString NAME = "Model Nuclear Power Plant";

				// Token: 0x0400C1BD RID: 49597
				public static LocString DESCRIPTION = "It's pronounced nu-clear.";

				// Token: 0x0400C1BE RID: 49598
				public static LocString ARTIFACT = "Though this Nuclear Power Plant was never built, this model exists as an artifact to a time early in the life of Gravitas when it was researching all alternatives to solving the global energy problem.\n\nUltimately, the idea of building a Nuclear Power Plant was abandoned in favor of the \"much safer\" alternative of developing the Temporal Bow.";
			}

			// Token: 0x02002CE1 RID: 11489
			public class MOODRING
			{
				// Token: 0x0400C1BF RID: 49599
				public static LocString NAME = "Radiation Mood Ring";

				// Token: 0x0400C1C0 RID: 49600
				public static LocString DESCRIPTION = "How radioactive are you feeling?";

				// Token: 0x0400C1C1 RID: 49601
				public static LocString ARTIFACT = "A wholly unique ring not found anywhere outside of the Gravitas Laboratory.\n\nThough it can't be determined for sure who worked on this extraordinary curiousity it's worth noting that, for his Ph.D. thesis, Dr. Travaldo Farrington wrote a paper entitled \"Novelty Uses for Radiochromatic Dyes\".";
			}

			// Token: 0x02002CE2 RID: 11490
			public class ORACLE
			{
				// Token: 0x0400C1C2 RID: 49602
				public static LocString NAME = "Useless Machine";

				// Token: 0x0400C1C3 RID: 49603
				public static LocString DESCRIPTION = "What does it do?";

				// Token: 0x0400C1C4 RID: 49604
				public static LocString ARTIFACT = "All of the parts for this contraption are recycled from projects abandoned by the Robotics department.\n\nThe design is very close to one published in an amateur DIY magazine that once sat in the lobby of the 'Employees Only' area of Gravitas' facilities.";
			}

			// Token: 0x02002CE3 RID: 11491
			public class GRUBSTATUE
			{
				// Token: 0x0400C1C5 RID: 49605
				public static LocString NAME = "Grubgrub Statue";

				// Token: 0x0400C1C6 RID: 49606
				public static LocString DESCRIPTION = "A moving tribute to a tiny plant hugger.";

				// Token: 0x0400C1C7 RID: 49607
				public static LocString ARTIFACT = "It's very likely this statue was placed in a hidden, secluded place in the Gravitas laboratory since the creation of Grubgrubs was a closely held secret that the general public was not privy to.\n\nThis is a shame since the artistic quality of this statue is really quite accomplished.";
			}

			// Token: 0x02002CE4 RID: 11492
			public class HONEYJAR
			{
				// Token: 0x0400C1C8 RID: 49608
				public static LocString NAME = "Honey Jar";

				// Token: 0x0400C1C9 RID: 49609
				public static LocString DESCRIPTION = "Sweet golden liquid with just a touch of uranium.";

				// Token: 0x0400C1CA RID: 49610
				public static LocString ARTIFACT = "Records from the Genetics and Biology Lab of the Gravitas facility show that several early iterations of a radioactive Bee would continue to produce honey and that this honey was once accidentally stored in the employee kitchen which resulted in several incidents of minor radiation poisoning when it was erroneously labled as a sweetener for tea.\n\nEmployees who used this product reported that it was the \"sweetest honey they'd ever tasted\" and expressed no regret at the mix-up.";
			}

			// Token: 0x02002CE5 RID: 11493
			public class PLASTICFLOWERS
			{
				// Token: 0x0400C1CB RID: 49611
				public static LocString NAME = "Plastic Flowers";

				// Token: 0x0400C1CC RID: 49612
				public static LocString DESCRIPTION = "Maintenance-free blooms that will outlast us all.";

				// Token: 0x0400C1CD RID: 49613
				public static LocString ARTIFACT = "Manufactured and sold by a home staging company hired by Gravitas to \"make Space feel more like home.\"\n\nThis bouquet is designed to smell like freshly baked cookies.";
			}

			// Token: 0x02002CE6 RID: 11494
			public class FOUNTAINPEN
			{
				// Token: 0x0400C1CE RID: 49614
				public static LocString NAME = "Fountain Pen";

				// Token: 0x0400C1CF RID: 49615
				public static LocString DESCRIPTION = "It cuts through red tape better than a sword ever could.";

				// Token: 0x0400C1D0 RID: 49616
				public static LocString ARTIFACT = "The handcrafted gold nib features a triangular logo with the letters V and I inside.\n\nIts owner was too proud to report it stolen, and would be shocked to learn of its whereabouts.";
			}
		}

		// Token: 0x020023B4 RID: 9140
		public class KEEPSAKES
		{
			// Token: 0x02002CE7 RID: 11495
			public class CRITTER_MANIPULATOR
			{
				// Token: 0x0400C1D1 RID: 49617
				public static LocString NAME = "Ceramic Morb";

				// Token: 0x0400C1D2 RID: 49618
				public static LocString DESCRIPTION = "A pottery project produced in an HR-mandated art therapy class.\n\nIt's glazed with a substance that once landed a curious lab technician in the ER.";
			}

			// Token: 0x02002CE8 RID: 11496
			public class MEGA_BRAIN
			{
				// Token: 0x0400C1D3 RID: 49619
				public static LocString NAME = "Model Plane";

				// Token: 0x0400C1D4 RID: 49620
				public static LocString DESCRIPTION = "A treasured souvenir that was once a common accompaniment to children's meals during commercial flights. There's a hole in the bottom from when Dr. Holland had it mounted on a stand.";
			}

			// Token: 0x02002CE9 RID: 11497
			public class LONELY_MINION
			{
				// Token: 0x0400C1D5 RID: 49621
				public static LocString NAME = "Rusty Toolbox";

				// Token: 0x0400C1D6 RID: 49622
				public static LocString DESCRIPTION = "On the inside of the lid, someone used a screwdriver to carve a drawing of a group of smiling Duplicants gathered around a massive crater.";
			}

			// Token: 0x02002CEA RID: 11498
			public class FOSSIL_HUNT
			{
				// Token: 0x0400C1D7 RID: 49623
				public static LocString NAME = "Critter Collar";

				// Token: 0x0400C1D8 RID: 49624
				public static LocString DESCRIPTION = "The tag reads \"Molly\".\n\nOn the reverse is \"Designed by B363\" stamped above what appears to be an unusually shaped pawprint.";
			}

			// Token: 0x02002CEB RID: 11499
			public class MORB_ROVER_MAKER
			{
				// Token: 0x0400C1D9 RID: 49625
				public static LocString NAME = "Toy Bot";

				// Token: 0x0400C1DA RID: 49626
				public static LocString DESCRIPTION = "A custom-made robot programmed to deliver puns in a variety of celebrity voices.\n\nIt is also a paper shredder.";
			}

			// Token: 0x02002CEC RID: 11500
			public class GEOTHERMAL_PLANT
			{
				// Token: 0x0400C1DB RID: 49627
				public static LocString NAME = "Shiny Coprolite";

				// Token: 0x0400C1DC RID: 49628
				public static LocString DESCRIPTION = "A spectacular sample of organic material fossilized into lead.\n\nSome things really <i>do</i> get better with age.";
			}

			// Token: 0x02002CED RID: 11501
			public class VIEWMASTER
			{
				// Token: 0x0400C1DD RID: 49629
				public static LocString NAME = "Stereoscope";

				// Token: 0x0400C1DE RID: 49630
				public static LocString DESCRIPTION = "A tool used to gaze into frozen moments of time.\n\nOne of the images is of a child standing in a field, waving a huge piece of blackened titanium.";
			}
		}

		// Token: 0x020023B5 RID: 9141
		public class SANDBOXTOOLS
		{
			// Token: 0x02002CEE RID: 11502
			public class SETTINGS
			{
				// Token: 0x020039D4 RID: 14804
				public class INSTANT_BUILD
				{
					// Token: 0x0400E6F8 RID: 59128
					public static LocString NAME = "Instant build mode ON";

					// Token: 0x0400E6F9 RID: 59129
					public static LocString TOOLTIP = "Toggle between placing construction plans and fully built buildings";
				}

				// Token: 0x020039D5 RID: 14805
				public class BRUSH_SIZE
				{
					// Token: 0x0400E6FA RID: 59130
					public static LocString NAME = "Size";

					// Token: 0x0400E6FB RID: 59131
					public static LocString TOOLTIP = "Adjust brush size";
				}

				// Token: 0x020039D6 RID: 14806
				public class BRUSH_NOISE_SCALE
				{
					// Token: 0x0400E6FC RID: 59132
					public static LocString NAME = "Noise A";

					// Token: 0x0400E6FD RID: 59133
					public static LocString TOOLTIP = "Adjust brush noisiness A";
				}

				// Token: 0x020039D7 RID: 14807
				public class BRUSH_NOISE_DENSITY
				{
					// Token: 0x0400E6FE RID: 59134
					public static LocString NAME = "Noise B";

					// Token: 0x0400E6FF RID: 59135
					public static LocString TOOLTIP = "Adjust brush noisiness B";
				}

				// Token: 0x020039D8 RID: 14808
				public class TEMPERATURE
				{
					// Token: 0x0400E700 RID: 59136
					public static LocString NAME = "Temperature";

					// Token: 0x0400E701 RID: 59137
					public static LocString TOOLTIP = "Adjust absolute temperature";
				}

				// Token: 0x020039D9 RID: 14809
				public class TEMPERATURE_ADDITIVE
				{
					// Token: 0x0400E702 RID: 59138
					public static LocString NAME = "Temperature";

					// Token: 0x0400E703 RID: 59139
					public static LocString TOOLTIP = "Adjust additive temperature";
				}

				// Token: 0x020039DA RID: 14810
				public class RADIATION
				{
					// Token: 0x0400E704 RID: 59140
					public static LocString NAME = "Absolute radiation";

					// Token: 0x0400E705 RID: 59141
					public static LocString TOOLTIP = "Adjust absolute radiation";
				}

				// Token: 0x020039DB RID: 14811
				public class RADIATION_ADDITIVE
				{
					// Token: 0x0400E706 RID: 59142
					public static LocString NAME = "Additive radiation";

					// Token: 0x0400E707 RID: 59143
					public static LocString TOOLTIP = "Adjust additive radiation";
				}

				// Token: 0x020039DC RID: 14812
				public class STRESS_ADDITIVE
				{
					// Token: 0x0400E708 RID: 59144
					public static LocString NAME = "Reduce Stress";

					// Token: 0x0400E709 RID: 59145
					public static LocString TOOLTIP = "Adjust stress reduction";
				}

				// Token: 0x020039DD RID: 14813
				public class MORALE
				{
					// Token: 0x0400E70A RID: 59146
					public static LocString NAME = "Adjust Morale";

					// Token: 0x0400E70B RID: 59147
					public static LocString TOOLTIP = "Bonus Morale adjustment";
				}

				// Token: 0x020039DE RID: 14814
				public class MASS
				{
					// Token: 0x0400E70C RID: 59148
					public static LocString NAME = "Mass";

					// Token: 0x0400E70D RID: 59149
					public static LocString TOOLTIP = "Adjust mass";
				}

				// Token: 0x020039DF RID: 14815
				public class DISEASE
				{
					// Token: 0x0400E70E RID: 59150
					public static LocString NAME = "Germ";

					// Token: 0x0400E70F RID: 59151
					public static LocString TOOLTIP = "Adjust type of germ";
				}

				// Token: 0x020039E0 RID: 14816
				public class DISEASE_COUNT
				{
					// Token: 0x0400E710 RID: 59152
					public static LocString NAME = "Germs";

					// Token: 0x0400E711 RID: 59153
					public static LocString TOOLTIP = "Adjust germ count";
				}

				// Token: 0x020039E1 RID: 14817
				public class BRUSH
				{
					// Token: 0x0400E712 RID: 59154
					public static LocString NAME = "Brush";

					// Token: 0x0400E713 RID: 59155
					public static LocString TOOLTIP = "Paint elements into the world simulation {Hotkey}";
				}

				// Token: 0x020039E2 RID: 14818
				public class ELEMENT
				{
					// Token: 0x0400E714 RID: 59156
					public static LocString NAME = "Element";

					// Token: 0x0400E715 RID: 59157
					public static LocString TOOLTIP = "Adjust type of element";
				}

				// Token: 0x020039E3 RID: 14819
				public class SPRINKLE
				{
					// Token: 0x0400E716 RID: 59158
					public static LocString NAME = "Sprinkle";

					// Token: 0x0400E717 RID: 59159
					public static LocString TOOLTIP = "Paint elements into the simulation using noise {Hotkey}";
				}

				// Token: 0x020039E4 RID: 14820
				public class FLOOD
				{
					// Token: 0x0400E718 RID: 59160
					public static LocString NAME = "Fill";

					// Token: 0x0400E719 RID: 59161
					public static LocString TOOLTIP = "Fill a section of the simulation with the chosen element {Hotkey}";
				}

				// Token: 0x020039E5 RID: 14821
				public class SAMPLE
				{
					// Token: 0x0400E71A RID: 59162
					public static LocString NAME = "Sample";

					// Token: 0x0400E71B RID: 59163
					public static LocString TOOLTIP = "Copy the settings from a cell to use with brush tools {Hotkey}";
				}

				// Token: 0x020039E6 RID: 14822
				public class HEATGUN
				{
					// Token: 0x0400E71C RID: 59164
					public static LocString NAME = "Heat Gun";

					// Token: 0x0400E71D RID: 59165
					public static LocString TOOLTIP = "Inject thermal energy into the simulation {Hotkey}";
				}

				// Token: 0x020039E7 RID: 14823
				public class RADSTOOL
				{
					// Token: 0x0400E71E RID: 59166
					public static LocString NAME = "Radiation Tool";

					// Token: 0x0400E71F RID: 59167
					public static LocString TOOLTIP = "Inject or remove radiation from the simulation {Hotkey}";
				}

				// Token: 0x020039E8 RID: 14824
				public class SPAWNER
				{
					// Token: 0x0400E720 RID: 59168
					public static LocString NAME = "Spawner";

					// Token: 0x0400E721 RID: 59169
					public static LocString TOOLTIP = "Spawn critters, food, equipment, and other entities {Hotkey}";
				}

				// Token: 0x020039E9 RID: 14825
				public class STRESS
				{
					// Token: 0x0400E722 RID: 59170
					public static LocString NAME = "Stress";

					// Token: 0x0400E723 RID: 59171
					public static LocString TOOLTIP = "Manage Duplicants' stress levels {Hotkey}";
				}

				// Token: 0x020039EA RID: 14826
				public class CLEAR_FLOOR
				{
					// Token: 0x0400E724 RID: 59172
					public static LocString NAME = "Clear Debris";

					// Token: 0x0400E725 RID: 59173
					public static LocString TOOLTIP = "Delete loose items cluttering the floor {Hotkey}";
				}

				// Token: 0x020039EB RID: 14827
				public class DESTROY
				{
					// Token: 0x0400E726 RID: 59174
					public static LocString NAME = "Destroy";

					// Token: 0x0400E727 RID: 59175
					public static LocString TOOLTIP = "Delete everything in the selected cell(s) {Hotkey}";
				}

				// Token: 0x020039EC RID: 14828
				public class SPAWN_ENTITY
				{
					// Token: 0x0400E728 RID: 59176
					public static LocString NAME = "Spawn";
				}

				// Token: 0x020039ED RID: 14829
				public class FOW
				{
					// Token: 0x0400E729 RID: 59177
					public static LocString NAME = "Reveal";

					// Token: 0x0400E72A RID: 59178
					public static LocString TOOLTIP = "Dispel the Fog of War shrouding the map {Hotkey}";
				}

				// Token: 0x020039EE RID: 14830
				public class CRITTER
				{
					// Token: 0x0400E72B RID: 59179
					public static LocString NAME = "Critter Removal";

					// Token: 0x0400E72C RID: 59180
					public static LocString TOOLTIP = "Remove critters! {Hotkey}";
				}

				// Token: 0x020039EF RID: 14831
				public class SPAWN_STORY_TRAIT
				{
					// Token: 0x0400E72D RID: 59181
					public static LocString NAME = "Story Traits";

					// Token: 0x0400E72E RID: 59182
					public static LocString TOOLTIP = "Spawn story traits {Hotkey}";
				}
			}

			// Token: 0x02002CEF RID: 11503
			public class FILTERS
			{
				// Token: 0x0400C1DF RID: 49631
				public static LocString BACK = "Back";

				// Token: 0x0400C1E0 RID: 49632
				public static LocString COMMON = "Common Substances";

				// Token: 0x0400C1E1 RID: 49633
				public static LocString SOLID = "Solids";

				// Token: 0x0400C1E2 RID: 49634
				public static LocString LIQUID = "Liquids";

				// Token: 0x0400C1E3 RID: 49635
				public static LocString GAS = "Gases";

				// Token: 0x020039F0 RID: 14832
				public class ENTITIES
				{
					// Token: 0x0400E72F RID: 59183
					public static LocString BIONICUPGRADES = "Boosters";

					// Token: 0x0400E730 RID: 59184
					public static LocString SPECIAL = "Special";

					// Token: 0x0400E731 RID: 59185
					public static LocString GRAVITAS = "Gravitas";

					// Token: 0x0400E732 RID: 59186
					public static LocString PLANTS = "Plants";

					// Token: 0x0400E733 RID: 59187
					public static LocString SEEDS = "Seeds";

					// Token: 0x0400E734 RID: 59188
					public static LocString CREATURE = "Critters";

					// Token: 0x0400E735 RID: 59189
					public static LocString CREATURE_EGG = "Eggs";

					// Token: 0x0400E736 RID: 59190
					public static LocString FOOD = "Foods";

					// Token: 0x0400E737 RID: 59191
					public static LocString EQUIPMENT = "Equipment";

					// Token: 0x0400E738 RID: 59192
					public static LocString GEYSERS = "Geysers";

					// Token: 0x0400E739 RID: 59193
					public static LocString EXPERIMENTS = "Experimental";

					// Token: 0x0400E73A RID: 59194
					public static LocString INDUSTRIAL_PRODUCTS = "Industrial";

					// Token: 0x0400E73B RID: 59195
					public static LocString COMETS = "Meteors";

					// Token: 0x0400E73C RID: 59196
					public static LocString ARTIFACTS = "Artifacts";

					// Token: 0x0400E73D RID: 59197
					public static LocString STORYTRAITS = "Story Traits";
				}
			}

			// Token: 0x02002CF0 RID: 11504
			public class CLEARFLOOR
			{
				// Token: 0x0400C1E4 RID: 49636
				public static LocString DELETED = "Deleted";
			}
		}

		// Token: 0x020023B6 RID: 9142
		public class RETIRED_COLONY_INFO_SCREEN
		{
			// Token: 0x04009F46 RID: 40774
			public static LocString SECONDS = "Seconds";

			// Token: 0x04009F47 RID: 40775
			public static LocString CYCLES = "Cycles";

			// Token: 0x04009F48 RID: 40776
			public static LocString CYCLE_COUNT = "Cycle Count: {0}";

			// Token: 0x04009F49 RID: 40777
			public static LocString DUPLICANT_AGE = "Age: {0} cycles";

			// Token: 0x04009F4A RID: 40778
			public static LocString SKILL_LEVEL = "Skill Level: {0}";

			// Token: 0x04009F4B RID: 40779
			public static LocString BUILDING_COUNT = "Count: {0}";

			// Token: 0x04009F4C RID: 40780
			public static LocString PREVIEW_UNAVAILABLE = "Preview\nUnavailable";

			// Token: 0x04009F4D RID: 40781
			public static LocString TIMELAPSE_UNAVAILABLE = "Timelapse\nUnavailable";

			// Token: 0x04009F4E RID: 40782
			public static LocString SEARCH = "SEARCH...";

			// Token: 0x02002CF1 RID: 11505
			public class BUTTONS
			{
				// Token: 0x0400C1E5 RID: 49637
				public static LocString RETURN_TO_GAME = "RETURN TO GAME";

				// Token: 0x0400C1E6 RID: 49638
				public static LocString VIEW_OTHER_COLONIES = "BACK";

				// Token: 0x0400C1E7 RID: 49639
				public static LocString QUIT_TO_MENU = "QUIT TO MAIN MENU";

				// Token: 0x0400C1E8 RID: 49640
				public static LocString CLOSE = "CLOSE";
			}

			// Token: 0x02002CF2 RID: 11506
			public class TITLES
			{
				// Token: 0x0400C1E9 RID: 49641
				public static LocString EXPLORER_HEADER = "COLONIES";

				// Token: 0x0400C1EA RID: 49642
				public static LocString RETIRED_COLONIES = "Colony Summaries";

				// Token: 0x0400C1EB RID: 49643
				public static LocString COLONY_STATISTICS = "Colony Statistics";

				// Token: 0x0400C1EC RID: 49644
				public static LocString DUPLICANTS = "Duplicants";

				// Token: 0x0400C1ED RID: 49645
				public static LocString BUILDINGS = "Buildings";

				// Token: 0x0400C1EE RID: 49646
				public static LocString CHEEVOS = "Colony Achievements";

				// Token: 0x0400C1EF RID: 49647
				public static LocString ACHIEVEMENT_HEADER = "ACHIEVEMENTS";

				// Token: 0x0400C1F0 RID: 49648
				public static LocString TIMELAPSE = "Timelapse";
			}

			// Token: 0x02002CF3 RID: 11507
			public class STATS
			{
				// Token: 0x0400C1F1 RID: 49649
				public static LocString OXYGEN_CREATED = "Total Oxygen Produced";

				// Token: 0x0400C1F2 RID: 49650
				public static LocString OXYGEN_CONSUMED = "Total Oxygen Consumed";

				// Token: 0x0400C1F3 RID: 49651
				public static LocString POWER_CREATED = "Average Power Produced";

				// Token: 0x0400C1F4 RID: 49652
				public static LocString POWER_WASTED = "Average Power Wasted";

				// Token: 0x0400C1F5 RID: 49653
				public static LocString TRAVEL_TIME = "Total Travel Time";

				// Token: 0x0400C1F6 RID: 49654
				public static LocString WORK_TIME = "Total Work Time";

				// Token: 0x0400C1F7 RID: 49655
				public static LocString AVERAGE_TRAVEL_TIME = "Average Travel Time";

				// Token: 0x0400C1F8 RID: 49656
				public static LocString AVERAGE_WORK_TIME = "Average Work Time";

				// Token: 0x0400C1F9 RID: 49657
				public static LocString CALORIES_CREATED = "Calorie Generation";

				// Token: 0x0400C1FA RID: 49658
				public static LocString CALORIES_CONSUMED = "Calorie Consumption";

				// Token: 0x0400C1FB RID: 49659
				public static LocString LIVE_DUPLICANTS = "Duplicants";

				// Token: 0x0400C1FC RID: 49660
				public static LocString AVERAGE_STRESS_CREATED = "Average Stress Created";

				// Token: 0x0400C1FD RID: 49661
				public static LocString AVERAGE_STRESS_REMOVED = "Average Stress Removed";

				// Token: 0x0400C1FE RID: 49662
				public static LocString NUMBER_DOMESTICATED_CRITTERS = "Domesticated Critters";

				// Token: 0x0400C1FF RID: 49663
				public static LocString NUMBER_WILD_CRITTERS = "Wild Critters";

				// Token: 0x0400C200 RID: 49664
				public static LocString AVERAGE_GERMS = "Average Germs";

				// Token: 0x0400C201 RID: 49665
				public static LocString ROCKET_MISSIONS = "Rocket Missions Underway";
			}
		}

		// Token: 0x020023B7 RID: 9143
		public class DROPDOWN
		{
			// Token: 0x04009F4F RID: 40783
			public static LocString NONE = "Unassigned";
		}

		// Token: 0x020023B8 RID: 9144
		public class FRONTEND
		{
			// Token: 0x04009F50 RID: 40784
			public static LocString GAME_VERSION = "Game Version: ";

			// Token: 0x04009F51 RID: 40785
			public static LocString LOADING = "Loading...";

			// Token: 0x04009F52 RID: 40786
			public static LocString DONE_BUTTON = "DONE";

			// Token: 0x02002CF4 RID: 11508
			public class DEMO_OVER_SCREEN
			{
				// Token: 0x0400C202 RID: 49666
				public static LocString TITLE = "Thanks for playing!";

				// Token: 0x0400C203 RID: 49667
				public static LocString BODY = "Thank you for playing the demo for Oxygen Not Included!\n\nThis game is still in development.\n\nGo to kleigames.com/o2 or ask one of us if you'd like more information.";

				// Token: 0x0400C204 RID: 49668
				public static LocString BUTTON_EXIT_TO_MENU = "EXIT TO MENU";
			}

			// Token: 0x02002CF5 RID: 11509
			public class CUSTOMGAMESETTINGSSCREEN
			{
				// Token: 0x020039F1 RID: 14833
				public class SETTINGS
				{
					// Token: 0x02003EA5 RID: 16037
					public class SANDBOXMODE
					{
						// Token: 0x0400F284 RID: 62084
						public static LocString NAME = "Sandbox Mode";

						// Token: 0x0400F285 RID: 62085
						public static LocString TOOLTIP = "Manipulate and customize the simulation with tools that ignore regular game constraints";

						// Token: 0x02003F2A RID: 16170
						public static class LEVELS
						{
							// Token: 0x02003F45 RID: 16197
							public static class DISABLED
							{
								// Token: 0x0400F3D8 RID: 62424
								public static LocString NAME = "Disabled";

								// Token: 0x0400F3D9 RID: 62425
								public static LocString TOOLTIP = "Unchecked: Sandbox Mode is turned off (Default)";
							}

							// Token: 0x02003F46 RID: 16198
							public static class ENABLED
							{
								// Token: 0x0400F3DA RID: 62426
								public static LocString NAME = "Enabled";

								// Token: 0x0400F3DB RID: 62427
								public static LocString TOOLTIP = "Checked: Sandbox Mode is turned on";
							}
						}
					}

					// Token: 0x02003EA6 RID: 16038
					public class FASTWORKERSMODE
					{
						// Token: 0x0400F286 RID: 62086
						public static LocString NAME = "Fast Workers Mode";

						// Token: 0x0400F287 RID: 62087
						public static LocString TOOLTIP = "Duplicants will finish most work immediately and require little sleep";

						// Token: 0x02003F2B RID: 16171
						public static class LEVELS
						{
							// Token: 0x02003F47 RID: 16199
							public static class DISABLED
							{
								// Token: 0x0400F3DC RID: 62428
								public static LocString NAME = "Disabled";

								// Token: 0x0400F3DD RID: 62429
								public static LocString TOOLTIP = "Unchecked: Fast Workers Mode is turned off (Default)";
							}

							// Token: 0x02003F48 RID: 16200
							public static class ENABLED
							{
								// Token: 0x0400F3DE RID: 62430
								public static LocString NAME = "Enabled";

								// Token: 0x0400F3DF RID: 62431
								public static LocString TOOLTIP = "Checked: Fast Workers Mode is turned on";
							}
						}
					}

					// Token: 0x02003EA7 RID: 16039
					public class EXPANSION1ACTIVE
					{
						// Token: 0x0400F288 RID: 62088
						public static LocString NAME = UI.DLC1.NAME_ITAL + " Content Enabled";

						// Token: 0x0400F289 RID: 62089
						public static LocString TOOLTIP = "If checked, content from the " + UI.DLC1.NAME_ITAL + " Expansion will be available";

						// Token: 0x02003F2C RID: 16172
						public static class LEVELS
						{
							// Token: 0x02003F49 RID: 16201
							public static class DISABLED
							{
								// Token: 0x0400F3E0 RID: 62432
								public static LocString NAME = "Disabled";

								// Token: 0x0400F3E1 RID: 62433
								public static LocString TOOLTIP = "Unchecked: " + UI.DLC1.NAME_ITAL + " Content is turned off (Default)";
							}

							// Token: 0x02003F4A RID: 16202
							public static class ENABLED
							{
								// Token: 0x0400F3E2 RID: 62434
								public static LocString NAME = "Enabled";

								// Token: 0x0400F3E3 RID: 62435
								public static LocString TOOLTIP = "Checked: " + UI.DLC1.NAME_ITAL + " Content is turned on";
							}
						}
					}

					// Token: 0x02003EA8 RID: 16040
					public class SAVETOCLOUD
					{
						// Token: 0x0400F28A RID: 62090
						public static LocString NAME = "Save To Cloud";

						// Token: 0x0400F28B RID: 62091
						public static LocString TOOLTIP = "This colony will be created in the cloud saves folder, and synced by the game platform.";

						// Token: 0x0400F28C RID: 62092
						public static LocString TOOLTIP_LOCAL = "This colony will be created in the local saves folder. It will not be a cloud save and will not be synced by the game platform.";

						// Token: 0x0400F28D RID: 62093
						public static LocString TOOLTIP_EXTRA = "This can be changed later with the colony management options in the load screen, from the main menu.";

						// Token: 0x02003F2D RID: 16173
						public static class LEVELS
						{
							// Token: 0x02003F4B RID: 16203
							public static class DISABLED
							{
								// Token: 0x0400F3E4 RID: 62436
								public static LocString NAME = "Disabled";

								// Token: 0x0400F3E5 RID: 62437
								public static LocString TOOLTIP = "Unchecked: This colony will be a local save";
							}

							// Token: 0x02003F4C RID: 16204
							public static class ENABLED
							{
								// Token: 0x0400F3E6 RID: 62438
								public static LocString NAME = "Enabled";

								// Token: 0x0400F3E7 RID: 62439
								public static LocString TOOLTIP = "Checked: This colony will be a cloud save (Default)";
							}
						}
					}

					// Token: 0x02003EA9 RID: 16041
					public class CAREPACKAGES
					{
						// Token: 0x0400F28E RID: 62094
						public static LocString NAME = "Care Packages";

						// Token: 0x0400F28F RID: 62095
						public static LocString TOOLTIP = "Affects what resources can be printed from the Printing Pod";

						// Token: 0x02003F2E RID: 16174
						public static class LEVELS
						{
							// Token: 0x02003F4D RID: 16205
							public static class NORMAL
							{
								// Token: 0x0400F3E8 RID: 62440
								public static LocString NAME = "All";

								// Token: 0x0400F3E9 RID: 62441
								public static LocString TOOLTIP = "Checked: The Printing Pod will offer both Duplicant blueprints and care packages (Default)";
							}

							// Token: 0x02003F4E RID: 16206
							public static class DUPLICANTS_ONLY
							{
								// Token: 0x0400F3EA RID: 62442
								public static LocString NAME = "Duplicants Only";

								// Token: 0x0400F3EB RID: 62443
								public static LocString TOOLTIP = "Unchecked: The Printing Pod will only offer Duplicant blueprints";
							}
						}
					}

					// Token: 0x02003EAA RID: 16042
					public class IMMUNESYSTEM
					{
						// Token: 0x0400F290 RID: 62096
						public static LocString NAME = "Disease";

						// Token: 0x0400F291 RID: 62097
						public static LocString TOOLTIP = "Affects Duplicants' chances of contracting a disease after germ exposure";

						// Token: 0x02003F2F RID: 16175
						public static class LEVELS
						{
							// Token: 0x02003F4F RID: 16207
							public static class COMPROMISED
							{
								// Token: 0x0400F3EC RID: 62444
								public static LocString NAME = "Outbreak Prone";

								// Token: 0x0400F3ED RID: 62445
								public static LocString TOOLTIP = "The whole colony will be ravaged by plague if a Duplicant so much as sneezes funny";

								// Token: 0x0400F3EE RID: 62446
								public static LocString ATTRIBUTE_MODIFIER_NAME = "Outbreak Prone (Highest Difficulty)";
							}

							// Token: 0x02003F50 RID: 16208
							public static class WEAK
							{
								// Token: 0x0400F3EF RID: 62447
								public static LocString NAME = "Germ Susceptible";

								// Token: 0x0400F3F0 RID: 62448
								public static LocString TOOLTIP = "These Duplicants have an increased chance of contracting diseases from germ exposure";

								// Token: 0x0400F3F1 RID: 62449
								public static LocString ATTRIBUTE_MODIFIER_NAME = "Germ Susceptibility (Difficulty Up)";
							}

							// Token: 0x02003F51 RID: 16209
							public static class DEFAULT
							{
								// Token: 0x0400F3F2 RID: 62450
								public static LocString NAME = "Default";

								// Token: 0x0400F3F3 RID: 62451
								public static LocString TOOLTIP = "Default disease chance";
							}

							// Token: 0x02003F52 RID: 16210
							public static class STRONG
							{
								// Token: 0x0400F3F4 RID: 62452
								public static LocString NAME = "Germ Resistant";

								// Token: 0x0400F3F5 RID: 62453
								public static LocString TOOLTIP = "These Duplicants have a decreased chance of contracting diseases from germ exposure";

								// Token: 0x0400F3F6 RID: 62454
								public static LocString ATTRIBUTE_MODIFIER_NAME = "Germ Resistance (Difficulty Down)";
							}

							// Token: 0x02003F53 RID: 16211
							public static class INVINCIBLE
							{
								// Token: 0x0400F3F7 RID: 62455
								public static LocString NAME = "Total Immunity";

								// Token: 0x0400F3F8 RID: 62456
								public static LocString TOOLTIP = "Like diplomatic immunity, but without the diplomacy. These Duplicants will never get sick";

								// Token: 0x0400F3F9 RID: 62457
								public static LocString ATTRIBUTE_MODIFIER_NAME = "Total Immunity (No Disease)";
							}
						}
					}

					// Token: 0x02003EAB RID: 16043
					public class MORALE
					{
						// Token: 0x0400F292 RID: 62098
						public static LocString NAME = "Morale";

						// Token: 0x0400F293 RID: 62099
						public static LocString TOOLTIP = "Adjusts the minimum morale Duplicants must maintain to avoid gaining stress";

						// Token: 0x02003F30 RID: 16176
						public static class LEVELS
						{
							// Token: 0x02003F54 RID: 16212
							public static class VERYHARD
							{
								// Token: 0x0400F3FA RID: 62458
								public static LocString NAME = "Draconian";

								// Token: 0x0400F3FB RID: 62459
								public static LocString TOOLTIP = "The finest of the finest can barely keep up with these Duplicants' stringent demands";

								// Token: 0x0400F3FC RID: 62460
								public static LocString ATTRIBUTE_MODIFIER_NAME = "Draconian (Highest Difficulty)";
							}

							// Token: 0x02003F55 RID: 16213
							public static class HARD
							{
								// Token: 0x0400F3FD RID: 62461
								public static LocString NAME = "A Bit Persnickety";

								// Token: 0x0400F3FE RID: 62462
								public static LocString TOOLTIP = "Duplicants require higher morale than usual to fend off stress";

								// Token: 0x0400F3FF RID: 62463
								public static LocString ATTRIBUTE_MODIFIER_NAME = "A Bit Persnickety (Difficulty Up)";
							}

							// Token: 0x02003F56 RID: 16214
							public static class DEFAULT
							{
								// Token: 0x0400F400 RID: 62464
								public static LocString NAME = "Default";

								// Token: 0x0400F401 RID: 62465
								public static LocString TOOLTIP = "Default morale needs";
							}

							// Token: 0x02003F57 RID: 16215
							public static class EASY
							{
								// Token: 0x0400F402 RID: 62466
								public static LocString NAME = "Chill";

								// Token: 0x0400F403 RID: 62467
								public static LocString TOOLTIP = "Duplicants require lower morale than usual to fend off stress";

								// Token: 0x0400F404 RID: 62468
								public static LocString ATTRIBUTE_MODIFIER_NAME = "Chill (Difficulty Down)";
							}

							// Token: 0x02003F58 RID: 16216
							public static class DISABLED
							{
								// Token: 0x0400F405 RID: 62469
								public static LocString NAME = "Totally Blasé";

								// Token: 0x0400F406 RID: 62470
								public static LocString TOOLTIP = "These Duplicants have zero standards and will never gain stress, regardless of their morale";

								// Token: 0x0400F407 RID: 62471
								public static LocString ATTRIBUTE_MODIFIER_NAME = "Totally Blasé (No Morale)";
							}
						}
					}

					// Token: 0x02003EAC RID: 16044
					public class CALORIE_BURN
					{
						// Token: 0x0400F294 RID: 62100
						public static LocString NAME = "Hunger";

						// Token: 0x0400F295 RID: 62101
						public static LocString TOOLTIP = "Affects how quickly Duplicants burn calories and become hungry";

						// Token: 0x02003F31 RID: 16177
						public static class LEVELS
						{
							// Token: 0x02003F59 RID: 16217
							public static class VERYHARD
							{
								// Token: 0x0400F408 RID: 62472
								public static LocString NAME = "Ravenous";

								// Token: 0x0400F409 RID: 62473
								public static LocString TOOLTIP = "Your Duplicants are on a see-food diet... They see food and they eat it";

								// Token: 0x0400F40A RID: 62474
								public static LocString ATTRIBUTE_MODIFIER_NAME = "Ravenous (Highest Difficulty)";
							}

							// Token: 0x02003F5A RID: 16218
							public static class HARD
							{
								// Token: 0x0400F40B RID: 62475
								public static LocString NAME = "Rumbly Tummies";

								// Token: 0x0400F40C RID: 62476
								public static LocString TOOLTIP = "Duplicants burn calories quickly and require more feeding than usual";

								// Token: 0x0400F40D RID: 62477
								public static LocString ATTRIBUTE_MODIFIER_NAME = "Rumbly Tummies (Difficulty Up)";
							}

							// Token: 0x02003F5B RID: 16219
							public static class DEFAULT
							{
								// Token: 0x0400F40E RID: 62478
								public static LocString NAME = "Default";

								// Token: 0x0400F40F RID: 62479
								public static LocString TOOLTIP = "Default calorie burn rate";
							}

							// Token: 0x02003F5C RID: 16220
							public static class EASY
							{
								// Token: 0x0400F410 RID: 62480
								public static LocString NAME = "Fasting";

								// Token: 0x0400F411 RID: 62481
								public static LocString TOOLTIP = "Duplicants burn calories slowly and get by with fewer meals";

								// Token: 0x0400F412 RID: 62482
								public static LocString ATTRIBUTE_MODIFIER_NAME = "Fasting (Difficulty Down)";
							}

							// Token: 0x02003F5D RID: 16221
							public static class DISABLED
							{
								// Token: 0x0400F413 RID: 62483
								public static LocString NAME = "Tummyless";

								// Token: 0x0400F414 RID: 62484
								public static LocString TOOLTIP = "These Duplicants were printed without tummies and need no food at all";

								// Token: 0x0400F415 RID: 62485
								public static LocString ATTRIBUTE_MODIFIER_NAME = "Tummyless (No Hunger)";
							}
						}
					}

					// Token: 0x02003EAD RID: 16045
					public class BIONICPOWERUSE
					{
						// Token: 0x0400F296 RID: 62102
						public static LocString NAME = "Bionic Wattage";

						// Token: 0x0400F297 RID: 62103
						public static LocString TOOLTIP = "Adjusts Bionic Duplicants' baseline power consumption";

						// Token: 0x02003F32 RID: 16178
						public static class LEVELS
						{
							// Token: 0x02003F5E RID: 16222
							public static class VERYHARD
							{
								// Token: 0x0400F416 RID: 62486
								public static LocString NAME = "Energy Vampire";

								// Token: 0x0400F417 RID: 62487
								public static LocString TOOLTIP = "These Bionic Duplicants drain batteries like it's their only job";
							}

							// Token: 0x02003F5F RID: 16223
							public static class HARD
							{
								// Token: 0x0400F418 RID: 62488
								public static LocString NAME = "Power Hungry";

								// Token: 0x0400F419 RID: 62489
								public static LocString TOOLTIP = "These Duplicants have an increased appetite for power";
							}

							// Token: 0x02003F60 RID: 16224
							public static class DEFAULT
							{
								// Token: 0x0400F41A RID: 62490
								public static LocString NAME = "Default";

								// Token: 0x0400F41B RID: 62491
								public static LocString TOOLTIP = "Default wattage";
							}

							// Token: 0x02003F61 RID: 16225
							public static class EASY
							{
								// Token: 0x0400F41C RID: 62492
								public static LocString NAME = "Energy Efficient";

								// Token: 0x0400F41D RID: 62493
								public static LocString TOOLTIP = "These Duplicants consume less power than usual";
							}

							// Token: 0x02003F62 RID: 16226
							public static class VERYEASY
							{
								// Token: 0x0400F41E RID: 62494
								public static LocString NAME = "Analog";

								// Token: 0x0400F41F RID: 62495
								public static LocString TOOLTIP = "These Bionic Duplicants run on old-school enthusiasm, and barely consume power at all";
							}
						}
					}

					// Token: 0x02003EAE RID: 16046
					public class DEMOLIORDIFFICULTY
					{
						// Token: 0x0400F298 RID: 62104
						public static LocString NAME = "Demolior Impact";

						// Token: 0x0400F299 RID: 62105
						public static LocString TOOLTIP = "Adjusts how soon the Demolior asteroid collides with <i>The Prehistoric Planet Pack</i> asteroid";

						// Token: 0x02003F33 RID: 16179
						public static class LEVELS
						{
							// Token: 0x02003F63 RID: 16227
							public static class VERYHARD
							{
								// Token: 0x0400F420 RID: 62496
								public static LocString NAME = "Imminent Extinction";

								// Token: 0x0400F421 RID: 62497
								public static LocString TOOLTIP = "It'll all be over soon\n\nOnly " + 100f.ToString() + " cycles until collision";
							}

							// Token: 0x02003F64 RID: 16228
							public static class HARD
							{
								// Token: 0x0400F422 RID: 62498
								public static LocString NAME = "Early Arrival";

								// Token: 0x0400F423 RID: 62499
								public static LocString TOOLTIP = "Demolior impacts sooner than usual\n\n" + 150f.ToString() + " cycles until collision";
							}

							// Token: 0x02003F65 RID: 16229
							public static class DEFAULT
							{
								// Token: 0x0400F424 RID: 62500
								public static LocString NAME = "Default";

								// Token: 0x0400F425 RID: 62501
								public static LocString TOOLTIP = "Demolior impacts in " + 200f.ToString() + " cycles";
							}

							// Token: 0x02003F66 RID: 16230
							public static class EASY
							{
								// Token: 0x0400F426 RID: 62502
								public static LocString NAME = "Slightly Delayed";

								// Token: 0x0400F427 RID: 62503
								public static LocString TOOLTIP = "Demolior impacts later than usual\n\n" + 300f.ToString() + " cycles until collision";
							}

							// Token: 0x02003F67 RID: 16231
							public static class VERYEASY
							{
								// Token: 0x0400F428 RID: 62504
								public static LocString NAME = "Far-Off Forecast";

								// Token: 0x0400F429 RID: 62505
								public static LocString TOOLTIP = "Duplicants could probably build a whole new asteroid by the time Demolior impacts this one\n\n500 cycles until collision";
							}

							// Token: 0x02003F68 RID: 16232
							public static class OFF
							{
								// Token: 0x0400F42A RID: 62506
								public static LocString NAME = "Disabled";

								// Token: 0x0400F42B RID: 62507
								public static LocString TOOLTIP = "Demolior does not exist in this universe and the achievement cannot be earned";
							}
						}
					}

					// Token: 0x02003EAF RID: 16047
					public class WORLD_CHOICE
					{
						// Token: 0x0400F29A RID: 62106
						public static LocString NAME = "World";

						// Token: 0x0400F29B RID: 62107
						public static LocString TOOLTIP = "New worlds added by mods can be selected here";
					}

					// Token: 0x02003EB0 RID: 16048
					public class CLUSTER_CHOICE
					{
						// Token: 0x0400F29C RID: 62108
						public static LocString NAME = "Asteroid Belt";

						// Token: 0x0400F29D RID: 62109
						public static LocString TOOLTIP = "New asteroid belts added by mods can be selected here";
					}

					// Token: 0x02003EB1 RID: 16049
					public class STORY_TRAIT_COUNT
					{
						// Token: 0x0400F29E RID: 62110
						public static LocString NAME = "Story Traits";

						// Token: 0x0400F29F RID: 62111
						public static LocString TOOLTIP = "Determines the number of story traits spawned";

						// Token: 0x02003F34 RID: 16180
						public static class LEVELS
						{
							// Token: 0x02003F69 RID: 16233
							public static class NONE
							{
								// Token: 0x0400F42C RID: 62508
								public static LocString NAME = "Zilch";

								// Token: 0x0400F42D RID: 62509
								public static LocString TOOLTIP = "Zero story traits. Zip. Nada. None";
							}

							// Token: 0x02003F6A RID: 16234
							public static class FEW
							{
								// Token: 0x0400F42E RID: 62510
								public static LocString NAME = "Stingy";

								// Token: 0x0400F42F RID: 62511
								public static LocString TOOLTIP = "Not zero, but not a lot";
							}

							// Token: 0x02003F6B RID: 16235
							public static class LOTS
							{
								// Token: 0x0400F430 RID: 62512
								public static LocString NAME = "Oodles";

								// Token: 0x0400F431 RID: 62513
								public static LocString TOOLTIP = "Plenty of story traits to go around";
							}
						}
					}

					// Token: 0x02003EB2 RID: 16050
					public class DURABILITY
					{
						// Token: 0x0400F2A0 RID: 62112
						public static LocString NAME = "Durability";

						// Token: 0x0400F2A1 RID: 62113
						public static LocString TOOLTIP = "Affects how quickly equippable suits wear out";

						// Token: 0x02003F35 RID: 16181
						public static class LEVELS
						{
							// Token: 0x02003F6C RID: 16236
							public static class INDESTRUCTIBLE
							{
								// Token: 0x0400F432 RID: 62514
								public static LocString NAME = "Indestructible";

								// Token: 0x0400F433 RID: 62515
								public static LocString TOOLTIP = "Duplicants have perfected clothes manufacturing and are able to make suits that last forever";

								// Token: 0x0400F434 RID: 62516
								public static LocString ATTRIBUTE_MODIFIER_NAME = "Indestructible Suits (No Durability)";
							}

							// Token: 0x02003F6D RID: 16237
							public static class REINFORCED
							{
								// Token: 0x0400F435 RID: 62517
								public static LocString NAME = "Reinforced";

								// Token: 0x0400F436 RID: 62518
								public static LocString TOOLTIP = "Suits are more durable than usual";

								// Token: 0x0400F437 RID: 62519
								public static LocString ATTRIBUTE_MODIFIER_NAME = "Reinforced Suits (Difficulty Down)";
							}

							// Token: 0x02003F6E RID: 16238
							public static class DEFAULT
							{
								// Token: 0x0400F438 RID: 62520
								public static LocString NAME = "Default";

								// Token: 0x0400F439 RID: 62521
								public static LocString TOOLTIP = "Default suit durability";
							}

							// Token: 0x02003F6F RID: 16239
							public static class FLIMSY
							{
								// Token: 0x0400F43A RID: 62522
								public static LocString NAME = "Flimsy";

								// Token: 0x0400F43B RID: 62523
								public static LocString TOOLTIP = "Suits wear out faster than usual";

								// Token: 0x0400F43C RID: 62524
								public static LocString ATTRIBUTE_MODIFIER_NAME = "Flimsy Suits (Difficulty Up)";
							}

							// Token: 0x02003F70 RID: 16240
							public static class THREADBARE
							{
								// Token: 0x0400F43D RID: 62525
								public static LocString NAME = "Threadbare";

								// Token: 0x0400F43E RID: 62526
								public static LocString TOOLTIP = "These Duplicants are no tailors - suits wear out much faster than usual";

								// Token: 0x0400F43F RID: 62527
								public static LocString ATTRIBUTE_MODIFIER_NAME = "Threadbare Suits (Highest Difficulty)";
							}
						}
					}

					// Token: 0x02003EB3 RID: 16051
					public class RADIATION
					{
						// Token: 0x0400F2A2 RID: 62114
						public static LocString NAME = "Radiation";

						// Token: 0x0400F2A3 RID: 62115
						public static LocString TOOLTIP = "Affects how susceptible Duplicants are to radiation sickness";

						// Token: 0x02003F36 RID: 16182
						public static class LEVELS
						{
							// Token: 0x02003F71 RID: 16241
							public static class HARDEST
							{
								// Token: 0x0400F440 RID: 62528
								public static LocString NAME = "Critical Mass";

								// Token: 0x0400F441 RID: 62529
								public static LocString TOOLTIP = "Duplicants feel ill at the merest mention of radiation...and may never truly recover";

								// Token: 0x0400F442 RID: 62530
								public static LocString ATTRIBUTE_MODIFIER_NAME = "Super Radiation (Highest Difficulty)";
							}

							// Token: 0x02003F72 RID: 16242
							public static class HARDER
							{
								// Token: 0x0400F443 RID: 62531
								public static LocString NAME = "Toxic Positivity";

								// Token: 0x0400F444 RID: 62532
								public static LocString TOOLTIP = "Duplicants are more sensitive to radiation exposure than usual";

								// Token: 0x0400F445 RID: 62533
								public static LocString ATTRIBUTE_MODIFIER_NAME = "Radiation Vulnerable (Difficulty Up)";
							}

							// Token: 0x02003F73 RID: 16243
							public static class DEFAULT
							{
								// Token: 0x0400F446 RID: 62534
								public static LocString NAME = "Default";

								// Token: 0x0400F447 RID: 62535
								public static LocString TOOLTIP = "Default radiation settings";
							}

							// Token: 0x02003F74 RID: 16244
							public static class EASIER
							{
								// Token: 0x0400F448 RID: 62536
								public static LocString NAME = "Healthy Glow";

								// Token: 0x0400F449 RID: 62537
								public static LocString TOOLTIP = "Duplicants are more resistant to radiation exposure than usual";

								// Token: 0x0400F44A RID: 62538
								public static LocString ATTRIBUTE_MODIFIER_NAME = "Radiation Shielded (Difficulty Down)";
							}

							// Token: 0x02003F75 RID: 16245
							public static class EASIEST
							{
								// Token: 0x0400F44B RID: 62539
								public static LocString NAME = "Nuke-Proof";

								// Token: 0x0400F44C RID: 62540
								public static LocString TOOLTIP = "Duplicants could bathe in radioactive waste and not even notice";

								// Token: 0x0400F44D RID: 62541
								public static LocString ATTRIBUTE_MODIFIER_NAME = "Radiation Protection (Lowest Difficulty)";
							}
						}
					}

					// Token: 0x02003EB4 RID: 16052
					public class STRESS
					{
						// Token: 0x0400F2A4 RID: 62116
						public static LocString NAME = "Stress";

						// Token: 0x0400F2A5 RID: 62117
						public static LocString TOOLTIP = "Affects how quickly Duplicant stress rises";

						// Token: 0x02003F37 RID: 16183
						public static class LEVELS
						{
							// Token: 0x02003F76 RID: 16246
							public static class INDOMITABLE
							{
								// Token: 0x0400F44E RID: 62542
								public static LocString NAME = "Cloud Nine";

								// Token: 0x0400F44F RID: 62543
								public static LocString TOOLTIP = "A strong emotional support system makes these Duplicants impervious to all stress";

								// Token: 0x0400F450 RID: 62544
								public static LocString ATTRIBUTE_MODIFIER_NAME = "Cloud Nine (No Stress)";
							}

							// Token: 0x02003F77 RID: 16247
							public static class OPTIMISTIC
							{
								// Token: 0x0400F451 RID: 62545
								public static LocString NAME = "Chipper";

								// Token: 0x0400F452 RID: 62546
								public static LocString TOOLTIP = "Duplicants gain stress slower than usual";

								// Token: 0x0400F453 RID: 62547
								public static LocString ATTRIBUTE_MODIFIER_NAME = "Chipper (Difficulty Down)";
							}

							// Token: 0x02003F78 RID: 16248
							public static class DEFAULT
							{
								// Token: 0x0400F454 RID: 62548
								public static LocString NAME = "Default";

								// Token: 0x0400F455 RID: 62549
								public static LocString TOOLTIP = "Default stress change rate";
							}

							// Token: 0x02003F79 RID: 16249
							public static class PESSIMISTIC
							{
								// Token: 0x0400F456 RID: 62550
								public static LocString NAME = "Glum";

								// Token: 0x0400F457 RID: 62551
								public static LocString TOOLTIP = "Duplicants gain stress more quickly than usual";

								// Token: 0x0400F458 RID: 62552
								public static LocString ATTRIBUTE_MODIFIER_NAME = "Glum (Difficulty Up)";
							}

							// Token: 0x02003F7A RID: 16250
							public static class DOOMED
							{
								// Token: 0x0400F459 RID: 62553
								public static LocString NAME = "Frankly Depressing";

								// Token: 0x0400F45A RID: 62554
								public static LocString TOOLTIP = "These Duplicants were never taught coping mechanisms... they're devastated by stress as a result";

								// Token: 0x0400F45B RID: 62555
								public static LocString ATTRIBUTE_MODIFIER_NAME = "Frankly Depressing (Highest Difficulty)";
							}
						}
					}

					// Token: 0x02003EB5 RID: 16053
					public class STRESS_BREAKS
					{
						// Token: 0x0400F2A6 RID: 62118
						public static LocString NAME = "Stress Reactions";

						// Token: 0x0400F2A7 RID: 62119
						public static LocString TOOLTIP = "Determines whether Duplicants wreak havoc on the colony when they reach maximum stress";

						// Token: 0x02003F38 RID: 16184
						public static class LEVELS
						{
							// Token: 0x02003F7B RID: 16251
							public static class DEFAULT
							{
								// Token: 0x0400F45C RID: 62556
								public static LocString NAME = "Enabled";

								// Token: 0x0400F45D RID: 62557
								public static LocString TOOLTIP = "Checked: Duplicants will wreak havoc when they reach 100% stress (Default)";
							}

							// Token: 0x02003F7C RID: 16252
							public static class DISABLED
							{
								// Token: 0x0400F45E RID: 62558
								public static LocString NAME = "Disabled";

								// Token: 0x0400F45F RID: 62559
								public static LocString TOOLTIP = "Unchecked: Duplicants will not wreak havoc at maximum stress";
							}
						}
					}

					// Token: 0x02003EB6 RID: 16054
					public class WORLDGEN_SEED
					{
						// Token: 0x0400F2A8 RID: 62120
						public static LocString NAME = "Worldgen Seed";

						// Token: 0x0400F2A9 RID: 62121
						public static LocString TOOLTIP = "This number chooses the procedural parameters that create your unique map\n\nWorldgen seeds can be copied and pasted so others can play a replica of your world configuration";

						// Token: 0x0400F2AA RID: 62122
						public static LocString FIXEDSEED = "This is a predetermined seed, and cannot be changed";
					}

					// Token: 0x02003EB7 RID: 16055
					public class TELEPORTERS
					{
						// Token: 0x0400F2AB RID: 62123
						public static LocString NAME = "Teleporters";

						// Token: 0x0400F2AC RID: 62124
						public static LocString TOOLTIP = "Determines whether teleporters will be spawned during Worldgen";

						// Token: 0x02003F39 RID: 16185
						public static class LEVELS
						{
							// Token: 0x02003F7D RID: 16253
							public static class ENABLED
							{
								// Token: 0x0400F460 RID: 62560
								public static LocString NAME = "Enabled";

								// Token: 0x0400F461 RID: 62561
								public static LocString TOOLTIP = "Checked: Teleporters will spawn during Worldgen (Default)";
							}

							// Token: 0x02003F7E RID: 16254
							public static class DISABLED
							{
								// Token: 0x0400F462 RID: 62562
								public static LocString NAME = "Disabled";

								// Token: 0x0400F463 RID: 62563
								public static LocString TOOLTIP = "Unchecked: No Teleporters will spawn during Worldgen";
							}
						}
					}

					// Token: 0x02003EB8 RID: 16056
					public class METEORSHOWERS
					{
						// Token: 0x0400F2AD RID: 62125
						public static LocString NAME = "Meteor Showers";

						// Token: 0x0400F2AE RID: 62126
						public static LocString TOOLTIP = "Adjusts the intensity of incoming space rocks";

						// Token: 0x02003F3A RID: 16186
						public static class LEVELS
						{
							// Token: 0x02003F7F RID: 16255
							public static class CLEAR_SKIES
							{
								// Token: 0x0400F464 RID: 62564
								public static LocString NAME = "Clear Skies";

								// Token: 0x0400F465 RID: 62565
								public static LocString TOOLTIP = "No meteor damage, no worries";
							}

							// Token: 0x02003F80 RID: 16256
							public static class INFREQUENT
							{
								// Token: 0x0400F466 RID: 62566
								public static LocString NAME = "Spring Showers";

								// Token: 0x0400F467 RID: 62567
								public static LocString TOOLTIP = "Meteor showers are less frequent and less intense than usual";
							}

							// Token: 0x02003F81 RID: 16257
							public static class DEFAULT
							{
								// Token: 0x0400F468 RID: 62568
								public static LocString NAME = "Default";

								// Token: 0x0400F469 RID: 62569
								public static LocString TOOLTIP = "Default meteor shower frequency and intensity";
							}

							// Token: 0x02003F82 RID: 16258
							public static class INTENSE
							{
								// Token: 0x0400F46A RID: 62570
								public static LocString NAME = "Cosmic Storm";

								// Token: 0x0400F46B RID: 62571
								public static LocString TOOLTIP = "Meteor showers are more frequent and more intense than usual";
							}

							// Token: 0x02003F83 RID: 16259
							public static class DOOMED
							{
								// Token: 0x0400F46C RID: 62572
								public static LocString NAME = "Doomsday";

								// Token: 0x0400F46D RID: 62573
								public static LocString TOOLTIP = "An onslaught of apocalyptic hailstorms that feels almost personal";
							}
						}
					}

					// Token: 0x02003EB9 RID: 16057
					public class DLC_MIXING
					{
						// Token: 0x02003F3B RID: 16187
						public static class LEVELS
						{
							// Token: 0x02003F84 RID: 16260
							public static class DISABLED
							{
								// Token: 0x0400F46E RID: 62574
								public static LocString NAME = "Disabled";

								// Token: 0x0400F46F RID: 62575
								public static LocString TOOLTIP = "Content from this DLC is currently <b>disabled</b>";
							}

							// Token: 0x02003F85 RID: 16261
							public static class ENABLED
							{
								// Token: 0x0400F470 RID: 62576
								public static LocString NAME = "Enabled";

								// Token: 0x0400F471 RID: 62577
								public static LocString TOOLTIP = "Content from this DLC is currently <b>enabled</b>\n\nThis includes Care Packages, buildings, and space POIs";
							}
						}
					}

					// Token: 0x02003EBA RID: 16058
					public class SUBWORLD_MIXING
					{
						// Token: 0x02003F3C RID: 16188
						public static class LEVELS
						{
							// Token: 0x02003F86 RID: 16262
							public static class DISABLED
							{
								// Token: 0x0400F472 RID: 62578
								public static LocString NAME = "Disabled";

								// Token: 0x0400F473 RID: 62579
								public static LocString TOOLTIP = "This biome will not be mixed into any world";

								// Token: 0x0400F474 RID: 62580
								public static LocString TOOLTIP_BASEGAME = "This biome will not be mixed in";
							}

							// Token: 0x02003F87 RID: 16263
							public static class TRY_MIXING
							{
								// Token: 0x0400F475 RID: 62581
								public static LocString NAME = "Likely";

								// Token: 0x0400F476 RID: 62582
								public static LocString TOOLTIP = "This biome is very likely to be mixed into a world";

								// Token: 0x0400F477 RID: 62583
								public static LocString TOOLTIP_BASEGAME = "This biome is very likely to be mixed in";
							}

							// Token: 0x02003F88 RID: 16264
							public static class GUARANTEE_MIXING
							{
								// Token: 0x0400F478 RID: 62584
								public static LocString NAME = "Guaranteed";

								// Token: 0x0400F479 RID: 62585
								public static LocString TOOLTIP = "This biome will be mixed into a world, even if it causes a worldgen failure";

								// Token: 0x0400F47A RID: 62586
								public static LocString TOOLTIP_BASEGAME = "This biome will be mixed in, even if it causes a worldgen failure";
							}
						}
					}

					// Token: 0x02003EBB RID: 16059
					public class WORLD_MIXING
					{
						// Token: 0x02003F3D RID: 16189
						public static class LEVELS
						{
							// Token: 0x02003F89 RID: 16265
							public static class DISABLED
							{
								// Token: 0x0400F47B RID: 62587
								public static LocString NAME = "Disabled";

								// Token: 0x0400F47C RID: 62588
								public static LocString TOOLTIP = "This asteroid will not be mixed in";
							}

							// Token: 0x02003F8A RID: 16266
							public static class TRY_MIXING
							{
								// Token: 0x0400F47D RID: 62589
								public static LocString NAME = "Likely";

								// Token: 0x0400F47E RID: 62590
								public static LocString TOOLTIP = "This asteroid is very likely to be mixed in";
							}

							// Token: 0x02003F8B RID: 16267
							public static class GUARANTEE_MIXING
							{
								// Token: 0x0400F47F RID: 62591
								public static LocString NAME = "Guaranteed";

								// Token: 0x0400F480 RID: 62592
								public static LocString TOOLTIP = "This asteroid will be mixed in, even if it causes worldgen failure";
							}
						}
					}
				}
			}

			// Token: 0x02002CF6 RID: 11510
			public class MAINMENU
			{
				// Token: 0x0400C205 RID: 49669
				public static LocString STARTDEMO = "START DEMO";

				// Token: 0x0400C206 RID: 49670
				public static LocString NEWGAME = "NEW GAME";

				// Token: 0x0400C207 RID: 49671
				public static LocString RESUMEGAME = "RESUME GAME";

				// Token: 0x0400C208 RID: 49672
				public static LocString LOADGAME = "LOAD GAME";

				// Token: 0x0400C209 RID: 49673
				public static LocString RETIREDCOLONIES = "COLONY SUMMARIES";

				// Token: 0x0400C20A RID: 49674
				public static LocString KLEIINVENTORY = "KLEI INVENTORY";

				// Token: 0x0400C20B RID: 49675
				public static LocString LOCKERMENU = "SUPPLY CLOSET";

				// Token: 0x0400C20C RID: 49676
				public static LocString SCENARIOS = "SCENARIOS";

				// Token: 0x0400C20D RID: 49677
				public static LocString TRANSLATIONS = "TRANSLATIONS";

				// Token: 0x0400C20E RID: 49678
				public static LocString OPTIONS = "OPTIONS";

				// Token: 0x0400C20F RID: 49679
				public static LocString QUITTODESKTOP = "QUIT";

				// Token: 0x0400C210 RID: 49680
				public static LocString RESTARTCONFIRM = "Should I really quit?\nAll unsaved progress will be lost.";

				// Token: 0x0400C211 RID: 49681
				public static LocString QUITCONFIRM = "Should I quit to the main menu?\nAll unsaved progress will be lost.";

				// Token: 0x0400C212 RID: 49682
				public static LocString RETIRECONFIRM = "Should I surrender under the soul-crushing weight of this universe's entropy and retire my colony?";

				// Token: 0x0400C213 RID: 49683
				public static LocString DESKTOPQUITCONFIRM = "Should I really quit?\nAll unsaved progress will be lost.";

				// Token: 0x0400C214 RID: 49684
				public static LocString RESUMEBUTTON_BASENAME = "{0}: Cycle {1}";

				// Token: 0x0400C215 RID: 49685
				public static LocString QUIT = "QUIT WITHOUT SAVING";

				// Token: 0x0400C216 RID: 49686
				public static LocString SAVEANDQUITTITLE = "SAVE AND QUIT";

				// Token: 0x0400C217 RID: 49687
				public static LocString SAVEANDQUITDESKTOP = "SAVE AND QUIT";

				// Token: 0x0400C218 RID: 49688
				public static LocString WISHLIST_AD = "Available now";

				// Token: 0x0400C219 RID: 49689
				public static LocString WISHLIST_AD_TOOLTIP = "<color=#ffff00ff><b>Click to view it in the store</b></color>";

				// Token: 0x020039F2 RID: 14834
				public class DLC
				{
					// Token: 0x0400E73E RID: 59198
					public static LocString ACTIVATE_EXPANSION1 = "ENABLE DLC";

					// Token: 0x0400E73F RID: 59199
					public static LocString ACTIVATE_EXPANSION1_TOOLTIP = "<b>This DLC is disabled</b>\n\n<color=#ffff00ff><b>Click to enable the <i>Spaced Out!</i> DLC</b></color>";

					// Token: 0x0400E740 RID: 59200
					public static LocString ACTIVATE_EXPANSION1_DESC = "The game will need to restart in order to enable <i>Spaced Out!</i>";

					// Token: 0x0400E741 RID: 59201
					public static LocString ACTIVATE_EXPANSION1_RAIL_DESC = "<i>Spaced Out!</i> will be enabled the next time you launch the game. The game will now close.";

					// Token: 0x0400E742 RID: 59202
					public static LocString DEACTIVATE_EXPANSION1 = "DISABLE DLC";

					// Token: 0x0400E743 RID: 59203
					public static LocString DEACTIVATE_EXPANSION1_TOOLTIP = "<b>This DLC is enabled</b>\n\n<color=#ffff00ff><b>Click to disable the <i>Spaced Out!</i> DLC</b></color>";

					// Token: 0x0400E744 RID: 59204
					public static LocString DEACTIVATE_EXPANSION1_DESC = "The game will need to restart in order to enable the <i>Oxygen Not Included</i> base game.";

					// Token: 0x0400E745 RID: 59205
					public static LocString DEACTIVATE_EXPANSION1_RAIL_DESC = "<i>Spaced Out!</i> will be disabled the next time you launch the game. The game will now close.";

					// Token: 0x0400E746 RID: 59206
					public static LocString AD_DLC1 = "Spaced Out! DLC";

					// Token: 0x0400E747 RID: 59207
					public static LocString CONTENT_INSTALLED_LABEL = "Installed";

					// Token: 0x0400E748 RID: 59208
					public static LocString CONTENT_ACTIVE_TOOLTIP = "<b>This DLC is enabled</b>\n\nFind it in the destination selection screen when starting a new game, or in the Load Game screen for existing DLC-enabled saves";

					// Token: 0x0400E749 RID: 59209
					public static LocString CONTENT_OWNED_NOTINSTALLED_LABEL = "";

					// Token: 0x0400E74A RID: 59210
					public static LocString CONTENT_OWNED_NOTINSTALLED_TOOLTIP = "This DLC is owned but not currently installed";

					// Token: 0x0400E74B RID: 59211
					public static LocString CONTENT_NOTOWNED_LABEL = "Available Now";

					// Token: 0x0400E74C RID: 59212
					public static LocString CONTENT_NOTOWNED_TOOLTIP = "This DLC is available now!";
				}
			}

			// Token: 0x02002CF7 RID: 11511
			public class DEVTOOLS
			{
				// Token: 0x0400C21A RID: 49690
				public static LocString TITLE = "About Dev Tools";

				// Token: 0x0400C21B RID: 49691
				public static LocString WARNING = "DANGER!!\n\nDev Tools are intended for developer use only. Using them may result in your save becoming unplayable, unstable, or severely damaged.\n\nThese tools are completely unsupported and may contain bugs. Are you sure you want to continue?";

				// Token: 0x0400C21C RID: 49692
				public static LocString DONTSHOW = "Do not show this message again";

				// Token: 0x0400C21D RID: 49693
				public static LocString BUTTON = "Show Dev Tools";
			}

			// Token: 0x02002CF8 RID: 11512
			public class NEWGAMESETTINGS
			{
				// Token: 0x0400C21E RID: 49694
				public static LocString HEADER = "GAME SETTINGS";

				// Token: 0x020039F3 RID: 14835
				public class BUTTONS
				{
					// Token: 0x0400E74D RID: 59213
					public static LocString STANDARDGAME = "Standard Game";

					// Token: 0x0400E74E RID: 59214
					public static LocString CUSTOMGAME = "Custom Game";

					// Token: 0x0400E74F RID: 59215
					public static LocString CANCEL = "Cancel";

					// Token: 0x0400E750 RID: 59216
					public static LocString STARTGAME = "Start Game";
				}
			}

			// Token: 0x02002CF9 RID: 11513
			public class COLONYDESTINATIONSCREEN
			{
				// Token: 0x0400C21F RID: 49695
				public static LocString TITLE = "CHOOSE A DESTINATION";

				// Token: 0x0400C220 RID: 49696
				public static LocString GENTLE_ZONE = "Habitable Zone";

				// Token: 0x0400C221 RID: 49697
				public static LocString DETAILS = "Destination Details";

				// Token: 0x0400C222 RID: 49698
				public static LocString START_SITE = "Immediate Surroundings";

				// Token: 0x0400C223 RID: 49699
				public static LocString COORDINATE = "Coordinates:";

				// Token: 0x0400C224 RID: 49700
				public static LocString CANCEL = "Back";

				// Token: 0x0400C225 RID: 49701
				public static LocString CUSTOMIZE = "Game Settings";

				// Token: 0x0400C226 RID: 49702
				public static LocString START_GAME = "Start Game";

				// Token: 0x0400C227 RID: 49703
				public static LocString SHUFFLE = "Shuffle";

				// Token: 0x0400C228 RID: 49704
				public static LocString SHUFFLETOOLTIP = "Reroll World Seed\n\nThis will shuffle the layout of your world and the geographical traits listed below";

				// Token: 0x0400C229 RID: 49705
				public static LocString SHUFFLETOOLTIP_DISABLED = "This world's seed is predetermined. It cannot be changed";

				// Token: 0x0400C22A RID: 49706
				public static LocString HEADER_ASTEROID_STARTING = "Starting Asteroid";

				// Token: 0x0400C22B RID: 49707
				public static LocString HEADER_ASTEROID_NEARBY = "Nearby Asteroids";

				// Token: 0x0400C22C RID: 49708
				public static LocString HEADER_ASTEROID_DISTANT = "Distant Asteroids";

				// Token: 0x0400C22D RID: 49709
				public static LocString TRAITS_HEADER = "World Traits";

				// Token: 0x0400C22E RID: 49710
				public static LocString STORY_TRAITS_HEADER = "Story Traits";

				// Token: 0x0400C22F RID: 49711
				public static LocString MIXING_SETTINGS_HEADER = "Scramble DLCs";

				// Token: 0x0400C230 RID: 49712
				public static LocString MIXING_DLC_HEADER = "DLC Content";

				// Token: 0x0400C231 RID: 49713
				public static LocString MIXING_WORLDMIXING_HEADER = "Asteroid Remix";

				// Token: 0x0400C232 RID: 49714
				public static LocString MIXING_SUBWORLDMIXING_HEADER = "Biome Remix";

				// Token: 0x0400C233 RID: 49715
				public static LocString MIXING_NO_OPTIONS = "No additional content currently available for remixing. Don't worry, there's plenty already baked in.";

				// Token: 0x0400C234 RID: 49716
				public static LocString MIXING_WARNING = "Choose additional content to remix into the game. Scrambling realities may cause cosmic collapse.";

				// Token: 0x0400C235 RID: 49717
				public static LocString MIXING_TOOLTIP_DLC_MIXING = "DLC content includes buildings, Care Packages, space POIs, critters, etc\n\nEnabling DLC content allows asteroid and biome remixes from that DLC to be customized in the sections below";

				// Token: 0x0400C236 RID: 49718
				public static LocString MIXING_TOOLTIP_ASTEROID_MIXING = "Asteroid remixing modifies which asteroids appear on the starmap\n\nRemixed asteroids will retain key features of the outer asteroids that they replace";

				// Token: 0x0400C237 RID: 49719
				public static LocString MIXING_TOOLTIP_BIOME_MIXING = "Biome remixing modifies which biomes will be included across multiple asteroids";

				// Token: 0x0400C238 RID: 49720
				public static LocString MIXING_TOOLTIP_TOO_MANY_GUARENTEED_ASTEROID_MIXINGS = UI.FRONTEND.COLONYDESTINATIONSCREEN.MIXING_TOOLTIP_ASTEROID_MIXING + "\n\nMaximum of {1} guaranteed asteroid remixes allowed\n\nTotal currently selected: {0}";

				// Token: 0x0400C239 RID: 49721
				public static LocString MIXING_TOOLTIP_TOO_MANY_GUARENTEED_BIOME_MIXINGS = UI.FRONTEND.COLONYDESTINATIONSCREEN.MIXING_TOOLTIP_BIOME_MIXING + "\n\nMaximum of {1} guaranteed biome remixes allowed\n\nTotal currently selected: {0}";

				// Token: 0x0400C23A RID: 49722
				public static LocString MIXING_TOOLTIP_LOCKED_START_NOT_SUPPORTED = "This destination does not support changing this setting";

				// Token: 0x0400C23B RID: 49723
				public static LocString MIXING_TOOLTIP_LOCKED_REQUIRE_DLC_NOT_ENABLED = "This setting requires the following content to be enabled:\n{0}";

				// Token: 0x0400C23C RID: 49724
				public static LocString MIXING_TOOLTIP_DLC_CONTENT = "This content is from {0}";

				// Token: 0x0400C23D RID: 49725
				public static LocString MIXING_TOOLTIP_MODDED_SETTING = "<i><color=#d6d6d6>This setting was added by a mod</color></i>";

				// Token: 0x0400C23E RID: 49726
				public static LocString MIXING_TOOLTIP_CANNOT_START = "Cannot start a new game with current asteroid and biome remix configuration";

				// Token: 0x0400C23F RID: 49727
				public static LocString NO_TRAITS = "No Traits";

				// Token: 0x0400C240 RID: 49728
				public static LocString SINGLE_TRAIT = "1 Trait";

				// Token: 0x0400C241 RID: 49729
				public static LocString TRAIT_COUNT = "{0} Traits";

				// Token: 0x0400C242 RID: 49730
				public static LocString TOO_MANY_TRAITS_WARNING = UI.YELLOW_PREFIX + "Too many!" + UI.COLOR_SUFFIX;

				// Token: 0x0400C243 RID: 49731
				public static LocString TOO_MANY_TRAITS_WARNING_TOOLTIP = UI.YELLOW_PREFIX + "Squeezing this many story traits into this asteroid may cause worldgen to fail\n\nConsider lowering the number of story traits or changing the selected asteroid" + UI.COLOR_SUFFIX;

				// Token: 0x0400C244 RID: 49732
				public static LocString SHUFFLE_STORY_TRAITS_TOOLTIP = "Randomize Story Traits\n\nThis will select a comfortable number of story traits for the starting asteroid";

				// Token: 0x0400C245 RID: 49733
				public static LocString SELECTED_CLUSTER_TRAITS_HEADER = "Target Details";
			}

			// Token: 0x02002CFA RID: 11514
			public class MODESELECTSCREEN
			{
				// Token: 0x0400C246 RID: 49734
				public static LocString HEADER = "GAME MODE";

				// Token: 0x0400C247 RID: 49735
				public static LocString BLANK_DESC = "Select a playstyle...";

				// Token: 0x0400C248 RID: 49736
				public static LocString SURVIVAL_TITLE = "SURVIVAL";

				// Token: 0x0400C249 RID: 49737
				public static LocString SURVIVAL_DESC = "Stay on your toes and one step ahead of this unforgiving world. One slip up could bring your colony crashing down.";

				// Token: 0x0400C24A RID: 49738
				public static LocString NOSWEAT_TITLE = "NO SWEAT";

				// Token: 0x0400C24B RID: 49739
				public static LocString NOSWEAT_DESC = "When disaster strikes (and it inevitably will), take a deep breath and stay calm. You have ample time to find a solution.";

				// Token: 0x0400C24C RID: 49740
				public static LocString ACTIVE_CONTENT_HEADER = "ACTIVE CONTENT";
			}

			// Token: 0x02002CFB RID: 11515
			public class CLUSTERCATEGORYSELECTSCREEN
			{
				// Token: 0x0400C24D RID: 49741
				public static LocString HEADER = "ASTEROID STYLE";

				// Token: 0x0400C24E RID: 49742
				public static LocString BLANK_DESC = "Select an asteroid style...";

				// Token: 0x0400C24F RID: 49743
				public static LocString VANILLA_TITLE = "Standard";

				// Token: 0x0400C250 RID: 49744
				public static LocString VANILLA_DESC = "Scenarios designed for classic gameplay.";

				// Token: 0x0400C251 RID: 49745
				public static LocString CLASSIC_TITLE = "Classic";

				// Token: 0x0400C252 RID: 49746
				public static LocString CLASSIC_DESC = "Scenarios similar to the <b>classic Oxygen Not Included</b> experience. Large starting asteroids with many resources.\nLess emphasis on space travel.";

				// Token: 0x0400C253 RID: 49747
				public static LocString SPACEDOUT_TITLE = "Spaced Out!";

				// Token: 0x0400C254 RID: 49748
				public static LocString SPACEDOUT_DESC = "Scenarios designed for the <b>Spaced Out! DLC</b>.\nSmaller starting asteroids with resources distributed across the starmap. More emphasis on space travel.";

				// Token: 0x0400C255 RID: 49749
				public static LocString EVENT_TITLE = "The Lab";

				// Token: 0x0400C256 RID: 49750
				public static LocString EVENT_DESC = "Alternative gameplay experiences, including experimental scenarios designed for special events.";
			}

			// Token: 0x02002CFC RID: 11516
			public class PATCHNOTESSCREEN
			{
				// Token: 0x0400C257 RID: 49751
				public static LocString HEADER = "IMPORTANT UPDATE NOTES";

				// Token: 0x0400C258 RID: 49752
				public static LocString OK_BUTTON = "OK";

				// Token: 0x0400C259 RID: 49753
				public static LocString FULLPATCHNOTES_TOOLTIP = "View the full patch notes online";
			}

			// Token: 0x02002CFD RID: 11517
			public class LOADSCREEN
			{
				// Token: 0x0400C25A RID: 49754
				public static LocString TITLE = "LOAD GAME";

				// Token: 0x0400C25B RID: 49755
				public static LocString TITLE_INSPECT = "LOAD GAME";

				// Token: 0x0400C25C RID: 49756
				public static LocString DELETEBUTTON = "DELETE";

				// Token: 0x0400C25D RID: 49757
				public static LocString BACKBUTTON = "< BACK";

				// Token: 0x0400C25E RID: 49758
				public static LocString CONFIRMDELETE = "Are you sure you want to delete {0}?\nYou cannot undo this action.";

				// Token: 0x0400C25F RID: 49759
				public static LocString SAVEDETAILS = "<b>File:</b> {0}\n\n<b>Save Date:</b>\n{1}\n\n<b>Base Name:</b> {2}\n<b>Duplicants Alive:</b> {3}\n<b>Cycle(s) Survived:</b> {4}";

				// Token: 0x0400C260 RID: 49760
				public static LocString AUTOSAVEWARNING = "<color=#F44A47FF>Autosave: This file will get deleted as new autosaves are created</color>";

				// Token: 0x0400C261 RID: 49761
				public static LocString CORRUPTEDSAVE = "<b><color=#F44A47FF>Could not load file {0}. Its data may be corrupted.</color></b>";

				// Token: 0x0400C262 RID: 49762
				public static LocString SAVE_TOO_NEW = "<b><color=#F44A47FF>Could not load file {0}. File is using build {1}, v{2}. This build is {3}, v{4}.</color></b>";

				// Token: 0x0400C263 RID: 49763
				public static LocString TOOLTIP_SAVE_INCOMPATABLE_DLC_CONFIGURATION = "This save file was created with a different DLC configuration\n\nTo load this file:";

				// Token: 0x0400C264 RID: 49764
				public static LocString TOOLTIP_SAVE_INCOMPATABLE_DLC_CONFIGURATION_ASK_TO_ENABLE = "    • Activate {0}";

				// Token: 0x0400C265 RID: 49765
				public static LocString TOOLTIP_SAVE_INCOMPATABLE_DLC_CONFIGURATION_ASK_TO_DISABLE = "    • Deactivate {0}";

				// Token: 0x0400C266 RID: 49766
				public static LocString TOOLTIP_SAVE_USES_DLC = "{0} save";

				// Token: 0x0400C267 RID: 49767
				public static LocString UNSUPPORTED_SAVE_VERSION = "<b><color=#F44A47FF>This save file is from a previous version of the game and is no longer supported.</color></b>";

				// Token: 0x0400C268 RID: 49768
				public static LocString MORE_INFO = "More Info";

				// Token: 0x0400C269 RID: 49769
				public static LocString NEWEST_SAVE = "NEWEST";

				// Token: 0x0400C26A RID: 49770
				public static LocString BASE_NAME = "Base Name";

				// Token: 0x0400C26B RID: 49771
				public static LocString CYCLES_SURVIVED = "Cycles Survived";

				// Token: 0x0400C26C RID: 49772
				public static LocString DUPLICANTS_ALIVE = "Duplicants Alive";

				// Token: 0x0400C26D RID: 49773
				public static LocString WORLD_NAME = "Asteroid Type";

				// Token: 0x0400C26E RID: 49774
				public static LocString NO_FILE_SELECTED = "No file selected";

				// Token: 0x0400C26F RID: 49775
				public static LocString COLONY_INFO_FMT = "{0}: {1}";

				// Token: 0x0400C270 RID: 49776
				public static LocString LOAD_MORE_COLONIES_BUTTON = "Load more...";

				// Token: 0x0400C271 RID: 49777
				public static LocString VANILLA_RESTART = "Loading this colony will require restarting the game with " + UI.DLC1.NAME_ITAL + " content disabled";

				// Token: 0x0400C272 RID: 49778
				public static LocString EXPANSION1_RESTART = "Loading this colony will require restarting the game with " + UI.DLC1.NAME_ITAL + " content enabled";

				// Token: 0x0400C273 RID: 49779
				public static LocString UNSUPPORTED_VANILLA_TEMP = "<b><color=#F44A47FF>This save file is from the base version of the game and currently cannot be loaded while " + UI.DLC1.NAME_ITAL + " is installed.</color></b>";

				// Token: 0x0400C274 RID: 49780
				public static LocString CONTENT = "Content";

				// Token: 0x0400C275 RID: 49781
				public static LocString VANILLA_CONTENT = "Vanilla FIXME";

				// Token: 0x0400C276 RID: 49782
				public static LocString EXPANSION1_CONTENT = UI.DLC1.NAME_ITAL + " Expansion FIXME";

				// Token: 0x0400C277 RID: 49783
				public static LocString SAVE_INFO = "{0} saves  {1} autosaves  {2}";

				// Token: 0x0400C278 RID: 49784
				public static LocString COLONIES_TITLE = "Colony View";

				// Token: 0x0400C279 RID: 49785
				public static LocString COLONY_TITLE = "Viewing colony '{0}'";

				// Token: 0x0400C27A RID: 49786
				public static LocString COLONY_FILE_SIZE = "Size: {0}";

				// Token: 0x0400C27B RID: 49787
				public static LocString COLONY_FILE_NAME = "File: '{0}'";

				// Token: 0x0400C27C RID: 49788
				public static LocString NO_PREVIEW = "NO PREVIEW";

				// Token: 0x0400C27D RID: 49789
				public static LocString LOCAL_SAVE = "local";

				// Token: 0x0400C27E RID: 49790
				public static LocString CLOUD_SAVE = "cloud";

				// Token: 0x0400C27F RID: 49791
				public static LocString CONVERT_COLONY = "CONVERT COLONY";

				// Token: 0x0400C280 RID: 49792
				public static LocString CONVERT_ALL_COLONIES = "CONVERT ALL";

				// Token: 0x0400C281 RID: 49793
				public static LocString CONVERT_ALL_WARNING = UI.PRE_KEYWORD + "\nWarning:" + UI.PST_KEYWORD + " Converting all colonies may take some time.";

				// Token: 0x0400C282 RID: 49794
				public static LocString SAVE_INFO_DIALOG_TITLE = "SAVE INFORMATION";

				// Token: 0x0400C283 RID: 49795
				public static LocString SAVE_INFO_DIALOG_TEXT = "Access your save files using the options below.";

				// Token: 0x0400C284 RID: 49796
				public static LocString SAVE_INFO_DIALOG_TOOLTIP = "Access your save file locations from here.";

				// Token: 0x0400C285 RID: 49797
				public static LocString CONVERT_ERROR_TITLE = "SAVE CONVERSION UNSUCCESSFUL";

				// Token: 0x0400C286 RID: 49798
				public static LocString CONVERT_ERROR = string.Concat(new string[]
				{
					"Converting the colony ",
					UI.PRE_KEYWORD,
					"{Colony}",
					UI.PST_KEYWORD,
					" was unsuccessful!\nThe error was:\n\n<b>{Error}</b>\n\nPlease try again, or post a bug in the forums if this problem keeps happening."
				});

				// Token: 0x0400C287 RID: 49799
				public static LocString CONVERT_TO_CLOUD = "CONVERT TO CLOUD SAVES";

				// Token: 0x0400C288 RID: 49800
				public static LocString CONVERT_TO_LOCAL = "CONVERT TO LOCAL SAVES";

				// Token: 0x0400C289 RID: 49801
				public static LocString CONVERT_COLONY_TO_CLOUD = "Convert colony to use cloud saves";

				// Token: 0x0400C28A RID: 49802
				public static LocString CONVERT_COLONY_TO_LOCAL = "Convert to colony to use local saves";

				// Token: 0x0400C28B RID: 49803
				public static LocString CONVERT_ALL_TO_CLOUD = "Convert <b>all</b> colonies below to use cloud saves";

				// Token: 0x0400C28C RID: 49804
				public static LocString CONVERT_ALL_TO_LOCAL = "Convert <b>all</b> colonies below to use local saves";

				// Token: 0x0400C28D RID: 49805
				public static LocString CONVERT_ALL_TO_CLOUD_SUCCESS = string.Concat(new string[]
				{
					UI.PRE_KEYWORD,
					"SUCCESS!",
					UI.PST_KEYWORD,
					"\nAll existing colonies have been converted into ",
					UI.PRE_KEYWORD,
					"cloud",
					UI.PST_KEYWORD,
					" saves.\nNew colonies will use ",
					UI.PRE_KEYWORD,
					"cloud",
					UI.PST_KEYWORD,
					" saves by default.\n\n{Client} may take longer than usual to sync the next time you exit the game as a result of this change."
				});

				// Token: 0x0400C28E RID: 49806
				public static LocString CONVERT_ALL_TO_LOCAL_SUCCESS = string.Concat(new string[]
				{
					UI.PRE_KEYWORD,
					"SUCCESS!",
					UI.PST_KEYWORD,
					"\nAll existing colonies have been converted into ",
					UI.PRE_KEYWORD,
					"local",
					UI.PST_KEYWORD,
					" saves.\nNew colonies will use ",
					UI.PRE_KEYWORD,
					"local",
					UI.PST_KEYWORD,
					" saves by default.\n\n{Client} may take longer than usual to sync the next time you exit the game as a result of this change."
				});

				// Token: 0x0400C28F RID: 49807
				public static LocString CONVERT_TO_CLOUD_DETAILS = "Converting a colony to use cloud saves will move all of the save files for that colony into the cloud saves folder.\n\nThis allows your game platform to sync this colony to the cloud for your account, so it can be played on multiple machines.";

				// Token: 0x0400C290 RID: 49808
				public static LocString CONVERT_TO_LOCAL_DETAILS = "Converting a colony to NOT use cloud saves will move all of the save files for that colony into the local saves folder.\n\n" + UI.PRE_KEYWORD + "These save files will no longer be synced to the cloud." + UI.PST_KEYWORD;

				// Token: 0x0400C291 RID: 49809
				public static LocString OPEN_SAVE_FOLDER = "LOCAL SAVES";

				// Token: 0x0400C292 RID: 49810
				public static LocString OPEN_CLOUDSAVE_FOLDER = "CLOUD SAVES";

				// Token: 0x0400C293 RID: 49811
				public static LocString MIGRATE_TITLE = "SAVE FILE MIGRATION";

				// Token: 0x0400C294 RID: 49812
				public static LocString MIGRATE_SAVE_FILES = "MIGRATE SAVE FILES";

				// Token: 0x0400C295 RID: 49813
				public static LocString MIGRATE_COUNT = string.Concat(new string[]
				{
					"\nFound ",
					UI.PRE_KEYWORD,
					"{0}",
					UI.PST_KEYWORD,
					" saves and ",
					UI.PRE_KEYWORD,
					"{1}",
					UI.PST_KEYWORD,
					" autosaves that require migration."
				});

				// Token: 0x0400C296 RID: 49814
				public static LocString MIGRATE_RESULT = string.Concat(new string[]
				{
					UI.PRE_KEYWORD,
					"SUCCESS!",
					UI.PST_KEYWORD,
					"\nMigration moved ",
					UI.PRE_KEYWORD,
					"{0}/{1}",
					UI.PST_KEYWORD,
					" saves and ",
					UI.PRE_KEYWORD,
					"{2}/{3}",
					UI.PST_KEYWORD,
					" autosaves",
					UI.PST_KEYWORD,
					"."
				});

				// Token: 0x0400C297 RID: 49815
				public static LocString MIGRATE_RESULT_FAILURES = string.Concat(new string[]
				{
					UI.PRE_KEYWORD,
					"<b>WARNING:</b> Not all saves could be migrated.",
					UI.PST_KEYWORD,
					"\nMigration moved ",
					UI.PRE_KEYWORD,
					"{0}/{1}",
					UI.PST_KEYWORD,
					" saves and ",
					UI.PRE_KEYWORD,
					"{2}/{3}",
					UI.PST_KEYWORD,
					" autosaves.\n\nThe file ",
					UI.PRE_KEYWORD,
					"{ErrorColony}",
					UI.PST_KEYWORD,
					" encountered this error:\n\n<b>{ErrorMessage}</b>"
				});

				// Token: 0x0400C298 RID: 49816
				public static LocString MIGRATE_RESULT_FAILURES_MORE_INFO_TITLE = "MIGRATION INCOMPLETE";

				// Token: 0x0400C299 RID: 49817
				public static LocString MIGRATE_RESULT_FAILURES_MORE_INFO_PRE = "<b>The game was unable to move all save files to their new location.\nTo fix this, please:</b>\n\n";

				// Token: 0x0400C29A RID: 49818
				public static LocString MIGRATE_RESULT_FAILURES_MORE_INFO_ITEM1 = "    1. Try temporarily disabling virus scanners and malware\n         protection programs.";

				// Token: 0x0400C29B RID: 49819
				public static LocString MIGRATE_RESULT_FAILURES_MORE_INFO_ITEM2 = "    2. Turn off file sync services such as OneDrive and DropBox.";

				// Token: 0x0400C29C RID: 49820
				public static LocString MIGRATE_RESULT_FAILURES_MORE_INFO_ITEM3 = "    3. Restart the game to retry file migration.";

				// Token: 0x0400C29D RID: 49821
				public static LocString MIGRATE_RESULT_FAILURES_MORE_INFO_POST = "\n<b>If this still doesn't solve the problem, please post a bug in the forums and we will attempt to assist with your issue.</b>";

				// Token: 0x0400C29E RID: 49822
				public static LocString MIGRATE_INFO = "We've changed how save files are organized!\nPlease " + UI.CLICK(UI.ClickType.click) + " the button below to automatically update your save file storage.";

				// Token: 0x0400C29F RID: 49823
				public static LocString MIGRATE_DONE = "CONTINUE";

				// Token: 0x0400C2A0 RID: 49824
				public static LocString MIGRATE_FAILURES_FORUM_BUTTON = "VISIT FORUMS";

				// Token: 0x0400C2A1 RID: 49825
				public static LocString MIGRATE_FAILURES_DONE = "MORE INFO";

				// Token: 0x0400C2A2 RID: 49826
				public static LocString CLOUD_TUTORIAL_BOUNCER = "Upload Saves to Cloud";
			}

			// Token: 0x02002CFE RID: 11518
			public class SAVESCREEN
			{
				// Token: 0x0400C2A3 RID: 49827
				public static LocString TITLE = "SAVE SLOTS";

				// Token: 0x0400C2A4 RID: 49828
				public static LocString NEWSAVEBUTTON = "New Save";

				// Token: 0x0400C2A5 RID: 49829
				public static LocString OVERWRITEMESSAGE = "Are you sure you want to overwrite {0}?";

				// Token: 0x0400C2A6 RID: 49830
				public static LocString SAVENAMETITLE = "SAVE NAME";

				// Token: 0x0400C2A7 RID: 49831
				public static LocString CONFIRMNAME = "Confirm";

				// Token: 0x0400C2A8 RID: 49832
				public static LocString CANCELNAME = "Cancel";

				// Token: 0x0400C2A9 RID: 49833
				public static LocString IO_ERROR = "An error occurred trying to save your game. Please ensure there is sufficient disk space.\n\n{0}";

				// Token: 0x0400C2AA RID: 49834
				public static LocString REPORT_BUG = "Report Bug";

				// Token: 0x0400C2AB RID: 49835
				public static LocString SAVE_COMPLETE_MESSAGE = "Save Complete";
			}

			// Token: 0x02002CFF RID: 11519
			public class RAILFORCEQUIT
			{
				// Token: 0x0400C2AC RID: 49836
				public static LocString SAVE_EXIT = "Play time has expired and the game is exiting. Would you like to overwrite {0}?";

				// Token: 0x0400C2AD RID: 49837
				public static LocString WARN_EXIT = "Play time has expired and the game will now exit.";

				// Token: 0x0400C2AE RID: 49838
				public static LocString DLC_NOT_PURCHASED = "The <i>Spaced Out!</i> DLC has not yet been purchased in the WeGame store. Purchase <i>Spaced Out!</i> to support <i>Oxygen Not Included</i> and enjoy the new content!";
			}

			// Token: 0x02002D00 RID: 11520
			public class MOD_ERRORS
			{
				// Token: 0x0400C2AF RID: 49839
				public static LocString TITLE = "MOD ERRORS";

				// Token: 0x0400C2B0 RID: 49840
				public static LocString DETAILS = "DETAILS";

				// Token: 0x0400C2B1 RID: 49841
				public static LocString CLOSE = "CLOSE";
			}

			// Token: 0x02002D01 RID: 11521
			public class MODS
			{
				// Token: 0x0400C2B2 RID: 49842
				public static LocString TITLE = "MODS";

				// Token: 0x0400C2B3 RID: 49843
				public static LocString MANAGE = "Subscription";

				// Token: 0x0400C2B4 RID: 49844
				public static LocString MANAGE_LOCAL = "Browse";

				// Token: 0x0400C2B5 RID: 49845
				public static LocString WORKSHOP = "STEAM WORKSHOP";

				// Token: 0x0400C2B6 RID: 49846
				public static LocString ENABLE_ALL = "ENABLE ALL";

				// Token: 0x0400C2B7 RID: 49847
				public static LocString DISABLE_ALL = "DISABLE ALL";

				// Token: 0x0400C2B8 RID: 49848
				public static LocString DRAG_TO_REORDER = "Drag to reorder";

				// Token: 0x0400C2B9 RID: 49849
				public static LocString REQUIRES_RESTART = "Mod changes require restart";

				// Token: 0x0400C2BA RID: 49850
				public static LocString FAILED_TO_LOAD = "A mod failed to load and is being disabled:\n\n{0}: {1}\n\n{2}";

				// Token: 0x0400C2BB RID: 49851
				public static LocString DB_CORRUPT = "An error occurred trying to load the Mod Database.\n\n{0}";

				// Token: 0x0400C2BC RID: 49852
				public static LocString NO_DESCRIPTION = "No description found in mod.yaml";

				// Token: 0x020039F4 RID: 14836
				public class CONTENT_FAILURE
				{
					// Token: 0x0400E751 RID: 59217
					public static LocString DISABLED_CONTENT = " - <b>Incompatible DLC configuration</b>";

					// Token: 0x0400E752 RID: 59218
					public static LocString DISABLED_CONTENT_TOOLTIP = "The current configuration of enabled/disabled DLCs does not match this mod's specifications.";

					// Token: 0x0400E753 RID: 59219
					public static LocString DISABLED_CONTENT_TOOLTIP_REQUIRED = "<b>Required DLCs:</b>";

					// Token: 0x0400E754 RID: 59220
					public static LocString DISABLED_CONTENT_TOOLTIP_FORBIDDEN_DLC = "<b>Forbidden DLCs:</b>";

					// Token: 0x0400E755 RID: 59221
					public static LocString NO_CONTENT = " - <b>No compatible mod found</b>";

					// Token: 0x0400E756 RID: 59222
					public static LocString NO_CONTENT_TOOLTIP = "No content was found to load.";

					// Token: 0x0400E757 RID: 59223
					public static LocString OLD_API = " - <b>Mod out-of-date</b>";

					// Token: 0x0400E758 RID: 59224
					public static LocString OLD_API_TOOLTIP = "This mod is outdated.";
				}

				// Token: 0x020039F5 RID: 14837
				public class TOOLTIPS
				{
					// Token: 0x0400E759 RID: 59225
					public static LocString ENABLED = "Enabled";

					// Token: 0x0400E75A RID: 59226
					public static LocString DISABLED = "Disabled";

					// Token: 0x0400E75B RID: 59227
					public static LocString MANAGE_STEAM_SUBSCRIPTION = "Manage Steam Subscription";

					// Token: 0x0400E75C RID: 59228
					public static LocString MANAGE_RAIL_SUBSCRIPTION = "Manage Subscription";

					// Token: 0x0400E75D RID: 59229
					public static LocString MANAGE_LOCAL_MOD = "Manage Local Mod";
				}

				// Token: 0x020039F6 RID: 14838
				public class RAILMODUPLOAD
				{
					// Token: 0x0400E75E RID: 59230
					public static LocString TITLE = "Upload Mod";

					// Token: 0x0400E75F RID: 59231
					public static LocString NAME = "Mod Name";

					// Token: 0x0400E760 RID: 59232
					public static LocString DESCRIPTION = "Mod Description";

					// Token: 0x0400E761 RID: 59233
					public static LocString VERSION = "Version Number";

					// Token: 0x0400E762 RID: 59234
					public static LocString PREVIEW_IMAGE = "Preview Image Path";

					// Token: 0x0400E763 RID: 59235
					public static LocString CONTENT_FOLDER = "Content Folder Path";

					// Token: 0x0400E764 RID: 59236
					public static LocString SHARE_TYPE = "Share Type";

					// Token: 0x0400E765 RID: 59237
					public static LocString SUBMIT = "Submit";

					// Token: 0x0400E766 RID: 59238
					public static LocString SUBMIT_READY = "This mod is ready to submit";

					// Token: 0x0400E767 RID: 59239
					public static LocString SUBMIT_NOT_READY = "The mod cannot be submitted. Check that all fields are properly entered and that the paths are valid.";

					// Token: 0x02003EBC RID: 16060
					public static class MOD_SHARE_TYPE
					{
						// Token: 0x0400F2AF RID: 62127
						public static LocString PRIVATE = "Private";

						// Token: 0x0400F2B0 RID: 62128
						public static LocString TOOLTIP_PRIVATE = "This mod will only be visible to its creator";

						// Token: 0x0400F2B1 RID: 62129
						public static LocString FRIEND = "Friend";

						// Token: 0x0400F2B2 RID: 62130
						public static LocString TOOLTIP_FRIEND = "Friend";

						// Token: 0x0400F2B3 RID: 62131
						public static LocString PUBLIC = "Public";

						// Token: 0x0400F2B4 RID: 62132
						public static LocString TOOLTIP_PUBLIC = "This mod will be available to all players after publishing. It may be subject to review before being allowed to be published.";
					}

					// Token: 0x02003EBD RID: 16061
					public static class MOD_UPLOAD_RESULT
					{
						// Token: 0x0400F2B5 RID: 62133
						public static LocString SUCCESS = "Mod upload succeeded.";

						// Token: 0x0400F2B6 RID: 62134
						public static LocString FAILURE = "Mod upload failed.";
					}
				}
			}

			// Token: 0x02002D02 RID: 11522
			public class MOD_EVENTS
			{
				// Token: 0x0400C2BD RID: 49853
				public static LocString REQUIRED = "REQUIRED";

				// Token: 0x0400C2BE RID: 49854
				public static LocString NOT_FOUND = "NOT FOUND";

				// Token: 0x0400C2BF RID: 49855
				public static LocString INSTALL_INFO_INACCESSIBLE = "INACCESSIBLE";

				// Token: 0x0400C2C0 RID: 49856
				public static LocString OUT_OF_ORDER = "ORDERING CHANGED";

				// Token: 0x0400C2C1 RID: 49857
				public static LocString ACTIVE_DURING_CRASH = "ACTIVE DURING CRASH";

				// Token: 0x0400C2C2 RID: 49858
				public static LocString EXPECTED_ENABLED = "NEWLY DISABLED";

				// Token: 0x0400C2C3 RID: 49859
				public static LocString EXPECTED_DISABLED = "NEWLY ENABLED";

				// Token: 0x0400C2C4 RID: 49860
				public static LocString VERSION_UPDATE = "VERSION UPDATE";

				// Token: 0x0400C2C5 RID: 49861
				public static LocString AVAILABLE_CONTENT_CHANGED = "CONTENT CHANGED";

				// Token: 0x0400C2C6 RID: 49862
				public static LocString INSTALL_FAILED = "INSTALL FAILED";

				// Token: 0x0400C2C7 RID: 49863
				public static LocString DOWNLOAD_FAILED = "STEAM DOWNLOAD FAILED";

				// Token: 0x0400C2C8 RID: 49864
				public static LocString INSTALLED = "INSTALLED";

				// Token: 0x0400C2C9 RID: 49865
				public static LocString UNINSTALLED = "UNINSTALLED";

				// Token: 0x0400C2CA RID: 49866
				public static LocString REQUIRES_RESTART = "RESTART REQUIRED";

				// Token: 0x0400C2CB RID: 49867
				public static LocString BAD_WORLD_GEN = "LOAD FAILED";

				// Token: 0x0400C2CC RID: 49868
				public static LocString DEACTIVATED = "DEACTIVATED";

				// Token: 0x0400C2CD RID: 49869
				public static LocString ALL_MODS_DISABLED_EARLY_ACCESS = "DEACTIVATED";

				// Token: 0x020039F7 RID: 14839
				public class TOOLTIPS
				{
					// Token: 0x0400E768 RID: 59240
					public static LocString REQUIRED = "The current save game couldn't load this mod. Unexpected things may happen!";

					// Token: 0x0400E769 RID: 59241
					public static LocString NOT_FOUND = "This mod isn't installed";

					// Token: 0x0400E76A RID: 59242
					public static LocString INSTALL_INFO_INACCESSIBLE = "Mod files are inaccessible";

					// Token: 0x0400E76B RID: 59243
					public static LocString OUT_OF_ORDER = "Active mod has changed order with respect to some other active mod";

					// Token: 0x0400E76C RID: 59244
					public static LocString ACTIVE_DURING_CRASH = "Mod was active during a crash and may be the cause";

					// Token: 0x0400E76D RID: 59245
					public static LocString EXPECTED_ENABLED = "This mod needs to be enabled";

					// Token: 0x0400E76E RID: 59246
					public static LocString EXPECTED_DISABLED = "This mod needs to be disabled";

					// Token: 0x0400E76F RID: 59247
					public static LocString VERSION_UPDATE = "New version detected";

					// Token: 0x0400E770 RID: 59248
					public static LocString AVAILABLE_CONTENT_CHANGED = "Content added or removed";

					// Token: 0x0400E771 RID: 59249
					public static LocString INSTALL_FAILED = "Installation failed";

					// Token: 0x0400E772 RID: 59250
					public static LocString DOWNLOAD_FAILED = "Steam failed to download the mod";

					// Token: 0x0400E773 RID: 59251
					public static LocString INSTALLED = "Installation succeeded";

					// Token: 0x0400E774 RID: 59252
					public static LocString UNINSTALLED = "Uninstalled";

					// Token: 0x0400E775 RID: 59253
					public static LocString BAD_WORLD_GEN = "Encountered an error while loading file";

					// Token: 0x0400E776 RID: 59254
					public static LocString DEACTIVATED = "Deactivated due to errors";

					// Token: 0x0400E777 RID: 59255
					public static LocString ALL_MODS_DISABLED_EARLY_ACCESS = "Deactivated due to Early Access for " + UI.DLC1.NAME_ITAL;
				}
			}

			// Token: 0x02002D03 RID: 11523
			public class MOD_DIALOGS
			{
				// Token: 0x0400C2CE RID: 49870
				public static LocString ADDITIONAL_MOD_EVENTS = "(...additional entries omitted)";

				// Token: 0x020039F8 RID: 14840
				public class INSTALL_INFO_INACCESSIBLE
				{
					// Token: 0x0400E778 RID: 59256
					public static LocString TITLE = "STEAM CONTENT ERROR";

					// Token: 0x0400E779 RID: 59257
					public static LocString MESSAGE = "Failed to access local Steam files for mod {0}.\nTry restarting Oxygen not Included.\nIf that doesn't work, try re-subscribing to the mod via Steam.";
				}

				// Token: 0x020039F9 RID: 14841
				public class STEAM_SUBSCRIBED
				{
					// Token: 0x0400E77A RID: 59258
					public static LocString TITLE = "STEAM MOD SUBSCRIBED";

					// Token: 0x0400E77B RID: 59259
					public static LocString MESSAGE = "Subscribed to Steam mod: {0}";
				}

				// Token: 0x020039FA RID: 14842
				public class STEAM_UPDATED
				{
					// Token: 0x0400E77C RID: 59260
					public static LocString TITLE = "STEAM MOD UPDATE";

					// Token: 0x0400E77D RID: 59261
					public static LocString MESSAGE = "Updating version of Steam mod: {0}";
				}

				// Token: 0x020039FB RID: 14843
				public class STEAM_UNSUBSCRIBED
				{
					// Token: 0x0400E77E RID: 59262
					public static LocString TITLE = "STEAM MOD UNSUBSCRIBED";

					// Token: 0x0400E77F RID: 59263
					public static LocString MESSAGE = "Unsubscribed from Steam mod: {0}";
				}

				// Token: 0x020039FC RID: 14844
				public class STEAM_REFRESH
				{
					// Token: 0x0400E780 RID: 59264
					public static LocString TITLE = "STEAM MODS REFRESHED";

					// Token: 0x0400E781 RID: 59265
					public static LocString MESSAGE = "Refreshed Steam mods:\n{0}";
				}

				// Token: 0x020039FD RID: 14845
				public class ALL_MODS_DISABLED_EARLY_ACCESS
				{
					// Token: 0x0400E782 RID: 59266
					public static LocString TITLE = "ALL MODS DISABLED";

					// Token: 0x0400E783 RID: 59267
					public static LocString MESSAGE = "Mod support is temporarily suspended for the initial launch of " + UI.DLC1.NAME_ITAL + " into Early Access:\n{0}";
				}

				// Token: 0x020039FE RID: 14846
				public class LOAD_FAILURE
				{
					// Token: 0x0400E784 RID: 59268
					public static LocString TITLE = "LOAD FAILURE";

					// Token: 0x0400E785 RID: 59269
					public static LocString MESSAGE = "Failed to load one or more mods:\n{0}\nThey will be re-installed when the game is restarted.\nGame may be unstable until restarted.";
				}

				// Token: 0x020039FF RID: 14847
				public class SAVE_GAME_MODS_DIFFER
				{
					// Token: 0x0400E786 RID: 59270
					public static LocString TITLE = "MOD DIFFERENCES";

					// Token: 0x0400E787 RID: 59271
					public static LocString MESSAGE = "Save game mods differ from currently active mods:\n{0}";
				}

				// Token: 0x02003A00 RID: 14848
				public class MOD_ERRORS_ON_BOOT
				{
					// Token: 0x0400E788 RID: 59272
					public static LocString TITLE = "MOD ERRORS";

					// Token: 0x0400E789 RID: 59273
					public static LocString MESSAGE = "An error occurred during start-up with mods active.\nAll mods have been disabled to ensure a clean restart.\n{0}";

					// Token: 0x0400E78A RID: 59274
					public static LocString DEV_MESSAGE = "An error occurred during start-up with mods active.\n{0}\nDisable all mods and restart, or continue in an unstable state?";
				}

				// Token: 0x02003A01 RID: 14849
				public class MODS_SCREEN_CHANGES
				{
					// Token: 0x0400E78B RID: 59275
					public static LocString TITLE = "MODS CHANGED";

					// Token: 0x0400E78C RID: 59276
					public static LocString MESSAGE = "{0}\nRestart required to reload mods.\nGame may be unstable until restarted.";
				}

				// Token: 0x02003A02 RID: 14850
				public class MOD_EVENTS
				{
					// Token: 0x0400E78D RID: 59277
					public static LocString TITLE = "MOD EVENTS";

					// Token: 0x0400E78E RID: 59278
					public static LocString MESSAGE = "{0}";

					// Token: 0x0400E78F RID: 59279
					public static LocString DEV_MESSAGE = "{0}\nCheck Player.log for details.";
				}

				// Token: 0x02003A03 RID: 14851
				public class RESTART
				{
					// Token: 0x0400E790 RID: 59280
					public static LocString OK = "RESTART";

					// Token: 0x0400E791 RID: 59281
					public static LocString CANCEL = "CONTINUE";

					// Token: 0x0400E792 RID: 59282
					public static LocString MESSAGE = "{0}\nRestart required.";

					// Token: 0x0400E793 RID: 59283
					public static LocString DEV_MESSAGE = "{0}\nRestart required.\nGame may be unstable until restarted.";
				}
			}

			// Token: 0x02002D04 RID: 11524
			public class PAUSE_SCREEN
			{
				// Token: 0x0400C2CF RID: 49871
				public static LocString TITLE = "PAUSED";

				// Token: 0x0400C2D0 RID: 49872
				public static LocString RESUME = "Resume";

				// Token: 0x0400C2D1 RID: 49873
				public static LocString LOGBOOK = "Logbook";

				// Token: 0x0400C2D2 RID: 49874
				public static LocString OPTIONS = "Options";

				// Token: 0x0400C2D3 RID: 49875
				public static LocString SAVE = "Save";

				// Token: 0x0400C2D4 RID: 49876
				public static LocString ALREADY_SAVED = "<i><color=#CAC8C8>Already Saved</color></i>";

				// Token: 0x0400C2D5 RID: 49877
				public static LocString SAVEAS = "Save As";

				// Token: 0x0400C2D6 RID: 49878
				public static LocString COLONY_SUMMARY = "Colony Summary";

				// Token: 0x0400C2D7 RID: 49879
				public static LocString LOCKERMENU = "Supply Closet";

				// Token: 0x0400C2D8 RID: 49880
				public static LocString LOAD = "Load";

				// Token: 0x0400C2D9 RID: 49881
				public static LocString QUIT = "Main Menu";

				// Token: 0x0400C2DA RID: 49882
				public static LocString DESKTOPQUIT = "Quit to Desktop";

				// Token: 0x0400C2DB RID: 49883
				public static LocString WORLD_SEED = "Coordinates: {0}";

				// Token: 0x0400C2DC RID: 49884
				public static LocString WORLD_SEED_TOOLTIP = "Share coordinates with a friend and they can start a colony on an identical asteroid!\n\n{0} - The asteroid\n\n{1} - The world seed\n\n{2} - Difficulty and Custom settings\n\n{3} - Story Trait settings\n\n{4} - Scramble DLC settings";

				// Token: 0x0400C2DD RID: 49885
				public static LocString WORLD_SEED_COPY_TOOLTIP = "Copy Coordinates to clipboard\n\nShare coordinates with a friend and they can start a colony on an identical asteroid!";

				// Token: 0x0400C2DE RID: 49886
				public static LocString MANAGEMENT_BUTTON = "Pause Menu";

				// Token: 0x02003A04 RID: 14852
				public class ADD_DLC_MENU
				{
					// Token: 0x0400E794 RID: 59284
					public static LocString ENABLE_QUESTION = "Enable DLC content on this save?\n\nThis will create a new copy of the save game. It will no longer be possible to load this copy without the DLC enabled.";

					// Token: 0x0400E795 RID: 59285
					public static LocString CONFIRM = "CONFIRM";

					// Token: 0x0400E796 RID: 59286
					public static LocString DLC_ENABLED_TOOLTIP = "This save has content from <b>{0}</b> DLC enabled";

					// Token: 0x0400E797 RID: 59287
					public static LocString DLC_DISABLED_TOOLTIP = "This save does not currently have content from <b>{0}</b> DLC enabled \n\n<b>Click to enable it</b>";

					// Token: 0x0400E798 RID: 59288
					public static LocString DLC_DISABLED_NOT_EDITABLE_TOOLTIP = "This save does not have content from the <b>{0}</b> DLC enabled";
				}
			}

			// Token: 0x02002D05 RID: 11525
			public class OPTIONS_SCREEN
			{
				// Token: 0x0400C2DF RID: 49887
				public static LocString TITLE = "OPTIONS";

				// Token: 0x0400C2E0 RID: 49888
				public static LocString GRAPHICS = "Graphics";

				// Token: 0x0400C2E1 RID: 49889
				public static LocString AUDIO = "Audio";

				// Token: 0x0400C2E2 RID: 49890
				public static LocString GAME = "Game";

				// Token: 0x0400C2E3 RID: 49891
				public static LocString CONTROLS = "Controls";

				// Token: 0x0400C2E4 RID: 49892
				public static LocString UNITS = "Temperature Units";

				// Token: 0x0400C2E5 RID: 49893
				public static LocString METRICS = "Data Communication";

				// Token: 0x0400C2E6 RID: 49894
				public static LocString LANGUAGE = "Change Language";

				// Token: 0x0400C2E7 RID: 49895
				public static LocString WORLD_GEN = "World Generation Key";

				// Token: 0x0400C2E8 RID: 49896
				public static LocString RESET_TUTORIAL = "Reset Tutorial Messages";

				// Token: 0x0400C2E9 RID: 49897
				public static LocString RESET_TUTORIAL_WARNING = "All tutorial messages will be reset, and\nwill show up again the next time you play the game.";

				// Token: 0x0400C2EA RID: 49898
				public static LocString FEEDBACK = "Feedback";

				// Token: 0x0400C2EB RID: 49899
				public static LocString CREDITS = "Credits";

				// Token: 0x0400C2EC RID: 49900
				public static LocString BACK = "Done";

				// Token: 0x0400C2ED RID: 49901
				public static LocString UNLOCK_SANDBOX = "Unlock Sandbox Mode";

				// Token: 0x0400C2EE RID: 49902
				public static LocString MODS = "MODS";

				// Token: 0x0400C2EF RID: 49903
				public static LocString SAVE_OPTIONS = "Save Options";

				// Token: 0x02003A05 RID: 14853
				public class TOGGLE_SANDBOX_SCREEN
				{
					// Token: 0x0400E799 RID: 59289
					public static LocString UNLOCK_SANDBOX_WARNING = "Sandbox Mode will be enabled for this save file";

					// Token: 0x0400E79A RID: 59290
					public static LocString CONFIRM = "Enable Sandbox Mode";

					// Token: 0x0400E79B RID: 59291
					public static LocString CANCEL = "Cancel";

					// Token: 0x0400E79C RID: 59292
					public static LocString CONFIRM_SAVE_BACKUP = "Enable Sandbox Mode, but save a backup first";

					// Token: 0x0400E79D RID: 59293
					public static LocString BACKUP_SAVE_GAME_APPEND = " (BACKUP)";
				}
			}

			// Token: 0x02002D06 RID: 11526
			public class INPUT_BINDINGS_SCREEN
			{
				// Token: 0x0400C2F0 RID: 49904
				public static LocString TITLE = "CUSTOMIZE KEYS";

				// Token: 0x0400C2F1 RID: 49905
				public static LocString RESET = "Reset";

				// Token: 0x0400C2F2 RID: 49906
				public static LocString APPLY = "Done";

				// Token: 0x0400C2F3 RID: 49907
				public static LocString DUPLICATE = "{0} was already bound to {1} and is now unbound.";

				// Token: 0x0400C2F4 RID: 49908
				public static LocString UNBOUND_ACTION = "{0} is unbound. Are you sure you want to continue?";

				// Token: 0x0400C2F5 RID: 49909
				public static LocString MULTIPLE_UNBOUND_ACTIONS = "You have multiple unbound actions, this may result in difficulty playing the game. Are you sure you want to continue?";

				// Token: 0x0400C2F6 RID: 49910
				public static LocString WAITING_FOR_INPUT = "???";
			}

			// Token: 0x02002D07 RID: 11527
			public class TRANSLATIONS_SCREEN
			{
				// Token: 0x0400C2F7 RID: 49911
				public static LocString TITLE = "TRANSLATIONS";

				// Token: 0x0400C2F8 RID: 49912
				public static LocString UNINSTALL = "Uninstall";

				// Token: 0x0400C2F9 RID: 49913
				public static LocString PREINSTALLED_HEADER = "Preinstalled Language Packs";

				// Token: 0x0400C2FA RID: 49914
				public static LocString UGC_HEADER = "Subscribed Workshop Language Packs";

				// Token: 0x0400C2FB RID: 49915
				public static LocString UGC_MOD_TITLE_FORMAT = "{0} (workshop)";

				// Token: 0x0400C2FC RID: 49916
				public static LocString ARE_YOU_SURE = "Are you sure you want to uninstall this language pack?";

				// Token: 0x0400C2FD RID: 49917
				public static LocString PLEASE_REBOOT = "Please restart your game for these changes to take effect.";

				// Token: 0x0400C2FE RID: 49918
				public static LocString NO_PACKS = "Steam Workshop";

				// Token: 0x0400C2FF RID: 49919
				public static LocString DOWNLOAD = "Start Download";

				// Token: 0x0400C300 RID: 49920
				public static LocString INSTALL = "Install";

				// Token: 0x0400C301 RID: 49921
				public static LocString INSTALLED = "Installed";

				// Token: 0x0400C302 RID: 49922
				public static LocString NO_STEAM = "Unable to retrieve language list from Steam";

				// Token: 0x0400C303 RID: 49923
				public static LocString RESTART = "RESTART";

				// Token: 0x0400C304 RID: 49924
				public static LocString CANCEL = "CANCEL";

				// Token: 0x0400C305 RID: 49925
				public static LocString MISSING_LANGUAGE_PACK = "Selected language pack ({0}) not found.\nReverting to default language.";

				// Token: 0x0400C306 RID: 49926
				public static LocString UNKNOWN = "Unknown";

				// Token: 0x02003A06 RID: 14854
				public class PREINSTALLED_LANGUAGES
				{
					// Token: 0x0400E79E RID: 59294
					public static LocString EN = "English (Klei)";

					// Token: 0x0400E79F RID: 59295
					public static LocString ZH_KLEI = "Chinese (Klei)";

					// Token: 0x0400E7A0 RID: 59296
					public static LocString KO_KLEI = "Korean (Klei)";

					// Token: 0x0400E7A1 RID: 59297
					public static LocString RU_KLEI = "Russian (Klei)";
				}
			}

			// Token: 0x02002D08 RID: 11528
			public class SCENARIOS_MENU
			{
				// Token: 0x0400C307 RID: 49927
				public static LocString TITLE = "Scenarios";

				// Token: 0x0400C308 RID: 49928
				public static LocString UNSUBSCRIBE = "Unsubscribe";

				// Token: 0x0400C309 RID: 49929
				public static LocString UNSUBSCRIBE_CONFIRM = "Are you sure you want to unsubscribe from this scenario?";

				// Token: 0x0400C30A RID: 49930
				public static LocString LOAD_SCENARIO_CONFIRM = "Load the \"{SCENARIO_NAME}\" scenario?";

				// Token: 0x0400C30B RID: 49931
				public static LocString LOAD_CONFIRM_TITLE = "LOAD";

				// Token: 0x0400C30C RID: 49932
				public static LocString SCENARIO_NAME = "Name:";

				// Token: 0x0400C30D RID: 49933
				public static LocString SCENARIO_DESCRIPTION = "Description";

				// Token: 0x0400C30E RID: 49934
				public static LocString BUTTON_DONE = "Done";

				// Token: 0x0400C30F RID: 49935
				public static LocString BUTTON_LOAD = "Load";

				// Token: 0x0400C310 RID: 49936
				public static LocString BUTTON_WORKSHOP = "Steam Workshop";

				// Token: 0x0400C311 RID: 49937
				public static LocString NO_SCENARIOS_AVAILABLE = "No scenarios available.\n\nSubscribe to some in the Steam Workshop.";
			}

			// Token: 0x02002D09 RID: 11529
			public class AUDIO_OPTIONS_SCREEN
			{
				// Token: 0x0400C312 RID: 49938
				public static LocString TITLE = "AUDIO OPTIONS";

				// Token: 0x0400C313 RID: 49939
				public static LocString HEADER_VOLUME = "VOLUME";

				// Token: 0x0400C314 RID: 49940
				public static LocString HEADER_SETTINGS = "SETTINGS";

				// Token: 0x0400C315 RID: 49941
				public static LocString DONE_BUTTON = "Done";

				// Token: 0x0400C316 RID: 49942
				public static LocString MUSIC_EVERY_CYCLE = "Play background music each morning";

				// Token: 0x0400C317 RID: 49943
				public static LocString MUSIC_EVERY_CYCLE_TOOLTIP = "If enabled, background music will play every cycle instead of every few cycles";

				// Token: 0x0400C318 RID: 49944
				public static LocString AUTOMATION_SOUNDS_ALWAYS = "Always play automation sounds";

				// Token: 0x0400C319 RID: 49945
				public static LocString AUTOMATION_SOUNDS_ALWAYS_TOOLTIP = "If enabled, automation sound effects will play even when outside of the " + UI.FormatAsOverlay("Automation Overlay");

				// Token: 0x0400C31A RID: 49946
				public static LocString MUTE_ON_FOCUS_LOST = "Mute when unfocused";

				// Token: 0x0400C31B RID: 49947
				public static LocString MUTE_ON_FOCUS_LOST_TOOLTIP = "If enabled, the game will be muted while minimized or if the application loses focus";

				// Token: 0x0400C31C RID: 49948
				public static LocString AUDIO_BUS_MASTER = "Master";

				// Token: 0x0400C31D RID: 49949
				public static LocString AUDIO_BUS_SFX = "SFX";

				// Token: 0x0400C31E RID: 49950
				public static LocString AUDIO_BUS_MUSIC = "Music";

				// Token: 0x0400C31F RID: 49951
				public static LocString AUDIO_BUS_AMBIENCE = "Ambience";

				// Token: 0x0400C320 RID: 49952
				public static LocString AUDIO_BUS_UI = "UI";
			}

			// Token: 0x02002D0A RID: 11530
			public class GAME_OPTIONS_SCREEN
			{
				// Token: 0x0400C321 RID: 49953
				public static LocString TITLE = "GAME OPTIONS";

				// Token: 0x0400C322 RID: 49954
				public static LocString GENERAL_GAME_OPTIONS = "GENERAL";

				// Token: 0x0400C323 RID: 49955
				public static LocString DISABLED_WARNING = "More options available in-game";

				// Token: 0x0400C324 RID: 49956
				public static LocString DEFAULT_TO_CLOUD_SAVES = "Default to cloud saves";

				// Token: 0x0400C325 RID: 49957
				public static LocString DEFAULT_TO_CLOUD_SAVES_TOOLTIP = "When a new colony is created, this controls whether it will be saved into the cloud saves folder for syncing or not.";

				// Token: 0x0400C326 RID: 49958
				public static LocString RESET_TUTORIAL_DESCRIPTION = "Mark all tutorial messages \"unread\"";

				// Token: 0x0400C327 RID: 49959
				public static LocString SANDBOX_DESCRIPTION = "Enable sandbox tools";

				// Token: 0x0400C328 RID: 49960
				public static LocString CONTROLS_DESCRIPTION = "Change key bindings";

				// Token: 0x0400C329 RID: 49961
				public static LocString TEMPERATURE_UNITS = "TEMPERATURE UNITS";

				// Token: 0x0400C32A RID: 49962
				public static LocString SAVE_OPTIONS = "SAVE";

				// Token: 0x0400C32B RID: 49963
				public static LocString CAMERA_SPEED_LABEL = "Camera Pan Speed: {0}%";
			}

			// Token: 0x02002D0B RID: 11531
			public class METRIC_OPTIONS_SCREEN
			{
				// Token: 0x0400C32C RID: 49964
				public static LocString TITLE = "DATA COMMUNICATION";

				// Token: 0x0400C32D RID: 49965
				public static LocString HEADER_METRICS = "USER DATA";
			}

			// Token: 0x02002D0C RID: 11532
			public class COLONY_SAVE_OPTIONS_SCREEN
			{
				// Token: 0x0400C32E RID: 49966
				public static LocString TITLE = "COLONY SAVE OPTIONS";

				// Token: 0x0400C32F RID: 49967
				public static LocString DESCRIPTION = "Note: These values are configured per save file";

				// Token: 0x0400C330 RID: 49968
				public static LocString AUTOSAVE_FREQUENCY = "Autosave frequency:";

				// Token: 0x0400C331 RID: 49969
				public static LocString AUTOSAVE_FREQUENCY_DESCRIPTION = "Every: {0} cycle(s)";

				// Token: 0x0400C332 RID: 49970
				public static LocString AUTOSAVE_NEVER = "Never";

				// Token: 0x0400C333 RID: 49971
				public static LocString TIMELAPSE_RESOLUTION = "Timelapse resolution:";

				// Token: 0x0400C334 RID: 49972
				public static LocString TIMELAPSE_RESOLUTION_DESCRIPTION = "{0}x{1}";

				// Token: 0x0400C335 RID: 49973
				public static LocString TIMELAPSE_DISABLED_DESCRIPTION = "Disabled";
			}

			// Token: 0x02002D0D RID: 11533
			public class FEEDBACK_SCREEN
			{
				// Token: 0x0400C336 RID: 49974
				public static LocString TITLE = "FEEDBACK";

				// Token: 0x0400C337 RID: 49975
				public static LocString HEADER = "We would love to hear from you!";

				// Token: 0x0400C338 RID: 49976
				public static LocString DESCRIPTION = "Let us know if you encounter any problems or how we can improve your Oxygen Not Included experience.\n\nWhen reporting a bug, please include your log and colony save file. The buttons to the right will help you find those files on your local drive.\n\nThank you for being part of the Oxygen Not Included community!";

				// Token: 0x0400C339 RID: 49977
				public static LocString ALT_DESCRIPTION = "Let us know if you encounter any problems or how we can improve your Oxygen Not Included experience.\n\nWhen reporting a bug, please include your log and colony save file.\n\nThank you for being part of the Oxygen Not Included community!";

				// Token: 0x0400C33A RID: 49978
				public static LocString BUG_FORUMS_BUTTON = "Report a Bug";

				// Token: 0x0400C33B RID: 49979
				public static LocString SUGGESTION_FORUMS_BUTTON = "Suggestions Forum";

				// Token: 0x0400C33C RID: 49980
				public static LocString LOGS_DIRECTORY_BUTTON = "Browse Log Files";

				// Token: 0x0400C33D RID: 49981
				public static LocString SAVE_FILES_DIRECTORY_BUTTON = "Browse Save Files";
			}

			// Token: 0x02002D0E RID: 11534
			public class WORLD_GEN_OPTIONS_SCREEN
			{
				// Token: 0x0400C33E RID: 49982
				public static LocString TITLE = "WORLD GENERATION OPTIONS";

				// Token: 0x0400C33F RID: 49983
				public static LocString USE_SEED = "Set Worldgen Seed";

				// Token: 0x0400C340 RID: 49984
				public static LocString DONE_BUTTON = "Done";

				// Token: 0x0400C341 RID: 49985
				public static LocString RANDOM_BUTTON = "Randomize";

				// Token: 0x0400C342 RID: 49986
				public static LocString RANDOM_BUTTON_TOOLTIP = "Randomize a new worldgen seed";

				// Token: 0x0400C343 RID: 49987
				public static LocString TOOLTIP = "This will override the current worldgen seed";
			}

			// Token: 0x02002D0F RID: 11535
			public class METRICS_OPTIONS_SCREEN
			{
				// Token: 0x0400C344 RID: 49988
				public static LocString TITLE = "DATA COMMUNICATION OPTIONS";

				// Token: 0x0400C345 RID: 49989
				public static LocString ENABLE_BUTTON = "Enable Data Communication";

				// Token: 0x0400C346 RID: 49990
				public static LocString DESCRIPTION = "Collecting user data helps us improve the game.\n\nPlayers who opt out of data communication will no longer send us crash reports and play data.\n\nThey will also be unable to receive new item unlocks from our servers, though existing unlocked items will continue to function.\n\nFor more details on our privacy policy and how we use the data we collect, please visit our <color=#ECA6C9><u><b>privacy center</b></u></color>.";

				// Token: 0x0400C347 RID: 49991
				public static LocString DONE_BUTTON = "Done";

				// Token: 0x0400C348 RID: 49992
				public static LocString RESTART_BUTTON = "Restart Game";

				// Token: 0x0400C349 RID: 49993
				public static LocString TOOLTIP = "Uncheck to disable data communication";

				// Token: 0x0400C34A RID: 49994
				public static LocString RESTART_WARNING = "A game restart is required to apply settings.";
			}

			// Token: 0x02002D10 RID: 11536
			public class UNIT_OPTIONS_SCREEN
			{
				// Token: 0x0400C34B RID: 49995
				public static LocString TITLE = "TEMPERATURE UNITS";

				// Token: 0x0400C34C RID: 49996
				public static LocString CELSIUS = "Celsius";

				// Token: 0x0400C34D RID: 49997
				public static LocString CELSIUS_TOOLTIP = "Change temperature unit to Celsius (°C)";

				// Token: 0x0400C34E RID: 49998
				public static LocString KELVIN = "Kelvin";

				// Token: 0x0400C34F RID: 49999
				public static LocString KELVIN_TOOLTIP = "Change temperature unit to Kelvin (K)";

				// Token: 0x0400C350 RID: 50000
				public static LocString FAHRENHEIT = "Fahrenheit";

				// Token: 0x0400C351 RID: 50001
				public static LocString FAHRENHEIT_TOOLTIP = "Change temperature unit to Fahrenheit (°F)";
			}

			// Token: 0x02002D11 RID: 11537
			public class GRAPHICS_OPTIONS_SCREEN
			{
				// Token: 0x0400C352 RID: 50002
				public static LocString TITLE = "GRAPHICS OPTIONS";

				// Token: 0x0400C353 RID: 50003
				public static LocString FULLSCREEN = "Fullscreen";

				// Token: 0x0400C354 RID: 50004
				public static LocString RESOLUTION = "Resolution:";

				// Token: 0x0400C355 RID: 50005
				public static LocString LOWRES = "Low Resolution Textures";

				// Token: 0x0400C356 RID: 50006
				public static LocString APPLYBUTTON = "Apply";

				// Token: 0x0400C357 RID: 50007
				public static LocString REVERTBUTTON = "Revert";

				// Token: 0x0400C358 RID: 50008
				public static LocString DONE_BUTTON = "Done";

				// Token: 0x0400C359 RID: 50009
				public static LocString UI_SCALE = "UI Scale";

				// Token: 0x0400C35A RID: 50010
				public static LocString HEADER_DISPLAY = "DISPLAY";

				// Token: 0x0400C35B RID: 50011
				public static LocString HEADER_UI = "INTERFACE";

				// Token: 0x0400C35C RID: 50012
				public static LocString COLORMODE = "Color Mode:";

				// Token: 0x0400C35D RID: 50013
				public static LocString COLOR_MODE_DEFAULT = "Default";

				// Token: 0x0400C35E RID: 50014
				public static LocString COLOR_MODE_PROTANOPIA = "Protanopia";

				// Token: 0x0400C35F RID: 50015
				public static LocString COLOR_MODE_DEUTERANOPIA = "Deuteranopia";

				// Token: 0x0400C360 RID: 50016
				public static LocString COLOR_MODE_TRITANOPIA = "Tritanopia";

				// Token: 0x0400C361 RID: 50017
				public static LocString ACCEPT_CHANGES = "Accept Changes?";

				// Token: 0x0400C362 RID: 50018
				public static LocString ACCEPT_CHANGES_STRING_COLOR = "Interface changes will be visible immediately, but applying color changes to in-game text will require a restart.\n\nAccept Changes?";

				// Token: 0x0400C363 RID: 50019
				public static LocString COLORBLIND_FEEDBACK = "Color blindness options are currently in progress.\n\nIf you would benefit from an alternative color mode or have had difficulties with any of the default colors, please visit the forums and let us know about your experiences.\n\nYour feedback is extremely helpful to us!";

				// Token: 0x0400C364 RID: 50020
				public static LocString COLORBLIND_FEEDBACK_BUTTON = "Provide Feedback";
			}

			// Token: 0x02002D12 RID: 11538
			public class WORLDGENSCREEN
			{
				// Token: 0x0400C365 RID: 50021
				public static LocString TITLE = "NEW GAME";

				// Token: 0x0400C366 RID: 50022
				public static LocString GENERATINGWORLD = "GENERATING WORLD";

				// Token: 0x0400C367 RID: 50023
				public static LocString SELECTSIZEPROMPT = "A new world is about to be created. Please select its size.";

				// Token: 0x0400C368 RID: 50024
				public static LocString LOADINGGAME = "LOADING WORLD...";

				// Token: 0x02003A07 RID: 14855
				public class SIZES
				{
					// Token: 0x0400E7A2 RID: 59298
					public static LocString TINY = "Tiny";

					// Token: 0x0400E7A3 RID: 59299
					public static LocString SMALL = "Small";

					// Token: 0x0400E7A4 RID: 59300
					public static LocString STANDARD = "Standard";

					// Token: 0x0400E7A5 RID: 59301
					public static LocString LARGE = "Big";

					// Token: 0x0400E7A6 RID: 59302
					public static LocString HUGE = "Colossal";
				}
			}

			// Token: 0x02002D13 RID: 11539
			public class MINSPECSCREEN
			{
				// Token: 0x0400C369 RID: 50025
				public static LocString TITLE = "WARNING!";

				// Token: 0x0400C36A RID: 50026
				public static LocString SIMFAILEDTOLOAD = "A problem occurred loading Oxygen Not Included. This is usually caused by the Visual Studio C++ 2015 runtime being improperly installed on the system. Please exit the game, run Windows Update, and try re-launching Oxygen Not Included.";

				// Token: 0x0400C36B RID: 50027
				public static LocString BODY = "We've detected that this computer does not meet the minimum requirements to run Oxygen Not Included. While you may continue with your current specs, the game might not run smoothly for you.\n\nPlease be aware that your experience may suffer as a result.";

				// Token: 0x0400C36C RID: 50028
				public static LocString OKBUTTON = "Okay, thanks!";

				// Token: 0x0400C36D RID: 50029
				public static LocString QUITBUTTON = "Quit";
			}

			// Token: 0x02002D14 RID: 11540
			public class SUPPORTWARNINGS
			{
				// Token: 0x0400C36E RID: 50030
				public static LocString AUDIO_DRIVERS = "A problem occurred initializing your audio device.\nSorry about that!\n\nThis is usually caused by outdated audio drivers.\n\nPlease visit your audio device manufacturer's website to download the latest drivers.";

				// Token: 0x0400C36F RID: 50031
				public static LocString AUDIO_DRIVERS_MORE_INFO = "More Info";

				// Token: 0x0400C370 RID: 50032
				public static LocString DUPLICATE_KEY_BINDINGS = "<b>Duplicate key bindings were detected.\nThis may be because your custom key bindings conflicted with a new feature's default key.\nPlease visit the controls screen to ensure your key bindings are set how you like them.</b>\n{0}";

				// Token: 0x0400C371 RID: 50033
				public static LocString SAVE_DIRECTORY_READ_ONLY = "A problem occurred while accessing your save directory.\nThis may be because your directory is set to read-only.\n\nPlease ensure your save directory is readable as well as writable and re-launch the game.\n{0}";

				// Token: 0x0400C372 RID: 50034
				public static LocString SAVE_DIRECTORY_INSUFFICIENT_SPACE = "There is insufficient disk space to write to your save directory.\n\nPlease free at least 15 MB to give your saves some room to breathe.\n{0}";

				// Token: 0x0400C373 RID: 50035
				public static LocString WORLD_GEN_FILES = "A problem occurred while accessing certain game files that will prevent starting new games.\n\nPlease ensure that the directory and files are readable as well as writable and re-launch the game:\n\n{0}";

				// Token: 0x0400C374 RID: 50036
				public static LocString WORLD_GEN_FAILURE = "A problem occurred while generating a world from this seed:\n{0}.\n\nUnfortunately, not all seeds germinate. Please try again with a different seed.";

				// Token: 0x0400C375 RID: 50037
				public static LocString WORLD_GEN_FAILURE_MIXING = "A problem occurred while trying to mix a world from this seed:\n{0}.\n\nUnfortunately, not all seeds germinate. Please try again with different remix settings or a different seed.";

				// Token: 0x0400C376 RID: 50038
				public static LocString WORLD_GEN_FAILURE_STORY = "A problem occurred while generating a world from this seed:\n{0}.\n\nNot all story traits were able to be placed. Please try again with a different seed or fewer story traits.";

				// Token: 0x0400C377 RID: 50039
				public static LocString PLAYER_PREFS_CORRUPTED = "A problem occurred while loading your game options.\nThey have been reset to their default settings.\n\n";

				// Token: 0x0400C378 RID: 50040
				public static LocString IO_UNAUTHORIZED = "An Unauthorized Access Error occurred when trying to write to disk.\n\nPlease check that you have permissions to write to:\n{0}\n\nThis may prevent the game from saving.";

				// Token: 0x0400C379 RID: 50041
				public static LocString IO_UNAUTHORIZED_ONEDRIVE = "An Unauthorized Access Error occurred when trying to write to disk.\n\nOneDrive may be interfering with the game.\n\nPlease check that you have permissions to write to:\n{0}\n\nThis may prevent the game from saving.";

				// Token: 0x0400C37A RID: 50042
				public static LocString IO_SUFFICIENT_SPACE = "An Insufficient Space Error occurred when trying to write to disk. \n\nPlease free up some space.\n{0}";

				// Token: 0x0400C37B RID: 50043
				public static LocString IO_UNKNOWN = "An unknown error occurred when trying to write or access a file.\n{0}";

				// Token: 0x0400C37C RID: 50044
				public static LocString MORE_INFO_BUTTON = "More Info";
			}

			// Token: 0x02002D15 RID: 11541
			public class SAVEUPGRADEWARNINGS
			{
				// Token: 0x0400C37D RID: 50045
				public static LocString SUDDENMORALEHELPER_TITLE = "MORALE CHANGES";

				// Token: 0x0400C37E RID: 50046
				public static LocString SUDDENMORALEHELPER = "Welcome to the Expressive Upgrade! This update introduces a new Morale system that replaces Food and Decor Expectations that were found in previous versions of the game.\n\nThe game you are trying to load was created before this system was introduced, and will need to be updated. You may either:\n\n\n1) Enable the new Morale system in this save, removing Food and Decor Expectations. It's possible that when you load your save your old colony won't meet your Duplicants' new Morale needs, so they'll receive a 5 cycle Morale boost to give you time to adjust.\n\n2) Disable Morale in this save. The new Morale mechanics will still be visible, but won't affect your Duplicants' stress. Food and Decor expectations will no longer exist in this save.";

				// Token: 0x0400C37F RID: 50047
				public static LocString SUDDENMORALEHELPER_BUFF = "1) Bring on Morale!";

				// Token: 0x0400C380 RID: 50048
				public static LocString SUDDENMORALEHELPER_DISABLE = "2) Disable Morale";

				// Token: 0x0400C381 RID: 50049
				public static LocString NEWAUTOMATIONWARNING_TITLE = "AUTOMATION CHANGES";

				// Token: 0x0400C382 RID: 50050
				public static LocString NEWAUTOMATIONWARNING = "The following buildings have acquired new automation ports!\n\nTake a moment to check whether these buildings in your colony are now unintentionally connected to existing " + BUILDINGS.PREFABS.LOGICWIRE.NAME + "s.";

				// Token: 0x0400C383 RID: 50051
				public static LocString MERGEDOWNCHANGES_TITLE = "BREATH OF FRESH AIR UPDATE CHANGES";

				// Token: 0x0400C384 RID: 50052
				public static LocString MERGEDOWNCHANGES = "Oxygen Not Included has had a <b>major update</b> since this save file was created! In addition to the <b>multitude of bug fixes and quality-of-life features</b>, please pay attention to these changes which may affect your existing colony:";

				// Token: 0x0400C385 RID: 50053
				public static LocString MERGEDOWNCHANGES_FOOD = "•<indent=20px>Fridges are more effective for early-game food storage</indent>\n•<indent=20px><b>Both</b> freezing temperatures and a sterile gas are needed for <b>total food preservation</b>.</indent>";

				// Token: 0x0400C386 RID: 50054
				public static LocString MERGEDOWNCHANGES_AIRFILTER = "•<indent=20px>" + BUILDINGS.PREFABS.AIRFILTER.NAME + " now requires <b>5w Power</b>.</indent>\n•<indent=20px>Duplicants will get <b>Stinging Eyes</b> from gasses such as chlorine and hydrogen.</indent>";

				// Token: 0x0400C387 RID: 50055
				public static LocString MERGEDOWNCHANGES_SIMULATION = "•<indent=20px>Many <b>simulation bugs</b> have been fixed.</indent>\n•<indent=20px>This may <b>change the effectiveness</b> of certain contraptions and " + BUILDINGS.PREFABS.STEAMTURBINE2.NAME + " setups.</indent>";

				// Token: 0x0400C388 RID: 50056
				public static LocString MERGEDOWNCHANGES_BUILDINGS = "•<indent=20px>The <b>" + BUILDINGS.PREFABS.OXYGENMASKSTATION.NAME + "</b> has been added to aid early-game exploration.</indent>\n•<indent=20px>Use the new <b>Meter Valves</b> for precise control of resources in pipes.</indent>";

				// Token: 0x0400C389 RID: 50057
				public static LocString SPACESCANNERANDTELESCOPECHANGES_TITLE = "JUNE 2023 QoL UPDATE CHANGES";

				// Token: 0x0400C38A RID: 50058
				public static LocString SPACESCANNERANDTELESCOPECHANGES_SUMMARY = "There have been significant changes to <b>Space Scanners</b> and <b>Telescopes</b> since this save file was created!\n\nMeteor showers have been disabled for 20 cycles to provide time to adapt.";

				// Token: 0x0400C38B RID: 50059
				public static LocString SPACESCANNERANDTELESCOPECHANGES_WARNING = "Please note these changes which may affect your existing colony:\n\n";

				// Token: 0x0400C38C RID: 50060
				public static LocString SPACESCANNERANDTELESCOPECHANGES_SPACESCANNERS = "•<indent=20px>Automation is synced between all Space Scanners targeting the same object.</indent>\n•<indent=20px>Network quality based on the total percentage of sky covered.</indent>\n•<indent=20px>Industrial machinery no longer impacts network quality.</indent>";

				// Token: 0x0400C38D RID: 50061
				public static LocString SPACESCANNERANDTELESCOPECHANGES_TELESCOPES = "•<indent=20px>Telescopes have a symmetrical scanning range.</indent>\n•<indent=20px>Obstructions block visibility from the blocked tile out toward the outer edge of scanning range.</indent>";

				// Token: 0x0400C38E RID: 50062
				public static LocString U50_CHANGES_TITLE = "IMPORTANT CHANGES";

				// Token: 0x0400C38F RID: 50063
				public static LocString U50_CHANGES_SUMMARY = "There have been significant changes to critters since this save file was created! Please check on your ranches.";

				// Token: 0x0400C390 RID: 50064
				public static LocString U50_CHANGES_MOOD = "•<indent=20px>Critter moods have been expanded to include miserable and satisfied states: Miserable stops reproduction. Satisfied gives full metabolism and default reproduction.</indent>";

				// Token: 0x0400C391 RID: 50065
				public static LocString U50_CHANGES_PACU = "•<indent=20px>Pacus have received a number of bug fixes and changes affecting their reproduction: Now correctly Confined when flopping or in less than 8 tiles of liquid. Easier to feed due to a rebalanced diet.</indent>";

				// Token: 0x0400C392 RID: 50066
				public static LocString U50_CHANGES_SUITCHECKPOINTS = "•<indent=20px>Suit checkpoints now have an automation port to disable them so Duplicants can pass through. Some checkpoints may now be unintentionally connected to existing " + BUILDINGS.PREFABS.LOGICWIRE.NAME + "s.";

				// Token: 0x0400C393 RID: 50067
				public static LocString U50_CHANGES_METER_VALVES = "•<indent=20px>Meter valves no longer continuously reset when receiving a green signal.</indent>";
			}
		}

		// Token: 0x020023B9 RID: 9145
		public class SANDBOX_TOGGLE
		{
			// Token: 0x04009F53 RID: 40787
			public static LocString TOOLTIP_LOCKED = "<b>Sandbox Mode</b> must be unlocked in the options menu before it can be used. {Hotkey}";

			// Token: 0x04009F54 RID: 40788
			public static LocString TOOLTIP_UNLOCKED = "Toggle <b>Sandbox Mode</b> {Hotkey}";
		}

		// Token: 0x020023BA RID: 9146
		public class SKILLS_SCREEN
		{
			// Token: 0x04009F55 RID: 40789
			public static LocString CURRENT_MORALE = "Current Morale: {0}\nMorale Need: {1}";

			// Token: 0x04009F56 RID: 40790
			public static LocString SORT_BY_DUPLICANT = "Duplicants";

			// Token: 0x04009F57 RID: 40791
			public static LocString SORT_BY_MORALE = "Morale";

			// Token: 0x04009F58 RID: 40792
			public static LocString SORT_BY_EXPERIENCE = "Skill Points";

			// Token: 0x04009F59 RID: 40793
			public static LocString SORT_BY_SKILL_AVAILABLE = "Skill Points";

			// Token: 0x04009F5A RID: 40794
			public static LocString SORT_BY_HAT = "Hat";

			// Token: 0x04009F5B RID: 40795
			public static LocString SELECT_HAT = "<b>SELECT HAT</b>";

			// Token: 0x04009F5C RID: 40796
			public static LocString POINTS_AVAILABLE = "<b>SKILL POINTS AVAILABLE</b>";

			// Token: 0x04009F5D RID: 40797
			public static LocString MORALE = "<b>Morale</b>";

			// Token: 0x04009F5E RID: 40798
			public static LocString MORALE_EXPECTATION = "<b>Morale Need</b>";

			// Token: 0x04009F5F RID: 40799
			public static LocString EXPERIENCE = "EXPERIENCE TO NEXT LEVEL";

			// Token: 0x04009F60 RID: 40800
			public static LocString EXPERIENCE_TOOLTIP = "{0}exp to next Skill Point";

			// Token: 0x04009F61 RID: 40801
			public static LocString NOT_AVAILABLE = "Not available";

			// Token: 0x04009F62 RID: 40802
			public static LocString ASSIGNED_BOOSTERS_HEADER = "{0}'s Boosters";

			// Token: 0x04009F63 RID: 40803
			public static LocString ASSIGNED_BOOSTERS_COUNT_LABEL = "{0}/{1} boosters assigned";

			// Token: 0x04009F64 RID: 40804
			public static LocString AVAILABLE_BOOSTERS_LABEL = "{0} available";

			// Token: 0x04009F65 RID: 40805
			public static LocString ASSIGNED_BOOSTERS_LABEL = "{0} assigned";

			// Token: 0x04009F66 RID: 40806
			public static LocString BIONIC_UPGRADE_SLOT_LOCKED = "This booster slot is unavailable\n\nBooster slots can be unlocked using " + UI.PRE_KEYWORD + "Skill Points" + UI.PST_KEYWORD;

			// Token: 0x04009F67 RID: 40807
			public static LocString BIONIC_UPGRADE_SLOT_AVAILABLE = "No booster installed\n\nAssign a booster from the menu below or craft new boosters at the " + UI.FormatAsLink("Crafting Station", "CRAFTINGTABLE");

			// Token: 0x04009F68 RID: 40808
			public static LocString BIONIC_UPGRADE_SLOT_UNASSIGN = UI.CLICK(UI.ClickType.click) + " to unassign";

			// Token: 0x02002D16 RID: 11542
			public class ASSIGNMENT_REQUIREMENTS
			{
				// Token: 0x0400C394 RID: 50068
				public static LocString EXPECTATION_TARGET_SKILL = "Current Morale: {0}\nSkill Morale Needs: {1}";

				// Token: 0x0400C395 RID: 50069
				public static LocString EXPECTATION_ALERT_TARGET_SKILL = "{2}'s Current: {0} Morale\n{3} Minimum Morale: {1}";

				// Token: 0x0400C396 RID: 50070
				public static LocString EXPECTATION_ALERT_DESC_EXPECTATION = "This Duplicant's Morale is too low to handle the rigors of this position, which will cause them Stress over time.";

				// Token: 0x02003A08 RID: 14856
				public class SKILLGROUP_ENABLED
				{
					// Token: 0x0400E7A7 RID: 59303
					public static LocString NAME = "Can perform {0}";

					// Token: 0x0400E7A8 RID: 59304
					public static LocString DESCRIPTION = "Capable of performing <b>{0}</b> skills";
				}

				// Token: 0x02003A09 RID: 14857
				public class MASTERY
				{
					// Token: 0x0400E7A9 RID: 59305
					public static LocString CAN_MASTER = "{0} <b>can learn</b> {1}";

					// Token: 0x0400E7AA RID: 59306
					public static LocString HAS_MASTERED = "{0} has <b>already learned</b> {1}";

					// Token: 0x0400E7AB RID: 59307
					public static LocString CANNOT_MASTER = "{0} <b>cannot learn</b> {1}";

					// Token: 0x0400E7AC RID: 59308
					public static LocString STRESS_WARNING_MESSAGE = string.Concat(new string[]
					{
						"Learning {0} will put {1} into a ",
						UI.PRE_KEYWORD,
						"Morale",
						UI.PST_KEYWORD,
						" deficit and cause unnecessary ",
						UI.PRE_KEYWORD,
						"Stress",
						UI.PST_KEYWORD,
						"!"
					});

					// Token: 0x0400E7AD RID: 59309
					public static LocString REQUIRES_MORE_SKILL_POINTS = "    • Not enough " + UI.PRE_KEYWORD + "Skill Points" + UI.PST_KEYWORD;

					// Token: 0x0400E7AE RID: 59310
					public static LocString REQUIRES_PREVIOUS_SKILLS = "    • Missing prerequisite " + UI.PRE_KEYWORD + "Skill" + UI.PST_KEYWORD;

					// Token: 0x0400E7AF RID: 59311
					public static LocString PREVENTED_BY_TRAIT = string.Concat(new string[]
					{
						"    • This Duplicant possesses the ",
						UI.PRE_KEYWORD,
						"{0}",
						UI.PST_KEYWORD,
						" Trait and cannot learn this Skill"
					});

					// Token: 0x0400E7B0 RID: 59312
					public static LocString SKILL_APTITUDE = string.Concat(new string[]
					{
						"{0} is interested in {1} and will receive a ",
						UI.PRE_KEYWORD,
						"Morale",
						UI.PST_KEYWORD,
						" bonus for learning it!"
					});

					// Token: 0x0400E7B1 RID: 59313
					public static LocString SKILL_GRANTED = "{0} has been granted {1} by a Trait, but does not have increased " + UI.FormatAsKeyWord("Morale Requirements") + " from learning it";
				}
			}
		}

		// Token: 0x020023BB RID: 9147
		public class KLEI_INVENTORY_SCREEN
		{
			// Token: 0x04009F69 RID: 40809
			public static LocString OPEN_INVENTORY_BUTTON = "Open Klei Inventory";

			// Token: 0x04009F6A RID: 40810
			public static LocString ITEM_FACADE_FOR = "This blueprint works with any {ConfigProperName}.";

			// Token: 0x04009F6B RID: 40811
			public static LocString ARTABLE_ITEM_FACADE_FOR = "This blueprint works with any {ConfigProperName} of {ArtableQuality} quality.";

			// Token: 0x04009F6C RID: 40812
			public static LocString CLOTHING_ITEM_FACADE_FOR = "This blueprint can be used in any outfit.";

			// Token: 0x04009F6D RID: 40813
			public static LocString BALLOON_ARTIST_FACADE_FOR = "This blueprint can be used by any Balloon Artist.";

			// Token: 0x04009F6E RID: 40814
			public static LocString MONUMENT_PART_FACADE_FOR = "This blueprint can be used in any Great Monument.";

			// Token: 0x04009F6F RID: 40815
			public static LocString COLLECTION = "Part of {Collection} collection.";

			// Token: 0x04009F70 RID: 40816
			public static LocString COLLECTION_COMING_SOON = "Part of {Collection} collection. Coming soon!";

			// Token: 0x04009F71 RID: 40817
			public static LocString ITEM_RARITY_DETAILS = "{RarityName} quality.";

			// Token: 0x04009F72 RID: 40818
			public static LocString ITEM_PLAYER_OWNED_AMOUNT = "My colony has {OwnedCount} of these blueprints.";

			// Token: 0x04009F73 RID: 40819
			public static LocString ITEM_PLAYER_OWN_NONE = "My colony doesn't have any of these yet.";

			// Token: 0x04009F74 RID: 40820
			public static LocString ITEM_PLAYER_OWNED_AMOUNT_ICON = "x{OwnedCount}";

			// Token: 0x04009F75 RID: 40821
			public static LocString ITEM_PLAYER_UNLOCKED_BUT_UNOWNABLE = "This blueprint is part of my colony's permanent collection.";

			// Token: 0x04009F76 RID: 40822
			public static LocString ITEM_DLC_REQUIRED = "This blueprint is designed for the <i>Spaced Out!</i> DLC.";

			// Token: 0x04009F77 RID: 40823
			public static LocString ITEM_UNKNOWN_NAME = "Uh oh!";

			// Token: 0x04009F78 RID: 40824
			public static LocString ITEM_UNKNOWN_DESCRIPTION = "Hmm. Looks like this blueprint is missing from the supply closet. Perhaps due to a temporal anomaly...";

			// Token: 0x04009F79 RID: 40825
			public static LocString SEARCH_PLACEHOLDER = "Search";

			// Token: 0x04009F7A RID: 40826
			public static LocString CLEAR_SEARCH_BUTTON_TOOLTIP = "Clear search";

			// Token: 0x04009F7B RID: 40827
			public static LocString TOOLTIP_VIEW_ALL_ITEMS = "Filter: Showing all items\n\n" + UI.CLICK(UI.ClickType.Click) + " to toggle";

			// Token: 0x04009F7C RID: 40828
			public static LocString TOOLTIP_VIEW_OWNED_ONLY = "Filter: Showing owned items only\n\n" + UI.CLICK(UI.ClickType.Click) + " to toggle";

			// Token: 0x04009F7D RID: 40829
			public static LocString TOOLTIP_VIEW_DOUBLES_ONLY = "Filter: Showing multiples owned only\n\n" + UI.CLICK(UI.ClickType.Click) + " to toggle";

			// Token: 0x02002D17 RID: 11543
			public static class BARTERING
			{
				// Token: 0x0400C397 RID: 50071
				public static LocString TOOLTIP_ACTION_INVALID_OFFLINE = "Currently unavailable";

				// Token: 0x0400C398 RID: 50072
				public static LocString BUY = "PRINT";

				// Token: 0x0400C399 RID: 50073
				public static LocString TOOLTIP_BUY_ACTIVE = "This item requires {0} spools of Filament to print";

				// Token: 0x0400C39A RID: 50074
				public static LocString TOOLTIP_UNBUYABLE = "This item is unprintable";

				// Token: 0x0400C39B RID: 50075
				public static LocString TOOLTIP_UNBUYABLE_BETA = "This item may be printable after the public testing period";

				// Token: 0x0400C39C RID: 50076
				public static LocString TOOLTIP_UNBUYABLE_ALREADY_OWNED = "My colony already owns one of these blueprints";

				// Token: 0x0400C39D RID: 50077
				public static LocString TOOLTIP_BUY_CANT_AFFORD = "Filament supply is too low";

				// Token: 0x0400C39E RID: 50078
				public static LocString SELL = "RECYCLE";

				// Token: 0x0400C39F RID: 50079
				public static LocString TOOLTIP_SELL_ACTIVE = "Recycle this blueprint for {0} spools of Filament";

				// Token: 0x0400C3A0 RID: 50080
				public static LocString TOOLTIP_UNSELLABLE = "This item is non-recyclable";

				// Token: 0x0400C3A1 RID: 50081
				public static LocString TOOLTIP_NONE_TO_SELL = "My colony does not own any of these blueprints";

				// Token: 0x0400C3A2 RID: 50082
				public static LocString CANCEL = "CANCEL";

				// Token: 0x0400C3A3 RID: 50083
				public static LocString CONFIRM_RECYCLE_HEADER = "RECYCLE INTO FILAMENT?";

				// Token: 0x0400C3A4 RID: 50084
				public static LocString CONFIRM_PRINT_HEADER = "PRINT ITEM?";

				// Token: 0x0400C3A5 RID: 50085
				public static LocString OFFLINE_LABEL = "Not connected to Klei server";

				// Token: 0x0400C3A6 RID: 50086
				public static LocString LOADING = "Connecting to server...";

				// Token: 0x0400C3A7 RID: 50087
				public static LocString TRANSACTION_ERROR = "Whoops! Something's gone wrong.";

				// Token: 0x0400C3A8 RID: 50088
				public static LocString ACTION_DESCRIPTION_RECYCLE = "Recycling this blueprint will recover Filament that my colony can use to print other items.\n\nOne copy of this blueprint will be removed from my colony's supply closet.";

				// Token: 0x0400C3A9 RID: 50089
				public static LocString ACTION_DESCRIPTION_PRINT = "Producing this blueprint requires Filament from my colony's supply.\n\nOne copy of this blueprint will be extruded at a time.";

				// Token: 0x0400C3AA RID: 50090
				public static LocString WALLET_TOOLTIP = "{0} spool of Filament available";

				// Token: 0x0400C3AB RID: 50091
				public static LocString WALLET_PLURAL_TOOLTIP = "{0} spools of Filament available";

				// Token: 0x0400C3AC RID: 50092
				public static LocString TRANSACTION_COMPLETE_HEADER = "SUCCESS!";

				// Token: 0x0400C3AD RID: 50093
				public static LocString TRANSACTION_INCOMPLETE_HEADER = "ERROR";

				// Token: 0x0400C3AE RID: 50094
				public static LocString PURCHASE_SUCCESS = "One copy of this blueprint has been added to my colony's supply closet.";

				// Token: 0x0400C3AF RID: 50095
				public static LocString SELL_SUCCESS = "The Filament recovered from recycling this item can now be used to print other items.";
			}

			// Token: 0x02002D18 RID: 11544
			public static class CATEGORIES
			{
				// Token: 0x0400C3B0 RID: 50096
				public static LocString EQUIPMENT = "Equipment";

				// Token: 0x0400C3B1 RID: 50097
				public static LocString DUPE_TOPS = "Tops & Onesies";

				// Token: 0x0400C3B2 RID: 50098
				public static LocString DUPE_BOTTOMS = "Bottoms";

				// Token: 0x0400C3B3 RID: 50099
				public static LocString DUPE_GLOVES = "Gloves";

				// Token: 0x0400C3B4 RID: 50100
				public static LocString DUPE_SHOES = "Footwear";

				// Token: 0x0400C3B5 RID: 50101
				public static LocString DUPE_HATS = "Headgear";

				// Token: 0x0400C3B6 RID: 50102
				public static LocString DUPE_ACCESSORIES = "Accessories";

				// Token: 0x0400C3B7 RID: 50103
				public static LocString ATMO_SUIT_HELMET = "Atmo Helmets";

				// Token: 0x0400C3B8 RID: 50104
				public static LocString ATMO_SUIT_BODY = "Atmo Suits";

				// Token: 0x0400C3B9 RID: 50105
				public static LocString ATMO_SUIT_GLOVES = "Atmo Gloves";

				// Token: 0x0400C3BA RID: 50106
				public static LocString ATMO_SUIT_BELT = "Atmo Belts";

				// Token: 0x0400C3BB RID: 50107
				public static LocString ATMO_SUIT_SHOES = "Atmo Boots";

				// Token: 0x0400C3BC RID: 50108
				public static LocString PRIMOGARB = "Primo Garb";

				// Token: 0x0400C3BD RID: 50109
				public static LocString ATMOSUITS = "Atmo Suits";

				// Token: 0x0400C3BE RID: 50110
				public static LocString BUILDINGS = "Buildings";

				// Token: 0x0400C3BF RID: 50111
				public static LocString CRITTERS = "Critters";

				// Token: 0x0400C3C0 RID: 50112
				public static LocString SWEEPYS = "Sweepys";

				// Token: 0x0400C3C1 RID: 50113
				public static LocString DUPLICANTS = "Duplicants";

				// Token: 0x0400C3C2 RID: 50114
				public static LocString ARTWORKS = "Artwork";

				// Token: 0x0400C3C3 RID: 50115
				public static LocString JOY_RESPONSE = "Overjoyed Responses";

				// Token: 0x02003A0A RID: 14858
				public static class JOY_RESPONSES
				{
					// Token: 0x0400E7B2 RID: 59314
					public static LocString BALLOON_ARTIST = "Balloon Artist";
				}
			}

			// Token: 0x02002D19 RID: 11545
			public static class TOP_LEVEL_CATEGORIES
			{
				// Token: 0x0400C3C4 RID: 50116
				public static LocString UNRELEASED = "DEBUG UNRELEASED";

				// Token: 0x0400C3C5 RID: 50117
				public static LocString CLOTHING_TOPS = "Tops & Onesies";

				// Token: 0x0400C3C6 RID: 50118
				public static LocString CLOTHING_BOTTOMS = "Bottoms";

				// Token: 0x0400C3C7 RID: 50119
				public static LocString CLOTHING_GLOVES = "Gloves";

				// Token: 0x0400C3C8 RID: 50120
				public static LocString CLOTHING_SHOES = "Footwear";

				// Token: 0x0400C3C9 RID: 50121
				public static LocString ATMOSUITS = "Atmo Suits";

				// Token: 0x0400C3CA RID: 50122
				public static LocString BUILDINGS = "Buildings";

				// Token: 0x0400C3CB RID: 50123
				public static LocString WALLPAPERS = "Wallpapers";

				// Token: 0x0400C3CC RID: 50124
				public static LocString ARTWORK = "Artwork";

				// Token: 0x0400C3CD RID: 50125
				public static LocString JOY_RESPONSES = "Joy Responses";
			}

			// Token: 0x02002D1A RID: 11546
			public static class SUBCATEGORIES
			{
				// Token: 0x0400C3CE RID: 50126
				public static LocString UNRELEASED = "DEBUG UNRELEASED";

				// Token: 0x0400C3CF RID: 50127
				public static LocString UNCATEGORIZED = "BUG: UNCATEGORIZED";

				// Token: 0x0400C3D0 RID: 50128
				public static LocString YAML = "YAML";

				// Token: 0x0400C3D1 RID: 50129
				public static LocString DEFAULT = "Default";

				// Token: 0x0400C3D2 RID: 50130
				public static LocString JOY_BALLOON = "Balloons";

				// Token: 0x0400C3D3 RID: 50131
				public static LocString JOY_STICKER = "Stickers";

				// Token: 0x0400C3D4 RID: 50132
				public static LocString PRIMO_GARB = "Primo Garb";

				// Token: 0x0400C3D5 RID: 50133
				public static LocString CLOTHING_TOPS_BASIC = "Standard Shirts";

				// Token: 0x0400C3D6 RID: 50134
				public static LocString CLOTHING_TOPS_TSHIRT = "Tees";

				// Token: 0x0400C3D7 RID: 50135
				public static LocString CLOTHING_TOPS_FANCY = "Specialty Tops";

				// Token: 0x0400C3D8 RID: 50136
				public static LocString CLOTHING_TOPS_JACKET = "Jackets";

				// Token: 0x0400C3D9 RID: 50137
				public static LocString CLOTHING_TOPS_UNDERSHIRT = "Undershirts";

				// Token: 0x0400C3DA RID: 50138
				public static LocString CLOTHING_TOPS_DRESS = "Dresses and Onesies";

				// Token: 0x0400C3DB RID: 50139
				public static LocString CLOTHING_BOTTOMS_BASIC = "Standard Pants";

				// Token: 0x0400C3DC RID: 50140
				public static LocString CLOTHING_BOTTOMS_FANCY = "Fancy Pants";

				// Token: 0x0400C3DD RID: 50141
				public static LocString CLOTHING_BOTTOMS_SHORTS = "Shorts";

				// Token: 0x0400C3DE RID: 50142
				public static LocString CLOTHING_BOTTOMS_SKIRTS = "Skirts";

				// Token: 0x0400C3DF RID: 50143
				public static LocString CLOTHING_BOTTOMS_UNDERWEAR = "Underwear";

				// Token: 0x0400C3E0 RID: 50144
				public static LocString CLOTHING_GLOVES_BASIC = "Standard Gloves";

				// Token: 0x0400C3E1 RID: 50145
				public static LocString CLOTHING_GLOVES_FORMAL = "Fancy Gloves";

				// Token: 0x0400C3E2 RID: 50146
				public static LocString CLOTHING_GLOVES_SHORT = "Short Gloves";

				// Token: 0x0400C3E3 RID: 50147
				public static LocString CLOTHING_GLOVES_PRINTS = "Specialty Gloves";

				// Token: 0x0400C3E4 RID: 50148
				public static LocString CLOTHING_SHOES_BASIC = "Standard Shoes";

				// Token: 0x0400C3E5 RID: 50149
				public static LocString CLOTHING_SHOE_SOCKS = "Socks";

				// Token: 0x0400C3E6 RID: 50150
				public static LocString CLOTHING_SHOES_FANCY = "Fancy Shoes";

				// Token: 0x0400C3E7 RID: 50151
				public static LocString ATMOSUIT_HELMETS_BASIC = "Atmo Helmets";

				// Token: 0x0400C3E8 RID: 50152
				public static LocString ATMOSUIT_HELMETS_FANCY = "Fancy Atmo Helmets";

				// Token: 0x0400C3E9 RID: 50153
				public static LocString ATMOSUIT_BODIES_BASIC = "Atmo Suits";

				// Token: 0x0400C3EA RID: 50154
				public static LocString ATMOSUIT_BODIES_FANCY = "Fancy Atmo Suits";

				// Token: 0x0400C3EB RID: 50155
				public static LocString ATMOSUIT_GLOVES_BASIC = "Atmo Gloves";

				// Token: 0x0400C3EC RID: 50156
				public static LocString ATMOSUIT_GLOVES_FANCY = "Fancy Atmo Gloves";

				// Token: 0x0400C3ED RID: 50157
				public static LocString ATMOSUIT_BELTS_BASIC = "Atmo Belts";

				// Token: 0x0400C3EE RID: 50158
				public static LocString ATMOSUIT_BELTS_FANCY = "Fancy Atmo Belts";

				// Token: 0x0400C3EF RID: 50159
				public static LocString ATMOSUIT_SHOES_BASIC = "Atmo Boots";

				// Token: 0x0400C3F0 RID: 50160
				public static LocString ATMOSUIT_SHOES_FANCY = "Fancy Atmo Boots";

				// Token: 0x0400C3F1 RID: 50161
				public static LocString BUILDING_WALLPAPER_BASIC = "Solid Wallpapers";

				// Token: 0x0400C3F2 RID: 50162
				public static LocString BUILDING_WALLPAPER_FANCY = "Geometric Wallpapers";

				// Token: 0x0400C3F3 RID: 50163
				public static LocString BUILDING_WALLPAPER_PRINTS = "Patterned Wallpapers";

				// Token: 0x0400C3F4 RID: 50164
				public static LocString BUILDING_CANVAS_STANDARD = "Standard Canvas";

				// Token: 0x0400C3F5 RID: 50165
				public static LocString BUILDING_CANVAS_PORTRAIT = "Portrait Canvas";

				// Token: 0x0400C3F6 RID: 50166
				public static LocString BUILDING_CANVAS_LANDSCAPE = "Landscape Canvas";

				// Token: 0x0400C3F7 RID: 50167
				public static LocString BUILDING_SCULPTURE = "Sculptures";

				// Token: 0x0400C3F8 RID: 50168
				public static LocString MONUMENT_BOTTOM = "Monument Base";

				// Token: 0x0400C3F9 RID: 50169
				public static LocString MONUMENT_MIDDLE = "Monument Midsection";

				// Token: 0x0400C3FA RID: 50170
				public static LocString MONUMENT_TOP = "Monument Top";

				// Token: 0x0400C3FB RID: 50171
				public static LocString BUILDINGS_FLOWER_VASE = "Pots and Planters";

				// Token: 0x0400C3FC RID: 50172
				public static LocString BUILDINGS_BED_COT = "Cots";

				// Token: 0x0400C3FD RID: 50173
				public static LocString BUILDINGS_BED_LUXURY = "Comfy Beds";

				// Token: 0x0400C3FE RID: 50174
				public static LocString BUILDING_CEILING_LIGHT = "Lights";

				// Token: 0x0400C3FF RID: 50175
				public static LocString BUILDINGS_STORAGE = "Storage";

				// Token: 0x0400C400 RID: 50176
				public static LocString BUILDINGS_INDUSTRIAL = "Industrial";

				// Token: 0x0400C401 RID: 50177
				public static LocString BUILDINGS_FOOD = "Cooking";

				// Token: 0x0400C402 RID: 50178
				public static LocString BUILDINGS_WASHROOM = "Sanitation";

				// Token: 0x0400C403 RID: 50179
				public static LocString BUILDINGS_RANCHING = "Agricultural";

				// Token: 0x0400C404 RID: 50180
				public static LocString BUILDINGS_RECREATION = "Recreation and Decor";

				// Token: 0x0400C405 RID: 50181
				public static LocString BUILDINGS_PRINTING_POD = "Printing Pods";

				// Token: 0x0400C406 RID: 50182
				public static LocString BUILDINGS_ELECTIC_WIRES = "Electrical";

				// Token: 0x0400C407 RID: 50183
				public static LocString BUILDINGS_AUTOMATION = "Automation";

				// Token: 0x0400C408 RID: 50184
				public static LocString BUILDINGS_RESEARCH = "Research";
			}

			// Token: 0x02002D1B RID: 11547
			public static class COLUMN_HEADERS
			{
				// Token: 0x0400C409 RID: 50185
				public static LocString CATEGORY_HEADER = "BLUEPRINTS";

				// Token: 0x0400C40A RID: 50186
				public static LocString ITEMS_HEADER = "Items";

				// Token: 0x0400C40B RID: 50187
				public static LocString DETAILS_HEADER = "Details";
			}
		}

		// Token: 0x020023BC RID: 9148
		public class ITEM_DROP_SCREEN
		{
			// Token: 0x04009F7E RID: 40830
			public static LocString THANKS_FOR_PLAYING = "New blueprints unlocked!";

			// Token: 0x04009F7F RID: 40831
			public static LocString WEB_REWARDS_AVAILABLE = "Rewards available online!";

			// Token: 0x04009F80 RID: 40832
			public static LocString NOTHING_AVAILABLE = "All available blueprints claimed";

			// Token: 0x04009F81 RID: 40833
			public static LocString OPEN_URL_BUTTON = "CLAIM";

			// Token: 0x04009F82 RID: 40834
			public static LocString PRINT_ITEM_BUTTON = "PRINT";

			// Token: 0x04009F83 RID: 40835
			public static LocString DISMISS_BUTTON = "DISMISS";

			// Token: 0x04009F84 RID: 40836
			public static LocString ERROR_CANNOTLOADITEM = "Whoops! Something's gone wrong.";

			// Token: 0x04009F85 RID: 40837
			public static LocString UNOPENED_ITEM_COUNT = "{0} unclaimed blueprints";

			// Token: 0x04009F86 RID: 40838
			public static LocString UNOPENED_ITEM = "{0} unclaimed blueprint";

			// Token: 0x02002D1C RID: 11548
			public static class IN_GAME_BUTTON
			{
				// Token: 0x0400C40C RID: 50188
				public static LocString TOOLTIP_ITEMS_AVAILABLE = "Unlock new blueprints";

				// Token: 0x0400C40D RID: 50189
				public static LocString TOOLTIP_ERROR_NO_ITEMS = "No new blueprints to unlock";
			}
		}

		// Token: 0x020023BD RID: 9149
		public class OUTFIT_BROWSER_SCREEN
		{
			// Token: 0x04009F87 RID: 40839
			public static LocString BUTTON_ADD_OUTFIT = "New Outfit";

			// Token: 0x04009F88 RID: 40840
			public static LocString BUTTON_PICK_OUTFIT = "Assign Outfit";

			// Token: 0x04009F89 RID: 40841
			public static LocString TOOLTIP_PICK_OUTFIT_ERROR_LOCKED = "Cannot assign this outfit to {MinionName} because my colony doesn't have all of these blueprints yet";

			// Token: 0x04009F8A RID: 40842
			public static LocString BUTTON_EDIT_OUTFIT = "Restyle Outfit";

			// Token: 0x04009F8B RID: 40843
			public static LocString BUTTON_COPY_OUTFIT = "Copy Outfit";

			// Token: 0x04009F8C RID: 40844
			public static LocString TOOLTIP_DELETE_OUTFIT = "Delete Outfit";

			// Token: 0x04009F8D RID: 40845
			public static LocString TOOLTIP_DELETE_OUTFIT_ERROR_READONLY = "This outfit cannot be deleted";

			// Token: 0x04009F8E RID: 40846
			public static LocString TOOLTIP_RENAME_OUTFIT = "Rename Outfit";

			// Token: 0x04009F8F RID: 40847
			public static LocString TOOLTIP_RENAME_OUTFIT_ERROR_READONLY = "This outfit cannot be renamed";

			// Token: 0x04009F90 RID: 40848
			public static LocString TOOLTIP_FILTER_BY_CLOTHING = "View your Clothing Outfits";

			// Token: 0x04009F91 RID: 40849
			public static LocString TOOLTIP_FILTER_BY_ATMO_SUITS = "View your Atmo Suit Outfits";

			// Token: 0x02002D1D RID: 11549
			public static class COLUMN_HEADERS
			{
				// Token: 0x0400C40E RID: 50190
				public static LocString GALLERY_HEADER = "OUTFITS";

				// Token: 0x0400C40F RID: 50191
				public static LocString MINION_GALLERY_HEADER = "WARDROBE";

				// Token: 0x0400C410 RID: 50192
				public static LocString DETAILS_HEADER = "Preview";
			}

			// Token: 0x02002D1E RID: 11550
			public class DELETE_WARNING_POPUP
			{
				// Token: 0x0400C411 RID: 50193
				public static LocString HEADER = "Delete \"{OutfitName}\"?";

				// Token: 0x0400C412 RID: 50194
				public static LocString BODY = "Are you sure you want to delete \"{OutfitName}\"?\n\nAny Duplicants assigned to wear this outfit on spawn will be printed wearing their default outfit instead. Existing Duplicants in saved games won't be affected.\n\nThis <b>cannot</b> be undone.";

				// Token: 0x0400C413 RID: 50195
				public static LocString BUTTON_YES_DELETE = "Yes, delete outfit";

				// Token: 0x0400C414 RID: 50196
				public static LocString BUTTON_DONT_DELETE = "Cancel";
			}

			// Token: 0x02002D1F RID: 11551
			public class RENAME_POPUP
			{
				// Token: 0x0400C415 RID: 50197
				public static LocString HEADER = "RENAME OUTFIT";
			}
		}

		// Token: 0x020023BE RID: 9150
		public class LOCKER_MENU
		{
			// Token: 0x04009F92 RID: 40850
			public static LocString TITLE = "SUPPLY CLOSET";

			// Token: 0x04009F93 RID: 40851
			public static LocString BUTTON_INVENTORY = "All";

			// Token: 0x04009F94 RID: 40852
			public static LocString BUTTON_INVENTORY_DESCRIPTION = "View all of my colony's blueprints";

			// Token: 0x04009F95 RID: 40853
			public static LocString BUTTON_DUPLICANTS = "Duplicants";

			// Token: 0x04009F96 RID: 40854
			public static LocString BUTTON_DUPLICANTS_DESCRIPTION = "Manage individual Duplicants' outfits";

			// Token: 0x04009F97 RID: 40855
			public static LocString BUTTON_OUTFITS = "Wardrobe";

			// Token: 0x04009F98 RID: 40856
			public static LocString BUTTON_OUTFITS_DESCRIPTION = "Manage my colony's collection of outfits";

			// Token: 0x04009F99 RID: 40857
			public static LocString DEFAULT_DESCRIPTION = "Select a screen";

			// Token: 0x04009F9A RID: 40858
			public static LocString BUTTON_CLAIM = "Claim Blueprints";

			// Token: 0x04009F9B RID: 40859
			public static LocString BUTTON_CLAIM_DESCRIPTION = "Claim any available blueprints";

			// Token: 0x04009F9C RID: 40860
			public static LocString BUTTON_CLAIM_NONE_DESCRIPTION = "All available blueprints claimed";

			// Token: 0x04009F9D RID: 40861
			public static LocString UNOPENED_ITEMS_TOOLTIP = "New blueprints available";

			// Token: 0x04009F9E RID: 40862
			public static LocString UNOPENED_ITEMS_NONE_TOOLTIP = "All available blueprints claimed";

			// Token: 0x04009F9F RID: 40863
			public static LocString OFFLINE_ICON_TOOLTIP = "Not connected to Klei server";
		}

		// Token: 0x020023BF RID: 9151
		public class LOCKER_NAVIGATOR
		{
			// Token: 0x04009FA0 RID: 40864
			public static LocString BUTTON_BACK = "BACK";

			// Token: 0x04009FA1 RID: 40865
			public static LocString BUTTON_CLOSE = "CLOSE";

			// Token: 0x02002D20 RID: 11552
			public class DATA_COLLECTION_WARNING_POPUP
			{
				// Token: 0x0400C416 RID: 50198
				public static LocString HEADER = "Data Communication is Disabled";

				// Token: 0x0400C417 RID: 50199
				public static LocString BODY = "Data Communication must be enabled in order to access newly unlocked items. This setting can be found in the Options menu.\n\nExisting item unlocks can still be used while Data Communication is disabled.";

				// Token: 0x0400C418 RID: 50200
				public static LocString BUTTON_OK = "Continue";

				// Token: 0x0400C419 RID: 50201
				public static LocString BUTTON_OPEN_SETTINGS = "Options Menu";
			}
		}

		// Token: 0x020023C0 RID: 9152
		public class JOY_RESPONSE_DESIGNER_SCREEN
		{
			// Token: 0x04009FA2 RID: 40866
			public static LocString CATEGORY_HEADER = "OVERJOYED RESPONSES";

			// Token: 0x04009FA3 RID: 40867
			public static LocString BUTTON_APPLY_TO_MINION = "Assign to {MinionName}";

			// Token: 0x04009FA4 RID: 40868
			public static LocString TOOLTIP_NO_FACADES_FOR_JOY_TRAIT = "There aren't any blueprints for {JoyResponseType} Duplicants yet";

			// Token: 0x04009FA5 RID: 40869
			public static LocString TOOLTIP_PICK_JOY_RESPONSE_ERROR_LOCKED = "This Overjoyed Response blueprint cannot be assigned because my colony doesn't own it yet";

			// Token: 0x02002D21 RID: 11553
			public class CHANGES_NOT_SAVED_WARNING_POPUP
			{
				// Token: 0x0400C41A RID: 50202
				public static LocString HEADER = "Discard changes to {MinionName}'s Overjoyed Response?";
			}
		}

		// Token: 0x020023C1 RID: 9153
		public class OUTFIT_DESIGNER_SCREEN
		{
			// Token: 0x04009FA6 RID: 40870
			public static LocString CATEGORY_HEADER = "CLOTHING";

			// Token: 0x02002D22 RID: 11554
			public class MINION_INSTANCE
			{
				// Token: 0x0400C41B RID: 50203
				public static LocString BUTTON_APPLY_TO_MINION = "Assign to {MinionName}";

				// Token: 0x0400C41C RID: 50204
				public static LocString BUTTON_APPLY_TO_TEMPLATE = "Apply to Template";

				// Token: 0x02003A0B RID: 14859
				public class APPLY_TEMPLATE_POPUP
				{
					// Token: 0x0400E7B3 RID: 59315
					public static LocString HEADER = "SAVE AS TEMPLATE";

					// Token: 0x0400E7B4 RID: 59316
					public static LocString DESC_SAVE_EXISTING = "\"{OutfitName}\" will be updated and applied to {MinionName} on save.";

					// Token: 0x0400E7B5 RID: 59317
					public static LocString DESC_SAVE_NEW = "A new outfit named \"{OutfitName}\" will be created and assigned to {MinionName} on save.";

					// Token: 0x0400E7B6 RID: 59318
					public static LocString BUTTON_SAVE_EXISTING = "Update Outfit";

					// Token: 0x0400E7B7 RID: 59319
					public static LocString BUTTON_SAVE_NEW = "Save New Outfit";
				}
			}

			// Token: 0x02002D23 RID: 11555
			public class OUTFIT_TEMPLATE
			{
				// Token: 0x0400C41D RID: 50205
				public static LocString BUTTON_SAVE = "Save Template";

				// Token: 0x0400C41E RID: 50206
				public static LocString BUTTON_COPY = "Save a Copy";

				// Token: 0x0400C41F RID: 50207
				public static LocString TOOLTIP_SAVE_ERROR_LOCKED = "Cannot save this outfit because my colony doesn't own all of its blueprints yet";

				// Token: 0x0400C420 RID: 50208
				public static LocString TOOLTIP_SAVE_ERROR_READONLY = "This wardrobe staple cannot be altered\n\nMake a copy to save your changes";
			}

			// Token: 0x02002D24 RID: 11556
			public class CHANGES_NOT_SAVED_WARNING_POPUP
			{
				// Token: 0x0400C421 RID: 50209
				public static LocString HEADER = "Discard changes to \"{OutfitName}\"?";

				// Token: 0x0400C422 RID: 50210
				public static LocString BODY = "There are unsaved changes which will be lost if you exit now.\n\nAre you sure you want to discard your changes?";

				// Token: 0x0400C423 RID: 50211
				public static LocString BUTTON_DISCARD = "Yes, discard changes";

				// Token: 0x0400C424 RID: 50212
				public static LocString BUTTON_RETURN = "Cancel";
			}

			// Token: 0x02002D25 RID: 11557
			public class COPY_POPUP
			{
				// Token: 0x0400C425 RID: 50213
				public static LocString HEADER = "RENAME COPY";
			}
		}

		// Token: 0x020023C2 RID: 9154
		public class OUTFIT_NAME
		{
			// Token: 0x04009FA7 RID: 40871
			public static LocString NEW = "Custom Outfit";

			// Token: 0x04009FA8 RID: 40872
			public static LocString COPY_OF = "Copy of {OutfitName}";

			// Token: 0x04009FA9 RID: 40873
			public static LocString RESOLVE_CONFLICT = "{OutfitName} ({ConflictNumber})";

			// Token: 0x04009FAA RID: 40874
			public static LocString ERROR_NAME_EXISTS = "There's already an outfit named \"{OutfitName}\"";

			// Token: 0x04009FAB RID: 40875
			public static LocString MINIONS_OUTFIT = "{MinionName}'s Current Outfit";

			// Token: 0x04009FAC RID: 40876
			public static LocString NONE = "Default Outfit";

			// Token: 0x04009FAD RID: 40877
			public static LocString NONE_JOY_RESPONSE = "Default Overjoyed Response";

			// Token: 0x04009FAE RID: 40878
			public static LocString NONE_ATMO_SUIT = "Default Atmo Suit";
		}

		// Token: 0x020023C3 RID: 9155
		public class OUTFIT_DESCRIPTION
		{
			// Token: 0x04009FAF RID: 40879
			public static LocString CONTAINS_NON_OWNED_ITEMS = "This outfit can only be worn once my colony has access to all of its blueprints.";

			// Token: 0x04009FB0 RID: 40880
			public static LocString NO_JOY_RESPONSE_NAME = "Default Overjoyed Response";

			// Token: 0x04009FB1 RID: 40881
			public static LocString NO_JOY_RESPONSE_DESC = "Default response to an overjoyed state.";
		}

		// Token: 0x020023C4 RID: 9156
		public class MINION_BROWSER_SCREEN
		{
			// Token: 0x04009FB2 RID: 40882
			public static LocString CATEGORY_HEADER = "DUPLICANTS";

			// Token: 0x04009FB3 RID: 40883
			public static LocString BUTTON_CHANGE_OUTFIT = "Open Wardrobe";

			// Token: 0x04009FB4 RID: 40884
			public static LocString BUTTON_EDIT_OUTFIT_ITEMS = "Restyle Outfit";

			// Token: 0x04009FB5 RID: 40885
			public static LocString BUTTON_EDIT_ATMO_SUIT_OUTFIT_ITEMS = "Restyle Atmo Suit";

			// Token: 0x04009FB6 RID: 40886
			public static LocString BUTTON_EDIT_JOY_RESPONSE = "Restyle Overjoyed Response";

			// Token: 0x04009FB7 RID: 40887
			public static LocString OUTFIT_TYPE_CLOTHING = "CLOTHING";

			// Token: 0x04009FB8 RID: 40888
			public static LocString OUTFIT_TYPE_JOY_RESPONSE = "OVERJOYED RESPONSE";

			// Token: 0x04009FB9 RID: 40889
			public static LocString OUTFIT_TYPE_ATMOSUIT = "ATMO SUIT";

			// Token: 0x04009FBA RID: 40890
			public static LocString TOOLTIP_FROM_DLC = "This Duplicant is part of {0} DLC";
		}

		// Token: 0x020023C5 RID: 9157
		public class PERMIT_RARITY
		{
			// Token: 0x04009FBB RID: 40891
			public static readonly LocString UNKNOWN = "Unknown";

			// Token: 0x04009FBC RID: 40892
			public static readonly LocString UNIVERSAL = "Universal";

			// Token: 0x04009FBD RID: 40893
			public static readonly LocString LOYALTY = "<color=#FFB037>Loyalty</color>";

			// Token: 0x04009FBE RID: 40894
			public static readonly LocString COMMON = "<color=#97B2B9>Common</color>";

			// Token: 0x04009FBF RID: 40895
			public static readonly LocString DECENT = "<color=#81EBDE>Decent</color>";

			// Token: 0x04009FC0 RID: 40896
			public static readonly LocString NIFTY = "<color=#71E379>Nifty</color>";

			// Token: 0x04009FC1 RID: 40897
			public static readonly LocString SPLENDID = "<color=#FF6DE7>Splendid</color>";
		}

		// Token: 0x020023C6 RID: 9158
		public class OUTFITS
		{
			// Token: 0x02002D26 RID: 11558
			public class BASIC_BLACK
			{
				// Token: 0x0400C426 RID: 50214
				public static LocString NAME = "Basic Black Outfit";
			}

			// Token: 0x02002D27 RID: 11559
			public class BASIC_WHITE
			{
				// Token: 0x0400C427 RID: 50215
				public static LocString NAME = "Basic White Outfit";
			}

			// Token: 0x02002D28 RID: 11560
			public class BASIC_RED
			{
				// Token: 0x0400C428 RID: 50216
				public static LocString NAME = "Basic Red Outfit";
			}

			// Token: 0x02002D29 RID: 11561
			public class BASIC_ORANGE
			{
				// Token: 0x0400C429 RID: 50217
				public static LocString NAME = "Basic Orange Outfit";
			}

			// Token: 0x02002D2A RID: 11562
			public class BASIC_YELLOW
			{
				// Token: 0x0400C42A RID: 50218
				public static LocString NAME = "Basic Yellow Outfit";
			}

			// Token: 0x02002D2B RID: 11563
			public class BASIC_GREEN
			{
				// Token: 0x0400C42B RID: 50219
				public static LocString NAME = "Basic Green Outfit";
			}

			// Token: 0x02002D2C RID: 11564
			public class BASIC_AQUA
			{
				// Token: 0x0400C42C RID: 50220
				public static LocString NAME = "Basic Aqua Outfit";
			}

			// Token: 0x02002D2D RID: 11565
			public class BASIC_PURPLE
			{
				// Token: 0x0400C42D RID: 50221
				public static LocString NAME = "Basic Purple Outfit";
			}

			// Token: 0x02002D2E RID: 11566
			public class BASIC_PINK_ORCHID
			{
				// Token: 0x0400C42E RID: 50222
				public static LocString NAME = "Basic Bubblegum Outfit";
			}

			// Token: 0x02002D2F RID: 11567
			public class BASIC_DEEPRED
			{
				// Token: 0x0400C42F RID: 50223
				public static LocString NAME = "Team Captain Outfit";
			}

			// Token: 0x02002D30 RID: 11568
			public class BASIC_BLUE_COBALT
			{
				// Token: 0x0400C430 RID: 50224
				public static LocString NAME = "True Blue Outfit";
			}

			// Token: 0x02002D31 RID: 11569
			public class BASIC_PINK_FLAMINGO
			{
				// Token: 0x0400C431 RID: 50225
				public static LocString NAME = "Pep Rally Outfit";
			}

			// Token: 0x02002D32 RID: 11570
			public class BASIC_GREEN_KELLY
			{
				// Token: 0x0400C432 RID: 50226
				public static LocString NAME = "Go Team! Outfit";
			}

			// Token: 0x02002D33 RID: 11571
			public class BASIC_GREY_CHARCOAL
			{
				// Token: 0x0400C433 RID: 50227
				public static LocString NAME = "Underdog Outfit";
			}

			// Token: 0x02002D34 RID: 11572
			public class BASIC_LEMON
			{
				// Token: 0x0400C434 RID: 50228
				public static LocString NAME = "Team Hype Outfit";
			}

			// Token: 0x02002D35 RID: 11573
			public class BASIC_SATSUMA
			{
				// Token: 0x0400C435 RID: 50229
				public static LocString NAME = "Superfan Outfit";
			}

			// Token: 0x02002D36 RID: 11574
			public class JELLYPUFF_BLUEBERRY
			{
				// Token: 0x0400C436 RID: 50230
				public static LocString NAME = "Blueberry Jelly Outfit";
			}

			// Token: 0x02002D37 RID: 11575
			public class JELLYPUFF_GRAPE
			{
				// Token: 0x0400C437 RID: 50231
				public static LocString NAME = "Grape Jelly Outfit";
			}

			// Token: 0x02002D38 RID: 11576
			public class JELLYPUFF_LEMON
			{
				// Token: 0x0400C438 RID: 50232
				public static LocString NAME = "Lemon Jelly Outfit";
			}

			// Token: 0x02002D39 RID: 11577
			public class JELLYPUFF_LIME
			{
				// Token: 0x0400C439 RID: 50233
				public static LocString NAME = "Lime Jelly Outfit";
			}

			// Token: 0x02002D3A RID: 11578
			public class JELLYPUFF_SATSUMA
			{
				// Token: 0x0400C43A RID: 50234
				public static LocString NAME = "Satsuma Jelly Outfit";
			}

			// Token: 0x02002D3B RID: 11579
			public class JELLYPUFF_STRAWBERRY
			{
				// Token: 0x0400C43B RID: 50235
				public static LocString NAME = "Strawberry Jelly Outfit";
			}

			// Token: 0x02002D3C RID: 11580
			public class JELLYPUFF_WATERMELON
			{
				// Token: 0x0400C43C RID: 50236
				public static LocString NAME = "Watermelon Jelly Outfit";
			}

			// Token: 0x02002D3D RID: 11581
			public class ATHLETE
			{
				// Token: 0x0400C43D RID: 50237
				public static LocString NAME = "Racing Outfit";
			}

			// Token: 0x02002D3E RID: 11582
			public class CIRCUIT
			{
				// Token: 0x0400C43E RID: 50238
				public static LocString NAME = "LED Party Outfit";
			}

			// Token: 0x02002D3F RID: 11583
			public class ATMOSUIT_LIMONE
			{
				// Token: 0x0400C43F RID: 50239
				public static LocString NAME = "Citrus Atmo Outfit";
			}

			// Token: 0x02002D40 RID: 11584
			public class ATMOSUIT_SPARKLE_RED
			{
				// Token: 0x0400C440 RID: 50240
				public static LocString NAME = "Red Glitter Atmo Outfit";
			}

			// Token: 0x02002D41 RID: 11585
			public class ATMOSUIT_SPARKLE_BLUE
			{
				// Token: 0x0400C441 RID: 50241
				public static LocString NAME = "Blue Glitter Atmo Outfit";
			}

			// Token: 0x02002D42 RID: 11586
			public class ATMOSUIT_SPARKLE_GREEN
			{
				// Token: 0x0400C442 RID: 50242
				public static LocString NAME = "Green Glitter Atmo Outfit";
			}

			// Token: 0x02002D43 RID: 11587
			public class ATMOSUIT_SPARKLE_LAVENDER
			{
				// Token: 0x0400C443 RID: 50243
				public static LocString NAME = "Violet Glitter Atmo Outfit";
			}

			// Token: 0x02002D44 RID: 11588
			public class ATMOSUIT_PUFT
			{
				// Token: 0x0400C444 RID: 50244
				public static LocString NAME = "Puft Atmo Outfit";
			}

			// Token: 0x02002D45 RID: 11589
			public class ATMOSUIT_CONFETTI
			{
				// Token: 0x0400C445 RID: 50245
				public static LocString NAME = "Confetti Atmo Outfit";
			}

			// Token: 0x02002D46 RID: 11590
			public class ATMOSUIT_BASIC_PURPLE
			{
				// Token: 0x0400C446 RID: 50246
				public static LocString NAME = "Eggplant Atmo Outfit";
			}

			// Token: 0x02002D47 RID: 11591
			public class ATMOSUIT_PINK_PURPLE
			{
				// Token: 0x0400C447 RID: 50247
				public static LocString NAME = "Pink Punch Atmo Outfit";
			}

			// Token: 0x02002D48 RID: 11592
			public class ATMOSUIT_RED_GREY
			{
				// Token: 0x0400C448 RID: 50248
				public static LocString NAME = "Blastoff Atmo Outfit";
			}

			// Token: 0x02002D49 RID: 11593
			public class CANUXTUX
			{
				// Token: 0x0400C449 RID: 50249
				public static LocString NAME = "Canadian Tuxedo Outfit";
			}

			// Token: 0x02002D4A RID: 11594
			public class GONCHIES_STRAWBERRY
			{
				// Token: 0x0400C44A RID: 50250
				public static LocString NAME = "Executive Undies Outfit";
			}

			// Token: 0x02002D4B RID: 11595
			public class GONCHIES_SATSUMA
			{
				// Token: 0x0400C44B RID: 50251
				public static LocString NAME = "Underling Undies Outfit";
			}

			// Token: 0x02002D4C RID: 11596
			public class GONCHIES_LEMON
			{
				// Token: 0x0400C44C RID: 50252
				public static LocString NAME = "Groupthink Undies Outfit";
			}

			// Token: 0x02002D4D RID: 11597
			public class GONCHIES_LIME
			{
				// Token: 0x0400C44D RID: 50253
				public static LocString NAME = "Stakeholder Undies Outfit";
			}

			// Token: 0x02002D4E RID: 11598
			public class GONCHIES_BLUEBERRY
			{
				// Token: 0x0400C44E RID: 50254
				public static LocString NAME = "Admin Undies Outfit";
			}

			// Token: 0x02002D4F RID: 11599
			public class GONCHIES_GRAPE
			{
				// Token: 0x0400C44F RID: 50255
				public static LocString NAME = "Buzzword Undies Outfit";
			}

			// Token: 0x02002D50 RID: 11600
			public class GONCHIES_WATERMELON
			{
				// Token: 0x0400C450 RID: 50256
				public static LocString NAME = "Synergy Undies Outfit";
			}

			// Token: 0x02002D51 RID: 11601
			public class NERD
			{
				// Token: 0x0400C451 RID: 50257
				public static LocString NAME = "Research Outfit";
			}

			// Token: 0x02002D52 RID: 11602
			public class REBELGI
			{
				// Token: 0x0400C452 RID: 50258
				public static LocString NAME = "Rebel Gi Outfit";
			}

			// Token: 0x02002D53 RID: 11603
			public class DONOR
			{
				// Token: 0x0400C453 RID: 50259
				public static LocString NAME = "Donor Outfit";
			}

			// Token: 0x02002D54 RID: 11604
			public class MECHANIC
			{
				// Token: 0x0400C454 RID: 50260
				public static LocString NAME = "Engineer Coveralls";
			}

			// Token: 0x02002D55 RID: 11605
			public class VELOUR_BLACK
			{
				// Token: 0x0400C455 RID: 50261
				public static LocString NAME = "PhD Velour Outfit";
			}

			// Token: 0x02002D56 RID: 11606
			public class SLEEVELESS_BOW_BW
			{
				// Token: 0x0400C456 RID: 50262
				public static LocString NAME = "PhD Dress Outfit";
			}

			// Token: 0x02002D57 RID: 11607
			public class VELOUR_BLUE
			{
				// Token: 0x0400C457 RID: 50263
				public static LocString NAME = "Shortwave Velour Outfit";
			}

			// Token: 0x02002D58 RID: 11608
			public class VELOUR_PINK
			{
				// Token: 0x0400C458 RID: 50264
				public static LocString NAME = "Gamma Velour Outfit";
			}

			// Token: 0x02002D59 RID: 11609
			public class WATER
			{
				// Token: 0x0400C459 RID: 50265
				public static LocString NAME = "HVAC Coveralls";
			}

			// Token: 0x02002D5A RID: 11610
			public class WAISTCOAT_PINSTRIPE_SLATE
			{
				// Token: 0x0400C45A RID: 50266
				public static LocString NAME = "Nobel Pinstripe Outfit";
			}

			// Token: 0x02002D5B RID: 11611
			public class TWEED_PINK_ORCHID
			{
				// Token: 0x0400C45B RID: 50267
				public static LocString NAME = "Power Brunch Outfit";
			}

			// Token: 0x02002D5C RID: 11612
			public class BALLET
			{
				// Token: 0x0400C45C RID: 50268
				public static LocString NAME = "Ballet Outfit";
			}

			// Token: 0x02002D5D RID: 11613
			public class ATMOSUIT_CANTALOUPE
			{
				// Token: 0x0400C45D RID: 50269
				public static LocString NAME = "Rocketmelon Atmo Outfit";
			}

			// Token: 0x02002D5E RID: 11614
			public class PAJAMAS_SNOW
			{
				// Token: 0x0400C45E RID: 50270
				public static LocString NAME = "Crystal-Iced Jammies";
			}

			// Token: 0x02002D5F RID: 11615
			public class X_SPORCHID
			{
				// Token: 0x0400C45F RID: 50271
				public static LocString NAME = "Sporefest Outfit";
			}

			// Token: 0x02002D60 RID: 11616
			public class X1_PINCHAPEPPERNUTBELLS
			{
				// Token: 0x0400C460 RID: 50272
				public static LocString NAME = "Pinchabell Outfit";
			}

			// Token: 0x02002D61 RID: 11617
			public class POMPOM_SHINEBUGS_PINK_PEPPERNUT
			{
				// Token: 0x0400C461 RID: 50273
				public static LocString NAME = "Pom Bug Outfit";
			}

			// Token: 0x02002D62 RID: 11618
			public class SNOWFLAKE_BLUE
			{
				// Token: 0x0400C462 RID: 50274
				public static LocString NAME = "Crystal-Iced Outfit";
			}

			// Token: 0x02002D63 RID: 11619
			public class POLKADOT_TRACKSUIT
			{
				// Token: 0x0400C463 RID: 50275
				public static LocString NAME = "Polka Dot Tracksuit";
			}

			// Token: 0x02002D64 RID: 11620
			public class SUPERSTAR
			{
				// Token: 0x0400C464 RID: 50276
				public static LocString NAME = "Superstar Outfit";
			}

			// Token: 0x02002D65 RID: 11621
			public class ATMOSUIT_SPIFFY
			{
				// Token: 0x0400C465 RID: 50277
				public static LocString NAME = "Spiffy Atmo Outfit";
			}

			// Token: 0x02002D66 RID: 11622
			public class ATMOSUIT_CUBIST
			{
				// Token: 0x0400C466 RID: 50278
				public static LocString NAME = "Cubist Atmo Outfit";
			}

			// Token: 0x02002D67 RID: 11623
			public class LUCKY
			{
				// Token: 0x0400C467 RID: 50279
				public static LocString NAME = "Lucky Jammies Outfit";
			}

			// Token: 0x02002D68 RID: 11624
			public class SWEETHEART
			{
				// Token: 0x0400C468 RID: 50280
				public static LocString NAME = "Sweetheart Jammies Outfit";
			}

			// Token: 0x02002D69 RID: 11625
			public class GINCH_GLUON
			{
				// Token: 0x0400C469 RID: 50281
				public static LocString NAME = "Frilly Saltrock Outfit";
			}

			// Token: 0x02002D6A RID: 11626
			public class GINCH_CORTEX
			{
				// Token: 0x0400C46A RID: 50282
				public static LocString NAME = "Dusk Undies Outfit";
			}

			// Token: 0x02002D6B RID: 11627
			public class GINCH_FROSTY
			{
				// Token: 0x0400C46B RID: 50283
				public static LocString NAME = "Frostbasin Undies Outfit";
			}

			// Token: 0x02002D6C RID: 11628
			public class GINCH_LOCUS
			{
				// Token: 0x0400C46C RID: 50284
				public static LocString NAME = "Balmy Undies Outfit";
			}

			// Token: 0x02002D6D RID: 11629
			public class GINCH_GOOP
			{
				// Token: 0x0400C46D RID: 50285
				public static LocString NAME = "Leachy Undies Outfit";
			}

			// Token: 0x02002D6E RID: 11630
			public class GINCH_BILE
			{
				// Token: 0x0400C46E RID: 50286
				public static LocString NAME = "Yellowcake Undies Outfit";
			}

			// Token: 0x02002D6F RID: 11631
			public class GINCH_NYBBLE
			{
				// Token: 0x0400C46F RID: 50287
				public static LocString NAME = "Atomic Undies Outfit";
			}

			// Token: 0x02002D70 RID: 11632
			public class GINCH_IRONBOW
			{
				// Token: 0x0400C470 RID: 50288
				public static LocString NAME = "Magma Undies Outfit";
			}

			// Token: 0x02002D71 RID: 11633
			public class GINCH_PHLEGM
			{
				// Token: 0x0400C471 RID: 50289
				public static LocString NAME = "Slate Undies Outfit";
			}

			// Token: 0x02002D72 RID: 11634
			public class GINCH_OBELUS
			{
				// Token: 0x0400C472 RID: 50290
				public static LocString NAME = "Charcoal Undies Outfit";
			}

			// Token: 0x02002D73 RID: 11635
			public class HIVIS
			{
				// Token: 0x0400C473 RID: 50291
				public static LocString NAME = "Hi-Vis Outfit";
			}

			// Token: 0x02002D74 RID: 11636
			public class DOWNTIME
			{
				// Token: 0x0400C474 RID: 50292
				public static LocString NAME = "Downtime Outfit";
			}

			// Token: 0x02002D75 RID: 11637
			public class FLANNEL_RED
			{
				// Token: 0x0400C475 RID: 50293
				public static LocString NAME = "Classic Flannel Outfit";
			}

			// Token: 0x02002D76 RID: 11638
			public class FLANNEL_ORANGE
			{
				// Token: 0x0400C476 RID: 50294
				public static LocString NAME = "Cadmium Flannel Outfit";
			}

			// Token: 0x02002D77 RID: 11639
			public class FLANNEL_YELLOW
			{
				// Token: 0x0400C477 RID: 50295
				public static LocString NAME = "Flax Flannel Outfit";
			}

			// Token: 0x02002D78 RID: 11640
			public class FLANNEL_GREEN
			{
				// Token: 0x0400C478 RID: 50296
				public static LocString NAME = "Swampy Flannel Outfit";
			}

			// Token: 0x02002D79 RID: 11641
			public class FLANNEL_BLUE_MIDDLE
			{
				// Token: 0x0400C479 RID: 50297
				public static LocString NAME = "Scrub Flannel Outfit";
			}

			// Token: 0x02002D7A RID: 11642
			public class FLANNEL_PURPLE
			{
				// Token: 0x0400C47A RID: 50298
				public static LocString NAME = "Fusion Flannel Outfit";
			}

			// Token: 0x02002D7B RID: 11643
			public class FLANNEL_PINK_ORCHID
			{
				// Token: 0x0400C47B RID: 50299
				public static LocString NAME = "Flare Flannel Outfit";
			}

			// Token: 0x02002D7C RID: 11644
			public class FLANNEL_WHITE
			{
				// Token: 0x0400C47C RID: 50300
				public static LocString NAME = "White Flannel Outfit";
			}

			// Token: 0x02002D7D RID: 11645
			public class FLANNEL_BLACK
			{
				// Token: 0x0400C47D RID: 50301
				public static LocString NAME = "Monochrome Flannel Outfit";
			}
		}

		// Token: 0x020023C7 RID: 9159
		public class ROLES_SCREEN
		{
			// Token: 0x04009FC2 RID: 40898
			public static LocString MANAGEMENT_BUTTON = "JOBS";

			// Token: 0x04009FC3 RID: 40899
			public static LocString ROLE_PROGRESS = "<b>Job Experience: {0}/{1}</b>\nDuplicants can become eligible for specialized jobs by maxing their current job experience";

			// Token: 0x04009FC4 RID: 40900
			public static LocString NO_JOB_STATION_WARNING = string.Concat(new string[]
			{
				"Build a ",
				UI.PRE_KEYWORD,
				"Printing Pod",
				UI.PST_KEYWORD,
				" to unlock this menu\n\nThe ",
				UI.PRE_KEYWORD,
				"Printing Pod",
				UI.PST_KEYWORD,
				" can be found in the ",
				UI.FormatAsBuildMenuTab("Base Tab", global::Action.Plan1),
				" of the Build Menu"
			});

			// Token: 0x04009FC5 RID: 40901
			public static LocString AUTO_PRIORITIZE = "Auto-Prioritize:";

			// Token: 0x04009FC6 RID: 40902
			public static LocString AUTO_PRIORITIZE_ENABLED = "Duplicant priorities are automatically reconfigured when they are assigned a new job";

			// Token: 0x04009FC7 RID: 40903
			public static LocString AUTO_PRIORITIZE_DISABLED = "Duplicant priorities can only be changed manually";

			// Token: 0x04009FC8 RID: 40904
			public static LocString EXPECTATION_ALERT_EXPECTATION = "Current Morale: {0}\nJob Morale Needs: {1}";

			// Token: 0x04009FC9 RID: 40905
			public static LocString EXPECTATION_ALERT_JOB = "Current Morale: {0}\n{2} Minimum Morale: {1}";

			// Token: 0x04009FCA RID: 40906
			public static LocString EXPECTATION_ALERT_TARGET_JOB = "{2}'s Current: {0} Morale\n{3} Minimum Morale: {1}";

			// Token: 0x04009FCB RID: 40907
			public static LocString EXPECTATION_ALERT_DESC_EXPECTATION = "This Duplicant's Morale is too low to handle the rigors of this position, which will cause them Stress over time.";

			// Token: 0x04009FCC RID: 40908
			public static LocString EXPECTATION_ALERT_DESC_JOB = "This Duplicant's Morale is too low to handle the assigned job, which will cause them Stress over time.";

			// Token: 0x04009FCD RID: 40909
			public static LocString EXPECTATION_ALERT_DESC_TARGET_JOB = "This Duplicant's Morale is too low to handle the rigors of this position, which will cause them Stress over time.";

			// Token: 0x04009FCE RID: 40910
			public static LocString HIGHEST_EXPECTATIONS_TIER = "<b>Highest Expectations</b>";

			// Token: 0x04009FCF RID: 40911
			public static LocString ADDED_EXPECTATIONS_AMOUNT = " (+{0} Expectation)";

			// Token: 0x02002D7E RID: 11646
			public class WIDGET
			{
				// Token: 0x0400C47E RID: 50302
				public static LocString NUMBER_OF_MASTERS_TOOLTIP = "<b>Duplicants who have mastered this job:</b>{0}";

				// Token: 0x0400C47F RID: 50303
				public static LocString NO_MASTERS_TOOLTIP = "<b>No Duplicants have mastered this job</b>";
			}

			// Token: 0x02002D7F RID: 11647
			public class TIER_NAMES
			{
				// Token: 0x0400C480 RID: 50304
				public static LocString ZERO = "Tier 0";

				// Token: 0x0400C481 RID: 50305
				public static LocString ONE = "Tier 1";

				// Token: 0x0400C482 RID: 50306
				public static LocString TWO = "Tier 2";

				// Token: 0x0400C483 RID: 50307
				public static LocString THREE = "Tier 3";

				// Token: 0x0400C484 RID: 50308
				public static LocString FOUR = "Tier 4";

				// Token: 0x0400C485 RID: 50309
				public static LocString FIVE = "Tier 5";

				// Token: 0x0400C486 RID: 50310
				public static LocString SIX = "Tier 6";

				// Token: 0x0400C487 RID: 50311
				public static LocString SEVEN = "Tier 7";

				// Token: 0x0400C488 RID: 50312
				public static LocString EIGHT = "Tier 8";

				// Token: 0x0400C489 RID: 50313
				public static LocString NINE = "Tier 9";
			}

			// Token: 0x02002D80 RID: 11648
			public class SLOTS
			{
				// Token: 0x0400C48A RID: 50314
				public static LocString UNASSIGNED = "Vacant Position";

				// Token: 0x0400C48B RID: 50315
				public static LocString UNASSIGNED_TOOLTIP = UI.CLICK(UI.ClickType.Click) + " to assign a Duplicant to this job opening";

				// Token: 0x0400C48C RID: 50316
				public static LocString NOSLOTS = "No slots available";

				// Token: 0x0400C48D RID: 50317
				public static LocString NO_ELIGIBLE_DUPLICANTS = "No Duplicants meet the requirements for this job";

				// Token: 0x0400C48E RID: 50318
				public static LocString ASSIGNMENT_PENDING = "(Pending)";

				// Token: 0x0400C48F RID: 50319
				public static LocString PICK_JOB = "No Job";

				// Token: 0x0400C490 RID: 50320
				public static LocString PICK_DUPLICANT = "None";
			}

			// Token: 0x02002D81 RID: 11649
			public class DROPDOWN
			{
				// Token: 0x0400C491 RID: 50321
				public static LocString NAME_AND_ROLE = "{0} <color=#F44A47FF>({1})</color>";

				// Token: 0x0400C492 RID: 50322
				public static LocString ALREADY_ROLE = "(Currently {0})";
			}

			// Token: 0x02002D82 RID: 11650
			public class SIDEBAR
			{
				// Token: 0x0400C493 RID: 50323
				public static LocString ASSIGNED_DUPLICANTS = "Assigned Duplicants";

				// Token: 0x0400C494 RID: 50324
				public static LocString UNASSIGNED_DUPLICANTS = "Unassigned Duplicants";

				// Token: 0x0400C495 RID: 50325
				public static LocString UNASSIGN = "Unassign job";
			}

			// Token: 0x02002D83 RID: 11651
			public class PRIORITY
			{
				// Token: 0x0400C496 RID: 50326
				public static LocString TITLE = "Job Priorities";

				// Token: 0x0400C497 RID: 50327
				public static LocString DESCRIPTION = "{0}s prioritize these work errands: ";

				// Token: 0x0400C498 RID: 50328
				public static LocString NO_EFFECT = "This job does not affect errand prioritization";
			}

			// Token: 0x02002D84 RID: 11652
			public class RESUME
			{
				// Token: 0x0400C499 RID: 50329
				public static LocString TITLE = "Qualifications";

				// Token: 0x0400C49A RID: 50330
				public static LocString PREVIOUS_ROLES = "PREVIOUS DUTIES";

				// Token: 0x0400C49B RID: 50331
				public static LocString UNASSIGNED = "Unassigned";

				// Token: 0x0400C49C RID: 50332
				public static LocString NO_SELECTION = "No Duplicant selected";
			}

			// Token: 0x02002D85 RID: 11653
			public class PERKS
			{
				// Token: 0x0400C49D RID: 50333
				public static LocString TITLE_BASICTRAINING = "Basic Job Training";

				// Token: 0x0400C49E RID: 50334
				public static LocString TITLE_MORETRAINING = "Additional Job Training";

				// Token: 0x0400C49F RID: 50335
				public static LocString NO_PERKS = "This job comes with no training";

				// Token: 0x0400C4A0 RID: 50336
				public static LocString ATTRIBUTE_EFFECT_FMT = "<b>{0}</b> " + UI.PRE_KEYWORD + "{1}" + UI.PST_KEYWORD;

				// Token: 0x02003A0C RID: 14860
				public class CAN_DIG_VERY_FIRM
				{
					// Token: 0x0400E7B8 RID: 59320
					public static LocString DESCRIPTION = UI.FormatAsLink(ELEMENTS.HARDNESS.HARDNESS_DESCRIPTOR.VERYFIRM + " Material", "HARDNESS") + " Mining";
				}

				// Token: 0x02003A0D RID: 14861
				public class CAN_DIG_NEARLY_IMPENETRABLE
				{
					// Token: 0x0400E7B9 RID: 59321
					public static LocString DESCRIPTION = UI.FormatAsLink("Abyssalite", "KATAIRITE") + " Mining";
				}

				// Token: 0x02003A0E RID: 14862
				public class CAN_DIG_SUPER_SUPER_HARD
				{
					// Token: 0x0400E7BA RID: 59322
					public static LocString DESCRIPTION = UI.FormatAsLink("Diamond", "DIAMOND") + " and " + UI.FormatAsLink("Obsidian", "OBSIDIAN") + " Mining";
				}

				// Token: 0x02003A0F RID: 14863
				public class CAN_DIG_RADIOACTIVE_MATERIALS
				{
					// Token: 0x0400E7BB RID: 59323
					public static LocString DESCRIPTION = UI.FormatAsLink("Corium", "CORIUM") + " Mining";
				}

				// Token: 0x02003A10 RID: 14864
				public class CAN_DIG_UNOBTANIUM
				{
					// Token: 0x0400E7BC RID: 59324
					public static LocString DESCRIPTION = UI.FormatAsLink("Neutronium", "UNOBTANIUM") + " Mining";
				}

				// Token: 0x02003A11 RID: 14865
				public class CAN_ART
				{
					// Token: 0x0400E7BD RID: 59325
					public static LocString DESCRIPTION = string.Concat(new string[]
					{
						"Can produce artwork using:\n<indent=30px>• ",
						BUILDINGS.PREFABS.CANVAS.NAME,
						"\n• ",
						BUILDINGS.PREFABS.SMALLSCULPTURE.NAME,
						"\n• ",
						BUILDINGS.PREFABS.SCULPTURE.NAME,
						"\n• ",
						BUILDINGS.PREFABS.ICESCULPTURE.NAME,
						"\n• ",
						BUILDINGS.PREFABS.METALSCULPTURE.NAME,
						"\n• ",
						BUILDINGS.PREFABS.WOODSCULPTURE.NAME,
						"</indent>"
					});
				}

				// Token: 0x02003A12 RID: 14866
				public class CAN_ART_UGLY
				{
					// Token: 0x0400E7BE RID: 59326
					public static LocString DESCRIPTION = UI.PRE_KEYWORD + "Crude" + UI.PST_KEYWORD + " artwork quality";
				}

				// Token: 0x02003A13 RID: 14867
				public class CAN_ART_OKAY
				{
					// Token: 0x0400E7BF RID: 59327
					public static LocString DESCRIPTION = UI.PRE_KEYWORD + "Mediocre" + UI.PST_KEYWORD + " artwork quality";
				}

				// Token: 0x02003A14 RID: 14868
				public class CAN_ART_GREAT
				{
					// Token: 0x0400E7C0 RID: 59328
					public static LocString DESCRIPTION = UI.PRE_KEYWORD + "Master" + UI.PST_KEYWORD + " artwork quality";
				}

				// Token: 0x02003A15 RID: 14869
				public class CAN_FARM_TINKER
				{
					// Token: 0x0400E7C1 RID: 59329
					public static LocString DESCRIPTION = UI.FormatAsLink("Crop Tending", "PLANTS");
				}

				// Token: 0x02003A16 RID: 14870
				public class CAN_IDENTIFY_MUTANT_SEEDS
				{
					// Token: 0x0400E7C2 RID: 59330
					public static LocString DESCRIPTION = string.Concat(new string[]
					{
						"Can identify ",
						UI.PRE_KEYWORD,
						"Mutant Seeds",
						UI.PST_KEYWORD,
						" at the ",
						BUILDINGS.PREFABS.GENETICANALYSISSTATION.NAME
					});
				}

				// Token: 0x02003A17 RID: 14871
				public class CAN_FARM_STATION
				{
					// Token: 0x0400E7C3 RID: 59331
					public static LocString DESCRIPTION = string.Concat(new string[]
					{
						"Can craft ",
						UI.PRE_KEYWORD,
						"Micronutrient Fertilizer",
						UI.PST_KEYWORD,
						" at the ",
						BUILDINGS.PREFABS.FARMSTATION.NAME
					});
				}

				// Token: 0x02003A18 RID: 14872
				public class CAN_WRANGLE_CREATURES
				{
					// Token: 0x0400E7C4 RID: 59332
					public static LocString DESCRIPTION = "Critter Wrangling";
				}

				// Token: 0x02003A19 RID: 14873
				public class CAN_USE_BUILDING
				{
					// Token: 0x0400E7C5 RID: 59333
					public static LocString DESCRIPTION = "{0} Usage";
				}

				// Token: 0x02003A1A RID: 14874
				public class CAN_USE_RANCH_STATION
				{
					// Token: 0x0400E7C6 RID: 59334
					public static LocString DESCRIPTION = BUILDINGS.PREFABS.RANCHSTATION.NAME + " Usage";
				}

				// Token: 0x02003A1B RID: 14875
				public class CAN_USE_MILKING_STATION
				{
					// Token: 0x0400E7C7 RID: 59335
					public static LocString DESCRIPTION = BUILDINGS.PREFABS.MILKINGSTATION.NAME + " Usage";
				}

				// Token: 0x02003A1C RID: 14876
				public class CAN_POWER_TINKER
				{
					// Token: 0x0400E7C8 RID: 59336
					public static LocString DESCRIPTION = UI.FormatAsLink("Generator", "POWER") + " Tuning and " + UI.FormatAsLink("Microchip", "POWER_STATION_TOOLS") + " Crafting";
				}

				// Token: 0x02003A1D RID: 14877
				public class CAN_ELECTRIC_GRILL
				{
					// Token: 0x0400E7C9 RID: 59337
					public static LocString DESCRIPTION = BUILDINGS.PREFABS.COOKINGSTATION.NAME + " Usage";
				}

				// Token: 0x02003A1E RID: 14878
				public class CAN_GAS_RANGE
				{
					// Token: 0x0400E7CA RID: 59338
					public static LocString DESCRIPTION = BUILDINGS.PREFABS.GOURMETCOOKINGSTATION.NAME + " Usage";
				}

				// Token: 0x02003A1F RID: 14879
				public class CAN_DEEP_FRYER
				{
					// Token: 0x0400E7CB RID: 59339
					public static LocString DESCRIPTION = BUILDINGS.PREFABS.DEEPFRYER.NAME + " Usage";
				}

				// Token: 0x02003A20 RID: 14880
				public class CAN_SPICE_GRINDER
				{
					// Token: 0x0400E7CC RID: 59340
					public static LocString DESCRIPTION = BUILDINGS.PREFABS.SPICEGRINDER.NAME + " Usage";
				}

				// Token: 0x02003A21 RID: 14881
				public class CAN_MAKE_MISSILES
				{
					// Token: 0x0400E7CD RID: 59341
					public static LocString DESCRIPTION = BUILDINGS.PREFABS.MISSILEFABRICATOR.NAME + " Usage";
				}

				// Token: 0x02003A22 RID: 14882
				public class CAN_CRAFT_ELECTRONICS
				{
					// Token: 0x0400E7CE RID: 59342
					public static LocString DESCRIPTION = BUILDINGS.PREFABS.ADVANCEDCRAFTINGTABLE.NAME + " Usage";
				}

				// Token: 0x02003A23 RID: 14883
				public class ADVANCED_RESEARCH
				{
					// Token: 0x0400E7CF RID: 59343
					public static LocString DESCRIPTION = BUILDINGS.PREFABS.ADVANCEDRESEARCHCENTER.NAME + " Usage";
				}

				// Token: 0x02003A24 RID: 14884
				public class INTERSTELLAR_RESEARCH
				{
					// Token: 0x0400E7D0 RID: 59344
					public static LocString DESCRIPTION = BUILDINGS.PREFABS.COSMICRESEARCHCENTER.NAME + " Usage";
				}

				// Token: 0x02003A25 RID: 14885
				public class NUCLEAR_RESEARCH
				{
					// Token: 0x0400E7D1 RID: 59345
					public static LocString DESCRIPTION = BUILDINGS.PREFABS.NUCLEARRESEARCHCENTER.NAME + " Usage";
				}

				// Token: 0x02003A26 RID: 14886
				public class ORBITAL_RESEARCH
				{
					// Token: 0x0400E7D2 RID: 59346
					public static LocString DESCRIPTION = BUILDINGS.PREFABS.DLC1COSMICRESEARCHCENTER.NAME + " Usage";
				}

				// Token: 0x02003A27 RID: 14887
				public class GEYSER_TUNING
				{
					// Token: 0x0400E7D3 RID: 59347
					public static LocString DESCRIPTION = BUILDINGS.PREFABS.GEOTUNER.NAME + " Usage";
				}

				// Token: 0x02003A28 RID: 14888
				public class CHEMISTRY
				{
					// Token: 0x0400E7D4 RID: 59348
					public static LocString DESCRIPTION = BUILDINGS.PREFABS.CHEMICALREFINERY.NAME + " Usage";
				}

				// Token: 0x02003A29 RID: 14889
				public class CAN_CLOTHING_ALTERATION
				{
					// Token: 0x0400E7D5 RID: 59349
					public static LocString DESCRIPTION = BUILDINGS.PREFABS.CLOTHINGALTERATIONSTATION.NAME + " Usage";
				}

				// Token: 0x02003A2A RID: 14890
				public class CAN_STUDY_WORLD_OBJECTS
				{
					// Token: 0x0400E7D6 RID: 59350
					public static LocString DESCRIPTION = "Geographical Analysis";
				}

				// Token: 0x02003A2B RID: 14891
				public class CAN_STUDY_ARTIFACTS
				{
					// Token: 0x0400E7D7 RID: 59351
					public static LocString DESCRIPTION = BUILDINGS.PREFABS.ARTIFACTANALYSISSTATION.NAME + " Usage";
				}

				// Token: 0x02003A2C RID: 14892
				public class CAN_USE_CLUSTER_TELESCOPE
				{
					// Token: 0x0400E7D8 RID: 59352
					public static LocString DESCRIPTION = BUILDINGS.PREFABS.CLUSTERTELESCOPE.NAME + " Usage";
				}

				// Token: 0x02003A2D RID: 14893
				public class CAN_CLUSTERTELESCOPEENCLOSED
				{
					// Token: 0x0400E7D9 RID: 59353
					public static LocString DESCRIPTION = BUILDINGS.PREFABS.CLUSTERTELESCOPEENCLOSED.NAME + " Usage";
				}

				// Token: 0x02003A2E RID: 14894
				public class EXOSUIT_EXPERTISE
				{
					// Token: 0x0400E7DA RID: 59354
					public static LocString DESCRIPTION = UI.FormatAsLink("Exosuit", "EQUIPMENT") + " Penalty Reduction";
				}

				// Token: 0x02003A2F RID: 14895
				public class EXOSUIT_DURABILITY
				{
					// Token: 0x0400E7DB RID: 59355
					public static LocString DESCRIPTION = "Slows " + UI.FormatAsLink("Exosuit", "EQUIPMENT") + " Durability Damage";
				}

				// Token: 0x02003A30 RID: 14896
				public class CONVEYOR_BUILD
				{
					// Token: 0x0400E7DC RID: 59356
					public static LocString DESCRIPTION = BUILDINGS.PREFABS.SOLIDCONDUIT.NAME + " Construction";
				}

				// Token: 0x02003A31 RID: 14897
				public class CAN_DO_PLUMBING
				{
					// Token: 0x0400E7DD RID: 59357
					public static LocString DESCRIPTION = "Pipe Emptying";
				}

				// Token: 0x02003A32 RID: 14898
				public class CAN_USE_ROCKETS
				{
					// Token: 0x0400E7DE RID: 59358
					public static LocString DESCRIPTION = BUILDINGS.PREFABS.COMMANDMODULE.NAME + " Usage";
				}

				// Token: 0x02003A33 RID: 14899
				public class CAN_DO_ASTRONAUT_TRAINING
				{
					// Token: 0x0400E7DF RID: 59359
					public static LocString DESCRIPTION = BUILDINGS.PREFABS.ASTRONAUTTRAININGCENTER.NAME + " Usage";
				}

				// Token: 0x02003A34 RID: 14900
				public class CAN_MISSION_CONTROL
				{
					// Token: 0x0400E7E0 RID: 59360
					public static LocString DESCRIPTION = BUILDINGS.PREFABS.MISSIONCONTROL.NAME + " Usage";
				}

				// Token: 0x02003A35 RID: 14901
				public class CAN_PILOT_ROCKET
				{
					// Token: 0x0400E7E1 RID: 59361
					public static LocString DESCRIPTION = BUILDINGS.PREFABS.ROCKETCONTROLSTATION.NAME + " Usage";
				}

				// Token: 0x02003A36 RID: 14902
				public class CAN_COMPOUND
				{
					// Token: 0x0400E7E2 RID: 59362
					public static LocString DESCRIPTION = BUILDINGS.PREFABS.APOTHECARY.NAME + " Usage";
				}

				// Token: 0x02003A37 RID: 14903
				public class CAN_DOCTOR
				{
					// Token: 0x0400E7E3 RID: 59363
					public static LocString DESCRIPTION = BUILDINGS.PREFABS.DOCTORSTATION.NAME + " Usage";
				}

				// Token: 0x02003A38 RID: 14904
				public class CAN_ADVANCED_MEDICINE
				{
					// Token: 0x0400E7E4 RID: 59364
					public static LocString DESCRIPTION = BUILDINGS.PREFABS.ADVANCEDDOCTORSTATION.NAME + " Usage";
				}

				// Token: 0x02003A39 RID: 14905
				public class CAN_DEMOLISH
				{
					// Token: 0x0400E7E5 RID: 59365
					public static LocString DESCRIPTION = "Demolish Gravitas Buildings";
				}

				// Token: 0x02003A3A RID: 14906
				public class EXTRA_BIONIC_BATTERIES
				{
					// Token: 0x0400E7E6 RID: 59366
					public static LocString DESCRIPTION = "Extra " + UI.FormatAsLink("Power Banks", "ELECTROBANK");
				}

				// Token: 0x02003A3B RID: 14907
				public class REDUCED_GUNK_PRODUCTION
				{
					// Token: 0x0400E7E7 RID: 59367
					public static LocString DESCRIPTION = "10% Slower " + UI.FormatAsLink("Gunk", "LIQUIDGUNK") + " Buildup";
				}

				// Token: 0x02003A3C RID: 14908
				public class EFFICIENT_BIONIC_GEARS
				{
					// Token: 0x0400E7E8 RID: 59368
					public static LocString DESCRIPTION = "50% Grinding Gears penalty reduction";
				}
			}

			// Token: 0x02002D86 RID: 11654
			public class ASSIGNMENT_REQUIREMENTS
			{
				// Token: 0x0400C4A1 RID: 50337
				public static LocString TITLE = "Qualifications";

				// Token: 0x0400C4A2 RID: 50338
				public static LocString NONE = "This position has no qualification requirements";

				// Token: 0x0400C4A3 RID: 50339
				public static LocString ALREADY_IS_ROLE = "{0} <b>is already</b> assigned to the {1} position";

				// Token: 0x0400C4A4 RID: 50340
				public static LocString ALREADY_IS_JOBLESS = "{0} <b>is already</b> unemployed";

				// Token: 0x0400C4A5 RID: 50341
				public static LocString MASTERED = "{0} has mastered the {1} position";

				// Token: 0x0400C4A6 RID: 50342
				public static LocString WILL_BE_UNASSIGNED = "Note: Assigning {0} to {1} will <color=#F44A47FF>unassign</color> them from {2}";

				// Token: 0x0400C4A7 RID: 50343
				public static LocString RELEVANT_ATTRIBUTES = "Relevant skills:";

				// Token: 0x0400C4A8 RID: 50344
				public static LocString APTITUDES = "Interests";

				// Token: 0x0400C4A9 RID: 50345
				public static LocString RELEVANT_APTITUDES = "Relevant Interests:";

				// Token: 0x0400C4AA RID: 50346
				public static LocString NO_APTITUDE = "None";

				// Token: 0x02003A3D RID: 14909
				public class ELIGIBILITY
				{
					// Token: 0x0400E7E9 RID: 59369
					public static LocString ELIGIBLE = "{0} is qualified for the {1} position";

					// Token: 0x0400E7EA RID: 59370
					public static LocString INELIGIBLE = "{0} is <color=#F44A47FF>not qualified</color> for the {1} position";
				}

				// Token: 0x02003A3E RID: 14910
				public class UNEMPLOYED
				{
					// Token: 0x0400E7EB RID: 59371
					public static LocString NAME = "Unassigned";

					// Token: 0x0400E7EC RID: 59372
					public static LocString DESCRIPTION = "Duplicant must not already have a job assignment";
				}

				// Token: 0x02003A3F RID: 14911
				public class HAS_COLONY_LEADER
				{
					// Token: 0x0400E7ED RID: 59373
					public static LocString NAME = "Has colony leader";

					// Token: 0x0400E7EE RID: 59374
					public static LocString DESCRIPTION = "A colony leader must be assigned";
				}

				// Token: 0x02003A40 RID: 14912
				public class HAS_ATTRIBUTE_DIGGING_BASIC
				{
					// Token: 0x0400E7EF RID: 59375
					public static LocString NAME = "Basic Digging";

					// Token: 0x0400E7F0 RID: 59376
					public static LocString DESCRIPTION = "Must have at least {0} digging skill";
				}

				// Token: 0x02003A41 RID: 14913
				public class HAS_ATTRIBUTE_COOKING_BASIC
				{
					// Token: 0x0400E7F1 RID: 59377
					public static LocString NAME = "Basic Cooking";

					// Token: 0x0400E7F2 RID: 59378
					public static LocString DESCRIPTION = "Must have at least {0} cooking skill";
				}

				// Token: 0x02003A42 RID: 14914
				public class HAS_ATTRIBUTE_LEARNING_BASIC
				{
					// Token: 0x0400E7F3 RID: 59379
					public static LocString NAME = "Basic Learning";

					// Token: 0x0400E7F4 RID: 59380
					public static LocString DESCRIPTION = "Must have at least {0} learning skill";
				}

				// Token: 0x02003A43 RID: 14915
				public class HAS_ATTRIBUTE_LEARNING_MEDIUM
				{
					// Token: 0x0400E7F5 RID: 59381
					public static LocString NAME = "Medium Learning";

					// Token: 0x0400E7F6 RID: 59382
					public static LocString DESCRIPTION = "Must have at least {0} learning skill";
				}

				// Token: 0x02003A44 RID: 14916
				public class HAS_EXPERIENCE
				{
					// Token: 0x0400E7F7 RID: 59383
					public static LocString NAME = "{0} Experience";

					// Token: 0x0400E7F8 RID: 59384
					public static LocString DESCRIPTION = "Mastery of the <b>{0}</b> job";
				}

				// Token: 0x02003A45 RID: 14917
				public class HAS_COMPLETED_ANY_OTHER_ROLE
				{
					// Token: 0x0400E7F9 RID: 59385
					public static LocString NAME = "General Experience";

					// Token: 0x0400E7FA RID: 59386
					public static LocString DESCRIPTION = "Mastery of <b>at least one</b> job";
				}

				// Token: 0x02003A46 RID: 14918
				public class CHOREGROUP_ENABLED
				{
					// Token: 0x0400E7FB RID: 59387
					public static LocString NAME = "Can perform {0}";

					// Token: 0x0400E7FC RID: 59388
					public static LocString DESCRIPTION = "Capable of performing <b>{0}</b> jobs";
				}
			}

			// Token: 0x02002D87 RID: 11655
			public class EXPECTATIONS
			{
				// Token: 0x0400C4AB RID: 50347
				public static LocString TITLE = "Special Provisions Request";

				// Token: 0x0400C4AC RID: 50348
				public static LocString NO_EXPECTATIONS = "No additional provisions are required to perform this job";

				// Token: 0x02003A47 RID: 14919
				public class PRIVATE_ROOM
				{
					// Token: 0x0400E7FD RID: 59389
					public static LocString NAME = "Private Bedroom";

					// Token: 0x0400E7FE RID: 59390
					public static LocString DESCRIPTION = "Duplicants in this job would appreciate their own place to unwind";
				}

				// Token: 0x02003A48 RID: 14920
				public class FOOD_QUALITY
				{
					// Token: 0x02003EBE RID: 16062
					public class MINOR
					{
						// Token: 0x0400F2B7 RID: 62135
						public static LocString NAME = "Standard Food";

						// Token: 0x0400F2B8 RID: 62136
						public static LocString DESCRIPTION = "Duplicants employed in this Tier desire food that meets basic living standards";
					}

					// Token: 0x02003EBF RID: 16063
					public class MEDIUM
					{
						// Token: 0x0400F2B9 RID: 62137
						public static LocString NAME = "Good Food";

						// Token: 0x0400F2BA RID: 62138
						public static LocString DESCRIPTION = "Duplicants employed in this Tier desire decent food for their efforts";
					}

					// Token: 0x02003EC0 RID: 16064
					public class HIGH
					{
						// Token: 0x0400F2BB RID: 62139
						public static LocString NAME = "Great Food";

						// Token: 0x0400F2BC RID: 62140
						public static LocString DESCRIPTION = "Duplicants employed in this Tier desire better than average food";
					}

					// Token: 0x02003EC1 RID: 16065
					public class VERY_HIGH
					{
						// Token: 0x0400F2BD RID: 62141
						public static LocString NAME = "Superb Food";

						// Token: 0x0400F2BE RID: 62142
						public static LocString DESCRIPTION = "Duplicants employed in this Tier have a refined taste for food";
					}

					// Token: 0x02003EC2 RID: 16066
					public class EXCEPTIONAL
					{
						// Token: 0x0400F2BF RID: 62143
						public static LocString NAME = "Ambrosial Food";

						// Token: 0x0400F2C0 RID: 62144
						public static LocString DESCRIPTION = "Duplicants employed in this Tier expect only the best cuisine";
					}
				}

				// Token: 0x02003A49 RID: 14921
				public class DECOR
				{
					// Token: 0x02003EC3 RID: 16067
					public class MINOR
					{
						// Token: 0x0400F2C1 RID: 62145
						public static LocString NAME = "Minor Decor";

						// Token: 0x0400F2C2 RID: 62146
						public static LocString DESCRIPTION = "Duplicants employed in this Tier desire slightly improved colony decor";
					}

					// Token: 0x02003EC4 RID: 16068
					public class MEDIUM
					{
						// Token: 0x0400F2C3 RID: 62147
						public static LocString NAME = "Medium Decor";

						// Token: 0x0400F2C4 RID: 62148
						public static LocString DESCRIPTION = "Duplicants employed in this Tier desire reasonably improved colony decor";
					}

					// Token: 0x02003EC5 RID: 16069
					public class HIGH
					{
						// Token: 0x0400F2C5 RID: 62149
						public static LocString NAME = "High Decor";

						// Token: 0x0400F2C6 RID: 62150
						public static LocString DESCRIPTION = "Duplicants employed in this Tier desire a decent increase in colony decor";
					}

					// Token: 0x02003EC6 RID: 16070
					public class VERY_HIGH
					{
						// Token: 0x0400F2C7 RID: 62151
						public static LocString NAME = "Superb Decor";

						// Token: 0x0400F2C8 RID: 62152
						public static LocString DESCRIPTION = "Duplicants employed in this Tier desire majorly improved colony decor";
					}

					// Token: 0x02003EC7 RID: 16071
					public class UNREASONABLE
					{
						// Token: 0x0400F2C9 RID: 62153
						public static LocString NAME = "Decadent Decor";

						// Token: 0x0400F2CA RID: 62154
						public static LocString DESCRIPTION = "Duplicants employed in this Tier desire unrealistically luxurious improvements to decor";
					}
				}

				// Token: 0x02003A4A RID: 14922
				public class QUALITYOFLIFE
				{
					// Token: 0x02003EC8 RID: 16072
					public class TIER0
					{
						// Token: 0x0400F2CB RID: 62155
						public static LocString NAME = "Morale Requirements";

						// Token: 0x0400F2CC RID: 62156
						public static LocString DESCRIPTION = "Tier 0";
					}

					// Token: 0x02003EC9 RID: 16073
					public class TIER1
					{
						// Token: 0x0400F2CD RID: 62157
						public static LocString NAME = "Morale Requirements";

						// Token: 0x0400F2CE RID: 62158
						public static LocString DESCRIPTION = "Tier 1";
					}

					// Token: 0x02003ECA RID: 16074
					public class TIER2
					{
						// Token: 0x0400F2CF RID: 62159
						public static LocString NAME = "Morale Requirements";

						// Token: 0x0400F2D0 RID: 62160
						public static LocString DESCRIPTION = "Tier 2";
					}

					// Token: 0x02003ECB RID: 16075
					public class TIER3
					{
						// Token: 0x0400F2D1 RID: 62161
						public static LocString NAME = "Morale Requirements";

						// Token: 0x0400F2D2 RID: 62162
						public static LocString DESCRIPTION = "Tier 3";
					}

					// Token: 0x02003ECC RID: 16076
					public class TIER4
					{
						// Token: 0x0400F2D3 RID: 62163
						public static LocString NAME = "Morale Requirements";

						// Token: 0x0400F2D4 RID: 62164
						public static LocString DESCRIPTION = "Tier 4";
					}

					// Token: 0x02003ECD RID: 16077
					public class TIER5
					{
						// Token: 0x0400F2D5 RID: 62165
						public static LocString NAME = "Morale Requirements";

						// Token: 0x0400F2D6 RID: 62166
						public static LocString DESCRIPTION = "Tier 5";
					}

					// Token: 0x02003ECE RID: 16078
					public class TIER6
					{
						// Token: 0x0400F2D7 RID: 62167
						public static LocString NAME = "Morale Requirements";

						// Token: 0x0400F2D8 RID: 62168
						public static LocString DESCRIPTION = "Tier 6";
					}

					// Token: 0x02003ECF RID: 16079
					public class TIER7
					{
						// Token: 0x0400F2D9 RID: 62169
						public static LocString NAME = "Morale Requirements";

						// Token: 0x0400F2DA RID: 62170
						public static LocString DESCRIPTION = "Tier 7";
					}

					// Token: 0x02003ED0 RID: 16080
					public class TIER8
					{
						// Token: 0x0400F2DB RID: 62171
						public static LocString NAME = "Morale Requirements";

						// Token: 0x0400F2DC RID: 62172
						public static LocString DESCRIPTION = "Tier 8";
					}
				}
			}
		}

		// Token: 0x020023C8 RID: 9160
		public class GAMEPLAY_EVENT_INFO_SCREEN
		{
			// Token: 0x04009FD0 RID: 40912
			public static LocString WHERE = "WHERE: {0}";

			// Token: 0x04009FD1 RID: 40913
			public static LocString WHEN = "WHEN: {0}";
		}

		// Token: 0x020023C9 RID: 9161
		public class DEBUG_TOOLS
		{
			// Token: 0x04009FD2 RID: 40914
			public static LocString ENTER_TEXT = "";

			// Token: 0x04009FD3 RID: 40915
			public static LocString DEBUG_ACTIVE = "Debug tools active";

			// Token: 0x04009FD4 RID: 40916
			public static LocString INVALID_LOCATION = "Invalid Location";

			// Token: 0x02002D88 RID: 11656
			public class PAINT_ELEMENTS_SCREEN
			{
				// Token: 0x0400C4AD RID: 50349
				public static LocString TITLE = "CELL PAINTER";

				// Token: 0x0400C4AE RID: 50350
				public static LocString ELEMENT = "Element";

				// Token: 0x0400C4AF RID: 50351
				public static LocString MASS_KG = "Mass (kg)";

				// Token: 0x0400C4B0 RID: 50352
				public static LocString TEMPERATURE_KELVIN = "Temperature (K)";

				// Token: 0x0400C4B1 RID: 50353
				public static LocString DISEASE = "Disease";

				// Token: 0x0400C4B2 RID: 50354
				public static LocString DISEASE_COUNT = "Disease Count";

				// Token: 0x0400C4B3 RID: 50355
				public static LocString BUILDINGS = "Buildings:";

				// Token: 0x0400C4B4 RID: 50356
				public static LocString CELLS = "Cells:";

				// Token: 0x0400C4B5 RID: 50357
				public static LocString ADD_FOW_MASK = "Prevent FoW Reveal";

				// Token: 0x0400C4B6 RID: 50358
				public static LocString REMOVE_FOW_MASK = "Allow FoW Reveal";

				// Token: 0x0400C4B7 RID: 50359
				public static LocString PAINT = "Paint";

				// Token: 0x0400C4B8 RID: 50360
				public static LocString SAMPLE = "Sample";

				// Token: 0x0400C4B9 RID: 50361
				public static LocString STORE = "Store";

				// Token: 0x0400C4BA RID: 50362
				public static LocString FILL = "Fill";

				// Token: 0x0400C4BB RID: 50363
				public static LocString SPAWN_ALL = "Spawn All (Slow)";
			}

			// Token: 0x02002D89 RID: 11657
			public class SAVE_BASE_TEMPLATE
			{
				// Token: 0x0400C4BC RID: 50364
				public static LocString TITLE = "Base and World Tools";

				// Token: 0x0400C4BD RID: 50365
				public static LocString SAVE_TITLE = "Save Selection";

				// Token: 0x0400C4BE RID: 50366
				public static LocString CLEAR_BUTTON = "Clear Floor";

				// Token: 0x0400C4BF RID: 50367
				public static LocString DESTROY_BUTTON = "Destroy";

				// Token: 0x0400C4C0 RID: 50368
				public static LocString DECONSTRUCT_BUTTON = "Deconstruct";

				// Token: 0x0400C4C1 RID: 50369
				public static LocString CLEAR_SELECTION_BUTTON = "Clear Selection";

				// Token: 0x0400C4C2 RID: 50370
				public static LocString DEFAULT_SAVE_NAME = "TemplateSaveName";

				// Token: 0x0400C4C3 RID: 50371
				public static LocString MORE = "More";

				// Token: 0x0400C4C4 RID: 50372
				public static LocString BASE_GAME_FOLDER_NAME = "Base Game";

				// Token: 0x02003A4B RID: 14923
				public class SELECTION_INFO_PANEL
				{
					// Token: 0x0400E7FF RID: 59391
					public static LocString TOTAL_MASS = "Total mass: {0}";

					// Token: 0x0400E800 RID: 59392
					public static LocString AVERAGE_MASS = "Average cell mass: {0}";

					// Token: 0x0400E801 RID: 59393
					public static LocString AVERAGE_TEMPERATURE = "Average temperature: {0}";

					// Token: 0x0400E802 RID: 59394
					public static LocString TOTAL_JOULES = "Total joules: {0}";

					// Token: 0x0400E803 RID: 59395
					public static LocString JOULES_PER_KILOGRAM = "Joules per kilogram: {0}";

					// Token: 0x0400E804 RID: 59396
					public static LocString TOTAL_RADS = "Total rads: {0}";

					// Token: 0x0400E805 RID: 59397
					public static LocString AVERAGE_RADS = "Average rads: {0}";
				}
			}
		}

		// Token: 0x020023CA RID: 9162
		public class WORLDGEN
		{
			// Token: 0x04009FD5 RID: 40917
			public static LocString NOHEADERS = "";

			// Token: 0x04009FD6 RID: 40918
			public static LocString COMPLETE = "Success! Space adventure awaits.";

			// Token: 0x04009FD7 RID: 40919
			public static LocString FAILED = "Goodness, has this ever gone terribly wrong!";

			// Token: 0x04009FD8 RID: 40920
			public static LocString RESTARTING = "Rebooting...";

			// Token: 0x04009FD9 RID: 40921
			public static LocString LOADING = "Loading world...";

			// Token: 0x04009FDA RID: 40922
			public static LocString GENERATINGWORLD = "The Galaxy Synthesizer";

			// Token: 0x04009FDB RID: 40923
			public static LocString CHOOSEWORLDSIZE = "Select the magnitude of your new galaxy.";

			// Token: 0x04009FDC RID: 40924
			public static LocString USING_PLAYER_SEED = "Using selected worldgen seed: {0}";

			// Token: 0x04009FDD RID: 40925
			public static LocString CLEARINGLEVEL = "Staring into the void...";

			// Token: 0x04009FDE RID: 40926
			public static LocString GENERATESOLARSYSTEM = "Catalyzing Big Bang...";

			// Token: 0x04009FDF RID: 40927
			public static LocString GENERATESOLARSYSTEM1 = "Catalyzing Big Bang...";

			// Token: 0x04009FE0 RID: 40928
			public static LocString GENERATESOLARSYSTEM2 = "Catalyzing Big Bang...";

			// Token: 0x04009FE1 RID: 40929
			public static LocString GENERATESOLARSYSTEM3 = "Catalyzing Big Bang...";

			// Token: 0x04009FE2 RID: 40930
			public static LocString GENERATESOLARSYSTEM4 = "Catalyzing Big Bang...";

			// Token: 0x04009FE3 RID: 40931
			public static LocString GENERATESOLARSYSTEM5 = "Catalyzing Big Bang...";

			// Token: 0x04009FE4 RID: 40932
			public static LocString GENERATESOLARSYSTEM6 = "Approaching event horizon...";

			// Token: 0x04009FE5 RID: 40933
			public static LocString GENERATESOLARSYSTEM7 = "Approaching event horizon...";

			// Token: 0x04009FE6 RID: 40934
			public static LocString GENERATESOLARSYSTEM8 = "Approaching event horizon...";

			// Token: 0x04009FE7 RID: 40935
			public static LocString GENERATESOLARSYSTEM9 = "Approaching event horizon...";

			// Token: 0x04009FE8 RID: 40936
			public static LocString SETUPNOISE = "BANG!";

			// Token: 0x04009FE9 RID: 40937
			public static LocString BUILDNOISESOURCE = "Sorting quadrillions of atoms...";

			// Token: 0x04009FEA RID: 40938
			public static LocString BUILDNOISESOURCE1 = "Sorting quadrillions of atoms...";

			// Token: 0x04009FEB RID: 40939
			public static LocString BUILDNOISESOURCE2 = "Sorting quadrillions of atoms...";

			// Token: 0x04009FEC RID: 40940
			public static LocString BUILDNOISESOURCE3 = "Ironing the fabric of creation...";

			// Token: 0x04009FED RID: 40941
			public static LocString BUILDNOISESOURCE4 = "Ironing the fabric of creation...";

			// Token: 0x04009FEE RID: 40942
			public static LocString BUILDNOISESOURCE5 = "Ironing the fabric of creation...";

			// Token: 0x04009FEF RID: 40943
			public static LocString BUILDNOISESOURCE6 = "Taking hot meteor shower...";

			// Token: 0x04009FF0 RID: 40944
			public static LocString BUILDNOISESOURCE7 = "Tightening asteroid belts...";

			// Token: 0x04009FF1 RID: 40945
			public static LocString BUILDNOISESOURCE8 = "Tightening asteroid belts...";

			// Token: 0x04009FF2 RID: 40946
			public static LocString BUILDNOISESOURCE9 = "Tightening asteroid belts...";

			// Token: 0x04009FF3 RID: 40947
			public static LocString GENERATENOISE = "Baking igneous rock...";

			// Token: 0x04009FF4 RID: 40948
			public static LocString GENERATENOISE1 = "Multilayering sediment...";

			// Token: 0x04009FF5 RID: 40949
			public static LocString GENERATENOISE2 = "Multilayering sediment...";

			// Token: 0x04009FF6 RID: 40950
			public static LocString GENERATENOISE3 = "Multilayering sediment...";

			// Token: 0x04009FF7 RID: 40951
			public static LocString GENERATENOISE4 = "Superheating gases...";

			// Token: 0x04009FF8 RID: 40952
			public static LocString GENERATENOISE5 = "Superheating gases...";

			// Token: 0x04009FF9 RID: 40953
			public static LocString GENERATENOISE6 = "Superheating gases...";

			// Token: 0x04009FFA RID: 40954
			public static LocString GENERATENOISE7 = "Vacuuming out vacuums...";

			// Token: 0x04009FFB RID: 40955
			public static LocString GENERATENOISE8 = "Vacuuming out vacuums...";

			// Token: 0x04009FFC RID: 40956
			public static LocString GENERATENOISE9 = "Vacuuming out vacuums...";

			// Token: 0x04009FFD RID: 40957
			public static LocString NORMALISENOISE = "Interpolating suffocating gas...";

			// Token: 0x04009FFE RID: 40958
			public static LocString WORLDLAYOUT = "Freezing ice formations...";

			// Token: 0x04009FFF RID: 40959
			public static LocString WORLDLAYOUT1 = "Freezing ice formations...";

			// Token: 0x0400A000 RID: 40960
			public static LocString WORLDLAYOUT2 = "Freezing ice formations...";

			// Token: 0x0400A001 RID: 40961
			public static LocString WORLDLAYOUT3 = "Freezing ice formations...";

			// Token: 0x0400A002 RID: 40962
			public static LocString WORLDLAYOUT4 = "Melting magma...";

			// Token: 0x0400A003 RID: 40963
			public static LocString WORLDLAYOUT5 = "Melting magma...";

			// Token: 0x0400A004 RID: 40964
			public static LocString WORLDLAYOUT6 = "Melting magma...";

			// Token: 0x0400A005 RID: 40965
			public static LocString WORLDLAYOUT7 = "Sprinkling sand...";

			// Token: 0x0400A006 RID: 40966
			public static LocString WORLDLAYOUT8 = "Sprinkling sand...";

			// Token: 0x0400A007 RID: 40967
			public static LocString WORLDLAYOUT9 = "Sprinkling sand...";

			// Token: 0x0400A008 RID: 40968
			public static LocString WORLDLAYOUT10 = "Sprinkling sand...";

			// Token: 0x0400A009 RID: 40969
			public static LocString COMPLETELAYOUT = "Cooling glass...";

			// Token: 0x0400A00A RID: 40970
			public static LocString COMPLETELAYOUT1 = "Cooling glass...";

			// Token: 0x0400A00B RID: 40971
			public static LocString COMPLETELAYOUT2 = "Cooling glass...";

			// Token: 0x0400A00C RID: 40972
			public static LocString COMPLETELAYOUT3 = "Cooling glass...";

			// Token: 0x0400A00D RID: 40973
			public static LocString COMPLETELAYOUT4 = "Digging holes...";

			// Token: 0x0400A00E RID: 40974
			public static LocString COMPLETELAYOUT5 = "Digging holes...";

			// Token: 0x0400A00F RID: 40975
			public static LocString COMPLETELAYOUT6 = "Digging holes...";

			// Token: 0x0400A010 RID: 40976
			public static LocString COMPLETELAYOUT7 = "Adding buckets of dirt...";

			// Token: 0x0400A011 RID: 40977
			public static LocString COMPLETELAYOUT8 = "Adding buckets of dirt...";

			// Token: 0x0400A012 RID: 40978
			public static LocString COMPLETELAYOUT9 = "Adding buckets of dirt...";

			// Token: 0x0400A013 RID: 40979
			public static LocString COMPLETELAYOUT10 = "Adding buckets of dirt...";

			// Token: 0x0400A014 RID: 40980
			public static LocString PROCESSRIVERS = "Pouring rivers...";

			// Token: 0x0400A015 RID: 40981
			public static LocString CONVERTTERRAINCELLSTOEDGES = "Hardening diamonds...";

			// Token: 0x0400A016 RID: 40982
			public static LocString PROCESSING = "Embedding metals...";

			// Token: 0x0400A017 RID: 40983
			public static LocString PROCESSING1 = "Embedding metals...";

			// Token: 0x0400A018 RID: 40984
			public static LocString PROCESSING2 = "Embedding metals...";

			// Token: 0x0400A019 RID: 40985
			public static LocString PROCESSING3 = "Burying precious ore...";

			// Token: 0x0400A01A RID: 40986
			public static LocString PROCESSING4 = "Burying precious ore...";

			// Token: 0x0400A01B RID: 40987
			public static LocString PROCESSING5 = "Burying precious ore...";

			// Token: 0x0400A01C RID: 40988
			public static LocString PROCESSING6 = "Burying precious ore...";

			// Token: 0x0400A01D RID: 40989
			public static LocString PROCESSING7 = "Excavating tunnels...";

			// Token: 0x0400A01E RID: 40990
			public static LocString PROCESSING8 = "Excavating tunnels...";

			// Token: 0x0400A01F RID: 40991
			public static LocString PROCESSING9 = "Excavating tunnels...";

			// Token: 0x0400A020 RID: 40992
			public static LocString BORDERS = "Just adding water...";

			// Token: 0x0400A021 RID: 40993
			public static LocString BORDERS1 = "Just adding water...";

			// Token: 0x0400A022 RID: 40994
			public static LocString BORDERS2 = "Staring at the void...";

			// Token: 0x0400A023 RID: 40995
			public static LocString BORDERS3 = "Staring at the void...";

			// Token: 0x0400A024 RID: 40996
			public static LocString BORDERS4 = "Staring at the void...";

			// Token: 0x0400A025 RID: 40997
			public static LocString BORDERS5 = "Avoiding awkward eye contact with the void...";

			// Token: 0x0400A026 RID: 40998
			public static LocString BORDERS6 = "Avoiding awkward eye contact with the void...";

			// Token: 0x0400A027 RID: 40999
			public static LocString BORDERS7 = "Avoiding awkward eye contact with the void...";

			// Token: 0x0400A028 RID: 41000
			public static LocString BORDERS8 = "Avoiding awkward eye contact with the void...";

			// Token: 0x0400A029 RID: 41001
			public static LocString BORDERS9 = "Avoiding awkward eye contact with the void...";

			// Token: 0x0400A02A RID: 41002
			public static LocString DRAWWORLDBORDER = "Establishing personal boundaries...";

			// Token: 0x0400A02B RID: 41003
			public static LocString PLACINGTEMPLATES = "Generating interest...";

			// Token: 0x0400A02C RID: 41004
			public static LocString SETTLESIM = "Infusing oxygen...";

			// Token: 0x0400A02D RID: 41005
			public static LocString SETTLESIM1 = "Infusing oxygen...";

			// Token: 0x0400A02E RID: 41006
			public static LocString SETTLESIM2 = "Too much oxygen. Removing...";

			// Token: 0x0400A02F RID: 41007
			public static LocString SETTLESIM3 = "Too much oxygen. Removing...";

			// Token: 0x0400A030 RID: 41008
			public static LocString SETTLESIM4 = "Ideal oxygen levels achieved...";

			// Token: 0x0400A031 RID: 41009
			public static LocString SETTLESIM5 = "Ideal oxygen levels achieved...";

			// Token: 0x0400A032 RID: 41010
			public static LocString SETTLESIM6 = "Planting space flora...";

			// Token: 0x0400A033 RID: 41011
			public static LocString SETTLESIM7 = "Planting space flora...";

			// Token: 0x0400A034 RID: 41012
			public static LocString SETTLESIM8 = "Releasing wildlife...";

			// Token: 0x0400A035 RID: 41013
			public static LocString SETTLESIM9 = "Releasing wildlife...";

			// Token: 0x0400A036 RID: 41014
			public static LocString ANALYZINGWORLD = "Shuffling DNA Blueprints...";

			// Token: 0x0400A037 RID: 41015
			public static LocString ANALYZINGWORLDCOMPLETE = "Tidying up for the Duplicants...";

			// Token: 0x0400A038 RID: 41016
			public static LocString PLACINGCREATURES = "Building the suspense...";
		}

		// Token: 0x020023CB RID: 9163
		public class TOOLTIPS
		{
			// Token: 0x0400A039 RID: 41017
			public static LocString MANAGEMENTMENU_JOBS = string.Concat(new string[]
			{
				"Manage my Duplicant Priorities {Hotkey}\n\n",
				UI.PRE_KEYWORD,
				"Duplicant Priorities",
				UI.PST_KEYWORD,
				" are calculated <i>before</i> the ",
				UI.PRE_KEYWORD,
				"Building Priorities",
				UI.PST_KEYWORD,
				" set by the ",
				UI.FormatAsTool("Priority Tool", global::Action.Prioritize)
			});

			// Token: 0x0400A03A RID: 41018
			public static LocString MANAGEMENTMENU_CONSUMABLES = "Manage my Duplicants' diets and medications {Hotkey}";

			// Token: 0x0400A03B RID: 41019
			public static LocString MANAGEMENTMENU_VITALS = "View my Duplicants' vitals {Hotkey}";

			// Token: 0x0400A03C RID: 41020
			public static LocString MANAGEMENTMENU_RESEARCH = "View the Research Tree {Hotkey}";

			// Token: 0x0400A03D RID: 41021
			public static LocString MANAGEMENTMENU_RESEARCH_NO_RESEARCH = "No active research projects";

			// Token: 0x0400A03E RID: 41022
			public static LocString MANAGEMENTMENU_RESEARCH_CARD_NAME = "Currently researching: {0}";

			// Token: 0x0400A03F RID: 41023
			public static LocString MANAGEMENTMENU_RESEARCH_ITEM_LINE = "• {0}";

			// Token: 0x0400A040 RID: 41024
			public static LocString MANAGEMENTMENU_REQUIRES_RESEARCH = string.Concat(new string[]
			{
				"Build a Research Station to unlock this menu\n\nThe ",
				BUILDINGS.PREFABS.RESEARCHCENTER.NAME,
				" can be found in the ",
				UI.FormatAsBuildMenuTab("Stations Tab", global::Action.Plan10),
				" of the Build Menu"
			});

			// Token: 0x0400A041 RID: 41025
			public static LocString MANAGEMENTMENU_DAILYREPORT = "View each cycle's Colony Report {Hotkey}";

			// Token: 0x0400A042 RID: 41026
			public static LocString MANAGEMENTMENU_CODEX = "Browse entries in my Database {Hotkey}";

			// Token: 0x0400A043 RID: 41027
			public static LocString MANAGEMENTMENU_SCHEDULE = "Adjust the colony's time usage {Hotkey}";

			// Token: 0x0400A044 RID: 41028
			public static LocString MANAGEMENTMENU_STARMAP = "Manage astronaut rocket missions {Hotkey}";

			// Token: 0x0400A045 RID: 41029
			public static LocString MANAGEMENTMENU_REQUIRES_TELESCOPE = string.Concat(new string[]
			{
				"Build a Telescope to unlock this menu\n\nThe ",
				BUILDINGS.PREFABS.TELESCOPE.NAME,
				" can be found in the ",
				UI.FormatAsBuildMenuTab("Stations Tab", global::Action.Plan10),
				" of the Build Menu"
			});

			// Token: 0x0400A046 RID: 41030
			public static LocString MANAGEMENTMENU_REQUIRES_TELESCOPE_CLUSTER = string.Concat(new string[]
			{
				"Build a Telescope to unlock this menu\n\nThe ",
				BUILDINGS.PREFABS.TELESCOPE.NAME,
				" can be found in the ",
				UI.FormatAsBuildMenuTab("Rocketry Tab", global::Action.Plan14),
				" of the Build Menu"
			});

			// Token: 0x0400A047 RID: 41031
			public static LocString MANAGEMENTMENU_SKILLS = "Manage Duplicants' Skill assignments {Hotkey}";

			// Token: 0x0400A048 RID: 41032
			public static LocString MANAGEMENTMENU_REQUIRES_SKILL_STATION = string.Concat(new string[]
			{
				"Build a Printing Pod to unlock this menu\n\nThe ",
				BUILDINGS.PREFABS.HEADQUARTERSCOMPLETE.NAME,
				" can be found in the ",
				UI.FormatAsBuildMenuTab("Base Tab", global::Action.Plan1),
				" of the Build Menu"
			});

			// Token: 0x0400A049 RID: 41033
			public static LocString MANAGEMENTMENU_PAUSEMENU = "Open the game menu {Hotkey}";

			// Token: 0x0400A04A RID: 41034
			public static LocString MANAGEMENTMENU_RESOURCES = "Open the resource management screen {Hotkey}";

			// Token: 0x0400A04B RID: 41035
			public static LocString OPEN_CODEX_ENTRY = "View full entry in database";

			// Token: 0x0400A04C RID: 41036
			public static LocString NO_CODEX_ENTRY = "No database entry available";

			// Token: 0x0400A04D RID: 41037
			public static LocString OPEN_RESOURCE_INFO = "{0} of {1} available for the Duplicants on this asteroid to use\n\nClick to open Resources menu";

			// Token: 0x0400A04E RID: 41038
			public static LocString CHANGE_OUTFIT = "Change this Duplicant's outfit";

			// Token: 0x0400A04F RID: 41039
			public static LocString CHANGE_MATERIAL = "Change this building's construction material";

			// Token: 0x0400A050 RID: 41040
			public static LocString METERSCREEN_AVGSTRESS = "Highest Stress: {0}";

			// Token: 0x0400A051 RID: 41041
			public static LocString METERSCREEN_MEALHISTORY = "Calories Available: {0}\n\nDuplicants consume a minimum of {1} calories each per cycle";

			// Token: 0x0400A052 RID: 41042
			public static LocString METERSCREEN_ELECTROBANK_JOULES = "Joules Available: {0}\n\nBionic Duplicants use a minimum of {1} each per cycle\n\nPower Banks Available: {2}\n";

			// Token: 0x0400A053 RID: 41043
			public static LocString METERSCREEN_POPULATION = "Population: {0}";

			// Token: 0x0400A054 RID: 41044
			public static LocString METERSCREEN_POPULATION_CLUSTER = UI.PRE_KEYWORD + "{0}" + UI.PST_KEYWORD + " Population: {1}\nTotal Population: {2}";

			// Token: 0x0400A055 RID: 41045
			public static LocString METERSCREEN_SICK_DUPES = "Sick Duplicants: {0}";

			// Token: 0x0400A056 RID: 41046
			public static LocString METERSCREEN_INVALID_FOOD_TYPE = "Invalid Food Type: {0}";

			// Token: 0x0400A057 RID: 41047
			public static LocString METERSCREEN_INVALID_ELECTROBANK_TYPE = "Invalid Power Bank Type: {0}";

			// Token: 0x0400A058 RID: 41048
			public static LocString PLAYBUTTON = "Start";

			// Token: 0x0400A059 RID: 41049
			public static LocString PAUSEBUTTON = "Pause";

			// Token: 0x0400A05A RID: 41050
			public static LocString PAUSE = "Pause {Hotkey}";

			// Token: 0x0400A05B RID: 41051
			public static LocString UNPAUSE = "Unpause {Hotkey}";

			// Token: 0x0400A05C RID: 41052
			public static LocString SPEEDBUTTON_SLOW = "Slow speed {Hotkey}";

			// Token: 0x0400A05D RID: 41053
			public static LocString SPEEDBUTTON_MEDIUM = "Medium speed {Hotkey}";

			// Token: 0x0400A05E RID: 41054
			public static LocString SPEEDBUTTON_FAST = "Fast speed {Hotkey}";

			// Token: 0x0400A05F RID: 41055
			public static LocString RED_ALERT_TITLE = "Toggle Red Alert";

			// Token: 0x0400A060 RID: 41056
			public static LocString RED_ALERT_CONTENT = "Duplicants will work, ignoring schedules and their basic needs\n\nUse in case of emergency";

			// Token: 0x0400A061 RID: 41057
			public static LocString DISINFECTBUTTON = "Disinfect buildings {Hotkey}";

			// Token: 0x0400A062 RID: 41058
			public static LocString MOPBUTTON = "Mop liquid spills {Hotkey}";

			// Token: 0x0400A063 RID: 41059
			public static LocString DIGBUTTON = "Set dig errands {Hotkey}";

			// Token: 0x0400A064 RID: 41060
			public static LocString CANCELBUTTON = "Cancel errands {Hotkey}";

			// Token: 0x0400A065 RID: 41061
			public static LocString DECONSTRUCTBUTTON = "Demolish buildings {Hotkey}";

			// Token: 0x0400A066 RID: 41062
			public static LocString ATTACKBUTTON = "Attack poor, wild critters {Hotkey}";

			// Token: 0x0400A067 RID: 41063
			public static LocString CAPTUREBUTTON = "Capture critters {Hotkey}";

			// Token: 0x0400A068 RID: 41064
			public static LocString CLEARBUTTON = "Move debris into storage {Hotkey}";

			// Token: 0x0400A069 RID: 41065
			public static LocString HARVESTBUTTON = "Harvest plants {Hotkey}";

			// Token: 0x0400A06A RID: 41066
			public static LocString PRIORITIZEMAINBUTTON = "";

			// Token: 0x0400A06B RID: 41067
			public static LocString PRIORITIZEBUTTON = string.Concat(new string[]
			{
				"Set Building Priority {Hotkey}\n\nDuplicant Priorities",
				UI.PST_KEYWORD,
				" ",
				UI.FormatAsHotKey(global::Action.ManagePriorities),
				" are calculated <i>before</i> the ",
				UI.PRE_KEYWORD,
				"Building Priorities",
				UI.PST_KEYWORD,
				" set by this tool"
			});

			// Token: 0x0400A06C RID: 41068
			public static LocString CLEANUPMAINBUTTON = "Mop and sweep messy floors {Hotkey}";

			// Token: 0x0400A06D RID: 41069
			public static LocString CANCELDECONSTRUCTIONBUTTON = "Cancel queued orders or deconstruct existing buildings {Hotkey}";

			// Token: 0x0400A06E RID: 41070
			public static LocString HELP_ROTATE_KEY = "Press " + UI.FormatAsHotKey(global::Action.RotateBuilding) + " to Rotate";

			// Token: 0x0400A06F RID: 41071
			public static LocString HELP_BUILDLOCATION_INVALID_CELL = "Invalid Cell";

			// Token: 0x0400A070 RID: 41072
			public static LocString HELP_BUILDLOCATION_MISSING_TELEPAD = "World has no " + BUILDINGS.PREFABS.HEADQUARTERSCOMPLETE.NAME + " or " + BUILDINGS.PREFABS.EXOBASEHEADQUARTERS.NAME;

			// Token: 0x0400A071 RID: 41073
			public static LocString HELP_BUILDLOCATION_FLOOR = "Must be built on solid ground";

			// Token: 0x0400A072 RID: 41074
			public static LocString HELP_BUILDLOCATION_WALL = "Must be built against a wall";

			// Token: 0x0400A073 RID: 41075
			public static LocString HELP_BUILDLOCATION_FLOOR_OR_ATTACHPOINT = "Must be built on solid ground or overlapping an {0}";

			// Token: 0x0400A074 RID: 41076
			public static LocString HELP_BUILDLOCATION_OCCUPIED = "Must be built in unoccupied space";

			// Token: 0x0400A075 RID: 41077
			public static LocString HELP_BUILDLOCATION_CEILING = "Must be built on the ceiling";

			// Token: 0x0400A076 RID: 41078
			public static LocString HELP_BUILDLOCATION_INSIDEGROUND = "Must be built in the ground";

			// Token: 0x0400A077 RID: 41079
			public static LocString HELP_BUILDLOCATION_ATTACHPOINT = "Must be built overlapping a {0}";

			// Token: 0x0400A078 RID: 41080
			public static LocString HELP_BUILDLOCATION_SPACE = "Must be built on the surface in space";

			// Token: 0x0400A079 RID: 41081
			public static LocString HELP_BUILDLOCATION_CORNER = "Must be built in a corner";

			// Token: 0x0400A07A RID: 41082
			public static LocString HELP_BUILDLOCATION_CORNER_FLOOR = "Must be built in a corner on the ground";

			// Token: 0x0400A07B RID: 41083
			public static LocString HELP_BUILDLOCATION_BELOWROCKETCEILING = "Must be placed further from the edge of space";

			// Token: 0x0400A07C RID: 41084
			public static LocString HELP_BUILDLOCATION_ONROCKETENVELOPE = "Must be built on the interior wall of a rocket";

			// Token: 0x0400A07D RID: 41085
			public static LocString HELP_BUILDLOCATION_LIQUID_CONDUIT_FORBIDDEN = "Obstructed by a building";

			// Token: 0x0400A07E RID: 41086
			public static LocString HELP_BUILDLOCATION_NOT_IN_TILES = "Cannot be built inside tile";

			// Token: 0x0400A07F RID: 41087
			public static LocString HELP_BUILDLOCATION_GASPORTS_OVERLAP = "Gas ports cannot overlap";

			// Token: 0x0400A080 RID: 41088
			public static LocString HELP_BUILDLOCATION_LIQUIDPORTS_OVERLAP = "Liquid ports cannot overlap";

			// Token: 0x0400A081 RID: 41089
			public static LocString HELP_BUILDLOCATION_SOLIDPORTS_OVERLAP = "Solid ports cannot overlap";

			// Token: 0x0400A082 RID: 41090
			public static LocString HELP_BUILDLOCATION_LOGIC_PORTS_OBSTRUCTED = "Automation ports cannot overlap";

			// Token: 0x0400A083 RID: 41091
			public static LocString HELP_BUILDLOCATION_WIRECONNECTORS_OVERLAP = "Power connectors cannot overlap";

			// Token: 0x0400A084 RID: 41092
			public static LocString HELP_BUILDLOCATION_HIGHWATT_NOT_IN_TILE = "Heavi-Watt connectors cannot be built inside tile";

			// Token: 0x0400A085 RID: 41093
			public static LocString HELP_BUILDLOCATION_WIRE_OBSTRUCTION = "Obstructed by Heavi-Watt Wire";

			// Token: 0x0400A086 RID: 41094
			public static LocString HELP_BUILDLOCATION_BACK_WALL = "Obstructed by back wall";

			// Token: 0x0400A087 RID: 41095
			public static LocString HELP_TUBELOCATION_NO_UTURNS = "Can't U-Turn";

			// Token: 0x0400A088 RID: 41096
			public static LocString HELP_TUBELOCATION_STRAIGHT_BRIDGES = "Can't Turn Here";

			// Token: 0x0400A089 RID: 41097
			public static LocString HELP_REQUIRES_ROOM = "Must be in a " + UI.PRE_KEYWORD + "Room" + UI.PST_KEYWORD;

			// Token: 0x0400A08A RID: 41098
			public static LocString OXYGENOVERLAYSTRING = "Displays ambient oxygen density {Hotkey}";

			// Token: 0x0400A08B RID: 41099
			public static LocString POWEROVERLAYSTRING = "Displays power grid components {Hotkey}";

			// Token: 0x0400A08C RID: 41100
			public static LocString TEMPERATUREOVERLAYSTRING = "Displays ambient temperature {Hotkey}";

			// Token: 0x0400A08D RID: 41101
			public static LocString HEATFLOWOVERLAYSTRING = "Displays comfortable temperatures for Duplicants {Hotkey}";

			// Token: 0x0400A08E RID: 41102
			public static LocString SUITOVERLAYSTRING = "Displays Exosuits and related buildings {Hotkey}";

			// Token: 0x0400A08F RID: 41103
			public static LocString LOGICOVERLAYSTRING = "Displays automation grid components {Hotkey}";

			// Token: 0x0400A090 RID: 41104
			public static LocString ROOMSOVERLAYSTRING = "Displays special purpose rooms and bonuses {Hotkey}";

			// Token: 0x0400A091 RID: 41105
			public static LocString JOULESOVERLAYSTRING = "Displays the thermal energy in each cell";

			// Token: 0x0400A092 RID: 41106
			public static LocString LIGHTSOVERLAYSTRING = "Displays the visibility radius of light sources {Hotkey}";

			// Token: 0x0400A093 RID: 41107
			public static LocString LIQUIDVENTOVERLAYSTRING = "Displays liquid pipe system components {Hotkey}";

			// Token: 0x0400A094 RID: 41108
			public static LocString GASVENTOVERLAYSTRING = "Displays gas pipe system components {Hotkey}";

			// Token: 0x0400A095 RID: 41109
			public static LocString DECOROVERLAYSTRING = "Displays areas with Morale-boosting decor values {Hotkey}";

			// Token: 0x0400A096 RID: 41110
			public static LocString PRIORITIESOVERLAYSTRING = "Displays work priority values {Hotkey}";

			// Token: 0x0400A097 RID: 41111
			public static LocString DISEASEOVERLAYSTRING = "Displays areas of disease risk {Hotkey}";

			// Token: 0x0400A098 RID: 41112
			public static LocString NOISE_POLLUTION_OVERLAY_STRING = "Displays ambient noise levels {Hotkey}";

			// Token: 0x0400A099 RID: 41113
			public static LocString CROPS_OVERLAY_STRING = "Displays plant growth progress {Hotkey}";

			// Token: 0x0400A09A RID: 41114
			public static LocString CONVEYOR_OVERLAY_STRING = "Displays conveyor transport components {Hotkey}";

			// Token: 0x0400A09B RID: 41115
			public static LocString TILEMODE_OVERLAY_STRING = "Displays material information {Hotkey}";

			// Token: 0x0400A09C RID: 41116
			public static LocString REACHABILITYOVERLAYSTRING = "Displays areas accessible by Duplicants";

			// Token: 0x0400A09D RID: 41117
			public static LocString RADIATIONOVERLAYSTRING = "Displays radiation levels {Hotkey}";

			// Token: 0x0400A09E RID: 41118
			public static LocString ENERGYREQUIRED = UI.FormatAsLink("Power", "POWER") + " Required";

			// Token: 0x0400A09F RID: 41119
			public static LocString ENERGYGENERATED = UI.FormatAsLink("Power", "POWER") + " Produced";

			// Token: 0x0400A0A0 RID: 41120
			public static LocString INFOPANEL = "The Info Panel contains an overview of the basic information about my Duplicant";

			// Token: 0x0400A0A1 RID: 41121
			public static LocString VITALSPANEL = "The Vitals Panel monitors the status and well being of my Duplicant";

			// Token: 0x0400A0A2 RID: 41122
			public static LocString STRESSPANEL = "The Stress Panel offers a detailed look at what is affecting my Duplicant psychologically";

			// Token: 0x0400A0A3 RID: 41123
			public static LocString STATSPANEL = "The Stats Panel gives me an overview of my Duplicant's individual stats";

			// Token: 0x0400A0A4 RID: 41124
			public static LocString ITEMSPANEL = "The Items Panel displays everything this Duplicant is in possession of";

			// Token: 0x0400A0A5 RID: 41125
			public static LocString STRESSDESCRIPTION = string.Concat(new string[]
			{
				"Accommodate my Duplicant's needs to manage their ",
				UI.FormatAsLink("Stress", "STRESS"),
				".\n\nLow ",
				UI.FormatAsLink("Stress", "STRESS"),
				" can provide a productivity boost, while high ",
				UI.FormatAsLink("Stress", "STRESS"),
				" can impair production or even lead to a nervous breakdown."
			});

			// Token: 0x0400A0A6 RID: 41126
			public static LocString ALERTSTOOLTIP = "Alerts provide important information about what's happening in the colony right now";

			// Token: 0x0400A0A7 RID: 41127
			public static LocString MESSAGESTOOLTIP = "Messages are events that have happened and tips to help me manage my colony";

			// Token: 0x0400A0A8 RID: 41128
			public static LocString NEXTMESSAGESTOOLTIP = "Next message";

			// Token: 0x0400A0A9 RID: 41129
			public static LocString CLOSETOOLTIP = "Close";

			// Token: 0x0400A0AA RID: 41130
			public static LocString DISMISSMESSAGE = "Dismiss message";

			// Token: 0x0400A0AB RID: 41131
			public static LocString RECIPE_QUEUE = "Queue {0} for continuous fabrication";

			// Token: 0x0400A0AC RID: 41132
			public static LocString RED_ALERT_BUTTON_ON = "Enable Red Alert";

			// Token: 0x0400A0AD RID: 41133
			public static LocString RED_ALERT_BUTTON_OFF = "Disable Red Alert";

			// Token: 0x0400A0AE RID: 41134
			public static LocString JOBSSCREEN_PRIORITY = "High priority tasks are always performed before low priority tasks.\n\nHowever, a busy Duplicant will continue to work on their current work errand until it's complete, even if a more important errand becomes available.";

			// Token: 0x0400A0AF RID: 41135
			public static LocString JOBSSCREEN_ATTRIBUTES = "The following attributes affect a Duplicant's efficiency at this errand:";

			// Token: 0x0400A0B0 RID: 41136
			public static LocString JOBSSCREEN_CANNOTPERFORMTASK = "{0} cannot perform this errand.";

			// Token: 0x0400A0B1 RID: 41137
			public static LocString JOBSSCREEN_RELEVANT_ATTRIBUTES = "Relevant Attributes:";

			// Token: 0x0400A0B2 RID: 41138
			public static LocString SORTCOLUMN = UI.CLICK(UI.ClickType.Click) + " to sort";

			// Token: 0x0400A0B3 RID: 41139
			public static LocString NOMATERIAL = "Not enough materials";

			// Token: 0x0400A0B4 RID: 41140
			public static LocString SELECTAMATERIAL = "There are insufficient materials to construct this building";

			// Token: 0x0400A0B5 RID: 41141
			public static LocString EDITNAME = "Give this Duplicant a new name";

			// Token: 0x0400A0B6 RID: 41142
			public static LocString RANDOMIZENAME = "Randomize this Duplicant's name";

			// Token: 0x0400A0B7 RID: 41143
			public static LocString EDITNAMEGENERIC = "Rename {0}";

			// Token: 0x0400A0B8 RID: 41144
			public static LocString EDITNAMEROCKET = "Rename this rocket";

			// Token: 0x0400A0B9 RID: 41145
			public static LocString BASE_VALUE = "Base Value";

			// Token: 0x0400A0BA RID: 41146
			public static LocString MATIERIAL_MOD = "Made out of {0}";

			// Token: 0x0400A0BB RID: 41147
			public static LocString VITALS_CHECKBOX_TEMPERATURE = string.Concat(new string[]
			{
				"This plant's internal ",
				UI.PRE_KEYWORD,
				"Temperature",
				UI.PST_KEYWORD,
				" is <b>{temperature}</b>"
			});

			// Token: 0x0400A0BC RID: 41148
			public static LocString VITALS_CHECKBOX_PRESSURE = string.Concat(new string[]
			{
				"The current ",
				UI.PRE_KEYWORD,
				"Gas",
				UI.PST_KEYWORD,
				" pressure is <b>{pressure}</b>"
			});

			// Token: 0x0400A0BD RID: 41149
			public static LocString VITALS_CHECKBOX_ATMOSPHERE = "This plant is immersed in {element}";

			// Token: 0x0400A0BE RID: 41150
			public static LocString VITALS_CHECKBOX_ILLUMINATION_DARK = "This plant is currently in the dark";

			// Token: 0x0400A0BF RID: 41151
			public static LocString VITALS_CHECKBOX_ILLUMINATION_LIGHT = "This plant is currently lit";

			// Token: 0x0400A0C0 RID: 41152
			public static LocString VITALS_CHECKBOX_SPACETREE_ILLUMINATION_DARK = "This plant must be lit in order to produce " + UI.PRE_KEYWORD + "Nectar" + UI.PST_KEYWORD;

			// Token: 0x0400A0C1 RID: 41153
			public static LocString VITALS_CHECKBOX_SPACETREE_ILLUMINATION_LIGHT = string.Concat(new string[]
			{
				"This plant is currently lit, and will produce ",
				UI.PRE_KEYWORD,
				"Nectar",
				UI.PST_KEYWORD,
				" when fully grown"
			});

			// Token: 0x0400A0C2 RID: 41154
			public static LocString VITALS_CHECKBOX_FERTILIZER = string.Concat(new string[]
			{
				"<b>{mass}</b> of ",
				UI.PRE_KEYWORD,
				"Fertilizer",
				UI.PST_KEYWORD,
				" is currently available"
			});

			// Token: 0x0400A0C3 RID: 41155
			public static LocString VITALS_CHECKBOX_IRRIGATION = string.Concat(new string[]
			{
				"<b>{mass}</b> of ",
				UI.PRE_KEYWORD,
				"Liquid",
				UI.PST_KEYWORD,
				" is currently available"
			});

			// Token: 0x0400A0C4 RID: 41156
			public static LocString VITALS_CHECKBOX_SUBMERGED_TRUE = "This plant is fully submerged in " + UI.PRE_KEYWORD + "Liquid" + UI.PRE_KEYWORD;

			// Token: 0x0400A0C5 RID: 41157
			public static LocString VITALS_CHECKBOX_SUBMERGED_FALSE = "This plant must be submerged in " + UI.PRE_KEYWORD + "Liquid" + UI.PST_KEYWORD;

			// Token: 0x0400A0C6 RID: 41158
			public static LocString VITALS_CHECKBOX_DROWNING_TRUE = "This plant is not drowning";

			// Token: 0x0400A0C7 RID: 41159
			public static LocString VITALS_CHECKBOX_DROWNING_FALSE = "This plant is drowning in " + UI.PRE_KEYWORD + "Liquid" + UI.PST_KEYWORD;

			// Token: 0x0400A0C8 RID: 41160
			public static LocString VITALS_CHECKBOX_RECEPTACLE_OPERATIONAL = "This plant is housed in an operational farm plot";

			// Token: 0x0400A0C9 RID: 41161
			public static LocString VITALS_CHECKBOX_RECEPTACLE_INOPERATIONAL = "This plant is not housed in an operational farm plot";

			// Token: 0x0400A0CA RID: 41162
			public static LocString VITALS_CHECKBOX_RADIATION = string.Concat(new string[]
			{
				"This plant is sitting in <b>{rads}</b> of ambient ",
				UI.PRE_KEYWORD,
				"Radiation",
				UI.PST_KEYWORD,
				". It needs between {minRads} and {maxRads} to grow"
			});

			// Token: 0x0400A0CB RID: 41163
			public static LocString VITALS_CHECKBOX_RADIATION_NO_MIN = string.Concat(new string[]
			{
				"This plant is sitting in <b>{rads}</b> of ambient ",
				UI.PRE_KEYWORD,
				"Radiation",
				UI.PST_KEYWORD,
				". It needs less than {maxRads} to grow"
			});

			// Token: 0x0400A0CC RID: 41164
			public static LocString VITALS_CHECKBOX_ENTITY_CONSUMER_REQUIREMENTS = string.Concat(new string[]
			{
				"This plant must consume ",
				UI.PRE_KEYWORD,
				"{0}",
				UI.PST_KEYWORD,
				" in order to grow"
			});

			// Token: 0x0400A0CD RID: 41165
			public static LocString VITALS_CHECKBOX_ENTITY_CONSUMER_SATISFIED = UI.PRE_KEYWORD + "{0}" + UI.PST_KEYWORD + " consumed";

			// Token: 0x0400A0CE RID: 41166
			public static LocString VITALS_CHECKBOX_ENTITY_CONSUMER_UNSATISFIED = "Awaiting prey";

			// Token: 0x0400A0CF RID: 41167
			public static LocString VITALS_CHECKBOX_POLLINATED = string.Concat(new string[]
			{
				"This plant was recently pollinated by a ",
				UI.PRE_KEYWORD,
				"Critter",
				UI.PST_KEYWORD,
				" "
			});

			// Token: 0x0400A0D0 RID: 41168
			public static LocString VITALS_CHECKBOX_UNPOLLINATED = string.Concat(new string[]
			{
				"This plant must be pollinated by a ",
				UI.PRE_KEYWORD,
				"Critter",
				UI.PST_KEYWORD,
				"{0}"
			});
		}

		// Token: 0x020023CC RID: 9164
		public class CLUSTERMAP
		{
			// Token: 0x0400A0D1 RID: 41169
			public static LocString PLANETOID = "Planetoid";

			// Token: 0x0400A0D2 RID: 41170
			public static LocString PLANETOID_KEYWORD = UI.PRE_KEYWORD + UI.CLUSTERMAP.PLANETOID + UI.PST_KEYWORD;

			// Token: 0x0400A0D3 RID: 41171
			public static LocString TITLE = "STARMAP";

			// Token: 0x0400A0D4 RID: 41172
			public static LocString LANDING_SITES = "LANDING SITES";

			// Token: 0x0400A0D5 RID: 41173
			public static LocString DESTINATION = "DESTINATION";

			// Token: 0x0400A0D6 RID: 41174
			public static LocString OCCUPANTS = "CREW";

			// Token: 0x0400A0D7 RID: 41175
			public static LocString ELEMENTS = "ELEMENTS";

			// Token: 0x0400A0D8 RID: 41176
			public static LocString UNKNOWN_DESTINATION = "Unknown";

			// Token: 0x0400A0D9 RID: 41177
			public static LocString TILES = "Tiles";

			// Token: 0x0400A0DA RID: 41178
			public static LocString TILES_PER_CYCLE = "Tiles per cycle";

			// Token: 0x0400A0DB RID: 41179
			public static LocString CHANGE_DESTINATION = UI.CLICK(UI.ClickType.Click) + " to change destination";

			// Token: 0x0400A0DC RID: 41180
			public static LocString SELECT_DESTINATION = "Select a new destination on the map";

			// Token: 0x0400A0DD RID: 41181
			public static LocString TOOLTIP_INVALID_DESTINATION_FOG_OF_WAR = "Cannot travel to this hex until it has been analyzed\n\nSpace can be analyzed with a " + BUILDINGS.PREFABS.CLUSTERTELESCOPE.NAME + " or " + BUILDINGS.PREFABS.SCANNERMODULE.NAME;

			// Token: 0x0400A0DE RID: 41182
			public static LocString TOOLTIP_INVALID_DESTINATION_NO_PATH = string.Concat(new string[]
			{
				"There is no navigable rocket path to this ",
				UI.CLUSTERMAP.PLANETOID_KEYWORD,
				"\n\nSpace can be analyzed with a ",
				BUILDINGS.PREFABS.CLUSTERTELESCOPE.NAME,
				" or ",
				BUILDINGS.PREFABS.SCANNERMODULE.NAME,
				" to clear the way"
			});

			// Token: 0x0400A0DF RID: 41183
			public static LocString TOOLTIP_INVALID_DESTINATION_NO_LAUNCH_PAD = string.Concat(new string[]
			{
				"There is no ",
				BUILDINGS.PREFABS.LAUNCHPAD.NAME,
				" on this ",
				UI.CLUSTERMAP.PLANETOID_KEYWORD,
				" for a rocket to land on\n\nUse a ",
				BUILDINGS.PREFABS.PIONEERMODULE.NAME,
				" or ",
				BUILDINGS.PREFABS.SCOUTMODULE.NAME,
				" to deploy a scout and make first contact"
			});

			// Token: 0x0400A0E0 RID: 41184
			public static LocString TOOLTIP_INVALID_DESTINATION_REQUIRE_ASTEROID = "Must select a " + UI.CLUSTERMAP.PLANETOID_KEYWORD + " destination";

			// Token: 0x0400A0E1 RID: 41185
			public static LocString TOOLTIP_INVALID_DESTINATION_OUT_OF_RANGE = "This destination is further away than the rocket's maximum range of {0}";

			// Token: 0x0400A0E2 RID: 41186
			public static LocString TOOLTIP_INVALID_METEOR_TARGET = "This destination does not have an impactor asteroid to target";

			// Token: 0x0400A0E3 RID: 41187
			public static LocString TOOLTIP_HIDDEN_HEX = "???";

			// Token: 0x0400A0E4 RID: 41188
			public static LocString TOOLTIP_PEEKED_HEX_WITH_OBJECT = "UNKNOWN OBJECT DETECTED!";

			// Token: 0x0400A0E5 RID: 41189
			public static LocString TOOLTIP_EMPTY_HEX = "EMPTY SPACE";

			// Token: 0x0400A0E6 RID: 41190
			public static LocString TOOLTIP_PATH_LENGTH = "Trip Distance: {0}/{1}";

			// Token: 0x0400A0E7 RID: 41191
			public static LocString TOOLTIP_PATH_LENGTH_RETURN = "Trip Distance: {0}/{1} (Return Trip)";

			// Token: 0x02002D8A RID: 11658
			public class STATUS
			{
				// Token: 0x0400C4C5 RID: 50373
				public static LocString NORMAL = "Normal";

				// Token: 0x02003A4C RID: 14924
				public class ROCKET
				{
					// Token: 0x0400E806 RID: 59398
					public static LocString GROUNDED = "Normal";

					// Token: 0x0400E807 RID: 59399
					public static LocString TRAVELING = "Traveling";

					// Token: 0x0400E808 RID: 59400
					public static LocString STRANDED = "Stranded";

					// Token: 0x0400E809 RID: 59401
					public static LocString IDLE = "Idle";
				}
			}

			// Token: 0x02002D8B RID: 11659
			public class ASTEROIDS
			{
				// Token: 0x02003A4D RID: 14925
				public class ELEMENT_AMOUNTS
				{
					// Token: 0x0400E80A RID: 59402
					public static LocString LOTS = "Plentiful";

					// Token: 0x0400E80B RID: 59403
					public static LocString SOME = "Significant amount";

					// Token: 0x0400E80C RID: 59404
					public static LocString LITTLE = "Small amount";

					// Token: 0x0400E80D RID: 59405
					public static LocString VERY_LITTLE = "Trace amount";
				}

				// Token: 0x02003A4E RID: 14926
				public class SURFACE_CONDITIONS
				{
					// Token: 0x0400E80E RID: 59406
					public static LocString LIGHT = "Peak Light";

					// Token: 0x0400E80F RID: 59407
					public static LocString RADIATION = "Cosmic Radiation";
				}
			}

			// Token: 0x02002D8C RID: 11660
			public class POI
			{
				// Token: 0x0400C4C6 RID: 50374
				public static LocString TITLE = "POINT OF INTEREST";

				// Token: 0x0400C4C7 RID: 50375
				public static LocString MASS_REMAINING = "<b>Total Mass Remaining</b>";

				// Token: 0x0400C4C8 RID: 50376
				public static LocString ROCKETS_AT_THIS_LOCATION = "<b>Rockets at this location</b>";

				// Token: 0x0400C4C9 RID: 50377
				public static LocString ARTIFACTS = "Artifact";

				// Token: 0x0400C4CA RID: 50378
				public static LocString ARTIFACTS_AVAILABLE = "Available";

				// Token: 0x0400C4CB RID: 50379
				public static LocString ARTIFACTS_DEPLETED = "Collected\nRecharge: {0}";
			}

			// Token: 0x02002D8D RID: 11661
			public class ROCKETS
			{
				// Token: 0x02003A4F RID: 14927
				public class SPEED
				{
					// Token: 0x0400E810 RID: 59408
					public static LocString NAME = "Rocket Speed: ";

					// Token: 0x0400E811 RID: 59409
					public static LocString TOOLTIP = "<b>Rocket speed</b> is calculated by dividing <b>engine power</b> by <b>burden</b>";

					// Token: 0x0400E812 RID: 59410
					public static LocString PILOT_SPEED_MODIFIER = "\n\nRockets operating on autopilot will have a reduced speed\n\n<b>Rocket speed</b> can be increased by a Duplicant pilot's <b>Skill</b> and by a " + UI.PRE_KEYWORD + "Robo-Pilot" + UI.PST_KEYWORD;

					// Token: 0x0400E813 RID: 59411
					public static LocString UNPILOTED_SPEED_TOOLTIP = "Rocket is operating on autopilot: -{speed_boost} speed";

					// Token: 0x0400E814 RID: 59412
					public static LocString SUPERPILOTED_SPEED_TOOLTIP = "Multi-Piloted: +{speed_boost} speed boost";

					// Token: 0x0400E815 RID: 59413
					public static LocString DUPEPILOT_SPEED_TOOLTIP = "Duplicant pilot <b>Skill</b>: +{speed_boost} speed boost";

					// Token: 0x0400E816 RID: 59414
					public static LocString ROBO_PILOT_ONLY_SPEED_TOOLTIP = string.Concat(new string[]
					{
						"Piloted by a ",
						UI.PRE_KEYWORD,
						"Robo-Pilot",
						UI.PST_KEYWORD,
						": +0% speed boost"
					});

					// Token: 0x0400E817 RID: 59415
					public static LocString DEAD_ROBO_PILOT_ONLY_SPEED_TOOLTIP = string.Concat(new string[]
					{
						UI.PRE_KEYWORD,
						"Robo-Pilot",
						UI.PST_KEYWORD,
						" has no ",
						UI.PRE_KEYWORD,
						"Data Banks",
						UI.PST_KEYWORD,
						". This rocket is stranded"
					});
				}

				// Token: 0x02003A50 RID: 14928
				public class FUEL_REMAINING
				{
					// Token: 0x0400E818 RID: 59416
					public static LocString NAME = "Fuel Remaining: ";

					// Token: 0x0400E819 RID: 59417
					public static LocString TOOLTIP = "This rocket has {0} fuel in its tank";
				}

				// Token: 0x02003A51 RID: 14929
				public class OXIDIZER_REMAINING
				{
					// Token: 0x0400E81A RID: 59418
					public static LocString NAME = "Oxidizer Power Remaining: ";

					// Token: 0x0400E81B RID: 59419
					public static LocString TOOLTIP = "This rocket has enough oxidizer in its tank for {0} of fuel";
				}

				// Token: 0x02003A52 RID: 14930
				public class RANGE
				{
					// Token: 0x0400E81C RID: 59420
					public static LocString NAME = "Range Remaining: ";

					// Token: 0x0400E81D RID: 59421
					public static LocString TOOLTIP = "<b>Range remaining</b> is calculated by dividing the lesser of <b>fuel remaining</b> and <b>oxidizer power remaining</b> by <b>fuel consumed per tile</b>";

					// Token: 0x0400E81E RID: 59422
					public static LocString ROBO_PILOTED_TOOLTIP = string.Concat(new string[]
					{
						"\nRockets piloted by a ",
						UI.PRE_KEYWORD,
						"Robo-Pilot",
						UI.PST_KEYWORD,
						" can travel one tile per {0} ",
						UI.PRE_KEYWORD,
						"Data Banks",
						UI.PST_KEYWORD,
						"\n    • ",
						UI.PRE_KEYWORD,
						"Data Banks",
						UI.PST_KEYWORD,
						" Remaining: {1}"
					});
				}

				// Token: 0x02003A53 RID: 14931
				public class FUEL_PER_HEX
				{
					// Token: 0x0400E81F RID: 59423
					public static LocString NAME = "Fuel consumed per Tile: {0}";

					// Token: 0x0400E820 RID: 59424
					public static LocString TOOLTIP = "This rocket can travel one tile per {0} of fuel";
				}

				// Token: 0x02003A54 RID: 14932
				public class BURDEN_TOTAL
				{
					// Token: 0x0400E821 RID: 59425
					public static LocString NAME = "Rocket burden: ";

					// Token: 0x0400E822 RID: 59426
					public static LocString TOOLTIP = "The combined burden of all the modules in this rocket";
				}

				// Token: 0x02003A55 RID: 14933
				public class BURDEN_MODULE
				{
					// Token: 0x0400E823 RID: 59427
					public static LocString NAME = "Module Burden: ";

					// Token: 0x0400E824 RID: 59428
					public static LocString TOOLTIP = "The selected module adds {0} to the rocket's total " + DUPLICANTS.ATTRIBUTES.ROCKETBURDEN.NAME;
				}

				// Token: 0x02003A56 RID: 14934
				public class POWER_TOTAL
				{
					// Token: 0x0400E825 RID: 59429
					public static LocString NAME = "Rocket engine power: ";

					// Token: 0x0400E826 RID: 59430
					public static LocString TOOLTIP = "The total engine power added by all the modules in this rocket";
				}

				// Token: 0x02003A57 RID: 14935
				public class POWER_MODULE
				{
					// Token: 0x0400E827 RID: 59431
					public static LocString NAME = "Module Engine Power: ";

					// Token: 0x0400E828 RID: 59432
					public static LocString TOOLTIP = "The selected module adds {0} to the rocket's total " + DUPLICANTS.ATTRIBUTES.ROCKETENGINEPOWER.NAME;
				}

				// Token: 0x02003A58 RID: 14936
				public class MODULE_STATS
				{
					// Token: 0x0400E829 RID: 59433
					public static LocString NAME = "Module Stats: ";

					// Token: 0x0400E82A RID: 59434
					public static LocString NAME_HEADER = "Module Stats";

					// Token: 0x0400E82B RID: 59435
					public static LocString TOOLTIP = "Properties of the selected module";
				}

				// Token: 0x02003A59 RID: 14937
				public class MAX_MODULES
				{
					// Token: 0x0400E82C RID: 59436
					public static LocString NAME = "Max Modules: ";

					// Token: 0x0400E82D RID: 59437
					public static LocString TOOLTIP = "The {0} can support {1} rocket modules, plus itself";
				}

				// Token: 0x02003A5A RID: 14938
				public class MAX_HEIGHT
				{
					// Token: 0x0400E82E RID: 59438
					public static LocString NAME = "Height: {0}/{1}";

					// Token: 0x0400E82F RID: 59439
					public static LocString NAME_RAW = "Height: ";

					// Token: 0x0400E830 RID: 59440
					public static LocString NAME_MAX_SUPPORTED = "Maximum supported rocket height: ";

					// Token: 0x0400E831 RID: 59441
					public static LocString TOOLTIP = "The {0} can support a total rocket height {1}";
				}

				// Token: 0x02003A5B RID: 14939
				public class ARTIFACT_MODULE
				{
					// Token: 0x0400E832 RID: 59442
					public static LocString EMPTY = "Empty";
				}
			}
		}

		// Token: 0x020023CD RID: 9165
		public class STARMAP
		{
			// Token: 0x0400A0E8 RID: 41192
			public static LocString TITLE = "STARMAP";

			// Token: 0x0400A0E9 RID: 41193
			public static LocString MANAGEMENT_BUTTON = "STARMAP";

			// Token: 0x0400A0EA RID: 41194
			public static LocString SUBROW = "•  {0}";

			// Token: 0x0400A0EB RID: 41195
			public static LocString UNKNOWN_DESTINATION = "Destination Unknown";

			// Token: 0x0400A0EC RID: 41196
			public static LocString ANALYSIS_AMOUNT = "Analysis {0} Complete";

			// Token: 0x0400A0ED RID: 41197
			public static LocString ANALYSIS_COMPLETE = "ANALYSIS COMPLETE";

			// Token: 0x0400A0EE RID: 41198
			public static LocString NO_ANALYZABLE_DESTINATION_SELECTED = "No destination selected";

			// Token: 0x0400A0EF RID: 41199
			public static LocString UNKNOWN_TYPE = "Type Unknown";

			// Token: 0x0400A0F0 RID: 41200
			public static LocString DISTANCE = "{0} km";

			// Token: 0x0400A0F1 RID: 41201
			public static LocString MODULE_MASS = "+ {0} t";

			// Token: 0x0400A0F2 RID: 41202
			public static LocString MODULE_STORAGE = "{0} / {1}";

			// Token: 0x0400A0F3 RID: 41203
			public static LocString ANALYSIS_DESCRIPTION = "Use a Telescope to analyze space destinations.\n\nCompleting analysis on an object will unlock rocket missions to that destination.";

			// Token: 0x0400A0F4 RID: 41204
			public static LocString RESEARCH_DESCRIPTION = "Gather Interstellar Research Data using Research Modules.";

			// Token: 0x0400A0F5 RID: 41205
			public static LocString ROCKET_RENAME_BUTTON_TOOLTIP = "Rename this rocket";

			// Token: 0x0400A0F6 RID: 41206
			public static LocString NO_ROCKETS_HELP_TEXT = "Rockets allow you to visit nearby celestial bodies.\n\nEach rocket must have a Command Module, an Engine, and Fuel.\n\nYou can also carry other modules that allow you to gather specific resources from the places you visit.\n\nRemember the more weight a rocket has, the more limited it'll be on the distance it can travel. You can add more fuel to fix that, but fuel will add weight as well.";

			// Token: 0x0400A0F7 RID: 41207
			public static LocString CONTAINER_REQUIRED = "{0} installation required to retrieve material";

			// Token: 0x0400A0F8 RID: 41208
			public static LocString CAN_CARRY_ELEMENT = "Gathered by: {1}";

			// Token: 0x0400A0F9 RID: 41209
			public static LocString CANT_CARRY_ELEMENT = "{0} installation required to retrieve material";

			// Token: 0x0400A0FA RID: 41210
			public static LocString STATUS = "SELECTED";

			// Token: 0x0400A0FB RID: 41211
			public static LocString DISTANCE_OVERLAY = "TOO FAR FOR THIS ROCKET";

			// Token: 0x0400A0FC RID: 41212
			public static LocString COMPOSITION_UNDISCOVERED = "?????????";

			// Token: 0x0400A0FD RID: 41213
			public static LocString COMPOSITION_UNDISCOVERED_TOOLTIP = "Further research required to identify resource\n\nSend a Research Module to this destination for more information";

			// Token: 0x0400A0FE RID: 41214
			public static LocString COMPOSITION_UNDISCOVERED_AMOUNT = "???";

			// Token: 0x0400A0FF RID: 41215
			public static LocString COMPOSITION_SMALL_AMOUNT = "Trace Amount";

			// Token: 0x0400A100 RID: 41216
			public static LocString CURRENT_MASS = "Current Mass";

			// Token: 0x0400A101 RID: 41217
			public static LocString CURRENT_MASS_TOOLTIP = "Warning: Missions to this destination will not return a full cargo load to avoid depleting the destination for future explorations\n\nDestination: {0} Resources Available\nRocket Capacity: {1}";

			// Token: 0x0400A102 RID: 41218
			public static LocString MAXIMUM_MASS = "Maximum Mass";

			// Token: 0x0400A103 RID: 41219
			public static LocString MINIMUM_MASS = "Minimum Mass";

			// Token: 0x0400A104 RID: 41220
			public static LocString MINIMUM_MASS_TOOLTIP = "This destination must retain at least this much mass in order to prevent depletion and allow the future regeneration of resources.\n\nDuplicants will always maintain a destination's minimum mass requirements, potentially returning with less cargo than their rocket can hold";

			// Token: 0x0400A105 RID: 41221
			public static LocString REPLENISH_RATE = "Replenished/Cycle:";

			// Token: 0x0400A106 RID: 41222
			public static LocString REPLENISH_RATE_TOOLTIP = "The rate at which this destination regenerates resources";

			// Token: 0x0400A107 RID: 41223
			public static LocString ROCKETLIST = "Rocket Hangar";

			// Token: 0x0400A108 RID: 41224
			public static LocString NO_ROCKETS_TITLE = "NO ROCKETS";

			// Token: 0x0400A109 RID: 41225
			public static LocString ROCKET_COUNT = "ROCKETS: {0}";

			// Token: 0x0400A10A RID: 41226
			public static LocString LAUNCH_MISSION = "LAUNCH MISSION";

			// Token: 0x0400A10B RID: 41227
			public static LocString CANT_LAUNCH_MISSION = "CANNOT LAUNCH";

			// Token: 0x0400A10C RID: 41228
			public static LocString LAUNCH_ROCKET = "Launch Rocket";

			// Token: 0x0400A10D RID: 41229
			public static LocString LAND_ROCKET = "Land Rocket";

			// Token: 0x0400A10E RID: 41230
			public static LocString SEE_ROCKETS_LIST = "See Rockets List";

			// Token: 0x0400A10F RID: 41231
			public static LocString DEFAULT_NAME = "Rocket";

			// Token: 0x0400A110 RID: 41232
			public static LocString ANALYZE_DESTINATION = "ANALYZE OBJECT";

			// Token: 0x0400A111 RID: 41233
			public static LocString SUSPEND_DESTINATION_ANALYSIS = "PAUSE ANALYSIS";

			// Token: 0x0400A112 RID: 41234
			public static LocString DESTINATIONTITLE = "Destination Status";

			// Token: 0x02002D8E RID: 11662
			public class DESTINATIONSTUDY
			{
				// Token: 0x0400C4CC RID: 50380
				public static LocString UPPERATMO = "Study upper atmosphere";

				// Token: 0x0400C4CD RID: 50381
				public static LocString LOWERATMO = "Study lower atmosphere";

				// Token: 0x0400C4CE RID: 50382
				public static LocString MAGNETICFIELD = "Study magnetic field";

				// Token: 0x0400C4CF RID: 50383
				public static LocString SURFACE = "Study surface";

				// Token: 0x0400C4D0 RID: 50384
				public static LocString SUBSURFACE = "Study subsurface";
			}

			// Token: 0x02002D8F RID: 11663
			public class COMPONENT
			{
				// Token: 0x0400C4D1 RID: 50385
				public static LocString FUEL_TANK = "Fuel Tank";

				// Token: 0x0400C4D2 RID: 50386
				public static LocString ROCKET_ENGINE = "Rocket Engine";

				// Token: 0x0400C4D3 RID: 50387
				public static LocString CARGO_BAY = "Cargo Bay";

				// Token: 0x0400C4D4 RID: 50388
				public static LocString OXIDIZER_TANK = "Oxidizer Tank";
			}

			// Token: 0x02002D90 RID: 11664
			public class MISSION_STATUS
			{
				// Token: 0x0400C4D5 RID: 50389
				public static LocString GROUNDED = "Grounded";

				// Token: 0x0400C4D6 RID: 50390
				public static LocString LAUNCHING = "Launching";

				// Token: 0x0400C4D7 RID: 50391
				public static LocString WAITING_TO_LAND = "Waiting To Land";

				// Token: 0x0400C4D8 RID: 50392
				public static LocString LANDING = "Landing";

				// Token: 0x0400C4D9 RID: 50393
				public static LocString UNDERWAY = "Underway";

				// Token: 0x0400C4DA RID: 50394
				public static LocString UNDERWAY_BOOSTED = "Underway <color=#5FDB37FF>(Boosted)</color>";

				// Token: 0x0400C4DB RID: 50395
				public static LocString DESTROYED = "Destroyed";

				// Token: 0x0400C4DC RID: 50396
				public static LocString GO = "ALL SYSTEMS GO";
			}

			// Token: 0x02002D91 RID: 11665
			public class LISTTITLES
			{
				// Token: 0x0400C4DD RID: 50397
				public static LocString MISSIONSTATUS = "Mission Status";

				// Token: 0x0400C4DE RID: 50398
				public static LocString LAUNCHCHECKLIST = "Launch Checklist";

				// Token: 0x0400C4DF RID: 50399
				public static LocString MAXRANGE = "Max Range";

				// Token: 0x0400C4E0 RID: 50400
				public static LocString MASS = "Mass";

				// Token: 0x0400C4E1 RID: 50401
				public static LocString STORAGE = "Storage";

				// Token: 0x0400C4E2 RID: 50402
				public static LocString FUEL = "Fuel";

				// Token: 0x0400C4E3 RID: 50403
				public static LocString OXIDIZER = "Oxidizer";

				// Token: 0x0400C4E4 RID: 50404
				public static LocString PASSENGERS = "Passengers";

				// Token: 0x0400C4E5 RID: 50405
				public static LocString RESEARCH = "Research";

				// Token: 0x0400C4E6 RID: 50406
				public static LocString ARTIFACTS = "Artifacts";

				// Token: 0x0400C4E7 RID: 50407
				public static LocString ANALYSIS = "Analysis";

				// Token: 0x0400C4E8 RID: 50408
				public static LocString WORLDCOMPOSITION = "World Composition";

				// Token: 0x0400C4E9 RID: 50409
				public static LocString RESOURCES = "Resources";

				// Token: 0x0400C4EA RID: 50410
				public static LocString MODULES = "Modules";

				// Token: 0x0400C4EB RID: 50411
				public static LocString TYPE = "Type";

				// Token: 0x0400C4EC RID: 50412
				public static LocString DISTANCE = "Distance";

				// Token: 0x0400C4ED RID: 50413
				public static LocString DESTINATION_MASS = "World Mass Available";

				// Token: 0x0400C4EE RID: 50414
				public static LocString STORAGECAPACITY = "Storage Capacity";
			}

			// Token: 0x02002D92 RID: 11666
			public class ROCKETWEIGHT
			{
				// Token: 0x0400C4EF RID: 50415
				public static LocString MASS = "Mass: ";

				// Token: 0x0400C4F0 RID: 50416
				public static LocString MASSPENALTY = "Mass Penalty: ";

				// Token: 0x0400C4F1 RID: 50417
				public static LocString CURRENTMASS = "Current Rocket Mass: ";

				// Token: 0x0400C4F2 RID: 50418
				public static LocString CURRENTMASSPENALTY = "Current Weight Penalty: ";
			}

			// Token: 0x02002D93 RID: 11667
			public class DESTINATIONSELECTION
			{
				// Token: 0x0400C4F3 RID: 50419
				public static LocString REACHABLE = "Destination set";

				// Token: 0x0400C4F4 RID: 50420
				public static LocString UNREACHABLE = "Destination set";

				// Token: 0x0400C4F5 RID: 50421
				public static LocString NOTSELECTED = "Destination set";
			}

			// Token: 0x02002D94 RID: 11668
			public class DESTINATIONSELECTION_TOOLTIP
			{
				// Token: 0x0400C4F6 RID: 50422
				public static LocString REACHABLE = "Viable destination selected, ready for launch";

				// Token: 0x0400C4F7 RID: 50423
				public static LocString UNREACHABLE = "The selected destination is beyond rocket reach";

				// Token: 0x0400C4F8 RID: 50424
				public static LocString NOTSELECTED = "Select the rocket's Command Module to set a destination";
			}

			// Token: 0x02002D95 RID: 11669
			public class HASFOOD
			{
				// Token: 0x0400C4F9 RID: 50425
				public static LocString NAME = "Food Loaded";

				// Token: 0x0400C4FA RID: 50426
				public static LocString TOOLTIP = "Sufficient food stores have been loaded, ready for launch";
			}

			// Token: 0x02002D96 RID: 11670
			public class HASSUIT
			{
				// Token: 0x0400C4FB RID: 50427
				public static LocString NAME = "Has " + EQUIPMENT.PREFABS.ATMO_SUIT.NAME;

				// Token: 0x0400C4FC RID: 50428
				public static LocString TOOLTIP = "An " + EQUIPMENT.PREFABS.ATMO_SUIT.NAME + " has been loaded";
			}

			// Token: 0x02002D97 RID: 11671
			public class NOSUIT
			{
				// Token: 0x0400C4FD RID: 50429
				public static LocString NAME = "Missing " + EQUIPMENT.PREFABS.ATMO_SUIT.NAME;

				// Token: 0x0400C4FE RID: 50430
				public static LocString TOOLTIP = "Rocket cannot launch without an " + EQUIPMENT.PREFABS.ATMO_SUIT.NAME + " loaded";
			}

			// Token: 0x02002D98 RID: 11672
			public class NOFOOD
			{
				// Token: 0x0400C4FF RID: 50431
				public static LocString NAME = "Insufficient Food";

				// Token: 0x0400C500 RID: 50432
				public static LocString TOOLTIP = "Rocket cannot launch without adequate food stores for passengers";
			}

			// Token: 0x02002D99 RID: 11673
			public class CARGOEMPTY
			{
				// Token: 0x0400C501 RID: 50433
				public static LocString NAME = "Emptied Cargo Bay";

				// Token: 0x0400C502 RID: 50434
				public static LocString TOOLTIP = "Cargo Bays must be emptied of all materials before launch";
			}

			// Token: 0x02002D9A RID: 11674
			public class LAUNCHCHECKLIST
			{
				// Token: 0x0400C503 RID: 50435
				public static LocString ASTRONAUT_TITLE = "Astronaut";

				// Token: 0x0400C504 RID: 50436
				public static LocString HASASTRONAUT = "Astronaut ready for liftoff";

				// Token: 0x0400C505 RID: 50437
				public static LocString ASTRONAUGHT = "No Astronaut assigned";

				// Token: 0x0400C506 RID: 50438
				public static LocString INSTALLED = "Installed";

				// Token: 0x0400C507 RID: 50439
				public static LocString INSTALLED_TOOLTIP = "A suitable {0} has been installed";

				// Token: 0x0400C508 RID: 50440
				public static LocString REQUIRED = "Required";

				// Token: 0x0400C509 RID: 50441
				public static LocString REQUIRED_TOOLTIP = "A {0} must be installed before launch";

				// Token: 0x0400C50A RID: 50442
				public static LocString MISSING_TOOLTIP = "No {0} installed\n\nThis rocket cannot launch without a completed {0}";

				// Token: 0x0400C50B RID: 50443
				public static LocString NO_DESTINATION = "No destination selected";

				// Token: 0x0400C50C RID: 50444
				public static LocString MINIMUM_MASS = "Resources available {0}";

				// Token: 0x0400C50D RID: 50445
				public static LocString RESOURCE_MASS_TOOLTIP = "{0} has {1} resources available\nThis rocket has capacity for {2}";

				// Token: 0x0400C50E RID: 50446
				public static LocString INSUFFICENT_MASS_TOOLTIP = "Launching to this destination will not return a full cargo load";

				// Token: 0x02003A5C RID: 14940
				public class CONSTRUCTION_COMPLETE
				{
					// Token: 0x02003ED1 RID: 16081
					public class STATUS
					{
						// Token: 0x0400F2DD RID: 62173
						public static LocString READY = "No active construction";

						// Token: 0x0400F2DE RID: 62174
						public static LocString FAILURE = "No active construction";

						// Token: 0x0400F2DF RID: 62175
						public static LocString WARNING = "No active construction";
					}

					// Token: 0x02003ED2 RID: 16082
					public class TOOLTIP
					{
						// Token: 0x0400F2E0 RID: 62176
						public static LocString READY = "Construction of all modules is complete";

						// Token: 0x0400F2E1 RID: 62177
						public static LocString FAILURE = "In-progress module construction is preventing takeoff";

						// Token: 0x0400F2E2 RID: 62178
						public static LocString WARNING = "Construction warning";
					}
				}

				// Token: 0x02003A5D RID: 14941
				public class PILOT_BOARDED
				{
					// Token: 0x0400E833 RID: 59443
					public static LocString READY = "Pilot boarded";

					// Token: 0x0400E834 RID: 59444
					public static LocString FAILURE = "Pilot boarded";

					// Token: 0x0400E835 RID: 59445
					public static LocString WARNING = "Pilot boarded";

					// Token: 0x0400E836 RID: 59446
					public static LocString ROBO_PILOT_WARNING = "Copilot boarded";

					// Token: 0x02003ED3 RID: 16083
					public class TOOLTIP
					{
						// Token: 0x0400F2E3 RID: 62179
						public static LocString READY = "A Duplicant with the " + DUPLICANTS.ROLES.ROCKETPILOT.NAME + " skill is currently onboard";

						// Token: 0x0400F2E4 RID: 62180
						public static LocString FAILURE = "At least one crew member aboard the rocket must possess the " + DUPLICANTS.ROLES.ROCKETPILOT.NAME + " skill to launch\n\nQualified Duplicants must be assigned to the rocket crew, and have access to the module's hatch";

						// Token: 0x0400F2E5 RID: 62181
						public static LocString WARNING = "Pilot warning";

						// Token: 0x0400F2E6 RID: 62182
						public static LocString ROBO_PILOT_WARNING = string.Concat(new string[]
						{
							"This rocket is being piloted by a ",
							UI.PRE_KEYWORD,
							"Robo-Pilot",
							UI.PST_KEYWORD,
							"\n\nThere are no Duplicants with the ",
							DUPLICANTS.ROLES.ROCKETPILOT.NAME,
							" skill currently onboard\n\nQualified Duplicants must be assigned to the rocket crew, and have access to the module's hatch"
						});
					}
				}

				// Token: 0x02003A5E RID: 14942
				public class CREW_BOARDED
				{
					// Token: 0x0400E837 RID: 59447
					public static LocString READY = "All crew boarded";

					// Token: 0x0400E838 RID: 59448
					public static LocString FAILURE = "All crew boarded";

					// Token: 0x0400E839 RID: 59449
					public static LocString WARNING = "All crew boarded";

					// Token: 0x02003ED4 RID: 16084
					public class TOOLTIP
					{
						// Token: 0x0400F2E7 RID: 62183
						public static LocString READY = "All Duplicants assigned to the rocket crew are boarded and ready for launch\n\n    • {0}/{1} Boarded";

						// Token: 0x0400F2E8 RID: 62184
						public static LocString FAILURE = "No crew members have boarded this rocket\n\nDuplicants must be assigned to the rocket crew and have access to the module's hatch to board\n\n    • {0}/{1} Boarded";

						// Token: 0x0400F2E9 RID: 62185
						public static LocString WARNING = "Some Duplicants assigned to this rocket crew have not yet boarded\n    • {0}/{1} Boarded";

						// Token: 0x0400F2EA RID: 62186
						public static LocString NONE = "There are no Duplicants assigned to this rocket crew\n    • {0}/{1} Boarded";
					}
				}

				// Token: 0x02003A5F RID: 14943
				public class NO_EXTRA_PASSENGERS
				{
					// Token: 0x0400E83A RID: 59450
					public static LocString READY = "Non-crew exited";

					// Token: 0x0400E83B RID: 59451
					public static LocString FAILURE = "Non-crew exited";

					// Token: 0x0400E83C RID: 59452
					public static LocString WARNING = "Non-crew exited";

					// Token: 0x02003ED5 RID: 16085
					public class TOOLTIP
					{
						// Token: 0x0400F2EB RID: 62187
						public static LocString READY = "All non-crew Duplicants have disembarked";

						// Token: 0x0400F2EC RID: 62188
						public static LocString FAILURE = "Non-crew Duplicants must exit the rocket before launch";

						// Token: 0x0400F2ED RID: 62189
						public static LocString WARNING = "Non-crew warning";
					}
				}

				// Token: 0x02003A60 RID: 14944
				public class FLIGHT_PATH_CLEAR
				{
					// Token: 0x02003ED6 RID: 16086
					public class STATUS
					{
						// Token: 0x0400F2EE RID: 62190
						public static LocString READY = "Clear launch path";

						// Token: 0x0400F2EF RID: 62191
						public static LocString FAILURE = "Clear launch path";

						// Token: 0x0400F2F0 RID: 62192
						public static LocString WARNING = "Clear launch path";
					}

					// Token: 0x02003ED7 RID: 16087
					public class TOOLTIP
					{
						// Token: 0x0400F2F1 RID: 62193
						public static LocString READY = "The rocket's launch path is clear for takeoff";

						// Token: 0x0400F2F2 RID: 62194
						public static LocString FAILURE = "This rocket does not have a clear line of sight to space, preventing launch\n\nThe rocket's launch path can be cleared by excavating undug tiles and deconstructing any buildings above the rocket";

						// Token: 0x0400F2F3 RID: 62195
						public static LocString WARNING = "";
					}
				}

				// Token: 0x02003A61 RID: 14945
				public class HAS_FUEL_TANK
				{
					// Token: 0x02003ED8 RID: 16088
					public class STATUS
					{
						// Token: 0x0400F2F4 RID: 62196
						public static LocString READY = "Fuel Tank";

						// Token: 0x0400F2F5 RID: 62197
						public static LocString FAILURE = "Fuel Tank";

						// Token: 0x0400F2F6 RID: 62198
						public static LocString WARNING = "Fuel Tank";
					}

					// Token: 0x02003ED9 RID: 16089
					public class TOOLTIP
					{
						// Token: 0x0400F2F7 RID: 62199
						public static LocString READY = "A fuel tank has been installed";

						// Token: 0x0400F2F8 RID: 62200
						public static LocString FAILURE = "No fuel tank installed\n\nThis rocket cannot launch without a completed fuel tank";

						// Token: 0x0400F2F9 RID: 62201
						public static LocString WARNING = "Fuel tank warning";
					}
				}

				// Token: 0x02003A62 RID: 14946
				public class HAS_ENGINE
				{
					// Token: 0x02003EDA RID: 16090
					public class STATUS
					{
						// Token: 0x0400F2FA RID: 62202
						public static LocString READY = "Engine";

						// Token: 0x0400F2FB RID: 62203
						public static LocString FAILURE = "Engine";

						// Token: 0x0400F2FC RID: 62204
						public static LocString WARNING = "Engine";
					}

					// Token: 0x02003EDB RID: 16091
					public class TOOLTIP
					{
						// Token: 0x0400F2FD RID: 62205
						public static LocString READY = "A suitable engine has been installed";

						// Token: 0x0400F2FE RID: 62206
						public static LocString FAILURE = "No engine installed\n\nThis rocket cannot launch without a completed engine";

						// Token: 0x0400F2FF RID: 62207
						public static LocString WARNING = "Engine warning";
					}
				}

				// Token: 0x02003A63 RID: 14947
				public class HAS_NOSECONE
				{
					// Token: 0x02003EDC RID: 16092
					public class STATUS
					{
						// Token: 0x0400F300 RID: 62208
						public static LocString READY = "Nosecone";

						// Token: 0x0400F301 RID: 62209
						public static LocString FAILURE = "Nosecone";

						// Token: 0x0400F302 RID: 62210
						public static LocString WARNING = "Nosecone";
					}

					// Token: 0x02003EDD RID: 16093
					public class TOOLTIP
					{
						// Token: 0x0400F303 RID: 62211
						public static LocString READY = "A suitable nosecone has been installed";

						// Token: 0x0400F304 RID: 62212
						public static LocString FAILURE = "No nosecone installed\n\nThis rocket cannot launch without a completed nosecone";

						// Token: 0x0400F305 RID: 62213
						public static LocString WARNING = "Nosecone warning";
					}
				}

				// Token: 0x02003A64 RID: 14948
				public class HAS_CARGO_BAY_FOR_NOSECONE_HARVEST
				{
					// Token: 0x02003EDE RID: 16094
					public class STATUS
					{
						// Token: 0x0400F306 RID: 62214
						public static LocString READY = "Drillcone Cargo Bay";

						// Token: 0x0400F307 RID: 62215
						public static LocString FAILURE = "Drillcone Cargo Bay";

						// Token: 0x0400F308 RID: 62216
						public static LocString WARNING = "Drillcone Cargo Bay";
					}

					// Token: 0x02003EDF RID: 16095
					public class TOOLTIP
					{
						// Token: 0x0400F309 RID: 62217
						public static LocString READY = "A suitable cargo bay has been installed";

						// Token: 0x0400F30A RID: 62218
						public static LocString FAILURE = "No cargo bay installed\n\nThis rocket has a " + UI.FormatAsLink("Drillcone", "NOSECONEHARVEST") + " installed but nowhere to store the materials";

						// Token: 0x0400F30B RID: 62219
						public static LocString WARNING = "No cargo bay installed\n\nThis rocket has a " + UI.FormatAsLink("Drillcone", "NOSECONEHARVEST") + " installed but nowhere to store the materials";
					}
				}

				// Token: 0x02003A65 RID: 14949
				public class HAS_CONTROLSTATION
				{
					// Token: 0x02003EE0 RID: 16096
					public class STATUS
					{
						// Token: 0x0400F30C RID: 62220
						public static LocString READY = "Control Station";

						// Token: 0x0400F30D RID: 62221
						public static LocString FAILURE = "Control Station";

						// Token: 0x0400F30E RID: 62222
						public static LocString WARNING = "Control Station";
					}

					// Token: 0x02003EE1 RID: 16097
					public class TOOLTIP
					{
						// Token: 0x0400F30F RID: 62223
						public static LocString READY = "The control station is installed and waiting for the pilot";

						// Token: 0x0400F310 RID: 62224
						public static LocString FAILURE = "No control station\n\nA new Rocket Control Station must be installed inside the rocket";

						// Token: 0x0400F311 RID: 62225
						public static LocString WARNING = "Control Station warning";

						// Token: 0x0400F312 RID: 62226
						public static LocString WARNING_ROBO_PILOT = "No control station\n\nThis rocket is being piloted by a Robo-Pilot Module";
					}
				}

				// Token: 0x02003A66 RID: 14950
				public class LOADING_COMPLETE
				{
					// Token: 0x02003EE2 RID: 16098
					public class STATUS
					{
						// Token: 0x0400F313 RID: 62227
						public static LocString READY = "Cargo Loading Complete";

						// Token: 0x0400F314 RID: 62228
						public static LocString FAILURE = "";

						// Token: 0x0400F315 RID: 62229
						public static LocString WARNING = "Cargo Loading Complete";
					}

					// Token: 0x02003EE3 RID: 16099
					public class TOOLTIP
					{
						// Token: 0x0400F316 RID: 62230
						public static LocString READY = "All possible loading and unloading has been completed";

						// Token: 0x0400F317 RID: 62231
						public static LocString FAILURE = "";

						// Token: 0x0400F318 RID: 62232
						public static LocString WARNING = "The " + BUILDINGS.PREFABS.LAUNCHPAD.NAME + " could still transfer cargo to or from this rocket";
					}
				}

				// Token: 0x02003A67 RID: 14951
				public class CARGO_TRANSFER_COMPLETE
				{
					// Token: 0x02003EE4 RID: 16100
					public class STATUS
					{
						// Token: 0x0400F319 RID: 62233
						public static LocString READY = "Cargo Transfer Complete";

						// Token: 0x0400F31A RID: 62234
						public static LocString FAILURE = "";

						// Token: 0x0400F31B RID: 62235
						public static LocString WARNING = "Cargo Transfer Complete";
					}

					// Token: 0x02003EE5 RID: 16101
					public class TOOLTIP
					{
						// Token: 0x0400F31C RID: 62236
						public static LocString READY = "All possible loading and unloading has been completed";

						// Token: 0x0400F31D RID: 62237
						public static LocString FAILURE = "";

						// Token: 0x0400F31E RID: 62238
						public static LocString WARNING = "The " + BUILDINGS.PREFABS.LAUNCHPAD.NAME + " could still transfer cargo to or from this rocket";
					}
				}

				// Token: 0x02003A68 RID: 14952
				public class INTERNAL_CONSTRUCTION_COMPLETE
				{
					// Token: 0x02003EE6 RID: 16102
					public class STATUS
					{
						// Token: 0x0400F31F RID: 62239
						public static LocString READY = "Landers Ready";

						// Token: 0x0400F320 RID: 62240
						public static LocString FAILURE = "Landers Ready";

						// Token: 0x0400F321 RID: 62241
						public static LocString WARNING = "";
					}

					// Token: 0x02003EE7 RID: 16103
					public class TOOLTIP
					{
						// Token: 0x0400F322 RID: 62242
						public static LocString READY = "All requested landers have been built and are ready for deployment";

						// Token: 0x0400F323 RID: 62243
						public static LocString FAILURE = "Additional landers must be constructed to fulfill the lander requests of this rocket";

						// Token: 0x0400F324 RID: 62244
						public static LocString WARNING = "";
					}
				}

				// Token: 0x02003A69 RID: 14953
				public class MAX_MODULES
				{
					// Token: 0x02003EE8 RID: 16104
					public class STATUS
					{
						// Token: 0x0400F325 RID: 62245
						public static LocString READY = "Module limit";

						// Token: 0x0400F326 RID: 62246
						public static LocString FAILURE = "Module limit";

						// Token: 0x0400F327 RID: 62247
						public static LocString WARNING = "Module limit";
					}

					// Token: 0x02003EE9 RID: 16105
					public class TOOLTIP
					{
						// Token: 0x0400F328 RID: 62248
						public static LocString READY = "The rocket's engine can support the number of installed rocket modules";

						// Token: 0x0400F329 RID: 62249
						public static LocString FAILURE = "The number of installed modules exceeds the engine's module limit\n\nExcess modules must be removed";

						// Token: 0x0400F32A RID: 62250
						public static LocString WARNING = "Module limit warning";
					}
				}

				// Token: 0x02003A6A RID: 14954
				public class HAS_RESOURCE
				{
					// Token: 0x02003EEA RID: 16106
					public class STATUS
					{
						// Token: 0x0400F32B RID: 62251
						public static LocString READY = "{0} {1} supplied";

						// Token: 0x0400F32C RID: 62252
						public static LocString FAILURE = "{0} missing {1}";

						// Token: 0x0400F32D RID: 62253
						public static LocString WARNING = "{0} missing {1}";
					}

					// Token: 0x02003EEB RID: 16107
					public class TOOLTIP
					{
						// Token: 0x0400F32E RID: 62254
						public static LocString READY = "{0} {1} supplied";

						// Token: 0x0400F32F RID: 62255
						public static LocString FAILURE = "{0} has less than {1} {2}";

						// Token: 0x0400F330 RID: 62256
						public static LocString WARNING = "{0} has less than {1} {2}";
					}
				}

				// Token: 0x02003A6B RID: 14955
				public class MAX_HEIGHT
				{
					// Token: 0x02003EEC RID: 16108
					public class STATUS
					{
						// Token: 0x0400F331 RID: 62257
						public static LocString READY = "Height limit";

						// Token: 0x0400F332 RID: 62258
						public static LocString FAILURE = "Height limit";

						// Token: 0x0400F333 RID: 62259
						public static LocString WARNING = "Height limit";
					}

					// Token: 0x02003EED RID: 16109
					public class TOOLTIP
					{
						// Token: 0x0400F334 RID: 62260
						public static LocString READY = "The rocket's engine can support the height of the rocket";

						// Token: 0x0400F335 RID: 62261
						public static LocString FAILURE = "The height of the rocket exceeds the engine's limit\n\nExcess modules must be removed";

						// Token: 0x0400F336 RID: 62262
						public static LocString WARNING = "Height limit warning";
					}
				}

				// Token: 0x02003A6C RID: 14956
				public class PROPERLY_FUELED
				{
					// Token: 0x02003EEE RID: 16110
					public class STATUS
					{
						// Token: 0x0400F337 RID: 62263
						public static LocString READY = "Fueled";

						// Token: 0x0400F338 RID: 62264
						public static LocString FAILURE = "Fueled";

						// Token: 0x0400F339 RID: 62265
						public static LocString WARNING = "Fueled";
					}

					// Token: 0x02003EEF RID: 16111
					public class TOOLTIP
					{
						// Token: 0x0400F33A RID: 62266
						public static LocString READY = "The rocket is sufficiently fueled for a roundtrip to its destination and back";

						// Token: 0x0400F33B RID: 62267
						public static LocString READY_NO_DESTINATION = "This rocket's fuel tanks have been filled to capacity, but it has no destination";

						// Token: 0x0400F33C RID: 62268
						public static LocString FAILURE = "This rocket does not have enough fuel to reach its destination\n\nIf the tanks are full, a different Fuel Tank Module may be required";

						// Token: 0x0400F33D RID: 62269
						public static LocString WARNING = "The rocket has enough fuel for a one-way trip to its destination, but will not be able to make it back";
					}
				}

				// Token: 0x02003A6D RID: 14957
				public class SUFFICIENT_OXIDIZER
				{
					// Token: 0x02003EF0 RID: 16112
					public class STATUS
					{
						// Token: 0x0400F33E RID: 62270
						public static LocString READY = "Sufficient Oxidizer";

						// Token: 0x0400F33F RID: 62271
						public static LocString FAILURE = "Sufficient Oxidizer";

						// Token: 0x0400F340 RID: 62272
						public static LocString WARNING = "Warning: Limited oxidizer";
					}

					// Token: 0x02003EF1 RID: 16113
					public class TOOLTIP
					{
						// Token: 0x0400F341 RID: 62273
						public static LocString READY = "This rocket has sufficient oxidizer for a roundtrip to its destination and back";

						// Token: 0x0400F342 RID: 62274
						public static LocString FAILURE = "This rocket does not have enough oxidizer to reach its destination\n\nIf the oxidizer tanks are full, a different Oxidizer Tank Module may be required";

						// Token: 0x0400F343 RID: 62275
						public static LocString WARNING = "The rocket has enough oxidizer for a one-way trip to its destination, but will not be able to make it back";
					}
				}

				// Token: 0x02003A6E RID: 14958
				public class ON_LAUNCHPAD
				{
					// Token: 0x02003EF2 RID: 16114
					public class STATUS
					{
						// Token: 0x0400F344 RID: 62276
						public static LocString READY = "On a launch pad";

						// Token: 0x0400F345 RID: 62277
						public static LocString FAILURE = "Not on a launch pad";

						// Token: 0x0400F346 RID: 62278
						public static LocString WARNING = "No launch pad";
					}

					// Token: 0x02003EF3 RID: 16115
					public class TOOLTIP
					{
						// Token: 0x0400F347 RID: 62279
						public static LocString READY = "On a launch pad";

						// Token: 0x0400F348 RID: 62280
						public static LocString FAILURE = "Not on a launch pad";

						// Token: 0x0400F349 RID: 62281
						public static LocString WARNING = "No launch pad";
					}
				}

				// Token: 0x02003A6F RID: 14959
				public class ROBOT_PILOT_DATA_REQUIREMENTS
				{
					// Token: 0x02003EF4 RID: 16116
					public class STATUS
					{
						// Token: 0x0400F34A RID: 62282
						public static LocString WARNING_NO_DATA_BANKS_HUMAN_PILOT = "Robo-Pilot programmed";

						// Token: 0x0400F34B RID: 62283
						public static LocString READY = "Robo-Pilot programmed";

						// Token: 0x0400F34C RID: 62284
						public static LocString FAILURE = "Robo-Pilot programmed";

						// Token: 0x0400F34D RID: 62285
						public static LocString WARNING = "Robo-Pilot programmed";
					}

					// Token: 0x02003EF5 RID: 16117
					public class TOOLTIP
					{
						// Token: 0x0400F34E RID: 62286
						public static LocString READY = string.Concat(new string[]
						{
							UI.PRE_KEYWORD,
							"Robo-Pilot",
							UI.PST_KEYWORD,
							" has sufficient ",
							UI.PRE_KEYWORD,
							"Data Banks",
							UI.PST_KEYWORD,
							" for a roundtrip to its destination and back\n    • ",
							UI.PRE_KEYWORD,
							"Data Banks",
							UI.PST_KEYWORD,
							" stored: {0}/{1}"
						});

						// Token: 0x0400F34F RID: 62287
						public static LocString READY_NO_DESTINATION = string.Concat(new string[]
						{
							UI.PRE_KEYWORD,
							"Robo-Pilot",
							UI.PST_KEYWORD,
							" has sufficient ",
							UI.PRE_KEYWORD,
							"Data Banks",
							UI.PST_KEYWORD,
							", but no destination has been set\n    • ",
							UI.PRE_KEYWORD,
							"Data Banks",
							UI.PST_KEYWORD,
							" stored: {0}"
						});

						// Token: 0x0400F350 RID: 62288
						public static LocString FAILURE_NO_DESTINATION = "No destination has been set";

						// Token: 0x0400F351 RID: 62289
						public static LocString FAILURE = string.Concat(new string[]
						{
							UI.PRE_KEYWORD,
							"Robo-Pilot",
							UI.PST_KEYWORD,
							" requires at least {0} ",
							UI.PRE_KEYWORD,
							"Data Banks",
							UI.PST_KEYWORD,
							" to reach its destination\n    • ",
							UI.PRE_KEYWORD,
							"Data Banks",
							UI.PST_KEYWORD,
							" stored: {1}"
						});

						// Token: 0x0400F352 RID: 62290
						public static LocString WARNING = string.Concat(new string[]
						{
							UI.PRE_KEYWORD,
							"Robo-Pilot",
							UI.PST_KEYWORD,
							" has insufficient ",
							UI.PRE_KEYWORD,
							"Data Banks",
							UI.PST_KEYWORD,
							" for a roundtrip to its destination and back\n    • ",
							UI.PRE_KEYWORD,
							"Data Banks",
							UI.PST_KEYWORD,
							"stored: {0}/{1}"
						});

						// Token: 0x0400F353 RID: 62291
						public static LocString WARNING_NO_DATA_BANKS_HUMAN_PILOT = string.Concat(new string[]
						{
							UI.PRE_KEYWORD,
							"Robo-Pilot",
							UI.PST_KEYWORD,
							" cannot function without ",
							UI.PRE_KEYWORD,
							"Data Banks",
							UI.PST_KEYWORD,
							"\n\nThis rocket is currently being operated by a Duplicant who possesses the ",
							DUPLICANTS.ROLES.ROCKETPILOT.NAME,
							" skill"
						});
					}
				}

				// Token: 0x02003A70 RID: 14960
				public class ROBOT_PILOT_POWER_SOUCRE
				{
					// Token: 0x02003EF6 RID: 16118
					public class STATUS
					{
						// Token: 0x0400F354 RID: 62292
						public static LocString READY = "Robo-Pilot has power";

						// Token: 0x0400F355 RID: 62293
						public static LocString WARNING = "Robo-Pilot has power";

						// Token: 0x0400F356 RID: 62294
						public static LocString FAILURE = "Robo-Pilot has power";
					}

					// Token: 0x02003EF7 RID: 16119
					public class TOOLTIP
					{
						// Token: 0x0400F357 RID: 62295
						public static LocString READY = string.Concat(new string[]
						{
							UI.PRE_KEYWORD,
							"Robo-Pilot",
							UI.PST_KEYWORD,
							" has a ",
							UI.PRE_KEYWORD,
							"Power",
							UI.PST_KEYWORD,
							" source"
						});

						// Token: 0x0400F358 RID: 62296
						public static LocString WARNING = string.Concat(new string[]
						{
							UI.PRE_KEYWORD,
							"Robo-Pilot",
							UI.PST_KEYWORD,
							" has insufficient  ",
							UI.PRE_KEYWORD,
							"Power",
							UI.PST_KEYWORD,
							" for a round-trip to its destination"
						});

						// Token: 0x0400F359 RID: 62297
						public static LocString FAILURE = string.Concat(new string[]
						{
							UI.PRE_KEYWORD,
							"Robo-Pilot",
							UI.PST_KEYWORD,
							" requires a ",
							UI.PRE_KEYWORD,
							"Power",
							UI.PST_KEYWORD,
							" source for launch"
						});
					}
				}
			}

			// Token: 0x02002D9B RID: 11675
			public class FULLTANK
			{
				// Token: 0x0400C50F RID: 50447
				public static LocString NAME = "Fuel Tank full";

				// Token: 0x0400C510 RID: 50448
				public static LocString TOOLTIP = "Tank is full, ready for launch";
			}

			// Token: 0x02002D9C RID: 11676
			public class EMPTYTANK
			{
				// Token: 0x0400C511 RID: 50449
				public static LocString NAME = "Fuel Tank not full";

				// Token: 0x0400C512 RID: 50450
				public static LocString TOOLTIP = "Fuel tank must be filled before launch";
			}

			// Token: 0x02002D9D RID: 11677
			public class FULLOXIDIZERTANK
			{
				// Token: 0x0400C513 RID: 50451
				public static LocString NAME = "Oxidizer Tank full";

				// Token: 0x0400C514 RID: 50452
				public static LocString TOOLTIP = "Tank is full, ready for launch";
			}

			// Token: 0x02002D9E RID: 11678
			public class EMPTYOXIDIZERTANK
			{
				// Token: 0x0400C515 RID: 50453
				public static LocString NAME = "Oxidizer Tank not full";

				// Token: 0x0400C516 RID: 50454
				public static LocString TOOLTIP = "Oxidizer tank must be filled before launch";
			}

			// Token: 0x02002D9F RID: 11679
			public class ROCKETSTATUS
			{
				// Token: 0x0400C517 RID: 50455
				public static LocString STATUS_TITLE = "Rocket Status";

				// Token: 0x0400C518 RID: 50456
				public static LocString NONE = "NONE";

				// Token: 0x0400C519 RID: 50457
				public static LocString SELECTED = "SELECTED";

				// Token: 0x0400C51A RID: 50458
				public static LocString LOCKEDIN = "LOCKED IN";

				// Token: 0x0400C51B RID: 50459
				public static LocString NODESTINATION = "No destination selected";

				// Token: 0x0400C51C RID: 50460
				public static LocString DESTINATIONVALUE = "None";

				// Token: 0x0400C51D RID: 50461
				public static LocString NOPASSENGERS = "No passengers";

				// Token: 0x0400C51E RID: 50462
				public static LocString STATUS = "Status";

				// Token: 0x0400C51F RID: 50463
				public static LocString TOTAL = "Total";

				// Token: 0x0400C520 RID: 50464
				public static LocString WEIGHTPENALTY = "Weight Penalty";

				// Token: 0x0400C521 RID: 50465
				public static LocString TIMEREMAINING = "Time Remaining";

				// Token: 0x0400C522 RID: 50466
				public static LocString BOOSTED_TIME_MODIFIER = "Less Than ";
			}

			// Token: 0x02002DA0 RID: 11680
			public class ROCKETSTATS
			{
				// Token: 0x0400C523 RID: 50467
				public static LocString TOTAL_OXIDIZABLE_FUEL = "Total oxidizable fuel";

				// Token: 0x0400C524 RID: 50468
				public static LocString TOTAL_OXIDIZER = "Total oxidizer";

				// Token: 0x0400C525 RID: 50469
				public static LocString TOTAL_FUEL = "Total fuel";

				// Token: 0x0400C526 RID: 50470
				public static LocString NO_ENGINE = "NO ENGINE";

				// Token: 0x0400C527 RID: 50471
				public static LocString ENGINE_EFFICIENCY = "Main engine efficiency";

				// Token: 0x0400C528 RID: 50472
				public static LocString OXIDIZER_EFFICIENCY = "Average oxidizer efficiency";

				// Token: 0x0400C529 RID: 50473
				public static LocString SOLID_BOOSTER = "Solid boosters";

				// Token: 0x0400C52A RID: 50474
				public static LocString ROBO_PILOT_RANGE = "Robo-Pilot Range";

				// Token: 0x0400C52B RID: 50475
				public static LocString ROBO_PILOT_EFFICIENCY = "Robo-Pilot can travel {0} per " + UI.PRE_KEYWORD + "Data Bank" + UI.PST_KEYWORD;

				// Token: 0x0400C52C RID: 50476
				public static LocString TOTAL_THRUST = "Total thrust";

				// Token: 0x0400C52D RID: 50477
				public static LocString TOTAL_RANGE = "Total range";

				// Token: 0x0400C52E RID: 50478
				public static LocString DRY_MASS = "Dry mass";

				// Token: 0x0400C52F RID: 50479
				public static LocString WET_MASS = "Wet mass";
			}

			// Token: 0x02002DA1 RID: 11681
			public class STORAGESTATS
			{
				// Token: 0x0400C530 RID: 50480
				public static LocString STORAGECAPACITY = "{0} / {1}";
			}
		}

		// Token: 0x020023CE RID: 9166
		public class RESEARCHSCREEN
		{
			// Token: 0x0400A113 RID: 41235
			public static LocString SEARCH_RESULTS_CATEGORY = "Search Results";

			// Token: 0x02002DA2 RID: 11682
			public class FILTER_BUTTONS
			{
				// Token: 0x0400C531 RID: 50481
				public static LocString HEADER = "Preset Filters";

				// Token: 0x0400C532 RID: 50482
				public static LocString ALL = "All";

				// Token: 0x0400C533 RID: 50483
				public static LocString AVAILABLE = "Next";

				// Token: 0x0400C534 RID: 50484
				public static LocString COMPLETED = "Completed";

				// Token: 0x0400C535 RID: 50485
				public static LocString OXYGEN = "Oxygen";

				// Token: 0x0400C536 RID: 50486
				public static LocString FOOD = "Food";

				// Token: 0x0400C537 RID: 50487
				public static LocString WATER = "Water";

				// Token: 0x0400C538 RID: 50488
				public static LocString POWER = "Power";

				// Token: 0x0400C539 RID: 50489
				public static LocString MORALE = "Morale";

				// Token: 0x0400C53A RID: 50490
				public static LocString RANCHING = "Ranching";

				// Token: 0x0400C53B RID: 50491
				public static LocString FILTER = "Filter";

				// Token: 0x0400C53C RID: 50492
				public static LocString TILE = "Tile";

				// Token: 0x0400C53D RID: 50493
				public static LocString TRANSPORT = "Transport";

				// Token: 0x0400C53E RID: 50494
				public static LocString AUTOMATION = "Automation";

				// Token: 0x0400C53F RID: 50495
				public static LocString MEDICINE = "Medicine";

				// Token: 0x0400C540 RID: 50496
				public static LocString ROCKET = "Rocket";

				// Token: 0x0400C541 RID: 50497
				public static LocString RADIATION = "Radiation";
			}
		}

		// Token: 0x020023CF RID: 9167
		public class CODEX
		{
			// Token: 0x0400A114 RID: 41236
			public static LocString SEARCH_HEADER = "Search Database";

			// Token: 0x0400A115 RID: 41237
			public static LocString BACK_BUTTON = "Back ({0})";

			// Token: 0x0400A116 RID: 41238
			public static LocString TIPS = "Tips";

			// Token: 0x0400A117 RID: 41239
			public static LocString GAME_SYSTEMS = "Systems";

			// Token: 0x0400A118 RID: 41240
			public static LocString DETAILS = "Details";

			// Token: 0x0400A119 RID: 41241
			public static LocString RECIPE_ITEM = "{0} x {1}{2}";

			// Token: 0x0400A11A RID: 41242
			public static LocString RECIPE_FABRICATOR = "{1} ({0} seconds)";

			// Token: 0x0400A11B RID: 41243
			public static LocString RECIPE_FABRICATOR_HEADER = "Produced by";

			// Token: 0x0400A11C RID: 41244
			public static LocString BACK_BUTTON_TOOLTIP = UI.CLICK(UI.ClickType.Click) + " to go back:\n{0}";

			// Token: 0x0400A11D RID: 41245
			public static LocString BACK_BUTTON_NO_HISTORY_TOOLTIP = UI.CLICK(UI.ClickType.Click) + " to go back:\nN/A";

			// Token: 0x0400A11E RID: 41246
			public static LocString FORWARD_BUTTON_TOOLTIP = UI.CLICK(UI.ClickType.Click) + " to go forward:\n{0}";

			// Token: 0x0400A11F RID: 41247
			public static LocString FORWARD_BUTTON_NO_HISTORY_TOOLTIP = UI.CLICK(UI.ClickType.Click) + " to go forward:\nN/A";

			// Token: 0x0400A120 RID: 41248
			public static LocString TITLE = "DATABASE";

			// Token: 0x0400A121 RID: 41249
			public static LocString MANAGEMENT_BUTTON = "DATABASE";

			// Token: 0x02002DA3 RID: 11683
			public class CODEX_DISCOVERED_MESSAGE
			{
				// Token: 0x0400C542 RID: 50498
				public static LocString TITLE = "New Log Entry";

				// Token: 0x0400C543 RID: 50499
				public static LocString BODY = "I've added a new entry to my log: {codex}\n";
			}

			// Token: 0x02002DA4 RID: 11684
			public class SUBWORLDS
			{
				// Token: 0x0400C544 RID: 50500
				public static LocString ELEMENTS = "Elements";

				// Token: 0x0400C545 RID: 50501
				public static LocString PLANTS = "Plants";

				// Token: 0x0400C546 RID: 50502
				public static LocString CRITTERS = "Critters";

				// Token: 0x0400C547 RID: 50503
				public static LocString NONE = "None";
			}

			// Token: 0x02002DA5 RID: 11685
			public class GEYSERS
			{
				// Token: 0x0400C548 RID: 50504
				public static LocString DESC = "Geysers and Fumaroles emit elements at variable intervals. They provide a sustainable source of material, albeit in typically low volumes.\n\nThe variable factors of a geyser are:\n\n    • Emission element \n    • Emission temperature \n    • Emission mass \n    • Cycle length \n    • Dormancy duration \n    • Disease emitted";
			}

			// Token: 0x02002DA6 RID: 11686
			public class EQUIPMENT
			{
				// Token: 0x0400C549 RID: 50505
				public static LocString DESC = "Equipment description";
			}

			// Token: 0x02002DA7 RID: 11687
			public class FOOD
			{
				// Token: 0x0400C54A RID: 50506
				public static LocString QUALITY = "Quality: {0}";

				// Token: 0x0400C54B RID: 50507
				public static LocString CALORIES = "Calories: {0}";

				// Token: 0x0400C54C RID: 50508
				public static LocString SPOILPROPERTIES = "Refrigeration temperature: {0}\nDeep Freeze temperature: {1}\nSpoil time: {2}";

				// Token: 0x0400C54D RID: 50509
				public static LocString NON_PERISHABLE = "Spoil time: Never";
			}

			// Token: 0x02002DA8 RID: 11688
			public class CATEGORYNAMES
			{
				// Token: 0x0400C54E RID: 50510
				public static LocString ROOT = UI.FormatAsLink("Index", "HOME");

				// Token: 0x0400C54F RID: 50511
				public static LocString PLANTS = UI.FormatAsLink("Plants", "PLANTS");

				// Token: 0x0400C550 RID: 50512
				public static LocString CREATURES = UI.FormatAsLink("Critters", "CREATURES");

				// Token: 0x0400C551 RID: 50513
				public static LocString DUPLICANTS = UI.FormatAsLink("Duplicants", "DUPLICANTS");

				// Token: 0x0400C552 RID: 50514
				public static LocString EMAILS = UI.FormatAsLink("E-mail", "EMAILS");

				// Token: 0x0400C553 RID: 50515
				public static LocString JOURNALS = UI.FormatAsLink("Journals", "JOURNALS");

				// Token: 0x0400C554 RID: 50516
				public static LocString MYLOG = UI.FormatAsLink("My Log", "MYLOG");

				// Token: 0x0400C555 RID: 50517
				public static LocString INVESTIGATIONS = UI.FormatAsLink("Investigations", "Investigations");

				// Token: 0x0400C556 RID: 50518
				public static LocString RESEARCHNOTES = UI.FormatAsLink("Research Notes", "RESEARCHNOTES");

				// Token: 0x0400C557 RID: 50519
				public static LocString NOTICES = UI.FormatAsLink("Notices", "NOTICES");

				// Token: 0x0400C558 RID: 50520
				public static LocString FOOD = UI.FormatAsLink("Food", "FOOD");

				// Token: 0x0400C559 RID: 50521
				public static LocString MINION_MODIFIERS = UI.FormatAsLink("Duplicant Effects (EDITOR ONLY)", "MINION_MODIFIERS");

				// Token: 0x0400C55A RID: 50522
				public static LocString BUILDINGS = UI.FormatAsLink("Buildings", "BUILDINGS");

				// Token: 0x0400C55B RID: 50523
				public static LocString ROOMS = UI.FormatAsLink("Rooms", "ROOMS");

				// Token: 0x0400C55C RID: 50524
				public static LocString TECH = UI.FormatAsLink("Research", "TECH");

				// Token: 0x0400C55D RID: 50525
				public static LocString TIPS = UI.FormatAsLink("Tutorials", "LESSONS");

				// Token: 0x0400C55E RID: 50526
				public static LocString EQUIPMENT = UI.FormatAsLink("Equipment", "EQUIPMENT");

				// Token: 0x0400C55F RID: 50527
				public static LocString BIOMES = UI.FormatAsLink("Biomes", "BIOMES");

				// Token: 0x0400C560 RID: 50528
				public static LocString STORYTRAITS = UI.FormatAsLink("Story Traits", "STORYTRAITS");

				// Token: 0x0400C561 RID: 50529
				public static LocString VIDEOS = UI.FormatAsLink("Videos", "VIDEOS");

				// Token: 0x0400C562 RID: 50530
				public static LocString MISCELLANEOUSTIPS = UI.FormatAsLink("Tips", "MISCELLANEOUSTIPS");

				// Token: 0x0400C563 RID: 50531
				public static LocString MISCELLANEOUSITEMS = UI.FormatAsLink("Items", "MISCELLANEOUSITEMS");

				// Token: 0x0400C564 RID: 50532
				public static LocString ELEMENTS = UI.FormatAsLink("Elements", "ELEMENTS");

				// Token: 0x0400C565 RID: 50533
				public static LocString ELEMENTSSOLID = UI.FormatAsLink("Solids", "ELEMENTS_SOLID");

				// Token: 0x0400C566 RID: 50534
				public static LocString ELEMENTSGAS = UI.FormatAsLink("Gases", "ELEMENTS_GAS");

				// Token: 0x0400C567 RID: 50535
				public static LocString ELEMENTSLIQUID = UI.FormatAsLink("Liquids", "ELEMENTS_LIQUID");

				// Token: 0x0400C568 RID: 50536
				public static LocString ELEMENTSOTHER = UI.FormatAsLink("Other", "ELEMENTS_OTHER");

				// Token: 0x0400C569 RID: 50537
				public static LocString ELEMENTTYPES = UI.FormatAsLink("Element Properties", "ELEMENT_TYPES");

				// Token: 0x0400C56A RID: 50538
				public static LocString BUILDINGMATERIALCLASSES = UI.FormatAsLink("Building Materials", "BUILDING_MATERIAL_CLASSES");

				// Token: 0x0400C56B RID: 50539
				public static LocString INDUSTRIALINGREDIENTS = UI.FormatAsLink("Industrial Ingredients", "INDUSTRIALINGREDIENTS");

				// Token: 0x0400C56C RID: 50540
				public static LocString DUPLICANTSCATEGORY = UI.FormatAsLink("Duplicants", "DUPLICANTS");

				// Token: 0x0400C56D RID: 50541
				public static LocString MEDICINES = UI.FormatAsLink("Medicines", "MEDICINES");

				// Token: 0x0400C56E RID: 50542
				public static LocString GEYSERS = UI.FormatAsLink("Geysers", "GEYSERS");

				// Token: 0x0400C56F RID: 50543
				public static LocString SYSTEMS = UI.FormatAsLink("Systems", "SYSTEMS");

				// Token: 0x0400C570 RID: 50544
				public static LocString ROLES = UI.FormatAsLink("Duplicant Skills", "ROLES");

				// Token: 0x0400C571 RID: 50545
				public static LocString DISEASE = UI.FormatAsLink("Disease", "DISEASE");

				// Token: 0x0400C572 RID: 50546
				public static LocString SICKNESS = UI.FormatAsLink("Sickness", "SICKNESS");

				// Token: 0x0400C573 RID: 50547
				public static LocString MEDIA = UI.FormatAsLink("Media", "MEDIA");
			}
		}

		// Token: 0x020023D0 RID: 9168
		public class DEVELOPMENTBUILDS
		{
			// Token: 0x0400A122 RID: 41250
			public static LocString WATERMARK = "BUILD: {0}";

			// Token: 0x0400A123 RID: 41251
			public static LocString TESTING_WATERMARK = "TESTING BUILD: {0}";

			// Token: 0x0400A124 RID: 41252
			public static LocString TESTING_TOOLTIP = "This game is currently running a Test version.\n\n" + UI.CLICK(UI.ClickType.Click) + " for more info.";

			// Token: 0x0400A125 RID: 41253
			public static LocString TESTING_MESSAGE_TITLE = "TESTING BUILD";

			// Token: 0x0400A126 RID: 41254
			public static LocString TESTING_MESSAGE = "This game is running a Test version of Oxygen Not Included. This means that some features may be in development or buggier than normal, and require more testing before they can be moved into the Release build.\n\nIf you encounter any bugs or strange behavior, please add a report to the bug forums. We appreciate it!";

			// Token: 0x0400A127 RID: 41255
			public static LocString TESTING_MORE_INFO = "BUG FORUMS";

			// Token: 0x0400A128 RID: 41256
			public static LocString FULL_PATCH_NOTES = "Full Patch Notes";

			// Token: 0x0400A129 RID: 41257
			public static LocString PREVIOUS_VERSION = "Previous Version";

			// Token: 0x02002DA9 RID: 11689
			public class ALPHA
			{
				// Token: 0x02003A71 RID: 14961
				public class MESSAGES
				{
					// Token: 0x0400E83D RID: 59453
					public static LocString FORUMBUTTON = "FORUMS";

					// Token: 0x0400E83E RID: 59454
					public static LocString MAILINGLIST = "MAILING LIST";

					// Token: 0x0400E83F RID: 59455
					public static LocString PATCHNOTES = "PATCH NOTES";

					// Token: 0x0400E840 RID: 59456
					public static LocString FEEDBACK = "FEEDBACK";
				}

				// Token: 0x02003A72 RID: 14962
				public class LOADING
				{
					// Token: 0x0400E841 RID: 59457
					public static LocString TITLE = "<b>Welcome to Oxygen Not Included!</b>";

					// Token: 0x0400E842 RID: 59458
					public static LocString BODY = "This game is in the early stages of development which means you're likely to encounter strange, amusing, and occasionally just downright frustrating bugs.\n\nDuring this time Oxygen Not Included will be receiving regular updates to fix bugs, add features, and introduce additional content, so if you encounter issues or just have suggestions to share, please let us know on our forums: <u>http://forums.kleientertainment.com</u>\n\nA special thanks to those who joined us during our time in Alpha. We value your feedback and thank you for joining us in the development process. We couldn't do this without you.\n\nEnjoy your time in deep space!\n\n- Klei";

					// Token: 0x0400E843 RID: 59459
					public static LocString BODY_NOLINKS = "This DLC is currently in active development, which means you're likely to encounter strange, amusing, and occasionally just downright frustrating bugs.\n\n During this time Spaced Out! will be receiving regular updates to fix bugs, add features, and introduce additional content.\n\n We've got lots of content old and new to add to this DLC before it's ready, and we're happy to have you along with us. Enjoy your time in deep space!\n\n - The Team at Klei";

					// Token: 0x0400E844 RID: 59460
					public static LocString FORUMBUTTON = "Visit Forums";
				}

				// Token: 0x02003A73 RID: 14963
				public class HEALTHY_MESSAGE
				{
					// Token: 0x0400E845 RID: 59461
					public static LocString CONTINUEBUTTON = "Thanks!";
				}
			}

			// Token: 0x02002DAA RID: 11690
			public class PREVIOUS_UPDATE
			{
				// Token: 0x0400C574 RID: 50548
				public static LocString TITLE = "<b>Welcome to Oxygen Not Included</b>";

				// Token: 0x0400C575 RID: 50549
				public static LocString BODY = "Whoops!\n\nYou're about to opt in to the <b>Previous Update branch</b>. That means opting out of all new features, fixes and content from the live branch.\n\nThis branch is temporary. It will be replaced when the next update is released. It's also completely unsupported: please don't report bugs or issues you find here.\n\nAre you sure you want to opt in?";

				// Token: 0x0400C576 RID: 50550
				public static LocString CONTINUEBUTTON = "Play Old Version";

				// Token: 0x0400C577 RID: 50551
				public static LocString FORUMBUTTON = "More Information";

				// Token: 0x0400C578 RID: 50552
				public static LocString QUITBUTTON = "Quit";
			}

			// Token: 0x02002DAB RID: 11691
			public class DLC_BETA
			{
				// Token: 0x0400C579 RID: 50553
				public static LocString TITLE = "<b>Welcome to Oxygen Not Included</b>";

				// Token: 0x0400C57A RID: 50554
				public static LocString BODY = "You're about to opt in to the beta for <b>The Bionic Booster Pack</b> DLC.\nThis free beta is a work in progress, and will be discontinued before the paid DLC is released. \n\nAre you sure you want to opt in?";

				// Token: 0x0400C57B RID: 50555
				public static LocString CONTINUEBUTTON = "Play Beta";

				// Token: 0x0400C57C RID: 50556
				public static LocString FORUMBUTTON = "More Information";

				// Token: 0x0400C57D RID: 50557
				public static LocString QUITBUTTON = "Quit";
			}

			// Token: 0x02002DAC RID: 11692
			public class UPDATES
			{
				// Token: 0x0400C57E RID: 50558
				public static LocString UPDATES_HEADER = "NEXT UPGRADE LIVE IN";

				// Token: 0x0400C57F RID: 50559
				public static LocString NOW = "Less than a day";

				// Token: 0x0400C580 RID: 50560
				public static LocString TWENTY_FOUR_HOURS = "Less than a day";

				// Token: 0x0400C581 RID: 50561
				public static LocString FINAL_WEEK = "{0} days";

				// Token: 0x0400C582 RID: 50562
				public static LocString BIGGER_TIMES = "{1} weeks {0} days";
			}
		}

		// Token: 0x020023D1 RID: 9169
		public class UNITSUFFIXES
		{
			// Token: 0x0400A12A RID: 41258
			public static LocString SECOND = " s";

			// Token: 0x0400A12B RID: 41259
			public static LocString PERSECOND = "/s";

			// Token: 0x0400A12C RID: 41260
			public static LocString PERCYCLE = "/cycle";

			// Token: 0x0400A12D RID: 41261
			public static LocString UNIT = " unit";

			// Token: 0x0400A12E RID: 41262
			public static LocString UNITS = " units";

			// Token: 0x0400A12F RID: 41263
			public static LocString PERCENT = "%";

			// Token: 0x0400A130 RID: 41264
			public static LocString DEGREES = " degrees";

			// Token: 0x0400A131 RID: 41265
			public static LocString CRITTERS = " critters";

			// Token: 0x0400A132 RID: 41266
			public static LocString GROWTH = "growth";

			// Token: 0x0400A133 RID: 41267
			public static LocString SECONDS = "Seconds";

			// Token: 0x0400A134 RID: 41268
			public static LocString DUPLICANTS = "Duplicants";

			// Token: 0x0400A135 RID: 41269
			public static LocString GERMS = "Germs";

			// Token: 0x0400A136 RID: 41270
			public static LocString ROCKET_MISSIONS = "Missions";

			// Token: 0x0400A137 RID: 41271
			public static LocString TILES = "Tiles";

			// Token: 0x02002DAD RID: 11693
			public class MASS
			{
				// Token: 0x0400C583 RID: 50563
				public static LocString TONNE = " t";

				// Token: 0x0400C584 RID: 50564
				public static LocString KILOGRAM = " kg";

				// Token: 0x0400C585 RID: 50565
				public static LocString GRAM = " g";

				// Token: 0x0400C586 RID: 50566
				public static LocString MILLIGRAM = " mg";

				// Token: 0x0400C587 RID: 50567
				public static LocString MICROGRAM = " mcg";

				// Token: 0x0400C588 RID: 50568
				public static LocString POUND = " lb";

				// Token: 0x0400C589 RID: 50569
				public static LocString DRACHMA = " dr";

				// Token: 0x0400C58A RID: 50570
				public static LocString GRAIN = " gr";
			}

			// Token: 0x02002DAE RID: 11694
			public class TEMPERATURE
			{
				// Token: 0x0400C58B RID: 50571
				public static LocString CELSIUS = " " + 'º'.ToString() + "C";

				// Token: 0x0400C58C RID: 50572
				public static LocString FAHRENHEIT = " " + 'º'.ToString() + "F";

				// Token: 0x0400C58D RID: 50573
				public static LocString KELVIN = " K";
			}

			// Token: 0x02002DAF RID: 11695
			public class CALORIES
			{
				// Token: 0x0400C58E RID: 50574
				public static LocString CALORIE = " cal";

				// Token: 0x0400C58F RID: 50575
				public static LocString KILOCALORIE = " kcal";
			}

			// Token: 0x02002DB0 RID: 11696
			public class ELECTRICAL
			{
				// Token: 0x0400C590 RID: 50576
				public static LocString JOULE = " J";

				// Token: 0x0400C591 RID: 50577
				public static LocString KILOJOULE = " kJ";

				// Token: 0x0400C592 RID: 50578
				public static LocString MEGAJOULE = " MJ";

				// Token: 0x0400C593 RID: 50579
				public static LocString WATT = " W";

				// Token: 0x0400C594 RID: 50580
				public static LocString KILOWATT = " kW";
			}

			// Token: 0x02002DB1 RID: 11697
			public class HEAT
			{
				// Token: 0x0400C595 RID: 50581
				public static LocString DTU = " DTU";

				// Token: 0x0400C596 RID: 50582
				public static LocString KDTU = " kDTU";

				// Token: 0x0400C597 RID: 50583
				public static LocString DTU_S = " DTU/s";

				// Token: 0x0400C598 RID: 50584
				public static LocString KDTU_S = " kDTU/s";
			}

			// Token: 0x02002DB2 RID: 11698
			public class DISTANCE
			{
				// Token: 0x0400C599 RID: 50585
				public static LocString METER = " m";

				// Token: 0x0400C59A RID: 50586
				public static LocString KILOMETER = " km";
			}

			// Token: 0x02002DB3 RID: 11699
			public class DISEASE
			{
				// Token: 0x0400C59B RID: 50587
				public static LocString UNITS = " germs";
			}

			// Token: 0x02002DB4 RID: 11700
			public class NOISE
			{
				// Token: 0x0400C59C RID: 50588
				public static LocString UNITS = " dB";
			}

			// Token: 0x02002DB5 RID: 11701
			public class INFORMATION
			{
				// Token: 0x0400C59D RID: 50589
				public static LocString BYTE = "B";

				// Token: 0x0400C59E RID: 50590
				public static LocString KILOBYTE = "kB";

				// Token: 0x0400C59F RID: 50591
				public static LocString MEGABYTE = "MB";

				// Token: 0x0400C5A0 RID: 50592
				public static LocString GIGABYTE = "GB";

				// Token: 0x0400C5A1 RID: 50593
				public static LocString TERABYTE = "TB";
			}

			// Token: 0x02002DB6 RID: 11702
			public class LIGHT
			{
				// Token: 0x0400C5A2 RID: 50594
				public static LocString LUX = " lux";
			}

			// Token: 0x02002DB7 RID: 11703
			public class RADIATION
			{
				// Token: 0x0400C5A3 RID: 50595
				public static LocString RADS = " rads";
			}

			// Token: 0x02002DB8 RID: 11704
			public class HIGHENERGYPARTICLES
			{
				// Token: 0x0400C5A4 RID: 50596
				public static LocString PARTRICLE = " Radbolt";

				// Token: 0x0400C5A5 RID: 50597
				public static LocString PARTRICLES = " Radbolts";
			}
		}

		// Token: 0x020023D2 RID: 9170
		public class OVERLAYS
		{
			// Token: 0x02002DB9 RID: 11705
			public class TILEMODE
			{
				// Token: 0x0400C5A6 RID: 50598
				public static LocString NAME = "MATERIALS OVERLAY";

				// Token: 0x0400C5A7 RID: 50599
				public static LocString BUTTON = "Materials Overlay";
			}

			// Token: 0x02002DBA RID: 11706
			public class OXYGEN
			{
				// Token: 0x0400C5A8 RID: 50600
				public static LocString NAME = "OXYGEN OVERLAY";

				// Token: 0x0400C5A9 RID: 50601
				public static LocString BUTTON = "Oxygen Overlay";

				// Token: 0x0400C5AA RID: 50602
				public static LocString LEGEND1 = "Very Breathable";

				// Token: 0x0400C5AB RID: 50603
				public static LocString LEGEND2 = "Breathable";

				// Token: 0x0400C5AC RID: 50604
				public static LocString LEGEND3 = "Barely Breathable";

				// Token: 0x0400C5AD RID: 50605
				public static LocString LEGEND4 = "Unbreathable";

				// Token: 0x0400C5AE RID: 50606
				public static LocString LEGEND5 = "Barely Breathable";

				// Token: 0x0400C5AF RID: 50607
				public static LocString LEGEND6 = "Unbreathable";

				// Token: 0x02003A74 RID: 14964
				public class TOOLTIPS
				{
					// Token: 0x0400E846 RID: 59462
					public static LocString LEGEND1 = string.Concat(new string[]
					{
						"<b>Very Breathable</b>\nHigh ",
						UI.PRE_KEYWORD,
						"Oxygen",
						UI.PST_KEYWORD,
						" concentrations"
					});

					// Token: 0x0400E847 RID: 59463
					public static LocString LEGEND2 = string.Concat(new string[]
					{
						"<b>Breathable</b>\nSufficient ",
						UI.PRE_KEYWORD,
						"Oxygen",
						UI.PST_KEYWORD,
						" concentrations"
					});

					// Token: 0x0400E848 RID: 59464
					public static LocString LEGEND3 = string.Concat(new string[]
					{
						"<b>Barely Breathable</b>\nLow ",
						UI.PRE_KEYWORD,
						"Oxygen",
						UI.PST_KEYWORD,
						" concentrations"
					});

					// Token: 0x0400E849 RID: 59465
					public static LocString LEGEND4 = string.Concat(new string[]
					{
						"<b>Unbreathable</b>\nExtremely low or absent ",
						UI.PRE_KEYWORD,
						"Oxygen",
						UI.PST_KEYWORD,
						" concentrations\n\nDuplicants will suffocate if trapped in these areas"
					});

					// Token: 0x0400E84A RID: 59466
					public static LocString LEGEND5 = "<b>Slightly Toxic</b>\nHarmful gas concentration";

					// Token: 0x0400E84B RID: 59467
					public static LocString LEGEND6 = "<b>Very Toxic</b>\nLethal gas concentration";
				}
			}

			// Token: 0x02002DBB RID: 11707
			public class ELECTRICAL
			{
				// Token: 0x0400C5B0 RID: 50608
				public static LocString NAME = "POWER OVERLAY";

				// Token: 0x0400C5B1 RID: 50609
				public static LocString BUTTON = "Power Overlay";

				// Token: 0x0400C5B2 RID: 50610
				public static LocString LEGEND1 = "<b>BUILDING POWER</b>";

				// Token: 0x0400C5B3 RID: 50611
				public static LocString LEGEND2 = "Consumer";

				// Token: 0x0400C5B4 RID: 50612
				public static LocString LEGEND3 = "Producer";

				// Token: 0x0400C5B5 RID: 50613
				public static LocString LEGEND4 = "<b>CIRCUIT POWER HEALTH</b>";

				// Token: 0x0400C5B6 RID: 50614
				public static LocString LEGEND5 = "Inactive";

				// Token: 0x0400C5B7 RID: 50615
				public static LocString LEGEND6 = "Safe";

				// Token: 0x0400C5B8 RID: 50616
				public static LocString LEGEND7 = "Strained";

				// Token: 0x0400C5B9 RID: 50617
				public static LocString LEGEND8 = "Overloaded";

				// Token: 0x0400C5BA RID: 50618
				public static LocString DIAGRAM_HEADER = "Energy from the <b>Left Outlet</b> is used by the <b>Right Outlet</b>";

				// Token: 0x0400C5BB RID: 50619
				public static LocString LEGEND_SWITCH = "Switch";

				// Token: 0x02003A75 RID: 14965
				public class TOOLTIPS
				{
					// Token: 0x0400E84C RID: 59468
					public static LocString LEGEND1 = "Displays whether buildings use or generate " + UI.FormatAsLink("Power", "POWER");

					// Token: 0x0400E84D RID: 59469
					public static LocString LEGEND2 = "<b>Consumer</b>\nThese buildings draw power from a circuit";

					// Token: 0x0400E84E RID: 59470
					public static LocString LEGEND3 = "<b>Producer</b>\nThese buildings generate power for a circuit";

					// Token: 0x0400E84F RID: 59471
					public static LocString LEGEND4 = "Displays the health of wire systems";

					// Token: 0x0400E850 RID: 59472
					public static LocString LEGEND5 = "<b>Inactive</b>\nThere is no power activity on these circuits";

					// Token: 0x0400E851 RID: 59473
					public static LocString LEGEND6 = "<b>Safe</b>\nThese circuits are not in danger of overloading";

					// Token: 0x0400E852 RID: 59474
					public static LocString LEGEND7 = "<b>Strained</b>\nThese circuits are close to consuming more power than their wires support";

					// Token: 0x0400E853 RID: 59475
					public static LocString LEGEND8 = "<b>Overloaded</b>\nThese circuits are consuming more power than their wires support";

					// Token: 0x0400E854 RID: 59476
					public static LocString LEGEND_SWITCH = "<b>Switch</b>\nActivates or deactivates connected circuits";
				}
			}

			// Token: 0x02002DBC RID: 11708
			public class TEMPERATURE
			{
				// Token: 0x0400C5BC RID: 50620
				public static LocString NAME = "TEMPERATURE OVERLAY";

				// Token: 0x0400C5BD RID: 50621
				public static LocString BUTTON = "Temperature Overlay";

				// Token: 0x0400C5BE RID: 50622
				public static LocString EXTREMECOLD = "Absolute Zero";

				// Token: 0x0400C5BF RID: 50623
				public static LocString VERYCOLD = "Cold";

				// Token: 0x0400C5C0 RID: 50624
				public static LocString COLD = "Chilled";

				// Token: 0x0400C5C1 RID: 50625
				public static LocString TEMPERATE = "Temperate";

				// Token: 0x0400C5C2 RID: 50626
				public static LocString HOT = "Warm";

				// Token: 0x0400C5C3 RID: 50627
				public static LocString VERYHOT = "Hot";

				// Token: 0x0400C5C4 RID: 50628
				public static LocString EXTREMEHOT = "Scorching";

				// Token: 0x0400C5C5 RID: 50629
				public static LocString MAXHOT = "Molten";

				// Token: 0x0400C5C6 RID: 50630
				public static LocString HEATSOURCES = "Heat Source";

				// Token: 0x0400C5C7 RID: 50631
				public static LocString HEATSINK = "Heat Sink";

				// Token: 0x0400C5C8 RID: 50632
				public static LocString DEFAULT_TEMPERATURE_BUTTON = "Default";

				// Token: 0x02003A76 RID: 14966
				public class TOOLTIPS
				{
					// Token: 0x0400E855 RID: 59477
					public static LocString TEMPERATURE = "Temperatures reaching {0}";

					// Token: 0x0400E856 RID: 59478
					public static LocString HEATSOURCES = "Elements displaying this symbol can produce heat";

					// Token: 0x0400E857 RID: 59479
					public static LocString HEATSINK = "Elements displaying this symbol can absorb heat";
				}
			}

			// Token: 0x02002DBD RID: 11709
			public class STATECHANGE
			{
				// Token: 0x0400C5C9 RID: 50633
				public static LocString LOWPOINT = "Low energy state change";

				// Token: 0x0400C5CA RID: 50634
				public static LocString STABLE = "Stable";

				// Token: 0x0400C5CB RID: 50635
				public static LocString HIGHPOINT = "High energy state change";

				// Token: 0x02003A77 RID: 14967
				public class TOOLTIPS
				{
					// Token: 0x0400E858 RID: 59480
					public static LocString LOWPOINT = "Nearing a low energy state change";

					// Token: 0x0400E859 RID: 59481
					public static LocString STABLE = "Not near any state changes";

					// Token: 0x0400E85A RID: 59482
					public static LocString HIGHPOINT = "Nearing high energy state change";
				}
			}

			// Token: 0x02002DBE RID: 11710
			public class HEATFLOW
			{
				// Token: 0x0400C5CC RID: 50636
				public static LocString NAME = "THERMAL TOLERANCE OVERLAY";

				// Token: 0x0400C5CD RID: 50637
				public static LocString HOVERTITLE = "THERMAL TOLERANCE";

				// Token: 0x0400C5CE RID: 50638
				public static LocString BUTTON = "Thermal Tolerance Overlay";

				// Token: 0x0400C5CF RID: 50639
				public static LocString COOLING = "Body Heat Loss";

				// Token: 0x0400C5D0 RID: 50640
				public static LocString NEUTRAL = "Comfort Zone";

				// Token: 0x0400C5D1 RID: 50641
				public static LocString HEATING = "Body Heat Retention";

				// Token: 0x0400C5D2 RID: 50642
				public static LocString COOLING_DUPE = "Body Heat Loss {0}\n\nUncomfortably chilly surroundings";

				// Token: 0x0400C5D3 RID: 50643
				public static LocString NEUTRAL_DUPE = "Comfort Zone {0}";

				// Token: 0x0400C5D4 RID: 50644
				public static LocString HEATING_DUPE = "Body Heat Loss {0}\n\nUncomfortably toasty surroundings";

				// Token: 0x02003A78 RID: 14968
				public class TOOLTIPS
				{
					// Token: 0x0400E85B RID: 59483
					public static LocString COOLING = "<b>Body Heat Loss</b>\nUncomfortably cold\n\nDuplicants lose more heat in chilly surroundings than they can absorb\n    • Warm Coats help Duplicants retain body heat";

					// Token: 0x0400E85C RID: 59484
					public static LocString NEUTRAL = "<b>Comfort Zone</b>\nComfortable area\n\nDuplicants can regulate their internal temperatures in these areas";

					// Token: 0x0400E85D RID: 59485
					public static LocString HEATING = "<b>Body Heat Retention</b>\nUncomfortably warm\n\nDuplicants absorb more heat in toasty surroundings than they can release";
				}
			}

			// Token: 0x02002DBF RID: 11711
			public class RELATIVETEMPERATURE
			{
				// Token: 0x0400C5D5 RID: 50645
				public static LocString NAME = "RELATIVE TEMPERATURE";

				// Token: 0x0400C5D6 RID: 50646
				public static LocString HOVERTITLE = "RELATIVE TEMPERATURE";

				// Token: 0x0400C5D7 RID: 50647
				public static LocString BUTTON = "Relative Temperature Overlay";
			}

			// Token: 0x02002DC0 RID: 11712
			public class ROOMS
			{
				// Token: 0x0400C5D8 RID: 50648
				public static LocString NAME = "ROOM OVERLAY";

				// Token: 0x0400C5D9 RID: 50649
				public static LocString BUTTON = "Room Overlay";

				// Token: 0x0400C5DA RID: 50650
				public static LocString ROOM = "Room {0}";

				// Token: 0x0400C5DB RID: 50651
				public static LocString HOVERTITLE = "ROOMS";

				// Token: 0x02003A79 RID: 14969
				public static class NOROOM
				{
					// Token: 0x0400E85E RID: 59486
					public static LocString HEADER = "No Room";

					// Token: 0x0400E85F RID: 59487
					public static LocString DESC = "Enclose this space with walls and doors to make a room";

					// Token: 0x0400E860 RID: 59488
					public static LocString TOO_BIG = "<color=#F44A47FF>    • Size: {0} Tiles\n    • Maximum room size: {1} Tiles</color>";
				}

				// Token: 0x02003A7A RID: 14970
				public class TOOLTIPS
				{
					// Token: 0x0400E861 RID: 59489
					public static LocString ROOM = "Completed Duplicant bedrooms";

					// Token: 0x0400E862 RID: 59490
					public static LocString NOROOMS = "Duplicants have nowhere to sleep";
				}
			}

			// Token: 0x02002DC1 RID: 11713
			public class JOULES
			{
				// Token: 0x0400C5DC RID: 50652
				public static LocString NAME = "JOULES";

				// Token: 0x0400C5DD RID: 50653
				public static LocString HOVERTITLE = "JOULES";

				// Token: 0x0400C5DE RID: 50654
				public static LocString BUTTON = "Joules Overlay";
			}

			// Token: 0x02002DC2 RID: 11714
			public class LIGHTING
			{
				// Token: 0x0400C5DF RID: 50655
				public static LocString NAME = "LIGHT OVERLAY";

				// Token: 0x0400C5E0 RID: 50656
				public static LocString BUTTON = "Light Overlay";

				// Token: 0x0400C5E1 RID: 50657
				public static LocString LITAREA = "Lit Area";

				// Token: 0x0400C5E2 RID: 50658
				public static LocString DARK = "Unlit Area";

				// Token: 0x0400C5E3 RID: 50659
				public static LocString HOVERTITLE = "LIGHT";

				// Token: 0x0400C5E4 RID: 50660
				public static LocString DESC = "{0} Lux";

				// Token: 0x02003A7B RID: 14971
				public class RANGES
				{
					// Token: 0x0400E863 RID: 59491
					public static LocString NO_LIGHT = "Pitch Black";

					// Token: 0x0400E864 RID: 59492
					public static LocString VERY_LOW_LIGHT = "Very Dim";

					// Token: 0x0400E865 RID: 59493
					public static LocString LOW_LIGHT = "Dim";

					// Token: 0x0400E866 RID: 59494
					public static LocString MEDIUM_LIGHT = "Well Lit";

					// Token: 0x0400E867 RID: 59495
					public static LocString HIGH_LIGHT = "Bright";

					// Token: 0x0400E868 RID: 59496
					public static LocString VERY_HIGH_LIGHT = "Brilliant";

					// Token: 0x0400E869 RID: 59497
					public static LocString MAX_LIGHT = "Blinding";
				}

				// Token: 0x02003A7C RID: 14972
				public class TOOLTIPS
				{
					// Token: 0x0400E86A RID: 59498
					public static LocString NAME = "LIGHT OVERLAY";

					// Token: 0x0400E86B RID: 59499
					public static LocString LITAREA = "<b>Lit Area</b>\nWorking in well-lit areas improves Duplicant " + UI.PRE_KEYWORD + "Morale" + UI.PST_KEYWORD;

					// Token: 0x0400E86C RID: 59500
					public static LocString DARK = "<b>Unlit Area</b>\nWorking in the dark has no effect on Duplicants";
				}
			}

			// Token: 0x02002DC3 RID: 11715
			public class CROP
			{
				// Token: 0x0400C5E5 RID: 50661
				public static LocString NAME = "FARMING OVERLAY";

				// Token: 0x0400C5E6 RID: 50662
				public static LocString BUTTON = "Farming Overlay";

				// Token: 0x0400C5E7 RID: 50663
				public static LocString GROWTH_HALTED = "Halted Growth";

				// Token: 0x0400C5E8 RID: 50664
				public static LocString GROWING = "Growing";

				// Token: 0x0400C5E9 RID: 50665
				public static LocString FULLY_GROWN = "Fully Grown";

				// Token: 0x02003A7D RID: 14973
				public class TOOLTIPS
				{
					// Token: 0x0400E86D RID: 59501
					public static LocString GROWTH_HALTED = "<b>Halted Growth</b>\nSubstandard conditions prevent these plants from growing";

					// Token: 0x0400E86E RID: 59502
					public static LocString GROWING = "<b>Growing</b>\nThese plants are thriving in their current conditions";

					// Token: 0x0400E86F RID: 59503
					public static LocString FULLY_GROWN = "<b>Fully Grown</b>\nThese plants have reached maturation\n\nSelect the " + UI.FormatAsTool("Harvest Tool", global::Action.Harvest) + " to batch harvest";
				}
			}

			// Token: 0x02002DC4 RID: 11716
			public class LIQUIDPLUMBING
			{
				// Token: 0x0400C5EA RID: 50666
				public static LocString NAME = "PLUMBING OVERLAY";

				// Token: 0x0400C5EB RID: 50667
				public static LocString BUTTON = "Plumbing Overlay";

				// Token: 0x0400C5EC RID: 50668
				public static LocString CONSUMER = "Output Pipe";

				// Token: 0x0400C5ED RID: 50669
				public static LocString FILTERED = "Filtered Output Pipe";

				// Token: 0x0400C5EE RID: 50670
				public static LocString PRODUCER = "Building Intake";

				// Token: 0x0400C5EF RID: 50671
				public static LocString CONNECTED = "Connected";

				// Token: 0x0400C5F0 RID: 50672
				public static LocString DISCONNECTED = "Disconnected";

				// Token: 0x0400C5F1 RID: 50673
				public static LocString NETWORK = "Liquid Network {0}";

				// Token: 0x0400C5F2 RID: 50674
				public static LocString DIAGRAM_BEFORE_ARROW = "Liquid flows from <b>Output Pipe</b>";

				// Token: 0x0400C5F3 RID: 50675
				public static LocString DIAGRAM_AFTER_ARROW = "<b>Building Intake</b>";

				// Token: 0x02003A7E RID: 14974
				public class TOOLTIPS
				{
					// Token: 0x0400E870 RID: 59504
					public static LocString CONNECTED = "Connected to a " + UI.FormatAsLink("Liquid Pipe", "LIQUIDCONDUIT");

					// Token: 0x0400E871 RID: 59505
					public static LocString DISCONNECTED = "Not connected to a " + UI.FormatAsLink("Liquid Pipe", "LIQUIDCONDUIT");

					// Token: 0x0400E872 RID: 59506
					public static LocString CONSUMER = "<b>Output Pipe</b>\nOutputs send liquid into pipes\n\nMust be on the same network as at least one " + UI.FormatAsLink("Intake", "LIQUIDPIPING");

					// Token: 0x0400E873 RID: 59507
					public static LocString FILTERED = "<b>Filtered Output Pipe</b>\nFiltered Outputs send filtered liquid into pipes\n\nMust be on the same network as at least one " + UI.FormatAsLink("Intake", "LIQUIDPIPING");

					// Token: 0x0400E874 RID: 59508
					public static LocString PRODUCER = "<b>Building Intake</b>\nIntakes send liquid into buildings\n\nMust be on the same network as at least one " + UI.FormatAsLink("Output", "LIQUIDPIPING");

					// Token: 0x0400E875 RID: 59509
					public static LocString NETWORK = "Liquid network {0}";
				}
			}

			// Token: 0x02002DC5 RID: 11717
			public class GASPLUMBING
			{
				// Token: 0x0400C5F4 RID: 50676
				public static LocString NAME = "VENTILATION OVERLAY";

				// Token: 0x0400C5F5 RID: 50677
				public static LocString BUTTON = "Ventilation Overlay";

				// Token: 0x0400C5F6 RID: 50678
				public static LocString CONSUMER = "Output Pipe";

				// Token: 0x0400C5F7 RID: 50679
				public static LocString FILTERED = "Filtered Output Pipe";

				// Token: 0x0400C5F8 RID: 50680
				public static LocString PRODUCER = "Building Intake";

				// Token: 0x0400C5F9 RID: 50681
				public static LocString CONNECTED = "Connected";

				// Token: 0x0400C5FA RID: 50682
				public static LocString DISCONNECTED = "Disconnected";

				// Token: 0x0400C5FB RID: 50683
				public static LocString NETWORK = "Gas Network {0}";

				// Token: 0x0400C5FC RID: 50684
				public static LocString DIAGRAM_BEFORE_ARROW = "Gas flows from <b>Output Pipe</b>";

				// Token: 0x0400C5FD RID: 50685
				public static LocString DIAGRAM_AFTER_ARROW = "<b>Building Intake</b>";

				// Token: 0x02003A7F RID: 14975
				public class TOOLTIPS
				{
					// Token: 0x0400E876 RID: 59510
					public static LocString CONNECTED = "Connected to a " + UI.FormatAsLink("Gas Pipe", "GASPIPING");

					// Token: 0x0400E877 RID: 59511
					public static LocString DISCONNECTED = "Not connected to a " + UI.FormatAsLink("Gas Pipe", "GASPIPING");

					// Token: 0x0400E878 RID: 59512
					public static LocString CONSUMER = string.Concat(new string[]
					{
						"<b>Output Pipe</b>\nOutputs send ",
						UI.PRE_KEYWORD,
						"Gas",
						UI.PST_KEYWORD,
						" into ",
						UI.PRE_KEYWORD,
						"Pipes",
						UI.PST_KEYWORD,
						"\n\nMust be on the same network as at least one ",
						UI.FormatAsLink("Intake", "GASPIPING")
					});

					// Token: 0x0400E879 RID: 59513
					public static LocString FILTERED = string.Concat(new string[]
					{
						"<b>Filtered Output Pipe</b>\nFiltered Outputs send filtered ",
						UI.PRE_KEYWORD,
						"Gas",
						UI.PST_KEYWORD,
						" into ",
						UI.PRE_KEYWORD,
						"Pipes",
						UI.PST_KEYWORD,
						"\n\nMust be on the same network as at least one ",
						UI.FormatAsLink("Intake", "GASPIPING")
					});

					// Token: 0x0400E87A RID: 59514
					public static LocString PRODUCER = "<b>Building Intake</b>\nIntakes send gas into buildings\n\nMust be on the same network as at least one " + UI.FormatAsLink("Output", "GASPIPING");

					// Token: 0x0400E87B RID: 59515
					public static LocString NETWORK = "Gas network {0}";
				}
			}

			// Token: 0x02002DC6 RID: 11718
			public class SUIT
			{
				// Token: 0x0400C5FE RID: 50686
				public static LocString NAME = "EXOSUIT OVERLAY";

				// Token: 0x0400C5FF RID: 50687
				public static LocString BUTTON = "Exosuit Overlay";

				// Token: 0x0400C600 RID: 50688
				public static LocString SUIT_ICON = "Exosuit";

				// Token: 0x0400C601 RID: 50689
				public static LocString SUIT_ICON_TOOLTIP = "<b>Exosuit</b>\nHighlights the current location of equippable exosuits";
			}

			// Token: 0x02002DC7 RID: 11719
			public class LOGIC
			{
				// Token: 0x0400C602 RID: 50690
				public static LocString NAME = "AUTOMATION OVERLAY";

				// Token: 0x0400C603 RID: 50691
				public static LocString BUTTON = "Automation Overlay";

				// Token: 0x0400C604 RID: 50692
				public static LocString INPUT = "Input Port";

				// Token: 0x0400C605 RID: 50693
				public static LocString OUTPUT = "Output Port";

				// Token: 0x0400C606 RID: 50694
				public static LocString RIBBON_INPUT = "Ribbon Input Port";

				// Token: 0x0400C607 RID: 50695
				public static LocString RIBBON_OUTPUT = "Ribbon Output Port";

				// Token: 0x0400C608 RID: 50696
				public static LocString RESET_UPDATE = "Reset Port";

				// Token: 0x0400C609 RID: 50697
				public static LocString CONTROL_INPUT = "Control Port";

				// Token: 0x0400C60A RID: 50698
				public static LocString CIRCUIT_STATUS_HEADER = "GRID STATUS";

				// Token: 0x0400C60B RID: 50699
				public static LocString ONE = "Green";

				// Token: 0x0400C60C RID: 50700
				public static LocString ZERO = "Red";

				// Token: 0x0400C60D RID: 50701
				public static LocString DISCONNECTED = "DISCONNECTED";

				// Token: 0x02003A80 RID: 14976
				public abstract class TOOLTIPS
				{
					// Token: 0x0400E87C RID: 59516
					public static LocString INPUT = "<b>Input Port</b>\nReceives a signal from an automation grid";

					// Token: 0x0400E87D RID: 59517
					public static LocString OUTPUT = "<b>Output Port</b>\nSends a signal out to an automation grid";

					// Token: 0x0400E87E RID: 59518
					public static LocString RIBBON_INPUT = "<b>Ribbon Input Port</b>\nReceives a 4-bit signal from an automation grid";

					// Token: 0x0400E87F RID: 59519
					public static LocString RIBBON_OUTPUT = "<b>Ribbon Output Port</b>\nSends a 4-bit signal out to an automation grid";

					// Token: 0x0400E880 RID: 59520
					public static LocString RESET_UPDATE = "<b>Reset Port</b>\nReset a " + BUILDINGS.PREFABS.LOGICMEMORY.NAME + "'s internal Memory to " + UI.FormatAsAutomationState("Red", UI.AutomationState.Standby);

					// Token: 0x0400E881 RID: 59521
					public static LocString CONTROL_INPUT = "<b>Control Port</b>\nControl the signal selection of a " + BUILDINGS.PREFABS.LOGICGATEMULTIPLEXER.NAME + " or " + BUILDINGS.PREFABS.LOGICGATEDEMULTIPLEXER.NAME;

					// Token: 0x0400E882 RID: 59522
					public static LocString ONE = "<b>Green</b>\nThis port is currently " + UI.FormatAsAutomationState("Green", UI.AutomationState.Active);

					// Token: 0x0400E883 RID: 59523
					public static LocString ZERO = "<b>Red</b>\nThis port is currently " + UI.FormatAsAutomationState("Red", UI.AutomationState.Standby);

					// Token: 0x0400E884 RID: 59524
					public static LocString DISCONNECTED = "<b>Disconnected</b>\nThis port is not connected to an automation grid";
				}
			}

			// Token: 0x02002DC8 RID: 11720
			public class CONVEYOR
			{
				// Token: 0x0400C60E RID: 50702
				public static LocString NAME = "CONVEYOR OVERLAY";

				// Token: 0x0400C60F RID: 50703
				public static LocString BUTTON = "Conveyor Overlay";

				// Token: 0x0400C610 RID: 50704
				public static LocString OUTPUT = "Loader";

				// Token: 0x0400C611 RID: 50705
				public static LocString INPUT = "Receptacle";

				// Token: 0x02003A81 RID: 14977
				public abstract class TOOLTIPS
				{
					// Token: 0x0400E885 RID: 59525
					public static LocString OUTPUT = string.Concat(new string[]
					{
						"<b>Loader</b>\nLoads material onto a ",
						UI.PRE_KEYWORD,
						"Conveyor Rail",
						UI.PST_KEYWORD,
						" for transport to Receptacles"
					});

					// Token: 0x0400E886 RID: 59526
					public static LocString INPUT = string.Concat(new string[]
					{
						"<b>Receptacle</b>\nReceives material from a ",
						UI.PRE_KEYWORD,
						"Conveyor Rail",
						UI.PST_KEYWORD,
						" and stores it for Duplicant use"
					});
				}
			}

			// Token: 0x02002DC9 RID: 11721
			public class DECOR
			{
				// Token: 0x0400C612 RID: 50706
				public static LocString NAME = "DECOR OVERLAY";

				// Token: 0x0400C613 RID: 50707
				public static LocString BUTTON = "Decor Overlay";

				// Token: 0x0400C614 RID: 50708
				public static LocString TOTAL = "Total Decor: ";

				// Token: 0x0400C615 RID: 50709
				public static LocString ENTRY = "{0} {1} {2}";

				// Token: 0x0400C616 RID: 50710
				public static LocString COUNT = "({0})";

				// Token: 0x0400C617 RID: 50711
				public static LocString VALUE = "{0}{1}";

				// Token: 0x0400C618 RID: 50712
				public static LocString VALUE_ZERO = "{0}{1}";

				// Token: 0x0400C619 RID: 50713
				public static LocString HEADER_POSITIVE = "Positive Value:";

				// Token: 0x0400C61A RID: 50714
				public static LocString HEADER_NEGATIVE = "Negative Value:";

				// Token: 0x0400C61B RID: 50715
				public static LocString LOWDECOR = "Negative Decor";

				// Token: 0x0400C61C RID: 50716
				public static LocString HIGHDECOR = "Positive Decor";

				// Token: 0x0400C61D RID: 50717
				public static LocString CLUTTER = "Debris";

				// Token: 0x0400C61E RID: 50718
				public static LocString LIGHTING = "Lighting";

				// Token: 0x0400C61F RID: 50719
				public static LocString CLOTHING = "{0}'s Outfit";

				// Token: 0x0400C620 RID: 50720
				public static LocString CLOTHING_TRAIT_DECORUP = "{0}'s Outfit (Innately Stylish)";

				// Token: 0x0400C621 RID: 50721
				public static LocString CLOTHING_TRAIT_DECORDOWN = "{0}'s Outfit (Shabby Dresser)";

				// Token: 0x0400C622 RID: 50722
				public static LocString HOVERTITLE = "DECOR";

				// Token: 0x0400C623 RID: 50723
				public static LocString MAXIMUM_DECOR = "{0}{1} (Maximum Decor)";

				// Token: 0x02003A82 RID: 14978
				public class TOOLTIPS
				{
					// Token: 0x0400E887 RID: 59527
					public static LocString LOWDECOR = string.Concat(new string[]
					{
						"<b>Negative Decor</b>\nArea with insufficient ",
						UI.PRE_KEYWORD,
						"Decor",
						UI.PST_KEYWORD,
						" values\n* Resources on the floor are considered \"debris\" and will decrease decor"
					});

					// Token: 0x0400E888 RID: 59528
					public static LocString HIGHDECOR = string.Concat(new string[]
					{
						"<b>Positive Decor</b>\nArea with sufficient ",
						UI.PRE_KEYWORD,
						"Decor",
						UI.PST_KEYWORD,
						" values\n* Lighting and aesthetically pleasing buildings increase decor"
					});
				}
			}

			// Token: 0x02002DCA RID: 11722
			public class PRIORITIES
			{
				// Token: 0x0400C624 RID: 50724
				public static LocString NAME = "PRIORITY OVERLAY";

				// Token: 0x0400C625 RID: 50725
				public static LocString BUTTON = "Priority Overlay";

				// Token: 0x0400C626 RID: 50726
				public static LocString ONE = "1 (Low Urgency)";

				// Token: 0x0400C627 RID: 50727
				public static LocString ONE_TOOLTIP = "Priority 1";

				// Token: 0x0400C628 RID: 50728
				public static LocString TWO = "2";

				// Token: 0x0400C629 RID: 50729
				public static LocString TWO_TOOLTIP = "Priority 2";

				// Token: 0x0400C62A RID: 50730
				public static LocString THREE = "3";

				// Token: 0x0400C62B RID: 50731
				public static LocString THREE_TOOLTIP = "Priority 3";

				// Token: 0x0400C62C RID: 50732
				public static LocString FOUR = "4";

				// Token: 0x0400C62D RID: 50733
				public static LocString FOUR_TOOLTIP = "Priority 4";

				// Token: 0x0400C62E RID: 50734
				public static LocString FIVE = "5";

				// Token: 0x0400C62F RID: 50735
				public static LocString FIVE_TOOLTIP = "Priority 5";

				// Token: 0x0400C630 RID: 50736
				public static LocString SIX = "6";

				// Token: 0x0400C631 RID: 50737
				public static LocString SIX_TOOLTIP = "Priority 6";

				// Token: 0x0400C632 RID: 50738
				public static LocString SEVEN = "7";

				// Token: 0x0400C633 RID: 50739
				public static LocString SEVEN_TOOLTIP = "Priority 7";

				// Token: 0x0400C634 RID: 50740
				public static LocString EIGHT = "8";

				// Token: 0x0400C635 RID: 50741
				public static LocString EIGHT_TOOLTIP = "Priority 8";

				// Token: 0x0400C636 RID: 50742
				public static LocString NINE = "9 (High Urgency)";

				// Token: 0x0400C637 RID: 50743
				public static LocString NINE_TOOLTIP = "Priority 9";
			}

			// Token: 0x02002DCB RID: 11723
			public class DISEASE
			{
				// Token: 0x0400C638 RID: 50744
				public static LocString NAME = "GERM OVERLAY";

				// Token: 0x0400C639 RID: 50745
				public static LocString BUTTON = "Germ Overlay";

				// Token: 0x0400C63A RID: 50746
				public static LocString HOVERTITLE = "Germ";

				// Token: 0x0400C63B RID: 50747
				public static LocString INFECTION_SOURCE = "Germ Source";

				// Token: 0x0400C63C RID: 50748
				public static LocString INFECTION_SOURCE_TOOLTIP = "<b>Germ Source</b>\nAreas where germs are produced\n•  Placing Wash Basins or Hand Sanitizers near these areas may prevent disease spread";

				// Token: 0x0400C63D RID: 50749
				public static LocString NO_DISEASE = "Zero surface germs";

				// Token: 0x0400C63E RID: 50750
				public static LocString DISEASE_NAME_FORMAT = "{0}<color=#{1}></color>";

				// Token: 0x0400C63F RID: 50751
				public static LocString DISEASE_NAME_FORMAT_NO_COLOR = "{0}";

				// Token: 0x0400C640 RID: 50752
				public static LocString DISEASE_FORMAT = "{1} [{0}]<color=#{2}></color>";

				// Token: 0x0400C641 RID: 50753
				public static LocString DISEASE_FORMAT_NO_COLOR = "{1} [{0}]";

				// Token: 0x0400C642 RID: 50754
				public static LocString CONTAINER_FORMAT = "\n    {0}: {1}";

				// Token: 0x02003A83 RID: 14979
				public class DISINFECT_THRESHOLD_DIAGRAM
				{
					// Token: 0x0400E889 RID: 59529
					public static LocString UNITS = "Germs";

					// Token: 0x0400E88A RID: 59530
					public static LocString MIN_LABEL = "0";

					// Token: 0x0400E88B RID: 59531
					public static LocString MAX_LABEL = "1m";

					// Token: 0x0400E88C RID: 59532
					public static LocString THRESHOLD_PREFIX = "Disinfect At:";

					// Token: 0x0400E88D RID: 59533
					public static LocString TOOLTIP = "Automatically disinfect any building with more than {NumberOfGerms} germs.";

					// Token: 0x0400E88E RID: 59534
					public static LocString TOOLTIP_DISABLED = "Automatic building disinfection disabled.";
				}
			}

			// Token: 0x02002DCC RID: 11724
			public class CROPS
			{
				// Token: 0x0400C643 RID: 50755
				public static LocString NAME = "FARMING OVERLAY";

				// Token: 0x0400C644 RID: 50756
				public static LocString BUTTON = "Farming Overlay";
			}

			// Token: 0x02002DCD RID: 11725
			public class POWER
			{
				// Token: 0x0400C645 RID: 50757
				public static LocString WATTS_GENERATED = "Watts Generated";

				// Token: 0x0400C646 RID: 50758
				public static LocString WATTS_CONSUMED = "Watts Consumed";
			}

			// Token: 0x02002DCE RID: 11726
			public class RADIATION
			{
				// Token: 0x0400C647 RID: 50759
				public static LocString NAME = "RADIATION";

				// Token: 0x0400C648 RID: 50760
				public static LocString BUTTON = "Radiation Overlay";

				// Token: 0x0400C649 RID: 50761
				public static LocString DESC = "{rads} per cycle ({description})";

				// Token: 0x0400C64A RID: 50762
				public static LocString SHIELDING_DESC = "Radiation Blocking: {radiationAbsorptionFactor}";

				// Token: 0x0400C64B RID: 50763
				public static LocString HOVERTITLE = "RADIATION";

				// Token: 0x02003A84 RID: 14980
				public class RANGES
				{
					// Token: 0x0400E88F RID: 59535
					public static LocString NONE = "Completely Safe";

					// Token: 0x0400E890 RID: 59536
					public static LocString VERY_LOW = "Mostly Safe";

					// Token: 0x0400E891 RID: 59537
					public static LocString LOW = "Barely Safe";

					// Token: 0x0400E892 RID: 59538
					public static LocString MEDIUM = "Slight Hazard";

					// Token: 0x0400E893 RID: 59539
					public static LocString HIGH = "Significant Hazard";

					// Token: 0x0400E894 RID: 59540
					public static LocString VERY_HIGH = "Extreme Hazard";

					// Token: 0x0400E895 RID: 59541
					public static LocString MAX = "Maximum Hazard";

					// Token: 0x0400E896 RID: 59542
					public static LocString INPUTPORT = "Radbolt Input Port";

					// Token: 0x0400E897 RID: 59543
					public static LocString OUTPUTPORT = "Radbolt Output Port";
				}

				// Token: 0x02003A85 RID: 14981
				public class TOOLTIPS
				{
					// Token: 0x0400E898 RID: 59544
					public static LocString NONE = "Completely Safe";

					// Token: 0x0400E899 RID: 59545
					public static LocString VERY_LOW = "Mostly Safe";

					// Token: 0x0400E89A RID: 59546
					public static LocString LOW = "Barely Safe";

					// Token: 0x0400E89B RID: 59547
					public static LocString MEDIUM = "Slight Hazard";

					// Token: 0x0400E89C RID: 59548
					public static LocString HIGH = "Significant Hazard";

					// Token: 0x0400E89D RID: 59549
					public static LocString VERY_HIGH = "Extreme Hazard";

					// Token: 0x0400E89E RID: 59550
					public static LocString MAX = "Maximum Hazard";

					// Token: 0x0400E89F RID: 59551
					public static LocString INPUTPORT = "Radbolt Input Port";

					// Token: 0x0400E8A0 RID: 59552
					public static LocString OUTPUTPORT = "Radbolt Output Port";
				}
			}
		}

		// Token: 0x020023D3 RID: 9171
		public class TABLESCREENS
		{
			// Token: 0x0400A138 RID: 41272
			public static LocString DUPLICANT_PROPERNAME = "<b>{0}</b>";

			// Token: 0x0400A139 RID: 41273
			public static LocString SELECT_DUPLICANT_BUTTON = UI.CLICK(UI.ClickType.Click) + " to select <b>{0}</b>";

			// Token: 0x0400A13A RID: 41274
			public static LocString GOTO_DUPLICANT_BUTTON = "Double-" + UI.CLICK(UI.ClickType.click) + " to go to <b>{0}</b>";

			// Token: 0x0400A13B RID: 41275
			public static LocString COLUMN_SORT_BY_NAME = "Sort by <b>Name</b>";

			// Token: 0x0400A13C RID: 41276
			public static LocString COLUMN_SORT_BY_STRESS = "Sort by <b>Stress</b>";

			// Token: 0x0400A13D RID: 41277
			public static LocString COLUMN_SORT_BY_HITPOINTS = "Sort by <b>Health</b>";

			// Token: 0x0400A13E RID: 41278
			public static LocString COLUMN_SORT_BY_SICKNESSES = "Sort by <b>Disease</b>";

			// Token: 0x0400A13F RID: 41279
			public static LocString COLUMN_SORT_BY_FULLNESS = "Sort by <b>Fullness</b>";

			// Token: 0x0400A140 RID: 41280
			public static LocString COLUMN_SORT_BY_EATEN_TODAY = "Sort by number of <b>Calories</b> consumed today";

			// Token: 0x0400A141 RID: 41281
			public static LocString COLUMN_SORT_BY_EXPECTATIONS = "Sort by <b>Morale</b>";

			// Token: 0x0400A142 RID: 41282
			public static LocString COLUMN_SORT_BY_POWERBANKS = "Sort by <b>Power Banks</b>";

			// Token: 0x0400A143 RID: 41283
			public static LocString NA = "N/A";

			// Token: 0x0400A144 RID: 41284
			public static LocString INFORMATION_NOT_AVAILABLE_TOOLTIP = "Information is not available because {1} is in {0}";

			// Token: 0x0400A145 RID: 41285
			public static LocString NOBODY_HERE = "Nobody here...";
		}

		// Token: 0x020023D4 RID: 9172
		public class CONSUMABLESSCREEN
		{
			// Token: 0x0400A146 RID: 41286
			public static LocString TITLE = "CONSUMABLES";

			// Token: 0x0400A147 RID: 41287
			public static LocString TOOLTIP_TOGGLE_ALL = "Toggle <b>all</b> food permissions <b>colonywide</b>";

			// Token: 0x0400A148 RID: 41288
			public static LocString TOOLTIP_TOGGLE_COLUMN = "Toggle colonywide <b>{0}</b> permission";

			// Token: 0x0400A149 RID: 41289
			public static LocString TOOLTIP_TOGGLE_ROW = "Toggle <b>all consumable permissions</b> for <b>{0}</b>";

			// Token: 0x0400A14A RID: 41290
			public static LocString NEW_MINIONS_TOOLTIP_TOGGLE_ROW = "Toggle <b>all consumable permissions</b> for <b>New Duplicants</b>";

			// Token: 0x0400A14B RID: 41291
			public static LocString NEW_MINIONS_FOOD_PERMISSION_ON = string.Concat(new string[]
			{
				"<b>New Duplicants</b> are <b>allowed</b> to eat \n",
				UI.PRE_KEYWORD,
				"{0}",
				UI.PST_KEYWORD,
				"</b> by default"
			});

			// Token: 0x0400A14C RID: 41292
			public static LocString NEW_MINIONS_FOOD_PERMISSION_OFF = string.Concat(new string[]
			{
				"<b>New Duplicants</b> are <b>not allowed</b> to eat \n",
				UI.PRE_KEYWORD,
				"{0}",
				UI.PST_KEYWORD,
				" by default"
			});

			// Token: 0x0400A14D RID: 41293
			public static LocString FOOD_PERMISSION_ON = "<b>{0}</b> is <b>allowed</b> to eat " + UI.PRE_KEYWORD + "{1}" + UI.PST_KEYWORD;

			// Token: 0x0400A14E RID: 41294
			public static LocString FOOD_PERMISSION_OFF = "<b>{0}</b> is <b>not allowed</b> to eat " + UI.PRE_KEYWORD + "{1}" + UI.PST_KEYWORD;

			// Token: 0x0400A14F RID: 41295
			public static LocString FOOD_CANT_CONSUME = "<b>{0}</b> <b>physically cannot</b> eat\n" + UI.PRE_KEYWORD + "{1}" + UI.PST_KEYWORD;

			// Token: 0x0400A150 RID: 41296
			public static LocString FOOD_REFUSE = "<b>{0}</b> <b>refuses</b> to eat\n" + UI.PRE_KEYWORD + "{1}" + UI.PST_KEYWORD;

			// Token: 0x0400A151 RID: 41297
			public static LocString FOOD_AVAILABLE = "Available: {0}";

			// Token: 0x0400A152 RID: 41298
			public static LocString FOOD_MORALE = UI.PRE_KEYWORD + "Morale" + UI.PST_KEYWORD + ": {0}";

			// Token: 0x0400A153 RID: 41299
			public static LocString FOOD_QUALITY = UI.PRE_KEYWORD + "Quality" + UI.PST_KEYWORD + ": {0}";

			// Token: 0x0400A154 RID: 41300
			public static LocString FOOD_QUALITY_VS_EXPECTATION = string.Concat(new string[]
			{
				"\nThis food will give ",
				UI.PRE_KEYWORD,
				"Morale",
				UI.PST_KEYWORD,
				" <b>{0}</b> if {1} eats it"
			});

			// Token: 0x0400A155 RID: 41301
			public static LocString CANNOT_ADJUST_PERMISSIONS = "Cannot adjust consumable permissions because they're in {0}";
		}

		// Token: 0x020023D5 RID: 9173
		public class JOBSSCREEN
		{
			// Token: 0x0400A156 RID: 41302
			public static LocString TITLE = "MANAGE DUPLICANT PRIORITIES";

			// Token: 0x0400A157 RID: 41303
			public static LocString TOOLTIP_TOGGLE_ALL = "Set priority of all Errand Types colonywide";

			// Token: 0x0400A158 RID: 41304
			public static LocString HEADER_TOOLTIP = string.Concat(new string[]
			{
				"<size=16>{Job} Errand Type</size>\n\n{Details}\n\nDuplicants will first choose what ",
				UI.PRE_KEYWORD,
				"Errand Type",
				UI.PST_KEYWORD,
				" to perform based on ",
				UI.PRE_KEYWORD,
				"Duplicant Priorities",
				UI.PST_KEYWORD,
				",\nthen they will choose individual tasks within that type using ",
				UI.PRE_KEYWORD,
				"Building Priorities",
				UI.PST_KEYWORD,
				" set by the ",
				UI.FormatAsLink("Priority Tool", "PRIORITIES"),
				" ",
				UI.FormatAsHotKey(global::Action.ManagePriorities)
			});

			// Token: 0x0400A159 RID: 41305
			public static LocString HEADER_DETAILS_TOOLTIP = "{Description}\n\nAffected errands: {ChoreList}";

			// Token: 0x0400A15A RID: 41306
			public static LocString HEADER_CHANGE_TOOLTIP = string.Concat(new string[]
			{
				"Set the priority for the ",
				UI.PRE_KEYWORD,
				"{Job}",
				UI.PST_KEYWORD,
				" Errand Type colonywide\n"
			});

			// Token: 0x0400A15B RID: 41307
			public static LocString NEW_MINION_ITEM_TOOLTIP = string.Concat(new string[]
			{
				"The ",
				UI.PRE_KEYWORD,
				"{Job}",
				UI.PST_KEYWORD,
				" Errand Type is automatically a {Priority} ",
				UI.PRE_KEYWORD,
				"Priority",
				UI.PST_KEYWORD,
				" for <b>Arriving Duplicants</b>"
			});

			// Token: 0x0400A15C RID: 41308
			public static LocString ITEM_TOOLTIP = UI.PRE_KEYWORD + "{Job}" + UI.PST_KEYWORD + " Priority for {Name}:\n<b>{Priority} Priority ({PriorityValue})</b>";

			// Token: 0x0400A15D RID: 41309
			public static LocString MINION_SKILL_TOOLTIP = string.Concat(new string[]
			{
				"{Name}'s ",
				UI.PRE_KEYWORD,
				"{Attribute}",
				UI.PST_KEYWORD,
				" Skill: "
			});

			// Token: 0x0400A15E RID: 41310
			public static LocString TRAIT_DISABLED = string.Concat(new string[]
			{
				"{Name} possesses the ",
				UI.PRE_KEYWORD,
				"{Trait}",
				UI.PST_KEYWORD,
				" trait and <b>cannot</b> do ",
				UI.PRE_KEYWORD,
				"{Job}",
				UI.PST_KEYWORD,
				" Errands"
			});

			// Token: 0x0400A15F RID: 41311
			public static LocString INCREASE_ROW_PRIORITY_NEW_MINION_TOOLTIP = string.Concat(new string[]
			{
				"Prioritize ",
				UI.PRE_KEYWORD,
				"All Errands",
				UI.PST_KEYWORD,
				" for <b>New Duplicants</b>"
			});

			// Token: 0x0400A160 RID: 41312
			public static LocString DECREASE_ROW_PRIORITY_NEW_MINION_TOOLTIP = string.Concat(new string[]
			{
				"Deprioritize ",
				UI.PRE_KEYWORD,
				"All Errands",
				UI.PST_KEYWORD,
				" for <b>New Duplicants</b>"
			});

			// Token: 0x0400A161 RID: 41313
			public static LocString INCREASE_ROW_PRIORITY_MINION_TOOLTIP = string.Concat(new string[]
			{
				"Prioritize ",
				UI.PRE_KEYWORD,
				"All Errands",
				UI.PST_KEYWORD,
				" for <b>{Name}</b>"
			});

			// Token: 0x0400A162 RID: 41314
			public static LocString DECREASE_ROW_PRIORITY_MINION_TOOLTIP = string.Concat(new string[]
			{
				"Deprioritize ",
				UI.PRE_KEYWORD,
				"All Errands",
				UI.PST_KEYWORD,
				" for <b>{Name}</b>"
			});

			// Token: 0x0400A163 RID: 41315
			public static LocString INCREASE_PRIORITY_TUTORIAL = "{Hotkey} Increase Priority";

			// Token: 0x0400A164 RID: 41316
			public static LocString DECREASE_PRIORITY_TUTORIAL = "{Hotkey} Decrease Priority";

			// Token: 0x0400A165 RID: 41317
			public static LocString CANNOT_ADJUST_PRIORITY = string.Concat(new string[]
			{
				"Priorities for ",
				UI.PRE_KEYWORD,
				"{0}",
				UI.PST_KEYWORD,
				" cannot be adjusted currently because they're in {1}"
			});

			// Token: 0x0400A166 RID: 41318
			public static LocString SORT_TOOLTIP = string.Concat(new string[]
			{
				"Sort by the ",
				UI.PRE_KEYWORD,
				"{Job}",
				UI.PST_KEYWORD,
				" Errand Type"
			});

			// Token: 0x0400A167 RID: 41319
			public static LocString DISABLED_TOOLTIP = string.Concat(new string[]
			{
				"{Name} may not perform ",
				UI.PRE_KEYWORD,
				"{Job}",
				UI.PST_KEYWORD,
				" Errands"
			});

			// Token: 0x0400A168 RID: 41320
			public static LocString OPTIONS = "Options";

			// Token: 0x0400A169 RID: 41321
			public static LocString TOGGLE_ADVANCED_MODE = "Enable Proximity";

			// Token: 0x0400A16A RID: 41322
			public static LocString TOGGLE_ADVANCED_MODE_TOOLTIP = "<b>Errand Proximity Settings</b>\n\nEnabling Proximity settings tells my Duplicants to always choose the closest, most urgent errand to perform.\n\nWhen disabled, Duplicants will choose between two high priority errands based on a hidden priority hierarchy instead.\n\nEnabling Proximity helps cut down on travel time in areas with lots of high priority errands, and is useful for large colonies.";

			// Token: 0x0400A16B RID: 41323
			public static LocString RESET_SETTINGS = "Reset Priorities";

			// Token: 0x0400A16C RID: 41324
			public static LocString RESET_SETTINGS_TOOLTIP = "<b>Reset Priorities</b>\n\nReturns all priorities to their default values.\n\nProximity Enabled: Priorities will be adjusted high-to-low.\n\nProximity Disabled: All priorities will be reset to neutral.";

			// Token: 0x02002DCF RID: 11727
			public class PRIORITY
			{
				// Token: 0x0400C64C RID: 50764
				public static LocString VERYHIGH = "Very High";

				// Token: 0x0400C64D RID: 50765
				public static LocString HIGH = "High";

				// Token: 0x0400C64E RID: 50766
				public static LocString STANDARD = "Standard";

				// Token: 0x0400C64F RID: 50767
				public static LocString LOW = "Low";

				// Token: 0x0400C650 RID: 50768
				public static LocString VERYLOW = "Very Low";

				// Token: 0x0400C651 RID: 50769
				public static LocString DISABLED = "Disallowed";
			}

			// Token: 0x02002DD0 RID: 11728
			public class PRIORITY_CLASS
			{
				// Token: 0x0400C652 RID: 50770
				public static LocString IDLE = "Idle";

				// Token: 0x0400C653 RID: 50771
				public static LocString BASIC = "Normal";

				// Token: 0x0400C654 RID: 50772
				public static LocString HIGH = "Urgent";

				// Token: 0x0400C655 RID: 50773
				public static LocString PERSONAL_NEEDS = "Personal Needs";

				// Token: 0x0400C656 RID: 50774
				public static LocString EMERGENCY = "Emergency";

				// Token: 0x0400C657 RID: 50775
				public static LocString COMPULSORY = "Involuntary";
			}
		}

		// Token: 0x020023D6 RID: 9174
		public class VITALSSCREEN
		{
			// Token: 0x0400A16D RID: 41325
			public static LocString HEALTH = "Health";

			// Token: 0x0400A16E RID: 41326
			public static LocString SICKNESS = "Diseases";

			// Token: 0x0400A16F RID: 41327
			public static LocString NO_SICKNESSES = "No diseases";

			// Token: 0x0400A170 RID: 41328
			public static LocString MULTIPLE_SICKNESSES = "Multiple diseases ({0})";

			// Token: 0x0400A171 RID: 41329
			public static LocString SICKNESS_REMAINING = "{0}\n({1})";

			// Token: 0x0400A172 RID: 41330
			public static LocString STRESS = "Stress";

			// Token: 0x0400A173 RID: 41331
			public static LocString EXPECTATIONS = "Expectations";

			// Token: 0x0400A174 RID: 41332
			public static LocString CALORIES = "Fullness";

			// Token: 0x0400A175 RID: 41333
			public static LocString EATEN_TODAY = "Eaten Today";

			// Token: 0x0400A176 RID: 41334
			public static LocString EATEN_TODAY_TOOLTIP = "Consumed {0} of food this cycle";

			// Token: 0x0400A177 RID: 41335
			public static LocString ATMOSPHERE_CONDITION = "Atmosphere:";

			// Token: 0x0400A178 RID: 41336
			public static LocString SUBMERSION = "Liquid Level";

			// Token: 0x0400A179 RID: 41337
			public static LocString NOT_DROWNING = "Liquid Level";

			// Token: 0x0400A17A RID: 41338
			public static LocString FOOD_EXPECTATIONS = "Food Expectation";

			// Token: 0x0400A17B RID: 41339
			public static LocString FOOD_EXPECTATIONS_TOOLTIP = "This Duplicant desires food that is {0} quality or better";

			// Token: 0x0400A17C RID: 41340
			public static LocString DECOR_EXPECTATIONS = "Decor Expectation";

			// Token: 0x0400A17D RID: 41341
			public static LocString DECOR_EXPECTATIONS_TOOLTIP = "This Duplicant desires decor that is {0} or higher";

			// Token: 0x0400A17E RID: 41342
			public static LocString QUALITYOFLIFE_EXPECTATIONS = "Morale";

			// Token: 0x0400A17F RID: 41343
			public static LocString QUALITYOFLIFE_EXPECTATIONS_TOOLTIP = "This Duplicant requires " + UI.FormatAsLink("{0} Morale", "MORALE") + ".\n\nCurrent Morale:";

			// Token: 0x0400A180 RID: 41344
			public static LocString POLLINATION = "Pollination";

			// Token: 0x02002DD1 RID: 11729
			public class CONDITIONS_GROWING
			{
				// Token: 0x02003A86 RID: 14982
				public class WILD
				{
					// Token: 0x0400E8A1 RID: 59553
					public static LocString BASE = "<b>Wild Growth\n[Life Cycle: {0}]</b>";

					// Token: 0x0400E8A2 RID: 59554
					public static LocString TOOLTIP = "This plant will take {0} to grow in the wild";
				}

				// Token: 0x02003A87 RID: 14983
				public class DOMESTIC
				{
					// Token: 0x0400E8A3 RID: 59555
					public static LocString BASE = "<b>Domestic Growth\n[Life Cycle: {0}]</b>";

					// Token: 0x0400E8A4 RID: 59556
					public static LocString TOOLTIP = "This plant will take {0} to grow domestically";
				}

				// Token: 0x02003A88 RID: 14984
				public class ADDITIONAL_DOMESTIC
				{
					// Token: 0x0400E8A5 RID: 59557
					public static LocString BASE = "<b>Additional Domestic Growth\n[Life Cycle: {0}]</b>";

					// Token: 0x0400E8A6 RID: 59558
					public static LocString TOOLTIP = "This plant will take {0} to grow domestically";
				}

				// Token: 0x02003A89 RID: 14985
				public class WILD_DECOR
				{
					// Token: 0x0400E8A7 RID: 59559
					public static LocString BASE = "<b>Wild Growth</b>";

					// Token: 0x0400E8A8 RID: 59560
					public static LocString TOOLTIP = "This plant must have these requirements met to grow in the wild";
				}

				// Token: 0x02003A8A RID: 14986
				public class WILD_INSTANT
				{
					// Token: 0x0400E8A9 RID: 59561
					public static LocString BASE = "<b>Wild Growth\n[{0}% Throughput]</b>";

					// Token: 0x0400E8AA RID: 59562
					public static LocString TOOLTIP = "This plant must have these requirements met to grow in the wild";
				}

				// Token: 0x02003A8B RID: 14987
				public class ADDITIONAL_DOMESTIC_INSTANT
				{
					// Token: 0x0400E8AB RID: 59563
					public static LocString BASE = "<b>Domestic Growth\n[{0}% Throughput]</b>";

					// Token: 0x0400E8AC RID: 59564
					public static LocString TOOLTIP = "This plant must have these requirements met to grow domestically";
				}
			}
		}

		// Token: 0x020023D7 RID: 9175
		public class SCHEDULESCREEN
		{
			// Token: 0x0400A181 RID: 41345
			public static LocString SCHEDULE_EDITOR = "SCHEDULE EDITOR";

			// Token: 0x0400A182 RID: 41346
			public static LocString SCHEDULE_NAME_DEFAULT = "Default Standard Schedule";

			// Token: 0x0400A183 RID: 41347
			public static LocString SCHEDULE_NAME_NEW = "New Schedule";

			// Token: 0x0400A184 RID: 41348
			public static LocString SCHEDULE_NAME_FORMAT = "Schedule {0}";

			// Token: 0x0400A185 RID: 41349
			public static LocString SCHEDULE_NAME_DEFAULT_BIONIC = "Default Bionic Schedule";

			// Token: 0x0400A186 RID: 41350
			public static LocString SCHEDULE_DROPDOWN_ASSIGNED = "{0} (Assigned)";

			// Token: 0x0400A187 RID: 41351
			public static LocString SCHEDULE_DROPDOWN_BLANK = "<i>Move Duplicant...</i>";

			// Token: 0x0400A188 RID: 41352
			public static LocString SCHEDULE_DOWNTIME_MORALE = "Duplicants will receive {0} Morale from the scheduled Downtime shifts";

			// Token: 0x0400A189 RID: 41353
			public static LocString RENAME_BUTTON_TOOLTIP = "Rename custom schedule";

			// Token: 0x0400A18A RID: 41354
			public static LocString ALARM_BUTTON_ON_TOOLTIP = "Toggle Notifications\n\nSounds and notifications will play when shifts change for this schedule.\n\nENABLED\n" + UI.CLICK(UI.ClickType.Click) + " to disable";

			// Token: 0x0400A18B RID: 41355
			public static LocString ALARM_BUTTON_OFF_TOOLTIP = "Toggle Notifications\n\nNo sounds or notifications will play for this schedule.\n\nDISABLED\n" + UI.CLICK(UI.ClickType.Click) + " to enable";

			// Token: 0x0400A18C RID: 41356
			public static LocString DELETE_BUTTON_TOOLTIP = "Delete Schedule";

			// Token: 0x0400A18D RID: 41357
			public static LocString PAINT_TOOLS = "Paint Tools:";

			// Token: 0x0400A18E RID: 41358
			public static LocString ADD_SCHEDULE = "Add New Schedule";

			// Token: 0x0400A18F RID: 41359
			public static LocString POO = "dar";

			// Token: 0x0400A190 RID: 41360
			public static LocString DOWNTIME_MORALE = "Downtime Morale: {0}";

			// Token: 0x0400A191 RID: 41361
			public static LocString ALARM_TITLE_ENABLED = "Alarm On";

			// Token: 0x0400A192 RID: 41362
			public static LocString ALARM_TITLE_DISABLED = "Alarm Off";

			// Token: 0x0400A193 RID: 41363
			public static LocString SETTINGS = "Settings";

			// Token: 0x0400A194 RID: 41364
			public static LocString ALARM_BUTTON = "Shift Alarms";

			// Token: 0x0400A195 RID: 41365
			public static LocString RESET_SETTINGS = "Reset Shifts";

			// Token: 0x0400A196 RID: 41366
			public static LocString RESET_SETTINGS_TOOLTIP = "Restore this schedule to default shifts";

			// Token: 0x0400A197 RID: 41367
			public static LocString DELETE_SCHEDULE = "Delete Schedule";

			// Token: 0x0400A198 RID: 41368
			public static LocString DELETE_SCHEDULE_TOOLTIP = "Remove this schedule and unassign all Duplicants from it";

			// Token: 0x0400A199 RID: 41369
			public static LocString DUPLICANT_NIGHTOWL_TOOLTIP = string.Concat(new string[]
			{
				DUPLICANTS.TRAITS.NIGHTOWL.NAME,
				"\n• All ",
				UI.PRE_KEYWORD,
				"Attributes",
				UI.PST_KEYWORD,
				" <b>+3</b> at night"
			});

			// Token: 0x0400A19A RID: 41370
			public static LocString DUPLICANT_EARLYBIRD_TOOLTIP = string.Concat(new string[]
			{
				DUPLICANTS.TRAITS.EARLYBIRD.NAME,
				"\n• All ",
				UI.PRE_KEYWORD,
				"Attributes",
				UI.PST_KEYWORD,
				" <b>+2</b> in the morning"
			});

			// Token: 0x0400A19B RID: 41371
			public static LocString SHIFT_SCHEDULE_LEFT_TOOLTIP = "Shift all schedule blocks left";

			// Token: 0x0400A19C RID: 41372
			public static LocString SHIFT_SCHEDULE_RIGHT_TOOLTIP = "Shift all schedule blocks right";

			// Token: 0x0400A19D RID: 41373
			public static LocString SHIFT_SCHEDULE_UP_TOOLTIP = "Swap this row with the one above it";

			// Token: 0x0400A19E RID: 41374
			public static LocString SHIFT_SCHEDULE_DOWN_TOOLTIP = "Swap this row with the one below it";

			// Token: 0x0400A19F RID: 41375
			public static LocString DUPLICATE_SCHEDULE_TIMETABLE = "Duplicate this row";

			// Token: 0x0400A1A0 RID: 41376
			public static LocString DELETE_SCHEDULE_TIMETABLE = "Delete this row\n\nSchedules must have two or more rows in order for one row to be deleted";

			// Token: 0x0400A1A1 RID: 41377
			public static LocString DUPLICATE_SCHEDULE = "Duplicate this schedule";
		}

		// Token: 0x020023D8 RID: 9176
		public class COLONYLOSTSCREEN
		{
			// Token: 0x0400A1A2 RID: 41378
			public static LocString COLONYLOST = "COLONY LOST";

			// Token: 0x0400A1A3 RID: 41379
			public static LocString COLONYLOSTDESCRIPTION = "All Duplicants are dead or incapacitated.";

			// Token: 0x0400A1A4 RID: 41380
			public static LocString RESTARTPROMPT = "Press <color=#F44A47><b>[ESC]</b></color> to return to a previous colony, or begin a new one.";

			// Token: 0x0400A1A5 RID: 41381
			public static LocString DISMISSBUTTON = "DISMISS";

			// Token: 0x0400A1A6 RID: 41382
			public static LocString QUITBUTTON = "MAIN MENU";
		}

		// Token: 0x020023D9 RID: 9177
		public class VICTORYSCREEN
		{
			// Token: 0x0400A1A7 RID: 41383
			public static LocString HEADER = "SUCCESS: IMPERATIVE ACHIEVED!";

			// Token: 0x0400A1A8 RID: 41384
			public static LocString DESCRIPTION = "I have fulfilled the conditions of one of my Hardwired Imperatives";

			// Token: 0x0400A1A9 RID: 41385
			public static LocString RESTARTPROMPT = "Press <color=#F44A47><b>[ESC]</b></color> to retire the colony and begin anew.";

			// Token: 0x0400A1AA RID: 41386
			public static LocString DISMISSBUTTON = "DISMISS";

			// Token: 0x0400A1AB RID: 41387
			public static LocString RETIREBUTTON = "RETIRE COLONY";
		}

		// Token: 0x020023DA RID: 9178
		public class GENESHUFFLERMESSAGE
		{
			// Token: 0x0400A1AC RID: 41388
			public static LocString HEADER = "NEURAL VACILLATION COMPLETE";

			// Token: 0x0400A1AD RID: 41389
			public static LocString BODY_SUCCESS = "Whew! <b>{0}'s</b> brain is still vibrating, but they've never felt better!\n\n<b>{0}</b> acquired the <b>{1}</b> trait.\n\n<b>{1}:</b>\n{2}";

			// Token: 0x0400A1AE RID: 41390
			public static LocString BODY_FAILURE = "The machine attempted to alter this Duplicant, but there's no improving on perfection.\n\n<b>{0}</b> already has all positive traits!";

			// Token: 0x0400A1AF RID: 41391
			public static LocString DISMISSBUTTON = "DISMISS";
		}

		// Token: 0x020023DB RID: 9179
		public class CRASHSCREEN
		{
			// Token: 0x0400A1B0 RID: 41392
			public static LocString TITLE = "\"Whoops! We're sorry, but it seems your game has encountered an error. It's okay though - these errors are how we find and fix problems to make our game more fun for everyone. If you use the box below to submit a crash report to us, we can use this information to get the issue sorted out.\"";

			// Token: 0x0400A1B1 RID: 41393
			public static LocString TITLE_MODS = "\"Oops-a-daisy! We're sorry, but it seems your game has encountered an error. If you uncheck all of the mods below, we will be able to help the next time this happens. Any mods that could be related to this error have already been unchecked.\"";

			// Token: 0x0400A1B2 RID: 41394
			public static LocString HEADER = "OPTIONAL CRASH DESCRIPTION";

			// Token: 0x0400A1B3 RID: 41395
			public static LocString HEADER_MODS = "ACTIVE MODS";

			// Token: 0x0400A1B4 RID: 41396
			public static LocString BODY = "Help! A black hole ate my game!";

			// Token: 0x0400A1B5 RID: 41397
			public static LocString THANKYOU = "Thank you!\n\nYou're making our game better, one crash at a time.";

			// Token: 0x0400A1B6 RID: 41398
			public static LocString UPLOAD_FAILED = "There was an issue in reporting this crash.\n\nPlease submit a bug report at:\n<u>https://forums.kleientertainment.com/klei-bug-tracker/oni/</u>";

			// Token: 0x0400A1B7 RID: 41399
			public static LocString UPLOADINFO = "UPLOAD ADDITIONAL INFO ({0})";

			// Token: 0x0400A1B8 RID: 41400
			public static LocString REPORTBUTTON = "REPORT CRASH";

			// Token: 0x0400A1B9 RID: 41401
			public static LocString REPORTING = "REPORTING, PLEASE WAIT...";

			// Token: 0x0400A1BA RID: 41402
			public static LocString CONTINUEBUTTON = "CONTINUE GAME";

			// Token: 0x0400A1BB RID: 41403
			public static LocString MOREINFOBUTTON = "MORE INFO";

			// Token: 0x0400A1BC RID: 41404
			public static LocString COPYTOCLIPBOARDBUTTON = "COPY TO CLIPBOARD";

			// Token: 0x0400A1BD RID: 41405
			public static LocString QUITBUTTON = "QUIT TO DESKTOP";

			// Token: 0x0400A1BE RID: 41406
			public static LocString SAVEFAILED = "Save Failed: {0}";

			// Token: 0x0400A1BF RID: 41407
			public static LocString LOADFAILED = "Load Failed: {0}\nSave Version: {1}\nExpected: {2}";

			// Token: 0x0400A1C0 RID: 41408
			public static LocString REPORTEDERROR_SUCCESS = "Thank you for reporting this error.";

			// Token: 0x0400A1C1 RID: 41409
			public static LocString REPORTEDERROR_FAILURE_TOO_LARGE = "Unable to report error. Save file is too large. Please contact us using the bug tracker.";

			// Token: 0x0400A1C2 RID: 41410
			public static LocString REPORTEDERROR_FAILURE = "Unable to report error. Please contact us using the bug tracker.";

			// Token: 0x0400A1C3 RID: 41411
			public static LocString UPLOADINPROGRESS = "Submitting {0}";
		}

		// Token: 0x020023DC RID: 9180
		public class DEMOOVERSCREEN
		{
			// Token: 0x0400A1C4 RID: 41412
			public static LocString TIMEREMAINING = "Demo time remaining:";

			// Token: 0x0400A1C5 RID: 41413
			public static LocString TIMERTOOLTIP = "Demo time remaining";

			// Token: 0x0400A1C6 RID: 41414
			public static LocString TIMERINACTIVE = "Timer inactive";

			// Token: 0x0400A1C7 RID: 41415
			public static LocString DEMOOVER = "END OF DEMO";

			// Token: 0x0400A1C8 RID: 41416
			public static LocString DESCRIPTION = "Thank you for playing <color=#F44A47>Oxygen Not Included</color>!";

			// Token: 0x0400A1C9 RID: 41417
			public static LocString DESCRIPTION_2 = "";

			// Token: 0x0400A1CA RID: 41418
			public static LocString QUITBUTTON = "RESET";
		}

		// Token: 0x020023DD RID: 9181
		public class CREDITSSCREEN
		{
			// Token: 0x0400A1CB RID: 41419
			public static LocString TITLE = "CREDITS";

			// Token: 0x0400A1CC RID: 41420
			public static LocString CLOSEBUTTON = "CLOSE";

			// Token: 0x02002DD2 RID: 11730
			public class THIRD_PARTY
			{
				// Token: 0x0400C658 RID: 50776
				public static LocString FMOD = "FMOD Sound System\nCopyright Firelight Technologies";

				// Token: 0x0400C659 RID: 50777
				public static LocString HARMONY = "Harmony by Andreas Pardeike";
			}
		}

		// Token: 0x020023DE RID: 9182
		public class ALLRESOURCESSCREEN
		{
			// Token: 0x0400A1CD RID: 41421
			public static LocString RESOURCES_TITLE = "RESOURCES";

			// Token: 0x0400A1CE RID: 41422
			public static LocString RESOURCES = "Resources";

			// Token: 0x0400A1CF RID: 41423
			public static LocString SEARCH = "Search";

			// Token: 0x0400A1D0 RID: 41424
			public static LocString NAME = "Resource";

			// Token: 0x0400A1D1 RID: 41425
			public static LocString TOTAL = "Total";

			// Token: 0x0400A1D2 RID: 41426
			public static LocString AVAILABLE = "Available";

			// Token: 0x0400A1D3 RID: 41427
			public static LocString RESERVED = "Reserved";

			// Token: 0x0400A1D4 RID: 41428
			public static LocString SEARCH_PLACEHODLER = "Enter text...";

			// Token: 0x0400A1D5 RID: 41429
			public static LocString FIRST_FRAME_NO_DATA = "...";

			// Token: 0x0400A1D6 RID: 41430
			public static LocString PIN_TOOLTIP = "Check to pin resource to side panel";

			// Token: 0x0400A1D7 RID: 41431
			public static LocString UNPIN_TOOLTIP = "Unpin resource";
		}

		// Token: 0x020023DF RID: 9183
		public class PRIORITYSCREEN
		{
			// Token: 0x0400A1D8 RID: 41432
			public static LocString BASIC = "Set the order in which specific pending errands should be done\n\n1: Least Urgent\n9: Most Urgent";

			// Token: 0x0400A1D9 RID: 41433
			public static LocString HIGH = "";

			// Token: 0x0400A1DA RID: 41434
			public static LocString TOP_PRIORITY = "Top Priority\n\nThis priority will override all other priorities and set the colony on Yellow Alert until the errand is completed";

			// Token: 0x0400A1DB RID: 41435
			public static LocString HIGH_TOGGLE = "";

			// Token: 0x0400A1DC RID: 41436
			public static LocString OPEN_JOBS_SCREEN = string.Concat(new string[]
			{
				UI.CLICK(UI.ClickType.Click),
				" to open the Priorities Screen\n\nDuplicants will first decide what to work on based on their ",
				UI.PRE_KEYWORD,
				"Duplicant Priorities",
				UI.PST_KEYWORD,
				", and then decide where to work based on ",
				UI.PRE_KEYWORD,
				"Building Priorities",
				UI.PST_KEYWORD
			});

			// Token: 0x0400A1DD RID: 41437
			public static LocString DIAGRAM = string.Concat(new string[]
			{
				"Duplicants will first choose what ",
				UI.PRE_KEYWORD,
				"Errand Type",
				UI.PST_KEYWORD,
				" to perform using their ",
				UI.PRE_KEYWORD,
				"Duplicant Priorities",
				UI.PST_KEYWORD,
				" ",
				UI.FormatAsHotKey(global::Action.ManagePriorities),
				"\n\nThey will then choose one ",
				UI.PRE_KEYWORD,
				"Errand",
				UI.PST_KEYWORD,
				" from within that type using the ",
				UI.PRE_KEYWORD,
				"Building Priorities",
				UI.PST_KEYWORD,
				" set by this tool"
			});

			// Token: 0x0400A1DE RID: 41438
			public static LocString DIAGRAM_TITLE = "BUILDING PRIORITY";
		}

		// Token: 0x020023E0 RID: 9184
		public class RESOURCESCREEN
		{
			// Token: 0x0400A1DF RID: 41439
			public static LocString HEADER = "RESOURCES";

			// Token: 0x0400A1E0 RID: 41440
			public static LocString CATEGORY_TOOLTIP = "Counts all unallocated resources within reach\n\n" + UI.CLICK(UI.ClickType.Click) + " to expand";

			// Token: 0x0400A1E1 RID: 41441
			public static LocString AVAILABLE_TOOLTIP = "Available: <b>{0}</b>\n({1} of {2} allocated to pending errands)";

			// Token: 0x0400A1E2 RID: 41442
			public static LocString TREND_TOOLTIP = "The available amount of this resource has {0} {1} in the last cycle";

			// Token: 0x0400A1E3 RID: 41443
			public static LocString TREND_TOOLTIP_NO_CHANGE = "The available amount of this resource has NOT CHANGED in the last cycle";

			// Token: 0x0400A1E4 RID: 41444
			public static LocString FLAT_STR = "<b>NOT CHANGED</b>";

			// Token: 0x0400A1E5 RID: 41445
			public static LocString INCREASING_STR = "<color=" + Constants.POSITIVE_COLOR_STR + ">INCREASED</color>";

			// Token: 0x0400A1E6 RID: 41446
			public static LocString DECREASING_STR = "<color=" + Constants.NEGATIVE_COLOR_STR + ">DECREASED</color>";

			// Token: 0x0400A1E7 RID: 41447
			public static LocString CLEAR_NEW_RESOURCES = "Clear New";

			// Token: 0x0400A1E8 RID: 41448
			public static LocString CLEAR_ALL = "Unpin all resources";

			// Token: 0x0400A1E9 RID: 41449
			public static LocString SEE_ALL = "+ See All ({0})";

			// Token: 0x0400A1EA RID: 41450
			public static LocString NEW_TAG = "NEW";
		}

		// Token: 0x020023E1 RID: 9185
		public class CONFIRMDIALOG
		{
			// Token: 0x0400A1EB RID: 41451
			public static LocString OK = "OK";

			// Token: 0x0400A1EC RID: 41452
			public static LocString CANCEL = "CANCEL";

			// Token: 0x0400A1ED RID: 41453
			public static LocString DIALOG_HEADER = "MESSAGE";
		}

		// Token: 0x020023E2 RID: 9186
		public class FACADE_SELECTION_PANEL
		{
			// Token: 0x0400A1EE RID: 41454
			public static LocString HEADER = "Select Blueprint";

			// Token: 0x0400A1EF RID: 41455
			public static LocString STORE_BUTTON_TOOLTIP = "See more Blueprints in the Supply Closet";
		}

		// Token: 0x020023E3 RID: 9187
		public class FILE_NAME_DIALOG
		{
			// Token: 0x0400A1F0 RID: 41456
			public static LocString ENTER_TEXT = "Enter Text...";
		}

		// Token: 0x020023E4 RID: 9188
		public class MINION_IDENTITY_SORT
		{
			// Token: 0x0400A1F1 RID: 41457
			public static LocString TITLE = "Sort By";

			// Token: 0x0400A1F2 RID: 41458
			public static LocString NAME = "Duplicant";

			// Token: 0x0400A1F3 RID: 41459
			public static LocString ROLE = "Role";

			// Token: 0x0400A1F4 RID: 41460
			public static LocString PERMISSION = "Permission";
		}

		// Token: 0x020023E5 RID: 9189
		public class UISIDESCREENS
		{
			// Token: 0x02002DD3 RID: 11731
			public class TABS
			{
				// Token: 0x0400C65A RID: 50778
				public static LocString HEADER = "Options";

				// Token: 0x0400C65B RID: 50779
				public static LocString CONFIGURATION = "Config";

				// Token: 0x0400C65C RID: 50780
				public static LocString MATERIAL = "Material";

				// Token: 0x0400C65D RID: 50781
				public static LocString SKIN = "Blueprint";
			}

			// Token: 0x02002DD4 RID: 11732
			public class BLUEPRINT_TAB
			{
				// Token: 0x0400C65E RID: 50782
				public static LocString EDIT_OUTFIT_BUTTON = "Restyle";

				// Token: 0x0400C65F RID: 50783
				public static LocString SUBCATEGORY_OUTFIT = "Clothing";

				// Token: 0x0400C660 RID: 50784
				public static LocString SUBCATEGORY_ATMOSUIT = "Atmo Suit";

				// Token: 0x0400C661 RID: 50785
				public static LocString SUBCATEGORY_JOYRESPONSE = "Overjoyed";
			}

			// Token: 0x02002DD5 RID: 11733
			public class NOCONFIG
			{
				// Token: 0x0400C662 RID: 50786
				public static LocString TITLE = "No configuration";

				// Token: 0x0400C663 RID: 50787
				public static LocString LABEL = "There is no configuration available for this object.";
			}

			// Token: 0x02002DD6 RID: 11734
			public class ARTABLESELECTIONSIDESCREEN
			{
				// Token: 0x0400C664 RID: 50788
				public static LocString TITLE = "Style Selection";

				// Token: 0x0400C665 RID: 50789
				public static LocString BUTTON = "Redecorate";

				// Token: 0x0400C666 RID: 50790
				public static LocString BUTTON_TOOLTIP = "Clears current artwork\n\nCreates errand for a skilled Duplicant to create selected style";

				// Token: 0x0400C667 RID: 50791
				public static LocString CLEAR_BUTTON_TOOLTIP = "Clears current artwork\n\nAllows a skilled Duplicant to create artwork of their choice";
			}

			// Token: 0x02002DD7 RID: 11735
			public class ARTIFACTANALYSISSIDESCREEN
			{
				// Token: 0x0400C668 RID: 50792
				public static LocString NO_ARTIFACTS_DISCOVERED = "No artifacts analyzed";

				// Token: 0x0400C669 RID: 50793
				public static LocString NO_ARTIFACTS_DISCOVERED_TOOLTIP = "Analyzing artifacts requires a Duplicant with the Masterworks skill";
			}

			// Token: 0x02002DD8 RID: 11736
			public class BUTTONMENUSIDESCREEN
			{
				// Token: 0x0400C66A RID: 50794
				public static LocString TITLE = "Building Menu";

				// Token: 0x0400C66B RID: 50795
				public static LocString ALLOW_INTERNAL_CONSTRUCTOR = "Enable Auto-Delivery";

				// Token: 0x0400C66C RID: 50796
				public static LocString ALLOW_INTERNAL_CONSTRUCTOR_TOOLTIP = "Order Duplicants to deliver {0}" + UI.FormatAsLink("s", "NONE") + " to this building automatically when they need replacing";

				// Token: 0x0400C66D RID: 50797
				public static LocString DISALLOW_INTERNAL_CONSTRUCTOR = "Cancel Auto-Delivery";

				// Token: 0x0400C66E RID: 50798
				public static LocString DISALLOW_INTERNAL_CONSTRUCTOR_TOOLTIP = "Cancel automatic {0} deliveries to this building";
			}

			// Token: 0x02002DD9 RID: 11737
			public class CONFIGURECONSUMERSIDESCREEN
			{
				// Token: 0x0400C66F RID: 50799
				public static LocString TITLE = "Configure Building";

				// Token: 0x0400C670 RID: 50800
				public static LocString SELECTION_DESCRIPTION_HEADER = "Description";
			}

			// Token: 0x02002DDA RID: 11738
			public class TREEFILTERABLESIDESCREEN
			{
				// Token: 0x0400C671 RID: 50801
				public static LocString TITLE = "Element Filter";

				// Token: 0x0400C672 RID: 50802
				public static LocString TITLE_CRITTER = "Critter Filter";

				// Token: 0x0400C673 RID: 50803
				public static LocString ALLBUTTON = "All Standard";

				// Token: 0x0400C674 RID: 50804
				public static LocString ALLBUTTONTOOLTIP = "Allow storage of all standard resources preferred by this building\n\nNon-standard resources must be selected manually\n\nNon-standard resources include:\n    • Clothing\n    • Critter Eggs\n    • Sublimators";

				// Token: 0x0400C675 RID: 50805
				public static LocString ALLBUTTON_EDIBLES = "All Edibles";

				// Token: 0x0400C676 RID: 50806
				public static LocString ALLBUTTON_EDIBLES_TOOLTIP = "Allow storage of all edible resources";

				// Token: 0x0400C677 RID: 50807
				public static LocString ALLBUTTON_CRITTERS = "All Critters";

				// Token: 0x0400C678 RID: 50808
				public static LocString ALLBUTTON_CRITTERS_TOOLTIP = "Allow storage of all eligible " + UI.PRE_KEYWORD + "Critters" + UI.PST_KEYWORD;

				// Token: 0x0400C679 RID: 50809
				public static LocString SPECIAL_RESOURCES = "Non-Standard";

				// Token: 0x0400C67A RID: 50810
				public static LocString SPECIAL_RESOURCES_TOOLTIP = "These objects may not be ideally suited to storage";

				// Token: 0x0400C67B RID: 50811
				public static LocString CATEGORYBUTTONTOOLTIP = "Allow storage of anything in the {0} resource category";

				// Token: 0x0400C67C RID: 50812
				public static LocString MATERIALBUTTONTOOLTIP = "Add or remove this material from storage";

				// Token: 0x0400C67D RID: 50813
				public static LocString ONLYALLOWTRANSPORTITEMSBUTTON = "Sweep Only";

				// Token: 0x0400C67E RID: 50814
				public static LocString ONLYALLOWTRANSPORTITEMSBUTTONTOOLTIP = "Only store objects marked Sweep <color=#F44A47><b>[K]</b></color> in this container";

				// Token: 0x0400C67F RID: 50815
				public static LocString ONLYALLOWSPICEDITEMSBUTTON = "Spiced Food Only";

				// Token: 0x0400C680 RID: 50816
				public static LocString ONLYALLOWSPICEDITEMSBUTTONTOOLTIP = "Only store foods that have been spiced at the " + UI.PRE_KEYWORD + "Spice Grinder" + UI.PST_KEYWORD;

				// Token: 0x0400C681 RID: 50817
				public static LocString SEARCH_PLACEHOLDER = "Search";
			}

			// Token: 0x02002DDB RID: 11739
			public class TELESCOPESIDESCREEN
			{
				// Token: 0x0400C682 RID: 50818
				public static LocString TITLE = "Telescope Configuration";

				// Token: 0x0400C683 RID: 50819
				public static LocString NO_SELECTED_ANALYSIS_TARGET = "No analysis focus selected\nOpen the " + UI.FormatAsManagementMenu("Starmap", global::Action.ManageStarmap) + " to select a focus";

				// Token: 0x0400C684 RID: 50820
				public static LocString ANALYSIS_TARGET_SELECTED = "Object focus selected\nAnalysis underway";

				// Token: 0x0400C685 RID: 50821
				public static LocString OPENSTARMAPBUTTON = "OPEN STARMAP";

				// Token: 0x0400C686 RID: 50822
				public static LocString ANALYSIS_TARGET_HEADER = "Object Analysis";
			}

			// Token: 0x02002DDC RID: 11740
			public class CLUSTERTELESCOPESIDESCREEN
			{
				// Token: 0x0400C687 RID: 50823
				public static LocString TITLE = "Telescope Configuration";

				// Token: 0x0400C688 RID: 50824
				public static LocString CHECKBOX_METEORS = "Allow meteor shower identification";

				// Token: 0x0400C689 RID: 50825
				public static LocString CHECKBOX_TOOLTIP_METEORS = string.Concat(new string[]
				{
					"Prioritizes unidentified meteors that come within range in a previously revealed location\n\nWill interrupt a Duplicant working on revealing a new ",
					UI.PRE_KEYWORD,
					"Starmap",
					UI.PST_KEYWORD,
					" location"
				});
			}

			// Token: 0x02002DDD RID: 11741
			public class TEMPORALTEARSIDESCREEN
			{
				// Token: 0x0400C68A RID: 50826
				public static LocString TITLE = "Temporal Tear";

				// Token: 0x0400C68B RID: 50827
				public static LocString BUTTON_OPEN = "Enter Tear";

				// Token: 0x0400C68C RID: 50828
				public static LocString BUTTON_CLOSED = "Tear Closed";

				// Token: 0x0400C68D RID: 50829
				public static LocString BUTTON_LABEL = "Enter Temporal Tear";

				// Token: 0x0400C68E RID: 50830
				public static LocString CONFIRM_POPUP_MESSAGE = "Are you sure you want to fire this?";

				// Token: 0x0400C68F RID: 50831
				public static LocString CONFIRM_POPUP_CONFIRM = "Yes, I'm ready for a meteor shower.";

				// Token: 0x0400C690 RID: 50832
				public static LocString CONFIRM_POPUP_CANCEL = "No, I need more time to prepare.";

				// Token: 0x0400C691 RID: 50833
				public static LocString CONFIRM_POPUP_TITLE = "Temporal Tear Opener";
			}

			// Token: 0x02002DDE RID: 11742
			public class RAILGUNSIDESCREEN
			{
				// Token: 0x0400C692 RID: 50834
				public static LocString TITLE = "Launcher Configuration";

				// Token: 0x0400C693 RID: 50835
				public static LocString NO_SELECTED_LAUNCH_TARGET = "No destination selected\nOpen the " + UI.FormatAsManagementMenu("Starmap", global::Action.ManageStarmap) + " to set a course";

				// Token: 0x0400C694 RID: 50836
				public static LocString LAUNCH_TARGET_SELECTED = "Launcher destination {0} set";

				// Token: 0x0400C695 RID: 50837
				public static LocString OPENSTARMAPBUTTON = "OPEN STARMAP";

				// Token: 0x0400C696 RID: 50838
				public static LocString LAUNCH_RESOURCES_HEADER = "Launch Resources:";

				// Token: 0x0400C697 RID: 50839
				public static LocString MINIMUM_PAYLOAD_MASS = "Minimum launch mass:";
			}

			// Token: 0x02002DDF RID: 11743
			public class CLUSTERWORLDSIDESCREEN
			{
				// Token: 0x0400C698 RID: 50840
				public static LocString TITLE = UI.CLUSTERMAP.PLANETOID;

				// Token: 0x0400C699 RID: 50841
				public static LocString VIEW_WORLD = "Oversee " + UI.CLUSTERMAP.PLANETOID;

				// Token: 0x0400C69A RID: 50842
				public static LocString VIEW_WORLD_DISABLE_TOOLTIP = "Cannot view " + UI.CLUSTERMAP.PLANETOID;

				// Token: 0x0400C69B RID: 50843
				public static LocString VIEW_WORLD_TOOLTIP = "View this " + UI.CLUSTERMAP.PLANETOID + "'s surface";
			}

			// Token: 0x02002DE0 RID: 11744
			public class ROCKETMODULESIDESCREEN
			{
				// Token: 0x0400C69C RID: 50844
				public static LocString TITLE = "Rocket Module";

				// Token: 0x0400C69D RID: 50845
				public static LocString CHANGEMODULEPANEL = "Add or Change Module";

				// Token: 0x0400C69E RID: 50846
				public static LocString ENGINE_MAX_HEIGHT = "This engine allows a <b>Maximum Rocket Height</b> of {0}";

				// Token: 0x02003A8C RID: 14988
				public class MODULESTATCHANGE
				{
					// Token: 0x0400E8AD RID: 59565
					public static LocString TITLE = "Rocket stats on construction:";

					// Token: 0x0400E8AE RID: 59566
					public static LocString BURDEN = "    • " + DUPLICANTS.ATTRIBUTES.ROCKETBURDEN.NAME + ": {0} ({1})";

					// Token: 0x0400E8AF RID: 59567
					public static LocString RANGE = string.Concat(new string[]
					{
						"    • Potential ",
						DUPLICANTS.ATTRIBUTES.FUELRANGEPERKILOGRAM.NAME,
						": {0}/1",
						UI.UNITSUFFIXES.MASS.KILOGRAM,
						" Fuel ({1})"
					});

					// Token: 0x0400E8B0 RID: 59568
					public static LocString SPEED = "    • Speed: {0} ({1})";

					// Token: 0x0400E8B1 RID: 59569
					public static LocString ENGINEPOWER = "    • " + DUPLICANTS.ATTRIBUTES.ROCKETENGINEPOWER.NAME + ": {0} ({1})";

					// Token: 0x0400E8B2 RID: 59570
					public static LocString HEIGHT = "    • " + DUPLICANTS.ATTRIBUTES.HEIGHT.NAME + ": {0}/{2} ({1})";

					// Token: 0x0400E8B3 RID: 59571
					public static LocString HEIGHT_NOMAX = "    • " + DUPLICANTS.ATTRIBUTES.HEIGHT.NAME + ": {0} ({1})";

					// Token: 0x0400E8B4 RID: 59572
					public static LocString POSITIVEDELTA = UI.FormatAsPositiveModifier("{0}");

					// Token: 0x0400E8B5 RID: 59573
					public static LocString NEGATIVEDELTA = UI.FormatAsNegativeModifier("{0}");
				}

				// Token: 0x02003A8D RID: 14989
				public class BUTTONSWAPMODULEUP
				{
					// Token: 0x0400E8B6 RID: 59574
					public static LocString DESC = "Swap this rocket module with the one above";

					// Token: 0x0400E8B7 RID: 59575
					public static LocString INVALID = "No module above may be swapped.\n\n    • A module above may be unable to have modules placed above it.\n    • A module above may be unable to fit into the space below it.\n    • This module may be unable to fit into the space above it.";
				}

				// Token: 0x02003A8E RID: 14990
				public class BUTTONVIEWINTERIOR
				{
					// Token: 0x0400E8B8 RID: 59576
					public static LocString LABEL = "View Interior";

					// Token: 0x0400E8B9 RID: 59577
					public static LocString DESC = "What's goin' on in there?";

					// Token: 0x0400E8BA RID: 59578
					public static LocString INVALID = "This module does not have an interior view";
				}

				// Token: 0x02003A8F RID: 14991
				public class BUTTONVIEWEXTERIOR
				{
					// Token: 0x0400E8BB RID: 59579
					public static LocString LABEL = "View Exterior";

					// Token: 0x0400E8BC RID: 59580
					public static LocString DESC = "Switch to external world view";

					// Token: 0x0400E8BD RID: 59581
					public static LocString INVALID = "Not available in flight";
				}

				// Token: 0x02003A90 RID: 14992
				public class BUTTONSWAPMODULEDOWN
				{
					// Token: 0x0400E8BE RID: 59582
					public static LocString DESC = "Swap this rocket module with the one below";

					// Token: 0x0400E8BF RID: 59583
					public static LocString INVALID = "No module below may be swapped.\n\n    • A module below may be unable to have modules placed below it.\n    • A module below may be unable to fit into the space above it.\n    • This module may be unable to fit into the space below it.";
				}

				// Token: 0x02003A91 RID: 14993
				public class BUTTONCHANGEMODULE
				{
					// Token: 0x0400E8C0 RID: 59584
					public static LocString DESC = "Swap this module for a different module";

					// Token: 0x0400E8C1 RID: 59585
					public static LocString INVALID = "This module cannot be changed to a different type";
				}

				// Token: 0x02003A92 RID: 14994
				public class BUTTONREMOVEMODULE
				{
					// Token: 0x0400E8C2 RID: 59586
					public static LocString DESC = "Remove this module";

					// Token: 0x0400E8C3 RID: 59587
					public static LocString INVALID = "This module cannot be removed";
				}

				// Token: 0x02003A93 RID: 14995
				public class ADDMODULE
				{
					// Token: 0x0400E8C4 RID: 59588
					public static LocString DESC = "Add a new module above this one";

					// Token: 0x0400E8C5 RID: 59589
					public static LocString INVALID = "Modules cannot be added above this module, or there is no room above to add a module";
				}
			}

			// Token: 0x02002DE1 RID: 11745
			public class CLUSTERLOCATIONFILTERSIDESCREEN
			{
				// Token: 0x0400C69F RID: 50847
				public static LocString TITLE = "Location Filter";

				// Token: 0x0400C6A0 RID: 50848
				public static LocString HEADER = "Send Green signal at locations";

				// Token: 0x0400C6A1 RID: 50849
				public static LocString EMPTY_SPACE_ROW = "In Space";
			}

			// Token: 0x02002DE2 RID: 11746
			public class DISPENSERSIDESCREEN
			{
				// Token: 0x0400C6A2 RID: 50850
				public static LocString TITLE = "Dispenser";

				// Token: 0x0400C6A3 RID: 50851
				public static LocString BUTTON_CANCEL = "Cancel order";

				// Token: 0x0400C6A4 RID: 50852
				public static LocString BUTTON_DISPENSE = "Dispense item";
			}

			// Token: 0x02002DE3 RID: 11747
			public class ROCKETRESTRICTIONSIDESCREEN
			{
				// Token: 0x0400C6A5 RID: 50853
				public static LocString TITLE = "Rocket Restrictions";

				// Token: 0x0400C6A6 RID: 50854
				public static LocString BUILDING_RESTRICTIONS_LABEL = "Interior Building Restrictions";

				// Token: 0x0400C6A7 RID: 50855
				public static LocString NONE_RESTRICTION_BUTTON = "None";

				// Token: 0x0400C6A8 RID: 50856
				public static LocString NONE_RESTRICTION_BUTTON_TOOLTIP = "There are no restrictions on buildings inside this rocket";

				// Token: 0x0400C6A9 RID: 50857
				public static LocString GROUNDED_RESTRICTION_BUTTON = "Grounded";

				// Token: 0x0400C6AA RID: 50858
				public static LocString GROUNDED_RESTRICTION_BUTTON_TOOLTIP = "Buildings with their access restricted cannot be operated while grounded, though they can still be filled";

				// Token: 0x0400C6AB RID: 50859
				public static LocString AUTOMATION = "Automation Controlled";

				// Token: 0x0400C6AC RID: 50860
				public static LocString AUTOMATION_TOOLTIP = "Building restrictions are managed by automation\n\nBuildings with their access restricted cannot be operated, though they can still be filled";
			}

			// Token: 0x02002DE4 RID: 11748
			public class HABITATMODULESIDESCREEN
			{
				// Token: 0x0400C6AD RID: 50861
				public static LocString TITLE = "Spacefarer Module";

				// Token: 0x0400C6AE RID: 50862
				public static LocString VIEW_BUTTON = "View Interior";

				// Token: 0x0400C6AF RID: 50863
				public static LocString VIEW_BUTTON_TOOLTIP = "What's goin' on in there?";
			}

			// Token: 0x02002DE5 RID: 11749
			public class HARVESTMODULESIDESCREEN
			{
				// Token: 0x0400C6B0 RID: 50864
				public static LocString TITLE = "Resource Gathering";

				// Token: 0x0400C6B1 RID: 50865
				public static LocString MINING_IN_PROGRESS = "Drilling...";

				// Token: 0x0400C6B2 RID: 50866
				public static LocString MINING_STOPPED = "Not drilling";

				// Token: 0x0400C6B3 RID: 50867
				public static LocString ENABLE = "Enable Drill";

				// Token: 0x0400C6B4 RID: 50868
				public static LocString DISABLE = "Disable Drill";
			}

			// Token: 0x02002DE6 RID: 11750
			public class SELECTMODULESIDESCREEN
			{
				// Token: 0x0400C6B5 RID: 50869
				public static LocString TITLE = "Select Module";

				// Token: 0x0400C6B6 RID: 50870
				public static LocString BUILDBUTTON = "Build";

				// Token: 0x02003A94 RID: 14996
				public class CONSTRAINTS
				{
					// Token: 0x02003EF8 RID: 16120
					public class RESEARCHED
					{
						// Token: 0x0400F35A RID: 62298
						public static LocString COMPLETE = "Research Completed";

						// Token: 0x0400F35B RID: 62299
						public static LocString FAILED = "Research Incomplete";
					}

					// Token: 0x02003EF9 RID: 16121
					public class MATERIALS_AVAILABLE
					{
						// Token: 0x0400F35C RID: 62300
						public static LocString COMPLETE = "Materials available";

						// Token: 0x0400F35D RID: 62301
						public static LocString FAILED = "• Materials unavailable";
					}

					// Token: 0x02003EFA RID: 16122
					public class ONE_COMMAND_PER_ROCKET
					{
						// Token: 0x0400F35E RID: 62302
						public static LocString COMPLETE = "";

						// Token: 0x0400F35F RID: 62303
						public static LocString FAILED = "• Command module already installed";
					}

					// Token: 0x02003EFB RID: 16123
					public class ONE_ENGINE_PER_ROCKET
					{
						// Token: 0x0400F360 RID: 62304
						public static LocString COMPLETE = "";

						// Token: 0x0400F361 RID: 62305
						public static LocString FAILED = "• Engine module already installed";
					}

					// Token: 0x02003EFC RID: 16124
					public class ENGINE_AT_BOTTOM
					{
						// Token: 0x0400F362 RID: 62306
						public static LocString COMPLETE = "";

						// Token: 0x0400F363 RID: 62307
						public static LocString FAILED = "• Must install at bottom of rocket";
					}

					// Token: 0x02003EFD RID: 16125
					public class TOP_ONLY
					{
						// Token: 0x0400F364 RID: 62308
						public static LocString COMPLETE = "";

						// Token: 0x0400F365 RID: 62309
						public static LocString FAILED = "• Must install at top of rocket";
					}

					// Token: 0x02003EFE RID: 16126
					public class SPACE_AVAILABLE
					{
						// Token: 0x0400F366 RID: 62310
						public static LocString COMPLETE = "";

						// Token: 0x0400F367 RID: 62311
						public static LocString FAILED = "• Space above rocket blocked";
					}

					// Token: 0x02003EFF RID: 16127
					public class PASSENGER_MODULE_AVAILABLE
					{
						// Token: 0x0400F368 RID: 62312
						public static LocString COMPLETE = "";

						// Token: 0x0400F369 RID: 62313
						public static LocString FAILED = "• Max number of passenger modules installed";
					}

					// Token: 0x02003F00 RID: 16128
					public class MAX_MODULES
					{
						// Token: 0x0400F36A RID: 62314
						public static LocString COMPLETE = "";

						// Token: 0x0400F36B RID: 62315
						public static LocString FAILED = "• Max module limit of engine reached";
					}

					// Token: 0x02003F01 RID: 16129
					public class MAX_HEIGHT
					{
						// Token: 0x0400F36C RID: 62316
						public static LocString COMPLETE = "";

						// Token: 0x0400F36D RID: 62317
						public static LocString FAILED = "• Engine's height limit reached or exceeded";

						// Token: 0x0400F36E RID: 62318
						public static LocString FAILED_NO_ENGINE = "• Rocket requires space for an engine";
					}

					// Token: 0x02003F02 RID: 16130
					public class ONE_ROBOPILOT_PER_ROCKET
					{
						// Token: 0x0400F36F RID: 62319
						public static LocString COMPLETE = "";

						// Token: 0x0400F370 RID: 62320
						public static LocString FAILED = "• Robo-Pilot module already installed";
					}
				}
			}

			// Token: 0x02002DE7 RID: 11751
			public class FILTERSIDESCREEN
			{
				// Token: 0x0400C6B7 RID: 50871
				public static LocString TITLE = "Filter Outputs";

				// Token: 0x0400C6B8 RID: 50872
				public static LocString NO_SELECTION = "None";

				// Token: 0x0400C6B9 RID: 50873
				public static LocString OUTPUTELEMENTHEADER = "Output 1";

				// Token: 0x0400C6BA RID: 50874
				public static LocString SELECTELEMENTHEADER = "Output 2";

				// Token: 0x0400C6BB RID: 50875
				public static LocString OUTPUTRED = "Output Red";

				// Token: 0x0400C6BC RID: 50876
				public static LocString OUTPUTGREEN = "Output Green";

				// Token: 0x0400C6BD RID: 50877
				public static LocString NOELEMENTSELECTED = "No element selected";

				// Token: 0x0400C6BE RID: 50878
				public static LocString DRIEDFOOD = "Dried Food";

				// Token: 0x02003A95 RID: 14997
				public static class UNFILTEREDELEMENTS
				{
					// Token: 0x0400E8C6 RID: 59590
					public static LocString GAS = "Gas Output:\nAll";

					// Token: 0x0400E8C7 RID: 59591
					public static LocString LIQUID = "Liquid Output:\nAll";

					// Token: 0x0400E8C8 RID: 59592
					public static LocString SOLID = "Solid Output:\nAll";
				}

				// Token: 0x02003A96 RID: 14998
				public static class FILTEREDELEMENT
				{
					// Token: 0x0400E8C9 RID: 59593
					public static LocString GAS = "Filtered Gas Output:\n{0}";

					// Token: 0x0400E8CA RID: 59594
					public static LocString LIQUID = "Filtered Liquid Output:\n{0}";

					// Token: 0x0400E8CB RID: 59595
					public static LocString SOLID = "Filtered Solid Output:\n{0}";
				}
			}

			// Token: 0x02002DE8 RID: 11752
			public class SINGLEITEMSELECTIONSIDESCREEN
			{
				// Token: 0x0400C6BF RID: 50879
				public static LocString TITLE = "Element Filter";

				// Token: 0x0400C6C0 RID: 50880
				public static LocString LIST_TITLE = "Options";

				// Token: 0x0400C6C1 RID: 50881
				public static LocString NO_SELECTION = "None";

				// Token: 0x02003A97 RID: 14999
				public class CURRENT_ITEM_SELECTED_SECTION
				{
					// Token: 0x0400E8CC RID: 59596
					public static LocString TITLE = "Current Selection";

					// Token: 0x0400E8CD RID: 59597
					public static LocString NO_ITEM_TITLE = "No Item Selected";

					// Token: 0x0400E8CE RID: 59598
					public static LocString NO_ITEM_MESSAGE = "Select an item for storage below.";
				}
			}

			// Token: 0x02002DE9 RID: 11753
			public class FEWOPTIONSELECTIONSIDESCREEN
			{
				// Token: 0x0400C6C2 RID: 50882
				public static LocString TITLE = "Options";
			}

			// Token: 0x02002DEA RID: 11754
			public class MISSILESELECTIONSIDESCREEN
			{
				// Token: 0x0400C6C3 RID: 50883
				public static LocString TITLE = BUILDINGS.PREFABS.MISSILELAUNCHER.NAME;

				// Token: 0x0400C6C4 RID: 50884
				public static LocString HEADER = "Projectile Selection";

				// Token: 0x02003A98 RID: 15000
				public class VANILLALARGEIMPACTOR
				{
					// Token: 0x0400E8CF RID: 59599
					public static LocString HEALTH_BAR_TITLE = "Health";

					// Token: 0x0400E8D0 RID: 59600
					public static LocString HEALTH_BAR_TOOLTIP = "Demolior health: {0} / {1}";

					// Token: 0x0400E8D1 RID: 59601
					public static LocString TIME_UNTIL_COLLISION_TITLE = "Time Until Impact";

					// Token: 0x0400E8D2 RID: 59602
					public static LocString TIME_UNTIL_COLLISION_TOOLTIP = "{0} cycles remaining until impact";
				}
			}

			// Token: 0x02002DEB RID: 11755
			public class LOGICBROADCASTCHANNELSIDESCREEN
			{
				// Token: 0x0400C6C5 RID: 50885
				public static LocString TITLE = "Channel Selector";

				// Token: 0x0400C6C6 RID: 50886
				public static LocString HEADER = "Channel Selector";

				// Token: 0x0400C6C7 RID: 50887
				public static LocString IN_RANGE = "In Range";

				// Token: 0x0400C6C8 RID: 50888
				public static LocString OUT_OF_RANGE = "Out of Range";

				// Token: 0x0400C6C9 RID: 50889
				public static LocString NO_SENDERS = "No Channels Available";

				// Token: 0x0400C6CA RID: 50890
				public static LocString NO_SENDERS_DESC = "Build a " + BUILDINGS.PREFABS.LOGICINTERASTEROIDSENDER.NAME + " to transmit a signal.";
			}

			// Token: 0x02002DEC RID: 11756
			public class CONDITIONLISTSIDESCREEN
			{
				// Token: 0x0400C6CB RID: 50891
				public static LocString TITLE = "Condition List";
			}

			// Token: 0x02002DED RID: 11757
			public class FABRICATORSIDESCREEN
			{
				// Token: 0x0400C6CC RID: 50892
				public static LocString TITLE = "Production Orders";

				// Token: 0x0400C6CD RID: 50893
				public static LocString SUBTITLE = "Recipes";

				// Token: 0x0400C6CE RID: 50894
				public static LocString NORECIPEDISCOVERED = "No discovered recipes";

				// Token: 0x0400C6CF RID: 50895
				public static LocString NORECIPEDISCOVERED_BODY = "Discover new ingredients or research new technology to unlock some recipes.";

				// Token: 0x0400C6D0 RID: 50896
				public static LocString UNDISCOVERED_RECIPES = "The following recipes are not yet discovered.\nI must discover new ingredients or research new technology to unlock them:";

				// Token: 0x0400C6D1 RID: 50897
				public static LocString NORECIPESELECTED = "No recipe selected";

				// Token: 0x0400C6D2 RID: 50898
				public static LocString SELECTRECIPE = "Select a recipe to fabricate.";

				// Token: 0x0400C6D3 RID: 50899
				public static LocString COST = "<b>Ingredients:</b>\n";

				// Token: 0x0400C6D4 RID: 50900
				public static LocString RESULTREQUIREMENTS = "<b>Requirements:</b>";

				// Token: 0x0400C6D5 RID: 50901
				public static LocString RESULTEFFECTS = "<b>Effects:</b>";

				// Token: 0x0400C6D6 RID: 50902
				public static LocString KG = "- {0}: {1}\n";

				// Token: 0x0400C6D7 RID: 50903
				public static LocString INFORMATION = "INFORMATION";

				// Token: 0x0400C6D8 RID: 50904
				public static LocString CANCEL = "Cancel";

				// Token: 0x0400C6D9 RID: 50905
				public static LocString RECIPE_REQUIREMENT = "{0}: {1}";

				// Token: 0x0400C6DA RID: 50906
				public static LocString RECIPE_AVAILABLE = "Available: {0}";

				// Token: 0x0400C6DB RID: 50907
				public static LocString RECIPEPRODUCT = "{0}: {1}";

				// Token: 0x0400C6DC RID: 50908
				public static LocString UNITS_AND_CALS = "{0} [{1}]";

				// Token: 0x0400C6DD RID: 50909
				public static LocString CALS = "{0}";

				// Token: 0x0400C6DE RID: 50910
				public static LocString QUEUED_MISSING_INGREDIENTS_TOOLTIP = "Missing {0} of {1}\n";

				// Token: 0x0400C6DF RID: 50911
				public static LocString CURRENT_ORDER = "Current order: {0}";

				// Token: 0x0400C6E0 RID: 50912
				public static LocString NEXT_ORDER = "Next order: {0}";

				// Token: 0x0400C6E1 RID: 50913
				public static LocString NO_WORKABLE_ORDER = "No workable order";

				// Token: 0x0400C6E2 RID: 50914
				public static LocString RECIPE_DETAILS = "Recipe Details";

				// Token: 0x0400C6E3 RID: 50915
				public static LocString RECIPE_QUEUE = "Order Production Quantity: ";

				// Token: 0x0400C6E4 RID: 50916
				public static LocString RECIPE_QUEUE_CLICK_DESCRIPTION = "<b>" + UI.CLICK(UI.ClickType.Click) + " to select the next queued variant of this recipe</b>";

				// Token: 0x0400C6E5 RID: 50917
				public static LocString RECIPE_FOREVER = "Forever";

				// Token: 0x0400C6E6 RID: 50918
				public static LocString RECIPE_NONE = "No Orders Queued";

				// Token: 0x0400C6E7 RID: 50919
				public static LocString CHANGE_RECIPE_ARROW_LABEL = "Change recipe";

				// Token: 0x0400C6E8 RID: 50920
				public static LocString RECIPE_RESEARCH_REQUIRED = "Research Required";

				// Token: 0x0400C6E9 RID: 50921
				public static LocString RECIPE_UNDISCOVERED_INGREDIENTS = "Undiscovered ingredients";

				// Token: 0x0400C6EA RID: 50922
				public static LocString INGREDIENT_CATEGORY = "Ingredient #{0}";

				// Token: 0x0400C6EB RID: 50923
				public static LocString ADDITIONAL_REQUIREMENTS = "Additional Requirements";

				// Token: 0x0400C6EC RID: 50924
				public static LocString ADDITIONAL_REQUIREMENTS_TOOLTIP = "This recipe requires a supply of " + ITEMS.RADIATION.HIGHENERGYPARITCLE.NAME + "s to be collected by the building's input port.";

				// Token: 0x0400C6ED RID: 50925
				public static LocString NO_DISCOVERED_INGREDIENTS = "No ingredients discovered";

				// Token: 0x0400C6EE RID: 50926
				public static LocString UNDISCOVERED_INGREDIENTS_IN_CATEGORY = "Some ingredient options have not been discovered yet:\n\n{0}";

				// Token: 0x0400C6EF RID: 50927
				public static LocString ALL_INGREDIENTS_IN_CATEGORY_DISOVERED = "All ingredient options in this category have been discovered.";

				// Token: 0x0400C6F0 RID: 50928
				public static LocString INGREDIENTS = "<b>Ingredients:</b>";

				// Token: 0x0400C6F1 RID: 50929
				public static LocString RECIPE_EFFECTS = "<b>Effects:</b>";

				// Token: 0x0400C6F2 RID: 50930
				public static LocString RECIPE_EFFECTS_HEADER = "Effects";

				// Token: 0x0400C6F3 RID: 50931
				public static LocString ALLOW_MUTANT_SEED_INGREDIENTS = "Building accepts mutant seeds";

				// Token: 0x0400C6F4 RID: 50932
				public static LocString ALLOW_MUTANT_SEED_INGREDIENTS_TOOLTIP = "Toggle whether Duplicants will deliver mutant seed species to this building as recipe ingredients.";

				// Token: 0x02003A99 RID: 15001
				public class TOOLTIPS
				{
					// Token: 0x0400E8D3 RID: 59603
					public static LocString RECIPE_WORKTIME = "This recipe takes {0} to complete";

					// Token: 0x0400E8D4 RID: 59604
					public static LocString RECIPERQUIREMENT_SUFFICIENT = "This recipe consumes {1} of an available {2} of {0}";

					// Token: 0x0400E8D5 RID: 59605
					public static LocString RECIPERQUIREMENT_INSUFFICIENT = "This recipe requires {1} {0}\nAvailable: {2}";

					// Token: 0x0400E8D6 RID: 59606
					public static LocString RECIPEPRODUCT = "This recipe produces {1} {0}";

					// Token: 0x0400E8D7 RID: 59607
					public static LocString ADDITIONAL_INGREDIENT_OPTIONS_MESSAGE = UIConstants.ColorPrefixYellow + "Alternative ingredient options are available." + UIConstants.ColorSuffix;
				}

				// Token: 0x02003A9A RID: 15002
				public class EFFECTS
				{
					// Token: 0x0400E8D8 RID: 59608
					public static LocString OXYGEN_TANK = STRINGS.EQUIPMENT.PREFABS.OXYGEN_TANK.NAME + " ({0})";

					// Token: 0x0400E8D9 RID: 59609
					public static LocString OXYGEN_TANK_UNDERWATER = STRINGS.EQUIPMENT.PREFABS.OXYGEN_TANK_UNDERWATER.NAME + " ({0})";

					// Token: 0x0400E8DA RID: 59610
					public static LocString JETSUIT_TANK = STRINGS.EQUIPMENT.PREFABS.JET_SUIT.TANK_EFFECT_NAME + " ({0})";

					// Token: 0x0400E8DB RID: 59611
					public static LocString LEADSUIT_BATTERY = STRINGS.EQUIPMENT.PREFABS.LEAD_SUIT.BATTERY_EFFECT_NAME + " ({0})";

					// Token: 0x0400E8DC RID: 59612
					public static LocString COOL_VEST = STRINGS.EQUIPMENT.PREFABS.COOL_VEST.NAME + " ({0})";

					// Token: 0x0400E8DD RID: 59613
					public static LocString WARM_VEST = STRINGS.EQUIPMENT.PREFABS.WARM_VEST.NAME + " ({0})";

					// Token: 0x0400E8DE RID: 59614
					public static LocString FUNKY_VEST = STRINGS.EQUIPMENT.PREFABS.FUNKY_VEST.NAME + " ({0})";

					// Token: 0x0400E8DF RID: 59615
					public static LocString RESEARCHPOINT = "{0}: +1";
				}

				// Token: 0x02003A9B RID: 15003
				public class RECIPE_CATEGORIES
				{
					// Token: 0x0400E8E0 RID: 59616
					public static LocString ATMO_SUIT_FACADES = "Atmo Suit Styles";

					// Token: 0x0400E8E1 RID: 59617
					public static LocString JET_SUIT_FACADES = "Jet Suit Styles";

					// Token: 0x0400E8E2 RID: 59618
					public static LocString LEAD_SUIT_FACADES = "Lead Suit Styles";

					// Token: 0x0400E8E3 RID: 59619
					public static LocString PRIMO_GARB_FACADES = "Primo Garb Styles";
				}
			}

			// Token: 0x02002DEE RID: 11758
			public class ASSIGNMENTGROUPCONTROLLER
			{
				// Token: 0x0400C6F5 RID: 50933
				public static LocString TITLE = "Duplicant Assignment";

				// Token: 0x0400C6F6 RID: 50934
				public static LocString PILOT = "Pilot";

				// Token: 0x0400C6F7 RID: 50935
				public static LocString OFFWORLD = "Offworld";

				// Token: 0x02003A9C RID: 15004
				public class TOOLTIPS
				{
					// Token: 0x0400E8E4 RID: 59620
					public static LocString DIFFERENT_WORLD = "This Duplicant is on a different " + UI.CLUSTERMAP.PLANETOID;

					// Token: 0x0400E8E5 RID: 59621
					public static LocString ASSIGN = "<b>Add</b> this Duplicant to rocket crew";

					// Token: 0x0400E8E6 RID: 59622
					public static LocString UNASSIGN = "<b>Remove</b> this Duplicant from rocket crew";
				}
			}

			// Token: 0x02002DEF RID: 11759
			public class LAUNCHPADSIDESCREEN
			{
				// Token: 0x0400C6F8 RID: 50936
				public static LocString TITLE = "Rocket Platform";

				// Token: 0x0400C6F9 RID: 50937
				public static LocString WAITING_TO_LAND_PANEL = "Waiting to land";

				// Token: 0x0400C6FA RID: 50938
				public static LocString NO_ROCKETS_WAITING = "No rockets in orbit";

				// Token: 0x0400C6FB RID: 50939
				public static LocString IN_ORBIT_ABOVE_PANEL = "Rockets in orbit";

				// Token: 0x0400C6FC RID: 50940
				public static LocString NEW_ROCKET_BUTTON = "NEW ROCKET";

				// Token: 0x0400C6FD RID: 50941
				public static LocString LAND_BUTTON = "LAND HERE";

				// Token: 0x0400C6FE RID: 50942
				public static LocString CANCEL_LAND_BUTTON = "CANCEL";

				// Token: 0x0400C6FF RID: 50943
				public static LocString LAUNCH_BUTTON = "BEGIN LAUNCH SEQUENCE";

				// Token: 0x0400C700 RID: 50944
				public static LocString LAUNCH_BUTTON_DEBUG = "BEGIN LAUNCH SEQUENCE (DEBUG ENABLED)";

				// Token: 0x0400C701 RID: 50945
				public static LocString LAUNCH_BUTTON_TOOLTIP = "Blast off!";

				// Token: 0x0400C702 RID: 50946
				public static LocString LAUNCH_BUTTON_NOT_READY_TOOLTIP = "This rocket is <b>not</b> ready to launch\n\n<b>Review the Launch Checklist in the status panel for more detail</b>";

				// Token: 0x0400C703 RID: 50947
				public static LocString LAUNCH_WARNINGS_BUTTON = "ACKNOWLEDGE WARNINGS";

				// Token: 0x0400C704 RID: 50948
				public static LocString LAUNCH_WARNINGS_BUTTON_TOOLTIP = "Some items in the Launch Checklist require attention\n\n<b>" + UI.CLICK(UI.ClickType.Click) + " to ignore warnings and proceed with launch</b>";

				// Token: 0x0400C705 RID: 50949
				public static LocString LAUNCH_REQUESTED_BUTTON = "CANCEL LAUNCH";

				// Token: 0x0400C706 RID: 50950
				public static LocString LAUNCH_REQUESTED_BUTTON_TOOLTIP = "This rocket will take off as soon as a Duplicant takes the controls\n\n<b>" + UI.CLICK(UI.ClickType.Click) + " to cancel launch</b>";

				// Token: 0x0400C707 RID: 50951
				public static LocString LAUNCH_AUTOMATION_CONTROLLED = "AUTOMATION CONTROLLED";

				// Token: 0x0400C708 RID: 50952
				public static LocString LAUNCH_AUTOMATION_CONTROLLED_TOOLTIP = "This " + BUILDINGS.PREFABS.LAUNCHPAD.NAME + "'s launch operation is controlled by automation signals";

				// Token: 0x02003A9D RID: 15005
				public class STATUS
				{
					// Token: 0x0400E8E7 RID: 59623
					public static LocString STILL_PREPPING = "Launch Checklist Incomplete";

					// Token: 0x0400E8E8 RID: 59624
					public static LocString READY_FOR_LAUNCH = "Ready to Launch";

					// Token: 0x0400E8E9 RID: 59625
					public static LocString LOADING_CREW = "Loading crew...";

					// Token: 0x0400E8EA RID: 59626
					public static LocString UNLOADING_PASSENGERS = "Unloading non-crew...";

					// Token: 0x0400E8EB RID: 59627
					public static LocString WAITING_FOR_PILOT = "Pilot requested at control station...";

					// Token: 0x0400E8EC RID: 59628
					public static LocString COUNTING_DOWN = "5... 4... 3... 2... 1...";

					// Token: 0x0400E8ED RID: 59629
					public static LocString TAKING_OFF = "Liftoff!!";
				}
			}

			// Token: 0x02002DF0 RID: 11760
			public class AUTOPLUMBERSIDESCREEN
			{
				// Token: 0x0400C709 RID: 50953
				public static LocString TITLE = "Automatic Building Configuration";

				// Token: 0x02003A9E RID: 15006
				public class BUTTONS
				{
					// Token: 0x02003F03 RID: 16131
					public class POWER
					{
						// Token: 0x0400F371 RID: 62321
						public static LocString TOOLTIP = "Add Dev Generator and Electrical Wires";
					}

					// Token: 0x02003F04 RID: 16132
					public class PIPES
					{
						// Token: 0x0400F372 RID: 62322
						public static LocString TOOLTIP = "Add Dev Pumps and Pipes";
					}

					// Token: 0x02003F05 RID: 16133
					public class SOLIDS
					{
						// Token: 0x0400F373 RID: 62323
						public static LocString TOOLTIP = "Spawn solid resources for a relevant recipe or conversions";
					}

					// Token: 0x02003F06 RID: 16134
					public class MINION
					{
						// Token: 0x0400F374 RID: 62324
						public static LocString TOOLTIP = "Spawn a Duplicant in front of the building";
					}

					// Token: 0x02003F07 RID: 16135
					public class FACADE
					{
						// Token: 0x0400F375 RID: 62325
						public static LocString TOOLTIP = "Toggle the building blueprint";
					}
				}
			}

			// Token: 0x02002DF1 RID: 11761
			public class SELFDESTRUCTSIDESCREEN
			{
				// Token: 0x0400C70A RID: 50954
				public static LocString TITLE = "Self Destruct";

				// Token: 0x0400C70B RID: 50955
				public static LocString MESSAGE_TEXT = "EMERGENCY PROCEDURES";

				// Token: 0x0400C70C RID: 50956
				public static LocString BUTTON_TEXT = "ABANDON SHIP!";

				// Token: 0x0400C70D RID: 50957
				public static LocString BUTTON_TEXT_CONFIRM = "CONFIRM ABANDON SHIP";

				// Token: 0x0400C70E RID: 50958
				public static LocString BUTTON_TOOLTIP = "This rocket is equipped with an emergency escape system.\n\nThe rocket's self-destruct sequence can be triggered to destroy it and propel fragments of the ship towards the nearest planetoid.\n\nAny Duplicants on board will be safely delivered in escape pods.";

				// Token: 0x0400C70F RID: 50959
				public static LocString BUTTON_TOOLTIP_CONFIRM = "<b>This will eject any passengers and destroy the rocket.<b>\n\nThe rocket's self-destruct sequence can be triggered to destroy it and propel fragments of the ship towards the nearest planetoid.\n\nAny Duplicants on board will be safely delivered in escape pods.";
			}

			// Token: 0x02002DF2 RID: 11762
			public class GENESHUFFLERSIDESREEN
			{
				// Token: 0x0400C710 RID: 50960
				public static LocString TITLE = "Neural Vacillator";

				// Token: 0x0400C711 RID: 50961
				public static LocString COMPLETE = "Something feels different.";

				// Token: 0x0400C712 RID: 50962
				public static LocString UNDERWAY = "Neural Vacillation in progress.";

				// Token: 0x0400C713 RID: 50963
				public static LocString CONSUMED = "There are no charges left in this Vacillator.";

				// Token: 0x0400C714 RID: 50964
				public static LocString CONSUMED_WAITING = "Recharge requested, awaiting delivery by Duplicant.";

				// Token: 0x0400C715 RID: 50965
				public static LocString BUTTON = "Complete Neural Process";

				// Token: 0x0400C716 RID: 50966
				public static LocString BUTTON_RECHARGE = "Recharge";

				// Token: 0x0400C717 RID: 50967
				public static LocString BUTTON_RECHARGE_CANCEL = "Cancel Recharge";
			}

			// Token: 0x02002DF3 RID: 11763
			public class MINIONTODOSIDESCREEN
			{
				// Token: 0x0400C718 RID: 50968
				public static LocString NAME = "Errands";

				// Token: 0x0400C719 RID: 50969
				public static LocString TOOLTIP = "<b>Errands</b>\nView current and upcoming errands";

				// Token: 0x0400C71A RID: 50970
				public static LocString CURRENT_TITLE = "Current Errand";

				// Token: 0x0400C71B RID: 50971
				public static LocString LIST_TITLE = "Upcoming Errands";

				// Token: 0x0400C71C RID: 50972
				public static LocString CURRENT_SCHEDULE_BLOCK = "CURRENT SHIFT: {0}";

				// Token: 0x0400C71D RID: 50973
				public static LocString CHORE_TARGET = "{Target}";

				// Token: 0x0400C71E RID: 50974
				public static LocString CHORE_TARGET_AND_GROUP = "{Target} -- {Groups}";

				// Token: 0x0400C71F RID: 50975
				public static LocString SELF_LABEL = "Self";

				// Token: 0x0400C720 RID: 50976
				public static LocString TRUNCATED_CHORES = "{0} more";

				// Token: 0x0400C721 RID: 50977
				public static LocString TOOLTIP_IDLE = string.Concat(new string[]
				{
					"{IdleDescription}\n\nDuplicants will only <b>{Errand}</b> when there is nothing else for them to do\n\nTotal ",
					UI.PRE_KEYWORD,
					"Priority",
					UI.PST_KEYWORD,
					": {TotalPriority}\n    • ",
					UI.JOBSSCREEN.PRIORITY_CLASS.IDLE,
					": {ClassPriority}\n    • All {BestGroup} Errands: {TypePriority}"
				});

				// Token: 0x0400C722 RID: 50978
				public static LocString TOOLTIP_NORMAL = string.Concat(new string[]
				{
					"{Description}\n\nErrand Type: {Groups}\n\nTotal ",
					UI.PRE_KEYWORD,
					"Priority",
					UI.PST_KEYWORD,
					": {TotalPriority}\n    • {Name}'s {BestGroup} Priority: {PersonalPriorityValue} ({PersonalPriority})\n    • This {Building}'s Priority: {BuildingPriority}\n    • All {BestGroup} Errands: {TypePriority}"
				});

				// Token: 0x0400C723 RID: 50979
				public static LocString TOOLTIP_PERSONAL = string.Concat(new string[]
				{
					"{Description}\n\n<b>{Errand}</b> is a ",
					UI.JOBSSCREEN.PRIORITY_CLASS.PERSONAL_NEEDS,
					" errand and so will be performed before all Regular errands\n\nTotal ",
					UI.PRE_KEYWORD,
					"Priority",
					UI.PST_KEYWORD,
					": {TotalPriority}\n    • ",
					UI.JOBSSCREEN.PRIORITY_CLASS.PERSONAL_NEEDS,
					": {ClassPriority}\n    • All {BestGroup} Errands: {TypePriority}"
				});

				// Token: 0x0400C724 RID: 50980
				public static LocString TOOLTIP_EMERGENCY = string.Concat(new string[]
				{
					"{Description}\n\n<b>{Errand}</b> is an ",
					UI.JOBSSCREEN.PRIORITY_CLASS.EMERGENCY,
					" errand and so will be performed before all Regular and Personal errands\n\nTotal ",
					UI.PRE_KEYWORD,
					"Priority",
					UI.PST_KEYWORD,
					": {TotalPriority}\n    • ",
					UI.JOBSSCREEN.PRIORITY_CLASS.EMERGENCY,
					" : {ClassPriority}\n    • This {Building}'s Priority: {BuildingPriority}\n    • All {BestGroup} Errands: {TypePriority}"
				});

				// Token: 0x0400C725 RID: 50981
				public static LocString TOOLTIP_COMPULSORY = string.Concat(new string[]
				{
					"{Description}\n\n<b>{Errand}</b> is a ",
					UI.JOBSSCREEN.PRIORITY_CLASS.COMPULSORY,
					" action and so will occur immediately\n\nTotal ",
					UI.PRE_KEYWORD,
					"Priority",
					UI.PST_KEYWORD,
					": {TotalPriority}\n    • ",
					UI.JOBSSCREEN.PRIORITY_CLASS.COMPULSORY,
					": {ClassPriority}\n    • All {BestGroup} Errands: {TypePriority}"
				});

				// Token: 0x0400C726 RID: 50982
				public static LocString TOOLTIP_DESC_ACTIVE = "{Name}'s Current Errand: <b>{Errand}</b>";

				// Token: 0x0400C727 RID: 50983
				public static LocString TOOLTIP_DESC_INACTIVE = "{Name} could work on <b>{Errand}</b>, but it's not their top priority right now";

				// Token: 0x0400C728 RID: 50984
				public static LocString TOOLTIP_IDLEDESC_ACTIVE = "{Name} is currently <b>Idle</b>";

				// Token: 0x0400C729 RID: 50985
				public static LocString TOOLTIP_IDLEDESC_INACTIVE = "{Name} could become <b>Idle</b> when all other errands are canceled or completed";

				// Token: 0x0400C72A RID: 50986
				public static LocString TOOLTIP_NA = "--";

				// Token: 0x0400C72B RID: 50987
				public static LocString CHORE_GROUP_SEPARATOR = " or ";
			}

			// Token: 0x02002DF4 RID: 11764
			public class MODULEFLIGHTUTILITYSIDESCREEN
			{
				// Token: 0x0400C72C RID: 50988
				public static LocString TITLE = "Deployables";

				// Token: 0x0400C72D RID: 50989
				public static LocString DEPLOY_BUTTON = "Deploy";

				// Token: 0x0400C72E RID: 50990
				public static LocString DEPLOY_BUTTON_TOOLTIP = "Send this module's contents to the surface of the currently orbited " + UI.CLUSTERMAP.PLANETOID_KEYWORD + "\n\nA specific deploy location may need to be chosen for certain modules";

				// Token: 0x0400C72F RID: 50991
				public static LocString REPEAT_BUTTON_TOOLTIP = "Automatically deploy this module's contents when a destination orbit is reached";

				// Token: 0x0400C730 RID: 50992
				public static LocString SELECT_DUPLICANT = "Select Duplicant";

				// Token: 0x0400C731 RID: 50993
				public static LocString PILOT_FMT = "{0} - Pilot";
			}

			// Token: 0x02002DF5 RID: 11765
			public class HIGHENERGYPARTICLEDIRECTIONSIDESCREEN
			{
				// Token: 0x0400C732 RID: 50994
				public static LocString TITLE = "Emitting Particle Direction";

				// Token: 0x0400C733 RID: 50995
				public static LocString SELECTED_DIRECTION = "Selected direction: {0}";

				// Token: 0x0400C734 RID: 50996
				public static LocString DIRECTION_N = "N";

				// Token: 0x0400C735 RID: 50997
				public static LocString DIRECTION_NE = "NE";

				// Token: 0x0400C736 RID: 50998
				public static LocString DIRECTION_E = "E";

				// Token: 0x0400C737 RID: 50999
				public static LocString DIRECTION_SE = "SE";

				// Token: 0x0400C738 RID: 51000
				public static LocString DIRECTION_S = "S";

				// Token: 0x0400C739 RID: 51001
				public static LocString DIRECTION_SW = "SW";

				// Token: 0x0400C73A RID: 51002
				public static LocString DIRECTION_W = "W";

				// Token: 0x0400C73B RID: 51003
				public static LocString DIRECTION_NW = "NW";
			}

			// Token: 0x02002DF6 RID: 11766
			public class MONUMENTSIDESCREEN
			{
				// Token: 0x0400C73C RID: 51004
				public static LocString TITLE = "Great Monument";

				// Token: 0x0400C73D RID: 51005
				public static LocString FLIP_FACING_BUTTON = UI.CLICK(UI.ClickType.CLICK) + " TO ROTATE";
			}

			// Token: 0x02002DF7 RID: 11767
			public class PLANTERSIDESCREEN
			{
				// Token: 0x0400C73E RID: 51006
				public static LocString TITLE = "{0} Seeds";

				// Token: 0x0400C73F RID: 51007
				public static LocString INFORMATION = "INFORMATION";

				// Token: 0x0400C740 RID: 51008
				public static LocString AWAITINGREQUEST = "PLANT: {0}";

				// Token: 0x0400C741 RID: 51009
				public static LocString AWAITINGDELIVERY = "AWAITING DELIVERY: {0}";

				// Token: 0x0400C742 RID: 51010
				public static LocString AWAITINGREMOVAL = "AWAITING DIGGING UP: {0}";

				// Token: 0x0400C743 RID: 51011
				public static LocString ENTITYDEPOSITED = "PLANTED: {0}";

				// Token: 0x0400C744 RID: 51012
				public static LocString MUTATIONS_HEADER = "Mutations";

				// Token: 0x0400C745 RID: 51013
				public static LocString DEPOSIT = "Plant";

				// Token: 0x0400C746 RID: 51014
				public static LocString CANCELDEPOSIT = "Cancel";

				// Token: 0x0400C747 RID: 51015
				public static LocString REMOVE = "Uproot";

				// Token: 0x0400C748 RID: 51016
				public static LocString CANCELREMOVAL = "Cancel";

				// Token: 0x0400C749 RID: 51017
				public static LocString SELECT_TITLE = "SELECT";

				// Token: 0x0400C74A RID: 51018
				public static LocString SELECT_DESC = "Select a seed to plant.";

				// Token: 0x0400C74B RID: 51019
				public static LocString LIFECYCLE = "<b>Life Cycle</b>:";

				// Token: 0x0400C74C RID: 51020
				public static LocString PLANTREQUIREMENTS = "<b>Growth Requirements</b>:";

				// Token: 0x0400C74D RID: 51021
				public static LocString PLANTEFFECTS = "<b>Effects</b>:";

				// Token: 0x0400C74E RID: 51022
				public static LocString NUMBEROFHARVESTS = "Harvests: {0}";

				// Token: 0x0400C74F RID: 51023
				public static LocString YIELD = "{0}: {1} ";

				// Token: 0x0400C750 RID: 51024
				public static LocString YIELD_NONFOOD = "{0}: {1} ";

				// Token: 0x0400C751 RID: 51025
				public static LocString YIELD_SINGLE = "{0}";

				// Token: 0x0400C752 RID: 51026
				public static LocString YIELDPERHARVEST = "{0} {1} per harvest";

				// Token: 0x0400C753 RID: 51027
				public static LocString TOTALHARVESTCALORIESWITHPERUNIT = "{0} [{1} / unit]";

				// Token: 0x0400C754 RID: 51028
				public static LocString TOTALHARVESTCALORIES = "{0}";

				// Token: 0x0400C755 RID: 51029
				public static LocString BONUS_SEEDS = "Base " + UI.FormatAsLink("Seed", "PLANTS") + " Harvest Chance: {0}";

				// Token: 0x0400C756 RID: 51030
				public static LocString YIELD_SEED = "{1} {0}";

				// Token: 0x0400C757 RID: 51031
				public static LocString YIELD_SEED_SINGLE = "{0}";

				// Token: 0x0400C758 RID: 51032
				public static LocString YIELD_SEED_FINAL_HARVEST = "{1} {0} - Final harvest only";

				// Token: 0x0400C759 RID: 51033
				public static LocString YIELD_SEED_SINGLE_FINAL_HARVEST = "{0} - Final harvest only";

				// Token: 0x0400C75A RID: 51034
				public static LocString ROTATION_NEED_FLOOR = "<b>Requires upward plot orientation.</b>";

				// Token: 0x0400C75B RID: 51035
				public static LocString ROTATION_NEED_WALL = "<b>Requires sideways plot orientation.</b>";

				// Token: 0x0400C75C RID: 51036
				public static LocString ROTATION_NEED_CEILING = "<b>Requires downward plot orientation.</b>";

				// Token: 0x0400C75D RID: 51037
				public static LocString NO_SPECIES_SELECTED = "Select a seed species above...";

				// Token: 0x0400C75E RID: 51038
				public static LocString DISEASE_DROPPER_BURST = "{Disease} Burst: {DiseaseAmount}";

				// Token: 0x0400C75F RID: 51039
				public static LocString DISEASE_DROPPER_CONSTANT = "{Disease}: {DiseaseAmount}";

				// Token: 0x0400C760 RID: 51040
				public static LocString DISEASE_ON_HARVEST = "{Disease} on crop: {DiseaseAmount}";

				// Token: 0x0400C761 RID: 51041
				public static LocString AUTO_SELF_HARVEST = "Self-Harvest On Grown";

				// Token: 0x02003A9F RID: 15007
				public class TOOLTIPS
				{
					// Token: 0x0400E8EE RID: 59630
					public static LocString PLANTLIFECYCLE = "Duration and number of harvests produced by this plant in a lifetime";

					// Token: 0x0400E8EF RID: 59631
					public static LocString PLANTREQUIREMENTS = "Minimum conditions for basic plant growth";

					// Token: 0x0400E8F0 RID: 59632
					public static LocString PLANTEFFECTS = "Additional attributes of this plant";

					// Token: 0x0400E8F1 RID: 59633
					public static LocString YIELD = UI.FormatAsLink("{2}", "KCAL") + " produced [" + UI.FormatAsLink("{1}", "KCAL") + " / unit]";

					// Token: 0x0400E8F2 RID: 59634
					public static LocString YIELD_NONFOOD = "{0} produced per harvest";

					// Token: 0x0400E8F3 RID: 59635
					public static LocString NUMBEROFHARVESTS = "This plant can mature {0} times before the end of its life cycle";

					// Token: 0x0400E8F4 RID: 59636
					public static LocString YIELD_SEED = "Sow to grow more of this plant";

					// Token: 0x0400E8F5 RID: 59637
					public static LocString YIELD_SEED_FINAL_HARVEST = "{0}\n\nProduced in the final harvest of the plant's life cycle";

					// Token: 0x0400E8F6 RID: 59638
					public static LocString BONUS_SEEDS = "This plant has a {0} chance to produce new seeds when harvested";

					// Token: 0x0400E8F7 RID: 59639
					public static LocString DISEASE_DROPPER_BURST = "At certain points in this plant's lifecycle, it will emit a burst of {DiseaseAmount} {Disease}.";

					// Token: 0x0400E8F8 RID: 59640
					public static LocString DISEASE_DROPPER_CONSTANT = "This plant emits {DiseaseAmount} {Disease} while it is alive.";

					// Token: 0x0400E8F9 RID: 59641
					public static LocString DISEASE_ON_HARVEST = "The {Crop} produced by this plant will have {DiseaseAmount} {Disease} on it.";

					// Token: 0x0400E8FA RID: 59642
					public static LocString AUTO_SELF_HARVEST = "This plant will instantly drop its crop and begin regrowing when it is matured.";

					// Token: 0x0400E8FB RID: 59643
					public static LocString PLANT_TOGGLE_TOOLTIP = "{0}\n\n{1}\n\n<b>{2}</b> seeds available";
				}
			}

			// Token: 0x02002DF8 RID: 11768
			public class EGGINCUBATOR
			{
				// Token: 0x0400C762 RID: 51042
				public static LocString TITLE = "Critter Eggs";

				// Token: 0x0400C763 RID: 51043
				public static LocString AWAITINGREQUEST = "INCUBATE: {0}";

				// Token: 0x0400C764 RID: 51044
				public static LocString AWAITINGDELIVERY = "AWAITING DELIVERY: {0}";

				// Token: 0x0400C765 RID: 51045
				public static LocString AWAITINGREMOVAL = "AWAITING REMOVAL: {0}";

				// Token: 0x0400C766 RID: 51046
				public static LocString ENTITYDEPOSITED = "INCUBATING: {0}";

				// Token: 0x0400C767 RID: 51047
				public static LocString DEPOSIT = "Incubate";

				// Token: 0x0400C768 RID: 51048
				public static LocString CANCELDEPOSIT = "Cancel";

				// Token: 0x0400C769 RID: 51049
				public static LocString REMOVE = "Remove";

				// Token: 0x0400C76A RID: 51050
				public static LocString CANCELREMOVAL = "Cancel";

				// Token: 0x0400C76B RID: 51051
				public static LocString SELECT_TITLE = "SELECT";

				// Token: 0x0400C76C RID: 51052
				public static LocString SELECT_DESC = "Select an egg to incubate.";
			}

			// Token: 0x02002DF9 RID: 11769
			public class BASICRECEPTACLE
			{
				// Token: 0x0400C76D RID: 51053
				public static LocString TITLE = "Displayed Object";

				// Token: 0x0400C76E RID: 51054
				public static LocString AWAITINGREQUEST = "SELECT: {0}";

				// Token: 0x0400C76F RID: 51055
				public static LocString AWAITINGDELIVERY = "AWAITING DELIVERY: {0}";

				// Token: 0x0400C770 RID: 51056
				public static LocString AWAITINGREMOVAL = "AWAITING REMOVAL: {0}";

				// Token: 0x0400C771 RID: 51057
				public static LocString ENTITYDEPOSITED = "DISPLAYING: {0}";

				// Token: 0x0400C772 RID: 51058
				public static LocString DEPOSIT = "Select";

				// Token: 0x0400C773 RID: 51059
				public static LocString CANCELDEPOSIT = "Cancel";

				// Token: 0x0400C774 RID: 51060
				public static LocString REMOVE = "Remove";

				// Token: 0x0400C775 RID: 51061
				public static LocString CANCELREMOVAL = "Cancel";

				// Token: 0x0400C776 RID: 51062
				public static LocString SELECT_TITLE = "SELECT OBJECT";

				// Token: 0x0400C777 RID: 51063
				public static LocString SELECT_DESC = "Select an object to display here.";
			}

			// Token: 0x02002DFA RID: 11770
			public class SPECIALCARGOBAYCLUSTER
			{
				// Token: 0x0400C778 RID: 51064
				public static LocString TITLE = "Target Critter";

				// Token: 0x0400C779 RID: 51065
				public static LocString AWAITINGREQUEST = "SELECT: {0}";

				// Token: 0x0400C77A RID: 51066
				public static LocString AWAITINGDELIVERY = "AWAITING DELIVERY: {0}";

				// Token: 0x0400C77B RID: 51067
				public static LocString AWAITINGREMOVAL = "AWAITING REMOVAL: {0}";

				// Token: 0x0400C77C RID: 51068
				public static LocString ENTITYDEPOSITED = "CONTENTS: {0}";

				// Token: 0x0400C77D RID: 51069
				public static LocString DEPOSIT = "Select";

				// Token: 0x0400C77E RID: 51070
				public static LocString CANCELDEPOSIT = "Cancel";

				// Token: 0x0400C77F RID: 51071
				public static LocString REMOVE = "Remove";

				// Token: 0x0400C780 RID: 51072
				public static LocString CANCELREMOVAL = "Cancel";

				// Token: 0x0400C781 RID: 51073
				public static LocString SELECT_TITLE = "SELECT CRITTER";

				// Token: 0x0400C782 RID: 51074
				public static LocString SELECT_DESC = "Select a critter to store in this module.";
			}

			// Token: 0x02002DFB RID: 11771
			public class LURE
			{
				// Token: 0x0400C783 RID: 51075
				public static LocString TITLE = "Select Bait";

				// Token: 0x0400C784 RID: 51076
				public static LocString INFORMATION = "INFORMATION";

				// Token: 0x0400C785 RID: 51077
				public static LocString AWAITINGREQUEST = "PLANT: {0}";

				// Token: 0x0400C786 RID: 51078
				public static LocString AWAITINGDELIVERY = "AWAITING DELIVERY: {0}";

				// Token: 0x0400C787 RID: 51079
				public static LocString AWAITINGREMOVAL = "AWAITING DIGGING UP: {0}";

				// Token: 0x0400C788 RID: 51080
				public static LocString ENTITYDEPOSITED = "PLANTED: {0}";

				// Token: 0x0400C789 RID: 51081
				public static LocString ATTRACTS = "Attract {1}s";
			}

			// Token: 0x02002DFC RID: 11772
			public class ROLESTATION
			{
				// Token: 0x0400C78A RID: 51082
				public static LocString TITLE = "Duplicant Skills";

				// Token: 0x0400C78B RID: 51083
				public static LocString OPENROLESBUTTON = "SKILLS";
			}

			// Token: 0x02002DFD RID: 11773
			public class RESEARCHSIDESCREEN
			{
				// Token: 0x0400C78C RID: 51084
				public static LocString TITLE = "Select Research";

				// Token: 0x0400C78D RID: 51085
				public static LocString CURRENTLYRESEARCHING = "Currently Researching";

				// Token: 0x0400C78E RID: 51086
				public static LocString NOSELECTEDRESEARCH = "No Research selected";

				// Token: 0x0400C78F RID: 51087
				public static LocString OPENRESEARCHBUTTON = "RESEARCH";
			}

			// Token: 0x02002DFE RID: 11774
			public class REFINERYSIDESCREEN
			{
				// Token: 0x0400C790 RID: 51088
				public static LocString RECIPE_FROM_TO = "{0} to {1}";

				// Token: 0x0400C791 RID: 51089
				public static LocString RECIPE_WITH = "{1} ({0})";

				// Token: 0x0400C792 RID: 51090
				public static LocString RECIPE_FROM_TO_WITH_NEWLINES = "{0}\nto\n{1}";

				// Token: 0x0400C793 RID: 51091
				public static LocString RECIPE_FROM_TO_COMPOSITE = "{0} to {1} and {2}";

				// Token: 0x0400C794 RID: 51092
				public static LocString RECIPE_FROM_TO_HEP = "{0} to " + UI.FormatAsLink("Radbolts", "RADIATION") + " and {1}";

				// Token: 0x0400C795 RID: 51093
				public static LocString RECIPE_SIMPLE_INCLUDE_AMOUNTS = "{0} {1}";

				// Token: 0x0400C796 RID: 51094
				public static LocString RECIPE_FROM_TO_INCLUDE_AMOUNTS = "{2} {0} to {3} {1}";

				// Token: 0x0400C797 RID: 51095
				public static LocString RECIPE_WITH_INCLUDE_AMOUNTS = "{3} {1} ({2} {0})";

				// Token: 0x0400C798 RID: 51096
				public static LocString RECIPE_FROM_TO_COMPOSITE_INCLUDE_AMOUNTS = "{3} {0} to {4} {1} and {5} {2}";

				// Token: 0x0400C799 RID: 51097
				public static LocString RECIPE_FROM_TO_HEP_INCLUDE_AMOUNTS = "{2} {0} to {3} " + UI.FormatAsLink("Radbolts", "RADIATION") + " and {4} {1}";
			}

			// Token: 0x02002DFF RID: 11775
			public class SEALEDDOORSIDESCREEN
			{
				// Token: 0x0400C79A RID: 51098
				public static LocString TITLE = "Sealed Door";

				// Token: 0x0400C79B RID: 51099
				public static LocString LABEL = "This door requires a sample to unlock.";

				// Token: 0x0400C79C RID: 51100
				public static LocString BUTTON = "SUBMIT BIOSCAN";

				// Token: 0x0400C79D RID: 51101
				public static LocString AWAITINGBUTTON = "AWAITING BIOSCAN";
			}

			// Token: 0x02002E00 RID: 11776
			public class ENCRYPTEDLORESIDESCREEN
			{
				// Token: 0x0400C79E RID: 51102
				public static LocString TITLE = "Encrypted File";

				// Token: 0x0400C79F RID: 51103
				public static LocString LABEL = "This computer contains encrypted files.";

				// Token: 0x0400C7A0 RID: 51104
				public static LocString BUTTON = "ATTEMPT DECRYPTION";

				// Token: 0x0400C7A1 RID: 51105
				public static LocString AWAITINGBUTTON = "AWAITING DECRYPTION";
			}

			// Token: 0x02002E01 RID: 11777
			public class ACCESS_CONTROL_SIDE_SCREEN
			{
				// Token: 0x0400C7A2 RID: 51106
				public static LocString TITLE = "Door Access Control";

				// Token: 0x0400C7A3 RID: 51107
				public static LocString DOOR_DEFAULT = "Default";

				// Token: 0x0400C7A4 RID: 51108
				public static LocString MINION_ACCESS = "Duplicant Access Permissions";

				// Token: 0x0400C7A5 RID: 51109
				public static LocString GO_LEFT_ENABLED = "Passing Left through this door is permitted\n\n" + UI.CLICK(UI.ClickType.Click) + " to revoke permission";

				// Token: 0x0400C7A6 RID: 51110
				public static LocString GO_LEFT_DISABLED = "Passing Left through this door is not permitted\n\n" + UI.CLICK(UI.ClickType.Click) + " to grant permission";

				// Token: 0x0400C7A7 RID: 51111
				public static LocString GO_RIGHT_ENABLED = "Passing Right through this door is permitted\n\n" + UI.CLICK(UI.ClickType.Click) + " to revoke permission";

				// Token: 0x0400C7A8 RID: 51112
				public static LocString GO_RIGHT_DISABLED = "Passing Right through this door is not permitted\n\n" + UI.CLICK(UI.ClickType.Click) + " to grant permission";

				// Token: 0x0400C7A9 RID: 51113
				public static LocString GO_UP_ENABLED = "Passing Up through this door is permitted\n\n" + UI.CLICK(UI.ClickType.Click) + " to revoke permission";

				// Token: 0x0400C7AA RID: 51114
				public static LocString GO_UP_DISABLED = "Passing Up through this door is not permitted\n\n" + UI.CLICK(UI.ClickType.Click) + " to grant permission";

				// Token: 0x0400C7AB RID: 51115
				public static LocString GO_DOWN_ENABLED = "Passing Down through this door is permitted\n\n" + UI.CLICK(UI.ClickType.Click) + " to revoke permission";

				// Token: 0x0400C7AC RID: 51116
				public static LocString GO_DOWN_DISABLED = "Passing Down through this door is not permitted\n\n" + UI.CLICK(UI.ClickType.Click) + " to grant permission";

				// Token: 0x0400C7AD RID: 51117
				public static LocString SET_TO_DEFAULT = UI.CLICK(UI.ClickType.Click) + " to clear custom permissions";

				// Token: 0x0400C7AE RID: 51118
				public static LocString SET_TO_CUSTOM = UI.CLICK(UI.ClickType.Click) + " to assign custom permissions";

				// Token: 0x0400C7AF RID: 51119
				public static LocString USING_DEFAULT = "Default Access";

				// Token: 0x0400C7B0 RID: 51120
				public static LocString USING_CUSTOM = "Custom Access";
			}

			// Token: 0x02002E02 RID: 11778
			public class OWNABLESSIDESCREEN
			{
				// Token: 0x0400C7B1 RID: 51121
				public static LocString TITLE = "Equipment and Amenities";

				// Token: 0x0400C7B2 RID: 51122
				public static LocString NO_ITEM_ASSIGNED = "Assign";

				// Token: 0x0400C7B3 RID: 51123
				public static LocString NO_ITEM_FOUND = "None found";

				// Token: 0x0400C7B4 RID: 51124
				public static LocString NO_APPLICABLE = "{0}: Ineligible";

				// Token: 0x02003AA0 RID: 15008
				public static class TOOLTIPS
				{
					// Token: 0x0400E8FC RID: 59644
					public static LocString NO_APPLICABLE = "This Duplicant cannot be assigned " + UI.PRE_KEYWORD + "{0}" + UI.PST_KEYWORD;

					// Token: 0x0400E8FD RID: 59645
					public static LocString NO_ITEM_ASSIGNED = string.Concat(new string[]
					{
						"Click to view and assign existing ",
						UI.PRE_KEYWORD,
						"{0}",
						UI.PST_KEYWORD,
						" to this Duplicant"
					});

					// Token: 0x0400E8FE RID: 59646
					public static LocString ITEM_ASSIGNED_GENERIC = "This Duplicant has {0} assigned to them";

					// Token: 0x0400E8FF RID: 59647
					public static LocString ITEM_ASSIGNED = "{0}\n\n{1}";
				}

				// Token: 0x02003AA1 RID: 15009
				public class CATEGORIES
				{
					// Token: 0x0400E900 RID: 59648
					public static LocString SUITS = "Suits";

					// Token: 0x0400E901 RID: 59649
					public static LocString AMENITIES = "Amenities";
				}
			}

			// Token: 0x02002E03 RID: 11779
			public class OWNABLESSECONDSIDESCREEN
			{
				// Token: 0x0400C7B5 RID: 51125
				public static LocString TITLE = "{0}";

				// Token: 0x0400C7B6 RID: 51126
				public static LocString NONE_ROW_LABEL = "Clear";

				// Token: 0x0400C7B7 RID: 51127
				public static LocString NONE_ROW_TOOLTIP = "Click to remove any item currently assigned to the selected slot";

				// Token: 0x0400C7B8 RID: 51128
				public static LocString ASSIGNED_TO_OTHER_STATUS = "Assigned to: {0}";

				// Token: 0x0400C7B9 RID: 51129
				public static LocString ASSIGNED_TO_SELF_STATUS = "Assigned";

				// Token: 0x0400C7BA RID: 51130
				public static LocString NOT_ASSIGNED = "Unassigned";
			}

			// Token: 0x02002E04 RID: 11780
			public class ASSIGNABLESIDESCREEN
			{
				// Token: 0x0400C7BB RID: 51131
				public static LocString TITLE = "Assign {0}";

				// Token: 0x0400C7BC RID: 51132
				public static LocString ASSIGNED = "Assigned";

				// Token: 0x0400C7BD RID: 51133
				public static LocString UNASSIGNED = "-";

				// Token: 0x0400C7BE RID: 51134
				public static LocString DISABLED = "Ineligible";

				// Token: 0x0400C7BF RID: 51135
				public static LocString SORT_BY_DUPLICANT = "Duplicant";

				// Token: 0x0400C7C0 RID: 51136
				public static LocString SORT_BY_ASSIGNMENT = "Assignment";

				// Token: 0x0400C7C1 RID: 51137
				public static LocString ASSIGN_TO_TOOLTIP = "Assign to {0}";

				// Token: 0x0400C7C2 RID: 51138
				public static LocString UNASSIGN_TOOLTIP = "Assigned to {0}";

				// Token: 0x0400C7C3 RID: 51139
				public static LocString DISABLED_TOOLTIP = "{0} is ineligible for this skill assignment";

				// Token: 0x0400C7C4 RID: 51140
				public static LocString PUBLIC = "Public";
			}

			// Token: 0x02002E05 RID: 11781
			public class COMETDETECTORSIDESCREEN
			{
				// Token: 0x0400C7C5 RID: 51141
				public static LocString TITLE = "Space Scanner";

				// Token: 0x0400C7C6 RID: 51142
				public static LocString HEADER = "Sends automation signal when selected object is detected";

				// Token: 0x0400C7C7 RID: 51143
				public static LocString ASSIGNED = "Assigned";

				// Token: 0x0400C7C8 RID: 51144
				public static LocString UNASSIGNED = "-";

				// Token: 0x0400C7C9 RID: 51145
				public static LocString DISABLED = "Ineligible";

				// Token: 0x0400C7CA RID: 51146
				public static LocString SORT_BY_DUPLICANT = "Duplicant";

				// Token: 0x0400C7CB RID: 51147
				public static LocString SORT_BY_ASSIGNMENT = "Assignment";

				// Token: 0x0400C7CC RID: 51148
				public static LocString ASSIGN_TO_TOOLTIP = "Scanning for {0}";

				// Token: 0x0400C7CD RID: 51149
				public static LocString UNASSIGN_TOOLTIP = "Scanning for {0}";

				// Token: 0x0400C7CE RID: 51150
				public static LocString NOTHING = "Nothing";

				// Token: 0x0400C7CF RID: 51151
				public static LocString COMETS = "Meteor Showers";

				// Token: 0x0400C7D0 RID: 51152
				public static LocString ROCKETS = "Rocket Landing Ping";

				// Token: 0x0400C7D1 RID: 51153
				public static LocString DUPEMADE = "Interplanetary Payloads";
			}

			// Token: 0x02002E06 RID: 11782
			public class GEOTUNERSIDESCREEN
			{
				// Token: 0x0400C7D2 RID: 51154
				public static LocString TITLE = "Select Geyser";

				// Token: 0x0400C7D3 RID: 51155
				public static LocString DESCRIPTION = "Select an analyzed geyser to transmit amplification data to.";

				// Token: 0x0400C7D4 RID: 51156
				public static LocString NOTHING = "No geyser selected";

				// Token: 0x0400C7D5 RID: 51157
				public static LocString UNSTUDIED_TOOLTIP = "This geyser must be analyzed before it can be selected\n\nDouble-click to view this geyser";

				// Token: 0x0400C7D6 RID: 51158
				public static LocString STUDIED_TOOLTIP = string.Concat(new string[]
				{
					"Increase this geyser's ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" and output"
				});

				// Token: 0x0400C7D7 RID: 51159
				public static LocString GEOTUNER_LIMIT_TOOLTIP = "This geyser cannot be targeted by more " + UI.PRE_KEYWORD + "Geotuners" + UI.PST_KEYWORD;

				// Token: 0x0400C7D8 RID: 51160
				public static LocString STUDIED_TOOLTIP_MATERIAL = "Required resource: {MATERIAL}";

				// Token: 0x0400C7D9 RID: 51161
				public static LocString STUDIED_TOOLTIP_POTENTIAL_OUTPUT = "Potential Output {POTENTIAL_OUTPUT}";

				// Token: 0x0400C7DA RID: 51162
				public static LocString STUDIED_TOOLTIP_BASE_TEMP = "Base {BASE}";

				// Token: 0x0400C7DB RID: 51163
				public static LocString STUDIED_TOOLTIP_VISIT_GEYSER = "Double-click to view this geyser";

				// Token: 0x0400C7DC RID: 51164
				public static LocString STUDIED_TOOLTIP_GEOTUNER_MODIFIER_ROW_TITLE = "Geotuned ";

				// Token: 0x0400C7DD RID: 51165
				public static LocString STUDIED_TOOLTIP_NUMBER_HOVERED = "This geyser is targeted by {0} Geotuners";
			}

			// Token: 0x02002E07 RID: 11783
			public class REMOTE_WORK_TERMINAL_SIDE_SCREEN
			{
				// Token: 0x0400C7DE RID: 51166
				public static LocString TITLE = "Dock Assignment";

				// Token: 0x0400C7DF RID: 51167
				public static LocString DESCRIPTION = "Select a remote worker dock for this controller to target.";

				// Token: 0x0400C7E0 RID: 51168
				public static LocString NOTHING_SELECTED = "None";

				// Token: 0x0400C7E1 RID: 51169
				public static LocString DOCK_TOOLTIP = "Click to assign this dock to this controller\n\nDouble-click to view this dock";
			}

			// Token: 0x02002E08 RID: 11784
			public class COMMAND_MODULE_SIDE_SCREEN
			{
				// Token: 0x0400C7E2 RID: 51170
				public static LocString TITLE = "Launch Conditions";

				// Token: 0x0400C7E3 RID: 51171
				public static LocString DESTINATION_BUTTON = "Show Starmap";

				// Token: 0x0400C7E4 RID: 51172
				public static LocString DESTINATION_BUTTON_EXPANSION = "Show Starmap";
			}

			// Token: 0x02002E09 RID: 11785
			public class CLUSTERDESTINATIONSIDESCREEN
			{
				// Token: 0x0400C7E5 RID: 51173
				public static LocString TITLE = "Destination";

				// Token: 0x0400C7E6 RID: 51174
				public static LocString TITLE_MISSILE_TARGET = "Long Range Target";

				// Token: 0x0400C7E7 RID: 51175
				public static LocString FIRSTAVAILABLE = "Any " + BUILDINGS.PREFABS.LAUNCHPAD.NAME;

				// Token: 0x0400C7E8 RID: 51176
				public static LocString NONEAVAILABLE = "No landing site";

				// Token: 0x0400C7E9 RID: 51177
				public static LocString NO_TALL_SITES_AVAILABLE = "No landing sites fit the height of this rocket";

				// Token: 0x0400C7EA RID: 51178
				public static LocString DROPDOWN_TOOLTIP_VALID_SITE = "Land at {0} when the site is clear";

				// Token: 0x0400C7EB RID: 51179
				public static LocString DROPDOWN_TOOLTIP_FIRST_AVAILABLE = "Select the first available landing site";

				// Token: 0x0400C7EC RID: 51180
				public static LocString DROPDOWN_TOOLTIP_TOO_SHORT = "This rocket's height exceeds the space available in this landing site";

				// Token: 0x0400C7ED RID: 51181
				public static LocString DROPDOWN_TOOLTIP_PATH_OBSTRUCTED = "Landing path obstructed";

				// Token: 0x0400C7EE RID: 51182
				public static LocString DROPDOWN_TOOLTIP_SITE_OBSTRUCTED = "Landing position on the platform is obstructed";

				// Token: 0x0400C7EF RID: 51183
				public static LocString DROPDOWN_TOOLTIP_PAD_DISABLED = BUILDINGS.PREFABS.LAUNCHPAD.NAME + " is disabled";

				// Token: 0x0400C7F0 RID: 51184
				public static LocString CHANGE_DESTINATION_BUTTON = "Change";

				// Token: 0x0400C7F1 RID: 51185
				public static LocString CHANGE_DESTINATION_BUTTON_TOOLTIP = "Select a new destination for this rocket";

				// Token: 0x0400C7F2 RID: 51186
				public static LocString CHANGE_DESTINATION_BUTTON_TOOLTIP_MISSILE = "Select a new target for this missile launcher";

				// Token: 0x0400C7F3 RID: 51187
				public static LocString CHANGE_DESTINATION_BUTTON_TOOLTIP_RAILGUN = "Select a new target for this payload launcher";

				// Token: 0x0400C7F4 RID: 51188
				public static LocString CLEAR_DESTINATION_BUTTON = "Clear";

				// Token: 0x0400C7F5 RID: 51189
				public static LocString CLEAR_DESTINATION_BUTTON_TOOLTIP = "Clear this rocket's selected destination";

				// Token: 0x0400C7F6 RID: 51190
				public static LocString CLEAR_DESTINATION_BUTTON_TOOLTIP_MISSILE = "Clear this missile launcher's selected target";

				// Token: 0x0400C7F7 RID: 51191
				public static LocString CLEAR_DESTINATION_BUTTON_TOOLTIP_RAILGUN = "Clear this payload launcher's selected target";

				// Token: 0x0400C7F8 RID: 51192
				public static LocString LOOP_BUTTON_TOOLTIP = "Toggle a roundtrip flight between this rocket's destination and its original takeoff location";

				// Token: 0x02003AA2 RID: 15010
				public class ASSIGNMENTSTATUS
				{
					// Token: 0x0400E902 RID: 59650
					public static LocString LOCAL = "Current";

					// Token: 0x0400E903 RID: 59651
					public static LocString DESTINATION = "Destination";
				}
			}

			// Token: 0x02002E0A RID: 11786
			public class EQUIPPABLESIDESCREEN
			{
				// Token: 0x0400C7F9 RID: 51193
				public static LocString TITLE = "Equip {0}";

				// Token: 0x0400C7FA RID: 51194
				public static LocString ASSIGNEDTO = "Assigned to: {Assignee}";

				// Token: 0x0400C7FB RID: 51195
				public static LocString UNASSIGNED = "Unassigned";

				// Token: 0x0400C7FC RID: 51196
				public static LocString GENERAL_CURRENTASSIGNED = "(Owner)";
			}

			// Token: 0x02002E0B RID: 11787
			public class EQUIPPABLE_SIDE_SCREEN
			{
				// Token: 0x0400C7FD RID: 51197
				public static LocString TITLE = "Assign To Duplicant";

				// Token: 0x0400C7FE RID: 51198
				public static LocString CURRENTLY_EQUIPPED = "Currently Equipped:\n{0}";

				// Token: 0x0400C7FF RID: 51199
				public static LocString NONE_EQUIPPED = "None";

				// Token: 0x0400C800 RID: 51200
				public static LocString EQUIP_BUTTON = "Equip";

				// Token: 0x0400C801 RID: 51201
				public static LocString DROP_BUTTON = "Drop";

				// Token: 0x0400C802 RID: 51202
				public static LocString SWAP_BUTTON = "Swap";
			}

			// Token: 0x02002E0C RID: 11788
			public class TELEPADSIDESCREEN
			{
				// Token: 0x0400C803 RID: 51203
				public static LocString TITLE = "Printables";

				// Token: 0x0400C804 RID: 51204
				public static LocString NEXTPRODUCTION = "Next Production: {0}";

				// Token: 0x0400C805 RID: 51205
				public static LocString GAMEOVER = "Colony Lost";

				// Token: 0x0400C806 RID: 51206
				public static LocString VICTORY_CONDITIONS = "Hardwired Imperatives";

				// Token: 0x0400C807 RID: 51207
				public static LocString SUMMARY_TITLE = "Colony Summary";

				// Token: 0x0400C808 RID: 51208
				public static LocString SKILLS_BUTTON = "Duplicant Skills";
			}

			// Token: 0x02002E0D RID: 11789
			public class VALVESIDESCREEN
			{
				// Token: 0x0400C809 RID: 51209
				public static LocString TITLE = "Flow Control";
			}

			// Token: 0x02002E0E RID: 11790
			public class BIONIC_SIDE_SCREEN
			{
				// Token: 0x0400C80A RID: 51210
				public static LocString TITLE = "Boosters";

				// Token: 0x0400C80B RID: 51211
				public static LocString UPGRADE_SLOT_LOCKED = "N/A";

				// Token: 0x0400C80C RID: 51212
				public static LocString UPGRADE_SLOT_EMPTY = "Empty";

				// Token: 0x0400C80D RID: 51213
				public static LocString UPGRADE_SLOT_ASSIGNED = "Assigned";

				// Token: 0x0400C80E RID: 51214
				public static LocString UPGRADE_SLOT_INSTALLED = "Installed";

				// Token: 0x0400C80F RID: 51215
				public static LocString CURRENT_WATTAGE_LABEL = "Current Wattage: <b>{0}</b>";

				// Token: 0x0400C810 RID: 51216
				public static LocString CURRENT_WATTAGE_LABEL_BATTERY_SAVE_MODE = "Current Wattage: <color=#0303fc><b>{0}</b> {1}</color>";

				// Token: 0x0400C811 RID: 51217
				public static LocString CURRENT_WATTAGE_LABEL_OFFLINE = "Current Wattage: <color=#GG2222>Offline {0}</color>";

				// Token: 0x0400C812 RID: 51218
				public const string OFFLINE_MODE_COLOR = "<color=#GG2222>";

				// Token: 0x0400C813 RID: 51219
				public const string BATTERY_SAVE_MODE_COLOR = "<color=#0303fc>";

				// Token: 0x0400C814 RID: 51220
				public const string COLOR_END = "</color>";

				// Token: 0x02003AA3 RID: 15011
				public class TOOLTIP
				{
					// Token: 0x0400E904 RID: 59652
					public static LocString CURRENT_WATTAGE = "Wattage is the amount of energy that this Duplicant's bionic parts consume per second\n\nInstalled boosters consume wattage while active";

					// Token: 0x0400E905 RID: 59653
					public static LocString SLOT_LOCKED = "This booster slot is unavailable\n\nBooster slots can be unlocked using " + UI.PRE_KEYWORD + "Skill Points" + UI.PST_KEYWORD;

					// Token: 0x0400E906 RID: 59654
					public static LocString SLOT_EMPTY = "No booster installed\n\nClick to view available boosters";

					// Token: 0x0400E907 RID: 59655
					public static LocString SLOT_ASSIGNED = string.Concat(new string[]
					{
						"This ",
						UI.PRE_KEYWORD,
						"{0}",
						UI.PST_KEYWORD,
						" will be installed when it is within this Duplicant's reach"
					});

					// Token: 0x0400E908 RID: 59656
					public static LocString SLOT_INSTALLED = "{0}";
				}

				// Token: 0x02003AA4 RID: 15012
				public class BOOSTER_ASSIGNMENT
				{
					// Token: 0x0400E909 RID: 59657
					public static LocString NOT_ALREADY_ASSIGNED = "{0} does not currently have this type of booster assigned";

					// Token: 0x0400E90A RID: 59658
					public static LocString ALREADY_ASSIGNED = "{0} currently has <b>{1} of this type</b> of booster assigned";

					// Token: 0x0400E90B RID: 59659
					public static LocString AVAILABLE_SLOTS = "{0} has <b>{1}/{2}</b> booster slots assigned";

					// Token: 0x0400E90C RID: 59660
					public static LocString NO_AVAILABLE_SLOTS = UI.YELLOW_PREFIX + "All of {0}'s booster slots are currently assigned: <b>{1}/{2}</b>" + UI.COLOR_SUFFIX;

					// Token: 0x0400E90D RID: 59661
					public static LocString HEADER_PERKS = "<b>Enables:</b>";

					// Token: 0x0400E90E RID: 59662
					public static LocString HEADER_ATTRIBUTES = "<b>Boosts:</b>";
				}
			}

			// Token: 0x02002E0F RID: 11791
			public class LIMIT_VALVE_SIDE_SCREEN
			{
				// Token: 0x0400C815 RID: 51221
				public static LocString TITLE = "Meter Control";

				// Token: 0x0400C816 RID: 51222
				public static LocString AMOUNT = "Amount: {0}";

				// Token: 0x0400C817 RID: 51223
				public static LocString LIMIT = "Limit:";

				// Token: 0x0400C818 RID: 51224
				public static LocString RESET_BUTTON = "Reset Amount";

				// Token: 0x0400C819 RID: 51225
				public static LocString SLIDER_TOOLTIP_UNITS = "The amount of Units or Mass passing through the sensor.";
			}

			// Token: 0x02002E10 RID: 11792
			public class NUCLEAR_REACTOR_SIDE_SCREEN
			{
				// Token: 0x0400C81A RID: 51226
				public static LocString TITLE = "Reaction Mass Target";

				// Token: 0x0400C81B RID: 51227
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Duplicants will attempt to keep the reactor supplied with ",
					UI.PRE_KEYWORD,
					"{0}{1}",
					UI.PST_KEYWORD,
					" of ",
					UI.PRE_KEYWORD,
					"{2}",
					UI.PST_KEYWORD
				});
			}

			// Token: 0x02002E11 RID: 11793
			public class MANUALGENERATORSIDESCREEN
			{
				// Token: 0x0400C81C RID: 51228
				public static LocString TITLE = "Battery Recharge Threshold";

				// Token: 0x0400C81D RID: 51229
				public static LocString CURRENT_THRESHOLD = "Current Threshold: {0}%";

				// Token: 0x0400C81E RID: 51230
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Duplicants will be requested to operate this generator when the total charge of the connected ",
					UI.PRE_KEYWORD,
					"Batteries",
					UI.PST_KEYWORD,
					" falls below <b>{0}%</b>"
				});
			}

			// Token: 0x02002E12 RID: 11794
			public class SPACEHEATERSIDESCREEN
			{
				// Token: 0x0400C81F RID: 51231
				public static LocString TITLE = "Power Consumption";

				// Token: 0x0400C820 RID: 51232
				public static LocString CURRENT_THRESHOLD = "Current Power Consumption: {0}";

				// Token: 0x0400C821 RID: 51233
				public static LocString TOOLTIP = "Adjust power consumption to determine how much heat is produced\n\nCurrent heat production: <b>{0}</b>";
			}

			// Token: 0x02002E13 RID: 11795
			public class MANUALDELIVERYGENERATORSIDESCREEN
			{
				// Token: 0x0400C822 RID: 51234
				public static LocString TITLE = "Fuel Request Threshold";

				// Token: 0x0400C823 RID: 51235
				public static LocString CURRENT_THRESHOLD = "Current Threshold: {0}%";

				// Token: 0x0400C824 RID: 51236
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Duplicants will be requested to deliver ",
					UI.PRE_KEYWORD,
					"{0}",
					UI.PST_KEYWORD,
					" when the total charge of the connected ",
					UI.PRE_KEYWORD,
					"Batteries",
					UI.PST_KEYWORD,
					" falls below <b>{1}%</b>"
				});
			}

			// Token: 0x02002E14 RID: 11796
			public class TIME_OF_DAY_SIDE_SCREEN
			{
				// Token: 0x0400C825 RID: 51237
				public static LocString TITLE = "Time-of-Day Sensor";

				// Token: 0x0400C826 RID: 51238
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Will send a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" after the selected Turn On time, and a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" after the selected Turn Off time"
				});

				// Token: 0x0400C827 RID: 51239
				public static LocString START = "Turn On";

				// Token: 0x0400C828 RID: 51240
				public static LocString STOP = "Turn Off";
			}

			// Token: 0x02002E15 RID: 11797
			public class CRITTER_COUNT_SIDE_SCREEN
			{
				// Token: 0x0400C829 RID: 51241
				public static LocString TITLE = "Critter Count Sensor";

				// Token: 0x0400C82A RID: 51242
				public static LocString TOOLTIP_ABOVE = string.Concat(new string[]
				{
					"Will send a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" if there are more than <b>{0}</b> ",
					UI.PRE_KEYWORD,
					"Critters",
					UI.PST_KEYWORD,
					" or ",
					UI.PRE_KEYWORD,
					"Eggs",
					UI.PST_KEYWORD,
					" in the room"
				});

				// Token: 0x0400C82B RID: 51243
				public static LocString TOOLTIP_BELOW = string.Concat(new string[]
				{
					"Will send a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" if there are fewer than <b>{0}</b> ",
					UI.PRE_KEYWORD,
					"Critters",
					UI.PST_KEYWORD,
					" or ",
					UI.PRE_KEYWORD,
					"Eggs",
					UI.PST_KEYWORD,
					" in the room"
				});

				// Token: 0x0400C82C RID: 51244
				public static LocString START = "Turn On";

				// Token: 0x0400C82D RID: 51245
				public static LocString STOP = "Turn Off";

				// Token: 0x0400C82E RID: 51246
				public static LocString VALUE_NAME = "Count";
			}

			// Token: 0x02002E16 RID: 11798
			public class OIL_WELL_CAP_SIDE_SCREEN
			{
				// Token: 0x0400C82F RID: 51247
				public static LocString TITLE = "Backpressure Release Threshold";

				// Token: 0x0400C830 RID: 51248
				public static LocString TOOLTIP = "Duplicants will be requested to release backpressure buildup when it exceeds <b>{0}%</b>";
			}

			// Token: 0x02002E17 RID: 11799
			public class MODULAR_CONDUIT_PORT_SIDE_SCREEN
			{
				// Token: 0x0400C831 RID: 51249
				public static LocString TITLE = "Pump Control";

				// Token: 0x0400C832 RID: 51250
				public static LocString LABEL_UNLOAD = "Unload Only";

				// Token: 0x0400C833 RID: 51251
				public static LocString LABEL_BOTH = "Load/Unload";

				// Token: 0x0400C834 RID: 51252
				public static LocString LABEL_LOAD = "Load Only";

				// Token: 0x0400C835 RID: 51253
				public static readonly List<LocString> LABELS = new List<LocString>
				{
					UI.UISIDESCREENS.MODULAR_CONDUIT_PORT_SIDE_SCREEN.LABEL_UNLOAD,
					UI.UISIDESCREENS.MODULAR_CONDUIT_PORT_SIDE_SCREEN.LABEL_BOTH,
					UI.UISIDESCREENS.MODULAR_CONDUIT_PORT_SIDE_SCREEN.LABEL_LOAD
				};

				// Token: 0x0400C836 RID: 51254
				public static LocString TOOLTIP_UNLOAD = "This pump will attempt to <b>Unload</b> cargo from the landed rocket, but not attempt to load new cargo";

				// Token: 0x0400C837 RID: 51255
				public static LocString TOOLTIP_BOTH = "This pump will both <b>Load</b> and <b>Unload</b> cargo from the landed rocket";

				// Token: 0x0400C838 RID: 51256
				public static LocString TOOLTIP_LOAD = "This pump will attempt to <b>Load</b> cargo onto the landed rocket, but will not unload it";

				// Token: 0x0400C839 RID: 51257
				public static readonly List<LocString> TOOLTIPS = new List<LocString>
				{
					UI.UISIDESCREENS.MODULAR_CONDUIT_PORT_SIDE_SCREEN.TOOLTIP_UNLOAD,
					UI.UISIDESCREENS.MODULAR_CONDUIT_PORT_SIDE_SCREEN.TOOLTIP_BOTH,
					UI.UISIDESCREENS.MODULAR_CONDUIT_PORT_SIDE_SCREEN.TOOLTIP_LOAD
				};

				// Token: 0x0400C83A RID: 51258
				public static LocString DESCRIPTION = "";
			}

			// Token: 0x02002E18 RID: 11800
			public class LOGIC_BUFFER_SIDE_SCREEN
			{
				// Token: 0x0400C83B RID: 51259
				public static LocString TITLE = "Buffer Time";

				// Token: 0x0400C83C RID: 51260
				public static LocString TOOLTIP = "Will continue to send a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " for <b>{0} seconds</b> after receiving a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002E19 RID: 11801
			public class LOGIC_FILTER_SIDE_SCREEN
			{
				// Token: 0x0400C83D RID: 51261
				public static LocString TITLE = "Filter Time";

				// Token: 0x0400C83E RID: 51262
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Will only send a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" if it receives ",
					UI.FormatAsAutomationState("Green", UI.AutomationState.Active),
					" for longer than <b>{0} seconds</b>"
				});
			}

			// Token: 0x02002E1A RID: 11802
			public class TIME_RANGE_SIDE_SCREEN
			{
				// Token: 0x0400C83F RID: 51263
				public static LocString TITLE = "Time Schedule";

				// Token: 0x0400C840 RID: 51264
				public static LocString ON = "Activation Time";

				// Token: 0x0400C841 RID: 51265
				public static LocString ON_TOOLTIP = string.Concat(new string[]
				{
					"Activation time determines the time of day this sensor should begin sending a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					"\n\nThis sensor sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" {0} through the day"
				});

				// Token: 0x0400C842 RID: 51266
				public static LocString DURATION = "Active Duration";

				// Token: 0x0400C843 RID: 51267
				public static LocString DURATION_TOOLTIP = string.Concat(new string[]
				{
					"Active duration determines what percentage of the day this sensor will spend sending a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					"\n\nThis sensor will send a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" for {0} of the day"
				});
			}

			// Token: 0x02002E1B RID: 11803
			public class TIMER_SIDE_SCREEN
			{
				// Token: 0x0400C844 RID: 51268
				public static LocString TITLE = "Timer";

				// Token: 0x0400C845 RID: 51269
				public static LocString ON = "Green Duration";

				// Token: 0x0400C846 RID: 51270
				public static LocString GREEN_DURATION_TOOLTIP = string.Concat(new string[]
				{
					"Green duration determines the amount of time this sensor should send a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					"\n\nThis sensor sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" for {0}"
				});

				// Token: 0x0400C847 RID: 51271
				public static LocString OFF = "Red Duration";

				// Token: 0x0400C848 RID: 51272
				public static LocString RED_DURATION_TOOLTIP = string.Concat(new string[]
				{
					"Red duration determines the amount of time this sensor should send a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					"\n\nThis sensor will send a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" for {0}"
				});

				// Token: 0x0400C849 RID: 51273
				public static LocString CURRENT_TIME = "{0}/{1}";

				// Token: 0x0400C84A RID: 51274
				public static LocString MODE_LABEL_SECONDS = "Mode: Seconds";

				// Token: 0x0400C84B RID: 51275
				public static LocString MODE_LABEL_CYCLES = "Mode: Cycles";

				// Token: 0x0400C84C RID: 51276
				public static LocString RESET_BUTTON = "Reset Timer";

				// Token: 0x0400C84D RID: 51277
				public static LocString DISABLED = "Timer Disabled";
			}

			// Token: 0x02002E1C RID: 11804
			public class COUNTER_SIDE_SCREEN
			{
				// Token: 0x0400C84E RID: 51278
				public static LocString TITLE = "Counter";

				// Token: 0x0400C84F RID: 51279
				public static LocString RESET_BUTTON = "Reset Counter";

				// Token: 0x0400C850 RID: 51280
				public static LocString DESCRIPTION = "Send " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " when count is reached:";

				// Token: 0x0400C851 RID: 51281
				public static LocString INCREMENT_MODE = "Mode: Increment";

				// Token: 0x0400C852 RID: 51282
				public static LocString DECREMENT_MODE = "Mode: Decrement";

				// Token: 0x0400C853 RID: 51283
				public static LocString ADVANCED_MODE = "Advanced Mode";

				// Token: 0x0400C854 RID: 51284
				public static LocString CURRENT_COUNT_SIMPLE = "{0} of ";

				// Token: 0x0400C855 RID: 51285
				public static LocString CURRENT_COUNT_ADVANCED = "{0} % ";

				// Token: 0x02003AA5 RID: 15013
				public class TOOLTIPS
				{
					// Token: 0x0400E90F RID: 59663
					public static LocString ADVANCED_MODE = string.Concat(new string[]
					{
						"In Advanced Mode, the ",
						BUILDINGS.PREFABS.LOGICCOUNTER.NAME,
						" will count from <b>0</b> rather than <b>1</b>. It will reset when the max is reached, and send a ",
						UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
						" as a brief pulse rather than continuously."
					});
				}
			}

			// Token: 0x02002E1D RID: 11805
			public class PASSENGERMODULESIDESCREEN
			{
				// Token: 0x0400C856 RID: 51286
				public static LocString REQUEST_CREW = "Crew";

				// Token: 0x0400C857 RID: 51287
				public static LocString REQUEST_CREW_TOOLTIP = "Crew may not leave the module, non crew-must exit";

				// Token: 0x0400C858 RID: 51288
				public static LocString AUTO_CREW = "Auto";

				// Token: 0x0400C859 RID: 51289
				public static LocString AUTO_CREW_TOOLTIP = "All Duplicants may enter and exit the module freely until the rocket is ready for launch\n\nBefore launch the crew will automatically be requested";

				// Token: 0x0400C85A RID: 51290
				public static LocString RELEASE_CREW = "All";

				// Token: 0x0400C85B RID: 51291
				public static LocString RELEASE_CREW_TOOLTIP = "All Duplicants may enter and exit the module freely";

				// Token: 0x0400C85C RID: 51292
				public static LocString REQUIRE_SUIT_LABEL = "Atmosuit Required";

				// Token: 0x0400C85D RID: 51293
				public static LocString REQUIRE_SUIT_LABEL_TOOLTIP = "If checked, Duplicants will be required to wear an Atmo Suit when entering this rocket";

				// Token: 0x0400C85E RID: 51294
				public static LocString CHANGE_CREW_BUTTON = "Change crew";

				// Token: 0x0400C85F RID: 51295
				public static LocString CHANGE_CREW_BUTTON_TOOLTIP = "Assign Duplicants to crew this rocket's missions";

				// Token: 0x0400C860 RID: 51296
				public static LocString ASSIGNED_TO_CREW = "Assigned to crew";

				// Token: 0x0400C861 RID: 51297
				public static LocString UNASSIGNED = "Unassigned";
			}

			// Token: 0x02002E1E RID: 11806
			public class TIMEDSWITCHSIDESCREEN
			{
				// Token: 0x0400C862 RID: 51298
				public static LocString TITLE = "Time Schedule";

				// Token: 0x0400C863 RID: 51299
				public static LocString ONTIME = "On Time:";

				// Token: 0x0400C864 RID: 51300
				public static LocString OFFTIME = "Off Time:";

				// Token: 0x0400C865 RID: 51301
				public static LocString TIMETODEACTIVATE = "Time until deactivation: {0}";

				// Token: 0x0400C866 RID: 51302
				public static LocString TIMETOACTIVATE = "Time until activation: {0}";

				// Token: 0x0400C867 RID: 51303
				public static LocString WARNING = "Switch must be connected to a " + UI.FormatAsLink("Power", "POWER") + " grid";

				// Token: 0x0400C868 RID: 51304
				public static LocString CURRENTSTATE = "Current State:";

				// Token: 0x0400C869 RID: 51305
				public static LocString ON = "On";

				// Token: 0x0400C86A RID: 51306
				public static LocString OFF = "Off";
			}

			// Token: 0x02002E1F RID: 11807
			public class CAPTURE_POINT_SIDE_SCREEN
			{
				// Token: 0x0400C86B RID: 51307
				public static LocString TITLE = "Stable Management";

				// Token: 0x0400C86C RID: 51308
				public static LocString AUTOWRANGLE = "Auto-Wrangle Surplus";

				// Token: 0x0400C86D RID: 51309
				public static LocString AUTOWRANGLE_TOOLTIP = string.Concat(new string[]
				{
					"A Duplicant will automatically wrangle any critters that exceed the population limit or that do not belong in this stable\n\nDuplicants must possess the ",
					UI.PRE_KEYWORD,
					"Critter Ranching",
					UI.PST_KEYWORD,
					" skill in order to wrangle critters"
				});

				// Token: 0x0400C86E RID: 51310
				public static LocString LIMIT_TOOLTIP = "Critters exceeding this population limit will automatically be wrangled:";

				// Token: 0x0400C86F RID: 51311
				public static LocString UNITS_SUFFIX = " Critters";
			}

			// Token: 0x02002E20 RID: 11808
			public class TEMPERATURESWITCHSIDESCREEN
			{
				// Token: 0x0400C870 RID: 51312
				public static LocString TITLE = "Temperature Threshold";

				// Token: 0x0400C871 RID: 51313
				public static LocString CURRENT_TEMPERATURE = "Current Temperature:\n{0}";

				// Token: 0x0400C872 RID: 51314
				public static LocString ACTIVATE_IF = "Send " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if:";

				// Token: 0x0400C873 RID: 51315
				public static LocString COLDER_BUTTON = "Below";

				// Token: 0x0400C874 RID: 51316
				public static LocString WARMER_BUTTON = "Above";
			}

			// Token: 0x02002E21 RID: 11809
			public class BRIGHTNESSSWITCHSIDESCREEN
			{
				// Token: 0x0400C875 RID: 51317
				public static LocString TITLE = "Brightness Threshold";

				// Token: 0x0400C876 RID: 51318
				public static LocString CURRENT_TEMPERATURE = "Current Brightness:\n{0}";

				// Token: 0x0400C877 RID: 51319
				public static LocString ACTIVATE_IF = "Send " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if:";

				// Token: 0x0400C878 RID: 51320
				public static LocString COLDER_BUTTON = "Below";

				// Token: 0x0400C879 RID: 51321
				public static LocString WARMER_BUTTON = "Above";
			}

			// Token: 0x02002E22 RID: 11810
			public class RADIATIONSWITCHSIDESCREEN
			{
				// Token: 0x0400C87A RID: 51322
				public static LocString TITLE = "Radiation Threshold";

				// Token: 0x0400C87B RID: 51323
				public static LocString CURRENT_TEMPERATURE = "Current Radiation:\n{0}/cycle";

				// Token: 0x0400C87C RID: 51324
				public static LocString ACTIVATE_IF = "Send " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if:";

				// Token: 0x0400C87D RID: 51325
				public static LocString COLDER_BUTTON = "Below";

				// Token: 0x0400C87E RID: 51326
				public static LocString WARMER_BUTTON = "Above";
			}

			// Token: 0x02002E23 RID: 11811
			public class WATTAGESWITCHSIDESCREEN
			{
				// Token: 0x0400C87F RID: 51327
				public static LocString TITLE = "Wattage Threshold";

				// Token: 0x0400C880 RID: 51328
				public static LocString CURRENT_TEMPERATURE = "Current Wattage:\n{0}";

				// Token: 0x0400C881 RID: 51329
				public static LocString ACTIVATE_IF = "Send " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if:";

				// Token: 0x0400C882 RID: 51330
				public static LocString COLDER_BUTTON = "Below";

				// Token: 0x0400C883 RID: 51331
				public static LocString WARMER_BUTTON = "Above";
			}

			// Token: 0x02002E24 RID: 11812
			public class HEPSWITCHSIDESCREEN
			{
				// Token: 0x0400C884 RID: 51332
				public static LocString TITLE = "Radbolt Threshold";
			}

			// Token: 0x02002E25 RID: 11813
			public class THRESHOLD_SWITCH_SIDESCREEN
			{
				// Token: 0x0400C885 RID: 51333
				public static LocString TITLE = "Pressure";

				// Token: 0x0400C886 RID: 51334
				public static LocString THRESHOLD_SUBTITLE = "Threshold:";

				// Token: 0x0400C887 RID: 51335
				public static LocString CURRENT_VALUE = "Current {0}:\n{1}";

				// Token: 0x0400C888 RID: 51336
				public static LocString ACTIVATE_IF = "Send " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if:";

				// Token: 0x0400C889 RID: 51337
				public static LocString ABOVE_BUTTON = "Above";

				// Token: 0x0400C88A RID: 51338
				public static LocString BELOW_BUTTON = "Below";

				// Token: 0x0400C88B RID: 51339
				public static LocString STATUS_ACTIVE = "Switch Active";

				// Token: 0x0400C88C RID: 51340
				public static LocString STATUS_INACTIVE = "Switch Inactive";

				// Token: 0x0400C88D RID: 51341
				public static LocString PRESSURE = "Ambient Pressure";

				// Token: 0x0400C88E RID: 51342
				public static LocString PRESSURE_TOOLTIP_ABOVE = string.Concat(new string[]
				{
					"Will send a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" if the ",
					UI.PRE_KEYWORD,
					"Pressure",
					UI.PST_KEYWORD,
					" is above <b>{0}</b>"
				});

				// Token: 0x0400C88F RID: 51343
				public static LocString PRESSURE_TOOLTIP_BELOW = string.Concat(new string[]
				{
					"Will send a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" if the ",
					UI.PRE_KEYWORD,
					"Pressure",
					UI.PST_KEYWORD,
					" is below <b>{0}</b>"
				});

				// Token: 0x0400C890 RID: 51344
				public static LocString TEMPERATURE = "Ambient Temperature";

				// Token: 0x0400C891 RID: 51345
				public static LocString TEMPERATURE_TOOLTIP_ABOVE = string.Concat(new string[]
				{
					"Will send a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" if the ambient ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" is above <b>{0}</b>"
				});

				// Token: 0x0400C892 RID: 51346
				public static LocString TEMPERATURE_TOOLTIP_BELOW = string.Concat(new string[]
				{
					"Will send a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" if the ambient ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" is below <b>{0}</b>"
				});

				// Token: 0x0400C893 RID: 51347
				public static LocString CONTENT_TEMPERATURE = "Internal Temperature";

				// Token: 0x0400C894 RID: 51348
				public static LocString CONTENT_TEMPERATURE_TOOLTIP_ABOVE = string.Concat(new string[]
				{
					"Will send a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" if the ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" of its contents is above <b>{0}</b>"
				});

				// Token: 0x0400C895 RID: 51349
				public static LocString CONTENT_TEMPERATURE_TOOLTIP_BELOW = string.Concat(new string[]
				{
					"Will send a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" if the ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" of its contents is below <b>{0}</b>"
				});

				// Token: 0x0400C896 RID: 51350
				public static LocString BRIGHTNESS = "Ambient Brightness";

				// Token: 0x0400C897 RID: 51351
				public static LocString BRIGHTNESS_TOOLTIP_ABOVE = string.Concat(new string[]
				{
					"Will send a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" if the ambient ",
					UI.PRE_KEYWORD,
					"Brightness",
					UI.PST_KEYWORD,
					" is above <b>{0}</b>"
				});

				// Token: 0x0400C898 RID: 51352
				public static LocString BRIGHTNESS_TOOLTIP_BELOW = string.Concat(new string[]
				{
					"Will send a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" if the ambient ",
					UI.PRE_KEYWORD,
					"Brightness",
					UI.PST_KEYWORD,
					" is below <b>{0}</b>"
				});

				// Token: 0x0400C899 RID: 51353
				public static LocString WATTAGE = "Wattage Reading";

				// Token: 0x0400C89A RID: 51354
				public static LocString WATTAGE_TOOLTIP_ABOVE = string.Concat(new string[]
				{
					"Will send a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" if the ",
					UI.PRE_KEYWORD,
					"Wattage",
					UI.PST_KEYWORD,
					" consumed is above <b>{0}</b>"
				});

				// Token: 0x0400C89B RID: 51355
				public static LocString WATTAGE_TOOLTIP_BELOW = string.Concat(new string[]
				{
					"Will send a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" if the ",
					UI.PRE_KEYWORD,
					"Wattage",
					UI.PST_KEYWORD,
					" consumed is below <b>{0}</b>"
				});

				// Token: 0x0400C89C RID: 51356
				public static LocString DISEASE_TITLE = "Germ Threshold";

				// Token: 0x0400C89D RID: 51357
				public static LocString DISEASE = "Ambient Germs";

				// Token: 0x0400C89E RID: 51358
				public static LocString DISEASE_TOOLTIP_ABOVE = string.Concat(new string[]
				{
					"Will send a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" if the number of ",
					UI.PRE_KEYWORD,
					"Germs",
					UI.PST_KEYWORD,
					" is above <b>{0}</b>"
				});

				// Token: 0x0400C89F RID: 51359
				public static LocString DISEASE_TOOLTIP_BELOW = string.Concat(new string[]
				{
					"Will send a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" if the number of ",
					UI.PRE_KEYWORD,
					"Germs",
					UI.PST_KEYWORD,
					" is below <b>{0}</b>"
				});

				// Token: 0x0400C8A0 RID: 51360
				public static LocString DISEASE_UNITS = "";

				// Token: 0x0400C8A1 RID: 51361
				public static LocString CONTENT_DISEASE = "Germ Count";

				// Token: 0x0400C8A2 RID: 51362
				public static LocString RADIATION = "Ambient Radiation";

				// Token: 0x0400C8A3 RID: 51363
				public static LocString RADIATION_TOOLTIP_ABOVE = string.Concat(new string[]
				{
					"Will send a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" if the ambient ",
					UI.PRE_KEYWORD,
					"Radiation",
					UI.PST_KEYWORD,
					" is above <b>{0}</b>"
				});

				// Token: 0x0400C8A4 RID: 51364
				public static LocString RADIATION_TOOLTIP_BELOW = string.Concat(new string[]
				{
					"Will send a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" if the ambient ",
					UI.PRE_KEYWORD,
					"Radiation",
					UI.PST_KEYWORD,
					" is below <b>{0}</b>"
				});

				// Token: 0x0400C8A5 RID: 51365
				public static LocString HEPS = "Radbolt Reading";

				// Token: 0x0400C8A6 RID: 51366
				public static LocString HEPS_TOOLTIP_ABOVE = string.Concat(new string[]
				{
					"Will send a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" if the ",
					UI.PRE_KEYWORD,
					"Radbolts",
					UI.PST_KEYWORD,
					" is above <b>{0}</b>"
				});

				// Token: 0x0400C8A7 RID: 51367
				public static LocString HEPS_TOOLTIP_BELOW = string.Concat(new string[]
				{
					"Will send a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" if the ",
					UI.PRE_KEYWORD,
					"Radbolts",
					UI.PST_KEYWORD,
					" is below <b>{0}</b>"
				});
			}

			// Token: 0x02002E26 RID: 11814
			public class CAPACITY_CONTROL_SIDE_SCREEN
			{
				// Token: 0x0400C8A8 RID: 51368
				public static LocString TITLE = "Automated Storage Capacity";

				// Token: 0x0400C8A9 RID: 51369
				public static LocString MAX_LABEL = "Max:";
			}

			// Token: 0x02002E27 RID: 11815
			public class DOOR_TOGGLE_SIDE_SCREEN
			{
				// Token: 0x0400C8AA RID: 51370
				public static LocString TITLE = "Door Setting";

				// Token: 0x0400C8AB RID: 51371
				public static LocString OPEN = "Door is open.";

				// Token: 0x0400C8AC RID: 51372
				public static LocString AUTO = "Door is on auto.";

				// Token: 0x0400C8AD RID: 51373
				public static LocString CLOSE = "Door is locked.";

				// Token: 0x0400C8AE RID: 51374
				public static LocString PENDING_FORMAT = "{0} {1}";

				// Token: 0x0400C8AF RID: 51375
				public static LocString OPEN_PENDING = "Awaiting Duplicant to open door.";

				// Token: 0x0400C8B0 RID: 51376
				public static LocString AUTO_PENDING = "Awaiting Duplicant to automate door.";

				// Token: 0x0400C8B1 RID: 51377
				public static LocString CLOSE_PENDING = "Awaiting Duplicant to lock door.";

				// Token: 0x0400C8B2 RID: 51378
				public static LocString ACCESS_FORMAT = "{0}\n\n{1}";

				// Token: 0x0400C8B3 RID: 51379
				public static LocString ACCESS_OFFLINE = "Emergency Access Permissions:\nAll Duplicants are permitted to use this door until " + UI.FormatAsLink("Power", "POWER") + " is restored.";

				// Token: 0x0400C8B4 RID: 51380
				public static LocString POI_INTERNAL = "This door cannot be manually controlled.";
			}

			// Token: 0x02002E28 RID: 11816
			public class ACTIVATION_RANGE_SIDE_SCREEN
			{
				// Token: 0x0400C8B5 RID: 51381
				public static LocString NAME = "Breaktime Policy";

				// Token: 0x0400C8B6 RID: 51382
				public static LocString ACTIVATE = "Break starts at:";

				// Token: 0x0400C8B7 RID: 51383
				public static LocString DEACTIVATE = "Break ends at:";
			}

			// Token: 0x02002E29 RID: 11817
			public class CAPACITY_SIDE_SCREEN
			{
				// Token: 0x0400C8B8 RID: 51384
				public static LocString TOOLTIP = "Adjust the maximum amount that can be stored here";
			}

			// Token: 0x02002E2A RID: 11818
			public class SUIT_SIDE_SCREEN
			{
				// Token: 0x0400C8B9 RID: 51385
				public static LocString TITLE = "Dock Inventory";

				// Token: 0x0400C8BA RID: 51386
				public static LocString CONFIGURATION_REQUIRED = "Configuration Required:";

				// Token: 0x0400C8BB RID: 51387
				public static LocString CONFIG_REQUEST_SUIT = "Deliver Suit";

				// Token: 0x0400C8BC RID: 51388
				public static LocString CONFIG_REQUEST_SUIT_TOOLTIP = "Duplicants will immediately deliver and dock the nearest unequipped suit";

				// Token: 0x0400C8BD RID: 51389
				public static LocString CONFIG_NO_SUIT = "Leave Empty";

				// Token: 0x0400C8BE RID: 51390
				public static LocString CONFIG_NO_SUIT_TOOLTIP = "The next suited Duplicant to pass by will unequip their suit and dock it here";

				// Token: 0x0400C8BF RID: 51391
				public static LocString CONFIG_CANCEL_REQUEST = "Cancel Request";

				// Token: 0x0400C8C0 RID: 51392
				public static LocString CONFIG_CANCEL_REQUEST_TOOLTIP = "Cancel this suit delivery";

				// Token: 0x0400C8C1 RID: 51393
				public static LocString CONFIG_DROP_SUIT = "Undock Suit";

				// Token: 0x0400C8C2 RID: 51394
				public static LocString CONFIG_DROP_SUIT_TOOLTIP = "Disconnect this suit, dropping it on the ground";

				// Token: 0x0400C8C3 RID: 51395
				public static LocString CONFIG_DROP_SUIT_NO_SUIT_TOOLTIP = "There is no suit in this building to undock";
			}

			// Token: 0x02002E2B RID: 11819
			public class AUTOMATABLE_SIDE_SCREEN
			{
				// Token: 0x0400C8C4 RID: 51396
				public static LocString TITLE = "Automatable Storage";

				// Token: 0x0400C8C5 RID: 51397
				public static LocString ALLOWMANUALBUTTON = "Allow Manual Use";

				// Token: 0x0400C8C6 RID: 51398
				public static LocString ALLOWMANUALBUTTONTOOLTIP = "Allow Duplicants to manually manage these storage materials";
			}

			// Token: 0x02002E2C RID: 11820
			public class STUDYABLE_SIDE_SCREEN
			{
				// Token: 0x0400C8C7 RID: 51399
				public static LocString TITLE = "Analyze Natural Feature";

				// Token: 0x0400C8C8 RID: 51400
				public static LocString STUDIED_STATUS = "Researchers have completed their analysis and compiled their findings.";

				// Token: 0x0400C8C9 RID: 51401
				public static LocString STUDIED_BUTTON = "ANALYSIS COMPLETE";

				// Token: 0x0400C8CA RID: 51402
				public static LocString SEND_STATUS = "Send a researcher to gather data here.\n\nAnalyzing a feature takes time, but yields useful results.";

				// Token: 0x0400C8CB RID: 51403
				public static LocString SEND_BUTTON = "ANALYZE";

				// Token: 0x0400C8CC RID: 51404
				public static LocString PENDING_STATUS = "A researcher is in the process of studying this feature.";

				// Token: 0x0400C8CD RID: 51405
				public static LocString PENDING_BUTTON = "CANCEL ANALYSIS";
			}

			// Token: 0x02002E2D RID: 11821
			public class MEDICALCOTSIDESCREEN
			{
				// Token: 0x0400C8CE RID: 51406
				public static LocString TITLE = "Severity Requirement";

				// Token: 0x0400C8CF RID: 51407
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"A Duplicant may not use this cot until their ",
					UI.PRE_KEYWORD,
					"Health",
					UI.PST_KEYWORD,
					" falls below <b>{0}%</b>"
				});
			}

			// Token: 0x02002E2E RID: 11822
			public class WARPPORTALSIDESCREEN
			{
				// Token: 0x0400C8D0 RID: 51408
				public static LocString TITLE = "Teleporter";

				// Token: 0x0400C8D1 RID: 51409
				public static LocString IDLE = "Teleporter online.\nPlease select a passenger:";

				// Token: 0x0400C8D2 RID: 51410
				public static LocString WAITING = "Ready to transmit passenger.";

				// Token: 0x0400C8D3 RID: 51411
				public static LocString COMPLETE = "Passenger transmitted!";

				// Token: 0x0400C8D4 RID: 51412
				public static LocString UNDERWAY = "Transmitting passenger...";

				// Token: 0x0400C8D5 RID: 51413
				public static LocString CONSUMED = "Teleporter recharging:";

				// Token: 0x0400C8D6 RID: 51414
				public static LocString BUTTON = "Teleport!";

				// Token: 0x0400C8D7 RID: 51415
				public static LocString CANCELBUTTON = "Cancel";
			}

			// Token: 0x02002E2F RID: 11823
			public class RADBOLTTHRESHOLDSIDESCREEN
			{
				// Token: 0x0400C8D8 RID: 51416
				public static LocString TITLE = "Radbolt Threshold";

				// Token: 0x0400C8D9 RID: 51417
				public static LocString CURRENT_THRESHOLD = "Current Threshold: {0}%";

				// Token: 0x0400C8DA RID: 51418
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Releases a ",
					UI.PRE_KEYWORD,
					"Radbolt",
					UI.PST_KEYWORD,
					" when stored Radbolts exceed <b>{0}</b>"
				});

				// Token: 0x0400C8DB RID: 51419
				public static LocString PROGRESS_BAR_LABEL = "Radbolt Generation";

				// Token: 0x0400C8DC RID: 51420
				public static LocString PROGRESS_BAR_TOOLTIP = string.Concat(new string[]
				{
					"The building will emit a ",
					UI.PRE_KEYWORD,
					"Radbolt",
					UI.PST_KEYWORD,
					" in the chosen direction when fully charged"
				});
			}

			// Token: 0x02002E30 RID: 11824
			public class LOGICBITSELECTORSIDESCREEN
			{
				// Token: 0x0400C8DD RID: 51421
				public static LocString RIBBON_READER_TITLE = "Ribbon Reader";

				// Token: 0x0400C8DE RID: 51422
				public static LocString RIBBON_READER_DESCRIPTION = "Selected <b>Bit's Signal</b> will be read by the <b>Output Port</b>";

				// Token: 0x0400C8DF RID: 51423
				public static LocString RIBBON_WRITER_TITLE = "Ribbon Writer";

				// Token: 0x0400C8E0 RID: 51424
				public static LocString RIBBON_WRITER_DESCRIPTION = "Received <b>Signal</b> will be written to selected <b>Bit</b>";

				// Token: 0x0400C8E1 RID: 51425
				public static LocString BIT = "Bit {0}";

				// Token: 0x0400C8E2 RID: 51426
				public static LocString STATE_ACTIVE = "Green";

				// Token: 0x0400C8E3 RID: 51427
				public static LocString STATE_INACTIVE = "Red";
			}

			// Token: 0x02002E31 RID: 11825
			public class LOGICALARMSIDESCREEN
			{
				// Token: 0x0400C8E4 RID: 51428
				public static LocString TITLE = "Notification Designer";

				// Token: 0x0400C8E5 RID: 51429
				public static LocString DESCRIPTION = "Notification will be sent upon receiving a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + "\n\nMaking modifications will clear any existing notifications being sent by this building.";

				// Token: 0x0400C8E6 RID: 51430
				public static LocString NAME = "<b>Name:</b>";

				// Token: 0x0400C8E7 RID: 51431
				public static LocString NAME_DEFAULT = "Notification";

				// Token: 0x0400C8E8 RID: 51432
				public static LocString TOOLTIP = "<b>Tooltip:</b>";

				// Token: 0x0400C8E9 RID: 51433
				public static LocString TOOLTIP_DEFAULT = "Tooltip";

				// Token: 0x0400C8EA RID: 51434
				public static LocString TYPE = "<b>Type:</b>";

				// Token: 0x0400C8EB RID: 51435
				public static LocString PAUSE = "<b>Pause:</b>";

				// Token: 0x0400C8EC RID: 51436
				public static LocString ZOOM = "<b>Zoom:</b>";

				// Token: 0x02003AA6 RID: 15014
				public class TOOLTIPS
				{
					// Token: 0x0400E910 RID: 59664
					public static LocString NAME = "Select notification text";

					// Token: 0x0400E911 RID: 59665
					public static LocString TOOLTIP = "Select notification hover text";

					// Token: 0x0400E912 RID: 59666
					public static LocString TYPE = "Select the visual and aural style of the notification";

					// Token: 0x0400E913 RID: 59667
					public static LocString PAUSE = "Time will pause upon notification when checked";

					// Token: 0x0400E914 RID: 59668
					public static LocString ZOOM = "The view will zoom to this building upon notification when checked";

					// Token: 0x0400E915 RID: 59669
					public static LocString BAD = "\"Boing boing!\"";

					// Token: 0x0400E916 RID: 59670
					public static LocString NEUTRAL = "\"Pop!\"";

					// Token: 0x0400E917 RID: 59671
					public static LocString DUPLICANT_THREATENING = "AHH!";
				}
			}

			// Token: 0x02002E32 RID: 11826
			public class GENETICANALYSISSIDESCREEN
			{
				// Token: 0x0400C8ED RID: 51437
				public static LocString TITLE = "Genetic Analysis";

				// Token: 0x0400C8EE RID: 51438
				public static LocString NONE_DISCOVERED = "No mutant seeds have been found.";

				// Token: 0x0400C8EF RID: 51439
				public static LocString SELECT_SEEDS = "Select which seed types to analyze:";

				// Token: 0x0400C8F0 RID: 51440
				public static LocString SEED_NO_MUTANTS = "</i>No mutants found</i>";

				// Token: 0x0400C8F1 RID: 51441
				public static LocString SEED_FORBIDDEN = "</i>Won't analyze</i>";

				// Token: 0x0400C8F2 RID: 51442
				public static LocString SEED_ALLOWED = "</i>Will analyze</i>";
			}

			// Token: 0x02002E33 RID: 11827
			public class RELATEDENTITIESSIDESCREEN
			{
				// Token: 0x0400C8F3 RID: 51443
				public static LocString TITLE = "Related Objects";
			}
		}

		// Token: 0x020023E6 RID: 9190
		public class USERMENUACTIONS
		{
			// Token: 0x02002E34 RID: 11828
			public class TINKER
			{
				// Token: 0x0400C8F4 RID: 51444
				public static LocString ALLOW = "Allow Tinker";

				// Token: 0x0400C8F5 RID: 51445
				public static LocString DISALLOW = "Disallow Tinker";

				// Token: 0x0400C8F6 RID: 51446
				public static LocString TOOLTIP_DISALLOW = "Disallow {0} on this {1}";

				// Token: 0x0400C8F7 RID: 51447
				public static LocString TOOLTIP_ALLOW = "Allow  {0} on this {1}";
			}

			// Token: 0x02002E35 RID: 11829
			public class TRANSITTUBEWAX
			{
				// Token: 0x0400C8F8 RID: 51448
				public static LocString NAME = "Enable Smooth Ride";

				// Token: 0x0400C8F9 RID: 51449
				public static LocString TOOLTIP = "Enables the use of " + ELEMENTS.MILKFAT.NAME + " to boost travel speed";
			}

			// Token: 0x02002E36 RID: 11830
			public class CANCELTRANSITTUBEWAX
			{
				// Token: 0x0400C8FA RID: 51450
				public static LocString NAME = "Disable Smooth Ride";

				// Token: 0x0400C8FB RID: 51451
				public static LocString TOOLTIP = "Disables travel speed boost and refunds stored " + ELEMENTS.MILKFAT.NAME;
			}

			// Token: 0x02002E37 RID: 11831
			public class CLEANTOILET
			{
				// Token: 0x0400C8FC RID: 51452
				public static LocString NAME = "Clean Toilet";

				// Token: 0x0400C8FD RID: 51453
				public static LocString TOOLTIP = "Empty waste from this toilet";
			}

			// Token: 0x02002E38 RID: 11832
			public class CANCELCLEANTOILET
			{
				// Token: 0x0400C8FE RID: 51454
				public static LocString NAME = "Cancel Clean";

				// Token: 0x0400C8FF RID: 51455
				public static LocString TOOLTIP = "Cancel this cleaning order";
			}

			// Token: 0x02002E39 RID: 11833
			public class EMPTYBEEHIVE
			{
				// Token: 0x0400C900 RID: 51456
				public static LocString NAME = "Enable Autoharvest";

				// Token: 0x0400C901 RID: 51457
				public static LocString TOOLTIP = "Automatically harvest this hive when full";
			}

			// Token: 0x02002E3A RID: 11834
			public class CANCELEMPTYBEEHIVE
			{
				// Token: 0x0400C902 RID: 51458
				public static LocString NAME = "Disable Autoharvest";

				// Token: 0x0400C903 RID: 51459
				public static LocString TOOLTIP = "Do not automatically harvest this hive";
			}

			// Token: 0x02002E3B RID: 11835
			public class EMPTYDESALINATOR
			{
				// Token: 0x0400C904 RID: 51460
				public static LocString NAME = "Empty Desalinator";

				// Token: 0x0400C905 RID: 51461
				public static LocString TOOLTIP = "Empty salt from this desalinator";
			}

			// Token: 0x02002E3C RID: 11836
			public class CHANGE_ROOM
			{
				// Token: 0x0400C906 RID: 51462
				public static LocString REQUEST_OUTFIT = "Request Outfit";

				// Token: 0x0400C907 RID: 51463
				public static LocString REQUEST_OUTFIT_TOOLTIP = "Request outfit to be delivered to this change room";

				// Token: 0x0400C908 RID: 51464
				public static LocString CANCEL_REQUEST = "Cancel Request";

				// Token: 0x0400C909 RID: 51465
				public static LocString CANCEL_REQUEST_TOOLTIP = "Cancel outfit request";

				// Token: 0x0400C90A RID: 51466
				public static LocString DROP_OUTFIT = "Drop Outfit";

				// Token: 0x0400C90B RID: 51467
				public static LocString DROP_OUTFIT_TOOLTIP = "Drop outfit on floor";
			}

			// Token: 0x02002E3D RID: 11837
			public class DUMP
			{
				// Token: 0x0400C90C RID: 51468
				public static LocString NAME = "Empty";

				// Token: 0x0400C90D RID: 51469
				public static LocString TOOLTIP = "Dump bottle contents onto the floor";

				// Token: 0x0400C90E RID: 51470
				public static LocString NAME_OFF = "Cancel Empty";

				// Token: 0x0400C90F RID: 51471
				public static LocString TOOLTIP_OFF = "Cancel this empty order";
			}

			// Token: 0x02002E3E RID: 11838
			public class TAGFILTER
			{
				// Token: 0x0400C910 RID: 51472
				public static LocString NAME = "Filter Settings";

				// Token: 0x0400C911 RID: 51473
				public static LocString TOOLTIP = "Assign materials to storage";
			}

			// Token: 0x02002E3F RID: 11839
			public class CANCELCONSTRUCTION
			{
				// Token: 0x0400C912 RID: 51474
				public static LocString NAME = "Cancel Build";

				// Token: 0x0400C913 RID: 51475
				public static LocString TOOLTIP = "Cancel this build order";
			}

			// Token: 0x02002E40 RID: 11840
			public class DIG
			{
				// Token: 0x0400C914 RID: 51476
				public static LocString NAME = "Dig";

				// Token: 0x0400C915 RID: 51477
				public static LocString TOOLTIP = "Dig out this cell";

				// Token: 0x0400C916 RID: 51478
				public static LocString TOOLTIP_OFF = "Cancel this dig order";
			}

			// Token: 0x02002E41 RID: 11841
			public class CANCELMOP
			{
				// Token: 0x0400C917 RID: 51479
				public static LocString NAME = "Cancel Mop";

				// Token: 0x0400C918 RID: 51480
				public static LocString TOOLTIP = "Cancel this mop order";
			}

			// Token: 0x02002E42 RID: 11842
			public class CANCELDIG
			{
				// Token: 0x0400C919 RID: 51481
				public static LocString NAME = "Cancel Dig";

				// Token: 0x0400C91A RID: 51482
				public static LocString TOOLTIP = "Cancel this dig order";
			}

			// Token: 0x02002E43 RID: 11843
			public class UPROOT
			{
				// Token: 0x0400C91B RID: 51483
				public static LocString NAME = "Uproot";

				// Token: 0x0400C91C RID: 51484
				public static LocString TOOLTIP = "Convert this plant into a seed";
			}

			// Token: 0x02002E44 RID: 11844
			public class CANCELUPROOT
			{
				// Token: 0x0400C91D RID: 51485
				public static LocString NAME = "Cancel Uproot";

				// Token: 0x0400C91E RID: 51486
				public static LocString TOOLTIP = "Cancel this uproot order";
			}

			// Token: 0x02002E45 RID: 11845
			public class HARVEST_WHEN_READY
			{
				// Token: 0x0400C91F RID: 51487
				public static LocString NAME = "Enable Autoharvest";

				// Token: 0x0400C920 RID: 51488
				public static LocString TOOLTIP = "Automatically harvest this plant when it matures";
			}

			// Token: 0x02002E46 RID: 11846
			public class CANCEL_HARVEST_WHEN_READY
			{
				// Token: 0x0400C921 RID: 51489
				public static LocString NAME = "Disable Autoharvest";

				// Token: 0x0400C922 RID: 51490
				public static LocString TOOLTIP = "Do not automatically harvest this plant";
			}

			// Token: 0x02002E47 RID: 11847
			public class HARVEST
			{
				// Token: 0x0400C923 RID: 51491
				public static LocString NAME = "Harvest";

				// Token: 0x0400C924 RID: 51492
				public static LocString TOOLTIP = "Harvest materials from this plant";

				// Token: 0x0400C925 RID: 51493
				public static LocString TOOLTIP_DISABLED = "This plant has nothing to harvest";
			}

			// Token: 0x02002E48 RID: 11848
			public class CANCELHARVEST
			{
				// Token: 0x0400C926 RID: 51494
				public static LocString NAME = "Cancel Harvest";

				// Token: 0x0400C927 RID: 51495
				public static LocString TOOLTIP = "Cancel this harvest order";
			}

			// Token: 0x02002E49 RID: 11849
			public class ATTACK
			{
				// Token: 0x0400C928 RID: 51496
				public static LocString NAME = "Attack";

				// Token: 0x0400C929 RID: 51497
				public static LocString TOOLTIP = "Attack this critter";
			}

			// Token: 0x02002E4A RID: 11850
			public class CANCELATTACK
			{
				// Token: 0x0400C92A RID: 51498
				public static LocString NAME = "Cancel Attack";

				// Token: 0x0400C92B RID: 51499
				public static LocString TOOLTIP = "Cancel this attack order";
			}

			// Token: 0x02002E4B RID: 11851
			public class CAPTURE
			{
				// Token: 0x0400C92C RID: 51500
				public static LocString NAME = "Wrangle";

				// Token: 0x0400C92D RID: 51501
				public static LocString TOOLTIP = "Capture this critter alive";
			}

			// Token: 0x02002E4C RID: 11852
			public class CANCELCAPTURE
			{
				// Token: 0x0400C92E RID: 51502
				public static LocString NAME = "Cancel Wrangle";

				// Token: 0x0400C92F RID: 51503
				public static LocString TOOLTIP = "Cancel this wrangle order";
			}

			// Token: 0x02002E4D RID: 11853
			public class RELEASEELEMENT
			{
				// Token: 0x0400C930 RID: 51504
				public static LocString NAME = "Empty Building";

				// Token: 0x0400C931 RID: 51505
				public static LocString TOOLTIP = "Refund all resources currently in use by this building";
			}

			// Token: 0x02002E4E RID: 11854
			public class DECONSTRUCT
			{
				// Token: 0x0400C932 RID: 51506
				public static LocString NAME = "Deconstruct";

				// Token: 0x0400C933 RID: 51507
				public static LocString TOOLTIP = "Deconstruct this building and refund all resources";

				// Token: 0x0400C934 RID: 51508
				public static LocString NAME_OFF = "Cancel Deconstruct";

				// Token: 0x0400C935 RID: 51509
				public static LocString TOOLTIP_OFF = "Cancel this deconstruct order";
			}

			// Token: 0x02002E4F RID: 11855
			public class DEMOLISH
			{
				// Token: 0x0400C936 RID: 51510
				public static LocString NAME = "Demolish";

				// Token: 0x0400C937 RID: 51511
				public static LocString TOOLTIP = "Demolish this building";

				// Token: 0x0400C938 RID: 51512
				public static LocString NAME_OFF = "Cancel Demolition";

				// Token: 0x0400C939 RID: 51513
				public static LocString TOOLTIP_OFF = "Cancel this demolition order";
			}

			// Token: 0x02002E50 RID: 11856
			public class ROCKETUSAGERESTRICTION
			{
				// Token: 0x0400C93A RID: 51514
				public static LocString NAME_UNCONTROLLED = "Uncontrolled";

				// Token: 0x0400C93B RID: 51515
				public static LocString TOOLTIP_UNCONTROLLED = "Do not allow this building to be controlled by a " + BUILDINGS.PREFABS.ROCKETCONTROLSTATION.NAME;

				// Token: 0x0400C93C RID: 51516
				public static LocString NAME_CONTROLLED = "Controlled";

				// Token: 0x0400C93D RID: 51517
				public static LocString TOOLTIP_CONTROLLED = "Allow this building's operation to be controlled by a " + BUILDINGS.PREFABS.ROCKETCONTROLSTATION.NAME;
			}

			// Token: 0x02002E51 RID: 11857
			public class MANUAL_DELIVERY
			{
				// Token: 0x0400C93E RID: 51518
				public static LocString NAME = "Disable Delivery";

				// Token: 0x0400C93F RID: 51519
				public static LocString TOOLTIP = "Do not deliver materials to this building";

				// Token: 0x0400C940 RID: 51520
				public static LocString NAME_OFF = "Enable Delivery";

				// Token: 0x0400C941 RID: 51521
				public static LocString TOOLTIP_OFF = "Deliver materials to this building";
			}

			// Token: 0x02002E52 RID: 11858
			public class SELECTRESEARCH
			{
				// Token: 0x0400C942 RID: 51522
				public static LocString NAME = "Select Research";

				// Token: 0x0400C943 RID: 51523
				public static LocString TOOLTIP = "Choose a technology from the " + UI.FormatAsManagementMenu("Research Tree", global::Action.ManageResearch);
			}

			// Token: 0x02002E53 RID: 11859
			public class RECONSTRUCT
			{
				// Token: 0x0400C944 RID: 51524
				public static LocString REQUEST_RECONSTRUCT = "Order Rebuild";

				// Token: 0x0400C945 RID: 51525
				public static LocString REQUEST_RECONSTRUCT_TOOLTIP = "Deconstruct this building and rebuild it using the selected material";

				// Token: 0x0400C946 RID: 51526
				public static LocString CANCEL_RECONSTRUCT = "Cancel Rebuild Order";

				// Token: 0x0400C947 RID: 51527
				public static LocString CANCEL_RECONSTRUCT_TOOLTIP = "Cancel deconstruction and rebuilding of this building";
			}

			// Token: 0x02002E54 RID: 11860
			public class RELOCATE
			{
				// Token: 0x0400C948 RID: 51528
				public static LocString NAME = "Relocate";

				// Token: 0x0400C949 RID: 51529
				public static LocString TOOLTIP = "Move this building to a new location\n\nCosts no additional resources";

				// Token: 0x0400C94A RID: 51530
				public static LocString NAME_OFF = "Cancel Relocation";

				// Token: 0x0400C94B RID: 51531
				public static LocString TOOLTIP_OFF = "Cancel this relocation order";
			}

			// Token: 0x02002E55 RID: 11861
			public class ENABLEBUILDING
			{
				// Token: 0x0400C94C RID: 51532
				public static LocString NAME = "Disable Building";

				// Token: 0x0400C94D RID: 51533
				public static LocString TOOLTIP = "Halt the use of this building {Hotkey}\n\nDisabled buildings consume no energy or resources";

				// Token: 0x0400C94E RID: 51534
				public static LocString NAME_OFF = "Enable Building";

				// Token: 0x0400C94F RID: 51535
				public static LocString TOOLTIP_OFF = "Resume the use of this building {Hotkey}";
			}

			// Token: 0x02002E56 RID: 11862
			public class READLORE
			{
				// Token: 0x0400C950 RID: 51536
				public static LocString NAME = "Inspect";

				// Token: 0x0400C951 RID: 51537
				public static LocString ALREADYINSPECTED = "Already inspected";

				// Token: 0x0400C952 RID: 51538
				public static LocString TOOLTIP = "Recover files from this structure";

				// Token: 0x0400C953 RID: 51539
				public static LocString TOOLTIP_ALREADYINSPECTED = "This structure has already been inspected";

				// Token: 0x0400C954 RID: 51540
				public static LocString GOTODATABASE = "View Entry";

				// Token: 0x0400C955 RID: 51541
				public static LocString SEARCH_DISPLAY = "The display is still functional. I copy its message into my database.\n\nNew Database Entry discovered.";

				// Token: 0x0400C956 RID: 51542
				public static LocString SEARCH_ELLIESDESK = "All I find on the machine is a curt e-mail from a disgruntled employee.\n\nNew Database Entry discovered.";

				// Token: 0x0400C957 RID: 51543
				public static LocString SEARCH_POD = "I search my incoming message history and find a single entry. I move the odd message into my database.\n\nNew Database Entry discovered.";

				// Token: 0x0400C958 RID: 51544
				public static LocString ALREADY_SEARCHED = "I already took everything of interest from this. I can check the Database to re-read what I found.";

				// Token: 0x0400C959 RID: 51545
				public static LocString SEARCH_CABINET = "One intact document remains - an old yellowing newspaper clipping. It won't be of much use, but I add it to my database nonetheless.\n\nNew Database Entry discovered.";

				// Token: 0x0400C95A RID: 51546
				public static LocString SEARCH_STERNSDESK = "There's an old magazine article from a publication called the \"Nucleoid\" tucked in the top drawer. I add it to my database.\n\nNew Database Entry discovered.";

				// Token: 0x0400C95B RID: 51547
				public static LocString ALREADY_SEARCHED_STERNSDESK = "The desk is eerily empty inside.";

				// Token: 0x0400C95C RID: 51548
				public static LocString SEARCH_TELEPORTER_SENDER = "While scanning the antiquated computer code of this machine I uncovered some research notes. I add them to my database.\n\nNew Database Entry discovered.";

				// Token: 0x0400C95D RID: 51549
				public static LocString SEARCH_TELEPORTER_RECEIVER = "Incongruously placed research notes are hidden within the operating instructions of this device. I add them to my database.\n\nNew Database Entry discovered.";

				// Token: 0x0400C95E RID: 51550
				public static LocString SEARCH_CRYO_TANK = "There are some safety instructions included in the operating instructions of this Cryotank. I add them to my database.\n\nNew Database Entry discovered.";

				// Token: 0x0400C95F RID: 51551
				public static LocString SEARCH_PROPGRAVITASCREATUREPOSTER = "There's a handwritten note taped to the back of this poster. I add it to my database.\n\nNew Database Entry discovered.";

				// Token: 0x02003AA7 RID: 15015
				public class SEARCH_COMPUTER_PODIUM
				{
					// Token: 0x0400E918 RID: 59672
					public static LocString SEARCH1 = "I search through the computer's database and find an unredacted e-mail.\n\nNew Database Entry unlocked.";
				}

				// Token: 0x02003AA8 RID: 15016
				public class SEARCH_COMPUTER_SUCCESS
				{
					// Token: 0x0400E919 RID: 59673
					public static LocString SEARCH1 = "After searching through the computer's database, I managed to piece together some files that piqued my interest.\n\nNew Database Entry unlocked.";

					// Token: 0x0400E91A RID: 59674
					public static LocString SEARCH2 = "Searching through the computer, I find some recoverable files that are still readable.\n\nNew Database Entry unlocked.";

					// Token: 0x0400E91B RID: 59675
					public static LocString SEARCH3 = "The computer looks pristine on the outside, but is corrupted internally. Still, I managed to find one uncorrupted file, and have added it to my database.\n\nNew Database Entry unlocked.";

					// Token: 0x0400E91C RID: 59676
					public static LocString SEARCH4 = "The computer was wiped almost completely clean, except for one file hidden in the recycle bin.\n\nNew Database Entry unlocked.";

					// Token: 0x0400E91D RID: 59677
					public static LocString SEARCH5 = "I search the computer, storing what useful data I can find in my own memory.\n\nNew Database Entry unlocked.";

					// Token: 0x0400E91E RID: 59678
					public static LocString SEARCH6 = "This computer is broken and requires some finessing to get working. Still, I recover a handful of interesting files.\n\nNew Database Entry unlocked.";
				}

				// Token: 0x02003AA9 RID: 15017
				public class SEARCH_COMPUTER_FAIL
				{
					// Token: 0x0400E91F RID: 59679
					public static LocString SEARCH1 = "Unfortunately, the computer's hard drive is irreparably corrupted.";

					// Token: 0x0400E920 RID: 59680
					public static LocString SEARCH2 = "The computer was wiped clean before I got here. There is nothing to recover.";

					// Token: 0x0400E921 RID: 59681
					public static LocString SEARCH3 = "Some intact files are available on the computer, but nothing I haven't already discovered elsewhere. I find nothing else.";

					// Token: 0x0400E922 RID: 59682
					public static LocString SEARCH4 = "The computer has nothing of import.";

					// Token: 0x0400E923 RID: 59683
					public static LocString SEARCH5 = "Someone's left a solitaire game up. There's nothing else of interest on the computer.\n\nAlso, it looks as though they were about to lose.";

					// Token: 0x0400E924 RID: 59684
					public static LocString SEARCH6 = "The background on this computer depicts two kittens hugging in a field of daisies. There is nothing else of import to be found.";

					// Token: 0x0400E925 RID: 59685
					public static LocString SEARCH7 = "The user alphabetized the shortcuts on their desktop. There is nothing else of import to be found.";

					// Token: 0x0400E926 RID: 59686
					public static LocString SEARCH8 = "The background is a picture of a golden retriever in a science lab. It looks very confused. There is nothing else of import to be found.";

					// Token: 0x0400E927 RID: 59687
					public static LocString SEARCH9 = "This user never changed their default background. There is nothing else of import to be found. How dull.";
				}

				// Token: 0x02003AAA RID: 15018
				public class SEARCH_TECHNOLOGY_SUCCESS
				{
					// Token: 0x0400E928 RID: 59688
					public static LocString SEARCH1 = "I scour the internal systems and find something of interest.\n\nNew Database Entry discovered.";

					// Token: 0x0400E929 RID: 59689
					public static LocString SEARCH2 = "I see if I can salvage anything from the electronics. I add what I find to my database.\n\nNew Database Entry discovered.";

					// Token: 0x0400E92A RID: 59690
					public static LocString SEARCH3 = "I look for anything of interest within the abandoned machinery and add what I find to my database.\n\nNew Database Entry discovered.";
				}

				// Token: 0x02003AAB RID: 15019
				public class SEARCH_OBJECT_SUCCESS
				{
					// Token: 0x0400E92B RID: 59691
					public static LocString SEARCH1 = "I look around and recover an old file.\n\nNew Database Entry discovered.";

					// Token: 0x0400E92C RID: 59692
					public static LocString SEARCH2 = "There's a three-ringed binder inside. I scan the surviving documents.\n\nNew Database Entry discovered.";

					// Token: 0x0400E92D RID: 59693
					public static LocString SEARCH3 = "A discarded journal inside remains mostly intact. I scan the pages of use.\n\nNew Database Entry discovered.";

					// Token: 0x0400E92E RID: 59694
					public static LocString SEARCH4 = "A single page of a long printout remains legible. I scan it and add it to my database.\n\nNew Database Entry discovered.";

					// Token: 0x0400E92F RID: 59695
					public static LocString SEARCH5 = "A few loose papers can be found inside. I scan the ones that look interesting.\n\nNew Database Entry discovered.";

					// Token: 0x0400E930 RID: 59696
					public static LocString SEARCH6 = "I find a memory stick inside and copy its data into my database.\n\nNew Database Entry discovered.";
				}

				// Token: 0x02003AAC RID: 15020
				public class SEARCH_OBJECT_FAIL
				{
					// Token: 0x0400E931 RID: 59697
					public static LocString SEARCH1 = "I look around but find nothing of interest.";
				}

				// Token: 0x02003AAD RID: 15021
				public class SEARCH_SPACEPOI_SUCCESS
				{
					// Token: 0x0400E932 RID: 59698
					public static LocString SEARCH1 = "A quick analysis of the hardware of this debris has uncovered some searchable files within.\n\nNew Database Entry unlocked.";

					// Token: 0x0400E933 RID: 59699
					public static LocString SEARCH2 = "There's an archaic interface I can interact with on this device.\n\nNew Database Entry unlocked.";

					// Token: 0x0400E934 RID: 59700
					public static LocString SEARCH3 = "While investigating the software of this wreckage, a compelling file comes to my attention.\n\nNew Database Entry unlocked.";

					// Token: 0x0400E935 RID: 59701
					public static LocString SEARCH4 = "Not much remains of the software that once ran this spacecraft except for one file that piques my interest.\n\nNew Database Entry unlocked.";

					// Token: 0x0400E936 RID: 59702
					public static LocString SEARCH5 = "I find some noteworthy data hidden amongst the system files of this space junk.\n\nNew Database Entry unlocked.";

					// Token: 0x0400E937 RID: 59703
					public static LocString SEARCH6 = "Despite being subjected to years of degradation, there are still recoverable files in this machinery.\n\nNew Database Entry unlocked.";
				}

				// Token: 0x02003AAE RID: 15022
				public class SEARCH_SPACEPOI_FAIL
				{
					// Token: 0x0400E938 RID: 59704
					public static LocString SEARCH1 = "There's nothing of interest left in this old space junk.";

					// Token: 0x0400E939 RID: 59705
					public static LocString SEARCH2 = "I've salvaged everything I can from this vehicle.";

					// Token: 0x0400E93A RID: 59706
					public static LocString SEARCH3 = "Years of neglect and radioactive decay have destroyed all the useful data from this derelict spacecraft.";
				}

				// Token: 0x02003AAF RID: 15023
				public class SEARCH_DISPLAY_FAIL
				{
					// Token: 0x0400E93B RID: 59707
					public static LocString SEARCH1 = "The display is frozen. Whatever information it once contained is long gone.";
				}
			}

			// Token: 0x02002E57 RID: 11863
			public class OPENPOI
			{
				// Token: 0x0400C960 RID: 51552
				public static LocString NAME = "Rummage";

				// Token: 0x0400C961 RID: 51553
				public static LocString TOOLTIP = "Scrounge for usable materials";

				// Token: 0x0400C962 RID: 51554
				public static LocString NAME_OFF = "Cancel Rummage";

				// Token: 0x0400C963 RID: 51555
				public static LocString TOOLTIP_OFF = "Cancel this rummage order";

				// Token: 0x0400C964 RID: 51556
				public static LocString ALREADY_RUMMAGED = "Already Rummaged";

				// Token: 0x0400C965 RID: 51557
				public static LocString TOOLTIP_ALREADYRUMMAGED = "There are no usable materials left to find";
			}

			// Token: 0x02002E58 RID: 11864
			public class OPEN_TECHUNLOCKS
			{
				// Token: 0x0400C966 RID: 51558
				public static LocString NAME = "Unlock Portal";

				// Token: 0x0400C967 RID: 51559
				public static LocString TOOLTIP = "Retrieve data stored in this building";

				// Token: 0x0400C968 RID: 51560
				public static LocString NAME_OFF = "Cancel Unlock Portal";

				// Token: 0x0400C969 RID: 51561
				public static LocString TOOLTIP_OFF = "Cancel this portal access order";

				// Token: 0x0400C96A RID: 51562
				public static LocString ALREADY_RUMMAGED = "Already Unlocked";

				// Token: 0x0400C96B RID: 51563
				public static LocString TOOLTIP_ALREADYRUMMAGED = "All data has been accessed and recorded";
			}

			// Token: 0x02002E59 RID: 11865
			public class UNLOCK_ASTEROID_VISUALIZER
			{
				// Token: 0x0400C96C RID: 51564
				public static LocString NAME = "Inspect";

				// Token: 0x0400C96D RID: 51565
				public static LocString TOOLTIP = "Something here looks ominous\n\nClick to take a closer look";

				// Token: 0x0400C96E RID: 51566
				public static LocString NAME_OFF = "Cancel Inspect";

				// Token: 0x0400C96F RID: 51567
				public static LocString TOOLTIP_OFF = "Cancel this inspection";

				// Token: 0x0400C970 RID: 51568
				public static LocString ALREADY_RUMMAGED = "Already Inspected";

				// Token: 0x0400C971 RID: 51569
				public static LocString TOOLTIP_ALREADYRUMMAGED = "This has been thoroughly inspected";
			}

			// Token: 0x02002E5A RID: 11866
			public class EMPTYSTORAGE
			{
				// Token: 0x0400C972 RID: 51570
				public static LocString NAME = "Empty Storage";

				// Token: 0x0400C973 RID: 51571
				public static LocString TOOLTIP = "Eject all resources from this container";

				// Token: 0x0400C974 RID: 51572
				public static LocString NAME_OFF = "Cancel Empty";

				// Token: 0x0400C975 RID: 51573
				public static LocString TOOLTIP_OFF = "Cancel this empty order";
			}

			// Token: 0x02002E5B RID: 11867
			public class CLOSESTORAGE
			{
				// Token: 0x0400C976 RID: 51574
				public static LocString NAME = "Close Storage";

				// Token: 0x0400C977 RID: 51575
				public static LocString TOOLTIP = "Prevent this container from receiving resources for storage";

				// Token: 0x0400C978 RID: 51576
				public static LocString NAME_OFF = "Cancel Close";

				// Token: 0x0400C979 RID: 51577
				public static LocString TOOLTIP_OFF = "Cancel this close order";
			}

			// Token: 0x02002E5C RID: 11868
			public class COPY_BUILDING_SETTINGS
			{
				// Token: 0x0400C97A RID: 51578
				public static LocString NAME = "Copy Settings";

				// Token: 0x0400C97B RID: 51579
				public static LocString TOOLTIP = "Apply the settings and priorities of this building to other buildings of the same type {Hotkey}";
			}

			// Token: 0x02002E5D RID: 11869
			public class CLEAR
			{
				// Token: 0x0400C97C RID: 51580
				public static LocString NAME = "Sweep";

				// Token: 0x0400C97D RID: 51581
				public static LocString TOOLTIP = "Put this object away in the nearest storage container";

				// Token: 0x0400C97E RID: 51582
				public static LocString NAME_OFF = "Cancel Sweeping";

				// Token: 0x0400C97F RID: 51583
				public static LocString TOOLTIP_OFF = "Cancel this sweep order";
			}

			// Token: 0x02002E5E RID: 11870
			public class COMPOST
			{
				// Token: 0x0400C980 RID: 51584
				public static LocString NAME = "Compost";

				// Token: 0x0400C981 RID: 51585
				public static LocString TOOLTIP = "Mark this object for compost";

				// Token: 0x0400C982 RID: 51586
				public static LocString NAME_OFF = "Cancel Compost";

				// Token: 0x0400C983 RID: 51587
				public static LocString TOOLTIP_OFF = "Cancel this compost order";
			}

			// Token: 0x02002E5F RID: 11871
			public class PICKUPABLEMOVE
			{
				// Token: 0x0400C984 RID: 51588
				public static LocString NAME = "Relocate To";

				// Token: 0x0400C985 RID: 51589
				public static LocString TOOLTIP = "Relocate this object to a specific location";

				// Token: 0x0400C986 RID: 51590
				public static LocString NAME_OFF = "Cancel Relocate";

				// Token: 0x0400C987 RID: 51591
				public static LocString TOOLTIP_OFF = "Cancel order to relocate this object";
			}

			// Token: 0x02002E60 RID: 11872
			public class UNEQUIP
			{
				// Token: 0x0400C988 RID: 51592
				public static LocString NAME = "Unequip {0}";

				// Token: 0x0400C989 RID: 51593
				public static LocString TOOLTIP = "Take off and drop this equipment";
			}

			// Token: 0x02002E61 RID: 11873
			public class QUARANTINE
			{
				// Token: 0x0400C98A RID: 51594
				public static LocString NAME = "Quarantine";

				// Token: 0x0400C98B RID: 51595
				public static LocString TOOLTIP = "Isolate this Duplicant\nThe Duplicant will return to their assigned Cot";

				// Token: 0x0400C98C RID: 51596
				public static LocString TOOLTIP_DISABLED = "No quarantine zone assigned";

				// Token: 0x0400C98D RID: 51597
				public static LocString NAME_OFF = "Cancel Quarantine";

				// Token: 0x0400C98E RID: 51598
				public static LocString TOOLTIP_OFF = "Cancel this quarantine order";
			}

			// Token: 0x02002E62 RID: 11874
			public class DRAWPATHS
			{
				// Token: 0x0400C98F RID: 51599
				public static LocString NAME = "Show Navigation";

				// Token: 0x0400C990 RID: 51600
				public static LocString TOOLTIP = "Show all areas within this Duplicant's reach";

				// Token: 0x0400C991 RID: 51601
				public static LocString NAME_OFF = "Hide Navigation";

				// Token: 0x0400C992 RID: 51602
				public static LocString TOOLTIP_OFF = "Hide areas within this Duplicant's reach";
			}

			// Token: 0x02002E63 RID: 11875
			public class MOVETOLOCATION
			{
				// Token: 0x0400C993 RID: 51603
				public static LocString NAME = "Move To";

				// Token: 0x0400C994 RID: 51604
				public static LocString TOOLTIP = "Move this Duplicant to a specific location";
			}

			// Token: 0x02002E64 RID: 11876
			public class FOLLOWCAM
			{
				// Token: 0x0400C995 RID: 51605
				public static LocString NAME = "Follow Cam";

				// Token: 0x0400C996 RID: 51606
				public static LocString TOOLTIP = "Track this Duplicant with the camera";
			}

			// Token: 0x02002E65 RID: 11877
			public class WORKABLE_DIRECTION_BOTH
			{
				// Token: 0x0400C997 RID: 51607
				public static LocString NAME = "Set Direction: Both";

				// Token: 0x0400C998 RID: 51608
				public static LocString TOOLTIP = "Select to make Duplicants wash when passing by in either direction";
			}

			// Token: 0x02002E66 RID: 11878
			public class WORKABLE_DIRECTION_LEFT
			{
				// Token: 0x0400C999 RID: 51609
				public static LocString NAME = "Set Direction: Left";

				// Token: 0x0400C99A RID: 51610
				public static LocString TOOLTIP = "Select to make Duplicants wash when passing by from right to left";
			}

			// Token: 0x02002E67 RID: 11879
			public class WORKABLE_DIRECTION_RIGHT
			{
				// Token: 0x0400C99B RID: 51611
				public static LocString NAME = "Set Direction: Right";

				// Token: 0x0400C99C RID: 51612
				public static LocString TOOLTIP = "Select to make Duplicants wash when passing by from left to right";
			}

			// Token: 0x02002E68 RID: 11880
			public class MANUAL_PUMP_DELIVERY
			{
				// Token: 0x02003AB0 RID: 15024
				public static class ALLOWED
				{
					// Token: 0x0400E93C RID: 59708
					public static LocString NAME = "Enable Auto-Bottle";

					// Token: 0x0400E93D RID: 59709
					public static LocString TOOLTIP = "If enabled, Duplicants will deliver bottled liquids to this building directly from these sources:\n";

					// Token: 0x0400E93E RID: 59710
					public static LocString ITEM = "\n{0}";
				}

				// Token: 0x02003AB1 RID: 15025
				public static class DENIED
				{
					// Token: 0x0400E93F RID: 59711
					public static LocString NAME = "Disable Auto-Bottle";

					// Token: 0x0400E940 RID: 59712
					public static LocString TOOLTIP = "If disabled, Duplicants will no longer deliver bottled liquids directly from Pitcher Pumps";
				}

				// Token: 0x02003AB2 RID: 15026
				public static class ALLOWED_GAS
				{
					// Token: 0x0400E941 RID: 59713
					public static LocString NAME = "Enable Auto-Bottle";

					// Token: 0x0400E942 RID: 59714
					public static LocString TOOLTIP = "If enabled, Duplicants will deliver gas canisters to this building directly from Canister Fillers";
				}

				// Token: 0x02003AB3 RID: 15027
				public static class DENIED_GAS
				{
					// Token: 0x0400E943 RID: 59715
					public static LocString NAME = "Disable Auto-Bottle";

					// Token: 0x0400E944 RID: 59716
					public static LocString TOOLTIP = "If disabled, Duplicants will no longer deliver gas canisters directly from Canister Fillers";
				}
			}

			// Token: 0x02002E69 RID: 11881
			public class SUIT_MARKER_TRAVERSAL
			{
				// Token: 0x02003AB4 RID: 15028
				public static class ONLY_WHEN_ROOM_AVAILABLE
				{
					// Token: 0x0400E945 RID: 59717
					public static LocString NAME = "Clearance: Vacancy";

					// Token: 0x0400E946 RID: 59718
					public static LocString TOOLTIP = "Suited Duplicants may only pass if there is an available dock to store their suit";
				}

				// Token: 0x02003AB5 RID: 15029
				public static class ALWAYS
				{
					// Token: 0x0400E947 RID: 59719
					public static LocString NAME = "Clearance: Always";

					// Token: 0x0400E948 RID: 59720
					public static LocString TOOLTIP = "Suited Duplicants may pass even if there is no room to store their suits\n\nWhen all available docks are full, Duplicants will unequip their suits and drop them on the floor";
				}
			}

			// Token: 0x02002E6A RID: 11882
			public class ACTIVATEBUILDING
			{
				// Token: 0x0400C99D RID: 51613
				public static LocString ACTIVATE = "Activate";

				// Token: 0x0400C99E RID: 51614
				public static LocString TOOLTIP_ACTIVATE = "Request a Duplicant to activate this building";

				// Token: 0x0400C99F RID: 51615
				public static LocString TOOLTIP_ACTIVATED = "This building has already been activated";

				// Token: 0x0400C9A0 RID: 51616
				public static LocString ACTIVATE_CANCEL = "Cancel Activation";

				// Token: 0x0400C9A1 RID: 51617
				public static LocString ACTIVATED = "Activated";

				// Token: 0x0400C9A2 RID: 51618
				public static LocString TOOLTIP_CANCEL = "Cancel activation of this building";
			}

			// Token: 0x02002E6B RID: 11883
			public class ACCEPT_MUTANT_SEEDS
			{
				// Token: 0x0400C9A3 RID: 51619
				public static LocString ACCEPT = "Allow Mutants";

				// Token: 0x0400C9A4 RID: 51620
				public static LocString REJECT = "Forbid Mutants";

				// Token: 0x0400C9A5 RID: 51621
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Toggle whether or not this building will accept ",
					UI.PRE_KEYWORD,
					"Mutant Seeds",
					UI.PST_KEYWORD,
					" for recipes that could use them"
				});

				// Token: 0x0400C9A6 RID: 51622
				public static LocString FISH_FEEDER_TOOLTIP = string.Concat(new string[]
				{
					"Toggle whether or not this feeder will accept ",
					UI.PRE_KEYWORD,
					"Mutant Seeds",
					UI.PST_KEYWORD,
					" for critters who eat them"
				});
			}

			// Token: 0x02002E6C RID: 11884
			public class CARVE
			{
				// Token: 0x0400C9A7 RID: 51623
				public static LocString NAME = "Carve";

				// Token: 0x0400C9A8 RID: 51624
				public static LocString TOOLTIP = "Carve this rock to enhance its positive effects";
			}

			// Token: 0x02002E6D RID: 11885
			public class CANCELCARVE
			{
				// Token: 0x0400C9A9 RID: 51625
				public static LocString NAME = "Cancel Carve";

				// Token: 0x0400C9AA RID: 51626
				public static LocString TOOLTIP = "Cancel order to carve this rock";
			}
		}

		// Token: 0x020023E7 RID: 9191
		public class BUILDCATEGORIES
		{
			// Token: 0x02002E6E RID: 11886
			public static class BASE
			{
				// Token: 0x0400C9AB RID: 51627
				public static LocString NAME = UI.FormatAsLink("Base", "BUILDCATEGORYBASE");

				// Token: 0x0400C9AC RID: 51628
				public static LocString TOOLTIP = "Maintain the colony's infrastructure with these homebase basics. {Hotkey}";
			}

			// Token: 0x02002E6F RID: 11887
			public static class CONVEYANCE
			{
				// Token: 0x0400C9AD RID: 51629
				public static LocString NAME = UI.FormatAsLink("Shipping", "BUILDCATEGORYCONVEYANCE");

				// Token: 0x0400C9AE RID: 51630
				public static LocString TOOLTIP = "Transport ore and solid materials around my base. {Hotkey}";
			}

			// Token: 0x02002E70 RID: 11888
			public static class OXYGEN
			{
				// Token: 0x0400C9AF RID: 51631
				public static LocString NAME = UI.FormatAsLink("Oxygen", "BUILDCATEGORYOXYGEN");

				// Token: 0x0400C9B0 RID: 51632
				public static LocString TOOLTIP = "Everything I need to keep the colony breathing. {Hotkey}";
			}

			// Token: 0x02002E71 RID: 11889
			public static class POWER
			{
				// Token: 0x0400C9B1 RID: 51633
				public static LocString NAME = UI.FormatAsLink("Power", "BUILDCATEGORYPOWER");

				// Token: 0x0400C9B2 RID: 51634
				public static LocString TOOLTIP = "Need to power the colony? Here's how to do it! {Hotkey}";
			}

			// Token: 0x02002E72 RID: 11890
			public static class FOOD
			{
				// Token: 0x0400C9B3 RID: 51635
				public static LocString NAME = UI.FormatAsLink("Food", "BUILDCATEGORYFOOD");

				// Token: 0x0400C9B4 RID: 51636
				public static LocString TOOLTIP = "Keep my Duplicants' spirits high and their bellies full. {Hotkey}";
			}

			// Token: 0x02002E73 RID: 11891
			public static class UTILITIES
			{
				// Token: 0x0400C9B5 RID: 51637
				public static LocString NAME = UI.FormatAsLink("Utilities", "BUILDCATEGORYUTILITIES");

				// Token: 0x0400C9B6 RID: 51638
				public static LocString TOOLTIP = "Heat up and cool down. {Hotkey}";
			}

			// Token: 0x02002E74 RID: 11892
			public static class PLUMBING
			{
				// Token: 0x0400C9B7 RID: 51639
				public static LocString NAME = UI.FormatAsLink("Plumbing", "BUILDCATEGORYPLUMBING");

				// Token: 0x0400C9B8 RID: 51640
				public static LocString TOOLTIP = "Get the colony's water running and its sewage flowing. {Hotkey}";
			}

			// Token: 0x02002E75 RID: 11893
			public static class HVAC
			{
				// Token: 0x0400C9B9 RID: 51641
				public static LocString NAME = UI.FormatAsLink("Ventilation", "BUILDCATEGORYHVAC");

				// Token: 0x0400C9BA RID: 51642
				public static LocString TOOLTIP = "Control the flow of gas in the base. {Hotkey}";
			}

			// Token: 0x02002E76 RID: 11894
			public static class REFINING
			{
				// Token: 0x0400C9BB RID: 51643
				public static LocString NAME = UI.FormatAsLink("Refinement", "BUILDCATEGORYREFINING");

				// Token: 0x0400C9BC RID: 51644
				public static LocString TOOLTIP = "Use the resources I want, filter the ones I don't. {Hotkey}";
			}

			// Token: 0x02002E77 RID: 11895
			public static class ROCKETRY
			{
				// Token: 0x0400C9BD RID: 51645
				public static LocString NAME = UI.FormatAsLink("Rocketry", "BUILDCATEGORYROCKETRY");

				// Token: 0x0400C9BE RID: 51646
				public static LocString TOOLTIP = "With rockets, the sky's no longer the limit! {Hotkey}";
			}

			// Token: 0x02002E78 RID: 11896
			public static class MEDICAL
			{
				// Token: 0x0400C9BF RID: 51647
				public static LocString NAME = UI.FormatAsLink("Medicine", "BUILDCATEGORYMEDICAL");

				// Token: 0x0400C9C0 RID: 51648
				public static LocString TOOLTIP = "A cure for everything but the common cold. {Hotkey}";
			}

			// Token: 0x02002E79 RID: 11897
			public static class FURNITURE
			{
				// Token: 0x0400C9C1 RID: 51649
				public static LocString NAME = UI.FormatAsLink("Furniture", "BUILDCATEGORYFURNITURE");

				// Token: 0x0400C9C2 RID: 51650
				public static LocString TOOLTIP = "Amenities to keep my Duplicants happy, comfy and efficient. {Hotkey}";
			}

			// Token: 0x02002E7A RID: 11898
			public static class EQUIPMENT
			{
				// Token: 0x0400C9C3 RID: 51651
				public static LocString NAME = UI.FormatAsLink("Stations", "BUILDCATEGORYEQUIPMENT");

				// Token: 0x0400C9C4 RID: 51652
				public static LocString TOOLTIP = "Unlock new technologies through the power of science! {Hotkey}";
			}

			// Token: 0x02002E7B RID: 11899
			public static class MISC
			{
				// Token: 0x0400C9C5 RID: 51653
				public static LocString NAME = UI.FormatAsLink("Decor", "BUILDCATEGORYMISC");

				// Token: 0x0400C9C6 RID: 51654
				public static LocString TOOLTIP = "Spruce up my colony with some lovely interior decorating. {Hotkey}";
			}

			// Token: 0x02002E7C RID: 11900
			public static class AUTOMATION
			{
				// Token: 0x0400C9C7 RID: 51655
				public static LocString NAME = UI.FormatAsLink("Automation", "BUILDCATEGORYAUTOMATION");

				// Token: 0x0400C9C8 RID: 51656
				public static LocString TOOLTIP = "Automate my base with a wide range of sensors. {Hotkey}";
			}

			// Token: 0x02002E7D RID: 11901
			public static class HEP
			{
				// Token: 0x0400C9C9 RID: 51657
				public static LocString NAME = UI.FormatAsLink("Radiation", "BUILDCATEGORYHEP");

				// Token: 0x0400C9CA RID: 51658
				public static LocString TOOLTIP = "Here's where things get rad. {Hotkey}";
			}
		}

		// Token: 0x020023E8 RID: 9192
		public class NEWBUILDCATEGORIES
		{
			// Token: 0x02002E7E RID: 11902
			public static class BASE
			{
				// Token: 0x0400C9CB RID: 51659
				public static LocString NAME = UI.FormatAsLink("Base", "BUILD_CATEGORY_BASE");

				// Token: 0x0400C9CC RID: 51660
				public static LocString TOOLTIP = "Maintain the colony's infrastructure with these homebase basics. {Hotkey}";
			}

			// Token: 0x02002E7F RID: 11903
			public static class INFRASTRUCTURE
			{
				// Token: 0x0400C9CD RID: 51661
				public static LocString NAME = UI.FormatAsLink("Utilities", "BUILD_CATEGORY_INFRASTRUCTURE");

				// Token: 0x0400C9CE RID: 51662
				public static LocString TOOLTIP = "Power, plumbing, and ventilation can all be found here. {Hotkey}";
			}

			// Token: 0x02002E80 RID: 11904
			public static class FOODANDAGRICULTURE
			{
				// Token: 0x0400C9CF RID: 51663
				public static LocString NAME = UI.FormatAsLink("Food", "BUILD_CATEGORY_FOODANDAGRICULTURE");

				// Token: 0x0400C9D0 RID: 51664
				public static LocString TOOLTIP = "Keep my Duplicants' spirits high and their bellies full. {Hotkey}";
			}

			// Token: 0x02002E81 RID: 11905
			public static class LOGISTICS
			{
				// Token: 0x0400C9D1 RID: 51665
				public static LocString NAME = UI.FormatAsLink("Logistics", "BUILD_CATEGORY_LOGISTICS");

				// Token: 0x0400C9D2 RID: 51666
				public static LocString TOOLTIP = "Devices for base automation and material transport. {Hotkey}";
			}

			// Token: 0x02002E82 RID: 11906
			public static class HEALTHANDHAPPINESS
			{
				// Token: 0x0400C9D3 RID: 51667
				public static LocString NAME = UI.FormatAsLink("Accommodation", "BUILD_CATEGORY_HEALTHANDHAPPINESS");

				// Token: 0x0400C9D4 RID: 51668
				public static LocString TOOLTIP = "Everything a Duplicant needs to stay happy, healthy, and fulfilled. {Hotkey}";
			}

			// Token: 0x02002E83 RID: 11907
			public static class INDUSTRIAL
			{
				// Token: 0x0400C9D5 RID: 51669
				public static LocString NAME = UI.FormatAsLink("Industrials", "BUILD_CATEGORY_INDUSTRIAL");

				// Token: 0x0400C9D6 RID: 51670
				public static LocString TOOLTIP = "Machinery for oxygen production, heat management, and material refinement. {Hotkey}";
			}

			// Token: 0x02002E84 RID: 11908
			public static class LADDERS
			{
				// Token: 0x0400C9D7 RID: 51671
				public static LocString NAME = "Ladders";

				// Token: 0x0400C9D8 RID: 51672
				public static LocString BUILDMENUTITLE = "Ladders";

				// Token: 0x0400C9D9 RID: 51673
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002E85 RID: 11909
			public static class TILES
			{
				// Token: 0x0400C9DA RID: 51674
				public static LocString NAME = "Tiles and Drywall";

				// Token: 0x0400C9DB RID: 51675
				public static LocString BUILDMENUTITLE = "Tiles and Drywall";

				// Token: 0x0400C9DC RID: 51676
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002E86 RID: 11910
			public static class PRINTINGPODS
			{
				// Token: 0x0400C9DD RID: 51677
				public static LocString NAME = "Printing Pods";

				// Token: 0x0400C9DE RID: 51678
				public static LocString BUILDMENUTITLE = "Printing Pods";

				// Token: 0x0400C9DF RID: 51679
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002E87 RID: 11911
			public static class DOORS
			{
				// Token: 0x0400C9E0 RID: 51680
				public static LocString NAME = "Doors";

				// Token: 0x0400C9E1 RID: 51681
				public static LocString BUILDMENUTITLE = "Doors";

				// Token: 0x0400C9E2 RID: 51682
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002E88 RID: 11912
			public static class STORAGE
			{
				// Token: 0x0400C9E3 RID: 51683
				public static LocString NAME = "Storage";

				// Token: 0x0400C9E4 RID: 51684
				public static LocString BUILDMENUTITLE = "Storage";

				// Token: 0x0400C9E5 RID: 51685
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002E89 RID: 11913
			public static class TRANSPORT
			{
				// Token: 0x0400C9E6 RID: 51686
				public static LocString NAME = "Transit Tubes";

				// Token: 0x0400C9E7 RID: 51687
				public static LocString BUILDMENUTITLE = "Transit Tubes";

				// Token: 0x0400C9E8 RID: 51688
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002E8A RID: 11914
			public static class OPERATIONS
			{
				// Token: 0x0400C9E9 RID: 51689
				public static LocString NAME = "Operations";

				// Token: 0x0400C9EA RID: 51690
				public static LocString BUILDMENUTITLE = "Operations";

				// Token: 0x0400C9EB RID: 51691
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002E8B RID: 11915
			public static class PRODUCERS
			{
				// Token: 0x0400C9EC RID: 51692
				public static LocString NAME = "Production";

				// Token: 0x0400C9ED RID: 51693
				public static LocString BUILDMENUTITLE = "Production";

				// Token: 0x0400C9EE RID: 51694
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002E8C RID: 11916
			public static class SCRUBBERS
			{
				// Token: 0x0400C9EF RID: 51695
				public static LocString NAME = "Purification";

				// Token: 0x0400C9F0 RID: 51696
				public static LocString BUILDMENUTITLE = "Purification";

				// Token: 0x0400C9F1 RID: 51697
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002E8D RID: 11917
			public static class BATTERIES
			{
				// Token: 0x0400C9F2 RID: 51698
				public static LocString NAME = "Batteries";

				// Token: 0x0400C9F3 RID: 51699
				public static LocString BUILDMENUTITLE = "Batteries";

				// Token: 0x0400C9F4 RID: 51700
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002E8E RID: 11918
			public static class SWITCHES
			{
				// Token: 0x0400C9F5 RID: 51701
				public static LocString NAME = "Switches";

				// Token: 0x0400C9F6 RID: 51702
				public static LocString BUILDMENUTITLE = "Switches";

				// Token: 0x0400C9F7 RID: 51703
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002E8F RID: 11919
			public static class COOKING
			{
				// Token: 0x0400C9F8 RID: 51704
				public static LocString NAME = "Cooking";

				// Token: 0x0400C9F9 RID: 51705
				public static LocString BUILDMENUTITLE = "Cooking";

				// Token: 0x0400C9FA RID: 51706
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002E90 RID: 11920
			public static class FARMING
			{
				// Token: 0x0400C9FB RID: 51707
				public static LocString NAME = "Farming";

				// Token: 0x0400C9FC RID: 51708
				public static LocString BUILDMENUTITLE = "Farming";

				// Token: 0x0400C9FD RID: 51709
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002E91 RID: 11921
			public static class RANCHING
			{
				// Token: 0x0400C9FE RID: 51710
				public static LocString NAME = "Ranching";

				// Token: 0x0400C9FF RID: 51711
				public static LocString BUILDMENUTITLE = "Ranching";

				// Token: 0x0400CA00 RID: 51712
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002E92 RID: 11922
			public static class WASHROOM
			{
				// Token: 0x0400CA01 RID: 51713
				public static LocString NAME = "Washroom";

				// Token: 0x0400CA02 RID: 51714
				public static LocString BUILDMENUTITLE = "Washroom";

				// Token: 0x0400CA03 RID: 51715
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002E93 RID: 11923
			public static class VALVES
			{
				// Token: 0x0400CA04 RID: 51716
				public static LocString NAME = "Valves";

				// Token: 0x0400CA05 RID: 51717
				public static LocString BUILDMENUTITLE = "Valves";

				// Token: 0x0400CA06 RID: 51718
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002E94 RID: 11924
			public static class PUMPS
			{
				// Token: 0x0400CA07 RID: 51719
				public static LocString NAME = "Pumps";

				// Token: 0x0400CA08 RID: 51720
				public static LocString BUILDMENUTITLE = "Pumps";

				// Token: 0x0400CA09 RID: 51721
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002E95 RID: 11925
			public static class SENSORS
			{
				// Token: 0x0400CA0A RID: 51722
				public static LocString NAME = "Sensors";

				// Token: 0x0400CA0B RID: 51723
				public static LocString BUILDMENUTITLE = "Sensors";

				// Token: 0x0400CA0C RID: 51724
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002E96 RID: 11926
			public static class PORTS
			{
				// Token: 0x0400CA0D RID: 51725
				public static LocString NAME = "Ports";

				// Token: 0x0400CA0E RID: 51726
				public static LocString BUILDMENUTITLE = "Ports";

				// Token: 0x0400CA0F RID: 51727
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002E97 RID: 11927
			public static class MATERIALS
			{
				// Token: 0x0400CA10 RID: 51728
				public static LocString NAME = "Materials";

				// Token: 0x0400CA11 RID: 51729
				public static LocString BUILDMENUTITLE = "Materials";

				// Token: 0x0400CA12 RID: 51730
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002E98 RID: 11928
			public static class OIL
			{
				// Token: 0x0400CA13 RID: 51731
				public static LocString NAME = "Oil";

				// Token: 0x0400CA14 RID: 51732
				public static LocString BUILDMENUTITLE = "Oil";

				// Token: 0x0400CA15 RID: 51733
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002E99 RID: 11929
			public static class ADVANCED
			{
				// Token: 0x0400CA16 RID: 51734
				public static LocString NAME = "Advanced";

				// Token: 0x0400CA17 RID: 51735
				public static LocString BUILDMENUTITLE = "Advanced";

				// Token: 0x0400CA18 RID: 51736
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002E9A RID: 11930
			public static class ORGANIC
			{
				// Token: 0x0400CA19 RID: 51737
				public static LocString NAME = "Organic";

				// Token: 0x0400CA1A RID: 51738
				public static LocString BUILDMENUTITLE = "Organic";

				// Token: 0x0400CA1B RID: 51739
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002E9B RID: 11931
			public static class BEDS
			{
				// Token: 0x0400CA1C RID: 51740
				public static LocString NAME = "Beds";

				// Token: 0x0400CA1D RID: 51741
				public static LocString BUILDMENUTITLE = "Beds";

				// Token: 0x0400CA1E RID: 51742
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002E9C RID: 11932
			public static class LIGHTS
			{
				// Token: 0x0400CA1F RID: 51743
				public static LocString NAME = "Lights";

				// Token: 0x0400CA20 RID: 51744
				public static LocString BUILDMENUTITLE = "Lights";

				// Token: 0x0400CA21 RID: 51745
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002E9D RID: 11933
			public static class DINING
			{
				// Token: 0x0400CA22 RID: 51746
				public static LocString NAME = "Dining";

				// Token: 0x0400CA23 RID: 51747
				public static LocString BUILDMENUTITLE = "Dining";

				// Token: 0x0400CA24 RID: 51748
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002E9E RID: 11934
			public static class MANUFACTURING
			{
				// Token: 0x0400CA25 RID: 51749
				public static LocString NAME = "Manufacturing";

				// Token: 0x0400CA26 RID: 51750
				public static LocString BUILDMENUTITLE = "Manufacturing";

				// Token: 0x0400CA27 RID: 51751
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002E9F RID: 11935
			public static class TEMPERATURE
			{
				// Token: 0x0400CA28 RID: 51752
				public static LocString NAME = "Temperature";

				// Token: 0x0400CA29 RID: 51753
				public static LocString BUILDMENUTITLE = "Temperature";

				// Token: 0x0400CA2A RID: 51754
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002EA0 RID: 11936
			public static class RESEARCH
			{
				// Token: 0x0400CA2B RID: 51755
				public static LocString NAME = "Research";

				// Token: 0x0400CA2C RID: 51756
				public static LocString BUILDMENUTITLE = "Research";

				// Token: 0x0400CA2D RID: 51757
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002EA1 RID: 11937
			public static class GENERATORS
			{
				// Token: 0x0400CA2E RID: 51758
				public static LocString NAME = "Generators";

				// Token: 0x0400CA2F RID: 51759
				public static LocString BUILDMENUTITLE = "Generators";

				// Token: 0x0400CA30 RID: 51760
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002EA2 RID: 11938
			public static class WIRES
			{
				// Token: 0x0400CA31 RID: 51761
				public static LocString NAME = "Wires";

				// Token: 0x0400CA32 RID: 51762
				public static LocString BUILDMENUTITLE = "Wires";

				// Token: 0x0400CA33 RID: 51763
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002EA3 RID: 11939
			public static class ELECTROBANKBUILDINGS
			{
				// Token: 0x0400CA34 RID: 51764
				public static LocString NAME = "Converters";

				// Token: 0x0400CA35 RID: 51765
				public static LocString BUILDMENUTITLE = "Converters";

				// Token: 0x0400CA36 RID: 51766
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002EA4 RID: 11940
			public static class LOGICGATES
			{
				// Token: 0x0400CA37 RID: 51767
				public static LocString NAME = "Gates";

				// Token: 0x0400CA38 RID: 51768
				public static LocString BUILDMENUTITLE = "Gates";

				// Token: 0x0400CA39 RID: 51769
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002EA5 RID: 11941
			public static class TRANSMISSIONS
			{
				// Token: 0x0400CA3A RID: 51770
				public static LocString NAME = "Transmissions";

				// Token: 0x0400CA3B RID: 51771
				public static LocString BUILDMENUTITLE = "Transmissions";

				// Token: 0x0400CA3C RID: 51772
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002EA6 RID: 11942
			public static class LOGICMANAGER
			{
				// Token: 0x0400CA3D RID: 51773
				public static LocString NAME = "Monitoring";

				// Token: 0x0400CA3E RID: 51774
				public static LocString BUILDMENUTITLE = "Monitoring";

				// Token: 0x0400CA3F RID: 51775
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002EA7 RID: 11943
			public static class LOGICAUDIO
			{
				// Token: 0x0400CA40 RID: 51776
				public static LocString NAME = "Ambience";

				// Token: 0x0400CA41 RID: 51777
				public static LocString BUILDMENUTITLE = "Ambience";

				// Token: 0x0400CA42 RID: 51778
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002EA8 RID: 11944
			public static class CONVEYANCESTRUCTURES
			{
				// Token: 0x0400CA43 RID: 51779
				public static LocString NAME = "Structural";

				// Token: 0x0400CA44 RID: 51780
				public static LocString BUILDMENUTITLE = "Structural";

				// Token: 0x0400CA45 RID: 51781
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002EA9 RID: 11945
			public static class BUILDMENUPORTS
			{
				// Token: 0x0400CA46 RID: 51782
				public static LocString NAME = "Ports";

				// Token: 0x0400CA47 RID: 51783
				public static LocString BUILDMENUTITLE = "Ports";

				// Token: 0x0400CA48 RID: 51784
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002EAA RID: 11946
			public static class POWERCONTROL
			{
				// Token: 0x0400CA49 RID: 51785
				public static LocString NAME = "Power\nRegulation";

				// Token: 0x0400CA4A RID: 51786
				public static LocString BUILDMENUTITLE = "Power Regulation";

				// Token: 0x0400CA4B RID: 51787
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002EAB RID: 11947
			public static class PLUMBINGSTRUCTURES
			{
				// Token: 0x0400CA4C RID: 51788
				public static LocString NAME = "Plumbing";

				// Token: 0x0400CA4D RID: 51789
				public static LocString BUILDMENUTITLE = "Plumbing";

				// Token: 0x0400CA4E RID: 51790
				public static LocString TOOLTIP = "Get the colony's water running and its sewage flowing. {Hotkey}";
			}

			// Token: 0x02002EAC RID: 11948
			public static class PIPES
			{
				// Token: 0x0400CA4F RID: 51791
				public static LocString NAME = "Pipes";

				// Token: 0x0400CA50 RID: 51792
				public static LocString BUILDMENUTITLE = "Pipes";

				// Token: 0x0400CA51 RID: 51793
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002EAD RID: 11949
			public static class VENTILATIONSTRUCTURES
			{
				// Token: 0x0400CA52 RID: 51794
				public static LocString NAME = "Ventilation";

				// Token: 0x0400CA53 RID: 51795
				public static LocString BUILDMENUTITLE = "Ventilation";

				// Token: 0x0400CA54 RID: 51796
				public static LocString TOOLTIP = "Control the flow of gas in your base. {Hotkey}";
			}

			// Token: 0x02002EAE RID: 11950
			public static class CONVEYANCE
			{
				// Token: 0x0400CA55 RID: 51797
				public static LocString NAME = "Ore\nTransport";

				// Token: 0x0400CA56 RID: 51798
				public static LocString BUILDMENUTITLE = "Ore Transport";

				// Token: 0x0400CA57 RID: 51799
				public static LocString TOOLTIP = "Transport ore and solid materials around my base. {Hotkey}";
			}

			// Token: 0x02002EAF RID: 11951
			public static class HYGIENE
			{
				// Token: 0x0400CA58 RID: 51800
				public static LocString NAME = "Hygiene";

				// Token: 0x0400CA59 RID: 51801
				public static LocString BUILDMENUTITLE = "Hygiene";

				// Token: 0x0400CA5A RID: 51802
				public static LocString TOOLTIP = "Keeps my Duplicants clean.";
			}

			// Token: 0x02002EB0 RID: 11952
			public static class MEDICAL
			{
				// Token: 0x0400CA5B RID: 51803
				public static LocString NAME = "Medical";

				// Token: 0x0400CA5C RID: 51804
				public static LocString BUILDMENUTITLE = "Medical";

				// Token: 0x0400CA5D RID: 51805
				public static LocString TOOLTIP = "A cure for everything but the common cold. {Hotkey}";
			}

			// Token: 0x02002EB1 RID: 11953
			public static class WELLNESS
			{
				// Token: 0x0400CA5E RID: 51806
				public static LocString NAME = "Wellness";

				// Token: 0x0400CA5F RID: 51807
				public static LocString BUILDMENUTITLE = "Wellness";

				// Token: 0x0400CA60 RID: 51808
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002EB2 RID: 11954
			public static class RECREATION
			{
				// Token: 0x0400CA61 RID: 51809
				public static LocString NAME = "Recreation";

				// Token: 0x0400CA62 RID: 51810
				public static LocString BUILDMENUTITLE = "Recreation";

				// Token: 0x0400CA63 RID: 51811
				public static LocString TOOLTIP = "Everything needed to reduce stress and increase fun.";
			}

			// Token: 0x02002EB3 RID: 11955
			public static class FURNITURE
			{
				// Token: 0x0400CA64 RID: 51812
				public static LocString NAME = "Furniture";

				// Token: 0x0400CA65 RID: 51813
				public static LocString BUILDMENUTITLE = "Furniture";

				// Token: 0x0400CA66 RID: 51814
				public static LocString TOOLTIP = "Amenities to keep my Duplicants happy, comfy and efficient. {Hotkey}";
			}

			// Token: 0x02002EB4 RID: 11956
			public static class DECOR
			{
				// Token: 0x0400CA67 RID: 51815
				public static LocString NAME = "Decor";

				// Token: 0x0400CA68 RID: 51816
				public static LocString BUILDMENUTITLE = "Decor";

				// Token: 0x0400CA69 RID: 51817
				public static LocString TOOLTIP = "Spruce up your colony with some lovely interior decorating. {Hotkey}";
			}

			// Token: 0x02002EB5 RID: 11957
			public static class OXYGEN
			{
				// Token: 0x0400CA6A RID: 51818
				public static LocString NAME = "Oxygen";

				// Token: 0x0400CA6B RID: 51819
				public static LocString BUILDMENUTITLE = "Oxygen";

				// Token: 0x0400CA6C RID: 51820
				public static LocString TOOLTIP = "Everything I need to keep my colony breathing. {Hotkey}";
			}

			// Token: 0x02002EB6 RID: 11958
			public static class UTILITIES
			{
				// Token: 0x0400CA6D RID: 51821
				public static LocString NAME = "Temperature";

				// Token: 0x0400CA6E RID: 51822
				public static LocString BUILDMENUTITLE = "Temperature";

				// Token: 0x0400CA6F RID: 51823
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002EB7 RID: 11959
			public static class REFINING
			{
				// Token: 0x0400CA70 RID: 51824
				public static LocString NAME = "Refinement";

				// Token: 0x0400CA71 RID: 51825
				public static LocString BUILDMENUTITLE = "Refinement";

				// Token: 0x0400CA72 RID: 51826
				public static LocString TOOLTIP = "Use the resources you want, filter the ones you don't. {Hotkey}";
			}

			// Token: 0x02002EB8 RID: 11960
			public static class EQUIPMENT
			{
				// Token: 0x0400CA73 RID: 51827
				public static LocString NAME = "Equipment";

				// Token: 0x0400CA74 RID: 51828
				public static LocString BUILDMENUTITLE = "Equipment";

				// Token: 0x0400CA75 RID: 51829
				public static LocString TOOLTIP = "Unlock new technologies through the power of science! {Hotkey}";
			}

			// Token: 0x02002EB9 RID: 11961
			public static class ARCHAEOLOGY
			{
				// Token: 0x0400CA76 RID: 51830
				public static LocString NAME = "Archaeology";

				// Token: 0x0400CA77 RID: 51831
				public static LocString BUILDMENUTITLE = "Archaeology";

				// Token: 0x0400CA78 RID: 51832
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002EBA RID: 11962
			public static class METEORDEFENSE
			{
				// Token: 0x0400CA79 RID: 51833
				public static LocString NAME = "Meteor Defense";

				// Token: 0x0400CA7A RID: 51834
				public static LocString BUILDMENUTITLE = "Meteor Defense";

				// Token: 0x0400CA7B RID: 51835
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002EBB RID: 11963
			public static class INDUSTRIALSTATION
			{
				// Token: 0x0400CA7C RID: 51836
				public static LocString NAME = "Industrial";

				// Token: 0x0400CA7D RID: 51837
				public static LocString BUILDMENUTITLE = "Industrial";

				// Token: 0x0400CA7E RID: 51838
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002EBC RID: 11964
			public static class TELESCOPES
			{
				// Token: 0x0400CA7F RID: 51839
				public static LocString NAME = "Telescopes";

				// Token: 0x0400CA80 RID: 51840
				public static LocString BUILDMENUTITLE = "Telescopes";

				// Token: 0x0400CA81 RID: 51841
				public static LocString TOOLTIP = "Unlock new technologies through the power of science! {Hotkey}";
			}

			// Token: 0x02002EBD RID: 11965
			public static class MISSILES
			{
				// Token: 0x0400CA82 RID: 51842
				public static LocString NAME = "Meteor Defense";

				// Token: 0x0400CA83 RID: 51843
				public static LocString BUILDMENUTITLE = "Meteor Defense";

				// Token: 0x0400CA84 RID: 51844
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002EBE RID: 11966
			public static class FITTINGS
			{
				// Token: 0x0400CA85 RID: 51845
				public static LocString NAME = "Fittings";

				// Token: 0x0400CA86 RID: 51846
				public static LocString BUILDMENUTITLE = "Fittings";

				// Token: 0x0400CA87 RID: 51847
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002EBF RID: 11967
			public static class SANITATION
			{
				// Token: 0x0400CA88 RID: 51848
				public static LocString NAME = "Sanitation";

				// Token: 0x0400CA89 RID: 51849
				public static LocString BUILDMENUTITLE = "Sanitation";

				// Token: 0x0400CA8A RID: 51850
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002EC0 RID: 11968
			public static class AUTOMATED
			{
				// Token: 0x0400CA8B RID: 51851
				public static LocString NAME = "Automated";

				// Token: 0x0400CA8C RID: 51852
				public static LocString BUILDMENUTITLE = "Automated";

				// Token: 0x0400CA8D RID: 51853
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002EC1 RID: 11969
			public static class ROCKETSTRUCTURES
			{
				// Token: 0x0400CA8E RID: 51854
				public static LocString NAME = "Structural";

				// Token: 0x0400CA8F RID: 51855
				public static LocString BUILDMENUTITLE = "Structural";

				// Token: 0x0400CA90 RID: 51856
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002EC2 RID: 11970
			public static class ROCKETNAV
			{
				// Token: 0x0400CA91 RID: 51857
				public static LocString NAME = "Navigation";

				// Token: 0x0400CA92 RID: 51858
				public static LocString BUILDMENUTITLE = "Navigation";

				// Token: 0x0400CA93 RID: 51859
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002EC3 RID: 11971
			public static class CONDUITSENSORS
			{
				// Token: 0x0400CA94 RID: 51860
				public static LocString NAME = "Pipe Sensors";

				// Token: 0x0400CA95 RID: 51861
				public static LocString BUILDMENUTITLE = "Pipe Sensors";

				// Token: 0x0400CA96 RID: 51862
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002EC4 RID: 11972
			public static class ROCKETRY
			{
				// Token: 0x0400CA97 RID: 51863
				public static LocString NAME = "Rocketry";

				// Token: 0x0400CA98 RID: 51864
				public static LocString BUILDMENUTITLE = "Rocketry";

				// Token: 0x0400CA99 RID: 51865
				public static LocString TOOLTIP = "Rocketry {Hotkey}";
			}

			// Token: 0x02002EC5 RID: 11973
			public static class ENGINES
			{
				// Token: 0x0400CA9A RID: 51866
				public static LocString NAME = "Engines";

				// Token: 0x0400CA9B RID: 51867
				public static LocString BUILDMENUTITLE = "Engines";

				// Token: 0x0400CA9C RID: 51868
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002EC6 RID: 11974
			public static class TANKS
			{
				// Token: 0x0400CA9D RID: 51869
				public static LocString NAME = "Tanks";

				// Token: 0x0400CA9E RID: 51870
				public static LocString BUILDMENUTITLE = "Tanks";

				// Token: 0x0400CA9F RID: 51871
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002EC7 RID: 11975
			public static class CARGO
			{
				// Token: 0x0400CAA0 RID: 51872
				public static LocString NAME = "Cargo";

				// Token: 0x0400CAA1 RID: 51873
				public static LocString BUILDMENUTITLE = "Cargo";

				// Token: 0x0400CAA2 RID: 51874
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002EC8 RID: 11976
			public static class MODULE
			{
				// Token: 0x0400CAA3 RID: 51875
				public static LocString NAME = "Modules";

				// Token: 0x0400CAA4 RID: 51876
				public static LocString BUILDMENUTITLE = "Modules";

				// Token: 0x0400CAA5 RID: 51877
				public static LocString TOOLTIP = "";
			}
		}

		// Token: 0x020023E9 RID: 9193
		public class TOOLS
		{
			// Token: 0x0400A1F5 RID: 41461
			public static LocString TOOL_AREA_FMT = "{0} x {1}\n{2} tiles";

			// Token: 0x0400A1F6 RID: 41462
			public static LocString TOOL_LENGTH_FMT = "{0}";

			// Token: 0x0400A1F7 RID: 41463
			public static LocString FILTER_HOVERCARD_HEADER = "   <style=\"hovercard_element\">({0})</style>";

			// Token: 0x0400A1F8 RID: 41464
			public static LocString CAPITALS = "<uppercase>{0}</uppercase>";

			// Token: 0x02002EC9 RID: 11977
			public class SANDBOX
			{
				// Token: 0x02003AB6 RID: 15030
				public class SANDBOX_TOGGLE
				{
					// Token: 0x0400E949 RID: 59721
					public static LocString NAME = "SANDBOX";
				}

				// Token: 0x02003AB7 RID: 15031
				public class BRUSH
				{
					// Token: 0x0400E94A RID: 59722
					public static LocString NAME = "Brush";

					// Token: 0x0400E94B RID: 59723
					public static LocString HOVERACTION = "PAINT SIM";
				}

				// Token: 0x02003AB8 RID: 15032
				public class SPRINKLE
				{
					// Token: 0x0400E94C RID: 59724
					public static LocString NAME = "Sprinkle";

					// Token: 0x0400E94D RID: 59725
					public static LocString HOVERACTION = "SPRINKLE SIM";
				}

				// Token: 0x02003AB9 RID: 15033
				public class FLOOD
				{
					// Token: 0x0400E94E RID: 59726
					public static LocString NAME = "Fill";

					// Token: 0x0400E94F RID: 59727
					public static LocString HOVERACTION = "PAINT SECTION";
				}

				// Token: 0x02003ABA RID: 15034
				public class MARQUEE
				{
					// Token: 0x0400E950 RID: 59728
					public static LocString NAME = "Marquee";
				}

				// Token: 0x02003ABB RID: 15035
				public class SAMPLE
				{
					// Token: 0x0400E951 RID: 59729
					public static LocString NAME = "Sample";

					// Token: 0x0400E952 RID: 59730
					public static LocString HOVERACTION = "COPY SELECTION";
				}

				// Token: 0x02003ABC RID: 15036
				public class HEATGUN
				{
					// Token: 0x0400E953 RID: 59731
					public static LocString NAME = "Heat Gun";

					// Token: 0x0400E954 RID: 59732
					public static LocString HOVERACTION = "PAINT HEAT";
				}

				// Token: 0x02003ABD RID: 15037
				public class RADSTOOL
				{
					// Token: 0x0400E955 RID: 59733
					public static LocString NAME = "Radiation Tool";

					// Token: 0x0400E956 RID: 59734
					public static LocString HOVERACTION = "PAINT RADS";
				}

				// Token: 0x02003ABE RID: 15038
				public class STRESSTOOL
				{
					// Token: 0x0400E957 RID: 59735
					public static LocString NAME = "Happy Tool";

					// Token: 0x0400E958 RID: 59736
					public static LocString HOVERACTION = "PAINT CALM";
				}

				// Token: 0x02003ABF RID: 15039
				public class SPAWNER
				{
					// Token: 0x0400E959 RID: 59737
					public static LocString NAME = "Spawner";

					// Token: 0x0400E95A RID: 59738
					public static LocString HOVERACTION = "SPAWN";
				}

				// Token: 0x02003AC0 RID: 15040
				public class CLEAR_FLOOR
				{
					// Token: 0x0400E95B RID: 59739
					public static LocString NAME = "Clear Floor";

					// Token: 0x0400E95C RID: 59740
					public static LocString HOVERACTION = "DELETE DEBRIS";
				}

				// Token: 0x02003AC1 RID: 15041
				public class DESTROY
				{
					// Token: 0x0400E95D RID: 59741
					public static LocString NAME = "Destroy";

					// Token: 0x0400E95E RID: 59742
					public static LocString HOVERACTION = "DELETE";
				}

				// Token: 0x02003AC2 RID: 15042
				public class SPAWN_ENTITY
				{
					// Token: 0x0400E95F RID: 59743
					public static LocString NAME = "Spawn";
				}

				// Token: 0x02003AC3 RID: 15043
				public class FOW
				{
					// Token: 0x0400E960 RID: 59744
					public static LocString NAME = "Reveal";

					// Token: 0x0400E961 RID: 59745
					public static LocString HOVERACTION = "DE-FOG";
				}

				// Token: 0x02003AC4 RID: 15044
				public class CRITTER
				{
					// Token: 0x0400E962 RID: 59746
					public static LocString NAME = "Critter Removal";

					// Token: 0x0400E963 RID: 59747
					public static LocString HOVERACTION = "DELETE CRITTERS";
				}

				// Token: 0x02003AC5 RID: 15045
				public class SPAWN_STORY_TRAIT
				{
					// Token: 0x0400E964 RID: 59748
					public static LocString NAME = "Story Trait";

					// Token: 0x0400E965 RID: 59749
					public static LocString HOVERACTION = "PLACE";

					// Token: 0x0400E966 RID: 59750
					public static LocString ERROR_ALREADY_EXISTS = "{StoryName} already exists in this save";

					// Token: 0x0400E967 RID: 59751
					public static LocString ERROR_INVALID_LOCATION = "Invalid location";

					// Token: 0x0400E968 RID: 59752
					public static LocString ERROR_DUPE_HAZARD = "One or more Duplicants are in the way";

					// Token: 0x0400E969 RID: 59753
					public static LocString ERROR_ROBOT_HAZARD = "One or more robots are in the way";

					// Token: 0x0400E96A RID: 59754
					public static LocString ERROR_CREATURE_HAZARD = "One or more critters are in the way";

					// Token: 0x0400E96B RID: 59755
					public static LocString ERROR_BUILDING_HAZARD = "One or more buildings are in the way";
				}
			}

			// Token: 0x02002ECA RID: 11978
			public class GENERIC
			{
				// Token: 0x0400CAA6 RID: 51878
				public static LocString BACK = "Back";

				// Token: 0x0400CAA7 RID: 51879
				public static LocString UNKNOWN = "UNKNOWN";

				// Token: 0x0400CAA8 RID: 51880
				public static LocString BUILDING_HOVER_NAME_FMT = "{Name}    <style=\"hovercard_element\">({Element})</style>";

				// Token: 0x0400CAA9 RID: 51881
				public static LocString LOGIC_INPUT_HOVER_FMT = "{Port}    <style=\"hovercard_element\">({Name})</style>";

				// Token: 0x0400CAAA RID: 51882
				public static LocString LOGIC_OUTPUT_HOVER_FMT = "{Port}    <style=\"hovercard_element\">({Name})</style>";

				// Token: 0x0400CAAB RID: 51883
				public static LocString LOGIC_MULTI_INPUT_HOVER_FMT = "{Port}    <style=\"hovercard_element\">({Name})</style>";

				// Token: 0x0400CAAC RID: 51884
				public static LocString LOGIC_MULTI_OUTPUT_HOVER_FMT = "{Port}    <style=\"hovercard_element\">({Name})</style>";
			}

			// Token: 0x02002ECB RID: 11979
			public class ATTACK
			{
				// Token: 0x0400CAAD RID: 51885
				public static LocString NAME = "Attack";

				// Token: 0x0400CAAE RID: 51886
				public static LocString TOOLNAME = "Attack tool";

				// Token: 0x0400CAAF RID: 51887
				public static LocString TOOLACTION = "DRAG";
			}

			// Token: 0x02002ECC RID: 11980
			public class CAPTURE
			{
				// Token: 0x0400CAB0 RID: 51888
				public static LocString NAME = "Wrangle";

				// Token: 0x0400CAB1 RID: 51889
				public static LocString TOOLNAME = "Wrangle tool";

				// Token: 0x0400CAB2 RID: 51890
				public static LocString TOOLACTION = "DRAG";

				// Token: 0x0400CAB3 RID: 51891
				public static LocString NOT_CAPTURABLE = "Cannot Wrangle";
			}

			// Token: 0x02002ECD RID: 11981
			public class BUILD
			{
				// Token: 0x0400CAB4 RID: 51892
				public static LocString NAME = "Build {0}";

				// Token: 0x0400CAB5 RID: 51893
				public static LocString TOOLNAME = "Build tool";

				// Token: 0x0400CAB6 RID: 51894
				public static LocString TOOLACTION = UI.CLICK(UI.ClickType.CLICK) + " TO BUILD";

				// Token: 0x0400CAB7 RID: 51895
				public static LocString TOOLACTION_DRAG = "DRAG";
			}

			// Token: 0x02002ECE RID: 11982
			public class PLACE
			{
				// Token: 0x0400CAB8 RID: 51896
				public static LocString NAME = "Place {0}";

				// Token: 0x0400CAB9 RID: 51897
				public static LocString TOOLNAME = "Place tool";

				// Token: 0x0400CABA RID: 51898
				public static LocString TOOLACTION = UI.CLICK(UI.ClickType.CLICK) + " TO PLACE";

				// Token: 0x02003AC6 RID: 15046
				public class REASONS
				{
					// Token: 0x0400E96C RID: 59756
					public static LocString CAN_OCCUPY_AREA = "Location blocked";

					// Token: 0x0400E96D RID: 59757
					public static LocString ON_FOUNDATION = "Must place on the ground";

					// Token: 0x0400E96E RID: 59758
					public static LocString VISIBLE_TO_SPACE = "Must have a clear path to space";

					// Token: 0x0400E96F RID: 59759
					public static LocString RESTRICT_TO_WORLD = "Incorrect " + UI.CLUSTERMAP.PLANETOID;
				}
			}

			// Token: 0x02002ECF RID: 11983
			public class MOVETOLOCATION
			{
				// Token: 0x0400CABB RID: 51899
				public static LocString NAME = "Relocate";

				// Token: 0x0400CABC RID: 51900
				public static LocString TOOLNAME = "Relocate Tool";

				// Token: 0x0400CABD RID: 51901
				public static LocString TOOLACTION = UI.CLICK(UI.ClickType.CLICK) ?? "";

				// Token: 0x0400CABE RID: 51902
				public static LocString UNREACHABLE = "UNREACHABLE";
			}

			// Token: 0x02002ED0 RID: 11984
			public class COPYSETTINGS
			{
				// Token: 0x0400CABF RID: 51903
				public static LocString NAME = "Paste Settings";

				// Token: 0x0400CAC0 RID: 51904
				public static LocString TOOLNAME = "Paste Settings Tool";

				// Token: 0x0400CAC1 RID: 51905
				public static LocString TOOLACTION = "DRAG";
			}

			// Token: 0x02002ED1 RID: 11985
			public class DIG
			{
				// Token: 0x0400CAC2 RID: 51906
				public static LocString NAME = "Dig";

				// Token: 0x0400CAC3 RID: 51907
				public static LocString TOOLNAME = "Dig tool";

				// Token: 0x0400CAC4 RID: 51908
				public static LocString TOOLACTION = "DRAG";
			}

			// Token: 0x02002ED2 RID: 11986
			public class DISINFECT
			{
				// Token: 0x0400CAC5 RID: 51909
				public static LocString NAME = "Disinfect";

				// Token: 0x0400CAC6 RID: 51910
				public static LocString TOOLNAME = "Disinfect tool";

				// Token: 0x0400CAC7 RID: 51911
				public static LocString TOOLACTION = "DRAG";
			}

			// Token: 0x02002ED3 RID: 11987
			public class DISCONNECT
			{
				// Token: 0x0400CAC8 RID: 51912
				public static LocString NAME = "Disconnect";

				// Token: 0x0400CAC9 RID: 51913
				public static LocString TOOLTIP = "Sever conduits and connectors {Hotkey}";

				// Token: 0x0400CACA RID: 51914
				public static LocString TOOLNAME = "Disconnect tool";

				// Token: 0x0400CACB RID: 51915
				public static LocString TOOLACTION = "DRAG";
			}

			// Token: 0x02002ED4 RID: 11988
			public class CANCEL
			{
				// Token: 0x0400CACC RID: 51916
				public static LocString NAME = "Cancel";

				// Token: 0x0400CACD RID: 51917
				public static LocString TOOLNAME = "Cancel tool";

				// Token: 0x0400CACE RID: 51918
				public static LocString TOOLACTION = "DRAG";
			}

			// Token: 0x02002ED5 RID: 11989
			public class DECONSTRUCT
			{
				// Token: 0x0400CACF RID: 51919
				public static LocString NAME = "Deconstruct";

				// Token: 0x0400CAD0 RID: 51920
				public static LocString TOOLNAME = "Deconstruct tool";

				// Token: 0x0400CAD1 RID: 51921
				public static LocString TOOLACTION = "DRAG";
			}

			// Token: 0x02002ED6 RID: 11990
			public class CLEANUPCATEGORY
			{
				// Token: 0x0400CAD2 RID: 51922
				public static LocString NAME = "Clean";

				// Token: 0x0400CAD3 RID: 51923
				public static LocString TOOLNAME = "Clean Up tools";
			}

			// Token: 0x02002ED7 RID: 11991
			public class PRIORITIESCATEGORY
			{
				// Token: 0x0400CAD4 RID: 51924
				public static LocString NAME = "Priority";
			}

			// Token: 0x02002ED8 RID: 11992
			public class MARKFORSTORAGE
			{
				// Token: 0x0400CAD5 RID: 51925
				public static LocString NAME = "Sweep";

				// Token: 0x0400CAD6 RID: 51926
				public static LocString TOOLNAME = "Sweep tool";

				// Token: 0x0400CAD7 RID: 51927
				public static LocString TOOLACTION = "DRAG";
			}

			// Token: 0x02002ED9 RID: 11993
			public class MOP
			{
				// Token: 0x0400CAD8 RID: 51928
				public static LocString NAME = "Mop";

				// Token: 0x0400CAD9 RID: 51929
				public static LocString TOOLNAME = "Mop tool";

				// Token: 0x0400CADA RID: 51930
				public static LocString TOOLACTION = "DRAG";

				// Token: 0x0400CADB RID: 51931
				public static LocString TOO_MUCH_LIQUID = "Too Much Liquid";

				// Token: 0x0400CADC RID: 51932
				public static LocString NOT_ON_FLOOR = "Not On Floor";
			}

			// Token: 0x02002EDA RID: 11994
			public class HARVEST
			{
				// Token: 0x0400CADD RID: 51933
				public static LocString NAME = "Harvest";

				// Token: 0x0400CADE RID: 51934
				public static LocString TOOLNAME = "Harvest tool";

				// Token: 0x0400CADF RID: 51935
				public static LocString TOOLACTION = "DRAG";
			}

			// Token: 0x02002EDB RID: 11995
			public class PRIORITIZE
			{
				// Token: 0x0400CAE0 RID: 51936
				public static LocString NAME = "Priority";

				// Token: 0x0400CAE1 RID: 51937
				public static LocString TOOLNAME = "Priority tool";

				// Token: 0x0400CAE2 RID: 51938
				public static LocString TOOLACTION = "DRAG";

				// Token: 0x0400CAE3 RID: 51939
				public static LocString SPECIFIC_PRIORITY = "Set Priority: {0}";
			}

			// Token: 0x02002EDC RID: 11996
			public class EMPTY_PIPE
			{
				// Token: 0x0400CAE4 RID: 51940
				public static LocString NAME = "Empty Pipe";

				// Token: 0x0400CAE5 RID: 51941
				public static LocString TOOLTIP = "Extract pipe contents {Hotkey}";

				// Token: 0x0400CAE6 RID: 51942
				public static LocString TOOLNAME = "Empty Pipe tool";

				// Token: 0x0400CAE7 RID: 51943
				public static LocString TOOLACTION = "DRAG";
			}

			// Token: 0x02002EDD RID: 11997
			public class FILTERSCREEN
			{
				// Token: 0x0400CAE8 RID: 51944
				public static LocString OPTIONS = "Tool Filter";
			}

			// Token: 0x02002EDE RID: 11998
			public class FILTERLAYERS
			{
				// Token: 0x02003AC7 RID: 15047
				public class BUILDINGS
				{
					// Token: 0x0400E970 RID: 59760
					public static LocString NAME = "Buildings";

					// Token: 0x0400E971 RID: 59761
					public static LocString TOOLTIP = "All buildings";
				}

				// Token: 0x02003AC8 RID: 15048
				public class TILES
				{
					// Token: 0x0400E972 RID: 59762
					public static LocString NAME = "Tiles";

					// Token: 0x0400E973 RID: 59763
					public static LocString TOOLTIP = "Tiles only";
				}

				// Token: 0x02003AC9 RID: 15049
				public class WIRES
				{
					// Token: 0x0400E974 RID: 59764
					public static LocString NAME = "Power Wires";

					// Token: 0x0400E975 RID: 59765
					public static LocString TOOLTIP = "Power wires only";
				}

				// Token: 0x02003ACA RID: 15050
				public class SOLIDCONDUITS
				{
					// Token: 0x0400E976 RID: 59766
					public static LocString NAME = "Conveyor Rails";

					// Token: 0x0400E977 RID: 59767
					public static LocString TOOLTIP = "Conveyor rails only";
				}

				// Token: 0x02003ACB RID: 15051
				public class DIGPLACER
				{
					// Token: 0x0400E978 RID: 59768
					public static LocString NAME = "Dig Orders";

					// Token: 0x0400E979 RID: 59769
					public static LocString TOOLTIP = "Dig orders only";
				}

				// Token: 0x02003ACC RID: 15052
				public class CLEANANDCLEAR
				{
					// Token: 0x0400E97A RID: 59770
					public static LocString NAME = "Sweep & Mop Orders";

					// Token: 0x0400E97B RID: 59771
					public static LocString TOOLTIP = "Sweep and mop orders only";
				}

				// Token: 0x02003ACD RID: 15053
				public class HARVEST_WHEN_READY
				{
					// Token: 0x0400E97C RID: 59772
					public static LocString NAME = "Enable Harvest";

					// Token: 0x0400E97D RID: 59773
					public static LocString TOOLTIP = "Enable harvest on selected plants";
				}

				// Token: 0x02003ACE RID: 15054
				public class DO_NOT_HARVEST
				{
					// Token: 0x0400E97E RID: 59774
					public static LocString NAME = "Disable Harvest";

					// Token: 0x0400E97F RID: 59775
					public static LocString TOOLTIP = "Disable harvest on selected plants";
				}

				// Token: 0x02003ACF RID: 15055
				public class ATTACK
				{
					// Token: 0x0400E980 RID: 59776
					public static LocString NAME = "Attack";

					// Token: 0x0400E981 RID: 59777
					public static LocString TOOLTIP = "";
				}

				// Token: 0x02003AD0 RID: 15056
				public class LOGIC
				{
					// Token: 0x0400E982 RID: 59778
					public static LocString NAME = "Automation";

					// Token: 0x0400E983 RID: 59779
					public static LocString TOOLTIP = "Automation buildings only";
				}

				// Token: 0x02003AD1 RID: 15057
				public class BACKWALL
				{
					// Token: 0x0400E984 RID: 59780
					public static LocString NAME = "Background Buildings";

					// Token: 0x0400E985 RID: 59781
					public static LocString TOOLTIP = "Background buildings only";
				}

				// Token: 0x02003AD2 RID: 15058
				public class LIQUIDPIPES
				{
					// Token: 0x0400E986 RID: 59782
					public static LocString NAME = "Liquid Pipes";

					// Token: 0x0400E987 RID: 59783
					public static LocString TOOLTIP = "Liquid pipes only";
				}

				// Token: 0x02003AD3 RID: 15059
				public class GASPIPES
				{
					// Token: 0x0400E988 RID: 59784
					public static LocString NAME = "Gas Pipes";

					// Token: 0x0400E989 RID: 59785
					public static LocString TOOLTIP = "Gas pipes only";
				}

				// Token: 0x02003AD4 RID: 15060
				public class ALL
				{
					// Token: 0x0400E98A RID: 59786
					public static LocString NAME = "All";

					// Token: 0x0400E98B RID: 59787
					public static LocString TOOLTIP = "Target all";
				}

				// Token: 0x02003AD5 RID: 15061
				public class ALL_OVERLAY
				{
					// Token: 0x0400E98C RID: 59788
					public static LocString NAME = "All";

					// Token: 0x0400E98D RID: 59789
					public static LocString TOOLTIP = "Show all";
				}

				// Token: 0x02003AD6 RID: 15062
				public class METAL
				{
					// Token: 0x0400E98E RID: 59790
					public static LocString NAME = "Metal";

					// Token: 0x0400E98F RID: 59791
					public static LocString TOOLTIP = "Show only metals";
				}

				// Token: 0x02003AD7 RID: 15063
				public class BUILDABLE
				{
					// Token: 0x0400E990 RID: 59792
					public static LocString NAME = "Mineral";

					// Token: 0x0400E991 RID: 59793
					public static LocString TOOLTIP = "Show only minerals";
				}

				// Token: 0x02003AD8 RID: 15064
				public class FILTER
				{
					// Token: 0x0400E992 RID: 59794
					public static LocString NAME = "Filtration Medium";

					// Token: 0x0400E993 RID: 59795
					public static LocString TOOLTIP = "Show only filtration mediums";
				}

				// Token: 0x02003AD9 RID: 15065
				public class CONSUMABLEORE
				{
					// Token: 0x0400E994 RID: 59796
					public static LocString NAME = "Consumable Ore";

					// Token: 0x0400E995 RID: 59797
					public static LocString TOOLTIP = "Show only consumable ore";
				}

				// Token: 0x02003ADA RID: 15066
				public class ORGANICS
				{
					// Token: 0x0400E996 RID: 59798
					public static LocString NAME = "Organic";

					// Token: 0x0400E997 RID: 59799
					public static LocString TOOLTIP = "Show only organic materials";
				}

				// Token: 0x02003ADB RID: 15067
				public class FARMABLE
				{
					// Token: 0x0400E998 RID: 59800
					public static LocString NAME = "Cultivable Soil";

					// Token: 0x0400E999 RID: 59801
					public static LocString TOOLTIP = "Show only cultivable soil";
				}

				// Token: 0x02003ADC RID: 15068
				public class LIQUIFIABLE
				{
					// Token: 0x0400E99A RID: 59802
					public static LocString NAME = "Liquefiable";

					// Token: 0x0400E99B RID: 59803
					public static LocString TOOLTIP = "Show only liquefiable elements";
				}

				// Token: 0x02003ADD RID: 15069
				public class GAS
				{
					// Token: 0x0400E99C RID: 59804
					public static LocString NAME = "Gas";

					// Token: 0x0400E99D RID: 59805
					public static LocString TOOLTIP = "Show only gases";
				}

				// Token: 0x02003ADE RID: 15070
				public class LIQUID
				{
					// Token: 0x0400E99E RID: 59806
					public static LocString NAME = "Liquid";

					// Token: 0x0400E99F RID: 59807
					public static LocString TOOLTIP = "Show only liquids";
				}

				// Token: 0x02003ADF RID: 15071
				public class MISC
				{
					// Token: 0x0400E9A0 RID: 59808
					public static LocString NAME = "Miscellaneous";

					// Token: 0x0400E9A1 RID: 59809
					public static LocString TOOLTIP = "Show only miscellaneous elements";
				}

				// Token: 0x02003AE0 RID: 15072
				public class ABSOLUTETEMPERATURE
				{
					// Token: 0x0400E9A2 RID: 59810
					public static LocString NAME = "Absolute Temperature";

					// Token: 0x0400E9A3 RID: 59811
					public static LocString TOOLTIP = "<b>Absolute Temperature</b>\nView the default temperature ranges and categories relative to absolute zero";
				}

				// Token: 0x02003AE1 RID: 15073
				public class RELATIVETEMPERATURE
				{
					// Token: 0x0400E9A4 RID: 59812
					public static LocString NAME = "Relative Temperature";

					// Token: 0x0400E9A5 RID: 59813
					public static LocString TOOLTIP = "<b>Relative Temperature</b>\nCustomize visual map to identify temperatures relative to a selected midpoint\n\nDrag the slider to adjust the relative temperature range";
				}

				// Token: 0x02003AE2 RID: 15074
				public class HEATFLOW
				{
					// Token: 0x0400E9A6 RID: 59814
					public static LocString NAME = "Thermal Tolerance";

					// Token: 0x0400E9A7 RID: 59815
					public static LocString TOOLTIP = "<b>Thermal Tolerance</b>\nView the impact of ambient temperatures on living beings";
				}

				// Token: 0x02003AE3 RID: 15075
				public class STATECHANGE
				{
					// Token: 0x0400E9A8 RID: 59816
					public static LocString NAME = "State Change";

					// Token: 0x0400E9A9 RID: 59817
					public static LocString TOOLTIP = "<b>State Change</b>\nView the impact of ambient temperatures on element states";
				}

				// Token: 0x02003AE4 RID: 15076
				public class BREATHABLE
				{
					// Token: 0x0400E9AA RID: 59818
					public static LocString NAME = "Breathable Gas";

					// Token: 0x0400E9AB RID: 59819
					public static LocString TOOLTIP = "Show only breathable gases";
				}

				// Token: 0x02003AE5 RID: 15077
				public class UNBREATHABLE
				{
					// Token: 0x0400E9AC RID: 59820
					public static LocString NAME = "Unbreathable Gas";

					// Token: 0x0400E9AD RID: 59821
					public static LocString TOOLTIP = "Show only unbreathable gases";
				}

				// Token: 0x02003AE6 RID: 15078
				public class AGRICULTURE
				{
					// Token: 0x0400E9AE RID: 59822
					public static LocString NAME = "Agriculture";

					// Token: 0x0400E9AF RID: 59823
					public static LocString TOOLTIP = "";
				}

				// Token: 0x02003AE7 RID: 15079
				public class ADAPTIVETEMPERATURE
				{
					// Token: 0x0400E9B0 RID: 59824
					public static LocString NAME = "Adapt. Temperature";

					// Token: 0x0400E9B1 RID: 59825
					public static LocString TOOLTIP = "";
				}

				// Token: 0x02003AE8 RID: 15080
				public class CONSTRUCTION
				{
					// Token: 0x0400E9B2 RID: 59826
					public static LocString NAME = "Construction";

					// Token: 0x0400E9B3 RID: 59827
					public static LocString TOOLTIP = string.Concat(new string[]
					{
						"Target ",
						UI.PRE_KEYWORD,
						"Construction",
						UI.PST_KEYWORD,
						" errands only"
					});
				}

				// Token: 0x02003AE9 RID: 15081
				public class DIG
				{
					// Token: 0x0400E9B4 RID: 59828
					public static LocString NAME = "Digging";

					// Token: 0x0400E9B5 RID: 59829
					public static LocString TOOLTIP = string.Concat(new string[]
					{
						"Target ",
						UI.PRE_KEYWORD,
						"Digging",
						UI.PST_KEYWORD,
						" errands only"
					});
				}

				// Token: 0x02003AEA RID: 15082
				public class CLEAN
				{
					// Token: 0x0400E9B6 RID: 59830
					public static LocString NAME = "Cleaning";

					// Token: 0x0400E9B7 RID: 59831
					public static LocString TOOLTIP = "Target cleaning errands only";
				}

				// Token: 0x02003AEB RID: 15083
				public class OPERATE
				{
					// Token: 0x0400E9B8 RID: 59832
					public static LocString NAME = "Duties";

					// Token: 0x0400E9B9 RID: 59833
					public static LocString TOOLTIP = "Target general duties only";
				}
			}
		}

		// Token: 0x020023EA RID: 9194
		public class DETAILTABS
		{
			// Token: 0x02002EDF RID: 11999
			public class STATS
			{
				// Token: 0x0400CAE9 RID: 51945
				public static LocString NAME = "Skills";

				// Token: 0x0400CAEA RID: 51946
				public static LocString TOOLTIP = "<b>Skills</b>\nView this Duplicant's resume and attributes";

				// Token: 0x0400CAEB RID: 51947
				public static LocString GROUPNAME_ATTRIBUTES = "ATTRIBUTES";

				// Token: 0x0400CAEC RID: 51948
				public static LocString GROUPNAME_STRESS = "TODAY'S STRESS";

				// Token: 0x0400CAED RID: 51949
				public static LocString GROUPNAME_EXPECTATIONS = "EXPECTATIONS";

				// Token: 0x0400CAEE RID: 51950
				public static LocString GROUPNAME_TRAITS = "TRAITS";
			}

			// Token: 0x02002EE0 RID: 12000
			public class SIMPLEINFO
			{
				// Token: 0x0400CAEF RID: 51951
				public static LocString NAME = "Status";

				// Token: 0x0400CAF0 RID: 51952
				public static LocString TOOLTIP = "<b>Status</b>\nView current status";

				// Token: 0x0400CAF1 RID: 51953
				public static LocString GROUPNAME_STATUS = "STATUS";

				// Token: 0x0400CAF2 RID: 51954
				public static LocString GROUPNAME_DESCRIPTION = "INFORMATION";

				// Token: 0x0400CAF3 RID: 51955
				public static LocString GROUPNAME_CONDITION = "CONDITION";

				// Token: 0x0400CAF4 RID: 51956
				public static LocString GROUPNAME_REQUIREMENTS = "REQUIREMENTS";

				// Token: 0x0400CAF5 RID: 51957
				public static LocString GROUPNAME_EFFECTS = "EFFECTS";

				// Token: 0x0400CAF6 RID: 51958
				public static LocString GROUPNAME_RESEARCH = "RESEARCH";

				// Token: 0x0400CAF7 RID: 51959
				public static LocString GROUPNAME_LORE = "RECOVERED FILES";

				// Token: 0x0400CAF8 RID: 51960
				public static LocString GROUPNAME_FERTILITY = "EGG CHANCES";

				// Token: 0x0400CAF9 RID: 51961
				public static LocString GROUPNAME_ROCKET = "ROCKETRY";

				// Token: 0x0400CAFA RID: 51962
				public static LocString GROUPNAME_CARGOBAY = "CARGO BAYS";

				// Token: 0x0400CAFB RID: 51963
				public static LocString GROUPNAME_ELEMENTS = "RESOURCES";

				// Token: 0x0400CAFC RID: 51964
				public static LocString GROUPNAME_LIFE = "LIFEFORMS";

				// Token: 0x0400CAFD RID: 51965
				public static LocString GROUPNAME_BIOMES = "BIOMES";

				// Token: 0x0400CAFE RID: 51966
				public static LocString GROUPNAME_GEYSERS = "GEYSERS";

				// Token: 0x0400CAFF RID: 51967
				public static LocString GROUPNAME_METEORSHOWERS = "METEOR SHOWERS";

				// Token: 0x0400CB00 RID: 51968
				public static LocString GROUPNAME_WORLDTRAITS = "WORLD TRAITS";

				// Token: 0x0400CB01 RID: 51969
				public static LocString GROUPNAME_CLUSTER_POI = "POINT OF INTEREST";

				// Token: 0x0400CB02 RID: 51970
				public static LocString GROUPNAME_MOVABLE = "MOVING";

				// Token: 0x0400CB03 RID: 51971
				public static LocString NO_METEORSHOWERS = "No meteor showers forecasted";

				// Token: 0x0400CB04 RID: 51972
				public static LocString NO_GEYSERS = "No geysers detected";

				// Token: 0x0400CB05 RID: 51973
				public static LocString UNKNOWN_GEYSERS = "Unknown Geysers ({num})";
			}

			// Token: 0x02002EE1 RID: 12001
			public class DETAILS
			{
				// Token: 0x0400CB06 RID: 51974
				public static LocString NAME = "Properties";

				// Token: 0x0400CB07 RID: 51975
				public static LocString MINION_NAME = "About";

				// Token: 0x0400CB08 RID: 51976
				public static LocString TOOLTIP = "<b>Properties</b>\nView elements, temperature, germs and more";

				// Token: 0x0400CB09 RID: 51977
				public static LocString MINION_TOOLTIP = "More information";

				// Token: 0x0400CB0A RID: 51978
				public static LocString GROUPNAME_DETAILS = "DETAILS";

				// Token: 0x0400CB0B RID: 51979
				public static LocString GROUPNAME_CONTENTS = "CONTENTS";

				// Token: 0x0400CB0C RID: 51980
				public static LocString GROUPNAME_MINION_CONTENTS = "CARRIED ITEMS";

				// Token: 0x0400CB0D RID: 51981
				public static LocString STORAGE_EMPTY = "None";

				// Token: 0x0400CB0E RID: 51982
				public static LocString CONTENTS_MASS = "{0}: {1}";

				// Token: 0x0400CB0F RID: 51983
				public static LocString CONTENTS_TEMPERATURE = "{0} at {1}";

				// Token: 0x0400CB10 RID: 51984
				public static LocString CONTENTS_ROTTABLE = "\n • {0}";

				// Token: 0x0400CB11 RID: 51985
				public static LocString CONTENTS_DISEASED = "\n • {0}";

				// Token: 0x0400CB12 RID: 51986
				public static LocString NET_STRESS = "<b>Today's Net Stress: {0}%</b>";

				// Token: 0x02003AEC RID: 15084
				public class RADIATIONABSORPTIONFACTOR
				{
					// Token: 0x0400E9BA RID: 59834
					public static LocString NAME = "Radiation Blocking: {0}";

					// Token: 0x0400E9BB RID: 59835
					public static LocString TOOLTIP = "This object will block approximately {0} of radiation.";
				}
			}

			// Token: 0x02002EE2 RID: 12002
			public class PERSONALITY
			{
				// Token: 0x0400CB13 RID: 51987
				public static LocString NAME = "Bio";

				// Token: 0x0400CB14 RID: 51988
				public static LocString TOOLTIP = "<b>Bio</b>\nView this Duplicant's personality, skills, traits and amenities";

				// Token: 0x0400CB15 RID: 51989
				public static LocString GROUPNAME_BIO = "ABOUT";

				// Token: 0x0400CB16 RID: 51990
				public static LocString GROUPNAME_RESUME = "{0}'S RESUME";

				// Token: 0x02003AED RID: 15085
				public class RESUME
				{
					// Token: 0x0400E9BC RID: 59836
					public static LocString MASTERED_SKILLS = "<b><size=13>Learned Skills:</size></b>";

					// Token: 0x0400E9BD RID: 59837
					public static LocString MASTERED_SKILLS_TOOLTIP = string.Concat(new string[]
					{
						"All ",
						UI.PRE_KEYWORD,
						"Traits",
						UI.PST_KEYWORD,
						" and ",
						UI.PRE_KEYWORD,
						"Morale Needs",
						UI.PST_KEYWORD,
						" become permanent once a Duplicant has learned a new ",
						UI.PRE_KEYWORD,
						"Skill",
						UI.PST_KEYWORD,
						"\n\n",
						STRINGS.BUILDINGS.PREFABS.RESETSKILLSSTATION.NAME,
						"s can be built from the ",
						UI.FormatAsBuildMenuTab("Stations Tab", global::Action.Plan10),
						" to completely reset a Duplicant's learned ",
						UI.PRE_KEYWORD,
						"Skills",
						UI.PST_KEYWORD,
						", refunding all ",
						UI.PRE_KEYWORD,
						"Skill Points",
						UI.PST_KEYWORD
					});

					// Token: 0x0400E9BE RID: 59838
					public static LocString JOBTRAINING_TOOLTIP = string.Concat(new string[]
					{
						"{0} learned this ",
						UI.PRE_KEYWORD,
						"Skill",
						UI.PST_KEYWORD,
						" while working as a {1}"
					});

					// Token: 0x02003F08 RID: 16136
					public class APTITUDES
					{
						// Token: 0x0400F376 RID: 62326
						public static LocString NAME = "<b><size=13>Personal Interests:</size></b>";

						// Token: 0x0400F377 RID: 62327
						public static LocString TOOLTIP = "{0} enjoys these types of work";
					}

					// Token: 0x02003F09 RID: 16137
					public class PERKS
					{
						// Token: 0x0400F378 RID: 62328
						public static LocString NAME = "<b><size=13>Skill Training:</size></b>";

						// Token: 0x0400F379 RID: 62329
						public static LocString TOOLTIP = "These are permanent skills {0} gained from learned skills";
					}

					// Token: 0x02003F0A RID: 16138
					public class CURRENT_ROLE
					{
						// Token: 0x0400F37A RID: 62330
						public static LocString NAME = "<size=13><b>Current Job:</b> {0}</size>";

						// Token: 0x0400F37B RID: 62331
						public static LocString TOOLTIP = "{0} is currently working as a {1}";

						// Token: 0x0400F37C RID: 62332
						public static LocString NOJOB_TOOLTIP = "This {0} is... \"between jobs\" at present";
					}

					// Token: 0x02003F0B RID: 16139
					public class NO_MASTERED_SKILLS
					{
						// Token: 0x0400F37D RID: 62333
						public static LocString NAME = "None";

						// Token: 0x0400F37E RID: 62334
						public static LocString TOOLTIP = string.Concat(new string[]
						{
							"{0} has not learned any ",
							UI.PRE_KEYWORD,
							"Skills",
							UI.PST_KEYWORD,
							" yet"
						});
					}
				}

				// Token: 0x02003AEE RID: 15086
				public class EQUIPMENT
				{
					// Token: 0x0400E9BF RID: 59839
					public static LocString GROUPNAME_ROOMS = "AMENITIES";

					// Token: 0x0400E9C0 RID: 59840
					public static LocString GROUPNAME_OWNABLE = "EQUIPMENT";

					// Token: 0x0400E9C1 RID: 59841
					public static LocString NO_ASSIGNABLES = "None";

					// Token: 0x0400E9C2 RID: 59842
					public static LocString NO_ASSIGNABLES_TOOLTIP = "{0} has not been assigned any buildings of their own";

					// Token: 0x0400E9C3 RID: 59843
					public static LocString UNASSIGNED = "Unassigned";

					// Token: 0x0400E9C4 RID: 59844
					public static LocString UNASSIGNED_TOOLTIP = "This Duplicant has not been assigned a {0}";

					// Token: 0x0400E9C5 RID: 59845
					public static LocString ASSIGNED_TOOLTIP = "{2} has been assigned a {0}\n\nEffects: {1}";

					// Token: 0x0400E9C6 RID: 59846
					public static LocString NOEQUIPMENT = "None";

					// Token: 0x0400E9C7 RID: 59847
					public static LocString NOEQUIPMENT_TOOLTIP = "{0}'s wearing their Printday Suit and nothing more";
				}
			}

			// Token: 0x02002EE3 RID: 12003
			public class ENERGYCONSUMER
			{
				// Token: 0x0400CB17 RID: 51991
				public static LocString NAME = "Energy";

				// Token: 0x0400CB18 RID: 51992
				public static LocString TOOLTIP = "View how much power this building consumes";
			}

			// Token: 0x02002EE4 RID: 12004
			public class ENERGYWIRE
			{
				// Token: 0x0400CB19 RID: 51993
				public static LocString NAME = "Energy";

				// Token: 0x0400CB1A RID: 51994
				public static LocString TOOLTIP = "View this wire's network";
			}

			// Token: 0x02002EE5 RID: 12005
			public class ENERGYGENERATOR
			{
				// Token: 0x0400CB1B RID: 51995
				public static LocString NAME = "Energy";

				// Token: 0x0400CB1C RID: 51996
				public static LocString TOOLTIP = "<b>Energy</b>\nMonitor the power this building is generating";

				// Token: 0x0400CB1D RID: 51997
				public static LocString CIRCUITOVERVIEW = "CIRCUIT OVERVIEW";

				// Token: 0x0400CB1E RID: 51998
				public static LocString GENERATORS = "POWER GENERATORS";

				// Token: 0x0400CB1F RID: 51999
				public static LocString CONSUMERS = "POWER CONSUMERS";

				// Token: 0x0400CB20 RID: 52000
				public static LocString BATTERIES = "BATTERIES";

				// Token: 0x0400CB21 RID: 52001
				public static LocString DISCONNECTED = "Not connected to an electrical circuit";

				// Token: 0x0400CB22 RID: 52002
				public static LocString NOGENERATORS = "No generators on this circuit";

				// Token: 0x0400CB23 RID: 52003
				public static LocString NOCONSUMERS = "No consumers on this circuit";

				// Token: 0x0400CB24 RID: 52004
				public static LocString NOBATTERIES = "No batteries on this circuit";

				// Token: 0x0400CB25 RID: 52005
				public static LocString AVAILABLE_JOULES = UI.FormatAsLink("Power", "POWER") + " stored: {0}";

				// Token: 0x0400CB26 RID: 52006
				public static LocString AVAILABLE_JOULES_TOOLTIP = "Amount of power stored in batteries";

				// Token: 0x0400CB27 RID: 52007
				public static LocString WATTAGE_GENERATED = UI.FormatAsLink("Power", "POWER") + " produced: {0}";

				// Token: 0x0400CB28 RID: 52008
				public static LocString WATTAGE_GENERATED_TOOLTIP = "The total amount of power generated by this circuit";

				// Token: 0x0400CB29 RID: 52009
				public static LocString WATTAGE_CONSUMED = UI.FormatAsLink("Power", "POWER") + " consumed: {0}";

				// Token: 0x0400CB2A RID: 52010
				public static LocString WATTAGE_CONSUMED_TOOLTIP = "The total amount of power used by this circuit";

				// Token: 0x0400CB2B RID: 52011
				public static LocString POTENTIAL_WATTAGE_CONSUMED = "Potential power consumed: {0}";

				// Token: 0x0400CB2C RID: 52012
				public static LocString POTENTIAL_WATTAGE_CONSUMED_TOOLTIP = "The total amount of power that can be used by this circuit if all connected buildings are active";

				// Token: 0x0400CB2D RID: 52013
				public static LocString MAX_SAFE_WATTAGE = "Maximum safe wattage: {0}";

				// Token: 0x0400CB2E RID: 52014
				public static LocString MAX_SAFE_WATTAGE_TOOLTIP = "Exceeding this value will overload the circuit and can result in damage to wiring and buildings";
			}

			// Token: 0x02002EE6 RID: 12006
			public class DISEASE
			{
				// Token: 0x0400CB2F RID: 52015
				public static LocString NAME = "Germs";

				// Token: 0x0400CB30 RID: 52016
				public static LocString TOOLTIP = "<b>Germs</b>\nView germ resistance and risk of contagion";

				// Token: 0x0400CB31 RID: 52017
				public static LocString DISEASE_SOURCE = "DISEASE SOURCE";

				// Token: 0x0400CB32 RID: 52018
				public static LocString IMMUNE_SYSTEM = "GERM HOST";

				// Token: 0x0400CB33 RID: 52019
				public static LocString CONTRACTION_RATES = "CONTRACTION RATES";

				// Token: 0x0400CB34 RID: 52020
				public static LocString CURRENT_GERMS = "SURFACE GERMS";

				// Token: 0x0400CB35 RID: 52021
				public static LocString NO_CURRENT_GERMS = "SURFACE GERMS";

				// Token: 0x0400CB36 RID: 52022
				public static LocString GERMS_INFO = "GERM LIFE CYCLE";

				// Token: 0x0400CB37 RID: 52023
				public static LocString INFECTION_INFO = "INFECTION DETAILS";

				// Token: 0x0400CB38 RID: 52024
				public static LocString DISEASE_INFO_POPUP_HEADER = "DISEASE INFO: {0}";

				// Token: 0x0400CB39 RID: 52025
				public static LocString DISEASE_INFO_POPUP_BUTTON = "FULL INFO";

				// Token: 0x0400CB3A RID: 52026
				public static LocString DISEASE_INFO_POPUP_TOOLTIP = "View detailed germ and infection info for {0}";

				// Token: 0x02003AEF RID: 15087
				public class DETAILS
				{
					// Token: 0x0400E9C8 RID: 59848
					public static LocString NODISEASE = "No surface germs";

					// Token: 0x0400E9C9 RID: 59849
					public static LocString NODISEASE_TOOLTIP = "There are no germs present on this object";

					// Token: 0x0400E9CA RID: 59850
					public static LocString DISEASE_AMOUNT = "{0}: {1}";

					// Token: 0x0400E9CB RID: 59851
					public static LocString DISEASE_AMOUNT_TOOLTIP = "{0} are present on the surface of the selected object";

					// Token: 0x0400E9CC RID: 59852
					public static LocString DEATH_FORMAT = "{0} dead/cycle";

					// Token: 0x0400E9CD RID: 59853
					public static LocString DEATH_FORMAT_TOOLTIP = "Germ count is being reduced by {0}/cycle";

					// Token: 0x0400E9CE RID: 59854
					public static LocString GROWTH_FORMAT = "{0} spawned/cycle";

					// Token: 0x0400E9CF RID: 59855
					public static LocString GROWTH_FORMAT_TOOLTIP = "Germ count is being increased by {0}/cycle";

					// Token: 0x0400E9D0 RID: 59856
					public static LocString NEUTRAL_FORMAT = "No change";

					// Token: 0x0400E9D1 RID: 59857
					public static LocString NEUTRAL_FORMAT_TOOLTIP = "Germ count is static";

					// Token: 0x02003F0C RID: 16140
					public class GROWTH_FACTORS
					{
						// Token: 0x0400F37F RID: 62335
						public static LocString TITLE = "\nGrowth factors:";

						// Token: 0x0400F380 RID: 62336
						public static LocString TOOLTIP = "These conditions are contributing to the multiplication of germs";

						// Token: 0x0400F381 RID: 62337
						public static LocString RATE_OF_CHANGE = "Change rate: {0}";

						// Token: 0x0400F382 RID: 62338
						public static LocString RATE_OF_CHANGE_TOOLTIP = "Germ count is fluctuating at a rate of {0}";

						// Token: 0x0400F383 RID: 62339
						public static LocString HALF_LIFE_NEG = "Half life: {0}";

						// Token: 0x0400F384 RID: 62340
						public static LocString HALF_LIFE_NEG_TOOLTIP = "In {0} the germ count on this object will be halved";

						// Token: 0x0400F385 RID: 62341
						public static LocString HALF_LIFE_POS = "Doubling time: {0}";

						// Token: 0x0400F386 RID: 62342
						public static LocString HALF_LIFE_POS_TOOLTIP = "In {0} the germ count on this object will be doubled";

						// Token: 0x0400F387 RID: 62343
						public static LocString HALF_LIFE_NEUTRAL = "Static";

						// Token: 0x0400F388 RID: 62344
						public static LocString HALF_LIFE_NEUTRAL_TOOLTIP = "The germ count is neither increasing nor decreasing";

						// Token: 0x02003F3E RID: 16190
						public class SUBSTRATE
						{
							// Token: 0x0400F3C3 RID: 62403
							public static LocString GROW = "    • Growing on {0}: {1}";

							// Token: 0x0400F3C4 RID: 62404
							public static LocString GROW_TOOLTIP = "Contact with this substance is causing germs to multiply";

							// Token: 0x0400F3C5 RID: 62405
							public static LocString NEUTRAL = "    • No change on {0}";

							// Token: 0x0400F3C6 RID: 62406
							public static LocString NEUTRAL_TOOLTIP = "Contact with this substance has no effect on germ count";

							// Token: 0x0400F3C7 RID: 62407
							public static LocString DIE = "    • Dying on {0}: {1}";

							// Token: 0x0400F3C8 RID: 62408
							public static LocString DIE_TOOLTIP = "Contact with this substance is causing germs to die off";
						}

						// Token: 0x02003F3F RID: 16191
						public class ENVIRONMENT
						{
							// Token: 0x0400F3C9 RID: 62409
							public static LocString TITLE = "    • Surrounded by {0}: {1}";

							// Token: 0x0400F3CA RID: 62410
							public static LocString GROW_TOOLTIP = "This atmosphere is causing germs to multiply";

							// Token: 0x0400F3CB RID: 62411
							public static LocString DIE_TOOLTIP = "This atmosphere is causing germs to die off";
						}

						// Token: 0x02003F40 RID: 16192
						public class TEMPERATURE
						{
							// Token: 0x0400F3CC RID: 62412
							public static LocString TITLE = "    • Current temperature {0}: {1}";

							// Token: 0x0400F3CD RID: 62413
							public static LocString GROW_TOOLTIP = "This temperature is allowing germs to multiply";

							// Token: 0x0400F3CE RID: 62414
							public static LocString DIE_TOOLTIP = "This temperature is causing germs to die off";
						}

						// Token: 0x02003F41 RID: 16193
						public class PRESSURE
						{
							// Token: 0x0400F3CF RID: 62415
							public static LocString TITLE = "    • Current pressure {0}: {1}";

							// Token: 0x0400F3D0 RID: 62416
							public static LocString GROW_TOOLTIP = "Atmospheric pressure is causing germs to multiply";

							// Token: 0x0400F3D1 RID: 62417
							public static LocString DIE_TOOLTIP = "Atmospheric pressure is causing germs to die off";
						}

						// Token: 0x02003F42 RID: 16194
						public class RADIATION
						{
							// Token: 0x0400F3D2 RID: 62418
							public static LocString TITLE = "    • Exposed to {0} Rads: {1}";

							// Token: 0x0400F3D3 RID: 62419
							public static LocString DIE_TOOLTIP = "Radiation exposure is causing germs to die off";
						}

						// Token: 0x02003F43 RID: 16195
						public class DYING_OFF
						{
							// Token: 0x0400F3D4 RID: 62420
							public static LocString TITLE = "    • <b>Dying off: {0}</b>";

							// Token: 0x0400F3D5 RID: 62421
							public static LocString TOOLTIP = "Low germ count in this area is causing germs to die rapidly\n\nFewer than {0} are on this {1} of material.\n({2} germs/" + UI.UNITSUFFIXES.MASS.KILOGRAM + ")";
						}

						// Token: 0x02003F44 RID: 16196
						public class OVERPOPULATED
						{
							// Token: 0x0400F3D6 RID: 62422
							public static LocString TITLE = "    • <b>Overpopulated: {0}</b>";

							// Token: 0x0400F3D7 RID: 62423
							public static LocString TOOLTIP = "Too many germs are present in this area, resulting in rapid die-off until the population stabilizes\n\nA maximum of {0} can be on this {1} of material.\n({2} germs/" + UI.UNITSUFFIXES.MASS.KILOGRAM + ")";
						}
					}
				}
			}

			// Token: 0x02002EE7 RID: 12007
			public class NEEDS
			{
				// Token: 0x0400CB3B RID: 52027
				public static LocString NAME = "Stress";

				// Token: 0x0400CB3C RID: 52028
				public static LocString TOOLTIP = "View this Duplicant's psychological status";

				// Token: 0x0400CB3D RID: 52029
				public static LocString CURRENT_STRESS_LEVEL = "Current " + UI.FormatAsLink("Stress", "STRESS") + " Level: {0}";

				// Token: 0x0400CB3E RID: 52030
				public static LocString OVERVIEW = "Overview";

				// Token: 0x0400CB3F RID: 52031
				public static LocString STRESS_CREATORS = UI.FormatAsLink("Stress", "STRESS") + " Creators";

				// Token: 0x0400CB40 RID: 52032
				public static LocString STRESS_RELIEVERS = UI.FormatAsLink("Stress", "STRESS") + " Relievers";

				// Token: 0x0400CB41 RID: 52033
				public static LocString CURRENT_NEED_LEVEL = "Current Level: {0}";

				// Token: 0x0400CB42 RID: 52034
				public static LocString NEXT_NEED_LEVEL = "Next Level: {0}";
			}

			// Token: 0x02002EE8 RID: 12008
			public class EGG_CHANCES
			{
				// Token: 0x0400CB43 RID: 52035
				public static LocString CHANCE_FORMAT = "{0}: {1}";

				// Token: 0x0400CB44 RID: 52036
				public static LocString CHANCE_FORMAT_TOOLTIP = "This critter has a {1} chance of laying {0}s.\n\nThis probability increases when the creature:\n{2}";

				// Token: 0x0400CB45 RID: 52037
				public static LocString CHANCE_MOD_FORMAT = "    • {0}\n";

				// Token: 0x0400CB46 RID: 52038
				public static LocString CHANCE_FORMAT_TOOLTIP_NOMOD = "This critter has a {1} chance of laying {0}s.";
			}

			// Token: 0x02002EE9 RID: 12009
			public class BUILDING_CHORES
			{
				// Token: 0x0400CB47 RID: 52039
				public static LocString NAME = "Errands";

				// Token: 0x0400CB48 RID: 52040
				public static LocString TOOLTIP = "<b>Errands</b>\nView available errands and current queue";

				// Token: 0x0400CB49 RID: 52041
				public static LocString CHORE_TYPE_TOOLTIP = "Errand Type: {0}";

				// Token: 0x0400CB4A RID: 52042
				public static LocString AVAILABLE_CHORES = "AVAILABLE ERRANDS";

				// Token: 0x0400CB4B RID: 52043
				public static LocString DUPE_TOOLTIP_FAILED = "{Name} cannot currently {Errand}\n\nReason:\n{FailedPrecondition}";

				// Token: 0x0400CB4C RID: 52044
				public static LocString DUPE_TOOLTIP_SUCCEEDED = "{Description}\n\n{Errand}'s Type: {Groups}\n\n{Name}'s {BestGroup} Priority: {PersonalPriorityValue} ({PersonalPriority})\n{Building} Priority: {BuildingPriority}\nAll {BestGroup} Errands: {TypePriority}\n\nTotal Priority: {TotalPriority}";

				// Token: 0x0400CB4D RID: 52045
				public static LocString DUPE_TOOLTIP_DESC_ACTIVE = "{Name} is currently busy: \"{Errand}\"";

				// Token: 0x0400CB4E RID: 52046
				public static LocString DUPE_TOOLTIP_DESC_INACTIVE = "\"{Errand}\" is #{Rank} on {Name}'s To Do list, after they finish their current errand";
			}

			// Token: 0x02002EEA RID: 12010
			public class PROCESS_CONDITIONS
			{
				// Token: 0x0400CB4F RID: 52047
				public static LocString NAME = "LAUNCH CHECKLIST";

				// Token: 0x0400CB50 RID: 52048
				public static LocString ROCKETPREP = "Rocket Construction";

				// Token: 0x0400CB51 RID: 52049
				public static LocString ROCKETPREP_TOOLTIP = "It is recommended that all boxes on the Rocket Construction checklist be ticked before launching";

				// Token: 0x0400CB52 RID: 52050
				public static LocString ROCKETSTORAGE = "Cargo Manifest";

				// Token: 0x0400CB53 RID: 52051
				public static LocString ROCKETSTORAGE_TOOLTIP = "It is recommended that all boxes on the Cargo Manifest checklist be ticked before launching";

				// Token: 0x0400CB54 RID: 52052
				public static LocString ROCKETFLIGHT = "Flight Route";

				// Token: 0x0400CB55 RID: 52053
				public static LocString ROCKETFLIGHT_TOOLTIP = "A rocket requires a clear path to a set destination to conduct a mission";

				// Token: 0x0400CB56 RID: 52054
				public static LocString ROCKETBOARD = "Crew Manifest";

				// Token: 0x0400CB57 RID: 52055
				public static LocString ROCKETBOARD_TOOLTIP = "It is recommended that all boxes on the Crew Manifest checklist be ticked before launching";

				// Token: 0x0400CB58 RID: 52056
				public static LocString ALL = "Requirements";

				// Token: 0x0400CB59 RID: 52057
				public static LocString ALL_TOOLTIP = "These conditions must be fulfilled in order to launch a rocket mission";
			}

			// Token: 0x02002EEB RID: 12011
			public class COSMETICS
			{
				// Token: 0x0400CB5A RID: 52058
				public static LocString NAME = "Blueprint";

				// Token: 0x0400CB5B RID: 52059
				public static LocString TOOLTIP = "<b>Blueprint</b>\nView and change assigned blueprints";
			}

			// Token: 0x02002EEC RID: 12012
			public class MATERIAL
			{
				// Token: 0x0400CB5C RID: 52060
				public static LocString NAME = "Material";

				// Token: 0x0400CB5D RID: 52061
				public static LocString TOOLTIP = "<b>Material</b>\nView and change this building's construction material";

				// Token: 0x0400CB5E RID: 52062
				public static LocString SUB_HEADER_CURRENT_MATERIAL = "CURRENT MATERIAL";

				// Token: 0x0400CB5F RID: 52063
				public static LocString BUTTON_CHANGE_MATERIAL = "Change Material";
			}

			// Token: 0x02002EED RID: 12013
			public class CONFIGURATION
			{
				// Token: 0x0400CB60 RID: 52064
				public static LocString NAME = "Config";

				// Token: 0x0400CB61 RID: 52065
				public static LocString TOOLTIP = "<b>Config</b>\nView and change filters, recipes, production orders and more";

				// Token: 0x0400CB62 RID: 52066
				public static LocString TOOLTIP_DUPLICANT = "<b>Config</b>\nView and change assigned equipment and amenities";
			}
		}

		// Token: 0x020023EB RID: 9195
		public class BUILDMENU
		{
			// Token: 0x0400A1F9 RID: 41465
			public static LocString GRID_VIEW_TOGGLE_TOOLTIP = "Toggle Grid View";

			// Token: 0x0400A1FA RID: 41466
			public static LocString LIST_VIEW_TOGGLE_TOOLTIP = "Toggle List View";

			// Token: 0x0400A1FB RID: 41467
			public static LocString NO_SEARCH_RESULTS = "NO RESULTS FOUND";

			// Token: 0x0400A1FC RID: 41468
			public static LocString SEARCH_RESULTS_HEADER = "SEARCH RESULTS";

			// Token: 0x0400A1FD RID: 41469
			public static LocString SEARCH_TEXT_PLACEHOLDER = "Search all buildings...";

			// Token: 0x0400A1FE RID: 41470
			public static LocString CLEAR_SEARCH_TOOLTIP = "Clear search";
		}

		// Token: 0x020023EC RID: 9196
		public class BUILDINGEFFECTS
		{
			// Token: 0x0400A1FF RID: 41471
			public static LocString OPERATIONREQUIREMENTS = "<b>Requirements:</b>";

			// Token: 0x0400A200 RID: 41472
			public static LocString REQUIRESPOWER = UI.FormatAsLink("Power", "POWER") + ": {0}";

			// Token: 0x0400A201 RID: 41473
			public static LocString REQUIRESELEMENT = "Supply of {0}";

			// Token: 0x0400A202 RID: 41474
			public static LocString REQUIRESLIQUIDINPUT = UI.FormatAsLink("Liquid Intake Pipe", "LIQUIDPIPING");

			// Token: 0x0400A203 RID: 41475
			public static LocString REQUIRESLIQUIDOUTPUT = UI.FormatAsLink("Liquid Output Pipe", "LIQUIDPIPING");

			// Token: 0x0400A204 RID: 41476
			public static LocString REQUIRESLIQUIDOUTPUTS = "Two " + UI.FormatAsLink("Liquid Output Pipes", "LIQUIDPIPING");

			// Token: 0x0400A205 RID: 41477
			public static LocString REQUIRESGASINPUT = UI.FormatAsLink("Gas Intake Pipe", "GASPIPING");

			// Token: 0x0400A206 RID: 41478
			public static LocString REQUIRESGASOUTPUT = UI.FormatAsLink("Gas Output Pipe", "GASPIPING");

			// Token: 0x0400A207 RID: 41479
			public static LocString REQUIRESGASOUTPUTS = "Two " + UI.FormatAsLink("Gas Output Pipes", "GASPIPING");

			// Token: 0x0400A208 RID: 41480
			public static LocString REQUIRESMANUALOPERATION = "Duplicant operation";

			// Token: 0x0400A209 RID: 41481
			public static LocString REQUIRESSKILLEDOPERATION = "Skilled Duplicant operation";

			// Token: 0x0400A20A RID: 41482
			public static LocString REQUIRESSKILLEDOPERATION_DLC3 = "Skilled Duplicant operation";

			// Token: 0x0400A20B RID: 41483
			public static LocString REQUIRESCREATIVITY = "Duplicant " + UI.FormatAsLink("Creativity", "ARTING1");

			// Token: 0x0400A20C RID: 41484
			public static LocString REQUIRESPOWERGENERATOR = UI.FormatAsLink("Power", "POWER") + " generator";

			// Token: 0x0400A20D RID: 41485
			public static LocString REQUIRESSEED = "1 Unplanted " + UI.FormatAsLink("Seed", "PLANTS");

			// Token: 0x0400A20E RID: 41486
			public static LocString PREFERS_ROOM = "Preferred Room: {0}";

			// Token: 0x0400A20F RID: 41487
			public static LocString REQUIRESROOM = "Dedicated Room: {0}";

			// Token: 0x0400A210 RID: 41488
			public static LocString ALLOWS_FERTILIZER = "Plant " + UI.FormatAsLink("Fertilization", "WILTCONDITIONS");

			// Token: 0x0400A211 RID: 41489
			public static LocString ALLOWS_IRRIGATION = "Plant " + UI.FormatAsLink("Liquid", "WILTCONDITIONS");

			// Token: 0x0400A212 RID: 41490
			public static LocString ASSIGNEDDUPLICANT = "Duplicant assignment";

			// Token: 0x0400A213 RID: 41491
			public static LocString CONSUMESANYELEMENT = "Any Element";

			// Token: 0x0400A214 RID: 41492
			public static LocString ENABLESDOMESTICGROWTH = "Enables " + UI.FormatAsLink("Plant Domestication", "PLANTS");

			// Token: 0x0400A215 RID: 41493
			public static LocString TRANSFORMER_INPUT_WIRE = "Input " + UI.FormatAsLink("Power Wire", "WIRE");

			// Token: 0x0400A216 RID: 41494
			public static LocString TRANSFORMER_OUTPUT_WIRE = "Output " + UI.FormatAsLink("Power Wire", "WIRE") + " (Limited to {0})";

			// Token: 0x0400A217 RID: 41495
			public static LocString OPERATIONEFFECTS = "<b>Effects:</b>";

			// Token: 0x0400A218 RID: 41496
			public static LocString BATTERYCAPACITY = UI.FormatAsLink("Power", "POWER") + " capacity: {0}";

			// Token: 0x0400A219 RID: 41497
			public static LocString BATTERYLEAK = UI.FormatAsLink("Power", "POWER") + " leak: {0}";

			// Token: 0x0400A21A RID: 41498
			public static LocString STORAGECAPACITY = "Storage capacity: {0}";

			// Token: 0x0400A21B RID: 41499
			public static LocString ELEMENTEMITTED_INPUTTEMP = "{0}: {1}";

			// Token: 0x0400A21C RID: 41500
			public static LocString ELEMENTEMITTED_ENTITYTEMP = "{0}: {1}";

			// Token: 0x0400A21D RID: 41501
			public static LocString ELEMENTEMITTED_MINORENTITYTEMP = "{0}: {1}";

			// Token: 0x0400A21E RID: 41502
			public static LocString ELEMENTEMITTED_MINTEMP = "{0}: {1}";

			// Token: 0x0400A21F RID: 41503
			public static LocString ELEMENTEMITTED_FIXEDTEMP = "{0}: {1}";

			// Token: 0x0400A220 RID: 41504
			public static LocString ELEMENTCONSUMED = "{0}: {1}";

			// Token: 0x0400A221 RID: 41505
			public static LocString ELEMENTEMITTED_TOILET = "{0}: {1} per use";

			// Token: 0x0400A222 RID: 41506
			public static LocString ELEMENTEMITTEDPERUSE = "{0}: {1} per use";

			// Token: 0x0400A223 RID: 41507
			public static LocString DISEASEEMITTEDPERUSE = "{0}: {1} per use";

			// Token: 0x0400A224 RID: 41508
			public static LocString DISEASECONSUMEDPERUSE = "All Diseases: -{0} per use";

			// Token: 0x0400A225 RID: 41509
			public static LocString ELEMENTCONSUMEDPERUSE = "{0}: {1} per use";

			// Token: 0x0400A226 RID: 41510
			public static LocString ENERGYCONSUMED = UI.FormatAsLink("Power", "POWER") + " consumed: {0}";

			// Token: 0x0400A227 RID: 41511
			public static LocString ENERGYGENERATED = UI.FormatAsLink("Power", "POWER") + ": +{0}";

			// Token: 0x0400A228 RID: 41512
			public static LocString HEATGENERATED = UI.FormatAsLink("Heat", "HEAT") + ": +{0}/s";

			// Token: 0x0400A229 RID: 41513
			public static LocString HEATCONSUMED = UI.FormatAsLink("Heat", "HEAT") + ": -{0}/s";

			// Token: 0x0400A22A RID: 41514
			public static LocString HEATER_TARGETTEMPERATURE = "Target " + UI.FormatAsLink("Temperature", "HEAT") + ": {0}";

			// Token: 0x0400A22B RID: 41515
			public static LocString HEATGENERATED_AIRCONDITIONER = UI.FormatAsLink("Heat", "HEAT") + ": +{0} (Approximate Value)";

			// Token: 0x0400A22C RID: 41516
			public static LocString HEATGENERATED_LIQUIDCONDITIONER = UI.FormatAsLink("Heat", "HEAT") + ": +{0} (Approximate Value)";

			// Token: 0x0400A22D RID: 41517
			public static LocString FABRICATES = "Fabricates";

			// Token: 0x0400A22E RID: 41518
			public static LocString FABRICATEDITEM = "{1}";

			// Token: 0x0400A22F RID: 41519
			public static LocString PROCESSES = "Refines:";

			// Token: 0x0400A230 RID: 41520
			public static LocString PROCESSEDITEM = "{1} {0}";

			// Token: 0x0400A231 RID: 41521
			public static LocString PLANTERBOX_PENTALTY = "Planter box penalty";

			// Token: 0x0400A232 RID: 41522
			public static LocString DECORPROVIDED = UI.FormatAsLink("Decor", "DECOR") + ": {1} (Radius: {2} tiles)";

			// Token: 0x0400A233 RID: 41523
			public static LocString OVERHEAT_TEMP = "Overheat " + UI.FormatAsLink("Temperature", "HEAT") + ": {0}";

			// Token: 0x0400A234 RID: 41524
			public static LocString MINIMUM_TEMP = "Freeze " + UI.FormatAsLink("Temperature", "HEAT") + ": {0}";

			// Token: 0x0400A235 RID: 41525
			public static LocString OVER_PRESSURE_MASS = "Overpressure: {0}";

			// Token: 0x0400A236 RID: 41526
			public static LocString REFILLOXYGENTANK = "Refills Exosuit " + STRINGS.EQUIPMENT.PREFABS.OXYGEN_TANK.NAME;

			// Token: 0x0400A237 RID: 41527
			public static LocString DUPLICANTMOVEMENTBOOST = "Runspeed: {0}";

			// Token: 0x0400A238 RID: 41528
			public static LocString ELECTROBANKS = UI.FormatAsLink("Charge", "POWER") + ": {0}";

			// Token: 0x0400A239 RID: 41529
			public static LocString STRESSREDUCEDPERMINUTE = UI.FormatAsLink("Stress", "STRESS") + ": {0} per minute";

			// Token: 0x0400A23A RID: 41530
			public static LocString REMOVESEFFECTSUBTITLE = "Cures";

			// Token: 0x0400A23B RID: 41531
			public static LocString REMOVEDEFFECT = "{0}";

			// Token: 0x0400A23C RID: 41532
			public static LocString ADDED_EFFECT = "Added Effect: {0}";

			// Token: 0x0400A23D RID: 41533
			public static LocString GASCOOLING = UI.FormatAsLink("Cooling factor", "HEAT") + ": {0}";

			// Token: 0x0400A23E RID: 41534
			public static LocString LIQUIDCOOLING = UI.FormatAsLink("Cooling factor", "HEAT") + ": {0}";

			// Token: 0x0400A23F RID: 41535
			public static LocString MAX_WATTAGE = "Max " + UI.FormatAsLink("Power", "POWER") + ": {0}";

			// Token: 0x0400A240 RID: 41536
			public static LocString MAX_BITS = UI.FormatAsLink("Bit", "LOGIC") + " Depth: {0}";

			// Token: 0x0400A241 RID: 41537
			public static LocString RESEARCH_MATERIALS = "{0}: {1} per " + UI.FormatAsLink("Research", "RESEARCH") + " point";

			// Token: 0x0400A242 RID: 41538
			public static LocString PRODUCES_RESEARCH_POINTS = "{0}";

			// Token: 0x0400A243 RID: 41539
			public static LocString HIT_POINTS_PER_CYCLE = UI.FormatAsLink("Health", "Health") + " per cycle: {0}";

			// Token: 0x0400A244 RID: 41540
			public static LocString KCAL_PER_CYCLE = UI.FormatAsLink("KCal", "FOOD") + " per cycle: {0}";

			// Token: 0x0400A245 RID: 41541
			public static LocString REMOVES_DISEASE = "Kills germs";

			// Token: 0x0400A246 RID: 41542
			public static LocString DOCTORING = "Doctoring";

			// Token: 0x0400A247 RID: 41543
			public static LocString RECREATION = "Recreation";

			// Token: 0x0400A248 RID: 41544
			public static LocString COOLANT = "Coolant: {1} {0}";

			// Token: 0x0400A249 RID: 41545
			public static LocString REFINEMENT_ENERGY = "Heat: {0}";

			// Token: 0x0400A24A RID: 41546
			public static LocString IMPROVED_BUILDINGS = "Improved Buildings";

			// Token: 0x0400A24B RID: 41547
			public static LocString IMPROVED_PLANTS = "Improved Plants";

			// Token: 0x0400A24C RID: 41548
			public static LocString IMPROVED_BUILDINGS_ITEM = "{0}";

			// Token: 0x0400A24D RID: 41549
			public static LocString IMPROVED_PLANTS_ITEM = "{0}";

			// Token: 0x0400A24E RID: 41550
			public static LocString GEYSER_PRODUCTION = "{0}: {1} at {2}";

			// Token: 0x0400A24F RID: 41551
			public static LocString GEYSER_DISEASE = "Germs: {0}";

			// Token: 0x0400A250 RID: 41552
			public static LocString GEYSER_PERIOD = "Eruption Period: {0} every {1}";

			// Token: 0x0400A251 RID: 41553
			public static LocString GEYSER_YEAR_UNSTUDIED = "Active Period: (Requires Analysis)";

			// Token: 0x0400A252 RID: 41554
			public static LocString GEYSER_YEAR_PERIOD = "Active Period: {0} every {1}";

			// Token: 0x0400A253 RID: 41555
			public static LocString GEYSER_YEAR_NEXT_ACTIVE = "Next Activity: {0}";

			// Token: 0x0400A254 RID: 41556
			public static LocString GEYSER_YEAR_NEXT_DORMANT = "Next Dormancy: {0}";

			// Token: 0x0400A255 RID: 41557
			public static LocString GEYSER_YEAR_AVR_OUTPUT_UNSTUDIED = "Average Output: (Requires Analysis)";

			// Token: 0x0400A256 RID: 41558
			public static LocString GEYSER_YEAR_AVR_OUTPUT = "Average Output: {0}";

			// Token: 0x0400A257 RID: 41559
			public static LocString CAPTURE_METHOD_WRANGLE = "Capture Method: Wrangling";

			// Token: 0x0400A258 RID: 41560
			public static LocString CAPTURE_METHOD_FLYING_TRAP = "Capture Method: Airborne Critter Trap";

			// Token: 0x0400A259 RID: 41561
			public static LocString CAPTURE_METHOD_LAND_TRAP = "Capture Method: Critter Trap";

			// Token: 0x0400A25A RID: 41562
			public static LocString CAPTURE_METHOD_FISH_TRAP = "Capture Method: Fish Trap";

			// Token: 0x0400A25B RID: 41563
			public static LocString DIET_HEADER = "Digestion:";

			// Token: 0x0400A25C RID: 41564
			public static LocString DIET_CONSUMED = "    • Diet: {Foodlist}";

			// Token: 0x0400A25D RID: 41565
			public static LocString DIET_STORED = "    • Stores: {Foodlist}";

			// Token: 0x0400A25E RID: 41566
			public static LocString DIET_CONSUMED_ITEM = "{Food}: {Amount}";

			// Token: 0x0400A25F RID: 41567
			public static LocString DIET_PRODUCED = "    • Excretion: {Items}";

			// Token: 0x0400A260 RID: 41568
			public static LocString DIET_PRODUCED_ITEM = "{Item}: {Percent} of consumed mass";

			// Token: 0x0400A261 RID: 41569
			public static LocString DIET_PRODUCED_ITEM_FROM_PLANT = "{Item}: {Amount} when properly fed";

			// Token: 0x0400A262 RID: 41570
			public static LocString DIET_ADDITIONAL_PRODUCED = "Secondary Excretion: {Items}";

			// Token: 0x0400A263 RID: 41571
			public static LocString SCALE_GROWTH = "Shearable {Item}: {Amount} per {Time}";

			// Token: 0x0400A264 RID: 41572
			public static LocString SCALE_GROWTH_ATMO = "Shearable {Item}: {Amount} per {Time} ({Atmosphere})";

			// Token: 0x0400A265 RID: 41573
			public static LocString SCALE_GROWTH_TEMP = "Shearable {Item}: {Amount} per {Time} ({TempMin} - {TempMax})";

			// Token: 0x0400A266 RID: 41574
			public static LocString ACCESS_CONTROL = "Duplicant Access Permissions";

			// Token: 0x0400A267 RID: 41575
			public static LocString ROCKETRESTRICTION_HEADER = "Restriction Control:";

			// Token: 0x0400A268 RID: 41576
			public static LocString ROCKETRESTRICTION_BUILDINGS = "    • Buildings: {buildinglist}";

			// Token: 0x0400A269 RID: 41577
			public static LocString UNSTABLEENTOMBDEFENSEREADY = "Entomb Defense: Ready";

			// Token: 0x0400A26A RID: 41578
			public static LocString UNSTABLEENTOMBDEFENSETHREATENED = "Entomb Defense: Threatened";

			// Token: 0x0400A26B RID: 41579
			public static LocString UNSTABLEENTOMBDEFENSEREACTING = "Entomb Defense: Reacting";

			// Token: 0x0400A26C RID: 41580
			public static LocString UNSTABLEENTOMBDEFENSEOFF = "Entomb Defense: Off";

			// Token: 0x0400A26D RID: 41581
			public static LocString ITEM_TEMPERATURE_ADJUST = "Stored " + UI.FormatAsLink("Temperature", "HEAT") + ": {0}";

			// Token: 0x0400A26E RID: 41582
			public static LocString NOISE_CREATED = UI.FormatAsLink("Noise", "SOUND") + ": {0} dB (Radius: {1} tiles)";

			// Token: 0x0400A26F RID: 41583
			public static LocString MESS_TABLE_SALT = "Table Salt: +{0}";

			// Token: 0x0400A270 RID: 41584
			public static LocString ACTIVE_PARTICLE_CONSUMPTION = "Radbolts: {Rate}";

			// Token: 0x0400A271 RID: 41585
			public static LocString PARTICLE_PORT_INPUT = "Radbolt Input Port";

			// Token: 0x0400A272 RID: 41586
			public static LocString PARTICLE_PORT_OUTPUT = "Radbolt Output Port";

			// Token: 0x0400A273 RID: 41587
			public static LocString IN_ORBIT_REQUIRED = "Active In Space";

			// Token: 0x0400A274 RID: 41588
			public static LocString KETTLE_MELT_RATE = "Melting Rate: {0}";

			// Token: 0x0400A275 RID: 41589
			public static LocString FOOD_DEHYDRATOR_WATER_OUTPUT = "Wet Floor";

			// Token: 0x02002EEE RID: 12014
			public class TOOLTIPS
			{
				// Token: 0x0400CB63 RID: 52067
				public static LocString OPERATIONREQUIREMENTS = "All requirements must be met in order for this building to operate";

				// Token: 0x0400CB64 RID: 52068
				public static LocString REQUIRESPOWER = string.Concat(new string[]
				{
					"Must be connected to a power grid with at least ",
					UI.FormatAsNegativeRate("{0}"),
					" of available ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD
				});

				// Token: 0x0400CB65 RID: 52069
				public static LocString REQUIRESELEMENT = string.Concat(new string[]
				{
					"Must receive deliveries of ",
					UI.PRE_KEYWORD,
					"{0}",
					UI.PST_KEYWORD,
					" to function"
				});

				// Token: 0x0400CB66 RID: 52070
				public static LocString REQUIRESLIQUIDINPUT = string.Concat(new string[]
				{
					"Must receive ",
					UI.PRE_KEYWORD,
					"Liquid",
					UI.PST_KEYWORD,
					" from a ",
					STRINGS.BUILDINGS.PREFABS.LIQUIDCONDUIT.NAME,
					" system"
				});

				// Token: 0x0400CB67 RID: 52071
				public static LocString REQUIRESLIQUIDOUTPUT = string.Concat(new string[]
				{
					"Must expel ",
					UI.PRE_KEYWORD,
					"Liquid",
					UI.PST_KEYWORD,
					" through a ",
					STRINGS.BUILDINGS.PREFABS.LIQUIDCONDUIT.NAME,
					" system"
				});

				// Token: 0x0400CB68 RID: 52072
				public static LocString REQUIRESLIQUIDOUTPUTS = string.Concat(new string[]
				{
					"Must expel ",
					UI.PRE_KEYWORD,
					"Liquid",
					UI.PST_KEYWORD,
					" through a ",
					STRINGS.BUILDINGS.PREFABS.LIQUIDCONDUIT.NAME,
					" system"
				});

				// Token: 0x0400CB69 RID: 52073
				public static LocString REQUIRESGASINPUT = string.Concat(new string[]
				{
					"Must receive ",
					UI.PRE_KEYWORD,
					"Gas",
					UI.PST_KEYWORD,
					" from a ",
					STRINGS.BUILDINGS.PREFABS.GASCONDUIT.NAME,
					" system"
				});

				// Token: 0x0400CB6A RID: 52074
				public static LocString REQUIRESGASOUTPUT = string.Concat(new string[]
				{
					"Must expel ",
					UI.PRE_KEYWORD,
					"Gas",
					UI.PST_KEYWORD,
					" through a ",
					STRINGS.BUILDINGS.PREFABS.GASCONDUIT.NAME,
					" system"
				});

				// Token: 0x0400CB6B RID: 52075
				public static LocString REQUIRESGASOUTPUTS = string.Concat(new string[]
				{
					"Must expel ",
					UI.PRE_KEYWORD,
					"Gas",
					UI.PST_KEYWORD,
					" through a ",
					STRINGS.BUILDINGS.PREFABS.GASCONDUIT.NAME,
					" system"
				});

				// Token: 0x0400CB6C RID: 52076
				public static LocString REQUIRESMANUALOPERATION = "A Duplicant must be present to run this building";

				// Token: 0x0400CB6D RID: 52077
				public static LocString REQUIRESSKILLEDOPERATION = "Only a Duplicant with the {Skill} skill can use this building";

				// Token: 0x0400CB6E RID: 52078
				public static LocString REQUIRESSKILLEDOPERATION_DLC3 = "Only a Duplicant with the {Skill} skill or {Booster} can use this building";

				// Token: 0x0400CB6F RID: 52079
				public static LocString REQUIRESCREATIVITY = "An expressive Duplicant must work on this object to create " + UI.PRE_KEYWORD + "Art" + UI.PST_KEYWORD;

				// Token: 0x0400CB70 RID: 52080
				public static LocString REQUIRESPOWERGENERATOR = string.Concat(new string[]
				{
					"Must be connected to a ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD,
					" producing generator to function"
				});

				// Token: 0x0400CB71 RID: 52081
				public static LocString REQUIRESSEED = "Must receive a plant " + UI.PRE_KEYWORD + "Seed" + UI.PST_KEYWORD;

				// Token: 0x0400CB72 RID: 52082
				public static LocString PREFERS_ROOM = "This building gains additional effects or functionality when built inside a " + UI.PRE_KEYWORD + "{0}" + UI.PST_KEYWORD;

				// Token: 0x0400CB73 RID: 52083
				public static LocString REQUIRESROOM = string.Concat(new string[]
				{
					"Must be built within a dedicated ",
					UI.PRE_KEYWORD,
					"Room",
					UI.PST_KEYWORD,
					"\n\n",
					UI.PRE_KEYWORD,
					"Room",
					UI.PST_KEYWORD,
					" will become a ",
					UI.PRE_KEYWORD,
					"{0}",
					UI.PST_KEYWORD,
					" after construction"
				});

				// Token: 0x0400CB74 RID: 52084
				public static LocString ALLOWS_FERTILIZER = string.Concat(new string[]
				{
					"Allows ",
					UI.PRE_KEYWORD,
					"Fertilizer",
					UI.PST_KEYWORD,
					" to be delivered to plants"
				});

				// Token: 0x0400CB75 RID: 52085
				public static LocString ALLOWS_IRRIGATION = string.Concat(new string[]
				{
					"Allows ",
					UI.PRE_KEYWORD,
					"Liquid",
					UI.PST_KEYWORD,
					" to be delivered to plants"
				});

				// Token: 0x0400CB76 RID: 52086
				public static LocString ALLOWS_IRRIGATION_PIPE = string.Concat(new string[]
				{
					"Allows irrigation ",
					UI.PRE_KEYWORD,
					"Pipe",
					UI.PST_KEYWORD,
					" connection"
				});

				// Token: 0x0400CB77 RID: 52087
				public static LocString ASSIGNEDDUPLICANT = "This amenity may only be used by the Duplicant it is assigned to";

				// Token: 0x0400CB78 RID: 52088
				public static LocString BUILDINGROOMREQUIREMENTCLASS = "This category of building may be required or prohibited in certain " + UI.PRE_KEYWORD + "Rooms" + UI.PST_KEYWORD;

				// Token: 0x0400CB79 RID: 52089
				public static LocString OPERATIONEFFECTS = "The building will produce these effects when its requirements are met";

				// Token: 0x0400CB7A RID: 52090
				public static LocString BATTERYCAPACITY = string.Concat(new string[]
				{
					"Can hold <b>{0}</b> of ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD,
					" when connected to a ",
					UI.PRE_KEYWORD,
					"Generator",
					UI.PST_KEYWORD
				});

				// Token: 0x0400CB7B RID: 52091
				public static LocString BATTERYLEAK = string.Concat(new string[]
				{
					UI.FormatAsNegativeRate("{0}"),
					" of this battery's charge will be lost as ",
					UI.PRE_KEYWORD,
					"Heat",
					UI.PST_KEYWORD
				});

				// Token: 0x0400CB7C RID: 52092
				public static LocString STORAGECAPACITY = "Holds up to <b>{0}</b> of material";

				// Token: 0x0400CB7D RID: 52093
				public static LocString ELEMENTEMITTED_INPUTTEMP = string.Concat(new string[]
				{
					"Produces ",
					UI.FormatAsPositiveRate("{1}"),
					" of ",
					UI.PRE_KEYWORD,
					"{0}",
					UI.PST_KEYWORD,
					" when in use\n\nIt will be the combined ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" of the input materials."
				});

				// Token: 0x0400CB7E RID: 52094
				public static LocString ELEMENTEMITTED_ENTITYTEMP = string.Concat(new string[]
				{
					"Produces ",
					UI.FormatAsPositiveRate("{1}"),
					" of ",
					UI.PRE_KEYWORD,
					"{0}",
					UI.PST_KEYWORD,
					" when in use\n\nIt will be the ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" of the building at the time of production"
				});

				// Token: 0x0400CB7F RID: 52095
				public static LocString ELEMENTEMITTED_MINORENTITYTEMP = string.Concat(new string[]
				{
					"Produces ",
					UI.FormatAsPositiveRate("{1}"),
					" of ",
					UI.PRE_KEYWORD,
					"{0}",
					UI.PST_KEYWORD,
					" when in use\n\nIt will be at least <b>{2}</b>, or hotter if the building is hotter."
				});

				// Token: 0x0400CB80 RID: 52096
				public static LocString ELEMENTEMITTED_MINTEMP = string.Concat(new string[]
				{
					"Produces ",
					UI.FormatAsPositiveRate("{1}"),
					" of ",
					UI.PRE_KEYWORD,
					"{0}",
					UI.PST_KEYWORD,
					" when in use\n\nIt will be at least <b>{2}</b>, or hotter if the input materials are hotter."
				});

				// Token: 0x0400CB81 RID: 52097
				public static LocString ELEMENTEMITTED_FIXEDTEMP = string.Concat(new string[]
				{
					"Produces ",
					UI.FormatAsPositiveRate("{1}"),
					" of ",
					UI.PRE_KEYWORD,
					"{0}",
					UI.PST_KEYWORD,
					" when in use\n\nIt will be produced at <b>{2}</b>."
				});

				// Token: 0x0400CB82 RID: 52098
				public static LocString ELEMENTCONSUMED = string.Concat(new string[]
				{
					"Consumes ",
					UI.FormatAsNegativeRate("{1}"),
					" of ",
					UI.PRE_KEYWORD,
					"{0}",
					UI.PST_KEYWORD,
					" when in use"
				});

				// Token: 0x0400CB83 RID: 52099
				public static LocString ELEMENTEMITTED_TOILET = string.Concat(new string[]
				{
					"Produces ",
					UI.FormatAsPositiveRate("{1}"),
					" of ",
					UI.PRE_KEYWORD,
					"{0}",
					UI.PST_KEYWORD,
					" per use\n\nDuplicant waste is emitted at <b>{2}</b>."
				});

				// Token: 0x0400CB84 RID: 52100
				public static LocString ELEMENTEMITTEDPERUSE = string.Concat(new string[]
				{
					"Produces ",
					UI.FormatAsPositiveRate("{1}"),
					" of ",
					UI.PRE_KEYWORD,
					"{0}",
					UI.PST_KEYWORD,
					" per use\n\nIt will be the ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" of the input materials."
				});

				// Token: 0x0400CB85 RID: 52101
				public static LocString DISEASEEMITTEDPERUSE = string.Concat(new string[]
				{
					"Produces ",
					UI.FormatAsPositiveRate("{1}"),
					" of ",
					UI.PRE_KEYWORD,
					"{0}",
					UI.PST_KEYWORD,
					" per use"
				});

				// Token: 0x0400CB86 RID: 52102
				public static LocString DISEASECONSUMEDPERUSE = "Removes " + UI.FormatAsNegativeRate("{0}") + " per use";

				// Token: 0x0400CB87 RID: 52103
				public static LocString ELEMENTCONSUMEDPERUSE = string.Concat(new string[]
				{
					"Consumes ",
					UI.FormatAsNegativeRate("{1}"),
					" of ",
					UI.PRE_KEYWORD,
					"{0}",
					UI.PST_KEYWORD,
					" per use"
				});

				// Token: 0x0400CB88 RID: 52104
				public static LocString ENERGYCONSUMED = string.Concat(new string[]
				{
					"Draws ",
					UI.FormatAsNegativeRate("{0}"),
					" from the ",
					UI.PRE_KEYWORD,
					"Power Grid",
					UI.PST_KEYWORD,
					" it's connected to"
				});

				// Token: 0x0400CB89 RID: 52105
				public static LocString ENERGYGENERATED = string.Concat(new string[]
				{
					"Produces ",
					UI.FormatAsPositiveRate("{0}"),
					" for the ",
					UI.PRE_KEYWORD,
					"Power Grid",
					UI.PST_KEYWORD,
					" it's connected to"
				});

				// Token: 0x0400CB8A RID: 52106
				public static LocString ENABLESDOMESTICGROWTH = string.Concat(new string[]
				{
					"Accelerates ",
					UI.PRE_KEYWORD,
					"Plant",
					UI.PST_KEYWORD,
					" growth and maturation"
				});

				// Token: 0x0400CB8B RID: 52107
				public static LocString HEATGENERATED = string.Concat(new string[]
				{
					"Generates ",
					UI.FormatAsPositiveRate("{0}"),
					" per second\n\nSum ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" change is affected by the material attributes of the heated substance:\n    • mass\n    • specific heat capacity\n    • surface area\n    • insulation thickness\n    • thermal conductivity"
				});

				// Token: 0x0400CB8C RID: 52108
				public static LocString HEATCONSUMED = string.Concat(new string[]
				{
					"Dissipates ",
					UI.FormatAsNegativeRate("{0}"),
					" per second\n\nSum ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" change can be affected by the material attributes of the cooled substance:\n    • mass\n    • specific heat capacity\n    • surface area\n    • insulation thickness\n    • thermal conductivity"
				});

				// Token: 0x0400CB8D RID: 52109
				public static LocString HEATER_TARGETTEMPERATURE = string.Concat(new string[]
				{
					"Stops heating when the surrounding average ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" is above <b>{0}</b>"
				});

				// Token: 0x0400CB8E RID: 52110
				public static LocString FABRICATES = "Fabrication is the production of items and equipment";

				// Token: 0x0400CB8F RID: 52111
				public static LocString PROCESSES = "Processes raw materials into refined materials";

				// Token: 0x0400CB90 RID: 52112
				public static LocString PROCESSEDITEM = "Refining this material produces " + UI.PRE_KEYWORD + "{0}" + UI.PST_KEYWORD;

				// Token: 0x0400CB91 RID: 52113
				public static LocString PLANTERBOX_PENTALTY = "Plants grow more slowly when contained within boxes";

				// Token: 0x0400CB92 RID: 52114
				public static LocString DECORPROVIDED = string.Concat(new string[]
				{
					"Improves ",
					UI.PRE_KEYWORD,
					"Decor",
					UI.PST_KEYWORD,
					" values by ",
					UI.FormatAsPositiveModifier("<b>{0}</b>"),
					" in a <b>{1}</b> tile radius"
				});

				// Token: 0x0400CB93 RID: 52115
				public static LocString DECORDECREASED = string.Concat(new string[]
				{
					"Decreases ",
					UI.PRE_KEYWORD,
					"Decor",
					UI.PST_KEYWORD,
					" values by ",
					UI.FormatAsNegativeModifier("<b>{0}</b>"),
					" in a <b>{1}</b> tile radius"
				});

				// Token: 0x0400CB94 RID: 52116
				public static LocString OVERHEAT_TEMP = "Begins overheating at <b>{0}</b>";

				// Token: 0x0400CB95 RID: 52117
				public static LocString MINIMUM_TEMP = "Ceases to function when temperatures fall below <b>{0}</b>";

				// Token: 0x0400CB96 RID: 52118
				public static LocString OVER_PRESSURE_MASS = "Ceases to function when the surrounding mass is above <b>{0}</b>";

				// Token: 0x0400CB97 RID: 52119
				public static LocString REFILLOXYGENTANK = string.Concat(new string[]
				{
					"Refills ",
					UI.PRE_KEYWORD,
					"Exosuit",
					UI.PST_KEYWORD,
					" Oxygen tanks with ",
					UI.PRE_KEYWORD,
					"Oxygen",
					UI.PST_KEYWORD,
					" for reuse"
				});

				// Token: 0x0400CB98 RID: 52120
				public static LocString DUPLICANTMOVEMENTBOOST = "Duplicants walk <b>{0}</b> faster on this tile";

				// Token: 0x0400CB99 RID: 52121
				public static LocString ELECTROBANKS = string.Concat(new string[]
				{
					"Power Banks store {0} of ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD,
					"\n\nThey can be discharged by circuits, buildings and Bionic Duplicants"
				});

				// Token: 0x0400CB9A RID: 52122
				public static LocString STRESSREDUCEDPERMINUTE = string.Concat(new string[]
				{
					"Removes <b>{0}</b> of Duplicants' ",
					UI.PRE_KEYWORD,
					"Stress",
					UI.PST_KEYWORD,
					" for every uninterrupted minute of use"
				});

				// Token: 0x0400CB9B RID: 52123
				public static LocString REMOVESEFFECTSUBTITLE = "Use of this building will remove the listed effects";

				// Token: 0x0400CB9C RID: 52124
				public static LocString REMOVEDEFFECT = "{0}";

				// Token: 0x0400CB9D RID: 52125
				public static LocString ADDED_EFFECT = "Effect being applied:\n\n{0}\n{1}";

				// Token: 0x0400CB9E RID: 52126
				public static LocString GASCOOLING = string.Concat(new string[]
				{
					"Reduces the ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" of piped ",
					UI.PRE_KEYWORD,
					"Gases",
					UI.PST_KEYWORD,
					" by <b>{0}</b>"
				});

				// Token: 0x0400CB9F RID: 52127
				public static LocString LIQUIDCOOLING = string.Concat(new string[]
				{
					"Reduces the ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" of piped ",
					UI.PRE_KEYWORD,
					"Liquid",
					UI.PST_KEYWORD,
					" by <b>{0}</b>"
				});

				// Token: 0x0400CBA0 RID: 52128
				public static LocString MAX_WATTAGE = string.Concat(new string[]
				{
					"Drawing more than the maximum allowed ",
					UI.PRE_KEYWORD,
					"Watts",
					UI.PST_KEYWORD,
					" can result in damage to the circuit"
				});

				// Token: 0x0400CBA1 RID: 52129
				public static LocString MAX_BITS = string.Concat(new string[]
				{
					"Sending an ",
					UI.PRE_KEYWORD,
					"Automation Signal",
					UI.PST_KEYWORD,
					" with a higher ",
					UI.PRE_KEYWORD,
					"Bit Depth",
					UI.PST_KEYWORD,
					" than the connected ",
					UI.PRE_KEYWORD,
					"Logic Wire",
					UI.PST_KEYWORD,
					" can result in damage to the circuit"
				});

				// Token: 0x0400CBA2 RID: 52130
				public static LocString RESEARCH_MATERIALS = string.Concat(new string[]
				{
					"This research station consumes ",
					UI.FormatAsNegativeRate("{1}"),
					" of ",
					UI.PRE_KEYWORD,
					"{0}",
					UI.PST_KEYWORD,
					" for each ",
					UI.PRE_KEYWORD,
					"Research Point",
					UI.PST_KEYWORD,
					" produced"
				});

				// Token: 0x0400CBA3 RID: 52131
				public static LocString PRODUCES_RESEARCH_POINTS = string.Concat(new string[]
				{
					"Produces ",
					UI.PRE_KEYWORD,
					"{0}",
					UI.PST_KEYWORD,
					" research"
				});

				// Token: 0x0400CBA4 RID: 52132
				public static LocString REMOVES_DISEASE = string.Concat(new string[]
				{
					"The cooking process kills all ",
					UI.PRE_KEYWORD,
					"Germs",
					UI.PST_KEYWORD,
					" present in the ingredients, removing the ",
					UI.PRE_KEYWORD,
					"Disease",
					UI.PST_KEYWORD,
					" risk when eating the product"
				});

				// Token: 0x0400CBA5 RID: 52133
				public static LocString DOCTORING = "Doctoring increases existing health benefits and can allow the treatment of otherwise stubborn " + UI.PRE_KEYWORD + "Diseases" + UI.PST_KEYWORD;

				// Token: 0x0400CBA6 RID: 52134
				public static LocString RECREATION = string.Concat(new string[]
				{
					"Improves Duplicant ",
					UI.PRE_KEYWORD,
					"Morale",
					UI.PST_KEYWORD,
					" during scheduled ",
					UI.PRE_KEYWORD,
					"Downtime",
					UI.PST_KEYWORD
				});

				// Token: 0x0400CBA7 RID: 52135
				public static LocString HEATGENERATED_AIRCONDITIONER = string.Concat(new string[]
				{
					"Generates ",
					UI.PRE_KEYWORD,
					"Heat",
					UI.PST_KEYWORD,
					" based on the ",
					UI.PRE_KEYWORD,
					"Volume",
					UI.PST_KEYWORD,
					" and ",
					UI.PRE_KEYWORD,
					"Specific Heat Capacity",
					UI.PST_KEYWORD,
					" of the pumped ",
					UI.PRE_KEYWORD,
					"Gas",
					UI.PST_KEYWORD,
					"\n\nCooling 1",
					UI.UNITSUFFIXES.MASS.KILOGRAM,
					" of ",
					ELEMENTS.OXYGEN.NAME,
					" the entire <b>{1}</b> will output <b>{0}</b>"
				});

				// Token: 0x0400CBA8 RID: 52136
				public static LocString HEATGENERATED_LIQUIDCONDITIONER = string.Concat(new string[]
				{
					"Generates ",
					UI.PRE_KEYWORD,
					"Heat",
					UI.PST_KEYWORD,
					" based on the ",
					UI.PRE_KEYWORD,
					"Volume",
					UI.PST_KEYWORD,
					" and ",
					UI.PRE_KEYWORD,
					"Specific Heat Capacity",
					UI.PST_KEYWORD,
					" of the pumped ",
					UI.PRE_KEYWORD,
					"Liquid",
					UI.PST_KEYWORD,
					"\n\nCooling 10",
					UI.UNITSUFFIXES.MASS.KILOGRAM,
					" of ",
					ELEMENTS.WATER.NAME,
					" the entire <b>{1}</b> will output <b>{0}</b>"
				});

				// Token: 0x0400CBA9 RID: 52137
				public static LocString MOVEMENT_BONUS = "Increases the Runspeed of Duplicants";

				// Token: 0x0400CBAA RID: 52138
				public static LocString COOLANT = string.Concat(new string[]
				{
					"<b>{1}</b> of ",
					UI.PRE_KEYWORD,
					"{0}",
					UI.PST_KEYWORD,
					" coolant is required to cool off an item produced by this building\n\nCoolant ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" increase is variable and dictated by the amount of energy needed to cool the produced item"
				});

				// Token: 0x0400CBAB RID: 52139
				public static LocString REFINEMENT_ENERGY_HAS_COOLANT = string.Concat(new string[]
				{
					UI.FormatAsPositiveRate("{0}"),
					" of ",
					UI.PRE_KEYWORD,
					"Heat",
					UI.PST_KEYWORD,
					" will be produced to cool off the fabricated item\n\nThis will raise the ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" of the contained ",
					UI.PRE_KEYWORD,
					"{1}",
					UI.PST_KEYWORD,
					" by ",
					UI.FormatAsPositiveModifier("{2}"),
					", and heat the containing building"
				});

				// Token: 0x0400CBAC RID: 52140
				public static LocString REFINEMENT_ENERGY_NO_COOLANT = string.Concat(new string[]
				{
					UI.FormatAsPositiveRate("{0}"),
					" of ",
					UI.PRE_KEYWORD,
					"Heat",
					UI.PST_KEYWORD,
					" will be produced to cool off the fabricated item\n\nIf ",
					UI.PRE_KEYWORD,
					"{1}",
					UI.PST_KEYWORD,
					" is used for coolant, its ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" will be raised by ",
					UI.FormatAsPositiveModifier("{2}"),
					", and will heat the containing building"
				});

				// Token: 0x0400CBAD RID: 52141
				public static LocString IMPROVED_BUILDINGS = UI.PRE_KEYWORD + "Tune Ups" + UI.PST_KEYWORD + " will improve these buildings:";

				// Token: 0x0400CBAE RID: 52142
				public static LocString IMPROVED_BUILDINGS_ITEM = "{0}";

				// Token: 0x0400CBAF RID: 52143
				public static LocString IMPROVED_PLANTS = UI.PRE_KEYWORD + "Crop Tending" + UI.PST_KEYWORD + " will improve growth times for these plants:";

				// Token: 0x0400CBB0 RID: 52144
				public static LocString IMPROVED_PLANTS_ITEM = "{0}";

				// Token: 0x0400CBB1 RID: 52145
				public static LocString GEYSER_PRODUCTION = string.Concat(new string[]
				{
					"While erupting, this geyser will produce ",
					UI.PRE_KEYWORD,
					"{0}",
					UI.PST_KEYWORD,
					" at a rate of ",
					UI.FormatAsPositiveRate("{1}"),
					", and at a ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" of <b>{2}</b>"
				});

				// Token: 0x0400CBB2 RID: 52146
				public static LocString GEYSER_PRODUCTION_GEOTUNED = string.Concat(new string[]
				{
					"While erupting, this geyser will produce ",
					UI.PRE_KEYWORD,
					"{0}",
					UI.PST_KEYWORD,
					" at a rate of ",
					UI.FormatAsPositiveRate("{1}"),
					", and at a ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" of <b>{2}</b>"
				});

				// Token: 0x0400CBB3 RID: 52147
				public static LocString GEYSER_PRODUCTION_GEOTUNED_COUNT = "<b>{0}</b> of <b>{1}</b> Geotuners targeting this geyser are amplifying it";

				// Token: 0x0400CBB4 RID: 52148
				public static LocString GEYSER_PRODUCTION_GEOTUNED_TOTAL = "Total geotuning: {0} {1}";

				// Token: 0x0400CBB5 RID: 52149
				public static LocString GEYSER_PRODUCTION_GEOTUNED_TOTAL_ROW_TITLE = "Geotuned ";

				// Token: 0x0400CBB6 RID: 52150
				public static LocString GEYSER_DISEASE = UI.PRE_KEYWORD + "{0}" + UI.PST_KEYWORD + " germs are present in the output of this geyser";

				// Token: 0x0400CBB7 RID: 52151
				public static LocString GEYSER_PERIOD = "This geyser will produce for <b>{0}</b> of every <b>{1}</b>";

				// Token: 0x0400CBB8 RID: 52152
				public static LocString GEYSER_YEAR_UNSTUDIED = "A researcher must analyze this geyser to determine its geoactive period";

				// Token: 0x0400CBB9 RID: 52153
				public static LocString GEYSER_YEAR_PERIOD = "This geyser will be active for <b>{0}</b> out of every <b>{1}</b>\n\nIt will be dormant the rest of the time";

				// Token: 0x0400CBBA RID: 52154
				public static LocString GEYSER_YEAR_NEXT_ACTIVE = "This geyser will become active in <b>{0}</b>";

				// Token: 0x0400CBBB RID: 52155
				public static LocString GEYSER_YEAR_NEXT_DORMANT = "This geyser will become dormant in <b>{0}</b>";

				// Token: 0x0400CBBC RID: 52156
				public static LocString GEYSER_YEAR_AVR_OUTPUT_UNSTUDIED = "A researcher must analyze this geyser to determine its average output rate";

				// Token: 0x0400CBBD RID: 52157
				public static LocString GEYSER_YEAR_AVR_OUTPUT = "This geyser emits an average of {average} of {element} during its lifetime\n\nThis includes its dormant period";

				// Token: 0x0400CBBE RID: 52158
				public static LocString GEYSER_YEAR_AVR_OUTPUT_BREAKDOWN_TITLE = "Total Geotuning ";

				// Token: 0x0400CBBF RID: 52159
				public static LocString GEYSER_YEAR_AVR_OUTPUT_BREAKDOWN_ROW = "Geotuned ";

				// Token: 0x0400CBC0 RID: 52160
				public static LocString CAPTURE_METHOD_WRANGLE = string.Concat(new string[]
				{
					"This critter can be captured\n\nMark critters for capture using the ",
					UI.FormatAsTool("Wrangle Tool", global::Action.Capture),
					"\n\nDuplicants must possess the ",
					UI.PRE_KEYWORD,
					"Critter Ranching",
					UI.PST_KEYWORD,
					" skill in order to wrangle critters"
				});

				// Token: 0x0400CBC1 RID: 52161
				public static LocString CAPTURE_METHOD_FLYING_TRAP = "This critter can be captured and moved using an " + STRINGS.BUILDINGS.PREFABS.CREATUREAIRTRAP.NAME;

				// Token: 0x0400CBC2 RID: 52162
				public static LocString CAPTURE_METHOD_TRAP = "This critter can be captured and moved using a " + STRINGS.BUILDINGS.PREFABS.CREATURETRAP.NAME;

				// Token: 0x0400CBC3 RID: 52163
				public static LocString CAPTURE_METHOD_FISH_TRAP = "This critter can be captured and moved using a " + STRINGS.BUILDINGS.PREFABS.FISHTRAP.NAME;

				// Token: 0x0400CBC4 RID: 52164
				public static LocString NOISE_POLLUTION_INCREASE = "Produces noise at <b>{0} dB</b> in a <b>{1}</b> tile radius";

				// Token: 0x0400CBC5 RID: 52165
				public static LocString NOISE_POLLUTION_DECREASE = "Dampens noise at <b>{0} dB</b> in a <b>{1}</b> tile radius";

				// Token: 0x0400CBC6 RID: 52166
				public static LocString ITEM_TEMPERATURE_ADJUST = string.Concat(new string[]
				{
					"Stored items will reach a ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" of <b>{0}</b> over time"
				});

				// Token: 0x0400CBC7 RID: 52167
				public static LocString DIET_HEADER = "Creatures will eat and digest only specific materials";

				// Token: 0x0400CBC8 RID: 52168
				public static LocString DIET_CONSUMED = "This critter can typically consume these materials at the following rates:\n\n{Foodlist}";

				// Token: 0x0400CBC9 RID: 52169
				public static LocString DIET_PRODUCED = "This critter will \"produce\" the following materials:\n\n{Items}";

				// Token: 0x0400CBCA RID: 52170
				public static LocString DIET_ADDITIONAL_PRODUCED = "This critter gets bloated after eating and will produce {Items}";

				// Token: 0x0400CBCB RID: 52171
				public static LocString ROCKETRESTRICTION_HEADER = "Controls whether a building is operational within a rocket interior";

				// Token: 0x0400CBCC RID: 52172
				public static LocString ROCKETRESTRICTION_BUILDINGS = "This station controls the operational status of the following buildings:\n\n{buildinglist}";

				// Token: 0x0400CBCD RID: 52173
				public static LocString UNSTABLEENTOMBDEFENSEREADY = string.Concat(new string[]
				{
					"This plant is ready to shake off ",
					UI.PRE_KEYWORD,
					"Unstable",
					UI.PST_KEYWORD,
					" elements that threaten to entomb it"
				});

				// Token: 0x0400CBCE RID: 52174
				public static LocString UNSTABLEENTOMBDEFENSETHREATENED = string.Concat(new string[]
				{
					"This plant is preparing to shake off ",
					UI.PRE_KEYWORD,
					"Unstable",
					UI.PST_KEYWORD,
					" elements that are entombing it"
				});

				// Token: 0x0400CBCF RID: 52175
				public static LocString UNSTABLEENTOMBDEFENSEREACTING = string.Concat(new string[]
				{
					"This plant is currently unentombing itself from ",
					UI.PRE_KEYWORD,
					"Unstable",
					UI.PST_KEYWORD,
					" elements"
				});

				// Token: 0x0400CBD0 RID: 52176
				public static LocString UNSTABLEENTOMBDEFENSEOFF = string.Concat(new string[]
				{
					"This plant's ability to unentomb itself from ",
					UI.PRE_KEYWORD,
					"Unstable",
					UI.PST_KEYWORD,
					" elements is currently disabled"
				});

				// Token: 0x0400CBD1 RID: 52177
				public static LocString BRANCH_GROWER_PLANT_POTENTIAL_OUTPUT = "{0} to {1}";

				// Token: 0x0400CBD2 RID: 52178
				public static LocString EDIBLE_PLANT_INTERNAL_STORAGE = "{0} of stored {1}";

				// Token: 0x0400CBD3 RID: 52179
				public static LocString SCALE_GROWTH = string.Concat(new string[]
				{
					"This critter can be sheared every <b>{Time}</b> to produce ",
					UI.FormatAsPositiveModifier("{Amount}"),
					" of ",
					UI.PRE_KEYWORD,
					"{Item}",
					UI.PST_KEYWORD
				});

				// Token: 0x0400CBD4 RID: 52180
				public static LocString SCALE_GROWTH_ATMO = string.Concat(new string[]
				{
					"This critter can be sheared every <b>{Time}</b> to produce ",
					UI.FormatAsPositiveRate("{Amount}"),
					" of ",
					UI.PRE_KEYWORD,
					"{Item}",
					UI.PST_KEYWORD,
					"\n\nIt must be kept in ",
					UI.PRE_KEYWORD,
					"{Atmosphere}",
					UI.PST_KEYWORD,
					"-rich environments to regrow sheared ",
					UI.PRE_KEYWORD,
					"{Item}",
					UI.PST_KEYWORD
				});

				// Token: 0x0400CBD5 RID: 52181
				public static LocString SCALE_GROWTH_TEMP = string.Concat(new string[]
				{
					"This critter can be sheared every <b>{Time}</b> to produce ",
					UI.FormatAsPositiveRate("{Amount}"),
					" of ",
					UI.PRE_KEYWORD,
					"{Item}",
					UI.PST_KEYWORD,
					"\n\nIt must eat food between {TempMin} - {TempMax} to regrow sheared ",
					UI.PRE_KEYWORD,
					"{Item}",
					UI.PST_KEYWORD
				});

				// Token: 0x0400CBD6 RID: 52182
				public static LocString SCALE_GROWTH_FED = string.Concat(new string[]
				{
					"This critter can be sheared every <b>{Time}</b> to produce ",
					UI.FormatAsPositiveModifier("{Amount}"),
					" of ",
					UI.PRE_KEYWORD,
					"{Item}",
					UI.PST_KEYWORD,
					"\n\nIt must be well fed to grow shearable ",
					UI.PRE_KEYWORD,
					"{Item}",
					UI.PST_KEYWORD
				});

				// Token: 0x0400CBD7 RID: 52183
				public static LocString MESS_TABLE_SALT = string.Concat(new string[]
				{
					"Duplicants gain ",
					UI.FormatAsPositiveModifier("+{0}"),
					" ",
					UI.PRE_KEYWORD,
					"Morale",
					UI.PST_KEYWORD,
					" when using ",
					UI.PRE_KEYWORD,
					"Table Salt",
					UI.PST_KEYWORD,
					" with their food at a ",
					STRINGS.BUILDINGS.PREFABS.DININGTABLE.NAME
				});

				// Token: 0x0400CBD8 RID: 52184
				public static LocString ACCESS_CONTROL = "Settings to allow or restrict Duplicants from passing through the door.";

				// Token: 0x0400CBD9 RID: 52185
				public static LocString TRANSFORMER_INPUT_WIRE = string.Concat(new string[]
				{
					"Connect a ",
					UI.PRE_KEYWORD,
					"Wire",
					UI.PST_KEYWORD,
					" to the large ",
					UI.PRE_KEYWORD,
					"Input",
					UI.PST_KEYWORD,
					" with any amount of ",
					UI.PRE_KEYWORD,
					"Watts",
					UI.PST_KEYWORD,
					"."
				});

				// Token: 0x0400CBDA RID: 52186
				public static LocString TRANSFORMER_OUTPUT_WIRE = string.Concat(new string[]
				{
					"The ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD,
					" provided by the the small ",
					UI.PRE_KEYWORD,
					"Output",
					UI.PST_KEYWORD,
					" will be limited to {0}."
				});

				// Token: 0x0400CBDB RID: 52187
				public static LocString FABRICATOR_INGREDIENTS = "Ingredients:\n{0}";

				// Token: 0x0400CBDC RID: 52188
				public static LocString ACTIVE_PARTICLE_CONSUMPTION = string.Concat(new string[]
				{
					"This building requires ",
					UI.PRE_KEYWORD,
					"Radbolts",
					UI.PST_KEYWORD,
					" to function, consuming them at a rate of {Rate} while in use"
				});

				// Token: 0x0400CBDD RID: 52189
				public static LocString PARTICLE_PORT_INPUT = "A Radbolt Port on this building allows it to receive " + UI.PRE_KEYWORD + "Radbolts" + UI.PST_KEYWORD;

				// Token: 0x0400CBDE RID: 52190
				public static LocString PARTICLE_PORT_OUTPUT = string.Concat(new string[]
				{
					"This building has a configurable Radbolt Port for ",
					UI.PRE_KEYWORD,
					"Radbolt",
					UI.PST_KEYWORD,
					" emission"
				});

				// Token: 0x0400CBDF RID: 52191
				public static LocString IN_ORBIT_REQUIRED = "This building is only operational while its parent rocket is in flight";

				// Token: 0x0400CBE0 RID: 52192
				public static LocString FOOD_DEHYDRATOR_WATER_OUTPUT = string.Concat(new string[]
				{
					"This building dumps ",
					UI.PRE_KEYWORD,
					"Water",
					UI.PST_KEYWORD,
					" on the floor while in use"
				});

				// Token: 0x0400CBE1 RID: 52193
				public static LocString KETTLE_MELT_RATE = string.Concat(new string[]
				{
					"This building melts {0} of ",
					UI.PRE_KEYWORD,
					"Ice",
					UI.PST_KEYWORD,
					" into {0} of cold ({1}) ",
					UI.PRE_KEYWORD,
					"Water",
					UI.PST_KEYWORD,
					"\n\n",
					UI.PRE_KEYWORD,
					"Wood",
					UI.PST_KEYWORD,
					" consumption varies depending on the initial temperature of the ",
					UI.PRE_KEYWORD,
					"Ice",
					UI.PST_KEYWORD
				});
			}
		}

		// Token: 0x020023ED RID: 9197
		public class LOGIC_PORTS
		{
			// Token: 0x0400A276 RID: 41590
			public static LocString INPUT_PORTS = UI.FormatAsLink("Auto Inputs", "LOGIC");

			// Token: 0x0400A277 RID: 41591
			public static LocString INPUT_PORTS_TOOLTIP = "Input ports change a state on this building when a signal is received";

			// Token: 0x0400A278 RID: 41592
			public static LocString OUTPUT_PORTS = UI.FormatAsLink("Auto Outputs", "LOGIC");

			// Token: 0x0400A279 RID: 41593
			public static LocString OUTPUT_PORTS_TOOLTIP = "Output ports send a signal when this building changes state";

			// Token: 0x0400A27A RID: 41594
			public static LocString INPUT_PORT_TOOLTIP = "Input Behavior:\n• {0}\n• {1}";

			// Token: 0x0400A27B RID: 41595
			public static LocString OUTPUT_PORT_TOOLTIP = "Output Behavior:\n• {0}\n• {1}";

			// Token: 0x0400A27C RID: 41596
			public static LocString CONTROL_OPERATIONAL = "Enable/Disable";

			// Token: 0x0400A27D RID: 41597
			public static LocString CONTROL_OPERATIONAL_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Enable building";

			// Token: 0x0400A27E RID: 41598
			public static LocString CONTROL_OPERATIONAL_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Disable building";

			// Token: 0x0400A27F RID: 41599
			public static LocString PORT_INPUT_DEFAULT_NAME = "INPUT";

			// Token: 0x0400A280 RID: 41600
			public static LocString PORT_OUTPUT_DEFAULT_NAME = "OUTPUT";

			// Token: 0x0400A281 RID: 41601
			public static LocString GATE_MULTI_INPUT_ONE_NAME = "INPUT A";

			// Token: 0x0400A282 RID: 41602
			public static LocString GATE_MULTI_INPUT_ONE_ACTIVE = "Green Signal";

			// Token: 0x0400A283 RID: 41603
			public static LocString GATE_MULTI_INPUT_ONE_INACTIVE = "Red Signal";

			// Token: 0x0400A284 RID: 41604
			public static LocString GATE_MULTI_INPUT_TWO_NAME = "INPUT B";

			// Token: 0x0400A285 RID: 41605
			public static LocString GATE_MULTI_INPUT_TWO_ACTIVE = "Green Signal";

			// Token: 0x0400A286 RID: 41606
			public static LocString GATE_MULTI_INPUT_TWO_INACTIVE = "Red Signal";

			// Token: 0x0400A287 RID: 41607
			public static LocString GATE_MULTI_INPUT_THREE_NAME = "INPUT C";

			// Token: 0x0400A288 RID: 41608
			public static LocString GATE_MULTI_INPUT_THREE_ACTIVE = "Green Signal";

			// Token: 0x0400A289 RID: 41609
			public static LocString GATE_MULTI_INPUT_THREE_INACTIVE = "Red Signal";

			// Token: 0x0400A28A RID: 41610
			public static LocString GATE_MULTI_INPUT_FOUR_NAME = "INPUT D";

			// Token: 0x0400A28B RID: 41611
			public static LocString GATE_MULTI_INPUT_FOUR_ACTIVE = "Green Signal";

			// Token: 0x0400A28C RID: 41612
			public static LocString GATE_MULTI_INPUT_FOUR_INACTIVE = "Red Signal";

			// Token: 0x0400A28D RID: 41613
			public static LocString GATE_SINGLE_INPUT_ONE_NAME = "INPUT";

			// Token: 0x0400A28E RID: 41614
			public static LocString GATE_SINGLE_INPUT_ONE_ACTIVE = "Green Signal";

			// Token: 0x0400A28F RID: 41615
			public static LocString GATE_SINGLE_INPUT_ONE_INACTIVE = "Red Signal";

			// Token: 0x0400A290 RID: 41616
			public static LocString GATE_MULTI_OUTPUT_ONE_NAME = "OUTPUT A";

			// Token: 0x0400A291 RID: 41617
			public static LocString GATE_MULTI_OUTPUT_ONE_ACTIVE = "Green Signal";

			// Token: 0x0400A292 RID: 41618
			public static LocString GATE_MULTI_OUTPUT_ONE_INACTIVE = "Red Signal";

			// Token: 0x0400A293 RID: 41619
			public static LocString GATE_MULTI_OUTPUT_TWO_NAME = "OUTPUT B";

			// Token: 0x0400A294 RID: 41620
			public static LocString GATE_MULTI_OUTPUT_TWO_ACTIVE = "Green Signal";

			// Token: 0x0400A295 RID: 41621
			public static LocString GATE_MULTI_OUTPUT_TWO_INACTIVE = "Red Signal";

			// Token: 0x0400A296 RID: 41622
			public static LocString GATE_MULTI_OUTPUT_THREE_NAME = "OUTPUT C";

			// Token: 0x0400A297 RID: 41623
			public static LocString GATE_MULTI_OUTPUT_THREE_ACTIVE = "Green Signal";

			// Token: 0x0400A298 RID: 41624
			public static LocString GATE_MULTI_OUTPUT_THREE_INACTIVE = "Red Signal";

			// Token: 0x0400A299 RID: 41625
			public static LocString GATE_MULTI_OUTPUT_FOUR_NAME = "OUTPUT D";

			// Token: 0x0400A29A RID: 41626
			public static LocString GATE_MULTI_OUTPUT_FOUR_ACTIVE = "Green Signal";

			// Token: 0x0400A29B RID: 41627
			public static LocString GATE_MULTI_OUTPUT_FOUR_INACTIVE = "Red Signal";

			// Token: 0x0400A29C RID: 41628
			public static LocString GATE_SINGLE_OUTPUT_ONE_NAME = "OUTPUT";

			// Token: 0x0400A29D RID: 41629
			public static LocString GATE_SINGLE_OUTPUT_ONE_ACTIVE = "Green Signal";

			// Token: 0x0400A29E RID: 41630
			public static LocString GATE_SINGLE_OUTPUT_ONE_INACTIVE = "Red Signal";

			// Token: 0x0400A29F RID: 41631
			public static LocString GATE_MULTIPLEXER_CONTROL_ONE_NAME = "CONTROL A";

			// Token: 0x0400A2A0 RID: 41632
			public static LocString GATE_MULTIPLEXER_CONTROL_ONE_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Set signal path to <b>down</b> position";

			// Token: 0x0400A2A1 RID: 41633
			public static LocString GATE_MULTIPLEXER_CONTROL_ONE_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Set signal path to <b>up</b> position";

			// Token: 0x0400A2A2 RID: 41634
			public static LocString GATE_MULTIPLEXER_CONTROL_TWO_NAME = "CONTROL B";

			// Token: 0x0400A2A3 RID: 41635
			public static LocString GATE_MULTIPLEXER_CONTROL_TWO_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Set signal path to <b>down</b> position";

			// Token: 0x0400A2A4 RID: 41636
			public static LocString GATE_MULTIPLEXER_CONTROL_TWO_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Set signal path to <b>up</b> position";
		}

		// Token: 0x020023EE RID: 9198
		public class GAMEOBJECTEFFECTS
		{
			// Token: 0x0400A2A5 RID: 41637
			public static LocString CALORIES = "+{0}";

			// Token: 0x0400A2A6 RID: 41638
			public static LocString FOOD_QUALITY = "Quality: {0}";

			// Token: 0x0400A2A7 RID: 41639
			public static LocString FOOD_MORALE = "Morale: {0}";

			// Token: 0x0400A2A8 RID: 41640
			public static LocString FORGAVEATTACKER = "Forgiveness";

			// Token: 0x0400A2A9 RID: 41641
			public static LocString COLDBREATHER = UI.FormatAsLink("Cooling Effect", "HEAT");

			// Token: 0x0400A2AA RID: 41642
			public static LocString LIFECYCLETITLE = "Growth:";

			// Token: 0x0400A2AB RID: 41643
			public static LocString GROWTHTIME_SIMPLE = "Life Cycle: {0}";

			// Token: 0x0400A2AC RID: 41644
			public static LocString GROWTHTIME_REGROWTH = "Domestic growth: {0} / {1}";

			// Token: 0x0400A2AD RID: 41645
			public static LocString GROWTHTIME = "Growth: {0}";

			// Token: 0x0400A2AE RID: 41646
			public static LocString INITIALGROWTHTIME = "Initial Growth: {0}";

			// Token: 0x0400A2AF RID: 41647
			public static LocString REGROWTHTIME = "Regrowth: {0}";

			// Token: 0x0400A2B0 RID: 41648
			public static LocString REQUIRES_LIGHT = UI.FormatAsLink("Light", "LIGHT") + ": {Lux}";

			// Token: 0x0400A2B1 RID: 41649
			public static LocString REQUIRES_DARKNESS = UI.FormatAsLink("Darkness", "LIGHT");

			// Token: 0x0400A2B2 RID: 41650
			public static LocString REQUIRESFERTILIZER = "{0}: {1}";

			// Token: 0x0400A2B3 RID: 41651
			public static LocString IDEAL_FERTILIZER = "{0}: {1}";

			// Token: 0x0400A2B4 RID: 41652
			public static LocString EQUIPMENT_MODS = "{Attribute} {Value}";

			// Token: 0x0400A2B5 RID: 41653
			public static LocString ROTTEN = "Rotten";

			// Token: 0x0400A2B6 RID: 41654
			public static LocString REQUIRES_ATMOSPHERE = UI.FormatAsLink("Atmosphere", "ATMOSPHERE") + ": {0}";

			// Token: 0x0400A2B7 RID: 41655
			public static LocString REQUIRES_PRESSURE = UI.FormatAsLink("Air", "ATMOSPHERE") + " Pressure: {0} minimum";

			// Token: 0x0400A2B8 RID: 41656
			public static LocString IDEAL_PRESSURE = UI.FormatAsLink("Air", "ATMOSPHERE") + " Pressure: {0}";

			// Token: 0x0400A2B9 RID: 41657
			public static LocString REQUIRES_TEMPERATURE = UI.FormatAsLink("Temperature", "HEAT") + ": {0} to {1}";

			// Token: 0x0400A2BA RID: 41658
			public static LocString IDEAL_TEMPERATURE = UI.FormatAsLink("Temperature", "HEAT") + ": {0} to {1}";

			// Token: 0x0400A2BB RID: 41659
			public static LocString REQUIRES_SUBMERSION = UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " Submersion";

			// Token: 0x0400A2BC RID: 41660
			public static LocString FOOD_EFFECTS = "Effects:";

			// Token: 0x0400A2BD RID: 41661
			public static LocString EMITS_LIGHT = UI.FormatAsLink("Light Range", "LIGHT") + ": {0} tiles";

			// Token: 0x0400A2BE RID: 41662
			public static LocString EMITS_LIGHT_LUX = UI.FormatAsLink("Brightness", "LIGHT") + ": {0} Lux";

			// Token: 0x0400A2BF RID: 41663
			public static LocString AMBIENT_RADIATION = "Ambient Radiation";

			// Token: 0x0400A2C0 RID: 41664
			public static LocString AMBIENT_RADIATION_FMT = "{minRads} - {maxRads}";

			// Token: 0x0400A2C1 RID: 41665
			public static LocString AMBIENT_NO_MIN_RADIATION_FMT = "Less than {maxRads}";

			// Token: 0x0400A2C2 RID: 41666
			public static LocString REQUIRES_NO_MIN_RADIATION = "Maximum " + UI.FormatAsLink("Radiation", "RADIATION") + ": {MaxRads}";

			// Token: 0x0400A2C3 RID: 41667
			public static LocString REQUIRES_RADIATION = UI.FormatAsLink("Radiation", "RADIATION") + ": {MinRads} to {MaxRads}";

			// Token: 0x0400A2C4 RID: 41668
			public static LocString MUTANT_STERILE = "Doesn't Drop " + UI.FormatAsLink("Seeds", "PLANTS");

			// Token: 0x0400A2C5 RID: 41669
			public static LocString DARKNESS = "Darkness";

			// Token: 0x0400A2C6 RID: 41670
			public static LocString LIGHT = "Light";

			// Token: 0x0400A2C7 RID: 41671
			public static LocString SEED_PRODUCTION_DIG_ONLY = "Consumes 1 " + UI.FormatAsLink("Seed", "PLANTS");

			// Token: 0x0400A2C8 RID: 41672
			public static LocString SEED_PRODUCTION_HARVEST = "Harvest yields " + UI.FormatAsLink("Seeds", "PLANTS");

			// Token: 0x0400A2C9 RID: 41673
			public static LocString SEED_PRODUCTION_FINAL_HARVEST = "Final harvest yields " + UI.FormatAsLink("Seeds", "PLANTS");

			// Token: 0x0400A2CA RID: 41674
			public static LocString SEED_PRODUCTION_FRUIT = "Fruit produces " + UI.FormatAsLink("Seeds", "PLANTS");

			// Token: 0x0400A2CB RID: 41675
			public static LocString SEED_REQUIREMENT_CEILING = "Plot Orientation: Downward";

			// Token: 0x0400A2CC RID: 41676
			public static LocString SEED_REQUIREMENT_WALL = "Plot Orientation: Sideways";

			// Token: 0x0400A2CD RID: 41677
			public static LocString REQUIRES_RECEPTACLE = "Farm Plot";

			// Token: 0x0400A2CE RID: 41678
			public static LocString PLANT_MARK_FOR_HARVEST = "Autoharvest Enabled";

			// Token: 0x0400A2CF RID: 41679
			public static LocString PLANT_DO_NOT_HARVEST = "Autoharvest Disabled";

			// Token: 0x0400A2D0 RID: 41680
			public static LocString REQUIRES_POLLINATION = "Pollination";

			// Token: 0x02002EEF RID: 12015
			public class INSULATED
			{
				// Token: 0x0400CBE2 RID: 52194
				public static LocString NAME = "Insulated";

				// Token: 0x0400CBE3 RID: 52195
				public static LocString TOOLTIP = "Proper insulation drastically reduces thermal conductivity";
			}

			// Token: 0x02002EF0 RID: 12016
			public class TOOLTIPS
			{
				// Token: 0x0400CBE4 RID: 52196
				public static LocString CALORIES = "+{0}";

				// Token: 0x0400CBE5 RID: 52197
				public static LocString FOOD_QUALITY = "Quality: {0}";

				// Token: 0x0400CBE6 RID: 52198
				public static LocString FOOD_MORALE = "Morale: {0}";

				// Token: 0x0400CBE7 RID: 52199
				public static LocString COLDBREATHER = "Lowers ambient air temperature";

				// Token: 0x0400CBE8 RID: 52200
				public static LocString GROWTHTIME_SIMPLE = "This plant takes <b>{0}</b> to grow";

				// Token: 0x0400CBE9 RID: 52201
				public static LocString GROWTHTIME_REGROWTH = "This plant initially takes <b>{0}</b> to grow, but only <b>{1}</b> to mature after first harvest";

				// Token: 0x0400CBEA RID: 52202
				public static LocString GROWTHTIME = "This plant takes <b>{0}</b> to grow";

				// Token: 0x0400CBEB RID: 52203
				public static LocString INITIALGROWTHTIME = "This plant takes <b>{0}</b> to mature again once replanted";

				// Token: 0x0400CBEC RID: 52204
				public static LocString REGROWTHTIME = "This plant takes <b>{0}</b> to mature again once harvested";

				// Token: 0x0400CBED RID: 52205
				public static LocString EQUIPMENT_MODS = "{Attribute} {Value}";

				// Token: 0x0400CBEE RID: 52206
				public static LocString REQUIRESFERTILIZER = string.Concat(new string[]
				{
					"This plant requires <b>{1}</b> ",
					UI.PRE_KEYWORD,
					"{0}",
					UI.PST_KEYWORD,
					" for basic growth"
				});

				// Token: 0x0400CBEF RID: 52207
				public static LocString IDEAL_FERTILIZER = string.Concat(new string[]
				{
					"This plant requires <b>{1}</b> of ",
					UI.PRE_KEYWORD,
					"{0}",
					UI.PST_KEYWORD,
					" for basic growth"
				});

				// Token: 0x0400CBF0 RID: 52208
				public static LocString REQUIRES_LIGHT = string.Concat(new string[]
				{
					"This plant requires a ",
					UI.PRE_KEYWORD,
					"Light",
					UI.PST_KEYWORD,
					" source bathing it in at least {Lux}"
				});

				// Token: 0x0400CBF1 RID: 52209
				public static LocString REQUIRES_DARKNESS = "This plant requires complete darkness";

				// Token: 0x0400CBF2 RID: 52210
				public static LocString REQUIRES_ATMOSPHERE = "This plant must be submerged in one of the following gases: {0}";

				// Token: 0x0400CBF3 RID: 52211
				public static LocString REQUIRES_ATMOSPHERE_LIQUID = "This plant must be submerged in one of the following liquids: {0}";

				// Token: 0x0400CBF4 RID: 52212
				public static LocString REQUIRES_ATMOSPHERE_MIXED = "This plant must be submerged in one of the following gases or liquids: {0}";

				// Token: 0x0400CBF5 RID: 52213
				public static LocString REQUIRES_PRESSURE = string.Concat(new string[]
				{
					"Ambient ",
					UI.PRE_KEYWORD,
					"Gas",
					UI.PST_KEYWORD,
					" pressure must be at least <b>{0}</b> for basic growth"
				});

				// Token: 0x0400CBF6 RID: 52214
				public static LocString IDEAL_PRESSURE = string.Concat(new string[]
				{
					"This plant requires ",
					UI.PRE_KEYWORD,
					"Gas",
					UI.PST_KEYWORD,
					" pressures above <b>{0}</b> for basic growth"
				});

				// Token: 0x0400CBF7 RID: 52215
				public static LocString REQUIRES_TEMPERATURE = string.Concat(new string[]
				{
					"Internal ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" must be between <b>{0}</b> and <b>{1}</b> for basic growth"
				});

				// Token: 0x0400CBF8 RID: 52216
				public static LocString IDEAL_TEMPERATURE = string.Concat(new string[]
				{
					"This plant requires internal ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" between <b>{0}</b> and <b>{1}</b> for basic growth"
				});

				// Token: 0x0400CBF9 RID: 52217
				public static LocString REQUIRES_SUBMERSION = string.Concat(new string[]
				{
					"This plant must be fully submerged in ",
					UI.PRE_KEYWORD,
					"Liquid",
					UI.PST_KEYWORD,
					" for basic growth"
				});

				// Token: 0x0400CBFA RID: 52218
				public static LocString FOOD_EFFECTS = "Duplicants will gain the following effects from eating this food: {0}";

				// Token: 0x0400CBFB RID: 52219
				public static LocString REQUIRES_RECEPTACLE = string.Concat(new string[]
				{
					"This plant must be housed in a ",
					UI.FormatAsLink("Planter Box", "PLANTERBOX"),
					", ",
					UI.FormatAsLink("Farm Tile", "FARMTILE"),
					", or ",
					UI.FormatAsLink("Hydroponic Farm", "HYDROPONICFARM"),
					" farm to grow domestically"
				});

				// Token: 0x0400CBFC RID: 52220
				public static LocString EMITS_LIGHT = string.Concat(new string[]
				{
					"Emits ",
					UI.PRE_KEYWORD,
					"Light",
					UI.PST_KEYWORD,
					"\n\nDuplicants can operate buildings more quickly when they're well lit"
				});

				// Token: 0x0400CBFD RID: 52221
				public static LocString EMITS_LIGHT_LUX = string.Concat(new string[]
				{
					"Emits ",
					UI.PRE_KEYWORD,
					"Light",
					UI.PST_KEYWORD,
					"\n\nDuplicants can operate buildings more quickly when they're well lit"
				});

				// Token: 0x0400CBFE RID: 52222
				public static LocString METEOR_SHOWER_SINGLE_METEOR_PERCENTAGE_TOOLTIP = "Distribution of meteor types in this shower";

				// Token: 0x0400CBFF RID: 52223
				public static LocString SEED_PRODUCTION_DIG_ONLY = "May be replanted, but will produce no further " + UI.PRE_KEYWORD + "Seeds" + UI.PST_KEYWORD;

				// Token: 0x0400CC00 RID: 52224
				public static LocString SEED_PRODUCTION_HARVEST = "Harvesting this plant will yield new " + UI.PRE_KEYWORD + "Seeds" + UI.PST_KEYWORD;

				// Token: 0x0400CC01 RID: 52225
				public static LocString SEED_PRODUCTION_FINAL_HARVEST = string.Concat(new string[]
				{
					"Yields new ",
					UI.PRE_KEYWORD,
					"Seeds",
					UI.PST_KEYWORD,
					" on the final harvest of its life cycle"
				});

				// Token: 0x0400CC02 RID: 52226
				public static LocString SEED_PRODUCTION_FRUIT = "Consuming this plant's fruit will yield new " + UI.PRE_KEYWORD + "Seeds" + UI.PST_KEYWORD;

				// Token: 0x0400CC03 RID: 52227
				public static LocString SEED_REQUIREMENT_CEILING = "This seed must be planted in a downward facing plot\n\nPress " + UI.FormatAsKeyWord("[O]") + " while building farm plots to rotate them";

				// Token: 0x0400CC04 RID: 52228
				public static LocString SEED_REQUIREMENT_WALL = "This seed must be planted in a side facing plot\n\nPress " + UI.FormatAsKeyWord("[O]") + " while building farm plots to rotate them";

				// Token: 0x0400CC05 RID: 52229
				public static LocString REQUIRES_NO_MIN_RADIATION = "This plant will stop growing if exposed to more than {MaxRads} of " + UI.FormatAsLink("Radiation", "RADIATION");

				// Token: 0x0400CC06 RID: 52230
				public static LocString REQUIRES_RADIATION = "This plant will only grow if it has between {MinRads} and {MaxRads} of " + UI.FormatAsLink("Radiation", "RADIATION");

				// Token: 0x0400CC07 RID: 52231
				public static LocString MUTANT_SEED_TOOLTIP = "\n\nGrowing near its maximum radiation increases the chance of mutant seeds being produced";

				// Token: 0x0400CC08 RID: 52232
				public static LocString MUTANT_STERILE = "This plant will not produce seeds of its own due to changes to its DNA";

				// Token: 0x0400CC09 RID: 52233
				public static LocString REQUIRES_POLLINATION = string.Concat(new string[]
				{
					"This plant must be tended by a ",
					UI.PRE_KEYWORD,
					"Critter",
					UI.PST_KEYWORD,
					" to grow"
				});
			}

			// Token: 0x02002EF1 RID: 12017
			public class DAMAGE_POPS
			{
				// Token: 0x0400CC0A RID: 52234
				public static LocString OVERHEAT = "Overheat Damage";

				// Token: 0x0400CC0B RID: 52235
				public static LocString CORROSIVE_ELEMENT = "Corrosive Element Damage";

				// Token: 0x0400CC0C RID: 52236
				public static LocString WRONG_ELEMENT = "Wrong Element Damage";

				// Token: 0x0400CC0D RID: 52237
				public static LocString CIRCUIT_OVERLOADED = "Overload Damage";

				// Token: 0x0400CC0E RID: 52238
				public static LocString LOGIC_CIRCUIT_OVERLOADED = "Signal Overload Damage";

				// Token: 0x0400CC0F RID: 52239
				public static LocString LIQUID_PRESSURE = "Pressure Damage";

				// Token: 0x0400CC10 RID: 52240
				public static LocString MINION_DESTRUCTION = "Tantrum Damage";

				// Token: 0x0400CC11 RID: 52241
				public static LocString CONDUIT_CONTENTS_FROZE = "Cold Damage";

				// Token: 0x0400CC12 RID: 52242
				public static LocString CONDUIT_CONTENTS_BOILED = "Heat Damage";

				// Token: 0x0400CC13 RID: 52243
				public static LocString MICROMETEORITE = "Micrometeorite Damage";

				// Token: 0x0400CC14 RID: 52244
				public static LocString COMET = "Meteor Damage";

				// Token: 0x0400CC15 RID: 52245
				public static LocString ROCKET = "Rocket Thruster Damage";

				// Token: 0x0400CC16 RID: 52246
				public static LocString POWER_BANK_WATER_DAMAGE = "Water Damage";
			}
		}

		// Token: 0x020023EF RID: 9199
		public class ASTEROIDCLOCK
		{
			// Token: 0x0400A2D1 RID: 41681
			public static LocString CYCLE = "Cycle";

			// Token: 0x0400A2D2 RID: 41682
			public static LocString CYCLES_OLD = "This Colony is {0} Cycle(s) Old";

			// Token: 0x0400A2D3 RID: 41683
			public static LocString TIME_PLAYED = "Time Played: {0} hours";

			// Token: 0x0400A2D4 RID: 41684
			public static LocString SCHEDULE_BUTTON_TOOLTIP = "Manage Schedule";

			// Token: 0x0400A2D5 RID: 41685
			public static LocString MILESTONE_TITLE = "Approaching Milestone";

			// Token: 0x0400A2D6 RID: 41686
			public static LocString MILESTONE_DESCRIPTION = "This colony is about to hit Cycle {0}!";
		}

		// Token: 0x020023F0 RID: 9200
		public class ENDOFDAYREPORT
		{
			// Token: 0x0400A2D7 RID: 41687
			public static LocString REPORT_TITLE = "DAILY REPORTS";

			// Token: 0x0400A2D8 RID: 41688
			public static LocString DAY_TITLE = "Cycle {0}";

			// Token: 0x0400A2D9 RID: 41689
			public static LocString DAY_TITLE_TODAY = "Cycle {0} - Today";

			// Token: 0x0400A2DA RID: 41690
			public static LocString DAY_TITLE_YESTERDAY = "Cycle {0} - Yesterday";

			// Token: 0x0400A2DB RID: 41691
			public static LocString NOTIFICATION_TITLE = "Cycle {0} report ready";

			// Token: 0x0400A2DC RID: 41692
			public static LocString NOTIFICATION_TOOLTIP = "The daily report for Cycle {0} is ready to view";

			// Token: 0x0400A2DD RID: 41693
			public static LocString NEXT = "Next";

			// Token: 0x0400A2DE RID: 41694
			public static LocString PREV = "Prev";

			// Token: 0x0400A2DF RID: 41695
			public static LocString ADDED = "Added";

			// Token: 0x0400A2E0 RID: 41696
			public static LocString REMOVED = "Removed";

			// Token: 0x0400A2E1 RID: 41697
			public static LocString NET = "Net";

			// Token: 0x0400A2E2 RID: 41698
			public static LocString DUPLICANT_DETAILS_HEADER = "Duplicant Details:";

			// Token: 0x0400A2E3 RID: 41699
			public static LocString TIME_DETAILS_HEADER = "Total Time Details:";

			// Token: 0x0400A2E4 RID: 41700
			public static LocString BASE_DETAILS_HEADER = "Base Details:";

			// Token: 0x0400A2E5 RID: 41701
			public static LocString AVERAGE_TIME_DETAILS_HEADER = "Average Time Details:";

			// Token: 0x0400A2E6 RID: 41702
			public static LocString MY_COLONY = "my colony";

			// Token: 0x0400A2E7 RID: 41703
			public static LocString NONE = "None";

			// Token: 0x02002EF2 RID: 12018
			public class OXYGEN_CREATED
			{
				// Token: 0x0400CC17 RID: 52247
				public static LocString NAME = UI.FormatAsLink("Oxygen", "OXYGEN") + " Generation:";

				// Token: 0x0400CC18 RID: 52248
				public static LocString POSITIVE_TOOLTIP = "{0} of " + UI.FormatAsLink("Oxygen", "OXYGEN") + " was produced by {1} over the course of the day";

				// Token: 0x0400CC19 RID: 52249
				public static LocString NEGATIVE_TOOLTIP = "{0} of " + UI.FormatAsLink("Oxygen", "OXYGEN") + " was consumed by {1} over the course of the day";
			}

			// Token: 0x02002EF3 RID: 12019
			public class CALORIES_CREATED
			{
				// Token: 0x0400CC1A RID: 52250
				public static LocString NAME = "Calorie Generation:";

				// Token: 0x0400CC1B RID: 52251
				public static LocString POSITIVE_TOOLTIP = "{0} of " + UI.FormatAsLink("Food", "FOOD") + " was produced by {1} over the course of the day";

				// Token: 0x0400CC1C RID: 52252
				public static LocString NEGATIVE_TOOLTIP = "{0} of " + UI.FormatAsLink("Food", "FOOD") + " was consumed by {1} over the course of the day";
			}

			// Token: 0x02002EF4 RID: 12020
			public class NUMBER_OF_DOMESTICATED_CRITTERS
			{
				// Token: 0x0400CC1D RID: 52253
				public static LocString NAME = "Domesticated Critters:";

				// Token: 0x0400CC1E RID: 52254
				public static LocString POSITIVE_TOOLTIP = "{0} domestic critters live in {1}";

				// Token: 0x0400CC1F RID: 52255
				public static LocString NEGATIVE_TOOLTIP = "{0} domestic critters live in {1}";
			}

			// Token: 0x02002EF5 RID: 12021
			public class NUMBER_OF_WILD_CRITTERS
			{
				// Token: 0x0400CC20 RID: 52256
				public static LocString NAME = "Wild Critters:";

				// Token: 0x0400CC21 RID: 52257
				public static LocString POSITIVE_TOOLTIP = "{0} wild critters live in {1}";

				// Token: 0x0400CC22 RID: 52258
				public static LocString NEGATIVE_TOOLTIP = "{0} wild critters live in {1}";
			}

			// Token: 0x02002EF6 RID: 12022
			public class ROCKETS_IN_FLIGHT
			{
				// Token: 0x0400CC23 RID: 52259
				public static LocString NAME = "Rocket Missions Underway:";

				// Token: 0x0400CC24 RID: 52260
				public static LocString POSITIVE_TOOLTIP = "{0} rockets are currently flying missions for {1}";

				// Token: 0x0400CC25 RID: 52261
				public static LocString NEGATIVE_TOOLTIP = "{0} rockets are currently flying missions for {1}";
			}

			// Token: 0x02002EF7 RID: 12023
			public class STRESS_DELTA
			{
				// Token: 0x0400CC26 RID: 52262
				public static LocString NAME = UI.FormatAsLink("Stress", "STRESS") + " Change:";

				// Token: 0x0400CC27 RID: 52263
				public static LocString POSITIVE_TOOLTIP = UI.FormatAsLink("Stress", "STRESS") + " increased by a total of {0} for {1}";

				// Token: 0x0400CC28 RID: 52264
				public static LocString NEGATIVE_TOOLTIP = UI.FormatAsLink("Stress", "STRESS") + " decreased by a total of {0} for {1}";
			}

			// Token: 0x02002EF8 RID: 12024
			public class TRAVELTIMEWARNING
			{
				// Token: 0x0400CC29 RID: 52265
				public static LocString WARNING_TITLE = "Long Commutes";

				// Token: 0x0400CC2A RID: 52266
				public static LocString WARNING_MESSAGE = "My Duplicants are spending a significant amount of time traveling between their errands (> {0})";
			}

			// Token: 0x02002EF9 RID: 12025
			public class TRAVEL_TIME
			{
				// Token: 0x0400CC2B RID: 52267
				public static LocString NAME = "Travel Time:";

				// Token: 0x0400CC2C RID: 52268
				public static LocString POSITIVE_TOOLTIP = "On average, {1} spent {0} of their time traveling between tasks";
			}

			// Token: 0x02002EFA RID: 12026
			public class WORK_TIME
			{
				// Token: 0x0400CC2D RID: 52269
				public static LocString NAME = "Work Time:";

				// Token: 0x0400CC2E RID: 52270
				public static LocString POSITIVE_TOOLTIP = "On average, {0} of {1}'s time was spent working";
			}

			// Token: 0x02002EFB RID: 12027
			public class IDLE_TIME
			{
				// Token: 0x0400CC2F RID: 52271
				public static LocString NAME = "Idle Time:";

				// Token: 0x0400CC30 RID: 52272
				public static LocString POSITIVE_TOOLTIP = "On average, {0} of {1}'s time was spent idling";
			}

			// Token: 0x02002EFC RID: 12028
			public class PERSONAL_TIME
			{
				// Token: 0x0400CC31 RID: 52273
				public static LocString NAME = "Personal Time:";

				// Token: 0x0400CC32 RID: 52274
				public static LocString POSITIVE_TOOLTIP = "On average, {0} of {1}'s time was spent tending to personal needs";
			}

			// Token: 0x02002EFD RID: 12029
			public class ENERGY_USAGE
			{
				// Token: 0x0400CC33 RID: 52275
				public static LocString NAME = UI.FormatAsLink("Power", "POWER") + " Usage:";

				// Token: 0x0400CC34 RID: 52276
				public static LocString POSITIVE_TOOLTIP = "{0} of " + UI.FormatAsLink("Power", "POWER") + " was created by {1} over the course of the day";

				// Token: 0x0400CC35 RID: 52277
				public static LocString NEGATIVE_TOOLTIP = "{0} of " + UI.FormatAsLink("Power", "POWER") + " was consumed by {1} over the course of the day";
			}

			// Token: 0x02002EFE RID: 12030
			public class ENERGY_WASTED
			{
				// Token: 0x0400CC36 RID: 52278
				public static LocString NAME = UI.FormatAsLink("Power", "POWER") + " Wasted:";

				// Token: 0x0400CC37 RID: 52279
				public static LocString NEGATIVE_TOOLTIP = "{0} of " + UI.FormatAsLink("Power", "POWER") + " was lost today due to battery runoff and overproduction in {1}";
			}

			// Token: 0x02002EFF RID: 12031
			public class LEVEL_UP
			{
				// Token: 0x0400CC38 RID: 52280
				public static LocString NAME = "Skill Increases:";

				// Token: 0x0400CC39 RID: 52281
				public static LocString TOOLTIP = "Today {1} gained a total of {0} skill levels";
			}

			// Token: 0x02002F00 RID: 12032
			public class TOILET_INCIDENT
			{
				// Token: 0x0400CC3A RID: 52282
				public static LocString NAME = "Restroom Accidents:";

				// Token: 0x0400CC3B RID: 52283
				public static LocString TOOLTIP = "{0} Duplicants couldn't quite reach the toilet in time today";
			}

			// Token: 0x02002F01 RID: 12033
			public class DISEASE_ADDED
			{
				// Token: 0x0400CC3C RID: 52284
				public static LocString NAME = UI.FormatAsLink("Diseases", "DISEASE") + " Contracted:";

				// Token: 0x0400CC3D RID: 52285
				public static LocString POSITIVE_TOOLTIP = "{0} " + UI.FormatAsLink("Disease", "DISEASE") + " were contracted by {1}";

				// Token: 0x0400CC3E RID: 52286
				public static LocString NEGATIVE_TOOLTIP = "{0} " + UI.FormatAsLink("Disease", "DISEASE") + " were cured by {1}";
			}

			// Token: 0x02002F02 RID: 12034
			public class CONTAMINATED_OXYGEN_FLATULENCE
			{
				// Token: 0x0400CC3F RID: 52287
				public static LocString NAME = UI.FormatAsLink("Flatulence", "CONTAMINATEDOXYGEN") + " Generation:";

				// Token: 0x0400CC40 RID: 52288
				public static LocString POSITIVE_TOOLTIP = "{0} of " + UI.FormatAsLink("Polluted Oxygen", "CONTAMINATEDOXYGEN") + " was generated by {1} over the course of the day";

				// Token: 0x0400CC41 RID: 52289
				public static LocString NEGATIVE_TOOLTIP = "{0} of " + UI.FormatAsLink("Polluted Oxygen", "CONTAMINATEDOXYGEN") + " was consumed by {1} over the course of the day";
			}

			// Token: 0x02002F03 RID: 12035
			public class CONTAMINATED_OXYGEN_TOILET
			{
				// Token: 0x0400CC42 RID: 52290
				public static LocString NAME = UI.FormatAsLink("Toilet Emissions: ", "CONTAMINATEDOXYGEN");

				// Token: 0x0400CC43 RID: 52291
				public static LocString POSITIVE_TOOLTIP = "{0} of " + UI.FormatAsLink("Polluted Oxygen", "CONTAMINATEDOXYGEN") + " was generated by {1} over the course of the day";

				// Token: 0x0400CC44 RID: 52292
				public static LocString NEGATIVE_TOOLTIP = "{0} of " + UI.FormatAsLink("Polluted Oxygen", "CONTAMINATEDOXYGEN") + " was consumed by {1} over the course of the day";
			}

			// Token: 0x02002F04 RID: 12036
			public class CONTAMINATED_OXYGEN_SUBLIMATION
			{
				// Token: 0x0400CC45 RID: 52293
				public static LocString NAME = UI.FormatAsLink("Sublimation", "CONTAMINATEDOXYGEN") + ":";

				// Token: 0x0400CC46 RID: 52294
				public static LocString POSITIVE_TOOLTIP = "{0} of " + UI.FormatAsLink("Polluted Oxygen", "CONTAMINATEDOXYGEN") + " was generated by {1} over the course of the day";

				// Token: 0x0400CC47 RID: 52295
				public static LocString NEGATIVE_TOOLTIP = "{0} of " + UI.FormatAsLink("Polluted Oxygen", "CONTAMINATEDOXYGEN") + " was consumed by {1} over the course of the day";
			}

			// Token: 0x02002F05 RID: 12037
			public class DISEASE_STATUS
			{
				// Token: 0x0400CC48 RID: 52296
				public static LocString NAME = "Disease Status:";

				// Token: 0x0400CC49 RID: 52297
				public static LocString TOOLTIP = "There are {0} covering {1}";
			}

			// Token: 0x02002F06 RID: 12038
			public class CHORE_STATUS
			{
				// Token: 0x0400CC4A RID: 52298
				public static LocString NAME = "Errands:";

				// Token: 0x0400CC4B RID: 52299
				public static LocString POSITIVE_TOOLTIP = "{0} errands are queued for {1}";

				// Token: 0x0400CC4C RID: 52300
				public static LocString NEGATIVE_TOOLTIP = "{0} errands were completed over the course of the day by {1}";
			}

			// Token: 0x02002F07 RID: 12039
			public class NOTES
			{
				// Token: 0x0400CC4D RID: 52301
				public static LocString NOTE_ENTRY_LINE_ITEM = "{0}\n{1}: {2}";

				// Token: 0x0400CC4E RID: 52302
				public static LocString BUTCHERED = "Butchered for {0}";

				// Token: 0x0400CC4F RID: 52303
				public static LocString BUTCHERED_CONTEXT = "Butchered";

				// Token: 0x0400CC50 RID: 52304
				public static LocString CRAFTED = "Crafted a {0}";

				// Token: 0x0400CC51 RID: 52305
				public static LocString CRAFTED_USED = "{0} used as ingredient";

				// Token: 0x0400CC52 RID: 52306
				public static LocString CRAFTED_CONTEXT = "Crafted";

				// Token: 0x0400CC53 RID: 52307
				public static LocString HARVESTED = "Harvested {0}";

				// Token: 0x0400CC54 RID: 52308
				public static LocString HARVESTED_CONTEXT = "Harvested";

				// Token: 0x0400CC55 RID: 52309
				public static LocString EATEN = "{0} eaten";

				// Token: 0x0400CC56 RID: 52310
				public static LocString ROTTED = "Rotten {0}";

				// Token: 0x0400CC57 RID: 52311
				public static LocString ROTTED_CONTEXT = "Rotted";

				// Token: 0x0400CC58 RID: 52312
				public static LocString GERMS = "On {0}";

				// Token: 0x0400CC59 RID: 52313
				public static LocString TIME_SPENT = "{0}";

				// Token: 0x0400CC5A RID: 52314
				public static LocString WORK_TIME = "{0}";

				// Token: 0x0400CC5B RID: 52315
				public static LocString PERSONAL_TIME = "{0}";

				// Token: 0x0400CC5C RID: 52316
				public static LocString FOODFIGHT_CONTEXT = "{0} ingested in food fight";
			}
		}

		// Token: 0x020023F1 RID: 9201
		public static class SCHEDULEBLOCKTYPES
		{
			// Token: 0x02002F08 RID: 12040
			public static class EAT
			{
				// Token: 0x0400CC5D RID: 52317
				public static LocString NAME = "Mealtime";

				// Token: 0x0400CC5E RID: 52318
				public static LocString DESCRIPTION = "EAT:\nDuring Mealtime Duplicants will head to their assigned mess halls and eat.";
			}

			// Token: 0x02002F09 RID: 12041
			public static class SLEEP
			{
				// Token: 0x0400CC5F RID: 52319
				public static LocString NAME = "Sleep";

				// Token: 0x0400CC60 RID: 52320
				public static LocString DESCRIPTION = "SLEEP:\nWhen it's time to sleep, Duplicants will head to their assigned rooms and rest.";
			}

			// Token: 0x02002F0A RID: 12042
			public static class WORK
			{
				// Token: 0x0400CC61 RID: 52321
				public static LocString NAME = "Work";

				// Token: 0x0400CC62 RID: 52322
				public static LocString DESCRIPTION = "WORK:\nDuring Work hours Duplicants will perform any pending errands in the colony.";
			}

			// Token: 0x02002F0B RID: 12043
			public static class RECREATION
			{
				// Token: 0x0400CC63 RID: 52323
				public static LocString NAME = "Recreation";

				// Token: 0x0400CC64 RID: 52324
				public static LocString DESCRIPTION = "HAMMER TIME:\nDuring Hammer Time, Duplicants will relieve their " + UI.FormatAsLink("Stress", "STRESS") + " through dance. Please be aware that no matter how hard my Duplicants try, they will absolutely not be able to touch this.";
			}

			// Token: 0x02002F0C RID: 12044
			public static class HYGIENE
			{
				// Token: 0x0400CC65 RID: 52325
				public static LocString NAME = "Hygiene";

				// Token: 0x0400CC66 RID: 52326
				public static LocString DESCRIPTION = "HYGIENE:\nDuring " + UI.FormatAsLink("Hygiene", "HYGIENE") + " hours Duplicants will head to their assigned washrooms to get cleaned up.";
			}
		}

		// Token: 0x020023F2 RID: 9202
		public static class SCHEDULEGROUPS
		{
			// Token: 0x0400A2E8 RID: 41704
			public static LocString TOOLTIP_FORMAT = "{0}\n\n{1}";

			// Token: 0x0400A2E9 RID: 41705
			public static LocString MISSINGBLOCKS = "Warning: Scheduling Issues ({0})";

			// Token: 0x0400A2EA RID: 41706
			public static LocString NOTIME = "No {0} shifts allotted";

			// Token: 0x02002F0D RID: 12045
			public static class HYGENE
			{
				// Token: 0x0400CC67 RID: 52327
				public static LocString NAME = "Bathtime";

				// Token: 0x0400CC68 RID: 52328
				public static LocString DESCRIPTION = "During Bathtime shifts my Duplicants will take care of their hygienic needs, such as going to the bathroom, using the shower or washing their hands.\n\nOnce they're all caught up on personal hygiene, Duplicants will head back to work.";

				// Token: 0x0400CC69 RID: 52329
				public static LocString NOTIFICATION_TOOLTIP = string.Concat(new string[]
				{
					"During ",
					UI.PRE_KEYWORD,
					"Bathtime",
					UI.PST_KEYWORD,
					" shifts my Duplicants will take care of their hygienic needs, such as going to the bathroom, using the shower or washing their hands."
				});
			}

			// Token: 0x02002F0E RID: 12046
			public static class WORKTIME
			{
				// Token: 0x0400CC6A RID: 52330
				public static LocString NAME = "Work";

				// Token: 0x0400CC6B RID: 52331
				public static LocString DESCRIPTION = "During Work shifts my Duplicants must perform the errands I have placed for them throughout the colony.\n\nIt's important when scheduling to maintain a good work-life balance for my Duplicants to maintain their health and prevent Morale loss.";

				// Token: 0x0400CC6C RID: 52332
				public static LocString NOTIFICATION_TOOLTIP = string.Concat(new string[]
				{
					"During ",
					UI.PRE_KEYWORD,
					"Work",
					UI.PST_KEYWORD,
					" shifts my Duplicants must perform the errands I've placed for them throughout the colony."
				});
			}

			// Token: 0x02002F0F RID: 12047
			public static class RECREATION
			{
				// Token: 0x0400CC6D RID: 52333
				public static LocString NAME = "Downtime";

				// Token: 0x0400CC6E RID: 52334
				public static LocString DESCRIPTION = "During Downtime my Duplicants they may do as they please.\n\nThis may include personal matters like bathroom visits or snacking, or they may choose to engage in leisure activities like socializing with friends.\n\nDowntime increases Duplicant Morale.";

				// Token: 0x0400CC6F RID: 52335
				public static LocString NOTIFICATION_TOOLTIP = string.Concat(new string[]
				{
					"During ",
					UI.PRE_KEYWORD,
					"Downtime",
					UI.PST_KEYWORD,
					" shifts my Duplicants they may do as they please."
				});
			}

			// Token: 0x02002F10 RID: 12048
			public static class SLEEP
			{
				// Token: 0x0400CC70 RID: 52336
				public static LocString NAME = "Bedtime";

				// Token: 0x0400CC71 RID: 52337
				public static LocString DESCRIPTION = "My Duplicants use Bedtime shifts to rest up after a hard day's work.\n\nScheduling too few bedtime shifts may prevent my Duplicants from regaining enough Stamina to make it through the following day.";

				// Token: 0x0400CC72 RID: 52338
				public static LocString NOTIFICATION_TOOLTIP = string.Concat(new string[]
				{
					"My Duplicants use ",
					UI.PRE_KEYWORD,
					"Bedtime",
					UI.PST_KEYWORD,
					" shifts to rest up after a hard day's work."
				});
			}
		}

		// Token: 0x020023F3 RID: 9203
		public class ELEMENTAL
		{
			// Token: 0x02002F11 RID: 12049
			public class AGE
			{
				// Token: 0x0400CC73 RID: 52339
				public static LocString NAME = "Age: {0}";

				// Token: 0x0400CC74 RID: 52340
				public static LocString TOOLTIP = "The selected object is {0} cycles old";

				// Token: 0x0400CC75 RID: 52341
				public static LocString UNKNOWN = "Unknown";

				// Token: 0x0400CC76 RID: 52342
				public static LocString UNKNOWN_TOOLTIP = "The age of the selected object is unknown";
			}

			// Token: 0x02002F12 RID: 12050
			public class UPTIME
			{
				// Token: 0x0400CC77 RID: 52343
				public static LocString NAME = "Uptime:\n{0}{1}: {2}\n{0}{3}: {4}\n{0}{5}: {6}";

				// Token: 0x0400CC78 RID: 52344
				public static LocString THIS_CYCLE = "This Cycle";

				// Token: 0x0400CC79 RID: 52345
				public static LocString LAST_CYCLE = "Last Cycle";

				// Token: 0x0400CC7A RID: 52346
				public static LocString LAST_X_CYCLES = "Last {0} Cycles";
			}

			// Token: 0x02002F13 RID: 12051
			public class PRIMARYELEMENT
			{
				// Token: 0x0400CC7B RID: 52347
				public static LocString NAME = "Primary Element: {0}";

				// Token: 0x0400CC7C RID: 52348
				public static LocString TOOLTIP = "The selected object is primarily composed of {0}";
			}

			// Token: 0x02002F14 RID: 12052
			public class UNITS
			{
				// Token: 0x0400CC7D RID: 52349
				public static LocString NAME = "Stack Units: {0}";

				// Token: 0x0400CC7E RID: 52350
				public static LocString TOOLTIP = "This stack contains {0} units of {1}";
			}

			// Token: 0x02002F15 RID: 12053
			public class MASS
			{
				// Token: 0x0400CC7F RID: 52351
				public static LocString NAME = "Mass: {0}";

				// Token: 0x0400CC80 RID: 52352
				public static LocString TOOLTIP = "The selected object has a mass of {0}";
			}

			// Token: 0x02002F16 RID: 12054
			public class TEMPERATURE
			{
				// Token: 0x0400CC81 RID: 52353
				public static LocString NAME = "Temperature: {0}";

				// Token: 0x0400CC82 RID: 52354
				public static LocString TOOLTIP = "The selected object's current temperature is {0}";
			}

			// Token: 0x02002F17 RID: 12055
			public class DISEASE
			{
				// Token: 0x0400CC83 RID: 52355
				public static LocString NAME = "Disease: {0}";

				// Token: 0x0400CC84 RID: 52356
				public static LocString TOOLTIP = "There are {0} on the selected object";
			}

			// Token: 0x02002F18 RID: 12056
			public class SHC
			{
				// Token: 0x0400CC85 RID: 52357
				public static LocString NAME = "Specific Heat Capacity: {0}";

				// Token: 0x0400CC86 RID: 52358
				public static LocString TOOLTIP = "{SPECIFIC_HEAT_CAPACITY} is required to heat 1 g of the selected object by 1 {TEMPERATURE_UNIT}";
			}

			// Token: 0x02002F19 RID: 12057
			public class THERMALCONDUCTIVITY
			{
				// Token: 0x0400CC87 RID: 52359
				public static LocString NAME = "Thermal Conductivity: {0}";

				// Token: 0x0400CC88 RID: 52360
				public static LocString TOOLTIP = "This object can conduct heat to other materials at a rate of {THERMAL_CONDUCTIVITY} W for each degree {TEMPERATURE_UNIT} difference\n\nBetween two objects, the rate of heat transfer will be determined by the object with the lowest Thermal Conductivity";

				// Token: 0x02003AF0 RID: 15088
				public class ADJECTIVES
				{
					// Token: 0x0400E9D2 RID: 59858
					public static LocString VALUE_WITH_ADJECTIVE = "{0} ({1})";

					// Token: 0x0400E9D3 RID: 59859
					public static LocString VERY_LOW_CONDUCTIVITY = "Highly Insulating";

					// Token: 0x0400E9D4 RID: 59860
					public static LocString LOW_CONDUCTIVITY = "Insulating";

					// Token: 0x0400E9D5 RID: 59861
					public static LocString MEDIUM_CONDUCTIVITY = "Conductive";

					// Token: 0x0400E9D6 RID: 59862
					public static LocString HIGH_CONDUCTIVITY = "Highly Conductive";

					// Token: 0x0400E9D7 RID: 59863
					public static LocString VERY_HIGH_CONDUCTIVITY = "Extremely Conductive";
				}
			}

			// Token: 0x02002F1A RID: 12058
			public class CONDUCTIVITYBARRIER
			{
				// Token: 0x0400CC89 RID: 52361
				public static LocString NAME = "Insulation Thickness: {0}";

				// Token: 0x0400CC8A RID: 52362
				public static LocString TOOLTIP = "Thick insulation reduces an object's Thermal Conductivity";
			}

			// Token: 0x02002F1B RID: 12059
			public class VAPOURIZATIONPOINT
			{
				// Token: 0x0400CC8B RID: 52363
				public static LocString NAME = "Vaporization Point: {0}";

				// Token: 0x0400CC8C RID: 52364
				public static LocString TOOLTIP = "The selected object will evaporate into a gas at {0}";
			}

			// Token: 0x02002F1C RID: 12060
			public class MELTINGPOINT
			{
				// Token: 0x0400CC8D RID: 52365
				public static LocString NAME = "Melting Point: {0}";

				// Token: 0x0400CC8E RID: 52366
				public static LocString TOOLTIP = "The selected object will melt into a liquid at {0}";
			}

			// Token: 0x02002F1D RID: 12061
			public class OVERHEATPOINT
			{
				// Token: 0x0400CC8F RID: 52367
				public static LocString NAME = "Overheat Modifier: {0}";

				// Token: 0x0400CC90 RID: 52368
				public static LocString TOOLTIP = "This building will overheat and take damage if its temperature reaches {0}\n\nBuilding with better building materials can increase overheat temperature";
			}

			// Token: 0x02002F1E RID: 12062
			public class FREEZEPOINT
			{
				// Token: 0x0400CC91 RID: 52369
				public static LocString NAME = "Freeze Point: {0}";

				// Token: 0x0400CC92 RID: 52370
				public static LocString TOOLTIP = "The selected object will cool into a solid at {0}";
			}

			// Token: 0x02002F1F RID: 12063
			public class DEWPOINT
			{
				// Token: 0x0400CC93 RID: 52371
				public static LocString NAME = "Condensation Point: {0}";

				// Token: 0x0400CC94 RID: 52372
				public static LocString TOOLTIP = "The selected object will condense into a liquid at {0}";
			}
		}

		// Token: 0x020023F4 RID: 9204
		public class IMMIGRANTSCREEN
		{
			// Token: 0x0400A2EB RID: 41707
			public static LocString IMMIGRANTSCREENTITLE = "Select a Blueprint";

			// Token: 0x0400A2EC RID: 41708
			public static LocString PROCEEDBUTTON = "Print";

			// Token: 0x0400A2ED RID: 41709
			public static LocString CANCELBUTTON = "Cancel";

			// Token: 0x0400A2EE RID: 41710
			public static LocString REJECTALL = "Reject All";

			// Token: 0x0400A2EF RID: 41711
			public static LocString EMBARK = "EMBARK";

			// Token: 0x0400A2F0 RID: 41712
			public static LocString SELECTDUPLICANTS = "Select {0} Duplicants";

			// Token: 0x0400A2F1 RID: 41713
			public static LocString SELECTYOURCREW = "CHOOSE THREE DUPLICANTS TO BEGIN";

			// Token: 0x0400A2F2 RID: 41714
			public static LocString SHUFFLE = "REROLL";

			// Token: 0x0400A2F3 RID: 41715
			public static LocString SHUFFLETOOLTIP = "Reroll for a different Duplicant";

			// Token: 0x0400A2F4 RID: 41716
			public static LocString BACK = "BACK";

			// Token: 0x0400A2F5 RID: 41717
			public static LocString CONFIRMATIONTITLE = "Reject All Printables?";

			// Token: 0x0400A2F6 RID: 41718
			public static LocString CONFIRMATIONBODY = "The Printing Pod will need time to recharge if I reject these Printables.";

			// Token: 0x0400A2F7 RID: 41719
			public static LocString NAME_YOUR_COLONY = "NAME THE COLONY";

			// Token: 0x0400A2F8 RID: 41720
			public static LocString CARE_PACKAGE_ELEMENT_QUANTITY = "{0} of {1}";

			// Token: 0x0400A2F9 RID: 41721
			public static LocString CARE_PACKAGE_ELEMENT_COUNT = "{0} x {1}";

			// Token: 0x0400A2FA RID: 41722
			public static LocString CARE_PACKAGE_ELEMENT_COUNT_ONLY = "x {0}";

			// Token: 0x0400A2FB RID: 41723
			public static LocString CARE_PACKAGE_CURRENT_AMOUNT = "Available: {0}";

			// Token: 0x0400A2FC RID: 41724
			public static LocString DUPLICATE_COLONY_NAME = "A colony named \"{0}\" already exists";
		}

		// Token: 0x020023F5 RID: 9205
		public class METERS
		{
			// Token: 0x02002F20 RID: 12064
			public class HEALTH
			{
				// Token: 0x0400CC95 RID: 52373
				public static LocString TOOLTIP = "Health";
			}

			// Token: 0x02002F21 RID: 12065
			public class BREATH
			{
				// Token: 0x0400CC96 RID: 52374
				public static LocString TOOLTIP = "Oxygen";
			}

			// Token: 0x02002F22 RID: 12066
			public class FUEL
			{
				// Token: 0x0400CC97 RID: 52375
				public static LocString TOOLTIP = "Fuel";
			}

			// Token: 0x02002F23 RID: 12067
			public class BATTERY
			{
				// Token: 0x0400CC98 RID: 52376
				public static LocString TOOLTIP = "Battery Charge";
			}
		}
	}
}
