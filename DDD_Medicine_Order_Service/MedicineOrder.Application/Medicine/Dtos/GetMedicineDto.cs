using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineOrder.Application.Medicine.Dtos
{
    public record GetMedicineDto
    {
        [Required]
        public int MedicineId { get; set; }
    }
}
