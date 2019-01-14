using Newtonsoft.Json;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;


namespace ExcelReader
{
    class Program
    {
        static void Main(string[] args)
        {
            //É necessário configurar a propriedade "Copy to oytput directory" como true. Se a propriedade estiver "false" o programa não consegue localizar o arquivo no caminho especificado.
            string fileExcel =  @"Assets\Base_de_dados.xlsx";
            
            FileInfo existingFile = new FileInfo(fileExcel);

            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                
                ExcelWorksheet worksheet = package.Workbook.Worksheets[1]; //Pega a primeira planilha da pasta de trabalho
                int colCount = worksheet.Dimension.End.Column;  //contador de colunas
                int rowCount = worksheet.Dimension.End.Row;     //contador de linhas
                List<CatalagoItem> catalago = new List<CatalagoItem>();
                

                //Percorre as linhas
                for (int row = 1; row <= rowCount; row++)
                {
                    //Ignora a primeira linha de cabeçalho. Ou seja, só vai pegar os dados da segunda linha em diante
                    if (row > 1)
                    {
                        CatalagoItem item = new CatalagoItem();

                        //Percorre as colunas
                        for (int col = 1; col <= colCount; col++)
                        {

                            var valor = worksheet.Cells[row, col].Value?.ToString().Trim();

                            //Para cada coluna seta uma propriedade diferente do objeto item (instância de CatalogoItem)
                            switch (col)
                            {
                                case 1:
                                    item.Artista = valor;
                                    break;
                                case 2:
                                    item.Codigo = valor;
                                    break;
                                case 3:
                                    item.Titulo = valor;
                                    break;
                                case 4:
                                    item.Inicio = valor;
                                    break;
                                case 5:
                                    item.Cartucho = valor;
                                    break;
                                default:
                                    break;
                            }

                        }

                        catalago.Add(item);
                    }
                }

                //Serializa o objeto catalogo num Json
                string jsonSaida = JsonConvert.SerializeObject(catalago);



            }

        }

        
    }

    
}


