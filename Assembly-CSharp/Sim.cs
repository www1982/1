using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

// Token: 0x02000C41 RID: 3137
public static class Sim
{
	// Token: 0x06005FA7 RID: 24487 RVA: 0x002339C0 File Offset: 0x00231BC0
	public static bool IsRadiationEnabled()
	{
		return DlcManager.FeatureRadiationEnabled();
	}

	// Token: 0x06005FA8 RID: 24488 RVA: 0x002339C7 File Offset: 0x00231BC7
	public static bool IsValidHandle(int h)
	{
		return h != -1 && h != -2;
	}

	// Token: 0x06005FA9 RID: 24489 RVA: 0x002339D7 File Offset: 0x00231BD7
	public static int GetHandleIndex(int h)
	{
		return h & 16777215;
	}

	// Token: 0x06005FAA RID: 24490
	[DllImport("SimDLL")]
	public static extern void SIM_Initialize(Sim.GAME_MessageHandler callback);

	// Token: 0x06005FAB RID: 24491
	[DllImport("SimDLL")]
	public static extern void SIM_Shutdown();

	// Token: 0x06005FAC RID: 24492
	[DllImport("SimDLL")]
	public unsafe static extern IntPtr SIM_HandleMessage(int sim_msg_id, int msg_length, byte* msg);

	// Token: 0x06005FAD RID: 24493
	[DllImport("SimDLL")]
	public unsafe static extern IntPtr SIM_HandleMessages(int sim_msg_id, int msg_length, int msg_count, byte* msg);

	// Token: 0x06005FAE RID: 24494
	[DllImport("SimDLL")]
	private unsafe static extern byte* SIM_BeginSave(int* size, int x, int y);

	// Token: 0x06005FAF RID: 24495
	[DllImport("SimDLL")]
	private static extern void SIM_EndSave();

	// Token: 0x06005FB0 RID: 24496
	[DllImport("SimDLL")]
	public static extern void SIM_DebugCrash();

	// Token: 0x06005FB1 RID: 24497 RVA: 0x002339E0 File Offset: 0x00231BE0
	public unsafe static IntPtr HandleMessage(SimMessageHashes sim_msg_id, int msg_length, byte[] msg)
	{
		IntPtr intPtr;
		fixed (byte[] array = msg)
		{
			byte* ptr;
			if (msg == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			intPtr = Sim.SIM_HandleMessage((int)sim_msg_id, msg_length, ptr);
		}
		return intPtr;
	}

	// Token: 0x06005FB2 RID: 24498 RVA: 0x00233A10 File Offset: 0x00231C10
	public unsafe static void Save(BinaryWriter writer, int x, int y)
	{
		int num;
		void* ptr = (void*)Sim.SIM_BeginSave(&num, x, y);
		byte[] array = new byte[num];
		Marshal.Copy((IntPtr)ptr, array, 0, num);
		Sim.SIM_EndSave();
		writer.Write(num);
		writer.Write(array);
	}

	// Token: 0x06005FB3 RID: 24499 RVA: 0x00233A50 File Offset: 0x00231C50
	public unsafe static int LoadWorld(IReader reader)
	{
		int num = reader.ReadInt32();
		byte[] array;
		byte* ptr;
		if ((array = reader.ReadBytes(num)) == null || array.Length == 0)
		{
			ptr = null;
		}
		else
		{
			ptr = &array[0];
		}
		IntPtr intPtr = Sim.SIM_HandleMessage(-672538170, num, ptr);
		array = null;
		if (intPtr == IntPtr.Zero)
		{
			return -1;
		}
		return 0;
	}

	// Token: 0x06005FB4 RID: 24500 RVA: 0x00233AA0 File Offset: 0x00231CA0
	public static void AllocateCells(int width, int height, bool headless = false)
	{
		using (MemoryStream memoryStream = new MemoryStream(16))
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
			{
				binaryWriter.Write(width);
				binaryWriter.Write(height);
				bool flag = Sim.IsRadiationEnabled();
				binaryWriter.Write(flag);
				binaryWriter.Write(headless);
				binaryWriter.Flush();
				Sim.HandleMessage(SimMessageHashes.AllocateCells, (int)memoryStream.Length, memoryStream.GetBuffer());
			}
		}
	}

	// Token: 0x06005FB5 RID: 24501 RVA: 0x00233B30 File Offset: 0x00231D30
	public unsafe static int Load(IReader reader)
	{
		int num = reader.ReadInt32();
		byte[] array;
		byte* ptr;
		if ((array = reader.ReadBytes(num)) == null || array.Length == 0)
		{
			ptr = null;
		}
		else
		{
			ptr = &array[0];
		}
		IntPtr intPtr = Sim.SIM_HandleMessage(-672538170, num, ptr);
		array = null;
		if (intPtr == IntPtr.Zero)
		{
			return -1;
		}
		return 0;
	}

	// Token: 0x06005FB6 RID: 24502 RVA: 0x00233B80 File Offset: 0x00231D80
	public unsafe static void Start()
	{
		Sim.GameDataUpdate* ptr = (Sim.GameDataUpdate*)(void*)Sim.SIM_HandleMessage(-931446686, 0, null);
		Grid.elementIdx = ptr->elementIdx;
		Grid.temperature = ptr->temperature;
		Grid.radiation = ptr->radiation;
		Grid.mass = ptr->mass;
		Grid.properties = ptr->properties;
		Grid.strengthInfo = ptr->strengthInfo;
		Grid.insulation = ptr->insulation;
		Grid.diseaseIdx = ptr->diseaseIdx;
		Grid.diseaseCount = ptr->diseaseCount;
		Grid.AccumulatedFlowValues = ptr->accumulatedFlow;
		PropertyTextures.externalFlowTex = ptr->propertyTextureFlow;
		PropertyTextures.externalLiquidTex = ptr->propertyTextureLiquid;
		PropertyTextures.externalLiquidDataTex = ptr->propertyTextureLiquidData;
		PropertyTextures.externalExposedToSunlight = ptr->propertyTextureExposedToSunlight;
		Grid.InitializeCells();
	}

