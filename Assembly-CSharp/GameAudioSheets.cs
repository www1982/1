using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000554 RID: 1364
public class GameAudioSheets : AudioSheets
{
	// Token: 0x06001E17 RID: 7703 RVA: 0x000A3832 File Offset: 0x000A1A32
	public static GameAudioSheets Get()
	{
		if (GameAudioSheets._Instance == null)
		{
			GameAudioSheets._Instance = Resources.Load<GameAudioSheets>("GameAudioSheets");
		}
		return GameAudioSheets._Instance;
	}

	// Token: 0x06001E18 RID: 7704 RVA: 0x000A3858 File Offset: 0x000A1A58
	public override void Initialize()
	{
		this.validFileNames.Add("game_triggered");
		foreach (KAnimFile kanimFile in Assets.instance.AnimAssets)
		{
			if (!(kanimFile == null))
			{
				this.validFileNames.Add(kanimFile.name);
			}
		}
		base.Initialize();
		foreach (AudioSheet audioSheet in this.sheets)
		{
			foreach (AudioSheet.SoundInfo soundInfo in audioSheet.soundInfos)
			{
				if (soundInfo.Type == "MouthFlapSoundEvent" || soundInfo.Type == "VoiceSoundEvent")
				{
					HashSet<HashedString> hashSet = null;
					if (!this.animsNotAllowedToPlaySpeech.TryGetValue(soundInfo.File, out hashSet))
					{
						hashSet = new HashSet<HashedString>();
						this.animsNotAllowedToPlaySpeech[soundInfo.File] = hashSet;
					}
					hashSet.Add(soundInfo.Anim);
				}
			}
		}
	}

	// Token: 0x06001E19 RID: 7705 RVA: 0x000A39C0 File Offset: 0x000A1BC0
	protected override AnimEvent CreateSoundOfType(string type, string file_name, string sound_name, int frame, float min_interval, string dlcId)
	{
		SoundEvent soundEvent = null;
		bool flag = true;
		if (sound_name.Contains(":disable_camera_position_scaling"))
		{
			sound_name = sound_name.Replace(":disable_camera_position_scaling", "");
			flag = false;
		}
		if (type == "FloorSoundEvent")
		{
			soundEvent = new FloorSoundEvent(file_name, sound_name, frame);
		}
		else if (type == "SoundEvent" || type == "LoopingSoundEvent")
		{
			bool flag2 = type == "LoopingSoundEvent";
			string[] array = sound_name.Split(':', StringSplitOptions.None);
			sound_name = array[0];
			soundEvent = new SoundEvent(file_name, sound_name, frame, true, flag2, min_interval, false);
			for (int i = 1; i < array.Length; i++)
			{
				if (array[i] == "IGNORE_PAUSE")
				{
					soundEvent.ignorePause = true;
				}
				else
				{
					global::Debug.LogWarning(sound_name + " has unknown parameter " + array[i]);
				}
			}
		}
		else if (type == "LadderSoundEvent")
		{
			soundEvent = new LadderSoundEvent(file_name, sound_name, frame);
		}
		else if (type == "LaserSoundEvent")
		{
			soundEvent = new LaserSoundEvent(file_name, sound_name, frame, min_interval);
		}
		else if (type == "HatchDrillSoundEvent")
		{
			soundEvent = new HatchDrillSoundEvent(file_name, sound_name, frame, min_interval);
		}
		else if (type == "CreatureChewSoundEvent")
		{
			soundEvent = new CreatureChewSoundEvent(file_name, sound_name, frame, min_interval);
		}
		else if (type == "BuildingDamageSoundEvent")
		{
			soundEvent = new BuildingDamageSoundEvent(file_name, sound_name, frame);
		}
		else if (type == "WallDamageSoundEvent")
		{
			soundEvent = new WallDamageSoundEvent(file_name, sound_name, frame, min_interval);
		}
		else if (type == "RemoteSoundEvent")
		{
			soundEvent = new RemoteSoundEvent(file_name, sound_name, frame, min_interval);
		}
		else if (type == "VoiceSoundEvent" || type == "LoopingVoiceSoundEvent")
		{
			soundEvent = new VoiceSoundEvent(file_name, sound_name, frame, type == "LoopingVoiceSoundEvent");
		}
		else if (type == "MouthFlapSoundEvent")
		{
			soundEvent = new MouthFlapSoundEvent(file_name, sound_name, frame, false);
		}
		else if (type == "MainMenuSoundEvent")
		{
			soundEvent = new MainMenuSoundEvent(file_name, sound_name, frame);
		}
		else if (type == "ClusterMapSoundEvent")
		{
			soundEvent = new ClusterMapSoundEvent(file_name, sound_name, frame, false);
		}
		else if (type == "ClusterMapLoopingSoundEvent")
		{
			soundEvent = new ClusterMapSoundEvent(file_name, sound_name, frame, true);
		}
		else if (type == "UIAnimationSoundEvent")
		{
			soundEvent = new UIAnimationSoundEvent(file_name, sound_name, frame, false);
		}
		else if (type == "UIAnimationVoiceSoundEvent")
		{
			soundEvent = new UIAnimationVoiceSoundEvent(file_name, sound_name, frame, false);
		}
		else if (type == "UIAnimationLoopingSoundEvent")
		{
			soundEvent = new UIAnimationSoundEvent(file_name, sound_name, frame, true);
		}
		else if (type == "CreatureVariationSoundEvent")
		{
			soundEvent = new CreatureVariationSoundEvent(file_name, sound_name, frame, true, type == "LoopingSoundEvent", min_interval, false);
		}
		else if (type == "CountedSoundEvent")
		{
			soundEvent = new CountedSoundEvent(file_name, sound_name, frame, true, false, min_interval, false);
		}
		else if (type == "SculptingSoundEvent")
		{
			soundEvent = new SculptingSoundEvent(file_name, sound_name, frame, true, false, min_interval, false);
		}
		else if (type == "PhonoboxSoundEvent")
		{
			soundEvent = new PhonoboxSoundEvent(file_name, sound_name, frame, min_interval);
		}
		else if (type == "PlantMutationSoundEvent")
		{
			soundEvent = new PlantMutationSoundEvent(file_name, sound_name, frame, min_interval);
		}
		if (soundEvent != null)
		{
			soundEvent.shouldCameraScalePosition = flag;
		}
		return soundEvent;
	}

