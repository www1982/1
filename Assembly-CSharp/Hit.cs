using System;
using Klei.AI;
using UnityEngine;

// Token: 0x02000830 RID: 2096
public class Hit
{
	// Token: 0x06003968 RID: 14696 RVA: 0x0013E128 File Offset: 0x0013C328
	public Hit(AttackProperties properties, GameObject target)
	{
		this.properties = properties;
		this.target = target;
		this.DeliverHit();
	}

	// Token: 0x06003969 RID: 14697 RVA: 0x0013E144 File Offset: 0x0013C344
	private float rollDamage()
	{
		return (float)Mathf.RoundToInt(global::UnityEngine.Random.Range(this.properties.base_damage_min, this.properties.base_damage_max));
	}

	// Token: 0x0600396A RID: 14698 RVA: 0x0013E168 File Offset: 0x0013C368
	private void DeliverHit()
	{
		Health component = this.target.GetComponent<Health>();
		if (!component)
		{
			return;
		}
		this.target.Trigger(-787691065, this.properties.attacker.GetComponent<FactionAlignment>());
		float num = this.rollDamage();
		AttackableBase component2 = this.target.GetComponent<AttackableBase>();
		num *= 1f + component2.GetDamageMultiplier();
		component.Damage(num);
		if (this.properties.effects == null)
		{
			return;
		}
		Effects component3 = this.target.GetComponent<Effects>();
		if (component3)
		{
			foreach (AttackEffect attackEffect in this.properties.effects)
			{
				if (global::UnityEngine.Random.Range(0f, 100f) < attackEffect.effectProbability * 100f)
				{
					component3.Add(attackEffect.effectID, true);
				}
			}
		}
	}

	// Token: 0x040022B7 RID: 8887
	private AttackProperties properties;

	// Token: 0x040022B8 RID: 8888
	private GameObject target;
}
