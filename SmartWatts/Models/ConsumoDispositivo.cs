using Microsoft.ML.Data;

namespace SmartWatts.Models
{
    public class ConsumoDispositivo
    {
        [LoadColumn(0)] 
        public string DataRegistro { get; set; }

        [LoadColumn(1)] 
        public float Consumo { get; set; }

        [LoadColumn(2)] 
        public float CustoEstimado { get; set; }

        [LoadColumn(3)] 
        public float CustoConsumo { get; set; }
    }

    public class ConsumoDispositivoPrediction
    {
        [ColumnName("Score")]
        public float PredictedConsumo { get; set; }
    }
}
