using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000BA0 RID: 2976
public class StatusItem : Resource
{
	// Token: 0x060058C5 RID: 22725 RVA: 0x00201880 File Offset: 0x001FFA80
	private StatusItem(string id, string composed_prefix)
		: base(id, Strings.Get(composed_prefix + ".NAME"))
	{
		this.composedPrefix = composed_prefix;
		this.tooltipText = Strings.Get(composed_prefix + ".TOOLTIP");
	}

	// Token: 0x060058C6 RID: 22726 RVA: 0x002018D4 File Offset: 0x001FFAD4
	private void SetIcon(string icon, StatusItem.IconType icon_type, NotificationType notification_type, bool allow_multiples, HashedString render_overlay, bool show_world_icon = true, int status_overlays = 129022, Func<string, object, string> resolve_string_callback = null)
	{
		switch (icon_type)
		{
		case StatusItem.IconType.Info:
			icon = "dash";
			break;
		case StatusItem.IconType.Exclamation:
			icon = "status_item_exclamation";
			break;
		}
		this.iconName = icon;
		this.notificationType = notification_type;
		this.sprite = Assets.GetTintedSprite(icon);
		if (this.sprite == null)
		{
			this.sprite = new TintedSprite();
			this.sprite.sprite = Assets.GetSprite(icon);
			this.sprite.color = new Color(0f, 0f, 0f, 255f);
		}
		this.iconType = icon_type;
		this.allowMultiples = allow_multiples;
		this.render_overlay = render_overlay;
		this.showShowWorldIcon = show_world_icon;
		this.status_overlays = status_overlays;
		this.resolveStringCallback = resolve_string_callback;
		if (this.sprite == null)
		{
			global::Debug.LogWarning("Status item '" + this.Id + "' references a missing icon: " + icon);
		}
	}

	// Token: 0x060058C7 RID: 22727 RVA: 0x002019C0 File Offset: 0x001FFBC0
	public StatusItem(string id, string prefix, string icon, StatusItem.IconType icon_type, NotificationType notification_type, bool allow_multiples, HashedString render_overlay, bool showWorldIcon = true, int status_overlays = 129022, Func<string, object, string> resolve_string_callback = null)
		: this(id, "STRINGS." + prefix + ".STATUSITEMS." + id.ToUpper())
	{
		this.SetIcon(icon, icon_type, notification_type, allow_multiples, render_overlay, showWorldIcon, status_overlays, resolve_string_callback);
	}

	// Token: 0x060058C8 RID: 22728 RVA: 0x00201A00 File Offset: 0x001FFC00
	public StatusItem(string id, string name, string tooltip, string icon, StatusItem.IconType icon_type, NotificationType notification_type, bool allow_multiples, HashedString render_overlay, int status_overlays = 129022, bool showWorldIcon = true, Func<string, object, string> resolve_string_callback = null)
		: base(id, name)
	{
		this.tooltipText = tooltip;
		this.SetIcon(icon, icon_type, notification_type, allow_multiples, render_overlay, showWorldIcon, status_overlays, resolve_string_callback);
	}

	// Token: 0x060058C9 RID: 22729 RVA: 0x00201A3C File Offset: 0x001FFC3C
	public void AddNotification(string sound_path = null, string notification_text = null, string notification_tooltip = null)
	{
		this.shouldNotify = true;
		if (sound_path == null)
		{
			if (this.notificationType == NotificationType.Bad)
			{
				this.soundPath = "Warning";
			}
			else
			{
				this.soundPath = "Notification";
			}
		}
		else
		{
			this.soundPath = sound_path;
		}
		if (notification_text != null)
		{
			this.notificationText = notification_text;
		}
		else
		{
			DebugUtil.Assert(this.composedPrefix != null, "When adding a notification, either set the status prefix or specify strings!");
			this.notificationText = Strings.Get(this.composedPrefix + ".NOTIFICATION_NAME");
		}
		if (notification_tooltip != null)
		{
			this.notificationTooltipText = notification_tooltip;
			return;
		}
		DebugUtil.Assert(this.composedPrefix != null, "When adding a notification, either set the status prefix or specify strings!");
		this.notificationTooltipText = Strings.Get(this.composedPrefix + ".NOTIFICATION_TOOLTIP");
	}

	// Token: 0x060058CA RID: 22730 RVA: 0x00201AFA File Offset: 0x001FFCFA
	public virtual string GetName(object data)
	{
		return this.ResolveString(this.Name, data);
	}

	// Token: 0x060058CB RID: 22731 RVA: 0x00201B09 File Offset: 0x001FFD09
	public virtual string GetTooltip(object data)
	{
		return this.ResolveTooltip(this.tooltipText, data);
	}

