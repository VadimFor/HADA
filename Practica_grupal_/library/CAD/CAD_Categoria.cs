using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using library.EN;
using System.Data.SqlClient;
using System.Data;

namespace library.CAD
{
    public class CAD_Categoria
    {
        public DataSet leer_Categoria(EN_Categoria en)
        {
            string constr = ConfigurationManager.ConnectionStrings["InmoData"].ToString();// connection string
            SqlConnection con = new SqlConnection(constr);
            con.Open();

            SqlCommand com = new SqlCommand("select * from [dbo].[Categoria]", con);
            // table name   
            SqlDataAdapter data_adapter = new SqlDataAdapter(com);
            DataSet data_set = new DataSet();
            data_adapter.Fill(data_set);  // fill dataset 

            return data_set;
        }
    }
}
