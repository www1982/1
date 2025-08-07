using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Klei;
using Klei.AI;
using KSerialization;
using UnityEngine;

// Token: 0x02000A78 RID: 2680
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/PrimaryElement")]
public class PrimaryElement : KMonoBehaviour, ISaveLoadable
{
	// Token: 0x06004DA2 RID: 19874 RVA: 0x001C17CC File Offset: 0x001BF9CC
	public void SetUseSimDiseaseInfo(bool use)
	{
		this.useSimDiseaseInfo = use;
	}

	// Token: 0x17000547 RID: 1351
	// (get) Token: 0x06004DA3 RID: 19875 RVA: 0x001C17D5 File Offset: 0x001BF9D5
	// (set) Token: 0x06004DA4 RID: 19876 RVA: 0x001C17E0 File Offset: 0x001BF9E0
	[Serialize]
	public float Units
	{
		get
		{
			return this._units;
		}
		set
		{
			if (float.IsInfinity(value) || float.IsNaN(value))
			{
				DebugUtil.DevLogError("Invalid units value for element, setting Units to 0");
				this._units = 0f;
			}
			else
			{
				this._units = value;
			}
			if (this.onDataChanged != null)
			{
				this.onDataChanged(this);
			}
		}
	}

	// Token: 0x17000548 RID: 1352
	// (get) Token: 0x06004DA5 RID: 19877 RVA: 0x001C182F File Offset: 0x001BFA2F
	// (set) Token: 0x06004DA6 RID: 19878 RVA: 0x001C183D File Offset: 0x001BFA3D
	public float Temperature
	{
		get
		{
			return this.getTemperatureCallback(this);
		}
		set
		{
			this.SetTemperature(value);
		}
	}

	// Token: 0x17000549 RID: 1353
	// (get) Token: 0x06004DA7 RID: 19879 RVA: 0x001C1846 File Offset: 0x001BFA46
	// (set) Token: 0x06004DA8 RID: 19880 RVA: 0x001C184E File Offset: 0x001BFA4E
	public float InternalTemperature
	{
		get
		{
			return this._Temperature;
		}
		set
		{
			this._Temperature = value;
		}
	}

	// Token: 0x06004DA9 RID: 19881 RVA: 0x001C1858 File Offset: 0x001BFA58
	[OnSerializing]
	private void OnSerializing()
	{
		this._Temperature = this.Temperature;
		this.SanitizeMassAndTemperature();
		this.diseaseID.HashValue = 0;
		this.diseaseCount = 0;
		if (this.useSimDiseaseInfo)
		{
			int num = Grid.PosToCell(base.transform.GetPosition());
			if (Grid.DiseaseIdx[num] != 255)
			{
				this.diseaseID = Db.Get().Diseases[(int)Grid.DiseaseIdx[num]].id;
				this.diseaseCount = Grid.DiseaseCount[num];
				return;
			}
		}
		else if (this.diseaseHandle.IsValid())
		{
			DiseaseHeader header = GameComps.DiseaseContainers.GetHeader(this.diseaseHandle);
			if (header.diseaseIdx != 255)
			{
				this.diseaseID = Db.Get().Diseases[(int)header.diseaseIdx].id;
				this.diseaseCount = header.diseaseCount;
			}
		}
	}

	// Token: 0x06004DAA RID: 19882 RVA: 0x001C1948 File Offset: 0x001BFB48
	[OnDeserialized]
	private void OnDeserialized()
	{
		if (this.ElementID == (SimHashes)351109216)
		{
			this.ElementID = SimHashes.Creature;
		}
		this.SanitizeMassAndTemperature();
		float num = this._Temperature;
		if (float.IsNaN(num) || float.IsInfinity(num) || num < 0f || 10000f < num)
		{
			DeserializeWarnings.Instance.PrimaryElementTemperatureIsNan.Warn(string.Format("{0} has invalid temperature of {1}. Resetting temperature.", base.name, this.Temperature), null);
			num = this.Element.defaultValues.temperature;
		}
		this._Temperature = num;
		this.Temperature = num;
		if (this.Element == null)
		{
			DeserializeWarnings.Instance.PrimaryElementHasNoElement.Warn(base.name + "Primary element has no element.", null);
		}
		if (this.Mass < 0f)
		{
			DebugUtil.DevLogError(base.gameObject, "deserialized ore with less than 0 mass. Error! Destroying");
			Util.KDestroyGameObject(base.gameObject);
			return;
		}
		if (this.Mass == 0f && !this.KeepZeroMassObject)
		{
			DebugUtil.DevLogError(base.gameObject, "deserialized element with 0 mass. Destroying");
			Util.KDestroyGameObject(base.gameObject);
			return;
		}
		if (this.onDataChanged != null)
		{
			this.onDataChanged(this);
		}
		byte index = Db.Get().Diseases.GetIndex(this.diseaseID);
		if (index == 255 || this.diseaseCount <= 0)
		{
			if (this.diseaseHandle.IsValid())
			{
				GameComps.DiseaseContainers.Remove(base.gameObject);
				this.diseaseHandle.Clear();
				return;
			}
		}
		else
		{
			if (this.diseaseHandle.IsValid())
			{
				DiseaseHeader header = GameComps.DiseaseContainers.GetHeader(this.diseaseHandle);
				header.diseaseIdx = index;
				header.diseaseCount = this.diseaseCount;
				GameComps.DiseaseContainers.SetHeader(this.diseaseHandle, header);
				return;
			}
			this.diseaseHandle = GameComps.DiseaseContainers.Add(base.gameObject, index, this.diseaseCount);
		}
	}

