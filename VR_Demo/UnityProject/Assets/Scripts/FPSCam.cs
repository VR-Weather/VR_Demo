//Shenjia inside
using UnityEngine;
using System.Collections;

public class FPSCam : MonoBehaviour {
	/*
		Hang this script onto the camera.
	*/

	//optional

	//selection
	//[SerializeField]SelectionLight sel;
	GameObject target=null,lastTarget=null;


	//main
	float theta=270f,phi=10f;
	public float phi_max=80f;
	public float phi_min=-60f;
	public float RotateSensitivity=100f;
	public bool invertX=false;
	public bool invertY=false;

	public float CamSpeed=0.5f;
	public bool ViewEnabled=true;

	//temp
	float MouseX,MouseY,dt;
	RaycastHit hitInfo;
	

	//float ScreenRatio,Dis;
	//camera center, up, down,right,left
	// Use this for initialization
	void Start () {
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;

		AccquireInput();
		Moving();

	}
	
	// Update is called once per frame
	void LateUpdate () {
		
		AccquireInput();
		
		Moving ();

		FindTarget();

		Interaction();
	}
	
	void AccquireInput()
	{
		//Convert Mouse Movement into Motion parameters
		dt=Time.deltaTime;

		if(Input.GetKeyDown(KeyCode.Escape))
		{
			ViewEnabled=Cursor.visible;
			Cursor.visible = !Cursor.visible;
			if(CursorLockMode.Locked==Cursor.lockState)
				Cursor.lockState=CursorLockMode.None;
			else
				Cursor.lockState=CursorLockMode.Locked;
			
		}

		if(ViewEnabled)
		{
			MouseX=Input.GetAxis("Mouse X")*RotateSensitivity*dt;
			MouseY=Input.GetAxis("Mouse Y")*RotateSensitivity*dt;
			//Invert Options
			if(invertX)
				MouseX*=-1f;
			if(invertY)
				MouseY*=-1f;
		}

	}
	
	void Moving()
	{
		if (ViewEnabled)
		{
			//motion planning
			phi=Mathf.Clamp(phi-MouseY,phi_min,phi_max);
			theta += MouseX;
		}


		transform.rotation = Quaternion.Euler(new Vector3(phi,theta,0f));
	}

	void FindTarget()
	{
		//1<<8 means "Selectable" layer 
		if(Physics.Raycast(new Ray(transform.position, transform.forward),out hitInfo, 100f, 1<<8))
		{
			target = hitInfo.collider.gameObject;
		}
		else
		{
			target = null;
		}

		//update target
		if(target!=lastTarget)
		{
			Debug.Log("Target changed");
			//sel.target = target;


			if(target!=null)
			{
				Dele_Handler dh = target.GetComponent<Dele_Handler>();
				if(dh!=null)
				{
					dh.OnFocus();
				}
			}

			if(lastTarget!=null)
			{
				Dele_Handler dh = lastTarget.GetComponent<Dele_Handler>();
				if(dh!=null)
				{
					dh.OnLostFocus();
				}
			}
		}

		lastTarget = target;
	}

	void Interaction()
	{
		if(Input.GetMouseButtonDown(0))
		{
			if(target!=null)
			{
				Dele_Handler dh = target.GetComponent<Dele_Handler>();
				if(dh!=null)
				{
					dh.OnClicked();
				}
			}
		}
	}
}