	// Token: 0x06005FB7 RID: 24503 RVA: 0x00233C3F File Offset: 0x00231E3F
	public static void Shutdown()
	{
		Sim.SIM_Shutdown();
		Grid.mass = null;
	}

	// Token: 0x06005FB8 RID: 24504
	[DllImport("SimDLL")]
	public unsafe static extern char* SYSINFO_Acquire();

	// Token: 0x06005FB9 RID: 24505
	[DllImport("SimDLL")]
	public static extern void SYSINFO_Release();

	// Token: 0x06005FBA RID: 24506 RVA: 0x00233C50 File Offset: 0x00231E50
	public unsafe static int DLL_MessageHandler(int message_id, IntPtr data)
	{
		if (message_id == 0)
		{
			Sim.DLLExceptionHandlerMessage* ptr = (Sim.DLLExceptionHandlerMessage*)(void*)data;
			string text = Marshal.PtrToStringAnsi(ptr->callstack);
			string text2 = Marshal.PtrToStringAnsi(ptr->dmpFilename);
			KCrashReporter.ReportSimDLLCrash("SimDLL Crash Dump", text, text2);
			return 0;
		}
		if (message_id == 1)
		{
			Sim.DLLReportMessageMessage* ptr2 = (Sim.DLLReportMessageMessage*)(void*)data;
			string text3 = "SimMessage: " + Marshal.PtrToStringAnsi(ptr2->message);
			string text4;
			if (ptr2->callstack != IntPtr.Zero)
			{
				text4 = Marshal.PtrToStringAnsi(ptr2->callstack);
			}
			else
			{
				string text5 = Marshal.PtrToStringAnsi(ptr2->file);
				int line = ptr2->line;
				text4 = text5 + ":" + line.ToString();
			}
			KCrashReporter.ReportSimDLLCrash(text3, text4, null);
			return 0;
		}
		return -1;
	}

	// Token: 0x04003FDC RID: 16348
	public const int InvalidHandle = -1;

	// Token: 0x04003FDD RID: 16349
	public const int QueuedRegisterHandle = -2;

	// Token: 0x04003FDE RID: 16350
	public const byte InvalidDiseaseIdx = 255;

	// Token: 0x04003FDF RID: 16351
	public const ushort InvalidElementIdx = 65535;

	// Token: 0x04003FE0 RID: 16352
	public const byte SpaceZoneID = 255;

	// Token: 0x04003FE1 RID: 16353
	public const byte SolidZoneID = 0;

	// Token: 0x04003FE2 RID: 16354
	public const int ChunkEdgeSize = 32;

	// Token: 0x04003FE3 RID: 16355
	public const float StateTransitionEnergy = 3f;

	// Token: 0x04003FE4 RID: 16356
	public const float ZeroDegreesCentigrade = 273.15f;

	// Token: 0x04003FE5 RID: 16357
	public const float StandardTemperature = 293.15f;

	// Token: 0x04003FE6 RID: 16358
	public const float StandardMeltingPointOffset = 10f;

	// Token: 0x04003FE7 RID: 16359
	public const float StandardPressure = 101.3f;

	// Token: 0x04003FE8 RID: 16360
	public const float Epsilon = 0.0001f;

	// Token: 0x04003FE9 RID: 16361
	public const float MaxTemperature = 10000f;

	// Token: 0x04003FEA RID: 16362
	public const float MinTemperature = 0f;

	// Token: 0x04003FEB RID: 16363
	public const float MaxRadiation = 9000000f;

	// Token: 0x04003FEC RID: 16364
	public const float MinRadiation = 0f;

	// Token: 0x04003FED RID: 16365
	public const float MaxMass = 10000f;

	// Token: 0x04003FEE RID: 16366
	public const float MinMass = 1.0001f;

	// Token: 0x04003FEF RID: 16367
	private const int PressureUpdateInterval = 1;

	// Token: 0x04003FF0 RID: 16368
	private const int TemperatureUpdateInterval = 1;

	// Token: 0x04003FF1 RID: 16369
	private const int LiquidUpdateInterval = 1;

	// Token: 0x04003FF2 RID: 16370
	private const int LifeUpdateInterval = 1;

	// Token: 0x04003FF3 RID: 16371
	public const byte ClearSkyGridValue = 253;

	// Token: 0x04003FF4 RID: 16372
	public const int PACKING_ALIGNMENT = 4;

	// Token: 0x02001DB7 RID: 7607
	// (Invoke) Token: 0x0600AF7C RID: 44924
	public delegate int GAME_MessageHandler(int message_id, IntPtr data);

	// Token: 0x02001DB8 RID: 7608
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct DLLExceptionHandlerMessage
	{
		// Token: 0x04008A91 RID: 35473
		public IntPtr callstack;

		// Token: 0x04008A92 RID: 35474
		public IntPtr dmpFilename;
	}

