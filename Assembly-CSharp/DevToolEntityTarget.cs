using System;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000669 RID: 1641
public abstract class DevToolEntityTarget
{
	// Token: 0x0600283B RID: 10299
	public abstract string GetTag();

	// Token: 0x0600283C RID: 10300
	[return: TupleElementNames(new string[] { "cornerA", "cornerB" })]
	public abstract Option<ValueTuple<Vector2, Vector2>> GetScreenRect();

	// Token: 0x0600283D RID: 10301 RVA: 0x000E5067 File Offset: 0x000E3267
	public string GetDebugName()
	{
		return "[" + this.GetTag() + "] " + this.ToString();
	}

	// Token: 0x020014EE RID: 5358
	public class ForUIGameObject : DevToolEntityTarget
	{
		// Token: 0x06008F5D RID: 36701 RVA: 0x0035D6A9 File Offset: 0x0035B8A9
		public ForUIGameObject(GameObject gameObject)
		{
			this.gameObject = gameObject;
		}

		// Token: 0x06008F5E RID: 36702 RVA: 0x0035D6B8 File Offset: 0x0035B8B8
		[return: TupleElementNames(new string[] { "cornerA", "cornerB" })]
		public override Option<ValueTuple<Vector2, Vector2>> GetScreenRect()
		{
			if (this.gameObject.IsNullOrDestroyed())
			{
				return Option.None;
			}
			RectTransform component = this.gameObject.GetComponent<RectTransform>();
			if (component.IsNullOrDestroyed())
			{
				return Option.None;
			}
			Canvas componentInParent = this.gameObject.GetComponentInParent<Canvas>();
			if (component.IsNullOrDestroyed())
			{
				return Option.None;
			}
			if (!componentInParent.worldCamera.IsNullOrDestroyed())
			{
				DevToolEntityTarget.ForUIGameObject.<>c__DisplayClass2_0 CS$<>8__locals1;
				CS$<>8__locals1.camera = componentInParent.worldCamera;
				Vector3[] array = new Vector3[4];
				component.GetWorldCorners(array);
				return new ValueTuple<Vector2, Vector2>(DevToolEntityTarget.ForUIGameObject.<GetScreenRect>g__ScreenPointToScreenPosition|2_0(CS$<>8__locals1.camera.WorldToScreenPoint(array[0]), ref CS$<>8__locals1), DevToolEntityTarget.ForUIGameObject.<GetScreenRect>g__ScreenPointToScreenPosition|2_0(CS$<>8__locals1.camera.WorldToScreenPoint(array[2]), ref CS$<>8__locals1));
			}
			if (componentInParent.renderMode == RenderMode.ScreenSpaceOverlay)
			{
				Vector3[] array2 = new Vector3[4];
				component.GetWorldCorners(array2);
				return new ValueTuple<Vector2, Vector2>(DevToolEntityTarget.ForUIGameObject.<GetScreenRect>g__ScreenPointToScreenPosition|2_1(array2[0]), DevToolEntityTarget.ForUIGameObject.<GetScreenRect>g__ScreenPointToScreenPosition|2_1(array2[2]));
			}
			return Option.None;
		}

		// Token: 0x06008F5F RID: 36703 RVA: 0x0035D7DB File Offset: 0x0035B9DB
		public override string GetTag()
		{
			return "UI";
		}

		// Token: 0x06008F60 RID: 36704 RVA: 0x0035D7E2 File Offset: 0x0035B9E2
		public override string ToString()
		{
			return DevToolEntity.GetNameFor(this.gameObject);
		}

		// Token: 0x06008F61 RID: 36705 RVA: 0x0035D7EF File Offset: 0x0035B9EF
		[CompilerGenerated]
		internal static Vector2 <GetScreenRect>g__ScreenPointToScreenPosition|2_0(Vector2 coord, ref DevToolEntityTarget.ForUIGameObject.<>c__DisplayClass2_0 A_1)
		{
			return new Vector2(coord.x, (float)A_1.camera.pixelHeight - coord.y);
		}

		// Token: 0x06008F62 RID: 36706 RVA: 0x0035D80F File Offset: 0x0035BA0F
		[CompilerGenerated]
		internal static Vector2 <GetScreenRect>g__ScreenPointToScreenPosition|2_1(Vector2 coord)
		{
			return new Vector2(coord.x, (float)Screen.height - coord.y);
		}

		// Token: 0x04006E51 RID: 28241
		public GameObject gameObject;
	}

	// Token: 0x020014EF RID: 5359
	public class ForWorldGameObject : DevToolEntityTarget
	{
		// Token: 0x06008F63 RID: 36707 RVA: 0x0035D829 File Offset: 0x0035BA29
		public ForWorldGameObject(GameObject gameObject)
		{
			this.gameObject = gameObject;
		}

