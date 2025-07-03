CREATE TABLE EmailTemplates (
    EmailTemplateId BIGINT PRIMARY KEY IDENTITY (1, 1),
    TemplateName NVARCHAR (255) NOT NULL,
    Subject NVARCHAR (255) NOT NULL,
    Body NVARCHAR (MAX) NOT NULL,
    IsHTML BIT NOT NULL,
    FromAddress NVARCHAR (255) NOT NULL,
    FromDisplayName NVARCHAR (255) NOT NULL,
    IsActive BIT NOT NULL,
    UpdatedDate DATETIME NOT NULL,
    UpdatedBy NVARCHAR (128) NOT NULL
);
