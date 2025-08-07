using System;
using UnityEngine;

// Token: 0x02000965 RID: 2405
public class AttackTool : DragTool
{
	// Token: 0x06004507 RID: 17671 RVA: 0x0018D960 File Offset: 0x0018BB60
	protected override void OnDragComplete(Vector3 downPos, Vector3 upPos)
	{
		Vector2 regularizedPos = base.GetRegularizedPos(Vector2.Min(downPos, upPos), true);
		Vector2 regularizedPos2 = base.GetRegularizedPos(Vector2.Max(downPos, upPos), false);
		AttackTool.MarkForAttack(regularizedPos, regularizedPos2, true);
	}

	// Token: 0x06004508 RID: 17672 RVA: 0x0018D9A8 File Offset: 0x0018BBA8
	public static void MarkForAttack(Vector2 min, Vector2 max, bool mark)
	{
		foreach (FactionAlignment factionAlignment in Components.FactionAlignments.Items)
		{
			if (!factionAlignment.IsNullOrDestroyed())
			{
				Vector2 vector = Grid.PosToXY(factionAlignment.transform.GetPosition());
				if (vector.x >= min.x && vector.x < max.x && vector.y >= min.y && vector.y < max.y)
				{
					if (mark)
					{
						if (FactionManager.Instance.GetDisposition(FactionManager.FactionID.Duplicant, factionAlignment.Alignment) != FactionManager.Disposition.Assist)
						{
							factionAlignment.SetPlayerTargeted(true);
							Prioritizable component = factionAlignment.GetComponent<Prioritizable>();
							if (component != null)
							{
								component.SetMasterPriority(ToolMenu.Instance.PriorityScreen.GetLastSelectedPriority());
							}
						}
					}
					else
					{
						factionAlignment.gameObject.Trigger(2127324410, null);
					}
				}
			}
		}
	}

	// Token: 0x06004509 RID: 17673 RVA: 0x0018DAAC File Offset: 0x0018BCAC
	protected override void OnActivateTool()
	{
		base.OnActivateTool();
		ToolMenu.Instance.PriorityScreen.Show(true);
	}

	// Token: 0x0600450A RID: 17674 RVA: 0x0018DAC4 File Offset: 0x0018BCC4
	protected override void OnDeactivateTool(InterfaceTool new_tool)
	{
		base.OnDeactivateTool(new_tool);
		ToolMenu.Instance.PriorityScreen.Show(false);
	}
}
