ALTER PROCEDURE sp_InitiateAuthByPhoneNumber
    @PhoneNumber NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @SP_Name VARCHAR(128) = 'sp_InitiateAuthByPhoneNumber';
    DECLARE @Success BIT = 1;
    DECLARE @ActionTaken NVARCHAR(500) = NULL;
    DECLARE @AffectedId INT = NULL;    

    DECLARE @NewTOTPSecret NVARCHAR(255) = REPLACE(NEWID(), '-', '');
    
    BEGIN TRY
        BEGIN TRANSACTION;

    IF EXISTS(SELECT 1 FROM Users WHERE PhoneNumber = @PhoneNumber)
    BEGIN
        UPDATE Users
        SET 
            TOTPSecret = @NewTOTPSecret, 
            UpdatedDate = GETDATE(), 
            UpdatedBy = @SP_Name
        WHERE PhoneNumber = @PhoneNumber;

        SELECT TOP 1 @AffectedId = UserId FROM Users WHERE PhoneNumber = @PhoneNumber;
        SET @ActionTaken = @NewTOTPSecret;
    END
    ELSE
    BEGIN
        INSERT INTO Users (PhoneNumber, TOTPSecret, FailedOTPAttempts,Avatar, UpdatedDate, UpdatedBy)
        VALUES (@PhoneNumber, @NewTOTPSecret, 0, 'https://api.dicebear.com/9.x/identicon/svg?seed='+CAST(NEWID() AS VARCHAR),GETDATE(), @SP_Name);

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