	// Token: 0x02001DB9 RID: 7609
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct DLLReportMessageMessage
	{
		// Token: 0x04008A93 RID: 35475
		public IntPtr callstack;

		// Token: 0x04008A94 RID: 35476
		public IntPtr message;

		// Token: 0x04008A95 RID: 35477
		public IntPtr file;

		// Token: 0x04008A96 RID: 35478
		public int line;
	}

	// Token: 0x02001DBA RID: 7610
	private enum GameHandledMessages
	{
		// Token: 0x04008A98 RID: 35480
		ExceptionHandler,
		// Token: 0x04008A99 RID: 35481
		ReportMessage
	}

	// Token: 0x02001DBB RID: 7611
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct PhysicsData
	{
		// Token: 0x0600AF7F RID: 44927 RVA: 0x003CFB64 File Offset: 0x003CDD64
		public void Write(BinaryWriter writer)
		{
			writer.Write(this.temperature);
			writer.Write(this.mass);
			writer.Write(this.pressure);
		}

		// Token: 0x04008A9A RID: 35482
		public float temperature;

		// Token: 0x04008A9B RID: 35483
		public float mass;

		// Token: 0x04008A9C RID: 35484
		public float pressure;
	}

	// Token: 0x02001DBC RID: 7612
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct Cell
	{
		// Token: 0x0600AF80 RID: 44928 RVA: 0x003CFB90 File Offset: 0x003CDD90
		public void Write(BinaryWriter writer)
		{
			writer.Write(this.elementIdx);
			writer.Write(0);
			writer.Write(this.insulation);
			writer.Write(0);
			writer.Write(this.pad0);
			writer.Write(this.pad1);
			writer.Write(this.pad2);
			writer.Write(this.temperature);
			writer.Write(this.mass);
		}

		// Token: 0x0600AF81 RID: 44929 RVA: 0x003CFC01 File Offset: 0x003CDE01
		public void SetValues(global::Element elem, List<global::Element> elements)
		{
			this.SetValues(elem, elem.defaultValues, elements);
		}

		// Token: 0x0600AF82 RID: 44930 RVA: 0x003CFC14 File Offset: 0x003CDE14
		public void SetValues(global::Element elem, Sim.PhysicsData pd, List<global::Element> elements)
		{
			this.elementIdx = (ushort)elements.IndexOf(elem);
			this.temperature = pd.temperature;
			this.mass = pd.mass;
			this.insulation = byte.MaxValue;
			DebugUtil.Assert(this.temperature > 0f || this.mass == 0f, "A non-zero mass cannot have a <= 0 temperature");
		}

		// Token: 0x0600AF83 RID: 44931 RVA: 0x003CFC7C File Offset: 0x003CDE7C
		public void SetValues(ushort new_elem_idx, float new_temperature, float new_mass)
		{
			this.elementIdx = new_elem_idx;
			this.temperature = new_temperature;
			this.mass = new_mass;
			this.insulation = byte.MaxValue;
			DebugUtil.Assert(this.temperature > 0f || this.mass == 0f, "A non-zero mass cannot have a <= 0 temperature");
		}

		// Token: 0x04008A9D RID: 35485
		public ushort elementIdx;

		// Token: 0x04008A9E RID: 35486
		public byte properties;

		// Token: 0x04008A9F RID: 35487
		public byte insulation;

		// Token: 0x04008AA0 RID: 35488
		public byte strengthInfo;

		// Token: 0x04008AA1 RID: 35489
		public byte pad0;

		// Token: 0x04008AA2 RID: 35490
		public byte pad1;

		// Token: 0x04008AA3 RID: 35491
		public byte pad2;

		// Token: 0x04008AA4 RID: 35492
		public float temperature;

		// Token: 0x04008AA5 RID: 35493
		public float mass;

		// Token: 0x020028F4 RID: 10484
		public enum Properties
		{
			// Token: 0x0400B55F RID: 46431
			GasImpermeable = 1,
			// Token: 0x0400B560 RID: 46432
			LiquidImpermeable,
			// Token: 0x0400B561 RID: 46433
			SolidImpermeable = 4,
			// Token: 0x0400B562 RID: 46434
			Unbreakable = 8,
			// Token: 0x0400B563 RID: 46435
			Transparent = 16,
			// Token: 0x0400B564 RID: 46436
			Opaque = 32,
			// Token: 0x0400B565 RID: 46437
			NotifyOnMelt = 64,
			// Token: 0x0400B566 RID: 46438
			ConstructedTile = 128
		}
	}

