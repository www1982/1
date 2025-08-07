using System;
using UnityEngine;

// Token: 0x02000C72 RID: 3186
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/CharacterOverlay")]
public class CharacterOverlay : KMonoBehaviour
{
	// Token: 0x06006157 RID: 24919 RVA: 0x0024267F File Offset: 0x0024087F
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.Register();
	}

	// Token: 0x06006158 RID: 24920 RVA: 0x0024268D File Offset: 0x0024088D
	public void Register()
	{
		if (this.registered)
		{
			return;
		}
		this.registered = true;
		NameDisplayScreen.Instance.AddNewEntry(base.gameObject);
	}

	// Token: 0x040041F6 RID: 16886
	public bool shouldShowName;

	// Token: 0x040041F7 RID: 16887
	private bool registered;
}
