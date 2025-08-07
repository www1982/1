using System;
using UnityEngine;

// Token: 0x0200098E RID: 2446
public class StampToolPreviewContext
{
	// Token: 0x04002EF7 RID: 12023
	public Transform previewParent;

	// Token: 0x04002EF8 RID: 12024
	public InterfaceTool tool;

	// Token: 0x04002EF9 RID: 12025
	public TemplateContainer stampTemplate;

	// Token: 0x04002EFA RID: 12026
	public global::System.Action frameAfterSetupFn;

	// Token: 0x04002EFB RID: 12027
	public Action<int> refreshFn;

	// Token: 0x04002EFC RID: 12028
	public global::System.Action onPlaceFn;

	// Token: 0x04002EFD RID: 12029
	public Action<string> onErrorChangeFn;

	// Token: 0x04002EFE RID: 12030
	public global::System.Action cleanupFn;
}
