using System;
using Klei.AI;
using KSerialization;
using UnityEngine;

// Token: 0x02000950 RID: 2384
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/Health")]
public class Health : KMonoBehaviour, ISaveLoadable
{
	// Token: 0x170004E2 RID: 1250
	// (get) Token: 0x0600445B RID: 17499 RVA: 0x00189162 File Offset: 0x00187362
	// (set) Token: 0x0600445C RID: 17500 RVA: 0x0018916A File Offset: 0x0018736A
	[Serialize]
	public Health.HealthState State { get; private set; }

	// Token: 0x170004E3 RID: 1251
	// (get) Token: 0x0600445D RID: 17501 RVA: 0x00189173 File Offset: 0x00187373
	// (set) Token: 0x0600445E RID: 17502 RVA: 0x0018917B File Offset: 0x0018737B
	[Serialize]
	public Tag CauseOfIncapacitation { get; private set; }

	// Token: 0x170004E4 RID: 1252
	// (get) Token: 0x0600445F RID: 17503 RVA: 0x00189184 File Offset: 0x00187384
	public AmountInstance GetAmountInstance
	{
		get
		{
			return this.amountInstance;
		}
	}

	// Token: 0x170004E5 RID: 1253
	// (get) Token: 0x06004460 RID: 17504 RVA: 0x0018918C File Offset: 0x0018738C
	// (set) Token: 0x06004461 RID: 17505 RVA: 0x00189199 File Offset: 0x00187399
	public float hitPoints
	{
		get
		{
			return this.amountInstance.value;
		}
		set
		{
			this.amountInstance.value = value;
		}
	}

	// Token: 0x170004E6 RID: 1254
	// (get) Token: 0x06004462 RID: 17506 RVA: 0x001891A7 File Offset: 0x001873A7
	public float maxHitPoints
	{
		get
		{
			return this.amountInstance.GetMax();
		}
	}

	// Token: 0x06004463 RID: 17507 RVA: 0x001891B4 File Offset: 0x001873B4
	public float percent()
	{
		return this.hitPoints / this.maxHitPoints;
	}

