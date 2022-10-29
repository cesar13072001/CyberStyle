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
	p_nombre varchar(100) not null,
	p_descripcion varchar(100) not null,
	p_categoria int not null,
	p_image image not null,
	stock int not null,
	estado varchar(10) check(estado in('Activo','Eliminado')),
	p_masvend int default 0 check(p_masvend in(1,0))
	foreign key (p_categoria) references Categoria(idcategoria)
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
(2,'Cesar','Aguilar Zambrano','71296307','cesar@gmail.com','1234','')
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

create proc p_productoMasVendido02
as
begin
	select idproducto,p_nombre,p_descripcion,p_image from Producto where  estado = 'Activo' and stock > 0 and p_masvend = 1 
end
go



create proc p_registrarProducto
@nombre varchar(100),
@descripcion varchar(100),
@Categoria int,
@imagen image,
@stock int
as
begin
	insert into Productos(p_nombre,p_descripcion,p_categoria,p_image,stock,estado)
	values(@nombre,@descripcion,@Categoria,@imagen,@stock,'Activo')
end
go
