USE [db_capstone_prototype]
GO
/****** Object:  Table [dbo].[company]    Script Date: 7/24/2021 3:07:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[company](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[headquarters] [ntext] NOT NULL,
	[website] [varchar](50) NULL,
	[description] [ntext] NULL,
	[avatar] [text] NULL,
	[create_date] [date] NULL,
	[recruiter_id] [int] NOT NULL,
 CONSTRAINT [PK_company] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[cv]    Script Date: 7/24/2021 3:07:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cv](
	[student_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL,
	[sex] [bit] NULL,
	[dob] [date] NULL,
	[avatar] [text] NULL,
	[school] [nvarchar](100) NULL,
	[experience] [nvarchar](500) NULL,
	[foreign_language] [nvarchar](10) NULL,
	[desired_salary_minimum] [int] NULL,
	[working_form] [int] NULL,
	[create_date] [date] NULL,
	[is_subscribed] [bit] NULL,
 CONSTRAINT [PK_cv] PRIMARY KEY CLUSTERED 
(
	[student_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[job]    Script Date: 7/24/2021 3:07:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[job](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [ntext] NOT NULL,
	[working_form] [int] NOT NULL,
	[location] [int] NOT NULL,
	[working_place] [ntext] NOT NULL,
	[description] [ntext] NOT NULL,
	[requirement] [ntext] NOT NULL,
	[type] [bit] NOT NULL,
	[offer] [ntext] NOT NULL,
	[sex] [int] NULL,
	[quantity] [int] NOT NULL,
	[salary_min] [int] NOT NULL,
	[salary_max] [int] NOT NULL,
	[recruiter_id] [int] NOT NULL,
	[create_date] [date] NOT NULL,
	[manager_id] [int] NULL,
	[status] [int] NOT NULL,
 CONSTRAINT [PK_job] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[manager]    Script Date: 7/24/2021 3:07:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[manager](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](20) NOT NULL,
	[password] [varchar](150) NOT NULL,
	[full_name] [nvarchar](50) NULL,
	[create_date] [date] NULL,
	[role] [varchar](20) NOT NULL,
	[is_banned] [bit] NULL,
 CONSTRAINT [PK_manager] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[recruiter]    Script Date: 7/24/2021 3:07:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[recruiter](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](20) NOT NULL,
	[password] [varchar](150) NOT NULL,
	[gmail] [varchar](50) NOT NULL,
	[phone] [text] NOT NULL,
	[avatar] [text] NULL,
	[create_date] [date] NULL,
	[role] [varchar](20) NOT NULL,
	[is_banned] [bit] NULL,
	[full_name] [nvarchar](50) NULL,
 CONSTRAINT [PK_recruiter] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_recruiter] UNIQUE NONCLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_recruiter_1] UNIQUE NONCLUSTERED 
(
	[gmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[student]    Script Date: 7/24/2021 3:07:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[student](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[gmail] [varchar](50) NOT NULL,
	[phone] [varchar](15) NULL,
	[is_banned] [bit] NULL,
	[create_date] [date] NOT NULL,
	[profile_status] [bit] NOT NULL,
	[avatar] [text] NULL,
	[google_id] [varchar](700) NOT NULL,
 CONSTRAINT [PK_student] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_student] UNIQUE NONCLUSTERED 
(
	[gmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[student_apply_job]    Script Date: 7/24/2021 3:07:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[student_apply_job](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[job_id] [int] NOT NULL,
	[student_id] [int] NOT NULL,
	[create_date] [date] NOT NULL,
 CONSTRAINT [PK_student_apply_job] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[company]  WITH CHECK ADD  CONSTRAINT [FK_company_recruiter] FOREIGN KEY([recruiter_id])
REFERENCES [dbo].[recruiter] ([id])
GO
ALTER TABLE [dbo].[company] CHECK CONSTRAINT [FK_company_recruiter]
GO
ALTER TABLE [dbo].[cv]  WITH CHECK ADD  CONSTRAINT [FK_cv_student] FOREIGN KEY([student_id])
REFERENCES [dbo].[student] ([id])
GO
ALTER TABLE [dbo].[cv] CHECK CONSTRAINT [FK_cv_student]
GO
ALTER TABLE [dbo].[job]  WITH CHECK ADD  CONSTRAINT [FK_job_manager] FOREIGN KEY([manager_id])
REFERENCES [dbo].[manager] ([id])
GO
ALTER TABLE [dbo].[job] CHECK CONSTRAINT [FK_job_manager]
GO
ALTER TABLE [dbo].[job]  WITH CHECK ADD  CONSTRAINT [FK_job_recruiter] FOREIGN KEY([recruiter_id])
REFERENCES [dbo].[recruiter] ([id])
GO
ALTER TABLE [dbo].[job] CHECK CONSTRAINT [FK_job_recruiter]
GO
ALTER TABLE [dbo].[student_apply_job]  WITH CHECK ADD  CONSTRAINT [FK_student_apply_job_job] FOREIGN KEY([job_id])
REFERENCES [dbo].[job] ([id])
GO
ALTER TABLE [dbo].[student_apply_job] CHECK CONSTRAINT [FK_student_apply_job_job]
GO
ALTER TABLE [dbo].[student_apply_job]  WITH CHECK ADD  CONSTRAINT [FK_student_apply_job_student] FOREIGN KEY([student_id])
REFERENCES [dbo].[student] ([id])
GO
ALTER TABLE [dbo].[student_apply_job] CHECK CONSTRAINT [FK_student_apply_job_student]
GO
