using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x02000A47 RID: 2631
[AddComponentMenu("KMonoBehaviour/scripts/OrbitalObject")]
[SerializationConfig(MemberSerialization.OptIn)]
public class OrbitalObject : KMonoBehaviour, IRenderEveryTick
{
	// Token: 0x06004C55 RID: 19541 RVA: 0x001BAD10 File Offset: 0x001B8F10
	public void Init(string orbit_data_name, WorldContainer orbiting_world, List<Ref<OrbitalObject>> orbiting_obj)
	{
		OrbitalData orbitalData = Db.Get().OrbitalTypeCategories.Get(orbit_data_name);
		if (orbiting_world != null)
		{
			this.orbitingWorldId = orbiting_world.id;
			this.world = orbiting_world;
			this.worldOrbitingOrigin = this.GetWorldOrigin(this.world, orbitalData);
		}
		else
		{
			this.worldOrbitingOrigin = new Vector3((float)Grid.WidthInCells * 0.5f, (float)Grid.HeightInCells * orbitalData.yGridPercent, 0f);
		}
		this.animFilename = orbitalData.animFile;
		this.initialAnim = this.GetInitialAnim(orbitalData);
		this.angle = this.GetAngle(orbitalData);
		this.timeoffset = this.GetTimeOffset(orbiting_obj);
		this.orbitalDBId = orbitalData.Id;
	}

	// Token: 0x06004C56 RID: 19542 RVA: 0x001BADC8 File Offset: 0x001B8FC8
	protected override void OnSpawn()
	{
		this.world = ClusterManager.Instance.GetWorld(this.orbitingWorldId);
		this.orbitData = Db.Get().OrbitalTypeCategories.Get(this.orbitalDBId);
		base.gameObject.SetActive(false);
		KBatchedAnimController kbatchedAnimController = base.gameObject.AddComponent<KBatchedAnimController>();
		kbatchedAnimController.isMovable = true;
		kbatchedAnimController.initialAnim = this.initialAnim;
		kbatchedAnimController.AnimFiles = new KAnimFile[] { Assets.GetAnim(this.animFilename) };
		kbatchedAnimController.initialMode = KAnim.PlayMode.Loop;
		kbatchedAnimController.visibilityType = KAnimControllerBase.VisibilityType.Always;
		this.animController = kbatchedAnimController;
	}

	// Token: 0x06004C57 RID: 19543 RVA: 0x001BAE68 File Offset: 0x001B9068
	public void RenderEveryTick(float dt)
	{
		float num = 450f;
		bool flag;
		Vector3 vector = this.CalculateWorldPos(num, out flag);
		Vector3 vector2 = vector;
		if (this.orbitData.periodInCycles > 0f)
		{
			vector2.x = vector.x / (float)Grid.WidthInCells;
			vector2.y = vector.y / (float)Grid.HeightInCells;
			vector2.x = Camera.main.ViewportToWorldPoint(vector2).x;
			vector2.y = Camera.main.ViewportToWorldPoint(vector2).y;
		}
		bool flag2 = (!this.orbitData.rotatesBehind || !flag) && (this.world == null || ClusterManager.Instance.activeWorldId == this.world.id);
		Vector3 vector3 = vector2 - base.gameObject.transform.position;
		vector3.z = 0f;
		this.animController.Offset = vector3;
		Vector3 vector4 = vector2;
		vector4.x = this.worldOrbitingOrigin.x;
		vector4.y = this.worldOrbitingOrigin.y;
		base.gameObject.transform.SetPosition(vector4);
		if (this.orbitData.periodInCycles > 0f)
		{
			base.gameObject.transform.localScale = Vector3.one * (CameraController.Instance.baseCamera.orthographicSize / this.orbitData.distance);
		}
		else
		{
			base.gameObject.transform.localScale = Vector3.one * this.orbitData.distance;
		}
		if (base.gameObject.activeSelf != flag2)
		{
			base.gameObject.SetActive(flag2);
		}
	}

