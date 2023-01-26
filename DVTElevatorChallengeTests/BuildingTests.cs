using DVTElevatorChallenge;
using DVTElevatorChallenge.Enums;
using NUnit.Framework;

namespace DVTElevatorChallengeTests
{
    [TestFixture]
    public class BuildingTests
    {
        [Test]
        public async Task TestElevatorMovement()
        {
            Building building = new(3, 10, 20);

            building.RequestElevator(15, 18, 2);

            // Check if the elevator is moving to the correct floor
            Assert.That(building.elevators[0].ElevatorState.Direction, Is.EqualTo(ElevatorStatus.Up));
            
            await building.SimulateElevatorMovement();

            // Check if the elevator is on the correct floor
            Assert.That(building.elevators[0].ElevatorState.CurrentFloor, Is.EqualTo(18));

            building.RequestElevator(14, 13, 2);
            
            // Check if the elevator is moving to the correct floor
            Assert.That(building.elevators[0].ElevatorState.CurrentFloor, Is.EqualTo(14));
            Assert.That(building.elevators[0].ElevatorState.Direction, Is.EqualTo(ElevatorStatus.Down));

            await building.SimulateElevatorMovement();

            // Check if the passenger(s) removed from the elevator once they reach their destination floor
            Assert.That(building.elevators[0].ElevatorState.NumberOfPassengers, Is.EqualTo(0));
        }

        [Test]
        public void TestNearestElevator()
        {
            Building building = new(3, 10, 20);

            building.elevators[0].ElevatorState.CurrentFloor = 6;
            building.elevators[1].ElevatorState.CurrentFloor = 3;
            building.elevators[2].ElevatorState.CurrentFloor = 1;

            building.RequestElevator(4, 7, 2);

            // Check if the nearest elevator is selected
            Assert.That(building.nearestElevator?.ElevatorNumber, Is.EqualTo(building.elevators[1].ElevatorNumber));
        }

        [Test]
        public void TestElevatorCapacity()
        {
            Building building = new(3, 10, 20);

            // Add more people to the elevator than its capacity
            for (int i = 0; i < 12; i++)
            {
                building.elevators[0].AddPassenger(1);
            }

            // Check if the elevator is at capacity
            Assert.That(building.elevators[0].IsAtCapacity(), Is.True);
        }

        [Test]
        public void TestInvalidFloor()
        {
            Building building = new(3, 10, 20);
            var response = building.RequestElevator(21, 22, 2);
            Assert.That(response, Does.Contain("Invalid floor number. Number of floors in building is 20"));
        }
        [Test]
        public void TestInvalidDestinationFloor()
        {
            Building building = new(3, 10, 20);
            var response = building.RequestElevator(2, 22, 2);
            Assert.That(response, Does.Contain("Invalid destination floor number. Number of floors in building is 20"));
        }
    }
}