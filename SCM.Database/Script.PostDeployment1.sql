/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

INSERT INTO dbo.Centers VALUES (1, N'Sons of Zouheir Ghreiwati', N'SY0005050012S');

INSERT INTO dbo.Departments VALUES (N'Undefined');
INSERT INTO dbo.Departments VALUES (N'AC + Electronice');
INSERT INTO dbo.Departments VALUES (N'HA');
INSERT INTO dbo.Departments VALUES (N'Carry-In Repair');
INSERT INTO dbo.Departments VALUES (N'Agents');

SET IDENTITY_INSERT dbo.Tags ON;
INSERT INTO DBO.Tags (Id, TagType, Name, Format) VALUES (1, N'C', N'General End User', N'label-default');
INSERT INTO DBO.Tags (Id, TagType, Name, Format) VALUES (2, N'C', N'Dealer', N'label-info');
INSERT INTO DBO.Tags (Id, TagType, Name, Format) VALUES (3, N'C', N'VIP', N'label-success');
INSERT INTO DBO.Tags (Id, TagType, Name, Format) VALUES (4, N'R', N'In-Home Service', N'label-default');
INSERT INTO DBO.Tags (Id, TagType, Name, Format) VALUES (5, N'R', N'Carry-In Service', N'label-primary');
INSERT INTO DBO.Tags (Id, TagType, Name, Format) VALUES (6, N'R', N'Installation', N'label-default');
INSERT INTO DBO.Tags (Id, TagType, Name, Format) VALUES (7, N'R', N'HM', N'label-default');
INSERT INTO DBO.Tags (Id, TagType, Name, Format) VALUES (8, N'R', N'In Warranty', N'label-primary');
INSERT INTO DBO.Tags (Id, TagType, Name, Format) VALUES (9, N'R', N'Out of Warranty', N'label-warning');
INSERT INTO DBO.Tags (Id, TagType, Name, Format) VALUES (10, N'R', N'Extended Card', N'label-warning');
INSERT INTO DBO.Tags (Id, TagType, Name, Format) VALUES (11, N'R', N'DOA', N'label-warning');
SET IDENTITY_INSERT dbo.Tags OFF;

-- Reports

INSERT [dbo].[Reports] ([Name], [Description], [Title], [SubTitle], [Command], [FilterByDate], [FilterByCustomer], [FilterByDepartment], [FilterByEngineer], [FilterByProduct], [FilterByStatus], [FilterByCity], [FilterByRegion], [FilterByTag], [FilterByText1], [FilterByText2], [FilterByNumber1], [FilterByNumber2], [FilterByDate1], [FilterByDate2], [Text1Title], [Text2Title], [Number1Title], [Number2Title], [Date1Title], [Date2Title], [GVisible], [GHeader], [F1Visible], [F1Header], [F1Map], [F1Color], [F1Total], [F2Visible], [F2Header], [F2Map], [F2Color], [F2Total], [F3Visible], [F3Header], [F3Map], [F3Color], [F3Total], [F4Visible], [F4Header], [F4Map], [F4Color], [F4Total], [F5Visible], [F5Header], [F5Map], [F5Color], [F5Total], [F6Visible], [F6Header], [F6Map], [F6Color], [F6Total], [F7Visible], [F7Header], [F7Map], [F7Color], [F7Total], [F8Visible], [F8Header], [F8Map], [F8Color], [F8Total]) VALUES (N'Cities', N'List of all cities', N'Cities', NULL, N'select  ''*'' [G], Name [T1] from Cities', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'Text1', N'Text 2', N'Number 1', N'Number 2', N'Date 1', N'Date 2', 0, N'Group Name', 1, N'City Name', N'T1', N'Black', N'Count', 0, NULL, NULL, N'Black', NULL, 0, NULL, NULL, N'Black', NULL, 0, NULL, NULL, N'Black', NULL, 0, NULL, NULL, N'Black', NULL, 0, NULL, NULL, N'Black', NULL, 0, NULL, NULL, N'Black', NULL, 0, NULL, NULL, N'Black', NULL)
INSERT [dbo].[Reports] ([Name], [Description], [Title], [SubTitle], [Command], [FilterByDate], [FilterByCustomer], [FilterByDepartment], [FilterByEngineer], [FilterByProduct], [FilterByStatus], [FilterByCity], [FilterByRegion], [FilterByTag], [FilterByText1], [FilterByText2], [FilterByNumber1], [FilterByNumber2], [FilterByDate1], [FilterByDate2], [Text1Title], [Text2Title], [Number1Title], [Number2Title], [Date1Title], [Date2Title], [GVisible], [GHeader], [F1Visible], [F1Header], [F1Map], [F1Color], [F1Total], [F2Visible], [F2Header], [F2Map], [F2Color], [F2Total], [F3Visible], [F3Header], [F3Map], [F3Color], [F3Total], [F4Visible], [F4Header], [F4Map], [F4Color], [F4Total], [F5Visible], [F5Header], [F5Map], [F5Color], [F5Total], [F6Visible], [F6Header], [F6Map], [F6Color], [F6Total], [F7Visible], [F7Header], [F7Map], [F7Color], [F7Total], [F8Visible], [F8Header], [F8Map], [F8Color], [F8Total]) VALUES (N'Products List', N'List of products', N'Products List', NULL, N'select ''*'' [G], Name [T1], Id [T2] from Products order by Name', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'Text 1', N'Text 2', N'Number 1', N'Number 2', N'Date 1', N'Date 2', 0, N'*', 1, N'Product', N'T1', N'Black', N'Count', 1, N'Code', N'T2', N'Black', NULL, 0, NULL, NULL, N'Black', NULL, 0, NULL, NULL, N'Black', NULL, 0, NULL, NULL, N'Black', NULL, 0, NULL, NULL, N'Black', NULL, 0, NULL, NULL, N'Black', NULL, 0, NULL, NULL, N'Black', NULL)

