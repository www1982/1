using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A49 RID: 2633
public class OreSizeVisualizerComponents : KGameObjectComponentManager<OreSizeVisualizerData>
{
	// Token: 0x06004C5F RID: 19551 RVA: 0x001BB328 File Offset: 0x001B9528
	public HandleVector<int>.Handle Add(GameObject go)
	{
		HandleVector<int>.Handle handle = base.Add(go, new OreSizeVisualizerData(go));
		this.OnPrefabInit(handle);
		return handle;
	}

	// Token: 0x06004C60 RID: 19552 RVA: 0x001BB34B File Offset: 0x001B954B
	public static HashedString GetAnimForMass(float mass)
	{
		return OreSizeVisualizerComponents.GetAnimForMass(OreSizeVisualizerComponents.TiersSetType.Ores, mass);
	}

	// Token: 0x06004C61 RID: 19553 RVA: 0x001BB354 File Offset: 0x001B9554
	public static HashedString GetAnimForMass(OreSizeVisualizerComponents.TiersSetType tierType, float mass)
	{
		for (int i = 0; i < OreSizeVisualizerComponents.TierSets[tierType].Length; i++)
		{
			if (mass <= OreSizeVisualizerComponents.TierSets[tierType][i].massRequired)
			{
				return OreSizeVisualizerComponents.TierSets[tierType][i].animName;
			}
		}
		return HashedString.Invalid;
	}

	// Token: 0x06004C62 RID: 19554 RVA: 0x001BB3B0 File Offset: 0x001B95B0
	protected override void OnPrefabInit(HandleVector<int>.Handle handle)
	{
		Action<object> action = delegate(object ev_data)
		{
			OreSizeVisualizerComponents.OnMassChanged(handle, ev_data);
		};
		OreSizeVisualizerData data = base.GetData(handle);
		data.onMassChangedCB = action;
		data.primaryElement.Subscribe(-2064133523, action);
		data.primaryElement.Subscribe(1335436905, action);
		base.SetData(handle, data);
	}

	// Token: 0x06004C63 RID: 19555 RVA: 0x001BB41D File Offset: 0x001B961D
	protected override void OnSpawn(HandleVector<int>.Handle handle)
	{
		base.GetData(handle);
		OreSizeVisualizerComponents.OnMassChanged(handle, null);
	}

	// Token: 0x06004C64 RID: 19556 RVA: 0x001BB430 File Offset: 0x001B9630
	protected override void OnCleanUp(HandleVector<int>.Handle handle)
	{
		OreSizeVisualizerData data = base.GetData(handle);
		if (data.primaryElement != null)
		{
			Action<object> onMassChangedCB = data.onMassChangedCB;
			data.primaryElement.Unsubscribe(-2064133523, onMassChangedCB);
			data.primaryElement.Unsubscribe(1335436905, onMassChangedCB);
		}
	}

	// Token: 0x06004C65 RID: 19557 RVA: 0x001BB47C File Offset: 0x001B967C
	private static void OnMassChanged(HandleVector<int>.Handle handle, object other_data)
	{
		OreSizeVisualizerData data = GameComps.OreSizeVisualizers.GetData(handle);
		PrimaryElement primaryElement = data.primaryElement;
		float mass = primaryElement.Mass;
		OreSizeVisualizerComponents.MassTier massTier = default(OreSizeVisualizerComponents.MassTier);
		OreSizeVisualizerComponents.MassTier[] array = OreSizeVisualizerComponents.TierSets[data.tierSetType];
		for (int i = 0; i < array.Length; i++)
		{
			if (mass <= array[i].massRequired)
			{
				massTier = array[i];
				break;
			}
		}
		primaryElement.GetComponent<KBatchedAnimController>().Play(massTier.animName, KAnim.PlayMode.Once, 1f, 0f);
		KCircleCollider2D component = primaryElement.GetComponent<KCircleCollider2D>();
		if (component != null)
		{
			component.radius = massTier.colliderRadius;
		}
		primaryElement.Trigger(1807976145, null);
	}