	// Token: 0x06001E1A RID: 7706 RVA: 0x000A3D08 File Offset: 0x000A1F08
	public bool IsAnimAllowedToPlaySpeech(KAnim.Anim anim)
	{
		HashSet<HashedString> hashSet = null;
		return !this.animsNotAllowedToPlaySpeech.TryGetValue(anim.animFile.name, out hashSet) || !hashSet.Contains(anim.hash);
	}

	// Token: 0x04001187 RID: 4487
	private static GameAudioSheets _Instance;

	// Token: 0x04001188 RID: 4488
	private HashSet<HashedString> validFileNames = new HashSet<HashedString>();

	// Token: 0x04001189 RID: 4489
	private Dictionary<HashedString, HashSet<HashedString>> animsNotAllowedToPlaySpeech = new Dictionary<HashedString, HashSet<HashedString>>();

	// Token: 0x0200139F RID: 5023
	private class SingleAudioSheetLoader : AsyncLoader
	{
		// Token: 0x06008AE2 RID: 35554 RVA: 0x00351B1C File Offset: 0x0034FD1C
		public override void Run()
		{
			this.sheet.soundInfos = new ResourceLoader<AudioSheet.SoundInfo>(this.text, this.name).resources.ToArray();
		}

		// Token: 0x04006A21 RID: 27169
		public AudioSheet sheet;

		// Token: 0x04006A22 RID: 27170
		public string text;

		// Token: 0x04006A23 RID: 27171
		public string name;
	}

	// Token: 0x020013A0 RID: 5024
	private class GameAudioSheetLoader : GlobalAsyncLoader<GameAudioSheets.GameAudioSheetLoader>
	{
		// Token: 0x06008AE4 RID: 35556 RVA: 0x00351B4C File Offset: 0x0034FD4C
		public override void CollectLoaders(List<AsyncLoader> loaders)
		{
			foreach (AudioSheet audioSheet in GameAudioSheets.Get().sheets)
			{
				loaders.Add(new GameAudioSheets.SingleAudioSheetLoader
				{
					sheet = audioSheet,
					text = audioSheet.asset.text,
					name = audioSheet.asset.name
				});
			}
		}

		// Token: 0x06008AE5 RID: 35557 RVA: 0x00351BD0 File Offset: 0x0034FDD0
		public override void Run()
		{
		}
	}
}
