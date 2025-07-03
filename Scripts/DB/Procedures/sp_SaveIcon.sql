CREATE PROCEDURE sp_SaveIcon
    @IconId BIGINT,
    @UserId BIGINT,
    @IconName NVARCHAR(255),
    @IconPath NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @SP_Name VARCHAR(128) = 'sp_SaveIcon';
    DECLARE @Success BIT = 1;
    DECLARE @ActionTaken NVARCHAR(500) = NULL;
    DECLARE @AffectedId INT = NULL;    
    
    BEGIN TRY
        BEGIN TRANSACTION;

     IF EXISTS(SELECT 1 FROM dbo.Icons WHERE IconId = @IconId OR @IconId > 0)
    BEGIN
        UPDATE Icons
        SET 
            IconName = @IconName,
            IconPath = @IconPath,
            UpdatedDate = GETDATE(), 
            UpdatedBy = @UserId
        WHERE IconId = @IconId;


        -- Check if the update was successful
        IF @@ROWCOUNT = 0
        BEGIN
            SET @Success = 0;
            SET @ActionTaken = 'Icon not found or you do not have permission to update it.';
        END
        ELSE
        BEGIN
        SET @AffectedId = @IconId;
        SET @ActionTaken = 'Icon updated successfully.';
        END

    END
    ELSE
    BEGIN
        INSERT INTO dbo.Icons (IconName, IconPath, UpdatedDate, UpdatedBy)
        VALUES (@IconName, @IconPath, GETDATE(), @UserId);

        SET @AffectedId = SCOPE_IDENTITY();
        SET @ActionTaken = 'Icon saved successfully.';
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
