using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using ImGuiNET;
using Klei;
using STRINGS;
using UnityEngine;

// Token: 0x02000671 RID: 1649
public class DevToolManager
{
	// Token: 0x170001F1 RID: 497
	// (get) Token: 0x06002872 RID: 10354 RVA: 0x000E8319 File Offset: 0x000E6519
	public bool Show
	{
		get
		{
			return this.showImGui;
		}
	}

	// Token: 0x170001F2 RID: 498
	// (get) Token: 0x06002873 RID: 10355 RVA: 0x000E8321 File Offset: 0x000E6521
	private bool quickDevEnabled
	{
		get
		{
			return DebugHandler.enabled && GenericGameSettings.instance.quickDevTools;
		}
	}

	// Token: 0x06002874 RID: 10356 RVA: 0x000E8338 File Offset: 0x000E6538
	public DevToolManager()
	{
		DevToolManager.Instance = this;
		this.RegisterDevTool<DevToolSimDebug>("Debuggers/Sim Debug");
		this.RegisterDevTool<DevToolStateMachineDebug>("Debuggers/State Machine");
		this.RegisterDevTool<DevToolSaveGameInfo>("Debuggers/Save Game Info");
		this.RegisterDevTool<DevToolPerformanceInfo>("Debuggers/Performance Info");
		this.RegisterDevTool<DevToolPrintingPodDebug>("Debuggers/Printing Pod Debug");
		this.RegisterDevTool<DevToolBigBaseMutations>("Debuggers/Big Base Mutation Utilities");
		this.RegisterDevTool<DevToolNavGrid>("Debuggers/Nav Grid");
		this.RegisterDevTool<DevToolResearchDebugger>("Debuggers/Research");
		this.RegisterDevTool<DevToolStatusItems>("Debuggers/StatusItems");
		this.RegisterDevTool<DevToolUI>("Debuggers/UI");
		this.RegisterDevTool<DevToolUnlockedIds>("Debuggers/UnlockedIds List");
		this.RegisterDevTool<DevToolStringsTable>("Debuggers/StringsTable");
		this.RegisterDevTool<DevToolChoreDebugger>("Debuggers/Chore");
		this.RegisterDevTool<DevToolBatchedAnimDebug>("Debuggers/Batched Anim");
		this.RegisterDevTool<DevTool_StoryTraits_Reveal>("Debuggers/Story Traits Reveal");
		this.RegisterDevTool<DevTool_StoryTrait_CritterManipulator>("Debuggers/Story Trait - Critter Manipulator");
		this.RegisterDevTool<DevToolAnimEventManager>("Debuggers/Anim Event Manager");
		this.RegisterDevTool<DevToolSceneBrowser>("Scene/Browser");
		this.RegisterDevTool<DevToolSceneInspector>("Scene/Inspector");
		this.menuNodes.AddAction("Help/" + UI.FRONTEND.DEVTOOLS.TITLE.text, delegate
		{
			this.warning.ShouldDrawWindow = true;
		});
		this.RegisterDevTool<DevToolCommandPalette>("Help/Command Palette");
		this.RegisterAdditionalDevToolsByReflection();
	}

	// Token: 0x06002875 RID: 10357 RVA: 0x000E84A1 File Offset: 0x000E66A1
	public void Init()
	{
		this.UserAcceptedWarning = KPlayerPrefs.GetInt("ShowDevtools", 0) == 1;
	}

	// Token: 0x06002876 RID: 10358 RVA: 0x000E84B8 File Offset: 0x000E66B8
	private void RegisterDevTool<T>(string location) where T : DevTool, new()
	{
		this.menuNodes.AddAction(location, delegate
		{
			this.panels.AddPanelFor<T>();
		});
		this.dontAutomaticallyRegisterTypes.Add(typeof(T));
		this.devToolNameDict[typeof(T)] = Path.GetFileName(location);
	}

