using TableOfUdtOracle.ApplicationCore.Dtos.Requests;
using TableOfUdtOracle.ApplicationCore.Dtos.Responses;
using TableOfUdtOracle.Domain.Entities;

namespace TableOfUdtOracle.ApplicationCore.Services.Interfaces
{
    public interface ITableOfUdtOracleServices
    {
        Task<AppResponse> AddDemandeTableOfUdtOracleAsync(TableOfUdtOracleRequest request);
        TYPE_DEMANDE_DOC_TAB GetDemandePermisDocumentsEntityFromRequest(DemandePermisDocuments[] permisDocuments);
        TYPE_DEMANDE_REC GetDemandePermisEntityFromRequest(DemandePermis demande);
    }
}
