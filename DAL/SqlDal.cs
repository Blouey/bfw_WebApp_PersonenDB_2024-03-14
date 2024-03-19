using System.ComponentModel;
using bfw_WebApp_PersonenDB_2024_03_14.Models;
using Microsoft.Data.SqlClient;

namespace bfw_WebApp_PersonenDB_2024_03_14.DAL;

public class SqlDal : IAccessible
{
    private readonly SqlConnection _con;
    
    public SqlDal(string conn)
    {
        _con = new SqlConnection(conn);
    }


    public List<Person> GetAllPersons()
    {
        SqlCommand cmd = _con.CreateCommand();
        cmd.CommandText = "SELECT * FROM tbl_Person";
         
        _con.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        List<Person> personList = new();
        
        while (reader.Read())
        {
            Person person = new()
            {
                Pid = reader.GetInt32(0),
                Vorname = reader.GetString(1),
                Nachname = reader.GetString(2),
                Geburtsdatum = reader.IsDBNull(3) ? null : reader.GetString(3),
                Groesse = reader.IsDBNull(4) ? null : reader.GetInt32(4),
                Bild = reader.IsDBNull(5) ? null : reader.GetString(5),
                Gewicht = reader.IsDBNull(6) ? null : reader.GetDouble(6)
            };
            
            personList.Add(person);
        }
        _con.Close();
        return personList;
    }

    public Person GetPersonById(int id)
    {
        SqlCommand cmd = _con.CreateCommand();
        cmd.CommandText = "SELECT * FROM tbl_Person WHERE PID = @id";
        cmd.Parameters.AddWithValue("@id", id);
        
        _con.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        Person person = new();
        
        while (reader.Read())
        {
            person.Pid = reader.GetInt32(0);
            person.Vorname = reader.GetString(1);
            person.Nachname = reader.GetString(2);
            person.Geburtsdatum = reader.IsDBNull(3) ? null : reader.GetString(3);
            person.Groesse = reader.IsDBNull(4) ? null : reader.GetInt32(4);
            person.Bild = reader.IsDBNull(5) ? null : reader.GetString(5);
            person.Gewicht = reader.IsDBNull(6) ? null : reader.GetDouble(6);
        }
        _con.Close();
        return person;
    }

    public int AddPerson(Person person)
    {
        SqlCommand cmd = _con.CreateCommand();
        cmd.CommandText = "INSERT INTO tbl_Person (Vorname, Nachname, Geburtsdatum, Groesse, Bild, Gewicht) VALUES (@vorname, @nachname, @geburtsdatum, @groesse, @bild, @gewicht); SELECT SCOPE_IDENTITY();";
        cmd.Parameters.AddWithValue("@vorname", person.Vorname);
        cmd.Parameters.AddWithValue("@nachname", person.Nachname);
        cmd.Parameters.AddWithValue("@geburtsdatum", person.Geburtsdatum);
        cmd.Parameters.AddWithValue("@groesse", person.Groesse);
        cmd.Parameters.AddWithValue("@bild", person.Bild);
        cmd.Parameters.AddWithValue("@gewicht", person.Gewicht);
        
        _con.Open();
        int addedId = Int32.Parse(cmd.ExecuteScalar().ToString());
        _con.Close();
        return addedId;
    }

    public bool UpdatePerson(Person person)
    {
        SqlCommand cmd = _con.CreateCommand();
        cmd.CommandText = "UPDATE tbl_Person SET Vorname = @vorname, Nachname = @nachname, Geburtsdatum = @geburtsdatum, Groesse = @groesse, Bild = @bild, Gewicht = @gewicht WHERE PID = @pid";
        cmd.Parameters.AddWithValue("@vorname", person.Vorname);
        cmd.Parameters.AddWithValue("@nachname", person.Nachname);
        cmd.Parameters.AddWithValue("@geburtsdatum", person.Geburtsdatum);
        cmd.Parameters.AddWithValue("@groesse", person.Groesse);
        cmd.Parameters.AddWithValue("@bild", person.Bild);
        cmd.Parameters.AddWithValue("@gewicht", person.Gewicht);
        cmd.Parameters.AddWithValue("@pid", person.Pid);
        
        _con.Open();
        bool success = cmd.ExecuteNonQuery() > 0;
        _con.Close();
        return success;
    }

    public bool DeletePersonById(int id)
    {
        SqlCommand cmd = _con.CreateCommand();
        cmd.CommandText = "DELETE FROM tbl_Person WHERE PID = @pid";
        cmd.Parameters.AddWithValue("@pid", id);
        
        _con.Open();
        bool success = cmd.ExecuteNonQuery() > 0;
        _con.Close();
        return success;
    }
}