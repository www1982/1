using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000C7E RID: 3198
public class CodexEntry : IHasDlcRestrictions
{
	// Token: 0x060061EC RID: 25068 RVA: 0x0024671A File Offset: 0x0024491A
	public CodexEntry()
	{
	}

	// Token: 0x060061ED RID: 25069 RVA: 0x0024675C File Offset: 0x0024495C
	public CodexEntry(string category, List<ContentContainer> contentContainers, string name)
	{
		this.category = category;
		this.name = name;
		this.contentContainers = contentContainers;
		if (string.IsNullOrEmpty(this.sortString))
		{
			this.sortString = UI.StripLinkFormatting(name);
		}
	}

	// Token: 0x060061EE RID: 25070 RVA: 0x002467D4 File Offset: 0x002449D4
	public CodexEntry(string category, string titleKey, List<ContentContainer> contentContainers)
	{
		this.category = category;
		this.title = titleKey;
		this.contentContainers = contentContainers;
		if (string.IsNullOrEmpty(this.sortString))
		{
			this.sortString = UI.StripLinkFormatting(this.title);
		}
	}

	// Token: 0x17000709 RID: 1801
	// (get) Token: 0x060061EF RID: 25071 RVA: 0x00246851 File Offset: 0x00244A51
	// (set) Token: 0x060061F0 RID: 25072 RVA: 0x00246859 File Offset: 0x00244A59
	public List<ContentContainer> contentContainers
	{
		get
		{
			return this._contentContainers;
		}
		private set
		{
			this._contentContainers = value;
		}
	}

	// Token: 0x060061F1 RID: 25073 RVA: 0x00246864 File Offset: 0x00244A64
	public static List<string> ContentContainerDebug(List<ContentContainer> _contentContainers)
	{
		List<string> list = new List<string>();
		foreach (ContentContainer contentContainer in _contentContainers)
		{
			if (contentContainer != null)
			{
				string text = string.Concat(new string[]
				{
					"<b>",
					contentContainer.contentLayout.ToString(),
					" container: ",
					((contentContainer.content == null) ? 0 : contentContainer.content.Count).ToString(),
					" items</b>"
				});
				if (contentContainer.content != null)
				{
					text += "\n";
					for (int i = 0; i < contentContainer.content.Count; i++)
					{
						text = string.Concat(new string[]
						{
							text,
							"    • ",
							contentContainer.content[i].ToString(),
							": ",
							CodexEntry.GetContentWidgetDebugString(contentContainer.content[i]),
							"\n"
						});
					}
				}
				list.Add(text);
			}
			else
			{
				list.Add("null container");
			}
		}
		return list;
	}

	// Token: 0x060061F2 RID: 25074 RVA: 0x002469BC File Offset: 0x00244BBC
	private static string GetContentWidgetDebugString(ICodexWidget widget)
	{
		CodexText codexText = widget as CodexText;
		if (codexText != null)
		{
			return codexText.text;
		}
		CodexLabelWithIcon codexLabelWithIcon = widget as CodexLabelWithIcon;
		if (codexLabelWithIcon != null)
		{
			return codexLabelWithIcon.label.text + " / " + codexLabelWithIcon.icon.spriteName;
		}
		CodexImage codexImage = widget as CodexImage;
		if (codexImage != null)
		{
			return codexImage.spriteName;
		}
		CodexVideo codexVideo = widget as CodexVideo;
		if (codexVideo != null)
		{
			return codexVideo.name;
		}
		CodexIndentedLabelWithIcon codexIndentedLabelWithIcon = widget as CodexIndentedLabelWithIcon;
		if (codexIndentedLabelWithIcon != null)
		{
			return codexIndentedLabelWithIcon.label.text + " / " + codexIndentedLabelWithIcon.icon.spriteName;
		}
		return "";
	}

	// Token: 0x060061F3 RID: 25075 RVA: 0x00246A5B File Offset: 0x00244C5B
	public void CreateContentContainerCollection()
	{
		this.contentContainers = new List<ContentContainer>();
	}

