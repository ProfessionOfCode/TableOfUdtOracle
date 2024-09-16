CREATE OR REPLACE TYPE TYPE_DEMANDE_DOC_REC AS OBJECT 
( /* TODO enter attribute and method declarations here */ 
    DATA_CONTENT    BLOB,
    MIME_TYPE       VARCHAR2(255 BYTE),
    FICH_NOM        VARCHAR(255 BYTE)
)