	// Token: 0x02001DBD RID: 7613
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct Element
	{
		// Token: 0x0600AF84 RID: 44932 RVA: 0x003CFCD0 File Offset: 0x003CDED0
		public Element(global::Element e, List<global::Element> elements)
		{
			this.id = e.id;
			this.state = (byte)e.state;
			if (e.HasTag(GameTags.Unstable))
			{
				this.state |= 8;
			}
			int num = elements.FindIndex((global::Element ele) => ele.id == e.lowTempTransitionTarget);
			int num2 = elements.FindIndex((global::Element ele) => ele.id == e.highTempTransitionTarget);
			this.lowTempTransitionIdx = (ushort)((num >= 0) ? num : 65535);
			this.highTempTransitionIdx = (ushort)((num2 >= 0) ? num2 : 65535);
			this.elementsTableIdx = (ushort)elements.IndexOf(e);
			this.specificHeatCapacity = e.specificHeatCapacity;
			this.thermalConductivity = e.thermalConductivity;
			this.solidSurfaceAreaMultiplier = e.solidSurfaceAreaMultiplier;
			this.liquidSurfaceAreaMultiplier = e.liquidSurfaceAreaMultiplier;
			this.gasSurfaceAreaMultiplier = e.gasSurfaceAreaMultiplier;
			this.molarMass = e.molarMass;
			this.strength = e.strength;
			this.flow = e.flow;
			this.viscosity = e.viscosity;
			this.minHorizontalFlow = e.minHorizontalFlow;
			this.minVerticalFlow = e.minVerticalFlow;
			this.maxMass = e.maxMass;
			this.lowTemp = e.lowTemp;
			this.highTemp = e.highTemp;
			this.highTempTransitionOreID = e.highTempTransitionOreID;
			this.highTempTransitionOreMassConversion = e.highTempTransitionOreMassConversion;
			this.lowTempTransitionOreID = e.lowTempTransitionOreID;
			this.lowTempTransitionOreMassConversion = e.lowTempTransitionOreMassConversion;
			this.sublimateIndex = (ushort)elements.FindIndex((global::Element ele) => ele.id == e.sublimateId);
			this.convertIndex = (ushort)elements.FindIndex((global::Element ele) => ele.id == e.convertId);
			this.pack0 = 0;
			if (e.substance == null)
			{
				this.colour = 0U;
			}
			else
			{
				Color32 color = e.substance.colour;
				this.colour = (uint)(((int)color.a << 24) | ((int)color.b << 16) | ((int)color.g << 8) | (int)color.r);
			}
			this.sublimateFX = e.sublimateFX;
			this.sublimateRate = e.sublimateRate;
			this.sublimateEfficiency = e.sublimateEfficiency;
			this.sublimateProbability = e.sublimateProbability;
			this.offGasProbability = e.offGasPercentage;
			this.lightAbsorptionFactor = e.lightAbsorptionFactor;
			this.radiationAbsorptionFactor = e.radiationAbsorptionFactor;
			this.radiationPer1000Mass = e.radiationPer1000Mass;
			this.defaultValues = e.defaultValues;
		}

		// Token: 0x0600AF85 RID: 44933 RVA: 0x003CFFE0 File Offset: 0x003CE1E0
		public void Write(BinaryWriter writer)
		{
			writer.Write((int)this.id);
			writer.Write(this.lowTempTransitionIdx);
			writer.Write(this.highTempTransitionIdx);
			writer.Write(this.elementsTableIdx);
			writer.Write(this.state);
			writer.Write(this.pack0);
			writer.Write(this.specificHeatCapacity);
			writer.Write(this.thermalConductivity);
			writer.Write(this.molarMass);
			writer.Write(this.solidSurfaceAreaMultiplier);
			writer.Write(this.liquidSurfaceAreaMultiplier);
			writer.Write(this.gasSurfaceAreaMultiplier);
			writer.Write(this.flow);
			writer.Write(this.viscosity);
			writer.Write(this.minHorizontalFlow);
			writer.Write(this.minVerticalFlow);
			writer.Write(this.maxMass);
			writer.Write(this.lowTemp);
			writer.Write(this.highTemp);
			writer.Write(this.strength);
			writer.Write((int)this.lowTempTransitionOreID);
			writer.Write(this.lowTempTransitionOreMassConversion);
			writer.Write((int)this.highTempTransitionOreID);
			writer.Write(this.highTempTransitionOreMassConversion);
			writer.Write(this.sublimateIndex);
			writer.Write(this.convertIndex);
			writer.Write(this.colour);
			writer.Write((int)this.sublimateFX);
			writer.Write(this.sublimateRate);
			writer.Write(this.sublimateEfficiency);
			writer.Write(this.sublimateProbability);
			writer.Write(this.offGasProbability);
			writer.Write(this.lightAbsorptionFactor);
			writer.Write(this.radiationAbsorptionFactor);
			writer.Write(this.radiationPer1000Mass);
			this.defaultValues.Write(writer);
		}

		// Token: 0x04008AA6 RID: 35494
		public SimHashes id;

		// Token: 0x04008AA7 RID: 35495
		public ushort lowTempTransitionIdx;

		// Token: 0x04008AA8 RID: 35496
		public ushort highTempTransitionIdx;

		// Token: 0x04008AA9 RID: 35497
		public ushort elementsTableIdx;

		// Token: 0x04008AAA RID: 35498
		public byte state;

		// Token: 0x04008AAB RID: 35499
		public byte pack0;

		// Token: 0x04008AAC RID: 35500
		public float specificHeatCapacity;

		// Token: 0x04008AAD RID: 35501
		public float thermalConductivity;

		// Token: 0x04008AAE RID: 35502
		public float molarMass;

		// Token: 0x04008AAF RID: 35503
		public float solidSurfaceAreaMultiplier;

		// Token: 0x04008AB0 RID: 35504
		public float liquidSurfaceAreaMultiplier;

		// Token: 0x04008AB1 RID: 35505
		public float gasSurfaceAreaMultiplier;

		// Token: 0x04008AB2 RID: 35506
		public float flow;

		// Token: 0x04008AB3 RID: 35507
		public float viscosity;

		// Token: 0x04008AB4 RID: 35508
		public float minHorizontalFlow;

		// Token: 0x04008AB5 RID: 35509
		public float minVerticalFlow;

		// Token: 0x04008AB6 RID: 35510
		public float maxMass;

		// Token: 0x04008AB7 RID: 35511
		public float lowTemp;

		// Token: 0x04008AB8 RID: 35512
		public float highTemp;

		// Token: 0x04008AB9 RID: 35513
		public float strength;

		// Token: 0x04008ABA RID: 35514
		public SimHashes lowTempTransitionOreID;

		// Token: 0x04008ABB RID: 35515
		public float lowTempTransitionOreMassConversion;

		// Token: 0x04008ABC RID: 35516
		public SimHashes highTempTransitionOreID;

		// Token: 0x04008ABD RID: 35517
		public float highTempTransitionOreMassConversion;

		// Token: 0x04008ABE RID: 35518
		public ushort sublimateIndex;

		// Token: 0x04008ABF RID: 35519
		public ushort convertIndex;

		// Token: 0x04008AC0 RID: 35520
		public uint colour;

		// Token: 0x04008AC1 RID: 35521
		public SpawnFXHashes sublimateFX;

		// Token: 0x04008AC2 RID: 35522
		public float sublimateRate;

		// Token: 0x04008AC3 RID: 35523
		public float sublimateEfficiency;

		// Token: 0x04008AC4 RID: 35524
		public float sublimateProbability;

		// Token: 0x04008AC5 RID: 35525
		public float offGasProbability;

		// Token: 0x04008AC6 RID: 35526
		public float lightAbsorptionFactor;

		// Token: 0x04008AC7 RID: 35527
		public float radiationAbsorptionFactor;

		// Token: 0x04008AC8 RID: 35528
		public float radiationPer1000Mass;

		// Token: 0x04008AC9 RID: 35529
		public Sim.PhysicsData defaultValues;
	}

