using System;
using System.Collections;
using FMOD.Studio;
using Klei.CustomSettings;
using ProcGen;
using STRINGS;
using UnityEngine;

// Token: 0x02000C26 RID: 3110
public class MinionSelectScreen : CharacterSelectionController
{
	// Token: 0x06005E86 RID: 24198 RVA: 0x0022A224 File Offset: 0x00228424
	protected override void OnPrefabInit()
	{
		base.IsStarterMinion = true;
		base.OnPrefabInit();
		if (MusicManager.instance.SongIsPlaying("Music_FrontEnd"))
		{
			MusicManager.instance.SetSongParameter("Music_FrontEnd", "songSection", 2f, true);
		}
		GameObject gameObject = GameObject.Find("ScreenSpaceOverlayCanvas");
		GameObject gameObject2 = global::Util.KInstantiateUI(this.wattsonMessagePrefab.gameObject, gameObject, false);
		gameObject2.name = "WattsonMessage";
		gameObject2.SetActive(false);
		Game.Instance.Subscribe(-1992507039, new Action<object>(this.OnBaseAlreadyCreated));
		this.backButton.onClick += delegate
		{
			LoadScreen.ForceStopGame();
			App.LoadScene("frontend");
		};
		this.InitializeContainers();
		base.StartCoroutine(this.SetDefaultMinionsRoutine());
	}

	// Token: 0x06005E87 RID: 24199 RVA: 0x0022A2F0 File Offset: 0x002284F0
	private IEnumerator SetDefaultMinionsRoutine()
	{
		yield return SequenceUtil.WaitForNextFrame;
		SettingLevel currentQualitySetting = CustomGameSettings.Instance.GetCurrentQualitySetting(CustomGameSettingConfigs.ClusterLayout);
		ClusterLayout clusterData = SettingsCache.clusterLayouts.GetClusterData(currentQualitySetting.id);
		bool flag = clusterData.clusterTags.Contains("CeresCluster");
		bool flag2 = clusterData.clusterTags.Contains("PrehistoricCluster");
		if (flag)
		{
			((CharacterContainer)this.containers[2]).SetMinion(new MinionStartingStats(Db.Get().Personalities.Get("FREYJA"), null, null, false));
			((CharacterContainer)this.containers[1]).GenerateCharacter(true, null);
			((CharacterContainer)this.containers[0]).GenerateCharacter(true, null);
		}
		else if (flag2)
		{
			((CharacterContainer)this.containers[2]).SetMinion(new MinionStartingStats(Db.Get().Personalities.Get("MAYA"), null, null, false));
			((CharacterContainer)this.containers[1]).SetMinion(new MinionStartingStats(Db.Get().Personalities.Get("HIGBY"), null, null, false));
			((CharacterContainer)this.containers[0]).GenerateCharacter(true, null);
		}
		yield break;
	}

	// Token: 0x06005E88 RID: 24200 RVA: 0x0022A300 File Offset: 0x00228500
	public void SetProceedButtonActive(bool state, string tooltip = null)
	{
		if (state)
		{
			base.EnableProceedButton();
		}
		else
		{
			base.DisableProceedButton();
		}
		ToolTip component = this.proceedButton.GetComponent<ToolTip>();
		if (component != null)
		{
			if (tooltip != null)
			{
				component.toolTip = tooltip;
				return;
			}
			component.ClearMultiStringTooltip();
		}
	}

	// Token: 0x06005E89 RID: 24201 RVA: 0x0022A344 File Offset: 0x00228544
	protected override void OnSpawn()
	{
		this.OnDeliverableAdded();
		base.EnableProceedButton();
		this.proceedButton.GetComponentInChildren<LocText>().text = UI.IMMIGRANTSCREEN.EMBARK;
		this.containers.ForEach(delegate(ITelepadDeliverableContainer container)
		{
			CharacterContainer characterContainer = container as CharacterContainer;
			if (characterContainer != null)
			{
				characterContainer.DisableSelectButton();
			}
		});
	}

	// Token: 0x06005E8A RID: 24202 RVA: 0x0022A3A4 File Offset: 0x002285A4
	protected override void OnProceed()
	{
		global::Util.KInstantiateUI(this.newBasePrefab.gameObject, GameScreenManager.Instance.ssOverlayCanvas, false);
		MusicManager.instance.StopSong("Music_FrontEnd", true, STOP_MODE.ALLOWFADEOUT);
		AudioMixer.instance.Start(AudioMixerSnapshots.Get().NewBaseSetupSnapshot);
		AudioMixer.instance.Stop(AudioMixerSnapshots.Get().FrontEndWorldGenerationSnapshot, STOP_MODE.ALLOWFADEOUT);
		int num = 0;
		this.selectedDeliverables.Clear();
		foreach (ITelepadDeliverableContainer telepadDeliverableContainer in this.containers)
		{
			CharacterContainer characterContainer = (CharacterContainer)telepadDeliverableContainer;
			this.selectedDeliverables.Add(characterContainer.Stats);
			if (characterContainer.Stats.personality.model == BionicMinionConfig.MODEL)
			{
				num++;
			}
		}
		NewBaseScreen.Instance.Init(SaveLoader.Instance.Cluster, this.selectedDeliverables.ToArray());
		if (this.OnProceedEvent != null)
		{
			this.OnProceedEvent();
		}
		if (Game.IsDlcActiveForCurrentSave("DLC3_ID") && Components.RoleStations.Count > 0)
		{
			BuildingFacade component = Components.RoleStations[0].GetComponent<BuildingFacade>();
			bool flag = !component.IsOriginal;
			if (num == 3 || (!flag && num > 0))
			{
				component.ApplyBuildingFacade(Db.GetBuildingFacades().Get("permit_hqbase_cyberpunk"), false);
			}
		}
		Game.Instance.Trigger(-838649377, null);
		BuildWatermark.Instance.gameObject.SetActive(false);
		this.Deactivate();
	}

	// Token: 0x06005E8B RID: 24203 RVA: 0x0022A53C File Offset: 0x0022873C
	private void OnBaseAlreadyCreated(object data)
	{
		Game.Instance.StopFE();
		Game.Instance.StartBE();
		Game.Instance.SetGameStarted();
		this.Deactivate();
	}

	// Token: 0x06005E8C RID: 24204 RVA: 0x0022A562 File Offset: 0x00228762
	private void ReshuffleAll()
	{
		if (this.OnReshuffleEvent != null)
		{
			this.OnReshuffleEvent(base.IsStarterMinion);
		}
	}

	// Token: 0x06005E8D RID: 24205 RVA: 0x0022A580 File Offset: 0x00228780
	public override void OnPressBack()
	{
		foreach (ITelepadDeliverableContainer telepadDeliverableContainer in this.containers)
		{
			CharacterContainer characterContainer = telepadDeliverableContainer as CharacterContainer;
			if (characterContainer != null)
			{
				characterContainer.ForceStopEditingTitle();
			}
		}
	}

	// Token: 0x04003EEF RID: 16111
	[SerializeField]
	private NewBaseScreen newBasePrefab;

	// Token: 0x04003EF0 RID: 16112
	[SerializeField]
	private WattsonMessage wattsonMessagePrefab;

	// Token: 0x04003EF1 RID: 16113
	public const string WattsonGameObjName = "WattsonMessage";

	// Token: 0x04003EF2 RID: 16114
	public KButton backButton;
}
