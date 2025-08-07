using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x020004B9 RID: 1209
public class EventInfoData
{
	// Token: 0x060019C6 RID: 6598 RVA: 0x0008DE34 File Offset: 0x0008C034
	public EventInfoData(string title, string description, HashedString animFileName)
	{
		this.title = title;
		this.description = description;
		this.animFileName = animFileName;
	}

	// Token: 0x060019C7 RID: 6599 RVA: 0x0008DE82 File Offset: 0x0008C082
	public List<EventInfoData.Option> GetOptions()
	{
		this.FinalizeText();
		return this.options;
	}

	// Token: 0x060019C8 RID: 6600 RVA: 0x0008DE90 File Offset: 0x0008C090
	public EventInfoData.Option AddOption(string mainText, string description = null)
	{
		EventInfoData.Option option = new EventInfoData.Option
		{
			mainText = mainText,
			description = description
		};
		this.options.Add(option);
		this.dirty = true;
		return option;
	}

	// Token: 0x060019C9 RID: 6601 RVA: 0x0008DEC8 File Offset: 0x0008C0C8
	public EventInfoData.Option SimpleOption(string mainText, global::System.Action callback)
	{
		EventInfoData.Option option = new EventInfoData.Option
		{
			mainText = mainText,
			callback = callback
		};
		this.options.Add(option);
		this.dirty = true;
		return option;
	}

	// Token: 0x060019CA RID: 6602 RVA: 0x0008DEFD File Offset: 0x0008C0FD
	public EventInfoData.Option AddDefaultOption(global::System.Action callback = null)
	{
		return this.SimpleOption(GAMEPLAY_EVENTS.DEFAULT_OPTION_NAME, callback);
	}

	// Token: 0x060019CB RID: 6603 RVA: 0x0008DF10 File Offset: 0x0008C110
	public EventInfoData.Option AddDefaultConsiderLaterOption(global::System.Action callback = null)
	{
		return this.SimpleOption(GAMEPLAY_EVENTS.DEFAULT_OPTION_CONSIDER_NAME, callback);
	}

	// Token: 0x060019CC RID: 6604 RVA: 0x0008DF23 File Offset: 0x0008C123
	public void SetTextParameter(string key, string value)
	{
		this.textParameters[key] = value;
		this.dirty = true;
	}

	// Token: 0x060019CD RID: 6605 RVA: 0x0008DF3C File Offset: 0x0008C13C
	public void FinalizeText()
	{
		if (!this.dirty)
		{
			return;
		}
		this.dirty = false;
		foreach (KeyValuePair<string, string> keyValuePair in this.textParameters)
		{
			string text = "{" + keyValuePair.Key + "}";
			if (this.title != null)
			{
				this.title = this.title.Replace(text, keyValuePair.Value);
			}
			if (this.description != null)
			{
				this.description = this.description.Replace(text, keyValuePair.Value);
			}
			if (this.location != null)
			{
				this.location = this.location.Replace(text, keyValuePair.Value);
			}
			if (this.whenDescription != null)
			{
				this.whenDescription = this.whenDescription.Replace(text, keyValuePair.Value);
			}
			foreach (EventInfoData.Option option in this.options)
			{
				if (option.mainText != null)
				{
					option.mainText = option.mainText.Replace(text, keyValuePair.Value);
				}
				if (option.description != null)
				{
					option.description = option.description.Replace(text, keyValuePair.Value);
				}
				if (option.tooltip != null)
				{
					option.tooltip = option.tooltip.Replace(text, keyValuePair.Value);
				}
				foreach (EventInfoData.OptionIcon optionIcon in option.informationIcons)
				{
					if (optionIcon.tooltip != null)
					{
						optionIcon.tooltip = optionIcon.tooltip.Replace(text, keyValuePair.Value);
					}
				}
				foreach (EventInfoData.OptionIcon optionIcon2 in option.consequenceIcons)
				{
					if (optionIcon2.tooltip != null)
					{
						optionIcon2.tooltip = optionIcon2.tooltip.Replace(text, keyValuePair.Value);
					}
				}
			}
		}
	}

	// Token: 0x04000EC8 RID: 3784
	public string title;

	// Token: 0x04000EC9 RID: 3785
	public string description;

	// Token: 0x04000ECA RID: 3786
	public string location;

