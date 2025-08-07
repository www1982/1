using System;
using System.Collections.Generic;

// Token: 0x0200082B RID: 2091
[Serializable]
public class AttackProperties
{
	// Token: 0x04002293 RID: 8851
	public Weapon attacker;

	// Token: 0x04002294 RID: 8852
	public AttackProperties.DamageType damageType;

	// Token: 0x04002295 RID: 8853
	public AttackProperties.TargetType targetType;

	// Token: 0x04002296 RID: 8854
	public float base_damage_min;

	// Token: 0x04002297 RID: 8855
	public float base_damage_max;

	// Token: 0x04002298 RID: 8856
	public int maxHits;

	// Token: 0x04002299 RID: 8857
	public float aoe_radius = 2f;

	// Token: 0x0400229A RID: 8858
	public List<AttackEffect> effects;

	// Token: 0x0200179F RID: 6047
	public enum DamageType
	{
		// Token: 0x04007659 RID: 30297
		Standard
	}

	// Token: 0x020017A0 RID: 6048
	public enum TargetType
	{
		// Token: 0x0400765B RID: 30299
		Single,
		// Token: 0x0400765C RID: 30300
		AreaOfEffect
	}
}
