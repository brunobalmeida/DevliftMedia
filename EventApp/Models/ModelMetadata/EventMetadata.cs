using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp.Models
{
    [ModelMetadataType(typeof(EventMetadata))]
    public partial class Events : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (String.IsNullOrEmpty(EventTitle))
            {
                yield return new ValidationResult("Event Title Name cannot be Empty",
                    new[] { nameof(EventTitle) });
            }

            if (String.IsNullOrEmpty(EventDescription))
            {
                yield return new ValidationResult("Event Description cannot be Empty",
                    new[] { nameof(EventDescription) });
            }

            if (String.IsNullOrEmpty(EventContact))
            {
                yield return new ValidationResult("Event Contact cannot be Empty",
                    new[] { nameof(EventContact) });
            }


            yield return ValidationResult.Success;
        }
    }



    public class EventMetadata
    {

        public int EventId { get; set; }
        [Display(Name = "Event Title")]
        public string EventTitle { get; set; }
        [Display(Name = "Event Date")]
        [DataType(DataType.Date)]
        public DateTime EventDate { get; set; }
        public byte[] Picture { get; set; }
        public string PictureType { get; set; }
        [Display(Name = "Event Description")]
        public string EventDescription { get; set; }
        [Display(Name = "Contact")]
        public string EventContact { get; set; }
        [Display(Name = "Event External Link")]
        public string EventLink { get; set; }
        [Display(Name = "Event Address")]
        public string EventAddress { get; set; }

    }
}
