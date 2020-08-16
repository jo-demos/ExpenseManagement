$(".escolhaMes").on("change", function () {
  var id = this.value;

  carregarDadosGastosMes(id);
});

function carregarDadosGastosMes(id = 1) {
  $.ajax({
    url: "/Despesas/GastosMes",
    method: "POST",
    data: { idMes: id },
    success: function (data) {
      $("#graficoGastoMes").remove();
      $("div.graficoGastoMes").append(
        '<canvas id="graficoGastoMes" style="width: 400px; height: 400px;"></canvas>'
      );

      const ctx = document.getElementById("graficoGastoMes").getContext("2d");

      const grafico = new Chart(ctx, {
        type: "doughnut",
        data: {
          labels: pegarTiposDespesas(data),
          datasets: [
            {
              label: "Gastos por despesa",
              backgroundColor: pegarCores(data.length),
              hoverBackgroundColor: pegarCores(data.length),
              data: pegarValores(data),
            },
          ],
        },
        options: {
          responsive: false,
          title: {
            display: true,
            text: "Gastos por despesa",
          },
        },
      });
    },
  });
}
