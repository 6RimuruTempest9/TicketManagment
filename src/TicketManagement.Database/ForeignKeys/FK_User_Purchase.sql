ALTER TABLE dbo.Purchase
ADD CONSTRAINT FK_User_Purchase FOREIGN KEY (UserId)     
    REFERENCES dbo.AspNetUsers (Id)
    ON UPDATE CASCADE
    ON DELETE CASCADE