using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x02000843 RID: 2115
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/ConsumerManager")]
public class ConsumerManager : KMonoBehaviour, ISaveLoadable
{
	// Token: 0x06003A14 RID: 14868 RVA: 0x00142C86 File Offset: 0x00140E86
	public static void DestroyInstance()
	{
		ConsumerManager.instance = null;
	}

	// Token: 0x14000015 RID: 21
	// (add) Token: 0x06003A15 RID: 14869 RVA: 0x00142C90 File Offset: 0x00140E90
	// (remove) Token: 0x06003A16 RID: 14870 RVA: 0x00142CC8 File Offset: 0x00140EC8
	public event Action<Tag> OnDiscover;

	// Token: 0x170003FE RID: 1022
	// (get) Token: 0x06003A17 RID: 14871 RVA: 0x00142CFD File Offset: 0x00140EFD
	public List<Tag> DefaultForbiddenTagsList
	{
		get
		{
			return this.defaultForbiddenTagsList;
		}
	}

	// Token: 0x170003FF RID: 1023
	// (get) Token: 0x06003A18 RID: 14872 RVA: 0x00142D08 File Offset: 0x00140F08
	public List<Tag> StandardDuplicantDietaryRestrictions
	{
		get
		{
			List<Tag> list = new List<Tag>();
			foreach (GameObject gameObject in Assets.GetPrefabsWithTag(GameTags.ChargedPortableBattery))
			{
				list.Add(gameObject.PrefabID());
			}
			list.Add(ConsumerManager.OXYGEN_TANK_ID);
			return list;
		}
	}

	// Token: 0x17000400 RID: 1024
	// (get) Token: 0x06003A19 RID: 14873 RVA: 0x00142D7C File Offset: 0x00140F7C
	public List<Tag> BionicDuplicantDietaryRestrictions
	{
		get
		{
			List<Tag> list = new List<Tag>();
			foreach (GameObject gameObject in Assets.GetPrefabsWithTag(GameTags.Edible))
			{
				list.Add(gameObject.PrefabID());
			}
			Tag[] array = new Tag[GameTags.BionicIncompatibleBatteries.Count];
			GameTags.BionicIncompatibleBatteries.CopyTo(array, 0);
			foreach (Tag tag in array)
			{
				list.Add(tag);
			}
			return list;
		}
	}

	// Token: 0x06003A1A RID: 14874 RVA: 0x00142E24 File Offset: 0x00141024
	protected override void OnSpawn()
	{
		base.OnSpawn();
		ConsumerManager.instance = this;
		this.RefreshDiscovered(null);
		DiscoveredResources.Instance.OnDiscover += this.OnWorldInventoryDiscover;
		Game.Instance.Subscribe(-107300940, new Action<object>(this.RefreshDiscovered));
	}

	// Token: 0x06003A1B RID: 14875 RVA: 0x00142E76 File Offset: 0x00141076
	public bool isDiscovered(Tag id)
	{
		return !this.undiscoveredConsumableTags.Contains(id);
	}

	// Token: 0x06003A1C RID: 14876 RVA: 0x00142E87 File Offset: 0x00141087
	private void OnWorldInventoryDiscover(Tag category_tag, Tag tag)
	{
		if (this.undiscoveredConsumableTags.Contains(tag))
		{
			this.RefreshDiscovered(null);
		}
	}

	// Token: 0x06003A1D RID: 14877 RVA: 0x00142EA0 File Offset: 0x001410A0
	public void RefreshDiscovered(object data = null)
	{
		foreach (EdiblesManager.FoodInfo foodInfo in EdiblesManager.GetAllFoodTypes())
		{
			if (!this.ShouldBeDiscovered(foodInfo.Id.ToTag()) && !this.undiscoveredConsumableTags.Contains(foodInfo.Id.ToTag()))
			{
				this.undiscoveredConsumableTags.Add(foodInfo.Id.ToTag());
				if (this.OnDiscover != null)
				{
					this.OnDiscover("UndiscoveredSomething".ToTag());
				}
			}
			else if (this.undiscoveredConsumableTags.Contains(foodInfo.Id.ToTag()) && this.ShouldBeDiscovered(foodInfo.Id.ToTag()))
			{
				this.undiscoveredConsumableTags.Remove(foodInfo.Id.ToTag());
				if (this.OnDiscover != null)
				{
					this.OnDiscover(foodInfo.Id.ToTag());
				}
				if (!DiscoveredResources.Instance.IsDiscovered(foodInfo.Id.ToTag()))
				{
					if (foodInfo.CaloriesPerUnit == 0f)
					{
						DiscoveredResources.Instance.Discover(foodInfo.Id.ToTag(), GameTags.CookingIngredient);
					}
					else
					{
						DiscoveredResources.Instance.Discover(foodInfo.Id.ToTag(), GameTags.Edible);
					}
				}
			}
		}
	}

	// Token: 0x06003A1E RID: 14878 RVA: 0x00143024 File Offset: 0x00141224
	private bool ShouldBeDiscovered(Tag food_id)
	{
		if (DiscoveredResources.Instance.IsDiscovered(food_id))
		{
			return true;
		}
		foreach (Recipe recipe in RecipeManager.Get().recipes)
		{
			if (recipe.Result == food_id)
			{
				foreach (string text in recipe.fabricators)
				{
					if (Db.Get().TechItems.IsTechItemComplete(text))
					{
						return true;
					}
				}
			}
		}
		foreach (Crop crop in Components.Crops.Items)
		{
			if (Grid.IsVisible(Grid.PosToCell(crop.gameObject)) && crop.cropId == food_id.Name)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x040023B2 RID: 9138
	public static ConsumerManager instance;

	// Token: 0x040023B4 RID: 9140
	[Serialize]
	private List<Tag> undiscoveredConsumableTags = new List<Tag>();

	// Token: 0x040023B5 RID: 9141
	[Serialize]
	private List<Tag> defaultForbiddenTagsList = new List<Tag>();

	// Token: 0x040023B6 RID: 9142
	public static string OXYGEN_TANK_ID = ClosestOxygenCanisterSensor.GenericBreathableGassesTankTag.ToString();
}
