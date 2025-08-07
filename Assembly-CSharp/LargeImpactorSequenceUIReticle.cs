using System;
using System.Collections;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000B4B RID: 2891
public class LargeImpactorSequenceUIReticle : KMonoBehaviour
{
	// Token: 0x0600560C RID: 22028 RVA: 0x001F385E File Offset: 0x001F1A5E
	protected override void OnPrefabInit()
	{
		this.transform = base.transform as RectTransform;
		this.bgOriginalColor = this.bg.color;
		this.calculatingImpactLabelOriginalColor = this.calculateImpactLabel.color;
		base.OnPrefabInit();
	}

	// Token: 0x0600560D RID: 22029 RVA: 0x001F3899 File Offset: 0x001F1A99
	protected override void OnSpawn()
	{
		this.ResetGraphics();
	}

	// Token: 0x0600560E RID: 22030 RVA: 0x001F38A1 File Offset: 0x001F1AA1
	public void Run(global::System.Action onPhase1Completed = null, global::System.Action onComplete = null)
	{
		this.SetVisibility(true);
		this.AbortCoroutine();
		this.ResetGraphics();
		this.onPhase1Completed = onPhase1Completed;
		this.onComplete = onComplete;
		this.InitializeAndRunCoroutine();
	}

	// Token: 0x0600560F RID: 22031 RVA: 0x001F38CA File Offset: 0x001F1ACA
	public void Hide()
	{
		this.AbortCoroutine();
		this.ResetGraphics();
		this.SetVisibility(false);
	}

	// Token: 0x06005610 RID: 22032 RVA: 0x001F38DF File Offset: 0x001F1ADF
	private void SetVisibility(bool visible)
	{
		this.isVisible = visible;
		this.label.enabled = visible;
		this.bg.enabled = visible;
		this.border.enabled = visible;
	}

	// Token: 0x06005611 RID: 22033 RVA: 0x001F390C File Offset: 0x001F1B0C
	private void InitializeAndRunCoroutine()
	{
		this.coroutine = base.StartCoroutine(this.EnterSequence());
	}

	// Token: 0x06005612 RID: 22034 RVA: 0x001F3920 File Offset: 0x001F1B20
	private void AbortCoroutine()
	{
		base.StopAllCoroutines();
		this.coroutine = null;
	}

	// Token: 0x06005613 RID: 22035 RVA: 0x001F392F File Offset: 0x001F1B2F
	public void SetTarget(LargeImpactorStatus.Instance largeImpactorStatus)
	{
		this.largeImpactorStatus = largeImpactorStatus;
		this.loopingSounds = largeImpactorStatus.GetComponent<LoopingSounds>();
	}

	// Token: 0x06005614 RID: 22036 RVA: 0x001F3944 File Offset: 0x001F1B44
	private void ResetGraphics()
	{
		this.label.SetText("");
		this.border.Opacity(0f);
		this.bg.color = this.bgOriginalColor;
		this.bg.Opacity(0f);
		this.sidePanelIcon.Opacity(0f);
		this.sidePanelTitleLabel.SetText("");
		this.calculateImpactLabel.SetText("");
		this.sidePanelDescriptionLabel.SetText("");
		this.calculateImpactLabel.color = this.calculatingImpactLabelOriginalColor;
	}

	// Token: 0x06005615 RID: 22037 RVA: 0x001F39E4 File Offset: 0x001F1BE4
	private void PlayLoopingSound(string soundName)
	{
		string sound = GlobalAssets.GetSound(soundName, false);
		this.loopingSounds.StartSound(sound, false, false, false);
	}