	// Token: 0x06002877 RID: 10359 RVA: 0x000E8510 File Offset: 0x000E6710
	private void RegisterAdditionalDevToolsByReflection()
	{
		using (List<Type>.Enumerator enumerator = ReflectionUtil.CollectTypesThatInheritOrImplement<DevTool>(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy).GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				Type type = enumerator.Current;
				if (!type.IsAbstract && !this.dontAutomaticallyRegisterTypes.Contains(type) && ReflectionUtil.HasDefaultConstructor(type))
				{
					this.menuNodes.AddAction("Debuggers/" + DevToolUtil.GenerateDevToolName(type), delegate
					{
						this.panels.AddPanelFor((DevTool)Activator.CreateInstance(type));
					});
				}
			}
		}
	}

	// Token: 0x06002878 RID: 10360 RVA: 0x000E85CC File Offset: 0x000E67CC
	public void UpdateShouldShowTools()
	{
		if (!DebugHandler.enabled)
		{
			this.showImGui = false;
			return;
		}
		bool flag = Input.GetKeyDown(KeyCode.BackQuote) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl));
		if (!this.toggleKeyWasDown && flag)
		{
			this.showImGui = !this.showImGui;
		}
		this.toggleKeyWasDown = flag;
	}

	// Token: 0x06002879 RID: 10361 RVA: 0x000E8634 File Offset: 0x000E6834
	public void UpdateTools()
	{
		if (!DebugHandler.enabled)
		{
			return;
		}
		if (this.showImGui)
		{
			if (this.warning.ShouldDrawWindow)
			{
				this.warning.DrawWindow(out this.warning.ShouldDrawWindow);
			}
			if (!this.UserAcceptedWarning)
			{
				this.warning.DrawMenuBar();
			}
			else
			{
				this.DrawMenu();
				this.panels.Render();
				if (this.showImguiState)
				{
					if (ImGui.Begin("ImGui state", ref this.showImguiState))
					{
						ImGui.Checkbox("ImGui.GetIO().WantCaptureMouse", ImGui.GetIO().WantCaptureMouse);
						ImGui.Checkbox("ImGui.GetIO().WantCaptureKeyboard", ImGui.GetIO().WantCaptureKeyboard);
					}
					ImGui.End();
				}
				if (this.showImguiDemo)
				{
					ImGui.ShowDemoWindow(ref this.showImguiDemo);
				}
			}
		}
		this.UpdateConsumingGameInputs();
		this.UpdateShortcuts();
	}

	// Token: 0x0600287A RID: 10362 RVA: 0x000E870B File Offset: 0x000E690B
	private void UpdateShortcuts()
	{
		if ((this.showImGui || this.quickDevEnabled) && this.UserAcceptedWarning)
		{
			this.<UpdateShortcuts>g__DoUpdate|26_0();
		}
	}

	// Token: 0x0600287B RID: 10363 RVA: 0x000E872C File Offset: 0x000E692C
	private void DrawMenu()
	{
		this.menuFontSize.InitializeIfNeeded();
		if (ImGui.BeginMainMenuBar())
		{
			this.menuNodes.Draw();
			this.menuFontSize.DrawMenu();
			if (ImGui.BeginMenu("IMGUI"))
			{
				ImGui.Checkbox("ImGui state", ref this.showImguiState);
				ImGui.Checkbox("ImGui Demo", ref this.showImguiDemo);
				ImGui.EndMenu();
			}
			ImGui.EndMainMenuBar();
		}
	}

	// Token: 0x0600287C RID: 10364 RVA: 0x000E879C File Offset: 0x000E699C
	private unsafe void UpdateConsumingGameInputs()
	{
		this.doesImGuiWantInput = false;
		if (this.showImGui)
		{
			this.doesImGuiWantInput = *ImGui.GetIO().WantCaptureMouse || *ImGui.GetIO().WantCaptureKeyboard;
			if (!this.prevDoesImGuiWantInput && this.doesImGuiWantInput)
			{
				DevToolManager.<UpdateConsumingGameInputs>g__OnInputEnterImGui|28_0();
			}
			if (this.prevDoesImGuiWantInput && !this.doesImGuiWantInput)
			{
				DevToolManager.<UpdateConsumingGameInputs>g__OnInputExitImGui|28_1();
			}
		}
		if (this.prevShowImGui && this.prevDoesImGuiWantInput && !this.showImGui)
		{
			DevToolManager.<UpdateConsumingGameInputs>g__OnInputExitImGui|28_1();
		}
		this.prevShowImGui = this.showImGui;
		this.prevDoesImGuiWantInput = this.doesImGuiWantInput;
		KInputManager.devToolFocus = this.showImGui && this.doesImGuiWantInput;
	}

	// Token: 0x0600287F RID: 10367 RVA: 0x000E8870 File Offset: 0x000E6A70
	[CompilerGenerated]
	private void <UpdateShortcuts>g__DoUpdate|26_0()
	{
		if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.Space))
		{
			DevToolCommandPalette.Init();
			this.showImGui = true;
		}
		if (Input.GetKeyDown(KeyCode.Comma))
		{
			DevToolUI.PingHoveredObject();
			this.showImGui = true;
		}
	}

	// Token: 0x06002880 RID: 10368 RVA: 0x000E88C0 File Offset: 0x000E6AC0
	[CompilerGenerated]
	internal static void <UpdateConsumingGameInputs>g__OnInputEnterImGui|28_0()
	{
		UnityMouseCatcherUI.SetEnabled(true);
		GameInputManager inputManager = Global.GetInputManager();
		for (int i = 0; i < inputManager.GetControllerCount(); i++)
		{
			inputManager.GetController(i).HandleCancelInput();
		}
	}

	// Token: 0x06002881 RID: 10369 RVA: 0x000E88F6 File Offset: 0x000E6AF6
	[CompilerGenerated]
	internal static void <UpdateConsumingGameInputs>g__OnInputExitImGui|28_1()
	{
		UnityMouseCatcherUI.SetEnabled(false);
	}

	// Token: 0x040017CB RID: 6091
	public const string SHOW_DEVTOOLS = "ShowDevtools";

	// Token: 0x040017CC RID: 6092
	public static DevToolManager Instance;

	// Token: 0x040017CD RID: 6093
	private bool toggleKeyWasDown;

	// Token: 0x040017CE RID: 6094
	private bool showImGui;

	// Token: 0x040017CF RID: 6095
	private bool prevShowImGui;

	// Token: 0x040017D0 RID: 6096
	private bool doesImGuiWantInput;

	// Token: 0x040017D1 RID: 6097
	private bool prevDoesImGuiWantInput;

	// Token: 0x040017D2 RID: 6098
	private bool showImguiState;

	// Token: 0x040017D3 RID: 6099
	private bool showImguiDemo;

	// Token: 0x040017D4 RID: 6100
	public bool UserAcceptedWarning;

	// Token: 0x040017D5 RID: 6101
	private DevToolWarning warning = new DevToolWarning();

	// Token: 0x040017D6 RID: 6102
	private DevToolMenuFontSize menuFontSize = new DevToolMenuFontSize();

	// Token: 0x040017D7 RID: 6103
	public DevPanelList panels = new DevPanelList();

	// Token: 0x040017D8 RID: 6104
	public DevToolMenuNodeList menuNodes = new DevToolMenuNodeList();

	// Token: 0x040017D9 RID: 6105
	public Dictionary<Type, string> devToolNameDict = new Dictionary<Type, string>();

	// Token: 0x040017DA RID: 6106
	private HashSet<Type> dontAutomaticallyRegisterTypes = new HashSet<Type>();
}
