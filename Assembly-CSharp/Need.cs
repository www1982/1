using System;
using Klei.AI;

// Token: 0x02000A27 RID: 2599
public abstract class Need : KMonoBehaviour
{
	// Token: 0x1700052B RID: 1323
	// (get) Token: 0x06004B87 RID: 19335 RVA: 0x001B67C7 File Offset: 0x001B49C7
	// (set) Token: 0x06004B88 RID: 19336 RVA: 0x001B67CF File Offset: 0x001B49CF
	public string Name { get; protected set; }

	// Token: 0x1700052C RID: 1324
	// (get) Token: 0x06004B89 RID: 19337 RVA: 0x001B67D8 File Offset: 0x001B49D8
	// (set) Token: 0x06004B8A RID: 19338 RVA: 0x001B67E0 File Offset: 0x001B49E0
	public string ExpectationTooltip { get; protected set; }

	// Token: 0x1700052D RID: 1325
	// (get) Token: 0x06004B8B RID: 19339 RVA: 0x001B67E9 File Offset: 0x001B49E9
	// (set) Token: 0x06004B8C RID: 19340 RVA: 0x001B67F1 File Offset: 0x001B49F1
	public string Tooltip { get; protected set; }

	// Token: 0x06004B8D RID: 19341 RVA: 0x001B67FA File Offset: 0x001B49FA
	public Klei.AI.Attribute GetExpectationAttribute()
	{
		return this.expectationAttribute.Attribute;
	}

	// Token: 0x06004B8E RID: 19342 RVA: 0x001B6807 File Offset: 0x001B4A07
	protected void SetModifier(Need.ModifierType modifier)
	{
		if (this.currentStressModifier != modifier)
		{
			if (this.currentStressModifier != null)
			{
				this.UnapplyModifier(this.currentStressModifier);
			}
			if (modifier != null)
			{
				this.ApplyModifier(modifier);
			}
			this.currentStressModifier = modifier;
		}
	}

	// Token: 0x06004B8F RID: 19343 RVA: 0x001B6838 File Offset: 0x001B4A38
	private void ApplyModifier(Need.ModifierType modifier)
	{
		if (modifier.modifier != null)
		{
			this.GetAttributes().Add(modifier.modifier);
		}
		if (modifier.statusItem != null)
		{
			base.GetComponent<KSelectable>().AddStatusItem(modifier.statusItem, null);
		}
		if (modifier.thought != null)
		{
			this.GetSMI<ThoughtGraph.Instance>().AddThought(modifier.thought);
		}
	}

	// Token: 0x06004B90 RID: 19344 RVA: 0x001B6894 File Offset: 0x001B4A94
	private void UnapplyModifier(Need.ModifierType modifier)
	{
		if (modifier.modifier != null)
		{
			this.GetAttributes().Remove(modifier.modifier);
		}
		if (modifier.statusItem != null)
		{
			base.GetComponent<KSelectable>().RemoveStatusItem(modifier.statusItem, false);
		}
		if (modifier.thought != null)
		{
			this.GetSMI<ThoughtGraph.Instance>().RemoveThought(modifier.thought);
		}
	}

	// Token: 0x04003229 RID: 12841
	protected AttributeInstance expectationAttribute;

	// Token: 0x0400322A RID: 12842
	protected Need.ModifierType stressBonus;

	// Token: 0x0400322B RID: 12843
	protected Need.ModifierType stressNeutral;

	// Token: 0x0400322C RID: 12844
	protected Need.ModifierType stressPenalty;

	// Token: 0x0400322D RID: 12845
	protected Need.ModifierType currentStressModifier;

	// Token: 0x02001AE5 RID: 6885
	protected class ModifierType
	{
		// Token: 0x04008156 RID: 33110
		public AttributeModifier modifier;

		// Token: 0x04008157 RID: 33111
		public StatusItem statusItem;

		// Token: 0x04008158 RID: 33112
		public Thought thought;
	}
}
