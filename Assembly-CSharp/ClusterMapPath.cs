using System;
using System.Collections.Generic;
using System.Linq;
using ProcGen;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

// Token: 0x02000C75 RID: 3189
public class ClusterMapPath : MonoBehaviour
{
	// Token: 0x06006170 RID: 24944 RVA: 0x00242E94 File Offset: 0x00241094
	public void Init()
	{
		this.lineRenderer = base.gameObject.GetComponentInChildren<UILineRenderer>();
		base.gameObject.SetActive(true);
	}

	// Token: 0x06006171 RID: 24945 RVA: 0x00242EB3 File Offset: 0x002410B3
	public void Init(List<Vector2> nodes, Color color)
	{
		this.m_nodes = nodes;
		this.m_color = color;
		this.lineRenderer = base.gameObject.GetComponentInChildren<UILineRenderer>();
		this.UpdateColor();
		this.UpdateRenderer();
		base.gameObject.SetActive(true);
	}

	// Token: 0x06006172 RID: 24946 RVA: 0x00242EEC File Offset: 0x002410EC
	public void SetColor(Color color)
	{
		this.m_color = color;
		this.UpdateColor();
	}

	// Token: 0x06006173 RID: 24947 RVA: 0x00242EFB File Offset: 0x002410FB
	private void UpdateColor()
	{
		this.lineRenderer.color = this.m_color;
		this.pathStart.color = this.m_color;
		this.pathEnd.color = this.m_color;
	}

	// Token: 0x06006174 RID: 24948 RVA: 0x00242F30 File Offset: 0x00241130
	public void SetPoints(List<Vector2> points)
	{
		this.m_nodes = points;
		this.UpdateRenderer();
	}

	// Token: 0x06006175 RID: 24949 RVA: 0x00242F40 File Offset: 0x00241140
	private void UpdateRenderer()
	{
		HashSet<Vector2> pointsOnCatmullRomSpline = global::ProcGen.Util.GetPointsOnCatmullRomSpline(this.m_nodes, 10);
		this.lineRenderer.Points = pointsOnCatmullRomSpline.ToArray<Vector2>();
		if (this.lineRenderer.Points.Length > 1)
		{
			this.pathStart.transform.localPosition = this.lineRenderer.Points[0];
			this.pathStart.gameObject.SetActive(true);
			Vector2 vector = this.lineRenderer.Points[this.lineRenderer.Points.Length - 1];
			Vector2 vector2 = this.lineRenderer.Points[this.lineRenderer.Points.Length - 2];
			this.pathEnd.transform.localPosition = vector;
			Vector2 vector3 = vector - vector2;
			this.pathEnd.transform.rotation = Quaternion.LookRotation(Vector3.forward, vector3);
			this.pathEnd.gameObject.SetActive(true);
			return;
		}
		this.pathStart.gameObject.SetActive(false);
		this.pathEnd.gameObject.SetActive(false);
	}

	// Token: 0x06006176 RID: 24950 RVA: 0x00243068 File Offset: 0x00241268
	public float GetRotationForNextSegment()
	{
		if (this.m_nodes.Count > 1)
		{
			Vector2 vector = this.m_nodes[0];
			Vector2 vector2 = this.m_nodes[1] - vector;
			return Vector2.SignedAngle(Vector2.up, vector2);
		}
		return 0f;
	}

	// Token: 0x04004209 RID: 16905
	private List<Vector2> m_nodes;

	// Token: 0x0400420A RID: 16906
	private Color m_color;

	// Token: 0x0400420B RID: 16907
	public UILineRenderer lineRenderer;

	// Token: 0x0400420C RID: 16908
	public Image pathStart;

	// Token: 0x0400420D RID: 16909
	public Image pathEnd;
}
