using System;
using System.Diagnostics;
using UnityEngine;

// Token: 0x020006A8 RID: 1704
[DebuggerDisplay("{name}")]
[Serializable]
public class TintedSprite : ISerializationCallbackReceiver
{
	// Token: 0x0600298A RID: 10634 RVA: 0x000F1626 File Offset: 0x000EF826
	public void OnAfterDeserialize()
	{
	}

	// Token: 0x0600298B RID: 10635 RVA: 0x000F1628 File Offset: 0x000EF828
	public void OnBeforeSerialize()
	{
		if (this.sprite != null)
		{
			this.name = this.sprite.name;
		}
	}

	// Token: 0x0400189D RID: 6301
	[ReadOnly]
	public string name;

	// Token: 0x0400189E RID: 6302
	public Sprite sprite;

	// Token: 0x0400189F RID: 6303
	public Color color;
}
