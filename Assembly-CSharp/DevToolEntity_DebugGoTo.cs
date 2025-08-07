using System;
using System.Runtime.CompilerServices;
using ImGuiNET;
using UnityEngine;

// Token: 0x0200066A RID: 1642
public class DevToolEntity_DebugGoTo : DevTool
{
	// Token: 0x0600283F RID: 10303 RVA: 0x000E508C File Offset: 0x000E328C
	public DevToolEntity_DebugGoTo()
		: this(Option.None)
	{
	}

	// Token: 0x06002840 RID: 10304 RVA: 0x000E509E File Offset: 0x000E329E
	public DevToolEntity_DebugGoTo(Option<DevToolEntityTarget.ForWorldGameObject> target)
	{
		this.targetOpt = target;
	}

	// Token: 0x06002841 RID: 10305 RVA: 0x000E50B4 File Offset: 0x000E32B4
	protected override void RenderTo(DevPanel panel)
	{
		if (ImGui.BeginMenuBar())
		{
			if (ImGui.MenuItem("Eyedrop New Target"))
			{
				panel.PushDevTool(new DevToolEntity_EyeDrop(delegate(DevToolEntityTarget target)
				{
					this.targetOpt = (DevToolEntityTarget.ForWorldGameObject)target;
				}, new Func<DevToolEntityTarget, Option<string>>(DevToolEntity_DebugGoTo.GetErrorForCandidateTarget)));
			}
			ImGui.EndMenuBar();
		}
		this.Name = "Debug Go To";
		if (this.targetOpt.IsNone())
		{
			ImGui.TextWrapped("No Target selected");
			return;
		}
		DevToolEntityTarget.ForWorldGameObject forWorldGameObject = this.targetOpt.Unwrap();
		Option<string> errorForCandidateTarget = DevToolEntity_DebugGoTo.GetErrorForCandidateTarget(forWorldGameObject);
		if (errorForCandidateTarget.IsSome())
		{
			ImGui.TextWrapped(errorForCandidateTarget.Unwrap());
			return;
		}
		this.Name = "Debug Go To for: " + DevToolEntity.GetNameFor(forWorldGameObject.gameObject);
		if (forWorldGameObject.gameObject.IsNullOrDestroyed())
		{
			ImGui.TextWrapped("Target GameObject is null");
			return;
		}
		ImGui.Checkbox("Draw Bounding Box", ref this.shouldDrawBoundingBox);
		ImGuiEx.SimpleField("Target GameObject", DevToolEntity.GetNameFor(forWorldGameObject.gameObject));
		ImGuiEx.SimpleField("Destination Cell Index", DevToolEntity_DebugGoTo.<RenderTo>g__GetCellName|6_1(this.destinationSimCellTarget));
		if (ImGui.Button("Select New Destination Cell"))
		{
			panel.PushDevTool(new DevToolEntity_EyeDrop(delegate(DevToolEntityTarget target)
			{
				this.destinationSimCellTarget = (DevToolEntityTarget.ForSimCell)target;
			}, delegate(DevToolEntityTarget uncastTarget)
			{
				if (!(uncastTarget is DevToolEntityTarget.ForSimCell))
				{
					return "Target is not a sim cell";
				}
				return Option.None;
			}));
		}
		ImGui.Separator();
		ImGui.Checkbox("Should Continously Request", ref this.shouldContinouslyRequest);
		string text = (this.shouldContinouslyRequest ? "Disable continous requests" : (this.destinationSimCellTarget.IsNone() ? "No destination target." : null));
		if (ImGuiEx.Button("Request Target go to Destination", text) || (this.shouldContinouslyRequest && this.destinationSimCellTarget.IsSome()))
		{
			DebugGoToMonitor.Instance smi = forWorldGameObject.gameObject.GetSMI<DebugGoToMonitor.Instance>();
			CreatureDebugGoToMonitor.Instance smi2 = forWorldGameObject.gameObject.GetSMI<CreatureDebugGoToMonitor.Instance>();
			if (!smi.IsNullOrDestroyed())
			{
				smi.GoToCell(this.destinationSimCellTarget.Unwrap().cellIndex);
			}
			else if (!smi2.IsNullOrDestroyed())
			{
				smi2.GoToCell(this.destinationSimCellTarget.Unwrap().cellIndex);
			}
			else
			{
				DebugUtil.DevLogError("No debug goto SMI found");
			}
		}
		if (this.shouldDrawBoundingBox)
		{
			Option<ValueTuple<Vector2, Vector2>> screenRect = forWorldGameObject.GetScreenRect();
			if (screenRect.IsSome())
			{
				DevToolEntity.DrawBoundingBox(screenRect.Unwrap(), "[Target]", ImGui.IsWindowFocused());
			}
			if (this.destinationSimCellTarget.IsSome())
			{
				Option<ValueTuple<Vector2, Vector2>> screenRect2 = this.destinationSimCellTarget.Unwrap().GetScreenRect();
				if (screenRect2.IsSome())
				{
					DevToolEntity.DrawBoundingBox(screenRect2.Unwrap(), "[Destination]", ImGui.IsWindowFocused());
				}
			}
		}
	}

	// Token: 0x06002842 RID: 10306 RVA: 0x000E5328 File Offset: 0x000E3528
	public static Option<string> GetErrorForCandidateTarget(DevToolEntityTarget uncastTarget)
	{
		if (!(uncastTarget is DevToolEntityTarget.ForWorldGameObject))
		{
			return "Target must be a world GameObject";
		}
		DevToolEntityTarget.ForWorldGameObject forWorldGameObject = (DevToolEntityTarget.ForWorldGameObject)uncastTarget;
		if (forWorldGameObject.gameObject.IsNullOrDestroyed())
		{
			return "Target GameObject is null or destroyed";
		}
		if (forWorldGameObject.gameObject.GetSMI<DebugGoToMonitor.Instance>().IsNullOrDestroyed() && forWorldGameObject.gameObject.GetSMI<CreatureDebugGoToMonitor.Instance>().IsNullOrDestroyed())
		{
			return "Target GameObject doesn't have either a DebugGoToMonitor or CreatureDebugGoToMonitor";
		}
		return Option.None;
	}

	// Token: 0x06002844 RID: 10308 RVA: 0x000E53B3 File Offset: 0x000E35B3
	[CompilerGenerated]
	internal static string <RenderTo>g__GetCellName|6_1(Option<DevToolEntityTarget.ForSimCell> target)
	{
		if (!target.IsNone())
		{
			return target.Unwrap().cellIndex.ToString();
		}
		return "<None>";
	}

	// Token: 0x04001797 RID: 6039
	private Option<DevToolEntityTarget.ForWorldGameObject> targetOpt;

	// Token: 0x04001798 RID: 6040
	private Option<DevToolEntityTarget.ForSimCell> destinationSimCellTarget;

	// Token: 0x04001799 RID: 6041
	private bool shouldDrawBoundingBox = true;

	// Token: 0x0400179A RID: 6042
	private bool shouldContinouslyRequest;
}