		// Token: 0x06008F64 RID: 36708 RVA: 0x0035D838 File Offset: 0x0035BA38
		[return: TupleElementNames(new string[] { "cornerA", "cornerB" })]
		public override Option<ValueTuple<Vector2, Vector2>> GetScreenRect()
		{
			if (this.gameObject.IsNullOrDestroyed())
			{
				return Option.None;
			}
			DevToolEntityTarget.ForWorldGameObject.<>c__DisplayClass2_0 CS$<>8__locals1;
			CS$<>8__locals1.camera = Camera.main;
			if (CS$<>8__locals1.camera.IsNullOrDestroyed())
			{
				return Option.None;
			}
			KCollider2D component = this.gameObject.GetComponent<KCollider2D>();
			if (component.IsNullOrDestroyed())
			{
				return Option.None;
			}
			return new ValueTuple<Vector2, Vector2>(DevToolEntityTarget.ForWorldGameObject.<GetScreenRect>g__ScreenPointToScreenPosition|2_0(CS$<>8__locals1.camera.WorldToScreenPoint(component.bounds.min), ref CS$<>8__locals1), DevToolEntityTarget.ForWorldGameObject.<GetScreenRect>g__ScreenPointToScreenPosition|2_0(CS$<>8__locals1.camera.WorldToScreenPoint(component.bounds.max), ref CS$<>8__locals1));
		}

		// Token: 0x06008F65 RID: 36709 RVA: 0x0035D8F4 File Offset: 0x0035BAF4
		public override string GetTag()
		{
			return "World";
		}

		// Token: 0x06008F66 RID: 36710 RVA: 0x0035D8FB File Offset: 0x0035BAFB
		public override string ToString()
		{
			return DevToolEntity.GetNameFor(this.gameObject);
		}

		// Token: 0x06008F67 RID: 36711 RVA: 0x0035D908 File Offset: 0x0035BB08
		[CompilerGenerated]
		internal static Vector2 <GetScreenRect>g__ScreenPointToScreenPosition|2_0(Vector2 coord, ref DevToolEntityTarget.ForWorldGameObject.<>c__DisplayClass2_0 A_1)
		{
			return new Vector2(coord.x, (float)A_1.camera.pixelHeight - coord.y);
		}

		// Token: 0x04006E52 RID: 28242
		public GameObject gameObject;
	}

	// Token: 0x020014F0 RID: 5360
	public class ForSimCell : DevToolEntityTarget
	{
		// Token: 0x06008F68 RID: 36712 RVA: 0x0035D928 File Offset: 0x0035BB28
		public ForSimCell(int cellIndex)
		{
			this.cellIndex = cellIndex;
		}

		// Token: 0x06008F69 RID: 36713 RVA: 0x0035D938 File Offset: 0x0035BB38
		[return: TupleElementNames(new string[] { "cornerA", "cornerB" })]
		public override Option<ValueTuple<Vector2, Vector2>> GetScreenRect()
		{
			DevToolEntityTarget.ForSimCell.<>c__DisplayClass2_0 CS$<>8__locals1;
			CS$<>8__locals1.camera = Camera.main;
			if (CS$<>8__locals1.camera.IsNullOrDestroyed())
			{
				return Option.None;
			}
			Vector2 vector = Grid.CellToPosCCC(this.cellIndex, Grid.SceneLayer.Background);
			Vector2 vector2 = Grid.HalfCellSizeInMeters * Vector2.one;
			Vector2 vector3 = vector - vector2;
			Vector2 vector4 = vector + vector2;
			return new ValueTuple<Vector2, Vector2>(DevToolEntityTarget.ForSimCell.<GetScreenRect>g__ScreenPointToScreenPosition|2_0(CS$<>8__locals1.camera.WorldToScreenPoint(vector3), ref CS$<>8__locals1), DevToolEntityTarget.ForSimCell.<GetScreenRect>g__ScreenPointToScreenPosition|2_0(CS$<>8__locals1.camera.WorldToScreenPoint(vector4), ref CS$<>8__locals1));
		}

		// Token: 0x06008F6A RID: 36714 RVA: 0x0035D9DD File Offset: 0x0035BBDD
		public override string GetTag()
		{
			return "Sim Cell";
		}

		// Token: 0x06008F6B RID: 36715 RVA: 0x0035D9E4 File Offset: 0x0035BBE4
		public override string ToString()
		{
			return this.cellIndex.ToString();
		}

		// Token: 0x06008F6C RID: 36716 RVA: 0x0035D9F1 File Offset: 0x0035BBF1
		[CompilerGenerated]
		internal static Vector2 <GetScreenRect>g__ScreenPointToScreenPosition|2_0(Vector2 coord, ref DevToolEntityTarget.ForSimCell.<>c__DisplayClass2_0 A_1)
		{
			return new Vector2(coord.x, (float)A_1.camera.pixelHeight - coord.y);
		}

		// Token: 0x04006E53 RID: 28243
		public int cellIndex;
	}
}
