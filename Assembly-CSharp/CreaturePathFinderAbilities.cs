using System;
using Klei.AI;

// Token: 0x02000467 RID: 1127
public class CreaturePathFinderAbilities : PathFinderAbilities
{
	// Token: 0x060017B8 RID: 6072 RVA: 0x00083697 File Offset: 0x00081897
	public CreaturePathFinderAbilities(Navigator navigator)
		: base(navigator)
	{
	}

	// Token: 0x060017B9 RID: 6073 RVA: 0x000836A0 File Offset: 0x000818A0
	protected override void Refresh(Navigator navigator)
	{
		if (PathFinder.IsSubmerged(Grid.PosToCell(navigator)))
		{
			this.canTraverseSubmered = true;
			return;
		}
		AttributeInstance attributeInstance = Db.Get().Attributes.MaxUnderwaterTravelCost.Lookup(navigator);
		this.canTraverseSubmered = attributeInstance == null;
	}

	// Token: 0x060017BA RID: 6074 RVA: 0x000836E2 File Offset: 0x000818E2
	public override bool TraversePath(ref PathFinder.PotentialPath path, int from_cell, NavType from_nav_type, int cost, int transition_id, bool submerged)
	{
		return !submerged || this.canTraverseSubmered;
	}

	// Token: 0x04000DB6 RID: 3510
	public bool canTraverseSubmered;
}
