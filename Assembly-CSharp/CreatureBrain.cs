using System;

// Token: 0x02000466 RID: 1126
public class CreatureBrain : Brain
{
	// Token: 0x060017B6 RID: 6070 RVA: 0x00083660 File Offset: 0x00081860
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		Navigator component = base.GetComponent<Navigator>();
		if (component != null)
		{
			component.SetAbilities(new CreaturePathFinderAbilities(component));
		}
	}

	// Token: 0x04000DB4 RID: 3508
	public string symbolPrefix;

	// Token: 0x04000DB5 RID: 3509
	public Tag species;
}