	// Token: 0x06005616 RID: 22038 RVA: 0x001F3A09 File Offset: 0x001F1C09
	private IEnumerator EnterSequence()
	{
		KFMOD.PlayUISound(GlobalAssets.GetSound("HUD_Imperative_analysis_start", false));
		yield return this.Interpolate(delegate(float n)
		{
			this.transform.sizeDelta = Vector2.Lerp(this.initialSize * 2f, this.initialSize, n);
			this.border.Opacity(n);
		}, 0.4f, null);
		if (this.bg.color != this.border.color)
		{
			this.bg.color = this.border.color;
		}
		yield return this.Interpolate(delegate(float n)
		{
			this.bg.Opacity(Mathf.Abs(Mathf.Sin(n * 3.1415927f * 3f)));
		}, 0.4f, delegate
		{
			this.bg.color = this.bgOriginalColor;
		});
		KFMOD.PlayUISound(GlobalAssets.GetSound("HUD_Imperative_bracket_open_first", false));
		yield return this.Interpolate(delegate(float n)
		{
			this.bg.Opacity(n * this.bgOriginalColor.a);
			this.transform.sizeDelta = Vector2.Lerp(this.initialSize, this.zoomedOutSize, n);
		}, 0.8f, null);
		this.PlayLoopingSound("HUD_Imperative_Text_typing_header");
		string titleText = MISC.NOTIFICATIONS.LARGEIMPACTORREVEALSEQUENCE.RETICLE.LARGE_IMPACTOR_NAME;
		yield return this.Interpolate(delegate(float n)
		{
			SequenceTools.TextWriter(this.label, titleText, n);
		}, 1f, null);
		this.loopingSounds.StopSound(GlobalAssets.GetSound("HUD_Imperative_Text_typing_header", false));
		yield return null;
		this.PlayLoopingSound("HUD_Imperative_Text_typing_header");
		this.sidePanelIcon.color = Color.white;
		string sidePanelTitleText = MISC.NOTIFICATIONS.LARGEIMPACTORREVEALSEQUENCE.RETICLE.SIDE_PANEL_TITLE;
		yield return this.Interpolate(delegate(float n)
		{
			this.sidePanelIcon.Opacity(n);
			this.sidePanelIcon.transform.localRotation = Quaternion.Euler(0f, Mathf.Lerp(90f, 0f, n), 0f);
			SequenceTools.TextWriter(this.sidePanelTitleLabel, sidePanelTitleText, n);
		}, 0.5f, null);
		this.loopingSounds.StopSound(GlobalAssets.GetSound("HUD_Imperative_Text_typing_header", false));
		this.PlayLoopingSound("HUD_Imperative_Text_typing_body");
		string sidePanelDescriptionText = GameUtil.SafeStringFormat(MISC.NOTIFICATIONS.LARGEIMPACTORREVEALSEQUENCE.RETICLE.SIDE_PANEL_DESCRIPTION, new object[] { GameUtil.GetFormattedCycles(this.largeImpactorStatus.TimeRemainingBeforeCollision, "F1", false).Split(' ', StringSplitOptions.None)[0] });
		yield return this.Interpolate(delegate(float n)
		{
			SequenceTools.TextWriter(this.sidePanelDescriptionLabel, sidePanelDescriptionText, n);
		}, 1.5f, null);
		this.loopingSounds.StopSound(GlobalAssets.GetSound("HUD_Imperative_Text_typing_body", false));
		yield return new WaitForSecondsRealtime(2f);
		if (this.onPhase1Completed != null)
		{
			this.onPhase1Completed();
			this.onPhase1Completed = null;
		}
		yield return this.Interpolate(delegate(float n)
		{
			SequenceTools.TextEraser(this.label, titleText, n);
			SequenceTools.TextEraser(this.sidePanelTitleLabel, sidePanelTitleText, n);
			SequenceTools.TextEraser(this.sidePanelDescriptionLabel, sidePanelDescriptionText, n);
			this.sidePanelIcon.color = Color.Lerp(Color.white, Color.red, n);
			this.sidePanelIcon.Opacity(1f - n);
		}, 0.5f, null);
		Color bgColor = this.bg.color;
		Color targetBgColor = Color.Lerp(this.bgOriginalColor, Color.black, 0.5f);
		targetBgColor.a = this.bgOriginalColor.a * 0.8f;
		KFMOD.PlayUISound(GlobalAssets.GetSound("HUD_Imperative_bracket_open_second", false));
		yield return this.Interpolate(delegate(float n)
		{
			this.bg.color = Color.Lerp(bgColor, targetBgColor, n);
			this.transform.sizeDelta = Vector2.Lerp(this.zoomedOutSize, this.calculatingImpactSize, n);
		}, 0.8f, null);
		KFMOD.PlayUISound(GlobalAssets.GetSound("HUD_Imperative_calculating_beep", false));
		Coroutine flashLabelCoroutine = null;
		this.Interpolate(delegate(float n)
		{
			this.calculateImpactLabel.color = Color.Lerp(Color.white, Color.red, Mathf.Abs(Mathf.Sin(n * 3.1415927f * 999f)));
		}, 999f, out flashLabelCoroutine, null);
		yield return this.Interpolate(delegate(float n)
		{
			SequenceTools.TextWriter(this.calculateImpactLabel, MISC.NOTIFICATIONS.LARGEIMPACTORREVEALSEQUENCE.RETICLE.CALCULATING_IMPACT_ZONE_TEXT, n);
		}, 0.5f, null);
		yield return new WaitForSecondsRealtime(3.5f);
		base.StopCoroutine(flashLabelCoroutine);
		flashLabelCoroutine = null;
		Color impactLabelColor = this.calculateImpactLabel.color;
		yield return this.Interpolate(delegate(float n)
		{
			float num = Mathf.Sqrt(1f - n);
			float num2 = Mathf.Sqrt(n);
			this.bg.Opacity(this.bgOriginalColor.a * num);
			this.border.Opacity(num);
			this.calculateImpactLabel.Opacity(impactLabelColor.a * num);
			this.transform.sizeDelta = Vector2.Lerp(this.calculatingImpactSize, this.calculatingImpactSize * 1.3f, num2);
		}, 0.8f, null);
		if (this.onComplete != null)
		{
			this.onComplete();
			this.onComplete = null;
		}
		yield break;
	}

