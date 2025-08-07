using System;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02001003 RID: 4099
	public class ModifierInstance<ModifierType> : IStateMachineTarget
	{
		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x06007E61 RID: 32353 RVA: 0x00328E04 File Offset: 0x00327004
		// (set) Token: 0x06007E62 RID: 32354 RVA: 0x00328E0C File Offset: 0x0032700C
		public GameObject gameObject { get; private set; }

		// Token: 0x06007E63 RID: 32355 RVA: 0x00328E15 File Offset: 0x00327015
		public ModifierInstance(GameObject game_object, ModifierType modifier)
		{
			this.gameObject = game_object;
			this.modifier = modifier;
		}

		// Token: 0x06007E64 RID: 32356 RVA: 0x00328E2B File Offset: 0x0032702B
		public ComponentType GetComponent<ComponentType>()
		{
			return this.gameObject.GetComponent<ComponentType>();
		}

		// Token: 0x06007E65 RID: 32357 RVA: 0x00328E38 File Offset: 0x00327038
		public int Subscribe(int hash, Action<object> handler)
		{
			return this.gameObject.GetComponent<KMonoBehaviour>().Subscribe(hash, handler);
		}

		// Token: 0x06007E66 RID: 32358 RVA: 0x00328E4C File Offset: 0x0032704C
		public void Unsubscribe(int hash, Action<object> handler)
		{
			this.gameObject.GetComponent<KMonoBehaviour>().Unsubscribe(hash, handler);
		}

		// Token: 0x06007E67 RID: 32359 RVA: 0x00328E60 File Offset: 0x00327060
		public void Unsubscribe(int id)
		{
			this.gameObject.GetComponent<KMonoBehaviour>().Unsubscribe(id);
		}

		// Token: 0x06007E68 RID: 32360 RVA: 0x00328E73 File Offset: 0x00327073
		public void Trigger(int hash, object data = null)
		{
			this.gameObject.GetComponent<KPrefabID>().Trigger(hash, data);
		}

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x06007E69 RID: 32361 RVA: 0x00328E87 File Offset: 0x00327087
		public Transform transform
		{
			get
			{
				return this.gameObject.transform;
			}
		}

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x06007E6A RID: 32362 RVA: 0x00328E94 File Offset: 0x00327094
		public bool isNull
		{
			get
			{
				return this.gameObject == null;
			}
		}

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x06007E6B RID: 32363 RVA: 0x00328EA2 File Offset: 0x003270A2
		public string name
		{
			get
			{
				return this.gameObject.name;
			}
		}

		// Token: 0x06007E6C RID: 32364 RVA: 0x00328EAF File Offset: 0x003270AF
		public virtual void OnCleanUp()
		{
		}

		// Token: 0x04005F5A RID: 24410
		public ModifierType modifier;
	}
}
