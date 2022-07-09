namespace Utils.Messages
{
    public class Created
    {
        public string message { get; set; }
        public Created()
        {
            message = "created successfully";
        }
    }

    public class Updated
    {
        public string message { get; set; }
        public Updated()
        {
            message = "updated successfully";
        }
    }

    public class Deleted
    {
        public string message { get; set; }
        public Deleted()
        {
            message = "deleted successfully";
        }
    }
}
