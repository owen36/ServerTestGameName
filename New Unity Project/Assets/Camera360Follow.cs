using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera360Follow : MonoBehaviour {

	public Transform target;
    public float distance = 2f;
    public float height = 1f;
	public Vector3 lookOffset = new Vector3 (0,1,0);
    public float camSpeed = 10f;
    public float rotSpeed = 10f;

	
	// Update is called once per frame
	void LateUpdate () 
	{
		if (target) 
		{
			Vector3 lookPosition = target.position + lookOffset;
			Vector3 relativePos = lookPosition - transform.position;
			Quaternion rot = Quaternion.LookRotation (relativePos);

			transform.rotation = Quaternion.Slerp (this.transform.rotation, rot, Time.deltaTime * rotSpeed * 0.1f);

			Vector3 targetPos = target.transform.position + target.transform.up * height - target.transform.forward * distance;
			this.transform.position = Vector3.Lerp (this.transform.position, targetPos, Time.deltaTime * camSpeed * 0.1f);
		}
	}
}
