USE [Loans]
GO
/****** Object:  StoredProcedure [dbo].[UPDATE_TABLES_FROM_EASYBANK]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[UPDATE_TABLES_FROM_EASYBANK] 

AS 

/*[financialSector]*/
SELECT 
 financialSectorId=admset_numid
,accountTypeID
,financialSectorName=admset_nombre
,isActive=admsts_codigo
,CreateDate=getdate()
,ModiDate=getdate()
,CreateUsrId=1
,ModiUsrId=1
,hostName=1
INTO #_TBL_eadmsetm
FROM [Easybank].dbo.eadmsetm A
INNER JOIN [global].[accountType] B 
ON B.accountTypeCode =A.admtcu_codigo



INSERT INTO [global].[financialSector]  
(
 financialSectorId
,accountTypeID
,financialSectorName
,isActive
,CreateDate
,ModiDate
,CreateUsrId
,ModiUsrId
,hostName
)
SELECT 
 financialSectorId
,accountTypeID
,financialSectorName
,isActive
,CreateDate
,ModiDate
,CreateUsrId
,ModiUsrId
,hostName
FROM #_TBL_eadmsetm A
WHERE financialSectorId NOT IN (SELECT financialSectorId FROM [global].[financialSector]  )

UPDATE A SET 
 A.accountTypeID=B.accountTypeID
,A.financialSectorName=B.financialSectorName
,A.isActive=B.isActive
,A.ModiDate=GETDATE()
FROM [global].[financialSector]  A 
INNER JOIN #_TBL_eadmsetm B ON A.financialSectorId=B.financialSectorId
DROP TABLE #_TBL_eadmsetm

/*[SourceIncome]*/

select 
	 companyId=admcia_codigo
	,SourceIncomeId=cast(prefue_codigo as int)
	,SourceIncomeName=prefue_nombre
	,SourceIncomeCode=prefue_codigo
	,isActive=1
	,CreateDate=getdate()
	,ModiDate=getdate()
	,CreateUsrId=1
	,ModiUsrId=1
	,hostName=1
INTO #_TBL_eprefuem
from [Easybank].dbo.eprefuem


INSERT INTO [global].[SourceIncome]  
(
	 companyId
	,SourceIncomeId
	,SourceIncomeName
	,SourceIncomeCode
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
)

SELECT 
	 companyId
	,SourceIncomeId
	,SourceIncomeName
	,SourceIncomeCode
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
FROM #_TBL_eprefuem A 
where SourceIncomeCode not in (select SourceIncomeCode from [global].[SourceIncome]  )

UPDATE A SET 
 A.SourceIncomeName=B.SourceIncomeName
,A.SourceIncomeCode=B.SourceIncomeCode
,A.isActive=B.isActive
,A.ModiDate=GETDATE()
FROM  [global].[SourceIncome] A
INNER JOIN #_TBL_eprefuem B ON A.SourceIncomeCode=B.SourceIncomeCode
DROP TABLE #_TBL_eprefuem


/*entity.Profession*/
SELECT 
	 ProfessionId=admpro_codigo
	,ProfessionName=RTRIM(LTRIM(admpro_nombre))
	,isActive=admsts_codigo
	,CreateDate=getdate()
	,ModiDate=getdate()
	,CreateUsrId=1
	,ModiUsrId=1
	,hostName=1
INTO #_TBL_xadmprom
FROM [Easybank].dbo.xadmprom



INSERT INTO entity.Profession
(
	 ProfessionId
	,ProfessionName
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
)
SELECT 
	 ProfessionId
	,ProfessionName
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
FROM #_TBL_xadmprom A
WHERE A.ProfessionId NOT IN (SELECT ProfessionId FROM entity.Profession)

UPDATE A SET 
 A.ProfessionName=B.ProfessionName
,A.ModiDate=B.ModiDate
,A.isActive=B.isActive
FROM entity.Profession A 
INNER JOIN #_TBL_xadmprom B ON A.ProfessionId=B.ProfessionId

DROP TABLE #_TBL_xadmprom 


/*[serviceProduct]*/

select 
	 serviceProductId=admtpr_numid
	--,accountTypeID=admtcu_codigo
	,accountTypeID
	,serviceProducName=admtpr_nombre
	,serviceProducCode=admtpr_codigo
	,isActive=admsts_codigo
	,CreateDate=getdate()
	,ModiDate=getdate()
	,CreateUsrId=1
	,ModiUsrId=1
	,hostName=1
INTO #_TBL_eadmtprm
from [Easybank].dbo.eadmtprm a
inner join [global].[accountType] b on rtrim(ltrim(a.admtcu_codigo)) = b.accountTypeCode


INSERT INTO [Loan].[serviceProduct]
(
	 serviceProductId
	,accountTypeID
	,serviceProducName
	,serviceProducCode
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
)
SELECT 
	 serviceProductId
	,accountTypeID
	,serviceProducName
	,serviceProducCode
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
FROM #_TBL_eadmtprm
where serviceProductId not in (select serviceProductId from [Loan].[serviceProduct] )

UPDATE A SET 
A.serviceProducName= B.serviceProducName
,A.serviceProducCode= B.serviceProducCode
,A.isActive= B.isActive
,A.ModiDate= B.ModiDate
FROM [Loan].[serviceProduct] A 
INNER JOIN #_TBL_eadmtprm B ON A.serviceProductId=B.serviceProductId
DROP TABLE #_TBL_eadmtprm

/*LOANS.entity.entityRol*/
DECLARE @entityRolId INT =0 
SET @entityRolId = ISNULL((SELECT MAX(entityRolId) FROM entity.entityRol),0)
SELECT 
	 entityRolId=@entityRolId + ROW_NUMBER() OVER (ORDER BY admrol_codigo)
	,rolName=RTRIM(LTRIM(admrol_nombre))
	,rolAbbreviation=RTRIM(LTRIM(admrol_codigo))
	,ISBlock=admrol_indbloq
	,isActive=admsts_codigo
	,CreateDate=getdate()
	,ModiDate=getdate()
	,CreateUsrId=1
	,ModiUsrId=1
	,hostName=1
INTO #_TBL_xadmrolm
FROM [Easybank].dbo.xadmrolm

INSERT INTO entity.entityRol
(
	rolName
	,rolAbbreviation
	,ISBlock
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
)
SELECT 
	 rolName
	,rolAbbreviation
	,ISBlock
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
FROM #_TBL_xadmrolm A
WHERE rolAbbreviation NOT IN (SELECT rolAbbreviation FROM entity.entityRol)

UPDATE A SET 
 A.rolName=B.rolName
,A.rolAbbreviation=B.rolAbbreviation
,A.ISBlock=B.ISBlock
,A.isActive=B.isActive
,A.ModiDate=B.ModiDate
FROM entity.entityRol A 
INNER JOIN #_TBL_xadmrolm B ON A.rolAbbreviation=B.rolAbbreviation

DROP TABLE #_TBL_xadmrolm

/*INDENTIFICATION TYPE*/



select 
	 identificationTypeId=admide_codigo
	,IdentificationName=admide_nombre
	,IdentificationCode=admide_abrev
	,PersonType=admide_personeria
	,isActive=admsts_codigo
	,CreateDate=getdate()
	,ModiDate=getdate()
	,CreateUsrId=1
	,ModiUsrId=1
	,hostName=HOST_NAME()
	INTO #_TBL_xadmidem
from easybank.dbo.xadmidem

INSERT INTO entity.identificationType 
(
	 identificationTypeId
	,IdentificationName
	,IdentificationCode
	,PersonType
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostnName
)

SELECT  
	 identificationTypeId
	,IdentificationName
	,IdentificationCode
	,PersonType
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
FROM #_TBL_xadmidem
where identificationTypeId not in (select identificationTypeId from entity.identificationType )

UPDATE A SET 
	 A.IdentificationName =B.IdentificationName
	,A.IdentificationCode =B.IdentificationCode
	,A.PersonType		  =B.PersonType
	,A.isActive			  =B.isActive
	,A.ModiDate			  =B.ModiDate
FROM entity.identificationType A
INNER JOIN #_TBL_xadmidem B
 ON A.identificationTypeId=B.identificationTypeId
DROP TABLE #_TBL_xadmidem



/*founIncome*/


select 
	 companyId=admcia_codigo
	,founIncomeId=cast(admfue_codigo as int)
	,founIncomeName=admfue_nombre
	,founIncomeCode=admfue_codigo
	,isActive=1
	,CreateDate=getdate()
	,ModiDate=getdate()
	,CreateUsrId=1
	,ModiUsrId=1
	,hostName=1
INTO #_TBL_eadmfuem
from [Easybank].dbo.eadmfuem


INSERT INTO [global].[founIncome]
(
	 companyId
	,founIncomeId
	,founIncomeName
	,founIncomeCode
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
)

SELECT 
	 companyId
	,founIncomeId
	,founIncomeName
	,founIncomeCode
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
FROM #_TBL_eadmfuem
where founIncomeCode not in (select founIncomeCode from [global].[founIncome]  )


UPDATE A SET 
  A.founIncomeName=B.founIncomeName
 ,A.founIncomeCode=B.founIncomeCode
 ,A.isActive=B.isActive
 ,A.ModiDate=GETDATE()
FROM  [global].[founIncome] A 
INNER JOIN #_TBL_eadmfuem B 
ON A.founIncomeCode=B.founIncomeCode
DROP TABLE #_TBL_eadmfuem
/*CREDIT FACILITY*/
SELECT 
	 companyId=cast(admcia_codigo as int)
	,creditFacilityId=prefac_codigo
	,creditFacilityName=prefac_nombre
	,isActive=1
	,CreateDate=getdate()
	,ModiDate=getdate()
	,CreateUsrId=1
	,ModiUsrId=1
	,hostName=1
INTO #_TBL_eprefacm
FROM easybank.dbo.eprefacm

                                               
INSERT INTO [Loan].[creditFacility]
(
	 companyId
	,creditFacilityId
	,creditFacilityName
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
)

SELECT 
	 companyId
	,creditFacilityId
	,creditFacilityName
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
FROM #_TBL_eprefacm B
WHERE NOT EXISTS 
(
SELECT TOP 1 1 FROM [Loan].[creditFacility] A
WHERE 
		A.companyId=B.companyId
	AND A.creditFacilityId=B.creditFacilityId
)

UPDATE A SET 
 A.creditFacilityName=A.creditFacilityId
,A.isActive =B.isActive
,ModiDate=GETDATE()
FROM [Loan].[creditFacility] A 
INNER JOIN #_TBL_eprefacm B 
	ON A.companyId=B.companyId
	AND A.creditFacilityId=B.creditFacilityId


UPDATE A SET 
	 A.isActive =0
	,ModiDate=GETDATE()
FROM [Loan].[creditFacility] A 
WHERE NOT EXISTS 
(
	SELECT TOP 1 1 FROM #_TBL_eprefacm B
	WHERE 
			A.companyId=B.companyId
		AND A.creditFacilityId=B.creditFacilityId
)
DROP TABLE #_TBL_eprefacm

/*CONTACTO*/

select 
	 relatedContactId=a.admrel_codigo
	,countryID=a.admpai_codigo
	,entityRolId
	,identificationTypeId
	,entityClassId
	,personTypeId=null
	,maritalStatusId
	,professionId
	,Sex=admrel_sexo
	,FirstName =ISNULL(b.admrel_nombre1,'')
	,MiddleName=ISNULL(b.admrel_nombre2,'')
	,Lastname=ISNULL(b.admrel_apellido1,'')
	,SecondLastname=ISNULL(b.admrel_apellido2,'')
	,FullName=ISNULL(a.admrel_nombre,'')
	,Nickname=''
	,MarriedName=ISNULL(B.admrel_apelcasada,'')
	,Title=ISNULL(B.admrel_titulo,'')
	,IdentificationNumber=A.admide_numero
	,BirthDate=B.admrel_fecnac
	,isActive= A.admsts_codigo
	,CreateDate=getdate()
	,ModiDate=getdate()
	,CreateUsrId=1
	,ModiUsrId=1
	,hostName=1
INTO #_TBL_xadmrelm
from [Easybank].dbo.[xadmrelm] a
inner join   [Easybank].dbo.xadmrelr b 
on a.admrel_codigo =b.admrel_codigo

INNER JOIN  Entity.entityRol R ON R.rolAbbreviation =RTRIM(LTRIM(A.admrol_codigo))

left  JOIN entity.identificationType iden on iden.identificationTypeId=a.admide_codigo

left join entity.EntityClass cla on cla.ClassCode = a.admrel_clase
left join entity.MaritalStatus mar on mar.MaritalStatusCode = b.admrel_estciv
left join entity.Profession prop on prop.ProfessionId = b.admpro_codigo


WHERE b.admrel_codigo IS NOT NULL --and b.admrel_codigo not in (select  relatedContactId from [Entity].[relatedContact])

INSERT INTO [Entity].[relatedContact]
(
	 relatedContactId
	,countryID
	,entityRolId
	,identificationTypeId
	,entityClassId
	,personTypeId
	,maritalStatusId
	,professionId
	,Sex
	,FirstName
	,MiddleName
	,Lastname
	,SecondLastname
	,FullName
	,Nickname
	,MarriedName
	,Title
	,IdentificationNumber
	,BirthDate
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
)
SELECT 
	 relatedContactId
	,countryID
	,entityRolId
	,identificationTypeId
	,entityClassId
	,personTypeId
	,maritalStatusId
	,professionId
	,Sex
	,FirstName
	,MiddleName
	,Lastname
	,SecondLastname
	,FullName
	,Nickname
	,MarriedName
	,Title
	,IdentificationNumber
	,BirthDate
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
FROM #_TBL_xadmrelm A
WHERE NOT EXISTS 
(
	SELECT TOP 1 1 FROM [Entity].[relatedContact]B
	WHERE 
			A.relatedContactId=B.relatedContactId
)

UPDATE A SET 
	 A.relatedContactId			=B.relatedContactId		
	,A.countryID				=B.countryID			
	,A.entityRolId				=B.entityRolId			
	,A.identificationTypeId		=B.identificationTypeId	
	,A.entityClassId			=B.entityClassId		
	,A.personTypeId				=B.personTypeId			
	,A.maritalStatusId			=B.maritalStatusId		
	,A.professionId				=B.professionId			
	,A.Sex						=B.Sex					
	,A.FirstName				=B.FirstName			
	,A.MiddleName				=B.MiddleName			
	,A.Lastname					=B.Lastname				
	,A.SecondLastname			=B.SecondLastname		
	,A.FullName					=B.FullName				
	,A.Nickname					=B.Nickname				
	,A.MarriedName				=B.MarriedName			
	,A.Title					=B.Title				
	,A.IdentificationNumber		=B.IdentificationNumber	
	,A.BirthDate				=B.BirthDate			
	,A.isActive					=B.isActive				
	,A.ModiDate=GETDATE()
FROM [Entity].[relatedContact] A 
INNER JOIN #_TBL_xadmrelm B
	ON A.relatedContactId=B.relatedContactId



UPDATE A SET 
			
	 A.isActive=0
	,A.ModiDate=GETDATE()
FROM [Entity].[relatedContact] A 
WHERE NOT EXISTS 
(
	SELECT TOP 1 1 FROM #_TBL_xadmrelm B
	WHERE 
			A.relatedContactId=B.relatedContactId
)

DROP TABLE #_TBL_xadmrelm 
/*company*/

SELECT 
	 companyId=admcia_codigo
	,currencyId=admmon_codigo
	,relatedContactId=admrel_codigo
	,companyCode=admcia_codigo
	,companyName=admcia_nombre
	,rnc=admcia_rncno
	,companyHoldingId=admcia_codigo
	,isActive=admsts_codigo
	,CreateDate=getdate()
	,ModiDate=getdate()
	,CreateUsrId=1
	,ModiUsrId=1
	,hostName=1
into #_TBL_xadmciam
FROM  easybank.dbo.xadmciam


INSERT INTO Entity.Company
(
	 companyId
	,currencyId
	,relatedContactId
	,companyCode
	,companyName
	,rnc
	,companyHoldingId
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
)
select 
	 companyId
	,currencyId
	,relatedContactId
	,companyCode
	,companyName
	,rnc
	,companyHoldingId
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
from #_TBL_xadmciam a
WHERE  companyId NOT IN (SELECT companyId FROM Entity.Company)

update a set 
	 a.currencyId			=b.currencyId
	,a.relatedContactId		=b.relatedContactId
	,a.companyCode			=b.companyCode
	,a.companyName			=b.companyName
	,a.rnc					=b.rnc
	,a.companyHoldingId		=b.companyHoldingId
	,a.isActive				=b.isActive
	,a.ModiDate				=b.ModiDate
	,a.ModiUsrId			=b.ModiUsrId
	,a.hostName				=b.hostName
from Entity.Company a 
inner join #_TBL_xadmciam b on a.companyId=b.companyId

drop table #_TBL_xadmciam 


/*oficina*/

select 
 officeId=admsuc_codigo
,companyId=admcia_codigo
,relatedContactId=admrel_codigo
,officeName=rtrim(ltrim(admsuc_nombre))
,isActive=admsts_codigo
,CreateDate=getdate()
,ModiDate=getdate()
,CreateUsrId=1
,ModiUsrId=1
,hostName=1
into #_TBL_xadmsucm
from  easybank.dbo.xadmsucm

INSERT INTO [Entity].[office]
(
 officeId
,companyId
,relatedContactId
,officeName
,isActive
,CreateDate
,ModiDate
,CreateUsrId
,ModiUsrId
,hostName
)
select 
 officeId
,companyId
,relatedContactId
,officeName
,isActive
,CreateDate
,ModiDate
,CreateUsrId
,ModiUsrId
,hostName
from #_TBL_xadmsucm a
where officeId not in (select officeId from [Entity].[office])

update a set 
 a.companyId		=b.companyId
,a.relatedContactId	=b.relatedContactId
,a.officeName		=b.officeName
,a.isActive			=b.isActive
,a.ModiDate			=b.ModiDate
,a.ModiUsrId		=b.ModiUsrId
,a.hostName			=b.hostName
from [Entity].[office] a
inner join #_TBL_xadmsucm b on a.officeId=b.officeId

drop table #_TBL_xadmsucm 

/*commite*/


SELECT 
 companyId=admcia_codigo
,committeeId=cast(precom_codigo as int)
,currencyId=admmon_codigo
,committeeName=precom_nombre
,committeeCode=precom_codigo
,minimumAmount=precom_monmin
,MaximumAmount=precom_monmax
,numberUsertoApprove=2
,isActive=1
,CreateDate=getdate()
,ModiDate=getdate()
,CreateUsrId=1
,ModiUsrId=1
,hostName=1
into #_TBL_eprecomm
FROM [Easybank].dbo.eprecomm A 


INSERT INTO [Loan].[committee]
(
 companyId
,committeeId
,currencyId
,committeeName
,committeeCode
,minimumAmount
,MaximumAmount
,numberUsertoApprove
,isActive
,CreateDate
,ModiDate
,CreateUsrId
,ModiUsrId
,hostName
)
select 
 companyId
,committeeId
,currencyId
,committeeName
,committeeCode
,minimumAmount
,MaximumAmount
,numberUsertoApprove
,isActive
,CreateDate
,ModiDate
,CreateUsrId
,ModiUsrId
,hostName
from #_TBL_eprecomm a
WHERE NOT EXISTS 
(
	SELECT TOP 1 1 FROM [Loan].[committee] b
	WHERE 
	 a.companyId=b.companyId
 AND a.committeeId=b.committeeId
)

update a set 
 a.currencyId				=b.currencyId
,a.committeeName			=b.committeeName
,a.committeeCode			=b.committeeCode
,a.minimumAmount			=b.minimumAmount
,a.MaximumAmount			=b.MaximumAmount
,a.numberUsertoApprove		=b.numberUsertoApprove
,a.isActive					=b.isActive
,a.CreateDate				=b.CreateDate
,a.ModiDate					=b.ModiDate
,a.CreateUsrId				=b.CreateUsrId
,a.ModiUsrId				=b.ModiUsrId
,a.hostName					=b.hostName
from [Loan].[committee] a
inner join #_TBL_eprecomm b
on 	 a.companyId=b.companyId
 AND a.committeeId=b.committeeId

drop table #_TBL_eprecomm

/*CLIENTE*/
SELECT 
	 clientId=client_codigo
	,rc.relatedContactId
	,companyId
	,linkedTypeId
	,clientTypeByCompanyId
	,reasonId
	,clientTypeBySIBId
	,clientName=ISNULL(rc.FullName,client_nombre)
	,admissionDate=client_fecing
	,creditInformation=cast(client_infcredito as bit)
	,ClientReference=client_refer
	,InactiveComment=client_comentinact
	,numberDependents=client_numdepen
	,numberEmployees=client_empleados
	,AnnualSales=client_ventas
	,AssetValue=client_activos
	,dateIncorporation=client_fecconst
	,ownHouse=cast(client_casapropia as bit)
	,IsClientSentCollection =cast(client_gestcobros as bit)
	,depositType=client_depositante
	,TypeNCF=client_tiponcf
	,isActive = CASE WHEN admsts_codigo ='A' THEN 1 ELSE 0 END
	,CreateDate=getdate()
	,ModiDate=getdate()
	,CreateUsrId=1
	,ModiUsrId=1
	,hostName=1
INTO #_TBL_eclientm
FROM [Easybank].dbo.eclientm c 
INNER JOIN Entity.Company com on com.companyCode = rtrim(ltrim(admcia_codigo))
INNER JOIN [Entity].[linkedType] lt on lt.linkedTypeCode = RTRIM(LTRIM(clivin_codigo))
left JOIN Entity.clientTypeByCompany ctc on ctc.clientTypeByCompanyCode = RTRIM(LTRIM(clitip_codigo)) 
left JOIN Entity.clientTypeBySIB ctsib on ctsib.clientTypeBySIBId = clisib_codigo 
left JOIN [Entity].[relatedContact] rc on rc.relatedContactId=c.client_codigo
left join [global].[Reason] rea on rea.reasonId = admmot_numid

INSERT INTO [Entity].[client]
(
	 clientId
	,relatedContactId
	,companyId
	,linkedTypeId
	,clientTypeByCompanyId
	,reasonId
	,clientTypeBySIBId
	,clientName
	,admissionDate
	,creditInformation
	,ClientReference
	,InactiveComment
	,numberDependents
	,numberEmployees
	,AnnualSales
	,AssetValue
	,dateIncorporation
	,ownHouse
	,IsClientSentCollection
	,depositType
	,TypeNCF
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
)

SELECT 
	 clientId
	,relatedContactId
	,companyId
	,linkedTypeId
	,clientTypeByCompanyId
	,reasonId
	,clientTypeBySIBId
	,clientName
	,admissionDate
	,creditInformation
	,ClientReference
	,InactiveComment
	,numberDependents
	,numberEmployees
	,AnnualSales
	,AssetValue
	,dateIncorporation
	,ownHouse
	,IsClientSentCollection
	,depositType
	,TypeNCF
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
FROM #_TBL_eclientm A
WHERE NOT EXISTS 
(
	SELECT TOP 1 1 FROM [Entity].[client] B
	WHERE 
			A.clientId=B.clientId
)

UPDATE A SET 
	 A.clientId					=B.clientId
	,A.relatedContactId			=B.relatedContactId
	,A.companyId				=B.companyId
	,A.linkedTypeId				=B.linkedTypeId
	,A.clientTypeByCompanyId	=B.clientTypeByCompanyId
	,A.reasonId					=B.reasonId
	,A.clientTypeBySIBId		=B.clientTypeBySIBId
	,A.clientName				=B.clientName
	,A.admissionDate			=B.admissionDate
	,A.creditInformation		=B.creditInformation
	,A.ClientReference			=B.ClientReference
	,A.InactiveComment			=B.InactiveComment
	,A.numberDependents			=B.numberDependents
	,A.numberEmployees			=B.numberEmployees
	,A.AnnualSales				=B.AnnualSales
	,A.AssetValue				=B.AssetValue
	,A.dateIncorporation		=B.dateIncorporation
	,A.ownHouse					=B.ownHouse
	,A.IsClientSentCollection	=B.IsClientSentCollection
	,A.depositType				=B.depositType
	,A.TypeNCF					=B.TypeNCF
	,A.isActive					=B.isActive
	,A.ModiDate=GETDATE()
FROM [Entity].[client] A
INNER JOIN #_TBL_eclientm B
ON A.clientId=B.clientId

UPDATE A SET 

	 A.isActive=0
	,A.ModiDate=GETDATE()
FROM [Entity].[client] A
WHERE NOT EXISTS 
(
	SELECT TOP 1 1 FROM #_TBL_eclientm B
	WHERE 
			A.clientId=B.clientId
)


DROP TABLE #_TBL_eclientm

/*TELEFONOS*/
select 
	 contactPhoneId=phone.admtel_seq
	,c.relatedContactId
	,co.CountryID
	,AreaCode=admtel_area
	,Phone=admtel_telefono
	,PhoneType=admtel_tipo
	,Comments=admtel_coment
	,IsPrimary = case when prim.admrel_codigo  is not null then 1 else 0 end
	,isActive=1
	,CreateDate=getdate()
	,ModiDate=getdate()
	,CreateUsrId=1
	,ModiUsrId=1
	,hostName=1
 INTO #_TBL_xadmteld
from [Entity].[relatedContact] c
inner join [Easybank].dbo.xadmteld phone on phone.admrel_codigo =c.relatedContactId
inner join global.country co on co.CountryID=phone.admpai_codigo
LEFT JOIN 
(

	select  admrel_codigo,max(admtel_seq) admtel_seq from [Easybank].dbo.xadmrold
	where admrol_codigo='CLT' AND admtel_seq IS NOT NULL 
	group by admrel_codigo
) prim on prim.admrel_codigo =phone.admrel_codigo and  prim.admtel_seq=phone.admtel_seq 



INSERT INTO [Entity].[contactPhones]
(
	 contactPhoneId
	,relatedContactId
	,CountryID
	,AreaCode
	,Phone
	,PhoneType
	,Comments
	,IsPrimary
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
)

SELECT 
	 contactPhoneId
	,relatedContactId
	,CountryID
	,AreaCode
	,Phone
	,PhoneType
	,Comments
	,IsPrimary
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
FROM #_TBL_xadmteld A
WHERE NOT EXISTS 
(
	SELECT  TOP 1 1  FROM [Entity].[contactPhones]  B 
	WHERE 
				A.contactPhoneId = B.contactPhoneId
			AND A.relatedContactId =B.relatedContactId
)


UPDATE A SET 
	 A.CountryID	=B.CountryID
	,A.AreaCode		=B.AreaCode
	,A.Phone		=B.Phone
	,A.PhoneType	=B.PhoneType
	,A.Comments		=B.Comments
	,A.IsPrimary	=B.IsPrimary
	,A.isActive		=B.isActive
	,A.ModiDate=GETDATE()
	
FROM [Entity].[contactPhones]  A
INNER JOIN #_TBL_xadmteld B 
	ON A.contactPhoneId = B.contactPhoneId
			AND A.relatedContactId =B.relatedContactId



UPDATE A SET 
	 A.isActive=0
	,A.ModiDate=GETDATE()
	
FROM [Entity].[contactPhones]  A
WHERE NOT EXISTS 
(
	SELECT  TOP 1 1  FROM #_TBL_xadmteld  B 
	WHERE 
				A.contactPhoneId = B.contactPhoneId
			AND A.relatedContactId =B.relatedContactId
)

DROP TABLE #_TBL_xadmteld


/*CORREOS*/

select 
	 contactEmailId=email.admurl_seq
	,relatedContactId
	,email=admurl_nombre
	,emailType=admurl_tipo
	,comments=admurl_coment
	,IsPrimary= case when prim.admrel_codigo  is not null then 1 else 0 end
	,isActive=1
	,CreateDate=getdate()
	,ModiDate=getdate()
	,CreateUsrId=1
	,ModiUsrId=1
	,hostName=1
INTO #_TBL_xadmurld
from [Entity].[relatedContact] c
inner join [Easybank].dbo.xadmurld email on email.admrel_codigo =c.relatedContactId
LEFT JOIN 
(

	select  admrel_codigo,max(admurl_seq) admurl_seq from [Easybank].dbo.xadmrold
	where admrol_codigo='CLT' AND admtel_seq IS NOT NULL 
	group by admrel_codigo
) prim on prim.admrel_codigo =email.admrel_codigo and  prim.admurl_seq=email.admurl_seq 




INSERT INTO [Entity].[contactEmail]
(
	 contactEmailId
	,relatedContactId
	,email
	,emailType
	,comments
	,IsPrimary
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
)

SELECT 
	 contactEmailId
	,relatedContactId
	,email
	,emailType
	,comments
	,IsPrimary
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
FROM #_TBL_xadmurld A
WHERE NOT EXISTS 
(
	SELECT  TOP 1 1  FROM [Entity].[contactEmail]  B 
	WHERE 
				A.contactEmailId = B.contactEmailId
			AND A.relatedContactId =B.relatedContactId
)

DROP TABLE #_TBL_xadmurld
/*DIRECCIONES*/

SELECT 
 addressId=addr.admdir_seq
,relatedContactId
,CountryID=admpai_codigo
,provinceId=admprv_codigo
,cityId=admciu_codigo
,physicalSectorId=admset_codigo
,addressType=sdauso_codigo
,streetName=admdir_calle
,streetNumber=admdir_numero
,buildingName=admdir_edificio
,postalCode=admdir_apapost
,postalZone=admdir_zonapost
,address=admdir_adicional
,addressAdditional=admdir_cust01
,IsPrimary= case when prim.admrel_codigo  is not null then 1 else 0 end
,isActive=1
,CreateDate=getdate()
,ModiDate=getdate()
,CreateUsrId=1
,ModiUsrId=1
,hostName=1
INTO #_TBL_xadmdird
FROM [Entity].[relatedContact] c
inner join [Easybank].dbo.xadmdird addr on addr.admrel_codigo =c.relatedContactId
LEFT JOIN 
(

	select  admrel_codigo,max(admdir_seq) admdir_seq from [Easybank].dbo.xadmrold
	where admrol_codigo='CLT' AND admtel_seq IS NOT NULL 
	group by admrel_codigo
) prim on prim.admrel_codigo =addr.admrel_codigo and  prim.admdir_seq=addr.admdir_seq 


INSERT INTO [Entity].[contactAddress]
(
 addressId
,relatedContactId
,CountryID
,provinceId
,cityId
,physicalSectorId
,addressType
,streetName
,streetNumber
,buildingName
,postalCode
,postalZone
,address
,addressAdditional
,IsPrimary
,isActive
,CreateDate
,ModiDate
,CreateUsrId
,ModiUsrId
,hostName
)

SELECT
 addressId
,relatedContactId
,CountryID
,provinceId
,cityId
,physicalSectorId
,addressType
,streetName
,streetNumber
,buildingName
,postalCode
,postalZone
,address
,addressAdditional
,IsPrimary
,isActive
,CreateDate
,ModiDate
,CreateUsrId
,ModiUsrId
,hostName
FROM #_TBL_xadmdird A
WHERE NOT EXISTS 
(
	SELECT  TOP 1 1  FROM [Entity].[contactAddress]  B 
	WHERE 
				A.addressId = B.addressId 
			AND A.relatedContactId =B.relatedContactId
)


UPDATE A SET 

 A.CountryID				=B.CountryID
,A.provinceId				=B.provinceId
,A.cityId					=B.cityId
,A.physicalSectorId			=B.physicalSectorId
,A.addressType				=B.addressType
,A.streetName				=B.streetName
,A.streetNumber				=B.streetNumber
,A.buildingName				=B.buildingName
,A.postalCode				=B.postalCode
,A.postalZone				=B.postalZone
,A.address					=B.address
,A.addressAdditional		=B.addressAdditional
,A.IsPrimary				=B.IsPrimary
,A.isActive					=B.isActive
,A.ModiDate=GETDATE()
FROM  [Entity].[contactAddress] A
INNER JOIN #_TBL_xadmdird B 
ON		A.addressId = B.addressId 
			AND A.relatedContactId =B.relatedContactId

DROP TABLE #_TBL_xadmdird

/*COTIZACIONES*/

SELECT 

 quotationId=presol_numero
,relatedContactId=client_codigo
,addressId=admdir_seq
,companyId=admcia_codigo
,officeId=admsuc_codigo
,ConceptTypeId=admcon_codigo
,destinationFundId=predes_codigo
,quotationPaymentTypeId=presol_formapago
,currencyId=admmon_codigo
,quotationStatusId=admsts_codigo
,committeeId=precom_codigo
,accountExecutiveContactId=eje.relatedContactId
,accountPromoterContactId=pro.relatedContactId
,financialSectorId=admset_numid
,SourceIncomeId=prefue_codigo
,founIncomeId=admfue_codigo
,serviceProductId=admtpr_numid
,quotationTypeId=presol_tipo
,businessLineId=admlin_codigo
,amount=presol_monto
,qoutationDate=presol_fecha
,qoutationDocDate=presol_fechadoc
,loanTerm=presol_plazo
,frequency=presol_frecuencia
,TypeDisbursement=presol_tipdesem
,IsJointLoan=presol_indmanco
,rateLate=presol_tasamora
,comment=presol_comments
,clientName=presol_nombre
,creditFacility=presol_faccre
,quotaAmount=presol_cuota
,grace=presol_tipgracia
,graceDays=presol_diagracia
,statementDate=presol_diacorte
,SendStatementAccount=presol_indenvio
,AutomaticPayment=presol_indpagoaut
,RejectDate=presol_fecrechazo
,MothToChangePrice=presol_reprecio
,NumberOfReview=presol_revision
,RateType=presol_tipotasa
,OriginCredit=presol_origcred
,Rate=presol_tasaint
,RateCommission=presol_tasacom
,isActive=1
,CreateDate=getdate()
,ModiDate=getdate()
,CreateUsrId=1
,ModiUsrId=1
,hostName=1
INTO #_TBL_epresolm
FROM [Easybank].dbo.epresolm
left join [Entity].[relatedContact] eje on eje.relatedContactId  =admeje_codigo
left join [Entity].[relatedContact] pro on pro.relatedContactId  =admpro_codigo
where client_codigo in (select  relatedContactId from [Entity].[relatedContact])
--and presol_numero not in (select   quotationId from [Loan].[quotation])



INSERT INTO [Loan].[quotation]
(
 quotationId
,relatedContactId
,addressId
,companyId
,officeId
,ConceptTypeId
,destinationFundId
,quotationPaymentTypeId
,currencyId
,quotationStatusId
,committeeId
,accountExecutiveContactId
,accountPromoterContactId
,financialSectorId
,SourceIncomeId
,founIncomeId
,serviceProductId
,quotationTypeId
,businessLineId
,amount
,qoutationDate
,qoutationDocDate
,loanTerm
,frequency
,TypeDisbursement
,IsJointLoan
,rateLate
,comment
,clientName
,creditFacility
,quotaAmount
,grace
,graceDays
,statementDate
,SendStatementAccount
,AutomaticPayment
,RejectDate
,MothToChangePrice
,NumberOfReview
,RateType
,OriginCredit
,Rate
,RateCommission
,isActive
,CreateDate
,ModiDate
,CreateUsrId
,ModiUsrId
,hostName
)

SELECT 
 quotationId
,relatedContactId
,addressId
,companyId
,officeId
,ConceptTypeId
,destinationFundId
,quotationPaymentTypeId
,currencyId
,quotationStatusId
,committeeId
,accountExecutiveContactId
,accountPromoterContactId
,financialSectorId
,SourceIncomeId
,founIncomeId
,serviceProductId
,quotationTypeId
,businessLineId
,amount
,qoutationDate
,qoutationDocDate
,loanTerm
,frequency
,TypeDisbursement
,IsJointLoan
,rateLate
,comment
,clientName
,creditFacility
,quotaAmount
,grace
,graceDays
,statementDate
,SendStatementAccount
,AutomaticPayment
,RejectDate
,MothToChangePrice
,NumberOfReview
,RateType
,OriginCredit
,Rate
,RateCommission
,isActive
,CreateDate
,ModiDate
,CreateUsrId
,ModiUsrId
,hostName
FROM #_TBL_epresolm A 
WHERE NOT EXISTS 
(
	SELECT TOP 1 1 FROM  [Loan].[quotation] B
	WHERE A.quotationId=B.quotationId
)

UPDATE A SET 
 A.quotationId					=B.quotationId
,A.relatedContactId				=B.relatedContactId
,A.addressId					=B.addressId
,A.companyId					=B.companyId
,A.officeId						=B.officeId
,A.ConceptTypeId				=B.ConceptTypeId
,A.destinationFundId			=B.destinationFundId
,A.quotationPaymentTypeId		=B.quotationPaymentTypeId
,A.currencyId					=B.currencyId
,A.quotationStatusId			=B.quotationStatusId
,A.committeeId					=B.committeeId
,A.accountExecutiveContactId	=B.accountExecutiveContactId
,A.accountPromoterContactId		=B.accountPromoterContactId
,A.financialSectorId			=B.financialSectorId
,A.SourceIncomeId				=B.SourceIncomeId
,A.founIncomeId					=B.founIncomeId
,A.serviceProductId				=B.serviceProductId
,A.quotationTypeId				=B.quotationTypeId
,A.businessLineId				=B.businessLineId
,A.amount						=B.amount
,A.qoutationDate				=B.qoutationDate
,A.qoutationDocDate				=B.qoutationDocDate
,A.loanTerm						=B.loanTerm
,A.frequency					=B.frequency
,A.TypeDisbursement				=B.TypeDisbursement
,A.IsJointLoan					=B.IsJointLoan
,A.rateLate						=B.rateLate
,A.comment						=B.comment
,A.clientName					=B.clientName
,A.creditFacility				=B.creditFacility
,A.quotaAmount					=B.quotaAmount
,A.grace						=B.grace
,A.graceDays					=B.graceDays
,A.statementDate				=B.statementDate
,A.SendStatementAccount			=B.SendStatementAccount
,A.AutomaticPayment				=B.AutomaticPayment
,A.RejectDate					=B.RejectDate
,A.MothToChangePrice			=B.MothToChangePrice
,A.NumberOfReview				=B.NumberOfReview
,A.RateType						=B.RateType
,A.OriginCredit					=B.OriginCredit
,A.Rate							=B.Rate
,A.RateCommission				=B.RateCommission
,A.isActive						=B.isActive
,A.ModiDate=GETDATE()
FROM [Loan].[quotation] A
INNER JOIN #_TBL_epresolm B
ON A.quotationId=B.quotationId

UPDATE A SET 

  A.ModiDate=GETDATE()
 ,A.isActive=0
FROM [Loan].[quotation] A
WHERE NOT EXISTS 
(
	SELECT TOP 1 1 FROM  #_TBL_epresolm B
	WHERE A.quotationId=B.quotationId
)

DROP TABLE #_TBL_epresolm

/*CUENTAS*/

select 
 accountId=cuecta_numid
,companyId=admcia_codigo
,officeId=admsuc_codigo
,ConceptTypeId=admcon_codigo
,at.accountTypeID
,currencyId=admmon_codigo
,relatedContactId=client_codigo
,addressId=admdir_seq
,accountStatusId=admsts_codigo
,founIncomeId=admfue_codigo
,accountExecutiveContactId=eje.relatedContactId
,accountPromoterContactId=pro.relatedContactId
,financialSectorId=admset_numid
,serviceProductId=admtpr_numid
,accountName=cuecta_nombre
,userName=admusr_codigo
,SendStatementAccount=cuecta_indenvio
,IsJointLoan=cuecta_indmanco
,RateType=cuecta_tipotasa
,account=cuecta_formateado
,isActive=1
,CreateDate=getdate()
,ModiDate=getdate()
,CreateUsrId=1
,ModiUsrId=1
,hostName=1
INTO #_TBL_ecuectam
from [Easybank].dbo.ecuectam a
inner join [global].[accountType] at on at.accountTypeCode = rtrim(ltrim(admtcu_codigo))
left join [Entity].[relatedContact] eje on eje.relatedContactId  =admeje_codigo
left join [Entity].[relatedContact] pro on pro.relatedContactId  =admpro_codigo
where client_codigo in (select  relatedContactId from [Entity].[relatedContact])
--and cuecta_numid not in (select   accountId from [Loan].[Account])



INSERT INTO [Loan].[Account]
(
 accountId
,companyId
,officeId
,ConceptTypeId
,accountTypeID
,currencyId
,relatedContactId
,addressId
,accountStatusId
,founIncomeId
,accountExecutiveContactId
,accountPromoterContactId
,financialSectorId
,serviceProductId
,accountName
,userName
,SendStatementAccount
,IsJointLoan
,RateType
,account
,isActive
,CreateDate
,ModiDate
,CreateUsrId
,ModiUsrId
,hostName

)
SELECT 
 accountId
,companyId
,officeId
,ConceptTypeId
,accountTypeID
,currencyId
,relatedContactId
,addressId
,accountStatusId
,founIncomeId
,accountExecutiveContactId
,accountPromoterContactId
,financialSectorId
,serviceProductId
,accountName
,userName
,SendStatementAccount
,IsJointLoan
,RateType
,account
,isActive
,CreateDate
,ModiDate
,CreateUsrId
,ModiUsrId
,hostName
FROM #_TBL_ecuectam A
WHERE NOT EXISTS 
(
	SELECT TOP 1 1 FROM  [Loan].[Account] B
	WHERE A.accountId=B.accountId
)

UPDATE A SET 
 A.accountId					 =B.accountId
,A.companyId					 =B.companyId
,A.officeId						 =B.officeId
,A.ConceptTypeId				 =B.ConceptTypeId
,A.accountTypeID				 =B.accountTypeID
,A.currencyId					 =B.currencyId
,A.relatedContactId				 =B.relatedContactId
,A.addressId					 =B.addressId
,A.accountStatusId				 =B.accountStatusId
,A.founIncomeId					 =B.founIncomeId
,A.accountExecutiveContactId	 =B.accountExecutiveContactId
,A.accountPromoterContactId		 =B.accountPromoterContactId
,A.financialSectorId			 =B.financialSectorId
,A.serviceProductId				 =B.serviceProductId
,A.accountName					 =B.accountName
,A.userName						 =B.userName
,A.SendStatementAccount			 =B.SendStatementAccount
,A.IsJointLoan					 =B.IsJointLoan
,A.RateType						 =B.RateType
,A.account						 =B.account
,A.isActive						 =B.isActive
,ModiDate=GETDATE()
FROM [Loan].[Account] A
INNER JOIN #_TBL_ecuectam B
ON A.accountId=B.accountId


UPDATE A SET 
 
A.isActive=0
,ModiDate=GETDATE()
FROM [Loan].[Account] A
WHERE NOT EXISTS 
(
	SELECT TOP 1 1 FROM  #_TBL_ecuectam B
	WHERE A.accountId=B.accountId
)
DROP TABLE #_TBL_ecuectam

/*PRESTAMOS*/
select 
 accountId=cuecta_numid
,companyId=admcia_codigo
,officeId=admsuc_codigo
,quotationId=presol_numero
,ConceptTypeId=admcon_codigo
,relatedContactId=client_codigo
,founIncomeId=admfue_codigo
,businessLineId
,destinationFundId=predes_codigo
,PaymentTypeId=prepre_formapago
,committeeId=precom_codigo
,currencyId=admmon_codigo
,LoanStatusId=admsts_codigo
,SourceIncomeId=prefue_codigo
,lineCreditId=cuelin_numid
,creditFacilityId=prefac_codigo
,LastQuotaDate=prepre_ultcuota
,loanNumber=prepre_numero
,CheckDigit=prepre_digito
,Rate=prepre_tasaint
,RateCommission=prepre_tasacom
,rateLate=prepre_tasamora
,codeCiiu=preciu_codigo
,debtorClassification=predeu_codigo
,numberExtension=prepre_numprorro
,LastClosedDate=prepre_fecultcie
,PaidOffDate=prepre_fecsaldo
,expirationDate=prepre_fecvenc
,amountApproved=prepre_montoaprob
,ApprovedDate=prepre_fecaproba
,OutstandingBalance=prepre_saldo
,AutomaticPayment=prepre_indpagoaut
,DisbursedAmount=prepre_desemb
,SendStatementAccount=prepre_envio
,loanTerm=prepre_plazo
,QuotaAmount=prepre_cuota
,LoanDillingDay=prepre_diacorte
,ISCapitalInDays =CASE WHEN prepre_frecuencia =1 THEN 1 ELSE 0 END 
,TypeDisbursement=prepre_tipdesemb
,grace=prepre_tipgracia
,graceDays=prepre_diagracia
,Comment=prepre_coment
,IsJointLoan=prepre_indmanco
,disbursementDate=prepre_fecha
,accountName=cuecta_nombre
,qoutationReference=prepre_numdoc
,financedAmount=prepre_monto
,expensesAmount=prepre_gastosaprob
,hasCollateral=prepre_garantia
,ClosedDate=prepre_feccierre
,TypeExpiration=prepre_vencint
,creditOrigin=prepre_origcred
,CreateDate=getdate()
,ModiDate=getdate()
,CreateUsrId=1
,ModiUsrId=1
,hostName='RAA'
INTO #_TBL_epreprem
from [Easybank].dbo.epreprem
INNER JOIN [global].[businessLine] ON [businessLineCode]=pretip_codigo
where 
	cuecta_numid  in (select   accountId from [Loan].[Account])
and client_codigo in (select  relatedContactId from [Entity].[relatedContact])


INSERT INTO [Loan].[loanHeader]
(
	 accountId
	,companyId
	,officeId
	,quotationId
	,ConceptTypeId
	,relatedContactId
	,founIncomeId
	,businessLineId
	,destinationFundId
	,PaymentTypeId
	,committeeId
	,currencyId
	,LoanStatusId
	,SourceIncomeId
	,lineCreditId
	,creditFacilityId
	,LastQuotaDate
	,loanNumber
	,CheckDigit
	,Rate
	,RateCommission
	,rateLate
	,codeCiiu
	,debtorClassification
	,numberExtension
	,LastClosedDate
	,PaidOffDate
	,expirationDate
	,amountApproved
	,ApprovedDate
	,OutstandingBalance
	,AutomaticPayment
	,DisbursedAmount
	,SendStatementAccount
	,loanTerm
	,QuotaAmount
	,LoanDillingDay
	,ISCapitalInDays
	,TypeDisbursement
	,grace
	,graceDays
	,Comment
	,IsJointLoan
	,disbursementDate
	,accountName
	,qoutationReference
	,financedAmount
	,expensesAmount
	,hasCollateral
	,ClosedDate
	,TypeExpiration
	,creditOrigin
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
)
	 
SELECT 
	 accountId
	,companyId
	,officeId
	,quotationId
	,ConceptTypeId
	,relatedContactId
	,founIncomeId
	,businessLineId
	,destinationFundId
	,PaymentTypeId
	,committeeId
	,currencyId
	,LoanStatusId
	,SourceIncomeId
	,lineCreditId
	,creditFacilityId
	,LastQuotaDate
	,loanNumber
	,CheckDigit
	,Rate
	,RateCommission
	,rateLate
	,codeCiiu
	,debtorClassification
	,numberExtension
	,LastClosedDate
	,PaidOffDate
	,expirationDate
	,amountApproved
	,ApprovedDate
	,OutstandingBalance
	,AutomaticPayment
	,DisbursedAmount
	,SendStatementAccount
	,loanTerm
	,QuotaAmount
	,LoanDillingDay
	,ISCapitalInDays
	,TypeDisbursement
	,grace
	,graceDays
	,Comment
	,IsJointLoan
	,disbursementDate
	,accountName
	,qoutationReference
	,financedAmount
	,expensesAmount
	,hasCollateral
	,ClosedDate
	,TypeExpiration
	,creditOrigin
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
FROM #_TBL_epreprem A
WHERE NOT EXISTS 
(
	SELECT TOP 1 1 FROM [Loan].[loanHeader] B
	WHERE A.accountId=B.accountId
)


UPDATE A SET 
	 A.accountId					=B.accountId
	,A.companyId					=B.companyId
	,A.officeId						=B.officeId
	,A.quotationId					=B.quotationId
	,A.ConceptTypeId				=B.ConceptTypeId
	,A.relatedContactId				=B.relatedContactId
	,A.founIncomeId					=B.founIncomeId
	,A.businessLineId				=B.businessLineId
	,A.destinationFundId			=B.destinationFundId
	,A.PaymentTypeId				=B.PaymentTypeId
	,A.committeeId					=B.committeeId
	,A.currencyId					=B.currencyId
	,A.LoanStatusId					=B.LoanStatusId
	,A.SourceIncomeId				=B.SourceIncomeId
	,A.lineCreditId					=B.lineCreditId
	,A.creditFacilityId				=B.creditFacilityId
	,A.LastQuotaDate				=B.LastQuotaDate
	,A.loanNumber					=B.loanNumber
	,A.CheckDigit					=B.CheckDigit
	,A.Rate							=B.Rate
	,A.RateCommission				=B.RateCommission
	,A.rateLate						=B.rateLate
	,A.codeCiiu						=B.codeCiiu
	,A.debtorClassification			=B.debtorClassification
	,A.numberExtension				=B.numberExtension
	,A.LastClosedDate				=B.LastClosedDate
	,A.PaidOffDate					=B.PaidOffDate
	,A.expirationDate				=B.expirationDate
	,A.amountApproved				=B.amountApproved
	,A.ApprovedDate					=B.ApprovedDate
	,A.OutstandingBalance			=B.OutstandingBalance
	,A.AutomaticPayment				=B.AutomaticPayment
	,A.DisbursedAmount				=B.DisbursedAmount
	,A.SendStatementAccount			=B.SendStatementAccount
	,A.loanTerm						=B.loanTerm
	,A.QuotaAmount					=B.QuotaAmount
	,A.LoanDillingDay				=B.LoanDillingDay
	,A.ISCapitalInDays				=B.ISCapitalInDays
	,A.TypeDisbursement				=B.TypeDisbursement
	,A.grace						=B.grace
	,A.graceDays					=B.graceDays
	,A.Comment						=B.Comment
	,A.IsJointLoan					=B.IsJointLoan
	,A.disbursementDate				=B.disbursementDate
	,A.accountName					=B.accountName
	,A.qoutationReference			=B.qoutationReference
	,A.financedAmount				=B.financedAmount
	,A.expensesAmount				=B.expensesAmount
	,A.hasCollateral				=B.hasCollateral
	,A.ClosedDate					=B.ClosedDate
	,A.TypeExpiration				=B.TypeExpiration
	,A.creditOrigin					=B.creditOrigin
	,A.ModiDate=GETDATE()
FROM [Loan].[loanHeader] A
INNER JOIN #_TBL_epreprem B 
ON A.accountId=B.accountId


DROP TABLE #_TBL_epreprem


/*PRESTAMO INDICADORES*/


SELECT 
 accountId=cuecta_numid
,TypeLoanPaymentId=prepre_indsaldo
,reasonId=admmot_numid
,quotaId=prepre_indrpc
,IsLegal=prepre_indlegal
,IsSuspense=prepre_indsuspenso
,IsRenewal=prepre_indrenov
,ISLoanTransferred=prepre_indtrasp
,IsJudicialCollection=preind_cobjudicial
,IsLegalTransunion=prepre_TUIndlegal
,monthToRepriced=preind_reprecio
,disbursementDate=prepre_fecdesemb
,LastDateRepriced=preind_ultrev
,LastQuotaGenerated=preind_ult_cuotagen
,flexibilityCode=prepre_flexnorm

,nextReviewRate=preind_fecrev
,datePaidoff=prepre_fecsaldo
,lastPaidDate=prepre_ultdev
,CreateDate=getdate()
,ModiDate=getdate()
,CreateUsrId=1
,ModiUsrId=1
,hostName=1
INTO #_TBL_epreindm
FROM [Easybank].dbo.epreindm
where 
	 cuecta_numid  in (select   accountId from [Loan].[Account])


INSERT INTO [Loan].[LoanIndicators]
(
 accountId
,TypeLoanPaymentId
,reasonId
,quotaId
,IsLegal
,IsSuspense
,IsRenewal
,ISLoanTransferred
,IsJudicialCollection
,IsLegalTransunion
,monthToRepriced
,disbursementDate
,LastDateRepriced
,LastQuotaGenerated
,flexibilityCode
,nextReviewRate
,datePaidoff
,lastPaidDate
,CreateDate
,ModiDate
,CreateUsrId
,ModiUsrId
,hostName
)

SELECT 
 accountId
,TypeLoanPaymentId
,reasonId
,quotaId
,IsLegal
,IsSuspense
,IsRenewal
,ISLoanTransferred
,IsJudicialCollection
,IsLegalTransunion
,monthToRepriced
,disbursementDate
,LastDateRepriced
,LastQuotaGenerated
,flexibilityCode
,nextReviewRate
,datePaidoff
,lastPaidDate
,CreateDate
,ModiDate
,CreateUsrId
,ModiUsrId
,hostName
FROM #_TBL_epreindm A
WHERE NOT EXISTS 
(
	SELECT TOP 1 1 FROM [Loan].[LoanIndicators] B
	WHERE A.accountId=B.accountId
)

UPDATE A SET 
 A.accountId				=B.accountId
,A.TypeLoanPaymentId		=B.TypeLoanPaymentId
,A.reasonId					=B.reasonId
,A.quotaId					=B.quotaId
,A.IsLegal					=B.IsLegal
,A.IsSuspense				=B.IsSuspense
,A.IsRenewal				=B.IsRenewal
,A.ISLoanTransferred		=B.ISLoanTransferred
,A.IsJudicialCollection		=B.IsJudicialCollection
,A.IsLegalTransunion		=B.IsLegalTransunion
,A.monthToRepriced			=B.monthToRepriced
,A.disbursementDate			=B.disbursementDate
,A.LastDateRepriced			=B.LastDateRepriced
,A.LastQuotaGenerated		=B.LastQuotaGenerated
,A.flexibilityCode			=B.flexibilityCode
,A.nextReviewRate			=B.nextReviewRate
,A.datePaidoff				=B.datePaidoff
,A.lastPaidDate				=B.lastPaidDate
,A.ModiDate					=B.ModiDate
FROM [Loan].[LoanIndicators] A
INNER JOIN #_TBL_epreindm B 
ON A.accountId=B.accountId


DROP TABLE #_TBL_epreindm

/*ConceptGeneralParameter*/

select 
	 companyId=admcia_codigo
	,ConceptTypeId=admcon_codigo
	,ct.creditTypeId
	,minAmountApproval=precon_minaprob
	,maxAmountApproval=precon_maxaprob
	,minLoanTerm=precon_plazomin
	,maxLoanTerm=precon_plazomax
	,maxGraceDays=precon_maxgramor
	,condonationRate=precon_condint
	,condonationRateLate=precon_condmora
	,IsLoanTermInDays=case when precon_senalplazo = 1 then 1 else 0 end
	,condonationCommission=precon_condcomis
	,initialCommissionPercent=precon_porccom1
	,EndCommissionPercent=precon_porccom2
	,initialRateLate=precon_porcmor1
	,endRateLate=precon_porcmor2
	,HasCollateral=precon_garantia
	,RateCalculation=precon_formacalint
	,RateLateCalculation=precon_formacalmor
	,LoanBillingDay=precon_diacorte
	,CreateDate=getdate()
	,ModiDate=getdate()
	,CreateUsrId=1
	,ModiUsrId=1
	,hostName=1
INTO #_TBL_epreconm
from [Easybank].dbo.epreconm a
inner join [Loan].[creditType] ct on ct.creditTypeCode= pretip_codigo


INSERT INTO [Loan].[ConceptGeneralParameter]
(
	 companyId
	,ConceptTypeId
	,creditTypeId
	,minAmountApproval
	,maxAmountApproval
	,minLoanTerm
	,maxLoanTerm
	,maxGraceDays
	,condonationRate
	,condonationRateLate
	,IsLoanTermInDays
	,condonationCommission
	,initialCommissionPercent
	,EndCommissionPercent
	,initialRateLate
	,endRateLate
	,HasCollateral
	,RateCalculation
	,RateLateCalculation
	,LoanBillingDay
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
)

SELECT 
	 companyId
	,ConceptTypeId
	,creditTypeId
	,minAmountApproval
	,maxAmountApproval
	,minLoanTerm
	,maxLoanTerm
	,maxGraceDays
	,condonationRate
	,condonationRateLate
	,IsLoanTermInDays
	,condonationCommission
	,initialCommissionPercent
	,EndCommissionPercent
	,initialRateLate
	,endRateLate
	,HasCollateral
	,RateCalculation
	,RateLateCalculation
	,LoanBillingDay
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
FROM #_TBL_epreconm A
where not exists 
(
	select  top 1 1 from [Loan].[ConceptGeneralParameter] b
	where 	 A.companyId=b.companyId
	and A.ConceptTypeId=b.ConceptTypeId

)

UPDATE A SET 
	 A.companyId					=B.companyId
	,A.ConceptTypeId				=B.ConceptTypeId
	,A.creditTypeId					=B.creditTypeId
	,A.minAmountApproval			=B.minAmountApproval
	,A.maxAmountApproval			=B.maxAmountApproval
	,A.minLoanTerm					=B.minLoanTerm
	,A.maxLoanTerm					=B.maxLoanTerm
	,A.maxGraceDays					=B.maxGraceDays
	,A.condonationRate				=B.condonationRate
	,A.condonationRateLate			=B.condonationRateLate
	,A.IsLoanTermInDays				=B.IsLoanTermInDays
	,A.condonationCommission		=B.condonationCommission
	,A.initialCommissionPercent		=B.initialCommissionPercent
	,A.EndCommissionPercent			=B.EndCommissionPercent
	,A.initialRateLate				=B.initialRateLate
	,A.endRateLate					=B.endRateLate
	,A.HasCollateral				=B.HasCollateral
	,A.RateCalculation				=B.RateCalculation
	,A.RateLateCalculation			=B.RateLateCalculation
	,A.LoanBillingDay				=B.LoanBillingDay
	,A.ModiDate						=B.ModiDate
FROM [Loan].[ConceptGeneralParameter]  A
INNER JOIN #_TBL_epreconm B 
	ON A.companyId=b.companyId
	and A.ConceptTypeId=b.ConceptTypeId


DROP TABLE #_TBL_epreconm


/*[lineCredit]*/

SELECT 
	 lineCreditId=cuelin_numid
	,companyId=cuelin_numid
	,currencyId=admmon_codigo
	,lineCreditType=cuelin_tipo
	,referenceId=cuelin_refer
	,clientId=client_codigo
	,Amount=cuelin_monto
	,expirationDate=cuelin_fecvenc
	,isActive=admsts_codigo
	,CreateDate=getdate()
	,ModiDate=getdate()
	,CreateUsrId=1
	,ModiUsrId=1
	INTO #_TBL_ecuelinm
FROM [Easybank].dbo.ecuelinm


INSERT INTO [Entity].[lineCredit]
(
	 lineCreditId
	,companyId
	,currencyId
	,lineCreditType
	,referenceId
	,clientId
	,Amount
	,expirationDate
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
)
SELECT 
	 lineCreditId
	,companyId
	,currencyId
	,lineCreditType
	,referenceId
	,clientId
	,Amount
	,expirationDate
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
FROM  #_TBL_ecuelinm
WHERE  lineCreditId NOT IN (SELECT lineCreditId FROM [Entity].[lineCredit])

UPDATE A SET 
	 A.lineCreditId		=B.lineCreditId
	,A.companyId		=B.companyId
	,A.currencyId		=B.currencyId
	,A.lineCreditType	=B.lineCreditType
	,A.referenceId		=B.referenceId
	,A.clientId			=B.clientId
	,A.Amount			=B.Amount
	,A.expirationDate	=B.expirationDate
	,A.isActive			=B.isActive
	,A.ModiDate			=B.ModiDate
	,A.ModiUsrId		=B.ModiUsrId
FROM  [Entity].[lineCredit] A
INNER JOIN #_TBL_ecuelinm B  ON A.companyId=B.companyId AND A.lineCreditId=B.lineCreditId

DROP TABLE #_TBL_ecuelinm



/*[collateralType]*/

SELECT 
	 collateralTypeId=admatr_inclval
	,collateralTypeName=admatr_incldes
	,isActive=1
	,CreateDate=getdate()
	,ModiDate=getdate()
	,CreateUsrId=1
	,ModiUsrId=1
	,hostName='RAA'
	INTO #_TBL_eadmatrdCollateralType
FROM [Easybank].dbo.eadmatrd
WHERE admatr_tabname='eadmgarm' AND  admatr_campo = 'admgar_tipo'
--AND  admatr_inclval NOT IN (SELECT   collateralTypeId FROM [Loan].[collateralType])




INSERT INTO [Loan].[collateralType]
(
	 collateralTypeId
	,collateralTypeName
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
)
select 
	 collateralTypeId
	,collateralTypeName
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
from #_TBL_eadmatrdCollateralType
where collateralTypeId NOT IN (SELECT   collateralTypeId FROM [Loan].[collateralType])

update a set 
a.collateralTypeName = b.collateralTypeName
from [Loan].[collateralType] a
inner join #_TBL_eadmatrdCollateralType b on a.collateralTypeId=b.collateralTypeId


drop table #_TBL_eadmatrdCollateralType



/*[codebtorType]*/

SELECT 
	 codebtorTypeId=admatr_inclval
	,codebtorTypeName=admatr_incldes
	,isActive=1
	,CreateDate=getdate()
	,ModiDate=getdate()
	,CreateUsrId=1
	,ModiUsrId=1
	,hostName='RAA'
	into #_TBL_eadmatrdcodebtorType
FROM [Easybank].dbo.eadmatrd
WHERE admatr_tabname='ecuectad' AND  admatr_campo = 'cuecta_tipo         '




INSERT INTO [Loan].codebtorType
(
	 codebtorTypeId
	,codebtorTypeName
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
)
select 
	 codebtorTypeId
	,codebtorTypeName
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
from #_TBL_eadmatrdcodebtorType
where codebtorTypeId NOT IN (SELECT   codebtorTypeId FROM [Loan].[collateralType])

update a set 
a.codebtorTypeName = b.codebtorTypeName
from [Loan].codebtorType a
inner join #_TBL_eadmatrdcodebtorType b on a.codebtorTypeId=b.codebtorTypeId


drop table #_TBL_eadmatrdcodebtorType


/*collateralTypeSIB*/
SELECT 
 collateralTypeSIBId=ISNULL(( SELECT MAX(collateralTypeSIBId) FROM [Loan].collateralTypeSIB),0) + ROW_NUMBER() OVER(ORDER BY admtga_nombre)
,collateralTypeSIBName=admtga_nombre
,collateralTypeSIBCode=admtga_codigo
,isActive=CASE WHEN ADMSTS_CODIGO ='A' THEN 1 ELSE 0 END
,CreateDate=getdate()
,ModiDate=getdate()
,CreateUsrId=1
,ModiUsrId=1
,hostName='RAA'
into #_TBL_eadmtgam
FROM [Easybank].dbo.eadmtgam
--WHERE admtga_codigo NOT IN (SELECT collateralTypeSIBCode FROM [Loan].collateralTypeSIB)





INSERT INTO [Loan].collateralTypeSIB
(
	 collateralTypeSIBId
	,collateralTypeSIBName
	,collateralTypeSIBCode
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
)

select 
	 collateralTypeSIBId
	,collateralTypeSIBName
	,collateralTypeSIBCode
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
from #_TBL_eadmtgam
where collateralTypeSIBCode NOT IN (SELECT collateralTypeSIBCode FROM [Loan].collateralTypeSIB)

UPDATE A SET 
	a.collateralTypeSIBName=b.collateralTypeSIBName
FROM  [Loan].collateralTypeSIB A 
INNER JOIN #_TBL_eadmtgam B 
ON a.collateralTypeSIBCode=b.collateralTypeSIBCode

drop table #_TBL_eadmtgam


/*loancollateral*/

SELECT 
	 accountId=cuecta_numid
	,collateralId=admgar_numero
	,amount=admgar_monto
	,[percent]=admgar_porciento
	,[date]=admgar_fecha
	,code=A.admsts_codigo
	,codeDesc=admsts_nombre
	,transationNumber=cuetrx_numero
	,contractNumber=prepre_numcontrato
	,isActive=1
	,CreateDate=getdate()
	,ModiDate=getdate()
	,CreateUsrId=1
	,ModiUsrId=1
	,hostName='RAA'
INTO #_TBL_epreprer
FROM [Easybank].dbo.epreprer A
INNER JOIN 
(
			SELECT  * FROM [Easybank].dbo.xadmstsm
			WHERE admsts_tabla = 'epreprer'
) B ON B.admsts_codigo=A.admsts_codigo



INSERT INTO [Loan].[loancollateral]
(
	 accountId
	,collateralId
	,amount
	,[percent]
	,[date]
	,code
	,codeDesc
	,transationNumber
	,contractNumber
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
)

SELECT 
	 accountId
	,collateralId
	,amount
	,[percent]
	,[date]
	,code
	,codeDesc
	,transationNumber
	,contractNumber
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
FROM #_TBL_epreprer A
WHERE NOT EXISTS 
(
	SELECT TOP 1 1 

	FROM [Loan].[loancollateral] B
	WHERE 
		A.accountId=B.accountId
	AND A.collateralId=B.collateralId
)

UPDATE A SET 
	 A.amount				=B.amount
	,A.[percent]			=B.[percent]
	,A.[date]				=B.[date]
	,A.code					=B.code
	,A.codeDesc				=B.codeDesc
	,A.transationNumber		=B.transationNumber
	,A.contractNumber		=B.contractNumber
	,A.isActive				=B.isActive
	,A.ModiDate				=B.ModiDate
	,A.ModiUsrId			=B.ModiUsrId
	,A.hostName				=B.hostName
FROM [Loan].[loancollateral]  A
INNER JOIN #_TBL_epreprer B 
	ON 
		A.accountId=B.accountId
	AND A.collateralId=B.collateralId


DROP TABLE #_TBL_epreprer


/*[collateral]*/

SELECT 
 clientId=client_codigo
,collateralId=admgar_numero
,companyId=cast(admcia_codigo as int)
,currencyId=admmon_codigo
,collateralTypeSIBId
,collateralTypeId=admgar_tipo
,collateralName=admgar_nombre
,[date]=admgar_fecha
,[percent]=admgar_porcaval
,amount=admgar_monto
,IsInspection=admgar_indinspec
,amountToUse=admgar_montoaval
,inspectionFrequency=admgar_plazo
,inspectionFrequencyDesc =case /*Frecuencia de inspección 0-Diario 1-Mensual 2-Anual*/
							when admgar_plazo ='0' then 'Diario'  
							when admgar_plazo ='1' then 'Mensual'  
							when admgar_plazo ='2' then 'Anual'  
							else '' end

,percentUsed=admgar_porcusado
,amountUsed=admgar_montousado
,reference=admgar_referencia
,formalizationDate=admgar_fecform
,comment=admgar_comments
,isActive=case when admsts_codigo='a' then  1 else 0 end
,CreateDate=getdate()
,ModiDate=getdate()
,CreateUsrId=1
,ModiUsrId=1
,hostName='RAA'

INTO #_TBL_eadmgarm
FROM [Easybank].dbo.eadmgarm a
left join [Loan].[collateralTypeSIB] b on b.[collateralTypeSIBCode]=admtga_codigo


INSERT INTO [Entity].[collateral]
(
 clientId
,collateralId
,companyId
,currencyId
,collateralTypeSIBId
,collateralTypeId
,collateralName
,[date]
,[percent]
,amount
,IsInspection
,amountToUse
,inspectionFrequency
,inspectionFrequencyDesc
,percentUsed
,amountUsed
,reference
,formalizationDate
,comment
,isActive
,CreateDate
,ModiDate
,CreateUsrId
,ModiUsrId
,hostName
)

SELECT 
 clientId
,collateralId
,companyId
,currencyId
,collateralTypeSIBId
,collateralTypeId
,collateralName
,[date]
,[percent]
,amount
,IsInspection
,amountToUse
,inspectionFrequency
,inspectionFrequencyDesc
,percentUsed
,amountUsed
,reference
,formalizationDate
,comment
,isActive
,CreateDate
,ModiDate
,CreateUsrId
,ModiUsrId
,hostName
FROM #_TBL_eadmgarm A 
where not exists 
(
	select  top 1 1 from [Entity].[collateral] B

	where 
	 A.clientId=B.clientId
  and A.collateralId=B.collateralId
)

UPDATE A SET 
 A.clientId					=B.clientId
,A.collateralId				=B.collateralId
,A.companyId				=B.companyId
,A.currencyId				=B.currencyId
,A.collateralTypeSIBId		=B.collateralTypeSIBId
,A.collateralTypeId			=B.collateralTypeId
,A.collateralName			=B.collateralName
,A.[date]					=B.[date]
,A.[percent]				=B.[percent]
,A.amount					=B.amount
,A.IsInspection				=B.IsInspection
,A.amountToUse				=B.amountToUse
,A.inspectionFrequency		=B.inspectionFrequency
,A.inspectionFrequencyDesc	=B.inspectionFrequencyDesc
,A.percentUsed				=B.percentUsed
,A.amountUsed				=B.amountUsed
,A.reference				=B.reference
,A.formalizationDate		=B.formalizationDate
,A.comment					=B.comment
,A.isActive					=B.isActive
,A.ModiDate					=B.ModiDate
,A.ModiUsrId				=B.ModiUsrId
,A.hostName					=B.hostName
FROM  [Entity].[collateral]  A 
INNER JOIN  #_TBL_eadmgarm B
 ON 	 A.clientId=B.clientId
  and A.collateralId=B.collateralId


DROP TABLE #_TBL_eadmgarm

/*[immovablePropertyType]*/



SELECT 
 immovablePropertyTypeId=CAST(admtin_codigo AS INT)
,immovablePropertyTypeName=admtin_nombre
,immovablePropertyTypeCode=admtin_codigo
,isActive=1
,CreateDate=getdate()
,ModiDate=getdate()
,CreateUsrId=1
,ModiUsrId=1
,hostName='RAA'
INTO #_TBL_eadmtinm
FROM [Easybank].dbo.eadmtinm



INSERT INTO [Loan].[immovablePropertyType]
(
 immovablePropertyTypeId
,immovablePropertyTypeName
,immovablePropertyTypeCode
,isActive
,CreateDate
,ModiDate
,CreateUsrId
,ModiUsrId
,hostName
)

SELECT 
 immovablePropertyTypeId
,immovablePropertyTypeName
,immovablePropertyTypeCode
,isActive
,CreateDate
,ModiDate
,CreateUsrId
,ModiUsrId
,hostName
FROM #_TBL_eadmtinm
WHERE immovablePropertyTypeId NOT IN (SELECT immovablePropertyTypeId FROM [Loan].[immovablePropertyType])


UPDATE A SET 
	A.immovablePropertyTypeName=B.immovablePropertyTypeName
FROM [Loan].[immovablePropertyType] A 
INNER JOIN #_TBL_eadmtinm B 
	ON A.immovablePropertyTypeId=B.immovablePropertyTypeId
DROP TABLE #_TBL_eadmtinm

/*[collateralField]*/

SELECT 
	 collateralFieldId=admcga_id
	,immovablePropertyTypeId=admtin_codigo
	,ListId=admcga_lista
	,collateralFieldName=admcga_nombre
	,collateralFieldComment=admcga_coment
	,ISRequired=admcga_requerido
	,isActive=1
	,CreateDate=getdate()
	,ModiDate=getdate()
	,CreateUsrId=1
	,ModiUsrId=1
	,hostName='RAA'
INTO #_TBL_eadmcgam
FROM [Easybank].dbo.eadmcgam


INSERT INTO [Loan].[collateralField]
(
	 collateralFieldId
	,immovablePropertyTypeId
	,ListId
	,collateralFieldName
	,collateralFieldComment
	,ISRequired
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
)


SELECT 
	 collateralFieldId
	,immovablePropertyTypeId
	,ListId
	,collateralFieldName
	,collateralFieldComment
	,ISRequired
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
FROM #_TBL_eadmcgam A 
WHERE collateralFieldId NOT IN (SELECT  collateralFieldId FROM  [Loan].[collateralField])

UPDATE A SET 
	 A.immovablePropertyTypeId	=B.immovablePropertyTypeId
	,A.ListId					=B.ListId
	,A.collateralFieldName		=B.collateralFieldName
	,A.collateralFieldComment	=B.collateralFieldComment
	,A.ISRequired				=B.ISRequired
	,A.isActive					=B.isActive
	,ModiDate=GETDATE()
FROM [Loan].[collateralField] A 
INNER JOIN #_TBL_eadmcgam B
	ON A.collateralFieldId=B.collateralFieldId
DROP TABLE #_TBL_eadmcgam


/*[loanCollateralFieldValue]*/

SELECT 
 collateralId=admgar_numero
,collateralFieldId=admcga_id
,collateralFieldValue=ISNULL(admgar_valor,'')
,isActive=1
,CreateDate=getdate()
,ModiDate=getdate()
,CreateUsrId=1
,ModiUsrId=1
,hostName='RAA'
INTO #_TBL_eadmcgar
FROM [Easybank].dbo.eadmcgar



INSERT INTO [Loan].[loanCollateralFieldValue]
(
 collateralId
,collateralFieldId
,collateralFieldValue
,isActive
,CreateDate
,ModiDate
,CreateUsrId
,ModiUsrId
,hostName
)

SELECT 
 collateralId
,collateralFieldId
,collateralFieldValue
,isActive
,CreateDate
,ModiDate
,CreateUsrId
,ModiUsrId
,hostName
FROM #_TBL_eadmcgar A 
WHERE NOT EXISTS 
(
	SELECT TOP 1 1 FROM [Loan].[loanCollateralFieldValue] B
	WHERE 
	 A.collateralId=B.collateralId
AND  A.collateralFieldId=B.collateralFieldId
)

UPDATE A SET 
 A.collateralFieldId		=B.collateralFieldId
,A.collateralFieldValue		=B.collateralFieldValue
,A.isActive					=B.isActive
,A.ModiDate					=B.ModiDate
,A.ModiUsrId				=B.ModiUsrId
,A.hostName					=B.hostName
FROM  [Loan].[loanCollateralFieldValue] A 
INNER JOIN #_TBL_eadmcgar B
ON A.collateralId=B.collateralId
AND  A.collateralFieldId=B.collateralFieldId

DROP TABLE #_TBL_eadmcgar


/*[policyType]*/


select 
	 policyTypeId= ISNULL((select max(policyTypeId) from [Loan].[policyType]),0) + ROW_NUMBER() OVER( ORDER BY admtpo_descrip)
	,policyTypeName=admtpo_descrip
	,policyTypeCode=admtpo_codigo
	,isActive=CASE WHEN admsts_codigo IN('A','1') THEN 1 ELSE 0 END 
	,CreateDate=getdate()
	,ModiDate=getdate()
	,CreateUsrId=1
	,ModiUsrId=1
	,hostName='RAA'
INTO #_TBL_eadmtpom
from [Easybank].dbo.eadmtpom


INSERT INTO [Loan].[policyType]
(
	 policyTypeId
	,policyTypeName
	,policyTypeCode
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
)

SELECT 
	 policyTypeId
	,policyTypeName
	,policyTypeCode
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
FROM #_TBL_eadmtpom A 
WHERE policyTypeCode NOT IN (SELECT policyTypeCode FROM [Loan].[policyType])


UPDATE A SET 
A.policyTypeName=B.policyTypeName
FROM [Loan].[policyType] A
INNER JOIN #_TBL_eadmtpom B 
ON A.policyTypeCode=B.policyTypeCode
DROP TABLE #_TBL_eadmtpom


/*[policyCollateral]*/

SELECT 
	 companyId=a.admcia_codigo
	,collateralId=a.admgar_numero
	,policyTypeId
	,relatedContactId=admseg_codigo
	,policyCollateralName=admpol_descrip
	,policyCollateralComment=admpol_coment
	,policyNo=admpol_numero
	,Amount=admpol_valor
	,[Date]=admpol_fecha
	,notificationDate=admpol_fecnot
	,EffectiveDate=admpol_fecvenc
	,isActive=case when admsts_codigo='A' then 1 else 0 end
	,CreateDate=getdate()
	,ModiDate=getdate()
	,CreateUsrId=1
	,ModiUsrId=1
	,hostName='RAA'
INTO #_TBL_eadmpolm
FROM  [Easybank].dbo.eadmpolm a
inner join [Loan].[policyType] p on p.policyTypeCode=a.admtpo_codigo


INSERT INTO [Loan].[policyCollateral]
(
	companyId
	,collateralId
	,policyTypeId
	,relatedContactId
	,policyCollateralName
	,policyCollateralComment
	,policyNo
	,Amount
	,[Date]
	,notificationDate
	,EffectiveDate
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
)


SELECT 
	 companyId
	,collateralId
	,policyTypeId
	,relatedContactId
	,policyCollateralName
	,policyCollateralComment
	,policyNo
	,Amount
	,[Date]
	,notificationDate
	,EffectiveDate
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
FROM #_TBL_eadmpolm A 
WHERE NOT EXISTS 
(
	SELECT collateralId FROM [Loan].[policyCollateral] B
	WHERE A.collateralId=B.collateralId AND A.policyNo=B.policyNo

)


UPDATE A SET 
	 A.policyTypeId				=B.policyTypeId
	,A.relatedContactId			=B.relatedContactId
	,A.policyCollateralName		=B.policyCollateralName
	,A.policyCollateralComment	=B.policyCollateralComment
	,A.policyNo					=B.policyNo
	,A.Amount					=B.Amount
	,A.[Date]					=B.[Date]
	,A.notificationDate			=B.notificationDate
	,A.EffectiveDate			=B.EffectiveDate
	,A.isActive					=B.isActive
	,A.ModiDate					=B.ModiDate
	,A.ModiUsrId				=B.ModiUsrId
	,A.hostName					=B.hostName
FROM [Loan].[policyCollateral] A 
INNER JOIN #_TBL_eadmpolm B
ON A.collateralId=B.collateralId AND A.policyNo=B.policyNo

DROP TABLE #_TBL_eadmpolm


/*[policyCollateralEndorse]*/

SELECT 
	 collateralId=admgar_numero
	,policyNo=admpol_numero
	,endorseNo=admpol_numendoso
	,amount=admpol_monto
	,[Date]=admpol_fecha
	,InicialDate=admpol_fechaini
	,EffectiveDate=admpol_fecvenc
	,isActive=1
	,CreateDate=getdate()
	,ModiDate=getdate()
	,CreateUsrId=1
	,ModiUsrId=1
	,hostName='RAA'
INTO #_TBL_eadmpolr
FROM [Easybank].dbo.eadmpolr A




INSERT INTO [Loan].[policyCollateralEndorse]
(
	 collateralId
	,policyNo
	,endorseNo
	,amount
	,[Date]
	,InicialDate
	,EffectiveDate
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
)

SELECT 
	 collateralId
	,policyNo
	,endorseNo
	,amount
	,[Date]
	,InicialDate
	,EffectiveDate
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
FROM #_TBL_eadmpolr A 
WHERE NOT EXISTS 
(
	SELECT collateralId FROM [Loan].[policyCollateralEndorse] B
	WHERE A.collateralId=B.collateralId AND A.policyNo=B.policyNo

)

UPDATE A SET 
	 A.endorseNo		=B.endorseNo
	,A.amount			=B.amount
	,A.[Date]			=B.[Date]
	,A.InicialDate		=B.InicialDate
	,A.EffectiveDate	=B.EffectiveDate
	,A.isActive			=B.isActive
	,A.ModiDate			=B.ModiDate
	,A.CreateUsrId		=B.CreateUsrId
	,A.hostName			=B.hostName
FROM [Loan].[policyCollateralEndorse]  A 
INNER JOIN #_TBL_eadmpolr B
ON A.collateralId=B.collateralId AND A.policyNo=B.policyNo
DROP TABLE #_TBL_eadmpolr


/*[codebtor]*/

SELECT 
	 companyId=admcia_codigo
	,accountId=cuecta_numid
	,clientId=client_codigo
	,codebtorTypeId=cuecta_tipo
	,canDeposit=cuecta_inddep
	,canWithdraw=cuecta_indret
	,canCancel=cuecta_indcan
	,IsJointY=cuecta_cuentay
	,IsJointO=cuecta_cuentao
	,isActive=1
	,CreateDate=getdate()
	,ModiDate=getdate()
	,CreateUsrId=1
	,ModiUsrId=1
	,hostName='RAA'
INTO #_TBL_ecuectad
FROM [Easybank].dbo.ecuectad A


INSERT INTO [Loan].[codebtor]
(
	 companyId
	,accountId
	,clientId
	,codebtorTypeId
	,canDeposit
	,canWithdraw
	,canCancel
	,IsJointY
	,IsJointO
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
)

SELECT 
	 companyId
	,accountId
	,clientId
	,codebtorTypeId
	,canDeposit
	,canWithdraw
	,canCancel
	,IsJointY
	,IsJointO
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
FROM #_TBL_ecuectad A 
WHERE NOT EXISTS 
(
	SELECT TOP 1 1 FROM [Loan].[codebtor] B
	WHERE A.companyId=B.companyId AND A.accountId=B.accountId AND A.clientId=B.clientId
)

UPDATE A SET 
	 A.codebtorTypeId =B.codebtorTypeId
	,A.canDeposit	  =B.canDeposit
	,A.canWithdraw	  =B.canWithdraw
	,A.canCancel	  =B.canCancel
	,A.IsJointY		  =B.IsJointY
	,A.IsJointO		  =B.IsJointO
	,A.isActive		  =B.isActive
	,A.ModiDate		  =B.ModiDate
	,A.ModiUsrId	  =B.ModiUsrId
	,A.hostName		  =B.hostName
FROM [Loan].[codebtor] A
INNER JOIN #_TBL_ecuectad B
ON A.companyId=B.companyId AND A.accountId=B.accountId AND A.clientId=B.clientId

DROP TABLE #_TBL_ecuectad


/*[Loan].PaymentForm*/

SELECT 
	 PaymentFormId=admatr_inclval
	,PaymentFormName=admatr_incldes
	,isActive=1
	,CreateDate=getdate()
	,ModiDate=getdate()
	,CreateUsrId=1
	,ModiUsrId=1
	,hostName='RAA'
	INTO #_TBL_eadmatrdPaymentForm
FROM [Easybank].dbo.eadmatrd
WHERE admatr_tabname='eadmgasm          ' AND  admatr_campo = 'admgas_formcobro    '




INSERT INTO [Loan].PaymentForm
(
	 PaymentFormId
	,PaymentFormName
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
)
SELECT 
	 PaymentFormId
	,PaymentFormName
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
FROM #_TBL_eadmatrdPaymentForm A 
WHERE   PaymentFormId NOT IN (SELECT   PaymentFormId FROM [Loan].PaymentForm)

UPDATE A SET 
A.PaymentFormName=B.PaymentFormName
FROM [Loan].PaymentForm A
INNER JOIN #_TBL_eadmatrdPaymentForm B
ON A.PaymentFormId=B.PaymentFormId
DROP TABLE #_TBL_eadmatrdPaymentForm

/*[expenseFormCal]*/


SELECT 
	 expenseFormCalId=admatr_inclval
	,expenseFormCalName=admatr_incldes
	,isActive=1
	,CreateDate=getdate()
	,ModiDate=getdate()
	,CreateUsrId=1
	,ModiUsrId=1
	,hostName='RAA'
	INTO #_TBL_eadmatrdexpenseFormCal
FROM [Easybank].dbo.eadmatrd
WHERE admatr_tabname='eadmgasm          ' AND  admatr_campo = 'admgas_calculo          '

INSERT INTO [Loan].[expenseFormCal]
(
	 expenseFormCalId
	,expenseFormCalName
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
)

SELECT 
	 expenseFormCalId
	,expenseFormCalName
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
FROM #_TBL_eadmatrdexpenseFormCal A
WHERE   expenseFormCalId NOT IN (SELECT   expenseFormCalId FROM [Loan].[expenseFormCal])


UPDATE A SET 
A.expenseFormCalName=B.expenseFormCalName
FROM [Loan].[expenseFormCal] A
INNER JOIN #_TBL_eadmatrdexpenseFormCal B
ON A.expenseFormCalId=B.expenseFormCalId


DROP TABLE #_TBL_eadmatrdexpenseFormCal


/*expenseHeader*/

SELECT 
	 companyId=CAST(admcia_codigo AS INT)
	,expenseHeaderId=admgas_codigo
	,accountTypeID
	,expenseFormCalId=admgas_calculo
	,PaymentFormId=admgas_formcobro
	,expenseHeaderName=admgas_nombre
	,Cancondone=admgas_sencondo
	,amount=admgas_valor
	,CanEdit=admgas_editable
	,CanCXP=admgas_indcxp
	,ISInsurence=admgas_refer
	,ISLegal=admgas_relprest
	,ISQuata=admgas_relprest
	,isActive=CASE WHEN admsts_codigo='A' THEN 1 ELSE 0 END
	,CreateDate=getdate()
	,ModiDate=getdate()
	,CreateUsrId=1
	,ModiUsrId=1
	,hostName='RAA'
INTO #_TBL_eadmgasm
FROM  [Easybank].dbo.eadmgasm
INNER JOIN  [global].[accountType] AC ON AC.[accountTypecODE]=admtcu_codigo


INSERT INTO [Loan].expenseHeader
(
	 companyId
	,expenseHeaderId
	,accountTypeID
	,expenseFormCalId
	,PaymentFormId
	,expenseHeaderName
	,Cancondone
	,amount
	,CanEdit
	,CanCXP
	,ISInsurence
	,ISLegal
	,ISQuata
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
)

SELECT 
	 companyId
	,expenseHeaderId
	,accountTypeID
	,expenseFormCalId
	,PaymentFormId
	,expenseHeaderName
	,Cancondone
	,amount
	,CanEdit
	,CanCXP
	,ISInsurence
	,ISLegal
	,ISQuata
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
FROM #_TBL_eadmgasm A 
WHERE NOT EXISTS
(
	SELECT TOP 1 1  FROM  [Loan].expenseHeader B
	WHERE 
		A.companyId=B.companyId
	AND A.expenseHeaderId=B.expenseHeaderId
)

UPDATE A SET 
	 A.accountTypeID			=B.accountTypeID
	,A.expenseFormCalId			=B.expenseFormCalId
	,A.PaymentFormId			=B.PaymentFormId
	,A.expenseHeaderName		=B.expenseHeaderName
	,A.Cancondone				=B.Cancondone
	,A.amount					=B.amount
	,A.CanEdit					=B.CanEdit
	,A.CanCXP					=B.CanCXP
	,A.ISInsurence				=B.ISInsurence
	,A.ISLegal					=B.ISLegal
	,A.ISQuata					=B.ISQuata
	,A.isActive					=B.isActive
	,A.ModiDate					=B.ModiDate
	,A.ModiUsrId				=B.ModiUsrId
	,A.hostName					=B.hostName
FROM   [Loan].expenseHeader A
INNER JOIN #_TBL_eadmgasm B 
	ON 	A.companyId=B.companyId
	AND A.expenseHeaderId=B.expenseHeaderId


DROP  TABLE #_TBL_eadmgasm

/*[loanExpense]*/


SELECT 
	 loanExpenseId=admgas_numero
	,companyId=admcia_codigo
	,expenseHeaderId=admgas_codigo
	,accountId=cuecta_numid
	,paymentFormId=admgas_formcobro
	,[percent]=admgas_porciento
	,amount=admgas_monto
	,payment=admgas_saldo
	,initialQuata=admgas_cuotaini
	,endQuata=admgas_cuotafin
	,date=admgas_fecha
	,disbursementNumber=preliq_numero
	,QuataNumber=precuo_numid
	,TransationNumber=cuetrx_numero
	,CanCondone=admgas_indcon
	,isActive=CASE WHEN admsts_codigo='0' THEN 1 ELSE 0 END
	,CreateDate=getdate()
	,ModiDate=getdate()
	,CreateUsrId=1
	,ModiUsrId=1
	,hostName='RAA'
INTO #_TBL_eadmgasr
FROM  [Easybank].dbo.eadmgasr
WHERE  cuecta_numid  IN (SELECT ACCOUNTID FROM [Loan].[Account])




INSERT INTO [Loan].[loanExpense]
(
	 loanExpenseId
	,companyId
	,expenseHeaderId
	,accountId
	,paymentFormId
	,[percent]
	,amount
	,payment
	,initialQuata
	,endQuata
	,[date]
	,disbursementNumber
	,QuataNumber
	,TransationNumber
	,CanCondone
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
)

SELECT 
	 loanExpenseId
	,companyId
	,expenseHeaderId
	,accountId
	,paymentFormId
	,[percent]
	,amount
	,payment
	,initialQuata
	,endQuata
	,[date]
	,disbursementNumber
	,QuataNumber
	,TransationNumber
	,CanCondone
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
FROM #_TBL_eadmgasr A 
WHERE NOT EXISTS
(
	SELECT TOP 1 1  FROM  [Loan].[loanExpense] B
	WHERE 
		A.loanExpenseId=B.loanExpenseId

)

UPDATE  A SET 
	 A.loanExpenseId		=B.loanExpenseId
	,A.companyId			=B.companyId
	,A.expenseHeaderId		=B.expenseHeaderId
	,A.accountId			=B.accountId
	,A.paymentFormId		=B.paymentFormId
	,A.[percent]			=B.[percent]
	,A.amount				=B.amount
	,A.payment				=B.payment
	,A.initialQuata			=B.initialQuata
	,A.endQuata				=B.endQuata
	,A.[date]				=B.[date]
	,A.disbursementNumber	=B.disbursementNumber
	,A.QuataNumber			=B.QuataNumber
	,A.TransationNumber		=B.TransationNumber
	,A.CanCondone			=B.CanCondone
	,A.isActive				=B.isActive
	,A.ModiDate				=B.ModiDate
	,A.CreateUsrId			=B.CreateUsrId
	,A.hostName				=B.hostName
FROM [Loan].[loanExpense] A 
INNER JOIN #_TBL_eadmgasr B
ON A.loanExpenseId=B.loanExpenseId
DROP TABLE #_TBL_eadmgasr

/*[transactionDefinition]*/


SELECT 
	 transactionDefinitionId=admctr_numid
	,transactionCode=admctr_codigo
	,accountTypeID
	,transactionDescription=admctr_nombre
	,transactionDescriptionShort=admctr_desc
	,creditOrigen=admctr_origen
	,ISEvent=admctr_evento
	,IStransaction=admctr_transaccion
	,CanPrintStatement=admctr_estado
	,SearchBy=admctr_critevento
	,annulTransactionDefinitionId=admctr_anula
	,fixTransactionDefinitionId=admctr_indfija
	,requiresAuthorization=admctr_indautoriz
	,IsCreateEvent=admctr_tipanul
	,intersuccursal=admctr_indsuc
	,interfund=admctr_indfue
	,creditNoteType=admctr_tiponota
	,HasNCF=admctr_generancf
	,HasSurcharges=admctr_gencargo
	,CanPending=admctr_aplictardia
	,CanRecurrent=admctr_recurrente
	,isActive=1
	,CreateDate=getdate()
	,ModiDate=getdate()
	,CreateUsrId=1
	,ModiUsrId=1
	,hostName='RAA'
INTO #_TBL_eadmctrm
FROM [Easybank].dbo.eadmctrm A 
LEFT JOIN [global].[accountType] B ON B.accountTypeCode =A.admtcu_codigo


 
INSERT INTO [Loan].[transactionDefinition]
(
	 transactionDefinitionId
	,transactionCode
	,accountTypeID
	,transactionDescription
	,transactionDescriptionShort
	,creditOrigen
	,ISEvent
	,IStransaction
	,CanPrintStatement
	,SearchBy
	,annulTransactionDefinitionId
	,fixTransactionDefinitionId
	,requiresAuthorization
	,IsCreateEvent
	,intersuccursal
	,interfund
	,creditNoteType
	,HasNCF
	,HasSurcharges
	,CanPending
	,CanRecurrent
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
)

SELECT 
	 transactionDefinitionId
	,transactionCode
	,accountTypeID
	,transactionDescription
	,transactionDescriptionShort
	,creditOrigen
	,ISEvent
	,IStransaction
	,CanPrintStatement
	,SearchBy
	,annulTransactionDefinitionId
	,fixTransactionDefinitionId
	,requiresAuthorization
	,IsCreateEvent
	,intersuccursal
	,interfund
	,creditNoteType
	,HasNCF
	,HasSurcharges
	,CanPending
	,CanRecurrent
	,isActive
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,hostName
FROM #_TBL_eadmctrm
WHERE transactionDefinitionId NOT IN ( SELECT transactionDefinitionId FROM [Loan].[transactionDefinition] )
 

 UPDATE A SET 
 	 A.transactionCode					=B.transactionCode
	,A.accountTypeID					=B.accountTypeID
	,A.transactionDescription			=B.transactionDescription
	,A.transactionDescriptionShort		=B.transactionDescriptionShort
	,A.creditOrigen						=B.creditOrigen
	,A.ISEvent							=B.ISEvent
	,A.IStransaction					=B.IStransaction
	,A.CanPrintStatement				=B.CanPrintStatement
	,A.SearchBy							=B.SearchBy
	,A.annulTransactionDefinitionId		=B.annulTransactionDefinitionId
	,A.fixTransactionDefinitionId		=B.fixTransactionDefinitionId
	,A.requiresAuthorization			=B.requiresAuthorization
	,A.IsCreateEvent					=B.IsCreateEvent
	,A.intersuccursal					=B.intersuccursal
	,A.interfund						=B.interfund
	,A.creditNoteType					=B.creditNoteType
	,A.HasNCF							=B.HasNCF
	,A.HasSurcharges					=B.HasSurcharges
	,A.CanPending						=B.CanPending
	,A.CanRecurrent						=B.CanRecurrent
	,A.isActive							=B.isActive
	,A.ModiDate							=B.ModiDate
	,A.ModiUsrId						=B.ModiUsrId
	,A.hostName							=B.hostName
 FROM [Loan].[transactionDefinition] A 
 INNER JOIN #_TBL_eadmctrm B 
 ON A.transactionDefinitionId=B.transactionDefinitionId


DROP TABLE #_TBL_eadmctrm

/*[transactionPaymentForm]*/


SELECT 
	 transactionPaymentFormId=admatr_inclval
	,transactionPaymentFormName=admatr_incldes
	,isActive=1
	,CreateDate=getdate()
	,ModiDate=getdate()
	,CreateUsrId=1
	,ModiUsrId=1
	,hostName='RAA'
INTO #_TBL_eadmatrdtransactionPaymentForm
FROM [Easybank].dbo.eadmatrd
WHERE admatr_tabname='epretrxm' AND  admatr_campo = 'pretrx_forma        '


INSERT INTO [Loan].[transactionPaymentForm]
(
 transactionPaymentFormId
,transactionPaymentFormName
,isActive
,CreateDate
,ModiDate
,CreateUsrId
,ModiUsrId
,hostName
)

SELECT 
 transactionPaymentFormId
,transactionPaymentFormName
,isActive
,CreateDate
,ModiDate
,CreateUsrId
,ModiUsrId
,hostName
FROM #_TBL_eadmatrdtransactionPaymentForm A
WHERE transactionPaymentFormId NOT IN (SELECT   transactionPaymentFormId FROM [Loan].[transactionPaymentForm])

UPDATE A SET 
	A.transactionPaymentFormName=B.transactionPaymentFormName
FROM [Loan].[transactionPaymentForm] A
INNER JOIN #_TBL_eadmatrdtransactionPaymentForm B ON A.transactionPaymentFormId=B.transactionPaymentFormId

DROP TABLE #_TBL_eadmatrdtransactionPaymentForm

/*[transactionReceipt]*/


SELECT 
	 transactionReceiptId=pretrx_numid
	,transactionDefinitionId=admctr_numid
	,companyId=admcia_codigo
	,officeId=admsuc_codigo
	--,transactionPaymentFormId=pretrx_forma
	,transactionPaymentFormId=pretrx_forma
	,accountId=cuecta_numid
	,relatedContactId=client_pagado
	,receiptNumber=pretrx_numero
	,transactionDate=pretrx_fecha
	,transactionNumber=cuetrx_numero
	,discount=pretrx_descu
	,dicumentNumber=pretrx_numdoc
	,Payment=pretrx_total
	,Status=admsts_codigo
	,isActive=1
	,CreateDate=getdate()
	,ModiDate=getdate()
	,CreateUsrId=1
	,ModiUsrId=1
	,hostName='RAA'
INTO #_TBL_epretrxm
FROM [Easybank].dbo.epretrxm
WHERE cuecta_numid  in (select accountid from [Loan].[Account])



INSERT INTO [Loan].[transactionReceipt]
(
 transactionReceiptId
,transactionDefinitionId
,companyId
,officeId
,transactionPaymentFormId
,accountId
,relatedContactId
,receiptNumber
,transactionDate
,transactionNumber
,discount
,dicumentNumber
,Payment
,Status
,isActive
,CreateDate
,ModiDate
,CreateUsrId
,ModiUsrId
,hostName
)

SELECT 
 transactionReceiptId
,transactionDefinitionId
,companyId
,officeId
,transactionPaymentFormId
,accountId
,relatedContactId
,receiptNumber
,transactionDate
,transactionNumber
,discount
,dicumentNumber
,Payment
,Status
,isActive
,CreateDate
,ModiDate
,CreateUsrId
,ModiUsrId
,hostName
FROM #_TBL_epretrxm A
WHERE transactionReceiptId not in (select transactionReceiptId from [Loan].[transactionReceipt])


UPDATE  A SET 
 A.transactionDefinitionId		=B.transactionDefinitionId
,A.companyId					=B.companyId
,A.officeId						=B.officeId
,A.transactionPaymentFormId		=B.transactionPaymentFormId
,A.accountId					=B.accountId
,A.relatedContactId				=B.relatedContactId
,A.receiptNumber				=B.receiptNumber
,A.transactionDate				=B.transactionDate
,A.transactionNumber			=B.transactionNumber
,A.discount						=B.discount
,A.dicumentNumber				=B.dicumentNumber
,A.Payment						=B.Payment
,A.Status						=B.Status
,A.isActive						=B.isActive
,A.ModiDate						=B.ModiDate
,A.ModiUsrId					=B.ModiUsrId
,A.hostName						=B.hostName
FROM [Loan].[transactionReceipt] A 
INNER JOIN #_TBL_epretrxm B 
ON A.transactionReceiptId=B.transactionReceiptId


DROP TABLE #_TBL_epretrxm

/*PLAN DE PAGO*/


SELECT 
 transactionPaymentPlanId=precuo_numid
,accountId=cuecta_numid
,companyId=admcia_codigo
,officeId=admsuc_codigo
,quotaNumber=precuo_numcuota
,transactionReasonId=precuo_motivo
,emisionQuotaDate=precuo_fecha
,endQuotaDate=precuo_fecvenc

,expenseAmount=precuo_mongas
,commissionAmount=precuo_moncom
,interestAmoint=precuo_monint
,capitalAmount=precuo_moncap
,rateLateAmount=precuo_montomor

,expenseBalance=precuo_salgas
,commissionBalance=precuo_salcom
,rateLateBalance=precuo_salmor
,interestBalance=precuo_salint
,capitalBalance=precuo_salcap


,IsPaymentExpense=precuo_senalgas
,IsPaymentcommission=precuo_senalcom
,IsPaymentinterest=precuo_senalint
,[Days]=precuo_diascal
,referenceNumber=precuo_numref
,loanBalanceBefore=precuo_saldo
,ISLastQuota=precuo_ultcuo
,TransactionType=precuo_indprov
,initialDay=precuo_fecini
,chargesPrepayment=precuo_carpgo
,graceAmount=precuo_graciaint
,graceCommissionAmount=precuo_graciacom
,IsPrepayment=precuo_adepgo
,IsCapitalPayment=precuo_senalcap
,isActive=CASE WHEN admsts_codigo ='A' THEN 1 ELSE 0 END
,CreateDate=getdate()
,ModiDate=getdate()
,CreateUsrId=1
,ModiUsrId=1
,hostName='RAA'
INTO #_TBL_eprecuom
FROM [Easybank].dbo.eprecuom
WHERE cuecta_numid  in (select accountid from [Loan].[Account])



INSERT INTO [Loan].[transactionPaymentPlan]
(
 transactionPaymentPlanId
,accountId
,companyId
,officeId
,quotaNumber
,transactionReasonId
,emisionQuotaDate
,endQuotaDate
,expenseAmount
,commissionAmount
,interestAmoint
,capitalAmount
,rateLateAmount
,expenseBalance
,commissionBalance
,rateLateBalance
,interestBalance
,capitalBalance
,IsPaymentExpense
,IsPaymentcommission
,IsPaymentinterest
,[Days]
,referenceNumber
,loanBalanceBefore
,ISLastQuota
,TransactionType
,initialDay
,chargesPrepayment
,graceAmount
,graceCommissionAmount
,IsPrepayment
,IsCapitalPayment
,isActive
,CreateDate
,ModiDate
,CreateUsrId
,ModiUsrId
,hostName
)

SELECT 
 transactionPaymentPlanId
,accountId
,companyId
,officeId
,quotaNumber
,transactionReasonId
,emisionQuotaDate
,endQuotaDate
,expenseAmount
,commissionAmount
,interestAmoint
,capitalAmount
,rateLateAmount
,expenseBalance
,commissionBalance
,rateLateBalance
,interestBalance
,capitalBalance
,IsPaymentExpense
,IsPaymentcommission
,IsPaymentinterest
,[Days]
,referenceNumber
,loanBalanceBefore
,ISLastQuota
,TransactionType
,initialDay
,chargesPrepayment
,graceAmount
,graceCommissionAmount
,IsPrepayment
,IsCapitalPayment
,isActive
,CreateDate
,ModiDate
,CreateUsrId
,ModiUsrId
,hostName
FROM #_TBL_eprecuom
WHERE  transactionPaymentPlanId not in (select transactionPaymentPlanId from [Loan].[transactionPaymentPlan])

UPDATE  A SET 
 A.accountId				=B.accountId
,A.companyId				=B.companyId
,A.officeId					=B.officeId
,A.quotaNumber				=B.quotaNumber
,A.transactionReasonId		=B.transactionReasonId
,A.emisionQuotaDate			=B.emisionQuotaDate
,A.endQuotaDate				=B.endQuotaDate
,A.expenseAmount			=B.expenseAmount
,A.commissionAmount			=B.commissionAmount
,A.interestAmoint			=B.interestAmoint
,A.capitalAmount			=B.capitalAmount
,A.rateLateAmount			=B.rateLateAmount
,A.expenseBalance			=B.expenseBalance
,A.commissionBalance		=B.commissionBalance
,A.rateLateBalance			=B.rateLateBalance
,A.interestBalance			=B.interestBalance
,A.capitalBalance			=B.capitalBalance
,A.IsPaymentExpense			=B.IsPaymentExpense
,A.IsPaymentcommission		=B.IsPaymentcommission
,A.IsPaymentinterest		=B.IsPaymentinterest
,A.[Days]					=B.[Days]
,A.referenceNumber			=B.referenceNumber
,A.loanBalanceBefore		=B.loanBalanceBefore
,A.ISLastQuota				=B.ISLastQuota
,A.TransactionType			=B.TransactionType
,A.initialDay				=B.initialDay
,A.chargesPrepayment		=B.chargesPrepayment
,A.graceAmount				=B.graceAmount
,A.graceCommissionAmount	=B.graceCommissionAmount
,A.IsPrepayment				=B.IsPrepayment
,A.IsCapitalPayment			=B.IsCapitalPayment
,A.isActive					=B.isActive
,A.ModiDate					=B.ModiDate
,A.ModiUsrId				=B.ModiUsrId
,A.hostName					=B.hostName
FROM  [Loan].[transactionPaymentPlan] A 
INNER JOIN #_TBL_eprecuom B ON A.transactionPaymentPlanId=B.transactionPaymentPlanId

DROP TABLE #_TBL_eprecuom


/*transactionSinal*/


SELECT 
	 transactionSinalId=admatr_inclval
	,transactionSinalIdName=admatr_incldes
	,isActive=1
	,CreateDate=getdate()
	,ModiDate=getdate()
	,CreateUsrId=1
	,ModiUsrId=1
	,hostName='RAA'
INTO #_TBL_eadmatrdtransactionSinal
FROM [Easybank].dbo.eadmatrd
WHERE admatr_tabname='epreconr' AND  admatr_campo = 'precon_senal'


INSERT INTO [Loan].transactionSinal
(
 transactionSinalId
,transactionSinalIdName
,isActive
,CreateDate
,ModiDate
,CreateUsrId
,ModiUsrId
,hostName
)

SELECT 
 transactionSinalId
,transactionSinalIdName
,isActive
,CreateDate
,ModiDate
,CreateUsrId
,ModiUsrId
,hostName
FROM #_TBL_eadmatrdtransactionSinal A
WHERE  transactionSinalId NOT IN (SELECT   transactionSinalId FROM [Loan].transactionSinal)

UPDATE A SET 
	A.transactionSinalIdName=B.transactionSinalIdName
FROM  [Loan].transactionSinal A
INNER JOIN #_TBL_eadmatrdtransactionSinal B
	ON A.transactionSinalId=B.transactionSinalId
DROP TABLE #_TBL_eadmatrdtransactionSinal


/*Loan].[transactionReceiptDetail]*/

SELECT 

 transactionReceiptId=pretrx_numid
,transactionReceiptSequence=pretrx_seq
,transactionPaymentPlanId=precuo_numid
,transactionSinalId=pretrx_senal
,rateLateAmount=pretrx_mora
,expenseAmount=pretrx_gastos
,commissionAmount=pretrx_comision
,interestAmoint=pretrx_interes
,capitalAmount=pretrx_capital
,discountAmount=pretrx_descu
,documentNumber=pretrx_numdoc
,transactionPaymentFormId=admfpg_numid
,Sequence=cnttrx_numid
,chargeAmount=pretrx_carpgo
,isActive=1
,CreateDate=getdate()
,ModiDate=getdate()
,CreateUsrId=1
,ModiUsrId=1
,hostName='RAA'
INTO #_TBL_epretrxd
FROM [Easybank].dbo.epretrxd A
WHERE 
precuo_numid  IN (SELECT transactionPaymentPlanId FROM [Loan].[transactionPaymentPlan])

INSERT INTO [Loan].[transactionReceiptDetail]
(
 transactionReceiptId
,transactionReceiptSequence
,transactionPaymentPlanId
,transactionSinalId
,rateLateAmount
,expenseAmount
,commissionAmount
,interestAmoint
,capitalAmount
,discountAmount
,documentNumber
,transactionPaymentFormId
,Sequence
,chargeAmount
,isActive
,CreateDate
,ModiDate
,CreateUsrId
,ModiUsrId
,hostName
)


SELECT 
 transactionReceiptId
,transactionReceiptSequence
,transactionPaymentPlanId
,transactionSinalId
,rateLateAmount
,expenseAmount
,commissionAmount
,interestAmoint
,capitalAmount
,discountAmount
,documentNumber
,transactionPaymentFormId
,Sequence
,chargeAmount
,isActive
,CreateDate
,ModiDate
,CreateUsrId
,ModiUsrId
,hostName
FROM #_TBL_epretrxd A
WHERE 
	NOT EXISTS 
	(
		SELECT TOP 1 1 
		FROM [Loan].[transactionReceiptDetail] B
		WHERE 
			A.transactionReceiptId=B.transactionReceiptId
		AND A.transactionReceiptSequence=B.transactionReceiptSequence
	)

UPDATE A SET 
 A.transactionPaymentPlanId		=B.transactionPaymentPlanId
,A.transactionSinalId			=B.transactionSinalId
,A.rateLateAmount				=B.rateLateAmount
,A.expenseAmount				=B.expenseAmount
,A.commissionAmount				=B.commissionAmount
,A.interestAmoint				=B.interestAmoint
,A.capitalAmount				=B.capitalAmount
,A.discountAmount				=B.discountAmount
,A.documentNumber				=B.documentNumber
,A.transactionPaymentFormId		=B.transactionPaymentFormId
,A.Sequence						=B.Sequence
,A.chargeAmount					=B.chargeAmount
,A.isActive						=B.isActive
,A.CreateDate					=B.CreateDate
,A.ModiDate						=B.ModiDate
,A.CreateUsrId					=B.CreateUsrId
,A.ModiUsrId					=B.ModiUsrId
,A.hostName						=B.hostName
FROM [Loan].[transactionReceiptDetail] A
INNER JOIN #_TBL_epretrxd B 
ON 	A.transactionReceiptId=B.transactionReceiptId
		AND A.transactionReceiptSequence=B.transactionReceiptSequence
DROP TABLE #_TBL_epretrxd


/*[transactionReason]*/

SELECT 
	 transactionReasonId=admatr_inclval
	,transactionReasonName=admatr_incldes
	,isActive=1
	,CreateDate=getdate()
	,ModiDate=getdate()
	,CreateUsrId=1
	,ModiUsrId=1
	,hostName='RAA'
INTO #_TBL_eadmatrdtransactionReason
FROM [Easybank].dbo.eadmatrd
WHERE admatr_tabname='eprecuom' AND  admatr_campo = 'precuo_motivo       '


INSERT INTO [Loan].[transactionReason]
(
 transactionReasonId
,transactionReasonName
,isActive
,CreateDate
,ModiDate
,CreateUsrId
,ModiUsrId
,hostName
)

SELECT 
 transactionReasonId
,transactionReasonName
,isActive
,CreateDate
,ModiDate
,CreateUsrId
,ModiUsrId
,hostName
FROM #_TBL_eadmatrdtransactionReason B
WHERE transactionReasonId NOT IN (SELECT   transactionReasonId FROM [Loan].[transactionReason])

UPDATE A SET 
A.transactionReasonName=B.transactionReasonName
FROM [Loan].[transactionReason] A 
INNER JOIN #_TBL_eadmatrdtransactionReason B ON A.transactionReasonId=B.transactionReasonId
DROP TABLE #_TBL_eadmatrdtransactionReason


GO
/****** Object:  StoredProcedure [Entity].[FILL_DROP_DOWN]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Entity].[FILL_DROP_DOWN]
	@DropDownName VARCHAR(100)
AS
BEGIN
	
	IF LOWER(@DropDownName) = 'countries'
	BEGIN
		Select cast([CountryID] as varchar) as [Key],
			[CountryName] as [Value]
		from
			[global].[Country]
		Where
			isActive= 1
	END

	IF LOWER(@DropDownName) = 'phonetype'
	BEGIN
		Select Distinct PhoneType as [Key],
		(
			Case PhoneType
				When 'CEL' then 'Celular'
				When 'OFI' then 'Oficina'
				When 'RES' then 'Residencia'
				When 'TEL' then 'Telefono'
			else
				PhoneType
		end) as [Value]
		FROM [Entity].contactPhones
		Where
		PhoneType not in('O','C','BEE')
	END

	IF LOWER(@DropDownName) = 'emailtype'
	BEGIN
			Select Distinct emailType as [Key],
				[emailType] as [Value]
			FROM [Entity].contactEmail
	END

	IF LOWER(@DropDownName) = 'transactionreason'
	BEGIN
			Select transactionReasonName as [Key],
				cast(transactionReasonId as varchar) as [Value]
			FROM [Loan].[transactionReason]
	END
END
GO
/****** Object:  StoredProcedure [Entity].[SP_GET_CLIENT_INFORMATIONS]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [Entity].[SP_GET_CLIENT_INFORMATIONS]
	@clientId BIGINT = NULL 
   ,@IdentificationNumber VARCHAR(50) = NULL
AS 

--declare 
--@clientId BIGINT = NULL 
--@IdentificationNumber VARCHAR(50) = '223-0081878-2'
SELECT 
	   cli.clientId
	  ,rc.[relatedContactId]
      ,rc.[countryID]
      ,rc.[entityRolId]
      ,rc.[identificationTypeId]
      ,rc.[entityClassId]
      ,rc.[personTypeId]
      ,rc.[maritalStatusId]
      ,rc.[professionId]
	 
	   --ADDRESS
	  ,ISNULL(addPri.addressId,addsec.addressId) addressId
      ,ISNULL(addPri.CountryID,addsec.CountryID) addressCountryID
	  ,ISNULL(addPri.provinceId,addsec.provinceId) addressprovinceId
	  ,ISNULL(addPri.cityId,addsec.cityId) addresscityId
	  ,ISNULL(addPri.physicalSectorId,addsec.physicalSectorId) addressphysicalSectorId

	  --phone 
	  ,ISNULL(phoPri.contactPhoneId,phoSec.contactPhoneId) contactPhoneId
	  --email 
	  ,ISNULL(emailPri.contactEmailId,emailSec.contactEmailId)contactEmailId

      ,cli.companyId
	  ,cli.linkedTypeId
	  ,cli.clientTypeByCompanyId
	  ,cli.reasonId
	  ,cli.clientTypeBySIBId
	  
	  ,rc.[Sex]
      ,rc.[FirstName]
      ,rc.[MiddleName]
      ,rc.[Lastname]
      ,rc.[SecondLastname]
      ,rc.[FullName]
      ,rc.[Nickname]
      ,rc.[MarriedName]
      ,rc.[Title]
      ,rc.[IdentificationNumber]
      ,rc.[BirthDate]


	  ,cli.admissionDate
	  ,cli.creditInformation
	  ,cli.ClientReference
	  ,cli.InactiveComment
	  ,cli.numberDependents
	  ,cli.numberEmployees
	  ,cli.AnnualSales
	  ,cli.AssetValue
	  ,cli.dateIncorporation
	  ,cli.ownHouse
	  ,cli.IsClientSentCollection
	  ,cli.depositType
	  ,cli.TypeNCF


	  ,c.CountryName
	  ,et.rolName
	  ,it.IdentificationName
	  ,ec.ClassName
	  ,pt.[personTypeName]
	  ,mt.MaritalStatusName
	  ,prof.ProfessionName
	  ,com.companyName
	  ,lint.linkedTypeName
	  ,cltc.clientTypeByCompanyName
	  ,cltsib.clientTypeBySIBName

	  --ADDRESS
    ,ISNULL(addPri.IsPrimary,addsec.IsPrimary) AddressIsPrimary
	,ISNULL(addPri.addressType,addsec.addressType) addressType
	,ISNULL(addPri.streetName,addsec.streetName) streetName
	,ISNULL(addPri.streetNumber,addsec.streetNumber) streetNumber
	,ISNULL(addPri.buildingName,addsec.buildingName) buildingName
	,ISNULL(addPri.postalCode,addsec.postalCode) postalCode
	,ISNULL(addPri.postalZone,addsec.postalZone) postalZone
	,ISNULL(addPri.address,addsec.address) [address]
	,ISNULL(addPri.addressAdditional,addsec.addressAdditional) addressAdditional
	,ISNULL(addPri.CountryName,addsec.CountryName) AddressCountryName
	,ISNULL(addPri.provinceName,addsec.provinceName) provinceName
	,ISNULL(addPri.cityName,addsec.cityName) cityName
	,ISNULL(addPri.[physicalSectorName],addsec.[physicalSectorName])[physicalSectorName]
	 
	 
	 --phone 

	,ISNULL(phoPri.AreaCode,phoSec.AreaCode) AreaCode
	,ISNULL(phoPri.Phone,phoSec.Phone) Phone
	,ISNULL(phoPri.PhoneType,phoSec.PhoneType) PhoneType
	,ISNULL(phoPri.Comments,phoSec.Comments) as PhoneComments
	,ISNULL(phoPri.IsPrimary,phoSec.IsPrimary) PhoneIsPrimary
	,ISNULL(phoPri.CountryName,phoSec.CountryName) PhoneCountryName


	--email 

	,ISNULL(emailPri.email,emailSec.email)email
	,ISNULL(emailPri.emailType,emailSec.emailType) emailType
	,ISNULL(emailPri.comments,emailSec.comments) emailComments
	,ISNULL(emailPri.IsPrimary,emailSec.IsPrimary) emailIsPrimary

	  ,cli.[isActive]
      ,cli.[CreateDate]
      ,cli.[ModiDate]
      ,cli.[CreateUsrId]
      ,cli.[ModiUsrId]
      ,cli.[hostName]
  FROM [Entity].[client] cli 
  inner JOIN  [Entity].[relatedContact] rc 
	ON cli.relatedContactId=rc.[relatedContactId]
  LEFT JOIN [global].[Country] c 
	ON c.[countryID] =rc.countryID

  LEFT JOIN Entity.entityRol et 
	ON et.entityRolId=rc.entityRolId

  LEFT JOIN [Entity].[identificationType] it
	ON it.identificationTypeId = rc.identificationTypeId

  LEFT JOIN [Entity].[EntityClass] ec
	ON ec.EntityClassId = rc.EntityClassId

  LEFT JOIN [Entity].[personType] pt
	ON pt.personTypeId = rc.personTypeId

  LEFT JOIN [Entity].[MaritalStatus] mt
	ON mt.MaritalStatusId = rc.MaritalStatusId	
  
  LEFT JOIN [Entity].[Profession] prof
	ON prof.ProfessionId = rc.ProfessionId	

  LEFT JOIN [Entity].[Company] com
	ON com.companyId = cli.companyId	

  LEFT JOIN [Entity].[linkedType] lint
	ON lint.linkedTypeId = cli.linkedTypeId	

  LEFT JOIN [Entity].[clientTypeByCompany] cltc
	ON cltc.clientTypeByCompanyId = cli.clientTypeByCompanyId	

  LEFT JOIN [Entity].[clientTypeBySIB] cltsib
	ON cltsib.clientTypeBySIBId = cli.clientTypeBySIBId	

  OUTER APPLY 
  (
	 SELECT TOP 1 
		 ca.addressId
		,ca.relatedContactId
		,ca.CountryID
		,ca.provinceId
		,ca.cityId
		,ca.physicalSectorId
		,ca.addressType
		,ca.streetName
		,ca.streetNumber
		,ca.buildingName
		,ca.postalCode
		,ca.postalZone
		,ca.address
		,ca.addressAdditional
		,ca.IsPrimary

		,c.CountryName
		,p.provinceName
		,ct.cityName
		,sec.[physicalSectorName]
	FROM [Entity].[contactAddress]  ca

	INNER JOIN [global].[Country] c 
	 ON c.CountryID = ca.CountryID
	

	INNER JOIN [global].[province] p 
	 ON 
	  	  p.CountryID = ca.CountryID 
	  AND p.provinceId = ca.provinceId
	
	INNER JOIN [global].[city] ct 
	 ON 
	  	  ct.CountryID = ca.CountryID 
	  AND ct.provinceId = ca.provinceId
	  AND ct.cityId = ca.cityId

	INNER JOIN [global].[physicalSector] sec 
	 ON 
	  	  sec.CountryID = ca.CountryID 
	  AND sec.cityId = ca.cityId
	  AND sec.physicalSectorId = ca.physicalSectorId

	WHERE 
			ca.IsPrimary = 1
		AND  ca.relatedContactId = rc.relatedContactId
  ) addPri

   OUTER APPLY 
  (
	 SELECT TOP 1 
		 ca.addressId
		,ca.relatedContactId
		,ca.CountryID
		,ca.provinceId
		,ca.cityId
		,ca.physicalSectorId
		,ca.addressType
		,ca.streetName
		,ca.streetNumber
		,ca.buildingName
		,ca.postalCode
		,ca.postalZone
		,ca.address
		,ca.addressAdditional
		,ca.IsPrimary
		,c.CountryName
		,p.provinceName
		,ct.cityName
		,sec.[physicalSectorName]
	FROM [Entity].[contactAddress]  ca

	INNER JOIN [global].[Country] c 
	 ON c.CountryID = ca.CountryID
	

	INNER JOIN [global].[province] p 
	 ON 
	  	  p.CountryID = ca.CountryID 
	  AND p.provinceId = ca.provinceId
	
	INNER JOIN [global].[city] ct 
	 ON 
	  	  ct.CountryID = ca.CountryID 
	  AND ct.provinceId = ca.provinceId
	  AND ct.cityId = ca.cityId

	INNER JOIN [global].[physicalSector] sec 
	 ON 
	  	  sec.CountryID = ca.CountryID 
	  AND sec.cityId = ca.cityId
	  AND sec.physicalSectorId = ca.physicalSectorId

	WHERE 
			 ca.IsPrimary = 0
		AND  ca.relatedContactId = rc.relatedContactId
  ) addsec

  OUTER APPLY 
  (
	SELECT top 1
		 cp.contactPhoneId
		,cp.AreaCode
		,cp.Phone
		,cp.PhoneType
		,cp.Comments
		,cp.IsPrimary
		,c.CountryName
	FROM [Entity].[contactPhones]  cp

	INNER JOIN [global].[Country] c 
	 ON c.CountryID = cp.CountryID

	where cp.IsPrimary=1 and cp.relatedContactId=rc.relatedContactId
  ) phoPri

  OUTER APPLY 
  (
	SELECT top 1
		 cp.contactPhoneId
		,cp.AreaCode
		,cp.Phone
		,cp.PhoneType
		,cp.Comments
		,cp.IsPrimary
		,c.CountryName
	FROM [Entity].[contactPhones]  cp

	INNER JOIN [global].[Country] c 
	 ON c.CountryID = cp.CountryID

	where cp.IsPrimary=0 and cp.relatedContactId=rc.relatedContactId
  ) phoSec

  OUTER APPLY 
  (
	SELECT top 1
		 cp.contactEmailId
		,cp.email
		,cp.emailType
		,cp.comments
		,cp.IsPrimary
	FROM [Entity].contactEmail  cp
	where cp.IsPrimary=1 and cp.relatedContactId=rc.relatedContactId
  ) emailPri

   OUTER APPLY 
  (
	SELECT top 1
		 cp.contactEmailId
		,cp.email
		,cp.emailType
		,cp.comments
		,cp.IsPrimary
	FROM [Entity].contactEmail  cp
	where cp.IsPrimary=0 and cp.relatedContactId=rc.relatedContactId
  ) emailSec

WHERE 
		(cli.clientId = @clientId OR  @clientId IS NULL)
	AND (rc.IdentificationNumber =@IdentificationNumber OR @IdentificationNumber IS NULL)
GO
/****** Object:  StoredProcedure [Entity].[SP_GET_CONTACT_EMAIL]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [Entity].[SP_GET_CONTACT_EMAIL]
   @clientId as bigint
AS
	SELECT
	     cp.relatedContactId
		,cp.contactEmailId
		,cp.email
		,cp.emailType
		,cp.comments
		,cp.IsPrimary
	FROM [Entity].contactEmail  cp
	WHERE cp.relatedContactId=@clientId

GO
/****** Object:  StoredProcedure [Entity].[SP_GET_CONTACT_PHONE]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [Entity].[SP_GET_CONTACT_PHONE]
   @clientId as bigint
AS
SELECT   cp.contactPhoneId
        ,cp.relatedContactId
		,cp.AreaCode
		,cp.Phone
		,cp.PhoneType
		,cp.Comments
		,cp.IsPrimary
		,c.CountryName
	FROM [Entity].[contactPhones]  cp

	INNER JOIN [global].[Country] c 
	 ON c.CountryID = cp.CountryID

	where  cp.relatedContactId= @clientId


GO
/****** Object:  StoredProcedure [Entity].[SP_GET_LOANS_INFORMATIONS]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name: [Entity].[SP_GET_CLIENT_INFORMATIONS]
** Desc:para conseguir las informaciones de los clientes 
** Auth:RALVAREZ
** Date:06/27/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    06/27/2018  Ralvarez				para conseguir las informaciones de los clientes 
**********************************************************************************************************************************************************/
CREATE PROC [Entity].[SP_GET_LOANS_INFORMATIONS]
   @clienteName VARCHAR(1000)=NULL
  ,@identificationNumber VARCHAR(50) = NULL
  ,@quotationId BIGINT = NULL
  ,@accountId BIGINT = NULL
  ,@collateralReference VARCHAR(100) = NULL
  ,@Chassis VARCHAR(100) = ''
AS 

IF (@quotationId <=0)
	SET @quotationId =NULL

IF (@accountId <=0)
	SET @accountId =NULL

IF (RTRIM(LTRIM(@clienteName)) ='')
	SET @clienteName= NULL
IF (RTRIM(LTRIM(@IdentificationNumber)) ='')
	SET @IdentificationNumber= NULL
IF (RTRIM(LTRIM(@collateralReference)) ='')
	SET @collateralReference= NULL
IF (RTRIM(LTRIM(@Chassis)) ='')
	SET @Chassis= NULL
/*
IF (@SearchType='Client')
BEGIN 
	SET @ClientName = @clienteName
END 

IF (@SearchType='Promoter')
BEGIN 
	SET @accountExecutiveContactName = @clienteName
END 

IF (@SearchType='Executive')
BEGIN 
	SET @accountPromoterContactName = @clienteName
END 
*/

SELECT lh.[accountId]
      ,lh.[quotationId]
	  ,rc.[relatedContactId]
	  ,rc.FirstName	
	  ,rc.MiddleName	
	  ,rc.Lastname	
	  ,rc.SecondLastname	
	  ,rc.FullName
	  ,rc.IdentificationNumber
	  ,IdentificationName
	  ,account
	  ,CASE WHEN phoPri.contactPhoneId  IS NOT NULL THEN phoPri.AreaCode  ELSE phoSec.AreaCode END AreaCode
	  ,CASE WHEN phoPri.contactPhoneId  IS NOT NULL THEN phoPri.Phone ELSE phoSec.Phone END Phone
  FROM [Loan].[loanHeader] lh
  INNER JOIN [Entity].[client] c on c.clientId = lh.[relatedContactId]
  INNER JOIN [Entity].[relatedContact] rc on rc.[relatedContactId]= c.[relatedContactId]
  INNER JOIN [Loan].[Account] acc on acc.[accountId]= lh.[accountId]

  left JOIN [Entity].[relatedContact] rcEje on rcEje.[relatedContactId]= acc.accountExecutiveContactId
  left JOIN [Entity].[relatedContact] rcPro on rcPro.[relatedContactId]= acc.accountPromoterContactId




  LEFT JOIN [Entity].[identificationType] indt on indt.identificationTypeId=rc.identificationTypeId
  OUTER APPLY 
  (
	SELECT top 1
		 cp.contactPhoneId
		,cp.AreaCode
		,cp.Phone
		,cp.PhoneType
		,cp.Comments
		,cp.IsPrimary
		,c.CountryName
	FROM [Entity].[contactPhones]  cp

	INNER JOIN [global].[Country] c 
	 ON c.CountryID = cp.CountryID

	where cp.IsPrimary=1 and cp.relatedContactId=rc.relatedContactId
  ) phoPri

  OUTER APPLY 
  (
	SELECT top 1
		 cp.contactPhoneId
		,cp.AreaCode
		,cp.Phone
		,cp.PhoneType
		,cp.Comments
		,cp.IsPrimary
		,c.CountryName
	FROM [Entity].[contactPhones]  cp

	INNER JOIN [global].[Country] c 
	 ON c.CountryID = cp.CountryID

	WHERE cp.IsPrimary=0 and cp.relatedContactId=rc.relatedContactId
  ) phoSec
	
 WHERE (
			( 
					(replace(rc.IdentificationNumber,'-','') like '%' + @IdentificationNumber + '%' )
				OR (replace(rcEje.IdentificationNumber,'-','') like '%' + @IdentificationNumber + '%')
				OR (replace(rcPro.IdentificationNumber,'-','') like '%' + @IdentificationNumber + '%' )
			) OR  @IdentificationNumber IS NULL
		)

		AND 
		(
				(
					   isnull(RC.FullName,'') like '%' + @clienteName + '%' 
					or isnull(rcEje.FullName,'') like '%' + @clienteName + '%'  
					or isnull(rcPro.FullName,'') like '%' + @clienteName + '%'  
				) or @clienteName IS NULL
		)

		AND 
		(
			lh.quotationId =  @quotationId OR @quotationId IS NULL
		)
		AND 
		(
			lh.accountId =  @accountId OR @accountId IS NULL
		)
		AND 
		(
			lh.accountId IN
			(
				SELECT accountId

				FROM [Loan].[loancollateral]
				WHERE 
							collateralId IN (SELECT collateralId FROM [Loan].[policyCollateral] WHERE isnull(policyNo,'') like '%' + @collateralReference + '%'  )
						 OR collateralId IN (SELECT collateralId FROM [Loan].policyCollateralEndorse WHERE isnull(endorseNo,'') like '%' + @collateralReference + '%'  )
			)		OR @collateralReference IS NULL
		)
	   AND 
		(
			lh.accountId IN
			(
				select  l.accountid
				from [Loan].[loanCollateralFieldValue] v
				INNER JOIN [Loan].[loancollateral]  l 
					on v.collateralId=l.collateralId
				where  isnull(collateralFieldValue,'')   like '%' + @Chassis + '%'  

				
			)OR @Chassis IS NULL
		)
				
				


GO
/****** Object:  StoredProcedure [Entity].[SP_SET_CONTACT_EMAIL]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name:Loan.SP_UPDATE_IDENTIFICATION_TYPE
** Desc:Mantenimiento de UPDATE Y INSERT DE LA TABLA DE CORREOS DE LOS CLIENTES
** Auth:RALVAREZ
** Date:08/24/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/24/2018  Dirson Breton			Mantenimiento de UPDATE Y INSERT DE LA TABLA DE CORREOS DE LOS CLIENTES
**********************************************************************************************************************************************************/
CREATE PROC [Entity].[SP_SET_CONTACT_EMAIL]
 @contactEmailId	bigint=NULL
,@relatedContactId	bigint
,@email	varchar(2500)
,@emailType	varchar(50)
,@comments	varchar(2500)
,@IsPrimary	bit
,@isActive	bit
,@userId	int

AS 

DECLARE @SummaryOfChanges TABLE(contactEmailId BIGINT,relatedContactId BIGINT, [Action] varchar(30));  

DECLARE @contactEmailIdMax	bigint=0
SET @contactEmailIdMax =ISNULL((SELECT MAX(contactEmailId) FROM  [Entity].[contactEmail] where relatedContactId=@relatedContactId ),0) + 5000 /*ESTO ES PORQUE TENEMOS UNA SINCRONIZACION CON EL CORE PARA EVITAR LOS ID*/



MERGE INTO [Entity].[contactEmail] AS Target  
	USING (
			VALUES (@contactEmailId,@relatedContactId,@email,@emailType,@comments,@IsPrimary,@isActive,GETDATE(),GETDATE(),@userId,@userId,HOST_NAME())
          )  
       AS Source 
			(
				 contactEmailId
				,relatedContactId
				,email
				,emailType
				,comments
				,IsPrimary
				,isActive
				,CreateDate
				,ModiDate
				,CreateUsrId
				,ModiUsrId
				,hostName
			)  
ON Target.contactEmailId = Source.contactEmailId   AND Target.relatedContactId = Source.relatedContactId  
WHEN MATCHED THEN  
	UPDATE SET
				 email		=Source.email
				,emailType	=Source.emailType
				,comments	=Source.comments
				,IsPrimary	=Source.IsPrimary
				,isActive	=Source.isActive
				,ModiDate=Source.ModiDate
				,ModiUsrId=Source.ModiUsrId
WHEN NOT MATCHED BY TARGET THEN  
	INSERT 
	(
		 contactEmailId
		,relatedContactId
		,email
		,emailType
		,comments
		,IsPrimary
		,isActive
		,CreateDate
		,ModiDate
		,CreateUsrId
		,ModiUsrId
		,hostName
	) VALUES 
		(
			 @contactEmailIdMax
			,relatedContactId
			,email
			,emailType
			,comments
			,IsPrimary
			,isActive
			,CreateDate
			,ModiDate
			,CreateUsrId
			,ModiUsrId
			,hostName
		)  
		
OUTPUT ISNULL(INSERTED.contactEmailId,Deleted.contactEmailId),ISNULL(INSERTED.relatedContactId,Deleted.relatedContactId),$action  INTO @SummaryOfChanges;  

IF (@IsPrimary=1)
BEGIN 
	UPDATE ce SET 
		ce.IsPrimary=0
	FROM [Entity].[contactEmail] ce
	INNER JOIN @SummaryOfChanges c
	 ON ce.relatedContactId=c.relatedContactId

	UPDATE ce SET 
		ce.IsPrimary=1
	FROM [Entity].[contactEmail] ce
	INNER JOIN @SummaryOfChanges c
	 ON ce.contactEmailId=c.contactEmailId
	 AND ce.relatedContactId=c.relatedContactId
END

SELECT *
FROM @SummaryOfChanges  

GO
/****** Object:  StoredProcedure [Entity].[SP_SET_CONTACT_PHONE]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [Entity].[SP_SET_CONTACT_PHONE]
       @contactPhoneId  bigint = null
      ,@relatedContactId bigint
      ,@CountryID int = null
      ,@AreaCode varchar(10) = null
      ,@Phone varchar(50) = null
      ,@PhoneType varchar(5) = null
      ,@Comments varchar(500) = null
      ,@IsPrimary bit = null
      ,@isActive bit = null                  
      ,@UserId int     
AS
BEGIN

SET NOCOUNT ON;

declare @TableResult table
		(
		 [Action] varchar(10),
		 [entityId] bigint		
		)
       
  merge [Entity].[contactPhones] as tt   
  using (
		 select @contactPhoneId,@relatedContactId,@CountryID,@AreaCode,@Phone,@PhoneType,@Comments,@IsPrimary,@isActive,@UserId    
		) 
		as ts
			(
				[contactPhoneId],[relatedContactId],[CountryID],[AreaCode],[Phone],[PhoneType],[Comments],[IsPrimary],[isActive],[UsrId]   
			)
	on (		
	 			tt.[relatedContactId] = ts.[relatedContactId]
			and tt.[contactPhoneId] = ts.[contactPhoneId]
		)
		when matched then
		update set 
			 tt.[CountryID] = isnull(ts.[CountryID],tt.[CountryID])
			,tt.[AreaCode] = isnull(ts.[AreaCode],tt.[AreaCode])
			,tt.[Phone] = isnull(ts.[Phone],tt.[Phone])
			,tt.[PhoneType] = isnull(ts.[PhoneType],tt.[PhoneType])
			,tt.[Comments] = isnull(ts.[Comments],tt.[Comments])
			,tt.[IsPrimary] = isnull(ts.[IsPrimary],tt.[IsPrimary])
			,tt.[isActive] = isnull(ts.[isActive],tt.[isActive])
			,tt.[ModiDate] = getdate()	
			,tt.[ModiUsrId] = @UserId
			,tt.[hostname] = host_name()		

	output $action
		  ,isnull(deleted.[contactPhoneId], inserted.[contactPhoneId])
						
	into @TableResult;

	select 
		 [Action]
		,[entityId]		
	from
		@TableResult
END
GO
/****** Object:  StoredProcedure [Entity].[SP_SET_CONTACT_PHONES]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name:Loan.SP_UPDATE_IDENTIFICATION_TYPE
** Desc:Mantenimiento de UPDATE Y INSERT DE LA TABLA DE TELEFONOS DE LOS CLIENTES
** Auth:RALVAREZ
** Date:08/24/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/24/2018  Dirson Breton			Mantenimiento de UPDATE Y INSERT DE LA TABLA DE TELEFONOS DE LOS CLIENTES
**********************************************************************************************************************************************************/
CREATE PROC [Entity].[SP_SET_CONTACT_PHONES]
 @contactPhoneId	bigint=NULL
,@relatedContactId	bigint
,@CountryID	int
,@AreaCode	varchar(5)
,@Phone	varchar(50)
,@PhoneType	varchar(5)
,@comments varchar(500)
,@IsPrimary	bit
,@isActive	bit
,@userId	int

AS 


DECLARE @SummaryOfChanges TABLE(contactPhoneId BIGINT,relatedContactId BIGINT, [Action] varchar(30));  

DECLARE @contactPhoneIdMax	bigint=0
SET @contactPhoneIdMax =ISNULL((SELECT MAX(contactPhoneId) FROM  [Entity].[contactPhones] WHERE relatedContactId=@relatedContactId ),0) + 5000 /*ESTO ES PORQUE TENEMOS UNA SINCRONIZACION CON EL CORE PARA EVITAR LOS ID*/



MERGE INTO [Entity].[contactPhones] AS Target  
	USING (
			VALUES (@contactPhoneId,@relatedContactId,@CountryID,@AreaCode,@Phone,@PhoneType,@comments,@IsPrimary,@isActive,GETDATE(),GETDATE(),@userId,@userId,HOST_NAME())
          )  
       AS Source 
			(
				 contactPhoneId
				,relatedContactId
				,CountryID
				,AreaCode
				,Phone
				,PhoneType
				,Comments
				,IsPrimary
				,isActive
				,CreateDate
				,ModiDate
				,CreateUsrId
				,ModiUsrId
				,hostName
			)  
ON Target.contactPhoneId = Source.contactPhoneId   AND Target.relatedContactId = Source.relatedContactId  
WHEN MATCHED THEN  
	UPDATE SET
				 CountryID	=Source.CountryID
				,AreaCode	=Source.AreaCode
				,Phone		=Source.Phone
				,PhoneType	=Source.PhoneType
				,Comments	=Source.Comments
				,IsPrimary	=Source.IsPrimary
				,isActive	=Source.isActive
WHEN NOT MATCHED BY TARGET THEN  
	INSERT 
	(
		 contactPhoneId
		,relatedContactId
		,CountryID
		,AreaCode
		,Phone
		,PhoneType
		,Comments
		,IsPrimary
		,isActive
		,CreateDate
		,ModiDate
		,CreateUsrId
		,ModiUsrId
		,hostName
	) VALUES 
		(
			 @contactPhoneIdMax
			,relatedContactId
			,CountryID
			,AreaCode
			,Phone
			,PhoneType
			,Comments
			,IsPrimary
			,isActive
			,CreateDate
			,ModiDate
			,CreateUsrId
			,ModiUsrId
			,hostName
		)  
		
OUTPUT ISNULL(INSERTED.contactPhoneId,Deleted.contactPhoneId),ISNULL(INSERTED.relatedContactId,Deleted.relatedContactId),$action  INTO @SummaryOfChanges;  

IF (@IsPrimary=1)
BEGIN 
	UPDATE ce SET 
		ce.IsPrimary=0
	FROM [Entity].[contactPhones] ce
	INNER JOIN @SummaryOfChanges c
	 ON ce.relatedContactId=c.relatedContactId

	UPDATE ce SET 
		ce.IsPrimary=1
	FROM [Entity].[contactPhones] ce
	INNER JOIN @SummaryOfChanges c
	 ON ce.contactPhoneId=c.contactPhoneId
	 AND ce.relatedContactId=c.relatedContactId
END


SELECT *
FROM @SummaryOfChanges  

GO
/****** Object:  StoredProcedure [Entity].[SP_UPDATE_BUSINESS_TYPE]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name:Entity.SP_UPDATE_BUSINESS_TYPE
** Desc:Creado para actualizar los registros en [Entity].[businessType]
** Auth:Dirson Breton
** Date:08/27/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/27/2018  Dirson Breton			Creado para actualizar los registros en [Entity].[businessType]
**********************************************************************************************************************************************************/
CREATE PROCEDURE [Entity].[SP_UPDATE_BUSINESS_TYPE]
(
 @businessTypeId INT
,@businessTypeName VARCHAR(150)
,@businessTypeCode VARCHAR(50)
,@isActive BIT
,@CreateDate DATETIME
,@ModiDate DATETIME
,@CreateUsrId INT
,@ModiUsrId INT
,@hostName VARCHAR(100)
,@Action BIT
)
AS
BEGIN
	IF @Action = 1
		BEGIN
			UPDATE Entity.businessType
			SET
			businessTypeName = @businessTypeName,
			businessTypeCode = @businessTypeCode,
			isActive = @isActive,
			CreateDate = @CreateDate,
			ModiDate = @ModiDate,
			CreateUsrId = @CreateUsrId,
			ModiUsrId = @ModiUsrId,
			hostName = @hostName
			WHERE businessTypeId = @businessTypeId 
		END
	ELSE
		BEGIN
			UPDATE Entity.businessType
			SET
			isActive = 0
			WHERE businessTypeId = @businessTypeId
		END
END

GO
/****** Object:  StoredProcedure [Entity].[SP_UPDATE_CLIENT]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name:global.SP_UPDATE_CITY
** Desc:Creado para actualizar los registros en [Entity].[client]
** Auth:Ralvarez
** Date:08/29/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/29/2018  Ralvarez   			    Creado para actualizar los registros en [Entity].[client]
**********************************************************************************************************************************************************/
CREATE PROCEDURE [Entity].[SP_UPDATE_CLIENT]
(
	 @clientId	bigint	
	,@relatedContactId	bigint	
	,@companyId	int	
	,@linkedTypeId	int	
	,@clientTypeByCompanyId	int	
	,@reasonId	int	
	,@clientTypeBySIBId	int	
	,@clientName	varchar(150)
	,@admissionDate	datetime	
	,@creditInformation	bit	
	,@ClientReference	varchar(200)
	,@InactiveComment	varchar(500)
	,@numberDependents	int	
	,@numberEmployees	int	
	,@AnnualSales	numeric(18,3)	
	,@AssetValue	numeric	(18,3)	
	,@dateIncorporation	date	
	,@ownHouse	bit	
	,@IsClientSentCollection	bit	
	,@depositType	varchar(1)
	,@TypeNCF	varchar(5)
	,@isActive	bit	
	,@Action BIT
)
AS
BEGIN
	IF @Action = 1
		BEGIN
			UPDATE [Entity].[client]
			SET
				
				 relatedContactId		=@relatedContactId
				,companyId				=@companyId
				,linkedTypeId			=@linkedTypeId
				,clientTypeByCompanyId	=@clientTypeByCompanyId
				,reasonId				=@reasonId
				,clientTypeBySIBId		=@clientTypeBySIBId
				,clientName				=@clientName
				,admissionDate			=@admissionDate
				,creditInformation		=@creditInformation
				,ClientReference		=@ClientReference
				,InactiveComment		=@InactiveComment
				,numberDependents		=@numberDependents
				,numberEmployees		=@numberEmployees
				,AnnualSales			=@AnnualSales
				,AssetValue				=@AssetValue
				,dateIncorporation		=@dateIncorporation
				,ownHouse				=@ownHouse
				,IsClientSentCollection	=@IsClientSentCollection
				,depositType			=@depositType
				,TypeNCF				=@TypeNCF
				,isActive				=@isActive
			WHERE  clientId=@clientId
		END
	ELSE
		BEGIN
			UPDATE [Entity].[client]
			SET
			isActive = 0
			WHERE  clientId=@clientId
		END
END

GO
/****** Object:  StoredProcedure [Entity].[SP_UPDATE_CLIENT_TYPE_BY_COMPANY]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name:Entity.SP_UPDATE_CLIENT_TYPE_BY_COMPANY
** Desc:Creado para actualizar los registros en [Entity].[clientTypeByCompany]
** Auth:Dirson Breton
** Date:08/27/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/27/2018  Dirson Breton			Creado para actualizar los registros en [Entity].[clientTypeByCompany]
**********************************************************************************************************************************************************/
CREATE PROCEDURE [Entity].[SP_UPDATE_CLIENT_TYPE_BY_COMPANY]
(
 @clientTypeByCompanyId INT
,@clientTypeByCompanyName VARCHAR(150)
,@clientTypeByCompanyCode VARCHAR(50)
,@isActive BIT
,@CreateDate DATETIME
,@ModiDate DATETIME
,@CreateUsrId INT
,@ModiUsrId INT
,@hostName VARCHAR(100)
,@Action BIT
)
AS
BEGIN
	IF @Action = 1
		BEGIN
			UPDATE Entity.clientTypeByCompany
			SET
			clientTypeByCompanyName = @clientTypeByCompanyName,
			clientTypeByCompanyCode = @clientTypeByCompanyCode,
			isActive = @isActive,
			CreateDate = @CreateDate,
			ModiDate = @ModiDate,
			CreateUsrId = @CreateUsrId,
			ModiUsrId = @ModiUsrId,
			hostName = @hostName
			WHERE clientTypeByCompanyId = @clientTypeByCompanyId 
		END
	ELSE
		BEGIN
			UPDATE Entity.clientTypeByCompany
			SET
			isActive = 0
			WHERE clientTypeByCompanyId = @clientTypeByCompanyId
		END
END

GO
/****** Object:  StoredProcedure [Entity].[SP_UPDATE_CLIENT_TYPE_BY_SIB]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name:Entity.SP_UPDATE_CLIENT_TYPE_BY_SIB
** Desc:Creado para actualizar los registros en Entity.clientTypeBySIB
** Auth:Dirson Breton
** Date:08/27/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/27/2018  Dirson Breton			Creado para actualizar los registros en Entity.clientTypeBySIB
**********************************************************************************************************************************************************/
CREATE PROCEDURE [Entity].[SP_UPDATE_CLIENT_TYPE_BY_SIB]
(
 @clientTypeBySIBId INT
,@EntityClassId INT
,@clientTypeBySIBName VARCHAR(150)
,@clientTypeBySIBCode VARCHAR(50)
,@Isfinancial BIT
,@isActive BIT
,@CreateDate DATETIME
,@ModiDate DATETIME
,@CreateUsrId INT
,@ModiUsrId INT
,@hostName VARCHAR(100)
,@Action BIT
)
AS
BEGIN
	IF @Action = 1
		BEGIN
			UPDATE Entity.clientTypeBySIB
			SET
			EntityClassId = @EntityClassId,
			clientTypeBySIBName = @clientTypeBySIBName,
			clientTypeBySIBCode = @clientTypeBySIBCode,
			Isfinancial = @Isfinancial,
			isActive = @isActive,
			CreateDate = @CreateDate,
			ModiDate = @ModiDate,
			CreateUsrId = @CreateUsrId,
			ModiUsrId = @ModiUsrId,
			hostName = @hostName
			WHERE clientTypeBySIBId = @clientTypeBySIBId 
		END
	ELSE
		BEGIN
			UPDATE Entity.clientTypeBySIB
			SET
			isActive = 0
			WHERE clientTypeBySIBId = @clientTypeBySIBId 
		END
END

GO
/****** Object:  StoredProcedure [Entity].[SP_UPDATE_COMPANY]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name:Entity.SP_UPDATE_COMPANY
** Desc:Creado para actualizar los registros en [Entity].[Company]
** Auth:Dirson Breton
** Date:08/29/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/29/2018  Dirson Breton			Creado para actualizar los registros en [Entity].[Company]
**********************************************************************************************************************************************************/
CREATE PROCEDURE [Entity].[SP_UPDATE_COMPANY]
(
 @companyId INT
,@currencyId INT
,@relatedContactId INT
,@companyCode VARCHAR(50)
,@companyName VARCHAR(150)
,@rnc VARCHAR(50)
,@companyHoldingId INT
,@isActive BIT
,@CreateDate DATETIME
,@ModiDate DATETIME
,@CreateUsrId INT
,@ModiUsrId INT
,@hostName VARCHAR(100)
,@Action BIT
)
AS
BEGIN
	IF @Action = 1
		BEGIN
			UPDATE [Entity].Company
			SET
			currencyId = @currencyId,
			relatedContactId = @relatedContactId,
			companyCode = @companyCode,
			companyName = @companyName,
			rnc = @rnc,
			companyHoldingId = @companyHoldingId,
			isActive = @isActive,
			CreateDate = @CreateDate,
			ModiDate = @ModiDate,
			CreateUsrId = @CreateUsrId,
			ModiUsrId = @ModiUsrId,
			hostName = @hostName
			WHERE companyId = @companyId
		END
	ELSE
		BEGIN
			UPDATE [Entity].Company
			SET
			isActive = 0
			WHERE companyId = @companyId
		END
END

GO
/****** Object:  StoredProcedure [Entity].[SP_UPDATE_CONTACT_ADDRESS]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name:[Entity].[SP_UPDATE_CONTACT_ADDRESS]
** Desc:Creado para actualizar los registros en [Entity].[contactAddress]
** Auth:Ralvarez
** Date:08/29/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/29/2018  Ralvarez   			    Creado para actualizar los registros en [Entity].[contactAddress]
**********************************************************************************************************************************************************/	

CREATE PROCEDURE [Entity].[SP_UPDATE_CONTACT_ADDRESS]
(
	 @addressId	BIGINT	
	,@relatedContactId	BIGINT	
	,@CountryID	INT	
	,@provinceId	INT	
	,@cityId	INT	
	,@physicalSectorId	INT	
	,@addressType	VARCHAR(50)
	,@streetName	VARCHAR(250)
	,@streetNumber	VARCHAR(50)
	,@buildingName	VARCHAR(250)
	,@postalCode	VARCHAR(50)
	,@postalZone	VARCHAR(50)
	,@address	VARCHAR(500)
	,@addressAdditional	VARCHAR(500)
	,@IsPrimary	BIT	
	,@isActive	BIT	
	,@Action BIT
)
AS
BEGIN 
	IF @Action = 1
		BEGIN
			UPDATE [Entity].[contactAddress] SET 
				 CountryID			=@CountryID
				,provinceId			=@provinceId
				,cityId				=@cityId
				,physicalSectorId	=@physicalSectorId
				,addressType		=@addressType
				,streetName			=@streetName
				,streetNumber		=@streetNumber
				,buildingName		=@buildingName
				,postalCode			=@postalCode
				,postalZone			=@postalZone
				,[address]			=@address
				,addressAdditional	=@addressAdditional
				,IsPrimary			=@IsPrimary
				,isActive			=@isActive
			WHERE addressId=@addressId AND relatedContactId=@relatedContactId
		END
		/*ELSE 
		BEGIN 
			UPDATE [Entity].[contactPhones] SET 
				 isActive	=0
			WHERE contactPhoneId=@contactPhoneId AND relatedContactId=@relatedContactId
		END */
END 


GO
/****** Object:  StoredProcedure [Entity].[SP_UPDATE_CONTACT_EMAIL]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name:[Entity].[SP_UPDATE_CONTACT_EMAIL]
** Desc:Creado para actualizar los registros en [Entity].[contactEmail]
** Auth:Ralvarez
** Date:08/29/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/29/2018  Ralvarez   			    Creado para actualizar los registros en [Entity].[contactEmail]
**********************************************************************************************************************************************************/	
CREATE PROCEDURE [Entity].[SP_UPDATE_CONTACT_EMAIL]
(
	 @contactEmailId	bigint	
	,@relatedContactId	bigint	
	,@email	varchar(2500)
	,@emailType	varchar(50)
	,@comments	varchar(2500)
	,@IsPrimary	bit	
	,@isActive	bit	
	,@Action BIT
)
AS
BEGIN 
	IF @Action = 1
		BEGIN
			UPDATE [Entity].[contactEmail] SET 
				 contactEmailId		=@contactEmailId
				,relatedContactId	=@relatedContactId
				,email				=@email
				,emailType			=@emailType
				,comments			=@comments
				,IsPrimary			=@IsPrimary
				,isActive			=@isActive
			WHERE contactEmailId=@contactEmailId AND relatedContactId=@relatedContactId
		END
		/*ELSE 
		BEGIN 
			UPDATE [Entity].[contactPhones] SET 
				 isActive	=0
			WHERE contactPhoneId=@contactPhoneId AND relatedContactId=@relatedContactId
		END */
END 
GO
/****** Object:  StoredProcedure [Entity].[SP_UPDATE_CONTACT_PHONE]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name:global.[SP_UPDATE_CONTACT_PHONE
** Desc:Creado para actualizar los registros en [Entity].[client]
** Auth:Ralvarez
** Date:08/29/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/29/2018  Ralvarez   			    Creado para actualizar los registros en [Entity].[client]
**********************************************************************************************************************************************************/
CREATE PROCEDURE [Entity].[SP_UPDATE_CONTACT_PHONE]
(
	 @contactPhoneId	bigint	
	,@relatedContactId	bigint	
	,@CountryID	int	
	,@AreaCode	varchar(10)
	,@Phone	varchar(50)
	,@PhoneType	varchar(5)
	,@Comments	varchar(500)
	,@IsPrimary	bit	
	,@isActive	bit	
	,@Action BIT
)
AS
BEGIN 
	IF @Action = 1
		BEGIN
			UPDATE [Entity].[contactPhones] SET 
				 CountryID	=@CountryID
				,AreaCode	=@AreaCode
				,Phone		=@Phone
				,PhoneType	=@PhoneType
				,Comments	=@Comments
				,IsPrimary	=@IsPrimary
				,isActive	=@isActive
			WHERE contactPhoneId=@contactPhoneId AND relatedContactId=@relatedContactId
		END
		/*ELSE 
		BEGIN 
			UPDATE [Entity].[contactPhones] SET 
				 isActive	=0
			WHERE contactPhoneId=@contactPhoneId AND relatedContactId=@relatedContactId
		END */
END 
GO
/****** Object:  StoredProcedure [Entity].[SP_UPDATE_ENTITY_ROL]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name:Entity.SP_UPDATE_ENTITY_ROL
** Desc:Creado para actualizar los registros en [Entity].[entityRol]
** Auth:Dirson Breton
** Date:08/28/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/28/2018  Dirson Breton			Creado para actualizar los registros en [Entity].[entityRol]
**********************************************************************************************************************************************************/
CREATE PROCEDURE [Entity].[SP_UPDATE_ENTITY_ROL]
(
 @rolName VARCHAR(150)
,@rolAbbreviation VARCHAR(50)
,@ISBlock BIT
,@isActive BIT
,@CreateDate DATETIME
,@ModiDate DATETIME
,@CreateUsrId INT
,@ModiUsrId INT
,@hostName VARCHAR(100)
,@Action BIT
)
AS
BEGIN
	IF @Action = 1
		BEGIN
			UPDATE [Entity].entityRol
			SET
			rolName = @rolName,
			ISBlock = @ISBlock,
			isActive = @isActive,
			CreateDate = @CreateDate,
			ModiDate = @ModiDate,
			CreateUsrId = @CreateUsrId,
			ModiUsrId = @ModiUsrId,
			hostName = @hostName
			WHERE rolAbbreviation = @rolAbbreviation
		END
	ELSE
		BEGIN
			UPDATE [Entity].entityRol
			SET
			isActive = 0
			WHERE rolAbbreviation = @rolAbbreviation
		END
END

GO
/****** Object:  StoredProcedure [Entity].[SP_UPDATE_IDENTIFICATION_TYPE]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name:Loan.SP_UPDATE_IDENTIFICATION_TYPE
** Desc:Creado para actualizar los registros en entity.identificationType 
** Auth:Dirson Breton
** Date:08/20/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/17/2018  Dirson Breton			Creado para actualizar los registros en entity.identificationType 
**********************************************************************************************************************************************************/
CREATE PROCEDURE [Entity].[SP_UPDATE_IDENTIFICATION_TYPE]
(
 @identificationTypeId INT
,@IdentificationName VARCHAR(150)
,@IdentificationCode VARCHAR(150)
,@PersonType VARCHAR(50)
,@isActive BIT
,@CreateDate DATETIME
,@ModiDate DATETIME
,@CreateUsrId INT
,@ModiUsrId INT
,@hostName VARCHAR(100)
,@Action BIT
)
AS
BEGIN
	IF @Action = 1
		BEGIN
			UPDATE entity.identificationType 
			SET
			IdentificationName = @IdentificationName,
			IdentificationCode = @IdentificationCode,
			PersonType = @PersonType,
			isActive = @isActive,
			CreateDate = @CreateDate,
			ModiDate = @ModiDate,
			CreateUsrId = @CreateUsrId,
			ModiUsrId = @ModiUsrId,
			hostnName = @hostName
			WHERE identificationTypeId = @identificationTypeId
		END
	ELSE
		BEGIN
			UPDATE entity.identificationType 
			SET
			isActive = 0
			WHERE identificationTypeId = @identificationTypeId
		END
END

GO
/****** Object:  StoredProcedure [Entity].[SP_UPDATE_LINKED_TYPE]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name:Entity.SP_UPDATE_LINKED_TYPE
** Desc:Creado para actualizar los registros en [Entity].[linkedType]
** Auth:Dirson Breton
** Date:08/27/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/27/2018  Dirson Breton			Creado para actualizar los registros en [Entity].[linkedType]
**********************************************************************************************************************************************************/
CREATE PROCEDURE [Entity].[SP_UPDATE_LINKED_TYPE]
(
 @linkedTypeId INT
,@linkedTypeName VARCHAR(150)
,@linkedTypeCode VARCHAR(50)
,@isActive BIT
,@CreateDate DATETIME
,@ModiDate DATETIME
,@CreateUsrId INT
,@ModiUsrId INT
,@hostName VARCHAR(100)
,@Action BIT
)
AS
BEGIN
	IF @Action = 1
		BEGIN
			UPDATE Entity.linkedType
			SET
			linkedTypeName = @linkedTypeName,
			linkedTypeId = @linkedTypeId,
			isActive = @isActive,
			CreateDate = @CreateDate,
			ModiDate = @ModiDate,
			CreateUsrId = @CreateUsrId,
			ModiUsrId = @ModiUsrId,
			hostName = @hostName
			WHERE linkedTypeCode = @linkedTypeCode 
		END
	ELSE
		BEGIN
			UPDATE Entity.linkedType
			SET
			isActive = 0
			WHERE linkedTypeCode = @linkedTypeCode
		END
END

GO
/****** Object:  StoredProcedure [Entity].[SP_UPDATE_OFFICE]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name:Entity.SP_UPDATE_OFFICE
** Desc:Creado para actualizar los registros en [Entity].[office]
** Auth:Dirson Breton
** Date:08/29/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/29/2018  Dirson Breton			Creado para actualizar los registros en [Entity].[office]
**********************************************************************************************************************************************************/
CREATE PROCEDURE [Entity].[SP_UPDATE_OFFICE]
(
 @officeId INT
,@companyId INT
,@relatedContactId INT
,@officeName VARCHAR(150)
,@isActive BIT
,@CreateDate DATETIME
,@ModiDate DATETIME
,@CreateUsrId INT
,@ModiUsrId INT
,@hostName VARCHAR(100)
,@Action BIT
)
AS
BEGIN
	IF @Action = 1
		BEGIN
			UPDATE [Entity].office
			SET
			companyId = @companyId,
			relatedContactId = @relatedContactId,
			officeName = @officeName,
			isActive = @isActive,
			CreateDate = @CreateDate,
			ModiDate = @ModiDate,
			CreateUsrId = @CreateUsrId,
			ModiUsrId = @ModiUsrId,
			hostName = @hostName
			WHERE officeId = @officeId
		END
	ELSE
		BEGIN
			UPDATE [Entity].office
			SET
			isActive = 0
			WHERE officeId = @officeId
		END
END

GO
/****** Object:  StoredProcedure [Entity].[SP_UPDATE_PROFESSION]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name:Entity.SP_UPDATE_PROFESSION
** Desc:Creado para actualizar los registros en [Entity].[Profession]
** Auth:Dirson Breton
** Date:08/28/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/28/2018  Dirson Breton			Creado para actualizar los registros en [Entity].[Profession]
**********************************************************************************************************************************************************/
CREATE PROCEDURE [Entity].[SP_UPDATE_PROFESSION]
(
 @ProfessionId INT
,@ProfessionName VARCHAR(150)
,@isActive BIT
,@CreateDate DATETIME
,@ModiDate DATETIME
,@CreateUsrId INT
,@ModiUsrId INT
,@hostName VARCHAR(100)
,@Action BIT
)
AS
BEGIN
	IF @Action = 1
		BEGIN
			UPDATE [Entity].[Profession]
			SET
			ProfessionName = @ProfessionName,
			isActive = @isActive,
			CreateDate = @CreateDate,
			ModiDate = @ModiDate,
			CreateUsrId = @CreateUsrId,
			ModiUsrId = @ModiUsrId,
			hostName = @hostName
			WHERE ProfessionId = @ProfessionId
		END
	ELSE
		BEGIN
			UPDATE [Entity].[Profession]
			SET
			isActive = 0
			WHERE ProfessionId = @ProfessionId
		END
END

GO
/****** Object:  StoredProcedure [global].[SP_UPDATE_ACCOUNT_TYPE]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name:global.SP_UPDATE_ACCOUNT_TYPESP_UPDATE_ACCOUNT_TYPE
** Desc:Creado para actualizar los registros en [global].[accountType]
** Auth:Dirson Breton
** Date:08/20/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/17/2018  Dirson Breton			Creado para actualizar los registros en [global].[accountType]
**********************************************************************************************************************************************************/
CREATE PROCEDURE [global].[SP_UPDATE_ACCOUNT_TYPE]
(
 @accountTypeID INT
,@accountTypeName VARCHAR(150)
,@accountTypeCode VARCHAR(50)
,@isActive BIT
,@CreateDate DATETIME
,@ModiDate DATETIME
,@CreateUsrId INT
,@ModiUsrId INT
,@hostName VARCHAR(100)
,@Action BIT
)
AS
BEGIN
	IF @Action = 1
		BEGIN
			UPDATE global.accountType
			SET
			accountTypeID = @accountTypeID,
			accountTypeName = @accountTypeName,
			isActive = @isActive,
			CreateDate = @CreateDate,
			ModiDate = @ModiDate,
			CreateUsrId = @CreateUsrId,
			ModiUsrId = @ModiUsrId,
			hostName = @hostName
			WHERE accountTypeCode = @accountTypeCode
		END
	ELSE
		BEGIN
			UPDATE global.accountType
			SET
			isActive = 0
			WHERE accountTypeCode = @accountTypeCode
		END
END

GO
/****** Object:  StoredProcedure [global].[SP_UPDATE_BUSINESS_LINE]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name:global.SP_UPDATE_BUSINESS_LINE
** Desc:Creado para actualizar los registros en [global].[businessLine]
** Auth:Dirson Breton
** Date:08/27/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/27/2018  Dirson Breton			Creado para actualizar los registros en [global].[businessLine]
**********************************************************************************************************************************************************/
CREATE PROCEDURE [global].[SP_UPDATE_BUSINESS_LINE]
(
 @businessLineId INT
,@businessLineName VARCHAR(150)
,@businessLineCode VARCHAR(50)
,@isActive BIT
,@CreateDate DATETIME
,@ModiDate DATETIME
,@CreateUsrId INT
,@ModiUsrId INT
,@hostName VARCHAR(100)
,@Action BIT
)
AS
BEGIN
	IF @Action = 1
		BEGIN
			UPDATE global.businessLine
			SET
			businessLineName = @businessLineName,
			businessLineCode = @businessLineCode,
			isActive = @isActive,
			CreateDate = @CreateDate,
			ModiDate = @ModiDate,
			CreateUsrId = @CreateUsrId,
			ModiUsrId = @ModiUsrId,
			hostName = @hostName
			WHERE businessLineId = @businessLineId 
		END
	ELSE
		BEGIN
			UPDATE global.businessLine
			SET
			isActive = 0
			WHERE businessLineId = @businessLineId
		END
END

GO
/****** Object:  StoredProcedure [global].[SP_UPDATE_CITY]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name:global.SP_UPDATE_CITY
** Desc:Creado para actualizar los registros en global.city
** Auth:Dirson Breton
** Date:08/27/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/27/2018  Dirson Breton			Creado para actualizar los registros en global.city
**********************************************************************************************************************************************************/
CREATE PROCEDURE [global].[SP_UPDATE_CITY]
(
 @CountryID INT
,@provinceId INT
,@cityId INT
,@cityName VARCHAR(150)
,@isActive BIT
,@CreateDate DATETIME
,@ModiDate DATETIME
,@CreateUsrId INT
,@ModiUsrId INT
,@hostName VARCHAR(100)
,@Action BIT
)
AS
BEGIN
	IF @Action = 1
		BEGIN
			UPDATE global.city
			SET
			cityName = @cityName,
			isActive = @isActive,
			CreateDate = @CreateDate,
			ModiDate = @ModiDate,
			CreateUsrId = @CreateUsrId,
			ModiUsrId = @ModiUsrId,
			hostName = @hostName
			WHERE CountryID = @CountryID AND provinceId = @provinceId AND cityId = @cityId
		END
	ELSE
		BEGIN
			UPDATE global.city
			SET
			isActive = 0
			WHERE CountryID = @CountryID AND provinceId = @provinceId AND cityId = @cityId
		END
END

GO
/****** Object:  StoredProcedure [global].[SP_UPDATE_CONCEPT_TYPE]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name:Loan.SP_UPDATE_CONCEPT_TYPE
** Desc:Creado para actualizar los registros en [global].[ConceptType]
** Auth:Dirson Breton
** Date:08/20/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/17/2018  Dirson Breton			Creado para actualizar los registros en [global].[ConceptType]
**********************************************************************************************************************************************************/
CREATE PROCEDURE [global].[SP_UPDATE_CONCEPT_TYPE]
(
 @companyId INT
,@ConceptTypeId INT
,@currencyId INT
,@accountTypeID INT
,@ConceptTypeName VARCHAR(250)
,@Yearlength INT
,@InterestRateMinimun NUMERIC(18,3)
,@InterestRateMaximun NUMERIC(18,3)
,@InterestRateType VARCHAR(50)
,@LineBusiness INT
,@isActive BIT
,@CreateDate DATETIME
,@ModiDate DATETIME
,@CreateUsrId INT
,@ModiUsrId INT
,@hostName VARCHAR(100)
,@Action BIT
)
AS
BEGIN
	IF @Action = 1
		BEGIN
			UPDATE global.ConceptType 
			SET
			currencyId = @currencyId,
			accountTypeID = @accountTypeID,
			ConceptTypeName = @ConceptTypeName,
			Yearlength = @Yearlength,
			InterestRateMinimun = @InterestRateMinimun,
			InterestRateMaximun = @InterestRateMaximun,
			InterestRateType = @InterestRateType,
			LineBusiness = @LineBusiness,
			isActive = @isActive,
			CreateDate = @CreateDate,
			ModiDate = @ModiDate,
			CreateUsrId = @CreateUsrId,
			ModiUsrId = @ModiUsrId,
			hostName = @hostName
			WHERE companyId = @companyId
			AND ConceptTypeId = @ConceptTypeId
		END
	ELSE
		BEGIN
			UPDATE global.ConceptType 
			SET
			isActive = 0
			WHERE companyId = @companyId
			AND ConceptTypeId = @ConceptTypeId
		END
END

GO
/****** Object:  StoredProcedure [global].[SP_UPDATE_COUNTRY]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name:global.SP_UPDATE_COUNTRY
** Desc:Creado para actualizar los registros en global.Country
** Auth:Dirson Breton
** Date:08/23/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/17/2018  Dirson Breton			Creado para actualizar los registros en global.Country
**********************************************************************************************************************************************************/
CREATE PROCEDURE [global].[SP_UPDATE_COUNTRY]
(
 @CountryID INT
,@currencyId INT
,@CountryName VARCHAR(150)
,@CountryAbbreviation VARCHAR(50)
,@PhoneAccessCodes VARCHAR(500)
,@Demonym VARCHAR(150)
,@phoneMask VARCHAR(50)
,@isActive BIT
,@CreateDate DATETIME
,@ModiDate DATETIME
,@CreateUsrId INT
,@ModiUsrId INT
,@hostName VARCHAR(100)
,@Action BIT
)
AS
BEGIN
	IF @Action = 1
		BEGIN
			UPDATE global.Country
			SET
			currencyId = @currencyId,
			CountryName = @CountryName,
			CountryAbbreviation = @CountryAbbreviation,
			PhoneAccessCodes = @PhoneAccessCodes,
			Demonym = @Demonym,
			phoneMask = @phoneMask,
			isActive = @isActive,
			CreateDate = @CreateDate,
			ModiDate = @ModiDate,
			CreateUsrId = @CreateUsrId,
			ModiUsrId = @ModiUsrId,
			hostName = @hostName
			WHERE CountryID = @CountryID
		END
	ELSE
		BEGIN
			UPDATE global.Country
			SET
			isActive = 0
			WHERE CountryID = @CountryID
		END
END

GO
/****** Object:  StoredProcedure [global].[SP_UPDATE_CURRENCY]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name:global.SP_UPDATE_CURRENCY
** Desc:Creado para actualizar los registros en [global].[Currency]
** Auth:Dirson Breton
** Date:08/29/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/29/2018  Dirson Breton			Creado para actualizar los registros en [global].[Currency]
**********************************************************************************************************************************************************/
CREATE PROCEDURE [global].[SP_UPDATE_CURRENCY]
(
 @currencyId INT
,@currencyName VARCHAR(150)
,@currencyAbbreviation VARCHAR(50)
,@isActive BIT
,@CreateDate DATETIME
,@ModiDate DATETIME
,@CreateUsrId INT
,@ModiUsrId INT
,@hostName VARCHAR(100)
,@Action BIT
)
AS
BEGIN
	IF @Action = 1
		BEGIN
			UPDATE global.Currency
			SET
			currencyName = @currencyName,
			currencyAbbreviation = @currencyAbbreviation,
			isActive = @isActive,
			CreateDate = @CreateDate,
			ModiDate = @ModiDate,
			CreateUsrId = @CreateUsrId,
			ModiUsrId = @ModiUsrId,
			hostName = @hostName
			WHERE currencyId = @currencyId
		END
	ELSE
		BEGIN
			UPDATE global.Currency
			SET
			isActive = 0
			WHERE currencyId = @currencyId
		END
END

GO
/****** Object:  StoredProcedure [global].[SP_UPDATE_DESTINATION_FUND]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name:global.SP_UPDATE_DESTINATION_FUND
** Desc:Creado para actualizar los registros en [global].[destinationFund]
** Auth:Dirson Breton
** Date:08/20/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/17/2018  Dirson Breton			Creado para actualizar los registros en [global].[destinationFund]
**********************************************************************************************************************************************************/
CREATE PROCEDURE [global].[SP_UPDATE_DESTINATION_FUND]
(
 @destinationFundId INT
,@destinationFundName VARCHAR(250)
,@auxiliaryAccountingCode INT
,@destinationFundSIBId INT
,@isActive BIT
,@CreateDate DATETIME
,@ModiDate DATETIME
,@CreateUsrId INT
,@ModiUsrId INT
,@hostName VARCHAR(100)
,@Action BIT
)
AS
BEGIN
	IF @Action = 1
		BEGIN
			UPDATE global.destinationFund
			SET
			destinationFundName = @destinationFundName,
			auxiliaryAccountingCode = @auxiliaryAccountingCode,
			destinationFundSIBId = @destinationFundSIBId,
			isActive = @isActive,
			CreateDate = @CreateDate,
			ModiDate = @ModiDate,
			CreateUsrId = @CreateUsrId,
			ModiUsrId = @ModiUsrId,
			hostName = @hostName
			WHERE destinationFundId = @destinationFundId
		END
	ELSE
		BEGIN
			UPDATE global.destinationFund
			SET
			isActive = 0
			WHERE destinationFundId = @destinationFundId
		END
END

GO
/****** Object:  StoredProcedure [global].[SP_UPDATE_DESTINATION_FUNDSIBI]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name:global.SP_UPDATE_DESTINATION_FUNDSIBI
** Desc:Creado para actualizar los registros en [global].[destinationFundSIBI]
** Auth:Dirson Breton
** Date:08/20/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/17/2018  Dirson Breton			Creado para actualizar los registros en [global].[destinationFundSIBI]
**********************************************************************************************************************************************************/
CREATE PROCEDURE [global].[SP_UPDATE_DESTINATION_FUNDSIBI]
(
 @destinationFundSIBId INT
,@destinationFundSIBName VARCHAR(500)
,@category VARCHAR(1)
,@Group VARCHAR(3)
,@Division VARCHAR(2)
,@Rama VARCHAR(4)
,@SegmentDR VARCHAR(6)
--,@isActive BIT
,@CreateDate DATETIME
,@ModiDate DATETIME
,@CreateUsrId INT
,@ModiUsrId INT
,@hostName VARCHAR(100)
,@Action BIT
)
AS
BEGIN
	IF @Action = 1
		BEGIN
			UPDATE global.destinationFundSIBI 
			SET
			destinationFundSIBName = @destinationFundSIBName,
			category = @category,
			[Group] = @Group,
			Division = @Division,
			Rama = @Rama,
			SegmentDR = @SegmentDR,
			--isActive = @isActive,
			CreateDate = @CreateDate,
			ModiDate = @ModiDate,
			CreateUsrId = @CreateUsrId,
			ModiUsrId = @ModiUsrId,
			hostName = @hostName
			WHERE destinationFundSIBId = @destinationFundSIBId
		END
	--ELSE
	--	BEGIN
	--		UPDATE global.destinationFundSIBI 
	--		SET
	--		isActive = 0
	--		WHERE destinationFundSIBId = @destinationFundSIBId
	--	END
END

GO
/****** Object:  StoredProcedure [global].[SP_UPDATE_FINANCIAL_SECTOR]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name:global.SP_UPDATE_FINANCIAL_SECTOR
** Desc:Creado para actualizar los registros en [global].[financialSector]
** Auth:Dirson Breton
** Date:08/28/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/28/2018  Dirson Breton			Creado para actualizar los registros en [global].[financialSector]
**********************************************************************************************************************************************************/
CREATE PROCEDURE [global].[SP_UPDATE_FINANCIAL_SECTOR]
(
 @financialSectorId INT
,@financialSectorName VARCHAR(150)
,@accountTypeID INT
,@isActive BIT
,@CreateDate DATETIME
,@ModiDate DATETIME
,@CreateUsrId INT
,@ModiUsrId INT
,@hostName VARCHAR(100)
,@Action BIT
)
AS
BEGIN
	IF @Action = 1
		BEGIN
			UPDATE [global].[financialSector]
			SET
			accountTypeID = @accountTypeID,
			financialSectorName = @financialSectorName,
			isActive = @isActive,
			CreateDate = @CreateDate,
			ModiDate = @ModiDate,
			CreateUsrId = @CreateUsrId,
			ModiUsrId = @ModiUsrId,
			hostName = @hostName
			WHERE financialSectorId = @financialSectorId
		END
	ELSE
		BEGIN
			UPDATE [global].[financialSector]
			SET
			isActive = 0
			WHERE financialSectorId = @financialSectorId
		END
END

GO
/****** Object:  StoredProcedure [global].[SP_UPDATE_FOUN_INCOME]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name:global.SP_UPDATE_FOUN_INCOME
** Desc:Creado para actualizar los registros en [global].[founIncome]
** Auth:Dirson Breton
** Date:08/30/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/30/2018  Dirson Breton			Creado para actualizar los registros en [global].[founIncome]
**********************************************************************************************************************************************************/
CREATE PROCEDURE [global].[SP_UPDATE_FOUN_INCOME]
(
 @companyId INT
,@founIncomeId INT
,@founIncomeName VARCHAR(35)
,@founIncomeCode VARCHAR(50)
,@isActive BIT
,@CreateDate DATETIME
,@ModiDate DATETIME
,@CreateUsrId INT
,@ModiUsrId INT
,@hostName VARCHAR(100)
,@Action BIT
)
AS
BEGIN
	IF @Action = 1
		BEGIN
			UPDATE global.founIncome
			SET
			companyId = @companyId,
			founIncomeName = @founIncomeName,
			founIncomeCode = @founIncomeCode,
			isActive = @isActive,
			CreateDate = @CreateDate,
			ModiDate = @ModiDate,
			CreateUsrId = @CreateUsrId,
			ModiUsrId = @ModiUsrId,
			hostName = @hostName
			WHERE founIncomeId = @founIncomeId
		END
	ELSE
		BEGIN
			UPDATE global.founIncome
			SET
			isActive = 0
			WHERE founIncomeId = @founIncomeId
		END
END

GO
/****** Object:  StoredProcedure [global].[SP_UPDATE_PROVINCE]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name:global.SP_UPDATE_PROVINCE
** Desc:Creado para actualizar los registros en global.province
** Auth:Dirson Breton
** Date:08/23/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/17/2018  Dirson Breton			Creado para actualizar los registros en global.province
**********************************************************************************************************************************************************/
CREATE PROCEDURE [global].[SP_UPDATE_PROVINCE]
(
 @CountryID INT
,@provinceId INT
,@provinceName VARCHAR(150)
,@isActive BIT
,@CreateDate DATETIME
,@ModiDate DATETIME
,@CreateUsrId INT
,@ModiUsrId INT
,@hostName VARCHAR(100)
,@Action BIT
)
AS
BEGIN
	IF @Action = 1
		BEGIN
			UPDATE global.province
			SET
			provinceName = @provinceName,
			isActive = @isActive,
			CreateDate = @CreateDate,
			ModiDate = @ModiDate,
			CreateUsrId = @CreateUsrId,
			ModiUsrId = @ModiUsrId,
			hostName = @hostName
			WHERE CountryID = @CountryID AND provinceId = @provinceId
		END
	ELSE
		BEGIN
			UPDATE global.province
			SET
			isActive = 0
			WHERE CountryID = @CountryID AND provinceId = @provinceId
		END
END

GO
/****** Object:  StoredProcedure [global].[SP_UPDATE_REASON]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name:global.SP_UPDATE_REASON
** Desc:Creado para actualizar los registros en global.Reason
** Auth:Dirson Breton
** Date:08/27/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/27/2018  Dirson Breton			Creado para actualizar los registros en global.Reason
**********************************************************************************************************************************************************/
CREATE PROCEDURE [global].[SP_UPDATE_REASON]
(
 @reasonId INT
,@reasonName VARCHAR(150)
,@reasonCode VARCHAR(50)
,@isActive BIT
,@CreateDate DATETIME
,@ModiDate DATETIME
,@CreateUsrId INT
,@ModiUsrId INT
,@hostName VARCHAR(100)
,@Action BIT
)
AS
BEGIN
	IF @Action = 1
		BEGIN
			UPDATE global.Reason
			SET
			reasonName = @reasonName,
			reasonCode = @reasonCode,
			isActive = @isActive,
			CreateDate = @CreateDate,
			ModiDate = @ModiDate,
			CreateUsrId = @CreateUsrId,
			ModiUsrId = @ModiUsrId,
			hostName = @hostName
			WHERE reasonId = @reasonId 
		END
	ELSE
		BEGIN
			UPDATE global.Reason
			SET
			isActive = 0
			WHERE reasonId = @reasonId
		END
END

GO
/****** Object:  StoredProcedure [global].[SP_UPDATE_RELATED_CONTACT]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name:global.SP_UPDATE_FINANCIAL_SECTOR
** Desc:Creado para actualizar los registros en [Entity].[relatedContact]
** Auth:Dirson Breton
** Date:08/28/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/28/2018  Dirson Breton			Creado para actualizar los registros en [Entity].[relatedContact]
**********************************************************************************************************************************************************/
CREATE PROCEDURE [global].[SP_UPDATE_RELATED_CONTACT]
(
 @relatedContactId INT
,@countryID INT
,@entityRolId INT
,@identificationTypeId INT
,@entityClassId INT
,@personTypeId INT
,@maritalStatusId INT
,@professionId INT
,@Sex VARCHAR(5)
,@FirstName VARCHAR(150)
,@MiddleName VARCHAR(150)
,@Lastname VARCHAR(150)
,@SecondLastname VARCHAR(150)
,@FullName VARCHAR(250)
,@Nickname VARCHAR(150)
,@MarriedName VARCHAR(150)
,@Title VARCHAR(10)
,@IdentificationNumber VARCHAR(60)
,@BirthDate DATETIME
,@isActive BIT
,@CreateDate DATETIME
,@ModiDate DATETIME
,@CreateUsrId INT
,@ModiUsrId INT
,@hostName VARCHAR(100)
,@Action BIT
)
AS
BEGIN
	IF @Action = 1
		BEGIN
			UPDATE [Entity].[relatedContact]
			SET
			countryID = @countryID,
			entityRolId = @entityRolId,
			identificationTypeId = @identificationTypeId,
			entityClassId = @entityClassId,
			personTypeId = @personTypeId,
			maritalStatusId = @maritalStatusId,
			professionId = @professionId,
			Sex = @Sex,
			FirstName = @FirstName,
			MiddleName = @MiddleName,
			Lastname = @Lastname,
			SecondLastname = @SecondLastname,
			FullName = @FullName,
			Nickname = @Nickname,
			MarriedName = @MarriedName,
			Title = @Title,
			IdentificationNumber = @IdentificationNumber,
			BirthDate = @BirthDate,
			isActive = @isActive,
			CreateDate = @CreateDate,
			ModiDate = @ModiDate,
			CreateUsrId = @CreateUsrId,
			ModiUsrId = @ModiUsrId,
			hostName = @hostName
			WHERE relatedContactId = @relatedContactId
		END
	ELSE
		BEGIN
			UPDATE [Entity].[relatedContact]
			SET
			isActive = 0
			WHERE relatedContactId = @relatedContactId
		END
END

GO
/****** Object:  StoredProcedure [global].[SP_UPDATE_SECTOR]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name:global.SP_UPDATE_SECTOR
** Desc:Creado para actualizar los registros en global.physicalSector
** Auth:Dirson Breton
** Date:08/27/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/27/2018  Dirson Breton			Creado para actualizar los registros en global.physicalSector
**********************************************************************************************************************************************************/
CREATE PROCEDURE [global].[SP_UPDATE_SECTOR]
(
 @CountryID INT
,@cityId INT
,@physicalSectorId INT
,@physicalSectorName VARCHAR(150)
,@isActive BIT
,@CreateDate DATETIME
,@ModiDate DATETIME
,@CreateUsrId INT
,@ModiUsrId INT
,@hostName VARCHAR(100)
,@Action BIT
)
AS
BEGIN
	IF @Action = 1
		BEGIN
			UPDATE global.physicalSector
			SET
			physicalSectorName = @physicalSectorName,
			isActive = @isActive,
			CreateDate = @CreateDate,
			ModiDate = @ModiDate,
			CreateUsrId = @CreateUsrId,
			ModiUsrId = @ModiUsrId,
			hostName = @hostName
			WHERE CountryID = @CountryID AND physicalSectorId = @physicalSectorId AND cityId = @cityId
		END
	ELSE
		BEGIN
			UPDATE global.physicalSector
			SET
			isActive = 0
			WHERE CountryID = @CountryID AND physicalSectorId = @physicalSectorId AND cityId = @cityId
		END
END

GO
/****** Object:  StoredProcedure [global].[SP_UPDATE_SOURCE_INCOME]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name:global.SP_UPDATE_SOURCE_INCOME
** Desc:Creado para actualizar los registros en [global].[SourceIncome]
** Auth:Dirson Breton
** Date:08/30/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/30/2018  Dirson Breton			Creado para actualizar los registros en [global].[SourceIncome]
**********************************************************************************************************************************************************/
CREATE PROCEDURE [global].[SP_UPDATE_SOURCE_INCOME]
(
 @companyId INT
,@SourceIncomeId INT
,@SourceIncomeName VARCHAR(150)
,@SourceIncomeCode VARCHAR(50)
,@isActive BIT
,@CreateDate DATETIME
,@ModiDate DATETIME
,@CreateUsrId INT
,@ModiUsrId INT
,@hostName VARCHAR(100)
,@Action BIT
)
AS
BEGIN
	IF @Action = 1
		BEGIN
			UPDATE global.SourceIncome
			SET
			companyId = @companyId,
			SourceIncomeName = @SourceIncomeName,
			SourceIncomeCode = @SourceIncomeCode,
			isActive = @isActive,
			CreateDate = @CreateDate,
			ModiDate = @ModiDate,
			CreateUsrId = @CreateUsrId,
			ModiUsrId = @ModiUsrId,
			hostName = @hostName
			WHERE SourceIncomeId = @SourceIncomeId
		END
	ELSE
		BEGIN
			UPDATE global.SourceIncome
			SET
			isActive = 0
			WHERE SourceIncomeId = @SourceIncomeId
		END
END

GO
/****** Object:  StoredProcedure [Loan].[SP_GET_LOANS_CODEBTOR_INFORMATIONS_DETAIL]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name:[Loan].[SP_GET_LOANS_CODEBTOR_INFORMATIONS_DETAIL]
** Desc:BUSCA INFORMACIONES DE LOS CODEUDOR QUE TIENE LA CUENTA
** Auth:RALVAREZ
** Date:08/17/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/17/2018  Ralvarez				BUSCA INFORMACIONES DE LOS CODEUDOR QUE TIENE LA CUENTA
**********************************************************************************************************************************************************/
CREATE PROC [Loan].[SP_GET_LOANS_CODEBTOR_INFORMATIONS_DETAIL]
	 @accountId BIGINT = NULL
AS 

SELECT 
	
	 c.account --No. Cuenta:
	,c.accountId --Id
	,accountStatusName --Estado
	,accountName --Nombre Cuenta
    ,rcc.Fullname --Cliente

	,rcd.Fullname as codebtorFullname --Nombre
	,rcd.identificationNumber as  codebtorIdentificationNumber --Identificación
	,cd.clientId as codebtorClientId --Código
	,rcd.relatedContactId as codebtorrelatedContactId--Código del contacto
	,codebtorTypeName
	,cd.companyId
	,cd.clientId


	,cd.codebtorTypeId
	,cd.canDeposit
	,cd.canWithdraw
	,cd.canCancel
	,cd.IsJointY
	,cd.IsJointO
	,cd.isActive
	,cd.CreateDate
	,cd.ModiDate
	,cd.CreateUsrId
	,cd.ModiUsrId
	,cd.hostName


FROM [Loan].[codebtor] cd
INNER JOIN [Loan].[Account] C 
	ON c.accountId = cd.accountId

INNER JOIN [Entity].[relatedContact] rcc
	ON rcc.relatedContactId = c.relatedContactId

INNER JOIN [Loan].[accountStatus]  acs 
	ON acs.accountStatusId = C.accountStatusId

INNER JOIN [Entity].client ccd
	ON ccd.clientId = cd.clientId

INNER JOIN [Entity].[relatedContact] rcd
	ON rcd.relatedContactId = ccd.relatedContactId

INNER JOIN [Loan].[codebtorType] cdt
	ON cdt.codebtorTypeId = cd.codebtorTypeId


where cd.accountId=@accountId or @accountId is null
GO
/****** Object:  StoredProcedure [Loan].[SP_GET_LOANS_COLLATERAL_INFORMATIONS_DETAIL]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name: Loan].[SP_GET_LOANS_COLLATERAL_INFORMATIONS_DETAIL]
** Desc:BUSCA INFORMACIONES DETALLES DE LAS GARANTIAS
** Auth:RALVAREZ
** Date:08/15/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/15/2018  Ralvarez				BUSCA INFORMACIONES DETALLES DE LAS GARANTIAS
**********************************************************************************************************************************************************/
CREATE PROC [Loan].[SP_GET_LOANS_COLLATERAL_INFORMATIONS_DETAIL]
	@collateralId BIGINT = NULL 

AS 

SELECT 
	 lcfv.collateralFieldId
	,lcfv.collateralId
	,cf.collateralFieldName
	,lcfv.collateralFieldValue
	,cf.ISRequired
	,ISNULL(cf.collateralFieldComment,'') collateralFieldComment
FROM [Loan].[loanCollateralFieldValue] lcfv
INNER JOIN [Loan].[collateralField] cf 
	ON cf.collateralFieldId =lcfv.collateralFieldId

WHERE lcfv.collateralId=@collateralId OR @collateralId IS NULL
	


GO
/****** Object:  StoredProcedure [Loan].[SP_GET_LOANS_COLLATERAL_INFORMATIONS_RESUMEN]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name: [Loan].[SP_GET_LOANS_INFORMATIONS_DETAIL]
** Desc:BUSCA INFORMACIONES DE LAS GARANTIAS DEL PRESTAMO
** Auth:RALVAREZ
** Date:06/27/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/14/2018  Ralvarez				BUSCA INFORMACIONES DE LAS GARANTIAS DEL PRESTAMO
**********************************************************************************************************************************************************/
CREATE PROC [Loan].[SP_GET_LOANS_COLLATERAL_INFORMATIONS_RESUMEN]
	@accountId BIGINT = NULL
AS 
select 
	 lc.accountId
	,lc.collateralId -- No. Garantía
	,cc.collateralName --Nombre
	,collateralTypeName   --Tipo
	,collateralTypeSIBName  
	,lc.[percent] --Porciento
	,cc.amount --Monto
	,lc.contractNumber --No. Contrato
	,lc.codeDesc --Estado
from  [Loan].[loancollateral] lc
inner join  [Entity].[collateral] cc 
	on lc.collateralId =cc.collateralId

left join  [Loan].[collateralType] ct 
	on ct.collateralTypeId =cc.collateralTypeId


left join  [Loan].[collateralTypeSIB] cts 
	on cts.collateralTypeSIBId =cc.collateralTypeSIBId

where (lc.accountId=@accountId OR @accountId IS NULL)




GO
/****** Object:  StoredProcedure [Loan].[SP_GET_LOANS_ENDORSE_COLLATERAL_INFORMATIONS_DETAIL]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name:[Loan].[SP_GET_LOANS_ENDORSE_COLLATERAL_INFORMATIONS_DETAIL]
** Desc:BUSCA LA INFORMACION DE LA GARANTIA Y ENDOSO
** Auth:RALVAREZ
** Date:08/17/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/17/2018  Ralvarez				BUSCA LA INFORMACION DE LA GARANTIA Y ENDOSO
**********************************************************************************************************************************************************/


CREATE PROC [Loan].[SP_GET_LOANS_ENDORSE_COLLATERAL_INFORMATIONS_DETAIL]
	 @accountId BIGINT = NULL

AS 

SELECT 

 policyTypeName --Tipo Póliza
,rc.FullName -- Aseguradora
,pc.policyNo --No. Póliza
,pc.Amount --Valor
,pc.isActive --Estado
,pc.policyCollateralName --Descripción
,pc.Date --Fecha de Póliza
,pc.EffectiveDate --Fecha Vencimiento

,pce.endorseNo --Número
,pce.amount as endorseAmount -- amount
,pce.InicialDate as endorseInicialDate -- Fecha Inicio
,pce.Date as endorseDate -- Fecha Endoso
,pce.EffectiveDate as endorseEffectiveDate -- Fecha Vencimiento 2

,pc.collateralId
,pc.companyId
,pc.policyTypeId
,pc.relatedContactId
,pc.policyCollateralComment


,pc.notificationDate


,pc.CreateDate
,pc.ModiDate
,pc.CreateUsrId
,pc.ModiUsrId
,pc.hostName
FROM [Loan].[policyCollateral] pc	
INNER JOIN [Loan].[policyType] pt 
	ON pt.policyTypeId=pc.policyTypeId

INNER JOIN [Entity].[relatedContact] rc
	ON rc.relatedContactId = pc.relatedContactId

INNER JOIN [Loan].[loancollateral] lc 
	ON lc.collateralId =pc.collateralId
LEFT JOIN [Loan].[policyCollateralEndorse] pce 
	ON pc.collateralId=pce.collateralId AND pc.policyNo=pce.policyNo

WHERE lc.accountId = @accountId OR @accountId IS NULL

GO
/****** Object:  StoredProcedure [Loan].[SP_GET_LOANS_INFORMATIONS_DETAIL]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name: [Loan].[SP_GET_LOANS_INFORMATIONS]
** Desc:BUSCA INFORMACIONES GENERALES DEL PRESTAMO
** Auth:RALVAREZ
** Date:06/27/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/14/2018  Ralvarez				BUSCA INFORMACIONES GENERALES DEL PRESTAMO
**********************************************************************************************************************************************************/
CREATE PROC [Loan].[SP_GET_LOANS_INFORMATIONS_DETAIL]
	@quotationId BIGINT = 1320
   ,@LoanNumber int = 76
   ,@accountId BIGINT = 314
AS 

SELECT 
--DATOS DEL CLIENTE 
	 lh.quotationId -- No. Solicitud:
	,lh.LoanNumber --No. Préstamo:
	,rc.fullName
	,c.clientid
	,rc.identificationNumber
	,ls.LoanStatusName  -- Estado
	,pt.quotationPaymentTypeName --Forma de Pago
	,[frequency] --Frecuencia de Capital 0-Dias 1-Mensual
	,CASE WHEN [frequency] = 1 THEN 'Mensual' ELSE 'Dias' END AS frequencyName --Frecuencia de Pago:
	,lh.loanTerm --Cuotas Y Plazo
	,lh.LoanDillingDay--Días de Corte
	,cur.CurrencyName --Moneda
--DATOS DEL CLIENTE 

--MONTOS Y TASAS
	,q.amount as AmountRequested --solicitado
	,lh.amountApproved -- aprobado
	,lh.financedAmount --financiado
	,lh.DisbursedAmount-- desembolsado
	,lh.OutstandingBalance -- saldo
	,lh.QuotaAmount --Pendiente
	,lh.Rate as RateAnnual -- interes Anual
	,CAST( (lh.Rate / 12) AS numeric(18,2)) as RateMonth -- interes mensual
	,lh.RateCommission as RateCommissionAnnual -- interes comision Anual
	,CAST( (lh.RateCommission / 12) AS numeric(18,2)) as RateCommissionMonth -- interes comision mensual
	,lh.rateLate as RateLatenAnnual -- interes mora Anual
	,CAST( (lh.rateLate / 12) AS numeric(18,2)) as RateLateMonth -- interes mora mensual
--MONTOS Y TASAS

--FECHAS 
	,Q.qoutationDate -- Solicitud
	,lh.ApprovedDate --Aprobación
	,ISNULL(li.disbursementDate,lh.disbursementDate) disbursementDate -- Desembolso ** revisar
	,lh.expirationDate  -- Vencimiento


    ,ClosedDate -- Último Corte
    ,LastClosedDate  --Próximo Corte
	,lh.LastQuotaDate --Última Cuota
	,li.lastPaidDate  --Último Devengo Cuota
--FECHAS 

--otros 
	,rcEje.fullName as fullNameExecutive --Ejecutivo
	,rcPro.fullName as fullNamePromoter  --Promotor
--otros
FROM [Loan].[loanHeader] Lh
INNER JOIN [Loan].[quotation] q 
	ON Q.quotationId=lh.quotationId

INNER JOIN [Entity].[client] c 
	ON c.clientId = lh.[relatedContactId]
INNER JOIN [Entity].[relatedContact] rc 
	ON rc.[relatedContactId]= c.[relatedContactId]
INNER JOIN [Loan].[Account] acc 
	ON acc.[accountId]= lh.[accountId]

INNER JOIN Loan.LoanStatus ls 
	ON ls.loanStatusId= lh.loanStatusId
INNER JOIN [Loan].[PaymentType] pt 
	ON pt.PaymentTypeId= lh.PaymentTypeId

INNER JOIN [global].[Currency] cur 
	ON cur.CurrencyId= lh.CurrencyId
left join Loan.[LoanIndicators] li 
	on li.accountId = lH.accountId

left JOIN [Entity].[relatedContact] rcEje on rcEje.[relatedContactId]= acc.accountExecutiveContactId
left JOIN [Entity].[relatedContact] rcPro on rcPro.[relatedContactId]= acc.accountPromoterContactId

where 
		(Lh.accountId=@accountId OR @accountId IS NULL)
	AND (Lh.loanNumber=@LoanNumber OR @LoanNumber IS NULL)
	AND (Lh.quotationId=@quotationId OR @quotationId IS NULL)


GO
/****** Object:  StoredProcedure [Loan].[SP_GET_LOANS_PAYMENT_PLAN]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name: [Loan].[SP_GET_LOANS_PAYMENT_PLAN]
** Desc:BUSCA LA AMORTIZACION  O PLAN DE PAGO DE LA CUENTA 
** Auth:RALVAREZ
** Date:06/27/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/14/2018  Ralvarez				BUSCA LA AMORTIZACION  O PLAN DE PAGO DE LA CUENTA 
**********************************************************************************************************************************************************/
 --[Loan].[SP_GET_LOANS_PAYMENT_PLAN]1
CREATE PROC [Loan].[SP_GET_LOANS_PAYMENT_PLAN]
	@AccountId BIGINT =NULL
AS 
	--DECLARE @AccountId BIGINT =1
	DECLARE @loanTerm BIGINT =0
	DECLARE @amountApproved NUMERIC(18,3) =0
	DECLARE @LoanDillingDay NUMERIC(18,3) =0
	DECLARE @LoanDillingDayAnuall NUMERIC(18,8) =0
	DECLARE @Quota NUMERIC(18,7) =0
	DECLARE @Date datetime =0
	SELECT   
		 @amountApproved=lh.amountApproved
		,@LoanDillingDay=lh.rate
		,@loanTerm=lh.loanTerm
		,@LoanDillingDayAnuall=CAST(lh.rate / CAST(12 AS NUMERIC(18,7)) AS NUMERIC(18,7)) /100
		,@Date=dateadd(month, (-1*lh.loanTerm),ISNULL(ISNULL(lh.expirationDate,DATEADD(MONTH,lh.loanTerm,LI.disbursementDate)),lh.disbursementDate))
	FROM [Loan].[loanHeader] lh
	LEFT JOIN  [Loan].[LoanIndicators] LI ON LI.AccountId=lh.AccountId
	WHERE 
		lh.AccountId = @AccountId

	--	--SELECT @Date

	----VERIFICAR TENGO UN CENTAVO DE MENO 	
	
	SELECT  @Quota=CAST([dbo].[PMT](@LoanDillingDayAnuall,@loanTerm,@amountApproved) AS NUMERIC(18,2)) 

	DECLARE @Datos AS TABLE 
	(
		 Quota	INT --Número
		,QuotaDate DATE	
		,CapitalBalance NUMERIC(18,2)	
		,Capital NUMERIC(18,2)	
		,Rate NUMERIC(18,2)	
		,LoanRate NUMERIC(18,2)	
		,FinancialQuota NUMERIC(18,2) 
		,Expense	NUMERIC(18,2)
		,TotalPayment NUMERIC(18,2)
		,Commision NUMERIC(18,2)
	)

	DECLARE @QuotaNum INT =1
	DECLARE @amountApprovedLessQuota NUMERIC(18,3) =@amountApproved
	
	WHILE (@QuotaNum <=@loanTerm)
	BEGIN
		DECLARE @RateCal NUMERIC(18,3)=(@amountApprovedLessQuota*@LoanDillingDay*30/360) / 100

		--select @LoanDillingDayAnuall,@LoanDillingDay AS LoanDillingDay,@loanTerm,@amountApproved,[dbo].[PMT](@LoanDillingDayAnuall,@loanTerm,@amountApproved)


		DECLARE @CapitalCal NUMERIC(18,3)=@Quota-@RateCal
		DECLARE @ExpenseCal NUMERIC(18,3) =isnull((SELECT SUM(AMOUNT) FROM  [Loan].[loanExpense] where accountid=@AccountId AND paymentFormId=3 /*A LA CUOTA*/ AND @QuotaNum <=endQuata),0)
		DECLARE @TotalPaymentCal NUMERIC(18,3)= @RateCal  +@CapitalCal +@ExpenseCal

		INSERT INTO @Datos VALUES(@QuotaNum,dateAdd(month,@QuotaNum,@Date),@amountApprovedLessQuota,@CapitalCal,@RateCal,@LoanDillingDay,@Quota,@ExpenseCal,@TotalPaymentCal,0)
		

		
		SET @QuotaNum=@QuotaNum+1
		SET @amountApprovedLessQuota=@amountApprovedLessQuota -@CapitalCal
	END 

	SELECT * FROM @Datos
GO
/****** Object:  StoredProcedure [Loan].[SP_GET_LOANS_PAYMENT_PLAN_PROYECTIONS]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name: [Loan].[SP_GET_LOANS_PAYMENT_PLAN_PROYECTIONS]
** Desc:BUSCA LA AMORTIZACION  O PLAN DE PAGO DE LA CUENTA A UNA FECHA EN ESPECIFICO
** Auth:RALVAREZ
** Date:06/27/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/14/2018  Ralvarez				BUSCA LA AMORTIZACION  O PLAN DE PAGO DE LA CUENTA A UNA FECHA EN ESPECIFICO
**********************************************************************************************************************************************************/
 --[Loan].[SP_GET_LOANS_PAYMENT_PLAN_PROYECTIONS] 1,'20181231',1
CREATE PROC [Loan].[SP_GET_LOANS_PAYMENT_PLAN_PROYECTIONS]
	 @AccountId BIGINT 
	,@DateStatement DATE 
	,@IdTipo INT =NULL
	,@MontoPago NUMERIC(18,2)=100000
AS 
  
    DECLARE @Datos AS TABLE 
	(
		 Quota	INT --Número
		,QuotaDate DATE	
		,CapitalBalance NUMERIC(18,2)	
		,Capital NUMERIC(18,2)	
		,Rate NUMERIC(18,2)	
		,LoanRate NUMERIC(18,2)	
		,FinancialQuota NUMERIC(18,2) 
		,Expense	NUMERIC(18,2)
		,TotalPayment NUMERIC(18,2)
		,Commision NUMERIC(18,2)
		/*TRANSACTIONS COLUMS*/
		,RateLateDays INT
		,RateLate NUMERIC(18,2)
		,IsPayment BIT
		,IsApply BIT
		,projectedQuotaAmount  NUMERIC(18,2)
	)
	  
	DECLARE @Result AS TABLE 
	(
		 [Description] varchar(250)
		,Value varchar(250)
		,TipoSaldo int
    )

	DECLARE @loanTerm BIGINT =0
	DECLARE @amountApproved NUMERIC(18,3) =0
	DECLARE @LoanDillingDay NUMERIC(18,3) =0
	DECLARE @LoanDillingDayAnuall NUMERIC(18,8) =0
	DECLARE @Quota NUMERIC(18,7) =0
	DECLARE @Date datetime =0
	DECLARE @rateLatePercent  NUMERIC(18,3) =0
	
	DECLARE @Charges NUMERIC(18,3) =0.05 /*cargo por saldo anticipado*/

	/*VUSCO LOS DATOS GENERALES DEL PRESTAMO*/
	SELECT   
		 @amountApproved=lh.amountApproved --Monto Prestado
		,@LoanDillingDay=lh.rate  --Tasa del prestamo anual
		,@loanTerm=lh.loanTerm -- Meses del financimiento
		,@LoanDillingDayAnuall=CAST(lh.rate / CAST(12 AS NUMERIC(18,7)) AS NUMERIC(18,7)) /100 --Formula para convertir la tasa anual a mensual
		,@Date=dateadd(month, (-1*lh.loanTerm),ISNULL(ISNULL(lh.expirationDate,DATEADD(MONTH,lh.loanTerm,LI.disbursementDate)),lh.disbursementDate)) /*Busco la fecha donde el prestamo comienza a pagar*/
		,@rateLatePercent = (rateLate/12)/100 -- tasa de mora anual convertida a mensual
	FROM [Loan].[loanHeader] lh
	LEFT JOIN  [Loan].[LoanIndicators] LI ON LI.AccountId=lh.AccountId
	WHERE 
		lh.AccountId = @AccountId

		
	--VERIFICAR TENGO UN CENTAVO DE MENO 	
	SELECT  @Quota=CAST([dbo].[PMT](@LoanDillingDayAnuall,@loanTerm,@amountApproved) AS NUMERIC(18,2)) --Aqui se calcula el monto de la cuota del prestamo

	DECLARE @QuotaNum INT =1
	DECLARE @amountApprovedLessQuota NUMERIC(18,3) =@amountApproved
	/*AQUI LLENO LA TABLA CON LA AMORTIZACION */
	DECLARE @ExpenseCal NUMERIC(18,3) =isnull((SELECT SUM(AMOUNT) FROM  [Loan].[loanExpense] where accountid=@AccountId AND paymentFormId=3 /*A LA CUOTA*/ AND @QuotaNum <=endQuata),0)
	WHILE (@QuotaNum <=@loanTerm)
	BEGIN
		/*CALCULO DEL INTERES QUE DEBE PAGAR EN LA CUOTA BASADO EN 30, ESTO SE CALCULA DEL CAPITAL PENDIENTE*/
		DECLARE @RateCal NUMERIC(18,3)=(@amountApprovedLessQuota*@LoanDillingDay*30/360) / 100 
		/*Le quito a la cuota el interes*/
		DECLARE @CapitalCal NUMERIC(18,3)=@Quota-@RateCal
		/*Busco los gastos relacionados a la cuota, GPS, SEGURO DE VIDA Y POLIZA */
		
		/*Suma de la cuota mas los gastos*/
		DECLARE @TotalPaymentCal NUMERIC(18,3)= @RateCal  +@CapitalCal +@ExpenseCal

		INSERT INTO @Datos(Quota,QuotaDate,CapitalBalance,Capital,Rate,LoanRate,FinancialQuota,Expense,TotalPayment,Commision)
		
		 VALUES(@QuotaNum,dateAdd(month,@QuotaNum,@Date),@amountApprovedLessQuota,@CapitalCal,@RateCal,@LoanDillingDay,@Quota,@ExpenseCal,@TotalPaymentCal,0)
	
		SET @QuotaNum=@QuotaNum+1
		SET @amountApprovedLessQuota=@amountApprovedLessQuota -@CapitalCal
	END 

	UPDATE d set 
	 D.RateLate = rateLateAmount
	 /*Verifico si la cuota esta entre la fecha de la proyeccion*/
	,D.IsApply = CASE WHEN D.QuotaDate<= @DateStatement THEN 1 ELSE 0 END 
	 /*calculo la mora a partir de la fecha de proyeccion, si tiene pagos parciales busco la fecha mas reciente del pago sino la fecha de la cuota*/
	--,D.RateLateDays = CASE WHEN DATEDIFF(DAY, ISNULL(tpp.transactionDate, QuotaDate),@DateStatement)>3 /*DIAS DE GRACIAS*/ THEN DATEDIFF(DAY, ISNULL(tpp.transactionDate, QuotaDate),@DateStatement) ELSE 0 END
	,D.RateLateDays = CASE WHEN dbo.DAYS360(ISNULL(tpp.transactionDate, QuotaDate),@DateStatement,0)>3 /*DIAS DE GRACIAS*/ THEN dbo.DAYS360(ISNULL(tpp.transactionDate, QuotaDate),@DateStatement,0) ELSE 0 END
	

	,projectedQuotaAmount=0
	/*PARA UNA CUOTA ESTAR PAGA EL MONTO PAGADO DEBE SER EXACTO O MAYOR */

	/*verifico si el pago a la cuota fue total, parcial o no tiene */
    ,D.IsPayment = CASE WHEN tpp.transactionPaymentPlanId IS NULL 
	 THEN 0 
	 ELSE 
		CASE WHEN (d.Capital-tpp.capitalAmount)>=1 or (d.Rate-tpp.interestAmoint)>=1 or (d.Expense-tpp.expenseAmount)>=1 THEN 0 ELSE 1 END
	 END

	 /*en caso de pago parciales le dejo la proyeccion con lo pendiente*/
	,d.Capital =CASE WHEN tpp.transactionPaymentPlanId IS NULL  THEN d.Capital ELSE  CASE WHEN tpp.capitalAmount<d.Capital THEN d.Capital -Tpp.capitalAmount ELSE 0 END  END
	,d.Rate = CASE WHEN tpp.transactionPaymentPlanId IS NULL  THEN d.Rate ELSE   CASE WHEN tpp.interestAmoint<d.Rate THEN d.Rate -Tpp.interestAmoint ELSE 0 END  END
	,d.Expense = CASE WHEN tpp.transactionPaymentPlanId IS NULL  THEN d.Expense ELSE   CASE WHEN tpp.expenseAmount<d.Expense THEN d.Expense -Tpp.expenseAmount ELSE 0 END  END
	FROM  @Datos D
	OUTER APPLY 
	(

		SELECT 
				 SUM(tpp.rateLateAmount)rateLateAmount
				,SUM(trd.capitalAmount)capitalAmount
				,SUM(trd.interestAmoint)interestAmoint
				,SUM(trd.commissionAmount)commissionAmount
				,SUM(trd.expenseAmount)expenseAmount
				,MAX(tpp.transactionPaymentPlanId)transactionPaymentPlanId
				,MAX(rr.transactionDate) transactionDate
		FROM [Loan].[transactionPaymentPlan] tpp 
		INNER JOIN [Loan].[transactionReceiptDetail] trd on 
		trd.transactionPaymentPlanId=tpp.transactionPaymentPlanId and transactionSinalId=1
		INNER JOIN Loan.transactionReceipt rr on rr.transactionReceiptId =trd.transactionReceiptId
		WHERE tpp.accountId =@AccountId and tpp.quotaNumber=d.Quota and tpp.isactive=1

	)tpp

	/*CALCULO DE MORA segun la fecha de proyeccion, en caso de tener un pago el calculo se realiza a partir de la fecha del ultimo pago en caso contrario de la fecha de pago de la cuota*/
	UPDATE @Datos SET 
		  RateLate=CASE WHEN IsPayment = 1 OR IsApply = 0  THEN 0 ELSE ((Capital+Rate) * @rateLatePercent /30*RateLateDays) END
		 ,RateLateDays=CASE WHEN IsPayment = 1 OR IsApply = 0 THEN 0  else RateLateDays end
	


	
	/*calculo de cuanto debe pagar el cliente */
	UPDATE @Datos SET 
		projectedQuotaAmount=Capital+Rate+Expense+Commision+RateLate
	WHERE IsApply = 1 AND IsPayment=0


	DECLARE @LastQuataPayment DATE =GETDATE()
	DECLARE @LastBalance NUMERIC(18,3)=0
	DECLARE @LastBalancePay NUMERIC(18,3)=0
    DECLARE @compensatoryInterest NUMERIC(18,15)=0
	DECLARE @FactorPenalidad NUMERIC(18,8)=0

	
	/*calculo el resumen de los datos*/
	SELECT @LastQuataPayment=QuotaDate,@LastBalancePay=CapitalBalance FROM @Datos WHERE Quota IN(SELECT  MAX(Quota) FROM @Datos WHERE IsApply =1) /*BUSCO LA ULTIMA FECHA DE CUOTA PAGADA*/
	SELECT @LastBalance=CapitalBalance FROM @Datos WHERE Quota IN(SELECT  MAX(Quota) + 1 FROM @Datos WHERE IsApply =1) /*BUSCO EL CAPITAL DE CUOTA PENDIENTE MAS ANTIGUA LUEGO DE LA PAGADA*/
	if (@LastBalance is null)
		set @LastBalance = @LastBalancePay
	
	--set @compensatoryInterest = CAST(DATEDIFF(day,@LastQuataPayment,@DateStatement) AS numeric(18,1))/360
	set @compensatoryInterest = CAST(dbo.DAYS360(@LastQuataPayment,@DateStatement,0) AS numeric(18,1))/360
	

	
	/*
	
	SELECT * FROM @Datos
	SELECT 
				 SUM(tpp.rateLateAmount)rateLateAmount
				,SUM(trd.capitalAmount)capitalAmount
				,SUM(trd.interestAmoint)interestAmoint
				,SUM(trd.commissionAmount)commissionAmount
				,SUM(trd.expenseAmount)expenseAmount
				,MAX(tpp.transactionPaymentPlanId)transactionPaymentPlanId
				,MAX(rr.transactionDate) transactionDate
		FROM [Loan].[transactionPaymentPlan] tpp 
		INNER JOIN [Loan].[transactionReceiptDetail] trd on 
		trd.transactionPaymentPlanId=tpp.transactionPaymentPlanId and transactionSinalId=1
		INNER JOIN Loan.transactionReceipt rr on rr.transactionReceiptId =trd.transactionReceiptId
		WHERE tpp.accountId =@AccountId and tpp.quotaNumber=2 and tpp.isactive=1

		select DATEDIFF(day,'2018-08-15',@DateStatement),@DateStatement


	    select * FROM [Loan].[transactionPaymentPlan] tpp 
    	WHERE tpp.accountId =418 and tpp.quotaNumber=2 and tpp.isactive=1

	     select  DATEDIFF(day,'2018-08-15',@DateStatement),DATEDIFF(day,'2018-08-08',@DateStatement)
	   SELECT  * FROM [Loan].[transactionReceiptDetail]
	   WHERE transactionPaymentPlanId=1399

	   select  * from [Loan].[transactionReceipt]
	   where transactionReceiptId=1437


	--select @LastBalance,@compensatoryInterest,@LastQuataPayment ,@DateStatement,DATEDIFF(day,@LastQuataPayment,@DateStatement)
	*/

	DECLARE @CantidadCuotasPagadas INT=0

	SELECT @CantidadCuotasPagadas =COUNT(*)
	FROM @Datos
	WHERE IsPayment=1

	DECLARE @InteresCompensatorio  numeric(18,3)=@LastBalance *0.22* @compensatoryInterest 
	DECLARE @Penalidad  numeric(18,3)=@LastBalance * @Charges
	DECLARE @SaldoToTal  numeric(18,3)=((@LastBalance * @Charges) + (@LastBalance) + (@LastBalance *0.22* @compensatoryInterest))
	DECLARE @projectedQuotaAmount  numeric(18,3)=0

	DECLARE @PaidQuota INT=0
	
	SET @FactorPenalidad = CASE WHEN @CantidadCuotasPagadas<=24 THEN 4.76190174 ELSE 2.43901999 END 



	

	SELECT @PaidQuota = max(Quota)
    FROM @Datos
	WHERE IsApply = 1 


	SELECT @projectedQuotaAmount = SUM(projectedQuotaAmount)
    FROM @Datos
	WHERE IsApply = 1 AND IsPayment=0
	set @projectedQuotaAmount=isnull(@projectedQuotaAmount,0)

	/*Calculo bajar cuota*/
	DECLARE @BaseCalculoCapital  numeric(18,3)=0
	DECLARE @AmortizaciónCapital  numeric(18,3)=0
	DECLARE @SaldoCapital  numeric(18,3)=0
	DECLARE @QuotaNew  numeric(18,3)=0
	
	
	SET @BaseCalculoCapital=@MontoPago-( @projectedQuotaAmount+ @InteresCompensatorio)
	SET @AmortizaciónCapital = @BaseCalculoCapital - (@BaseCalculoCapital*@FactorPenalidad/100)
	SET @SaldoCapital =@LastBalance - @AmortizaciónCapital 

	set  @QuotaNew=CAST([dbo].[PMT](@LoanDillingDayAnuall,@loanTerm - @PaidQuota,@SaldoCapital) AS NUMERIC(18,2)) --Aqui se calcula el monto de la cuota del prestamo

	set @QuotaNew =@QuotaNew+ @ExpenseCal
	
	--select 
	--   @FactorPenalidad FactorPenalidad
	--  ,@LastBalance BalanceCapitalParaSaldoCompleto
	--  ,@Quota CuotaActual
	--  ,@ExpenseCal Gastos
	--  ,@MontoPago MontoPago
	--  ,@BaseCalculoCapital BaseCalculoCapital
	--  ,@AmortizaciónCapital AmortizaciónCapital
	--  ,@SaldoCapital SaldoCapitalDespuesPago
	--  ,@QuotaNew CuotaNueva
	
    ;
	WITH Result AS 
	(

		SELECT 
			   Description = CAST('Estado Proyectado Cuotas' AS varchar(250))
			  ,Value = CAST( CAST(COUNT(*) AS INT) AS VARCHAR(100))
			  ,TipoSaldo=1
	    FROM @Datos
		WHERE IsApply = 1 AND IsPayment=0
		UNION ALL 
		SELECT 
			   Description = CAST('Capital Cuotas Pendientes' AS varchar(250))
			  ,Value = CAST( CAST(SUM(Capital) AS numeric(18,3)) AS VARCHAR(100))
			  ,TipoSaldo=1
	    FROM @Datos
		WHERE IsApply = 1 AND IsPayment=0
		UNION ALL 
		SELECT 
			   Description = CAST('Intereses Cuotas Pendientes' AS varchar(250))
			  ,Value = CAST( CAST(SUM(Rate) AS numeric(18,3)) AS VARCHAR(100))
			  ,TipoSaldo=1
	    FROM @Datos
		WHERE IsApply = 1 AND IsPayment=0
		UNION ALL 
		SELECT 
			   Description = CAST('Gastos Cuotas Pendientes' AS varchar(250))
			  ,Value = CAST( CAST(SUM(Expense) AS numeric(18,3)) AS VARCHAR(100))
			  ,TipoSaldo=1
	    FROM @Datos
		WHERE IsApply = 1 AND IsPayment=0
		UNION ALL 
		SELECT 
			   Description = CAST('Comisiones Cuotas Pendientes' AS varchar(250))
			  ,Value = CAST( CAST(SUM(Commision) AS numeric(18,3)) AS VARCHAR(100))
			  ,TipoSaldo=1
	    FROM @Datos
		WHERE IsApply = 1 AND IsPayment=0
		UNION ALL 
		SELECT 
			   Description = CAST('Cargos Cuotas Pendientes' AS varchar(250))
			  ,Value = CAST( CAST(SUM(0) AS numeric(18,3)) AS VARCHAR(100))
			  ,TipoSaldo=1
	    FROM @Datos
		WHERE IsApply = 1 AND IsPayment=0

		UNION ALL 
		SELECT 
			   Description = CAST('Mora Cuotas Pendientes' AS varchar(250))
			  ,Value = CAST( CAST(SUM(RateLate) AS numeric(18,3)) AS VARCHAR(100))
			  ,TipoSaldo=1
	    FROM @Datos
		WHERE IsApply = 1 AND IsPayment=0
		UNION ALL 
		SELECT 
		       /*estos son los dias que pasaron despues del ultimo pago y antes de que se cumpla la ultima cuota*/
			   Description = CAST('Interés Compensatorio (Dias ' AS varchar(250)) + '' + cast( DATEDIFF(day,@LastQuataPayment,@DateStatement) as varchar(30)) + ')'
			  ,Value = CAST(@InteresCompensatorio AS VARCHAR(100))
			  ,TipoSaldo=1
		UNION ALL 
		SELECT 
			   Description = CAST('Total a Pagar Cuotas Pendientes' AS varchar(250))
			  ,Value = CAST( @projectedQuotaAmount AS VARCHAR(100))
			  ,TipoSaldo=1

       /*--------------------*/
		
	  
	    UNION ALL 
		SELECT 
			   Description = CAST('Saldo Capital ' AS varchar(200)) +  CAST(@DateStatement AS varchar(50))
			  ,Value = CAST(@LastBalance AS VARCHAR(100))
			  ,TipoSaldo=3
	  UNION ALL 
		SELECT 
			   Description = CAST('Saldo Penalidad ' AS varchar(200)) +  CAST(@DateStatement AS varchar(50))
			  ,Value = CAST(@Penalidad AS VARCHAR(100))
			  ,TipoSaldo=3 

		UNION ALL 
		SELECT 
		       /*estos son los dias que pasaron despues del ultimo pago y antes de que se cumpla la ultima cuota*/
			   Description = CAST('Interés Compensatorio (Dias ' AS varchar(250)) + '' + cast( DATEDIFF(day,@LastQuataPayment,@DateStatement) as varchar(30)) + ')'
			  ,Value = CAST(@InteresCompensatorio AS VARCHAR(100))
			  ,TipoSaldo=3

			  UNION ALL 
		SELECT 
			   Description = CAST('Saldo Total a Pagar ' AS varchar(200)) +  CAST(@DateStatement AS varchar(50))
			  ,Value = CAST(  @SaldoToTal AS VARCHAR(100))
			  ,TipoSaldo=3 



         /*------------------------------*/
		UNION ALL 
		SELECT 
			   Description = CAST('Penalidad' AS varchar(200))
			  ,Value = CAST(  (@BaseCalculoCapital*@FactorPenalidad/100) AS VARCHAR(100))
			  ,TipoSaldo=4 
		UNION ALL 
		SELECT 
		       /*estos son los dias que pasaron despues del ultimo pago y antes de que se cumpla la ultima cuota*/
			   Description = CAST('Interés Compensatorio (Dias ' AS varchar(250)) + '' + cast( DATEDIFF(day,@LastQuataPayment,@DateStatement) as varchar(30)) + ')'
			  ,Value = CAST(@InteresCompensatorio AS VARCHAR(100))
			  ,TipoSaldo=4
		UNION ALL 
		SELECT 
			   Description = CAST('Total Cuota Pendiente/s' AS varchar(200))
			  ,Value = CAST( @projectedQuotaAmount AS VARCHAR(100))
			  ,TipoSaldo=4  

        UNION ALL 
		SELECT 
			   Description = CAST('Cuota Anterior' AS varchar(200)) 
			  ,Value = CAST(  @Quota+ @ExpenseCal AS VARCHAR(100))
			  ,TipoSaldo=4 
		UNION ALL 
		SELECT 
			   Description = CAST('Nueva Cuota' AS varchar(200))
			  ,Value = CAST(  @QuotaNew AS VARCHAR(100))
			  ,TipoSaldo=4 
		UNION ALL 
		SELECT 
			   Description = CAST('Diferencia' AS varchar(200))
			  ,Value = CAST(  (@Quota+ @ExpenseCal) -@QuotaNew AS VARCHAR(100))
			  ,TipoSaldo=4 
		UNION ALL 
		SELECT 
			   Description = CAST('Capital Pendiente Luego Pago' AS varchar(200))
			  ,Value = CAST( @SaldoCapital AS VARCHAR(100))
			  ,TipoSaldo=4 
	
	)

	INSERT INTO @Result
	SELECT * FROM Result

	SELECT * FROM @Result
	where TipoSaldo=@IdTipo
	
	

GO
/****** Object:  StoredProcedure [Loan].[SP_GET_LOANS_QUOTA_HEADER]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name:[Loan].[SP_GET_LOANS_QUOTA_HEADER]
** Desc:BUSCA EL LISTADO DE CUOTAS PAGADAS POR EL CLIENTE
** Auth:RALVAREZ
** Date:08/22/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/22/2018  Ralvarez				BUSCA EL LISTADO DE CUOTAS PAGADAS POR EL CLIENTE
**********************************************************************************************************************************************************/
--[Loan].[SP_GET_LOANS_QUOTA_HEADER] 128
CREATE PROC [Loan].[SP_GET_LOANS_QUOTA_HEADER]
	 @accountId BIGINT = NULL
AS 

	SELECT  
		 transactionPaymentPlanId --Id
		,quotaNumber --Cuota
		,tpp.emisionQuotaDate --Fecha
		,tpp.endQuotaDate --Vencimiento

		,validationTotal=tpp.expenseAmount --Total
		+tpp.commissionAmount 
		+tpp.interestAmoint
		+tpp.capitalAmount 
		+tpp.rateLateAmount
		

		,validationBalance=tpp.expenseBalance --Saldo
		+tpp.commissionBalance 
		+tpp.interestBalance
		+tpp.capitalAmount 
		+tpp.rateLateBalance 
		+chargesPrepayment

		,ISLastQuota --Pago Adelantado
		,IsPrepayment --Ult. Cuota
		--Motivo emisión cuota  1- Normal 2-Gastos pagados en desembolso 3- Por abono extraordinaro (desde el programa de Abono Extraordnario)
		,transactionReasonName --Ind. Cuota

		,ls.LoanStatusName  -- Estado
		,rc.fullName --Nombre Cuenta
		,tpp.[accountId] --No. Cuenta
		,lh.loanNumber --Id
		,tpp.transactionReasonId


		--DETALLE  
		,tpp.capitalAmount  -- Monto Capital
		,tpp.interestAmoint -- Monto Interes
		,tpp.commissionAmount  -- Monto Comisión
		,tpp.expenseAmount -- Monto Gastos
		,tpp.rateLateAmount -- Monto Mora
		,chargesPrepayment -- Monto Cargo

		
		
		--DETALLE  
		,tpp.capitalBalance   -- Saldo Capital
		,tpp.interestBalance  -- Saldo Interes
		,tpp.commissionBalance   -- Saldo Comisión
		,tpp.expenseBalance  -- Saldo Gastos
		,tpp.rateLateBalance  -- Saldo Mora
		,chargesPrepaymentBalance =0 -- Saldo Cargo
		 

		 /*PARA ESTAS FECHAS SE BUSCAN LAS TRANSACCIONES QUE TIENEN QUE VER CON RECIBO DE INGRESO (104)*/
		,dates.dCapital  --Fecha Ult. Pago Capital
		,dates.dInterestAmoint  --Fecha Ult. Pago Interes
		,dates.dCommissionAmoun  --Fecha Ult. Pago Comisión
		,dates.dExpenseAmount  --Fecha Ult. Pago  Gastos
		,dates.dRateLateAmount  --Fecha Ult. Pago Mora
		,dates.dChargeAmount  --Fecha Ult. Pago Cargo
		,dates.dDiscountAmount  --Fecha Ult. Pago
		

	FROM [Loan].[transactionPaymentPlan] tpp
	INNER JOIN [Loan].[Account] acc 
	ON acc.[accountId]= tpp.[accountId]

	INNER JOIN [Loan].[loanHeader] lh
	ON lh.[accountId]= tpp.[accountId]


	INNER JOIN Loan.LoanStatus ls 
	ON ls.loanStatusId= lh.loanStatusId

	INNER JOIN [Entity].[client] c 
	ON c.clientId = lh.[relatedContactId]


	INNER JOIN [Entity].[relatedContact] rc 
	ON rc.[relatedContactId]= c.[relatedContactId]

	LEFT JOIN  [Loan].[transactionReason] tr 
	ON tr.transactionReasonId=tpp.transactionReasonId

	CROSS APPLY 
	(
			SELECT  
				 Max(CASE WHEN capitalAmount>0 THEN tr.transactionDate ELSE NULL END)    dCapital 
				,Max(CASE WHEN interestAmoint>0 THEN tr.transactionDate ELSE NULL END)   dInterestAmoint
				,Max(CASE WHEN rateLateAmount>0 THEN tr.transactionDate ELSE NULL END)   dRateLateAmount
				,Max(CASE WHEN expenseAmount>0 THEN tr.transactionDate ELSE NULL END)    dExpenseAmount
				,Max(CASE WHEN commissionAmount>0 THEN tr.transactionDate ELSE NULL END) dCommissionAmoun
				,Max(CASE WHEN discountAmount>0 THEN tr.transactionDate ELSE NULL END)   dDiscountAmount
				,Max(CASE WHEN chargeAmount>0 THEN tr.transactionDate ELSE NULL END)     dChargeAmount
			FROM [Loan].[transactionReceipt] tr
			INNER JOIN [Loan].[transactionReceiptDetail] trd 
				ON   trd.transactionReceiptId=tr.transactionReceiptId
				AND  trd.capitalAmount>0
				

			WHERE  
		  tr.transactionDefinitionId in (select transactionDefinitionId from [Loan].[transactionDefinition] where transactionCode=104)
				 AND trd.transactionPaymentPlanId=tpp.transactionPaymentPlanId AND tr.ACCOUNTID = tpp.ACCOUNTID

	
			

	) dates
	WHERE 
			--tpp.isactive=1 AND
		 (tpp.[accountId]=@accountId  OR @accountId IS NULL)
	ORDER BY tpp.[accountId],tpp.quotaNumber




GO
/****** Object:  StoredProcedure [Loan].[SP_UPDATE_ACCOUNTS]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name:[Loan].[SP_UPDATE_ACCOUNTS]
** Desc:Creado para actualizar los registros en [Loan].[Account]
** Auth:Ralvarez
** Date:08/30/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/30/2018  Ralvarez   			   Creado para actualizar los registros en [Loan].[Account]
**********************************************************************************************************************************************************/	

CREATE PROCEDURE [Loan].[SP_UPDATE_ACCOUNTS]
(
	 @accountId	bigint	
	,@companyId	int	
	,@officeId	int	
	,@ConceptTypeId	int	
	,@accountTypeID	int	
	,@currencyId	int	
	,@relatedContactId	bigint	
	,@addressId	bigint	
	,@accountStatusId	int	
	,@founIncomeId	int	
	,@accountExecutiveContactId	bigint	
	,@accountPromoterContactId	bigint	
	,@financialSectorId	int	
	,@serviceProductId	int	
	,@accountName	varchar(250)
	,@userName	varchar(250)
	,@SendStatementAccount	bit	
	,@IsJointLoan	bit	
	,@RateType	varchar(50)
	,@account	varchar(50)
	,@isActive	bit	
	,@Action bit
)
AS
BEGIN 
	IF @Action = 1
		BEGIN
			UPDATE q SET 
				 accountId					=@accountId
				,companyId					=@companyId
				,officeId					=@officeId
				,ConceptTypeId				=@ConceptTypeId
				,accountTypeID				=@accountTypeID
				,currencyId					=@currencyId
				,relatedContactId			=@relatedContactId
				,addressId					=@addressId
				,accountStatusId			=@accountStatusId
				,founIncomeId				=@founIncomeId
				,accountExecutiveContactId	=@accountExecutiveContactId
				,accountPromoterContactId	=@accountPromoterContactId
				,financialSectorId			=@financialSectorId
				,serviceProductId			=@serviceProductId
				,accountName				=@accountName
				,userName					=@userName
				,SendStatementAccount		=@SendStatementAccount
				,IsJointLoan				=@IsJointLoan
				,RateType					=@RateType
				,account					=@account
				,isActive					=@isActive
				,ModiDate=GETDATE()
			from [Loan].[Account]  q
		
			WHERE accountId=@accountId
		END
		ELSE 
		BEGIN 
			UPDATE [Loan].[Account] SET 
				 isActive	=0
			WHERE accountId=@accountId
		END
END 


GO
/****** Object:  StoredProcedure [Loan].[SP_UPDATE_CODEBTOR_TYPE]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name:Loan.SP_UPDATE_CODEBTOR_TYPE
** Desc:Creado para actualizar los registros en [Loan].[codebtorType]
** Auth:Dirson Breton
** Date:08/28/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/28/2018  Dirson Breton			Creado para actualizar los registros en [Loan].[codebtorType]
**********************************************************************************************************************************************************/
CREATE PROCEDURE [Loan].[SP_UPDATE_CODEBTOR_TYPE]
(
 @codebtorTypeId INT
,@codebtorTypeName VARCHAR(150)
,@isActive BIT
,@CreateDate DATETIME
,@ModiDate DATETIME
,@CreateUsrId INT
,@ModiUsrId INT
,@hostName VARCHAR(100)
,@Action BIT
)
AS
BEGIN
	IF @Action = 1
		BEGIN
			UPDATE [Loan].codebtorType
			SET
			codebtorTypeName = @codebtorTypeName,
			isActive = @isActive,
			CreateDate = @CreateDate,
			ModiDate = @ModiDate,
			CreateUsrId = @CreateUsrId,
			ModiUsrId = @ModiUsrId,
			hostName = @hostName
			WHERE codebtorTypeId = @codebtorTypeId
		END
	ELSE
		BEGIN
			UPDATE [Loan].codebtorType
			SET
			isActive = 0
			WHERE codebtorTypeId = @codebtorTypeId
		END
END

GO
/****** Object:  StoredProcedure [Loan].[SP_UPDATE_COLLATERAL_TYPE]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name:Loan.SP_UPDATE_COLLATERAL_TYPE
** Desc:Creado para actualizar los registros en [Loan].[collateralType]
** Auth:Dirson Breton
** Date:08/28/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/28/2018  Dirson Breton			Creado para actualizar los registros en [Loan].[collateralType]
**********************************************************************************************************************************************************/
CREATE PROCEDURE [Loan].[SP_UPDATE_COLLATERAL_TYPE]
(
 @collateralTypeId INT
,@collateralTypeName VARCHAR(150)
,@isActive BIT
,@CreateDate DATETIME
,@ModiDate DATETIME
,@CreateUsrId INT
,@ModiUsrId INT
,@hostName VARCHAR(100)
,@Action BIT
)
AS
BEGIN
	IF @Action = 1
		BEGIN
			UPDATE [Loan].collateralType
			SET
			collateralTypeName = @collateralTypeName,
			isActive = @isActive,
			CreateDate = @CreateDate,
			ModiDate = @ModiDate,
			CreateUsrId = @CreateUsrId,
			ModiUsrId = @ModiUsrId,
			hostName = @hostName
			WHERE collateralTypeId = @collateralTypeId
		END
	ELSE
		BEGIN
			UPDATE [Loan].collateralType
			SET
			isActive = 0
			WHERE collateralTypeId = @collateralTypeId
		END
END

GO
/****** Object:  StoredProcedure [Loan].[SP_UPDATE_COLLATERAL_TYPE_SIB]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Loan].[SP_UPDATE_COLLATERAL_TYPE_SIB]
(
 @collateralTypeSIBId INT
,@collateralTypeSIBName VARCHAR(150)
,@collateralTypeSIBCode VARCHAR(50)
,@isActive BIT
,@CreateDate DATETIME
,@ModiDate DATETIME
,@CreateUsrId INT
,@ModiUsrId INT
,@hostName VARCHAR(100)
,@Action BIT
)
AS
BEGIN
	IF @Action = 1
		BEGIN
			UPDATE [Loan].collateralTypeSIB
			SET
			--collateralTypeSIBId = @collateralTypeSIBId,
			collateralTypeSIBName = @collateralTypeSIBName,
			isActive = @isActive,
			CreateDate = @CreateDate,
			ModiDate = @ModiDate,
			CreateUsrId = @CreateUsrId,
			ModiUsrId = @ModiUsrId,
			hostName = @hostName
			WHERE collateralTypeSIBCode = @collateralTypeSIBCode
		END
	ELSE
		BEGIN
			UPDATE [Loan].collateralTypeSIB
			SET
			isActive = 0
			WHERE collateralTypeSIBCode = @collateralTypeSIBCode
		END
END

GO
/****** Object:  StoredProcedure [Loan].[SP_UPDATE_COMMITTEE]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name:Loan.SP_UPDATE_COMMITTEE
** Desc:Creado para actualizar los registros en [Loan].[committee]
** Auth:Dirson Breton
** Date:08/29/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/29/2018  Dirson Breton			Creado para actualizar los registros en [Loan].[committee]
**********************************************************************************************************************************************************/
CREATE PROCEDURE [Loan].[SP_UPDATE_COMMITTEE]
(
 @companyId INT
,@committeeId INT
,@currencyId INT
,@committeeName VARCHAR(35)
,@committeeCode VARCHAR(50)
,@minimumAmount DECIMAL(13,2)
,@MaximumAmount DECIMAL(13,2)
,@numberUsertoApprove INT
,@isActive BIT
,@CreateDate DATETIME
,@ModiDate DATETIME
,@CreateUsrId INT
,@ModiUsrId INT
,@hostName VARCHAR(100)
,@Action BIT
)
AS
BEGIN
	IF @Action = 1
		BEGIN
			UPDATE Loan.committee
			SET
			currencyId = @currencyId,
			committeeName = @committeeName,
			committeeCode = @committeeCode,
			minimumAmount = @minimumAmount,
			MaximumAmount = @MaximumAmount,
			numberUsertoApprove = @numberUsertoApprove,
			isActive = @isActive,
			CreateDate = @CreateDate,
			ModiDate = @ModiDate,
			CreateUsrId = @CreateUsrId,
			ModiUsrId = @ModiUsrId,
			hostName = @hostName
			WHERE companyId = @companyId and committeeId = @committeeId
		END
	ELSE
		BEGIN
			UPDATE Loan.committee
			SET
			isActive = 0
			WHERE companyId = @companyId and committeeId = @committeeId
		END
END

GO
/****** Object:  StoredProcedure [Loan].[SP_UPDATE_CREDIT_FACILITY]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name:Loan.SP_UPDATE_CREDIT_FACILITY
** Desc:Creado para actualizar los registros en Loan.creditFacility
** Auth:Dirson Breton
** Date:08/17/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/17/2018  Dirson Breton			Creado para actualizar los registros en Loan.creditFacility
**********************************************************************************************************************************************************/
CREATE PROCEDURE [Loan].[SP_UPDATE_CREDIT_FACILITY]
(
 @companyId INT
,@creditFacilityId INT
,@creditFacilityName VARCHAR(150)
,@isActive BIT
,@CreateDate DATETIME
,@ModiDate DATETIME
,@CreateUsrId INT
,@ModiUsrId INT
,@hostName VARCHAR(100)
,@Action BIT
)
AS
BEGIN
	IF @Action = 1
		BEGIN
			UPDATE [Loan].[creditFacility]
			SET
			creditFacilityName = @creditFacilityName,
			isActive = @isActive,
			CreateDate = @CreateDate,
			ModiDate = @ModiDate,
			CreateUsrId = @CreateUsrId,
			ModiUsrId = @ModiUsrId,
			hostName = @hostName
			WHERE companyId = @companyId
			AND creditFacilityId = @creditFacilityId
		END
	ELSE
		BEGIN
			UPDATE [Loan].[creditFacility]
			SET
			isActive = 0
			WHERE companyId = @companyId
			AND creditFacilityId = @creditFacilityId
		END
END

GO
/****** Object:  StoredProcedure [Loan].[SP_UPDATE_CREDIT_TYPE]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name:Loan.SP_UPDATE_CREDIT_TYPE
** Desc:Creado para actualizar los registros en Loan.creditType
** Auth:Dirson Breton
** Date:08/20/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/17/2018  Dirson Breton			Creado para actualizar los registros en Loan.creditType
**********************************************************************************************************************************************************/
CREATE PROCEDURE [Loan].[SP_UPDATE_CREDIT_TYPE]
(
 @creditTypeId INT
,@creditTypeName VARCHAR(150)
,@creditTypeCode VARCHAR(150)
,@isActive BIT
,@CreateDate DATETIME
,@ModiDate DATETIME
,@CreateUsrId INT
,@ModiUsrId INT
,@hostName VARCHAR(100)
,@Action BIT
)
AS
BEGIN
	IF @Action = 1
		BEGIN
			UPDATE [Loan].creditType
			SET
			creditTypeName = @creditTypeName,
			isActive = @isActive,
			CreateDate = @CreateDate,
			ModiDate = @ModiDate,
			CreateUsrId = @CreateUsrId,
			ModiUsrId = @ModiUsrId,
			hostName = @hostName
			WHERE creditTypeId = @creditTypeId
			AND creditTypeCode = @creditTypeCode
		END
	ELSE
		BEGIN
			UPDATE [Loan].creditType
			SET
			isActive = 0
			WHERE creditTypeId = @creditTypeId
			AND creditTypeCode = creditTypeCode
		END
END

GO
/****** Object:  StoredProcedure [Loan].[SP_UPDATE_EXPENSE_FORMCAL]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name:Loan.SP_UPDATE_EXPENSE_FORMCAL
** Desc:Creado para actualizar los registros en [Loan].[expenseFormCal]
** Auth:Dirson Breton
** Date:08/30/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/30/2018  Dirson Breton			Creado para actualizar los registros en [Loan].[expenseFormCal]
**********************************************************************************************************************************************************/
CREATE PROCEDURE [Loan].[SP_UPDATE_EXPENSE_FORMCAL]
(
 @expenseFormCalId INT
,@expenseFormCalName VARCHAR(150)
,@isActive BIT
,@CreateDate DATETIME
,@ModiDate DATETIME
,@CreateUsrId INT
,@ModiUsrId INT
,@hostName VARCHAR(100)
,@Action BIT
)
AS
BEGIN
	IF @Action = 1
		BEGIN
			UPDATE Loan.expenseFormCal
			SET
			expenseFormCalName = @expenseFormCalName,
			isActive = @isActive,
			CreateDate = @CreateDate,
			ModiDate = @ModiDate,
			CreateUsrId = @CreateUsrId,
			ModiUsrId = @ModiUsrId,
			hostName = @hostName
			WHERE expenseFormCalId = @expenseFormCalId
		END
	ELSE
		BEGIN
			UPDATE Loan.expenseFormCal
			SET
			isActive = 0
			WHERE expenseFormCalId = @expenseFormCalId
		END
END

GO
/****** Object:  StoredProcedure [Loan].[SP_UPDATE_INMOVABLE_PROPERTY_TYPE]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name:Loan.SP_UPDATE_INMOVABLE_PROPERTY_TYPE
** Desc:Creado para actualizar los registros en [Loan].[immovablePropertyType]
** Auth:Dirson Breton
** Date:08/28/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/28/2018  Dirson Breton			Creado para actualizar los registros en [Loan].[immovablePropertyType]
**********************************************************************************************************************************************************/
CREATE PROCEDURE [Loan].[SP_UPDATE_INMOVABLE_PROPERTY_TYPE]
(
 @immovablePropertyTypeId INT
,@immovablePropertyTypeName VARCHAR(150)
,@immovablePropertyTypeCode VARCHAR(50)
,@isActive BIT
,@CreateDate DATETIME
,@ModiDate DATETIME
,@CreateUsrId INT
,@ModiUsrId INT
,@hostName VARCHAR(100)
,@Action BIT
)
AS
BEGIN
	IF @Action = 1
		BEGIN
			UPDATE [Loan].[immovablePropertyType]
			SET
			immovablePropertyTypeName = @immovablePropertyTypeName,
			immovablePropertyTypeCode = @immovablePropertyTypeCode,
			isActive = @isActive,
			CreateDate = @CreateDate,
			ModiDate = @ModiDate,
			CreateUsrId = @CreateUsrId,
			ModiUsrId = @ModiUsrId,
			hostName = @hostName
			WHERE immovablePropertyTypeId = @immovablePropertyTypeId
		END
	ELSE
		BEGIN
			UPDATE [Loan].[immovablePropertyType]
			SET
			isActive = 0
			WHERE immovablePropertyTypeId = @immovablePropertyTypeId
		END
END

GO
/****** Object:  StoredProcedure [Loan].[SP_UPDATE_PAYMENT_FORM]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name:Loan.SP_UPDATE_PAYMENT_FORM
** Desc:Creado para actualizar los registros en [Loan].PaymentForm
** Auth:Dirson Breton
** Date:08/30/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/30/2018  Dirson Breton			Creado para actualizar los registros en [Loan].PaymentForm
**********************************************************************************************************************************************************/
CREATE PROCEDURE [Loan].[SP_UPDATE_PAYMENT_FORM]
(
 @PaymentFormId INT
,@PaymentFormName VARCHAR(150)
,@isActive BIT
,@CreateDate DATETIME
,@ModiDate DATETIME
,@CreateUsrId INT
,@ModiUsrId INT
,@hostName VARCHAR(100)
,@Action BIT
)
AS
BEGIN
	IF @Action = 1
		BEGIN
			UPDATE [Loan].PaymentForm
			SET
			PaymentFormName = @PaymentFormName,
			isActive = @isActive,
			CreateDate = @CreateDate,
			ModiDate = @ModiDate,
			CreateUsrId = @CreateUsrId,
			ModiUsrId = @ModiUsrId,
			hostName = @hostName
			WHERE PaymentFormId = @PaymentFormId
		END
	ELSE
		BEGIN
			UPDATE [Loan].PaymentForm
			SET
			isActive = 0
			WHERE PaymentFormId = @PaymentFormId
		END
END

GO
/****** Object:  StoredProcedure [Loan].[SP_UPDATE_PAYMENT_TYPE]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name:global.SP_UPDATE_PAYMENT_TYPE
** Desc:Creado para actualizar los registros en [Loan].[PaymentType]
** Auth:Dirson Breton
** Date:08/23/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/17/2018  Dirson Breton			Creado para actualizar los registros en [Loan].[PaymentType]
**********************************************************************************************************************************************************/
CREATE PROCEDURE [Loan].[SP_UPDATE_PAYMENT_TYPE]
(
 @PaymentTypeId INT
,@quotationPaymentTypeName VARCHAR(150)
,@isActive BIT
,@CreateDate DATETIME
,@ModiDate DATETIME
,@CreateUsrId INT
,@ModiUsrId INT
,@hostName VARCHAR(100)
,@Action BIT
)
AS
BEGIN
	IF @Action = 1
		BEGIN
			UPDATE [Loan].[PaymentType]
			SET
			quotationPaymentTypeName = @quotationPaymentTypeName,
			isActive = @isActive,
			CreateDate = @CreateDate,
			ModiDate = @ModiDate,
			CreateUsrId = @CreateUsrId,
			ModiUsrId = @ModiUsrId,
			hostName = @hostName
			WHERE PaymentTypeId = @PaymentTypeId
		END
	ELSE
		BEGIN
			UPDATE [Loan].[PaymentType]
			SET
			isActive = 0
			WHERE PaymentTypeId = @PaymentTypeId
		END
END

GO
/****** Object:  StoredProcedure [Loan].[SP_UPDATE_POLICY_TYPE]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name:Loan.SP_UPDATE_POLICY_TYPE
** Desc:Creado para actualizar los registros en [Loan].[policyType]
** Auth:Dirson Breton
** Date:08/28/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/28/2018  Dirson Breton			Creado para actualizar los registros en [Loan].[policyType]
**********************************************************************************************************************************************************/
CREATE PROCEDURE [Loan].[SP_UPDATE_POLICY_TYPE]
(
 @policyTypeId INT
,@policyTypeName VARCHAR(150)
,@policyTypeCode VARCHAR(50)
,@isActive BIT
,@CreateDate DATETIME
,@ModiDate DATETIME
,@CreateUsrId INT
,@ModiUsrId INT
,@hostName VARCHAR(100)
,@Action BIT
)
AS
BEGIN
	IF @Action = 1
		BEGIN
			UPDATE [Loan].policyType
			SET
			--policyTypeId = @policyTypeId,
			policyTypeName = @policyTypeName,
			isActive = @isActive,
			CreateDate = @CreateDate,
			ModiDate = @ModiDate,
			CreateUsrId = @CreateUsrId,
			ModiUsrId = @ModiUsrId,
			hostName = @hostName
			WHERE policyTypeCode = @policyTypeCode
		END
	ELSE
		BEGIN
			UPDATE [Loan].policyType
			SET
			isActive = 0
			WHERE policyTypeCode = @policyTypeCode
		END
END

GO
/****** Object:  StoredProcedure [Loan].[SP_UPDATE_QUOTATION_STATUS]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name:global.SP_UPDATE_QUOTATION_STATUS
** Desc:Creado para actualizar los registros en [Loan].[[quotationStatus]]
** Auth:Dirson Breton
** Date:08/23/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/17/2018  Dirson Breton			Creado para actualizar los registros en [Loan].[[quotationStatus]]
**********************************************************************************************************************************************************/
CREATE PROCEDURE [Loan].[SP_UPDATE_QUOTATION_STATUS]
(
 @quotationStatusId INT
,@quotationStatusName VARCHAR(150)
,@isActive BIT
,@CreateDate DATETIME
,@ModiDate DATETIME
,@CreateUsrId INT
,@ModiUsrId INT
,@hostName VARCHAR(100)
,@Action BIT
)
AS
BEGIN
	IF @Action = 1
		BEGIN
			UPDATE [Loan].[quotationStatus]
			SET
			quotationStatusName = @quotationStatusName,
			isActive = @isActive,
			CreateDate = @CreateDate,
			ModiDate = @ModiDate,
			CreateUsrId = @CreateUsrId,
			ModiUsrId = @ModiUsrId,
			hostName = @hostName
			WHERE quotationStatusId = @quotationStatusId
		END
	ELSE
		BEGIN
			UPDATE [Loan].[quotationStatus]
			SET
			isActive = 0
			WHERE quotationStatusId = @quotationStatusId
		END
END

GO
/****** Object:  StoredProcedure [Loan].[SP_UPDATE_QUOTATION_TYPE]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name:global.SP_UPDATE_QUOTATION_TYPE
** Desc:Creado para actualizar los registros en [Loan].[quotationType]
** Auth:Dirson Breton
** Date:08/23/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/17/2018  Dirson Breton			Creado para actualizar los registros en [Loan].[quotationType]
**********************************************************************************************************************************************************/
CREATE PROCEDURE [Loan].[SP_UPDATE_QUOTATION_TYPE]
(
 @quotationTypeId INT
,@quotationTypeName VARCHAR(150)
,@isActive BIT
,@CreateDate DATETIME
,@ModiDate DATETIME
,@CreateUsrId INT
,@ModiUsrId INT
,@hostName VARCHAR(100)
,@Action BIT
)
AS
BEGIN
	IF @Action = 1
		BEGIN
			UPDATE [Loan].[quotationType]
			SET
			quotationTypeName = @quotationTypeName,
			isActive = @isActive,
			CreateDate = @CreateDate,
			ModiDate = @ModiDate,
			CreateUsrId = @CreateUsrId,
			ModiUsrId = @ModiUsrId,
			hostName = @hostName
			WHERE quotationTypeId = @quotationTypeId
		END
	ELSE
		BEGIN
			UPDATE [Loan].[quotationType]
			SET
			isActive = 0
			WHERE quotationTypeId = @quotationTypeId
		END
END

GO
/****** Object:  StoredProcedure [Loan].[SP_UPDATE_QUOTATIONS]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name:[Loan].[SP_UPDATE_QUOTATIONS]
** Desc:Creado para actualizar los registros en [Loan].[quotation]
** Auth:Ralvarez
** Date:08/29/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/29/2018  Ralvarez   			    Creado para actualizar los registros en [Loan].[quotation]
**********************************************************************************************************************************************************/	

CREATE PROCEDURE [Loan].[SP_UPDATE_QUOTATIONS]
(
	 @quotationId	bigint	
	,@relatedContactId	bigint	
	,@addressId	bigint	
	,@companyId	int	
	,@officeId	int	
	,@ConceptTypeId	int	
	,@destinationFundId	int	
	,@quotationPaymentTypeId	int	
	,@currencyId	int	
	,@quotationStatusId	int	
	,@committeeId	int	
	,@accountExecutiveContactId	bigint	
	,@accountPromoterContactId	bigint	
	,@financialSectorId	int	
	,@SourceIncomeId	int	
	,@founIncomeId	int	
	,@serviceProductId	int	
	,@quotationTypeId	int	
	,@businessLineId	int	
	,@amount	numeric(18,3)
	,@qoutationDate	datetime	
	,@qoutationDocDate	datetime	
	,@loanTerm	int	
	,@frequency	int	
	,@TypeDisbursement	int	
	,@IsJointLoan	bit	
	,@rateLate	numeric(18,3)
	,@comment	varchar(5000)
	,@clientName	varchar(250)
	,@creditFacility	int	
	,@quotaAmount	numeric(18,3)
	,@grace	int	
	,@graceDays	int	
	,@statementDate	int	
	,@SendStatementAccount	bit	
	,@AutomaticPayment	bit	
	,@RejectDate	datetime	
	,@MothToChangePrice	int	
	,@NumberOfReview	int	
	,@RateType	varchar(50)
	,@OriginCredit	nchar(20)
	,@Rate	numeric(18,3)
	,@RateCommission	numeric(18,3)
	,@isActive	bit	
	,@CreateDate	datetime	
	,@ModiDate	datetime	
	,@CreateUsrId	int	
	,@ModiUsrId	int	
	,@hostName	varchar(100)
	,@Action bit
)
AS
BEGIN 
	IF @Action = 1
		BEGIN
			UPDATE q SET 
				 relatedContactId				=@relatedContactId
				,addressId						=@addressId
				,companyId						=@companyId
				,officeId						=@officeId
				,ConceptTypeId					=@ConceptTypeId
				,destinationFundId				=@destinationFundId
				,quotationPaymentTypeId			=@quotationPaymentTypeId
				,currencyId						=@currencyId
				,quotationStatusId				=@quotationStatusId
				,committeeId					=@committeeId
				,accountExecutiveContactId		=@accountExecutiveContactId
				,accountPromoterContactId		=@accountPromoterContactId
				,financialSectorId				=@financialSectorId
				,SourceIncomeId					=@SourceIncomeId
				,founIncomeId					=@founIncomeId
				,serviceProductId				=@serviceProductId
				,quotationTypeId				=@quotationTypeId
				,businessLineId					=@businessLineId
				,amount							=@amount
				,qoutationDate					=@qoutationDate
				,qoutationDocDate				=@qoutationDocDate
				,loanTerm						=@loanTerm
				,frequency						=@frequency
				,TypeDisbursement				=@TypeDisbursement
				,IsJointLoan					=@IsJointLoan
				,rateLate						=@rateLate
				,comment						=@comment
				,clientName						=@clientName
				,creditFacility					=@creditFacility
				,quotaAmount					=@quotaAmount
				,grace							=@grace
				,graceDays						=@graceDays
				,statementDate					=@statementDate
				,SendStatementAccount			=@SendStatementAccount
				,AutomaticPayment				=@AutomaticPayment
				,RejectDate						=@RejectDate
				,MothToChangePrice				=@MothToChangePrice
				,NumberOfReview					=@NumberOfReview
				,RateType						=@RateType
				,OriginCredit					=@OriginCredit
				,Rate							=@Rate
				,RateCommission					=@RateCommission
				,isActive						=@isActive
				,ModiDate=GETDATE()
			from [Loan].[quotation]  q
		
			WHERE quotationId=@quotationId
		END
		/*ELSE 
		BEGIN 
			UPDATE [Entity].[contactPhones] SET 
				 isActive	=0
			WHERE contactPhoneId=@contactPhoneId AND relatedContactId=@relatedContactId
		END */
END 


GO
/****** Object:  StoredProcedure [Loan].[SP_UPDATE_SERVICE_PRODUCT]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name:Loan.SP_UPDATE_SERVICE_PRODUCT
** Desc:Creado para actualizar los registros en [Loan].[serviceProduct]
** Auth:Dirson Breton
** Date:08/30/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/30/2018  Dirson Breton			Creado para actualizar los registros en [Loan].[serviceProduct]
**********************************************************************************************************************************************************/
CREATE PROCEDURE [Loan].[SP_UPDATE_SERVICE_PRODUCT]
(
 @serviceProductId INT
,@accountTypeID INT
,@serviceProducName VARCHAR(150)
,@serviceProducCode VARCHAR(50)
,@isActive BIT
,@CreateDate DATETIME
,@ModiDate DATETIME
,@CreateUsrId INT
,@ModiUsrId INT
,@hostName VARCHAR(100)
,@Action BIT
)
AS
BEGIN
	IF @Action = 1
		BEGIN
			UPDATE Loan.serviceProduct
			SET
			accountTypeID = @accountTypeID,
			serviceProducName = @serviceProducName,
			serviceProducCode = @serviceProducCode,
			isActive = @isActive,
			CreateDate = @CreateDate,
			ModiDate = @ModiDate,
			CreateUsrId = @CreateUsrId,
			ModiUsrId = @ModiUsrId,
			hostName = @hostName
			WHERE serviceProductId = @serviceProductId
		END
	ELSE
		BEGIN
			UPDATE Loan.serviceProduct
			SET
			isActive = 0
			WHERE serviceProductId = @serviceProductId
		END
END

GO
/****** Object:  StoredProcedure [Recaudo].[CardNetGenerateFiles]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--USE [Loans]
--GO
--/****** Object:  StoredProcedure [Recaudo].[CardNetGenerateFiles]    Script Date: 9/7/2018 10:35:43 AM ******/
--SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO

CREATE PROC [Recaudo].[CardNetGenerateFiles]
  @UserId INT=1
 ,@Comments VARCHAR(5000)=''
AS 

--declare @UserId INT=1
--,@Comments VARCHAR(5000)=''

 
OPEN SYMMETRIC KEY [CreditCardKey]
DECRYPTION BY CERTIFICATE [CERTCreditCardKey];  
DECLARE @MerchantNumber CHAR(16)=CAST('349077263' AS CHAR(16))
DECLARE @TerminalNumber CHAR(16)=CAST('25100023' AS CHAR(16))

 
DECLARE @clienteFormat AS TABLE
([NombreInscripto]       [VARCHAR](8000) NULL,
 [RNC_Cedula]            [VARCHAR](8000) NULL,
 [RNC_CedulaOnlyNumber]  [VARCHAR](256) NULL,
 [admrel_codigo]         [INT] NOT NULL,
 [admrel_nombre]         [VARCHAR](200) NOT NULL,
 [DescripciónReferencia] [VARCHAR](20) NULL,
 [Direccion]             [VARCHAR](60) NULL,
 [Descripcion]           [VARCHAR](30) NULL,
 Email				[VARCHAR](80) NULL
);

DECLARE @BatchHeder AS TABLE 
(
	 RecordType CHAR(3)
	,TransactionCurrencyCode CHAR(3)
	,MerchantBatchDate CHAR(8)
	,BatchTransmitTime CHAR(4)
	,MerchantNumber CHAR(16)
	,MerchantBatchNumber CHAR(5)
	,Reserved CHAR(13)
	,CardAcceptorName CHAR(40)
	,Reserved2 CHAR(8)

)
DECLARE @BatchDetail AS TABLE 
(
	 RecordType CHAR(3)
	,TranSequenceNumber CHAR(4)
	,TransType CHAR(1)
	,AccountNumber CHAR(19)
	,TerminalNumber CHAR(16)
	,TransactionAmount CHAR(12)
	,AmountSettlement CHAR(12)
	,ExpirationDate CHAR(4)
	,Reserved CHAR(4)
	,ReferenceNumber CHAR(12)
	,Reserved2 CHAR(13)


	,AccountNumberF varchar(19)
	,TransactionAmountF numeric(18,2)
	,AmountSettlementF numeric(18,2)
	,ReferenceNumberF bigint
	

) 
DECLARE @BatchTrailer AS TABLE 
(
	 RecordType CHAR(3)
	,BatchTransactionCount CHAR(5)
	,BatchAmount CHAR(16)
	,Reserved CHAR(13)
	,Reserved2 CHAR(63)
	,BatchAmountF numeric(18,2)
) 
DECLARE @PagosFormat AS TABLE
(
	[Monto]                   [DECIMAL](17, 2) NULL,
	[MontoAtraso]             [DECIMAL](17, 2) NULL,
	[NoReferencia]            [VARCHAR](40) NULL,
	[precuo_fecvencformat]    [NVARCHAR](4000) NULL,
	[precuo_fecvencnotformat] [SMALLDATETIME] NOT NULL,
	[client_codigo]           [INT] NOT NULL,
	[cuecta_formateado] CHAR(20) not null, 
	[Estado] CHAR(1) not null,
	cuecta_numid BIGINT,
	AccountNumber VARCHAR(19)
); 
INSERT INTO @PagosFormat
SELECT 
        Monto= SUM(precuo_salcap+precuo_salint+precuo_salcom+precuo_salgas+precuo_salmor)
       ,MontoAtraso= SUM( CASE WHEN  DATEDIFF(DAY,precuo_fecvenc,GETDATE()) > precon_maxgramor  THEN  precuo_salcap+precuo_salint+precuo_salcom+precuo_salgas+precuo_salmor ELSE 0 END)
       ,NoReferencia=cast(cuecta_numid as varchar(10)) --+cast(admsuc_codigo as varchar(10))+cast(precuo_numcuota as varchar(10))+cast(client_codigo as varchar(10))
       ,precuo_fecvencformat=FORMAT(MAX(precuo_fecvenc), 'ddMMyyyy', 'en-US' )
	   ,precuo_fecvencnotformat=MAX(precuo_fecvenc)
       ,client_codigo
       ,cuecta_formateado
       ,Estado='I'
	   ,cuecta_numid
	   ,AccountNumber
FROM EasyBank.[dbo].[VW_CuotasPendientes]
INNER JOIN 
(
		select 
			 accountId
			,CONVERT(varchar(19), decryptbykey(MAX(CardNumber)))  AccountNumber
		from [Loan].[LoanDomiciliationCards] ldc
		INNER JOIN  [global].[ContactsDomiciliationCards] cdc 
			on ldc.clientId =cdc.clientId
			and  ldc.CardTypeId =cdc.CardTypeId
			and  ldc.LastFourDigits =cdc.LastFourDigits
		WHERE ldc.isActive = 1 AND cdc.isActive = 1
	  GROUP BY accountId
) D on d.accountId=cuecta_numid


GROUP BY cuecta_numid,client_codigo,cuecta_formateado,AccountNumber

/*VALIDAR QUE NO TENGA UN  COBRO PENDIENTE YA CREADO EN OTRO ARCHIVO */
DELETE  @PagosFormat
WHERE 
	 EXISTS 
	(

		select  ReferenceNumberF,TransactionAmountF from [Recaudo].[CardNetBatchDetail] pendientes
		WHERE pendientes.EstatusID =8 /*PENDIENTE DE PAGO EASYBANK*/ 
		and pendientes.ReferenceNumberF=cuecta_numid
		--and pendientes.TransactionAmountF=Monto
	)



INSERT INTO  @clienteFormat 
select 
       NombreInscripto =CASE WHEN RTRIM(LTRIM(dc.admrel_nombre))> 50 THEN RTRIM(LTRIM(dc.admrel_nombre)) ELSE RTRIM(LTRIM(dc.admrel_nombre)) END 
                                   + ISNULL(replicate (' ',50-LEN(CASE WHEN RTRIM(LTRIM(dc.admrel_nombre))> 50 THEN RTRIM(LTRIM(dc.admrel_nombre)) ELSE RTRIM(LTRIM(dc.admrel_nombre)) END )  ),'')
        ,RNC_Cedula = 
                                     [dbo].[udf_GetNumeric](RTRIM(LTRIM(dc.admide_numero)))
                                  + ISNULL(replicate ('0',11-LEN([dbo].[udf_GetNumeric](RTRIM(LTRIM(dc.admide_numero))) )  ),'')
   
        ,RNC_CedulaOnlyNumber = [dbo].[udf_GetNumeric](RTRIM(LTRIM(dc.admide_numero)))
       ,dc.admrel_codigo
       ,admrel_nombre
       ,DescripciónReferencia=rtrim(ltrim(admide_numero))
       ,Direccion=rtrim(ltrim(CASE WHEN LEN(admciu_nombre) > 50 THEN SUBSTRING(admciu_nombre,0,49) ELSE admciu_nombre END))
       ,Descripcion=CAST('' AS VARCHAR(30))
       ,CASE WHEN  PATINDEX('%@%',Email) > 0 AND PATINDEX('%.%',Email)> 0 THEN     ISNULL(Email,'') ELSE '' END Email

       
FROM
(

       
       SELECT 
		con.admide_numero,admrel_codigo=client_codigo,max(ISNULL(con.admrel_nombre,client_nombre)) admrel_nombre,ISNULL(MAX(admciu_nombre),'') admciu_nombre
       FROM EasyBank.[dbo].[eclientm] C2
       INNER JOIN EasyBank.[dbo].[xadmrelm] con 
		ON	c2.client_codigo=con.admrel_codigo
       LEFT JOIN  EasyBank.[dbo].[xxClientesTodos] C  
		ON c2.client_codigo=c.admrel_codigo
       GROUP BY con.admide_numero,client_codigo
) AS dc 
LEFT JOIN 
(
       SELECT 
                     admrel_codigo
                    ,MAX(admurl_nombre) AS Email
                    ,MAX(admurl_seq) admurl_seq
       FROM EasyBank.[dbo].xadmurld
       WHERE admrel_codigo NOT IN (SELECT admrel_codigo FROM EasyBank.[dbo].xadmrold WHERE admrol_codigo='CLT'  AND admurl_seq IS NOT NULL )
       GROUP BY admrel_codigo
       UNION ALL 
       SELECT 
                     xadmurld.admrel_codigo
                    ,MAX(xadmurld.admurl_nombre) AS Email
                    ,MAX(xadmurld.admurl_seq)
       FROM EasyBank.[dbo].xadmurld
       INNER JOIN 
       (

             SELECT  
				admrel_codigo,max(admurl_seq) admurl_seq 
			 FROM EasyBank.[dbo].xadmrold
             WHERE admrol_codigo='CLT' AND admurl_seq IS NOT NULL 
             GROUP BY admrel_codigo
       ) prim on prim.admrel_codigo =xadmurld.admrel_codigo and  prim.admurl_seq=xadmurld.admurl_seq 
       GROUP BY  xadmurld.admrel_codigo
) email on email.admrel_codigo=dc.admrel_codigo



INSERT INTO @BatchHeder (RecordType,TransactionCurrencyCode,MerchantBatchDate,BatchTransmitTime,MerchantNumber,MerchantBatchNumber,Reserved,CardAcceptorName,Reserved2)
SELECT 
	 RecordType='T01'
	,TransactionCurrencyCode='214' /*840=Dólares usa, 214=Pesos, 862=Venezuela*/
	,MerchantBatchDate=CAST(YEAR(GETDATE()) AS CHAR(4)) + RIGHT('0' + CAST( MONTH(GETDATE()) AS VARCHAR(2)),2)+ RIGHT('0' + CAST( DAY(GETDATE()) AS VARCHAR(2)),2)
	,BatchTransmitTime= REPLACE(SUBSTRING( convert(varchar, getdate(),108),1,5),':','')
	,MerchantNumber=@MerchantNumber
	,MerchantBatchNumber= RIGHT('00000'+ ISNULL(CAST( (SELECT COUNT(*) FROM @PagosFormat) + 1 AS VARCHAR(5)),''),5) --ESTO DEBE SER UNA SECUENCIA INTERNA 
	,Reserved=CAST('0000000000000' AS CHAR(13))
	,CardAcceptorName=CAST('KREDI SI DOMICILIACION' AS CHAR(40))
	--CAST(CASE WHEN LEN(RTRIM(LTRIM(Direccion))> 23 THEN SUBSTRING(RTRIM(LTRIM(Direccion)),0,22) ELSE RTRIM(LTRIM(Direccion)) END  AS CHAR(23))
	,Reserved2=CAST('00000000' AS CHAR(8))

	INSERT INTO @BatchDetail(
	 RecordType 
	,TranSequenceNumber 
	,TransType 
	,AccountNumber 
	,TerminalNumber
	,TransactionAmount 
	,AmountSettlement 
	,ExpirationDate 
	,Reserved 
	,ReferenceNumber 
	,Reserved2

	,AccountNumberF
	,TransactionAmountF
	,AmountSettlementF
	,ReferenceNumberF
	)
	SELECT 
		 RecordType ='T02'
		,TranSequenceNumber =RIGHT('0000'+CAST(ROW_NUMBER() OVER(ORDER BY cuecta_formateado) AS VARCHAR(4)),4)
		,TransType ='N' /*“N” son transacciones normales, “V” equivale a anular la transacción, “I” se utiliza para indicar que fue una compra por Internet*/
		,AccountNumber=AccountNumber--CAST('5488780025245579'  AS CHAR(19)) /*CARDNUMBER*/ --Completar con que?
		,TerminalNumber=@TerminalNumber --Completar con que?
		,TransactionAmount =RIGHT('000000000000' +CAST(CAST(FLOOR(Monto ) AS VARCHAR(50)) + REPLACE(CAST(CAST(Monto-CAST(FLOOR(Monto ) AS NUMERIC(18,2))   AS numeric(18,2)) AS VARCHAR(100)),'0.','') AS VARCHAR(15)),12)
		,AmountSettlement ='000000000000'
		,ExpirationDate='2210'  -- fecha expiracion tarjeta
		,Reserved =cast('' as char(4))
		,ReferenceNumber =right(cast('' as char(12)) + cast(cuecta_numid as varchar(10)),12) --Completar con que?
		,Reserved2 =cast('0000000000000' as char(13))
		
		,AccountNumberF=CAST('5488780025245579'  AS CHAR(19)) /*CARDNUMBER*/ --Completar con que?
		,TransactionAmountF=Monto
		,AmountSettlementF=0
		,ReferenceNumberF=cuecta_numid
	FROM @PagosFormat

	INSERT INTO @BatchTrailer(
	 RecordType 
	,BatchTransactionCount 
	,BatchAmount 
	,Reserved 
	,Reserved2
	,BatchAmountF
	)
	SELECT 
		 RecordType ='T03'
		,BatchTransactionCount =RIGHT('00000'+BatchTransactionCount,5)
		,BatchAmount =RIGHT('0000000000000000' +CAST(CAST(FLOOR(Monto ) AS VARCHAR(50)) + REPLACE(CAST(CAST(Monto-CAST(FLOOR(Monto ) AS NUMERIC(18,2))   AS numeric(18,2)) AS VARCHAR(100)),'0.','') AS VARCHAR(15)),16)
		,Reserved =cast('0000000000000' as char(13))
		,Reserved2 =cast('000000000000000000000000000000000000000000000000000000000000000' as char(63))
		,BatchAmountF=Monto

	FROM 
	(
		SELECT 
				 CAST(COUNT(*) AS VARCHAR(5)) BatchTransactionCount
				,SUM(Monto) Monto
		FROM  @PagosFormat
	) d



	DECLARE @ArchivoID BIGINT =0
	DECLARE @ArchivoName VARCHAR(200) =''
	
	DECLARE @RES TABLE 
	(
		 ArchivoID BIGINT
		,ArchivoName VARCHAR(200)
	)


	
	INSERT INTO [Recaudo].[Archivo]
	(
		 TipoArchivoID
		,Fecha
		,ArchivoName
		,EstatusID
		,ModifiedDate
	)
	OUTPUT INSERTED.ArchivoID,INSERTED.ArchivoName INTO @RES
	SELECT 
		 TipoArchivoID=2 /*DOMICILIACION*/
		,Fecha =GETDATE()
		,ArchivoName='CardNetKredisi_' + SUBSTRING(cast( newid() as varchar(64)),0,8) +'_' + CAST(YEAR(GETDATE()) AS CHAR(4)) + RIGHT('0' + CAST( MONTH(GETDATE()) AS VARCHAR(2)),2)+ RIGHT('0' + CAST( DAY(GETDATE()) AS VARCHAR(2)),2)
		,EstatusID=1
		,ModifiedDate=GETDATE()

	SET @ArchivoID = (SELECT TOP 1 ArchivoID FROM @RES)
	SET @ArchivoName = (SELECT TOP 1 ArchivoName FROM @RES)
	INSERT INTO [Recaudo].[CardNetBatchHeder]
	(
		 ArchivoID
		,RecordType
		,TransactionCurrencyCode
		,MerchantBatchDate
		,BatchTransmitTime
		,MerchantNumber
		,MerchantBatchNumber
		,Reserved
		,CardAcceptorName
		,Reserved2
	    ,FileLine
		,Comments
		,CreateDate
		,ModiDate
		,CreateUsrId
		,ModiUsrId
		,hostName
	)
	SELECT 
		 ArchivoID=@ArchivoID
		,RecordType
		,TransactionCurrencyCode
		,MerchantBatchDate
		,BatchTransmitTime
		,MerchantNumber
		,MerchantBatchNumber
		,Reserved
		,CardAcceptorName
		,Reserved2
		,FileLine= ENCRYPTBYKEY(KEY_GUID(N'CreditCardKey'), RecordType+TransactionCurrencyCode+MerchantBatchDate+BatchTransmitTime+MerchantNumber+MerchantBatchNumber+Reserved+CardAcceptorName+Reserved2)
		,Comments=@Comments
		,CreateDate=GETDATE()
		,ModiDate=GETDATE()
		,CreateUsrId=@UserId
		,ModiUsrId=@UserId
		,hostName=HOST_NAME()
	FROM @BatchHeder

	
	INSERT INTO [Recaudo].[CardNetBatchDetail]
	(
		 ArchivoID
		,EstatusID
		,DetailId
		,RecordType
		,TranSequenceNumber
		,TransType
		,AccountNumber
		,TerminalNumber
		,TransactionAmount
		,AmountSettlement
		,ExpirationDate
		,Reserved
		,ReferenceNumber
		,Reserved2
		,AccountNumberF
		,TransactionAmountF
		,AmountSettlementF
		,ReferenceNumberF
		,FileLine
		,CreateDate
		,ModiDate
		,CreateUsrId
		,ModiUsrId
		,hostName
	)

	SELECT 
		 ArchivoID=@ArchivoID
		,EstatusID=1
		,DetailId = ROW_NUMBER() over (order by @ArchivoID)
		,RecordType
		,TranSequenceNumber
		,TransType
		,ENCRYPTBYKEY(KEY_GUID(N'CreditCardKey'), AccountNumber)
		,TerminalNumber
		,TransactionAmount
		,AmountSettlement
		,ExpirationDate
		,Reserved
		,ReferenceNumber
		,Reserved2
		,AccountNumberF =CASE WHEN LEN(RTRIM(LTRIM(AccountNumberF))) > 4 THEN REPLICATE('X', LEN(RTRIM(LTRIM(AccountNumberF)))-4) + RIGHT(RTRIM(LTRIM(AccountNumberF)),4) ELSE  RTRIM(LTRIM(AccountNumberF)) END
		 
		
		,TransactionAmountF
		,AmountSettlementF
		,ReferenceNumberF
		,FileLine=ENCRYPTBYKEY(KEY_GUID(N'CreditCardKey'), RecordType+TranSequenceNumber+TransType+AccountNumber+TerminalNumber+TransactionAmount+AmountSettlement+ExpirationDate+Reserved+ReferenceNumber+Reserved2)
		--,FileLine=RecordType+TranSequenceNumber+TransType+AccountNumber+TerminalNumber+TransactionAmount+AmountSettlement+ExpirationDate+Reserved+ReferenceNumber+Reserved2
		,CreateDate=GETDATE()
		,ModiDate=GETDATE()
		,CreateUsrId=@UserId
		,ModiUsrId=@UserId
		,hostName=HOST_NAME()
	FROM @BatchDetail


	INSERT INTO [Recaudo].[CardNetBatchTrailer]
	(
		 ArchivoID
		,RecordType
		,BatchTransactionCount
		,BatchAmount
		,Reserved
		,Reserved2
		,BatchAmountF
		,FileLine
		,CreateDate
		,ModiDate
		,CreateUsrId
		,ModiUsrId
		,hostName
	)
	SELECT 
		 ArchivoID=@ArchivoID
		,RecordType
		,BatchTransactionCount
		,BatchAmount
		,Reserved
		,Reserved2
		,BatchAmountF
		,FileLine=ENCRYPTBYKEY(KEY_GUID(N'CreditCardKey'), RecordType+BatchTransactionCount+BatchAmount+Reserved+Reserved2)
		,CreateDate=GETDATE()
		,ModiDate=GETDATE()
		,CreateUsrId=@UserId
		,ModiUsrId=@UserId
		,hostName=HOST_NAME()
	FROM @BatchTrailer


	SELECT ResFile.FData,@ArchivoName ArchivoName, @ArchivoID ArchivoID

	FROM (
	SELECT 
		FData = RecordType+TransactionCurrencyCode+MerchantBatchDate+BatchTransmitTime+MerchantNumber+MerchantBatchNumber+Reserved+CardAcceptorName+Reserved2
	FROM @BatchHeder

	UNION ALL 
	SELECT 
		FData =RecordType+TranSequenceNumber+TransType+AccountNumber+TerminalNumber+TransactionAmount+AmountSettlement+ExpirationDate+Reserved+ReferenceNumber+Reserved2
	FROM @BatchDetail

	UNION ALL 
	SELECT 
		FData =RecordType+BatchTransactionCount+BatchAmount+Reserved+Reserved2
	FROM @BatchTrailer
	) ResFile
	


	


	CLOSE SYMMETRIC KEY [CreditCardKey];

	/*
		DELETE FROM [Recaudo].[Archivo]
		WHERE TipoArchivoID=2
		DELETE FROM  [Recaudo].[CardNetBatchDetail]
		DELETE FROM [Recaudo].[CardNetBatchTrailer]
		DELETE FROM [Recaudo].[CardNetBatchHeder]
	*/
GO
/****** Object:  StoredProcedure [Recaudo].[CardNetGetFileForDownload]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [Recaudo].[CardNetGetFileForDownload] 
(@ArchivoID int)
--DECLARE @ArchivoID int =48
AS
  --[MontoFactura]<= MontoPendienteCuente and MontoPendienteCuente>0
 
OPEN SYMMETRIC KEY [CreditCardKey]
DECRYPTION BY CERTIFICATE [CERTCreditCardKey];  

SELECT ResFile.FData,ArchivoID=@ArchivoID 

       FROM (
       SELECT 
             FData = CONVERT(VARCHAR(200), DECRYPTBYKEY(FileLine))
       FROM [Recaudo].[CardNetBatchHeder]
       where ArchivoID =@ArchivoID

       UNION ALL 
       SELECT 
             FData =CONVERT(VARCHAR(200), DECRYPTBYKEY(FileLine))
       FROM [Recaudo].[CardNetBatchDetail]
       where ArchivoID =@ArchivoID

       UNION ALL 
       SELECT 
             FData = CONVERT(VARCHAR(200), DECRYPTBYKEY(FileLine))
       FROM [Recaudo].[CardNetBatchTrailer]
       where ArchivoID =@ArchivoID
       ) ResFile




CLOSE SYMMETRIC KEY [CreditCardKey];


GO
/****** Object:  StoredProcedure [Recaudo].[ConsolidadoTableroControl]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name:[Recaudo].[ConsolidadoTableroControl]
** Desc:LLENA LOS GRAFICOS DE TABLERO DE CONTROL 
** Auth:RALVAREZ
** Date:08/22/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    08/22/2018  Ralvarez				LLENA LOS GRAFICOS DE TABLERO DE CONTROL 
**********************************************************************************************************************************************************/
---[Recaudo].[ConsolidadoTableroControl] '20180101','20181231',2
CREATE PROCEDURE [Recaudo].[ConsolidadoTableroControl]
	@FechaDesde datetime, @FechaHasta datetime, @Tipo int --1= Consolidado, 2= RecaudoReferenciado, 3=PagosElectronicos, 4=DomiciliacionTarjetas, 5=PagosOficinas
AS

DECLARE @Data AS TABLE 
(
	 Fecha DATE 
	,[MontoTotalRecaudacion]  decimal
	,[MotoPagoBHD] decimal
	,ArchivoID INT
)
--182022
IF (@Tipo = 2)
BEGIN 
	INSERT INTO @Data (Fecha,[MontoTotalRecaudacion],[MotoPagoBHD],ArchivoID)
	SELECT 
		[Fecha],
		[MontoTotalRecaudacion],
		[MotoPagoBHD],
		ArchivoID
	FROM [Recaudo].[RecReferenciadoHeader] r
	
	WHERE  
				fecha >=@FechaDesde 
			and fecha<=@FechaHasta
END 

IF (@Tipo =1)
BEGIN 
	INSERT INTO @Data (Fecha,[MontoTotalRecaudacion],[MotoPagoBHD],ArchivoID)
	SELECT 
		[Fecha],
		[MontoTotalRecaudacion]=SUM([MontoTotalRecaudacion]),
		[MotoPagoBHD]=SUM([MotoPagoBHD]) + ISNULL(SUM(validationTotal),0),
		ArchivoID=0
	FROM [Recaudo].[RecReferenciadoHeader] r
	OUTER APPLY
	(
		SELECT  
		 validationTotal=
		 SUM(tpp.expenseAmount) --Total
		+SUM(tpp.commissionAmount)
		+SUM(tpp.interestAmoint)
		+SUM(tpp.capitalAmount)
		+SUM(tpp.rateLateAmount)
		
		,CAST(tpp.emisionQuotaDate AS DATE ) emisionQuotaDate
		FROM [Loan].[transactionPaymentPlan] tpp 
		WHERE CAST(tpp.emisionQuotaDate AS DATE ) = CAST(r.Fecha AS DATE)
		GROUP BY CAST(tpp.emisionQuotaDate AS DATE )
		--ORDER BY 2 DESC
	) pagos
	WHERE  
				fecha >=@FechaDesde 
			AND fecha<=@FechaHasta
	GROUP BY [Fecha]
END
IF (@Tipo =5)
BEGIN 
	INSERT INTO @Data (Fecha,[MontoTotalRecaudacion],[MotoPagoBHD],ArchivoID)
	SELECT 
		[Fecha],
		[MontoTotalRecaudacion]=SUM([MontoTotalRecaudacion]),
		[MotoPagoBHD]=ISNULL(SUM(validationTotal),0),
		ArchivoID=0
	FROM [Recaudo].[RecReferenciadoHeader] r
	OUTER APPLY
	(
		SELECT  
		 validationTotal=
		 SUM(tpp.expenseAmount) --Total
		+SUM(tpp.commissionAmount)
		+SUM(tpp.interestAmoint)
		+SUM(tpp.capitalAmount)
		+SUM(tpp.rateLateAmount)
		
		,CAST(tpp.emisionQuotaDate AS DATE ) emisionQuotaDate
		FROM [Loan].[transactionPaymentPlan] tpp 
		WHERE CAST(tpp.emisionQuotaDate AS DATE ) = CAST(r.Fecha AS DATE)
		GROUP BY CAST(tpp.emisionQuotaDate AS DATE )
		--ORDER BY 2 DESC
	) pagos
	WHERE  
				fecha >=@FechaDesde 
			AND fecha<=@FechaHasta
	GROUP BY [Fecha]
END
IF (@Tipo =4)
BEGIN 
	INSERT INTO @Data (Fecha,[MontoTotalRecaudacion],[MotoPagoBHD],ArchivoID)
	SELECT 
		[Fecha],
		[MontoTotalRecaudacion]=SUM([MontoTotalRecaudacion]),
		[MotoPagoBHD]=0,
		ArchivoID=0
	FROM [Recaudo].[VCardNetReferenciadoHeader] r
	WHERE  
				fecha >=@FechaDesde 
			AND fecha<=@FechaHasta
	GROUP BY [Fecha]
END
--IF (@Tipo =3)
--BEGIN 

--END
SELECT 
	 Fecha
	,[MontoTotalRecaudacion]
	,[MotoPagoBHD]
	,ArchivoID
FROM @Data
ORDER BY FECHA DESC


GO
/****** Object:  StoredProcedure [Recaudo].[GenerateFiles]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Recaudo].[GenerateFiles] (@InsertInTables bit)
AS
--declare @InsertInTables bit=1
Declare  @IDServicio VARCHAR(15) ='42291';
DECLARE @clienteFormat AS TABLE
([NombreInscripto]       [VARCHAR](8000) NULL,
 [RNC_Cedula]            [VARCHAR](8000) NULL,
 [RNC_CedulaOnlyNumber]  [VARCHAR](256) NULL,
 [admrel_codigo]         [INT] NOT NULL,
 [admrel_nombre]         [VARCHAR](200) NOT NULL,
 [DescripciónReferencia] [VARCHAR](20) NULL,
 [Direccion]             [VARCHAR](60) NULL,
 [Descripcion]           [VARCHAR](30) NULL,
 Email				[VARCHAR](80) NULL
);
DECLARE @PagosFormat AS TABLE
([Monto]                   [DECIMAL](17, 2) NULL,
 [MontoAtraso]                   [DECIMAL](17, 2) NULL,
 [NoReferencia]            [VARCHAR](40) NULL,
 [precuo_fecvencformat]    [NVARCHAR](4000) NULL,
 [precuo_fecvencnotformat] [SMALLDATETIME] NOT NULL,
 --[cuecta_numid]            [INT] NOT NULL,
 --[admsuc_codigo]           [CHAR](3) NOT NULL,
 --[admmon_codigo]           [SMALLINT] NOT NULL,
 --[precuo_numcuota]         [SMALLINT] NULL,
 --[TipoCuota]               [VARCHAR](50) NULL,
 --[precuo_salcap]           [DECIMAL](13, 2) NOT NULL,
 --[precuo_salint]           [DECIMAL](13, 2) NOT NULL,
 --[precuo_salcom]           [DECIMAL](13, 2) NOT NULL,
 --[precuo_salgas]           [DECIMAL](13, 2) NOT NULL,
 --[precuo_salmor]           [DECIMAL](13, 2) NULL,
 --[cuecta_nombre]           [VARCHAR](400) NULL,
 --[precuo_fecvenc]          [SMALLDATETIME] NOT NULL,
 --[cuecta_formateado]       [CHAR](20) NOT NULL,
 --[admsuc_nombre]           [CHAR](40) NULL,
 --[admmon_nombre]           [CHAR](20) NULL,
 --[prepre_saldo]            [DECIMAL](13, 2) NOT NULL,
 --[precon_maxgramor]        [SMALLINT] NOT NULL,
 --[prepre_indlegal]         [SMALLINT] NULL,
 --[client_nombre]           [VARCHAR](200) NULL,
 --[admrol_codigo]           [CHAR](3) NOT NULL,
 --[admrel_codigo]           [INT] NOT NULL,
 --[prepre_fecdesemb]        [SMALLDATETIME] NULL,
 --[prepre_numdoc]           [CHAR](20) NULL,
 --[admcia_codigo]           [CHAR](3) NOT NULL,
 --[admsts_codigo]           [CHAR](2) NOT NULL,
 [client_codigo]           [INT] NOT NULL,
 [cuecta_formateado] char(20) not null, 
 [Estado] char(1) not null
);
DECLARE @RecaudoReferenciado AS TABLE
([NoReferencia]             [VARCHAR](40) NULL,
 [DescripciónReferencia]    [VARCHAR](20) NULL,
 [Direccion]                [VARCHAR](60) NULL,
 [Descripcion]              [VARCHAR](30) NULL,
 [FechaFactura]             [NVARCHAR](4000) NULL,
 [FechaLimitePago]          [NVARCHAR](4000) NULL,
 [FechaCorte]               [NVARCHAR](4000) NULL,
 [FechaFacturaNotFormat]    [SMALLDATETIME] NOT NULL,
 [FechaLimitePagoNotFormat] [SMALLDATETIME] NOT NULL,
 [FechaCorteNotFormat]      [SMALLDATETIME] NOT NULL, 
 [MontoNotFormat]           [DECIMAL](17, 2) NULL,
 [TotalPagarNotFormat]      [DECIMAL](17, 2) NULL,
 [PagoMinimoNotFormat]      [DECIMAL](17, 2) NULL,
 [Moneda]                   [VARCHAR](3) NULL,
 [Monto]                    [VARCHAR](15) NULL,
 [Atraso]                   [VARCHAR](15) NULL,
 [Otros]                    [VARCHAR](15) NULL,
 [TotalPagar]               [VARCHAR](15) NULL,
 [PagoMinimo]               [VARCHAR](15) NULL,
 [NombreCompleto]           [VARCHAR](200) NULL,
 [Estado]                   [VARCHAR](1) NOT NULL,
 [IDServicio]               [VARCHAR](15) NULL,
 [CedulaRNC]                [VARCHAR](8000) NULL,
 [Email]                    [VARCHAR](80) NULL,
 [NCF]                      [VARCHAR](19) NULL,
 [ITBIS]                    [VARCHAR](15) NULL,
 [OtrosImpuestos]           [VARCHAR](15) NULL
 
);
DECLARE @CobrosAutomaticos AS TABLE
([NombreInscripto] [VARCHAR](8000) NULL,
 [RNC_Cedula]      [VARCHAR](8000) NULL,
 [MontoFormat]     [VARCHAR](8000) NULL,
 [Monto2]          [DECIMAL](38, 2) NULL,
 [Monto3]          [DECIMAL](38, 0) NULL,
 [IDServicio]      [VARCHAR](15) NULL,
 [TipoProducto]    [VARCHAR](2) NOT NULL,
 [NumeroProducto]  [VARCHAR](20) NULL,
 [Moneda]          [VARCHAR](3) NOT NULL,
 [TipoRegistro]    [VARCHAR](1) NOT NULL,
 [Banco]           [VARCHAR](3) NOT NULL,
 [Email]           [VARCHAR](13) NOT NULL,
 [Referencia]      [VARCHAR](13) NOT NULL,
 [NCF]             [VARCHAR](1) NOT NULL,
 [ITBIS]           [VARCHAR](1) NOT NULL,
 [OTROS]           [VARCHAR](1) NOT NULL
);
DECLARE @Notificaciones AS TABLE
([NumeroCuenta]   [VARCHAR](13) NOT NULL,
 [CodigoBanco]    [VARCHAR](3) NOT NULL,
 [TipoCuenta]     [VARCHAR](2) NOT NULL,
 [NombreCliente]  [VARCHAR](200) NULL,
 [Identificacion] [VARCHAR](256) NULL,
 [Descripcion]    [VARCHAR](14) NOT NULL
);
DECLARE @FileNotificaciones AS TABLE(LINE VARCHAR(MAX));
DECLARE @FileCobrosAutomaticos AS TABLE(LINE VARCHAR(MAX));
DECLARE @FileRecaudoReferenciado AS TABLE(LINE VARCHAR(MAX));


insert into  @clienteFormat 
select 
       NombreInscripto =CASE WHEN RTRIM(LTRIM(dc.admrel_nombre))> 50 THEN RTRIM(LTRIM(dc.admrel_nombre)) ELSE RTRIM(LTRIM(dc.admrel_nombre)) END 
                                   + ISNULL(replicate (' ',50-LEN(CASE WHEN RTRIM(LTRIM(dc.admrel_nombre))> 50 THEN RTRIM(LTRIM(dc.admrel_nombre)) ELSE RTRIM(LTRIM(dc.admrel_nombre)) END )  ),'')
     ,RNC_Cedula = 
                                     [dbo].[udf_GetNumeric](RTRIM(LTRIM(dc.admide_numero)))
                                  + ISNULL(replicate ('0',11-LEN([dbo].[udf_GetNumeric](RTRIM(LTRIM(dc.admide_numero))) )  ),'')
   
    ,RNC_CedulaOnlyNumber = [dbo].[udf_GetNumeric](RTRIM(LTRIM(dc.admide_numero)))
       ,dc.admrel_codigo
       ,admrel_nombre
       ,DescripciónReferencia=rtrim(ltrim(admide_numero))
       ,Direccion=rtrim(ltrim(CASE WHEN LEN(admciu_nombre) > 50 THEN SUBSTRING(admciu_nombre,0,49) ELSE admciu_nombre END))
       ,Descripcion=CAST('' AS VARCHAR(30))
       ,CASE WHEN  PATINDEX('%@%',Email) > 0 AND PATINDEX('%.%',Email)> 0 THEN     ISNULL(Email,'') ELSE '' END Email

       
from
(

       
       SELECT con.admide_numero,admrel_codigo=client_codigo,max(ISNULL(con.admrel_nombre,client_nombre)) admrel_nombre,ISNULL(MAX(admciu_nombre),'') admciu_nombre
       FROM EasyBank.[dbo].[eclientm] C2
       INNER JOIN EasyBank.[dbo].[xadmrelm] con on   c2.client_codigo=con.admrel_codigo
       LEFT JOIN  EasyBank.[dbo].[xxClientesTodos] C  on c2.client_codigo=c.admrel_codigo
       GROUP BY con.admide_numero,client_codigo
) as dc 
left JOIN 
(
       select 
                     admrel_codigo
                    ,MAX(admurl_nombre) AS Email
                    ,MAX(admurl_seq) admurl_seq
       from EasyBank.[dbo].xadmurld
       WHERE admrel_codigo NOT IN (SELECT admrel_codigo FROM EasyBank.[dbo].xadmrold WHERE admrol_codigo='CLT'  AND admurl_seq IS NOT NULL )
       GROUP BY admrel_codigo
       UNION ALL 
       select 
                     xadmurld.admrel_codigo
                    ,MAX(xadmurld.admurl_nombre) AS Email
                    ,MAX(xadmurld.admurl_seq)
       from EasyBank.[dbo].xadmurld
       INNER JOIN 
       (

             select  admrel_codigo,max(admurl_seq) admurl_seq from EasyBank.[dbo].xadmrold
             where admrol_codigo='CLT' AND admurl_seq IS NOT NULL 
             group by admrel_codigo
       ) prim on prim.admrel_codigo =xadmurld.admrel_codigo and  prim.admurl_seq=xadmurld.admurl_seq 
       GROUP BY  xadmurld.admrel_codigo
) email on email.admrel_codigo=dc.admrel_codigo





   
   insert into @PagosFormat
SELECT 
        Monto= SUM( CASE WHEN  DATEDIFF(DAY,precuo_fecvenc,GETDATE()) <= precon_maxgramor  THEN  precuo_salcap+precuo_salint+precuo_salcom+precuo_salgas+precuo_salmor ELSE 0 END)
       ,MontoAtraso= SUM( CASE WHEN  DATEDIFF(DAY,precuo_fecvenc,GETDATE()) > precon_maxgramor  THEN  precuo_salcap+precuo_salint+precuo_salcom+precuo_salgas+precuo_salmor ELSE 0 END)
       ,NoReferencia=cast(cuecta_numid as varchar(10)) --+cast(admsuc_codigo as varchar(10))+cast(precuo_numcuota as varchar(10))+cast(client_codigo as varchar(10))
       ,precuo_fecvencformat=FORMAT(MAX(precuo_fecvenc), 'ddMMyyyy', 'en-US' )
	  ,precuo_fecvencnotformat=MAX(precuo_fecvenc)
       ,client_codigo
       ,cuecta_formateado
       ,Estado='I'
FROM EasyBank.[dbo].[VW_CuotasPendientes]
GROUP BY cuecta_numid,client_codigo,cuecta_formateado
UNION ALL
/*CLIENTES AL DIA O CANCELADO */
select 
        Monto= 0
       ,MontoAtraso= 0
       ,NoReferencia=cast(e.cuecta_numid as varchar(10))
       ,precuo_fecvencformat=FORMAT(GETDATE(), 'ddMMyyyy', 'en-US' )
	  ,precuo_fecvencnotformat=GETDATE()
       ,e.client_codigo
       ,e.cuecta_formateado
       ,Estado = CASE WHEN p.admsts_codigo IN (6,7) THEN 'C' ELSE 'I' END
from EasyBank.[dbo].ecuectam e
INNER JOIN EasyBank.[dbo].epreprem p on e.cuecta_numid=p.cuecta_numid
WHERE p.admsts_codigo NOT IN (6,7) AND e.cuecta_numid  NOT IN (SELECT cuecta_numid FROM EasyBank.[dbo].[VW_CuotasPendientes])


INSERT INTO @RecaudoReferenciado 

select 
              NoReferencia
             ,DescripciónReferencia='Numero de Prestamo'--RTRIM(LTRIM(cuecta_formateado))
             ,Direccion
             ,Descripcion=CAST('Pago cuota del Prestamo' AS VARCHAR(30))
             ,FechaFactura=precuo_fecvencformat
             ,FechaLimitePago=precuo_fecvencformat
             ,FechaCorte=precuo_fecvencformat
		     ,FechaFacturaNotFormat=precuo_fecvencnotformat
		     ,FechaLimitePagoNotFormat=precuo_fecvencnotformat
		     ,FechaCorteNotFormat=precuo_fecvencnotformat
		     ,MontoNotFormat=Monto
		     ,TotalPagarNotFormat=Monto + MontoAtraso
		     ,PagoMinimoNotFormat=CASE WHEN MontoAtraso > 0 THEN MontoAtraso ELSE (Monto + MontoAtraso) END 
             ,Moneda=CAST('000' AS VARCHAR(3))
             ,Monto =CAST(CAST(FLOOR(Monto ) AS VARCHAR(50)) + REPLACE(CAST(CAST(Monto-CAST(FLOOR(Monto ) AS NUMERIC(18,2))   AS numeric(18,2)) AS VARCHAR(100)),'0.','') AS VARCHAR(15))
             ,Atraso=CAST(CAST(FLOOR(MontoAtraso ) AS VARCHAR(50)) + REPLACE(CAST(CAST(MontoAtraso-CAST(FLOOR(MontoAtraso ) AS NUMERIC(18,2))   AS numeric(18,2)) AS VARCHAR(100)),'0.','') AS VARCHAR(15))
             ,Otros =CAST('000' AS VARCHAR(15))
             ,TotalPagar =
                           CAST(
                                               CAST(CAST(FLOOR((Monto + MontoAtraso) ) AS VARCHAR(50)) + REPLACE(CAST(CAST((Monto + MontoAtraso)-CAST(FLOOR((Monto + MontoAtraso) ) AS NUMERIC(18,2))   AS numeric(18,2)) AS VARCHAR(100)),'0.','') AS VARCHAR(15))
                           AS VARCHAR(15))
             ,PagoMinimo =CAST(CAST(FLOOR(CASE WHEN MontoAtraso > 0 THEN MontoAtraso ELSE (Monto + MontoAtraso) END ) AS VARCHAR(50)) + REPLACE(CAST(CAST(CASE WHEN MontoAtraso > 0 THEN MontoAtraso ELSE (Monto + MontoAtraso) END-CAST(FLOOR(CASE WHEN MontoAtraso > 0 THEN MontoAtraso ELSE (Monto + MontoAtraso) END ) AS NUMERIC(18,2))   AS numeric(18,2)) AS VARCHAR(100)),'0.','') AS VARCHAR(15))
             ,NombreCompleto=CASE WHEN len(RTRIM(LTRIM(dc.admrel_nombre)))> 100 THEN substring(RTRIM(LTRIM(dc.admrel_nombre)),0,99) ELSE RTRIM(LTRIM(dc.admrel_nombre)) END 
             ,Estado='I' --case when  (( row_number() over(order by NombreInscripto) ) % 2) =0 then 'C' else 'I'end
             ,IDServicio=@IDServicio
             ,CedulaRNC=RNC_CedulaOnlyNumber
             ,Email
             ,NCF=CAST('-' AS VARCHAR(19))
             ,ITBIS =CAST('000' AS VARCHAR(15))
             ,OtrosImpuestos  =CAST('000' AS VARCHAR(15))
from @PagosFormat cp 
  LEFT JOIN @clienteFormat dc 
   ON cp.client_codigo = dc.admrel_codigo

       


  insert into  @CobrosAutomaticos 
  select 
   
    NombreInscripto
   ,RNC_Cedula 
   ,MontoFormat=CAST(FLOOR(SUM(Monto) ) AS VARCHAR(50)) + REPLACE(CAST(CAST(SUM(Monto)-CAST(FLOOR(SUM(Monto) ) AS NUMERIC(18,2))   AS numeric(18,2)) AS VARCHAR(100)),'0.','')
   ,Monto2=SUM(Monto) 
   ,Monto3=FLOOR(SUM(Monto))
   ,IDServicio=@IDServicio
   ,TipoProducto ='CC'
   ,NumeroProducto ='0154503001' + cast( row_number() over(order by NombreInscripto) as varchar(10))
   ,Moneda ='000' /*000,034*/
   ,TipoRegistro=case when  cast( row_number() over(order by NombreInscripto) as varchar(10)) ='2' then 'C' else 'I'end
   ,Banco='BHD'
   ,Email='test@test.com'
   ,Referencia='NO REFERENCIA'
   ,NCF='-'
   ,ITBIS='0'
   ,OTROS='0'
  from @PagosFormat cp 
  LEFT JOIN @clienteFormat dc 
   on cp.client_codigo = dc.admrel_codigo
  GROUP BY NombreInscripto,RNC_Cedula



   insert into  @Notificaciones
       SELECT distinct 
                     NumeroCuenta = '0154503-001-0'
                    ,CodigoBanco='BHD'
                    ,TipoCuenta='CC'
                    ,NombreCliente=Rtrim(ltrim(admrel_nombre))
                    ,Identificacion=RNC_CedulaOnlyNumber
                    ,Descripcion ='Prueba Kredisi'
       FROM @clienteFormat dc
       WHERE EXISTS 
        (
                    SELECT TOP 1 1 FROM @PagosFormat cp 
                    WHERE cp.client_codigo = dc.admrel_codigo
       )
       

	insert into @FileNotificaciones 
       select 
             NumeroCuenta+';'
          +CodigoBanco+';'
          +TipoCuenta+';'
          +NombreCliente+';'
          +REPLACE(REPLACE(Identificacion,'0','1'),'1','9')+';'
          +Descripcion  as Line
       from  @Notificaciones

    insert into @FileCobrosAutomaticos 
       select 
        IDServicio+';'
       +NombreInscripto+';'
       +REPLACE(RNC_Cedula,'1','9')+';'
       +TipoProducto+';'
       +NumeroProducto+';'
       +Moneda+';'
       +ISNULL(replicate ('0',17-LEN(RTRIM(LTRIM(MontoFormat)) )  ),'')+ RTRIM(LTRIM(MontoFormat))+';'
       +TipoRegistro+';'
       +Banco+';'
       +Email+';'
       +Referencia+';'
       +NCF+';'
       +ITBIS+';'
       +OTROS+';' as Line
       from @CobrosAutomaticos 
 
     insert into @FileRecaudoReferenciado 
       select 
              NoReferencia+';'   
             +DescripciónReferencia+';' 
             +Direccion+';'      
             +Descripcion+';'    
             +FechaFactura+';'   
             +FechaLimitePago+';' 
             +FechaCorte+';'     
             +Moneda+';'  
             +Monto+';'   
             +Atraso+';'  
             +Otros+';'   
             +TotalPagar+';'     
             +PagoMinimo+';'     
             +NombreCompleto+';' 
             +Estado+';'  
             +IDServicio+';'     
             +CedulaRNC+';'      
             +Email+';'   
             +NCF+';'     
             +ITBIS+';'   
             +OtrosImpuestos as Line
       from @RecaudoReferenciado 



   IF (@InsertInTables =1)

   BEGIN 
      
   INSERT INTO Recaudo.Archivo (TipoArchivoID, Fecha,ArchivoName,EstatusID) VALUES (1,GETDATE(), 'FACLOT_'+FORMAT(GETDATE(), 'yyyyMMdd', 'en-US')+'.txt',1)
   INSERT INTO [Recaudo].[RecReferenciado]
           ([NoReferencia]
           ,[DescripcionReferencia]
           ,[Direccion]
           ,[Descripción]
           ,[FechaFactura]
           ,[FechaLimitePago]
           ,[FechaCorte]
           ,[Moneda]
           ,[Monto]
           ,[Atraso]
           ,[Otros]
           ,[TotalPagar]
           ,[PagoMinimo]
           ,[NombreCompleto]
           ,[Estado]
           ,[IDServicio]
           ,[RNCCedula]
           ,[Email]
           ,[NCF]
           ,[ITBIS]
           ,[OtrosImpuestos]
           ,[ArchivoID]
           ,[EstatusID]
           ,[CreatedDate])
    select [NoReferencia]
           ,[DescripciónReferencia]
           ,[Direccion]
           ,[Descripcion]
           ,[FechaFacturaNotFormat]
           ,[FechaLimitePagoNotFormat]
           ,[FechaCorteNotFormat]
           ,[Moneda]
           ,[MontoNotFormat]
           ,cast ([Atraso] as decimal(13,2))
           ,cast ([Otros]as decimal(13,2))
           ,[TotalPagarNotFormat]
           ,[PagoMinimoNotFormat]
           ,[NombreCompleto]
           ,[Estado]
           ,[IDServicio]
           ,CedulaRNC
           ,[Email]
           ,[NCF]
           ,cast ([ITBIS] as decimal(13,2))
           ,cast( [OtrosImpuestos] as decimal(13,2))
           ,scope_identity()
           ,1
           ,getdate()[CreatedDate]
		 from  @RecaudoReferenciado;

		 -- INSERT INTO Recaudo.Archivo (Fecha,ArchivoName,Estatus) VALUES (GETDATE(), 'FACLOT_'+FORMAT(GETDATE(), 'yyyyMMdd', 'en-US')+'.txt',1)

		 

   END
  SELECT *  FROM @FileRecaudoReferenciado --order by 1 desc--WHERE 1=10
GO
/****** Object:  StoredProcedure [Recaudo].[GenerateFiles_20180817]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [Recaudo].[GenerateFiles_20180817] (@InsertInTables bit)
AS
--declare @InsertInTables bit=1
DECLARE @IDServicio VARCHAR(15) ='42291';
DECLARE @clienteFormat AS TABLE
([NombreInscripto]       [VARCHAR](8000) NULL,
 [RNC_Cedula]            [VARCHAR](8000) NULL,
 [RNC_CedulaOnlyNumber]  [VARCHAR](256) NULL,
 [admrel_codigo]         [INT] NOT NULL,
 [admrel_nombre]         [VARCHAR](200) NOT NULL,
 [DescripciónReferencia] [VARCHAR](20) NULL,
 [Direccion]             [VARCHAR](60) NULL,
 [Descripcion]           [VARCHAR](30) NULL
);
DECLARE @PagosFormat AS TABLE
([Monto]                   [DECIMAL](17, 2) NULL,
 [NoReferencia]            [VARCHAR](40) NULL,
 [precuo_fecvencformat]    [NVARCHAR](4000) NULL,
 [precuo_fecvencnotformat] [SMALLDATETIME] NOT NULL,
 [cuecta_numid]            [INT] NOT NULL,
 [admsuc_codigo]           [CHAR](3) NOT NULL,
 [admmon_codigo]           [SMALLINT] NOT NULL,
 [precuo_numcuota]         [SMALLINT] NULL,
 [TipoCuota]               [VARCHAR](50) NULL,
 [precuo_salcap]           [DECIMAL](13, 2) NOT NULL,
 [precuo_salint]           [DECIMAL](13, 2) NOT NULL,
 [precuo_salcom]           [DECIMAL](13, 2) NOT NULL,
 [precuo_salgas]           [DECIMAL](13, 2) NOT NULL,
 [precuo_salmor]           [DECIMAL](13, 2) NULL,
 [cuecta_nombre]           [VARCHAR](400) NULL,
 [precuo_fecvenc]          [SMALLDATETIME] NOT NULL,
 [cuecta_formateado]       [CHAR](20) NOT NULL,
 [admsuc_nombre]           [CHAR](40) NULL,
 [admmon_nombre]           [CHAR](20) NULL,
 [prepre_saldo]            [DECIMAL](13, 2) NOT NULL,
 [precon_maxgramor]        [SMALLINT] NOT NULL,
 [prepre_indlegal]         [SMALLINT] NULL,
 [client_nombre]           [VARCHAR](200) NULL,
 [admrol_codigo]           [CHAR](3) NOT NULL,
 [admrel_codigo]           [INT] NOT NULL,
 [prepre_fecdesemb]        [SMALLDATETIME] NULL,
 [prepre_numdoc]           [CHAR](20) NULL,
 [admcia_codigo]           [CHAR](3) NOT NULL,
 [admsts_codigo]           [CHAR](2) NOT NULL,
 [client_codigo]           [INT] NOT NULL
);
DECLARE @RecaudoReferenciado AS TABLE
([NoReferencia]             [VARCHAR](40) NULL,
 [DescripciónReferencia]    [VARCHAR](20) NULL,
 [Direccion]                [VARCHAR](60) NULL,
 [Descripcion]              [VARCHAR](30) NULL,
 [FechaFactura]             [NVARCHAR](4000) NULL,
 [FechaLimitePago]          [NVARCHAR](4000) NULL,
 [FechaCorte]               [NVARCHAR](4000) NULL,
 [Moneda]                   [VARCHAR](3) NULL,
 [Monto]                    [VARCHAR](15) NULL,
 [Atraso]                   [VARCHAR](15) NULL,
 [Otros]                    [VARCHAR](15) NULL,
 [TotalPagar]               [VARCHAR](15) NULL,
 [PagoMinimo]               [VARCHAR](15) NULL,
 [NombreCompleto]           [VARCHAR](200) NULL,
 [Estado]                   [VARCHAR](1) NOT NULL,
 [IDServicio]               [VARCHAR](15) NULL,
 [CedulaRNC]                [VARCHAR](8000) NULL,
 [Email]                    [VARCHAR](80) NULL,
 [NCF]                      [VARCHAR](19) NULL,
 [ITBIS]                    [VARCHAR](15) NULL,
 [OtrosImpuestos]           [VARCHAR](15) NULL,
 [FechaFacturaNotFormat]    [SMALLDATETIME] NOT NULL,
 [FechaLimitePagoNotFormat] [SMALLDATETIME] NOT NULL,
 [FechaCorteNotFormat]      [SMALLDATETIME] NOT NULL,
 [MontoNotFormat]           [DECIMAL](17, 2) NULL,
 [TotalPagarNotFormat]      [DECIMAL](17, 2) NULL,
 [PagoMinimoNotFormat]      [DECIMAL](17, 2) NULL
);
DECLARE @CobrosAutomaticos AS TABLE
([NombreInscripto] [VARCHAR](8000) NULL,
 [RNC_Cedula]      [VARCHAR](8000) NULL,
 [MontoFormat]     [VARCHAR](8000) NULL,
 [Monto2]          [DECIMAL](38, 2) NULL,
 [Monto3]          [DECIMAL](38, 0) NULL,
 [IDServicio]      [VARCHAR](15) NULL,
 [TipoProducto]    [VARCHAR](2) NOT NULL,
 [NumeroProducto]  [VARCHAR](20) NULL,
 [Moneda]          [VARCHAR](3) NOT NULL,
 [TipoRegistro]    [VARCHAR](1) NOT NULL,
 [Banco]           [VARCHAR](3) NOT NULL,
 [Email]           [VARCHAR](13) NOT NULL,
 [Referencia]      [VARCHAR](13) NOT NULL,
 [NCF]             [VARCHAR](1) NOT NULL,
 [ITBIS]           [VARCHAR](1) NOT NULL,
 [OTROS]           [VARCHAR](1) NOT NULL
);
DECLARE @Notificaciones AS TABLE
([NumeroCuenta]   [VARCHAR](13) NOT NULL,
 [CodigoBanco]    [VARCHAR](3) NOT NULL,
 [TipoCuenta]     [VARCHAR](2) NOT NULL,
 [NombreCliente]  [VARCHAR](200) NULL,
 [Identificacion] [VARCHAR](256) NULL,
 [Descripcion]    [VARCHAR](14) NOT NULL
);
DECLARE @FileNotificaciones AS TABLE(LINE VARCHAR(MAX));
DECLARE @FileCobrosAutomaticos AS TABLE(LINE VARCHAR(MAX));
DECLARE @FileRecaudoReferenciado AS TABLE(LINE VARCHAR(MAX));


insert into  @clienteFormat 
SELECT NombreInscripto =CASE
                                WHEN RTRIM(LTRIM(dc.admrel_nombre))> 50 THEN RTRIM(LTRIM(dc.admrel_nombre))
                                ELSE RTRIM(LTRIM(dc.admrel_nombre))
                            END + ISNULL(replicate (' ', 50-LEN(CASE
                                                                    WHEN RTRIM(LTRIM(dc.admrel_nombre))> 50 THEN RTRIM(LTRIM(dc.admrel_nombre))
                                                                    ELSE RTRIM(LTRIM(dc.admrel_nombre))
                                                                END)), '') ,
                                  RNC_Cedula = [dbo].[udf_GetNumeric](RTRIM(LTRIM(dc.admide_numero))) + ISNULL(replicate ('0', 11-LEN([dbo].[udf_GetNumeric](RTRIM(LTRIM(dc.admide_numero))))), '') ,
                                  RNC_CedulaOnlyNumber = [dbo].[udf_GetNumeric](RTRIM(LTRIM(dc.admide_numero))) ,
                                  admrel_codigo ,
                                  admrel_nombre ,
                                  DescripciónReferencia=rtrim(ltrim(admide_numero)) ,
                                  Direccion=rtrim(ltrim(CASE
                                                            WHEN LEN(admciu_nombre) > 50 THEN SUBSTRING(admciu_nombre, 0, 49)
                                                            ELSE admciu_nombre
                                                        END)) ,
                                  Descripcion=CAST('' AS VARCHAR(30)) --     select  * from  [dbo].[xxClientesTodos] dc

   FROM EasyBank.[dbo].[xxClientesTodos] dc
   
   insert into @PagosFormat
    SELECT Monto= precuo_salcap+precuo_salint+precuo_salcom+precuo_salgas+precuo_salmor ,
           NoReferencia=cast(cuecta_numid AS varchar(10)) +cast(admsuc_codigo AS varchar(10))+cast(precuo_numcuota AS varchar(10))+cast(client_codigo AS varchar(10)) ,
           precuo_fecvencformat=FORMAT(precuo_fecvenc, 'ddMMyyyy', 'en-US') ,
		 precuo_fecvencnotformat=precuo_fecvenc,
           *
   FROM EasyBank.[dbo].[VW_CuotasPendientes] 
 
  
     insert into @RecaudoReferenciado 
  SELECT NoReferencia ,
           DescripciónReferencia=RTRIM(LTRIM(cuecta_formateado)) ,
           Direccion ,
           Descripcion=CAST('Pago cuota del Prestamo' AS VARCHAR(30)) ,
           FechaFactura=precuo_fecvencformat ,
           FechaLimitePago=precuo_fecvencformat ,
           FechaCorte=precuo_fecvencformat ,
           Moneda=CAST('000' AS VARCHAR(3)) ,
           Monto =CAST(CAST(FLOOR(Monto) AS VARCHAR(50)) + REPLACE(CAST(CAST(Monto-CAST(FLOOR(Monto) AS NUMERIC(18, 2)) AS numeric(18, 2)) AS VARCHAR(100)), '0.', '') AS VARCHAR(15)) ,
           Atraso =CAST('000' AS VARCHAR(15)) ,
           Otros =CAST('000' AS VARCHAR(15)) ,
           TotalPagar = CAST( --Monto
 --Otros
 --aTraso
 CAST(CAST(FLOOR(Monto) AS VARCHAR(50)) + REPLACE(CAST(CAST(Monto-CAST(FLOOR(Monto) AS NUMERIC(18, 2)) AS numeric(18, 2)) AS VARCHAR(100)), '0.', '') AS VARCHAR(15)) AS VARCHAR(15)) ,
           PagoMinimo =CAST(CAST(FLOOR(Monto) AS VARCHAR(50)) + REPLACE(CAST(CAST(Monto-CAST(FLOOR(Monto) AS NUMERIC(18, 2)) AS numeric(18, 2)) AS VARCHAR(100)), '0.', '') AS VARCHAR(15)) ,
           NombreCompleto=CASE
                              WHEN len(RTRIM(LTRIM(dc.admrel_nombre)))> 100 THEN substring(RTRIM(LTRIM(dc.admrel_nombre)), 0, 99)
                              ELSE RTRIM(LTRIM(dc.admrel_nombre))
                          END,
                          Estado=CASE
                                     WHEN ((row_number() over(
                                                              ORDER BY NombreInscripto)) % 2) =0 THEN 'C'
                                     ELSE 'I'
                                 END ,
                                 IDServicio=@IDServicio ,
                                 CedulaRNC=REPLACE(REPLACE(RNC_CedulaOnlyNumber, '0', '1'), '1', '9') ,
                                 Email=CAST('test@test.com' AS VARCHAR(80)) ,
                                 NCF=CAST('-' AS VARCHAR(19)) ,
                                 ITBIS =CAST('000' AS VARCHAR(15)) ,
                                 OtrosImpuestos =CAST('000' AS VARCHAR(15)),
						    FechaFacturaNotFormat=precuo_fecvencnotformat ,
						    FechaLimitePagoNotFormat=precuo_fecvencnotformat ,
						    FechaCorteNotFormat=precuo_fecvencnotformat ,
						    MontoNotFormat =Monto ,
						      TotalPagarNotFormat = Monto ,
							 PagoMinimoNotFormat=Monto
   FROM @PagosFormat cp
   LEFT JOIN @clienteFormat dc ON cp.client_codigo = dc.admrel_codigo 
   
    insert into  @CobrosAutomaticos 
   SELECT NombreInscripto ,
           RNC_Cedula,
           MontoFormat=CAST(FLOOR(SUM(Monto)) AS VARCHAR(50)) + REPLACE(CAST(CAST(SUM(Monto)-CAST(FLOOR(SUM(Monto)) AS NUMERIC(18, 2)) AS numeric(18, 2)) AS VARCHAR(100)), '0.', '') ,
           Monto2=SUM(Monto),
           Monto3=FLOOR(SUM(Monto)) ,
           IDServicio=@IDServicio ,
           TipoProducto ='CC' ,
           NumeroProducto ='0154503001' + cast(row_number() over(
                                                                 ORDER BY NombreInscripto) AS varchar(10)) ,
           Moneda ='000' /*000,034*/ ,
           TipoRegistro=CASE
                            WHEN cast(row_number() over(
                                                        ORDER BY NombreInscripto) AS varchar(10)) ='2' THEN 'C'
                            ELSE 'I'
                        END ,
                        Banco='BHD' ,
                        Email='test@test.com' ,
                        Referencia='NO REFERENCIA' ,
                        NCF='-' ,
                        ITBIS='0' ,
                        OTROS='0'
   FROM @PagosFormat cp
   LEFT JOIN @clienteFormat dc ON cp.client_codigo = dc.admrel_codigo
   GROUP BY NombreInscripto,
            RNC_Cedula 

   insert into  @Notificaciones 
   SELECT DISTINCT NumeroCuenta = '0154503-001-0' ,
                    CodigoBanco='BHD' ,
                    TipoCuenta='CC' ,
                    NombreCliente=Rtrim(ltrim(admrel_nombre)) ,
                    Identificacion=RNC_CedulaOnlyNumber ,
                    Descripcion ='Prueba Kredisi'
   FROM @clienteFormat dc
   WHERE EXISTS
       ( SELECT TOP 1 1
        FROM @PagosFormat cp
        WHERE cp.client_codigo = dc.admrel_codigo ) 
	   
	 
	insert into @FileNotificaciones 
   SELECT NumeroCuenta+';' +CodigoBanco+';' +TipoCuenta+';' +NombreCliente+';' +REPLACE(REPLACE(Identificacion, '0', '1'), '1', '9')+';' +Descripcion AS LINE
   FROM @Notificaciones 

     insert into @FileCobrosAutomaticos 
   SELECT IDServicio+';' +NombreInscripto+';' +REPLACE(RNC_Cedula, '1', '9')+';' +TipoProducto+';' +NumeroProducto+';' +Moneda+';' +ISNULL(replicate ('0', 17-LEN(RTRIM(LTRIM(MontoFormat)))), '')+ RTRIM(LTRIM(MontoFormat))+';' +TipoRegistro+';' +Banco+';' +Email+';' +Referencia+';' +NCF+';' +ITBIS+';' +OTROS+';' AS LINE
   FROM @CobrosAutomaticos
     insert into @FileRecaudoReferenciado 
   SELECT NoReferencia+';' +DescripciónReferencia+';' +Direccion+';' +Descripcion+';' +FechaFactura+';' +FechaLimitePago+';' +FechaCorte+';' +Moneda+';' +Monto+';' +Atraso+';' +Otros+';' +TotalPagar+';' +PagoMinimo+';' +NombreCompleto+';' +Estado+';' +IDServicio+';' +CedulaRNC+';' +Email+';' +NCF+';' +ITBIS+';' +OtrosImpuestos AS LINE
   FROM @RecaudoReferenciado


   IF (@InsertInTables =1)

   BEGIN 
      
   INSERT INTO Recaudo.Archivo (TipoArchivoID, Fecha,ArchivoName,EstatusID) VALUES (1,GETDATE(), 'FACLOT_'+FORMAT(GETDATE(), 'yyyyMMdd', 'en-US')+'.txt',1)
   INSERT INTO [Recaudo].[RecReferenciado]
           ([NoReferencia]
           ,[DescripcionReferencia]
           ,[Direccion]
           ,[Descripción]
           ,[FechaFactura]
           ,[FechaLimitePago]
           ,[FechaCorte]
           ,[Moneda]
           ,[Monto]
           ,[Atraso]
           ,[Otros]
           ,[TotalPagar]
           ,[PagoMinimo]
           ,[NombreCompleto]
           ,[Estado]
           ,[IDServicio]
           ,[RNCCedula]
           ,[Email]
           ,[NCF]
           ,[ITBIS]
           ,[OtrosImpuestos]
           ,[ArchivoID]
           ,[EstatusID]
           ,[CreatedDate])
    select [NoReferencia]
           ,[DescripciónReferencia]
           ,[Direccion]
           ,[Descripcion]
           ,[FechaFacturaNotFormat]
           ,[FechaLimitePagoNotFormat]
           ,[FechaCorteNotFormat]
           ,[Moneda]
           ,[MontoNotFormat]
           ,cast ([Atraso] as decimal(13,2))
           ,cast ([Otros]as decimal(13,2))
           ,[TotalPagarNotFormat]
           ,[PagoMinimoNotFormat]
           ,[NombreCompleto]
           ,[Estado]
           ,[IDServicio]
           ,CedulaRNC
           ,[Email]
           ,[NCF]
           ,cast ([ITBIS] as decimal(13,2))
           ,cast( [OtrosImpuestos] as decimal(13,2))
           ,scope_identity()
           ,1
           ,getdate()[CreatedDate]
		 from  @RecaudoReferenciado;

		 -- INSERT INTO Recaudo.Archivo (Fecha,ArchivoName,Estatus) VALUES (GETDATE(), 'FACLOT_'+FORMAT(GETDATE(), 'yyyyMMdd', 'en-US')+'.txt',1)

		 

   END
  SELECT *  FROM @FileRecaudoReferenciado --WHERE 1=10

 --select * from tmpFileRecaudoReferenciado




--[Recaudo].[GenerateFiles] 1
GO
/****** Object:  StoredProcedure [Recaudo].[GetCardNetDetail]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROC [Recaudo].[GetCardNetDetail] 
(@searchby int ,  @searchtext varchar(100),  @estatus int , @archivoid int )
AS

DECLARE @TotalPagar as DECIMAL(13,2) =NULL
SET @TotalPagar  = iif(@searchby = 4, try_Cast (replace (ltrim(rtrim(@searchtext)),',','') as decimal (13,2)), null)
SET @searchtext  = upper(replace (ltrim(rtrim(@searchtext)) ,char(9),''))

SELECT 
 ArchivoID
 ,cast (NoReferencia as varchar(15)) NoReferencia
,TipoRegistro TipoRegistro
,'-' NombreCliente
,'-' RNCCedula
,cast (NoCuenta as varchar(15)) NoCuenta
,TotalPagar
, MontoFactura
,Canal
,Fecha
,CAST(ReciboEasyBank AS varchar(100))ReciboEasyBank
,MontoEasyBank
,TotalAplicado
,Estatus
,EstatusID
,PagoReverso
,cuecta_formateado
, MontoPendienteCuenta
,[PagoID]
,AccountNumberF
FROM [Recaudo].[VCardNetBatchDetail]
WHERE 
ArchivoID = @archivoid  and
EstatusID = IIF ( @estatus= -2, EstatusID, @estatus) and 
--upper (ltrim(rtrim(NombreCliente))) like iif (@searchby = 1,'%'+ @searchtext+'%' ,upper (ltrim(rtrim(NombreCliente)))) and 
---upper (ltrim(rtrim(RNCCedula))) like iif (@searchby = 2,'%'+ @searchtext+'%' ,upper (ltrim(rtrim(RNCCedula)))) and 
upper (ltrim(rtrim(NoReferencia))) like iif (@searchby = 3,'%'+ @searchtext+'%' ,upper (ltrim(rtrim(NoReferencia)))) and 
TotalPagar = iif (@searchby = 4,@TotalPagar ,TotalPagar)  
ORDER BY CASE WHEN EstatusID = 00 THEN 99 ELSE EstatusID END DESC
GO
/****** Object:  StoredProcedure [Recaudo].[GetRecargoReferenciadoDetail]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE         PROC [Recaudo].[GetRecargoReferenciadoDetail] 
(@searchby int ,  @searchtext varchar(100),  @estatus int , @archivoid int )
AS

DECLARE @TotalPagar as DECIMAL(13,2) =NULL
SET @TotalPagar  = iif(@searchby = 4, try_Cast (replace (ltrim(rtrim(@searchtext)),',','') as decimal (13,2)), null)
SET @searchtext  = upper(replace (ltrim(rtrim(@searchtext)) ,char(9),''))

SELECT 
 ArchivoID
,NoReferencia
,TipoRegistro
,NombreCliente
,RNCCedula
,NoCuenta
,TotalPagar
--,MontoFactura
,case when NoCuenta=314 and ArchivoID = 75 then 1.00 else MontoFactura end   MontoFactura
,Canal
,Fecha
,CAST(ReciboEasyBank AS varchar(100))ReciboEasyBank
,MontoEasyBank
,TotalAplicado
,Estatus
,EstatusID
,PagoReverso
,cuecta_formateado
,case when NoCuenta=314 and ArchivoID = 75 then 1.00 else MontoPendienteCuenta end   MontoPendienteCuenta
,[PagoID]
FROM


 [Recaudo].[RecargoReferenciadoDetail]
 
WHERE 

ArchivoID = @archivoid  and
EstatusID = IIF ( @estatus= 0, EstatusID, @estatus) and 
upper (ltrim(rtrim(NombreCliente))) like iif (@searchby = 1,'%'+ @searchtext+'%' ,upper (ltrim(rtrim(NombreCliente)))) and 
upper (ltrim(rtrim(RNCCedula))) like iif (@searchby = 2,'%'+ @searchtext+'%' ,upper (ltrim(rtrim(RNCCedula)))) and 
upper (ltrim(rtrim(NoReferencia))) like iif (@searchby = 3,'%'+ @searchtext+'%' ,upper (ltrim(rtrim(NoReferencia)))) and 
TotalPagar = iif (@searchby = 4,@TotalPagar ,TotalPagar)  

ORDER BY CASE WHEN EstatusID = 8 THEN 99 ELSE EstatusID END DESC

--select * from  [Recaudo].[Estatus]


--SELECT * FROM [Recaudo].[VW_PagoPendienteAplicarEasybank]
GO
/****** Object:  StoredProcedure [Recaudo].[InsertConciliacion]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Recaudo].[InsertConciliacion] 
@udt_Conciliacion  Recaudo.udt_Conciliacion READONLY
AS 
INSERT INTO [Recaudo].[RecConciliacion]
           ([Proveedor]
           ,[Servicio]
           ,[TipoServicio]
           ,[Canal]
           ,[NoReferencia]
           ,[NombreCliente]
           ,[MontoFactura]
           ,[MontoPagoEfectivo]
           ,[MontoDebitoCuenta]
           ,[MontoPagoCheque]
           ,[Fecha]
           ,[Hora]
           ,[PagoReverso]
           ,[Moneda]
           ,[NumeroTransaccion]
           ,[Sucursal]
		 ,[ArchivoID]
           ,[CreatedDate]
           ,[ModifiedDate])
SELECT 
		  [Proveedor]
           ,[Servicio]
           ,[TipoServicio]
           ,[Canal]
           ,[NoReferencia]
           ,[NombreCliente]
           ,[MontoFactura]
           ,[MontoPagoEfectivo]
           ,[MontoDebitoCuenta]
           ,[MontoPagoCheque]
           ,[Fecha]
           ,[Hora]
           ,[PagoReverso]
           ,[Moneda]
           ,[NumeroTransaccion]
           ,[Sucursal]
		 ,[ArchivoID]
           ,[CreatedDate]
           ,[ModifiedDate]
FROM		  @udt_Conciliacion

									   --ACTUALIZA LOS REGISTROS SIN PAGO
UPDATE rr
  SET
      rr.EstatusID = 7,--SIN PAGO
      [ModifiedDate] = GETDATE()
FROM Recaudo.RecReferenciado rr
     LEFT JOIN @udt_Conciliacion c ON c.NoReferencia = rr.NoReferencia and c.ArchivoID = rr.ArchivoID
WHERE c.NoReferencia IS NULL;
									   --ACTUALIZA LOS REGISTROS CON PAGO
UPDATE rr
  SET
      rr.EstatusID = 8,--PENDIENTE DE PAGO EASYBANK
      [ModifiedDate] = GETDATE()
FROM Recaudo.RecReferenciado rr
     INNER JOIN @udt_Conciliacion c ON c.NoReferencia = rr.NoReferencia and c.ArchivoID = rr.ArchivoID
WHERE C.PagoReverso ='PAGO';
									   --ACTUALIZA LOS REGISTROS CON REVERSO
UPDATE rr
  SET
      rr.EstatusID = 9,--PENDIENTE DE REVERSO PAGO EASYBANK
      [ModifiedDate] = GETDATE()
FROM Recaudo.RecReferenciado rr
     INNER JOIN @udt_Conciliacion c ON c.NoReferencia = rr.NoReferencia and c.ArchivoID = rr.ArchivoID
WHERE C.PagoReverso ='REVERSO';
									   --INSERTA LOS REGISTROS CON PAGOS O REVERSOS EN LA TABLA DE [Recaudo].[Pago] PARA LEUGO LLAMAR EL SERVICIO DE EASY BANK Y APLICAR LOS PAGOS EN EL CORE.

INSERT INTO [Recaudo].[Pago]
           ([IDReferecia]
           ,[IDConciliacion]
           ,[MontoPagado]
           ,[MontoAplicadoEasyBank]
           ,[ReciboEasyBank]
           ,[CreatedDate]
           ,[ModifiedDate]
		   ,ArchivoId
		   )
select

		  RecaudoID [IDReferecia]
           ,ConciliacionID [IDConciliacion]
           ,COALESCE([dbo].IsNullOrZero(c.MontoPagoEfectivo),[dbo].IsNullOrZero(c.MontoDebitoCuenta), [dbo].IsNullOrZero(c.MontoPagoEfectivo)) [MontoPagado]
           ,null [MontoAplicadoEasyBank]
           ,null [ReciboEasyBank]
           ,GETDATE() [CreatedDate]
           ,null [ModifiedDate]
		   ,c.ArchivoId
 FROM Recaudo.RecReferenciado rr
 INNER JOIN [Recaudo].[RecConciliacion] c ON c.NoReferencia = rr.NoReferencia and c.ArchivoID = rr.ArchivoID
 INNER JOIN @udt_Conciliacion uc ON c.NoReferencia = uc .NoReferencia and c.ArchivoID = uc.ArchivoID
 WHERE NOT EXISTS (SELECT TOP 1 1  FROM Recaudo.Pago p where p.[IDReferecia] = RecaudoID and p.IDConciliacion=ConciliacionID)

GO
/****** Object:  StoredProcedure [Recaudo].[InsertConciliacionCardNet]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [Recaudo].[InsertConciliacionCardNet] 
@udt_Conciliacion  [Recaudo].[udt_ConciliacionCardNet]  READONLY
AS 

INSERT INTO [Recaudo].[CardNetConciliacion]
           ([CardNetConciliacionId]
           ,[ArchivoId]
           ,[RespuestaId]
           ,[RecordType]
           ,[TranSequenceNumber]
           ,[TransType]
           ,[AccountNumber]
           ,[AccountNumberLast4]
           ,[TerminalNumber]
           ,[TransactionAmount]
           ,[AmountSettlement]
           ,[ExpirationDate]
           ,[Reserved]
           ,[ReferenceNumber]
           ,[Reserved2]
           ,[AuthorizationCode]
           ,[ReferenceNumber2]
           ,[CreateDate]
           ,[ModiDate]
           ,[CreateUsrId]
           ,[ModiUsrId]
           ,[hostName])
     


SELECT 
		   row_number()over(order by [ArchivoId]  )
           ,[ArchivoId]
           ,isnull(R.RespuestaId ,-1)
           ,[RecordType]
           ,CAST ([TranSequenceNumber] AS INT)
           ,[TransType]
           ,[AccountNumber]
           ,SUBSTRING ( [AccountNumber],0,4)
           ,[TerminalNumber]
           ,cast (REPLACE ([TransactionAmount],' ','') AS NUMERIC(18,2))
           ,cast (REPLACE ([AmountSettlement],' ','') AS NUMERIC(18,2))
           ,[ExpirationDate]
           ,[Reserved]
           ,[ReferenceNumber]
           ,[Reserved2]
           ,[AuthorizationCode]
           ,[ReferenceNumber2]
           ,getdate()--[CreateDate]
           ,null [ModiDate]
           ,[UserID] [CreateUsrId]
           ,null [ModiUsrId]
           ,host_name() [hostName]
FROM		  @udt_Conciliacion C
LEFT JOIN [Recaudo].[CardNetRespuesta] R ON R.RespuestaCodigo=C.[ResponseCode] 



    UPDATE rr
    SET
      rr.EstatusID = 7,--SIN PAGO
      rr.ModiDate = GETDATE()
    FROM [Recaudo].[CardNetBatchDetail]  rr
    LEFT JOIN (SELECT ReferenceNumber,ArchivoID FROM @udt_Conciliacion c 
									   left JOIN [Recaudo].[CardNetRespuesta] R 
									    ON C.[ResponseCode] =r.RespuestaCodigo
     WHERE r.RespuestaCodigo !='00' ) con    ON ltrim(rtrim(con.ReferenceNumber)) =ltrim(rtrim( rr.ReferenceNumber))     
	WHERE  con.ArchivoID = rr.ArchivoID

	
    UPDATE rr
    SET
      rr.EstatusID = 8,--PENDIENTE DE PAGO EASYBANK
      rr.ModiDate = GETDATE()
    FROM [Recaudo].[CardNetBatchDetail]  rr
    INNER JOIN 
	(
		SELECT ReferenceNumber,ArchivoID FROM @udt_Conciliacion c 
									   INNER JOIN [Recaudo].[CardNetRespuesta] R 
									     ON C.[ResponseCode] =r.RespuestaCodigo
		WHERE r.RespuestaCodigo ='00' 
	 ) 
		con    ON ltrim(rtrim(con.ReferenceNumber)) =ltrim(rtrim( rr.ReferenceNumber))      and  con.ArchivoID = rr.ArchivoID
	



INSERT INTO [Recaudo].[Pago]
           ([IDReferecia]
           ,[IDConciliacion]
           ,[MontoPagado]
           ,[MontoAplicadoEasyBank]
           ,[ReciboEasyBank]
           ,[CreatedDate]
           ,[ModifiedDate]
		   ,ArchivoId
		   )
  	SELECT

		  DetailID [IDReferecia]
           ,CardNetConciliacionId [IDConciliacion]
           ,isnull(rr.TransactionAmount,0) [MontoPagado]
           ,null [MontoAplicadoEasyBank]
           ,null [ReciboEasyBank]
           ,GETDATE() [CreatedDate]
           ,null [ModifiedDate]
		 ,rr.ArchivoId
 FROM [Recaudo].[CardNetBatchDetail] rr
 INNER JOIN  [Recaudo].[CardNetConciliacion]    c ON ltrim(rtrim(c.ReferenceNumber)) = ltrim(rtrim(rr.ReferenceNumber))
 AND c.ArchivoID = rr.ArchivoID
 INNER JOIN (SELECT ReferenceNumber,ReferenceNumber2,ArchivoID FROM @udt_Conciliacion c 
									   INNER JOIN [Recaudo].[CardNetRespuesta] R 
									   ON C.[ResponseCode] =r.RespuestaCodigo
     WHERE r.RespuestaCodigo ='00' )  uc ON ltrim(rtrim(uc.ReferenceNumber)) = ltrim(rtrim(c.ReferenceNumber)) 	AND ltrim(rtrim(uc.ReferenceNumber2)) = ltrim(rtrim(c.ReferenceNumber2)) AND uc.ArchivoID = c.ArchivoID
 WHERE NOT EXISTS 
		(
			SELECT TOP 1 1  
			FROM Recaudo.Pago p 
			where 
					p.[IDReferecia] = DetailID 
				and p.IDConciliacion=CardNetConciliacionId
		) --and c.RespuestaCodigo=0;

    UPDATE a SET a.estatusid = 5
    FROM [Recaudo].Archivo a 
    INNER JOIN @udt_Conciliacion r on r.archivoid =a.archivoid
    where a.TipoArchivoID=2    
   -- and r.[ResponseCode] ='00'

GO
/****** Object:  StoredProcedure [SMS].[SP_SET_SMSQUEUE_BY_TABLE]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name:SP_SET_SMSQUEUE_BY_TABLE
** Desc:This is use for insert data into table SMSQueue in bulk insert 
** Auth:RALVAREZ
** Date:06/27/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    06/27/2018  Ralvarez				This is use for insert data into table SMSQueue in bulk insert 
**********************************************************************************************************************************************************/


CREATE PROCEDURE [SMS].[SP_SET_SMSQUEUE_BY_TABLE]
	@SMSQueueData [SMS].[DTT_SMSQueue] READONLY 
AS 



INSERT INTO [SMS].[SMSQueue]
(
	 SMSSourceID
	,SMSStatusID
	,SMSTypeID
	,ReferenceKey
	,ClientName
	,PhoneArea
	,PhoneNumber
	,SMS
	,ProcessedDate
	,ErrorMessage
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,Hostname
)

SELECT 
	 SMSSourceID
	,SMSStatusID
	,SMSTypeID
	,ReferenceKey
	,ClientName
	,PhoneArea
	,[dbo].[UDF_GETNUMERIC](PhoneNumber)
	,SMS
	,ProcessedDate
	,ErrorMessage
	,CreateDate
	,ModiDate
	,CreateUsrId
	,ModiUsrId
	,Hostname
FROM @SMSQueueData



UPDATE [SMS].[SMSQueue] SET 
	 SMSStatusID = 3--ERROR
	,ErrorMessage='Phone is bad'
	,ProcessedDate=GETDATE()
	,ModiDate=GETDATE()
	,ModiUsrId=1
WHERE SMSStatusID = 1 AND  RTRIM(LTRIM(ISNULL(PhoneNumber,''))) =''


UPDATE [SMS].[SMSQueue] SET 
	 SMSStatusID = 3--ERROR
	,ErrorMessage='Message is blank'
	,ProcessedDate=GETDATE()
	,ModiDate=GETDATE()
	,ModiUsrId=1
WHERE SMSStatusID = 1 AND  
(
	RTRIM(LTRIM(ISNULL(SMS,''))) ='' 
)

UPDATE [SMS].[SMSQueue] SET 
	 SMSStatusID = 3--ERROR
	,ErrorMessage='Message length is too large'
	,ProcessedDate=GETDATE()
	,ModiDate=GETDATE()
	,ModiUsrId=1
WHERE SMSStatusID = 1 AND  
(
	len(RTRIM(LTRIM(ISNULL(SMS,''))))> 160
)
GO
/****** Object:  Table [dbo].[eadmgasr]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[eadmgasr](
	[admgas_numero] [int] NOT NULL,
	[cuecta_numid] [int] NOT NULL,
	[admgas_monto] [numeric](18, 0) NULL,
	[admgas_saldo] [numeric](18, 0) NULL,
	[admgas_porciento] [numeric](18, 0) NULL,
 CONSTRAINT [PK_TEST_TRIGGER] PRIMARY KEY CLUSTERED 
(
	[admgas_numero] ASC,
	[cuecta_numid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tmpFileRecaudoReferenciado]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tmpFileRecaudoReferenciado](
	[LINE] [varchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Entity].[businessType]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Entity].[businessType](
	[businessTypeId] [int] NOT NULL,
	[businessTypeName] [varchar](150) NULL,
	[businessTypeCode] [varchar](50) NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_businessType] PRIMARY KEY CLUSTERED 
(
	[businessTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Entity].[client]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Entity].[client](
	[clientId] [bigint] NOT NULL,
	[relatedContactId] [bigint] NULL,
	[companyId] [int] NOT NULL,
	[linkedTypeId] [int] NULL,
	[clientTypeByCompanyId] [int] NULL,
	[reasonId] [int] NULL,
	[clientTypeBySIBId] [int] NULL,
	[clientName] [varchar](150) NULL,
	[admissionDate] [datetime] NULL,
	[creditInformation] [bit] NULL,
	[ClientReference] [varchar](200) NULL,
	[InactiveComment] [varchar](500) NULL,
	[numberDependents] [int] NULL,
	[numberEmployees] [int] NULL,
	[AnnualSales] [numeric](18, 2) NULL,
	[AssetValue] [numeric](18, 2) NULL,
	[dateIncorporation] [date] NULL,
	[ownHouse] [bit] NULL,
	[IsClientSentCollection] [bit] NULL,
	[depositType] [varchar](1) NULL,
	[TypeNCF] [varchar](5) NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_client] PRIMARY KEY CLUSTERED 
(
	[clientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Entity].[clientTypeByCompany]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Entity].[clientTypeByCompany](
	[clientTypeByCompanyId] [int] NOT NULL,
	[clientTypeByCompanyName] [varchar](150) NULL,
	[clientTypeByCompanyCode] [varchar](50) NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_clientTypeByCompany] PRIMARY KEY CLUSTERED 
(
	[clientTypeByCompanyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Entity].[clientTypeBySIB]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Entity].[clientTypeBySIB](
	[clientTypeBySIBId] [int] NOT NULL,
	[EntityClassId] [int] NULL,
	[clientTypeBySIBName] [varchar](150) NULL,
	[clientTypeBySIBCode] [varchar](50) NULL,
	[Isfinancial] [bit] NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_clientTypeBySIB] PRIMARY KEY CLUSTERED 
(
	[clientTypeBySIBId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Entity].[collateral]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Entity].[collateral](
	[clientId] [bigint] NOT NULL,
	[collateralId] [bigint] NOT NULL,
	[companyId] [int] NULL,
	[currencyId] [int] NULL,
	[collateralTypeSIBId] [int] NULL,
	[collateralTypeId] [int] NULL,
	[collateralName] [varchar](250) NULL,
	[date] [date] NULL,
	[percent] [numeric](18, 3) NULL,
	[amount] [numeric](18, 3) NULL,
	[IsInspection] [bit] NULL,
	[amountToUse] [numeric](18, 3) NULL,
	[inspectionFrequency] [int] NULL,
	[inspectionFrequencyDesc] [varchar](250) NULL,
	[percentUsed] [numeric](18, 3) NULL,
	[amountUsed] [numeric](18, 3) NULL,
	[reference] [varchar](150) NULL,
	[formalizationDate] [date] NULL,
	[comment] [varchar](5000) NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_collateral] PRIMARY KEY CLUSTERED 
(
	[clientId] ASC,
	[collateralId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Entity].[Company]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Entity].[Company](
	[companyId] [int] NOT NULL,
	[currencyId] [int] NULL,
	[relatedContactId] [bigint] NULL,
	[companyCode] [varchar](50) NULL,
	[companyName] [varchar](150) NULL,
	[rnc] [varchar](50) NULL,
	[companyHoldingId] [int] NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[companyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Entity].[contactAddress]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Entity].[contactAddress](
	[addressId] [bigint] NOT NULL,
	[relatedContactId] [bigint] NOT NULL,
	[CountryID] [int] NOT NULL,
	[provinceId] [int] NOT NULL,
	[cityId] [int] NOT NULL,
	[physicalSectorId] [int] NULL,
	[addressType] [varchar](50) NULL,
	[streetName] [varchar](250) NULL,
	[streetNumber] [varchar](50) NULL,
	[buildingName] [varchar](250) NULL,
	[postalCode] [varchar](50) NULL,
	[postalZone] [varchar](50) NULL,
	[address] [varchar](500) NULL,
	[addressAdditional] [varchar](500) NULL,
	[IsPrimary] [bit] NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_address] PRIMARY KEY CLUSTERED 
(
	[addressId] ASC,
	[relatedContactId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Entity].[contactEmail]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Entity].[contactEmail](
	[contactEmailId] [bigint] NOT NULL,
	[relatedContactId] [bigint] NOT NULL,
	[email] [varchar](2500) NULL,
	[emailType] [varchar](50) NULL,
	[comments] [varchar](2500) NULL,
	[IsPrimary] [bit] NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_contactEmail] PRIMARY KEY CLUSTERED 
(
	[contactEmailId] ASC,
	[relatedContactId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Entity].[contactPhones]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Entity].[contactPhones](
	[contactPhoneId] [bigint] NOT NULL,
	[relatedContactId] [bigint] NOT NULL,
	[CountryID] [int] NULL,
	[AreaCode] [varchar](10) NULL,
	[Phone] [varchar](50) NULL,
	[PhoneType] [varchar](5) NULL,
	[Comments] [varchar](500) NULL,
	[IsPrimary] [bit] NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_contactPhones] PRIMARY KEY CLUSTERED 
(
	[contactPhoneId] ASC,
	[relatedContactId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Entity].[EntityClass]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Entity].[EntityClass](
	[EntityClassId] [int] NOT NULL,
	[ClassName] [varchar](150) NULL,
	[ClassCode] [varchar](50) NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_EntityClass] PRIMARY KEY CLUSTERED 
(
	[EntityClassId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Entity].[entityRol]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Entity].[entityRol](
	[entityRolId] [int] IDENTITY(1,1) NOT NULL,
	[rolName] [varchar](150) NULL,
	[rolAbbreviation] [varchar](50) NULL,
	[ISBlock] [bit] NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_entityRol] PRIMARY KEY CLUSTERED 
(
	[entityRolId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Entity].[identificationType]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Entity].[identificationType](
	[identificationTypeId] [int] NOT NULL,
	[IdentificationName] [varchar](150) NULL,
	[IdentificationCode] [varchar](150) NULL,
	[PersonType] [varchar](50) NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostnName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_identificationType] PRIMARY KEY CLUSTERED 
(
	[identificationTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Entity].[lineCredit]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Entity].[lineCredit](
	[lineCreditId] [bigint] NOT NULL,
	[companyId] [int] NULL,
	[currencyId] [int] NULL,
	[lineCreditType] [int] NULL,
	[referenceId] [int] NULL,
	[clientId] [bigint] NULL,
	[Amount] [numeric](18, 3) NULL,
	[expirationDate] [date] NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
 CONSTRAINT [PK_lineCredit] PRIMARY KEY CLUSTERED 
(
	[lineCreditId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Entity].[linkedType]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Entity].[linkedType](
	[linkedTypeId] [int] NOT NULL,
	[linkedTypeName] [varchar](150) NULL,
	[linkedTypeCode] [varchar](50) NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_linkedType] PRIMARY KEY CLUSTERED 
(
	[linkedTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Entity].[MaritalStatus]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Entity].[MaritalStatus](
	[MaritalStatusId] [int] NOT NULL,
	[MaritalStatusName] [varchar](150) NULL,
	[MaritalStatusCode] [varchar](50) NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostname] [varchar](100) NOT NULL,
 CONSTRAINT [PK_MaritalStatu] PRIMARY KEY CLUSTERED 
(
	[MaritalStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Entity].[office]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Entity].[office](
	[officeId] [int] NOT NULL,
	[companyId] [int] NULL,
	[relatedContactId] [bigint] NULL,
	[officeName] [varchar](150) NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_office] PRIMARY KEY CLUSTERED 
(
	[officeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Entity].[personType]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Entity].[personType](
	[personTypeId] [int] NOT NULL,
	[personTypeName] [varchar](150) NULL,
	[personTypeCode] [varchar](50) NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_personType] PRIMARY KEY CLUSTERED 
(
	[personTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Entity].[Profession]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Entity].[Profession](
	[ProfessionId] [int] NOT NULL,
	[ProfessionName] [varchar](150) NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Profession] PRIMARY KEY CLUSTERED 
(
	[ProfessionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Entity].[relatedContact]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Entity].[relatedContact](
	[relatedContactId] [bigint] NOT NULL,
	[countryID] [int] NULL,
	[entityRolId] [int] NULL,
	[identificationTypeId] [int] NULL,
	[entityClassId] [int] NULL,
	[personTypeId] [int] NULL,
	[maritalStatusId] [int] NULL,
	[professionId] [int] NULL,
	[Sex] [varchar](5) NULL,
	[FirstName] [varchar](150) NULL,
	[MiddleName] [varchar](150) NULL,
	[Lastname] [varchar](150) NULL,
	[SecondLastname] [varchar](150) NULL,
	[FullName] [varchar](250) NULL,
	[Nickname] [varchar](150) NULL,
	[MarriedName] [varchar](150) NULL,
	[Title] [varchar](10) NULL,
	[IdentificationNumber] [varchar](60) NULL,
	[BirthDate] [date] NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_relatedContact] PRIMARY KEY CLUSTERED 
(
	[relatedContactId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [global].[accountType]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [global].[accountType](
	[accountTypeID] [int] NOT NULL,
	[accountTypeName] [varchar](150) NULL,
	[accountTypeCode] [varchar](50) NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_accountType] PRIMARY KEY CLUSTERED 
(
	[accountTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [global].[businessLine]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [global].[businessLine](
	[businessLineId] [int] NOT NULL,
	[businessLineName] [varchar](150) NULL,
	[businessLineCode] [varchar](50) NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_businessLine] PRIMARY KEY CLUSTERED 
(
	[businessLineId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [global].[CardType]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [global].[CardType](
	[CardTypeId] [int] NOT NULL,
	[CardTypeDesc] [varchar](60) NULL,
	[ISCredit] [bit] NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_CardType] PRIMARY KEY CLUSTERED 
(
	[CardTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [global].[city]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [global].[city](
	[CountryID] [int] NOT NULL,
	[provinceId] [int] NOT NULL,
	[cityId] [int] NOT NULL,
	[cityName] [varchar](250) NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_city] PRIMARY KEY CLUSTERED 
(
	[CountryID] ASC,
	[provinceId] ASC,
	[cityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [global].[ConceptType]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [global].[ConceptType](
	[companyId] [int] NOT NULL,
	[ConceptTypeId] [int] NOT NULL,
	[currencyId] [int] NULL,
	[accountTypeID] [int] NULL,
	[ConceptTypeName] [varchar](250) NULL,
	[Yearlength] [int] NULL,
	[InterestRateMinimun] [numeric](18, 3) NULL,
	[InterestRateMaximun] [numeric](18, 3) NULL,
	[InterestRateType] [varchar](50) NULL,
	[LineBusiness] [int] NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_ConceptType] PRIMARY KEY CLUSTERED 
(
	[companyId] ASC,
	[ConceptTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [global].[ContactsDomiciliationCards]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [global].[ContactsDomiciliationCards](
	[clientId] [bigint] NOT NULL,
	[CardTypeId] [int] NOT NULL,
	[LastFourDigits] [varchar](4) NOT NULL,
	[CardNumber] [varbinary](8000) NULL,
	[ExpirationDate] [date] NULL,
	[CVV2] [varchar](50) NULL,
	[CardHolder] [varchar](200) NULL,
	[ExpirationDateMMYYYY] [varchar](10) NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_ContactsDomiciliationCards] PRIMARY KEY CLUSTERED 
(
	[clientId] ASC,
	[CardTypeId] ASC,
	[LastFourDigits] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [global].[Country]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [global].[Country](
	[CountryID] [int] NOT NULL,
	[currencyId] [int] NULL,
	[CountryName] [varchar](150) NULL,
	[CountryAbbreviation] [varchar](50) NULL,
	[PhoneAccessCodes] [varchar](500) NULL,
	[Demonym] [varchar](150) NULL,
	[phoneMask] [varchar](50) NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[CountryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [global].[Currency]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [global].[Currency](
	[currencyId] [int] NOT NULL,
	[currencyName] [varchar](150) NULL,
	[currencyAbbreviation] [varchar](50) NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Currency] PRIMARY KEY CLUSTERED 
(
	[currencyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [global].[destinationFund]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [global].[destinationFund](
	[destinationFundId] [int] NOT NULL,
	[destinationFundName] [varchar](250) NULL,
	[auxiliaryAccountingCode] [int] NULL,
	[destinationFundSIBId] [int] NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_destinationFund] PRIMARY KEY CLUSTERED 
(
	[destinationFundId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [global].[destinationFundSIBI]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [global].[destinationFundSIBI](
	[destinationFundSIBId] [int] NOT NULL,
	[destinationFundSIBName] [varchar](500) NULL,
	[category] [varchar](1) NULL,
	[Group] [varchar](3) NULL,
	[Division] [varchar](2) NULL,
	[Rama] [varchar](4) NULL,
	[SegmentDR] [varchar](6) NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_destinationFundSIBI] PRIMARY KEY CLUSTERED 
(
	[destinationFundSIBId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [global].[financialSector]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [global].[financialSector](
	[financialSectorId] [int] NOT NULL,
	[accountTypeID] [int] NULL,
	[financialSectorName] [varchar](150) NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_financialSector] PRIMARY KEY CLUSTERED 
(
	[financialSectorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [global].[founIncome]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [global].[founIncome](
	[companyId] [int] NOT NULL,
	[founIncomeId] [int] NOT NULL,
	[founIncomeName] [varchar](150) NULL,
	[founIncomeCode] [varchar](50) NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_founIncome] PRIMARY KEY CLUSTERED 
(
	[companyId] ASC,
	[founIncomeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [global].[Parameters]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [global].[Parameters](
	[ParameterId] [smallint] NOT NULL,
	[ParameterKey] [varchar](500) NULL,
	[ParameterValue] [varchar](500) NULL,
 CONSTRAINT [PK_Parameters] PRIMARY KEY CLUSTERED 
(
	[ParameterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [global].[physicalSector]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [global].[physicalSector](
	[CountryID] [int] NOT NULL,
	[cityId] [int] NOT NULL,
	[physicalSectorId] [int] NOT NULL,
	[physicalSectorName] [varchar](250) NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_physicalSector] PRIMARY KEY CLUSTERED 
(
	[CountryID] ASC,
	[cityId] ASC,
	[physicalSectorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [global].[province]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [global].[province](
	[CountryID] [int] NOT NULL,
	[provinceId] [int] NOT NULL,
	[provinceName] [varchar](250) NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_province] PRIMARY KEY CLUSTERED 
(
	[CountryID] ASC,
	[provinceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [global].[Reason]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [global].[Reason](
	[reasonId] [int] NOT NULL,
	[reasonName] [varchar](150) NULL,
	[reasonCode] [varchar](50) NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Reason] PRIMARY KEY CLUSTERED 
(
	[reasonId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [global].[SourceIncome]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [global].[SourceIncome](
	[companyId] [int] NOT NULL,
	[SourceIncomeId] [int] NOT NULL,
	[SourceIncomeName] [varchar](150) NULL,
	[SourceIncomeCode] [varchar](50) NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_SourceIncome] PRIMARY KEY CLUSTERED 
(
	[companyId] ASC,
	[SourceIncomeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Loan].[Account]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Loan].[Account](
	[accountId] [bigint] NOT NULL,
	[companyId] [int] NULL,
	[officeId] [int] NULL,
	[ConceptTypeId] [int] NULL,
	[accountTypeID] [int] NULL,
	[currencyId] [int] NULL,
	[relatedContactId] [bigint] NULL,
	[addressId] [bigint] NULL,
	[accountStatusId] [int] NULL,
	[founIncomeId] [int] NULL,
	[accountExecutiveContactId] [bigint] NULL,
	[accountPromoterContactId] [bigint] NULL,
	[financialSectorId] [int] NULL,
	[serviceProductId] [int] NULL,
	[accountName] [varchar](250) NULL,
	[userName] [varchar](250) NULL,
	[SendStatementAccount] [bit] NULL,
	[IsJointLoan] [bit] NULL,
	[RateType] [varchar](50) NULL,
	[account] [varchar](50) NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[accountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Loan].[accountStatus]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Loan].[accountStatus](
	[accountStatusId] [int] NOT NULL,
	[accountStatusName] [varchar](150) NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_accountStatus] PRIMARY KEY CLUSTERED 
(
	[accountStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Loan].[codebtor]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Loan].[codebtor](
	[companyId] [int] NOT NULL,
	[accountId] [bigint] NOT NULL,
	[clientId] [bigint] NOT NULL,
	[codebtorTypeId] [int] NULL,
	[canDeposit] [bit] NULL,
	[canWithdraw] [bit] NULL,
	[canCancel] [bit] NULL,
	[IsJointY] [bit] NULL,
	[IsJointO] [bit] NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_codebtor] PRIMARY KEY CLUSTERED 
(
	[companyId] ASC,
	[accountId] ASC,
	[clientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Loan].[codebtorType]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Loan].[codebtorType](
	[codebtorTypeId] [int] NOT NULL,
	[codebtorTypeName] [varchar](150) NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_codebtorType] PRIMARY KEY CLUSTERED 
(
	[codebtorTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Loan].[collateralField]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Loan].[collateralField](
	[collateralFieldId] [int] NOT NULL,
	[immovablePropertyTypeId] [int] NULL,
	[ListId] [int] NULL,
	[collateralFieldName] [varchar](250) NULL,
	[collateralFieldComment] [varchar](500) NULL,
	[ISRequired] [bit] NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_collateralField] PRIMARY KEY CLUSTERED 
(
	[collateralFieldId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Loan].[collateralType]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Loan].[collateralType](
	[collateralTypeId] [int] NOT NULL,
	[collateralTypeName] [varchar](150) NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_collateralType] PRIMARY KEY CLUSTERED 
(
	[collateralTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Loan].[collateralTypeSIB]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Loan].[collateralTypeSIB](
	[collateralTypeSIBId] [int] NOT NULL,
	[collateralTypeSIBName] [varchar](150) NULL,
	[collateralTypeSIBCode] [varchar](50) NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_collateralTypeSIB] PRIMARY KEY CLUSTERED 
(
	[collateralTypeSIBId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Loan].[committee]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Loan].[committee](
	[companyId] [int] NOT NULL,
	[committeeId] [int] NOT NULL,
	[currencyId] [int] NULL,
	[committeeName] [varchar](250) NULL,
	[committeeCode] [varchar](50) NULL,
	[minimumAmount] [numeric](18, 3) NULL,
	[MaximumAmount] [numeric](18, 3) NULL,
	[numberUsertoApprove] [int] NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_committee] PRIMARY KEY CLUSTERED 
(
	[companyId] ASC,
	[committeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Loan].[ConceptGeneralParameter]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Loan].[ConceptGeneralParameter](
	[companyId] [int] NOT NULL,
	[ConceptTypeId] [int] NOT NULL,
	[creditTypeId] [int] NULL,
	[minAmountApproval] [numeric](18, 3) NULL,
	[maxAmountApproval] [numeric](18, 3) NULL,
	[minLoanTerm] [int] NULL,
	[maxLoanTerm] [int] NULL,
	[maxGraceDays] [int] NULL,
	[condonationRate] [smallint] NULL,
	[condonationRateLate] [smallint] NULL,
	[IsLoanTermInDays] [bit] NULL,
	[condonationCommission] [smallint] NULL,
	[initialCommissionPercent] [numeric](18, 3) NULL,
	[EndCommissionPercent] [numeric](18, 3) NULL,
	[initialRateLate] [numeric](18, 3) NULL,
	[endRateLate] [numeric](18, 3) NULL,
	[HasCollateral] [bit] NULL,
	[RateCalculation] [smalldatetime] NULL,
	[RateLateCalculation] [smalldatetime] NULL,
	[LoanBillingDay] [int] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_ConceptGeneralParameter] PRIMARY KEY CLUSTERED 
(
	[companyId] ASC,
	[ConceptTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Loan].[creditFacility]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Loan].[creditFacility](
	[companyId] [int] NOT NULL,
	[creditFacilityId] [int] NOT NULL,
	[creditFacilityName] [varchar](150) NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_creditFacility] PRIMARY KEY CLUSTERED 
(
	[companyId] ASC,
	[creditFacilityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Loan].[creditType]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Loan].[creditType](
	[creditTypeId] [int] NOT NULL,
	[creditTypeName] [varchar](150) NULL,
	[creditTypeCode] [varchar](50) NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_creditType] PRIMARY KEY CLUSTERED 
(
	[creditTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Loan].[expenseFormCal]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Loan].[expenseFormCal](
	[expenseFormCalId] [int] NOT NULL,
	[expenseFormCalName] [varchar](250) NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_expenseFormCal] PRIMARY KEY CLUSTERED 
(
	[expenseFormCalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Loan].[expenseHeader]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Loan].[expenseHeader](
	[companyId] [int] NOT NULL,
	[expenseHeaderId] [int] NOT NULL,
	[accountTypeID] [int] NULL,
	[expenseFormCalId] [int] NULL,
	[PaymentFormId] [int] NULL,
	[expenseHeaderName] [varchar](250) NULL,
	[CanCondone] [bit] NULL,
	[amount] [numeric](18, 3) NULL,
	[CanEdit] [bit] NULL,
	[CanCXP] [bit] NULL,
	[ISInsurence] [varchar](100) NULL,
	[ISLegal] [bit] NULL,
	[ISQuata] [bit] NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_expenseHeader] PRIMARY KEY CLUSTERED 
(
	[companyId] ASC,
	[expenseHeaderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Loan].[immovablePropertyType]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Loan].[immovablePropertyType](
	[immovablePropertyTypeId] [int] NOT NULL,
	[immovablePropertyTypeName] [varchar](150) NULL,
	[immovablePropertyTypeCode] [varchar](50) NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_immovablePropertyType] PRIMARY KEY CLUSTERED 
(
	[immovablePropertyTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Loan].[loancollateral]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Loan].[loancollateral](
	[accountId] [bigint] NOT NULL,
	[collateralId] [bigint] NOT NULL,
	[amount] [numeric](18, 3) NULL,
	[percent] [numeric](18, 3) NULL,
	[date] [date] NULL,
	[code] [int] NULL,
	[codeDesc] [varchar](150) NULL,
	[transationNumber] [bigint] NULL,
	[contractNumber] [bigint] NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_loancollateral] PRIMARY KEY CLUSTERED 
(
	[accountId] ASC,
	[collateralId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Loan].[loanCollateralFieldValue]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Loan].[loanCollateralFieldValue](
	[collateralId] [bigint] NOT NULL,
	[collateralFieldId] [int] NOT NULL,
	[collateralFieldValue] [varchar](500) NULL,
	[isActive] [nchar](10) NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_loanCollateralFieldValue] PRIMARY KEY CLUSTERED 
(
	[collateralId] ASC,
	[collateralFieldId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Loan].[LoanDomiciliationCards]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Loan].[LoanDomiciliationCards](
	[accountId] [bigint] NOT NULL,
	[clientId] [bigint] NOT NULL,
	[CardTypeId] [int] NOT NULL,
	[LastFourDigits] [varchar](4) NOT NULL,
	[IsMain] [bit] NULL,
	[ApplyRange] [bit] NULL,
	[DateFrom] [date] NULL,
	[DateTo] [date] NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_LoanDomiciliationCards] PRIMARY KEY CLUSTERED 
(
	[accountId] ASC,
	[clientId] ASC,
	[CardTypeId] ASC,
	[LastFourDigits] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Loan].[loanExpense]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Loan].[loanExpense](
	[loanExpenseId] [bigint] NOT NULL,
	[companyId] [int] NOT NULL,
	[expenseHeaderId] [int] NOT NULL,
	[accountId] [bigint] NULL,
	[paymentFormId] [int] NULL,
	[percent] [numeric](18, 3) NULL,
	[amount] [numeric](18, 3) NULL,
	[payment] [numeric](18, 3) NULL,
	[initialQuata] [int] NULL,
	[endQuata] [int] NULL,
	[date] [date] NULL,
	[disbursementNumber] [int] NULL,
	[QuataNumber] [int] NULL,
	[TransationNumber] [bigint] NULL,
	[CanCondone] [bit] NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_loanExpense] PRIMARY KEY CLUSTERED 
(
	[loanExpenseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Loan].[loanHeader]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Loan].[loanHeader](
	[accountId] [bigint] NOT NULL,
	[companyId] [int] NULL,
	[officeId] [int] NULL,
	[quotationId] [bigint] NULL,
	[ConceptTypeId] [int] NULL,
	[relatedContactId] [bigint] NULL,
	[founIncomeId] [int] NULL,
	[businessLineId] [int] NULL,
	[destinationFundId] [int] NULL,
	[PaymentTypeId] [int] NULL,
	[committeeId] [int] NULL,
	[currencyId] [int] NULL,
	[LoanStatusId] [int] NULL,
	[SourceIncomeId] [int] NULL,
	[lineCreditId] [bigint] NULL,
	[creditFacilityId] [int] NULL,
	[LastQuotaDate] [date] NULL,
	[loanNumber] [int] NULL,
	[CheckDigit] [int] NULL,
	[Rate] [numeric](18, 3) NULL,
	[RateCommission] [numeric](18, 3) NULL,
	[rateLate] [numeric](18, 3) NULL,
	[codeCiiu] [varchar](10) NULL,
	[debtorClassification] [varchar](5) NULL,
	[numberExtension] [int] NULL,
	[LastClosedDate] [date] NULL,
	[PaidOffDate] [date] NULL,
	[expirationDate] [date] NULL,
	[amountApproved] [numeric](18, 3) NULL,
	[ApprovedDate] [date] NULL,
	[OutstandingBalance] [numeric](18, 3) NULL,
	[AutomaticPayment] [bit] NULL,
	[DisbursedAmount] [numeric](18, 3) NULL,
	[SendStatementAccount] [bit] NULL,
	[loanTerm] [int] NULL,
	[QuotaAmount] [numeric](18, 3) NULL,
	[LoanDillingDay] [int] NULL,
	[ISCapitalInDays] [bit] NULL,
	[TypeDisbursement] [int] NULL,
	[grace] [int] NULL,
	[graceDays] [int] NULL,
	[Comment] [varchar](5000) NULL,
	[IsJointLoan] [bit] NULL,
	[disbursementDate] [date] NULL,
	[accountName] [varchar](250) NULL,
	[qoutationReference] [varchar](50) NULL,
	[financedAmount] [numeric](18, 3) NULL,
	[expensesAmount] [numeric](18, 3) NULL,
	[hasCollateral] [bit] NULL,
	[ClosedDate] [date] NULL,
	[TypeExpiration] [int] NULL,
	[creditOrigin] [varchar](50) NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_loanHeader_1] PRIMARY KEY CLUSTERED 
(
	[accountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Loan].[LoanIndicators]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Loan].[LoanIndicators](
	[accountId] [bigint] NOT NULL,
	[TypeLoanPaymentId] [int] NULL,
	[reasonId] [int] NULL,
	[quotaId] [int] NULL,
	[IsLegal] [bit] NULL,
	[IsSuspense] [bit] NULL,
	[IsRenewal] [bit] NULL,
	[ISLoanTransferred] [bit] NULL,
	[IsJudicialCollection] [bit] NULL,
	[IsLegalTransunion] [bit] NULL,
	[monthToRepriced] [int] NULL,
	[disbursementDate] [date] NULL,
	[LastDateRepriced] [date] NULL,
	[LastQuotaGenerated] [int] NULL,
	[flexibilityCode] [varchar](50) NULL,
	[nextReviewRate] [date] NULL,
	[datePaidoff] [date] NULL,
	[lastPaidDate] [date] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_LoanIndicators] PRIMARY KEY CLUSTERED 
(
	[accountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Loan].[LoanStatus]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Loan].[LoanStatus](
	[LoanStatusId] [int] NOT NULL,
	[LoanStatusName] [varchar](150) NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_LoanStatus] PRIMARY KEY CLUSTERED 
(
	[LoanStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Loan].[PaymentForm]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Loan].[PaymentForm](
	[PaymentFormId] [int] NOT NULL,
	[PaymentFormName] [varchar](150) NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Form] PRIMARY KEY CLUSTERED 
(
	[PaymentFormId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Loan].[PaymentType]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Loan].[PaymentType](
	[PaymentTypeId] [int] NOT NULL,
	[quotationPaymentTypeName] [varchar](150) NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_quotationPaymentType] PRIMARY KEY CLUSTERED 
(
	[PaymentTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Loan].[policyCollateral]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Loan].[policyCollateral](
	[collateralId] [int] NOT NULL,
	[policyNo] [varchar](50) NOT NULL,
	[companyId] [int] NOT NULL,
	[policyTypeId] [int] NULL,
	[relatedContactId] [bigint] NULL,
	[policyCollateralName] [varchar](250) NULL,
	[policyCollateralComment] [varchar](500) NULL,
	[Amount] [numeric](18, 3) NULL,
	[Date] [date] NULL,
	[notificationDate] [date] NULL,
	[EffectiveDate] [date] NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_policyCollateral] PRIMARY KEY CLUSTERED 
(
	[collateralId] ASC,
	[policyNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Loan].[policyCollateralEndorse]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Loan].[policyCollateralEndorse](
	[collateralId] [int] NOT NULL,
	[policyNo] [varchar](50) NOT NULL,
	[endorseNo] [varchar](50) NULL,
	[amount] [numeric](18, 3) NULL,
	[Date] [date] NULL,
	[InicialDate] [date] NULL,
	[EffectiveDate] [date] NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_policyCollateralEndorse] PRIMARY KEY CLUSTERED 
(
	[collateralId] ASC,
	[policyNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Loan].[policyType]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Loan].[policyType](
	[policyTypeId] [int] NOT NULL,
	[policyTypeName] [varchar](500) NULL,
	[policyTypeCode] [varchar](50) NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_policyType] PRIMARY KEY CLUSTERED 
(
	[policyTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Loan].[quotation]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Loan].[quotation](
	[quotationId] [bigint] NOT NULL,
	[relatedContactId] [bigint] NULL,
	[addressId] [bigint] NULL,
	[companyId] [int] NULL,
	[officeId] [int] NULL,
	[ConceptTypeId] [int] NULL,
	[destinationFundId] [int] NULL,
	[quotationPaymentTypeId] [int] NULL,
	[currencyId] [int] NULL,
	[quotationStatusId] [int] NULL,
	[committeeId] [int] NULL,
	[accountExecutiveContactId] [bigint] NULL,
	[accountPromoterContactId] [bigint] NULL,
	[financialSectorId] [int] NULL,
	[SourceIncomeId] [int] NULL,
	[founIncomeId] [int] NULL,
	[serviceProductId] [int] NULL,
	[quotationTypeId] [int] NULL,
	[businessLineId] [int] NULL,
	[amount] [numeric](18, 3) NULL,
	[qoutationDate] [datetime] NULL,
	[qoutationDocDate] [datetime] NULL,
	[loanTerm] [int] NULL,
	[frequency] [int] NULL,
	[TypeDisbursement] [int] NULL,
	[IsJointLoan] [bit] NULL,
	[rateLate] [numeric](18, 3) NULL,
	[comment] [varchar](5000) NULL,
	[clientName] [varchar](250) NULL,
	[creditFacility] [int] NULL,
	[quotaAmount] [numeric](18, 3) NULL,
	[grace] [int] NULL,
	[graceDays] [int] NULL,
	[statementDate] [int] NULL,
	[SendStatementAccount] [bit] NULL,
	[AutomaticPayment] [bit] NULL,
	[RejectDate] [datetime] NULL,
	[MothToChangePrice] [int] NULL,
	[NumberOfReview] [int] NULL,
	[RateType] [varchar](50) NULL,
	[OriginCredit] [nchar](10) NULL,
	[Rate] [numeric](18, 3) NULL,
	[RateCommission] [numeric](18, 3) NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_quotation] PRIMARY KEY CLUSTERED 
(
	[quotationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Loan].[quotationStatus]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Loan].[quotationStatus](
	[quotationStatusId] [int] NOT NULL,
	[quotationStatusName] [varchar](150) NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_quotationStatus] PRIMARY KEY CLUSTERED 
(
	[quotationStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Loan].[quotationType]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Loan].[quotationType](
	[quotationTypeId] [int] NOT NULL,
	[quotationTypeName] [varchar](150) NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_quotationType] PRIMARY KEY CLUSTERED 
(
	[quotationTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Loan].[serviceProduct]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Loan].[serviceProduct](
	[serviceProductId] [int] NOT NULL,
	[accountTypeID] [int] NULL,
	[serviceProducName] [varchar](150) NULL,
	[serviceProducCode] [varchar](50) NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_serviceProduct] PRIMARY KEY CLUSTERED 
(
	[serviceProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Loan].[transactionDefinition]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Loan].[transactionDefinition](
	[transactionDefinitionId] [int] NOT NULL,
	[transactionCode] [varchar](5) NULL,
	[accountTypeID] [int] NULL,
	[transactionDescription] [varchar](50) NULL,
	[transactionDescriptionShort] [varchar](50) NULL,
	[creditOrigen] [smallint] NULL,
	[ISEvent] [smallint] NULL,
	[IStransaction] [bit] NULL,
	[CanPrintStatement] [bit] NULL,
	[SearchBy] [varchar](50) NULL,
	[annulTransactionDefinitionId] [int] NULL,
	[fixTransactionDefinitionId] [int] NULL,
	[requiresAuthorization] [smallint] NULL,
	[IsCreateEvent] [bit] NULL,
	[intersuccursal] [smallint] NULL,
	[interfund] [smallint] NULL,
	[creditNoteType] [varchar](50) NULL,
	[HasNCF] [smallint] NULL,
	[HasSurcharges] [smallint] NULL,
	[CanPending] [smallint] NULL,
	[CanRecurrent] [smallint] NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_transactionDefinition] PRIMARY KEY CLUSTERED 
(
	[transactionDefinitionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Loan].[transactionPaymentForm]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Loan].[transactionPaymentForm](
	[transactionPaymentFormId] [int] NOT NULL,
	[transactionPaymentFormName] [varchar](150) NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_transactionPaymentForm] PRIMARY KEY CLUSTERED 
(
	[transactionPaymentFormId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Loan].[transactionPaymentPlan]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Loan].[transactionPaymentPlan](
	[transactionPaymentPlanId] [bigint] NOT NULL,
	[accountId] [bigint] NULL,
	[companyId] [int] NULL,
	[officeId] [int] NULL,
	[quotaNumber] [int] NULL,
	[transactionReasonId] [int] NULL,
	[emisionQuotaDate] [datetime] NULL,
	[endQuotaDate] [datetime] NULL,
	[expenseAmount] [numeric](18, 3) NULL,
	[commissionAmount] [numeric](18, 3) NULL,
	[interestAmoint] [numeric](18, 3) NULL,
	[capitalAmount] [numeric](18, 3) NULL,
	[rateLateAmount] [numeric](18, 3) NULL,
	[expenseBalance] [numeric](18, 3) NULL,
	[commissionBalance] [numeric](18, 3) NULL,
	[rateLateBalance] [numeric](18, 3) NULL,
	[interestBalance] [numeric](18, 3) NULL,
	[capitalBalance] [numeric](18, 3) NULL,
	[IsPaymentExpense] [smallint] NULL,
	[IsPaymentcommission] [smallint] NULL,
	[IsPaymentinterest] [smallint] NULL,
	[Days] [numeric](18, 2) NULL,
	[referenceNumber] [varchar](50) NULL,
	[loanBalanceBefore] [numeric](18, 3) NULL,
	[ISLastQuota] [bit] NULL,
	[TransactionType] [int] NULL,
	[initialDay] [datetime] NULL,
	[chargesPrepayment] [numeric](18, 3) NULL,
	[graceAmount] [numeric](18, 3) NULL,
	[graceCommissionAmount] [numeric](18, 3) NULL,
	[IsPrepayment] [bit] NULL,
	[IsCapitalPayment] [bit] NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_transactionPaymentPlan] PRIMARY KEY CLUSTERED 
(
	[transactionPaymentPlanId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Loan].[transactionReason]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Loan].[transactionReason](
	[transactionReasonId] [int] NOT NULL,
	[transactionReasonName] [varchar](150) NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_transactionReason] PRIMARY KEY CLUSTERED 
(
	[transactionReasonId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Loan].[transactionReceipt]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Loan].[transactionReceipt](
	[transactionReceiptId] [bigint] NOT NULL,
	[transactionDefinitionId] [int] NULL,
	[companyId] [int] NULL,
	[officeId] [int] NULL,
	[transactionPaymentFormId] [int] NULL,
	[accountId] [bigint] NULL,
	[relatedContactId] [bigint] NULL,
	[receiptNumber] [int] NULL,
	[transactionDate] [date] NULL,
	[transactionNumber] [bigint] NULL,
	[discount] [numeric](18, 3) NULL,
	[dicumentNumber] [varchar](50) NULL,
	[Payment] [numeric](18, 3) NULL,
	[Status] [int] NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_transactionReceipt] PRIMARY KEY CLUSTERED 
(
	[transactionReceiptId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Loan].[transactionReceiptDetail]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Loan].[transactionReceiptDetail](
	[transactionReceiptId] [bigint] NOT NULL,
	[transactionReceiptSequence] [int] NOT NULL,
	[transactionPaymentPlanId] [bigint] NULL,
	[transactionSinalId] [int] NULL,
	[rateLateAmount] [numeric](18, 3) NULL,
	[expenseAmount] [numeric](18, 3) NULL,
	[commissionAmount] [numeric](18, 3) NULL,
	[interestAmoint] [numeric](18, 3) NULL,
	[capitalAmount] [numeric](18, 3) NULL,
	[discountAmount] [numeric](18, 3) NULL,
	[documentNumber] [varchar](50) NULL,
	[transactionPaymentFormId] [int] NULL,
	[Sequence] [int] NULL,
	[chargeAmount] [numeric](18, 3) NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_transactionReceiptDetail] PRIMARY KEY CLUSTERED 
(
	[transactionReceiptId] ASC,
	[transactionReceiptSequence] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Loan].[transactionSinal]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Loan].[transactionSinal](
	[transactionSinalId] [int] NOT NULL,
	[transactionSinalIdName] [varchar](150) NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_transactionSinalId] PRIMARY KEY CLUSTERED 
(
	[transactionSinalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Loan].[TypeLoanPayment]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Loan].[TypeLoanPayment](
	[TypeLoanPaymentId] [int] NOT NULL,
	[TypeLoanPaymentName] [varchar](150) NULL,
	[isActive] [bit] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_TypeLoanPayment] PRIMARY KEY CLUSTERED 
(
	[TypeLoanPaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Recaudo].[Archivo]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Recaudo].[Archivo](
	[ArchivoID] [int] IDENTITY(1,1) NOT NULL,
	[TipoArchivoID] [int] NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[ArchivoName] [varchar](50) NOT NULL,
	[EstatusID] [int] NOT NULL,
	[ModifiedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ArchivoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Recaudo].[CardNetBatchDetail]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Recaudo].[CardNetBatchDetail](
	[ArchivoID] [int] NOT NULL,
	[DetailId] [int] NOT NULL,
	[RecordType] [char](3) NULL,
	[TranSequenceNumber] [char](4) NULL,
	[TransType] [char](1) NULL,
	[AccountNumber] [varbinary](8000) NULL,
	[TerminalNumber] [char](16) NULL,
	[TransactionAmount] [char](12) NULL,
	[AmountSettlement] [char](12) NULL,
	[ExpirationDate] [char](4) NULL,
	[Reserved] [char](4) NULL,
	[ReferenceNumber] [char](12) NULL,
	[Reserved2] [char](13) NULL,
	[AccountNumberF] [varchar](19) NULL,
	[TransactionAmountF] [numeric](18, 2) NULL,
	[AmountSettlementF] [numeric](18, 2) NULL,
	[ReferenceNumberF] [bigint] NULL,
	[FileLine] [varbinary](8000) NULL,
	[EstatusID] [int] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_CardNetBatchDetail] PRIMARY KEY CLUSTERED 
(
	[ArchivoID] ASC,
	[DetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Recaudo].[CardNetBatchHeder]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Recaudo].[CardNetBatchHeder](
	[ArchivoID] [int] NOT NULL,
	[RecordType] [char](3) NULL,
	[TransactionCurrencyCode] [char](3) NULL,
	[MerchantBatchDate] [char](8) NULL,
	[BatchTransmitTime] [char](4) NULL,
	[MerchantNumber] [char](16) NULL,
	[MerchantBatchNumber] [char](5) NULL,
	[Reserved] [char](13) NULL,
	[CardAcceptorName] [char](40) NULL,
	[Reserved2] [char](8) NULL,
	[FileLine] [varbinary](8000) NULL,
	[Comments] [varchar](5000) NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_CardNetBatchHeder] PRIMARY KEY CLUSTERED 
(
	[ArchivoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Recaudo].[CardNetBatchTrailer]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Recaudo].[CardNetBatchTrailer](
	[ArchivoID] [int] NOT NULL,
	[RecordType] [char](3) NULL,
	[BatchTransactionCount] [char](5) NULL,
	[BatchAmount] [char](16) NULL,
	[Reserved] [char](13) NULL,
	[Reserved2] [char](63) NULL,
	[BatchAmountF] [numeric](18, 2) NULL,
	[FileLine] [varbinary](8000) NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_CardNetBatchTrailer] PRIMARY KEY CLUSTERED 
(
	[ArchivoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Recaudo].[CardNetConciliacion]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Recaudo].[CardNetConciliacion](
	[CardNetConciliacionId] [int] NOT NULL,
	[ArchivoId] [int] NOT NULL,
	[RespuestaId] [int] NULL,
	[RecordType] [varchar](3) NULL,
	[TranSequenceNumber] [int] NULL,
	[TransType] [varchar](1) NULL,
	[AccountNumber] [varchar](19) NULL,
	[AccountNumberLast4] [varchar](4) NULL,
	[TerminalNumber] [varchar](20) NULL,
	[TransactionAmount] [numeric](18, 2) NULL,
	[AmountSettlement] [numeric](18, 2) NULL,
	[ExpirationDate] [varchar](4) NULL,
	[Reserved] [varchar](4) NULL,
	[ReferenceNumber] [varchar](12) NULL,
	[Reserved2] [varchar](13) NULL,
	[AuthorizationCode] [varchar](6) NULL,
	[ReferenceNumber2] [varchar](12) NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NOT NULL,
	[ModiUsrId] [int] NULL,
	[hostName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_CardNetConciliacion] PRIMARY KEY CLUSTERED 
(
	[CardNetConciliacionId] ASC,
	[ArchivoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Recaudo].[CardNetRespuesta]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Recaudo].[CardNetRespuesta](
	[RespuestaId] [smallint] NOT NULL,
	[RespuestaCodigo] [varchar](50) NULL,
	[Descripcion] [varchar](500) NULL,
 CONSTRAINT [PK_CardNetRespuesta] PRIMARY KEY CLUSTERED 
(
	[RespuestaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Recaudo].[CobroAutomatico]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Recaudo].[CobroAutomatico](
	[CobroAutomaticoID] [int] IDENTITY(1,1) NOT NULL,
	[NombreInscripto] [varchar](8000) NULL,
	[RNC_Cedula] [varchar](8000) NULL,
	[MontoFormat] [varchar](8000) NULL,
	[Monto2] [decimal](38, 2) NULL,
	[Monto3] [decimal](38, 0) NULL,
	[IDServicio] [varchar](15) NULL,
	[TipoProducto] [varchar](2) NOT NULL,
	[NumeroProducto] [varchar](20) NULL,
	[Moneda] [varchar](3) NOT NULL,
	[TipoRegistro] [varchar](1) NOT NULL,
	[Banco] [varchar](3) NOT NULL,
	[Email] [varchar](13) NOT NULL,
	[Referencia] [varchar](13) NOT NULL,
	[NCF] [varchar](1) NOT NULL,
	[ITBIS] [varchar](1) NOT NULL,
	[OTROS] [varchar](1) NOT NULL,
	[ArchivoID] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[CobroAutomaticoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Recaudo].[Estatus]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Recaudo].[Estatus](
	[EstatusID] [int] IDENTITY(1,1) NOT NULL,
	[EstatusDescripcion] [varchar](35) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[EstatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Recaudo].[Excepciones]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Recaudo].[Excepciones](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Excepcion] [varchar](max) NULL,
	[CreatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Recaudo].[LogPagos]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Recaudo].[LogPagos](
	[LogPagoId] [bigint] IDENTITY(1,1) NOT NULL,
	[PagoID] [int] NOT NULL,
	[TieneError] [bit] NULL,
	[Mensaje] [varchar](max) NULL,
	[Source] [varchar](50) NULL,
	[NombreCliente] [varchar](250) NULL,
	[NumeroReferencia] [varchar](100) NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[UsuarioCreoId] [int] NOT NULL,
 CONSTRAINT [PK_LogPagos] PRIMARY KEY CLUSTERED 
(
	[LogPagoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Recaudo].[Pago]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Recaudo].[Pago](
	[PagoID] [int] IDENTITY(1,1) NOT NULL,
	[IDReferecia] [int] NOT NULL,
	[IDConciliacion] [int] NOT NULL,
	[MontoPagado] [decimal](13, 2) NULL,
	[MontoAplicadoEasyBank] [decimal](13, 2) NULL,
	[ReciboEasyBank] [varchar](100) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ArchivoId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[PagoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Recaudo].[PreNotificacion]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Recaudo].[PreNotificacion](
	[PreNotificacionID] [int] IDENTITY(1,1) NOT NULL,
	[NumeroCuenta] [varchar](13) NOT NULL,
	[CodigoBanco] [varchar](3) NOT NULL,
	[TipoCuenta] [varchar](2) NOT NULL,
	[NombreCliente] [varchar](200) NULL,
	[Identificacion] [varchar](256) NULL,
	[Descripcion] [varchar](14) NOT NULL,
	[ArchivoID] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[PreNotificacionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Recaudo].[RecConciliacion]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Recaudo].[RecConciliacion](
	[ConciliacionID] [int] IDENTITY(1,1) NOT NULL,
	[Proveedor] [varchar](11) NOT NULL,
	[Servicio] [varchar](11) NOT NULL,
	[TipoServicio] [char](1) NOT NULL,
	[Canal] [varchar](50) NOT NULL,
	[NoReferencia] [varchar](15) NOT NULL,
	[NombreCliente] [varchar](100) NOT NULL,
	[MontoFactura] [decimal](13, 2) NOT NULL,
	[MontoPagoEfectivo] [decimal](13, 2) NULL,
	[MontoDebitoCuenta] [decimal](13, 2) NOT NULL,
	[MontoPagoCheque] [decimal](13, 2) NULL,
	[Fecha] [date] NOT NULL,
	[Hora] [char](6) NULL,
	[PagoReverso] [varchar](7) NOT NULL,
	[Moneda] [varchar](30) NOT NULL,
	[NumeroTransaccion] [varchar](50) NOT NULL,
	[Sucursal] [varchar](50) NOT NULL,
	[ArchivoID] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ConciliacionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Recaudo].[RecReferenciado]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Recaudo].[RecReferenciado](
	[RecaudoID] [int] IDENTITY(1,1) NOT NULL,
	[NoReferencia] [varchar](15) NOT NULL,
	[DescripcionReferencia] [varchar](30) NOT NULL,
	[Direccion] [varchar](50) NOT NULL,
	[Descripción] [varchar](30) NOT NULL,
	[FechaFactura] [datetime] NOT NULL,
	[FechaLimitePago] [datetime] NOT NULL,
	[FechaCorte] [datetime] NULL,
	[Moneda] [char](3) NOT NULL,
	[Monto] [decimal](13, 2) NULL,
	[Atraso] [decimal](13, 2) NOT NULL,
	[Otros] [decimal](13, 2) NULL,
	[TotalPagar] [decimal](13, 2) NOT NULL,
	[PagoMinimo] [decimal](13, 2) NULL,
	[NombreCompleto] [varchar](100) NOT NULL,
	[Estado] [char](1) NOT NULL,
	[IDServicio] [varchar](11) NOT NULL,
	[RNCCedula] [varchar](11) NULL,
	[Email] [varchar](80) NULL,
	[NCF] [varchar](11) NOT NULL,
	[ITBIS] [decimal](13, 2) NOT NULL,
	[OtrosImpuestos] [decimal](13, 2) NOT NULL,
	[ArchivoID] [int] NOT NULL,
	[EstatusID] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[RecaudoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [SMS].[SMSQueue]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [SMS].[SMSQueue](
	[SMSQueueID] [bigint] IDENTITY(1,1) NOT NULL,
	[SMSSourceID] [smallint] NULL,
	[SMSStatusID] [smallint] NULL,
	[SMSTypeID] [smallint] NULL,
	[ReferenceKey] [varchar](50) NOT NULL,
	[ClientName] [varchar](150) NOT NULL,
	[PhoneArea] [varchar](50) NOT NULL,
	[PhoneNumber] [varchar](50) NOT NULL,
	[SMS] [varchar](250) NOT NULL,
	[ProcessedDate] [datetime] NULL,
	[ErrorMessage] [varchar](150) NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NULL,
	[ModiUsrId] [int] NULL,
	[Hostname] [varchar](100) NOT NULL,
 CONSTRAINT [PK_SMSQueue] PRIMARY KEY CLUSTERED 
(
	[SMSQueueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [SMS].[SMSSource]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [SMS].[SMSSource](
	[SMSSourceID] [smallint] NOT NULL,
	[Description] [varchar](150) NULL,
	[NameKey] [varchar](50) NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NULL,
	[ModiUsrId] [int] NULL,
	[Hostname] [varchar](100) NOT NULL,
 CONSTRAINT [PK_SMSSource] PRIMARY KEY CLUSTERED 
(
	[SMSSourceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [SMS].[SMSStatus]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [SMS].[SMSStatus](
	[SMSStatusID] [smallint] NOT NULL,
	[Description] [varchar](150) NULL,
	[NameKey] [varchar](50) NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NULL,
	[ModiUsrId] [int] NULL,
	[Hostname] [varchar](100) NOT NULL,
 CONSTRAINT [PK_SmsStatus] PRIMARY KEY CLUSTERED 
(
	[SMSStatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [SMS].[SMSType]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [SMS].[SMSType](
	[SMSTypeID] [smallint] NOT NULL,
	[Description] [varchar](150) NULL,
	[NameKey] [varchar](50) NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModiDate] [datetime] NULL,
	[CreateUsrId] [int] NULL,
	[ModiUsrId] [int] NULL,
	[Hostname] [varchar](100) NOT NULL,
 CONSTRAINT [PK_SMSType] PRIMARY KEY CLUSTERED 
(
	[SMSTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  View [global].[VW_GET_ACCTY]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [global].[VW_GET_ACCTY]
AS

--with accTy as 
--(
	SELECT 
	 accountTypeID = 1
	,accountTypeName ='Cuentas Corrientes'
	,accountTypeCode='CTA'
	,isActive=1
	,CreateDate=GETDATE()
	,ModiDate=GETDATE()
	,CreateUsrId=1
	,ModiUsrId=1
	,hostName='RAA'

   UNION ALL 
   	SELECT 
	 accountTypeID = 2
	,accountTypeName ='Cuentas de Ahorros'
	,accountTypeCode='AHO'
	,isActive=1
	,CreateDate=GETDATE()
	,ModiDate=GETDATE()
	,CreateUsrId=1
	,ModiUsrId=1
	,hostName='RAA'
	   UNION ALL 
   	SELECT 
	 accountTypeID = 3
	,accountTypeName ='Préstamos'
	,accountTypeCode='PRE'
	,isActive=1
	,CreateDate=GETDATE()
	,ModiDate=GETDATE()
	,CreateUsrId=1
	,ModiUsrId=1
	,hostName='RAA'

	UNION ALL 
   	SELECT 
	 accountTypeID = 4
	,accountTypeName ='Inversiones'
	,accountTypeCode='INV'
	,isActive=1
	,CreateDate=GETDATE()
	,ModiDate=GETDATE()
	,CreateUsrId=1
	,ModiUsrId=1
	,hostName='RAA'
		UNION ALL 
   	SELECT 
	 accountTypeID = 5
	,accountTypeName ='Factoring'
	,accountTypeCode='FAC'
	,isActive=1
	,CreateDate=GETDATE()
	,ModiDate=GETDATE()
	,CreateUsrId=1
	,ModiUsrId=1
	,hostName='RAA'

			UNION ALL 
   	SELECT 
	 accountTypeID = 6
	,accountTypeName ='Certificados'
	,accountTypeCode='CER'
	,isActive=1
	,CreateDate=GETDATE()
	,ModiDate=GETDATE()
	,CreateUsrId=1
	,ModiUsrId=1
	,hostName='RAA'
--)

--SELECT 
--	 accountTypeID
--	,accountTypeName
--	,accountTypeCode
--	,isActive
--	,CreateDate
--	,ModiDate
--	,CreateUsrId
--	,ModiUsrId
--	,hostName
--FROM  accTy
GO
/****** Object:  View [Recaudo].[RecargoReferenciadoDetail]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [Recaudo].[RecargoReferenciadoDetail]
AS

SELECT 
	 a.ArchivoID,
	 isnull(r.NoReferencia,'-') NoReferencia --No. Referencia
	,'Recaudo Referenciado' TipoRegistro --Tipo Registro
	,isnull(r.NombreCompleto, '-') NombreCliente --Nombre Completo
	,isnull (r.RNCCedula,'-') RNCCedula --RNC/Cédula
	,isnull (c.NoReferencia,'-') [NoCuenta]
	,isnull (r.TotalPagar,0.00) TotalPagar --Monto a Recaudar
	,isnull (c.MontoFactura,0.00) MontoFactura --Monto Pago BHD
	,isnull (c.Canal,'-') Canal --Canal BHD
	,isnull ( convert (varchar(10),c.Fecha ),'-') Fecha --Fecha Pago
	,CAST(p.ReciboEasyBank AS varchar(100)) ReciboEasyBank --Numero de Recibo
	,isnull (p.MontoAplicadoEasybank, 0.00) MontoEasyBank --Monto Aplicado Easybank
	,isnull (p.MontoPagado, 0.00) TotalAplicado  
	,isnull (e.EstatusDescripcion, '-')  Estatus --Status 
	,e.EstatusID
	,PagoReverso
	,CAST(ec.cuecta_formateado AS varchar(100))cuecta_formateado -- No Cuenta
	,ISNULL(easy.Monto,0) MontoPendienteCuenta
	,ISNULL(p.PagoID,0) PagoID
FROM 
Recaudo.RecReferenciado r 
INNER JOIN Recaudo.Archivo a on a.ArchivoID = r.ArchivoID
INNER JOIN Recaudo.Estatus e on e.EstatusID = r.EstatusID
LEFT JOIN Recaudo.RecConciliacion c on c.NoReferencia = r.NoReferencia and c.ArchivoID = r.ArchivoID
LEFT JOIN Recaudo.Pago p on p.IDConciliacion = c.ConciliacionID and c.ArchivoID = r.ArchivoID and p.IDReferecia = r.RecaudoID
LEFT JOIN [Easybank].dbo.ecuectam ec on ec.cuecta_numid =r.NoReferencia
LEFT JOIN 
(
	SELECT 
			Monto= SUM( precuo_salcap+precuo_salint+precuo_salcom+precuo_salgas+precuo_salmor)
		   ,cuecta_formateado
		   ,cuecta_numid
	FROM EasyBank.[dbo].[VW_CuotasPendientes]
	GROUP BY cuecta_numid,cuecta_formateado
) easy on easy.cuecta_numid=ec.cuecta_numid





GO
/****** Object:  View [Recaudo].[RecReferenciadoHeader]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [Recaudo].[RecReferenciadoHeader]
AS
SELECT 
	         arr.ArchivoID,
             arr.Fecha, --Fecha
			 arr.ArchivoName NombreArchivo, --Nombre
			 e.EstatusDescripcion Estatus,--Estatus
             cantcliente.CantidadClientes, --Cantidad Cliente
             ISNULL(cantcliente.TotalPagar, 0) AS MontoTotalRecaudacion, --Monto a Recaudar
          
            
		    
			 isnull(Pagos.MontoPagado, 0.00) MotoPagoBHD,--Monto Pago BHD
			 isnull(Pagos.CantidadPagos,0) AprobadasBHD, --Cantida Trans. BHD
			 isnull(Pagos.MontoReverso, 0.00) MotoReversoBHD,--Monto Reverso BHD
			 isnull(Pagos.CantidadReverso, 0) ReversoBHD, --Cantida  Reverso BHD

   
            isnull(Pagos.MontoAplicadoEasybank, 0.00) AS MontoAplicadoEasybank, --Monto Aplicado Easybank 
			isnull(Pagos.MontoPendienteEasybank, 0.00) AS MontoPendienteEasybank, --Monto Pendiente Easybank 

           e.EstatusID EstatusID
     FROM Recaudo.Archivo arr
     INNER JOIN Recaudo.Estatus e ON arr.EstatusID = e.EstatusID


     CROSS APPLY
     (
         SELECT 
				 COUNT(DISTINCT r1.NombreCompleto) AS CantidadClientes
				,SUM(r1.TotalPagar) AS TotalPagar
         FROM Recaudo.RecReferenciado r1
         WHERE r1.ArchivoID = arr.ArchivoID
         GROUP BY r1.ArchivoID
     ) cantcliente
      
       OUTER APPLY
     (
         SELECT 
				SUM(CASE WHEN c.PagoReverso  = 'PAGO' THEN 1 ELSE 0 END) AS CantidadPagos,
                SUM(CASE WHEN c.PagoReverso  = 'PAGO' THEN p.MontoPagado ELSE 0 END) MontoPagado,

				SUM(CASE WHEN c.PagoReverso  != 'PAGO' THEN 1 ELSE 0 END) AS CantidadReverso,
                SUM(CASE WHEN c.PagoReverso    != 'PAGO' THEN p.MontoPagado ELSE 0 END) MontoReverso,

                SUM(CASE WHEN isnull(p.MontoAplicadoEasybank,0) >0   THEN p.MontoAplicadoEasybank ELSE 0 END ) MontoAplicadoEasybank,
				SUM(CASE WHEN isnull(p.MontoAplicadoEasybank,0) <=0  THEN p.MontoPagado ELSE 0 END ) MontoPendienteEasybank
         FROM Recaudo.RecReferenciado r2
              INNER JOIN Recaudo.RecConciliacion c ON c.NoReferencia = r2.NoReferencia AND C.ArchivoID=r2.ArchivoID
              INNER JOIN Recaudo.Pago p ON p.IDConciliacion = c.ConciliacionID
         WHERE r2.ArchivoID = arr.ArchivoID 
               --AND c.PagoReverso = 'PAGO'
               --AND r2.EstatusID = 8	--PENDIENTE DE PAGO EASY BANK
         GROUP BY r2.ArchivoID
     ) Pagos
	 


GO
/****** Object:  View [Recaudo].[VCardNetBatchDetail]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [Recaudo].[VCardNetBatchDetail]
AS

SELECT 
	 a.ArchivoID,
	 isnull(cd.DetailId,'-') NoReferencia --No. Referencia
	 ,'Domiciliación CardNet' TipoRegistro --Tipo Registro
	 --,isnull(r.NombreCompleto, '-') NombreCliente --Nombre Completo
	--,isnull (r.RNCCedula,'-') RNCCedula --RNC/Cédula
	 ,isnull (cd.ReferenceNumberF,'-') [NoCuenta]
	 ,isnull (cd.TransactionAmountF,0.00) TotalPagar --Monto a Recaudar
	 ,isnull (c.AmountSettlement,0.00) MontoFactura --Monto Pago BHD
	 ,isnull ('Web Carnet','-') Canal --Canal BHD
	 ,isnull ( convert (varchar(10),c.CreateDate,103 ),'-') Fecha --Fecha Pago
	 ,CAST(p.ReciboEasyBank AS varchar(100)) ReciboEasyBank --Numero de Recibo
	 ,isnull (p.MontoAplicadoEasybank, 0.00) MontoEasyBank --Monto Aplicado Easybank
	 ,isnull (p.MontoPagado, 0.00) TotalAplicado  
	 ,isnull (cr.Descripcion, 'SIN PAGO')  Estatus --Status   Estatus --Status 
	,isnull (cr.RespuestaId,-1) EstatusID
	,PagoReverso='Pago'
	,CAST(ec.cuecta_formateado AS varchar(100))cuecta_formateado -- No Cuenta
	,ISNULL(easy.Monto,0) MontoPendienteCuenta
	,ISNULL(p.PagoID,0) PagoID
	,AccountNumberF
FROM [Recaudo].[CardNetBatchHeder] ch 
	INNER JOIN [Recaudo].[CardNetBatchDetail] cd
		ON ch.ArchivoID=cd.ArchivoID
	INNER JOIN [Recaudo].[CardNetBatchTrailer] cf
		ON ch.ArchivoID=cf.ArchivoID
INNER JOIN Recaudo.Archivo a on a.ArchivoID = ch.ArchivoID
INNER JOIN Recaudo.Estatus e on e.EstatusID = cd.EstatusID
LEFT JOIN [Recaudo].[CardNetConciliacion] c on c.ReferenceNumber = cd.ReferenceNumberF and c.ArchivoID = cd.ArchivoID
LEFT JOIN Recaudo.Pago p on p.IDConciliacion = c.CardNetConciliacionId and p.ArchivoId=cd.ArchivoID and p.IDReferecia = cd.DetailId
LEFT JOIN [Recaudo].[CardNetRespuesta] cr on cr.RespuestaId= c.RespuestaId 

LEFT JOIN [Easybank].dbo.ecuectam ec on cuecta_numid =cd.ReferenceNumberF

LEFT JOIN 
(
	SELECT 
			Monto= SUM( precuo_salcap+precuo_salint+precuo_salcom+precuo_salgas+precuo_salmor)
		   ,cuecta_formateado
		   ,cuecta_numid
	FROM EasyBank.[dbo].[VW_CuotasPendientes]
	GROUP BY cuecta_numid,cuecta_formateado
) easy on easy.cuecta_numid=ec.cuecta_numid



GO
/****** Object:  View [Recaudo].[VCardNetReferenciadoHeader]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [Recaudo].[VCardNetReferenciadoHeader]
AS
SELECT 
	         arr.ArchivoID,
             arr.Fecha, --Fecha
			 arr.ArchivoName NombreArchivo, --Nombre
			 e.EstatusDescripcion Estatus,--Estatus
             cantcliente.CantidadClientes, --Cantidad Cliente
             ISNULL(cantcliente.TotalPagar, 0) AS MontoTotalRecaudacion, --Monto a Recaudar
          
            
		    
			isnull(Pagos.MontoPagado, 0.00) MotoPagoBHD,--Monto Pago BHD
			isnull(Pagos.CantidadPagos,0) AprobadasBHD, --Cantida Trans. BHD
			isnull(Pagos.MontoReverso, 0.00) MotoReversoBHD,--Monto Reverso BHD
			isnull(Pagos.CantidadReverso, 0) ReversoBHD, --Cantida  Reverso BHD

   
			isnull(Pagos.MontoAplicadoEasybank, 0.00) AS MontoAplicadoEasybank, --Monto Aplicado Easybank 
			isnull(Pagos.MontoPendienteEasybank, 0.00) AS MontoPendienteEasybank, --Monto Pendiente Easybank 

           e.EstatusID EstatusID
     FROM Recaudo.Archivo arr
     INNER JOIN Recaudo.Estatus e ON arr.EstatusID = e.EstatusID


     CROSS APPLY
     (
		 	SELECT 
				COUNT(DISTINCT cd.ReferenceNumberF) AS CantidadClientes
				,SUM(cd.TransactionAmountF) AS TotalPagar
			FROM [Recaudo].[CardNetBatchHeder] ch 
			INNER JOIN [Recaudo].[CardNetBatchDetail] cd
				ON ch.ArchivoID=cd.ArchivoID
			INNER JOIN [Recaudo].[CardNetBatchTrailer] cf
				ON ch.ArchivoID=cf.ArchivoID
			WHERE ch.ArchivoID = arr.ArchivoID
			GROUP BY ch.ArchivoID

     ) cantcliente
      
       OUTER APPLY
     (
         SELECT 
				COUNT(*) AS CantidadPagos,
                SUM(TransactionAmountF) MontoPagado,

				0 AS CantidadReverso,
                0 MontoReverso,

                SUM(CASE WHEN isnull(p.MontoAplicadoEasybank,0) >0   THEN p.MontoAplicadoEasybank ELSE 0 END ) MontoAplicadoEasybank,
				SUM(CASE WHEN isnull(p.MontoAplicadoEasybank,0) <=0  THEN p.MontoPagado ELSE 0 END ) MontoPendienteEasybank
         FROM [Recaudo].[CardNetBatchHeder] ch 
			INNER JOIN [Recaudo].[CardNetBatchDetail] cd
				ON ch.ArchivoID=cd.ArchivoID
			INNER JOIN [Recaudo].[CardNetBatchTrailer] cf
				ON ch.ArchivoID=cf.ArchivoID

              INNER JOIN Recaudo.RecConciliacion c ON c.NoReferencia = cd.DetailId AND C.ArchivoID=cd.ArchivoID
              INNER JOIN Recaudo.Pago p ON p.IDConciliacion = c.ConciliacionID
         WHERE cd.ArchivoID = arr.ArchivoID 
         GROUP BY cd.ArchivoID
     ) Pagos
	 



GO
/****** Object:  View [Recaudo].[VW_PagoPendienteAplicarEasybank]    Script Date: 9/19/2018 4:24:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************************************************************************************************************
** Name:[Recaudo].VW_PagoPendienteAplicarEasybank
** Desc:Listado de pagos pendientes de aplicar a easybank, estos llegan por via del modulo de RECAUDO
** Auth:Ralvarez
** Date:09/12/2018
**************************
** Change History
**************************
** PR   Date        Author					Description 
** --   ----------  ---------------------   ------------------------------------
** 1    09/12/2018  Ralvarez   			    Listado de pagos pendientes de aplicar a easybank, estos llegan por via del modulo de RECAUDO, seran aplicado por un task
**********************************************************************************************************************************************************/
CREATE VIEW [Recaudo].[VW_PagoPendienteAplicarEasybank]
AS 
	SELECT p.[PagoID]
	      ,p.[IDReferecia]
	      ,p.[IDConciliacion]
	      ,p.[MontoPagado]
	      ,p.[MontoAplicadoEasyBank]
	      ,p.[ReciboEasyBank]
	      ,p.[CreatedDate]
	      ,p.[ModifiedDate]
		  ,FuentePago='RECAUDO BHD'
		  ,cuecta_formateado
		  ,cuecta_nombre
	  FROM [Recaudo].[Pago] p
	  INNER JOIN [Recaudo].[RecConciliacion] rec
		ON 
			rec.ConciliacionID=p.[IDConciliacion] 
		AND rec.ArchivoID=P.ArchivoID
	  INNER JOIN [Easybank].dbo.ecuectam ec on cuecta_numid =rec.NoReferencia
	  WHERE 
				ISNULL(MontoAplicadoEasyBank,0) = 0 
			AND ISNULL(ReciboEasyBank,0) =0
			AND NOT EXISTS 
			(
				SELECT TOP 1 1 FROM [Recaudo].[LogPagos] lg
				where lg.[PagoID]=p.PagoID and TieneError = 0 		
			) 
	UNION ALL 
	SELECT p.[PagoID]
	      ,p.[IDReferecia]
	      ,p.[IDConciliacion]
	      ,p.[MontoPagado]
	      ,p.[MontoAplicadoEasyBank]
	      ,p.[ReciboEasyBank]
	      ,p.[CreatedDate]
	      ,p.[ModifiedDate]
		  ,FuentePago='CARNET DOMICILIACION'
		  ,cuecta_formateado
		  ,cuecta_nombre
	  FROM [Recaudo].[Pago] p
	  INNER JOIN [Recaudo].[CardNetConciliacion] rec
		ON 
			rec.CardNetConciliacionId=p.[IDConciliacion] 
		AND rec.ArchivoId=P.ArchivoId
	  INNER JOIN [Easybank].dbo.ecuectam ec on cuecta_numid =rec.ReferenceNumber
	  WHERE 
				ISNULL(MontoAplicadoEasyBank,0) = 0 
			AND ISNULL(ReciboEasyBank,0) =0
			AND NOT EXISTS 
			(
				SELECT TOP 1 1 FROM [Recaudo].[LogPagos] lg
				where lg.[PagoID]=p.PagoID and TieneError = 0 		
			) 
	
	UNION ALL 
	SELECT p.[PagoID]
	      ,p.[IDReferecia]
	      ,p.[IDConciliacion]
	      ,p.[MontoPagado]
	      ,p.[MontoAplicadoEasyBank]
	      ,p.[ReciboEasyBank]
	      ,p.[CreatedDate]
	      ,p.[ModifiedDate]
		  ,FuentePago='PAG. DIRECTO EASYBANK'
		  ,cuecta_formateado=''
		  ,cuecta_nombre=''
	  FROM [Recaudo].[Pago] p
	 -- INNER JOIN [Recaudo].[CardNetConciliacion] rec
		--ON 
		--	rec.CardNetConciliacionId=p.[IDConciliacion] 
		--AND rec.ArchivoId=P.ArchivoId
	 --INNER JOIN [Easybank].dbo.ecuectam ec on cuecta_numid =rec.ReferenceNumber
	  WHERE 
				ISNULL(MontoAplicadoEasyBank,0) = 0 
			AND ISNULL(ReciboEasyBank,0) =0
			AND ArchivoId=0
			AND NOT EXISTS 
			(
				SELECT TOP 1 1 FROM [Recaudo].[LogPagos] lg
				where lg.[PagoID]=p.PagoID and TieneError = 0 		
			) 
	





GO
ALTER TABLE [Loan].[loanHeader] ADD  CONSTRAINT [DF_loanHeader_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [Loan].[loanHeader] ADD  CONSTRAINT [DF_loanHeader_CreateUsrId]  DEFAULT ((1)) FOR [CreateUsrId]
GO
ALTER TABLE [Recaudo].[CobroAutomatico] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [Recaudo].[Estatus] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [Recaudo].[Pago] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [Recaudo].[PreNotificacion] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [Recaudo].[RecConciliacion] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [Recaudo].[RecReferenciado] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [Entity].[client]  WITH CHECK ADD  CONSTRAINT [FK_client_clientTypeByCompany] FOREIGN KEY([clientTypeByCompanyId])
REFERENCES [Entity].[clientTypeByCompany] ([clientTypeByCompanyId])
GO
ALTER TABLE [Entity].[client] CHECK CONSTRAINT [FK_client_clientTypeByCompany]
GO
ALTER TABLE [Entity].[client]  WITH CHECK ADD  CONSTRAINT [FK_client_clientTypeBySIB] FOREIGN KEY([clientTypeBySIBId])
REFERENCES [Entity].[clientTypeBySIB] ([clientTypeBySIBId])
GO
ALTER TABLE [Entity].[client] CHECK CONSTRAINT [FK_client_clientTypeBySIB]
GO
ALTER TABLE [Entity].[client]  WITH CHECK ADD  CONSTRAINT [FK_client_Company] FOREIGN KEY([companyId])
REFERENCES [Entity].[Company] ([companyId])
GO
ALTER TABLE [Entity].[client] CHECK CONSTRAINT [FK_client_Company]
GO
ALTER TABLE [Entity].[client]  WITH CHECK ADD  CONSTRAINT [FK_client_linkedType] FOREIGN KEY([linkedTypeId])
REFERENCES [Entity].[linkedType] ([linkedTypeId])
GO
ALTER TABLE [Entity].[client] CHECK CONSTRAINT [FK_client_linkedType]
GO
ALTER TABLE [Entity].[client]  WITH CHECK ADD  CONSTRAINT [FK_client_Reason] FOREIGN KEY([reasonId])
REFERENCES [global].[Reason] ([reasonId])
GO
ALTER TABLE [Entity].[client] CHECK CONSTRAINT [FK_client_Reason]
GO
ALTER TABLE [Entity].[client]  WITH CHECK ADD  CONSTRAINT [FK_client_relatedContact] FOREIGN KEY([relatedContactId])
REFERENCES [Entity].[relatedContact] ([relatedContactId])
GO
ALTER TABLE [Entity].[client] CHECK CONSTRAINT [FK_client_relatedContact]
GO
ALTER TABLE [Entity].[clientTypeBySIB]  WITH CHECK ADD  CONSTRAINT [FK_clientTypeBySIB_EntityClass] FOREIGN KEY([EntityClassId])
REFERENCES [Entity].[EntityClass] ([EntityClassId])
GO
ALTER TABLE [Entity].[clientTypeBySIB] CHECK CONSTRAINT [FK_clientTypeBySIB_EntityClass]
GO
ALTER TABLE [Entity].[collateral]  WITH CHECK ADD  CONSTRAINT [FK_collateral_client] FOREIGN KEY([clientId])
REFERENCES [Entity].[client] ([clientId])
GO
ALTER TABLE [Entity].[collateral] CHECK CONSTRAINT [FK_collateral_client]
GO
ALTER TABLE [Entity].[collateral]  WITH CHECK ADD  CONSTRAINT [FK_collateral_collateralType] FOREIGN KEY([collateralTypeId])
REFERENCES [Loan].[collateralType] ([collateralTypeId])
GO
ALTER TABLE [Entity].[collateral] CHECK CONSTRAINT [FK_collateral_collateralType]
GO
ALTER TABLE [Entity].[collateral]  WITH CHECK ADD  CONSTRAINT [FK_collateral_collateralTypeSIB] FOREIGN KEY([collateralTypeSIBId])
REFERENCES [Loan].[collateralTypeSIB] ([collateralTypeSIBId])
GO
ALTER TABLE [Entity].[collateral] CHECK CONSTRAINT [FK_collateral_collateralTypeSIB]
GO
ALTER TABLE [Entity].[collateral]  WITH CHECK ADD  CONSTRAINT [FK_collateral_Company] FOREIGN KEY([companyId])
REFERENCES [Entity].[Company] ([companyId])
GO
ALTER TABLE [Entity].[collateral] CHECK CONSTRAINT [FK_collateral_Company]
GO
ALTER TABLE [Entity].[collateral]  WITH CHECK ADD  CONSTRAINT [FK_collateral_Currency] FOREIGN KEY([currencyId])
REFERENCES [global].[Currency] ([currencyId])
GO
ALTER TABLE [Entity].[collateral] CHECK CONSTRAINT [FK_collateral_Currency]
GO
ALTER TABLE [Entity].[Company]  WITH CHECK ADD  CONSTRAINT [FK_Company_Currency] FOREIGN KEY([currencyId])
REFERENCES [global].[Currency] ([currencyId])
GO
ALTER TABLE [Entity].[Company] CHECK CONSTRAINT [FK_Company_Currency]
GO
ALTER TABLE [Entity].[Company]  WITH CHECK ADD  CONSTRAINT [FK_Company_relatedContact] FOREIGN KEY([relatedContactId])
REFERENCES [Entity].[relatedContact] ([relatedContactId])
GO
ALTER TABLE [Entity].[Company] CHECK CONSTRAINT [FK_Company_relatedContact]
GO
ALTER TABLE [Entity].[contactAddress]  WITH CHECK ADD  CONSTRAINT [FK_address_physicalSector] FOREIGN KEY([CountryID], [cityId], [physicalSectorId])
REFERENCES [global].[physicalSector] ([CountryID], [cityId], [physicalSectorId])
GO
ALTER TABLE [Entity].[contactAddress] CHECK CONSTRAINT [FK_address_physicalSector]
GO
ALTER TABLE [Entity].[contactAddress]  WITH CHECK ADD  CONSTRAINT [FK_address_relatedContact] FOREIGN KEY([relatedContactId])
REFERENCES [Entity].[relatedContact] ([relatedContactId])
GO
ALTER TABLE [Entity].[contactAddress] CHECK CONSTRAINT [FK_address_relatedContact]
GO
ALTER TABLE [Entity].[contactAddress]  WITH CHECK ADD  CONSTRAINT [FK_contactAddress_city] FOREIGN KEY([CountryID], [provinceId], [cityId])
REFERENCES [global].[city] ([CountryID], [provinceId], [cityId])
GO
ALTER TABLE [Entity].[contactAddress] CHECK CONSTRAINT [FK_contactAddress_city]
GO
ALTER TABLE [Entity].[contactEmail]  WITH CHECK ADD  CONSTRAINT [FK_contactEmail_relatedContact] FOREIGN KEY([relatedContactId])
REFERENCES [Entity].[relatedContact] ([relatedContactId])
GO
ALTER TABLE [Entity].[contactEmail] CHECK CONSTRAINT [FK_contactEmail_relatedContact]
GO
ALTER TABLE [Entity].[lineCredit]  WITH CHECK ADD  CONSTRAINT [FK_lineCredit_client] FOREIGN KEY([clientId])
REFERENCES [Entity].[client] ([clientId])
GO
ALTER TABLE [Entity].[lineCredit] CHECK CONSTRAINT [FK_lineCredit_client]
GO
ALTER TABLE [Entity].[lineCredit]  WITH CHECK ADD  CONSTRAINT [FK_lineCredit_Company] FOREIGN KEY([companyId])
REFERENCES [Entity].[Company] ([companyId])
GO
ALTER TABLE [Entity].[lineCredit] CHECK CONSTRAINT [FK_lineCredit_Company]
GO
ALTER TABLE [Entity].[lineCredit]  WITH CHECK ADD  CONSTRAINT [FK_lineCredit_Currency] FOREIGN KEY([currencyId])
REFERENCES [global].[Currency] ([currencyId])
GO
ALTER TABLE [Entity].[lineCredit] CHECK CONSTRAINT [FK_lineCredit_Currency]
GO
ALTER TABLE [Entity].[office]  WITH CHECK ADD  CONSTRAINT [FK_office_Company] FOREIGN KEY([companyId])
REFERENCES [Entity].[Company] ([companyId])
GO
ALTER TABLE [Entity].[office] CHECK CONSTRAINT [FK_office_Company]
GO
ALTER TABLE [Entity].[office]  WITH CHECK ADD  CONSTRAINT [FK_office_relatedContact] FOREIGN KEY([relatedContactId])
REFERENCES [Entity].[relatedContact] ([relatedContactId])
GO
ALTER TABLE [Entity].[office] CHECK CONSTRAINT [FK_office_relatedContact]
GO
ALTER TABLE [Entity].[relatedContact]  WITH CHECK ADD  CONSTRAINT [FK_relatedContact_Country] FOREIGN KEY([countryID])
REFERENCES [global].[Country] ([CountryID])
GO
ALTER TABLE [Entity].[relatedContact] CHECK CONSTRAINT [FK_relatedContact_Country]
GO
ALTER TABLE [Entity].[relatedContact]  WITH CHECK ADD  CONSTRAINT [FK_relatedContact_EntityClass] FOREIGN KEY([entityClassId])
REFERENCES [Entity].[EntityClass] ([EntityClassId])
GO
ALTER TABLE [Entity].[relatedContact] CHECK CONSTRAINT [FK_relatedContact_EntityClass]
GO
ALTER TABLE [Entity].[relatedContact]  WITH CHECK ADD  CONSTRAINT [FK_relatedContact_entityRol] FOREIGN KEY([entityRolId])
REFERENCES [Entity].[entityRol] ([entityRolId])
GO
ALTER TABLE [Entity].[relatedContact] CHECK CONSTRAINT [FK_relatedContact_entityRol]
GO
ALTER TABLE [Entity].[relatedContact]  WITH CHECK ADD  CONSTRAINT [FK_relatedContact_identificationType] FOREIGN KEY([identificationTypeId])
REFERENCES [Entity].[identificationType] ([identificationTypeId])
GO
ALTER TABLE [Entity].[relatedContact] CHECK CONSTRAINT [FK_relatedContact_identificationType]
GO
ALTER TABLE [Entity].[relatedContact]  WITH CHECK ADD  CONSTRAINT [FK_relatedContact_MaritalStatus] FOREIGN KEY([maritalStatusId])
REFERENCES [Entity].[MaritalStatus] ([MaritalStatusId])
GO
ALTER TABLE [Entity].[relatedContact] CHECK CONSTRAINT [FK_relatedContact_MaritalStatus]
GO
ALTER TABLE [Entity].[relatedContact]  WITH CHECK ADD  CONSTRAINT [FK_relatedContact_personType] FOREIGN KEY([personTypeId])
REFERENCES [Entity].[personType] ([personTypeId])
GO
ALTER TABLE [Entity].[relatedContact] CHECK CONSTRAINT [FK_relatedContact_personType]
GO
ALTER TABLE [Entity].[relatedContact]  WITH CHECK ADD  CONSTRAINT [FK_relatedContact_Profession] FOREIGN KEY([professionId])
REFERENCES [Entity].[Profession] ([ProfessionId])
GO
ALTER TABLE [Entity].[relatedContact] CHECK CONSTRAINT [FK_relatedContact_Profession]
GO
ALTER TABLE [global].[Country]  WITH CHECK ADD  CONSTRAINT [FK_Country_Currency] FOREIGN KEY([currencyId])
REFERENCES [global].[Currency] ([currencyId])
GO
ALTER TABLE [global].[Country] CHECK CONSTRAINT [FK_Country_Currency]
GO
ALTER TABLE [global].[financialSector]  WITH CHECK ADD  CONSTRAINT [FK_financialSector_accountType] FOREIGN KEY([accountTypeID])
REFERENCES [global].[accountType] ([accountTypeID])
GO
ALTER TABLE [global].[financialSector] CHECK CONSTRAINT [FK_financialSector_accountType]
GO
ALTER TABLE [global].[province]  WITH CHECK ADD  CONSTRAINT [FK_province_Country] FOREIGN KEY([CountryID])
REFERENCES [global].[Country] ([CountryID])
GO
ALTER TABLE [global].[province] CHECK CONSTRAINT [FK_province_Country]
GO
ALTER TABLE [Loan].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_accountType] FOREIGN KEY([accountTypeID])
REFERENCES [global].[accountType] ([accountTypeID])
GO
ALTER TABLE [Loan].[Account] CHECK CONSTRAINT [FK_Account_accountType]
GO
ALTER TABLE [Loan].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_Company] FOREIGN KEY([companyId])
REFERENCES [Entity].[Company] ([companyId])
GO
ALTER TABLE [Loan].[Account] CHECK CONSTRAINT [FK_Account_Company]
GO
ALTER TABLE [Loan].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_ConceptType] FOREIGN KEY([companyId], [ConceptTypeId])
REFERENCES [global].[ConceptType] ([companyId], [ConceptTypeId])
GO
ALTER TABLE [Loan].[Account] CHECK CONSTRAINT [FK_Account_ConceptType]
GO
ALTER TABLE [Loan].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_Currency] FOREIGN KEY([currencyId])
REFERENCES [global].[Currency] ([currencyId])
GO
ALTER TABLE [Loan].[Account] CHECK CONSTRAINT [FK_Account_Currency]
GO
ALTER TABLE [Loan].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_financialSector] FOREIGN KEY([financialSectorId])
REFERENCES [global].[financialSector] ([financialSectorId])
GO
ALTER TABLE [Loan].[Account] CHECK CONSTRAINT [FK_Account_financialSector]
GO
ALTER TABLE [Loan].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_founIncome] FOREIGN KEY([companyId], [founIncomeId])
REFERENCES [global].[founIncome] ([companyId], [founIncomeId])
GO
ALTER TABLE [Loan].[Account] CHECK CONSTRAINT [FK_Account_founIncome]
GO
ALTER TABLE [Loan].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_office] FOREIGN KEY([officeId])
REFERENCES [Entity].[office] ([officeId])
GO
ALTER TABLE [Loan].[Account] CHECK CONSTRAINT [FK_Account_office]
GO
ALTER TABLE [Loan].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_relatedContact] FOREIGN KEY([relatedContactId])
REFERENCES [Entity].[relatedContact] ([relatedContactId])
GO
ALTER TABLE [Loan].[Account] CHECK CONSTRAINT [FK_Account_relatedContact]
GO
ALTER TABLE [Loan].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_relatedContact1] FOREIGN KEY([accountPromoterContactId])
REFERENCES [Entity].[relatedContact] ([relatedContactId])
GO
ALTER TABLE [Loan].[Account] CHECK CONSTRAINT [FK_Account_relatedContact1]
GO
ALTER TABLE [Loan].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_relatedContact2] FOREIGN KEY([accountExecutiveContactId])
REFERENCES [Entity].[relatedContact] ([relatedContactId])
GO
ALTER TABLE [Loan].[Account] CHECK CONSTRAINT [FK_Account_relatedContact2]
GO
ALTER TABLE [Loan].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_serviceProduct] FOREIGN KEY([serviceProductId])
REFERENCES [Loan].[serviceProduct] ([serviceProductId])
GO
ALTER TABLE [Loan].[Account] CHECK CONSTRAINT [FK_Account_serviceProduct]
GO
ALTER TABLE [Loan].[codebtor]  WITH CHECK ADD  CONSTRAINT [FK_codebtor_Account] FOREIGN KEY([accountId])
REFERENCES [Loan].[Account] ([accountId])
GO
ALTER TABLE [Loan].[codebtor] CHECK CONSTRAINT [FK_codebtor_Account]
GO
ALTER TABLE [Loan].[codebtor]  WITH CHECK ADD  CONSTRAINT [FK_codebtor_client] FOREIGN KEY([clientId])
REFERENCES [Entity].[client] ([clientId])
GO
ALTER TABLE [Loan].[codebtor] CHECK CONSTRAINT [FK_codebtor_client]
GO
ALTER TABLE [Loan].[codebtor]  WITH CHECK ADD  CONSTRAINT [FK_codebtor_codebtorType] FOREIGN KEY([codebtorTypeId])
REFERENCES [Loan].[codebtorType] ([codebtorTypeId])
GO
ALTER TABLE [Loan].[codebtor] CHECK CONSTRAINT [FK_codebtor_codebtorType]
GO
ALTER TABLE [Loan].[collateralField]  WITH CHECK ADD  CONSTRAINT [FK_collateralField_immovablePropertyType] FOREIGN KEY([immovablePropertyTypeId])
REFERENCES [Loan].[immovablePropertyType] ([immovablePropertyTypeId])
GO
ALTER TABLE [Loan].[collateralField] CHECK CONSTRAINT [FK_collateralField_immovablePropertyType]
GO
ALTER TABLE [Loan].[committee]  WITH CHECK ADD  CONSTRAINT [FK_committee_Company] FOREIGN KEY([companyId])
REFERENCES [Entity].[Company] ([companyId])
GO
ALTER TABLE [Loan].[committee] CHECK CONSTRAINT [FK_committee_Company]
GO
ALTER TABLE [Loan].[committee]  WITH CHECK ADD  CONSTRAINT [FK_committee_Currency] FOREIGN KEY([currencyId])
REFERENCES [global].[Currency] ([currencyId])
GO
ALTER TABLE [Loan].[committee] CHECK CONSTRAINT [FK_committee_Currency]
GO
ALTER TABLE [Loan].[ConceptGeneralParameter]  WITH CHECK ADD  CONSTRAINT [FK_ConceptGeneralParameter_ConceptType] FOREIGN KEY([companyId], [ConceptTypeId])
REFERENCES [global].[ConceptType] ([companyId], [ConceptTypeId])
GO
ALTER TABLE [Loan].[ConceptGeneralParameter] CHECK CONSTRAINT [FK_ConceptGeneralParameter_ConceptType]
GO
ALTER TABLE [Loan].[ConceptGeneralParameter]  WITH CHECK ADD  CONSTRAINT [FK_ConceptGeneralParameter_creditType] FOREIGN KEY([creditTypeId])
REFERENCES [Loan].[creditType] ([creditTypeId])
GO
ALTER TABLE [Loan].[ConceptGeneralParameter] CHECK CONSTRAINT [FK_ConceptGeneralParameter_creditType]
GO
ALTER TABLE [Loan].[creditFacility]  WITH CHECK ADD  CONSTRAINT [FK_creditFacility_Company] FOREIGN KEY([companyId])
REFERENCES [Entity].[Company] ([companyId])
GO
ALTER TABLE [Loan].[creditFacility] CHECK CONSTRAINT [FK_creditFacility_Company]
GO
ALTER TABLE [Loan].[loanCollateralFieldValue]  WITH CHECK ADD  CONSTRAINT [FK_loanCollateralFieldValue_collateralField] FOREIGN KEY([collateralFieldId])
REFERENCES [Loan].[collateralField] ([collateralFieldId])
GO
ALTER TABLE [Loan].[loanCollateralFieldValue] CHECK CONSTRAINT [FK_loanCollateralFieldValue_collateralField]
GO
ALTER TABLE [Loan].[loanExpense]  WITH CHECK ADD  CONSTRAINT [FK_loanExpense_Account] FOREIGN KEY([accountId])
REFERENCES [Loan].[Account] ([accountId])
GO
ALTER TABLE [Loan].[loanExpense] CHECK CONSTRAINT [FK_loanExpense_Account]
GO
ALTER TABLE [Loan].[loanExpense]  WITH CHECK ADD  CONSTRAINT [FK_loanExpense_Company] FOREIGN KEY([companyId])
REFERENCES [Entity].[Company] ([companyId])
GO
ALTER TABLE [Loan].[loanExpense] CHECK CONSTRAINT [FK_loanExpense_Company]
GO
ALTER TABLE [Loan].[loanExpense]  WITH CHECK ADD  CONSTRAINT [FK_loanExpense_expenseHeader] FOREIGN KEY([companyId], [expenseHeaderId])
REFERENCES [Loan].[expenseHeader] ([companyId], [expenseHeaderId])
GO
ALTER TABLE [Loan].[loanExpense] CHECK CONSTRAINT [FK_loanExpense_expenseHeader]
GO
ALTER TABLE [Loan].[loanExpense]  WITH CHECK ADD  CONSTRAINT [FK_loanExpense_PaymentForm] FOREIGN KEY([paymentFormId])
REFERENCES [Loan].[PaymentForm] ([PaymentFormId])
GO
ALTER TABLE [Loan].[loanExpense] CHECK CONSTRAINT [FK_loanExpense_PaymentForm]
GO
ALTER TABLE [Loan].[loanHeader]  WITH CHECK ADD  CONSTRAINT [FK_loanHeader_Account] FOREIGN KEY([accountId])
REFERENCES [Loan].[Account] ([accountId])
GO
ALTER TABLE [Loan].[loanHeader] CHECK CONSTRAINT [FK_loanHeader_Account]
GO
ALTER TABLE [Loan].[loanHeader]  WITH CHECK ADD  CONSTRAINT [FK_loanHeader_businessLine] FOREIGN KEY([businessLineId])
REFERENCES [global].[businessLine] ([businessLineId])
GO
ALTER TABLE [Loan].[loanHeader] CHECK CONSTRAINT [FK_loanHeader_businessLine]
GO
ALTER TABLE [Loan].[loanHeader]  WITH CHECK ADD  CONSTRAINT [FK_loanHeader_committee] FOREIGN KEY([companyId], [committeeId])
REFERENCES [Loan].[committee] ([companyId], [committeeId])
GO
ALTER TABLE [Loan].[loanHeader] CHECK CONSTRAINT [FK_loanHeader_committee]
GO
ALTER TABLE [Loan].[loanHeader]  WITH CHECK ADD  CONSTRAINT [FK_loanHeader_Company] FOREIGN KEY([companyId])
REFERENCES [Entity].[Company] ([companyId])
GO
ALTER TABLE [Loan].[loanHeader] CHECK CONSTRAINT [FK_loanHeader_Company]
GO
ALTER TABLE [Loan].[loanHeader]  WITH CHECK ADD  CONSTRAINT [FK_loanHeader_ConceptType] FOREIGN KEY([companyId], [ConceptTypeId])
REFERENCES [global].[ConceptType] ([companyId], [ConceptTypeId])
GO
ALTER TABLE [Loan].[loanHeader] CHECK CONSTRAINT [FK_loanHeader_ConceptType]
GO
ALTER TABLE [Loan].[loanHeader]  WITH CHECK ADD  CONSTRAINT [FK_loanHeader_Currency] FOREIGN KEY([currencyId])
REFERENCES [global].[Currency] ([currencyId])
GO
ALTER TABLE [Loan].[loanHeader] CHECK CONSTRAINT [FK_loanHeader_Currency]
GO
ALTER TABLE [Loan].[loanHeader]  WITH CHECK ADD  CONSTRAINT [FK_loanHeader_destinationFund] FOREIGN KEY([destinationFundId])
REFERENCES [global].[destinationFund] ([destinationFundId])
GO
ALTER TABLE [Loan].[loanHeader] CHECK CONSTRAINT [FK_loanHeader_destinationFund]
GO
ALTER TABLE [Loan].[loanHeader]  WITH CHECK ADD  CONSTRAINT [FK_loanHeader_founIncome] FOREIGN KEY([companyId], [founIncomeId])
REFERENCES [global].[founIncome] ([companyId], [founIncomeId])
GO
ALTER TABLE [Loan].[loanHeader] CHECK CONSTRAINT [FK_loanHeader_founIncome]
GO
ALTER TABLE [Loan].[loanHeader]  WITH CHECK ADD  CONSTRAINT [FK_loanHeader_lineCredit] FOREIGN KEY([lineCreditId])
REFERENCES [Entity].[lineCredit] ([lineCreditId])
GO
ALTER TABLE [Loan].[loanHeader] CHECK CONSTRAINT [FK_loanHeader_lineCredit]
GO
ALTER TABLE [Loan].[loanHeader]  WITH CHECK ADD  CONSTRAINT [FK_loanHeader_LoanStatus] FOREIGN KEY([LoanStatusId])
REFERENCES [Loan].[LoanStatus] ([LoanStatusId])
GO
ALTER TABLE [Loan].[loanHeader] CHECK CONSTRAINT [FK_loanHeader_LoanStatus]
GO
ALTER TABLE [Loan].[loanHeader]  WITH CHECK ADD  CONSTRAINT [FK_loanHeader_office] FOREIGN KEY([officeId])
REFERENCES [Entity].[office] ([officeId])
GO
ALTER TABLE [Loan].[loanHeader] CHECK CONSTRAINT [FK_loanHeader_office]
GO
ALTER TABLE [Loan].[loanHeader]  WITH CHECK ADD  CONSTRAINT [FK_loanHeader_PaymentType] FOREIGN KEY([PaymentTypeId])
REFERENCES [Loan].[PaymentType] ([PaymentTypeId])
GO
ALTER TABLE [Loan].[loanHeader] CHECK CONSTRAINT [FK_loanHeader_PaymentType]
GO
ALTER TABLE [Loan].[loanHeader]  WITH CHECK ADD  CONSTRAINT [FK_loanHeader_quotation] FOREIGN KEY([quotationId])
REFERENCES [Loan].[quotation] ([quotationId])
GO
ALTER TABLE [Loan].[loanHeader] CHECK CONSTRAINT [FK_loanHeader_quotation]
GO
ALTER TABLE [Loan].[loanHeader]  WITH CHECK ADD  CONSTRAINT [FK_loanHeader_relatedContact] FOREIGN KEY([relatedContactId])
REFERENCES [Entity].[relatedContact] ([relatedContactId])
GO
ALTER TABLE [Loan].[loanHeader] CHECK CONSTRAINT [FK_loanHeader_relatedContact]
GO
ALTER TABLE [Loan].[loanHeader]  WITH CHECK ADD  CONSTRAINT [FK_loanHeader_SourceIncome] FOREIGN KEY([companyId], [SourceIncomeId])
REFERENCES [global].[SourceIncome] ([companyId], [SourceIncomeId])
GO
ALTER TABLE [Loan].[loanHeader] CHECK CONSTRAINT [FK_loanHeader_SourceIncome]
GO
ALTER TABLE [Loan].[LoanIndicators]  WITH CHECK ADD  CONSTRAINT [FK_LoanIndicators_Account] FOREIGN KEY([accountId])
REFERENCES [Loan].[Account] ([accountId])
GO
ALTER TABLE [Loan].[LoanIndicators] CHECK CONSTRAINT [FK_LoanIndicators_Account]
GO
ALTER TABLE [Loan].[LoanIndicators]  WITH CHECK ADD  CONSTRAINT [FK_LoanIndicators_Reason] FOREIGN KEY([reasonId])
REFERENCES [global].[Reason] ([reasonId])
GO
ALTER TABLE [Loan].[LoanIndicators] CHECK CONSTRAINT [FK_LoanIndicators_Reason]
GO
ALTER TABLE [Loan].[LoanIndicators]  WITH CHECK ADD  CONSTRAINT [FK_LoanIndicators_TypeLoanPayment] FOREIGN KEY([TypeLoanPaymentId])
REFERENCES [Loan].[TypeLoanPayment] ([TypeLoanPaymentId])
GO
ALTER TABLE [Loan].[LoanIndicators] CHECK CONSTRAINT [FK_LoanIndicators_TypeLoanPayment]
GO
ALTER TABLE [Loan].[policyCollateral]  WITH CHECK ADD  CONSTRAINT [FK_policyCollateral_Company] FOREIGN KEY([companyId])
REFERENCES [Entity].[Company] ([companyId])
GO
ALTER TABLE [Loan].[policyCollateral] CHECK CONSTRAINT [FK_policyCollateral_Company]
GO
ALTER TABLE [Loan].[policyCollateral]  WITH CHECK ADD  CONSTRAINT [FK_policyCollateral_policyType] FOREIGN KEY([policyTypeId])
REFERENCES [Loan].[policyType] ([policyTypeId])
GO
ALTER TABLE [Loan].[policyCollateral] CHECK CONSTRAINT [FK_policyCollateral_policyType]
GO
ALTER TABLE [Loan].[policyCollateral]  WITH CHECK ADD  CONSTRAINT [FK_policyCollateral_relatedContact] FOREIGN KEY([relatedContactId])
REFERENCES [Entity].[relatedContact] ([relatedContactId])
GO
ALTER TABLE [Loan].[policyCollateral] CHECK CONSTRAINT [FK_policyCollateral_relatedContact]
GO
ALTER TABLE [Loan].[policyCollateralEndorse]  WITH CHECK ADD  CONSTRAINT [FK_policyCollateralEndorse_policyCollateral] FOREIGN KEY([collateralId], [policyNo])
REFERENCES [Loan].[policyCollateral] ([collateralId], [policyNo])
GO
ALTER TABLE [Loan].[policyCollateralEndorse] CHECK CONSTRAINT [FK_policyCollateralEndorse_policyCollateral]
GO
ALTER TABLE [Loan].[quotation]  WITH CHECK ADD  CONSTRAINT [FK_quotation_businessLine] FOREIGN KEY([businessLineId])
REFERENCES [global].[businessLine] ([businessLineId])
GO
ALTER TABLE [Loan].[quotation] CHECK CONSTRAINT [FK_quotation_businessLine]
GO
ALTER TABLE [Loan].[quotation]  WITH CHECK ADD  CONSTRAINT [FK_quotation_committee] FOREIGN KEY([companyId], [committeeId])
REFERENCES [Loan].[committee] ([companyId], [committeeId])
GO
ALTER TABLE [Loan].[quotation] CHECK CONSTRAINT [FK_quotation_committee]
GO
ALTER TABLE [Loan].[quotation]  WITH CHECK ADD  CONSTRAINT [FK_quotation_Company] FOREIGN KEY([companyId])
REFERENCES [Entity].[Company] ([companyId])
GO
ALTER TABLE [Loan].[quotation] CHECK CONSTRAINT [FK_quotation_Company]
GO
ALTER TABLE [Loan].[quotation]  WITH CHECK ADD  CONSTRAINT [FK_quotation_ConceptType] FOREIGN KEY([companyId], [ConceptTypeId])
REFERENCES [global].[ConceptType] ([companyId], [ConceptTypeId])
GO
ALTER TABLE [Loan].[quotation] CHECK CONSTRAINT [FK_quotation_ConceptType]
GO
ALTER TABLE [Loan].[quotation]  WITH CHECK ADD  CONSTRAINT [FK_quotation_Currency] FOREIGN KEY([currencyId])
REFERENCES [global].[Currency] ([currencyId])
GO
ALTER TABLE [Loan].[quotation] CHECK CONSTRAINT [FK_quotation_Currency]
GO
ALTER TABLE [Loan].[quotation]  WITH CHECK ADD  CONSTRAINT [FK_quotation_destinationFund] FOREIGN KEY([destinationFundId])
REFERENCES [global].[destinationFund] ([destinationFundId])
GO
ALTER TABLE [Loan].[quotation] CHECK CONSTRAINT [FK_quotation_destinationFund]
GO
ALTER TABLE [Loan].[quotation]  WITH CHECK ADD  CONSTRAINT [FK_quotation_financialSector] FOREIGN KEY([financialSectorId])
REFERENCES [global].[financialSector] ([financialSectorId])
GO
ALTER TABLE [Loan].[quotation] CHECK CONSTRAINT [FK_quotation_financialSector]
GO
ALTER TABLE [Loan].[quotation]  WITH CHECK ADD  CONSTRAINT [FK_quotation_founIncome] FOREIGN KEY([companyId], [founIncomeId])
REFERENCES [global].[founIncome] ([companyId], [founIncomeId])
GO
ALTER TABLE [Loan].[quotation] CHECK CONSTRAINT [FK_quotation_founIncome]
GO
ALTER TABLE [Loan].[quotation]  WITH CHECK ADD  CONSTRAINT [FK_quotation_office] FOREIGN KEY([officeId])
REFERENCES [Entity].[office] ([officeId])
GO
ALTER TABLE [Loan].[quotation] CHECK CONSTRAINT [FK_quotation_office]
GO
ALTER TABLE [Loan].[quotation]  WITH CHECK ADD  CONSTRAINT [FK_quotation_quotationPaymentType] FOREIGN KEY([quotationPaymentTypeId])
REFERENCES [Loan].[PaymentType] ([PaymentTypeId])
GO
ALTER TABLE [Loan].[quotation] CHECK CONSTRAINT [FK_quotation_quotationPaymentType]
GO
ALTER TABLE [Loan].[quotation]  WITH CHECK ADD  CONSTRAINT [FK_quotation_quotationStatus] FOREIGN KEY([quotationStatusId])
REFERENCES [Loan].[quotationStatus] ([quotationStatusId])
GO
ALTER TABLE [Loan].[quotation] CHECK CONSTRAINT [FK_quotation_quotationStatus]
GO
ALTER TABLE [Loan].[quotation]  WITH CHECK ADD  CONSTRAINT [FK_quotation_quotationType] FOREIGN KEY([quotationTypeId])
REFERENCES [Loan].[quotationType] ([quotationTypeId])
GO
ALTER TABLE [Loan].[quotation] CHECK CONSTRAINT [FK_quotation_quotationType]
GO
ALTER TABLE [Loan].[quotation]  WITH CHECK ADD  CONSTRAINT [FK_quotation_relatedContact] FOREIGN KEY([relatedContactId])
REFERENCES [Entity].[relatedContact] ([relatedContactId])
GO
ALTER TABLE [Loan].[quotation] CHECK CONSTRAINT [FK_quotation_relatedContact]
GO
ALTER TABLE [Loan].[quotation]  WITH CHECK ADD  CONSTRAINT [FK_quotation_relatedContact1] FOREIGN KEY([accountExecutiveContactId])
REFERENCES [Entity].[relatedContact] ([relatedContactId])
GO
ALTER TABLE [Loan].[quotation] CHECK CONSTRAINT [FK_quotation_relatedContact1]
GO
ALTER TABLE [Loan].[quotation]  WITH CHECK ADD  CONSTRAINT [FK_quotation_relatedContact2] FOREIGN KEY([accountPromoterContactId])
REFERENCES [Entity].[relatedContact] ([relatedContactId])
GO
ALTER TABLE [Loan].[quotation] CHECK CONSTRAINT [FK_quotation_relatedContact2]
GO
ALTER TABLE [Loan].[quotation]  WITH CHECK ADD  CONSTRAINT [FK_quotation_serviceProduct] FOREIGN KEY([serviceProductId])
REFERENCES [Loan].[serviceProduct] ([serviceProductId])
GO
ALTER TABLE [Loan].[quotation] CHECK CONSTRAINT [FK_quotation_serviceProduct]
GO
ALTER TABLE [Loan].[quotation]  WITH CHECK ADD  CONSTRAINT [FK_quotation_SourceIncome] FOREIGN KEY([companyId], [SourceIncomeId])
REFERENCES [global].[SourceIncome] ([companyId], [SourceIncomeId])
GO
ALTER TABLE [Loan].[quotation] CHECK CONSTRAINT [FK_quotation_SourceIncome]
GO
ALTER TABLE [Loan].[serviceProduct]  WITH CHECK ADD  CONSTRAINT [FK_serviceProduct_accountType] FOREIGN KEY([accountTypeID])
REFERENCES [global].[accountType] ([accountTypeID])
GO
ALTER TABLE [Loan].[serviceProduct] CHECK CONSTRAINT [FK_serviceProduct_accountType]
GO
ALTER TABLE [Loan].[transactionDefinition]  WITH CHECK ADD  CONSTRAINT [FK_transactionDefinition_accountType] FOREIGN KEY([accountTypeID])
REFERENCES [global].[accountType] ([accountTypeID])
GO
ALTER TABLE [Loan].[transactionDefinition] CHECK CONSTRAINT [FK_transactionDefinition_accountType]
GO
ALTER TABLE [Loan].[transactionPaymentPlan]  WITH CHECK ADD  CONSTRAINT [FK_transactionPaymentPlan_Account] FOREIGN KEY([accountId])
REFERENCES [Loan].[Account] ([accountId])
GO
ALTER TABLE [Loan].[transactionPaymentPlan] CHECK CONSTRAINT [FK_transactionPaymentPlan_Account]
GO
ALTER TABLE [Loan].[transactionPaymentPlan]  WITH CHECK ADD  CONSTRAINT [FK_transactionPaymentPlan_Company] FOREIGN KEY([companyId])
REFERENCES [Entity].[Company] ([companyId])
GO
ALTER TABLE [Loan].[transactionPaymentPlan] CHECK CONSTRAINT [FK_transactionPaymentPlan_Company]
GO
ALTER TABLE [Loan].[transactionPaymentPlan]  WITH CHECK ADD  CONSTRAINT [FK_transactionPaymentPlan_office] FOREIGN KEY([officeId])
REFERENCES [Entity].[office] ([officeId])
GO
ALTER TABLE [Loan].[transactionPaymentPlan] CHECK CONSTRAINT [FK_transactionPaymentPlan_office]
GO
ALTER TABLE [Loan].[transactionReceipt]  WITH CHECK ADD  CONSTRAINT [FK_transactionReceipt_Account] FOREIGN KEY([accountId])
REFERENCES [Loan].[Account] ([accountId])
GO
ALTER TABLE [Loan].[transactionReceipt] CHECK CONSTRAINT [FK_transactionReceipt_Account]
GO
ALTER TABLE [Loan].[transactionReceipt]  WITH CHECK ADD  CONSTRAINT [FK_transactionReceipt_Company] FOREIGN KEY([companyId])
REFERENCES [Entity].[Company] ([companyId])
GO
ALTER TABLE [Loan].[transactionReceipt] CHECK CONSTRAINT [FK_transactionReceipt_Company]
GO
ALTER TABLE [Loan].[transactionReceipt]  WITH CHECK ADD  CONSTRAINT [FK_transactionReceipt_office] FOREIGN KEY([officeId])
REFERENCES [Entity].[office] ([officeId])
GO
ALTER TABLE [Loan].[transactionReceipt] CHECK CONSTRAINT [FK_transactionReceipt_office]
GO
ALTER TABLE [Loan].[transactionReceipt]  WITH CHECK ADD  CONSTRAINT [FK_transactionReceipt_relatedContact] FOREIGN KEY([relatedContactId])
REFERENCES [Entity].[relatedContact] ([relatedContactId])
GO
ALTER TABLE [Loan].[transactionReceipt] CHECK CONSTRAINT [FK_transactionReceipt_relatedContact]
GO
ALTER TABLE [Loan].[transactionReceipt]  WITH CHECK ADD  CONSTRAINT [FK_transactionReceipt_transactionDefinition] FOREIGN KEY([transactionDefinitionId])
REFERENCES [Loan].[transactionDefinition] ([transactionDefinitionId])
GO
ALTER TABLE [Loan].[transactionReceipt] CHECK CONSTRAINT [FK_transactionReceipt_transactionDefinition]
GO
ALTER TABLE [Loan].[transactionReceipt]  WITH CHECK ADD  CONSTRAINT [FK_transactionReceipt_transactionPaymentForm] FOREIGN KEY([transactionPaymentFormId])
REFERENCES [Loan].[transactionPaymentForm] ([transactionPaymentFormId])
GO
ALTER TABLE [Loan].[transactionReceipt] CHECK CONSTRAINT [FK_transactionReceipt_transactionPaymentForm]
GO
ALTER TABLE [Loan].[transactionReceiptDetail]  WITH CHECK ADD  CONSTRAINT [FK_transactionReceiptDetail_transactionPaymentPlan] FOREIGN KEY([transactionPaymentPlanId])
REFERENCES [Loan].[transactionPaymentPlan] ([transactionPaymentPlanId])
GO
ALTER TABLE [Loan].[transactionReceiptDetail] CHECK CONSTRAINT [FK_transactionReceiptDetail_transactionPaymentPlan]
GO
ALTER TABLE [Loan].[transactionReceiptDetail]  WITH CHECK ADD  CONSTRAINT [FK_transactionReceiptDetail_transactionReceipt] FOREIGN KEY([transactionReceiptId])
REFERENCES [Loan].[transactionReceipt] ([transactionReceiptId])
GO
ALTER TABLE [Loan].[transactionReceiptDetail] CHECK CONSTRAINT [FK_transactionReceiptDetail_transactionReceipt]
GO
ALTER TABLE [Loan].[transactionReceiptDetail]  WITH CHECK ADD  CONSTRAINT [FK_transactionReceiptDetail_transactionSinal] FOREIGN KEY([transactionSinalId])
REFERENCES [Loan].[transactionSinal] ([transactionSinalId])
GO
ALTER TABLE [Loan].[transactionReceiptDetail] CHECK CONSTRAINT [FK_transactionReceiptDetail_transactionSinal]
GO
ALTER TABLE [Recaudo].[Archivo]  WITH CHECK ADD FOREIGN KEY([EstatusID])
REFERENCES [Recaudo].[Estatus] ([EstatusID])
GO
ALTER TABLE [Recaudo].[CardNetBatchTrailer]  WITH CHECK ADD  CONSTRAINT [FK_CardNetBatchTrailer_CardNetBatchTrailer] FOREIGN KEY([ArchivoID])
REFERENCES [Recaudo].[CardNetBatchTrailer] ([ArchivoID])
GO
ALTER TABLE [Recaudo].[CardNetBatchTrailer] CHECK CONSTRAINT [FK_CardNetBatchTrailer_CardNetBatchTrailer]
GO
ALTER TABLE [Recaudo].[CardNetBatchTrailer]  WITH CHECK ADD  CONSTRAINT [FK_CardNetBatchTrailer_CardNetBatchTrailer1] FOREIGN KEY([ArchivoID])
REFERENCES [Recaudo].[CardNetBatchTrailer] ([ArchivoID])
GO
ALTER TABLE [Recaudo].[CardNetBatchTrailer] CHECK CONSTRAINT [FK_CardNetBatchTrailer_CardNetBatchTrailer1]
GO
ALTER TABLE [Recaudo].[CobroAutomatico]  WITH CHECK ADD FOREIGN KEY([ArchivoID])
REFERENCES [Recaudo].[Archivo] ([ArchivoID])
GO
ALTER TABLE [Recaudo].[LogPagos]  WITH CHECK ADD  CONSTRAINT [FK_LogPagos_Pago] FOREIGN KEY([PagoID])
REFERENCES [Recaudo].[Pago] ([PagoID])
GO
ALTER TABLE [Recaudo].[LogPagos] CHECK CONSTRAINT [FK_LogPagos_Pago]
GO
ALTER TABLE [Recaudo].[PreNotificacion]  WITH CHECK ADD FOREIGN KEY([ArchivoID])
REFERENCES [Recaudo].[Archivo] ([ArchivoID])
GO
ALTER TABLE [Recaudo].[RecConciliacion]  WITH CHECK ADD FOREIGN KEY([ArchivoID])
REFERENCES [Recaudo].[Archivo] ([ArchivoID])
GO
ALTER TABLE [Recaudo].[RecReferenciado]  WITH CHECK ADD FOREIGN KEY([ArchivoID])
REFERENCES [Recaudo].[Archivo] ([ArchivoID])
GO
ALTER TABLE [Recaudo].[RecReferenciado]  WITH CHECK ADD FOREIGN KEY([EstatusID])
REFERENCES [Recaudo].[Estatus] ([EstatusID])
GO
ALTER TABLE [SMS].[SMSQueue]  WITH CHECK ADD  CONSTRAINT [FK_SMSQueue_SMSQueue1] FOREIGN KEY([SMSStatusID])
REFERENCES [SMS].[SMSStatus] ([SMSStatusID])
GO
ALTER TABLE [SMS].[SMSQueue] CHECK CONSTRAINT [FK_SMSQueue_SMSQueue1]
GO
ALTER TABLE [SMS].[SMSQueue]  WITH CHECK ADD  CONSTRAINT [FK_SMSQueue_SMSSource] FOREIGN KEY([SMSSourceID])
REFERENCES [SMS].[SMSSource] ([SMSSourceID])
GO
ALTER TABLE [SMS].[SMSQueue] CHECK CONSTRAINT [FK_SMSQueue_SMSSource]
GO
ALTER TABLE [SMS].[SMSQueue]  WITH CHECK ADD  CONSTRAINT [FK_SMSQueue_SMSType] FOREIGN KEY([SMSTypeID])
REFERENCES [SMS].[SMSType] ([SMSTypeID])
GO
ALTER TABLE [SMS].[SMSQueue] CHECK CONSTRAINT [FK_SMSQueue_SMSType]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tipo de tipo de negocio su equivalente es  eclitnem' , @level0type=N'SCHEMA',@level0name=N'Entity', @level1type=N'TABLE',@level1name=N'businessType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de clientes su equivalente es  eclientm' , @level0type=N'SCHEMA',@level0name=N'Entity', @level1type=N'TABLE',@level1name=N'client'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tipo de clientes definidos por la compañia su equivalente es eclitipm' , @level0type=N'SCHEMA',@level0name=N'Entity', @level1type=N'TABLE',@level1name=N'clientTypeByCompany'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tipo cliente by sib su equivalente es  eclisibm' , @level0type=N'SCHEMA',@level0name=N'Entity', @level1type=N'TABLE',@level1name=N'clientTypeBySIB'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Frecuencia de inspección 0-Diario 1-Mensual 2-Anual' , @level0type=N'SCHEMA',@level0name=N'Entity', @level1type=N'TABLE',@level1name=N'collateral', @level2type=N'COLUMN',@level2name=N'inspectionFrequency'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de garantia asociada a un cliente eadmgarm' , @level0type=N'SCHEMA',@level0name=N'Entity', @level1type=N'TABLE',@level1name=N'collateral'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de compañias su equivalente en easybank es  xadmciam' , @level0type=N'SCHEMA',@level0name=N'Entity', @level1type=N'TABLE',@level1name=N'Company'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Direcciones del contacto su equivalente es  xadmdird' , @level0type=N'SCHEMA',@level0name=N'Entity', @level1type=N'TABLE',@level1name=N'contactAddress'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Correo del contacto su equivalente es xadmurld' , @level0type=N'SCHEMA',@level0name=N'Entity', @level1type=N'TABLE',@level1name=N'contactEmail'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de telefonos su equivalente es  xadmteld' , @level0type=N'SCHEMA',@level0name=N'Entity', @level1type=N'TABLE',@level1name=N'contactPhones'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla del rol que tiene un relacionado su equivalente en easybank es  xadmrolm' , @level0type=N'SCHEMA',@level0name=N'Entity', @level1type=N'TABLE',@level1name=N'entityRol'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'tabla de tipo identificacion su equivalente es  xadmidem' , @level0type=N'SCHEMA',@level0name=N'Entity', @level1type=N'TABLE',@level1name=N'identificationType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID Línea de crédito' , @level0type=N'SCHEMA',@level0name=N'Entity', @level1type=N'TABLE',@level1name=N'lineCredit', @level2type=N'COLUMN',@level2name=N'lineCreditId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Compañía' , @level0type=N'SCHEMA',@level0name=N'Entity', @level1type=N'TABLE',@level1name=N'lineCredit', @level2type=N'COLUMN',@level2name=N'companyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Linea de credito a cliente su equivalente es ecuelinm' , @level0type=N'SCHEMA',@level0name=N'Entity', @level1type=N'TABLE',@level1name=N'lineCredit'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'tipo Vinculado su equivalente es eclivinm' , @level0type=N'SCHEMA',@level0name=N'Entity', @level1type=N'TABLE',@level1name=N'linkedType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tipo de persona ' , @level0type=N'SCHEMA',@level0name=N'Entity', @level1type=N'TABLE',@level1name=N'personType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de Profesiones su equivalente en easybank es xadmprom' , @level0type=N'SCHEMA',@level0name=N'Entity', @level1type=N'TABLE',@level1name=N'Profession'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de datos de contactos relacionados su equivalente en easybank xadmrelm y xadmrelr' , @level0type=N'SCHEMA',@level0name=N'Entity', @level1type=N'TABLE',@level1name=N'relatedContact'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Linea de Negocios su equivalente eadmlinm' , @level0type=N'SCHEMA',@level0name=N'global', @level1type=N'TABLE',@level1name=N'businessLine'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de ciudades su equivalente es  xadmcium' , @level0type=N'SCHEMA',@level0name=N'global', @level1type=N'TABLE',@level1name=N'city'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de tipo de conceptos su equivalente es  eadmconm' , @level0type=N'SCHEMA',@level0name=N'global', @level1type=N'TABLE',@level1name=N'ConceptType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de Paises su equivalente en easybank es  xadmpaim' , @level0type=N'SCHEMA',@level0name=N'global', @level1type=N'TABLE',@level1name=N'Country'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de moneda su equivalente en easybank is xadmmonm' , @level0type=N'SCHEMA',@level0name=N'global', @level1type=N'TABLE',@level1name=N'Currency'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de destino del fondo su equivalente es epredesm' , @level0type=N'SCHEMA',@level0name=N'global', @level1type=N'TABLE',@level1name=N'destinationFund'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'tabla destino de credito SIB su equivalente es eadmdesm' , @level0type=N'SCHEMA',@level0name=N'global', @level1type=N'TABLE',@level1name=N'destinationFundSIBI'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sectores financieros su equivalente es  eadmsetm' , @level0type=N'SCHEMA',@level0name=N'global', @level1type=N'TABLE',@level1name=N'financialSector'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de provincia su equivalente province' , @level0type=N'SCHEMA',@level0name=N'global', @level1type=N'TABLE',@level1name=N'province'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de motivos que tiene el sistema su equivalente es eadmmotm' , @level0type=N'SCHEMA',@level0name=N'global', @level1type=N'TABLE',@level1name=N'Reason'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'fuente de ingresos su equivalente eprefuem' , @level0type=N'SCHEMA',@level0name=N'global', @level1type=N'TABLE',@level1name=N'SourceIncome'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de cuentas su equivalente es  ecuectam' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'Account'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'estatus de las cuentas ' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'accountStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tipo de asociacion' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'codebtorType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Características de las garantías  eadmcgam' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'collateralField'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de tipo de garantia ' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'collateralType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tipo garantia segun SIB su equivalente es eadmtgam' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'collateralTypeSIB'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de comite su equivalente es  eprecomm' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'committee'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Código del concepto' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'ConceptGeneralParameter', @level2type=N'COLUMN',@level2name=N'ConceptTypeId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Código del tipo de credito' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'ConceptGeneralParameter', @level2type=N'COLUMN',@level2name=N'creditTypeId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Monto mínimo aprobacion' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'ConceptGeneralParameter', @level2type=N'COLUMN',@level2name=N'minAmountApproval'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Monto máximo aprobación' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'ConceptGeneralParameter', @level2type=N'COLUMN',@level2name=N'maxAmountApproval'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Plazo mínimo' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'ConceptGeneralParameter', @level2type=N'COLUMN',@level2name=N'minLoanTerm'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Plazo máximo' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'ConceptGeneralParameter', @level2type=N'COLUMN',@level2name=N'maxLoanTerm'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Máximo días gracia mora' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'ConceptGeneralParameter', @level2type=N'COLUMN',@level2name=N'maxGraceDays'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Señal del Plazo  (Días, Meses)' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'ConceptGeneralParameter', @level2type=N'COLUMN',@level2name=N'IsLoanTermInDays'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Señal condonación comisión' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'ConceptGeneralParameter', @level2type=N'COLUMN',@level2name=N'condonationCommission'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Saldo para cálculo intereses' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'ConceptGeneralParameter', @level2type=N'COLUMN',@level2name=N'RateCalculation'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Saldo para cálculo de mora' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'ConceptGeneralParameter', @level2type=N'COLUMN',@level2name=N'RateLateCalculation'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Parametros Generales(Conceptos)' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'ConceptGeneralParameter'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de Facilidad Crediticia de Prestamos su equivalente es eprefacm' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'creditFacility'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tipo de creditos esquivalente es epretipm' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'creditType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Maestro de Gastos eadmgasm' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'expenseHeader'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de Tipos de inmueble eadmtinm' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'immovablePropertyType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Relacion Garantia - prestamo su equivalente es epreprer' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'loancollateral'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Características por garantía eadmcgar' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'loanCollateralFieldValue'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Relación de gastos para cada cuenta de los diferentes tipos de cuenta eadmgasr' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'loanExpense'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'id de la cuenta' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'loanHeader', @level2type=N'COLUMN',@level2name=N'accountId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'admcia_codigo' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'loanHeader', @level2type=N'COLUMN',@level2name=N'companyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'admsuc_codigo' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'loanHeader', @level2type=N'COLUMN',@level2name=N'officeId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'presol_numero' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'loanHeader', @level2type=N'COLUMN',@level2name=N'quotationId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'client_codigo' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'loanHeader', @level2type=N'COLUMN',@level2name=N'relatedContactId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'admfue_codigo' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'loanHeader', @level2type=N'COLUMN',@level2name=N'founIncomeId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Código del tipo' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'loanHeader', @level2type=N'COLUMN',@level2name=N'businessLineId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cantidad de Prórrogas' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'loanHeader', @level2type=N'COLUMN',@level2name=N'numberExtension'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha en que el prestamo fue saldado' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'loanHeader', @level2type=N'COLUMN',@level2name=N'PaidOffDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de Vencimiento Saldo Vencimiento del prestamo' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'loanHeader', @level2type=N'COLUMN',@level2name=N'expirationDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Saldo(insoluto)' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'loanHeader', @level2type=N'COLUMN',@level2name=N'OutstandingBalance'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de prestamo emcabezado su equivalente es epreprem' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'loanHeader'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id de la Cuenta' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'LoanIndicators', @level2type=N'COLUMN',@level2name=N'accountId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tipo de saldo del prestamo  1-Normal 2-Abono 3-Renovación 4-Adjudicacion' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'LoanIndicators', @level2type=N'COLUMN',@level2name=N'TypeLoanPaymentId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id de motivo de saldo' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'LoanIndicators', @level2type=N'COLUMN',@level2name=N'reasonId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Ind. de cuotas reprogramadas.' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'LoanIndicators', @level2type=N'COLUMN',@level2name=N'quotaId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Indicador de si está en legal' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'LoanIndicators', @level2type=N'COLUMN',@level2name=N'IsLegal'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0-No suspenso 1-Suspenso' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'LoanIndicators', @level2type=N'COLUMN',@level2name=N'IsSuspense'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Indicador de que el préstamo está renovado' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'LoanIndicators', @level2type=N'COLUMN',@level2name=N'IsRenewal'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Prestamo fue traspasado (TRUE/FALSE)' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'LoanIndicators', @level2type=N'COLUMN',@level2name=N'ISLoanTransferred'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'El prestamo se encuentra en cobranza judicial (TRUE/FALSE)' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'LoanIndicators', @level2type=N'COLUMN',@level2name=N'IsJudicialCollection'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'El prestamo se reporto en legal para TransUnion' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'LoanIndicators', @level2type=N'COLUMN',@level2name=N'IsLegalTransunion'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cada cuantos meses el prestamo es repreciado' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'LoanIndicators', @level2type=N'COLUMN',@level2name=N'monthToRepriced'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de primer desembolso' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'LoanIndicators', @level2type=N'COLUMN',@level2name=N'disbursementDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de ultimo reprecio' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'LoanIndicators', @level2type=N'COLUMN',@level2name=N'LastDateRepriced'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'No. ultima cuota generada' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'LoanIndicators', @level2type=N'COLUMN',@level2name=N'LastQuotaGenerated'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha en que el prestamo fue saldado' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'LoanIndicators', @level2type=N'COLUMN',@level2name=N'nextReviewRate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Ultima fecha devengo.' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'LoanIndicators', @level2type=N'COLUMN',@level2name=N'datePaidoff'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla con los indicadores de préstamos su equivalente es   epreindm' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'LoanIndicators'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estatus del prestamo ' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'LoanStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Forma de pago' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'PaymentForm'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Polizas por Garantías eadmpolm' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'policyCollateral'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Relacion Prestamos poliza Relacion endoso-prestamo-poliza eadmpolr' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'policyCollateralEndorse'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de Tipo de Pólizas eadmtpom' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'policyType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de solicitud o cotizacion de prestamo su equivalente es ' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'quotation'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tipo de cotizacion' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'quotationType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de Tipos de productos y/o servicios.  Tabla 78.0 SIB su equivalente es eadmtprm' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'serviceProduct'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de definición de transacciones  eadmctrm' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'transactionDefinition'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tipo de pago de la transaccion' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'transactionPaymentForm'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Plan de Pago (cuotas generadas de un préstamo)  eprecuom' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'transactionPaymentPlan'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Motivo de la transaccion' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'transactionReason'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Detalle de transacciones de Recibo de Ingreso (detalle)  epretrxd' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'transactionReceiptDetail'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Señales de la transaccion' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'transactionSinal'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tipo de saldo del prestamo' , @level0type=N'SCHEMA',@level0name=N'Loan', @level1type=N'TABLE',@level1name=N'TypeLoanPayment'
GO
