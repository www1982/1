using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005FB RID: 1531
public class PrioritizableRenderer
{
	// Token: 0x17000192 RID: 402
	// (get) Token: 0x06002448 RID: 9288 RVA: 0x000CEA37 File Offset: 0x000CCC37
	// (set) Token: 0x06002449 RID: 9289 RVA: 0x000CEA3F File Offset: 0x000CCC3F
	public PrioritizeTool currentTool
	{
		get
		{
			return this.tool;
		}
		set
		{
			this.tool = value;
		}
	}

	// Token: 0x0600244A RID: 9290 RVA: 0x000CEA48 File Offset: 0x000CCC48
	public PrioritizableRenderer()
	{
		this.layer = LayerMask.NameToLayer("UI");
		Shader shader = Shader.Find("Klei/Prioritizable");
		Texture2D texture = Assets.GetTexture("priority_overlay_atlas");
		this.material = new Material(shader);
		this.material.SetTexture(Shader.PropertyToID("_MainTex"), texture);
		this.prioritizables = new List<Prioritizable>();
		this.mesh = new Mesh();
		this.mesh.name = "Prioritizables";
		this.mesh.MarkDynamic();
	}

	// Token: 0x0600244B RID: 9291 RVA: 0x000CEAD4 File Offset: 0x000CCCD4
	public void Cleanup()
	{
		this.material = null;
		this.vertices = null;
		this.uvs = null;
		this.prioritizables = null;
		this.triangles = null;
		global::UnityEngine.Object.DestroyImmediate(this.mesh);
		this.mesh = null;
	}

	// Token: 0x0600244C RID: 9292 RVA: 0x000CEB0C File Offset: 0x000CCD0C
	public void RenderEveryTick()
	{
		using (new KProfiler.Region("PrioritizableRenderer", null))
		{
			if (!(GameScreenManager.Instance == null))
			{
				if (!(SimDebugView.Instance == null) && !(SimDebugView.Instance.GetMode() != OverlayModes.Priorities.ID))
				{
					this.prioritizables.Clear();
					Vector2I vector2I;
					Vector2I vector2I2;
					Grid.GetVisibleExtents(out vector2I, out vector2I2);
					int num = vector2I2.y - vector2I.y;
					int num2 = vector2I2.x - vector2I.x;
					Extents extents = new Extents(vector2I.x, vector2I.y, num2, num);
					List<ScenePartitionerEntry> list = new List<ScenePartitionerEntry>();
					GameScenePartitioner.Instance.GatherEntries(extents, GameScenePartitioner.Instance.prioritizableObjects, list);
					foreach (ScenePartitionerEntry scenePartitionerEntry in list)
					{
						Prioritizable prioritizable = (Prioritizable)scenePartitionerEntry.obj;
						if (prioritizable != null && prioritizable.showIcon && prioritizable.IsPrioritizable() && this.tool.IsActiveLayer(this.tool.GetFilterLayerFromGameObject(prioritizable.gameObject)) && prioritizable.GetMyWorldId() == ClusterManager.Instance.activeWorldId)
						{
							this.prioritizables.Add(prioritizable);
						}
					}
					if (this.prioritizableCount != this.prioritizables.Count)
					{
						this.prioritizableCount = this.prioritizables.Count;
						this.vertices = new Vector3[4 * this.prioritizableCount];
						this.uvs = new Vector2[4 * this.prioritizableCount];
						this.triangles = new int[6 * this.prioritizableCount];
					}
					if (this.prioritizableCount != 0)
					{
						for (int i = 0; i < this.prioritizables.Count; i++)
						{
							Prioritizable prioritizable2 = this.prioritizables[i];
							Vector3 vector = Vector3.zero;
							KAnimControllerBase component = prioritizable2.GetComponent<KAnimControllerBase>();
							if (component != null)
							{
								vector = component.GetWorldPivot();
							}
							else
							{
								vector = prioritizable2.transform.GetPosition();
							}
							vector.x += prioritizable2.iconOffset.x;
							vector.y += prioritizable2.iconOffset.y;
							Vector2 vector2 = new Vector2(0.2f, 0.3f) * prioritizable2.iconScale;
							float num3 = -5f;
							int num4 = 4 * i;
							this.vertices[num4] = new Vector3(vector.x - vector2.x, vector.y - vector2.y, num3);
							this.vertices[1 + num4] = new Vector3(vector.x - vector2.x, vector.y + vector2.y, num3);
							this.vertices[2 + num4] = new Vector3(vector.x + vector2.x, vector.y - vector2.y, num3);
							this.vertices[3 + num4] = new Vector3(vector.x + vector2.x, vector.y + vector2.y, num3);
							float num5 = 0.1f;
							PrioritySetting masterPriority = prioritizable2.GetMasterPriority();
							float num6 = -1f;
							if (masterPriority.priority_class >= PriorityScreen.PriorityClass.high)
							{
								num6 += 9f;
							}
							if (masterPriority.priority_class >= PriorityScreen.PriorityClass.topPriority)
							{
								num6 += 0f;
							}
							num6 += (float)masterPriority.priority_value;
							float num7 = num5 * num6;
							float num8 = 0f;
							float num9 = num5;
							float num10 = 1f;
							this.uvs[num4] = new Vector2(num7, num8);
							this.uvs[1 + num4] = new Vector2(num7, num8 + num10);
							this.uvs[2 + num4] = new Vector2(num7 + num9, num8);
							this.uvs[3 + num4] = new Vector2(num7 + num9, num8 + num10);
							int num11 = 6 * i;
							this.triangles[num11] = num4;
							this.triangles[1 + num11] = num4 + 1;
							this.triangles[2 + num11] = num4 + 2;
							this.triangles[3 + num11] = num4 + 2;
							this.triangles[4 + num11] = num4 + 1;
							this.triangles[5 + num11] = num4 + 3;
						}
						this.mesh.Clear();
						this.mesh.vertices = this.vertices;
						this.mesh.uv = this.uvs;
						this.mesh.SetTriangles(this.triangles, 0);
						this.mesh.RecalculateBounds();
						Graphics.DrawMesh(this.mesh, Vector3.zero, Quaternion.identity, this.material, this.layer, GameScreenManager.Instance.worldSpaceCanvas.GetComponent<Canvas>().worldCamera, 0, null, false, false);
					}
				}
			}
		}
	}

	// Token: 0x0400151C RID: 5404
	private Mesh mesh;

	// Token: 0x0400151D RID: 5405
	private int layer;

	// Token: 0x0400151E RID: 5406
	private Material material;

	// Token: 0x0400151F RID: 5407
	private int prioritizableCount;

	// Token: 0x04001520 RID: 5408
	private Vector3[] vertices;

	// Token: 0x04001521 RID: 5409
	private Vector2[] uvs;

	// Token: 0x04001522 RID: 5410
	private int[] triangles;

	// Token: 0x04001523 RID: 5411
	private List<Prioritizable> prioritizables;

	// Token: 0x04001524 RID: 5412
	private PrioritizeTool tool;
}
