using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ionic.Zip;
using Steamworks;
using UnityEngine;

// Token: 0x02000BF1 RID: 3057
public class SteamUGCService : MonoBehaviour
{
	// Token: 0x170006BF RID: 1727
	// (get) Token: 0x06005C1E RID: 23582 RVA: 0x00217F2A File Offset: 0x0021612A
	public static SteamUGCService Instance
	{
		get
		{
			return SteamUGCService.instance;
		}
	}

	// Token: 0x06005C1F RID: 23583 RVA: 0x00217F34 File Offset: 0x00216134
	private SteamUGCService()
	{
		this.on_subscribed = Callback<RemoteStoragePublishedFileSubscribed_t>.Create(new Callback<RemoteStoragePublishedFileSubscribed_t>.DispatchDelegate(this.OnItemSubscribed));
		this.on_unsubscribed = Callback<RemoteStoragePublishedFileUnsubscribed_t>.Create(new Callback<RemoteStoragePublishedFileUnsubscribed_t>.DispatchDelegate(this.OnItemUnsubscribed));
		this.on_updated = Callback<RemoteStoragePublishedFileUpdated_t>.Create(new Callback<RemoteStoragePublishedFileUpdated_t>.DispatchDelegate(this.OnItemUpdated));
		this.on_query_completed = CallResult<SteamUGCQueryCompleted_t>.Create(new CallResult<SteamUGCQueryCompleted_t>.APIDispatchDelegate(this.OnSteamUGCQueryDetailsCompleted));
		this.on_download_completed = Callback<DownloadItemResult_t>.Create(new Callback<DownloadItemResult_t>.DispatchDelegate(this.OnDownloadItemComplete));
		this.mods = new List<SteamUGCService.Mod>();
	}

	// Token: 0x06005C20 RID: 23584 RVA: 0x00218040 File Offset: 0x00216240
	public static void Initialize()
	{
		if (SteamUGCService.instance != null)
		{
			return;
		}
		GameObject gameObject = GameObject.Find("/SteamManager");
		SteamUGCService.instance = gameObject.GetComponent<SteamUGCService>();
		if (SteamUGCService.instance == null)
		{
			SteamUGCService.instance = gameObject.AddComponent<SteamUGCService>();
		}
	}

	// Token: 0x06005C21 RID: 23585 RVA: 0x0021808C File Offset: 0x0021628C
	public void AddClient(SteamUGCService.IClient client)
	{
		this.clients.Add(client);
		ListPool<PublishedFileId_t, SteamUGCService>.PooledList pooledList = ListPool<PublishedFileId_t, SteamUGCService>.Allocate();
		foreach (SteamUGCService.Mod mod in this.mods)
		{
			pooledList.Add(mod.fileId);
		}
		client.UpdateMods(pooledList, Enumerable.Empty<PublishedFileId_t>(), Enumerable.Empty<PublishedFileId_t>(), Enumerable.Empty<SteamUGCService.Mod>());
		pooledList.Recycle();
	}

	// Token: 0x06005C22 RID: 23586 RVA: 0x00218114 File Offset: 0x00216314
	public void RemoveClient(SteamUGCService.IClient client)
	{
		this.clients.Remove(client);
	}

	// Token: 0x06005C23 RID: 23587 RVA: 0x00218124 File Offset: 0x00216324
	public void Awake()
	{
		global::Debug.Assert(SteamUGCService.instance == null);
		SteamUGCService.instance = this;
		uint numSubscribedItems = SteamUGC.GetNumSubscribedItems();
		if (numSubscribedItems != 0U)
		{
			PublishedFileId_t[] array = new PublishedFileId_t[numSubscribedItems];
			SteamUGC.GetSubscribedItems(array, numSubscribedItems);
			this.downloads.UnionWith(array);
		}
	}

	// Token: 0x06005C24 RID: 23588 RVA: 0x0021816C File Offset: 0x0021636C
	public bool IsSubscribed(PublishedFileId_t item)
	{
		return this.downloads.Contains(item) || this.proxies.Contains(item) || this.queries.Contains(item) || this.publishes.Any((SteamUGCDetails_t candidate) => candidate.m_nPublishedFileId == item) || this.mods.Exists((SteamUGCService.Mod candidate) => candidate.fileId == item);
	}

