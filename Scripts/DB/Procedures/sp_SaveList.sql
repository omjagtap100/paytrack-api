ALTER PROCEDURE sp_SaveList
    @ListId BIGINT,
    @UserId BIGINT,
    @Title NVARCHAR(255),
    @Description NVARCHAR(MAX)= NULL,
    @Icon NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @SP_Name VARCHAR(128) = 'sp_SaveList';
    DECLARE @Success BIT = 1;
    DECLARE @ActionTaken NVARCHAR(500) = NULL;
    DECLARE @AffectedId INT = NULL;    
    
    BEGIN TRY
        BEGIN TRANSACTION;

     IF EXISTS(SELECT 1 FROM dbo.Lists WHERE ListId = @ListId OR @ListId > 0)
    BEGIN
        UPDATE Lists
        SET 
            Title = @Title,
            Description = @Description,
            Icon = @Icon,
            UpdatedDate = GETDATE(), 
            UpdatedBy = @UserId
        WHERE ListId = @ListId
        AND CreatorUserId = @UserId;


        -- Check if the update was successful
        IF @@ROWCOUNT = 0
        BEGIN
            SET @Success = 0;
            SET @ActionTaken = 'List not found or you do not have permission to update it.';
        END
        ELSE
        BEGIN
        SET @AffectedId = @ListId;
        SET @ActionTaken = 'List updated successfully.';
        END

    END
    ELSE
    BEGIN
        INSERT INTO dbo.Lists (CreatorUserId, Title, Description, Icon, UpdatedDate, UpdatedBy)
        VALUES (@UserId, @Title, @Description, @Icon, GETDATE(), @UserId);

        SET @AffectedId = SCOPE_IDENTITY();
        SET @ActionTaken = 'List saved successfully.';
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
