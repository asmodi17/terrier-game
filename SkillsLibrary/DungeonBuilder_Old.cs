using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameLibrary;

// This requires a huge amount of refactoring.  Commenting it out until I get to it.

/*
namespace GameLibrary
{
    public enum roomType
    {
        armory,
        cafeteria,
        hallway,
        stairwell,
        room,
        bedroom,
        dungeon,
        cell,
        sunRoom,
        arboretum,
        tortureRoom,
        startingArea
    }

    public class GlobalStrings
    {
        public string[] roomTitleStrings = { "Armory", "Cafeteria", "Hallway", 
                                           "Stairwell", "Room", "Bedroom", "Dungeon", "Cell",
                                           "Sun Room", "Arboretum", "Torture Room", "Starting Area" };

        public string[] roomStrings = { "A collection of weapons is stored in one corner of the room, while an equally impressive collection of armor takes up the rest of the room.  None of it is useable, however.", 
                                      "This room smells like cabbage and burnt casseroles.",
                                      "A long hallway, with tattered rugs on the floor and faded tapestries lining the walls.  Every so often a sputtering torch lights the way.",
                                      "A winding stairwell leading in some unknown direction.",
                                      "Just a room.",
                                      "A large, soft-looking bed is the primary focus of this room.  It has silken sheets and looks very comfortable and inviting.",
                                      "This room has 4 cells, each is very dismal.  The smell of urine and feces is overwhelming.",
                                      "This cell has a single stone wall with steel bars on the remaining three sides.",
                                      "This room is furnished comfortably with a set of stained cushions arranged in a group of circles and is lit inexplicably by soft, white light that comes from nowhere.",
                                      "A collection of ornamental trees that have since overgrown their planters, this room is now overgrown with roots, branches, and leaves.",
                                      "The smell of blood permeates this room.  Implements of torture litter the room, stained black with dried blood.",
                                      "The very first room of this dungeon."};
        public readonly int CellNum = 8;

        
    }

    public class DungeonBuilder
    {
        // Dungeon will keep track of all the rooms by a special token that it will
        // enumerate as the dungeon is being built.
        // Since each room has a title and a desc, the name by which the DungeonBuilder
        // keeps track of the Rooms is irrelevant.
        public Dictionary<int, Room> Dungeon;
        Random R;
        public int numRooms;

        private Room GetRandomRoomWithAvailableExit(ExitDirections direction)
        {
            int x = 0;
            int count = numRooms * numRooms;
            while (count > 0)
            {
                x = R.Next(numRooms);
                count -= 1;
                switch (direction)
                {
                    case ExitDirections.north:
                        if (Dungeon[x].North == null) return Dungeon[x];
                        break;
                    case ExitDirections.south:
                        if (Dungeon[x].South == null) return Dungeon[x];
                        break;
                    case ExitDirections.east:
                        if (Dungeon[x].East == null) return Dungeon[x];
                        break;
                    case ExitDirections.west:
                        if (Dungeon[x].West == null) return Dungeon[x];
                        break;
                    case ExitDirections.northeast:
                        if (Dungeon[x].Northeast == null) return Dungeon[x];
                        break;
                    case ExitDirections.southeast:
                        if (Dungeon[x].Southeast == null) return Dungeon[x];
                        break;
                    case ExitDirections.northwest:
                        if (Dungeon[x].Northwest == null) return Dungeon[x];
                        break;
                    case ExitDirections.southwest:
                        if (Dungeon[x].Southwest == null) return Dungeon[x];
                        break;
                    default:
                        throw new ArgumentException("Direction was not valid: " + direction.ToString());
                }
            }
            return null;
        }

        // Since each room must be generated with an array of strings containing the exits,
        // Adding the Rooms to the Dungeon must be done carefully.
        // If we want to do this randomly, there needs to be some sort of order in which the rooms
        // are created.  Exits must always refer to a Room # when they are added to the room


        // The easiest way to randomly generate a dungeon is to create all rooms, then iterate through
        // each room and add exits which link to other rooms, generating the links one at a time
        // This leads to the problem that directionality is not preserved and the shape of the dungeon
        // is not easily determined.

        // Another way to randomly generate a dungeon is to create a single room, generate a random number
        // to represent the number of exits to generate (This will likely require some logic based on
        // how many rooms should still be created). Then create a set of rooms for each of those and add them
        // to a queue.
        // Then iterate through the Queue and perform the same steps until the desired number is reached.
        // This type of construction will generate an n-tree which does not link back to previously created
        // rooms, however.

        // The easiest type of ordered dungeon is a grid dungeon, where every room has an exit to the rooms
        // adjacent, but not diagonally.  As this type matches with the currently implemented ExitDirections,
        // this shouldn't be a problem.

        public DungeonBuilder(int numRooms, string type)
        {
            this.numRooms = numRooms;
            switch (type)
            {
                case ("Random"):
                    DungeonMakerRandom(numRooms);
                    break;
                case ("NTree"):
                    DungeonMakerNTree(numRooms);
                    break;
                case ("Grid"):
                    DungeonMakerGrid(numRooms);
                    break;
                default:
                    DungeonMakerGrid(numRooms);
                    break;
            }
        }

        // This is currently a random dungeon builder.
        /// <summary>
        /// This dungeon will have a random set of rooms that are not guaranteed to be connected to each other.
        /// </summary>
        /// <param name="numRooms"></param>
        /// <param name="Tree"></param>
        private void DungeonMakerRandom(int numRooms)
        {
            Dungeon = new Dictionary<int, Room>();
            GlobalStrings gs = new GlobalStrings();
            this.numRooms = numRooms;
            R = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            int cellCount = 0;
            for (int i = 0; i < numRooms; i++)
            {
                int numExits = 0;
                int room = R.Next(gs.roomTitleStrings.Length);
                // Do not create Cells this way, or randomly create Cells
                while (room == gs.CellNum)
                {
                    //Console.WriteLine("Bad While Loop");
                    room = R.Next(gs.roomTitleStrings.Length);
                }
                
                // Do not randomly create Cells, as they are special rooms created
                // only for Dungeons
                // Create a single Room
                Room r = new Room();
                r.Title = gs.roomTitleStrings[room];
                r.Desc = gs.roomStrings[room];

                if (gs.roomTitleStrings[room].Equals("Dungeon"))
                {
                    // do not include Cell Exits
                    numExits = 1 + R.Next(4);
                    // Create the Cells for the Dungeon
                    Room nwCell = new Room();
                    nwCell.Title = gs.roomTitleStrings[gs.CellNum];
                    nwCell.Desc = gs.roomStrings[gs.CellNum];
                    nwCell.AddExit(ExitDirections.southeast, r);
                    Dungeon.Add(numRooms + cellCount, nwCell);
                    cellCount++;

                    Room swCell = new Room();
                    swCell.Title = gs.roomTitleStrings[gs.CellNum];
                    swCell.Desc = gs.roomStrings[gs.CellNum];
                    swCell.AddExit(ExitDirections.northeast, r);
                    Dungeon.Add(numRooms + cellCount, swCell);
                    cellCount++;

                    Room neCell = new Room();
                    neCell.Title = gs.roomTitleStrings[gs.CellNum];
                    neCell.Desc = gs.roomStrings[gs.CellNum];
                    neCell.AddExit(ExitDirections.southwest, r);
                    Dungeon.Add(numRooms + cellCount, nwCell);
                    cellCount++;

                    Room seCell = new Room();
                    seCell.Title = gs.roomTitleStrings[gs.CellNum];
                    seCell.Desc = gs.roomStrings[gs.CellNum];
                    seCell.AddExit(ExitDirections.northwest, r);
                    Dungeon.Add(numRooms + cellCount, nwCell);
                    cellCount++;
                }

                Dungeon.Add(i, r);
            }

            // Now we have a Dictionary of rooms which are ready to be linked
            for (int i = 0; i < numRooms; i++)
            {
                int numExits = 0;
                // The room type will determine the number of exits
                if (Dungeon[i].Title.Equals("Hallway") || Dungeon[i].Equals("Stairwell"))
                {
                    numExits = 2;
                }
                else if (Dungeon[i].Equals("Bedroom") || Dungeon[i].Equals("Room") || Dungeon[i].Equals("Torture Room"))
                {
                    numExits = 2;
                }
                else if (Dungeon[i].Equals("Dungeon"))
                {
                    numExits = 1 + R.Next(4);
                }
                else
                {
                    // This will generate a maximum of 8 exits
                    // which is the length of the enum ExitDirections
                    numExits = 2 + R.Next(7);
                }

                // Generate links between rooms randomly, ensuring that the exits on the opposing room
                // match the directionality of the random exit direction
                // This is now done as part of the AddExit member method of the Room Class
                // Also, if there are more exits than Rooms, then we need to only generate numRooms - 1 exits
                if (numExits > numRooms) numExits = numRooms - 1;
                

                for (int j = 0; j < numExits; j++)
                {
                    int origRoomExitDirection = 0;
                    bool randomDirectionWorks = false;

                    do
                    {
                        if (Dungeon[i].Title.Equals("Dungeon")) origRoomExitDirection = R.Next(4);
                        else origRoomExitDirection = R.Next(8);
                        Room r = null;

                        // Inside this switch, we should find a random room inside DungeonBuilder.Dungeon
                        // that has the correct exit free.  That should be a method inside DungeonBuilder.
                        switch (origRoomExitDirection)
                        {
                            case 0:
                                if (Dungeon[i].North == null)
                                {
                                    randomDirectionWorks = true;
                                    r = GetRandomRoomWithAvailableExit(ExitDirections.south);
                                    // We have a random room with that available direction,
                                    // so add the new room to the Dungeon[i]

                                    if (r != null) Dungeon[i].AddExit(ExitDirections.north, r);
                                }
                                break;

                            case 1:
                                if (Dungeon[i].South == null)
                                {
                                    randomDirectionWorks = true;
                                    r = GetRandomRoomWithAvailableExit(ExitDirections.north);
                                    if (r != null) Dungeon[i].AddExit(ExitDirections.south, r);
                                }
                                break;
                            case 2:
                                if (Dungeon[i].East == null)
                                {
                                    randomDirectionWorks = true;
                                    r = GetRandomRoomWithAvailableExit(ExitDirections.west);
                                    if (r != null) Dungeon[i].AddExit(ExitDirections.east, r);
                                }
                                break;
                            case 3:
                                if (Dungeon[i].West == null)
                                {
                                    randomDirectionWorks = true;
                                    r = GetRandomRoomWithAvailableExit(ExitDirections.east);
                                    if (r != null) Dungeon[i].AddExit(ExitDirections.west, r);
                                }
                                break;
                            case 4:
                                if (Dungeon[i].Northeast == null)
                                {
                                    randomDirectionWorks = true;
                                    r = GetRandomRoomWithAvailableExit(ExitDirections.southwest);
                                    if (r != null) Dungeon[i].AddExit(ExitDirections.northeast, r);
                                }
                                break;
                            case 5:
                                if (Dungeon[i].Northwest == null)
                                {
                                    randomDirectionWorks = true;
                                    r = GetRandomRoomWithAvailableExit(ExitDirections.southeast);
                                    if (r != null) Dungeon[i].AddExit(ExitDirections.northwest, r);
                                }
                                break;
                            case 6:
                                if (Dungeon[i].Southeast == null)
                                {
                                    randomDirectionWorks = true;
                                    r = GetRandomRoomWithAvailableExit(ExitDirections.northwest);
                                    if (r != null) Dungeon[i].AddExit(ExitDirections.southeast, r);
                                }
                                break;
                            case 7:
                                if (Dungeon[i].Southwest == null)
                                {
                                    randomDirectionWorks = true;
                                    r = GetRandomRoomWithAvailableExit(ExitDirections.northeast);
                                    if (r != null) Dungeon[i].AddExit(ExitDirections.southwest, r);
                                }
                                break;
                        }

                        if (r == null) randomDirectionWorks = true;
                    } while (!randomDirectionWorks);
                }
            }

            // All the rooms have been created, and all have been linked randomly.
            // There is, however, no way of knowing whether all the rooms are accessible
            // without crawling every path.
            // This is probably okay for now.
            // Crawling every path could become an infinitely long process as n gets arbitrarily
            // large.
        }

        /// <summary>
        /// An ordered dungeon builder that creates a square grid dungeon
        /// </summary>
        /// <param name="numRooms"></param>
        /// <param name="Ratio"></param>
        private void DungeonMakerGrid(int numRooms)
        {
            Dungeon = new Dictionary<int, Room>();
            GlobalStrings gs = new GlobalStrings();
            int width;
            width = (int)Math.Sqrt(numRooms);
            
            numRooms = width * width;
            
            // Okay, since we want to generate a grid of rooms, each of which is accessible
            // by any of its adjacent rooms, we start with a width and generate the first room

            for (int i = 0; i < numRooms; i++)
            {
                Room loopRoom = new Room();
                Dungeon.Add(i, loopRoom);
            }

            for (int count = 0; count < numRooms; count++)
            {
                //Random R = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
                Room r = Dungeon[count];

                r.Title = gs.roomTitleStrings[(int)roomType.room];
                r.Desc = gs.roomStrings[(int)roomType.room];

                // Since we are iterating from the top down,
                // we only need to add the rooms that are to the right and below
                // the current room
                if (count % width == 0 && count < numRooms - width)
                {
                    // This is the top left corner case
                    // as well as the left column case
                    Dungeon[count].AddExit(ExitDirections.east, Dungeon[count + 1]);
                    Dungeon[count].AddExit(ExitDirections.south, Dungeon[count + width]);
                    Dungeon[count].AddExit(ExitDirections.southeast, Dungeon[count + width + 1]);
                }
                else if (count % width == width - 1 && count != numRooms - 1)
                {
                    // This is the top right corner case
                    // as well as the right column case
                    Dungeon[count].AddExit(ExitDirections.southwest, Dungeon[count + width - 1]);
                    Dungeon[count].AddExit(ExitDirections.south, Dungeon[count + width]);
                }
                else if (count < numRooms - 1 && count >= numRooms - width - 1)
                {
                    // This is the bottom left corner case
                    // as well as the bottom row case
                    Dungeon[count].AddExit(ExitDirections.east, Dungeon[count + 1]);
                }
                else if (count == numRooms - 1)
                {
                    // This is the bottom right corner case
                    // Don't add any rooms.
                }
                else if (count % width < width)
                {
                    // This is the top row case
                    // as well as the middle rooms case
                    Dungeon[count].AddExit(ExitDirections.east, Dungeon[count + 1]);
                    Dungeon[count].AddExit(ExitDirections.south, Dungeon[count + width]);
                    Dungeon[count].AddExit(ExitDirections.southeast, Dungeon[count + width + 1]);
                    Dungeon[count].AddExit(ExitDirections.southwest, Dungeon[count + width - 1]);
                }
                else
                {
                    // This is for any of the rooms in the middle
                    Console.WriteLine("We shouldn't be here {0}", count);
                }
            }

        }

        /// <summary>
        /// This will be an n-Tree random dungeon
        /// </summary>
        /// <param name="numRooms"></param>
        /// <param name="Random"></param>
        private void DungeonMakerNTree(int numRooms)
        {
            Dungeon = new Dictionary<int, Room>();
            Queue<Room> buildQ = new Queue<Room>();
            GlobalStrings gs = new GlobalStrings();
            Random R = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);

            //var rooms = RoomBuilder(numRooms);
            //Room prevRoom = null;

            Room r = new Room();
            //r.Title = gs.roomTitleStrings[(int)roomType.startingArea];
            //r.Desc = gs.roomStrings[(int)roomType.startingArea];

            buildQ.Enqueue(r);
            
            int count = 0;
            int cellCount = 0;

            while (buildQ.Count > 0 && count < numRooms)
            {
                // Here, we need to make sure that there is something to dequeue.
                // if there isn't, then go back until there is a room with an exit remaining
                Room currentRoom = null;
                if (buildQ.Count > 0) currentRoom = buildQ.Dequeue();
                else currentRoom = FindRoomWithAvailableExit();

                if (currentRoom == null) { Console.WriteLine("Couldn't make all the rooms. Could only make {0} rooms.", count); break; }

                int type = -1;
                if (count == 0)
                {
                    type = (int)roomType.startingArea;
                    Dungeon.Add(count, currentRoom);
                    count = count + 1;
                    currentRoom.Desc = gs.roomStrings[type];
                    currentRoom.Title = gs.roomTitleStrings[type];
                }

                if ((roomType)type == roomType.dungeon)
                {
                    Room Cell = new Room();
                    Cell.Title = gs.roomTitleStrings[(int)roomType.cell];
                    Cell.Desc = gs.roomStrings[(int)roomType.cell];
                    if (currentRoom.AddExit(ExitDirections.northeast, Cell))
                    {
                        Dungeon.Add(numRooms + cellCount, Cell);
                        cellCount += 1;
                    }

                    Cell = new Room();
                    Cell.Title = gs.roomTitleStrings[(int)roomType.cell];
                    Cell.Desc = gs.roomStrings[(int)roomType.cell];
                    if (currentRoom.AddExit(ExitDirections.northwest, Cell))
                    {
                        Dungeon.Add(numRooms + cellCount, Cell);
                        cellCount += 1;
                    }

                    Cell = new Room();
                    Cell.Title = gs.roomTitleStrings[(int)roomType.cell];
                    Cell.Desc = gs.roomStrings[(int)roomType.cell];
                    if (currentRoom.AddExit(ExitDirections.southeast, Cell))
                    {
                        Dungeon.Add(numRooms + cellCount, Cell);
                        cellCount += 1;
                    }

                    Cell = new Room();
                    Cell.Title = gs.roomTitleStrings[(int)roomType.cell];
                    Cell.Desc = gs.roomStrings[(int)roomType.cell];
                    if (currentRoom.AddExit(ExitDirections.southwest, Cell))
                    {
                        Dungeon.Add(numRooms + cellCount, Cell);
                        cellCount += 1;
                    }
                }

                int numExits = R.Next(4) + 1;
                for (int i = 0; i < numExits; i++)
                {
                    if (currentRoom.NumExits < 8)
                    {
                        Room temp = new Room();
                        type = R.Next(gs.roomStrings.Length - 1);
                        while (type == (int)roomType.cell) type = R.Next(gs.roomStrings.Length - 1);
                        temp.Desc = gs.roomStrings[type];
                        temp.Title = gs.roomTitleStrings[type];
                        if (type == (int)roomType.dungeon)
                        {
                            Room Cell = new Room();
                            Cell.Title = gs.roomTitleStrings[(int)roomType.cell];
                            Cell.Desc = gs.roomStrings[(int)roomType.cell];
                            temp.AddExit(ExitDirections.northeast, Cell);
                            Dungeon.Add(numRooms + cellCount, Cell);
                            cellCount += 1;

                            Cell = new Room();
                            Cell.Title = gs.roomTitleStrings[(int)roomType.cell];
                            Cell.Desc = gs.roomStrings[(int)roomType.cell];
                            temp.AddExit(ExitDirections.northwest, Cell);
                            Dungeon.Add(numRooms + cellCount, Cell);
                            cellCount += 1;

                            Cell = new Room();
                            Cell.Title = gs.roomTitleStrings[(int)roomType.cell];
                            Cell.Desc = gs.roomStrings[(int)roomType.cell];
                            temp.AddExit(ExitDirections.southeast, Cell);
                            Dungeon.Add(numRooms + cellCount, Cell);
                            cellCount += 1;

                            Cell = new Room();
                            Cell.Title = gs.roomTitleStrings[(int)roomType.cell];
                            Cell.Desc = gs.roomStrings[(int)roomType.cell];
                            temp.AddExit(ExitDirections.southwest, Cell);
                            Dungeon.Add(numRooms + cellCount, Cell);
                            cellCount += 1;
                        }
                        
                        ExitDirections direction = ExitDirections.northwest;
                        bool finished = false;
                        int exitDirection = -1;
                        while (!finished)
                        {
                            exitDirection = R.Next(8);
                            switch (exitDirection)
                            {
                                case (int)ExitDirections.east:
                                    if (currentRoom.East == null) { direction = ExitDirections.east; finished = true; }
                                    break;
                                case (int)ExitDirections.north:
                                    if (currentRoom.North == null) { direction = ExitDirections.north; finished = true; }
                                    break;
                                case (int)ExitDirections.west:
                                    if (currentRoom.West == null) { direction = ExitDirections.west; finished = true; }
                                    break;
                                case (int)ExitDirections.south:
                                    if (currentRoom.South == null) { direction = ExitDirections.south; finished = true; }
                                    break;
                                case (int)ExitDirections.northeast:
                                    if (currentRoom.Northeast == null) { direction = ExitDirections.northeast; finished = true; }
                                    break;
                                case (int)ExitDirections.northwest:
                                    if (currentRoom.Northwest == null) { direction = ExitDirections.northwest; finished = true; }
                                    break;
                                case (int)ExitDirections.southeast:
                                    if (currentRoom.Southeast == null) { direction = ExitDirections.southeast; finished = true; }
                                    break;
                                case (int)ExitDirections.southwest:
                                    if (currentRoom.Southwest == null) { direction = ExitDirections.southwest; finished = true; }
                                    break;
                                default:
                                    direction = ExitDirections.northwest;
                                    finished = true;
                                    throw new ArgumentException("There was a problem with the random number recieved: " + exitDirection.ToString() + " is not valid.");
                            }
                        }

                        // now we should have a valid direction, so add the room.
                        currentRoom.AddExit(direction, temp);
                        // now add the new room to the queue
                        buildQ.Enqueue(temp);
                        // also, add the new room to the Dungeon
                        // but if the current count is greater than numRooms, don't
                        // add the room.
                        // rooms start at 0 and go up to numRooms - 1
                        if (count >= numRooms) break;
                        Dungeon.Add(count, temp);
                        // increment count and continue
                        count += 1;
                    }
                    else
                    {
                        // If we get to this case, then we need to go back
                        currentRoom = FindRoomWithAvailableExit();
                        if (currentRoom == null) { Console.WriteLine("Couldn't make any more exits for room."); break; }
                        else if (buildQ.Count == 0) buildQ.Enqueue(currentRoom);
                        else break;
                    }
                }

                // We don't need prevRoom.
                //prevRoom = currentRoom;
            }

            // If we have reached the number of rooms, there is no reason to go back and add more exits.
            if (numRooms - count > 0) Console.WriteLine("Still have {0} rooms remaining.", numRooms - count);
            //else if (buildQ.Count > 0) Console.WriteLine("There are still {0} rooms in the queue.", buildQ.Count);
        }

        private Room FindRoomWithAvailableExit()
        {
            Room result = null;

            foreach (Room r in Dungeon.Values)
            {
                if (r.NumExits < 8) result = r;
            }

            return result;
        }

        public IEnumerable<Room> RoomBuilder(int numRooms)
        {
            for (int i = 0; i < numRooms; i++)
            {
                Room r = new Room();
                yield return r;
            }
        }
    }

    public enum ExitDirections
    {
        north,
        south,
        east,
        west,
        northeast,
        northwest,
        southeast,
        southwest
    }

    public class Room
    {
        // The Key for Exits will be from the enum ExitDirections
        // The Value for Exits will be an integer identifier generated by the DungeonBuilder
        // This means that access to Exits must by tightly coupled with the DungeonBuilder
        // as it is generating the Rooms and Links with the Rooms.
        //public Dictionary<ExitDirections, Room> Exits;
        

        public Room North { get; private set; }
        public Room South { get; private set; }
        public Room East { get; private set; }
        public Room West { get; private set; }
        public Room Northwest { get; private set; }
        public Room Southwest { get; private set; }
        public Room Northeast { get; private set; }
        public Room Southeast { get; private set; }

        public int NumExits { get; private set; }
        
        public bool AddExit(ExitDirections direction, Room room)
        {
            if (GetExit(direction) != null || room == null)
                return false;

            NumExits += 1;

            switch (direction)
            {
                case ExitDirections.north:
                    North = room;
                    North.NumExits += 1;
                    North.South = this;
                    break;
                case ExitDirections.south:
                    South = room;
                    South.NumExits += 1;
                    South.North = this;
                    break;
                case ExitDirections.east:
                    East = room;
                    East.NumExits += 1;
                    East.West = this;
                    break;
                case ExitDirections.west:
                    West = room;
                    West.NumExits += 1;
                    West.East = this;
                    break;
                case ExitDirections.northeast:
                    Northeast = room;
                    Northeast.NumExits += 1;
                    Northeast.Southwest = this;
                    break;
                case ExitDirections.northwest:
                    Northwest = room;
                    Northwest.NumExits += 1;
                    Northwest.Southeast = this;
                    break;
                case ExitDirections.southeast:
                    Southeast = room;
                    Southeast.NumExits += 1;
                    Southeast.Northwest = this;
                    break;
                case ExitDirections.southwest:
                    Southwest = room;
                    Southwest.NumExits += 1;
                    Southwest.Northeast = this;
                    break;
                default: throw new ArgumentException("Room direction not valid");
            }
            return true;
        }

        public Room GetExit(ExitDirections direction)
        {
            switch (direction)
            {
                case ExitDirections.north:
                    return North;
                case ExitDirections.south:
                    return South;
                case ExitDirections.east:
                    return East;
                case ExitDirections.west:
                    return West;
                case ExitDirections.northeast:
                    return Northeast;
                case ExitDirections.northwest:
                    return Northwest;
                case ExitDirections.southeast:
                    return Southeast;
                case ExitDirections.southwest:
                    return Southwest;
                default: throw new ArgumentException("Room direction not valid");
            }
        }

        //public string[] Exits;

        // Rooms will be able to have:
        //  a dictionary of Characters(enemies) by name
        //  a dictionary of items by name
        public Dictionary<string, Player> Characters;
        
        public Dictionary<string, GameObject> Items;

        public void GenerateItemsAndAdd(int level)
        {
            // Likelihood of a potion should be better than equipment
            // 12.5% likely to generate a potion
            // 6.25% likely to generate equipment
            int equipChance;
            int potionChance;

            Random R = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            equipChance = R.Next(10000) + 1;
            potionChance = R.Next(10000) + 1;
            Consumable c = null;

            if (equipChance > 9375 && equipChance <= 10000)
            {
                // Generate a random piece of equipment
                Nonconsumable n = new Nonconsumable("", EquipmentTypes.weapon, -1, "");
                int enhance = 0;
                equipChance = R.Next(7);
                switch (equipChance)
                {
                    case (int)EquipmentTypes.arm:
                        enhance = R.Next(100);
                        n.Name = "Gloves";
                        if (enhance == 98) n.Enhancement = "Cursed";
                        else if (enhance == 99) n.Enhancement = "Blessed";
                        n.Type = EquipmentTypes.arm;
                        enhance = R.Next(3)*(1 + level/3) + 1;
                        //if (n.Enhancement.Equals("Cursed")) enhance = 0 - enhance;
                        n.Mod = enhance;
                        break;
                    case (int)EquipmentTypes.chest:
                        enhance = R.Next(100);
                        n.Name = "Breastplate";
                        if (enhance == 98) n.Enhancement = "Cursed";
                        else if (enhance == 99) n.Enhancement = "Blessed";
                        n.Type = EquipmentTypes.chest;
                        //n.Type = "Chest Armor";
                        enhance = R.Next(3) * (1 + level / 3) + 1;
                        n.Mod = enhance;
                        break;
                    case (int)EquipmentTypes.head:
                        enhance = R.Next(100);
                        n.Name = "Helmet";
                        if (enhance == 98) n.Enhancement = "Cursed";
                        else if (enhance == 99) n.Enhancement = "Blessed";
                        n.Type = EquipmentTypes.head;
                        //n.Type = "Head Armor";
                        enhance = R.Next(3)*(1 + level/3) + 1;
                        //if (n.Enhancement.Equals("Cursed")) enhance = 0 - enhance;
                        n.Mod = enhance;
                        break;
                    case (int)EquipmentTypes.legArmor:
                        enhance = R.Next(100);
                        n.Name = "Greaves";
                        if (enhance == 98) n.Enhancement = "Cursed";
                        else if (enhance == 99) n.Enhancement = "Blessed";
                        //n.Type = "Leg Armor";
                        n.Type = EquipmentTypes.legArmor;
                        enhance = R.Next(3)*(1 + level/3) + 1;
                        //if (n.Enhancement.Equals("Cursed")) enhance = 0 - enhance;
                        n.Mod = enhance;
                        break;
                    case (int)EquipmentTypes.legWeapon:
                        enhance = R.Next(100);
                        n.Name = "Greave Blades";
                        if (enhance == 98) n.Enhancement = "Cursed";
                        else if (enhance == 99) n.Enhancement = "Blessed";
                        //n.Type = "Leg Weapon";
                        n.Type = EquipmentTypes.legWeapon;
                        enhance = R.Next(3)*(1 + level/3) + 1;
                        n.Mod = enhance;
                        break;
                    case (int)EquipmentTypes.weapon:
                        enhance = R.Next(100);
                        int weaponType = R.Next(2);
                        if (weaponType == 0) n.Name = "Hammer";
                        else n.Name = "Sword";
                        if (enhance == 98) n.Enhancement = "Cursed";
                        else if (enhance == 99) n.Enhancement = "Blessed";
                        //n.Type = "Weapon";
                        n.Type = EquipmentTypes.weapon;
                        enhance = R.Next(3)*(1 + level/3) + 1;
                        n.Mod = enhance;
                        break;
                    default:
                        enhance = R.Next(100);
                        n.Name = "Sword";
                        if (enhance == 98) n.Enhancement = "Cursed";
                        else if (enhance == 99) n.Enhancement = "Blessed";
                        //n.Type = "Weapon";
                        n.Type = EquipmentTypes.weapon;
                        enhance = R.Next(3)*(1 + level/3) + 1;
                        n.Mod = enhance;
                        break;
                }

                AddItem(n);

                if (potionChance > 9375)
                {
                    c = GeneratePotionsAndAdd(R, level);
                    AddItem(c);
                }
            }
            else
            {
                if (potionChance > 9375)
                {
                    c = GeneratePotionsAndAdd(R, level);
                    AddItem(c);
                }
            }
        }

        public Consumable GeneratePotionsAndAdd(Random R, int level)
        {
            Consumable result = new Consumable("", EquipmentTypes.potion, -1, stats.healthPoints);

            result.Type = EquipmentTypes.potion;
            int stat = R.Next(14);
            if (stat == 0) result.Stat = stats.strength;
            else if (stat == 1) result.Stat = stats.stamina;
            else if (stat == 2) result.Stat = stats.dexterity;
            else if (stat == 3) result.Stat = stats.health;
            else if (stat == 4) result.Stat = stats.dominance;
            else if (stat >= 5 && stat <= 7) result.Stat = stats.dominancePoints;
            else if (stat >= 8 && stat <= 10) result.Stat = stats.staminaPoints;
            else if (stat >= 11 && stat <= 13) result.Stat = stats.healthPoints;
            else result.Stat = stats.healthPoints;

            if (stat < 5) result.Mod = R.Next(3) + 1;
            else result.Mod = R.Next(4) + 5*(level+1);
            //result.Count = 0;
            //Console.WriteLine(result.Count);

            result.Name = "Potion of " + GetStatString(result.Stat);

            return result;
        }

        // moving around should randomly generate zombies.
        public void GenerateEnemiesAndAdd(int level)
        {
            // Good chance there is one enemy (~50%)
            // fair chance there are two enemies (~25%)
            // poor chance there are three enemies (~15%)
            // slim chance there are four enemies (~7%)
            // tiny chance there are four enemies of level and one enemy of level + 2 (~3%)

            // If there is only one enemy, there is a 5% chance that the enemy is 2 levels higher than the player's level

            Random R = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            int numEnemies = R.Next(100);
            int enemyLevel = level;
            if (numEnemies < 50) numEnemies = 0;
            else
            {
                numEnemies = R.Next(100);
                if (numEnemies < 50) { numEnemies = 1; enemyLevel = (R.Next(100) > 95) ? level + 2 : level; }
                else if (numEnemies >= 50 && numEnemies < 75) numEnemies = 2;
                else if (numEnemies >= 75 && numEnemies < 90) numEnemies = 3;
                else if (numEnemies >= 90 && numEnemies < 97) numEnemies = 4;
                else { numEnemies = 4; enemyLevel = level + 2; }
            }
            
            string suffix = "";

            for (int i = 0; i < numEnemies; i++)
            {
                if (enemyLevel == level)
                {
                    // no boss, just create a new zombie and add it.
                    if (i != 0) suffix = " " + i.ToString();
                    AddPlayer(new Player("Zombie" + suffix, level));
                }
                else
                {
                    // we have a boss.  If i = 0, create the boss.  Otherwise, create a new zombie and add it.
                    if (i == 0) AddPlayer(new Player("Boss Zombie", enemyLevel));
                    else
                    {
                        if (i > 1) suffix = " " + (i - 1).ToString();
                        AddPlayer(new Player("Zombie" + suffix, level));
                    }
                }
            }
        }

        public bool AddPlayer(Player p)
        {
            // We need to ensure that there is a unique identifier associated with the Players in each room
            // The best way would be to use an int as an index.  The downside to this method
            // would be that we would have to iterate to remove a PC from the room
            // We can get around this by having a key in the Player Class which gets updated when the
            // player is added to a new room.
            // This doesn't take into account players that move out of the room.
            // We could have a simple counter that gets incremented, and is set to an initial value when the
            // room is created.
            if (Characters.ContainsKey(p.Name.ToLower())) return false;
            //playerKeyCounter += 1;
            Characters.Add(p.Name.ToLower(), p);
            //p.roomKey = playerKeyCounter;
            return true;
        }

        public bool RemovePlayer(Player p)
        {
            if (Characters.Values.Contains(p) == false) return false;
            Characters.Remove(p.Name.ToLower());
            return true;
        }

        public Item GetItem(string itemName)
        {
            foreach (Item i in Items.Values)
            {
                if (i.Name.ToLower() == itemName.ToLower()) return i;
            }
            return null;
        }

        public Player GetPlayer(string playerName)
        {
            if (!Characters.ContainsKey(playerName.ToLower())) return null;
            return Characters[playerName.ToLower()];
        }

        public bool AddItem(Item i)
        {
            // If the item already exists
            if (Items.ContainsKey(i.Name.ToLower()))
            {
                if (!i.IsConsumable) { return false; }
                ((Consumable)Items[i.Name.ToLower()]).Count += 1;
                //Console.WriteLine("Adding Existing Item: " + i.Name + ":" + ((Consumable)Items[i.Name.ToLower()]).Count.ToString());
                return true;
            }

            // if the item doesn't exist, then create it
            Item newItem = null;
            if (i.IsConsumable)
            {
                newItem = new Consumable(i.Name, i.Type, ((Consumable)i).Mod, ((Consumable)i).Stat);
                ((Consumable)newItem).Count += 1;
                //Console.WriteLine("Adding New Item: " + newItem.Name + ":" + ((Consumable)newItem).Count.ToString());
            }
            else newItem = new Nonconsumable(i.Name, i.Type, ((Nonconsumable)i).Mod, ((Nonconsumable)i).Enhancement);

            //itemKeyCounter += 1;
            Items.Add(newItem.Name.ToLower(), newItem);
            //i.roomKey = itemKeyCounter;
            return true;
        }

        public bool RemoveItem(Item i)
        {
            if (Items.ContainsKey(i.Name.ToLower()) == false) return false;
            if (i.IsConsumable)
            {
                ((Consumable)Items[i.Name.ToLower()]).Count -= 1;
                //Console.WriteLine("Removing " + i.Name + ": " + ((Consumable)Items[i.Name.ToLower()]).Count.ToString());
                if (((Consumable)Items[i.Name.ToLower()]).Count == 0) Items.Remove(i.Name.ToLower());
                return true;
            }
            Console.WriteLine("Shouldn't have gotten here...");
            Items.Remove(i.Name.ToLower());
            return true;
        }

        public string _Title, _Desc;
        public string Title
        {
            get { return _Title; }
            set { _Title = (string.IsNullOrEmpty(value)) ? "" : value; }
        }
        public string Desc
        {
            get { return _Desc; }
            set { _Desc = (string.IsNullOrEmpty(value)) ? "" : value; }
        }
        
        public Room()
        {
            //Exits = new Dictionary<string, Room>();
            //Exits = new Dictionary<int, int>();
            //this.Exits = Exits;
            NumExits = 0;
            Characters = new Dictionary<string, Player>();
            Items = new Dictionary<string, Item>();
            //this.Title = Title;
            //this.Desc = Desc;
        }   
    }
}


*/