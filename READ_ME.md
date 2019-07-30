
Run the code

https://localhost:44323/swagger

There are 4 end points
	POST /api/Customer		to create a new customer
	PUT  /api/Customer		to amend customer details
	GET  /api/Customer/customerId	to get customer details
	POST /api/Customer/login	to get customer details by entering a validated password

There are also a suite of integration tests to demonstrate each endpoint in the controller which will use a separate database in the bin folder. 
