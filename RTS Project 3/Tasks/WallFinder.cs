using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFinder : TaskInterface
{
   public void Execute(DeviceRegistry devices) 
   {
        if(Jobs.STATE_OBJECT_DETECTED == devices.memory[0])
        {
            if(244 == devices.pixels[1,14,1] &&
              244 == devices.pixels[2,14,1] )
            {
                devices.memory[0] = Jobs.STATE_WALL_DETECTED;    //wall detected
            }
        }

        if(Jobs.STATE_WALL_DETECTED == devices.memory[0])
        {
            devices.steeringControl[0] = 1f;
            devices.steeringControl[1] = 0.5f;    //Right 

        }

        if ((devices.compass[0] > 179f) && 
            (devices.compass[0] < 360f-179f) && 
            (Jobs.STATE_WALL_DETECTED == devices.memory[0]))
        {
             devices.steeringControl[0] = 1f;
             devices.steeringControl[1] = 0f;
             devices.memory[0] = Jobs.STATE_MOVING_FORWARD_WITH_1F_SPEED;   //Right done
        }

        if(Jobs.STATE_TURNING_RIGHT == devices.memory[0])     //TODO:@BUG
        {
            devices.steeringControl[0] = 1f;
            devices.steeringControl[1] = 0.5f;    //Right
        }

        if ((devices.compass[0] < -89f) && 
            (devices.compass[0] > -91f) && 
            (Jobs.STATE_TURNING_RIGHT == devices.memory[0]))
        {
             devices.steeringControl[0] = 1f;
             devices.steeringControl[1] = 0f;
             devices.memory[1] = Jobs.STATE_TURNING_RIGHT;
             devices.memory[0] = Jobs.STATE_MOVING_FORWARD_WITH_2F_SPEED;   //Right done
        }

        if(Jobs.STATE_TURNING_LEFT == devices.memory[0])
        {
            devices.steeringControl[0] = 1f;
            devices.steeringControl[1] = -0.5f;    //Left
        }
        
        if ((devices.compass[0] > 179f) && 
            (devices.compass[0] < 360f-179f) && 
            (Jobs.STATE_TURNING_LEFT == devices.memory[0]))
        {
             devices.steeringControl[0] = 1f;
             devices.steeringControl[1] = 0f;
             devices.memory[0] = 13;   //Left done
        }


        if(Jobs.STATE_U_TURN_2 == devices.memory[0])
        {
            devices.steeringControl[0] = 1f;
            devices.steeringControl[1] = -0.5f;    //Left
        }
        
        if((devices.compass[0] < 91f) && 
            (devices.compass[0] > 89f) && 
            (Jobs.STATE_U_TURN_2 == devices.memory[0]))
        {
             devices.steeringControl[0] = 1f;
             devices.steeringControl[1] = 0f;
             devices.memory[1] = Jobs.STATE_U_TURN_2;
             devices.memory[0] = Jobs.STATE_MOVING_FORWARD_WITH_2F_SPEED;   //Left done
        }
   }
}
