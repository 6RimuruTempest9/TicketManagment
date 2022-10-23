--- Venue

	--- Insert data
	INSERT INTO [dbo].[Venue]
		(
			[dbo].[Venue].[Description],
			[dbo].[Venue].[Address],
			[dbo].[Venue].[Phone]
		)
	VALUES
		(N'First venue' , N'First venue address' , N'123 45 678 90 01'),
		(N'Second venue', N'Second venue address', N'123 45 678 90 02'),
		(N'Third venue' , N'Third venue address' , N'123 45 678 90 03');
	GO

--- Layout

	--- Create temp procedures
	CREATE PROCEDURE sp_temp_GetVenueIdByDescription
		@description NVARCHAR(1024),
		@venueId INT OUTPUT
	AS BEGIN
		SELECT @venueId = [dbo].[Venue].[Id] FROM [dbo].[Venue] WHERE [dbo].[Venue].[Description] = @description;
	END
	GO

	--- Insert data
	DECLARE @firstVenueId  INT
	DECLARE @secondVenueId INT
	DECLARE @thirdVenueId  INT

	EXECUTE sp_temp_GetVenueIdByDescription N'First venue' , @firstVenueId  OUTPUT
	EXECUTE sp_temp_GetVenueIdByDescription N'Second venue', @secondVenueId OUTPUT
	EXECUTE sp_temp_GetVenueIdByDescription N'Third venue' , @thirdVenueId  OUTPUT

	INSERT INTO [dbo].[Layout]
		(
			[dbo].[Layout].[Description],
			[dbo].[Layout].[VenueId]
		)
	VALUES
		(N'First layout of first venue'  , @firstVenueId ),
		(N'Second layout of first venue' , @firstVenueId ),
		(N'Third layout of first venue'  , @firstVenueId ),
		(N'First layout of second venue' , @secondVenueId),
		(N'Second layout of second venue', @secondVenueId),
		(N'First layout of third venue'  , @thirdVenueId );
	GO

	--- Drop temp procedures
	DROP PROCEDURE sp_temp_GetVenueIdByDescription
	GO

--- Area

	--- Create temp procedures
	CREATE PROCEDURE sp_temp_GetLayoutIdByDescription
		@description NVARCHAR(1024),
		@layoutId INT OUTPUT
	AS BEGIN
		SELECT @layoutId = [dbo].[Layout].[Id] FROM [dbo].[Layout] WHERE [dbo].[Layout].[Description] = @description;
	END
	GO

	--- Insert data
	DECLARE @firstLayoutOfFirstVenueId   INT
	DECLARE @secondLayoutOfFirstVenueId  INT
	DECLARE @thirdLayoutOfFirstVenueId   INT
	DECLARE @firstLayoutOfSecondVenueId  INT
	DECLARE @secondLayoutOfSecondVenueId INT
	DECLARE @firstLayoutOfThirdVenueId   INT

	EXECUTE sp_temp_GetLayoutIdByDescription N'First layout of first venue'  , @firstLayoutOfFirstVenueId   OUTPUT --- Venue 1, Layout 1
	EXECUTE sp_temp_GetLayoutIdByDescription N'Second layout of first venue' , @secondLayoutOfFirstVenueId  OUTPUT --- Venue 1, Layout 2
	EXECUTE sp_temp_GetLayoutIdByDescription N'Third layout of first venue'  , @thirdLayoutOfFirstVenueId   OUTPUT --- Venue 1, Layout 3
	EXECUTE sp_temp_GetLayoutIdByDescription N'First layout of second venue' , @firstLayoutOfSecondVenueId  OUTPUT --- Venue 2, Layout 1
	EXECUTE sp_temp_GetLayoutIdByDescription N'Second layout of second venue', @secondLayoutOfSecondVenueId OUTPUT --- Venue 2, Layout 2
	EXECUTE sp_temp_GetLayoutIdByDescription N'First layout of third venue'  , @firstLayoutOfThirdVenueId   OUTPUT --- Venue 3, Layout 1

	INSERT INTO [dbo].[Area]
		(
			[dbo].[Area].[Description],
			[dbo].[Area].[CoordX],
			[dbo].[Area].[CoordY],
			[dbo].[Area].[LayoutId]
		)
	VALUES
		--- Venue 1, Layout 1
		(N'First area of first layout of first venue'  ,   0,   0, @firstLayoutOfFirstVenueId  ),
		(N'Second area of first layout of first venue' ,  50,   0, @firstLayoutOfFirstVenueId  ),
		--- Venue 1, Layout 2                          ,    ,    ,                             ),
		(N'First area of second layout of first venue' , 100,   0, @secondLayoutOfFirstVenueId ),
		(N'Second area of second layout of first venue', 150,   0, @secondLayoutOfFirstVenueId ),
		--- Venue 1, Layout 3                          ,    ,    ,                             ),
		(N'First area of third layout of first venue'  ,   0, 100, @thirdLayoutOfFirstVenueId  ),
		(N'Second area of third layout of first venue' ,  50, 100, @thirdLayoutOfFirstVenueId  ),
		(N'Third area of third layout of first venue'  , 150, 100, @thirdLayoutOfFirstVenueId  ),
		--- Venue 2, Layout 1                          ,    ,    ,                             ),
		(N'First area of first layout of second venue' ,   0,   0, @firstLayoutOfSecondVenueId ),
		(N'Second area of first layout of second venue',  50,   0, @firstLayoutOfSecondVenueId ),
		(N'Third area of first layout of second venue' , 100,   0, @firstLayoutOfSecondVenueId ),
		(N'Fourth area of first layout of second venue', 150,   0, @firstLayoutOfSecondVenueId ),
		--- Venue 2, Layout 2                          ,    ,    ,                             ),
		(N'First area of second layout of second venue',   0, 100, @secondLayoutOfSecondVenueId),
		--- Venue 3, Layout 1                          ,    ,    ,                             ),
		(N'First area of first layout of third venue'  ,   0,   0, @firstLayoutOfThirdVenueId  );
	GO

	--- Drop temp procedures
	DROP PROCEDURE sp_temp_GetLayoutIdByDescription
	GO

