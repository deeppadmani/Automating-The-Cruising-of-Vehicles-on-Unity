using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  Jobs
{
        public const float STATE_INITIAL_ORIENTATION = 0; 
        public const float STATE_TAKING_INITIAL_POSITION = 1; 
        public const float STATE_INITIAL_STATE_REACHED = 2; 
        public const float STATE_INITIAL_DIRECTION_CALIBRATION = 3; 
        public const float STATE_MOVING_FORWARD_WITH_3F_SPEED = 4; 
        public const float STATE_OBJECT_DETECTION = 5; 
        public const float STATE_OBJECT_DETECTED = 6; 
        public const float STATE_WALL_DETECTED = 7; 
        public const float STATE_FUTURE_USE = 8; 
        public const float STATE_MOVING_FORWARD_WITH_1F_SPEED = 9; 
        public const float STATE_LANE_DETECTION = 10; 
        public const float STATE_TURNING_RIGHT = 11; 
        public const float STATE_TURNING_LEFT = 12; 
        public const float STATE_U_TURN_1 = 13; 
        public const float STATE_U_TURN_2 = 14; 
        public const float STATE_PARKING_DETECTED = 15; 
        public const float STATE_READY_FOR_PARKING = 16; 
        public const float STATE_PARKING_EGO_VEHICLE = 17; 
        public const float STATE_PARKING_DONE = 18;
}

public class InitialPosTask : TaskInterface
{
    public void Execute(DeviceRegistry devices) 
    {
        if (Jobs.STATE_INITIAL_ORIENTATION == devices.memory[0])             // 0 --> init
        {
            if(27 < devices.gps[1])        
            {
                    if (devices.compass[0] > 179f)
                    {
                        // Left 
                        
                        devices.steeringControl[0] = 1f;
                        devices.steeringControl[1] = -0.5f;
                    }
                    else if (devices.compass[0] < 360f-179f)
                    {
                        // Right
                        
                        devices.steeringControl[0] = 1f;
                        devices.steeringControl[1] = 0.5f;
                    }
                    else
                    {
                        devices.steeringControl[0] = 0f;
                    }

                    if ((devices.compass[0] > 179f) && 
                    (devices.compass[0] < 360f-179f))
                    {
                        devices.memory[0] = Jobs.STATE_TAKING_INITIAL_POSITION; // init pos GPSstate 1
                    } 
            }
            else{
                    devices.memory[0] = Jobs.STATE_INITIAL_DIRECTION_CALIBRATION;      // init pos GPSstate 1
            }
        }
        if(Jobs.STATE_TAKING_INITIAL_POSITION == devices.memory[0]) // init pos state 1
        {
                 devices.steeringControl[0] = 1f;
                 devices.steeringControl[1] = 0f;

                 devices.speedControl[0] = 1f;
                 devices.speedControl[1] = 1f;
                 if(28f > devices.gps[1])            
                 {
                        devices.memory[0] = Jobs.STATE_INITIAL_STATE_REACHED;      // init pos GPSstate 2
                 }
        }
        if(Jobs.STATE_INITIAL_STATE_REACHED == devices.memory[0])          // init pos GPSstate 2
        {
                devices.speedControl[0] = 1f;
                devices.speedControl[1] = 0f;
                devices.memory[0] = Jobs.STATE_INITIAL_DIRECTION_CALIBRATION;  //INIT POS DIR
        }
        if(Jobs.STATE_INITIAL_DIRECTION_CALIBRATION == devices.memory[0])
        {
            if (devices.compass[0] > 91f)
            {
                 // Left 
                 devices.steeringControl[0] = 1f;
                 devices.steeringControl[1] = -0.5f;
            }
            else if (devices.compass[0] < 89f)
            {
                 // Right
                 devices.steeringControl[0] = 1f;
                 devices.steeringControl[1] = 0.5f;
            }
            else
            {
                devices.steeringControl[0] = 0f;
            }

            if((devices.compass[0] < 91f) && 
            (devices.compass[0] > 89f) )
            {
                devices.steeringControl[0] = 1f;
                devices.steeringControl[1] = 0f;
                devices.memory[0] = Jobs.STATE_MOVING_FORWARD_WITH_3F_SPEED;      //mov fwd
            }

        }
    }
}
