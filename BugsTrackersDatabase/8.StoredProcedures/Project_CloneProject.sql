SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO



CREATE PROCEDURE Project_CloneProject 
(
  @projectID INT,
  @projectName NVarChar(256)
)
AS
-- Copy Project
INSERT Projects
(
  ProjectName,
  ProjectDescription,
  ProjectCreatorId,
  ProjectManagerId
)
SELECT
  @ProjectName,
  ProjectDescription,
  ProjectCreatorId,
  ProjectManagerId
FROM 
  Projects
WHERE
  ProjectId = @ProjectId
  
DECLARE @NewProjectId INT
SET @NewProjectId = @@IDENTITY

-- Copy Milestones
INSERT ProjectMilestones
(
  ProjectId,
  MilestoneName,
  MilestoneImageUrl
)
SELECT
  @NewProjectId,
  MilestoneName,
  MilestoneImageUrl
FROM
  ProjectMilestones
WHERE
  ProjectId = @ProjectID  

-- Copy Priorities
INSERT ProjectPriorities
(
  ProjectId,
  PriorityName,
  PriorityImageUrl
)
SELECT
  @NewProjectId,
  PriorityName,
  PriorityImageUrl
FROM
  ProjectPriorities
WHERE
  ProjectId = @ProjectID   

-- Copy Status
INSERT ProjectStatus
(
  ProjectId,
  StatusName,
  StatusImageUrl
)
SELECT
  @NewProjectId,
  StatusName,
  StatusImageUrl
FROM
  ProjectStatus
WHERE
  ProjectId = @ProjectID

-- Copy Project Members
INSERT ProjectMembers
(
  UserId,
  ProjectId
)
SELECT
  UserId,
  @NewProjectId
FROM
  ProjectMembers
WHERE
  ProjectId = @ProjectId

-- Copy Custom Fields
INSERT ProjectCustomFields
(
  ProjectId,
  CustomFieldName,
  CustomFieldRequired,
  CustomFieldDataType
)
SELECT
  @NewProjectId,
  CustomFieldName,
  CustomFieldRequired,
  CustomFieldDataType
FROM
  ProjectCustomFields
WHERE
  ProjectId = @ProjectId



-- Copy Categories
INSERT ProjectCategories
(
  ProjectId,
  CategoryName,
  ParentCategoryId
)
SELECT
  @NewProjectId,
  CategoryName,
  ParentCategoryID
FROM
  ProjectCategories
WHERE
  ProjectId = @ProjectId  


CREATE TABLE #OldCategories
(
  OldRowNumber INT IDENTITY,
  OldCategoryId INT,
)

INSERT #OldCategories
(
  OldCategoryId
)
SELECT
  CategoryId
FROM
  ProjectCategories
WHERE
  ProjectId = @ProjectId
ORDER BY CategoryId

CREATE TABLE #NewCategories
(
  NewRowNumber INT IDENTITY,
  NewCategoryId INT,
)

INSERT #NewCategories
(
  NewCategoryId
)
SELECT
  CategoryId
FROM
  ProjectCategories
WHERE
  ProjectId = @NewProjectId
ORDER BY CategoryId


UPDATE ProjectCategories SET
  ParentCategoryId = NewCategoryID
FROM
  #OldCategories INNER JOIN #NewCategories ON OldRowNumber = NewRowNumber
WHERE
  ProjectId = @NewProjectId
  And ParentCategoryID = OldCategoryId


GO
