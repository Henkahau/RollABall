using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;



public class PlayerController : MonoBehaviour {

    public float speed;
    public int count;
    

    public Text countText;
    public Text winText;

    private Rigidbody rb;
    
    

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        winText.text = "";
        SetCount();
        FindClosesPickUp();
     
    }
 
    private void FixedUpdate()
    {  
        if (!IsGameOver(CameraController.playerCount, count))
        {
            //Moves the ball to the target position
            transform.position = Vector3.MoveTowards( transform.position,
                                                    FindClosesPickUp().transform.position,
                                                    speed * Time.deltaTime);
        }
            
        else
            rb.Sleep();

    }


    //Collect pick up's
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count += 1;

            SetCount();
                                         
        }
    }

    private bool ChooseWinner()
    {
        return CameraController.playerCount > count;
    }

    private bool IsGameOver(int player, int rival)
    {
        return player + rival == 31;
    }

    void SetCount()
    {   
        countText.text = "Rival points " + count;

        if (IsGameOver(CameraController.playerCount, count))
        {
            if (ChooseWinner())
            {
                winText.text = "You win!" + Environment.NewLine + 
                    "Tap the screen for replay" + Environment.NewLine + "with three fingers";
            }
               
            else
            {
                winText.text = "You lose!" + Environment.NewLine +
                   "Tap the screen for replay" + Environment.NewLine + "with three fingers";

            }
               
        }
            
    }


   private GameObject FindClosesPickUp()
    {
        GameObject[] pickUps;
        pickUps = GameObject.FindGameObjectsWithTag("Pick Up");
        GameObject closestPU = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (GameObject pu in pickUps)
        {
            Vector3 diff = pu.transform.position - position;
            float currentDistance = diff.sqrMagnitude;

            if (currentDistance < distance)
            {
                closestPU = pu;
                distance = currentDistance;
            }
        }

        return closestPU;
    }

}
