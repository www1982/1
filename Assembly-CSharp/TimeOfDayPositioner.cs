using System;
using UnityEngine;

// Token: 0x02000E66 RID: 3686
public class TimeOfDayPositioner : KMonoBehaviour
{
	// Token: 0x060075A5 RID: 30117 RVA: 0x002D0B2C File Offset: 0x002CED2C
	public void SetTargetTimetable(GameObject TimetableRow)
	{
		if (TimetableRow == null)
		{
			this.targetRect = null;
			base.transform.SetParent(null);
			return;
		}
		RectTransform rectTransform = TimetableRow.GetComponent<HierarchyReferences>().GetReference<RectTransform>("BlockContainer").rectTransform();
		this.targetRect = rectTransform;
		base.transform.SetParent(this.targetRect.transform);
	}

	// Token: 0x060075A6 RID: 30118 RVA: 0x002D0B8C File Offset: 0x002CED8C
	private void Update()
	{
		if (this.targetRect == null)
		{
			return;
		}
		if (base.transform.parent != this.targetRect.transform)
		{
			base.transform.parent = this.targetRect.transform;
		}
		float num = GameClock.Instance.GetCurrentCycleAsPercentage() * this.targetRect.rect.width;
		(base.transform as RectTransform).anchoredPosition = new Vector2(Mathf.Round(num), 0f);
	}

	// Token: 0x04005192 RID: 20882
	private RectTransform targetRect;
}
