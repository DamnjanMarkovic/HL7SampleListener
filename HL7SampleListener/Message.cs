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
            Console.WriteLine("Tokeni su: " + tokens.ToString());
            foreach (var item in tokens)
            {
                //var segment = new Segment();
                segment.DeSerializeSegment(item.Trim('\n'));
                Add(segment);
                if (item.Contains("PID"))
                {
                    char[] separators = { '|' };
                    string temp = item.Trim('|');
                    string[] fields = temp.Split(separators, StringSplitOptions.None);

                    PatientModel patient = new PatientModel();
                    patient.Id = Int32.Parse(fields[3]);
                    patient.Name = fields[5];
                    patient.Address = fields[11];
                    patient.DOB = fields[7];

                    Console.WriteLine("Patient id: " + patient.Id + ", patient name: " + patient.Name + " patient DOB: " + patient.DOB + 
                        " and patient address: " + patient.Address);


                    Console.WriteLine("Token PID: " + patient.ToString());
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

