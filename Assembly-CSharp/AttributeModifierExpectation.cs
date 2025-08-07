using System;
using Klei.AI;
using UnityEngine;

// Token: 0x02000911 RID: 2321
public class AttributeModifierExpectation : Expectation
{
	// Token: 0x0600409F RID: 16543 RVA: 0x0016A488 File Offset: 0x00168688
	public AttributeModifierExpectation(string id, string name, string description, AttributeModifier modifier, Sprite icon)
		: base(id, name, description, delegate(MinionResume resume)
		{
			resume.GetAttributes().Get(modifier.AttributeId).Add(modifier);
		}, delegate(MinionResume resume)
		{
			resume.GetAttributes().Get(modifier.AttributeId).Remove(modifier);
		})
	{
		this.modifier = modifier;
		this.icon = icon;
	}

	// Token: 0x04002853 RID: 10323
	public AttributeModifier modifier;

	// Token: 0x04002854 RID: 10324
	public Sprite icon;
}
