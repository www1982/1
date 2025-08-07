using System;

// Token: 0x02000DEA RID: 3562
public interface IConfigurableConsumer
{
	// Token: 0x0600708F RID: 28815
	IConfigurableConsumerOption[] GetSettingOptions();

	// Token: 0x06007090 RID: 28816
	IConfigurableConsumerOption GetSelectedOption();

	// Token: 0x06007091 RID: 28817
	void SetSelectedOption(IConfigurableConsumerOption option);
}
