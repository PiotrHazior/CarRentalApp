# ProjektSemestralnyPO

Baza danych:
USE master;
GO

CREATE DATABASE CarRentalPH;
GO

USE CarRentalPH;
GO

CREATE TABLE [dbo].[Customers] (
    [ID_Customer] INT          IDENTITY (1, 1) NOT NULL,
    [LastName]    VARCHAR (30) NOT NULL,
    [Phone]       VARCHAR (11) NOT NULL,
    [Password]    VARCHAR (30) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID_Customer] ASC)
);

CREATE TABLE [dbo].[Employees] (
    [ID_Employee] INT           IDENTITY (1, 1) NOT NULL,
    [LastName]    VARCHAR (30)  NOT NULL,
    [PhoneNumber] NVARCHAR (11) NOT NULL,
    [Password]    VARCHAR (30)  NOT NULL,
    PRIMARY KEY CLUSTERED ([ID_Employee] ASC)
);

CREATE TABLE [dbo].[SportCars] (
    [ID_SportCar] INT           IDENTITY (1, 1) NOT NULL,
    [Brand]       NVARCHAR (15) NULL,
    [Model]       NVARCHAR (15) NULL,
    [Price]       MONEY         NOT NULL,
    PRIMARY KEY CLUSTERED ([ID_SportCar] ASC)
);

CREATE TABLE [dbo].[RentalCar] (
    [ID_Rental]    INT  IDENTITY (1, 1) NOT NULL,
    [RentalDate]   DATE NOT NULL,
    [DateOfReturn] DATE NOT NULL,
    [ID_Customer]  INT  NOT NULL,
    [ID_SportCar]  INT  NOT NULL,
    PRIMARY KEY CLUSTERED ([ID_Rental] ASC),
    CONSTRAINT [FK_RentalCar_SportCars] FOREIGN KEY ([ID_SportCar]) REFERENCES [dbo].[SportCars] ([ID_SportCar])
);

CREATE TABLE [dbo].[Reservation] (
    [ID_Reservation]    INT  IDENTITY (1, 1) NOT NULL,
    [DateOfReservation] DATE NOT NULL,
    [DateOfReceipt]     DATE NOT NULL,
    [ID_Customer]       INT  NOT NULL,
    [ID_SportCar]       INT  NOT NULL,
    [ID_Employee]       INT  NOT NULL,
    PRIMARY KEY CLUSTERED ([ID_Reservation] ASC),
    CONSTRAINT [r1] FOREIGN KEY ([ID_Customer]) REFERENCES [dbo].[Customers] ([ID_Customer]),
    CONSTRAINT [r3] FOREIGN KEY ([ID_Employee]) REFERENCES [dbo].[Employees] ([ID_Employee]),
    CONSTRAINT [r2] FOREIGN KEY ([ID_SportCar]) REFERENCES [dbo].[SportCars] ([ID_SportCar])
);

Aplikacja WPF C# - SPORTS RENTAL CAR
Aplikacja została stworzona dla klientów, którzy chcą wynająć sportowy samochód oraz dla 
pracowników, którzy mogą zarządzać samochodami. 
Program zawiera 5 tabel bazodanowy:
- Customers – przechowuje dane o klientach (ID klienta, nazwisko, hasło oraz telefon)
- Employees – przechowuje dane o pracownikach (ID pracownika, nazwisko, hasło oraz telefon)
- SportCars – przechowuje dane o samochodach (ID samochodu, marka, model, cena)
- RentalCar – przechowuje dane o wynajmie samochodów (ID wynajmu, ID samochodu, ID klienta, 
data wynajmu oraz data zwrotu
- Reservation – przechowuje dane o rezerwacji samochodu (ID rezerwacji, ID samochodu, ID klienta, 
data wynajmu oraz data zwrotu)
Aplikacja zawiera 7 okienek:
1. MainWindow – główne okno aplikacji, które zawiera 2 przyciski, dzięki którym można się 
przenieść do okna dla pracowników lub do okna dla klientów. Wszystko zależy od decyzji 
użytkownika aplikacji. 
2. EmployeeMenu – okno dla pracowników. Jeśli dany pracownik jest już w systemie, ma 
możliwość zalogowania się i przejścia do następnego okna do zarządzania samochodami. 
Natomiast jeśli osoba jeszcze nie ma konta, ma możliwość zarejestrowania się w systemie 
poprzez kliknięcie w przycisk Register.
3. RegisterEmployee – okno, dzięki któremu użytkownik ma możliwość stać się pracownikiem 
firmy i zarządzaniem sportowymi samochodami. Należy podać tylko swoje nazwisko, hasło 
oraz telefon, który wyląduje w systemie po zatwierdzeniu. 
4. ManageCarsMenu – okno do zarządzania samochodami w systemie. Pracownik może dodać 
samochód do wynajęcia poprzez podanie marki samochodu, modelu oraz ceny wynajmu, 
bądź też usunąć samochód z systemu poprzez podanie samego ID samochodu. W razie 
problemów jak zrobić daną czynność, w tym oknie jest przycisk HELP, który wytłumaczy jak i 
co należy zrobić.
5. CustomerMenu – okno dla klientów. Jeśli klient jest już w systemie i ma konto, może bez 
problemu zalogować się i przejść do następnego okna służącego do wynajmowania 
samochodu. Gdy jednak dany użytkownik nie posiada jeszcze konta, ma możliwość 
zarejestrowania się poprzez wciśnięcie przycisku Register, który przerzuci do następnego 
okna.
6. RegisterMenu – okno do rejestracji dla klientów. Dane wpisane przez użytkownika, takie jak 
nazwisko, hasło oraz telefon, po zatwierdzeniu lądują w systemie, dzięki czemu dana osoba 
ma możliwość zalogowania się na to konto.
7. RentalMenu – okno stworzone do wynajmowania samochodów. Klient ma możliwość 
wynajmu samochodu dostępnego w systemie, poprzez wybranie danego samochodu w tabeli 
oraz podanie daty wynajmu i daty zwrotu. W razie problemów istnieje przycisk o nazwie 
HELP, który wytłumaczy jak to zrobić. Jeśli natomiast klient chce anulować wynajem 
samochodu, ma oczywiście taką możliwość. Musi tylko podać ID danego samochodu.
Uwaga:
Rejestracja w oknie RegisterEmployee tworzy konto dla pracownika, więc nie może zalogować się w 
oknie CustomerMenu, ponieważ jest to okno dla klientów aplikacji. Działa to również w drugą stronę. 
Klient, po założeniu konta w oknie RegisterMenu, nie ma możliwości zalogowania się jako pracownik 
oraz do zarządzania samochodami w systemie.
