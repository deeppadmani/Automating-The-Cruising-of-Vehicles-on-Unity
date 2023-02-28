using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug_Task : TaskInterface
{
    public void Execute(DeviceRegistry devices) {
        if (Input.GetKey("w")) {
           /** Problem 1: AVL Environment (10 points)  */
            devices.speedControl[0] = 1f;
            devices.speedControl[1] = 10f;
            // STUDENTS
            // Put your control code here
        }

        if (Input.GetKey("s")) {
            devices.speedControl[0] = 1f;
            devices.speedControl[1] = -10f;
        }

        if (Input.GetKey("x")) {
            devices.speedControl[0] = 1f;
            devices.speedControl[1] = 0f;
        }

        if (Input.GetKey("a") || Input.GetKey("d")) {
            devices.steeringControl[0] = 1f;

            if (Input.GetKey("a")) {
                devices.steeringControl[1] = -3;  //left
            } else {
                devices.steeringControl[1] = 3;   //right
            }
        } else {
            devices.steeringControl[0] = 1f;
            devices.steeringControl[1] = 0f;
        }

        if (Input.GetKey("space")) {
            devices.brakeControl[0] = 1f;
            devices.brakeControl[1] = Time.fixedDeltaTime;
        }
    }
}
