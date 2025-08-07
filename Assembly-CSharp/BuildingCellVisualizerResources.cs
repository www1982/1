using System;
using UnityEngine;

// Token: 0x02000C00 RID: 3072
public class BuildingCellVisualizerResources : ScriptableObject
{
	// Token: 0x170006C1 RID: 1729
	// (get) Token: 0x06005C81 RID: 23681 RVA: 0x0021C60C File Offset: 0x0021A80C
	public string heatSourceAnimFile
	{
		get
		{
			return "heat_fx_kanim";
		}
	}

	// Token: 0x170006C2 RID: 1730
	// (get) Token: 0x06005C82 RID: 23682 RVA: 0x0021C613 File Offset: 0x0021A813
	public string heatAnimName
	{
		get
		{
			return "heatfx_a";
		}
	}

	// Token: 0x170006C3 RID: 1731
	// (get) Token: 0x06005C83 RID: 23683 RVA: 0x0021C61A File Offset: 0x0021A81A
	public string heatSinkAnimFile
	{
		get
		{
			return "heat_fx_kanim";
		}
	}

	// Token: 0x170006C4 RID: 1732
	// (get) Token: 0x06005C84 RID: 23684 RVA: 0x0021C621 File Offset: 0x0021A821
	public string heatSinkAnimName
	{
		get
		{
			return "heatfx_b";
		}
	}

	// Token: 0x170006C5 RID: 1733
	// (get) Token: 0x06005C85 RID: 23685 RVA: 0x0021C628 File Offset: 0x0021A828
	// (set) Token: 0x06005C86 RID: 23686 RVA: 0x0021C630 File Offset: 0x0021A830
	public Material backgroundMaterial { get; set; }

	// Token: 0x170006C6 RID: 1734
	// (get) Token: 0x06005C87 RID: 23687 RVA: 0x0021C639 File Offset: 0x0021A839
	// (set) Token: 0x06005C88 RID: 23688 RVA: 0x0021C641 File Offset: 0x0021A841
	public Material iconBackgroundMaterial { get; set; }

	// Token: 0x170006C7 RID: 1735
	// (get) Token: 0x06005C89 RID: 23689 RVA: 0x0021C64A File Offset: 0x0021A84A
	// (set) Token: 0x06005C8A RID: 23690 RVA: 0x0021C652 File Offset: 0x0021A852
	public Material powerInputMaterial { get; set; }

	// Token: 0x170006C8 RID: 1736
	// (get) Token: 0x06005C8B RID: 23691 RVA: 0x0021C65B File Offset: 0x0021A85B
	// (set) Token: 0x06005C8C RID: 23692 RVA: 0x0021C663 File Offset: 0x0021A863
	public Material powerOutputMaterial { get; set; }

	// Token: 0x170006C9 RID: 1737
	// (get) Token: 0x06005C8D RID: 23693 RVA: 0x0021C66C File Offset: 0x0021A86C
	// (set) Token: 0x06005C8E RID: 23694 RVA: 0x0021C674 File Offset: 0x0021A874
	public Material liquidInputMaterial { get; set; }

	// Token: 0x170006CA RID: 1738
	// (get) Token: 0x06005C8F RID: 23695 RVA: 0x0021C67D File Offset: 0x0021A87D
	// (set) Token: 0x06005C90 RID: 23696 RVA: 0x0021C685 File Offset: 0x0021A885
	public Material liquidOutputMaterial { get; set; }

	// Token: 0x170006CB RID: 1739
	// (get) Token: 0x06005C91 RID: 23697 RVA: 0x0021C68E File Offset: 0x0021A88E
	// (set) Token: 0x06005C92 RID: 23698 RVA: 0x0021C696 File Offset: 0x0021A896
	public Material gasInputMaterial { get; set; }

	// Token: 0x170006CC RID: 1740
	// (get) Token: 0x06005C93 RID: 23699 RVA: 0x0021C69F File Offset: 0x0021A89F
	// (set) Token: 0x06005C94 RID: 23700 RVA: 0x0021C6A7 File Offset: 0x0021A8A7
	public Material gasOutputMaterial { get; set; }

