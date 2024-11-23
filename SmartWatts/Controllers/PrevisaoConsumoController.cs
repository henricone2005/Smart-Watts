using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using Microsoft.ML.Data;
using System.IO;
using SmartWatts.Models;

namespace SmartWatts.Controllers
{
    // Representa o modelo de dados de consumo de energia.
    public class ConsumoEnergia
    {
        [LoadColumn(0)] public string DataRegistro { get; set; }
        [LoadColumn(1)] public float Consumo { get; set; }
        [LoadColumn(2)] public float CustoEstimado { get; set; }
        [LoadColumn(3)] public float CustoConsumo { get; set; }
    }

    // Representa a previsão do consumo de energia.
    public class ConsumoEnergiaPrediction
    {
        [ColumnName("Score")]
        public float PredictedConsumo { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class PrevisaoConsumoController : ControllerBase
    {
        // Caminhos para o modelo treinado e arquivo de dados de treinamento
        private readonly string caminhoModelo = Path.Combine(Environment.CurrentDirectory, "wwwroot", "MLModels", "ModeloConsumo.zip");
        private readonly string caminhoTreinamento = Path.Combine(Environment.CurrentDirectory, "Data", "consumo-train.csv");
        private readonly MLContext mlContext;

        public PrevisaoConsumoController()
        {
            mlContext = new MLContext();

            // Se o modelo não existir, inicia o treinamento
            if (!System.IO.File.Exists(caminhoModelo))
            {
                Console.WriteLine("Modelo não encontrado. Iniciando treinamento...");
                TreinarModelo();
            }
        }

        private void TreinarModelo()
        {
            var pastaModelo = Path.GetDirectoryName(caminhoModelo);
            if (!Directory.Exists(pastaModelo))
            {
                Directory.CreateDirectory(pastaModelo);
            }

            // Carregar os dados de treinamento
            IDataView dadosTreinamento = mlContext.Data.LoadFromTextFile<ConsumoEnergia>(
                path: caminhoTreinamento, hasHeader: true, separatorChar: ',');

            // Definir o pipeline de transformação de dados e modelo de regressão
            var pipeline = mlContext.Transforms.Concatenate("Features", nameof(ConsumoEnergia.Consumo), nameof(ConsumoEnergia.CustoEstimado))
                .Append(mlContext.Regression.Trainers.Sdca(labelColumnName: nameof(ConsumoEnergia.CustoConsumo), featureColumnName: "Features"));

            // Treinar o modelo
            var modelo = pipeline.Fit(dadosTreinamento);

            // Salvar o modelo treinado
            mlContext.Model.Save(modelo, dadosTreinamento.Schema, caminhoModelo);
        }

        // Endpoint para prever o consumo de energia com base nos dados recebidos
        [HttpPost("prever")]
        public ActionResult<ConsumoEnergiaPrediction> PreverConsumo([FromBody] ConsumoEnergia dadosConsumo)
        {
            // Verifica se o modelo já foi treinado
            if (!System.IO.File.Exists(caminhoModelo))
            {
                return BadRequest("O modelo ainda não foi treinado.");
            }

            // Carregar o modelo treinado
            ITransformer modelo;
            using (var stream = new FileStream(caminhoModelo, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                modelo = mlContext.Model.Load(stream, out var modeloSchema);
            }

            // Criar o motor de previsão
            var enginePrevisao = mlContext.Model.CreatePredictionEngine<ConsumoEnergia, ConsumoEnergiaPrediction>(modelo);

            // Realizar a previsão
            var previsao = enginePrevisao.Predict(dadosConsumo);

            // Retornar a previsão
            return Ok(previsao);
        }

        // Endpoint para baixar o modelo treinado
        [HttpGet("baixar-modelo")]
        public IActionResult BaixarModelo()
        {
            if (!System.IO.File.Exists(caminhoModelo))
            {
                return NotFound("Modelo não encontrado.");
            }

            // Carregar o modelo como bytes
            byte[] fileContents = System.IO.File.ReadAllBytes(caminhoModelo);

            // Retornar o arquivo do modelo
            return File(fileContents, "application/octet-stream", "ModeloConsumo.zip");
        }
    }
}
