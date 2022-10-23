ALTER TABLE dbo.Purchase
ADD CONSTRAINT FK_EventSeat_Purchase FOREIGN KEY (EventSeatId)     
    REFERENCES dbo.EventSeat (Id)
    ON UPDATE CASCADE
    ON DELETE CASCADE