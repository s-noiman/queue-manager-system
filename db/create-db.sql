
CREATE DATABASE Queue_db; 

USE Queue_db;


BEGIN  

CREATE TABLE [Queues]
(
	[Id] int IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[Date] nvarchar(20)
)

END
