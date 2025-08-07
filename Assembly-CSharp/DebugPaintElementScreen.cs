using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000CB2 RID: 3250
public class DebugPaintElementScreen : KScreen
{
	// Token: 0x1700074D RID: 1869
	// (get) Token: 0x060063FC RID: 25596 RVA: 0x00258B59 File Offset: 0x00256D59
	// (set) Token: 0x060063FD RID: 25597 RVA: 0x00258B60 File Offset: 0x00256D60
	public static DebugPaintElementScreen Instance { get; private set; }

	// Token: 0x060063FE RID: 25598 RVA: 0x00258B68 File Offset: 0x00256D68
	public static void DestroyInstance()
	{
		DebugPaintElementScreen.Instance = null;
	}

	// Token: 0x060063FF RID: 25599 RVA: 0x00258B70 File Offset: 0x00256D70
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		DebugPaintElementScreen.Instance = this;
		this.SetupLocText();
		this.inputFields.Add(this.massInput);
		this.inputFields.Add(this.temperatureInput);
		this.inputFields.Add(this.diseaseCountInput);
		this.inputFields.Add(this.filterInput);
		foreach (KInputTextField kinputTextField in this.inputFields)
		{
			kinputTextField.onFocus = (global::System.Action)Delegate.Combine(kinputTextField.onFocus, new global::System.Action(delegate
			{
				base.isEditing = true;
			}));
			kinputTextField.onEndEdit.AddListener(delegate(string value)
			{
				base.isEditing = false;
			});
		}
		this.temperatureInput.onEndEdit.AddListener(delegate(string value)
		{
			this.OnChangeTemperature();
		});
		this.massInput.onEndEdit.AddListener(delegate(string value)
		{
			this.OnChangeMass();
		});
		this.diseaseCountInput.onEndEdit.AddListener(delegate(string value)
		{
			this.OnDiseaseCountChange();
		});
		base.gameObject.SetActive(false);
		this.activateOnSpawn = true;
		base.ConsumeMouseScroll = true;
	}

	// Token: 0x06006400 RID: 25600 RVA: 0x00258CB8 File Offset: 0x00256EB8
	private void SetupLocText()
	{
		HierarchyReferences component = base.GetComponent<HierarchyReferences>();
		component.GetReference<LocText>("Title").text = UI.DEBUG_TOOLS.PAINT_ELEMENTS_SCREEN.TITLE;
		component.GetReference<LocText>("ElementLabel").text = UI.DEBUG_TOOLS.PAINT_ELEMENTS_SCREEN.ELEMENT;
		component.GetReference<LocText>("MassLabel").text = UI.DEBUG_TOOLS.PAINT_ELEMENTS_SCREEN.MASS_KG;
		component.GetReference<LocText>("TemperatureLabel").text = UI.DEBUG_TOOLS.PAINT_ELEMENTS_SCREEN.TEMPERATURE_KELVIN;
		component.GetReference<LocText>("DiseaseLabel").text = UI.DEBUG_TOOLS.PAINT_ELEMENTS_SCREEN.DISEASE;
		component.GetReference<LocText>("DiseaseCountLabel").text = UI.DEBUG_TOOLS.PAINT_ELEMENTS_SCREEN.DISEASE_COUNT;
		component.GetReference<LocText>("AddFoWMaskLabel").text = UI.DEBUG_TOOLS.PAINT_ELEMENTS_SCREEN.ADD_FOW_MASK;
		component.GetReference<LocText>("RemoveFoWMaskLabel").text = UI.DEBUG_TOOLS.PAINT_ELEMENTS_SCREEN.REMOVE_FOW_MASK;
		this.elementButton.GetComponentsInChildren<LocText>()[0].text = UI.DEBUG_TOOLS.PAINT_ELEMENTS_SCREEN.ELEMENT;
		this.diseaseButton.GetComponentsInChildren<LocText>()[0].text = UI.DEBUG_TOOLS.PAINT_ELEMENTS_SCREEN.DISEASE;
		this.paintButton.GetComponentsInChildren<LocText>()[0].text = UI.DEBUG_TOOLS.PAINT_ELEMENTS_SCREEN.PAINT;
		this.fillButton.GetComponentsInChildren<LocText>()[0].text = UI.DEBUG_TOOLS.PAINT_ELEMENTS_SCREEN.FILL;
		this.spawnButton.GetComponentsInChildren<LocText>()[0].text = UI.DEBUG_TOOLS.PAINT_ELEMENTS_SCREEN.SPAWN_ALL;
		this.sampleButton.GetComponentsInChildren<LocText>()[0].text = UI.DEBUG_TOOLS.PAINT_ELEMENTS_SCREEN.SAMPLE;
		this.storeButton.GetComponentsInChildren<LocText>()[0].text = UI.DEBUG_TOOLS.PAINT_ELEMENTS_SCREEN.STORE;
		this.affectBuildings.transform.parent.GetComponentsInChildren<LocText>()[0].text = UI.DEBUG_TOOLS.PAINT_ELEMENTS_SCREEN.BUILDINGS;
		this.affectCells.transform.parent.GetComponentsInChildren<LocText>()[0].text = UI.DEBUG_TOOLS.PAINT_ELEMENTS_SCREEN.CELLS;
	}

	// Token: 0x06006401 RID: 25601 RVA: 0x00258EAC File Offset: 0x002570AC
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.element = SimHashes.Ice;
		this.diseaseIdx = byte.MaxValue;
		this.ConfigureElements();
		List<string> list = new List<string>();
		list.Insert(0, "None");
		foreach (Disease disease in Db.Get().Diseases.resources)
		{
			list.Add(disease.Name);
		}
		this.diseasePopup.SetOptions(list.ToArray());
		KPopupMenu kpopupMenu = this.diseasePopup;
		kpopupMenu.OnSelect = (Action<string, int>)Delegate.Combine(kpopupMenu.OnSelect, new Action<string, int>(this.OnSelectDisease));
		this.SelectDiseaseOption((int)this.diseaseIdx);
		this.paintButton.onClick += this.OnClickPaint;
		this.fillButton.onClick += this.OnClickFill;
		this.sampleButton.onClick += this.OnClickSample;
		this.storeButton.onClick += this.OnClickStore;
		if (SaveGame.Instance.worldGenSpawner.SpawnsRemain())
		{
			this.spawnButton.onClick += this.OnClickSpawn;
		}
		KPopupMenu kpopupMenu2 = this.elementPopup;
		kpopupMenu2.OnSelect = (Action<string, int>)Delegate.Combine(kpopupMenu2.OnSelect, new Action<string, int>(this.OnSelectElement));
		this.elementButton.onClick += this.elementPopup.OnClick;
		this.diseaseButton.onClick += this.diseasePopup.OnClick;
	}

	// Token: 0x06006402 RID: 25602 RVA: 0x00259068 File Offset: 0x00257268
	private void FilterElements(string filterValue)
	{
		if (string.IsNullOrEmpty(filterValue))
		{
			foreach (KButtonMenu.ButtonInfo buttonInfo in this.elementPopup.GetButtons())
			{
				buttonInfo.uibutton.gameObject.SetActive(true);
			}
			return;
		}
		filterValue = this.filter.ToLower();
		foreach (KButtonMenu.ButtonInfo buttonInfo2 in this.elementPopup.GetButtons())
		{
			buttonInfo2.uibutton.gameObject.SetActive(buttonInfo2.text.ToLower().Contains(filterValue));
		}
	}

	// Token: 0x06006403 RID: 25603 RVA: 0x00259134 File Offset: 0x00257334
	private void ConfigureElements()
	{
		if (this.filter != null)
		{
			this.filter = this.filter.ToLower();
		}
		List<DebugPaintElementScreen.ElemDisplayInfo> list = new List<DebugPaintElementScreen.ElemDisplayInfo>();
		foreach (Element element in ElementLoader.elements)
		{
			if (element.name != "Element Not Loaded" && element.substance != null && element.substance.showInEditor && (string.IsNullOrEmpty(this.filter) || element.name.ToLower().Contains(this.filter)))
			{
				list.Add(new DebugPaintElementScreen.ElemDisplayInfo
				{
					id = element.id,
					displayStr = element.name + " (" + element.GetStateString() + ")"
				});
			}
		}
		list.Sort((DebugPaintElementScreen.ElemDisplayInfo a, DebugPaintElementScreen.ElemDisplayInfo b) => a.displayStr.CompareTo(b.displayStr));
		if (string.IsNullOrEmpty(this.filter))
		{
			SimHashes[] array = new SimHashes[]
			{
				SimHashes.SlimeMold,
				SimHashes.Vacuum,
				SimHashes.Dirt,
				SimHashes.CarbonDioxide,
				SimHashes.Water,
				SimHashes.Oxygen
			};
			for (int i = 0; i < array.Length; i++)
			{
				Element element2 = ElementLoader.FindElementByHash(array[i]);
				list.Insert(0, new DebugPaintElementScreen.ElemDisplayInfo
				{
					id = element2.id,
					displayStr = element2.name + " (" + element2.GetStateString() + ")"
				});
			}
		}
		this.options_list = new List<string>();
		List<string> list2 = new List<string>();
		foreach (DebugPaintElementScreen.ElemDisplayInfo elemDisplayInfo in list)
		{
			list2.Add(elemDisplayInfo.displayStr);
			this.options_list.Add(elemDisplayInfo.id.ToString());
		}
		this.elementPopup.SetOptions(list2);
		for (int j = 0; j < list.Count; j++)
		{
			if (list[j].id == this.element)
			{
				this.elementPopup.SelectOption(list2[j], j);
			}
		}
		this.elementPopup.GetComponent<ScrollRect>().normalizedPosition = new Vector2(0f, 1f);
	}

	// Token: 0x06006404 RID: 25604 RVA: 0x002593B4 File Offset: 0x002575B4
	private void OnClickSpawn()
	{
		foreach (WorldContainer worldContainer in ClusterManager.Instance.WorldContainers)
		{
			worldContainer.SetDiscovered(true);
		}
		SaveGame.Instance.worldGenSpawner.SpawnEverything();
		this.spawnButton.GetComponent<KButton>().isInteractable = false;
	}

	// Token: 0x06006405 RID: 25605 RVA: 0x0025942C File Offset: 0x0025762C
	private void OnClickPaint()
	{
		this.OnChangeMass();
		this.OnChangeTemperature();
		this.OnDiseaseCountChange();
		this.OnChangeFOWReveal();
		DebugTool.Instance.Activate(DebugTool.Type.ReplaceSubstance);
	}

	// Token: 0x06006406 RID: 25606 RVA: 0x00259451 File Offset: 0x00257651
	private void OnClickStore()
	{
		this.OnChangeMass();
		this.OnChangeTemperature();
		this.OnDiseaseCountChange();
		this.OnChangeFOWReveal();
		DebugTool.Instance.Activate(DebugTool.Type.StoreSubstance);
	}

	// Token: 0x06006407 RID: 25607 RVA: 0x00259476 File Offset: 0x00257676
	private void OnClickSample()
	{
		this.OnChangeMass();
		this.OnChangeTemperature();
		this.OnDiseaseCountChange();
		this.OnChangeFOWReveal();
		DebugTool.Instance.Activate(DebugTool.Type.Sample);
	}

	// Token: 0x06006408 RID: 25608 RVA: 0x0025949B File Offset: 0x0025769B
	private void OnClickFill()
	{
		this.OnChangeMass();
		this.OnChangeTemperature();
		this.OnDiseaseCountChange();
		DebugTool.Instance.Activate(DebugTool.Type.FillReplaceSubstance);
	}

	// Token: 0x06006409 RID: 25609 RVA: 0x002594BA File Offset: 0x002576BA
	private void OnSelectElement(string str, int index)
	{
		this.element = (SimHashes)Enum.Parse(typeof(SimHashes), this.options_list[index]);
		this.elementButton.GetComponentInChildren<LocText>().text = str;
	}

	// Token: 0x0600640A RID: 25610 RVA: 0x002594F3 File Offset: 0x002576F3
	private void OnSelectElement(SimHashes element)
	{
		this.element = element;
		this.elementButton.GetComponentInChildren<LocText>().text = ElementLoader.FindElementByHash(element).name;
	}

	// Token: 0x0600640B RID: 25611 RVA: 0x00259518 File Offset: 0x00257718
	private void OnSelectDisease(string str, int index)
	{
		this.diseaseIdx = byte.MaxValue;
		for (int i = 0; i < Db.Get().Diseases.Count; i++)
		{
			if (Db.Get().Diseases[i].Name == str)
			{
				this.diseaseIdx = (byte)i;
			}
		}
		this.SelectDiseaseOption((int)this.diseaseIdx);
	}

	// Token: 0x0600640C RID: 25612 RVA: 0x0025957C File Offset: 0x0025777C
	private void SelectDiseaseOption(int diseaseIdx)
	{
		if (diseaseIdx == 255)
		{
			this.diseaseButton.GetComponentInChildren<LocText>().text = "None";
			return;
		}
		string name = Db.Get().Diseases[diseaseIdx].Name;
		this.diseaseButton.GetComponentInChildren<LocText>().text = name;
	}

	// Token: 0x0600640D RID: 25613 RVA: 0x002595D0 File Offset: 0x002577D0
	private void OnChangeFOWReveal()
	{
		if (this.paintPreventFOWReveal.isOn)
		{
			this.paintAllowFOWReveal.isOn = false;
		}
		if (this.paintAllowFOWReveal.isOn)
		{
			this.paintPreventFOWReveal.isOn = false;
		}
		this.set_prevent_fow_reveal = this.paintPreventFOWReveal.isOn;
		this.set_allow_fow_reveal = this.paintAllowFOWReveal.isOn;
	}

	// Token: 0x0600640E RID: 25614 RVA: 0x00259634 File Offset: 0x00257834
	public void OnChangeMass()
	{
		float num;
		try
		{
			num = Convert.ToSingle(this.massInput.text);
		}
		catch
		{
			num = -1f;
		}
		if (num <= 0f)
		{
			num = 1f;
			this.massInput.text = "1";
		}
		this.mass = num;
	}

	// Token: 0x0600640F RID: 25615 RVA: 0x00259694 File Offset: 0x00257894
	public void OnChangeTemperature()
	{
		float num;
		try
		{
			num = Convert.ToSingle(this.temperatureInput.text);
		}
		catch
		{
			num = -1f;
		}
		if (num <= 0f)
		{
			num = 1f;
			this.temperatureInput.text = "1";
		}
		this.temperature = num;
	}

	// Token: 0x06006410 RID: 25616 RVA: 0x002596F4 File Offset: 0x002578F4
	public void OnDiseaseCountChange()
	{
		int num;
		int.TryParse(this.diseaseCountInput.text, out num);
		if (num < 0)
		{
			num = 0;
			this.diseaseCountInput.text = "0";
		}
		this.diseaseCount = num;
	}

	// Token: 0x06006411 RID: 25617 RVA: 0x00259731 File Offset: 0x00257931
	public void OnElementsFilterEdited(string new_filter)
	{
		this.filter = (string.IsNullOrEmpty(this.filterInput.text) ? null : this.filterInput.text);
		this.FilterElements(this.filter);
	}

	// Token: 0x06006412 RID: 25618 RVA: 0x00259768 File Offset: 0x00257968
	public void SampleCell(int cell)
	{
		this.massInput.text = Grid.Mass[cell].ToString();
		this.temperatureInput.text = Grid.Temperature[cell].ToString();
		this.OnSelectElement(ElementLoader.GetElementID(Grid.Element[cell].tag));
		this.OnChangeMass();
		this.OnChangeTemperature();
	}

	// Token: 0x04004410 RID: 17424
	[Header("Current State")]
	public SimHashes element;

	// Token: 0x04004411 RID: 17425
	[NonSerialized]
	public float mass = 1000f;

	// Token: 0x04004412 RID: 17426
	[NonSerialized]
	public float temperature = -1f;

	// Token: 0x04004413 RID: 17427
	[NonSerialized]
	public bool set_prevent_fow_reveal;

	// Token: 0x04004414 RID: 17428
	[NonSerialized]
	public bool set_allow_fow_reveal;

	// Token: 0x04004415 RID: 17429
	[NonSerialized]
	public int diseaseCount;

	// Token: 0x04004416 RID: 17430
	public byte diseaseIdx;

	// Token: 0x04004417 RID: 17431
	[Header("Popup Buttons")]
	[SerializeField]
	private KButton elementButton;

	// Token: 0x04004418 RID: 17432
	[SerializeField]
	private KButton diseaseButton;

	// Token: 0x04004419 RID: 17433
	[Header("Popup Menus")]
	[SerializeField]
	private KPopupMenu elementPopup;

	// Token: 0x0400441A RID: 17434
	[SerializeField]
	private KPopupMenu diseasePopup;

	// Token: 0x0400441B RID: 17435
	[Header("Value Inputs")]
	[SerializeField]
	private KInputTextField massInput;

	// Token: 0x0400441C RID: 17436
	[SerializeField]
	private KInputTextField temperatureInput;

	// Token: 0x0400441D RID: 17437
	[SerializeField]
	private KInputTextField diseaseCountInput;

	// Token: 0x0400441E RID: 17438
	[SerializeField]
	private KInputTextField filterInput;

	// Token: 0x0400441F RID: 17439
	[Header("Tool Buttons")]
	[SerializeField]
	private KButton paintButton;

	// Token: 0x04004420 RID: 17440
	[SerializeField]
	private KButton fillButton;

	// Token: 0x04004421 RID: 17441
	[SerializeField]
	private KButton sampleButton;

	// Token: 0x04004422 RID: 17442
	[SerializeField]
	private KButton spawnButton;

	// Token: 0x04004423 RID: 17443
	[SerializeField]
	private KButton storeButton;

	// Token: 0x04004424 RID: 17444
	[Header("Parameter Toggles")]
	public Toggle paintElement;

	// Token: 0x04004425 RID: 17445
	public Toggle paintMass;

	// Token: 0x04004426 RID: 17446
	public Toggle paintTemperature;

	// Token: 0x04004427 RID: 17447
	public Toggle paintDisease;

	// Token: 0x04004428 RID: 17448
	public Toggle paintDiseaseCount;

	// Token: 0x04004429 RID: 17449
	public Toggle affectBuildings;

	// Token: 0x0400442A RID: 17450
	public Toggle affectCells;

	// Token: 0x0400442B RID: 17451
	public Toggle paintPreventFOWReveal;

	// Token: 0x0400442C RID: 17452
	public Toggle paintAllowFOWReveal;

	// Token: 0x0400442D RID: 17453
	private List<KInputTextField> inputFields = new List<KInputTextField>();

	// Token: 0x0400442E RID: 17454
	private List<string> options_list = new List<string>();

	// Token: 0x0400442F RID: 17455
	private string filter;

	// Token: 0x02001E7C RID: 7804
	private struct ElemDisplayInfo
	{
		// Token: 0x04008DA3 RID: 36259
		public SimHashes id;

		// Token: 0x04008DA4 RID: 36260
		public string displayStr;
	}
}
