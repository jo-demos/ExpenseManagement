$(".escolhaMes").on("change", function () {
  var id = this.value;

  carregarDadosGastosTotaisMes(id);
});

function carregarDadosGastosTotaisMes(id = 1) {
  $.ajax({
    url: "/Despesas/GastosTotaisMes",
    method: "POST",
    data: { idMes: id },
    success: function (data) {
      $("#graficoGastoTotalMes").remove();
      $("div.graficoGastoTotalMes").append(
        '<canvas id="graficoGastoTotalMes" style="width: 400px; height: 400px;"></canvas>'
      );

      const ctx = document
        .getElementById("graficoGastoTotalMes")
        .getContext("2d");

      const grafico = new Chart(ctx, {
        type: "doughnut",
        data: {
          labels: ["Restante", "Total gasto"],
          datasets: [
            {
              label: "Total gasto",
              backgroundColor: ["#27ae60", "#c0392b"],
              data: [data.salario - data.valorTotalGasto, data.valorTotalGasto],
            },
          ],
        },
        options: {
          responsive: false,
          title: {
            display: true,
            text: "Total gasto no mês",
          },
        },
      });
    },
  });
}
