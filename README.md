# Things I did
1. I have decoupled the controller and the dbContext class using dependency injection for more flexiblity.
2. I have developed an extension to handle exception which keeps the error handling code seperate from the core logic.
3. Writte more unit tests to test the functionality.
4. Removed some code that was not in the requirements.

# Things I would have like to do
1. I would have implemented a Logger service.
2. A date service that would handle the date times for the investments.
3. More types of exceptions under InvestmentBaseException to handle errors more appropriately and easy for testing.
4. More rigourous validation of investments ie. Can we have start date for investment in future? Can we have a start date for investment in past? 