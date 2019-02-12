namespace SampleProject.Domain.Customers.Orders
{
    public enum OrderStatus
    {
        Placed = 0,
        InRealization = 1,
        Canceled = 2,
        Delivered = 3,
        Sent = 4,
        WaitingForPayment = 5
    }
}