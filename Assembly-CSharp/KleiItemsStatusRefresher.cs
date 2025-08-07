using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000D06 RID: 3334
public static class KleiItemsStatusRefresher
{
	// Token: 0x060066E1 RID: 26337 RVA: 0x0026DC10 File Offset: 0x0026BE10
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
	private static void Initialize()
	{
		KleiItems.AddInventoryRefreshCallback(new KleiItems.InventoryRefreshCallback(KleiItemsStatusRefresher.OnRefreshResponseFromServer));
	}

	// Token: 0x060066E2 RID: 26338 RVA: 0x0026DC24 File Offset: 0x0026BE24
	private static void OnRefreshResponseFromServer()
	{
		foreach (KleiItemsStatusRefresher.UIListener uilistener in KleiItemsStatusRefresher.listeners)
		{
			uilistener.Internal_RefreshUI();
		}
	}

	// Token: 0x060066E3 RID: 26339 RVA: 0x0026DC74 File Offset: 0x0026BE74
	public static void Refresh()
	{
		foreach (KleiItemsStatusRefresher.UIListener uilistener in KleiItemsStatusRefresher.listeners)
		{
			uilistener.Internal_RefreshUI();
		}
	}

	// Token: 0x060066E4 RID: 26340 RVA: 0x0026DCC4 File Offset: 0x0026BEC4
	public static KleiItemsStatusRefresher.UIListener AddOrGetListener(Component component)
	{
		return KleiItemsStatusRefresher.AddOrGetListener(component.gameObject);
	}

	// Token: 0x060066E5 RID: 26341 RVA: 0x0026DCD1 File Offset: 0x0026BED1
	public static KleiItemsStatusRefresher.UIListener AddOrGetListener(GameObject onGameObject)
	{
		return onGameObject.AddOrGet<KleiItemsStatusRefresher.UIListener>();
	}

	// Token: 0x0400468B RID: 18059
	public static HashSet<KleiItemsStatusRefresher.UIListener> listeners = new HashSet<KleiItemsStatusRefresher.UIListener>();

	// Token: 0x02001ED8 RID: 7896
	public class UIListener : MonoBehaviour
	{
		// Token: 0x0600B18E RID: 45454 RVA: 0x003D5B2C File Offset: 0x003D3D2C
		public void Internal_RefreshUI()
		{
			if (this.refreshUIFn != null)
			{
				this.refreshUIFn();
			}
		}

		// Token: 0x0600B18F RID: 45455 RVA: 0x003D5B41 File Offset: 0x003D3D41
		public void OnRefreshUI(global::System.Action fn)
		{
			this.refreshUIFn = fn;
		}

		// Token: 0x0600B190 RID: 45456 RVA: 0x003D5B4A File Offset: 0x003D3D4A
		private void OnEnable()
		{
			KleiItemsStatusRefresher.listeners.Add(this);
		}

		// Token: 0x0600B191 RID: 45457 RVA: 0x003D5B58 File Offset: 0x003D3D58
		private void OnDisable()
		{
			KleiItemsStatusRefresher.listeners.Remove(this);
		}

		// Token: 0x04008F16 RID: 36630
		private global::System.Action refreshUIFn;
	}
}
