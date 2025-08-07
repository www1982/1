using System;
using System.Collections.Generic;
using Klei.AI;

namespace Database
{
	// Token: 0x02000F63 RID: 3939
	public class SkillGroup : Resource, IListableOption
	{
		// Token: 0x06007B54 RID: 31572 RVA: 0x0030E4C9 File Offset: 0x0030C6C9
		string IListableOption.GetProperName()
		{
			return Strings.Get("STRINGS.DUPLICANTS.SKILLGROUPS." + this.Id.ToUpper() + ".NAME");
		}

		// Token: 0x06007B55 RID: 31573 RVA: 0x0030E4EF File Offset: 0x0030C6EF
		public SkillGroup(string id, string choreGroupID, string name, string icon, string archetype_icon)
			: base(id, name)
		{
			this.choreGroupID = choreGroupID;
			this.choreGroupIcon = icon;
			this.archetypeIcon = archetype_icon;
		}

		// Token: 0x04005A89 RID: 23177
		public string choreGroupID;

		// Token: 0x04005A8A RID: 23178
		public List<Klei.AI.Attribute> relevantAttributes;

		// Token: 0x04005A8B RID: 23179
		public List<string> requiredChoreGroups;

		// Token: 0x04005A8C RID: 23180
		public string choreGroupIcon;

		// Token: 0x04005A8D RID: 23181
		public string archetypeIcon;

		// Token: 0x04005A8E RID: 23182
		public bool allowAsAptitude = true;
	}
}
