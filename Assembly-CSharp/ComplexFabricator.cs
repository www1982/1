using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Klei;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020006F2 RID: 1778
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/ComplexFabricator")]
public class ComplexFabricator : RemoteDockWorkTargetComponent, ISim200ms, ISim1000ms
{
	// Token: 0x17000242 RID: 578
	// (get) Token: 0x06002C39 RID: 11321 RVA: 0x000FED82 File Offset: 0x000FCF82
	public ComplexFabricatorWorkable Workable
	{
		get
		{
			return this.workable;
		}
	}

	// Token: 0x17000243 RID: 579
	// (get) Token: 0x06002C3A RID: 11322 RVA: 0x000FED8A File Offset: 0x000FCF8A
	// (set) Token: 0x06002C3B RID: 11323 RVA: 0x000FED92 File Offset: 0x000FCF92
	public bool ForbidMutantSeeds
	{
		get
		{
			return this.forbidMutantSeeds;
		}
		set
		{
			this.forbidMutantSeeds = value;
			this.ToggleMutantSeedFetches();
			this.UpdateMutantSeedStatusItem();
		}
	}

	// Token: 0x17000244 RID: 580
	// (get) Token: 0x06002C3C RID: 11324 RVA: 0x000FEDA7 File Offset: 0x000FCFA7
	public Tag[] ForbiddenTags
	{
		get
		{
			if (!this.forbidMutantSeeds)
			{
				return null;
			}
			return this.forbiddenMutantTags;
		}
	}

	// Token: 0x17000245 RID: 581
	// (get) Token: 0x06002C3D RID: 11325 RVA: 0x000FEDB9 File Offset: 0x000FCFB9
	public int CurrentOrderIdx
	{
		get
		{
			return this.nextOrderIdx;
		}
	}

	// Token: 0x17000246 RID: 582
	// (get) Token: 0x06002C3E RID: 11326 RVA: 0x000FEDC1 File Offset: 0x000FCFC1
	public ComplexRecipe CurrentWorkingOrder
	{
		get
		{
			if (!this.HasWorkingOrder)
			{
				return null;
			}
			return this.recipe_list[this.workingOrderIdx];
		}
	}

	// Token: 0x17000247 RID: 583
	// (get) Token: 0x06002C3F RID: 11327 RVA: 0x000FEDDA File Offset: 0x000FCFDA
	public ComplexRecipe NextOrder
	{
		get
		{
			if (!this.nextOrderIsWorkable)
			{
				return null;
			}
			return this.recipe_list[this.nextOrderIdx];
		}
	}

	// Token: 0x17000248 RID: 584
	// (get) Token: 0x06002C40 RID: 11328 RVA: 0x000FEDF3 File Offset: 0x000FCFF3
	// (set) Token: 0x06002C41 RID: 11329 RVA: 0x000FEDFB File Offset: 0x000FCFFB
	public float OrderProgress
	{
		get
		{
			return this.orderProgress;
		}
		set
		{
			this.orderProgress = value;
		}
	}

	// Token: 0x17000249 RID: 585
	// (get) Token: 0x06002C42 RID: 11330 RVA: 0x000FEE04 File Offset: 0x000FD004
	public bool HasAnyOrder
	{
		get
		{
			return this.HasWorkingOrder || this.hasOpenOrders;
		}
	}

	// Token: 0x1700024A RID: 586
	// (get) Token: 0x06002C43 RID: 11331 RVA: 0x000FEE16 File Offset: 0x000FD016
	public bool HasWorker
	{
		get
		{
			return !this.duplicantOperated || this.workable.worker != null;
		}
	}

	// Token: 0x1700024B RID: 587
	// (get) Token: 0x06002C44 RID: 11332 RVA: 0x000FEE33 File Offset: 0x000FD033
	public bool WaitingForWorker
	{
		get
		{
			return this.HasWorkingOrder && !this.HasWorker;
		}
	}

	// Token: 0x1700024C RID: 588
	// (get) Token: 0x06002C45 RID: 11333 RVA: 0x000FEE48 File Offset: 0x000FD048
	private bool HasWorkingOrder
	{
		get
		{
			return this.workingOrderIdx > -1;
		}
	}

	// Token: 0x06002C46 RID: 11334 RVA: 0x000FEE54 File Offset: 0x000FD054
	public List<ComplexRecipe> GetRecipesWithCategoryID(string categoryID)
	{
		return this.recipe_list.Where((ComplexRecipe match) => match.recipeCategoryID == categoryID).ToList<ComplexRecipe>();
	}

	// Token: 0x1700024D RID: 589
	// (get) Token: 0x06002C47 RID: 11335 RVA: 0x000FEE8A File Offset: 0x000FD08A
	public List<FetchList2> DebugFetchLists
	{
		get
		{
			return this.fetchListList;
		}
	}

	// Token: 0x06002C48 RID: 11336 RVA: 0x000FEE94 File Offset: 0x000FD094
	[OnDeserialized]
	protected virtual void OnDeserializedMethod()
	{
		List<string> list = new List<string>();
		foreach (string text in this.recipeQueueCounts.Keys)
		{
			if (ComplexRecipeManager.Get().GetRecipe(text) == null)
			{
				list.Add(text);
			}
		}
		foreach (string text2 in list)
		{
			global::Debug.LogWarningFormat("{1} removing missing recipe from queue: {0}", new object[] { text2, base.name });
			this.recipeQueueCounts.Remove(text2);
		}
	}

