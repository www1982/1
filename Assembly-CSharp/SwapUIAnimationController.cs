using System;
using UnityEngine;

// Token: 0x02000E5F RID: 3679
public class SwapUIAnimationController : MonoBehaviour
{
	// Token: 0x06007551 RID: 30033 RVA: 0x002CECFC File Offset: 0x002CCEFC
	public void SetState(bool Primary)
	{
		this.AnimationControllerObject_Primary.SetActive(Primary);
		if (!Primary)
		{
			this.AnimationControllerObject_Alternate.GetComponent<KAnimControllerBase>().TintColour = new Color(1f, 1f, 1f, 0.5f);
			this.AnimationControllerObject_Primary.GetComponent<KAnimControllerBase>().TintColour = Color.clear;
		}
		this.AnimationControllerObject_Alternate.SetActive(!Primary);
		if (Primary)
		{
			this.AnimationControllerObject_Primary.GetComponent<KAnimControllerBase>().TintColour = Color.white;
			this.AnimationControllerObject_Alternate.GetComponent<KAnimControllerBase>().TintColour = Color.clear;
		}
	}

	// Token: 0x0400515D RID: 20829
	public GameObject AnimationControllerObject_Primary;

	// Token: 0x0400515E RID: 20830
	public GameObject AnimationControllerObject_Alternate;
}
