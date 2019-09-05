
namespace Api.Controllers.ViewModels
{
    public class TaskViewModel
    {
        public int Id { get; set; } = 0;

        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string Status { get; set; } = "";
    }
}
