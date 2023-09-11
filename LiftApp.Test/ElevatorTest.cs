using System;
using System.IO;
using LiftApp.Interfaces;
using LiftApp.Model;
using LiftApp.Model.Constants;
using LiftApp.Services;
using Xunit;

namespace LiftApp.Tests
{
    public class ElevatorServiceTests
    {
        private const int MaxWeightLimit = 1000;

        [Fact]
        public void ElevatorService_Constructor_MaxWeightLimitSet()
        {
            var elevatorService = new ElevatorService(MaxWeightLimit);

            Assert.Equal(MaxWeightLimit, elevatorService.elevator.maxWeightLimit);
        }

        [Fact]
        public void OpenDoor_DoorClosed_DoorOpened()
        {
            var elevatorService = new ElevatorService(MaxWeightLimit);
            elevatorService.OpenDoor();

            Assert.True(elevatorService.elevator.isDoorOpened);
        }

        [Fact]
        public void CloseDoor_DoorOpened_DoorClosed()
        {
            var elevatorService = new ElevatorService(MaxWeightLimit);
            elevatorService.OpenDoor(); // Open the door first
            elevatorService.CloseDoor();

            Assert.False(elevatorService.elevator.isDoorOpened);
        }


        [Fact]
        public void PressFloor_DoorOpened_NoAction()
        {
            var elevatorService = new ElevatorService(MaxWeightLimit);
            elevatorService.OpenDoor(); // Open the door first
            elevatorService.PressFloor("2U");

            Assert.Empty(elevatorService.elevator.floorRequests);
        }

        [Fact]
        public void AddQueue_AddFloorToQueue()
        {
            var elevatorService = new ElevatorService(MaxWeightLimit);
            elevatorService.AddQueue("3D");

            Assert.Single(elevatorService.elevator.floorRequests);
        }

        [Fact]
        public void StartMoving_SingleFloorRequest_ElevatorMoves()
        {
            var elevatorService = new ElevatorService(MaxWeightLimit);
            elevatorService.PressFloor("3U");
            elevatorService.StartMoving("3U");

            Assert.Equal(3, elevatorService.elevator.currentFloor);
        }

        [Fact]
        public void GoToFloor_ElevatorMovesToTargetFloor()
        {
            var elevatorService = new ElevatorService(MaxWeightLimit);
            elevatorService.elevator.currentFloor = 1;
            elevatorService.GoToFloor(3);

            Assert.Equal(3, elevatorService.elevator.currentFloor);
        }

        [Fact]
        public void NextFloor_ElevatorGoingUp_ReturnsNextFloor()
        {
            var elevatorService = new ElevatorService(MaxWeightLimit);
            elevatorService.elevator.currentFloor = 2;
            elevatorService.elevator.ElevatorOptions = ElevatorOptions.GO_UP;
            elevatorService.elevator.floorRequests.Add(new FlloorDirection { FloorNumber = 3, Direction = "U" });

            var nextFloor = elevatorService.NextFloor();

            Assert.Equal(3, nextFloor);
        }

        [Fact]
        public void NextFloor_ElevatorGoingDown_ReturnsNextFloor()
        {
            var elevatorService = new ElevatorService(MaxWeightLimit);
            elevatorService.elevator.currentFloor = 3;
            elevatorService.elevator.ElevatorOptions = ElevatorOptions.GOD_OWN;
            elevatorService.elevator.floorRequests.Add(new FlloorDirection { FloorNumber = 2, Direction = "D" });

            var nextFloor = elevatorService.NextFloor();

            Assert.Equal(2, nextFloor);
        }

        [Fact]
        public void LogInfo_LogMessageLoggedToFileAndConsole()
        {
            var elevatorService = new ElevatorService(MaxWeightLimit);
            var logFilePath = @"C:\Users\DELL\Desktop\log.log";

            elevatorService.LogInfo("Test Log Message");

            var lastLogLine = File.ReadAllLines(logFilePath)[^1]; // Get the last line in the log file

            Assert.EndsWith("Test Log Message", lastLogLine);
        }

       
    }
}
