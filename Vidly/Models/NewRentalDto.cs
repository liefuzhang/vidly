using System.Collections.Generic;
using Vidly.Dtos;

namespace Vidly.Models {
    public class NewRentalDto {
        public int CustomerId { get; set; }
        public List<int> MovieIds { get; set; }
    }
}