	// Token: 0x06005C25 RID: 23589 RVA: 0x002181F4 File Offset: 0x002163F4
	public SteamUGCService.Mod FindMod(PublishedFileId_t item)
	{
		return this.mods.Find((SteamUGCService.Mod candidate) => candidate.fileId == item);
	}

	// Token: 0x06005C26 RID: 23590 RVA: 0x00218225 File Offset: 0x00216425
	private void OnDestroy()
	{
		global::Debug.Assert(SteamUGCService.instance == this);
		SteamUGCService.instance = null;
	}

	// Token: 0x06005C27 RID: 23591 RVA: 0x00218240 File Offset: 0x00216440
	private Texture2D LoadPreviewImage(SteamUGCDetails_t details)
	{
		byte[] array = null;
		if (details.m_hPreviewFile != UGCHandle_t.Invalid)
		{
			SteamRemoteStorage.UGCDownload(details.m_hPreviewFile, 0U);
			array = new byte[details.m_nPreviewFileSize];
			if (SteamRemoteStorage.UGCRead(details.m_hPreviewFile, array, details.m_nPreviewFileSize, 0U, EUGCReadAction.k_EUGCRead_ContinueReadingUntilFinished) != details.m_nPreviewFileSize)
			{
				if (this.retry_counts[details.m_nPublishedFileId] % 100 == 0)
				{
					global::Debug.LogFormat("Steam: Preview image load failed", Array.Empty<object>());
				}
				array = null;
			}
		}
		if (array == null)
		{
			global::System.DateTime dateTime;
			array = SteamUGCService.GetBytesFromZip(details.m_nPublishedFileId, SteamUGCService.previewFileNames, out dateTime, false);
		}
		Texture2D texture2D = null;
		if (array != null)
		{
			texture2D = new Texture2D(2, 2);
			texture2D.LoadImage(array);
		}
		else
		{
			Dictionary<PublishedFileId_t, int> dictionary = this.retry_counts;
			PublishedFileId_t nPublishedFileId = details.m_nPublishedFileId;
			dictionary[nPublishedFileId]++;
		}
		return texture2D;
	}

