using System;
using System.Collections.Generic;

namespace TemplateClasses
{
	// Token: 0x02000EB5 RID: 3765
	[Serializable]
	public class Prefab
	{
		// Token: 0x0600786D RID: 30829 RVA: 0x002EAED7 File Offset: 0x002E90D7
		public Prefab()
		{
			this.type = Prefab.Type.Other;
		}

		// Token: 0x0600786E RID: 30830 RVA: 0x002EAEE8 File Offset: 0x002E90E8
		public Prefab(string _id, Prefab.Type _type, int loc_x, int loc_y, SimHashes _element, float _temperature = -1f, float _units = 1f, string _disease = null, int _disease_count = 0, Orientation _rotation = Orientation.Neutral, Prefab.template_amount_value[] _amount_values = null, Prefab.template_amount_value[] _other_values = null, int _connections = 0, string facadeIdId = null)
		{
			this.id = _id;
			this.type = _type;
			this.location_x = loc_x;
			this.location_y = loc_y;
			this.connections = _connections;
			this.element = _element;
			this.temperature = _temperature;
			this.units = _units;
			this.diseaseName = _disease;
			this.diseaseCount = _disease_count;
			this.facadeId = facadeIdId;
			this.rotationOrientation = _rotation;
			if (_amount_values != null && _amount_values.Length != 0)
			{
				this.amounts = _amount_values;
			}
			if (_other_values != null && _other_values.Length != 0)
			{
				this.other_values = _other_values;
			}
		}

		// Token: 0x0600786F RID: 30831 RVA: 0x002EAF7C File Offset: 0x002E917C
		public Prefab Clone(Vector2I offset)
		{
			Prefab prefab = new Prefab(this.id, this.type, offset.x + this.location_x, offset.y + this.location_y, this.element, this.temperature, this.units, this.diseaseName, this.diseaseCount, this.rotationOrientation, this.amounts, this.other_values, this.connections, this.facadeId);
			if (this.rottable != null)
			{
				prefab.rottable = new Rottable();
				prefab.rottable.rotAmount = this.rottable.rotAmount;
			}
			if (this.storage != null && this.storage.Count > 0)
			{
				prefab.storage = new List<StorageItem>();
				foreach (StorageItem storageItem in this.storage)
				{
					prefab.storage.Add(storageItem.Clone());
				}
			}
			return prefab;
		}

