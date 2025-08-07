using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000616 RID: 1558
public class SpriteSheetAnimator
{
	// Token: 0x060024FA RID: 9466 RVA: 0x000D30E0 File Offset: 0x000D12E0
	public SpriteSheetAnimator(SpriteSheet sheet)
	{
		this.sheet = sheet;
		this.mesh = new Mesh();
		this.mesh.name = "SpriteSheetAnimator";
		this.mesh.MarkDynamic();
		this.materialProperties = new MaterialPropertyBlock();
		this.materialProperties.SetTexture("_MainTex", sheet.texture);
	}

	// Token: 0x060024FB RID: 9467 RVA: 0x000D3158 File Offset: 0x000D1358
	public void Play(Vector3 pos, Quaternion rotation, Vector2 size, Color colour)
	{
		if (rotation == Quaternion.identity)
		{
			this.anims.Add(new SpriteSheetAnimator.AnimInfo
			{
				elapsedTime = 0f,
				pos = pos,
				rotation = rotation,
				size = size,
				colour = colour
			});
			return;
		}
		this.rotatedAnims.Add(new SpriteSheetAnimator.AnimInfo
		{
			elapsedTime = 0f,
			pos = pos,
			rotation = rotation,
			size = size,
			colour = colour
		});
	}

	// Token: 0x060024FC RID: 9468 RVA: 0x000D3200 File Offset: 0x000D1400
	private void GetUVs(int frame, out Vector2 uv_bl, out Vector2 uv_br, out Vector2 uv_tl, out Vector2 uv_tr)
	{
		int num = frame / this.sheet.numXFrames;
		int num2 = frame % this.sheet.numXFrames;
		float num3 = (float)num2 * this.sheet.uvFrameSize.x;
		float num4 = (float)(num2 + 1) * this.sheet.uvFrameSize.x;
		float num5 = 1f - (float)(num + 1) * this.sheet.uvFrameSize.y;
		float num6 = 1f - (float)num * this.sheet.uvFrameSize.y;
		uv_bl = new Vector2(num3, num5);
		uv_br = new Vector2(num4, num5);
		uv_tl = new Vector2(num3, num6);
		uv_tr = new Vector2(num4, num6);
	}

	// Token: 0x060024FD RID: 9469 RVA: 0x000D32C0 File Offset: 0x000D14C0
	public int GetFrameFromElapsedTime(float elapsed_time)
	{
		return Mathf.Min(this.sheet.numFrames, (int)(elapsed_time / 0.033333335f));
	}

	// Token: 0x060024FE RID: 9470 RVA: 0x000D32DC File Offset: 0x000D14DC
	public int GetFrameFromElapsedTimeLooping(float elapsed_time)
	{
		int num = (int)(elapsed_time / 0.033333335f);
		if (num > this.sheet.numFrames)
		{
			num %= this.sheet.numFrames;
		}
		return num;
	}

	// Token: 0x060024FF RID: 9471 RVA: 0x000D330F File Offset: 0x000D150F
	public void UpdateAnims(float dt)
	{
		this.UpdateAnims(dt, this.anims);
		this.UpdateAnims(dt, this.rotatedAnims);
	}

	// Token: 0x06002500 RID: 9472 RVA: 0x000D332C File Offset: 0x000D152C
	private void UpdateAnims(float dt, IList<SpriteSheetAnimator.AnimInfo> anims)
	{
		int num = anims.Count;
		int i = 0;
		while (i < num)
		{
			SpriteSheetAnimator.AnimInfo animInfo = anims[i];
			animInfo.elapsedTime += dt;
			animInfo.frame = Mathf.Min(this.sheet.numFrames, (int)(animInfo.elapsedTime / 0.033333335f));
			if (animInfo.frame >= this.sheet.numFrames)
			{
				num--;
				anims[i] = anims[num];
				anims.RemoveAt(num);
			}
			else
			{
				anims[i] = animInfo;
				i++;
			}
		}
	}

