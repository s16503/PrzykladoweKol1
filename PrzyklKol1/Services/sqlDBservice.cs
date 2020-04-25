using PrzyklKol1.Models;
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

                //string col = columnName.Split(" ")[0];

                // com.Parameters.AddWithValue("Column", col);
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
    }
}
