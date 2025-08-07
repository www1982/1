using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000AB5 RID: 2741
[AddComponentMenu("KMonoBehaviour/scripts/Radiator")]
public class Radiator : KMonoBehaviour, IGameObjectEffectDescriptor
{
	// Token: 0x06004F7D RID: 20349 RVA: 0x001CBD34 File Offset: 0x001C9F34
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.emitter = new RadiationGridEmitter(Grid.PosToCell(base.gameObject), this.intensity);
		this.emitter.projectionCount = this.projectionCount;
		this.emitter.direction = this.direction;
		this.emitter.angle = this.angle;
		if (base.GetComponent<Operational>() == null)
		{
			this.emitter.enabled = true;
		}
		else
		{
			base.Subscribe(824508782, new Action<object>(this.OnOperationalChanged));
		}
		RadiationGridManager.emitters.Add(this.emitter);
	}

	// Token: 0x06004F7E RID: 20350 RVA: 0x001CBDDA File Offset: 0x001C9FDA
	protected override void OnCleanUp()
	{
		RadiationGridManager.emitters.Remove(this.emitter);
		base.OnCleanUp();
	}

	// Token: 0x06004F7F RID: 20351 RVA: 0x001CBDF4 File Offset: 0x001C9FF4
	private void OnOperationalChanged(object data)
	{
		bool isActive = base.GetComponent<Operational>().IsActive;
		this.emitter.enabled = isActive;
	}

	// Token: 0x06004F80 RID: 20352 RVA: 0x001CBE19 File Offset: 0x001CA019
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		return new List<Descriptor>
		{
			new Descriptor(string.Format(UI.GAMEOBJECTEFFECTS.EMITS_LIGHT, this.intensity), UI.GAMEOBJECTEFFECTS.TOOLTIPS.EMITS_LIGHT, Descriptor.DescriptorType.Effect, false)
		};
	}

	// Token: 0x06004F81 RID: 20353 RVA: 0x001CBE51 File Offset: 0x001CA051
	private void Update()
	{
		this.emitter.originCell = Grid.PosToCell(base.gameObject);
	}

	// Token: 0x0400357B RID: 13691
	public RadiationGridEmitter emitter;

	// Token: 0x0400357C RID: 13692
	public int intensity;

	// Token: 0x0400357D RID: 13693
	public int projectionCount;

	// Token: 0x0400357E RID: 13694
	public int direction;

	// Token: 0x0400357F RID: 13695
	public int angle = 360;
}
