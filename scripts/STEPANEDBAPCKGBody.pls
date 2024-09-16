create or replace PACKAGE BODY STEPANEDBAPCKG AS

  PROCEDURE CALL_P_CREER_DEM_PERMIS(p_type_demande_rec C##STEPHANE.TYPE_DEMANDE_REC,
                                p_type_demande_doc_tab C##STEPHANE.TYPE_DEMANDE_DOC_TAB,
                                p_resultat      in out varchar2,
                                p_resultat_message  in out varchar2) AS
    v_resultat Varchar2(1);
    v_resultat_message Varchar2(4000);
BEGIN
-- TODO: Implementation required for PROCEDURE STEPANEDBAPCKG.P_CREER_DEM_PERMIS
--Insertion of demande/request
    v_resultat := 'S';
    v_resultat_message := 'Operation completed successfully';
    p_resultat := v_resultat;
    p_resultat_message := v_resultat_message;
    INSERT INTO pnw_dem_exerc_permis (nom, prenom, requete_id)
    VALUES (p_type_demande_rec.nom, p_type_demande_rec.prenom, p_type_demande_rec.requete_id);
--Insertion of associated documents        
    FOR i IN 1..p_type_demande_doc_tab.COUNT LOOP
            INSERT INTO pnw_doc_exerc_files (fich_contenu, fich_nom, mime_type)
            VALUES (p_type_demande_doc_tab(i).data_content, p_type_demande_doc_tab(i).fich_nom, p_type_demande_doc_tab(i).mime_type);                
    END LOOP;
EXCEPTION
    WHEN OTHERS THEN
        v_resultat := 'E';
        v_resultat_message := 'Operation failed'|| SQLERRM; 
        p_resultat := v_resultat;
        p_resultat_message := v_resultat_message;
  END CALL_P_CREER_DEM_PERMIS;

END STEPANEDBAPCKG;