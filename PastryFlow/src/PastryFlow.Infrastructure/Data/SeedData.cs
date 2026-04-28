using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PastryFlow.Domain.Entities;
using PastryFlow.Domain.Enums;

namespace PastryFlow.Infrastructure.Data;

public static class SeedData
{
    public static void Initialize(ModelBuilder modelBuilder)
    {
        var adminBranchId = (Guid?)null;
        var p1BranchId = Guid.Parse("11111111-1111-1111-1111-111111111101");
        var p2BranchId = Guid.Parse("11111111-1111-1111-1111-111111111102");
        var p3BranchId = Guid.Parse("11111111-1111-1111-1111-111111111103");
        var s1BranchId = Guid.Parse("11111111-1111-1111-1111-111111111104");
        var s2BranchId = Guid.Parse("11111111-1111-1111-1111-111111111105");
        var s3BranchId = Guid.Parse("11111111-1111-1111-1111-111111111106");

        modelBuilder.Entity<Branch>().HasData(
            new Branch { Id = p1BranchId, Name = "Kırklareli Mutfak", City = City.Kirklareli, BranchType = BranchType.Production },
            new Branch { Id = p2BranchId, Name = "Edirne Mutfak", City = City.Edirne, BranchType = BranchType.Production },
            new Branch { Id = p3BranchId, Name = "Lüleburgaz Mutfak", City = City.Luleburgaz, BranchType = BranchType.Production },
            new Branch { Id = s1BranchId, Name = "Kırklareli Tezgah", City = City.Kirklareli, BranchType = BranchType.Sales },
            new Branch { Id = s2BranchId, Name = "Edirne Tezgah", City = City.Edirne, BranchType = BranchType.Sales },
            new Branch { Id = s3BranchId, Name = "Lüleburgaz Tezgah", City = City.Luleburgaz, BranchType = BranchType.Sales }
        );

        var passwordHash = "$2a$11$NleqkNOCjv3N1YWwrJYYfuHYXvpO5vaE1.It3fUTKLgHrTkXCABWO";

        modelBuilder.Entity<User>().HasData(
            new User { Id = Guid.Parse("33333333-3333-3333-3333-333333333301"), Email = "admin@pastryflow.com", FullName = "Admin Kullanıcı", Role = UserRole.Admin, BranchId = adminBranchId, PasswordHash = passwordHash },
            new User { Id = Guid.Parse("33333333-3333-3333-3333-333333333302"), Email = "kirklareli.mutfak@pastryflow.com", FullName = "Kırklareli Üretim", Role = UserRole.Production, BranchId = p1BranchId, PasswordHash = passwordHash },
            new User { Id = Guid.Parse("33333333-3333-3333-3333-333333333303"), Email = "edirne.mutfak@pastryflow.com", FullName = "Edirne Üretim", Role = UserRole.Production, BranchId = p2BranchId, PasswordHash = passwordHash },
            new User { Id = Guid.Parse("33333333-3333-3333-3333-333333333304"), Email = "luleburgaz.mutfak@pastryflow.com", FullName = "Lüleburgaz Üretim", Role = UserRole.Production, BranchId = p3BranchId, PasswordHash = passwordHash },
            new User { Id = Guid.Parse("33333333-3333-3333-3333-333333333305"), Email = "kirklareli.tezgah@pastryflow.com", FullName = "Kırklareli Satış", Role = UserRole.Sales, BranchId = s1BranchId, PasswordHash = passwordHash },
            new User { Id = Guid.Parse("33333333-3333-3333-3333-333333333306"), Email = "edirne.tezgah@pastryflow.com", FullName = "Edirne Satış", Role = UserRole.Sales, BranchId = s2BranchId, PasswordHash = passwordHash },
            new User { Id = Guid.Parse("33333333-3333-3333-3333-333333333307"), Email = "luleburgaz.tezgah@pastryflow.com", FullName = "Lüleburgaz Satış", Role = UserRole.Sales, BranchId = s3BranchId, PasswordHash = passwordHash },
            new User { Id = Guid.Parse("33333333-3333-3333-3333-333333333308"), Email = "sofor@pastryflow.com", FullName = "Şoför", Role = UserRole.Driver, BranchId = adminBranchId, PasswordHash = passwordHash }
        );

        var catIds = new Dictionary<string, Guid> {
            { "KEK", Guid.Parse("22222222-2222-2222-2222-222222222201") },
            { "MAYALILAR", Guid.Parse("22222222-2222-2222-2222-222222222203") },
            { "KURABİYE", Guid.Parse("22222222-2222-2222-2222-222222222204") },
            { "PASTALAR", Guid.Parse("22222222-2222-2222-2222-222222222205") },
            { "İÇECEK", Guid.Parse("22222222-2222-2222-2222-222222222206") },
            { "FIRIN", Guid.Parse("22222222-2222-2222-2222-222222222207") },
            { "HAMMADDE", Guid.Parse("22222222-2222-2222-2222-222222222208") }
        };

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = catIds["KEK"], Name = "KEK", SortOrder = 1 },
            new Category { Id = catIds["MAYALILAR"], Name = "MAYALILAR", SortOrder = 2 },
            new Category { Id = catIds["KURABİYE"], Name = "KURABİYE", SortOrder = 3 },
            new Category { Id = catIds["PASTALAR"], Name = "PASTALAR", SortOrder = 4 },
            new Category { Id = catIds["İÇECEK"], Name = "İÇECEK", SortOrder = 5 },
            new Category { Id = catIds["FIRIN"], Name = "FIRIN", SortOrder = 6 },
            new Category { Id = catIds["HAMMADDE"], Name = "HAMMADDE", SortOrder = 7 }
        );

        var products = new List<Product>();

        int pCounter = 1;

        void AddProduct(string name, UnitType unit, Guid? pBranchId, Guid cId, ProductType pType)
        {
            var idString = $"44444444-4444-4444-4444-{pCounter:D12}";
            products.Add(new Product { Id = Guid.Parse(idString), Name = name, Unit = unit, ProductionBranchId = pBranchId, CategoryId = cId, ProductType = pType });
            pCounter++;
        }

        // KEK
        var kekNames = new[] { "KEK DİLİM", "KEK KALIP BÜYÜK", "KEK KALIP KÜÇÜK", "KEK KALIP ORTA", "KEK MUFİN" };
        foreach (var n in kekNames) AddProduct(n, UnitType.Adet, p1BranchId, catIds["KEK"], ProductType.FinishedProduct);

        // EKMEK (Edirne Mutfak bread products go into FIRIN category)
        var ekmekNames = new[] { "EKŞİ MAYA (3'LÜ)", "EKŞİ MAYA (2'Lİ)", "ÇAVDAR", "TAHILLI", "MISIR", "TEKLİ BEYAZ", "ÇİFTLİ BEYAZ", "KEPEK", "TAMBUĞDAY", "PİDE", "SANDVİÇ", "SİYEZ" };
        foreach (var n in ekmekNames) AddProduct(n, UnitType.Adet, p2BranchId, catIds["FIRIN"], ProductType.FinishedProduct);

        // MAYALILAR
        var mayaliNames = new[] { "BÖREK", "KAFKAS BÖREĞİ", "AÇMA", "İÇLİ SİMİT", "PİZZA", "POĞAÇA", "POĞAÇA İRAN", "POĞAÇA SAKALLI", "SİMİT", "SU BÖREĞİ", "İÇLİ SANDVİÇ" };
        foreach (var n in mayaliNames) AddProduct(n, UnitType.Adet, p3BranchId, catIds["MAYALILAR"], ProductType.FinishedProduct);

        // KURABİYE
        AddProduct("ACIBADEM", UnitType.Kg, p3BranchId, catIds["KURABİYE"], ProductType.FinishedProduct);
        AddProduct("BEZE BÜYÜK", UnitType.Adet, p3BranchId, catIds["KURABİYE"], ProductType.FinishedProduct);
        AddProduct("BEZE KUTU", UnitType.Adet, p3BranchId, catIds["KURABİYE"], ProductType.FinishedProduct);
        var kurabiyeKgs = new[] { "GÜLEN YÜZ", "KURABİYE EKSTRA", "KURABİYE MUZ / BONCUK", "KURABİYE SADE", "KURABİYE ŞAM", "KURABİYE TAHİNLİ", "KURABİYE UN", "KURU PASTA" };
        foreach (var n in kurabiyeKgs) AddProduct(n, UnitType.Kg, p3BranchId, catIds["KURABİYE"], ProductType.FinishedProduct);
        AddProduct("KURABİYE TART", UnitType.Adet, p3BranchId, catIds["KURABİYE"], ProductType.FinishedProduct);
        AddProduct("KANDİL SİMİDİ", UnitType.Adet, p3BranchId, catIds["KURABİYE"], ProductType.FinishedProduct);

        // PASTALAR
        var pastaAdets = new[] { "ALMAN PASTASI", "RULO SARMA", "PASTA ADET", "PASTA CHEESECAKE", "PASTA B1", "PASTA NO 0", "PASTA NO 1", "FİGÜR-BUDAPEŞTE-SEBASTİAN", "SÜTLÜ TATLILAR", "TRİLEÇE", "SİPARİŞ PASTA" };
        foreach (var n in pastaAdets) AddProduct(n, UnitType.Adet, p3BranchId, catIds["PASTALAR"], ProductType.FinishedProduct);
        AddProduct("PASTA KG", UnitType.Kg, p3BranchId, catIds["PASTALAR"], ProductType.FinishedProduct);

        // İÇECEK
        var icecekler = new[] { "AYRAN", "ÇAY SATIŞ", "ESPRESSO", "GAZLI İÇECEK", "LİMONATA", "MEYVE SUYU", "SODA MEYVELİ", "SODA SADE", "SU 0.5 L", "TÜRK KAHVESİ", "BİTKİ ÇAYI", "PİKNİK ÜRÜNLER", "KAHVALTI TABAĞI" };
        foreach (var n in icecekler) AddProduct(n, UnitType.Adet, p3BranchId, catIds["İÇECEK"], ProductType.FinishedProduct);

        // FIRIN
        var firinlar = new[] { "GALETE", "EKMEK BEYAZ", "EKMEK ÇEŞİT", "EKMEK KEPEK", "EKMEK MISIR", "EKMEK SANDVİÇ", "TAHILLI EKMEK", "PİDE ÇİFTLİ", "TANDIR EKMEĞİ" };
        foreach (var n in firinlar) AddProduct(n, UnitType.Adet, p3BranchId, catIds["FIRIN"], ProductType.FinishedProduct);

        // HAMMADDE
        var hammaddesKg = new[] { "UN", "TUZ", "ŞEKER", "MAYA", "TEREYAĞI", "MARGARİN", "KAKAO", "KABARTMA TOZU", "NİŞASTA" };
        foreach (var n in hammaddesKg) AddProduct(n, UnitType.Kg, null, catIds["HAMMADDE"], ProductType.RawMaterial);
        var hammaddesLitre = new[] { "SÜT", "KREMA", "AYÇİÇEK YAĞI" };
        foreach (var n in hammaddesLitre) AddProduct(n, UnitType.Litre, null, catIds["HAMMADDE"], ProductType.RawMaterial);
        AddProduct("YUMURTA", UnitType.Adet, null, catIds["HAMMADDE"], ProductType.RawMaterial);

        modelBuilder.Entity<Product>().HasData(products);

        // CakeOptions Seed Data
        var cakeOptions = new List<CakeOption>();
        int optionCounter = 1;
        
        void AddOption(string name, CakeOptionType type, int sortOrder)
        {
            var idString = $"55555555-5555-5555-5555-{optionCounter:D12}";
            cakeOptions.Add(new CakeOption { Id = Guid.Parse(idString), Name = name, OptionType = type, SortOrder = sortOrder });
            optionCounter++;
        }

        // CakeType
        var cakeTypes = new[] { "Kakaolu", "Vanilyalı", "Meyveli", "Havuçlu", "Muzlu", "Limonlu" };
        for (int i = 0; i < cakeTypes.Length; i++) AddOption(cakeTypes[i], CakeOptionType.CakeType, i + 1);

        // InnerCream
        var innerCreams = new[] { "Çikolatalı", "Muzlu", "Frambuazlı", "Vanilyalı", "Karamelli", "Fıstıklı", "Beyaz Çikolatalı" };
        for (int i = 0; i < innerCreams.Length; i++) AddOption(innerCreams[i], CakeOptionType.InnerCream, i + 1);

        // OuterCream  
        var outerCreams = new[] { "Toz Pembe", "Beyaz", "Çikolata", "Mavi", "Kırmızı", "Mor", "Yeşil", "Sarı", "Turuncu" };
        for (int i = 0; i < outerCreams.Length; i++) AddOption(outerCreams[i], CakeOptionType.OuterCream, i + 1);

        modelBuilder.Entity<CakeOption>().HasData(cakeOptions);
    }
}
