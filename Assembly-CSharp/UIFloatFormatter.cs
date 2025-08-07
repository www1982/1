using System;
using System.Collections.Generic;

// Token: 0x02000C47 RID: 3143
public class UIFloatFormatter
{
	// Token: 0x0600600E RID: 24590 RVA: 0x002363E3 File Offset: 0x002345E3
	public string Format(string format, float value)
	{
		return this.Replace(format, "{0}", value);
	}

	// Token: 0x0600600F RID: 24591 RVA: 0x002363F4 File Offset: 0x002345F4
	private string Replace(string format, string key, float value)
	{
		UIFloatFormatter.Entry entry = default(UIFloatFormatter.Entry);
		if (this.activeStringCount >= this.entries.Count)
		{
			entry.format = format;
			entry.key = key;
			entry.value = value;
			entry.result = entry.format.Replace(key, value.ToString());
			this.entries.Add(entry);
		}
		else
		{
			entry = this.entries[this.activeStringCount];
			if (entry.format != format || entry.key != key || entry.value != value)
			{
				entry.format = format;
				entry.key = key;
				entry.value = value;
				entry.result = entry.format.Replace(key, value.ToString());
				this.entries[this.activeStringCount] = entry;
			}
		}
		this.activeStringCount++;
		return entry.result;
	}

	// Token: 0x06006010 RID: 24592 RVA: 0x002364EB File Offset: 0x002346EB
	public void BeginDrawing()
	{
		this.activeStringCount = 0;
	}

	// Token: 0x06006011 RID: 24593 RVA: 0x002364F4 File Offset: 0x002346F4
	public void EndDrawing()
	{
	}

	// Token: 0x0400411A RID: 16666
	private int activeStringCount;

	// Token: 0x0400411B RID: 16667
	private List<UIFloatFormatter.Entry> entries = new List<UIFloatFormatter.Entry>();

	// Token: 0x02001E10 RID: 7696
	private struct Entry
	{
		// Token: 0x04008C75 RID: 35957
		public string format;

		// Token: 0x04008C76 RID: 35958
		public string key;

		// Token: 0x04008C77 RID: 35959
		public float value;

		// Token: 0x04008C78 RID: 35960
		public string result;
	}
}
