using System;
using System.Linq;

namespace project2
{
    // I decided to approach this problem using classes 
    // First I made a class to hold all my global variables that can be accessed later
    public static class GlobalVariables {
        // This holds an array of floors of the building
        public static readonly int[] floors = {-1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
        // This holds the number of floors, only used for the main class to access the last element
        public static int numberOfFloors = floors.Length;
    }
    // Here is my class elevator
    class Elevator {
        // currFloor acts like a pointer to the current floor that the elevator is on while no access lets the elevator know which
        // floors can't be accessed by the elevator, I could change it to an array or list 
        private int currFloor = 0, noAccess;
        // This holds the name of the elevator, in this case 'A' or 'B' 
        private char elevatorID;
        // So timer holder the time for each elevator to move from one floor to another
        // total time holds the total time of the whole operation of the elevator
        // number of trips holds the number of times the elevator can move
        public int timer = 0, total_time = 0, noOftrips = 0;
        // So I made a property to access the total time
        public int totalTime {
            get => total_time;
        }
        // And another property to access of number of trips
        public int noOfTrips {
            get => noOftrips;
        }
        // My elevator constructor will take an a char which is the ID
        // and the floor that will not be accessed by the elevator, since
        // it is assumed that both elevators are working on the same building with the same 
        // number of floors
        public Elevator(char ID, int excludedFloor) {
            elevatorID = ID;
            noAccess = excludedFloor;
        }
        // The move to floor function will move the elevator when the elevator is allowed to access the floor it will go to
        public void moveToFloor(int startingFloor, int destinationFloor) {
            // This checks if the floor the user is on, is in the array of floors
            if(!GlobalVariables.floors.Contains(startingFloor)) {
                Console.WriteLine("Floor {0} is not on the list of floors for elevator {1}\n", startingFloor, elevatorID);
                return;
            }
            // This checks if the floor the user is going to, is in the array of floors
            if(!GlobalVariables.floors.Contains(destinationFloor)) {
                Console.WriteLine("Floor {0} is not on the list of floors for elevator {1}\n", destinationFloor, elevatorID);
                return;
            }
            // I check if the the floor the user wants to go to is accessible or not
            if(destinationFloor == noAccess || !GlobalVariables.floors.Contains(destinationFloor)) {
                Console.WriteLine("Elevator {0} has no Access to floor {1}\n", elevatorID, destinationFloor);
                return;
            }
            // Prints out the current floor of the elevator
            Console.WriteLine("Elevator {0} is currently on floor {1}", elevatorID, currFloor);
            // Calculates the time for the elevator to go to the floor of the user and bring the user to their floor
            timer = Math.Abs(currFloor - startingFloor) + Math.Abs(destinationFloor - startingFloor);
            // Sums up all the times together
            total_time += timer;
            // Increments by 1 to know how many times it moved
            noOftrips += 1;
            Console.WriteLine("Elevator {0} will take {1} seconds to reach floor {2} and then go to floor {3}", elevatorID, timer, startingFloor, destinationFloor);
            // Prints that the door opens
            openDoor();
            // Prints that the door closes, which means the trip will be made
            closeDoor();
            // Finally I set the current floor of the elevator to the floor that the elevator has dropped off the user
            currFloor = destinationFloor;
        }
        // Open function
        public void openDoor() {
            Console.WriteLine("{0}: Open doors", elevatorID);
        }
        // Close function
        public void closeDoor() {
            Console.WriteLine("{0}: Close door\n", elevatorID);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // This simulates the elevator trips to random inputs
            Console.WriteLine("ELEVATOR SIMULATION\n");
            // Initialized the random class
            Random random = new Random();
            // Initialized the two elevators
            Elevator elevatorA = new Elevator('A', GlobalVariables.floors[GlobalVariables.numberOfFloors - 1]);
            Elevator elevatorB = new Elevator('B', GlobalVariables.floors[0]);
            // Made a random number int that is between 0-50
            int randomNumOfMoves = random.Next(0, 50);

            // This simulates trips on elevator A and B, it goes this way:
            // 1. it will create a random starting floor and floor where the user wants to be dropped off
            // 2. then the move function will start the elevator from floor 0 and then go the user if possible, then continue
            //      from that floor to the floor where the next user is and deliver them to the floor they want to be dropped off
            // 3. loops till the end of random number of moves
            // This prints out all the necessary information to know how long each elevator was in use, how many attempts and the total 
            // number of trips the elevator has successfully made for each user.
            // Only opens if the user is using an elevator that can access the floor they are on and can go to the floor the user wishes to go to

            Console.WriteLine("Starting Elevator A...");
            for(int i = 0; i < randomNumOfMoves; i++) {
                int startingFloorRandom = random.Next(-1, 12); // generates a random number from -1 to 11
                int destinationFloorRandom = random.Next(-1, 11); // generates a random number from -1 to 11
                elevatorA.moveToFloor(startingFloorRandom, destinationFloorRandom);
            }
            Console.WriteLine("Elevator A had {0} attempts and {1} successful moves and total time of operation was {2} seconds\n", randomNumOfMoves, elevatorA.noOfTrips, elevatorA.totalTime);

            Console.WriteLine("Starting Elevator B...");
            randomNumOfMoves = random.Next(0, 50);
            for(int i = 0; i < randomNumOfMoves; i++) {
                int startingFloorRandom = random.Next(-1, 11); // generates a random number from -1 to 11
                int destinationFloorRandom = random.Next(-1, 11); // generates a random number from -1 to 11
                elevatorB.moveToFloor(startingFloorRandom, destinationFloorRandom);
            }
            Console.WriteLine("Elevator B had {0} attempts and {1} successful moves and total time of operation was {2} seconds\n", randomNumOfMoves, elevatorB.noOfTrips, elevatorB.totalTime);
        }
    }
}
