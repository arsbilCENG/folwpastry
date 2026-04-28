using System;
using PastryFlow.Domain.Enums;

namespace PastryFlow.Domain.Entities;

public class Notification : BaseEntity
{
    // Kime
    public Guid? UserId { get; set; }           // Spesifik kullanıcıya (null = şube bazlı)
    public Guid? BranchId { get; set; }          // Şubeye (null = role bazlı)
    public string? TargetRole { get; set; }      // Role bazlı (örn: "Admin")
    
    // İçerik
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public NotificationType Type { get; set; }
    
    // Kaynak
    public string? SourceEntity { get; set; }    // "Demand", "Waste", "DayClosing" vb.
    public Guid? SourceEntityId { get; set; }    // İlgili entity'nin ID'si
    public Guid? SourceBranchId { get; set; }    // Bildirimi tetikleyen şube
    public string? SourceBranchName { get; set; }
    
    // Durum
    public bool IsRead { get; set; } = false;
    public DateTime? ReadAt { get; set; }
    
    // Navigation
    public User? User { get; set; }
    public Branch? Branch { get; set; }
}