		// Token: 0x06007870 RID: 30832 RVA: 0x002EB08C File Offset: 0x002E928C
		public void AssignStorage(StorageItem _storage)
		{
			if (this.storage == null)
			{
				this.storage = new List<StorageItem>();
			}
			this.storage.Add(_storage);
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x06007871 RID: 30833 RVA: 0x002EB0AD File Offset: 0x002E92AD
		// (set) Token: 0x06007872 RID: 30834 RVA: 0x002EB0B5 File Offset: 0x002E92B5
		public string id { get; set; }

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06007873 RID: 30835 RVA: 0x002EB0BE File Offset: 0x002E92BE
		// (set) Token: 0x06007874 RID: 30836 RVA: 0x002EB0C6 File Offset: 0x002E92C6
		public int location_x { get; set; }

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06007875 RID: 30837 RVA: 0x002EB0CF File Offset: 0x002E92CF
		// (set) Token: 0x06007876 RID: 30838 RVA: 0x002EB0D7 File Offset: 0x002E92D7
		public int location_y { get; set; }

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x06007877 RID: 30839 RVA: 0x002EB0E0 File Offset: 0x002E92E0
		// (set) Token: 0x06007878 RID: 30840 RVA: 0x002EB0E8 File Offset: 0x002E92E8
		public SimHashes element { get; set; }

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x06007879 RID: 30841 RVA: 0x002EB0F1 File Offset: 0x002E92F1
		// (set) Token: 0x0600787A RID: 30842 RVA: 0x002EB0F9 File Offset: 0x002E92F9
		public float temperature { get; set; }

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x0600787B RID: 30843 RVA: 0x002EB102 File Offset: 0x002E9302
		// (set) Token: 0x0600787C RID: 30844 RVA: 0x002EB10A File Offset: 0x002E930A
		public float units { get; set; }

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x0600787D RID: 30845 RVA: 0x002EB113 File Offset: 0x002E9313
		// (set) Token: 0x0600787E RID: 30846 RVA: 0x002EB11B File Offset: 0x002E931B
		public string diseaseName { get; set; }

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x0600787F RID: 30847 RVA: 0x002EB124 File Offset: 0x002E9324
		// (set) Token: 0x06007880 RID: 30848 RVA: 0x002EB12C File Offset: 0x002E932C
		public int diseaseCount { get; set; }

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x06007881 RID: 30849 RVA: 0x002EB135 File Offset: 0x002E9335
		// (set) Token: 0x06007882 RID: 30850 RVA: 0x002EB13D File Offset: 0x002E933D
		public Orientation rotationOrientation { get; set; }

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x06007883 RID: 30851 RVA: 0x002EB146 File Offset: 0x002E9346
		// (set) Token: 0x06007884 RID: 30852 RVA: 0x002EB14E File Offset: 0x002E934E
		public List<StorageItem> storage { get; set; }

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x06007885 RID: 30853 RVA: 0x002EB157 File Offset: 0x002E9357
		// (set) Token: 0x06007886 RID: 30854 RVA: 0x002EB15F File Offset: 0x002E935F
		public Prefab.Type type { get; set; }

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x06007887 RID: 30855 RVA: 0x002EB168 File Offset: 0x002E9368
		// (set) Token: 0x06007888 RID: 30856 RVA: 0x002EB170 File Offset: 0x002E9370
		public string facadeId { get; set; }

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x06007889 RID: 30857 RVA: 0x002EB179 File Offset: 0x002E9379
		// (set) Token: 0x0600788A RID: 30858 RVA: 0x002EB181 File Offset: 0x002E9381
		public int connections { get; set; }

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x0600788B RID: 30859 RVA: 0x002EB18A File Offset: 0x002E938A
		// (set) Token: 0x0600788C RID: 30860 RVA: 0x002EB192 File Offset: 0x002E9392
		public Rottable rottable { get; set; }

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x0600788D RID: 30861 RVA: 0x002EB19B File Offset: 0x002E939B
		// (set) Token: 0x0600788E RID: 30862 RVA: 0x002EB1A3 File Offset: 0x002E93A3
		public Prefab.template_amount_value[] amounts { get; set; }

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x0600788F RID: 30863 RVA: 0x002EB1AC File Offset: 0x002E93AC
		// (set) Token: 0x06007890 RID: 30864 RVA: 0x002EB1B4 File Offset: 0x002E93B4
		public Prefab.template_amount_value[] other_values { get; set; }

		// Token: 0x020020C9 RID: 8393
		public enum Type
		{
			// Token: 0x0400954E RID: 38222
			Building,
			// Token: 0x0400954F RID: 38223
			Ore,
			// Token: 0x04009550 RID: 38224
			Pickupable,
			// Token: 0x04009551 RID: 38225
			Other
		}

		// Token: 0x020020CA RID: 8394
		[Serializable]
		public class template_amount_value
		{
			// Token: 0x0600B730 RID: 46896 RVA: 0x003E40DA File Offset: 0x003E22DA
			public template_amount_value()
			{
			}

			// Token: 0x0600B731 RID: 46897 RVA: 0x003E40E2 File Offset: 0x003E22E2
			public template_amount_value(string id, float value)
			{
				this.id = id;
				this.value = value;
			}

			// Token: 0x17000CB3 RID: 3251
			// (get) Token: 0x0600B732 RID: 46898 RVA: 0x003E40F8 File Offset: 0x003E22F8
			// (set) Token: 0x0600B733 RID: 46899 RVA: 0x003E4100 File Offset: 0x003E2300
			public string id { get; set; }

			// Token: 0x17000CB4 RID: 3252
			// (get) Token: 0x0600B734 RID: 46900 RVA: 0x003E4109 File Offset: 0x003E2309
			// (set) Token: 0x0600B735 RID: 46901 RVA: 0x003E4111 File Offset: 0x003E2311
			public float value { get; set; }
		}
	}
}
