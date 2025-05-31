namespace PFS.Domain.Models.RequestBody
{
    public class ResponseResult<T>
    {
        public IEnumerable<string> errors { get; set; }
        public IEnumerable<T> obj { get; set; }
        public bool success { get; set; } = true;
    }
}