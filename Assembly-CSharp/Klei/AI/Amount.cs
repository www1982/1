using System;
using System.Diagnostics;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000FCF RID: 4047
	[DebuggerDisplay("{Id}")]
	public class Amount : Resource
	{
		// Token: 0x06007D0A RID: 32010 RVA: 0x003214A4 File Offset: 0x0031F6A4
		public Amount(string id, string name, string description, Attribute min_attribute, Attribute max_attribute, Attribute delta_attribute, bool show_max, Units units, float visual_delta_threshold, bool show_in_ui, string uiSprite = null, string thoughtSprite = null)
		{
			this.Id = id;
			this.Name = name;
			this.description = description;
			this.minAttribute = min_attribute;
			this.maxAttribute = max_attribute;
			this.deltaAttribute = delta_attribute;
			this.showMax = show_max;
			this.units = units;
			this.visualDeltaThreshold = visual_delta_threshold;
			this.showInUI = show_in_ui;
			this.uiSprite = uiSprite;
			this.thoughtSprite = thoughtSprite;
		}

		// Token: 0x06007D0B RID: 32011 RVA: 0x00321514 File Offset: 0x0031F714
		public void SetDisplayer(IAmountDisplayer displayer)
		{
			this.displayer = displayer;
			this.minAttribute.SetFormatter(displayer.Formatter);
			this.maxAttribute.SetFormatter(displayer.Formatter);
			this.deltaAttribute.SetFormatter(displayer.Formatter);
		}

		// Token: 0x06007D0C RID: 32012 RVA: 0x00321550 File Offset: 0x0031F750
		public AmountInstance Lookup(Component cmp)
		{
			return this.Lookup(cmp.gameObject);
		}

		// Token: 0x06007D0D RID: 32013 RVA: 0x00321560 File Offset: 0x0031F760
		public AmountInstance Lookup(GameObject go)
		{
			Amounts amounts = go.GetAmounts();
			if (amounts != null)
			{
				return amounts.Get(this);
			}
			return null;
		}

		// Token: 0x06007D0E RID: 32014 RVA: 0x00321580 File Offset: 0x0031F780
		public void Copy(GameObject to, GameObject from)
		{
			AmountInstance amountInstance = this.Lookup(to);
			AmountInstance amountInstance2 = this.Lookup(from);
			amountInstance.value = amountInstance2.value;
		}

		// Token: 0x06007D0F RID: 32015 RVA: 0x003215A7 File Offset: 0x0031F7A7
		public string GetValueString(AmountInstance instance)
		{
			return this.displayer.GetValueString(this, instance);
		}

		// Token: 0x06007D10 RID: 32016 RVA: 0x003215B6 File Offset: 0x0031F7B6
		public string GetDescription(AmountInstance instance)
		{
			return this.displayer.GetDescription(this, instance);
		}

		// Token: 0x06007D11 RID: 32017 RVA: 0x003215C5 File Offset: 0x0031F7C5
		public string GetTooltip(AmountInstance instance)
		{
			return this.displayer.GetTooltip(this, instance);
		}

		// Token: 0x06007D12 RID: 32018 RVA: 0x003215D4 File Offset: 0x0031F7D4
		public void DebugSetValue(AmountInstance instance, float value)
		{
			if (this.debugSetValue != null)
			{
				this.debugSetValue(instance, value);
				return;
			}
			instance.SetValue(value);
		}

		// Token: 0x04005E6F RID: 24175
		public string description;

		// Token: 0x04005E70 RID: 24176
		public bool showMax;

		// Token: 0x04005E71 RID: 24177
		public Units units;

		// Token: 0x04005E72 RID: 24178
		public float visualDeltaThreshold;

		// Token: 0x04005E73 RID: 24179
		public Attribute minAttribute;

		// Token: 0x04005E74 RID: 24180
		public Attribute maxAttribute;

		// Token: 0x04005E75 RID: 24181
		public Attribute deltaAttribute;

		// Token: 0x04005E76 RID: 24182
		public Action<AmountInstance, float> debugSetValue;

		// Token: 0x04005E77 RID: 24183
		public bool showInUI;

		// Token: 0x04005E78 RID: 24184
		public string uiSprite;

		// Token: 0x04005E79 RID: 24185
		public string thoughtSprite;

		// Token: 0x04005E7A RID: 24186
		public IAmountDisplayer displayer;
	}
}
