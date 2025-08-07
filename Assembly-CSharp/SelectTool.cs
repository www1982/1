using System;
using FMOD.Studio;
using UnityEngine;

// Token: 0x0200098B RID: 2443
public class SelectTool : InterfaceTool
{
	// Token: 0x060046EB RID: 18155 RVA: 0x00197CAB File Offset: 0x00195EAB
	public static void DestroyInstance()
	{
		SelectTool.Instance = null;
	}

	// Token: 0x060046EC RID: 18156 RVA: 0x00197CB4 File Offset: 0x00195EB4
	protected override void OnPrefabInit()
	{
		this.defaultLayerMask = 1 | LayerMask.GetMask(new string[] { "World", "Pickupable", "Place", "PlaceWithDepth", "BlockSelection", "Construction", "Selection" });
		this.layerMask = this.defaultLayerMask;
		this.selectMarker = global::Util.KInstantiateUI<SelectMarker>(EntityPrefabs.Instance.SelectMarker, GameScreenManager.Instance.worldSpaceCanvas, false);
		this.selectMarker.gameObject.SetActive(false);
		this.populateHitsList = true;
		SelectTool.Instance = this;
	}

	// Token: 0x060046ED RID: 18157 RVA: 0x00197D56 File Offset: 0x00195F56
	public void Activate()
	{
		PlayerController.Instance.ActivateTool(this);
		ToolMenu.Instance.PriorityScreen.ResetPriority();
		this.Select(null, false);
	}

	// Token: 0x060046EE RID: 18158 RVA: 0x00197D7A File Offset: 0x00195F7A
	public void SetLayerMask(int mask)
	{
		this.layerMask = mask;
		base.ClearHover();
		this.LateUpdate();
	}

	// Token: 0x060046EF RID: 18159 RVA: 0x00197D8F File Offset: 0x00195F8F
	public void ClearLayerMask()
	{
		this.layerMask = this.defaultLayerMask;
	}

	// Token: 0x060046F0 RID: 18160 RVA: 0x00197D9D File Offset: 0x00195F9D
	public int GetDefaultLayerMask()
	{
		return this.defaultLayerMask;
	}

	// Token: 0x060046F1 RID: 18161 RVA: 0x00197DA5 File Offset: 0x00195FA5
	protected override void OnDeactivateTool(InterfaceTool new_tool)
	{
		base.OnDeactivateTool(new_tool);
		base.ClearHover();
		this.Select(null, false);
	}

	// Token: 0x060046F2 RID: 18162 RVA: 0x00197DBC File Offset: 0x00195FBC
	public void Focus(Vector3 pos, KSelectable selectable, Vector3 offset)
	{
		if (selectable != null)
		{
			pos = selectable.transform.GetPosition();
		}
		pos.z = -40f;
		pos += offset;
		WorldContainer worldFromPosition = ClusterManager.Instance.GetWorldFromPosition(pos);
		if (worldFromPosition != null)
		{
			GameUtil.FocusCameraOnWorld(worldFromPosition.id, pos, 10f, null, true);
			return;
		}
		DebugUtil.DevLogError("DevError: specified camera focus position has null world - possible out of bounds location");
	}

	// Token: 0x060046F3 RID: 18163 RVA: 0x00197E27 File Offset: 0x00196027
	public void SelectAndFocus(Vector3 pos, KSelectable selectable, Vector3 offset)
	{
		this.Focus(pos, selectable, offset);
		this.Select(selectable, false);
	}

	// Token: 0x060046F4 RID: 18164 RVA: 0x00197E3A File Offset: 0x0019603A
	public void SelectAndFocus(Vector3 pos, KSelectable selectable)
	{
		this.SelectAndFocus(pos, selectable, Vector3.zero);
	}

	// Token: 0x060046F5 RID: 18165 RVA: 0x00197E49 File Offset: 0x00196049
	public void SelectNextFrame(KSelectable new_selected, bool skipSound = false)
	{
		this.delayedNextSelection = new_selected;
		this.delayedSkipSound = skipSound;
		UIScheduler.Instance.ScheduleNextFrame("DelayedSelect", new Action<object>(this.DoSelectNextFrame), null, null);
	}

