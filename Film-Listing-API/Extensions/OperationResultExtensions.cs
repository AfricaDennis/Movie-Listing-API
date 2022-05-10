using Film_Listing_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Film_Listing_API.Extensions
{
    public static class OperationResultExtensions
    {
        public static IActionResult ContentOrError(this IOperationResult operationResult)
        {
            return new ObjectResult(operationResult.Content) { StatusCode = (int)operationResult.Status };
        }

        public static IActionResult FIleOrError(this IOperationResult operationResult)
        {
            if (!operationResult.Success) return new ObjectResult(operationResult.Content) { StatusCode = (int)operationResult.Status };
            return new FileContentResult(operationResult.FileContent, operationResult.FileContentType);
        }
    }
}
