using System;
using System.Collections.Generic;
using System.IO;
using Klei;
using TemplateClasses;
using UnityEngine;

// Token: 0x0200089B RID: 2203
[Serializable]
public class TemplateContainer
{
	// Token: 0x1700043A RID: 1082
	// (get) Token: 0x06003CF3 RID: 15603 RVA: 0x0015303E File Offset: 0x0015123E
	// (set) Token: 0x06003CF4 RID: 15604 RVA: 0x00153046 File Offset: 0x00151246
	public string name { get; set; }

	// Token: 0x1700043B RID: 1083
	// (get) Token: 0x06003CF5 RID: 15605 RVA: 0x0015304F File Offset: 0x0015124F
	// (set) Token: 0x06003CF6 RID: 15606 RVA: 0x00153057 File Offset: 0x00151257
	public int priority { get; set; }

	// Token: 0x1700043C RID: 1084
	// (get) Token: 0x06003CF7 RID: 15607 RVA: 0x00153060 File Offset: 0x00151260
	// (set) Token: 0x06003CF8 RID: 15608 RVA: 0x00153068 File Offset: 0x00151268
	public TemplateContainer.Info info { get; set; }

	// Token: 0x1700043D RID: 1085
	// (get) Token: 0x06003CF9 RID: 15609 RVA: 0x00153071 File Offset: 0x00151271
	// (set) Token: 0x06003CFA RID: 15610 RVA: 0x00153079 File Offset: 0x00151279
	public List<Cell> cells { get; set; }

	// Token: 0x1700043E RID: 1086
	// (get) Token: 0x06003CFB RID: 15611 RVA: 0x00153082 File Offset: 0x00151282
	// (set) Token: 0x06003CFC RID: 15612 RVA: 0x0015308A File Offset: 0x0015128A
	public List<Prefab> buildings { get; set; }

	// Token: 0x1700043F RID: 1087
	// (get) Token: 0x06003CFD RID: 15613 RVA: 0x00153093 File Offset: 0x00151293
	// (set) Token: 0x06003CFE RID: 15614 RVA: 0x0015309B File Offset: 0x0015129B
	public List<Prefab> pickupables { get; set; }

	// Token: 0x17000440 RID: 1088
	// (get) Token: 0x06003CFF RID: 15615 RVA: 0x001530A4 File Offset: 0x001512A4
	// (set) Token: 0x06003D00 RID: 15616 RVA: 0x001530AC File Offset: 0x001512AC
	public List<Prefab> elementalOres { get; set; }

	// Token: 0x17000441 RID: 1089
	// (get) Token: 0x06003D01 RID: 15617 RVA: 0x001530B5 File Offset: 0x001512B5
	// (set) Token: 0x06003D02 RID: 15618 RVA: 0x001530BD File Offset: 0x001512BD
	public List<Prefab> otherEntities { get; set; }

	// Token: 0x06003D03 RID: 15619 RVA: 0x001530C8 File Offset: 0x001512C8
	public void Init(List<Cell> _cells, List<Prefab> _buildings, List<Prefab> _pickupables, List<Prefab> _elementalOres, List<Prefab> _otherEntities)
	{
		if (_cells != null && _cells.Count > 0)
		{
			this.cells = _cells;
		}
		if (_buildings != null && _buildings.Count > 0)
		{
			this.buildings = _buildings;
		}
		if (_pickupables != null && _pickupables.Count > 0)
		{
			this.pickupables = _pickupables;
		}
		if (_elementalOres != null && _elementalOres.Count > 0)
		{
			this.elementalOres = _elementalOres;
		}
		if (_otherEntities != null && _otherEntities.Count > 0)
		{
			this.otherEntities = _otherEntities;
		}
		this.info = new TemplateContainer.Info();
		this.RefreshInfo();
	}

	// Token: 0x06003D04 RID: 15620 RVA: 0x0015314B File Offset: 0x0015134B
	public RectInt GetTemplateBounds(int padding = 0)
	{
		return this.GetTemplateBounds(Vector2I.zero, padding);
	}

