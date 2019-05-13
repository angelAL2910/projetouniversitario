USE [ShipLogs]
GO
/****** Object:  Table [dbo].[AddressBook]    Script Date: 5/12/2019 10:08:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AddressBook](
	[UniqueAddressID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](50) NULL,
	[ATTN] [nchar](50) NULL,
	[Address] [text] NULL,
	[City] [nchar](40) NULL,
	[State] [nchar](40) NULL,
	[ZipCode] [nchar](20) NULL,
	[Country] [nchar](20) NULL,
	[PhoneNumber] [nchar](20) NULL,
	[MaxWeeklyShipments] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedAtWorkstation] [nchar](30) NULL,
	[CreatedByUser] [nchar](30) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedAtWorkstation] [nchar](30) NULL,
	[ModifiedByUser] [nchar](30) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Carriers]    Script Date: 5/12/2019 10:08:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Carriers](
	[CarrierUniqueID] [int] IDENTITY(1,1) NOT NULL,
	[CarrierName] [nchar](20) NOT NULL,
	[AccountNumber] [nchar](20) NOT NULL,
	[Comments] [text] NULL,
	[CarrierStatus] [bit] NOT NULL,
	[position] [int] NULL,
 CONSTRAINT [PK_Carriers] PRIMARY KEY CLUSTERED 
(
	[CarrierName] ASC,
	[AccountNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Carriers_20190508]    Script Date: 5/12/2019 10:08:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Carriers_20190508](
	[CarrierUniqueID] [int] IDENTITY(1,1) NOT NULL,
	[CarrierName] [nchar](20) NOT NULL,
	[AccountNumber] [nchar](20) NOT NULL,
	[Comments] [text] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Operators]    Script Date: 5/12/2019 10:08:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Operators](
	[OperatorID] [nchar](16) NULL,
	[OperatorFirstName] [nchar](12) NULL,
	[OperatorLastName] [nchar](12) NULL,
	[Status] [nchar](10) NULL,
	[position] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ShipmentDetails]    Script Date: 5/12/2019 10:08:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShipmentDetails](
	[DetailUniqueID] [int] IDENTITY(1,1) NOT NULL,
	[ShipUniqueID] [int] NULL,
	[AssignedTo] [nchar](20) NULL,
	[ItemDetail] [text] NULL,
 CONSTRAINT [PK_ShipmentDetails] PRIMARY KEY CLUSTERED 
(
	[DetailUniqueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ShipmentFiles]    Script Date: 5/12/2019 10:08:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShipmentFiles](
	[ShipUniqueID] [int] IDENTITY(1,1) NOT NULL,
	[CarrierName] [varchar](20) NULL,
	[AccountNumber] [varchar](15) NULL,
	[LoginID] [nvarchar](50) NULL,
	[ShipmentNumber] [nvarchar](20) NULL,
	[ShipmentDate] [datetime] NULL,
	[ReceiverCompany] [varchar](50) NULL,
	[ReceiverDepartment] [nvarchar](50) NULL,
	[ReceiverAddress] [nvarchar](150) NULL,
	[ReceiverCity] [nvarchar](30) NULL,
	[ReceiverState] [nvarchar](30) NULL,
	[ReceiverZipCode] [nvarchar](10) NULL,
	[ReceiverCountry] [nvarchar](30) NULL,
	[ReceiverPhoneNumber] [nvarchar](10) NULL,
	[ReceiverAttentionTo] [nvarchar](50) NULL,
	[BillToAccountNumber] [nvarchar](15) NULL,
	[PackageWeight] [nvarchar](10) NULL,
	[PackageDescription] [nvarchar](50) NULL,
	[CustomsValue] [nvarchar](5) NULL,
	[PieceCount] [nvarchar](5) NULL,
	[ServiceDescription] [nvarchar](50) NULL,
	[EstFreightChgs] [nvarchar](10) NULL,
	[EstOtherChgs] [nvarchar](10) NULL,
	[EstTotalChgs] [nvarchar](10) NULL,
	[SentBy] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Shipments]    Script Date: 5/12/2019 10:08:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shipments](
	[ShipUniqueID] [int] IDENTITY(1,1) NOT NULL,
	[CarrierName] [varchar](20) NULL,
	[AccountNumber] [varchar](12) NULL,
	[ShipmentNumber] [varchar](20) NULL,
	[ShipmentDate] [datetime] NULL,
	[ShipmentWeight] [numeric](18, 0) NULL,
	[ShipmentQTY] [int] NULL,
	[ShipPackageType] [varchar](20) NULL,
	[Operator] [varchar](16) NULL,
	[Sender] [varchar](50) NULL,
	[Receiver] [varchar](50) NULL,
	[ReceiverAttn] [varchar](50) NULL,
	[ReceiverAddress] [varchar](150) NULL,
	[ReceiverCity] [varchar](40) NULL,
	[ReceiverState] [varchar](40) NULL,
	[ReceiverZipCode] [varchar](20) NULL,
	[ReceiverCountry] [varchar](20) NULL,
	[ReceiverPhoneNumber] [varchar](20) NULL,
	[ShipmentComments] [varchar](max) NULL,
	[Transit] [bit] NULL,
	[Incoming] [bit] NULL,
	[CommissionChecks] [bit] NULL,
	[Materials] [bit] NULL,
	[OtherContents] [bit] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[SP_FILL_DROPDOWN]    Script Date: 5/12/2019 10:08:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Pedro Ven eiker 
-- Create date: 2019-01-24
-- Description:	Get DropDown Data
-- =============================================
CREATE PROCEDURE [dbo].[SP_FILL_DROPDOWN]
	@DropDownType varchar(30) = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF @DropDownType = 'ShipLogs_CARRIERS' 
		BEGIN
			SELECT   '0'[IdAlf],[CarrierUniqueID][Id],[CarrierName][Name]  FROM [dbo].[Carriers] order by 2 
		END
	
	IF @DropDownType = 'ShipLogs_OPERATOR' 
		BEGIN
			SELECT [OperatorID][IdAlf],0[Id],[OperatorFirstName]+''+[OperatorLastName][Name] FROM [ShipLogs].[dbo].[Operators] where Status='Active' ORDER BY Status 
		END
    IF @DropDownType = 'ShipLogs_ASSIGNED_TO' 
		BEGIN
		SELECT '0'[IdAlf],[Id],[NameAssigne][Name] FROM [dbo].[AssigneToPersonal] where IsActive=1 order by NameAssigne 
			 
		END
		

END
 

 
GO
/****** Object:  StoredProcedure [dbo].[SP_FROM_Shipment]    Script Date: 5/12/2019 10:08:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_FROM_Shipment]
( 
@ShipmentNumberParameter      nchar(20)
)
AS
BEGIN 
SELECT  a.ShipUniqueID        
	    ,CarrierName        
	    ,AccountNumber      
	    ,ShipmentNumber     
	    ,ShipmentDate       
	    ,ShipmentWeight     
	    ,ShipmentQTY        
	    ,ShipPackageType    
	    ,Operator[Operat]           
	    ,Sender             
	    ,Receiver           
	    ,ReceiverAttn       
	    ,ReceiverAddress    
	    ,ReceiverCity       
	    ,ReceiverState      
	    ,ReceiverZipCode    
	    ,ReceiverCountry    
	    ,ReceiverPhoneNumber
	    ,ShipmentComments   
	    ,Transit            
	    ,Incoming           
	    ,CommissionChecks   
	    ,Materials          
	    ,OtherContents      
	    ,AssignedTo         
	    ,b.ItemDetail  
		,b.DetailUniqueID
		,b.ShipUniqueID
 FROM DBO.Shipments a left join DBO.ShipmentDetails b on a.ShipUniqueID=b.ShipUniqueID

where a.ShipmentNumber=@ShipmentNumberParameter order by ShipmentDate desc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_FROM_Shipment_All]    Script Date: 5/12/2019 10:08:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
           
CREATE PROCEDURE [dbo].[SP_FROM_Shipment_All]
 
AS
BEGIN 
SELECT  

        ShipUniqueID        
	    ,RTRIM(LTRIM(isnull(CarrierName,'Personal')))[CarrierName]        
	    ,AccountNumber 
	    ,RTRIM(LTRIM(isnull(ShipmentNumber,'0')))[ShipmentNumber]       
	    ,ShipmentDate       
	    ,ShipmentWeight     
	    ,ShipmentQTY        
	    ,ShipPackageType    
	    ,Operator[Operat]           
	    ,Sender             
	    ,Receiver           
	    ,ReceiverAttn       
	    ,ReceiverAddress    
	    ,ReceiverCity       
	    ,ReceiverState      
	    ,ReceiverZipCode    
	    ,ReceiverCountry    
	    ,ReceiverPhoneNumber
	    ,ShipmentComments   
	    ,Transit            
	    ,iif(Incoming=1,'Incoming','Outgoing')[IncomingLiteral]
		,Incoming           
	    ,CommissionChecks   
	    ,Materials          
	    ,OtherContents      
	           
 FROM DBO.Shipments 
 order by ShipmentDate desc
END

       
GO
/****** Object:  StoredProcedure [dbo].[SP_GET_CHECK_LOGTYPE]    Script Date: 5/12/2019 10:08:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
           
CREATE  PROCEDURE  [dbo].[SP_GET_CHECK_LOGTYPE]
 @param int
AS
BEGIN 
SELECT TOP 1
        [Incoming]
 
 FROM DBO.Shipments  WHERE ShipUniqueID=@param 
 order by ShipmentDate desc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GET_IsIncomingVerification]    Script Date: 5/12/2019 10:08:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
           
CREATE  PROCEDURE   [dbo].[SP_GET_IsIncomingVerification]
 @param int
AS
BEGIN 
SELECT top 1
        iif(Incoming=1,'Incoming','Outgoing')[Incoming]
 FROM DBO.Shipments  WHERE ShipUniqueID=@param
 order by ShipmentDate desc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GET_Obtain_Shimet_Incoming]    Script Date: 5/12/2019 10:08:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
           
CREATE  PROCEDURE   [dbo].[SP_GET_Obtain_Shimet_Incoming]
 @param int
AS
BEGIN 
SELECT 
         a.ShipUniqueID     
	    ,AssignedTo         
	    ,b.ItemDetail  
		,b.DetailUniqueID
		,b.ShipUniqueID
 FROM DBO.Shipments a left join DBO.ShipmentDetails b on a.ShipUniqueID=b.ShipUniqueID WHERE b.ShipUniqueID=@param and a.Incoming=1
 order by ShipmentDate desc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GET_Obtain_Shimet_Outgoing]    Script Date: 5/12/2019 10:08:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE   [dbo].[SP_GET_Obtain_Shimet_Outgoing]
 
 @param int
 
AS
BEGIN 
SELECT   ShipUniqueID        
	    ,LTRIM(RTRIM(CarrierName))[CarrierName]      
	    ,AccountNumber      
	    ,LTRIM(RTRIM(isnull(ShipmentNumber,'No Num')))[ShipmentNumber]   
	    ,ShipmentDate       
	    ,ShipmentWeight     
	    ,ShipmentQTY        
	    ,ShipPackageType    
	    ,Operator[Operat] 
		,Operator          
	    ,Sender             
	    ,Receiver           
	    ,ReceiverAttn       
	    ,ReceiverAddress    
	    ,ReceiverCity       
	    ,ReceiverState      
	    ,ReceiverZipCode    
	    ,ReceiverCountry    
	    ,ReceiverPhoneNumber 
		,LTRIM(RTRIM(isnull(ShipmentComments,'No Comments')))[ShipmentComments] 
	    ,Transit            
	    ,iif(Incoming=1,'Incoming','Outgoing')[IncomingLiteral] 
		,Incoming          
	    ,CommissionChecks   
	    ,Materials          
	    ,OtherContents   
 FROM DBO.Shipments  

where ShipUniqueID=@param and Incoming=0

 order by ShipmentDate desc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GET_OPERATOR]    Script Date: 5/12/2019 10:08:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_GET_OPERATOR]
AS

BEGIN
SELECT distinct ltrim(rtrim(OperatorID))[OperatorID]
      ,ltrim(rtrim(OperatorFirstName))+' '+ ltrim(rtrim(OperatorLastName))[OperatorLastName]
	  ,position
  FROM [ShipLogs].[dbo].[Operators] where Status='active'
  END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCarrierDirec]    Script Date: 5/12/2019 10:08:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_GetCarrierDirec]
AS
BEGIN
SELECT        CarrierUniqueID, 
                LTRIM(RTRIM(ISNULL(upper(CarrierName),'Carrier Not Exist'))) [CarrierName], 
			   AccountNumber, 
			   Comments, 
			   CarrierStatus, 
			   position
FROM       [dbo].[Carriers] 
ORDER BY position
END
GO
/****** Object:  StoredProcedure [dbo].[SP_SET_SHIPMENT_DETAIL_SAVE]    Script Date: 5/12/2019 10:08:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
CREATE PROC [dbo].[SP_SET_SHIPMENT_DETAIL_SAVE]

 @ShipUniqueID   int=0
,@AssignedTo     nchar(20)=NULL
,@ItemDetail     text=NULL 
,@DetailUniqueID int=0
 
 AS

 
 
 BEGIN 
      INSERT INTO [dbo].[ShipmentDetails] VALUES(@ShipUniqueID,@AssignedTo,@ItemDetail)	 
 END
GO
/****** Object:  StoredProcedure [dbo].[SP_SET_SHIPMENT_DETAIL_UPDATE]    Script Date: 5/12/2019 10:08:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE PROC [dbo].[SP_SET_SHIPMENT_DETAIL_UPDATE]

 @ShipUniqueID   int=0
,@AssignedTo     nchar(20)=NULL
,@ItemDetail     text=NULL 
,@DetailUniqueID int=0
 
 AS

   
 
 BEGIN 
     UPDATE [dbo].[ShipmentDetails] SET 
	                                     @ShipUniqueID=@ShipUniqueID
	                                    ,@AssignedTo=@AssignedTo
									    ,@ItemDetail=@ItemDetail
                                    
									WHERE ShipUniqueID=@ShipUniqueID AND 
									      DetailUniqueID=@DetailUniqueID 
 
 END
GO
/****** Object:  StoredProcedure [dbo].[SP_SET_SHIPMENTS_UPDATE_INSERT]    Script Date: 5/12/2019 10:08:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE PROC [dbo].[SP_SET_SHIPMENTS_UPDATE_INSERT] 
  @ShipUniqueID        int
 ,@CarrierName         nchar(20)
 ,@AccountNumber       nchar(12)
 ,@ShipmentNumber      nchar(20)
 ,@ShipmentDate        datetime
 ,@ShipmentWeight      numeric(18,0)
 ,@ShipmentQTY         int
 ,@ShipPackageType     nchar(20)
 ,@Operator            nchar(16)
 ,@Sender              nchar(50)
 ,@Receiver            nchar(50)
 ,@ReceiverAttn        nchar(50)
 ,@ReceiverAddress     nchar(150)
 ,@ReceiverCity        nchar(40)
 ,@ReceiverState       nchar(40)
 ,@ReceiverZipCode     nchar(20)
 ,@ReceiverCountry     nchar(20)
 ,@ReceiverPhoneNumber nchar(20)
 ,@ShipmentComments    text
 ,@Transit             bit
 ,@Incoming            bit
 ,@CommissionChecks    bit
 ,@Materials           bit
 ,@OtherContents       bit

 
AS
 
BEGIN
DECLARE @ShipmentsID_New INT
IF @ShipUniqueID=0
BEGIN 
INSERT [DBO].[Shipments]
(
       [CarrierName]           
      ,[AccountNumber]         
      ,[ShipmentNumber]        
      ,[ShipmentDate]          
      ,[ShipmentWeight]        
      ,[ShipmentQTY]           
      ,[ShipPackageType]       
      ,[Operator]              
      ,[Sender]                
      ,[Receiver]              
      ,[ReceiverAttn]          
      ,[ReceiverAddress]       
      ,[ReceiverCity]          
      ,[ReceiverState]         
      ,[ReceiverZipCode]       
      ,[ReceiverCountry]       
      ,[ReceiverPhoneNumber]   
      ,[ShipmentComments]      
      ,[Transit]               
      ,[Incoming]              
      ,[CommissionChecks]      
      ,[Materials]             
      ,[OtherContents]   
)
VALUES
(
 @CarrierName         
 ,@AccountNumber       
 ,@ShipmentNumber      
 ,@ShipmentDate        
 ,@ShipmentWeight      
 ,@ShipmentQTY         
 ,@ShipPackageType     
 ,@Operator            
 ,@Sender              
 ,@Receiver            
 ,@ReceiverAttn        
 ,@ReceiverAddress     
 ,@ReceiverCity        
 ,@ReceiverState       
 ,@ReceiverZipCode     
 ,@ReceiverCountry     
 ,@ReceiverPhoneNumber 
 ,@ShipmentComments    
 ,@Transit             
 ,@Incoming            
 ,@CommissionChecks    
 ,@Materials           
 ,@OtherContents 
)

 SET @ShipmentsID_New = @@IDENTITY

SELECT @ShipmentsID_New[Id],'INSERT'[Value],'0'[IdAlf]
 
END 
ELSE 
BEGIN 
UPDATE [DBO].[Shipments] 
   SET [CarrierName]         =@CarrierName                  
      ,[AccountNumber]       =@AccountNumber              
      ,[ShipmentNumber]      =@ShipmentNumber            
      ,[ShipmentDate]        =@ShipmentDate                
      ,[ShipmentWeight]      =@ShipmentWeight            
      ,[ShipmentQTY]         =@ShipmentQTY                  
      ,[ShipPackageType]     =@ShipPackageType          
      ,[Operator]            =@Operator                        
      ,[Sender]              =@Sender                            
      ,[Receiver]            =@Receiver                        
      ,[ReceiverAttn]        =@ReceiverAttn                
      ,[ReceiverAddress]     =@ReceiverAddress          
      ,[ReceiverCity]        =@ReceiverCity                
      ,[ReceiverState]       =@ReceiverState              
      ,[ReceiverZipCode]     =@ReceiverZipCode          
      ,[ReceiverCountry]     =@ReceiverCountry          
      ,[ReceiverPhoneNumber] =@ReceiverPhoneNumber  
      ,[ShipmentComments]    =@ShipmentComments        
      ,[Transit]             =@Transit                          
      ,[Incoming]            =@Incoming                        
      ,[CommissionChecks]    =@CommissionChecks        
      ,[Materials]           =@Materials                      
      ,[OtherContents]       =@OtherContents
	          
WHERE ShipUniqueID=@ShipUniqueID 

SELECT @ShipUniqueID[Id],'UPDATE'[Value],'0'[IdAlf]
 
END  
END
GO
