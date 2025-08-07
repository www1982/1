using System;
using Database;
using UnityEngine;

// Token: 0x02000D09 RID: 3337
public interface IKleiPermitDioramaVisTarget
{
	// Token: 0x060066FB RID: 26363
	GameObject GetGameObject();

	// Token: 0x060066FC RID: 26364
	void ConfigureSetup();

	// Token: 0x060066FD RID: 26365
	void ConfigureWith(PermitResource permit);
}
