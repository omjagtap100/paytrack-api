ALTER PROCEDURE sp_MarkSubTaskAsCompleted
    @UserId BIGINT ,
    @SubTaskId BIGINT
    AS 
    BEGIN
    SET NOCOUNT ON;

    DECLARE @SP_Name VARCHAR(128) = 'sp_MarkSubTaskAsCompleted';
    DECLARE @Success BIT = 1;
    DECLARE @ActionTaken NVARCHAR(500) = NULL;
    DECLARE @AffectedId INT = NULL;    
    
    BEGIN TRY
        BEGIN TRANSACTION;
         
        UPDATE st
        SET st.IsCompleted = 1, 
            st.UpdatedBy = @UserId,
            st.CompletedDate = GETDATE(),
            st.UpdatedDate = GETDATE()
          FROM dbo.SubTasks  as st 
          INNER JOIN dbo.Items as i on st.ParentItemID = i.ItemId
        WHERE
        st.SubTaskID = @SubTaskId
        AND i.AssignedToUserId = @UserId;
         -- Check if the update was successful
        IF @@ROWCOUNT = 0
        BEGIN
            SET @Success = 0;
            SET @ActionTaken = 'SubTasks not found or you do not have permission to update it.';
        END
        ELSE
        BEGIN
        SET @AffectedId = @SubTaskId;
        SET @ActionTaken = 'SubTasks Completedsuccessfully.';
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
