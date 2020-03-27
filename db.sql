--USE [aGroup];

---- Default login username: 'admin'
---- Default password is an empty string (there is no password for admin).


--BEGIN TRY; -- Try drop and create database	
--	BEGIN TRANSACTION [TableCreateTransaction];
--	BEGIN TRY; -- Try create tables
				
--		IF OBJECT_ID('EmployeeType', 'U') IS NULL CREATE TABLE EmployeeType (
--			EmployeeType varchar(256) NOT NULL,
--			PRIMARY KEY (EmployeeType),
--		);
--		PRINT 'EmployeeType table created';

--		IF OBJECT_ID('Employee', 'U') IS NULL CREATE TABLE Employee (
--			EmployeeId int IDENTITY(1000, 1) NOT NULL,
--			EmployeeType varchar(256) NOT NULL DEFAULT 'none',
--			Username varchar(256) NOT NULL UNIQUE,
--			[Password] varchar(256) NOT NULL DEFAULT 'e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855',
--			FirstName varchar(126) NOT NULL DEFAULT '',
--			LastName varchar(126) NOT NULL DEFAULT '',
--			DateOfBirth datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
--			Email varchar(256) NOT NULL DEFAULT '',
--			ContactNumber varchar(16) NOT NULL DEFAULT '',
--			[Address] varchar(256) NOT NULL DEFAULT '',
--			NextOfKinName varchar(256) NOT NULL DEFAULT '',
--			NextOfKinNumber varchar(16) NOT NULL DEFAULT '',
--			Ppsn varchar(10) NOT NULL DEFAULT '',
--			Iban varchar(34) NOT NULL DEFAULT '',
--			Bic varchar(8) NOT NULL DEFAULT '',
--			IsTemporary tinyint NOT NULL DEFAULT 0,
--			IsLoggedIn tinyint NOT NULL DEFAULT 0,
--			PRIMARY KEY (EmployeeId),
--			CONSTRAINT FK_Employee_EmployeeType FOREIGN KEY (EmployeeType) REFERENCES EmployeeType(EmployeeType),
--		);
--		PRINT 'Employee table created';
		
--		IF OBJECT_ID('CleaningStatus', 'U') IS NULL CREATE TABLE CleaningStatus (
--			CleaningStatus varchar(256) NOT NULL,
--			PRIMARY KEY (CleaningStatus),
--		);
--		PRINT 'CleaningStatus table created';
		
--		IF OBJECT_ID('RoomType', 'U') IS NULL CREATE TABLE RoomType (
--			RoomType varchar(256) NOT NULL,
--			PRIMARY KEY (RoomType),
--		);
--		PRINT 'RoomType table created';
		
--		IF OBJECT_ID('MenuType', 'U') IS NULL CREATE TABLE MenuType (
--			MenuType varchar(256) NOT NULL,
--			PRIMARY KEY (MenuType),
--		);
--		PRINT 'MenuType table created';
		
--		IF OBJECT_ID('Room', 'U') IS NULL CREATE TABLE Room (
--			RoomId int IDENTITY(1, 1) NOT NULL,
--			RoomType varchar(256) NOT NULL,
--			CleaningStatus varchar(256) NOT NULL,
--			MaxGuests int NOT NULL DEFAULT 0,
--			IsAvalible tinyint NOT NULL DEFAULT 0,
--			AllowsSmoking tinyint NOT NULL DEFAULT 0,
--			HasCot tinyint NOT NULL DEFAULT 0,
--			PRIMARY KEY (RoomId),
--			CONSTRAINT FK_Room_RoomType FOREIGN KEY (RoomType) REFERENCES RoomType(RoomType),
--			CONSTRAINT FK_Room_CleaningStatus FOREIGN KEY (CleaningStatus) REFERENCES CleaningStatus(CleaningStatus),
--		);
--		PRINT 'Room table created';
		
--		IF OBJECT_ID('StockLocation', 'U') IS NULL CREATE TABLE StockLocation (
--			StockLocation varchar(256) NOT NULL,
--			PRIMARY KEY (StockLocation),
--		);
--		PRINT 'StockLocation table created';
		
