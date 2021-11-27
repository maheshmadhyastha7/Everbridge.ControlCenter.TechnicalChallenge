using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Everbridge.ControlCenter.TechnicalChallenge.DoorDatabase
{
    [Table("CC.TechnicalChallenge.DoorRecord")]
    public class DoorRecord
    {
        public DoorRecord()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key, Column("Id"), MaxLength(128)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        [Column("Label"), MaxLength(256), Required]
        public string Label { get; set; }

        [Column("IsOpen"), Required]
        public bool IsOpen { get; set; }

        [Column("IsLocked"), Required]
        public bool IsLocked { get; set; }
    }
}
