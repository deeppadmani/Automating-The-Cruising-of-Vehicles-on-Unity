using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDetect : TaskInterface
{
    public void Execute(DeviceRegistry devices) { 
        
        if(devices.memory[0] == 8 || devices.memory[0] == 6 || devices.memory[0] == 19){
            //obstacle ahead 
            devices.speedControl[0] = 1f;
            devices.speedControl[1] = 0f;
            devices.brakeControl[0] = 1f;
            devices.brakeControl[1] = 2f;
            if(devices.memory[1] == 17 || devices.memory[1] == 9 || devices.memory[1] == 13){
         //       Debug.Log("In["+devices.memory[1]+"]");
                devices.memory[0] = devices.memory[1];
            }
            else
            {
       //         Debug.Log("In else memory0["+devices.memory[0]+"] else memory1"+devices.memory[1]+"]");
                devices.memory[0] = 4;
            }
        }

        // for pedestrains - check always
        if (checkPedestrains(devices) == true && devices.speedControl[0] > 0){
            // check for parking mode
            if (devices.memory[0] == 17){
                devices.memory[0] = 19;
                devices.memory[1] = 17;
            }else {
                devices.memory[0] = 19;
            }
            
        }

        if(devices.memory[0] == 5){
            if (checkCar(devices) == true && devices.speedControl[0] > 0){
                devices.memory[0] = 8;
            }
        }
    }

    public bool checkCar(DeviceRegistry devices){
        // return true if car in front of vehicle
        //(0, 124, 255) (0, 255, 27) (172, 0, 255) (192, 192, 192) (101, 33, 0)
        if((devices.pixels[5,4,0] == 0 && devices.pixels[5,4,1] == 124 && devices.pixels[5,4,2] == 255) || 
        (devices.pixels[5,4,0] == 0 && devices.pixels[5,4,1] == 255 && devices.pixels[5,4,2] == 27) || 
        (devices.pixels[5,4,0] == 172 && devices.pixels[5,4,1] == 0 && devices.pixels[5,4,2] == 255) || 
        (devices.pixels[5,4,0] == 192 && devices.pixels[5,4,1] == 192 && devices.pixels[5,4,2] == 192) || 
        (devices.pixels[5,4,0] == 101 && devices.pixels[5,4,1] == 33 && devices.pixels[5,4,2] == 0)||
        (devices.pixels[4,4,0] == 0 && devices.pixels[4,4,1] == 124 && devices.pixels[4,4,2] == 255) || 
        (devices.pixels[4,4,0] == 0 && devices.pixels[4,4,1] == 255 && devices.pixels[4,4,2] == 27) || 
        (devices.pixels[4,4,0] == 172 && devices.pixels[4,4,1] == 0 && devices.pixels[4,4,2] == 255) || 
        (devices.pixels[4,4,0] == 192 && devices.pixels[4,4,1] == 192 && devices.pixels[4,4,2] == 192) || 
        (devices.pixels[4,4,0] == 101 && devices.pixels[4,4,1] == 33 && devices.pixels[4,4,2] == 0)){
            return true;
        }
        return false;
    }

    public bool checkPedestrains(DeviceRegistry devices){
        // return true if pedestrain in front of vehicle
        if((devices.pixels[5,4,1] == 0 && devices.pixels[5,4,0] == 255 && devices.pixels[5,4,2] == 27) || 
        (devices.pixels[4,4,1] == 0 && devices.pixels[4,4,0] == 255 && devices.pixels[4,4,2] == 27)){
            return true;
        }
        return false;
    }
}
