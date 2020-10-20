using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace HL7SampleListener
{
    public class DataAccess
    {
        public void InsertPatient(int medicalID, string firstName, string lastName, string emailAddress, string phoneNumber, string address)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(HelperDB.CnnVal("HL7SampleDB")))
            {
                //Console.WriteLine("Stigli na upis");

                //List<PatientModel> people = new List<PatientModel>();
                PatientModel patient = new PatientModel
                {

                    MedicalID = medicalID,                    
                    FirstName = firstName,
                    LastName = lastName,
                    DOB = emailAddress,
                    PhoneNumber = phoneNumber, 
                    Address = address


                };
            //people.Add(new PatientModel
            //    {
            //        MedicalID = medicalID,                    
            //        FirstName = firstName,
            //        LastName = lastName,
            //        DOB = emailAddress,
            //        PhoneNumber = phoneNumber, 
            //        Address = address

                    
            //    });
            //    foreach (var person in people)
            //    {
            //        Console.WriteLine("Pred bazu stampa: " + person.MedicalID + " " + person.FirstName + " " + person.LastName);
            //    }
                //int number = connection.Execute("dbo.People_Insert @FirstName, @LastName, @DOB, @PhoneNumber, @Address", people);
                int number = connection.Execute("dbo.Patient_Insert @MedicalID, @FirstName, @LastName, @DOB, @PhoneNumber, @Address", patient);
                //int number = connection.Execute("dbo.Patient_Insert @FirstName, @LastName, @DOB, @Address", people);
                Console.WriteLine("Broj koji je vracen je :" + number);

            }


        }

    }
}
