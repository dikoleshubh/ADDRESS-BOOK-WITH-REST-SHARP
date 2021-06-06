﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
     
}
        
    


    
