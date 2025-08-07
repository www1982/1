using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000C3B RID: 3131
public class TagFilterScreen : SideScreenContent
{
	// Token: 0x06005F4A RID: 24394 RVA: 0x0023072B File Offset: 0x0022E92B
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<TreeFilterable>() != null;
	}

	// Token: 0x06005F4B RID: 24395 RVA: 0x0023073C File Offset: 0x0022E93C
	public override void SetTarget(GameObject target)
	{
		if (target == null)
		{
			global::Debug.LogError("The target object provided was null");
			return;
		}
		this.targetFilterable = target.GetComponent<TreeFilterable>();
		if (this.targetFilterable == null)
		{
			global::Debug.LogError("The target provided does not have a Tree Filterable component");
			return;
		}
		if (!this.targetFilterable.showUserMenu)
		{
			return;
		}
		this.Filter(this.targetFilterable.AcceptedTags);
		base.Activate();
	}

	// Token: 0x06005F4C RID: 24396 RVA: 0x002307A8 File Offset: 0x0022E9A8
	protected override void OnActivate()
	{
		this.rootItem = this.BuildDisplay(this.rootTag);
		this.treeControl.SetUserItemRoot(this.rootItem);
		this.treeControl.root.opened = true;
		this.Filter(this.treeControl.root, this.acceptedTags, false);
	}

	// Token: 0x06005F4D RID: 24397 RVA: 0x00230804 File Offset: 0x0022EA04
	public static List<Tag> GetAllTags()
	{
		List<Tag> list = new List<Tag>();
		foreach (TagFilterScreen.TagEntry tagEntry in TagFilterScreen.defaultRootTag.children)
		{
			if (tagEntry.tag.IsValid)
			{
				list.Add(tagEntry.tag);
			}
		}
		return list;
	}

	// Token: 0x06005F4E RID: 24398 RVA: 0x00230850 File Offset: 0x0022EA50
	private KTreeControl.UserItem BuildDisplay(TagFilterScreen.TagEntry root)
	{
		KTreeControl.UserItem userItem = null;
		if (root.name != null && root.name != "")
		{
			userItem = new KTreeControl.UserItem
			{
				text = root.name,
				userData = root.tag
			};
			List<KTreeControl.UserItem> list = new List<KTreeControl.UserItem>();
			if (root.children != null)
			{
				foreach (TagFilterScreen.TagEntry tagEntry in root.children)
				{
					list.Add(this.BuildDisplay(tagEntry));
				}
			}
			userItem.children = list;
		}
		return userItem;
	}

	// Token: 0x06005F4F RID: 24399 RVA: 0x002308DC File Offset: 0x0022EADC
	private static KTreeControl.UserItem CreateTree(string tree_name, Tag tree_tag, IList<Element> items)
	{
		KTreeControl.UserItem userItem = new KTreeControl.UserItem
		{
			text = tree_name,
			userData = tree_tag,
			children = new List<KTreeControl.UserItem>()
		};
		foreach (Element element in items)
		{
			KTreeControl.UserItem userItem2 = new KTreeControl.UserItem
			{
				text = element.name,
				userData = GameTagExtensions.Create(element.id)
			};
			userItem.children.Add(userItem2);
		}
		return userItem;
	}

	// Token: 0x06005F50 RID: 24400 RVA: 0x00230978 File Offset: 0x0022EB78
	public void SetRootTag(TagFilterScreen.TagEntry root_tag)
	{
		this.rootTag = root_tag;
	}

	// Token: 0x06005F51 RID: 24401 RVA: 0x00230981 File Offset: 0x0022EB81
	public void Filter(HashSet<Tag> acceptedTags)
	{
		this.acceptedTags = acceptedTags;
	}

	// Token: 0x06005F52 RID: 24402 RVA: 0x0023098C File Offset: 0x0022EB8C
	private void Filter(KTreeItem root, HashSet<Tag> acceptedTags, bool parentEnabled)
	{
		root.checkboxChecked = parentEnabled || (root.userData != null && acceptedTags.Contains((Tag)root.userData));
		foreach (KTreeItem ktreeItem in root.children)
		{
			this.Filter(ktreeItem, acceptedTags, root.checkboxChecked);
		}
		if (!root.checkboxChecked && root.children.Count > 0)
		{
			bool flag = true;
			using (IEnumerator<KTreeItem> enumerator = root.children.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.checkboxChecked)
					{
						flag = false;
						break;
					}
				}
			}
			root.checkboxChecked = flag;
		}
	}

	// Token: 0x04003F8B RID: 16267
	[SerializeField]
	private KTreeControl treeControl;

	// Token: 0x04003F8C RID: 16268
	private KTreeControl.UserItem rootItem;

	// Token: 0x04003F8D RID: 16269
	private TagFilterScreen.TagEntry rootTag = TagFilterScreen.defaultRootTag;

	// Token: 0x04003F8E RID: 16270
	private HashSet<Tag> acceptedTags = new HashSet<Tag>();

	// Token: 0x04003F8F RID: 16271
	private TreeFilterable targetFilterable;

	// Token: 0x04003F90 RID: 16272
	public static TagFilterScreen.TagEntry defaultRootTag = new TagFilterScreen.TagEntry
	{
		name = "All",
		tag = default(Tag),
		children = new TagFilterScreen.TagEntry[0]
	};

	// Token: 0x02001DA8 RID: 7592
	public class TagEntry
	{
		// Token: 0x04008A58 RID: 35416
		public string name;

		// Token: 0x04008A59 RID: 35417
		public Tag tag;

		// Token: 0x04008A5A RID: 35418
		public TagFilterScreen.TagEntry[] children;
	}
}
