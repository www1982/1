using System;

// Token: 0x020006AA RID: 1706
internal class ElementAudioFileLoader : AsyncCsvLoader<ElementAudioFileLoader, ElementsAudio.ElementAudioConfig>
{
	// Token: 0x060029B6 RID: 10678 RVA: 0x000F25F9 File Offset: 0x000F07F9
	public ElementAudioFileLoader()
		: base(Assets.instance.elementAudio)
	{
	}

	// Token: 0x060029B7 RID: 10679 RVA: 0x000F260B File Offset: 0x000F080B
	public override void Run()
	{
		base.Run();
	}
}
