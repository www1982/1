using System;
using System.Collections.Generic;
using Klei;
using UnityEngine;

// Token: 0x02000D9C RID: 3484
public class PopFXManager : KScreen
{
	// Token: 0x06006D10 RID: 27920 RVA: 0x00294A41 File Offset: 0x00292C41
	public static void DestroyInstance()
	{
		PopFXManager.Instance = null;
	}

	// Token: 0x06006D11 RID: 27921 RVA: 0x00294A49 File Offset: 0x00292C49
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		PopFXManager.Instance = this;
	}

	// Token: 0x06006D12 RID: 27922 RVA: 0x00294A58 File Offset: 0x00292C58
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.ready = true;
		if (GenericGameSettings.instance.disablePopFx)
		{
			return;
		}
		for (int i = 0; i < 20; i++)
		{
			PopFX popFX = this.CreatePopFX();
			this.Pool.Add(popFX);
		}
	}

	// Token: 0x06006D13 RID: 27923 RVA: 0x00294A9F File Offset: 0x00292C9F
	public bool Ready()
	{
		return this.ready;
	}

	// Token: 0x06006D14 RID: 27924 RVA: 0x00294AA8 File Offset: 0x00292CA8
	public PopFX SpawnFX(Sprite icon, string text, Transform target_transform, Vector3 offset, float lifetime = 1.5f, bool track_target = false, bool force_spawn = false)
	{
		if (GenericGameSettings.instance.disablePopFx)
		{
			return null;
		}
		if (Game.IsQuitting())
		{
			return null;
		}
		Vector3 vector = offset;
		if (target_transform != null)
		{
			vector += target_transform.GetPosition();
		}
		if (!force_spawn)
		{
			int num = Grid.PosToCell(vector);
			if (!Grid.IsValidCell(num) || !Grid.IsVisible(num))
			{
				return null;
			}
		}
		PopFX popFX;
		if (this.Pool.Count > 0)
		{
			popFX = this.Pool[0];
			this.Pool[0].gameObject.SetActive(true);
			this.Pool[0].Spawn(icon, text, target_transform, offset, lifetime, track_target);
			this.Pool.RemoveAt(0);
		}
		else
		{
			popFX = this.CreatePopFX();
			popFX.gameObject.SetActive(true);
			popFX.Spawn(icon, text, target_transform, offset, lifetime, track_target);
		}
		return popFX;
	}

	// Token: 0x06006D15 RID: 27925 RVA: 0x00294B81 File Offset: 0x00292D81
	public PopFX SpawnFX(Sprite icon, string text, Transform target_transform, float lifetime = 1.5f, bool track_target = false)
	{
		return this.SpawnFX(icon, text, target_transform, Vector3.zero, lifetime, track_target, false);
	}

	// Token: 0x06006D16 RID: 27926 RVA: 0x00294B96 File Offset: 0x00292D96
	private PopFX CreatePopFX()
	{
		GameObject gameObject = Util.KInstantiate(this.Prefab_PopFX, base.gameObject, "Pooled_PopFX");
		gameObject.transform.localScale = Vector3.one;
		return gameObject.GetComponent<PopFX>();
	}

	// Token: 0x06006D17 RID: 27927 RVA: 0x00294BC3 File Offset: 0x00292DC3
	public void RecycleFX(PopFX fx)
	{
		this.Pool.Add(fx);
	}

	// Token: 0x04004A6F RID: 19055
	public static PopFXManager Instance;

	// Token: 0x04004A70 RID: 19056
	public GameObject Prefab_PopFX;

	// Token: 0x04004A71 RID: 19057
	public List<PopFX> Pool = new List<PopFX>();

	// Token: 0x04004A72 RID: 19058
	public Sprite sprite_Plus;

	// Token: 0x04004A73 RID: 19059
	public Sprite sprite_Negative;

	// Token: 0x04004A74 RID: 19060
	public Sprite sprite_Resource;

	// Token: 0x04004A75 RID: 19061
	public Sprite sprite_Building;

	// Token: 0x04004A76 RID: 19062
	public Sprite sprite_Research;

	// Token: 0x04004A77 RID: 19063
	private bool ready;
}
