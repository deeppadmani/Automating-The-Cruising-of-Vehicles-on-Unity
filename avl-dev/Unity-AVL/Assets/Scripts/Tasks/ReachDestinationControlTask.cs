using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachDestinationControlTask : TaskInterface
{
    public void Execute(DeviceRegistry devices) 
    {
        if (Constants.STATE_READY_FOR_STRAIGHT_MOVE == devices.memory[0])
        {  
            devices.speedControl[0] = Constants.STEERING_CONTROL_IDX0;
            devices.speedControl[1] = Constants.SPEED_2F;
            devices.memory[0] = Constants.STATE_CHECK_FOR_GRASS;
        }
        if (Constants.STATE_CHECK_FOR_GRASS == devices.memory[0])
        {      
            if(Constants.GRASS == devices.pixels[5,8,1]){
                devices.speedControl[0] = Constants.STEERING_CONTROL_IDX0;
                devices.speedControl[1] = Constants.STEERING_CONTROL_NORMAL;
                devices.memory[0] = Constants.STATE_GRASS_DETACTED;
            }
        }

        if (Constants.STATE_LEFT_MOVE_DONE == devices.memory[0])
        {
            devices.speedControl[0] = Constants.STEERING_CONTROL_IDX0;
            devices.speedControl[1] = Constants.SPEED_2F;
            devices.memory[0] = Constants.STATE_READY_TO_MOVE_TOWARDS_BRIDGE;
        }
        if (Constants.STATE_READY_TO_MOVE_TOWARDS_BRIDGE == devices.memory[0])
        {
            if(Constants.GRASS == devices.pixels[3,8,1]){
                devices.speedControl[0] = Constants.STEERING_CONTROL_IDX0;
                devices.speedControl[1] = Constants.STEERING_CONTROL_NORMAL;
                devices.memory[0] = Constants.STATE_GRASS_DETACTED_NEAR_BRIDGE;
            }
        }
        
        if (Constants.STATE_MOVE_TOWARDS_DEST == devices.memory[0])
        {    
            if (Constants.BRIDGE != devices.pixels[4,8,1])
            {
                devices.speedControl[0] = Constants.STEERING_CONTROL_IDX0;
                devices.speedControl[1] = Constants.STEERING_CONTROL_NORMAL;
                devices.transmitterControl[0] = Constants.TX_CONTROL_NORMAL;
                devices.memory[0] = Constants.STATE_DESTINATION_STOP;
            }            
        }
        if (Constants.STATE_TX_CONTROL_AND_READY_MOVE_TOWARDS_DEST == devices.memory[0])
        {  
            devices.transmitterControl[0] = Constants.STEERING_CONTROL_IDX0;
            devices.transmitterControl[1] = Constants.TX_CONTROL_WHILE_CROSSING_BRIDGE;
            devices.speedControl[0] = Constants.STEERING_CONTROL_IDX0;
            devices.speedControl[1] = Constants.SPEED_10F;
            devices.memory[0] = Constants.STATE_MOVE_TOWARDS_DEST;
        }
    }
}
