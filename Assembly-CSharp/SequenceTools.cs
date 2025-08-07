using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000828 RID: 2088
public static class SequenceTools
{
	// Token: 0x0600393B RID: 14651 RVA: 0x0013D6C8 File Offset: 0x0013B8C8
	public static WaitUntil Interpolate(this MonoBehaviour owner, Action<float> action, float duration, global::System.Action then = null)
	{
		Coroutine coroutine;
		return owner.Interpolate(action, duration, out coroutine, then);
	}

	// Token: 0x0600393C RID: 14652 RVA: 0x0013D6E0 File Offset: 0x0013B8E0
	public static WaitUntil Interpolate(this MonoBehaviour owner, Action<float> action, float duration, out Coroutine coroutineOut, global::System.Action then = null)
	{
		bool completed = false;
		global::System.Action action2 = delegate
		{
			if (then != null)
			{
				then();
			}
			completed = true;
		};
		coroutineOut = owner.StartCoroutine(SequenceTools.InterpolateCoroutineLogic(action, duration, action2));
		return new WaitUntil(() => completed);
	}

	// Token: 0x0600393D RID: 14653 RVA: 0x0013D72E File Offset: 0x0013B92E
	private static IEnumerator InterpolateCoroutineLogic(Action<float> action, float duration, global::System.Action then)
	{
		float timer = 0f;
		while (timer < duration)
		{
			float num = timer / duration;
			action(num);
			timer += Time.unscaledDeltaTime;
			yield return null;
		}
		action(1f);
		yield return null;
		if (then != null)
		{
			then();
		}
		yield break;
	}

	// Token: 0x0600393E RID: 14654 RVA: 0x0013D74C File Offset: 0x0013B94C
	public static void TextEraser(LocText label, string text, float progress)
	{
		string text2 = text.Substring(0, Mathf.CeilToInt((float)text.Length * (1f - progress)));
		label.SetText(text2);
		label.ForceMeshUpdate();
	}

	// Token: 0x0600393F RID: 14655 RVA: 0x0013D784 File Offset: 0x0013B984
	public static void TextWriter(LocText label, string text, float progress)
	{
		string text2 = ((progress == 1f) ? text : text.Substring(0, Mathf.CeilToInt((float)text.Length * progress)));
		label.SetText(text2);
		label.ForceMeshUpdate();
	}
}
