using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PastryFlow.Domain.Entities;
using PastryFlow.Domain.Enums;

namespace PastryFlow.Infrastructure.Data;

public static class SeedData
{
    private static readonly DateTime SeedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public static void Initialize(ModelBuilder modelBuilder)
    {
        SeedBranches(modelBuilder);
        SeedUsers(modelBuilder);
        SeedCategories(modelBuilder);
        SeedProducts(modelBuilder);
        SeedCakeOptions(modelBuilder);
        SeedWallets(modelBuilder);
    }

    // ─── ŞUBELER ──────────────────────────────────────────────

    private static void SeedBranches(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Branch>().HasData(
            new Branch
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111101"),
                Name = "Kırklareli Mutfak",
                City = City.Kirklareli,
                BranchType = BranchType.Production,
                IsActive = true,
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate
            },
            new Branch
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111102"),
                Name = "Edirne Mutfak",
                City = City.Edirne,
                BranchType = BranchType.Production,
                IsActive = true,
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate
            },
            new Branch
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111103"),
                Name = "Lüleburgaz Mutfak",
                City = City.Luleburgaz,
                BranchType = BranchType.Production,
                IsActive = true,
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate
            },
            new Branch
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111104"),
                Name = "Kırklareli Tezgah",
                City = City.Kirklareli,
                BranchType = BranchType.Sales,
                IsActive = true,
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate
            },
            new Branch
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111105"),
                Name = "Edirne Tezgah",
                City = City.Edirne,
                BranchType = BranchType.Sales,
                IsActive = true,
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate
            },
            new Branch
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111106"),
                Name = "Lüleburgaz Tezgah",
                City = City.Luleburgaz,
                BranchType = BranchType.Sales,
                IsActive = true,
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate
            }
        );
    }

    // ─── KULLANICILAR ─────────────────────────────────────────

    private static void SeedUsers(ModelBuilder modelBuilder)
    {
        // Şifre: PastryFlow2024!
        const string passwordHash = "$2a$11$NleqkNOCjv3N1YWwrJYYfuHYXvpO5vaE1.It3fUTKLgHrTkXCABWO";

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333301"),
                Email = "admin@pastryflow.com",
                FullName = "Admin Kullanıcı",
                Role = UserRole.Admin,
                BranchId = null,
                PasswordHash = passwordHash,
                IsActive = true,
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate
            },
            new User
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333302"),
                Email = "kirklareli.mutfak@pastryflow.com",
                FullName = "Kırklareli Üretim",
                Role = UserRole.Production,
                BranchId = Guid.Parse("11111111-1111-1111-1111-111111111101"),
                PasswordHash = passwordHash,
                IsActive = true,
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate
            },
            new User
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333303"),
                Email = "edirne.mutfak@pastryflow.com",
                FullName = "Edirne Üretim",
                Role = UserRole.Production,
                BranchId = Guid.Parse("11111111-1111-1111-1111-111111111102"),
                PasswordHash = passwordHash,
                IsActive = true,
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate
            },
            new User
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333304"),
                Email = "luleburgaz.mutfak@pastryflow.com",
                FullName = "Lüleburgaz Üretim",
                Role = UserRole.Production,
                BranchId = Guid.Parse("11111111-1111-1111-1111-111111111103"),
                PasswordHash = passwordHash,
                IsActive = true,
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate
            },
            new User
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333305"),
                Email = "kirklareli.tezgah@pastryflow.com",
                FullName = "Kırklareli Satış",
                Role = UserRole.Sales,
                BranchId = Guid.Parse("11111111-1111-1111-1111-111111111104"),
                PasswordHash = passwordHash,
                IsActive = true,
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate
            },
            new User
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333306"),
                Email = "edirne.tezgah@pastryflow.com",
                FullName = "Edirne Satış",
                Role = UserRole.Sales,
                BranchId = Guid.Parse("11111111-1111-1111-1111-111111111105"),
                PasswordHash = passwordHash,
                IsActive = true,
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate
            },
            new User
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333307"),
                Email = "luleburgaz.tezgah@pastryflow.com",
                FullName = "Lüleburgaz Satış",
                Role = UserRole.Sales,
                BranchId = Guid.Parse("11111111-1111-1111-1111-111111111106"),
                PasswordHash = passwordHash,
                IsActive = true,
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate
            },
            new User
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333308"),
                Email = "sofor@pastryflow.com",
                FullName = "Şoför",
                Role = UserRole.Driver,
                BranchId = null,
                PasswordHash = passwordHash,
                IsActive = true,
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate
            }
        );
    }

    // ─── KATEGORİLER ──────────────────────────────────────────

    private static void SeedCategories(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = Guid.Parse("22222222-2222-2222-2222-222222222201"), Name = "KEK", SortOrder = 1, IsActive = true, CreatedAt = SeedDate, UpdatedAt = SeedDate },
            new Category { Id = Guid.Parse("22222222-2222-2222-2222-222222222202"), Name = "MAYALILAR", SortOrder = 2, IsActive = true, CreatedAt = SeedDate, UpdatedAt = SeedDate },
            new Category { Id = Guid.Parse("22222222-2222-2222-2222-222222222203"), Name = "KURABİYE", SortOrder = 3, IsActive = true, CreatedAt = SeedDate, UpdatedAt = SeedDate },
            new Category { Id = Guid.Parse("22222222-2222-2222-2222-222222222204"), Name = "PASTALAR", SortOrder = 4, IsActive = true, CreatedAt = SeedDate, UpdatedAt = SeedDate },
            new Category { Id = Guid.Parse("22222222-2222-2222-2222-222222222205"), Name = "İÇECEK", SortOrder = 5, IsActive = true, CreatedAt = SeedDate, UpdatedAt = SeedDate },
            new Category { Id = Guid.Parse("22222222-2222-2222-2222-222222222206"), Name = "FIRIN", SortOrder = 6, IsActive = true, CreatedAt = SeedDate, UpdatedAt = SeedDate },
            new Category { Id = Guid.Parse("22222222-2222-2222-2222-222222222207"), Name = "KAHVALTI", SortOrder = 7, IsActive = true, CreatedAt = SeedDate, UpdatedAt = SeedDate },
            new Category { Id = Guid.Parse("22222222-2222-2222-2222-222222222208"), Name = "HAMMADDE", SortOrder = 8, IsActive = true, CreatedAt = SeedDate, UpdatedAt = SeedDate }
        );
    }

    // ─── ÜRÜNLER ──────────────────────────────────────────────

    private static void SeedProducts(ModelBuilder modelBuilder)
    {
        var p1 = Guid.Parse("11111111-1111-1111-1111-111111111101"); // Kırklareli Mutfak
        var p2 = Guid.Parse("11111111-1111-1111-1111-111111111102"); // Edirne Mutfak
        var p3 = Guid.Parse("11111111-1111-1111-1111-111111111103"); // Lüleburgaz Mutfak

        var catKek      = Guid.Parse("22222222-2222-2222-2222-222222222201");
        var catMayali   = Guid.Parse("22222222-2222-2222-2222-222222222202");
        var catKurabiye = Guid.Parse("22222222-2222-2222-2222-222222222203");
        var catPasta    = Guid.Parse("22222222-2222-2222-2222-222222222204");
        var catIcecek   = Guid.Parse("22222222-2222-2222-2222-222222222205");
        var catFirin    = Guid.Parse("22222222-2222-2222-2222-222222222206");
        var catKahvalti = Guid.Parse("22222222-2222-2222-2222-222222222207");
        var catHam      = Guid.Parse("22222222-2222-2222-2222-222222222208");

        var products = new List<Product>();
        int counter = 1;

        Product P(string name, UnitType unit, Guid? branch, Guid cat,
                  TrackingType tracking = TrackingType.Production,
                  decimal? price = null, bool isRaw = false)
        {
            var id = Guid.Parse($"44444444-4444-4444-4444-{counter:D12}");
            counter++;
            return new Product
            {
                Id = id,
                Name = name,
                Unit = unit,
                ProductionBranchId = branch,
                CategoryId = cat,
                TrackingType = tracking,
                UnitPrice = price,
                ProductType = isRaw ? ProductType.RawMaterial : ProductType.FinishedProduct,
                IsActive = true,
                SortOrder = counter,
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate
            };
        }

        // ── KEK (Kırklareli Mutfak) ──────────────────────────
        products.Add(P("KEK DİLİM",         UnitType.Adet, p1, catKek, price: 45m));
        products.Add(P("KEK KALIP KÜÇÜK",   UnitType.Adet, p1, catKek, price: 180m));
        products.Add(P("KEK KALIP ORTA",    UnitType.Adet, p1, catKek, price: 250m));
        products.Add(P("KEK KALIP BÜYÜK",   UnitType.Adet, p1, catKek, price: 320m));
        products.Add(P("KEK MUFİN",         UnitType.Adet, p1, catKek, price: 35m));

        // ── MAYALILAR (Lüleburgaz Mutfak) ───────────────────
        products.Add(P("AÇMA",              UnitType.Adet, p3, catMayali, price: 30m));
        products.Add(P("BÖREK",             UnitType.Adet, p3, catMayali, price: 55m));
        products.Add(P("KAFKAS BÖREĞİ",     UnitType.Adet, p3, catMayali, price: 65m));
        products.Add(P("İÇLİ SİMİT",        UnitType.Adet, p3, catMayali, price: 35m));
        products.Add(P("İÇLİ SANDVİÇ",      UnitType.Adet, p3, catMayali, price: 60m));
        products.Add(P("PİZZA",             UnitType.Adet, p3, catMayali, price: 80m));
        products.Add(P("POĞAÇA",            UnitType.Adet, p3, catMayali, price: 30m));
        products.Add(P("POĞAÇA İRAN",       UnitType.Adet, p3, catMayali, price: 35m));
        products.Add(P("POĞAÇA SAKALLI",    UnitType.Adet, p3, catMayali, price: 35m));
        products.Add(P("SİMİT",             UnitType.Adet, p3, catMayali, price: 20m));
        products.Add(P("SU BÖREĞİ",         UnitType.Adet, p3, catMayali, price: 70m));

        // ── KURABİYE (Lüleburgaz Mutfak) ────────────────────
        products.Add(P("ACIBADEM",               UnitType.Kg,   p3, catKurabiye, price: 320m));
        products.Add(P("BEZE BÜYÜK",             UnitType.Adet, p3, catKurabiye, price: 45m));
        products.Add(P("BEZE KUTU",              UnitType.Adet, p3, catKurabiye, price: 120m));
        products.Add(P("GÜLEN YÜZ",              UnitType.Kg,   p3, catKurabiye, price: 280m));
        products.Add(P("KANDİL SİMİDİ",          UnitType.Adet, p3, catKurabiye, price: 25m));
        products.Add(P("KURABİYE EKSTRA",         UnitType.Kg,   p3, catKurabiye, price: 300m));
        products.Add(P("KURABİYE MUZ / BONCUK",  UnitType.Kg,   p3, catKurabiye, price: 280m));
        products.Add(P("KURABİYE SADE",           UnitType.Kg,   p3, catKurabiye, price: 260m));
        products.Add(P("KURABİYE ŞAM",            UnitType.Kg,   p3, catKurabiye, price: 290m));
        products.Add(P("KURABİYE TAHİNLİ",        UnitType.Kg,   p3, catKurabiye, price: 270m));
        products.Add(P("KURABİYE TART",           UnitType.Adet, p3, catKurabiye, price: 35m));
        products.Add(P("KURABİYE UN",             UnitType.Kg,   p3, catKurabiye, price: 250m));
        products.Add(P("KURU PASTA",              UnitType.Kg,   p3, catKurabiye, price: 300m));

        // ── PASTALAR (Lüleburgaz Mutfak) ────────────────────
        products.Add(P("ALMAN PASTASI",               UnitType.Adet, p3, catPasta, price: 380m));
        products.Add(P("FİGÜR-BUDAPEŞTE-SEBASTİAN",   UnitType.Adet, p3, catPasta, price: 450m));
        products.Add(P("PASTA ADET",                  UnitType.Adet, p3, catPasta, price: 350m));
        products.Add(P("PASTA B1",                    UnitType.Adet, p3, catPasta, price: 420m));
        products.Add(P("PASTA CHEESECAKE",            UnitType.Adet, p3, catPasta, price: 400m));
        products.Add(P("PASTA KG",                    UnitType.Kg,   p3, catPasta, price: 280m));
        products.Add(P("PASTA NO 0",                  UnitType.Adet, p3, catPasta, price: 280m));
        products.Add(P("PASTA NO 1",                  UnitType.Adet, p3, catPasta, price: 350m));
        products.Add(P("RULO SARMA",                  UnitType.Adet, p3, catPasta, price: 320m));
        products.Add(P("SİPARİŞ PASTA",              UnitType.Adet, p3, catPasta, price: 500m));
        products.Add(P("SÜTLÜ TATLILAR",             UnitType.Adet, p3, catPasta, price: 85m));
        products.Add(P("TRİLEÇE",                    UnitType.Adet, p3, catPasta, price: 90m));

        // ── İÇECEK ──────────────────────────────────────────
        // Counter (stok takibi yok, satılan girilir)
        products.Add(P("ÇAY SATIŞ",    UnitType.Adet, null, catIcecek, TrackingType.Counter,   price: 20m));
        products.Add(P("TÜRK KAHVESİ", UnitType.Adet, null, catIcecek, TrackingType.Counter,   price: 35m));
        products.Add(P("ESPRESSO",     UnitType.Adet, null, catIcecek, TrackingType.Counter,   price: 40m));
        products.Add(P("BİTKİ ÇAYI",   UnitType.Adet, null, catIcecek, TrackingType.Counter,   price: 25m));
        // Purchased (satın alınıp satılan)
        products.Add(P("AYRAN",        UnitType.Adet, null, catIcecek, TrackingType.Purchased, price: 25m));
        products.Add(P("GAZLI İÇECEK", UnitType.Adet, null, catIcecek, TrackingType.Purchased, price: 30m));
        products.Add(P("LİMONATA",     UnitType.Adet, null, catIcecek, TrackingType.Purchased, price: 35m));
        products.Add(P("MEYVE SUYU",   UnitType.Adet, null, catIcecek, TrackingType.Purchased, price: 30m));
        products.Add(P("SODA MEYVELİ", UnitType.Adet, null, catIcecek, TrackingType.Purchased, price: 25m));
        products.Add(P("SODA SADE",    UnitType.Adet, null, catIcecek, TrackingType.Purchased, price: 20m));
        products.Add(P("SU 0.5 L",     UnitType.Adet, null, catIcecek, TrackingType.Purchased, price: 15m));
        products.Add(P("PİKNİK ÜRÜNLER", UnitType.Adet, p3, catIcecek, TrackingType.Production, price: null));

        // ── KAHVALTI (Counter) ───────────────────────────────
        products.Add(P("Kahvaltı Tabağı", UnitType.Adet, null, catKahvalti, TrackingType.Counter, price: 250m));
        products.Add(P("Serpme Kahvaltı", UnitType.Adet, null, catKahvalti, TrackingType.Counter, price: 500m));

        // ── FIRIN (Lüleburgaz + Edirne Mutfak) ──────────────
        products.Add(P("ÇAVDAR EKMEĞİ",    UnitType.Adet, p2, catFirin, price: 45m));
        products.Add(P("EKŞİ MAYA 2'Lİ",   UnitType.Adet, p2, catFirin, price: 60m));
        products.Add(P("EKŞİ MAYA 3'LÜ",   UnitType.Adet, p2, catFirin, price: 85m));
        products.Add(P("KEPEK EKMEĞİ",     UnitType.Adet, p2, catFirin, price: 40m));
        products.Add(P("MISIR EKMEĞİ",     UnitType.Adet, p2, catFirin, price: 40m));
        products.Add(P("PİDE",             UnitType.Adet, p2, catFirin, price: 35m));
        products.Add(P("SANDVİÇ EKMEĞİ",   UnitType.Adet, p2, catFirin, price: 30m));
        products.Add(P("SİYEZ EKMEĞİ",     UnitType.Adet, p2, catFirin, price: 50m));
        products.Add(P("TAHİLLI EKMEK",    UnitType.Adet, p2, catFirin, price: 45m));
        products.Add(P("TAM BUĞDAY",       UnitType.Adet, p2, catFirin, price: 40m));
        products.Add(P("TEKLİ BEYAZ",      UnitType.Adet, p2, catFirin, price: 25m));
        products.Add(P("ÇİFTLİ BEYAZ",     UnitType.Adet, p2, catFirin, price: 40m));
        // Lüleburgaz fırın ürünleri
        products.Add(P("EKMEK BEYAZ",      UnitType.Adet, p3, catFirin, price: 25m));
        products.Add(P("EKMEK ÇEŞİT",     UnitType.Adet, p3, catFirin, price: 40m));
        products.Add(P("GALETE",           UnitType.Adet, p3, catFirin, price: 30m));
        products.Add(P("PİDE ÇİFTLİ",     UnitType.Adet, p3, catFirin, price: 50m));
        products.Add(P("TANDIR EKMEĞİ",   UnitType.Adet, p3, catFirin, price: 35m));

        // ── HAMMADDE (Shared — stok takibi yok) ─────────────
        products.Add(P("AYÇİÇEK YAĞI",     UnitType.Litre, null, catHam, isRaw: true));
        products.Add(P("KAKAO",            UnitType.Kg,    null, catHam, isRaw: true));
        products.Add(P("KABARTMA TOZU",    UnitType.Kg,    null, catHam, isRaw: true));
        products.Add(P("KREMA",            UnitType.Litre, null, catHam, isRaw: true));
        products.Add(P("MARGARİN",         UnitType.Kg,    null, catHam, isRaw: true));
        products.Add(P("MAYA",             UnitType.Kg,    null, catHam, isRaw: true));
        products.Add(P("NİŞASTA",          UnitType.Kg,    null, catHam, isRaw: true));
        products.Add(P("ŞEKER",            UnitType.Kg,    null, catHam, isRaw: true));
        products.Add(P("SÜT",             UnitType.Litre, null, catHam, isRaw: true));
        products.Add(P("TEREYAĞI",         UnitType.Kg,    null, catHam, isRaw: true));
        products.Add(P("TUZ",             UnitType.Kg,    null, catHam, isRaw: true));
        products.Add(P("UN",              UnitType.Kg,    null, catHam, isRaw: true));
        products.Add(P("YUMURTA",         UnitType.Adet,  null, catHam, isRaw: true));

        modelBuilder.Entity<Product>().HasData(products);
    }

    // ─── PASTA SEÇENEKLERİ ────────────────────────────────────

    private static void SeedCakeOptions(ModelBuilder modelBuilder)
    {
        var options = new List<CakeOption>();
        int counter = 1;

        CakeOption O(string name, CakeOptionType type, int sort)
        {
            var id = Guid.Parse($"55555555-5555-5555-5555-{counter:D12}");
            counter++;
            return new CakeOption
            {
                Id = id,
                Name = name,
                OptionType = type,
                SortOrder = sort,
                IsActive = true,
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate
            };
        }

        // Kek tipi
        options.Add(O("Kakaolu",       CakeOptionType.CakeType, 1));
        options.Add(O("Vanilyalı",     CakeOptionType.CakeType, 2));
        options.Add(O("Meyveli",       CakeOptionType.CakeType, 3));
        options.Add(O("Havuçlu",       CakeOptionType.CakeType, 4));
        options.Add(O("Muzlu",         CakeOptionType.CakeType, 5));
        options.Add(O("Limonlu",       CakeOptionType.CakeType, 6));

        // İç krema
        options.Add(O("Çikolatalı",         CakeOptionType.InnerCream, 1));
        options.Add(O("Muzlu",              CakeOptionType.InnerCream, 2));
        options.Add(O("Frambuazlı",         CakeOptionType.InnerCream, 3));
        options.Add(O("Vanilyalı",          CakeOptionType.InnerCream, 4));
        options.Add(O("Karamelli",          CakeOptionType.InnerCream, 5));
        options.Add(O("Fıstıklı",           CakeOptionType.InnerCream, 6));
        options.Add(O("Beyaz Çikolatalı",   CakeOptionType.InnerCream, 7));

        // Dış krema
        options.Add(O("Beyaz",    CakeOptionType.OuterCream, 1));
        options.Add(O("Çikolata", CakeOptionType.OuterCream, 2));
        options.Add(O("Toz Pembe",CakeOptionType.OuterCream, 3));
        options.Add(O("Mavi",     CakeOptionType.OuterCream, 4));
        options.Add(O("Kırmızı",  CakeOptionType.OuterCream, 5));
        options.Add(O("Mor",      CakeOptionType.OuterCream, 6));
        options.Add(O("Yeşil",    CakeOptionType.OuterCream, 7));
        options.Add(O("Sarı",     CakeOptionType.OuterCream, 8));
        options.Add(O("Turuncu",  CakeOptionType.OuterCream, 9));

        modelBuilder.Entity<CakeOption>().HasData(options);
    }

    // ─── WALLET BAŞLANGIÇ KAYITLARI ───────────────────────────

    private static void SeedWallets(ModelBuilder modelBuilder)
    {
        // BranchWallet — her şube için Cash + Bank (CurrentBalance = 0)
        // Admin panelinden SetInitialBalance ile gerçek değerler girilecek
        var branchIds = new[]
        {
            Guid.Parse("11111111-1111-1111-1111-111111111101"),
            Guid.Parse("11111111-1111-1111-1111-111111111102"),
            Guid.Parse("11111111-1111-1111-1111-111111111103"),
            Guid.Parse("11111111-1111-1111-1111-111111111104"),
            Guid.Parse("11111111-1111-1111-1111-111111111105"),
            Guid.Parse("11111111-1111-1111-1111-111111111106"),
        };

        var wallets = new List<BranchWallet>();
        int wCounter = 1;

        foreach (var branchId in branchIds)
        {
            wallets.Add(new BranchWallet
            {
                Id = Guid.Parse($"66666666-6666-6666-6666-{wCounter:D12}"),
                BranchId = branchId,
                WalletType = WalletType.Cash,
                CurrentBalance = 0,
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate
            });
            wCounter++;

            wallets.Add(new BranchWallet
            {
                Id = Guid.Parse($"66666666-6666-6666-6666-{wCounter:D12}"),
                BranchId = branchId,
                WalletType = WalletType.Bank,
                CurrentBalance = 0,
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate
            });
            wCounter++;
        }

        modelBuilder.Entity<BranchWallet>().HasData(wallets);

        // AdminWallet — Cash + Bank
        modelBuilder.Entity<AdminWallet>().HasData(
            new AdminWallet
            {
                Id = Guid.Parse("77777777-7777-7777-7777-000000000001"),
                WalletType = WalletType.Cash,
                CurrentBalance = 0,
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate
            },
            new AdminWallet
            {
                Id = Guid.Parse("77777777-7777-7777-7777-000000000002"),
                WalletType = WalletType.Bank,
                CurrentBalance = 0,
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate
            }
        );
    }
}
