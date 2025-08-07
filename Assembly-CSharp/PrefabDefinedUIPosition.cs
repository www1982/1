using System;
using UnityEngine;

// Token: 0x02000D01 RID: 3329
public class PrefabDefinedUIPosition
{
	// Token: 0x060066D1 RID: 26321 RVA: 0x0026D925 File Offset: 0x0026BB25
	public void SetOn(GameObject gameObject)
	{
		if (this.position.HasValue)
		{
			gameObject.rectTransform().anchoredPosition = this.position.Value;
			return;
		}
		this.position = gameObject.rectTransform().anchoredPosition;
	}

	// Token: 0x060066D2 RID: 26322 RVA: 0x0026D961 File Offset: 0x0026BB61
	public void SetOn(Component component)
	{
		if (this.position.HasValue)
		{
			component.rectTransform().anchoredPosition = this.position.Value;
			return;
		}
		this.position = component.rectTransform().anchoredPosition;
	}

	// Token: 0x04004681 RID: 18049
	private Option<Vector2> position;
}
