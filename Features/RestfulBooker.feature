Feature: Booking Restful Api

Background: Generate Auth token
	Given User create auth token using username and password
	
@Mapping1 @Api
Scenario: CRUD Operations on Booking Api - Mapping 1
	Given User create a booking with all valid parameters
	Then User retrieve all booking IDs
	Then User retreive single booking by ID
	And User updates an expired booking
	Then User delete an existing booking

@Mapping2 @Api
Scenario: CRUD Operations on Booking Api - Mapping 2
	Given User create a booking with all invalid parameters, should fail gracefully
	Then User retrieve some booking IDs by query
	Then User do a partial update with not all parameters
	Then User delete a non existing booking, should fail gracefully
	
