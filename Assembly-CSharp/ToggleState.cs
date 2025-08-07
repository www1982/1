using System;
using UnityEngine;

// Token: 0x02000E87 RID: 3719
[Serializable]
public struct ToggleState
{
	// Token: 0x04005276 RID: 21110
	public string Name;

	// Token: 0x04005277 RID: 21111
	public string on_click_override_sound_path;

	// Token: 0x04005278 RID: 21112
	public string on_release_override_sound_path;

	// Token: 0x04005279 RID: 21113
	public string sound_parameter_name;

	// Token: 0x0400527A RID: 21114
	public float sound_parameter_value;

	// Token: 0x0400527B RID: 21115
	public bool has_sound_parameter;

	// Token: 0x0400527C RID: 21116
	public Sprite sprite;

	// Token: 0x0400527D RID: 21117
	public Color color;

	// Token: 0x0400527E RID: 21118
	public Color color_on_hover;

	// Token: 0x0400527F RID: 21119
	public bool use_color_on_hover;

	// Token: 0x04005280 RID: 21120
	public bool use_rect_margins;

	// Token: 0x04005281 RID: 21121
	public Vector2 rect_margins;

	// Token: 0x04005282 RID: 21122
	public StatePresentationSetting[] additional_display_settings;
}
