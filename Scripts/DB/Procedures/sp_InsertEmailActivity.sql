ALTER PROCEDURE sp_InsertEmailActivity
    @EmailTemplateId BIGINT,
    @UserId BIGINT,
    @ToAddress NVARCHAR(255),
    @Body NVARCHAR(MAX),
    @SentStatus BIT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @SP_Name VARCHAR(128) = 'sp_InsertEmailActivity';
    DECLARE @Success BIT = 1;
    DECLARE @ActionTaken NVARCHAR(500) = NULL;
    DECLARE @AffectedId INT = NULL;    
    
    BEGIN TRY
        BEGIN TRANSACTION;

    INSERT INTO EmailActivities (EmailTemplateId, UserId, ToAddress, Body, SentDate,SentStatus)
    VALUES (@EmailTemplateId, @UserId, @ToAddress, @Body, GETDATE(),@SentStatus);

    SET @ActionTaken = 'Email Activity Inserted Successfully';
    

     COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        SET @Success = 0;
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

		SET @ActionTaken = ERROR_MESSAGE();

    END CATCH;

     SELECT
        CASE
            WHEN @AffectedId IS NOT NULL
                     AND @AffectedId > 0
                THEN @AffectedId
            ELSE @Success
            END as SuccessIndicator,
        @ActionTaken as Message;
END
GO
