using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody; //declare Rigidbody variable;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>(); //initialize rigidBody variable with information about the Rigidbody component
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }

    void Thrust(){
        if(Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up);
        }
    }
    
    void Rotate(){

        rigidBody.freezeRotation = true;

        if(Input.GetKey(KeyCode.A))
        {            
            transform.Rotate(Vector3.forward); //rotate on z
        }
        else if(Input.GetKey(KeyCode.D))
        {            
            transform.Rotate(-Vector3.forward);  //rotate on z
        }

        rigidBody.freezeRotation = false;
    }
}