	// Token: 0x06004C58 RID: 19544 RVA: 0x001BB024 File Offset: 0x001B9224
	private Vector3 CalculateWorldPos(float time, out bool behind)
	{
		Vector3 vector3;
		if (this.orbitData.periodInCycles > 0f)
		{
			float num = this.orbitData.periodInCycles * 600f;
			float num2 = ((time + (float)this.timeoffset) / num - (float)((int)((time + (float)this.timeoffset) / num))) * 2f * 3.1415927f;
			float num3 = 0.5f * this.orbitData.radiusScale * (float)this.world.WorldSize.x;
			Vector3 vector = new Vector3(Mathf.Cos(num2), 0f, Mathf.Sin(num2));
			behind = vector.z > this.orbitData.behindZ;
			Vector3 vector2 = Quaternion.Euler(this.angle, 0f, 0f) * (vector * num3);
			vector3 = this.worldOrbitingOrigin + vector2;
			vector3.z = ((this.orbitData.GetRenderZ == null) ? this.orbitData.renderZ : this.orbitData.GetRenderZ());
		}
		else
		{
			behind = false;
			vector3 = this.worldOrbitingOrigin;
			vector3.z = ((this.orbitData.GetRenderZ == null) ? this.orbitData.renderZ : this.orbitData.GetRenderZ());
		}
		return vector3;
	}

	// Token: 0x06004C59 RID: 19545 RVA: 0x001BB174 File Offset: 0x001B9374
	private string GetInitialAnim(OrbitalData data)
	{
		if (data.initialAnim.IsNullOrWhiteSpace())
		{
			KAnimFileData data2 = Assets.GetAnim(data.animFile).GetData();
			int num = new KRandom().Next(0, data2.animCount - 1);
			return data2.GetAnim(num).name;
		}
		return data.initialAnim;
	}

	// Token: 0x06004C5A RID: 19546 RVA: 0x001BB1CC File Offset: 0x001B93CC
	private Vector3 GetWorldOrigin(WorldContainer wc, OrbitalData data)
	{
		if (wc != null)
		{
			float num = (float)wc.WorldOffset.x + (float)wc.WorldSize.x * data.xGridPercent;
			float num2 = (float)wc.WorldOffset.y + (float)wc.WorldSize.y * data.yGridPercent;
			return new Vector3(num, num2, 0f);
		}
		return new Vector3((float)Grid.WidthInCells * data.xGridPercent, (float)Grid.HeightInCells * data.yGridPercent, 0f);
	}

	// Token: 0x06004C5B RID: 19547 RVA: 0x001BB253 File Offset: 0x001B9453
	private float GetAngle(OrbitalData data)
	{
		return global::UnityEngine.Random.Range(data.minAngle, data.maxAngle);
	}

	// Token: 0x06004C5C RID: 19548 RVA: 0x001BB268 File Offset: 0x001B9468
	private int GetTimeOffset(List<Ref<OrbitalObject>> orbiting_obj)
	{
		List<int> list = new List<int>();
		foreach (Ref<OrbitalObject> @ref in orbiting_obj)
		{
			if (@ref.Get().world == this.world)
			{
				list.Add(@ref.Get().timeoffset);
			}
		}
		int num = global::UnityEngine.Random.Range(0, 600);
		while (list.Contains(num))
		{
			num = global::UnityEngine.Random.Range(0, 600);
		}
		return num;
	}

	// Token: 0x04003292 RID: 12946
	private WorldContainer world;

	// Token: 0x04003293 RID: 12947
	private OrbitalData orbitData;

	// Token: 0x04003294 RID: 12948
	private KBatchedAnimController animController;

	// Token: 0x04003295 RID: 12949
	[Serialize]
	private string animFilename;

	// Token: 0x04003296 RID: 12950
	[Serialize]
	private string initialAnim;

	// Token: 0x04003297 RID: 12951
	[Serialize]
	private Vector3 worldOrbitingOrigin;

	// Token: 0x04003298 RID: 12952
	[Serialize]
	private int orbitingWorldId;

	// Token: 0x04003299 RID: 12953
	[Serialize]
	private float angle;

	// Token: 0x0400329A RID: 12954
	[Serialize]
	public int timeoffset;

	// Token: 0x0400329B RID: 12955
	[Serialize]
	public string orbitalDBId;
}
