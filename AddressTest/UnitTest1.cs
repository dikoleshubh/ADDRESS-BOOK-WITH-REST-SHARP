using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using RestSharp;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using AddressRestSharp;

namespace AddressTest
{
    [TestClass]
    public class UnitTest1
    {

        RestClient client;

        [TestInitialize]
        public void SetUp()
        {
            //Initialize the base URL to execute requests made by the instance
            client = new RestClient("http://localhost:3000");
        }
        private IRestResponse GetContactList()
        {
            //Arrange
            //Initialize the request object with proper method and URL
            RestRequest request = new RestRequest("contacts", Method.GET);
            //Act
            // Execute the request
            RestSharp.IRestResponse response = client.Execute(request);
            return response;
        }

        // UC1 Ability to read the entries from json server.

        [TestMethod]
        public void ReadEntriesFromJsonServer()
        {
            IRestResponse response = GetContactList();
            /// Check if the status code of response equals the default code for the method requested
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            /// Convert the response object to list of employees
            List<Contact> employeeList = JsonConvert.DeserializeObject<List<Contact>>(response.Content);
            Assert.AreEqual(5, employeeList.Count);
            foreach (Contact c in employeeList)
            {
                Console.WriteLine($"Id: {c.Id}\tFullName: {c.FirstName} {c.LastName}\tPhoneNo: {c.PhoneNumber}\tAddress: {c.Address}\tCity: {c.City}\tState: {c.State}\tZip: {c.Zip}\tEmail: {c.Email}");
            }
        }
        /// UC23 Ability to adding multiple contacts to the address book JSON server and return the same
        /// </summary>
        [TestMethod]
        public void OnCallingPostAPIForAContactListWithMultipleContacts_ReturnContactObject()
        {
            // Arrange
            List<Contact> contactList = new List<Contact>();
            contactList.Add(new Contact { FirstName = "Naad", LastName = "Bhau", PhoneNumber = "8777456345", Address = "Wakanda", City = "Mumbai", State = "Maharastra", Zip = "547677", Email = "vs@gmail.com" });
            contactList.Add(new Contact { FirstName = "Arr", LastName = "TAta", PhoneNumber = "3456723456", Address = "Dagadi Chaal", City = "Mumbai", State = "MH", Zip = "435627", Email = "yz@gmail.com" });
            contactList.Add(new Contact { FirstName = "Bhai", LastName = "Wakanda", PhoneNumber = "5654564345", Address = "Wanaknda", City = "Mumbai", State = "Maharatra", Zip = "113425", Email = "ted@gmail.com" });

            //Iterate the loop for each contact
            foreach (var v in contactList)
            {
                //Initialize the request for POST to add new contact
                RestRequest request = new RestRequest("contacts", Method.POST);
                JsonObject jsonObj = new JsonObject();
                jsonObj.Add("firstname", v.FirstName);
                jsonObj.Add("lastname", v.LastName);
                jsonObj.Add("phoneNo", v.PhoneNumber);
                jsonObj.Add("address", v.Address);
                jsonObj.Add("city", v.City);
                jsonObj.Add("state", v.State);
                jsonObj.Add("zip", v.Zip);
                jsonObj.Add("email", v.Email);

                //Added parameters to the request object such as the content-type and attaching the jsonObj with the request
                request.AddParameter("application/json", jsonObj, ParameterType.RequestBody);

                //Act
                IRestResponse response = client.Execute(request);

                //Assert
                Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
                Contact contact = JsonConvert.DeserializeObject<Contact>(response.Content);
                Assert.AreEqual(v.FirstName, contact.FirstName);
                Assert.AreEqual(v.LastName, contact.LastName);
                Console.WriteLine(response.Content);
            }

        }
        // UC3 Ability to update the phoneNo into the json file in json server

        [TestMethod]
        public void OnCallingPutAPI_ReturnContactObjects()
        {
            //Arrange
            //Initialize the request for PUT to add new employee
            RestRequest request = new RestRequest("/contacts/6", Method.PUT);
            JsonObject jsonObj = new JsonObject();
            jsonObj.Add("firstname", "baki");
            jsonObj.Add("lastname", "Bata");
            jsonObj.Add("phoneNo", "8777456345");
            jsonObj.Add("address", "Wakanda");
            jsonObj.Add("city", "Wakanda");
            jsonObj.Add("state", "Maharashtra");
            jsonObj.Add("zip", "3556547");
            jsonObj.Add("email", "vs@gmail.com");
            //Added parameters to the request object such as the content-type and attaching the jsonObj with the request
            request.AddParameter("application/json", jsonObj, ParameterType.RequestBody);

            //Act
            IRestResponse response = client.Execute(request);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Contact contact = JsonConvert.DeserializeObject<Contact>(response.Content);
            Assert.AreEqual("Shikhar", contact.FirstName);
            Assert.AreEqual("Dhawan", contact.LastName);
            Assert.AreEqual("535678", contact.Zip);
            Console.WriteLine(response.Content);
        }
    }
}
        
    


    
