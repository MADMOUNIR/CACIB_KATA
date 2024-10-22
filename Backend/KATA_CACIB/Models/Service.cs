namespace KATA_CACIB.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<MyAction> Actions { get; set; }

        public virtual string GetServiceDetails()
        {
            return $"Service ID: {Id}, Name: {Name} , Actions : {String.Join(", ", Actions.Select(x =>x.Name).ToArray()) }";
        }
    }
}
