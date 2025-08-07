using System;
using System.Collections.Generic;
using System.Linq;
using ImGuiNET;
using UnityEngine;

// Token: 0x02000665 RID: 1637
public class DevToolCommandPalette : DevTool
{
	// Token: 0x06002828 RID: 10280 RVA: 0x000E428D File Offset: 0x000E248D
	public DevToolCommandPalette()
		: this(null)
	{
	}

	// Token: 0x06002829 RID: 10281 RVA: 0x000E4298 File Offset: 0x000E2498
	public DevToolCommandPalette(List<DevToolCommandPalette.Command> commands = null)
	{
		this.drawFlags |= ImGuiWindowFlags.NoResize;
		this.drawFlags |= ImGuiWindowFlags.NoScrollbar;
		this.drawFlags |= ImGuiWindowFlags.NoScrollWithMouse;
		if (commands == null)
		{
			this.commands.allValues = DevToolCommandPaletteUtil.GenerateDefaultCommandPalette();
			return;
		}
		this.commands.allValues = commands;
	}

	// Token: 0x0600282A RID: 10282 RVA: 0x000E4327 File Offset: 0x000E2527
	public static void Init()
	{
		DevToolCommandPalette.InitWithCommands(DevToolCommandPaletteUtil.GenerateDefaultCommandPalette());
	}

	// Token: 0x0600282B RID: 10283 RVA: 0x000E4333 File Offset: 0x000E2533
	public static void InitWithCommands(List<DevToolCommandPalette.Command> commands)
	{
		DevToolManager.Instance.panels.AddPanelFor(new DevToolCommandPalette(commands));
	}

