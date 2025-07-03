CREATE TABLE EmailActivities (
    EmailActivityId BIGINT PRIMARY KEY IDENTITY (1, 1),
    EmailTemplateId BIGINT NOT NULL,
    UserId BIGINT NOT NULL,
    ToAddress NVARCHAR (255) NOT NULL,
    Body NVARCHAR (MAX) NOT NULL,
    SentStatus bit NOT NULL,
    SentDate DATETIME NOT NULL
);
