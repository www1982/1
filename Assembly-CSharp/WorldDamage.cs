using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using STRINGS;
using UnityEngine;

// Token: 0x02000BE8 RID: 3048
[AddComponentMenu("KMonoBehaviour/scripts/WorldDamage")]
public class WorldDamage : KMonoBehaviour
{
	// Token: 0x170006B9 RID: 1721
	// (get) Token: 0x06005BB6 RID: 23478 RVA: 0x00211DC8 File Offset: 0x0020FFC8
	// (set) Token: 0x06005BB7 RID: 23479 RVA: 0x00211DCF File Offset: 0x0020FFCF
	public static WorldDamage Instance { get; private set; }

	// Token: 0x06005BB8 RID: 23480 RVA: 0x00211DD7 File Offset: 0x0020FFD7
	public static void DestroyInstance()
	{
		WorldDamage.Instance = null;
	}

	// Token: 0x06005BB9 RID: 23481 RVA: 0x00211DDF File Offset: 0x0020FFDF
	protected override void OnPrefabInit()
	{
		WorldDamage.Instance = this;
	}

	// Token: 0x06005BBA RID: 23482 RVA: 0x00211DE7 File Offset: 0x0020FFE7
	public void RestoreDamageToValue(int cell, float amount)
	{
		if (Grid.Damage[cell] > amount)
		{
			Grid.Damage[cell] = amount;
		}
	}

	// Token: 0x06005BBB RID: 23483 RVA: 0x00211DFB File Offset: 0x0020FFFB
	public float ApplyDamage(Sim.WorldDamageInfo damage_info)
	{
		return this.ApplyDamage(damage_info.gameCell, this.damageAmount, damage_info.damageSourceOffset, BUILDINGS.DAMAGESOURCES.LIQUID_PRESSURE, UI.GAMEOBJECTEFFECTS.DAMAGE_POPS.LIQUID_PRESSURE);
	}

	// Token: 0x06005BBC RID: 23484 RVA: 0x00211E2C File Offset: 0x0021002C
	public float ApplyDamage(int cell, float amount, int src_cell, WorldDamage.DamageType damageType, string source_name = null, string pop_text = null)
	{
		float num = 0f;
		if (Grid.Solid[cell])
		{
			float num2 = Grid.Damage[cell];
			num = Mathf.Min(amount, 1f - num2);
			num2 += amount;
			bool flag = num2 > 0.15f;
			if (flag && damageType != WorldDamage.DamageType.NoBuildingDamage)
			{
				GameObject gameObject = Grid.Objects[cell, 9];
				if (gameObject != null)
				{
					BuildingHP component = gameObject.GetComponent<BuildingHP>();
					if (component != null)
					{
						if (!component.invincible)
						{
							int num3 = Mathf.RoundToInt(Mathf.Max((float)component.HitPoints - (1f - num2) * (float)component.MaxHitPoints, 0f));
							gameObject.Trigger(-794517298, new BuildingHP.DamageSourceInfo
							{
								damage = num3,
								source = source_name,
								popString = pop_text
							});
						}
						else
						{
							num2 = 0f;
						}
					}
				}
			}
			Grid.Damage[cell] = Mathf.Min(1f, num2);
			if (Grid.Damage[cell] >= 1f)
			{
				this.DestroyCell(cell);
			}
			else if (Grid.IsValidCell(src_cell) && flag)
			{
				Element element = Grid.Element[src_cell];
				if (element.IsLiquid && Grid.Mass[src_cell] > 1f)
				{
					int num4 = cell - src_cell;
					if (num4 == 1 || num4 == -1 || num4 == Grid.WidthInCells || num4 == -Grid.WidthInCells)
					{
						int num5 = cell + num4;
						if (Grid.IsValidCell(num5))
						{
							Element element2 = Grid.Element[num5];
							if (!element2.IsSolid && (!element2.IsLiquid || (element2.id == element.id && Grid.Mass[num5] <= 100f)) && (Grid.Properties[num5] & 2) == 0 && !this.spawnTimes.ContainsKey(num5))
							{
								this.spawnTimes[num5] = Time.realtimeSinceStartup;
								ushort idx = element.idx;
								float num6 = Grid.Temperature[src_cell];
								base.StartCoroutine(this.DelayedSpawnFX(src_cell, num5, num4, element, idx, num6));
							}
						}
					}
				}
			}
		}
		return num;
	}

