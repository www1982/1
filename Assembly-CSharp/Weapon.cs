using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000831 RID: 2097
[AddComponentMenu("KMonoBehaviour/scripts/Weapon")]
public class Weapon : KMonoBehaviour
{
	// Token: 0x0600396B RID: 14699 RVA: 0x0013E268 File Offset: 0x0013C468
	public void Configure(float base_damage_min, float base_damage_max, AttackProperties.DamageType attackType = AttackProperties.DamageType.Standard, AttackProperties.TargetType targetType = AttackProperties.TargetType.Single, int maxHits = 1, float aoeRadius = 0f)
	{
		this.properties = new AttackProperties();
		this.properties.base_damage_min = base_damage_min;
		this.properties.base_damage_max = base_damage_max;
		this.properties.maxHits = maxHits;
		this.properties.damageType = attackType;
		this.properties.aoe_radius = aoeRadius;
		this.properties.attacker = this;
	}

	// Token: 0x0600396C RID: 14700 RVA: 0x0013E2CA File Offset: 0x0013C4CA
	public void AddEffect(string effectID = "WasAttacked", float probability = 1f)
	{
		if (this.properties.effects == null)
		{
			this.properties.effects = new List<AttackEffect>();
		}
		this.properties.effects.Add(new AttackEffect(effectID, probability));
	}

	// Token: 0x0600396D RID: 14701 RVA: 0x0013E300 File Offset: 0x0013C500
	public int AttackArea(Vector3 centerPoint)
	{
		Vector3 vector = Vector3.zero;
		this.alignment = base.GetComponent<FactionAlignment>();
		if (this.alignment == null)
		{
			return 0;
		}
		List<GameObject> list = new List<GameObject>();
		foreach (Health health in Components.Health.Items)
		{
			if (!(health.gameObject == base.gameObject) && !health.IsDefeated())
			{
				FactionAlignment component = health.GetComponent<FactionAlignment>();
				if (!(component == null) && component.IsAlignmentActive() && FactionManager.Instance.GetDisposition(this.alignment.Alignment, component.Alignment) == FactionManager.Disposition.Attack)
				{
					vector = health.transform.GetPosition();
					vector.z = centerPoint.z;
					if (Vector3.Distance(centerPoint, vector) <= this.properties.aoe_radius)
					{
						list.Add(health.gameObject);
					}
				}
			}
		}
		this.AttackTargets(list.ToArray());
		return list.Count;
	}

	// Token: 0x0600396E RID: 14702 RVA: 0x0013E428 File Offset: 0x0013C628
	public void AttackTarget(GameObject target)
	{
		this.AttackTargets(new GameObject[] { target });
	}

	// Token: 0x0600396F RID: 14703 RVA: 0x0013E43A File Offset: 0x0013C63A
	public void AttackTargets(GameObject[] targets)
	{
		if (this.properties == null)
		{
			global::Debug.LogWarning(string.Format("Attack properties not configured. {0} cannot attack with weapon.", base.gameObject.name));
			return;
		}
		new Attack(this.properties, targets);
	}

	// Token: 0x06003970 RID: 14704 RVA: 0x0013E46C File Offset: 0x0013C66C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.properties.attacker = this;
	}

	// Token: 0x040022B9 RID: 8889
	[MyCmpReq]
	private FactionAlignment alignment;

	// Token: 0x040022BA RID: 8890
	public AttackProperties properties;
}
