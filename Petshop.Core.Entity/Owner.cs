using System.Threading;

namespace Petshop.Core.Entity
{
    public class Owner
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int PetsID { get; set; }
        
    }
}