	// Token: 0x06004464 RID: 17508 RVA: 0x001891C4 File Offset: 0x001873C4
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		Components.Health.Add(this);
		this.amountInstance = Db.Get().Amounts.HitPoints.Lookup(base.gameObject);
		this.amountInstance.value = this.amountInstance.GetMax();
		AmountInstance amountInstance = this.amountInstance;
		amountInstance.OnDelta = (Action<float>)Delegate.Combine(amountInstance.OnDelta, new Action<float>(this.OnHealthChanged));
	}

	// Token: 0x06004465 RID: 17509 RVA: 0x00189240 File Offset: 0x00187440
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.State == Health.HealthState.Incapacitated || this.hitPoints == 0f)
		{
			if (this.canBeIncapacitated)
			{
				this.Incapacitate(GameTags.HitPointsDepleted);
			}
			else
			{
				this.Kill();
			}
		}
		if (this.State != Health.HealthState.Incapacitated && this.State != Health.HealthState.Dead)
		{
			this.UpdateStatus();
		}
		this.effects = base.GetComponent<Effects>();
		this.UpdateHealthBar();
		this.UpdateWoundEffects();
	}

	// Token: 0x06004466 RID: 17510 RVA: 0x001892B4 File Offset: 0x001874B4
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Components.Health.Remove(this);
	}

	// Token: 0x06004467 RID: 17511 RVA: 0x001892C8 File Offset: 0x001874C8
	public void UpdateHealthBar()
	{
		if (NameDisplayScreen.Instance == null)
		{
			return;
		}
		bool flag = this.State == Health.HealthState.Dead || this.State == Health.HealthState.Incapacitated || this.hitPoints >= this.maxHitPoints || base.gameObject.HasTag("HideHealthBar");
		NameDisplayScreen.Instance.SetHealthDisplay(base.gameObject, new Func<float>(this.percent), !flag);
	}

	// Token: 0x06004468 RID: 17512 RVA: 0x0018933C File Offset: 0x0018753C
	private void OnRecover()
	{
		base.GetComponent<KPrefabID>().RemoveTag(GameTags.HitPointsDepleted);
	}

	// Token: 0x06004469 RID: 17513 RVA: 0x00189350 File Offset: 0x00187550
	public void OnHealthChanged(float delta)
	{
		base.Trigger(-1664904872, delta);
		if (this.State != Health.HealthState.Invincible)
		{
			if (this.hitPoints == 0f && !this.IsDefeated())
			{
				if (this.canBeIncapacitated)
				{
					this.Incapacitate(GameTags.HitPointsDepleted);
				}
				else
				{
					this.Kill();
				}
			}
			else
			{
				base.GetComponent<KPrefabID>().RemoveTag(GameTags.HitPointsDepleted);
			}
		}
		this.UpdateStatus();
		this.UpdateWoundEffects();
		this.UpdateHealthBar();
	}

	// Token: 0x0600446A RID: 17514 RVA: 0x001893CB File Offset: 0x001875CB
	[ContextMenu("DoDamage")]
	public void DoDamage()
	{
		this.Damage(1f);
	}

	// Token: 0x0600446B RID: 17515 RVA: 0x001893D8 File Offset: 0x001875D8
	public void Damage(float amount)
	{
		if (this.State != Health.HealthState.Invincible)
		{
			this.hitPoints = Mathf.Max(0f, this.hitPoints - amount);
		}
		this.OnHealthChanged(-amount);
	}

	// Token: 0x0600446C RID: 17516 RVA: 0x00189404 File Offset: 0x00187604
	private void UpdateWoundEffects()
	{
		if (!this.effects)
		{
			return;
		}
		if (this.isCritter != this.isCritterPrev)
		{
			if (this.isCritterPrev)
			{
				this.effects.Remove("LightWoundsCritter");
				this.effects.Remove("ModerateWoundsCritter");
				this.effects.Remove("SevereWoundsCritter");
			}
			else
			{
				this.effects.Remove("LightWounds");
				this.effects.Remove("ModerateWounds");
				this.effects.Remove("SevereWounds");
			}
			this.isCritterPrev = this.isCritter;
		}
		string text;
		string text2;
		string text3;
		if (this.isCritter)
		{
			text = "LightWoundsCritter";
			text2 = "ModerateWoundsCritter";
			text3 = "SevereWoundsCritter";
		}
		else
		{
			text = "LightWounds";
			text2 = "ModerateWounds";
			text3 = "SevereWounds";
		}
		switch (this.State)
		{
		case Health.HealthState.Perfect:
		case Health.HealthState.Alright:
		case Health.HealthState.Incapacitated:
		case Health.HealthState.Dead:
			this.effects.Remove(text);
			this.effects.Remove(text2);
			this.effects.Remove(text3);
			break;
		case Health.HealthState.Scuffed:
			if (!this.effects.HasEffect(text))
			{
				this.effects.Add(text, true);
			}
			this.effects.Remove(text2);
			this.effects.Remove(text3);
			return;
		case Health.HealthState.Injured:
			this.effects.Remove(text);
			if (!this.effects.HasEffect(text2))
			{
				this.effects.Add(text2, true);
			}
			this.effects.Remove(text3);
			return;
		case Health.HealthState.Critical:
			this.effects.Remove(text);
			this.effects.Remove(text2);
			if (!this.effects.HasEffect(text3))
			{
				this.effects.Add(text3, true);
				return;
			}
			break;
		default:
			return;
		}
	}

	// Token: 0x0600446D RID: 17517 RVA: 0x001895C0 File Offset: 0x001877C0
	private void UpdateStatus()
	{
		float num = this.hitPoints / this.maxHitPoints;
		Health.HealthState healthState;
		if (this.State == Health.HealthState.Invincible)
		{
			healthState = Health.HealthState.Invincible;
		}
		else if (num >= 1f)
		{
			healthState = Health.HealthState.Perfect;
		}
		else if (num >= 0.85f)
		{
			healthState = Health.HealthState.Alright;
		}
		else if (num >= 0.66f)
		{
			healthState = Health.HealthState.Scuffed;
		}
		else if ((double)num >= 0.33)
		{
			healthState = Health.HealthState.Injured;
		}
		else if (num > 0f)
		{
			healthState = Health.HealthState.Critical;
		}
		else if (num == 0f)
		{
			healthState = Health.HealthState.Incapacitated;
		}
		else
		{
			healthState = Health.HealthState.Dead;
		}
		if (this.State != healthState)
		{
			if (this.State == Health.HealthState.Incapacitated && healthState != Health.HealthState.Dead)
			{
				this.OnRecover();
			}
			if (healthState == Health.HealthState.Perfect)
			{
				base.Trigger(-1491582671, this);
			}
			this.State = healthState;
			KSelectable component = base.GetComponent<KSelectable>();
			if (this.State != Health.HealthState.Dead && this.State != Health.HealthState.Perfect && this.State != Health.HealthState.Alright && !this.isCritter)
			{
				component.SetStatusItem(Db.Get().StatusItemCategories.Hitpoints, Db.Get().CreatureStatusItems.HealthStatus, this.State);
				return;
			}
			component.SetStatusItem(Db.Get().StatusItemCategories.Hitpoints, null, null);
		}
	}

	// Token: 0x0600446E RID: 17518 RVA: 0x001896DE File Offset: 0x001878DE
	public bool IsIncapacitated()
	{
		return this.State == Health.HealthState.Incapacitated;
	}

	// Token: 0x0600446F RID: 17519 RVA: 0x001896E9 File Offset: 0x001878E9
	public bool IsDefeated()
	{
		return this.State == Health.HealthState.Incapacitated || this.State == Health.HealthState.Dead;
	}

	// Token: 0x06004470 RID: 17520 RVA: 0x001896FF File Offset: 0x001878FF
	public void Incapacitate(Tag cause)
	{
		this.CauseOfIncapacitation = cause;
		this.State = Health.HealthState.Incapacitated;
		this.Damage(this.hitPoints);
		base.gameObject.Trigger(-1506500077, null);
	}

	// Token: 0x06004471 RID: 17521 RVA: 0x0018972C File Offset: 0x0018792C
	private void Kill()
	{
		if (base.gameObject.GetSMI<DeathMonitor.Instance>() != null)
		{
			base.gameObject.GetSMI<DeathMonitor.Instance>().Kill(Db.Get().Deaths.Slain);
		}
	}

	// Token: 0x04002DBF RID: 11711
	[Serialize]
	public bool canBeIncapacitated;

	// Token: 0x04002DC2 RID: 11714
	public HealthBar healthBar;

	// Token: 0x04002DC3 RID: 11715
	public bool isCritter;

	// Token: 0x04002DC4 RID: 11716
	private bool isCritterPrev;

	// Token: 0x04002DC5 RID: 11717
	private Effects effects;

	// Token: 0x04002DC6 RID: 11718
	private AmountInstance amountInstance;

	// Token: 0x02001963 RID: 6499
	public enum HealthState
	{
		// Token: 0x04007BF5 RID: 31733
		Perfect,
		// Token: 0x04007BF6 RID: 31734
		Alright,
		// Token: 0x04007BF7 RID: 31735
		Scuffed,
		// Token: 0x04007BF8 RID: 31736
		Injured,
		// Token: 0x04007BF9 RID: 31737
		Critical,
		// Token: 0x04007BFA RID: 31738
		Incapacitated,
		// Token: 0x04007BFB RID: 31739
		Dead,
		// Token: 0x04007BFC RID: 31740
		Invincible
	}
}
