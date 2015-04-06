CREATE TABLE [dbo].[Employees] (
    [EmployeeID]          INT            IDENTITY (1, 1) NOT NULL,
    [firstName]           NVARCHAR (MAX) NULL,
    [lastName]            NVARCHAR (MAX) NULL,
    [jobTitle]            NVARCHAR (MAX) NULL,
    [birthDate]           DATETIME       NOT NULL,
    [hireDate]            DATETIME       NOT NULL,
    [daysFirstCall]       INT            NOT NULL,
    [daysSecondCall]      INT            NOT NULL,
    [Email]               NVARCHAR (MAX) NULL,
    [Employee_EmployeeID] INT            NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_Employee_EmployeeID]
    ON [dbo].[Employees]([Employee_EmployeeID] ASC);

