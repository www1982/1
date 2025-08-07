using System;

// Token: 0x020008AB RID: 2219
public class DiagnosticCriterion
{
	// Token: 0x1700044E RID: 1102
	// (get) Token: 0x06003DAB RID: 15787 RVA: 0x0015837B File Offset: 0x0015657B
	// (set) Token: 0x06003DAC RID: 15788 RVA: 0x00158383 File Offset: 0x00156583
	public string id { get; private set; }

	// Token: 0x1700044F RID: 1103
	// (get) Token: 0x06003DAD RID: 15789 RVA: 0x0015838C File Offset: 0x0015658C
	// (set) Token: 0x06003DAE RID: 15790 RVA: 0x00158394 File Offset: 0x00156594
	public string name { get; private set; }

	// Token: 0x06003DAF RID: 15791 RVA: 0x0015839D File Offset: 0x0015659D
	public DiagnosticCriterion(string name, Func<ColonyDiagnostic.DiagnosticResult> action)
	{
		this.name = name;
		this.evaluateAction = action;
	}

	// Token: 0x06003DB0 RID: 15792 RVA: 0x001583B3 File Offset: 0x001565B3
	public void SetID(string id)
	{
		this.id = id;
	}

	// Token: 0x06003DB1 RID: 15793 RVA: 0x001583BC File Offset: 0x001565BC
	public ColonyDiagnostic.DiagnosticResult Evaluate()
	{
		return this.evaluateAction();
	}

	// Token: 0x04002616 RID: 9750
	private Func<ColonyDiagnostic.DiagnosticResult> evaluateAction;
}
