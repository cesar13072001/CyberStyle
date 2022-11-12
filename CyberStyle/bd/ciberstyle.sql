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


insert into Categoria values
('Pantalones','Activo'),
('Chompas','Activo'),
('Zapatos','Activo'),
('Blusas','Activo'),
('Short','Activo')
go



create table Producto
(
	idproducto int identity(1,1) primary key,
	nombre varchar(100) not null,
	descripcion varchar(100) not null,
	precio decimal,
	idcategoria int not null,
	imagen image not null,
	stock int not null,
	estado varchar(10) check(estado in('Activo','Eliminado')),
	masvend int default 0 check(masvend in(1,0))
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



create table Reclamos
(
idreclamo varchar(50) primary key,
idpago int,
telefono varchar(20),
descripcion varchar(200),
estado varchar(15),
fecha varchar(10),
foreign key (idpago) references Pago(idpago)
)
go



/*PROCEDIMIENTOS ALMACENADOS*/
/*AGUILAR ZAMBRANO*/
insert into usuario values
(1,'Cesar','Aguilar Zambrano','71296307','cesar@gmail.com','1234','2001-07-13')
go

insert into usuario values
(2,'Fernando','Alvarez Delgado','123456789','fernando@gmail.com','1234','1997-01-01')
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

/***************************/

create proc p_productoMasVendido02
as
begin
	select idproducto,nombre,descripcion,precio,imagen from Producto where  estado = 'Activo' and stock > 0 and masvend = 1 
end
go



create proc p_registrarProducto
@nombre varchar(100),
@descripcion varchar(100),
@Categoria int,
@precio decimal,
@imagen image,
@stock int
as
begin
	insert into Productos(nombre,descripcion,categoria,precio,imagen,stock,estado)
	values(@nombre,@descripcion,@Categoria,@precio,@imagen,@stock,'Activo')
end
go


create proc p_productoGeneral
as
begin
	select idproducto,nombre,descripcion,precio,imagen from Producto where  estado = 'Activo' and stock > 0
end
go


create or ALTER proc mostrarUltimoPago
@idUsuario int
as
begin
	select TOP 1 * from Pago where idUsuario=@idUsuario
	order by idPago desc;
end
go



create or alter proc compradetalle
@idpago int
as
begin
  select pr.nombre, dp.cantidad, dp.subtotal, p.total 
  from Pago p inner join DetallePago dp
  on p.idpago = dp.idpago
  inner join Producto pr 
  on dp.idproducto = pr.idproducto
  where p.idpago = @idpago
end
go








create or alter proc listar_fecha
@fecha varchar(10)
as
begin
select idpago, idusuario, fechaPago, total from Pago
where CONVERT(VARCHAR(25), fechaPago, 126) like @fecha +'%' 
end
go

