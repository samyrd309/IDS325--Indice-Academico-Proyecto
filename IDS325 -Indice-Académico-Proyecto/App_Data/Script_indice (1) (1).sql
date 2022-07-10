CREATE DATABASE Indice
GO

USE Indice
GO

CREATE TABLE [Rol] (
  [IdRol] int identity(1,1),
  [DescripcionRol] varchar(20) NOT NULL,
  [FechaIngresoRol] Datetime DEFAULT GETDATE(),
  [VigenciaRol] bit DEFAULT 1,
  PRIMARY KEY ([IdRol])
);


insert into Rol(DescripcionRol) values ('Administridor')
insert into Rol(DescripcionRol) values ('Estudiante')
insert into Rol(DescripcionRol) values ('Docente')

select *from Rol

CREATE TABLE [Carrera] (
  [CodigoCarrera] char(3),
  [NombreCarrera] varchar(150) NOT NULL,
  [FechaIngresoCarrera] Datetime DEFAULT GETDATE(),
  [VigenciaCarrera] bit DEFAULT 1,
  PRIMARY KEY ([CodigoCarrera])
);


insert into Carrera(CodigoCarrera, NombreCarrera) values ('IDS','Carrera1')
insert into Carrera(CodigoCarrera, NombreCarrera)values ('ICS','Carrera2')
insert into Carrera(CodigoCarrera, NombreCarrera) values ('ING','Carrera3')

CREATE TABLE [AreaAcademica] (
  [CodigoArea] char(2) PRIMARY KEY,
  [NombreArea] varchar(150) NOT NULL,
  [FechaIngresoArea] Datetime DEFAULT GETDATE(),
  [VigenciaArea] bit DEFAULT 1
);

select *from pers

insert into AreaAcademica(CodigoArea, NombreArea) values ('IN', 'Area1')
insert into AreaAcademica(CodigoArea, NombreArea) values ('NG', 'Area2')
insert into AreaAcademica(CodigoArea, NombreArea) values ('SH', 'Area3')
insert into AreaAcademica(CodigoArea, NombreArea) values ('SA', 'Area4')

select *from Persona

CREATE TABLE [Persona] (
  [Matricula] int identity(1,1),
  [IdRol] int NOT NULL,
  [Carrera] char(3),
  [CodigoArea] char(2),
  [Nombre] varchar(50) NOT NULL,
  [Apellido] varchar(50) NOT NULL,
  [CorreoElectronico] varchar(320) NOT NULL,
  [FechaIngresoPersona] Datetime DEFAULT GETDATE(),
  [VigenciaPersona] bit DEFAULT 1,
  [Indice] decimal(3,2) default 0,
  [Contraseña] varchar(16),
  PRIMARY KEY ([Matricula]),
  CONSTRAINT [FK_Persona.IdRol]
    FOREIGN KEY ([IdRol])
      REFERENCES [Rol]([IdRol]),
  CONSTRAINT [FK_Persona.Carrera]
    FOREIGN KEY ([Carrera])
      REFERENCES [Carrera]([CodigoCarrera]),
  CONSTRAINT [FK_Persona.CodigoArea]
    FOREIGN KEY ([CodigoArea])
      REFERENCES [AreaAcademica]([CodigoArea])
);

insert into Persona(IdRol, Carrera, CodigoArea, Nombre, Apellido, CorreoElectronico, Contraseña,Indice) values (2, 'IDS', 'IN', 'Samuel', 'Charles', 'ejemplo@gmail.com', '123', null)
insert into Persona(IdRol, Carrera, CodigoArea, Nombre, Apellido, CorreoElectronico, Contraseña,Indice) values (1, 'IDS', 'IN', 'Samuel', 'Charles', 'ejemplo@gmail.com', '123', null)
insert into Persona(IdRol, Carrera, CodigoArea, Nombre, Apellido, CorreoElectronico, Contraseña,Indice) values (3, 'IDS', 'IN', 'Samuel', 'Charles', 'ejemplo@gmail.com', '123', null)

