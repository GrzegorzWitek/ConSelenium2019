Feature: TagsUrls
	Example of tests using Proxy to check traffic between browser and target servers/pages. Used for testing GoogleAnalytics feature.

Scenario: Tag test after opening page
	Given I am on Main Page
	Then I see new tag "cd3" with value "maker's mark"

Scenario: Simple tag test
	Given I am on Main Page
	When I enter birth date "1900-01-01"
		And I click Enter button	
		And I scroll to the end of MainPage page
	Then I see new tag "ea" with value "100 % scrolled"