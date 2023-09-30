# TransportForLondon
# SpecFlow Selenium C# BDD Automation for Tfl-Plan A Journey component

# Packages used in the automation
* NUNIT - 3.13.2
* Selenium WebDriver - 4.13.0
* Extent Reports - 4.1.0
* SpecFlow NUnit - 3.9.40

Followed gherkin syntax with Specflow
Automated the below 5 test cases - Excel sheet is attached in the project
1. TFL_PlanJourney_01:	Verify that a valid journey can be planned using the widget. (A valid journey will consist of a valid locations entered into the widget)
2. TFL_PlanJourney_02:	Verify that the widget is unable to provide results when an invalid journey is planned. (An invalid journey will consist of 1 or more invalid locations entered into the widget). 
3. TFL_PlanJourney_03:	Verify that the widget is unable to plan a journey if no locations are entered into the widget.
4. TFL_PlanJourney_04:	On the Journey results page, verify that a journey can be amended by using the “Edit Journey” button.
5. TFL_PlanJourney_05:	Verify that the “Recents” tab on the widget displays a list of recently planned journeys. 

**Automation implementation**
Created a Feature: PlanAJourney
Added above 5 test cases as scenarios by following the same order.
Scenario: 01 Valid Journey Planning Verification
Scenario: 02 Invalid Journey Planning Verification
Scenario: 03 Journey Planning Verification on Empty Locations
Scenario: 04 Journey amended through Edit Journey on Results Page
Scenario: 05 Recent Search verification on PlanAJourney Page

# Implemented the step definitions in PlanAJourneyStepDefinitions.cs file.
# Applied partial page object model architecture for good structuring of the code.
# Created a class- WebDriverContext.cs for ChromeDriver and WebDriverWait initialization. The initialized variables are used across the step definitions.
# As a part of POM implementation, created individual pages for each web page UI, to initialize the respective page locators.
# Passed the location data sets from a separate JSON file, for more readability and reusability. Created a separate class - DataReaderClass.cs for this usage.
# Included AfterScenario hook on the PlanAJourneyStepDefinitions.cs, to skip addition of the Hooks class.
# Extent Report library is used to generate reports automatically whenever any scenario is executed.
# Created a class to handle the report creation- ExtentReport.cs
# Added summary blocks on each method for better understanding on the method execution.

*Attached the scenarios execution videos and reports in the project.*