	// Token: 0x060061F4 RID: 25076 RVA: 0x00246A68 File Offset: 0x00244C68
	public void InsertContentContainer(int index, ContentContainer container)
	{
		this.contentContainers.Insert(index, container);
	}

	// Token: 0x060061F5 RID: 25077 RVA: 0x00246A77 File Offset: 0x00244C77
	public void RemoveContentContainerAt(int index)
	{
		this.contentContainers.RemoveAt(index);
	}

	// Token: 0x060061F6 RID: 25078 RVA: 0x00246A85 File Offset: 0x00244C85
	public void AddContentContainer(ContentContainer container)
	{
		this.contentContainers.Add(container);
	}

	// Token: 0x060061F7 RID: 25079 RVA: 0x00246A93 File Offset: 0x00244C93
	public void AddContentContainerRange(IEnumerable<ContentContainer> containers)
	{
		this.contentContainers.AddRange(containers);
	}

	// Token: 0x060061F8 RID: 25080 RVA: 0x00246AA1 File Offset: 0x00244CA1
	public void RemoveContentContainer(ContentContainer container)
	{
		this.contentContainers.Remove(container);
	}

	// Token: 0x060061F9 RID: 25081 RVA: 0x00246AB0 File Offset: 0x00244CB0
	public ICodexWidget GetFirstWidget()
	{
		for (int i = 0; i < this.contentContainers.Count; i++)
		{
			if (this.contentContainers[i].content != null)
			{
				for (int j = 0; j < this.contentContainers[i].content.Count; j++)
				{
					if (this.contentContainers[i].content[j] != null)
					{
						return this.contentContainers[i].content[j];
					}
				}
			}
		}
		return null;
	}

	// Token: 0x1700070A RID: 1802
	// (get) Token: 0x060061FA RID: 25082 RVA: 0x00246B39 File Offset: 0x00244D39
	// (set) Token: 0x060061FB RID: 25083 RVA: 0x00246B41 File Offset: 0x00244D41
	public string[] requiredDlcIds { get; set; }

	// Token: 0x1700070B RID: 1803
	// (get) Token: 0x060061FC RID: 25084 RVA: 0x00246B4A File Offset: 0x00244D4A
	// (set) Token: 0x060061FD RID: 25085 RVA: 0x00246B52 File Offset: 0x00244D52
	public string[] forbiddenDlcIds { get; set; }

	// Token: 0x060061FE RID: 25086 RVA: 0x00246B5B File Offset: 0x00244D5B
	public string[] GetRequiredDlcIds()
	{
		return this.requiredDlcIds;
	}

	// Token: 0x060061FF RID: 25087 RVA: 0x00246B63 File Offset: 0x00244D63
	public string[] GetForbiddenDlcIds()
	{
		return this.forbiddenDlcIds;
	}

	// Token: 0x1700070C RID: 1804
	// (get) Token: 0x06006200 RID: 25088 RVA: 0x00246B6B File Offset: 0x00244D6B
	// (set) Token: 0x06006201 RID: 25089 RVA: 0x00246B73 File Offset: 0x00244D73
	public string id
	{
		get
		{
			return this._id;
		}
		set
		{
			this._id = value;
		}
	}

	// Token: 0x1700070D RID: 1805
	// (get) Token: 0x06006202 RID: 25090 RVA: 0x00246B7C File Offset: 0x00244D7C
	// (set) Token: 0x06006203 RID: 25091 RVA: 0x00246B84 File Offset: 0x00244D84
	public string parentId
	{
		get
		{
			return this._parentId;
		}
		set
		{
			this._parentId = value;
		}
	}

	// Token: 0x1700070E RID: 1806
	// (get) Token: 0x06006204 RID: 25092 RVA: 0x00246B8D File Offset: 0x00244D8D
	// (set) Token: 0x06006205 RID: 25093 RVA: 0x00246B95 File Offset: 0x00244D95
	public string category
	{
		get
		{
			return this._category;
		}
		set
		{
			this._category = value;
		}
	}

