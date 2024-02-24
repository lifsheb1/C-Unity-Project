using UnityEngine;
using System.Collections;

public class CharacterMovementScript : MonoBehaviour
{
    public float lookSpeed = 3;

    private Vector2 rotation = Vector2.zero;

    public Camera mainCamera;

    public GameObject vacuum;
    public Material bronze;
    public Material silver;
    public Material gold;
    

    public AudioSource walkingSound;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float playerSpeed = 4.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    private bool soundPlaying = false;

    // Start is called before the first frame update
    void Awake()
    {
        mainCamera = FindObjectOfType<Camera>();
        controller = GetComponent<CharacterController>();
        mainCamera.nearClipPlane = 0.1f;
        // hide the player mesh
        // the mesh is useful for development though
        GetComponent<MeshRenderer>().enabled = false;
        if(PlayerPrefs.HasKey("material")){
            if(PlayerPrefs.GetString("material") == "bronze"){
                vacuum.GetComponent<MeshRenderer>().material = bronze;
            }
            else if(PlayerPrefs.GetString("material") == "silver"){
                vacuum.GetComponent<MeshRenderer>().material = silver;
            }
            else{
                vacuum.GetComponent<MeshRenderer>().material = gold;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GetIsFrozen()){ 
            walkingSound.Stop();
            soundPlaying = false;
            return;
        }
        
        rotation.y += Input.GetAxis("Mouse X");
        rotation.x += -Input.GetAxis("Mouse Y");
        rotation.x = Mathf.Clamp(rotation.x, -15f, 15f);
        transform.eulerAngles = new Vector2(0,rotation.y) * lookSpeed;
        mainCamera.transform.localRotation = Quaternion.Euler(rotation.x * lookSpeed, rotation.y * lookSpeed, 0);
        vacuum.transform.localRotation = Quaternion.Euler(90f, rotation.y * lookSpeed, 0);
        var vacYRotation = vacuum.transform.rotation.eulerAngles.y;
        float xChange;
        mainCamera.transform.position =
            new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z);
        vacuum.transform.position =
            new Vector3(transform.position.x+1.4f, transform.position.y+0.2f, transform.position.z+0.3f);
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = gravityValue;
        }

        var move = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
        
        controller.Move(move * (Time.deltaTime * playerSpeed));

        if (move != Vector3.zero)
        {
            if(!soundPlaying){
                walkingSound.Play();
                soundPlaying = true;
            }
            gameObject.transform.forward = move;

        }
        else{
            walkingSound.Stop();
            soundPlaying = false;
        }
            
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue) + -gravityValue;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

       /* Vector3 vacPos = new Vector3();
        vacPos.x = transform.position.x+0.4f;
        vacPos.y = transform.position.y;
        //vacPos.z = transform.position.z+0.5f;
        vacPos.z = vacuum.transform.position.z;
        vacuum.transform.position = vacPos;
        
        Vector3 vacRotation = vacuum.transform.rotation.eulerAngles;
        vacRotation.x = transform.rotation.x + 90f;
        vacRotation.y = transform.rotation.y*2f;
        vacRotation.z = transform.rotation.z;
        vacuum.transform.rotation = Quaternion.Euler(vacRotation);*/
    }
}
