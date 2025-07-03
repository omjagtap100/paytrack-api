ALTER PROCEDURE sp_SaveSubTask
    @SubTaskId BIGINT,
    @ParentItemId BIGINT,
    @UserId BIGINT,
    @Title NVARCHAR(1000)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @SP_Name VARCHAR(128) = 'sp_SaveSubTask';
    DECLARE @Success BIT = 1;
    DECLARE @ActionTaken NVARCHAR(500) = NULL;
    DECLARE @AffectedId INT = NULL;    
    
    BEGIN TRY
        BEGIN TRANSACTION;

     IF EXISTS(SELECT 1 FROM dbo.SubTasks WHERE SubTaskID = @SubTaskId )
    BEGIN
        UPDATE st
        SET 
            Title = @Title,
            UpdatedDate = GETDATE(), 
            UpdatedBy = @UserId
        FROM dbo.SubTasks st
        INNER JOIN dbo.Items i ON st.ParentItemID = i.ItemId
        WHERE st.SubTaskID=@SubTaskId
        AND i.AssignedToUserId = @UserId;


        -- Check if the update was successful
        IF @@ROWCOUNT = 0
        BEGIN
            SET @Success = 0;
            SET @ActionTaken = 'SubTask not found or you do not have permission to update it.';
        END
        ELSE
        BEGIN
        SET @AffectedId = @ParentItemId;
        SET @ActionTaken = 'SubTask updated successfully.';
        END

    END
    ELSE
    BEGIN
        
        INSERT INTO dbo.SubTasks (ParentItemID,Title,IsCompleted,CompletedDate,IsDeleted,UpdatedDate,UpdatedBy)
        VALUES (@ParentItemId,@Title,0,NULL,0,GETDATE(),@UserId);

        SET @AffectedId = SCOPE_IDENTITY();
        SET @ActionTaken = 'SubTask saved successfully.';
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