--		IF OBJECT_ID('Stock', 'U') IS NULL CREATE TABLE Stock (
--			StockId int IDENTITY(1000, 1) NOT NULL,
--			StockLocation varchar(256) NOT NULL,
--			ItemName varchar(256) NOT NULL DEFAULT '',
--			[Description] varchar(1000) NOT NULL DEFAULT '',
--			Quantity int NOT NULL DEFAULT 0,
--			Supplier varchar(256) NOT NULL DEFAULT '',
--			DateAdded datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
--			ExpiryDate datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
--			PRIMARY KEY (StockId),
--			CONSTRAINT FK_Stock_StockLocation FOREIGN KEY (StockLocation) REFERENCES StockLocation(StockLocation),
--		);
--		PRINT 'Stock table created';
		
--		IF OBJECT_ID('Menu', 'U') IS NULL CREATE TABLE Menu (
--			MenuId int IDENTITY(1, 1) NOT NULL,
--			MenuType varchar(256) NOT NULL DEFAULT 'none',
--			MenuName varchar(256) NOT NULL DEFAULT '',
--			MenuDescription varchar(256) NOT NULL DEFAULT '',
--			IsActive tinyint NOT NULL DEFAULT 0,
--			PRIMARY KEY (MenuId),
--			CONSTRAINT FK_Menu_MenuType FOREIGN KEY (MenuType) REFERENCES MenuType(MenuType),
--		);
--		PRINT 'Menu table created';
		
--		IF OBJECT_ID('MenuItem', 'U') IS NULL CREATE TABLE MenuItem (
--			MenuItemId int IDENTITY(1, 1) NOT NULL,
--			MenuId int NOT NULL,
--			MenuItemName varchar(256) NOT NULL DEFAULT '',
--			ItemPrice decimal NOT NULL,
--			AllergyInfo varchar(1000) NOT NULL DEFAULT '',
--			PRIMARY KEY (MenuItemId),
--			CONSTRAINT FK_MenuItem_MenuId FOREIGN KEY (MenuId) REFERENCES Menu(MenuId),
--		);
--		PRINT 'MenuItem table created';
		
--		IF OBJECT_ID('BarSale', 'U') IS NULL CREATE TABLE BarSale (
--			BarSaleId int IDENTITY(1, 1) NOT NULL,
--			EmployeeId int NOT NULL,
--			DateOfSale datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
--			AmountPaid decimal NOT NULL DEFAULT 0,
--			IsPaid tinyint NOT NULL DEFAULT 0,
--			PRIMARY KEY (BarSaleId),
--			CONSTRAINT FK_BarSale_EmployeeId FOREIGN KEY (EmployeeId) REFERENCES Employee(EmployeeId),
--		);
--		PRINT 'BarSale table created';
		
--		IF OBJECT_ID('BarSaleItem', 'U') IS NULL CREATE TABLE BarSaleItem (
--			BarSaleItemId int IDENTITY(1, 1) NOT NULL,
--			BarSaleId int NOT NULL,
--			MenuItemId int NOT NULL,
--			Quantity int NOT NULL DEFAULT 0,
--			ItemPrice decimal NOT NULL,
--			Notes varchar(1000) NOT NULL DEFAULT '',
--			PRIMARY KEY (BarSaleItemId),
--			CONSTRAINT FK_BarSaleItem_BarSaleId FOREIGN KEY (BarSaleId) REFERENCES BarSale(BarSaleId),
--			CONSTRAINT FK_BarSaleItem_MenuItemId FOREIGN KEY (MenuItemId) REFERENCES MenuItem(MenuItemId),
--		);
--		PRINT 'BarSaleItem table created';
		
