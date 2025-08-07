using System;
using Klei.AI;
using UnityEngine;

// Token: 0x020000F8 RID: 248
public class HugMinionReactable : Reactable
{
	// Token: 0x06000477 RID: 1143 RVA: 0x00024C6C File Offset: 0x00022E6C
	public HugMinionReactable(GameObject gameObject)
		: base(gameObject, "HugMinionReactable", Db.Get().ChoreTypes.Hug, 1, 1, true, 1f, 0f, float.PositiveInfinity, 0f, ObjectLayer.Minion)
	{
	}

	// Token: 0x06000478 RID: 1144 RVA: 0x00024CB4 File Offset: 0x00022EB4
	public override bool InternalCanBegin(GameObject newReactor, Navigator.ActiveTransition transition)
	{
		if (this.reactor != null)
		{
			return false;
		}
		Navigator component = newReactor.GetComponent<Navigator>();
		return !(component == null) && component.IsMoving();
	}

	// Token: 0x06000479 RID: 1145 RVA: 0x00024CEE File Offset: 0x00022EEE
	public override void Update(float dt)
	{
		this.gameObject.GetComponent<Facing>().SetFacing(this.reactor.GetComponent<Facing>().GetFacing());
	}

	// Token: 0x0600047A RID: 1146 RVA: 0x00024D10 File Offset: 0x00022F10
	protected override void InternalBegin()
	{
		KAnimControllerBase component = this.reactor.GetComponent<KAnimControllerBase>();
		component.AddAnimOverrides(Assets.GetAnim("anim_react_pip_kanim"), 0f);
		component.Play("hug_dupe_pre", KAnim.PlayMode.Once, 1f, 0f);
		component.Queue("hug_dupe_loop", KAnim.PlayMode.Once, 1f, 0f);
		component.Queue("hug_dupe_pst", KAnim.PlayMode.Once, 1f, 0f);
		component.onAnimComplete += this.Finish;
		this.gameObject.GetSMI<AnimInterruptMonitor.Instance>().PlayAnimSequence(new HashedString[] { "hug_dupe_pre", "hug_dupe_loop", "hug_dupe_pst" });
	}

	// Token: 0x0600047B RID: 1147 RVA: 0x00024DF0 File Offset: 0x00022FF0
	private void Finish(HashedString anim)
	{
		if (anim == "hug_dupe_pst")
		{
			if (this.reactor != null)
			{
				this.reactor.GetComponent<KAnimControllerBase>().onAnimComplete -= this.Finish;
				this.ApplyEffects();
			}
			else
			{
				DebugUtil.LogWarningArgs(new object[] { "HugMinionReactable finishing without adding a Hugged effect." });
			}
			base.End();
		}
	}

	// Token: 0x0600047C RID: 1148 RVA: 0x00024E5C File Offset: 0x0002305C
	private void ApplyEffects()
	{
		this.reactor.GetComponent<Effects>().Add("Hugged", true);
		HugMonitor.Instance smi = this.gameObject.GetSMI<HugMonitor.Instance>();
		if (smi != null)
		{
			smi.EnterHuggingFrenzy();
		}
	}

	// Token: 0x0600047D RID: 1149 RVA: 0x00024E95 File Offset: 0x00023095
	protected override void InternalEnd()
	{
	}

	// Token: 0x0600047E RID: 1150 RVA: 0x00024E97 File Offset: 0x00023097
	protected override void InternalCleanup()
	{
	}
}
