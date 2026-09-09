


SELECT    SUM(dbo.qryFO_FaktorTitrSatr.Mkol3) AS RialForosh, dbo.qryFO_FaktorTitrSatr.NameForoshandeh, 
                      dbo.qryFO_FaktorTitrSatr.NameBrand,dbo.tblFO_Hadaf.Rial, dbo.tblFO_Hadaf.ccMah
                      , dbo.tblFO_Hadaf.CodeDoreh, 
                      dbo.tblFO_Hadaf.ccMarkazPakhsh,
                     dbo.tblFO_Hadaf.Rial- SUM(dbo.qryFO_FaktorTitrSatr.Mkol3) as HadafRialMandeh,
                     (SUM(dbo.qryFO_FaktorTitrSatr.Mkol3)*100)/dbo.tblFO_Hadaf.Rial as DarsadHadaf,
                       100-((SUM(dbo.qryFO_FaktorTitrSatr.Mkol3)*100)/dbo.tblFO_Hadaf.Rial) as DarsadHadafMandeh
                         
FROM         dbo.qryFO_FaktorTitrSatr  RIGHT OUTER JOIN
                      dbo.tblFO_Hadaf ON dbo.qryFO_FaktorTitrSatr.ccForoshandeh = dbo.tblFO_Hadaf.ccForoshandeh AND
                       dbo.qryFO_FaktorTitrSatr.FaktorTarikh  between '13910901' and '13910930' and 
                      dbo.qryFO_FaktorTitrSatr.ccBrand = dbo.tblFO_Hadaf.ccBrand
                      where dbo.qryFO_FaktorTitrSatr.NameForoshandeh is not null and dbo.tblFO_Hadaf.ccMah =9 and dbo.tblFO_Hadaf.CodeDoreh ='1391'
GROUP BY dbo.qryFO_FaktorTitrSatr.NameForoshandeh, dbo.qryFO_FaktorTitrSatr.NameBrand, dbo.tblFO_Hadaf.Tedad, dbo.tblFO_Hadaf.Rial, 
                      dbo.tblFO_Hadaf.ccMah, dbo.tblFO_Hadaf.CodeDoreh, dbo.tblFO_Hadaf.ccMarkazPakhsh
ORDER BY dbo.qryFO_FaktorTitrSatr.NameForoshandeh