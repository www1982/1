using System;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000FED RID: 4077
	public class CustomSickEffectSickness : Sickness.SicknessComponent
	{
		// Token: 0x06007DDB RID: 32219 RVA: 0x00326522 File Offset: 0x00324722
		public CustomSickEffectSickness(string effect_kanim, string effect_anim_name)
		{
			this.kanim = effect_kanim;
			this.animName = effect_anim_name;
		}

		// Token: 0x06007DDC RID: 32220 RVA: 0x00326538 File Offset: 0x00324738
		public override object OnInfect(GameObject go, SicknessInstance diseaseInstance)
		{
			KBatchedAnimController kbatchedAnimController = FXHelpers.CreateEffect(this.kanim, go.transform.GetPosition() + new Vector3(0f, 0f, -0.1f), go.transform, true, Grid.SceneLayer.Front, false);
			kbatchedAnimController.Play(this.animName, KAnim.PlayMode.Loop, 1f, 0f);
			return kbatchedAnimController;
		}

		// Token: 0x06007DDD RID: 32221 RVA: 0x0032659A File Offset: 0x0032479A
		public override void OnCure(GameObject go, object instance_data)
		{
			((KAnimControllerBase)instance_data).gameObject.DeleteObject();
		}

		// Token: 0x04005EF1 RID: 24305
		private string kanim;

		// Token: 0x04005EF2 RID: 24306
		private string animName;
	}
}
