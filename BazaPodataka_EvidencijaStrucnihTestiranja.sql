/*
    Evidencija stručnih testiranja
    SQL skripta za kreiranje baze, tabela, relacija,
    stored procedure, pogleda i početnog admin korisnika.
*/

IF DB_ID(N'EvidencijaStrucnihTestiranja') IS NULL
BEGIN
    CREATE DATABASE EvidencijaStrucnihTestiranja;
END;
GO

USE EvidencijaStrucnihTestiranja;
GO

/* =========================
   TABELA: ZAPOSLENI
   ========================= */
IF OBJECT_ID(N'dbo.ZAPOSLENI', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.ZAPOSLENI
    (
        JMBG        NVARCHAR(13)  NOT NULL,
        Ime         NVARCHAR(40)  NOT NULL,
        Prezime     NVARCHAR(40)  NOT NULL,
        RadnoMesto  NVARCHAR(50) NOT NULL,
        Email       NVARCHAR(100) NOT NULL,

        CONSTRAINT PK_ZAPOSLENI PRIMARY KEY (JMBG),
        CONSTRAINT CK_ZAPOSLENI_JMBG CHECK
        (
            LEN(JMBG) = 13 AND JMBG NOT LIKE '%[^0-9]%'
        )
    );
END;
GO

/* =========================
   TABELA: VRSTA_TESTA
   ========================= */
IF OBJECT_ID(N'dbo.VRSTA_TESTA', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.VRSTA_TESTA
    (
        IDVrsteTesta       INT IDENTITY(1,1) NOT NULL,
        Naziv               NVARCHAR(60) NOT NULL,
        MinimalanBrojPoena  INT NOT NULL,

        CONSTRAINT PK_VRSTA_TESTA PRIMARY KEY (IDVrsteTesta),
        CONSTRAINT CK_VRSTA_TESTA_MIN_POENI CHECK
        (
            MinimalanBrojPoena BETWEEN 0 AND 100
        )
    );
END;
GO

/* =========================
   TABELA: KORISNIK
   ========================= */
IF OBJECT_ID(N'dbo.KORISNIK', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.KORISNIK
    (
        IDKorisnika    INT IDENTITY(1,1) NOT NULL,
        Ime             NVARCHAR(40)  NOT NULL,
        Prezime         NVARCHAR(40)  NOT NULL,
        KorisnickoIme   NVARCHAR(30)  NOT NULL,
        Sifra           NVARCHAR(100) NOT NULL,
        Status          NVARCHAR(20)  NOT NULL,
        Uloga           NVARCHAR(20)  NOT NULL,

        CONSTRAINT PK_KORISNIK PRIMARY KEY (IDKorisnika),
        CONSTRAINT UQ_KORISNIK_KorisnickoIme UNIQUE (KorisnickoIme)
    );
END;
GO

/* =========================
   TABELA: TESTIRANJE
   ========================= */
IF OBJECT_ID(N'dbo.TESTIRANJE', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.TESTIRANJE
    (
        IDTestiranja     INT IDENTITY(1,1) NOT NULL,
        JMBG              NVARCHAR(13) NOT NULL,
        IDVrsteTesta      INT NOT NULL,
        DatumTestiranja   DATE NOT NULL,
        BrojPoena         INT NOT NULL,
        Polozio           BIT NOT NULL,

        CONSTRAINT PK_TESTIRANJE PRIMARY KEY (IDTestiranja),
        CONSTRAINT CK_TESTIRANJE_BrojPoena CHECK
        (
            BrojPoena BETWEEN 0 AND 100
        ),
        CONSTRAINT FK_TESTIRANJE_ZAPOSLENI
            FOREIGN KEY (JMBG)
            REFERENCES dbo.ZAPOSLENI(JMBG),
        CONSTRAINT FK_TESTIRANJE_VRSTA_TESTA
            FOREIGN KEY (IDVrsteTesta)
            REFERENCES dbo.VRSTA_TESTA(IDVrsteTesta)
    );
END;
GO

/* =========================
   TABELA: DNEVNIK_AKTIVNOSTI
   ========================= */
IF OBJECT_ID(N'dbo.DNEVNIK_AKTIVNOSTI', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.DNEVNIK_AKTIVNOSTI
    (
        IDDnevnika     INT IDENTITY(1,1) NOT NULL,
        IDTestiranja   INT NULL,
        Akcija          NVARCHAR(100) NOT NULL,
        DatumVreme      DATETIME2 NOT NULL
            CONSTRAINT DF_DNEVNIK_AKTIVNOSTI_DatumVreme DEFAULT SYSDATETIME(),

        CONSTRAINT PK_DNEVNIK_AKTIVNOSTI PRIMARY KEY (IDDnevnika),
        CONSTRAINT FK_Dnevnik_Testiranje
            FOREIGN KEY (IDTestiranja)
            REFERENCES dbo.TESTIRANJE(IDTestiranja)
            ON DELETE SET NULL
    );
END;
GO

/* =========================
   STORED PROCEDURE: DodajTestiranje
   Vraća ID novog testiranja preko SELECT-a,
   ========================= */
CREATE OR ALTER PROCEDURE dbo.DodajTestiranje
    @JMBG NVARCHAR(13),
    @IDVrsteTesta INT,
    @DatumTestiranja DATE,
    @BrojPoena INT,
    @Polozio BIT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.TESTIRANJE
        (JMBG, IDVrsteTesta, DatumTestiranja, BrojPoena, Polozio)
    OUTPUT INSERTED.IDTestiranja
    VALUES
        (@JMBG, @IDVrsteTesta, @DatumTestiranja, @BrojPoena, @Polozio);
END;
GO

/* =========================
   POGLED: PregledTestiranja
   ========================= */
CREATE OR ALTER VIEW dbo.PregledTestiranja
AS
    SELECT
        t.IDTestiranja,
        t.JMBG,
        z.Ime,
        z.Prezime,
        t.IDVrsteTesta,
        vt.Naziv AS NazivVrsteTesta,
        t.DatumTestiranja,
        t.BrojPoena,
        t.Polozio
    FROM dbo.TESTIRANJE AS t
    INNER JOIN dbo.ZAPOSLENI AS z
        ON z.JMBG = t.JMBG
    INNER JOIN dbo.VRSTA_TESTA AS vt
        ON vt.IDVrsteTesta = t.IDVrsteTesta;
GO

/* =========================
   POČETNI KORISNICI
   ========================= */

INSERT INTO KORISNIK
    (Ime, Prezime, KorisnickoIme, Sifra, Status, Uloga)
VALUES
    ('Admin', 'Administrator',
     'admin', 'admin123', 'Aktivan', 'Admin'),

    ('Operater', 'Korisnik',
     'operater', 'operater123', 'Aktivan', 'Operater');
GO


/* =========================
   POČETNE VRSTE TESTOVA
   ========================= */

IF NOT EXISTS (SELECT 1 FROM dbo.VRSTA_TESTA)
BEGIN
    INSERT INTO dbo.VRSTA_TESTA (Naziv, MinimalanBrojPoena)
    VALUES
        (N'Test stručnog znanja', 50),
        (N'Bezbednost i zaštita na radu', 50),
        (N'Provera praktičnih veština', 50);
END;
GO


