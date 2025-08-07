using System;
using UnityEngine;

namespace Rendering.World
{
	// Token: 0x02000EA6 RID: 3750
	public class DynamicSubMesh
	{
		// Token: 0x06007808 RID: 30728 RVA: 0x002E8960 File Offset: 0x002E6B60
		public DynamicSubMesh(string name, Bounds bounds, int idx_offset)
		{
			this.IdxOffset = idx_offset;
			this.Mesh = new Mesh();
			this.Mesh.name = name;
			this.Mesh.bounds = bounds;
			this.Mesh.MarkDynamic();
		}

		// Token: 0x06007809 RID: 30729 RVA: 0x002E89CC File Offset: 0x002E6BCC
		public void Reserve(int vertex_count, int triangle_count)
		{
			if (vertex_count > this.Vertices.Length)
			{
				this.Vertices = new Vector3[vertex_count];
				this.UVs = new Vector2[vertex_count];
				this.SetUVs = true;
			}
			else
			{
				this.SetUVs = false;
			}
			if (this.Triangles.Length != triangle_count)
			{
				this.Triangles = new int[triangle_count];
				this.SetTriangles = true;
				return;
			}
			this.SetTriangles = false;
		}

		// Token: 0x0600780A RID: 30730 RVA: 0x002E8A32 File Offset: 0x002E6C32
		public bool AreTrianglesFull()
		{
			return this.Triangles.Length == this.TriangleIdx;
		}

		// Token: 0x0600780B RID: 30731 RVA: 0x002E8A44 File Offset: 0x002E6C44
		public bool AreVerticesFull()
		{
			return this.Vertices.Length == this.VertexIdx;
		}

		// Token: 0x0600780C RID: 30732 RVA: 0x002E8A56 File Offset: 0x002E6C56
		public bool AreUVsFull()
		{
			return this.UVs.Length == this.UVIdx;
		}

		// Token: 0x0600780D RID: 30733 RVA: 0x002E8A68 File Offset: 0x002E6C68
		public void Commit()
		{
			if (this.SetTriangles)
			{
				this.Mesh.Clear();
			}
			this.Mesh.vertices = this.Vertices;
			if (this.SetUVs || this.SetTriangles)
			{
				this.Mesh.uv = this.UVs;
			}
			if (this.SetTriangles)
			{
				this.Mesh.triangles = this.Triangles;
			}
			this.VertexIdx = 0;
			this.UVIdx = 0;
			this.TriangleIdx = 0;
		}

		// Token: 0x0600780E RID: 30734 RVA: 0x002E8AE8 File Offset: 0x002E6CE8
		public void AddTriangle(int triangle)
		{
			int[] triangles = this.Triangles;
			int triangleIdx = this.TriangleIdx;
			this.TriangleIdx = triangleIdx + 1;
			triangles[triangleIdx] = triangle + this.IdxOffset;
		}

		// Token: 0x0600780F RID: 30735 RVA: 0x002E8B18 File Offset: 0x002E6D18
		public void AddUV(Vector2 uv)
		{
			Vector2[] uvs = this.UVs;
			int uvidx = this.UVIdx;
			this.UVIdx = uvidx + 1;
			uvs[uvidx] = uv;
		}

		// Token: 0x06007810 RID: 30736 RVA: 0x002E8B44 File Offset: 0x002E6D44
		public void AddVertex(Vector3 vertex)
		{
			Vector3[] vertices = this.Vertices;
			int vertexIdx = this.VertexIdx;
			this.VertexIdx = vertexIdx + 1;
			vertices[vertexIdx] = vertex;
		}

		// Token: 0x06007811 RID: 30737 RVA: 0x002E8B70 File Offset: 0x002E6D70
		public void Render(Vector3 position, Quaternion rotation, Material material, int layer, MaterialPropertyBlock property_block)
		{
			Graphics.DrawMesh(this.Mesh, position, rotation, material, layer, null, 0, property_block, false, false);
		}

		// Token: 0x0400535B RID: 21339
		public Vector3[] Vertices = new Vector3[0];

		// Token: 0x0400535C RID: 21340
		public Vector2[] UVs = new Vector2[0];

		// Token: 0x0400535D RID: 21341
		public int[] Triangles = new int[0];

		// Token: 0x0400535E RID: 21342
		public Mesh Mesh;

		// Token: 0x0400535F RID: 21343
		public bool SetUVs;

		// Token: 0x04005360 RID: 21344
		public bool SetTriangles;

		// Token: 0x04005361 RID: 21345
		private int VertexIdx;

		// Token: 0x04005362 RID: 21346
		private int UVIdx;

		// Token: 0x04005363 RID: 21347
		private int TriangleIdx;

		// Token: 0x04005364 RID: 21348
		private int IdxOffset;
	}
}
