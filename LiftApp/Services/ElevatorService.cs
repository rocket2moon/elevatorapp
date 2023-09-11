using LiftApp.Interfaces;
using LiftApp.Model;
using LiftApp.Model.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiftApp.Services
{
    public class ElevatorService : IElevatorService
    {
        public Elevator elevator;
        public ElevatorService(
            int maxWeightLimit
            )
        {
            elevator = new Elevator(maxWeightLimit);
         }

        public void OpenDoor()
        {
            if (elevator.isDoorOpened == true)
            {
                LogInfo($"The door is Opened at {elevator.currentFloor} floor");
                Console.WriteLine($"The door is Opened at {elevator.currentFloor} floor");
            }
            else
            {
                elevator.isDoorOpened = true;
                LogInfo($"The door is Opened at {elevator.currentFloor} floor");
                Console.WriteLine($"The door is Opened at {elevator.currentFloor} floor");

            }

        }
        public void CloseDoor()
        {
            if (elevator.isDoorOpened == true)
            {
                elevator.isDoorOpened = false;
                LogInfo($"The door is closed at {elevator.currentFloor} floor");
                Console.WriteLine($"The door is closed at {elevator.currentFloor} floor");
            }
            else
            {
                elevator.isDoorOpened = false;
                LogInfo($"The door is closed at {elevator.currentFloor} floor");
                Console.WriteLine($"The door is closed at {elevator.currentFloor} floor");
            }
        }

        public void PressFloor(string floorNumer)
        {
            if (elevator.isDoorOpened == false)
            {
                var splitted  =  floorNumer.ToCharArray();
                int floorNum = Convert.ToInt32(splitted[0].ToString());
                string direction = splitted[1].ToString();
                if (floorNum == elevator.currentFloor 
                    || elevator.floorRequests.Select(x=>x.FloorNumber).Contains(floorNum))
                {
                    LogInfo($"Elevator is already on {floorNum} floor");
                    Console.WriteLine($"Elevator is already on {floorNum} floor");
                    return;
                }
                elevator.floorRequests.Add(new FlloorDirection 
                { FloorNumber = floorNum, Direction = direction});
                if (!elevator.isRunning)
                {
                    StartMoving(floorNumer);
                }
            }
            
        }

        public  void AddQueue(string floorNumber)
        {
            var splitted = floorNumber.ToCharArray();
            int floorNum = Convert.ToInt32(splitted[0].ToString());
            string direction = splitted[1].ToString();
            if (elevator.floorRequests.Count < 1)
            {
                elevator.floorRequests.Add(new FlloorDirection
                {
                    Direction = direction,
                    FloorNumber = floorNum
                });
            }
            
        }
        public void StartMoving( string floorNumer)
        {
            elevator.isRunning = true;
            LogInfo($"Please supply your floor");

            
            var splitted = floorNumer.ToCharArray();
            int floorNum = Convert.ToInt32(splitted[0].ToString());
            string direction = splitted[1].ToString();
            elevator.floorRequests.Add(new FlloorDirection
            {
                Direction = direction,
                FloorNumber = floorNum
            });
            Console.WriteLine($"Supply Floor Number");
            
            while (elevator.floorRequests.Count > 0 )
            {
                LogInfo($"Elevator is leaving  {elevator.currentFloor} floor");

                int nextFloor = NextFloor();
                LogInfo($"Elevator is moving to {nextFloor} floor");

                if (nextFloor > elevator.currentFloor && direction == "U")
                {
                    elevator.ElevatorOptions = ElevatorOptions.GO_UP;
                    GoToFloor(nextFloor);
                }
                if (nextFloor > elevator.currentFloor && direction == "D")
                {
                    elevator.ElevatorOptions = ElevatorOptions.GOD_OWN;
                    nextFloor  = elevator.currentFloor - 1;
                    GoToFloor(nextFloor);
                }
                if (nextFloor < elevator.currentFloor && direction == "D")
                {
                    nextFloor = elevator.currentFloor - 1;
                    elevator.ElevatorOptions = ElevatorOptions.GOD_OWN;
                    GoToFloor(nextFloor);
                }

                //GoToFloor(nextFloor);

                if (elevator.maxWeightLimit > 100)
                {
                    GoToFloor(nextFloor);
                    OpenDoor();
                }
                
                if (elevator.floorRequests.Select(x=>x.FloorNumber).Contains(nextFloor))
                {
                    var remover  = elevator.floorRequests.Where(x=>x.FloorNumber == nextFloor && x.Direction == direction).FirstOrDefault();
                    elevator.floorRequests.Remove(remover);
                    OpenDoor();
                    if (!string.IsNullOrEmpty(floorNumer))
                    {
                        floorNumer = Console.ReadLine();
                        AddQueue(floorNumer);
                        splitted = floorNumer.ToCharArray();
                        floorNum = Convert.ToInt32(splitted[0].ToString());
                        direction = splitted[1].ToString();
                        floorNumer = string.Empty;
                    }
                    elevator.currentFloor = floorNum;
                }


            }

            elevator.ElevatorOptions = ElevatorOptions.STOP_MOVING;
            elevator.isRunning = false;
        }
        public void GoToFloor(int floorNumber)
        {
            while (elevator.currentFloor != floorNumber)
            {
                Thread.Sleep(3000);
                if (floorNumber > elevator.currentFloor)
                {
                    elevator.currentFloor += 1;
                }

                if (floorNumber < elevator.currentFloor)
                {
                    elevator.currentFloor -= 1;
                }
                LogInfo($"Elevator has arrived {floorNumber} floor");
            }
        }

        public int NextFloor()
        {
            int nextFloor = elevator.currentFloor;
            foreach (var floor in elevator.floorRequests)
            {

                if ( floor.FloorNumber > elevator.currentFloor)
                {
                    nextFloor  = elevator.currentFloor+1;
                        
                        //floor.FloorNumber;
                }
                else if (floor.FloorNumber < elevator.currentFloor)
                {
                    nextFloor = elevator.currentFloor - 1;
                        
                        //floor.FloorNumber;
                }
            }
            return nextFloor;
        }

        public void LogInfo(string message)
        {
            var logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}";
            File.AppendAllText(@"C:\Users\DELL\Desktop\log.log", logMessage+ Environment.NewLine);
            Console.WriteLine(logMessage);
        }
    }
}
