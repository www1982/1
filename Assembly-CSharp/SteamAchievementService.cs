using System;
using System.Diagnostics;
using Steamworks;
using UnityEngine;

// Token: 0x02000BF0 RID: 3056
public class SteamAchievementService : MonoBehaviour
{
	// Token: 0x170006BE RID: 1726
	// (get) Token: 0x06005C11 RID: 23569 RVA: 0x00217D13 File Offset: 0x00215F13
	public static SteamAchievementService Instance
	{
		get
		{
			return SteamAchievementService.instance;
		}
	}

	// Token: 0x06005C12 RID: 23570 RVA: 0x00217D1C File Offset: 0x00215F1C
	public static void Initialize()
	{
		if (SteamAchievementService.instance == null)
		{
			GameObject gameObject = GameObject.Find("/SteamManager");
			SteamAchievementService.instance = gameObject.GetComponent<SteamAchievementService>();
			if (SteamAchievementService.instance == null)
			{
				SteamAchievementService.instance = gameObject.AddComponent<SteamAchievementService>();
			}
		}
	}

	// Token: 0x06005C13 RID: 23571 RVA: 0x00217D64 File Offset: 0x00215F64
	public void Awake()
	{
		this.setupComplete = false;
		global::Debug.Assert(SteamAchievementService.instance == null);
		SteamAchievementService.instance = this;
	}

	// Token: 0x06005C14 RID: 23572 RVA: 0x00217D83 File Offset: 0x00215F83
	private void OnDestroy()
	{
		global::Debug.Assert(SteamAchievementService.instance == this);
		SteamAchievementService.instance = null;
	}

	// Token: 0x06005C15 RID: 23573 RVA: 0x00217D9B File Offset: 0x00215F9B
	private void Update()
	{
		if (!SteamManager.Initialized)
		{
			return;
		}
		if (Game.Instance != null)
		{
			return;
		}
		if (!this.setupComplete && DistributionPlatform.Initialized)
		{
			this.Setup();
		}
	}

	// Token: 0x06005C16 RID: 23574 RVA: 0x00217DC8 File Offset: 0x00215FC8
	private void Setup()
	{
		this.cbUserStatsReceived = Callback<UserStatsReceived_t>.Create(new Callback<UserStatsReceived_t>.DispatchDelegate(this.OnUserStatsReceived));
		this.cbUserStatsStored = Callback<UserStatsStored_t>.Create(new Callback<UserStatsStored_t>.DispatchDelegate(this.OnUserStatsStored));
		this.cbUserAchievementStored = Callback<UserAchievementStored_t>.Create(new Callback<UserAchievementStored_t>.DispatchDelegate(this.OnUserAchievementStored));
		this.setupComplete = true;
		this.RefreshStats();
	}

	// Token: 0x06005C17 RID: 23575 RVA: 0x00217E27 File Offset: 0x00216027
	private void RefreshStats()
	{
		SteamUserStats.RequestCurrentStats();
	}

	// Token: 0x06005C18 RID: 23576 RVA: 0x00217E2F File Offset: 0x0021602F
	private void OnUserStatsReceived(UserStatsReceived_t data)
	{
		if (data.m_eResult != EResult.k_EResultOK)
		{
			DebugUtil.LogWarningArgs(new object[] { "OnUserStatsReceived", data.m_eResult, data.m_steamIDUser });
			return;
		}
	}

	// Token: 0x06005C19 RID: 23577 RVA: 0x00217E6A File Offset: 0x0021606A
	private void OnUserStatsStored(UserStatsStored_t data)
	{
		if (data.m_eResult != EResult.k_EResultOK)
		{
			DebugUtil.LogWarningArgs(new object[] { "OnUserStatsStored", data.m_eResult });
			return;
		}
	}

	// Token: 0x06005C1A RID: 23578 RVA: 0x00217E97 File Offset: 0x00216097
	private void OnUserAchievementStored(UserAchievementStored_t data)
	{
	}

	// Token: 0x06005C1B RID: 23579 RVA: 0x00217E9C File Offset: 0x0021609C
	public void Unlock(string achievement_id)
	{
		bool flag = SteamUserStats.SetAchievement(achievement_id);
		global::Debug.LogFormat("SetAchievement {0} {1}", new object[] { achievement_id, flag });
		bool flag2 = SteamUserStats.StoreStats();
		global::Debug.LogFormat("StoreStats {0}", new object[] { flag2 });
	}

	// Token: 0x06005C1C RID: 23580 RVA: 0x00217EEC File Offset: 0x002160EC
	[Conditional("UNITY_EDITOR")]
	[ContextMenu("Reset All Achievements")]
	private void ResetAllAchievements()
	{
		bool flag = SteamUserStats.ResetAllStats(true);
		global::Debug.LogFormat("ResetAllStats {0}", new object[] { flag });
		if (flag)
		{
			this.RefreshStats();
		}
	}

	// Token: 0x04003CF7 RID: 15607
	private Callback<UserStatsReceived_t> cbUserStatsReceived;

	// Token: 0x04003CF8 RID: 15608
	private Callback<UserStatsStored_t> cbUserStatsStored;

	// Token: 0x04003CF9 RID: 15609
	private Callback<UserAchievementStored_t> cbUserAchievementStored;

	// Token: 0x04003CFA RID: 15610
	private bool setupComplete;

	// Token: 0x04003CFB RID: 15611
	private static SteamAchievementService instance;
}
