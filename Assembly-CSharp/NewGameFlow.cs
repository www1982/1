using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000D78 RID: 3448
[AddComponentMenu("KMonoBehaviour/scripts/NewGameFlow")]
public class NewGameFlow : KMonoBehaviour
{
	// Token: 0x06006B43 RID: 27459 RVA: 0x00288608 File Offset: 0x00286808
	public void BeginFlow()
	{
		this.currentScreenIndex = -1;
		this.Next();
	}

	// Token: 0x06006B44 RID: 27460 RVA: 0x00288617 File Offset: 0x00286817
	private void Next()
	{
		this.ClearCurrentScreen();
		this.currentScreenIndex++;
		this.ActivateCurrentScreen();
	}

	// Token: 0x06006B45 RID: 27461 RVA: 0x00288633 File Offset: 0x00286833
	private void Previous()
	{
		this.ClearCurrentScreen();
		this.currentScreenIndex--;
		this.ActivateCurrentScreen();
	}

	// Token: 0x06006B46 RID: 27462 RVA: 0x0028864F File Offset: 0x0028684F
	private void ClearCurrentScreen()
	{
		if (this.currentScreen != null)
		{
			this.currentScreen.Deactivate();
			this.currentScreen = null;
		}
	}

	// Token: 0x06006B47 RID: 27463 RVA: 0x00288674 File Offset: 0x00286874
	private void ActivateCurrentScreen()
	{
		if (this.currentScreenIndex >= 0 && this.currentScreenIndex < this.newGameFlowScreens.Count)
		{
			NewGameFlowScreen newGameFlowScreen = Util.KInstantiateUI<NewGameFlowScreen>(this.newGameFlowScreens[this.currentScreenIndex].gameObject, base.transform.parent.gameObject, true);
			newGameFlowScreen.OnNavigateForward += this.Next;
			newGameFlowScreen.OnNavigateBackward += this.Previous;
			if (!newGameFlowScreen.IsActive() && !newGameFlowScreen.activateOnSpawn)
			{
				newGameFlowScreen.Activate();
			}
			this.currentScreen = newGameFlowScreen;
		}
	}

	// Token: 0x04004909 RID: 18697
	public List<NewGameFlowScreen> newGameFlowScreens;

	// Token: 0x0400490A RID: 18698
	private int currentScreenIndex = -1;

	// Token: 0x0400490B RID: 18699
	private NewGameFlowScreen currentScreen;
}
