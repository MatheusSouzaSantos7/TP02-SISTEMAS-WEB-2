namespace TP_02.Models
{
    public class Author
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public char Gender { get; set; }

        public Author(string name, string email, char gender)
        {
            Name = name;
            Email = email;
            Gender = gender;
        }

        public override string ToString() => $"Author[name={Name},email={Email},gender={Gender}]";
    }
}