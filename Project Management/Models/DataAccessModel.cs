using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace Project_Management.Models
{
    public class DataAccessModel
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["sqlconnectionstring_Common"].ConnectionString;
        protected string ConnectionString
        {
            get
            {
                return this.connectionString;
            }
            set
            {
                this.connectionString = value;
            }
        }
        public DataSet GetData(object[] objParameters, string strSPName)
        {
            //DataSet ds;
            DataSet dsData = new DataSet();
            SqlDatabase database = new SqlDatabase(this.ConnectionString);
            DbCommand sqlCmd = database.DbProviderFactory.CreateCommand();
            sqlCmd = database.GetStoredProcCommand(strSPName, objParameters);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandTimeout = 7200;

            try
            {
                dsData = database.ExecuteDataSet(sqlCmd);
            }

            catch (Exception ex)
            {
                dsData = null;
            }
            return dsData;
        }




        public DataSet GetData(string strSPName)
        {
            DataSet dsData = new DataSet();
            SqlDatabase database = new SqlDatabase(this.ConnectionString);
            dsData = database.ExecuteDataSet(strSPName);
            //SqlConnection objConn = new SqlConnection(strConnectionString);
            //SqlCommand objCmd = new SqlCommand();
            //SqlDataAdapter objDA;
            //try
            //{
            //    objCmd = GetCommand(strSPName, objConn);
            //    objDA = new SqlDataAdapter(objCmd);
            //    objDA.Fill(objDS);
            //    return objDS;
            //}
            //catch (Exception ex)
            //{
            //    return null;
            //}
            //finally
            //{
            //    objConn.Close();
            //    //objCmd.Connection.Close();
            //}
            return dsData;
        }



        public int SaveData(object[] objParameters, string strSPName)
        {
            SqlDatabase database = new SqlDatabase(this.ConnectionString);
            return database.ExecuteNonQuery(strSPName, objParameters);
            //SqlConnection objConn = new SqlConnection(strConnectionString);
            //SqlCommand objCmd = new SqlCommand();
            //try
            //{
            //    objCmd = GetCommand(objParameters, strSPName, objConn);
            //    objCmd.Connection.Open();
            //    return objCmd.ExecuteNonQuery();
            //}
            //catch (Exception ex)
            //{
            //    return -1;
            //}
            //finally
            //{
            //    objConn.Close();
            //    //objCmd.Connection.Close();
            //}
        }
        public int SaveData(object[] objParameters, string strSPName, string _ConnectionString)
        {
            SqlDatabase database = new SqlDatabase(_ConnectionString);
            return database.ExecuteNonQuery(strSPName, objParameters);
            //SqlConnection objConn = new SqlConnection(strConnectionString);
            //SqlCommand objCmd = new SqlCommand();
            //try
            //{
            //    objCmd = GetCommand(objParameters, strSPName, objConn);
            //    objCmd.Connection.Open();
            //    return objCmd.ExecuteNonQuery();
            //}
            //catch (Exception ex)
            //{
            //    return -1;
            //}
            //finally
            //{
            //    objConn.Close();
            //    //objCmd.Connection.Close();
            //}
        }

        public DataSet SaveAndGetData(object[] objParameters, string strSPName)
        {
            DataSet dsData = new DataSet();
            SqlDatabase database = new SqlDatabase(this.ConnectionString);
            dsData = database.ExecuteDataSet(strSPName, objParameters);
            //IDataAdapter objDA;
            //SqlConnection objConn = new SqlConnection(strConnectionString);
            //SqlCommand objCmd = new SqlCommand();
            //try
            //{
            //    objCmd = GetCommand(objParameters, strSPName, objConn);
            //    objDA = new SqlDataAdapter(objCmd);
            //    objDA.Fill(objDS);
            //    return objDS;
            //}
            //catch (Exception ex)
            //{
            //    return null;
            //}
            //finally
            //{
            //    objConn.Close();
            //    objCmd.Connection.Close();
            //}
            return dsData;
        }

        public DataTable getErrorTable()
        {
            DataTable DTError = new DataTable();
            DTError.Columns.Add("ErrNo");
            DTError.Columns.Add("ErrMsg");
            DataRow DRError = DTError.NewRow();
            DRError["ErrNo"] = "01";
            DRError["ErrMsg"] = "Error on reteriving data, please contact administrator";
            DTError.Rows.Add(DRError);
            DTError.AcceptChanges();
            return DTError;
        }
    }
}