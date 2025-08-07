using System;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000FEC RID: 4076
	public class CommonSickEffectSickness : Sickness.SicknessComponent
	{
		// Token: 0x06007DD8 RID: 32216 RVA: 0x003264A8 File Offset: 0x003246A8
		public override object OnInfect(GameObject go, SicknessInstance diseaseInstance)
		{
			KBatchedAnimController kbatchedAnimController = FXHelpers.CreateEffect("contaminated_crew_fx_kanim", go.transform.GetPosition() + new Vector3(0f, 0f, -0.1f), go.transform, true, Grid.SceneLayer.Front, false);
			kbatchedAnimController.Play("fx_loop", KAnim.PlayMode.Loop, 1f, 0f);
			return kbatchedAnimController;
		}

		// Token: 0x06007DD9 RID: 32217 RVA: 0x00326508 File Offset: 0x00324708
		public override void OnCure(GameObject go, object instance_data)
		{
			((KAnimControllerBase)instance_data).gameObject.DeleteObject();
		}
	}
}
