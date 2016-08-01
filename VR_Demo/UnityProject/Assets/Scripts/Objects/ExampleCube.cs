using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Dele_Handler))]

public class ExampleCube : MonoBehaviour {

	//delegation
	Dele_Handler dh;

	//color
	Renderer rend;
	[SerializeField]Material matNorm,matHigh;

	//rotation
	bool rotation;
	[SerializeField]float omega=60f;
	float theta;

	//temp
	float dt;

	void Awake()
	{
		dh=GetComponent<Dele_Handler>();
		rend = GetComponent<MeshRenderer>();
	}
		
	// Use this for initialization
	void Start () {
		dh.OnFocus = OnFocus;
		dh.OnLostFocus = UnFocus;
		dh.OnClicked = OnClick;
	}
	
	// Update is called once per frame
	void Update () {
	
		dt = Time.deltaTime;

		Rotation();
	}

	void Rotation()
	{
		if(rotation)
		{
			transform.Rotate(0f,dt*omega,0f);
		}
	}

	void OnFocus()
	{
		rend.material = matHigh;
	}

	void UnFocus()
	{
		rend.material = matNorm;
	}

	void OnClick()
	{
		rotation = !rotation;
	}
}
