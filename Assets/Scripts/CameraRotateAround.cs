using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CameraRotateAround : MonoBehaviour {

	public Transform target;
	public Vector3 offset;
	public float sensitivity = 3; // чувствительность мышки
	public float limit = 80; // ограничение вращения по Y
	private float X, Y;
	public GameObject Permission;

	Vector2 tch1, tch2, stch1, stch2; //"stch"(x) - start touch
	float sdistance, distance;
	public float speed;
	GameObject build;
	void Start () 
	{
		limit = Mathf.Abs(limit);
		if (limit > 90) limit = 90;
		transform.position = target.position + offset;
		build = GameObject.FindGameObjectWithTag("building");
	}

	bool p = true;
	//bool p2 = false;
	public Vector3 sp;


	void Update ()
	{
		//Zoom
		if (Input.touchCount == 2)
		{
			sp = Vector3.zero;
			Permission.GetComponent<Permis>().permission2 = false;
			tch1 = Input.touches[0].position;
			tch2 = Input.touches[1].position;
			distance = Distance(tch1.x, tch2.x, tch1.y, tch2.y, distance);
			if ((stch1 == Vector2.zero) && (stch2 == Vector2.zero))
			{
				stch1 = Input.touches[0].position;
				stch2 = Input.touches[1].position;
				sdistance = Distance(stch1.x, stch2.x, stch1.y, stch2.y, sdistance);
			}
			if (sdistance > distance)
			{
				Camera.main.orthographicSize += 1f * speed;
			}
			if (sdistance < distance && Camera.main.orthographicSize > 3)
			{
				Camera.main.orthographicSize -= 1f * speed;
			}

			

		}
		if (Input.touchCount == 0)
		{
			stch1 = Vector2.zero;
			stch2 = Vector2.zero;
			Permission.GetComponent<Permis>().permission2 = true;
			p = true;
			sp = Vector3.zero;
		}


		//movementANdrotation
		if (Input.touchCount == 1 && Permission.GetComponent<Permis>().permission2 && p)
		{
			sp = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
			p = false;
		}
		if (Input.touchCount == 1 && Permission.GetComponent<Permis>().permission && Permission.GetComponent<Permis>().permission2)
		{
			X = transform.localEulerAngles.y + Input.touches[0].deltaPosition.x * sensitivity;
			Y += Input.touches[0].deltaPosition.y * sensitivity;
			Y = Mathf.Clamp(Y, -limit, limit);
			transform.localEulerAngles = new Vector3(-Y, X, 0);
			transform.position = transform.localRotation * offset + target.position;
		}
		
	     if (Input.touchCount == 1 && !Permission.GetComponent<Permis>().permission && Permission.GetComponent<Permis>().permission2 && sp != Vector3.zero)
	     {
			Vector3 diff = sp - Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Camera.main.transform.position += diff ;
			target.position += diff;
         }


	}
	float Distance(float n1, float n2, float n3, float n4, float dist)
	{
		dist = Convert.ToSingle(Math.Sqrt(Math.Pow(Math.Max(n1, n2) - Math.Min(n1, n2), 2) + Math.Pow(Math.Max(n3, n4) - Math.Min(n3, n4), 2)));
		return dist;
	}

}