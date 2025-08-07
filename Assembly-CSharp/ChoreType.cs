using System;
using System.Collections.Generic;
using System.Diagnostics;

// Token: 0x02000655 RID: 1621
[DebuggerDisplay("{IdHash}")]
public class ChoreType : Resource
{
	// Token: 0x170001CF RID: 463
	// (get) Token: 0x06002798 RID: 10136 RVA: 0x000E1A94 File Offset: 0x000DFC94
	// (set) Token: 0x06002799 RID: 10137 RVA: 0x000E1A9C File Offset: 0x000DFC9C
	public Urge urge { get; private set; }

	// Token: 0x170001D0 RID: 464
	// (get) Token: 0x0600279A RID: 10138 RVA: 0x000E1AA5 File Offset: 0x000DFCA5
	// (set) Token: 0x0600279B RID: 10139 RVA: 0x000E1AAD File Offset: 0x000DFCAD
	public ChoreGroup[] groups { get; private set; }

	// Token: 0x170001D1 RID: 465
	// (get) Token: 0x0600279C RID: 10140 RVA: 0x000E1AB6 File Offset: 0x000DFCB6
	// (set) Token: 0x0600279D RID: 10141 RVA: 0x000E1ABE File Offset: 0x000DFCBE
	public int priority { get; private set; }

	// Token: 0x170001D2 RID: 466
	// (get) Token: 0x0600279E RID: 10142 RVA: 0x000E1AC7 File Offset: 0x000DFCC7
	// (set) Token: 0x0600279F RID: 10143 RVA: 0x000E1ACF File Offset: 0x000DFCCF
	public int interruptPriority { get; set; }

	// Token: 0x170001D3 RID: 467
	// (get) Token: 0x060027A0 RID: 10144 RVA: 0x000E1AD8 File Offset: 0x000DFCD8
	// (set) Token: 0x060027A1 RID: 10145 RVA: 0x000E1AE0 File Offset: 0x000DFCE0
	public int explicitPriority { get; private set; }

	// Token: 0x060027A2 RID: 10146 RVA: 0x000E1AE9 File Offset: 0x000DFCE9
	private string ResolveStringCallback(string str, object data)
	{
		return ((Chore)data).ResolveString(str);
	}

	// Token: 0x060027A3 RID: 10147 RVA: 0x000E1AF8 File Offset: 0x000DFCF8
	public ChoreType(string id, ResourceSet parent, string[] chore_groups, string urge, string name, string status_message, string tooltip, IEnumerable<Tag> interrupt_exclusion, int implicit_priority, int explicit_priority)
		: base(id, parent, name)
	{
		this.statusItem = new StatusItem(id, status_message, tooltip, "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, 129022, true, null);
		this.statusItem.resolveStringCallback = new Func<string, object, string>(this.ResolveStringCallback);
		this.tags.Add(TagManager.Create(id));
		this.interruptExclusion = new HashSet<Tag>(interrupt_exclusion);
		Db.Get().DuplicantStatusItems.Add(this.statusItem);
		List<ChoreGroup> list = new List<ChoreGroup>();
		for (int i = 0; i < chore_groups.Length; i++)
		{
			ChoreGroup choreGroup = Db.Get().ChoreGroups.TryGet(chore_groups[i]);
			if (choreGroup != null)
			{
				if (!choreGroup.choreTypes.Contains(this))
				{
					choreGroup.choreTypes.Add(this);
				}
				list.Add(choreGroup);
			}
		}
		this.groups = list.ToArray();
		if (!string.IsNullOrEmpty(urge))
		{
			this.urge = Db.Get().Urges.Get(urge);
		}
		this.priority = implicit_priority;
		this.explicitPriority = explicit_priority;
	}

	// Token: 0x04001740 RID: 5952
	public StatusItem statusItem;

	// Token: 0x04001745 RID: 5957
	public HashSet<Tag> tags = new HashSet<Tag>();

	// Token: 0x04001746 RID: 5958
	public HashSet<Tag> interruptExclusion;

	// Token: 0x04001748 RID: 5960
	public string reportName;
}
