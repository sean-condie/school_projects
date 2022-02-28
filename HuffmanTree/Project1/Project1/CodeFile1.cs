using System;
public static class Assignment3
{
    public static void Main()
    {
        //declare variables
        char guestCommand;

        string[] roomArray = new string[12];
        int n = 0;
        bool done = false;

        //prompt user to enter command
        do
        {
            Console.Write("Please enter the character ‘r’ or ‘R’ to reserve a room, ‘d’or ‘D’ delete a room reservation, ‘p’ or ‘P’ for print the room assignments, ‘l’ or ‘L’ for available rooms, and ‘q’ or ‘Q’ to quit the program.");
            guestCommand = Convert.ToChar(Console.ReadLine());
            switch (char.ToUpper(guestCommand))
            {
                case 'R':
                    n = FindAvailRoomNum(roomArray);
                    break;
                case 'D':
                case 'P':
                case 'L':
                case 'Q':
                    break;
            }

        } while (!done);

    }

    public static int FindAvailRoomNum(string[] RoomAssign)
    {
        for (int i = 0; i < RoomAssign.Length; i++)
        {
            if (RoomAssign[i] == null)
            {
                return i;
            } 
        }

        return -1;
    }
}