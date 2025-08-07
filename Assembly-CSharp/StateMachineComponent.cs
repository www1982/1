using System;
using KSerialization;

// Token: 0x02000512 RID: 1298
[SerializationConfig(MemberSerialization.OptIn)]
public abstract class StateMachineComponent : KMonoBehaviour, ISaveLoadable, IStateMachineTarget
{
	// Token: 0x06001BBD RID: 7101
	public abstract StateMachine.Instance GetSMI();

	// Token: 0x0400105A RID: 4186
	[MyCmpAdd]
	protected StateMachineController stateMachineController;
}
