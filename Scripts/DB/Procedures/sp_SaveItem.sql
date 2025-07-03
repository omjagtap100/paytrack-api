ALTER PROCEDURE sp_SaveItem
    @ItemId BIGINT,
    @UserId BIGINT,
    @ListId BIGINT,
    @Title NVARCHAR(1000),    
    @Priority INT,
    @Notes NVARCHAR(max) = NULL,
    @DueDate DATETIME = NULL,
    @Icon NVARCHAR(max) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @SP_Name VARCHAR(128) = 'sp_SaveItem';
    DECLARE @Success BIT = 1;
    DECLARE @ActionTaken NVARCHAR(500) = NULL;
    DECLARE @AffectedId INT = NULL;    
    
    BEGIN TRY
        BEGIN TRANSACTION;

     IF EXISTS(SELECT 1 FROM dbo.Items WHERE ItemId = @ItemId OR @ItemId > 0)
    BEGIN
        UPDATE i
        SET 
            Title = @Title,
            Notes = @Notes,
            DueDate = @DueDate,
            Icon = @Icon,
            Priority = @Priority,
            UpdatedDate = GETDATE(), 
            UpdatedBy = @UserId
        FROM dbo.Items i
        INNER JOIN dbo.Lists l ON i.ListId = l.ListId
        WHERE i.ItemId = @ItemId
        AND l.CreatorUserId = @UserId;


        -- Check if the update was successful
        IF @@ROWCOUNT = 0
        BEGIN
            SET @Success = 0;
            SET @ActionTaken = 'Item not found or you do not have permission to update it.';
        END
        ELSE
        BEGIN
        SET @AffectedId = @ListId;
        SET @ActionTaken = 'Item updated successfully.';
        END

    END
    ELSE
    BEGIN
        INSERT INTO dbo.Items (ListId,Title,Notes,AssignedToUserId,DueDate,IsCompleted,CompletedDate,IsDeleted,Icon,Priority,UpdatedDate,UpdatedBy)
        VALUES (@ListId, @Title, @Notes, @UserId, @DueDate, 0, NULL, 0, @Icon, @Priority, GETDATE(), @UserId);

        SET @AffectedId = SCOPE_IDENTITY();
        SET @ActionTaken = 'Item saved successfully.';
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
