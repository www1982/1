using System;
using UnityEngine;

// Token: 0x0200052E RID: 1326
public class KBatchedAnimTracker : MonoBehaviour
{
	// Token: 0x06001D3A RID: 7482 RVA: 0x0009DF18 File Offset: 0x0009C118
	private void Start()
	{
		if (this.controller == null)
		{
			Transform transform = base.transform.parent;
			while (transform != null)
			{
				this.controller = transform.GetComponent<KBatchedAnimController>();
				if (this.controller != null)
				{
					break;
				}
				transform = transform.parent;
			}
		}
		if (this.controller == null)
		{
			global::Debug.Log("Controller Null for tracker on " + base.gameObject.name, base.gameObject);
			base.enabled = false;
			return;
		}
		this.controller.onAnimEnter += this.OnAnimStart;
		this.controller.onAnimComplete += this.OnAnimStop;
		this.controller.onLayerChanged += this.OnLayerChanged;
		this.forceUpdate = true;
		if (this.myAnim != null)
		{
			return;
		}
		this.myAnim = base.GetComponent<KBatchedAnimController>();
		KBatchedAnimController kbatchedAnimController = this.myAnim;
		kbatchedAnimController.getPositionDataFunctionInUse = (Func<Vector4>)Delegate.Combine(kbatchedAnimController.getPositionDataFunctionInUse, new Func<Vector4>(this.MyAnimGetPosition));
	}

	// Token: 0x06001D3B RID: 7483 RVA: 0x0009E030 File Offset: 0x0009C230
	private Vector4 MyAnimGetPosition()
	{
		if (this.myAnim != null && this.controller != null && this.controller.transform == this.myAnim.transform.parent)
		{
			Vector3 pivotSymbolPosition = this.myAnim.GetPivotSymbolPosition();
			return new Vector4(pivotSymbolPosition.x - this.controller.Offset.x, pivotSymbolPosition.y - this.controller.Offset.y, pivotSymbolPosition.x, pivotSymbolPosition.y);
		}
		return base.transform.GetPosition();
	}

	// Token: 0x06001D3C RID: 7484 RVA: 0x0009E0D8 File Offset: 0x0009C2D8
	private void OnDestroy()
	{
		if (this.controller != null)
		{
			this.controller.onAnimEnter -= this.OnAnimStart;
			this.controller.onAnimComplete -= this.OnAnimStop;
			this.controller.onLayerChanged -= this.OnLayerChanged;
			this.controller = null;
		}
		if (this.myAnim != null)
		{
			KBatchedAnimController kbatchedAnimController = this.myAnim;
			kbatchedAnimController.getPositionDataFunctionInUse = (Func<Vector4>)Delegate.Remove(kbatchedAnimController.getPositionDataFunctionInUse, new Func<Vector4>(this.MyAnimGetPosition));
		}
		this.myAnim = null;
	}

	// Token: 0x06001D3D RID: 7485 RVA: 0x0009E17C File Offset: 0x0009C37C
	private void LateUpdate()
	{
		if (this.controller != null && (this.controller.IsVisible() || this.forceAlwaysVisible || this.forceUpdate))
		{
			this.UpdateFrame();
		}
		if (!this.alive)
		{
			base.enabled = false;
		}
	}

	// Token: 0x06001D3E RID: 7486 RVA: 0x0009E1C9 File Offset: 0x0009C3C9
	public void SetAnimControllers(KBatchedAnimController controller, KBatchedAnimController parentController)
	{
		this.myAnim = controller;
		this.controller = parentController;
	}

