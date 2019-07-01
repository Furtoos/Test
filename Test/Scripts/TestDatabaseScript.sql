use TESTDATABASE_0c34c7624a68428ba31ded219a5a4f77
GO
create trigger Users_Insert
ON AspNetUsers
AFTER INSERT
AS
INSERT INTO Movements(UserName, Date, Change)
SELECT 'Admin', GETDATE() as date, 'add account with name ' + UserName
FROM INSERTED
GO
CREATE TRIGGER Users_Delete
ON AspNetUsers
AFTER DELETE
AS
INSERT INTO Movements(UserName, Date, Change)
SELECT 'Admin', GETDATE() as date, 'delete account with name ' + UserName
FROM DELETED

GO
CREATE TRIGGER Users_Update
ON AspNetUsers
AFTER UPDATE
AS
INSERT INTO Movements(UserName, Date, Change)
SELECT 'Admin', GETDATE() as date, 'update account with name ' + UserName
FROM INSERTED