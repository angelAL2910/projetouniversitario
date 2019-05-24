USE [ShipLogs]
GO
/****** Object:  StoredProcedure [dbo].[SP_FILL_DROPDOWN]    Script Date: 5/24/2019 5:56:25 PM ******/
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
/****** Object:  StoredProcedure [dbo].[SP_FROM_Shipment]    Script Date: 5/24/2019 5:56:25 PM ******/
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
/****** Object:  StoredProcedure [dbo].[SP_FROM_Shipment_All]    Script Date: 5/24/2019 5:56:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
           
CREATE PROCEDURE [dbo].[SP_FROM_Shipment_All]
 
AS
BEGIN 
SELECT   top 1000
         ShipUniqueID        
	    ,RTRIM(LTRIM(isnull(CarrierName,'OTHER')))[CarrierName]        
	    ,AccountNumber 
	    ,RTRIM(LTRIM(isnull(ShipmentNumber,'0')))[ShipmentNumber]       
	    ,CONVERT(DATE,ShipmentDate) [ShipmentDate]  
	    ,ShipmentWeight     
	    ,isnull(ShipmentQTY,0)[ShipmentQTY]        
	    ,ShipPackageType    
	    ,isnull(Operator,'MVAZQUEZ')[Operator]        
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
	    ,isnull(Transit,0)[Transit] 
	    ,iif(Incoming=1,'Incoming','Outgoing')[IncomingLiteral] 
		,isnull(Incoming,0)[Incoming]
	    ,isnull(CommissionChecks,0)[CommissionChecks]   
	    ,isnull(Materials,0)[Materials]          
	    ,isnull(OtherContents,0)[OtherContents] 
		
		FROM DBO.Shipments  
 order by ShipmentDate desc
END

 

GO
/****** Object:  StoredProcedure [dbo].[SP_GET_CHECK_LOGTYPE]    Script Date: 5/24/2019 5:56:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
           
CREATE  PROCEDURE  [dbo].[SP_GET_CHECK_LOGTYPE]
 @param int
AS
BEGIN 
SELECT TOP 1
        ISNULL([Incoming],0)[Incoming]
 FROM DBO.Shipments  WHERE ShipUniqueID=@param
 order by ShipmentDate desc
END

 
GO
/****** Object:  StoredProcedure [dbo].[SP_GET_IsIncomingVerification]    Script Date: 5/24/2019 5:56:25 PM ******/
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
/****** Object:  StoredProcedure [dbo].[SP_GET_Obtain_Shimet_Incoming]    Script Date: 5/24/2019 5:56:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
           
CREATE  PROCEDURE   [dbo].[SP_GET_Obtain_Shimet_Incoming]
 @param int
AS
BEGIN 
SELECT top 1

       a.[ShipUniqueID]      
	    ,LTRIM(RTRIM(CarrierName))[CarrierName] --CarrierName        
	    ,AccountNumber      
	    ,LTRIM(RTRIM(isnull(ShipmentNumber,'No Num')))[ShipmentNumber]   
	    ,ShipmentDate       
	    ,ShipmentWeight     
	    ,isnull(ShipmentQTY,0)[ShipmentQTY]        
	    ,ShipPackageType    
	    ,Operator[Operat] 
		,LTRIM(RTRIM(isnull(Operator,'No Data')))[Operator]          
	    ,Sender             
	    ,Receiver           
	    ,ReceiverAttn       
	    ,ReceiverAddress    
	    ,ReceiverCity       
	    ,ReceiverState      
	    ,ReceiverZipCode    
	    ,ReceiverCountry    
	    ,ReceiverPhoneNumber
	    ,LTRIM(RTRIM(isnull(ShipmentComments,'Not Shipment Comments')))ShipmentComments   
	    ,Transit            
	    ,iif(Incoming=1,'Incoming','Outgoing')[IncomingLiteral] 
		,Incoming          
	    ,isnull(CommissionChecks,0)[CommissionChecks]   
	    ,isnull(Materials,0)[Materials]             
	    ,isnull(OtherContents,0)[OtherContents]    
	  ,b.[DetailUniqueID]  

 FROM DBO.Shipments a left join DBO.ShipmentDetails b on a.ShipUniqueID=b.ShipUniqueID WHERE b.ShipUniqueID=@param and a.Incoming=1
 order by ShipmentDate desc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GET_Obtain_Shimet_Outgoing]    Script Date: 5/24/2019 5:56:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE   [dbo].[SP_GET_Obtain_Shimet_Outgoing]
 
 @param int
 
AS
BEGIN 
SELECT   ShipUniqueID        
	    ,LTRIM(RTRIM(CarrierName))[CarrierName] --CarrierName        
	    ,AccountNumber      
	    ,LTRIM(RTRIM(isnull(ShipmentNumber,'No Num')))[ShipmentNumber]   
	    ,ShipmentDate       
	    ,ShipmentWeight     
	    ,ShipmentQTY        
	    ,ShipPackageType    
	    ,Operator[Operat] 
		,LTRIM(RTRIM(isnull(Operator,'No Data')))[Operator]          
	    ,Sender             
	    ,Receiver           
	    ,ReceiverAttn       
	    ,ReceiverAddress    
	    ,ReceiverCity       
	    ,ReceiverState      
	    ,ReceiverZipCode    
	    ,ReceiverCountry    
	    ,ReceiverPhoneNumber
	    ,LTRIM(RTRIM(isnull(ShipmentComments,'Not Shipment Comments')))ShipmentComments   
	    ,Transit            
	    ,iif(Incoming=1,'Incoming','Outgoing')[IncomingLiteral] 
		,Incoming          
	    ,isnull(CommissionChecks,0)[CommissionChecks]   
	    ,isnull(Materials,0)[Materials]             
	    ,isnull(OtherContents,0)[OtherContents]      
 FROM DBO.Shipments  

where ShipUniqueID=@param and Incoming=0

 order by ShipmentDate desc
END



GO
/****** Object:  StoredProcedure [dbo].[SP_GET_OPERATOR]    Script Date: 5/24/2019 5:56:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_GET_OPERATOR]
AS

BEGIN
  SELECT ltrim(rtrim(OperatorID))[OperatorID]
      ,ltrim(rtrim(OperatorFirstName))+' '+ ltrim(rtrim(OperatorLastName))[OperatorLastName]
      ,[position]
  FROM [ShipLogs].[dbo].[Operators] where position IN(
  1,2,3,4,5
  ) ORDER BY position
END



 
GO
/****** Object:  StoredProcedure [dbo].[SP_GET_SHIPMENTDETAILS]    Script Date: 5/24/2019 5:56:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE PROC [dbo].[SP_GET_SHIPMENTDETAILS]

 @ShipUniqueID   int=0
 
 
 AS

   
 
 BEGIN 

    SELECT A.ShipUniqueID,B.AssignedTo,B.ItemDetail,B.DetailUniqueID FROM DBO.[Shipments]A inner JOIN DBO.[ShipmentDetails]B
	ON A.ShipUniqueID=B.ShipUniqueID

	WHERE A.ShipUniqueID=@ShipUniqueID AND A.Incoming=1

	ORDER BY B.ShipUniqueID 
	
 
 END

 
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCarrierDirec]    Script Date: 5/24/2019 5:56:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE PROC [dbo].[SP_GetCarrierDirec]
AS
BEGIN
SELECT        CarrierUniqueID, 
                LTRIM(RTRIM(ISNULL(upper(CarrierName),'OTHER'))) [CarrierName],  
			   CarrierStatus, 
			   position
    FROM     [dbo].[Carriers]
	  
ORDER BY position 
END

GO
/****** Object:  StoredProcedure [dbo].[SP_SET_SHIPMENT_DETAIL_DELETE]    Script Date: 5/24/2019 5:56:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE PROC [dbo].[SP_SET_SHIPMENT_DETAIL_DELETE]

 @ShipUniqueID   int=0
 
 AS   
 
 BEGIN 
     DELETE [dbo].[ShipmentDetails] WHERE ShipUniqueID=@ShipUniqueID
    
 END
GO
/****** Object:  StoredProcedure [dbo].[SP_SET_SHIPMENT_DETAIL_SAVE]    Script Date: 5/24/2019 5:56:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
CREATE PROC [dbo].[SP_SET_SHIPMENT_DETAIL_SAVE]

 @ShipUniqueID   int=0
,@AssignedTo     nchar(20)=NULL
,@ItemDetail     text=NULL 
,@operation      varchar(50)
,@DetailUniqueID int=0
 
 AS

 
 
 BEGIN 
      INSERT INTO [dbo].[ShipmentDetails] VALUES(@ShipUniqueID,@AssignedTo,@ItemDetail)	 
 END
GO
/****** Object:  StoredProcedure [dbo].[SP_SET_SHIPMENT_DETAIL_UPDATE]    Script Date: 5/24/2019 5:56:25 PM ******/
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
/****** Object:  StoredProcedure [dbo].[SP_SET_SHIPMENTS_UPDATE_INSERT]    Script Date: 5/24/2019 5:56:25 PM ******/
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

