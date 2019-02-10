CREATE SCHEMA orders AUTHORIZATION dbo
GO

CREATE TABLE orders.Customers
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[Name] VARCHAR(200),
	CONSTRAINT [PK_orders_Customers_Id] PRIMARY KEY ([Id] ASC)
)
GO

INSERT INTO orders.Customers VALUES ('8A812F08-0647-443B-8FA3-A98C3B9493A7', 'John Doe');
INSERT INTO orders.Customers VALUES ('42441057-b6c1-4852-9ea7-1f382f99e4eb', 'Jane Doe');

CREATE TABLE orders.Orders
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[CustomerId] UNIQUEIDENTIFIER NOT NULL,
	[IsRemoved] BIT NOT NULL,
	[Value] DECIMAL (18, 2) NOT NULL,
	[Currency] VARCHAR(3) NOT NULL,
	CONSTRAINT [PK_orders_Orders_Id] PRIMARY KEY ([Id] ASC)
)
GO

CREATE TABLE orders.Products
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[Name] VARCHAR(200)
	CONSTRAINT [PK_orders_Products_Id] PRIMARY KEY ([Id] ASC)
)
GO

INSERT INTO orders.Products VALUES ('8fad1e5a-d4a2-4688-aa49-e70776940c19', 'Jacket');
INSERT INTO orders.Products VALUES ('9db6e474-ae74-4cf5-a0dc-ba23a42e2566', 'T-shirt');
GO

CREATE TABLE orders.OrderProducts
(
	[OrderId] UNIQUEIDENTIFIER NOT NULL,
	[ProductId] UNIQUEIDENTIFIER NOT NULL,
	[Quantity] INT,
	[Value] DECIMAL(18, 2),
	[Currency] VARCHAR(3),
	CONSTRAINT [PK_orders_OrderProducts_OrderId_ProductId] PRIMARY KEY ([OrderId] ASC, [ProductId] ASC)
)
GO

CREATE TABLE orders.ProductPrices
(
	[ProductId] UNIQUEIDENTIFIER NOT NULL,
	[Value] DECIMAL(18, 2) NOT NULL,
	[Currency] VARCHAR(3) NOT NULL,
	CONSTRAINT [PK_orders_ProductPrices_ProductId, Currency] PRIMARY KEY ([ProductId] ASC, [Currency] ASC)
)
GO

INSERT INTO orders.ProductPrices VALUES ('8fad1e5a-d4a2-4688-aa49-e70776940c19', 200, 'USD');
INSERT INTO orders.ProductPrices VALUES ('8fad1e5a-d4a2-4688-aa49-e70776940c19', 180, 'EUR');
INSERT INTO orders.ProductPrices VALUES ('9db6e474-ae74-4cf5-a0dc-ba23a42e2566', 40, 'USD');
INSERT INTO orders.ProductPrices VALUES ('9db6e474-ae74-4cf5-a0dc-ba23a42e2566', 35, 'EUR');
GO

CREATE VIEW orders.v_Orders
AS
(
	SELECT
		[Order].[Id],
		[Order].[CustomerId],
		[Order].[Value],
		[Order].[IsRemoved],
		[Order].[Currency]
	FROM orders.Orders AS [Order]
)
GO

CREATE VIEW orders.v_OrderProducts
AS
(
	SELECT
		[OrderProduct].[OrderId],
		[OrderProduct].[ProductId],
		[OrderProduct].[Quantity],
		[OrderProduct].[Value],
		[OrderProduct].[Currency],
		[Product].[Name]
	FROM orders.OrderProducts AS [OrderProduct]
		INNER JOIN orders.Products AS [Product]
			ON [OrderProduct].ProductId = [Product].[Id]
)
GO