using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using TableOfUdtOracle.Domain.Entities;
using TableOfUdtOracle.Infrastructure.Interface;

namespace TableOfUdtOracle.Infrastructure
{
    public class TableOfUdtOracleDbContext : DbContext, ITableOfUdtOracleDbContext
    {
        public TableOfUdtOracleDbContext(){}
        public TableOfUdtOracleDbContext(DbContextOptions options):base(options) { }
        
        public async Task<string> AddDemandeToDB(TYPE_DEMANDE_REC demande, TYPE_DEMANDE_DOC_TAB demandeDocTab)
        {
            if (demandeDocTab == null || demande == null) throw new InvalidOperationException();

            string resultatOp;
            
            using (var conn = (OracleConnection)Database.GetDbConnection())
            {
                try
                {

                    await conn.OpenAsync();
                    using var cmd = conn.CreateCommand();
                    cmd.CommandText = "STEPANEDBAPCKG.CALL_P_CREER_DEM_PERMIS";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add("p_type_demande_rec", OracleDbType.Object, demande, System.Data.ParameterDirection.Input).UdtTypeName = "C##STEPHANE.TYPE_DEMANDE_REC";
                    cmd.Parameters.Add("p_type_demande_doc_tab", OracleDbType.Object, demandeDocTab, System.Data.ParameterDirection.Input).UdtTypeName = "C##STEPHANE.TYPE_DEMANDE_DOC_TAB";
                    cmd.Parameters.Add("p_resultat", OracleDbType.Varchar2, 1, null, System.Data.ParameterDirection.InputOutput);
                    cmd.Parameters.Add("p_resultat_message", OracleDbType.Varchar2, 4000, null, System.Data.ParameterDirection.InputOutput);

                    await cmd.ExecuteNonQueryAsync();

                    string result = cmd.Parameters["p_resultat_message"].Value.ToString()!;
                    resultatOp = result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn?.Close();                    
                }
            }

            return resultatOp;
        }
    }
}
