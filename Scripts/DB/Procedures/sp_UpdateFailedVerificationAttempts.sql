CREATE PROCEDURE sp_UpdateFailedVerificationAttempts
    @UserId BIGINT,
    @Count INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @SP_Name VARCHAR(128) = 'sp_UpdateFailedVerificationAttempts';
    DECLARE @Success BIT = 1;
    DECLARE @ActionTaken NVARCHAR(500) = NULL;
    DECLARE @AffectedId INT = NULL;    
    
    BEGIN TRY
        BEGIN TRANSACTION;

    UPDATE dbo.Users
    SET FailedOTPAttempts = CASE WHEN @Count = 0 THEN 0 ELSE FailedOTPAttempts + 1 END
    WHERE UserId = @UserId;

    SET @ActionTaken = 'Failed verification attempts incremented successfully.';
    SELECT @AffectedId = FailedOTPAttempts FROM dbo.Users WHERE UserId = @UserId;
    

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
