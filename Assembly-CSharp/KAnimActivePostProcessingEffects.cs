using System;
using UnityEngine;

// Token: 0x02000AA7 RID: 2727
public class KAnimActivePostProcessingEffects : KMonoBehaviour
{
	// Token: 0x06004F13 RID: 20243 RVA: 0x001C9378 File Offset: 0x001C7578
	public void EnableEffect(KAnimConverter.PostProcessingEffects effect_flag)
	{
		this.currentActiveEffects |= effect_flag;
	}

	// Token: 0x06004F14 RID: 20244 RVA: 0x001C9388 File Offset: 0x001C7588
	public void DisableEffect(KAnimConverter.PostProcessingEffects effect_flag)
	{
		if (this.IsEffectActive(effect_flag))
		{
			this.currentActiveEffects ^= effect_flag;
		}
	}

	// Token: 0x06004F15 RID: 20245 RVA: 0x001C93A1 File Offset: 0x001C75A1
	public bool IsEffectActive(KAnimConverter.PostProcessingEffects effect_flag)
	{
		return (this.currentActiveEffects & effect_flag) > (KAnimConverter.PostProcessingEffects)0;
	}

	// Token: 0x06004F16 RID: 20246 RVA: 0x001C93AE File Offset: 0x001C75AE
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		Graphics.Blit(source, destination);
		if (this.currentActiveEffects != (KAnimConverter.PostProcessingEffects)0)
		{
			KAnimBatchManager.Instance().RenderKAnimPostProcessingEffects(this.currentActiveEffects);
		}
	}

	// Token: 0x0400347B RID: 13435
	private KAnimConverter.PostProcessingEffects currentActiveEffects;
}
