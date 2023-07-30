using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskList : MonoBehaviour
{
    protected TaskInterface[] tasks = new TaskInterface[] 
    {
        new InitialPosTask(),
        new RightWallFollow(),
        new WallFinder(),
        new SearchParkingSlot(),
        new ParkingTask()
      
    };

    public TaskInterface[] GetTasks() {
        return this.tasks;
    }
}
