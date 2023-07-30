using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchParkingSlot : TaskInterface
{
   public void Execute(DeviceRegistry devices) 
   {
        if(Jobs.STATE_OBJECT_DETECTION == devices.memory[0])
        {
            if(-19 < devices.gps[0] && 12 > devices.gps[0])
            {
                if((devices.lidar[4] > 4f && devices.lidar[4] < 8f)   
                && (devices.lidar[3] > 4f && devices.lidar[3] < 8f))
                {
                    devices.memory[0] = Jobs.STATE_PARKING_DETECTED;    //Parking Detected
                }
            }
        }

        if(Jobs.STATE_PARKING_DETECTED == devices.memory[0])
        {
            devices.speedControl[0] = 1f;
            devices.speedControl[1] = 0f;
            devices.brakeControl[0] = 1f;
            devices.brakeControl[1] = 2f;

            devices.memory[0] = Jobs.STATE_READY_FOR_PARKING;    //Ready for parking
        }
   }
}