	// Token: 0x04000ECB RID: 3787
	public string whenDescription;

	// Token: 0x04000ECC RID: 3788
	public Transform clickFocus;

	// Token: 0x04000ECD RID: 3789
	public GameObject[] minions;

	// Token: 0x04000ECE RID: 3790
	public GameObject artifact;

	// Token: 0x04000ECF RID: 3791
	public HashedString animFileName;

	// Token: 0x04000ED0 RID: 3792
	public HashedString mainAnim = "event";

	// Token: 0x04000ED1 RID: 3793
	public Dictionary<string, string> textParameters = new Dictionary<string, string>();

	// Token: 0x04000ED2 RID: 3794
	public List<EventInfoData.Option> options = new List<EventInfoData.Option>();

	// Token: 0x04000ED3 RID: 3795
	public global::System.Action showCallback;

	// Token: 0x04000ED4 RID: 3796
	private bool dirty;

	// Token: 0x020012F2 RID: 4850
	public class OptionIcon
	{
		// Token: 0x0600883A RID: 34874 RVA: 0x00349127 File Offset: 0x00347327
		public OptionIcon(Sprite sprite, EventInfoData.OptionIcon.ContainerType containerType, string tooltip, float scale = 1f)
		{
			this.sprite = sprite;
			this.containerType = containerType;
			this.tooltip = tooltip;
			this.scale = scale;
		}

		// Token: 0x040067FA RID: 26618
		public EventInfoData.OptionIcon.ContainerType containerType;

		// Token: 0x040067FB RID: 26619
		public Sprite sprite;

		// Token: 0x040067FC RID: 26620
		public string tooltip;

		// Token: 0x040067FD RID: 26621
		public float scale;

		// Token: 0x02002694 RID: 9876
		public enum ContainerType
		{
			// Token: 0x0400AB71 RID: 43889
			Neutral,
			// Token: 0x0400AB72 RID: 43890
			Positive,
			// Token: 0x0400AB73 RID: 43891
			Negative,
			// Token: 0x0400AB74 RID: 43892
			Information
		}
	}

	// Token: 0x020012F3 RID: 4851
	public class Option
	{
		// Token: 0x0600883B RID: 34875 RVA: 0x0034914C File Offset: 0x0034734C
		public void AddInformationIcon(string tooltip, float scale = 1f)
		{
			this.informationIcons.Add(new EventInfoData.OptionIcon(null, EventInfoData.OptionIcon.ContainerType.Information, tooltip, scale));
		}

		// Token: 0x0600883C RID: 34876 RVA: 0x00349162 File Offset: 0x00347362
		public void AddPositiveIcon(Sprite sprite, string tooltip, float scale = 1f)
		{
			this.consequenceIcons.Add(new EventInfoData.OptionIcon(sprite, EventInfoData.OptionIcon.ContainerType.Positive, tooltip, scale));
		}

		// Token: 0x0600883D RID: 34877 RVA: 0x00349178 File Offset: 0x00347378
		public void AddNeutralIcon(Sprite sprite, string tooltip, float scale = 1f)
		{
			this.consequenceIcons.Add(new EventInfoData.OptionIcon(sprite, EventInfoData.OptionIcon.ContainerType.Neutral, tooltip, scale));
		}

		// Token: 0x0600883E RID: 34878 RVA: 0x0034918E File Offset: 0x0034738E
		public void AddNegativeIcon(Sprite sprite, string tooltip, float scale = 1f)
		{
			this.consequenceIcons.Add(new EventInfoData.OptionIcon(sprite, EventInfoData.OptionIcon.ContainerType.Negative, tooltip, scale));
		}

		// Token: 0x040067FE RID: 26622
		public string mainText;

		// Token: 0x040067FF RID: 26623
		public string description;

		// Token: 0x04006800 RID: 26624
		public string tooltip;

		// Token: 0x04006801 RID: 26625
		public global::System.Action callback;

		// Token: 0x04006802 RID: 26626
		public List<EventInfoData.OptionIcon> informationIcons = new List<EventInfoData.OptionIcon>();

		// Token: 0x04006803 RID: 26627
		public List<EventInfoData.OptionIcon> consequenceIcons = new List<EventInfoData.OptionIcon>();

		// Token: 0x04006804 RID: 26628
		public bool allowed = true;
	}
}
