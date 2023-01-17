using DVTElevatorChallenge.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVTElevatorChallenge.Models
{
    public class ElevatorState
    {
        public int CurrentFloor { get; set; }
        public int Capacity { get; set; }
        public int NumberOfPassengers { get; set; } = 0;
        public List<int> DestinationFloors { get; set; } = new();
        public ElevatorStatus Direction { get; set; } = ElevatorStatus.Idle;
        public List<int> PassengerDestinationFloors { get; set; } = new();
    }
}
