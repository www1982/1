using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using ImGuiNET;
using UnityEngine;

// Token: 0x02000664 RID: 1636
public class DevToolChoreDebugger : DevTool
{
	// Token: 0x06002821 RID: 10273 RVA: 0x000E3BF5 File Offset: 0x000E1DF5
	protected override void RenderTo(DevPanel panel)
	{
		this.Update();
	}

	// Token: 0x06002822 RID: 10274 RVA: 0x000E3C00 File Offset: 0x000E1E00
	public void Update()
	{
		if (!Application.isPlaying || SelectTool.Instance == null || SelectTool.Instance.selected == null || SelectTool.Instance.selected.gameObject == null)
		{
			return;
		}
		GameObject gameObject = SelectTool.Instance.selected.gameObject;
		if (this.Consumer == null || (!this.lockSelection && this.selectedGameObject != gameObject))
		{
			this.Consumer = gameObject.GetComponent<ChoreConsumer>();
			this.selectedGameObject = gameObject;
		}
		if (this.Consumer != null)
		{
			ImGui.InputText("Filter:", ref this.filter, 256U);
			this.DisplayAvailableChores();
			ImGui.Text("");
		}
	}

	// Token: 0x06002823 RID: 10275 RVA: 0x000E3CC8 File Offset: 0x000E1EC8
	private void DisplayAvailableChores()
	{
		ImGui.Checkbox("Lock selection", ref this.lockSelection);
		ImGui.Checkbox("Show Last Successful Chore Selection", ref this.showLastSuccessfulPreconditionSnapshot);
		ImGui.Text("Available Chores:");
		ChoreConsumer.PreconditionSnapshot preconditionSnapshot = this.Consumer.GetLastPreconditionSnapshot();
		if (this.showLastSuccessfulPreconditionSnapshot)
		{
			preconditionSnapshot = this.Consumer.GetLastSuccessfulPreconditionSnapshot();
		}
		this.ShowChores(preconditionSnapshot);
	}

	// Token: 0x06002824 RID: 10276 RVA: 0x000E3D28 File Offset: 0x000E1F28
	private void ShowChores(ChoreConsumer.PreconditionSnapshot target_snapshot)
	{
		ImGuiTableFlags imGuiTableFlags = ImGuiTableFlags.RowBg | ImGuiTableFlags.BordersInnerH | ImGuiTableFlags.BordersOuterH | ImGuiTableFlags.BordersInnerV | ImGuiTableFlags.BordersOuterV | ImGuiTableFlags.SizingFixedFit | ImGuiTableFlags.ScrollX | ImGuiTableFlags.ScrollY;
		this.rowIndex = 0;
		if (ImGui.BeginTable("Available Chores", this.columns.Count, imGuiTableFlags))
		{
			foreach (object obj in this.columns.Keys)
			{
				ImGui.TableSetupColumn(obj.ToString(), ImGuiTableColumnFlags.WidthFixed);
			}
			ImGui.TableHeadersRow();
			for (int i = target_snapshot.succeededContexts.Count - 1; i >= 0; i--)
			{
				this.ShowContext(target_snapshot.succeededContexts[i]);
			}
			if (target_snapshot.doFailedContextsNeedSorting)
			{
				target_snapshot.failedContexts.Sort();
				target_snapshot.doFailedContextsNeedSorting = false;
			}
			for (int j = target_snapshot.failedContexts.Count - 1; j >= 0; j--)
			{
				this.ShowContext(target_snapshot.failedContexts[j]);
			}
			ImGui.EndTable();
		}
	}

	// Token: 0x06002825 RID: 10277 RVA: 0x000E3E2C File Offset: 0x000E202C
	private void ShowContext(Chore.Precondition.Context context)
	{
		string text = "";
		Chore chore = context.chore;
		if (!context.IsSuccess())
		{
			text = context.chore.GetPreconditions()[context.failedPreconditionId].condition.id;
		}
		string text2 = "";
		if (chore.driver != null)
		{
			text2 = chore.driver.name;
		}
		string text3 = "";
		if (chore.overrideTarget != null)
		{
			text3 = chore.overrideTarget.name;
		}
		string text4 = "";
		if (!chore.isNull)
		{
			text4 = chore.gameObject.name;
		}
		if (Chore.Precondition.Context.ShouldFilter(this.filter, chore.GetType().ToString()) && Chore.Precondition.Context.ShouldFilter(this.filter, chore.choreType.Id) && Chore.Precondition.Context.ShouldFilter(this.filter, text) && Chore.Precondition.Context.ShouldFilter(this.filter, text2) && Chore.Precondition.Context.ShouldFilter(this.filter, text3) && Chore.Precondition.Context.ShouldFilter(this.filter, text4))
		{
			return;
		}
		this.columns["Id"] = chore.id.ToString();
		this.columns["Class"] = chore.GetType().ToString().Replace("`1", "");
		this.columns["Type"] = chore.choreType.Id;
		this.columns["PriorityClass"] = context.masterPriority.priority_class.ToString();
		this.columns["PersonalPriority"] = context.personalPriority.ToString();
		this.columns["PriorityValue"] = context.masterPriority.priority_value.ToString();
		this.columns["Priority"] = context.priority.ToString();
		this.columns["PriorityMod"] = context.priorityMod.ToString();
		this.columns["ConsumerPriority"] = context.consumerPriority.ToString();
		this.columns["Cost"] = context.cost.ToString();
		this.columns["Interrupt"] = context.interruptPriority.ToString();
		this.columns["Precondition"] = text;
		this.columns["Override"] = text3;
		this.columns["Assigned To"] = text2;
		this.columns["Owner"] = text4;
		this.columns["Details"] = "";
		ImGui.TableNextRow();
		string text5 = "ID_row_{0}";
		int num = this.rowIndex;
		this.rowIndex = num + 1;
		ImGui.PushID(string.Format(text5, num));
		for (int i = 0; i < this.columns.Count; i++)
		{
			ImGui.TableSetColumnIndex(i);
			ImGui.Text(this.columns[i].ToString());
		}
		ImGui.PopID();
	}

