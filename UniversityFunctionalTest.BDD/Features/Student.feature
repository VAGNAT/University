Feature: Student

I want to add a student
I want to change the Firstname and Lastname of an existing student
I want to delete an existing student

@tag1
Scenario: Add student
	Given Add an existing group
	And Add an existing student
	When Add a new student with the first name Alice and the last name Moore
	Then The number of students is equal to 2
	And The database contains a student with the first name Alice and the last name Moore

@tag2
Scenario: Change student
	Given Add an existing group
	And Add an existing student
	When Change first name to Alice and last name to Moore in existing student
	Then The number of students is equal to 1
	And The database contains a student with the first name Alice and the last name Moore

@tag3
Scenario: Delete student
	Given Add an existing student
	When Delete existing student
	Then The number of students is equal to 0
	And The database not contains a students
