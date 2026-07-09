IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260709071331_IlkOlusturma'
)
BEGIN
    CREATE TABLE [Hizmetler] (
        [Id] int NOT NULL IDENTITY,
        [Baslik] nvarchar(150) NOT NULL,
        [Aciklama] nvarchar(1000) NOT NULL,
        [GorselUrl] nvarchar(300) NULL,
        [AktifMi] bit NOT NULL,
        [SiraNo] int NOT NULL,
        CONSTRAINT [PK_Hizmetler] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260709071331_IlkOlusturma'
)
BEGIN
    CREATE TABLE [IletisimMesajlari] (
        [Id] int NOT NULL IDENTITY,
        [AdSoyad] nvarchar(100) NOT NULL,
        [Email] nvarchar(150) NOT NULL,
        [Telefon] nvarchar(20) NULL,
        [EtkinlikTuru] nvarchar(100) NULL,
        [Mesaj] nvarchar(2000) NOT NULL,
        [KayitTarihi] datetime2 NOT NULL,
        [OkunduMu] bit NOT NULL,
        CONSTRAINT [PK_IletisimMesajlari] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260709071331_IlkOlusturma'
)
BEGIN
    CREATE TABLE [Referanslar] (
        [Id] int NOT NULL IDENTITY,
        [MusteriAdi] nvarchar(150) NOT NULL,
        [Yorum] nvarchar(500) NULL,
        [LogoUrl] nvarchar(300) NULL,
        [AktifMi] bit NOT NULL,
        CONSTRAINT [PK_Referanslar] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260709071331_IlkOlusturma'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260709071331_IlkOlusturma', N'10.0.9');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260709184256_SeedDataEklendi'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Aciklama', N'AktifMi', N'Baslik', N'GorselUrl', N'SiraNo') AND [object_id] = OBJECT_ID(N'[Hizmetler]'))
        SET IDENTITY_INSERT [Hizmetler] ON;
    EXEC(N'INSERT INTO [Hizmetler] ([Id], [Aciklama], [AktifMi], [Baslik], [GorselUrl], [SiraNo])
    VALUES (1, N''Lansman, bayi toplantısı, gala gecesi ve şirket organizasyonlarında uçtan uca planlama.'', CAST(1 AS bit), N''Kurumsal Etkinlikler'', NULL, 1),
    (2, N''Nişandan kına gecesine, salon süslemesinden orkestraya hayalinizdeki düğünü kurgular, siz sadece anın tadını çıkarırsınız.'', CAST(1 AS bit), N''Düğün Organizasyonu'', NULL, 2),
    (3, N''Sahne kurulumu, ses-ışık sistemleri, sanatçı yönetimi ve güvenlik koordinasyonu dahil büyük ölçekli etkinlik yönetimi.'', CAST(1 AS bit), N''Festival ve Konser'', NULL, 3),
    (4, N''Mağaza açılışı, ürün lansmanı ve basın etkinliklerinde markanızı en doğru şekilde sahneye koyuyoruz.'', CAST(1 AS bit), N''Açılış ve Lansman'', NULL, 4)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Aciklama', N'AktifMi', N'Baslik', N'GorselUrl', N'SiraNo') AND [object_id] = OBJECT_ID(N'[Hizmetler]'))
        SET IDENTITY_INSERT [Hizmetler] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260709184256_SeedDataEklendi'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AktifMi', N'LogoUrl', N'MusteriAdi', N'Yorum') AND [object_id] = OBJECT_ID(N'[Referanslar]'))
        SET IDENTITY_INSERT [Referanslar] ON;
    EXEC(N'INSERT INTO [Referanslar] ([Id], [AktifMi], [LogoUrl], [MusteriAdi], [Yorum])
    VALUES (1, CAST(1 AS bit), NULL, N''Yılmaz Holding'', N''Bayi toplantımız kusursuz geçti, her detay düşünülmüştü.''),
    (2, CAST(1 AS bit), NULL, N''Elif & Mert'', N''Düğünümüz hayal ettiğimizden de güzeldi, iyi ki sizi seçmişiz!''),
    (3, CAST(1 AS bit), NULL, N''TechNova Yazılım'', N''Ürün lansmanımızda basın ve konuk yönetimi profesyonelceydi.'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AktifMi', N'LogoUrl', N'MusteriAdi', N'Yorum') AND [object_id] = OBJECT_ID(N'[Referanslar]'))
        SET IDENTITY_INSERT [Referanslar] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260709184256_SeedDataEklendi'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260709184256_SeedDataEklendi', N'10.0.9');
END;

COMMIT;
GO

