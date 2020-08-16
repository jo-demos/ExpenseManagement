$(".escolhaMes").on("change", function () {
  var id = this.value;

  carregarDadosGastosTotais(id);
});

function carregarDadosGastosTotais(id = 1) {
  $.ajax({
    url: "/Despesas/GastosTotais",
    method: "POST",
    success: function (data) {
      var el = document.getElementById("graficoGastosTotais");
      new Chart(el, {
        type: "line",
        data: {
          labels: pegarMeses(data),
          datasets: [
            {
              label: "Total gasto",
              data: pegarValores(data),
              backgroundColor: pegarCores(data.length),
              hoverBackgroundColor: pegarCores(data.length),
              fill: false,
              spanGaps: false,
            },
          ],
        },
        options: {
          title: {
            display: true,
            text: "Total gasto",
          },
        },
      });
    },
  });
}
