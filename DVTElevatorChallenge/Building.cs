using DVTElevatorChallenge.Interfaces;
using DVTElevatorChallenge.Models;

namespace DVTElevatorChallenge
{
    public class Building : IBuilding
    {
        public int NumberOfElevators { get; set; }
        public int Capacity { get; set; }
        public int NumberOfFloors { get; set; }
        public List<Elevator> elevators;
        public Elevator? nearestElevator;

        public Building(int numberOfElevators, int capacity, int numberOfFloors)
        {
            NumberOfElevators = numberOfElevators;
            Capacity = capacity;
            NumberOfFloors = numberOfFloors;
            elevators = new List<Elevator>();
            for (int i = 1; i <= numberOfElevators; i++)
            {
                elevators.Add(new Elevator(capacity, i));
            }
        }

        public string RequestElevator(int floor, int destinationFloor, int numberOfPeople)
        {
            if (floor < 1 || floor > NumberOfFloors)
            {
                return $"Invalid floor number. Number of floors in building is {NumberOfFloors}";
            }

            if (destinationFloor < 1 || destinationFloor > NumberOfFloors)
            {
                return $"Invalid destination floor number. Number of floors in building is {NumberOfFloors}";
            }

            var elevatorIndex = 0;
            nearestElevator = GetNearestElevator(floor, elevatorIndex);
            var result = "All elevators are currently at capacity.";
            if (nearestElevator != null)
            {
                var passengerCheck = nearestElevator.ElevatorState.NumberOfPassengers + numberOfPeople;
                if(passengerCheck <= nearestElevator.ElevatorState.Capacity)
                {
                    result = AddPassengers(nearestElevator, numberOfPeople, destinationFloor, floor);
                }
                else
                {
                    while (passengerCheck > nearestElevator.ElevatorState.Capacity && elevatorIndex <= elevators.Count - 1)
                    {
                        elevatorIndex++;

                        nearestElevator = GetNearestElevator(floor, elevatorIndex);

                        passengerCheck = nearestElevator.ElevatorState.NumberOfPassengers + numberOfPeople;

                        if (passengerCheck <= nearestElevator.ElevatorState.Capacity)
                        {
                            result = AddPassengers(nearestElevator, numberOfPeople, destinationFloor, floor);
                            break;
                        }
                    }
                }

                return result;
            }
            else
            {
                return result;
            }
        }

        public async Task SimulateElevatorMovement()
        {
            List<Task> tasks = new List<Task>();
            foreach (Elevator elevator in elevators)
            {
                tasks.Add(elevator.Move());
            }    

            await Task.WhenAll(tasks);
        }

        public Dictionary<int, ElevatorState> GetElevatorStatus()
        {
            var elevatorStates = new Dictionary<int, ElevatorState>();
            foreach (Elevator elevator in elevators)
            {
                elevatorStates.Add(elevator.ElevatorNumber, elevator.ElevatorState);
            }

            return elevatorStates;
        }

        private Elevator GetNearestElevator(int floor, int index)
        {
            var elevator = elevators[index];
            for (int i = 1; i < elevators.Count; i++)
            {
                if (Math.Abs(elevators[i].ElevatorState.CurrentFloor - floor) < Math.Abs(elevator.ElevatorState.CurrentFloor - floor))
                {
                    elevator = elevators[i];
                }
            }

            return elevator;
        }

        private static string AddPassengers(Elevator elevator, int numberOfPeople, int destinationFloor, int floor)
        {
            for (int i = 0; i < numberOfPeople; i++)
            {
                elevator.AddPassenger(destinationFloor);
            }
            elevator.ElevatorState.CurrentFloor = floor;
            return $"Added {numberOfPeople} passenger(s) to elevator {elevator.ElevatorNumber}.";
        }
    }
}
