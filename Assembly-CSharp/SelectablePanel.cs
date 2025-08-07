using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000DC4 RID: 3524
public class SelectablePanel : MonoBehaviour, IDeselectHandler, IEventSystemHandler
{
	// Token: 0x06006F2F RID: 28463 RVA: 0x002A56FD File Offset: 0x002A38FD
	public void OnDeselect(BaseEventData evt)
	{
		base.gameObject.SetActive(false);
	}
}
