CREATE TABLE [dbo].[Usuarios] (
    [email]    NVARCHAR (50) NOT NULL,
    [nif]      NVARCHAR (16) NOT NULL,
    [nombre]   NVARCHAR (32) NOT NULL,
    [apellido] NVARCHAR (64) NOT NULL,
    [contraseña]    NVARCHAR (32) NOT NULL,
    [telefono] NVARCHAR (15) NOT NULL,
	[esAdmin] NVARCHAR (15) NOT NULL,

    CONSTRAINT [pk_usuarios] PRIMARY KEY CLUSTERED ([email] ASC)
);

