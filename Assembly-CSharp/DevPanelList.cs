using System;
using System.Collections.Generic;

// Token: 0x0200065D RID: 1629
public class DevPanelList
{
	// Token: 0x060027F8 RID: 10232 RVA: 0x000E26A0 File Offset: 0x000E08A0
	public DevPanel AddPanelFor<T>() where T : DevTool, new()
	{
		return this.AddPanelFor(new T());
	}

	// Token: 0x060027F9 RID: 10233 RVA: 0x000E26B4 File Offset: 0x000E08B4
	public DevPanel AddPanelFor(DevTool devTool)
	{
		DevPanel devPanel = new DevPanel(devTool, this);
		this.activePanels.Add(devPanel);
		return devPanel;
	}

	// Token: 0x060027FA RID: 10234 RVA: 0x000E26D8 File Offset: 0x000E08D8
	public Option<T> GetDevTool<T>() where T : DevTool
	{
		foreach (DevPanel devPanel in this.activePanels)
		{
			T t = devPanel.GetCurrentDevTool() as T;
			if (t != null)
			{
				return t;
			}
		}
		return Option.None;
	}

	// Token: 0x060027FB RID: 10235 RVA: 0x000E2750 File Offset: 0x000E0950
	public T AddOrGetDevTool<T>() where T : DevTool, new()
	{
		bool flag;
		T t;
		this.GetDevTool<T>().Deconstruct(out flag, out t);
		bool flag2 = flag;
		T t2 = t;
		if (!flag2)
		{
			t2 = new T();
			this.AddPanelFor(t2);
		}
		return t2;
	}

	// Token: 0x060027FC RID: 10236 RVA: 0x000E2788 File Offset: 0x000E0988
	public void ClosePanel(DevPanel panel)
	{
		if (this.activePanels.Remove(panel))
		{
			panel.Internal_Uninit();
		}
	}

	// Token: 0x060027FD RID: 10237 RVA: 0x000E27A0 File Offset: 0x000E09A0
	public void Render()
	{
		if (this.activePanels.Count == 0)
		{
			return;
		}
		using (ListPool<DevPanel, DevPanelList>.PooledList pooledList = ListPool<DevPanel, DevPanelList>.Allocate())
		{
			for (int i = 0; i < this.activePanels.Count; i++)
			{
				DevPanel devPanel = this.activePanels[i];
				devPanel.RenderPanel();
				if (devPanel.isRequestingToClose)
				{
					pooledList.Add(devPanel);
				}
			}
			foreach (DevPanel devPanel2 in pooledList)
			{
				this.ClosePanel(devPanel2);
			}
		}
	}

	// Token: 0x060027FE RID: 10238 RVA: 0x000E2854 File Offset: 0x000E0A54
	public void Internal_InitPanelId(Type initialDevToolType, out string panelId, out uint idPostfixNumber)
	{
		idPostfixNumber = this.Internal_GetUniqueIdPostfix(initialDevToolType);
		panelId = initialDevToolType.Name + idPostfixNumber.ToString();
	}

	// Token: 0x060027FF RID: 10239 RVA: 0x000E2874 File Offset: 0x000E0A74
	public uint Internal_GetUniqueIdPostfix(Type initialDevToolType)
	{
		uint num3;
		using (HashSetPool<uint, DevPanelList>.PooledHashSet pooledHashSet = HashSetPool<uint, DevPanelList>.Allocate())
		{
			foreach (DevPanel devPanel in this.activePanels)
			{
				if (!(devPanel.initialDevToolType != initialDevToolType))
				{
					pooledHashSet.Add(devPanel.idPostfixNumber);
				}
			}
			for (uint num = 0U; num < 100U; num += 1U)
			{
				if (!pooledHashSet.Contains(num))
				{
					return num;
				}
			}
			Debug.Assert(false, "Something went wrong, this should only assert if there's over 100 of the same type of debug window");
			uint num2 = this.fallbackUniqueIdPostfixNumber;
			this.fallbackUniqueIdPostfixNumber = num2 + 1U;
			num3 = num2;
		}
		return num3;
	}

	// Token: 0x04001779 RID: 6009
	private List<DevPanel> activePanels = new List<DevPanel>();

	// Token: 0x0400177A RID: 6010
	private uint fallbackUniqueIdPostfixNumber = 300U;
}
