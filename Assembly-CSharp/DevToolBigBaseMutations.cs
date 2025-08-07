using System;
using ImGuiNET;

// Token: 0x02000662 RID: 1634
public class DevToolBigBaseMutations : DevTool
{
	// Token: 0x06002817 RID: 10263 RVA: 0x000E3626 File Offset: 0x000E1826
	protected override void RenderTo(DevPanel panel)
	{
		if (Game.Instance != null)
		{
			this.ShowButtons();
			return;
		}
		ImGui.Text("Game not available");
	}

	// Token: 0x06002818 RID: 10264 RVA: 0x000E3648 File Offset: 0x000E1848
	private void ShowButtons()
	{
		if (ImGui.Button("Destroy Ladders"))
		{
			this.DestroyGameObjects<Ladder>(Components.Ladders, Tag.Invalid);
		}
		if (ImGui.Button("Destroy Tiles"))
		{
			this.DestroyGameObjects<BuildingComplete>(Components.BuildingCompletes, GameTags.FloorTiles);
		}
		if (ImGui.Button("Destroy Wires"))
		{
			this.DestroyGameObjects<BuildingComplete>(Components.BuildingCompletes, GameTags.Wires);
		}
		if (ImGui.Button("Destroy Pipes"))
		{
			this.DestroyGameObjects<BuildingComplete>(Components.BuildingCompletes, GameTags.Pipes);
		}
	}

	// Token: 0x06002819 RID: 10265 RVA: 0x000E36C8 File Offset: 0x000E18C8
	private void DestroyGameObjects<T>(Components.Cmps<T> componentsList, Tag filterForTag)
	{
		for (int i = componentsList.Count - 1; i >= 0; i--)
		{
			if (!componentsList[i].IsNullOrDestroyed() && (!(filterForTag != Tag.Invalid) || (componentsList[i] as KMonoBehaviour).gameObject.HasTag(filterForTag)))
			{
				Util.KDestroyGameObject(componentsList[i] as KMonoBehaviour);
			}
		}
	}
}
