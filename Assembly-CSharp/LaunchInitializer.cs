using System;
using System.IO;
using System.Threading;
using UnityEngine;

// Token: 0x020009A3 RID: 2467
public class LaunchInitializer : MonoBehaviour
{
	// Token: 0x060047B0 RID: 18352 RVA: 0x0019D7E1 File Offset: 0x0019B9E1
	public static string BuildPrefix()
	{
		return LaunchInitializer.BUILD_PREFIX;
	}

	// Token: 0x060047B1 RID: 18353 RVA: 0x0019D7E8 File Offset: 0x0019B9E8
	public static int UpdateNumber()
	{
		return 56;
	}

	// Token: 0x060047B2 RID: 18354 RVA: 0x0019D7EC File Offset: 0x0019B9EC
	private void Update()
	{
		if (this.numWaitFrames > Time.renderedFrameCount)
		{
			return;
		}
		if (!DistributionPlatform.Initialized)
		{
			if (!SystemInfo.SupportsTextureFormat(TextureFormat.RGBAFloat))
			{
				global::Debug.LogError("Machine does not support RGBAFloat32");
			}
			GraphicsOptionsScreen.SetSettingsFromPrefs();
			Util.ApplyInvariantCultureToThread(Thread.CurrentThread);
			global::Debug.Log("Date: " + global::System.DateTime.Now.ToString());
			global::Debug.Log("Build: " + BuildWatermark.GetBuildText() + " (release)");
			global::UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			KPlayerPrefs.instance.Load();
			DistributionPlatform.Initialize();
		}
		if (!DistributionPlatform.Inst.IsDLCStatusReady())
		{
			return;
		}
		global::Debug.Log("DistributionPlatform initialized.");
		DebugUtil.LogArgs(new object[] { DebugUtil.LINE });
		global::Debug.Log("Build: " + BuildWatermark.GetBuildText() + " (release)");
		DebugUtil.LogArgs(new object[] { DebugUtil.LINE });
		DebugUtil.LogArgs(new object[] { "DLC Information" });
		foreach (string text in DlcManager.GetOwnedDLCIds())
		{
			global::Debug.Log(string.Format("- {0} loaded: {1}", text, DlcManager.IsContentSubscribed(text)));
		}
		DebugUtil.LogArgs(new object[] { DebugUtil.LINE });
		KFMOD.Initialize();
		for (int i = 0; i < this.SpawnPrefabs.Length; i++)
		{
			GameObject gameObject = this.SpawnPrefabs[i];
			if (gameObject != null)
			{
				Util.KInstantiate(gameObject, base.gameObject, null);
			}
		}
		LaunchInitializer.DeleteLingeringFiles();
		base.enabled = false;
	}

	// Token: 0x060047B3 RID: 18355 RVA: 0x0019D99C File Offset: 0x0019BB9C
	private static void DeleteLingeringFiles()
	{
		string[] array = new string[] { "fmod.log", "load_stats_0.json", "OxygenNotIncluded_Data/output_log.txt" };
		string directoryName = Path.GetDirectoryName(Application.dataPath);
		foreach (string text in array)
		{
			string text2 = Path.Combine(directoryName, text);
			try
			{
				if (File.Exists(text2))
				{
					File.Delete(text2);
				}
			}
			catch (Exception ex)
			{
				global::Debug.LogWarning(ex);
			}
		}
	}

	// Token: 0x04002F76 RID: 12150
	private const string PREFIX = "U";

	// Token: 0x04002F77 RID: 12151
	private const int UPDATE_NUMBER = 56;

	// Token: 0x04002F78 RID: 12152
	private static readonly string BUILD_PREFIX = "U" + 56.ToString();

	// Token: 0x04002F79 RID: 12153
	public GameObject[] SpawnPrefabs;

	// Token: 0x04002F7A RID: 12154
	[SerializeField]
	private int numWaitFrames = 1;
}
