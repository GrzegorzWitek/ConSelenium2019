Feature: DataLayer
	Example of tests using JavaScript Executor to get DataLayer values. Used for testing GoogleAnalytics feature.

Scenario: DataLayer simple test
	Given I am on Main Page
	Then In DataLayer, in global section is entry "siteType" with value "standard"

Scenario: DataLayer user status test
	Given I am on Main Page
	When I enter birth date "1900-01-01" 
		And I click Enter button
	Then In DataLayer, in user section is entry "loggedInStatus" with value "logged out"

Scenario: DataLayer user age test
	Given I am on Main Page
	When I enter birth date "1900-01-01" 
		And I click Enter button
	Then In DataLayer, in user section is entry "ageGateYear" with value "1900"