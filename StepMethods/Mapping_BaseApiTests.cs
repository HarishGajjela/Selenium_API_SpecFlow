using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;


namespace QA_Automation_Challenge.StepMethods
{
    public class Mapping_BaseApiTests
    {
        public static RestClient? client;
        public static RestRequest? request;
        public static RestResponse? response;
        public static string? authToken;
        public static string? bookingId;
        public static string baseUrl = "https://restful-booker.herokuapp.com/";
        static Random random = new Random();
        public static int randomNum = random.Next(maxValue: 1000);

        public static void SetBaseUriAndAuth()
        {
            client = new RestClient(baseUrl);
            request = new RestRequest("auth", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            // Define the request body (booking data)
            var requestBody = new
            {
                username = "admin",
                password = "password123",
            };

            // Serialize the request body as JSON and set it as the request's parameter
            request.AddJsonBody(requestBody);
            // Execute the request
            var response = client.Execute(request);
            // Parse the JSON response
            var tokenObject = JObject.Parse(response.Content);

            // Extract the token value
            authToken = (string)tokenObject["token"];

            Console.WriteLine("Authentication token: " + authToken);
            Assert.True(authToken != null);
            Assert.AreEqual(response.StatusDescription.ToString(), "OK", "Expected and Actual status code are incorrect.");
        }

        public static String Post_CreateBooking()
        {
            request = new RestRequest("booking", Method.Post);

            // Add headers, if needed
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");


            // Define the request body (booking data)
            var requestBody = new
            {
                firstname = "Jim_" + randomNum,
                lastname = "Brown",
                totalprice = 111,
                depositpaid = true,
                bookingdates = new
                {
                    checkin = "2018-01-01",
                    checkout = "2018-01-01"
                },
                additionalneeds = "Breakfast"
            };

            // Serialize the request body as JSON and set it as the request's parameter
            request.AddJsonBody(requestBody);

            // Execute the request and get the response
            var response = client?.Execute(request);
            // Parse the JSON response
            var resp = JObject.Parse(response.Content);

            // Extract the token value
            bookingId = (string)resp["bookingid"];

            // Output the response status code and content
            Console.WriteLine("Response Status Code: " + (int)response.StatusCode);
            Console.WriteLine("Response Content: " + response.Content);
            Assert.AreEqual(response.StatusDescription.ToString(), "OK", "Expected and Actual status code are incorrect.");
            Assert.AreEqual("Jim_" + randomNum, (string)resp["booking"]["firstname"], "firstname is incorrect with actual response");
            return bookingId;
        }

        public static void Put_UpdateBooking()
        {
            SetBaseUriAndAuth();
            // Create the request
            var request = new RestRequest($"booking/{bookingId}", Method.Put);

            // Set the request content type and add the updated booking data to the request body
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Cookie", "token=" + authToken);
            request.AddHeader("Authorization", "Basic YWRtaW46cGFzc3dvcmQxMjM=");

            // Replace with the actual updated booking data you want to send
            var requestBody = new
            {
                firstname = "James",
                lastname = "Brown",
                totalprice = 111,
                depositpaid = true,
                bookingdates = new
                {
                    checkin = "2018-01-01",
                    checkout = "2018-01-01"
                },
                additionalneeds = "Breakfast"
            };

            // Serialize the request body as JSON and set it as the request's parameter
            request.AddJsonBody(requestBody);

            // Execute the request and get the response
            var response = client?.Execute(request);
            // Parse the JSON response
            var resp = JObject.Parse(response.Content);

            // Check the response status
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Request successful!");
                Assert.AreEqual(response.StatusDescription.ToString(), "OK", "Expected and Actual status code are incorrect.");
                Assert.AreEqual("James", (string)resp["booking"]["firstname"], "firstname is incorrect with actual response");
                Console.WriteLine("Booking data updated successfully!");
            }
            else
            {
                Assert.Fail("Failed to update booking. Response: " + response.Content);
            }
        }

