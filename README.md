# CarApp - WPF C# Application

I would like to show you my first WPF C# application. This application was created for customers who want to rent a car and for employees who manage the car fleet. Below, I present some brief information about what our application offers.

##Application Features - Windows
### Main Window
This is the main application window that serves to differentiate between employees and customers.
![image](https://github.com/PiotrHazior/ProjektSemestralnyPO/assets/86978938/06e68468-8488-420a-988b-0559395293d6)

### Customer Menu
Customers can log in here if they have an account or register if they are new users.
![image](https://github.com/PiotrHazior/ProjektSemestralnyPO/assets/86978938/475c8a29-6288-4c50-99b4-50826d647a26)

### Customer Registration Menu
This window is dedicated to customer registration. Here, users can create their own accounts.
![image](https://github.com/PiotrHazior/ProjektSemestralnyPO/assets/86978938/52bc8c10-2a10-4204-bad0-90ed1cd44ca0)

### Rental Car Menu
Allows customers to rent a car. Customers choose a car from the available list, enter rental and return dates, and can seek assistance if needed.
![image](https://github.com/PiotrHazior/ProjektSemestralnyPO/assets/86978938/35fc43a8-7fe0-4502-bdff-6e81754b61f5)

### Employee Menu
Employees can log in and manage the car fleet. New employees can register in the system.
![image](https://github.com/PiotrHazior/ProjektSemestralnyPO/assets/86978938/2f2741c2-a216-48f6-bf34-0ff4de711842)

### Employee Registration Menu
This window is used for employee registration. After registration, employees gain access to car management.
![image](https://github.com/PiotrHazior/ProjektSemestralnyPO/assets/86978938/fb3bb691-7360-45a8-b88e-e8f0cd0a1414)

### Manage Car Menu
This window enables employees to manage cars. Employees can add new cars or remove existing ones.
![image](https://github.com/PiotrHazior/ProjektSemestralnyPO/assets/86978938/757cd7bd-9b0b-4d6d-a0aa-ca2d4d809713)

## Database Tables
The CarRental application uses a database consisting of five tables:
-Customers - Stores customer data, such as customer ID, last name, password, and phone number.
-Employees - Contains employee data, including employee ID, last name, password, and phone number.
-SportCars - This table collects information about cars, such as car ID, brand, model, and rental price.
-RentalCar - Stores data related to car rentals, including rental ID, car ID, customer ID, and rental and return dates.
-Reservation - This table contains information about car reservations, such as reservation ID, car ID, customer ID, and rental and return dates.

## Note
This summary illustrates how the CarRental application works and outlines its main features. I encourage you to use our application and wish you successful car rentals! :)










Database:
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
