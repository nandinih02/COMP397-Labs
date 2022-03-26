using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public CharacterController controller;

    [Header("Movement")]
    public float maxSpeed = 10.0f;
    public float gravity = -30.0f;
    public float jumpHeight = 3.0f;
    public Vector3 velocity;

    [Header("Ground Detection")]
    public Transform groundCheck;
    public float groundRadius = 0.5f;
    public LayerMask groundMask;
    public bool isGrounded;

    [Header("Health System")]
    public UIControls controls;
    public bool isColliding = false;

    [Header("Onscreen Controller")]
    public Joystick leftjoystick;
    public GameObject onScreenControls;
    public GameObject miniMap;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        switch(Application.platform)
        {
            case RuntimePlatform.Android:
                //turn on Onscreen Controls
                onScreenControls.SetActive(true);
                break;
            case RuntimePlatform.WebGLPlayer:
                //Turn Onscreen Controls Off
                onScreenControls.SetActive(false);
                break;
            case RuntimePlatform.WindowsEditor:
                //Turn off Onscreen Controls
                onScreenControls.SetActive(false);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundMask);

        if(isGrounded && velocity.y < 0.0f)
        {
            velocity.y = -2.0f;
        }

        //foreach (var touch in Input.touches)
        //{
        //    Debug.Log(touch.position);
        //}

        //Keyboard Input (fallback)
        float x = Input.GetAxis("Horizontal") + leftjoystick.Horizontal;
        float z = Input.GetAxis("Vertical") + leftjoystick.Vertical;

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * maxSpeed * Time.deltaTime);

        if(Input.GetButton("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.M))
        {
            miniMap.SetActive(!miniMap.activeInHierarchy);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(!isColliding && hit.collider.gameObject.CompareTag("Enemy"))
        {
            controls.TakeDamage(10);
            isColliding = true;
        }
        
    }

    public void OnJumpButton_Pressed()
    {
        if(isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }
    }

    public void onMapButton_Pressed()
    {
        //Toggle Minimap
        miniMap.SetActive(!miniMap.activeInHierarchy);

    }
}
