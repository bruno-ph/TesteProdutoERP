using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace GeradorTxt
{
    /// <summary>
    /// Implementa a geração do Leiaute 1.
    /// IMPORTANTE: métodos NÃO marcados como virtual de propósito.
    /// O candidato deve decidir onde permitir override para suportar versões futuras.
    /// </summary>
    public class GeradorArquivoBase
    {

        public void Gerar(List<Empresa> empresas, string outputPath)
        {
            var sb = new StringBuilder();
            foreach (var emp in empresas)
            {
                ListarEmpresa(sb, emp);

            }
            File.WriteAllText(outputPath, sb.ToString(), Encoding.UTF8);
        }

        protected string ToMoney(decimal val)
        {
            // Força ponto como separador decimal, conforme muitos leiautes.
            return val.ToString("0.00", CultureInfo.InvariantCulture);
        }

        protected virtual void ListarEmpresa(StringBuilder sb,Empresa emp)
        {
            EscreverTipo00(sb, emp);
            foreach (var doc in emp.Documentos)
            {
                ListarDocumento(sb, doc);
            }
        }

        protected virtual void ListarDocumento(StringBuilder sb, Documento doc)
        {
            EscreverTipo01(sb, doc);
            decimal valorDocumento = doc.Valor;
            decimal somatorioValorItens = 0m;
            foreach (var item in doc.Itens)
            {
                ListarItem(sb, item);
                somatorioValorItens += item.Valor;
            }

            if (somatorioValorItens != valorDocumento)
            {
                throw new InvalidDataException("Somatório dos valores de itens diferente do valor do " +
                    "documento. Verifique o arquivo de entrada e tente novamente");
            }
        }

        protected virtual void ListarItem(StringBuilder sb, ItemDocumento item)
        {
            EscreverTipo02(sb, item);
        }

        protected virtual void EscreverTipo00(StringBuilder sb, Empresa emp)
        {
            // 00|CNPJEMPRESA|NOMEEMPRESA|TELEFONE
            sb.Append("00").Append("|")
              .Append(emp.CNPJ).Append("|")
              .Append(emp.Nome).Append("|")
              .Append(emp.Telefone).AppendLine();
        }

        protected virtual void EscreverTipo01(StringBuilder sb, Documento doc)
        {
            // 01|MODELODOCUMENTO|NUMERODOCUMENTO|VALORDOCUMENTO
            sb.Append("01").Append("|")
              .Append(doc.Modelo).Append("|")
              .Append(doc.Numero).Append("|")
              .Append(ToMoney(doc.Valor)).AppendLine();
        }

        protected virtual void EscreverTipo02(StringBuilder sb, ItemDocumento item)
        {
            // 02|DESCRICAOITEM|VALORITEM
            sb.Append("02").Append("|")
              .Append(item.Descricao).Append("|")
              .Append(ToMoney(item.Valor)).AppendLine();
        }
    }
}
