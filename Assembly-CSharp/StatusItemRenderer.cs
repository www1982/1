using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200061B RID: 1563
public class StatusItemRenderer
{
	// Token: 0x170001A1 RID: 417
	// (get) Token: 0x0600254F RID: 9551 RVA: 0x000D524C File Offset: 0x000D344C
	// (set) Token: 0x06002550 RID: 9552 RVA: 0x000D5254 File Offset: 0x000D3454
	public int layer { get; private set; }

	// Token: 0x170001A2 RID: 418
	// (get) Token: 0x06002551 RID: 9553 RVA: 0x000D525D File Offset: 0x000D345D
	// (set) Token: 0x06002552 RID: 9554 RVA: 0x000D5265 File Offset: 0x000D3465
	public int selectedHandle { get; private set; }

	// Token: 0x170001A3 RID: 419
	// (get) Token: 0x06002553 RID: 9555 RVA: 0x000D526E File Offset: 0x000D346E
	// (set) Token: 0x06002554 RID: 9556 RVA: 0x000D5276 File Offset: 0x000D3476
	public int highlightHandle { get; private set; }

	// Token: 0x170001A4 RID: 420
	// (get) Token: 0x06002555 RID: 9557 RVA: 0x000D527F File Offset: 0x000D347F
	// (set) Token: 0x06002556 RID: 9558 RVA: 0x000D5287 File Offset: 0x000D3487
	public Color32 backgroundColor { get; private set; }

	// Token: 0x170001A5 RID: 421
	// (get) Token: 0x06002557 RID: 9559 RVA: 0x000D5290 File Offset: 0x000D3490
	// (set) Token: 0x06002558 RID: 9560 RVA: 0x000D5298 File Offset: 0x000D3498
	public Color32 selectedColor { get; private set; }

	// Token: 0x170001A6 RID: 422
	// (get) Token: 0x06002559 RID: 9561 RVA: 0x000D52A1 File Offset: 0x000D34A1
	// (set) Token: 0x0600255A RID: 9562 RVA: 0x000D52A9 File Offset: 0x000D34A9
	public Color32 neutralColor { get; private set; }

	// Token: 0x170001A7 RID: 423
	// (get) Token: 0x0600255B RID: 9563 RVA: 0x000D52B2 File Offset: 0x000D34B2
	// (set) Token: 0x0600255C RID: 9564 RVA: 0x000D52BA File Offset: 0x000D34BA
	public Sprite arrowSprite { get; private set; }

	// Token: 0x170001A8 RID: 424
	// (get) Token: 0x0600255D RID: 9565 RVA: 0x000D52C3 File Offset: 0x000D34C3
	// (set) Token: 0x0600255E RID: 9566 RVA: 0x000D52CB File Offset: 0x000D34CB
	public Sprite backgroundSprite { get; private set; }

	// Token: 0x170001A9 RID: 425
	// (get) Token: 0x0600255F RID: 9567 RVA: 0x000D52D4 File Offset: 0x000D34D4
	// (set) Token: 0x06002560 RID: 9568 RVA: 0x000D52DC File Offset: 0x000D34DC
	public float scale { get; private set; }

	// Token: 0x06002561 RID: 9569 RVA: 0x000D52E8 File Offset: 0x000D34E8
	public StatusItemRenderer()
	{
		this.layer = LayerMask.NameToLayer("UI");
		this.entries = new StatusItemRenderer.Entry[100];
		this.shader = Shader.Find("Klei/StatusItem");
		for (int i = 0; i < this.entries.Length; i++)
		{
			StatusItemRenderer.Entry entry = default(StatusItemRenderer.Entry);
			entry.Init(this.shader);
			this.entries[i] = entry;
		}
		this.backgroundColor = new Color32(244, 74, 71, byte.MaxValue);
		this.selectedColor = new Color32(225, 181, 180, byte.MaxValue);
		this.neutralColor = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
		this.arrowSprite = Assets.GetSprite("StatusBubbleTop");
		this.backgroundSprite = Assets.GetSprite("StatusBubble");
		this.scale = 1f;
		Game.Instance.Subscribe(2095258329, new Action<object>(this.OnHighlightObject));
	}