	// Token: 0x02001DBE RID: 7614
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct DiseaseCell
	{
		// Token: 0x0600AF86 RID: 44934 RVA: 0x003D01B4 File Offset: 0x003CE3B4
		public void Write(BinaryWriter writer)
		{
			writer.Write(this.diseaseIdx);
			writer.Write(this.reservedInfestationTickCount);
			writer.Write(this.pad1);
			writer.Write(this.pad2);
			writer.Write(this.elementCount);
			writer.Write(this.reservedAccumulatedError);
		}

		// Token: 0x04008ACA RID: 35530
		public byte diseaseIdx;

		// Token: 0x04008ACB RID: 35531
		private byte reservedInfestationTickCount;

		// Token: 0x04008ACC RID: 35532
		private byte pad1;

		// Token: 0x04008ACD RID: 35533
		private byte pad2;

		// Token: 0x04008ACE RID: 35534
		public int elementCount;

		// Token: 0x04008ACF RID: 35535
		private float reservedAccumulatedError;

		// Token: 0x04008AD0 RID: 35536
		public static readonly Sim.DiseaseCell Invalid = new Sim.DiseaseCell
		{
			diseaseIdx = byte.MaxValue,
			elementCount = 0
		};
	}

	// Token: 0x02001DBF RID: 7615
	// (Invoke) Token: 0x0600AF89 RID: 44937
	public delegate void GAME_Callback();

	// Token: 0x02001DC0 RID: 7616
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct SolidInfo
	{
		// Token: 0x04008AD1 RID: 35537
		public int cellIdx;

		// Token: 0x04008AD2 RID: 35538
		public int isSolid;
	}

	// Token: 0x02001DC1 RID: 7617
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct LiquidChangeInfo
	{
		// Token: 0x04008AD3 RID: 35539
		public int cellIdx;
	}

	// Token: 0x02001DC2 RID: 7618
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct SolidSubstanceChangeInfo
	{
		// Token: 0x04008AD4 RID: 35540
		public int cellIdx;
	}

	// Token: 0x02001DC3 RID: 7619
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct SubstanceChangeInfo
	{
		// Token: 0x04008AD5 RID: 35541
		public int cellIdx;

		// Token: 0x04008AD6 RID: 35542
		public ushort oldElemIdx;

		// Token: 0x04008AD7 RID: 35543
		public ushort newElemIdx;
	}

	// Token: 0x02001DC4 RID: 7620
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct CallbackInfo
	{
		// Token: 0x04008AD8 RID: 35544
		public int callbackIdx;
	}

