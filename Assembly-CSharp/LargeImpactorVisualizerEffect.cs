using System;
using Unity.Collections;
using UnityEngine;

// Token: 0x02000BF4 RID: 3060
public class LargeImpactorVisualizerEffect : KMonoBehaviour
{
	// Token: 0x06005C42 RID: 23618 RVA: 0x00219900 File Offset: 0x00217B00
	protected override void OnSpawn()
	{
		GameplayEventInstance gameplayEventInstance = GameplayEventManager.Instance.GetGameplayEventInstance(Db.Get().GameplayEvents.LargeImpactor.Id, -1);
		this.material = new Material(Shader.Find("Klei/PostFX/LargeImpactorVisualizerShader"));
		if (!this.SetLargeImpactObjectFromEventInstance(gameplayEventInstance))
		{
			GameplayEventManager.Instance.Subscribe(1491341646, new Action<object>(this.SetupOnGameplayEventStart));
		}
		this.icon = Assets.GetSprite("iconWarning");
	}

	// Token: 0x06005C43 RID: 23619 RVA: 0x00219984 File Offset: 0x00217B84
	private bool SetLargeImpactObjectFromEventInstance(GameplayEventInstance eventInstance)
	{
		if (eventInstance != null)
		{
			LargeImpactorEvent.StatesInstance statesInstance = (LargeImpactorEvent.StatesInstance)eventInstance.smi;
			this.rangeVisualizer = statesInstance.impactorInstance.GetComponent<LargeImpactorVisualizer>();
			LargeImpactorNotificationMonitor.Instance smi = statesInstance.impactorInstance.GetSMI<LargeImpactorNotificationMonitor.Instance>();
			if (this.rangeVisualizer != null)
			{
				this.material.SetFloat("_EntryStartTime", -1f);
				this.material.SetFloat("_ZoneWasRevealed", (float)(smi.HasRevealSequencePlayed ? 1 : 0));
			}
			statesInstance.impactorInstance.Subscribe(-467702038, new Action<object>(this.OnAnySequenceRelatedToImpactorCompleted));
			return true;
		}
		return false;
	}

	// Token: 0x06005C44 RID: 23620 RVA: 0x00219A20 File Offset: 0x00217C20
	private void OnAnySequenceRelatedToImpactorCompleted(object o)
	{
		this.material.SetFloat("_ZoneWasRevealed", 1f);
	}

	// Token: 0x06005C45 RID: 23621 RVA: 0x00219A38 File Offset: 0x00217C38
	private void SetupOnGameplayEventStart(object data)
	{
		GameplayEventInstance gameplayEventInstance = (GameplayEventInstance)data;
		if (gameplayEventInstance.eventID == Db.Get().GameplayEvents.LargeImpactor.Id)
		{
			this.SetLargeImpactObjectFromEventInstance(gameplayEventInstance);
		}
		GameplayEventManager.Instance.Unsubscribe(1491341646, new Action<object>(this.SetupOnGameplayEventStart));
	}

