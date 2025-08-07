using System;
using UnityEngine;

// Token: 0x02000BFD RID: 3069
public class FixGraphicsCorruption : MonoBehaviour
{
	// Token: 0x06005C73 RID: 23667 RVA: 0x0021C3AC File Offset: 0x0021A5AC
	private void Start()
	{
		Camera component = base.GetComponent<Camera>();
		component.transparencySortMode = TransparencySortMode.Orthographic;
		component.tag = "Untagged";
	}

	// Token: 0x06005C74 RID: 23668 RVA: 0x0021C3C5 File Offset: 0x0021A5C5
	private void OnRenderImage(RenderTexture source, RenderTexture dest)
	{
		Graphics.Blit(source, dest);
	}
}
