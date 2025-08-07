using System;

// Token: 0x02000509 RID: 1289
public abstract class GameStateMachine<StateMachineType, StateMachineInstanceType> : GameStateMachine<StateMachineType, StateMachineInstanceType, IStateMachineTarget, object> where StateMachineType : GameStateMachine<StateMachineType, StateMachineInstanceType, IStateMachineTarget, object> where StateMachineInstanceType : GameStateMachine<StateMachineType, StateMachineInstanceType, IStateMachineTarget, object>.GameInstance
{
}
