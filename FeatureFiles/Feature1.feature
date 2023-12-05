Feature: FDTS

A short summary of the feature


Scenario: 01Verifying S&R Tool
   Given [Launch SandRTool]
	When [Moving to Settings]
	Then [Finish Capture]
@tag1
Scenario: 02Verifying FDTS Tool
	Given [Launch FDTS]
	When [Select Device]
	Then [Do Flashing]

