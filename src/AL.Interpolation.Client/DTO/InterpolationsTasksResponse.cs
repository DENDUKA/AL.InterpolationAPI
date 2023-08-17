using AL.Interpolation.Entities.Interpolation;

namespace AL.Interpolation.Client.DTO
{
    public class InterpolationsTasksResponse
    {
        public string TaskId { get; set; }
        public TaskStatus Status { get; set; }
        public Surface? Surface { get; set; }
    }
}
