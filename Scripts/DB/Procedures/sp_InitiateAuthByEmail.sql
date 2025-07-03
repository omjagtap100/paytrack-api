ALTER PROCEDURE sp_InitiateAuthByEmail
    @Email NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @SP_Name VARCHAR(128) = 'sp_InitiateAuthByEmail';
    DECLARE @Success BIT = 1;
    DECLARE @ActionTaken NVARCHAR(500) = NULL;
    DECLARE @AffectedId INT = NULL;    

    DECLARE @NewTOTPSecret NVARCHAR(255) = REPLACE(NEWID(), '-', '');
    
    BEGIN TRY
        BEGIN TRANSACTION;

    IF EXISTS(SELECT 1 FROM Users WHERE Email = @Email)
    BEGIN
        UPDATE Users
        SET 
            FailedOTPAttempts = 0,
            UpdatedDate = GETDATE(), 
            UpdatedBy = @SP_Name
        WHERE Email = @Email;

        SELECT TOP 1 @AffectedId = UserId,@ActionTaken = TOTPSecret  FROM Users WHERE Email = @Email;
    END
    ELSE
    BEGIN
        INSERT INTO Users (Email, TOTPSecret, FailedOTPAttempts, Avatar,UpdatedDate, UpdatedBy)
        VALUES (@Email, @NewTOTPSecret, 0, 'https://api.dicebear.com/9.x/identicon/svg?seed='+CAST(NEWID() AS VARCHAR(255)),GETDATE(), @SP_Name);

        SET @AffectedId = SCOPE_IDENTITY();
        SET @ActionTaken = @NewTOTPSecret;
    END
    

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