--- Seat

	--- Create temp procedures
	CREATE PROCEDURE sp_temp_GetAreaIdByDescription
		@description NVARCHAR(1024),
		@areaId INT OUTPUT
	AS BEGIN
		SELECT @areaId = [dbo].[Area].[Id] FROM [dbo].[Area] WHERE [dbo].[Area].[Description] = @description;
	END
	GO

	CREATE PROCEDURE sp_temp_CreateSeats
		@areaId          INT,
		@rowsCount       INT,
		@countSeatsInRow INT
	AS BEGIN
		DECLARE @currentRow INT
		DECLARE @currentNumber INT

		SET @currentRow = 1
		SET @currentNumber = 1

		WHILE @currentRow <= @rowsCount
		BEGIN
			WHILE @currentNumber <= @countSeatsInRow
			BEGIN
				INSERT INTO [dbo].[Seat]
					(
						[dbo].[Seat].[Row],
						[dbo].[Seat].[Number],
						[dbo].[Seat].[AreaId]
					)
				VALUES (@currentRow, @currentNumber, @areaId);

				SET @currentNumber = @currentNumber + 1
			END

			SET @currentRow = @currentRow + 1
		END
	END
	GO

	--- Insert data
	DECLARE @firstAreaOfFirstLayoutOfFirstVenue   INT --- Venue 1, Layout 1, Area 1
	DECLARE @secondAreaOfFirstLayoutOfFirstVenue  INT --- Venue 1, Layout 1, Area 2
	DECLARE @firstAreaOfSecondLayoutOfFirstVenue  INT --- Venue 1, Layout 2, Area 1
	DECLARE @secondAreaOfSecondLayoutOfFirstVenue INT --- Venue 1, Layout 2, Area 2
	DECLARE @firstAreaOfThirdLayoutOfFirstVenue   INT --- Venue 1, Layout 3, Area 1
	DECLARE @secondAreaOfThirdLayoutOfFirstVenue  INT --- Venue 1, Layout 3, Area 2
	DECLARE @thirdAreaOfThirdLayoutOfFirstVenue   INT --- Venue 1, Layout 3, Area 3
	DECLARE @firstAreaOfFirstLayoutOfSecondVenue  INT --- Venue 2, Layout 1, Area 1
	DECLARE @secondAreaOfFirstLayoutOfSecondVenue INT --- Venue 2, Layout 1, Area 2
	DECLARE @thirdAreaOfFirstLayoutOfSecondVenue  INT --- Venue 2, Layout 1, Area 3
	DECLARE @fourthAreaOfFirstLayoutOfSecondVenue INT --- Venue 2, Layout 1, Area 4
	DECLARE @firstAreaOfSecondLayoutOfSecondVenue INT --- Venue 2, Layout 2, Area 1
	DECLARE @firstAreaOfFirstLayoutOfThirdVenue   INT --- Venue 3, Layout 1, Area 1

	EXECUTE sp_temp_GetAreaIdByDescription N'First area of first layout of first venue'  , @firstAreaOfFirstLayoutOfFirstVenue   OUTPUT
	EXECUTE sp_temp_GetAreaIdByDescription N'Second area of first layout of first venue' , @secondAreaOfFirstLayoutOfFirstVenue  OUTPUT
	EXECUTE sp_temp_GetAreaIdByDescription N'First area of second layout of first venue' , @firstAreaOfSecondLayoutOfFirstVenue  OUTPUT
	EXECUTE sp_temp_GetAreaIdByDescription N'Second area of second layout of first venue', @secondAreaOfSecondLayoutOfFirstVenue OUTPUT
	EXECUTE sp_temp_GetAreaIdByDescription N'First area of third layout of first venue'  , @firstAreaOfThirdLayoutOfFirstVenue   OUTPUT
	EXECUTE sp_temp_GetAreaIdByDescription N'Second area of third layout of first venue' , @secondAreaOfThirdLayoutOfFirstVenue  OUTPUT
	EXECUTE sp_temp_GetAreaIdByDescription N'Third area of third layout of first venue'  , @thirdAreaOfThirdLayoutOfFirstVenue   OUTPUT
	EXECUTE sp_temp_GetAreaIdByDescription N'First area of first layout of second venue' , @firstAreaOfFirstLayoutOfSecondVenue  OUTPUT
	EXECUTE sp_temp_GetAreaIdByDescription N'Second area of first layout of second venue', @secondAreaOfFirstLayoutOfSecondVenue OUTPUT
	EXECUTE sp_temp_GetAreaIdByDescription N'Third area of first layout of second venue' , @thirdAreaOfFirstLayoutOfSecondVenue  OUTPUT
	EXECUTE sp_temp_GetAreaIdByDescription N'Fourth area of first layout of second venue', @fourthAreaOfFirstLayoutOfSecondVenue OUTPUT
	EXECUTE sp_temp_GetAreaIdByDescription N'First area of second layout of second venue', @firstAreaOfSecondLayoutOfSecondVenue OUTPUT
	EXECUTE sp_temp_GetAreaIdByDescription N'First area of first layout of third venue'  , @firstAreaOfFirstLayoutOfThirdVenue   OUTPUT

	EXECUTE sp_temp_CreateSeats @firstAreaOfFirstLayoutOfFirstVenue, 4, 2
	EXECUTE sp_temp_CreateSeats @secondAreaOfFirstLayoutOfFirstVenue, 4, 2
	EXECUTE sp_temp_CreateSeats @firstAreaOfSecondLayoutOfFirstVenue, 4, 2
	EXECUTE sp_temp_CreateSeats @secondAreaOfSecondLayoutOfFirstVenue, 4, 2
	EXECUTE sp_temp_CreateSeats @firstAreaOfThirdLayoutOfFirstVenue, 4, 2
	EXECUTE sp_temp_CreateSeats @secondAreaOfThirdLayoutOfFirstVenue, 4, 4
	EXECUTE sp_temp_CreateSeats @thirdAreaOfThirdLayoutOfFirstVenue, 4, 2
	EXECUTE sp_temp_CreateSeats @firstAreaOfFirstLayoutOfSecondVenue, 4, 2
	EXECUTE sp_temp_CreateSeats @secondAreaOfFirstLayoutOfSecondVenue, 4, 2
	EXECUTE sp_temp_CreateSeats @thirdAreaOfFirstLayoutOfSecondVenue, 4, 2
	EXECUTE sp_temp_CreateSeats @fourthAreaOfFirstLayoutOfSecondVenue, 4, 2
	EXECUTE sp_temp_CreateSeats @firstAreaOfSecondLayoutOfSecondVenue, 4, 8
	EXECUTE sp_temp_CreateSeats @firstAreaOfFirstLayoutOfThirdVenue, 10, 8
	GO

	--- Drop temp procedures
	DROP PROCEDURE sp_temp_GetAreaIdByDescription
	GO

	DROP PROCEDURE sp_temp_CreateSeats
	GO

