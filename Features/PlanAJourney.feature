Feature: PlanAJourney

Plan a Journey feature verification

@TFL_PlanJourney_01
Scenario: 01 Valid Journey Planning Verification
	Given Open the URL in chrome browser
	When Identify Plan a journey widget
	When From and To fields are entered with valid locations
	Then valid journey results are loaded

@TFL_PlanJourney_02
Scenario: 02 Invalid Journey Planning Verification
	Given Open the URL in chrome browser
	When Identify Plan a journey widget
	When From and To fields are entered with invalid locations
	Then invalid journey results are loaded

@TFL_PlanJourney_03
Scenario: 03 Journey Planning Verification on Empty Locations
	Given Open the URL in chrome browser
	When Identify Plan a journey widget
	When From and To fields are entered with empty locations
	Then Field Error messages validated

@TFL_PlanJourney_04
Scenario: 04 Journey amended through Edit Journey on Results Page
	Given Open the URL in chrome browser
	When Identify Plan a journey widget
	When From and To fields are entered with valid locations
	Then valid journey results are loaded
	When Edit Journey Results
	Then New To Location Added
	Then New valid journey results are loaded

@TFL_PlanJourney_05
Scenario: 05 Recent Search verification on PlanAJourney Page
	Given Open the URL in chrome browser
	When Identify Plan a journey widget
	When From and To fields are entered with valid locations
	Then valid journey results are loaded
	When Return to PlanAJourney Page
	Then Verify Recent searches