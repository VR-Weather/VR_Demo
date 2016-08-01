using UnityEngine;
using System.Collections;

public delegate void dele();

public class Dele_Handler : MonoBehaviour {

	public dele OnClicked,OnFocus,OnLostFocus;

	void Start()
	{
		OnClicked+=clicked;
		OnFocus+=focused;
		OnLostFocus+=deFocused;
	}

	void clicked()
	{
		Debug.Log(name+" is clicked");
	}

	void focused()
	{
		Debug.Log(name+" is focused");
	}

	void deFocused()
	{
		Debug.Log(name+" lost focus");
	}
}
