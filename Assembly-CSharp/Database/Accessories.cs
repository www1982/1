using System;

namespace Database
{
	// Token: 0x02000ECE RID: 3790
	public class Accessories : ResourceSet<Accessory>
	{
		// Token: 0x06007918 RID: 31000 RVA: 0x002EC2A4 File Offset: 0x002EA4A4
		public Accessories(ResourceSet parent)
			: base("Accessories", parent)
		{
		}

		// Token: 0x06007919 RID: 31001 RVA: 0x002EC2B4 File Offset: 0x002EA4B4
		public void AddAccessories(string id, KAnimFile anim_file)
		{
			if (anim_file != null)
			{
				KAnim.Build build = anim_file.GetData().build;
				for (int i = 0; i < build.symbols.Length; i++)
				{
					string text = HashCache.Get().Get(build.symbols[i].hash);
					AccessorySlot accessorySlot = Db.Get().AccessorySlots.Find(text);
					if (accessorySlot != null)
					{
						Accessory accessory = new Accessory(id + text, this, accessorySlot, anim_file.batchTag, build.symbols[i], anim_file, null);
						accessorySlot.accessories.Add(accessory);
						HashCache.Get().Add(accessory.IdHash.HashValue, accessory.Id);
					}
				}
			}
		}

		// Token: 0x0600791A RID: 31002 RVA: 0x002EC36C File Offset: 0x002EA56C
		public void AddCustomAccessories(KAnimFile anim_file, ResourceSet parent, AccessorySlots slots)
		{
			if (anim_file != null)
			{
				KAnim.Build build = anim_file.GetData().build;
				for (int i = 0; i < build.symbols.Length; i++)
				{
					string symbol_name = HashCache.Get().Get(build.symbols[i].hash);
					AccessorySlot accessorySlot = slots.resources.Find((AccessorySlot slot) => symbol_name.IndexOf(slot.Id, 0, StringComparison.OrdinalIgnoreCase) != -1);
					if (accessorySlot != null)
					{
						Accessory accessory = new Accessory(symbol_name, parent, accessorySlot, anim_file.batchTag, build.symbols[i], anim_file, null);
						accessorySlot.accessories.Add(accessory);
						HashCache.Get().Add(accessory.IdHash.HashValue, accessory.Id);
					}
				}
			}
		}
	}
}