	// Token: 0x06001D3F RID: 7487 RVA: 0x0009E1DC File Offset: 0x0009C3DC
	private void UpdateFrame()
	{
		this.forceUpdate = false;
		bool flag = false;
		if (this.controller.CurrentAnim != null)
		{
			Matrix2x3 symbolLocalTransform = this.controller.GetSymbolLocalTransform(this.symbol, out flag);
			Vector3 position = this.controller.transform.GetPosition();
			if (flag && (this.previousMatrix != symbolLocalTransform || position != this.previousPosition || (this.useTargetPoint && this.targetPoint != this.previousTargetPoint) || (this.matchParentOffset && this.myAnim.Offset != this.controller.Offset)))
			{
				this.previousMatrix = symbolLocalTransform;
				this.previousPosition = position;
				Matrix2x3 matrix2x = ((this.useTargetPoint || this.myAnim == null) ? this.controller.GetTransformMatrix() : this.controller.GetTransformMatrix(new Vector2(this.myAnim.animWidth * this.myAnim.animScale, -this.myAnim.animHeight * this.myAnim.animScale))) * symbolLocalTransform;
				float z = base.transform.GetPosition().z;
				base.transform.SetPosition(matrix2x.MultiplyPoint(this.offset));
				if (this.useTargetPoint)
				{
					this.previousTargetPoint = this.targetPoint;
					Vector3 position2 = base.transform.GetPosition();
					position2.z = 0f;
					Vector3 vector = this.targetPoint - position2;
					float num = Vector3.Angle(vector, Vector3.right);
					if (vector.y < 0f)
					{
						num = 360f - num;
					}
					base.transform.localRotation = Quaternion.identity;
					base.transform.RotateAround(position2, new Vector3(0f, 0f, 1f), num);
					float sqrMagnitude = vector.sqrMagnitude;
					this.myAnim.GetBatchInstanceData().SetClipRadius(base.transform.GetPosition().x, base.transform.GetPosition().y, sqrMagnitude, true);
				}
				else
				{
					Vector3 vector2 = (this.controller.FlipX ? Vector3.left : Vector3.right);
					Vector3 vector3 = (this.controller.FlipY ? Vector3.down : Vector3.up);
					base.transform.up = matrix2x.MultiplyVector(vector3);
					base.transform.right = matrix2x.MultiplyVector(vector2);
					if (this.myAnim != null)
					{
						KBatchedAnimInstanceData batchInstanceData = this.myAnim.GetBatchInstanceData();
						if (batchInstanceData != null)
						{
							batchInstanceData.SetOverrideTransformMatrix(matrix2x);
						}
					}
				}
				base.transform.SetPosition(new Vector3(base.transform.GetPosition().x, base.transform.GetPosition().y, z));
				if (this.matchParentOffset)
				{
					this.myAnim.Offset = this.controller.Offset;
				}
				this.myAnim.SetDirty();
			}
		}
		if (this.myAnim != null && flag != this.myAnim.enabled && this.synchronizeEnabledState)
		{
			this.myAnim.enabled = flag;
		}
	}

	// Token: 0x06001D40 RID: 7488 RVA: 0x0009E51E File Offset: 0x0009C71E
	[ContextMenu("ForceAlive")]
	private void OnAnimStart(HashedString name)
	{
		this.alive = true;
		base.enabled = true;
		this.forceUpdate = true;
	}

	// Token: 0x06001D41 RID: 7489 RVA: 0x0009E535 File Offset: 0x0009C735
	private void OnAnimStop(HashedString name)
	{
		if (!this.forceAlwaysAlive)
		{
			this.alive = false;
		}
	}

	// Token: 0x06001D42 RID: 7490 RVA: 0x0009E546 File Offset: 0x0009C746
	private void OnLayerChanged(int layer)
	{
		this.myAnim.SetLayer(layer);
	}

	// Token: 0x06001D43 RID: 7491 RVA: 0x0009E554 File Offset: 0x0009C754
	public void SetTarget(Vector3 target)
	{
		this.targetPoint = target;
		this.targetPoint.z = 0f;
	}

	// Token: 0x04001105 RID: 4357
	public KBatchedAnimController controller;

	// Token: 0x04001106 RID: 4358
	public Vector3 offset = Vector3.zero;

	// Token: 0x04001107 RID: 4359
	public HashedString symbol;

	// Token: 0x04001108 RID: 4360
	public Vector3 targetPoint = Vector3.zero;

	// Token: 0x04001109 RID: 4361
	public Vector3 previousTargetPoint;

	// Token: 0x0400110A RID: 4362
	public bool useTargetPoint;

	// Token: 0x0400110B RID: 4363
	public bool fadeOut = true;

	// Token: 0x0400110C RID: 4364
	public bool forceAlwaysVisible;

	// Token: 0x0400110D RID: 4365
	public bool matchParentOffset;

	// Token: 0x0400110E RID: 4366
	public bool forceAlwaysAlive;

	// Token: 0x0400110F RID: 4367
	private bool alive = true;

	// Token: 0x04001110 RID: 4368
	private bool forceUpdate;

	// Token: 0x04001111 RID: 4369
	private Matrix2x3 previousMatrix;

	// Token: 0x04001112 RID: 4370
	private Vector3 previousPosition;

	// Token: 0x04001113 RID: 4371
	public bool synchronizeEnabledState = true;

	// Token: 0x04001114 RID: 4372
	[SerializeField]
	private KBatchedAnimController myAnim;
}
