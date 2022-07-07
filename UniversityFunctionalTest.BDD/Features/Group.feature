Feature: Group

I want to add a group
I want to change the name of an existing group
I want to delete an existing group

@tag1
Scenario: Add group
	Given Add an existing course
	And Add an existing group
	When Add a new group with the name SR-20
	Then The number of groups is equal to 2
	And The database contains a group with the name SR-20

@tag2
Scenario: Change group
	Given Add an existing course
	And Add an existing group
	When Change name to SR-20 in existing group
	Then The number of groups is equal to 1
	And The database contains a group with the name SR-20

@tag3
Scenario: Delete group
	Given Add an existing group
	When Delete existing group
	Then The number of groups is equal to 0
	And The database not contains a groups
