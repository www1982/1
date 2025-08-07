using System;
using System.IO;
using System.Runtime.Serialization;
using Klei;
using KSerialization;
using UnityEngine;

// Token: 0x02000927 RID: 2343
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/GameClock")]
public class GameClock : KMonoBehaviour, ISaveLoadable, ISim33ms, IRender1000ms
{
	// Token: 0x060041A6 RID: 16806 RVA: 0x00172498 File Offset: 0x00170698
	public static void DestroyInstance()
	{
		GameClock.Instance = null;
	}

	// Token: 0x060041A7 RID: 16807 RVA: 0x001724A0 File Offset: 0x001706A0
	protected override void OnPrefabInit()
	{
		GameClock.Instance = this;
		this.timeSinceStartOfCycle = 50f;
	}

	// Token: 0x060041A8 RID: 16808 RVA: 0x001724B4 File Offset: 0x001706B4
	[OnDeserialized]
	private void OnDeserialized()
	{
		if (this.time != 0f)
		{
			this.cycle = (int)(this.time / 600f);
			this.timeSinceStartOfCycle = Mathf.Max(this.time - (float)this.cycle * 600f, 0f);
			this.time = 0f;
		}
	}

	// Token: 0x060041A9 RID: 16809 RVA: 0x00172510 File Offset: 0x00170710
	public void Sim33ms(float dt)
	{
		this.AddTime(dt);
	}

	// Token: 0x060041AA RID: 16810 RVA: 0x00172519 File Offset: 0x00170719
	public void Render1000ms(float dt)
	{
		this.timePlayed += dt;
	}

	// Token: 0x060041AB RID: 16811 RVA: 0x00172529 File Offset: 0x00170729
	private void LateUpdate()
	{
		this.frame++;
	}

	// Token: 0x060041AC RID: 16812 RVA: 0x0017253C File Offset: 0x0017073C
	private void AddTime(float dt)
	{
		this.timeSinceStartOfCycle += dt;
		bool flag = false;
		while (this.timeSinceStartOfCycle >= 600f)
		{
			this.cycle++;
			this.timeSinceStartOfCycle -= 600f;
			base.Trigger(631075836, null);
			foreach (WorldContainer worldContainer in ClusterManager.Instance.WorldContainers)
			{
				worldContainer.Trigger(631075836, null);
			}
			flag = true;
		}
		if (!this.isNight && this.IsNighttime())
		{
			this.isNight = true;
			base.Trigger(-722330267, null);
		}
		if (this.isNight && !this.IsNighttime())
		{
			this.isNight = false;
		}
		if (flag && SaveGame.Instance.AutoSaveCycleInterval > 0 && this.cycle % SaveGame.Instance.AutoSaveCycleInterval == 0)
		{
			this.DoAutoSave(this.cycle);
		}
		int num = Mathf.FloorToInt(this.timeSinceStartOfCycle - dt / 25f);
		int num2 = Mathf.FloorToInt(this.timeSinceStartOfCycle / 25f);
		if (num != num2)
		{
			base.Trigger(-1215042067, num2);
		}
	}

	// Token: 0x060041AD RID: 16813 RVA: 0x00172688 File Offset: 0x00170888
	public float GetTimeSinceStartOfReport()
	{
		if (this.IsNighttime())
		{
			return 525f - this.GetTimeSinceStartOfCycle();
		}
		return this.GetTimeSinceStartOfCycle() + 75f;
	}

	// Token: 0x060041AE RID: 16814 RVA: 0x001726AB File Offset: 0x001708AB
	public float GetTimeSinceStartOfCycle()
	{
		return this.timeSinceStartOfCycle;
	}

	// Token: 0x060041AF RID: 16815 RVA: 0x001726B3 File Offset: 0x001708B3
	public float GetCurrentCycleAsPercentage()
	{
		return this.timeSinceStartOfCycle / 600f;
	}

