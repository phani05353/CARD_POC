Repositiry:
https://github.com/phani05353/CARD_POC

POST call:

https://postcall.azurewebsites.net/api/HttpTrigger1?code=EUjae3EHis6xwTQRYF8ldnjfuAsKgRLKNi5lf0i0nspxvnyBQ0nPgw==&APRStartTime=2009-05-08%2014:40:52,531&APREndTime=2009-05-08%2014:40:52,531&Country=US&City=GR&CardType=1

GET Call:

https://datafromsql.azurewebsites.net/api/HttpTrigger1?code=rM/ctXSJ375vCnhNXilChHLE0cQCiYVL4S5Bf/nKBB0VR3fPZudWQQ==


Get Stored Procedure:

CREATE PROCEDURE testproc
@cardType int 
AS SET NOCOUNT ON; 
SELECT APRStartDate, APREndDate, Country,City 
FROM testTable 
WHERE CardType =@cardType

Insert Statement:

INSERT INTO testTable
VALUES (1, getdate(), getdate(),'India','hyderabad');

Table Create Statement:

CREATE TABLE testTable (
    CardType int,
    APRStartDate datetime,
    APREndDate datetime,
    Country varchar(255),
    City varchar(255),
    CARD_ID int -- Identity Column
);

