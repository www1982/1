using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x020007FC RID: 2044
public class BundledAssetsLoader : KMonoBehaviour
{
	// Token: 0x170003BF RID: 959
	// (get) Token: 0x0600379D RID: 14237 RVA: 0x00134945 File Offset: 0x00132B45
	// (set) Token: 0x0600379E RID: 14238 RVA: 0x0013494D File Offset: 0x00132B4D
	public BundledAssets Expansion1Assets { get; private set; }

	// Token: 0x170003C0 RID: 960
	// (get) Token: 0x0600379F RID: 14239 RVA: 0x00134956 File Offset: 0x00132B56
	// (set) Token: 0x060037A0 RID: 14240 RVA: 0x0013495E File Offset: 0x00132B5E
	public List<BundledAssets> DlcAssetsList { get; private set; }

	// Token: 0x060037A1 RID: 14241 RVA: 0x00134968 File Offset: 0x00132B68
	protected override void OnPrefabInit()
	{
		BundledAssetsLoader.instance = this;
		if (DlcManager.IsExpansion1Active())
		{
			global::Debug.Log("Loading Expansion1 assets from bundle");
			AssetBundle assetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, DlcManager.GetContentBundleName("EXPANSION1_ID")));
			global::Debug.Assert(assetBundle != null, "Expansion1 is Active but its asset bundle failed to load");
			GameObject gameObject = assetBundle.LoadAsset<GameObject>("Expansion1Assets");
			global::Debug.Assert(gameObject != null, "Could not load the Expansion1Assets prefab");
			this.Expansion1Assets = Util.KInstantiate(gameObject, base.gameObject, null).GetComponent<BundledAssets>();
		}
		this.DlcAssetsList = new List<BundledAssets>(DlcManager.DLC_PACKS.Count);
		foreach (KeyValuePair<string, DlcManager.DlcInfo> keyValuePair in DlcManager.DLC_PACKS)
		{
			if (DlcManager.IsContentSubscribed(keyValuePair.Key))
			{
				global::Debug.Log("Loading DLC " + keyValuePair.Key + " assets from bundle");
				AssetBundle assetBundle2 = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, DlcManager.GetContentBundleName(keyValuePair.Key)));
				global::Debug.Assert(assetBundle2 != null, "DLC " + keyValuePair.Key + " is Active but its asset bundle failed to load");
				GameObject gameObject2 = assetBundle2.LoadAsset<GameObject>(keyValuePair.Value.directory + "Assets");
				global::Debug.Assert(gameObject2 != null, "Could not load the " + keyValuePair.Key + " prefab");
				this.DlcAssetsList.Add(Util.KInstantiate(gameObject2, base.gameObject, null).GetComponent<BundledAssets>());
			}
		}
	}

	// Token: 0x040021BB RID: 8635
	public static BundledAssetsLoader instance;
}
