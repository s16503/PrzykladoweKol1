using PrzyklKol1.Models;
using PrzyklKol1.Requests;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PrzyklKol1.Services
{
    public class sqlDBservice : AnimalsDBservice
    {


        private const string ConString = "Data Source=db-mssql;Initial Catalog=s16503;Integrated Security=True;";

        public IEnumerable<Animal> getAnimals(string columnName)
        { 
            
            Console.WriteLine("\n\n"+columnName+"\n\n");

            List<Animal> list = new List<Animal>();
            using (var con = new SqlConnection(ConString))
            using (var com = new SqlCommand())
            {
                com.Connection = con;

                com.CommandText = "SELECT Animal.Name as name , Animal.Type as type  , Animal.AdmissionDate as date , Owner.LastName as owner" +
                    "  FROM Animal" +
                    " JOIN Owner ON Owner.IdOwner = Animal.IdOwner " +
                    "ORDER BY " + columnName + ";";

                con.Open();
                var dr = com.ExecuteReader();

                while (dr.Read())
                {
                    list.Add(new Animal
                    {
                        Name = dr["name"].ToString(),
                        Type = dr["type"].ToString(),
                        AdmissionDate = dr["date"].ToString(),
                        LastNameOfOwner = dr["owner"].ToString()
                    });
                }

            }

            return list;

        }


        public Animal AddAnimal(AddAnimalRequest req)
        {
            using (var con = new SqlConnection(ConString))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
               
                com.Parameters.AddWithValue("@IdOwner",req.IdOwner);
                com.CommandText = "SELECT LastName FROM Owner WHERE IdOwner = @IdOwner;";

                con.Open();
                var tran = con.BeginTransaction("SampleTransaction");
                com.Transaction = tran;

                var dr = com.ExecuteReader();

                string LastName;
                if (dr.Read())
                {
                    LastName = dr["LastName"].ToString();                  
                }
                else
                    throw new Exception("Nie znaleziono zarejestrowanego właściciela");
                dr.Close();
                //com.CommandText = "SELECT max(IdAnimal)+1 as id FROM Animal;";
             
                //if(dr.Read())
                //{
                //    AnimalId = (int)dr["id"];
                //}

               


                //com.Parameters.AddWithValue("Id",AnimalId);
                com.Parameters.AddWithValue("Name",req.Name);
                com.Parameters.AddWithValue("Type",req.Type);
                com.Parameters.AddWithValue("OwnerId",req.IdOwner);
                com.Parameters.AddWithValue("AdmissionDate",req.AdmissionDate);


                com.CommandText = "INSERT INTO Animal VALUES(@Name,@Type,@AdmissionDate,@OwnerId)";

                com.ExecuteNonQuery();

                com.CommandText = "SELECT max(IdAnimal) as id FROM Animal;";
                 dr = com.ExecuteReader();
                int AnimalId = 1;
                if(dr.Read())
                {
                    AnimalId = (int)dr["id"];
                }
                dr.Close();

                    com.Parameters.AddWithValue("AnimalId", AnimalId);
                    com.Parameters.AddWithValue("Pname",null);
                  //  com.Parameters.AddWithValue("procedureId", null);

                    DateTime date = DateTime.Now;
                    string sqlFormattedDate = date.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    com.Parameters.AddWithValue("Date", sqlFormattedDate);
                foreach (string proc in req.Procedures)
                {
                    
                    com.Parameters["Pname"].Value = proc;                   
                    com.CommandText = "SELECT IdProcedure FROM \"Procedure\" WHERE Name =@Pname;";
                    dr = com.ExecuteReader();
                    if (!dr.Read())
                    {
                        dr.Close();
                        tran.Rollback();
                        throw new Exception("Nie znaleziono takiego zabiegu");
                    }
                
                    int procId = (int)dr["IdProcedure"];
                    dr.Close();
                    if(com.Parameters.Contains("procedureId"))
                        com.Parameters["procedureId"].Value = procId;
                    else
                    com.Parameters.AddWithValue("procedureId", procId);

                   
       
                    com.CommandText = "INSERT INTO Procedure_Animal VALUES(@procedureId,@AnimalId,@Date)";
                    com.ExecuteNonQuery();
                }

                tran.Commit();

                Animal animal = new Animal
                {
                    Name = req.Name,
                    Type = req.Type,
                    AdmissionDate = req.AdmissionDate,
                    LastNameOfOwner = LastName

                };
                return animal;

            }

            return null;
        }
    }
}
