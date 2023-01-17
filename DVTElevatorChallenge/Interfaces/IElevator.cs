namespace DVTElevatorChallenge.Interfaces
{
    internal interface IElevator
    {
        public bool AddPassenger(int destinationFloor);

        public void RemovePassenger(int floor);

        public bool IsAtCapacity();

        public void AddDestinationFloor(int floor);

        public void RemoveDestinationFloor(int floor);

        public void Move();
    }
}
