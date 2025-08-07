using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using UnityEngine;

// Token: 0x02000CC6 RID: 3270
public class EntryDevLog
{
	// Token: 0x060064D0 RID: 25808 RVA: 0x0025E468 File Offset: 0x0025C668
	[Conditional("UNITY_EDITOR")]
	public void AddModificationRecord(EntryDevLog.ModificationRecord.ActionType actionType, string target, object newValue)
	{
		string text = this.TrimAuthor();
		this.modificationRecords.Add(new EntryDevLog.ModificationRecord(actionType, target, newValue, text));
	}

	// Token: 0x060064D1 RID: 25809 RVA: 0x0025E490 File Offset: 0x0025C690
	[Conditional("UNITY_EDITOR")]
	public void InsertModificationRecord(int index, EntryDevLog.ModificationRecord.ActionType actionType, string target, object newValue)
	{
		string text = this.TrimAuthor();
		this.modificationRecords.Insert(index, new EntryDevLog.ModificationRecord(actionType, target, newValue, text));
	}

	// Token: 0x060064D2 RID: 25810 RVA: 0x0025E4BC File Offset: 0x0025C6BC
	private string TrimAuthor()
	{
		string text = "";
		string[] array = new string[] { "Invoke", "CreateInstance", "AwakeInternal", "Internal", "<>", "YamlDotNet", "Deserialize" };
		string[] array2 = new string[]
		{
			".ctor", "Trigger", "AddContentContainerRange", "AddContentContainer", "InsertContentContainer", "KInstantiateUI", "Start", "InitializeComponentAwake", "TrimAuthor", "InsertModificationRecord",
			"AddModificationRecord", "SetValue", "Write"
		};
		StackTrace stackTrace = new StackTrace();
		int i = 0;
		int num = 0;
		int num2 = 3;
		while (i < num2)
		{
			num++;
			if (stackTrace.FrameCount <= num)
			{
				break;
			}
			MethodBase method = stackTrace.GetFrame(num).GetMethod();
			bool flag = false;
			for (int j = 0; j < array.Length; j++)
			{
				flag = flag || method.Name.Contains(array[j]);
			}
			for (int k = 0; k < array2.Length; k++)
			{
				flag = flag || method.Name.Contains(array2[k]);
			}
			if (!flag && !stackTrace.GetFrame(num).GetMethod().Name.StartsWith("set_") && !stackTrace.GetFrame(num).GetMethod().Name.StartsWith("Instantiate"))
			{
				if (i != 0)
				{
					text += " < ";
				}
				i++;
				text += stackTrace.GetFrame(num).GetMethod().Name;
			}
		}
		return text;
	}

	// Token: 0x040044D6 RID: 17622
	[SerializeField]
	public List<EntryDevLog.ModificationRecord> modificationRecords = new List<EntryDevLog.ModificationRecord>();

	// Token: 0x02001E8D RID: 7821
	public class ModificationRecord
	{
		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x0600B0BE RID: 45246 RVA: 0x003D3497 File Offset: 0x003D1697
		// (set) Token: 0x0600B0BF RID: 45247 RVA: 0x003D349F File Offset: 0x003D169F
		public EntryDevLog.ModificationRecord.ActionType actionType { get; private set; }

		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x0600B0C0 RID: 45248 RVA: 0x003D34A8 File Offset: 0x003D16A8
		// (set) Token: 0x0600B0C1 RID: 45249 RVA: 0x003D34B0 File Offset: 0x003D16B0
		public string target { get; private set; }

		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x0600B0C2 RID: 45250 RVA: 0x003D34B9 File Offset: 0x003D16B9
		// (set) Token: 0x0600B0C3 RID: 45251 RVA: 0x003D34C1 File Offset: 0x003D16C1
		public object newValue { get; private set; }

		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x0600B0C4 RID: 45252 RVA: 0x003D34CA File Offset: 0x003D16CA
		// (set) Token: 0x0600B0C5 RID: 45253 RVA: 0x003D34D2 File Offset: 0x003D16D2
		public string author { get; private set; }

		// Token: 0x0600B0C6 RID: 45254 RVA: 0x003D34DB File Offset: 0x003D16DB
		public ModificationRecord(EntryDevLog.ModificationRecord.ActionType actionType, string target, object newValue, string author)
		{
			this.target = target;
			this.newValue = newValue;
			this.author = author;
			this.actionType = actionType;
		}

		// Token: 0x020028FD RID: 10493
		public enum ActionType
		{
			// Token: 0x0400B585 RID: 46469
			Created,
			// Token: 0x0400B586 RID: 46470
			ChangeSubEntry,
			// Token: 0x0400B587 RID: 46471
			ChangeContent,
			// Token: 0x0400B588 RID: 46472
			ValueChange,
			// Token: 0x0400B589 RID: 46473
			YAMLData
		}
	}
}
