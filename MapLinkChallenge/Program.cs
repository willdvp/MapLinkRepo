using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapLinkChallenge.br.com.maplink.services;

namespace MapLinkChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            string json = File.ReadAllText("coordinates.txt");

            List<Coordinates> coordinates = JsonHelper.Deserialize<List<Coordinates>>(json);
            List<RouteData> routeData = new List<RouteData>();

            foreach (Coordinates coo in coordinates)
                routeData.Add(Program.GetRoute(coo));

            using (FileStream resultFile = new FileStream("JsonResult.txt", FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter writer = new StreamWriter(resultFile))
                {
                    writer.Write(JsonHelper.Serialize<List<RouteData>>(routeData));
                }
            }
        }

        static RouteData GetRoute(Coordinates coordinate)
        {
            const string token = "c13iyCvmcC9mzwkLd0LCbmYC5mUF5m2jNGNtNGt6NmK6NJK=";

            RouteStop originRoute = new RouteStop
            {
                description = "Ponto de partida",
                point = new Point { x = coordinate.originCoordinate.longitude, y = coordinate.originCoordinate.latitude }
            };

            RouteStop destinationRoute = new RouteStop
            {
                description = "Ponto de chegada",
                point = new Point { x = coordinate.destinationCoordinate.longitude, y = coordinate.destinationCoordinate.latitude }
            };

            var routes = new[] { originRoute, destinationRoute };

            var routeOptions = new RouteOptions
            {
                language = "portugues",
                routeDetails = new RouteDetails { descriptionType = 0, routeType = 0, optimizeRoute = true },
                vehicle = new Vehicle { tankCapacity = 50, averageConsumption = 10, fuelPrice = 2.69D, averageSpeed = 60, tollFeeCat = 0 }
            };

            RouteData routeData;

            using (Route route = new Route())
            {
                RouteInfo routeInfo = route.getRoute(routes, routeOptions, token);

                routeData = new RouteData();
                routeData.id = coordinate.id;
                routeData.fuelCost = (float)(routeInfo.routeTotals.totalDistance * routeOptions.vehicle.fuelPrice);
                routeData.distance = routeInfo.routeTotals.totalDistance;
                routeData.totalTime = routeInfo.routeTotals.totalTime;
            }

            return routeData;
        }
    }
}
