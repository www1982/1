using System;
using System.Collections.Generic;
using FMOD.Studio;
using KSerialization;
using UnityEngine;

// Token: 0x020009C8 RID: 2504
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/MiniComet")]
public class MiniComet : KMonoBehaviour, ISim33ms
{
	// Token: 0x1700051F RID: 1311
	// (get) Token: 0x0600490E RID: 18702 RVA: 0x001A5C20 File Offset: 0x001A3E20
	public Vector3 TargetPosition
	{
		get
		{
			return this.anim.PositionIncludingOffset;
		}
	}

	// Token: 0x17000520 RID: 1312
	// (get) Token: 0x0600490F RID: 18703 RVA: 0x001A5C2D File Offset: 0x001A3E2D
	// (set) Token: 0x06004910 RID: 18704 RVA: 0x001A5C35 File Offset: 0x001A3E35
	public Vector2 Velocity
	{
		get
		{
			return this.velocity;
		}
		set
		{
			this.velocity = value;
		}
	}

	// Token: 0x06004911 RID: 18705 RVA: 0x001A5C40 File Offset: 0x001A3E40
	private float GetVolume(GameObject gameObject)
	{
		float num = 1f;
		if (gameObject != null && this.selectable != null && this.selectable.IsSelected)
		{
			num = 1f;
		}
		return num;
	}

