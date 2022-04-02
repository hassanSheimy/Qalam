using Qalam.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Qalam.MYSQL.Entities
{
    public class User
    {
        public User()
        {
            Files = new HashSet<DataFile>();
            ReceivedNotifications = new HashSet<Notification>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public DateTime JoinDate { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string NormalizedEmail { get; set; }
        [Required]
        public string HashedPassword { get; set; }
        public bool IsActive { get; set; }
        [Required]
        public bool IsConfirmed { get; set; }
        public string About { get; set; }
        public string Facebook { get; set; }

        [ForeignKey("Country")]
        [Required]
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }

        [ForeignKey("Role")]
        [Required]
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        public virtual Teacher Teacher { get; set; }
        public virtual Student Student { get; set; }

        public virtual ICollection<DataFile> Files { get; set; }     
        public virtual ICollection<Notification> ReceivedNotifications { get; set; }
        public virtual ICollection<Notification> SentNotifications { get; set; }
    }
}
