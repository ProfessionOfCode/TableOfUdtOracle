create or replace PACKAGE STEPANEDBAPCKG AUTHID CURRENT_USER IS
/* TODO enter package declarations (types, exceptions, methods etc) here */ 

PROCEDURE CALL_P_CREER_DEM_PERMIS(p_type_demande_rec C##STEPHANE.TYPE_DEMANDE_REC,
                                p_type_demande_doc_tab C##STEPHANE.TYPE_DEMANDE_DOC_TAB,
                                p_resultat      in out varchar2,
                                p_resultat_message  in out varchar2);

END STEPANEDBAPCKG;