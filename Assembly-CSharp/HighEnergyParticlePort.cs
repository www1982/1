using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000954 RID: 2388
public class HighEnergyParticlePort : KMonoBehaviour, IGameObjectEffectDescriptor
{
	// Token: 0x06004488 RID: 17544 RVA: 0x0018A0AF File Offset: 0x001882AF
	public int GetHighEnergyParticleInputPortPosition()
	{
		return this.m_building.GetHighEnergyParticleInputCell();
	}

	// Token: 0x06004489 RID: 17545 RVA: 0x0018A0BC File Offset: 0x001882BC
	public int GetHighEnergyParticleOutputPortPosition()
	{
		return this.m_building.GetHighEnergyParticleOutputCell();
	}

	// Token: 0x0600448A RID: 17546 RVA: 0x0018A0C9 File Offset: 0x001882C9
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x0600448B RID: 17547 RVA: 0x0018A0D1 File Offset: 0x001882D1
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Components.HighEnergyParticlePorts.Add(this);
	}

	// Token: 0x0600448C RID: 17548 RVA: 0x0018A0E4 File Offset: 0x001882E4
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Components.HighEnergyParticlePorts.Remove(this);
	}

	// Token: 0x0600448D RID: 17549 RVA: 0x0018A0F8 File Offset: 0x001882F8
	public bool InputActive()
	{
		Operational component = base.GetComponent<Operational>();
		return this.particleInputEnabled && component != null && component.IsFunctional && (!this.requireOperational || component.IsOperational);
	}

	// Token: 0x0600448E RID: 17550 RVA: 0x0018A137 File Offset: 0x00188337
	public bool AllowCapture(HighEnergyParticle particle)
	{
		return this.onParticleCaptureAllowed == null || this.onParticleCaptureAllowed(particle);
	}

	// Token: 0x0600448F RID: 17551 RVA: 0x0018A14F File Offset: 0x0018834F
	public void Capture(HighEnergyParticle particle)
	{
		this.currentParticle = particle;
		if (this.onParticleCapture != null)
		{
			this.onParticleCapture(particle);
		}
	}

	// Token: 0x06004490 RID: 17552 RVA: 0x0018A16C File Offset: 0x0018836C
	public void Uncapture(HighEnergyParticle particle)
	{
		if (this.onParticleUncapture != null)
		{
			this.onParticleUncapture(particle);
		}
		this.currentParticle = null;
	}

	// Token: 0x06004491 RID: 17553 RVA: 0x0018A18C File Offset: 0x0018838C
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		if (this.particleInputEnabled)
		{
			list.Add(new Descriptor(UI.BUILDINGEFFECTS.PARTICLE_PORT_INPUT, UI.BUILDINGEFFECTS.TOOLTIPS.PARTICLE_PORT_INPUT, Descriptor.DescriptorType.Requirement, false));
		}
		if (this.particleOutputEnabled)
		{
			list.Add(new Descriptor(UI.BUILDINGEFFECTS.PARTICLE_PORT_OUTPUT, UI.BUILDINGEFFECTS.TOOLTIPS.PARTICLE_PORT_OUTPUT, Descriptor.DescriptorType.Effect, false));
		}
		return list;
	}

	// Token: 0x04002DDE RID: 11742
	[MyCmpGet]
	private Building m_building;

	// Token: 0x04002DDF RID: 11743
	public HighEnergyParticlePort.OnParticleCapture onParticleCapture;

	// Token: 0x04002DE0 RID: 11744
	public HighEnergyParticlePort.OnParticleCaptureAllowed onParticleCaptureAllowed;

	// Token: 0x04002DE1 RID: 11745
	public HighEnergyParticlePort.OnParticleCapture onParticleUncapture;

	// Token: 0x04002DE2 RID: 11746
	public HighEnergyParticle currentParticle;

	// Token: 0x04002DE3 RID: 11747
	public bool requireOperational = true;

	// Token: 0x04002DE4 RID: 11748
	public bool particleInputEnabled;

	// Token: 0x04002DE5 RID: 11749
	public bool particleOutputEnabled;

	// Token: 0x04002DE6 RID: 11750
	public CellOffset particleInputOffset;

	// Token: 0x04002DE7 RID: 11751
	public CellOffset particleOutputOffset;

	// Token: 0x02001968 RID: 6504
	// (Invoke) Token: 0x06009F10 RID: 40720
	public delegate void OnParticleCapture(HighEnergyParticle particle);

	// Token: 0x02001969 RID: 6505
	// (Invoke) Token: 0x06009F14 RID: 40724
	public delegate bool OnParticleCaptureAllowed(HighEnergyParticle particle);
}
