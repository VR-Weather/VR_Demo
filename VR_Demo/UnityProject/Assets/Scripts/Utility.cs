using UnityEngine;
using System.Collections;

public enum TrigType{Once,Sychro,OnceAndInput};

public static class Shenjia{
	static float dtr=Mathf.Deg2Rad;
	
	public static Vector3 Multi(Vector3 A,Vector3 B)
	{
		return new Vector3(A.x*B.x, A.y*B.y,  A.z*B.z);
	}
	public static Vector3 Div(Vector3 A,Vector3 B)
	{
		return new Vector3(A.x/B.x, A.y/B.y,  A.z/B.z);
	}
	public static Vector3 GalileoTransform(Transform Base, Vector3 A)
	{
		//	This is another transform. 
		//	It is different from Transform Point, because
		//this transform ignores the scale of the transform.
		return (Base.TransformPoint(Div(A,Base.lossyScale)));
	}
	public static Vector3 InvGalileoTrans(Transform Base, Vector3 A)
	{
		//	This is another transform. 
		//	It is different from Inverse Transform Point, because
		//this transform ignores the scale of the transform.
		return Multi(Base.InverseTransformPoint(A),Base.lossyScale);
	}
	public static Vector3 RotateVector(Vector3 A,Vector3 Angle)
	{
		float rX=Angle.x;
		float rY=Angle.y;
		float rZ=Angle.z;
		//The angle should be in radians.
		
		Vector3 B = new Vector3(A.x*Mathf.Cos(rZ)-A.y*Mathf.Sin(rZ),
		                        A.x*Mathf.Sin(rZ)+A.y*Mathf.Cos(rZ),
		                        A.z);
		
		B = new Vector3(B.x*Mathf.Cos(rY)-B.z*Mathf.Sin(rY),
		                B.y,
		                B.x*Mathf.Sin(rY)+B.z*Mathf.Cos(rY)
		                ) ;
		
		B = new Vector3(B.x,
		                B.y*Mathf.Cos(rX)-B.z*Mathf.Sin(rX),
		                B.y*Mathf.Sin(rX)+B.z*Mathf.Cos(rX) ) ;
		
		return B;
		
	}
	
	public static Vector3 CR(Vector3 A,Vector3 B,Vector3 C,Vector3 D,float t)
	{
		Vector3 a_3=-A+3*B-3*C+D; 
		Vector3 a_2=2*A-5*B+4*C-D;
		Vector3 a_1=-A+C;
		Vector3 a_0=B;
		
		return 0.5f*(t*(t*(a_3*t+a_2)+a_1))+a_0 ;
	}
	
	public static Vector3 Sphere_to_Cartesian(Vector3 A)
	{
		//A:(r,theta,phi)
		//my phi is that with xOz in degrees;
		float x=A.x*Mathf.Cos(dtr*A.y)*Mathf.Cos(dtr*A.z);
		float y=A.x*Mathf.Sin(dtr*A.z);
		float z=A.x*Mathf.Sin(dtr*A.y)*Mathf.Cos(dtr*A.z);
		return new Vector3(x,y,z);
	}
	public static Vector3 Sphere_to_Cartesian(float r,float theta,float phi)
	{
		//A:(r,theta,phi)
		//my phi is that with xOz
		float x=r*Mathf.Cos(dtr*theta)*Mathf.Cos(dtr*phi);
		float y=r*Mathf.Sin(dtr*phi);
		float z=r*Mathf.Sin(dtr*theta)*Mathf.Cos(dtr*phi);
		return new Vector3(x,y,z);
	}
	

	
	public static Vector3 ScreenToUnit(Vector3 A, float CamSize)
	{
		//Converts pixels to Unity units.
		double ratio = 2*CamSize / Screen.height;	//a cam with size 5 will have 10 units for a screen, vertically.
		//Applies the conversion, since camera itself looks in the Z direction, no conversion is needed.
		//And makes the zero point in the center of the object, not the corner
		Vector3 B = new Vector3((float)((A.x-Screen.width/2)*ratio), (float)((A.y-Screen.height/2)*ratio) , 0f);
		
		return B;
	}
	
	public static void DrawRectXZ(Rect R, Vector3 center)
	{
		Vector3 A,B,C,D;
		A=center+new Vector3(R.width/2,0,R.height/2);
		B=center+new Vector3(R.width/2,0,-R.height/2);
		C=center+new Vector3(-R.width/2,0,-R.height/2);
		D=center+new Vector3(-R.width/2,0,R.height/2);
		
		Gizmos.DrawLine(A,B);
		Gizmos.DrawLine(C,B);
		Gizmos.DrawLine(C,D);
		Gizmos.DrawLine(A,D);
		return;
	}
}