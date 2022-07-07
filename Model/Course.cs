using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Course
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Course course &&
                   Name == course.Name &&
                   Description == course.Description;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Description);
        }
    }
}