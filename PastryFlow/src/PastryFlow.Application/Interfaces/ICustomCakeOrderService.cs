using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PastryFlow.Application.Common;
using PastryFlow.Application.DTOs.CustomCakeOrders;
using PastryFlow.Domain.Enums;

namespace PastryFlow.Application.Interfaces;

public interface ICustomCakeOrderService
{
    // Sipariş oluştur (Tezgah)
    Task<ApiResponse<CustomCakeOrderDto>> CreateAsync(CreateCustomCakeOrderDto dto, Guid userId, Guid branchId);
    
    // Siparişleri listele
    Task<ApiResponse<List<CustomCakeOrderDto>>> GetByBranchAsync(Guid branchId, CustomCakeOrderStatus? status = null);
    Task<ApiResponse<List<CustomCakeOrderDto>>> GetByProductionBranchAsync(Guid productionBranchId, CustomCakeOrderStatus? status = null);
    Task<ApiResponse<List<CustomCakeOrderDto>>> GetAllAsync(CustomCakeOrderStatus? status = null); // Admin
    
    // Tekil sipariş
    Task<ApiResponse<CustomCakeOrderDto>> GetByIdAsync(Guid id);
    
    // Durum güncelle (Üretim + Admin)
    Task<ApiResponse<CustomCakeOrderDto>> UpdateStatusAsync(Guid id, UpdateCakeOrderStatusDto dto, Guid userId);
    
    // Fotoğraf yükle
    Task<ApiResponse<string>> UploadReferencePhotoAsync(Guid id, IFormFile photo, string webRootPath);
    
    // İptal (Tezgah - sadece Created durumda)
    Task<ApiResponse<bool>> CancelAsync(Guid id, Guid userId);
}
