CREATE PROCEDURE sp_UpdateName
    @UserId BIGINT ,
    @Name NVARCHAR(1000)
    AS 
    BEGIN
    SET NOCOUNT ON;

    DECLARE @SP_Name VARCHAR(128) = 'sp_UpdateName';
    DECLARE @Success BIT = 1;
    DECLARE @ActionTaken NVARCHAR(500) = NULL;
    DECLARE @AffectedId INT = NULL;    
    
    BEGIN TRY
        BEGIN TRANSACTION;
		 SET NOCOUNT ON;
        UPDATE dbo.Users  
        SET Name = @Name,
        UpdatedDate = GETDATE(),
        UpdatedBy = @UserId 
        WHERE UserId = @UserId;
         -- Check if the update was successful
        IF @@ROWCOUNT = 0
        BEGIN
            SET @Success = 0;
            SET @ActionTaken = 'User not found or you do not have permission to update it.';
        END
        ELSE
        BEGIN
        SET @AffectedId = @UserId;
        SET @ActionTaken = 'User updated successfully.';
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