using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public Group Group { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Student student &&
                   FirstName == student.FirstName &&
                   LastName == student.LastName &&
                   EqualityComparer<Group>.Default.Equals(Group, student.Group);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FirstName, LastName, Group);
        }
    }
}
