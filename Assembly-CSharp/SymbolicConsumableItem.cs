using System;

// Token: 0x02000D3C RID: 3388
public class SymbolicConsumableItem : IConsumableUIItem
{
	// Token: 0x0600689D RID: 26781 RVA: 0x002784C8 File Offset: 0x002766C8
	public SymbolicConsumableItem(string id, string name, int majorOrder, int minorOrder, bool display, string overrideSpriteName, Func<bool> revealTest)
	{
		this.id = id;
		this.name = name;
		this.majorOrder = majorOrder;
		this.minorOrder = minorOrder;
		this.display = display;
		this.overrideSpriteName = overrideSpriteName;
		this.revealTest = revealTest;
	}

	// Token: 0x17000775 RID: 1909
	// (get) Token: 0x0600689E RID: 26782 RVA: 0x00278505 File Offset: 0x00276705
	string IConsumableUIItem.ConsumableId
	{
		get
		{
			return this.id;
		}
	}

	// Token: 0x17000776 RID: 1910
	// (get) Token: 0x0600689F RID: 26783 RVA: 0x0027850D File Offset: 0x0027670D
	string IConsumableUIItem.ConsumableName
	{
		get
		{
			return this.name;
		}
	}

	// Token: 0x17000777 RID: 1911
	// (get) Token: 0x060068A0 RID: 26784 RVA: 0x00278515 File Offset: 0x00276715
	int IConsumableUIItem.MajorOrder
	{
		get
		{
			return this.majorOrder;
		}
	}

	// Token: 0x17000778 RID: 1912
	// (get) Token: 0x060068A1 RID: 26785 RVA: 0x0027851D File Offset: 0x0027671D
	int IConsumableUIItem.MinorOrder
	{
		get
		{
			return this.minorOrder;
		}
	}

	// Token: 0x17000779 RID: 1913
	// (get) Token: 0x060068A2 RID: 26786 RVA: 0x00278525 File Offset: 0x00276725
	bool IConsumableUIItem.Display
	{
		get
		{
			return this.display;
		}
	}

	// Token: 0x060068A3 RID: 26787 RVA: 0x0027852D File Offset: 0x0027672D
	string IConsumableUIItem.OverrideSpriteName()
	{
		return this.overrideSpriteName;
	}

	// Token: 0x060068A4 RID: 26788 RVA: 0x00278535 File Offset: 0x00276735
	bool IConsumableUIItem.RevealTest()
	{
		return this.revealTest();
	}

	// Token: 0x040047BD RID: 18365
	private string id;

	// Token: 0x040047BE RID: 18366
	private string name;

	// Token: 0x040047BF RID: 18367
	private int majorOrder;

	// Token: 0x040047C0 RID: 18368
	private int minorOrder;

	// Token: 0x040047C1 RID: 18369
	private bool display;

	// Token: 0x040047C2 RID: 18370
	private string overrideSpriteName;

	// Token: 0x040047C3 RID: 18371
	private Func<bool> revealTest;
}
