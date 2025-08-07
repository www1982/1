using System;
using System.Collections.Generic;

namespace Klei.Actions
{
	// Token: 0x02001020 RID: 4128
	public class ActionFactory<ActionFactoryType, ActionType, EnumType> where ActionFactoryType : ActionFactory<ActionFactoryType, ActionType, EnumType>
	{
		// Token: 0x06007F15 RID: 32533 RVA: 0x0032B7E0 File Offset: 0x003299E0
		public static ActionType GetOrCreateAction(EnumType actionType)
		{
			ActionType actionType2;
			if (!ActionFactory<ActionFactoryType, ActionType, EnumType>.actionInstances.TryGetValue(actionType, out actionType2))
			{
				ActionFactory<ActionFactoryType, ActionType, EnumType>.EnsureFactoryInstance();
				actionType2 = (ActionFactory<ActionFactoryType, ActionType, EnumType>.actionInstances[actionType] = ActionFactory<ActionFactoryType, ActionType, EnumType>.actionFactory.CreateAction(actionType));
			}
			return actionType2;
		}

		// Token: 0x06007F16 RID: 32534 RVA: 0x0032B81F File Offset: 0x00329A1F
		private static void EnsureFactoryInstance()
		{
			if (ActionFactory<ActionFactoryType, ActionType, EnumType>.actionFactory != null)
			{
				return;
			}
			ActionFactory<ActionFactoryType, ActionType, EnumType>.actionFactory = Activator.CreateInstance(typeof(ActionFactoryType)) as ActionFactoryType;
		}

		// Token: 0x06007F17 RID: 32535 RVA: 0x0032B84C File Offset: 0x00329A4C
		protected virtual ActionType CreateAction(EnumType actionType)
		{
			throw new InvalidOperationException("Can not call InterfaceToolActionFactory<T1, T2>.CreateAction()! This function must be called from a deriving class!");
		}

		// Token: 0x04005FBC RID: 24508
		private static Dictionary<EnumType, ActionType> actionInstances = new Dictionary<EnumType, ActionType>();

		// Token: 0x04005FBD RID: 24509
		private static ActionFactoryType actionFactory = default(ActionFactoryType);
	}
}