	// Token: 0x06004DAB RID: 19883 RVA: 0x001C1B2C File Offset: 0x001BFD2C
	protected override void OnLoadLevel()
	{
		base.OnLoadLevel();
	}

	// Token: 0x06004DAC RID: 19884 RVA: 0x001C1B34 File Offset: 0x001BFD34
	private void SanitizeMassAndTemperature()
	{
		if (this._Temperature <= 0f)
		{
			DebugUtil.DevLogError(base.gameObject.name + " is attempting to serialize a temperature of <= 0K. Resetting to default. world=" + base.gameObject.DebugGetMyWorldName());
			this._Temperature = this.Element.defaultValues.temperature;
		}
		if (this.Mass > PrimaryElement.MAX_MASS)
		{
			DebugUtil.DevLogError(string.Format("{0} is attempting to serialize very large mass {1}. Resetting to default. world={2}", base.gameObject.name, this.Mass, base.gameObject.DebugGetMyWorldName()));
			this.Mass = this.Element.defaultValues.mass;
		}
	}

	// Token: 0x1700054A RID: 1354
	// (get) Token: 0x06004DAD RID: 19885 RVA: 0x001C1BDC File Offset: 0x001BFDDC
	// (set) Token: 0x06004DAE RID: 19886 RVA: 0x001C1BEB File Offset: 0x001BFDEB
	public float Mass
	{
		get
		{
			return this.Units * this.MassPerUnit;
		}
		set
		{
			this.SetMass(value);
			if (this.onDataChanged != null)
			{
				this.onDataChanged(this);
			}
		}
	}

	// Token: 0x06004DAF RID: 19887 RVA: 0x001C1C08 File Offset: 0x001BFE08
	private void SetMass(float mass)
	{
		if ((mass > PrimaryElement.MAX_MASS || mass < 0f) && this.ElementID != SimHashes.Regolith)
		{
			DebugUtil.DevLogErrorFormat(base.gameObject, "{0} is getting an abnormal mass set {1}.", new object[]
			{
				base.gameObject.name,
				mass
			});
		}
		mass = Mathf.Clamp(mass, 0f, PrimaryElement.MAX_MASS);
		this.Units = mass / this.MassPerUnit;
		if (this.Units <= 0f && !this.KeepZeroMassObject)
		{
			Util.KDestroyGameObject(base.gameObject);
		}
	}

	// Token: 0x06004DB0 RID: 19888 RVA: 0x001C1CA0 File Offset: 0x001BFEA0
	private void SetTemperature(float temperature)
	{
		if (float.IsNaN(temperature) || float.IsInfinity(temperature))
		{
			DebugUtil.LogErrorArgs(base.gameObject, new object[] { "Invalid temperature [" + temperature.ToString() + "]" });
			return;
		}
		if (temperature <= 0f)
		{
			KCrashReporter.Assert(false, "Tried to set PrimaryElement.Temperature to a value <= 0", null);
		}
		this.setTemperatureCallback(this, temperature);
	}

	// Token: 0x06004DB1 RID: 19889 RVA: 0x001C1D09 File Offset: 0x001BFF09
	public void SetMassTemperature(float mass, float temperature)
	{
		this.SetMass(mass);
		this.SetTemperature(temperature);
	}

