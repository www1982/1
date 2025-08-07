using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Database;
using Klei.AI;
using Klei.AI.DiseaseGrowthRules;
using STRINGS;

// Token: 0x02000C42 RID: 3138
public static class SimMessages
{
	// Token: 0x06005FBB RID: 24507 RVA: 0x00233D08 File Offset: 0x00231F08
	public unsafe static void AddElementConsumer(int gameCell, ElementConsumer.Configuration configuration, SimHashes element, byte radius, int cb_handle)
	{
		Debug.Assert(Grid.IsValidCell(gameCell));
		if (!Grid.IsValidCell(gameCell))
		{
			return;
		}
		ushort elementIndex = ElementLoader.GetElementIndex(element);
		SimMessages.AddElementConsumerMessage* ptr;
		checked
		{
			ptr = stackalloc SimMessages.AddElementConsumerMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.AddElementConsumerMessage)];
			ptr->cellIdx = gameCell;
		}
		ptr->configuration = (byte)configuration;
		ptr->elementIdx = elementIndex;
		ptr->radius = radius;
		ptr->callbackIdx = cb_handle;
		Sim.SIM_HandleMessage(2024405073, sizeof(SimMessages.AddElementConsumerMessage), (byte*)ptr);
	}

	// Token: 0x06005FBC RID: 24508 RVA: 0x00233D74 File Offset: 0x00231F74
	public unsafe static void SetElementConsumerData(int sim_handle, int cell, float consumptionRate)
	{
		if (!Sim.IsValidHandle(sim_handle))
		{
			return;
		}
		checked
		{
			SimMessages.SetElementConsumerDataMessage* ptr = stackalloc SimMessages.SetElementConsumerDataMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.SetElementConsumerDataMessage)];
			ptr->handle = sim_handle;
			ptr->cell = cell;
			ptr->consumptionRate = consumptionRate;
			Sim.SIM_HandleMessage(1575539738, sizeof(SimMessages.SetElementConsumerDataMessage), (byte*)ptr);
		}
	}

	// Token: 0x06005FBD RID: 24509 RVA: 0x00233DC0 File Offset: 0x00231FC0
	public unsafe static void RemoveElementConsumer(int cb_handle, int sim_handle)
	{
		if (!Sim.IsValidHandle(sim_handle))
		{
			Debug.Assert(false, "Invalid handle");
			return;
		}
		checked
		{
			SimMessages.RemoveElementConsumerMessage* ptr = stackalloc SimMessages.RemoveElementConsumerMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.RemoveElementConsumerMessage)];
			ptr->callbackIdx = cb_handle;
			ptr->handle = sim_handle;
			Sim.SIM_HandleMessage(894417742, sizeof(SimMessages.RemoveElementConsumerMessage), (byte*)ptr);
		}
	}

	// Token: 0x06005FBE RID: 24510 RVA: 0x00233E10 File Offset: 0x00232010
	public unsafe static void AddElementEmitter(float max_pressure, int on_registered, int on_blocked = -1, int on_unblocked = -1)
	{
		checked
		{
			SimMessages.AddElementEmitterMessage* ptr = stackalloc SimMessages.AddElementEmitterMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.AddElementEmitterMessage)];
			ptr->maxPressure = max_pressure;
			ptr->callbackIdx = on_registered;
			ptr->onBlockedCB = on_blocked;
			ptr->onUnblockedCB = on_unblocked;
			Sim.SIM_HandleMessage(-505471181, sizeof(SimMessages.AddElementEmitterMessage), (byte*)ptr);
		}
	}

	// Token: 0x06005FBF RID: 24511 RVA: 0x00233E58 File Offset: 0x00232058
	public unsafe static void ModifyElementEmitter(int sim_handle, int game_cell, int max_depth, SimHashes element, float emit_interval, float emit_mass, float emit_temperature, float max_pressure, byte disease_idx, int disease_count)
	{
		Debug.Assert(Grid.IsValidCell(game_cell));
		if (!Grid.IsValidCell(game_cell))
		{
			return;
		}
		ushort elementIndex = ElementLoader.GetElementIndex(element);
		SimMessages.ModifyElementEmitterMessage* ptr;
		checked
		{
			ptr = stackalloc SimMessages.ModifyElementEmitterMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.ModifyElementEmitterMessage)];
			ptr->handle = sim_handle;
			ptr->cellIdx = game_cell;
			ptr->emitInterval = emit_interval;
			ptr->emitMass = emit_mass;
			ptr->emitTemperature = emit_temperature;
			ptr->maxPressure = max_pressure;
			ptr->elementIdx = elementIndex;
		}
		ptr->maxDepth = (byte)max_depth;
		ptr->diseaseIdx = disease_idx;
		ptr->diseaseCount = disease_count;
		Sim.SIM_HandleMessage(403589164, sizeof(SimMessages.ModifyElementEmitterMessage), (byte*)ptr);
	}

	// Token: 0x06005FC0 RID: 24512 RVA: 0x00233EEC File Offset: 0x002320EC
	public unsafe static void RemoveElementEmitter(int cb_handle, int sim_handle)
	{
		if (!Sim.IsValidHandle(sim_handle))
		{
			Debug.Assert(false, "Invalid handle");
			return;
		}
		checked
		{
			SimMessages.RemoveElementEmitterMessage* ptr = stackalloc SimMessages.RemoveElementEmitterMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.RemoveElementEmitterMessage)];
			ptr->callbackIdx = cb_handle;
			ptr->handle = sim_handle;
			Sim.SIM_HandleMessage(-1524118282, sizeof(SimMessages.RemoveElementEmitterMessage), (byte*)ptr);
		}
	}

	// Token: 0x06005FC1 RID: 24513 RVA: 0x00233F3C File Offset: 0x0023213C
	public unsafe static void AddRadiationEmitter(int on_registered, int game_cell, short emitRadiusX, short emitRadiusY, float emitRads, float emitRate, float emitSpeed, float emitDirection, float emitAngle, RadiationEmitter.RadiationEmitterType emitType)
	{
		checked
		{
			SimMessages.AddRadiationEmitterMessage* ptr = stackalloc SimMessages.AddRadiationEmitterMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.AddRadiationEmitterMessage)];
			ptr->callbackIdx = on_registered;
			ptr->cell = game_cell;
			ptr->emitRadiusX = emitRadiusX;
			ptr->emitRadiusY = emitRadiusY;
			ptr->emitRads = emitRads;
			ptr->emitRate = emitRate;
			ptr->emitSpeed = emitSpeed;
			ptr->emitDirection = emitDirection;
			ptr->emitAngle = emitAngle;
			ptr->emitType = (int)emitType;
			Sim.SIM_HandleMessage(-1505895314, sizeof(SimMessages.AddRadiationEmitterMessage), (byte*)ptr);
		}
	}

	// Token: 0x06005FC2 RID: 24514 RVA: 0x00233FB4 File Offset: 0x002321B4
	public unsafe static void ModifyRadiationEmitter(int sim_handle, int game_cell, short emitRadiusX, short emitRadiusY, float emitRads, float emitRate, float emitSpeed, float emitDirection, float emitAngle, RadiationEmitter.RadiationEmitterType emitType)
	{
		if (!Grid.IsValidCell(game_cell))
		{
			return;
		}
		checked
		{
			SimMessages.ModifyRadiationEmitterMessage* ptr = stackalloc SimMessages.ModifyRadiationEmitterMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.ModifyRadiationEmitterMessage)];
			ptr->handle = sim_handle;
			ptr->cell = game_cell;
			ptr->callbackIdx = -1;
			ptr->emitRadiusX = emitRadiusX;
			ptr->emitRadiusY = emitRadiusY;
			ptr->emitRads = emitRads;
			ptr->emitRate = emitRate;
			ptr->emitSpeed = emitSpeed;
			ptr->emitDirection = emitDirection;
			ptr->emitAngle = emitAngle;
			ptr->emitType = (int)emitType;
			Sim.SIM_HandleMessage(-503965465, sizeof(SimMessages.ModifyRadiationEmitterMessage), (byte*)ptr);
		}
	}

	// Token: 0x06005FC3 RID: 24515 RVA: 0x0023403C File Offset: 0x0023223C
	public unsafe static void RemoveRadiationEmitter(int cb_handle, int sim_handle)
	{
		if (!Sim.IsValidHandle(sim_handle))
		{
			Debug.Assert(false, "Invalid handle");
			return;
		}
		checked
		{
			SimMessages.RemoveRadiationEmitterMessage* ptr = stackalloc SimMessages.RemoveRadiationEmitterMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.RemoveRadiationEmitterMessage)];
			ptr->callbackIdx = cb_handle;
			ptr->handle = sim_handle;
			Sim.SIM_HandleMessage(-704259919, sizeof(SimMessages.RemoveRadiationEmitterMessage), (byte*)ptr);
		}
	}

	// Token: 0x06005FC4 RID: 24516 RVA: 0x0023408C File Offset: 0x0023228C
	public unsafe static void AddElementChunk(int gameCell, SimHashes element, float mass, float temperature, float surface_area, float thickness, float ground_transfer_scale, int cb_handle)
	{
		Debug.Assert(Grid.IsValidCell(gameCell));
		if (!Grid.IsValidCell(gameCell))
		{
			return;
		}
		if (mass * temperature > 0f)
		{
			ushort elementIndex = ElementLoader.GetElementIndex(element);
			checked
			{
				SimMessages.AddElementChunkMessage* ptr = stackalloc SimMessages.AddElementChunkMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.AddElementChunkMessage)];
				ptr->gameCell = gameCell;
				ptr->callbackIdx = cb_handle;
				ptr->mass = mass;
				ptr->temperature = temperature;
				ptr->surfaceArea = surface_area;
				ptr->thickness = thickness;
				ptr->groundTransferScale = ground_transfer_scale;
				ptr->elementIdx = elementIndex;
				Sim.SIM_HandleMessage(1445724082, sizeof(SimMessages.AddElementChunkMessage), (byte*)ptr);
			}
		}
	}

	// Token: 0x06005FC5 RID: 24517 RVA: 0x00234118 File Offset: 0x00232318
	public unsafe static void RemoveElementChunk(int sim_handle, int cb_handle)
	{
		if (!Sim.IsValidHandle(sim_handle))
		{
			Debug.Assert(false, "Invalid handle");
			return;
		}
		checked
		{
			SimMessages.RemoveElementChunkMessage* ptr = stackalloc SimMessages.RemoveElementChunkMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.RemoveElementChunkMessage)];
			ptr->callbackIdx = cb_handle;
			ptr->handle = sim_handle;
			Sim.SIM_HandleMessage(-912908555, sizeof(SimMessages.RemoveElementChunkMessage), (byte*)ptr);
		}
	}

	// Token: 0x06005FC6 RID: 24518 RVA: 0x00234168 File Offset: 0x00232368
	public unsafe static void SetElementChunkData(int sim_handle, float temperature, float heat_capacity)
	{
		if (!Sim.IsValidHandle(sim_handle))
		{
			return;
		}
		checked
		{
			SimMessages.SetElementChunkDataMessage* ptr = stackalloc SimMessages.SetElementChunkDataMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.SetElementChunkDataMessage)];
			ptr->handle = sim_handle;
			ptr->temperature = temperature;
			ptr->heatCapacity = heat_capacity;
			Sim.SIM_HandleMessage(-435115907, sizeof(SimMessages.SetElementChunkDataMessage), (byte*)ptr);
		}
	}

	// Token: 0x06005FC7 RID: 24519 RVA: 0x002341B4 File Offset: 0x002323B4
	public unsafe static void MoveElementChunk(int sim_handle, int cell)
	{
		if (!Sim.IsValidHandle(sim_handle))
		{
			Debug.Assert(false, "Invalid handle");
			return;
		}
		checked
		{
			SimMessages.MoveElementChunkMessage* ptr = stackalloc SimMessages.MoveElementChunkMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.MoveElementChunkMessage)];
			ptr->handle = sim_handle;
			ptr->gameCell = cell;
			Sim.SIM_HandleMessage(-374911358, sizeof(SimMessages.MoveElementChunkMessage), (byte*)ptr);
		}
	}

	// Token: 0x06005FC8 RID: 24520 RVA: 0x00234204 File Offset: 0x00232404
	public unsafe static void ModifyElementChunkEnergy(int sim_handle, float delta_kj)
	{
		if (!Sim.IsValidHandle(sim_handle))
		{
			Debug.Assert(false, "Invalid handle");
			return;
		}
		checked
		{
			SimMessages.ModifyElementChunkEnergyMessage* ptr = stackalloc SimMessages.ModifyElementChunkEnergyMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.ModifyElementChunkEnergyMessage)];
			ptr->handle = sim_handle;
			ptr->deltaKJ = delta_kj;
			Sim.SIM_HandleMessage(1020555667, sizeof(SimMessages.ModifyElementChunkEnergyMessage), (byte*)ptr);
		}
	}

	// Token: 0x06005FC9 RID: 24521 RVA: 0x00234254 File Offset: 0x00232454
	public unsafe static void ModifyElementChunkTemperatureAdjuster(int sim_handle, float temperature, float heat_capacity, float thermal_conductivity)
	{
		if (!Sim.IsValidHandle(sim_handle))
		{
			Debug.Assert(false, "Invalid handle");
			return;
		}
		checked
		{
			SimMessages.ModifyElementChunkAdjusterMessage* ptr = stackalloc SimMessages.ModifyElementChunkAdjusterMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.ModifyElementChunkAdjusterMessage)];
			ptr->handle = sim_handle;
			ptr->temperature = temperature;
			ptr->heatCapacity = heat_capacity;
			ptr->thermalConductivity = thermal_conductivity;
			Sim.SIM_HandleMessage(-1387601379, sizeof(SimMessages.ModifyElementChunkAdjusterMessage), (byte*)ptr);
		}
	}

	// Token: 0x06005FCA RID: 24522 RVA: 0x002342B0 File Offset: 0x002324B0
	public unsafe static void AddBuildingHeatExchange(Extents extents, float mass, float temperature, float thermal_conductivity, float operating_kw, ushort elem_idx, int callbackIdx = -1)
	{
		if (!Grid.IsValidCell(Grid.XYToCell(extents.x, extents.y)))
		{
			return;
		}
		int num = Grid.XYToCell(extents.x + extents.width, extents.y + extents.height);
		if (!Grid.IsValidCell(num))
		{
			Debug.LogErrorFormat("Invalid Cell [{0}] Extents [{1},{2}] [{3},{4}]", new object[] { num, extents.x, extents.y, extents.width, extents.height });
		}
		if (!Grid.IsValidCell(num))
		{
			return;
		}
		SimMessages.AddBuildingHeatExchangeMessage* ptr;
		checked
		{
			ptr = stackalloc SimMessages.AddBuildingHeatExchangeMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.AddBuildingHeatExchangeMessage)];
			ptr->callbackIdx = callbackIdx;
			ptr->elemIdx = elem_idx;
			ptr->mass = mass;
			ptr->temperature = temperature;
			ptr->thermalConductivity = thermal_conductivity;
			ptr->overheatTemperature = float.MaxValue;
			ptr->operatingKilowatts = operating_kw;
			ptr->minX = extents.x;
			ptr->minY = extents.y;
		}
		ptr->maxX = extents.x + extents.width;
		ptr->maxY = extents.y + extents.height;
		Sim.SIM_HandleMessage(1739021608, sizeof(SimMessages.AddBuildingHeatExchangeMessage), (byte*)ptr);
	}

	// Token: 0x06005FCB RID: 24523 RVA: 0x002343EC File Offset: 0x002325EC
	public unsafe static void ModifyBuildingHeatExchange(int sim_handle, Extents extents, float mass, float temperature, float thermal_conductivity, float overheat_temperature, float operating_kw, ushort element_idx)
	{
		int num = Grid.XYToCell(extents.x, extents.y);
		Debug.Assert(Grid.IsValidCell(num));
		if (!Grid.IsValidCell(num))
		{
			return;
		}
		int num2 = Grid.XYToCell(extents.x + extents.width, extents.y + extents.height);
		Debug.Assert(Grid.IsValidCell(num2));
		if (!Grid.IsValidCell(num2))
		{
			return;
		}
		SimMessages.ModifyBuildingHeatExchangeMessage* ptr;
		checked
		{
			ptr = stackalloc SimMessages.ModifyBuildingHeatExchangeMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.ModifyBuildingHeatExchangeMessage)];
			ptr->callbackIdx = sim_handle;
			ptr->elemIdx = element_idx;
			ptr->mass = mass;
			ptr->temperature = temperature;
			ptr->thermalConductivity = thermal_conductivity;
			ptr->overheatTemperature = overheat_temperature;
			ptr->operatingKilowatts = operating_kw;
			ptr->minX = extents.x;
			ptr->minY = extents.y;
		}
		ptr->maxX = extents.x + extents.width;
		ptr->maxY = extents.y + extents.height;
		Sim.SIM_HandleMessage(1818001569, sizeof(SimMessages.ModifyBuildingHeatExchangeMessage), (byte*)ptr);
	}

	// Token: 0x06005FCC RID: 24524 RVA: 0x002344E0 File Offset: 0x002326E0
	public unsafe static void RemoveBuildingHeatExchange(int sim_handle, int callbackIdx = -1)
	{
		checked
		{
			SimMessages.RemoveBuildingHeatExchangeMessage* ptr = stackalloc SimMessages.RemoveBuildingHeatExchangeMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.RemoveBuildingHeatExchangeMessage)];
			Debug.Assert(Sim.IsValidHandle(sim_handle));
			ptr->handle = sim_handle;
			ptr->callbackIdx = callbackIdx;
			Sim.SIM_HandleMessage(-456116629, sizeof(SimMessages.RemoveBuildingHeatExchangeMessage), (byte*)ptr);
		}
	}

	// Token: 0x06005FCD RID: 24525 RVA: 0x00234524 File Offset: 0x00232724
	public unsafe static void ModifyBuildingEnergy(int sim_handle, float delta_kj, float min_temperature, float max_temperature)
	{
		checked
		{
			SimMessages.ModifyBuildingEnergyMessage* ptr = stackalloc SimMessages.ModifyBuildingEnergyMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.ModifyBuildingEnergyMessage)];
			Debug.Assert(Sim.IsValidHandle(sim_handle));
			ptr->handle = sim_handle;
			ptr->deltaKJ = delta_kj;
			ptr->minTemperature = min_temperature;
			ptr->maxTemperature = max_temperature;
			Sim.SIM_HandleMessage(-1348791658, sizeof(SimMessages.ModifyBuildingEnergyMessage), (byte*)ptr);
		}
	}

	// Token: 0x06005FCE RID: 24526 RVA: 0x00234578 File Offset: 0x00232778
	public unsafe static void RegisterBuildingToBuildingHeatExchange(int structureTemperatureHandler, int callbackIdx = -1)
	{
		checked
		{
			SimMessages.RegisterBuildingToBuildingHeatExchangeMessage* ptr = stackalloc SimMessages.RegisterBuildingToBuildingHeatExchangeMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.RegisterBuildingToBuildingHeatExchangeMessage)];
			ptr->structureTemperatureHandler = structureTemperatureHandler;
			ptr->callbackIdx = callbackIdx;
			Sim.SIM_HandleMessage(-1338718217, sizeof(SimMessages.RegisterBuildingToBuildingHeatExchangeMessage), (byte*)ptr);
		}
	}

	// Token: 0x06005FCF RID: 24527 RVA: 0x002345B4 File Offset: 0x002327B4
	public unsafe static void AddBuildingToBuildingHeatExchange(int selfHandler, int buildingInContact, int cellsInContact)
	{
		checked
		{
			SimMessages.AddBuildingToBuildingHeatExchangeMessage* ptr = stackalloc SimMessages.AddBuildingToBuildingHeatExchangeMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.AddBuildingToBuildingHeatExchangeMessage)];
			ptr->selfHandler = selfHandler;
			ptr->buildingInContactHandle = buildingInContact;
			ptr->cellsInContact = cellsInContact;
			Sim.SIM_HandleMessage(-1586724321, sizeof(SimMessages.AddBuildingToBuildingHeatExchangeMessage), (byte*)ptr);
		}
	}

	// Token: 0x06005FD0 RID: 24528 RVA: 0x002345F4 File Offset: 0x002327F4
	public unsafe static void RemoveBuildingInContactFromBuildingToBuildingHeatExchange(int selfHandler, int buildingToRemove)
	{
		checked
		{
			SimMessages.RemoveBuildingInContactFromBuildingToBuildingHeatExchangeMessage* ptr = stackalloc SimMessages.RemoveBuildingInContactFromBuildingToBuildingHeatExchangeMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.RemoveBuildingInContactFromBuildingToBuildingHeatExchangeMessage)];
			ptr->selfHandler = selfHandler;
			ptr->buildingNoLongerInContactHandler = buildingToRemove;
			Sim.SIM_HandleMessage(-1993857213, sizeof(SimMessages.RemoveBuildingInContactFromBuildingToBuildingHeatExchangeMessage), (byte*)ptr);
		}
	}

	// Token: 0x06005FD1 RID: 24529 RVA: 0x00234630 File Offset: 0x00232830
	public unsafe static void RemoveBuildingToBuildingHeatExchange(int selfHandler, int callback = -1)
	{
		checked
		{
			SimMessages.RemoveBuildingToBuildingHeatExchangeMessage* ptr = stackalloc SimMessages.RemoveBuildingToBuildingHeatExchangeMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.RemoveBuildingToBuildingHeatExchangeMessage)];
			ptr->callbackIdx = callback;
			ptr->selfHandler = selfHandler;
			Sim.SIM_HandleMessage(697100730, sizeof(SimMessages.RemoveBuildingToBuildingHeatExchangeMessage), (byte*)ptr);
		}
	}

	// Token: 0x06005FD2 RID: 24530 RVA: 0x0023466C File Offset: 0x0023286C
	public unsafe static void AddDiseaseEmitter(int callbackIdx)
	{
		checked
		{
			SimMessages.AddDiseaseEmitterMessage* ptr = stackalloc SimMessages.AddDiseaseEmitterMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.AddDiseaseEmitterMessage)];
			ptr->callbackIdx = callbackIdx;
			Sim.SIM_HandleMessage(1486783027, sizeof(SimMessages.AddDiseaseEmitterMessage), (byte*)ptr);
		}
	}

	// Token: 0x06005FD3 RID: 24531 RVA: 0x002346A0 File Offset: 0x002328A0
	public unsafe static void ModifyDiseaseEmitter(int sim_handle, int cell, byte range, byte disease_idx, float emit_interval, int emit_count)
	{
		Debug.Assert(Sim.IsValidHandle(sim_handle));
		checked
		{
			SimMessages.ModifyDiseaseEmitterMessage* ptr = stackalloc SimMessages.ModifyDiseaseEmitterMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.ModifyDiseaseEmitterMessage)];
			ptr->handle = sim_handle;
			ptr->gameCell = cell;
			ptr->maxDepth = range;
			ptr->diseaseIdx = disease_idx;
			ptr->emitInterval = emit_interval;
			ptr->emitCount = emit_count;
			Sim.SIM_HandleMessage(-1899123924, sizeof(SimMessages.ModifyDiseaseEmitterMessage), (byte*)ptr);
		}
	}

	// Token: 0x06005FD4 RID: 24532 RVA: 0x00234704 File Offset: 0x00232904
	public unsafe static void RemoveDiseaseEmitter(int cb_handle, int sim_handle)
	{
		Debug.Assert(Sim.IsValidHandle(sim_handle));
		checked
		{
			SimMessages.RemoveDiseaseEmitterMessage* ptr = stackalloc SimMessages.RemoveDiseaseEmitterMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.RemoveDiseaseEmitterMessage)];
			ptr->handle = sim_handle;
			ptr->callbackIdx = cb_handle;
			Sim.SIM_HandleMessage(468135926, sizeof(SimMessages.RemoveDiseaseEmitterMessage), (byte*)ptr);
		}
	}

	// Token: 0x06005FD5 RID: 24533 RVA: 0x00234748 File Offset: 0x00232948
	public unsafe static void SetSavedOptionValue(SimMessages.SimSavedOptions option, int zero_or_one)
	{
		checked
		{
			SimMessages.SetSavedOptionsMessage* ptr = stackalloc SimMessages.SetSavedOptionsMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.SetSavedOptionsMessage)];
			if (zero_or_one == 0)
			{
				SimMessages.SetSavedOptionsMessage* ptr2 = ptr;
				ptr2->clearBits = ptr2->clearBits | (byte)option;
				ptr->setBits = 0;
			}
			else
			{
				ptr->clearBits = 0;
				SimMessages.SetSavedOptionsMessage* ptr3 = ptr;
				ptr3->setBits = ptr3->setBits | (byte)option;
			}
			Sim.SIM_HandleMessage(1154135737, sizeof(SimMessages.SetSavedOptionsMessage), (byte*)ptr);
		}
	}

	// Token: 0x06005FD6 RID: 24534 RVA: 0x002347A0 File Offset: 0x002329A0
	private static void WriteKleiString(this BinaryWriter writer, string str)
	{
		byte[] bytes = Encoding.UTF8.GetBytes(str);
		writer.Write(bytes.Length);
		if (bytes.Length != 0)
		{
			writer.Write(bytes);
		}
	}

	// Token: 0x06005FD7 RID: 24535 RVA: 0x002347D0 File Offset: 0x002329D0
	public unsafe static void CreateSimElementsTable(List<Element> elements)
	{
		MemoryStream memoryStream = new MemoryStream(Marshal.SizeOf(typeof(int)) + Marshal.SizeOf(typeof(Sim.Element)) * elements.Count);
		BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
		Debug.Assert(elements.Count < 65535, "SimDLL internals assume there are fewer than 65535 elements");
		binaryWriter.Write(elements.Count);
		for (int i = 0; i < elements.Count; i++)
		{
			Sim.Element element = new Sim.Element(elements[i], elements);
			element.Write(binaryWriter);
		}
		for (int j = 0; j < elements.Count; j++)
		{
			binaryWriter.WriteKleiString(UI.StripLinkFormatting(elements[j].name));
		}
		byte[] buffer = memoryStream.GetBuffer();
		byte[] array;
		byte* ptr;
		if ((array = buffer) == null || array.Length == 0)
		{
			ptr = null;
		}
		else
		{
			ptr = &array[0];
		}
		Sim.SIM_HandleMessage(1108437482, buffer.Length, ptr);
		array = null;
	}

	// Token: 0x06005FD8 RID: 24536 RVA: 0x002348C0 File Offset: 0x00232AC0
	public unsafe static void CreateDiseaseTable(Diseases diseases)
	{
		MemoryStream memoryStream = new MemoryStream(1024);
		BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
		binaryWriter.Write(diseases.Count);
		List<Element> elements = ElementLoader.elements;
		binaryWriter.Write(elements.Count);
		for (int i = 0; i < diseases.Count; i++)
		{
			Disease disease = diseases[i];
			binaryWriter.WriteKleiString(UI.StripLinkFormatting(disease.Name));
			binaryWriter.Write(disease.id.GetHashCode());
			binaryWriter.Write(disease.strength);
			disease.temperatureRange.Write(binaryWriter);
			disease.temperatureHalfLives.Write(binaryWriter);
			disease.pressureRange.Write(binaryWriter);
			disease.pressureHalfLives.Write(binaryWriter);
			binaryWriter.Write(disease.radiationKillRate);
			for (int j = 0; j < elements.Count; j++)
			{
				ElemGrowthInfo elemGrowthInfo = disease.elemGrowthInfo[j];
				elemGrowthInfo.Write(binaryWriter);
			}
		}
		byte[] array;
		byte* ptr;
		if ((array = memoryStream.GetBuffer()) == null || array.Length == 0)
		{
			ptr = null;
		}
		else
		{
			ptr = &array[0];
		}
		Sim.SIM_HandleMessage(825301935, (int)memoryStream.Length, ptr);
		array = null;
	}

	// Token: 0x06005FD9 RID: 24537 RVA: 0x002349FC File Offset: 0x00232BFC
	public unsafe static void DefineWorldOffsets(List<SimMessages.WorldOffsetData> worldOffsets)
	{
		MemoryStream memoryStream = new MemoryStream(1024);
		BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
		binaryWriter.Write(worldOffsets.Count);
		foreach (SimMessages.WorldOffsetData worldOffsetData in worldOffsets)
		{
			binaryWriter.Write(worldOffsetData.worldOffsetX);
			binaryWriter.Write(worldOffsetData.worldOffsetY);
			binaryWriter.Write(worldOffsetData.worldSizeX);
			binaryWriter.Write(worldOffsetData.worldSizeY);
		}
		byte[] array;
		byte* ptr;
		if ((array = memoryStream.GetBuffer()) == null || array.Length == 0)
		{
			ptr = null;
		}
		else
		{
			ptr = &array[0];
		}
		Sim.SIM_HandleMessage(-895846551, (int)memoryStream.Length, ptr);
		array = null;
	}

	// Token: 0x06005FDA RID: 24538 RVA: 0x00234ACC File Offset: 0x00232CCC
	public static void SimDataInitializeFromCells(int width, int height, uint simSeed, Sim.Cell[] cells, float[] bgTemp, Sim.DiseaseCell[] dc, bool headless)
	{
		MemoryStream memoryStream = new MemoryStream(Marshal.SizeOf(typeof(int)) + Marshal.SizeOf(typeof(int)) + Marshal.SizeOf(typeof(uint)) + Marshal.SizeOf(typeof(bool)) + Marshal.SizeOf(typeof(bool)) + Marshal.SizeOf(typeof(Sim.Cell)) * width * height + Marshal.SizeOf(typeof(float)) * width * height + Marshal.SizeOf(typeof(Sim.DiseaseCell)) * width * height);
		BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
		binaryWriter.Write(width);
		binaryWriter.Write(height);
		binaryWriter.Write(simSeed);
		bool flag = Sim.IsRadiationEnabled();
		binaryWriter.Write(flag);
		binaryWriter.Write(headless);
		int num = width * height;
		for (int i = 0; i < num; i++)
		{
			cells[i].Write(binaryWriter);
		}
		for (int j = 0; j < num; j++)
		{
			binaryWriter.Write(bgTemp[j]);
		}
		for (int k = 0; k < num; k++)
		{
			dc[k].Write(binaryWriter);
		}
		byte[] buffer = memoryStream.GetBuffer();
		Sim.HandleMessage(SimMessageHashes.SimData_InitializeFromCells, buffer.Length, buffer);
	}

	// Token: 0x06005FDB RID: 24539 RVA: 0x00234C10 File Offset: 0x00232E10
	public static void SimDataResizeGridAndInitializeVacuumCells(Vector2I grid_size, int width, int height, int x_offset, int y_offset)
	{
		MemoryStream memoryStream = new MemoryStream(Marshal.SizeOf(typeof(int)) + Marshal.SizeOf(typeof(int)));
		BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
		binaryWriter.Write(grid_size.x);
		binaryWriter.Write(grid_size.y);
		binaryWriter.Write(width);
		binaryWriter.Write(height);
		binaryWriter.Write(x_offset);
		binaryWriter.Write(y_offset);
		byte[] buffer = memoryStream.GetBuffer();
		Sim.HandleMessage(SimMessageHashes.SimData_ResizeAndInitializeVacuumCells, buffer.Length, buffer);
	}

	// Token: 0x06005FDC RID: 24540 RVA: 0x00234C90 File Offset: 0x00232E90
	public static void SimDataFreeCells(int width, int height, int x_offset, int y_offset)
	{
		MemoryStream memoryStream = new MemoryStream(Marshal.SizeOf(typeof(int)) + Marshal.SizeOf(typeof(int)));
		BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
		binaryWriter.Write(width);
		binaryWriter.Write(height);
		binaryWriter.Write(x_offset);
		binaryWriter.Write(y_offset);
		byte[] buffer = memoryStream.GetBuffer();
		Sim.HandleMessage(SimMessageHashes.SimData_FreeCells, buffer.Length, buffer);
	}

	// Token: 0x06005FDD RID: 24541 RVA: 0x00234CF8 File Offset: 0x00232EF8
	public unsafe static void Dig(int gameCell, int callbackIdx = -1, bool skipEvent = false)
	{
		if (!Grid.IsValidCell(gameCell))
		{
			return;
		}
		checked
		{
			SimMessages.DigMessage* ptr = stackalloc SimMessages.DigMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.DigMessage)];
			ptr->cellIdx = gameCell;
			ptr->callbackIdx = callbackIdx;
			ptr->skipEvent = skipEvent;
			Sim.SIM_HandleMessage(833038498, sizeof(SimMessages.DigMessage), (byte*)ptr);
		}
	}

	// Token: 0x06005FDE RID: 24542 RVA: 0x00234D44 File Offset: 0x00232F44
	public unsafe static void SetInsulation(int gameCell, float value)
	{
		if (!Grid.IsValidCell(gameCell))
		{
			return;
		}
		checked
		{
			SimMessages.SetCellFloatValueMessage* ptr = stackalloc SimMessages.SetCellFloatValueMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.SetCellFloatValueMessage)];
			ptr->cellIdx = gameCell;
			ptr->value = value;
			Sim.SIM_HandleMessage(-898773121, sizeof(SimMessages.SetCellFloatValueMessage), (byte*)ptr);
		}
	}

	// Token: 0x06005FDF RID: 24543 RVA: 0x00234D88 File Offset: 0x00232F88
	public unsafe static void SetStrength(int gameCell, int weight, float strengthMultiplier)
	{
		if (!Grid.IsValidCell(gameCell))
		{
			return;
		}
		SimMessages.SetCellFloatValueMessage* ptr;
		checked
		{
			ptr = stackalloc SimMessages.SetCellFloatValueMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.SetCellFloatValueMessage)];
			ptr->cellIdx = gameCell;
		}
		int num = (int)(strengthMultiplier * 4f) & 127;
		int num2 = ((weight & 1) << 7) | num;
		ptr->value = (float)((byte)num2);
		Sim.SIM_HandleMessage(1593243982, sizeof(SimMessages.SetCellFloatValueMessage), (byte*)ptr);
	}

	// Token: 0x06005FE0 RID: 24544 RVA: 0x00234DE0 File Offset: 0x00232FE0
	public unsafe static void SetCellProperties(int gameCell, byte properties)
	{
		if (!Grid.IsValidCell(gameCell))
		{
			return;
		}
		checked
		{
			SimMessages.CellPropertiesMessage* ptr = stackalloc SimMessages.CellPropertiesMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.CellPropertiesMessage)];
			ptr->cellIdx = gameCell;
			ptr->properties = properties;
			ptr->set = 1;
			Sim.SIM_HandleMessage(-469311643, sizeof(SimMessages.CellPropertiesMessage), (byte*)ptr);
		}
	}

	// Token: 0x06005FE1 RID: 24545 RVA: 0x00234E2C File Offset: 0x0023302C
	public unsafe static void ClearCellProperties(int gameCell, byte properties)
	{
		if (!Grid.IsValidCell(gameCell))
		{
			return;
		}
		checked
		{
			SimMessages.CellPropertiesMessage* ptr = stackalloc SimMessages.CellPropertiesMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.CellPropertiesMessage)];
			ptr->cellIdx = gameCell;
			ptr->properties = properties;
			ptr->set = 0;
			Sim.SIM_HandleMessage(-469311643, sizeof(SimMessages.CellPropertiesMessage), (byte*)ptr);
		}
	}

	// Token: 0x06005FE2 RID: 24546 RVA: 0x00234E78 File Offset: 0x00233078
	public unsafe static void ModifyCell(int gameCell, ushort elementIdx, float temperature, float mass, byte disease_idx, int disease_count, SimMessages.ReplaceType replace_type = SimMessages.ReplaceType.None, bool do_vertical_solid_displacement = false, int callbackIdx = -1)
	{
		if (!Grid.IsValidCell(gameCell))
		{
			return;
		}
		Element element = ElementLoader.elements[(int)elementIdx];
		if (element.maxMass == 0f && mass > element.maxMass)
		{
			Debug.LogWarningFormat("Invalid cell modification (mass greater than element maximum): Cell={0}, EIdx={1}, T={2}, M={3}, {4} max mass = {5}", new object[] { gameCell, elementIdx, temperature, mass, element.id, element.maxMass });
			mass = element.maxMass;
		}
		if (temperature < 0f || temperature > 10000f)
		{
			Debug.LogWarningFormat("Invalid cell modification (temp out of bounds): Cell={0}, EIdx={1}, T={2}, M={3}, {4} default temp = {5}", new object[]
			{
				gameCell,
				elementIdx,
				temperature,
				mass,
				element.id,
				element.defaultValues.temperature
			});
			temperature = element.defaultValues.temperature;
		}
		if (temperature == 0f && mass > 0f)
		{
			Debug.LogWarningFormat("Invalid cell modification (zero temp with non-zero mass): Cell={0}, EIdx={1}, T={2}, M={3}, {4} default temp = {5}", new object[]
			{
				gameCell,
				elementIdx,
				temperature,
				mass,
				element.id,
				element.defaultValues.temperature
			});
			temperature = element.defaultValues.temperature;
		}
		SimMessages.ModifyCellMessage* ptr;
		checked
		{
			ptr = stackalloc SimMessages.ModifyCellMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.ModifyCellMessage)];
			ptr->cellIdx = gameCell;
			ptr->callbackIdx = callbackIdx;
			ptr->temperature = temperature;
			ptr->mass = mass;
			ptr->elementIdx = elementIdx;
		}
		ptr->replaceType = (byte)replace_type;
		ptr->diseaseIdx = disease_idx;
		ptr->diseaseCount = disease_count;
		ptr->addSubType = (do_vertical_solid_displacement ? 0 : 1);
		Sim.SIM_HandleMessage(-1252920804, sizeof(SimMessages.ModifyCellMessage), (byte*)ptr);
	}

	// Token: 0x06005FE3 RID: 24547 RVA: 0x00235058 File Offset: 0x00233258
	public unsafe static void ModifyDiseaseOnCell(int gameCell, byte disease_idx, int disease_delta)
	{
		checked
		{
			SimMessages.CellDiseaseModification* ptr = stackalloc SimMessages.CellDiseaseModification[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.CellDiseaseModification)];
			ptr->cellIdx = gameCell;
			ptr->diseaseIdx = disease_idx;
			ptr->diseaseCount = disease_delta;
			Sim.SIM_HandleMessage(-1853671274, sizeof(SimMessages.CellDiseaseModification), (byte*)ptr);
		}
	}

	// Token: 0x06005FE4 RID: 24548 RVA: 0x00235098 File Offset: 0x00233298
	public unsafe static void ModifyRadiationOnCell(int gameCell, float radiationDelta, int callbackIdx = -1)
	{
		checked
		{
			SimMessages.CellRadiationModification* ptr = stackalloc SimMessages.CellRadiationModification[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.CellRadiationModification)];
			ptr->cellIdx = gameCell;
			ptr->radiationDelta = radiationDelta;
			ptr->callbackIdx = callbackIdx;
			Sim.SIM_HandleMessage(-1914877797, sizeof(SimMessages.CellRadiationModification), (byte*)ptr);
		}
	}

	// Token: 0x06005FE5 RID: 24549 RVA: 0x002350D8 File Offset: 0x002332D8
	public unsafe static void ModifyRadiationParams(RadiationParams type, float value)
	{
		checked
		{
			SimMessages.RadiationParamsModification* ptr = stackalloc SimMessages.RadiationParamsModification[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.RadiationParamsModification)];
			ptr->RadiationParamsType = (int)type;
			ptr->value = value;
			Sim.SIM_HandleMessage(377112707, sizeof(SimMessages.RadiationParamsModification), (byte*)ptr);
		}
	}

	// Token: 0x06005FE6 RID: 24550 RVA: 0x00235111 File Offset: 0x00233311
	public static ushort GetElementIndex(SimHashes element)
	{
		return ElementLoader.GetElementIndex(element);
	}

	// Token: 0x06005FE7 RID: 24551 RVA: 0x0023511C File Offset: 0x0023331C
	public unsafe static void ConsumeMass(int gameCell, SimHashes element, float mass, byte radius, int callbackIdx = -1)
	{
		if (!Grid.IsValidCell(gameCell))
		{
			return;
		}
		ushort elementIndex = ElementLoader.GetElementIndex(element);
		checked
		{
			SimMessages.MassConsumptionMessage* ptr = stackalloc SimMessages.MassConsumptionMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.MassConsumptionMessage)];
			ptr->cellIdx = gameCell;
			ptr->callbackIdx = callbackIdx;
			ptr->mass = mass;
			ptr->elementIdx = elementIndex;
			ptr->radius = radius;
			Sim.SIM_HandleMessage(1727657959, sizeof(SimMessages.MassConsumptionMessage), (byte*)ptr);
		}
	}

	// Token: 0x06005FE8 RID: 24552 RVA: 0x0023517C File Offset: 0x0023337C
	public unsafe static void EmitMass(int gameCell, ushort element_idx, float mass, float temperature, byte disease_idx, int disease_count, int callbackIdx = -1)
	{
		if (!Grid.IsValidCell(gameCell))
		{
			return;
		}
		checked
		{
			SimMessages.MassEmissionMessage* ptr = stackalloc SimMessages.MassEmissionMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.MassEmissionMessage)];
			ptr->cellIdx = gameCell;
			ptr->callbackIdx = callbackIdx;
			ptr->mass = mass;
			ptr->temperature = temperature;
			ptr->elementIdx = element_idx;
			ptr->diseaseIdx = disease_idx;
			ptr->diseaseCount = disease_count;
			Sim.SIM_HandleMessage(797274363, sizeof(SimMessages.MassEmissionMessage), (byte*)ptr);
		}
	}

	// Token: 0x06005FE9 RID: 24553 RVA: 0x002351E4 File Offset: 0x002333E4
	public unsafe static void ConsumeDisease(int game_cell, float percent_to_consume, int max_to_consume, int callback_idx)
	{
		if (!Grid.IsValidCell(game_cell))
		{
			return;
		}
		checked
		{
			SimMessages.ConsumeDiseaseMessage* ptr = stackalloc SimMessages.ConsumeDiseaseMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.ConsumeDiseaseMessage)];
			ptr->callbackIdx = callback_idx;
			ptr->gameCell = game_cell;
			ptr->percentToConsume = percent_to_consume;
			ptr->maxToConsume = max_to_consume;
			Sim.SIM_HandleMessage(-1019841536, sizeof(SimMessages.ConsumeDiseaseMessage), (byte*)ptr);
		}
	}

	// Token: 0x06005FEA RID: 24554 RVA: 0x00235234 File Offset: 0x00233434
	public static void AddRemoveSubstance(int gameCell, SimHashes new_element, CellAddRemoveSubstanceEvent ev, float mass, float temperature, byte disease_idx, int disease_count, bool do_vertical_solid_displacement = true, int callbackIdx = -1)
	{
		ushort elementIndex = SimMessages.GetElementIndex(new_element);
		SimMessages.AddRemoveSubstance(gameCell, elementIndex, ev, mass, temperature, disease_idx, disease_count, do_vertical_solid_displacement, callbackIdx);
	}

	// Token: 0x06005FEB RID: 24555 RVA: 0x0023525C File Offset: 0x0023345C
	public static void AddRemoveSubstance(int gameCell, ushort elementIdx, CellAddRemoveSubstanceEvent ev, float mass, float temperature, byte disease_idx, int disease_count, bool do_vertical_solid_displacement = true, int callbackIdx = -1)
	{
		if (elementIdx == 65535)
		{
			return;
		}
		Element element = ElementLoader.elements[(int)elementIdx];
		float num = ((temperature != -1f) ? temperature : element.defaultValues.temperature);
		SimMessages.ModifyCell(gameCell, elementIdx, num, mass, disease_idx, disease_count, SimMessages.ReplaceType.None, do_vertical_solid_displacement, callbackIdx);
	}

	// Token: 0x06005FEC RID: 24556 RVA: 0x002352AC File Offset: 0x002334AC
	public static void ReplaceElement(int gameCell, SimHashes new_element, CellElementEvent ev, float mass, float temperature = -1f, byte diseaseIdx = 255, int diseaseCount = 0, int callbackIdx = -1)
	{
		ushort elementIndex = SimMessages.GetElementIndex(new_element);
		if (elementIndex != 65535)
		{
			Element element = ElementLoader.elements[(int)elementIndex];
			float num = ((temperature != -1f) ? temperature : element.defaultValues.temperature);
			SimMessages.ModifyCell(gameCell, elementIndex, num, mass, diseaseIdx, diseaseCount, SimMessages.ReplaceType.Replace, false, callbackIdx);
		}
	}

	// Token: 0x06005FED RID: 24557 RVA: 0x00235300 File Offset: 0x00233500
	public static void ReplaceAndDisplaceElement(int gameCell, SimHashes new_element, CellElementEvent ev, float mass, float temperature = -1f, byte disease_idx = 255, int disease_count = 0, int callbackIdx = -1)
	{
		ushort elementIndex = SimMessages.GetElementIndex(new_element);
		if (elementIndex != 65535)
		{
			Element element = ElementLoader.elements[(int)elementIndex];
			float num = ((temperature != -1f) ? temperature : element.defaultValues.temperature);
			SimMessages.ModifyCell(gameCell, elementIndex, num, mass, disease_idx, disease_count, SimMessages.ReplaceType.ReplaceAndDisplace, false, callbackIdx);
		}
	}

	// Token: 0x06005FEE RID: 24558 RVA: 0x00235354 File Offset: 0x00233554
	public unsafe static void ModifyEnergy(int gameCell, float kilojoules, float max_temperature, SimMessages.EnergySourceID id)
	{
		if (!Grid.IsValidCell(gameCell))
		{
			return;
		}
		if (max_temperature <= 0f)
		{
			Debug.LogError("invalid max temperature for cell energy modification");
			return;
		}
		checked
		{
			SimMessages.ModifyCellEnergyMessage* ptr = stackalloc SimMessages.ModifyCellEnergyMessage[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.ModifyCellEnergyMessage)];
			ptr->cellIdx = gameCell;
			ptr->kilojoules = kilojoules;
			ptr->maxTemperature = max_temperature;
			ptr->id = (int)id;
			Sim.SIM_HandleMessage(818320644, sizeof(SimMessages.ModifyCellEnergyMessage), (byte*)ptr);
		}
	}

	// Token: 0x06005FEF RID: 24559 RVA: 0x002353B8 File Offset: 0x002335B8
	public static void ModifyMass(int gameCell, float mass, byte disease_idx, int disease_count, CellModifyMassEvent ev, float temperature = -1f, SimHashes element = SimHashes.Vacuum)
	{
		if (element != SimHashes.Vacuum)
		{
			ushort elementIndex = SimMessages.GetElementIndex(element);
			if (elementIndex != 65535)
			{
				if (temperature == -1f)
				{
					temperature = ElementLoader.elements[(int)elementIndex].defaultValues.temperature;
				}
				SimMessages.ModifyCell(gameCell, elementIndex, temperature, mass, disease_idx, disease_count, SimMessages.ReplaceType.None, false, -1);
				return;
			}
		}
		else
		{
			SimMessages.ModifyCell(gameCell, 0, temperature, mass, disease_idx, disease_count, SimMessages.ReplaceType.None, false, -1);
		}
	}

	// Token: 0x06005FF0 RID: 24560 RVA: 0x00235420 File Offset: 0x00233620
	public unsafe static void CreateElementInteractions(SimMessages.ElementInteraction[] interactions)
	{
		checked
		{
			fixed (SimMessages.ElementInteraction[] array = interactions)
			{
				SimMessages.ElementInteraction* ptr;
				if (interactions == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				SimMessages.CreateElementInteractionsMsg* ptr2 = stackalloc SimMessages.CreateElementInteractionsMsg[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.CreateElementInteractionsMsg)];
				ptr2->numInteractions = interactions.Length;
				ptr2->interactions = ptr;
				Sim.SIM_HandleMessage(-930289787, sizeof(SimMessages.CreateElementInteractionsMsg), (byte*)ptr2);
			}
		}
	}

	// Token: 0x06005FF1 RID: 24561 RVA: 0x00235478 File Offset: 0x00233678
	public unsafe static void NewGameFrame(float elapsed_seconds, List<Game.SimActiveRegion> activeRegions)
	{
		Debug.Assert(activeRegions.Count > 0, "NewGameFrame cannot be called with zero activeRegions");
		Sim.NewGameFrame* ptr;
		Sim.NewGameFrame* ptr2;
		checked
		{
			ptr = stackalloc Sim.NewGameFrame[unchecked((UIntPtr)activeRegions.Count) * (UIntPtr)sizeof(Sim.NewGameFrame)];
			ptr2 = ptr;
		}
		foreach (Game.SimActiveRegion simActiveRegion in activeRegions)
		{
			Pair<Vector2I, Vector2I> region = simActiveRegion.region;
			region.first = new Vector2I(MathUtil.Clamp(0, Grid.WidthInCells - 1, simActiveRegion.region.first.x), MathUtil.Clamp(0, Grid.HeightInCells - 1, simActiveRegion.region.first.y));
			region.second = new Vector2I(MathUtil.Clamp(0, Grid.WidthInCells, simActiveRegion.region.second.x), MathUtil.Clamp(0, Grid.HeightInCells - 1, simActiveRegion.region.second.y));
			ptr2->elapsedSeconds = elapsed_seconds;
			ptr2->minX = region.first.x;
			ptr2->minY = region.first.y;
			ptr2->maxX = region.second.x;
			ptr2->maxY = region.second.y;
			ptr2->currentSunlightIntensity = simActiveRegion.currentSunlightIntensity;
			ptr2->currentCosmicRadiationIntensity = simActiveRegion.currentCosmicRadiationIntensity;
			ptr2++;
		}
		Sim.SIM_HandleMessage(-775326397, sizeof(Sim.NewGameFrame) * activeRegions.Count, (byte*)ptr);
	}

	// Token: 0x06005FF2 RID: 24562 RVA: 0x00235614 File Offset: 0x00233814
	public unsafe static void SetDebugProperties(Sim.DebugProperties properties)
	{
		checked
		{
			Sim.DebugProperties* ptr = stackalloc Sim.DebugProperties[unchecked((UIntPtr)1) * (UIntPtr)sizeof(Sim.DebugProperties)];
			*ptr = properties;
			ptr->buildingTemperatureScale = properties.buildingTemperatureScale;
			ptr->buildingToBuildingTemperatureScale = properties.buildingToBuildingTemperatureScale;
			Sim.SIM_HandleMessage(-1683118492, sizeof(Sim.DebugProperties), (byte*)ptr);
		}
	}

	// Token: 0x06005FF3 RID: 24563 RVA: 0x00235660 File Offset: 0x00233860
	public unsafe static void ModifyCellWorldZone(int cell, byte zone_id)
	{
		checked
		{
			SimMessages.CellWorldZoneModification* ptr = stackalloc SimMessages.CellWorldZoneModification[unchecked((UIntPtr)1) * (UIntPtr)sizeof(SimMessages.CellWorldZoneModification)];
			ptr->cell = cell;
			ptr->zoneID = zone_id;
			Sim.SIM_HandleMessage(-449718014, sizeof(SimMessages.CellWorldZoneModification), (byte*)ptr);
		}
	}

	// Token: 0x04003FF5 RID: 16373
	public const int InvalidCallback = -1;

	// Token: 0x04003FF6 RID: 16374
	public const float STATE_TRANSITION_TEMPERATURE_BUFER = 3f;

	// Token: 0x02001DDD RID: 7645
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct AddElementConsumerMessage
	{
		// Token: 0x04008B87 RID: 35719
		public int cellIdx;

		// Token: 0x04008B88 RID: 35720
		public int callbackIdx;

		// Token: 0x04008B89 RID: 35721
		public byte radius;

		// Token: 0x04008B8A RID: 35722
		public byte configuration;

		// Token: 0x04008B8B RID: 35723
		public ushort elementIdx;
	}

	// Token: 0x02001DDE RID: 7646
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct SetElementConsumerDataMessage
	{
		// Token: 0x04008B8C RID: 35724
		public int handle;

		// Token: 0x04008B8D RID: 35725
		public int cell;

		// Token: 0x04008B8E RID: 35726
		public float consumptionRate;
	}

	// Token: 0x02001DDF RID: 7647
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct RemoveElementConsumerMessage
	{
		// Token: 0x04008B8F RID: 35727
		public int handle;

		// Token: 0x04008B90 RID: 35728
		public int callbackIdx;
	}

	// Token: 0x02001DE0 RID: 7648
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct AddElementEmitterMessage
	{
		// Token: 0x04008B91 RID: 35729
		public float maxPressure;

		// Token: 0x04008B92 RID: 35730
		public int callbackIdx;

		// Token: 0x04008B93 RID: 35731
		public int onBlockedCB;

		// Token: 0x04008B94 RID: 35732
		public int onUnblockedCB;
	}

	// Token: 0x02001DE1 RID: 7649
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct ModifyElementEmitterMessage
	{
		// Token: 0x04008B95 RID: 35733
		public int handle;

		// Token: 0x04008B96 RID: 35734
		public int cellIdx;

		// Token: 0x04008B97 RID: 35735
		public float emitInterval;

		// Token: 0x04008B98 RID: 35736
		public float emitMass;

		// Token: 0x04008B99 RID: 35737
		public float emitTemperature;

		// Token: 0x04008B9A RID: 35738
		public float maxPressure;

		// Token: 0x04008B9B RID: 35739
		public int diseaseCount;

		// Token: 0x04008B9C RID: 35740
		public ushort elementIdx;

		// Token: 0x04008B9D RID: 35741
		public byte maxDepth;

		// Token: 0x04008B9E RID: 35742
		public byte diseaseIdx;
	}

	// Token: 0x02001DE2 RID: 7650
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct RemoveElementEmitterMessage
	{
		// Token: 0x04008B9F RID: 35743
		public int handle;

		// Token: 0x04008BA0 RID: 35744
		public int callbackIdx;
	}

	// Token: 0x02001DE3 RID: 7651
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct AddRadiationEmitterMessage
	{
		// Token: 0x04008BA1 RID: 35745
		public int callbackIdx;

		// Token: 0x04008BA2 RID: 35746
		public int cell;

		// Token: 0x04008BA3 RID: 35747
		public short emitRadiusX;

		// Token: 0x04008BA4 RID: 35748
		public short emitRadiusY;

		// Token: 0x04008BA5 RID: 35749
		public float emitRads;

		// Token: 0x04008BA6 RID: 35750
		public float emitRate;

		// Token: 0x04008BA7 RID: 35751
		public float emitSpeed;

		// Token: 0x04008BA8 RID: 35752
		public float emitDirection;

		// Token: 0x04008BA9 RID: 35753
		public float emitAngle;

		// Token: 0x04008BAA RID: 35754
		public int emitType;
	}

	// Token: 0x02001DE4 RID: 7652
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct ModifyRadiationEmitterMessage
	{
		// Token: 0x04008BAB RID: 35755
		public int handle;

		// Token: 0x04008BAC RID: 35756
		public int cell;

		// Token: 0x04008BAD RID: 35757
		public int callbackIdx;

		// Token: 0x04008BAE RID: 35758
		public short emitRadiusX;

		// Token: 0x04008BAF RID: 35759
		public short emitRadiusY;

		// Token: 0x04008BB0 RID: 35760
		public float emitRads;

		// Token: 0x04008BB1 RID: 35761
		public float emitRate;

		// Token: 0x04008BB2 RID: 35762
		public float emitSpeed;

		// Token: 0x04008BB3 RID: 35763
		public float emitDirection;

		// Token: 0x04008BB4 RID: 35764
		public float emitAngle;

		// Token: 0x04008BB5 RID: 35765
		public int emitType;
	}

	// Token: 0x02001DE5 RID: 7653
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct RemoveRadiationEmitterMessage
	{
		// Token: 0x04008BB6 RID: 35766
		public int handle;

		// Token: 0x04008BB7 RID: 35767
		public int callbackIdx;
	}

	// Token: 0x02001DE6 RID: 7654
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct AddElementChunkMessage
	{
		// Token: 0x04008BB8 RID: 35768
		public int gameCell;

		// Token: 0x04008BB9 RID: 35769
		public int callbackIdx;

		// Token: 0x04008BBA RID: 35770
		public float mass;

		// Token: 0x04008BBB RID: 35771
		public float temperature;

		// Token: 0x04008BBC RID: 35772
		public float surfaceArea;

		// Token: 0x04008BBD RID: 35773
		public float thickness;

		// Token: 0x04008BBE RID: 35774
		public float groundTransferScale;

		// Token: 0x04008BBF RID: 35775
		public ushort elementIdx;

		// Token: 0x04008BC0 RID: 35776
		public byte pad0;

		// Token: 0x04008BC1 RID: 35777
		public byte pad1;
	}

	// Token: 0x02001DE7 RID: 7655
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct RemoveElementChunkMessage
	{
		// Token: 0x04008BC2 RID: 35778
		public int handle;

		// Token: 0x04008BC3 RID: 35779
		public int callbackIdx;
	}

	// Token: 0x02001DE8 RID: 7656
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct SetElementChunkDataMessage
	{
		// Token: 0x04008BC4 RID: 35780
		public int handle;

		// Token: 0x04008BC5 RID: 35781
		public float temperature;

		// Token: 0x04008BC6 RID: 35782
		public float heatCapacity;
	}

	// Token: 0x02001DE9 RID: 7657
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct MoveElementChunkMessage
	{
		// Token: 0x04008BC7 RID: 35783
		public int handle;

		// Token: 0x04008BC8 RID: 35784
		public int gameCell;
	}

	// Token: 0x02001DEA RID: 7658
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct ModifyElementChunkEnergyMessage
	{
		// Token: 0x04008BC9 RID: 35785
		public int handle;

		// Token: 0x04008BCA RID: 35786
		public float deltaKJ;
	}

	// Token: 0x02001DEB RID: 7659
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct ModifyElementChunkAdjusterMessage
	{
		// Token: 0x04008BCB RID: 35787
		public int handle;

		// Token: 0x04008BCC RID: 35788
		public float temperature;

		// Token: 0x04008BCD RID: 35789
		public float heatCapacity;

		// Token: 0x04008BCE RID: 35790
		public float thermalConductivity;
	}

	// Token: 0x02001DEC RID: 7660
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct AddBuildingHeatExchangeMessage
	{
		// Token: 0x04008BCF RID: 35791
		public int callbackIdx;

		// Token: 0x04008BD0 RID: 35792
		public ushort elemIdx;

		// Token: 0x04008BD1 RID: 35793
		public byte pad0;

		// Token: 0x04008BD2 RID: 35794
		public byte pad1;

		// Token: 0x04008BD3 RID: 35795
		public float mass;

		// Token: 0x04008BD4 RID: 35796
		public float temperature;

		// Token: 0x04008BD5 RID: 35797
		public float thermalConductivity;

		// Token: 0x04008BD6 RID: 35798
		public float overheatTemperature;

		// Token: 0x04008BD7 RID: 35799
		public float operatingKilowatts;

		// Token: 0x04008BD8 RID: 35800
		public int minX;

		// Token: 0x04008BD9 RID: 35801
		public int minY;

		// Token: 0x04008BDA RID: 35802
		public int maxX;

		// Token: 0x04008BDB RID: 35803
		public int maxY;
	}

	// Token: 0x02001DED RID: 7661
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct ModifyBuildingHeatExchangeMessage
	{
		// Token: 0x04008BDC RID: 35804
		public int callbackIdx;

		// Token: 0x04008BDD RID: 35805
		public ushort elemIdx;

		// Token: 0x04008BDE RID: 35806
		public byte pad0;

		// Token: 0x04008BDF RID: 35807
		public byte pad1;

		// Token: 0x04008BE0 RID: 35808
		public float mass;

		// Token: 0x04008BE1 RID: 35809
		public float temperature;

		// Token: 0x04008BE2 RID: 35810
		public float thermalConductivity;

		// Token: 0x04008BE3 RID: 35811
		public float overheatTemperature;

		// Token: 0x04008BE4 RID: 35812
		public float operatingKilowatts;

		// Token: 0x04008BE5 RID: 35813
		public int minX;

		// Token: 0x04008BE6 RID: 35814
		public int minY;

		// Token: 0x04008BE7 RID: 35815
		public int maxX;

		// Token: 0x04008BE8 RID: 35816
		public int maxY;
	}

	// Token: 0x02001DEE RID: 7662
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct ModifyBuildingEnergyMessage
	{
		// Token: 0x04008BE9 RID: 35817
		public int handle;

		// Token: 0x04008BEA RID: 35818
		public float deltaKJ;

		// Token: 0x04008BEB RID: 35819
		public float minTemperature;

		// Token: 0x04008BEC RID: 35820
		public float maxTemperature;
	}

	// Token: 0x02001DEF RID: 7663
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct RemoveBuildingHeatExchangeMessage
	{
		// Token: 0x04008BED RID: 35821
		public int handle;

		// Token: 0x04008BEE RID: 35822
		public int callbackIdx;
	}

	// Token: 0x02001DF0 RID: 7664
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct RegisterBuildingToBuildingHeatExchangeMessage
	{
		// Token: 0x04008BEF RID: 35823
		public int callbackIdx;

		// Token: 0x04008BF0 RID: 35824
		public int structureTemperatureHandler;
	}

	// Token: 0x02001DF1 RID: 7665
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct AddBuildingToBuildingHeatExchangeMessage
	{
		// Token: 0x04008BF1 RID: 35825
		public int selfHandler;

		// Token: 0x04008BF2 RID: 35826
		public int buildingInContactHandle;

		// Token: 0x04008BF3 RID: 35827
		public int cellsInContact;
	}

	// Token: 0x02001DF2 RID: 7666
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct RemoveBuildingInContactFromBuildingToBuildingHeatExchangeMessage
	{
		// Token: 0x04008BF4 RID: 35828
		public int selfHandler;

		// Token: 0x04008BF5 RID: 35829
		public int buildingNoLongerInContactHandler;
	}

	// Token: 0x02001DF3 RID: 7667
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct RemoveBuildingToBuildingHeatExchangeMessage
	{
		// Token: 0x04008BF6 RID: 35830
		public int callbackIdx;

		// Token: 0x04008BF7 RID: 35831
		public int selfHandler;
	}

	// Token: 0x02001DF4 RID: 7668
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct AddDiseaseEmitterMessage
	{
		// Token: 0x04008BF8 RID: 35832
		public int callbackIdx;
	}

	// Token: 0x02001DF5 RID: 7669
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct ModifyDiseaseEmitterMessage
	{
		// Token: 0x04008BF9 RID: 35833
		public int handle;

		// Token: 0x04008BFA RID: 35834
		public int gameCell;

		// Token: 0x04008BFB RID: 35835
		public byte diseaseIdx;

		// Token: 0x04008BFC RID: 35836
		public byte maxDepth;

		// Token: 0x04008BFD RID: 35837
		private byte pad0;

		// Token: 0x04008BFE RID: 35838
		private byte pad1;

		// Token: 0x04008BFF RID: 35839
		public float emitInterval;

		// Token: 0x04008C00 RID: 35840
		public int emitCount;
	}

	// Token: 0x02001DF6 RID: 7670
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct RemoveDiseaseEmitterMessage
	{
		// Token: 0x04008C01 RID: 35841
		public int handle;

		// Token: 0x04008C02 RID: 35842
		public int callbackIdx;
	}

	// Token: 0x02001DF7 RID: 7671
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct SetSavedOptionsMessage
	{
		// Token: 0x04008C03 RID: 35843
		public byte clearBits;

		// Token: 0x04008C04 RID: 35844
		public byte setBits;
	}

	// Token: 0x02001DF8 RID: 7672
	public enum SimSavedOptions : byte
	{
		// Token: 0x04008C06 RID: 35846
		ENABLE_DIAGONAL_FALLING_SAND = 1
	}

	// Token: 0x02001DF9 RID: 7673
	public struct WorldOffsetData
	{
		// Token: 0x04008C07 RID: 35847
		public int worldOffsetX;

		// Token: 0x04008C08 RID: 35848
		public int worldOffsetY;

		// Token: 0x04008C09 RID: 35849
		public int worldSizeX;

		// Token: 0x04008C0A RID: 35850
		public int worldSizeY;
	}

	// Token: 0x02001DFA RID: 7674
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct DigMessage
	{
		// Token: 0x04008C0B RID: 35851
		public int cellIdx;

		// Token: 0x04008C0C RID: 35852
		public int callbackIdx;

		// Token: 0x04008C0D RID: 35853
		public bool skipEvent;
	}

	// Token: 0x02001DFB RID: 7675
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct SetCellFloatValueMessage
	{
		// Token: 0x04008C0E RID: 35854
		public int cellIdx;

		// Token: 0x04008C0F RID: 35855
		public float value;
	}

	// Token: 0x02001DFC RID: 7676
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct CellPropertiesMessage
	{
		// Token: 0x04008C10 RID: 35856
		public int cellIdx;

		// Token: 0x04008C11 RID: 35857
		public byte properties;

		// Token: 0x04008C12 RID: 35858
		public byte set;

		// Token: 0x04008C13 RID: 35859
		public byte pad0;

		// Token: 0x04008C14 RID: 35860
		public byte pad1;
	}

	// Token: 0x02001DFD RID: 7677
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct SetInsulationValueMessage
	{
		// Token: 0x04008C15 RID: 35861
		public int cellIdx;

		// Token: 0x04008C16 RID: 35862
		public float value;
	}

	// Token: 0x02001DFE RID: 7678
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct ModifyCellMessage
	{
		// Token: 0x04008C17 RID: 35863
		public int cellIdx;

		// Token: 0x04008C18 RID: 35864
		public int callbackIdx;

		// Token: 0x04008C19 RID: 35865
		public float temperature;

		// Token: 0x04008C1A RID: 35866
		public float mass;

		// Token: 0x04008C1B RID: 35867
		public int diseaseCount;

		// Token: 0x04008C1C RID: 35868
		public ushort elementIdx;

		// Token: 0x04008C1D RID: 35869
		public byte replaceType;

		// Token: 0x04008C1E RID: 35870
		public byte diseaseIdx;

		// Token: 0x04008C1F RID: 35871
		public byte addSubType;
	}

	// Token: 0x02001DFF RID: 7679
	public enum ReplaceType
	{
		// Token: 0x04008C21 RID: 35873
		None,
		// Token: 0x04008C22 RID: 35874
		Replace,
		// Token: 0x04008C23 RID: 35875
		ReplaceAndDisplace
	}

	// Token: 0x02001E00 RID: 7680
	private enum AddSolidMassSubType
	{
		// Token: 0x04008C25 RID: 35877
		DoVerticalDisplacement,
		// Token: 0x04008C26 RID: 35878
		OnlyIfSameElement
	}

	// Token: 0x02001E01 RID: 7681
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct CellDiseaseModification
	{
		// Token: 0x04008C27 RID: 35879
		public int cellIdx;

		// Token: 0x04008C28 RID: 35880
		public byte diseaseIdx;

		// Token: 0x04008C29 RID: 35881
		public byte pad0;

		// Token: 0x04008C2A RID: 35882
		public byte pad1;

		// Token: 0x04008C2B RID: 35883
		public byte pad2;

		// Token: 0x04008C2C RID: 35884
		public int diseaseCount;
	}

	// Token: 0x02001E02 RID: 7682
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct RadiationParamsModification
	{
		// Token: 0x04008C2D RID: 35885
		public int RadiationParamsType;

		// Token: 0x04008C2E RID: 35886
		public float value;
	}

	// Token: 0x02001E03 RID: 7683
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct CellRadiationModification
	{
		// Token: 0x04008C2F RID: 35887
		public int cellIdx;

		// Token: 0x04008C30 RID: 35888
		public float radiationDelta;

		// Token: 0x04008C31 RID: 35889
		public int callbackIdx;
	}

	// Token: 0x02001E04 RID: 7684
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct MassConsumptionMessage
	{
		// Token: 0x04008C32 RID: 35890
		public int cellIdx;

		// Token: 0x04008C33 RID: 35891
		public int callbackIdx;

		// Token: 0x04008C34 RID: 35892
		public float mass;

		// Token: 0x04008C35 RID: 35893
		public ushort elementIdx;

		// Token: 0x04008C36 RID: 35894
		public byte radius;
	}

	// Token: 0x02001E05 RID: 7685
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct MassEmissionMessage
	{
		// Token: 0x04008C37 RID: 35895
		public int cellIdx;

		// Token: 0x04008C38 RID: 35896
		public int callbackIdx;

		// Token: 0x04008C39 RID: 35897
		public float mass;

		// Token: 0x04008C3A RID: 35898
		public float temperature;

		// Token: 0x04008C3B RID: 35899
		public int diseaseCount;

		// Token: 0x04008C3C RID: 35900
		public ushort elementIdx;

		// Token: 0x04008C3D RID: 35901
		public byte diseaseIdx;
	}

	// Token: 0x02001E06 RID: 7686
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct ConsumeDiseaseMessage
	{
		// Token: 0x04008C3E RID: 35902
		public int gameCell;

		// Token: 0x04008C3F RID: 35903
		public int callbackIdx;

		// Token: 0x04008C40 RID: 35904
		public float percentToConsume;

		// Token: 0x04008C41 RID: 35905
		public int maxToConsume;
	}

	// Token: 0x02001E07 RID: 7687
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct ModifyCellEnergyMessage
	{
		// Token: 0x04008C42 RID: 35906
		public int cellIdx;

		// Token: 0x04008C43 RID: 35907
		public float kilojoules;

		// Token: 0x04008C44 RID: 35908
		public float maxTemperature;

		// Token: 0x04008C45 RID: 35909
		public int id;
	}

	// Token: 0x02001E08 RID: 7688
	public enum EnergySourceID
	{
		// Token: 0x04008C47 RID: 35911
		DebugHeat = 1000,
		// Token: 0x04008C48 RID: 35912
		DebugCool,
		// Token: 0x04008C49 RID: 35913
		FierySkin,
		// Token: 0x04008C4A RID: 35914
		Overheatable,
		// Token: 0x04008C4B RID: 35915
		LiquidCooledFan,
		// Token: 0x04008C4C RID: 35916
		ConduitTemperatureManager,
		// Token: 0x04008C4D RID: 35917
		Excavator,
		// Token: 0x04008C4E RID: 35918
		HeatBulb,
		// Token: 0x04008C4F RID: 35919
		WarmBlooded,
		// Token: 0x04008C50 RID: 35920
		StructureTemperature,
		// Token: 0x04008C51 RID: 35921
		Burner,
		// Token: 0x04008C52 RID: 35922
		VacuumRadiator
	}

	// Token: 0x02001E09 RID: 7689
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct VisibleCells
	{
		// Token: 0x04008C53 RID: 35923
		public Vector2I min;

		// Token: 0x04008C54 RID: 35924
		public Vector2I max;
	}

	// Token: 0x02001E0A RID: 7690
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct WakeCellMessage
	{
		// Token: 0x04008C55 RID: 35925
		public int gameCell;
	}

	// Token: 0x02001E0B RID: 7691
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct ElementInteraction
	{
		// Token: 0x04008C56 RID: 35926
		public uint interactionType;

		// Token: 0x04008C57 RID: 35927
		public ushort elemIdx1;

		// Token: 0x04008C58 RID: 35928
		public ushort elemIdx2;

		// Token: 0x04008C59 RID: 35929
		public ushort elemResultIdx;

		// Token: 0x04008C5A RID: 35930
		public byte pad0;

		// Token: 0x04008C5B RID: 35931
		public byte pad1;

		// Token: 0x04008C5C RID: 35932
		public float minMass;

		// Token: 0x04008C5D RID: 35933
		public float interactionProbability;

		// Token: 0x04008C5E RID: 35934
		public float elem1MassDestructionPercent;

		// Token: 0x04008C5F RID: 35935
		public float elem2MassRequiredMultiplier;

		// Token: 0x04008C60 RID: 35936
		public float elemResultMassCreationMultiplier;
	}

	// Token: 0x02001E0C RID: 7692
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct CreateElementInteractionsMsg
	{
		// Token: 0x04008C61 RID: 35937
		public int numInteractions;

		// Token: 0x04008C62 RID: 35938
		public unsafe SimMessages.ElementInteraction* interactions;
	}

	// Token: 0x02001E0D RID: 7693
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct PipeChange
	{
		// Token: 0x04008C63 RID: 35939
		public int cell;

		// Token: 0x04008C64 RID: 35940
		public byte layer;

		// Token: 0x04008C65 RID: 35941
		public byte pad0;

		// Token: 0x04008C66 RID: 35942
		public byte pad1;

		// Token: 0x04008C67 RID: 35943
		public byte pad2;

		// Token: 0x04008C68 RID: 35944
		public float mass;

		// Token: 0x04008C69 RID: 35945
		public float temperature;

		// Token: 0x04008C6A RID: 35946
		public int elementHash;
	}

	// Token: 0x02001E0E RID: 7694
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct CellWorldZoneModification
	{
		// Token: 0x04008C6B RID: 35947
		public int cell;

		// Token: 0x04008C6C RID: 35948
		public byte zoneID;
	}
}
