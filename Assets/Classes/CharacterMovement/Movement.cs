using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    public float playerSpeed = 5.0f;
    private Rigidbody rigidbody;
    Vector3 rotationY;
    void Start() {
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        if (Input.GetAxis("Vertical") != 0)
        {
            MoveVertical(Input.GetAxis("Vertical"));
        }
        if (Input.GetAxis("Horizontal") != 0)
        {
            MoveHorizontal(Input.GetAxis("Horizontal"));
        }

        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            var anim = GetComponent<Animation>();
            anim.Play("idle");
        }
        
    }
    void MoveVertical(float verticalNumber)
    {
        
        //Player to move left, right, up, down
        if (verticalNumber > 0)
        {
            rigidbody.MovePosition(transform.position + transform.forward * Time.deltaTime * playerSpeed);
        }
        else {
            rigidbody.MovePosition(transform.position - transform.forward * Time.deltaTime * playerSpeed);
        }
        var anim = GetComponent<Animation>();
        anim.Play("Walk");
    }
    void MoveHorizontal(float verticalNumber)
    {
        if (verticalNumber > 0)
        {
            rotationY.Set(0f, verticalNumber, 0f);
            rotationY = rotationY.normalized * playerSpeed;
            Quaternion deltaRotation = Quaternion.Euler(rotationY * Time.deltaTime * playerSpeed * 4);
            rigidbody.MoveRotation(deltaRotation * rigidbody.rotation);
        }
        else {
            rotationY.Set(0f, verticalNumber, 0f);
            rotationY = rotationY.normalized * playerSpeed;
            Quaternion deltaRotation = Quaternion.Euler(rotationY * Time.deltaTime * playerSpeed * 4);
            rigidbody.MoveRotation(deltaRotation * rigidbody.rotation);
        }
        var anim = GetComponent<Animation>();
        anim.Play("Walk");
    }
}
