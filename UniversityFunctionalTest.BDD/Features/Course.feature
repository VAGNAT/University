Feature: Course

I want to add a course
I want to change the name and description of an existing course
I want to delete an existing course

@tag1
Scenario: Add course
	Given Add an existing course
	When Add a new course with the name Chemical and the description "During their studies, students undergo several types of internships: educational - in the form of excursions to the chemical laboratories of enterprises and research institutes of the city."
	Then The number of courses is equal to 2
	And The database contains a course with the name Chemical and the description "During their studies, students undergo several types of internships: educational - in the form of excursions to the chemical laboratories of enterprises and research institutes of the city."

@tag2
Scenario: Change course
	Given Add an existing course
	When Change name to Chemical and description to "During their studies, students undergo several types of internships: educational - in the form of excursions to the chemical laboratories of enterprises and research institutes of the city." in existing course
	Then The number of courses is equal to 1
	And The database contains a course with the name Chemical and the description "During their studies, students undergo several types of internships: educational - in the form of excursions to the chemical laboratories of enterprises and research institutes of the city."

@tag3
Scenario: Delete course
	Given Add an existing course
	When Delete existing course
	Then The number of courses is equal to 0
	And The database not contains a courses