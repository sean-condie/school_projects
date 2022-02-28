using System.Text;

namespace Airports
{
    public class AirportNode
    {
        public string Name { get; set; } //property for name field. 
        public string Code { get; set; } //property for code field.
        public List<AirportNode> Destinations { get; set; } //property for list of destinations.
        public AirportNode(string name, string code) //constructor 5%
        {
            Name = name;
            Code = code;
            Destinations = new List<AirportNode>();
        }
        void AddDestination(AirportNode destAirport)//method to add destination. 5%
        {
            if (destAirport != null) //check if the argument is null
            {
                if (Destinations.Count > 0) //check if there are destinations
                {
                    if (Destinations.Contains(destAirport)) //the destinations already contain the argument destination
                    {
                        Console.WriteLine("{0} already has a route from {1}!", destAirport.Name, this.Name);
                    }
                    else //the destinations do not contain the argument destination
                    {
                        Destinations.Add(destAirport); //add the destination
                    }
                }
                else //no destinations
                {
                    Destinations.Add(destAirport); //add the destination
                }
            }
            else //argument is null
            {
                Console.WriteLine("Error, try again.");
            }

        }
        void RemoveDestination(AirportNode destAirport) //method to remove destination. 5%
        {
            if (destAirport != null) //check if the argument is null
            {
                if (!(Destinations.Contains(destAirport))) //if the airport is not in the destinations list
                {
                    Console.WriteLine("{0} is not a current destination from {1}!", destAirport.Name, this.Name); //print an error message

                }
                else //if the airport is in the destinations list
                {
                    Destinations.Remove(destAirport); //remove the desitination
                }
            }
            else //argument is null
            {
                Console.WriteLine("Error, try again.");
            }
        }
        public override string ToString() //ToString method overload to print out airport name, code, and list of deestinations. 5%
        {
            string destString = ""; //to contain the formatted string of the destinations

            if (Destinations.Count == 0) //there are no destinations
            {
                destString = "NONE!";
            }
            else if (Destinations.Count >= 1) //there is at least 1 destination
            {
                int destCount = Destinations.Count; //iterator

                while (destCount > 1) //for every destination exept the last one
                {
                    destString = destString + Destinations[destCount-1].Code + ", ";
                    destCount--;
                }

                destString = destString + Destinations[0].Code; //for the last destination (also for if there is only one destination)
            }
            
            return "Airport: " + Name + "  ||  " + "Code: " + Code + "  ||  " + "Destinations: " + destString + "\n";
        }
    }
    public class RouteMap
    {
        private List<AirportNode> Airports; //List of airport nodes.
        public RouteMap() //RouteMap constructor. 5%
        {
            Airports = new List<AirportNode>();
        }
        public bool FindAirportName(string name) //Method to find airport by name. 5%
        {
            if (name != null) //check that the argument is not null
            {
                if (Airports.Count > 0) //check if there is at least one airport
                {
                    foreach (AirportNode node in Airports) //loop through each airport
                    {
                        if (node.Name == name) //check if the argument matches the current airport name
                        {
                            return true;
                        }
                    }

                    return false; //no matches
                }
                else //there are no airports in the route map
                {
                    return false;
                }
            }
            else //the argument is null
            {
                Console.WriteLine("Error, try again.");
            }
            return false; //default return false
        }
        public bool FindAirportCode(string code) //Method to find airport by code. 5%
        {
            if (code != null) //make sure the argument is not null
            {
                if (Airports.Count > 0) //check if there is at least one airport
                {
                    foreach (AirportNode node in Airports) //loop through each airport
                    {
                        if (node.Code == code) //if the argument matches the current airport code
                        {
                            return true; //there is a match
                        }
                    }
                    return false; //no match
                }
            }
            else //argument is null
            {
                Console.WriteLine("Error, try again.");
            }
            return false; //default return false
        }
        public void AddAirport(AirportNode a) //Method to add airport node. Duplicates not allowed.5% 
        {
            if (!(Airports.Contains(a))) // make sure the airport doesn't already exist in the rout map
            {
                Airports.Add(a); //if not, then add it
            }
            else //airport already exists in the route map
            {
                Console.WriteLine("{0} is already on the route map!", a.Name);
            }
        }
        public void RemoveAirport(AirportNode a) //Method to remove airport node. Node must exist. 5% 
        {
            if (Airports.Contains(a)) //check that the airport exists in the route map
            {
                foreach (AirportNode node in Airports) //cycle through each airport
                {
                    if (node.Destinations != null) //make sure destinations is not null
                    {
                        if (node.Destinations.Contains(a)) //check if the current airport contains a destination to the deleted airport
                        {
                            node.Destinations.Remove(a); //if so, then remove it
                        }

                    }
                    
                }

                Airports.Remove(a); //remove the airport
            }
            else //the airport does not exist in the route map
            {
                Console.WriteLine("{0} is not on the route map!", a.Name); 
            }
        }
        public void AddRoute(AirportNode origin, AirportNode dest) //5%
        {
            if (origin != null && dest != null) //make sure arguments are not null
            {
                if (Airports.Contains(origin) && Airports.Contains(dest)) //check that both airports exist in the route map
                {
                    if (!(origin.Destinations.Contains(dest))) //check if the destination alread exists for the origin airport
                    {
                        origin.AddDestination(dest); //if not, then add the destination
                    }
                    else //the destination already exists
                    {
                        Console.WriteLine("{0} is already a desination for {1}!", dest.Code, origin.Code);
                    }

                }
                else //one or both of the airports does not exist
                {
                    if (!Airports.Contains(origin)) //orgin doesn't exist in the route map
                    {
                        Console.WriteLine("{0} does not exist in the route map, please add it first.", origin.Code);
                    }
                    if (!Airports.Contains(dest)) //destination doesn't exist in the route map
                    {
                        Console.WriteLine("{0} does not exist in the route map, please add it first.", dest.Code);
                    }
                }
            }
            else //arguments are null
            {
                Console.WriteLine("Error, try again.");
            }
        }
        public void RemoveRoute(AirportNode origin, AirportNode dest) //5%
        {
            if (origin != null && dest != null) //make sure the arguments are not null
            {
                if (Airports.Contains(origin) && Airports.Contains(dest)) //check if the airports exist in the route map
                {
                    if (origin.Destinations.Contains(dest)) //check if the destination exists for the origin
                    {
                        origin.Destinations.Remove(dest); //if so, remove it
                    }
                    else //the destination does not exist for the origin
                    {
                        Console.WriteLine("{0} is not a desination for {1}!", dest.Code, origin.Code);
                    }

                }
                else //one or both of the airports does not exist
                {
                    if (!Airports.Contains(origin)) //orgin doesn't exist in the route map
                    {
                        Console.WriteLine("{0} does not exist in the route map, please add it first.", origin.Code);
                    }
                    if (!Airports.Contains(dest)) //destination doesn't exist in the route map
                    {
                        Console.WriteLine("{0} does not exist in the route map, please add it first.", dest.Code);
                    }
                }
            }
            else //arguments are null
            {
                Console.WriteLine("Error, try again.");
            }
        }
        public void FastestRoute(AirportNode origin, AirportNode dest) //a bredth first search
        {
            if (origin != null && dest != null) //arguments are not null
            {
                if (origin != dest) //make sure the origin is not the same as the destination
                {
                    if (this.Airports.Contains(origin) && this.Airports.Contains(dest)) //make sure the origin and the destination is in the route map
                    {
                        AirportNode startAirport = origin; //set the origin as the starting airport

                        LinkedList<AirportNode> searchList = new LinkedList<AirportNode>(); //somewhere to store airports to search
                        searchList.AddFirst(startAirport); //add the orgin airport as the first airport to search

                        Dictionary<AirportNode, PathInfo> paths = new Dictionary<AirportNode, PathInfo>(); //combine airport nodes with their associated path in a dictionary of airport nodes
                        paths.Add(startAirport, new PathInfo(null)); //add the origin as the first entry into the dictionary

                        while (searchList.Count > 0) //while there are still airports in the search list
                        {
                            AirportNode currentAirport = searchList.First(); //extract first airport in the search list
                            searchList.RemoveFirst(); //remove the first airport from the search list 

                            foreach (AirportNode d in currentAirport.Destinations) //loop through the destinations of the current airport
                            {
                                if (d == dest) //if the current destination is the desired destination airport
                                {
                                    paths.Add(d, new PathInfo(currentAirport)); //combine the destination with its origin
                                    PrintPath(paths, d); //destination is reached so print the path 
                                    return; //end the function
                                }
                                else if (!paths.ContainsKey(d)) //if the current destination has not already been visited by the algorithm
                                {
                                    paths.Add(d, new PathInfo(currentAirport)); //combine the destination with its origin
                                    searchList.AddLast(d); //add the destination to the end of the search list
                                }
                                else //the current destination has already been visited by the algorithm
                                {
                                    continue;
                                }


                            }

                        }
                    }
                    else if (!this.Airports.Contains(origin)) //the route map does not contain the origin specified
                    {
                        Console.WriteLine("{0} is not in the route map!", origin.Code);
                    }
                    else if (!this.Airports.Contains(dest)) //the route map does not contain the destination specified
                    {
                        Console.WriteLine("{0} is not in the route map!", dest.Code);
                    }

                }
                else //the destination is the same as the origin
                {
                    Console.WriteLine("Destination cannot be the same as the origin!");
                }
            }
        }
        void PrintPath(Dictionary<AirportNode, PathInfo> paths, AirportNode dest) //prints the shortest path found by FastestRoute
        {
            Console.WriteLine("The shortest path from {0} to {1} is: \n", paths.First().Key.Name, dest.Name); //display the desired route info

            LinkedList<AirportNode> path = new LinkedList<AirportNode>(); //stores the path of airports
            path.AddFirst(dest); //add the destination to the path
            AirportNode previous = paths[dest].Previous; //find the previous airport of the destination

            while (previous != null) //while there is still another airport before the current airport
            {
                path.AddFirst(previous); //add the previous airport before the current airport
                previous = paths[previous].Previous; //find the previous airport of the current airport
            }

            StringBuilder pathString = new StringBuilder(); //initialize the string to be printed
            LinkedListNode<AirportNode> currentAirport = path.First; //inicialize a current airport as the first airport to be printed
            int i = 0; //iterator
            while (currentAirport != null) //while there is still another airport to be printed
            {
                i++; //incriment the iterator
                pathString.Append(currentAirport.Value.Code); //add the current airport code to the string
                if (i < path.Count) //if not the last airport
                {
                    pathString.Append("  -->  "); //add a seperator between this airport and the next

                }
                currentAirport = currentAirport.Next; //move to the next airport to be printed
            }
            Console.WriteLine(pathString.ToString()); //print the string.

        }
        public override string ToString() //format the output when we print the route map
        {
            string returnString = "The Route Map: \n\n"; //initialize the string

            if (Airports.Count == 0) //check if there are no airports in the route
            {
                return "There are no airports in this route map yet!"; //return early, saying there are no airports
            }

            foreach (AirportNode a in Airports) //loop through each airport
            {
                if (a.Destinations.Count == 0) //check if the current airport has no destinations
                {
                    returnString = returnString + a.Code + "  -->  NO DESTINATIONS\n";
                }
                foreach (AirportNode dest in a.Destinations) //loop through each destination of the current airport
                {
                    returnString = returnString + a.Code + "  -->  " + dest.Code + "\n"; //add the route to the output string
                }
            }

            return returnString; //return the final string
        }
    }

    public class PathInfo //creates and stores a previous airport node 
    {
        AirportNode previous; //stores the previus airport node

        public PathInfo(AirportNode previous) //constructor
        {
            this.previous = previous; 
        }

        public AirportNode Previous //returns the previous airport node for a current airport node
        {
            get
            {
                return previous;
            }
        }
    }
}

