# LondonStockApi
Demo : We are the London Stock Exchange and we are creating an API to receive notification of trades from authorised brokers and expose the updated price to them.

Data Model:

This design is based around 2 SQL relational tables:

CREATE TABLE Stock (
    Id int NOT NULL,
    StockTicker varchar(10) NOT NULL,
    PRIMARY KEY (Id)
);

CREATE TABLE Trade (
    Id int NOT NULL,
    StockId int NOT NULL,
    Price DECIMAL(X,X) NOT NULL,
    Shares DECIMAL(X,X) NOT NULL,
    BrokerID int NOT NULL,
    PRIMARY KEY (Id),
    FOREIGN KEY (StockId) REFERENCES Stock(Id)
);

Exact values for the decimals would be a requirement to work agree with the Product Ownder.
The Broker ID would also be a foreign key referencing a new brokers table also ideally.

In this simplified POC both of these tables are replaced with Fake Repo's and then the only initially available stocks to post trades against are:
StockTicker = "VOD"
StockTicker = "GLEN"
StockTicker = "RR."
StockTicker = "CNA"
StockTicker = "BP."

A simple API spec can be viewed at http://localhost:7071/api/swagger/ui when app is running.

Trades can be added at this address http://localhost:7071/api/UploadTrade

Using a body like the below:
{
    "StockTicker": "VOD",
    "Price": 1000,
    "Shares": 2,
    "BrokerID": 22
}

Trades can be looked up as in the example queries:
http://localhost:7071/api/PriceLookup?stockticker=VOD
http://localhost:7071/api/PriceLookup?stockticker=VOD,CNA
http://localhost:7071/api/PriceLookup

Enhancements
-First item would be to implement full unit testing of all code - I had to stop doing TDD to complete within the timelimits.
-Establish the actual value types for price and shares.
-Creation of Database ideally using ORM and infastructure as code.
-Establish NFRs around time to respond. Implement pagination or similar option to handle return of all stock prices or large number of stocks requested.
-Discover exact nature of stock ticker value and switch to appropriate value types in c# and SQL, also with validation.
-Establish and implement validation and validation response rules.
-Improve Open API spec, this hasn't been implemented to scratch again due to time limitations.
-Implement automated integration testing to DB with API calls.
-Update to .Net 7 if shorter life version of .Net is acceptable.