	// Token: 0x02001DC5 RID: 7621
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct GameDataUpdate
	{
		// Token: 0x04008AD9 RID: 35545
		public int numFramesProcessed;

		// Token: 0x04008ADA RID: 35546
		public unsafe ushort* elementIdx;

		// Token: 0x04008ADB RID: 35547
		public unsafe float* temperature;

		// Token: 0x04008ADC RID: 35548
		public unsafe float* mass;

		// Token: 0x04008ADD RID: 35549
		public unsafe byte* properties;

		// Token: 0x04008ADE RID: 35550
		public unsafe byte* insulation;

		// Token: 0x04008ADF RID: 35551
		public unsafe byte* strengthInfo;

		// Token: 0x04008AE0 RID: 35552
		public unsafe float* radiation;

		// Token: 0x04008AE1 RID: 35553
		public unsafe byte* diseaseIdx;

		// Token: 0x04008AE2 RID: 35554
		public unsafe int* diseaseCount;

		// Token: 0x04008AE3 RID: 35555
		public int numSolidInfo;

		// Token: 0x04008AE4 RID: 35556
		public unsafe Sim.SolidInfo* solidInfo;

		// Token: 0x04008AE5 RID: 35557
		public int numLiquidChangeInfo;

		// Token: 0x04008AE6 RID: 35558
		public unsafe Sim.LiquidChangeInfo* liquidChangeInfo;

		// Token: 0x04008AE7 RID: 35559
		public int numSolidSubstanceChangeInfo;

		// Token: 0x04008AE8 RID: 35560
		public unsafe Sim.SolidSubstanceChangeInfo* solidSubstanceChangeInfo;

		// Token: 0x04008AE9 RID: 35561
		public int numSubstanceChangeInfo;

		// Token: 0x04008AEA RID: 35562
		public unsafe Sim.SubstanceChangeInfo* substanceChangeInfo;

		// Token: 0x04008AEB RID: 35563
		public int numCallbackInfo;

		// Token: 0x04008AEC RID: 35564
		public unsafe Sim.CallbackInfo* callbackInfo;

		// Token: 0x04008AED RID: 35565
		public int numSpawnFallingLiquidInfo;

		// Token: 0x04008AEE RID: 35566
		public unsafe Sim.SpawnFallingLiquidInfo* spawnFallingLiquidInfo;

		// Token: 0x04008AEF RID: 35567
		public int numDigInfo;

		// Token: 0x04008AF0 RID: 35568
		public unsafe Sim.SpawnOreInfo* digInfo;

		// Token: 0x04008AF1 RID: 35569
		public int numSpawnOreInfo;

		// Token: 0x04008AF2 RID: 35570
		public unsafe Sim.SpawnOreInfo* spawnOreInfo;

		// Token: 0x04008AF3 RID: 35571
		public int numSpawnFXInfo;

		// Token: 0x04008AF4 RID: 35572
		public unsafe Sim.SpawnFXInfo* spawnFXInfo;

		// Token: 0x04008AF5 RID: 35573
		public int numUnstableCellInfo;

		// Token: 0x04008AF6 RID: 35574
		public unsafe Sim.UnstableCellInfo* unstableCellInfo;

		// Token: 0x04008AF7 RID: 35575
		public int numWorldDamageInfo;

		// Token: 0x04008AF8 RID: 35576
		public unsafe Sim.WorldDamageInfo* worldDamageInfo;

		// Token: 0x04008AF9 RID: 35577
		public int numBuildingTemperatures;

		// Token: 0x04008AFA RID: 35578
		public unsafe Sim.BuildingTemperatureInfo* buildingTemperatures;

		// Token: 0x04008AFB RID: 35579
		public int numMassConsumedCallbacks;

		// Token: 0x04008AFC RID: 35580
		public unsafe Sim.MassConsumedCallback* massConsumedCallbacks;

		// Token: 0x04008AFD RID: 35581
		public int numMassEmittedCallbacks;

		// Token: 0x04008AFE RID: 35582
		public unsafe Sim.MassEmittedCallback* massEmittedCallbacks;

		// Token: 0x04008AFF RID: 35583
		public int numDiseaseConsumptionCallbacks;

		// Token: 0x04008B00 RID: 35584
		public unsafe Sim.DiseaseConsumptionCallback* diseaseConsumptionCallbacks;

		// Token: 0x04008B01 RID: 35585
		public int numComponentStateChangedMessages;

		// Token: 0x04008B02 RID: 35586
		public unsafe Sim.ComponentStateChangedMessage* componentStateChangedMessages;

		// Token: 0x04008B03 RID: 35587
		public int numRemovedMassEntries;

		// Token: 0x04008B04 RID: 35588
		public unsafe Sim.ConsumedMassInfo* removedMassEntries;

		// Token: 0x04008B05 RID: 35589
		public int numEmittedMassEntries;

		// Token: 0x04008B06 RID: 35590
		public unsafe Sim.EmittedMassInfo* emittedMassEntries;

		// Token: 0x04008B07 RID: 35591
		public int numElementChunkInfos;

		// Token: 0x04008B08 RID: 35592
		public unsafe Sim.ElementChunkInfo* elementChunkInfos;

		// Token: 0x04008B09 RID: 35593
		public int numElementChunkMeltedInfos;

		// Token: 0x04008B0A RID: 35594
		public unsafe Sim.MeltedInfo* elementChunkMeltedInfos;

		// Token: 0x04008B0B RID: 35595
		public int numBuildingOverheatInfos;

		// Token: 0x04008B0C RID: 35596
		public unsafe Sim.MeltedInfo* buildingOverheatInfos;

		// Token: 0x04008B0D RID: 35597
		public int numBuildingNoLongerOverheatedInfos;

		// Token: 0x04008B0E RID: 35598
		public unsafe Sim.MeltedInfo* buildingNoLongerOverheatedInfos;

		// Token: 0x04008B0F RID: 35599
		public int numBuildingMeltedInfos;

		// Token: 0x04008B10 RID: 35600
		public unsafe Sim.MeltedInfo* buildingMeltedInfos;

		// Token: 0x04008B11 RID: 35601
		public int numCellMeltedInfos;

		// Token: 0x04008B12 RID: 35602
		public unsafe Sim.CellMeltedInfo* cellMeltedInfos;

		// Token: 0x04008B13 RID: 35603
		public int numDiseaseEmittedInfos;

		// Token: 0x04008B14 RID: 35604
		public unsafe Sim.DiseaseEmittedInfo* diseaseEmittedInfos;

		// Token: 0x04008B15 RID: 35605
		public int numDiseaseConsumedInfos;

		// Token: 0x04008B16 RID: 35606
		public unsafe Sim.DiseaseConsumedInfo* diseaseConsumedInfos;

		// Token: 0x04008B17 RID: 35607
		public int numRadiationConsumedCallbacks;

		// Token: 0x04008B18 RID: 35608
		public unsafe Sim.ConsumedRadiationCallback* radiationConsumedCallbacks;

		// Token: 0x04008B19 RID: 35609
		public unsafe float* accumulatedFlow;

		// Token: 0x04008B1A RID: 35610
		public IntPtr propertyTextureFlow;

		// Token: 0x04008B1B RID: 35611
		public IntPtr propertyTextureLiquid;

		// Token: 0x04008B1C RID: 35612
		public IntPtr propertyTextureLiquidData;

		// Token: 0x04008B1D RID: 35613
		public IntPtr propertyTextureExposedToSunlight;
	}