--		IF OBJECT_ID('ServingQueueItem', 'U') IS NULL CREATE TABLE ServingQueueItem (
--			ServingQueueItemId int IDENTITY(1, 1) NOT NULL,
--			BarSaleItemId int,
--			TimeAdded datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
--			TimeCompleted datetime,
--			IsComplete tinyint NOT NULL DEFAULT 0,
--			PRIMARY KEY (ServingQueueItemId),
--			CONSTRAINT FK_ServingQueueItem_BarSaleItemId FOREIGN KEY (BarSaleItemId) REFERENCES BarSaleItem(BarSaleItemId),
--		);
--		PRINT 'ServingQueueItem table created';
		
--		IF OBJECT_ID('Reservation', 'U') IS NULL CREATE TABLE Reservation (
--			ReservationId int IDENTITY(1000, 1) NOT NULL,
--			RoomType varchar(256) NOT NULL,
--			RoomId int,
--			CheckInDate datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
--			CheckOutDate datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
--			PricePerNight decimal NOT NULL DEFAULT 0,
--			GuestFirstName varchar(128) NOT NULL DEFAULT '',
--			GuestLastName varchar(128) NOT NULL DEFAULT '',
--			GuestEmail varchar(256) NOT NULL DEFAULT '',
--			GuestPhoneNumber varchar(256) NOT NULL DEFAULT '',
--			NumberOfGuests int NOT NULL DEFAULT 0,
--			Deposit decimal NOT NULL DEFAULT 0,
--			SmokingNeeded tinyint NOT NULL DEFAULT 0,
--			CotNeeded tinyint NOT NULL DEFAULT 0,
--			PRIMARY KEY (ReservationId),
--			CONSTRAINT FK_Reservation_RoomType FOREIGN KEY (RoomType) REFERENCES RoomType(RoomType),
--			CONSTRAINT FK_Reservation_RoomId FOREIGN KEY (RoomId) REFERENCES Room(RoomId),
--		);
--		PRINT 'Reservation table created';
		
--		IF OBJECT_ID('CustomerBill', 'U') IS NULL CREATE TABLE CustomerBill (
--			CustomerBillId int IDENTITY(1, 1) NOT NULL,
--			ReservationId int NOT NULL,
--			AmountPaid decimal NOT NULL DEFAULT 0,
--			IsPaid tinyint NOT NULL DEFAULT 0,
--			PRIMARY KEY (CustomerBillId),
--			CONSTRAINT FK_CustomerBill_ReservationId FOREIGN KEY (ReservationId) REFERENCES Reservation(ReservationId),
--		);
--		PRINT 'CustomerBill table created';
		
--		IF OBJECT_ID('CustomerBillItem', 'U') IS NULL CREATE TABLE CustomerBillItem (
--			CustomerBillItemId int IDENTITY(1, 1) NOT NULL,
--			CustomerBillId int NOT NULL,
--			BarSaleId int,
--			ItemPrice decimal NOT NULL DEFAULT 0,
--			PRIMARY KEY (CustomerBillItemId),
--			CONSTRAINT FK_CustomerBillItem_CustomerBillId FOREIGN KEY (CustomerBillId) REFERENCES CustomerBill(CustomerBillId),
--			CONSTRAINT FK_CustomerBillItem_BarSaleId FOREIGN KEY (BarSaleId) REFERENCES BarSale(BarSaleId),
--		);
--		PRINT 'CustomerBillItem table created';
		
--		IF OBJECT_ID('CleaningRoster', 'U') IS NULL CREATE TABLE CleaningRoster (
--			CleaningRosterId int IDENTITY(1, 1) NOT NULL,
--			CleaningRosterDate datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
--			IsActive tinyint NOT NULL DEFAULT 0,
--			PRIMARY KEY (CleaningRosterId),
--		);
--		PRINT 'CleaningRoster table created';