--- Event

	--- Create temp procedures
	CREATE PROCEDURE sp_temp_GetLayoutIdByDescription
		@description NVARCHAR(1024),
		@layoutId INT OUTPUT
	AS BEGIN
		SELECT @layoutId = [dbo].[Layout].[Id] FROM [dbo].[Layout] WHERE [dbo].[Layout].[Description] = @description;
	END
	GO

	--- Insert data
	DECLARE @thirdLayoutIdOfFirstVenue   INT --- Venue 1, Layout 3
	DECLARE @secondLayoutIdOfSecondVenue INT --- Venue 2, Layout 2
	DECLARE @firstLayoutIdOfThirdVenue   INT --- Venue 3, Layout 1

	EXECUTE sp_temp_GetLayoutIdByDescription N'Third layout of first venue'  , @thirdLayoutIdOfFirstVenue   OUTPUT
	EXECUTE sp_temp_GetLayoutIdByDescription N'Second layout of second venue', @secondLayoutIdOfSecondVenue OUTPUT
	EXECUTE sp_temp_GetLayoutIdByDescription N'First layout of third venue'  , @firstLayoutIdOfThirdVenue   OUTPUT

	--- Event 1, Venue 1, Layout 3
	DECLARE @firstEventStartDate DATETIME
	DECLARE @firstEventEndDate   DATETIME

	SELECT @firstEventStartDate = convert(datetime, '18-06-22 10:00:00 AM', 5)
	SELECT @firstEventEndDate   = convert(datetime, '18-06-22 12:00:00 AM', 5)

	EXECUTE AddEvent
		N'Event 1',
		N'Event 1. Venue 1. Layout 3.',
		@firstEventStartDate,
		@firstEventEndDate,
		N'https://www.meme-arsenal.com/memes/d80e757e7702cdf73fd53bc90bce45c3.jpg',
		@thirdLayoutIdOfFirstVenue

	--- Event 2, Venue 2, Layout 2
	DECLARE @secondEventStartDate DATETIME
	DECLARE @secondEventEndDate   DATETIME

	SELECT @secondEventStartDate = convert(datetime, '15-07-22 2:00:00 PM', 5)
	SELECT @secondEventEndDate   = convert(datetime, '15-07-22 4:00:00 PM', 5)

	EXECUTE AddEvent
		N'Event 2',
		N'Event 2. Venue 2. Layout 2.',
		@secondEventStartDate,
		@secondEventEndDate,
		N'https://www.meme-arsenal.com/memes/d80e757e7702cdf73fd53bc90bce45c3.jpg',
		@secondLayoutIdOfSecondVenue
	
	--- Event 3, Venue 3, Layout 1
	DECLARE @thirdEventStartDate DATETIME
	DECLARE @thirdEventEndDate   DATETIME

	SELECT @thirdEventStartDate = convert(datetime, '21-08-22 6:00:00 PM', 5)
	SELECT @thirdEventEndDate   = convert(datetime, '21-08-22 9:00:00 PM', 5)

	EXECUTE AddEvent
		N'Event 3',
		N'Event 3. Venue 3. Layout 1.',
		@thirdEventStartDate,
		@thirdEventEndDate,
		N'https://www.meme-arsenal.com/memes/d80e757e7702cdf73fd53bc90bce45c3.jpg',
		@firstLayoutIdOfThirdVenue

	--- Drop temp procedures
	DROP PROCEDURE sp_temp_GetLayoutIdByDescription
	GO