	// Token: 0x0600282C RID: 10284 RVA: 0x000E434C File Offset: 0x000E254C
	protected override void RenderTo(DevPanel panel)
	{
		DevToolCommandPalette.Resize(panel);
		if (this.commands.allValues == null)
		{
			ImGui.Text("No commands list given");
			return;
		}
		if (this.commands.allValues.Count == 0)
		{
			ImGui.Text("Given command list is empty, no results to show.");
			return;
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			panel.Close();
			return;
		}
		if (!ImGui.IsWindowFocused(ImGuiFocusedFlags.ChildWindows))
		{
			panel.Close();
			return;
		}
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			this.m_selected_index--;
			this.shouldScrollToSelectedCommandFlag = true;
		}
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			this.m_selected_index++;
			this.shouldScrollToSelectedCommandFlag = true;
		}
		if (this.commands.filteredValues.Count > 0)
		{
			while (this.m_selected_index < 0)
			{
				this.m_selected_index += this.commands.filteredValues.Count;
			}
			this.m_selected_index %= this.commands.filteredValues.Count;
		}
		else
		{
			this.m_selected_index = 0;
		}
		DevToolCommandPalette.Command command = null;
		if ((Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.KeypadEnter)) && this.commands.filteredValues.Count > 0 && command == null)
		{
			command = this.commands.filteredValues[this.m_selected_index];
		}
		if (this.m_should_focus_search)
		{
			ImGui.SetKeyboardFocusHere();
		}
		if (ImGui.InputText("Filter", ref this.commands.filter, 30U) || this.m_should_focus_search)
		{
			this.commands.Refilter();
		}
		this.m_should_focus_search = false;
		ImGui.Separator();
		string text = "Up arrow & down arrow to navigate. Enter to select. ";
		if (this.commands.filteredValues.Count > 0 && this.commands.didUseFilter)
		{
			text += string.Format("Found {0} Results", this.commands.filteredValues.Count);
		}
		ImGui.Text(text);
		ImGui.Separator();
		if (ImGui.BeginChild("ID_scroll_region"))
		{
			if (this.commands.filteredValues.Count <= 0)
			{
				ImGui.Text("Couldn't find anything that matches \"" + this.commands.filter + "\", maybe it hasn't been added yet?");
			}
			else
			{
				for (int i = 0; i < this.commands.filteredValues.Count; i++)
				{
					DevToolCommandPalette.Command command2 = this.commands.filteredValues[i];
					bool flag = i == this.m_selected_index;
					ImGui.PushID(i);
					bool flag2;
					if (flag)
					{
						flag2 = ImGui.Selectable("> " + command2.display_name, flag);
					}
					else
					{
						flag2 = ImGui.Selectable("  " + command2.display_name, flag);
					}
					ImGui.PopID();
					if (this.shouldScrollToSelectedCommandFlag && flag)
					{
						this.shouldScrollToSelectedCommandFlag = false;
						ImGui.SetScrollHereY(0.5f);
					}
					if (flag2 && command == null)
					{
						command = command2;
					}
				}
			}
		}
		ImGui.EndChild();
		if (command != null)
		{
			command.Internal_Select();
			panel.Close();
		}
	}

	// Token: 0x0600282D RID: 10285 RVA: 0x000E463C File Offset: 0x000E283C
	private static void Resize(DevPanel devToolPanel)
	{
		float num = 800f;
		float num2 = 400f;
		Rect rect = new Rect(0f, 0f, (float)Screen.width, (float)Screen.height);
		Rect rect2 = new Rect
		{
			x = rect.x + rect.width / 2f - num / 2f,
			y = rect.y + rect.height / 2f - num2 / 2f,
			width = num,
			height = num2
		};
		devToolPanel.SetPosition(rect2.position, ImGuiCond.None);
		devToolPanel.SetSize(rect2.size, ImGuiCond.None);
	}

	// Token: 0x04001791 RID: 6033
	private int m_selected_index;

	// Token: 0x04001792 RID: 6034
	private StringSearchableList<DevToolCommandPalette.Command> commands = new StringSearchableList<DevToolCommandPalette.Command>(delegate(DevToolCommandPalette.Command command, in string filter)
	{
		return !StringSearchableListUtil.DoAnyTagsMatchFilter(command.tags, in filter);
	});

	// Token: 0x04001793 RID: 6035
	private bool m_should_focus_search = true;

	// Token: 0x04001794 RID: 6036
	private bool shouldScrollToSelectedCommandFlag;

	// Token: 0x020014EB RID: 5355
	public class Command
	{
		// Token: 0x06008F4E RID: 36686 RVA: 0x0035D4FD File Offset: 0x0035B6FD
		public Command(string primary_tag, global::System.Action on_select)
			: this(new string[] { primary_tag }, on_select)
		{
		}

		// Token: 0x06008F4F RID: 36687 RVA: 0x0035D510 File Offset: 0x0035B710
		public Command(string primary_tag, string tag_a, global::System.Action on_select)
			: this(new string[] { primary_tag, tag_a }, on_select)
		{
		}

		// Token: 0x06008F50 RID: 36688 RVA: 0x0035D527 File Offset: 0x0035B727
		public Command(string primary_tag, string tag_a, string tag_b, global::System.Action on_select)
			: this(new string[] { primary_tag, tag_a, tag_b }, on_select)
		{
		}

		// Token: 0x06008F51 RID: 36689 RVA: 0x0035D543 File Offset: 0x0035B743
		public Command(string primary_tag, string tag_a, string tag_b, string tag_c, global::System.Action on_select)
			: this(new string[] { primary_tag, tag_a, tag_b, tag_c }, on_select)
		{
		}

		// Token: 0x06008F52 RID: 36690 RVA: 0x0035D564 File Offset: 0x0035B764
		public Command(string primary_tag, string tag_a, string tag_b, string tag_c, string tag_d, global::System.Action on_select)
			: this(new string[] { primary_tag, tag_a, tag_b, tag_c, tag_d }, on_select)
		{
		}

		// Token: 0x06008F53 RID: 36691 RVA: 0x0035D58A File Offset: 0x0035B78A
		public Command(string primary_tag, string tag_a, string tag_b, string tag_c, string tag_d, string tag_e, global::System.Action on_select)
			: this(new string[] { primary_tag, tag_a, tag_b, tag_c, tag_d, tag_e }, on_select)
		{
		}

		// Token: 0x06008F54 RID: 36692 RVA: 0x0035D5B5 File Offset: 0x0035B7B5
		public Command(string primary_tag, string tag_a, string tag_b, string tag_c, string tag_d, string tag_e, string tag_f, global::System.Action on_select)
			: this(new string[] { primary_tag, tag_a, tag_b, tag_c, tag_d, tag_e, tag_f }, on_select)
		{
		}

		// Token: 0x06008F55 RID: 36693 RVA: 0x0035D5E5 File Offset: 0x0035B7E5
		public Command(string primary_tag, string[] additional_tags, global::System.Action on_select)
			: this(new string[] { primary_tag }.Concat(additional_tags).ToArray<string>(), on_select)
		{
		}

		// Token: 0x06008F56 RID: 36694 RVA: 0x0035D604 File Offset: 0x0035B804
		public Command(string[] tags, global::System.Action on_select)
		{
			this.display_name = tags[0];
			this.tags = tags.Select((string t) => t.ToLowerInvariant()).ToArray<string>();
			this.m_on_select = on_select;
		}

		// Token: 0x06008F57 RID: 36695 RVA: 0x0035D657 File Offset: 0x0035B857
		public void Internal_Select()
		{
			this.m_on_select();
		}

		// Token: 0x04006E4B RID: 28235
		public string display_name;

		// Token: 0x04006E4C RID: 28236
		public string[] tags;

		// Token: 0x04006E4D RID: 28237
		private global::System.Action m_on_select;
	}
}
