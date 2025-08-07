using System;
using UnityEngine;

// Token: 0x020003EF RID: 1007
public class SidescreenViewObjectButton : KMonoBehaviour, ISidescreenButtonControl
{
	// Token: 0x06001495 RID: 5269 RVA: 0x00075A30 File Offset: 0x00073C30
	public bool IsValid()
	{
		SidescreenViewObjectButton.Mode trackMode = this.TrackMode;
		if (trackMode != SidescreenViewObjectButton.Mode.Target)
		{
			return trackMode == SidescreenViewObjectButton.Mode.Cell && Grid.IsValidCell(this.TargetCell);
		}
		return this.Target != null;
	}

	// Token: 0x17000052 RID: 82
	// (get) Token: 0x06001496 RID: 5270 RVA: 0x00075A65 File Offset: 0x00073C65
	public string SidescreenButtonText
	{
		get
		{
			return this.Text;
		}
	}

	// Token: 0x17000053 RID: 83
	// (get) Token: 0x06001497 RID: 5271 RVA: 0x00075A6D File Offset: 0x00073C6D
	public string SidescreenButtonTooltip
	{
		get
		{
			return this.Tooltip;
		}
	}

	// Token: 0x06001498 RID: 5272 RVA: 0x00075A75 File Offset: 0x00073C75
	public void SetButtonTextOverride(ButtonMenuTextOverride textOverride)
	{
		throw new NotImplementedException();
	}

	// Token: 0x06001499 RID: 5273 RVA: 0x00075A7C File Offset: 0x00073C7C
	public bool SidescreenEnabled()
	{
		return true;
	}

	// Token: 0x0600149A RID: 5274 RVA: 0x00075A7F File Offset: 0x00073C7F
	public bool SidescreenButtonInteractable()
	{
		return this.IsValid();
	}

	// Token: 0x0600149B RID: 5275 RVA: 0x00075A87 File Offset: 0x00073C87
	public int HorizontalGroupID()
	{
		return this.horizontalGroupID;
	}

	// Token: 0x0600149C RID: 5276 RVA: 0x00075A90 File Offset: 0x00073C90
	public void OnSidescreenButtonPressed()
	{
		if (this.IsValid())
		{
			SidescreenViewObjectButton.Mode trackMode = this.TrackMode;
			if (trackMode == SidescreenViewObjectButton.Mode.Target)
			{
				GameUtil.FocusCamera(this.Target.transform.GetPosition(), 2f, true, true);
				return;
			}
			if (trackMode == SidescreenViewObjectButton.Mode.Cell)
			{
				GameUtil.FocusCamera(Grid.CellToPos(this.TargetCell), 2f, true, true);
				return;
			}
		}
		else
		{
			base.gameObject.Trigger(1980521255, null);
		}
	}

	// Token: 0x0600149D RID: 5277 RVA: 0x00075AF9 File Offset: 0x00073CF9
	public int ButtonSideScreenSortOrder()
	{
		return 20;
	}

	// Token: 0x04000C4A RID: 3146
	public string Text;

	// Token: 0x04000C4B RID: 3147
	public string Tooltip;

	// Token: 0x04000C4C RID: 3148
	public SidescreenViewObjectButton.Mode TrackMode;

	// Token: 0x04000C4D RID: 3149
	public GameObject Target;

	// Token: 0x04000C4E RID: 3150
	public int TargetCell;

	// Token: 0x04000C4F RID: 3151
	public int horizontalGroupID = -1;

	// Token: 0x0200120E RID: 4622
	public enum Mode
	{
		// Token: 0x040064F4 RID: 25844
		Target,
		// Token: 0x040064F5 RID: 25845
		Cell
	}
}
