using System;
using System.Collections.Generic;
using Klei.AI;
using TemplateClasses;
using UnityEngine;

// Token: 0x02000CB0 RID: 3248
public class DebugBaseTemplateButton : KScreen
{
	// Token: 0x1700074C RID: 1868
	// (get) Token: 0x060063DB RID: 25563 RVA: 0x00257859 File Offset: 0x00255A59
	// (set) Token: 0x060063DC RID: 25564 RVA: 0x00257860 File Offset: 0x00255A60
	public static DebugBaseTemplateButton Instance { get; private set; }

	// Token: 0x060063DD RID: 25565 RVA: 0x00257868 File Offset: 0x00255A68
	public static void DestroyInstance()
	{
		DebugBaseTemplateButton.Instance = null;
	}

	// Token: 0x060063DE RID: 25566 RVA: 0x00257870 File Offset: 0x00255A70
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		DebugBaseTemplateButton.Instance = this;
		base.gameObject.SetActive(false);
		this.SetupLocText();
		base.ConsumeMouseScroll = true;
		KInputTextField kinputTextField = this.nameField;
		kinputTextField.onFocus = (global::System.Action)Delegate.Combine(kinputTextField.onFocus, new global::System.Action(delegate
		{
			base.isEditing = true;
		}));
		this.nameField.onEndEdit.AddListener(delegate
		{
			base.isEditing = false;
		});
		this.nameField.onValueChanged.AddListener(delegate
		{
			Util.ScrubInputField(this.nameField, true, false);
		});
	}

	// Token: 0x060063DF RID: 25567 RVA: 0x00257901 File Offset: 0x00255B01
	protected override void OnActivate()
	{
		base.OnActivate();
		base.ConsumeMouseScroll = true;
	}

	// Token: 0x060063E0 RID: 25568 RVA: 0x00257910 File Offset: 0x00255B10
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.saveBaseButton != null)
		{
			this.saveBaseButton.onClick -= this.OnClickSaveBase;
			this.saveBaseButton.onClick += this.OnClickSaveBase;
		}
		if (this.clearButton != null)
		{
			this.clearButton.onClick -= this.OnClickClear;
			this.clearButton.onClick += this.OnClickClear;
		}
		if (this.AddSelectionButton != null)
		{
			this.AddSelectionButton.onClick -= this.OnClickAddSelection;
			this.AddSelectionButton.onClick += this.OnClickAddSelection;
		}
		if (this.RemoveSelectionButton != null)
		{
			this.RemoveSelectionButton.onClick -= this.OnClickRemoveSelection;
			this.RemoveSelectionButton.onClick += this.OnClickRemoveSelection;
		}
		if (this.clearSelectionButton != null)
		{
			this.clearSelectionButton.onClick -= this.OnClickClearSelection;
			this.clearSelectionButton.onClick += this.OnClickClearSelection;
		}
		if (this.MoveButton != null)
		{
			this.MoveButton.onClick -= this.OnClickMove;
			this.MoveButton.onClick += this.OnClickMove;
		}
		if (this.DestroyButton != null)
		{
			this.DestroyButton.onClick -= this.OnClickDestroySelection;
			this.DestroyButton.onClick += this.OnClickDestroySelection;
		}
		if (this.DeconstructButton != null)
		{
			this.DeconstructButton.onClick -= this.OnClickDeconstructSelection;
			this.DeconstructButton.onClick += this.OnClickDeconstructSelection;
		}
	}

	// Token: 0x060063E1 RID: 25569 RVA: 0x00257B03 File Offset: 0x00255D03
	private void SetupLocText()
	{
	}

	// Token: 0x060063E2 RID: 25570 RVA: 0x00257B05 File Offset: 0x00255D05
	private void OnClickDestroySelection()
	{
		DebugTool.Instance.Activate(DebugTool.Type.Destroy);
	}

	// Token: 0x060063E3 RID: 25571 RVA: 0x00257B12 File Offset: 0x00255D12
	private void OnClickDeconstructSelection()
	{
		DebugTool.Instance.Activate(DebugTool.Type.Deconstruct);
	}

	// Token: 0x060063E4 RID: 25572 RVA: 0x00257B1F File Offset: 0x00255D1F
	private void OnClickMove()
	{
		DebugTool.Instance.DeactivateTool(null);
		this.moveAsset = this.GetSelectionAsAsset();
		StampTool.Instance.Activate(this.moveAsset, false, false);
	}

	// Token: 0x060063E5 RID: 25573 RVA: 0x00257B4A File Offset: 0x00255D4A
	private void OnClickAddSelection()
	{
		DebugTool.Instance.Activate(DebugTool.Type.AddSelection);
	}

	// Token: 0x060063E6 RID: 25574 RVA: 0x00257B57 File Offset: 0x00255D57
	private void OnClickRemoveSelection()
	{
		DebugTool.Instance.Activate(DebugTool.Type.RemoveSelection);
	}

	// Token: 0x060063E7 RID: 25575 RVA: 0x00257B64 File Offset: 0x00255D64
	private void OnClickClearSelection()
	{
		this.ClearSelection();
		this.nameField.text = "";
	}

	// Token: 0x060063E8 RID: 25576 RVA: 0x00257B7C File Offset: 0x00255D7C
	private void OnClickClear()
	{
		DebugTool.Instance.Activate(DebugTool.Type.Clear);
	}

	// Token: 0x060063E9 RID: 25577 RVA: 0x00257B89 File Offset: 0x00255D89
	protected override void OnDeactivate()
	{
		if (DebugTool.Instance != null)
		{
			DebugTool.Instance.DeactivateTool(null);
		}
		base.OnDeactivate();
	}

	// Token: 0x060063EA RID: 25578 RVA: 0x00257BA9 File Offset: 0x00255DA9
	protected override void OnDisable()
	{
		if (DebugTool.Instance != null)
		{
			DebugTool.Instance.DeactivateTool(null);
		}
	}

	// Token: 0x060063EB RID: 25579 RVA: 0x00257BC4 File Offset: 0x00255DC4
	private TemplateContainer GetSelectionAsAsset()
	{
		List<Cell> list = new List<Cell>();
		List<Prefab> list2 = new List<Prefab>();
		List<Prefab> list3 = new List<Prefab>();
		List<Prefab> list4 = new List<Prefab>();
		List<Prefab> list5 = new List<Prefab>();
		HashSet<GameObject> hashSet = new HashSet<GameObject>();
		float num = 0f;
		float num2 = 0f;
		foreach (int num3 in this.SelectedCells)
		{
			num += (float)Grid.CellToXY(num3).x;
			num2 += (float)Grid.CellToXY(num3).y;
		}
		float num4 = num / (float)this.SelectedCells.Count;
		float num5;
		num2 = (num5 = num2 / (float)this.SelectedCells.Count);
		int rootX;
		int rootY;
		Grid.CellToXY(Grid.PosToCell(new Vector3(num4, num5, 0f)), out rootX, out rootY);
		for (int i = 0; i < this.SelectedCells.Count; i++)
		{
			int num6 = this.SelectedCells[i];
			int num7;
			int num8;
			Grid.CellToXY(this.SelectedCells[i], out num7, out num8);
			Element element = ElementLoader.elements[(int)Grid.ElementIdx[num6]];
			string text = ((Grid.DiseaseIdx[num6] != byte.MaxValue) ? Db.Get().Diseases[(int)Grid.DiseaseIdx[num6]].Id : null);
			int num9 = Grid.DiseaseCount[num6];
			if (num9 <= 0)
			{
				num9 = 0;
				text = null;
			}
			list.Add(new Cell(num7 - rootX, num8 - rootY, element.id, Grid.Temperature[num6], Grid.Mass[num6], text, num9, Grid.PreventFogOfWarReveal[this.SelectedCells[i]]));
		}
		for (int j = 0; j < Components.BuildingCompletes.Count; j++)
		{
			BuildingComplete buildingComplete = Components.BuildingCompletes[j];
			if (!hashSet.Contains(buildingComplete.gameObject))
			{
				int num10 = Grid.PosToCell(buildingComplete);
				int num11;
				int num12;
				Grid.CellToXY(num10, out num11, out num12);
				if (this.SaveAllBuildings || this.SelectedCells.Contains(num10))
				{
					int[] placementCells = buildingComplete.PlacementCells;
					string text2;
					for (int k = 0; k < placementCells.Length; k++)
					{
						int num13 = placementCells[k];
						int xplace;
						int yplace;
						Grid.CellToXY(num13, out xplace, out yplace);
						text2 = ((Grid.DiseaseIdx[num13] != byte.MaxValue) ? Db.Get().Diseases[(int)Grid.DiseaseIdx[num13]].Id : null);
						if (list.Find((Cell c) => c.location_x == xplace - rootX && c.location_y == yplace - rootY) == null)
						{
							list.Add(new Cell(xplace - rootX, yplace - rootY, Grid.Element[num13].id, Grid.Temperature[num13], Grid.Mass[num13], text2, Grid.DiseaseCount[num13], false));
						}
					}
					Orientation orientation = Orientation.Neutral;
					Rotatable component = buildingComplete.gameObject.GetComponent<Rotatable>();
					if (component != null)
					{
						orientation = component.GetOrientation();
					}
					SimHashes simHashes = SimHashes.Void;
					float num14 = 280f;
					text2 = null;
					int num15 = 0;
					PrimaryElement component2 = buildingComplete.GetComponent<PrimaryElement>();
					if (component2 != null)
					{
						simHashes = component2.ElementID;
						num14 = component2.Temperature;
						text2 = ((component2.DiseaseIdx != byte.MaxValue) ? Db.Get().Diseases[(int)component2.DiseaseIdx].Id : null);
						num15 = component2.DiseaseCount;
					}
					List<Prefab.template_amount_value> list6 = new List<Prefab.template_amount_value>();
					List<Prefab.template_amount_value> list7 = new List<Prefab.template_amount_value>();
					foreach (AmountInstance amountInstance in buildingComplete.gameObject.GetAmounts())
					{
						list6.Add(new Prefab.template_amount_value(amountInstance.amount.Id, amountInstance.value));
					}
					Battery component3 = buildingComplete.GetComponent<Battery>();
					if (component3 != null)
					{
						float joulesAvailable = component3.JoulesAvailable;
						list7.Add(new Prefab.template_amount_value("joulesAvailable", joulesAvailable));
					}
					Unsealable component4 = buildingComplete.GetComponent<Unsealable>();
					if (component4 != null)
					{
						float num16 = (float)(component4.facingRight ? 1 : 0);
						list7.Add(new Prefab.template_amount_value("sealedDoorDirection", num16));
					}
					LogicSwitch component5 = buildingComplete.GetComponent<LogicSwitch>();
					if (component5 != null)
					{
						float num17 = (float)(component5.IsSwitchedOn ? 1 : 0);
						list7.Add(new Prefab.template_amount_value("switchSetting", num17));
					}
					int num18 = 0;
					IHaveUtilityNetworkMgr component6 = buildingComplete.GetComponent<IHaveUtilityNetworkMgr>();
					if (component6 != null)
					{
						num18 = (int)component6.GetNetworkManager().GetConnections(num10, true);
					}
					string text3 = null;
					BuildingFacade component7 = buildingComplete.GetComponent<BuildingFacade>();
					if (component7 != null)
					{
						text3 = component7.CurrentFacade;
					}
					num11 -= rootX;
					num12 -= rootY;
					num14 = Mathf.Clamp(num14, 1f, 99999f);
					Prefab prefab = new Prefab(buildingComplete.PrefabID().Name, Prefab.Type.Building, num11, num12, simHashes, num14, 0f, text2, num15, orientation, list6.ToArray(), list7.ToArray(), num18, text3);
					Storage component8 = buildingComplete.gameObject.GetComponent<Storage>();
					if (component8 != null)
					{
						foreach (GameObject gameObject in component8.items)
						{
							float num19 = 0f;
							SimHashes simHashes2 = SimHashes.Vacuum;
							float num20 = 280f;
							string text4 = null;
							int num21 = 0;
							bool flag = false;
							PrimaryElement component9 = gameObject.GetComponent<PrimaryElement>();
							if (component9 != null)
							{
								num19 = component9.Units;
								simHashes2 = component9.ElementID;
								num20 = component9.Temperature;
								text4 = ((component9.DiseaseIdx != byte.MaxValue) ? Db.Get().Diseases[(int)component9.DiseaseIdx].Id : null);
								num21 = component9.DiseaseCount;
							}
							global::Rottable.Instance smi = gameObject.gameObject.GetSMI<global::Rottable.Instance>();
							if (gameObject.GetComponent<ElementChunk>() != null)
							{
								flag = true;
							}
							StorageItem storageItem = new StorageItem(gameObject.PrefabID().Name, num19, num20, simHashes2, text4, num21, flag);
							if (smi != null)
							{
								storageItem.rottable.rotAmount = smi.RotValue;
							}
							prefab.AssignStorage(storageItem);
							hashSet.Add(gameObject);
						}
					}
					list2.Add(prefab);
					hashSet.Add(buildingComplete.gameObject);
				}
			}
		}
		for (int l = 0; l < Components.Pickupables.Count; l++)
		{
			if (Components.Pickupables[l].gameObject.activeSelf)
			{
				Pickupable pickupable = Components.Pickupables[l];
				if (!hashSet.Contains(pickupable.gameObject))
				{
					int num22 = Grid.PosToCell(pickupable);
					if ((this.SaveAllPickups || this.SelectedCells.Contains(num22)) && !Components.Pickupables[l].gameObject.GetComponent<MinionBrain>())
					{
						int num23;
						int num24;
						Grid.CellToXY(num22, out num23, out num24);
						num23 -= rootX;
						num24 -= rootY;
						SimHashes simHashes3 = SimHashes.Void;
						float num25 = 280f;
						float num26 = 1f;
						string text5 = null;
						int num27 = 0;
						float num28 = 0f;
						global::Rottable.Instance smi2 = pickupable.gameObject.GetSMI<global::Rottable.Instance>();
						if (smi2 != null)
						{
							num28 = smi2.RotValue;
						}
						PrimaryElement component10 = pickupable.gameObject.GetComponent<PrimaryElement>();
						if (component10 != null)
						{
							simHashes3 = component10.ElementID;
							num26 = component10.Units;
							num25 = component10.Temperature;
							text5 = ((component10.DiseaseIdx != byte.MaxValue) ? Db.Get().Diseases[(int)component10.DiseaseIdx].Id : null);
							num27 = component10.DiseaseCount;
						}
						if (pickupable.gameObject.GetComponent<ElementChunk>() != null)
						{
							Prefab prefab2 = new Prefab(pickupable.PrefabID().Name, Prefab.Type.Ore, num23, num24, simHashes3, num25, num26, text5, num27, Orientation.Neutral, null, null, 0, null);
							list4.Add(prefab2);
						}
						else
						{
							list3.Add(new Prefab(pickupable.PrefabID().Name, Prefab.Type.Pickupable, num23, num24, simHashes3, num25, num26, text5, num27, Orientation.Neutral, null, null, 0, null)
							{
								rottable = new global::TemplateClasses.Rottable(),
								rottable = 
								{
									rotAmount = num28
								}
							});
						}
						hashSet.Add(pickupable.gameObject);
					}
				}
			}
		}
		this.GetEntities<Crop>(Components.Crops.Items, rootX, rootY, ref list4, ref list5, ref hashSet);
		this.GetEntities<Health>(Components.Health.Items, rootX, rootY, ref list4, ref list5, ref hashSet);
		this.GetEntities<Harvestable>(Components.Harvestables.Items, rootX, rootY, ref list4, ref list5, ref hashSet);
		this.GetEntities<Edible>(Components.Edibles.Items, rootX, rootY, ref list4, ref list5, ref hashSet);
		this.GetEntities<Geyser>(rootX, rootY, ref list4, ref list5, ref hashSet);
		this.GetEntities<OccupyArea>(rootX, rootY, ref list4, ref list5, ref hashSet);
		this.GetEntities<FogOfWarMask>(rootX, rootY, ref list4, ref list5, ref hashSet);
		list5.RemoveAll((Prefab x) => Assets.GetPrefab(x.id).HasTag(GameTags.ExcludeFromTemplate));
		TemplateContainer templateContainer = new TemplateContainer();
		templateContainer.Init(list, list2, list3, list4, list5);
		return templateContainer;
	}

	// Token: 0x060063EC RID: 25580 RVA: 0x00258624 File Offset: 0x00256824
	private void GetEntities<T>(int rootX, int rootY, ref List<Prefab> _primaryElementOres, ref List<Prefab> _otherEntities, ref HashSet<GameObject> _excludeEntities)
	{
		object[] array = global::UnityEngine.Object.FindObjectsOfType(typeof(T));
		object[] array2 = array;
		this.GetEntities<object>(array2, rootX, rootY, ref _primaryElementOres, ref _otherEntities, ref _excludeEntities);
	}

	// Token: 0x060063ED RID: 25581 RVA: 0x00258654 File Offset: 0x00256854
	private void GetEntities<T>(IEnumerable<T> component_collection, int rootX, int rootY, ref List<Prefab> _primaryElementOres, ref List<Prefab> _otherEntities, ref HashSet<GameObject> _excludeEntities)
	{
		foreach (T t in component_collection)
		{
			if (!_excludeEntities.Contains((t as KMonoBehaviour).gameObject) && (t as KMonoBehaviour).gameObject.activeSelf)
			{
				int num = Grid.PosToCell(t as KMonoBehaviour);
				if (this.SelectedCells.Contains(num) && !(t as KMonoBehaviour).gameObject.GetComponent<MinionBrain>())
				{
					Orientation orientation = Orientation.Neutral;
					Rotatable component = (t as KMonoBehaviour).GetComponent<Rotatable>();
					if (component != null)
					{
						orientation = component.Orientation;
					}
					int num2;
					int num3;
					Grid.CellToXY(num, out num2, out num3);
					num2 -= rootX;
					num3 -= rootY;
					SimHashes simHashes = SimHashes.Void;
					float num4 = 280f;
					float num5 = 1f;
					string text = null;
					int num6 = 0;
					PrimaryElement component2 = (t as KMonoBehaviour).gameObject.GetComponent<PrimaryElement>();
					if (component2 != null)
					{
						simHashes = component2.ElementID;
						num5 = component2.Units;
						num4 = component2.Temperature;
						text = ((component2.DiseaseIdx != byte.MaxValue) ? Db.Get().Diseases[(int)component2.DiseaseIdx].Id : null);
						num6 = component2.DiseaseCount;
					}
					List<Prefab.template_amount_value> list = new List<Prefab.template_amount_value>();
					if ((t as KMonoBehaviour).gameObject.GetAmounts() != null)
					{
						foreach (AmountInstance amountInstance in (t as KMonoBehaviour).gameObject.GetAmounts())
						{
							list.Add(new Prefab.template_amount_value(amountInstance.amount.Id, amountInstance.value));
						}
					}
					if ((t as KMonoBehaviour).gameObject.GetComponent<ElementChunk>() != null)
					{
						string name = (t as KMonoBehaviour).PrefabID().Name;
						Prefab.Type type = Prefab.Type.Ore;
						int num7 = num2;
						int num8 = num3;
						SimHashes simHashes2 = simHashes;
						float num9 = num4;
						float num10 = num5;
						string text2 = text;
						int num11 = num6;
						Prefab.template_amount_value[] array = list.ToArray();
						Prefab prefab = new Prefab(name, type, num7, num8, simHashes2, num9, num10, text2, num11, orientation, array, null, 0, null);
						_primaryElementOres.Add(prefab);
						_excludeEntities.Add((t as KMonoBehaviour).gameObject);
					}
					else
					{
						string name2 = (t as KMonoBehaviour).PrefabID().Name;
						Prefab.Type type2 = Prefab.Type.Other;
						int num12 = num2;
						int num13 = num3;
						SimHashes simHashes3 = simHashes;
						float num14 = num4;
						float num15 = num5;
						string text3 = text;
						int num16 = num6;
						Prefab.template_amount_value[] array = list.ToArray();
						Prefab prefab = new Prefab(name2, type2, num12, num13, simHashes3, num14, num15, text3, num16, orientation, array, null, 0, null);
						_otherEntities.Add(prefab);
						_excludeEntities.Add((t as KMonoBehaviour).gameObject);
					}
				}
			}
		}
	}

	// Token: 0x060063EE RID: 25582 RVA: 0x0025895C File Offset: 0x00256B5C
	private void OnClickSaveBase()
	{
		TemplateContainer selectionAsAsset = this.GetSelectionAsAsset();
		if (this.SelectedCells.Count <= 0)
		{
			global::Debug.LogWarning("No cells selected. Use buttons above to select the area you want to save.");
			return;
		}
		this.SaveName = this.nameField.text;
		if (this.SaveName == null || this.SaveName == "")
		{
			global::Debug.LogWarning("Invalid save name. Please enter a name in the input field.");
			return;
		}
		selectionAsAsset.SaveToYaml(this.SaveName);
		TemplateCache.Clear();
		TemplateCache.Init();
		PasteBaseTemplateScreen.Instance.RefreshStampButtons();
	}

	// Token: 0x060063EF RID: 25583 RVA: 0x002589E0 File Offset: 0x00256BE0
	public void ClearSelection()
	{
		for (int i = this.SelectedCells.Count - 1; i >= 0; i--)
		{
			this.RemoveFromSelection(this.SelectedCells[i]);
		}
	}

	// Token: 0x060063F0 RID: 25584 RVA: 0x00258A17 File Offset: 0x00256C17
	public void DestroySelection()
	{
	}

	// Token: 0x060063F1 RID: 25585 RVA: 0x00258A19 File Offset: 0x00256C19
	public void DeconstructSelection()
	{
	}

	// Token: 0x060063F2 RID: 25586 RVA: 0x00258A1C File Offset: 0x00256C1C
	public void AddToSelection(int cell)
	{
		if (!this.SelectedCells.Contains(cell))
		{
			GameObject gameObject = Util.KInstantiate(this.Placer, null, null);
			Grid.Objects[cell, 7] = gameObject;
			Vector3 vector = Grid.CellToPosCBC(cell, this.visualizerLayer);
			float num = -0.15f;
			vector.z += num;
			gameObject.transform.SetPosition(vector);
			this.SelectedCells.Add(cell);
		}
	}

	// Token: 0x060063F3 RID: 25587 RVA: 0x00258A90 File Offset: 0x00256C90
	public void RemoveFromSelection(int cell)
	{
		if (this.SelectedCells.Contains(cell))
		{
			GameObject gameObject = Grid.Objects[cell, 7];
			if (gameObject != null)
			{
				gameObject.DeleteObject();
			}
			this.SelectedCells.Remove(cell);
		}
	}

	// Token: 0x040043FB RID: 17403
	private bool SaveAllBuildings;

	// Token: 0x040043FC RID: 17404
	private bool SaveAllPickups;

	// Token: 0x040043FD RID: 17405
	public KButton saveBaseButton;

	// Token: 0x040043FE RID: 17406
	public KButton clearButton;

	// Token: 0x040043FF RID: 17407
	private TemplateContainer pasteAndSelectAsset;

	// Token: 0x04004400 RID: 17408
	public KButton AddSelectionButton;

	// Token: 0x04004401 RID: 17409
	public KButton RemoveSelectionButton;

	// Token: 0x04004402 RID: 17410
	public KButton clearSelectionButton;

	// Token: 0x04004403 RID: 17411
	public KButton DestroyButton;

	// Token: 0x04004404 RID: 17412
	public KButton DeconstructButton;

	// Token: 0x04004405 RID: 17413
	public KButton MoveButton;

	// Token: 0x04004406 RID: 17414
	public TemplateContainer moveAsset;

	// Token: 0x04004407 RID: 17415
	public KInputTextField nameField;

	// Token: 0x04004408 RID: 17416
	private string SaveName = "enter_template_name";

	// Token: 0x04004409 RID: 17417
	public GameObject Placer;

	// Token: 0x0400440A RID: 17418
	public Grid.SceneLayer visualizerLayer = Grid.SceneLayer.Move;

	// Token: 0x0400440B RID: 17419
	public List<int> SelectedCells = new List<int>();
}
