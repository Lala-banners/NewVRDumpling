using UnityEngine;

public class VersionControl : MonoBehaviour
{
	#region Variables

	public GameObject VrRig;
	public GameObject PcRig;

	public GameObject VRUI;
	public GameObject PCUI;

	#endregion

	#region Start

	void Start()
	{
		//VR interface is active
		if(VrUtils.IsVREnabled())
		{
			SwitchToVR();
		}
		//PC interface is active
		else
		{
			SwitchToPC();
		}
	}

	#endregion

	private void Update()
	{
		if(VrUtils.IsVREnabled()) { }
		else
		{
			//Bring up the hoop menu if escape is pressed
			if(Input.GetKeyDown(KeyCode.Escape))
			{
				PCMenuActive();
			}
		}
	}

	public void PCMenuActive()
	{
		if(!PCUI.gameObject.activeSelf)
		{
			PCUI.gameObject.SetActive(true);
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			Time.timeScale = 0;
		}
		else
		{
			PCUI.gameObject.SetActive(false);
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			Time.timeScale = 1;
		}
	}

	public void SwitchToPC()
	{
		VrRig.gameObject.SetActive(false);
		PcRig.gameObject.SetActive(true);
		PCMenuActive();
		VRUI.gameObject.SetActive(false);
	}

	public void SwitchToVR()
	{
		VrRig.gameObject.SetActive(true);
		PcRig.gameObject.SetActive(false);
		VRUI.gameObject.SetActive(true);
		PCUI.gameObject.SetActive(false);
	}
}