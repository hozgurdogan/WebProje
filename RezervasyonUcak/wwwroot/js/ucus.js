


console.log("sadsadasdas");

document.getElementById("sendButton").addEventListener("click", function () {
	console.log("sadsadasdas");

	var selectedId = document.getElementById("flightId").value;
	var selectedDate = document.getElementById("flightDate").value;
	console.log(selectedId);




	fetch(`https://localhost:7004/Employees/Musteri/UcusGetir?konumId=${selectedId}&tarih_=${selectedDate}`)
		.then(response => response.json())
		.then(data => console.log(data))
		.catch(error => console.error('Hata:', error))





});









