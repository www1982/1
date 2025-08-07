using System;

// Token: 0x02000563 RID: 1379
public class Blueprints
{
	// Token: 0x06001EAE RID: 7854 RVA: 0x000A5580 File Offset: 0x000A3780
	public static Blueprints Get()
	{
		if (Blueprints.instance == null)
		{
			Blueprints.instance = new Blueprints();
			Blueprints.instance.all.AddBlueprintsFrom<Blueprints_Default>(new Blueprints_Default());
			foreach (BlueprintProvider blueprintProvider in Blueprints.instance.skinsReleaseProviders)
			{
				Blueprints.instance.skinsRelease.AddBlueprintsFrom<BlueprintProvider>(blueprintProvider);
			}
			Blueprints.instance.all.AddBlueprintsFrom(Blueprints.instance.skinsRelease);
			Blueprints.instance.skinsRelease.PostProcess();
			Blueprints.instance.all.PostProcess();
		}
		return Blueprints.instance;
	}

	// Token: 0x040011E7 RID: 4583
	public BlueprintCollection all = new BlueprintCollection();

	// Token: 0x040011E8 RID: 4584
	public BlueprintCollection skinsRelease = new BlueprintCollection();

	// Token: 0x040011E9 RID: 4585
	public BlueprintProvider[] skinsReleaseProviders = new BlueprintProvider[]
	{
		new Blueprints_U51AndBefore(),
		new Blueprints_DlcPack2(),
		new Blueprints_U53(),
		new Blueprints_DlcPack3(),
		new Blueprints_DlcPack4()
	};

	// Token: 0x040011EA RID: 4586
	private static Blueprints instance;
}
