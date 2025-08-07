using System;
using Klei.AI;
using UnityEngine;

// Token: 0x020005AF RID: 1455
[AddComponentMenu("KMonoBehaviour/scripts/EffectArea")]
public class EffectArea : KMonoBehaviour
{
	// Token: 0x0600218C RID: 8588 RVA: 0x000C1E1D File Offset: 0x000C001D
	protected override void OnPrefabInit()
	{
		this.Effect = Db.Get().effects.Get(this.EffectName);
	}

	// Token: 0x0600218D RID: 8589 RVA: 0x000C1E3C File Offset: 0x000C003C
	private void Update()
	{
		int num = 0;
		int num2 = 0;
		Grid.PosToXY(base.transform.GetPosition(), out num, out num2);
		foreach (MinionIdentity minionIdentity in Components.MinionIdentities.Items)
		{
			int num3 = 0;
			int num4 = 0;
			Grid.PosToXY(minionIdentity.transform.GetPosition(), out num3, out num4);
			if (Math.Abs(num3 - num) <= this.Area && Math.Abs(num4 - num2) <= this.Area)
			{
				minionIdentity.GetComponent<Effects>().Add(this.Effect, true);
			}
		}
	}

	// Token: 0x0400138B RID: 5003
	public string EffectName;

	// Token: 0x0400138C RID: 5004
	public int Area;

	// Token: 0x0400138D RID: 5005
	private Effect Effect;
}
