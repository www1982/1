using System;
using UnityEngine;

namespace rendering
{
	// Token: 0x02000EAD RID: 3757
	public class BackWall : MonoBehaviour
	{
		// Token: 0x06007835 RID: 30773 RVA: 0x002E98EB File Offset: 0x002E7AEB
		private void Awake()
		{
			this.backwallMaterial.SetTexture("images", this.array);
		}

		// Token: 0x0400538C RID: 21388
		[SerializeField]
		public Material backwallMaterial;

		// Token: 0x0400538D RID: 21389
		[SerializeField]
		public Texture2DArray array;
	}
}
