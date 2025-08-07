using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000544 RID: 1348
[AddComponentMenu("KMonoBehaviour/scripts/AudioEventManager")]
public class AudioEventManager : KMonoBehaviour
{
	// Token: 0x06001DD1 RID: 7633 RVA: 0x000A1C04 File Offset: 0x0009FE04
	public static AudioEventManager Get()
	{
		if (AudioEventManager.instance == null)
		{
			if (App.IsExiting)
			{
				return null;
			}
			GameObject gameObject = GameObject.Find("/AudioEventManager");
			if (gameObject == null)
			{
				gameObject = new GameObject();
				gameObject.name = "AudioEventManager";
			}
			AudioEventManager.instance = gameObject.GetComponent<AudioEventManager>();
			if (AudioEventManager.instance == null)
			{
				AudioEventManager.instance = gameObject.AddComponent<AudioEventManager>();
			}
		}
		return AudioEventManager.instance;
	}

	// Token: 0x06001DD2 RID: 7634 RVA: 0x000A1C74 File Offset: 0x0009FE74
	protected override void OnSpawn()
	{
		base.OnPrefabInit();
		this.spatialSplats.Reset(Grid.WidthInCells, Grid.HeightInCells, 16, 16);
	}

	// Token: 0x06001DD3 RID: 7635 RVA: 0x000A1C95 File Offset: 0x0009FE95
	public static float LoudnessToDB(float loudness)
	{
		if (loudness <= 0f)
		{
			return 0f;
		}
		return 10f * Mathf.Log10(loudness);
	}

	// Token: 0x06001DD4 RID: 7636 RVA: 0x000A1CB1 File Offset: 0x0009FEB1
	public static float DBToLoudness(float src_db)
	{
		return Mathf.Pow(10f, src_db / 10f);
	}

	// Token: 0x06001DD5 RID: 7637 RVA: 0x000A1CC4 File Offset: 0x0009FEC4
	public float GetDecibelsAtCell(int cell)
	{
		return Mathf.Round(AudioEventManager.LoudnessToDB(Grid.Loudness[cell]) * 2f) / 2f;
	}

	// Token: 0x06001DD6 RID: 7638 RVA: 0x000A1CE4 File Offset: 0x0009FEE4
	public static string GetLoudestNoisePolluterAtCell(int cell)
	{
		float negativeInfinity = float.NegativeInfinity;
		string text = null;
		AudioEventManager audioEventManager = AudioEventManager.Get();
		Vector2I vector2I = Grid.CellToXY(cell);
		Vector2 vector = new Vector2((float)vector2I.x, (float)vector2I.y);
		foreach (object obj in audioEventManager.spatialSplats.GetAllIntersecting(vector))
		{
			NoiseSplat noiseSplat = (NoiseSplat)obj;
			if (noiseSplat.GetLoudness(cell) > negativeInfinity)
			{
				text = noiseSplat.GetProvider().GetName();
			}
		}
		return text;
	}

	// Token: 0x06001DD7 RID: 7639 RVA: 0x000A1D88 File Offset: 0x0009FF88
	public void ClearNoiseSplat(NoiseSplat splat)
	{
		if (this.splats.Contains(splat))
		{
			this.splats.Remove(splat);
			this.spatialSplats.Remove(splat);
		}
	}

	// Token: 0x06001DD8 RID: 7640 RVA: 0x000A1DB1 File Offset: 0x0009FFB1
	public void AddSplat(NoiseSplat splat)
	{
		this.splats.Add(splat);
		this.spatialSplats.Add(splat);
	}

	// Token: 0x06001DD9 RID: 7641 RVA: 0x000A1DCC File Offset: 0x0009FFCC
	public NoiseSplat CreateNoiseSplat(Vector2 pos, int dB, int radius, string name, GameObject go)
	{
		Polluter polluter = this.GetPolluter(radius);
		polluter.SetAttributes(pos, dB, go, name);
		NoiseSplat noiseSplat = new NoiseSplat(polluter, 0f);
		polluter.SetSplat(noiseSplat);
		return noiseSplat;
	}

	// Token: 0x06001DDA RID: 7642 RVA: 0x000A1E00 File Offset: 0x000A0000
	public List<AudioEventManager.PolluterDisplay> GetPollutersForCell(int cell)
	{
		this.polluters.Clear();
		Vector2I vector2I = Grid.CellToXY(cell);
		Vector2 vector = new Vector2((float)vector2I.x, (float)vector2I.y);
		foreach (object obj in this.spatialSplats.GetAllIntersecting(vector))
		{
			NoiseSplat noiseSplat = (NoiseSplat)obj;
			float loudness = noiseSplat.GetLoudness(cell);
			if (loudness > 0f)
			{
				AudioEventManager.PolluterDisplay polluterDisplay = default(AudioEventManager.PolluterDisplay);
				polluterDisplay.name = noiseSplat.GetName();
				polluterDisplay.value = AudioEventManager.LoudnessToDB(loudness);
				polluterDisplay.provider = noiseSplat.GetProvider();
				this.polluters.Add(polluterDisplay);
			}
		}
		return this.polluters;
	}

