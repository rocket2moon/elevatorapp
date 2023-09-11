using LiftApp.Interfaces;
using LiftApp.Services;

namespace LiftApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IElevatorService elevator = new ElevatorService(100);
            string floor = "5U";
            var thread = new Thread(()=>elevator.StartMoving(floorNumer: floor));
            thread.Start();

            //elevator.PressFloor(8);
            //elevator.PressFloor(2);
            //elevator.PressFloor(1);

            Thread.Sleep(1000);

            //elevator.PressFloor(3);
        }
    }
}