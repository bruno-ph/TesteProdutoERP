using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorTxt
{
    class GeradorArquivoLeiaute2: GeradorArquivoBase
    {
        public override void Gerar(List<Empresa> empresas, string outputPath)
        {
            var sb = new StringBuilder();
            foreach (var emp in empresas)
            {
                EscreverTipo00(sb, emp);
                foreach (var doc in emp.Documentos)
                {
                    EscreverTipo01(sb, doc);
                    decimal valorDocumento = doc.Valor;
                    decimal somatorioValorItens = 0m;
                    foreach (var item in doc.Itens)
                    {
                        EscreverTipo02(sb, item);
                        somatorioValorItens += item.Valor;
                        foreach (var categoria in item.Categorias) 
                        {
                            EscreverTipo03(sb, categoria);
                        }
                    }

                    if (somatorioValorItens != valorDocumento)
                    {
                        throw new InvalidDataException("Somatório dos valores de itens diferente do valor do" +
                            "documento. Verifique o arquivo de entrada e tente novamente");
                    }
                }
            }
            File.WriteAllText(outputPath, sb.ToString(), Encoding.UTF8);
        }

        protected override void EscreverTipo02(StringBuilder sb, ItemDocumento item)
        {
            // 02|NUMEROITEM|DESCRICAOITEM|VALORITEM
            sb.Append("02").Append("|")
              .Append(item.NumeroItem).Append("|")
              .Append(item.Descricao).Append("|")
              .Append(ToMoney(item.Valor)).AppendLine();
        }

        protected void EscreverTipo03(StringBuilder sb, CategoriaItem categoria)
        {
            // 02|NUMEROCATEGORIA|DESCRICAOCATEGORIA
            sb.Append("03").Append("|")
              .Append(categoria.NumeroCategoria).Append("|")
              .Append(categoria.DescricaoCategoria).AppendLine();
        }

    }
}
