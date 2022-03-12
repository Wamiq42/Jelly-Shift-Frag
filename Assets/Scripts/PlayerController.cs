using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Rigidbody playerRigidbody;
    public Slider transformationSlider;
    public Transform cameraTransform;

    private Vector3 cameraPositionOffset = new Vector3(0, 1.52999997f, -5.76999998f);
    private float sliderLastValue;
    private float speed = 20.0f;
    private float velocityLimit = 15.0f;
    private bool isBouncing = false;

    // Start is called before the first frame update
    void Start()
    {
        sliderLastValue = transformationSlider.value;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        CameraFollowsPlayer();
        PlayerTransformation();
       
    }
    
    void CameraFollowsPlayer()
    {
        cameraTransform.position = transform.position + cameraPositionOffset;
    }
    void PlayerTransformation()
    {
        
        if (transformationSlider.value > 5 && transform.localScale.x > 0.5 )/* transformationSlider.value != sliderLastValue*/
        {
            Debug.Log(transformationSlider.value);
            transform.localScale = new Vector3(transform.localScale.x - 0.05f,
                 transform.localScale.y + 0.08f, transform.localScale.z);
            
            sliderLastValue = transformationSlider.value;
        }
        else if (transformationSlider.value < 5 && transform.localScale.y > 0.5 )/*ransformationSlider.value!=sliderLastValue*/
        {
            Debug.Log(transformationSlider.value);
            transform.localScale = new Vector3(transform.localScale.x + 0.05f,
                  transform.localScale.y - 0.08f, transform.localScale.z);
            
            sliderLastValue = transformationSlider.value;
        }
        //if (Input.GetKey(KeyCode.W) && transform.localScale.magnitude > 1)
        //{
        //    transform.localScale += new Vector3(transform.position.x - 0.02f,
        //        transform.position.y + 0.02f, 0) * Time.deltaTime;

        //}
        //if (Input.GetKey(KeyCode.S) && transform.localScale.magnitude > 1)
        //{
        //    transform.localScale += new Vector3(transform.position.x + 0.02f,
        //        transform.position.y - 0.02f, 0) * Time.deltaTime;
        //}
    }
    void PlayerMovement()
    {
        if(playerRigidbody.velocity.magnitude < velocityLimit && !isBouncing)
            playerRigidbody.velocity += Vector3.forward * speed * Time.deltaTime;
        if(isBouncing)
        {
            Debug.Log("isBouncing is ture");
            playerRigidbody.velocity += Vector3.back * speed * 2 * Time.deltaTime;
            playerRigidbody.velocity += Vector3.zero;
            //isBouncing = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Collided");
            isBouncing = true;

            playerRigidbody.velocity = Vector3.zero;
           
            StartCoroutine("IsBouncingCoroutine");

        }
            
    }

    IEnumerator IsBouncingCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        isBouncing = false;
    }
}
