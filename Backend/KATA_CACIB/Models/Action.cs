namespace KATA_CACIB.Models
{
    public class MyAction
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual string GetServiceDetails()
        {
            return $"Service ID: {Id}, Name: {Name}";
        }
    }
}