--		IF OBJECT_ID('CleaningRosterItem', 'U') IS NULL CREATE TABLE CleaningRosterItem (
--			CleaningRosterItemId int IDENTITY(1, 1) NOT NULL,
--			CleaningRosterId int NOT NULL,
--			RoomId int NOT NULL,
--			EmployeeId int,
--			TimeAdded datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
--			TimeCompleted datetime,
--			IsComplete tinyint NOT NULL DEFAULT 0,
--			CONSTRAINT FK_CleaningRosterItem_CleaningRosterId FOREIGN KEY (CleaningRosterId) REFERENCES CleaningRoster(CleaningRosterId),
--			CONSTRAINT FK_CleaningRosterItem_RoomId FOREIGN KEY (RoomId) REFERENCES Room(RoomId),
--			CONSTRAINT FK_CleaningRosterItem_EmployeeId FOREIGN KEY (EmployeeId) REFERENCES Employee(EmployeeId),
--		);
--		PRINT 'CleaningRosterItem table created';
		
--		IF OBJECT_ID('Ingredients', 'U') IS NULL CREATE TABLE Ingredients (
--			IngredientId int IDENTITY(1,1) NOT NULL,
--			StockId int NOT NULL,
--			MenuItemId int NOT NULL,
--			Quantity int NOT NULL DEFAULT 0,
--			PRIMARY KEY (IngredientId),
--			CONSTRAINT FK_Ingredients_StockId FOREIGN KEY (StockId) REFERENCES Stock(StockId),
--			CONSTRAINT FK_Ingredients_MenuItemId FOREIGN KEY (MenuItemId) REFERENCES MenuItem(MenuItemId),
--		);
--		PRINT 'Ingredients table created';

--		IF OBJECT_ID('MovingStock', 'U') IS NULL CREATE TABLE MovingStock (
--			MovingStockId int IDENTITY(1, 1) NOT NULL,
--			FromLocation varchar(256) NOT NULL,
--			ToLocation varchar(256) NOT NULL,
--			PRIMARY KEY (MovingStockId),
--			CONSTRAINT FK_MovingStock_FromLocation FOREIGN KEY (FromLocation) REFERENCES StockLocation(StockLocation),
--			CONSTRAINT FK_MovingStock_ToLocation FOREIGN KEY (ToLocation) REFERENCES StockLocation(StockLocation),
--		);
--		PRINT 'MovingStock table created';
		
--		IF OBJECT_ID('MovingStockItem', 'U') IS NULL CREATE TABLE MovingStockItem (
--			MovingStockItemId int IDENTITY(1, 1) NOT NULL,
--			MovingStockId int NOT NULL,
--			StockId int NOT NULL,
--			Quantity int NOT NULL DEFAULT 0,
--			PRIMARY KEY (MovingStockItemId),
--			CONSTRAINT FK_MovingStockItem_MovingStockId FOREIGN KEY (MovingStockId) REFERENCES MovingStock(MovingStockId),
--			CONSTRAINT FK_MovingStockItem_StockId FOREIGN KEY (StockId) REFERENCES Stock(StockId),
--		);
--		PRINT 'MovingStockItem table created';
		
--		COMMIT TRANSACTION [TableCreateTransaction];
		
--		BEGIN TRANSACTION [InsertDataTransaction];		
--		BEGIN TRY; -- Try insert data	
--			INSERT INTO EmployeeType (EmployeeType)
--			VALUES 
--			('admin'),
--			('none');

--			PRINT 'EmployeeType (1) data inserted';
			
--			INSERT INTO MenuType (MenuType)
--			VALUES 
--			('none'),
--			('drink'),
--			('food');

--			PRINT 'MenuType (3) data inserted';
			
--			INSERT INTO Employee (EmployeeType, Username)
--			VALUES ('admin', 'admin');

--			PRINT 'Employee (1) data inserted';
			
--			COMMIT TRANSACTION [InsertDataTransaction];
--		END TRY BEGIN CATCH; -- End try insert data
--			PRINT '### Failed to insert';
--			ROLLBACK TRANSACTION [InsertDataTransaction];	
--		END CATCH;
			
--	END TRY BEGIN CATCH; -- End try create tables
--		PRINT '### Failed to make tables';
--		ROLLBACK TRANSACTION [TableCreateTransaction];
--	END CATCH;

--END TRY BEGIN CATCH; -- End try drop and create database
--	PRINT '### Failed to make database';
--END CATCH;