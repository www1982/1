using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000516 RID: 1302
public class StateMachineDebuggerSettings : ScriptableObject
{
	// Token: 0x06001BE6 RID: 7142 RVA: 0x00097D05 File Offset: 0x00095F05
	public IEnumerator<StateMachineDebuggerSettings.Entry> GetEnumerator()
	{
		return this.entries.GetEnumerator();
	}

	// Token: 0x06001BE7 RID: 7143 RVA: 0x00097D17 File Offset: 0x00095F17
	public static StateMachineDebuggerSettings Get()
	{
		if (StateMachineDebuggerSettings._Instance == null)
		{
			StateMachineDebuggerSettings._Instance = Resources.Load<StateMachineDebuggerSettings>("StateMachineDebuggerSettings");
			StateMachineDebuggerSettings._Instance.Initialize();
		}
		return StateMachineDebuggerSettings._Instance;
	}

	// Token: 0x06001BE8 RID: 7144 RVA: 0x00097D44 File Offset: 0x00095F44
	private void Initialize()
	{
		foreach (Type type in App.GetCurrentDomainTypes())
		{
			if (typeof(StateMachine).IsAssignableFrom(type))
			{
				this.CreateEntry(type);
			}
		}
		this.entries.RemoveAll((StateMachineDebuggerSettings.Entry x) => x.type == null);
	}

	// Token: 0x06001BE9 RID: 7145 RVA: 0x00097DD4 File Offset: 0x00095FD4
	public StateMachineDebuggerSettings.Entry CreateEntry(Type type)
	{
		foreach (StateMachineDebuggerSettings.Entry entry in this.entries)
		{
			if (type.FullName == entry.typeName)
			{
				entry.type = type;
				return entry;
			}
		}
		StateMachineDebuggerSettings.Entry entry2 = new StateMachineDebuggerSettings.Entry(type);
		this.entries.Add(entry2);
		return entry2;
	}

	// Token: 0x06001BEA RID: 7146 RVA: 0x00097E54 File Offset: 0x00096054
	public void Clear()
	{
		this.entries.Clear();
		this.Initialize();
	}

	// Token: 0x04001061 RID: 4193
	public List<StateMachineDebuggerSettings.Entry> entries = new List<StateMachineDebuggerSettings.Entry>();

	// Token: 0x04001062 RID: 4194
	private static StateMachineDebuggerSettings _Instance;

	// Token: 0x02001376 RID: 4982
	[Serializable]
	public class Entry
	{
		// Token: 0x06008A9A RID: 35482 RVA: 0x003506E8 File Offset: 0x0034E8E8
		public Entry(Type type)
		{
			this.typeName = type.FullName;
			this.type = type;
		}

		// Token: 0x0400695F RID: 26975
		public Type type;

		// Token: 0x04006960 RID: 26976
		public string typeName;

		// Token: 0x04006961 RID: 26977
		public bool breakOnGoTo;

		// Token: 0x04006962 RID: 26978
		public bool enableConsoleLogging;

		// Token: 0x04006963 RID: 26979
		public bool saveHistory;
	}
}
