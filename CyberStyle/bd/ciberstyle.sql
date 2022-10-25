use master
go

create database ciberstyle
go

use ciberstyle
go

Create table Rol 
( 
idrol int primary key identity(1,1),
nombrerol varchar(100)
) 
go 

insert into Rol values('Administrador')--1
insert into Rol values('Usuario')--2
go

create table Usuario
(
idusuario int identity(1,1) primary key,
idrol int,
nombres varchar(100),
apellidos varchar(100),
dni char(8),
correo varchar(100),
contrasenia varchar(100),
fechaRegistro datetime,
foreign key (idrol) references Rol(idrol)
)

create table Categoria
(
	idcategoria int identity(1,1) primary key,
	nomcategoria varchar(100) not null,
	estado char(9) not null
)
go


create table Producto
(
idproducto int identity(1,1) primary key,
idcategoria int,
nombreproducto varchar(100),
precio money,
stock int,
foto image,
foreign key (idcategoria) references Categoria(idcategoria)
)



create table Pago
(
idpago int primary key identity(100,1),
idusuario int,
fechaPago datetime,
nomTarjeta varchar(100),
numTarjeta varchar(16),
annio char(4),
mes char(2),
cvv char(4),
total decimal(9,2)
foreign key (idusuario) references Usuario(idusuario)
)
go

create table DetallePago
(
idpago int,
idproducto int,
cantidad int,
subtotal decimal(9,2),
foreign key (idpago) references Pago(idpago),
foreign key (idproducto) references Producto(idproducto),
constraint pkdetalle Primary key(idpago, idproducto)
)
go


/*create table Recuperar
(
idrecuperar int primary key,
idusuario int,
fechacreacion datetime,
fechaexpiracion datetime,
estado varchar(50),
foreign key (idusuario) references Usuario(idusuario)
)
go*/



/*PROCEDIMIENTOS ALMACENADOS*/
/*AGUILAR ZAMBRANO*/
insert into usuario values
(2,'Cesar','Aguilar Zambrano','71296307','cesar@gmail.com','1234','2001-07-13')
go

create or alter proc login_usuario
@correo varchar(100),
@contrasenia varchar(100)
as
begin
select idusuario, idrol, nombres, apellidos, dni, correo,
fechaRegistro from usuario
where correo = @correo and contrasenia = @contrasenia
end
go

exec login_usuario 'cesar@gmail.com', '1234'
go
/***************************/



