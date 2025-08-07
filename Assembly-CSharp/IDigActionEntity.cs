using System;

// Token: 0x02000BC3 RID: 3011
public interface IDigActionEntity
{
	// Token: 0x06005A2F RID: 23087
	void Dig();

	// Token: 0x06005A30 RID: 23088
	void MarkForDig(bool instantOnDebug = true);
}
