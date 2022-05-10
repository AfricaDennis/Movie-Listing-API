using Newtonsoft.Json;
using System.Net;
using System.Net.Http;

namespace Film_Listing_API.Models
{
    public class OperationResult : IOperationResult
    {
        private OperationResult(HttpStatusCode status, object content)
        {
            Status = status;
            Content = content;
        }

        private OperationResult(HttpStatusCode status, byte[] content, string contentType)
        {
            Status = status;
            FileContent = content;
            FileContentType = contentType;
        }

        public static OperationResult Success(object content = null)
        {
            return new OperationResult(HttpStatusCode.OK, content);
        }

        public static OperationResult Success(string content)
        {
            return new OperationResult(HttpStatusCode.OK, JsonConvert.SerializeObject(content));
        }

        public static OperationResult Created(object content = null)
        {
            return new OperationResult(HttpStatusCode.Created, content);
        }

        public static OperationResult Created(string content)
        {
            return new OperationResult(HttpStatusCode.Created, JsonConvert.SerializeObject(content));
        }

        public static OperationResult Unauthorized(object content = null)
        {
            return new OperationResult(HttpStatusCode.Unauthorized, content);
        }

        public static OperationResult Unauthorized(string content)
        {
            return new OperationResult(HttpStatusCode.Unauthorized, JsonConvert.SerializeObject(content));
        }

        public static OperationResult Forbidden(string content = "Forbidden")
        {
            return new OperationResult(HttpStatusCode.Forbidden, JsonConvert.SerializeObject(content));
        }

        public static OperationResult UnexpectedError(string content)
        {
            return new OperationResult(HttpStatusCode.InternalServerError, content);
        }

        public static OperationResult UnexpectedError()
        {
            return new OperationResult(HttpStatusCode.InternalServerError, "Error, contact with Administrator");
        }

        public static OperationResult BadRequest(object content = null)
        {
            return new OperationResult(HttpStatusCode.BadRequest, content);
        }

        public static OperationResult BadRequest(string content)
        {
            return new OperationResult(HttpStatusCode.BadRequest, JsonConvert.SerializeObject(content));
        }

        public static OperationResult FromStatusCode(HttpStatusCode statusCode, object content = null)
        {
            return new OperationResult(statusCode, content);
        }

        public static OperationResult NotFound(object content = null)
        {
            return new OperationResult(HttpStatusCode.NotFound, content);
        }

        public static OperationResult NotFound(string content)
        {
            return new OperationResult(HttpStatusCode.NotFound, JsonConvert.SerializeObject(content));
        }

        public static OperationResult Gone(object content = null)
        {
            return new OperationResult(HttpStatusCode.Gone, content);
        }

        public static OperationResult Gone(string content)
        {
            return new OperationResult(HttpStatusCode.Gone, JsonConvert.SerializeObject(content));
        }

        public static OperationResult Accepted(object content = null)
        {
            return new OperationResult(HttpStatusCode.Gone, content);
        }

        public static OperationResult Accepted(string content)
        {
            return new OperationResult(HttpStatusCode.Accepted, JsonConvert.SerializeObject(content));
        }

        public static OperationResult File(byte[] fileBytes, string contentType)
        {
            return new OperationResult(HttpStatusCode.OK, fileBytes, contentType);
        }

        public object Content { get; private set; }
        public byte[] FileContent { get; private set; }
        public string FileContentType { get; private set; }
        public HttpStatusCode Status { get; private set; }
        bool IOperationResult.Success => new HttpResponseMessage(Status).IsSuccessStatusCode;



    }
}
