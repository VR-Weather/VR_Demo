//obselete

using UnityEngine;
using System.Collections;

public class SelectionLight : MonoBehaviour {

	[SerializeField]Light _light;
	public GameObject target;

	void Awake()
	{
		if(null == _light)
		{
			_light = GetComponent<Light>();
		}
	}

	void Update()
	{
		followTarget();
	}

	void followTarget()
	{
		if(null == target)
		{
			LightPower(false);
		}
		else
		{
			transform.LookAt(target.transform);
			LightPower(true);
		}
	}

	public void LightPower(bool var)
	{
		_light.enabled = var;
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawRay(new Ray(transform.position,transform.position + transform.forward));
	}
}
