using DVTElevatorChallenge.Enums;
using DVTElevatorChallenge.Interfaces;
using DVTElevatorChallenge.Models;

namespace DVTElevatorChallenge
{
    public class Elevator : IElevator
    {
        public ElevatorState ElevatorState;
        public int ElevatorNumber = 0;

        public Elevator(int capacity, int elevatorNumber)
        {
            ElevatorState = new()
            {
                Capacity = capacity,
                CurrentFloor = 1
            };
            ElevatorNumber = elevatorNumber;
        }

        public bool AddPassenger(int destinationFloor)
        {
            if (!IsAtCapacity())
            {
                ElevatorState.NumberOfPassengers++;
                if(!ElevatorState.PassengerDestinationFloors.Contains(destinationFloor))
                {
                    ElevatorState.PassengerDestinationFloors.Add(destinationFloor);
                }
                AddDestinationFloor(destinationFloor);
                return true;
            }
            return false;
        }

        public void RemovePassenger(int floor)
        {
            if (ElevatorState.PassengerDestinationFloors.Contains(floor))
            {
                for(int i = ElevatorState.NumberOfPassengers; i > 0; i--)
                {
                    ElevatorState.NumberOfPassengers--;
                }
                ElevatorState.PassengerDestinationFloors.Remove(floor);
            }
        }

        public bool IsAtCapacity()
        {
            return ElevatorState.NumberOfPassengers >= ElevatorState.Capacity;
        }

        public void AddDestinationFloor(int floor)
        {
            if (!ElevatorState.DestinationFloors.Contains(floor))
            {
                ElevatorState.DestinationFloors.Add(floor);
            }
            
            if (ElevatorState.CurrentFloor < floor)
                ElevatorState.Direction = ElevatorStatus.Up;
            else if (ElevatorState.CurrentFloor > floor)
                ElevatorState.Direction = ElevatorStatus.Down;
            else
                ElevatorState.Direction = ElevatorStatus.Idle;
        }

        public void RemoveDestinationFloor(int floor)
        {
            ElevatorState.DestinationFloors.Remove(floor);
            if (ElevatorState.DestinationFloors.Count > 0)
            {
                if (ElevatorState.CurrentFloor < ElevatorState.DestinationFloors[0])
                    ElevatorState.Direction = ElevatorStatus.Up;
                else if (ElevatorState.CurrentFloor > ElevatorState.DestinationFloors[0])
                    ElevatorState.Direction = ElevatorStatus.Down;
                else
                    ElevatorState.Direction = ElevatorStatus.Idle;
            }
            else
                ElevatorState.Direction = ElevatorStatus.Idle;
        }

        public void Move()
        {
            if (ElevatorState.DestinationFloors.Count > 0)
            {
                if (ElevatorState.CurrentFloor < ElevatorState.DestinationFloors[0])
                    for (int i = ElevatorState.CurrentFloor; i < ElevatorState.DestinationFloors[0]; i++)
                    {
                        ElevatorState.CurrentFloor++;
                    }
                else if (ElevatorState.CurrentFloor > ElevatorState.DestinationFloors[0])
                    for (int i = ElevatorState.CurrentFloor; i > ElevatorState.DestinationFloors[0]; i--)
                    {
                        ElevatorState.CurrentFloor--;
                    }

                if (ElevatorState.CurrentFloor == ElevatorState.DestinationFloors[0])
                {
                    RemovePassenger(ElevatorState.DestinationFloors[0]);
                    RemoveDestinationFloor(ElevatorState.DestinationFloors[0]);
                }
            }
        }
    }
}
