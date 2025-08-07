using System;
using UnityEngine;

// Token: 0x02000AF0 RID: 2800
public class SceneInitializerLoader : MonoBehaviour
{
	// Token: 0x06005226 RID: 21030 RVA: 0x001DEE0C File Offset: 0x001DD00C
	private void Awake()
	{
		Camera[] array = global::UnityEngine.Object.FindObjectsOfType<Camera>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].enabled = false;
		}
		KMonoBehaviour.isLoadingScene = false;
		Singleton<StateMachineManager>.Instance.Clear();
		Util.KInstantiate(this.sceneInitializer, null, null);
		if (SceneInitializerLoader.ReportDeferredError != null && SceneInitializerLoader.deferred_error.IsValid)
		{
			SceneInitializerLoader.ReportDeferredError(SceneInitializerLoader.deferred_error);
			SceneInitializerLoader.deferred_error = default(SceneInitializerLoader.DeferredError);
		}
	}

	// Token: 0x0400373D RID: 14141
	public SceneInitializer sceneInitializer;

	// Token: 0x0400373E RID: 14142
	public static SceneInitializerLoader.DeferredError deferred_error;

	// Token: 0x0400373F RID: 14143
	public static SceneInitializerLoader.DeferredErrorDelegate ReportDeferredError;

	// Token: 0x02001BF8 RID: 7160
	public struct DeferredError
	{
		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x0600A94B RID: 43339 RVA: 0x003B755A File Offset: 0x003B575A
		public bool IsValid
		{
			get
			{
				return !string.IsNullOrEmpty(this.msg);
			}
		}

		// Token: 0x040084A4 RID: 33956
		public string msg;

		// Token: 0x040084A5 RID: 33957
		public string stack_trace;
	}

	// Token: 0x02001BF9 RID: 7161
	// (Invoke) Token: 0x0600A94D RID: 43341
	public delegate void DeferredErrorDelegate(SceneInitializerLoader.DeferredError deferred_error);
}
