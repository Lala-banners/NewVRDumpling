using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public static class VrUtils
{
	//XR = mixed reality
	private static List<XRInputSubsystem> subsystems = new List<XRInputSubsystem>();

	public static void SetVREnabled(bool _enabled)
	{
		//Get all the connected XR devices
		SubsystemManager.GetInstances<XRInputSubsystem>(subsystems);

		//Loop through all XR devices
		foreach (XRInputSubsystem subsystem in subsystems)
		{
			//If we want to enable it, start it, otherwise stop it
			if (_enabled)
			{
				subsystem.Start();
			}
			else
			{
				subsystem.Stop();
			}
		}
	}

	public static bool IsVREnabled()
	{
		//Get all the connected XR devices
		SubsystemManager.GetInstances<XRInputSubsystem>(subsystems);

		//Loop through all XR devices
		foreach (XRInputSubsystem subsystem in subsystems)
		{
			//Check if the subsystem is active, if so return true
			if (subsystem.running)
			{
				return true;
			}
		}

		//No active XR device
		return false;
	}
}