	// Token: 0x06002562 RID: 9570 RVA: 0x000D541C File Offset: 0x000D361C
	public int GetIdx(Transform transform)
	{
		int instanceID = transform.GetInstanceID();
		int num = 0;
		if (!this.handleTable.TryGetValue(instanceID, out num))
		{
			int num2 = this.entryCount;
			this.entryCount = num2 + 1;
			num = num2;
			this.handleTable[instanceID] = num;
			StatusItemRenderer.Entry entry = this.entries[num];
			entry.handle = instanceID;
			entry.transform = transform;
			entry.buildingPos = transform.GetPosition();
			entry.building = transform.GetComponent<Building>();
			entry.isBuilding = entry.building != null;
			entry.selectable = transform.GetComponent<KSelectable>();
			this.entries[num] = entry;
		}
		return num;
	}

	// Token: 0x06002563 RID: 9571 RVA: 0x000D54CC File Offset: 0x000D36CC
	public void Add(Transform transform, StatusItem status_item)
	{
		if (this.entryCount == this.entries.Length)
		{
			StatusItemRenderer.Entry[] array = new StatusItemRenderer.Entry[this.entries.Length * 2];
			for (int i = 0; i < this.entries.Length; i++)
			{
				array[i] = this.entries[i];
			}
			for (int j = this.entries.Length; j < array.Length; j++)
			{
				array[j].Init(this.shader);
			}
			this.entries = array;
		}
		int idx = this.GetIdx(transform);
		StatusItemRenderer.Entry entry = this.entries[idx];
		entry.Add(status_item);
		this.entries[idx] = entry;
	}

	// Token: 0x06002564 RID: 9572 RVA: 0x000D557C File Offset: 0x000D377C
	public void Remove(Transform transform, StatusItem status_item)
	{
		int instanceID = transform.GetInstanceID();
		int num = 0;
		if (!this.handleTable.TryGetValue(instanceID, out num))
		{
			return;
		}
		StatusItemRenderer.Entry entry = this.entries[num];
		if (entry.statusItems.Count == 0)
		{
			return;
		}
		entry.Remove(status_item);
		this.entries[num] = entry;
		if (entry.statusItems.Count == 0)
		{
			this.ClearIdx(num);
		}
	}

	// Token: 0x06002565 RID: 9573 RVA: 0x000D55E8 File Offset: 0x000D37E8
	private void ClearIdx(int idx)
	{
		StatusItemRenderer.Entry entry = this.entries[idx];
		this.handleTable.Remove(entry.handle);
		if (idx != this.entryCount - 1)
		{
			entry.Replace(this.entries[this.entryCount - 1]);
			this.entries[idx] = entry;
			this.handleTable[entry.handle] = idx;
		}
		entry = this.entries[this.entryCount - 1];
		entry.Clear();
		this.entries[this.entryCount - 1] = entry;
		this.entryCount--;
	}

	// Token: 0x06002566 RID: 9574 RVA: 0x000D5695 File Offset: 0x000D3895
	private HashedString GetMode()
	{
		if (OverlayScreen.Instance != null)
		{
			return OverlayScreen.Instance.mode;
		}
		return OverlayModes.None.ID;
	}

	// Token: 0x06002567 RID: 9575 RVA: 0x000D56B4 File Offset: 0x000D38B4
	public void MarkAllDirty()
	{
		for (int i = 0; i < this.entryCount; i++)
		{
			this.entries[i].MarkDirty();
		}
	}

