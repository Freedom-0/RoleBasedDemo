USE [AUTH]
GO
/****** Object:  UserDefinedTableType [dbo].[MenuAccessData]    Script Date: 4/16/2024 1:44:22 PM ******/
CREATE TYPE [dbo].[MenuAccessData] AS TABLE(
	[MenuID] [int] NULL,
	[SubMenuID] [int] NULL
)
GO
/****** Object:  Table [dbo].[BranchLocation]    Script Date: 4/16/2024 1:44:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BranchLocation](
	[BranchId] [int] IDENTITY(1,1) NOT NULL,
	[BranchName] [varchar](255) NULL,
	[BranchCity] [varchar](255) NULL,
	[IsActive] [bit] NULL,
	[TDate] [date] NULL,
	[BranchCode]  AS (upper((left([BranchName],(2))+left([BranchCity],(2)))+CONVERT([varchar],[BranchId]))) PERSISTED,
PRIMARY KEY CLUSTERED 
(
	[BranchId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 4/16/2024 1:44:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[CustomerId] [int] NOT NULL,
	[CustomerName] [varchar](100) NOT NULL,
	[Email] [varchar](100) NULL,
	[Phone] [varchar](20) NULL,
	[TDATE] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerDetails]    Script Date: 4/16/2024 1:44:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomeID] [varchar](255) NULL,
	[CustomerName] [varchar](255) NULL,
	[CustomerAddress] [varchar](255) NULL,
	[CustomerPhone] [varchar](255) NULL,
	[CustomerEmail] [varchar](255) NULL,
	[CustomerDOB] [date] NULL,
	[AccountType] [varchar](255) NULL,
	[AccountNo] [varchar](255) NULL,
	[CreatedBy] [varchar](255) NULL,
	[CreatorRole] [varchar](255) NULL,
	[TDate] [date] NULL,
	[Branch] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_MenuAccess]    Script Date: 4/16/2024 1:44:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_MenuAccess](
	[MenuId] [int] NULL,
	[MenuSubId] [int] NULL,
	[UserId] [int] NULL,
	[PermittedBy] [varchar](255) NULL,
	[TDate] [date] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MasterMenu]    Script Date: 4/16/2024 1:44:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MasterMenu](
	[MenuId] [int] NOT NULL,
	[MenuName] [varchar](255) NULL,
	[MenuIcon] [varchar](255) NULL,
	[HasChild] [bit] NULL,
	[Status] [bit] NULL,
	[Url] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[MenuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MasterSubMenu]    Script Date: 4/16/2024 1:44:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MasterSubMenu](
	[MenuId] [int] NULL,
	[SubMenuId] [int] NULL,
	[SubMenuName] [varchar](255) NULL,
	[Status] [bit] NULL,
	[Url] [varchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleAssigned]    Script Date: 4/16/2024 1:44:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleAssigned](
	[RId] [int] IDENTITY(1,1) NOT NULL,
	[RoleID] [int] NULL,
	[BranchId] [int] NULL,
	[UserID] [int] NULL,
	[TDATE] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[RId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransctionBalance]    Script Date: 4/16/2024 1:44:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransctionBalance](
	[BID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [varchar](255) NULL,
	[CAccountNo] [varchar](255) NULL,
	[CName] [varchar](255) NULL,
	[TDate] [date] NULL,
	[TAmount] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[BID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransctionHistory]    Script Date: 4/16/2024 1:44:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransctionHistory](
	[TID] [int] IDENTITY(1,1) NOT NULL,
	[TAmount] [varchar](255) NULL,
	[TUserName] [varchar](255) NULL,
	[TAccountNo] [varchar](255) NULL,
	[TUserPhone] [varchar](255) NULL,
	[TDoneBy] [varchar](255) NULL,
	[TDoneID] [varchar](255) NULL,
	[TDate] [date] NULL,
	[TStatus] [varchar](255) NULL,
	[TApproverName] [varchar](255) NULL,
	[TBranch] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[TID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserCredentials]    Script Date: 4/16/2024 1:44:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserCredentials](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserDetails]    Script Date: 4/16/2024 1:44:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserDetails](
	[DetailID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[FullName] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[DetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 4/16/2024 1:44:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[RoleID] [int] NOT NULL,
	[Role] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[BranchLocation] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[BranchLocation] ADD  DEFAULT (getdate()) FOR [TDate]
GO
ALTER TABLE [dbo].[Customer] ADD  DEFAULT (getdate()) FOR [TDATE]
GO
ALTER TABLE [dbo].[CustomerDetails] ADD  DEFAULT (getdate()) FOR [TDate]
GO
ALTER TABLE [dbo].[Master_MenuAccess] ADD  DEFAULT (getdate()) FOR [TDate]
GO
ALTER TABLE [dbo].[TransctionBalance] ADD  DEFAULT (getdate()) FOR [TDate]
GO
ALTER TABLE [dbo].[TransctionHistory] ADD  DEFAULT (getdate()) FOR [TDate]
GO
ALTER TABLE [dbo].[MasterSubMenu]  WITH CHECK ADD FOREIGN KEY([MenuId])
REFERENCES [dbo].[MasterMenu] ([MenuId])
GO
/****** Object:  StoredProcedure [dbo].[CheckUserCredentials]    Script Date: 4/16/2024 1:44:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[CheckUserCredentials]
  @p_username NVARCHAR(255)='',
  @p_password NVARCHAR(255)='',
  @ACTIVITY VARCHAR(100) = ''
AS
BEGIN
  SET NOCOUNT ON;

  DECLARE @result INT;


    IF (@ACTIVITY = 'validate_user')
       BEGIN
          SELECT @result = COUNT(*) FROM UserCredentials
          WHERE username = @p_username AND Password = @p_password;
           -- Return 1 if there is a match, 0 otherwise
          IF @result = 1
           BEGIN SELECT 1 AS result; END
          ELSE
         BEGIN
             SELECT 0 AS result;
          END;
      END
	


	  -------------
   IF (@ACTIVITY = 'teST')  BEGIN  SELECT u.fullname as FullName,u.Email as UserMail, u.UserID as UID FROM userdetails u JOIN usercredentials ud ON u.userid = ud.userid WHERE ud.username = @p_username END
   IF (@ACTIVITY = 'ExtractRole')  BEGIN  SELECT  ur.role as UserRole FROM userrole ur JOIN  roleassigned ra ON ur.roleid = ra.roleid JOIN userdetails ud ON ra.userid = ud.userid WHERE ud.fullname = @p_username END
   IF (@ACTIVITY = 'ExtractLocation')  BEGIN  SELECT  ur.BranchName as BranchAssigned FROM BranchLocation ur JOIN  roleassigned ra ON ur.BranchId = ra.BranchId JOIN userdetails ud ON ra.userid = ud.userid WHERE ud.fullname = @p_username END
END;
GO
/****** Object:  StoredProcedure [dbo].[CUSTOMER_INFO]    Script Date: 4/16/2024 1:44:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CUSTOMER_INFO]

    @Activity VARCHAR(255),
    @CID VARCHAR(255) =null,                  
    @CustomerName VARCHAR(255) =null,
    @CustomerAddress VARCHAR(255) =null,
    @CustomerPhone VARCHAR(255)=null,
    @CustomerEmail VARCHAR(255)=null,
    @CustomerDOB DATE=null,
    @AccountType VARCHAR(255)=null,
    @AccountNo Varchar(255) =null,
    @CreatedBy VARCHAR(255)=null,
    @CreatorRole VARCHAR(255)=null,
	@Branch VARCHAR(255)=null,
	--added new parameters
	@CreatorID VARCHAR(255)=null,
	@TApproverName VARCHAR(255)=null,
	@TAmount VARCHAR(255)=null,
	@TStatus VARCHAR(255)=null,
	@TID VARCHAR(255) = null
	

AS
BEGIN
    IF (@Activity = 'NewCustomer')
    BEGIN
        -- Insert data into the CustomerDetails table
        INSERT INTO CustomerDetails (CustomeID, CustomerName, CustomerAddress, CustomerPhone, CustomerEmail, CustomerDOB, AccountType, AccountNo, CreatedBy, CreatorRole, Branch)
        VALUES (@CID, @CustomerName, @CustomerAddress, @CustomerPhone, @CustomerEmail, @CustomerDOB, @AccountType, @AccountNo, @CreatedBy, @CreatorRole, @Branch)
    END

    IF (@Activity = 'CustomerList')
    BEGIN
        -- Select list of customers
        SELECT accountno AS CODE, CustomerName AS NAME FROM customerdetails
    END

    IF (@Activity = 'AccountDetails')
    BEGIN
        -- Select account details based on account number
        SELECT accountno AS ACCOUNT, CustomerName AS NAME, customerPhone AS PHONE FROM customerdetails WHERE accountno = @CID
    END

	----transctions--
	 IF (@Activity = 'TxnProcess')
	 BEGIN
	 IF EXISTS (SELECT 1 FROM TransctionHistory WHERE TUserName = @CustomerName AND TAccountNo = @AccountNo AND TStatus = 'Pending')
    BEGIN
        -- Update existing transaction
        UPDATE TransctionHistory
        SET TStatus = CASE WHEN @TStatus = 'Approved' THEN 'Approved' ELSE 'Rejected' END,
            TApproverName = CASE WHEN @TStatus = 'Approved' THEN @TApproverName ELSE 'Checker' END
        WHERE TUserName =  @CustomerName AND TAccountNo = @AccountNo AND TStatus = 'Pending' AND TID =@TID
    END
    ELSE
    BEGIN
        -- Insert new transaction
        INSERT INTO TransctionHistory (TAmount, TUserName, TAccountNo, TUserPhone, TDoneBy, TDoneID, TStatus, TApproverName, TBranch)
        VALUES (@TAmount, @CustomerName, @AccountNo,@CustomerPhone,@CreatedBy, @CreatorID, 'Pending', 'Checker', 	@Branch )
    END
	END

	IF(@Activity ='PendingTxn')
	BEGIN
	 select * from TransctionHistory where Tstatus ='Pending'
	END
	--select * from TransctionHistory
	--select * from RoleAssigned
	--delete * from 
	--delete  from TransctionHistory
	--select * from transctionbalance
END
--TID	TAmount	TUserName	TAccountNo	TUserPhone	TDoneBy	TDoneID	TDate	TStatus	TApproverName
GO
/****** Object:  StoredProcedure [dbo].[DEMO_SP]    Script Date: 4/16/2024 1:44:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--select * from UserDetails
--select * from UserCredentials
--select * from RoleAssigned
--select * from UserRole


CREATE   PROCEDURE [dbo].[DEMO_SP]
  @user NVARCHAR(255)='',
  @userpass NVARCHAR(255)='',
  @ACTIVITY VARCHAR(100) = '',
  @fullname  NVARCHAR(255) ='',
  @email NVARCHAR(255) =''
AS
BEGIN
  SET NOCOUNT ON;

IF (@ACTIVITY = 'RegisterUser')
BEGIN
    BEGIN TRY
        -- Insert the user credentials
        INSERT INTO UserCredentials (Username, Password)
        VALUES (@user, @userpass);

        -- Get the generated UserID from the inserted row in UserCredentials
        DECLARE @NewUserId INT;
        SET @NewUserId = SCOPE_IDENTITY();

        -- Insert user details with the generated UserID
        INSERT INTO UserDetails (UserID, FullName, Email, UserName)
        VALUES (@NewUserId, @FullName, @Email, @user);

        -- Return 1 if successful
        SELECT 1 AS result;
    END TRY
    BEGIN CATCH
        -- If an error occurs, return 0 to indicate failure
        SELECT 0 AS result;
    END CATCH
END



 IF (@Activity = 'EmployeeList')
BEGIN  
select Userid as CODE , fullName as NAME from userdetails
END

 IF (@Activity = 'BranchList')
BEGIN  
select BranchId as CODE , BranchName as NAME from branchlocation
END


 IF (@Activity = 'RoleList')
BEGIN  
select RoleId as CODE , Role as NAME from UserRole
END




IF (@Activity ='GetSubMenuList')
BEGIN
SELECT Menuid,submenuid,submenuname,url from MasterSubMenu where status=1 order by menuid,submenuid
END

IF (@Activity ='GetMenuList')
BEGIN
--select * from mastermenu
SELECT Menuid,menuname,menuicon, cast(haschild as int) as haschild, cast(status as int) as status ,url from MasterMenu order by menuid
END

IF (@Activity ='CustomerList')
BEGIN
--select * from mastermenu
SELECT Menuid,menuname,menuicon, cast(haschild as int) as haschild, cast(status as int) as status ,url from MasterMenu order by menuid
END



    IF (@Activity = 'AddedCustomerList')
    BEGIN
        -- Select list of customers
        SELECT cast(accountno as int) AS CODE, CustomerName AS NAME FROM customerdetails
    END

   
--select * from branchlocation
--select * from Roleassigned
--select * from userrole
--select * from UserDetails



END;


--delete from UserCredentials
--select * from UserDetails
--select * from roleAssigned
--select * from Customer
--select * from CustomerDetails
--select * from transctionhistory
--select * from Master_MenuAccess

GO
/****** Object:  StoredProcedure [dbo].[TEST]    Script Date: 4/16/2024 1:44:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[TEST]
	@Activity varchar(50),
    @Data1 varchar(50) =null,
	@Data2 varchar(50) = null,
	@Data3 varchar(50) = null,
	@Data4 varchar(50) =null,
	@Data5 varchar(50) = null,
	@Data6 varchar(50) = null,
	@Data7 varchar(50)= null

	--not when in view or browser if you get error like @parameters any parameters from above is not in sp
	--to resole such issue simply put  =null ...
AS
BEGIN
	IF (@Activity = 'AccountDetails')
    BEGIN
        -- Select account details based on account number
        SELECT accountno AS ACCOUNT, CustomerName AS NAME, customerPhone AS PHONE FROM customerdetails WHERE accountno = @Data1
    END
	
END

GO
/****** Object:  StoredProcedure [dbo].[USERINFO]    Script Date: 4/16/2024 1:44:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USERINFO]
    @UserID INT,
    @RoleID INT,
    @BranchID INT,
    @MenuID INT,
    @SubMenuID INT,
    @Activity VARCHAR(20) -- Specify whether to update or insert ("UpdateOrInsert")
AS
BEGIN
    IF (@Activity = 'UpdateOrInsert')
    BEGIN
        -- Update or insert role assignment
        IF EXISTS (SELECT  * FROM RoleAssigned WHERE UserID = @UserID)
        BEGIN
            -- Update existing role assignment
            UPDATE RoleAssigned SET TDATE = GETDATE(), BranchId = @BranchID, RoleID = @RoleID WHERE UserID = @UserID;
        END
        ELSE
        BEGIN
            -- Insert new role assignment
            INSERT INTO RoleAssigned (RoleID, UserID, TDATE, BranchId) VALUES (@RoleID, @UserID, GETDATE(), @BranchID);
        END

     DELETE FROM Master_MenuAccess WHERE UserID = @UserID;

        -- Insert new menu access data
        INSERT INTO Master_MenuAccess (MenuID, MenuSubID, UserID, PermittedBy, TDate)
        VALUES (@MenuID, @SubMenuID, @UserID, 'Admin', GETDATE());



    END
	--select * from master_MenuAccess 
	--select * from roleassigned
	--select
	--select * from userdetails

	--to get user data
	IF (@Activity = 'GetRolesAndBranch')
    BEGIN
        -- Retrieve assigned roles,brnach for the user
        SELECT RoleID,BranchId
        FROM RoleAssigned
        WHERE UserID =@UserID;
    END
    ELSE IF (@Activity = 'GetMenuAccess')
    BEGIN
        -- Retrieve assigned menu,submenu access for the user
        SELECT MenuId, MenuSubId FROM Master_MenuAccess WHERE UserID = @UserID;
    END
END



GO
