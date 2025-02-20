Feature: OCRWorkflow

A short summary of the feature

@OCRWorkflow
Scenario: 01Test Case ID 1769058: Verify that the OCR service returns a list of all identified character strings from the image provided

	When Send the API request with a correct image as input and Verify the response
	Then The API response must contain a list of all the identified character strings