	// Token: 0x1700070F RID: 1807
	// (get) Token: 0x06006206 RID: 25094 RVA: 0x00246B9E File Offset: 0x00244D9E
	// (set) Token: 0x06006207 RID: 25095 RVA: 0x00246BA6 File Offset: 0x00244DA6
	public string title
	{
		get
		{
			return this._title;
		}
		set
		{
			this._title = value;
		}
	}

	// Token: 0x17000710 RID: 1808
	// (get) Token: 0x06006208 RID: 25096 RVA: 0x00246BAF File Offset: 0x00244DAF
	// (set) Token: 0x06006209 RID: 25097 RVA: 0x00246BB7 File Offset: 0x00244DB7
	public string name
	{
		get
		{
			return this._name;
		}
		set
		{
			this._name = value;
		}
	}

	// Token: 0x17000711 RID: 1809
	// (get) Token: 0x0600620A RID: 25098 RVA: 0x00246BC0 File Offset: 0x00244DC0
	// (set) Token: 0x0600620B RID: 25099 RVA: 0x00246BC8 File Offset: 0x00244DC8
	public string subtitle
	{
		get
		{
			return this._subtitle;
		}
		set
		{
			this._subtitle = value;
		}
	}

	// Token: 0x17000712 RID: 1810
	// (get) Token: 0x0600620C RID: 25100 RVA: 0x00246BD1 File Offset: 0x00244DD1
	// (set) Token: 0x0600620D RID: 25101 RVA: 0x00246BD9 File Offset: 0x00244DD9
	public List<SubEntry> subEntries
	{
		get
		{
			return this._subEntries;
		}
		set
		{
			this._subEntries = value;
		}
	}

	// Token: 0x17000713 RID: 1811
	// (get) Token: 0x0600620E RID: 25102 RVA: 0x00246BE2 File Offset: 0x00244DE2
	// (set) Token: 0x0600620F RID: 25103 RVA: 0x00246BEA File Offset: 0x00244DEA
	public List<CodexEntry_MadeAndUsed> contentMadeAndUsed
	{
		get
		{
			return this._contentMadeAndUsed;
		}
		set
		{
			this._contentMadeAndUsed = value;
		}
	}

	// Token: 0x17000714 RID: 1812
	// (get) Token: 0x06006210 RID: 25104 RVA: 0x00246BF3 File Offset: 0x00244DF3
	// (set) Token: 0x06006211 RID: 25105 RVA: 0x00246BFB File Offset: 0x00244DFB
	public Sprite icon
	{
		get
		{
			return this._icon;
		}
		set
		{
			this._icon = value;
		}
	}

	// Token: 0x17000715 RID: 1813
	// (get) Token: 0x06006212 RID: 25106 RVA: 0x00246C04 File Offset: 0x00244E04
	// (set) Token: 0x06006213 RID: 25107 RVA: 0x00246C0C File Offset: 0x00244E0C
	public Color iconColor
	{
		get
		{
			return this._iconColor;
		}
		set
		{
			this._iconColor = value;
		}
	}

	// Token: 0x17000716 RID: 1814
	// (get) Token: 0x06006214 RID: 25108 RVA: 0x00246C15 File Offset: 0x00244E15
	// (set) Token: 0x06006215 RID: 25109 RVA: 0x00246C1D File Offset: 0x00244E1D
	public string iconPrefabID
	{
		get
		{
			return this._iconPrefabID;
		}
		set
		{
			this._iconPrefabID = value;
		}
	}

	// Token: 0x17000717 RID: 1815
	// (get) Token: 0x06006216 RID: 25110 RVA: 0x00246C26 File Offset: 0x00244E26
	// (set) Token: 0x06006217 RID: 25111 RVA: 0x00246C2E File Offset: 0x00244E2E
	public string iconLockID
	{
		get
		{
			return this._iconLockID;
		}
		set
		{
			this._iconLockID = value;
		}
	}

	// Token: 0x17000718 RID: 1816
	// (get) Token: 0x06006218 RID: 25112 RVA: 0x00246C37 File Offset: 0x00244E37
	// (set) Token: 0x06006219 RID: 25113 RVA: 0x00246C3F File Offset: 0x00244E3F
	public string iconAssetName
	{
		get
		{
			return this._iconAssetName;
		}
		set
		{
			this._iconAssetName = value;
		}
	}

