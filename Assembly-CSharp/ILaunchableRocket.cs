using System;
using UnityEngine;

// Token: 0x02000B53 RID: 2899
public interface ILaunchableRocket
{
	// Token: 0x1700063D RID: 1597
	// (get) Token: 0x06005659 RID: 22105
	LaunchableRocketRegisterType registerType { get; }

	// Token: 0x1700063E RID: 1598
	// (get) Token: 0x0600565A RID: 22106
	GameObject LaunchableGameObject { get; }

	// Token: 0x1700063F RID: 1599
	// (get) Token: 0x0600565B RID: 22107
	float rocketSpeed { get; }

	// Token: 0x17000640 RID: 1600
	// (get) Token: 0x0600565C RID: 22108
	bool isLanding { get; }
}
