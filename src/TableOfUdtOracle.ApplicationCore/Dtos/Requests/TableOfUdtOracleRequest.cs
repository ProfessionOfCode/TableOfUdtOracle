namespace TableOfUdtOracle.ApplicationCore.Dtos.Requests
{
    public class TableOfUdtOracleRequest
    {
        public DemandePermis DemandePermis { get; set; }
        public DemandePermisDocuments[] DemandePermisDoc { get; set; }
    }

    public class DemandePermis
    {                
        public string PRENOM { get; set; }        
        public string REQUETE_ID { get; set; }
        public string NOM { get; set; }
    }
    
    public class DemandePermisDocuments
    {                
        public byte[] DATA_CONTENT { get; set; }
        public string MIME_TYPE { get; set; }
        public string FICH_NOM { get; set; }
    }

}
