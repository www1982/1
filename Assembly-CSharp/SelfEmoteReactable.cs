using System;
using UnityEngine;

// Token: 0x020004ED RID: 1261
public class SelfEmoteReactable : EmoteReactable
{
	// Token: 0x06001B0B RID: 6923 RVA: 0x000952C4 File Offset: 0x000934C4
	public SelfEmoteReactable(GameObject gameObject, HashedString id, ChoreType chore_type, float globalCooldown = 0f, float localCooldown = 20f, float lifeSpan = float.PositiveInfinity, float max_initial_delay = 0f)
		: base(gameObject, id, chore_type, 3, 3, globalCooldown, localCooldown, lifeSpan, max_initial_delay)
	{
	}

	// Token: 0x06001B0C RID: 6924 RVA: 0x000952E4 File Offset: 0x000934E4
	public override bool InternalCanBegin(GameObject reactor, Navigator.ActiveTransition transition)
	{
		if (reactor != this.gameObject)
		{
			return false;
		}
		Navigator component = reactor.GetComponent<Navigator>();
		return !(component == null) && component.IsMoving();
	}

	// Token: 0x06001B0D RID: 6925 RVA: 0x0009531C File Offset: 0x0009351C
	public void PairEmote(EmoteChore emoteChore)
	{
		this.chore = emoteChore;
	}

	// Token: 0x06001B0E RID: 6926 RVA: 0x00095328 File Offset: 0x00093528
	protected override void InternalEnd()
	{
		if (this.chore != null && this.chore.driver != null)
		{
			this.chore.PairReactable(null);
			this.chore.Cancel("Reactable ended");
			this.chore = null;
		}
		base.InternalEnd();
	}

	// Token: 0x04000FF8 RID: 4088
	private EmoteChore chore;
}
