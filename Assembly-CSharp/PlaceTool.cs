using System;
using UnityEngine;

// Token: 0x0200097B RID: 2427
public class PlaceTool : DragTool
{
	// Token: 0x06004639 RID: 17977 RVA: 0x001941DD File Offset: 0x001923DD
	public static void DestroyInstance()
	{
		PlaceTool.Instance = null;
	}

	// Token: 0x0600463A RID: 17978 RVA: 0x001941E5 File Offset: 0x001923E5
	protected override void OnPrefabInit()
	{
		PlaceTool.Instance = this;
		this.tooltip = base.GetComponent<ToolTip>();
	}

	// Token: 0x0600463B RID: 17979 RVA: 0x001941FC File Offset: 0x001923FC
	protected override void OnActivateTool()
	{
		this.active = true;
		base.OnActivateTool();
		this.visualizer = new GameObject("PlaceToolVisualizer");
		this.visualizer.SetActive(false);
		this.visualizer.SetLayerRecursively(LayerMask.NameToLayer("Place"));
		KBatchedAnimController kbatchedAnimController = this.visualizer.AddComponent<KBatchedAnimController>();
		kbatchedAnimController.visibilityType = KAnimControllerBase.VisibilityType.Always;
		kbatchedAnimController.isMovable = true;
		kbatchedAnimController.SetLayer(LayerMask.NameToLayer("Place"));
		kbatchedAnimController.AnimFiles = new KAnimFile[] { Assets.GetAnim(this.source.kAnimName) };
		kbatchedAnimController.initialAnim = this.source.animName;
		this.visualizer.SetActive(true);
		this.ShowToolTip();
		base.GetComponent<PlaceToolHoverTextCard>().currentPlaceable = this.source;
		ResourceRemainingDisplayScreen.instance.ActivateDisplay(this.visualizer);
		GridCompositor.Instance.ToggleMajor(true);
	}

	// Token: 0x0600463C RID: 17980 RVA: 0x001942E4 File Offset: 0x001924E4
	protected override void OnDeactivateTool(InterfaceTool new_tool)
	{
		this.active = false;
		GridCompositor.Instance.ToggleMajor(false);
		this.HideToolTip();
		ResourceRemainingDisplayScreen.instance.DeactivateDisplay();
		global::UnityEngine.Object.Destroy(this.visualizer);
		KMonoBehaviour.PlaySound(GlobalAssets.GetSound(this.GetDeactivateSound(), false));
		this.source = null;
		this.onPlacedCallback = null;
		base.OnDeactivateTool(new_tool);
	}

	// Token: 0x0600463D RID: 17981 RVA: 0x00194344 File Offset: 0x00192544
	public void Activate(Placeable source, Action<Placeable, int> onPlacedCallback)
	{
		this.source = source;
		this.onPlacedCallback = onPlacedCallback;
		PlayerController.Instance.ActivateTool(this);
	}

	// Token: 0x0600463E RID: 17982 RVA: 0x00194360 File Offset: 0x00192560
	protected override void OnDragTool(int cell, int distFromOrigin)
	{
		if (this.visualizer == null)
		{
			return;
		}
		bool flag = false;
		string text;
		if (this.source.IsValidPlaceLocation(cell, out text))
		{
			this.onPlacedCallback(this.source, cell);
			flag = true;
		}
		if (flag)
		{
			base.DeactivateTool(null);
		}
	}

	// Token: 0x0600463F RID: 17983 RVA: 0x001943AC File Offset: 0x001925AC
	protected override DragTool.Mode GetMode()
	{
		return DragTool.Mode.Brush;
	}

	// Token: 0x06004640 RID: 17984 RVA: 0x001943AF File Offset: 0x001925AF
	private void ShowToolTip()
	{
		ToolTipScreen.Instance.SetToolTip(this.tooltip);
	}

	// Token: 0x06004641 RID: 17985 RVA: 0x001943C1 File Offset: 0x001925C1
	private void HideToolTip()
	{
		ToolTipScreen.Instance.ClearToolTip(this.tooltip);
	}

	// Token: 0x06004642 RID: 17986 RVA: 0x001943D4 File Offset: 0x001925D4
	public override void OnMouseMove(Vector3 cursorPos)
	{
		cursorPos = base.ClampPositionToWorld(cursorPos, ClusterManager.Instance.activeWorld);
		int num = Grid.PosToCell(cursorPos);
		KBatchedAnimController component = this.visualizer.GetComponent<KBatchedAnimController>();
		string text;
		if (this.source.IsValidPlaceLocation(num, out text))
		{
			component.TintColour = Color.white;
		}
		else
		{
			component.TintColour = Color.red;
		}
		base.OnMouseMove(cursorPos);
	}

	// Token: 0x06004643 RID: 17987 RVA: 0x00194440 File Offset: 0x00192640
	public void Update()
	{
		if (this.active)
		{
			KBatchedAnimController component = this.visualizer.GetComponent<KBatchedAnimController>();
			if (component != null)
			{
				component.SetLayer(LayerMask.NameToLayer("Place"));
			}
		}
	}

	// Token: 0x06004644 RID: 17988 RVA: 0x0019447A File Offset: 0x0019267A
	public override string GetDeactivateSound()
	{
		return "HUD_Click_Deselect";
	}

	// Token: 0x04002EAE RID: 11950
	[SerializeField]
	private TextStyleSetting tooltipStyle;

	// Token: 0x04002EAF RID: 11951
	private Action<Placeable, int> onPlacedCallback;

	// Token: 0x04002EB0 RID: 11952
	private Placeable source;

	// Token: 0x04002EB1 RID: 11953
	private ToolTip tooltip;

	// Token: 0x04002EB2 RID: 11954
	public static PlaceTool Instance;

	// Token: 0x04002EB3 RID: 11955
	private bool active;
}
