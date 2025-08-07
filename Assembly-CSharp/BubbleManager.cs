using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006C2 RID: 1730
[AddComponentMenu("KMonoBehaviour/scripts/BubbleManager")]
public class BubbleManager : KMonoBehaviour, ISim33ms, IRenderEveryTick
{
	// Token: 0x06002A88 RID: 10888 RVA: 0x000F6182 File Offset: 0x000F4382
	public static void DestroyInstance()
	{
		BubbleManager.instance = null;
	}

	// Token: 0x06002A89 RID: 10889 RVA: 0x000F618A File Offset: 0x000F438A
	protected override void OnPrefabInit()
	{
		BubbleManager.instance = this;
	}

	// Token: 0x06002A8A RID: 10890 RVA: 0x000F6194 File Offset: 0x000F4394
	public void SpawnBubble(Vector2 position, Vector2 velocity, SimHashes element, float mass, float temperature)
	{
		BubbleManager.Bubble bubble = new BubbleManager.Bubble
		{
			position = position,
			velocity = velocity,
			element = element,
			temperature = temperature,
			mass = mass
		};
		this.bubbles.Add(bubble);
	}

	// Token: 0x06002A8B RID: 10891 RVA: 0x000F61E4 File Offset: 0x000F43E4
	public void Sim33ms(float dt)
	{
		ListPool<BubbleManager.Bubble, BubbleManager>.PooledList pooledList = ListPool<BubbleManager.Bubble, BubbleManager>.Allocate();
		ListPool<BubbleManager.Bubble, BubbleManager>.PooledList pooledList2 = ListPool<BubbleManager.Bubble, BubbleManager>.Allocate();
		foreach (BubbleManager.Bubble bubble in this.bubbles)
		{
			bubble.position += bubble.velocity * dt;
			bubble.elapsedTime += dt;
			int num = Grid.PosToCell(bubble.position);
			if (!Grid.IsVisiblyInLiquid(bubble.position) || Grid.Element[num].id == bubble.element)
			{
				pooledList2.Add(bubble);
			}
			else
			{
				pooledList.Add(bubble);
			}
		}
		foreach (BubbleManager.Bubble bubble2 in pooledList2)
		{
			SimMessages.AddRemoveSubstance(Grid.PosToCell(bubble2.position), bubble2.element, CellEventLogger.Instance.FallingWaterAddToSim, bubble2.mass, bubble2.temperature, byte.MaxValue, 0, true, -1);
		}
		this.bubbles.Clear();
		this.bubbles.AddRange(pooledList);
		pooledList2.Recycle();
		pooledList.Recycle();
	}

	// Token: 0x06002A8C RID: 10892 RVA: 0x000F633C File Offset: 0x000F453C
	public void RenderEveryTick(float dt)
	{
		ListPool<SpriteSheetAnimator.AnimInfo, BubbleManager>.PooledList pooledList = ListPool<SpriteSheetAnimator.AnimInfo, BubbleManager>.Allocate();
		SpriteSheetAnimator spriteSheetAnimator = SpriteSheetAnimManager.instance.GetSpriteSheetAnimator("liquid_splash1");
		foreach (BubbleManager.Bubble bubble in this.bubbles)
		{
			SpriteSheetAnimator.AnimInfo animInfo = new SpriteSheetAnimator.AnimInfo
			{
				frame = spriteSheetAnimator.GetFrameFromElapsedTimeLooping(bubble.elapsedTime),
				elapsedTime = bubble.elapsedTime,
				pos = new Vector3(bubble.position.x, bubble.position.y, 0f),
				rotation = Quaternion.identity,
				size = Vector2.one,
				colour = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue)
			};
			pooledList.Add(animInfo);
		}
		pooledList.Recycle();
	}

	// Token: 0x0400191A RID: 6426
	public static BubbleManager instance;

	// Token: 0x0400191B RID: 6427
	private List<BubbleManager.Bubble> bubbles = new List<BubbleManager.Bubble>();

	// Token: 0x02001548 RID: 5448
	private struct Bubble
	{
		// Token: 0x04006F32 RID: 28466
		public Vector2 position;

		// Token: 0x04006F33 RID: 28467
		public Vector2 velocity;

		// Token: 0x04006F34 RID: 28468
		public float elapsedTime;

		// Token: 0x04006F35 RID: 28469
		public int frame;

		// Token: 0x04006F36 RID: 28470
		public SimHashes element;

		// Token: 0x04006F37 RID: 28471
		public float temperature;

		// Token: 0x04006F38 RID: 28472
		public float mass;
	}
}
