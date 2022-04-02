using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Entities
{
    public class Author
    {  
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
