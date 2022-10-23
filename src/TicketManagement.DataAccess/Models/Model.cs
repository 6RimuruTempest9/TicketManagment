namespace TicketManagement.DataAccess.Models
{
    public abstract class Model : IModel
    {
        public int Id { get; set; }

        public abstract object Clone();
    }
}