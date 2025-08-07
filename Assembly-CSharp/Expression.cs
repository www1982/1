using System;
using System.Diagnostics;

// Token: 0x02000913 RID: 2323
[DebuggerDisplay("{face.hash} {priority}")]
public class Expression : Resource
{
	// Token: 0x060040A3 RID: 16547 RVA: 0x0016A6C3 File Offset: 0x001688C3
	public Expression(string id, ResourceSet parent, Face face)
		: base(id, parent, null)
	{
		this.face = face;
	}

	// Token: 0x04002856 RID: 10326
	public Face face;

	// Token: 0x04002857 RID: 10327
	public int priority;
}
