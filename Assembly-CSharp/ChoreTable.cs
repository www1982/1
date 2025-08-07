using System;
using System.Collections.Generic;

// Token: 0x020004B1 RID: 1201
public class ChoreTable
{
	// Token: 0x06001997 RID: 6551 RVA: 0x0008D205 File Offset: 0x0008B405
	public ChoreTable(ChoreTable.Entry[] entries)
	{
		this.entries = entries;
	}

	// Token: 0x06001998 RID: 6552 RVA: 0x0008D214 File Offset: 0x0008B414
	public ref ChoreTable.Entry GetEntry<T>()
	{
		ref ChoreTable.Entry ptr = ref ChoreTable.InvalidEntry;
		for (int i = 0; i < this.entries.Length; i++)
		{
			if (this.entries[i].stateMachineDef is T)
			{
				ptr = ref this.entries[i];
				break;
			}
		}
		return ref ptr;
	}

	// Token: 0x06001999 RID: 6553 RVA: 0x0008D264 File Offset: 0x0008B464
	public int GetChorePriority<StateMachineType>(ChoreConsumer chore_consumer)
	{
		for (int i = 0; i < this.entries.Length; i++)
		{
			ChoreTable.Entry entry = this.entries[i];
			if (entry.stateMachineDef.GetStateMachineType() == typeof(StateMachineType))
			{
				return entry.choreType.priority;
			}
		}
		Debug.LogError(chore_consumer.name + "'s chore table does not have an entry for: " + typeof(StateMachineType).Name);
		return -1;
	}

	// Token: 0x04000EB6 RID: 3766
	private ChoreTable.Entry[] entries;

	// Token: 0x04000EB7 RID: 3767
	public static ChoreTable.Entry InvalidEntry;

	// Token: 0x020012E8 RID: 4840
	public class Builder
	{
		// Token: 0x06008824 RID: 34852 RVA: 0x00348B09 File Offset: 0x00346D09
		public ChoreTable.Builder PushInterruptGroup()
		{
			this.interruptGroupId++;
			return this;
		}

		// Token: 0x06008825 RID: 34853 RVA: 0x00348B1A File Offset: 0x00346D1A
		public ChoreTable.Builder PopInterruptGroup()
		{
			DebugUtil.Assert(this.interruptGroupId > 0);
			this.interruptGroupId--;
			return this;
		}

		// Token: 0x06008826 RID: 34854 RVA: 0x00348B3C File Offset: 0x00346D3C
		public ChoreTable.Builder Add(StateMachine.BaseDef def, bool condition = true, int forcePriority = -1)
		{
			if (condition)
			{
				ChoreTable.Builder.Info info = new ChoreTable.Builder.Info
				{
					interruptGroupId = this.interruptGroupId,
					forcePriority = forcePriority,
					def = def
				};
				this.infos.Add(info);
			}
			return this;
		}

		// Token: 0x06008827 RID: 34855 RVA: 0x00348B80 File Offset: 0x00346D80
		public bool HasChoreType(Type choreType)
		{
			return this.infos.Exists((ChoreTable.Builder.Info info) => info.def.GetType() == choreType);
		}

		// Token: 0x06008828 RID: 34856 RVA: 0x00348BB4 File Offset: 0x00346DB4
		public bool TryGetChoreDef<T>(out T def) where T : StateMachine.BaseDef
		{
			for (int i = 0; i < this.infos.Count; i++)
			{
				if (this.infos[i].def != null && typeof(T).IsAssignableFrom(this.infos[i].def.GetType()))
				{
					def = (T)((object)this.infos[i].def);
					return true;
				}
			}
			def = default(T);
			return false;
		}

		// Token: 0x06008829 RID: 34857 RVA: 0x00348C38 File Offset: 0x00346E38
		public ChoreTable CreateTable()
		{
			DebugUtil.Assert(this.interruptGroupId == 0);
			ChoreTable.Entry[] array = new ChoreTable.Entry[this.infos.Count];
			Stack<int> stack = new Stack<int>();
			int num = 10000;
			for (int i = 0; i < this.infos.Count; i++)
			{
				int num2 = ((this.infos[i].forcePriority != -1) ? this.infos[i].forcePriority : (num - 100));
				num = num2;
				int num3 = 10000 - i * 100;
				int num4 = this.infos[i].interruptGroupId;
				if (num4 != 0)
				{
					if (stack.Count != num4)
					{
						stack.Push(num3);
					}
					else
					{
						num3 = stack.Peek();
					}
				}
				else if (stack.Count > 0)
				{
					stack.Pop();
				}
				array[i] = new ChoreTable.Entry(this.infos[i].def, num2, num3);
			}
			return new ChoreTable(array);
		}

		// Token: 0x040067E0 RID: 26592
		private int interruptGroupId;

		// Token: 0x040067E1 RID: 26593
		private List<ChoreTable.Builder.Info> infos = new List<ChoreTable.Builder.Info>();

		// Token: 0x040067E2 RID: 26594
		private const int INVALID_PRIORITY = -1;

		// Token: 0x02002690 RID: 9872
		private struct Info
		{
			// Token: 0x0400AB6B RID: 43883
			public int interruptGroupId;

			// Token: 0x0400AB6C RID: 43884
			public int forcePriority;

