using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CameraController : MonoBehaviour {

    
    //GameObject cam;
    public float camMovingSpeed;
    public Text countText;

    public static int playerCount;
    //private Quaternion adjustRotation;

    // Use this for initialization
    void Start()
    {
        Input.gyro.enabled = true;
        playerCount = 0;
        SetCount();

        //cam = GameObject.FindGameObjectWithTag("MainCamera");


        /*** if using more than one gyroscope axels for moving camera
         * this can be used for reset the camera view if it gets messed up
         * **/
        //adjustRotation = Quaternion.Euler(90f, 0f, 0f) * Quaternion.Inverse(Input.gyro.attitude);

    }

    // Update is called once per frame
    void LateUpdate ()
    {

#if UNITY_EDITOR

        //if using unity editor
        transform.Translate(Input.GetAxis("Horizontal") * camMovingSpeed * Time.deltaTime, 
                           0, 
                           Input.GetAxis("Vertical") * camMovingSpeed * Time.deltaTime);
#endif

        /**** tapping screen with 3 fingers restarts the game
        ******/
        if (Input.touchCount == 3)
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);

        }
        

        // moves camera forward automatically
        //transform.position = transform.position + 
        //    new Vector3(cam.transform.forward.x * camMovingSpeed * Time.deltaTime, 
        //    0,
        //    cam.transform.forward.z * camMovingSpeed * Time.deltaTime) ;

        //tilting phone will move the camera by accelerometer
        transform.Translate( 0, 0, -Input.acceleration.z * camMovingSpeed * Time.deltaTime);

        //rotates camera by gyro
        GyroModifyCamera();
  
    }


    private void GyroModifyCamera()
    {   
        //uses only y-axel for rotating camera because tilting camera is not possible if 
        //accelerometer is used for moving the camera forward/backwards
        transform.Rotate(/*-Input.gyro.rotationRate.x*/0, -Input.gyro.rotationRate.y, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            playerCount++;
            SetCount();
        }
    }

    private void SetCount()
    {
        countText.text = "Your points: " + playerCount;
    }

}
