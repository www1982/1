using System;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000DBB RID: 3515
[AddComponentMenu("KMonoBehaviour/scripts/ScheduleBlockPainter")]
public class ScheduleBlockPainter : KMonoBehaviour, IPointerDownHandler, IEventSystemHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	// Token: 0x06006EDD RID: 28381 RVA: 0x002A3554 File Offset: 0x002A1754
	public void SetEntry(ScheduleScreenEntry entry)
	{
		this.entry = entry;
	}

	// Token: 0x06006EDE RID: 28382 RVA: 0x002A355D File Offset: 0x002A175D
	public void OnBeginDrag(PointerEventData eventData)
	{
		this.PaintBlocksBelow(eventData);
	}

	// Token: 0x06006EDF RID: 28383 RVA: 0x002A3566 File Offset: 0x002A1766
	public void OnDrag(PointerEventData eventData)
	{
		this.PaintBlocksBelow(eventData);
	}

	// Token: 0x06006EE0 RID: 28384 RVA: 0x002A356F File Offset: 0x002A176F
	public void OnEndDrag(PointerEventData eventData)
	{
		this.PaintBlocksBelow(eventData);
	}

	// Token: 0x06006EE1 RID: 28385 RVA: 0x002A3578 File Offset: 0x002A1778
	public void OnPointerDown(PointerEventData eventData)
	{
		ScheduleBlockPainter.paintCounter = 0;
		this.PaintBlocksBelow(eventData);
	}

	// Token: 0x06006EE2 RID: 28386 RVA: 0x002A3588 File Offset: 0x002A1788
	private void PaintBlocksBelow(PointerEventData eventData)
	{
		if (ScheduleScreen.Instance.SelectedPaint.IsNullOrWhiteSpace())
		{
			return;
		}
		List<RaycastResult> list = new List<RaycastResult>();
		global::UnityEngine.EventSystems.EventSystem.current.RaycastAll(eventData, list);
		if (list != null && list.Count > 0)
		{
			ScheduleBlockButton component = list[0].gameObject.GetComponent<ScheduleBlockButton>();
			if (component != null)
			{
				if (this.entry.PaintBlock(component))
				{
					string sound = GlobalAssets.GetSound("ScheduleMenu_Select", false);
					if (sound != null)
					{
						EventInstance eventInstance = SoundEvent.BeginOneShot(sound, SoundListenerController.Instance.transform.GetPosition(), 1f, false);
						eventInstance.setParameterByName("Drag_Count", (float)ScheduleBlockPainter.paintCounter, false);
						ScheduleBlockPainter.paintCounter++;
						SoundEvent.EndOneShot(eventInstance);
						this.previousBlockTriedPainted = component.gameObject;
						return;
					}
				}
				else if (this.previousBlockTriedPainted != component.gameObject)
				{
					this.previousBlockTriedPainted = component.gameObject;
					string sound2 = GlobalAssets.GetSound("ScheduleMenu_Select_none", false);
					if (sound2 != null)
					{
						SoundEvent.EndOneShot(SoundEvent.BeginOneShot(sound2, SoundListenerController.Instance.transform.GetPosition(), 1f, false));
					}
				}
			}
		}
	}

	// Token: 0x04004C3A RID: 19514
	private ScheduleScreenEntry entry;

	// Token: 0x04004C3B RID: 19515
	private static int paintCounter;

	// Token: 0x04004C3C RID: 19516
	private GameObject previousBlockTriedPainted;
}
