using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200044D RID: 1101
public class CoroutineRunner : MonoBehaviour
{
	// Token: 0x060016DC RID: 5852 RVA: 0x000814D8 File Offset: 0x0007F6D8
	public Promise Run(IEnumerator routine)
	{
		return new Promise(delegate(global::System.Action resolve)
		{
			this.StartCoroutine(this.RunRoutine(routine, resolve));
		});
	}

	// Token: 0x060016DD RID: 5853 RVA: 0x00081500 File Offset: 0x0007F700
	public ValueTuple<Promise, global::System.Action> RunCancellable(IEnumerator routine)
	{
		Promise promise = new Promise();
		Coroutine coroutine = base.StartCoroutine(this.RunRoutine(routine, new global::System.Action(promise.Resolve)));
		global::System.Action action = delegate
		{
			this.StopCoroutine(coroutine);
		};
		return new ValueTuple<Promise, global::System.Action>(promise, action);
	}

	// Token: 0x060016DE RID: 5854 RVA: 0x00081551 File Offset: 0x0007F751
	private IEnumerator RunRoutine(IEnumerator routine, global::System.Action completedCallback)
	{
		yield return routine;
		completedCallback();
		yield break;
	}

	// Token: 0x060016DF RID: 5855 RVA: 0x00081567 File Offset: 0x0007F767
	public static CoroutineRunner Create()
	{
		return new GameObject("CoroutineRunner").AddComponent<CoroutineRunner>();
	}

	// Token: 0x060016E0 RID: 5856 RVA: 0x00081578 File Offset: 0x0007F778
	public static Promise RunOne(IEnumerator routine)
	{
		CoroutineRunner runner = CoroutineRunner.Create();
		return runner.Run(routine).Then(delegate
		{
			global::UnityEngine.Object.Destroy(runner.gameObject);
		});
	}
}
