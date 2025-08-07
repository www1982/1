using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Database
{
	// Token: 0x02000F0C RID: 3852
	[DebuggerDisplay("{Id}")]
	public class SpaceDestinationType : Resource
	{
		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x060079E2 RID: 31202 RVA: 0x003024C8 File Offset: 0x003006C8
		// (set) Token: 0x060079E3 RID: 31203 RVA: 0x003024D0 File Offset: 0x003006D0
		public int maxiumMass { get; private set; }

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x060079E4 RID: 31204 RVA: 0x003024D9 File Offset: 0x003006D9
		// (set) Token: 0x060079E5 RID: 31205 RVA: 0x003024E1 File Offset: 0x003006E1
		public int minimumMass { get; private set; }

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x060079E6 RID: 31206 RVA: 0x003024EA File Offset: 0x003006EA
		public float replishmentPerCycle
		{
			get
			{
				return 1000f / (float)this.cyclesToRecover;
			}
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x060079E7 RID: 31207 RVA: 0x003024F9 File Offset: 0x003006F9
		public float replishmentPerSim1000ms
		{
			get
			{
				return 1000f / ((float)this.cyclesToRecover * 600f);
			}
		}

		// Token: 0x060079E8 RID: 31208 RVA: 0x00302510 File Offset: 0x00300710
		public SpaceDestinationType(string id, ResourceSet parent, string name, string description, int iconSize, string spriteName, Dictionary<SimHashes, MathUtil.MinMax> elementTable, Dictionary<string, int> recoverableEntities = null, ArtifactDropRate artifactDropRate = null, int max = 64000000, int min = 63994000, int cycles = 6, bool visitable = true)
			: base(id, parent, name)
		{
			this.typeName = name;
			this.description = description;
			this.iconSize = iconSize;
			this.spriteName = spriteName;
			this.elementTable = elementTable;
			this.recoverableEntities = recoverableEntities;
			this.artifactDropTable = artifactDropRate;
			this.maxiumMass = max;
			this.minimumMass = min;
			this.cyclesToRecover = cycles;
			this.visitable = visitable;
		}

		// Token: 0x040058EA RID: 22762
		public const float MASS_TO_RECOVER = 1000f;

		// Token: 0x040058EB RID: 22763
		public string typeName;

		// Token: 0x040058EC RID: 22764
		public string description;

		// Token: 0x040058ED RID: 22765
		public int iconSize = 128;

		// Token: 0x040058EE RID: 22766
		public string spriteName;

		// Token: 0x040058EF RID: 22767
		public Dictionary<SimHashes, MathUtil.MinMax> elementTable;

		// Token: 0x040058F0 RID: 22768
		public Dictionary<string, int> recoverableEntities;

		// Token: 0x040058F1 RID: 22769
		public ArtifactDropRate artifactDropTable;

		// Token: 0x040058F2 RID: 22770
		public bool visitable;

		// Token: 0x040058F5 RID: 22773
		public int cyclesToRecover;
	}
}
