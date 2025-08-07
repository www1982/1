using System;
using System.Collections.Generic;
using TUNING;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000DBA RID: 3514
[AddComponentMenu("KMonoBehaviour/scripts/ScheduleBlockButton")]
public class ScheduleBlockButton : KMonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x06006ED7 RID: 28375 RVA: 0x002A3460 File Offset: 0x002A1660
	public void Setup(int hour)
	{
		if (hour < TRAITS.EARLYBIRD_SCHEDULEBLOCK)
		{
			base.GetComponent<HierarchyReferences>().GetReference<RectTransform>("MorningIcon").gameObject.SetActive(true);
		}
		else if (hour >= 21)
		{
			base.GetComponent<HierarchyReferences>().GetReference<RectTransform>("NightIcon").gameObject.SetActive(true);
		}
		base.gameObject.name = "ScheduleBlock_" + hour.ToString();
		this.ToggleHighlight(false);
	}

	// Token: 0x06006ED8 RID: 28376 RVA: 0x002A34D8 File Offset: 0x002A16D8
	public void SetBlockTypes(List<ScheduleBlockType> blockTypes)
	{
		ScheduleGroup scheduleGroup = Db.Get().ScheduleGroups.FindGroupForScheduleTypes(blockTypes);
		if (scheduleGroup != null)
		{
			this.image.color = scheduleGroup.uiColor;
			this.toolTip.SetSimpleTooltip(scheduleGroup.Name);
			return;
		}
		this.toolTip.SetSimpleTooltip("UNKNOWN");
	}

	// Token: 0x06006ED9 RID: 28377 RVA: 0x002A352C File Offset: 0x002A172C
	public void OnPointerEnter(PointerEventData eventData)
	{
		this.ToggleHighlight(true);
	}

	// Token: 0x06006EDA RID: 28378 RVA: 0x002A3535 File Offset: 0x002A1735
	public void OnPointerExit(PointerEventData eventData)
	{
		this.ToggleHighlight(false);
	}

	// Token: 0x06006EDB RID: 28379 RVA: 0x002A353E File Offset: 0x002A173E
	private void ToggleHighlight(bool on)
	{
		this.highlightObject.SetActive(on);
	}

	// Token: 0x04004C37 RID: 19511
	[SerializeField]
	private Image image;

	// Token: 0x04004C38 RID: 19512
	[SerializeField]
	private ToolTip toolTip;

	// Token: 0x04004C39 RID: 19513
	[SerializeField]
	private GameObject highlightObject;
}