	// Token: 0x060058CC RID: 22732 RVA: 0x00201B18 File Offset: 0x001FFD18
	private string ResolveString(string str, object data)
	{
		if (this.resolveStringCallback != null && (data != null || this.resolveStringCallback_shouldStillCallIfDataIsNull))
		{
			return this.resolveStringCallback(str, data);
		}
		return str;
	}

	// Token: 0x060058CD RID: 22733 RVA: 0x00201B3C File Offset: 0x001FFD3C
	private string ResolveTooltip(string str, object data)
	{
		if (data != null)
		{
			if (this.resolveTooltipCallback != null)
			{
				return this.resolveTooltipCallback(str, data);
			}
			if (this.resolveStringCallback != null)
			{
				return this.resolveStringCallback(str, data);
			}
		}
		else
		{
			if (this.resolveStringCallback_shouldStillCallIfDataIsNull && this.resolveStringCallback != null)
			{
				return this.resolveStringCallback(str, data);
			}
			if (this.resolveTooltipCallback_shouldStillCallIfDataIsNull && this.resolveTooltipCallback != null)
			{
				return this.resolveTooltipCallback(str, data);
			}
		}
		return str;
	}

	// Token: 0x060058CE RID: 22734 RVA: 0x00201BB5 File Offset: 0x001FFDB5
	public bool ShouldShowIcon()
	{
		return this.iconType == StatusItem.IconType.Custom && this.showShowWorldIcon;
	}

	// Token: 0x060058CF RID: 22735 RVA: 0x00201BC8 File Offset: 0x001FFDC8
	public virtual void ShowToolTip(ToolTip tooltip_widget, object data, TextStyleSetting property_style)
	{
		tooltip_widget.ClearMultiStringTooltip();
		string tooltip = this.GetTooltip(data);
		tooltip_widget.AddMultiStringTooltip(tooltip, property_style);
	}

	// Token: 0x060058D0 RID: 22736 RVA: 0x00201BEB File Offset: 0x001FFDEB
	public void SetIcon(Image image, object data)
	{
		if (this.sprite == null)
		{
			return;
		}
		image.color = this.sprite.color;
		image.sprite = this.sprite.sprite;
	}

	// Token: 0x060058D1 RID: 22737 RVA: 0x00201C18 File Offset: 0x001FFE18
	public bool UseConditionalCallback(HashedString overlay, Transform transform)
	{
		return overlay != OverlayModes.None.ID && this.conditionalOverlayCallback != null && this.conditionalOverlayCallback(overlay, transform);
	}

	// Token: 0x060058D2 RID: 22738 RVA: 0x00201C3E File Offset: 0x001FFE3E
	public StatusItem SetResolveStringCallback(Func<string, object, string> cb)
	{
		this.resolveStringCallback = cb;
		return this;
	}

	// Token: 0x060058D3 RID: 22739 RVA: 0x00201C48 File Offset: 0x001FFE48
	public void OnClick(object data)
	{
		if (this.statusItemClickCallback != null)
		{
			this.statusItemClickCallback(data);
		}
	}

	// Token: 0x060058D4 RID: 22740 RVA: 0x00201C60 File Offset: 0x001FFE60
	public static StatusItem.StatusItemOverlays GetStatusItemOverlayBySimViewMode(HashedString mode)
	{
		StatusItem.StatusItemOverlays statusItemOverlays;
		if (!StatusItem.overlayBitfieldMap.TryGetValue(mode, out statusItemOverlays))
		{
			string text = "ViewMode ";
			HashedString hashedString = mode;
			global::Debug.LogWarning(text + hashedString.ToString() + " has no StatusItemOverlay value");
			statusItemOverlays = StatusItem.StatusItemOverlays.None;
		}
		return statusItemOverlays;
	}

	// Token: 0x04003AEC RID: 15084
	public string tooltipText;

	// Token: 0x04003AED RID: 15085
	public string notificationText;

	// Token: 0x04003AEE RID: 15086
	public string notificationTooltipText;

	// Token: 0x04003AEF RID: 15087
	public string soundPath;

	// Token: 0x04003AF0 RID: 15088
	public string iconName;

	// Token: 0x04003AF1 RID: 15089
	public bool unique;

	// Token: 0x04003AF2 RID: 15090
	public TintedSprite sprite;

	// Token: 0x04003AF3 RID: 15091
	public bool shouldNotify;

	// Token: 0x04003AF4 RID: 15092
	public StatusItem.IconType iconType;

	// Token: 0x04003AF5 RID: 15093
	public NotificationType notificationType;

	// Token: 0x04003AF6 RID: 15094
	public Notification.ClickCallback notificationClickCallback;

	// Token: 0x04003AF7 RID: 15095
	public Func<string, object, string> resolveStringCallback;