INSERT [dbo].[Reports] ([Name], [Description], [Title], [SubTitle], [Command], [FilterByDate], [FilterByCustomer], [FilterByDepartment], [FilterByEngineer], [FilterByProduct], [FilterByStatus], [FilterByCity], [FilterByRegion], [FilterByTag], [FilterByText1], [FilterByText2], [FilterByNumber1], [FilterByNumber2], [FilterByDate1], [FilterByDate2], [Text1Title], [Text2Title], [Number1Title], [Number2Title], [Date1Title], [Date2Title], [GVisible], [GHeader], [F1Visible], [F1Header], [F1Map], [F1Color], [F1Total], [F2Visible], [F2Header], [F2Map], [F2Color], [F2Total], [F3Visible], [F3Header], [F3Map], [F3Color], [F3Total], [F4Visible], [F4Header], [F4Map], [F4Color], [F4Total], [F5Visible], [F5Header], [F5Map], [F5Color], [F5Total], [F6Visible], [F6Header], [F6Map], [F6Color], [F6Total], [F7Visible], [F7Header], [F7Map], [F7Color], [F7Total], [F8Visible], [F8Header], [F8Map], [F8Color], [F8Total]) VALUES (N'Address Book', N'List of all customers and thier contacts and locations', N'Address Book', N'Customer Contacts and Location', N'select c.Name [G], cu.Name [T1], r.Name [T2], cu.Phone [T3], cu.Mobile [T4], cu.Address [T8]
from Customers cu 
	left outer join Cities c on cu.CityId = c.Id
	left outer join Regions r on cu.RegionId = r.Id
	where cu.Name Like ''%$Text1%'' 
	and (''$CityId'' = '''' or cu.CityId = Cast(''$CityId'' as int))
	and (''$RegionId'' = '''' or cu.RegionId = Cast(''$RegionId'' as int))
order by c.Name, cu.Name
', 0, 0, 0, 0, 0, 0, 1, 1, 0, 1, 0, 0, 0, 0, 0, N'Customer Name', N'Text 2', N'Number 1', N'Number 2', N'Date 1', N'Date 2', 1, N'City', 1, N'Customer Name', N'T1', N'Black', N'Count', 1, N'Region', N'T2', N'Black', NULL, 1, N'Phone', N'T3', N'Black', NULL, 0, N'Mobile', NULL, N'Black', NULL, 0, NULL, NULL, N'Black', NULL, 0, NULL, NULL, N'Black', NULL, 0, NULL, NULL, N'Black', NULL, 1, N'Address', N'T8', N'Black', NULL)

INSERT [dbo].[Reports] ([Name], [Description], [Title], [SubTitle], [Command], [FilterByDate], [FilterByCustomer], [FilterByDepartment], [FilterByEngineer], [FilterByProduct], [FilterByStatus], [FilterByCity], [FilterByRegion], [FilterByTag], [FilterByText1], [FilterByText2], [FilterByNumber1], [FilterByNumber2], [FilterByDate1], [FilterByDate2], [Text1Title], [Text2Title], [Number1Title], [Number2Title], [Date1Title], [Date2Title], [GVisible], [GHeader], [F1Visible], [F1Header], [F1Map], [F1Color], [F1Total], [F2Visible], [F2Header], [F2Map], [F2Color], [F2Total], [F3Visible], [F3Header], [F3Map], [F3Color], [F3Total], [F4Visible], [F4Header], [F4Map], [F4Color], [F4Total], [F5Visible], [F5Header], [F5Map], [F5Color], [F5Total], [F6Visible], [F6Header], [F6Map], [F6Color], [F6Total], [F7Visible], [F7Header], [F7Map], [F7Color], [F7Total], [F8Visible], [F8Header], [F8Map], [F8Color], [F8Total]) VALUES (N'City Status Report', N'Statistics by status', N'Status Report', N'by city', N'
select
	''*'' [G] ,
	c.Name [T1], 
	Sum((case when sr.StatusId = 10 then 1 else 0 end)) [N1],
	Sum((case when sr.StatusId = 20 then 1 else 0 end)) [N2],
	Sum((case when sr.StatusId = 90 then 1 else 0 end)) [N3],
	Sum((case when sr.StatusId = 100 then 1 else 0 end)) [N4]

from ServiceRequests sr
left outer join Customers cu on sr.CustomerId = cu.Id
left outer join Cities c on cu.CityId = c.Id
left outer join Regions r on cu.RegionId = r.Id
left outer join RequestTags rt on rt.ServiceRequestId = sr.Id
left outer join CustomerTags ct on ct.CustomerId = cu.Id

where 
	(sr.RequestDate between $FromDate and $ToDate)
	and (sr.StatusId in $Status)
	and (''$CustomerId'' = '''' or sr.CustomerId = Cast(''$CustomerId'' as int))
	and (''$CityId'' = '''' or cu.CityId = Cast(''$CityId'' as int))
	and (''$RegionId'' = '''' or cu.RegionId = Cast(''$RegionId'' as int))
	and (''$DepartmentId'' = '''' or sr.DepartmentId = Cast(''$DepartmentId'' as int))
	and (''$EngineerId'' = '''' or sr.EngineerId = Cast(''$EngineerId'' as int))
	and (''$ProductId'' = '''' or sr.ProductId = ''$DepartmentId'')
	and (ct.TagId in $Tags or rt.TagId in $Tags)
group by c.Name
having 	
	(Sum((case when sr.StatusId = 10 then 1 else 0 end)) +
	Sum((case when sr.StatusId = 20 then 1 else 0 end)) +
	Sum((case when sr.StatusId = 90 then 1 else 0 end)) +
	Sum((case when sr.StatusId = 100 then 1 else 0 end)) ) > 0

order by c.Name

', 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, N'Text 1', N'Text 2', N'Number 1', N'Number 2', N'Date 1', N'Date 2', 0, N'*', 1, N'City', N'T1', N'Black', N'Count', 0, NULL, NULL, N'Black', NULL, 1, N'Open', N'N1', N'Blue', N'Sum', 1, N'Pending', N'N2', N'Red', N'Sum', 1, N'Cancelled', N'N3', N'Maroon', N'Sum', 1, N'Closed', N'N4', N'DarkGreen', N'Sum', 0, NULL, NULL, N'Black', NULL, 0, NULL, NULL, N'Black', NULL)

INSERT [dbo].[Reports] ([Name], [Description], [Title], [SubTitle], [Command], [FilterByDate], [FilterByCustomer], [FilterByDepartment], [FilterByEngineer], [FilterByProduct], [FilterByStatus], [FilterByCity], [FilterByRegion], [FilterByTag], [FilterByText1], [FilterByText2], [FilterByNumber1], [FilterByNumber2], [FilterByDate1], [FilterByDate2], [Text1Title], [Text2Title], [Number1Title], [Number2Title], [Date1Title], [Date2Title], [GVisible], [GHeader], [F1Visible], [F1Header], [F1Map], [F1Color], [F1Total], [F2Visible], [F2Header], [F2Map], [F2Color], [F2Total], [F3Visible], [F3Header], [F3Map], [F3Color], [F3Total], [F4Visible], [F4Header], [F4Map], [F4Color], [F4Total], [F5Visible], [F5Header], [F5Map], [F5Color], [F5Total], [F6Visible], [F6Header], [F6Map], [F6Color], [F6Total], [F7Visible], [F7Header], [F7Map], [F7Color], [F7Total], [F8Visible], [F8Header], [F8Map], [F8Color], [F8Total]) VALUES (N'Department Status Report', N'Statistics by department', N'Status Report', N'by department', N'
select
	''*'' [G] ,
	d.Name [T1], 
	Sum((case when sr.StatusId = 10 then 1 else 0 end)) [N1],
	Sum((case when sr.StatusId = 20 then 1 else 0 end)) [N2],
	Sum((case when sr.StatusId = 90 then 1 else 0 end)) [N3],
	Sum((case when sr.StatusId = 100 then 1 else 0 end)) [N4]

from ServiceRequests sr
left outer join Customers cu on sr.CustomerId = cu.Id
left outer join Departments d on sr.DepartmentId = d.Id
left outer join Cities c on cu.CityId = c.Id
left outer join Regions r on cu.RegionId = r.Id
left outer join RequestTags rt on rt.ServiceRequestId = sr.Id
left outer join CustomerTags ct on ct.CustomerId = cu.Id

where 
	(sr.RequestDate between $FromDate and $ToDate)
	and (sr.StatusId in $Status)
	and (''$CustomerId'' = '''' or sr.CustomerId = Cast(''$CustomerId'' as int))
	and (''$CityId'' = '''' or cu.CityId = Cast(''$CityId'' as int))
	and (''$RegionId'' = '''' or cu.RegionId = Cast(''$RegionId'' as int))
	and (''$DepartmentId'' = '''' or sr.DepartmentId = Cast(''$DepartmentId'' as int))
	and (''$EngineerId'' = '''' or sr.EngineerId = Cast(''$EngineerId'' as int))
	and (''$ProductId'' = '''' or sr.ProductId = ''$DepartmentId'')
	and (ct.TagId in $Tags or rt.TagId in $Tags)
group by d.Name
having 	
	(Sum((case when sr.StatusId = 10 then 1 else 0 end)) +
	Sum((case when sr.StatusId = 20 then 1 else 0 end)) +
	Sum((case when sr.StatusId = 90 then 1 else 0 end)) +
	Sum((case when sr.StatusId = 100 then 1 else 0 end)) ) > 0

order by d.Name

', 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, N'Text 1', N'Text 2', N'Number 1', N'Number 2', N'Date 1', N'Date 2', 0, N'*', 1, N'Department', N'T1', N'Black', N'Count', 0, NULL, NULL, N'Black', NULL, 1, N'Open', N'N1', N'Blue', N'Sum', 1, N'Pending', N'N2', N'Red', N'Sum', 1, N'Cancelled', N'N3', N'Maroon', N'Sum', 1, N'Closed', N'N4', N'DarkGreen', N'Sum', 0, NULL, NULL, N'Black', NULL, 0, NULL, NULL, N'Black', NULL)
INSERT [dbo].[Reports] ([Name], [Description], [Title], [SubTitle], [Command], [FilterByDate], [FilterByCustomer], [FilterByDepartment], [FilterByEngineer], [FilterByProduct], [FilterByStatus], [FilterByCity], [FilterByRegion], [FilterByTag], [FilterByText1], [FilterByText2], [FilterByNumber1], [FilterByNumber2], [FilterByDate1], [FilterByDate2], [Text1Title], [Text2Title], [Number1Title], [Number2Title], [Date1Title], [Date2Title], [GVisible], [GHeader], [F1Visible], [F1Header], [F1Map], [F1Color], [F1Total], [F2Visible], [F2Header], [F2Map], [F2Color], [F2Total], [F3Visible], [F3Header], [F3Map], [F3Color], [F3Total], [F4Visible], [F4Header], [F4Map], [F4Color], [F4Total], [F5Visible], [F5Header], [F5Map], [F5Color], [F5Total], [F6Visible], [F6Header], [F6Map], [F6Color], [F6Total], [F7Visible], [F7Header], [F7Map], [F7Color], [F7Total], [F8Visible], [F8Header], [F8Map], [F8Color], [F8Total]) VALUES (N'Engineer Status Report', N'Statistics by engineer', N'Status Report', N'by engineer', N'
select
	''*'' [G] ,
	e.Name [T1], 
	Sum((case when sr.StatusId = 10 then 1 else 0 end)) [N1],
	Sum((case when sr.StatusId = 20 then 1 else 0 end)) [N2],
	Sum((case when sr.StatusId = 90 then 1 else 0 end)) [N3],
	Sum((case when sr.StatusId = 100 then 1 else 0 end)) [N4]

from ServiceRequests sr
left outer join Customers cu on sr.CustomerId = cu.Id
left outer join Engineers e on sr.EngineertId = e.Id
left outer join Cities c on cu.CityId = c.Id
left outer join Regions r on cu.RegionId = r.Id
left outer join RequestTags rt on rt.ServiceRequestId = sr.Id
left outer join CustomerTags ct on ct.CustomerId = cu.Id

where 
	(sr.RequestDate between $FromDate and $ToDate)
	and (sr.StatusId in $Status)
	and (''$CustomerId'' = '''' or sr.CustomerId = Cast(''$CustomerId'' as int))
	and (''$CityId'' = '''' or cu.CityId = Cast(''$CityId'' as int))
	and (''$RegionId'' = '''' or cu.RegionId = Cast(''$RegionId'' as int))
	and (''$DepartmentId'' = '''' or sr.DepartmentId = Cast(''$DepartmentId'' as int))
	and (''$EngineerId'' = '''' or sr.EngineerId = Cast(''$EngineerId'' as int))
	and (''$ProductId'' = '''' or sr.ProductId = ''$DepartmentId'')
	and (ct.TagId in $Tags or rt.TagId in $Tags)
group by e.Name
having 	
	(Sum((case when sr.StatusId = 10 then 1 else 0 end)) +
	Sum((case when sr.StatusId = 20 then 1 else 0 end)) +
	Sum((case when sr.StatusId = 90 then 1 else 0 end)) +
	Sum((case when sr.StatusId = 100 then 1 else 0 end)) ) > 0

order by e.Name

', 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, N'Text 1', N'Text 2', N'Number 1', N'Number 2', N'Date 1', N'Date 2', 0, N'*', 1, N'Engineer', N'T1', N'Black', N'Count', 0, NULL, NULL, N'Black', NULL, 1, N'Open', N'N1', N'Blue', N'Sum', 1, N'Pending', N'N2', N'Red', N'Sum', 1, N'Cancelled', N'N3', N'Maroon', N'Sum', 1, N'Closed', N'N4', N'DarkGreen', N'Sum', 0, NULL, NULL, N'Black', NULL, 0, NULL, NULL, N'Black', NULL)
INSERT [dbo].[Reports] ([Name], [Description], [Title], [SubTitle], [Command], [FilterByDate], [FilterByCustomer], [FilterByDepartment], [FilterByEngineer], [FilterByProduct], [FilterByStatus], [FilterByCity], [FilterByRegion], [FilterByTag], [FilterByText1], [FilterByText2], [FilterByNumber1], [FilterByNumber2], [FilterByDate1], [FilterByDate2], [Text1Title], [Text2Title], [Number1Title], [Number2Title], [Date1Title], [Date2Title], [GVisible], [GHeader], [F1Visible], [F1Header], [F1Map], [F1Color], [F1Total], [F2Visible], [F2Header], [F2Map], [F2Color], [F2Total], [F3Visible], [F3Header], [F3Map], [F3Color], [F3Total], [F4Visible], [F4Header], [F4Map], [F4Color], [F4Total], [F5Visible], [F5Header], [F5Map], [F5Color], [F5Total], [F6Visible], [F6Header], [F6Map], [F6Color], [F6Total], [F7Visible], [F7Header], [F7Map], [F7Color], [F7Total], [F8Visible], [F8Header], [F8Map], [F8Color], [F8Total]) VALUES (N'Product Status Report', N'Statistics by product', N'Status Report', N'by product', N'
select
	''*'' [G] ,
	p.Name [T1], 
	Sum((case when sr.StatusId = 10 then 1 else 0 end)) [N1],
	Sum((case when sr.StatusId = 20 then 1 else 0 end)) [N2],
	Sum((case when sr.StatusId = 90 then 1 else 0 end)) [N3],
	Sum((case when sr.StatusId = 100 then 1 else 0 end)) [N4]

from ServiceRequests sr
left outer join Customers cu on sr.CustomerId = cu.Id
left outer join Products p on sr.ProductId = p.Id
left outer join Cities c on cu.CityId = c.Id
left outer join Regions r on cu.RegionId = r.Id
left outer join RequestTags rt on rt.ServiceRequestId = sr.Id
left outer join CustomerTags ct on ct.CustomerId = cu.Id

where 
	(sr.RequestDate between $FromDate and $ToDate)
	and (sr.StatusId in $Status)
	and (''$CustomerId'' = '''' or sr.CustomerId = Cast(''$CustomerId'' as int))
	and (''$CityId'' = '''' or cu.CityId = Cast(''$CityId'' as int))
	and (''$RegionId'' = '''' or cu.RegionId = Cast(''$RegionId'' as int))
	and (''$DepartmentId'' = '''' or sr.DepartmentId = Cast(''$DepartmentId'' as int))
	and (''$EngineerId'' = '''' or sr.EngineerId = Cast(''$EngineerId'' as int))
	and (''$ProductId'' = '''' or sr.ProductId = ''$DepartmentId'')
	and (ct.TagId in $Tags or rt.TagId in $Tags)
	and (sr.Model Like ''%$Text1%'')
	and (sr.SN Like ''%$Text2%'')
group by p.Name
having 	
	(Sum((case when sr.StatusId = 10 then 1 else 0 end)) +
	Sum((case when sr.StatusId = 20 then 1 else 0 end)) +
	Sum((case when sr.StatusId = 90 then 1 else 0 end)) +
	Sum((case when sr.StatusId = 100 then 1 else 0 end)) ) > 0

order by p.Name

', 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, N'Model', N'S/N', N'Number 1', N'Number 2', N'Date 1', N'Date 2', 0, N'*', 1, N'Product', N'T1', N'Black', N'Count', 0, NULL, NULL, N'Black', NULL, 1, N'Open', N'N1', N'Blue', N'Sum', 1, N'Pending', N'N2', N'Red', N'Sum', 1, N'Cancelled', N'N3', N'Maroon', N'Sum', 1, N'Closed', N'N4', N'DarkGreen', N'Sum', 0, NULL, NULL, N'Black', NULL, 0, NULL, NULL, N'Black', NULL)
INSERT [dbo].[Reports] ([Name], [Description], [Title], [SubTitle], [Command], [FilterByDate], [FilterByCustomer], [FilterByDepartment], [FilterByEngineer], [FilterByProduct], [FilterByStatus], [FilterByCity], [FilterByRegion], [FilterByTag], [FilterByText1], [FilterByText2], [FilterByNumber1], [FilterByNumber2], [FilterByDate1], [FilterByDate2], [Text1Title], [Text2Title], [Number1Title], [Number2Title], [Date1Title], [Date2Title], [GVisible], [GHeader], [F1Visible], [F1Header], [F1Map], [F1Color], [F1Total], [F2Visible], [F2Header], [F2Map], [F2Color], [F2Total], [F3Visible], [F3Header], [F3Map], [F3Color], [F3Total], [F4Visible], [F4Header], [F4Map], [F4Color], [F4Total], [F5Visible], [F5Header], [F5Map], [F5Color], [F5Total], [F6Visible], [F6Header], [F6Map], [F6Color], [F6Total], [F7Visible], [F7Header], [F7Map], [F7Color], [F7Total], [F8Visible], [F8Header], [F8Map], [F8Color], [F8Total]) VALUES ( N'City Requests Report', N'Request details by city', N'Requests Report', N'by city', N'
select
	distinct c.Name [G] ,
	cu.Name [T1], 
	e.Name [T2],
	p.Name [T3],
	case 
	when sr.StatusId = 10 then ''Open'' 
	when sr.StatusId = 20 then ''Pending'' 
	when sr.StatusId = 90 then ''Cancelled''
	when sr.StatusId = 100 then ''Closed'' end [T4],
	sr.RequestDate [D1],
	sr.StatusDate [D2],
	sr.Id [N1]

from ServiceRequests sr
left outer join Customers cu on sr.CustomerId = cu.Id
left outer join Engineers e on sr.EngineerId = e.Id
left outer join Products p on sr.ProductId = p.Id 
left outer join Cities c on cu.CityId = c.Id
left outer join Regions r on cu.RegionId = r.Id
left outer join RequestTags rt on rt.ServiceRequestId = sr.Id
left outer join CustomerTags ct on ct.CustomerId = cu.Id

where 
	(sr.RequestDate between $FromDate and $ToDate)
	and (sr.StatusId in $Status)
	and (''$CustomerId'' = '''' or sr.CustomerId = Cast(''$CustomerId'' as int))
	and (''$CityId'' = '''' or cu.CityId = Cast(''$CityId'' as int))
	and (''$RegionId'' = '''' or cu.RegionId = Cast(''$RegionId'' as int))
	and (''$DepartmentId'' = '''' or sr.DepartmentId = Cast(''$DepartmentId'' as int))
	and (''$EngineerId'' = '''' or sr.EngineerId = Cast(''$EngineerId'' as int))
	and (''$ProductId'' = '''' or sr.ProductId = ''$DepartmentId'')
	and (ct.TagId in $Tags or rt.TagId in $Tags)

order by c.Name, sr.RequestDate

', 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, N'Text 1', N'Text 2', N'Number 1', N'Number 2', N'Date 1', N'Date 2', 1, N'City', 1, N'Customer', N'T1', N'Black', N'Count', 1, N'Engineer', N'T2', N'Black', NULL, 1, N'Product', N'T3', N'Black', NULL, 1, N'Status', N'T4', N'Maroon', NULL, 1, N'Request On', N'D1', N'Black', NULL, 1, N'Status On', N'D2', N'Blue', NULL, 1, N'No.', N'N1', N'Black', NULL, 0, NULL, NULL, N'Black', NULL)
INSERT [dbo].[Reports] ([Name], [Description], [Title], [SubTitle], [Command], [FilterByDate], [FilterByCustomer], [FilterByDepartment], [FilterByEngineer], [FilterByProduct], [FilterByStatus], [FilterByCity], [FilterByRegion], [FilterByTag], [FilterByText1], [FilterByText2], [FilterByNumber1], [FilterByNumber2], [FilterByDate1], [FilterByDate2], [Text1Title], [Text2Title], [Number1Title], [Number2Title], [Date1Title], [Date2Title], [GVisible], [GHeader], [F1Visible], [F1Header], [F1Map], [F1Color], [F1Total], [F2Visible], [F2Header], [F2Map], [F2Color], [F2Total], [F3Visible], [F3Header], [F3Map], [F3Color], [F3Total], [F4Visible], [F4Header], [F4Map], [F4Color], [F4Total], [F5Visible], [F5Header], [F5Map], [F5Color], [F5Total], [F6Visible], [F6Header], [F6Map], [F6Color], [F6Total], [F7Visible], [F7Header], [F7Map], [F7Color], [F7Total], [F8Visible], [F8Header], [F8Map], [F8Color], [F8Total]) VALUES (N'Department Requests Report', N'Request details by department', N'Requests Report', N'by department', N'
select
	distinct d.Name [G] ,
	cu.Name [T1], 
	e.Name [T2],
	p.Name [T3],
	case 
	when sr.StatusId = 10 then ''Open'' 
	when sr.StatusId = 20 then ''Pending'' 
	when sr.StatusId = 90 then ''Cancelled''
	when sr.StatusId = 100 then ''Closed'' end [T4],
	sr.RequestDate [D1],
	sr.StatusDate [D2],
	sr.Id [N1]

from ServiceRequests sr
left outer join Customers cu on sr.CustomerId = cu.Id
left outer join Departments d on sr.DepartmentId = d.Id
left outer join Engineers e on sr.EngineerId = e.Id
left outer join Products p on sr.ProductId = p.Id 
left outer join Cities c on cu.CityId = c.Id
left outer join Regions r on cu.RegionId = r.Id
left outer join RequestTags rt on rt.ServiceRequestId = sr.Id
left outer join CustomerTags ct on ct.CustomerId = cu.Id

where 
	(sr.RequestDate between $FromDate and $ToDate)
	and (sr.StatusId in $Status)
	and (''$CustomerId'' = '''' or sr.CustomerId = Cast(''$CustomerId'' as int))
	and (''$CityId'' = '''' or cu.CityId = Cast(''$CityId'' as int))
	and (''$RegionId'' = '''' or cu.RegionId = Cast(''$RegionId'' as int))
	and (''$DepartmentId'' = '''' or sr.DepartmentId = Cast(''$DepartmentId'' as int))
	and (''$EngineerId'' = '''' or sr.EngineerId = Cast(''$EngineerId'' as int))
	and (''$ProductId'' = '''' or sr.ProductId = ''$DepartmentId'')
	and (ct.TagId in $Tags or rt.TagId in $Tags)

order by d.Name, sr.RequestDate

', 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, N'Text 1', N'Text 2', N'Number 1', N'Number 2', N'Date 1', N'Date 2', 1, N'Department', 1, N'Customer', N'T1', N'Black', N'Count', 1, N'Engineer', N'T2', N'Black', NULL, 1, N'Product', N'T3', N'Black', NULL, 1, N'Status', N'T4', N'Maroon', NULL, 1, N'Request On', N'D1', N'Black', NULL, 1, N'Status On', N'D2', N'Blue', NULL, 1, N'No.', N'N1', N'Black', NULL, 0, NULL, NULL, N'Black', NULL)
INSERT [dbo].[Reports] ([Name], [Description], [Title], [SubTitle], [Command], [FilterByDate], [FilterByCustomer], [FilterByDepartment], [FilterByEngineer], [FilterByProduct], [FilterByStatus], [FilterByCity], [FilterByRegion], [FilterByTag], [FilterByText1], [FilterByText2], [FilterByNumber1], [FilterByNumber2], [FilterByDate1], [FilterByDate2], [Text1Title], [Text2Title], [Number1Title], [Number2Title], [Date1Title], [Date2Title], [GVisible], [GHeader], [F1Visible], [F1Header], [F1Map], [F1Color], [F1Total], [F2Visible], [F2Header], [F2Map], [F2Color], [F2Total], [F3Visible], [F3Header], [F3Map], [F3Color], [F3Total], [F4Visible], [F4Header], [F4Map], [F4Color], [F4Total], [F5Visible], [F5Header], [F5Map], [F5Color], [F5Total], [F6Visible], [F6Header], [F6Map], [F6Color], [F6Total], [F7Visible], [F7Header], [F7Map], [F7Color], [F7Total], [F8Visible], [F8Header], [F8Map], [F8Color], [F8Total]) VALUES (N'Engineer Request Report', N'Request details by engineer', N'Requests Report', N'by engineer', N'
select
	distinct e.Name [G] ,
	cu.Name [T1], 
	e.Name [T2],
	p.Name [T3],
	case 
	when sr.StatusId = 10 then ''Open'' 
	when sr.StatusId = 20 then ''Pending'' 
	when sr.StatusId = 90 then ''Cancelled''
	when sr.StatusId = 100 then ''Closed'' end [T4],
	sr.RequestDate [D1],
	sr.StatusDate [D2],
	sr.Id [N1]

from ServiceRequests sr
left outer join Customers cu on sr.CustomerId = cu.Id
left outer join Engineers e on sr.EngineerId = e.Id
left outer join Products p on sr.ProductId = p.Id 
left outer join Cities c on cu.CityId = c.Id
left outer join Regions r on cu.RegionId = r.Id
left outer join RequestTags rt on rt.ServiceRequestId = sr.Id
left outer join CustomerTags ct on ct.CustomerId = cu.Id

where 
	(sr.RequestDate between $FromDate and $ToDate)
	and (sr.StatusId in $Status)
	and (''$CustomerId'' = '''' or sr.CustomerId = Cast(''$CustomerId'' as int))
	and (''$CityId'' = '''' or cu.CityId = Cast(''$CityId'' as int))
	and (''$RegionId'' = '''' or cu.RegionId = Cast(''$RegionId'' as int))
	and (''$DepartmentId'' = '''' or sr.DepartmentId = Cast(''$DepartmentId'' as int))
	and (''$EngineerId'' = '''' or sr.EngineerId = Cast(''$EngineerId'' as int))
	and (''$ProductId'' = '''' or sr.ProductId = ''$DepartmentId'')
	and (ct.TagId in $Tags or rt.TagId in $Tags)

order by e.Name, sr.RequestDate

', 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, N'Text 1', N'Text 2', N'Number 1', N'Number 2', N'Date 1', N'Date 2', 1, N'Engineer', 1, N'Customer', N'T1', N'Black', N'Count', 0, NULL, NULL, N'Black', NULL, 1, N'Product', N'T3', N'Black', NULL, 1, N'Status', N'T4', N'Maroon', NULL, 1, N'Request On', N'D1', N'Black', NULL, 1, N'Status On', N'D2', N'Blue', NULL, 1, N'No.', N'N1', N'Black', NULL, 0, NULL, NULL, N'Black', NULL)
INSERT [dbo].[Reports] ([Name], [Description], [Title], [SubTitle], [Command], [FilterByDate], [FilterByCustomer], [FilterByDepartment], [FilterByEngineer], [FilterByProduct], [FilterByStatus], [FilterByCity], [FilterByRegion], [FilterByTag], [FilterByText1], [FilterByText2], [FilterByNumber1], [FilterByNumber2], [FilterByDate1], [FilterByDate2], [Text1Title], [Text2Title], [Number1Title], [Number2Title], [Date1Title], [Date2Title], [GVisible], [GHeader], [F1Visible], [F1Header], [F1Map], [F1Color], [F1Total], [F2Visible], [F2Header], [F2Map], [F2Color], [F2Total], [F3Visible], [F3Header], [F3Map], [F3Color], [F3Total], [F4Visible], [F4Header], [F4Map], [F4Color], [F4Total], [F5Visible], [F5Header], [F5Map], [F5Color], [F5Total], [F6Visible], [F6Header], [F6Map], [F6Color], [F6Total], [F7Visible], [F7Header], [F7Map], [F7Color], [F7Total], [F8Visible], [F8Header], [F8Map], [F8Color], [F8Total]) VALUES (N'Product Requests Report', N'Request details by product', N'Requests Report', N'by product', N'
select
	distinct p.Name [G] ,
	cu.Name [T1], 
	e.Name [T2],
	p.Name [T3],
	case 
	when sr.StatusId = 10 then ''Open'' 
	when sr.StatusId = 20 then ''Pending'' 
	when sr.StatusId = 90 then ''Cancelled''
	when sr.StatusId = 100 then ''Closed'' end [T4],
	sr.RequestDate [D1],
	sr.StatusDate [D2],
	sr.Id [N1]

from ServiceRequests sr
left outer join Customers cu on sr.CustomerId = cu.Id
left outer join Engineers e on sr.EngineerId = e.Id
left outer join Products p on sr.ProductId = p.Id 
left outer join Cities c on cu.CityId = c.Id
left outer join Regions r on cu.RegionId = r.Id
left outer join RequestTags rt on rt.ServiceRequestId = sr.Id
left outer join CustomerTags ct on ct.CustomerId = cu.Id

where 
	(sr.RequestDate between $FromDate and $ToDate)
	and (sr.StatusId in $Status)
	and (''$CustomerId'' = '''' or sr.CustomerId = Cast(''$CustomerId'' as int))
	and (''$CityId'' = '''' or cu.CityId = Cast(''$CityId'' as int))
	and (''$RegionId'' = '''' or cu.RegionId = Cast(''$RegionId'' as int))
	and (''$DepartmentId'' = '''' or sr.DepartmentId = Cast(''$DepartmentId'' as int))
	and (''$EngineerId'' = '''' or sr.EngineerId = Cast(''$EngineerId'' as int))
	and (''$ProductId'' = '''' or sr.ProductId = ''$DepartmentId'')
	and (ct.TagId in $Tags or rt.TagId in $Tags)

order by p.Name, sr.RequestDate

', 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, N'Text 1', N'Text 2', N'Number 1', N'Number 2', N'Date 1', N'Date 2', 1, N'Product', 1, N'Customer', N'T1', N'Black', N'Count', 1, N'Engineer', N'T2', N'Black', NULL, 0, NULL, NULL, N'Black', NULL, 1, N'Status', N'T4', N'Maroon', NULL, 1, N'Request On', N'D1', N'Black', NULL, 1, N'Status On', N'D2', N'Blue', NULL, 1, N'No.', N'N1', N'Black', NULL, 0, NULL, NULL, N'Black', NULL)
INSERT [dbo].[Reports] ([Name], [Description], [Title], [SubTitle], [Command], [FilterByDate], [FilterByCustomer], [FilterByDepartment], [FilterByEngineer], [FilterByProduct], [FilterByStatus], [FilterByCity], [FilterByRegion], [FilterByTag], [FilterByText1], [FilterByText2], [FilterByNumber1], [FilterByNumber2], [FilterByDate1], [FilterByDate2], [Text1Title], [Text2Title], [Number1Title], [Number2Title], [Date1Title], [Date2Title], [GVisible], [GHeader], [F1Visible], [F1Header], [F1Map], [F1Color], [F1Total], [F2Visible], [F2Header], [F2Map], [F2Color], [F2Total], [F3Visible], [F3Header], [F3Map], [F3Color], [F3Total], [F4Visible], [F4Header], [F4Map], [F4Color], [F4Total], [F5Visible], [F5Header], [F5Map], [F5Color], [F5Total], [F6Visible], [F6Header], [F6Map], [F6Color], [F6Total], [F7Visible], [F7Header], [F7Map], [F7Color], [F7Total], [F8Visible], [F8Header], [F8Map], [F8Color], [F8Total]) VALUES (N'Customer Duplication', N'Capture duplication of customer names', N'Customer Duplication', NULL, N'select cu.Name [G], 
	c.Name [T2], r.Name [T3], cu.Phone [T4], cu.Mobile [T5], cu.Address [T8]
from Customers cu 
	left outer join Cities c on cu.CityId = c.Id
	left outer join Regions r on cu.RegionId = r.Id
where
	(''$CityId'' = '''' or cu.CityId = Cast(''$CityId'' as int))
	and (''$RegionId'' = '''' or cu.RegionId = Cast(''$RegionId'' as int))
    and (cu.Name in (select Name from Customers group by Name Having count(Id) > 1)) 
order by cu.Name
', 0, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, N'Text 1', N'Text 2', N'Number 1', N'Number 2', N'Date 1', N'Date 2', 1, N'Customer', 0, N'F1', N'T1', N'Black', N'Count', 1, N'City', N'T2', N'Black', NULL, 1, N'Region', N'T3', N'Black', NULL, 1, N'Phone', N'T4', N'Black', NULL, 1, N'Mobile', N'T5', N'Black', NULL, 0, NULL, NULL, N'Black', NULL, 0, NULL, NULL, N'Black', NULL, 1, N'Address', N'T8', N'Black', NULL)