SELECT @ShipmentsID_New[Id],
               'INSERT'[Value]
			   ,iif(@Incoming=1,'Incoming','Outgoing')[IdAlf]
 
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

SELECT @ShipUniqueID[Id],'UPDATE'[Value],iif(@Incoming=1,'Incoming','Outgoing')[IdAlf]
 
END  
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ShipmentErrorsFile]    Script Date: 5/24/2019 5:56:25 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[sp_ShipmentErrorsFile]
@carrierCompany varchar(15),
@office varchar(50),
@dateFrom datetime,
@dateTo datetime
AS

BEGIN

DECLARE @sql nvarchar(1000)

SET @sql = 'SELECT
sf.ShipmentNumber,
s.Sender,
sf.SentBy,
s.ShipmentWeight,
sf.PackageWeight,
s.ShipmentQTY,
sf.PieceCount,
sf.ReceiverCompany,
s.ShipPackageType,
convert(varchar(10), sf.ShipmentDate, 101) as ShipmentDate
FROM 
Shipments s join ShipmentFiles sf ON
s.ShipmentNumber = sf.ShipmentNumber
WHERE
s.CarrierName = ''' + @carrierCompany + '''' +
' AND sf.CustomsValue not like ''%$%''' + 
' AND sf.ShipmentDate >= ''' + convert(varchar(25), @dateFrom) + '''' +
' AND sf.ShipmentDate <= ''' + convert(varchar(25), @dateTo) + ''''

IF @office <> ''
BEGIN
	SET @sql = @sql + ' AND sf.ReceiverCompany = ''' + @office + ''''
END

SET @sql = @sql + ' ORDER BY s.ShipmentDate, s.ShipmentNumber'

EXEC sp_executesql @sql

END


GO
/****** Object:  StoredProcedure [dbo].[sp_ShipmentMatch]    Script Date: 5/24/2019 5:56:25 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[sp_ShipmentMatch]
@carrierCompany varchar(15),
@office varchar(50),
@dateFrom datetime,
@dateTo datetime
AS

BEGIN

DECLARE @sql nvarchar(1000)

SET @sql = 'SELECT
sf.ShipmentNumber,
s.Sender,
sf.SentBy,
s.ShipmentWeight,
sf.PackageWeight,
s.ShipmentQTY,
sf.PieceCount,
sf.ReceiverCompany,
s.ShipPackageType,
convert(varchar(10), sf.ShipmentDate, 101) as ShipmentDate
FROM 
Shipments s JOIN ShipmentFiles sf ON
s.ShipmentNumber = sf.ShipmentNumber
AND s.Sender = sf.SentBy
AND s.ShipmentQTY = CASE WHEN substring(sf.PieceCount, 1,1) = ''$'' THEN 0 ELSE
			CASE WHEN isnumeric(sf.PieceCount)=0 then 0 ELSE sf.PieceCount END END
AND s.ShipmentWeight = CASE WHEN isnumeric(sf.PackageWeight)=0 then 0 ELSE sf.PackageWeight END
WHERE
s.CarrierName = ''' + @carrierCompany + '''' +
' AND sf.ShipmentDate >= ''' + convert(varchar(25), @dateFrom) + '''' +
' AND sf.ShipmentDate <= ''' + convert(varchar(25), @dateTo) + ''''

IF @office <> ''
BEGIN
	SET @sql = @sql + ' AND sf.ReceiverCompany = ''' + @office + ''''
END

SET @sql = @sql + ' ORDER BY s.ShipmentDate, s.ShipmentNumber'

EXEC sp_executesql @sql

END
GO
/****** Object:  StoredProcedure [dbo].[sp_ShipmentMatchTotal]    Script Date: 5/24/2019 5:56:25 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[sp_ShipmentMatchTotal]
@carrierCompany varchar(15),
@office varchar(50),
@dateFrom datetime,
@dateTo datetime
AS

BEGIN

DECLARE @sql nvarchar(1000)

SET @sql = 'SELECT
SUM(CAST(EstTotalChgs AS money)) AS Total
FROM 
Shipments s join ShipmentFiles sf ON
s.ShipmentNumber = sf.ShipmentNumber
AND s.Sender = sf.SentBy
AND s.ShipmentQTY = CASE WHEN substring(sf.PieceCount, 1,1) = ''$'' THEN 0 ELSE
			CASE WHEN isnumeric(sf.PieceCount)=0 then 0 ELSE sf.PieceCount END END
AND s.ShipmentWeight = CASE WHEN isnumeric(sf.PackageWeight)=0 then 0 ELSE sf.PackageWeight END
WHERE
s.CarrierName = ''' + @carrierCompany + '''' +
' AND sf.ShipmentDate >= ''' + convert(varchar(25), @dateFrom) + '''' +
' AND sf.ShipmentDate <= ''' + convert(varchar(25), @dateTo) + ''''

IF @office <> ''
BEGIN
	SET @sql = @sql + ' AND sf.ReceiverCompany = ''' + @office + ''''
END

EXEC sp_executesql @sql

END
GO
/****** Object:  StoredProcedure [dbo].[sp_ShipmentMismatch]    Script Date: 5/24/2019 5:56:25 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[sp_ShipmentMismatch]
@carrierCompany varchar(15),
@office varchar(50),
@dateFrom datetime,
@dateTo datetime
AS

BEGIN

DECLARE @sql nvarchar(1000)

SET @sql = 'SELECT
sf.ShipmentNumber,
s.Sender,
sf.SentBy,
s.ShipmentWeight,
sf.PackageWeight,
s.ShipmentQTY,
sf.PieceCount,
sf.ReceiverCompany,
s.ShipPackageType,
convert(varchar(10), sf.ShipmentDate, 101) as ShipmentDate
FROM 
Shipments s join ShipmentFiles sf ON
s.ShipmentNumber = sf.ShipmentNumber
WHERE
s.CarrierName = ''' + @carrierCompany + '''' +
' AND (s.Sender <> sf.SentBy
 OR s.ShipmentQTY <> CASE WHEN substring(sf.PieceCount, 1,1) = ''$'' THEN 0 ELSE
			CASE WHEN isnumeric(sf.PieceCount)=0 then 0 ELSE sf.PieceCount END END
OR s.ShipmentWeight <> CASE WHEN isnumeric(sf.PackageWeight)=0 then 0 ELSE sf.PackageWeight END)' +
' AND sf.ShipmentDate >= ''' + convert(varchar(25), @dateFrom) + '''' +
' AND sf.ShipmentDate <= ''' + convert(varchar(25), @dateTo) + ''''

IF @office <> ''
BEGIN
	SET @sql = @sql + ' AND sf.ReceiverCompany = ''' + @office + ''''
END

SET @sql = @sql + ' ORDER BY s.ShipmentDate, s.ShipmentNumber'

EXEC sp_executesql @sql

END
GO
/****** Object:  StoredProcedure [dbo].[sp_ShipmentMismatchTotal]    Script Date: 5/24/2019 5:56:25 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[sp_ShipmentMismatchTotal]
@carrierCompany varchar(15),
@office varchar(50),
@dateFrom datetime,
@dateTo datetime
AS

BEGIN

DECLARE @sql nvarchar(1000)

SET @sql = 'SELECT
SUM(CAST(EstTotalChgs AS money)) AS Total
FROM 
Shipments s join ShipmentFiles sf ON
s.ShipmentNumber = sf.ShipmentNumber
WHERE
s.CarrierName = ''' + @carrierCompany + '''' +
' AND (s.Sender <> sf.SentBy
 OR s.ShipmentQTY <> CASE WHEN substring(sf.PieceCount, 1,1) = ''$'' THEN 0 ELSE
			CASE WHEN isnumeric(sf.PieceCount)=0 then 0 ELSE sf.PieceCount END END
OR s.ShipmentWeight <> CASE WHEN isnumeric(sf.PackageWeight)=0 then 0 ELSE sf.PackageWeight END)' +
' AND sf.ShipmentDate >= ''' + convert(varchar(25), @dateFrom) + '''' +
' AND sf.ShipmentDate <= ''' + convert(varchar(25), @dateTo) + ''''

IF @office <> ''
BEGIN
	SET @sql = @sql + ' AND sf.ReceiverCompany = ''' + @office + ''''
END

EXEC sp_executesql @sql

END
GO
/****** Object:  StoredProcedure [dbo].[sp_ShipmentOfficeSender]    Script Date: 5/24/2019 5:56:25 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[sp_ShipmentOfficeSender]  AS

SELECT Name, 
CASE WHEN MaxWeeklyShipments IS NULL THEN 0 
ELSE MaxWeeklyShipments END AS MaxWeeklyShipments
FROM AddressBook 
GROUP BY Name, MaxWeeklyShipments

GO
/****** Object:  StoredProcedure [dbo].[sp_ShipmentQuantityGreaterThan]    Script Date: 5/24/2019 5:56:25 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_ShipmentQuantityGreaterThan]
@carrierCompany varchar(15),
@dateFrom datetime,
@dateTo datetime,
@quantity int
AS

SELECT
sf.ShipmentNumber,
s.Sender,
sf.SentBy,
s.ShipmentWeight,
sf.PackageWeight,
s.ShipmentQTY,
sf.PieceCount,
sf.ReceiverCompany,
s.ShipPackageType,
convert(varchar(10), sf.ShipmentDate, 101) as ShipmentDate
FROM 
Shipments s join ShipmentFiles sf ON
s.ShipmentNumber = sf.ShipmentNumber
WHERE
s.CarrierName = @carrierCompany and
sf.ShipmentDate >= @dateFrom and sf.ShipmentDate <= @dateTo
and s.ShipmentQTY >= @quantity
order by s.ShipmentDate, s.ShipmentNumber
GO
/****** Object:  StoredProcedure [dbo].[sp_ShipmentSaveFile]    Script Date: 5/24/2019 5:56:25 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[sp_ShipmentSaveFile]
(
@CarrierName varchar(20),
@AccountNumber varchar(15),
@LoginID varchar(50),
@ShipmentNumber varchar(20),
@ShipmentDate datetime,
@ReceiverCompany varchar(50),
@ReceiverDepartment varchar(50),
@ReceiverAddress varchar(150),
@ReceiverCity varchar(30),
@ReceiverState	 varchar(30)	,
@ReceiverZipCode varchar(10),
@ReceiverCountry varchar(30),
@ReceiverPhoneNumber varchar(10),
@ReceiverAttentionTo varchar(50),
@BillToAccountNumber	varchar(15),
@PackageWeight varchar(10),
@PackageDescription varchar(50),
@CustomsValue	 varchar(5),
@PieceCount varchar(5),
@ServiceDescription varchar(50),
@EstFreightChgs varchar(10),
@EstOtherChgs varchar(10),
@EstTotalChgs varchar(10),
@SentBy varchar(50)
)
AS

DECLARE @count bit

SET @count = (
SELECT COUNT(*) FROM ShipmentFiles 
WHERE 
AccountNumber = @AccountNumber AND
ShipmentNumber = @ShipmentNumber AND
ShipmentDate = @ShipmentDate
)

IF @count = 0
BEGIN

INSERT ShipmentFiles
(
CarrierName,
AccountNumber,
LoginID,
ShipmentNumber,
ShipmentDate,
ReceiverCompany,
ReceiverDepartment,
ReceiverAddress,
ReceiverCity,
ReceiverState	,
ReceiverZipCode,
ReceiverCountry,
ReceiverPhoneNumber,
ReceiverAttentionTo,
BillToAccountNumber,
PackageWeight,
PackageDescription,
CustomsValue,
PieceCount,
ServiceDescription,
EstFreightChgs,
EstOtherChgs,
EstTotalChgs,
SentBy
)
VALUES
(
@CarrierName,
@AccountNumber,
@LoginID,
@ShipmentNumber,
@ShipmentDate,
@ReceiverCompany,
@ReceiverDepartment,
@ReceiverAddress,
@ReceiverCity,
@ReceiverState	,
@ReceiverZipCode,
@ReceiverCountry,
@ReceiverPhoneNumber,
@ReceiverAttentionTo,
@BillToAccountNumber,
@PackageWeight,
@PackageDescription,
@CustomsValue	,
@PieceCount,
@ServiceDescription,
@EstFreightChgs,
@EstOtherChgs,
@EstTotalChgs,
@SentBy
)

END

GO
/****** Object:  StoredProcedure [dbo].[sp_ShipmentTotalShipments]    Script Date: 5/24/2019 5:56:25 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[sp_ShipmentTotalShipments]
@carrierCompany varchar(15),
@office varchar(50),
@dateFrom datetime,
@dateTo datetime,
@quantity int
AS

BEGIN

DECLARE @sql nvarchar(1000)

SET @sql = 'SELECT 
ReceiverCompany, 
count(ReceiverCompany) AS Total 
FROM 
Shipments s JOIN ShipmentFiles sf ON
s.ShipmentNumber = sf.ShipmentNumber
WHERE
(s.CommissionChecks IS NULL OR s.CommissionChecks = 0)
AND (s.Materials IS NULL OR s.Materials = 0)
AND s.CarrierName = ''' + @carrierCompany + '''' +
' AND sf.ShipmentDate >= ''' + convert(varchar(25), @dateFrom) + '''' +
' AND sf.ShipmentDate <= ''' + convert(varchar(25), @dateTo) + ''''

IF @office <> ''
BEGIN
	SET @sql = @sql + ' AND sf.ReceiverCompany = ''' + @office + ''''
END

SET @sql = @sql + 
' GROUP BY ReceiverCompany 
HAVING COUNT(ReceiverCompany) > ' +  convert(varchar(2), @quantity)

EXEC sp_executesql @sql

END
GO
/****** Object:  Table [dbo].[Shipments]    Script Date: 5/24/2019 5:56:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Shipments](
	[ShipUniqueID] [int] IDENTITY(1,1) NOT NULL,
	[CarrierName] [varchar](25) NULL,
	[AccountNumber] [varchar](50) NULL,
	[ShipmentNumber] [varchar](50) NULL,
	[ShipmentDate] [datetime] NULL,
	[ShipmentWeight] [numeric](18, 0) NULL,
	[ShipmentQTY] [int] NULL,
	[ShipPackageType] [varchar](50) NULL,
	[Operator] [varchar](50) NULL,
	[Sender] [varchar](50) NULL,
	[Receiver] [varchar](50) NULL,
	[ReceiverAttn] [varchar](50) NULL,
	[ReceiverAddress] [varchar](150) NULL,
	[ReceiverCity] [varchar](50) NULL,
	[ReceiverState] [varchar](50) NULL,
	[ReceiverZipCode] [varchar](50) NULL,
	[ReceiverCountry] [varchar](50) NULL,
	[ReceiverPhoneNumber] [varchar](50) NULL,
	[ShipmentComments] [varchar](max) NULL,
	[Transit] [bit] NULL,
	[Incoming] [bit] NULL,
	[CommissionChecks] [bit] NULL,
	[Materials] [bit] NULL,
	[OtherContents] [bit] NULL,
 CONSTRAINT [PK_Shipments] PRIMARY KEY CLUSTERED 
(
	[ShipUniqueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Shipments] ADD  CONSTRAINT [DF_Shipments_Transit]  DEFAULT (0) FOR [Transit]
GO
ALTER TABLE [dbo].[Shipments] ADD  CONSTRAINT [DF_Shipments_Incoming]  DEFAULT (0) FOR [Incoming]
GO
ALTER TABLE [dbo].[Shipments] ADD  CONSTRAINT [DF_Shipments_Materials]  DEFAULT (0) FOR [Materials]
GO
ALTER TABLE [dbo].[Shipments] ADD  CONSTRAINT [DF_Shipments_OtherContents]  DEFAULT (0) FOR [OtherContents]
GO