create proc sp_ValidarUsuario (
@Matricula int,
@Contraseña varchar(16)
)
as 
begin
	if (exists(select *from Persona where Matricula = @Matricula and Contraseña = @Contraseña))
		select Matricula, IdRol, Carrera, CodigoArea, Nombre, Apellido, CorreoElectronico, Indice from Persona where Matricula = @Matricula and Contraseña = @Contraseña
	else
		select '0'
end
go 

sp_ValidarUsuario 1, '123'

CREATE PROC  sp_ListarEstudiantes
as
	select Matricula, Carrera, CodigoArea, Nombre, Apellido, CorreoElectronico from Persona where IdRol =  2 and VigenciaPersona = 1
go


CREATE PROC  sp_ListarAsignaturas
as
	select CodigoAsignatura, NombreAsignatura, CodigoCarrera,CodigoArea, Credito  from Asignatura where VigenciaAsignatura = 1
go

CREATE PROC  sp_ListarSeccion
as
	select IdSeccion, CodigoAsignatura, s.Matricula  from Seccion s
	inner join Persona p
	on p.Matricula = s.Matricula where p.IdRol = 3 and VigenciaSección = 1
go


select *from Persona where IdRol = 3

CREATE PROC  sp_ObtenerEstudiantes
@Matricula int
as
	select Matricula, Carrera, CodigoArea, Nombre, Apellido, CorreoElectronico from Persona where IdRol =  2 and Matricula = @Matricula and VigenciaPersona = 1
go

CREATE PROC  sp_ListarDocentes
as
	select Matricula, Carrera, CodigoArea, Nombre, Apellido, CorreoElectronico from Persona where IdRol =  3 and VigenciaPersona = 1
go

CREATE PROC  sp_ObtenerDocentes
@Matricula int
as
	select Matricula, Carrera, CodigoArea, Nombre, Apellido, CorreoElectronico from Persona where IdRol =  3 and Matricula = @Matricula and VigenciaPersona = 1
go




CREATE PROC sp_GuardarPersona
@IdRol int,
@Carrera char(3),
@CodigoArea char(2),
@Nombre varchar(50) ,
@Apellido varchar(50),
@CorreoElectronico varchar(320),
@Contraseña varchar(16)
as
begin
	declare @Indice decimal(3,2)
	set @Indice = 0
	insert into Persona(IdRol, Carrera, CodigoArea, Nombre, Apellido, CorreoElectronico, Contraseña, Indice) values (@IdRol, @Carrera, @CodigoArea, @Nombre, @Apellido, @CorreoElectronico, @Contraseña,@Indice)
end 
go

exec sp_GuardarPersona 2,'IDS','IN', 'Carlos', 'Brito', 'ejemplo@gmail.com', '123456'

select *from Persona


create PROC sp_EditarPersona
@Matricula  int,
@Carrera char(3),
@CodigoArea char(2),
@CorreoElectronico varchar(320),
@Contraseña varchar(16)
as
begin
	update Persona set Carrera = Carrera, CodigoArea = @CodigoArea, CorreoElectronico = @CorreoElectronico, Contraseña = @Contraseña where Matricula = @Matricula
end 
go

--exec sp_EditarPersona 4, 'IDS','IN', 'ejemplo4@gmail.com', '123456'

select *from Persona

CREATE PROC sp_EliminarPersona
@Matricula int 
as 
begin
	update Persona set VigenciaPersona = 0 where Matricula = @Matricula
end 
go 



--exec sp_EliminarPersona 4









select *from Persona


CREATE TABLE [Literal] (
  [Nota] nvarchar(2),
  [Numero] decimal(2,1) NULL,
  PRIMARY KEY ([Nota])
);

insert into Literal(Nota, Numero) values ('A', 4.0)
insert into Literal(Nota, Numero) values ('B+', 3.5)
insert into Literal(Nota, Numero) values ('B', 3.0)
insert into Literal(Nota, Numero) values ('C+', 2.5)
insert into Literal(Nota, Numero) values ('C', 2.0)
insert into Literal(Nota, Numero) values ('D', 1.0)
insert into Literal(Nota, Numero) values ('F', 0)
insert into Literal(Nota, Numero) values ('R', NULL)

