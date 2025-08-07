using System;

// Token: 0x02000745 RID: 1861
public class HEPBridgeTileVisualizer : KMonoBehaviour, IHighEnergyParticleDirection
{
	// Token: 0x06002F29 RID: 12073 RVA: 0x0010E890 File Offset: 0x0010CA90
	protected override void OnSpawn()
	{
		base.Subscribe<HEPBridgeTileVisualizer>(-1643076535, HEPBridgeTileVisualizer.OnRotateDelegate);
		this.OnRotate();
	}

	// Token: 0x06002F2A RID: 12074 RVA: 0x0010E8A9 File Offset: 0x0010CAA9
	public void OnRotate()
	{
		Game.Instance.ForceOverlayUpdate(true);
	}

	// Token: 0x17000281 RID: 641
	// (get) Token: 0x06002F2B RID: 12075 RVA: 0x0010E8B8 File Offset: 0x0010CAB8
	// (set) Token: 0x06002F2C RID: 12076 RVA: 0x0010E905 File Offset: 0x0010CB05
	public EightDirection Direction
	{
		get
		{
			EightDirection eightDirection = EightDirection.Right;
			Rotatable component = base.GetComponent<Rotatable>();
			if (component != null)
			{
				switch (component.Orientation)
				{
				case Orientation.Neutral:
					eightDirection = EightDirection.Left;
					break;
				case Orientation.R90:
					eightDirection = EightDirection.Up;
					break;
				case Orientation.R180:
					eightDirection = EightDirection.Right;
					break;
				case Orientation.R270:
					eightDirection = EightDirection.Down;
					break;
				}
			}
			return eightDirection;
		}
		set
		{
		}
	}

	// Token: 0x04001BE6 RID: 7142
	private static readonly EventSystem.IntraObjectHandler<HEPBridgeTileVisualizer> OnRotateDelegate = new EventSystem.IntraObjectHandler<HEPBridgeTileVisualizer>(delegate(HEPBridgeTileVisualizer component, object data)
	{
		component.OnRotate();
	});
}
