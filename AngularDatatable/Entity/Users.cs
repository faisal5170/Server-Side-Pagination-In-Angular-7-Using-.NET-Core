using System.ComponentModel.DataAnnotations;

namespace AngularDatatable.Entity
{
    public class Users
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Company { get; set; }

        public string ContactNumber { get; set; }
    }
}
