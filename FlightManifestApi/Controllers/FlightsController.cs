using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace apiblueprint.Controllers
{
    public class Flight
    {
        public int FlightId { get; set; }
        public string Brief { get; set; }
        public Crew[] Crew { get; set; }
    }
    public class Crew
    {
        public string Name { get; set; }
    }

    public static class StoredData
    {
        public static Flight[] Flights = new[]{
                new Flight(){
                    FlightId = 1,
                    Brief = "Test Flight",
                    Crew = new Crew[]{}
                },
                new Flight(){
                    FlightId = 2,
                    Brief = "Test Crash",
                    Crew = new Crew[]{
                        new Crew(){
                            Name = "Bob"
                        }
                    }
                },
                new Flight(){
                    FlightId = 3,
                    Brief = "Flight to low Kerbin orbit",
                    Crew = new Crew[]{}
                }
            };

        public static void RemoveFlight(int flightId)
        {
            Flights = Flights.Where(f => f.FlightId != flightId).ToArray();
        }

        public static Flight AssignCrewToFlight(int flightId, string crewName)
        {
            var flight = Flights.FirstOrDefault(f => f.FlightId == flightId);
            flight.Crew = new Crew[]{
                new Crew(){
                    Name = crewName
                }
            };
            return flight;
        }

        public static void RemoveCrewFromFlight(int flightId, string crewName)
        {
            var flight = Flights.FirstOrDefault(f => f.FlightId == flightId);
            if(flight == null){
                throw new ArgumentNullException($"no flight for flightId: {flightId}");
            }
            flight.Crew = flight.Crew.Where(c => c.Name != crewName).ToArray();
        }

        public static object AddFlight(Flight flight)
        {
            StoredData.Flights = StoredData.Flights.Append(flight).ToArray();
            return flight;
        }
    }

    [Route("api/[controller]")]
    public class FlightsController : Controller
    {
        // GET api/flights
        [HttpGet, ProducesResponseType(200)]
        public IActionResult ListAllFlights()
        {
            return Ok(StoredData.Flights);
        }

        // GET api/flights/1
        [HttpGet("{flightId}"), ProducesResponseType(200)]
        public IActionResult FlightDetail(int flightId)
        {
            var flight = StoredData.Flights.FirstOrDefault(f => f.FlightId == flightId);
            return Ok(flight);
        }

        // DELETE api/flights/1
        [HttpDelete("{flightId}"), ProducesResponseType(204)]
        public IActionResult RemoveFlight(int flightId){
            StoredData.RemoveFlight(flightId);
            return NoContent();
        }

        // POST api/flights/3/assign/Bob
        [HttpPost("{flightId}/assign/{crewName}"), ProducesResponseType(201)]
        public IActionResult AssignCrewToFlight(int flightId, string crewName){
            var flight = StoredData.AssignCrewToFlight(flightId, crewName);
            return Created("/flights/1", flight);
        }

        // DELETE api/flights/3/assign/Bob
        [HttpDelete("{flightId}/assign/{crewName}"), ProducesResponseType(204)]
        public IActionResult RemoveCrewFromFlight(int flightId, string crewName){
            StoredData.RemoveCrewFromFlight(flightId, crewName);
            return NoContent();
        }


    }
}