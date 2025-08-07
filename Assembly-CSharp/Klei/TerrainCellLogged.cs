using System;
using System.Collections.Generic;
using ProcGen.Map;
using ProcGenGame;
using VoronoiTree;

namespace Klei
{
	// Token: 0x02000FC3 RID: 4035
	public class TerrainCellLogged : TerrainCell
	{
		// Token: 0x06007CB2 RID: 31922 RVA: 0x0031FB6A File Offset: 0x0031DD6A
		public TerrainCellLogged()
		{
		}

		// Token: 0x06007CB3 RID: 31923 RVA: 0x0031FB72 File Offset: 0x0031DD72
		public TerrainCellLogged(Cell node, Diagram.Site site, Dictionary<Tag, int> distancesToTags)
			: base(node, site, distancesToTags)
		{
		}

		// Token: 0x06007CB4 RID: 31924 RVA: 0x0031FB7D File Offset: 0x0031DD7D
		public override void LogInfo(string evt, string param, float value)
		{
		}
	}
}
