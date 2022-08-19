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
    
    [SerializeField] GameObject aa;
    [SerializeField] float distance = 100.0f;
    [SerializeField] LayerMask layer;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        characterControl = GetComponent<CharacterController>();
    }
    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //바닥과 충돌한 상태라면 
           if (characterControl.isGrounded)
           {
                // 점프를 할 수 있도록 설정합니다
                moveForce.y= 7.5f;
           }
        }
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
            TwoStepRay();
           

        }

        
        if(characterControl.isGrounded==false)
        {
            moveForce.y -= gravity * Time.deltaTime;

        }
        Jump();

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


    public void TwoStepRay()
    {
        Ray ray;
        RaycastHit hit;
        Vector3 target = Vector3.zero;

        // 화면의 중앙 좌표 (Cross Hair 를 기준으로 Raycast를 연산 합니다.)

        ray = Camera.main.ViewportPointToRay(Vector2.one * 0.5f);

        //공격 사거리 안에 부딪히는 오브젝트가 있으면 target 은 광선에 부딪힌 위치로 설정합니다.
        if(Physics.Raycast(ray,out hit,distance))
        {
            target = hit.point;
        }
        //공격 사거리 안에 부딪히는 오브젝트가 없으면 target 포인터는 최대 사거리에 위치로 설정합니다.
        else
        {
            target = ray.origin + ray.direction * distance;
        }


        // 첫번째 Raycast 연산으로 얻어진 target 의 정보를 목표지점으로 설정하고, 
        //총구 위치에서 Raycast 를 발사합니다.


        Vector3 direction = (target - effect.transform.position).normalized;

        if(Physics.Raycast(effect.transform.position,direction,out hit,distance,layer))
        {
           
            hit.collider.GetComponentInParent<AIControl>().health -= 20;
            hit.collider.GetComponentInParent<AIControl>().Death();


            Instantiate(effect, hit.transform.position, hit.transform.rotation);

          
        }

    }


}