	// Token: 0x02001DC6 RID: 7622
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct SpawnFallingLiquidInfo
	{
		// Token: 0x04008B1E RID: 35614
		public int cellIdx;

		// Token: 0x04008B1F RID: 35615
		public ushort elemIdx;

		// Token: 0x04008B20 RID: 35616
		public byte diseaseIdx;

		// Token: 0x04008B21 RID: 35617
		public byte pad0;

		// Token: 0x04008B22 RID: 35618
		public float mass;

		// Token: 0x04008B23 RID: 35619
		public float temperature;

		// Token: 0x04008B24 RID: 35620
		public int diseaseCount;
	}

	// Token: 0x02001DC7 RID: 7623
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct SpawnOreInfo
	{
		// Token: 0x04008B25 RID: 35621
		public int cellIdx;

		// Token: 0x04008B26 RID: 35622
		public ushort elemIdx;

		// Token: 0x04008B27 RID: 35623
		public byte diseaseIdx;

		// Token: 0x04008B28 RID: 35624
		private byte pad0;

		// Token: 0x04008B29 RID: 35625
		public float mass;

		// Token: 0x04008B2A RID: 35626
		public float temperature;

		// Token: 0x04008B2B RID: 35627
		public int diseaseCount;
	}

	// Token: 0x02001DC8 RID: 7624
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct SpawnFXInfo
	{
		// Token: 0x04008B2C RID: 35628
		public int cellIdx;

		// Token: 0x04008B2D RID: 35629
		public int fxHash;

		// Token: 0x04008B2E RID: 35630
		public float rotation;
	}

	// Token: 0x02001DC9 RID: 7625
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct UnstableCellInfo
	{
		// Token: 0x04008B2F RID: 35631
		public int cellIdx;

		// Token: 0x04008B30 RID: 35632
		public ushort elemIdx;

		// Token: 0x04008B31 RID: 35633
		public byte fallingInfo;

		// Token: 0x04008B32 RID: 35634
		public byte diseaseIdx;

		// Token: 0x04008B33 RID: 35635
		public float mass;

		// Token: 0x04008B34 RID: 35636
		public float temperature;

		// Token: 0x04008B35 RID: 35637
		public int diseaseCount;

		// Token: 0x020028F6 RID: 10486
		public enum FallingInfo
		{
			// Token: 0x0400B569 RID: 46441
			StartedFalling,
			// Token: 0x0400B56A RID: 46442
			StoppedFalling
		}
	}

	// Token: 0x02001DCA RID: 7626
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct NewGameFrame
	{
		// Token: 0x04008B36 RID: 35638
		public float elapsedSeconds;

		// Token: 0x04008B37 RID: 35639
		public int minX;

		// Token: 0x04008B38 RID: 35640
		public int minY;

		// Token: 0x04008B39 RID: 35641
		public int maxX;

		// Token: 0x04008B3A RID: 35642
		public int maxY;

		// Token: 0x04008B3B RID: 35643
		public float currentSunlightIntensity;

		// Token: 0x04008B3C RID: 35644
		public float currentCosmicRadiationIntensity;
	}

	// Token: 0x02001DCB RID: 7627
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct WorldDamageInfo
	{
		// Token: 0x04008B3D RID: 35645
		public int gameCell;

		// Token: 0x04008B3E RID: 35646
		public int damageSourceOffset;
	}

	// Token: 0x02001DCC RID: 7628
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct PipeTemperatureChange
	{
		// Token: 0x04008B3F RID: 35647
		public int cellIdx;

		// Token: 0x04008B40 RID: 35648
		public float temperature;
	}

	// Token: 0x02001DCD RID: 7629
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct MassConsumedCallback
	{
		// Token: 0x04008B41 RID: 35649
		public int callbackIdx;

		// Token: 0x04008B42 RID: 35650
		public ushort elemIdx;

		// Token: 0x04008B43 RID: 35651
		public byte diseaseIdx;

		// Token: 0x04008B44 RID: 35652
		private byte pad0;

		// Token: 0x04008B45 RID: 35653
		public float mass;

		// Token: 0x04008B46 RID: 35654
		public float temperature;

		// Token: 0x04008B47 RID: 35655
		public int diseaseCount;
	}

	// Token: 0x02001DCE RID: 7630
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct MassEmittedCallback
	{
		// Token: 0x04008B48 RID: 35656
		public int callbackIdx;

		// Token: 0x04008B49 RID: 35657
		public ushort elemIdx;

		// Token: 0x04008B4A RID: 35658
		public byte suceeded;

		// Token: 0x04008B4B RID: 35659
		public byte diseaseIdx;

		// Token: 0x04008B4C RID: 35660
		public float mass;

		// Token: 0x04008B4D RID: 35661
		public float temperature;

		// Token: 0x04008B4E RID: 35662
		public int diseaseCount;
	}

