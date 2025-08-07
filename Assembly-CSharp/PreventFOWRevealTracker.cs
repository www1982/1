using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using KSerialization;
using UnityEngine;

// Token: 0x02000A77 RID: 2679
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/PreventFOWRevealTracker")]
public class PreventFOWRevealTracker : KMonoBehaviour
{
	// Token: 0x06004D9F RID: 19871 RVA: 0x001C1728 File Offset: 0x001BF928
	[OnSerializing]
	private void OnSerialize()
	{
		this.preventFOWRevealCells.Clear();
		for (int i = 0; i < Grid.VisMasks.Length; i++)
		{
			if (Grid.PreventFogOfWarReveal[i])
			{
				this.preventFOWRevealCells.Add(i);
			}
		}
	}

	// Token: 0x06004DA0 RID: 19872 RVA: 0x001C176C File Offset: 0x001BF96C
	[OnDeserialized]
	private void OnDeserialized()
	{
		foreach (int num in this.preventFOWRevealCells)
		{
			Grid.PreventFogOfWarReveal[num] = true;
		}
	}

	// Token: 0x0400339D RID: 13213
	[Serialize]
	public List<int> preventFOWRevealCells;
}
