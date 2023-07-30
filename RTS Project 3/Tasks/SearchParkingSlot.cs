using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchParkingSlot : TaskInterface
{
   public void Execute(DeviceRegistry devices) 
   {
        if(Jobs.STATE_OBJECT_DETECTION == devices.memory[0])
        {
            if(-14f < devices.gps[0] && 14f > devices.gps[0])
            {
         //       Debug.Log("GPS0 -- > ["+devices.gps[0]+"]");
         //       Debug.Log("Lidar4 ["+devices.lidar[4]+"]  Lidar5["+devices.lidar[5]+"]");
                if(((devices.lidar[4] > 4f && devices.lidar[4] < 8f)   
                && (devices.lidar[5] > 4f && devices.lidar[5] < 8f))
                ||((devices.lidar[3] > 4f && devices.lidar[3] < 8f)      //TODO:@Add By deep
                && (devices.lidar[4] > 4f && devices.lidar[4] < 8f))
                )
                {
                    devices.memory[0] = Jobs.STATE_PARKING_DETECTED;    //Parking Detected
                }
                //TODO: @CMT by Deep
               /* else if((12f < devices.gps[0] && 13f >devices.gps[0]) || (-12 < devices.gps[0] && -13.5 > devices.gps[0]))
                {
                    if((devices.lidar[4] >6.5f && devices.lidar[4] <=10f )&&((devices.lidar[5] > 4f && devices.lidar[5] < 7.5f))){ 
                        devices.memory[0] = Jobs.STATE_PARKING_DETECTED;
                        // Parking detected at the corner 
                    }
                    
                }*/
            }
        }

        else if(Jobs.STATE_PARKING_DETECTED == devices.memory[0])
        {
            
            devices.brakeControl[0] = 1f;
            devices.brakeControl[1] = 2f;
            devices.speedControl[0] = 1f;
            devices.speedControl[1] = 0f;

            devices.memory[0] = Jobs.STATE_READY_FOR_PARKING;    //Ready for parking
        }
   }
}