	// Token: 0x06005C28 RID: 23592 RVA: 0x00218310 File Offset: 0x00216510
	private void Update()
	{
		if (!SteamManager.Initialized)
		{
			return;
		}
		if (Game.Instance != null)
		{
			return;
		}
		this.downloads.ExceptWith(this.removals);
		this.publishes.RemoveWhere((SteamUGCDetails_t publish) => this.removals.Contains(publish.m_nPublishedFileId));
		this.previews.RemoveWhere((SteamUGCDetails_t publish) => this.removals.Contains(publish.m_nPublishedFileId));
		this.proxies.ExceptWith(this.removals);
		HashSetPool<SteamUGCService.Mod, SteamUGCService>.PooledHashSet loaded_previews = HashSetPool<SteamUGCService.Mod, SteamUGCService>.Allocate();
		HashSetPool<PublishedFileId_t, SteamUGCService>.PooledHashSet cancelled_previews = HashSetPool<PublishedFileId_t, SteamUGCService>.Allocate();
		SteamUGCService.Mod mod;
		foreach (SteamUGCDetails_t steamUGCDetails_t in this.previews)
		{
			mod = this.FindMod(steamUGCDetails_t.m_nPublishedFileId);
			DebugUtil.DevAssert(mod != null, "expect mod with pending preview to be published", null);
			mod.previewImage = this.LoadPreviewImage(steamUGCDetails_t);
			if (mod.previewImage != null)
			{
				loaded_previews.Add(mod);
			}
			else if (1000 < this.retry_counts[steamUGCDetails_t.m_nPublishedFileId])
			{
				cancelled_previews.Add(mod.fileId);
			}
		}
		this.previews.RemoveWhere((SteamUGCDetails_t publish) => loaded_previews.Any((SteamUGCService.Mod mod) => mod.fileId == publish.m_nPublishedFileId) || cancelled_previews.Contains(publish.m_nPublishedFileId));
		cancelled_previews.Recycle();
		ListPool<SteamUGCService.Mod, SteamUGCService>.PooledList pooledList = ListPool<SteamUGCService.Mod, SteamUGCService>.Allocate();
		HashSetPool<PublishedFileId_t, SteamUGCService>.PooledHashSet published = HashSetPool<PublishedFileId_t, SteamUGCService>.Allocate();
		foreach (SteamUGCDetails_t steamUGCDetails_t2 in this.publishes)
		{
			if ((SteamUGC.GetItemState(steamUGCDetails_t2.m_nPublishedFileId) & 48U) == 0U)
			{
				global::Debug.LogFormat("Steam: updating info for mod {0}", new object[] { steamUGCDetails_t2.m_rgchTitle });
				SteamUGCService.Mod mod2 = new SteamUGCService.Mod(steamUGCDetails_t2, this.LoadPreviewImage(steamUGCDetails_t2));
				pooledList.Add(mod2);
				if (steamUGCDetails_t2.m_hPreviewFile != UGCHandle_t.Invalid && mod2.previewImage == null)
				{
					this.previews.Add(steamUGCDetails_t2);
				}
				published.Add(steamUGCDetails_t2.m_nPublishedFileId);
			}
		}
		this.publishes.RemoveWhere((SteamUGCDetails_t publish) => published.Contains(publish.m_nPublishedFileId));
		published.Recycle();
		foreach (PublishedFileId_t publishedFileId_t in this.proxies)
		{
			global::Debug.LogFormat("Steam: proxy mod {0}", new object[] { publishedFileId_t });
			pooledList.Add(new SteamUGCService.Mod(publishedFileId_t));
		}
		this.proxies.Clear();
		ListPool<PublishedFileId_t, SteamUGCService>.PooledList pooledList2 = ListPool<PublishedFileId_t, SteamUGCService>.Allocate();
		ListPool<PublishedFileId_t, SteamUGCService>.PooledList pooledList3 = ListPool<PublishedFileId_t, SteamUGCService>.Allocate();
		using (List<SteamUGCService.Mod>.Enumerator enumerator3 = pooledList.GetEnumerator())
		{
			while (enumerator3.MoveNext())
			{
				SteamUGCService.Mod mod = enumerator3.Current;
				int num = this.mods.FindIndex((SteamUGCService.Mod candidate) => candidate.fileId == mod.fileId);
				if (num == -1)
				{
					this.mods.Add(mod);
					pooledList2.Add(mod.fileId);
				}
				else
				{
					this.mods[num] = mod;
					pooledList3.Add(mod.fileId);
				}
			}
		}
		pooledList.Recycle();
		bool flag = this.details_query == UGCQueryHandle_t.Invalid;
		if (pooledList2.Count != 0 || pooledList3.Count != 0 || (flag && this.removals.Count != 0) || loaded_previews.Count != 0)
		{
			foreach (SteamUGCService.IClient client in this.clients)
			{
				IEnumerable<PublishedFileId_t> enumerable = pooledList2;
				IEnumerable<PublishedFileId_t> enumerable2 = pooledList3;
				IEnumerable<PublishedFileId_t> enumerable3;
				if (!flag)
				{
					enumerable3 = Enumerable.Empty<PublishedFileId_t>();
				}
				else
				{
					IEnumerable<PublishedFileId_t> enumerable4 = this.removals;
					enumerable3 = enumerable4;
				}
				client.UpdateMods(enumerable, enumerable2, enumerable3, loaded_previews);
			}
		}
		pooledList2.Recycle();
		pooledList3.Recycle();
		loaded_previews.Recycle();
		if (flag)
		{
			using (HashSet<PublishedFileId_t>.Enumerator enumerator2 = this.removals.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					PublishedFileId_t removal = enumerator2.Current;
					this.mods.RemoveAll((SteamUGCService.Mod candidate) => candidate.fileId == removal);
				}
			}
			this.removals.Clear();
		}
		foreach (PublishedFileId_t publishedFileId_t2 in this.downloads)
		{
			EItemState itemState = (EItemState)SteamUGC.GetItemState(publishedFileId_t2);
			if (((itemState & EItemState.k_EItemStateInstalled) == EItemState.k_EItemStateNone || (itemState & EItemState.k_EItemStateNeedsUpdate) != EItemState.k_EItemStateNone) && (itemState & (EItemState.k_EItemStateDownloading | EItemState.k_EItemStateDownloadPending)) == EItemState.k_EItemStateNone && SteamUGC.DownloadItem(publishedFileId_t2, false))
			{
				this.awaiting_download.Add(publishedFileId_t2);
			}
		}
		if (this.details_query == UGCQueryHandle_t.Invalid)
		{
			this.queries.UnionWith(this.downloads);
			this.queries.ExceptWith(this.awaiting_download);
			this.downloads.Clear();
			if (this.queries.Count != 0)
			{
				this.details_query = SteamUGC.CreateQueryUGCDetailsRequest(this.queries.ToArray<PublishedFileId_t>(), (uint)this.queries.Count);
				SteamAPICall_t steamAPICall_t = SteamUGC.SendQueryUGCRequest(this.details_query);
				this.on_query_completed.Set(steamAPICall_t, null);
			}
		}
	}

	// Token: 0x06005C29 RID: 23593 RVA: 0x002188E8 File Offset: 0x00216AE8
	private void OnSteamUGCQueryDetailsCompleted(SteamUGCQueryCompleted_t pCallback, bool bIOFailure)
	{
		EResult eResult = pCallback.m_eResult;
		if (eResult != EResult.k_EResultOK)
		{
			if (eResult != EResult.k_EResultBusy)
			{
				string[] array = new string[10];
				array[0] = "Steam: [OnSteamUGCQueryDetailsCompleted] - handle: ";
				int num = 1;
				UGCQueryHandle_t ugcqueryHandle_t = pCallback.m_handle;
				array[num] = ugcqueryHandle_t.ToString();
				array[2] = " -- Result: ";
				array[3] = pCallback.m_eResult.ToString();
				array[4] = " -- NUm results: ";
				array[5] = pCallback.m_unNumResultsReturned.ToString();
				array[6] = " --Total Matching: ";
				array[7] = pCallback.m_unTotalMatchingResults.ToString();
				array[8] = " -- cached: ";
				array[9] = pCallback.m_bCachedData.ToString();
				global::Debug.Log(string.Concat(array));
				HashSet<PublishedFileId_t> hashSet = this.proxies;
				this.proxies = this.queries;
				this.queries = hashSet;
			}
			else
			{
				string[] array2 = new string[5];
				array2[0] = "Steam: [OnSteamUGCQueryDetailsCompleted] - handle: ";
				int num2 = 1;
				UGCQueryHandle_t ugcqueryHandle_t = pCallback.m_handle;
				array2[num2] = ugcqueryHandle_t.ToString();
				array2[2] = " -- Result: ";
				array2[3] = pCallback.m_eResult.ToString();
				array2[4] = " Resending";
				global::Debug.Log(string.Concat(array2));
			}
		}
		else
		{
			for (uint num3 = 0U; num3 < pCallback.m_unNumResultsReturned; num3 += 1U)
			{
				SteamUGCDetails_t steamUGCDetails_t = default(SteamUGCDetails_t);
				if (SteamUGC.GetQueryUGCResult(this.details_query, num3, out steamUGCDetails_t))
				{
					if (!this.removals.Contains(steamUGCDetails_t.m_nPublishedFileId))
					{
						this.publishes.Add(steamUGCDetails_t);
						this.retry_counts[steamUGCDetails_t.m_nPublishedFileId] = 0;
					}
					this.queries.Remove(steamUGCDetails_t.m_nPublishedFileId);
				}
				else
				{
					KCrashReporter.ReportDevNotification("SteamUGCService.GetQueryUGCResult details_query is an invalid handle!", Environment.StackTrace, "", false, null);
				}
			}
		}
		SteamUGC.ReleaseQueryUGCRequest(this.details_query);
		this.details_query = UGCQueryHandle_t.Invalid;
	}

	// Token: 0x06005C2A RID: 23594 RVA: 0x00218AB9 File Offset: 0x00216CB9
	private void OnItemSubscribed(RemoteStoragePublishedFileSubscribed_t pCallback)
	{
		this.downloads.Add(pCallback.m_nPublishedFileId);
	}

	// Token: 0x06005C2B RID: 23595 RVA: 0x00218ACD File Offset: 0x00216CCD
	private void OnItemUpdated(RemoteStoragePublishedFileUpdated_t pCallback)
	{
		this.downloads.Add(pCallback.m_nPublishedFileId);
	}

	// Token: 0x06005C2C RID: 23596 RVA: 0x00218AE1 File Offset: 0x00216CE1
	private void OnItemUnsubscribed(RemoteStoragePublishedFileUnsubscribed_t pCallback)
	{
		this.removals.Add(pCallback.m_nPublishedFileId);
	}

	// Token: 0x06005C2D RID: 23597 RVA: 0x00218AF8 File Offset: 0x00216CF8
	private void OnDownloadItemComplete(DownloadItemResult_t callback)
	{
		if (SteamManager.ONI_STEAM_APP_IDS.Contains(callback.m_unAppID) && callback.m_eResult == EResult.k_EResultOK)
		{
			this.queries.Add(callback.m_nPublishedFileId);
			this.awaiting_download.Remove(callback.m_nPublishedFileId);
		}
	}

	// Token: 0x06005C2E RID: 23598 RVA: 0x00218B44 File Offset: 0x00216D44
	public static byte[] GetBytesFromZip(PublishedFileId_t item, string[] filesToExtract, out global::System.DateTime lastModified, bool getFirstMatch = false)
	{
		byte[] array = null;
		lastModified = global::System.DateTime.MinValue;
		ulong num;
		string text;
		uint num2;
		SteamUGC.GetItemInstallInfo(item, out num, out text, 1024U, out num2);
		try
		{
			lastModified = File.GetLastWriteTimeUtc(text);
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (ZipFile zipFile = ZipFile.Read(text))
				{
					ZipEntry zipEntry = null;
					foreach (string text2 in filesToExtract)
					{
						if (text2.Length > 4)
						{
							if (zipFile.ContainsEntry(text2))
							{
								zipEntry = zipFile[text2];
							}
						}
						else
						{
							foreach (ZipEntry zipEntry2 in zipFile.Entries)
							{
								if (zipEntry2.FileName.EndsWith(text2))
								{
									zipEntry = zipEntry2;
									break;
								}
							}
						}
						if (zipEntry != null)
						{
							break;
						}
					}
					if (zipEntry != null)
					{
						zipEntry.Extract(memoryStream);
						memoryStream.Flush();
						array = memoryStream.ToArray();
					}
				}
			}
		}
		catch (Exception)
		{
		}
		return array;
	}

	// Token: 0x04003CFC RID: 15612
	private UGCQueryHandle_t details_query = UGCQueryHandle_t.Invalid;

	// Token: 0x04003CFD RID: 15613
	private Callback<RemoteStoragePublishedFileSubscribed_t> on_subscribed;

	// Token: 0x04003CFE RID: 15614
	private Callback<RemoteStoragePublishedFileUpdated_t> on_updated;

	// Token: 0x04003CFF RID: 15615
	private Callback<RemoteStoragePublishedFileUnsubscribed_t> on_unsubscribed;

	// Token: 0x04003D00 RID: 15616
	private CallResult<SteamUGCQueryCompleted_t> on_query_completed;

	// Token: 0x04003D01 RID: 15617
	private Callback<DownloadItemResult_t> on_download_completed;

	// Token: 0x04003D02 RID: 15618
	private HashSet<PublishedFileId_t> downloads = new HashSet<PublishedFileId_t>();

	// Token: 0x04003D03 RID: 15619
	private HashSet<PublishedFileId_t> queries = new HashSet<PublishedFileId_t>();

	// Token: 0x04003D04 RID: 15620
	private HashSet<PublishedFileId_t> proxies = new HashSet<PublishedFileId_t>();

	// Token: 0x04003D05 RID: 15621
	private HashSet<SteamUGCDetails_t> publishes = new HashSet<SteamUGCDetails_t>();

	// Token: 0x04003D06 RID: 15622
	private HashSet<PublishedFileId_t> removals = new HashSet<PublishedFileId_t>();

	// Token: 0x04003D07 RID: 15623
	private HashSet<SteamUGCDetails_t> previews = new HashSet<SteamUGCDetails_t>();

	// Token: 0x04003D08 RID: 15624
	private HashSet<PublishedFileId_t> awaiting_download = new HashSet<PublishedFileId_t>();

	// Token: 0x04003D09 RID: 15625
	private List<SteamUGCService.Mod> mods = new List<SteamUGCService.Mod>();

	// Token: 0x04003D0A RID: 15626
	private Dictionary<PublishedFileId_t, int> retry_counts = new Dictionary<PublishedFileId_t, int>();

	// Token: 0x04003D0B RID: 15627
	private static readonly string[] previewFileNames = new string[] { "preview.png", "Preview.png", "PREVIEW.png", ".png", ".jpg" };

	// Token: 0x04003D0C RID: 15628
	private List<SteamUGCService.IClient> clients = new List<SteamUGCService.IClient>();

	// Token: 0x04003D0D RID: 15629
	private static SteamUGCService instance;

	// Token: 0x04003D0E RID: 15630
	private const EItemState DOWNLOADING_MASK = EItemState.k_EItemStateDownloading | EItemState.k_EItemStateDownloadPending;

	// Token: 0x04003D0F RID: 15631
	private const int RETRY_THRESHOLD = 1000;

	// Token: 0x02001D2A RID: 7466
	public class Mod
	{
		// Token: 0x0600AD59 RID: 44377 RVA: 0x003C4698 File Offset: 0x003C2898
		public Mod(SteamUGCDetails_t item, Texture2D previewImage)
		{
			this.title = item.m_rgchTitle;
			this.description = item.m_rgchDescription;
			this.fileId = item.m_nPublishedFileId;
			this.lastUpdateTime = (ulong)item.m_rtimeUpdated;
			this.tags = new List<string>(item.m_rgchTags.Split(',', StringSplitOptions.None));
			this.previewImage = previewImage;
		}

		// Token: 0x0600AD5A RID: 44378 RVA: 0x003C46FF File Offset: 0x003C28FF
		public Mod(PublishedFileId_t id)
		{
			this.title = string.Empty;
			this.description = string.Empty;
			this.fileId = id;
			this.lastUpdateTime = 0UL;
			this.tags = new List<string>();
			this.previewImage = null;
		}

		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x0600AD5B RID: 44379 RVA: 0x003C473E File Offset: 0x003C293E
		// (set) Token: 0x0600AD5C RID: 44380 RVA: 0x003C4746 File Offset: 0x003C2946
		public string title { get; private set; }

		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x0600AD5D RID: 44381 RVA: 0x003C474F File Offset: 0x003C294F
		// (set) Token: 0x0600AD5E RID: 44382 RVA: 0x003C4757 File Offset: 0x003C2957
		public string description { get; private set; }

		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x0600AD5F RID: 44383 RVA: 0x003C4760 File Offset: 0x003C2960
		// (set) Token: 0x0600AD60 RID: 44384 RVA: 0x003C4768 File Offset: 0x003C2968
		public PublishedFileId_t fileId { get; private set; }

		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x0600AD61 RID: 44385 RVA: 0x003C4771 File Offset: 0x003C2971
		// (set) Token: 0x0600AD62 RID: 44386 RVA: 0x003C4779 File Offset: 0x003C2979
		public ulong lastUpdateTime { get; private set; }

		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x0600AD63 RID: 44387 RVA: 0x003C4782 File Offset: 0x003C2982
		// (set) Token: 0x0600AD64 RID: 44388 RVA: 0x003C478A File Offset: 0x003C298A
		public List<string> tags { get; private set; }

		// Token: 0x04008865 RID: 34917
		public Texture2D previewImage;
	}

	// Token: 0x02001D2B RID: 7467
	public interface IClient
	{
		// Token: 0x0600AD65 RID: 44389
		void UpdateMods(IEnumerable<PublishedFileId_t> added, IEnumerable<PublishedFileId_t> updated, IEnumerable<PublishedFileId_t> removed, IEnumerable<SteamUGCService.Mod> loaded_previews);
	}
}
