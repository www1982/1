using System;

// Token: 0x02000C14 RID: 3092
public class DebugOverlays : KScreen
{
	// Token: 0x170006E2 RID: 1762
	// (get) Token: 0x06005DAB RID: 23979 RVA: 0x00224163 File Offset: 0x00222363
	// (set) Token: 0x06005DAC RID: 23980 RVA: 0x0022416A File Offset: 0x0022236A
	public static DebugOverlays instance { get; private set; }

	// Token: 0x06005DAD RID: 23981 RVA: 0x00224174 File Offset: 0x00222374
	protected override void OnPrefabInit()
	{
		DebugOverlays.instance = this;
		KPopupMenu componentInChildren = base.GetComponentInChildren<KPopupMenu>();
		componentInChildren.SetOptions(new string[] { "None", "Rooms", "Lighting", "Style", "Flow" });
		KPopupMenu kpopupMenu = componentInChildren;
		kpopupMenu.OnSelect = (Action<string, int>)Delegate.Combine(kpopupMenu.OnSelect, new Action<string, int>(this.OnSelect));
		base.gameObject.SetActive(false);
	}

	// Token: 0x06005DAE RID: 23982 RVA: 0x002241F0 File Offset: 0x002223F0
	private void OnSelect(string str, int index)
	{
		if (str == "None")
		{
			SimDebugView.Instance.SetMode(OverlayModes.None.ID);
			return;
		}
		if (str == "Flow")
		{
			SimDebugView.Instance.SetMode(SimDebugView.OverlayModes.Flow);
			return;
		}
		if (str == "Lighting")
		{
			SimDebugView.Instance.SetMode(OverlayModes.Light.ID);
			return;
		}
		if (!(str == "Rooms"))
		{
			Debug.LogError("Unknown debug view: " + str);
			return;
		}
		SimDebugView.Instance.SetMode(OverlayModes.Rooms.ID);
	}
}
