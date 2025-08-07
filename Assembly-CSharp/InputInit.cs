using System;
using UnityEngine;

// Token: 0x02000963 RID: 2403
internal class InputInit : MonoBehaviour
{
	// Token: 0x06004502 RID: 17666 RVA: 0x0018D800 File Offset: 0x0018BA00
	private void Awake()
	{
		GameInputManager inputManager = Global.GetInputManager();
		for (int i = 0; i < inputManager.GetControllerCount(); i++)
		{
			KInputController controller = inputManager.GetController(i);
			if (controller.IsGamepad)
			{
				Component[] components = base.gameObject.GetComponents<Component>();
				for (int j = 0; j < components.Length; j++)
				{
					IInputHandler inputHandler = components[j] as IInputHandler;
					if (inputHandler != null)
					{
						KInputHandler.Add(controller, inputHandler, 0);
						inputManager.usedMenus.Add(inputHandler);
					}
				}
			}
		}
		if (KInputManager.currentController != null)
		{
			KInputHandler.Add(KInputManager.currentController, KScreenManager.Instance, 10);
		}
		else
		{
			KInputHandler.Add(inputManager.GetDefaultController(), KScreenManager.Instance, 10);
		}
		inputManager.usedMenus.Add(KScreenManager.Instance);
		DebugHandler debugHandler = new DebugHandler();
		if (KInputManager.currentController != null)
		{
			KInputHandler.Add(KInputManager.currentController, debugHandler, -1);
		}
		else
		{
			KInputHandler.Add(inputManager.GetDefaultController(), debugHandler, -1);
		}
		inputManager.usedMenus.Add(debugHandler);
	}
}
