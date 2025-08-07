using System;
using System.Collections.Generic;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000FD5 RID: 4053
	[AddComponentMenu("KMonoBehaviour/scripts/AttributeConverters")]
	public class AttributeConverters : KMonoBehaviour
	{
		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06007D3B RID: 32059 RVA: 0x00321B3E File Offset: 0x0031FD3E
		public int Count
		{
			get
			{
				return this.converters.Count;
			}
		}

		// Token: 0x06007D3C RID: 32060 RVA: 0x00321B4C File Offset: 0x0031FD4C
		protected override void OnPrefabInit()
		{
			foreach (AttributeInstance attributeInstance in this.GetAttributes())
			{
				foreach (AttributeConverter attributeConverter in attributeInstance.Attribute.converters)
				{
					AttributeConverterInstance attributeConverterInstance = new AttributeConverterInstance(base.gameObject, attributeConverter, attributeInstance);
					this.converters.Add(attributeConverterInstance);
				}
			}
		}

		// Token: 0x06007D3D RID: 32061 RVA: 0x00321BF0 File Offset: 0x0031FDF0
		public AttributeConverterInstance Get(AttributeConverter converter)
		{
			foreach (AttributeConverterInstance attributeConverterInstance in this.converters)
			{
				if (attributeConverterInstance.converter == converter)
				{
					return attributeConverterInstance;
				}
			}
			return null;
		}

		// Token: 0x06007D3E RID: 32062 RVA: 0x00321C4C File Offset: 0x0031FE4C
		public AttributeConverterInstance GetConverter(string id)
		{
			foreach (AttributeConverterInstance attributeConverterInstance in this.converters)
			{
				if (attributeConverterInstance.converter.Id == id)
				{
					return attributeConverterInstance;
				}
			}
			return null;
		}

		// Token: 0x04005E98 RID: 24216
		public List<AttributeConverterInstance> converters = new List<AttributeConverterInstance>();
	}
}
