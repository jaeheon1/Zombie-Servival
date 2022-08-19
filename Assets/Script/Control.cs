using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{

    [SerializeField] float axisspeed = 5.0f; //ī�޶� ȸ�� �ӵ�
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
            //�ٴڰ� �浹�� ���¶�� 
           if (characterControl.isGrounded)
           {
                // ������ �� �� �ֵ��� �����մϴ�
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

        //ī�޶� ȸ������ ���� ������ ���ϱ� ������ ȸ�� ���� ���ؼ� �����մϴ�. 
        direction = transform.rotation * new Vector3(direction.x, 0, direction.z);

        moveForce = new Vector3(direction.x * speed, moveForce.y, direction.z * speed);
    }


    public void UpdateRotate(float mouseX, float mouseY)
    {
        eulerAngleY += mouseX * axisspeed; //���콺 ��/�� �̵����� ī�޶� .y �� ȸ��
        // ���콺 �Ʒ��� ������ -�� �����ε� ������Ʈ�� x���� + �������� ȸ���ؾ� �Ʒ��� ���� �����Դϴ�. 
        eulerAngleX -= mouseY * axisspeed; //���콺 ��/�Ʒ� �̵����� ī�޶� x �� ȸ��

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

        // ȭ���� �߾� ��ǥ (Cross Hair �� �������� Raycast�� ���� �մϴ�.)

        ray = Camera.main.ViewportPointToRay(Vector2.one * 0.5f);

        //���� ��Ÿ� �ȿ� �ε����� ������Ʈ�� ������ target �� ������ �ε��� ��ġ�� �����մϴ�.
        if(Physics.Raycast(ray,out hit,distance))
        {
            target = hit.point;
        }
        //���� ��Ÿ� �ȿ� �ε����� ������Ʈ�� ������ target �����ʹ� �ִ� ��Ÿ��� ��ġ�� �����մϴ�.
        else
        {
            target = ray.origin + ray.direction * distance;
        }


        // ù��° Raycast �������� ����� target �� ������ ��ǥ�������� �����ϰ�, 
        //�ѱ� ��ġ���� Raycast �� �߻��մϴ�.


        Vector3 direction = (target - effect.transform.position).normalized;

        if(Physics.Raycast(effect.transform.position,direction,out hit,distance,layer))
        {
           
            hit.collider.GetComponentInParent<AIControl>().health -= 20;
            hit.collider.GetComponentInParent<AIControl>().Death();


            Instantiate(effect, hit.transform.position, hit.transform.rotation);

          
        }

    }


}
