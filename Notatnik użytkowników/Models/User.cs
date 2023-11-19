namespace Notatnik_użytkowników.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }


        string NewMethod(string separator)
        {
            var newString = @$"{Id}{separator}{Name}{separator}{LastName}";

            return newString;
        }
    }
}