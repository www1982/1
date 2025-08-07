using System;
using System.Collections.Generic;

// Token: 0x02000808 RID: 2056
public class Chatty : KMonoBehaviour, ISimEveryTick
{
	// Token: 0x060037F1 RID: 14321 RVA: 0x0013676E File Offset: 0x0013496E
	protected override void OnPrefabInit()
	{
		base.GetComponent<KPrefabID>().AddTag(GameTags.AlwaysConverse, false);
		base.Subscribe(-594200555, new Action<object>(this.OnStartedTalking));
		this.identity = base.GetComponent<MinionIdentity>();
	}

	// Token: 0x060037F2 RID: 14322 RVA: 0x001367A8 File Offset: 0x001349A8
	private void OnStartedTalking(object data)
	{
		MinionIdentity minionIdentity = data as MinionIdentity;
		if (minionIdentity == null)
		{
			return;
		}
		this.conversationPartners.Add(minionIdentity);
	}

	// Token: 0x060037F3 RID: 14323 RVA: 0x001367D4 File Offset: 0x001349D4
	public void SimEveryTick(float dt)
	{
		if (this.conversationPartners.Count == 0)
		{
			return;
		}
		for (int i = this.conversationPartners.Count - 1; i >= 0; i--)
		{
			MinionIdentity minionIdentity = this.conversationPartners[i];
			this.conversationPartners.RemoveAt(i);
			if (!(minionIdentity == this.identity))
			{
				minionIdentity.AddTag(GameTags.PleasantConversation);
			}
		}
		base.gameObject.AddTag(GameTags.PleasantConversation);
	}

	// Token: 0x0400220B RID: 8715
	private MinionIdentity identity;

	// Token: 0x0400220C RID: 8716
	private List<MinionIdentity> conversationPartners = new List<MinionIdentity>();
}
