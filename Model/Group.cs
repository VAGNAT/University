using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Group
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Course Course { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Group group &&
                   Name == group.Name
                   &&
                   EqualityComparer<Course>.Default.Equals(Course, group.Course);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Course);
        }
    }
}
