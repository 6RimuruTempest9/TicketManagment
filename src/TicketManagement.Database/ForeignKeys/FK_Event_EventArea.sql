ALTER TABLE dbo.EventArea
ADD CONSTRAINT FK_Event_EventArea FOREIGN KEY ([EventId])     
    REFERENCES dbo.[Event] (Id)
    ON UPDATE CASCADE
    ON DELETE CASCADE