namespace PastryFlow.Domain.Enums;

public enum NotificationType
{
    DemandCreated = 0,
    DemandApproved = 1,
    DemandRejected = 2,
    DemandPartiallyApproved = 3,
    DeliveryReady = 4,
    DeliveryReceived = 5,
    WasteRecorded = 6,
    DayClosingCorrected = 7,
    Info = 8,
    CakeOrderCreated = 9,
    CakeOrderStatusChanged = 10
}
