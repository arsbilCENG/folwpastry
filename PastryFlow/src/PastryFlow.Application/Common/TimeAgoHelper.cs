using System;

namespace PastryFlow.Application.Common;

public static class TimeAgoHelper
{
    public static string Calculate(DateTime createdAt)
    {
        var turkeyTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
        var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, turkeyTimeZone);
        var created = TimeZoneInfo.ConvertTimeFromUtc(createdAt, turkeyTimeZone);
        var diff = now - created;

        if (diff.TotalMinutes < 1) return "Az önce";
        if (diff.TotalMinutes < 60) return $"{(int)diff.TotalMinutes} dakika önce";
        if (diff.TotalHours < 24) return $"{(int)diff.TotalHours} saat önce";
        if (diff.TotalDays < 7) return $"{(int)diff.TotalDays} gün önce";
        if (diff.TotalDays < 30) return $"{(int)(diff.TotalDays / 7)} hafta önce";
        if (diff.TotalDays < 365) return $"{(int)(diff.TotalDays / 30)} ay önce";
        return $"{(int)(diff.TotalDays / 365)} yıl önce";
    }
}
