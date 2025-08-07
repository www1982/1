using System;
using System.Linq;
using Database;
using UnityEngine;

// Token: 0x02000D18 RID: 3352
public class KleiPermitDioramaVis_JoyResponseBalloon : KMonoBehaviour, IKleiPermitDioramaVisTarget
{
	// Token: 0x0600673C RID: 26428 RVA: 0x0026F405 File Offset: 0x0026D605
	public GameObject GetGameObject()
	{
		return base.gameObject;
	}

	// Token: 0x0600673D RID: 26429 RVA: 0x0026F410 File Offset: 0x0026D610
	public void ConfigureSetup()
	{
		this.minionUI.transform.localScale = Vector3.one * 0.7f;
		this.minionUI.transform.localPosition = new Vector3(this.minionUI.transform.localPosition.x - 73f, this.minionUI.transform.localPosition.y - 152f + 8f, this.minionUI.transform.localPosition.z);
	}

	// Token: 0x0600673E RID: 26430 RVA: 0x0026F4A2 File Offset: 0x0026D6A2
	public void ConfigureWith(PermitResource permit)
	{
		this.ConfigureWith(Option.Some<BalloonArtistFacadeResource>((BalloonArtistFacadeResource)permit));
	}

	// Token: 0x0600673F RID: 26431 RVA: 0x0026F4B8 File Offset: 0x0026D6B8
	public void ConfigureWith(Option<BalloonArtistFacadeResource> permit)
	{
		KleiPermitDioramaVis_JoyResponseBalloon.<>c__DisplayClass10_0 CS$<>8__locals1 = new KleiPermitDioramaVis_JoyResponseBalloon.<>c__DisplayClass10_0();
		CS$<>8__locals1.permit = permit;
		KBatchedAnimController component = this.minionUI.SpawnedAvatar.GetComponent<KBatchedAnimController>();
		CS$<>8__locals1.minionSymbolOverrider = this.minionUI.SpawnedAvatar.GetComponent<SymbolOverrideController>();
		this.minionUI.SetMinion(this.specificPersonality.UnwrapOrElse(() => (from p in Db.Get().Personalities.GetAll(true, true)
			where p.joyTrait == "BalloonArtist"
			select p).GetRandom<Personality>(), null));
		if (!this.didAddAnims)
		{
			this.didAddAnims = true;
			component.AddAnimOverrides(Assets.GetAnim("anim_interacts_balloon_artist_kanim"), 0f);
		}
		component.Play("working_pre", KAnim.PlayMode.Once, 1f, 0f);
		component.Queue("working_loop", KAnim.PlayMode.Loop, 1f, 0f);
		CS$<>8__locals1.<ConfigureWith>g__DisplayNextBalloon|3();
		Updater[] array = new Updater[2];
		array[0] = Updater.WaitForSeconds(1.3f);
		int num = 1;
		Func<Updater>[] array2 = new Func<Updater>[2];
		array2[0] = () => Updater.WaitForSeconds(1.618f);
		array2[1] = () => Updater.Do(new global::System.Action(base.<ConfigureWith>g__DisplayNextBalloon|3));
		array[num] = Updater.Loop(array2);
		this.QueueUpdater(Updater.Series(array));
	}

	// Token: 0x06006740 RID: 26432 RVA: 0x0026F5F9 File Offset: 0x0026D7F9
	public void SetMinion(Personality personality)
	{
		this.specificPersonality = personality;
		if (base.gameObject.activeInHierarchy)
		{
			this.minionUI.SetMinion(personality);
		}
	}

	// Token: 0x06006741 RID: 26433 RVA: 0x0026F620 File Offset: 0x0026D820
	private void QueueUpdater(Updater updater)
	{
		if (base.gameObject.activeInHierarchy)
		{
			this.RunUpdater(updater);
			return;
		}
		this.updaterToRunOnStart = updater;
	}

	// Token: 0x06006742 RID: 26434 RVA: 0x0026F643 File Offset: 0x0026D843
	private void RunUpdater(Updater updater)
	{
		if (this.updaterRoutine != null)
		{
			base.StopCoroutine(this.updaterRoutine);
			this.updaterRoutine = null;
		}
		this.updaterRoutine = base.StartCoroutine(updater);
	}

	// Token: 0x06006743 RID: 26435 RVA: 0x0026F672 File Offset: 0x0026D872
	private void OnEnable()
	{
		if (this.updaterToRunOnStart.IsSome())
		{
			this.RunUpdater(this.updaterToRunOnStart.Unwrap());
			this.updaterToRunOnStart = Option.None;
		}
	}

	// Token: 0x040046C7 RID: 18119
	private const int FRAMES_TO_MAKE_BALLOON_IN_ANIM = 39;

	// Token: 0x040046C8 RID: 18120
	private const float SECONDS_TO_MAKE_BALLOON_IN_ANIM = 1.3f;

	// Token: 0x040046C9 RID: 18121
	private const float SECONDS_BETWEEN_BALLOONS = 1.618f;

	// Token: 0x040046CA RID: 18122
	[SerializeField]
	private UIMinion minionUI;

	// Token: 0x040046CB RID: 18123
	private bool didAddAnims;

	// Token: 0x040046CC RID: 18124
	private const string TARGET_SYMBOL_TO_OVERRIDE = "body";

	// Token: 0x040046CD RID: 18125
	private const int TARGET_OVERRIDE_PRIORITY = 0;

	// Token: 0x040046CE RID: 18126
	private Option<Personality> specificPersonality;

	// Token: 0x040046CF RID: 18127
	private Option<PermitResource> lastConfiguredPermit;

	// Token: 0x040046D0 RID: 18128
	private Option<Updater> updaterToRunOnStart;

	// Token: 0x040046D1 RID: 18129
	private Coroutine updaterRoutine;
}
