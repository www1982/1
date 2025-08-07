using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000BAD RID: 2989
public class SubstanceTable : ScriptableObject, ISerializationCallbackReceiver
{
	// Token: 0x06005960 RID: 22880 RVA: 0x00204BE5 File Offset: 0x00202DE5
	public List<Substance> GetList()
	{
		return this.list;
	}

	// Token: 0x06005961 RID: 22881 RVA: 0x00204BF0 File Offset: 0x00202DF0
	public Substance GetSubstance(SimHashes substance)
	{
		int count = this.list.Count;
		for (int i = 0; i < count; i++)
		{
			if (this.list[i].elementID == substance)
			{
				return this.list[i];
			}
		}
		return null;
	}

	// Token: 0x06005962 RID: 22882 RVA: 0x00204C37 File Offset: 0x00202E37
	public void OnBeforeSerialize()
	{
		this.BindAnimList();
	}

	// Token: 0x06005963 RID: 22883 RVA: 0x00204C3F File Offset: 0x00202E3F
	public void OnAfterDeserialize()
	{
		this.BindAnimList();
	}

	// Token: 0x06005964 RID: 22884 RVA: 0x00204C48 File Offset: 0x00202E48
	private void BindAnimList()
	{
		foreach (Substance substance in this.list)
		{
			if (substance.anim != null && (substance.anims == null || substance.anims.Length == 0))
			{
				substance.anims = new KAnimFile[1];
				substance.anims[0] = substance.anim;
			}
		}
	}

	// Token: 0x06005965 RID: 22885 RVA: 0x00204CD0 File Offset: 0x00202ED0
	public void RemoveDuplicates()
	{
		this.list = this.list.Distinct(new SubstanceTable.SubstanceEqualityComparer()).ToList<Substance>();
	}

	// Token: 0x04003B55 RID: 15189
	[SerializeField]
	private List<Substance> list;

	// Token: 0x04003B56 RID: 15190
	public Material solidMaterial;

	// Token: 0x04003B57 RID: 15191
	public Material liquidMaterial;

	// Token: 0x02001CE0 RID: 7392
	private class SubstanceEqualityComparer : IEqualityComparer<Substance>
	{
		// Token: 0x0600AC65 RID: 44133 RVA: 0x003C1AF1 File Offset: 0x003BFCF1
		public bool Equals(Substance x, Substance y)
		{
			return x.elementID.Equals(y.elementID);
		}

		// Token: 0x0600AC66 RID: 44134 RVA: 0x003C1B0F File Offset: 0x003BFD0F
		public int GetHashCode(Substance obj)
		{
			return obj.elementID.GetHashCode();
		}
	}
}
