using System;
using System.Collections.Generic;
using Database;
using KSerialization;

// Token: 0x02000BA4 RID: 2980
[SerializationConfig(MemberSerialization.OptIn)]
public class StoryInstance : ISaveLoadable
{
	// Token: 0x17000678 RID: 1656
	// (get) Token: 0x060058DB RID: 22747 RVA: 0x00201F8A File Offset: 0x0020018A
	// (set) Token: 0x060058DC RID: 22748 RVA: 0x00201F94 File Offset: 0x00200194
	public StoryInstance.State CurrentState
	{
		get
		{
			return this.state;
		}
		set
		{
			if (this.state == value)
			{
				return;
			}
			this.state = value;
			this.Telemetry.LogStateChange(this.state, GameClock.Instance.GetTimeInCycles());
			Action<StoryInstance.State> storyStateChanged = this.StoryStateChanged;
			if (storyStateChanged == null)
			{
				return;
			}
			storyStateChanged(this.state);
		}
	}

	// Token: 0x17000679 RID: 1657
	// (get) Token: 0x060058DD RID: 22749 RVA: 0x00201FE3 File Offset: 0x002001E3
	public StoryManager.StoryTelemetry Telemetry
	{
		get
		{
			if (this.telemetry == null)
			{
				this.telemetry = new StoryManager.StoryTelemetry();
			}
			return this.telemetry;
		}
	}

	// Token: 0x1700067A RID: 1658
	// (get) Token: 0x060058DE RID: 22750 RVA: 0x00201FFE File Offset: 0x002001FE
	// (set) Token: 0x060058DF RID: 22751 RVA: 0x00202006 File Offset: 0x00200206
	public EventInfoData EventInfo { get; private set; }

	// Token: 0x1700067B RID: 1659
	// (get) Token: 0x060058E0 RID: 22752 RVA: 0x0020200F File Offset: 0x0020020F
	// (set) Token: 0x060058E1 RID: 22753 RVA: 0x00202017 File Offset: 0x00200217
	public Notification Notification { get; private set; }

	// Token: 0x1700067C RID: 1660
	// (get) Token: 0x060058E2 RID: 22754 RVA: 0x00202020 File Offset: 0x00200220
	// (set) Token: 0x060058E3 RID: 22755 RVA: 0x00202028 File Offset: 0x00200228
	public EventInfoDataHelper.PopupType PendingType { get; private set; } = EventInfoDataHelper.PopupType.NONE;

	// Token: 0x060058E4 RID: 22756 RVA: 0x00202031 File Offset: 0x00200231
	public Story GetStory()
	{
		if (this._story == null)
		{
			this._story = Db.Get().Stories.Get(this.storyId);
		}
		return this._story;
	}

	// Token: 0x060058E5 RID: 22757 RVA: 0x0020205C File Offset: 0x0020025C
	public StoryInstance()
	{
	}

	// Token: 0x060058E6 RID: 22758 RVA: 0x00202076 File Offset: 0x00200276
	public StoryInstance(Story story, int worldId)
	{
		this._story = story;
		this.storyId = story.Id;
		this.worldId = worldId;
	}

	// Token: 0x060058E7 RID: 22759 RVA: 0x002020AA File Offset: 0x002002AA
	public bool HasDisplayedPopup(EventInfoDataHelper.PopupType type)
	{
		return this.popupDisplayedStates != null && this.popupDisplayedStates.Contains(type);
	}

	// Token: 0x060058E8 RID: 22760 RVA: 0x002020C4 File Offset: 0x002002C4
	public void SetPopupData(StoryManager.PopupInfo info, EventInfoData eventInfo, Notification notification = null)
	{
		this.EventInfo = eventInfo;
		this.Notification = notification;
		this.PendingType = info.PopupType;
		eventInfo.showCallback = (global::System.Action)Delegate.Combine(eventInfo.showCallback, new global::System.Action(this.OnPopupDisplayed));
		if (info.DisplayImmediate)
		{
			EventInfoScreen.ShowPopup(eventInfo);
		}
	}

	// Token: 0x060058E9 RID: 22761 RVA: 0x0020211C File Offset: 0x0020031C
	private void OnPopupDisplayed()
	{
		if (this.popupDisplayedStates == null)
		{
			this.popupDisplayedStates = new HashSet<EventInfoDataHelper.PopupType>();
		}
		this.popupDisplayedStates.Add(this.PendingType);
		this.EventInfo = null;
		this.Notification = null;
		this.PendingType = EventInfoDataHelper.PopupType.NONE;
	}

	// Token: 0x04003B0C RID: 15116
	public Action<StoryInstance.State> StoryStateChanged;

	// Token: 0x04003B0D RID: 15117
	[Serialize]
	public readonly string storyId;

	// Token: 0x04003B0E RID: 15118
	[Serialize]
	public int worldId;

	// Token: 0x04003B0F RID: 15119
	[Serialize]
	private StoryInstance.State state;

	// Token: 0x04003B10 RID: 15120
	[Serialize]
	private StoryManager.StoryTelemetry telemetry;

	// Token: 0x04003B11 RID: 15121
	[Serialize]
	private HashSet<EventInfoDataHelper.PopupType> popupDisplayedStates = new HashSet<EventInfoDataHelper.PopupType>();

	// Token: 0x04003B15 RID: 15125
	private Story _story;

	// Token: 0x02001CCF RID: 7375
	public enum State
	{
		// Token: 0x04008763 RID: 34659
		RETROFITTED = -1,
		// Token: 0x04008764 RID: 34660
		NOT_STARTED,
		// Token: 0x04008765 RID: 34661
		DISCOVERED,
		// Token: 0x04008766 RID: 34662
		IN_PROGRESS,
		// Token: 0x04008767 RID: 34663
		COMPLETE
	}
}
