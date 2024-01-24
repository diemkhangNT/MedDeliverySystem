using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineOrder.Application.MedicineOrder.Dtos
{
    public record GetListOrderByPharmacyIdDto
    {
        [Required]
        public int PharmacyId { get; set; }
    }
}
