using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class Drive : MonoBehaviour {

	public float speed = 10.0F;
    public float rotationSpeed = 100.0F;
    Rigidbody rb;
   // public Canvas playerCanvas;
    //public Button goButton;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
      //ork  playerCanvas.gameObject.SetActive(true);
        //goButton.onClick.AddListener(OnButtonDown);
    }

    void Update() {
        float translation = CrossPlatformInputManager.GetAxis("Vertical") * speed;
        float rotation = CrossPlatformInputManager.GetAxis("Horizontal") * rotationSpeed;
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;


        Quaternion turn = Quaternion.Euler(0, rotation, 0);
        rb.MovePosition(rb.position + this.transform.forward * translation);
        rb.MoveRotation(rb.rotation * turn);
    }
}