	// Token: 0x06005C46 RID: 23622 RVA: 0x00219A98 File Offset: 0x00217C98
	private void OnPostRender()
	{
		if (this.rangeVisualizer == null)
		{
			return;
		}
		if (!this.rangeVisualizer.Active)
		{
			return;
		}
		if (this.rangeVisualizer.Folded && Time.unscaledTime - this.rangeVisualizer.LastTimeSetToFolded > this.rangeVisualizer.FoldEffectDuration + 1f)
		{
			return;
		}
		Vector2I vector2I = Grid.PosToXY(this.rangeVisualizer.transform.position);
		bool flag = false;
		if (this.OcclusionTex == null || this.OcclusionTex.width != this.rangeVisualizer.TexSize.X || this.OcclusionTex.height != this.rangeVisualizer.TexSize.Y)
		{
			this.OcclusionTex = new Texture2D(this.rangeVisualizer.TexSize.X, this.rangeVisualizer.TexSize.Y, TextureFormat.Alpha8, false);
			this.OcclusionTex.filterMode = FilterMode.Point;
			this.OcclusionTex.wrapMode = TextureWrapMode.Clamp;
			flag = true;
		}
		Vector2I vector2I2;
		Vector2I vector2I3;
		this.FindWorldBounds(out vector2I2, out vector2I3);
		Vector2I rangeMin = this.rangeVisualizer.RangeMin;
		Vector2I rangeMax = this.rangeVisualizer.RangeMax;
		Vector2I originOffset = this.rangeVisualizer.OriginOffset;
		Vector2I vector2I4 = vector2I + originOffset;
		if (flag)
		{
			int width = this.OcclusionTex.width;
			NativeArray<byte> pixelData = this.OcclusionTex.GetPixelData<byte>(0);
			for (int i = 0; i <= rangeMax.y - rangeMin.y; i++)
			{
				int num = vector2I4.y + rangeMin.y + i;
				for (int j = 0; j <= rangeMax.x - rangeMin.x; j++)
				{
					int num2 = vector2I4.x + rangeMin.x + j;
					int num3 = Grid.XYToCell(num2, num);
					bool flag2 = num2 > vector2I2.x && num2 < vector2I3.x && num > vector2I2.y && num < vector2I3.y && this.rangeVisualizer.BlockingCb(num3);
					pixelData[i * width + j] = (flag2 ? 0 : byte.MaxValue);
				}
			}
		}
		this.OcclusionTex.Apply(false, false);
		Vector2I vector2I5 = rangeMin + vector2I4;
		Vector2I vector2I6 = rangeMax + vector2I4;
		if (this.myCamera == null)
		{
			this.myCamera = base.GetComponent<Camera>();
			if (this.myCamera == null)
			{
				return;
			}
		}
		Ray ray = this.myCamera.ViewportPointToRay(Vector3.zero);
		float num4 = Mathf.Abs(ray.origin.z / ray.direction.z);
		Vector3 vector = ray.GetPoint(num4);
		Vector4 vector2;
		vector2.x = vector.x;
		vector2.y = vector.y;
		ray = this.myCamera.ViewportPointToRay(Vector3.one);
		num4 = Mathf.Abs(ray.origin.z / ray.direction.z);
		vector = ray.GetPoint(num4);
		vector2.z = vector.x - vector2.x;
		vector2.w = vector.y - vector2.y;
		this.material.SetVector("_UVOffsetScale", vector2);
		Vector4 vector3;
		vector3.x = (float)vector2I5.x;
		vector3.y = (float)vector2I5.y;
		vector3.z = (float)(vector2I6.x + 1);
		vector3.w = (float)(vector2I6.y + 1);
		this.material.SetVector("_RangeParams", vector3);
		this.material.SetColor("_HighlightColor", this.highlightColor);
		this.material.SetTexture("_Icon", this.icon.texture);
		Vector4 vector4;
		vector4.x = 1f / (float)this.OcclusionTex.width;
		vector4.y = 1f / (float)this.OcclusionTex.height;
		vector4.z = 0f;
		vector4.w = 0f;
		this.material.SetVector("_OcclusionParams", vector4);
		this.material.SetTexture("_OcclusionTex", this.OcclusionTex);
		Vector4 vector5;
		vector5.x = (float)Grid.WidthInCells;
		vector5.y = (float)Grid.HeightInCells;
		vector5.z = 1f / (float)Grid.WidthInCells;
		vector5.w = 1f / (float)Grid.HeightInCells;
		this.material.SetVector("_WorldParams", vector5);
		if (this.rangeVisualizer.ShouldResetEntryEffect)
		{
			this.material.SetFloat("_EntryStartTime", Time.unscaledTime);
			this.rangeVisualizer.SetShouldResetEntryEffect(false);
		}
		this.material.SetFloat("_EntryEffectDuration", this.rangeVisualizer.EntryEffectDuration);
		this.material.SetFloat("_FoldDuration", this.rangeVisualizer.FoldEffectDuration);
		this.material.SetFloat("_UnscaledTime", Time.unscaledTime);
		this.material.SetVector("_UIToggleScreenPosition", this.rangeVisualizer.ScreenSpaceNotificationTogglePosition);
		this.material.SetFloat("_LastTimeSetToFold", this.rangeVisualizer.LastTimeSetToFolded);
		GL.PushMatrix();
		this.material.SetPass(0);
		GL.LoadOrtho();
		GL.Begin(5);
		GL.Color(Color.white);
		GL.Vertex3(0f, 0f, 0f);
		GL.Vertex3(0f, 1f, 0f);
		GL.Vertex3(1f, 0f, 0f);
		GL.Vertex3(1f, 1f, 0f);
		GL.End();
		GL.PopMatrix();
	}

	// Token: 0x06005C47 RID: 23623 RVA: 0x0021A060 File Offset: 0x00218260
	private void FindWorldBounds(out Vector2I world_min, out Vector2I world_max)
	{
		if (ClusterManager.Instance != null)
		{
			WorldContainer activeWorld = ClusterManager.Instance.activeWorld;
			world_min = activeWorld.WorldOffset;
			world_max = activeWorld.WorldOffset + activeWorld.WorldSize;
			return;
		}
		world_min.x = 0;
		world_min.y = 0;
		world_max.x = Grid.WidthInCells;
		world_max.y = Grid.HeightInCells;
	}

	// Token: 0x04003D19 RID: 15641
	private Material material;

	// Token: 0x04003D1A RID: 15642
	private Camera myCamera;

	// Token: 0x04003D1B RID: 15643
	public Color highlightColor = new Color(1f, 0.7f, 0.3f, 1f);

	// Token: 0x04003D1C RID: 15644
	private Texture2D OcclusionTex;

	// Token: 0x04003D1D RID: 15645
	private LargeImpactorVisualizer rangeVisualizer;

	// Token: 0x04003D1E RID: 15646
	private Sprite icon;
}
