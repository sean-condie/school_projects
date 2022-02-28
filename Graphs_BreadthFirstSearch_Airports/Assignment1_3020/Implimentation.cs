using Airports;

//create a new route map
RouteMap route_map = new RouteMap();

//create the airpoorts to add to the route map
AirportNode Calgary = new AirportNode("Calgary International Airport", "YYC");
AirportNode Edmonton = new AirportNode("Edmonton International Airport", "YEG");
AirportNode Fredricton = new AirportNode("Fredericton International Airport", "YFC");
AirportNode Gander = new AirportNode("Gander International Airport", "YQX");
AirportNode Halifax = new AirportNode("Halifax Stanfield International Airport", "YHZ");
AirportNode Moncton = new AirportNode("Greater Moncton Roméo LeBlanc International Airport", "YQM");
AirportNode Montreal = new AirportNode("Montréal–Trudeau International Airport", "YUL");
AirportNode Ottawa = new AirportNode("Ottawa Macdonald–Cartier International Airport", "YOW");
AirportNode Quebec = new AirportNode("Québec/Jean Lesage International Airport", "YQB");
AirportNode StJohns = new AirportNode("St. John's International Airport", "YYT");
AirportNode Toronto = new AirportNode("Toronto Pearson International Airport", "YYZ");
AirportNode Vancouver = new AirportNode("Vancouver International Airport", "YVR");
AirportNode Winnipeg = new AirportNode("Winnipeg International Airport", "YWG");

//add the airports to the route map
route_map.AddAirport(Calgary);
route_map.AddAirport(Edmonton);
route_map.AddAirport(Fredricton);
route_map.AddAirport(Gander);
route_map.AddAirport(Halifax);
route_map.AddAirport(Moncton);
route_map.AddAirport(Montreal);
route_map.AddAirport(Ottawa);
route_map.AddAirport(Quebec);
route_map.AddAirport(StJohns);
route_map.AddAirport(Toronto);
route_map.AddAirport(Vancouver);
route_map.AddAirport(Winnipeg);

//add routes to the route map

//vancouver routes
route_map.AddRoute(Vancouver, Edmonton);
route_map.AddRoute(Vancouver, Calgary);
route_map.AddRoute(Vancouver, Toronto);

//edmonton routes
//none

//calgary rotes
route_map.AddRoute(Calgary, Vancouver);
route_map.AddRoute(Calgary, Edmonton);
route_map.AddRoute(Calgary, Winnipeg);

//winnipeg routes
route_map.AddRoute(Winnipeg, Calgary);
route_map.AddRoute(Winnipeg, Toronto);

//toronto routes
route_map.AddRoute(Toronto, Vancouver);
route_map.AddRoute(Toronto, Calgary);
route_map.AddRoute(Toronto, Winnipeg);
route_map.AddRoute(Toronto, Quebec);
route_map.AddRoute(Toronto, Ottawa);
route_map.AddRoute(Toronto, Montreal);
route_map.AddRoute(Toronto, Halifax);
route_map.AddRoute(Toronto, StJohns);


//ottawa routes
route_map.AddRoute(Ottawa, Toronto);
route_map.AddRoute(Ottawa, Montreal);

//montreal routes
route_map.AddRoute(Montreal, Ottawa);
route_map.AddRoute(Montreal, Toronto);
route_map.AddRoute(Montreal, Quebec);

//quebec routes
route_map.AddRoute(Quebec, Montreal);
route_map.AddRoute(Quebec, Fredricton);

//fredricton routes
route_map.AddRoute(Fredricton, Quebec);
route_map.AddRoute(Fredricton, Moncton);

//moncton routes
route_map.AddRoute(Moncton, Halifax);
route_map.AddRoute(Moncton, Fredricton);

//halifax routes
route_map.AddRoute(Halifax, Toronto);
route_map.AddRoute(Halifax, Moncton);
route_map.AddRoute(Halifax, StJohns);

//gander routes
route_map.AddRoute(Gander, StJohns);

//st. johns routes
route_map.AddRoute(StJohns, Halifax);

/*List of Classes and their Functions (excluding constructors) to Test
 * 
 * Class: AirportNode
 *      Functions: ToString
 *      
 * Class: RouteMap
 *      Functions: FindAirportName, FindAirportCode, AddAirport, RemoveAirport, AddRoute, RemoveRoute, FastestRoute, ToString
 * 
 */


//Testing Constructors





//find shortest rout from ottawa to vancouver
//route_map.FastestRoute(Ottawa, Vancouver);

//remove routes
//rout_map.RemoveRoute(Calgary, Montreal);

//remove airports
//rout_map.RemoveAirport(Calgary);

//print an airport's details
//Console.WriteLine(Toronto.ToString());

//print the route map
//Console.WriteLine(rout_map.ToString());

//test find by name (valid)

//test find by name (invalid)

//test find by code (valid)

//test find by code (invalid)

//test if we can add an airport that already exists



//test if we can remove an airport that doesn't exist



//test if we can add a route that already exists



//test if we can add a route that doesn't exist but origin airport does not exist in the route map)



//test if we can add a route that doesn't exist but destination airport does not exist in the route map)



//test if we can add a route that doesn't exist but both airports do not exist in the route map)



//test if we can remove a route that doesn't exist (both airports do exist in the route map)



//test if we can remove a route that doesn't exist (origin airport does not exist in the route map)



//test if we can remove a route that doesn't exist (destination airport does not exist in the route map)



//test if we can remove a route that doesn't exist (both airports do not exist in the route map)