select *from Literal

CREATE TABLE [Asignatura] (
  [CodigoAsignatura] nvarchar(7),
  [CodigoCarrera] char(3) NOT NULL,
  [CodigoArea] char(2) NOT NULL,
  [Credito] int NOT NULL,
  [NombreAsignatura] varchar(50) NOT NULL,
  [FechaIngresoAsignatura] datetime DEFAULT GETDATE(),
  [VigenciaAsignatura] bit DEFAULT 1,
  PRIMARY KEY ([CodigoAsignatura]),
  CONSTRAINT [FK_Asignatura.CodigoCarrera]
    FOREIGN KEY ([CodigoCarrera])
      REFERENCES [Carrera]([CodigoCarrera]),
  CONSTRAINT [FK_Asignatura.CodigoArea]
    FOREIGN KEY ([CodigoArea])
      REFERENCES [AreaAcademica]([CodigoArea])
);

insert into Asignatura(CodigoAsignatura, CodigoCarrera, CodigoArea, Credito, NombreAsignatura) values ('IDS325', 'IDS', 'IN', 4, 'Asignatura 1')
insert into Asignatura(CodigoAsignatura, CodigoCarrera, CodigoArea, Credito, NombreAsignatura) values ('IDS322', 'IDS', 'IN', 3, 'Asignatura 2')
insert into Asignatura(CodigoAsignatura, CodigoCarrera, CodigoArea, Credito, NombreAsignatura) values ('IDS329', 'IDS', 'IN', 1, 'Asignatura 3')

select *from Asignatura

drop TABLE [Seccion] (
  [IdSeccion] int, --Crear función de asignación de sección y revisar porque puede que no funcione
  [Matricula] int,
  [CodigoAsignatura] nvarchar(7),
  [FechaIngresoSección] Datetime DEFAULT GETDATE(),
  [VigenciaSección] bit DEFAULT 1,
  PRIMARY KEY ([IdSeccion], [CodigoAsignatura]),
  CONSTRAINT [FK_Sección.Matricula]
    FOREIGN KEY ([Matricula])
      REFERENCES [Persona]([Matricula]),
  CONSTRAINT [FK_Sección.CodigoAsignatura]
    FOREIGN KEY ([CodigoAsignatura])
      REFERENCES [Asignatura]([CodigoAsignatura])
);

CREATE TABLE [Seccion] (
  [CodigoAsignatura] nvarchar(7),
  [IdSeccion] int identity(1,1),
  [Matricula] int,
  [FechaIngresoSeccion] Datetime DEFAULT GETDATE(),
  [VigenciaSección] bit DEFAULT 1,
  [NumeroSección] int,
  PRIMARY KEY ([IdSeccion]),
  CONSTRAINT [FK_Seccion.Matricula]
    FOREIGN KEY ([Matricula])
      REFERENCES [Persona]([Matricula]),
  CONSTRAINT [FK_Seccion.CodigoAsignatura]
    FOREIGN KEY ([CodigoAsignatura])
      REFERENCES [Asignatura]([CodigoAsignatura])
);


insert into Seccion(NumeroSección, CodigoAsignatura) values (1,'IDS325')
insert into Seccion(NumeroSección, CodigoAsignatura) values (2, 'IDS325')
insert into Seccion(NumeroSección, CodigoAsignatura) values (3, 'IDS325')

insert into Seccion(NumeroSección, CodigoAsignatura) values (1, 'IDS322')
insert into Seccion(NumeroSección, CodigoAsignatura) values (2, 'IDS322')
insert into Seccion(NumeroSección, CodigoAsignatura) values (3, 'IDS322')

