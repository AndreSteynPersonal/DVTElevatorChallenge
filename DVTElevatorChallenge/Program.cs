namespace DVTElevatorChallenge
{
    internal class Program
    {
        private const string QUIT = "q";
        static void Main(string[] _)
        {
            var input = string.Empty;
            var numberOfFloors = 0;
            var capacity = 0;
            var numberOfElevators = 0;

            Console.WriteLine($"Welcome to the virtual building! Press {QUIT} at any point to quit.");

            while (numberOfFloors == 0 || capacity == 0 || numberOfElevators == 0 && input != QUIT)
            {
                input = string.Empty;

                if (numberOfFloors == 0)
                {
                    Console.WriteLine("How many floors does the building have?");
                    input = Console.ReadLine();
                    if (!int.TryParse(input, out numberOfFloors) || numberOfFloors <= 0)
                    {
                        PromptValidInput();
                        continue;
                    }
                }

                if (numberOfElevators == 0)
                {
                    Console.WriteLine("How many elevators are installed?");
                    input = Console.ReadLine();
                    if (!int.TryParse(input, out numberOfElevators) || numberOfElevators <= 0)
                    {
                        PromptValidInput();
                        continue;
                    }
                }

                if (capacity == 0)
                {
                    Console.WriteLine("How many people can fit into a single elevator?");
                    input = Console.ReadLine();
                    if (!int.TryParse(input, out capacity) || capacity <= 0)
                    {
                        PromptValidInput();
                        continue;
                    }
                }
            }

            if (numberOfFloors > 0 && capacity > 0 && numberOfElevators > 0 && input != QUIT)
            {
                Building building = new(numberOfElevators, capacity, numberOfFloors);
                while (true)
                {
                    Console.WriteLine($"Which floor are you on?");
                    input = Console.ReadLine();
                    if (input == QUIT)
                        break;
                    else
                    {
                        if (int.TryParse(input, out int floor))
                        {
                            Console.WriteLine("How many people are waiting on your floor?");
                            input = Console.ReadLine();
                            if (int.TryParse(input, out int numberOfPeople))
                            {
                                if(numberOfPeople > capacity)
                                {
                                    var capacityWarningString = capacity == 1 ? $"{capacity} person" : $"{capacity} people";
                                    Console.WriteLine($"An elevator can only carry {capacityWarningString}.");
                                }

                                Console.WriteLine("Which floor would you like to go to?");
                                input = Console.ReadLine();
                                
                                if (int.TryParse(input, out int destinationFloor))
                                {
                                    Console.WriteLine(building.RequestElevator(floor, destinationFloor, numberOfPeople));

                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("** ELEVATOR STATUS BEFORE SIMULATION **");
                                    Console.ResetColor();

                                    ShowElevatorStatus(building);

                                    building.SimulateElevatorMovement();

                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("** ELEVATOR STATUS AFTER SIMULATION **");
                                    Console.ResetColor();

                                    ShowElevatorStatus(building);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input. Please enter a valid number of people.");
                                }
                            }
                            else
                            {
                                PromptValidInput();
                            }
                        }
                        else
                        {
                            PromptValidInput();
                        }
                    }
                }
            }

            Console.WriteLine("Thank you for allowing us to elevate you today :-)");
        }

        private static void ShowElevatorStatus(Building building)
        {
            var elevatorStates = building.GetElevatorStatus();
            foreach (var elevatorState in elevatorStates)
            {
                Console.WriteLine("-----------------------------------------------------------------------------------------------------------------");
                var movementString = elevatorState.Value.Direction == Enums.ElevatorStatus.Idle ? Enums.ElevatorStatus.Idle.ToString() : $"moving {elevatorState.Value.Direction}";
                var capacityString = elevatorState.Value.NumberOfPassengers == 1 ? $"{elevatorState.Value.NumberOfPassengers} person" : $"{elevatorState.Value.NumberOfPassengers} people";
                Console.WriteLine($"Elevator {elevatorState.Key} is on floor {elevatorState.Value.CurrentFloor} and is {movementString}. Carrying {capacityString}.");

                if(elevatorState.Key == elevatorStates.Count)
                {
                    Console.WriteLine("-----------------------------------------------------------------------------------------------------------------");
                }
            }
        }

        private static void PromptValidInput()
        {
            Console.WriteLine($"Please provide valid input or press {QUIT} to quit.");
        }
    }
}