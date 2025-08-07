using System;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

// Token: 0x02000BFF RID: 3071
public class AudioMixerSnapshots : ScriptableObject
{
	// Token: 0x06005C7E RID: 23678 RVA: 0x0021C588 File Offset: 0x0021A788
	[ContextMenu("Reload")]
	public void ReloadSnapshots()
	{
		this.snapshotMap.Clear();
		EventReference[] array = this.snapshots;
		for (int i = 0; i < array.Length; i++)
		{
			string eventReferencePath = KFMOD.GetEventReferencePath(array[i]);
			if (!eventReferencePath.IsNullOrWhiteSpace())
			{
				this.snapshotMap.Add(eventReferencePath);
			}
		}
	}

	// Token: 0x06005C7F RID: 23679 RVA: 0x0021C5D6 File Offset: 0x0021A7D6
	public static AudioMixerSnapshots Get()
	{
		if (AudioMixerSnapshots.instance == null)
		{
			AudioMixerSnapshots.instance = Resources.Load<AudioMixerSnapshots>("AudioMixerSnapshots");
		}
		return AudioMixerSnapshots.instance;
	}

	// Token: 0x04003D44 RID: 15684
	public EventReference TechFilterOnMigrated;

	// Token: 0x04003D45 RID: 15685
	public EventReference TechFilterLogicOn;

	// Token: 0x04003D46 RID: 15686
	public EventReference NightStartedMigrated;

	// Token: 0x04003D47 RID: 15687
	public EventReference MenuOpenMigrated;

	// Token: 0x04003D48 RID: 15688
	public EventReference MenuOpenHalfEffect;

	// Token: 0x04003D49 RID: 15689
	public EventReference SpeedPausedMigrated;

	// Token: 0x04003D4A RID: 15690
	public EventReference DuplicantCountAttenuatorMigrated;

	// Token: 0x04003D4B RID: 15691
	public EventReference NewBaseSetupSnapshot;

	// Token: 0x04003D4C RID: 15692
	public EventReference FrontEndSnapshot;

	// Token: 0x04003D4D RID: 15693
	public EventReference FrontEndWelcomeScreenSnapshot;

	// Token: 0x04003D4E RID: 15694
	public EventReference FrontEndWorldGenerationSnapshot;

	// Token: 0x04003D4F RID: 15695
	public EventReference IntroNIS;

	// Token: 0x04003D50 RID: 15696
	public EventReference PulseSnapshot;

	// Token: 0x04003D51 RID: 15697
	public EventReference ESCPauseSnapshot;

	// Token: 0x04003D52 RID: 15698
	public EventReference MENUNewDuplicantSnapshot;

	// Token: 0x04003D53 RID: 15699
	public EventReference UserVolumeSettingsSnapshot;

	// Token: 0x04003D54 RID: 15700
	public EventReference DuplicantCountMovingSnapshot;

	// Token: 0x04003D55 RID: 15701
	public EventReference DuplicantCountSleepingSnapshot;

	// Token: 0x04003D56 RID: 15702
	public EventReference PortalLPDimmedSnapshot;

	// Token: 0x04003D57 RID: 15703
	public EventReference DynamicMusicPlayingSnapshot;

	// Token: 0x04003D58 RID: 15704
	public EventReference FabricatorSideScreenOpenSnapshot;

	// Token: 0x04003D59 RID: 15705
	public EventReference SpaceVisibleSnapshot;

	// Token: 0x04003D5A RID: 15706
	public EventReference MENUStarmapSnapshot;

	// Token: 0x04003D5B RID: 15707
	public EventReference MENUStarmapNotPausedSnapshot;

	// Token: 0x04003D5C RID: 15708
	public EventReference GameNotFocusedSnapshot;

	// Token: 0x04003D5D RID: 15709
	public EventReference FacilityVisibleSnapshot;

	// Token: 0x04003D5E RID: 15710
	public EventReference TutorialVideoPlayingSnapshot;

	// Token: 0x04003D5F RID: 15711
	public EventReference VictoryMessageSnapshot;

	// Token: 0x04003D60 RID: 15712
	public EventReference VictoryNISGenericSnapshot;

	// Token: 0x04003D61 RID: 15713
	public EventReference VictoryNISRocketSnapshot;

	// Token: 0x04003D62 RID: 15714
	public EventReference VictoryCinematicSnapshot;

	// Token: 0x04003D63 RID: 15715
	public EventReference VictoryFadeToBlackSnapshot;

	// Token: 0x04003D64 RID: 15716
	public EventReference MuteDynamicMusicSnapshot;

	// Token: 0x04003D65 RID: 15717
	public EventReference ActiveBaseChangeSnapshot;

	// Token: 0x04003D66 RID: 15718
	public EventReference EventPopupSnapshot;

	// Token: 0x04003D67 RID: 15719
	public EventReference SmallRocketInteriorReverbSnapshot;

	// Token: 0x04003D68 RID: 15720
	public EventReference MediumRocketInteriorReverbSnapshot;

	// Token: 0x04003D69 RID: 15721
	public EventReference MainMenuVideoPlayingSnapshot;

	// Token: 0x04003D6A RID: 15722
	public EventReference TechFilterRadiationOn;

	// Token: 0x04003D6B RID: 15723
	public EventReference FrontEndSupplyClosetSnapshot;

	// Token: 0x04003D6C RID: 15724
	public EventReference FrontEndItemDropScreenSnapshot;

	// Token: 0x04003D6D RID: 15725
	[SerializeField]
	private EventReference[] snapshots;

	// Token: 0x04003D6E RID: 15726
	[NonSerialized]
	public List<string> snapshotMap = new List<string>();

	// Token: 0x04003D6F RID: 15727
	private static AudioMixerSnapshots instance;
}
