using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskList : MonoBehaviour
{
    protected TaskInterface[] tasks = new TaskInterface[] 
    {
        new Debug_Task(),
        new DebugLog(),         //TODO:@ add By deep
        new InitialPosTask(),
        new RightWallFollow(),
        new WallFinder(),
        new SearchParkingSlot(),
        new ParkingTask(),
        new ObstacleDetect(),
        
      
    };

    public TaskInterface[] GetTasks() {
        return this.tasks;
    }
}
