using System;
using STRINGS;
using TUNING;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000DB4 RID: 3508
[AddComponentMenu("KMonoBehaviour/scripts/RocketThrustWidget")]
public class RocketThrustWidget : KMonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x06006E6C RID: 28268 RVA: 0x0029FFF2 File Offset: 0x0029E1F2
	protected override void OnPrefabInit()
	{
	}

	// Token: 0x06006E6D RID: 28269 RVA: 0x0029FFF4 File Offset: 0x0029E1F4
	public void Draw(CommandModule commandModule)
	{
		if (this.rectTransform == null)
		{
			this.rectTransform = this.graphBar.gameObject.GetComponent<RectTransform>();
		}
		this.commandModule = commandModule;
		this.totalWidth = this.rectTransform.rect.width;
		this.UpdateGraphDotPos(commandModule);
	}

	// Token: 0x06006E6E RID: 28270 RVA: 0x002A004C File Offset: 0x0029E24C
	private void UpdateGraphDotPos(CommandModule rocket)
	{
		this.totalWidth = this.rectTransform.rect.width;
		float num = Mathf.Lerp(0f, this.totalWidth, rocket.rocketStats.GetTotalMass() / this.maxMass);
		num = Mathf.Clamp(num, 0f, this.totalWidth);
		this.graphDot.rectTransform.SetLocalPosition(new Vector3(num, 0f, 0f));
		this.graphDotText.text = "-" + Util.FormatWholeNumber(rocket.rocketStats.GetTotalThrust() - rocket.rocketStats.GetRocketMaxDistance()) + "km";
	}

	// Token: 0x06006E6F RID: 28271 RVA: 0x002A0100 File Offset: 0x0029E300
	private void Update()
	{
		if (this.mouseOver)
		{
			if (this.rectTransform == null)
			{
				this.rectTransform = this.graphBar.gameObject.GetComponent<RectTransform>();
			}
			Vector3 position = this.rectTransform.GetPosition();
			Vector2 size = this.rectTransform.rect.size;
			float num = KInputManager.GetMousePos().x - position.x + size.x / 2f;
			num = Mathf.Clamp(num, 0f, this.totalWidth);
			this.hoverMarker.rectTransform.SetLocalPosition(new Vector3(num, 0f, 0f));
			float num2 = Mathf.Lerp(0f, this.maxMass, num / this.totalWidth);
			float totalThrust = this.commandModule.rocketStats.GetTotalThrust();
			float rocketMaxDistance = this.commandModule.rocketStats.GetRocketMaxDistance();
			this.hoverTooltip.SetSimpleTooltip(string.Concat(new string[]
			{
				UI.STARMAP.ROCKETWEIGHT.MASS,
				GameUtil.GetFormattedMass(num2, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.Kilogram, true, "{0:0.#}"),
				"\n",
				UI.STARMAP.ROCKETWEIGHT.MASSPENALTY,
				Util.FormatWholeNumber(ROCKETRY.CalculateMassWithPenalty(num2)),
				UI.UNITSUFFIXES.DISTANCE.KILOMETER,
				"\n\n",
				UI.STARMAP.ROCKETWEIGHT.CURRENTMASS,
				GameUtil.GetFormattedMass(this.commandModule.rocketStats.GetTotalMass(), GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.Kilogram, true, "{0:0.#}"),
				"\n",
				UI.STARMAP.ROCKETWEIGHT.CURRENTMASSPENALTY,
				Util.FormatWholeNumber(totalThrust - rocketMaxDistance),
				UI.UNITSUFFIXES.DISTANCE.KILOMETER
			}));
		}
	}

	// Token: 0x06006E70 RID: 28272 RVA: 0x002A02B9 File Offset: 0x0029E4B9
	public void OnPointerEnter(PointerEventData eventData)
	{
		this.mouseOver = true;
		this.hoverMarker.SetAlpha(1f);
	}

	// Token: 0x06006E71 RID: 28273 RVA: 0x002A02D2 File Offset: 0x0029E4D2
	public void OnPointerExit(PointerEventData eventData)
	{
		this.mouseOver = false;
		this.hoverMarker.SetAlpha(0f);
	}

	// Token: 0x04004BE0 RID: 19424
	public Image graphBar;

	// Token: 0x04004BE1 RID: 19425
	public Image graphDot;

	// Token: 0x04004BE2 RID: 19426
	public LocText graphDotText;

	// Token: 0x04004BE3 RID: 19427
	public Image hoverMarker;

	// Token: 0x04004BE4 RID: 19428
	public ToolTip hoverTooltip;

	// Token: 0x04004BE5 RID: 19429
	public RectTransform markersContainer;

	// Token: 0x04004BE6 RID: 19430
	public Image markerTemplate;

	// Token: 0x04004BE7 RID: 19431
	private RectTransform rectTransform;

	// Token: 0x04004BE8 RID: 19432
	private float maxMass = 20000f;

	// Token: 0x04004BE9 RID: 19433
	private float totalWidth = 5f;

	// Token: 0x04004BEA RID: 19434
	private bool mouseOver;

	// Token: 0x04004BEB RID: 19435
	public CommandModule commandModule;
}
