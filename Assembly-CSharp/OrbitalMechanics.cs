using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x02000A46 RID: 2630
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/OrbitalMechanics")]
public class OrbitalMechanics : KMonoBehaviour
{
	// Token: 0x06004C4E RID: 19534 RVA: 0x001BA8EF File Offset: 0x001B8AEF
	protected override void OnPrefabInit()
	{
		base.Subscribe<OrbitalMechanics>(-1298331547, this.OnClusterLocationChangedDelegate);
	}

	// Token: 0x06004C4F RID: 19535 RVA: 0x001BA904 File Offset: 0x001B8B04
	private void OnClusterLocationChanged(object data)
	{
		ClusterLocationChangedEvent clusterLocationChangedEvent = (ClusterLocationChangedEvent)data;
		this.UpdateLocation(clusterLocationChangedEvent.newLocation);
	}

	// Token: 0x06004C50 RID: 19536 RVA: 0x001BA924 File Offset: 0x001B8B24
	protected override void OnCleanUp()
	{
		if (this.orbitingObjects != null)
		{
			foreach (Ref<OrbitalObject> @ref in this.orbitingObjects)
			{
				if (!@ref.Get().IsNullOrDestroyed())
				{
					Util.KDestroyGameObject(@ref.Get());
				}
			}
		}
	}

	// Token: 0x06004C51 RID: 19537 RVA: 0x001BA990 File Offset: 0x001B8B90
	[ContextMenu("Rebuild")]
	private void Rebuild()
	{
		List<string> list = new List<string>();
		if (this.orbitingObjects != null)
		{
			foreach (Ref<OrbitalObject> @ref in this.orbitingObjects)
			{
				if (!@ref.Get().IsNullOrDestroyed())
				{
					list.Add(@ref.Get().orbitalDBId);
					Util.KDestroyGameObject(@ref.Get());
				}
			}
			this.orbitingObjects = new List<Ref<OrbitalObject>>();
		}
		if (list.Count > 0)
		{
			for (int i = 0; i < list.Count; i++)
			{
				this.CreateOrbitalObject(list[i]);
			}
		}
	}

	// Token: 0x06004C52 RID: 19538 RVA: 0x001BAA48 File Offset: 0x001B8C48
	private void UpdateLocation(AxialI location)
	{
		if (this.orbitingObjects.Count > 0)
		{
			foreach (Ref<OrbitalObject> @ref in this.orbitingObjects)
			{
				if (!@ref.Get().IsNullOrDestroyed())
				{
					Util.KDestroyGameObject(@ref.Get());
				}
			}
			this.orbitingObjects = new List<Ref<OrbitalObject>>();
		}
		ClusterGridEntity visibleEntityOfLayerAtCell = ClusterGrid.Instance.GetVisibleEntityOfLayerAtCell(location, EntityLayer.POI);
		if (visibleEntityOfLayerAtCell != null)
		{
			ArtifactPOIClusterGridEntity component = visibleEntityOfLayerAtCell.GetComponent<ArtifactPOIClusterGridEntity>();
			if (component != null)
			{
				ArtifactPOIStates.Instance smi = component.GetSMI<ArtifactPOIStates.Instance>();
				if (smi != null && smi.configuration.poiType.orbitalObject != null)
				{
					foreach (string text in smi.configuration.poiType.orbitalObject)
					{
						this.CreateOrbitalObject(text);
					}
				}
			}
			HarvestablePOIClusterGridEntity component2 = visibleEntityOfLayerAtCell.GetComponent<HarvestablePOIClusterGridEntity>();
			if (component2 != null)
			{
				HarvestablePOIStates.Instance smi2 = component2.GetSMI<HarvestablePOIStates.Instance>();
				if (smi2 != null && smi2.configuration.poiType.orbitalObject != null)
				{
					List<string> orbitalObject = smi2.configuration.poiType.orbitalObject;
					KRandom krandom = new KRandom();
					float num = smi2.poiCapacity / smi2.configuration.GetMaxCapacity() * (float)smi2.configuration.poiType.maxNumOrbitingObjects;
					int num2 = 0;
					while ((float)num2 < num)
					{
						int num3 = krandom.Next(orbitalObject.Count);
						this.CreateOrbitalObject(orbitalObject[num3]);
						num2++;
					}
					return;
				}
			}
		}
		else
		{
			Clustercraft component3 = base.GetComponent<Clustercraft>();
			if (component3 != null)
			{
				if (component3.GetOrbitAsteroid() != null || component3.Status == Clustercraft.CraftStatus.Launching)
				{
					this.CreateOrbitalObject(Db.Get().OrbitalTypeCategories.orbit.Id);
					return;
				}
				if (component3.Status == Clustercraft.CraftStatus.Landing)
				{
					this.CreateOrbitalObject(Db.Get().OrbitalTypeCategories.landed.Id);
				}
			}
		}
	}

	// Token: 0x06004C53 RID: 19539 RVA: 0x001BAC78 File Offset: 0x001B8E78
	public void CreateOrbitalObject(string orbit_db_name)
	{
		WorldContainer component = base.GetComponent<WorldContainer>();
		GameObject gameObject = Util.KInstantiate(Assets.GetPrefab(OrbitalBGConfig.ID), base.gameObject, null);
		OrbitalObject component2 = gameObject.GetComponent<OrbitalObject>();
		component2.Init(orbit_db_name, component, this.orbitingObjects);
		gameObject.SetActive(true);
		this.orbitingObjects.Add(new Ref<OrbitalObject>(component2));
	}

	// Token: 0x04003290 RID: 12944
	[Serialize]
	private List<Ref<OrbitalObject>> orbitingObjects = new List<Ref<OrbitalObject>>();

	// Token: 0x04003291 RID: 12945
	private EventSystem.IntraObjectHandler<OrbitalMechanics> OnClusterLocationChangedDelegate = new EventSystem.IntraObjectHandler<OrbitalMechanics>(delegate(OrbitalMechanics cmp, object data)
	{
		cmp.OnClusterLocationChanged(data);
	});
}
