CREATE FUNCTION dbo.SumOfPartsandLabors (@workOrderId AS INT)
RETURNS DECIMAL(18,2)

AS
BEGIN
DECLARE @sumOfParts DECIMAL(18,2), @sumOfLabors DECIMAL(18,2)

SELECT @sumOfParts = SUM(Total)
FROM dbo.Parts
WHERE WorkOrderId = @workOrderId

SELECT @sumOfLabors = SUM(Total)
FROM dbo.Labors
WHERE WorkOrderId = @workOrderId

    RETURN ISNULL(@sumOfLabors,0) + ISNULL(@sumOfParts,0)
END
