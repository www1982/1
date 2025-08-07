using System;
using System.Collections.Generic;
using System.Reflection;

// Token: 0x02000836 RID: 2102
public class GameComps : KComponents
{
	// Token: 0x06003998 RID: 14744 RVA: 0x00140160 File Offset: 0x0013E360
	public GameComps()
	{
		foreach (FieldInfo fieldInfo in typeof(GameComps).GetFields())
		{
			object obj = Activator.CreateInstance(fieldInfo.FieldType);
			fieldInfo.SetValue(null, obj);
			base.Add<IComponentManager>(obj as IComponentManager);
			if (obj is IKComponentManager)
			{
				IKComponentManager ikcomponentManager = obj as IKComponentManager;
				GameComps.AddKComponentManager(fieldInfo.FieldType, ikcomponentManager);
			}
		}
	}

	// Token: 0x06003999 RID: 14745 RVA: 0x001401D4 File Offset: 0x0013E3D4
	public new void Clear()
	{
		FieldInfo[] fields = typeof(GameComps).GetFields();
		for (int i = 0; i < fields.Length; i++)
		{
			IComponentManager componentManager = fields[i].GetValue(null) as IComponentManager;
			if (componentManager != null)
			{
				componentManager.Clear();
			}
		}
	}

	// Token: 0x0600399A RID: 14746 RVA: 0x00140217 File Offset: 0x0013E417
	public static void AddKComponentManager(Type kcomponent, IKComponentManager inst)
	{
		GameComps.kcomponentManagers[kcomponent] = inst;
	}

	// Token: 0x0600399B RID: 14747 RVA: 0x00140225 File Offset: 0x0013E425
	public static IKComponentManager GetKComponentManager(Type kcomponent_type)
	{
		return GameComps.kcomponentManagers[kcomponent_type];
	}

	// Token: 0x04002357 RID: 9047
	public static GravityComponents Gravities;

	// Token: 0x04002358 RID: 9048
	public static FallerComponents Fallers;

	// Token: 0x04002359 RID: 9049
	public static InfraredVisualizerComponents InfraredVisualizers;

	// Token: 0x0400235A RID: 9050
	public static ElementSplitterComponents ElementSplitters;

	// Token: 0x0400235B RID: 9051
	public static OreSizeVisualizerComponents OreSizeVisualizers;

	// Token: 0x0400235C RID: 9052
	public static StructureTemperatureComponents StructureTemperatures;

	// Token: 0x0400235D RID: 9053
	public static DiseaseContainers DiseaseContainers;

	// Token: 0x0400235E RID: 9054
	public static RequiresFoundation RequiresFoundations;

	// Token: 0x0400235F RID: 9055
	public static WhiteBoard WhiteBoards;

	// Token: 0x04002360 RID: 9056
	private static Dictionary<Type, IKComponentManager> kcomponentManagers = new Dictionary<Type, IKComponentManager>();
}
