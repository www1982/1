using System;
using System.Runtime.Serialization;
using KSerialization;
using UnityEngine;

// Token: 0x02000B06 RID: 2822
public class SelfChargingElectrobank : Electrobank
{
	// Token: 0x170005CE RID: 1486
	// (get) Token: 0x060052DA RID: 21210 RVA: 0x001E2A1E File Offset: 0x001E0C1E
	public float LifetimeRemaining
	{
		get
		{
			return this.lifetimeRemaining;
		}
	}

	// Token: 0x060052DB RID: 21211 RVA: 0x001E2A28 File Offset: 0x001E0C28
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.selectable = base.GetComponent<KSelectable>();
		this.selectable.AddStatusItem(Db.Get().MiscStatusItems.ElectrobankSelfCharging, 60f);
		this.lifetimeStatus = this.selectable.AddStatusItem(Db.Get().MiscStatusItems.ElectrobankLifetimeRemaining, this);
		Components.SelfChargingElectrobanks.Add(base.gameObject.GetMyWorldId(), this);
		if (this.lifetimeRemaining <= 0f)
		{
			this.Delete();
		}
	}

	// Token: 0x060052DC RID: 21212 RVA: 0x001E2AB8 File Offset: 0x001E0CB8
	[OnDeserialized]
	private void OnDeserialized()
	{
		PrimaryElement component = base.GetComponent<PrimaryElement>();
		if (component != null)
		{
			component.Mass = 20f;
		}
	}

	// Token: 0x060052DD RID: 21213 RVA: 0x001E2AE0 File Offset: 0x001E0CE0
	public override void Sim200ms(float dt)
	{
		base.Sim200ms(dt);
		if (this.lifetimeRemaining > 0f)
		{
			base.AddPower(dt * 60f);
			this.lifetimeRemaining -= dt;
			return;
		}
		this.Explode();
	}

	// Token: 0x060052DE RID: 21214 RVA: 0x001E2B1C File Offset: 0x001E0D1C
	public override void Explode()
	{
		Game.Instance.SpawnFX(SpawnFXHashes.MeteorImpactMetal, base.gameObject.transform.position, 0f);
		KFMOD.PlayOneShot(GlobalAssets.GetSound("Battery_explode", false), base.gameObject.transform.position, 1f);
		base.LaunchNearbyStuff();
		SimMessages.AddRemoveSubstance(Grid.PosToCell(base.transform.position), SimHashes.NuclearWaste, CellEventLogger.Instance.ElementEmitted, 20f, 3000f, Db.Get().Diseases.GetIndex(Db.Get().Diseases.RadiationPoisoning.Id), Mathf.RoundToInt(10000000f), true, -1);
		if (base.transform.parent != null)
		{
			Storage component = base.transform.parent.GetComponent<Storage>();
			if (component != null)
			{
				Health component2 = component.GetComponent<Health>();
				if (component2 != null)
				{
					component2.Damage(500f);
				}
			}
		}
		this.Delete();
	}

	// Token: 0x060052DF RID: 21215 RVA: 0x001E2C28 File Offset: 0x001E0E28
	private void Delete()
	{
		if (!this.IsNullOrDestroyed() && !base.gameObject.IsNullOrDestroyed())
		{
			base.gameObject.DeleteObject();
		}
	}

	// Token: 0x060052E0 RID: 21216 RVA: 0x001E2C4A File Offset: 0x001E0E4A
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Components.SelfChargingElectrobanks.Remove(base.gameObject.GetMyWorldId(), this);
	}

	// Token: 0x040037C0 RID: 14272
	[Serialize]
	private float lifetimeRemaining = 90000f;

	// Token: 0x040037C1 RID: 14273
	private KSelectable selectable;

	// Token: 0x040037C2 RID: 14274
	private Guid lifetimeStatus = Guid.Empty;
}
