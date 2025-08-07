using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000556 RID: 1366
public class NoiseSplat : IUniformGridObject
{
	// Token: 0x170000EF RID: 239
	// (get) Token: 0x06001E1F RID: 7711 RVA: 0x000A3DEA File Offset: 0x000A1FEA
	// (set) Token: 0x06001E20 RID: 7712 RVA: 0x000A3DF2 File Offset: 0x000A1FF2
	public int dB { get; private set; }

	// Token: 0x170000F0 RID: 240
	// (get) Token: 0x06001E21 RID: 7713 RVA: 0x000A3DFB File Offset: 0x000A1FFB
	// (set) Token: 0x06001E22 RID: 7714 RVA: 0x000A3E03 File Offset: 0x000A2003
	public float deathTime { get; private set; }

	// Token: 0x06001E23 RID: 7715 RVA: 0x000A3E0C File Offset: 0x000A200C
	public string GetName()
	{
		return this.provider.GetName();
	}

	// Token: 0x06001E24 RID: 7716 RVA: 0x000A3E19 File Offset: 0x000A2019
	public IPolluter GetProvider()
	{
		return this.provider;
	}

	// Token: 0x06001E25 RID: 7717 RVA: 0x000A3E21 File Offset: 0x000A2021
	public Vector2 PosMin()
	{
		return new Vector2(this.position.x - (float)this.radius, this.position.y - (float)this.radius);
	}

	// Token: 0x06001E26 RID: 7718 RVA: 0x000A3E4E File Offset: 0x000A204E
	public Vector2 PosMax()
	{
		return new Vector2(this.position.x + (float)this.radius, this.position.y + (float)this.radius);
	}

	// Token: 0x06001E27 RID: 7719 RVA: 0x000A3E7C File Offset: 0x000A207C
	public NoiseSplat(NoisePolluter setProvider, float death_time = 0f)
	{
		this.deathTime = death_time;
		this.dB = 0;
		this.radius = 5;
		if (setProvider.dB != null)
		{
			this.dB = (int)setProvider.dB.GetTotalValue();
		}
		int num = Grid.PosToCell(setProvider.gameObject);
		if (!NoisePolluter.IsNoiseableCell(num))
		{
			this.dB = 0;
		}
		if (this.dB == 0)
		{
			return;
		}
		setProvider.Clear();
		OccupyArea occupyArea = setProvider.occupyArea;
		this.baseExtents = occupyArea.GetExtents();
		this.provider = setProvider;
		this.position = setProvider.transform.GetPosition();
		if (setProvider.dBRadius != null)
		{
			this.radius = (int)setProvider.dBRadius.GetTotalValue();
		}
		if (this.radius == 0)
		{
			return;
		}
		int num2 = 0;
		int num3 = 0;
		Grid.CellToXY(num, out num2, out num3);
		int widthInCells = occupyArea.GetWidthInCells();
		int heightInCells = occupyArea.GetHeightInCells();
		Vector2I vector2I = new Vector2I(num2 - this.radius, num3 - this.radius);
		Vector2I vector2I2 = vector2I + new Vector2I(this.radius * 2 + widthInCells, this.radius * 2 + heightInCells);
		vector2I = Vector2I.Max(vector2I, Vector2I.zero);
		vector2I2 = Vector2I.Min(vector2I2, new Vector2I(Grid.WidthInCells - 1, Grid.HeightInCells - 1));
		this.effectExtents = new Extents(vector2I.x, vector2I.y, vector2I2.x - vector2I.x, vector2I2.y - vector2I.y);
		this.partitionerEntry = GameScenePartitioner.Instance.Add("NoiseSplat.SplatCollectNoisePolluters", setProvider.gameObject, this.effectExtents, GameScenePartitioner.Instance.noisePolluterLayer, setProvider.onCollectNoisePollutersCallback);
		this.solidChangedPartitionerEntry = GameScenePartitioner.Instance.Add("NoiseSplat.SplatSolidCheck", setProvider.gameObject, this.effectExtents, GameScenePartitioner.Instance.solidChangedLayer, setProvider.refreshPartionerCallback);
	}

