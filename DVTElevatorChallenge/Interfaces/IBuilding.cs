using DVTElevatorChallenge.Models;

namespace DVTElevatorChallenge.Interfaces
{
    public interface IBuilding
    {
        public string RequestElevator(int floor, int destinationFloor, int numberOfPeople);

        public Task SimulateElevatorMovement();

        public Dictionary<int, ElevatorState> GetElevatorStatus();
    }
}
