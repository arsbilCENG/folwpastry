using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PastryFlow.Application.DTOs.Transfer;
using PastryFlow.Domain.Enums;

namespace PastryFlow.Application.Interfaces;

public interface ITransferService
{
    Task<TransferDto> CreateTransferAsync(
        CreateTransferRequest request, Guid senderBranchId, Guid userId);

    Task<TransferDto> ReceiveTransferAsync(
        Guid transferId, Guid receiverBranchId, Guid userId);

    Task CancelTransferAsync(
        Guid transferId, Guid requestingBranchId, Guid userId,
        CancelTransferRequest request);

    Task<List<TransferDto>> GetOutgoingTransfersAsync(
        Guid branchId, TransferStatus? status = null);

    Task<List<TransferDto>> GetIncomingTransfersAsync(
        Guid branchId, TransferStatus? status = null);

    Task<List<TransferDto>> GetAllTransfersAsync(
        TransferStatus? status = null);

    Task<TransferDto> GetTransferByIdAsync(Guid transferId);
}
