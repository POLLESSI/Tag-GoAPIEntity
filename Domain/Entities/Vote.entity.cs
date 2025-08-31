using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace MyApi.Domain.Entities
{
    public class VoteEntity
    {
        [Key]
        public int Id { get; set; }

        public int? Value { get; set; } // Note / 4

        public string? Text { get; set; } // Commentaire

        public required int User_id { get; set; }

        public required int Activity_id { get; set; }
        

        [ForeignKey(nameof(User_id))]
        public required UserEntity User { get; set; }

        [ForeignKey(nameof(Activity_id))]
        public required ActivityEntity Activity { get; set; }
    }
}