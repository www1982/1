using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000575 RID: 1397
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/Automatable")]
public class Automatable : KMonoBehaviour
{
	// Token: 0x06001F32 RID: 7986 RVA: 0x000B2B91 File Offset: 0x000B0D91
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<Automatable>(-905833192, Automatable.OnCopySettingsDelegate);
	}

	// Token: 0x06001F33 RID: 7987 RVA: 0x000B2BAC File Offset: 0x000B0DAC
	private void OnCopySettings(object data)
	{
		Automatable component = ((GameObject)data).GetComponent<Automatable>();
		if (component != null)
		{
			this.automationOnly = component.automationOnly;
		}
	}

	// Token: 0x06001F34 RID: 7988 RVA: 0x000B2BDA File Offset: 0x000B0DDA
	public bool GetAutomationOnly()
	{
		return this.automationOnly;
	}

	// Token: 0x06001F35 RID: 7989 RVA: 0x000B2BE2 File Offset: 0x000B0DE2
	public void SetAutomationOnly(bool only)
	{
		this.automationOnly = only;
	}

	// Token: 0x06001F36 RID: 7990 RVA: 0x000B2BEB File Offset: 0x000B0DEB
	public bool AllowedByAutomation(bool is_transfer_arm)
	{
		return !this.GetAutomationOnly() || is_transfer_arm;
	}

	// Token: 0x0400121E RID: 4638
	[Serialize]
	private bool automationOnly = true;

	// Token: 0x0400121F RID: 4639
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x04001220 RID: 4640
	private static readonly EventSystem.IntraObjectHandler<Automatable> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<Automatable>(delegate(Automatable component, object data)
	{
		component.OnCopySettings(data);
	});
}
