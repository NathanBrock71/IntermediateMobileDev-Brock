USE [master]
GO
/****** Object:  Database [MoviesAPI]    Script Date: 5/8/2024 10:50:48 PM ******/
CREATE DATABASE [MoviesAPI]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MoviesAPI', FILENAME = N'/var/opt/mssql/data/MoviesAPI.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MoviesAPI_log', FILENAME = N'/var/opt/mssql/data/MoviesAPI.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [MoviesAPI] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MoviesAPI].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MoviesAPI] SET ANSI_NULL_DEFAULT ON 
GO
ALTER DATABASE [MoviesAPI] SET ANSI_NULLS ON 
GO
ALTER DATABASE [MoviesAPI] SET ANSI_PADDING ON 
GO
ALTER DATABASE [MoviesAPI] SET ANSI_WARNINGS ON 
GO
ALTER DATABASE [MoviesAPI] SET ARITHABORT ON 
GO
ALTER DATABASE [MoviesAPI] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MoviesAPI] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MoviesAPI] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MoviesAPI] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MoviesAPI] SET CURSOR_DEFAULT  LOCAL 
GO
ALTER DATABASE [MoviesAPI] SET CONCAT_NULL_YIELDS_NULL ON 
GO
ALTER DATABASE [MoviesAPI] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MoviesAPI] SET QUOTED_IDENTIFIER ON 
GO
ALTER DATABASE [MoviesAPI] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MoviesAPI] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MoviesAPI] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MoviesAPI] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MoviesAPI] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MoviesAPI] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MoviesAPI] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MoviesAPI] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MoviesAPI] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MoviesAPI] SET RECOVERY FULL 
GO
ALTER DATABASE [MoviesAPI] SET  MULTI_USER 
GO
ALTER DATABASE [MoviesAPI] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MoviesAPI] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MoviesAPI] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MoviesAPI] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MoviesAPI] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MoviesAPI] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'MoviesAPI', N'ON'
GO
ALTER DATABASE [MoviesAPI] SET QUERY_STORE = OFF
GO
USE [MoviesAPI]
GO
/****** Object:  Table [dbo].[Movies]    Script Date: 5/8/2024 10:50:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movies](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](1000) NOT NULL,
	[Director] [nvarchar](50) NOT NULL,
	[Rating] [nvarchar](6) NOT NULL,
	[ReleaseDate] [nvarchar](18) NOT NULL,
	[ReviewScore] [nvarchar](7) NOT NULL,
	[PosterUrl] [nvarchar](200) NULL
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [MoviesAPI] SET  READ_WRITE 
GO

-- Insert JSON movies
USE [MoviesAPI]
GO
INSERT INTO [dbo].[Movies] ([Id], [Title], [Description], [Director], [Rating], [ReleaseDate], [ReviewScore], [PosterUrl])
VALUES
    ('784f1d07-77f9-4d34-8677-da344dd5f8e5', 'Star Wars: Episode IV - A New Hope', 'Luke Skywalker joins forces with Princess Leia to battle the evil Empire.', 'George Lucas', 'PG', '1977-05-25', '8.6', 'https://m.media-amazon.com/images/I/612h-jwI+EL._AC_UF1000,1000_QL80_.jpg'),
    ('3248ce16-2a6e-4c9f-b1dd-41f2434933be', 'Star Wars: Episode V - The Empire Strikes Back', 'The Empire counterattacks the Rebel Alliance on the ice planet Hoth.', 'Irvin Kershner', 'PG', '1980-05-21', '8.7', 'https://m.media-amazon.com/images/I/711xW80aSGL._AC_UF1000,1000_QL80_.jpg'),
    ('30a93a64-3cb7-48f8-a449-fdeaac176bcf', 'Star Wars: Episode VI - Return of the Jedi', 'Luke Skywalker confronts Darth Vader and the Emperor.', 'Richard Marquand', 'PG', '1983-05-25', '8.3', 'https://m.media-amazon.com/images/I/617CD+sLC-L._AC_UF1000,1000_QL80_.jpg'),
    ('82353ede-2d20-478e-82b2-d09cb7293436', 'Star Wars: Episode I - The Phantom Menace', 'Young Anakin Skywalkers journey to becoming a Jedi.', 'George Lucas', 'PG', '1999-05-19', '6.5', 'https://m.media-amazon.com/images/M/MV5BYTRhNjcwNWQtMGJmMi00NmQyLWE2YzItODVmMTdjNWI0ZDA2XkEyXkFqcGdeQXVyNTAyODkwOQ@@._V1_.jpg'),
    ('06614dd1-645d-4613-a9f3-48a2e5e90f79', 'Star Wars: Episode II - Attack of the Clones', 'Obi-Wan Kenobi investigates a separatist conspiracy.', 'George Lucas', 'PG', '2002-05-16', '6.5', 'https://m.media-amazon.com/images/M/MV5BMDAzM2M0Y2UtZjRmZi00MzVlLTg4MjEtOTE3NzU5ZDVlMTU5XkEyXkFqcGdeQXVyNDUyOTg3Njg@._V1_.jpg'),
    ('e7c5d592-cec4-41ed-a8c7-0b25dd41de88', 'Star Wars: Episode III - Revenge of the Sith', 'Anakin Skywalkers transformation into Darth Vader.', 'George Lucas', 'PG-13', '2005-05-19', '7.5', 'https://m.media-amazon.com/images/I/61AjJzUvj5L._AC_UF894,1000_QL80_.jpg'),
    ('98ad4dc7-5b69-405c-acfd-78122960b518', 'Star Wars: Episode VII - The Force Awakens', 'A new generation of heroes and villains emerges.', 'J.J. Abrams', 'PG-13', '2015-12-18', '7.8', 'https://m.media-amazon.com/images/M/MV5BOTAzODEzNDAzMl5BMl5BanBnXkFtZTgwMDU1MTgzNzE@._V1_.jpg'),
    ('4a832153-6317-4049-b5c4-24e4e6d3a2dc', 'Star Wars: Episode VIII - The Last Jedi', 'The Resistance faces the First Order once more.', 'Rian Johnson', 'PG-13', '2017-12-15', '7.0', 'https://m.media-amazon.com/images/M/MV5BMjQ1MzcxNjg4N15BMl5BanBnXkFtZTgwNzgwMjY4MzI@._V1_.jpg'),
    ('49d2a65a-52e1-45a7-8cf3-63bfd4056fa0', 'Star Wars: Episode IX - The Rise of Skywalker', 'The final battle between the Jedi and the Sith.', 'J.J. Abrams', 'PG-13', '2019-12-20', '6.5', 'https://i0.wp.com/www.patspropaganda.com/wp-content/uploads/2020/07/Dumpster-fire-pic.jpg?resize=1024%2C1024&ssl=1'),
    ('46446934-eb32-403d-8913-5b9e87953448', 'Raiders of the Lost Ark', 'Indiana Jones hunts for the Ark of the Covenant.', 'Steven Spielberg', 'PG', '1981-06-12', '8.4', 'https://m.media-amazon.com/images/I/61H2YD-bubL._AC_UF894,1000_QL80_.jpg'),
    ('54ff7b30-ed26-4253-87ed-47cc7cc64cbe', 'Indiana Jones and the Temple of Doom', 'Indy seeks a mystical stone and faces a cult.', 'Steven Spielberg', 'PG', '1984-05-23', '7.6', 'https://m.media-amazon.com/images/I/61AbSnzGR+L._AC_UF1000,1000_QL80_.jpg'),
    ('112ccdb5-bb4a-4de0-ad74-688150390c22', 'Indiana Jones and the Last Crusade', 'Indiana Jones seeks the Holy Grail.', 'Steven Spielberg', 'PG-13', '1989-05-24', '8.2', 'https://m.media-amazon.com/images/I/91U2RGwI3xL._AC_UF894,1000_QL80_.jpg'),
    ('6b9f2315-78bd-4797-90f2-e27682481171', 'Indiana Jones and the Kingdom of the Crystal Skull', 'Indy faces Soviet agents and ancient aliens.', 'Steven Spielberg', 'PG-13', '2008-05-22', '6.1', 'https://m.media-amazon.com/images/I/61nzwied4LL._AC_UF1000,1000_QL80_.jpg');
    