CREATE FUNCTION TrimestreAct()
RETURNS char(7) AS
BEGIN
	DECLARE @trimestre char(7)
	DECLARE @año int = (SELECT CAST(YEAR(GETDATE()) as int))
	IF (SELECT CAST(MONTH(GETDATE()) as int)) = 1
		SET @año -= 1
	DECLARE @periodo int
	SET @periodo = (SELECT CASE
							WHEN (SELECT CAST(MONTH(GETDATE()) as int)) >= 2 AND (SELECT CAST(MONTH(GETDATE()) as int)) <= 4 
								THEN '01'
							WHEN (SELECT CAST(MONTH(GETDATE()) as int)) >= 5 AND (SELECT CAST(MONTH(GETDATE()) as int)) <= 7 
								THEN '02'
							WHEN (SELECT CAST(MONTH(GETDATE()) as int)) >= 8 AND (SELECT CAST(MONTH(GETDATE()) as int)) <= 10 
								THEN '03'
							WHEN (SELECT CAST(MONTH(GETDATE()) as int)) = 11 OR (SELECT CAST(MONTH(GETDATE()) as int)) = 12 OR (SELECT CAST(MONTH(GETDATE()) as int)) = 1 
								THEN '04'
							END) 
	SET @trimestre = (SELECT CONCAT(CONVERT(varchar(4), @año), '-', CONVERT(varchar(1), @periodo)))
RETURN @trimestre
END
GO

cre TABLE [Calificacion] (
  [Matricula] int,
  [CodigoAsignatura] nvarchar(7),
  [Nota] nvarchar(2),
  [IdSeccion] int,
  [Trimestre] char(7) DEFAULT dbo.TrimestreAct(), -- Hay que ejecutar la función para quitar el error
  [FechaIngresoCalificacion] Datetime  DEFAULT GETDATE(),
  [VigenciaCalificacion] bit  DEFAULT 1
  PRIMARY KEY ([Matricula], [CodigoAsignatura], [Trimestre]),
  CONSTRAINT [FK_Calificacion.Matricula]
    FOREIGN KEY ([Matricula])
      REFERENCES [Persona]([Matricula]),
  CONSTRAINT [FK_Calificacion.Nota]
    FOREIGN KEY ([Nota])
      REFERENCES [Literal]([Nota]),
  CONSTRAINT [FK_Calificacion.IdSeccion]
    FOREIGN KEY ([IdSeccion], [CodigoAsignatura])
      REFERENCES [Seccion]([IdSeccion], [CodigoAsignatura])
);	


CREATE TABLE [Calificacion] (
  [Matricula] int,
  [CodigoAsignatura] nvarchar(7),
 [Trimestre] char(7) DEFAULT dbo.TrimestreAct(),
  [Nota] nvarchar(2),
  [IdSeccion] int,
  [FechaIngresoCalificacion] Datetime  DEFAULT GETDATE(),
  [VigenciaCalificacion] bit  DEFAULT 1,
  PRIMARY KEY ([Matricula], [CodigoAsignatura]),
  CONSTRAINT [FK_Calificacion.Matricula]
    FOREIGN KEY ([Matricula])
      REFERENCES [Persona]([Matricula]),
   CONSTRAINT [FK_Calificacion.IdSeccion]
    FOREIGN KEY ([IdSeccion])
      REFERENCES [Seccion]([IdSeccion])
);


select *from Seccion

insert into Calificacion(Matricula, CodigoAsignatura, Nota, IdSeccion) values (1, 'IDS325', 'A', 1)


select *from Calificacion






CREATE PROCEDURE ppUppsertEstudiante
@Matricula int,
@IdRol int,
@Carrera char(3),
@Nombre varchar(50),
@Apellido varchar(50),
@CorreoElectronico varchar(320)
AS
BEGIN TRANSACTION
	BEGIN TRY
		IF not exists (SELECT TOP 1 Matricula FROM Persona WHERE Matricula = @Matricula)
			INSERT INTO Persona(IdRol, Carrera, Nombre, Apellido, CorreoElectronico)
					VALUES(@IdRol, @Carrera, @Nombre, @Apellido, @CorreoElectronico)
		ELSE
			UPDATE Persona SET IdRol = @IdRol, Carrera = @Carrera, Nombre = @Nombre, Apellido = @Apellido, CorreoElectronico = @CorreoElectronico
			WHERE Matricula = @Matricula
		COMMIT
	END TRY
	BEGIN CATCH
		ROLLBACK
	END CATCH

