using System;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

// Token: 0x02000C39 RID: 3129
public class SolidConduitFlowVisualizer
{
	// Token: 0x06005F30 RID: 24368 RVA: 0x0022F92C File Offset: 0x0022DB2C
	public SolidConduitFlowVisualizer(SolidConduitFlow flow_manager, Game.ConduitVisInfo vis_info, EventReference overlay_sound, SolidConduitFlowVisualizer.Tuning tuning)
	{
		this.flowManager = flow_manager;
		this.visInfo = vis_info;
		this.overlaySound = overlay_sound;
		this.tuning = tuning;
		this.movingBallMesh = new SolidConduitFlowVisualizer.ConduitFlowMesh();
		this.staticBallMesh = new SolidConduitFlowVisualizer.ConduitFlowMesh();
	}

	// Token: 0x06005F31 RID: 24369 RVA: 0x0022F9A8 File Offset: 0x0022DBA8
	public void FreeResources()
	{
		this.movingBallMesh.Cleanup();
		this.staticBallMesh.Cleanup();
	}

	// Token: 0x06005F32 RID: 24370 RVA: 0x0022F9C0 File Offset: 0x0022DBC0
	private float CalculateMassScale(float mass)
	{
		float num = (mass - this.visInfo.overlayMassScaleRange.x) / (this.visInfo.overlayMassScaleRange.y - this.visInfo.overlayMassScaleRange.x);
		return Mathf.Lerp(this.visInfo.overlayMassScaleValues.x, this.visInfo.overlayMassScaleValues.y, num);
	}

	// Token: 0x06005F33 RID: 24371 RVA: 0x0022FA28 File Offset: 0x0022DC28
	private Color32 GetContentsColor(Element element, Color32 default_color)
	{
		if (element != null)
		{
			Color color = element.substance.conduitColour;
			color.a = 128f;
			return color;
		}
		return default_color;
	}

	// Token: 0x06005F34 RID: 24372 RVA: 0x0022FA60 File Offset: 0x0022DC60
	private Color32 GetBackgroundColor(float insulation_lerp)
	{
		if (this.showContents)
		{
			return Color32.Lerp(GlobalAssets.Instance.colorSet.GetColorByName(this.visInfo.overlayTintName), GlobalAssets.Instance.colorSet.GetColorByName(this.visInfo.overlayInsulatedTintName), insulation_lerp);
		}
		return Color32.Lerp(this.visInfo.tint, this.visInfo.insulatedTint, insulation_lerp);
	}

