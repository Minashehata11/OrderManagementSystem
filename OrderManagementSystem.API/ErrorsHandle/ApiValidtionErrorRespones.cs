
namespace OrderManagementSystem.API.ErrorsHandle
{
    public class ApiValidtionErrorRespones : ErrorApiResponse
    {
        public IEnumerable<string> Errors { get; set; }
        public ApiValidtionErrorRespones() : base(400)
        {
            Errors = new List<string>();
        }

    }
}
