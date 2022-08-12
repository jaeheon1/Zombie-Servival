using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{

    [SerializeField] float axisspeed = 5.0f; //카메라 회전 속도
    [SerializeField] GameObject eye;

    private float eulerAngleX;
    private float eulerAngleY;

    private CharacterController characterControl;
    private Vector3 moveForce;
    [SerializeField] float speed;


    [SerializeField] float gravity = 20.0f;
    [SerializeField] ParticleSystem effect;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject aa;


    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        characterControl = GetComponent<CharacterController>();
    }

    
    void Update()
    {
        UpdateRotate(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        characterControl.Move(moveForce*Time.deltaTime);
        MoveTo(new Vector3 (Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));

        if(Input.GetButtonDown("Fire1"))
        {
            effect.Play();
            Soundsystem.instance.Sound(0);
            Instantiate(bullet, aa.transform.position, aa.transform.rotation);

        }


        if(characterControl.isGrounded==false)
        {
            moveForce.y -= gravity * Time.deltaTime;

        }
        else
        {
            moveForce.y = 0.1f;
        }

    }

    public void MoveTo(Vector3 direction)
    {

        //카메라 회전으로 전방 방향이 변하기 때문에 회전 값을 곱해서 연산합니다. 
        direction = transform.rotation * new Vector3(direction.x, 0, direction.z);

        moveForce = new Vector3(direction.x * speed, moveForce.y, direction.z * speed);
    }


    public void UpdateRotate(float mouseX, float mouseY)
    {
        eulerAngleY += mouseX * axisspeed; //마우스 좌/우 이동으로 카메라 .y 축 회전
        // 마우스 아래로 내리면 -로 음수인데 오브젝트의 x축이 + 방향으로 회전해야 아래를 보기 때문입니다. 
        eulerAngleX -= mouseY * axisspeed; //마우스 위/아래 이동으로 카메라 x 축 회전

        eulerAngleX = ClampAngle(eulerAngleX, -80, 50);

        transform.rotation = Quaternion.Euler(transform.rotation.x, eulerAngleY, 0);

        eye.transform.rotation = Quaternion.Euler(eulerAngleX,transform.eulerAngles.y, 0);

    }
    public float ClampAngle(float angle,float min,float max)
    {
        return Mathf.Clamp(angle, min, max);
    }


}
