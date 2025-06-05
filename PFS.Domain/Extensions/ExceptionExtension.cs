using PFS.Domain.Models.RequestBody;

namespace PFS.Domain.Extensions
{
    public static class ExceptionExtension
    {
        public static string GetFullMessage(this Exception exception)
        {
            return exception.Message + "\n" + exception.InnerException.Message + "\n" + exception.InnerException.StackTrace;
        }

        public static void GenerateErrorStatus<T>(this ResponseResult<T> result,params string[] errors)
        {
            result.errors = errors.Concat(result.errors);
            result.success = false;
        }
        
    }
}