using System;
using System.Collections.Generic;
using ImGuiNET;
using UnityEngine;

// Token: 0x0200065C RID: 1628
public class DevPanel
{
	// Token: 0x170001EC RID: 492
	// (get) Token: 0x060027E1 RID: 10209 RVA: 0x000E21C0 File Offset: 0x000E03C0
	// (set) Token: 0x060027E2 RID: 10210 RVA: 0x000E21C8 File Offset: 0x000E03C8
	public bool isRequestingToClose { get; private set; }

	// Token: 0x170001ED RID: 493
	// (get) Token: 0x060027E3 RID: 10211 RVA: 0x000E21D1 File Offset: 0x000E03D1
	// (set) Token: 0x060027E4 RID: 10212 RVA: 0x000E21D9 File Offset: 0x000E03D9
	public Option<ValueTuple<Vector2, ImGuiCond>> nextImGuiWindowPosition { get; private set; }

	// Token: 0x170001EE RID: 494
	// (get) Token: 0x060027E5 RID: 10213 RVA: 0x000E21E2 File Offset: 0x000E03E2
	// (set) Token: 0x060027E6 RID: 10214 RVA: 0x000E21EA File Offset: 0x000E03EA
	public Option<ValueTuple<Vector2, ImGuiCond>> nextImGuiWindowSize { get; private set; }

	// Token: 0x060027E7 RID: 10215 RVA: 0x000E21F4 File Offset: 0x000E03F4
	public DevPanel(DevTool devTool, DevPanelList manager)
	{
		this.manager = manager;
		this.devTools = new List<DevTool>();
		this.devTools.Add(devTool);
		this.currentDevToolIndex = 0;
		this.initialDevToolType = devTool.GetType();
		manager.Internal_InitPanelId(this.initialDevToolType, out this.uniquePanelId, out this.idPostfixNumber);
	}

	// Token: 0x060027E8 RID: 10216 RVA: 0x000E2250 File Offset: 0x000E0450
	public void PushValue<T>(T value) where T : class
	{
		this.PushDevTool(new DevToolObjectViewer<T>(() => value));
	}

	// Token: 0x060027E9 RID: 10217 RVA: 0x000E2281 File Offset: 0x000E0481
	public void PushValue<T>(Func<T> value)
	{
		this.PushDevTool(new DevToolObjectViewer<T>(value));
	}

	// Token: 0x060027EA RID: 10218 RVA: 0x000E228F File Offset: 0x000E048F
	public void PushDevTool<T>() where T : DevTool, new()
	{
		this.PushDevTool(new T());
	}

	// Token: 0x060027EB RID: 10219 RVA: 0x000E22A4 File Offset: 0x000E04A4
	public void PushDevTool(DevTool devTool)
	{
		if (Input.GetKey(KeyCode.LeftShift))
		{
			this.manager.AddPanelFor(devTool);
			return;
		}
		for (int i = this.devTools.Count - 1; i > this.currentDevToolIndex; i--)
		{
			this.devTools[i].Internal_Uninit();
			this.devTools.RemoveAt(i);
		}
		this.devTools.Add(devTool);
		this.currentDevToolIndex = this.devTools.Count - 1;
	}

	// Token: 0x060027EC RID: 10220 RVA: 0x000E2324 File Offset: 0x000E0524
	public bool NavGoBack()
	{
		Option<int> option = this.TryGetDevToolIndexByOffset(-1);
		if (option.IsNone())
		{
			return false;
		}
		this.currentDevToolIndex = option.Unwrap();
		return true;
	}

	// Token: 0x060027ED RID: 10221 RVA: 0x000E2354 File Offset: 0x000E0554
	public bool NavGoForward()
	{
		Option<int> option = this.TryGetDevToolIndexByOffset(1);
		if (option.IsNone())
		{
			return false;
		}
		this.currentDevToolIndex = option.Unwrap();
		return true;
	}

	// Token: 0x060027EE RID: 10222 RVA: 0x000E2382 File Offset: 0x000E0582
	public DevTool GetCurrentDevTool()
	{
		return this.devTools[this.currentDevToolIndex];
	}

	// Token: 0x060027EF RID: 10223 RVA: 0x000E2398 File Offset: 0x000E0598
	public Option<int> TryGetDevToolIndexByOffset(int offsetFromCurrentIndex)
	{
		int num = this.currentDevToolIndex + offsetFromCurrentIndex;
		if (num < 0)
		{
			return Option.None;
		}
		if (num >= this.devTools.Count)
		{
			return Option.None;
		}
		return num;
	}