	// Token: 0x1700054B RID: 1355
	// (get) Token: 0x06004DB2 RID: 19890 RVA: 0x001C1D19 File Offset: 0x001BFF19
	public Element Element
	{
		get
		{
			if (this._Element == null)
			{
				this._Element = ElementLoader.FindElementByHash(this.ElementID);
			}
			return this._Element;
		}
	}

	// Token: 0x1700054C RID: 1356
	// (get) Token: 0x06004DB3 RID: 19891 RVA: 0x001C1D3C File Offset: 0x001BFF3C
	public byte DiseaseIdx
	{
		get
		{
			if (this.diseaseRedirectTarget)
			{
				return this.diseaseRedirectTarget.DiseaseIdx;
			}
			byte b = byte.MaxValue;
			if (this.useSimDiseaseInfo)
			{
				int num = Grid.PosToCell(base.transform.GetPosition());
				b = Grid.DiseaseIdx[num];
			}
			else if (this.diseaseHandle.IsValid())
			{
				b = GameComps.DiseaseContainers.GetHeader(this.diseaseHandle).diseaseIdx;
			}
			return b;
		}
	}

	// Token: 0x1700054D RID: 1357
	// (get) Token: 0x06004DB4 RID: 19892 RVA: 0x001C1DB4 File Offset: 0x001BFFB4
	public int DiseaseCount
	{
		get
		{
			if (this.diseaseRedirectTarget)
			{
				return this.diseaseRedirectTarget.DiseaseCount;
			}
			int num = 0;
			if (this.useSimDiseaseInfo)
			{
				int num2 = Grid.PosToCell(base.transform.GetPosition());
				num = Grid.DiseaseCount[num2];
			}
			else if (this.diseaseHandle.IsValid())
			{
				num = GameComps.DiseaseContainers.GetHeader(this.diseaseHandle).diseaseCount;
			}
			return num;
		}
	}

