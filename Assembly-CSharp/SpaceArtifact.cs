using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using KSerialization;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000B96 RID: 2966
[AddComponentMenu("KMonoBehaviour/scripts/SpaceArtifact")]
public class SpaceArtifact : KMonoBehaviour, IGameObjectEffectDescriptor
{
	// Token: 0x0600588D RID: 22669 RVA: 0x00200428 File Offset: 0x001FE628
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.loadCharmed && DlcManager.IsExpansion1Active())
		{
			base.gameObject.AddTag(GameTags.CharmedArtifact);
			this.SetEntombedDecor();
		}
		else
		{
			this.loadCharmed = false;
			this.SetAnalyzedDecor();
		}
		this.UpdateStatusItem();
		Components.SpaceArtifacts.Add(this);
		this.UpdateAnim();
	}

	// Token: 0x0600588E RID: 22670 RVA: 0x00200486 File Offset: 0x001FE686
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Components.SpaceArtifacts.Remove(this);
	}

	// Token: 0x0600588F RID: 22671 RVA: 0x00200499 File Offset: 0x001FE699
	public void RemoveCharm()
	{
		base.gameObject.RemoveTag(GameTags.CharmedArtifact);
		this.UpdateStatusItem();
		this.loadCharmed = false;
		this.UpdateAnim();
		this.SetAnalyzedDecor();
	}

	// Token: 0x06005890 RID: 22672 RVA: 0x002004C4 File Offset: 0x001FE6C4
	private void SetEntombedDecor()
	{
		base.GetComponent<DecorProvider>().SetValues(DECOR.BONUS.TIER0);
	}

	// Token: 0x06005891 RID: 22673 RVA: 0x002004D6 File Offset: 0x001FE6D6
	private void SetAnalyzedDecor()
	{
		base.GetComponent<DecorProvider>().SetValues(this.artifactTier.decorValues);
	}

	// Token: 0x06005892 RID: 22674 RVA: 0x002004F0 File Offset: 0x001FE6F0
	public void UpdateStatusItem()
	{
		if (base.gameObject.HasTag(GameTags.CharmedArtifact))
		{
			base.gameObject.GetComponent<KSelectable>().AddStatusItem(Db.Get().MiscStatusItems.ArtifactEntombed, null);
			return;
		}
		base.gameObject.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().MiscStatusItems.ArtifactEntombed, false);
	}

	// Token: 0x06005893 RID: 22675 RVA: 0x00200552 File Offset: 0x001FE752
	public void SetArtifactTier(ArtifactTier tier)
	{
		this.artifactTier = tier;
	}

	// Token: 0x06005894 RID: 22676 RVA: 0x0020055B File Offset: 0x001FE75B
	public ArtifactTier GetArtifactTier()
	{
		return this.artifactTier;
	}

	// Token: 0x06005895 RID: 22677 RVA: 0x00200563 File Offset: 0x001FE763
	public void SetUIAnim(string anim)
	{
		this.ui_anim = anim;
	}

	// Token: 0x06005896 RID: 22678 RVA: 0x0020056C File Offset: 0x001FE76C
	public string GetUIAnim()
	{
		return this.ui_anim;
	}

	// Token: 0x06005897 RID: 22679 RVA: 0x00200574 File Offset: 0x001FE774
	public List<Descriptor> GetEffectDescriptions()
	{
		List<Descriptor> list = new List<Descriptor>();
		if (base.gameObject.HasTag(GameTags.CharmedArtifact))
		{
			Descriptor descriptor = new Descriptor(global::STRINGS.BUILDINGS.PREFABS.ARTIFACTANALYSISSTATION.PAYLOAD_DROP_RATE.Replace("{chance}", GameUtil.GetFormattedPercent(this.artifactTier.payloadDropChance * 100f, GameUtil.TimeSlice.None)), global::STRINGS.BUILDINGS.PREFABS.ARTIFACTANALYSISSTATION.PAYLOAD_DROP_RATE_TOOLTIP.Replace("{chance}", GameUtil.GetFormattedPercent(this.artifactTier.payloadDropChance * 100f, GameUtil.TimeSlice.None)), Descriptor.DescriptorType.Effect, false);
			list.Add(descriptor);
		}
		Descriptor descriptor2 = new Descriptor(string.Format("This is an artifact from space", Array.Empty<object>()), string.Format("This is the tooltip string", Array.Empty<object>()), Descriptor.DescriptorType.Information, false);
		list.Add(descriptor2);
		return list;
	}

	// Token: 0x06005898 RID: 22680 RVA: 0x00200624 File Offset: 0x001FE824
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		return this.GetEffectDescriptions();
	}

	// Token: 0x06005899 RID: 22681 RVA: 0x0020062C File Offset: 0x001FE82C
	private void UpdateAnim()
	{
		string text;
		if (base.gameObject.HasTag(GameTags.CharmedArtifact))
		{
			text = "entombed_" + this.uniqueAnimNameFragment.Replace("idle_", "");
		}
		else
		{
			text = this.uniqueAnimNameFragment;
		}
		base.GetComponent<KBatchedAnimController>().Play(text, KAnim.PlayMode.Loop, 1f, 0f);
	}

	// Token: 0x0600589A RID: 22682 RVA: 0x00200690 File Offset: 0x001FE890
	[OnDeserialized]
	public void OnDeserialize()
	{
		Pickupable component = base.GetComponent<Pickupable>();
		if (component != null)
		{
			component.deleteOffGrid = false;
		}
	}

	// Token: 0x04003AD8 RID: 15064
	public const string ID = "SpaceArtifact";

	// Token: 0x04003AD9 RID: 15065
	private const string charmedPrefix = "entombed_";

	// Token: 0x04003ADA RID: 15066
	private const string idlePrefix = "idle_";

	// Token: 0x04003ADB RID: 15067
	[SerializeField]
	private string ui_anim;

	// Token: 0x04003ADC RID: 15068
	[Serialize]
	private bool loadCharmed = true;

	// Token: 0x04003ADD RID: 15069
	public ArtifactTier artifactTier;

	// Token: 0x04003ADE RID: 15070
	public ArtifactType artifactType;

	// Token: 0x04003ADF RID: 15071
	public string uniqueAnimNameFragment;
}
