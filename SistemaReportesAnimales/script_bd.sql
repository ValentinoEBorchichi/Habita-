CREATE DATABASE ControlAnimalesDB;
GO

USE ControlAnimalesDB;
GO

CREATE TABLE Usuarios (
    Id INT PRIMARY KEY IDENTITY(1,1),
    NombreCompleto NVARCHAR(100) NOT NULL,
    Telefono NVARCHAR(50)
);

CREATE TABLE Autoridades (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Cargo NVARCHAR(50),
    TelefonoContacto NVARCHAR(50)
);

CREATE TABLE Reportes (
    Id INT PRIMARY KEY IDENTITY(1,1),
    FechaReporte DATETIME NOT NULL,
    Ubicacion NVARCHAR(255) NOT NULL,
    UsuarioId INT FOREIGN KEY REFERENCES Usuarios(Id),
    AutoridadId INT FOREIGN KEY REFERENCES Autoridades(Id)
);

CREATE TABLE Animales (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Especie NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(255),
    ReporteId INT FOREIGN KEY REFERENCES Reportes(Id)
);

-- Insertar datos de prueba
INSERT INTO Usuarios (NombreCompleto, Telefono) VALUES ('Juan Perez', '11-1234-5678');
INSERT INTO Usuarios (NombreCompleto, Telefono) VALUES ('Maria Gomez', '11-8765-4321');

INSERT INTO Autoridades (Nombre, Cargo, TelefonoContacto) VALUES ('Oficial Rodriguez', 'Control Animal', '103');
INSERT INTO Autoridades (Nombre, Cargo, TelefonoContacto) VALUES ('Inspectora Silva', 'Zoonosis', '105');

INSERT INTO Reportes (FechaReporte, Ubicacion, UsuarioId, AutoridadId) VALUES (GETDATE(), 'Ruta Nacional 3, Km 35', 1, 1);
INSERT INTO Reportes (FechaReporte, Ubicacion, UsuarioId, AutoridadId) VALUES (GETDATE(), 'Plaza Central, San Justo', 2, 2);

INSERT INTO Animales (Especie, Descripcion, ReporteId) VALUES ('Caballo', 'Color marrón, sin marcas, suelto en la ruta', 1);
INSERT INTO Animales (Especie, Descripcion, ReporteId) VALUES ('Vaca', 'Raza Holando, suelta en el parque', 1);
INSERT INTO Animales (Especie, Descripcion, ReporteId) VALUES ('Perro', 'Cruza, color negro, tamaño mediano', 2);
