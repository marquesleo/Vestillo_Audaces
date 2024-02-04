using System.Collections.Generic;
using TemplateAudacesApi.Models;

namespace TemplateAudacesApi.Services
{
    public class FichaModeloService
    {

        public object retornarFichaModelo()
        {

            var produto = new Garment()
            {
                name = "FICHAMODELO",
                uid = "FICHAMODELO",
                description = "UTILIZAR ESSE MODELO PARA FICHA MODELO",

            };

            CustomFields customFieldsCores = RetornarCoresDoVestillo();
            CustomFields customFieldsTamanho = RetornarTamanhosVestillo();
            CustomFields customFieldsColecoes = RetornarColecoesVestillo();
            CustomFields customFieldSegmentos = RetornarSegmentosVestillo();
            CustomFields customFieldGrupo = RetornarGrupoDeProdutoVestillo();
            CustomFields customFieldReferencia = RetornarCampoReferencia();
            CustomFields customAnoVestillo = RetornarAnoVestillo();
           // CustomFields customFieldDestino = RetornarDetinoDoVestillo();
            produto.custom_fields.Add(customFieldsCores);
            produto.custom_fields.Add(customFieldsTamanho);
            produto.custom_fields.Add(customFieldsColecoes);
            produto.custom_fields.Add(customFieldSegmentos);
            produto.custom_fields.Add(customFieldGrupo);
            produto.custom_fields.Add(customFieldReferencia);
            produto.custom_fields.Add(customAnoVestillo);
           // produto.custom_fields.Add(customFieldDestino);
            return produto;
        }



        public static CustomFields RetornarTamanhosVestillo()
        {
            var customFieldsTamanho = new CustomFields();
            customFieldsTamanho.name = "Size";
            customFieldsTamanho.type = "string";
            var lstTamanho = new List<string>();
            Services.Utils.lstTamanho.ForEach(t => lstTamanho.Add(t.Id + "-" + t.Descricao));

            customFieldsTamanho.options = lstTamanho;
            customFieldsTamanho.editable = "true";
            return customFieldsTamanho;
        }

        public static CustomFields RetornarAnoVestillo()
        {
            var customFieldsAno = new CustomFields();
            customFieldsAno.name = "ANO";
            customFieldsAno.type = "string";
            var lstAno = new List<string>();
            for (int i = 2020; i < 2041; i++)
            {
                lstAno.Add(i.ToString());
            }

            customFieldsAno.options = lstAno;
            customFieldsAno.editable = "true";
            return customFieldsAno;
        }


        public static CustomFields RetornarCampoReferencia()
        {
            var customFieldReferencia = new CustomFields();
            customFieldReferencia.name = "Referencia";
            customFieldReferencia.type = "string";
            customFieldReferencia.value = "";
            customFieldReferencia.editable = "true";
            return customFieldReferencia;
        }

        public static CustomFields RetornarColecoesVestillo()
        {
            var customFieldsColecao = new CustomFields();
            customFieldsColecao.name = "COLECAO";
            customFieldsColecao.type = "string";
            var lstColeacao = new List<string>();
            Services.Utils.lstColecao.ForEach(t => lstColeacao.Add(t.Descricao));
            customFieldsColecao.options = lstColeacao;
            customFieldsColecao.editable = "true";
            return customFieldsColecao;
        }

        public static CustomFields RetornarCoresDoVestillo()
        {
            var customFieldsCores = new CustomFields();
            customFieldsCores.name = "Cor";
            customFieldsCores.type = "string";
            var lstCores = new List<string>();
            Services.Utils.lstCor.ForEach(c => lstCores.Add(c.Id + "-" + c.Descricao));
            customFieldsCores.options = lstCores;
            customFieldsCores.editable = "true";
            
            return customFieldsCores;
        }

        public static CustomFields RetornarDetinoDoVestillo()
        {
            var customFieldsCores = new CustomFields();
            customFieldsCores.name = "Destinos";
            customFieldsCores.type = "string";
            var lstDestino = new List<string>();
            Services.Utils.lstDestinos.ForEach(c => lstDestino.Add(c.Id + "-" + c.Descricao));
            customFieldsCores.options = lstDestino;
            customFieldsCores.editable = "true";
            return customFieldsCores;
        }

        public static CustomFields RetornarSegmentosVestillo()
        {
            var customSegmentosVestillo = new CustomFields();
            customSegmentosVestillo.name = "SEGMENTO";
            customSegmentosVestillo.type = "string";
            var lstSegmento = new List<string>();
            Services.Utils.lstSegmento.ForEach(c => lstSegmento.Add(c.Id + "-" + c.Descricao));
            customSegmentosVestillo.options = lstSegmento;
            customSegmentosVestillo.editable = "true";
            return customSegmentosVestillo;
        }

        public static CustomFields RetornarGrupoDeProdutoVestillo()
        {
            var customGrupoVestillo = new CustomFields();
            customGrupoVestillo.name = "GRUPO";
            customGrupoVestillo.type = "string";
            var lstGrupoDeProduto = new List<string>();
            Services.Utils.lstGrupo.ForEach(c => lstGrupoDeProduto.Add(c.Id + "-" + c.Descricao));
            customGrupoVestillo.options = lstGrupoDeProduto;
            customGrupoVestillo.editable = "true";
            return customGrupoVestillo;
        }

    }
}
