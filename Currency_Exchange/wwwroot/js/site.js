const currencyEl_one = document.getElementById('currency-one');
const amountEl_one = document.getElementById('amount-one');
const currencyEl_two = document.getElementById('currency-two');
const amountEl_two = document.getElementById('amount-two');

const rateEl = document.getElementById('rate');
const swap = document.getElementById('swap');
const buy_button = document.getElementById('buy');
const sell_button = document.getElementById('sell');

// Fetch exchange rates and update the DOM
function calculate() {
  const currency_one = currencyEl_one.value;
  const currency_two = currencyEl_two.value;

  fetch(`https://api.exchangerate-api.com/v4/latest/${currency_one}`)
    .then(res => res.json())
    .then(data => {
      // console.log(data);
      const rate = data.rates[currency_two];
      
      rateEl.innerText = `1 ${currency_one} = ${rate} ${currency_two}`;

      amountEl_two.value = (amountEl_one.value * rate).toFixed(2)
    })

}

function buy() {

  var myHeaders = new Headers();
  myHeaders.append("Authorization", "Basic aWxnYXo6ZGVkZWRlZGU=");
  myHeaders.append("Content-Type", "application/json");

  var raw = "     \n    {\"properties\": {\"content_type\": \"application/json\"},\n    \"routing_key\": \"currency_queue\",\n    \"payload\":\n    \"{\n        \\\"currency\\\" : \\\"" + currencyEl_one.value + "\\\",\n        \\\"amount\\\" : " + amountEl_one.value +",\n        \\\"oper\\\" : \\\"Buy\\\",\n}\n\",\n    \"payload_encoding\": \"string\"\n    }\n";

  var requestOptions = {
    method: 'POST',
    headers: myHeaders,
    body: raw,
    redirect: 'follow'
  };

  fetch("http://localhost:15672/api/exchanges/%2f/amq.default/publish", requestOptions)
    .then(response => response.text())
    .then(result => console.log(result))
    .catch(error => console.log('error', error));
}

function sell() {

  var myHeaders = new Headers();
  myHeaders.append("Authorization", "Basic aWxnYXo6ZGVkZWRlZGU=");
  myHeaders.append("Content-Type", "application/json");
  
  var raw = "     \n    {\"properties\": {\"content_type\": \"application/json\"},\n    \"routing_key\": \"currency_queue\",\n    \"payload\":\n    \"{\n        \\\"currency\\\" : \\\"" + currencyEl_one.value + "\\\",\n        \\\"amount\\\" : " + amountEl_one.value +",\n        \\\"oper\\\" : \\\"Sell\\\",\n}\n\",\n    \"payload_encoding\": \"string\"\n    }\n";
  
  var requestOptions = {
    method: 'POST',
    headers: myHeaders,
    body: raw,
    redirect: 'follow'
  };
  
  fetch("http://localhost:15672/api/exchanges/%2f/amq.default/publish", requestOptions)
    .then(response => response.text())
    .then(result => console.log(result))
    .catch(error => console.log('error', error));
}

function calculate() {
  const currency_one = currencyEl_one.value;
  const currency_two = currencyEl_two.value;

  fetch(`https://api.exchangerate-api.com/v4/latest/${currency_one}`)
    .then(res => res.json())
    .then(data => {
      // console.log(data);
      const rate = data.rates[currency_two];
      
      rateEl.innerText = `1 ${currency_one} = ${rate} ${currency_two}`;

      amountEl_two.value = (amountEl_one.value * rate).toFixed(2)
    })

}

// Event listeners
currencyEl_one.addEventListener('change', calculate);
amountEl_one.addEventListener('input', calculate);
currencyEl_two.addEventListener('change', calculate);
amountEl_two.addEventListener('input', calculate);
buy_button.addEventListener('click', buy);
sell_button.addEventListener('click', sell);
swap.addEventListener('click', ()=> {
  const temp = currencyEl_one.value;
  currencyEl_one.value = currencyEl_two.value;
  currencyEl_two.value = temp;
  calculate()
})

calculate()