	// Token: 0x170006CD RID: 1741
	// (get) Token: 0x06005C95 RID: 23701 RVA: 0x0021C6B0 File Offset: 0x0021A8B0
	// (set) Token: 0x06005C96 RID: 23702 RVA: 0x0021C6B8 File Offset: 0x0021A8B8
	public Material highEnergyParticleInputMaterial { get; set; }

	// Token: 0x170006CE RID: 1742
	// (get) Token: 0x06005C97 RID: 23703 RVA: 0x0021C6C1 File Offset: 0x0021A8C1
	// (set) Token: 0x06005C98 RID: 23704 RVA: 0x0021C6C9 File Offset: 0x0021A8C9
	public Material highEnergyParticleOutputMaterial { get; set; }

	// Token: 0x170006CF RID: 1743
	// (get) Token: 0x06005C99 RID: 23705 RVA: 0x0021C6D2 File Offset: 0x0021A8D2
	// (set) Token: 0x06005C9A RID: 23706 RVA: 0x0021C6DA File Offset: 0x0021A8DA
	public Mesh backgroundMesh { get; set; }

	// Token: 0x170006D0 RID: 1744
	// (get) Token: 0x06005C9B RID: 23707 RVA: 0x0021C6E3 File Offset: 0x0021A8E3
	// (set) Token: 0x06005C9C RID: 23708 RVA: 0x0021C6EB File Offset: 0x0021A8EB
	public Mesh iconMesh { get; set; }

	// Token: 0x170006D1 RID: 1745
	// (get) Token: 0x06005C9D RID: 23709 RVA: 0x0021C6F4 File Offset: 0x0021A8F4
	// (set) Token: 0x06005C9E RID: 23710 RVA: 0x0021C6FC File Offset: 0x0021A8FC
	public int backgroundLayer { get; set; }

	// Token: 0x170006D2 RID: 1746
	// (get) Token: 0x06005C9F RID: 23711 RVA: 0x0021C705 File Offset: 0x0021A905
	// (set) Token: 0x06005CA0 RID: 23712 RVA: 0x0021C70D File Offset: 0x0021A90D
	public int iconLayer { get; set; }

	// Token: 0x06005CA1 RID: 23713 RVA: 0x0021C716 File Offset: 0x0021A916
	public static void DestroyInstance()
	{
		BuildingCellVisualizerResources._Instance = null;
	}

	// Token: 0x06005CA2 RID: 23714 RVA: 0x0021C71E File Offset: 0x0021A91E
	public static BuildingCellVisualizerResources Instance()
	{
		if (BuildingCellVisualizerResources._Instance == null)
		{
			BuildingCellVisualizerResources._Instance = Resources.Load<BuildingCellVisualizerResources>("BuildingCellVisualizerResources");
			BuildingCellVisualizerResources._Instance.Initialize();
		}
		return BuildingCellVisualizerResources._Instance;
	}

	// Token: 0x06005CA3 RID: 23715 RVA: 0x0021C74C File Offset: 0x0021A94C
	private void Initialize()
	{
		Shader shader = Shader.Find("Klei/BuildingCell");
		this.backgroundMaterial = new Material(shader);
		this.backgroundMaterial.mainTexture = GlobalResources.Instance().WhiteTexture;
		this.iconBackgroundMaterial = new Material(shader);
		this.iconBackgroundMaterial.mainTexture = GlobalResources.Instance().WhiteTexture;
		this.powerInputMaterial = new Material(shader);
		this.powerOutputMaterial = new Material(shader);
		this.liquidInputMaterial = new Material(shader);
		this.liquidOutputMaterial = new Material(shader);
		this.gasInputMaterial = new Material(shader);
		this.gasOutputMaterial = new Material(shader);
		this.highEnergyParticleInputMaterial = new Material(shader);
		this.highEnergyParticleOutputMaterial = new Material(shader);
		this.backgroundMesh = this.CreateMesh("BuildingCellVisualizer", Vector2.zero, 0.5f);
		float num = 0.5f;
		this.iconMesh = this.CreateMesh("BuildingCellVisualizerIcon", Vector2.zero, num * 0.5f);
		this.backgroundLayer = LayerMask.NameToLayer("Default");
		this.iconLayer = LayerMask.NameToLayer("Place");
	}

