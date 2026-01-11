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
        protected override void ListarItem(StringBuilder sb, ItemDocumento item)
        {
            linhasPorTipo[2] += 1;
            EscreverTipo02(sb, item);
            if (item.Categorias!=null)
            {
                foreach (var categoria in item.Categorias)
                {
                    ListarCategoria(sb, categoria);
                }
            }
        }

        protected virtual void ListarCategoria(StringBuilder sb, CategoriaItem categoria)
        {
            linhasPorTipo[3] += 1;
            EscreverTipo03(sb, categoria);
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
