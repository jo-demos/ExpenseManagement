const pegarValores = (dados) => {
  let valores = [];

  dados.forEach((item) => valores.push(item.valores));

  return valores;
};

const pegarTiposDespesas = (dados) => {
  let labels = [];

  dados.forEach((item) => labels.push(item.tiposDespesas));

  return labels;
};

const pegarMeses = (dados) => {
  let labels = [];

  dados.forEach((item) => labels.push(item.nomeMeses));

  return labels;
};

const pegarCores = (quantity) => {
  var colours = [];

  while (quantity > 0) {
    var r = Math.floor(Math.random() * 255);
    var g = Math.floor(Math.random() * 255);
    var b = Math.floor(Math.random() * 255);

    colours.push(`rgb(${r}, ${g}, ${b})`);

    quantity--;
  }

  return colours;
};
