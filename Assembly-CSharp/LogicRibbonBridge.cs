using System;

// Token: 0x02000771 RID: 1905
public class LogicRibbonBridge : KMonoBehaviour
{
	// Token: 0x060031B8 RID: 12728 RVA: 0x0011A370 File Offset: 0x00118570
	protected override void OnSpawn()
	{
		base.OnSpawn();
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		switch (base.GetComponent<Rotatable>().GetOrientation())
		{
		case Orientation.Neutral:
			component.Play("0", KAnim.PlayMode.Once, 1f, 0f);
			return;
		case Orientation.R90:
			component.Play("90", KAnim.PlayMode.Once, 1f, 0f);
			return;
		case Orientation.R180:
			component.Play("180", KAnim.PlayMode.Once, 1f, 0f);
			return;
		case Orientation.R270:
			component.Play("270", KAnim.PlayMode.Once, 1f, 0f);
			return;
		default:
			return;
		}
	}
}
