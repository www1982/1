using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000617 RID: 1559
[AddComponentMenu("KMonoBehaviour/scripts/SpriteSheetAnimManager")]
public class SpriteSheetAnimManager : KMonoBehaviour, IRenderEveryTick
{
	// Token: 0x06002503 RID: 9475 RVA: 0x000D37C0 File Offset: 0x000D19C0
	public static void DestroyInstance()
	{
		SpriteSheetAnimManager.instance = null;
	}

	// Token: 0x06002504 RID: 9476 RVA: 0x000D37C8 File Offset: 0x000D19C8
	protected override void OnPrefabInit()
	{
		SpriteSheetAnimManager.instance = this;
	}

	// Token: 0x06002505 RID: 9477 RVA: 0x000D37D0 File Offset: 0x000D19D0
	protected override void OnSpawn()
	{
		for (int i = 0; i < this.sheets.Length; i++)
		{
			int num = Hash.SDBMLower(this.sheets[i].name);
			this.nameIndexMap[num] = new SpriteSheetAnimator(this.sheets[i]);
		}
	}

	// Token: 0x06002506 RID: 9478 RVA: 0x000D3824 File Offset: 0x000D1A24
	public void Play(string name, Vector3 pos, Vector2 size, Color32 colour)
	{
		int num = Hash.SDBMLower(name);
		this.Play(num, pos, Quaternion.identity, size, colour);
	}

	// Token: 0x06002507 RID: 9479 RVA: 0x000D3848 File Offset: 0x000D1A48
	public void Play(string name, Vector3 pos, Quaternion rotation, Vector2 size, Color32 colour)
	{
		int num = Hash.SDBMLower(name);
		this.Play(num, pos, rotation, size, colour);
	}

	// Token: 0x06002508 RID: 9480 RVA: 0x000D3869 File Offset: 0x000D1A69
	public void Play(int name_hash, Vector3 pos, Quaternion rotation, Vector2 size, Color32 colour)
	{
		this.nameIndexMap[name_hash].Play(pos, rotation, size, colour);
	}

	// Token: 0x06002509 RID: 9481 RVA: 0x000D3887 File Offset: 0x000D1A87
	public void RenderEveryTick(float dt)
	{
		this.UpdateAnims(dt);
		this.Render();
	}

	// Token: 0x0600250A RID: 9482 RVA: 0x000D3898 File Offset: 0x000D1A98
	public void UpdateAnims(float dt)
	{
		foreach (KeyValuePair<int, SpriteSheetAnimator> keyValuePair in this.nameIndexMap)
		{
			keyValuePair.Value.UpdateAnims(dt);
		}
	}

	// Token: 0x0600250B RID: 9483 RVA: 0x000D38F4 File Offset: 0x000D1AF4
	public void Render()
	{
		Vector3 zero = Vector3.zero;
		foreach (KeyValuePair<int, SpriteSheetAnimator> keyValuePair in this.nameIndexMap)
		{
			keyValuePair.Value.Render();
		}
	}

	// Token: 0x0600250C RID: 9484 RVA: 0x000D3954 File Offset: 0x000D1B54
	public SpriteSheetAnimator GetSpriteSheetAnimator(HashedString name)
	{
		return this.nameIndexMap[name.HashValue];
	}

	// Token: 0x040015BE RID: 5566
	public const float SECONDS_PER_FRAME = 0.033333335f;

	// Token: 0x040015BF RID: 5567
	[SerializeField]
	private SpriteSheet[] sheets;

	// Token: 0x040015C0 RID: 5568
	private Dictionary<int, SpriteSheetAnimator> nameIndexMap = new Dictionary<int, SpriteSheetAnimator>();

	// Token: 0x040015C1 RID: 5569
	public static SpriteSheetAnimManager instance;
}