	// Token: 0x06005F35 RID: 24373 RVA: 0x0022FACC File Offset: 0x0022DCCC
	public void Render(float z, int render_layer, float lerp_percent, bool trigger_audio = false)
	{
		GridArea visibleArea = GridVisibleArea.GetVisibleArea();
		Vector2I vector2I = new Vector2I(Mathf.Max(0, visibleArea.Min.x - 1), Mathf.Max(0, visibleArea.Min.y - 1));
		Vector2I vector2I2 = new Vector2I(Mathf.Min(Grid.WidthInCells - 1, visibleArea.Max.x + 1), Mathf.Min(Grid.HeightInCells - 1, visibleArea.Max.y + 1));
		this.animTime += (double)Time.deltaTime;
		if (trigger_audio)
		{
			if (this.audioInfo == null)
			{
				this.audioInfo = new List<SolidConduitFlowVisualizer.AudioInfo>();
			}
			for (int i = 0; i < this.audioInfo.Count; i++)
			{
				SolidConduitFlowVisualizer.AudioInfo audioInfo = this.audioInfo[i];
				audioInfo.distance = float.PositiveInfinity;
				audioInfo.position = Vector3.zero;
				audioInfo.blobCount = (audioInfo.blobCount + 1) % SolidConduitFlowVisualizer.BLOB_SOUND_COUNT;
				this.audioInfo[i] = audioInfo;
			}
		}
		Vector3 position = CameraController.Instance.transform.GetPosition();
		Element element = null;
		if (this.tuning.renderMesh)
		{
			float num = 0f;
			if (this.showContents)
			{
				num = 1f;
			}
			float num2 = (float)((int)(this.animTime / (1.0 / (double)this.tuning.framesPerSecond)) % (int)this.tuning.spriteCount) * (1f / this.tuning.spriteCount);
			this.movingBallMesh.Begin();
			this.movingBallMesh.SetTexture("_BackgroundTex", this.tuning.backgroundTexture);
			this.movingBallMesh.SetTexture("_ForegroundTex", this.tuning.foregroundTexture);
			this.movingBallMesh.SetVector("_SpriteSettings", new Vector4(1f / this.tuning.spriteCount, 1f, num, num2));
			this.movingBallMesh.SetVector("_Highlight", new Vector4((float)this.highlightColour.r / 255f, (float)this.highlightColour.g / 255f, (float)this.highlightColour.b / 255f, 0f));
			this.staticBallMesh.Begin();
			this.staticBallMesh.SetTexture("_BackgroundTex", this.tuning.backgroundTexture);
			this.staticBallMesh.SetTexture("_ForegroundTex", this.tuning.foregroundTexture);
			this.staticBallMesh.SetVector("_SpriteSettings", new Vector4(1f / this.tuning.spriteCount, 1f, num, 0f));
			this.staticBallMesh.SetVector("_Highlight", new Vector4((float)this.highlightColour.r / 255f, (float)this.highlightColour.g / 255f, (float)this.highlightColour.b / 255f, 0f));
			for (int j = 0; j < this.flowManager.GetSOAInfo().NumEntries; j++)
			{
				Vector2I vector2I3 = Grid.CellToXY(this.flowManager.GetSOAInfo().GetCell(j));
				if (!(vector2I3 < vector2I) && !(vector2I3 > vector2I2))
				{
					SolidConduitFlow.Conduit conduit = this.flowManager.GetSOAInfo().GetConduit(j);
					SolidConduitFlow.ConduitFlowInfo lastFlowInfo = conduit.GetLastFlowInfo(this.flowManager);
					SolidConduitFlow.ConduitContents initialContents = conduit.GetInitialContents(this.flowManager);
					bool flag = lastFlowInfo.direction > SolidConduitFlow.FlowDirection.None;
					if (flag)
					{
						int cell = conduit.GetCell(this.flowManager);
						int cellFromDirection = SolidConduitFlow.GetCellFromDirection(cell, lastFlowInfo.direction);
						Vector2I vector2I4 = Grid.CellToXY(cell);
						Vector2I vector2I5 = Grid.CellToXY(cellFromDirection);
						Vector2 vector = vector2I4;
						if (cell != -1)
						{
							vector = Vector2.Lerp(new Vector2((float)vector2I4.x, (float)vector2I4.y), new Vector2((float)vector2I5.x, (float)vector2I5.y), lerp_percent);
						}
						float num3 = (this.insulatedCells.Contains(cell) ? 1f : 0f);
						float num4 = (this.insulatedCells.Contains(cellFromDirection) ? 1f : 0f);
						float num5 = Mathf.Lerp(num3, num4, lerp_percent);
						Color color = this.GetBackgroundColor(num5);
						Vector2I vector2I6 = new Vector2I(0, 0);
						Vector2I vector2I7 = new Vector2I(0, 1);
						Vector2I vector2I8 = new Vector2I(1, 0);
						Vector2I vector2I9 = new Vector2I(1, 1);
						float num6 = 0f;
						if (this.showContents)
						{
							if (flag != initialContents.pickupableHandle.IsValid())
							{
								this.movingBallMesh.AddQuad(vector, color, this.tuning.size, 0f, 0f, vector2I6, vector2I7, vector2I8, vector2I9);
							}
						}
						else
						{
							element = null;
							if (Grid.PosToCell(new Vector3(vector.x + SolidConduitFlowVisualizer.GRID_OFFSET.x, vector.y + SolidConduitFlowVisualizer.GRID_OFFSET.y, 0f)) == this.highlightedCell)
							{
								num6 = 1f;
							}
						}
						Color32 contentsColor = this.GetContentsColor(element, color);
						float num7 = 1f;
						this.movingBallMesh.AddQuad(vector, contentsColor, this.tuning.size * num7, 1f, num6, vector2I6, vector2I7, vector2I8, vector2I9);
						if (trigger_audio)
						{
							this.AddAudioSource(conduit, position);
						}
					}
					if (initialContents.pickupableHandle.IsValid() && !flag)
					{
						int cell2 = conduit.GetCell(this.flowManager);
						Vector2 vector2 = Grid.CellToXY(cell2);
						float num8 = (this.insulatedCells.Contains(cell2) ? 1f : 0f);
						Vector2I vector2I10 = new Vector2I(0, 0);
						Vector2I vector2I11 = new Vector2I(0, 1);
						Vector2I vector2I12 = new Vector2I(1, 0);
						Vector2I vector2I13 = new Vector2I(1, 1);
						float num9 = 0f;
						Color color2 = this.GetBackgroundColor(num8);
						float num10 = 1f;
						if (this.showContents)
						{
							this.staticBallMesh.AddQuad(vector2, color2, this.tuning.size * num10, 0f, 0f, vector2I10, vector2I11, vector2I12, vector2I13);
						}
						else
						{
							element = null;
							if (cell2 == this.highlightedCell)
							{
								num9 = 1f;
							}
						}
						Color32 contentsColor2 = this.GetContentsColor(element, color2);
						this.staticBallMesh.AddQuad(vector2, contentsColor2, this.tuning.size * num10, 1f, num9, vector2I10, vector2I11, vector2I12, vector2I13);
					}
				}
			}
			this.movingBallMesh.End(z, this.layer);
			this.staticBallMesh.End(z, this.layer);
		}
		if (trigger_audio)
		{
			this.TriggerAudio();
		}
	}

