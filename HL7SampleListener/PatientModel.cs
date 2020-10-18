using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HL7SampleListener
{
    public class PatientModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string DOB { get; set; }

        public string Address { get; set; }

        //public string overide ToString()
        //{
        //    return "Patient id: " + Id + ", patient name: " + Name + " patient DOB: " + DOB + " and patient address: " + Address;
        //}

    }
}
