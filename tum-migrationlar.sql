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

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260710065015_EtkinlikSemasiEklendi'
)
BEGIN
    ALTER TABLE [Hizmetler] ADD [DetayAciklama] nvarchar(4000) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260710065015_EtkinlikSemasiEklendi'
)
BEGIN
    ALTER TABLE [Hizmetler] ADD [Slug] nvarchar(160) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260710065015_EtkinlikSemasiEklendi'
)
BEGIN
    CREATE TABLE [Etkinlikler] (
        [Id] int NOT NULL IDENTITY,
        [Baslik] nvarchar(150) NOT NULL,
        [Slug] nvarchar(160) NOT NULL,
        [Aciklama] nvarchar(2000) NULL,
        [Mekan] nvarchar(150) NULL,
        [Tarih] datetime2 NOT NULL,
        [YayindaMi] bit NOT NULL,
        [HizmetId] int NOT NULL,
        CONSTRAINT [PK_Etkinlikler] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Etkinlikler_Hizmetler_HizmetId] FOREIGN KEY ([HizmetId]) REFERENCES [Hizmetler] ([Id]) ON DELETE NO ACTION
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260710065015_EtkinlikSemasiEklendi'
)
BEGIN
    CREATE TABLE [EtkinlikFotograflari] (
        [Id] int NOT NULL IDENTITY,
        [DosyaYolu] nvarchar(300) NOT NULL,
        [AltMetin] nvarchar(200) NULL,
        [KapakMi] bit NOT NULL,
        [SiraNo] int NOT NULL,
        [EtkinlikId] int NOT NULL,
        CONSTRAINT [PK_EtkinlikFotograflari] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_EtkinlikFotograflari_Etkinlikler_EtkinlikId] FOREIGN KEY ([EtkinlikId]) REFERENCES [Etkinlikler] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260710065015_EtkinlikSemasiEklendi'
)
BEGIN
    EXEC(N'UPDATE [Hizmetler] SET [DetayAciklama] = NULL, [Slug] = NULL
    WHERE [Id] = 1;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260710065015_EtkinlikSemasiEklendi'
)
BEGIN
    EXEC(N'UPDATE [Hizmetler] SET [DetayAciklama] = NULL, [Slug] = NULL
    WHERE [Id] = 2;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260710065015_EtkinlikSemasiEklendi'
)
BEGIN
    EXEC(N'UPDATE [Hizmetler] SET [DetayAciklama] = NULL, [Slug] = NULL
    WHERE [Id] = 3;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260710065015_EtkinlikSemasiEklendi'
)
BEGIN
    EXEC(N'UPDATE [Hizmetler] SET [DetayAciklama] = NULL, [Slug] = NULL
    WHERE [Id] = 4;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260710065015_EtkinlikSemasiEklendi'
)
BEGIN
    CREATE INDEX [IX_EtkinlikFotograflari_EtkinlikId] ON [EtkinlikFotograflari] ([EtkinlikId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260710065015_EtkinlikSemasiEklendi'
)
BEGIN
    CREATE INDEX [IX_Etkinlikler_HizmetId] ON [Etkinlikler] ([HizmetId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260710065015_EtkinlikSemasiEklendi'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Etkinlikler_Slug] ON [Etkinlikler] ([Slug]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260710065015_EtkinlikSemasiEklendi'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260710065015_EtkinlikSemasiEklendi', N'10.0.9');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260710065642_IdentityEklendi'
)
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260710065642_IdentityEklendi'
)
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260710065642_IdentityEklendi'
)
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260710065642_IdentityEklendi'
)
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260710065642_IdentityEklendi'
)
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260710065642_IdentityEklendi'
)
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260710065642_IdentityEklendi'
)
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260710065642_IdentityEklendi'
)
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260710065642_IdentityEklendi'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260710065642_IdentityEklendi'
)
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260710065642_IdentityEklendi'
)
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260710065642_IdentityEklendi'
)
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260710065642_IdentityEklendi'
)
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260710065642_IdentityEklendi'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260710065642_IdentityEklendi'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260710065642_IdentityEklendi', N'10.0.9');
END;

COMMIT;
GO

