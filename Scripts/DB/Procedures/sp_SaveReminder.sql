ALTER PROCEDURE sp_SaveReminder
@ReminderId BIGINT,
@ItemId BIGINT,
@UserId BIGINT,
@ReminderTime DATETIME, 
@Message NVARCHAR(1000)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @SP_Name VARCHAR(128) = 'sp_SaveReminder';
    DECLARE @Success BIT = 1;
    DECLARE @ActionTaken NVARCHAR(500) = NULL;
    DECLARE @AffectedId INT = NULL;    
    
    BEGIN TRY
        BEGIN TRANSACTION;

     IF EXISTS(SELECT 1 FROM dbo.Reminders WHERE ReminderId = @ReminderId OR @RemainderId>0 )
    BEGIN
        UPDATE rm
        SET 
            ReminderTime = @ReminderTime,
            Message = @Message,
            UpdatedDate = GETDATE(), 
            UpdatedBy = @UserId
        FROM dbo.Reminders rm
        INNER JOIN dbo.Items i ON i.ItemId = rm.ItemId
        WHERE rm.ReminderId = @ReminderId
        AND i.AssignedToUserId = @UserId;


        -- Check if the update was successful
        IF @@ROWCOUNT = 0
        BEGIN
            SET @Success = 0;
            SET @ActionTaken = 'Reminder not found or you do not have permission to update it.';
        END
        ELSE
        BEGIN
        SET @AffectedId = @ReminderId;
        SET @ActionTaken = 'Reminder updated successfully.';
        END

    END
    ELSE
    BEGIN
        INSERT INTO dbo.Reminders( ItemId, ReminderTime, IsSent, IsDeleted, Message, UpdatedDate, UpdatedBy)
        VALUES (@ItemId, @ReminderTime,0, 0, @Message,GETDATE(),@UserId);
        SET @AffectedId = SCOPE_IDENTITY();
        SET @ActionTaken = 'Reminder saved successfully.';
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