	// Token: 0x06005BBD RID: 23485 RVA: 0x0021205A File Offset: 0x0021025A
	public float ApplyDamage(int cell, float amount, int src_cell, string source_name = null, string pop_text = null)
	{
		return this.ApplyDamage(cell, amount, src_cell, WorldDamage.DamageType.Absolute, source_name, pop_text);
	}

	// Token: 0x06005BBE RID: 23486 RVA: 0x0021206A File Offset: 0x0021026A
	private void ReleaseGO(GameObject go)
	{
		go.DeleteObject();
	}

	// Token: 0x06005BBF RID: 23487 RVA: 0x00212072 File Offset: 0x00210272
	private IEnumerator DelayedSpawnFX(int src_cell, int dest_cell, int offset, Element elem, ushort idx, float temperature)
	{
		float num = global::UnityEngine.Random.value * 0.25f;
		yield return new WaitForSeconds(num);
		Vector3 vector = Grid.CellToPosCCC(dest_cell, Grid.SceneLayer.Front);
		GameObject gameObject = GameUtil.KInstantiate(this.leakEffect.gameObject, vector, Grid.SceneLayer.Front, null, 0);
		KBatchedAnimController component = gameObject.GetComponent<KBatchedAnimController>();
		component.TintColour = elem.substance.colour;
		component.onDestroySelf = new Action<GameObject>(this.ReleaseGO);
		SimMessages.AddRemoveSubstance(src_cell, idx, CellEventLogger.Instance.WorldDamageDelayedSpawnFX, -1f, temperature, byte.MaxValue, 0, true, -1);
		if (offset == -1)
		{
			component.Play("side", KAnim.PlayMode.Once, 1f, 0f);
			component.FlipX = true;
			component.enabled = false;
			component.enabled = true;
			gameObject.transform.SetPosition(gameObject.transform.GetPosition() + Vector3.right * 0.5f);
			FallingWater.instance.AddParticle(dest_cell, idx, 1f, temperature, byte.MaxValue, 0, true, false, false, false);
		}
		else if (offset == Grid.WidthInCells)
		{
			gameObject.transform.SetPosition(gameObject.transform.GetPosition() - Vector3.up * 0.5f);
			component.Play("floor", KAnim.PlayMode.Once, 1f, 0f);
			component.enabled = false;
			component.enabled = true;
			SimMessages.AddRemoveSubstance(dest_cell, idx, CellEventLogger.Instance.WorldDamageDelayedSpawnFX, 1f, temperature, byte.MaxValue, 0, true, -1);
		}
		else if (offset == -Grid.WidthInCells)
		{
			component.Play("ceiling", KAnim.PlayMode.Once, 1f, 0f);
			component.enabled = false;
			component.enabled = true;
			gameObject.transform.SetPosition(gameObject.transform.GetPosition() + Vector3.up * 0.5f);
			FallingWater.instance.AddParticle(dest_cell, idx, 1f, temperature, byte.MaxValue, 0, true, false, false, false);
		}
		else
		{
			component.Play("side", KAnim.PlayMode.Once, 1f, 0f);
			component.enabled = false;
			component.enabled = true;
			gameObject.transform.SetPosition(gameObject.transform.GetPosition() - Vector3.right * 0.5f);
			FallingWater.instance.AddParticle(dest_cell, idx, 1f, temperature, byte.MaxValue, 0, true, false, false, false);
		}
		if (CameraController.Instance.IsAudibleSound(gameObject.transform.GetPosition(), this.leakSoundMigrated))
		{
			SoundEvent.PlayOneShot(this.leakSoundMigrated, gameObject.transform.GetPosition(), 1f);
		}
		yield return null;
		yield break;
	}

