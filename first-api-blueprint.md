FORMAT: 1A

# Flight Manifest

Flight Manifest is a basic API allowing flights to be recorded and crew members assigned to flights.

# Group Flights

Resources related to flights that will or have happened.

## Flights Collection [/flights]

### List All Flights [GET]

+ Response 200 (application/json)

        [{
            "brief": "To low Kerbin orbit",
            "desription": "We've some science experiments we need to test out in low Kerbin orbit",
            "departure_time": "2018-01-01T09:00:00.000Z",
            "crew": [{
                "name": "Bob",
                "assigned_at": "2018-01-01T09:00:00.000Z"
            }]
        }]

### Create a New Flight [POST]

You can crew new flights using this action. Takes a JSON object containing a brief for the flight, a longer description, departure time and optionally any crew scheduled for the flight.

+ brief (string) - The one liner to describe the flight
+ description (string) - Long description about why this flight is taking place
+ departure_time (datetime) - When the flight is scheduled to depart
+ crew (array[string], optional) - A collection of crew names

+ Request (application/json)

        {
            "brief": "Land on the Mun",
            "description": "We want to get some soil from the surface of the Mun",
            "departure_time": "2018-12-01T09:00:00.000Z",
            "crew": [
                "Bob",
                "Jeb"
            ]
        }

+ Response 201 (application/json)

    + Headers

        Location: /flights/1

    + Body

            {
                "brief": "Land on the Mun",
                "description": "We want to get some soil from the surface of the Mun",
                "departure_time": "2018-12-01T09:00:00.000Z",
                "crew": [{
                    "name": "Bob",
                    "assigned_at": "2018-12-01T09:00:00.000Z"
                }, {
                    "name": "Jeb",
                    "assigned_at": "2018-12-01T09:00:00.000Z"
                }]
            }

## Flight [/flights/{flight_id}]

+ Parameters
    + flight_id (number, required) - ID of the flight as an integer

### View a Flights Detail [GET]

+ Response 200 (application/json)

        {
            "brief": "To low Kerbin orbit",
            "desription": "We've some science experiments we need to test out in low Kerbin orbit",
            "departure_time": "2018-01-01T09:00:00.000Z",
            "crew": [{
                "name": "Bob",
                "assigned_at": "2018-01-01T09:00:00.000Z"
            }]
        }

### Delete [DELETE]

+ Response 204

## Crew Assignment [/flights/{flight_id}/assign/{crew_name}]

+ Parameters
    + flight_id (number, required) - ID of the flight as an integer
    + crew_name (string, required) - Name of the crew member

### Assign a Crew member to a Flight [POST]

Action allows a crew member to be assigned to a flight.

+ Response 201

    + Headers

        Location: /flights/1

### Remove a Crew member from a Flight [DELETE]

Action allows a crew member to be removed from a flight.

+ Response 204

    + Headers

        Location: /flights/1