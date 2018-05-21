using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Monsees;

// MonseesDBStaticTables
// D.S.Harmor
// Description - A singleton class the builds dictionaries of common information to minimize accessing the database server
// 08/19/2011 - Initial Version
// 
// 

public class MonseesDBStaticTables
{

    public static readonly MonseesDBStaticTables m_Instance = new MonseesDBStaticTables();
    public static MonseesDBStaticTables Instance { get { return m_Instance; } }

    public Dictionary<string, string> EmployeeDictionary;
    public Dictionary<string, string> MachineDictionary;
    public Dictionary<string, string> SetupDictionary;
    public Dictionary<string, string> RolespDictionary;
     
    // Prevent instance creation from other classes
    private MonseesDBStaticTables()
    {
        EmployeeDictionary = new Dictionary<string, string>();
        MachineDictionary = new Dictionary<string, string>();
        SetupDictionary = new Dictionary<string, string>();
        RolespDictionary = new Dictionary<string, string>();
        LoadEmployees();
        LoadMachines();
        LoadSetups();
    }


    private void LoadEmployees()
    {
        MonseesDB objMonseesDB;
        System.Data.SqlClient.SqlDataReader reader;

        objMonseesDB = new MonseesDB();

        string sqlstring = "Select * from [Employees] where Active = 1 ORDER BY name ASC";

        // execute sql command and store a return values in reade
        reader = objMonseesDB.ExecuteReader(sqlstring);
        while (reader.Read())
        {
            EmployeeDictionary.Add(reader["name"].ToString().Trim(), reader["EmployeeID"].ToString().Trim());
            RolespDictionary.Add(reader["name"].ToString().Trim(), reader["Role"].ToString().Trim());
        }
        objMonseesDB.Close();
    }

    private void LoadMachines()
    {
        MonseesDB objMonseesDB;
        System.Data.SqlClient.SqlDataReader reader;

        objMonseesDB = new MonseesDB();

        string sqlstring = "Select [MachineID],[Machine] from [Machines]";

        // execute sql command and store a return values in reader
        reader = objMonseesDB.ExecuteReader(sqlstring);
        while (reader.Read())
        {
            MachineDictionary.Add(reader["Machine"].ToString().Trim(), reader["MachineID"].ToString().Trim());
        }
        objMonseesDB.Close();
    }

    private void LoadSetups()
    {
        MonseesDB objMonseesDB;
        System.Data.SqlClient.SqlDataReader reader;

        objMonseesDB = new MonseesDB();

        string sqlstring = "Select [OperationID],[OperationName] from [Operation]";

        // execute sql command and store a return values in reade
        reader = objMonseesDB.ExecuteReader(sqlstring);
        while (reader.Read())
        {
            SetupDictionary.Add(reader["OperationName"].ToString().Trim(), reader["OperationID"].ToString().Trim());
        }
        objMonseesDB.Close();

       
    }

}