	// Token: 0x06001DDB RID: 7643 RVA: 0x000A1ED8 File Offset: 0x000A00D8
	private void RemoveExpiredSplats()
	{
		if (this.removeTime.Count > 1)
		{
			this.removeTime.Sort((Pair<float, NoiseSplat> a, Pair<float, NoiseSplat> b) => a.first.CompareTo(b.first));
		}
		int num = -1;
		int num2 = 0;
		while (num2 < this.removeTime.Count && this.removeTime[num2].first <= Time.time)
		{
			NoiseSplat second = this.removeTime[num2].second;
			if (second != null)
			{
				IPolluter provider = second.GetProvider();
				this.FreePolluter(provider as Polluter);
			}
			num = num2;
			num2++;
		}
		for (int i = num; i >= 0; i--)
		{
			this.removeTime.RemoveAt(i);
		}
	}

	// Token: 0x06001DDC RID: 7644 RVA: 0x000A1F94 File Offset: 0x000A0194
	private void Update()
	{
		this.RemoveExpiredSplats();
	}

	// Token: 0x06001DDD RID: 7645 RVA: 0x000A1F9C File Offset: 0x000A019C
	private Polluter GetPolluter(int radius)
	{
		if (!this.freePool.ContainsKey(radius))
		{
			this.freePool.Add(radius, new List<Polluter>());
		}
		Polluter polluter;
		if (this.freePool[radius].Count > 0)
		{
			polluter = this.freePool[radius][0];
			this.freePool[radius].RemoveAt(0);
		}
		else
		{
			polluter = new Polluter(radius);
		}
		if (!this.inusePool.ContainsKey(radius))
		{
			this.inusePool.Add(radius, new List<Polluter>());
		}
		this.inusePool[radius].Add(polluter);
		return polluter;
	}

	// Token: 0x06001DDE RID: 7646 RVA: 0x000A2040 File Offset: 0x000A0240
	private void FreePolluter(Polluter pol)
	{
		if (pol != null)
		{
			pol.Clear();
			global::Debug.Assert(this.inusePool[pol.radius].Contains(pol));
			this.inusePool[pol.radius].Remove(pol);
			this.freePool[pol.radius].Add(pol);
		}
	}

	// Token: 0x06001DDF RID: 7647 RVA: 0x000A20A4 File Offset: 0x000A02A4
	public void PlayTimedOnceOff(Vector2 pos, int dB, int radius, string name, GameObject go, float time = 1f)
	{
		if (dB > 0 && radius > 0 && time > 0f)
		{
			Polluter polluter = this.GetPolluter(radius);
			polluter.SetAttributes(pos, dB, go, name);
			this.AddTimedInstance(polluter, time);
		}
	}

	// Token: 0x06001DE0 RID: 7648 RVA: 0x000A20E0 File Offset: 0x000A02E0
	private void AddTimedInstance(Polluter p, float time)
	{
		NoiseSplat noiseSplat = new NoiseSplat(p, time + Time.time);
		p.SetSplat(noiseSplat);
		this.removeTime.Add(new Pair<float, NoiseSplat>(time + Time.time, noiseSplat));
	}

	// Token: 0x06001DE1 RID: 7649 RVA: 0x000A211A File Offset: 0x000A031A
	private static void SoundLog(long itemId, string message)
	{
		global::Debug.Log(" [" + itemId.ToString() + "] \t" + message);
	}

	// Token: 0x0400115D RID: 4445
	public const float NO_NOISE_EFFECTORS = 0f;

	// Token: 0x0400115E RID: 4446
	public const float MIN_LOUDNESS_THRESHOLD = 1f;

	// Token: 0x0400115F RID: 4447
	private static AudioEventManager instance;

	// Token: 0x04001160 RID: 4448
	private List<Pair<float, NoiseSplat>> removeTime = new List<Pair<float, NoiseSplat>>();

	// Token: 0x04001161 RID: 4449
	private Dictionary<int, List<Polluter>> freePool = new Dictionary<int, List<Polluter>>();

	// Token: 0x04001162 RID: 4450
	private Dictionary<int, List<Polluter>> inusePool = new Dictionary<int, List<Polluter>>();

	// Token: 0x04001163 RID: 4451
	private HashSet<NoiseSplat> splats = new HashSet<NoiseSplat>();

	// Token: 0x04001164 RID: 4452
	private UniformGrid<NoiseSplat> spatialSplats = new UniformGrid<NoiseSplat>();

	// Token: 0x04001165 RID: 4453
	private List<AudioEventManager.PolluterDisplay> polluters = new List<AudioEventManager.PolluterDisplay>();

	// Token: 0x02001398 RID: 5016
	public enum NoiseEffect
	{
		// Token: 0x040069ED RID: 27117
		Peaceful,
		// Token: 0x040069EE RID: 27118
		Quiet = 36,
		// Token: 0x040069EF RID: 27119
		TossAndTurn = 45,
		// Token: 0x040069F0 RID: 27120
		WakeUp = 60,
		// Token: 0x040069F1 RID: 27121
		Passive = 80,
		// Token: 0x040069F2 RID: 27122
		Active = 106
	}

	// Token: 0x02001399 RID: 5017
	public struct PolluterDisplay
	{
		// Token: 0x040069F3 RID: 27123
		public string name;

		// Token: 0x040069F4 RID: 27124
		public float value;

		// Token: 0x040069F5 RID: 27125
		public IPolluter provider;
	}
}