	// Token: 0x06004912 RID: 18706 RVA: 0x001A5C7E File Offset: 0x001A3E7E
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.loopingSounds = base.gameObject.GetComponent<LoopingSounds>();
		this.flyingSound = GlobalAssets.GetSound("Meteor_LP", false);
		this.RandomizeVelocity();
	}

	// Token: 0x06004913 RID: 18707 RVA: 0x001A5CB0 File Offset: 0x001A3EB0
	protected override void OnSpawn()
	{
		this.anim.Offset = this.offsetPosition;
		if (this.spawnWithOffset)
		{
			this.SetupOffset();
		}
		base.OnSpawn();
		this.StartLoopingSound();
		bool flag = this.offsetPosition.x != 0f || this.offsetPosition.y != 0f;
		this.selectable.enabled = !flag;
		this.typeID = base.GetComponent<KPrefabID>().PrefabTag;
	}

	// Token: 0x06004914 RID: 18708 RVA: 0x001A5D33 File Offset: 0x001A3F33
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
	}

	// Token: 0x06004915 RID: 18709 RVA: 0x001A5D3C File Offset: 0x001A3F3C
	protected void SetupOffset()
	{
		Vector3 position = base.transform.GetPosition();
		Vector3 position2 = base.transform.GetPosition();
		position2.z = 0f;
		Vector3 vector = new Vector3(this.velocity.x, this.velocity.y, 0f);
		WorldContainer myWorld = base.gameObject.GetMyWorld();
		float num = (float)(myWorld.WorldOffset.y + myWorld.Height + MissileLauncher.Def.launchRange.y) * Grid.CellSizeInMeters - position2.y;
		float num2 = Vector3.Angle(Vector3.up, -vector) * 0.017453292f;
		float num3 = Mathf.Abs(num / Mathf.Cos(num2));
		Vector3 vector2 = position2 - vector.normalized * num3;
		float num4 = (float)(myWorld.WorldOffset.x + myWorld.Width) * Grid.CellSizeInMeters;
		if (vector2.x < (float)myWorld.WorldOffset.x * Grid.CellSizeInMeters || vector2.x > num4)
		{
			float num5 = ((vector.x < 0f) ? (num4 - position2.x) : (position2.x - (float)myWorld.WorldOffset.x * Grid.CellSizeInMeters));
			num2 = Vector3.Angle((vector.x < 0f) ? Vector3.right : Vector3.left, -vector) * 0.017453292f;
			num3 = Mathf.Abs(num5 / Mathf.Cos(num2));
		}
		Vector3 vector3 = -vector.normalized * num3;
		(position2 + vector3).z = position.z;
		this.offsetPosition = vector3;
		this.anim.Offset = this.offsetPosition;
	}

	// Token: 0x06004916 RID: 18710 RVA: 0x001A5F00 File Offset: 0x001A4100
	public virtual void RandomizeVelocity()
	{
		float num = global::UnityEngine.Random.Range(this.spawnAngle.x, this.spawnAngle.y);
		float num2 = num * 3.1415927f / 180f;
		float num3 = global::UnityEngine.Random.Range(this.spawnVelocity.x, this.spawnVelocity.y);
		this.velocity = new Vector2(-Mathf.Cos(num2) * num3, Mathf.Sin(num2) * num3);
		base.GetComponent<KBatchedAnimController>().Rotation = -num - 90f;
	}

	// Token: 0x06004917 RID: 18711 RVA: 0x001A5F82 File Offset: 0x001A4182
	public int GetRandomNumOres()
	{
		return global::UnityEngine.Random.Range(this.explosionOreCount.x, this.explosionOreCount.y + 1);
	}

	// Token: 0x06004918 RID: 18712 RVA: 0x001A5FA4 File Offset: 0x001A41A4
	[ContextMenu("Explode")]
	private void Explode(Vector3 pos, int cell, int prev_cell, Element element)
	{
		byte b = Grid.WorldIdx[cell];
		this.PlayImpactSound(pos);
		Vector3 vector = pos;
		vector.z = Grid.GetLayerZ(Grid.SceneLayer.FXFront2);
		if (this.explosionEffectHash != SpawnFXHashes.None)
		{
			Game.Instance.SpawnFX(this.explosionEffectHash, vector, 0f);
		}
		if (element != null)
		{
			Substance substance = element.substance;
			int randomNumOres = this.GetRandomNumOres();
			Vector2 vector2 = -this.velocity.normalized;
			Vector2 vector3 = new Vector2(vector2.y, -vector2.x);
			float num = ((randomNumOres > 0) ? (this.pe.Mass / (float)randomNumOres) : 1f);
			for (int i = 0; i < randomNumOres; i++)
			{
				Vector2 normalized = (vector2 + vector3 * global::UnityEngine.Random.Range(-1f, 1f)).normalized;
				Vector3 vector4 = normalized * global::UnityEngine.Random.Range(this.explosionSpeedRange.x, this.explosionSpeedRange.y);
				Vector3 vector5 = vector + normalized.normalized * 1.25f;
				GameObject gameObject = substance.SpawnResource(vector5, num, this.pe.Temperature, this.pe.DiseaseIdx, this.pe.DiseaseCount / randomNumOres, false, false, false);
				if (GameComps.Fallers.Has(gameObject))
				{
					GameComps.Fallers.Remove(gameObject);
				}
				GameComps.Fallers.Add(gameObject, vector4);
			}
		}
		if (this.OnImpact != null)
		{
			this.OnImpact();
		}
	}

	// Token: 0x06004919 RID: 18713 RVA: 0x001A613C File Offset: 0x001A433C
	public float GetDistanceFromImpact()
	{
		float num = this.velocity.x / this.velocity.y;
		Vector3 position = base.transform.GetPosition();
		float num2 = 0f;
		while (num2 > -6f)
		{
			num2 -= 1f;
			num2 = Mathf.Ceil(position.y + num2) - 0.2f - position.y;
			float num3 = num2 * num;
			Vector3 vector = new Vector3(num3, num2, 0f);
			int num4 = Grid.PosToCell(position + vector);
			if (Grid.IsValidCell(num4) && Grid.Solid[num4])
			{
				return vector.magnitude;
			}
		}
		return 6f;
	}

	// Token: 0x0600491A RID: 18714 RVA: 0x001A61E5 File Offset: 0x001A43E5
	public float GetSoundDistance()
	{
		return this.GetDistanceFromImpact();
	}

	// Token: 0x0600491B RID: 18715 RVA: 0x001A61F0 File Offset: 0x001A43F0
	public void Sim33ms(float dt)
	{
		if (this.hasExploded)
		{
			return;
		}
		if (this.offsetPosition.y > 0f)
		{
			Vector3 vector = new Vector3(this.velocity.x * dt, this.velocity.y * dt, 0f);
			Vector3 vector2 = this.offsetPosition + vector;
			this.offsetPosition = vector2;
			this.anim.Offset = this.offsetPosition;
		}
		else
		{
			if (this.anim.Offset != Vector3.zero)
			{
				this.anim.Offset = Vector3.zero;
			}
			if (!this.selectable.enabled)
			{
				this.selectable.enabled = true;
			}
			Vector2 vector3 = new Vector2((float)Grid.WidthInCells, (float)Grid.HeightInCells) * -0.1f;
			Vector2 vector4 = new Vector2((float)Grid.WidthInCells, (float)Grid.HeightInCells) * 1.1f;
			Vector3 position = base.transform.GetPosition();
			Vector3 vector5 = position + new Vector3(this.velocity.x * dt, this.velocity.y * dt, 0f);
			Grid.PosToCell(vector5);
			this.loopingSounds.UpdateVelocity(this.flyingSound, vector5 - position);
			if (vector5.x < vector3.x || vector4.x < vector5.x || vector5.y < vector3.y)
			{
				global::Util.KDestroyGameObject(base.gameObject);
			}
			int num = Grid.PosToCell(this);
			int num2 = Grid.PosToCell(this.previousPosition);
			if (num != num2 && Grid.IsValidCell(num) && Grid.Solid[num])
			{
				PrimaryElement component = base.GetComponent<PrimaryElement>();
				this.Explode(position, num, num2, component.Element);
				this.hasExploded = true;
				global::Util.KDestroyGameObject(base.gameObject);
				return;
			}
			this.previousPosition = position;
			base.transform.SetPosition(vector5);
		}
		this.age += dt;
	}

	// Token: 0x0600491C RID: 18716 RVA: 0x001A6400 File Offset: 0x001A4600
	private void PlayImpactSound(Vector3 pos)
	{
		if (this.impactSound == null)
		{
			this.impactSound = "Meteor_Large_Impact";
		}
		this.loopingSounds.StopSound(this.flyingSound);
		string sound = GlobalAssets.GetSound(this.impactSound, false);
		int num = Grid.PosToCell(pos);
		if (Grid.IsValidCell(num) && (int)Grid.WorldIdx[num] == ClusterManager.Instance.activeWorldId)
		{
			float volume = this.GetVolume(base.gameObject);
			pos.z = 0f;
			EventInstance eventInstance = KFMOD.BeginOneShot(sound, pos, volume);
			eventInstance.setParameterByName("userVolume_SFX", KPlayerPrefs.GetFloat("Volume_SFX"), false);
			KFMOD.EndOneShot(eventInstance);
		}
	}

	// Token: 0x0600491D RID: 18717 RVA: 0x001A64A1 File Offset: 0x001A46A1
	private void StartLoopingSound()
	{
		this.loopingSounds.StartSound(this.flyingSound);
		this.loopingSounds.UpdateFirstParameter(this.flyingSound, this.FLYING_SOUND_ID_PARAMETER, (float)this.flyingSoundID);
	}

	// Token: 0x0600491E RID: 18718 RVA: 0x001A64D4 File Offset: 0x001A46D4
	public void Explode()
	{
		PrimaryElement component = base.GetComponent<PrimaryElement>();
		Vector3 position = base.transform.GetPosition();
		int num = Grid.PosToCell(position);
		this.Explode(position, num, num, component.Element);
		this.hasExploded = true;
		global::Util.KDestroyGameObject(base.gameObject);
	}

	// Token: 0x0400301D RID: 12317
	[MyCmpGet]
	private PrimaryElement pe;

	// Token: 0x0400301E RID: 12318
	public Vector2 spawnVelocity = new Vector2(7f, 9f);

	// Token: 0x0400301F RID: 12319
	public Vector2 spawnAngle = new Vector2(30f, 150f);

	// Token: 0x04003020 RID: 12320
	public SpawnFXHashes explosionEffectHash;

	// Token: 0x04003021 RID: 12321
	public int addDiseaseCount;

	// Token: 0x04003022 RID: 12322
	public byte diseaseIdx = byte.MaxValue;

	// Token: 0x04003023 RID: 12323
	public Vector2I explosionOreCount = new Vector2I(1, 1);

	// Token: 0x04003024 RID: 12324
	public Vector2 explosionSpeedRange = new Vector2(0f, 0f);

	// Token: 0x04003025 RID: 12325
	public string impactSound;

	// Token: 0x04003026 RID: 12326
	public string flyingSound;

	// Token: 0x04003027 RID: 12327
	public int flyingSoundID;

	// Token: 0x04003028 RID: 12328
	private HashedString FLYING_SOUND_ID_PARAMETER = "meteorType";

	// Token: 0x04003029 RID: 12329
	public bool Targeted;

	// Token: 0x0400302A RID: 12330
	[Serialize]
	protected Vector3 offsetPosition;

	// Token: 0x0400302B RID: 12331
	[Serialize]
	protected Vector2 velocity;

	// Token: 0x0400302C RID: 12332
	private Vector3 previousPosition;

	// Token: 0x0400302D RID: 12333
	private bool hasExploded;

	// Token: 0x0400302E RID: 12334
	public string[] craterPrefabs;

	// Token: 0x0400302F RID: 12335
	public bool spawnWithOffset;

	// Token: 0x04003030 RID: 12336
	private float age;

	// Token: 0x04003031 RID: 12337
	public global::System.Action OnImpact;

	// Token: 0x04003032 RID: 12338
	public Ref<KPrefabID> ignoreObstacleForDamage = new Ref<KPrefabID>();

	// Token: 0x04003033 RID: 12339
	[MyCmpGet]
	private KBatchedAnimController anim;

	// Token: 0x04003034 RID: 12340
	[MyCmpGet]
	private KSelectable selectable;

	// Token: 0x04003035 RID: 12341
	public Tag typeID;

	// Token: 0x04003036 RID: 12342
	private LoopingSounds loopingSounds;

	// Token: 0x04003037 RID: 12343
	private List<GameObject> damagedEntities = new List<GameObject>();

	// Token: 0x04003038 RID: 12344
	private List<int> destroyedCells = new List<int>();

	// Token: 0x04003039 RID: 12345
	private const float MAX_DISTANCE_TEST = 6f;
}
