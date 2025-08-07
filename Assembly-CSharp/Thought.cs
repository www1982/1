using System;
using UnityEngine;

// Token: 0x02000BB6 RID: 2998
public class Thought : Resource
{
	// Token: 0x060059B3 RID: 22963 RVA: 0x00206884 File Offset: 0x00204A84
	public Thought(string id, ResourceSet parent, Sprite icon, string mode_icon, string sound_name, string bubble, string speech_prefix, LocString hover_text, bool show_immediately = false, float show_time = 4f)
		: base(id, parent, null)
	{
		this.sprite = icon;
		if (mode_icon != null)
		{
			this.modeSprite = Assets.GetSprite(mode_icon);
		}
		this.bubbleSprite = Assets.GetSprite(bubble);
		this.sound = sound_name;
		this.speechPrefix = speech_prefix;
		this.hoverText = hover_text;
		this.showImmediately = show_immediately;
		this.showTime = show_time;
	}

	// Token: 0x060059B4 RID: 22964 RVA: 0x002068F4 File Offset: 0x00204AF4
	public Thought(string id, ResourceSet parent, string icon, string mode_icon, string sound_name, string bubble, string speech_prefix, LocString hover_text, bool show_immediately = false, float show_time = 4f)
		: base(id, parent, null)
	{
		this.sprite = Assets.GetSprite(icon);
		if (mode_icon != null)
		{
			this.modeSprite = Assets.GetSprite(mode_icon);
		}
		this.bubbleSprite = Assets.GetSprite(bubble);
		this.sound = sound_name;
		this.speechPrefix = speech_prefix;
		this.hoverText = hover_text;
		this.showImmediately = show_immediately;
		this.showTime = show_time;
	}

	// Token: 0x04003B85 RID: 15237
	public int priority;

	// Token: 0x04003B86 RID: 15238
	public Sprite sprite;

	// Token: 0x04003B87 RID: 15239
	public Sprite modeSprite;

	// Token: 0x04003B88 RID: 15240
	public string sound;

	// Token: 0x04003B89 RID: 15241
	public Sprite bubbleSprite;

	// Token: 0x04003B8A RID: 15242
	public string speechPrefix;

	// Token: 0x04003B8B RID: 15243
	public LocString hoverText;

	// Token: 0x04003B8C RID: 15244
	public bool showImmediately;

	// Token: 0x04003B8D RID: 15245
	public float showTime;
}
