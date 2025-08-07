using System;
using UnityEngine;

// Token: 0x02000A34 RID: 2612
public class Polluter : IPolluter
{
	// Token: 0x1700052E RID: 1326
	// (get) Token: 0x06004BB9 RID: 19385 RVA: 0x001B7148 File Offset: 0x001B5348
	// (set) Token: 0x06004BBA RID: 19386 RVA: 0x001B7150 File Offset: 0x001B5350
	public int radius
	{
		get
		{
			return this._radius;
		}
		private set
		{
			this._radius = value;
			if (this._radius == 0)
			{
				global::Debug.LogFormat("[{0}] has a 0 radius noise, this will disable it", new object[] { this.GetName() });
				return;
			}
		}
	}

	// Token: 0x06004BBB RID: 19387 RVA: 0x001B717B File Offset: 0x001B537B
	public void SetAttributes(Vector2 pos, int dB, GameObject go, string name)
	{
		this.position = pos;
		this.sourceName = name;
		this.decibels = dB;
		this.gameObject = go;
	}

	// Token: 0x06004BBC RID: 19388 RVA: 0x001B719A File Offset: 0x001B539A
	public string GetName()
	{
		return this.sourceName;
	}

	// Token: 0x06004BBD RID: 19389 RVA: 0x001B71A2 File Offset: 0x001B53A2
	public int GetRadius()
	{
		return this.radius;
	}

	// Token: 0x06004BBE RID: 19390 RVA: 0x001B71AA File Offset: 0x001B53AA
	public int GetNoise()
	{
		return this.decibels;
	}

	// Token: 0x06004BBF RID: 19391 RVA: 0x001B71B2 File Offset: 0x001B53B2
	public GameObject GetGameObject()
	{
		return this.gameObject;
	}

	// Token: 0x06004BC0 RID: 19392 RVA: 0x001B71BA File Offset: 0x001B53BA
	public Polluter(int radius)
	{
		this.radius = radius;
	}

	// Token: 0x06004BC1 RID: 19393 RVA: 0x001B71C9 File Offset: 0x001B53C9
	public void SetSplat(NoiseSplat new_splat)
	{
		if (new_splat == null && this.splat != null)
		{
			this.Clear();
		}
		this.splat = new_splat;
		if (this.splat != null)
		{
			AudioEventManager.Get().AddSplat(this.splat);
		}
	}

	// Token: 0x06004BC2 RID: 19394 RVA: 0x001B71FB File Offset: 0x001B53FB
	public void Clear()
	{
		if (this.splat != null)
		{
			AudioEventManager.Get().ClearNoiseSplat(this.splat);
			this.splat.Clear();
			this.splat = null;
		}
	}

	// Token: 0x06004BC3 RID: 19395 RVA: 0x001B7227 File Offset: 0x001B5427
	public Vector2 GetPosition()
	{
		return this.position;
	}

	// Token: 0x04003235 RID: 12853
	private int _radius;

	// Token: 0x04003236 RID: 12854
	private int decibels;

	// Token: 0x04003237 RID: 12855
	private Vector2 position;

	// Token: 0x04003238 RID: 12856
	private string sourceName;

	// Token: 0x04003239 RID: 12857
	private GameObject gameObject;

	// Token: 0x0400323A RID: 12858
	private NoiseSplat splat;
}
