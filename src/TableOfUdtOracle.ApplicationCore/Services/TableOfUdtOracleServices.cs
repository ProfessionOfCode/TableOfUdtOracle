using TableOfUdtOracle.ApplicationCore.Dtos.Requests;
using TableOfUdtOracle.ApplicationCore.Dtos.Responses;
using TableOfUdtOracle.ApplicationCore.Services.Interfaces;
using TableOfUdtOracle.Domain.Entities;
using TableOfUdtOracle.Infrastructure.Interface;

namespace TableOfUdtOracle.ApplicationCore.Services
{
    public class TableOfUdtOracleServices : ITableOfUdtOracleServices
    {
        private readonly ITableOfUdtOracleDbContext _context;

        public TableOfUdtOracleServices(ITableOfUdtOracleDbContext context)
        {
            _context = context;
        }
        public async Task<AppResponse> AddDemandeTableOfUdtOracleAsync(TableOfUdtOracleRequest request)
        {
            if (request is null || request.DemandePermis is null || request.DemandePermisDoc is null) throw new ArgumentNullException($"{nameof(TableOfUdtOracleRequest)} and its members can not be null");
            var response = new AppResponse();

            var resultOfcontextOp = await _context.AddDemandeToDB(GetDemandePermisEntityFromRequest(request.DemandePermis), GetDemandePermisDocumentsEntityFromRequest(request.DemandePermisDoc));
            response.IsSuccess = resultOfcontextOp!=null && !resultOfcontextOp.Contains("failed", StringComparison.CurrentCultureIgnoreCase);
            response.Message = resultOfcontextOp!;
            return response;
        }

        public TYPE_DEMANDE_DOC_TAB GetDemandePermisDocumentsEntityFromRequest(DemandePermisDocuments[] permisDocuments)
        {
            var typeDemandeDocResoTab = new TYPE_DEMANDE_DOC_TAB();
            var documents = new TYPE_DEMANDE_DOC_REC[permisDocuments.Length];
            int count = 0;
            foreach (var document in permisDocuments)
            {
                documents[count] =  new TYPE_DEMANDE_DOC_REC
                {
                    //CODE_DOC = document.CODE_DOC,
                    DATA_CONTENT = document.DATA_CONTENT,
                    MIME_TYPE = document.MIME_TYPE,
                    FICH_NOM = document.FICH_NOM
                };
                count++;
            }
            typeDemandeDocResoTab.Value = documents;
            return typeDemandeDocResoTab;
        }

        public TYPE_DEMANDE_REC GetDemandePermisEntityFromRequest(DemandePermis demande)
        {
            return new TYPE_DEMANDE_REC()
            {
                //COURRIEL = demande.COURRIEL,
                NOM = demande.NOM,
                PRENOM = demande.PRENOM,
                REQUETE_ID = demande.REQUETE_ID
            };
        }
    }
}