	// Token: 0x06002501 RID: 9473 RVA: 0x000D33BC File Offset: 0x000D15BC
	public void Render(List<SpriteSheetAnimator.AnimInfo> anim_infos, bool apply_rotation)
	{
		ListPool<Vector3, SpriteSheetAnimManager>.PooledList pooledList = ListPool<Vector3, SpriteSheetAnimManager>.Allocate();
		ListPool<Vector2, SpriteSheetAnimManager>.PooledList pooledList2 = ListPool<Vector2, SpriteSheetAnimManager>.Allocate();
		ListPool<Color32, SpriteSheetAnimManager>.PooledList pooledList3 = ListPool<Color32, SpriteSheetAnimManager>.Allocate();
		ListPool<int, SpriteSheetAnimManager>.PooledList pooledList4 = ListPool<int, SpriteSheetAnimManager>.Allocate();
		this.mesh.Clear();
		if (apply_rotation)
		{
			int count = anim_infos.Count;
			for (int i = 0; i < count; i++)
			{
				SpriteSheetAnimator.AnimInfo animInfo = anim_infos[i];
				Vector2 vector = animInfo.size * 0.5f;
				Vector3 vector2 = animInfo.rotation * -vector;
				Vector3 vector3 = animInfo.rotation * new Vector2(vector.x, -vector.y);
				Vector3 vector4 = animInfo.rotation * new Vector2(-vector.x, vector.y);
				Vector3 vector5 = animInfo.rotation * vector;
				pooledList.Add(animInfo.pos + vector2);
				pooledList.Add(animInfo.pos + vector3);
				pooledList.Add(animInfo.pos + vector5);
				pooledList.Add(animInfo.pos + vector4);
				Vector2 vector6;
				Vector2 vector7;
				Vector2 vector8;
				Vector2 vector9;
				this.GetUVs(animInfo.frame, out vector6, out vector7, out vector8, out vector9);
				pooledList2.Add(vector6);
				pooledList2.Add(vector7);
				pooledList2.Add(vector9);
				pooledList2.Add(vector8);
				pooledList3.Add(animInfo.colour);
				pooledList3.Add(animInfo.colour);
				pooledList3.Add(animInfo.colour);
				pooledList3.Add(animInfo.colour);
				int num = i * 4;
				pooledList4.Add(num);
				pooledList4.Add(num + 1);
				pooledList4.Add(num + 2);
				pooledList4.Add(num);
				pooledList4.Add(num + 2);
				pooledList4.Add(num + 3);
			}
		}
		else
		{
			int count2 = anim_infos.Count;
			for (int j = 0; j < count2; j++)
			{
				SpriteSheetAnimator.AnimInfo animInfo2 = anim_infos[j];
				Vector2 vector10 = animInfo2.size * 0.5f;
				Vector3 vector11 = -vector10;
				Vector3 vector12 = new Vector2(vector10.x, -vector10.y);
				Vector3 vector13 = new Vector2(-vector10.x, vector10.y);
				Vector3 vector14 = vector10;
				pooledList.Add(animInfo2.pos + vector11);
				pooledList.Add(animInfo2.pos + vector12);
				pooledList.Add(animInfo2.pos + vector14);
				pooledList.Add(animInfo2.pos + vector13);
				Vector2 vector15;
				Vector2 vector16;
				Vector2 vector17;
				Vector2 vector18;
				this.GetUVs(animInfo2.frame, out vector15, out vector16, out vector17, out vector18);
				pooledList2.Add(vector15);
				pooledList2.Add(vector16);
				pooledList2.Add(vector18);
				pooledList2.Add(vector17);
				pooledList3.Add(animInfo2.colour);
				pooledList3.Add(animInfo2.colour);
				pooledList3.Add(animInfo2.colour);
				pooledList3.Add(animInfo2.colour);
				int num2 = j * 4;
				pooledList4.Add(num2);
				pooledList4.Add(num2 + 1);
				pooledList4.Add(num2 + 2);
				pooledList4.Add(num2);
				pooledList4.Add(num2 + 2);
				pooledList4.Add(num2 + 3);
			}
		}
		this.mesh.SetVertices(pooledList);
		this.mesh.SetUVs(0, pooledList2);
		this.mesh.SetColors(pooledList3);
		this.mesh.SetTriangles(pooledList4, 0);
		Graphics.DrawMesh(this.mesh, Vector3.zero, Quaternion.identity, this.sheet.material, this.sheet.renderLayer, null, 0, this.materialProperties);
		pooledList4.Recycle();
		pooledList3.Recycle();
		pooledList2.Recycle();
		pooledList.Recycle();
	}

	// Token: 0x06002502 RID: 9474 RVA: 0x000D37A4 File Offset: 0x000D19A4
	public void Render()
	{
		this.Render(this.anims, false);
		this.Render(this.rotatedAnims, true);
	}

	// Token: 0x040015B9 RID: 5561
	private SpriteSheet sheet;

	// Token: 0x040015BA RID: 5562
	private Mesh mesh;

	// Token: 0x040015BB RID: 5563
	private MaterialPropertyBlock materialProperties;

	// Token: 0x040015BC RID: 5564
	private List<SpriteSheetAnimator.AnimInfo> anims = new List<SpriteSheetAnimator.AnimInfo>();

	// Token: 0x040015BD RID: 5565
	private List<SpriteSheetAnimator.AnimInfo> rotatedAnims = new List<SpriteSheetAnimator.AnimInfo>();

	// Token: 0x020014AB RID: 5291
	public struct AnimInfo
	{
		// Token: 0x04006D71 RID: 28017
		public int frame;

		// Token: 0x04006D72 RID: 28018
		public float elapsedTime;

		// Token: 0x04006D73 RID: 28019
		public Vector3 pos;

		// Token: 0x04006D74 RID: 28020
		public Quaternion rotation;

		// Token: 0x04006D75 RID: 28021
		public Vector2 size;

		// Token: 0x04006D76 RID: 28022
		public Color32 colour;
	}
}
