CREATE OR REPLACE TYPE TYPE_DEMANDE_REC AS OBJECT 
( /* TODO enter attribute and method declarations here */ 
    REQUETE_ID          VARCHAR2(50 BYTE),
    PRENOM              VARCHAR2(100 BYTE),
    NOM                 VARCHAR(100 BYTE) 
)