	// Token: 0x06005BC0 RID: 23488 RVA: 0x002120B0 File Offset: 0x002102B0
	private void Update()
	{
		this.expiredCells.Clear();
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		foreach (KeyValuePair<int, float> keyValuePair in this.spawnTimes)
		{
			if (realtimeSinceStartup - keyValuePair.Value > 1f)
			{
				this.expiredCells.Add(keyValuePair.Key);
			}
		}
		foreach (int num in this.expiredCells)
		{
			this.spawnTimes.Remove(num);
		}
		this.expiredCells.Clear();
	}

	// Token: 0x06005BC1 RID: 23489 RVA: 0x00212184 File Offset: 0x00210384
	public void DestroyCell(int cell)
	{
		if (Grid.Solid[cell])
		{
			SimMessages.Dig(cell, -1, false);
		}
	}

	// Token: 0x06005BC2 RID: 23490 RVA: 0x0021219B File Offset: 0x0021039B
	public void OnSolidStateChanged(int cell)
	{
		Grid.Damage[cell] = 0f;
	}

	// Token: 0x06005BC3 RID: 23491 RVA: 0x002121AC File Offset: 0x002103AC
	public void OnDigComplete(int cell, float mass, float temperature, ushort element_idx, byte disease_idx, int disease_count)
	{
		Vector3 vector = Grid.CellToPos(cell, CellAlignment.RandomInternal, Grid.SceneLayer.Ore);
		Element element = ElementLoader.elements[(int)element_idx];
		Grid.Damage[cell] = 0f;
		WorldDamage.Instance.PlaySoundForSubstance(element, vector);
		float num = mass * 0.5f;
		if (num <= 0f)
		{
			return;
		}
		GameObject gameObject = element.substance.SpawnResource(vector, num, temperature, disease_idx, disease_count, false, false, false);
		Pickupable component = gameObject.GetComponent<Pickupable>();
		if (component != null && component.GetMyWorld() != null && component.GetMyWorld().worldInventory.IsReachable(component))
		{
			PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Resource, Mathf.RoundToInt(num).ToString() + " " + element.name, gameObject.transform, 1.5f, false);
		}
	}

	// Token: 0x06005BC4 RID: 23492 RVA: 0x00212288 File Offset: 0x00210488
	private void PlaySoundForSubstance(Element element, Vector3 pos)
	{
		string text = element.substance.GetMiningBreakSound();
		if (text == null)
		{
			if (element.HasTag(GameTags.RefinedMetal))
			{
				text = "RefinedMetal";
			}
			else if (element.HasTag(GameTags.Metal))
			{
				text = "RawMetal";
			}
			else
			{
				text = "Rock";
			}
		}
		text = "Break_" + text;
		text = GlobalAssets.GetSound(text, false);
		if (CameraController.Instance && CameraController.Instance.IsAudibleSound(pos, text))
		{
			KFMOD.PlayOneShot(text, CameraController.Instance.GetVerticallyScaledPosition(pos, false), 1f);
		}
	}

	// Token: 0x04003CCC RID: 15564
	public KBatchedAnimController leakEffect;

	// Token: 0x04003CCD RID: 15565
	[SerializeField]
	private FMODAsset leakSound;

	// Token: 0x04003CCE RID: 15566
	[SerializeField]
	private EventReference leakSoundMigrated;

	// Token: 0x04003CCF RID: 15567
	private float damageAmount = 0.00083333335f;

	// Token: 0x04003CD1 RID: 15569
	private const float SPAWN_DELAY = 1f;

	// Token: 0x04003CD2 RID: 15570
	private Dictionary<int, float> spawnTimes = new Dictionary<int, float>();

	// Token: 0x04003CD3 RID: 15571
	private List<int> expiredCells = new List<int>();

	// Token: 0x02001D17 RID: 7447
	public enum DamageType
	{
		// Token: 0x04008825 RID: 34853
		Absolute,
		// Token: 0x04008826 RID: 34854
		NoBuildingDamage
	}
}