--- AspNetUsers
INSERT INTO [dbo].[AspNetUsers]
	(
		[dbo].[AspNetUsers].[Id],
		[dbo].[AspNetUsers].[AccessFailedCount],
		[dbo].[AspNetUsers].[ConcurrencyStamp],
		[dbo].[AspNetUsers].[Email],
		[dbo].[AspNetUsers].[EmailConfirmed],
		[dbo].[AspNetUsers].[LockoutEnabled],
		[dbo].[AspNetUsers].[NormalizedEmail],
		[dbo].[AspNetUsers].[NormalizedUserName],
		[dbo].[AspNetUsers].[PasswordHash],
		[dbo].[AspNetUsers].[PhoneNumberConfirmed],
		[dbo].[AspNetUsers].[SecurityStamp],
		[dbo].[AspNetUsers].[TwoFactorEnabled],
		[dbo].[AspNetUsers].[UserName],
		[dbo].[AspNetUsers].[FirstName],
		[dbo].[AspNetUsers].[LastName],
		[dbo].[AspNetUsers].[Language],
		[dbo].[AspNetUsers].[TimeZone],
		[dbo].[AspNetUsers].[Balance]
	)
VALUES
	(
		N'1',
		0,
		N'd753637f-642e-4e38-995e-64c3101ada49',
		N'admin@epam.com',
		1,
		1,
		N'ADMIN@EPAM.COM',
		N'ADMIN@EPAM.COM',
		N'AQAAAAEAACcQAAAAEO6QEEOuFhhkpqkWqJacEztsmh4/tRqWoYhlNpVl2mUb5pvhWYWV9JRfjddue0aeFg==',
		0,
		N'LTXKGKE6C53MMPCX4MLWPTVHGG7XVPT4',
		0,
		N'admin@epam.com',
		N'Admin',
		N'Admin',
		N'English',
		N'(UTC-12:00) Линия перемены дат',
		100000
	),
	(
		N'2',
		0,
		N'd753637f-642e-4e38-995e-64c3101ada49',
		N'manager@epam.com',
		1,
		1,
		N'MANAGER@EPAM.COM',
		N'MANAGER@EPAM.COM',
		N'AQAAAAEAACcQAAAAEO6QEEOuFhhkpqkWqJacEztsmh4/tRqWoYhlNpVl2mUb5pvhWYWV9JRfjddue0aeFg==',
		0,
		N'LTXKGKE6C53MMPCX4MLWPTVHGG7XVPT4',
		0,
		N'manager@epam.com',
		N'Manager',
		N'Manager',
		N'English',
		N'(UTC-12:00) Линия перемены дат',
		50000
	),
	(
		N'3',
		0,
		N'd753637f-642e-4e38-995e-64c3101ada49',
		N'default@epam.com',
		1,
		1,
		N'DEFAULT@EPAM.COM',
		N'DEFAULT@EPAM.COM',
		N'AQAAAAEAACcQAAAAEO6QEEOuFhhkpqkWqJacEztsmh4/tRqWoYhlNpVl2mUb5pvhWYWV9JRfjddue0aeFg==',
		0,
		N'LTXKGKE6C53MMPCX4MLWPTVHGG7XVPT4',
		0,
		N'default@epam.com',
		N'Default',
		N'Default',
		N'English',
		N'(UTC-12:00) Линия перемены дат',
		5000
	);

--- AspNetRoles
INSERT INTO [dbo].[AspNetRoles]
	([dbo].[AspNetRoles].[Id], [dbo].[AspNetRoles].[Name], [dbo].[AspNetRoles].[NormalizedName])
VALUES
	(N'1', N'Admin'  , N'ADMIN'),
	(N'2', N'Manager', N'MANAGER'),
	(N'3', N'Default', N'DEFAULT');

--- AspNetUserRoles
INSERT INTO [dbo].[AspNetUserRoles]
	([dbo].[AspNetUserRoles].[UserId], [dbo].[AspNetUserRoles].[RoleId])
VALUES
	(N'1', N'1'),
	(N'1', N'2'),
	(N'1', N'3'),
	(N'2', N'2'),
	(N'2', N'3'),
	(N'3', N'3');