	// Token: 0x04003AF8 RID: 15096
	public Func<string, object, string> resolveTooltipCallback;

	// Token: 0x04003AF9 RID: 15097
	public bool resolveStringCallback_shouldStillCallIfDataIsNull;

	// Token: 0x04003AFA RID: 15098
	public bool resolveTooltipCallback_shouldStillCallIfDataIsNull;

	// Token: 0x04003AFB RID: 15099
	public bool allowMultiples;

	// Token: 0x04003AFC RID: 15100
	public Func<HashedString, object, bool> conditionalOverlayCallback;

	// Token: 0x04003AFD RID: 15101
	public HashedString render_overlay;

	// Token: 0x04003AFE RID: 15102
	public int status_overlays;

	// Token: 0x04003AFF RID: 15103
	public Action<object> statusItemClickCallback;

	// Token: 0x04003B00 RID: 15104
	private string composedPrefix;

	// Token: 0x04003B01 RID: 15105
	private bool showShowWorldIcon = true;

	// Token: 0x04003B02 RID: 15106
	public const int ALL_OVERLAYS = 129022;

	// Token: 0x04003B03 RID: 15107
	private static Dictionary<HashedString, StatusItem.StatusItemOverlays> overlayBitfieldMap = new Dictionary<HashedString, StatusItem.StatusItemOverlays>
	{
		{
			OverlayModes.None.ID,
			StatusItem.StatusItemOverlays.None
		},
		{
			OverlayModes.Power.ID,
			StatusItem.StatusItemOverlays.PowerMap
		},
		{
			OverlayModes.Temperature.ID,
			StatusItem.StatusItemOverlays.Temperature
		},
		{
			OverlayModes.ThermalConductivity.ID,
			StatusItem.StatusItemOverlays.ThermalComfort
		},
		{
			OverlayModes.Light.ID,
			StatusItem.StatusItemOverlays.Light
		},
		{
			OverlayModes.LiquidConduits.ID,
			StatusItem.StatusItemOverlays.LiquidPlumbing
		},
		{
			OverlayModes.GasConduits.ID,
			StatusItem.StatusItemOverlays.GasPlumbing
		},
		{
			OverlayModes.SolidConveyor.ID,
			StatusItem.StatusItemOverlays.Conveyor
		},
		{
			OverlayModes.Decor.ID,
			StatusItem.StatusItemOverlays.Decor
		},
		{
			OverlayModes.Disease.ID,
			StatusItem.StatusItemOverlays.Pathogens
		},
		{
			OverlayModes.Crop.ID,
			StatusItem.StatusItemOverlays.Farming
		},
		{
			OverlayModes.Rooms.ID,
			StatusItem.StatusItemOverlays.Rooms
		},
		{
			OverlayModes.Suit.ID,
			StatusItem.StatusItemOverlays.Suits
		},
		{
			OverlayModes.Logic.ID,
			StatusItem.StatusItemOverlays.Logic
		},
		{
			OverlayModes.Oxygen.ID,
			StatusItem.StatusItemOverlays.None
		},
		{
			OverlayModes.TileMode.ID,
			StatusItem.StatusItemOverlays.None
		},
		{
			OverlayModes.Radiation.ID,
			StatusItem.StatusItemOverlays.Radiation
		}
	};

	// Token: 0x02001CCB RID: 7371
	public enum IconType
	{
		// Token: 0x0400874D RID: 34637
		Info,
		// Token: 0x0400874E RID: 34638
		Exclamation,
		// Token: 0x0400874F RID: 34639
		Custom
	}

	// Token: 0x02001CCC RID: 7372
	[Flags]
	public enum StatusItemOverlays
	{
		// Token: 0x04008751 RID: 34641
		None = 2,
		// Token: 0x04008752 RID: 34642
		PowerMap = 4,
		// Token: 0x04008753 RID: 34643
		Temperature = 8,
		// Token: 0x04008754 RID: 34644
		ThermalComfort = 16,
		// Token: 0x04008755 RID: 34645
		Light = 32,
		// Token: 0x04008756 RID: 34646
		LiquidPlumbing = 64,
		// Token: 0x04008757 RID: 34647
		GasPlumbing = 128,
		// Token: 0x04008758 RID: 34648
		Decor = 256,
		// Token: 0x04008759 RID: 34649
		Pathogens = 512,
		// Token: 0x0400875A RID: 34650
		Farming = 1024,
		// Token: 0x0400875B RID: 34651
		Rooms = 4096,
		// Token: 0x0400875C RID: 34652
		Suits = 8192,
		// Token: 0x0400875D RID: 34653
		Logic = 16384,
		// Token: 0x0400875E RID: 34654
		Conveyor = 32768,
		// Token: 0x0400875F RID: 34655
		Radiation = 65536
	}
}