	// Token: 0x06005CA4 RID: 23716 RVA: 0x0021C864 File Offset: 0x0021AA64
	private Mesh CreateMesh(string name, Vector2 base_offset, float half_size)
	{
		Mesh mesh = new Mesh();
		mesh.name = name;
		mesh.vertices = new Vector3[]
		{
			new Vector3(-half_size + base_offset.x, -half_size + base_offset.y, 0f),
			new Vector3(half_size + base_offset.x, -half_size + base_offset.y, 0f),
			new Vector3(-half_size + base_offset.x, half_size + base_offset.y, 0f),
			new Vector3(half_size + base_offset.x, half_size + base_offset.y, 0f)
		};
		mesh.uv = new Vector2[]
		{
			new Vector2(0f, 0f),
			new Vector2(1f, 0f),
			new Vector2(0f, 1f),
			new Vector2(1f, 1f)
		};
		mesh.triangles = new int[] { 0, 1, 2, 2, 1, 3 };
		mesh.RecalculateBounds();
		return mesh;
	}

	// Token: 0x04003D70 RID: 15728
	[Header("Electricity")]
	public Color electricityInputColor;

	// Token: 0x04003D71 RID: 15729
	public Color electricityOutputColor;

	// Token: 0x04003D72 RID: 15730
	public Sprite electricityInputIcon;

	// Token: 0x04003D73 RID: 15731
	public Sprite electricityOutputIcon;

	// Token: 0x04003D74 RID: 15732
	public Sprite electricityConnectedIcon;

	// Token: 0x04003D75 RID: 15733
	public Sprite electricityBridgeIcon;

	// Token: 0x04003D76 RID: 15734
	public Sprite electricityBridgeConnectedIcon;

	// Token: 0x04003D77 RID: 15735
	public Sprite electricityArrowIcon;

	// Token: 0x04003D78 RID: 15736
	public Sprite switchIcon;

	// Token: 0x04003D79 RID: 15737
	public Color32 switchColor;

	// Token: 0x04003D7A RID: 15738
	public Color32 switchOffColor = Color.red;

	// Token: 0x04003D7B RID: 15739
	[Header("Gas")]
	public Sprite gasInputIcon;

	// Token: 0x04003D7C RID: 15740
	public Sprite gasOutputIcon;

	// Token: 0x04003D7D RID: 15741
	public BuildingCellVisualizerResources.IOColours gasIOColours;

	// Token: 0x04003D7E RID: 15742
	[Header("Liquid")]
	public Sprite liquidInputIcon;

	// Token: 0x04003D7F RID: 15743
	public Sprite liquidOutputIcon;

	// Token: 0x04003D80 RID: 15744
	public BuildingCellVisualizerResources.IOColours liquidIOColours;

	// Token: 0x04003D81 RID: 15745
	[Header("High Energy Particle")]
	public Sprite highEnergyParticleInputIcon;

	// Token: 0x04003D82 RID: 15746
	public Sprite[] highEnergyParticleOutputIcons;

	// Token: 0x04003D83 RID: 15747
	public Color highEnergyParticleInputColour;

	// Token: 0x04003D84 RID: 15748
	public Color highEnergyParticleOutputColour;

	// Token: 0x04003D85 RID: 15749
	[Header("Heat Sources and Sinks")]
	public Sprite heatSourceIcon;

	// Token: 0x04003D86 RID: 15750
	public Sprite heatSinkIcon;

	// Token: 0x04003D87 RID: 15751
	[Header("Alternate IO Colours")]
	public BuildingCellVisualizerResources.IOColours alternateIOColours;

	// Token: 0x04003D96 RID: 15766
	private static BuildingCellVisualizerResources _Instance;

	// Token: 0x02001D3E RID: 7486
	[Serializable]
	public struct ConnectedDisconnectedColours
	{
		// Token: 0x04008897 RID: 34967
		public Color32 connected;

		// Token: 0x04008898 RID: 34968
		public Color32 disconnected;
	}

	// Token: 0x02001D3F RID: 7487
	[Serializable]
	public struct IOColours
	{
		// Token: 0x04008899 RID: 34969
		public BuildingCellVisualizerResources.ConnectedDisconnectedColours input;

		// Token: 0x0400889A RID: 34970
		public BuildingCellVisualizerResources.ConnectedDisconnectedColours output;
	}
}
