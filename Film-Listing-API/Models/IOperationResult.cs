using System.Net;

namespace Film_Listing_API.Models
{
    public interface IOperationResult
    {
        object Content { get; }
        byte[] FileContent { get; }
        string FileContentType { get; }
        HttpStatusCode Status { get; }
        bool Success { get; }
    }
}