        /*******************Mapping 1**************************/
        public static void Post_CreateBooking_AllValidParameters()
        {
            client = new RestClient(baseUrl);
            request = new RestRequest("booking", Method.Post);

            // Add headers, if needed
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");

            // Define the request body (booking data)
            var requestBody = new
            {
                firstname = "Jim_" + randomNum,
                lastname = "Brown",
                totalprice = 111,
                depositpaid = true,
                bookingdates = new
                {
                    checkin = "2018-01-01",
                    checkout = "2018-01-01"
                },
                additionalneeds = "Breakfast"
            };

            // Serialize the request body as JSON and set it as the request's parameter
            request.AddJsonBody(requestBody);

            // Execute the request and get the response
            var response = client?.Execute(request);
            // Parse the JSON response
            var resp = JObject.Parse(response.Content);

            // Extract the token value
            bookingId = (string)resp["bookingid"];

            // Output the response status code and content
            Console.WriteLine("Response Status Code: " + (int)response.StatusCode);
            Console.WriteLine("Response Content: " + response.Content);
            Assert.AreEqual(response.StatusDescription.ToString(), "OK", "Expected and Actual status code are incorrect.");
            Assert.AreEqual("Jim_" + randomNum, (string)resp["booking"]["firstname"], "firstname is correct with actual response");
        }

        public static void Get_RetreiveAllBookingIds()
        {
            request = new RestRequest("booking", Method.Get);

            // Execute the request and get the response
            var response = client?.Execute(request);

            // Check if the request was successful
            if (response.IsSuccessful)
            {
                Console.WriteLine("Request successful!");
                Assert.AreEqual(response.StatusDescription.ToString(), "OK", "Expected and Actual status code are incorrect.");
                Console.WriteLine("Response:");
                Console.WriteLine(response.Content);
            }
            else
            {
                Assert.Fail("Request failed." + response.ErrorMessage);
            }
        }

        public static void Get_RetreiveSingleBooking()
        {
            // Create a RestSharp request
            var request = new RestRequest($"booking/{bookingId}", Method.Get);
            request.AddHeader("Accept", "application/json");

            // Execute the request and get the response
            var response = client?.Execute(request);
            // Parse the JSON response
            var resp = JObject.Parse(response.Content);

            // Check if the request was successful
            if (response.IsSuccessful)
            {
                Assert.AreEqual(response.StatusDescription.ToString(), "OK", "Expected and Actual status code are incorrect.");
                Assert.AreEqual("Jim_" + randomNum, (string)resp["firstname"], "firstname is correct with actual response");
                Console.WriteLine("Response content:");
                Console.WriteLine(response.Content);
            }
            else
            {
                Assert.Fail("Error occurred: " + response.ErrorMessage);
            }
        }

        public static void Put_UpdateAnExpiredBooking()
        {
            // Create the request
            var request = new RestRequest($"booking/{bookingId}", Method.Put);

            // Set the request content type and add the updated booking data to the request body
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");

            // Replace with the actual updated booking data you want to send
            var requestBody = new
            {
                firstname = "James",
                lastname = "Brown",
                totalprice = 111,
                depositpaid = true,
                bookingdates = new
                {
                    checkin = "2018-01-01",
                    checkout = "2018-01-01"
                },
                additionalneeds = "Breakfast"
            };

            // Serialize the request body as JSON and set it as the request's parameter
            request.AddJsonBody(requestBody);

            // Execute the request and get the response
            var response = client?.Execute(request);

            // Check the response status
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.Forbidden);
            Assert.AreEqual(response.StatusCode.ToString(), "Forbidden", "Expected and Actual status code are incorrect.");
        }

