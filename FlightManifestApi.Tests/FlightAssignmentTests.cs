using Microsoft.VisualStudio.TestTools.UnitTesting;
using apiblueprint.Controllers;
using System.Linq;
using System;

namespace FlightManifestApi.Tests
{
    [TestClass]
    public class FlightAssignmentTests
    {
        [TestMethod]
        public void CanAssignCrew()
        {
            var flight = StoredData.AssignCrewToFlight(1, "test");
            Assert.IsTrue(
                // There's exactly one crew member called "test" added to this flight
                flight.Crew.Where(c => c.Name == "test").Count() == 1
            );
        }

        [TestInitialize]
        public void SeedTestData()
        {
            StoredData.Flights = StoredData.Flights.Append(new Flight()
            {
                FlightId = 4,
                Brief = "Unit Test Flight",
                Crew = new Crew[]{
                    new Crew(){
                        Name = "test"
                    }
                }
            }).ToArray();
        }

        [TestMethod]
        public void CanRemoveCrew()
        {
            StoredData.RemoveCrewFromFlight(4, "test");
            Assert.IsTrue(
                // Find this test flight
                StoredData.Flights.Count(f => f.FlightId == 4
                    // Where there's no longer a "test" crew member
                    && f.Crew.Count(c => c.Name == "test") == 0) == 1
            );
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ThrowExceptionNoFlightExists(){
            // -1 is a flight that doesn't exist
            StoredData.RemoveCrewFromFlight(-1, "test");
        }

        [TestMethod]
        public void CanRemoveFlights()
        {
            StoredData.RemoveFlight(4);
            Assert.IsTrue(
                // Flight "4" doesn't exist now
                StoredData.Flights.Count(f => f.FlightId == 4) == 0
            );
        }

        [TestMethod]
        public void CanAddFlights()
        {
            var flight = StoredData.AddFlight(new Flight()
            {
                FlightId = 5,
                Brief = "Can Add Flights",
                Crew = new Crew[]{
                    new Crew(){
                        Name = "test name"
                    }
                }
            });
            Assert.IsTrue(
                // There's one flight added
                StoredData.Flights.Count(f => f.FlightId == 5) == 1
            );
        }
    }
}