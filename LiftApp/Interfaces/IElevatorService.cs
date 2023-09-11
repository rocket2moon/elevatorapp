using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiftApp.Interfaces
{
    public interface IElevatorService
    {
        void PressFloor(string floorNumer);
        void OpenDoor();
        void CloseDoor();
        void StartMoving( string floorNumer);
        int NextFloor();
        void GoToFloor(int floorNumber);
        void LogInfo(string message);
    }
}
