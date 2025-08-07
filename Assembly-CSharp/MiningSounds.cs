using System;
using FMODUnity;
using UnityEngine;

// Token: 0x020005E0 RID: 1504
[AddComponentMenu("KMonoBehaviour/scripts/MiningSounds")]
public class MiningSounds : KMonoBehaviour
{
	// Token: 0x060022D0 RID: 8912 RVA: 0x000C7E47 File Offset: 0x000C6047
	protected override void OnPrefabInit()
	{
		base.Subscribe<MiningSounds>(-1762453998, MiningSounds.OnStartMiningSoundDelegate);
		base.Subscribe<MiningSounds>(939543986, MiningSounds.OnStopMiningSoundDelegate);
	}

	// Token: 0x060022D1 RID: 8913 RVA: 0x000C7E6C File Offset: 0x000C606C
	private void OnStartMiningSound(object data)
	{
		if (this.miningSound == null)
		{
			Element element = data as Element;
			if (element != null)
			{
				string text = element.substance.GetMiningSound();
				if (text == null || text == "")
				{
					return;
				}
				text = "Mine_" + text;
				string sound = GlobalAssets.GetSound(text, false);
				this.miningSoundEvent = RuntimeManager.PathToEventReference(sound);
				if (!this.miningSoundEvent.IsNull)
				{
					this.loopingSounds.StartSound(this.miningSoundEvent);
				}
			}
		}
	}

	// Token: 0x060022D2 RID: 8914 RVA: 0x000C7EED File Offset: 0x000C60ED
	private void OnStopMiningSound(object data)
	{
		if (!this.miningSoundEvent.IsNull)
		{
			this.loopingSounds.StopSound(this.miningSoundEvent);
			this.miningSound = null;
		}
	}

	// Token: 0x060022D3 RID: 8915 RVA: 0x000C7F14 File Offset: 0x000C6114
	public void SetPercentComplete(float progress)
	{
		if (!this.miningSoundEvent.IsNull)
		{
			this.loopingSounds.SetParameter(this.miningSoundEvent, MiningSounds.HASH_PERCENTCOMPLETE, progress);
		}
	}

	// Token: 0x04001431 RID: 5169
	private static HashedString HASH_PERCENTCOMPLETE = "percentComplete";

	// Token: 0x04001432 RID: 5170
	[MyCmpGet]
	private LoopingSounds loopingSounds;

	// Token: 0x04001433 RID: 5171
	private FMODAsset miningSound;

	// Token: 0x04001434 RID: 5172
	private EventReference miningSoundEvent;

	// Token: 0x04001435 RID: 5173
	private static readonly EventSystem.IntraObjectHandler<MiningSounds> OnStartMiningSoundDelegate = new EventSystem.IntraObjectHandler<MiningSounds>(delegate(MiningSounds component, object data)
	{
		component.OnStartMiningSound(data);
	});

	// Token: 0x04001436 RID: 5174
	private static readonly EventSystem.IntraObjectHandler<MiningSounds> OnStopMiningSoundDelegate = new EventSystem.IntraObjectHandler<MiningSounds>(delegate(MiningSounds component, object data)
	{
		component.OnStopMiningSound(data);
	});
}
