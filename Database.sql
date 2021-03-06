USE [TestApp]
GO
/****** Object:  User [TestApp]    Script Date: 22/02/2022 06:36:59 p. m. ******/
CREATE USER [TestApp] FOR LOGIN [TestApp] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_accessadmin] ADD MEMBER [TestApp]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 22/02/2022 06:36:59 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Username] [nvarchar](20) NOT NULL,
	[Password] [nvarchar](20) NOT NULL,
	[Genre] [varchar](50) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [Email], [Username], [Password], [Genre], [CreationDate], [Status]) VALUES (1, N'jonathanjbm@outlook.es', N'Jonathan', N'Sm9uaWJlYmUxMjMk', N'Masculino', CAST(N'2022-02-21T16:35:31.163' AS DateTime), 1)
INSERT [dbo].[Users] ([Id], [Email], [Username], [Password], [Genre], [CreationDate], [Status]) VALUES (2, N'smydoljbm@outlook.com', N'TestUser', N'Sm9uaWJlYmUxMjMk', N'Masculino', CAST(N'2022-02-22T18:34:30.393' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
/****** Object:  StoredProcedure [dbo].[DeleteUser]    Script Date: 22/02/2022 06:36:59 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<JBM,Jonathan Gonzalez Jimenez>
-- Create date: <17 de febrero>
-- Description:	<Desactivar usuario>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteUser]
	@Id	INT
AS
BEGIN

UPDATE Users
SET [Status] = 0
WHERE [Id] = @Id

END
GO
/****** Object:  StoredProcedure [dbo].[GetAllUsers]    Script Date: 22/02/2022 06:36:59 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<JBM,Jonathan Gonzalez Jimenez>
-- Create date: <17 de febrero>
-- Description:	<Obtener todos los usuarios>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllUsers]
AS
BEGIN

SELECT * FROM Users WHERE Status = 1;

END
GO
/****** Object:  StoredProcedure [dbo].[GetExistingUser]    Script Date: 22/02/2022 06:36:59 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<JBM,Jonathan Gonzalez Jimenez>
-- Create date: <17 de febrero>
-- Description:	<Obtener un usuario existente>
-- =============================================
CREATE PROCEDURE [dbo].[GetExistingUser]
	@Username NVARCHAR(MAX),
	@Email NVARCHAR(MAX),
	@Genre NVARCHAR(MAX)
AS
BEGIN

SELECT * FROM Users 
WHERE [Username] = @Username
OR [Email] = @Email
AND [Status] = 1;

END
GO
/****** Object:  StoredProcedure [dbo].[GetUser]    Script Date: 22/02/2022 06:36:59 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<JBM,Jonathan Gonzalez Jimenez>
-- Create date: <17 de febrero>
-- Description:	<Obtener un usuario>
-- =============================================
CREATE PROCEDURE [dbo].[GetUser]
	@Id INT
AS
BEGIN

SELECT * FROM Users WHERE [Id] = @Id;

END
GO
/****** Object:  StoredProcedure [dbo].[GetUserByUsername]    Script Date: 22/02/2022 06:36:59 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<JBM,Jonathan Gonzalez Jimenez>
-- Create date: <17 de febrero>
-- Description:	<Obtener registro por nombre de usuario>
-- =============================================
CREATE PROCEDURE [dbo].[GetUserByUsername]
	@Username NVARCHAR(MAX)
AS
BEGIN

SELECT * FROM Users 
WHERE [Username] = @Username;

END
GO
/****** Object:  StoredProcedure [dbo].[InsertUser]    Script Date: 22/02/2022 06:36:59 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<JBM,Jonathan Gonzalez Jimenez>
-- Create date: <17 de febrero>
-- Description:	<Insertar usuario>
-- =============================================
CREATE PROCEDURE [dbo].[InsertUser]
	@Email NVARCHAR(MAX),
	@Username NVARCHAR(MAX),
	@Password NVARCHAR(MAX),
	@Genre NVARCHAR(MAX)
AS
BEGIN

INSERT INTO Users(
	[Email], 
	[Username], 
	[Password], 
	[Genre], 
	[CreationDate], 
	[Status])
VALUES (
	@Email,
	@Username,
	@Password,
	@Genre, 
	GETDATE(), 
	1);

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateUser]    Script Date: 22/02/2022 06:36:59 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<JBM,Jonathan Gonzalez Jimenez>
-- Create date: <17 de febrero>
-- Description:	<Actualizar usuario>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateUser]
	@Id	INT,
	@Email NVARCHAR(MAX),
	@Username NVARCHAR(MAX),
	@Password NVARCHAR(MAX),
	@Genre NVARCHAR(MAX)
AS
BEGIN

UPDATE Users
SET [Email] = @Email, 
	[Username] = @Username, 
	[Password] = @Password, 
	[Genre] = @Genre
WHERE [Id] = @Id

END
GO