	// Token: 0x17000719 RID: 1817
	// (get) Token: 0x0600621A RID: 25114 RVA: 0x00246C48 File Offset: 0x00244E48
	// (set) Token: 0x0600621B RID: 25115 RVA: 0x00246C50 File Offset: 0x00244E50
	public bool disabled
	{
		get
		{
			return this._disabled;
		}
		set
		{
			this._disabled = value;
		}
	}

	// Token: 0x1700071A RID: 1818
	// (get) Token: 0x0600621C RID: 25116 RVA: 0x00246C59 File Offset: 0x00244E59
	// (set) Token: 0x0600621D RID: 25117 RVA: 0x00246C61 File Offset: 0x00244E61
	public bool searchOnly
	{
		get
		{
			return this._searchOnly;
		}
		set
		{
			this._searchOnly = value;
		}
	}

	// Token: 0x1700071B RID: 1819
	// (get) Token: 0x0600621E RID: 25118 RVA: 0x00246C6A File Offset: 0x00244E6A
	// (set) Token: 0x0600621F RID: 25119 RVA: 0x00246C72 File Offset: 0x00244E72
	public int customContentLength
	{
		get
		{
			return this._customContentLength;
		}
		set
		{
			this._customContentLength = value;
		}
	}

	// Token: 0x1700071C RID: 1820
	// (get) Token: 0x06006220 RID: 25120 RVA: 0x00246C7B File Offset: 0x00244E7B
	// (set) Token: 0x06006221 RID: 25121 RVA: 0x00246C83 File Offset: 0x00244E83
	public string sortString
	{
		get
		{
			return this._sortString;
		}
		set
		{
			this._sortString = value;
		}
	}

	// Token: 0x1700071D RID: 1821
	// (get) Token: 0x06006222 RID: 25122 RVA: 0x00246C8C File Offset: 0x00244E8C
	// (set) Token: 0x06006223 RID: 25123 RVA: 0x00246C94 File Offset: 0x00244E94
	public bool showBeforeGeneratedCategoryLinks
	{
		get
		{
			return this._showBeforeGeneratedCategoryLinks;
		}
		set
		{
			this._showBeforeGeneratedCategoryLinks = value;
		}
	}

	// Token: 0x04004261 RID: 16993
	public EntryDevLog log = new EntryDevLog();

	// Token: 0x04004262 RID: 16994
	private List<ContentContainer> _contentContainers = new List<ContentContainer>();

	// Token: 0x04004265 RID: 16997
	private string _id;

	// Token: 0x04004266 RID: 16998
	private string _parentId;

	// Token: 0x04004267 RID: 16999
	private string _category;

	// Token: 0x04004268 RID: 17000
	private string _title;

	// Token: 0x04004269 RID: 17001
	private string _name;

	// Token: 0x0400426A RID: 17002
	private string _subtitle;

	// Token: 0x0400426B RID: 17003
	private List<SubEntry> _subEntries = new List<SubEntry>();

	// Token: 0x0400426C RID: 17004
	private List<CodexEntry_MadeAndUsed> _contentMadeAndUsed = new List<CodexEntry_MadeAndUsed>();

	// Token: 0x0400426D RID: 17005
	private Sprite _icon;

	// Token: 0x0400426E RID: 17006
	private Color _iconColor = Color.white;

	// Token: 0x0400426F RID: 17007
	private string _iconPrefabID;

	// Token: 0x04004270 RID: 17008
	private string _iconLockID;

	// Token: 0x04004271 RID: 17009
	private string _iconAssetName;

	// Token: 0x04004272 RID: 17010
	private bool _disabled;

	// Token: 0x04004273 RID: 17011
	private bool _searchOnly;

	// Token: 0x04004274 RID: 17012
	private int _customContentLength;

	// Token: 0x04004275 RID: 17013
	private string _sortString;

	// Token: 0x04004276 RID: 17014
	private bool _showBeforeGeneratedCategoryLinks;
}
