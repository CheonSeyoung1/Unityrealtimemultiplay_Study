using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSet : MonoBehaviour
{
	[SerializeField] private MonoBehaviour[] components;
	[SerializeField] private GameObject[] objects;
	public void IsLocalPlayer()
	{
		foreach (var m_component in components)
		{
			if (m_component is null) continue;
			m_component.enabled = true;
		}
		foreach (var m_object in objects)
		{
			if (m_object is null) continue;
			m_object.SetActive(true);
		}
	}
}
