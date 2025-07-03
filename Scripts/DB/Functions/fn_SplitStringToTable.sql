SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fn_SplitStringToTable]
(
    @String NVARCHAR(MAX),
    @Delimiter CHAR(1)
)
RETURNS @Result TABLE (Value NVARCHAR(MAX))
AS
BEGIN
    DECLARE @Xml XML
    SET @Xml = N'<root><item>' + REPLACE(@String, @Delimiter, '</item><item>') + '</item></root>'

    INSERT INTO @Result (Value)
    SELECT x.i.value('.', 'NVARCHAR(MAX)') AS Value
    FROM @Xml.nodes('/root/item') AS x(i)

    RETURN
END
GO
