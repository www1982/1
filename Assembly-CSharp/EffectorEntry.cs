using System;
using STRINGS;

// Token: 0x020008D3 RID: 2259
internal struct EffectorEntry
{
	// Token: 0x06003EAE RID: 16046 RVA: 0x001614EF File Offset: 0x0015F6EF
	public EffectorEntry(string name, float value)
	{
		this.name = name;
		this.value = value;
		this.count = 1;
	}

	// Token: 0x06003EAF RID: 16047 RVA: 0x00161508 File Offset: 0x0015F708
	public override string ToString()
	{
		string text = "";
		if (this.count > 1)
		{
			text = string.Format(UI.OVERLAYS.DECOR.COUNT, this.count);
		}
		return string.Format(UI.OVERLAYS.DECOR.ENTRY, GameUtil.GetFormattedDecor(this.value, false), this.name, text);
	}

	// Token: 0x040026AA RID: 9898
	public string name;

	// Token: 0x040026AB RID: 9899
	public int count;

	// Token: 0x040026AC RID: 9900
	public float value;
}
