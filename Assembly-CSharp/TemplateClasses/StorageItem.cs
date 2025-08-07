using System;

namespace TemplateClasses
{
	// Token: 0x02000EB7 RID: 3767
	[Serializable]
	public class StorageItem
	{
		// Token: 0x060078A3 RID: 30883 RVA: 0x002EB2A0 File Offset: 0x002E94A0
		public StorageItem()
		{
			this.rottable = new Rottable();
		}

		// Token: 0x060078A4 RID: 30884 RVA: 0x002EB2B4 File Offset: 0x002E94B4
		public StorageItem(string _id, float _units, float _temp, SimHashes _element, string _disease, int _disease_count, bool _isOre)
		{
			this.rottable = new Rottable();
			this.id = _id;
			this.element = _element;
			this.units = _units;
			this.diseaseName = _disease;
			this.diseaseCount = _disease_count;
			this.isOre = _isOre;
			this.temperature = _temp;
		}

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x060078A5 RID: 30885 RVA: 0x002EB307 File Offset: 0x002E9507
		// (set) Token: 0x060078A6 RID: 30886 RVA: 0x002EB30F File Offset: 0x002E950F
		public string id { get; set; }

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x060078A7 RID: 30887 RVA: 0x002EB318 File Offset: 0x002E9518
		// (set) Token: 0x060078A8 RID: 30888 RVA: 0x002EB320 File Offset: 0x002E9520
		public SimHashes element { get; set; }

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x060078A9 RID: 30889 RVA: 0x002EB329 File Offset: 0x002E9529
		// (set) Token: 0x060078AA RID: 30890 RVA: 0x002EB331 File Offset: 0x002E9531
		public float units { get; set; }

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x060078AB RID: 30891 RVA: 0x002EB33A File Offset: 0x002E953A
		// (set) Token: 0x060078AC RID: 30892 RVA: 0x002EB342 File Offset: 0x002E9542
		public bool isOre { get; set; }

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x060078AD RID: 30893 RVA: 0x002EB34B File Offset: 0x002E954B
		// (set) Token: 0x060078AE RID: 30894 RVA: 0x002EB353 File Offset: 0x002E9553
		public float temperature { get; set; }

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x060078AF RID: 30895 RVA: 0x002EB35C File Offset: 0x002E955C
		// (set) Token: 0x060078B0 RID: 30896 RVA: 0x002EB364 File Offset: 0x002E9564
		public string diseaseName { get; set; }

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x060078B1 RID: 30897 RVA: 0x002EB36D File Offset: 0x002E956D
		// (set) Token: 0x060078B2 RID: 30898 RVA: 0x002EB375 File Offset: 0x002E9575
		public int diseaseCount { get; set; }

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x060078B3 RID: 30899 RVA: 0x002EB37E File Offset: 0x002E957E
		// (set) Token: 0x060078B4 RID: 30900 RVA: 0x002EB386 File Offset: 0x002E9586
		public Rottable rottable { get; set; }

		// Token: 0x060078B5 RID: 30901 RVA: 0x002EB390 File Offset: 0x002E9590
		public StorageItem Clone()
		{
			return new StorageItem(this.id, this.units, this.temperature, this.element, this.diseaseName, this.diseaseCount, this.isOre)
			{
				rottable = 
				{
					rotAmount = this.rottable.rotAmount
				}
			};
		}
	}
}