        public static void Delete_DeleteExistingBooking()
        {
            SetBaseUriAndAuth();
            var bookingIdToDelete = bookingId;

            // Create a RestRequest with the DELETE method and the resource path
            var request = new RestRequest($"booking/{bookingIdToDelete}", Method.Delete);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Cookie", "token=" + authToken);
            request.AddHeader("Authorization", "Basic YWRtaW46cGFzc3dvcmQxMjM=");

            // Execute the request
            var response = client?.Execute(request);

            // Check the response status and content
            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                Assert.AreEqual(response.StatusCode.ToString(), "Created", "Expected and Actual status code are incorrect.");
                Console.WriteLine($"Booking with ID {bookingIdToDelete} was successfully deleted.");
            }
            else
            {
                Assert.Fail("Failed to delete booking. Response: " + response.StatusCode);
            }
        }

        /*******************Mapping 2**************************/
        public static void Post_CreateBooking_AllInvalidParameters()
        {
            client = new RestClient(baseUrl);
            request = new RestRequest("booking", Method.Post);

            // Add headers, if needed
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");

            // Define the request body (booking data)
            var requestBody = new
            {
                firstname = "Jim_" + randomNum,
                bookingdates = new
                {
                    checkin = "2018-01-01",
                    checkout = "2018-01-01"
                },
                additionalneeds = "Breakfast"
            };

            // Serialize the request body as JSON and set it as the request's parameter
            request.AddJsonBody(requestBody);

            // Execute the request and get the response
            var response = client?.Execute(request);

            // Output the response status code and content
            Console.WriteLine("Response Status Code: " + (int)response.StatusCode);
            Assert.AreEqual(response.StatusCode.ToString(), "InternalServerError", "Expected and Actual status code are incorrect.");
        }

        public static void Get_RetreiveSomeBookingIdsByQuery()
        {
            request = new RestRequest("booking?firstname=James&lastname=Brown", Method.Get);

            // Execute the request and get the response
            var response = client?.Execute(request);

            // Check if the request was successful
            Assert.AreEqual(response.StatusDescription.ToString(), "OK", "Expected and Actual status code are incorrect.");
            Console.WriteLine("Response:");
            Console.WriteLine(response.Content);
        }

        public static void Patch_PartialUpdateBooking()
        {
            SetBaseUriAndAuth();
            bookingId = Post_CreateBooking();
            // Create the request
            var request = new RestRequest($"booking/{bookingId}", Method.Patch);

            // Set the request content type and add the updated booking data to the request body
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Cookie", "token=" + authToken);
            request.AddHeader("Authorization", "Basic YWRtaW46cGFzc3dvcmQxMjM=");

            // Replace with the actual updated booking data you want to send
            var requestBody = new
            {
                firstname = "James",
                lastname = "Brown"
            };

            // Serialize the request body as JSON and set it as the request's parameter
            request.AddJsonBody(requestBody);

            // Execute the request and get the response
            var response = client?.Execute(request);
            // Parse the JSON response
            var resp = JObject.Parse(response.Content);

            // Check the response status
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Request successful!");
                Assert.AreEqual(response.StatusCode.ToString(), "OK", "Expected and Actual status code are incorrect.");
                Assert.AreEqual("James", (string)resp["firstname"], "firstname is correct with actual response");
                Console.WriteLine("Booking data updated successfully!");
            }
            else
            {
                Assert.Fail("Failed to update booking. Response: " + response.Content);
            }
        }

        public static void Delete_DeleteNonExistingBooking()
        {
            SetBaseUriAndAuth();
            var bookingIdToDelete = 9999999999999; // Non Existing Booking

            // Create a RestRequest with the DELETE method and the resource path
            var request = new RestRequest($"booking/{bookingIdToDelete}", Method.Delete);

            request.AddHeader("Content-Type", "application/json");

            // Execute the request
            var response = client?.Execute(request);

            // Check the response status and content
            if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                Assert.AreEqual(response.StatusCode.ToString(), "Forbidden", "Expected and Actual status code are incorrect.");
                Console.WriteLine($"Booking with ID {bookingIdToDelete} was successfully deleted.");
            }
            else
            {
                Assert.Fail("Failed to delete booking. Response: " + response.StatusCode);
            }
        }
    }
}