using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform ViewPoint;
    public float MouseSens = 1f;
    private float verticalRotStore;
    private Vector2 mouseInput;

    public bool InvertLook;

    public float MoveSpeed = 5f, RunSpeed = 8f;
    private float activeMooveSpeed;
    private Vector3 moveDir;
    private Vector3 movement;

    public CharacterController CharCont;
    private Camera cam;

    public float JumpForce = 12f, GravityMod = 2.5f;
    public Transform GroundChechPoint;
    private bool isGrounded;
    public LayerMask GroundLayer;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        activeMooveSpeed = MoveSpeed;
        cam = Camera.main;
    }
    private void Update()
    {
        mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * MouseSens;

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);

        verticalRotStore += mouseInput.y;
        verticalRotStore = Mathf.Clamp(verticalRotStore, -60f, 60f);

        if (InvertLook)
        {
            ViewPoint.rotation = Quaternion.Euler(verticalRotStore, ViewPoint.rotation.eulerAngles.y, ViewPoint.rotation.eulerAngles.z);
        }
        else
        {
            ViewPoint.rotation = Quaternion.Euler(-verticalRotStore, ViewPoint.rotation.eulerAngles.y, ViewPoint.rotation.eulerAngles.z);
        }
        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            activeMooveSpeed = RunSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            activeMooveSpeed = MoveSpeed;
        }
        float yVel = movement.y;
        movement = ((transform.forward * moveDir.z) + (transform.right * moveDir.x)).normalized * activeMooveSpeed;

        movement.y = yVel;

        if (CharCont.isGrounded)
        {
            movement.y = 0;
        }
        isGrounded = Physics.Raycast(GroundChechPoint.position, Vector3.down, .3f, GroundLayer);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            movement.y = JumpForce;
        }
        movement.y += Physics.gravity.y * Time.deltaTime * GravityMod;
        CharCont.Move(movement * Time.deltaTime);
    }
    private void LateUpdate()
    {       
            cam.transform.position = ViewPoint.position;
            cam.transform.rotation = ViewPoint.rotation;      
    }

}