	// Token: 0x06002826 RID: 10278 RVA: 0x000E414F File Offset: 0x000E234F
	public void ConsumerDebugDisplayLog()
	{
	}

	// Token: 0x0400178A RID: 6026
	private string filter = "";

	// Token: 0x0400178B RID: 6027
	private bool showLastSuccessfulPreconditionSnapshot;

	// Token: 0x0400178C RID: 6028
	private bool lockSelection;

	// Token: 0x0400178D RID: 6029
	private ChoreConsumer Consumer;

	// Token: 0x0400178E RID: 6030
	private GameObject selectedGameObject;

	// Token: 0x0400178F RID: 6031
	private OrderedDictionary columns = new OrderedDictionary
	{
		{ "BP", "" },
		{ "Id", "" },
		{ "Class", "" },
		{ "Type", "" },
		{ "PriorityClass", "" },
		{ "PersonalPriority", "" },
		{ "PriorityValue", "" },
		{ "Priority", "" },
		{ "PriorityMod", "" },
		{ "ConsumerPriority", "" },
		{ "Cost", "" },
		{ "Interrupt", "" },
		{ "Precondition", "" },
		{ "Override", "" },
		{ "Assigned To", "" },
		{ "Owner", "" },
		{ "Details", "" }
	};

	// Token: 0x04001790 RID: 6032
	private int rowIndex;

	// Token: 0x020014EA RID: 5354
	public class EditorPreconditionSnapshot
	{
		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x06008F49 RID: 36681 RVA: 0x0035D4D3 File Offset: 0x0035B6D3
		// (set) Token: 0x06008F4A RID: 36682 RVA: 0x0035D4DB File Offset: 0x0035B6DB
		public List<DevToolChoreDebugger.EditorPreconditionSnapshot.EditorContext> SucceededContexts { get; set; }

		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x06008F4B RID: 36683 RVA: 0x0035D4E4 File Offset: 0x0035B6E4
		// (set) Token: 0x06008F4C RID: 36684 RVA: 0x0035D4EC File Offset: 0x0035B6EC
		public List<DevToolChoreDebugger.EditorPreconditionSnapshot.EditorContext> FailedContexts { get; set; }

		// Token: 0x0200274C RID: 10060
		public struct EditorContext
		{
			// Token: 0x17000CD2 RID: 3282
			// (get) Token: 0x0600C662 RID: 50786 RVA: 0x004125FE File Offset: 0x004107FE
			// (set) Token: 0x0600C663 RID: 50787 RVA: 0x00412606 File Offset: 0x00410806
			public string Chore { readonly get; set; }

			// Token: 0x17000CD3 RID: 3283
			// (get) Token: 0x0600C664 RID: 50788 RVA: 0x0041260F File Offset: 0x0041080F
			// (set) Token: 0x0600C665 RID: 50789 RVA: 0x00412617 File Offset: 0x00410817
			public string ChoreType { readonly get; set; }

			// Token: 0x17000CD4 RID: 3284
			// (get) Token: 0x0600C666 RID: 50790 RVA: 0x00412620 File Offset: 0x00410820
			// (set) Token: 0x0600C667 RID: 50791 RVA: 0x00412628 File Offset: 0x00410828
			public string FailedPrecondition { readonly get; set; }

			// Token: 0x17000CD5 RID: 3285
			// (get) Token: 0x0600C668 RID: 50792 RVA: 0x00412631 File Offset: 0x00410831
			// (set) Token: 0x0600C669 RID: 50793 RVA: 0x00412639 File Offset: 0x00410839
			public int WorldId { readonly get; set; }
		}
	}
}
