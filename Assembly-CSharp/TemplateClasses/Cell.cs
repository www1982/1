using System;

namespace TemplateClasses
{
	// Token: 0x02000EB6 RID: 3766
	[Serializable]
	public class Cell
	{
		// Token: 0x06007891 RID: 30865 RVA: 0x002EB1BD File Offset: 0x002E93BD
		public Cell()
		{
		}

		// Token: 0x06007892 RID: 30866 RVA: 0x002EB1C8 File Offset: 0x002E93C8
		public Cell(int loc_x, int loc_y, SimHashes _element, float _temperature, float _mass, string _diseaseName, int _diseaseCount, bool _preventFoWReveal = false)
		{
			this.location_x = loc_x;
			this.location_y = loc_y;
			this.element = _element;
			this.temperature = _temperature;
			this.mass = _mass;
			this.diseaseName = _diseaseName;
			this.diseaseCount = _diseaseCount;
			this.preventFoWReveal = _preventFoWReveal;
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x06007893 RID: 30867 RVA: 0x002EB218 File Offset: 0x002E9418
		// (set) Token: 0x06007894 RID: 30868 RVA: 0x002EB220 File Offset: 0x002E9420
		public SimHashes element { get; set; }

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x06007895 RID: 30869 RVA: 0x002EB229 File Offset: 0x002E9429
		// (set) Token: 0x06007896 RID: 30870 RVA: 0x002EB231 File Offset: 0x002E9431
		public float mass { get; set; }

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x06007897 RID: 30871 RVA: 0x002EB23A File Offset: 0x002E943A
		// (set) Token: 0x06007898 RID: 30872 RVA: 0x002EB242 File Offset: 0x002E9442
		public float temperature { get; set; }

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06007899 RID: 30873 RVA: 0x002EB24B File Offset: 0x002E944B
		// (set) Token: 0x0600789A RID: 30874 RVA: 0x002EB253 File Offset: 0x002E9453
		public string diseaseName { get; set; }

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x0600789B RID: 30875 RVA: 0x002EB25C File Offset: 0x002E945C
		// (set) Token: 0x0600789C RID: 30876 RVA: 0x002EB264 File Offset: 0x002E9464
		public int diseaseCount { get; set; }

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x0600789D RID: 30877 RVA: 0x002EB26D File Offset: 0x002E946D
		// (set) Token: 0x0600789E RID: 30878 RVA: 0x002EB275 File Offset: 0x002E9475
		public int location_x { get; set; }

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x0600789F RID: 30879 RVA: 0x002EB27E File Offset: 0x002E947E
		// (set) Token: 0x060078A0 RID: 30880 RVA: 0x002EB286 File Offset: 0x002E9486
		public int location_y { get; set; }

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x060078A1 RID: 30881 RVA: 0x002EB28F File Offset: 0x002E948F
		// (set) Token: 0x060078A2 RID: 30882 RVA: 0x002EB297 File Offset: 0x002E9497
		public bool preventFoWReveal { get; set; }
	}
}