	// Token: 0x06003D05 RID: 15621 RVA: 0x00153159 File Offset: 0x00151359
	public RectInt GetTemplateBounds(Vector2 position, int padding = 0)
	{
		return this.GetTemplateBounds(new Vector2I((int)position.x, (int)position.y), padding);
	}

	// Token: 0x06003D06 RID: 15622 RVA: 0x00153178 File Offset: 0x00151378
	public RectInt GetTemplateBounds(Vector2I position, int padding = 0)
	{
		if ((this.info.min - new Vector2f(0, 0)).sqrMagnitude <= 1E-06f)
		{
			this.RefreshInfo();
		}
		return this.info.GetBounds(position, padding);
	}

	// Token: 0x06003D07 RID: 15623 RVA: 0x001531C0 File Offset: 0x001513C0
	public void RefreshInfo()
	{
		if (this.cells == null)
		{
			return;
		}
		int num = 1;
		int num2 = -1;
		int num3 = 1;
		int num4 = -1;
		foreach (Cell cell in this.cells)
		{
			if (cell.location_x < num)
			{
				num = cell.location_x;
			}
			if (cell.location_x > num2)
			{
				num2 = cell.location_x;
			}
			if (cell.location_y < num3)
			{
				num3 = cell.location_y;
			}
			if (cell.location_y > num4)
			{
				num4 = cell.location_y;
			}
		}
		this.info.size = new Vector2((float)(1 + (num2 - num)), (float)(1 + (num4 - num3)));
		this.info.min = new Vector2((float)num, (float)num3);
		this.info.area = this.cells.Count;
	}

	// Token: 0x06003D08 RID: 15624 RVA: 0x001532B8 File Offset: 0x001514B8
	public void SaveToYaml(string save_name)
	{
		string text = TemplateCache.RewriteTemplatePath(save_name);
		if (!Directory.Exists(Path.GetDirectoryName(text)))
		{
			Directory.CreateDirectory(Path.GetDirectoryName(text));
		}
		YamlIO.Save<TemplateContainer>(this, text + ".yaml", null);
	}

	// Token: 0x02001864 RID: 6244
	[Serializable]
	public class Info
	{
		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x06009C6C RID: 40044 RVA: 0x00390C01 File Offset: 0x0038EE01
		// (set) Token: 0x06009C6D RID: 40045 RVA: 0x00390C09 File Offset: 0x0038EE09
		public Vector2f size { get; set; }

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x06009C6E RID: 40046 RVA: 0x00390C12 File Offset: 0x0038EE12
		// (set) Token: 0x06009C6F RID: 40047 RVA: 0x00390C1A File Offset: 0x0038EE1A
		public Vector2f min { get; set; }

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x06009C70 RID: 40048 RVA: 0x00390C23 File Offset: 0x0038EE23
		// (set) Token: 0x06009C71 RID: 40049 RVA: 0x00390C2B File Offset: 0x0038EE2B
		public int area { get; set; }

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x06009C72 RID: 40050 RVA: 0x00390C34 File Offset: 0x0038EE34
		// (set) Token: 0x06009C73 RID: 40051 RVA: 0x00390C3C File Offset: 0x0038EE3C
		public Tag[] tags { get; set; }

		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x06009C74 RID: 40052 RVA: 0x00390C45 File Offset: 0x0038EE45
		// (set) Token: 0x06009C75 RID: 40053 RVA: 0x00390C4D File Offset: 0x0038EE4D
		public Tag[] discover_tags { get; set; }

		// Token: 0x06009C76 RID: 40054 RVA: 0x00390C58 File Offset: 0x0038EE58
		public RectInt GetBounds(Vector2I position, int padding)
		{
			return new RectInt(position.x + (int)this.min.x - padding, position.y + (int)this.min.y - padding, (int)this.size.x + padding * 2, (int)this.size.y + padding * 2);
		}
	}
}
