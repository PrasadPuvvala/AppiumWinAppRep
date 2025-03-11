using System;
using DocumentFormat.OpenXml.Bibliography;
using Newtonsoft.Json;
using System.Collections.Generic;
using Reqnroll;
using sun.security.krb5.@internal;
using RestSharp;
using System.IO;
using NUnit.Framework;
using AventStack.ExtentReports;
using System.Diagnostics;

namespace AppiumWinApp.StepDefinitions
{
    [Binding]
    public class OCRWorkflowStepDefinitions
    {
        private static RestClient client;
        private static RestRequest request;
        private static RestResponse response;
        private ExtentTest test;

        [When("Send the API request with a correct image as input and Verify the response")]
        public void WhenSendTheAPIRequestWithACorrectImageAsInputAndVerifyTheResponse()
        {
            client = new RestClient("https://dev.europe.api.apt.gn.com/ocr-service/v1/analyze-image");
            request = new RestRequest { Method = Method.Post };

            // Add Headers
            request.AddHeader("Ocp-Apim-Subscription-Key", "39731117349c436792eca8513c7d2eb6");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/octet-stream"); // Binary format
            request.AddHeader("username", "surya");
            request.AddHeader("machinename", "FSWIRAY112");
            request.AddHeader("site", "99");

            string imagePath = @"C:\Users\iray3\Downloads\2400811734.png";

            test = ScenarioContext.Current["extentTest"] as ExtentTest;
            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());

            try
            {
                // Read Image as Byte Array
                byte[] imageBytes = File.ReadAllBytes(imagePath);

                // Add Image as Binary Body
                request.AddParameter("application/octet-stream", imageBytes, ParameterType.RequestBody);

                // Measure response time
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                // Execute Request
                response = (RestResponse)client.Execute(request);

                stopwatch.Stop();
                long responseTimeMs = stopwatch.ElapsedMilliseconds;
                List<double> responseTimes = new List<double>();

                // Print response details
                Console.WriteLine($"Status Code: {response.StatusCode} ({(int)response.StatusCode})");
                Console.WriteLine($"Response Time: {responseTimeMs} ms");
                Console.WriteLine($"Response Content: {response.Content}");

                // Print response
                Console.WriteLine("Response: " + response.Content);


                // Check if response is successful
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    // Assert success
                    Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK), "Expected HTTP 200 OK");
                    stepName.Log(Status.Pass, $"StatusCode : {response.StatusCode}");
                    responseTimes.Add(responseTimeMs);
                }
                else
                {
                    // Log failure
                    Assert.Fail($"Test Failed: Unexpected HTTP Status Code {response.StatusCode}");
                    stepName.Log(Status.Fail, $"{response.StatusCode}");
                }

                double medianResponseTime = CalculateMedian(responseTimes);

                if (medianResponseTime < 3000)
                {
                    stepName.Log(Status.Pass, $"Median response time is below 3 seconds");
                }
                else
                {
                    stepName.Log(Status.Fail, $"Median response time is not below 3 seconds");
                }

                // Optionally reset the responseTimes array for future tests
                responseTimes.Clear();
            }
            catch (Exception ex)
            {
                Assert.Fail("Test Failed due to Exception: " + ex.Message);
                stepName.Log(Status.Fail, $"{ex.Message}");
            }
        }

        [Then("The API response must contain a list of all the identified character strings")]
        public void ThenTheAPIResponseMustContainAListOfAllTheIdentifiedCharacterStrings()
        {
            response = (RestResponse)client.Execute(request);

            test = ScenarioContext.Current["extentTest"] as ExtentTest;
            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                // Deserialize JSON response
                var result = JsonConvert.DeserializeObject<List<dynamic>>(response.Content);

                // Log each result item
                foreach (var item in result)
                {
                    stepName.Log(Status.Pass, $"Image Analysis Data : {item}");
                }
            }
            else
            {
                stepName.Log(Status.Fail, $"Image Analysis Data is Not found");
            }
        }

        private double CalculateMedian(List<double> responseTimes)
        {
            responseTimes.Sort();
            int count = responseTimes.Count;
            if (count % 2 == 0)
            {
                // Even number of elements
                double midElement1 = responseTimes[(count / 2) - 1];
                double midElement2 = responseTimes[count / 2];
                return (midElement1 + midElement2) / 2.0;
            }
            else
            {
                // Odd number of elements
                return responseTimes[count / 2];
            }
        }
    }
}