GO


CREATE PROCEDURE ppEliminarPersona
@Matricula int
AS
UPDATE PERSONA SET VigenciaPersona = 0 WHERE Matricula = @Matricula

GO

CREATE PROCEDURE ppUppsertAsignaturas
@CodigoAsignatura nvarchar(7),
@CodigoCarrera char(3),
@Credito int,
@NombreAsignatura varchar(50)
AS
BEGIN TRANSACTION
	BEGIN TRY
		IF not exists (SELECT TOP 1 CodigoAsignatura FROM Asignatura)
			INSERT INTO Asignatura(CodigoAsignatura, CodigoCarrera, Credito, NombreAsignatura)
							VALUES(@CodigoAsignatura, @CodigoCarrera, @Credito, @NombreAsignatura)
		ELSE
			UPDATE Asignatura SET CodigoAsignatura = @CodigoAsignatura, CodigoCarrera = @CodigoCarrera, Credito = @Credito, NombreAsignatura = @NombreAsignatura
			WHERE CodigoAsignatura = @CodigoAsignatura
		COMMIT
	END TRY
	BEGIN CATCH
		ROLLBACK
	END CATCH

GO

CREATE PROCEDURE ppEliminarAsignatura
@CodigoAsignatura nvarchar(7)
AS
UPDATE Asignatura SET VigenciaAsignatura = 0 WHERE CodigoAsignatura = @CodigoAsignatura

GO

CREATE PROCEDURE ppUpsertCalificacion
@Matricula int,
@CodigoAsignatura nvarchar(7),
@Nota nvarchar(2)
AS
BEGIN TRANSACTION
	BEGIN TRY
		IF not exists (SELECT Nota FROM Calificacion WHERE Matricula = @Matricula AND CodigoAsignatura = @CodigoAsignatura)
			INSERT INTO Calificacion(Matricula, CodigoAsignatura, Nota) VALUES (@Matricula, @CodigoAsignatura, @Nota)
		ELSE 
			UPDATE Calificacion SET Nota = @Nota WHERE Matricula = @Matricula AND CodigoAsignatura = @CodigoAsignatura
	END TRY
	BEGIN CATCH
		ROLLBACK
	END CATCH

GO

CREATE PROCEDURE ppEliminarCalificacion
@Matricula int, 
@CodigoAsignatura nvarchar(7)
AS
UPDATE Calificacion SET VigenciaCalificacion = 0 WHERE Matricula = @Matricula AND CodigoAsignatura = @CodigoAsignatura

GO


-- Hay que probar esto
CREATE FUNCTION CalcularIndice (@Matricula int)
RETURNS decimal(3,2) AS
BEGIN
	DECLARE @indice decimal(3,2)
	SET @indice = (SELECT SUM(l.Numero) / SUM(a.Credito) FROM Calificacion c 
					INNER JOIN Literal l ON l.Nota = c.Nota
					INNER JOIN Asignatura a ON c.CodigoAsignatura = a.CodigoAsignatura
					WHERE c.Matricula = @Matricula AND c.NOTA <> 'R')
RETURN @indice
END

GO


CREATE PROCEDURE CambiarContraseña 
@matricula int,
@contraseña varchar(16)
AS
BEGIN
	IF (SELECT VigenciaPersona FROM Persona WHERE Matricula = @matricula) = 1
		UPDATE Persona SET Contraseña = @contraseña WHERE Matricula = @matricula
END

GO

CREATE PROCEDURE AsignarCalificación -- Modificar SP con IF anidado
@matricula int,
@calificacion varchar(2),
@codigo varchar(7)
AS
BEGIN
	IF (SELECT VigenciaPersona FROM Persona WHERE Matricula = @matricula) = 1
		BEGIN
			IF (SELECT VigenciaCalificacion FROM Calificacion WHERE Matricula = @matricula AND CodigoAsignatura = @codigo) = 1
				BEGIN
					UPDATE Calificacion SET Nota = @calificacion WHERE Matricula = @matricula and CodigoAsignatura = @codigo
					UPDATE Persona SET Indice = (SELECT dbo.Indice(@matricula)) WHERE Matricula = @matricula
				END
		END
