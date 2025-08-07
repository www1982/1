using System;

namespace Klei.AI
{
	// Token: 0x0200100C RID: 4108
	public class TraitGroup : ModifierGroup<Trait>
	{
		// Token: 0x06007EB0 RID: 32432 RVA: 0x00329F0D File Offset: 0x0032810D
		public TraitGroup(string id, string name, bool is_spawn_trait)
			: base(id, name)
		{
			this.IsSpawnTrait = is_spawn_trait;
		}

		// Token: 0x04005F78 RID: 24440
		public bool IsSpawnTrait;
	}
}