	// Token: 0x060027F0 RID: 10224 RVA: 0x000E23DC File Offset: 0x000E05DC
	public void RenderPanel()
	{
		DevTool currentDevTool = this.GetCurrentDevTool();
		currentDevTool.Internal_TryInit();
		if (currentDevTool.isRequestingToClosePanel)
		{
			this.isRequestingToClose = true;
			return;
		}
		ImGuiWindowFlags imGuiWindowFlags;
		this.ConfigureImGuiWindowFor(currentDevTool, out imGuiWindowFlags);
		currentDevTool.Internal_Update();
		bool flag = true;
		if (ImGui.Begin(currentDevTool.Name + "###ID_" + this.uniquePanelId, ref flag, imGuiWindowFlags))
		{
			if (!flag)
			{
				this.isRequestingToClose = true;
				ImGui.End();
				return;
			}
			if (ImGui.BeginMenuBar())
			{
				this.DrawNavigation();
				ImGui.SameLine(0f, 20f);
				this.DrawMenuBarContents();
				ImGui.EndMenuBar();
			}
			currentDevTool.DoImGui(this);
			if (this.GetCurrentDevTool() != currentDevTool)
			{
				ImGui.SetScrollY(0f);
			}
		}
		ImGui.End();
		if (this.GetCurrentDevTool().isRequestingToClosePanel)
		{
			this.isRequestingToClose = true;
		}
	}

	// Token: 0x060027F1 RID: 10225 RVA: 0x000E24A4 File Offset: 0x000E06A4
	private void DrawNavigation()
	{
		Option<int> option = this.TryGetDevToolIndexByOffset(-1);
		if (ImGuiEx.Button(" < ", option.IsSome()))
		{
			this.currentDevToolIndex = option.Unwrap();
		}
		if (option.IsSome())
		{
			ImGuiEx.TooltipForPrevious("Go back to " + this.devTools[option.Unwrap()].Name);
		}
		else
		{
			ImGuiEx.TooltipForPrevious("Go back");
		}
		ImGui.SameLine(0f, 5f);
		Option<int> option2 = this.TryGetDevToolIndexByOffset(1);
		if (ImGuiEx.Button(" > ", option2.IsSome()))
		{
			this.currentDevToolIndex = option2.Unwrap();
		}
		if (option2.IsSome())
		{
			ImGuiEx.TooltipForPrevious("Go forward to " + this.devTools[option2.Unwrap()].Name);
			return;
		}
		ImGuiEx.TooltipForPrevious("Go forward");
	}

	// Token: 0x060027F2 RID: 10226 RVA: 0x000E2585 File Offset: 0x000E0785
	private void DrawMenuBarContents()
	{
	}

	// Token: 0x060027F3 RID: 10227 RVA: 0x000E2588 File Offset: 0x000E0788
	private void ConfigureImGuiWindowFor(DevTool currentDevTool, out ImGuiWindowFlags drawFlags)
	{
		drawFlags = ImGuiWindowFlags.MenuBar | currentDevTool.drawFlags;
		if (this.nextImGuiWindowPosition.HasValue)
		{
			ValueTuple<Vector2, ImGuiCond> value = this.nextImGuiWindowPosition.Value;
			Vector2 item = value.Item1;
			ImGuiCond item2 = value.Item2;
			ImGui.SetNextWindowPos(item, item2);
			this.nextImGuiWindowPosition = default(Option<ValueTuple<Vector2, ImGuiCond>>);
		}
		if (this.nextImGuiWindowSize.HasValue)
		{
			Vector2 item3 = this.nextImGuiWindowSize.Value.Item1;
			ImGui.SetNextWindowSize(item3);
			this.nextImGuiWindowSize = default(Option<ValueTuple<Vector2, ImGuiCond>>);
		}
	}

	// Token: 0x060027F4 RID: 10228 RVA: 0x000E261F File Offset: 0x000E081F
	public void SetPosition(Vector2 position, ImGuiCond condition = ImGuiCond.None)
	{
		this.nextImGuiWindowPosition = new ValueTuple<Vector2, ImGuiCond>(position, condition);
	}

	// Token: 0x060027F5 RID: 10229 RVA: 0x000E2633 File Offset: 0x000E0833
	public void SetSize(Vector2 size, ImGuiCond condition = ImGuiCond.None)
	{
		this.nextImGuiWindowSize = new ValueTuple<Vector2, ImGuiCond>(size, condition);
	}

	// Token: 0x060027F6 RID: 10230 RVA: 0x000E2647 File Offset: 0x000E0847
	public void Close()
	{
		this.isRequestingToClose = true;
	}

	// Token: 0x060027F7 RID: 10231 RVA: 0x000E2650 File Offset: 0x000E0850
	public void Internal_Uninit()
	{
		foreach (DevTool devTool in this.devTools)
		{
			devTool.Internal_Uninit();
		}
	}

	// Token: 0x04001773 RID: 6003
	public readonly string uniquePanelId;

	// Token: 0x04001774 RID: 6004
	public readonly DevPanelList manager;

	// Token: 0x04001775 RID: 6005
	public readonly Type initialDevToolType;

	// Token: 0x04001776 RID: 6006
	public readonly uint idPostfixNumber;

	// Token: 0x04001777 RID: 6007
	private List<DevTool> devTools;

	// Token: 0x04001778 RID: 6008
	private int currentDevToolIndex;
}
