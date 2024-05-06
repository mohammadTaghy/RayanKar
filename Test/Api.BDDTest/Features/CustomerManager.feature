Feature: Create, Reade, Edit and Delete Cutomer

Background:
	Given system errors code are folowing
		| Code | Description      |
		| 500  | validation error |
		

Scenario: Create, Reade, Edit and Delete Cutomer
	Given Exist "0" customers
	When user want to create a customer by below data by sending "Create Customer Command"
		| FirstName | LastName | Email        | PhoneNumber   | DateOfBirth | BankAccountNumber          |
		| John      | Doe      | john@doe.com | +989121234567 | 01-JAN-2000 | IR830120010000001387998021 |
	Then user can filter customers by follow data and get "1" data
		| FirstName | LastName | Email        | PhoneNumber   | DateOfBirth | BankAccountNumber          |
		| John      | Doe      | john@doe.com | +989121234567 | 01-JAN-2000 | IR830120010000001387998021 |
	When user want to create a customer by below data by sending "Create Customer Command"
		| FirstName | LastName | Email        | PhoneNumber   | DateOfBirth | BankAccountNumber          |
		| John      | doe      | john@doe.com | +989121234567 | 01-JAN-2000 | IR830120010000001387998021 |
	Then user must receive error codes
          | Code |
          | 500  |
	When user edit customer with new data
		| FirstName | LastName | Email            | PhoneNumber  | DateOfBirth | BankAccountNumber          |
		| Jane      | William  | jane@william.com | +31612345678 | 01-FEB-2010 | IR720550012870106395265001 |
	Then user can filter customers by follow data and get "0" data
		| FirstName | LastName | Email        | PhoneNumber   | DateOfBirth | BankAccountNumber          |
		| John      | Doe      | john@doe.com | +989121234567 | 01-JAN-2000 | IR830120010000001387998021 |
	And user can filter customers by follow data and get "1" data
		| FirstName | LastName | Email            | PhoneNumber  | DateOfBirth | BankAccountNumber          |
		| Jane      | William  | jane@william.com | +31612345678 | 01-FEB-2010 | IR720550012870106395265001 |
	When user delete customer with email of "jane@william.com"
	Then user can query to get all customers and get "0" records