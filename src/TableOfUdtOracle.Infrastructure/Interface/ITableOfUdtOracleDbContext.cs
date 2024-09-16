
using TableOfUdtOracle.Domain.Entities;

namespace TableOfUdtOracle.Infrastructure.Interface
{
    public interface ITableOfUdtOracleDbContext
    {
        Task<string> AddDemandeToDB(TYPE_DEMANDE_REC demande, TYPE_DEMANDE_DOC_TAB demandeDocTab);
    }
}
