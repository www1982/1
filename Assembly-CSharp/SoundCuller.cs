using System;
using UnityEngine;

// Token: 0x02000557 RID: 1367
public struct SoundCuller
{
	// Token: 0x06001E2E RID: 7726 RVA: 0x000A446C File Offset: 0x000A266C
	public static bool IsAudibleWorld(Vector2 pos)
	{
		bool flag = false;
		int num = Grid.PosToCell(pos);
		if (Grid.IsValidCell(num) && (int)Grid.WorldIdx[num] == ClusterManager.Instance.activeWorldId)
		{
			flag = true;
		}
		return flag;
	}

	// Token: 0x06001E2F RID: 7727 RVA: 0x000A44A0 File Offset: 0x000A26A0
	public bool IsAudible(Vector2 pos)
	{
		return SoundCuller.IsAudibleWorld(pos) && this.min.LessEqual(pos) && pos.LessEqual(this.max);
	}

	// Token: 0x06001E30 RID: 7728 RVA: 0x000A44C8 File Offset: 0x000A26C8
	public bool IsAudibleNoCameraScaling(Vector2 pos, float falloff_distance_sq)
	{
		return (pos.x - this.cameraPos.x) * (pos.x - this.cameraPos.x) + (pos.y - this.cameraPos.y) * (pos.y - this.cameraPos.y) < falloff_distance_sq;
	}

	// Token: 0x06001E31 RID: 7729 RVA: 0x000A4523 File Offset: 0x000A2723
	public bool IsAudible(Vector2 pos, float falloff_distance_sq)
	{
		if (!SoundCuller.IsAudibleWorld(pos))
		{
			return false;
		}
		pos = this.GetVerticallyScaledPosition(pos, false);
		return this.IsAudibleNoCameraScaling(pos, falloff_distance_sq);
	}

	// Token: 0x06001E32 RID: 7730 RVA: 0x000A454B File Offset: 0x000A274B
	public bool IsAudible(Vector2 pos, HashedString sound_path)
	{
		return sound_path.IsValid && this.IsAudible(pos, KFMOD.GetSoundEventDescription(sound_path).falloffDistanceSq);
	}

	// Token: 0x06001E33 RID: 7731 RVA: 0x000A456C File Offset: 0x000A276C
	public Vector3 GetVerticallyScaledPosition(Vector3 pos, bool objectIsSelectedAndVisible = false)
	{
		float num = 1f;
		float num2;
		if (pos.y > this.max.y)
		{
			num2 = Mathf.Abs(pos.y - this.max.y);
		}
		else if (pos.y < this.min.y)
		{
			num2 = Mathf.Abs(pos.y - this.min.y);
			num = -1f;
		}
		else
		{
			num2 = 0f;
		}
		float extraYRange = TuningData<SoundCuller.Tuning>.Get().extraYRange;
		num2 = ((num2 < extraYRange) ? num2 : extraYRange);
		float num3 = num2 * num2 / (4f * this.zoomScaler);
		num3 *= num;
		Vector3 vector = new Vector3(pos.x, pos.y + num3, 0f);
		if (objectIsSelectedAndVisible)
		{
			vector.z = pos.z;
		}
		return vector;
	}

	// Token: 0x06001E34 RID: 7732 RVA: 0x000A4640 File Offset: 0x000A2840
	public static SoundCuller CreateCuller()
	{
		SoundCuller soundCuller = default(SoundCuller);
		Camera main = Camera.main;
		Vector3 vector = main.ViewportToWorldPoint(new Vector3(1f, 1f, Camera.main.transform.GetPosition().z));
		Vector3 vector2 = main.ViewportToWorldPoint(new Vector3(0f, 0f, Camera.main.transform.GetPosition().z));
		soundCuller.min = new Vector3(vector2.x, vector2.y, 0f);
		soundCuller.max = new Vector3(vector.x, vector.y, 0f);
		soundCuller.cameraPos = main.transform.GetPosition();
		Audio audio = Audio.Get();
		float num = CameraController.Instance.OrthographicSize / (audio.listenerReferenceZ - audio.listenerMinZ);
		if (num <= 0f)
		{
			num = 2f;
		}
		else
		{
			num = 1f;
		}
		soundCuller.zoomScaler = num;
		return soundCuller;
	}

	// Token: 0x04001195 RID: 4501
	private Vector2 min;

	// Token: 0x04001196 RID: 4502
	private Vector2 max;

	// Token: 0x04001197 RID: 4503
	private Vector2 cameraPos;

	// Token: 0x04001198 RID: 4504
	private float zoomScaler;

	// Token: 0x020013A1 RID: 5025
	public class Tuning : TuningData<SoundCuller.Tuning>
	{
		// Token: 0x04006A24 RID: 27172
		public float extraYRange;
	}
}
