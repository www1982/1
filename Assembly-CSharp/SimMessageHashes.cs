using System;

// Token: 0x02000C44 RID: 3140
public enum SimMessageHashes
{
	// Token: 0x040040B7 RID: 16567
	Elements_CreateTable = 1108437482,
	// Token: 0x040040B8 RID: 16568
	Elements_CreateInteractions = -930289787,
	// Token: 0x040040B9 RID: 16569
	SetWorldZones = -457308393,
	// Token: 0x040040BA RID: 16570
	ModifyCellWorldZone = -449718014,
	// Token: 0x040040BB RID: 16571
	Disease_CreateTable = 825301935,
	// Token: 0x040040BC RID: 16572
	Load = -672538170,
	// Token: 0x040040BD RID: 16573
	Start = -931446686,
	// Token: 0x040040BE RID: 16574
	AllocateCells = 1092408308,
	// Token: 0x040040BF RID: 16575
	ClearUnoccupiedCells = -1836204275,
	// Token: 0x040040C0 RID: 16576
	DefineWorldOffsets = -895846551,
	// Token: 0x040040C1 RID: 16577
	PrepareGameData = 1078620451,
	// Token: 0x040040C2 RID: 16578
	SimData_InitializeFromCells = 2062421945,
	// Token: 0x040040C3 RID: 16579
	SimData_ResizeAndInitializeVacuumCells = -752676153,
	// Token: 0x040040C4 RID: 16580
	SimData_FreeCells = -1167792921,
	// Token: 0x040040C5 RID: 16581
	SimFrameManager_NewGameFrame = -775326397,
	// Token: 0x040040C6 RID: 16582
	Dig = 833038498,
	// Token: 0x040040C7 RID: 16583
	ModifyCell = -1252920804,
	// Token: 0x040040C8 RID: 16584
	ModifyCellEnergy = 818320644,
	// Token: 0x040040C9 RID: 16585
	SetInsulationValue = -898773121,
	// Token: 0x040040CA RID: 16586
	SetStrengthValue = 1593243982,
	// Token: 0x040040CB RID: 16587
	SetVisibleCells = -563057023,
	// Token: 0x040040CC RID: 16588
	ChangeCellProperties = -469311643,
	// Token: 0x040040CD RID: 16589
	AddBuildingHeatExchange = 1739021608,
	// Token: 0x040040CE RID: 16590
	ModifyBuildingHeatExchange = 1818001569,
	// Token: 0x040040CF RID: 16591
	ModifyBuildingEnergy = -1348791658,
	// Token: 0x040040D0 RID: 16592
	RemoveBuildingHeatExchange = -456116629,
	// Token: 0x040040D1 RID: 16593
	AddBuildingToBuildingHeatExchange = -1338718217,
	// Token: 0x040040D2 RID: 16594
	AddInContactBuildingToBuildingToBuildingHeatExchange = -1586724321,
	// Token: 0x040040D3 RID: 16595
	RemoveBuildingInContactFromBuildingToBuildingHeatExchange = -1993857213,
	// Token: 0x040040D4 RID: 16596
	RemoveBuildingToBuildingHeatExchange = 697100730,
	// Token: 0x040040D5 RID: 16597
	SetDebugProperties = -1683118492,
	// Token: 0x040040D6 RID: 16598
	MassConsumption = 1727657959,
	// Token: 0x040040D7 RID: 16599
	MassEmission = 797274363,
	// Token: 0x040040D8 RID: 16600
	AddElementConsumer = 2024405073,
	// Token: 0x040040D9 RID: 16601
	RemoveElementConsumer = 894417742,
	// Token: 0x040040DA RID: 16602
	SetElementConsumerData = 1575539738,
	// Token: 0x040040DB RID: 16603
	AddElementEmitter = -505471181,
	// Token: 0x040040DC RID: 16604
	ModifyElementEmitter = 403589164,
	// Token: 0x040040DD RID: 16605
	RemoveElementEmitter = -1524118282,
	// Token: 0x040040DE RID: 16606
	AddElementChunk = 1445724082,
	// Token: 0x040040DF RID: 16607
	RemoveElementChunk = -912908555,
	// Token: 0x040040E0 RID: 16608
	SetElementChunkData = -435115907,
	// Token: 0x040040E1 RID: 16609
	MoveElementChunk = -374911358,
	// Token: 0x040040E2 RID: 16610
	ModifyElementChunkEnergy = 1020555667,
	// Token: 0x040040E3 RID: 16611
	ModifyChunkTemperatureAdjuster = -1387601379,
	// Token: 0x040040E4 RID: 16612
	AddDiseaseEmitter = 1486783027,
	// Token: 0x040040E5 RID: 16613
	ModifyDiseaseEmitter = -1899123924,
	// Token: 0x040040E6 RID: 16614
	RemoveDiseaseEmitter = 468135926,
	// Token: 0x040040E7 RID: 16615
	AddDiseaseConsumer = 348345681,
	// Token: 0x040040E8 RID: 16616
	ModifyDiseaseConsumer = -1822987624,
	// Token: 0x040040E9 RID: 16617
	RemoveDiseaseConsumer = -781641650,
	// Token: 0x040040EA RID: 16618
	ConsumeDisease = -1019841536,
	// Token: 0x040040EB RID: 16619
	CellDiseaseModification = -1853671274,
	// Token: 0x040040EC RID: 16620
	ToggleProfiler = -409964931,
	// Token: 0x040040ED RID: 16621
	SetSavedOptions = 1154135737,
	// Token: 0x040040EE RID: 16622
	CellRadiationModification = -1914877797,
	// Token: 0x040040EF RID: 16623
	RadiationSickness = -727746602,
	// Token: 0x040040F0 RID: 16624
	AddRadiationEmitter = -1505895314,
	// Token: 0x040040F1 RID: 16625
	ModifyRadiationEmitter = -503965465,
	// Token: 0x040040F2 RID: 16626
	RemoveRadiationEmitter = -704259919,
	// Token: 0x040040F3 RID: 16627
	RadiationParamsModification = 377112707
}