	// Token: 0x060041B0 RID: 16816 RVA: 0x001726C1 File Offset: 0x001708C1
	public float GetTime()
	{
		return this.timeSinceStartOfCycle + (float)this.cycle * 600f;
	}

	// Token: 0x060041B1 RID: 16817 RVA: 0x001726D7 File Offset: 0x001708D7
	public float GetTimeInCycles()
	{
		return (float)this.cycle + this.GetCurrentCycleAsPercentage();
	}

	// Token: 0x060041B2 RID: 16818 RVA: 0x001726E7 File Offset: 0x001708E7
	public int GetFrame()
	{
		return this.frame;
	}

	// Token: 0x060041B3 RID: 16819 RVA: 0x001726EF File Offset: 0x001708EF
	public int GetCycle()
	{
		return this.cycle;
	}

	// Token: 0x060041B4 RID: 16820 RVA: 0x001726F7 File Offset: 0x001708F7
	public bool IsNighttime()
	{
		return GameClock.Instance.GetCurrentCycleAsPercentage() >= 0.875f;
	}

	// Token: 0x060041B5 RID: 16821 RVA: 0x0017270D File Offset: 0x0017090D
	public float GetDaytimeDurationInPercentage()
	{
		return 0.875f;
	}

	// Token: 0x060041B6 RID: 16822 RVA: 0x00172714 File Offset: 0x00170914
	public void SetTime(float new_time)
	{
		float num = Mathf.Max(new_time - this.GetTime(), 0f);
		this.AddTime(num);
	}

	// Token: 0x060041B7 RID: 16823 RVA: 0x0017273B File Offset: 0x0017093B
	public float GetTimePlayedInSeconds()
	{
		return this.timePlayed;
	}

	// Token: 0x060041B8 RID: 16824 RVA: 0x00172744 File Offset: 0x00170944
	private void DoAutoSave(int day)
	{
		if (GenericGameSettings.instance.disableAutosave)
		{
			return;
		}
		day++;
		OniMetrics.LogEvent(OniMetrics.Event.EndOfCycle, GameClock.NewCycleKey, day);
		OniMetrics.SendEvent(OniMetrics.Event.EndOfCycle, "DoAutoSave");
		string text = SaveLoader.GetActiveSaveFilePath();
		if (text == null)
		{
			text = SaveLoader.GetAutosaveFilePath();
		}
		int num = text.LastIndexOf("\\");
		if (num > 0)
		{
			int num2 = text.IndexOf(" Cycle ", num);
			if (num2 > 0)
			{
				text = text.Substring(0, num2);
			}
		}
		text = Path.ChangeExtension(text, null);
		text = text + " Cycle " + day.ToString();
		text = SaveScreen.GetValidSaveFilename(text);
		text = Path.Combine(SaveLoader.GetActiveAutoSavePath(), Path.GetFileName(text));
		string text2 = text;
		int num3 = 1;
		while (File.Exists(text))
		{
			text = text2.Replace(".sav", "");
			text = SaveScreen.GetValidSaveFilename(text2 + " (" + num3.ToString() + ")");
			num3++;
		}
		Game.Instance.StartDelayedSave(text, true, false);
	}

	// Token: 0x0400294A RID: 10570
	public static GameClock Instance;

	// Token: 0x0400294B RID: 10571
	[Serialize]
	private int frame;

	// Token: 0x0400294C RID: 10572
	[Serialize]
	private float time;

	// Token: 0x0400294D RID: 10573
	[Serialize]
	private float timeSinceStartOfCycle;

	// Token: 0x0400294E RID: 10574
	[Serialize]
	private int cycle;

	// Token: 0x0400294F RID: 10575
	[Serialize]
	private float timePlayed;

	// Token: 0x04002950 RID: 10576
	[Serialize]
	private bool isNight;

	// Token: 0x04002951 RID: 10577
	public static readonly string NewCycleKey = "NewCycle";
}
