using System;
using UnityEngine;

namespace Rendering.World
{
	// Token: 0x02000EA5 RID: 3749
	public class DynamicMesh
	{
		// Token: 0x06007800 RID: 30720 RVA: 0x002E86CC File Offset: 0x002E68CC
		public DynamicMesh(string name, Bounds bounds)
		{
			this.Name = name;
			this.Bounds = bounds;
		}

		// Token: 0x06007801 RID: 30721 RVA: 0x002E86F0 File Offset: 0x002E68F0
		public void Reserve(int vertex_count, int triangle_count)
		{
			if (vertex_count > this.VertexCount)
			{
				this.SetUVs = true;
			}
			else
			{
				this.SetUVs = false;
			}
			if (this.TriangleCount != triangle_count)
			{
				this.SetTriangles = true;
			}
			else
			{
				this.SetTriangles = false;
			}
			int num = (int)Mathf.Ceil((float)triangle_count / (float)DynamicMesh.TrianglesPerMesh);
			if (num != this.Meshes.Length)
			{
				this.Meshes = new DynamicSubMesh[num];
				for (int i = 0; i < this.Meshes.Length; i++)
				{
					int num2 = -i * DynamicMesh.VerticesPerMesh;
					this.Meshes[i] = new DynamicSubMesh(this.Name, this.Bounds, num2);
				}
				this.SetUVs = true;
				this.SetTriangles = true;
			}
			for (int j = 0; j < this.Meshes.Length; j++)
			{
				if (j == this.Meshes.Length - 1)
				{
					this.Meshes[j].Reserve(vertex_count % DynamicMesh.VerticesPerMesh, triangle_count % DynamicMesh.TrianglesPerMesh);
				}
				else
				{
					this.Meshes[j].Reserve(DynamicMesh.VerticesPerMesh, DynamicMesh.TrianglesPerMesh);
				}
			}
			this.VertexCount = vertex_count;
			this.TriangleCount = triangle_count;
		}

		// Token: 0x06007802 RID: 30722 RVA: 0x002E87FC File Offset: 0x002E69FC
		public void Commit()
		{
			DynamicSubMesh[] meshes = this.Meshes;
			for (int i = 0; i < meshes.Length; i++)
			{
				meshes[i].Commit();
			}
			this.TriangleMeshIdx = 0;
			this.UVMeshIdx = 0;
			this.VertexMeshIdx = 0;
		}

		// Token: 0x06007803 RID: 30723 RVA: 0x002E883C File Offset: 0x002E6A3C
		public void AddTriangle(int triangle)
		{
			if (this.Meshes[this.TriangleMeshIdx].AreTrianglesFull())
			{
				DynamicSubMesh[] meshes = this.Meshes;
				int num = this.TriangleMeshIdx + 1;
				this.TriangleMeshIdx = num;
				object obj = meshes[num];
			}
			this.Meshes[this.TriangleMeshIdx].AddTriangle(triangle);
		}

		// Token: 0x06007804 RID: 30724 RVA: 0x002E888C File Offset: 0x002E6A8C
		public void AddUV(Vector2 uv)
		{
			DynamicSubMesh dynamicSubMesh = this.Meshes[this.UVMeshIdx];
			if (dynamicSubMesh.AreUVsFull())
			{
				DynamicSubMesh[] meshes = this.Meshes;
				int num = this.UVMeshIdx + 1;
				this.UVMeshIdx = num;
				dynamicSubMesh = meshes[num];
			}
			dynamicSubMesh.AddUV(uv);
		}

		// Token: 0x06007805 RID: 30725 RVA: 0x002E88D0 File Offset: 0x002E6AD0
		public void AddVertex(Vector3 vertex)
		{
			DynamicSubMesh dynamicSubMesh = this.Meshes[this.VertexMeshIdx];
			if (dynamicSubMesh.AreVerticesFull())
			{
				DynamicSubMesh[] meshes = this.Meshes;
				int num = this.VertexMeshIdx + 1;
				this.VertexMeshIdx = num;
				dynamicSubMesh = meshes[num];
			}
			dynamicSubMesh.AddVertex(vertex);
		}

		// Token: 0x06007806 RID: 30726 RVA: 0x002E8914 File Offset: 0x002E6B14
		public void Render(Vector3 position, Quaternion rotation, Material material, int layer, MaterialPropertyBlock property_block)
		{
			DynamicSubMesh[] meshes = this.Meshes;
			for (int i = 0; i < meshes.Length; i++)
			{
				meshes[i].Render(position, rotation, material, layer, property_block);
			}
		}

		// Token: 0x0400534C RID: 21324
		private static int TrianglesPerMesh = 65004;

		// Token: 0x0400534D RID: 21325
		private static int VerticesPerMesh = 4 * DynamicMesh.TrianglesPerMesh / 6;

		// Token: 0x0400534E RID: 21326
		public bool SetUVs;

		// Token: 0x0400534F RID: 21327
		public bool SetTriangles;

		// Token: 0x04005350 RID: 21328
		public string Name;

		// Token: 0x04005351 RID: 21329
		public Bounds Bounds;

		// Token: 0x04005352 RID: 21330
		public DynamicSubMesh[] Meshes = new DynamicSubMesh[0];

		// Token: 0x04005353 RID: 21331
		private int VertexCount;

		// Token: 0x04005354 RID: 21332
		private int TriangleCount;

		// Token: 0x04005355 RID: 21333
		private int VertexIdx;

		// Token: 0x04005356 RID: 21334
		private int UVIdx;

		// Token: 0x04005357 RID: 21335
		private int TriangleIdx;

		// Token: 0x04005358 RID: 21336
		private int TriangleMeshIdx;

		// Token: 0x04005359 RID: 21337
		private int VertexMeshIdx;

		// Token: 0x0400535A RID: 21338
		private int UVMeshIdx;
	}
}
