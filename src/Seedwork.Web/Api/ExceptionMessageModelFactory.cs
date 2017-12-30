namespace Hqv.Seedwork.Web.Api
{
    /// <summary>
    /// Factory to create exception message models
    /// </summary>
    public static class ExceptionMessageModelFactory
    {
        public static ExceptionMessageModel ResourceNotFound(string detail = null)
        {
            return new ExceptionMessageModel(4040, "Resource Not Found", detail);
        }

        public static ExceptionMessageModel ParentToResourceNotFound(string detail = null)
        {
            return new ExceptionMessageModel(4041, "Parent To Resource Not Found", detail);
        }

        public static ExceptionMessageModel BadRequestBody(string detail = null)
        {
            return new ExceptionMessageModel(4000, "Bad Request Body", detail);
        }

        public static ExceptionMessageModel BadRequstFieldsParameters(string detail = null)
        {
            return new ExceptionMessageModel(4001, "Bad Fields parameters", detail);
        }

        public static ExceptionMessageModel BadRequstOrderByParameters(string detail = null)
        {
            return new ExceptionMessageModel(4002, "Bad OrderBy parameters", detail);
        }

        public static ExceptionMessageModel BadRequestParentMismatch(string detail = null)
        {
            return new ExceptionMessageModel(4003, "Parent mismatch", detail);
        }

        public static ExceptionMessageModel BadRequestModelStateInvalid(object modelState)
        {
            return new ExceptionMessageModel(4004, "Model State Invalid", modelState);
        }
    }
}