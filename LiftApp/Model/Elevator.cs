using LiftApp.Interfaces;
using LiftApp.Model.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiftApp.Model
{
    public class Elevator 
    {
        public Elevator(int maxWeightLimit)
        {
            this.maxWeightLimit = maxWeightLimit;
            this.floorRequests = new List<FlloorDirection>();
            this.currentFloor = currentFloor;
            this.ElevatorOptions = ElevatorOptions.STOP_MOVING;
            this.isRunning = false;
            this.isDoorOpened = false;
        }
        public int currentFloor { get; set; }
        public ElevatorOptions ElevatorOptions { get; set; }
        public int maxWeightLimit { get; set; }
        public int currentWeight { get; set; }
        public List<FlloorDirection> floorRequests { get; set; }
        public bool isRunning { get; set; }
        public bool isDoorOpened { get; set; }

        




    }

    public class FlloorDirection
    {
        public int FloorNumber { get; set; }
        public string Direction { get; set; }
    }
}
