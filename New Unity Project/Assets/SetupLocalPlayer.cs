using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine;
using System;
using UnityStandardAssets.Vehicles.Car;

public class SetupLocalPlayer : NetworkBehaviour {

    public CarUserControl driveScript;
    //public Text playerNamePrefab;
    public Camera camPrefab;
    public Transform camTransform;
    public Canvas playerCanvas;
    public GameType currentGame;


    //private Text playerName;
    //private string pName = "Player";
    private Camera cam;
    private Rigidbody rb;

	[SyncVar (hook = "OnForceApplied")]
	public Vector3 volocity;

	public void OnForceApplied(Vector3 rigidBody)
	{
		rb.velocity = rigidBody;
	}

    public enum GameType
    {
        HorseRaces,
        GoldenBalls,
        MonsterTrucks 
    }

    private void OnGUI()
    {
       // if(isLocalPlayer)
       // {
       //     pName = GUI.TextField(new Rect(25, 15, 100, 25), "Player");
       //     if (GUI.Button(new Rect(130, 15, 60, 25), "Apply"))
       //         CmdChangeName(pName);
           
       // }

#if UNITY_ANDROID || UNITY_EDITOR
        if(playerCanvas != null)
            playerCanvas.gameObject.SetActive(isLocalPlayer);
#endif
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        //CmdChangeName("Player");
    }


   // [Command]
//private void CmdChangeName(string pName)
   // {
    //  //  playerName.text = pName;
    //}


    // Use this for initialization
    void Start ()
    {
        if (currentGame == GameType.MonsterTrucks)
        {
            if (!isLocalPlayer)
                driveScript.enabled = false;
            else
            {
                driveScript.enabled = true;
				cam = Instantiate(camPrefab);
				Camera360Follow follow = cam.GetComponent<Camera360Follow>();
				follow.target = camTransform;
                rb = this.GetComponent<Rigidbody>();

				volocity = rb.velocity;
            }
        }

        //GameObject canvas = GameObject.FindWithTag("Finish");
        //playerName = Instantiate(playerNamePrefab, Vector3.zero, Quaternion.identity) as Text;
        //playerName.transform.SetParent(canvas.transform);
    }

    private void LateUpdate()
    {
        //Vector3 namePos = Camera.main.WorldToScreenPoint(nameTransform.position);

       // if (playerName != null)
        //    playerName.transform.position = namePos;

       // if (isLocalPlayer && playerName != null)
       //     playerName.enabled = false;


        //if (cam != null)
        //{
        //    cam.transform.position = camTransform.position;
        //    cam.transform.rotation = camTransform.rotation;
        //}
    }



    void OnCollisionEnter(Collision collision)
	{		
		if (collision.gameObject.tag == "Player" && isLocalPlayer)
        {
			CmdApplyForce (collision.relativeVelocity, collision.transform.position);
        }
    }

	[Command]
	public void CmdApplyForce(Vector3 velocity, Vector3 position)
	{
		this.rb.AddForceAtPosition (velocity, position, ForceMode.Impulse);
	}
}
