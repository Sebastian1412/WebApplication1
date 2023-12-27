create database UsuarioPragma
go

use UsuarioPragma
go

CREATE TABLE Usuarios (
    Id INT PRIMARY KEY,
    Nombre VARCHAR(50),
    Rut VARCHAR(20) UNIQUE,
    Correo VARCHAR(100) NULL,
    FechaNacimiento DATE
);

select *from Usuarios

-- Insert 1
INSERT INTO Usuarios (Id, Nombre, Rut, Correo, FechaNacimiento)
VALUES (1, 'Alonso Mallea', '112223374', 'Alonso.Mallea@Pragma.com', '1998-05-15');

-- Insert 2
INSERT INTO Usuarios (Id, Nombre, Rut, Correo, FechaNacimiento)
VALUES (2, 'Sebastian Mella', '1127773334', 'Sebastian.Mella@Pragma.com', '1990-06-25');

-- Insert 3
INSERT INTO Usuarios (Id, Nombre, Rut, Correo, FechaNacimiento)
VALUES (3, 'Allen Walker', '116547334', 'Allen.Walker@Pragma.com', '1991-05-25');

-- Insert 4
INSERT INTO Usuarios (Id, Nombre, Rut, Correo, FechaNacimiento)
VALUES (4, 'Luisa Rodriguez', '445566778', 'Luisa.Rodriguez@Pragma.cl', '1985-09-22');

-- Insert 5
INSERT INTO Usuarios (Id, Nombre, Rut, Correo, FechaNacimiento)
VALUES (5, 'Pedro Martínez', '889900011', 'Pedro.Martinez@Pragma.cl', '1993-07-14');

-- Insert 6
INSERT INTO Usuarios (Id, Nombre, Rut, Correo, FechaNacimiento)
VALUES (6, 'María González', '223344556', 'Maria.Gonzalez@Pragma.cl', '1988-12-30');

-- Insert 7
INSERT INTO Usuarios (Id, Nombre, Rut, Correo, FechaNacimiento)
VALUES (7, 'Javier Soto', '667788899', 'Javier.Soto@Pragma.cl', '1997-03-18');

-- Insert 8
INSERT INTO Usuarios (Id, Nombre, Rut, Correo, FechaNacimiento)
VALUES (8, 'Laura Morales', '112233445', 'Laura.Morales@Pragma.cl', '1991-11-05');

-- Insert 9
INSERT INTO Usuarios (Id, Nombre, Rut, Correo, FechaNacimiento)
VALUES (9, 'Carlos Ruiz', '556677889', 'Carlos.Ruiz@Pragma.cl', '1984-02-28');

-- Insert 10
INSERT INTO Usuarios (Id, Nombre, Rut, Correo, FechaNacimiento)
VALUES (10, 'Carmen Torres', '990011223', 'Carmen.Torres@Pragma.cl', '1995-06-10');

-- Insert 11
INSERT INTO Usuarios (Id, Nombre, Rut, Correo, FechaNacimiento)
VALUES (11, 'Ricardo Silva', '334455667', 'Ricardo.Silva@Pragma.cl', '1987-04-25');

-- Insert 12
INSERT INTO Usuarios (Id, Nombre, Rut, Correo, FechaNacimiento)
VALUES (12, 'Valeria Pérez', '778899001', 'Valeria.Perez@Pragma.cl', '1999-08-12');

-- Insert 13
INSERT INTO Usuarios (Id, Nombre, Rut, Correo, FechaNacimiento)
VALUES (13, 'Gabriel Mendoza', '112233445', 'Gabriel.Mendoza@Pragma.cl', '1992-10-03');

-- Insert 14
INSERT INTO Usuarios (Id, Nombre, Rut, Correo, FechaNacimiento)
VALUES (14, 'Paola Rojas', '556677889', 'Paola.Rojas@Pragma.cl', '1986-01-15');

-- Insert 15
INSERT INTO Usuarios (Id, Nombre, Rut, Correo, FechaNacimiento)
VALUES (15, 'Andrés Castro', '334455667', 'Andres.Castro@Pragma.cl', '1994-09-08');


truncate table Usuarios;