	// Token: 0x06004DB5 RID: 19893 RVA: 0x001C1E27 File Offset: 0x001C0027
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		GameComps.InfraredVisualizers.Add(base.gameObject);
		base.Subscribe<PrimaryElement>(1335436905, PrimaryElement.OnSplitFromChunkDelegate);
		base.Subscribe<PrimaryElement>(-2064133523, PrimaryElement.OnAbsorbDelegate);
	}

	// Token: 0x06004DB6 RID: 19894 RVA: 0x001C1E64 File Offset: 0x001C0064
	protected override void OnSpawn()
	{
		Attributes attributes = this.GetAttributes();
		if (attributes != null)
		{
			foreach (AttributeModifier attributeModifier in this.Element.attributeModifiers)
			{
				attributes.Add(attributeModifier);
			}
		}
	}

	// Token: 0x06004DB7 RID: 19895 RVA: 0x001C1EC8 File Offset: 0x001C00C8
	public void ForcePermanentDiseaseContainer(bool force_on)
	{
		if (force_on)
		{
			if (!this.diseaseHandle.IsValid())
			{
				this.diseaseHandle = GameComps.DiseaseContainers.Add(base.gameObject, byte.MaxValue, 0);
			}
		}
		else if (this.diseaseHandle.IsValid() && this.DiseaseIdx == 255)
		{
			GameComps.DiseaseContainers.Remove(base.gameObject);
			this.diseaseHandle.Clear();
		}
		this.forcePermanentDiseaseContainer = force_on;
	}

	// Token: 0x06004DB8 RID: 19896 RVA: 0x001C1F3F File Offset: 0x001C013F
	protected override void OnCleanUp()
	{
		GameComps.InfraredVisualizers.Remove(base.gameObject);
		if (this.diseaseHandle.IsValid())
		{
			GameComps.DiseaseContainers.Remove(base.gameObject);
			this.diseaseHandle.Clear();
		}
		base.OnCleanUp();
	}

	// Token: 0x06004DB9 RID: 19897 RVA: 0x001C1F7F File Offset: 0x001C017F
	public void SetElement(SimHashes element_id, bool addTags = true)
	{
		this.ElementID = element_id;
		if (addTags)
		{
			this.UpdateTags();
		}
	}

	// Token: 0x06004DBA RID: 19898 RVA: 0x001C1F94 File Offset: 0x001C0194
	public void UpdateTags()
	{
		if (this.ElementID == (SimHashes)0)
		{
			global::Debug.Log("UpdateTags() Primary element 0", base.gameObject);
			return;
		}
		KPrefabID component = base.GetComponent<KPrefabID>();
		if (component != null)
		{
			List<Tag> list = new List<Tag>();
			foreach (Tag tag in this.Element.oreTags)
			{
				list.Add(tag);
			}
			if (component.HasAnyTags(PrimaryElement.metalTags))
			{
				list.Add(GameTags.StoredMetal);
			}
			foreach (Tag tag2 in list)
			{
				component.AddTag(tag2, false);
			}
		}
	}

	// Token: 0x06004DBB RID: 19899 RVA: 0x001C2058 File Offset: 0x001C0258
	public void ModifyDiseaseCount(int delta, string reason)
	{
		if (this.diseaseRedirectTarget)
		{
			this.diseaseRedirectTarget.ModifyDiseaseCount(delta, reason);
			return;
		}
		if (this.useSimDiseaseInfo)
		{
			SimMessages.ModifyDiseaseOnCell(Grid.PosToCell(this), byte.MaxValue, delta);
			return;
		}
		if (delta != 0 && this.diseaseHandle.IsValid() && GameComps.DiseaseContainers.ModifyDiseaseCount(this.diseaseHandle, delta) <= 0 && !this.forcePermanentDiseaseContainer)
		{
			base.Trigger(-1689370368, false);
			GameComps.DiseaseContainers.Remove(base.gameObject);
			this.diseaseHandle.Clear();
		}
	}

	// Token: 0x06004DBC RID: 19900 RVA: 0x001C20F4 File Offset: 0x001C02F4
	public void AddDisease(byte disease_idx, int delta, string reason)
	{
		if (delta == 0)
		{
			return;
		}
		if (this.diseaseRedirectTarget)
		{
			this.diseaseRedirectTarget.AddDisease(disease_idx, delta, reason);
			return;
		}
		if (this.useSimDiseaseInfo)
		{
			SimMessages.ModifyDiseaseOnCell(Grid.PosToCell(this), disease_idx, delta);
			return;
		}
		if (this.diseaseHandle.IsValid())
		{
			if (GameComps.DiseaseContainers.AddDisease(this.diseaseHandle, disease_idx, delta) <= 0)
			{
				GameComps.DiseaseContainers.Remove(base.gameObject);
				this.diseaseHandle.Clear();
				return;
			}
		}
		else if (delta > 0)
		{
			this.diseaseHandle = GameComps.DiseaseContainers.Add(base.gameObject, disease_idx, delta);
			base.Trigger(-1689370368, true);
			base.Trigger(-283306403, null);
		}
	}

	// Token: 0x06004DBD RID: 19901 RVA: 0x001C21AE File Offset: 0x001C03AE
	private static float OnGetTemperature(PrimaryElement primary_element)
	{
		return primary_element._Temperature;
	}

	// Token: 0x06004DBE RID: 19902 RVA: 0x001C21B8 File Offset: 0x001C03B8
	private static void OnSetTemperature(PrimaryElement primary_element, float temperature)
	{
		global::Debug.Assert(!float.IsNaN(temperature));
		if (temperature <= 0f)
		{
			DebugUtil.LogErrorArgs(primary_element.gameObject, new object[] { primary_element.gameObject.name + " has a temperature of zero which has always been an error in my experience." });
		}
		primary_element._Temperature = temperature;
	}

	// Token: 0x06004DBF RID: 19903 RVA: 0x001C220C File Offset: 0x001C040C
	private void OnSplitFromChunk(object data)
	{
		Pickupable pickupable = (Pickupable)data;
		if (pickupable == null)
		{
			return;
		}
		float num = this.Units / (this.Units + pickupable.PrimaryElement.Units);
		SimUtil.DiseaseInfo percentOfDisease = SimUtil.GetPercentOfDisease(pickupable.PrimaryElement, num);
		this.AddDisease(percentOfDisease.idx, percentOfDisease.count, "PrimaryElement.SplitFromChunk");
		pickupable.PrimaryElement.ModifyDiseaseCount(-percentOfDisease.count, "PrimaryElement.SplitFromChunk");
	}

	// Token: 0x06004DC0 RID: 19904 RVA: 0x001C2280 File Offset: 0x001C0480
	private void OnAbsorb(object data)
	{
		Pickupable pickupable = (Pickupable)data;
		if (pickupable == null)
		{
			return;
		}
		this.AddDisease(pickupable.PrimaryElement.DiseaseIdx, pickupable.PrimaryElement.DiseaseCount, "PrimaryElement.OnAbsorb");
	}

	// Token: 0x06004DC1 RID: 19905 RVA: 0x001C22C0 File Offset: 0x001C04C0
	private void SetDiseaseVisualProvider(GameObject visualizer)
	{
		HandleVector<int>.Handle handle = GameComps.DiseaseContainers.GetHandle(base.gameObject);
		if (handle != HandleVector<int>.InvalidHandle)
		{
			DiseaseContainer payload = GameComps.DiseaseContainers.GetPayload(handle);
			payload.visualDiseaseProvider = visualizer;
			GameComps.DiseaseContainers.SetPayload(handle, ref payload);
		}
	}

	// Token: 0x06004DC2 RID: 19906 RVA: 0x001C230C File Offset: 0x001C050C
	public void RedirectDisease(GameObject target)
	{
		this.SetDiseaseVisualProvider(target);
		this.diseaseRedirectTarget = (target ? target.GetComponent<PrimaryElement>() : null);
		global::Debug.Assert(this.diseaseRedirectTarget != this, "Disease redirect target set to myself");
	}

	// Token: 0x0400339E RID: 13214
	public static float MAX_MASS = 100000f;

	// Token: 0x0400339F RID: 13215
	public SimTemperatureTransfer sttOptimizationHook;

	// Token: 0x040033A0 RID: 13216
	public PrimaryElement.GetTemperatureCallback getTemperatureCallback = new PrimaryElement.GetTemperatureCallback(PrimaryElement.OnGetTemperature);

	// Token: 0x040033A1 RID: 13217
	public PrimaryElement.SetTemperatureCallback setTemperatureCallback = new PrimaryElement.SetTemperatureCallback(PrimaryElement.OnSetTemperature);

	// Token: 0x040033A2 RID: 13218
	private PrimaryElement diseaseRedirectTarget;

	// Token: 0x040033A3 RID: 13219
	private bool useSimDiseaseInfo;

	// Token: 0x040033A4 RID: 13220
	public const float DefaultChunkMass = 400f;

	// Token: 0x040033A5 RID: 13221
	private static readonly Tag[] metalTags = new Tag[]
	{
		GameTags.Metal,
		GameTags.RefinedMetal
	};

	// Token: 0x040033A6 RID: 13222
	[Serialize]
	[HashedEnum]
	public SimHashes ElementID;

	// Token: 0x040033A7 RID: 13223
	private float _units = 1f;

	// Token: 0x040033A8 RID: 13224
	[Serialize]
	[SerializeField]
	private float _Temperature;

	// Token: 0x040033A9 RID: 13225
	[Serialize]
	[NonSerialized]
	public bool KeepZeroMassObject;

	// Token: 0x040033AA RID: 13226
	[Serialize]
	private HashedString diseaseID;

	// Token: 0x040033AB RID: 13227
	[Serialize]
	private int diseaseCount;

	// Token: 0x040033AC RID: 13228
	private HandleVector<int>.Handle diseaseHandle = HandleVector<int>.InvalidHandle;

	// Token: 0x040033AD RID: 13229
	public float MassPerUnit = 1f;

	// Token: 0x040033AE RID: 13230
	[NonSerialized]
	private Element _Element;

	// Token: 0x040033AF RID: 13231
	[NonSerialized]
	public Action<PrimaryElement> onDataChanged;

	// Token: 0x040033B0 RID: 13232
	[NonSerialized]
	private bool forcePermanentDiseaseContainer;

	// Token: 0x040033B1 RID: 13233
	private static readonly EventSystem.IntraObjectHandler<PrimaryElement> OnSplitFromChunkDelegate = new EventSystem.IntraObjectHandler<PrimaryElement>(delegate(PrimaryElement component, object data)
	{
		component.OnSplitFromChunk(data);
	});

	// Token: 0x040033B2 RID: 13234
	private static readonly EventSystem.IntraObjectHandler<PrimaryElement> OnAbsorbDelegate = new EventSystem.IntraObjectHandler<PrimaryElement>(delegate(PrimaryElement component, object data)
	{
		component.OnAbsorb(data);
	});

	// Token: 0x02001B5C RID: 7004
	// (Invoke) Token: 0x0600A770 RID: 42864
	public delegate float GetTemperatureCallback(PrimaryElement primary_element);

	// Token: 0x02001B5D RID: 7005
	// (Invoke) Token: 0x0600A774 RID: 42868
	public delegate void SetTemperatureCallback(PrimaryElement primary_element, float temperature);
}
