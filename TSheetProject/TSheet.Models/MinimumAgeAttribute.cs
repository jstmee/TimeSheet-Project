using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSheet.Models
{
    public class MinimumAgeAttribute: ValidationAttribute
    {
        int _minimumAge;
        public MinimumAgeAttribute(int minimumAge)
        {
            this._minimumAge = minimumAge;
        }
        public override bool IsValid(object value)
        {
            DateTime date;
            if(DateTime.TryParse(value.ToString(), out date))
            {
                return date.AddYears(_minimumAge)< DateTime.Now;
            }
            return false;
        }
    }
}