	// Token: 0x06004C67 RID: 19559 RVA: 0x001BB53C File Offset: 0x001B973C
	// Note: this type is marked as 'beforefieldinit'.
	static OreSizeVisualizerComponents()
	{
		Dictionary<OreSizeVisualizerComponents.TiersSetType, OreSizeVisualizerComponents.MassTier[]> dictionary = new Dictionary<OreSizeVisualizerComponents.TiersSetType, OreSizeVisualizerComponents.MassTier[]>();
		dictionary[OreSizeVisualizerComponents.TiersSetType.Ores] = OreSizeVisualizerComponents.MassTiers;
		dictionary[OreSizeVisualizerComponents.TiersSetType.PokeShells] = OreSizeVisualizerComponents.PokeShellMassTiers;
		dictionary[OreSizeVisualizerComponents.TiersSetType.WoodPokeShells] = OreSizeVisualizerComponents.WoodPokeShellMassTiers;
		OreSizeVisualizerComponents.TierSets = dictionary;
	}

	// Token: 0x0400329F RID: 12959
	private static readonly OreSizeVisualizerComponents.MassTier[] MassTiers = new OreSizeVisualizerComponents.MassTier[]
	{
		new OreSizeVisualizerComponents.MassTier
		{
			animName = "idle1",
			massRequired = 50f,
			colliderRadius = 0.15f
		},
		new OreSizeVisualizerComponents.MassTier
		{
			animName = "idle2",
			massRequired = 600f,
			colliderRadius = 0.2f
		},
		new OreSizeVisualizerComponents.MassTier
		{
			animName = "idle3",
			massRequired = float.MaxValue,
			colliderRadius = 0.25f
		}
	};

	// Token: 0x040032A0 RID: 12960
	private static readonly OreSizeVisualizerComponents.MassTier[] PokeShellMassTiers = new OreSizeVisualizerComponents.MassTier[]
	{
		new OreSizeVisualizerComponents.MassTier
		{
			animName = "idle1",
			massRequired = 7.5f,
			colliderRadius = 0.15f
		},
		new OreSizeVisualizerComponents.MassTier
		{
			animName = "idle2",
			massRequired = 15f,
			colliderRadius = 0.2f
		},
		new OreSizeVisualizerComponents.MassTier
		{
			animName = "idle3",
			massRequired = float.MaxValue,
			colliderRadius = 0.25f
		}
	};

	// Token: 0x040032A1 RID: 12961
	private static readonly OreSizeVisualizerComponents.MassTier[] WoodPokeShellMassTiers = new OreSizeVisualizerComponents.MassTier[]
	{
		new OreSizeVisualizerComponents.MassTier
		{
			animName = "idle1",
			massRequired = 75f,
			colliderRadius = 0.15f
		},
		new OreSizeVisualizerComponents.MassTier
		{
			animName = "idle2",
			massRequired = 150f,
			colliderRadius = 0.2f
		},
		new OreSizeVisualizerComponents.MassTier
		{
			animName = "idle3",
			massRequired = float.MaxValue,
			colliderRadius = 0.25f
		}
	};

	// Token: 0x040032A2 RID: 12962
	private static readonly Dictionary<OreSizeVisualizerComponents.TiersSetType, OreSizeVisualizerComponents.MassTier[]> TierSets;

	// Token: 0x02001B02 RID: 6914
	private struct MassTier
	{
		// Token: 0x04008178 RID: 33144
		public HashedString animName;

		// Token: 0x04008179 RID: 33145
		public float massRequired;

		// Token: 0x0400817A RID: 33146
		public float colliderRadius;
	}

	// Token: 0x02001B03 RID: 6915
	public enum TiersSetType
	{
		// Token: 0x0400817C RID: 33148
		Ores,
		// Token: 0x0400817D RID: 33149
		PokeShells,
		// Token: 0x0400817E RID: 33150
		WoodPokeShells
	}
}
