DELETE FROM payments.Payments
GO

DELETE FROM orders.OrderProducts
GO

DELETE FROM orders.Orders
GO

DELETE FROM orders.Customers
WHERE Id NOT IN ('8A812F08-0647-443B-8FA3-A98C3B9493A7', '42441057-b6c1-4852-9ea7-1f382f99e4eb')

DELETE FROM app.OutboxMessages
GO

DELETE FROM app.InternalCommands
GO