	// Token: 0x06002568 RID: 9576 RVA: 0x000D56E4 File Offset: 0x000D38E4
	public void RenderEveryTick()
	{
		if (DebugHandler.HideUI)
		{
			return;
		}
		this.scale = 1f + Mathf.Sin(Time.unscaledTime * 8f) * 0.1f;
		Shader.SetGlobalVector("_StatusItemParameters", new Vector4(this.scale, 0f, 0f, 0f));
		Vector3 vector = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, Camera.main.transform.GetPosition().z));
		Vector3 vector2 = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, Camera.main.transform.GetPosition().z));
		this.visibleEntries.Clear();
		Camera worldCamera = GameScreenManager.Instance.worldSpaceCanvas.GetComponent<Canvas>().worldCamera;
		for (int i = 0; i < this.entryCount; i++)
		{
			this.entries[i].Render(this, vector2, vector, this.GetMode(), worldCamera);
		}
	}

	// Token: 0x06002569 RID: 9577 RVA: 0x000D57E8 File Offset: 0x000D39E8
	public void GetIntersections(Vector2 pos, List<InterfaceTool.Intersection> intersections)
	{
		foreach (StatusItemRenderer.Entry entry in this.visibleEntries)
		{
			entry.GetIntersection(pos, intersections, this.scale);
		}
	}

	// Token: 0x0600256A RID: 9578 RVA: 0x000D5844 File Offset: 0x000D3A44
	public void GetIntersections(Vector2 pos, List<KSelectable> selectables)
	{
		foreach (StatusItemRenderer.Entry entry in this.visibleEntries)
		{
			entry.GetIntersection(pos, selectables, this.scale);
		}
	}

	// Token: 0x0600256B RID: 9579 RVA: 0x000D58A0 File Offset: 0x000D3AA0
	public void SetOffset(Transform transform, Vector3 offset)
	{
		int num = 0;
		if (this.handleTable.TryGetValue(transform.GetInstanceID(), out num))
		{
			this.entries[num].offset = offset;
		}
	}

	// Token: 0x0600256C RID: 9580 RVA: 0x000D58D8 File Offset: 0x000D3AD8
	private void OnSelectObject(object data)
	{
		int num = 0;
		if (this.handleTable.TryGetValue(this.selectedHandle, out num))
		{
			this.entries[num].MarkDirty();
		}
		GameObject gameObject = (GameObject)data;
		if (gameObject != null)
		{
			this.selectedHandle = gameObject.transform.GetInstanceID();
			if (this.handleTable.TryGetValue(this.selectedHandle, out num))
			{
				this.entries[num].MarkDirty();
				return;
			}
		}
		else
		{
			this.highlightHandle = -1;
		}
	}

	// Token: 0x0600256D RID: 9581 RVA: 0x000D595C File Offset: 0x000D3B5C
	private void OnHighlightObject(object data)
	{
		int num = 0;
		if (this.handleTable.TryGetValue(this.highlightHandle, out num))
		{
			StatusItemRenderer.Entry entry = this.entries[num];
			entry.MarkDirty();
			this.entries[num] = entry;
		}
		GameObject gameObject = (GameObject)data;
		if (gameObject != null)
		{
			this.highlightHandle = gameObject.transform.GetInstanceID();
			if (this.handleTable.TryGetValue(this.highlightHandle, out num))
			{
				StatusItemRenderer.Entry entry2 = this.entries[num];
				entry2.MarkDirty();
				this.entries[num] = entry2;
				return;
			}
		}
		else
		{
			this.highlightHandle = -1;
		}
	}

	// Token: 0x0600256E RID: 9582 RVA: 0x000D5A00 File Offset: 0x000D3C00
	public void Destroy()
	{
		Game.Instance.Unsubscribe(-1503271301, new Action<object>(this.OnSelectObject));
		Game.Instance.Unsubscribe(-1201923725, new Action<object>(this.OnHighlightObject));
		foreach (StatusItemRenderer.Entry entry in this.entries)
		{
			entry.Clear();
			entry.FreeResources();
		}
	}

	// Token: 0x040015EC RID: 5612
	private StatusItemRenderer.Entry[] entries;

	// Token: 0x040015ED RID: 5613
	private int entryCount;

	// Token: 0x040015EE RID: 5614
	private Dictionary<int, int> handleTable = new Dictionary<int, int>();

	// Token: 0x040015F8 RID: 5624
	private Shader shader;

	// Token: 0x040015F9 RID: 5625
	public List<StatusItemRenderer.Entry> visibleEntries = new List<StatusItemRenderer.Entry>();

	// Token: 0x020014B1 RID: 5297
	public struct Entry
	{
		// Token: 0x06008E98 RID: 36504 RVA: 0x0035BAAB File Offset: 0x00359CAB
		public void Init(Shader shader)
		{
			this.statusItems = new List<StatusItem>();
			this.mesh = new Mesh();
			this.mesh.name = "StatusItemRenderer";
			this.dirty = true;
			this.material = new Material(shader);
		}

		// Token: 0x06008E99 RID: 36505 RVA: 0x0035BAE8 File Offset: 0x00359CE8
		public void Render(StatusItemRenderer renderer, Vector3 camera_bl, Vector3 camera_tr, HashedString overlay, Camera camera)
		{
			if (this.transform == null)
			{
				string text = "Error cleaning up status items:";
				foreach (StatusItem statusItem in this.statusItems)
				{
					text += statusItem.Id;
				}
				global::Debug.LogWarning(text);
				return;
			}
			Vector3 vector = (this.isBuilding ? this.buildingPos : this.transform.GetPosition());
			if (this.isBuilding)
			{
				vector.x += (float)((this.building.Def.WidthInCells - 1) % 2) / 2f;
			}
			if (vector.x < camera_bl.x || vector.x > camera_tr.x || vector.y < camera_bl.y || vector.y > camera_tr.y)
			{
				return;
			}
			int num = Grid.PosToCell(vector);
			if (Grid.IsValidCell(num) && (!Grid.IsVisible(num) || (int)Grid.WorldIdx[num] != ClusterManager.Instance.activeWorldId))
			{
				return;
			}
			if (!this.selectable.IsSelectable)
			{
				return;
			}
			renderer.visibleEntries.Add(this);
			if (this.dirty)
			{
				int num2 = 0;
				StatusItemRenderer.Entry.spritesListedToRender.Clear();
				StatusItemRenderer.Entry.statusItemsToRender_Index.Clear();
				int num3 = -1;
				foreach (StatusItem statusItem2 in this.statusItems)
				{
					num3++;
					if (statusItem2.UseConditionalCallback(overlay, this.transform) || !(overlay != OverlayModes.None.ID) || !(statusItem2.render_overlay != overlay))
					{
						Sprite sprite = statusItem2.sprite.sprite;
						if (!statusItem2.unique)
						{
							if (StatusItemRenderer.Entry.spritesListedToRender.Contains(sprite) || StatusItemRenderer.Entry.spritesListedToRender.Count >= StatusItemRenderer.Entry.spritesListedToRender.Capacity)
							{
								continue;
							}
							StatusItemRenderer.Entry.spritesListedToRender.Add(sprite);
						}
						StatusItemRenderer.Entry.statusItemsToRender_Index.Add(num3);
						num2++;
					}
				}
				this.hasVisibleStatusItems = num2 != 0;
				StatusItemRenderer.Entry.MeshBuilder meshBuilder = new StatusItemRenderer.Entry.MeshBuilder(num2 + 6, this.material);
				float num4 = 0.25f;
				float num5 = -5f;
				Vector2 vector2 = new Vector2(0.05f, -0.05f);
				float num6 = 0.02f;
				Color32 color = new Color32(0, 0, 0, byte.MaxValue);
				Color32 color2 = new Color32(0, 0, 0, 75);
				Color32 color3 = renderer.neutralColor;
				if (renderer.selectedHandle == this.handle || renderer.highlightHandle == this.handle)
				{
					color3 = renderer.selectedColor;
				}
				else
				{
					for (int i = 0; i < this.statusItems.Count; i++)
					{
						if (this.statusItems[i].notificationType != NotificationType.Neutral)
						{
							color3 = renderer.backgroundColor;
							break;
						}
					}
				}
				meshBuilder.AddQuad(new Vector2(0f, 0.29f) + vector2, new Vector2(0.05f, 0.05f), num5, renderer.arrowSprite, color2);
				meshBuilder.AddQuad(new Vector2(0f, 0f) + vector2, new Vector2(num4 * (float)num2, num4), num5, renderer.backgroundSprite, color2);
				meshBuilder.AddQuad(new Vector2(0f, 0f), new Vector2(num4 * (float)num2 + num6, num4 + num6), num5, renderer.backgroundSprite, color);
				meshBuilder.AddQuad(new Vector2(0f, 0f), new Vector2(num4 * (float)num2, num4), num5, renderer.backgroundSprite, color3);
				for (int j = 0; j < StatusItemRenderer.Entry.statusItemsToRender_Index.Count; j++)
				{
					StatusItem statusItem3 = this.statusItems[StatusItemRenderer.Entry.statusItemsToRender_Index[j]];
					float num7 = (float)j * num4 * 2f - num4 * (float)(num2 - 1);
					if (statusItem3.sprite == null)
					{
						DebugUtil.DevLogError(string.Concat(new string[] { "Status Item ", statusItem3.Id, " has null sprite for icon '", statusItem3.iconName, "', you need to run Collect Sprites or manually add the sprite to the TintedSprites list in the GameAssets prefab." }));
						statusItem3.iconName = "status_item_exclamation";
						statusItem3.sprite = Assets.GetTintedSprite("status_item_exclamation");
					}
					Sprite sprite2 = statusItem3.sprite.sprite;
					meshBuilder.AddQuad(new Vector2(num7, 0f), new Vector2(num4, num4), num5, sprite2, color);
				}
				meshBuilder.AddQuad(new Vector2(0f, 0.29f + num6), new Vector2(0.05f + num6, 0.05f + num6), num5, renderer.arrowSprite, color);
				meshBuilder.AddQuad(new Vector2(0f, 0.29f), new Vector2(0.05f, 0.05f), num5, renderer.arrowSprite, color3);
				meshBuilder.End(this.mesh);
				this.dirty = false;
			}
			if (this.hasVisibleStatusItems && GameScreenManager.Instance != null)
			{
				Graphics.DrawMesh(this.mesh, vector + this.offset, Quaternion.identity, this.material, renderer.layer, camera, 0, null, false, false);
			}
		}

		// Token: 0x06008E9A RID: 36506 RVA: 0x0035C070 File Offset: 0x0035A270
		public void Add(StatusItem status_item)
		{
			this.statusItems.Add(status_item);
			this.dirty = true;
		}

		// Token: 0x06008E9B RID: 36507 RVA: 0x0035C085 File Offset: 0x0035A285
		public void Remove(StatusItem status_item)
		{
			this.statusItems.Remove(status_item);
			this.dirty = true;
		}

		// Token: 0x06008E9C RID: 36508 RVA: 0x0035C09C File Offset: 0x0035A29C
		public void Replace(StatusItemRenderer.Entry entry)
		{
			this.handle = entry.handle;
			this.transform = entry.transform;
			this.building = this.transform.GetComponent<Building>();
			this.buildingPos = this.transform.GetPosition();
			this.isBuilding = this.building != null;
			this.selectable = this.transform.GetComponent<KSelectable>();
			this.offset = entry.offset;
			this.dirty = true;
			this.statusItems.Clear();
			this.statusItems.AddRange(entry.statusItems);
		}

		// Token: 0x06008E9D RID: 36509 RVA: 0x0035C138 File Offset: 0x0035A338
		private bool Intersects(Vector2 pos, float scale)
		{
			if (this.transform == null)
			{
				return false;
			}
			Bounds bounds = this.mesh.bounds;
			Vector3 vector = this.buildingPos + this.offset + bounds.center;
			Vector2 vector2 = new Vector2(vector.x, vector.y);
			Vector3 size = bounds.size;
			Vector2 vector3 = new Vector2(size.x * scale * 0.5f, size.y * scale * 0.5f);
			Vector2 vector4 = vector2 - vector3;
			Vector2 vector5 = vector2 + vector3;
			return pos.x >= vector4.x && pos.x <= vector5.x && pos.y >= vector4.y && pos.y <= vector5.y;
		}

		// Token: 0x06008E9E RID: 36510 RVA: 0x0035C210 File Offset: 0x0035A410
		public void GetIntersection(Vector2 pos, List<InterfaceTool.Intersection> intersections, float scale)
		{
			if (this.Intersects(pos, scale) && this.selectable.IsSelectable)
			{
				intersections.Add(new InterfaceTool.Intersection
				{
					component = this.selectable,
					distance = -100f
				});
			}
		}

		// Token: 0x06008E9F RID: 36511 RVA: 0x0035C25C File Offset: 0x0035A45C
		public void GetIntersection(Vector2 pos, List<KSelectable> selectables, float scale)
		{
			if (this.Intersects(pos, scale) && this.selectable.IsSelectable && !selectables.Contains(this.selectable))
			{
				selectables.Add(this.selectable);
			}
		}

		// Token: 0x06008EA0 RID: 36512 RVA: 0x0035C28F File Offset: 0x0035A48F
		public void Clear()
		{
			this.statusItems.Clear();
			this.offset = Vector3.zero;
			this.dirty = false;
		}

		// Token: 0x06008EA1 RID: 36513 RVA: 0x0035C2AE File Offset: 0x0035A4AE
		public void FreeResources()
		{
			if (this.mesh != null)
			{
				global::UnityEngine.Object.DestroyImmediate(this.mesh);
				this.mesh = null;
			}
			if (this.material != null)
			{
				global::UnityEngine.Object.DestroyImmediate(this.material);
			}
		}

		// Token: 0x06008EA2 RID: 36514 RVA: 0x0035C2E9 File Offset: 0x0035A4E9
		public void MarkDirty()
		{
			this.dirty = true;
		}

		// Token: 0x04006D82 RID: 28034
		public int handle;

		// Token: 0x04006D83 RID: 28035
		public Transform transform;

		// Token: 0x04006D84 RID: 28036
		public Building building;

		// Token: 0x04006D85 RID: 28037
		public Vector3 buildingPos;

		// Token: 0x04006D86 RID: 28038
		public KSelectable selectable;

		// Token: 0x04006D87 RID: 28039
		public List<StatusItem> statusItems;

		// Token: 0x04006D88 RID: 28040
		public Mesh mesh;

		// Token: 0x04006D89 RID: 28041
		public bool dirty;

		// Token: 0x04006D8A RID: 28042
		public int layer;

		// Token: 0x04006D8B RID: 28043
		public Material material;

		// Token: 0x04006D8C RID: 28044
		public Vector3 offset;

		// Token: 0x04006D8D RID: 28045
		public bool hasVisibleStatusItems;

		// Token: 0x04006D8E RID: 28046
		public bool isBuilding;

		// Token: 0x04006D8F RID: 28047
		private const int STATUS_ICONS_LIMIT = 12;

		// Token: 0x04006D90 RID: 28048
		public static List<Sprite> spritesListedToRender = new List<Sprite>(12);

		// Token: 0x04006D91 RID: 28049
		public static List<int> statusItemsToRender_Index = new List<int>(12);

		// Token: 0x0200274A RID: 10058
		private struct MeshBuilder
		{
			// Token: 0x0600C65B RID: 50779 RVA: 0x00412130 File Offset: 0x00410330
			public MeshBuilder(int quad_count, Material material)
			{
				this.vertices = new Vector3[4 * quad_count];
				this.uvs = new Vector2[4 * quad_count];
				this.uv2s = new Vector2[4 * quad_count];
				this.colors = new Color32[4 * quad_count];
				this.triangles = new int[6 * quad_count];
				this.material = material;
				this.quadIdx = 0;
			}

			// Token: 0x0600C65C RID: 50780 RVA: 0x00412194 File Offset: 0x00410394
			public void AddQuad(Vector2 center, Vector2 half_size, float z, Sprite sprite, Color color)
			{
				if (this.quadIdx == StatusItemRenderer.Entry.MeshBuilder.textureIds.Length)
				{
					return;
				}
				Rect rect = sprite.rect;
				Rect textureRect = sprite.textureRect;
				float num = textureRect.width / rect.width;
				float num2 = textureRect.height / rect.height;
				int num3 = 4 * this.quadIdx;
				this.vertices[num3] = new Vector3((center.x - half_size.x) * num, (center.y - half_size.y) * num2, z);
				this.vertices[1 + num3] = new Vector3((center.x - half_size.x) * num, (center.y + half_size.y) * num2, z);
				this.vertices[2 + num3] = new Vector3((center.x + half_size.x) * num, (center.y - half_size.y) * num2, z);
				this.vertices[3 + num3] = new Vector3((center.x + half_size.x) * num, (center.y + half_size.y) * num2, z);
				float num4 = textureRect.x / (float)sprite.texture.width;
				float num5 = textureRect.y / (float)sprite.texture.height;
				float num6 = textureRect.width / (float)sprite.texture.width;
				float num7 = textureRect.height / (float)sprite.texture.height;
				this.uvs[num3] = new Vector2(num4, num5);
				this.uvs[1 + num3] = new Vector2(num4, num5 + num7);
				this.uvs[2 + num3] = new Vector2(num4 + num6, num5);
				this.uvs[3 + num3] = new Vector2(num4 + num6, num5 + num7);
				this.colors[num3] = color;
				this.colors[1 + num3] = color;
				this.colors[2 + num3] = color;
				this.colors[3 + num3] = color;
				float num8 = (float)this.quadIdx + 0.5f;
				this.uv2s[num3] = new Vector2(num8, 0f);
				this.uv2s[1 + num3] = new Vector2(num8, 0f);
				this.uv2s[2 + num3] = new Vector2(num8, 0f);
				this.uv2s[3 + num3] = new Vector2(num8, 0f);
				int num9 = 6 * this.quadIdx;
				this.triangles[num9] = num3;
				this.triangles[1 + num9] = num3 + 1;
				this.triangles[2 + num9] = num3 + 2;
				this.triangles[3 + num9] = num3 + 2;
				this.triangles[4 + num9] = num3 + 1;
				this.triangles[5 + num9] = num3 + 3;
				this.material.SetTexture(StatusItemRenderer.Entry.MeshBuilder.textureIds[this.quadIdx], sprite.texture);
				this.quadIdx++;
			}

			// Token: 0x0600C65D RID: 50781 RVA: 0x004124E0 File Offset: 0x004106E0
			public void End(Mesh mesh)
			{
				mesh.Clear();
				mesh.vertices = this.vertices;
				mesh.uv = this.uvs;
				mesh.uv2 = this.uv2s;
				mesh.colors32 = this.colors;
				mesh.SetTriangles(this.triangles, 0);
				mesh.RecalculateBounds();
			}

			// Token: 0x0400AD67 RID: 44391
			private Vector3[] vertices;

			// Token: 0x0400AD68 RID: 44392
			private Vector2[] uvs;

			// Token: 0x0400AD69 RID: 44393
			private Vector2[] uv2s;

			// Token: 0x0400AD6A RID: 44394
			private int[] triangles;

			// Token: 0x0400AD6B RID: 44395
			private Color32[] colors;

			// Token: 0x0400AD6C RID: 44396
			private int quadIdx;

			// Token: 0x0400AD6D RID: 44397
			private Material material;

			// Token: 0x0400AD6E RID: 44398
			private static int[] textureIds = new int[]
			{
				Shader.PropertyToID("_Tex0"),
				Shader.PropertyToID("_Tex1"),
				Shader.PropertyToID("_Tex2"),
				Shader.PropertyToID("_Tex3"),
				Shader.PropertyToID("_Tex4"),
				Shader.PropertyToID("_Tex5"),
				Shader.PropertyToID("_Tex6"),
				Shader.PropertyToID("_Tex7"),
				Shader.PropertyToID("_Tex8"),
				Shader.PropertyToID("_Tex9"),
				Shader.PropertyToID("_Tex10")
			};
		}
	}
}
