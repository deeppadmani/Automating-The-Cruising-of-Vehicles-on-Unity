using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightWallFollow : TaskInterface
{
    public void Execute(DeviceRegistry devices) 
    {
        if (Jobs.STATE_MOVING_FORWARD_WITH_2F_SPEED == devices.memory[0])
        {  
            devices.brakeControl[0] = 2f;
            devices.brakeControl[1] = 0f;
            devices.speedControl[0] = 1f;
            devices.speedControl[1] = 1.5f;                 //TODO@: Speed Changed by Deep
            devices.memory[0] = Jobs.STATE_OBJECT_DETECTION;    //checking for object
        }
        if (Jobs.STATE_OBJECT_DETECTION == devices.memory[0])
        {   
           if(10 > devices.lidar[0] )
           {
                devices.speedControl[0] = 1f;
                devices.speedControl[1] = 0f;
                
       //         Debug.Log("############ STATE_OBJECT_DETECTION GPS0 -- > ["+devices.gps[0]+"] GPS1 --> [" + devices.gps[1]+"]");
                if((28f > devices.gps[1] && 25f < devices.gps[1]) ||      // lane-1
                  (7.5f > devices.gps[1] && 6.5f < devices.gps[1]) ||      // Lane-3 
                  (-12.5f > devices.gps[1] && -13.5f < devices.gps[1])   //Lane-5  
                )
                {
            //        Debug.Log("############ STATE_OBJECT_DETECTED ");
                    devices.memory[0] = Jobs.STATE_OBJECT_DETECTED;  //object detectd
                }
                else
                {
                   if(244 == devices.pixels[1,14,1] &&
                        244 == devices.pixels[2,14,1] ){  //Condition to check for wall on lanes 2,4,6
                        devices.memory[0] = Jobs.STATE_TURNING_LEFT;  //turn left to change lane  
                    }  
                    else {
                        devices.memory[0]=Jobs.STATE_OBJECT_DETECTED; //Peds or car detected
                    }
                }
           }
        }


        if (Jobs.STATE_MOVING_FORWARD_WITH_1F_SPEED == devices.memory[0])         //lane change 
        {   
          /*  if(10 > devices.lidar[0]){                    //TODO: @CMT By Deep
                devices.memory[1] = 9;
                devices.memory[0] = Jobs.STATE_OBJECT_DETECTED;
            } */
            devices.speedControl[0] = 1f;
            devices.speedControl[1] = 1.5f;  
            devices.memory[0] = Jobs.STATE_LANE_DETECTION;
        }

        if (Jobs.STATE_LANE_DETECTION == devices.memory[0])
        {   
            if(10 > devices.lidar[0]){
                devices.memory[1] = 9;
                devices.memory[0] = Jobs.STATE_OBJECT_DETECTED;
            } 
            if(15 > devices.gps[1] &&  14.5 < devices.gps[1]||    //Lane-2          // TODO@:Changed by DEEP 
              (-5.8 > devices.gps[1] && -6.3 < devices.gps[1]) ||    //Lane-4
              (-25.8 > devices.gps[1] && -26.3 < devices.gps[1])    //Lane-6  
              )
            {
                    devices.memory[0] = Jobs.STATE_TURNING_RIGHT;         
                    devices.speedControl[0] = 1f;
                    devices.speedControl[1] = 0f;
            }
        }

        if(Jobs.STATE_U_TURN_1 == devices.memory[0])         // U-Turn state
        {
            if(10 > devices.lidar[0]){
                devices.memory[1] = 13;
                devices.memory[0] = Jobs.STATE_OBJECT_DETECTED;
            }
            devices.speedControl[0] = 1f;
            devices.speedControl[1] = 1f; 

            if(8 > devices.gps[1] && 7.5 < devices.gps[1] || 
               (-12 > devices.gps[1] && -13 < devices.gps[1]))
            {
                devices.speedControl[0] = 1f;
                devices.speedControl[1] = 0f;
                devices.memory[0] = Jobs.STATE_U_TURN_2;
            }
        }
    }
}
