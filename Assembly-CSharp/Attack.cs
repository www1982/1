using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200082A RID: 2090
public class Attack
{
	// Token: 0x06003942 RID: 14658 RVA: 0x0013D7D5 File Offset: 0x0013B9D5
	public Attack(AttackProperties properties, GameObject[] targets)
	{
		this.properties = properties;
		this.targets = targets;
		this.RollHits();
	}

	// Token: 0x06003943 RID: 14659 RVA: 0x0013D7F4 File Offset: 0x0013B9F4
	private void RollHits()
	{
		int num = 0;
		while (num < this.targets.Length && num <= this.properties.maxHits - 1)
		{
			if (this.targets[num] != null)
			{
				new Hit(this.properties, this.targets[num]);
			}
			num++;
		}
	}

	// Token: 0x04002290 RID: 8848
	private AttackProperties properties;

	// Token: 0x04002291 RID: 8849
	private GameObject[] targets;

	// Token: 0x04002292 RID: 8850
	public List<Hit> Hits;
}