	// Token: 0x02001DCF RID: 7631
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct DiseaseConsumptionCallback
	{
		// Token: 0x04008B4F RID: 35663
		public int callbackIdx;

		// Token: 0x04008B50 RID: 35664
		public byte diseaseIdx;

		// Token: 0x04008B51 RID: 35665
		private byte pad0;

		// Token: 0x04008B52 RID: 35666
		private byte pad1;

		// Token: 0x04008B53 RID: 35667
		private byte pad2;

		// Token: 0x04008B54 RID: 35668
		public int diseaseCount;
	}

	// Token: 0x02001DD0 RID: 7632
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct ComponentStateChangedMessage
	{
		// Token: 0x04008B55 RID: 35669
		public int callbackIdx;

		// Token: 0x04008B56 RID: 35670
		public int simHandle;
	}

	// Token: 0x02001DD1 RID: 7633
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct DebugProperties
	{
		// Token: 0x04008B57 RID: 35671
		public float buildingTemperatureScale;

		// Token: 0x04008B58 RID: 35672
		public float buildingToBuildingTemperatureScale;

		// Token: 0x04008B59 RID: 35673
		public float biomeTemperatureLerpRate;

		// Token: 0x04008B5A RID: 35674
		public byte isDebugEditing;

		// Token: 0x04008B5B RID: 35675
		public byte pad0;

		// Token: 0x04008B5C RID: 35676
		public byte pad1;

		// Token: 0x04008B5D RID: 35677
		public byte pad2;
	}

	// Token: 0x02001DD2 RID: 7634
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct EmittedMassInfo
	{
		// Token: 0x04008B5E RID: 35678
		public ushort elemIdx;

		// Token: 0x04008B5F RID: 35679
		public byte diseaseIdx;

		// Token: 0x04008B60 RID: 35680
		public byte pad0;

		// Token: 0x04008B61 RID: 35681
		public float mass;

		// Token: 0x04008B62 RID: 35682
		public float temperature;

		// Token: 0x04008B63 RID: 35683
		public int diseaseCount;
	}

	// Token: 0x02001DD3 RID: 7635
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct ConsumedMassInfo
	{
		// Token: 0x04008B64 RID: 35684
		public int simHandle;

		// Token: 0x04008B65 RID: 35685
		public ushort removedElemIdx;

		// Token: 0x04008B66 RID: 35686
		public byte diseaseIdx;

		// Token: 0x04008B67 RID: 35687
		private byte pad0;

		// Token: 0x04008B68 RID: 35688
		public float mass;

		// Token: 0x04008B69 RID: 35689
		public float temperature;

		// Token: 0x04008B6A RID: 35690
		public int diseaseCount;
	}

	// Token: 0x02001DD4 RID: 7636
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct ConsumedDiseaseInfo
	{
		// Token: 0x04008B6B RID: 35691
		public int simHandle;

		// Token: 0x04008B6C RID: 35692
		public byte diseaseIdx;

		// Token: 0x04008B6D RID: 35693
		private byte pad0;

		// Token: 0x04008B6E RID: 35694
		private byte pad1;

		// Token: 0x04008B6F RID: 35695
		private byte pad2;

		// Token: 0x04008B70 RID: 35696
		public int diseaseCount;
	}

	// Token: 0x02001DD5 RID: 7637
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct ElementChunkInfo
	{
		// Token: 0x04008B71 RID: 35697
		public float temperature;

		// Token: 0x04008B72 RID: 35698
		public float deltaKJ;
	}

	// Token: 0x02001DD6 RID: 7638
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct MeltedInfo
	{
		// Token: 0x04008B73 RID: 35699
		public int handle;
	}

	// Token: 0x02001DD7 RID: 7639
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct CellMeltedInfo
	{
		// Token: 0x04008B74 RID: 35700
		public int gameCell;
	}

	// Token: 0x02001DD8 RID: 7640
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct BuildingTemperatureInfo
	{
		// Token: 0x04008B75 RID: 35701
		public int handle;

		// Token: 0x04008B76 RID: 35702
		public float temperature;
	}

	// Token: 0x02001DD9 RID: 7641
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct BuildingConductivityData
	{
		// Token: 0x04008B77 RID: 35703
		public float temperature;

		// Token: 0x04008B78 RID: 35704
		public float heatCapacity;

		// Token: 0x04008B79 RID: 35705
		public float thermalConductivity;
	}

	// Token: 0x02001DDA RID: 7642
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct DiseaseEmittedInfo
	{
		// Token: 0x04008B7A RID: 35706
		public byte diseaseIdx;

		// Token: 0x04008B7B RID: 35707
		private byte pad0;

		// Token: 0x04008B7C RID: 35708
		private byte pad1;

		// Token: 0x04008B7D RID: 35709
		private byte pad2;

		// Token: 0x04008B7E RID: 35710
		public int count;
	}

	// Token: 0x02001DDB RID: 7643
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct DiseaseConsumedInfo
	{
		// Token: 0x04008B7F RID: 35711
		public byte diseaseIdx;

		// Token: 0x04008B80 RID: 35712
		private byte pad0;

		// Token: 0x04008B81 RID: 35713
		private byte pad1;

		// Token: 0x04008B82 RID: 35714
		private byte pad2;

		// Token: 0x04008B83 RID: 35715
		public int count;
	}

	// Token: 0x02001DDC RID: 7644
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct ConsumedRadiationCallback
	{
		// Token: 0x04008B84 RID: 35716
		public int callbackIdx;

		// Token: 0x04008B85 RID: 35717
		public int gameCell;

		// Token: 0x04008B86 RID: 35718
		public float radiation;
	}
}
