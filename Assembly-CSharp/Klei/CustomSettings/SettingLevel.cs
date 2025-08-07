using System;

namespace Klei.CustomSettings
{
	// Token: 0x02000FC5 RID: 4037
	public class SettingLevel
	{
		// Token: 0x06007CB6 RID: 31926 RVA: 0x00320820 File Offset: 0x0031EA20
		public SettingLevel(string id, string label, string tooltip, long coordinate_value = 0L, object userdata = null)
		{
			this.id = id;
			this.label = label;
			this.tooltip = tooltip;
			this.userdata = userdata;
			this.coordinate_value = coordinate_value;
		}

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x06007CB7 RID: 31927 RVA: 0x0032084D File Offset: 0x0031EA4D
		// (set) Token: 0x06007CB8 RID: 31928 RVA: 0x00320855 File Offset: 0x0031EA55
		public string id { get; private set; }

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x06007CB9 RID: 31929 RVA: 0x0032085E File Offset: 0x0031EA5E
		// (set) Token: 0x06007CBA RID: 31930 RVA: 0x00320866 File Offset: 0x0031EA66
		public string tooltip { get; private set; }

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06007CBB RID: 31931 RVA: 0x0032086F File Offset: 0x0031EA6F
		// (set) Token: 0x06007CBC RID: 31932 RVA: 0x00320877 File Offset: 0x0031EA77
		public string label { get; private set; }

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x06007CBD RID: 31933 RVA: 0x00320880 File Offset: 0x0031EA80
		// (set) Token: 0x06007CBE RID: 31934 RVA: 0x00320888 File Offset: 0x0031EA88
		public object userdata { get; private set; }

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x06007CBF RID: 31935 RVA: 0x00320891 File Offset: 0x0031EA91
		// (set) Token: 0x06007CC0 RID: 31936 RVA: 0x00320899 File Offset: 0x0031EA99
		public long coordinate_value { get; private set; }
	}
}
