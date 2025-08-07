using System;
using STRINGS;

// Token: 0x020002BA RID: 698
public class ExcavateButton : KMonoBehaviour, ISidescreenButtonControl
{
	// Token: 0x1700003D RID: 61
	// (get) Token: 0x06000E2B RID: 3627 RVA: 0x00052BF1 File Offset: 0x00050DF1
	public string SidescreenButtonText
	{
		get
		{
			if (this.isMarkedForDig == null || !this.isMarkedForDig())
			{
				return CODEX.STORY_TRAITS.FOSSILHUNT.UISIDESCREENS.DIG_SITE_EXCAVATE_BUTTON;
			}
			return CODEX.STORY_TRAITS.FOSSILHUNT.UISIDESCREENS.DIG_SITE_CANCEL_EXCAVATION_BUTTON;
		}
	}

	// Token: 0x1700003E RID: 62
	// (get) Token: 0x06000E2C RID: 3628 RVA: 0x00052C1D File Offset: 0x00050E1D
	public string SidescreenButtonTooltip
	{
		get
		{
			if (this.isMarkedForDig == null || !this.isMarkedForDig())
			{
				return CODEX.STORY_TRAITS.FOSSILHUNT.UISIDESCREENS.DIG_SITE_EXCAVATE_BUTTON_TOOLTIP;
			}
			return CODEX.STORY_TRAITS.FOSSILHUNT.UISIDESCREENS.DIG_SITE_CANCEL_EXCAVATION_BUTTON_TOOLTIP;
		}
	}

	// Token: 0x06000E2D RID: 3629 RVA: 0x00052C49 File Offset: 0x00050E49
	public int HorizontalGroupID()
	{
		return -1;
	}

	// Token: 0x06000E2E RID: 3630 RVA: 0x00052C4C File Offset: 0x00050E4C
	public void SetButtonTextOverride(ButtonMenuTextOverride textOverride)
	{
		throw new NotImplementedException();
	}

	// Token: 0x06000E2F RID: 3631 RVA: 0x00052C53 File Offset: 0x00050E53
	public bool SidescreenEnabled()
	{
		return true;
	}

	// Token: 0x06000E30 RID: 3632 RVA: 0x00052C56 File Offset: 0x00050E56
	public bool SidescreenButtonInteractable()
	{
		return true;
	}

	// Token: 0x06000E31 RID: 3633 RVA: 0x00052C59 File Offset: 0x00050E59
	public void OnSidescreenButtonPressed()
	{
		global::System.Action onButtonPressed = this.OnButtonPressed;
		if (onButtonPressed == null)
		{
			return;
		}
		onButtonPressed();
	}

	// Token: 0x06000E32 RID: 3634 RVA: 0x00052C6B File Offset: 0x00050E6B
	public int ButtonSideScreenSortOrder()
	{
		return 20;
	}

	// Token: 0x0400092B RID: 2347
	public Func<bool> isMarkedForDig;

	// Token: 0x0400092C RID: 2348
	public global::System.Action OnButtonPressed;
}
