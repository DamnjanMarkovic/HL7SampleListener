using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HL7SampleListener
{
    public class Message
    {
        private const string MSH = "MSH";
        private const int MSH_MSG_CONTROL_ID = 10;

        private List<Segment> segments;

        public Message()
        {
            Initialize();
        }

        public void Initialize()
        {
            segments = new List<Segment>();
        }

        protected Segment Header()
        {
            if (segments.Count == 0 || segments[0].Name != MSH)
            {
                return null;
            }
            return segments[0];
        }

        public string MessageControlId()
        {
            Segment msh = Header();
            if (msh == null) return String.Empty;
            return msh.Field(MSH_MSG_CONTROL_ID);
        }

        public void Add(Segment segment)
        {
            if (!String.IsNullOrEmpty(segment.Name) && segment.Name.Length == 3)
            {
                segments.Add(segment);
            }
        }

        public void DeSerializeMessage(string msg)
        {
            Initialize();
            
            var segment = new Segment();
            char[] separator = { '\r' };
            var tokens = msg.Split(separator, StringSplitOptions.None);
            foreach (var item in tokens)
            {
                //var segment = new Segment();
                segment.DeSerializeSegment(item.Trim('\n'));
                Add(segment);
                if (item.Contains("PID"))
                {
                    Console.WriteLine("Item sa PID-om je: " + item);
                    char[] separators = { '|' };
                    string temp = item.Trim('|');
                    string[] fields = temp.Split(separators, StringSplitOptions.None);

                    PatientModel patient = new PatientModel();
                    patient.MedicalID = Int32.Parse(fields[3]);
                    patient.FirstName = fields[5].Split('^')[0];
                    patient.LastName = fields[5].Split('^')[1];
                    patient.DOB = fields[7];
                    patient.PhoneNumber = fields[13];
                    patient.Address = fields[11];


                    DataAccess dataAccess = new DataAccess();

                    

                    Console.WriteLine("Patient id: " + patient.MedicalID + ";\nPatient name: " + patient.FirstName + ";\nPatient last name: " + patient.LastName );
                    
                    
                    dataAccess.InsertPatient(patient.MedicalID, patient.FirstName, patient.LastName, patient.DOB, patient.PhoneNumber, patient.Address);
                }

            }

            //Console.WriteLine("Prima poruku: " + segment);
        }

        public string SerializeMessage()
        {
            var builder = new StringBuilder();
            char[] separators = { '\r', '\n' };

            foreach (var segment in segments)
            {
                builder.Append(segment.SerializeSegment());
                builder.Append("\r\n");
            }
            return builder.ToString().TrimEnd(separators);
        }

    }



}

