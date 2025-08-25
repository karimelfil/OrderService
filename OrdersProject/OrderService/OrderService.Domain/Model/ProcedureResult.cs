
namespace OrderService.Domain.Model
{
    public class ProcedureResult<T>
    {
        public int ErrorCode { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
    }
}