	// Token: 0x060046F6 RID: 18166 RVA: 0x00197E77 File Offset: 0x00196077
	private void DoSelectNextFrame(object data)
	{
		this.Select(this.delayedNextSelection, this.delayedSkipSound);
		this.delayedNextSelection = null;
	}

	// Token: 0x060046F7 RID: 18167 RVA: 0x00197E94 File Offset: 0x00196094
	public void Select(KSelectable new_selected, bool skipSound = false)
	{
		if (new_selected == this.previousSelection)
		{
			return;
		}
		this.previousSelection = new_selected;
		if (this.selected != null)
		{
			this.selected.Unselect();
		}
		GameObject gameObject = null;
		if (new_selected != null && new_selected.GetMyWorldId() == ClusterManager.Instance.activeWorldId)
		{
			SelectToolHoverTextCard component = base.GetComponent<SelectToolHoverTextCard>();
			if (component != null)
			{
				int num = component.currentSelectedSelectableIndex;
				int recentNumberOfDisplayedSelectables = component.recentNumberOfDisplayedSelectables;
				if (recentNumberOfDisplayedSelectables != 0)
				{
					num = (num + 1) % recentNumberOfDisplayedSelectables;
					if (!skipSound)
					{
						if (recentNumberOfDisplayedSelectables == 1)
						{
							KFMOD.PlayUISound(GlobalAssets.GetSound("Select_empty", false));
						}
						else
						{
							EventInstance eventInstance = KFMOD.BeginOneShot(GlobalAssets.GetSound("Select_full", false), Vector3.zero, 1f);
							eventInstance.setParameterByName("selection", (float)num, false);
							SoundEvent.EndOneShot(eventInstance);
						}
						this.playedSoundThisFrame = true;
					}
				}
			}
			if (new_selected == this.hover)
			{
				base.ClearHover();
			}
			new_selected.Select();
			gameObject = new_selected.gameObject;
			this.selectMarker.SetTargetTransform(gameObject.transform);
			this.selectMarker.gameObject.SetActive(!new_selected.DisableSelectMarker);
		}
		else if (this.selectMarker != null)
		{
			this.selectMarker.gameObject.SetActive(false);
		}
		this.selected = ((gameObject == null) ? null : new_selected);
		Game.Instance.Trigger(-1503271301, gameObject);
	}

	// Token: 0x060046F8 RID: 18168 RVA: 0x00198000 File Offset: 0x00196200
	public override void OnLeftClickDown(Vector3 cursor_pos)
	{
		KSelectable objectUnderCursor = base.GetObjectUnderCursor<KSelectable>(true, (KSelectable s) => s.GetComponent<KSelectable>().IsSelectable, this.selected);
		this.selectedCell = Grid.PosToCell(cursor_pos);
		this.Select(objectUnderCursor, false);
		if (DevToolSimDebug.Instance != null)
		{
			DevToolSimDebug.Instance.SetCell(this.selectedCell);
		}
		if (DevToolNavGrid.Instance != null)
		{
			DevToolNavGrid.Instance.SetCell(this.selectedCell);
		}
	}

	// Token: 0x060046F9 RID: 18169 RVA: 0x0019807C File Offset: 0x0019627C
	public int GetSelectedCell()
	{
		return this.selectedCell;
	}

	// Token: 0x04002EE8 RID: 12008
	public KSelectable selected;

	// Token: 0x04002EE9 RID: 12009
	protected int cell_new;

	// Token: 0x04002EEA RID: 12010
	private int selectedCell;

	// Token: 0x04002EEB RID: 12011
	protected int defaultLayerMask;

	// Token: 0x04002EEC RID: 12012
	public static SelectTool Instance;

	// Token: 0x04002EED RID: 12013
	private KSelectable delayedNextSelection;

	// Token: 0x04002EEE RID: 12014
	private bool delayedSkipSound;

	// Token: 0x04002EEF RID: 12015
	private KSelectable previousSelection;
}
