using System;
using UnityEngine;

namespace Database
{
	// Token: 0x02000F17 RID: 3863
	public class TechTreeTitles : ResourceSet<TechTreeTitle>
	{
		// Token: 0x06007A14 RID: 31252 RVA: 0x003048F0 File Offset: 0x00302AF0
		public TechTreeTitles(ResourceSet parent)
			: base("TreeTitles", parent)
		{
		}

		// Token: 0x06007A15 RID: 31253 RVA: 0x00304900 File Offset: 0x00302B00
		public void Load(TextAsset tree_file)
		{
			foreach (ResourceTreeNode resourceTreeNode in new ResourceTreeLoader<ResourceTreeNode>(tree_file))
			{
				if (string.Equals(resourceTreeNode.Id.Substring(0, 1), "_"))
				{
					new TechTreeTitle(resourceTreeNode.Id, this, Strings.Get("STRINGS.RESEARCH.TREES.TITLE" + resourceTreeNode.Id.ToUpper()), resourceTreeNode);
				}
			}
		}
	}
}