	// Token: 0x06001E28 RID: 7720 RVA: 0x000A4064 File Offset: 0x000A2264
	public NoiseSplat(IPolluter setProvider, float death_time = 0f)
	{
		this.deathTime = death_time;
		this.provider = setProvider;
		this.provider.Clear();
		this.position = this.provider.GetPosition();
		this.dB = this.provider.GetNoise();
		int num = Grid.PosToCell(this.position);
		if (!NoisePolluter.IsNoiseableCell(num))
		{
			this.dB = 0;
		}
		if (this.dB == 0)
		{
			return;
		}
		this.radius = this.provider.GetRadius();
		if (this.radius == 0)
		{
			return;
		}
		int num2 = 0;
		int num3 = 0;
		Grid.CellToXY(num, out num2, out num3);
		Vector2I vector2I = new Vector2I(num2 - this.radius, num3 - this.radius);
		Vector2I vector2I2 = vector2I + new Vector2I(this.radius * 2, this.radius * 2);
		vector2I = Vector2I.Max(vector2I, Vector2I.zero);
		vector2I2 = Vector2I.Min(vector2I2, new Vector2I(Grid.WidthInCells - 1, Grid.HeightInCells - 1));
		this.effectExtents = new Extents(vector2I.x, vector2I.y, vector2I2.x - vector2I.x, vector2I2.y - vector2I.y);
		this.baseExtents = new Extents(num2, num3, 1, 1);
		this.AddNoise();
	}

	// Token: 0x06001E29 RID: 7721 RVA: 0x000A41AD File Offset: 0x000A23AD
	public void Clear()
	{
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
		GameScenePartitioner.Instance.Free(ref this.solidChangedPartitionerEntry);
		this.RemoveNoise();
	}

	// Token: 0x06001E2A RID: 7722 RVA: 0x000A41D8 File Offset: 0x000A23D8
	private void AddNoise()
	{
		int num = Grid.PosToCell(this.position);
		int num2 = this.effectExtents.x + this.effectExtents.width;
		int num3 = this.effectExtents.y + this.effectExtents.height;
		int num4 = this.effectExtents.x;
		int num5 = this.effectExtents.y;
		int num6 = 0;
		int num7 = 0;
		Grid.CellToXY(num, out num6, out num7);
		num2 = Math.Min(num2, Grid.WidthInCells);
		num3 = Math.Min(num3, Grid.HeightInCells);
		num4 = Math.Max(0, num4);
		num5 = Math.Max(0, num5);
		for (int i = num5; i < num3; i++)
		{
			for (int j = num4; j < num2; j++)
			{
				if (Grid.VisibilityTest(num6, num7, j, i, false))
				{
					int num8 = Grid.XYToCell(j, i);
					float dbforCell = this.GetDBForCell(num8);
					if (dbforCell > 0f)
					{
						float num9 = AudioEventManager.DBToLoudness(dbforCell);
						Grid.Loudness[num8] += num9;
						Pair<int, float> pair = new Pair<int, float>(num8, num9);
						this.decibels.Add(pair);
					}
				}
			}
		}
	}

	// Token: 0x06001E2B RID: 7723 RVA: 0x000A42F0 File Offset: 0x000A24F0
	public float GetDBForCell(int cell)
	{
		Vector2 vector = Grid.CellToPos2D(cell);
		float num = Mathf.Floor(Vector2.Distance(this.position, vector));
		if (vector.x >= (float)this.baseExtents.x && vector.x < (float)(this.baseExtents.x + this.baseExtents.width) && vector.y >= (float)this.baseExtents.y && vector.y < (float)(this.baseExtents.y + this.baseExtents.height))
		{
			num = 0f;
		}
		return Mathf.Round((float)this.dB - (float)this.dB * num * 0.05f);
	}

	// Token: 0x06001E2C RID: 7724 RVA: 0x000A43A8 File Offset: 0x000A25A8
	private void RemoveNoise()
	{
		for (int i = 0; i < this.decibels.Count; i++)
		{
			Pair<int, float> pair = this.decibels[i];
			float num = Math.Max(0f, Grid.Loudness[pair.first] - pair.second);
			Grid.Loudness[pair.first] = ((num < 1f) ? 0f : num);
		}
		this.decibels.Clear();
	}

	// Token: 0x06001E2D RID: 7725 RVA: 0x000A4420 File Offset: 0x000A2620
	public float GetLoudness(int cell)
	{
		float num = 0f;
		for (int i = 0; i < this.decibels.Count; i++)
		{
			Pair<int, float> pair = this.decibels[i];
			if (pair.first == cell)
			{
				num = pair.second;
				break;
			}
		}
		return num;
	}

	// Token: 0x0400118A RID: 4490
	public const float noiseFalloff = 0.05f;

	// Token: 0x0400118D RID: 4493
	private IPolluter provider;

	// Token: 0x0400118E RID: 4494
	private Vector2 position;

	// Token: 0x0400118F RID: 4495
	private int radius;

	// Token: 0x04001190 RID: 4496
	private Extents effectExtents;

	// Token: 0x04001191 RID: 4497
	private Extents baseExtents;

	// Token: 0x04001192 RID: 4498
	private HandleVector<int>.Handle partitionerEntry;

	// Token: 0x04001193 RID: 4499
	private HandleVector<int>.Handle solidChangedPartitionerEntry;

	// Token: 0x04001194 RID: 4500
	private List<Pair<int, float>> decibels = new List<Pair<int, float>>();
}