	// Token: 0x06005F36 RID: 24374 RVA: 0x00230190 File Offset: 0x0022E390
	public void ColourizePipeContents(bool show_contents, bool move_to_overlay_layer)
	{
		this.showContents = show_contents;
		this.layer = ((show_contents && move_to_overlay_layer) ? LayerMask.NameToLayer("MaskedOverlay") : 0);
	}

	// Token: 0x06005F37 RID: 24375 RVA: 0x002301B4 File Offset: 0x0022E3B4
	private void AddAudioSource(SolidConduitFlow.Conduit conduit, Vector3 camera_pos)
	{
		using (new KProfiler.Region("AddAudioSource", null))
		{
			UtilityNetwork network = this.flowManager.GetNetwork(conduit);
			if (network != null)
			{
				Vector3 vector = Grid.CellToPosCCC(conduit.GetCell(this.flowManager), Grid.SceneLayer.Building);
				float num = Vector3.SqrMagnitude(vector - camera_pos);
				bool flag = false;
				for (int i = 0; i < this.audioInfo.Count; i++)
				{
					SolidConduitFlowVisualizer.AudioInfo audioInfo = this.audioInfo[i];
					if (audioInfo.networkID == network.id)
					{
						if (num < audioInfo.distance)
						{
							audioInfo.distance = num;
							audioInfo.position = vector;
							this.audioInfo[i] = audioInfo;
						}
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					SolidConduitFlowVisualizer.AudioInfo audioInfo2 = default(SolidConduitFlowVisualizer.AudioInfo);
					audioInfo2.networkID = network.id;
					audioInfo2.position = vector;
					audioInfo2.distance = num;
					audioInfo2.blobCount = 0;
					this.audioInfo.Add(audioInfo2);
				}
			}
		}
	}

	// Token: 0x06005F38 RID: 24376 RVA: 0x002302CC File Offset: 0x0022E4CC
	private void TriggerAudio()
	{
		if (SpeedControlScreen.Instance.IsPaused)
		{
			return;
		}
		CameraController instance = CameraController.Instance;
		int num = 0;
		List<SolidConduitFlowVisualizer.AudioInfo> list = new List<SolidConduitFlowVisualizer.AudioInfo>();
		for (int i = 0; i < this.audioInfo.Count; i++)
		{
			if (instance.IsVisiblePos(this.audioInfo[i].position))
			{
				list.Add(this.audioInfo[i]);
				num++;
			}
		}
		for (int j = 0; j < list.Count; j++)
		{
			SolidConduitFlowVisualizer.AudioInfo audioInfo = list[j];
			if (audioInfo.distance != float.PositiveInfinity)
			{
				Vector3 position = audioInfo.position;
				position.z = 0f;
				EventInstance eventInstance = SoundEvent.BeginOneShot(this.overlaySound, position, 1f, false);
				eventInstance.setParameterByName("blobCount", (float)audioInfo.blobCount, false);
				eventInstance.setParameterByName("networkCount", (float)num, false);
				SoundEvent.EndOneShot(eventInstance);
			}
		}
	}

	// Token: 0x06005F39 RID: 24377 RVA: 0x002303BE File Offset: 0x0022E5BE
	public void SetInsulated(int cell, bool insulated)
	{
		if (insulated)
		{
			this.insulatedCells.Add(cell);
			return;
		}
		this.insulatedCells.Remove(cell);
	}

	// Token: 0x06005F3A RID: 24378 RVA: 0x002303DE File Offset: 0x0022E5DE
	public void SetHighlightedCell(int cell)
	{
		this.highlightedCell = cell;
	}

	// Token: 0x04003F70 RID: 16240
	private SolidConduitFlow flowManager;

	// Token: 0x04003F71 RID: 16241
	private EventReference overlaySound;

	// Token: 0x04003F72 RID: 16242
	private bool showContents;

	// Token: 0x04003F73 RID: 16243
	private double animTime;

	// Token: 0x04003F74 RID: 16244
	private int layer;

	// Token: 0x04003F75 RID: 16245
	private static Vector2 GRID_OFFSET = new Vector2(0.5f, 0.5f);

	// Token: 0x04003F76 RID: 16246
	private static int BLOB_SOUND_COUNT = 7;

	// Token: 0x04003F77 RID: 16247
	private List<SolidConduitFlowVisualizer.AudioInfo> audioInfo;

	// Token: 0x04003F78 RID: 16248
	private HashSet<int> insulatedCells = new HashSet<int>();

	// Token: 0x04003F79 RID: 16249
	private Game.ConduitVisInfo visInfo;

	// Token: 0x04003F7A RID: 16250
	private SolidConduitFlowVisualizer.ConduitFlowMesh movingBallMesh;

	// Token: 0x04003F7B RID: 16251
	private SolidConduitFlowVisualizer.ConduitFlowMesh staticBallMesh;

	// Token: 0x04003F7C RID: 16252
	private int highlightedCell = -1;

	// Token: 0x04003F7D RID: 16253
	private Color32 highlightColour = new Color(0.2f, 0.2f, 0.2f, 0.2f);

	// Token: 0x04003F7E RID: 16254
	private SolidConduitFlowVisualizer.Tuning tuning;

	// Token: 0x02001DA3 RID: 7587
	[Serializable]
	public class Tuning
	{
		// Token: 0x04008A3F RID: 35391
		public bool renderMesh;

		// Token: 0x04008A40 RID: 35392
		public float size;

		// Token: 0x04008A41 RID: 35393
		public float spriteCount;

		// Token: 0x04008A42 RID: 35394
		public float framesPerSecond;

		// Token: 0x04008A43 RID: 35395
		public Texture2D backgroundTexture;

		// Token: 0x04008A44 RID: 35396
		public Texture2D foregroundTexture;
	}

	// Token: 0x02001DA4 RID: 7588
	private class ConduitFlowMesh
	{
		// Token: 0x0600AF41 RID: 44865 RVA: 0x003CEE80 File Offset: 0x003CD080
		public ConduitFlowMesh()
		{
			this.mesh = new Mesh();
			this.mesh.name = "ConduitMesh";
			this.material = new Material(Shader.Find("Klei/ConduitBall"));
		}

		// Token: 0x0600AF42 RID: 44866 RVA: 0x003CEEF0 File Offset: 0x003CD0F0
		public void AddQuad(Vector2 pos, Color32 color, float size, float is_foreground, float highlight, Vector2I uvbl, Vector2I uvtl, Vector2I uvbr, Vector2I uvtr)
		{
			float num = size * 0.5f;
			this.positions.Add(new Vector3(pos.x - num, pos.y - num, 0f));
			this.positions.Add(new Vector3(pos.x - num, pos.y + num, 0f));
			this.positions.Add(new Vector3(pos.x + num, pos.y - num, 0f));
			this.positions.Add(new Vector3(pos.x + num, pos.y + num, 0f));
			this.uvs.Add(new Vector4((float)uvbl.x, (float)uvbl.y, is_foreground, highlight));
			this.uvs.Add(new Vector4((float)uvtl.x, (float)uvtl.y, is_foreground, highlight));
			this.uvs.Add(new Vector4((float)uvbr.x, (float)uvbr.y, is_foreground, highlight));
			this.uvs.Add(new Vector4((float)uvtr.x, (float)uvtr.y, is_foreground, highlight));
			this.colors.Add(color);
			this.colors.Add(color);
			this.colors.Add(color);
			this.colors.Add(color);
			this.triangles.Add(this.quadIndex * 4);
			this.triangles.Add(this.quadIndex * 4 + 1);
			this.triangles.Add(this.quadIndex * 4 + 2);
			this.triangles.Add(this.quadIndex * 4 + 2);
			this.triangles.Add(this.quadIndex * 4 + 1);
			this.triangles.Add(this.quadIndex * 4 + 3);
			this.quadIndex++;
		}

		// Token: 0x0600AF43 RID: 44867 RVA: 0x003CF0E3 File Offset: 0x003CD2E3
		public void SetTexture(string id, Texture2D texture)
		{
			this.material.SetTexture(id, texture);
		}

		// Token: 0x0600AF44 RID: 44868 RVA: 0x003CF0F2 File Offset: 0x003CD2F2
		public void SetVector(string id, Vector4 data)
		{
			this.material.SetVector(id, data);
		}

		// Token: 0x0600AF45 RID: 44869 RVA: 0x003CF101 File Offset: 0x003CD301
		public void Begin()
		{
			this.positions.Clear();
			this.uvs.Clear();
			this.triangles.Clear();
			this.colors.Clear();
			this.quadIndex = 0;
		}

		// Token: 0x0600AF46 RID: 44870 RVA: 0x003CF138 File Offset: 0x003CD338
		public void End(float z, int layer)
		{
			this.mesh.Clear();
			this.mesh.SetVertices(this.positions);
			this.mesh.SetUVs(0, this.uvs);
			this.mesh.SetColors(this.colors);
			this.mesh.SetTriangles(this.triangles, 0, false);
			Graphics.DrawMesh(this.mesh, new Vector3(SolidConduitFlowVisualizer.GRID_OFFSET.x, SolidConduitFlowVisualizer.GRID_OFFSET.y, z - 0.1f), Quaternion.identity, this.material, layer);
		}

		// Token: 0x0600AF47 RID: 44871 RVA: 0x003CF1CE File Offset: 0x003CD3CE
		public void Cleanup()
		{
			global::UnityEngine.Object.Destroy(this.mesh);
			this.mesh = null;
			global::UnityEngine.Object.Destroy(this.material);
			this.material = null;
		}

		// Token: 0x04008A45 RID: 35397
		private Mesh mesh;

		// Token: 0x04008A46 RID: 35398
		private Material material;

		// Token: 0x04008A47 RID: 35399
		private List<Vector3> positions = new List<Vector3>();

		// Token: 0x04008A48 RID: 35400
		private List<Vector4> uvs = new List<Vector4>();

		// Token: 0x04008A49 RID: 35401
		private List<int> triangles = new List<int>();

		// Token: 0x04008A4A RID: 35402
		private List<Color32> colors = new List<Color32>();

		// Token: 0x04008A4B RID: 35403
		private int quadIndex;
	}

	// Token: 0x02001DA5 RID: 7589
	private struct AudioInfo
	{
		// Token: 0x04008A4C RID: 35404
		public int networkID;

		// Token: 0x04008A4D RID: 35405
		public int blobCount;

		// Token: 0x04008A4E RID: 35406
		public float distance;

		// Token: 0x04008A4F RID: 35407
		public Vector3 position;
	}
}
