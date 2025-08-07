using System;
using System.IO;
using ProcGenGame;
using UnityEngine;

// Token: 0x02000E8C RID: 3724
public class WorldGenScreen : NewGameFlowScreen
{
	// Token: 0x0600769E RID: 30366 RVA: 0x002D638A File Offset: 0x002D458A
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		WorldGenScreen.Instance = this;
	}

	// Token: 0x0600769F RID: 30367 RVA: 0x002D6398 File Offset: 0x002D4598
	protected override void OnForcedCleanUp()
	{
		WorldGenScreen.Instance = null;
		base.OnForcedCleanUp();
	}

	// Token: 0x060076A0 RID: 30368 RVA: 0x002D63A8 File Offset: 0x002D45A8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (MainMenu.Instance != null)
		{
			MainMenu.Instance.StopAmbience();
		}
		this.TriggerLoadingMusic();
		global::UnityEngine.Object.FindObjectOfType<FrontEndBackground>().gameObject.SetActive(false);
		SaveLoader.SetActiveSaveFilePath(null);
		try
		{
			if (File.Exists(WorldGen.WORLDGEN_SAVE_FILENAME))
			{
				File.Delete(WorldGen.WORLDGEN_SAVE_FILENAME);
			}
		}
		catch (Exception ex)
		{
			DebugUtil.LogWarningArgs(new object[] { ex.ToString() });
		}
		this.offlineWorldGen.Generate();
	}

	// Token: 0x060076A1 RID: 30369 RVA: 0x002D6438 File Offset: 0x002D4638
	private void TriggerLoadingMusic()
	{
		if (AudioDebug.Get().musicEnabled && !MusicManager.instance.SongIsPlaying("Music_FrontEnd"))
		{
			MainMenu.Instance.StopMainMenuMusic();
			AudioMixer.instance.Start(AudioMixerSnapshots.Get().FrontEndWorldGenerationSnapshot);
			MusicManager.instance.PlaySong("Music_FrontEnd", false);
			MusicManager.instance.SetSongParameter("Music_FrontEnd", "songSection", 1f, true);
		}
	}

	// Token: 0x060076A2 RID: 30370 RVA: 0x002D64AB File Offset: 0x002D46AB
	public override void OnKeyDown(KButtonEvent e)
	{
		if (!e.Consumed)
		{
			e.TryConsume(global::Action.Escape);
		}
		if (!e.Consumed)
		{
			e.TryConsume(global::Action.MouseRight);
		}
		base.OnKeyDown(e);
	}

	// Token: 0x04005294 RID: 21140
	[MyCmpReq]
	private OfflineWorldGen offlineWorldGen;

	// Token: 0x04005295 RID: 21141
	public static WorldGenScreen Instance;
}
