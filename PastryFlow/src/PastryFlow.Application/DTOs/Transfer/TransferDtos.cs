using System;
using System.Collections.Generic;
using PastryFlow.Domain.Enums;

namespace PastryFlow.Application.DTOs.Transfer;

public class CreateTransferRequest
{
    public Guid ReceiverBranchId { get; set; }
    public string? Notes { get; set; }
    public List<CreateTransferItemRequest> Items { get; set; } = new();
}

public class CreateTransferItemRequest
{
    public Guid ProductId { get; set; }
    public decimal Quantity { get; set; }
}

public class ReceiveTransferRequest
{
    // Ya tümü ya hiçbiri — ek alan gerekmez
    // Sadece transfer ID yeterli
}

public class CancelTransferRequest
{
    public string CancellationReason { get; set; } = string.Empty;
}

public class TransferDto
{
    public Guid Id { get; set; }
    public string TransferNumber { get; set; } = string.Empty;
    public string SenderBranchName { get; set; } = string.Empty;
    public Guid SenderBranchId { get; set; }
    public string ReceiverBranchName { get; set; } = string.Empty;
    public Guid ReceiverBranchId { get; set; }
    public string StatusLabel { get; set; } = string.Empty;
    public TransferStatus Status { get; set; }
    public DateTime ShippedAt { get; set; }
    public DateTime? ReceivedAt { get; set; }
    public DateTime? CancelledAt { get; set; }
    public string? Notes { get; set; }
    public string? CancellationReason { get; set; }
    public string CreatedByName { get; set; } = string.Empty;
    public string? ReceivedByName { get; set; }
    public List<TransferItemDto> Items { get; set; } = new();
}

public class TransferItemDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string Unit { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
}