	// Token: 0x06005617 RID: 22039 RVA: 0x001F3A18 File Offset: 0x001F1C18
	protected override void OnCmpDisable()
	{
		this.AbortCoroutine();
	}

	// Token: 0x06005618 RID: 22040 RVA: 0x001F3A20 File Offset: 0x001F1C20
	protected override void OnCmpEnable()
	{
		if (this.isVisible)
		{
			this.AbortCoroutine();
			this.ResetGraphics();
			this.InitializeAndRunCoroutine();
		}
	}

	// Token: 0x04003977 RID: 14711
	private const float reticleEnterDuration = 0.4f;

	// Token: 0x04003978 RID: 14712
	private const float flashDuration = 0.4f;

	// Token: 0x04003979 RID: 14713
	private const int flashTimes = 3;

	// Token: 0x0400397A RID: 14714
	private const float reticleZoomOutDuration = 0.8f;

	// Token: 0x0400397B RID: 14715
	private const float labelRevealDuration = 1f;

	// Token: 0x0400397C RID: 14716
	private const float sidePanel_TitleRevealDuration = 0.5f;

	// Token: 0x0400397D RID: 14717
	private const float sidePanel_DescriptionRevealDuration = 1.5f;

	// Token: 0x0400397E RID: 14718
	private const float exitToCalculationDuration = 0.5f;

	// Token: 0x0400397F RID: 14719
	private const float expandReticleHorizontallyDuration = 0.8f;

	// Token: 0x04003980 RID: 14720
	private const float calculateImpactZoneTextRevealDuration = 0.5f;

	// Token: 0x04003981 RID: 14721
	private const float exitDuration = 0.8f;

	// Token: 0x04003982 RID: 14722
	public const float RevealPOI_LandingZone_Duration = 3.5f;

	// Token: 0x04003983 RID: 14723
	private const string Sound_LockTarget = "HUD_Imperative_analysis_start";

	// Token: 0x04003984 RID: 14724
	private const string Sound_BracketSquareExpand = "HUD_Imperative_bracket_open_first";

	// Token: 0x04003985 RID: 14725
	private const string Sound_BracketExpandsForCalculatingLandingZone = "HUD_Imperative_bracket_open_second";

	// Token: 0x04003986 RID: 14726
	private const string Sound_CalculatingLandingZoneTextAppears = "HUD_Imperative_calculating_beep";

	// Token: 0x04003987 RID: 14727
	private const string Sound_TypeHeader = "HUD_Imperative_Text_typing_header";

	// Token: 0x04003988 RID: 14728
	private const string Sound_TypeBody = "HUD_Imperative_Text_typing_body";

	// Token: 0x04003989 RID: 14729
	public Vector2 initialSize = new Vector2(100f, 100f);

	// Token: 0x0400398A RID: 14730
	public Vector2 zoomedOutSize = new Vector2(180f, 180f);

	// Token: 0x0400398B RID: 14731
	public Vector2 calculatingImpactSize = new Vector2(500f, 120f);

	// Token: 0x0400398C RID: 14732
	[Space]
	public LocText label;

	// Token: 0x0400398D RID: 14733
	public LocText sidePanelTitleLabel;

	// Token: 0x0400398E RID: 14734
	public LocText sidePanelDescriptionLabel;

	// Token: 0x0400398F RID: 14735
	public LocText calculateImpactLabel;

	// Token: 0x04003990 RID: 14736
	public Image bg;

	// Token: 0x04003991 RID: 14737
	public Image border;

	// Token: 0x04003992 RID: 14738
	public Image sidePanelIcon;

	// Token: 0x04003993 RID: 14739
	private new RectTransform transform;

	// Token: 0x04003994 RID: 14740
	private LargeImpactorStatus.Instance largeImpactorStatus;

	// Token: 0x04003995 RID: 14741
	private LoopingSounds loopingSounds;

	// Token: 0x04003996 RID: 14742
	private bool isVisible;

	// Token: 0x04003997 RID: 14743
	private Color bgOriginalColor;

	// Token: 0x04003998 RID: 14744
	private Color calculatingImpactLabelOriginalColor;

	// Token: 0x04003999 RID: 14745
	private global::System.Action onPhase1Completed;

	// Token: 0x0400399A RID: 14746
	private global::System.Action onComplete;

	// Token: 0x0400399B RID: 14747
	private Coroutine coroutine;
}