			// Token: 0x0400AB6D RID: 43885
			public StateMachine.BaseDef def;
		}
	}

	// Token: 0x020012E9 RID: 4841
	public class ChoreTableChore<StateMachineType, StateMachineInstanceType> : Chore<StateMachineInstanceType> where StateMachineInstanceType : StateMachine.Instance
	{
		// Token: 0x0600882B RID: 34859 RVA: 0x00348D48 File Offset: 0x00346F48
		public ChoreTableChore(StateMachine.BaseDef state_machine_def, ChoreType chore_type, KPrefabID prefab_id)
			: base(chore_type, prefab_id, prefab_id.GetComponent<ChoreProvider>(), true, null, null, null, PriorityScreen.PriorityClass.basic, 5, false, true, 0, false, ReportManager.ReportType.WorkTime)
		{
			this.showAvailabilityInHoverText = false;
			base.smi = state_machine_def.CreateSMI(this) as StateMachineInstanceType;
		}
	}

	// Token: 0x020012EA RID: 4842
	public struct Entry
	{
		// Token: 0x0600882C RID: 34860 RVA: 0x00348D90 File Offset: 0x00346F90
		public Entry(StateMachine.BaseDef state_machine_def, int priority, int interrupt_priority)
		{
			Type stateMachineInstanceType = Singleton<StateMachineManager>.Instance.CreateStateMachine(state_machine_def.GetStateMachineType()).GetStateMachineInstanceType();
			Type[] array = new Type[]
			{
				state_machine_def.GetStateMachineType(),
				stateMachineInstanceType
			};
			this.choreClassType = typeof(ChoreTable.ChoreTableChore<, >).MakeGenericType(array);
			this.choreType = new ChoreType(state_machine_def.ToString(), null, new string[0], "", "", "", "", new Tag[0], priority, priority);
			this.choreType.interruptPriority = interrupt_priority;
			this.stateMachineDef = state_machine_def;
		}

		// Token: 0x040067E3 RID: 26595
		public Type choreClassType;

		// Token: 0x040067E4 RID: 26596
		public ChoreType choreType;

		// Token: 0x040067E5 RID: 26597
		public StateMachine.BaseDef stateMachineDef;
	}

	// Token: 0x020012EB RID: 4843
	public class Instance
	{
		// Token: 0x0600882D RID: 34861 RVA: 0x00348E24 File Offset: 0x00347024
		public static void ResetParameters()
		{
			for (int i = 0; i < ChoreTable.Instance.parameters.Length; i++)
			{
				ChoreTable.Instance.parameters[i] = null;
			}
		}

		// Token: 0x0600882E RID: 34862 RVA: 0x00348E4C File Offset: 0x0034704C
		public Instance(ChoreTable chore_table, KPrefabID prefab_id)
		{
			this.prefabId = prefab_id;
			this.entries = ListPool<ChoreTable.Instance.Entry, ChoreTable.Instance>.Allocate();
			for (int i = 0; i < chore_table.entries.Length; i++)
			{
				this.entries.Add(new ChoreTable.Instance.Entry(chore_table.entries[i], prefab_id));
			}
		}

		// Token: 0x0600882F RID: 34863 RVA: 0x00348EA4 File Offset: 0x003470A4
		~Instance()
		{
			this.OnCleanUp(this.prefabId);
		}

		// Token: 0x06008830 RID: 34864 RVA: 0x00348ED8 File Offset: 0x003470D8
		public void OnCleanUp(KPrefabID prefab_id)
		{
			if (this.entries == null)
			{
				return;
			}
			for (int i = 0; i < this.entries.Count; i++)
			{
				this.entries[i].OnCleanUp(prefab_id);
			}
			this.entries.Recycle();
			this.entries = null;
		}

		// Token: 0x040067E6 RID: 26598
		private static object[] parameters = new object[3];

		// Token: 0x040067E7 RID: 26599
		private KPrefabID prefabId;

		// Token: 0x040067E8 RID: 26600
		private ListPool<ChoreTable.Instance.Entry, ChoreTable.Instance>.PooledList entries;

		// Token: 0x02002692 RID: 9874
		private struct Entry
		{
			// Token: 0x0600C434 RID: 50228 RVA: 0x0040D2A4 File Offset: 0x0040B4A4
			public Entry(ChoreTable.Entry chore_table_entry, KPrefabID prefab_id)
			{
				ChoreTable.Instance.parameters[0] = chore_table_entry.stateMachineDef;
				ChoreTable.Instance.parameters[1] = chore_table_entry.choreType;
				ChoreTable.Instance.parameters[2] = prefab_id;
				this.chore = (Chore)Activator.CreateInstance(chore_table_entry.choreClassType, ChoreTable.Instance.parameters);
				ChoreTable.Instance.parameters[0] = null;
				ChoreTable.Instance.parameters[1] = null;
				ChoreTable.Instance.parameters[2] = null;
			}

			// Token: 0x0600C435 RID: 50229 RVA: 0x0040D306 File Offset: 0x0040B506
			public void OnCleanUp(KPrefabID prefab_id)
			{
				if (this.chore != null)
				{
					this.chore.Cancel("ChoreTable.Instance.OnCleanUp");
					this.chore = null;
				}
			}

			// Token: 0x0400AB6F RID: 43887
			public Chore chore;
		}
	}
}