END

GO

CREATE PROCEDURE MostrarCalificaciones
@matricula int
AS
BEGIN
	SELECT 
		c.CodigoAsignatura,
		a.NombreAsignatura,
		a.Credito,
		c.Nota
	FROM Calificacion c
	INNER JOIN Asignatura a
	ON a.CodigoAsignatura = c.CodigoAsignatura
	WHERE c.Matricula = @matricula
END

GO

CREATE PROCEDURE MostrarRanking
AS
BEGIN
	SELECT 
		Matricula,
		Carrera,
		Indice
	FROM Persona
END

GO

CREATE PROCEDURE AsignarEstudiantes
@matricula int,
@asignatura varchar(7),
@seccion int
AS
BEGIN TRANSACTION
	BEGIN TRY
		INSERT INTO Seccion(CodigoAsignatura, Matricula, IdSeccion) VALUES (@asignatura, @matricula, @seccion)
		COMMIT
	END TRY
	BEGIN CATCH
		ROLLBACK
	END CATCH

GO

create PROCEDURE CalcularIndice 
@Matricula int
AS
SELECT 
	(SELECT dbo.Indice(@Matricula)) as 'Indice', 
	SUM(a.Credito) as 'TotalCreditos',
	(SELECT dbo.Merito(@Matricula)) as 'Meritos'
FROM Calificacion c
INNER JOIN Asignatura a
ON a.CodigoAsignatura = c.CodigoAsignatura and c.Matricula = @Matricula
INNER JOIN Literal l
ON c.Nota = l.Nota AND c.Nota <> 'R'

GO

CREATE FUNCTION Merito(@Matricula int)
RETURNS varchar(30)
AS
BEGIN
	DECLARE @Merito varchar(30)
	DECLARE @Indice decimal(3,2) = (SELECT dbo.Indice(@Matricula))
	SET @Merito = (SELECT CASE 
								WHEN @Indice >= 3.80 THEN 'Summa Cum Laude'
								WHEN @Indice >= 3.60 THEN 'Magna Cum Laude'
								WHEN @Indice >= 3.40 THEN 'Cum Laude'
								ELSE 'Sin Mérito'
							END)
	RETURN @Merito
END

GO

CREATE FUNCTION Indice(@Matricula int)
RETURNS decimal(3,2)
AS
BEGIN
	DECLARE @Indice decimal(3,2) = (SELECT 
		(SUM(l.Numero * a.Credito) / (SUM(a.Credito))) as 'Indice'
	FROM Calificacion c
	INNER JOIN Asignatura a
	ON a.CodigoAsignatura = c.CodigoAsignatura and c.Matricula = @Matricula
	INNER JOIN Literal l
	ON c.Nota = l.Nota AND c.Nota <> 'R')
	RETURN @Indice
END

GO

CREATE PROCEDURE ModificarIndice
@matricula int
AS
BEGIN TRANSACTION
	BEGIN TRY
		IF (SELECT VigenciaPersona FROM Persona WHERE Matricula = @matricula) = 1
			UPDATE Persona SET Indice = (SELECT dbo.Indice(@matricula)) WHERE Matricula = @matricula 
		COMMIT
	END TRY
	BEGIN CATCH
		ROLLBACK
	END CATCH

GO

CREATE PROCEDURE ListadoSeccion
@asignatura varchar(7),
@seccion int
AS
BEGIN
	SELECT 
		c.Matricula as 'Matricula',
		CONCAT(p.Nombre, ' ', p.Apellido) as 'Nombre',
		c.Nota as 'Calificacion'
	FROM Calificacion c
	INNER JOIN Persona p
	ON c.Matricula = p.Matricula
	WHERE CodigoAsignatura = @asignatura AND IdSeccion = @seccion
END

select *from Persona
