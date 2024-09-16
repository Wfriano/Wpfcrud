CREATE DATABASE Samtel;
GO

USE Samtel;
GO

-- Crear la tabla de Áreas
CREATE TABLE [dbo].[Areas](
    [IdArea] INT IDENTITY(1,1) NOT NULL,
    [NombreArea] VARCHAR(100) NOT NULL,
    CONSTRAINT [PK_Areas] PRIMARY KEY CLUSTERED ([IdArea] ASC)
);
GO

-- Crear la tabla de Usuarios
CREATE TABLE [dbo].[Usuarios](
    [IdUsuario] INT IDENTITY(1,1) NOT NULL,
    [Nombres] VARCHAR(100) NOT NULL,
    [Apellidos] VARCHAR(100) NOT NULL,
    [Cargo] VARCHAR(100) NOT NULL,
    [Correo] VARCHAR(100) NOT NULL,
    CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED ([IdUsuario] ASC)
);
GO

-- Crear la tabla intermedia AreaUsuarios
CREATE TABLE [dbo].[AreaUsuarios](
    [IdArea] INT NOT NULL,
    [IdUsuario] INT NOT NULL,
    CONSTRAINT [PK_AreaUsuarios] PRIMARY KEY CLUSTERED ([IdArea], [IdUsuario]),
    CONSTRAINT [UQ_Usuario_Area] UNIQUE ([IdUsuario]),
    CONSTRAINT [FK_AreaUsuarios_Area] FOREIGN KEY ([IdArea]) REFERENCES [dbo].[Areas]([IdArea]),
    CONSTRAINT [FK_AreaUsuarios_Usuario] FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuarios]([IdUsuario])
);
GO

INSERT INTO Areas (NombreArea)
VALUES ('Desarrollo de Software');

INSERT INTO Areas (NombreArea)
VALUES ('Ciencia de Datos');

INSERT INTO Areas (NombreArea)
VALUES ('Inteligencia Artificial');

INSERT INTO Areas (NombreArea)
VALUES ('Ciberseguridad');

INSERT INTO Areas (NombreArea)
VALUES ('Desarrollo Web');

INSERT INTO Areas (NombreArea)
VALUES ('Marketing Digital');

INSERT INTO Areas (NombreArea)
VALUES ('Blockchain');

INSERT INTO Areas (NombreArea)
VALUES ('Soporte Técnico');

INSERT INTO Areas (NombreArea)
VALUES ('Infraestructura Cloud');

INSERT INTO Areas (NombreArea)
VALUES ('Gestión de Productos');