	// Token: 0x06002C49 RID: 11337 RVA: 0x000FEF64 File Offset: 0x000FD164
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.GetRecipes();
		this.simRenderLoadBalance = true;
		this.choreType = Db.Get().ChoreTypes.Fabricate;
		base.Subscribe<ComplexFabricator>(-1957399615, ComplexFabricator.OnDroppedAllDelegate);
		base.Subscribe<ComplexFabricator>(-592767678, ComplexFabricator.OnOperationalChangedDelegate);
		base.Subscribe<ComplexFabricator>(-905833192, ComplexFabricator.OnCopySettingsDelegate);
		base.Subscribe<ComplexFabricator>(-1697596308, ComplexFabricator.OnStorageChangeDelegate);
		base.Subscribe<ComplexFabricator>(-1837862626, ComplexFabricator.OnParticleStorageChangedDelegate);
		this.workable = base.GetComponent<ComplexFabricatorWorkable>();
		Components.ComplexFabricators.Add(this);
		base.Subscribe<ComplexFabricator>(493375141, ComplexFabricator.OnRefreshUserMenuDelegate);
	}

	// Token: 0x06002C4A RID: 11338 RVA: 0x000FF018 File Offset: 0x000FD218
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.InitRecipeQueueCount();
		foreach (string text in this.recipeQueueCounts.Keys)
		{
			if (this.recipeQueueCounts[text] == 100)
			{
				this.recipeQueueCounts[text] = ComplexFabricator.QUEUE_INFINITE;
			}
		}
		this.buildStorage.Transfer(this.inStorage, true, true);
		this.DropExcessIngredients(this.inStorage);
		int num = this.FindRecipeIndex(this.lastWorkingRecipe);
		if (num > -1)
		{
			this.nextOrderIdx = num;
		}
		this.UpdateMutantSeedStatusItem();
	}

	// Token: 0x06002C4B RID: 11339 RVA: 0x000FF0D4 File Offset: 0x000FD2D4
	protected override void OnCleanUp()
	{
		this.CancelAllOpenOrders();
		this.CancelChore();
		Components.ComplexFabricators.Remove(this);
		base.OnCleanUp();
	}

	// Token: 0x06002C4C RID: 11340 RVA: 0x000FF0F4 File Offset: 0x000FD2F4
	private void OnRefreshUserMenu(object data)
	{
		if (Game.IsDlcActiveForCurrentSave("EXPANSION1_ID") && this.HasRecipiesWithSeeds())
		{
			Game.Instance.userMenu.AddButton(base.gameObject, new KIconButtonMenu.ButtonInfo("action_switch_toggle", this.ForbidMutantSeeds ? UI.USERMENUACTIONS.ACCEPT_MUTANT_SEEDS.ACCEPT : UI.USERMENUACTIONS.ACCEPT_MUTANT_SEEDS.REJECT, delegate
			{
				this.ForbidMutantSeeds = !this.ForbidMutantSeeds;
				this.OnRefreshUserMenu(null);
			}, global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.ACCEPT_MUTANT_SEEDS.TOOLTIP, true), 1f);
		}
	}

	// Token: 0x06002C4D RID: 11341 RVA: 0x000FF174 File Offset: 0x000FD374
	private bool HasRecipiesWithSeeds()
	{
		bool flag = false;
		ComplexRecipe[] array = this.recipe_list;
		for (int i = 0; i < array.Length; i++)
		{
			ComplexRecipe.RecipeElement[] ingredients = array[i].ingredients;
			for (int j = 0; j < ingredients.Length; j++)
			{
				GameObject prefab = Assets.GetPrefab(ingredients[j].material);
				if (prefab != null && prefab.GetComponent<PlantableSeed>() != null)
				{
					flag = true;
					break;
				}
			}
		}
		return flag;
	}

	// Token: 0x06002C4E RID: 11342 RVA: 0x000FF1E4 File Offset: 0x000FD3E4
	private void UpdateMutantSeedStatusItem()
	{
		base.gameObject.GetComponent<KSelectable>().ToggleStatusItem(Db.Get().BuildingStatusItems.FabricatorAcceptsMutantSeeds, Game.IsDlcActiveForCurrentSave("EXPANSION1_ID") && this.HasRecipiesWithSeeds() && !this.forbidMutantSeeds, null);
	}

	// Token: 0x06002C4F RID: 11343 RVA: 0x000FF235 File Offset: 0x000FD435
	private void OnOperationalChanged(object data)
	{
		if ((bool)data)
		{
			this.queueDirty = true;
		}
		else
		{
			this.CancelAllOpenOrders();
		}
		this.UpdateChore();
	}

	// Token: 0x06002C50 RID: 11344 RVA: 0x000FF254 File Offset: 0x000FD454
	public virtual void Sim1000ms(float dt)
	{
		this.RefreshAndStartNextOrder();
		if (this.materialNeedCache.Count > 0 && this.fetchListList.Count == 0)
		{
			global::Debug.LogWarningFormat(base.gameObject, "{0} has material needs cached, but no open fetches. materialNeedCache={1}, fetchListList={2}", new object[]
			{
				base.gameObject,
				this.materialNeedCache.Count,
				this.fetchListList.Count
			});
			this.queueDirty = true;
		}
	}

	// Token: 0x06002C51 RID: 11345 RVA: 0x000FF2CE File Offset: 0x000FD4CE
	protected virtual float ComputeWorkProgress(float dt, ComplexRecipe recipe)
	{
		return dt / recipe.time;
	}

	// Token: 0x06002C52 RID: 11346 RVA: 0x000FF2D8 File Offset: 0x000FD4D8
	public void Sim200ms(float dt)
	{
		if (!this.operational.IsOperational)
		{
			return;
		}
		this.operational.SetActive(this.HasWorkingOrder && this.HasWorker, false);
		if (!this.duplicantOperated && this.HasWorkingOrder)
		{
			this.orderProgress += this.ComputeWorkProgress(dt, this.recipe_list[this.workingOrderIdx]);
			if (this.orderProgress >= 1f)
			{
				this.ShowProgressBar(false);
				this.CompleteWorkingOrder();
			}
		}
	}

	// Token: 0x06002C53 RID: 11347 RVA: 0x000FF35C File Offset: 0x000FD55C
	private void RefreshAndStartNextOrder()
	{
		if (!this.operational.IsOperational)
		{
			return;
		}
		if (this.queueDirty)
		{
			this.RefreshQueue();
		}
		if (!this.HasWorkingOrder && this.nextOrderIsWorkable)
		{
			this.ShowProgressBar(true);
			this.StartWorkingOrder(this.nextOrderIdx);
		}
	}

	// Token: 0x06002C54 RID: 11348 RVA: 0x000FF3A8 File Offset: 0x000FD5A8
	public virtual float GetPercentComplete()
	{
		return this.orderProgress;
	}

	// Token: 0x06002C55 RID: 11349 RVA: 0x000FF3B0 File Offset: 0x000FD5B0
	private void ShowProgressBar(bool show)
	{
		if (show && this.showProgressBar && !this.duplicantOperated)
		{
			if (this.progressBar == null)
			{
				this.progressBar = ProgressBar.CreateProgressBar(base.gameObject, new Func<float>(this.GetPercentComplete));
			}
			this.progressBar.enabled = true;
			this.progressBar.SetVisibility(true);
			return;
		}
		if (this.progressBar != null)
		{
			this.progressBar.gameObject.DeleteObject();
			this.progressBar = null;
		}
	}

	// Token: 0x06002C56 RID: 11350 RVA: 0x000FF43A File Offset: 0x000FD63A
	public void SetQueueDirty()
	{
		this.queueDirty = true;
	}

	// Token: 0x06002C57 RID: 11351 RVA: 0x000FF443 File Offset: 0x000FD643
	private void RefreshQueue()
	{
		this.queueDirty = false;
		this.ValidateWorkingOrder();
		this.ValidateNextOrder();
		this.UpdateOpenOrders();
		this.DropExcessIngredients(this.inStorage);
		base.Trigger(1721324763, this);
	}

	// Token: 0x06002C58 RID: 11352 RVA: 0x000FF478 File Offset: 0x000FD678
	private void StartWorkingOrder(int index)
	{
		global::Debug.Assert(!this.HasWorkingOrder, "machineOrderIdx already set");
		this.workingOrderIdx = index;
		if (this.recipe_list[this.workingOrderIdx].id != this.lastWorkingRecipe)
		{
			this.orderProgress = 0f;
			this.lastWorkingRecipe = this.recipe_list[this.workingOrderIdx].id;
		}
		this.TransferCurrentRecipeIngredientsForBuild();
		global::Debug.Assert(this.openOrderCounts[this.workingOrderIdx] > 0, "openOrderCount invalid");
		List<int> list = this.openOrderCounts;
		int num = this.workingOrderIdx;
		int num2 = list[num];
		list[num] = num2 - 1;
		this.UpdateChore();
		base.Trigger(2023536846, this.recipe_list[this.workingOrderIdx]);
		this.AdvanceNextOrder();
	}

	// Token: 0x06002C59 RID: 11353 RVA: 0x000FF547 File Offset: 0x000FD747
	private void CancelWorkingOrder()
	{
		global::Debug.Assert(this.HasWorkingOrder, "machineOrderIdx not set");
		this.buildStorage.Transfer(this.inStorage, true, true);
		this.workingOrderIdx = -1;
		this.orderProgress = 0f;
		this.UpdateChore();
	}

	// Token: 0x06002C5A RID: 11354 RVA: 0x000FF584 File Offset: 0x000FD784
	public virtual void CompleteWorkingOrder()
	{
		if (!this.HasWorkingOrder)
		{
			global::Debug.LogWarning("CompleteWorkingOrder called with no working order.", base.gameObject);
			return;
		}
		ComplexRecipe complexRecipe = this.recipe_list[this.workingOrderIdx];
		this.SpawnOrderProduct(complexRecipe);
		float num = this.buildStorage.MassStored();
		if (num != 0f)
		{
			global::Debug.LogWarningFormat(base.gameObject, "{0} build storage contains mass {1} after order completion.", new object[] { base.gameObject, num });
			this.buildStorage.Transfer(this.inStorage, true, true);
		}
		this.DecrementRecipeQueueCountInternal(complexRecipe, true);
		this.workingOrderIdx = -1;
		this.orderProgress = 0f;
		this.CancelChore();
		base.Trigger(1355439576, complexRecipe);
		if (!this.cancelling)
		{
			this.RefreshAndStartNextOrder();
		}
	}

	// Token: 0x06002C5B RID: 11355 RVA: 0x000FF64C File Offset: 0x000FD84C
	private void ValidateWorkingOrder()
	{
		if (!this.HasWorkingOrder)
		{
			return;
		}
		ComplexRecipe complexRecipe = this.recipe_list[this.workingOrderIdx];
		if (!this.IsRecipeQueued(complexRecipe))
		{
			this.CancelWorkingOrder();
		}
	}

	// Token: 0x06002C5C RID: 11356 RVA: 0x000FF680 File Offset: 0x000FD880
	private void UpdateChore()
	{
		if (!this.duplicantOperated)
		{
			return;
		}
		bool flag = this.operational.IsOperational && this.HasWorkingOrder;
		if (flag && this.chore == null)
		{
			this.CreateChore();
			return;
		}
		if (!flag && this.chore != null)
		{
			this.CancelChore();
		}
	}

	// Token: 0x06002C5D RID: 11357 RVA: 0x000FF6D0 File Offset: 0x000FD8D0
	private void AdvanceNextOrder()
	{
		for (int i = 0; i < this.recipe_list.Length; i++)
		{
			this.nextOrderIdx = (this.nextOrderIdx + 1) % this.recipe_list.Length;
			ComplexRecipe complexRecipe = this.recipe_list[this.nextOrderIdx];
			this.nextOrderIsWorkable = this.GetRemainingQueueCount(complexRecipe) > 0 && this.HasIngredients(complexRecipe, this.inStorage);
			if (this.nextOrderIsWorkable)
			{
				break;
			}
		}
	}

	// Token: 0x06002C5E RID: 11358 RVA: 0x000FF740 File Offset: 0x000FD940
	private void ValidateNextOrder()
	{
		ComplexRecipe complexRecipe = this.recipe_list[this.nextOrderIdx];
		this.nextOrderIsWorkable = this.GetRemainingQueueCount(complexRecipe) > 0 && this.HasIngredients(complexRecipe, this.inStorage);
		if (!this.nextOrderIsWorkable)
		{
			this.AdvanceNextOrder();
		}
	}

	// Token: 0x06002C5F RID: 11359 RVA: 0x000FF78C File Offset: 0x000FD98C
	private void CancelAllOpenOrders()
	{
		for (int i = 0; i < this.openOrderCounts.Count; i++)
		{
			this.openOrderCounts[i] = 0;
		}
		this.ClearMaterialNeeds();
		this.CancelFetches();
	}

	// Token: 0x06002C60 RID: 11360 RVA: 0x000FF7C8 File Offset: 0x000FD9C8
	private void UpdateOpenOrders()
	{
		ComplexRecipe[] recipes = this.GetRecipes();
		if (recipes.Length != this.openOrderCounts.Count)
		{
			global::Debug.LogErrorFormat(base.gameObject, "Recipe count {0} doesn't match open order count {1}", new object[]
			{
				recipes.Length,
				this.openOrderCounts.Count
			});
		}
		bool flag = false;
		this.hasOpenOrders = false;
		for (int i = 0; i < recipes.Length; i++)
		{
			ComplexRecipe complexRecipe = recipes[i];
			int recipePrefetchCount = this.GetRecipePrefetchCount(complexRecipe);
			if (recipePrefetchCount > 0)
			{
				this.hasOpenOrders = true;
			}
			int num = this.openOrderCounts[i];
			if (num != recipePrefetchCount)
			{
				if (recipePrefetchCount < num)
				{
					flag = true;
				}
				this.openOrderCounts[i] = recipePrefetchCount;
			}
		}
		DictionaryPool<Tag, float, ComplexFabricator>.PooledDictionary pooledDictionary = DictionaryPool<Tag, float, ComplexFabricator>.Allocate();
		DictionaryPool<Tag, float, ComplexFabricator>.PooledDictionary pooledDictionary2 = DictionaryPool<Tag, float, ComplexFabricator>.Allocate();
		for (int j = 0; j < this.openOrderCounts.Count; j++)
		{
			if (this.openOrderCounts[j] > 0)
			{
				foreach (ComplexRecipe.RecipeElement recipeElement in this.recipe_list[j].ingredients)
				{
					pooledDictionary[recipeElement.material] = this.inStorage.GetAmountAvailable(recipeElement.material);
				}
			}
		}
		for (int l = 0; l < this.recipe_list.Length; l++)
		{
			int num2 = this.openOrderCounts[l];
			if (num2 > 0)
			{
				foreach (ComplexRecipe.RecipeElement recipeElement2 in this.recipe_list[l].ingredients)
				{
					float num3 = recipeElement2.amount * (float)num2;
					float num4 = num3 - pooledDictionary[recipeElement2.material];
					if (num4 > 0f)
					{
						float num5;
						pooledDictionary2.TryGetValue(recipeElement2.material, out num5);
						num4 *= FetchChore.GetMinimumFetchAmount(recipeElement2.material, 1f);
						pooledDictionary2[recipeElement2.material] = num5 + num4;
						pooledDictionary[recipeElement2.material] = 0f;
					}
					else
					{
						DictionaryPool<Tag, float, ComplexFabricator>.PooledDictionary pooledDictionary3 = pooledDictionary;
						Tag material = recipeElement2.material;
						pooledDictionary3[material] -= num3;
					}
				}
			}
		}
		if (flag)
		{
			this.CancelFetches();
		}
		if (pooledDictionary2.Count > 0)
		{
			this.UpdateFetches(pooledDictionary2);
		}
		this.UpdateMaterialNeeds(pooledDictionary2);
		pooledDictionary2.Recycle();
		pooledDictionary.Recycle();
	}

	// Token: 0x06002C61 RID: 11361 RVA: 0x000FFA28 File Offset: 0x000FDC28
	private void UpdateMaterialNeeds(Dictionary<Tag, float> missingAmounts)
	{
		this.ClearMaterialNeeds();
		foreach (KeyValuePair<Tag, float> keyValuePair in missingAmounts)
		{
			MaterialNeeds.UpdateNeed(keyValuePair.Key, keyValuePair.Value, base.gameObject.GetMyWorldId());
			this.materialNeedCache.Add(keyValuePair.Key, keyValuePair.Value);
		}
	}

	// Token: 0x06002C62 RID: 11362 RVA: 0x000FFAAC File Offset: 0x000FDCAC
	private void ClearMaterialNeeds()
	{
		foreach (KeyValuePair<Tag, float> keyValuePair in this.materialNeedCache)
		{
			MaterialNeeds.UpdateNeed(keyValuePair.Key, -keyValuePair.Value, base.gameObject.GetMyWorldId());
		}
		this.materialNeedCache.Clear();
	}

	// Token: 0x06002C63 RID: 11363 RVA: 0x000FFB24 File Offset: 0x000FDD24
	public int HighestHEPQueued()
	{
		int num = 0;
		foreach (KeyValuePair<string, int> keyValuePair in this.recipeQueueCounts)
		{
			if (keyValuePair.Value > 0)
			{
				num = Math.Max(this.recipe_list[this.FindRecipeIndex(keyValuePair.Key)].consumedHEP, num);
			}
		}
		return num;
	}

	// Token: 0x06002C64 RID: 11364 RVA: 0x000FFBA0 File Offset: 0x000FDDA0
	private void OnFetchComplete()
	{
		for (int i = this.fetchListList.Count - 1; i >= 0; i--)
		{
			if (this.fetchListList[i].IsComplete)
			{
				this.fetchListList.RemoveAt(i);
				this.queueDirty = true;
			}
		}
	}

	// Token: 0x06002C65 RID: 11365 RVA: 0x000FFBEB File Offset: 0x000FDDEB
	private void OnStorageChange(object data)
	{
		this.queueDirty = true;
	}

	// Token: 0x06002C66 RID: 11366 RVA: 0x000FFBF4 File Offset: 0x000FDDF4
	private void OnDroppedAll(object data)
	{
		if (this.HasWorkingOrder)
		{
			this.CancelWorkingOrder();
		}
		this.CancelAllOpenOrders();
		this.RefreshQueue();
	}

	// Token: 0x06002C67 RID: 11367 RVA: 0x000FFC10 File Offset: 0x000FDE10
	private void DropExcessIngredients(Storage storage)
	{
		HashSet<Tag> hashSet = new HashSet<Tag>();
		if (this.keepAdditionalTag != Tag.Invalid)
		{
			hashSet.Add(this.keepAdditionalTag);
		}
		for (int i = 0; i < this.recipe_list.Length; i++)
		{
			ComplexRecipe complexRecipe = this.recipe_list[i];
			if (this.IsRecipeQueued(complexRecipe))
			{
				foreach (ComplexRecipe.RecipeElement recipeElement in complexRecipe.ingredients)
				{
					hashSet.Add(recipeElement.material);
				}
			}
		}
		for (int k = storage.items.Count - 1; k >= 0; k--)
		{
			GameObject gameObject = storage.items[k];
			if (!(gameObject == null))
			{
				PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
				if (!(component == null) && (!this.keepExcessLiquids || !component.Element.IsLiquid))
				{
					KPrefabID component2 = gameObject.GetComponent<KPrefabID>();
					if (component2 && !hashSet.Contains(component2.PrefabID()))
					{
						storage.Drop(gameObject, true);
					}
				}
			}
		}
	}

	// Token: 0x06002C68 RID: 11368 RVA: 0x000FFD20 File Offset: 0x000FDF20
	private void OnCopySettings(object data)
	{
		GameObject gameObject = (GameObject)data;
		if (gameObject == null)
		{
			return;
		}
		ComplexFabricator component = gameObject.GetComponent<ComplexFabricator>();
		if (component == null)
		{
			return;
		}
		this.ForbidMutantSeeds = component.ForbidMutantSeeds;
		foreach (ComplexRecipe complexRecipe in this.recipe_list)
		{
			int num;
			if (!component.recipeQueueCounts.TryGetValue(complexRecipe.id, out num))
			{
				num = 0;
			}
			this.SetRecipeQueueCountInternal(complexRecipe, num);
		}
		this.RefreshQueue();
	}

	// Token: 0x06002C69 RID: 11369 RVA: 0x000FFD9E File Offset: 0x000FDF9E
	private int CompareRecipe(ComplexRecipe a, ComplexRecipe b)
	{
		if (a.sortOrder != b.sortOrder)
		{
			return a.sortOrder - b.sortOrder;
		}
		return StringComparer.InvariantCulture.Compare(a.id, b.id);
	}

	// Token: 0x06002C6A RID: 11370 RVA: 0x000FFDD4 File Offset: 0x000FDFD4
	public ComplexRecipe GetRecipe(string id)
	{
		if (this.recipe_list != null)
		{
			foreach (ComplexRecipe complexRecipe in this.recipe_list)
			{
				if (complexRecipe.id == id)
				{
					return complexRecipe;
				}
			}
		}
		return null;
	}

	// Token: 0x06002C6B RID: 11371 RVA: 0x000FFE14 File Offset: 0x000FE014
	public ComplexRecipe[] GetRecipes()
	{
		if (this.recipe_list == null)
		{
			Tag prefabTag = base.GetComponent<KPrefabID>().PrefabTag;
			List<ComplexRecipe> recipes = ComplexRecipeManager.Get().recipes;
			List<ComplexRecipe> list = new List<ComplexRecipe>();
			foreach (ComplexRecipe complexRecipe in recipes)
			{
				using (List<Tag>.Enumerator enumerator2 = complexRecipe.fabricators.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						if (enumerator2.Current == prefabTag && Game.IsCorrectDlcActiveForCurrentSave(complexRecipe))
						{
							list.Add(complexRecipe);
						}
					}
				}
			}
			this.recipe_list = list.ToArray();
			Array.Sort<ComplexRecipe>(this.recipe_list, new Comparison<ComplexRecipe>(this.CompareRecipe));
			foreach (ComplexRecipe complexRecipe2 in this.recipe_list)
			{
				if (!this.mostRecentRecipeSelectionByCategory.ContainsKey(complexRecipe2.recipeCategoryID))
				{
					this.mostRecentRecipeSelectionByCategory.Add(complexRecipe2.recipeCategoryID, null);
				}
			}
		}
		return this.recipe_list;
	}

	// Token: 0x06002C6C RID: 11372 RVA: 0x000FFF48 File Offset: 0x000FE148
	private void InitRecipeQueueCount()
	{
		foreach (ComplexRecipe complexRecipe in this.GetRecipes())
		{
			bool flag = false;
			using (Dictionary<string, int>.KeyCollection.Enumerator enumerator = this.recipeQueueCounts.Keys.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current == complexRecipe.id)
					{
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				this.recipeQueueCounts.Add(complexRecipe.id, 0);
			}
			this.openOrderCounts.Add(0);
		}
	}

	// Token: 0x06002C6D RID: 11373 RVA: 0x000FFFE8 File Offset: 0x000FE1E8
	private int FindRecipeIndex(string id)
	{
		for (int i = 0; i < this.recipe_list.Length; i++)
		{
			if (this.recipe_list[i].id == id)
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x06002C6E RID: 11374 RVA: 0x00100020 File Offset: 0x000FE220
	public int GetRecipeQueueCount(ComplexRecipe recipe)
	{
		return this.recipeQueueCounts[recipe.id];
	}

	// Token: 0x06002C6F RID: 11375 RVA: 0x00100034 File Offset: 0x000FE234
	public int GetIngredientQueueCount(string recipeCategoryID, Tag tag)
	{
		int num = 0;
		foreach (ComplexRecipe complexRecipe in this.GetRecipesWithCategoryID(recipeCategoryID))
		{
			ComplexRecipe.RecipeElement[] ingredients = complexRecipe.ingredients;
			for (int i = 0; i < ingredients.Length; i++)
			{
				if (ingredients[i].material == tag)
				{
					num += this.GetRecipeQueueCount(complexRecipe);
					break;
				}
			}
		}
		return num;
	}

	// Token: 0x06002C70 RID: 11376 RVA: 0x001000BC File Offset: 0x000FE2BC
	public int GetRecipeCategoryQueueCount(string recipeCategoryID)
	{
		int num = 0;
		IEnumerable<ComplexRecipe> enumerable = this.recipe_list;
		Func<ComplexRecipe, bool> <>9__0;
		Func<ComplexRecipe, bool> func;
		if ((func = <>9__0) == null)
		{
			func = (<>9__0 = (ComplexRecipe match) => match.recipeCategoryID == recipeCategoryID);
		}
		foreach (ComplexRecipe complexRecipe in enumerable.Where(func))
		{
			if (this.recipeQueueCounts[complexRecipe.id] == ComplexFabricator.QUEUE_INFINITE)
			{
				return ComplexFabricator.QUEUE_INFINITE;
			}
			num += this.recipeQueueCounts[complexRecipe.id];
		}
		return num;
	}

	// Token: 0x06002C71 RID: 11377 RVA: 0x00100174 File Offset: 0x000FE374
	public bool IsRecipeQueued(ComplexRecipe recipe)
	{
		int num = this.recipeQueueCounts[recipe.id];
		global::Debug.Assert(num >= 0 || num == ComplexFabricator.QUEUE_INFINITE);
		return num != 0;
	}

	// Token: 0x06002C72 RID: 11378 RVA: 0x001001AC File Offset: 0x000FE3AC
	public int GetRecipePrefetchCount(ComplexRecipe recipe)
	{
		int remainingQueueCount = this.GetRemainingQueueCount(recipe);
		global::Debug.Assert(remainingQueueCount >= 0);
		return Mathf.Min(2, remainingQueueCount);
	}

	// Token: 0x06002C73 RID: 11379 RVA: 0x001001D4 File Offset: 0x000FE3D4
	private int GetRemainingQueueCount(ComplexRecipe recipe)
	{
		int num = this.recipeQueueCounts[recipe.id];
		global::Debug.Assert(num >= 0 || num == ComplexFabricator.QUEUE_INFINITE);
		if (num == ComplexFabricator.QUEUE_INFINITE)
		{
			return ComplexFabricator.MAX_QUEUE_SIZE;
		}
		if (num > 0)
		{
			if (this.IsCurrentRecipe(recipe))
			{
				num--;
			}
			return num;
		}
		return 0;
	}

	// Token: 0x06002C74 RID: 11380 RVA: 0x00100229 File Offset: 0x000FE429
	private bool IsCurrentRecipe(ComplexRecipe recipe)
	{
		return this.workingOrderIdx >= 0 && this.recipe_list[this.workingOrderIdx].id == recipe.id;
	}

	// Token: 0x06002C75 RID: 11381 RVA: 0x00100253 File Offset: 0x000FE453
	public void SetRecipeQueueCount(ComplexRecipe recipe, int count)
	{
		this.SetRecipeQueueCountInternal(recipe, count);
		this.RefreshQueue();
	}

	// Token: 0x06002C76 RID: 11382 RVA: 0x00100263 File Offset: 0x000FE463
	private void SetRecipeQueueCountInternal(ComplexRecipe recipe, int count)
	{
		this.recipeQueueCounts[recipe.id] = count;
	}

	// Token: 0x06002C77 RID: 11383 RVA: 0x00100278 File Offset: 0x000FE478
	public void IncrementRecipeQueueCount(ComplexRecipe recipe)
	{
		if (this.recipeQueueCounts[recipe.id] == ComplexFabricator.QUEUE_INFINITE)
		{
			this.recipeQueueCounts[recipe.id] = 0;
		}
		else if (this.recipeQueueCounts[recipe.id] >= ComplexFabricator.MAX_QUEUE_SIZE)
		{
			this.recipeQueueCounts[recipe.id] = ComplexFabricator.QUEUE_INFINITE;
		}
		else
		{
			Dictionary<string, int> dictionary = this.recipeQueueCounts;
			string id = recipe.id;
			int num = dictionary[id];
			dictionary[id] = num + 1;
		}
		this.RefreshQueue();
	}

	// Token: 0x06002C78 RID: 11384 RVA: 0x00100305 File Offset: 0x000FE505
	public void DecrementRecipeQueueCount(ComplexRecipe recipe, bool respectInfinite = true)
	{
		this.DecrementRecipeQueueCountInternal(recipe, respectInfinite);
		this.RefreshQueue();
	}

	// Token: 0x06002C79 RID: 11385 RVA: 0x00100318 File Offset: 0x000FE518
	private void DecrementRecipeQueueCountInternal(ComplexRecipe recipe, bool respectInfinite = true)
	{
		if (!respectInfinite || this.recipeQueueCounts[recipe.id] != ComplexFabricator.QUEUE_INFINITE)
		{
			if (this.recipeQueueCounts[recipe.id] == ComplexFabricator.QUEUE_INFINITE)
			{
				this.recipeQueueCounts[recipe.id] = ComplexFabricator.MAX_QUEUE_SIZE;
				return;
			}
			if (this.recipeQueueCounts[recipe.id] == 0)
			{
				this.recipeQueueCounts[recipe.id] = ComplexFabricator.QUEUE_INFINITE;
				return;
			}
			Dictionary<string, int> dictionary = this.recipeQueueCounts;
			string id = recipe.id;
			int num = dictionary[id];
			dictionary[id] = num - 1;
		}
	}

	// Token: 0x06002C7A RID: 11386 RVA: 0x001003B7 File Offset: 0x000FE5B7
	private void CreateChore()
	{
		global::Debug.Assert(this.chore == null, "chore should be null");
		this.chore = this.workable.CreateWorkChore(this.choreType, this.orderProgress);
	}

	// Token: 0x1700024E RID: 590
	// (get) Token: 0x06002C7B RID: 11387 RVA: 0x001003E9 File Offset: 0x000FE5E9
	public override Chore RemoteDockChore
	{
		get
		{
			if (!this.duplicantOperated)
			{
				return null;
			}
			return this.chore;
		}
	}

	// Token: 0x06002C7C RID: 11388 RVA: 0x001003FB File Offset: 0x000FE5FB
	private void CancelChore()
	{
		if (this.cancelling)
		{
			return;
		}
		this.cancelling = true;
		if (this.chore != null)
		{
			this.chore.Cancel("order cancelled");
			this.chore = null;
		}
		this.cancelling = false;
	}

	// Token: 0x06002C7D RID: 11389 RVA: 0x00100434 File Offset: 0x000FE634
	private void UpdateFetches(DictionaryPool<Tag, float, ComplexFabricator>.PooledDictionary missingAmounts)
	{
		ChoreType byHash = Db.Get().ChoreTypes.GetByHash(this.fetchChoreTypeIdHash);
		foreach (KeyValuePair<Tag, float> keyValuePair in missingAmounts)
		{
			if (!this.allowManualFluidDelivery)
			{
				Element element = ElementLoader.GetElement(keyValuePair.Key);
				if (element != null && (element.IsLiquid || element.IsGas))
				{
					continue;
				}
			}
			if (keyValuePair.Value >= PICKUPABLETUNING.MINIMUM_PICKABLE_AMOUNT && !this.HasPendingFetch(keyValuePair.Key))
			{
				FetchList2 fetchList = new FetchList2(this.inStorage, byHash);
				FetchList2 fetchList2 = fetchList;
				Tag key = keyValuePair.Key;
				float value = keyValuePair.Value;
				fetchList2.Add(key, this.ForbiddenTags, value, Operational.State.None);
				fetchList.ShowStatusItem = false;
				fetchList.Submit(new global::System.Action(this.OnFetchComplete), false);
				this.fetchListList.Add(fetchList);
			}
		}
	}

	// Token: 0x06002C7E RID: 11390 RVA: 0x00100534 File Offset: 0x000FE734
	private bool HasPendingFetch(Tag tag)
	{
		foreach (FetchList2 fetchList in this.fetchListList)
		{
			float num;
			fetchList.MinimumAmount.TryGetValue(tag, out num);
			if (num > 0f)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06002C7F RID: 11391 RVA: 0x0010059C File Offset: 0x000FE79C
	private void CancelFetches()
	{
		foreach (FetchList2 fetchList in this.fetchListList)
		{
			fetchList.Cancel("cancel all orders");
		}
		this.fetchListList.Clear();
	}

	// Token: 0x06002C80 RID: 11392 RVA: 0x001005FC File Offset: 0x000FE7FC
	protected virtual void TransferCurrentRecipeIngredientsForBuild()
	{
		ComplexRecipe.RecipeElement[] ingredients = this.recipe_list[this.workingOrderIdx].ingredients;
		int i = 0;
		while (i < ingredients.Length)
		{
			ComplexRecipe.RecipeElement recipeElement = ingredients[i];
			float num;
			for (;;)
			{
				num = recipeElement.amount - this.buildStorage.GetAmountAvailable(recipeElement.material);
				if (num <= 0f)
				{
					break;
				}
				if (this.inStorage.GetAmountAvailable(recipeElement.material) <= 0f)
				{
					goto Block_2;
				}
				this.inStorage.TransferUnitMass(this.buildStorage, recipeElement.material, num, false, false, true);
			}
			IL_009D:
			i++;
			continue;
			Block_2:
			global::Debug.LogWarningFormat("TransferCurrentRecipeIngredientsForBuild ran out of {0} but still needed {1} more.", new object[] { recipeElement.material, num });
			goto IL_009D;
		}
	}

	// Token: 0x06002C81 RID: 11393 RVA: 0x001006B4 File Offset: 0x000FE8B4
	protected virtual bool HasIngredients(ComplexRecipe recipe, Storage storage)
	{
		ComplexRecipe.RecipeElement[] ingredients = recipe.ingredients;
		if (recipe.consumedHEP > 0)
		{
			HighEnergyParticleStorage component = base.GetComponent<HighEnergyParticleStorage>();
			if (component == null || component.Particles < (float)recipe.consumedHEP)
			{
				return false;
			}
		}
		foreach (ComplexRecipe.RecipeElement recipeElement in ingredients)
		{
			float amountAvailable = storage.GetAmountAvailable(recipeElement.material);
			if (recipeElement.amount - amountAvailable >= PICKUPABLETUNING.MINIMUM_PICKABLE_AMOUNT)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06002C82 RID: 11394 RVA: 0x0010072C File Offset: 0x000FE92C
	private void ToggleMutantSeedFetches()
	{
		if (this.HasAnyOrder)
		{
			ChoreType byHash = Db.Get().ChoreTypes.GetByHash(this.fetchChoreTypeIdHash);
			List<FetchList2> list = new List<FetchList2>();
			foreach (FetchList2 fetchList in this.fetchListList)
			{
				foreach (FetchOrder2 fetchOrder in fetchList.FetchOrders)
				{
					foreach (Tag tag in fetchOrder.Tags)
					{
						GameObject prefab = Assets.GetPrefab(tag);
						if (prefab != null && prefab.GetComponent<PlantableSeed>() != null)
						{
							fetchList.Cancel("MutantSeedTagChanged");
							list.Add(fetchList);
						}
					}
				}
			}
			foreach (FetchList2 fetchList2 in list)
			{
				this.fetchListList.Remove(fetchList2);
				foreach (FetchOrder2 fetchOrder2 in fetchList2.FetchOrders)
				{
					foreach (Tag tag2 in fetchOrder2.Tags)
					{
						FetchList2 fetchList3 = new FetchList2(this.inStorage, byHash);
						FetchList2 fetchList4 = fetchList3;
						Tag tag3 = tag2;
						float totalAmount = fetchOrder2.TotalAmount;
						fetchList4.Add(tag3, this.ForbiddenTags, totalAmount, Operational.State.None);
						fetchList3.ShowStatusItem = false;
						fetchList3.Submit(new global::System.Action(this.OnFetchComplete), false);
						this.fetchListList.Add(fetchList3);
					}
				}
			}
		}
	}

	// Token: 0x06002C83 RID: 11395 RVA: 0x0010096C File Offset: 0x000FEB6C
	protected virtual List<GameObject> SpawnOrderProduct(ComplexRecipe recipe)
	{
		List<GameObject> list = new List<GameObject>();
		SimUtil.DiseaseInfo diseaseInfo;
		diseaseInfo.count = 0;
		diseaseInfo.idx = 0;
		float num = 0f;
		float num2 = 0f;
		string text = null;
		foreach (ComplexRecipe.RecipeElement recipeElement in recipe.ingredients)
		{
			num2 += recipeElement.amount;
		}
		ComplexRecipe.RecipeElement recipeElement2 = null;
		Element element = null;
		foreach (ComplexRecipe.RecipeElement recipeElement3 in recipe.ingredients)
		{
			float num3 = recipeElement3.amount / num2;
			if (recipe.ProductHasFacade && text.IsNullOrWhiteSpace())
			{
				RepairableEquipment component = this.buildStorage.FindFirst(recipeElement3.material).GetComponent<RepairableEquipment>();
				if (component != null)
				{
					text = component.facadeID;
				}
			}
			if (recipeElement3.inheritElement)
			{
				recipeElement2 = recipeElement3;
				element = this.buildStorage.FindFirst(recipeElement3.material).GetComponent<PrimaryElement>().Element;
			}
			if (recipeElement3.doNotConsume)
			{
				recipeElement2 = recipeElement3;
				this.buildStorage.TransferMass(this.outStorage, recipeElement3.material, recipeElement3.amount, true, true, true);
			}
			else
			{
				float num4;
				SimUtil.DiseaseInfo diseaseInfo2;
				float num5;
				this.buildStorage.ConsumeAndGetDisease(recipeElement3.material, recipeElement3.amount, out num4, out diseaseInfo2, out num5);
				if (diseaseInfo2.count > diseaseInfo.count)
				{
					diseaseInfo = diseaseInfo2;
				}
				num += num5 * num3;
			}
		}
		if (recipe.consumedHEP > 0)
		{
			base.GetComponent<HighEnergyParticleStorage>().ConsumeAndGet((float)recipe.consumedHEP);
		}
		foreach (ComplexRecipe.RecipeElement recipeElement4 in recipe.results)
		{
			GameObject gameObject = this.buildStorage.FindFirst(recipeElement4.material);
			if (gameObject != null)
			{
				Edible component2 = gameObject.GetComponent<Edible>();
				if (component2)
				{
					ReportManager.Instance.ReportValue(ReportManager.ReportType.CaloriesCreated, -component2.Calories, StringFormatter.Replace(UI.ENDOFDAYREPORT.NOTES.CRAFTED_USED, "{0}", component2.GetProperName()), UI.ENDOFDAYREPORT.NOTES.CRAFTED_CONTEXT);
				}
			}
			switch (recipeElement4.temperatureOperation)
			{
			case ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature:
			case ComplexRecipe.RecipeElement.TemperatureOperation.Heated:
			{
				GameObject gameObject2 = GameUtil.KInstantiate(Assets.GetPrefab(recipeElement4.material), Grid.SceneLayer.Ore, null, 0);
				int num6 = Grid.PosToCell(this);
				gameObject2.transform.SetPosition(Grid.CellToPosCCC(num6, Grid.SceneLayer.Ore) + this.outputOffset);
				PrimaryElement component3 = gameObject2.GetComponent<PrimaryElement>();
				component3.Units = recipeElement4.amount;
				component3.Temperature = ((recipeElement4.temperatureOperation == ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature) ? num : this.heatedTemperature);
				if (element != null)
				{
					component3.SetElement(element.id, false);
				}
				if (recipe.ProductHasFacade && !text.IsNullOrWhiteSpace())
				{
					Equippable component4 = gameObject2.GetComponent<Equippable>();
					if (component4 != null)
					{
						EquippableFacade.AddFacadeToEquippable(component4, text);
					}
				}
				gameObject2.SetActive(true);
				float num7 = recipeElement4.amount / recipe.TotalResultUnits();
				component3.AddDisease(diseaseInfo.idx, Mathf.RoundToInt((float)diseaseInfo.count * num7), "ComplexFabricator.CompleteOrder");
				if (!recipeElement4.facadeID.IsNullOrWhiteSpace())
				{
					Equippable component5 = gameObject2.GetComponent<Equippable>();
					if (component5 != null)
					{
						EquippableFacade.AddFacadeToEquippable(component5, recipeElement4.facadeID);
					}
				}
				gameObject2.GetComponent<KMonoBehaviour>().Trigger(748399584, null);
				list.Add(gameObject2);
				if (this.storeProduced || recipeElement4.storeElement)
				{
					this.outStorage.Store(gameObject2, false, false, true, false);
				}
				break;
			}
			case ComplexRecipe.RecipeElement.TemperatureOperation.Melted:
				if (this.storeProduced || recipeElement4.storeElement)
				{
					float temperature = ElementLoader.GetElement(recipeElement4.material).defaultValues.temperature;
					this.outStorage.AddLiquid(ElementLoader.GetElementID(recipeElement4.material), recipeElement4.amount, temperature, 0, 0, false, true);
				}
				break;
			case ComplexRecipe.RecipeElement.TemperatureOperation.Dehydrated:
			{
				for (int j = 0; j < (int)recipeElement4.amount; j++)
				{
					GameObject gameObject3 = GameUtil.KInstantiate(Assets.GetPrefab(recipeElement4.material), Grid.SceneLayer.Ore, null, 0);
					int num8 = Grid.PosToCell(this);
					gameObject3.transform.SetPosition(Grid.CellToPosCCC(num8, Grid.SceneLayer.Ore) + this.outputOffset);
					float num9 = recipeElement2.amount / recipeElement4.amount;
					gameObject3.GetComponent<PrimaryElement>().Temperature = ((recipeElement4.temperatureOperation == ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature) ? num : this.heatedTemperature);
					DehydratedFoodPackage component6 = gameObject3.GetComponent<DehydratedFoodPackage>();
					if (component6 != null)
					{
						Storage component7 = component6.GetComponent<Storage>();
						this.outStorage.TransferMass(component7, recipeElement2.material, num9, true, false, false);
					}
					gameObject3.SetActive(true);
					gameObject3.GetComponent<KMonoBehaviour>().Trigger(748399584, null);
					list.Add(gameObject3);
					if (this.storeProduced || recipeElement4.storeElement)
					{
						this.outStorage.Store(gameObject3, false, false, true, false);
					}
				}
				break;
			}
			}
			if (list.Count > 0)
			{
				SymbolOverrideController component8 = base.GetComponent<SymbolOverrideController>();
				if (component8 != null)
				{
					KAnim.Build build = list[0].GetComponent<KBatchedAnimController>().AnimFiles[0].GetData().build;
					KAnim.Build.Symbol symbol = build.GetSymbol(build.name);
					if (symbol != null)
					{
						component8.TryRemoveSymbolOverride("output_tracker", 0);
						component8.AddSymbolOverride("output_tracker", symbol, 0);
					}
					else
					{
						global::Debug.LogWarning(component8.name + " is missing symbol " + build.name);
					}
				}
			}
		}
		if (recipe.producedHEP > 0)
		{
			base.GetComponent<HighEnergyParticleStorage>().Store((float)recipe.producedHEP);
		}
		return list;
	}

	// Token: 0x06002C84 RID: 11396 RVA: 0x00100F18 File Offset: 0x000FF118
	public virtual List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		ComplexRecipe[] recipes = this.GetRecipes();
		if (recipes.Length != 0)
		{
			Descriptor descriptor = default(Descriptor);
			descriptor.SetupDescriptor(UI.BUILDINGEFFECTS.PROCESSES, UI.BUILDINGEFFECTS.TOOLTIPS.PROCESSES, Descriptor.DescriptorType.Effect);
			list.Add(descriptor);
		}
		foreach (ComplexRecipe complexRecipe in recipes)
		{
			string text = "";
			string uiname = complexRecipe.GetUIName(false);
			foreach (ComplexRecipe.RecipeElement recipeElement in complexRecipe.ingredients)
			{
				text = text + "• " + string.Format(UI.BUILDINGEFFECTS.PROCESSEDITEM, recipeElement.material.ProperName(), recipeElement.amount) + "\n";
			}
			Descriptor descriptor2 = new Descriptor(uiname, string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.FABRICATOR_INGREDIENTS, text), Descriptor.DescriptorType.Effect, false);
			descriptor2.IncreaseIndent();
			list.Add(descriptor2);
		}
		return list;
	}

	// Token: 0x06002C85 RID: 11397 RVA: 0x00101010 File Offset: 0x000FF210
	public virtual List<Descriptor> AdditionalEffectsForRecipe(ComplexRecipe recipe)
	{
		return new List<Descriptor>();
	}

	// Token: 0x06002C86 RID: 11398 RVA: 0x00101018 File Offset: 0x000FF218
	public string GetConversationTopic()
	{
		if (this.HasWorkingOrder)
		{
			ComplexRecipe complexRecipe = this.recipe_list[this.workingOrderIdx];
			if (complexRecipe != null)
			{
				return complexRecipe.results[0].material.Name;
			}
		}
		return null;
	}

	// Token: 0x06002C87 RID: 11399 RVA: 0x00101054 File Offset: 0x000FF254
	public bool NeedsMoreHEPForQueuedRecipe()
	{
		if (this.hasOpenOrders)
		{
			HighEnergyParticleStorage component = base.GetComponent<HighEnergyParticleStorage>();
			foreach (KeyValuePair<string, int> keyValuePair in this.recipeQueueCounts)
			{
				if (keyValuePair.Value > 0)
				{
					foreach (ComplexRecipe complexRecipe in this.GetRecipes())
					{
						if (complexRecipe.id == keyValuePair.Key && (float)complexRecipe.consumedHEP > component.Particles)
						{
							return true;
						}
					}
				}
			}
			return false;
		}
		return false;
	}

	// Token: 0x04001A1B RID: 6683
	private const int MaxPrefetchCount = 2;

	// Token: 0x04001A1C RID: 6684
	public bool duplicantOperated = true;

	// Token: 0x04001A1D RID: 6685
	protected ComplexFabricatorWorkable workable;

	// Token: 0x04001A1E RID: 6686
	public string SideScreenSubtitleLabel = UI.UISIDESCREENS.FABRICATORSIDESCREEN.SUBTITLE;

	// Token: 0x04001A1F RID: 6687
	public string SideScreenRecipeScreenTitle = UI.UISIDESCREENS.FABRICATORSIDESCREEN.RECIPE_DETAILS;

	// Token: 0x04001A20 RID: 6688
	[SerializeField]
	public HashedString fetchChoreTypeIdHash = Db.Get().ChoreTypes.FabricateFetch.IdHash;

	// Token: 0x04001A21 RID: 6689
	[SerializeField]
	public float heatedTemperature;

	// Token: 0x04001A22 RID: 6690
	[SerializeField]
	public bool storeProduced;

	// Token: 0x04001A23 RID: 6691
	[SerializeField]
	public bool allowManualFluidDelivery = true;

	// Token: 0x04001A24 RID: 6692
	public ComplexFabricatorSideScreen.StyleSetting sideScreenStyle = ComplexFabricatorSideScreen.StyleSetting.ListQueueHybrid;

	// Token: 0x04001A25 RID: 6693
	public bool labelByResult = true;

	// Token: 0x04001A26 RID: 6694
	public Vector3 outputOffset = Vector3.zero;

	// Token: 0x04001A27 RID: 6695
	public ChoreType choreType;

	// Token: 0x04001A28 RID: 6696
	public bool keepExcessLiquids;

	// Token: 0x04001A29 RID: 6697
	public Tag keepAdditionalTag = Tag.Invalid;

	// Token: 0x04001A2A RID: 6698
	public StatusItem workingStatusItem = Db.Get().BuildingStatusItems.ComplexFabricatorProducing;

	// Token: 0x04001A2B RID: 6699
	public static int MAX_QUEUE_SIZE = 99;

	// Token: 0x04001A2C RID: 6700
	public static int QUEUE_INFINITE = -1;

	// Token: 0x04001A2D RID: 6701
	[Serialize]
	private Dictionary<string, int> recipeQueueCounts = new Dictionary<string, int>();

	// Token: 0x04001A2E RID: 6702
	[Serialize]
	public Dictionary<string, string> mostRecentRecipeSelectionByCategory = new Dictionary<string, string>();

	// Token: 0x04001A2F RID: 6703
	private int nextOrderIdx;

	// Token: 0x04001A30 RID: 6704
	private bool nextOrderIsWorkable;

	// Token: 0x04001A31 RID: 6705
	private int workingOrderIdx = -1;

	// Token: 0x04001A32 RID: 6706
	[Serialize]
	private string lastWorkingRecipe;

	// Token: 0x04001A33 RID: 6707
	[Serialize]
	private float orderProgress;

	// Token: 0x04001A34 RID: 6708
	private List<int> openOrderCounts = new List<int>();

	// Token: 0x04001A35 RID: 6709
	[Serialize]
	private bool forbidMutantSeeds;

	// Token: 0x04001A36 RID: 6710
	private Tag[] forbiddenMutantTags = new Tag[] { GameTags.MutatedSeed };

	// Token: 0x04001A37 RID: 6711
	private bool queueDirty = true;

	// Token: 0x04001A38 RID: 6712
	private bool hasOpenOrders;

	// Token: 0x04001A39 RID: 6713
	private List<FetchList2> fetchListList = new List<FetchList2>();

	// Token: 0x04001A3A RID: 6714
	private Chore chore;

	// Token: 0x04001A3B RID: 6715
	private bool cancelling;

	// Token: 0x04001A3C RID: 6716
	private ComplexRecipe[] recipe_list;

	// Token: 0x04001A3D RID: 6717
	private Dictionary<Tag, float> materialNeedCache = new Dictionary<Tag, float>();

	// Token: 0x04001A3E RID: 6718
	[SerializeField]
	public Storage inStorage;

	// Token: 0x04001A3F RID: 6719
	[SerializeField]
	public Storage buildStorage;

	// Token: 0x04001A40 RID: 6720
	[SerializeField]
	public Storage outStorage;

	// Token: 0x04001A41 RID: 6721
	[MyCmpAdd]
	private LoopingSounds loopingSounds;

	// Token: 0x04001A42 RID: 6722
	[MyCmpReq]
	protected Operational operational;

	// Token: 0x04001A43 RID: 6723
	[MyCmpAdd]
	protected ComplexFabricatorSM fabricatorSM;

	// Token: 0x04001A44 RID: 6724
	private ProgressBar progressBar;

	// Token: 0x04001A45 RID: 6725
	public bool showProgressBar;

	// Token: 0x04001A46 RID: 6726
	private static readonly EventSystem.IntraObjectHandler<ComplexFabricator> OnStorageChangeDelegate = new EventSystem.IntraObjectHandler<ComplexFabricator>(delegate(ComplexFabricator component, object data)
	{
		component.OnStorageChange(data);
	});

	// Token: 0x04001A47 RID: 6727
	private static readonly EventSystem.IntraObjectHandler<ComplexFabricator> OnParticleStorageChangedDelegate = new EventSystem.IntraObjectHandler<ComplexFabricator>(delegate(ComplexFabricator component, object data)
	{
		component.OnStorageChange(data);
	});

	// Token: 0x04001A48 RID: 6728
	private static readonly EventSystem.IntraObjectHandler<ComplexFabricator> OnDroppedAllDelegate = new EventSystem.IntraObjectHandler<ComplexFabricator>(delegate(ComplexFabricator component, object data)
	{
		component.OnDroppedAll(data);
	});

	// Token: 0x04001A49 RID: 6729
	private static readonly EventSystem.IntraObjectHandler<ComplexFabricator> OnOperationalChangedDelegate = new EventSystem.IntraObjectHandler<ComplexFabricator>(delegate(ComplexFabricator component, object data)
	{
		component.OnOperationalChanged(data);
	});

	// Token: 0x04001A4A RID: 6730
	private static readonly EventSystem.IntraObjectHandler<ComplexFabricator> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<ComplexFabricator>(delegate(ComplexFabricator component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x04001A4B RID: 6731
	private static readonly EventSystem.IntraObjectHandler<ComplexFabricator> OnRefreshUserMenuDelegate = new EventSystem.IntraObjectHandler<ComplexFabricator>(delegate(ComplexFabricator component, object data)
	{
		component.OnRefreshUserMenu(data);
	});
}
