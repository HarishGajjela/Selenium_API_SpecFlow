using QA_Automation_Challenge.StepMethods;
using System;
using TechTalk.SpecFlow;

namespace QA_Automation_Challenge.StepDefinitions
{
    [Binding]
    public class BookingStepDefinitions
    {
       /*****************Background*************/

        [Given(@"User create auth token using username and password")]
        public void GivenUserCreateAuthTokenUsingUsernameAndPassword()
        {
            Mapping_BaseApiTests.SetBaseUriAndAuth();
        }

        /*****************Mapping 1*************/


        [Given(@"User create a booking with all valid parameters")]
        public void GivenUserCreateABookingWithAllValidParameters()
        {
            Mapping_BaseApiTests.Post_CreateBooking_AllValidParameters();
        }

        [Then(@"User retrieve all booking IDs")]
        public void ThenUserRetrieveAllBookingIDs()
        {
            Mapping_BaseApiTests.Get_RetreiveAllBookingIds();
        }

        [Then(@"User retreive single booking by ID")]
        public void ThenUserRetreiveSingleBookingByID()
        {
            Mapping_BaseApiTests.Get_RetreiveSingleBooking();
        }

        [Then(@"User updates an expired booking")]
        public void ThenUserUpdatesAnExpiredBooking()
        {
            Mapping_BaseApiTests.Put_UpdateAnExpiredBooking();
        }

        [Then(@"User delete an existing booking")]
        public void ThenUserDeleteAnExistingBooking()
        {
            Mapping_BaseApiTests.Delete_DeleteExistingBooking();
        }

        /*****************Mapping 2*************/

        [Given(@"User create a booking with all invalid parameters, should fail gracefully")]
        public void GivenUserCreateABookingWithAllInvalidParametersShouldFailGracefully()
        {
            Mapping_BaseApiTests.Post_CreateBooking_AllInvalidParameters();
        }

        [Then(@"User retrieve some booking IDs by query")]
        public void ThenUserRetrieveSomeBookingIDsByQuery()
        {
            Mapping_BaseApiTests.Get_RetreiveSomeBookingIdsByQuery();
        }

        [Then(@"User do a partial update with not all parameters")]
        public void ThenUserDoAPartialUpdateWithNotAllParameters()
        {
            Mapping_BaseApiTests.Patch_PartialUpdateBooking();
        }

        [Then(@"User delete a non existing booking, should fail gracefully")]
        public void ThenUserDeleteANonExistingBookingShouldFailGracefully()
        {
            Mapping_BaseApiTests.Delete_DeleteNonExistingBooking();
        }
    }
}
