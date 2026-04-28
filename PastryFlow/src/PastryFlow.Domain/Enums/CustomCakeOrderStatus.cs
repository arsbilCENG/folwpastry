namespace PastryFlow.Domain.Enums;

public enum CustomCakeOrderStatus
{
    Created = 0,
    SentToProduction = 1,
    InProduction = 2,
    Ready = 3,
    Delivered = 4,
    Cancelled = 5
}
